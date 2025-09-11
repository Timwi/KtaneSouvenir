using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum STouchTransmission
{
    [SouvenirQuestion("What was the transmitted word in {0}?", ThreeColumns6Answers, "that", "this", "not", "your", "all", "new", "was", "can", "has", "but", "one", "may", "what", "which", "their", "use", "any", "there", "see", "his", "here", "web", "get", "been", "were", "these", "its", "than", "find", "top", "had", "list", "just", "over", "year", "day", "into", "two", "used", "last", "most", "buy", "post", "add", "such", "best", "where", "info", "high", "very", "read", "sex", "need", "user", "set", "map", "know", "way", "part", "real", "must", "line", "did", "send", "using", "forum", "even", "being", "much", "link", "open", "south", "both", "power", "care", "down", "him", "without", "think", "big", "law", "shop", "old", "main", "man", "card", "job", "teen", "too", "join", "west", "team", "box", "gay", "start", "air", "yes", "hot", "cost", "march", "say", "going", "test", "cart", "staff", "things", "tax", "got", "let", "park", "act", "key", "few", "age", "hard", "pay", "four", "offer", "easy", "fax", "china", "yet", "areas", "sun", "enter", "share", "run", "net", "term", "put", "try", "god", "head", "least", "log", "cars", "fun", "arts", "lot", "ask", "beach", "past", "due", "ever", "ago", "cheap", "mark", "bad", "edit", "fast", "often", "though", "town", "step", "shows", "enough", "death", "brand", "oil", "bit", "near", "stuff", "doing", "stay", "mean", "force", "cash", "bay", "seen", "stop", "dog", "mind", "lost", "tour", "menu", "wish", "lower", "fine", "hour", "gas", "six", "bush", "sat", "zip", "bid", "kind", "sent", "shown", "lead", "went", "idea", "deal", "forms", "feed", "cut", "earth", "ship", "kit", "boy", "wine", "stars", "owner", "son", "bring", "grand", "van", "skin", "pop", "rest", "hit", "fish", "eye", "string", "youth", "fee", "rent", "dark", "aid", "host", "hands", "fat", "saw", "dead", "farm", "showing", "hear", "fan", "former", "cat", "die", "flow", "path", "pet", "guy", "cup", "army", "gear", "forest", "ending", "wind", "bob", "fit", "pain", "cum", "edge", "ice", "pink", "shot", "bus", "heat", "nor", "bug", "soft", "theme", "rich", "touch", "chain", "died", "reach", "lab", "snow", "owned", "chart", "gene", "ends", "cast", "soul", "ended", "dining", "mix", "fix", "ray", "bear", "gain", "dry", "blow", "shared", "cent", "forced", "zero", "bath", "sharing", "won", "wear", "mom", "rare", "bars", "seat", "aim", "rings", "tip", "mine", "whom", "math", "fly", "fear", "standing", "wars", "hey", "beat", "arms", "sky", "toy", "slow", "hip", "nine", "grow", "dot", "rain", "yeah", "cap", "peak", "raw", "sharp", "wet", "ram", "fox", "mesh", "dean", "pub", "hop", "mouth", "gun", "lens", "warm", "rear", "showed", "mens", "bowl", "kid", "pan", "dish", "eating", "vary", "arab", "bands", "push", "tower", "sum", "shower", "dear", "vat", "beer", "sir", "earn", "twin", "spy", "chip", "sit", "echo", "fig", "stands", "teach", "tab", "beds", "aged", "seed", "peer", "meat", "inner", "leg", "tiny", "gap", "rob", "mining", "jet", "mad", "shoe", "joy", "ran", "seal", "ill", "lay", "wings", "bet", "throw", "dad", "pat", "yard", "pour", "dust", "kings", "tie", "ward", "roof", "beast", "rush", "wins", "ghost", "toe", "shit", "ease", "arena", "lands", "armed", "pine", "tend", "candy", "finger", "tough", "lie", "chest", "weak", "leaf", "pad", "rod", "sad", "meal", "pot", "mars", "theft", "swing", "mint", "spin", "wash", "jam", "hero", "ion", "peru", "singer", "aging", "reed", "ban", "vast", "odd", "beam", "shut", "inform", "cry", "zoo", "arrow", "rough", "outer", "steam", "ace", "sue", "eggs", "mins", "stem", "opt", "rap", "charm", "soup", "cod", "singing", "gel", "doug", "mart", "coin", "harm", "deer", "pal", "oven", "cheat", "gym", "tan", "pie", "tied", "bingo", "cedar", "stud", "bend", "dam", "chad", "dying", "bench", "tub", "inns", "easter", "landing", "bean", "wheat", "bee", "loud", "bare", "pit", "ton", "lying", "handed", "sink", "pins", "handy", "rid", "rip", "lip", "sap", "forming", "eyed", "ought", "aye", "forty", "rows", "ears", "fist", "mere", "dig", "caring", "deny", "rim", "tier", "andrea", "pig", "lit", "duo", "fog", "fur", "rug", "ham", "sheer", "bind", "lows", "pest", "sofa", "tent", "dare", "wax", "nut", "lean", "bye", "strand", "dash", "lap", "steal", "ant", "gem", "heath", "yeast", "myth", "gig", "weed", "hint", "barn", "fare", "herb", "ate", "mud", "shark", "shine", "dip", "hash", "lined", "pens", "lid", "deaf", "keen", "peas", "owns", "hay", "zinc", "tear", "nest", "cop", "dim", "stan", "sip", "feat", "glow", "ware", "foul", "seas", "forge", "pod", "ours", "wit", "yarn", "mug", "marsh", "bent", "hat")]
    Word,
    
    [SouvenirQuestion("In what order was the Braille read in {0}?", OneColumn4Answers, "Standard Braille Order", "Individual Reading Order", "Merged Reading Order", "Chinese Reading Order", TranslateAnswers = true)]
    Order
}

public partial class SouvenirModule
{
    [SouvenirHandler("touchTransmission", "Touch Transmission", typeof(STouchTransmission), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessTouchTransmission(ModuleData module)
    {
        var comp = GetComponent(module, "TouchTransmissionScript");
        yield return WaitForSolve;

        var fldGenWord = GetField<string>(comp, "generatedWord");
        var fldOrder = GetField<object>(comp, "chosenOrder");

        addQuestions(module,
            makeQuestion(Question.TouchTransmissionWord, module, correctAnswers: new[] { fldGenWord.Get().ToLowerInvariant() }),
            makeQuestion(Question.TouchTransmissionOrder, module, correctAnswers: new[] { fldOrder.Get().ToString().Replace('_', ' ') }));
    }
}
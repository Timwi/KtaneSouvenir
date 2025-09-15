using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SVariety
{
    [SouvenirQuestion("What color was the LED flashing in {0}?", TwoColumns4Answers, "Red", "Yellow", "Blue", "White", "Black", TranslateAnswers = true, TranslatableStrings = ["the Variety that has one", "the Variety that has one (LED)", "the Variety that has one (digit display)", "the Variety that has one (letter display)", "the Variety that has one (timer)", "the Variety that has one (ascendingtimer)", "the Variety that has one (descendingtimer)", "the Variety that has one (knob)", "the Variety that has one (coloredknob)", "the Variety that has one (redknob)", "the Variety that has one (yellowknob)", "the Variety that has one (blueknob)", "the Variety that has one (blackknob)", "the Variety that has one (bulb)", "the Variety that has one (redbulb)", "the Variety that has one (yellowbulb)", "the Variety that has {0}", "a knob", "a colored knob", "a white knob", "a red knob", "a black knob", "a blue knob", "a yellow knob", "a keypad", "a white keypad", "a red keypad", "a yellow keypad", "a blue keypad", "a slider", "a horizontal slider", "a vertical slider", "an LED", "a digit display", "a wire", "a black wire", "a blue wire", "a red wire", "a yellow wire", "a white wire", "a button", "a red button", "a yellow button", "a blue button", "a white button", "a letter display", "a Braille display", "a key-in-lock", "a switch", "a red switch", "a yellow switch", "a blue switch", "a white switch", "a timer", "an ascending timer", "a descending timer", "a die", "a light-on-dark die", "a dark-on-light die", "a bulb", "a red bulb", "a yellow bulb", "a maze", "a 3×3 maze", "a 3×4 maze", "a 4×3 maze", "a 4×4 maze"])]
    LED,
    
    [SouvenirQuestion("What digit was displayed but not the answer for the digit display in {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "0")]
    DigitDisplay,
    
    [SouvenirQuestion("What word could be formed but was not the answer for the letter display in {0}?", ThreeColumns6Answers, "ACE", "ACT", "AID", "AIM", "AIR", "ALE", "ALL", "AND", "ANT", "APT", "ARM", "ART", "AWE", "AYE", "BAD", "BAG", "BAR", "BAT", "BAY", "BED", "BEE", "BEG", "BET", "BID", "BIG", "BIT", "BIZ", "BOB", "BOW", "BOY", "BUT", "BUY", "BYE", "CAN", "CAP", "CAR", "CAT", "COP", "COT", "COW", "CUE", "CUP", "CUT", "DAD", "DAM", "DAY", "DIE", "DIG", "DIM", "DIP", "DOG", "DOT", "DRY", "DUE", "DUG", "DUO", "DYE", "EAR", "EAT", "FAN", "FAQ", "FAR", "FAT", "FAX", "FED", "FEE", "FEN", "FEW", "FIN", "FIT", "FIX", "FLY", "FOG", "FOR", "FRK", "FRQ", "FRY", "FUN", "FUR", "GET", "GIG", "GIN", "GUM", "GUT", "GUY", "HAM", "HAT", "HAY", "HEN", "HER", "HEY", "HIM", "HIP", "HIT", "HOP", "HOT", "HOW", "HUT", "ILK", "ILL", "IND", "INK", "IRK", "JAM", "JAR", "JAW", "JOB", "JOY", "KID", "KIN", "KIT", "LAD", "LAP", "LAW", "LAY", "LEG", "LET", "LID", "LIE", "LIP", "LIT", "LOG", "LOO", "LOT", "LOW", "LUA", "LUG", "MAD", "MAN", "MAP", "MAT", "MAX", "MAY", "MIC", "MID", "MIX", "MOB", "MOD", "MUD", "MUG", "MUM", "NET", "NEW", "NIL", "NLL", "NOD", "NOR", "NOT", "NOW", "NUN", "NUT", "OIL", "OPT", "OUR", "OUT", "OWE", "OWL", "PAD", "PAN", "PAR", "PAT", "PAY", "PEG", "PEN", "PER", "PET", "PIE", "PIG", "PIN", "PIT", "POP", "POT", "POW", "PUB", "PUT", "QUA", "QUE", "QUO", "RAG", "RAM", "RAT", "RAW", "RED", "RGB", "RIB", "RID", "RIG", "RIM", "ROB", "ROD", "ROT", "ROW", "RUB", "RUG", "RUM", "RUN", "SAD", "SAW", "SAY", "SEA", "SEE", "SET", "SHE", "SHY", "SIC", "SIG", "SIN", "SIR", "SIT", "SIX", "SLY", "SND", "SUE", "SUM", "SUN", "TAG", "TAP", "TAX", "TEA", "TEE", "TEN", "TGB", "THY", "TIE", "TIN", "TIP", "TOE", "TOO", "TOP", "TOY", "TRN", "TRY", "TUB", "VAT", "VET", "WAR", "WAX", "WAY", "WEE", "WET", "WHY", "WIG", "WIN", "WIT", "WIZ", "WRY", "YEN", "YET", "ZAG", "ZIG")]
    LetterDisplay,
    
    [SouvenirQuestion("What was the maximum display for the {1}timer in {0}?", ThreeColumns6Answers, "1 1", "2 1", "4 1", "6 1", "1 2", "2 2", "4 2", "1 4", "2 4", Arguments = ["", "ascending ", "descending "], TranslateArguments = [true], ArgumentGroupSize = 1)]
    Timer,
    
    [SouvenirQuestion("What was n for the {1}knob in {0}?", TwoColumns4Answers, Arguments = ["", "colored ", "red ", "black ", "blue ", "yellow "], TranslateArguments = [true], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(3, 6)]
    ColoredKnob,
    
    [SouvenirQuestion("What was n for the {1}bulb in {0}?", ThreeColumns6Answers, Arguments = ["", "red ", "yellow "], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(5, 13)]
    Bulb
}

public partial class SouvenirModule
{
    [SouvenirHandler("VarietyModule", "Variety", typeof(SVariety), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessVariety(ModuleData module)
    {
        var comp = GetComponent(module, "VarietyModule");

        var items = GetField<IEnumerable>(comp, "_items").Get().Cast<object>().ToArray();
        if (items.Length != 10)
            throw new AbandonModuleException($"Expected 10 items, found {items.Length}.");
        var itemTypes = items.Select(i => i.GetType().Name).ToArray();

        string[] itemString(object piece)
        {
            var typeName = piece.GetType().Name;
            switch (typeName)
            {
                case "BrailleDisplay":
                    return new[] { "a Braille display" };
                case "Bulb":
                    var color = GetProperty<object>(piece, "Color", isPublic: true).Get().ToString();
                    return new[] { "a bulb", $"a {color.Substring(0, color.Length - 4).ToLowerInvariant()} bulb" };
                case "Button":
                    color = GetProperty<object>(piece, "Color", isPublic: true).Get().ToString();
                    return new[] { "a button", $"a {color.Substring(0, color.Length - 6).ToLowerInvariant()} button" };
                case "ColoredKeypad":
                    color = GetProperty<object>(piece, "Color", isPublic: true).Get().ToString();
                    return new[] { "a keypad", $"a {color.Substring(0, color.Length - 6).ToLowerInvariant()} keypad" };
                case "ColoredKnob":
                    color = GetProperty<object>(piece, "Color", isPublic: true).Get().ToString();
                    return new[] { "a knob", "a colored knob", $"a {color.Substring(0, color.Length - 4).ToLowerInvariant()} knob" };
                case "Die":
                    var flavor = GetField<bool>(piece, "_flavor").Get();
                    return new[] { "a die", flavor ? "a dark-on-light die" : "a light-on-dark die" };
                case "DigitDisplay":
                    return new[] { "a digit display" };
                case "Key":
                    return new[] { "a key-in-lock" };
                case "Keypad":
                    return new[] { "a keypad", "a white keypad" };
                case "Knob":
                    return new[] { "a knob", "a white knob" };
                case "Led":
                    return new[] { "an LED" };
                case "LetterDisplay":
                    return new[] { "a letter display" };
                case "Maze":
                    var width = GetProperty<int>(piece, "Width", isPublic: true).Get();
                    var height = GetProperty<int>(piece, "Height", isPublic: true).Get();
                    return new[] { "a maze", $"a {width}×{height} maze" };
                case "Slider":
                    var orientation = GetProperty<object>(piece, "Orientation", isPublic: true).Get().ToString();
                    return new[] { "a slider", $"a {orientation.Substring(0, orientation.Length - 6).ToLowerInvariant()} slider" };
                case "Switch":
                    color = GetProperty<object>(piece, "Color", isPublic: true).Get().ToString();
                    return new[] { "a switch", $"a {color.Substring(0, color.Length - 6).ToLowerInvariant()} switch" };
                case "Timer":
                    flavor = ((int) GetProperty<object>(piece, "FlavorType", isPublic: true).Get()) != 0;
                    return new[] { "a timer", flavor ? "a descending timer" : "an ascending timer" };
                case "Wire":
                    color = GetProperty<object>(piece, "Color", isPublic: true).Get().ToString();
                    return new[] { "a wire", $"a {color.Substring(0, color.Length - 4).ToLowerInvariant()} wire" };
                default:
                    throw new AbandonModuleException($"Unexpected component type {typeName}");
            }
        }
        var itemStrings = new HashSet<string>(items.SelectMany(itemString));

        _varietyParts.Add(itemStrings);

        yield return WaitForSolve;

        var allDisambiguators = Ut.NewArray("a knob", "a colored knob", "a white knob", "a red knob", "a black knob", "a blue knob", "a yellow knob", "a keypad", "a white keypad", "a red keypad", "a yellow keypad", "a blue keypad", "a slider", "a horizontal slider", "a vertical slider", "an LED", "a digit display", "a wire", "a black wire", "a blue wire", "a red wire", "a yellow wire", "a white wire", "a button", "a red button", "a yellow button", "a blue button", "a white button", "a letter display", "a Braille display", "a key-in-lock", "a switch", "a red switch", "a yellow switch", "a blue switch", "a white switch", "a timer", "an ascending timer", "a descending timer", "a die", "a light-on-dark die", "a dark-on-light die", "a bulb", "a red bulb", "a yellow bulb", "a maze", "a 3×3 maze", "a 3×4 maze", "a 4×3 maze", "a 4×4 maze");
        bool canDisambiguateWith(string phrase) => _varietyParts.Count(v => v.Contains(phrase)) == 1;

        string disambiguate(string component, string flavor)
        {
            if (UnityEngine.Random.Range(0, 4) == 0)
                return null;

            switch (component)
            {
                case "Led":
                    if (canDisambiguateWith("an LED"))
                    {
                        var str = translateString(Question.VarietyLED, "the Variety that has one");
                        return str != "" ? str : translateString(Question.VarietyLED, "the Variety that has one (LED)");
                    }
                    break;
                case "DigitDisplay":
                    if (canDisambiguateWith("a digit display"))
                    {
                        var str = translateString(Question.VarietyLED, "the Variety that has one");
                        return str != "" ? str : translateString(Question.VarietyLED, "the Variety that has one (digit display)");
                    }
                    break;
                case "LetterDisplay":
                    if (canDisambiguateWith("a letter display"))
                    {
                        var str = translateString(Question.VarietyLED, "the Variety that has one");
                        return str != "" ? str : translateString(Question.VarietyLED, "the Variety that has one (letter display)");
                    }
                    break;
                case "Timer":
                    if (flavor == null && canDisambiguateWith("a timer") ||
                        flavor == "ascending" && canDisambiguateWith("an ascending timer") ||
                        flavor == "descending" && canDisambiguateWith("a descending timer"))
                    {
                        var str = translateString(Question.VarietyLED, "the Variety that has one");
                        return str != "" ? str : translateString(Question.VarietyLED, $"the Variety that has one ({flavor ?? ""}timer)");
                    }
                    break;
                case "ColoredKnob":
                    if (flavor == null && canDisambiguateWith("a knob") ||
                        flavor != null && canDisambiguateWith($"a {flavor} knob"))
                    {
                        var str = translateString(Question.VarietyLED, "the Variety that has one");
                        return str != "" ? str : translateString(Question.VarietyLED, $"the Variety that has one ({flavor ?? ""}knob)");
                    }
                    break;
                case "Bulb":
                    if (flavor == null && canDisambiguateWith("a bulb") ||
                        flavor != null && canDisambiguateWith($"a {flavor} bulb"))
                    {
                        var str = translateString(Question.VarietyLED, "the Variety that has one");
                        return str != "" ? str : translateString(Question.VarietyLED, $"the Variety that has one ({flavor ?? ""}bulb)");
                    }
                    break;
                default:
                    throw new AbandonModuleException($"Unexpected component type {component}");
            }

            var formats = allDisambiguators.Where(canDisambiguateWith).ToArray();

            var chosen = formats.Any() ? formats.PickRandom() : null;
            if (chosen is null)
                return null;
            return string.Format(translateString(Question.VarietyLED, "the Variety that has {0}"), translateString(Question.VarietyLED, chosen));
        }

        List<QandA> questions = new();
        var disableSelectables = false;

        if (Array.IndexOf(itemTypes, "Led") is var i and not -1)
        {
            var led = items[i];
            var c1 = GetProperty<object>(led, "Color1", isPublic: true).Get(null, v => (int) v is < 0 or > 4 ? $"Unknown LED color {v}" : null).ToString();
            var c2 = GetProperty<object>(led, "Color2", isPublic: true).Get(null, v => (int) v is < 0 or > 4 ? $"Unknown LED color {v}" : null).ToString();
            questions.Add(makeQuestion(Question.VarietyLED, module, formattedModuleName: disambiguate("Led", null), correctAnswers: new[] { c1, c2 }));
            disableSelectables = true;
        }

        if (Array.IndexOf(itemTypes, "DigitDisplay") is var j and not -1)
        {
            var display = items[j];
            var amount = GetField<int>(display, "_numStates").Get(v => v is < 1 or > 9 ? $"Bad number of digit display states {v}" : null);
            if (amount == 1)
                Debug.Log($"<Souvenir #{_moduleId}> Variety: Not asking about the digit display because there was only one valid digit.");
            else
            {
                var displays = GetArrayField<int>(display, "_displayedDigitPerState").Get(expectedLength: 9);
                var solution = GetProperty<int>(display, "State", isPublic: true).Get(v => v is < 0 || v >= amount ? $"Bad digit display solution state {v}" : null);
                List<string> ans = new();
                for (var ix = 0; ix < amount; ix++)
                    if (ix != solution)
                        ans.Add(displays[ix].ToString());
                questions.Add(makeQuestion(Question.VarietyDigitDisplay, module, formattedModuleName: disambiguate("DigitDisplay", null), correctAnswers: ans.ToArray(), preferredWrongAnswers: new[] { displays[solution].ToString() }));
                disableSelectables = true;
            }
        }

        if (Array.IndexOf(itemTypes, "LetterDisplay") is var k and not -1)
        {
            var display = items[k];
            // Actually a property, but this method handles that
            var words = GetArrayField<string>(display, "FormableWords", isPublic: true).Get();
            if (words.Length == 1)
                Debug.Log($"<Souvenir #{_moduleId}> Variety: Not asking about the letter display because there was only one valid word.");
            else
            {
                var solution = GetProperty<int>(display, "State", isPublic: true).Get(v => v is < 0 || v >= words.Length ? $"Bad letter display solution state {v}" : null);
                questions.Add(makeQuestion(Question.VarietyLetterDisplay, module, formattedModuleName: disambiguate("LetterDisplay", null), correctAnswers: words.Where((_, i) => i != solution).ToArray(), preferredWrongAnswers: new[] { words[solution] }));
                disableSelectables = true;
            }
        }

        if (Array.IndexOf(itemTypes, "Timer") is var l and not -1)
        {
            var timers = Array.IndexOf(itemTypes, "Timer", l + 1) is var x and not -1 ? (new[] { items[l], items[x] }) : (new[] { items[l] });
            var data = timers.Select(timer =>
            {
                var a = GetField<int>(timer, "_a").Get(v => v is not 2 and not 3 and not 5 and not 7 ? $"Unknown timer A value {v}" : null);
                var b = GetField<int>(timer, "_b").Get(v => v is not 2 and not 3 and not 5 || a is 3 && v is 5 || a is 5 && v is > 2 || a is 7 && v is not 2 ? $"Unknown timer B value {v}" : null);
                var flavor = GetProperty<object>(timer, "FlavorType", isPublic: true).Get(null, validator: v => v is < 0 or > 1 ? $"Unknown timer flavor {v}" : null);
                return (A: a, B: b, Flavor: flavor);
            }).ToArray();
            if (data.Length == 1)
                questions.Add(makeQuestion(Question.VarietyTimer, module, formattedModuleName: disambiguate("Timer", null), formatArgs: new[] { "" }, correctAnswers: new[] { $"{data[0].A - 1} {data[0].B - 1}" }));
            else
                questions.AddRange(new[] {
                    makeQuestion(Question.VarietyTimer, module, formattedModuleName: disambiguate("Timer", data[0].Flavor.ToString().ToLowerInvariant()), formatArgs: new[] { data[0].Flavor.ToString().ToLowerInvariant() + " " }, correctAnswers: new[] { $"{data[0].A - 1} {data[0].B - 1}" }),
                    makeQuestion(Question.VarietyTimer, module, formattedModuleName: disambiguate("Timer", data[1].Flavor.ToString().ToLowerInvariant()), formatArgs: new[] { data[1].Flavor.ToString().ToLowerInvariant() + " " }, correctAnswers: new[] { $"{data[1].A - 1} {data[1].B - 1}" })
                });
            disableSelectables = true;
        }

        var cknobs = itemTypes.Select((t, i) => (t, i)).Where(tup => tup.t == "ColoredKnob").Select(tup => tup.i).ToArray();
        var wknobs = itemTypes.Contains("Knob") ? 1 : 0;
        if (cknobs.Length != 0)
        {
            var format = cknobs.Length == 1 && wknobs != 0 ? "colored " : cknobs.Length == 1 ? "" : null;
            foreach (var knob in cknobs.Select(i => items[i]))
            {
                var ans = GetProperty<int>(knob, "NumStates", isPublic: true).Get(null, v => v is < 3 or > 6 ? $"Bad colored knob state count {v}" : null);
                var flavor = GetProperty<object>(knob, "Color", isPublic: true).Get(null, v => (int) v is < 0 or > 3 ? $"Unknown knob color {v}" : null).ToString();
                questions.Add(makeQuestion(Question.VarietyColoredKnob, module, formattedModuleName: disambiguate("ColoredKnob", cknobs.Length + wknobs == 1 ? null : cknobs.Length == 1 ? "colored" : flavor.ToLowerInvariant().Substring(0, flavor.Length - 4)), formatArgs: new[] { format ?? flavor.ToLowerInvariant().Substring(0, flavor.Length - 4) + " " }, correctAnswers: new[] { ans.ToString() }));
            }
            disableSelectables = true;
        }

        var bulbs = itemTypes.Select((t, i) => (t, i)).Where(tup => tup.t == "Bulb").Select(tup => tup.i).ToArray();
        if (bulbs.Length != 0)
        {
            var format = bulbs.Length == 1 ? "" : null;

            foreach (var bulb in bulbs.Select(ix => items[ix]))
            {
                var ans = GetProperty<int>(bulb, "N", isPublic: true).Get(null, v => v is < 5 or > 13 ? $"Unknown bulb N {v}" : null);
                var flavor = GetProperty<object>(bulb, "Color", isPublic: true).Get(null, v => (int) v is < 0 or > 1 ? $"Unknown bulb color {v}" : null).ToString();
                questions.Add(makeQuestion(Question.VarietyBulb, module, formattedModuleName: disambiguate("Bulb", bulbs.Length == 1 ? null : flavor.Substring(0, flavor.Length - 4).ToLowerInvariant()), formatArgs: new[] { format ?? flavor.Substring(0, flavor.Length - 4).ToLowerInvariant() + " " }, correctAnswers: new[] { ans.ToString() }));
            }
            disableSelectables = true;
        }

        if (questions.Count == 0)
            yield return legitimatelyNoQuestion(module, "There were no relevant components (or they were not possible to ask about).");

        if (disableSelectables)
        {
            var sel = GetField<KMSelectable>(comp, "ModuleSelectable", isPublic: true).Get();
            foreach (var c in sel.Children)
            {
                if (c is not null)
                {
                    c.OnInteract = () => false;
                    c.OnInteractEnded = () => { };
                }
            }
        }

        addQuestions(module, questions);
    }
}
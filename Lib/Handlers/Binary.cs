using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBinary
{
    [SouvenirQuestion("What word was displayed in {0}?", ThreeColumns6Answers, "ah", "at", "am", "as", "an", "be", "by", "go", "if", "in", "is", "it", "mu", "nu", "no", "nu", "of", "pi", "to", "up", "us", "we", "xi", "ace", "aim", "air", "bed", "bob", "but", "buy", "can", "cat", "chi", "cut", "day", "die", "dog", "dot", "eat", "eye", "for", "fly", "get", "gut", "had", "hat", "hot", "ice", "lie", "lit", "mad", "map", "may", "new", "not", "now", "one", "pay", "phi", "pie", "psi", "red", "rho", "sad", "say", "sea", "see", "set", "six", "sky", "tau", "the", "too", "two", "why", "win", "yes", "zoo", "alfa", "beta", "blue", "chat", "cyan", "demo", "door", "east", "easy", "each", "edit", "fail", "fall", "fire", "five", "four", "game", "golf", "grid", "hard", "hate", "help", "hold", "iota", "kilo", "lima", "lime", "list", "lock", "lost", "stop", "test", "time", "tree", "type", "west", "wire", "wood", "xray", "yell", "zero", "zeta", "zulu", "abort", "about", "alpha", "black", "bravo", "clock", "close", "could", "crash", "delta", "digit", "eight", "gamma", "glass", "green", "guess", "hotel", "india", "kappa", "later", "least", "lemon", "month", "morse", "north", "omega", "oscar", "panic", "press", "romeo", "seven", "sigma", "smash", "south", "tango", "timer", "voice", "while", "white", "world", "worry", "would", "binary", "defuse", "disarm", "expert", "finish", "forget", "lambda", "manual", "module", "number", "orange", "period", "purple", "quebec", "should", "sierra", "source", "strike", "submit", "twitch", "victor", "violet", "window", "yellow", "yankee", "charlie", "epsilon", "explode", "foxtrot", "juliett", "measure", "mission", "omicron", "subject", "uniform", "upsilon", "whiskey", "detonate", "notsolve", "november")]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("Binary", "Binary", typeof(SBinary), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessBinary(ModuleData module)
    {
        var comp = GetComponent(module, "Binary");
        yield return WaitForSolve;

        addQuestions(module, makeQuestion(Question.BinaryWord, module, formatArgs: null, correctAnswers: new[] { Question.BinaryWord.GetAnswers()[GetField<int>(comp, "te").Get()] }));
    }
}
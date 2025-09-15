using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotTheBulb
{
    [SouvenirQuestion("What word flashed on {0}?", OneColumn4Answers, ExampleAnswers = ["Amplitude", "Boulevard", "Chemistry", "Duplicate", "Eightfold", "Filaments", "Goldsmith", "Harlequin", "Injectors", "Juxtapose", "Kilohertz", "Labyrinth", "Moustache", "Neighbour", "Obscurity", "Penumbral", "Quicksand", "Rhapsodic", "Squawking", "Triglyphs", "Universal", "Vexations", "Whizbangs", "Xenoglyph", "Yardstick", "Zigamorph"])]
    Word,

    [SouvenirQuestion("What color was the bulb on {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Yellow", "Purple", "White", TranslateAnswers = true)]
    Color,

    [SouvenirQuestion("What was the material of the screw cap on {0}?", ThreeColumns6Answers, "Copper", "Silver", "Gold", "Plastic", "Carbon Fibre", "Ceramic", TranslateAnswers = true)]
    ScrewCap
}

public partial class SouvenirModule
{
    [SouvenirHandler("notTheBulb", "Not The Bulb", typeof(SNotTheBulb), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessNotTheBulb(ModuleData module)
    {
        var comp = GetComponent(module, "NtBScript");
        yield return WaitForSolve;
        var qs = new List<QandA>();

        // Transmitted word
        var words = GetArrayField<string>(comp, "words").Get();
        var wordList = GetArrayField<string>(comp, "wordlist").Get();
        var targetWord = words[0].Substring(0, 1) + words[0].Substring(1).ToLowerInvariant();
        var wordListLower = Enumerable.Range(0, wordList.Length).Select(word => wordList[word].Substring(0, 1) + wordList[word].Substring(1).ToLowerInvariant()).ToArray();
        qs.Add(makeQuestion(Question.NotTheBulbWord, module, correctAnswers: new[] { targetWord }, preferredWrongAnswers: wordListLower));

        // Bulb color
        var properties = GetArrayField<int>(comp, "properties").Get();
        var colorNames = new[] { "Red", "Green", "Blue", "Yellow", "Purple", "White" };
        var bulbColor = colorNames[properties[0]];
        qs.Add(makeQuestion(Question.NotTheBulbColor, module, correctAnswers: new[] { bulbColor }, preferredWrongAnswers: colorNames));

        // Screw cap material
        var screwCapNames = new[] { "Copper", "Silver", "Gold", "Plastic", "Carbon Fibre", "Ceramic" };
        var screwCap = screwCapNames[properties[1]];
        qs.Add(makeQuestion(Question.NotTheBulbScrewCap, module, correctAnswers: new[] { screwCap }, preferredWrongAnswers: screwCapNames));

        addQuestions(module, qs);
    }
}
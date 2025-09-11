using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMssngvWls
{
    [SouvenirQuestion("Which vowel was missing in {0}?", ThreeColumns6Answers, "A", "E", "I", "O", "U", TranslatableStrings = ["AEIOU"])]
    MssNgvwL
}

public partial class SouvenirModule
{
    [SouvenirHandler("MssngvWls", "Mssngv Wls", typeof(SMssngvWls), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessMssngvWls(ModuleData module)
    {
        yield return WaitForSolve;

        const char StartModuleName = '\uE001'; // In the private use area
        const char EndModuleName = '\uE002';
        const int MinWordLength = 2;
        const int MaxWordLength = 6;
        const float SpaceChance = 0.33f;

        var comp = GetComponent(module, "MssngvWls");
        var missingVowel = GetIntField(comp, "ForbiddenNumber").Get(0, 4);

        var vowels = translateString(Question.MssngvWlsMssNgvwL, "AEIOU");

        GetField<TextMesh>(comp, "Text", true).Get().text = "";
        GetField<KMSelectable>(comp, "CycleButton", true).Get().OnInteract = () => false;

        if (vowels is "")
        {
            addQuestion(module, Question.MssngvWlsMssNgvwL, correctAnswers: new[] { "AEIOU"[missingVowel].ToString() });
            yield break;
        }

        var moduleName = formatModuleName(Question.MssngvWlsMssNgvwL, _moduleCounts.Get(module.Module.ModuleType, 0) > 1, module.SolveIndex);
        var questionText = string.Format(translateQuestion(Question.MssngvWlsMssNgvwL), moduleName);

        using var letters = questionText.Normalize().GetEnumerator();

        StringBuilder newText = new();
        var curWordLen = 0;
        while (letters.MoveNext())
        {
            if (char.IsWhiteSpace(letters.Current) || vowels.Contains(char.ToUpperInvariant(letters.Current)))
                continue;
            if (char.IsLetter(letters.Current) && ((curWordLen >= MinWordLength && UnityEngine.Random.value < SpaceChance) || curWordLen >= MaxWordLength))
            {
                newText.Append(' ');
                curWordLen = 0;
            }
            if (letters.Current is StartModuleName)
            {
                if (curWordLen is not 0)
                    newText.Append(' ');
                curWordLen = 0;
                while (letters.MoveNext() && letters.Current is not EndModuleName)
                {
                    curWordLen++;
                    newText.Append(letters.Current);
                    if (char.IsWhiteSpace(letters.Current))
                        curWordLen = 0;
                }
            }
            else
            {
                newText.Append(letters.Current);
                curWordLen++;
            }
        }

        var attr = Ut.GetAttribute(Question.MssngvWlsMssNgvwL);
        var answers = "AEIOU".Select(c => c.ToString()).ToArray().Shuffle();

        addQuestions(module, new QandA(
            q: Question.MssngvWlsMssNgvwL,
            module: attr.ModuleNameWithThe,
            question: new QandA.TextQuestion(newText.ToString(), attr.Layout, null, 0f),
            answerSet: new QandA.TextAnswerSet(5, attr.Layout, answers, Fonts[_translation?.DefaultFontIndex ?? 0], attr.FontSize, attr.CharacterSize, FontTextures[_translation?.DefaultFontIndex ?? 0], FontMaterial),
            correctIndex: Array.IndexOf(answers, "AEIOU"[missingVowel].ToString())));
    }
}
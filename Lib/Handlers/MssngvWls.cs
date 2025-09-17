using System.Collections.Generic;
using System.Linq;
using System.Text;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SMssngvWls
{
    [SouvenirQuestion("{1}", ThreeColumns6Answers, "A", "E", "I", "O", "U", Arguments = ["Whc hvw lwsm ssn gn {0}?"], ArgumentGroupSize = 1, TranslatableStrings = ["Which vowel was missing in {0}?", "AEIOU"])]
    MssNgvwL
}

public partial class SouvenirModule
{
    [SouvenirHandler("MssngvWls", "\uE001Mssngv Wls\uE002", typeof(SMssngvWls), "Anonymous")]
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

        var vowels = TranslateQuestionString(SMssngvWls.MssNgvwL, "AEIOU");
        var questionText = string.Format(
            TranslateQuestionString(SMssngvWls.MssNgvwL, "Which vowel was missing in {0}?"),
            formatModuleName(typeof(SMssngvWls).GetHandlerAttribute(), module.Info.NumModules > 1, module.SolveIndex));

        if (vowels is not "")
        {
            using var letters = questionText.Normalize().GetEnumerator();
            var newText = new StringBuilder();
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
            questionText = newText.ToString();
        }

        yield return question(SMssngvWls.MssNgvwL, args: [questionText]).Answers("AEIOU"[missingVowel].ToString());

        GetField<TextMesh>(comp, "Text", true).Get().text = "";
        GetField<KMSelectable>(comp, "CycleButton", true).Get().OnInteract = () => false;
    }
}

using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SZoni
{
    [SouvenirQuestion("What was the {1} word in {0}?", OneColumn4Answers, ExampleAnswers = ["angel", "thing", "dance", "heavy", "quote", "radio"], Type = AnswerType.DynamicFont, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Words
}

public partial class SouvenirModule
{
    [SouvenirHandler("lgndZoni", "Zoni", typeof(SZoni), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessZoni(ModuleData module)
    {
        var comp = GetComponent(module, "ZoniModuleScript");
        var fldIndex = GetIntField(comp, "wordIndex");
        var fldStage = GetIntField(comp, "solvedStages");

        var buttons = GetArrayField<KMSelectable>(comp, "buttons", isPublic: true).Get();
        var words = GetArrayField<string>(comp, "wordlist", isPublic: true).Get();
        var index = fldIndex.Get(0, words.Length - 1);
        var stage = fldStage.Get(v => v != 0 ? "‘solvedStages’ did not start at 0" : null);

        var wordsAnswered = new List<int>();
        for (var i = 0; i < buttons.Length; i++)
        {
            var prevInteract = buttons[i].OnInteract;
            buttons[i].OnInteract = delegate
            {
                var ret = prevInteract();
                var st = fldStage.Get();
                if (stage != st)  // If they are equal, the user got a strike
                {
                    wordsAnswered.Add(index);
                    stage = st;
                }
                index = fldIndex.Get();
                return ret;
            };
        }

        yield return WaitForSolve;

        if (wordsAnswered.Count != 3)
            throw new AbandonModuleException($"The received number of valid words was not 3: was {wordsAnswered.Count}.");

        var textbox = GetField<TextMesh>(comp, "textBox", isPublic: true).Get();
        var font = textbox.font;
        var fontTexture = textbox.GetComponent<MeshRenderer>().sharedMaterial.mainTexture;

        addQuestions(module,
            makeQuestion(Question.ZoniWords, module, formatArgs: new[] { "first" }, font: font, fontTexture: fontTexture, correctAnswers: new[] { words[wordsAnswered[0]] }, preferredWrongAnswers: words),
            makeQuestion(Question.ZoniWords, module, formatArgs: new[] { "second" }, font: font, fontTexture: fontTexture, correctAnswers: new[] { words[wordsAnswered[1]] }, preferredWrongAnswers: words),
            makeQuestion(Question.ZoniWords, module, formatArgs: new[] { "third" }, font: font, fontTexture: fontTexture, correctAnswers: new[] { words[wordsAnswered[2]] }, preferredWrongAnswers: words));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SPhosphorescence
{
    [Question("Which color was present on a button in {0}?", ThreeColumns6Answers, ["Azure", "Blue", "Crimson", "Diamond", "Emerald", "Fuchsia", "Green", "Hazel", "Ice", "Jade", "Kiwi", "Lime", "Magenta", "Navy", "Orange", "Purple", "Quartz", "Red", "Salmon", "Tan", "Ube", "Vibe", "White", "Xotic", "Yellow", "Zen"], TranslateAnswers = true)]
    ButtonColors,

    [Question("What was the offset in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 419)]
    Offset,

    [Question("What was the decrypted word in {0}?", ThreeColumns6Answers, ExampleAnswers = ["abacus", "abbey", "acid", "act", "action", "admire"])]
    Word
}

public partial class SouvenirModule
{
    [Handler("Phosphorescence", "Phosphorescence", typeof(SPhosphorescence), "Espik")]
    [ManualQuestion("What colors were the buttons?")]
    [ManualQuestion("What was the offset?")]
    [ManualQuestion("What was the decrypted word?")]
    private IEnumerator<SouvenirInstruction> ProcessPhosphorescence(ModuleData module)
    {
        var comp = GetComponent(module, "PhosphorescenceScript");
        var init = GetField<object>(comp, "init").Get();
        var select = GetField<object>(init, "select").Get();

        yield return WaitForSolve;

        var buttonsArray = GetField<Array>(select, "buttons").Get(ar =>
            ar.Length != 8 ? "expected length 8" :
            ar.OfType<object>().Any(v => !SPhosphorescence.ButtonColors.GetAnswers().Contains(v.ToString())) ? "contains unknown color" : null);

        var foundButtons = new List<string>();
        for (var i = 0; i < buttonsArray.GetLength(0); i++)
            foundButtons.Add(buttonsArray.GetValue(i).ToString());

        var index = GetIntField(init, "index").Get(min: 0, max: 419);

        var solutionWord = GetField<string>(init, "solution").Get();
        var wordList = GetField<TextAsset>(comp, "WordList", isPublic: true).Get().text.Split('\n');
        var validWords = wordList.Where(x => x.Length == solutionWord.Length).ToArray(); // This can be from 3-6 letters

        yield return question(SPhosphorescence.ButtonColors).Answers(foundButtons.ToArray());
        yield return question(SPhosphorescence.Offset).Answers(index.ToString());
        yield return question(SPhosphorescence.Word).Answers(solutionWord, preferredWrong: validWords);
    }
}

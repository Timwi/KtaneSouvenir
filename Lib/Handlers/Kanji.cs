using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SKanji
{
    [SouvenirQuestion("What was the displayed word in the {1} stage of {0}?", TwoColumns4Answers, Type = AnswerType.JapaneseFont, ExampleAnswers = ["ばくはつ", "でんき", "でんしゃ", "でんわ"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DisplayedWords
}

public partial class SouvenirModule
{
    [SouvenirHandler("KanjiModule", "Kanji", typeof(SKanji), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessKanji(ModuleData module)
    {
        var comp = GetComponent(module, "KanjiModule");

        var fldCalculating = GetField<bool>(comp, "Calculating");
        var fldStage = GetIntField(comp, "Stage");
        var screen = GetField<TextMesh>(comp, "ScreenText", isPublic: true).Get();
        var displayedWords = new string[3];
        var stage = 0;

        module.Module.OnStrike += () => { stage--; return false; }; // Grab the text on the screen again on strike.

        while (screen.text == "爆発")
            yield return null; // Don’t wait .1 seconds so that we are absolutely sure we get the right stage. (yes I stole this comment :D)
        displayedWords[0] = screen.text;

        for (stage = 1; stage <= 2 || module.Unsolved; stage++)
        {
            while (fldStage.Get(min: stage, max: stage + 1) == stage)
                yield return new WaitForSeconds(.1f); // Stage animation takes much longer than .1 seconds anyway.
            while (fldCalculating.Get())
                yield return null; // Don’t wait .1 seconds so that we are absolutely sure we get the right stage.
            if (stage < 3)
                displayedWords[stage] = screen.text;
            yield return new WaitForSeconds(.1f); // Keep looping until solve here so we can still grab the text in the event of a strike on the last stage.
        }

        var wordLists = new string[][]
        {
            GetArrayField<string>(comp, "Stage1Words").Get(arr => !arr.Contains(displayedWords[0]) ? $"expected array to contain \"{displayedWords[0]}\"" : null),
            GetArrayField<string>(comp, "Stage2Char").Get(arr => !arr.Contains(displayedWords[1]) ? $"expected array to contain \"{displayedWords[1]}\"" : null),
            GetArrayField<string>(comp, "Stage3Words").Get(arr => !arr.Contains(displayedWords[2]) ? $"expected array to contain \"{displayedWords[2]}\"" : null)
        };
        for (var stage = 0; stage < 3; stage++)
            yield return question(SKanji.DisplayedWords, args: [Ordinal(stage + 1)]).Answers(displayedWords[stage], preferredWrong: wordLists[stage]);
    }
}
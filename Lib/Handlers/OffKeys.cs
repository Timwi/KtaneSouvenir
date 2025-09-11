using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SOffKeys
{
    [SouvenirQuestion("Which of these keys played at an incorrect pitch in {0}?", ThreeColumns6Answers, "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B")]
    IncorrectPitch,
    
    [SouvenirQuestion("Which of these runes was displayed in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "OffKeysSprites")]
    Runes
}

public partial class SouvenirModule
{
    [SouvenirHandler("offKeys", "Off Keys", typeof(SOffKeys), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessOffKeys(ModuleData module)
    {
        var comp = GetComponent(module, "OffKeysScript");
        yield return WaitForSolve;

        var notes = new string[] { "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B" };
        var qs = new List<QandA>();

        var faultyKeys = GetListField<int>(comp, "FaultyKeys").Get();
        qs.Add(makeQuestion(Question.OffKeysIncorrectPitch, module, correctAnswers: faultyKeys.Select(i => notes[i]).ToArray()));

        var pickedSymbols = GetArrayField<int>(comp, "PickedSymbols").Get();
        var correctSymbols = pickedSymbols.Select(i => OffKeysSprites[i]).ToArray();

        qs.Add(makeQuestion(Question.OffKeysRunes, module, correctAnswers: correctSymbols));

        addQuestions(module, qs);
    }
}
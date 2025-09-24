using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGarfieldKart
{
    [SouvenirQuestion("What was the track in {0}?", OneColumn4Answers, ExampleAnswers = ["Play Misty for Me", "Sneak-A-Peak", "Blazing Oasis", "Pastacosi Factory", "Mysterious Temple", "Prohibited Site"])]
    Track,

    [SouvenirQuestion("How many puzzle pieces did {0} have?", TwoColumns4Answers, "0", "1", "2", "3")]
    PuzzleCount
}

public partial class SouvenirModule
{
    [SouvenirHandler("garfieldKart", "Garfield Kart", typeof(SGarfieldKart), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessGarfieldKart(ModuleData module)
    {
        var comp = GetComponent(module, "garfieldKartScript");
        yield return WaitForSolve;

        var allAnswers = GetListField<string>(comp, "trackNames", isPublic: true).Get(expectedLength: 16);
        var answerIndex = GetIntField(comp, "trackNum").Get(min: 0, max: allAnswers.Count - 1);
        var puzzlePiecesMeshRenders = GetArrayField<GameObject>(comp, "PuzzlePieces", isPublic: true).Get(expectedLength: 3).Select(obj => obj.GetComponent<MeshRenderer>());
        var materials = GetArrayField<Material>(comp, "PuzzleMats", isPublic: true).Get(expectedLength: 2);
        var puzzleCount = puzzlePiecesMeshRenders.Count(mr => mr.material.name.Substring(0, 6).Trim() == materials[1].name);

        yield return question(SGarfieldKart.Track).Answers(allAnswers[answerIndex], all: allAnswers.ToArray());
        yield return question(SGarfieldKart.PuzzleCount).Answers(puzzleCount.ToString());

        // Allow other Souvenirs to get the puzzle count
        yield return null;

        // Change the puzzle pieces to orange
        foreach (var mr in puzzlePiecesMeshRenders)
            mr.material = materials[0];
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SQwirkle
{
    [SouvenirQuestion("What tile did you place {1} in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "QwirkleSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    TilesPlaced
}

public partial class SouvenirModule
{
    [SouvenirHandler("qwirkle", "Qwirkle", typeof(SQwirkle), "GoodHood")]
    private IEnumerator<SouvenirInstruction> ProcessQwirkle(ModuleData module)
    {
        var comp = GetComponent(module, "qwirkleScript");
        yield return WaitForSolve;

        var tilesPlaces = GetField<IList>(comp, "placed").Get(l => l.Count != 4 ? "expected length 4" : null);
        var tilesIndex = new int[4];

        for (var i = 0; i < 4; i++)
        {
            var colourIndex = GetIntField(tilesPlaces[i], "color", isPublic: true).Get(min: 0, max: 5);
            var shapeIndex = GetIntField(tilesPlaces[i], "shape", isPublic: true).Get(min: 0, max: 5);
            tilesIndex[i] = shapeIndex * 6 + colourIndex;
        }

        addQuestions(module,
            Enumerable.Range(0, 4).Select(tile => makeQuestion(SQwirkle.TilesPlaced, module,
            formatArgs: new[] { Ordinal(tile + 1) }, correctAnswers: new[] { QwirkleSprites[tilesIndex[tile]] })));
    }
}
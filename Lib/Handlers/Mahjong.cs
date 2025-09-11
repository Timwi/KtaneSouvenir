using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMahjong
{
    [SouvenirQuestion("Which tile was shown in the bottom-left of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "MahjongSprites")]
    CountingTile,
    
    [SouvenirQuestion("Which tile was part of the {1} matched pair in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "MahjongSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Matches
}

public partial class SouvenirModule
{
    [SouvenirHandler("MahjongModule", "Mahjong", typeof(SMahjong), "River")]
    private IEnumerator<SouvenirInstruction> ProcessMahjong(ModuleData module)
    {
        var comp = GetComponent(module, "MahjongModule");

        // Capture the player’s matching pairs until the module is solved
        var taken = GetArrayField<bool>(comp, "_taken").Get();
        var currentTaken = taken.ToArray();
        var matchedTiles = new List<int>();

        while (true)
        {
            yield return null;
            if (!currentTaken.SequenceEqual(taken))
            {
                matchedTiles.AddRange(Enumerable.Range(0, taken.Length).Where(ix => currentTaken[ix] != taken[ix]));
                if (taken.All(x => x))
                    break;
                currentTaken = taken.ToArray();
            }
        }
        yield return WaitForSolve;

        // Remove the counting tile, complete with smoke animation
        var countingTile = GetField<MeshRenderer>(comp, "CountingTile", true).Get();
        if (countingTile.gameObject.activeSelf)     // Do it only if another Souvenir module on the same bomb hasn’t already done it
        {
            var smoke = GetField<ParticleSystem>(comp, "Smoke1", true).Get();
            GetField<KMAudio>(comp, "Audio", true).Get().PlaySoundAtTransform("Elimination", countingTile.transform);
            smoke.transform.localPosition = countingTile.transform.localPosition;
            smoke.Play();
            countingTile.gameObject.SetActive(false);
        }

        // Stuff for the “counting tile” question (bottom-left of the module)
        var countingTileName = countingTile.material.mainTexture.name.Replace(" normal", "");
        var countingTileSprite = MahjongSprites.FirstOrDefault(x => x.name == countingTileName) ?? throw new AbandonModuleException($"The sprite for the counting tile ({countingTileName}) doesn’t exist.");

        // Stuff for the “matching tiles” question
        var matchRow1 = GetArrayField<int>(comp, "_matchRow1").Get();
        var matchRow2 = GetArrayField<int>(comp, "_matchRow2").Get();
        var tileSelectables = GetArrayField<KMSelectable>(comp, "Tiles", true).Get();

        var tileSprites = matchRow1.Concat(matchRow2).Select(ix => MahjongSprites[ix]).ToArray();
        var matchedTileSpriteNames = matchedTiles.Select(ix => tileSelectables[ix].GetComponent<MeshRenderer>().material.mainTexture.name.Replace(" normal", "").Replace(" highlighted", "")).ToArray();
        var matchedTileSprites = matchedTileSpriteNames.Select(name => tileSprites.FirstOrDefault(spr => spr.name == name)).ToArray();

        var invalidIx = matchedTileSprites.IndexOf(spr => spr == null);
        if (invalidIx != -1)
            throw new AbandonModuleException($"The sprite for one of the matched tiles ({matchedTileSpriteNames[invalidIx]}) doesn’t exist. matchedTileSpriteNames=[{matchedTileSpriteNames.JoinString(", ")}], matchedTileSprites=[{matchedTileSprites.Select(spr => spr == null ? "<null>" : spr.name).JoinString(", ")}], countingRow=[{GetArrayField<int>(comp, "_countingRow").Get().JoinString(", ")}], matchRow1=[{matchRow1.JoinString(", ")}], matchRow2=[{matchRow2.JoinString(", ")}], tileSprites=[{tileSprites.Select(spr => spr.name).JoinString(", ")}]");

        addQuestions(module,
            makeQuestion(Question.MahjongCountingTile, module, correctAnswers: new[] { countingTileSprite }, preferredWrongAnswers: GetArrayField<int>(comp, "_countingRow").Get().Select(ix => MahjongSprites[ix]).ToArray()),
            makeQuestion(Question.MahjongMatches, module, formatArgs: new[] { "first" }, correctAnswers: matchedTileSprites.Take(2).ToArray(), preferredWrongAnswers: tileSprites),
            makeQuestion(Question.MahjongMatches, module, formatArgs: new[] { "second" }, correctAnswers: matchedTileSprites.Skip(2).Take(2).ToArray(), preferredWrongAnswers: tileSprites));
    }
}
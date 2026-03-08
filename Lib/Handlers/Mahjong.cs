using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMahjong
{
    [SouvenirQuestion("Which tile was shown in the bottom-left of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "MahjongSprites")]
    CountingTile
}

public partial class SouvenirModule
{
    [SouvenirHandler("MahjongModule", "Mahjong", typeof(SMahjong), "River")]
    private IEnumerator<SouvenirInstruction> ProcessMahjong(ModuleData module)
    {
        var comp = GetComponent(module, "MahjongModule");

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

        yield return question(SMahjong.CountingTile).Answers(countingTileSprite, preferredWrong: GetArrayField<int>(comp, "_countingRow").Get().Select(ix => MahjongSprites[ix]).ToArray());
    }
}

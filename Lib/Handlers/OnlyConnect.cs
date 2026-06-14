using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SOnlyConnect
{
    [Question("Which Egyptian hieroglyph was in the {1} in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right"], ArgumentGroupSize = 1, Type = AnswerType.Sprites, SpriteFieldName = "OnlyConnectSprites")]
    QHieroglyphs,

    [Discriminator("the Only Connect where this hieroglyph was in the {0}", Arguments = ["top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right"], ArgumentGroupSize = 1, UsesQuestionSprite = true, TranslateArguments = [true])]
    DHieroglyphs
}

public partial class SouvenirModule
{
    [Handler("OnlyConnectModule", "Only Connect", typeof(SOnlyConnect), "Timwi")]
    [ManualQuestion("What were the positions of the Egyptian hieroglyphs?")]
    private IEnumerator<SouvenirInstruction> ProcessOnlyConnect(ModuleData module)
    {
        var comp = GetComponent(module, "OnlyConnectModule");
        yield return WaitForActivate;

        var hieroglyphsDisplayed = GetArrayField<int>(comp, "_hieroglyphsDisplayed").Get(expectedLength: 6, validator: v => v is < 0 or > 5 ? "expected range 0–5" : null);

        yield return WaitForSolve;

        var positions = new[] { "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right" };
        for (var i = 0; i < positions.Length; i++)
        {
            yield return new Discriminator(SOnlyConnect.DHieroglyphs, $"hieroglyph-{i}", hieroglyphsDisplayed[i], args: [positions[i]], questionSprite: OnlyConnectSprites[hieroglyphsDisplayed[i]]);
            yield return question(SOnlyConnect.QHieroglyphs, args: [positions[i]])
                .AvoidDiscriminators($"hieroglyph-{i}")
                .Answers(OnlyConnectSprites[hieroglyphsDisplayed[i]]);
        }
    }
}

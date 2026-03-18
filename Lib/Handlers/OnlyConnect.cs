using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SOnlyConnect
{
    [SouvenirQuestion("Which Egyptian hieroglyph was in the {1} in {0}?", TwoColumns4Answers, "Two Reeds", "Lion", "Twisted Flax", "Horned Viper", "Water", "Eye of Horus", TranslateAnswers = true, TranslateArguments = [true], Arguments = ["top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right"], ArgumentGroupSize = 1)]
    QHieroglyphs,

    [SouvenirDiscriminator("the Only Connect where the Egyptian hieroglyph in the {1} was {0}", Arguments = ["Two Reeds", "top left", "Lion", "top middle", "Twisted Flax", "top right", "Horned Viper", "bottom left", "Water", "bottom middle", "Eye of Horus", "bottom right"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    DHierohlyphs
}

public partial class SouvenirModule
{
    [SouvenirHandler("OnlyConnectModule", "Only Connect", typeof(SOnlyConnect), "Timwi")]
    [SouvenirManualQuestion("What were the positions of the Egyptian hieroglyphs?")]
    private IEnumerator<SouvenirInstruction> ProcessOnlyConnect(ModuleData module)
    {
        var comp = GetComponent(module, "OnlyConnectModule");
        yield return WaitForActivate;

        var hieroglyphsDisplayed = GetArrayField<int>(comp, "_hieroglyphsDisplayed").Get(expectedLength: 6, validator: v => v is < 0 or > 5 ? "expected range 0–5" : null);

        yield return WaitForSolve;

        var hieroglyphs = new[] { "Two Reeds", "Lion", "Twisted Flax", "Horned Viper", "Water", "Eye of Horus" };
        var positions = new[] { "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right" };
        for (var i = 0; i < positions.Length; i++)
        {
            yield return new Discriminator(SOnlyConnect.DHierohlyphs, $"hieroglyph-{i}", hieroglyphs[hieroglyphsDisplayed[i]], args: [hieroglyphs[hieroglyphsDisplayed[i]], positions[i]]);
            yield return question(SOnlyConnect.QHieroglyphs, args: [positions[i]])
                .AvoidDiscriminators($"hieroglyph-{i}")
                .Answers(hieroglyphs[hieroglyphsDisplayed[i]]);
        }
    }
}
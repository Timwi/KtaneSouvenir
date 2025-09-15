using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SOnlyConnect
{
    [SouvenirQuestion("Which Egyptian hieroglyph was in the {1} in {0}?", TwoColumns4Answers, "Two Reeds", "Lion", "Twisted Flax", "Horned Viper", "Water", "Eye of Horus", TranslateAnswers = true, TranslateArguments = [true], Arguments = ["top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right"], ArgumentGroupSize = 1)]
    Hieroglyphs
}

public partial class SouvenirModule
{
    [SouvenirHandler("OnlyConnectModule", "Only Connect", typeof(SOnlyConnect), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessOnlyConnect(ModuleData module)
    {
        var comp = GetComponent(module, "OnlyConnectModule");
        yield return WaitForActivate;

        var hieroglyphsDisplayed = GetArrayField<int>(comp, "_hieroglyphsDisplayed").Get(expectedLength: 6, validator: v => v is < 0 or > 5 ? "expected range 0–5" : null);

        yield return WaitForSolve;

        var hieroglyphs = new[] { "Two Reeds", "Lion", "Twisted Flax", "Horned Viper", "Water", "Eye of Horus" };
        var positions = new[] { "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right" };
        addQuestions(module, positions.Select((p, i) => makeQuestion(Question.OnlyConnectHieroglyphs, module, formatArgs: new[] { p }, correctAnswers: new[] { hieroglyphs[hieroglyphsDisplayed[i]] })));
    }
}
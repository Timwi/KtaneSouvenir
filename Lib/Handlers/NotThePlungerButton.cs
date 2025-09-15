using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotThePlungerButton
{
    [SouvenirQuestion("What color did the background flash in {0}?", TwoColumns4Answers, "Black", "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", "White", TranslateAnswers = true)]
    Background
}

public partial class SouvenirModule
{
    [SouvenirHandler("notPlungerButtonModule", "Not The Plunger Button", typeof(SNotThePlungerButton), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessNotThePlungerButton(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "NotThePlungerButtonScript");
        var colors = GetArrayField<int>(comp, "chosenColors").Get(expectedLength: 3, validator: v => v is < 0 or > 7 ? "Expected range [0, 7]" : null);
        yield return question(SNotThePlungerButton.Background).Answers(colors.Select(i => Question.NotThePlungerButtonBackground.GetAnswers()[i]).ToArray());
    }
}
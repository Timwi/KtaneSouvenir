using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGreenButton
{
    [SouvenirQuestion("What was the word submitted in {0}?", ThreeColumns6Answers, ExampleAnswers = ["model", "vigor", "pedal", "relic", "lemon", "spoke", "brick", "berry", "equal", "loopy", "trash", "learn", "amuse", "valve", "bench", "igloo", "maybe", "fluid", "truck", "torch"])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("GreenButtonModule", "Green Button", typeof(SGreenButton), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessGreenButton(ModuleData module)
    {
        var comp = GetComponent(module, "GreenButtonScript");
        var words = GetStaticField<List<string>>(comp.GetType(), "_words").Get().Select(w => w[0] + w.Substring(1).ToLowerInvariant()).ToArray();

        yield return WaitForSolve;

        var displayedString = GetField<string>(comp, "_displayedString").Get(validator: str => str.Length != 7 ? "expected length 7" : null);
        var submission = GetArrayField<bool>(comp, "_submission").Get(expectedLength: 7);
        var submittedWord = Enumerable.Range(0, displayedString.Length).Select(ix => submission[ix] ? displayedString.Substring(ix, 1) : "").JoinString();
        yield return question(SGreenButton.Word).Answers(submittedWord[0] + submittedWord.Substring(1).ToLowerInvariant(), preferredWrong: words);
    }
}
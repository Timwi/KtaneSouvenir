using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotTextField
{
    [SouvenirQuestion("Which letter appeared 9 times at the start of {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'F')]
    BackgroundLetter,

    [SouvenirQuestion("Which letter was pressed in the first stage of {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings('A', 'F')]
    InitialPresses
}

public partial class SouvenirModule
{
    [SouvenirHandler("notTextField", "Not Text Field", typeof(SNotTextField), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessNotTextField(ModuleData module)
    {
        var comp = GetComponent(module, "NotTextFieldScript");
        var hasStruck = false;
        module.Module.OnStrike += delegate () { hasStruck = true; return false; };

        var fldSolution = GetArrayField<char>(comp, "solution");
        var solution = fldSolution.Get(expectedLength: 3).Select(x => x.ToString()).ToArray();
        var fldBG = GetField<char>(comp, "bgChar");

        while (module.Unsolved)
        {
            if (hasStruck)
            {
                hasStruck = false;
                solution = fldSolution.Get(expectedLength: 3).Select(x => x.ToString()).ToArray();
            }
            yield return new WaitForSeconds(.1f);
        }

        var bgChar = fldBG.Get(ch => ch is < 'A' or > 'F' ? "expected in range A-F" : null);

        yield return question(SNotTextField.BackgroundLetter).Answers(bgChar.ToString());
        yield return question(SNotTextField.InitialPresses).Answers(solution);
    }
}
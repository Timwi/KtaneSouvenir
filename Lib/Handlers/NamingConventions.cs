using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNamingConventions
{
    [SouvenirQuestion("What was the label of the first button in {0}?", TwoColumns4Answers, "Class", "Constructor", "Method", "Argument", "Local", "Constant", "Field", "Property", "Delegate", "Enum")]
    Object
}

public partial class SouvenirModule
{
    [SouvenirHandler("NamingConventions", "Naming Conventions", typeof(SNamingConventions), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessNamingConventions(ModuleData module)
    {
        var comp = GetComponent(module, "NamingConventionsScript");
        yield return WaitForSolve;

        // Set the relevant button to "naming"
        var texts = GetArrayField<int[]>(comp, "_textIndexes").Get(expectedLength: 7);
        texts[0] = new int[] { 11, 0, 10, 8, 11, 6, -1, -1, -1, -1 };

        var type = (int) GetProperty<object>(comp, "DataType").Get(v => (int) v is < 0 or > 9 ? "expected data type 0–9" : null);
        var ans = Ut.Attributes[Question.NamingConventionsObject].AllAnswers[type];
        yield return question(SNamingConventions.Object).Answers(ans);
    }
}
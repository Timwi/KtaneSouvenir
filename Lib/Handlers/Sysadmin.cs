using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSysadmin
{
    [SouvenirQuestion("What error code did you fix in {0}?", ThreeColumns6Answers, ExampleAnswers = ["391M", "4HZZ", "56OW", "6RO0", "6WMJ", "8V94", "CYB6", "HR71", "PT68", "X8IZ"])]
    FixedErrorCodes
}

public partial class SouvenirModule
{
    [SouvenirHandler("sysadmin", "Sysadmin", typeof(SSysadmin), "NickLatkovich")]
    private IEnumerator<SouvenirInstruction> ProcessSysadmin(ModuleData module)
    {
        var comp = GetComponent(module, "SysadminModule");
        yield return WaitForSolve;

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
            yield return legitimatelyNoQuestion(module, "The module was force-solved.");

        var fixedErrorCodes = GetProperty<HashSet<string>>(comp, "fixedErrorCodes", true).Get();
        if (fixedErrorCodes.Count == 0)
            yield return legitimatelyNoQuestion(module, "There are no errors to ask about.");
        var allErrorCodes = GetStaticProperty<HashSet<string>>(comp.GetType(), "allErrorCodes", true).Get();
        yield return question(SSysadmin.FixedErrorCodes).Answers(fixedErrorCodes.ToArray(), preferredWrong: allErrorCodes.ToArray());
    }
}
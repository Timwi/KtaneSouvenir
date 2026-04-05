using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSUSadmin
{
    [Question("Which security protocol was installed in {0}?", TwoColumns4Answers, "ByteDefender", "Kasperovich", "Awast", "MedicWeb", "Disco", "MOD32")]
    Security,

    [Question("What was the version number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99)]
    Version
}

public partial class SouvenirModule
{
    [Handler("susadmin", "SUSadmin", typeof(SSUSadmin), "Anonymous")]
    [ManualQuestion("Which security protocols were installed?")]
    [ManualQuestion("What was the version number?")]
    private IEnumerator<SouvenirInstruction> ProcessSUSadmin(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "SusadminModule");

        var protocols = GetListField<int>(comp, "securityProtocols").Get(expectedLength: 3, validator: v => v is < 0 or > 5 ? "Expected range [0, 5]" : null);
        var version = GetIntField(comp, "osVersion").Get();

        yield return question(SSUSadmin.Security).Answers(protocols.Select(i => SSUSadmin.Security.GetAnswers()[i]).ToArray());
        yield return question(SSUSadmin.Version).Answers(version.ToString());
    }
}

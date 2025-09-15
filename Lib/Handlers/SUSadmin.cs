using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSUSadmin
{
    [SouvenirQuestion("Which security protocol was installed in {0}?", TwoColumns4Answers, "ByteDefender", "Kasperovich", "Awast", "MedicWeb", "Disco", "MOD32")]
    Security
}

public partial class SouvenirModule
{
    [SouvenirHandler("susadmin", "SUSadmin", typeof(SSUSadmin), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessSUSadmin(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "SusadminModule");
        var protocols = GetListField<int>(comp, "securityProtocols").Get(expectedLength: 3, validator: v => v is < 0 or > 5 ? "Expected range [0, 5]" : null);
        yield return question(SSUSadmin.Security).Answers(protocols.Select(i => Question.SUSadminSecurity.GetAnswers()[i]).ToArray());
    }
}
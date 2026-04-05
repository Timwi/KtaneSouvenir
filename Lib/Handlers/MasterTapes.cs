using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMasterTapes
{
    [Question("Which song was played in {0}?", OneColumn4Answers, "Redemption Song", "Do You Want To Know A Secret", "La Bamba", "Rock-A-Hula Baby", "Pickney Gal", "Dogs", "Young Americans", "Duvet", "Shadows Of Lost Days")]
    PlayedSong
}

public partial class SouvenirModule
{
    [Handler("masterTape", "Master Tapes", typeof(SMasterTapes), "Kuro")]
    [ManualQuestion("Which song was played?")]
    private IEnumerator<SouvenirInstruction> ProcessMasterTapes(ModuleData module)
    {
        var comp = GetComponent(module, "MasterTape");

        yield return WaitForSolve;

        var songIndex = GetIntField(comp, "currentSong").Get(min: 1, max: 9) - 1;
        yield return question(SMasterTapes.PlayedSong).Answers(SMasterTapes.PlayedSong.GetAnswers()[songIndex]);
    }
}
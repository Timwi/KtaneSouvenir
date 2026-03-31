using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SScorchingAlchemist
{
    [SouvenirQuestion("Which sword was present on the ground on {0}?", TwoColumns4Answers, "Plain Edge", "Rock Crusher", "Shining Horn", "Rising Sun", "Metal Breaker", "Murakumo Type-0", "Alexander", "Blizzard Edge", "Saba Luga", "Phantom Slayer", "Gustbringer", "Demon Rouser", "Judgement Halo", "Libra King's Sword", "Desert Seeker", "Pride of Kings", "Duke Nightmare", "Great Caesar", "Cosmolore", "Ixion", "Pegasus Lord", "Sword of Sin", "Dorgenedge", "Zeo Sychros")]
    SwordNames
}

public partial class SouvenirModule
{
    [SouvenirHandler("ScorchingAlchemist", "Scorching Alchemist", typeof(SScorchingAlchemist), "thunder725")]
    private IEnumerator<SouvenirInstruction> ProcessScorchingAlchemist(ModuleData module)
    {
        var comp = GetComponent(module, "ScorchingAlchemist");

        yield return WaitForSolve;

        var startingSwordName = GetField<string>(comp, "startingSwordName").Get();

        yield return question(SScorchingAlchemist.SwordNames).Answers(startingSwordName);
    }
}

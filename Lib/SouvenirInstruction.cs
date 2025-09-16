using UnityEngine;

namespace Souvenir;

public abstract class SouvenirInstruction
{
    public static implicit operator SouvenirInstruction(QandAStump qa) => new QandAInstruction(qa);
    public static implicit operator SouvenirInstruction(Discriminator dis) => new DiscriminatorInstruction(dis);
    public static implicit operator SouvenirInstruction(YieldInstruction obj) => new SouvenirYieldInstruction(obj);
}

public sealed class QandAInstruction(QandAStump q) : SouvenirInstruction
{
    public QandAStump Stump { get; } = q;
}

public sealed class DiscriminatorInstruction(Discriminator discriminator) : SouvenirInstruction
{
    public Discriminator Discriminator { get; } = discriminator;
}

public sealed class WaitForActivateInstruction : SouvenirInstruction
{
    public static WaitForActivateInstruction Instance = new();
}

public sealed class WaitForSolveInstruction : SouvenirInstruction
{
    public static WaitForSolveInstruction Instance = new();
}

public sealed class WaitForUnignoredModulesInstruction : SouvenirInstruction
{
    public static WaitForUnignoredModulesInstruction Instance = new();
}

public sealed class SouvenirYieldInstruction(YieldInstruction obj) : SouvenirInstruction
{
    public YieldInstruction Object { get; } = obj;
}

public sealed class LegitimatelyNoQuestionInstruction : SouvenirInstruction
{
    public static LegitimatelyNoQuestionInstruction Instance = new();
}

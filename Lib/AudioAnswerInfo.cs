namespace Souvenir;

public readonly struct AudioAnswerInfo(float multiplier, string foreignKey)
{
    public float Multiplier { get; } = multiplier;
    public string ForeignKey { get; } = foreignKey;
}

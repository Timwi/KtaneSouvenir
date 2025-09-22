using System;
using System.Linq;
using System.Text;

namespace Souvenir;

public abstract class SouvenirGimmickAttribute() : Attribute
{
    public abstract string ApplyGimmick(string questionText, object[] args);
}

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class MssNgvWlsGimmickAttribute() : SouvenirGimmickAttribute
{
    public override string ApplyGimmick(string questionText, object[] args)
    {
        if (args is not { Length: 2 } arr || arr[1] is not string { Length: > 0 } vowels)
            return questionText;

        const int MinWordLength = 2;
        const int MaxWordLength = 6;
        const float SpaceChance = 0.33f;

        using var letters = questionText.Normalize().GetEnumerator();
        var newText = new StringBuilder();
        var curWordLen = 0;
        while (letters.MoveNext())
        {
            if (char.IsWhiteSpace(letters.Current) || vowels.Contains(char.ToUpperInvariant(letters.Current)))
                continue;
            if (char.IsLetter(letters.Current) && ((curWordLen >= MinWordLength && UnityEngine.Random.value < SpaceChance) || curWordLen >= MaxWordLength))
            {
                newText.Append(' ');
                curWordLen = 0;
            }
            newText.Append(curWordLen == 0 ? char.ToUpperInvariant(letters.Current) : char.ToLowerInvariant(letters.Current));
            curWordLen++;
        }
        return newText.ToString();
    }
}

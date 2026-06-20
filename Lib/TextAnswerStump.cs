using System.Linq;

namespace Souvenir;

public class TextAnswerStump(string[] correct, string[] preferredWrong, string[] all, TextAnswerInfo info) : AnswerStump<string>(correct, preferredWrong, all)
{
    public TextAnswerInfo Info { get; } = info;
    protected static readonly InfoType[] _textAnswerTypes = Ut.GetEnumValues<InfoType>().Where(a => a is InfoType.DynamicFont or >= 0).ToArray();
    protected override InfoType[] acceptableTypes => _textAnswerTypes;
    protected override AnswerSet MakeAnswerSet(string[] answers, int correctIndex, QuestionAttribute qAttr, SouvenirModule souvenir) =>
        new TextAnswerSet(answers, correctIndex, qAttr, Info);
    public override string ToString() => $"{Correct.Stringify()}{(Info.Font != null ? $"; font={Info.Font.name}" : "")}";
}

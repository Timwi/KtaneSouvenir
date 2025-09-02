using System;
using System.Linq;
using UnityEngine;

namespace Souvenir;

public abstract class QuestionStump(Enum enumValue, SouvenirModule souvenir)
{
    public Enum EnumValue { get; private set; } = enumValue;
    public SouvenirModule Souvenir { get; private set; } = souvenir;
    public SouvenirHandlerAttribute HandlerAttribute => EnumValue.GetHandlerAttribute();
    public SouvenirQuestionAttribute QuestionAttribute => EnumValue.GetQuestionAttribute();

    public QandAStump Answers(string correct, string[] preferredWrong = null, string[] all = null, TextAnswerInfo info = default) => Answers([correct], preferredWrong, all, info);
    public QandAStump Answers(string[] correct, string[] preferredWrong = null, string[] all = null, TextAnswerInfo info = default) => new(this, new TextAnswerStump(correct, preferredWrong, all, info));
    public QandAStump Answers(Sprite correct, Sprite[] preferredWrong = null, Sprite[] all = null) => Answers([correct], preferredWrong, all);
    public QandAStump Answers(Sprite[] correct, Sprite[] preferredWrong = null, Sprite[] all = null) => new(this, new SpriteAnswerStump(correct, preferredWrong, all));
    public QandAStump Answers(AudioClip correct, AudioClip[] preferredWrong = null, AudioClip[] all = null, AudioAnswerInfo info = default) => Answers([correct], preferredWrong, all, info);
    public QandAStump Answers(AudioClip[] correct, AudioClip[] preferredWrong = null, AudioClip[] all = null, AudioAnswerInfo info = default) => new(this, new AudioAnswerStump(correct, preferredWrong, all, info));
    public QandAStump Answers(Coord correct, Coord[] preferredWrong = null) => Answers([correct], preferredWrong);
    public QandAStump Answers(Coord[] correct, Coord[] preferredWrong = null)
    {
        var w = correct[0].Width;
        var h = correct[0].Height;
        if (correct.Concat(preferredWrong ?? []).Any(c => c.Width != w || c.Height != h))
            throw new AbandonModuleException($"The handler provided grid coordinates for different sizes of grids.");
        return new(this, new CoordAnswerStump(correct, preferredWrong, Enumerable.Range(0, w * h).Select(ix => new Coord(w, h, ix)).ToArray()));
    }

    public abstract QuestionBase MakeQuestion(string moduleFormat, SouvenirModule souvenir);
}

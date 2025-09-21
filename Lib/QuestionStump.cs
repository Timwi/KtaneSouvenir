using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Souvenir;

public abstract class QuestionStump(Enum enumValue, SouvenirModule souvenir, string[] args)
{
    public Enum EnumValue { get; private set; } = enumValue;
    public SouvenirModule Souvenir { get; private set; } = souvenir;
    public string[] Args { get; private set; } = args;

    public string[] DiscriminatorIdsToAvoid { get; protected set; }
    public Enum[] DiscriminatorsToAvoid { get; protected set; }

    public SouvenirHandlerAttribute HandlerAttribute => EnumValue.GetHandlerAttribute();
    public SouvenirQuestionAttribute QuestionAttribute => EnumValue.GetQuestionAttribute();

    public QandAStump Answers(string correct, string[] preferredWrong = null, string[] all = null, TextAnswerInfo info = default) => Answers([correct], preferredWrong, all, info);
    public QandAStump Answers(string[] correct, string[] preferredWrong = null, string[] all = null, TextAnswerInfo info = default) => new(this, new TextAnswerStump(correct, preferredWrong, all, info));
    public QandAStump Answers(Sprite correct, Sprite[] preferredWrong = null, Sprite[] all = null) => Answers([correct], preferredWrong, all);
    public QandAStump Answers(Sprite[] correct, Sprite[] preferredWrong = null, Sprite[] all = null) => new(this, new SpriteAnswerStump(correct, preferredWrong, all ?? EnumValue.GetAllSprites(Souvenir)));
    public QandAStump Answers(AudioClip correct, AudioClip[] preferredWrong = null, AudioClip[] all = null) => Answers([correct], preferredWrong, all);
    public QandAStump Answers(AudioClip[] correct, AudioClip[] preferredWrong = null, AudioClip[] all = null) => new(this, new AudioAnswerStump(correct, preferredWrong, all ?? EnumValue.GetAllSounds(Souvenir)));
    public QandAStump Answers(Coord correct, Coord[] preferredWrong = null) => Answers([correct], preferredWrong);
    public QandAStump Answers(Coord[] correct, Coord[] preferredWrong = null)
    {
        var w = correct[0].Width;
        var h = correct[0].Height;
        if (correct.Concat(preferredWrong ?? []).Any(c => c.Width != w || c.Height != h))
            throw new AbandonModuleException($"The handler provided grid coordinates for different sizes of grids.");
        return new(this, new CoordAnswerStump(correct, preferredWrong, Enumerable.Range(0, w * h).Select(ix => new Coord(w, h, ix)).ToArray()));
    }

    public abstract QuestionBase MakeQuestion(string moduleFormat, Sprite questionSpriteFromDiscriminator, float questionSpriteRotationFromDiscriminator);

    public QuestionStump AvoidDiscriminators(params string[] discriminatorIds) => AvoidDiscriminators((IEnumerable<string>) discriminatorIds);
    public QuestionStump AvoidDiscriminators(IEnumerable<string> discriminatorIds)
    {
        DiscriminatorIdsToAvoid = (discriminatorIds as string[]) ?? discriminatorIds.ToArray();
        return this;
    }

    public QuestionStump AvoidDiscriminators(params Enum[] discriminators) => AvoidDiscriminators((IEnumerable<Enum>) discriminators);
    public QuestionStump AvoidDiscriminators(IEnumerable<Enum> discriminators)
    {
        DiscriminatorsToAvoid = (discriminators as Enum[]) ?? discriminators.ToArray();
        return this;
    }

    protected string FormattedQuestion(string moduleFormat)
    {
        var attr = EnumValue.GetQuestionAttribute();
        var allFormatArgs = new object[Args != null ? Args.Length + 1 : 1];
        allFormatArgs[0] = moduleFormat;

        if (Args != null)
            for (var i = 0; i < Args.Length; i++)
                allFormatArgs[i + 1] = SouvenirModule.Snip(attr.TranslateArguments is { } ta && ta[i] ? Souvenir.TranslateQuestionArgument(EnumValue, Args[i]) : Args[i]);

        return string.Format(Souvenir.TranslateQuestion(EnumValue), allFormatArgs);
    }

    public override string ToString() => $"{EnumValue.GetType().Name}.{EnumValue} {Args.Stringify()}{(DiscriminatorsToAvoid == null ? "" : $" (avoid: {DiscriminatorsToAvoid.Stringify()})")}{(DiscriminatorIdsToAvoid == null ? "" : $" (avoid: {DiscriminatorIdsToAvoid.Stringify()})")}";
}

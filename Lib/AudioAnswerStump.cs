using UnityEngine;

namespace Souvenir;

public class AudioAnswerStump(AudioClip[] correct, AudioClip[] preferredWrong, AudioClip[] all) : AnswerStump<AudioClip>(correct, preferredWrong, all)
{
    protected override AnswerType[] acceptableTypes => [AnswerType.Audio];
    protected override AnswerSet MakeAnswerSet(AudioClip[] answers, int correctIndex, SouvenirQuestionAttribute qAttr, SouvenirModule souvenir) => new AudioAnswerSet(qAttr, answers, correctIndex, souvenir);
    public override string ToString() => $"audio {Correct.Stringify()}";
}

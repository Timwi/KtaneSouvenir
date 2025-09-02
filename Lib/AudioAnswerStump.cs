using UnityEngine;

namespace Souvenir;

public class AudioAnswerStump(AudioClip[] correct, AudioClip[] preferredWrong, AudioClip[] all, AudioAnswerInfo info) : AnswerStump<AudioClip>(correct, preferredWrong, all)
{
    protected override AnswerType[] acceptableTypes => _standardAnswerTypes;
    protected override AnswerSet MakeAnswerSet(AudioClip[] answers, int correctIndex, AnswerLayout layout, SouvenirModule souvenir) => new AudioAnswerSet(layout, answers, correctIndex, souvenir, info);
}

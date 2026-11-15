using System.Collections.Generic;
using UnityEngine;

namespace Souvenir;

public class AudioAnswerStump(AudioClip[] correct, AudioClip[] preferredWrong, AudioClip[] all, IEnumerable<AudioClip> answerGenerator = null) : AnswerStump<AudioClip>(correct, preferredWrong, all, answerGenerator)
{
    protected override InfoType[] acceptableTypes => [InfoType.Audio];
    protected override AnswerSet MakeAnswerSet(AudioClip[] answers, int correctIndex, QuestionAttribute qAttr, SouvenirModule souvenir) => new AudioAnswerSet(qAttr, answers, correctIndex, souvenir);
    public override string ToString() => Correct.Stringify();
}

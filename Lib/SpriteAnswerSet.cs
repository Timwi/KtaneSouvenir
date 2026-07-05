using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Souvenir;

public class SpriteAnswerSet(AnswerLayout layout, Sprite[] answers, int correctIndex, float xStretch = 1f) : AnswerSet(answers.Length, correctIndex, layout)
{
    protected readonly Sprite[] _answers = answers;
    public override object[] Answers => _answers;

    protected override float _multiColumnVerticalSpacing => 7.75f;

    public override IEnumerable<string> DebugAnswers => _answers.Distinct().Select(a => a.name);
    protected override int NumAnswersProvided => _answers.Length;

    public override void BlinkAnswer(bool on, SouvenirModule souvenir, int answerIndex) => souvenir.Answers[answerIndex].transform.Find("SpriteHolder").gameObject.SetActive(on);

    protected override void RenderAnswers(SouvenirModule souvenir)
    {
        for (var i = 0; i < souvenir.Answers.Length; i++)
        {
            var spriteRenderer = souvenir.Answers[i].transform.Find("SpriteHolder").GetComponent<SpriteRenderer>();
            spriteRenderer.gameObject.SetActive(i < _answers.Length);
            spriteRenderer.sprite = i < _answers.Length ? _answers[i] : null;

            // Just for X-Ray, implement a hacky way to get a sprite to show up vertically flipped: give it a name starting with “Souvenir_FlipY_”
            spriteRenderer.transform.localScale = new Vector3(20 * xStretch, spriteRenderer.sprite?.name?.StartsWith("Souvenir_FlipY_") ?? false ? -20 : 20, 1);
        }
    }
}

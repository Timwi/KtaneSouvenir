using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Souvenir;

public class CoordAnswerSet(AnswerLayout layout, Coord[] answers, int correctIndex) : AnswerSet(answers.Length, correctIndex, layout)
{
    protected readonly Coord[] _answers = answers;
    public override object[] Answers => _answers;

    protected override float _multiColumnVerticalSpacing => 7.75f;

    public override IEnumerable<string> DebugAnswers => _answers.Select(a => a.ToString());
    protected override int NumAnswersProvided => _answers.Length;

    public override void BlinkAnswer(bool on, SouvenirModule souvenir, int answerIndex) => souvenir.Answers[answerIndex].transform.Find("SpriteHolder").gameObject.SetActive(on);

    protected override void RenderAnswers(SouvenirModule souvenir)
    {
        for (var i = 0; i < souvenir.Answers.Length; i++)
        {
            var spriteRenderer = souvenir.Answers[i].transform.Find("SpriteHolder").GetComponent<SpriteRenderer>();
            spriteRenderer.gameObject.SetActive(i < _answers.Length);
            spriteRenderer.sprite = i < _answers.Length ? Sprites.GenerateGridSprite(_answers[i]) : null;
            spriteRenderer.transform.localScale = new Vector3(20, 20, 1);
        }
    }
}

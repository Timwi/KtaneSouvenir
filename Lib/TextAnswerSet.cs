using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Souvenir;

public sealed class TextAnswerSet(string[] answers, int correctIndex, SouvenirQuestionAttribute qAttr, TextAnswerInfo info) : AnswerSet(answers.Length, correctIndex, qAttr.Layout)
{
    public override IEnumerable<string> DebugAnswers => answers.Distinct();
    protected override int NumAnswersProvided => answers.Length;
    public override object[] Answers => answers;

    public override void BlinkAnswer(bool on, SouvenirModule souvenir, int answerIndex) => souvenir.Answers[answerIndex].transform.Find("AnswerText").gameObject.SetActive(on);

    protected override void RenderAnswers(SouvenirModule souvenir)
    {
        for (var i = 0; i < souvenir.Answers.Length; i++)
        {
            var mesh = souvenir.Answers[i].transform.Find("AnswerText").GetComponent<TextMesh>();
            mesh.gameObject.SetActive(true);

            mesh.text = i < answers.Length ? answers[i] : "•";
            mesh.font = qAttr.Type == AnswerType.DynamicFont ? info.Font : souvenir.Fonts[(int) qAttr.Type];
            mesh.fontSize = qAttr.FontSize;
            mesh.characterSize = qAttr.CharacterSize;
            mesh.GetComponent<MeshRenderer>().material = souvenir.FontMaterial;
            mesh.GetComponent<MeshRenderer>().material.mainTexture = qAttr.Type == AnswerType.DynamicFont ? info.FontTexture : souvenir.FontTextures[(int) qAttr.Type];

            // Determine size of the answer and if it’s too long, shrink it horizontally to make it fit
            var origRotation = mesh.transform.localRotation;
            mesh.transform.eulerAngles = new(90, 0, 0);
            mesh.transform.localScale = new(1, 1, 1);
            var bounds = mesh.GetComponent<Renderer>().bounds.size;
            var fac = _layout switch
            {
                AnswerLayout.OneColumn3Answers => 1.5 * souvenir.SurfaceSizeFactor,
                AnswerLayout.OneColumn4Answers => 1.5 * souvenir.SurfaceSizeFactor,
                AnswerLayout.ThreeColumns3Answers => .45 * souvenir.SurfaceSizeFactor,
                AnswerLayout.ThreeColumns6Answers => .45 * souvenir.SurfaceSizeFactor,
                AnswerLayout.TwoColumns2Answers => .7 * souvenir.SurfaceSizeFactor,
                AnswerLayout.TwoColumns4Answers => .7 * souvenir.SurfaceSizeFactor,
                _ => throw new InvalidOperationException("Invalid AnswerLayout.")
            };
            if (bounds.x > fac)
                mesh.transform.localScale = new((float) (fac / bounds.x), 1, 1);
            mesh.transform.localRotation = origRotation;
            mesh.transform.localPosition = new(0, info.RaiseBy, 0);
        }
    }
}

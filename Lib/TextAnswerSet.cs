using System;
using System.Collections.Generic;
using UnityEngine;

namespace Souvenir;

public sealed class TextAnswerSet(AnswerLayout layout, string[] answers, int correctIndex, TextAnswerInfo info) : AnswerSet(answers.Length, correctIndex, layout)
{
    public override IEnumerable<string> DebugAnswers => answers;
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
            mesh.font = info.Font;
            mesh.fontSize = info.FontSize;
            mesh.characterSize = info.CharacterSize;
            mesh.GetComponent<MeshRenderer>().material = info.FontMaterial;
            mesh.GetComponent<MeshRenderer>().material.mainTexture = info.FontTexture;

            // Determine size of the answer and if it’s too long, shrink it horizontally to make it fit
            var origRotation = mesh.transform.localRotation;
            mesh.transform.eulerAngles = new Vector3(90, 0, 0);
            mesh.transform.localScale = new Vector3(1, 1, 1);
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
                mesh.transform.localScale = new Vector3((float) (fac / bounds.x), 1, 1);
            mesh.transform.localRotation = origRotation;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Souvenir;

public abstract class AnswerSet(int numAnswers, int correctIndex, AnswerLayout layout)
{
    protected readonly AnswerLayout _layout = layout;
    protected virtual float _multiColumnVerticalSpacing => 6.25f;

    public int NumAnswersAllowed { get; private set; } = numAnswers;
    public int CorrectIndex { get; private set; } = correctIndex;

    protected abstract int NumAnswersProvided { get; }
    public abstract IEnumerable<string> DebugAnswers { get; }
    public abstract object[] Answers { get; }

    public abstract void BlinkAnswer(bool on, SouvenirModule souvenir, int answerIndex);
    protected abstract void RenderAnswers(SouvenirModule souvenir);

    public void SetAnswers(SouvenirModule souvenir)
    {
        switch (_layout)
        {
            case AnswerLayout.OneColumn3Answers: SetUpOneColumn3Answers(souvenir); break;
            case AnswerLayout.OneColumn4Answers: SetUpOneColumn4Answers(souvenir); break;
            case AnswerLayout.TwoColumns2Answers: SetUpTwoColumn2Answers(souvenir); break;
            case AnswerLayout.TwoColumns4Answers: SetUpTwoColumn4Answers(souvenir); break;
            case AnswerLayout.ThreeColumns3Answers: SetUpThreeColumn3Answers(souvenir); break;
            case AnswerLayout.ThreeColumns6Answers: SetUpThreeColumn6Answers(souvenir); break;
            default: throw new InvalidOperationException("Unexpected AnswerLayout value.");
        }
        RenderAnswers(souvenir);
    }

    protected virtual void SetUpOneColumn3Answers(SouvenirModule souvenir)
    {
        SetupAnswers(souvenir, souvenir.HighlightVeryLong,
            getX: i => -18.125f,
            getZ: i => -17f + 5 * (2 - i),
            boxCenter: new Vector3(17, 0, 0),
            boxSize: new Vector3(38, 5, 3));

        SetSelectableChildren(souvenir, rowLength: 1, 0, 1, 2, 3, 4, 5);
    }

    protected virtual void SetUpOneColumn4Answers(SouvenirModule souvenir)
    {
        SetupAnswers(souvenir, souvenir.HighlightVeryLong,
            getX: i => -18.125f,
            getZ: i => -17f + 5 * (3 - i),
            boxCenter: new Vector3(17, 0, 0),
            boxSize: new Vector3(38, 5, 3));

        SetSelectableChildren(souvenir, rowLength: 1, 0, 1, 2, 3, 4, 5);
    }

    protected virtual void SetUpTwoColumn2Answers(SouvenirModule souvenir)
    {
        SetupAnswers(souvenir, souvenir.HighlightLong,
            getX: i => -18.125f + 19.375f * (i % 2),
            getZ: i => -22.5f + _multiColumnVerticalSpacing * (1 - i / 2),
            boxCenter: new Vector3(8, 0, 0),
            boxSize: new Vector3(19, 6, 3));

        if (Application.isEditor)
            SetSelectableChildren(souvenir, rowLength: 3, 0, 1, 2, 3, 4, 5);
        else
            SetSelectableChildren(souvenir, rowLength: 2, 0, 1);
    }

    protected virtual void SetUpTwoColumn4Answers(SouvenirModule souvenir)
    {
        SetupAnswers(souvenir, souvenir.HighlightLong,
            getX: i => -18.125f + 19.375f * (i / 2),
            getZ: i => -16.25f + _multiColumnVerticalSpacing * (1 - i % 2),
            boxCenter: new Vector3(8, 0, 0),
            boxSize: new Vector3(19, 6, 3));

        if (Application.isEditor)
            SetSelectableChildren(souvenir, rowLength: 3, 0, 2, 4, 1, 3, 5);
        else
            SetSelectableChildren(souvenir, rowLength: 2, 0, 2, 1, 3);
    }

    protected virtual void SetUpThreeColumn3Answers(SouvenirModule souvenir)
    {
        SetupAnswers(souvenir, souvenir.HighlightShort,
            getX: i => -18.125f + 13.125f * (i % 3),
            getZ: i => -22.5f + _multiColumnVerticalSpacing * (1 - i / 3),
            boxCenter: new Vector3(5, 0, 0),
            boxSize: new Vector3(13, 6, 3));

        SetSelectableChildren(souvenir, rowLength: 3, 0, 1, 2, 3, 4, 5);
    }

    protected virtual void SetUpThreeColumn6Answers(SouvenirModule souvenir)
    {
        SetupAnswers(souvenir, souvenir.HighlightShort,
            getX: i => -18.125f + 13.125f * (i / 2),
            getZ: i => -16.25f + _multiColumnVerticalSpacing * (1 - i % 2),
            boxCenter: new Vector3(5, 0, 0),
            boxSize: new Vector3(13, 6, 3));

        SetSelectableChildren(souvenir, rowLength: 3, 0, 2, 4, 1, 3, 5);
    }

    protected void SetupAnswers(SouvenirModule souvenir, Mesh highlightMesh, Func<int, float> getX, Func<int, float> getZ, Vector3 boxCenter, Vector3 boxSize)
    {
        for (var i = 0; i < souvenir.Answers.Length; i++)
        {
            souvenir.Answers[i].gameObject.SetActive(Application.isEditor || i < NumAnswersAllowed);
            souvenir.Answers[i].transform.Find("SpriteHolder").gameObject.SetActive(false);
            souvenir.Answers[i].transform.Find("AnswerText").gameObject.SetActive(false);
            souvenir.Answers[i].transform.Find("PlayHead").gameObject.SetActive(false);
            souvenir.Answers[i].transform.Find("PlayIcon").gameObject.SetActive(false);
            var h1 = souvenir.Answers[i].transform.Find("Highlight");
            h1.GetComponent<MeshFilter>().sharedMesh = highlightMesh;
            var h2 = h1.Find("Highlight(Clone)");
            if (h2 != null)
                h2.GetComponent<MeshFilter>().sharedMesh = highlightMesh;
            souvenir.Answers[i].transform.localPosition = new Vector3(getX(i), 2.525f, getZ(i));
            souvenir.Answers[i].GetComponent<BoxCollider>().center = boxCenter;
            souvenir.Answers[i].GetComponent<BoxCollider>().size = boxSize;
        }
    }

    protected void SetSelectableChildren(SouvenirModule souvenir, int rowLength, params int[] order) =>
        souvenir
            .RootSelectable
            .UpdateChildren(rowLength, order
                .Select(o => o < NumAnswersProvided || Application.isEditor ? souvenir.Answers[o] : null)
                .ToArray());

    // Can return true to indicate Souvenir should skip normal button handling.
    public virtual bool OnPress(int index) => false;
}

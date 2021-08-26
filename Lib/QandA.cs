using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Souvenir
{
    abstract class QandA
    {
        public string ModuleNameWithThe { get; private set; }
        public string QuestionText { get; private set; }
        public Sprite QuestionSprite { get; private set; }
        public int CorrectIndex { get; private set; }
        public int NumAnswers { get; private set; } // must be 4 or 6

        public QandA(string module, string question, int correct, int numAnswers, Sprite sprite = null)
        {
            ModuleNameWithThe = module;
            QuestionText = question;
            QuestionSprite = sprite;
            CorrectIndex = correct;
            NumAnswers = numAnswers;
        }

        public abstract void SetAnswers(SouvenirModule souvenir);
        public string DebugString => string.Format("{0} — {1}", QuestionText, DebugAnswers.Select((a, ix) => string.Format(ix == CorrectIndex ? "[_{0}_]" : "{0}", a)).JoinString(" | "));
        public abstract IEnumerable<string> DebugAnswers { get; }
        public abstract double DesiredHeightFactor { get; }
        public abstract void BlinkCorrectAnswer(bool on, SouvenirModule souvenir);

        protected void SetupTwoColumnAnswers(SouvenirModule souvenir, float xSpacing, float ySpacing)
        {
            SetupAnswers(souvenir, NumAnswers > 4 ? souvenir.HighlightShort : souvenir.HighlightLong,
                getX: i => -18.125f + xSpacing * (i / 2),
                getZ: i => -16.25f + ySpacing * (1 - i % 2),
                boxCenter: new Vector3(NumAnswers > 4 ? 5 : 8, 0, 0),
                boxSize: new Vector3(NumAnswers > 4 ? 13 : 19, 6, 3));
        }

        protected void SetupAnswers(SouvenirModule souvenir, Mesh highlightMesh, Func<int, float> getX, Func<int, float> getZ, Vector3 boxCenter, Vector3 boxSize)
        {
            for (int i = 0; i < souvenir.Answers.Length; i++)
            {
                souvenir.Answers[i].gameObject.SetActive(Application.isEditor || i < NumAnswers);
                souvenir.Answers[i].transform.Find("SpriteHolder").gameObject.SetActive(false);
                souvenir.Answers[i].transform.Find("AnswerText").gameObject.SetActive(false);
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
    }

    sealed class QandAText : QandA
    {
        private readonly string[] _answers;
        private readonly Font _font;
        private readonly int _fontSize;
        private readonly Texture _fontTexture;
        private readonly Material _fontMaterial;
        private readonly AnswerLayout _layout;
        public QandAText(string module, string question, int correct, string[] answers, Font font, int fontSize, Texture fontTexture, Material fontMaterial, AnswerLayout layout)
            : base(module, question, correct, answers.Length)
        {
            _answers = answers;
            _font = font;
            _fontSize = fontSize;
            _fontTexture = fontTexture;
            _fontMaterial = fontMaterial;
            _layout = layout;
        }
        public override IEnumerable<string> DebugAnswers => _answers;
        public override double DesiredHeightFactor => _layout == AnswerLayout.OneColumn4Answers ? .825 : 1.1;

        public override void SetAnswers(SouvenirModule souvenir)
        {
            if (_layout == AnswerLayout.OneColumn4Answers)
            {
                SetupAnswers(souvenir, souvenir.HighlightVeryLong,
                    getX: i => -18.125f,
                    getZ: i => -17f + 5 * (3 - i),
                    boxCenter: new Vector3(17, 0, 0),
                    boxSize: new Vector3(38, 5, 3));
            }
            else
                SetupTwoColumnAnswers(souvenir, _answers.Length > 4 ? 13.125f : 19.375f, 6.25f);
            for (int i = 0; i < souvenir.Answers.Length; i++)
            {
                var mesh = souvenir.Answers[i].transform.Find("AnswerText").GetComponent<TextMesh>();
                mesh.gameObject.SetActive(true);

                mesh.text = i < _answers.Length ? _answers[i] : "•";
                mesh.font = _font;
                mesh.fontSize = _fontSize;
                mesh.GetComponent<MeshRenderer>().material = _fontMaterial;
                mesh.GetComponent<MeshRenderer>().material.mainTexture = _fontTexture;

                // Determine size of the answer and if it’s too long, shrink it horizontally to make it fit
                var origRotation = mesh.transform.localRotation;
                mesh.transform.eulerAngles = new Vector3(90, 0, 0);
                mesh.transform.localScale = new Vector3(1, 1, 1);
                var bounds = mesh.GetComponent<Renderer>().bounds.size;
                var fac = (_layout == AnswerLayout.OneColumn4Answers ? 1.5 : _answers.Length > 4 ? .45 : .7) * souvenir.SurfaceSizeFactor;
                if (bounds.x > fac)
                    mesh.transform.localScale = new Vector3((float) (fac / bounds.x), 1, 1);
                mesh.transform.localRotation = origRotation;
            }
        }

        public override void BlinkCorrectAnswer(bool on, SouvenirModule souvenir)
        {
            souvenir.Answers[CorrectIndex].transform.Find("AnswerText").gameObject.SetActive(on);
        }
    }

    sealed class QandASprite : QandA
    {
        private readonly Sprite[] _answers;
        public QandASprite(string module, string question, int correct, Sprite[] answers) : base(module, question, correct, answers.Length) { _answers = answers; }
        public override IEnumerable<string> DebugAnswers => _answers.Select(s => s.name);
        public override double DesiredHeightFactor => 1;

        public override void SetAnswers(SouvenirModule souvenir)
        {
            SetupTwoColumnAnswers(souvenir, _answers.Length > 4 ? 13.125f : 19.375f, 7.75f);
            for (int i = 0; i < souvenir.Answers.Length; i++)
            {
                var spriteRenderer = souvenir.Answers[i].transform.Find("SpriteHolder").GetComponent<SpriteRenderer>();
                spriteRenderer.gameObject.SetActive(i < _answers.Length);
                spriteRenderer.sprite = i < _answers.Length ? _answers[i] : null;
            }
        }

        public override void BlinkCorrectAnswer(bool on, SouvenirModule souvenir)
        {
            souvenir.Answers[CorrectIndex].transform.Find("SpriteHolder").gameObject.SetActive(on);
        }
    }
}

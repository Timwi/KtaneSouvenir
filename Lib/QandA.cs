using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Souvenir
{
    sealed class QandA
    {
        public const string Ordinal = "\ufffdordinal";

        private readonly QuestionBase _question;
        private readonly AnswerSet _answerSet;

        public QandA(Question q, string module, QuestionBase question, AnswerSet answerSet, int correctIndex)
        {
            Question = q;
            ModuleNameWithThe = module;
            _question = question;
            _answerSet = answerSet;
            CorrectIndex = correctIndex;
        }

        public Question Question { get; private set; }
        public string ModuleNameWithThe { get; private set; }
        public int CorrectIndex { get; private set; }
        public int NumAnswers => _answerSet.NumAnswers;

        public string DebugString => $"{_question.DebugText} — {DebugAnswers.Select((a, ix) => string.Format(ix == CorrectIndex ? "[_{0}_]" : "{0}", a)).JoinString(" | ")}";
        public IEnumerable<string> DebugAnswers => _answerSet.DebugAnswers;

        public void SetQandAs(SouvenirModule souvenir)
        {
            _question.SetQuestion(souvenir);
            _answerSet.SetAnswers(souvenir);
        }

        public void BlinkCorrectAnswer(bool on, SouvenirModule souvenir) => _answerSet.BlinkAnswer(on, souvenir, CorrectIndex);

        public abstract class QuestionBase
        {
            protected string _text;
            public virtual string DebugText => _text;

            protected QuestionBase(string question) => _text = question;

            public abstract void SetQuestion(SouvenirModule souvenir);
        }

        public sealed class TextQuestion : QuestionBase
        {
            private readonly Sprite _questionSprite;
            private readonly float _questionSpriteRotation;
            private readonly double _desiredHeightFactor;
            private readonly Translation _translation;

            public TextQuestion(string question, AnswerLayout layout, Sprite questionSprite, float questionSpriteRotation, Translation translation)
                : base(question)
            {
                _desiredHeightFactor = layout == AnswerLayout.OneColumn4Answers ? .825 : 1.1;
                _questionSprite = questionSprite;
                _questionSpriteRotation = questionSpriteRotation;
                _translation = translation;
            }

            public override void SetQuestion(SouvenirModule souv)
            {
                souv.SetWordWrappedText(_text, _desiredHeightFactor, _questionSprite != null);

                if (_questionSprite != null)
                {
                    var sprite = Sprite.Create(_questionSprite.texture, _questionSprite.rect, new Vector2(1, .5f), _questionSprite.pixelsPerUnit);
                    souv.QuestionSprite.sprite = sprite;
                    souv.QuestionSprite.transform.localEulerAngles = new Vector3(90, _questionSpriteRotation);
                    souv.QuestionSprite.gameObject.SetActive(true);
                }
            }
        }

        public sealed class SpriteQuestion : QuestionBase
        {
            private readonly Sprite _questionSprite;

            public SpriteQuestion(string questionText, Sprite questionSprite) : base(questionText) => _questionSprite = questionSprite;

            public override string DebugText => $"(Depicted visually) {base.DebugText}";

            public override void SetQuestion(SouvenirModule souvenir)
            {
                souvenir.EntireQuestionSprite.gameObject.SetActive(true);
                souvenir.EntireQuestionSprite.sprite = _questionSprite;
            }
        }

        public abstract class AnswerSet
        {
            protected readonly AnswerLayout _layout;
            protected float _multiColumnVerticalSpacing = 6.25f;

            protected AnswerSet(int numAnswers, AnswerLayout layout)
            {
                NumAnswers = numAnswers;
                _layout = layout;
            }

            public int NumAnswers { get; private set; } // must be 4 or 6
            public abstract IEnumerable<string> DebugAnswers { get; }

            public abstract void BlinkAnswer(bool on, SouvenirModule souvenir, int answerIndex);
            protected abstract void RenderAnswers(SouvenirModule souvenir);

            public void SetAnswers(SouvenirModule souvenir)
            {
                switch (_layout)
                {
                    case AnswerLayout.OneColumn4Answers: SetUpOneColumnAnswers(souvenir); break;
                    case AnswerLayout.TwoColumns4Answers: SetUpTwoColumnAnswers(souvenir); break;
                    case AnswerLayout.ThreeColumns6Answers: SetUpThreeColumnAnswers(souvenir); break;
                    default: throw new InvalidOperationException("Unexpected AnswerLayout value.");
                }
                RenderAnswers(souvenir);
            }

            protected virtual void SetUpOneColumnAnswers(SouvenirModule souvenir)
            {
                SetupAnswers(souvenir, souvenir.HighlightVeryLong,
                    getX: i => -18.125f,
                    getZ: i => -17f + 5 * (3 - i),
                    boxCenter: new Vector3(17, 0, 0),
                    boxSize: new Vector3(38, 5, 3));
            }

            protected virtual void SetUpTwoColumnAnswers(SouvenirModule souvenir)
            {
                SetupAnswers(souvenir, souvenir.HighlightLong,
                    getX: i => -18.125f + 19.375f * (i / 2),
                    getZ: i => -16.25f + _multiColumnVerticalSpacing * (1 - i % 2),
                    boxCenter: new Vector3(8, 0, 0),
                    boxSize: new Vector3(19, 6, 3));
            }

            protected virtual void SetUpThreeColumnAnswers(SouvenirModule souvenir)
            {
                SetupAnswers(souvenir, souvenir.HighlightShort,
                    getX: i => -18.125f + 13.125f * (i / 2),
                    getZ: i => -16.25f + _multiColumnVerticalSpacing * (1 - i % 2),
                    boxCenter: new Vector3(5, 0, 0),
                    boxSize: new Vector3(13, 6, 3));
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

        public sealed class TextAnswerSet : AnswerSet
        {
            private readonly string[] _answers;
            private readonly Font _font;
            private readonly int _fontSize;
            private readonly float _characterSize;
            private readonly Texture _fontTexture;
            private readonly Material _fontMaterial;

            public TextAnswerSet(int numAnswers, AnswerLayout layout, string[] answers, Font font, int fontSize, float characterSize, Texture fontTexture, Material fontMaterial)
                : base(numAnswers, layout)
            {
                _answers = answers;
                _font = font;
                _fontSize = fontSize;
                _characterSize = characterSize;
                _fontTexture = fontTexture;
                _fontMaterial = fontMaterial;
            }

            public override IEnumerable<string> DebugAnswers => _answers;

            public override void BlinkAnswer(bool on, SouvenirModule souvenir, int answerIndex)
            {
                souvenir.Answers[answerIndex].transform.Find("AnswerText").gameObject.SetActive(on);
            }

            protected override void RenderAnswers(SouvenirModule souvenir)
            {
                for (int i = 0; i < souvenir.Answers.Length; i++)
                {
                    var mesh = souvenir.Answers[i].transform.Find("AnswerText").GetComponent<TextMesh>();
                    mesh.gameObject.SetActive(true);

                    mesh.text = i < _answers.Length ? _answers[i] : "•";
                    mesh.font = _font;
                    mesh.fontSize = _fontSize;
                    mesh.characterSize = _characterSize;
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
        }

        public sealed class SpriteAnswerSet : AnswerSet
        {
            private readonly Sprite[] _answers;

            public SpriteAnswerSet(int numAnswers, AnswerLayout layout, Sprite[] answers)
                : base(numAnswers, layout)
            {
                _answers = answers;
                _multiColumnVerticalSpacing = 7.75f;
            }

            public override IEnumerable<string> DebugAnswers => _answers.Select(a => a.name);

            public override void BlinkAnswer(bool on, SouvenirModule souvenir, int answerIndex)
            {
                souvenir.Answers[answerIndex].transform.Find("SpriteHolder").gameObject.SetActive(on);
            }

            protected override void RenderAnswers(SouvenirModule souvenir)
            {
                for (int i = 0; i < souvenir.Answers.Length; i++)
                {
                    var spriteRenderer = souvenir.Answers[i].transform.Find("SpriteHolder").GetComponent<SpriteRenderer>();
                    spriteRenderer.gameObject.SetActive(i < _answers.Length);
                    spriteRenderer.sprite = i < _answers.Length ? _answers[i] : null;
                }
            }
        }
    }
}

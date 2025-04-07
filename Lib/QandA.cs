using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Souvenir
{
    sealed class QandA
    {
        public const string Ordinal = "\uE047ordinal";

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

        public AnswerSet Answers => _answerSet;
        public Question Question { get; private set; }
        public string ModuleNameWithThe { get; private set; }
        public int CorrectIndex { get; private set; }
        public int NumAnswers => _answerSet.NumAnswersAllowed;

        public string DebugString => $"{_question.DebugText} — {DebugAnswers.Select((a, ix) => string.Format(ix == CorrectIndex ? "[_{0}_]" : "{0}", a)).JoinString(" | ")}";
        public IEnumerable<string> DebugAnswers => _answerSet.DebugAnswers;
        public bool OnPress(int ix) => _answerSet.OnPress(ix);

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

            public override string DebugText => $"{_text}{(_questionSprite == null ? "" : $" (sprite: {_questionSprite.name}{(_questionSpriteRotation != 0 ? $" (rotated {_questionSpriteRotation}°)" : "")})")}";

            public TextQuestion(string question, AnswerLayout layout, Sprite questionSprite, float questionSpriteRotation)
                : base(question)
            {
                _desiredHeightFactor = layout switch
                {
                    AnswerLayout.OneColumn4Answers => .825,
                    AnswerLayout.TwoColumns2Answers => 1.375,
                    AnswerLayout.TwoColumns4Answers => 1.1,
                    AnswerLayout.ThreeColumns6Answers => 1.1,
                    _ => throw new InvalidOperationException("Invalid AnswerLayout."),
                };
                _questionSprite = questionSprite;
                _questionSpriteRotation = questionSpriteRotation;
            }

            public override void SetQuestion(SouvenirModule souv)
            {
                if (_questionSprite != null)
                {
                    var sprite = Sprite.Create(_questionSprite.texture, _questionSprite.rect, new Vector2(1, .5f), _questionSprite.pixelsPerUnit);
                    sprite.name = _questionSprite.name;
                    souv.QuestionSprite.sprite = sprite;
                    souv.QuestionSprite.transform.localEulerAngles = new Vector3(90, _questionSpriteRotation);
                    souv.QuestionSprite.gameObject.SetActive(true);
                }
                souv.SetWordWrappedText(_text, _desiredHeightFactor, _questionSprite != null);
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
                NumAnswersAllowed = numAnswers;
                _layout = layout;
            }

            public int NumAnswersAllowed { get; private set; } // must be 4 or 6
            protected abstract int NumAnswersProvided { get; }
            public abstract IEnumerable<string> DebugAnswers { get; }

            public abstract void BlinkAnswer(bool on, SouvenirModule souvenir, int answerIndex);
            protected abstract void RenderAnswers(SouvenirModule souvenir);

            public void SetAnswers(SouvenirModule souvenir)
            {
                switch (_layout)
                {
                    case AnswerLayout.OneColumn4Answers: SetUpOneColumn4Answers(souvenir); break;
                    case AnswerLayout.TwoColumns2Answers: SetUpTwoColumn2Answers(souvenir); break;
                    case AnswerLayout.TwoColumns4Answers: SetUpTwoColumn4Answers(souvenir); break;
                    case AnswerLayout.ThreeColumns6Answers: SetUpThreeColumn6Answers(souvenir); break;
                    default: throw new InvalidOperationException("Unexpected AnswerLayout value.");
                }
                RenderAnswers(souvenir);
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
                    boxSize: new Vector3(8, 6, 3));

                if (Application.isEditor)
                    SetSelectableChildren(souvenir, rowLength: 3, 0, 2, 4, 1, 3, 5);
                else
                    SetSelectableChildren(souvenir, rowLength: 2, 0, 2, 1, 3);
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
                for (int i = 0; i < souvenir.Answers.Length; i++)
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
            public virtual bool OnPress(int index)
            {
                return false;
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
            protected override int NumAnswersProvided => _answers.Length;

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

        public class SpriteAnswerSet : AnswerSet
        {
            protected readonly Sprite[] _answers;

            public SpriteAnswerSet(int numAnswers, AnswerLayout layout, Sprite[] answers)
                : base(numAnswers, layout)
            {
                _answers = answers;
                _multiColumnVerticalSpacing = 7.75f;
            }

            public override IEnumerable<string> DebugAnswers => _answers.Select(a => a.name);
            protected override int NumAnswersProvided => _answers.Length;

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
                    spriteRenderer.transform.localScale = new Vector3(20, 20, 1);
                }
            }
        }

        public class AudioAnswerSet : SpriteAnswerSet
        {
            private readonly SouvenirModule _parent;
            private readonly string _foreignKey;
            private readonly AudioClip[] _clips;
            internal int _selected = -1;
            private Coroutine _coroutine;
            KMAudio.KMAudioRef _audioRef;

            public AudioAnswerSet(int numAnswers, AnswerLayout layout, AudioClip[] answers, SouvenirModule parent, float multiplier, string foreignKey)
                : base(numAnswers, layout, answers.Select(c => Sprites.RenderWaveform(c, parent, multiplier)).ToArray())
            {
                _parent = parent;
                _foreignKey = foreignKey;
                _clips = answers.ToArray();
            }

            public override void BlinkAnswer(bool on, SouvenirModule souvenir, int answerIndex)
            {
                base.BlinkAnswer(on, souvenir, answerIndex);
                souvenir.Answers[answerIndex].transform.Find("PlayHead").gameObject.SetActive(false);
                souvenir.Answers[answerIndex].transform.Find("PlayIcon").gameObject.SetActive(on);
            }

            protected override void RenderAnswers(SouvenirModule souvenir)
            {
                base.RenderAnswers(souvenir);
                for (int i = 0; i < souvenir.Answers.Length; i++)
                {
                    souvenir.Answers[i].transform.Find("SpriteHolder").localScale = _layout switch
                    {
                        AnswerLayout.TwoColumns4Answers => new Vector3(_parent.TwitchPlaysActive ? 14 : 15, 10, 1),
                        AnswerLayout.ThreeColumns6Answers => new Vector3(_parent.TwitchPlaysActive ? 8 : 10, 10, 1),
                        AnswerLayout.OneColumn4Answers => new Vector3(30, 10, 1),
                        // Unreachable
                        _ => throw new NotImplementedException(),
                    };
                    souvenir.Answers[i].transform.Find("PlayIcon").gameObject.SetActive(i < _answers.Length);
                    souvenir.Answers[i].transform.Find("PlayIcon").GetComponent<SpriteRenderer>().sprite = souvenir.AudioSprites[0];
                    souvenir.Answers[i].transform.Find("PlayIcon").GetComponent<SpriteRenderer>().color = new Color32(0x17, 0xF4, 0x19, 0xFF);
                }
            }

            public override bool OnPress(int index)
            {
                if (index == _selected || index >= NumAnswersAllowed || index >= NumAnswersProvided)
                {
                    StopSound();
                    return false;
                }

                PlaySound(index);
                return true;
            }

            internal void StopSound()
            {
                if (_coroutine != null)
                    _parent.StopCoroutine(_coroutine);

                if (_selected != -1)
                    _parent.Answers[_selected].transform.Find("PlayHead").gameObject.SetActive(false);

                _audioRef?.StopSound();
            }

            public float PlaySound(int index)
            {
                StopSound();

                Deselect();

                _selected = index;

                _parent.Answers[_selected].transform.Find("PlayIcon").GetComponent<SpriteRenderer>().sprite = _parent.AudioSprites[1];
                _parent.Answers[_selected].transform.Find("PlayIcon").GetComponent<SpriteRenderer>().color = new Color32(0xD8, 0x26, 0x26, 0xFF);
                var head = _parent.Answers[_selected].transform.Find("PlayHead");
                head.gameObject.SetActive(true);

                _audioRef = _foreignKey == null || Application.isEditor
                    ? _parent.Audio.HandlePlaySoundAtTransformWithRef?.Invoke(_clips[index].name, _parent.transform, false)
                    : Sounds.PlayForeignClip(_foreignKey, _clips[index].name, _parent.transform);
                _coroutine = _parent.StartCoroutine(AnimatePlayHead(head, _layout switch
                {
                    AnswerLayout.TwoColumns4Answers => _parent.TwitchPlaysActive ? 14 : 15,
                    AnswerLayout.ThreeColumns6Answers => _parent.TwitchPlaysActive ? 8 : 10,
                    AnswerLayout.OneColumn4Answers => 30,
                    // Unreachable  
                    _ => throw new NotImplementedException(),
                }, _clips[index].length));

                return _clips[index].length;
            }

            public void Deselect()
            {
                if (_selected != -1)
                {
                    _parent.Answers[_selected].transform.Find("PlayIcon").GetComponent<SpriteRenderer>().sprite = _parent.AudioSprites[0];
                    _parent.Answers[_selected].transform.Find("PlayIcon").GetComponent<SpriteRenderer>().color = new Color32(0x17, 0xF4, 0x19, 0xFF);
                }

                _selected = -1;
            }

            private IEnumerator AnimatePlayHead(Transform head, float end, float duration)
            {
                float endTime = Time.time + duration;
                head.localPosition = new Vector3(1.5f, 0.35f, 0f);
                while (Time.time < endTime)
                {
                    head.localPosition = new Vector3(Mathf.Lerp(1.5f, 1.5f + end, 1 - (endTime - Time.time) / duration), 0.35f, 0f);
                    yield return null;
                }
                head.gameObject.SetActive(false);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Souvenir;

public class AudioAnswerSet(SouvenirQuestionAttribute qAttr, AudioClip[] answers, int correctIndex, SouvenirModule parent)
    : SpriteAnswerSet(qAttr.Layout, answers.Select(c => Sprites.RenderWaveform(c, parent, qAttr.AudioSizeMultiplier)).ToArray(), correctIndex)
{
    private readonly AudioClip[] _clips = answers.ToArray();
    private readonly string _foreignAudioID = qAttr.ForeignAudioID;
    internal int _selected = -1;
    private Coroutine _coroutine;
    private KMAudio.KMAudioRef _audioRef;
    public override IEnumerable<string> DebugAnswers => _clips.Distinct().Select(c => c.name);

    public override void BlinkAnswer(bool on, SouvenirModule souvenir, int answerIndex)
    {
        base.BlinkAnswer(on, souvenir, answerIndex);
        souvenir.Answers[answerIndex].transform.Find("PlayHead").gameObject.SetActive(false);
        souvenir.Answers[answerIndex].transform.Find("PlayIcon").gameObject.SetActive(on);
    }

    protected override void RenderAnswers(SouvenirModule souvenir)
    {
        base.RenderAnswers(souvenir);
        for (var i = 0; i < souvenir.Answers.Length; i++)
        {
            souvenir.Answers[i].transform.Find("SpriteHolder").localScale = _layout switch
            {
                AnswerLayout.OneColumn3Answers => new Vector3(30, 10, 1),
                AnswerLayout.OneColumn4Answers => new Vector3(30, 10, 1),
                AnswerLayout.TwoColumns2Answers => new Vector3(parent.TwitchPlaysActive ? 14 : 15, 10, 1),
                AnswerLayout.TwoColumns4Answers => new Vector3(parent.TwitchPlaysActive ? 14 : 15, 10, 1),
                AnswerLayout.ThreeColumns3Answers => new Vector3(parent.TwitchPlaysActive ? 8 : 10, 10, 1),
                AnswerLayout.ThreeColumns6Answers => new Vector3(parent.TwitchPlaysActive ? 8 : 10, 10, 1),
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
            parent.StopCoroutine(_coroutine);

        if (_selected != -1)
            parent.Answers[_selected].transform.Find("PlayHead").gameObject.SetActive(false);

        _audioRef?.StopSound();
    }

    public float PlaySound(int index)
    {
        StopSound();

        Deselect();

        _selected = index;

        parent.Answers[_selected].transform.Find("PlayIcon").GetComponent<SpriteRenderer>().sprite = parent.AudioSprites[1];
        parent.Answers[_selected].transform.Find("PlayIcon").GetComponent<SpriteRenderer>().color = new Color32(0xD8, 0x26, 0x26, 0xFF);
        var head = parent.Answers[_selected].transform.Find("PlayHead");
        head.gameObject.SetActive(true);

        _audioRef = _foreignAudioID == null || Application.isEditor
            ? parent.Audio.HandlePlaySoundAtTransformWithRef?.Invoke(_clips[index].name, parent.transform, false)
            : Sounds.PlayForeignClip(_foreignAudioID, _clips[index].name, parent.transform);
        _coroutine = parent.StartCoroutine(AnimatePlayHead(head, _layout switch
        {
            AnswerLayout.OneColumn3Answers => 30,
            AnswerLayout.OneColumn4Answers => 30,
            AnswerLayout.TwoColumns2Answers => parent.TwitchPlaysActive ? 14 : 15,
            AnswerLayout.TwoColumns4Answers => parent.TwitchPlaysActive ? 14 : 15,
            AnswerLayout.ThreeColumns3Answers => parent.TwitchPlaysActive ? 8 : 10,
            AnswerLayout.ThreeColumns6Answers => parent.TwitchPlaysActive ? 8 : 10,
            // Unreachable  
            _ => throw new NotImplementedException(),
        }, _clips[index].length));

        return _clips[index].length;
    }

    public void Deselect()
    {
        if (_selected != -1)
        {
            parent.Answers[_selected].transform.Find("PlayIcon").GetComponent<SpriteRenderer>().sprite = parent.AudioSprites[0];
            parent.Answers[_selected].transform.Find("PlayIcon").GetComponent<SpriteRenderer>().color = new Color32(0x17, 0xF4, 0x19, 0xFF);
        }

        _selected = -1;
    }

    private IEnumerator AnimatePlayHead(Transform head, float end, float duration)
    {
        var endTime = Time.time + duration;
        head.localPosition = new Vector3(1.5f, 0.35f, 0f);
        while (Time.time < endTime)
        {
            head.localPosition = new Vector3(Mathf.Lerp(1.5f, 1.5f + end, 1 - (endTime - Time.time) / duration), 0.35f, 0f);
            yield return null;
        }
        head.gameObject.SetActive(false);
    }
}

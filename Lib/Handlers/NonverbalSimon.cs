using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SNonverbalSimon
{
    [SouvenirQuestion("Which button flashed in the {1} stage in {0}?", TwoColumns4Answers, IsEntireQuestionSprite = true, Type = AnswerType.Sprites, SpriteFieldName = "NonverbalSimonSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Flashes
}

public partial class SouvenirModule
{
    [SouvenirHandler("nonverbalSimon", "❖", typeof(SNonverbalSimon), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessNonverbalSimon(ModuleData module)
    {
        var comp = GetComponent(module, "NonverbalSimonHandler");
        yield return WaitForSolve;

        var flashes = GetMethod<List<string>>(comp, "GrabCombinedFlashes", 0, true).Invoke(new object[0]);
        var qs = new List<QandA>(flashes.Count);
        var names = new[] { "Red", "Orange", "Yellow", "Green" };

        for (var stage = 0; stage < flashes.Count; stage++)
        {
            var name = $"{flashes.Count}-{stage + 1}";
            var tex = NonverbalSimonQuestions.First(t => t.name.Equals(name));

            if (_moduleCounts.Get("nonverbalSimon") > 1)
            {
                var num = module.SolveIndex.ToString();
                var tmp = new Texture2D(400, 320, TextureFormat.ARGB32, false);
                tmp.SetPixels(tex.GetPixels());
                tex = NonverbalSimonQuestions.First(t => t.name.Equals("Name"));
                tmp.SetPixels(40, 90, tex.width, tex.height, tex.GetPixels());
                for (var digit = 0; digit < num.Length; digit++)
                {
                    tex = DigitTextures[num[digit] - '0'];
                    tmp.SetPixels(100 + 40 * digit, 90, tex.width, tex.height, tex.GetPixels());
                }

                tmp.Apply(false, true);
                _questionTexturesToDestroyLater.Add(tmp);
                tex = tmp;
            }

            var q = Sprite.Create(tex, Rect.MinMaxRect(0f, 0f, 400f, 320f), new Vector2(.5f, .5f), 1280f, 1u, SpriteMeshType.Tight);
            q.name = $"NVSQ{stage}-{module.SolveIndex}";
            qs.Add(makeSpriteQuestion(q, SNonverbalSimon.Flashes, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { NonverbalSimonSprites[Array.IndexOf(names, flashes[stage])] }, preferredWrongAnswers: NonverbalSimonSprites));
        }

        addQuestions(module, qs);
    }
}
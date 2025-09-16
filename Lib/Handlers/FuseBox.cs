using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SFuseBox
{
    [SouvenirQuestion("What color flashed {1} in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, IsEntireQuestionSprite = true, SpriteFieldName = "FuseBoxColorSprites", ArgumentGroupSize = 1, Arguments = [QandA.Ordinal])]
    Flashes,

    [SouvenirQuestion("What arrow was shown {1} in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, IsEntireQuestionSprite = true, SpriteFieldName = "FuseBoxArrowSprites", ArgumentGroupSize = 1, Arguments = [QandA.Ordinal])]
    Arrows
}

public partial class SouvenirModule
{
    [SouvenirHandler("FuseBox", "Fuse Box", typeof(SFuseBox), "Anonymous", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessFuseBox(ModuleData module)
    {
        var comp = GetComponent(module, "FuseBoxScript");
        var fldAnimating = GetField<bool>(comp, "animating");
        var fldOpened = GetField<bool>(comp, "opened");
        var mthToggleDoor = GetMethod<IEnumerator>(comp, "ToggleDoor", 0);

        yield return WaitForSolve;

        var children = comp.GetComponent<KMSelectable>().Children;
        if (children.Length == 0)
        {
            // Another Souvenir is already taking care of the coroutine
            while (fldAnimating.Get() || fldOpened.Get())
                yield return new WaitForSeconds(0.1f);
        }
        else
        {
            // Disable all the button handlers
            foreach (var button in children)
                button.OnInteract = () => false;

            // Set the children array to an empty array to signal to other Souvenirs on the same bomb that we’re already taking care of this
            comp.GetComponent<KMSelectable>().Children = new KMSelectable[0];

            while (fldAnimating.Get())
                yield return new WaitForSeconds(0.1f);
            if (fldOpened.Get())
                yield return ((MonoBehaviour) comp).StartCoroutine(mthToggleDoor.Invoke(new object[0]));
        }

        var flashes = GetField<int[]>(comp, "lightColors")
            .Get(arr => arr.Length != 4 ? "Bad length" : arr.Any(i => i is < 0 or > 3) ? "Bad item" : null)
            .ToList();
        var qs = new List<QandA>(8);
        var arrows = GetField<int[]>(comp, "correctButtons")
            .Get(arr => arr.Length != 4 ? "Bad length" : arr.Any(i => i is < 0 or > 3) ? "Bad item" : null)
            .ToList();

        var moduleCount = _moduleCounts.Get("FuseBox");
        for (var ix = 0; ix < 4; ix++)
        {
            var tex = FuseBoxQuestions.First(t => t.name.Equals($"flash{ix + 1}"));
            var tex2 = FuseBoxQuestions.First(t => t.name.Equals($"arrow{ix + 1}"));

            if (moduleCount > 1)
            {
                var num = module.SolveIndex.ToString();
                var tmp = new Texture2D(400, 320, TextureFormat.ARGB32, false);
                var tmp2 = new Texture2D(400, 320, TextureFormat.ARGB32, false);
                tmp.SetPixels(tex.GetPixels());
                tmp2.SetPixels(tex2.GetPixels());

                tex = FuseBoxQuestions.First(t => t.name.Equals("name"));
                tmp.SetPixels(20, 120, tex.width, tex.height, tex.GetPixels());
                tmp2.SetPixels(32, 0, tex.width, tex.height, tex.GetPixels());
                for (var digit = 0; digit < num.Length; digit++)
                {
                    tex = DigitTextures[num[digit] - '0'];
                    tmp.SetPixels(20 + tex.width + 40 * digit, 120, tex.width, tex.height, tex.GetPixels());
                    tmp2.SetPixels(32 + tex.width + 40 * digit, 0, tex.width, tex.height, tex.GetPixels());
                }

                tmp.Apply(false, true);
                tmp2.Apply(false, true);
                _questionTexturesToDestroyLater.Add(tmp);
                _questionTexturesToDestroyLater.Add(tmp2);
                tex = tmp;
                tex2 = tmp2;
            }

            var q = Sprite.Create(tex, Rect.MinMaxRect(0f, 0f, 400f, 320f), new Vector2(.5f, .5f), 1280f, 1u, SpriteMeshType.Tight);
            var q2 = Sprite.Create(tex2, Rect.MinMaxRect(0f, 0f, 400f, 320f), new Vector2(.5f, .5f), 1280f, 1u, SpriteMeshType.Tight);
            q.name = $"FuseBox-Flash-{ix}-{module.SolveIndex}";
            q2.name = $"FuseBox-Arrow-{ix}-{module.SolveIndex}";
            qs.Add(makeSpriteQuestion(q, SFuseBox.Flashes, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { FuseBoxColorSprites[flashes[ix]] }));
            qs.Add(makeSpriteQuestion(q2, SFuseBox.Arrows, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { FuseBoxArrowSprites[arrows[ix]] }));
        }

        addQuestions(module, qs);
    }
}
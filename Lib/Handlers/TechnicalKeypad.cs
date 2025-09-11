using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum STechnicalKeypad
{
    [SouvenirQuestion("What was the {1} displayed digit in {0}?", ThreeColumns6Answers, IsEntireQuestionSprite = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    DisplayedDigits
}

public partial class SouvenirModule
{
    [SouvenirHandler("TechnicalKeypad", "Technical Keypad", typeof(STechnicalKeypad), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessTechnicalKeypad(ModuleData module)
    {
        var comp = GetComponent(module, "TechnicalKeypadModule");
        var digits = GetProperty<string>(GetField<object>(comp, "_keypadInfo").Get(), "Digits", isPublic: true).Get(seq => seq.Length != 12 ? "expected length 12" : null);

        yield return WaitForSolve;

        var qs = new List<QandA>();
        for (var position = 0; position < 12; position++)
        {
            var tex = TechnicalKeypadQuestions[position];
            var tmp = new Texture2D(400, 320, TextureFormat.ARGB32, false);

            tmp.SetPixels(tex.GetPixels());
            tex = TechnicalKeypadQuestions.First(t => t.name.Equals("name"));
            tmp.SetPixels(40, 90, tex.width, tex.height, tex.GetPixels());

            var modCount = _moduleCounts.Get("TechnicalKeypad");
            if (modCount > 1)
            {
                var numText = module.SolveIndex.ToString();
                for (var digit = 0; digit < numText.Length; digit++)
                {
                    tex = DigitTextures[numText[digit] - '0'];
                    tmp.SetPixels(140 + 40 * digit, 90, tex.width, tex.height, tex.GetPixels());
                }
            }

            tmp.Apply(updateMipmaps: false, makeNoLongerReadable: true);
            _questionTexturesToDestroyLater.Add(tmp);
            tex = tmp;

            var questionSprite = Sprite.Create(tex, Rect.MinMaxRect(0, 0, 400, 320), new Vector2(.5f, .5f), 1280f, 1, SpriteMeshType.Tight);
            questionSprite.name = $"Technical-Keypad-{position}-{module.SolveIndex}";
            qs.Add(makeSpriteQuestion(questionSprite, Question.TechnicalKeypadDisplayedDigits, module, formatArgs: new[] { Ordinal(position + 1) }, correctAnswers: new[] { digits[position].ToString() }));
        }
        addQuestions(module, qs);
    }
}
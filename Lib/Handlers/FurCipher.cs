using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SFurCipher
{
    [Question("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    QScreen,

    [Discriminator("the Fur Cipher that had {0} on the {1} screen on page {2}", Arguments = ["AMBUSH", "top", "1", "BANZAI", "middle", "1", "BIGGER", "bottom", "1", "GAMBLE", "top", "2", "KETOSE", "middle", "2", "OCULUS", "bottom", "2"], ArgumentGroupSize = 3, TranslateArguments = [false, true, false])]
    DScreen
}

public partial class SouvenirModule
{
    [Handler("furCipher", "Fur Cipher", typeof(SFurCipher), "Infusion")]
    [ManualQuestion("What was on each screen?")]
    private IEnumerator<SouvenirInstruction> ProcessFurCipher(ModuleData module)
    {
        var comp = GetComponent(module, "furCipherScript");
        var data = comp.GetType().Assembly.GetType("Words.Data") ?? throw new AbandonModuleException("I cannot find the Words.Data type.");

        string[] generateWrongAnswers(string correctAnswer, AnswerGeneratorAttribute<string> gen)
        {
            var set = new HashSet<string> { correctAnswer };
            var iter = 0;
            while (set.Count < 6 && iter < 100)
            {
                foreach (var ans in gen.GetAnswers(this).Take(6 - set.Count))
                    set.Add(ans);
                iter++;
            }
            return set.ToArray();
        }

        yield return WaitForSolve;

        var fldPages = GetArrayField<string[]>(comp, "pages").Get(expectedLength: 2);
        var screens = new string[6];

        for (int i = 0; i < 6; i++)
        {
            screens[i] = fldPages[i / 3][i % 3];
        }

        var possibleWords = GetArrayField<List<string>>(Activator.CreateInstance(data), "_allWords").Get();

        string[] preferredWrongAnswers;

        for (int page = 0; page < 2; page++)
        {
            for (int screen = 0; screen < 3; screen++)
            {
                var text = screens[page * 3 + screen];
                var screenPosition = "";
                switch (screen)
                {
                    case 0: screenPosition = "top"; break;
                    case 1: screenPosition = "middle"; break;
                    case 2: screenPosition = "bottom"; break;
                }
                if (possibleWords.SelectMany(x => x).ToArray().Contains(text))
                    preferredWrongAnswers = possibleWords.SelectMany(x => x).ToArray();
                else if (Regex.IsMatch(text, @"^[A-Z]{6}$"))
                    preferredWrongAnswers = generateWrongAnswers(text, new AnswerGenerator.Strings(text.Length, 'A', 'Z'));
                else
                    preferredWrongAnswers = generateWrongAnswers(text, new AnswerGenerator.Integers(3, 17));
                yield return new Discriminator(SFurCipher.DScreen, $"{page}-{screen}", text, args: [text, screenPosition, (page + 1).ToString()]);
                yield return question(SFurCipher.QScreen, args: [screenPosition.ToString(), (page + 1).ToString()]).AvoidDiscriminators($"{page}-{screen}").Answers(text, preferredWrong: preferredWrongAnswers);
            }
        }
    }
}
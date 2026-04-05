using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SHorribleMemory
{
    [Question("What was the color of the button in the {1} position in the {2} stage of {0}?", ThreeColumns6Answers, "blue", "green", "red", "orange", "purple", "pink", TranslateAnswers = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    QColorsByPosition,

    [Question("What was the color of the button labeled {1} in the {2} stage of {0}?", ThreeColumns6Answers, "blue", "green", "red", "orange", "purple", "pink", TranslateAnswers = true, Arguments = ["1", QandA.Ordinal, "2", QandA.Ordinal, "3", QandA.Ordinal, "4", QandA.Ordinal, "5", QandA.Ordinal, "6", QandA.Ordinal], ArgumentGroupSize = 2)]
    QColorsByLabel,

    [Question("What was the label of the button in the {1} position in the {2} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(1, 6)]
    QLabelsByPosition,

    [Question("What was the label of the {1} button in the {2} stage of {0}?", ThreeColumns6Answers, Arguments = ["blue", QandA.Ordinal, "green", QandA.Ordinal, "red", QandA.Ordinal, "orange", QandA.Ordinal, "purple", QandA.Ordinal, "pink", QandA.Ordinal], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    [AnswerGenerator.Integers(1, 6)]
    QLabelsByColor,

    [Question("What was the position of the {1} button in the {2} stage of {0}?", ThreeColumns6Answers, "first", "second", "third", "fourth", "fifth", "sixth", TranslateAnswers = true, Arguments = ["blue", QandA.Ordinal, "green", QandA.Ordinal, "red", QandA.Ordinal, "orange", QandA.Ordinal, "purple", QandA.Ordinal, "pink", QandA.Ordinal], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    QPositionsByColor,

    [Question("What was the position of the button labeled {1} in the {2} stage of {0}?", ThreeColumns6Answers, "first", "second", "third", "fourth", "fifth", "sixth", TranslateAnswers = true, Arguments = ["1", QandA.Ordinal, "2", QandA.Ordinal, "3", QandA.Ordinal, "4", QandA.Ordinal, "5", QandA.Ordinal, "6", QandA.Ordinal], ArgumentGroupSize = 2)]
    QPositionsByLabel,

    [Question("What number was displayed in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    QDisplays,

    [Discriminator("the Horrible Memory that had a {0} button in the {1} position in the {2} stage", Arguments = ["blue", QandA.Ordinal, QandA.Ordinal, "green", QandA.Ordinal, QandA.Ordinal, "red", QandA.Ordinal, QandA.Ordinal, "orange", QandA.Ordinal, QandA.Ordinal, "purple", QandA.Ordinal, QandA.Ordinal, "pink", QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 3, TranslateArguments = [true, false, false])]
    DColorAndPosition,

    [Discriminator("the Horrible Memory that had a button labeled {0} in the {1} position in the {2} stage", Arguments = ["1", QandA.Ordinal, QandA.Ordinal, "2", QandA.Ordinal, QandA.Ordinal, "3", QandA.Ordinal, QandA.Ordinal, "4", QandA.Ordinal, QandA.Ordinal, "5", QandA.Ordinal, QandA.Ordinal, "6", QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 3)]
    DLabelAndPosition,

    [Discriminator("the Horrible Memory that had a {0} button labeled {1} in the {2} stage", Arguments = ["blue", "1", QandA.Ordinal, "green", "2", QandA.Ordinal, "red", "3", QandA.Ordinal, "orange", "4", QandA.Ordinal, "purple", "5", QandA.Ordinal, "pink", "6", QandA.Ordinal], ArgumentGroupSize = 3, TranslateArguments = [true, false, false])]
    DColorAndLabel,

    [Discriminator("the Horrible Memory that displayed a {0} in the {1} stage", Arguments = ["1", QandA.Ordinal, "2", QandA.Ordinal, "3", QandA.Ordinal, "4", QandA.Ordinal, "5", QandA.Ordinal, "6", QandA.Ordinal], ArgumentGroupSize = 2)]
    DDisplay
}

public partial class SouvenirModule
{
    [Handler("horribleMemory", "Horrible Memory", typeof(SHorribleMemory), "Quinn Wuest")]
    [ManualQuestion("What were the colors and labels of each button in each stage?")]
    [ManualQuestion("What digit was displayed in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessHorribleMemory(ModuleData module)
    {
        var comp = GetComponent(module, "cruelMemoryScript");
        var fldStage = GetField<int>(comp, "stage", isPublic: true);
        var fldButtons = GetArrayField<object>(comp, "buttons", isPublic: true);

        var colors = new string[4][] { new string[6], new string[6], new string[6], new string[6] };
        var labels = new string[4][] { new string[6], new string[6], new string[6], new string[6] };
        var displays = new string[4];
        int tempStage = -1;
        while (module.Unsolved)
        {
            var stage = fldStage.Get() - 1;
            if (stage > 3 || stage == tempStage)
            {
                yield return null;
                continue;
            }
            tempStage = stage;
            var buttons = fldButtons.Get();
            displays[stage] = GetField<int>(comp, "screenIndex").Get().ToString();
            for (int i = 0; i < 6; i++)
            {
                labels[stage][i] = GetField<int>(buttons[i], "labelName", isPublic: true).Get().ToString();
                colors[stage][i] = GetField<string>(buttons[i], "colourName", isPublic: true).Get();
            }
            yield return null;
        }

        for (int stage = 0; stage < 4; stage++)
        {
            for (int pos = 0; pos < 6; pos++)
            {
                yield return new Discriminator(SHorribleMemory.DColorAndPosition, $"CandP-{stage}-{pos}", colors[stage][pos], args: [colors[stage][pos], Ordinal(pos + 1), Ordinal(stage + 1)]);
                yield return new Discriminator(SHorribleMemory.DLabelAndPosition, $"LandP-{stage}-{pos}", labels[stage][pos], args: [labels[stage][pos], Ordinal(pos + 1), Ordinal(stage + 1)]);
                yield return new Discriminator(SHorribleMemory.DColorAndLabel, $"CandL-{stage}-{labels[stage][pos]}", colors[stage][pos], args: [colors[stage][pos], labels[stage][pos], Ordinal(stage + 1)]);

                yield return question(SHorribleMemory.QLabelsByPosition, args: [Ordinal(pos + 1), Ordinal(stage + 1)])
                    .AvoidDiscriminators(SHorribleMemory.DLabelAndPosition)
                    .Answers(labels[stage][pos]);
                yield return question(SHorribleMemory.QLabelsByColor, args: [colors[stage][pos], Ordinal(stage + 1)])
                    .AvoidDiscriminators(SHorribleMemory.DColorAndLabel)
                    .Answers(labels[stage][pos]);
                yield return question(SHorribleMemory.QColorsByPosition, args: [Ordinal(pos + 1), Ordinal(stage + 1)])
                    .AvoidDiscriminators(SHorribleMemory.DColorAndPosition)
                    .Answers(colors[stage][pos]);
                yield return question(SHorribleMemory.QColorsByLabel, args: [labels[stage][pos], Ordinal(stage + 1)])
                    .AvoidDiscriminators(SHorribleMemory.DColorAndLabel)
                    .Answers(colors[stage][pos]);
                yield return question(SHorribleMemory.QPositionsByColor, args: [colors[stage][pos], Ordinal(stage + 1)])
                    .AvoidDiscriminators(SHorribleMemory.DColorAndPosition)
                    .Answers(Ordinal(pos + 1));
                yield return question(SHorribleMemory.QPositionsByLabel, args: [labels[stage][pos], Ordinal(stage + 1)])
                    .AvoidDiscriminators(SHorribleMemory.DLabelAndPosition)
                    .Answers(Ordinal(pos + 1));
            }
            yield return new Discriminator(SHorribleMemory.DDisplay, $"Disp-{stage}", displays[stage], args: [displays[stage], Ordinal(stage + 1)]);
            yield return question(SHorribleMemory.QDisplays, args: [Ordinal(stage + 1)])
                .AvoidDiscriminators(SHorribleMemory.DDisplay)
                .Answers(displays[stage]);
        }
    }
}

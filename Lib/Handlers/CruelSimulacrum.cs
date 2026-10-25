using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCruelSimulacrum
{
    [Question("What colour was the starting prompt in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Yellow", "Magenta", "Cyan", "White", TranslateAnswers = true)]
    StartingPrompt,

    [Question("What colour was the {1} blob in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Yellow", "Magenta", "Cyan", "White", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    BlobColour,

    [Question("Which LED was {1} in the {2} LED set in {0}?", ThreeColumns3Answers, Arguments = ["white", QandA.Ordinal, "grey", QandA.Ordinal, "black", QandA.Ordinal], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    [AnswerGenerator.Strings('A', 'C')]
    LedColour,

    [Question("What was the {1} light movement of the {2} sequence in {0}?", OneColumn3Answers, "Clockwise", "Counter-clockwise", "Flip", TranslateAnswers = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    LightMovements,

    [Discriminator("the Cruel Simulacrum where the starting prompt was {0}", Arguments = ["red", "green", "blue", "yellow", "magenta", "cyan", "white"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    StartingPromptDiscriminator,

    [Discriminator("the Cruel Simulacrum where the {0} blob was {1}", Arguments = [QandA.Ordinal, "red", QandA.Ordinal, "green", QandA.Ordinal, "blue", QandA.Ordinal, "yellow", QandA.Ordinal, "magenta", QandA.Ordinal, "cyan", QandA.Ordinal, "white"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    BlobColourDiscriminator,

    [Discriminator("the Cruel Simulacrum where LED {2} of the {1} LED set was {0}", Arguments = ["white", QandA.Ordinal, "A", "grey", QandA.Ordinal, "B", "black", QandA.Ordinal, "C"], ArgumentGroupSize = 3, TranslateArguments = [true, false, false])]
    LedColourDiscriminator,

    [Discriminator("the Cruel Simulacrum where the {0} light movement of the {1} sequence was {2}", Arguments = [QandA.Ordinal, QandA.Ordinal, "clockwise", QandA.Ordinal, QandA.Ordinal, "counter-clockwise", QandA.Ordinal, QandA.Ordinal, "flip"], ArgumentGroupSize = 3, TranslateArguments = [false, false, true])]
    LightMovementsDiscriminator
}

public partial class SouvenirModule
{
    [Handler("CruelSimulacrum", "Cruel Simulacrum", typeof(SCruelSimulacrum), "thunder725")]
    [ManualQuestion("What was the colour of the prompt?")]
    [ManualQuestion("What were the colours of the blobs?")]
    [ManualQuestion("What were the colour of each LED?")]
    [ManualQuestion("What were the light movements?")]
    private IEnumerator<SouvenirInstruction> ProcessCruelSimulacrum(ModuleData module)
    {
        var comp = GetComponent(module, "CruelSimulacrum");

        yield return WaitForSolve;

        string[] ledsPerStage = GetArrayField<string>(comp, "NodePriorityOrderPerStage")
            .Get(expectedLength: 3, validator: s => s.Length != 3 ? "expected string of length 3" :
            s.Distinct().Count() != s.Length ? "expected string with all unique characters" :
            Regex.IsMatch(s, @"[ABC]") == false ? "expected string with only ABC letters" : null);

        var coloursPerStage = GetField<IList>(comp, "ColourPerStage")
            .Get(validator: v => v.Count != 3 ? "expected List of length 3" : null);

        var lightMovementsPerStage = GetField<IList[]>(comp, "LightMovementsPerStage")
            .Get(validator: v => v.Length != 3 ? "expected array of length 3" : null);

        for (int stage = 0; stage < 3; stage++)
        {
            for (int led = 0; led < 3; led++)
            {
                string ledName = ledsPerStage[stage][led].ToString();
                string colour = led == 0 ? "white" : led == 1 ? "grey" : "black";
                yield return question(SCruelSimulacrum.LedColour, args: [colour, Ordinal(stage + 1)])
                    .AvoidDiscriminators([SCruelSimulacrum.LedColourDiscriminator])
                    .Answers(correct: ledName);

                yield return new Discriminator(SCruelSimulacrum.LedColourDiscriminator, $"LedCol{stage}_{colour}",
                    ledName, args: [colour, Ordinal(stage + 1), ledName]);
            }

            if (stage == 0)
            {
                yield return question(SCruelSimulacrum.StartingPrompt)
                    .AvoidDiscriminators([SCruelSimulacrum.StartingPromptDiscriminator])
                    .Answers(coloursPerStage[0].ToString());

                yield return new Discriminator(SCruelSimulacrum.StartingPromptDiscriminator, "startPrompt",
                    coloursPerStage[0].ToString(), args: [coloursPerStage[0].ToString()]);
            }
            else
            {
                yield return question(SCruelSimulacrum.BlobColour)
                                    .AvoidDiscriminators([SCruelSimulacrum.BlobColourDiscriminator])
                                    .Answers(coloursPerStage[stage].ToString());

                yield return new Discriminator(SCruelSimulacrum.BlobColourDiscriminator, $"blob{stage}",
                    coloursPerStage[stage].ToString(), args: [Ordinal(stage), coloursPerStage[stage].ToString()]);
            }

            for (int moves = 0; moves < lightMovementsPerStage[stage].Count; moves++)
            {
                string answer = lightMovementsPerStage[stage][moves].ToString();
                if (answer == "CounterClockwise") { answer = "Counter-Clockwise"; }

                yield return question(SCruelSimulacrum.LightMovements, args: [Ordinal(moves + 1), Ordinal(stage + 1)])
                    .AvoidDiscriminators(SCruelSimulacrum.LightMovementsDiscriminator)
                    .Answers(answer);

                yield return new Discriminator(SCruelSimulacrum.LightMovementsDiscriminator, $"light{stage}_{moves}", answer,
                    args: [Ordinal(moves + 1), Ordinal(stage + 1), answer]);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Souvenir;
using UnityEngine;

public enum SSixTen
{
    [Question("What was the vertical cycle delay in {0}?", AnswerLayout.TwoColumns4Answers)]
    [AnswerGenerator.Integers(1, 4)]
    QVCycleDelay,

    [Question("What was the horizontal cycle delay of the {1} channel in {0}?", AnswerLayout.TwoColumns4Answers, Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(1, 4)]
    QHCycleDelay,

    [Question("What was the vertical cycle direction in {0}?", AnswerLayout.TwoColumns2Answers, "Up", "Down", TranslateAnswers = true)]
    QVCycleDirection,

    [Question("Which channels cycled {1} in {0}?", AnswerLayout.TwoColumns4Answers, "none", "red", "green", "blue", "red and green", "red and blue", "green and blue", "all of them", Arguments = ["left", "right"], ArgumentGroupSize = 1, TranslateAnswers = true, TranslateArguments = [true])]
    QHCycleDirection,

    [Discriminator("the SixTen whose vertical cycle delay was {0}", Arguments = ["1", "2", "3", "4"], ArgumentGroupSize = 1)]
    DVCycleDelay,

    [Discriminator("the SixTen whose {1} channel had a horizontal cycle delay of {0}", Arguments = ["1", "red", "2", "green", "3", "blue"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    [AnswerGenerator.Integers(1, 4)]
    DHCycleDelay,

    [Discriminator("the SixTen that vertically cycled {0}", Arguments = ["up", "down"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DVCycleDirection,

    [Discriminator("the SixTen whose {0} channel cycled {1}", Arguments = ["red", "left", "green", "right", "blue", "left"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    DHCycleDirection,
}

public partial class SouvenirModule
{
    [Handler("sixten", "SixTen", typeof(SSixTen), "Timwi")]
    [ManualQuestion("What were the cycle delays and directions (vertical and each channel)?")]
    private IEnumerator<SouvenirInstruction> ProcessSixTen(ModuleData module)
    {
        var comp = GetComponent(module, "SixTenScript");
        var vertCycleDelay = GetIntField(comp, "verShiftDelay").Get(min: 1, max: 4);
        var vertCycleDirection = GetIntField(comp, "verShift").Get(v => v is not -1 and not 1 ? "expected -1 or 1" : null);

        // Workaround for getting values from a dictionary whose keys are a custom enum:
        // Use the non-generic IDictionary, whose enumerator returns non-generic DictionaryEntry instead of KeyValuePair<,>
        static IEnumerable<DictionaryEntry> GetKeyValuePairs(IDictionary dic)
        {
            foreach (DictionaryEntry de in dic)
                yield return de;
        }

        var horizCycleDelays = GetKeyValuePairs(GetField<IDictionary>(comp, "horShiftDelay").Get()).ToDictionary(de => de.Key.ToString(), de => (int) de.Value);
        var horizCycleDirections = GetKeyValuePairs(GetField<IDictionary>(comp, "horShift").Get()).ToDictionary(de => de.Key.ToString(), de => (int) de.Value);

        if (!horizCycleDelays.Keys.OrderBy(s => s).SequenceEqual(["Blue", "Green", "Red"]) || !horizCycleDirections.Keys.OrderBy(s => s).SequenceEqual(["Blue", "Green", "Red"]))
            throw new AbandonModuleException($"Abandoning SixTen because ‘horShiftDelay’ and/or ‘horShift’ have unexpected keys (expected red/green/blue):\nhorShiftDelay = {horizCycleDelays.Stringify()}\nhorShift = {horizCycleDirections.Stringify()}");

        if (!horizCycleDelays.Values.OrderBy(s => s).All(i => i >= 1 && i <= 4) || !horizCycleDirections.Values.OrderBy(s => s).All(i => i is -1 or 1))
            throw new AbandonModuleException($"Abandoning SixTen because ‘horShiftDelay’ and/or ‘horShift’ have unexpected values:\nhorShiftDelay (expected 1–4) = {horizCycleDelays.Stringify()}\nhorShift (expected -1 or 1) = {horizCycleDirections.Stringify()}");

        yield return WaitForSolve;

        // Vertical
        yield return question(SSixTen.QVCycleDelay).AvoidDiscriminators(SSixTen.DVCycleDelay).Answers(vertCycleDelay.ToString());
        yield return question(SSixTen.QVCycleDirection).AvoidDiscriminators(SSixTen.DVCycleDirection).Answers(vertCycleDirection == -1 ? "Up" : "Down");
        yield return new Discriminator(SSixTen.DVCycleDelay, "vd", vertCycleDelay, args: [vertCycleDelay.ToString()]);
        yield return new Discriminator(SSixTen.DVCycleDirection, "vs", vertCycleDirection, args: [vertCycleDirection == -1 ? "up" : "down"]);

        // Horizontal
        var channelNamesUc = new[] { "Red", "Green", "Blue" };
        var channelNamesLc = new[] { "red", "green", "blue" };
        var channelCombos = new[] { "none", "red", "green", "red and green", "blue", "red and blue", "green and blue", "all of them" };
        for (var channel = 0; channel < 3; channel++)
        {
            yield return question(SSixTen.QHCycleDelay, args: [channelNamesLc[channel]]).AvoidDiscriminators(SSixTen.DHCycleDelay).Answers(horizCycleDelays[channelNamesUc[channel]].ToString());
            yield return new Discriminator(SSixTen.DHCycleDelay, $"hd-{channel}", horizCycleDelays[channelNamesUc[channel]], args: [horizCycleDelays[channelNamesUc[channel]].ToString(), channelNamesLc[channel]]);
            yield return new Discriminator(SSixTen.DHCycleDirection, $"hs-{channel}", horizCycleDirections[channelNamesUc[channel]], args: [channelNamesLc[channel], horizCycleDirections[channelNamesUc[channel]] == -1 ? "left" : "right"]);
        }
        foreach (var hDir in new[] { -1, 1 })
        {
            var combo = 0;
            for (var channel = 0; channel < 3; channel++)
                if (horizCycleDirections[channelNamesUc[channel]] == hDir)
                    combo |= (1 << channel);
            yield return question(SSixTen.QHCycleDirection, args: [hDir == -1 ? "left" : "right"]).AvoidDiscriminators(SSixTen.DHCycleDirection).Answers(channelCombos[combo]);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimonSelects
{
    [SouvenirQuestion("Which color flashed {1} in the {2} stage of {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Magenta", "Cyan", TranslateAnswers = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Order
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonSelectsModule", "Simon Selects", typeof(SSimonSelects), "tachatat18")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSelects(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSelectsScript");
        yield return WaitForSolve;

        var order = Enumerable.Range(0, 3).Select(i => GetArrayField<int>(comp, $"stg{i + 1}order").Get(minLength: 3, maxLength: 5)).ToArray();
        var btnRenderers = GetArrayField<Renderer>(comp, "buttonrend", isPublic: true).Get(expectedLength: 8);

        // Sequences of colors that flashes in each stage
        var seqs = new string[3][];

        // Parsing the received string
        for (var stage = 0; stage < 3; stage++)
        {
            var parsedString = new string[order[stage].Length];
            for (var flash = 0; flash < order[stage].Length; flash++)
                parsedString[flash] = btnRenderers[order[stage][flash]].material.name.Replace(" (Instance)", "");
            seqs[stage] = parsedString;
        }

        // Used to validate colors
        string[] colorNames = { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Magenta", "Cyan" };

        if (seqs.Any(seq => seq.Any(color => !colorNames.Contains(color))))
            throw new AbandonModuleException($"‘colors’ contains an invalid color: [{seqs.Select(seq => seq.JoinString(", ")).JoinString("; ")}]");

        addQuestions(module, seqs.SelectMany((seq, stage) => seq.Select((col, ix) => makeQuestion(Question.SimonSelectsOrder, module,
            formatArgs: new[] { Ordinal(ix + 1), Ordinal(stage + 1) },
            correctAnswers: new[] { col }))));
    }
}
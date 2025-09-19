using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SBlueButton
{
    [SouvenirQuestion("What was D in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Integers(1, 4)]
    D,

    [SouvenirQuestion("What was {1} in {0}?", TwoColumns4Answers, Arguments = ["E", "F", "G", "H"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 3)]
    EFGH,

    [SouvenirQuestion("What was M in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 9)]
    M,

    [SouvenirQuestion("What was N in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(4, 9)]
    N,

    [SouvenirQuestion("What was P in {0}?", ThreeColumns6Answers, "♠♥♣", "♠♣♥", "♥♠♣", "♥♣♠", "♣♠♥", "♣♥♠")]
    P,

    [SouvenirQuestion("What was Q in {0}?", ThreeColumns6Answers, "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow", TranslateAnswers = true)]
    Q,

    [SouvenirQuestion("What was X in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Integers(1, 5)]
    X,

    [SouvenirDiscriminator("the Blue Button where Q was {0}", Arguments = ["Blue", "Green", "Cyan", "Red", "Magenta", "Yellow"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DQ,

    [SouvenirDiscriminator("the Blue Button where {0} was {1}")]
    DOther
}

public partial class SouvenirModule
{
    [SouvenirHandler("BlueButtonModule", "Blue Button", typeof(SBlueButton), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessBlueButton(ModuleData module)
    {
        var comp = GetComponent(module, "BlueButtonScript");

        var suitsGoal = GetArrayField<int>(comp, "_suitsGoal").Get(expectedLength: 4);
        var colorStageColors = GetArrayField<int>(comp, "_colorStageColors").Get();
        var jumps = GetArrayField<int>(comp, "_jumps").Get(expectedLength: 4, validator: v => v is < 0 or >= 4 ? "expected range 0–3" : null);
        var equationOffsets = GetArrayField<int>(comp, "_equationOffsets").Get(expectedLength: 4);

        var colorNames = new[] { "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow" };

        var valD = Array.IndexOf(suitsGoal, 3) + 1; // 1–4
        var valE = jumps[0];    // 0–3
        var valF = jumps[1];    // 0–3
        var valG = jumps[2];    // 0–3
        var valH = jumps[3];    // 0–3
        var valM = equationOffsets[3];  // 1–9
        var valN = colorStageColors.Length; // 4–9
        var valP = suitsGoal.Where(s => s != 3).Select(s => "♠♥♣"[s]).JoinString(); // permutation of ♠♥♣
        var valQ = colorStageColors[3]; // 0–5, color
        var valX = equationOffsets[2];  // 1–5

        yield return new Discriminator(SBlueButton.DOther, "D", valD, ["D", valD.ToString()]);
        yield return new Discriminator(SBlueButton.DOther, "E", valE, ["E", valE.ToString()]);
        yield return new Discriminator(SBlueButton.DOther, "F", valF, ["F", valF.ToString()]);
        yield return new Discriminator(SBlueButton.DOther, "G", valG, ["G", valG.ToString()]);
        yield return new Discriminator(SBlueButton.DOther, "H", valH, ["H", valH.ToString()]);
        yield return new Discriminator(SBlueButton.DOther, "M", valM, ["M", valM.ToString()]);
        yield return new Discriminator(SBlueButton.DOther, "N", valN, ["N", valN.ToString()]);
        yield return new Discriminator(SBlueButton.DOther, "P", valP, ["P", valP]);
        yield return new Discriminator(SBlueButton.DQ, "Q", valQ, [colorNames[valQ]]);
        yield return new Discriminator(SBlueButton.DOther, "X", valX, ["X", valX.ToString()]);

        yield return WaitForSolve;

        yield return question(SBlueButton.EFGH, args: ["E"]).AvoidDiscriminators("E").Answers(valE.ToString());
        yield return question(SBlueButton.EFGH, args: ["F"]).AvoidDiscriminators("F").Answers(valF.ToString());
        yield return question(SBlueButton.EFGH, args: ["G"]).AvoidDiscriminators("G").Answers(valG.ToString());
        yield return question(SBlueButton.EFGH, args: ["H"]).AvoidDiscriminators("H").Answers(valH.ToString());
        yield return question(SBlueButton.D).AvoidDiscriminators("D").Answers(valD.ToString());
        yield return question(SBlueButton.M).AvoidDiscriminators("M").Answers(valM.ToString());
        yield return question(SBlueButton.N).AvoidDiscriminators("N").Answers(valN.ToString());
        yield return question(SBlueButton.P).AvoidDiscriminators("P").Answers(valP);
        yield return question(SBlueButton.Q).AvoidDiscriminators("Q").Answers(colorNames[valQ]);
        yield return question(SBlueButton.X).AvoidDiscriminators("X").Answers(valX.ToString());
    }
}

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
    X
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
        var valQ = colorStageColors[3]; // 0–5; corresponds to a color in ‘colorNames’ above
        var valX = equationOffsets[2];  // 1–5

        _blueButtonInfos.Add((valD, valE, valF, valG, valH, valM, valN, valP, valQ, valX));

        yield return WaitForSolve;

        var candidateDiscriminators = new List<(string format, string name)> { (null, null) };
        void addCandidateDiscriminator<T>(Func<(int d, int e, int f, int g, int h, int m, int n, string p, int q, int x), T> getter, string name, T value, string valueReadable)
        {
            if (_blueButtonInfos.Count(tup => getter(tup).Equals(value)) == 1)
                candidateDiscriminators.Add((string.Format(translateString(Question.BlueButtonD, "the Blue Button where {0} was {1}"), name, valueReadable), name));
        }
        addCandidateDiscriminator(tup => tup.d, "D", valD, valD.ToString());
        addCandidateDiscriminator(tup => tup.e, "E", valE, valE.ToString());
        addCandidateDiscriminator(tup => tup.f, "F", valF, valF.ToString());
        addCandidateDiscriminator(tup => tup.g, "G", valG, valG.ToString());
        addCandidateDiscriminator(tup => tup.h, "H", valH, valH.ToString());
        addCandidateDiscriminator(tup => tup.m, "M", valM, valM.ToString());
        addCandidateDiscriminator(tup => tup.n, "N", valN, valN.ToString());
        addCandidateDiscriminator(tup => tup.p, "P", valP, valP);
        addCandidateDiscriminator(tup => tup.q, "Q", valQ, translateString(Question.BlueButtonD, colorNames[valQ]));
        addCandidateDiscriminator(tup => tup.x, "X", valX, valX.ToString());

        QandA makeQ(Question q, string forbiddenDiscriminator, string correctAnswer, string[] formatArgs = null) =>
            makeQuestion(q, module, formatArgs: formatArgs, correctAnswers: new[] { correctAnswer },
                formattedModuleName: candidateDiscriminators.Where(tup => tup.name != forbiddenDiscriminator).PickRandom().format);

        addQuestions(module,
            makeQ(Question.BlueButtonEFGH, "E", valE.ToString(), formatArgs: new[] { "E" }),
            makeQ(Question.BlueButtonEFGH, "F", valF.ToString(), formatArgs: new[] { "F" }),
            makeQ(Question.BlueButtonEFGH, "G", valG.ToString(), formatArgs: new[] { "G" }),
            makeQ(Question.BlueButtonEFGH, "H", valH.ToString(), formatArgs: new[] { "H" }),
            makeQ(Question.BlueButtonD, "D", valD.ToString()),
            makeQ(Question.BlueButtonM, "M", valM.ToString()),
            makeQ(Question.BlueButtonN, "N", valN.ToString()),
            makeQ(Question.BlueButtonP, "P", valP),
            makeQ(Question.BlueButtonQ, "Q", colorNames[valQ]),
            makeQ(Question.BlueButtonX, "X", valX.ToString()));
    }
}

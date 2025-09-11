using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SOctadecayotton
{
    [SouvenirQuestion("What was the starting sphere in {0}?", OneColumn4Answers, ExampleAnswers = ["--+", "-+-+-++-+", "-++-+--+-", "+++-+-++-", "--++-++-+-++"])]
    Sphere,
    
    [SouvenirQuestion("What was one of the subrotations in the {1} rotation in {0}?", OneColumn4Answers, ExampleAnswers = ["-X", "+Y-Z", "+U+V+W", "-R+S+T-O", "+P-Q-X+Y-Z"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Rotations
}

public partial class SouvenirModule
{
    [SouvenirHandler("TheOctadecayotton", "Octadecayotton", typeof(SOctadecayotton), "Emik", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessOctadecayotton(ModuleData module)
    {
        var comp = GetComponent(module, "TheOctadecayottonScript");
        yield return WaitForSolve;

        var interact = GetField<object>(comp, "Interact", isPublic: true).Get();
        var dimension = GetProperty<int>(interact, "Dimension").Get();
        var sphere = GetField<string>(comp, "souvenirSphere").Get().Where(c => c is '-' or '+').JoinString();
        var rotations = GetField<string>(comp, "souvenirRotations").Get().Split('&').ToArray();

        string[] wrongPositions;
        string toPosition(int i) => Convert.ToString(i, 2).Select(s => s == '0' ? '-' : '+').JoinString().PadLeft(dimension, '-');

        if (dimension <= 9)
            wrongPositions = Enumerable.Range(0, (int) Math.Pow(2, dimension)).Select(toPosition).ToArray();
        else
        {
            wrongPositions = new string[10];
            for (var i = 0; i < wrongPositions.Length; i++)
                do { wrongPositions[i] = toPosition(Rnd.Range(0, (int) Math.Pow(2, dimension))); }
                while (wrongPositions.Take(i - 1).Contains(wrongPositions[i]));
        }
        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.OctadecayottonSphere, module, correctAnswers: new[] { sphere }, preferredWrongAnswers: wrongPositions));
        for (var i = 0; i < rotations.Length; i++)
            qs.Add(makeQuestion(Question.OctadecayottonRotations, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: rotations[i].Split(',').Select(s => s.Trim()).ToArray(), preferredWrongAnswers: Enumerable.Range(1, 10).Select(n => new[] { "X", "Y", "Z", "W", "U", "V", "R", "S", "T", "O", "P", "Q", "L", "M", "M", "I", "J", "K", "F", "G", "H", "C", "D", "E", "A", "B", "XX" }.Take(dimension).ToArray().Shuffle().Take(Rnd.Range(1, Math.Min(6, dimension + 1))).Select(c => (Rnd.Range(0, 1f) > 0.5 ? "+" : "-") + c).JoinString()).ToArray()));
        addQuestions(module, qs);
    }
}
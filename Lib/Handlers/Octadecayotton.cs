using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SOctadecayotton
{
    [Question("What were the positive axes of the starting sphere in {0}?", OneColumn4Answers, ExampleAnswers = ["none", "X,Z,U", "Y,R,S,T", "X", "Y,U,R", "W,V"], TranslatableStrings = ["none"])]
    Sphere,

    [Question("What was one of the subrotations in the {1} rotation in {0}?", OneColumn4Answers, ExampleAnswers = ["-X", "+Y-Z", "+U+V+W", "-R+S+T-O", "+P-Q-X+Y-Z"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Rotations
}

public partial class SouvenirModule
{
    [Handler("TheOctadecayotton", "Octadecayotton", typeof(SOctadecayotton), "Emik", AddThe = true)]
    [ManualQuestion("What was the starting sphere?")]
    [ManualQuestion("What were the subtransformations in each transformation?")]
    private IEnumerator<SouvenirInstruction> ProcessOctadecayotton(ModuleData module)
    {
        var comp = GetComponent(module, "TheOctadecayottonScript");
        yield return WaitForSolve;

        var interact = GetField<object>(comp, "Interact", isPublic: true).Get();
        var dimension = GetProperty<int>(interact, "Dimension").Get();
        var sphere = GetField<string>(comp, "souvenirSphere").Get().Where(c => c is '-' or '+').JoinString();
        var rotations = GetField<string>(comp, "souvenirRotations").Get().Split('&').ToArray();

        var dimensionNames = new[] { "X", "Y", "Z", "W", "V", "U", "R", "S", "T", "O", "P", "Q", "L", "M", "N", "I", "J", "K", "F", "G", "H", "C", "D", "E", "A", "B", "XX" };

        string appendAxis(char sign, int pos, int charsFound)
        {
            var str = charsFound > 0 ? "," : "";
            return sign == '+' ? str + dimensionNames[pos] : "";
        }

        string convertAxes(string str)
        {
            var foundAxes = "";

            for (var i = 0; i < dimension; i++)
                foundAxes += appendAxis(str[i], i, foundAxes.Length);

            return foundAxes == "" ? TranslateQuestionString(SOctadecayotton.Sphere, "none") : foundAxes;
        }

        string correctAxis = convertAxes(sphere);

        string[] wrongAxes;
        string toPosition(int i) => Convert.ToString(i, 2).Select(s => s == '0' ? '-' : '+').JoinString().PadLeft(dimension, '-');

        if (dimension <= 9)
        {
            wrongAxes = Enumerable.Range(0, (int) Math.Pow(2, dimension)).Select(toPosition).ToArray();
            wrongAxes = wrongAxes.Select(x => convertAxes(x)).ToArray();
        }

        else
        {
            wrongAxes = new string[10];
            for (var i = 0; i < wrongAxes.Length; i++)
                do { 
                    wrongAxes[i] = toPosition(Rnd.Range(0, (int) Math.Pow(2, dimension)));
                    wrongAxes[i] = convertAxes(wrongAxes[i]);
                }
                while (wrongAxes.Take(i - 1).Contains(wrongAxes[i]));
        }

        yield return question(SOctadecayotton.Sphere).Answers(correctAxis, preferredWrong: wrongAxes);

        if (rotations.Length > 1 || rotations[0] != "") // If there are no rotations, the variable has a list of length 1 with just "". This stops that question from being asked.
        {
            for (var i = 0; i < rotations.Length; i++)
                yield return question(SOctadecayotton.Rotations, args: [Ordinal(i + 1)]).Answers(rotations[i].Split(',').Select(s => s.Trim()).ToArray(), preferredWrong: Enumerable.Range(1, 10).Select(n => dimensionNames.Take(dimension).ToArray().Shuffle().Take(Rnd.Range(1, Math.Min(6, dimension + 1))).Select(c => (Rnd.Range(0, 1f) > 0.5 ? "+" : "-") + c).JoinString()).ToArray());
        }
    }
}

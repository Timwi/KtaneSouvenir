using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SDenialDisplays
{
    [SouvenirQuestion("What number was initially shown on display {1} in {0}?", ThreeColumns6Answers, ExampleAnswers = ["1", "22", "333", "4", "55", "666", "7", "88", "999"], Arguments = ["A", "B", "C", "D", "E"], ArgumentGroupSize = 1)]
    Displays
}

public partial class SouvenirModule
{
    [SouvenirHandler("DenialDisplaysModule", "Denial Displays", typeof(SDenialDisplays), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessDenialDisplays(ModuleData module)
    {
        var comp = GetComponent(module, "DenialDisplaysScript");
        yield return WaitForSolve;

        var initial = GetArrayField<int>(comp, "_initialDisplayNums").Get();

        var rands = new List<int>();
        for (var i = 0; i < 50; i++)
        {
            var r = Rnd.Range(0, 3);
            if (r == 0)
                rands.Add(Rnd.Range(0, 10));
            else if (r == 1)
                rands.Add(Rnd.Range(10, 100));
            else
                rands.Add(Rnd.Range(100, 1000));
        }
        for (var disp = 0; disp < 5; disp++)
            yield return question(SDenialDisplays.Displays, args: ["ABCDE"[disp].ToString()]).Answers(initial[disp].ToString(), preferredWrong: rands.Select(i => i.ToString()).ToArray());
    }
}

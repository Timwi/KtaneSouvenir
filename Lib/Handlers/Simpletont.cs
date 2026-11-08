using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SSimpletont
{
    [Question("Which of these binary numbers flashed in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings(6, "01")]
    Flashes
}

public partial class SouvenirModule
{
    [Handler("notsimple", "Simpleton’t", typeof(SSimpletont), "Infusion")]
    [ManualQuestion("What were the flashed binary numbers?")]
    private IEnumerator<SouvenirInstruction> ProcessSimpletont(ModuleData module)
    {
        var comp = GetComponent(module, "SimpntScript");
        var flashes = GetArrayField<int>(comp, "vals").Get(expectedLength: 9);

        var binaryNumbers = new string[9];

        for (int i = 0; i < 9; i++)
        {
            var flash = flashes[i];
            binaryNumbers[i] = (flash % 2).ToString();
            for (int j = 0; j < 5; j++)
            {
                flash /= 2;
                binaryNumbers[i] = (flash % 2).ToString() + binaryNumbers[i];
            }
        }

        yield return WaitForSolve;

        for (var i = 0; i < 9; i++)
            yield return question(SSimpletont.Flashes).Answers(binaryNumbers[i]);
    }
}

using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SSimpletont
{
    [Question("Which of these binary numbers flashed in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings(6, "01")]
    Flashes,

    [Discriminator("the Simpleton’t that also flashed {0}", Arguments = ["000000", "001101", "110110"], ArgumentGroupSize = 1)]
    Discriminator
}

public partial class SouvenirModule
{
    [Handler("notsimple", "Simpleton’t", typeof(SSimpletont), "Infusion")]
    [ManualQuestion("What were the flashed binary numbers?")]
    private IEnumerator<SouvenirInstruction> ProcessSimpletont(ModuleData module)
    {
        var comp = GetComponent(module, "SimpntScript");
        var flashes = GetArrayField<int>(comp, "vals").Get(expectedLength: 9, validator: v => v < 0 || v > 63 ? $"out of range; expected 0–63" : null);
        var binaryNumbers = flashes.Select(val => Enumerable.Range(0, 6).Select(j => (val & (1 << (5 - j))) != 0 ? '1' : '0').JoinString()).ToArray();

        yield return WaitForSolve;

        yield return question(SSimpletont.Flashes).Answers(binaryNumbers);

        foreach (var bin in binaryNumbers.Distinct())
            yield return new Discriminator(SSimpletont.Discriminator, $"flashed-{bin}", args: [bin], avoidAnswers: [bin]);
    }
}

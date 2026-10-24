using System.Collections;
using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SFaultyAccelerando
{
    [Question("Which number was paired with the letter {1} in {0}?", ThreeColumns6Answers, Arguments = ["A", "B", "C", "D", "E", "F"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 20)]
    QNumbersWithLetters,

    [Question("Which letter was paired with the number {1} in {0}?", ThreeColumns6Answers, Arguments = ["1", "2", "3", "4", "5", "6"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    QLettersWithNumbers,

    [Discriminator("the Faulty Accelerando where the number {0} was paired with the letter {1}", Arguments = ["1", "A", "2", "B", "3", "C"], ArgumentGroupSize = 2)]
    DNumberLetterPairs
}

public partial class SouvenirModule
{
    [Handler("faultyAccelerandoModule", "Faulty Accelerando", typeof(SFaultyAccelerando), "Infusion")]
    [ManualQuestion("Which numbers and letters were paired with each other?")]

    private IEnumerator<SouvenirInstruction> ProcessFaultyAccelerando(ModuleData module)
    {
        var comp = GetComponent(module, "FaultyAccelerandoScript");
        var fldPairs = GetField<IList>(comp, "pairs");

        yield return WaitForSolve;

        var pairs = fldPairs.Get(validator: v => v.Count != 20 ? "expected length 20" : null);
        var fldNumber = GetField<int>(pairs[0], "Number", isPublic: true);
        var fldLetter = GetField<char>(pairs[0], "Letter", isPublic: true);

        for (var i = 0; i < 20; i++)
        {
            var number = fldNumber.GetFrom(pairs[i], validator: i => i < 1 || i > 20 ? "expected range 1–20" : null).ToString();
            var letter = fldLetter.GetFrom(pairs[i], validator: ch => ch < 'A' || ch > 'Z' ? "expected letter A–Z" : null).ToString();
            yield return new Discriminator(SFaultyAccelerando.DNumberLetterPairs, $"pair{i}", pairs[i], args: [number, letter]);
            yield return question(SFaultyAccelerando.QNumbersWithLetters, args: [letter]).AvoidDiscriminators($"pair{i}").Answers(number);
            yield return question(SFaultyAccelerando.QLettersWithNumbers, args: [number]).AvoidDiscriminators($"pair{i}").Answers(letter);
        }
    }
}

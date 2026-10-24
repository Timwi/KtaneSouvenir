using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SAccelerando
{
	[Question("Which number was paired with the letter {1} in {0}?", ThreeColumns6Answers, Arguments = ["A", "B", "C", "D", "E", "F"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 20)]
    QNumbersWithLetters,

    [Question("Which letter was paired with the number {1} in {0}?", ThreeColumns6Answers, Arguments = ["1", "2", "3", "4", "5", "6"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    QLettersWithNumbers,

    [Discriminator("the Accelerando where the number {0} was paired with the letter {1}", Arguments = ["1", "A", "2", "B", "3", "C"], ArgumentGroupSize = 2)]
    DNumberLetterPairs
}

public partial class SouvenirModule
{
	[Handler("accelerando", "Accelerando", typeof(SAccelerando), "Infusion")]
	[ManualQuestion("Which numbers and letters were paired with each other?")]

	private IEnumerator<SouvenirInstruction> ProcessAccelerando(ModuleData module)
    {
        var comp = GetComponent(module, "AccelerandoScript");
        var fldPairs = comp.GetType().GetField("pairs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        System.Collections.IList pairs = (System.Collections.IList)fldPairs.GetValue(comp);

        yield return WaitForSolve;

        for (var i = 0; i < 20; i++)
        {
            var pair = pairs[i];
            var type = pair.GetType();
            int number = (int)type.GetField("Number").GetValue(pair);
            char letter = (char)type.GetField("Letter").GetValue(pair);
            yield return new Discriminator(SAccelerando.DNumberLetterPairs, $"pair{i}", pairs[i], args: [number.ToString(), letter.ToString()]);
            yield return question(SAccelerando.QNumbersWithLetters, args: [letter.ToString()]).AvoidDiscriminators($"pair{i}").Answers(number.ToString());
            yield return question(SAccelerando.QLettersWithNumbers, args: [number.ToString()]).AvoidDiscriminators($"pair{i}").Answers(letter.ToString());
        }
    }
}
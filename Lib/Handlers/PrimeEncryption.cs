using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPrimeEncryption
{
    [SouvenirQuestion("What was the number shown in {0}?", ThreeColumns6Answers, ExampleAnswers = ["1147", "1271", "1333", "1457", "1643", "1829"])]
    DisplayedValue
}

public partial class SouvenirModule
{
    [SouvenirHandler("primeEncryption", "Prime Encryption", typeof(SPrimeEncryption), "VFlyer")]
    private IEnumerator<SouvenirInstruction> ProcessPrimeEncryption(ModuleData module)
    {
        var comp = GetComponent(module, "PrimeEncryptionScript");
        yield return WaitForSolve;

        var displayedValue = GetField<int>(comp, "encryption").Get();
        var allPrimeNumbersUsed = GetArrayField<int>(comp, "primeNumbers").Get();

        // Generate wrong answers based on a combination of prime numbers determined from the module.
        var incorrectValues = new List<int>();
        while (incorrectValues.Count < 5)
        {
            var onePrime = allPrimeNumbersUsed.PickRandom();
            var anotherPrime = allPrimeNumbersUsed.PickRandom();
            while (anotherPrime == onePrime)
                anotherPrime = allPrimeNumbersUsed.PickRandom();

            var productPrimes = onePrime * anotherPrime;
            if (productPrimes != displayedValue && !incorrectValues.Contains(productPrimes))
                incorrectValues.Add(productPrimes);
        }

        yield return question(SPrimeEncryption.DisplayedValue).Answers(displayedValue.ToString(), preferredWrong: incorrectValues.Select(val => val.ToString()).ToArray());
    }
}
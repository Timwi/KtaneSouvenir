using System.Collections.Generic;
using Souvenir;

public enum SWireAssociation
{
    [Question("Which letter was wire {1} in {0}?", AnswerLayout.ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", Arguments = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16"], ArgumentGroupSize = 1)]
    NumberToLetter,

    [Question("Which number was wire {1} in {0}?", AnswerLayout.ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", Arguments = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P"], ArgumentGroupSize = 1)]
    LetterToNumber,

    [Discriminator("the Wire Association in which wire {0} was {1}", Arguments = ["A", "1", "B", "2", "C", "3", "D", "4", "E", "5", "F", "6", "G", "7", "H", "8", "I", "9", "J", "10", "K", "11", "L", "12", "M", "13", "N", "14", "O", "15", "P", "16"], ArgumentGroupSize = 2)]
    Discriminator
}

public partial class SouvenirModule
{
    [Handler("WireAssociationModule", "Wire Association", typeof(SWireAssociation), "Timwi")]
    [ManualQuestion("Which letter was associated with which number?")]
    private IEnumerator<SouvenirInstruction> ProcessWireAssociation(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "WireAssociationModule");
        var assoc = GetArrayField<int>(comp, "_assoc").Get(minLength: 11, maxLength: 16);
        for (var ix = 0; ix < assoc.Length; ix++)
            if (assoc[ix] < 0 || assoc[ix] >= assoc.Length)
                throw new AbandonModuleException($"_assoc[{ix}] = {assoc[ix]}, expected range 0–{assoc.Length - 1}");

        for (var n = 0; n < assoc.Length; n++)
        {
            var ltr = ((char) ('A' + assoc[n])).ToString();
            var num = (n + 1).ToString();
            yield return question(SWireAssociation.NumberToLetter, args: [num]).Answers(ltr);
            yield return question(SWireAssociation.LetterToNumber, args: [ltr]).Answers(num);
            yield return new Discriminator(SWireAssociation.Discriminator, ltr, n, args: [ltr, num], avoidAnswers: [ltr, num]);
        }
    }
}

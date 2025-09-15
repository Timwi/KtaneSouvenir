using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SIceCream
{
    [SouvenirQuestion("Which one of these flavours {1} to the {2} customer in {0}?", OneColumn4Answers, "Tutti Frutti", "Rocky Road", "Raspberry Ripple", "Double Chocolate", "Double Strawberry", "Cookies & Cream", "Neapolitan", "Mint Chocolate Chip", "The Classic", "Vanilla", TranslateArguments = [true, false], Arguments = ["was on offer, but not sold,", QandA.Ordinal, "was not on offer", QandA.Ordinal], ArgumentGroupSize = 2)]
    Flavour,

    [SouvenirQuestion("Who was the {1} customer in {0}?", ThreeColumns6Answers, "Mike", "Tim", "Tom", "Dave", "Adam", "Cheryl", "Sean", "Ashley", "Jessica", "Taylor", "Simon", "Sally", "Jade", "Sam", "Gary", "Victor", "George", "Jacob", "Pat", "Bob", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Customer
}

public partial class SouvenirModule
{
    [SouvenirHandler("iceCreamModule", "Ice Cream", typeof(SIceCream), "CaitSith2")]
    private IEnumerator<SouvenirInstruction> ProcessIceCream(ModuleData module)
    {
        var comp = GetComponent(module, "IceCreamModule");
        var fldCurrentStage = GetIntField(comp, "CurrentStage");
        var fldCustomers = GetArrayField<int>(comp, "CustomerNamesSolution");
        var fldSolution = GetArrayField<int>(comp, "Solution");
        var fldFlavourOptions = GetArrayField<int[]>(comp, "FlavorOptions");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var flavourNames = Question.IceCreamFlavour.GetAnswers();
        var customerNames = Question.IceCreamCustomer.GetAnswers();

        var flavours = new int[3][];
        var solution = new int[3];
        var customers = new int[3];

        for (var i = 0; i < 3; i++)
        {
            while (fldCurrentStage.Get() == i)
                yield return new WaitForSeconds(.1f);
            if (fldCurrentStage.Get() < i)
                throw new AbandonModuleException($"The stage number went down from {i} to {fldCurrentStage.Get()}.");

            var options = fldFlavourOptions.Get(expectedLength: 3, validator: x => x.Length != 5 ? "expected length 5" : x.Any(y => y < 0 || y >= flavourNames.Length) ? $"expected range 0–{flavourNames.Length - 1}" : null);
            var sol = fldSolution.Get(ar => ar.Any(x => x < 0 || x >= flavourNames.Length) ? $"expected range 0–{flavourNames.Length - 1}" : null);
            var cus = fldCustomers.Get(ar => ar.Any(x => x < 0 || x >= customerNames.Length) ? $"expected range 0–{customerNames.Length - 1}" : null);

            flavours[i] = options[i].ToArray();
            solution[i] = flavours[i][sol[i]];
            customers[i] = cus[i];
        }
        yield return WaitForSolve;

        for (var i = 0; i < 3; i++)
        {
            yield return question(SIceCream.Flavour, args: ["was on offer, but not sold,", Ordinal(i + 1)]).Answers(flavours[i].Where(ix => ix != solution[i]).Select(ix => flavourNames[ix]).ToArray());
            yield return question(SIceCream.Flavour, args: ["was not on offer", Ordinal(i + 1)]).Answers(flavourNames.Where((f, ix) => !flavours[i].Contains(ix)).ToArray());
            if (i != 2)
                yield return question(SIceCream.Customer, args: [Ordinal(i + 1)]).Answers(customerNames[customers[i]], preferredWrong: customers.Select(ix => customerNames[ix]).ToArray());
        }
    }
}
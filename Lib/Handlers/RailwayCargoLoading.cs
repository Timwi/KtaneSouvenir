using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SRailwayCargoLoading
{
    [SouvenirQuestion("What was the {1} car in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Cars,

    [SouvenirQuestion("Which freight table rule {1} in {0}?", OneColumn4Answers, "Over 150 lumber/75 logs", "Over 100 sheet metal", "Over 250 crude oil", "Over 400 mail", "Over 30 livestock", "Over 600 milk/water/resin", "Over 100 liquid fuel", "Over 700 industrial gas", "Over 150 food", "Over 100 coal", "Over 500 loose bulk (excl. coal)", "Over 7 large objects", "Over 5 automobiles", "Over 700 industrial gas", Arguments = ["was met", "wasn’t met"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    FreightTableRules
}

public partial class SouvenirModule
{
    [SouvenirHandler("RailwayCargoLoading", "Railway Cargo Loading", typeof(SRailwayCargoLoading), "LotsOfS")]
    private IEnumerator<SouvenirInstruction> ProcessRailwayCargoLoading(ModuleData module)
    {
        var comp = GetComponent(module, "TrainLoading");
        yield return WaitForSolve;

        // We need to take a copy of the sprites from the module in order to change the ‘pixelsPerUnit’ and ‘pivot’ properties.
        var allTrainCars = GetField<Array>(comp, "_trainCars").Get();
        var fldCarAppearance = GetField<Sprite>(allTrainCars.GetValue(0), "Appearance", isPublic: true);
        var fldCarFriendlyName = GetField<string>(allTrainCars.GetValue(0), "FriendlyName", isPublic: true);
        var carSpriteDic = allTrainCars.Cast<object>()
            .Select(car => (sprite: fldCarAppearance.GetFrom(car), name: fldCarFriendlyName.GetFrom(car)))
            .ToDictionary(tup => tup.sprite, tup => tup.sprite.TranslateSprite(420, tup.name));
        var allCarSprites = carSpriteDic.Values.ToArray();

        var trainCars = GetField<Array>(comp, "_train")
            .Get(ar => ar.Length != 15 ? "expected length 15" : null)
            .Cast<object>()
            .Select(car => carSpriteDic[fldCarAppearance.GetFrom(car)])
            .ToArray();

        // Ask about the correctly connected cars/locomotives
        for (var i = 0; i < 14; i++)    // skip 15 because it’s always the Caboose
            yield return question(SRailwayCargoLoading.Cars, args: [Ordinal(i + 1)]).Answers(trainCars[i], preferredWrong: allCarSprites);

        // Ask about the met or unmet freight table rules
        var freightTableRules = GetField<Array>(comp, "_freightTable").Get(ar => ar.Length != 14 ? "expected length 14" : null);
        var fldTableRuleMet = GetIntField(freightTableRules.GetValue(0), "_metAtStage", isPublic: false);
        var fldTableRuleResource = GetField<object>(freightTableRules.GetValue(0), "_resource", isPublic: false);
        var fldTableRuleResourceName = GetField<string>(fldTableRuleResource.Get(), "DisplayName", isPublic: true);

        var metRules = new List<string>();
        var unmetRules = new List<string>();

        for (var i = 0; i < 14; i++)
        {
            var ruleResource = fldTableRuleResource.GetFrom(freightTableRules.GetValue(i));
            var ruleName = fldTableRuleResourceName.GetFrom(ruleResource) switch
            {
                "Sulfuric Acid" or "Nitric Acid" => "Over 700 industrial gas",
                "Automobiles" => "Over 5 automobiles",
                "Farming Equipment" or "Military Hardware" or "Wings" => "Over 7 large objects",
                "Grain" or "Sand" or "Clay" or "Cement" or "Iron Ore" or "Gold Ore" => "Over 500 loose bulk (excl. coal)",
                "Coal" => "Over 100 coal",
                "Meat" or "Vegetables" or "Fruit" => "Over 150 food",
                "Helium" or "Argon" or "Nitrogen" or "Acetylene" => "Over 700 industrial gas",
                "Kerosene" or "Gasoline" or "Diesel" => "Over 100 liquid fuel",
                "Milk" or "Water" or "Resin" => "Over 600 milk/water/resin",
                "Livestock" => "Over 30 livestock",
                "Mail" => "Over 400 mail",
                "Crude Oil" => "Over 250 crude oil",
                "Sheet Metal" => "Over 100 sheet metal",
                "Lumber" or "Logs" => "Over 150 lumber/75 logs",
                var invalid => throw new AbandonModuleException($"There was an invalid resource found for one of the freight table rules: {invalid}"),
            };
            (fldTableRuleMet.GetFrom(freightTableRules.GetValue(i)) < 15 ? metRules : unmetRules).Add(ruleName);
        }

        if (metRules.Count + unmetRules.Count != 14)
            throw new AbandonModuleException($"The total amount of freight table rules is not 14. Met: {metRules.Count}, unmet: {unmetRules.Count}");

        if (metRules.Count >= 1 && unmetRules.Count >= 3)
            yield return question(SRailwayCargoLoading.FreightTableRules, args: ["was met"]).Answers(metRules.ToArray(), preferredWrong: unmetRules.ToArray());
        if (unmetRules.Count >= 1 && metRules.Count >= 3)
            yield return question(SRailwayCargoLoading.FreightTableRules, args: ["wasn’t met"]).Answers(unmetRules.ToArray(), preferredWrong: metRules.ToArray());
    }
}
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SFlags
{
    [SouvenirQuestion("What was the displayed number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 7)]
    DisplayedNumber,
    
    [SouvenirQuestion("What was the main country flag in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "FlagsSprites")]
    MainCountry,
    
    [SouvenirQuestion("Which of these country flags was shown, but not the main country flag, in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "FlagsSprites")]
    Countries
}

public partial class SouvenirModule
{
    [SouvenirHandler("FlagsModule", "Flags", typeof(SFlags), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessFlags(ModuleData module)
    {
        var comp = GetComponent(module, "FlagsModule");
        var mainCountry = GetField<object>(comp, "mainCountry").Get();
        var countries = GetField<IList>(comp, "countries").Get(v => v.Count != 7 ? "expected length 7" : null);
        var number = GetIntField(comp, "number").Get(1, 7);

        var propCountryName = GetProperty<string>(mainCountry, "CountryName", isPublic: true);
        var mainCountryName = propCountryName.GetFrom(mainCountry);
        var mainCountrySprite = FlagsSprites.FirstOrDefault(spr => spr.name == mainCountryName) ?? throw new AbandonModuleException($"Country name “{mainCountryName}” (main country) has no corresponding sprite.");

        var otherCountrySprites = countries.Cast<object>()
            .Select(country => propCountryName.GetFrom(country))
            .Select((countryName, countryIx) => FlagsSprites.FirstOrDefault(spr => spr.name == countryName) ?? throw new AbandonModuleException($"Country name “{countryName}” (country #{countryIx}) has no corresponding sprite."))
            .ToArray();

        yield return WaitForSolve;

        addQuestions(module,
            // Displayed number
            makeQuestion(Question.FlagsDisplayedNumber, module, correctAnswers: new[] { number.ToString() }),
            // Main country flag
            makeQuestion(Question.FlagsMainCountry, module, correctAnswers: new[] { mainCountrySprite }, preferredWrongAnswers: otherCountrySprites),
            // Rest of the country flags
            makeQuestion(Question.FlagsCountries, module, correctAnswers: otherCountrySprites, preferredWrongAnswers: FlagsSprites));
    }
}
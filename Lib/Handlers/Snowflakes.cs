using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSnowflakes
{
    [SouvenirQuestion("Which snowflake was on the {1} button of {0}?", ThreeColumns6Answers, Type = AnswerType.SnowflakesFont, FontSize = 400, CharacterSize = 0.2f, Arguments = ["top", "right", "bottom", "left"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Strings("A-Za-z")]
    DisplayedSnowflakes
}

public partial class SouvenirModule
{
    [SouvenirHandler("snowflakes", "Snowflakes", typeof(SSnowflakes), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessSnowflakes(ModuleData module)
    {
        var comp = GetComponent(module, "snowflakes");

        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var gameOnPassDelegate = module.Module.OnPass;
        module.Module.OnPass = () => { return false; };

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        yield return new WaitForSeconds(5); // Wait for the snowflakes to disappear
        gameOnPassDelegate();

        var displays = GetArrayField<TextMesh>(comp, "displays", isPublic: true).Get(expectedLength: 4);
        var directions = new[] { "top", "right", "bottom", "left" };
        addQuestions(module, directions.Select((dir, ix) => makeQuestion(SSnowflakes.DisplayedSnowflakes, module, formatArgs: new[] { dir }, correctAnswers: new[] { displays[ix].text })));
    }
}
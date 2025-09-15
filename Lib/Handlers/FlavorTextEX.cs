using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SFlavorTextEX
{
    [SouvenirQuestion("Which module’s flavor text was shown in the {1} stage of {0}?", OneColumn4Answers, ExampleAnswers = ["Totally Accurate Minecraft Simulator", "Rock-Paper-Scissors-Lizard-Spock", "The Octadecayotton", "Power Button"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Module
}

public partial class SouvenirModule
{
    [SouvenirHandler("FlavorTextCruel", "Flavor Text EX", typeof(SFlavorTextEX), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessFlavorTextEX(ModuleData module)
    {
        var comp = GetComponent(module, "FlavorTextCruel");
        var fldStage = GetIntField(comp, "stage");
        var fldTextOption = GetField<object>(comp, "textOption");

        yield return WaitForActivate;

        var maxStageAmount = GetIntField(comp, "maxStageAmount").Get();
        var answers = new string[maxStageAmount];
        var fldName = GetField<string>(fldTextOption.Get(), "name", isPublic: true);

        while (fldStage.Get() < maxStageAmount)
        {
            answers[fldStage.Get()] = fldName.GetFrom(fldTextOption.Get());
            yield return null;
        }

        if (answers.Any(a => a == null))
            throw new AbandonModuleException($"Abandoning Flavor Text EX because Stage {Array.IndexOf(answers, null)} has a null entry.");

        var textOptionList = GetField<IList>(comp, "textOptions").Get();
        var moduleNames = Enumerable.Range(0, textOptionList.Count).Select(index => fldName.GetFrom(textOptionList[index])).ToArray();

        for (var i = 0; i < maxStageAmount; i++)
            yield return question(SFlavorTextEX.Module, args: [Ordinal(i + 1)]).Answers(answers[i], all: moduleNames, preferredWrong: answers);
    }
}
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSouvenir
{
    [SouvenirQuestion("What was the first module asked about in the other Souvenir on this bomb?", OneColumn4Answers, ExampleAnswers = ["Probing", "Microcontroller", "Third Base", "Kudosudoku", "Quintuples", "3D Tunnels", "Uncolored Squares", "Pattern Cube", "Synonyms", "The Moon", "Human Resources", "Algebra"])]
    FirstQuestion,

    [SouvenirDiscriminator("Souvenir")]
    NullDiscriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("SouvenirModule", "Souvenir", typeof(SSouvenir), "CaitSith2", IsBossModule = true)]
    private IEnumerator<SouvenirInstruction> ProcessSouvenir(ModuleData module)
    {
        var comp = module.Module.GetComponent<SouvenirModule>();
        if (comp == this)
        {
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        if (module.Info.NumModules > 2)
            yield return legitimatelyNoQuestion(module, "There are more than two Souvenir modules on this bomb.");

        // Prefer names of supported modules on the bomb other than Souvenir.
        var preferredWrongAnswers = new List<string>();
        var allAnswers = new List<string>();
        var modulesOnTheBomb = _supportedModuleNames.Where(s => s != "Souvenir").Select(m => m.Replace("'", "’"));
        foreach (var (name, trName) in Ut.ModuleHandlers.Values
            .Select(h => (h.ModuleNameWithThe, _translation?.TranslateModule(h.EnumType)?.ModuleName ?? h.ModuleNameWithThe))
            .Distinct())
        {
            allAnswers.Add(trName);
            if (modulesOnTheBomb.Contains(name.Replace("\u00a0", " ")))
                preferredWrongAnswers.Add(trName);
        }

        while (comp._currentQuestion == null)
            yield return new WaitForSeconds(0.1f);

        var firstQuestion = comp._currentQuestion;
        var attr = firstQuestion.EnumValue.GetHandlerAttribute();
        var firstModule = _translation?.TranslateModule(attr.EnumType)?.ModuleName ?? attr.ModuleNameWithThe;

        // Wait for the user to solve that question before asking about it
        while (comp._currentQuestion == firstQuestion)
            yield return new WaitForSeconds(0.1f);

        yield return question(SSouvenir.FirstQuestion).Answers(firstModule, all: allAnswers.ToArray(), preferredWrong: preferredWrongAnswers.ToArray());
        yield return new Discriminator(SSouvenir.NullDiscriminator, "", module.Module);
    }
}

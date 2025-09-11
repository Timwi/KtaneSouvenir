using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSouvenir
{
    [SouvenirQuestion("What was the first module asked about in the other Souvenir on this bomb?", OneColumn4Answers, ExampleAnswers = ["Probing", "Microcontroller", "Third Base", "Kudosudoku", "Quintuples", "3D Tunnels", "Uncolored Squares", "Pattern Cube", "Synonyms", "The Moon", "Human Resources", "Algebra"])]
    FirstQuestion
}

public partial class SouvenirModule
{
    [SouvenirHandler("SouvenirModule", "Souvenir", typeof(SSouvenir), "CaitSith2")]
    private IEnumerator<SouvenirInstruction> ProcessSouvenir(ModuleData module)
    {
        var comp = module.Module.GetComponent<SouvenirModule>();
        const string moduleId = "SouvenirModule";
        if (comp == this)
        {
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        if (!_moduleCounts.TryGetValue(moduleId, out var souvenirCount) || souvenirCount != 2)
        {
            if (souvenirCount > 2)
                Debug.Log($"[Souvenir #{_moduleId}] There are more than two Souvenir modules on this bomb. Not asking any questions about them.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        // Prefer names of supported modules on the bomb other than Souvenir.
        var preferredWrongAnswers = new List<string>();
        var allAnswers = new List<string>();
        var modulesOnTheBomb = _supportedModuleNames.Where(s => s != "Souvenir").Select(m => m.Replace("'", "’"));
        foreach (var (name, trName) in Ut.Attributes.Select(a => (a.Value.ModuleNameWithThe, _translation?.Translate(a.Key)?.ModuleName ?? a.Value.ModuleNameWithThe)).Distinct())
        {
            allAnswers.Add(trName);
            if (modulesOnTheBomb.Contains(name.Replace("\u00a0", " ")))
                preferredWrongAnswers.Add(trName);
        }

        while (comp._currentQuestion == null)
            yield return new WaitForSeconds(0.1f);

        var firstQuestion = comp._currentQuestion;
        var firstModule = _translation?.Translate(firstQuestion.Question)?.ModuleName ?? firstQuestion.ModuleNameWithThe;

        // Wait for the user to solve that question before asking about it
        while (comp._currentQuestion == firstQuestion)
            yield return new WaitForSeconds(0.1f);

        module.SolveIndex = 1; // The question does not use the formatted module name. However, since the module may not be solved at this point, we need to specify a solve index anyways.
        addQuestion(module, Question.SouvenirFirstQuestion, correctAnswers: new[] { firstModule },
            preferredWrongAnswers: preferredWrongAnswers.ToArray(), allAnswers: allAnswers.ToArray());
    }
}
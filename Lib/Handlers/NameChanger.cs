using System.Collections.Generic;
using Souvenir;

public enum SNameChanger
{
    [Question("What was the starting word in {0}?", AnswerLayout.TwoColumns4Answers, "STEREOTYPE", "MICROPHONE", "PRESIDENCY", "ACCEPTANCE", "PRODUCTIVE", "ARTICULATE", "CONSTRAINT", "INVESTMENT", "CONCLUSION", "DISABILITY", "INHABITANT", "PSYCHOLOGY", "ACCOUNTANT", "DEFICIENCY", "ATTRACTIVE", "BASKETBALL", "EXPRESSION", "MASTERMIND", "ACCEPTABLE", "THOUGHTFUL", "TOURNAMENT", "POPULATION", "ASSUMPTION", "LITERATURE")]
    InitialWord
}

public partial class SouvenirModule
{
    [Handler("nameChanger", "Name Changer", typeof(SNameChanger), "Timwi")]
    [ManualQuestion("What was the starting word?")]
    private IEnumerator<SouvenirInstruction> ProcessNameChanger(ModuleData module)
    {
        var comp = GetComponent(module, "NameChanger");
        yield return WaitForSolve;
        var words = SNameChanger.InitialWord.GetAnswers();
        var startingWord = GetIntField(comp, "startingWord").Get(min: 0, max: words.Length - 1);
        yield return question(SNameChanger.InitialWord).Answers(words[startingWord]);
    }
}

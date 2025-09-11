using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWeakestLink
{
    [SouvenirQuestion("Who did you eliminate in {0}?", OneColumn4Answers, ExampleAnswers = ["Annie", "Albert", "Josephine", "Frederick"])]
    Elimination,
    
    [SouvenirQuestion("Who made it to the Money Phase with you in {0}?", OneColumn4Answers, ExampleAnswers = ["Annie", "Albert", "Josephine", "Frederick"])]
    MoneyPhaseName,
    
    [SouvenirQuestion("What was {1}’s skill in {0}?", OneColumn4Answers, "Geography", "Language", "Wildlife", "Biology", "Maths", "KTANE", "History", "Other", ExampleAnswers = ["KTANE", "Geography", "Language", "Wildlife"], Arguments = ["Annie", "Albert", "Josephine", "Frederick"], ArgumentGroupSize = 1)]
    Skill,
    
    [SouvenirQuestion("What ratio did {1} get in the Question Phase in {0}?", OneColumn4Answers, Arguments = ["Annie", "Albert", "Josephine", "Frederick"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("0-5", "/", "56")]
    Ratio
}

public partial class SouvenirModule
{
    [SouvenirHandler("TheWeakestLink", "Weakest Link", typeof(SWeakestLink), "Hawker", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessWeakestLink(ModuleData module)
    {
        var comp = GetComponent(module, "WeakestLink");
        yield return WaitForSolve;

        var contestantArr = GetArrayField<object>(comp, "contestants").Get(expectedLength: 3).Skip(1).ToArray();
        var fldCorrectAnswer = GetIntField(contestantArr[0], "CorrectAnswer", isPublic: true);
        var fldQuestionsAsked = GetIntField(contestantArr[0], "QuestionsAsked", isPublic: true);
        var fldCategory = GetField<Enum>(contestantArr[0], "Category", isPublic: true);
        var fldName = GetField<string>(contestantArr[0], "Name", isPublic: true);

        var ratioArr = new string[2];
        var names = new string[2];
        var skill = new Enum[2];

        for (var i = 0; i < 2; i++)
        {
            var person = contestantArr[i];
            skill[i] = fldCategory.GetFrom(person);
            ratioArr[i] = fldCorrectAnswer.GetFrom(person) + "/" + fldQuestionsAsked.GetFrom(person);
            names[i] = fldName.GetFrom(person);
        }

        var eliminatedPerson = GetField<object>(comp, "personToEliminate").Get();
        var eliminationPersonName = GetField<string>(eliminatedPerson, "Name").Get();
        var moneyPhaseName = eliminationPersonName == names[0] ? names[1] : names[0];
        var jsonReader = GetStaticField<object>(comp.GetType(), "jsonData").Get();
        var allNames = GetStaticProperty<List<string>>(jsonReader.GetType(), "ContestantNames", isPublic: true).Get().ToArray();

        addQuestions(module,
            makeQuestion(Question.WeakestLinkElimination, module, correctAnswers: new[] { eliminationPersonName }, preferredWrongAnswers: allNames),
            makeQuestion(Question.WeakestLinkMoneyPhaseName, module, correctAnswers: new[] { moneyPhaseName }, preferredWrongAnswers: allNames),
            makeQuestion(Question.WeakestLinkSkill, module, formatArgs: new[] { names[0] }, correctAnswers: new[] { skill[0].ToString() }),
            makeQuestion(Question.WeakestLinkSkill, module, formatArgs: new[] { names[1] }, correctAnswers: new[] { skill[1].ToString() }),
            makeQuestion(Question.WeakestLinkRatio, module, formatArgs: new[] { names[0] }, correctAnswers: new[] { ratioArr[0] }),
            makeQuestion(Question.WeakestLinkRatio, module, formatArgs: new[] { names[1] }, correctAnswers: new[] { ratioArr[1] })
        );
    }
}
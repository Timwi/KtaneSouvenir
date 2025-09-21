using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Rnd = UnityEngine.Random;

namespace Souvenir;

public abstract class AnswerStump
{
    public abstract AnswerSet GenerateAnswerSet(QuestionStump questionStump, SouvenirModule souvenir);
    protected abstract AnswerType[] acceptableTypes { get; }
}

public abstract class AnswerStump<T>(T[] correct, T[] preferredWrong, T[] all) : AnswerStump
{
    public T[] Correct { get; } = correct;
    public T[] PreferredWrong { get; } = preferredWrong;
    public T[] All { get; } = all;

    protected abstract AnswerSet MakeAnswerSet(T[] answers, int correctIndex, SouvenirQuestionAttribute qAttr, SouvenirModule souvenir);

    public override AnswerSet GenerateAnswerSet(QuestionStump questionStump, SouvenirModule souvenir)
    {
        var question = questionStump.EnumValue;
        if (question.GetQuestionAttribute() is not { } attr)
        {
            Debug.LogError($"[Souvenir #{souvenir._moduleId}] Abandoning {question.GetHandlerAttribute().ModuleNameWithThe} because: Question {question.GetType().Name}.{question} has no SouvenirQuestionAttribute.");
            return null;
        }
        if (!acceptableTypes.Contains(attr.Type))
        {
            Debug.LogError($"[Souvenir #{souvenir._moduleId}] Abandoning {question.GetHandlerAttribute().ModuleNameWithThe} because: The module handler for {questionStump.HandlerAttribute.ModuleName} attempted to generate Question {question.GetType().Name}.{question} (type={attr.Type}) but used the wrong answer type.");
            return null;
        }

        var allAnswersWasNull = All == null;
        var allAnswers = All ?? attr.AllAnswers as T[];
        if (allAnswers != null)
        {
            var inconsistency = Correct.Except(allAnswers).FirstOrDefault();
            if (inconsistency != null)
            {
                Debug.LogError($"[Souvenir #{souvenir._moduleId}] Abandoning {question.GetHandlerAttribute().ModuleNameWithThe} because: Question {question.GetType().Name}.{question}: invalid answer: {inconsistency}. allAnswers ({(allAnswersWasNull ? "was null" : "was not null")}): {allAnswers.Stringify()}; correctAnswers: {Correct.Stringify()}");
                return null;
            }
            if (PreferredWrong != null)
            {
                var inconsistency2 = PreferredWrong.Except(allAnswers).FirstOrDefault();
                if (inconsistency2 != null)
                {
                    Debug.LogError($"[Souvenir #{souvenir._moduleId}] Abandoning {question.GetHandlerAttribute().ModuleNameWithThe} because: Question {question.GetType().Name}.{question}: invalid preferred wrong answer: {inconsistency2}. allAnswers ({(allAnswersWasNull ? "was null" : "was not null")}): {allAnswers.Stringify()}");
                    return null;
                }
            }
        }

        var answers = new List<T>(attr.NumAnswers);
        if (allAnswers == null && attr.AnswerGenerators == null)
        {
            if (PreferredWrong == null || PreferredWrong.Length == 0)
            {
                Debug.LogError($"[Souvenir #{souvenir._moduleId}] Abandoning {question.GetHandlerAttribute().ModuleNameWithThe} because: Question {question.GetType().Name}.{question} has no answers. You must specify either the full set of possible answers in the attribute, use an AnswerGenerator, or provide answers through the preferredWrong or all parameters on .Answers().");
                return null;
            }
            answers.AddRange(PreferredWrong.Except(Correct).Distinct());
        }
        else
        {
            // Pick 𝑛−1 random wrong answers.
            if (allAnswers != null)
                answers.AddRange(allAnswers.Except(Correct));
            if (answers.Count <= attr.NumAnswers - 1)
            {
                if (attr.AnswerGenerators?.FirstOrDefault() is AnswerGeneratorAttribute<T>)
                    answers.AddRange(attr.AnswerGenerators.OfType<AnswerGeneratorAttribute<T>>().GetAnswers(souvenir).Except(answers.Concat(Correct)).Distinct().Take(attr.NumAnswers - 1 - answers.Count));
                if (answers.Count == 0 && (PreferredWrong == null || PreferredWrong.Length == 0))
                {
                    Debug.LogError($"[Souvenir #{souvenir._moduleId}] Abandoning {question.GetHandlerAttribute().ModuleNameWithThe} because: Question {question.GetType().Name}.{question}’s answer generator did not generate any answers.");
                    return null;
                }
            }
            else
            {
                answers.Shuffle();
                answers.RemoveRange(attr.NumAnswers - 1, answers.Count - (attr.NumAnswers - 1));
            }
            // Add the preferred wrong answers, if any. If we had added them earlier, they’d come up too rarely.
            if (PreferredWrong != null)
                answers.AddRange(PreferredWrong.Except(answers.Concat(Correct)).Distinct());
        }
        answers.Shuffle();
        if (answers.Count >= attr.NumAnswers)
            answers.RemoveRange(attr.NumAnswers - 1, answers.Count - (attr.NumAnswers - 1));

        var correctIndex = Rnd.Range(0, answers.Count + 1);
        answers.Insert(correctIndex, Correct.PickRandom());
        if (answers[0] is string && attr.TranslateAnswers)
            for (var i = 0; i < answers.Count; i++)
                answers[i] = (T) (object) souvenir.TranslateAnswer(question, (string) (object) answers[i]);
        return MakeAnswerSet(answers.ToArray(), correctIndex, attr, souvenir);
    }
}

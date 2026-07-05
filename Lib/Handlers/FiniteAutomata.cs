using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SFiniteAutomata
{
    [Question("What was the displayed expression for index {1} in {0}?", OneColumn4Answers, ExampleAnswers = ["(b|a|a(a*|b))*(aaab)*", "((b|ε)(b|a)b*|b|a)*", "(ε|a|b)b(ε|b|aba*)", "((b*|a)aa|b)a(aba|(ab)*)", "aaba*(b(a(a|b*))*)*"], Arguments = ["0", "1", "2", "3", "4"], ArgumentGroupSize = 1)]
    Expressions
}

public partial class SouvenirModule
{
    [Handler("FiniteAutomataModule", "Finite Automata", typeof(SFiniteAutomata), "Espik")]
    [ManualQuestion("What were the relevant regular expressions?")]
    private IEnumerator<SouvenirInstruction> ProcessFiniteAutomata(ModuleData module)
    {
        var comp = GetComponent(module, "FiniteAutomataModule");
        yield return WaitForSolve;

        var expressions = GetArrayField<string>(comp, "regex_strings", isPublic: true).Get(expectedLength: 5);
        var validExpressions = new HashSet<int>();

        var serialNumbers = Bomb.GetSerialNumberNumbers();

        if ((serialNumbers.Count() & 1) == 1) // Odd digits in serial
            validExpressions.Add(serialNumbers.ElementAt((serialNumbers.Count() - 1) / 2) % 5);

        else // Even digits in serial
        {
            validExpressions.Add(serialNumbers.First() % 5);
            validExpressions.Add(serialNumbers.Last() % 5);
        }

        foreach (int index in validExpressions)
            yield return question(SFiniteAutomata.Expressions, args: [index.ToString()]).Answers(expressions[index], all: expressions);
    }
}

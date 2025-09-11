using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGryphons
{
    [SouvenirQuestion("What was the gryphon’s name in {0}?", ThreeColumns6Answers, "Gabe", "Gabriel", "Gad", "Gael", "Gage", "Gaia", "Galena", "Galina", "Gallo", "Gallagher", "Ganymede", "Ganzorig", "Garen", "Gareth", "Garland", "Garnett", "Garret", "Garrick", "Gary", "Gaspar", "Gaston", "Gauthier", "Gavin", "Gaz", "Geena", "Geff", "Geffrey", "Gela", "Geltrude", "Gene", "Geneva", "Genevieve", "Geno", "Gentius", "Geoff", "George", "Georgio", "Georgius", "Gerald", "Geraldo", "Gerda", "Gerel", "Gergana", "Gerhardt", "Gerhart", "Gerry", "Gertrude", "Gervais", "Gervaise", "Ghada", "Ghadir", "Ghassan", "Ghjulia", "Gia", "Giada", "Giampaolo", "Giampiero", "Giancarlo", "Giana", "Gianna", "Gideon", "Gidon", "Gilbert", "Gilberta", "Gino", "Giorgio", "Giovanni", "Giove", "Girish", "Girisha", "Gisela", "Giselle", "Gittel", "Gizella", "Gjorgji", "Gladys", "Glauco", "Glaukos", "Glen", "Glenn", "Godfrey", "Godfried", "Gojko", "Gol", "Golda", "Gona", "Gonzalo", "Gordie", "Gordy", "Goretti", "Gosia", "Gosse", "Gotzon", "Gotzone", "Gowri", "Gozzo", "Grace", "Gracia", "Griffith", "Gwynnyth")]
    Name,
    
    [SouvenirQuestion("What was the gryphon’s age in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(23, 34)]
    Age
}

public partial class SouvenirModule
{
    [SouvenirHandler("gryphons", "Gryphons", typeof(SGryphons), "JerryEris")]
    private IEnumerator<SouvenirInstruction> ProcessGryphons(ModuleData module)
    {
        var comp = GetComponent(module, "Gryphons");
        yield return WaitForSolve;

        var age = GetIntField(comp, "age").Get(23, 34);
        var name = GetField<string>(comp, "theirName").Get();

        addQuestions(module,
            makeQuestion(Question.GryphonsName, module, correctAnswers: new[] { name }),
            makeQuestion(Question.GryphonsAge, module, correctAnswers: new[] { age.ToString() }, preferredWrongAnswers:
                Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(23, 34).ToString()).Distinct().Take(6).ToArray()));
    }
}
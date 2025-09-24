using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSpongebobBirthdayIdentification
{
    [SouvenirQuestion("Who was the {1} child displayed in {0}?", OneColumn4Answers, "Abela", "Aiden", "Allen", "Amber", "Apollo Yuojan", "Ashley", "Bobby", "Brayden", "Brendon", "Brent", "Bryce", "Caoimhe", "Carl Pobie", "Carlos Paolo", "Carson", "Chester Paul", "Christopher", "Cristian James Glavez", "Cyan Miguel", "Danny", "Dave", "Davian", "Donn Jeff Velionix Fijo", "Drew Justin", "Ethan", "Fabio", "Frame Baby", "Gabriel Felix", "Grayson", "Hayden", "Jacob", "Jaden", "Jake", "James", "Jayden", "Jeremiah", "Jon JonJon Eric Cabebe Jr.", "Juan Carlos", "Julian", "Junely Delos Reyes Jr.", "Kate Venus Valadores", "Ken Ivan", "Kenny Lee", "King Monic", "Kurt", "Landon", "Logan", "Lukas", "Makenly", "Mason", "Max", "Melvern Ryann", "Michael", "Miguel", "Myles A. Williams", "Neftali Xyler S. Ilao", "Noah", "Patrick", "Raymond", "Rhojus", "Sam Daniel", "Seth Laurence", "Shik", "Simon", "Sony Boy", "Spanky", "Spencer", "Stacey", "Steve Jr.", "Xander Chio E. Ceniza", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Children
}

public partial class SouvenirModule
{
    [SouvenirHandler("spongebobBirthdayIdentification", "Spongebob Birthday Identification", typeof(SSpongebobBirthdayIdentification), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessSpongebobBirthdayIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "SpongebobBirthdayIdentificationScript");
        var fldStage = GetIntField(comp, "stage");
        var fldAnswer = GetField<string>(comp, "answer");

        var answers = new List<string>();
        var currentStage = fldStage.Get();
        while (module.Unsolved)
        {
            var newStage = fldStage.Get();
            if (currentStage != newStage)
            {
                answers.Add(fldAnswer.Get());
                currentStage = newStage;
            }
            yield return null;
        }
        yield return new WaitForSeconds(.1f);

        var allNames = GetField<Texture[]>(comp, "allImages", isPublic: true).Get().Select(x => x.name).ToArray();
        for (var ix = 0; ix < answers.Count; ix++)
            yield return question(SSpongebobBirthdayIdentification.Children, args: [Ordinal(ix + 1)]).Answers(answers[ix], preferredWrong: allNames);
    }
}

using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SHumanResources
{
    [SouvenirQuestion("Which was a descriptor shown in {1} in {0}?", TwoColumns4Answers, "Intellectual", "Deviser", "Confidant", "Helper", "Auditor", "Innovator", "Defender", "Chameleon", "Director", "Designer", "Educator", "Advocate", "Manager", "Showman", "Contributor", "Entertainer", Arguments = ["red", "green"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    Descriptors,
    
    [SouvenirQuestion("Who was {1} in {0}?", ThreeColumns6Answers, "Rebecca", "Damian", "Jean", "Mike", "River", "Samuel", "Yoshi", "Caleb", "Ashley", "Tim", "Eliott", "Ursula", "Silas", "Noah", "Quinn", "Dylan", Arguments = ["fired", "hired"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    HiredFired
}

public partial class SouvenirModule
{
    [SouvenirHandler("HumanResourcesModule", "Human Resources", typeof(SHumanResources), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessHumanResources(ModuleData module)
    {
        var comp = GetComponent(module, "HumanResourcesModule");
        var people = GetStaticField<Array>(comp.GetType(), "_people").Get(ar => ar.Length != 16 ? "expected length 16" : null);
        var personToFire = GetIntField(comp, "_personToFire").Get(0, 15);
        var personToHire = GetIntField(comp, "_personToHire").Get(0, 15);

        var person = people.GetValue(0);
        var fldName = GetField<string>(person, "Name", isPublic: true);
        var fldDesc = GetField<string>(person, "Descriptor", isPublic: true);

        yield return WaitForSolve;

        var descs = GetArrayField<int>(comp, "_availableDescs").Get(expectedLength: 5);
        addQuestions(module,
            makeQuestion(Question.HumanResourcesDescriptors, module, formatArgs: new[] { "red" }, correctAnswers: descs.Take(3).Select(ix => fldDesc.GetFrom(people.GetValue(ix))).ToArray()),
            makeQuestion(Question.HumanResourcesDescriptors, module, formatArgs: new[] { "green" }, correctAnswers: descs.Skip(3).Select(ix => fldDesc.GetFrom(people.GetValue(ix))).ToArray()),
            makeQuestion(Question.HumanResourcesHiredFired, module, formatArgs: new[] { "fired" }, correctAnswers: new[] { fldName.GetFrom(people.GetValue(personToFire)) }),
            makeQuestion(Question.HumanResourcesHiredFired, module, formatArgs: new[] { "hired" }, correctAnswers: new[] { fldName.GetFrom(people.GetValue(personToHire)) }));
    }
}
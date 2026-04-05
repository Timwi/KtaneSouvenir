using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SHumanResources
{
    [Question("Which was a descriptor shown in {1} in {0}?", TwoColumns4Answers, "Intellectual", "Deviser", "Confidant", "Helper", "Auditor", "Innovator", "Defender", "Chameleon", "Director", "Designer", "Educator", "Advocate", "Manager", "Showman", "Contributor", "Entertainer", Arguments = ["red", "green"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Descriptors,

    [Question("Who was {1} at the start of {0}?", ThreeColumns6Answers, "Rebecca", "Damian", "Jean", "Mike", "River", "Samuel", "Yoshi", "Caleb", "Ashley", "Tim", "Eliott", "Ursula", "Silas", "Noah", "Quinn", "Dylan", Arguments = ["an employee", "an applicant"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Employees
}

public partial class SouvenirModule
{
    [Handler("HumanResourcesModule", "Human Resources", typeof(SHumanResources), "Timwi")]
    [ManualQuestion("Which employees and applicants were present?")]
    [ManualQuestion("Which descriptors were shown in red and green?")]
    private IEnumerator<SouvenirInstruction> ProcessHumanResources(ModuleData module)
    {
        var comp = GetComponent(module, "HumanResourcesModule");
        var people = GetStaticField<Array>(comp.GetType(), "_people").Get(ar => ar.Length != 16 ? "expected length 16" : null);

        var person = people.GetValue(0);
        var fldName = GetField<string>(person, "Name", isPublic: true);
        var fldDesc = GetField<string>(person, "Descriptor", isPublic: true);

        yield return WaitForSolve;

        var names = GetArrayField<int>(comp, "_availableNames").Get(expectedLength: 10);
        var descs = GetArrayField<int>(comp, "_availableDescs").Get(expectedLength: 5);

        yield return question(SHumanResources.Descriptors, args: ["red"]).Answers(descs.Take(3).Select(ix => fldDesc.GetFrom(people.GetValue(ix))).ToArray());
        yield return question(SHumanResources.Descriptors, args: ["green"]).Answers(descs.Skip(3).Select(ix => fldDesc.GetFrom(people.GetValue(ix))).ToArray());
        yield return question(SHumanResources.Employees, args: ["an employee"]).Answers(names.Take(5).Select(ix => fldName.GetFrom(people.GetValue(ix))).ToArray());
        yield return question(SHumanResources.Employees, args: ["an applicant"]).Answers(names.Skip(5).Select(ix => fldName.GetFrom(people.GetValue(ix))).ToArray());
    }
}

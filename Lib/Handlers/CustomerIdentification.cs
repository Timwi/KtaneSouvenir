using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCustomerIdentification
{
    [SouvenirQuestion("Who was the {1} customer in {0}?", OneColumn4Answers, "Akari", "Alberto", "Allan", "Amy", "Austin", "Bertha", "Big Pauly", "Boomer", "Boopsy & Bill", "Brody", "Bruna Romano", "C.J. Friskins", "Cameo", "Captain Cori", "Carlo Romano", "Cecilia", "Cherissa", "Chester", "Chuck", "Clair", "Cletus", "Clover", "Connor", "Cooper", "Crystal", "Daniela", "Deano", "Didar", "Doan", "Drakson", "Duke Gotcha", "Edna", "Elle", "Ember", "Emmlette", "Evelyn", "Fernanda", "Foodini", "Franco", "Georgito", "Gino Romano", "Greg", "Gremmie", "Hacky Zak", "Hank", "Hope", "Hugo", "Iggy", "Indigo", "Ivy", "James", "Janana", "Johnny", "Jojo", "Joy", "Julep", "Kahuna", "Kaleb", "Kasey O", "Kayla", "Kenji", "Kenton", "Kingsley", "Koilee", "LePete", "Liezel", "Lisa", "Little Edoardo", "Maggie", "Mandi", "Marty", "Mary", "Matt", "Mayor Mallow", "Mesa", "Mindy", "Mitch", "Moe", "Mousse", "Mr. Bombolony", "Nevada", "Nick", "Ninjoy", "Nye", "Okalani", "Olga", "Olivia", "Pally", "Papa Louie", "Peggy", "Penny", "Perri", "Petrona", "Pinch Hitwell", "Professor Fitz", "Prudence", "Quinn", "Radlynn", "Rhonda", "Rico", "Ripley", "Rita", "Robby", "Rollie", "Roy", "Rudy", "Santa", "Sarge Fan", "Sasha", "Scarlett", "Scooter", "Shannon", "Sienna", "Simone", "Skip", "Skyler", "Sprinks The Clown", "Steven", "Sue", "Taylor", "The Dynamoe", "Timm", "Tohru", "Tony", "Trishna", "Utah", "Vicky", "Vincent", "Wally", "Wendy", "Whiff", "Whippa", "Willow", "Wylan B", "Xandra", "Xolo", "Yippy", "Yui", "Zoe", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Customer
}

public partial class SouvenirModule
{
    [SouvenirHandler("xelCustomerIdentification", "Customer Identification", typeof(SCustomerIdentification), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessCustomerIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "CustomerIdentificationScript");

        yield return WaitForSolve;

        var seedPacketIdentifier = GetField<Sprite[]>(comp, "SeedPacketIdentifier", isPublic: true).Get();
        var unique = GetArrayField<int>(comp, "Unique").Get(expectedLength: 3);
        var answers = unique.Select(uq => seedPacketIdentifier[uq].name).ToArray();

        for (var i = 0; i < 3; i++)
            yield return question(SCustomerIdentification.Customer, args: [Ordinal(i + 1)]).Answers(answers[i]);
    }
}
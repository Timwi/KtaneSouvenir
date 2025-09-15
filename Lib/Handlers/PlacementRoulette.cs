using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPlacementRoulette
{
    [SouvenirQuestion("What was the character listed on the information display in {0}?", TwoColumns4Answers, "Baby Mario", "Baby Luigi", "Baby Peach", "Baby Daisy", "Toad", "Toadette", "Koopa Troopa", "Dry Bones", "Mario", "Luigi", "Peach", "Daisy", "Yoshi", "Birdo", "Diddy Kong", "Bowser Jr.", "Mii", "Wario", "Waluigi", "Donkey Kong", "Bowser", "King Boo", "Rosalina", "Funky Kong", "Dry Bowser")]
    Char,

    [SouvenirQuestion("What was the track listed on the information display in {0}?", OneColumn4Answers, "Luigi Circuit", "Moo Moo Meadows", "Mushroom Gorge", "Toad's Factory", "Mario Circuit", "Coconut Mall", "DK Snowboard Cross", "Wario's Gold Mine", "Daisy Circuit", "Koopa Cape", "Maple Treeway", "Grumble Volcano", "Dry Dry Ruins", "Moonview Highway", "Bowser's Castle", "Rainbow Road", "GCN Peach Beach", "DS Yoshi Falls", "SNES Ghost Valley 2", "N64 Mario Raceway", "N64 Sherbet Land", "GBA Shy Guy Beach", "DS Delfino Square", "GCN Waluigi Stadium", "DS Desert Hills", "GBA Bowser Castle 3", "N64 DK's Jungle Parkway", "GCN Mario Circuit", "SNES Mario Circuit 3", "DS Peach Gardens", "GCN DK Mountain", "N64 Bowser's Castle")]
    Track,

    [SouvenirQuestion("What was the vehicle listed on the information display in {0}?", OneColumn4Answers, "Standard Kart S", "Baby Booster", "Concerto", "Cheep Charger", "Rally Romper", "Blue Falcon", "Standard Bike S", "Bullet Bike", "Nanobike", "Quacker", "Magikruiser", "Bubble Bike", "Standard Kart M", "Nostalgia 1", "Wild Wing", "Turbo Blooper", "Royal Racer", "B Dasher Mk. 2", "Standard Bike M", "Mach Bike", "Bon Bon", "Rapide", "Nitrocycle", "Dolphin Dasher", "Standard Kart L", "Offroader", "Flame Flyer", "Piranha Prowler", "Jetsetter", "Honeycoupe", "Standard Bike L", "Bowser Bike", "Wario Bike", "Twinkle Star", "Torpedo", "Phantom")]
    Vehicle
}

public partial class SouvenirModule
{
    [SouvenirHandler("PlacementRouletteModule", "Placement Roulette", typeof(SPlacementRoulette), "Brawlboxgaming")]
    private IEnumerator<SouvenirInstruction> ProcessPlacementRoulette(ModuleData module)
    {
        var comp = GetComponent(module, "PlacementRouletteModule");
        yield return WaitForSolve;

        var character = GetField<string>(comp, "Character").Get();
        var vehicle = GetField<string>(comp, "Vehicle").Get();
        var track = GetField<string>(comp, "Track").Get();

        addQuestions(module,
            makeQuestion(Question.PlacementRouletteChar, module, correctAnswers: new[] { character }),
            makeQuestion(Question.PlacementRouletteTrack, module, correctAnswers: new[] { track }),
            makeQuestion(Question.PlacementRouletteVehicle, module, correctAnswers: new[] { vehicle })
        );
    }
}
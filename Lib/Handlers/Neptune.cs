using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNeptune
{
    [SouvenirQuestion("Which star was displayed in {0}?", OneColumn4Answers, ExampleAnswers = ["Bob-omb Battlefield #1", "Whomp's Fortress #2", "Jolly Roger Bay #3", "Bowser in the Sky"])]
    Star
}

public partial class SouvenirModule
{
    [SouvenirHandler("neptune", "Neptune", typeof(SNeptune), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessNeptune(ModuleData module)
    {
        var comp = GetComponent(module, "NeptuneScript");
        yield return WaitForSolve;

        var starNames = new string[] {
            "Bob-omb Battlefield #1", "Bob-omb Battlefield #2", "Bob-omb Battlefield #3", "Bob-omb Battlefield #4", "Bob-omb Battlefield #5", "Bob-omb Battlefield #6", "Bob-omb Battlefield 100 Coins",
            "Whomp's Fortress #1", "Whomp's Fortress #2", "Whomp's Fortress #3", "Whomp's Fortress #4", "Whomp's Fortress #5", "Whomp's Fortress #6", "Whomp's Fortress 100 Coins",
            "Jolly Roger Bay #1", "Jolly Roger Bay #2", "Jolly Roger Bay #3", "Jolly Roger Bay #4", "Jolly Roger Bay #5", "Jolly Roger Bay #6", "Jolly Roger Bay 100 Coins",
            "Cool Cool Mountain #1", "Cool Cool Mountain #2", "Cool Cool Mountain #3", "Cool Cool Mountain #4", "Cool Cool Mountain #5", "Cool Cool Mountain #6", "Cool Cool Mountain 100 Coins",
            "Big Boo's Haunt #1", "Big Boo's Haunt #2", "Big Boo's Haunt #3", "Big Boo's Haunt #4", "Big Boo's Haunt #5", "Big Boo's Haunt #6", "Big Boo's Haunt 100 Coins",
            "Hazy Maze Cave #1", "Hazy Maze Cave #2", "Hazy Maze Cave #3", "Hazy Maze Cave #4", "Hazy Maze Cave #5", "Hazy Maze Cave #6", "Hazy Maze Cave 100 Coins",
            "Lethal Lava Land #1", "Lethal Lava Land #2", "Lethal Lava Land #3", "Lethal Lava Land #4", "Lethal Lava Land #5", "Lethal Lava Land #6", "Lethal Lava Land 100 Coins",
            "Shifting Sand Land #1", "Shifting Sand Land #2", "Shifting Sand Land #3", "Shifting Sand Land #4", "Shifting Sand Land #5", "Shifting Sand Land #6", "Shifting Sand Land 100 Coins",
            "Dire Dire Docks #1", "Dire Dire Docks #2", "Dire Dire Docks #3", "Dire Dire Docks #4", "Dire Dire Docks #5", "Dire Dire Docks #6", "Dire Dire Docks 100 Coins",
            "Snowman's Land #1", "Snowman's Land #2", "Snowman's Land #3", "Snowman's Land #4", "Snowman's Land #5", "Snowman's Land #6", "Snowman's Land 100 Coins",
            "Wet Dry World #1", "Wet Dry World #2", "Wet Dry World #3", "Wet Dry World #4", "Wet Dry World #5", "Wet Dry World #6", "Wet Dry World 100 Coins",
            "Tall Tall Mountain #1", "Tall Tall Mountain #2", "Tall Tall Mountain #3", "Tall Tall Mountain #4", "Tall Tall Mountain #5", "Tall Tall Mountain #6", "Tall Tall Mountain 100 Coins",
            "Tiny Huge Island #1", "Tiny Huge Island #2", "Tiny Huge Island #3", "Tiny Huge Island #4", "Tiny Huge Island #5", "Tiny Huge Island #6", "Tiny Huge Island 100 Coins",
            "Tick Tock Clock #1", "Tick Tock Clock #2", "Tick Tock Clock #3", "Tick Tock Clock #4", "Tick Tock Clock #5", "Tick Tock Clock #6", "Tick Tock Clock 100 Coins",
            "Rainbow Ride #1", "Rainbow Ride #2", "Rainbow Ride #3", "Rainbow Ride #4", "Rainbow Ride #5", "Rainbow Ride #6", "Rainbow Ride 100 Coins",
            "Princess' Secret Slide (Normal)", "Princess' Secret Slide (Fast)", "Secret Aquarium", "Tower of the Wing Cap", "Cavern of the Metal Cap", "Vanish Cap under the Moat", "Wing Mario over the Rainbow",
            "MIPS #1", "MIPS #2", "Toad #1", "Toad #2", "Toad #3", "Bowser in the Dark World", "Bowser in the Fire Sea", "Bowser in the Sky" };

        addQuestion(module, Question.NeptuneStar, correctAnswers: new[] { starNames[GetIntField(comp, "chosenStar").Get()] }, preferredWrongAnswers: starNames);
    }
}
using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBottomGear
{
    [SouvenirQuestion("What tweet was shown in {0}?", OneColumn4Answers, "Today on bottom gear I drive a silent electric ca…", "*show budget does not exceed 23¥", "good evening ladies and gents today, our todayz s…", "today we will be reviewing one of a kin vehicle t…", "helo mate we are going to asda do  uwant sanythij…", "hello i am stug i go quikk noom", "oy luv you posh dickead oy 'ave cum bak gimme a s…", "hammon you tiny man where is the lambo chevy?", "gon ei crashed it into James car", "hammond you sodding tic tac this was my laborghin…", "call 999 my fokin cah is beaning on Fire mate", "ham ond i have crack additcion i am die", "Jeremy I have to write divorce papers today I don…", "we do not hav petroleum hmalet", "Tody on medium gear, wat happens when taste exhoo…", "K, I'll have a wiff.", "Ery nice.", "No Jeremia, car gas bad for helf.", "Shut mouth hammock.", "cock", "Shut up jams", "th Esped is a lot !", "weed", "car", "feet")]
    Tweet
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSBottomGear", "Bottom Gear", typeof(SBottomGear), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessBottomGear(ModuleData module)
    {
        var comp = GetComponent(module, "BottomGearScript");
        yield return WaitForSolve;

        var index = GetField<int>(comp, "ThisIsARandomNumberUsedToSelectText").Get(v => v is < 0 or >= 25 ? "expected index 0–24" : null);
        var tweets = Ut.Attributes[Question.BottomGearTweet].AllAnswers;
        yield return question(SBottomGear.Tweet).Answers(tweets[index]);
    }
}
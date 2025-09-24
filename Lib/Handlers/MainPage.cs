using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMainPage
{
    [SouvenirQuestion("Which main page did the {1} button’s effect come from in {0}?", ThreeColumns6Answers, Arguments = ["toons", "games", "characters", "downloads", "store", "email"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(1, 26)]
    ButtonEffectOrigin,

    [SouvenirQuestion("Which main page did {1} come from in {0}?", ThreeColumns6Answers, Arguments = ["Homestar", "the background"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(1, 27)]
    HomestarBackgroundOrigin,

    [SouvenirQuestion("Which color did the bubble not display in {0}?", TwoColumns4Answers, "Blue", "Green", "Red", "Yellow", TranslateAnswers = true)]
    BubbleColors,

    [SouvenirQuestion("Which of the following messages did the bubble {1} in {0}?", OneColumn4Answers, "play a game", "latest toon", "latest merch", "new strong bad email", "new sbemail a comin", "email soon", "new toon soon", "new cartoon!", "hey, a new toon!!", "more biz cas fri", "biz cas fri", "new biz cas fri!", "short shorts!", "new short shortly", "new short!", Arguments = ["display", "not display"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    BubbleMessages
}

public partial class SouvenirModule
{
    [SouvenirHandler("mainpage", "Main Page", typeof(SMainPage), "ObjectsCountries")]
    private IEnumerator<SouvenirInstruction> ProcessMainPage(ModuleData module)
    {
        var comp = GetComponent(module, "_mainpagescript");

        // Homestar and the background
        var homestarBackground = GetField<object>(comp, "HSBG", isPublic: true).Get();
        var homestarNum = GetIntField(homestarBackground, "HSnumber").Get(min: 0, max: 26) + 1;
        var backgroundNum = GetIntField(homestarBackground, "BGnumber").Get(min: 0, max: 26) + 1;

        yield return WaitForSolve;

        // The buttons' effects
        var anims = GetArrayField<KMSelectable>(comp, "menuButtons", isPublic: true).Get(expectedLength: 6);
        var animComps = anims.Select(b => b.GetComponent("_mpAnims")).ToArray();
        var animNums = Enumerable.Range(0, 6).Select(ix => GetIntField(animComps[ix], "animNum").Get(min: 0, max: 25) + 1).ToArray();

        // The color that the bubble did not show
        var absentBubbleColor = titleCase(GetField<string>(comp, "colorNotPresent").Get(col => !new[] { "blue", "green", "red", "yellow" }.Contains(col) ? $"unexpected color '{col}'" : null));

        // The bubble's messages
        var bubbleFirstMessage = GetArrayField<string>(comp, "chosenFirstMessages").Get(expectedLength: 5);
        var bubbleMessages = GetField<string[,]>(comp, "messages").Get(arr => arr.GetLength(0) != 5 || arr.GetLength(1) != 3 ? $"expected 5x3 array. Array was {arr.GetLength(0)}x{arr.GetLength(1)}" : null);
        var bubbleIndices = Enumerable.Range(1, 3).Select(ix => GetIntField(comp, $"message{ix}").Get(min: 0, max: ix == 1 ? 4 : 14)).ToArray();
        var bubbleMessageAnswers = new[] { bubbleFirstMessage[bubbleIndices[0]], bubbleMessages[bubbleIndices[1] % 5, bubbleIndices[1] / 5], bubbleMessages[bubbleIndices[2] % 5, bubbleIndices[2] / 5] };

        var buttons = new[] { "toons", "games", "characters", "downloads", "store", "email" };
        for (var ix = 0; ix < buttons.Length; ix++)
            yield return question(SMainPage.ButtonEffectOrigin, args: [buttons[ix]]).Answers(animNums[ix].ToString());
        yield return question(SMainPage.HomestarBackgroundOrigin, args: ["Homestar"]).Answers(homestarNum.ToString());
        yield return question(SMainPage.HomestarBackgroundOrigin, args: ["the background"]).Answers(backgroundNum.ToString());
        yield return question(SMainPage.BubbleColors).Answers(absentBubbleColor);
        yield return question(SMainPage.BubbleMessages, args: ["display"]).Answers(bubbleMessageAnswers);
        yield return question(SMainPage.BubbleMessages, args: ["not display"]).Answers(SMainPage.BubbleMessages.GetAnswers().Except(bubbleMessageAnswers).ToArray());
    }
}

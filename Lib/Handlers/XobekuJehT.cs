using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SXobekuJehT
{
    [SouvenirQuestion("What song was played on {0}?", OneColumn4Answers, ExampleAnswers = ["Gimme Gimme Gimme", "Take On Me", "Barbie Girl", "Do I Wanna Know"])]
    Song
}

public partial class SouvenirModule
{
    [SouvenirHandler("xobekuj", "xobekuJ ehT", typeof(SXobekuJehT), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessXobekuJehT(ModuleData module)
    {
        var comp = GetComponent(module, "tpircSxobekuJ");
        yield return WaitForSolve;

        var songIx = GetIntField(comp, "songselect").Get();
        var songList = GetArrayField<string>(comp, "titles").Get();

        yield return question(SXobekuJehT.Song).Answers(songList[songIx], preferredWrong: songList);
    }
}
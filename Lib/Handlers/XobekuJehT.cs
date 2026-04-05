using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SXobekuJehT
{
    [Question("What song was played on {0}?", OneColumn4Answers, ExampleAnswers = ["Gimme Gimme Gimme", "Take On Me", "Barbie Girl", "Do I Wanna Know"])]
    [ReverseQuestionGimmick]
    Song
}

public partial class SouvenirModule
{
    [Handler("xobekuj", "xobekuJ ehT", typeof(SXobekuJehT), "Quinn Wuest")]
    [ManualQuestion("What song was played?")]
    private IEnumerator<SouvenirInstruction> ProcessXobekuJehT(ModuleData module)
    {
        var comp = GetComponent(module, "tpircSxobekuJ");
        yield return WaitForSolve;

        var songIx = GetIntField(comp, "songselect").Get();
        var songList = GetArrayField<string>(comp, "titles").Get();

        yield return question(SXobekuJehT.Song).Answers(songList[songIx], preferredWrong: songList);
    }
}

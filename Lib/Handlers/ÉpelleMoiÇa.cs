using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SÉpelleMoiÇa
{
    [SouvenirQuestion("What word was asked to be spelled in {0}?", TwoColumns4Answers, ExampleAnswers = ["abasourdi", "aberrant", "abrasive", "acatalectique", "accueil", "acrobatie", "aligot", "amphigourique", "analgésiante", "antipasti"])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("epelleMoiCa", "Épelle-moi Ça", typeof(SÉpelleMoiÇa), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessÉpelleMoiÇa(ModuleData module)
    {
        var comp = GetComponent(module, "EpelleMoiCaScript");
        yield return WaitForSolve;

        var wordList = GetField<string[][]>(comp, "wordList").Get();
        var inputtedText = GetField<string>(comp, "inputtedText").Get();
        var index = -1;
        for (var i = 0; i < wordList.Length; i++)
            if (wordList[i].Contains(inputtedText))
                index = i;
        var words = Enumerable.Range(0, wordList.Length).Except(new[] { index }).Select(i => wordList[i][0]).ToArray();

        yield return question(SÉpelleMoiÇa.Word).Answers(inputtedText, preferredWrong: words);
    }
}
using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STWords
{
    [Question("Which word was present in {0}?", OneColumn4Answers, "Tautochronous", "Tarantella", "Tenderometer", "Tellurometer", "Tectosphere", "Tessaraglot", "Tamandua", "Tabernacular", "Tachygraphy", "Tangoreceptor", "Tatterdemalion", "Teichoscopy", "Terpsichorean", "Tellurian", "Taphephobia", "Tabernacle", "Tachyphrasia", "Tauromorphous", "Taphrogenesis", "Tablature")]
    Words
}

public partial class SouvenirModule
{
    [Handler("tWords", "T-Words", typeof(STWords), "Espik")]
    [ManualQuestion("What were the words?")]
    private IEnumerator<SouvenirInstruction> ProcessTWords(ModuleData module)
    {
        var comp = GetComponent(module, "tWordsScript");
        yield return WaitForSolve;

        var pressedWords = GetListField<string>(comp, "pressedWords").Get(expectedLength: 4);
        yield return question(STWords.Words).Answers(pressedWords.ToArray());
    }
}

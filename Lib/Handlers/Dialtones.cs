using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SDialtones
{
    [Question("What was the received word in {0}?", ThreeColumns6Answers, "ANGEL", "AZURE", "BEACH", "CANDY", "DRAKE", "ENNUI", "EQUAL", "FOLIO", "GHOST", "HELIX", "INERT", "JOKER", "LIMBO", "MANIA", "NIMOY", "NOTED", "OPERA", "PHONE", "QUARK", "RADIO", "SPACE", "STACK", "THING", "TOUCH", "UNITE", "VELDT", "WALTZ", "XENON", "YOUNG", "ZONER")]
    Word
}

public partial class SouvenirModule
{
    private static readonly Dictionary<string, string> _dialtonesAnswers = [];

    [Handler("xelDialtones", "Dialtones", typeof(SDialtones), "Anonymous")]
    [ManualQuestion("What was the received word?")]
    private IEnumerator<SouvenirInstruction> ProcessDialtones(ModuleData module)
    {
        var comp = GetComponent(module, "Dialtones");
        if (_dialtonesAnswers.Count is 0)
        {
            var convert = GetMethod<string>(comp, "ConvertToDialtones", 1);
            foreach (var word in SDialtones.Word.GetAnswers())
            {
                var tones = convert.Invoke([word]);
                _dialtonesAnswers[tones] = word;
            }
        }

        yield return WaitForSolve;

        var toneString = GetField<string>(comp, "questionWord").Get();
        if (!_dialtonesAnswers.TryGetValue(toneString, out var receivedWord))
            throw new AbandonModuleException($"Unexpected set of question dialtones {toneString}");
        toneString = GetField<string>(comp, "answerWord").Get();
        if (!_dialtonesAnswers.TryGetValue(toneString, out var answerWord))
            throw new AbandonModuleException($"Unexpected set of solution dialtones {toneString}");

        yield return question(SDialtones.Word).Answers(receivedWord, preferredWrong: [answerWord]);
    }
}

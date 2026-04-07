using System;
using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEeBgnillepS
{
    [Question("What word was asked to be spelled in {0}?", ThreeColumns6Answers, Type = AnswerType.Audio, AudioFieldName = "eeBgnillepSAudio")]
    [ReverseQuestionGimmick]
    Word
}

public partial class SouvenirModule
{
    [Handler("eeBgnilleps", "eeB gnillepS", typeof(SEeBgnillepS), "Quinn Wuest")]
    [ManualQuestion("What word was asked to be spelled?")]
    private IEnumerator<SouvenirInstruction> ProcessEeBgnillepS(ModuleData module)
    {
        var comp = GetComponent(module, "tpircSeeBgnillepS");
        var wordList = new[] { "accommodation", "acquiesce", "antediluvian", "appoggiatura", "autochthonous", "bouillabaisse", "bourgeoisie", "chauffeur", "chiaroscurist", "cholmondeley", "chrematistic", "chrysanthemum", "cnemidophorous", "conscientious", "courtoisie", "cymotrichous", "daquiri", "demitasse", "elucubrate", "embarrass",  "eudaemonic", "euonym", "featherstonehaugh", "feuilleton", "fluorescent", "foudroyant", "gnocchi", "idiosyncracy", "irascible", "kierkagaardian",  "laodicean", "liaison", "logorrhea", "mainwaring", "malfeasance", "manoeuvre", "memento", "milquetoast", "minuscule", "odontalgia",  "onomatopoeia", "paraphernalia", "pharaoh", "playwright", "pococurante", "precocious", "privilege", "prospicience", "psittaceous", "psoriasis",  "pterodactyl", "questionnaire", "rhythm", "sacreligious", "scherenschnitte", "sergeant", "smaragdine", "stromuhr", "succedaneum", "surveillance",  "taaffeite", "unconscious", "ursprache", "vengeance", "vivisepulture", "wednesday", "withhold", "worcestershire", "xanthosis", "ytterbium"};

        yield return WaitForSolve;
        var word = Array.IndexOf(wordList, GetField<string>(comp, "drowyek").Get().ToLowerInvariant());

        yield return question(SEeBgnillepS.Word).Answers(eeBgnillepSAudio[word], preferredWrong: eeBgnillepSAudio);
    }
}

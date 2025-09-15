using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEeBgnillepS
{
    [SouvenirQuestion("What word was asked to be spelled in {0}?", TwoColumns4Answers, ExampleAnswers = ["odontalgia", "precocious", "privilege", "prospicience"])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("eeBgnilleps", "eeB gnillepS", typeof(SEeBgnillepS), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessEeBgnillepS(ModuleData module)
    {
        var comp = GetComponent(module, "tpircSeeBgnillepS");
        yield return WaitForSolve;

        var focus = GetField<string>(comp, "drowyek").Get().ToLowerInvariant();
        var spellTheWord = new[] { "accommodation", "acquiesce", "antediluvian", "appoggiatura", "autochthonous", "bouillabaisse", "bourgeoisie", "chauffeur", "chiaroscurist", "cholmondeley", "chrematistic", "chrysanthemum", "cnemidophorous", "conscientious", "courtoisie", "cymotrichous", "daquiri", "demitasse", "elucubrate", "embarrass", "eudaemonic", "euonym", "featherstonehaugh", "feuilleton", "fluorescent", "foudroyant", "gnocchi", "idiosyncracy", "irascible", "kierkagaardian", "laodicean", "liaison", "logorrhea", "mainwaring", "malfeasance", "manoeuvre", "memento", "milquetoast", "minuscule", "odontalgia", "onomatopoeia", "paraphernalia", "pharaoh", "playwright", "pococurante", "precocious", "privilege", "prospicience", "psittaceous", "psoriasis", "pterodactyl", "questionnaire", "rhythm", "sacreligious", "scherenschnitte", "sergeant", "smaragdine", "stromuhr", "succedaneum", "surveillance", "taaffeite", "unconscious", "ursprache", "vengeance", "vivisepulture", "wednesday", "withhold", "worcestershire", "xanthosis", "ytterbium" };

        addQuestion(module, Question.eeBgnillepSWord, correctAnswers: new[] { focus }, preferredWrongAnswers: spellTheWord);
    }
}
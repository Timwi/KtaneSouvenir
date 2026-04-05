using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGiantsCipher
{
    [Question("What was the displayed keyword in {0}?", ThreeColumns6Answers, "ACACIA", "MADMAN", "ACHING", "FALCON", "BALSAM", "MAGNET", "MAGNUM", "HADRON", "GERBIL", "EASILY", "CARING", "ABSENT", "KETTLE", "BANNER", "BASQUE", "KAZOOS", "COFFEE", "HOBBIT", "ANALOG", "BRAINS", "ERASED", "DRIVEN", "FOGRUM", "COLORS", "ATOMIC", "LUNACY", "JOYFUL", "LONDON", "INSTIL", "AUTUMN", "CONSUL", "CONVOY", "ZAGGED", "TABLES", "NAMING", "SADIST", "OBEYED", "RAISES", "VACUUM", "REBOOT", "ULTIMA", "OBTAIN", "TAXING", "NINETY", "TAPPED", "ZIPPER", "THRONE", "NEURON", "QUEBEC", "QUACKS", "WRAITH", "QUEENS", "TRIPLE", "TRASHY", "QUINOA", "ROBOTS", "WORKED", "VOWELS", "OWNING", "NOTION", "TROUGH", "VORTEX", "SPOUSE", "SORROW")]
    Keywords
}

public partial class SouvenirModule
{
    [Handler("GiantsCipher", "Giants Cipher", typeof(SGiantsCipher), "thunder725")]
    [ManualQuestion("What was the displayed keyword?")]
    private IEnumerator<SouvenirInstruction> ProcessGiantsCipher(ModuleData module)
    {
        var comp = GetComponent(module, "GiantsCipher");
        var keywordAnswer = GetField<string>(comp, "SelectedKeyword").Get();

        yield return WaitForSolve;

        yield return question(SGiantsCipher.Keywords).Answers(keywordAnswer);
    }
}

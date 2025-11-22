using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGiantsCipher
{
    [SouvenirQuestion("What was the displayed Keyword in {0}?", ThreeColumns6Answers, "ACACIA", "MADMAN", "ACHING", "FALCON", "BALSAM", "MAGNET", "MAGNUM", "HADRON", "GERBIL", "EASILY", "CARING", "ABSENT", "KETTLE", "BANNER", "BASQUE", "KAZOOS", "COFFEE", "HOBBIT", "ANALOG", "BRAINS", "ERASED", "DRIVEN", "FOGRUM", "COLORS", "ATOMIC", "LUNACY", "JOYFUL", "LONDON", "INSTIL", "AUTUMN", "CONSUL", "CONVOY", "ZAGGED", "TABLES", "NAMING", "SADIST", "OBEYED", "RAISES", "VACUUM", "REBOOT", "ULTIMA", "OBTAIN", "TAXING", "NINETY", "TAPPED", "ZIPPER", "THRONE", "NEURON", "QUEBEC", "QUACKS", "WRAITH", "QUEENS", "TRIPLE", "TRASHY", "QUINOA", "ROBOTS", "WORKED", "VOWELS", "OWNING", "NOTION", "TROUGH", "VORTEX", "SPOUSE", "SORROW")]
    Keywords
}

public partial class SouvenirModule
{
    [SouvenirHandler("GiantsCipher", "Giants Cipher", typeof(SGiantsCipher), "thunder725")]
    private IEnumerator<SouvenirInstruction> ProcessGiantsCipher(ModuleData module)
    {
        var comp = GetComponent(module, "GiantsCipher");
        var keywordAnswer = GetField<string>(comp, "SelectedKeyword").Get();

        yield return WaitForSolve;

        yield return question(SGiantsCipher.Keywords).Answers(keywordAnswer);
    }
}

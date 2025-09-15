using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBoobTube
{
    [SouvenirQuestion("Which word was shown on {0}?", OneColumn4Answers, "Shittah", "Dik-Dik", "Aktashite", "Tetheradick", "Sack-Butt", "Nobber", "Knobstick", "Jerkinhead", "Haboob", "Fanny-Blower", "Assapanick", "Fuksheet", "Clatterfart", "Humpenscrump", "Cock-Bell", "Slagger", "Pakapoo", "Wankapin", "Lobcocked", "Poonga", "Sexagesm", "Tit-Bore", "Pershitte", "Invagination", "Bumfiddler", "Nestle-Cock", "Gullgroper", "Boob Tube", "Boobyalla", "Dreamhole")]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("boobTubeModule", "Boob Tube", typeof(SBoobTube), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessBoobTube(ModuleData module)
    {
        var comp = GetComponent(module, "BoobTubeScript");
        var buttons = GetArrayField<KMSelectable>(comp, "buttons", true).Get(expectedLength: 6);
        var buttonTexts = GetArrayField<TextMesh>(comp, "buttonTexts", true).Get(expectedLength: 6);
        var struck = false;
        module.Module.OnStrike += () => struck = true;
        for (var i = 0; i < 6; i++)
        {
            var j = i;
            var origInteract = buttons[i].OnInteract;
            buttons[i].OnInteract = () =>
            {
                origInteract();
                if (!struck)
                    buttonTexts[j].text = "✓";
                struck = false;
                return false;
            };
        }

        yield return WaitForSolve;

        var words = GetArrayField<string>(comp, "chosenWords").Get(expectedLength: 6, validator: v => !SBoobTube.Word.GetAnswers().Contains(v) ? "Unknown word" : null);
        yield return question(SBoobTube.Word).Answers(words);
    }
}
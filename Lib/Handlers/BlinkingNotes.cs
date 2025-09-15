using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBlinkingNotes
{
    [SouvenirQuestion("What song was flashed in {0}?", OneColumn4Answers, "New Super Mario Bros. - Castle Theme", "Better Call Saul Intro", "Franz Schubert - Serenade", "Keep Talking and Nobody Explodes OST - SMILEYFACE", "Plants Vs. Zombies OST - Watery Graves (Horde)", "Cass Elliot - Make Your Own Kind Of Music", "Michael Jackson - Earth Song", "Maon Kurosaki - DEAD OR LIE", "La Marseillaise (French National Anthem)", "Dave James & Keith Beauvais - Class Act", "Rhythm Heaven Fever OST - Exhibition Match", "Lost OST - Hollywood And Vines", "TLoZ: A Link To The Past - Hyrule Castle", "TLoZ: Spirit Tracks OST - Realm Overworld", "Jamiroquai - Virtual Insanity", "Mii Channel Theme")]
    Song
}

public partial class SouvenirModule
{
    [SouvenirHandler("blinkingNotes", "Blinking Notes", typeof(SBlinkingNotes), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessBlinkingNotes(ModuleData module)
    {
        yield return WaitForSolve;
        var comp = GetComponent(module, "blinkingLightsScript");
        var correct = GetIntField(comp, "correctClip").Get(min: 0, max: 15);

        yield return question(SBlinkingNotes.Song).Answers(Question.BlinkingNotesSong.GetAnswers()[correct]);
    }
}
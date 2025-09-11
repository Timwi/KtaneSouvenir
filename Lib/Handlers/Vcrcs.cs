using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SVcrcs
{
    [SouvenirQuestion("What was the word in {0}?", TwoColumns4Answers, "destiny", "control", "refresh", "grouped", "wedging", "summary", "kitchen", "teacher", "concern", "section", "similar", "western", "dropper", "checker", "xeroses", "sunrise", "abolish", "harvest", "protest", "shallow", "plotted", "deafens", "colored", "aroused", "unsling", "holiday", "dictate", "dribble", "retreat", "episode", "crashed", "crazily", "silvers", "usurped", "witcher", "jealous", "village", "wizards", "prosper", "recycle", "pounced", "nonfood", "imblaze", "dryable", "swiftly", "mention", "rubbish", "realize", "collect", "surgeon", "gearbox", "schnozz", "passion", "freshen", "society", "passive", "archive", "shelter", "harmful", "freedom", "papayas", "thwarts", "railway", "teapots", "ravines", "density", "provide", "diagram", "lighter", "general", "upriver", "editors", "mingled", "ransoms", "prairie", "balance", "applied", "history", "calorie", "realism", "liquids", "validly", "varying", "wickers", "isolate", "falsify", "painter", "mixture", "bedroom", "dilemma", "skylike", "ranging", "simplex", "gallied", "missile", "posture", "highway", "prevent", "bracket", "project")]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("VCRCS", "Vcrcs", typeof(SVcrcs), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessVcrcs(ModuleData module)
    {
        var comp = GetComponent(module, "VcrcsScript");
        var wordTextMesh = GetField<TextMesh>(comp, "Words", isPublic: true).Get();

        string word = null;
        // The module changes the displayed word to “SOLVED” _before_ calling HandlePass, so this will get the last word displayed
        module.Module.OnPass += delegate { word = wordTextMesh.text; return false; };
        yield return WaitForSolve;

        if (word == null)
            throw new AbandonModuleException("‘Words.text’ is null, or OnPass was never called.");

        addQuestion(module, Question.VcrcsWord, correctAnswers: new[] { word });
    }
}
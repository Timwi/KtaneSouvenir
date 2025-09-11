using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimonSpeaks
{
    [SouvenirQuestion("Which bubble flashed first in {0}?", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    Positions,
    
    [SouvenirQuestion("Which bubble flashed second in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "SimonSpeaksSprites")]
    Shapes,
    
    [SouvenirQuestion("Which language was the bubble that flashed third in {0} in?", TwoColumns4Answers, "English", "Danish", "Dutch", "Esperanto", "Finnish", "French", "German", "Hungarian", "Italian")]
    Languages,
    
    [SouvenirQuestion("Which word was in the bubble that flashed fourth in {0}?", ThreeColumns6Answers, "black", "sort", "zwart", "nigra", "musta", "noir", "schwarz", "fekete", "nero", "blue", "blå", "blauw", "blua", "sininen", "bleu", "blau", "kék", "blu", "green", "grøn", "groen", "verda", "vihreä", "vert", "grün", "zöld", "verde", "cyan", "turkis", "turkoois", "turkisa", "turkoosi", "turquoise", "türkis", "türkiz", "turchese", "red", "rød", "rood", "ruĝa", "punainen", "rouge", "rot", "piros", "rosso", "purple", "lilla", "purper", "purpura", "purppura", "pourpre", "lila", "bíbor", "porpora", "yellow", "gul", "geel", "flava", "keltainen", "jaune", "gelb", "sárga", "giallo", "white", "hvid", "wit", "blanka", "valkoinen", "blanc", "weiß", "fehér", "bianco", "gray", "grå", "grijs", "griza", "harmaa", "gris", "grau", "szürke", "grigio")]
    Words,
    
    [SouvenirQuestion("What color was the bubble that flashed fifth in {0}?", ThreeColumns6Answers, "black", "blue", "green", "cyan", "red", "purple", "yellow", "white", "gray", TranslateAnswers = true)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("SimonSpeaksModule", "Simon Speaks", typeof(SSimonSpeaks), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSpeaks(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSpeaksModule");
        yield return WaitForSolve;

        var sequence = GetArrayField<int>(comp, "_sequence").Get(expectedLength: 5);
        var colors = GetArrayField<int>(comp, "_colors").Get(expectedLength: 9);
        var words = GetArrayField<int>(comp, "_words").Get(expectedLength: 9);
        var shapes = GetArrayField<int>(comp, "_shapes").Get(expectedLength: 9);
        var languages = GetArrayField<int>(comp, "_languages").Get(expectedLength: 9);
        var wordsTable = GetStaticField<string[][]>(comp.GetType(), "_wordsTable").Get(ar => ar.Length != 9 ? "expected length 9" : null);
        var positionNames = GetStaticField<string[]>(comp.GetType(), "_positionNames").Get(ar => ar.Length != 9 ? "expected length 9" : null);
        var languageNames = new[] { "English", "Danish", "Dutch", "Esperanto", "Finnish", "French", "German", "Hungarian", "Italian" };

        addQuestions(module,
            makeQuestion(Question.SimonSpeaksPositions, module, correctAnswers: new[] { positionNames[sequence[0]] }),
            makeQuestion(Question.SimonSpeaksShapes, module, correctAnswers: new[] { SimonSpeaksSprites[shapes[sequence[1]]] }, preferredWrongAnswers: SimonSpeaksSprites),
            makeQuestion(Question.SimonSpeaksLanguages, module, correctAnswers: new[] { languageNames[languages[sequence[2]]] }),
            makeQuestion(Question.SimonSpeaksWords, module, correctAnswers: new[] { wordsTable[words[sequence[3]]][languages[sequence[3]]] }),
            makeQuestion(Question.SimonSpeaksColors, module, correctAnswers: new[] { wordsTable[colors[sequence[4]]][0] }));
    }
}
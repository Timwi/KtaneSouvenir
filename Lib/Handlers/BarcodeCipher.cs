using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBarcodeCipher
{
    [SouvenirQuestion("What was the screen number in {0}?", OneColumn4Answers)]
    [AnswerGenerator.Integers(0, 999999, "000000")]
    ScreenNumber,
    
    [SouvenirQuestion("What was the edgework represented by the {1} barcode in {0}?", OneColumn4Answers, "SERIAL NUMBER", "BATTERIES", "BATTERY HOLDERS", "PORTS", "PORT PLATES", "LIT INDICATORS", "UNLIT INDICATORS", "INDICATORS", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    BarcodeEdgework,
    
    [SouvenirQuestion("What was the answer for the {1} barcode in {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    BarcodeAnswers
}

public partial class SouvenirModule
{
    [SouvenirHandler("BarcodeCipherModule", "Barcode Cipher", typeof(SBarcodeCipher), "Brawlboxgaming")]
    private IEnumerator<SouvenirInstruction> ProcessBarcodeCipher(ModuleData module)
    {
        var comp = GetComponent(module, "BarcodeCipherScript");

        var edgeworkInfos = GetField<Array>(comp, "edgework").Get();
        var fldName = GetField<string>(edgeworkInfos.GetValue(0), "Name", isPublic: true);
        var barcodes = new[] { fldName.GetFrom(edgeworkInfos.GetValue(0)), fldName.GetFrom(edgeworkInfos.GetValue(1)), fldName.GetFrom(edgeworkInfos.GetValue(2)) };
        var fldScreenNumber = GetField<string>(comp, "screenNumber").Get(validator: str => str.Length != 6 ? "expected length 6" : str.Any(ch => ch is < '0' or > '9') ? "expected digits 0–9" : null);
        var answers = GetArrayField<int>(comp, "answerNumbers").Get(validator: arr => arr.Length != 3 ? "expected length 3" : arr.Any(n => n is < 0 or > 8) ? "expected numbers 0–8" : null);

        yield return WaitForSolve;

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.BarcodeCipherScreenNumber, module, correctAnswers: new[] { fldScreenNumber }));
        for (var i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.BarcodeCipherBarcodeEdgework, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { barcodes[i] }));
            qs.Add(makeQuestion(Question.BarcodeCipherBarcodeAnswers, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { answers[i].ToString() }));
        }
        addQuestions(module, qs);
    }
}
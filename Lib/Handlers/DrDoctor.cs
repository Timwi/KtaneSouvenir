using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDrDoctor
{
    [SouvenirQuestion("Which of these diseases was listed on {0}, but not the one treated?", TwoColumns4Answers, "Alztimer’s", "Braintenance", "Color allergy", "Detonession", "Emojilepsy", "Foot and Morse", "Gout of Life", "HRV", "Indicitis", "Jaundry", "Keypad stones", "Legomania", "Microcontusion", "Narcolization", "OCd", "Piekinson’s", "Quackgrounds", "Royal Flu", "Seizure Siphor", "Tetrinus", "Urinary LEDs", "Verticode", "Widgeting", "XMAs", "Yes-no infection", "Zooties", "Chronic Talk", "Jukepox", "Neurolysis", "Perspective Loss", "Orientitis", "Huntington’s disease")]
    Diseases,

    [SouvenirQuestion("Which of these symptoms was listed on {0}?", TwoColumns4Answers, "Bloating", "Chills", "Cold Hands", "Constipation", "Cough", "Diarrhea", "Disappearance of the Ears", "Dizziness", "Excessive Crying", "Fatigue", "Fever", "Foot swelling", "Gas", "Hallucination", "Headache", "Loss of Smell", "Muscle Cramp", "Nausea", "Numbness", "Shortness of Breath", "Sleepiness", "Thirstiness", "Throat irritation")]
    Symptoms
}

public partial class SouvenirModule
{
    [SouvenirHandler("DrDoctorModule", "Dr. Doctor", typeof(SDrDoctor), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessDrDoctor(ModuleData module)
    {
        var comp = GetComponent(module, "DrDoctorModule");
        yield return WaitForSolve;

        var diagnoses = GetArrayField<string>(comp, "_selectableDiagnoses").Get();
        var symptoms = GetArrayField<string>(comp, "_selectableSymptoms").Get();
        var diagnoseText = GetField<TextMesh>(comp, "DiagnoseText", isPublic: true).Get();

        yield return question(SDrDoctor.Diseases).Answers(diagnoses.Except(new[] { diagnoseText.text }).ToArray());
        yield return question(SDrDoctor.Symptoms).Answers(symptoms);
    }
}
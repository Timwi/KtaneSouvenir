using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMatrix
{
    [SouvenirQuestion("Which word was part of the latest access code used in {0}?", TwoColumns4Answers, "Twins", "Neo", "Seraph", "Cypher", "Persephone", "Tank", "Dozer", "Mouse", "Switch", "Architect", "Smith", "Merovingian", "Morpheus", "Niobe", "Bane", "Oracle", "Keymaker", "Link", "Trinity", "Apoc")]
    AccessCode
}

public partial class SouvenirModule
{
    [SouvenirHandler("matrix", "Matrix", typeof(SMatrix), "BigCrunch22", AddThe = true)]
    [SouvenirManualQuestion("Which word was part of the latest access code used?")]
    private IEnumerator<SouvenirInstruction> ProcessMatrix(ModuleData module)
    {
        var comp = GetComponent(module, "MatrixScript");

        var selectedNames = GetArrayField<string>(comp, "selectedNames");
        var switchObject = GetField<KMSelectable>(comp, "switchObject", isPublic: true).Get();
        var enteredMatrix = false;
        var unscrambledNames = new string[2];

        switchObject.OnInteract += delegate ()
        {
            enteredMatrix = true;

            // “selectedNames” contains the scrambled versions of the names. Find the unscrambled name.
            unscrambledNames = selectedNames.Get()
                .Select(n => SMatrix.AccessCode.GetAnswers().FirstOrDefault(ac => n.ToLowerInvariant().OrderBy(ch => ch).JoinString() == ac.ToLowerInvariant().OrderBy(ch => ch).JoinString()))
                .ToArray();

            return false;
        };

        yield return WaitForSolve;

        if (!enteredMatrix) // Only possible with logfile solve
            yield return legitimatelyNoQuestion(module, "The module was solved without entering the matrix.");

        yield return question(SMatrix.AccessCode).Answers(unscrambledNames);
    }
}

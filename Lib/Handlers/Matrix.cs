using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMatrix
{
    [SouvenirQuestion("Which word was part of the latest access code in {0}?", TwoColumns4Answers, "Twins", "Neo", "Seraph", "Cypher", "Persephone", "Tank", "Dozer", "Mouse", "Switch", "Architect", "Smith", "Merovingian", "Morpheus", "Niobe", "Bane", "Oracle", "Keymaker", "Link", "Trinity", "Apoc")]
    AccessCode,

    [SouvenirQuestion("What was the glitched word in {0}?", TwoColumns4Answers, "headjack", "phone", "dystopia", "control", "paradise", "utopia", "version", "nebuchadnezzar", "zion", "fight", "utopia", "mind", "squiddy", "guns", "trace", "spoon", "machine", "red", "white", "paradise", "metacortex", "flint", "nova", "white", "rabbit", "follow", "matrix", "free", "neural", "mind", "fight", "free", "nova", "blue", "fields", "choice", "battery", "program", "flint", "headjack", "kungfu", "choi", "red", "blue", "pill", "jump", "program", "agent", "sentient", "squiddy", "dystopia", "rabbit", "jump", "code", "mirror", "cookie", "human", "pill", "follow", "version", "sentinel", "machine", "prison", "human", "fields", "battery", "code", "training", "guns", "hel", "elevator", "sentinel", "choi", "matrix", "nebuchadnezzar", "control", "metacortex", "sentient", "unplug", "hardwire", "trainman", "spoon", "cookie", "elevator", "hardwire", "choice", "trace", "mirror", "unplug", "interface", "prison", "kungfu", "interface", "neural", "trainman", "hel", "agent", "training", "zion", "phone")]
    GlitchWord
}

public partial class SouvenirModule
{
    [SouvenirHandler("matrix", "Matrix", typeof(SMatrix), "BigCrunch22", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessMatrix(ModuleData module)
    {
        var comp = GetComponent(module, "MatrixScript");
        yield return WaitForSolve;

        // “selectedNames” contains the scrambled versions of the names. Find the unscrambled name.
        var unscrambledNames = GetArrayField<string>(comp, "selectedNames").Get()
            .Select(n => Question.MatrixAccessCode.GetAnswers().FirstOrDefault(ac => n.ToLowerInvariant().OrderBy(ch => ch).JoinString() == ac.ToLowerInvariant().OrderBy(ch => ch).JoinString()))
            .ToArray();

        addQuestions(module,
            makeQuestion(Question.MatrixAccessCode, module, correctAnswers: unscrambledNames),
            makeQuestion(Question.MatrixGlitchWord, module, correctAnswers: new[] { GetField<string>(comp, "illegalWordText").Get().ToLowerInvariant() }));
    }
}
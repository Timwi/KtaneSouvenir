using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPuzzleIdentification
{
    [SouvenirQuestion("What was the {1} puzzle number in {0}?", ThreeColumns6Answers, ExampleAnswers = ["001", "002", "003", "004", "005", "006"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 170, 1, "000")]
    Num,

    [SouvenirQuestion("What game was the {1} puzzle in {0} from?", OneColumn4Answers, "Professor Layton and the Curious Village", "Professor Layton and Pandora's Box", "Professor Layton and the Lost Future", "Professor Layton and the Spectre's Call", "Professor Layton and the Miracle Mask", "Professor Layton and the Azran Legacy", "Layton's Mystery Journey: Katrielle and the Millionaire's Conspiracy", "Professor Layton vs. Phoenix Wright: Ace Attorney", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Game,

    [SouvenirQuestion("What was the {1} puzzle in {0}?", OneColumn4Answers, ExampleAnswers = ["Where's the Village?", "Dr Schrader's Map", "A Party Crasher", "A Secret Message"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Name
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSPuzzleIdentification", "Puzzle Identification", typeof(SPuzzleIdentification), "GhostSalt")]
    private IEnumerator<SouvenirInstruction> ProcessPuzzleIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "PuzzleIdentificationScript");
        yield return WaitForSolve;

        var namesType = comp.GetType().Assembly.GetType("PuzzleIdentification.Data") ?? throw new AbandonModuleException("I cannot find the PuzzleIdentification.Data type.");
        var names = GetStaticField<string[][]>(namesType, "PuzzleNames", isPublic: true).Get();

        // Grabs the first two puzzle numbers and their games of origin
        var puzzlesOneAndTwo = GetArrayField<int>(comp, "ChosenPuzzles").Get();
        // Grabs the third puzzle number
        var puzzleThree = GetField<int>(comp, "ChosenPuzzle").Get();
        // Grabs the third game of origin
        var gameThree = GetField<int>(comp, "ChosenGame").Get();
        var gameNames = GetField<string[]>(comp, "GameNames").Get();

        addQuestions(module,
            makeQuestion(Question.PuzzleIdentificationNum, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { (puzzlesOneAndTwo[0] + 1).ToString("000") }),
            makeQuestion(Question.PuzzleIdentificationNum, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { (puzzlesOneAndTwo[1] + 1).ToString("000") }),
            makeQuestion(Question.PuzzleIdentificationNum, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { (puzzleThree + 1).ToString("000") }),
            makeQuestion(Question.PuzzleIdentificationGame, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { gameNames[puzzlesOneAndTwo[2]] }, preferredWrongAnswers: gameNames),
            makeQuestion(Question.PuzzleIdentificationGame, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { gameNames[puzzlesOneAndTwo[3]] }, preferredWrongAnswers: gameNames),
            makeQuestion(Question.PuzzleIdentificationGame, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { gameNames[gameThree] }, preferredWrongAnswers: gameNames),
            makeQuestion(Question.PuzzleIdentificationName, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { names[puzzlesOneAndTwo[2]][puzzlesOneAndTwo[0]] }, preferredWrongAnswers: names[puzzlesOneAndTwo[2]]),
            makeQuestion(Question.PuzzleIdentificationName, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { names[puzzlesOneAndTwo[3]][puzzlesOneAndTwo[1]] }, preferredWrongAnswers: names[puzzlesOneAndTwo[3]]),
            makeQuestion(Question.PuzzleIdentificationName, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { names[gameThree][puzzleThree] }, preferredWrongAnswers: names[gameThree]));
    }
}
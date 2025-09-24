using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SKlaxon
{
    [SouvenirQuestion("What was the first module to set off {0}?", OneColumn4Answers, ExampleAnswers = ["3D Maze", "Adjacent Letters", "Adventure Game", "Alphabet", "Anagrams", "Astrology", "Battleship", "Binary LEDs", "Bitmaps", "Bitwise Operations", "Blind Alley", "Boolean Venn Diagram", "Broken Buttons", "The Bulb", "Caesar Cipher", "Cheap Checkout", "Chess", "Chord Qualities", "The Clock", "Color Math", "Colored Squares", "Colour Flash", "Combination Lock", "Complicated Buttons", "Connection Check", "Coordinates", "Crazy Talk", "Creation", "Cryptography", "Double-Oh", "Emoji Math", "English Test", "Fast Math", "FizzBuzz", "Follow the Leader", "Foreign Exchange Rates", "Friendship", "The Gamepad", "Hexamaze", "Ice Cream", "Laundry", "LED Encryption", "Letter Keys", "Light Cycle", "Listening", "Logic", "Microcontroller", "Minesweeper", "Modules Against Humanity", "Monsplode, Fight!", "Morsematics", "Mouse In The Maze", "Murder", "Mystic Square", "Neutralization", "Number Pad", "Only Connect", "Orientation Cube", "Perspective Pegs", "Piano Keys", "Plumbing", "Point of Order", "Probing", "Resistors", "Rhythms", "Rock-Paper-Scissors-Lizard-Spock", "Round Keypad", "Rubik's Cube", "Safety Safe", "The Screw", "Sea Shells", "Semaphore", "Shape Shift", "Silly Slots", "Simon Screams", "Simon States", "Skewed Slots", "Square Button", "Switches", "Symbolic Password", "Text Field", "Third Base", "Tic Tac Toe", "Two Bits", "Web Design", "Wire Placement", "Word Scramble", "Word Search", "Yahtzee", "Zoo"])]
    FirstModule
}

public partial class SouvenirModule
{
    [SouvenirHandler("klaxon", "Klaxon", typeof(SKlaxon), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessKlaxon(ModuleData module)
    {
        yield return WaitForActivate;

        var comp = GetComponent(module, "KlaxonScript");
        var letter = GetArrayField<char>(comp, "CorrectLetters").Get(minLength: 1, validator: c => c is < 'A' or > 'Z' ? "Expected uppercase letters" : null)[0];

        var prevSolved = new HashSet<string>();
        string answer = null;
        while (answer == null)
        {
            var newSolved = Bomb.GetSolvedModuleNames().Where(m => !prevSolved.Contains(m) && m.ToUpperInvariant().Contains(letter)).ToArray();
            if (newSolved.Length == 1)
                answer = newSolved[0];
            else if (newSolved.Length > 1)
                yield return legitimatelyNoQuestion(module, $"It looks like two modules ({newSolved[0]} and {newSolved[1]}) solved at the same time.");
            yield return null;
        }

        yield return WaitForSolve;

        if (answer == null)
            yield return legitimatelyNoQuestion(module, "No module set it off.");

        var preferredWrongAnswers = Bomb.GetSolvedModuleNames();
        preferredWrongAnswers.Remove("The Klaxon");
        if (preferredWrongAnswers.Count < 7)
            preferredWrongAnswers.AddRange(SKlaxon.FirstModule.GetExampleAnswers());

        yield return question(SKlaxon.FirstModule).Answers(answer, preferredWrong: preferredWrongAnswers.ToArray());
    }
}

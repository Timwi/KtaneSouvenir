using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHyperlink
{
    [SouvenirQuestion("What was the {1} character of the hyperlink in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "_", "-", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Characters,

    [SouvenirQuestion("Which module was referenced on {0}?", OneColumn4Answers, "3D Maze", "Adjacent Letters", "Adventure Game", "Alphabet", "Anagrams", "Answering Questions", "Astrology", "Backgrounds", "Battleship", "Big Circle", "Bitmaps", "Blind Alley", "Blind Maze", "Braille", "Broken Buttons", "Burglar Alarm", "Button Sequence", "Caesar Cipher", "Capacitor Discharge", "Cheap Checkout", "Chess", "Chord Qualities", "Color Flash", "Colored Squares", "Colored Switches", "Combination Lock", "Complicated Buttons", "Complicated Wires", "Connection Check", "Cooking", "Coordinates", "Crazy Talk", "Creation", "Cryptography", "Double-Oh", "Emoji Math", "English Test", "European Travel", "Faulty Backgrounds", "Filibuster", "Follow The Leader", "Foreign Exchange Rates", "Forget Me Not", "Friendship", "Game Of Life Cruel", "Game Of Life Simple", "Hexamaze", "HTTP Response", "Hunting", "Ice Cream", "Keypad", "Knob", "Laundry", "Letter Keys", "Light Cycle", "Lights Out", "Listening", "Logic", "Math", "Maze", "Memory", "Microcontroller", "Module Against Humanity", "Monsplode Trading Cards", "Monsplode, Fight!", "Morse Code", "Morsematics", "Mortal Kombat", "Motion Sense", "Mouse In The Maze", "Murder", "Mystic Square", "Neutralization", "Number Pad", "Orientation Cube", "Password", "Perspective Pegs", "Piano Keys", "Plumbing", "Probing", "Resistors", "Rock-Paper-Scissors-Lizard-Spock", "Rotary Phone", "Round Keypad", "Safety Safe", "Sea Shells", "Semaphore", "Shape Shift", "Silly Slots", "Simon Says", "Simon Screams", "Simon States", "Skewed Slots", "Souvenir", "Square Button", "Switches", "Symbolic Coordinates", "Symbolic Password", "Tetris", "Text Field", "The Bulb", "The Button", "The Clock", "The Gamepad", "The iPhone", "The Moon", "The Stopwatch", "The Sun", "The Swan", "Third Base", "Tic-Tac-Toe", "Turn The Key", "Turn The Keys", "Two Bits", "Venting Gas", "Who’s On First", "Who’s That Monsplode", "Wire Placement", "Wire Sequence", "Wires", "Word Scramble", "Word Search", "Zoo")]
    Answer
}

public partial class SouvenirModule
{
    [SouvenirHandler("hyperlink", "Hyperlink", typeof(SHyperlink), "Espik", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessHyperlink(ModuleData module)
    {
        var comp = GetComponent(module, "hyperlinkScript");
        yield return WaitForSolve;

        var moduleNamesType = comp.GetType().Assembly.GetType("IDList") ?? throw new AbandonModuleException("I cannot find the IDList type.");
        var moduleNames = GetStaticField<string[]>(moduleNamesType, "phrases", isPublic: true).Get(validator: ar => ar.Length % 2 != 0 ? "expected even number of items" : null);
        var hyperlink = GetField<string>(comp, "selectedString").Get();
        var anchor = GetIntField(comp, "anchor").Get();

        var questions = new List<QandA>();
        for (var i = 0; i < 11; i++)
            questions.Add(makeQuestion(Question.HyperlinkCharacters, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { hyperlink[i].ToString() }));
        questions.Add(makeQuestion(Question.HyperlinkAnswer, module, correctAnswers: new[] { moduleNames[anchor + 1].Replace("'", "’") }));

        addQuestions(module, questions);
    }
}
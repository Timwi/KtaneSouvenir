using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SScalarDials
{
    [Question("What note was on the {1} display when the dials were in their initial calculated positions in {0}?", ThreeColumns6Answers, Arguments = ["left", "middle", "right"], ArgumentGroupSize = 1, TranslateArguments = [true], AnswerType = InfoType.DynamicFont, ExampleAnswers = ["¯!'×w!", "¯!'g!", "&!'{!", "¯!'K!", "&!'H!", "&!'ÕE!", "&!'ë[!", "ÿ!'êJ!", "&!'I!", "¯!'ÖF!", "ÿ!'äd!", "ÿ!'ÔD!", "¯!'×W!", "ÿ!'ØX!", "¯!'T!", "¯!'F!", "ÿ!'çw!", "&!'èH!", "ÿ!'×w!", "&!'èx!", "ÿ!'d!", "ÿ!'Y!", "¯!'æf!", "ÿ!'æV!"])]
    QNote,

    [Question("What color was the {1} display when the dials were in their initial calculated positions in {0}?", TwoColumns2Answers, "White", "Black", Arguments = ["left", "middle", "right"], ArgumentGroupSize = 1, TranslateArguments = [true], TranslateAnswers = true)]
    QColor,

    [Question("What was the {1} note on the large display in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, AnswerType = InfoType.DynamicFont, ExampleAnswers = ["¯!'×w!", "¯!'g!", "&!'{!", "¯!'K!", "&!'H!", "&!'ÕE!", "&!'ë[!", "ÿ!'êJ!", "&!'I!", "¯!'ÖF!", "ÿ!'äd!", "ÿ!'ÔD!", "¯!'×W!", "ÿ!'ØX!", "¯!'T!", "¯!'F!", "ÿ!'çw!", "&!'èH!", "ÿ!'×w!", "&!'èx!", "ÿ!'d!", "ÿ!'Y!", "¯!'æf!", "ÿ!'æV!"])]
    QLargeDisplayNote,

    [Question("Which major scale was played by the {1} dial in {0}?", TwoColumns4Answers, "C", "C♯/D♭", "D", "D♯/E♭", "E", "F", "F♯/G♭", "G", "G♯/A♭", "A", "A♯/B♭", "B", Arguments = ["left", "middle", "right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    QScale,

    [Discriminator("the Scalar Dials where this note was on the {0} display when the dials were in their initial calculated positions", Arguments = ["left", "middle", "right"], ArgumentGroupSize = 1, TranslateArguments = [true], QuestionExtraType = InfoType.DynamicFont)]
    DNote,

    [Discriminator("the Scalar Dials where the {0} display was {1} when the dials were in their initial calculated positions", Arguments = ["left", "white", "middle", "white", "right", "white", "left", "black", "middle", "black", "right", "black"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    DColor,

    [Discriminator("the Scalar Dials where this was the {0} note on the large display", Arguments = [QandA.Ordinal, "&!'ÔD!", QandA.Ordinal, "¯!'èH!", QandA.Ordinal, "ÿ!'y!"], ArgumentGroupSize = 2, QuestionExtraType = InfoType.DynamicFont)]
    DLargeDisplayNote,

    [Discriminator("the Scalar Dials whose {1} dial played the {0} major scale", Arguments = ["C", "left", "C♯/D♭", "middle", "D", "right", "D♯/E♭", "left", "E", "middle", "F", "right", "F♯/G♭", "left", "G", "middle", "G♯/A♭", "right", "A", "left", "A♯/B♭", "middle", "B", "right"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    DScale,
}

public partial class SouvenirModule
{
    private static readonly int[][] _scalarDials_ClefStarts = "4,5,7,9,11,0,2,4,5;7,9,11,0,2,4,5,7,9;5,7,9,11,0,2,4,5,7".Split(';').Select(str => str.Split(',').Select(int.Parse).ToArray()).ToArray();

    [Handler("scalarDials", "Scalar Dials", typeof(SScalarDials), "Timwi")]
    [ManualQuestion("What were the notes on the displays and their colors when the dials were in their initial calculated positions?")]
    [ManualQuestion("What were the notes on the large display?")]
    [ManualQuestion("Which scales were played by each dial?")]
    private IEnumerator<SouvenirInstruction> ProcessScalarDials(ModuleData module)
    {
        var comp = GetComponent(module, "digitalDials");    // Yup, the class is misnamed

        // noteTexts: Full list of all notes that can be on the display (using the characters from the font)
        var noteTexts = GetArrayField<string[]>(comp, "noteTexts").Get(expectedLength: 36, validator: inner => inner.Length != 3 ? "expected inner lengths 3" : null);
        var noteTextsRegex = noteTexts.SelectMany(arr => arr.Select(note => Regex.Escape(note))).JoinString("|");
        var mainScreen = GetField<TextMesh>(comp, "mainScreen", isPublic: true).Get();
        if (!mainScreen.text.RegexMatch($@"^(?<clef>[&¯ÿ])!'(?<n0>{noteTextsRegex})!'(?<n1>{noteTextsRegex})!'(?<n2>{noteTextsRegex})!$", out var mainScreenMatch))
            throw new AbandonModuleException($"Main screen display is not in expected format. Got: {mainScreen.text.Stringify()}");

        var clef = mainScreenMatch.Groups["clef"].Value[0];
        var clefNum = "&¯ÿ".IndexOf(clef);
        var mainScreenNotes = Enumerable.Range(0, 3).Select(i => $"{clef}!'{mainScreenMatch.Groups[$"n{i}"].Value}!").ToArray();
        var fontInfo = new TextAnswerInfo(mainScreen.font, mainScreen.GetComponent<MeshRenderer>().sharedMaterial.mainTexture);
        var invert = GetArrayField<bool[]>(comp, "invert").Get(expectedLength: 3, validator: inner => inner.Length != 10 ? "expected inner length 10" : null);

        // scalenames: an array that for some reason contains 12 empty strings followed by the names of the 12 scales we actually need
        var scalenames = GetArrayField<string>(comp, "scalenames").Get(expectedLength: 24).Skip(12).ToArray();
        var dialScales = GetArrayField<string>(comp, "dialScalesNote")
            .Get(expectedLength: 3, validator: v => !v.Contains(' ') || !scalenames.Contains(v.Split(' ')[0]) ? "expected scale name followed by a space" : null)
            .Select(str => (Array.IndexOf(scalenames, str.Split(' ')[0]) + 6) % 12) // scalenames starts at F♯ for some reason
            .ToArray();
        var table1 = GetArrayField<int[]>(comp, "table1").Get(expectedLength: 12, validator: arr => arr.Length != 12 ? "expected inner lengths 12" : null);
        var myScaleNames = SScalarDials.QScale.GetAnswers();    // uses proper ♯ and ♭ instead of pleb # and b

        // Calculate the “initial calculated positions” of the dials because those are not stored in fields
        var dialInitialPos = Ut.NewArray(3, dial =>
        {
            var baseNote = noteTexts.IndexOf(arr => arr.Contains(mainScreenMatch.Groups[$"n{dial}"].Value));
            var accidental = Array.IndexOf(noteTexts[baseNote], mainScreenMatch.Groups[$"n{dial}"].Value);
            int col = (_scalarDials_ClefStarts[clefNum][baseNote % 9] + (accidental switch { 0 => 0, 1 => 1, _ => 11 })) % 12;
            return table1[dialScales[dial]][col];
        });

        // allDisplayNotes: Notes shown on the small display for each dial and dial position
        var allDisplayNotes = GetArrayField<string[]>(comp, "displayNotesText").Get(expectedLength: 3, validator: arr => arr.Length != 10 ? "expected inner lengths 10" : null);
        // displayNotes: Notes shown on the small display when each dial is in the calculated initial position
        var displayNotes = Ut.NewArray(3, dial => $"{clef}!{allDisplayNotes[dial][dialInitialPos[dial]]}");

        yield return WaitForSolve;

        var posNames = new[] { "left", "middle", "right" };

        // Use only wrong answers with the same clef
        var allAnswers = noteTexts.SelectMany(arr => arr.Select(note => $"{clef}!'{note}!")).ToArray();
        var preferredWrongNotes = mainScreenNotes.Concat(displayNotes).ToArray();
        var preferredWrongScales = dialScales.Select(pos => myScaleNames[pos]).ToArray();

        for (var pos = 0; pos < 3; pos++)
        {
            yield return question(SScalarDials.QNote, args: [posNames[pos]]).AvoidDiscriminators(SScalarDials.DNote)
                .Answers(displayNotes[pos], preferredWrong: preferredWrongNotes, all: allAnswers, info: fontInfo);
            yield return new Discriminator(SScalarDials.DNote, $"note-{pos}", displayNotes[pos], args: [posNames[pos]],
                questionExtra: new QuestionExtraText(displayNotes[pos], fontInfo.Font, fontInfo.FontTexture));

            yield return question(SScalarDials.QLargeDisplayNote, args: [Ordinal(pos + 1)])
                .AvoidDiscriminators(SScalarDials.DLargeDisplayNote)
                .Answers(mainScreenNotes[pos], preferredWrong: preferredWrongNotes, all: allAnswers, info: fontInfo);
            yield return new Discriminator(SScalarDials.DLargeDisplayNote, $"large-{pos}", mainScreenNotes[pos], args: [Ordinal(pos + 1)],
                questionExtra: new QuestionExtraText(mainScreenNotes[pos], fontInfo.Font, fontInfo.FontTexture));

            yield return question(SScalarDials.QScale, args: [posNames[pos]]).AvoidDiscriminators(SScalarDials.DScale)
                .Answers(myScaleNames[dialScales[pos]], preferredWrong: preferredWrongScales);
            yield return new Discriminator(SScalarDials.DScale, $"scale-{pos}", dialScales[pos], args: [myScaleNames[dialScales[pos]], posNames[pos]]);

            yield return question(SScalarDials.QColor, args: [posNames[pos]]).AvoidDiscriminators(SScalarDials.DColor)
                .Answers(invert[pos][dialInitialPos[pos]] ? "Black" : "White");
            yield return new Discriminator(SScalarDials.DColor, $"color-{pos}", invert[pos][dialInitialPos[pos]], args: [posNames[pos], invert[pos][dialInitialPos[pos]] ? "black" : "white"]);
        }
    }
}

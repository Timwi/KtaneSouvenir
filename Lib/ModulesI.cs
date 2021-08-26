using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessIceCream(KMBombModule module)
    {
        var comp = GetComponent(module, "IceCreamModule");
        var fldCurrentStage = GetIntField(comp, "CurrentStage");
        var fldCustomers = GetArrayField<int>(comp, "CustomerNamesSolution");
        var fldSolution = GetArrayField<int>(comp, "Solution");
        var fldFlavourOptions = GetArrayField<int[]>(comp, "FlavorOptions");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var flavourNames = GetAnswers(Question.IceCreamFlavour);
        var customerNames = GetAnswers(Question.IceCreamCustomer);

        var flavours = new int[3][];
        var solution = new int[3];
        var customers = new int[3];

        for (var i = 0; i < 3; i++)
        {
            while (fldCurrentStage.Get() == i)
                yield return new WaitForSeconds(.1f);
            if (fldCurrentStage.Get() < i)
                throw new AbandonModuleException("The stage number went down from {0} to {1}.", i, fldCurrentStage.Get());

            var options = fldFlavourOptions.Get(expectedLength: 3, validator: x => x.Length != 5 ? "expected length 5" : x.Any(y => y < 0 || y >= flavourNames.Length) ? string.Format("expected range 0–{0}", flavourNames.Length - 1) : null);
            var sol = fldSolution.Get(ar => ar.Any(x => x < 0 || x >= flavourNames.Length) ? string.Format("expected range 0–{0}", flavourNames.Length - 1) : null);
            var cus = fldCustomers.Get(ar => ar.Any(x => x < 0 || x >= customerNames.Length) ? string.Format("expected range 0–{0}", customerNames.Length - 1) : null);

            flavours[i] = options[i].ToArray();
            solution[i] = flavours[i][sol[i]];
            customers[i] = cus[i];
        }
        var qs = new List<QandA>();
        _modulesSolved.IncSafe(_IceCream);

        for (var i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.IceCreamFlavour, _IceCream, formatArgs: new[] { "was on offer, but not sold,", ordinal(i + 1) }, correctAnswers: flavours[i].Where(ix => ix != solution[i]).Select(ix => flavourNames[ix]).ToArray()));
            qs.Add(makeQuestion(Question.IceCreamFlavour, _IceCream, formatArgs: new[] { "was not on offer", ordinal(i + 1) }, correctAnswers: flavourNames.Where((f, ix) => !flavours[i].Contains(ix)).ToArray()));
            if (i != 2)
                qs.Add(makeQuestion(Question.IceCreamCustomer, _IceCream, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { customerNames[customers[i]] }, preferredWrongAnswers: customers.Select(ix => customerNames[ix]).ToArray()));
        }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessIdentityParade(KMBombModule module)
    {
        var comp = GetComponent(module, "identityParadeScript");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_IdentityParade);

        foreach (var btnFieldName in new[] { "hairLeft", "hairRight", "buildLeft", "buildRight", "attireLeft", "attireRight", "suspectLeft", "suspectRight", "convictBut" })
        {
            var btn = GetField<KMSelectable>(comp, btnFieldName, isPublic: true).Get();
            btn.OnInteract = delegate
            {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btn.transform);
                btn.AddInteractionPunch(0.5f);
                return false;
            };
        }

        var textMeshes = new[] { "hairText", "buildText", "attireText", "suspectText" }.Select(fldName => GetField<TextMesh>(comp, fldName, isPublic: true).Get()).ToArray();
        textMeshes[0].text = "Identity";
        textMeshes[1].text = "Parade";
        textMeshes[2].text = "has been";
        textMeshes[3].text = "solved";

        var hairs = GetListField<string>(comp, "hairEntries").Get(expectedLength: 3);
        var builds = GetListField<string>(comp, "buildEntries").Get(expectedLength: 3);
        var attires = GetListField<string>(comp, "attireEntries").Get(expectedLength: 3);

        var validHairs = new[] { "Black", "Blonde", "Brown", "Grey", "Red", "White" };
        var validBuilds = new[] { "Fat", "Hunched", "Muscular", "Short", "Slim", "Tall" };
        var validAttires = new[] { "Blazer", "Hoodie", "Jumper", "Suit", "T-shirt", "Tank top" };

        addQuestions(module,
            makeQuestion(Question.IdentityParadeHairColors, _IdentityParade, formatArgs: new[] { "was" }, correctAnswers: hairs.ToArray()),
            makeQuestion(Question.IdentityParadeHairColors, _IdentityParade, formatArgs: new[] { "was not" }, correctAnswers: validHairs.Except(hairs).ToArray()),
            makeQuestion(Question.IdentityParadeBuilds, _IdentityParade, formatArgs: new[] { "was" }, correctAnswers: builds.ToArray()),
            makeQuestion(Question.IdentityParadeBuilds, _IdentityParade, formatArgs: new[] { "was not" }, correctAnswers: validBuilds.Except(builds).ToArray()),
            makeQuestion(Question.IdentityParadeAttires, _IdentityParade, formatArgs: new[] { "was" }, correctAnswers: attires.ToArray()),
            makeQuestion(Question.IdentityParadeAttires, _IdentityParade, formatArgs: new[] { "was not" }, correctAnswers: validAttires.Except(attires).ToArray()));
    }

    private IEnumerable<object> ProcessIndigoCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "indigoCipher", Question.IndigoCipherAnswer, _IndigoCipher);
    }

    private IEnumerable<object> ProcessInfiniteLoop(KMBombModule module)
    {
        var comp = GetComponent(module, "InfiniteLoop");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_InfiniteLoop);

        var selectedWord = GetField<string>(comp, "SelectedWord").Get();
        addQuestions(module, makeQuestion(Question.InfiniteLoopSelectedWord, _InfiniteLoop, correctAnswers: new[] { selectedWord }));
    }

    private IEnumerable<object> ProcessInnerConnections(KMBombModule module)
    {
        var comp = GetComponent(module, "InnerConnectionsScript");
        var morseNumber = GetField<int>(comp, "morseNumber").Get();
        var rndLEDColour = GetField<int>(comp, "rndLEDColour").Get();
        var fldSolved = GetField<bool>(comp, "_moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_InnerConnections);

        var colourList = new[] { "Black", "Blue", "Red", "White", "Yellow" };

        addQuestions(module,
            makeQuestion(Question.InnerConnectionsLED, _InnerConnections, correctAnswers: new[] { colourList[rndLEDColour] }),
            makeQuestion(Question.InnerConnectionsMorse, _InnerConnections, correctAnswers: new[] { morseNumber.ToString() }));
    }

    private IEnumerable<object> ProcessInterpunct(KMBombModule module)
    {
        var comp = GetComponent(module, "InterpunctScript");
        var fldDisplay = GetField<string>(comp, "displaySymbol");
        var fldStage = GetIntField(comp, "stage");

        var currentStage = 0;
        var texts = new string[3];
        var hasStruck = false;
        module.OnStrike += delegate () { hasStruck = true; return false; };

        while (currentStage < 3)
        {
            yield return null;
            var nextStage = fldStage.Get(min: 1, max: 3);   // stage numbers are 1–3, not 0–2
            if (currentStage != nextStage || hasStruck)
            {
                currentStage = nextStage;
                texts[currentStage - 1] = fldDisplay.Get();
                hasStruck = false;
            }
        }
        _modulesSolved.IncSafe(_Interpunct);

        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.InterpunctDisplay, _Interpunct, formatArgs: new[] { (i + 1).ToString() }, correctAnswers: new[] { texts[i] }));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessIPA(KMBombModule module)
    {
        var comp = GetComponent(module, "ipa");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var symbols = GetStaticField<string[]>(comp.GetType(), "symbols").Get();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_IPA);

        var soundIx = GetIntField(comp, "soundPresent").Get(0, symbols.Length - 1);
        var textMesh = GetArrayField<TextMesh>(comp, "buttonTexts", isPublic: true).Get(expectedLength: 9)[0];
        addQuestions(module, makeQuestion(Question.IpaSymbol, _IPA, textMesh.font, textMesh.GetComponent<MeshRenderer>().sharedMaterial.mainTexture,
            correctAnswers: new[] { symbols[soundIx] }, preferredWrongAnswers: symbols));
    }

    private IEnumerable<object> ProcessiPhone(KMBombModule module)
    {
        var comp = GetComponent(module, "iPhoneScript");
        var fldSolved = GetField<string>(comp, "solved");
        var digits = GetListField<string>(comp, "pinDigits", isPublic: true).Get(expectedLength: 4);

        while (fldSolved.Get() != "solved")
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_iPhone);

        addQuestions(module,
            makeQuestion(Question.iPhoneDigits, _iPhone, formatArgs: new[] { "first" }, correctAnswers: new[] { digits[0] }, preferredWrongAnswers: new[] { digits[1], digits[2], digits[3] }),
            makeQuestion(Question.iPhoneDigits, _iPhone, formatArgs: new[] { "second" }, correctAnswers: new[] { digits[1] }, preferredWrongAnswers: new[] { digits[0], digits[2], digits[3] }),
            makeQuestion(Question.iPhoneDigits, _iPhone, formatArgs: new[] { "third" }, correctAnswers: new[] { digits[2] }, preferredWrongAnswers: new[] { digits[1], digits[0], digits[3] }),
            makeQuestion(Question.iPhoneDigits, _iPhone, formatArgs: new[] { "fourth" }, correctAnswers: new[] { digits[3] }, preferredWrongAnswers: new[] { digits[1], digits[2], digits[0] }));
    }
}
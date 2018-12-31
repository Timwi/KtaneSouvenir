using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

using Rnd = UnityEngine.Random;

/// <summary>
/// On the Subject of Souvenir
/// Created by Timwi
/// </summary>
public class SouvenirModule : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMBombModule Module;
    public KMAudio Audio;
    public KMSelectable[] Answers4;
    public KMSelectable[] Answers6;
    public GameObject Answers4Parent;
    public GameObject Answers6Parent;
    public GameObject[] TpNumbers;

    public KMSelectable MainSelectable;
    public TextMesh TextMesh;
    public Renderer TextRenderer;
    public Renderer SurfaceRenderer;
    public Material FontMaterial;
    public Font FontDefault;
    public Texture FontDefaultTexture;
    public Font FontSymbols;
    public Texture FontSymbolsTexture;

    public static readonly string[] _ignoredModules = {
        "Souvenir",
        "Forget Everything",
        "Forget Me Not",
        "Turn The Key",
        "The Time Keeper"
    };

    private static readonly bool _isTimwisComputer = new[] { "TEKELIA", "CORNFLOWER", "CAITSITH2-PC" }.Contains(Environment.GetEnvironmentVariable("COMPUTERNAME"));
    private static readonly string _timwiPath = @"D:\c\KTANE\Souvenir modules.txt";
    private readonly List<QuestionBatch> _questions = new List<QuestionBatch>();
    private readonly HashSet<KMBombModule> _legitimatelyNoQuestions = new HashSet<KMBombModule>();
    private bool _isActivated = false;

    private QandA _currentQuestion = null;
    private bool _isSolved = false;
    private bool _animating = false;
    private bool _exploded = false;
    private double _surfaceSizeFactor;

    private Dictionary<string, int> _moduleCounts = new Dictionary<string, int>();
    private Dictionary<string, int> _modulesSolved = new Dictionary<string, int>();
    private int _coroutinesActive;

    private static int _moduleIdCounter = 1;
    private int _moduleId;
    private Dictionary<string, Func<KMBombModule, IEnumerable<object>>> _moduleProcessors;

    // The values here are “ModuleType” property on the KMBombModule components.
    const string _3DMaze = "spwiz3DMaze";
    const string _3DTunnels = "3dTunnels";
    const string _AdventureGame = "spwizAdventureGame";
    const string _Algebra = "algebra";
    const string _BigCircle = "BigCircle";
    const string _BinaryLEDs = "BinaryLeds";
    const string _Bitmaps = "BitmapsModule";
    const string _Braille = "BrailleModule";
    const string _BrokenButtons = "BrokenButtonsModule";
    const string _Bulb = "TheBulbModule";
    const string _BurglarAlarm = "burglarAlarm";
    const string _ButtonSequences = "buttonSequencesModule";
    const string _Calendar = "calendar";
    const string _CheapCheckout = "CheapCheckoutModule";
    const string _Chess = "ChessModule";
    const string _ChordQualities = "ChordQualities";
    const string _ColorDecoding = "Color Decoding";
    const string _ColoredSquares = "ColoredSquaresModule";
    const string _ColorMorse = "ColorMorseModule";
    const string _Coordinates = "CoordinatesModule";
    const string _Crackbox = "CrackboxModule";
    const string _Creation = "CreationModule";
    const string _DoubleOh = "DoubleOhModule";
    const string _FastMath = "fastMath";
    const string _Functions = "qFunctions";
    const string _Gamepad = "TheGamepadModule";
    const string _GridLock = "GridlockModule";
    const string _LogicalButtons = "logicalButtonsModule";
    const string _Hexamaze = "HexamazeModule";
    const string _HumanResources = "HumanResourcesModule";
    const string _Hunting = "hunting";
    const string _IceCream = "iceCreamModule";
    const string _Listening = "Listening";
    const string _LogicGates = "logicGates";
    const string _Mafia = "MafiaModule";
    const string _MaritimeFlags = "MaritimeFlagsModule";
    const string _MonsplodeFight = "monsplodeFight";
    const string _MonsplodeTradingCards = "monsplodeCards";
    const string _Moon = "moon";
    const string _MorseAMaze = "MorseAMaze";
    const string _Morsematics = "MorseV2";
    const string _MouseInTheMaze = "MouseInTheMaze";
    const string _Murder = "murder";
    const string _Neutralization = "neutralization";
    const string _OnlyConnect = "OnlyConnectModule";
    const string _OrientationCube = "OrientationCube";
    const string _PatternCube = "PatternCubeModule";
    const string _PerspectivePegs = "spwizPerspectivePegs";
    const string _PolyhedralMaze = "PolyhedralMazeModule";
    const string _Quintuples = "quintuples";
    const string _Rhythms = "MusicRhythms";
    const string _SeaShells = "SeaShells";
    const string _ShapeShift = "shapeshift";
    const string _SillySlots = "SillySlots";
    const string _SimonSamples = "simonSamples";
    const string _SimonScreams = "SimonScreamsModule";
    const string _SimonSends = "SimonSendsModule";
    const string _SimonSings = "SimonSingsModule";
    const string _SimonStates = "SimonV2";
    const string _SkewedSlots = "SkewedSlotsModule";
    const string _Skyrim = "skyrim";
    const string _SonicTheHedgehog = "sonic";
    const string _Synonyms = "synonyms";
    const string _TapCode = "tapCode";
    const string _TenButtonColorCode = "TenButtonColorCode";
    const string _TicTacToe = "TicTacToeModule";
    const string _Timezone = "timezone";
    const string _TwoBits = "TwoBits";
    const string _UncoloredSquares = "UncoloredSquaresModule";
    const string _VisualImpairment = "visual_impairment";
    const string _Wire = "wire";
    const string _Yahtzee = "YahtzeeModule";

    void Start()
    {
        _moduleId = _moduleIdCounter;
        _moduleIdCounter++;

        _moduleProcessors = new Dictionary<string, Func<KMBombModule, IEnumerable<object>>>()
        {
            { _3DMaze, Process3DMaze },
            { _3DTunnels, Process3DTunnels },
            { _AdventureGame, ProcessAdventureGame },
            { _Algebra, ProcessAlgebra },
            { _BigCircle, ProcessBigCircle },
            { _BinaryLEDs, ProcessBinaryLEDs },
            { _Bitmaps, ProcessBitmaps },
            { _Braille, ProcessBraille },
            { _BrokenButtons, ProcessBrokenButtons },
            { _Bulb, ProcessBulb },
            { _BurglarAlarm, ProcessBurglarAlarm },
            { _ButtonSequences, ProcessButtonSequences },
            { _Calendar, ProcessCalendar },
            { _CheapCheckout, ProcessCheapCheckout },
            { _Chess, ProcessChess },
            { _ChordQualities, ProcessChordQualities },
            { _ColorDecoding, ProcessColorDecoding },
            { _ColoredSquares, ProcessColoredSquares },
            { _ColorMorse, ProcessColorMorse },
            { _Coordinates, ProcessCoordinates },
            { _Crackbox, ProcessCrackbox },
            { _Creation, ProcessCreation },
            { _DoubleOh, ProcessDoubleOh },
            { _FastMath, ProcessFastMath },
            { _Functions, ProcessFunctions },
            { _Gamepad, ProcessGamepad },
            { _GridLock, ProcessGridLock },
            { _LogicalButtons, ProcessLogicalButtons },
            { _Hexamaze, ProcessHexamaze },
            { _HumanResources, ProcessHumanResources },
            { _Hunting, ProcessHunting },
            { _IceCream, ProcessIceCream },
            { _Listening, ProcessListening },
            { _LogicGates, ProcessLogicGates },
            { _Mafia, ProcessMafia },
            { _MaritimeFlags, ProcessMaritimeFlags },
            { _MonsplodeFight, ProcessMonsplodeFight },
            { _MonsplodeTradingCards, ProcessMonsplodeTradingCards },
            { _Moon, ProcessMoon },
            { _MorseAMaze, ProcessMorseAMaze },
            { _Morsematics, ProcessMorsematics },
            { _MouseInTheMaze, ProcessMouseInTheMaze },
            { _Murder, ProcessMurder },
            { _Neutralization, ProcessNeutralization },
            { _OnlyConnect, ProcessOnlyConnect },
            { _OrientationCube, ProcessOrientationCube },
            { _PatternCube, ProcessPatternCube },
            { _PerspectivePegs, ProcessPerspectivePegs },
            { _PolyhedralMaze, ProcessPolyhedralMaze },
            { _Quintuples, ProcessQuintuples },
            { _Rhythms, ProcessRhythms },
            { _SeaShells, ProcessSeaShells },
            { _ShapeShift, ProcessShapeShift },
            { _SillySlots, ProcessSillySlots },
            { _SimonSamples, ProcessSimonSamples },
            { _SimonScreams, ProcessSimonScreams },
            { _SimonSends, ProcessSimonSends },
            { _SimonSings, ProcessSimonSings },
            { _SimonStates, ProcessSimonStates },
            { _SkewedSlots, ProcessSkewedSlots },
            { _Skyrim, ProcessSkyrim },
            { _SonicTheHedgehog, ProcessSonicTheHedgehog },
            { _Synonyms, ProcessSynonyms },
            { _TapCode, ProcessTapCode },
            { _TenButtonColorCode, ProcessTenButtonColorCode },
            { _TicTacToe, ProcessTicTacToe },
            { _Timezone, ProcessTimezone },
            { _TwoBits, ProcessTwoBits },
            { _UncoloredSquares, ProcessUncoloredSquares },
            { _VisualImpairment, ProcessVisualImpairment },
            { _Wire, ProcessWire },
            { _Yahtzee, ProcessYahtzee }
        };

        Bomb.OnBombExploded += delegate { _exploded = true; StopAllCoroutines(); };
        Bomb.OnBombSolved += delegate
        {
            // This delegate gets invoked when _any_ bomb in the room is solved,
            // so we need to check if the bomb this module is on is actually solved
            if (Bomb.GetSolvedModuleNames().Count == Bomb.GetSolvableModuleNames().Count)
                StopAllCoroutines();
        };

        _attributes = typeof(Question).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => Ut.KeyValuePair((Question) f.GetValue(null), f.GetCustomAttribute<SouvenirQuestionAttribute>()))
            .Where(kvp => kvp.Value != null)
            .ToDictionary();

        var origRotation = SurfaceRenderer.transform.rotation;
        SurfaceRenderer.transform.eulerAngles = new Vector3(0, 180, 0);
        _surfaceSizeFactor = SurfaceRenderer.bounds.size.x / (2 * .834) * .9;
        SurfaceRenderer.transform.rotation = origRotation;

        disappear();
        SetWordWrappedText(Ut.NewArray(
            "I see dead defusers.",     // “I see dead people”, (Sixth Sense)
            "Welcome... to the real bomb.",     // “Welcome... to the real world.” (The Matrix)
            "I’m gonna make him a bomb he can’t defuse.",   // “I’m gonna make him an offer he can’t refuse.” (The Godfather)
            "Defuse it again, Sam.",    // “Play it again, Sam.” (Casablanca) (misquote)
            "Louis, I think this is the beginning of a beautiful explosion.",   // “Louis, I think this is the beginning of a beautiful friendship.” (Casablanca)
            "Here’s looking at you, defuser.",  // “Here’s looking at you, kid.” (Casablanca)
            "Hey. I could defuse this bomb in ten seconds flat.",   // “Hey. I could clear the sky in ten seconds flat.” (MLP:FiM, Friendship is Magic - Part 1)
            "Go ahead, solve my bomb.", // “Go ahead, make my day.” (Sudden Impact / Dirty Harry series)
            "May the bomb be with you.",    // “May the Force be with you.” (Star Wars IV: A New Hope)
            "I love the smell of explosions in the morning.",   // “I love the smell of napalm in the morning.” (Apocalypse Now)
            "Blowing up means never having to say you’re sorry.",   // “Love means never having to say you're sorry.” (Love Story)
            "The stuff that bombs are made of.",    // “The Stuff That Dreams Are Made Of” (“Coming Around Again” album by Carly Simon)
            "E.T. defuse bomb.",    // “E.T. phone home.” (E.T. the Extra-Terrestrial)
            "Bomb. James Bomb.",    // “Bond. James Bond.” (Dr. No / James Bond series)
            "You can’t handle the bomb!",   // “You can’t handle the truth!” (A Few Good Men)
            "Blow up the usual suspects.",  // “Round up the usual suspects.” (Casablanca)
            "You’re gonna need a bigger bomb.", // “You’re gonna need a bigger boat.” (Jaws)
            "Bombs are like a box of chocolates. You never know what you’re gonna get.",    // “My mom always said life was like a box of chocolates. You never know what you're gonna get.” (Forrest Gump)
            "Houston, we have a module.",   // “Houston, we have a problem.” (Apollo 13)
            "Elementary, my dear expert.",  // “Elementary, my dear Watson.” (Sherlock Holmes) (misquote)
            "Forget it, Jake, it’s KTANE.",     // “Forget it, Jake, it’s Chinatown.” (Chinatown)
            "I have always depended on the fitness of experts.",    // “I’ve always depended on the kindness of strangers.” (A Streetcar Named Desire)
            "A bomb. Exploded, not defused.",   // “A Martini. Shaken, not stirred.” (Diamonds Are Forever (novel) / James Bond)
            "I’m the king of the bomb!",    // “I’m the king of the world!” (Titanic)
            "Blow me up, Scotty.",  // “Beam me up, Scotty!” (Star Trek misquote)
            "Yabba dabba boom!",    // “Yabba dabba doo!” (Flintstones)
            "This bomb will self-destruct in five seconds.",    // “This tape will self-destruct in five seconds.” (Mission: Impossible)
            "Defusing is futile.",  // “Resistance is futile.” (Star Trek: The Next Generation)
            "Is that your final answer?",   // direct quote (Who Wants to be a Millionaire?)
            "A bomb’s best friend is his defuser.", // “A man’s best friend is his dog.” (attorney George Graham Vest, 1870 Warrensburg)
            "Keep your experts close, but your bomb closer.",   // “Keep your friends close and your enemies closer.” (The Prince / Machiavelli)
            "Fasten your seatbelts. It’s going to be a bomby night.",   // “Fasten your seat belts, it’s going to be a bumpy night.” (All About Eve)
            "Show me the modules!", // “Show me the money!” (Jerry Maguire)
            "We’ll always have batteries.", // “We’ll always have Paris.” (Casablanca)
            "Say hello to my little bomb.", // “Say hello to my little friend!” (Scarface)
            "You’re a defuser, Harry.", // “You’re a wizard, Harry.” (Harry Potter and the Philosopher’s Stone)
            "I’m sorry, Dave. I’m afraid I can’t defuse that.", // “I’m sorry, Dave. I’m afraid I can’t do that.” (2001: A Space Odyssey)
            "You either die a defuser, or you live long enough to see yourself become the expert.", // “Well, I guess you either die a hero or you live long enough to see yourself become the villain.” (The Dark Knight)
            "This isn’t defusing. This is exploding... with style.",    // “This isn’t flying. This is falling... with style.” (Toy Story)
            "Could you describe the module, sir?",  // “Could you describe the ruckus, sir?” (The Breakfast Club)
            "You want widgets? I got twenty.",  // “You want thingamabobs? I got twenty.” (The Little Mermaid)
            "We don’t need no stinking widgets.",   // “We don’t need no stinking badges!” (The Treasure of the Sierra Madre)
            "Say edgework one more goddamn time.",  // “Say what one more goddamn time.” (Pulp Fiction)
            "How do you like them modules?",    // “How do you like them apples?” (Good Will Hunting)
            "Introducing: The Double... Decker... Bomb!",   // “Introducing: The Double... Decker... Couch!” (The LEGO Movie)
            "Have you got your wires crossed?", // “Have you got your lions crossed?” (The Lion King)
            "Don't cross the wires.",   // “Don’t cross the streams.” (Ghostbusters)
            "Wanna hear the most annoying explosion in the world?", // “Wanna hear the most annoying sound in the world?” (Dumb & Dumber)
            "Manuals? Where we’re going, we don’t need manuals.",   // “Roads? Where we’re going, we don’t need roads.” (Back to the Future)
            "On a long enough time line, the survival rate for everyone will drop to zero.", // direct quote (Fight Club (novel))
            "This is your bomb, and it's ending one minute at a time.", // “This is your life and it’s ending one minute at a time.” (Fight Club)
            "The first rule of defusal is, you keep talking about defusal.",    // “The first rule of Fight Club is, you don’t talk about Fight Club.” (Fight Club)
            "Well, here's another nice mess you’ve gotten me into!",     // direct quote (Sons of the Desert / Oliver Hardy)
            "You know how to defuse, don’t you, Steve? You just put your wires together and cut.",  // “You know how to whistle, don't you Steve? You just put your lips together, and blow.” (To Have And Have Not)
            "Mrs. Defuser, you’re trying to disarm me. Aren’t you?",    // “Mrs. Robinson, you’re trying to seduce me. Aren’t you?” (The Graduate)
            "We defuse bombs.",  // “We rob banks.” (Bonnie and Clyde)
            "Somebody set up us the bomb."  // direct quote (Zero Wing)
        ).PickRandom(), 1.75);

        if (transform.parent != null)
        {
            if (_isTimwisComputer)
                lock (_timwiPath)
                    File.WriteAllText(_timwiPath, "");

            for (int i = 0; i < transform.parent.childCount; i++)
            {
                var module = transform.parent.GetChild(i).gameObject.GetComponent<KMBombModule>();
                if (module != null)
                    StartCoroutine(ProcessModule(module));
            }
        }

        _isActivated = false;
        Module.OnActivate += delegate
        {
            _isActivated = true;
            if (Application.isEditor)
            {
                // Testing in Unity
                Debug.LogFormat("<Souvenir #{0}> Entering Unity testing mode.", _moduleId);
                var questions = Ut.GetEnumValues<Question>();
                var curQuestion = 0;
                var curOrd = 0;
                var curExample = 0;
                Action showQuestion = () =>
                {
                    SouvenirQuestionAttribute attr;
                    if (!_attributes.TryGetValue(questions[curQuestion], out attr))
                    {
                        Debug.LogFormat("<Souvenir #{1}> Error: Question {0} has no attribute.", questions[curQuestion], _moduleId);
                        return;
                    }
                    if (attr.ExampleExtraFormatArguments != null && attr.ExampleExtraFormatArguments.Length > 0 && attr.ExampleExtraFormatArgumentGroupSize > 0)
                    {
                        var numExamples = attr.ExampleExtraFormatArguments.Length / attr.ExampleExtraFormatArgumentGroupSize;
                        curExample = (curExample % numExamples + numExamples) % numExamples;
                    }
                    var fmt = new object[attr.ExampleExtraFormatArgumentGroupSize + 1];
                    fmt[0] = curOrd == 0 ? attr.AddThe ? "The\u00a0" + attr.ModuleName : attr.ModuleName : string.Format("the {0} you solved {1}", attr.ModuleName, ordinal(curOrd));
                    for (int i = 0; i < attr.ExampleExtraFormatArgumentGroupSize; i++)
                        fmt[i + 1] = attr.ExampleExtraFormatArguments[curExample * attr.ExampleExtraFormatArgumentGroupSize + i];
                    try
                    {
                        SetQuestion(new QandA(string.Format(attr.QuestionText, fmt), (attr.AllAnswers ?? attr.ExampleAnswers).ToList().Shuffle().Take(attr.NumAnswers).ToArray(), Rnd.Range(0, attr.NumAnswers), font(attr.Font), fontTexture(attr.Font)));
                    }
                    catch (FormatException e)
                    {
                        Debug.LogFormat("<Souvenir #{3}> FormatException {0}\nQuestionText={1}\nfmt=[{2}]", e.Message, attr.QuestionText, fmt.JoinString(", ", "\"", "\""), _moduleId);
                    }
                };
                showQuestion();

                setAnswerHandler(0, _ =>
                {
                    curQuestion = (curQuestion + questions.Length - 1) % questions.Length;
                    curExample = 0;
                    curOrd = 0;
                    showQuestion();
                });
                setAnswerHandler(1, _ =>
                {
                    curQuestion = (curQuestion + 1) % questions.Length;
                    curExample = 0;
                    curOrd = 0;
                    showQuestion();
                });
                setAnswerHandler(2, _ => { if (curOrd > 0) curOrd--; showQuestion(); });
                setAnswerHandler(3, _ => { curOrd++; showQuestion(); });
                setAnswerHandler(4, _ => { curExample--; showQuestion(); });
                setAnswerHandler(5, _ => { curExample++; showQuestion(); });
            }
            else
            {
                // Playing for real
                for (int i = 0; i < 6; i++)
                    setAnswerHandler(i, HandleAnswer);
                disappear();
                StartCoroutine(Play());
            }
        };

        //var sph = SurfaceRenderer.transform.Find("Sphere");
        //for (int i = 0; i < _acceptableWidths.Length; i++)
        //{
        //    var s = Instantiate(sph);
        //    s.parent = sph.parent;
        //    s.localPosition = new Vector3((float) -_acceptableWidths[i][1] / 10 + .0834f, .0101f, (float) _acceptableWidths[i][0] / 10 - .0834f);
        //    s.localScale = new Vector3(.01f, .01f, .01f);
        //    s.localRotation = Quaternion.identity;
        //}
        //Destroy(sph.gameObject);
    }

    private Font font(AnswerFont font)
    {
        switch (font)
        {
            case AnswerFont.SymbolsFont:
                return FontSymbols;
        }
        return FontDefault;
    }

    private Texture fontTexture(AnswerFont font)
    {
        switch (font)
        {
            case AnswerFont.SymbolsFont:
                return FontSymbolsTexture;
        }
        return FontDefaultTexture;
    }

    void setAnswerHandler(int index, Action<int> handler)
    {
        Answers6[index].OnInteract = delegate
        {
            Answers6[index].AddInteractionPunch();
            handler(index);
            return false;
        };
        Answers4[index].OnInteract = delegate
        {
            Answers4[index].AddInteractionPunch();
            handler(index);
            return false;
        };
    }

    private void disappear()
    {
        TextMesh.gameObject.SetActive(false);
        Answers4Parent.SetActive(false);
        Answers6Parent.SetActive(false);
    }

    private void HandleAnswer(int index)
    {
        if (_animating || _isSolved)
            return;

        if (_currentQuestion == null || index >= _currentQuestion.Answers.Length)
            return;

        Debug.LogFormat("[Souvenir #{0}] Clicked answer #{1} ({2}). {3}.", _moduleId, index + 1, _currentQuestion.Answers[index], _currentQuestion.CorrectIndex == index ? "Correct" : "Wrong");

        if (_currentQuestion.CorrectIndex == index)
        {
            StartCoroutine(CorrectAnswer());
        }
        else
        {
            Module.HandleStrike();
            if (!_exploded)
            {
                // Blink the correct answer, then move on to the next question
                _animating = true;
                StartCoroutine(revealThenMoveOn());
            }
        }
    }

    private IEnumerator CorrectAnswer()
    {
        _animating = true;
        Audio.PlaySoundAtTransform("Answer", transform);
        dismissQuestion();
        yield return new WaitForSeconds(.5f);
        _animating = false;
    }

    private void dismissQuestion()
    {
        _currentQuestion = null;
        disappear();
    }

    private IEnumerator revealThenMoveOn()
    {
        yield return new WaitForSeconds(.3f);

        var on = false;
        var answ = Answers4Parent.activeSelf ? Answers4 : Answers6;
        var textMesh = answ[_currentQuestion.CorrectIndex].transform.Find("AnswerText").GetComponent<TextMesh>();
        var text = textMesh.text;
        for (int i = 0; i < 15; i++)
        {
            textMesh.text = on ? text : "";
            on = !on;
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(.3f);

        dismissQuestion();
        _animating = false;
    }

    private IEnumerator Play()
    {
        if (TwitchPlaysActive)
            ActivateTwitchPlaysNumbers();

        var numPlayableModules = Bomb.GetSolvableModuleNames().Count(x => !_ignoredModules.Contains(x));

        while (true)
        {
            var numSolved = Bomb.GetSolvedModuleNames().Count(x => !_ignoredModules.Contains(x));
            if (_questions.Count == 0 && (numSolved >= numPlayableModules || _coroutinesActive == 0))
            {
                // Very rare case: another coroutine could still be waiting to detect that a module is solved and then add another question to the queue
                yield return new WaitForSeconds(.1f);

                // If still no new questions, all supported modules are solved and we’re done. (Or maybe a coroutine is stuck in a loop, but then it’s bugged and we need to cancel it anyway.)
                if (_questions.Count == 0)
                    break;
            }

            IEnumerable<QuestionBatch> eligible = _questions;

            // If we reached the end of the bomb, everything is eligible.
            if (numSolved < numPlayableModules)
                // Otherwise, make sure there has been another solved module since
                eligible = eligible.Where(e => e.NumSolved < numSolved);

            var numEligibles = eligible.Count();

            if ((numSolved < numPlayableModules && numEligibles < 3) || numEligibles == 0)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }

            var batch = eligible.PickRandom();
            _questions.Remove(batch);
            if (batch.Questions.Length == 0)
                continue;

            SetQuestion(batch.Questions.PickRandom());
            while (_currentQuestion != null || _animating)
                yield return new WaitForSeconds(.5f);
        }

        Debug.LogFormat("[Souvenir #{0}] Questions exhausted. Module solved.", _moduleId);
        _isSolved = true;
        Module.HandlePass();
    }

    private void ActivateTwitchPlaysNumbers()
    {
        Answers4Parent.transform.localPosition = new Vector3(.005f, 0, 0);
        Answers6Parent.transform.localPosition = new Vector3(.005f, 0, 0);
        foreach (var gobj in TpNumbers)
            gobj.SetActive(true);
    }

    private void SetQuestion(QandA q)
    {
        Debug.LogFormat("[Souvenir #{0}] Asking question: {1}", _moduleId, q.DebugString);
        _currentQuestion = q;
        SetWordWrappedText(q.QuestionText);
        ShowAnswers(q.Answers, q.Font, q.FontTexture);
        Audio.PlaySoundAtTransform("Question", transform);
    }

    private static readonly double[][] _acceptableWidths = Ut.NewArray(
        // First value is y (vertical text advancement), second value is width of the Surface mesh at this y
        new[] { 0.834 - 0.834, 0.834 + 0.3556 },
        new[] { 0.834 - 0.7628, 0.834 + 0.424 },
        new[] { 0.834 - 0.6864, 0.834 + 0.424 },
        new[] { 0.834 - 0.528, 0.834 + 0.5102 },
        new[] { 0.834 - 0.4452, 0.834 + 0.6618 },
        new[] { 0.834 - 0.4452, 0.834 + 0.7745 },
        new[] { 0.834 - 0.391, 0.834 + 0.834 }
    );

    private void SetWordWrappedText(string text, double desiredHeightFactor = 1.1)
    {
        var low = 1;
        var high = 256;
        var desiredHeight = desiredHeightFactor * _surfaceSizeFactor;
        var wrappeds = new Dictionary<int, string>();
        var origRotation = TextMesh.transform.rotation;
        TextMesh.transform.eulerAngles = new Vector3(90, 0, 0);

        while (high - low > 1)
        {
            var mid = (low + high) / 2;
            TextMesh.fontSize = mid;

            TextMesh.text = "\u00a0";
            var size = TextRenderer.bounds.size;
            var widthOfASpace = size.x;
            var heightOfALine = size.z;
            var wrapWidths = new List<double>();

            var wrappedSB = new StringBuilder();
            var first = true;
            foreach (var line in Ut.WordWrap(
                text,
                line =>
                {
                    var y = line * heightOfALine / _surfaceSizeFactor;
                    if (line < wrapWidths.Count)
                        return wrapWidths[line];
                    while (wrapWidths.Count < line)
                        wrapWidths.Add(0);
                    var i = 1;
                    while (i < _acceptableWidths.Length && _acceptableWidths[i][0] < y)
                        i++;
                    if (i == _acceptableWidths.Length)
                        wrapWidths.Add(_acceptableWidths[i - 1][1] * _surfaceSizeFactor);
                    else
                    {
                        var lambda = (y - _acceptableWidths[i - 1][0]) / (_acceptableWidths[i][0] - _acceptableWidths[i - 1][0]);
                        wrapWidths.Add((_acceptableWidths[i - 1][1] * (1 - lambda) + _acceptableWidths[i][1] * lambda) * _surfaceSizeFactor);
                    }

                    return wrapWidths[line];
                },
                widthOfASpace,
                str =>
                {
                    TextMesh.text = str;
                    return TextRenderer.bounds.size.x;
                },
                allowBreakingWordsApart: false
            ))
            {
                if (line == null)
                {
                    // There was a word that was too long to fit into a line.
                    high = mid;
                    wrappedSB = null;
                    break;
                }
                if (!first)
                    wrappedSB.Append('\n');
                first = false;
                wrappedSB.Append(line);
            }

            if (wrappedSB != null)
            {
                var wrapped = wrappedSB.ToString();
                wrappeds[mid] = wrapped;
                TextMesh.text = wrapped;
                size = TextRenderer.bounds.size;
                if (size.z > desiredHeight)
                    high = mid;
                else
                    low = mid;
            }
        }

        TextMesh.fontSize = low;
        TextMesh.text = wrappeds[low];
        TextMesh.transform.rotation = origRotation;
        TextMesh.gameObject.SetActive(true);
    }

    void ShowAnswers(string[] answers, Font font, Texture fontTexture)
    {
        if (answers == null || answers.Length == 0 || answers.Length > 6)
        {
            Debug.LogFormat("<Souvenir #{2}> Something went wrong setting answers. length={0}, answers=[{1}]", answers == null ? "null" : answers.Length.ToString(), answers == null ? "null" : answers.JoinString(), _moduleId);
            Module.HandlePass();
            _isSolved = true;
            disappear();
            TextMesh.gameObject.SetActive(true);
            SetWordWrappedText("Error.");
            return;
        }

        var btns = answers.Length > 4 ? Answers6 : Answers4;

        Answers4Parent.SetActive(answers.Length <= 4);
        Answers6Parent.SetActive(answers.Length > 4);

        var children = new KMSelectable[6];
        for (int i = 0; i < btns.Length; i++)
        {
            var mesh = btns[i].transform.Find("AnswerText").GetComponent<TextMesh>();

            mesh.text = i < answers.Length ? answers[i] : "•";
            mesh.font = font ?? FontDefault;
            mesh.GetComponent<MeshRenderer>().material = FontMaterial;
            mesh.GetComponent<MeshRenderer>().material.mainTexture = fontTexture ?? FontDefaultTexture;
            btns[i].gameObject.SetActive(Application.isEditor || i < answers.Length);
            children[3 * (i % 2) + (i / 2)] = Application.isEditor || i < answers.Length ? btns[i] : null;

            var origRotation = mesh.transform.localRotation;
            mesh.transform.eulerAngles = new Vector3(90, 0, 0);
            mesh.transform.localScale = new Vector3(1, 1, 1);
            var bounds = mesh.GetComponent<Renderer>().bounds.size;
            var fac = (answers.Length > 4 ? .45 : .7);
            if (bounds.x > fac * _surfaceSizeFactor)
                // Adjust width of answer so that it fits horizontally
                mesh.transform.localScale = new Vector3((float) (fac * _surfaceSizeFactor / bounds.x), 1, 1);
            mesh.transform.localRotation = origRotation;
        }

        MainSelectable.Children = children;
        MainSelectable.UpdateChildren();
    }

    sealed class FieldInfo<T>
    {
        private readonly object _target;
        private readonly int _souvenirID;
        public readonly FieldInfo Field;

        public FieldInfo(object target, FieldInfo field, int souvenirID)
        {
            _target = target;
            Field = field;
            _souvenirID = souvenirID;
        }

        public T Get(bool nullAllowed = false)
        {
            var t = (T) Field.GetValue(_target);
            if (!nullAllowed && t == null)
                Debug.LogFormat("<Souvenir #{2}> Field {1}.{0} is null.", Field.Name, Field.DeclaringType.FullName, _souvenirID);
            return t;
        }

        public T GetFrom(object obj, bool nullAllowed = false)
        {
            var t = (T) Field.GetValue(obj);
            if (!nullAllowed && t == null)
                Debug.LogFormat("<Souvenir #{2}> Field {1}.{0} is null.", Field.Name, Field.DeclaringType.FullName, _souvenirID);
            return t;
        }

        public void Set(T value) { Field.SetValue(_target, value); }
    }

    sealed class MethodInfo<T>
    {
        private object _target;
        public MethodInfo Method { get; private set; }

        public MethodInfo(object target, MethodInfo method)
        {
            _target = target;
            Method = method;
        }

        public T Invoke(params object[] arguments)
        {
            return (T) Method.Invoke(_target, arguments);
        }
    }

    private Component GetComponent(KMBombModule module, string name)
    {
        return GetComponent(module.gameObject, name);
    }
    private Component GetComponent(GameObject module, string name)
    {
        var comp = module.GetComponent(name);
        if (comp == null)
        {
            Debug.LogFormat("<Souvenir #{2}> {0} game object has no {1} component. Components are: {3}", module.name, name, _moduleId, module.GetComponents(typeof(Component)).Select(c => c.GetType().FullName).JoinString(", "));
            return null;
        }
        return comp;
    }

    private FieldInfo<T> GetField<T>(object target, string name, bool isPublic = false)
    {
        if (target == null)
        {
            Debug.LogFormat("<Souvenir #{3}> Attempt to get {1} field {0} of type {2} from a null object.", name, isPublic ? "public" : "non-public", typeof(T).FullName, _moduleId);
            return null;
        }
        return GetFieldImpl<T>(target, target.GetType(), name, isPublic, BindingFlags.Instance);
    }

    private FieldInfo<T> GetStaticField<T>(Type targetType, string name, bool isPublic = false)
    {
        if (targetType == null)
        {
            Debug.LogFormat("<Souvenir #{0}> Attempt to get {1} static field {2} of type {3} from a null type.", _moduleId, isPublic ? "public" : "non-public", name, typeof(T).FullName);
            return null;
        }
        return GetFieldImpl<T>(null, targetType, name, isPublic, BindingFlags.Static);
    }

    private FieldInfo<T> GetFieldImpl<T>(object target, Type targetType, string name, bool isPublic, BindingFlags bindingFlags)
    {
        var fld = targetType.GetField(name, (isPublic ? BindingFlags.Public : BindingFlags.NonPublic) | bindingFlags);
        if (fld == null)
        {
            // In case it’s actually an auto-implemented property and not a field.
            fld = targetType.GetField("<" + name + ">k__BackingField", BindingFlags.NonPublic | bindingFlags);
            if (fld == null)
            {
                Debug.LogFormat("<Souvenir #{3}> Type {0} does not contain {1} field {2}. Fields are: {4}", targetType, isPublic ? "public" : "non-public", name, _moduleId,
                    targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Select(f => string.Format("{0} {1} {2}", f.IsPublic ? "public" : "private", f.FieldType.FullName, f.Name)).JoinString(", "));
                return null;
            }
        }
        if (!typeof(T).IsAssignableFrom(fld.FieldType))
        {
            Debug.LogFormat("<Souvenir #{5}> Type {0} has {1} field {2} of type {3} but expected type {4}.", targetType, isPublic ? "public" : "non-public", name, fld.FieldType.FullName, typeof(T).FullName, _moduleId);
            return null;
        }
        return new FieldInfo<T>(target, fld, _moduleId);
    }

    private MethodInfo<T> GetMethod<T>(object target, string name, int numParameters, bool isPublic = false)
    {
        if (target == null)
        {
            Debug.LogFormat("<Souvenir #{3}> Attempt to get {1} method {0} of return type {2} from a null object.", name, isPublic ? "public" : "non-public", typeof(T).FullName, _moduleId);
            return null;
        }
        var bindingFlags = (isPublic ? BindingFlags.Public : BindingFlags.NonPublic) | BindingFlags.Instance;
        var targetType = target.GetType();
        var mths = targetType.GetMethods(bindingFlags).Where(m => m.Name == name && m.GetParameters().Length == numParameters && typeof(T).IsAssignableFrom(m.ReturnType)).Take(2).ToArray();
        if (mths.Length == 0)
        {
            Debug.LogFormat("<Souvenir #{5}> Type {0} does not contain {1} method {2} with return type {3} and {4} parameters.", targetType, isPublic ? "public" : "non-public", name, typeof(T).FullName, numParameters, _moduleId);
            return null;
        }
        if (mths.Length > 1)
        {
            Debug.LogFormat("<Souvenir #{5}> Type {0} contains multiple {1} methods {2} with return type {3} and {4} parameters.", targetType, isPublic ? "public" : "non-public", name, typeof(T).FullName, numParameters, _moduleId);
            return null;
        }
        return new MethodInfo<T>(target, mths[0]);
    }

    private IEnumerator ProcessModule(KMBombModule module)
    {
        _coroutinesActive++;
        var moduleType = module.ModuleType;
        Debug.LogFormat("<Souvenir #{1}> Start processing {0}.", moduleType, _moduleId);
        _moduleCounts.IncSafe(moduleType);
        var iterator = _moduleProcessors.Get(moduleType, null);

        if (iterator != null)
        {
            foreach (var obj in iterator(module))
            {
                yield return obj;
                if (TwitchAbandonModule.Contains(module))
                {
                    Debug.LogFormat("<Souvenir #{0}> Abandoning {1} because Twitch Plays told me to.", _moduleId, module.ModuleDisplayName);
                    yield break;
                }
            }
            if (!_legitimatelyNoQuestions.Contains(module) && !_questions.Any(q => q.Module == module))
                Debug.LogFormat("[Souvenir #{0}] There was no question generated for {1}. Please report this to Timwi as this may indicate a bug in the module. Remember to send him this logfile.", _moduleId, module.ModuleDisplayName);
        }
        else if (_isTimwisComputer)
        {
            var s = new StringBuilder();
            s.AppendLine("Unrecognized module: " + module.name + ", KMBombModule.ModuleType: " + moduleType);
            foreach (var comp in module.GetComponents(typeof(UnityEngine.Object)))
                s.AppendLine("    - " + (comp == null ? "<null>" : comp.GetType().FullName));
            lock (_timwiPath)
                File.AppendAllText(_timwiPath, s.ToString());
        }

        Debug.LogFormat("<Souvenir #{1}> Finished processing {0}.", moduleType, _moduleId);
        _coroutinesActive--;
    }

    private IEnumerable<object> Process3DMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "ThreeDMazeModule");
        var fldMap = GetField<object>(comp, "map");
        var fldIsComplete = GetField<bool>(comp, "isComplete");
        if (comp == null || fldMap == null || fldIsComplete == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var map = fldMap.Get();
        if (map == null)
            yield break;
        var fldMapData = GetField<Array>(map, "mapData");
        if (fldMapData == null)
            yield break;
        var mapData = fldMapData.Get();
        if (mapData == null)
            yield break;
        if (mapData.GetLength(0) != 8 || mapData.GetLength(1) != 8)
        {
            Debug.LogFormat("<Souvenir #{2}> 3D maze wrong size ({0},{1}, expected 8,8).", mapData.GetLength(0), mapData.GetLength(1), _moduleId);
            yield break;
        }
        var fldLabel = GetField<char>(mapData.GetValue(0, 0), "label", isPublic: true);
        if (fldLabel == null)
            yield break;
        var chars = new HashSet<char>();
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
            {
                var ch = fldLabel.GetFrom(mapData.GetValue(i, j));
                if ("ABCDH".Contains(ch))
                    chars.Add(ch);
            }
        var correctMarkings = chars.OrderBy(c => c).JoinString();

        char bearing;
        if (correctMarkings == "ABC") bearing = fldLabel.GetFrom(mapData.GetValue(1, 1));
        else if (correctMarkings == "ABD") bearing = fldLabel.GetFrom(mapData.GetValue(7, 0));
        else if (correctMarkings == "ABH") bearing = fldLabel.GetFrom(mapData.GetValue(0, 1));
        else if (correctMarkings == "ACD") bearing = fldLabel.GetFrom(mapData.GetValue(1, 2));
        else if (correctMarkings == "ACH") bearing = fldLabel.GetFrom(mapData.GetValue(0, 1));
        else if (correctMarkings == "ADH") bearing = fldLabel.GetFrom(mapData.GetValue(5, 0));
        else if (correctMarkings == "BCD") bearing = fldLabel.GetFrom(mapData.GetValue(6, 1));
        else if (correctMarkings == "BCH") bearing = fldLabel.GetFrom(mapData.GetValue(2, 2));
        else if (correctMarkings == "BDH") bearing = fldLabel.GetFrom(mapData.GetValue(3, 1));
        else if (correctMarkings == "CDH") bearing = fldLabel.GetFrom(mapData.GetValue(5, 1));
        else
        {
            Debug.LogFormat(@"<Souvenir #{1}> Abandoning 3D Maze because unexpected markings: ""{0}"".", correctMarkings, _moduleId);
            yield break;
        }

        if (!"NSWE".Contains(bearing))
        {
            Debug.LogFormat("<Souvenir #{1}> Abandoning 3D Maze because unexpected bearing: '{0}'.", bearing, _moduleId);
            yield break;
        }

        while (!fldIsComplete.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_3DMaze);
        addQuestions(module,
            makeQuestion(Question._3DMazeMarkings, _3DMaze, new[] { correctMarkings }),
            makeQuestion(Question._3DMazeBearing, _3DMaze, new[] { bearing == 'N' ? "North" : bearing == 'S' ? "South" : bearing == 'W' ? "West" : "East" }));
    }

    private IEnumerable<object> Process3DTunnels(KMBombModule module)
    {
        var comp = GetComponent(module, "ThreeDTunnels");
        var fldSymbols = comp == null ? null : GetStaticField<string>(comp.GetType(), "_symbols");
        var fldTargetNodes = GetField<List<int>>(comp, "_targetNodes");
        var fldSolved = GetField<bool>(comp, "_solved");

        if (comp == null || fldSymbols == null || fldTargetNodes == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_3DTunnels);

        var symbols = fldSymbols.Get();
        var targetNodes = fldTargetNodes.Get();
        if (symbols == null || targetNodes == null || targetNodes.Any(tn => tn < 0 || tn >= symbols.Length))
        {
            Debug.LogFormat("<Souvenir #{0}> 3D Tunnels: invalid values: symbols={1}, targetNodes={2}",
                _moduleId,
                symbols ?? "<null>",
                targetNodes == null ? "null" : string.Format("[{0}]", targetNodes.JoinString(", ")));
            yield break;
        }

        var targetNodeNames = targetNodes.Select(tn => symbols[tn].ToString()).ToArray();
        addQuestions(module, targetNodeNames.Select((tn, ix) => makeQuestion(Question._3DTunnelsTargetNode, _3DTunnels, new[] { tn }, new[] { ordinal(ix + 1) }, targetNodeNames)));
    }

    private IEnumerable<object> ProcessAdventureGame(KMBombModule module)
    {
        var comp = GetComponent(module, "AdventureGameModule");
        var fldButtonUse = GetField<KMSelectable>(comp, "ButtonUse", isPublic: true);
        var fldInvValues = GetField<IList>(comp, "InvValues"); // actually List<AdventureGameModule.ITEM>
        var fldInvWeaponCount = GetField<int>(comp, "InvWeaponCount");
        var fldSelectedItem = GetField<int>(comp, "SelectedItem");
        var fldSelectedEnemy = GetField<object>(comp, "SelectedEnemy");
        var fldTextEnemy = GetField<TextMesh>(comp, "TextEnemy", isPublic: true);
        var fldNumWeapons = GetField<int>(comp, "NumWeapons");
        var mthItemName = GetMethod<string>(comp, "ItemName", 1);
        var mthShouldUseItem = GetMethod<bool>(comp, "ShouldUseItem", 1);

        if (comp == null || fldButtonUse == null || fldInvValues == null || fldInvWeaponCount == null || fldSelectedItem == null || fldSelectedEnemy == null || fldTextEnemy == null || fldNumWeapons == null || mthItemName == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var invValues = fldInvValues.Get();
        var buttonUse = fldButtonUse.Get();
        if (invValues == null || buttonUse == null)
            yield break;

        var enemy = fldSelectedEnemy.Get();
        var textEnemy = fldTextEnemy.Get();
        if (enemy == null || textEnemy == null)
            yield break;

        var invWeaponCount = fldInvWeaponCount.Get();
        var numWeapons = fldNumWeapons.Get();
        if (invWeaponCount == 0 || numWeapons == 0)
        {
            Debug.LogFormat("<Souvenir #{2}> {0} field {1} is 0 (zero).", comp.GetType().FullName, invWeaponCount == 0 ? fldInvWeaponCount.Field.Name : fldNumWeapons.Field.Name, _moduleId);
            yield break;
        }

        var prevInteract = buttonUse.OnInteract;
        if (prevInteract == null)
        {
            Debug.LogFormat("<Souvenir #{0}> Adventure Game: ButtonUse.OnInteract is null.", _moduleId);
            yield break;
        }

        var origInvValues = new List<int>(invValues.Cast<int>());
        var correctItemsUsed = 0;
        var qs = new List<Func<QandA>>();
        var solved = false;

        buttonUse.OnInteract = delegate
        {
            var selectedItem = fldSelectedItem.Get();
            var itemUsed = origInvValues[selectedItem];
            var shouldUse = mthShouldUseItem.Invoke(selectedItem);
            for (int j = invWeaponCount; j < invValues.Count; j++)
                shouldUse &= !mthShouldUseItem.Invoke(j);

            var ret = prevInteract();

            if (invValues.Count != origInvValues.Count)
            {
                // If the length of the inventory has changed, the user used a correct non-weapon item.
                var itemIndex = ++correctItemsUsed;
                qs.Add(() => makeQuestion(Question.AdventureGameCorrectItem, _AdventureGame, new[] { titleCase(mthItemName.Invoke(itemUsed)) }, new[] { ordinal(itemIndex) }));
                origInvValues.Clear();
                origInvValues.AddRange(invValues.Cast<int>());
            }
            else if (shouldUse)
            {
                // The user solved the module.
                solved = true;
                textEnemy.text = "Victory!";
            }

            return ret;
        };

        while (!solved)
            yield return new WaitForSeconds(.1f);

        buttonUse.OnInteract = prevInteract;
        _modulesSolved.IncSafe(_AdventureGame);
        var enemyName = enemy.ToString();
        enemyName = enemyName.Substring(0, 1).ToUpperInvariant() + enemyName.Substring(1).ToLowerInvariant();
        addQuestions(module, qs.Select(q => q()).Concat(new[] { makeQuestion(Question.AdventureGameEnemy, _AdventureGame, new[] { enemyName }) }));
    }

    private IEnumerable<object> ProcessAlgebra(KMBombModule module)
    {
        var comp = GetComponent(module, "algebraScript");
        var fldEquations = Enumerable.Range(1, 3).Select(i => GetField<Texture>(comp, string.Format("level{0}Equation", i))).ToArray();
        var fldStage = GetField<int>(comp, "stage");

        if (comp == null || fldEquations.Any(f => f == null))
            yield break;

        while (fldStage.Get() <= 3)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Algebra);

        var textures = fldEquations.Select(f => f.Get());
        if (textures.Any(t => t == null))
        {
            Debug.LogFormat("<Souvenir #{0}> Algebra: texture #{1} is null.", _moduleId, textures.IndexOf(t => t == null) + 1);
            yield break;
        }

        addQuestions(module, textures.Take(2).Select((t, ix) => makeQuestion(ix == 0 ? Question.AlgebraEquation1 : Question.AlgebraEquation2, _Algebra, new[] { t.name.Replace(';', '/') })));
    }

    private IEnumerable<object> ProcessBigCircle(KMBombModule module)
    {
        var comp = GetComponent(module, "TheBigCircle");
        var fldSolved = GetField<bool>(comp, "_solved");
        var fldSolution = GetField<Array>(comp, "_currentSolution");

        if (comp == null || fldSolved == null || fldSolution == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        var solution = fldSolution.Get();
        if (solution == null || solution.Length != 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Big Circle: solution is null or has an unexpected length ({1}).", _moduleId, solution == null ? "null" : solution.Length.ToString());
            yield break;
        }

        _modulesSolved.IncSafe(_BigCircle);

        addQuestions(module, solution.Cast<object>().Select((color, ix) => makeQuestion(
             Question.BigCircleColors, _BigCircle,
             possibleCorrectAnswers: new[] { color.ToString() },
             extraFormatArguments: new[] { ordinal(ix + 1) })));
    }

    private IEnumerable<object> ProcessBinaryLEDs(KMBombModule module)
    {
        var comp = GetComponent(module, "BinaryLeds");
        var fldSequences = GetField<int[,]>(comp, "sequences");
        var fldSequenceIndex = GetField<int>(comp, "sequenceIndex");
        var fldColors = GetField<int[]>(comp, "colorIndices");
        var fldSolutions = GetField<int[,]>(comp, "solutions");
        var fldWires = GetField<KMSelectable[]>(comp, "wires", isPublic: true);
        var fldSolved = GetField<bool>(comp, "solved");
        var fldBlinkDelay = GetField<float>(comp, "blinkDelay");
        var mthGetIndexFromTime = GetMethod<int>(comp, "GetIndexFromTime", 2);

        if (comp == null || fldSequences == null || fldSequenceIndex == null || fldColors == null || fldSolutions == null || fldWires == null || fldSolved == null || fldBlinkDelay == null || mthGetIndexFromTime == null)
            yield break;

        yield return null;

        int answer = -1;
        var wires = fldWires.Get();
        if (wires == null || wires.Length != 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Binary LEDs because ‘wires’ array is null or its length is unexpected or one of the values is null ({1}).", _moduleId, wires == null ? "null" : string.Format("[{0}]", wires.Select(w => w == null ? "null" : "not null").JoinString(", ")));
            yield break;
        }

        for (int i = 0; i < wires.Length; i++)
        {
            // Need an extra scope to work around bug in Mono 2.0 C# compiler
            new Action<int, KMSelectable.OnInteractHandler>((j, oldInteract) =>
            {
                wires[j].OnInteract = delegate
                {
                    wires[j].OnInteract = oldInteract;  // Restore original interaction, so that this can only ever be called once per wire.
                    var wasSolved = fldSolved.Get();    // Get this before calling oldInteract()
                    var seqIx = fldSequenceIndex.Get();
                    var numIx = mthGetIndexFromTime.Invoke(Time.time, fldBlinkDelay.Get());
                    var colors = fldColors.Get();
                    var solutions = fldSolutions.Get();
                    var result = oldInteract();

                    if (wasSolved)
                        return result;

                    if (colors == null || colors.Length <= j)
                    {
                        Debug.LogFormat("<Souvenir #{0}> Abandoning Binary LEDs because ‘colors’ array has unexpected length ({1}).", _moduleId,
                            colors == null ? "null" : colors.Length.ToString());
                        return result;
                    }

                    if (solutions == null || solutions.GetLength(0) <= seqIx || solutions.GetLength(1) <= colors[j])
                    {
                        Debug.LogFormat("<Souvenir #{0}> Abandoning Binary LEDs because ‘solutions’ array has unexpected lengths ({1}, {2}).", _moduleId,
                            solutions == null ? "null" : solutions.GetLength(0).ToString(),
                            solutions == null ? "null" : solutions.GetLength(1).ToString());
                        return result;
                    }

                    // Ignore if this wasn’t a solve
                    if (solutions[seqIx, colors[j]] != numIx)
                        return result;

                    // Find out which value is displayed
                    var sequences = fldSequences.Get();

                    if (sequences == null || sequences.GetLength(0) <= seqIx || sequences.GetLength(1) <= numIx)
                    {
                        Debug.LogFormat("<Souvenir #{0}> Abandoning Binary LEDs because ‘sequences’ array has unexpected lengths ({1}, {2}).", _moduleId,
                            sequences == null ? "null" : sequences.GetLength(0).ToString(),
                            sequences == null ? "null" : sequences.GetLength(1).ToString());
                        return result;
                    }

                    answer = sequences[seqIx, numIx];
                    return result;
                };
            })(i, wires[i].OnInteract);
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BinaryLEDs);

        if (answer != -1)
            addQuestion(module, Question.BinaryLEDsValue, new[] { answer.ToString() }, preferredWrongAnswers: Enumerable.Range(0, 32).Select(i => i.ToString()).ToArray());
    }

    private IEnumerable<object> ProcessBitmaps(KMBombModule module)
    {
        var comp = GetComponent(module, "BitmapsModule");
        var fldBitmap = GetField<bool[][]>(comp, "_bitmap");
        var fldIsSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldBitmap == null || fldIsSolved == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Bitmaps);

        var bitmap = fldBitmap.Get();
        var qCounts = new int[4];
        for (int x = 0; x < 8; x++)
            for (int y = 0; y < 8; y++)
                if (bitmap[x][y])
                    qCounts[(y / 4) * 2 + (x / 4)]++;

        var preferredWrongAnswers = qCounts.SelectMany(i => new[] { i, 16 - i }).Distinct().Select(i => i.ToString()).ToArray();

        addQuestions(module,
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { qCounts[0].ToString() }, new[] { "white", "top left" }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { qCounts[1].ToString() }, new[] { "white", "top right" }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { qCounts[2].ToString() }, new[] { "white", "bottom left" }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { qCounts[3].ToString() }, new[] { "white", "bottom right" }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { (16 - qCounts[0]).ToString() }, new[] { "black", "top left" }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { (16 - qCounts[1]).ToString() }, new[] { "black", "top right" }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { (16 - qCounts[2]).ToString() }, new[] { "black", "bottom left" }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { (16 - qCounts[3]).ToString() }, new[] { "black", "bottom right" }, preferredWrongAnswers));
    }

    private IEnumerable<object> ProcessBraille(KMBombModule module)
    {
        var comp = GetComponent(module, "BrailleModule");
        var fldWord = GetField<string>(comp, "_word");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldWord == null || fldSolved == null)
            yield break;

        yield return null;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Braille);
        addQuestion(module, Question.BrailleWord, new[] { fldWord.Get() });
    }

    private IEnumerable<object> ProcessBrokenButtons(KMBombModule module)
    {
        var comp = GetComponent(module, "BrokenButtonModule");
        var fldPressed = GetField<List<string>>(comp, "Pressed");
        var fldSolved = GetField<bool>(comp, "Solved");

        if (comp == null || fldPressed == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        var pressed = fldPressed.Get();
        if (pressed == null)
            yield break;

        _modulesSolved.IncSafe(_BrokenButtons);

        if (pressed.All(p => p.Length == 0))
        {
            Debug.LogFormat("[Souvenir #{0}] No question for Broken Buttons because the only buttons you pressed were literally blank.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        // skip the literally blank buttons.
        addQuestions(module, pressed.Select((p, i) => p.Length == 0 ? null : makeQuestion(Question.BrokenButtons, _BrokenButtons, new[] { p }, new[] { ordinal(i + 1) }, pressed.Except(new[] { "" }).ToArray())));
    }

    private IEnumerable<object> ProcessBurglarAlarm(KMBombModule module)
    {
        var comp = GetComponent(module, "BurglarAlarmScript");
        var fldDisplayText = GetField<TextMesh>(comp, "DisplayText", isPublic: true);
        var fldModuleNumber = GetField<int[]>(comp, "moduleNumber");
        var fldSolved = GetField<bool>(comp, "isSolved");

        if (comp == null || fldDisplayText == null || fldModuleNumber == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BurglarAlarm);

        var displayText = fldDisplayText.Get();
        var moduleNumber = fldModuleNumber.Get();
        if (displayText == null || moduleNumber == null)
            yield break;
        displayText.text = "";
        if (moduleNumber.Length != 8 || moduleNumber.Any(mn => mn < 0 || mn > 9))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Burglar Alarm because the module number is not 8 digits long or has an invalid number in it: [{1}].", _moduleId, moduleNumber.JoinString(", "));
            yield break;
        }
        addQuestions(module, moduleNumber.Select((mn, ix) => makeQuestion(Question.BurglarAlarmDigits, _BurglarAlarm, new[] { mn.ToString() }, new[] { ordinal(ix + 1) }, moduleNumber.Select(n => n.ToString()).ToArray())));
    }

    private IEnumerable<object> ProcessButtonSequences(KMBombModule module)
    {
        var comp = GetComponent(module, "ButtonSequencesModule");
        var fldPanelInfo = GetField<Array>(comp, "PanelInfo");
        var fldButtonsActive = GetField<bool>(comp, "buttonsActive");
        var fldColorNames = GetField<string[]>(comp, "ColorNames");

        if (comp == null || fldPanelInfo == null || fldButtonsActive == null || fldColorNames == null)
            yield break;

        while (fldButtonsActive.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ButtonSequences);

        var panelInfo = fldPanelInfo.Get();
        if (panelInfo == null || panelInfo.Rank != 2 || panelInfo.GetLength(1) != 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Button Sequences because panelInfo {1}.", _moduleId, panelInfo == null ? "is null" : panelInfo.Rank != 2 ? string.Format("has rank {0} instead of 2", panelInfo.Rank) : string.Format("has GetLength(1) == {0} instead of 3", panelInfo.GetLength(1)));
            yield break;
        }

        var obj = panelInfo.GetValue(0, 0);
        var fldColor = GetField<int>(obj, "color", isPublic: true);
        var colorNames = fldColorNames.Get();
        if (obj == null || fldColor == null || colorNames == null)
            yield break;
        var colorOccurrences = new Dictionary<int, int>();
        for (int i = panelInfo.GetLength(0) - 1; i >= 0; i--)
            for (int j = 0; j < 3; j++)
                colorOccurrences.IncSafe(fldColor.GetFrom(panelInfo.GetValue(i, j)));

        if (colorOccurrences.Keys.Any(key => key < 0 || key >= colorNames.Length))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Button Sequences because colorOccurrences=[{1}] while colorNames=[{2}].", _moduleId,
                colorOccurrences.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)).JoinString(", "),
                colorNames.Select(name => string.Format(@"""{0}""", name)).JoinString(", "));
            yield break;
        }

        addQuestions(module, colorOccurrences.Select(kvp => makeQuestion(Question.ButtonSequencesColorOccurrences, _ButtonSequences, new[] { kvp.Value.ToString() }, new[] { colorNames[kvp.Key].ToLowerInvariant() }, colorOccurrences.Values.Select(v => v.ToString()).ToArray())));
    }

    private IEnumerable<object> ProcessCalendar(KMBombModule module)
    {
        var comp = GetComponent(module, "calendar");
        var fldColorblindText = GetField<TextMesh>(comp, "colorblindText", isPublic: true);
        var fldLightsOn = GetField<bool>(comp, "_lightsOn");
        var fldIsSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldColorblindText == null || fldLightsOn == null || fldIsSolved == null)
            yield break;

        while (!fldLightsOn.Get())
            yield return new WaitForSeconds(.1f);

        var colorblindText = fldColorblindText.Get();
        if (colorblindText == null || colorblindText.text == null)
            yield break;

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Calendar);
        addQuestion(module, Question.CalendarLedColor, new[] { colorblindText.text });
    }

    private IEnumerable<object> ProcessCheapCheckout(KMBombModule module)
    {
        var comp = GetComponent(module, "CheapCheckoutModule");
        var fldPaid = GetField<decimal>(comp, "Paid");
        var fldDisplay = GetField<decimal>(comp, "Display");
        var fldWaiting = GetField<bool>(comp, "waiting");
        var fldSolved = GetField<bool>(comp, "solved");

        if (comp == null || fldPaid == null || fldDisplay == null || fldWaiting == null || fldSolved == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var paids = new List<decimal> { fldDisplay.Get() };
        var paid = fldPaid.Get();
        if (paid != paids[0])
            paids.Add(paid);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_CheapCheckout);

        addQuestions(module, paids.Select((p, i) => makeQuestion(Question.CheapCheckoutPaid, _CheapCheckout, new[] { "$" + p.ToString("N2") },
             extraFormatArguments: new[] { paids.Count == 1 ? "" : ordinal(i + 1) + " " },
             preferredWrongAnswers: Enumerable.Range(0, int.MaxValue).Select(_ => (decimal) Rnd.Range(5, 50)).Select(amt => "$" + amt.ToString("N2")).Distinct().Take(5).ToArray())));
    }

    private IEnumerable<object> ProcessChess(KMBombModule module)
    {
        var comp = GetComponent(module, "ChessBehaviour");
        var fldIndexSelected = GetField<int[]>(comp, "indexSelected"); // this contains both the coordinates and the solution
        var fldIsSolved = GetField<bool>(comp, "isSolved", isPublic: true);

        if (comp == null || fldIndexSelected == null || fldIsSolved == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var indexSelected = fldIndexSelected.Get();
        if (indexSelected == null)
            yield break;
        if (indexSelected.Length != 7 || indexSelected.Any(b => b / 10 < 0 || b / 10 >= 6 || b % 10 < 0 || b % 10 >= 6))
        {
            Debug.LogFormat("<Souvenir #{1}> Abandoning Chess because indexSelected array length is unexpected or one of the values is weird ({0}).", indexSelected.Select(iSel => iSel.ToString()).JoinString(", "), _moduleId);
            yield break;
        }

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Chess);

        addQuestions(module, Enumerable.Range(0, 6).Select(i => makeQuestion(Question.ChessCoordinate, _Chess, new[] { "" + ((char) (indexSelected[i] / 10 + 'a')) + (indexSelected[i] % 10 + 1) }, new[] { ordinal(i + 1) })));
    }

    private IEnumerable<object> ProcessChordQualities(KMBombModule module)
    {
        var comp = GetComponent(module, "ChordQualities");
        var fldLights = GetField<Array>(comp, "lights", isPublic: true);
        var fldIsSolved = GetField<bool>(comp, "isSolved", isPublic: true);
        var fldGivenChord = GetField<object>(comp, "givenChord");

        if (comp == null || fldLights == null || fldIsSolved == null || fldGivenChord == null)
            yield break;

        // Make sure that Chord Qualities’s Start() has run.
        yield return null;

        var givenChord = fldGivenChord.Get();
        var fldNotes = givenChord == null ? null : GetField<Array>(givenChord, "notes");
        var notes = fldNotes == null ? null : fldNotes.Get();
        var fldQuality = givenChord == null ? null : GetField<object>(givenChord, "quality");
        var quality = fldQuality == null ? null : fldQuality.Get();
        var fldQualityName = quality == null ? null : GetField<string>(quality, "name");
        var qualityName = fldQualityName == null ? null : fldQualityName.Get();

        if (givenChord == null || fldNotes == null || notes == null || fldQuality == null || quality == null || fldQualityName == null || qualityName == null)
            yield break;

        if (notes.Length != 4)
        {
            Debug.LogFormat(@"<Souvenir #{0}> Abandoning Chord Qualities because ‘notes’ has unexpected length ({1}).", _moduleId, notes == null ? "null" : notes.Length.ToString());
            yield break;
        }

        var lights = fldLights.Get();
        if (lights == null || lights.Length != 12)
        {
            Debug.LogFormat(@"<Souvenir #{0}> Abandoning Chord Qualities because ‘lights’ is null or has unexpected length ({1}).", _moduleId, lights == null ? "null" : lights.Length.ToString());
            yield break;
        }

        var fldsSetOutputLight = lights.Cast<object>().Select(light => GetMethod<object>(light, "setOutputLight", numParameters: 1, isPublic: true)).ToArray();
        if (fldsSetOutputLight.Any(meth => meth == null))
            yield break;

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ChordQualities);

        foreach (var meth in fldsSetOutputLight)
            meth.Invoke(false);

        var noteNames = notes.Cast<object>().Select(note => note.ToString().Replace("sharp", "♯")).ToArray();
        addQuestions(module,
            makeQuestion(Question.ChordQualitiesNotes, _ChordQualities, noteNames),
            makeQuestion(Question.ChordQualitiesQuality, _ChordQualities, new[] { qualityName }));
    }

    private static readonly Dictionary<string, string> _ColorDecoding_ColorNameMapping = new Dictionary<string, string>
    {
        { "R", "Red" },
        { "G", "Green" },
        { "B", "Blue" },
        { "Y", "Yellow" },
        { "P", "Purple" }
    };

    private IEnumerable<object> ProcessColorDecoding(KMBombModule module)
    {
        var comp = GetComponent(module, "ColorDecoding");
        var fldInputButtons = GetField<KMSelectable[]>(comp, "InputButtons", isPublic: true);
        var fldStageNum = GetField<int>(comp, "stagenum");
        var fldIndicator = GetField<object>(comp, "indicator");
        var fldIndicatorGrid = GetField<GameObject[]>(comp, "IndicatorGrid", isPublic: true);

        if (comp == null || fldInputButtons == null || fldStageNum == null || fldIndicator == null || fldIndicatorGrid == null)
            yield break;

        // Ensure Start() has run.
        yield return null;

        var indicatorGrid = fldIndicatorGrid.Get();
        if (indicatorGrid == null)
            yield break;

        var patterns = new Dictionary<int, string>();
        var colors = new Dictionary<int, string[]>();
        var isSolved = false;
        var isAbandoned = false;

        var inputButtons = fldInputButtons.Get();
        var origInteract = inputButtons.Select(ib => ib.OnInteract).ToArray();
        object lastIndicator = null;

        var update = new Action(() =>
        {
            var ind = fldIndicator.Get();
            if (ReferenceEquals(ind, lastIndicator))
                return;
            lastIndicator = ind;
            var fldPattern = GetField<object>(ind, "pattern");
            var fldColors = GetField<IList>(ind, "indicator_colors");
            var indColors = fldColors == null ? null : fldColors.Get();
            if (fldPattern == null || fldColors == null || indColors == null || indColors.Count == 0 || indColors.Cast<object>().Any(col => !_ColorDecoding_ColorNameMapping.ContainsKey(col.ToString())))
            {
                Debug.LogFormat(@"<Souvenir #{0}> Abandoning Color Decoding because something is null, or indicator_colors contains an invalid value: {1}.", _moduleId,
                    indColors == null ? "<null>" : string.Format("[{0}]", indColors.Cast<object>().JoinString(", ")));
                isAbandoned = true;
                return;
            }
            var stageNum = fldStageNum.Get();
            var patternName = fldPattern.Get().ToString();
            patterns[stageNum] = patternName.Substring(0, 1) + patternName.Substring(1).ToLowerInvariant();
            colors[stageNum] = indColors.Cast<object>().Select(obj => _ColorDecoding_ColorNameMapping[obj.ToString()]).ToArray();
        });
        update();

        for (int ix = 0; ix < inputButtons.Length; ix++)
        {
            new Action<int>(i =>
            {
                inputButtons[i].OnInteract = delegate
                {
                    var ret = origInteract[i]();
                    if (isSolved || isAbandoned)
                        return ret;

                    if (fldStageNum.Get() >= 3)
                    {
                        for (int j = 0; j < indicatorGrid.Length; j++)
                            indicatorGrid[j].GetComponent<MeshRenderer>().material.color = Color.black;
                        isSolved = true;
                    }
                    else
                        update();

                    return ret;
                };
            })(ix);
        }

        while (!isSolved && !isAbandoned)
            yield return new WaitForSeconds(.1f);

        for (int ix = 0; ix < inputButtons.Length; ix++)
            inputButtons[ix].OnInteract = origInteract[ix];

        if (isAbandoned)
        {
            Debug.LogFormat(@"<Souvenir #{0}> Abandoning Color Decoding.", _moduleId);
            yield break;
        }
        _modulesSolved.IncSafe(_ColorDecoding);

        if (Enumerable.Range(0, 3).Any(k => !patterns.ContainsKey(k) || !colors.ContainsKey(k)))
        {
            Debug.LogFormat(@"<Souvenir #{0}> Abandoning Color Decoding because I have a discontinuous set of stages: {1}/{2}.", _moduleId, patterns.Keys.JoinString(", "), colors.Keys.JoinString(", "));
            yield break;
        }

        addQuestions(module, Enumerable.Range(0, 3).SelectMany(stage => Ut.NewArray(
             colors[stage].Length <= 3 ? makeQuestion(Question.ColorDecodingIndicatorColors, _ColorDecoding, colors[stage], new[] { "appeared", ordinal(stage + 1) }) : null,
             colors[stage].Length >= 3 ? makeQuestion(Question.ColorDecodingIndicatorColors, _ColorDecoding, _ColorDecoding_ColorNameMapping.Keys.Except(colors[stage]).ToArray(), new[] { "did not appear", ordinal(stage + 1) }) : null,
             makeQuestion(Question.ColorDecodingIndicatorPattern, _ColorDecoding, new[] { patterns[stage] }, new[] { ordinal(stage + 1) }))));
    }

    private IEnumerable<object> ProcessColoredSquares(KMBombModule module)
    {
        var comp = GetComponent(module, "ColoredSquaresModule");
        var fldExpectedPresses = GetField<object>(comp, "_expectedPresses");
        var fldFirstStageColor = GetField<object>(comp, "_firstStageColor");

        if (comp == null || fldExpectedPresses == null || fldFirstStageColor == null)
            yield break;

        yield return null;

        // Colored Squares sets _expectedPresses to null when it’s solved
        while (fldExpectedPresses.Get(nullAllowed: true) != null)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_ColoredSquares);
        addQuestion(module, Question.ColoredSquaresFirstGroup, new[] { fldFirstStageColor.Get().ToString() });
    }

    private IEnumerable<object> ProcessColorMorse(KMBombModule module)
    {
        var comp = GetComponent(module, "FlashingMathModule");
        var fldNumbers = GetField<int[]>(comp, "Numbers");
        var fldColors = GetField<int[]>(comp, "Colors");
        var fldColorNames = GetField<string[]>(comp, "ColorNames", isPublic: true);
        var fldFlashingEnabled = GetField<bool>(comp, "flashingEnabled");

        if (comp == null || fldNumbers == null || fldColors == null || fldColorNames == null || fldFlashingEnabled == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        // Once Color Morse is activated, ‘flashingEnabled’ is set to true, and then it is only set to false when the module is solved.
        while (fldFlashingEnabled.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ColorMorse);

        var numbers = fldNumbers.Get();
        var colors = fldColors.Get();
        var colorNames = fldColorNames.Get();

        if (numbers == null || numbers.Length != 3 || colors == null || colors.Length != 3 || colorNames == null || colorNames.Any(cn => cn == null) || colors.Any(c => c < 0 || c >= colorNames.Length))
        {
            Debug.LogFormat(@"<Souvenir #{0}> Abandoning Color Morse because an array is null or not the expected length: numbers={1}, colors={2}, colorNames={3}.", _moduleId,
                numbers == null ? "null" : string.Format("[{0}]", numbers.JoinString(", ")),
                colors == null ? "null" : string.Format("[{0}]", colors.JoinString(", ")),
                colorNames == null ? "null" : string.Format("[{0}]", colorNames.Select(cn => cn == null ? "<null>" : string.Format(@"""{0}""", cn)).JoinString(", ")));
            yield break;
        }

        var flashedColorNames = colors.Select(c => colorNames[c].Substring(0, 1) + colorNames[c].Substring(1).ToLowerInvariant()).ToArray();
        var flashedCharacters = numbers.Select(num => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(num, 1)).ToArray();
        addQuestions(module, Enumerable.Range(0, 3).SelectMany(ix => Ut.NewArray(
             makeQuestion(Question.ColorMorseColor, _ColorMorse, new[] { flashedColorNames[ix] }, new[] { ordinal(ix + 1) }, flashedColorNames),
             makeQuestion(Question.ColorMorseCharacter, _ColorMorse, new[] { flashedCharacters[ix] }, new[] { ordinal(ix + 1) }, flashedCharacters)
         )));
    }

    private IEnumerable<object> ProcessCoordinates(KMBombModule module)
    {
        var comp = GetComponent(module, "CoordinatesModule");
        var fldFirstSubmitted = GetField<int?>(comp, "_firstCorrectSubmitted");
        var fldClues = GetField<IList>(comp, "_clues");

        if (comp == null || fldFirstSubmitted == null || fldClues == null)
            yield break;

        while (fldFirstSubmitted.Get(nullAllowed: true) == null)
            yield return new WaitForSeconds(.1f);

        var clues = fldClues.Get();
        var index = fldFirstSubmitted.Get().Value;
        if (clues == null || index < 0 || index >= clues.Count)
        {
            Debug.LogFormat(@"<Souvenir #{0}> Abandoning Coordinates because ‘clues’ is null or ‘index’ has unexpected value ({1}, clues length {2}).", _moduleId, index, clues == null ? "null" : clues.Count.ToString());
            yield break;
        }
        var clue = clues[index];
        var fldClueText = GetField<string>(clue, "Text");
        var fldClueSystem = GetField<int?>(clue, "System");
        if (fldClueText == null || fldClueSystem == null)
            yield break;

        var clueText = fldClueText.Get();
        if (clueText == null)
            yield break;

        // The module sets ‘clues’ to null to indicate that it is solved.
        while (fldClues.Get(nullAllowed: true) != null)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Coordinates);
        var shortenCoordinate = Ut.Lambda((string str) =>
        {
            if (str == null)
                return null;

            str = str.Replace("\n", " ");
            if (str.Length > 13)
            {
                str = str
                    .Replace(",", "")
                    .Replace("north", "N")
                    .Replace("south", "S")
                    .Replace("west", "W")
                    .Replace("east", "E")
                    .Replace("up", "U")
                    .Replace("down", "D")
                    .Replace("left", "L")
                    .Replace("right", "R")
                    .Replace("top", "T")
                    .Replace("bottom", "B")
                    .Replace("middle", "M")
                    .Replace("center", "C")
                    .Replace("from", "fr.")
                    .Replace(" o’clock", "")
                    .Replace(" corner", "");
                str = Regex.Replace(str, @"\b[A-Z] [A-Z]\b", m => m.Value.Remove(1, 1));
            }
            return str;
        });

        // The size clue is the only one where fldClueSystem is null
        var sizeClue = clues.Cast<object>().Where(szCl => fldClueSystem.GetFrom(szCl, nullAllowed: true) == null).FirstOrDefault();
        addQuestions(module,
            makeQuestion(Question.CoordinatesFirstSolution, _Coordinates, new[] { shortenCoordinate(clueText) }, preferredWrongAnswers: clues.Cast<object>().Select(c => shortenCoordinate(fldClueText.GetFrom(c))).Where(t => t != null).ToArray()),
            sizeClue == null ? null : makeQuestion(Question.CoordinatesSize, _Coordinates, new[] { fldClueText.GetFrom(sizeClue) }));
    }

    private IEnumerable<object> ProcessCrackbox(KMBombModule module)
    {
        var comp = GetComponent(module, "CrackboxScript");
        var fldGridItems = GetField<Array>(comp, "originalGridItems");
        var fldSolved = GetField<bool>(comp, "isSolved");

        if (comp == null || fldGridItems == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Crackbox);

        var array = fldGridItems.Get();
        if (array == null || array.Length != 16)
        {
            Debug.LogFormat(@"<Souvenir #{0}> Abandoning Crackbox because ‘originalGridItems’ is null or has unexpected length ({1}, expected 16).", _moduleId, array == null ? "<null>" : array.Length.ToString());
            yield break;
        }
        var obj = array.GetValue(0);
        var fldIsBlack = GetField<bool>(obj, "IsBlack", isPublic: true);
        var fldIsLocked = GetField<bool>(obj, "IsLocked", isPublic: true);
        var fldValue = GetField<int>(obj, "Value", isPublic: true);
        if (fldIsBlack == null || fldIsLocked == null || fldValue == null)
            yield break;

        var qs = new List<QandA>();
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                obj = array.GetValue(y * 4 + x);
                qs.Add(makeQuestion(Question.CrackboxInitialState, _Crackbox, new[] { fldIsBlack.GetFrom(obj) ? "black" : !fldIsLocked.GetFrom(obj) ? "white" : fldValue.GetFrom(obj).ToString() }, new[] { ((char) ('A' + x)).ToString(), (y + 1).ToString() }));
            }
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessCreation(KMBombModule module)
    {
        var comp = GetComponent(module, "CreationModule");
        var fldSolved = GetField<bool>(comp, "Solved");
        var fldDay = GetField<int>(comp, "Day");
        var fldWeather = GetField<string>(comp, "Weather");

        if (comp == null || fldSolved == null || fldDay == null || fldWeather == null)
            yield break;

        var weatherNames = GetAnswers(Question.CreationWeather);

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var currentDay = fldDay.Get();
        var currentWeather = fldWeather.Get();
        var allWeather = new List<string>();
        var badData = currentDay != 1 || currentWeather == null || !weatherNames.Contains(currentWeather);
        while (!badData)
        {
            while (fldDay.Get() == currentDay && !fldSolved.Get() && currentWeather == fldWeather.Get())
                yield return new WaitForSeconds(0.1f);

            if (fldSolved.Get())
                break;

            if (fldDay.Get() <= currentDay)
                allWeather.Clear();
            else
                allWeather.Add(currentWeather);

            currentDay = fldDay.Get();
            currentWeather = fldWeather.Get();
            badData = currentDay < 1 || currentDay > 6 || currentWeather == null || !weatherNames.Contains(currentWeather);
        }

        if (badData)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Creation because of unexpected data. Day = {1}, Weather = {2}", _moduleId, currentDay, currentWeather ?? "<null>");
            yield break;
        }

        _modulesSolved.IncSafe(_Creation);
        addQuestions(module, allWeather.Select((t, i) => makeQuestion(Question.CreationWeather, _Creation, new[] { t }, new[] { ordinal(i + 1) })));
    }

    private IEnumerable<object> ProcessDoubleOh(KMBombModule module)
    {
        var comp = GetComponent(module, "DoubleOhModule");
        var fldFunctions = GetField<Array>(comp, "_functions");
        var fldIsSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldFunctions == null || fldIsSolved == null)
            yield break;

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);

        var functions = fldFunctions.Get();
        var submitIndex = functions.Cast<object>().IndexOf(f => f.ToString() == "Submit");
        if (submitIndex < 0 || submitIndex > 4)
        {
            Debug.LogFormat(@"<Souvenir #{0}> Double-Oh: submit button is at index {1} (expected 0–4).", _moduleId, submitIndex);
            yield break;
        }

        _modulesSolved.IncSafe(_DoubleOh);
        addQuestion(module, Question.DoubleOhSubmitButton, new[] { "↕↔⇔⇕◆".Substring(submitIndex, 1) });
    }

    private IEnumerable<object> ProcessFastMath(KMBombModule module)
    {
        var comp = GetComponent(module, "FastMath");
        var fldScreen = GetField<TextMesh>(comp, "Screen", isPublic: true);
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldScreen == null || fldSolved == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var prevLetters = new HashSet<string>();
        string letters = null;
        while (!fldSolved.Get())
        {
            var display = fldScreen.Get().text;
            if (display.Length != 3)
            {
                Debug.LogFormat(@"<Souvenir #{1}> Abandoning Fast Math because the screen contains something other than three characters: ""{0}"" ({2} characters).", display, _moduleId, display.Length);
                yield break;
            }
            letters = display[0] + "" + display[2];
            prevLetters.Add(letters);
            yield return new WaitForSeconds(.1f);
        }
        if (letters == null)
        {
            Debug.LogFormat(@"<Souvenir #{0}> Abandoning Fast Math because no letters were extracted before the module was solved.", _moduleId);
            yield break;
        }

        _modulesSolved.IncSafe(_FastMath);
        addQuestion(module, Question.FastMathLastLetters, new[] { letters }, preferredWrongAnswers: prevLetters.ToArray());
    }

    private IEnumerable<object> ProcessFunctions(KMBombModule module)
    {
        var comp = GetComponent(module, "qFunctions");
        var fldFirstLastDigit = GetField<int>(comp, "firstLastDigit");
        var fldSolved = GetField<bool>(comp, "isSolved");


        if (fldFirstLastDigit == null)
            yield break;


        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        var lastDigit = fldFirstLastDigit.Get();
        if (lastDigit == -1)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Functions because they solved it with no queries?! This isn't a bug, just impressive (or cheating).", _moduleId);
            yield break;
        }
        else if (lastDigit > 9 || lastDigit < 0)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Functions because the first last digit is {1} when it should be from 0 to 9.", _moduleId, lastDigit);
            yield break;
        }
        _modulesSolved.IncSafe(_Functions);
        addQuestion(module, Question.FunctionsLastDigit, new[] { lastDigit.ToString() });
    }

    private IEnumerable<object> ProcessGridLock(KMBombModule module)
    {
        var comp = GetComponent(module, "GridlockModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var fldPages = GetField<int[][]>(comp, "_pages");
        var fldSolution = GetField<int>(comp, "_solution");

        if (comp == null || fldSolved == null || fldPages == null || fldSolution == null)
            yield break;

        var locations = GetAnswers(Question.GridLockStartingLocation);
        var colors = GetAnswers(Question.GridLockStartingColor);
        if (locations == null || colors == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var solution = fldSolution.Get();
        var pages = fldPages.Get();
        if (pages == null || pages.Length < 5 || pages.Length > 10 || solution < 0 || solution > 15 ||
            pages.Any(p => p == null || p.Length != 16 || p.Any(q => q < 0 || (q & 15) > 12 || (q & (15 << 4)) > (4 << 4))))
        {
            Debug.LogFormat(@"<Souvenir #{0}> Abandoning Gridlock because unxpected values were found (pages={1}, solution={2}).", _moduleId, pages == null ? "<null>" : string.Format("[{0}]", pages.Select(p => string.Format("[{0}]", p.JoinString(", "))).JoinString(", ")), solution);
            yield break;
        }

        var start = pages[0].IndexOf(i => (i & 15) == 4);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_GridLock);
        addQuestions(module,
            makeQuestion(Question.GridLockStartingLocation, _GridLock, new[] { locations[start] }),
            makeQuestion(Question.GridLockEndingLocation, _GridLock, new[] { locations[solution] }),
            makeQuestion(Question.GridLockStartingColor, _GridLock, new[] { colors[(pages[0][start] >> 4) - 1] }));
    }

    private static readonly string[] _logicalButtonsButtonNames = new[] { "top", "bottom-left", "bottom-right" };
    private IEnumerable<object> ProcessLogicalButtons(KMBombModule module)
    {
        var comp = GetComponent(module, "LogicalButtonsScript");
        var fldSolved = GetField<bool>(comp, "isSolved");
        var fldStage = GetField<int>(comp, "stage");
        var fldButtons = GetField<Array>(comp, "buttons");
        var fldGateOperator = GetField<object>(comp, "gateOperator");
        if (comp == null || fldSolved == null || fldStage == null || fldButtons == null || fldGateOperator == null)
            yield break;

        var curStage = 0;
        var colors = new List<string[]>();
        var labels = new List<string[]>();
        var initialOperators = new List<string>();

        while (!fldSolved.Get())
        {
            var stage = fldStage.Get();
            if (stage != curStage)
            {
                if (stage != curStage + 1)
                {
                    Debug.LogFormat(@"<Souvenir #{0}> Abandoning Logical Buttons because I must have missed a stage (it went from {1} to {2}).", _moduleId, curStage, stage);
                    yield break;
                }

                var buttons = fldButtons.Get();
                if (buttons == null || buttons.Length != 3)
                {
                    Debug.LogFormat(@"<Souvenir #{0}> Abandoning Logical Buttons because “buttons” {1} (expected length 3).", _moduleId, buttons == null ? "is null" : "has length " + buttons.Length);
                    yield break;
                }
                var infs = buttons.Cast<object>().Select(obj =>
                {
                    var fldLabel = GetField<string>(obj, "<Label>k__BackingField");
                    var fldColor = GetField<object>(obj, "<Color>k__BackingField");
                    var fldIndex = GetField<int>(obj, "<Index>k__BackingField");
                    if (fldLabel == null || fldColor == null || fldIndex == null)
                        return null;
                    return new { Label = fldLabel.Get(), Color = fldColor.Get(), Index = fldIndex.Get() };
                }).ToArray();
                if (infs.Length != 3 || infs.Any(inf => inf == null || inf.Label == null || inf.Color == null) || infs[0].Index != 0 || infs[1].Index != 1 || infs[2].Index != 2)
                {
                    Debug.LogFormat(@"<Souvenir #{0}> Abandoning Logical Buttons because I got an unexpected value ([{1}]).", _moduleId, infs.Select(inf => inf == null ? "<null>" : inf.ToString()).JoinString(", "));
                    yield break;
                }
                var gateOperator = fldGateOperator.Get();
                var mthGetName = GetMethod<string>(gateOperator, "get_Name", 0, isPublic: true);
                if (gateOperator == null || mthGetName == null)
                    yield break;

                colors.Add(infs.Select(inf => inf.Color.ToString()).ToArray());
                labels.Add(infs.Select(inf => inf.Label).ToArray());
                initialOperators.Add(mthGetName.Invoke());
                curStage = stage;
            }

            yield return new WaitForSeconds(.1f);
        }

        _modulesSolved.IncSafe(_LogicalButtons);
        if (initialOperators.Any(io => io == null))
        {
            Debug.LogFormat(@"<Souvenir #{0}> Abandoning Logical Buttons because there is a null initial operator ([{1}]).", _moduleId, initialOperators.Select(io => io == null ? "<null>" : string.Format(@"""{0}""", io)).JoinString(", "));
            yield break;
        }

        addQuestions(module,
            colors.SelectMany((clrs, stage) => clrs.Select((clr, btnIx) => makeQuestion(Question.LogicalButtonsColor, _LogicalButtons, new[] { clr }, new[] { _logicalButtonsButtonNames[btnIx], ordinal(stage + 1) })))
                .Concat(labels.SelectMany((lbls, stage) => lbls.Select((lbl, btnIx) => makeQuestion(Question.LogicalButtonsLabel, _LogicalButtons, new[] { lbl }, new[] { _logicalButtonsButtonNames[btnIx], ordinal(stage + 1) }))))
                .Concat(initialOperators.Select((op, stage) => makeQuestion(Question.LogicalButtonsOperator, _LogicalButtons, new[] { op }, new[] { ordinal(stage + 1) }))));
    }

    private IEnumerable<object> ProcessHexamaze(KMBombModule module)
    {
        var comp = GetComponent(module, "HexamazeModule");
        var fldPawnColor = GetField<int>(comp, "_pawnColor");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        if (comp == null | fldPawnColor == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Hexamaze);
        var pawnColor = fldPawnColor.Get();
        if (pawnColor < 0 || pawnColor >= 6)
        {
            Debug.LogFormat("<Souvenir #{1}> Abandoning Hexamaze because invalid pawn color {0}.", pawnColor, _moduleId);
            yield break;
        }

        addQuestion(module, Question.HexamazePawnColor, new[] { new[] { "Red", "Yellow", "Green", "Cyan", "Blue", "Pink" }[pawnColor] });
    }

    private IEnumerable<object> ProcessHumanResources(KMBombModule module)
    {
        var comp = GetComponent(module, "HumanResourcesModule");
        var fldPeople = comp == null ? null : GetStaticField<Array>(comp.GetType(), "_people");
        var people = fldPeople == null ? null : fldPeople.Get();
        var fldNames = GetField<int[]>(comp, "_availableNames");
        var fldDescs = GetField<int[]>(comp, "_availableDescs");
        var fldToHire = GetField<int>(comp, "_personToHire");
        var fldToFire = GetField<int>(comp, "_personToFire");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldPeople == null || people == null || fldNames == null || fldDescs == null || fldToHire == null || fldToFire == null || fldSolved == null)
            yield break;

        if (people.Length != 16)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Human Resources because _people array has unexpected length ({1} instead of 16).", _moduleId, people.Length);
            yield break;
        }
        var person = people.GetValue(0);
        var fldName = GetField<string>(person, "Name", isPublic: true);
        var fldDesc = GetField<string>(person, "Descriptor", isPublic: true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_HumanResources);

        var names = fldNames.Get();
        var descs = fldDescs.Get();
        var toHire = fldToHire.Get();
        var toFire = fldToFire.Get();
        if (names == null || names.Length != 10 || descs == null || descs.Length != 5)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Human Resources because unexpected length: (names={1} (should be 10), descs={2} (should be 5)).", _moduleId,
                names == null ? "null" : names.Length.ToString(), descs == null ? "null" : descs.Length.ToString());
            yield break;
        }

        addQuestions(module,
            makeQuestion(Question.HumanResourcesEmployees, _HumanResources, names.Take(5).Where(ix => ix != toFire).Select(ix => fldName.GetFrom(people.GetValue(ix))).ToArray(), new[] { "an employee that was not fired" }),
            makeQuestion(Question.HumanResourcesEmployees, _HumanResources, names.Skip(5).Where(ix => ix != toHire).Select(ix => fldName.GetFrom(people.GetValue(ix))).ToArray(), new[] { "an applicant that was not hired" }),
            makeQuestion(Question.HumanResourcesDescriptors, _HumanResources, descs.Take(3).Select(ix => fldDesc.GetFrom(people.GetValue(ix))).ToArray(), new[] { "red" }),
            makeQuestion(Question.HumanResourcesDescriptors, _HumanResources, descs.Skip(3).Select(ix => fldDesc.GetFrom(people.GetValue(ix))).ToArray(), new[] { "green" }));
    }

    private IEnumerable<object> ProcessHunting(KMBombModule module)
    {
        var comp = GetComponent(module, "hunting");
        var fldStage = GetField<int>(comp, "stage");
        var fldReverseClues = GetField<bool>(comp, "reverseClues");
        var fldAcceptingInput = GetField<bool>(comp, "acceptingInput");

        if (comp == null || fldStage == null || fldReverseClues == null || fldAcceptingInput == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var hasRowFirst = new bool[4];
        while (fldStage.Get() < 5)
        {
            hasRowFirst[fldStage.Get() - 1] = fldReverseClues.Get();
            yield return new WaitForSeconds(.1f);
        }

        _modulesSolved.IncSafe(_Hunting);
        var qs = new List<QandA>();
        foreach (var row in new[] { false, true })
            foreach (var first in new[] { false, true })
                qs.Add(makeQuestion(Question.HuntingColumnsRows, _Hunting,
                    possibleCorrectAnswers: new[] { _attributes[Question.HuntingColumnsRows].AllAnswers[(hasRowFirst[0] ^ row ^ first ? 1 : 0) | (hasRowFirst[1] ^ row ^ first ? 2 : 0) | (hasRowFirst[2] ^ row ^ first ? 4 : 0)] },
                    extraFormatArguments: new[] { row ? "row" : "column", first ? "first" : "second" }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessIceCream(KMBombModule module)
    {
        var comp = GetComponent(module, "IceCreamModule");
        var fldCurrentStage = GetField<int>(comp, "CurrentStage");
        var fldCustomers = GetField<int[]>(comp, "CustomerNamesSolution");
        var fldSolution = GetField<int[]>(comp, "Solution");
        var fldFlavourOptions = GetField<int[][]>(comp, "FlavorOptions");

        if (comp == null || fldCurrentStage == null || fldCustomers == null || fldSolution == null || fldFlavourOptions == null)
            yield break;

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

            var options = fldFlavourOptions.Get();
            var sol = fldSolution.Get();
            var cus = fldCustomers.Get();

            if (options == null || sol == null || cus == null || options.Length != 3 || fldCurrentStage.Get() < i ||
                options.Any(x => x == null || x.Length != 5 || x.Any(y => y < 0 || y >= flavourNames.Length)) ||
                sol.Any(x => x < 0 || x >= flavourNames.Length) || cus.Any(x => x < 0 || x >= customerNames.Length))
            {
                Debug.LogFormat("<Souvenir #{0}> Abandoning Ice Cream because of unexpected values.", _moduleId);
                yield break;
            }
            flavours[i] = options[i].ToArray();
            solution[i] = flavours[i][sol[i]];
            customers[i] = cus[i];
        }
        var questions = new List<QandA>();
        _modulesSolved.IncSafe(_IceCream);

        for (var i = 0; i < 3; i++)
        {
            questions.Add(makeQuestion(Question.IceCreamFlavour, _IceCream, flavours[i].Where(ix => ix != solution[i]).Select(ix => flavourNames[ix]).ToArray(), new[] { "was on offer, but not sold,", ordinal(i + 1) }));
            questions.Add(makeQuestion(Question.IceCreamFlavour, _IceCream, flavourNames.Where((f, ix) => !flavours[i].Contains(ix)).ToArray(), new[] { "was not on offer", ordinal(i + 1) }));
            if (i != 2)
                questions.Add(makeQuestion(Question.IceCreamCustomer, _IceCream, new[] { customerNames[customers[i]] }, new[] { ordinal(i + 1) }, preferredWrongAnswers: customers.Select(ix => customerNames[ix]).ToArray()));
        }

        addQuestions(module, questions);
    }

    private IEnumerable<object> ProcessListening(KMBombModule module)
    {
        var comp = GetComponent(module, "Listening");
        var fldIsActivated = GetField<bool>(comp, "isActivated");
        var fldCodeInput = GetField<char[]>(comp, "codeInput");
        var fldCodeInputPosition = GetField<int>(comp, "codeInputPosition");
        var fldSound = GetField<object>(comp, "sound");
        var fldDollarButton = GetField<KMSelectable>(comp, "DollarButton", isPublic: true);
        var fldPoundButton = GetField<KMSelectable>(comp, "PoundButton", isPublic: true);
        var fldStarButton = GetField<KMSelectable>(comp, "StarButton", isPublic: true);
        var fldAmpersandButton = GetField<KMSelectable>(comp, "AmpersandButton", isPublic: true);

        if (comp == null || fldIsActivated == null || fldCodeInput == null || fldCodeInputPosition == null || fldSound == null || fldDollarButton == null || fldPoundButton == null || fldStarButton == null || fldAmpersandButton == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var attr = _attributes.Get(Question.Listening);
        if (attr == null)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Listening because SouvenirQuestionAttribute for Question.Listening is null.", _moduleId);
            yield break;
        }

        var sound = fldSound.Get();
        var buttons = new[] { fldDollarButton.Get(), fldPoundButton.Get(), fldStarButton.Get(), fldAmpersandButton.Get() };
        if (sound == null || buttons.Contains(null))
            yield break;

        var prevInteracts = buttons.Select(btn => btn.OnInteract).ToArray();
        var nullIndex = Array.IndexOf(prevInteracts, null);
        if (nullIndex != -1)
        {
            Debug.LogFormat("<Souvenir #{1}> Abandoning Listening because buttons[{0}].OnInteract is null.", nullIndex, _moduleId);
            yield break;
        }

        var fldSoundCode = GetField<string>(sound, "code", isPublic: true);
        if (fldSoundCode == null)
            yield break;
        var correctCode = fldSoundCode.Get();
        if (correctCode == null)
            yield break;

        var code = "";
        var solved = false;
        for (int i = 0; i < 4; i++)
        {
            // Workaround bug in Mono 2.0 C# compiler
            new Action<int>(j =>
            {
                buttons[i].OnInteract = delegate
                {
                    var ret = prevInteracts[j]();
                    code += "$#*&"[j];
                    if (code.Length == 5)
                    {
                        if (code == correctCode)
                        {
                            solved = true;
                            // Sneaky: make it so that the player can no longer play the sound
                            fldIsActivated.Set(false);
                        }
                        code = "";
                    }
                    return ret;
                };
            })(i);
        }

        while (!solved)
            yield return new WaitForSeconds(.1f);

        for (int i = 0; i < 4; i++)
            buttons[i].OnInteract = prevInteracts[i];

        _modulesSolved.IncSafe(_Listening);
        addQuestion(module, Question.Listening, new[] { correctCode }, preferredWrongAnswers: attr.ExampleAnswers);
    }

    private IEnumerable<object> ProcessLogicGates(KMBombModule module)
    {
        var comp = GetComponent(module, "LogicGates");
        var fldGates = GetField<IList>(comp, "_gates");
        var fldSolution = GetField<int>(comp, "_solution");
        var fldInputs = GetField<List<int>>(comp, "_inputs");
        var fldCurrentInputIndex = GetField<int>(comp, "_currentInputIndex");
        var fldButtonCheck = GetField<KMSelectable>(comp, "ButtonCheck", isPublic: true);

        if (comp == null || fldGates == null || fldSolution == null || fldInputs == null || fldCurrentInputIndex == null || fldButtonCheck == null)
            yield break;

        // Make sure Start() has run
        yield return null;

        var inputs = fldInputs.Get();
        var gates = fldGates.Get();
        var btnCheck = fldButtonCheck.Get();
        var solution = fldSolution.Get();
        if (inputs == null || inputs.Count == 0 || gates == null || gates.Count == 0 || btnCheck == null)
            yield break;

        var fldGateType = GetField<object>(gates[0], "GateType", isPublic: true);
        var tmpGateType = fldGateType == null ? null : fldGateType.Get();
        var fldGateTypeName = tmpGateType == null ? null : GetField<string>(tmpGateType, "Name", isPublic: true);
        if (fldGateType == null || tmpGateType == null || fldGateTypeName == null)
            yield break;

        var gateTypeNames = gates.Cast<object>().Select(obj => fldGateTypeName.GetFrom(fldGateType.GetFrom(obj)).ToString()).ToArray();
        string duplicate = null;
        bool isDuplicateInvalid = false;
        for (int i = 0; i < gateTypeNames.Length; i++)
            for (int j = i + 1; j < gateTypeNames.Length; j++)
                if (gateTypeNames[i] == gateTypeNames[j])
                {
                    if (duplicate != null)
                        isDuplicateInvalid = true;
                    else
                        duplicate = gateTypeNames[i];
                }

        // Unfortunately Logic Gates has no “isSolved” field, so we need to hook into the button
        var oldInteract = btnCheck.OnInteract;
        var solved = false;
        btnCheck.OnInteract = delegate
        {
            var ret = oldInteract();
            if (inputs[fldCurrentInputIndex.Get()] == solution)
                solved = true;
            return ret;
        };

        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_LogicGates);
        btnCheck.OnInteract = oldInteract;

        var qs = new List<QandA>();
        for (int i = 0; i < gateTypeNames.Length; i++)
            qs.Add(makeQuestion(Question.LogicGatesGates, _LogicGates, new[] { gateTypeNames[i] }, new[] { "gate " + (char) ('A' + i) }));
        if (!isDuplicateInvalid)
            qs.Add(makeQuestion(Question.LogicGatesGates, _LogicGates, new[] { duplicate }, new[] { "the duplicated gate" }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessMafia(KMBombModule module)
    {
        var comp = GetComponent(module, "MafiaModule");
        var fldSuspects = GetField<Array>(comp, "_suspects");
        var fldGodfather = GetField<object>(comp, "_godfather");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldSuspects == null || fldGodfather == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        var godfather = fldGodfather.Get();
        var suspects = fldSuspects.Get();

        if (godfather == null || suspects == null || suspects.Length != 8)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Mafia because ‘{1}’ is null or unexpected length ({2}).", _moduleId, godfather == null ? "godfather" : "suspects", suspects == null ? "null" : suspects.Length.ToString());
            yield break;
        }

        _modulesSolved.IncSafe(_Mafia);
        addQuestion(module, Question.MafiaPlayers, suspects.Cast<object>().Select(obj => obj.ToString()).Except(new[] { godfather.ToString() }).ToArray());
    }

    private IEnumerable<object> ProcessMaritimeFlags(KMBombModule module)
    {
        var comp = GetComponent(module, "MaritimeFlagsModule");
        var fldBearing = GetField<int>(comp, "_bearingOnModule");
        var fldCallsign = GetField<object>(comp, "_callsign");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldBearing == null || fldCallsign == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MaritimeFlags);

        var bearing = fldBearing.Get();
        var callsignObj = fldCallsign.Get();

        if (callsignObj == null || bearing < 0 || bearing >= 360)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Maritime Flags because callsign is null ({1}) or bearing is out of range ({2}).", _moduleId, callsignObj == null, bearing);
            yield break;
        }

        var fldCallsignName = GetField<string>(callsignObj, "Name", isPublic: true);
        if (fldCallsignName == null)
            yield break;
        var callsign = fldCallsignName.Get();
        if (callsign == null || callsign.Length != 7)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Maritime Flags because callsign is null or length not 7 (it’s {1}).", _moduleId, callsign == null ? "null" : callsign.Length.ToString());
            yield break;
        }

        addQuestions(module,
            makeQuestion(Question.MaritimeFlagsBearing, _MaritimeFlags, new[] { bearing.ToString() }),
            makeQuestion(Question.MaritimeFlagsCallsign, _MaritimeFlags, new[] { callsign.ToLowerInvariant() }));
    }

    private IEnumerable<object> ProcessMonsplodeFight(KMBombModule module)
    {
        var comp = GetComponent(module, "MonsplodeFightModule");
        var fldCreatureData = GetField<object>(comp, "CD", isPublic: true);
        var fldMovesData = GetField<object>(comp, "MD", isPublic: true);
        var fldCreatureID = GetField<int>(comp, "crID");
        var fldMoveIDs = GetField<int[]>(comp, "moveIDs");
        var fldRevive = GetField<bool>(comp, "revive");
        var fldButtons = GetField<KMSelectable[]>(comp, "buttons", isPublic: true);
        var fldCorrectCount = GetField<int>(comp, "correctCount");

        if (comp == null || fldCreatureData == null || fldMovesData == null || fldCreatureID == null || fldMoveIDs == null || fldRevive == null || fldButtons == null || fldCorrectCount == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var creatureData = fldCreatureData.Get();
        var movesData = fldMovesData.Get();
        var buttons = fldButtons.Get();
        if (creatureData == null || movesData == null || buttons == null)
            yield break;
        var buttonNullIndex = Array.IndexOf(buttons, null);
        if (buttons.Length != 4 || buttonNullIndex != -1)
        {
            Debug.LogFormat("<Souvenir #{2}> Abandoning Monsplode, Fight! because unexpected buttons array length ({0}, expected 4) or one of them is null ({1}, expected -1).", buttons.Length, buttonNullIndex, _moduleId);
            yield break;
        }

        var fldCreatureNames = GetField<string[]>(creatureData, "names", isPublic: true);
        var fldMoveNames = GetField<string[]>(movesData, "names", isPublic: true);
        if (fldCreatureNames == null || fldMoveNames == null)
            yield break;

        var creatureNames = fldCreatureNames.Get();
        var moveNames = fldMoveNames.Get();
        if (creatureNames == null || moveNames == null)
            yield break;

        var displayedCreature = new List<string>();
        var displayedMoves = new List<string[]>();
        var pushedMoves = new List<int>();
        var correctMoves = new List<bool>();
        var finished = false;

        var origInteracts = buttons.Select(btn => btn.OnInteract).ToArray();
        for (int i = 0; i < buttons.Length; i++)
        {
            // Need an extra scope to work around bug in Mono 2.0 C# compiler
            new Action<int>(j =>
            {
                buttons[j].OnInteract = delegate
                {
                    // Before processing the button push, get the creature and moves
                    string curCreatureName = null;
                    string[] curMoveNames = null;

                    var creatureID = fldCreatureID.Get();
                    if (creatureID < 0 || creatureID >= creatureNames.Length || string.IsNullOrEmpty(creatureNames[creatureID]))
                        Debug.LogFormat("<Souvenir #{2}> Monsplode, Fight!: Unexpected creature ID: {0}; creature names are: [{1}]", creatureID, creatureNames.Select(cn => cn == null ? "null" : '"' + cn + '"').JoinString(", "), _moduleId);
                    else
                    {
                        var moveIDs = fldMoveIDs.Get();
                        if (moveIDs == null || moveIDs.Length != 4 || moveIDs.Any(mid => mid >= moveNames.Length || string.IsNullOrEmpty(moveNames[mid])))
                            Debug.LogFormat("<Souvenir #{2}> Monsplode, Fight!: Unexpected move IDs: {0}; moves names are: [{1}]",
                                moveIDs == null ? null : "[" + moveIDs.JoinString(", ") + "]",
                                moveNames.Select(mn => mn == null ? "null" : '"' + mn + '"').JoinString(", "),
                                _moduleId);
                        else
                        {
                            curCreatureName = creatureNames[creatureID];
                            curMoveNames = moveIDs.Select(mid => moveNames[mid].Replace("\r", "").Replace("\n", " ")).ToArray();
                        }
                    }

                    var prevCorrectCount = fldCorrectCount.Get();
                    var ret = origInteracts[j]();

                    if (curCreatureName == null || curMoveNames == null)
                    {
                        Debug.LogFormat("<Souvenir #{0}> Monsplode, Fight!: Abandoning due to error above.", _moduleId);
                        // Set these to null to signal that something went wrong and we need to abort
                        displayedCreature = null;
                        displayedMoves = null;
                        pushedMoves = null;
                        correctMoves = null;
                        finished = true;
                    }
                    else
                    {
                        var wasCorrect = fldCorrectCount.Get() > prevCorrectCount;
                        // If ‘revive’ is ‘false’, there is not going to be another stage.
                        if (!fldRevive.Get())
                            finished = true;

                        if (curCreatureName != null && curMoveNames != null && displayedCreature != null && displayedMoves != null)
                        {
                            displayedCreature.Add(curCreatureName);
                            displayedMoves.Add(curMoveNames);
                            pushedMoves.Add(j);
                            correctMoves.Add(wasCorrect);
                        }
                    }
                    return ret;
                };
            })(i);
        }

        while (!finished)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MonsplodeFight);

        for (int i = 0; i < buttons.Length; i++)
            buttons[i].OnInteract = origInteracts[i];

        if (displayedCreature == null || displayedMoves == null)
            yield break;

        if (displayedCreature.Count != displayedMoves.Count || displayedCreature.Count != pushedMoves.Count || displayedCreature.Count != correctMoves.Count)
        {
            Debug.LogFormat("<Souvenir #{4}> Monsplode, Fight!: Inconsistent list lengths: {0}, {1}, {2}, {3}.", displayedCreature.Count, displayedMoves.Count, pushedMoves.Count, correctMoves.Count, _moduleId);
            yield break;
        }

        var attr = _attributes.Get(Question.MonsplodeFightMove);
        var allDisplayedCreatures = displayedCreature.ToArray();
        var allDisplayedMoves = displayedMoves.SelectMany(x => x).Distinct().ToArray();
        var qs = new List<QandA>();
        for (int i = 0; i < displayedCreature.Count; i++)
        {
            qs.Add(makeQuestion(Question.MonsplodeFightCreature, _MonsplodeFight, new[] { displayedCreature[i] }, new[] { displayedCreature.Count == 1 ? "" : ordinal(i + 1) + " " }, allDisplayedCreatures));
            var ord = displayedCreature.Count == 1 ? "" : string.Format("for the {0} creature ", ordinal(i + 1));
            qs.Add(makeQuestion(Question.MonsplodeFightMove, _MonsplodeFight, displayedMoves[i], new[] { "was", ord }, allDisplayedMoves));
            if (attr != null && attr.AllAnswers != null)
                qs.Add(makeQuestion(Question.MonsplodeFightMove, _MonsplodeFight, attr.AllAnswers.Except(displayedMoves[i]).ToArray(), new[] { "was not", ord }, allDisplayedMoves));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessMonsplodeTradingCards(KMBombModule module)
    {
        var comp = GetComponent(module, "MonsplodeCardModule");
        var fldStage = GetField<int>(comp, "correctOffer", isPublic: true);
        var fldStageCount = GetField<int>(comp, "offerCount", isPublic: true);
        var fldDeck = GetField<Array>(comp, "deck", isPublic: true);
        var fldOffer = GetField<object>(comp, "offer", isPublic: true);
        var fldData = GetField<object>(comp, "CD", isPublic: true);

        if (comp == null || fldStage == null || fldStageCount == null || fldDeck == null || fldOffer == null || fldData == null)
            yield break;

        yield return null;

        var stageCount = fldStageCount.Get();
        if (stageCount != 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Monsplode Trading Cards because ‘offerCount’ has unexpected value {1} instead of 3.", _moduleId, stageCount);
            yield break;
        }
        var data = fldData.Get();
        if (data == null)
            yield break;
        var fldNames = GetField<string[]>(data, "names", isPublic: true);
        if (fldNames == null)
            yield break;
        var monsplodeNames = fldNames.Get();
        if (monsplodeNames == null)
            yield break;

        while (fldStage.Get() < stageCount)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MonsplodeTradingCards);

        if (fldStage.Get() != stageCount)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Monsplode Trading Cards because ‘correctOffer’ has unexpected value {1} instead of {2}.", _moduleId, fldStage.Get(), stageCount);
            yield break;
        }

        var deckRaw = fldDeck.Get();
        var offer = fldOffer.Get();
        if (deckRaw == null || offer == null)
            yield break;
        var deck = deckRaw.Cast<object>().ToArray();
        if (deck.Length != 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Monsplode Trading Cards because ‘deck’ has unexpected length {1} instead of 3.", _moduleId, deck.Length);
            yield break;
        }

        var fldMonsplode = GetField<int>(offer, "monsplode", isPublic: true);
        var fldRarity = GetField<int>(offer, "rarity", isPublic: true);
        var fldPrintDigit = GetField<int>(offer, "printDigit", isPublic: true);
        var fldPrintChar = GetField<char>(offer, "printChar", isPublic: true);
        if (fldMonsplode == null || fldRarity == null || fldPrintDigit == null || fldPrintChar == null)
            yield break;

        var monsplodeIds = new[] { fldMonsplode.Get() }.Concat(deck.Select(card => fldMonsplode.GetFrom(card))).ToArray();
        if (monsplodeIds.Any(monsplode => monsplode < 0 || monsplode >= monsplodeNames.Length))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Monsplode Trading Cards because of an unexpected Monsplode ({1}). Names are [{2}].", _moduleId, monsplodeIds.JoinString(", "), monsplodeNames.JoinString(", "));
            yield break;
        }
        var monsplodes = monsplodeIds.Select(mn => monsplodeNames[mn].Replace("\r", "").Replace("\n", " ")).ToArray();
        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsCards, _MonsplodeTradingCards, new[] { monsplodes[0] }, new[] { "card on offer" }, monsplodeNames));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsCards, _MonsplodeTradingCards, new[] { monsplodes[1] }, new[] { "first card in your hand" }, monsplodeNames));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsCards, _MonsplodeTradingCards, new[] { monsplodes[2] }, new[] { "second card in your hand" }, monsplodeNames));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsCards, _MonsplodeTradingCards, new[] { monsplodes[3] }, new[] { "third card in your hand" }, monsplodeNames));

        var rarityNames = new[] { "common", "uncommon", "rare", "ultra rare" };
        var rarityIds = new[] { fldRarity.Get() }.Concat(deck.Select(card => fldRarity.GetFrom(card))).ToArray();
        if (rarityIds.Any(rarity => rarity < 0 || rarity >= rarityNames.Length))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Monsplode Trading Cards because of an unexpected rarity ({1}). Names are [{2}].", _moduleId, rarityIds.JoinString(", "), rarityNames.JoinString(", "));
            yield break;
        }
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsRarities, _MonsplodeTradingCards, new[] { rarityNames[rarityIds[0]] }, new[] { "card on offer" }));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsRarities, _MonsplodeTradingCards, new[] { rarityNames[rarityIds[1]] }, new[] { "first card in your hand" }));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsRarities, _MonsplodeTradingCards, new[] { rarityNames[rarityIds[2]] }, new[] { "second card in your hand" }));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsRarities, _MonsplodeTradingCards, new[] { rarityNames[rarityIds[3]] }, new[] { "third card in your hand" }));

        var printVersions = new[] { fldPrintChar.Get() + "" + fldPrintDigit.Get() }.Concat(deck.Select(card => fldPrintChar.GetFrom(card) + "" + fldPrintDigit.GetFrom(card))).ToArray();
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsPrintVersions, _MonsplodeTradingCards, new[] { printVersions[0] }, new[] { "card on offer" }, printVersions));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsPrintVersions, _MonsplodeTradingCards, new[] { printVersions[1] }, new[] { "first card in your hand" }, printVersions));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsPrintVersions, _MonsplodeTradingCards, new[] { printVersions[2] }, new[] { "second card in your hand" }, printVersions));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsPrintVersions, _MonsplodeTradingCards, new[] { printVersions[3] }, new[] { "third card in your hand" }, printVersions));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessMoon(KMBombModule module)
    {
        var comp = GetComponent(module, "theMoonScript");
        var fldStage = GetField<int>(comp, "stage");
        var fldLightIndex = GetField<int>(comp, "lightIndex");

        if (comp == null || fldStage == null || fldLightIndex == null)
            yield break;

        // The Moon sets ‘stage’ to 9 when the module is solved.
        while (fldStage.Get() != 9)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Moon);

        var lightIndex = fldLightIndex.Get();
        if (lightIndex < 0 || lightIndex >= 8)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning The Moon because ‘lightIndex’ has unexpected value {1}.", _moduleId, lightIndex);
            yield break;
        }

        var qNames = new[] { "first initially lit", "second initially lit", "third initially lit", "fourth initially lit", "first initially unlit", "second initially unlit", "third initially unlit", "fourth initially unlit" };
        var aNames = new[] { "south", "south-west", "west", "north-west", "north", "north-east", "east", "south-east" };
        addQuestions(module, Enumerable.Range(0, 8).Select(i => makeQuestion(Question.MoonLitUnlit, _Moon, new[] { aNames[(i + lightIndex) % 8] }, new[] { qNames[i] })));
    }

    private IEnumerable<object> ProcessMorseAMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "MorseAMaze");
        var fldSolved = GetField<bool>(comp, "_solved");
        var fldStart = GetField<string>(comp, "_souvenirQuestionStartingLocation");
        var fldEnd = GetField<string>(comp, "_souvenirQuestionEndingLocation");
        var fldWord = GetField<string>(comp, "_souvenirQuestionWordPlaying");
        var fldWords = GetField<string[]>(comp, "_souvenirQuestionWordList");

        if (comp == null || fldSolved == null || fldStart == null || fldEnd == null || fldWord == null || fldWords == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var start = fldStart.Get();
        var end = fldEnd.Get();
        var word = fldWord.Get();
        var words = fldWords.Get();
        if (start == null || start.Length != 2)
        {
            Debug.LogFormat("<Souvenir #{0}> Morse-A-Maze starting coordinate is null or has unexpected value: {1}", _moduleId, start ?? "<null>");
            yield break;
        }
        if (end == null || end.Length != 2)
        {
            Debug.LogFormat("<Souvenir #{0}> Morse-A-Maze ending coordinate is null or has unexpected value: {1}", _moduleId, end ?? "<null>");
            yield break;
        }
        if (word == null || word.Length < 4)
        {
            Debug.LogFormat("<Souvenir #{0}> Morse-A-Maze morse code word is null or has unexpected value: {1}", _moduleId, word ?? "<null>");
            yield break;
        }
        if (words == null || words.Length != 36)
        {
            Debug.LogFormat("<Souvenir #{0}> Morse-A-Maze word list is null or its length is not 36: {1}", _moduleId, words == null ? "<null>" : words.Length.ToString());
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_MorseAMaze);
        addQuestions(module,
            makeQuestion(Question.MorseAMazeStartingCoordinate, _MorseAMaze, new[] { start }),
            makeQuestion(Question.MorseAMazeEndingCoordinate, _MorseAMaze, new[] { end }),
            makeQuestion(Question.MorseAMazeMorseCodeWord, _MorseAMaze, new[] { word }, preferredWrongAnswers: words));
    }

    private IEnumerable<object> ProcessMorsematics(KMBombModule module)
    {
        var comp = GetComponent(module, "AdvancedMorse");
        var fldSolved = GetField<bool>(comp, "solved");
        var fldChars = GetField<string[]>(comp, "DisplayCharsRaw");

        if (comp == null || fldSolved == null)
            yield break;

        yield return null;

        var chars = fldChars.Get();
        if (chars == null)
            yield break;
        if (chars.Length != 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Morsematics: Unexpected length of DisplayCharsRaw array ({1} instead of 3).", _moduleId, chars.Length);
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Morsematics);
        addQuestions(module, Enumerable.Range(0, 3).Select(i => makeQuestion(Question.MorsematicsReceivedLetters, _Morsematics, new[] { chars[i] }, new[] { ordinal(i + 1) }, chars)));
    }

    private IEnumerable<object> ProcessMouseInTheMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "Maze_3d");
        var fldSphereColors = GetField<int[]>(comp, "sphereColors");
        var fldTorusColor = GetField<int>(comp, "torusColor");
        var fldGoalPosition = GetField<int>(comp, "goalPosition");
        var fldIsSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldSphereColors == null || fldTorusColor == null || fldGoalPosition == null || fldIsSolved == null)
            yield break;

        var sphereColors = fldSphereColors.Get();
        if (sphereColors == null)
            yield break;
        if (sphereColors.Length != 4)
        {
            Debug.LogFormat("<Souvenir #{1}> Mouse in the Maze: sphereColors has unexpected length ({0}; expected 4).", sphereColors.Length, _moduleId);
            yield break;
        }

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var goalPos = fldGoalPosition.Get();
        if (goalPos < 0 || goalPos >= 4)
        {
            Debug.LogFormat("<Souvenir #{1}> Mouse in the Maze: Unexpected goalPos ({0}; expected 0 to 3).", goalPos, _moduleId);
            yield break;
        }

        var torusColor = fldTorusColor.Get();
        var goalColor = sphereColors[goalPos];
        if (torusColor < 0 || torusColor >= 4 || goalColor < 0 || goalColor >= 4)
        {
            Debug.LogFormat("<Souvenir #{2}> Mouse in the Maze: Unexpected color (torus={0}; goal={1}).", torusColor, goalColor, _moduleId);
            yield break;
        }

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_MouseInTheMaze);
        addQuestions(module,
            makeQuestion(Question.MouseInTheMazeSphere, _MouseInTheMaze, new[] { new[] { "white", "green", "blue", "yellow" }[goalColor] }),
            makeQuestion(Question.MouseInTheMazeTorus, _MouseInTheMaze, new[] { new[] { "white", "green", "blue", "yellow" }[torusColor] }));
    }

    private IEnumerable<object> ProcessMurder(KMBombModule module)
    {
        var comp = GetComponent(module, "MurderModule");
        var fldSolved = GetField<bool>(comp, "isSolved");
        var fldSolution = GetField<int[]>(comp, "solution");
        var fldNames = GetField<string[,]>(comp, "names");
        var fldSkipDisplay = GetField<int[,]>(comp, "skipDisplay");
        var fldSuspects = GetField<int>(comp, "suspects");
        var fldWeapons = GetField<int>(comp, "weapons");
        var fldBodyFound = GetField<int>(comp, "bodyFound");

        if (comp == null || fldSolved == null || fldNames == null || fldSkipDisplay == null || fldSuspects == null || fldWeapons == null || fldBodyFound == null)
            yield break;

        yield return null;

        if (fldSuspects.Get() != 4 || fldWeapons.Get() != 4)
        {
            Debug.LogFormat("<Souvenir #{0}> Murder: Unexpected number of suspects ({1} instead of 4) or weapons ({2} instead of 4).", _moduleId, fldSuspects.Get(), fldWeapons.Get());
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Murder);

        var solution = fldSolution.Get();
        var skipDisplay = fldSkipDisplay.Get();
        var names = fldNames.Get();
        if (solution == null || skipDisplay == null || names == null)
            yield break;
        if (solution.Length != 3 || skipDisplay.GetLength(0) != 2 || skipDisplay.GetLength(1) != 6 || names.GetLength(0) != 3 || names.GetLength(1) != 9)
        {
            Debug.LogFormat("<Souvenir #{0}> Murder: Unexpected length of solution array ({1} instead of 3) or solution array ({2}/{3} instead of 2/6) or names array ({4}/{5} instead of 3/9).", _moduleId, solution.Length, skipDisplay.GetLength(0), skipDisplay.GetLength(1), names.GetLength(0), names.GetLength(1));
            yield break;
        }

        var actualSuspect = solution[0];
        var actualWeapon = solution[1];
        var actualRoom = solution[2];
        var bodyFound = fldBodyFound.Get();
        if (actualSuspect < 0 || actualSuspect >= 6 || actualWeapon < 0 || actualWeapon >= 6 || actualRoom < 0 || actualRoom >= 9 || bodyFound < 0 || bodyFound >= 9)
        {
            Debug.LogFormat("<Souvenir #{0}> Murder: Unexpected suspect, weapon, room or bodyFound (expected 0–5/0–5/0–8/0–8, got {1}/{2}/{3}/{4}).", _moduleId, actualSuspect, actualWeapon, actualRoom, bodyFound);
            yield break;
        }

        addQuestions(module,
            makeQuestion(Question.MurderSuspect, _Murder,
                Enumerable.Range(0, 6).Where(suspectIx => skipDisplay[0, suspectIx] == 0 && suspectIx != actualSuspect).Select(suspectIx => names[0, suspectIx]).ToArray(),
                new[] { "a suspect but not the murderer" }),
            makeQuestion(Question.MurderSuspect, _Murder,
                Enumerable.Range(0, 6).Where(suspectIx => skipDisplay[0, suspectIx] == 1).Select(suspectIx => names[0, suspectIx]).ToArray(),
                new[] { "not a suspect" }),

            makeQuestion(Question.MurderWeapon, _Murder,
                Enumerable.Range(0, 6).Where(weaponIx => skipDisplay[1, weaponIx] == 0 && weaponIx != actualWeapon).Select(weaponIx => names[1, weaponIx]).ToArray(),
                new[] { "a potential weapon but not the murder weapon" }),
            makeQuestion(Question.MurderWeapon, _Murder,
                Enumerable.Range(0, 6).Where(weaponIx => skipDisplay[1, weaponIx] == 1).Select(weaponIx => names[1, weaponIx]).ToArray(),
                new[] { "not a potential weapon" }),

            bodyFound == actualRoom ? null : makeQuestion(Question.MurderBodyFound, _Murder, new[] { names[2, bodyFound] }));
    }

    private IEnumerable<object> ProcessNeutralization(KMBombModule module)
    {
        var comp = GetComponent(module, "neutralization");
        var fldAcidType = GetField<int>(comp, "acidType");
        var fldAcidVol = GetField<int>(comp, "acidVol");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        if (comp == null || fldAcidType == null || fldAcidVol == null || fldSolved == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var acidType = fldAcidType.Get();
        if (acidType < 0 || acidType > 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Neutralization: Unexpected acid type: {1}", _moduleId, acidType);
            yield break;
        }
        var acidVol = fldAcidVol.Get();
        if (acidVol < 5 || acidVol > 20 || acidVol % 5 != 0)
        {
            Debug.LogFormat("<Souvenir #{0}> Neutralization: Unexpected acid volume: {1}", _moduleId, acidVol);
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Neutralization);
        addQuestions(module,
            makeQuestion(Question.NeutralizationColor, _Neutralization, new[] { new[] { "Yellow", "Green", "Red", "Blue" }[acidType] }),
            makeQuestion(Question.NeutralizationVolume, _Neutralization, new[] { acidVol.ToString() }));
    }

    private IEnumerable<object> ProcessOnlyConnect(KMBombModule module)
    {
        var comp = GetComponent(module, "OnlyConnectModule");
        var fldHieroglyphsDisplayed = GetField<int[]>(comp, "_hieroglyphsDisplayed");
        var fldIsSolved = GetField<bool>(comp, "_isSolved");
        if (comp == null || fldHieroglyphsDisplayed == null || fldIsSolved == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var hieroglyphsDisplayed = fldHieroglyphsDisplayed.Get();
        if (hieroglyphsDisplayed == null || hieroglyphsDisplayed.Length != 6 || hieroglyphsDisplayed.Any(h => h < 0 || h >= 6))
        {
            Debug.LogFormat("<Souvenir #{0}> Only Connect: hieroglyphsDisplayed has unexpected value: {1}", _moduleId,
                hieroglyphsDisplayed == null ? "null" : string.Format("[{0}]", hieroglyphsDisplayed.JoinString(", ")));
            yield break;
        }

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_OnlyConnect);

        var hieroglyphs = new[] { "Two Reeds", "Lion", "Twisted Flax", "Horned Viper", "Water", "Eye of Horus" };
        var positions = new[] { "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right" };
        addQuestions(module, positions.Select((p, i) => makeQuestion(Question.OnlyConnectHieroglyphs, _OnlyConnect, new[] { hieroglyphs[hieroglyphsDisplayed[i]] }, new[] { p })));
    }

    private IEnumerable<object> ProcessOrientationCube(KMBombModule module)
    {
        var comp = GetComponent(module, "OrientationModule");
        var fldInitialVirtualViewAngle = GetField<float>(comp, "initialVirtualViewAngle");
        var fldSubmitButton = GetField<KMSelectable>(comp, "SubmitButton", isPublic: true);
        var mthGetRule = GetMethod<object>(comp, "GetRule", 0);
        var mthIsFacing = GetMethod<bool>(comp, "IsFacing", 2);
        if (comp == null || fldInitialVirtualViewAngle == null || fldSubmitButton == null || mthGetRule == null || mthIsFacing == null)
            yield break;

        // Wait for one frame to ensure Orientation Cube has set initialVirtualViewAngle in its Start()
        yield return null;

        var initialVirtualViewAngle = fldInitialVirtualViewAngle.Get();
        var initialAnglePos = Array.IndexOf(new[] { 0f, 90f, 180f, 270f }, initialVirtualViewAngle);
        if (initialAnglePos == -1)
        {
            Debug.LogFormat("<Souvenir #{1}> Orientation Cube: initialVirtualViewAngle has unexpected value: {0}", initialVirtualViewAngle, _moduleId);
            yield break;
        }

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var submitButton = fldSubmitButton.Get();
        if (submitButton == null)
            yield break;

        var prevInteract = submitButton.OnInteract;
        var solved = false;

        submitButton.OnInteract = delegate
        {
            var rule = mthGetRule.Invoke();
            var fldFromFacing = GetField<Vector3>(rule, "fromFacing", isPublic: true);
            var fldToFacing = GetField<Vector3>(rule, "toFacing", isPublic: true);
            var fldHasSecondaryRule = GetField<bool>(rule, "hasSecondaryRule", isPublic: true);
            var fldSecondaryFromFacing = GetField<Vector3>(rule, "secondaryFromFacing", isPublic: true);
            var fldSecondaryToFacing = GetField<Vector3>(rule, "secondaryToFacing", isPublic: true);

            if (rule == null || fldFromFacing == null || fldToFacing == null || fldHasSecondaryRule == null || fldSecondaryFromFacing == null || fldSecondaryToFacing == null || submitButton == null)
            {
                Debug.LogFormat("<Souvenir #{0}> Abandoning Orientation Cube.", _moduleId);
                submitButton.OnInteract = prevInteract;
            }
            else if (mthIsFacing.Invoke(fldFromFacing.GetFrom(rule), fldToFacing.GetFrom(rule)) &&
                    !fldHasSecondaryRule.GetFrom(rule) || mthIsFacing.Invoke(fldSecondaryFromFacing.GetFrom(rule), fldSecondaryToFacing.GetFrom(rule)))
            {
                solved = true;
                submitButton.OnInteract = prevInteract;
            }
            return prevInteract();
        };

        while (!solved)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_OrientationCube);

        addQuestion(module, Question.OrientationCubeInitialObserverPosition, new[] { new[] { "front", "left", "back", "right" }[initialAnglePos] });
    }

    private IEnumerable<object> ProcessPatternCube(KMBombModule module)
    {
        var comp = GetComponent(module, "PatternCubeModule");
        var fldSelectableSymbols = GetField<Array>(comp, "_selectableSymbols");
        var fldHighlightedPosition = GetField<int>(comp, "_highlightedPosition");

        yield return null;
        var selectableSymbols = fldSelectableSymbols.Get();
        if (selectableSymbols == null || selectableSymbols.Length != 5)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Pattern Cube because _selectableSymbols {1} (expected length 5).", _moduleId, selectableSymbols == null ? "was null" : "had length " + selectableSymbols.Length);
            yield break;
        }
        while (selectableSymbols.Cast<object>().Any(obj => obj != null))
            yield return new WaitForSeconds(.1f);

        var highlightPos = fldHighlightedPosition.Get();
        if (highlightPos < 0 || highlightPos > 4)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Pattern Cube because _highlightedPosition was {1} (expected 0–4).", _moduleId, highlightPos);
            yield break;
        }

        _modulesSolved.IncSafe(_PatternCube);
        addQuestions(module,
            makeQuestion(Question.PatternCubeHighlightPosition, _PatternCube, new[] { ordinal(highlightPos + 1) }, new[] { "top" }),
            makeQuestion(Question.PatternCubeHighlightPosition, _PatternCube, new[] { ordinal(5 - highlightPos) }, new[] { "bottom" }));
    }

    private IEnumerable<object> ProcessPerspectivePegs(KMBombModule module)
    {
        var comp = GetComponent(module, "PerspectivePegsModule");
        var fldIsComplete = GetField<bool>(comp, "isComplete");
        var fldEnteredSequence = GetField<List<int>>(comp, "EnteredSequence");
        if (comp == null || fldIsComplete == null || fldEnteredSequence == null)
            yield break;

        while (!fldIsComplete.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PerspectivePegs);

        var entered = fldEnteredSequence.Get();
        if (entered == null)
            yield break;
        if (entered.Count != 3 || entered.Any(e => e < 0 || e >= 5))
        {
            Debug.LogFormat("<Souvenir #{1}> Perspective Pegs: EnteredSequence has unrecognized member or unexpected length: [{0}]", entered.JoinString(", "), _moduleId);
            yield break;
        }

        var theory = new[] { "top", "top right", "bottom right", "bottom left", "top left" };
        addQuestions(module, Enumerable.Range(0, 3).Select(i => makeQuestion(
             Question.PerspectivePegsSolution,
             _PerspectivePegs,
             new[] { theory[entered[i]] },
             extraFormatArguments: new[] { ordinal(i + 1) },
             preferredWrongAnswers: entered.Select(e => theory[e]).ToArray())));
    }

    private IEnumerable<object> ProcessPolyhedralMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "PolyhedralMazeModule");
        var fldStartFace = GetField<int>(comp, "_startFace");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldStartFace == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_PolyhedralMaze);
        addQuestion(module, Question.PolyhedralMazeStartPosition, new[] { fldStartFace.Get().ToString() }, null, Enumerable.Range(0, 62).Select(i => i.ToString()).ToArray());
    }

    private IEnumerable<object> ProcessQuintuples(KMBombModule module)
    {
        var comp = GetComponent(module, "quinaryNumbersScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldNumbers = GetField<int[]>(comp, "cyclingNumbers", isPublic: true);
        var fldColors = GetField<string[]>(comp, "chosenColorsName", isPublic: true);
        var fldColorCounts = GetField<int[]>(comp, "numberOfEachColour", isPublic: true);
        var fldColorNames = GetField<string[]>(comp, "potentialColorsName", isPublic: true);

        if (comp == null || fldSolved == null || fldNumbers == null || fldColors == null || fldColorCounts == null || fldColorNames == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Quintuples);

        var numbers = fldNumbers.Get();
        var colors = fldColors.Get();
        var colorCounts = fldColorCounts.Get();
        var colorNames = fldColorNames.Get();
        if (numbers == null || numbers.Length != 25 || numbers.Any(n => n < 1 || n > 10) ||
            colors == null || colors.Length != 25 ||
            colorCounts == null || colorCounts.Length != 5 || colorCounts.Any(cc => cc < 0 || cc > 25) ||
            colorNames == null || colorNames.Length != 5)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Quintuples because an array has unexpected length or values: (numbers={1} / colors={2} / numberOfEachColour={3} / potentialColorsName={4})", _moduleId,
                numbers == null ? "<null>" : string.Format("[{0}]", numbers.JoinString(", ")),
                colors == null ? "<null>" : string.Format("[{0}]", colors.Select(c => string.Format(@"""{0}""", c)).JoinString(", ")),
                colorCounts == null ? "<null>" : string.Format("[{0}]", colorCounts.JoinString(", ")),
                colorNames == null ? "<null>" : string.Format("[{0}]", colorNames.JoinString(", ")));
            yield break;
        }

        addQuestions(module,
            numbers.Select((n, ix) => makeQuestion(Question.QuintuplesNumbers, _Quintuples, new[] { (n % 10).ToString() }, new[] { ordinal(ix % 5 + 1), ordinal(ix / 5 + 1) })).Concat(
            colors.Select((color, ix) => makeQuestion(Question.QuintuplesColors, _Quintuples, new[] { color }, new[] { ordinal(ix % 5 + 1), ordinal(ix / 5 + 1) }))).Concat(
            colorCounts.Select((cc, ix) => makeQuestion(Question.QuintuplesColorCounts, _Quintuples, new[] { cc.ToString() }, new[] { colorNames[ix] }))));
    }

    private IEnumerable<object> ProcessRhythms(KMBombModule module)
    {
        var comp = GetComponent(module, "Rhythms");
        var fldSolved = GetField<bool>(comp, "isSolved", isPublic: true);
        var fldColor = GetField<int>(comp, "lightColor");

        if (comp == null || fldSolved == null || fldColor == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Rhythms);

        var color = fldColor.Get();
        if (color < 0 || color >= 4)
            Debug.LogFormat("<Souvenir #{0}> Abandoning Rhythms because lightColor has unexpected value ({1}).", _moduleId, color);
        else
            addQuestion(module, Question.RhythmsColor, new[] { new[] { "Blue", "Red", "Green", "Yellow" }[color] });
    }

    private IEnumerable<object> ProcessSeaShells(KMBombModule module)
    {
        var comp = GetComponent(module, "SeaShellsModule");
        var fldRow = GetField<int>(comp, "row");
        var fldCol = GetField<int>(comp, "col");
        var fldKeynum = GetField<int>(comp, "keynum");
        var fldStage = GetField<int>(comp, "stage");
        var fldSolved = GetField<bool>(comp, "isPassed");
        var fldDisplay = GetField<TextMesh>(comp, "Display", isPublic: true);

        if (comp == null || fldRow == null || fldCol == null || fldKeynum == null || fldStage == null || fldSolved == null || fldDisplay == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var rows = new List<int>();
        var cols = new List<int>();
        var keynums = new List<int>();
        while (true)
        {
            while (fldDisplay.Get().text == " ")
            {
                yield return new WaitForSeconds(.1f);
                if (fldSolved.Get())
                    goto solved;
            }

            rows.Add(fldRow.Get());
            cols.Add(fldCol.Get());
            keynums.Add(fldKeynum.Get());

            while (fldDisplay.Get().text != " ")
            {
                yield return new WaitForSeconds(.1f);
                if (fldSolved.Get())
                    goto solved;
            }
        }

        solved:
        _modulesSolved.IncSafe(_SeaShells);

        var qs = new List<QandA>();
        for (int i = 0; i < rows.Count; i++)
        {
            qs.Add(makeQuestion(Question.SeaShells1, _SeaShells, new[] { new[] { "she sells", "she shells", "sea shells", "sea sells" }[rows[i]] }, new[] { ordinal(i + 1) }));
            qs.Add(makeQuestion(Question.SeaShells2, _SeaShells, new[] { new[] { "sea shells", "she shells", "sea sells", "she sells" }[cols[i]] }, new[] { ordinal(i + 1) }));
            qs.Add(makeQuestion(Question.SeaShells3, _SeaShells, new[] { new[] { "sea shore", "she sore", "she sure", "seesaw" }[keynums[i]] }, new[] { ordinal(i + 1) }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessShapeShift(KMBombModule module)
    {
        var comp = GetComponent(module, "ShapeShiftModule");
        var fldSolved = GetField<bool>(comp, "isSolved");
        var fldStartL = GetField<int>(comp, "startL");
        var fldStartR = GetField<int>(comp, "startR");
        var fldSolutionL = GetField<int>(comp, "solutionL");
        var fldSolutionR = GetField<int>(comp, "solutionR");

        if (comp == null || fldSolved == null || fldStartL == null || fldStartR == null || fldSolutionL == null || fldSolutionR == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ShapeShift);

        var stL = fldStartL.Get();
        var stR = fldStartR.Get();
        var solL = fldSolutionL.Get();
        var solR = fldSolutionR.Get();
        var answers = new HashSet<string>();
        for (int l = 0; l < 4; l++)
            if (stL != solL || l == stL)
                for (int r = 0; r < 4; r++)
                    if (stR != solR || r == stR)
                        answers.Add(((char) ('A' + r + (4 * l))).ToString());
        if (answers.Count < 4)
        {
            Debug.LogFormat("[Souvenir #{0}] No question for Shape Shift because the answer was the same as the initial state.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
        }
        else
            addQuestion(module, Question.ShapeShiftInitialShape, new[] { ((char) ('A' + stR + (4 * stL))).ToString() }, preferredWrongAnswers: answers.ToArray());
    }

    private IEnumerable<object> ProcessSillySlots(KMBombModule module)
    {
        var comp = GetComponent(module, "SillySlots");
        var fldSolved = GetField<bool>(comp, "solved");
        var fldPrevSlots = GetField<IList>(comp, "mPreviousSlots");

        if (comp == null || fldSolved == null || fldPrevSlots == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_SillySlots);

        var prevSlots = fldPrevSlots.Get();
        if (prevSlots == null)
            yield break;
        if (prevSlots.Count < 2)
        {
            // Legitimate: first stage was a keep already
            Debug.LogFormat("[Souvenir #{0}] No question for Silly Slots because there was only one stage.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        if (prevSlots.Cast<object>().Any(obj => !(obj is Array) || ((Array) obj).Length != 3))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Silly Slots because prevSlots {1}.",
                _moduleId,
                prevSlots == null ? "is null" :
                prevSlots.Count == 0 ? "has length 0" :
                string.Format("has an unexpected item (expected arrays of length 3): [{0}]", prevSlots.Cast<object>().Select(obj => obj == null ? "<null>" : !(obj is Array) ? string.Format("<{0}>", obj.GetType().FullName) : string.Format("<Array, length={0}>", ((Array) obj).Length)).JoinString(", ")));
            yield break;
        }

        var testSlot = ((Array) prevSlots[0]).GetValue(0);
        var fldShape = GetField<object>(testSlot, "shape", isPublic: true);
        var fldColor = GetField<object>(testSlot, "color", isPublic: true);
        if (fldShape == null || fldColor == null)
            yield break;

        var qs = new List<QandA>();
        // Skip the last stage because if the last action was Keep, it is still visible on the module
        for (int stage = 0; stage < prevSlots.Count - 1; stage++)
        {
            var slotStrings = ((Array) prevSlots[stage]).Cast<object>().Select(obj => (fldColor.GetFrom(obj).ToString() + " " + fldShape.GetFrom(obj).ToString()).ToLowerInvariant()).ToArray();
            for (int slot = 0; slot < slotStrings.Length; slot++)
                qs.Add(makeQuestion(Question.SillySlots, _SillySlots, new[] { slotStrings[slot] }, new[] { ordinal(slot + 1), ordinal(stage + 1) }, slotStrings));
        }
        addQuestions(module, qs);
    }

    private static readonly string[] _simonSamplesFAs = new[] { "played in the first stage", "added in the second stage", "added in the third stage" };
    private IEnumerable<object> ProcessSimonSamples(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSamples");
        var fldCalls = GetField<List<string>>(comp, "_calls");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldCalls == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonSamples);

        var calls = fldCalls.Get();
        if (calls == null || calls.Count != 3 || Enumerable.Range(1, 2).Any(i => calls[i].Length <= calls[i - 1].Length || !calls[i].StartsWith(calls[i - 1])))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Samples because _calls={1} (expected length 3 and expected each element to start with the previous).", _moduleId, calls == null ? "<null>" : string.Format("[{0}]", calls.Select(c => string.Format(@"""{0}""", c)).JoinString(", ")));
            yield break;
        }

        addQuestions(module, calls.Select((c, ix) => makeQuestion(Question.SimonSamplesSamples, _SimonSamples, new[] { (ix == 0 ? c : c.Substring(calls[ix - 1].Length)).Replace("0", "K").Replace("1", "S").Replace("2", "H").Replace("3", "O") }, new[] { _simonSamplesFAs[ix] })));
    }

    private IEnumerable<object> ProcessSimonScreams(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonScreamsModule");
        var fldSequences = GetField<int[][]>(comp, "_sequences");
        var fldColors = GetField<Array>(comp, "_colors");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var fldRowCriteria = GetField<Array>(comp, "_rowCriteria");

        if (comp == null || fldSequences == null || fldColors == null || fldSolved == null || fldRowCriteria == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_SimonScreams);

        var seqs = fldSequences.Get();
        var colorsRaw = fldColors.Get();
        var rules = fldRowCriteria.Get();
        if (seqs == null || colorsRaw == null || fldRowCriteria == null)
            yield break;
        // colorsRaw contains enum values; stringify them.
        var colors = colorsRaw.Cast<object>().Select(obj => obj.ToString()).ToArray();

        if (seqs.Length != 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Screams because _sequences length is {1} (expected 3).", _moduleId, seqs.Length);
            yield break;
        }
        if (colors.Length != 6)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Screams because _colors has length {1} (expected 6).", _moduleId, colors.Length);
            yield break;
        }
        if (rules.Length != 6)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Screams because _rowCriteria has length {1} (expected 6).", _moduleId, rules.Length);
            yield break;
        }

        var qs = new List<QandA>();
        var lastSeq = seqs.Last();
        for (int i = 0; i < lastSeq.Length; i++)
            qs.Add(makeQuestion(Question.SimonScreamsFlashing, _SimonScreams, new[] { colors[lastSeq[i]] }, new[] { ordinal(i + 1) }));

        // First determine which rule applied in which stage
        var fldCheck = GetField<Func<int[], bool>>(rules.GetValue(0), "Check", isPublic: true);
        var fldRuleName = GetField<string>(rules.GetValue(0), "Name", isPublic: true);
        if (fldCheck == null || fldRuleName == null)
            yield break;
        var stageRules = new int[seqs.Length];
        for (int i = 0; i < seqs.Length; i++)
        {
            stageRules[i] = rules.Cast<object>().IndexOf(rule => fldCheck.GetFrom(rule)(seqs[i]));
            if (stageRules[i] == -1)
            {
                Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Screams because apparently none of the criteria applies to Stage {1} ({2}).", _moduleId, i + 1, seqs[i].Select(ix => colors[ix]).JoinString(", "));
                yield break;
            }
        }

        // Now set the questions
        // Skip the last rule because it’s the “otherwise” row
        for (int rule = 0; rule < rules.Length - 1; rule++)
        {
            var applicableStages = new List<string>();
            for (int stage = 0; stage < stageRules.Length; stage++)
                if (stageRules[stage] == rule)
                    applicableStages.Add(ordinal(stage + 1));
            if (applicableStages.Count > 0)
                qs.Add(makeQuestion(Question.SimonScreamsRule, _SimonScreams,
                    new[] { applicableStages.Count == stageRules.Length ? "all of them" : applicableStages.JoinString(", ", lastSeparator: " and ") },
                    new[] { fldRuleName.GetFrom(rules.GetValue(rule)) },
                    applicableStages.Count == 1
                        ? Enumerable.Range(1, seqs.Length).Select(i => ordinal(i)).ToArray()
                        : Enumerable.Range(1, seqs.Length).SelectMany(a => Enumerable.Range(a + 1, seqs.Length - a).Select(b => ordinal(a) + " and " + ordinal(b))).Concat(new[] { "all of them" }).ToArray()));
        }

        addQuestions(module, qs);
    }

    private static readonly string[] _SimonSends_Morse = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };

    private IEnumerable<object> ProcessSimonSends(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSendsModule");
        var fldAnswerSoFar = GetField<List<int>>(comp, "_answerSoFar");
        var fldMorseR = GetField<string>(comp, "_morseR");
        var fldMorseG = GetField<string>(comp, "_morseG");
        var fldMorseB = GetField<string>(comp, "_morseB");

        if (comp == null || fldAnswerSoFar == null || fldMorseR == null || fldMorseG == null || fldMorseB == null)
            yield break;

        yield return null;

        var morseR = fldMorseR.Get();
        var morseG = fldMorseG.Get();
        var morseB = fldMorseB.Get();

        if (morseR == null || morseG == null || morseB == null)
            yield break;

        var charR = ((char) ('A' + Array.IndexOf(_SimonSends_Morse, morseR.Replace("###", "-").Replace("#", ".").Replace("_", "")))).ToString();
        var charG = ((char) ('A' + Array.IndexOf(_SimonSends_Morse, morseG.Replace("###", "-").Replace("#", ".").Replace("_", "")))).ToString();
        var charB = ((char) ('A' + Array.IndexOf(_SimonSends_Morse, morseB.Replace("###", "-").Replace("#", ".").Replace("_", "")))).ToString();

        // Simon Sends sets “_answerSoFar” to null when it’s done
        while (fldAnswerSoFar.Get(nullAllowed: true) != null)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_SimonSends);
        addQuestions(module,
            makeQuestion(Question.SimonSendsReceivedLetters, _SimonSends, new[] { charR }, new[] { "red" }, new[] { charG, charB }),
            makeQuestion(Question.SimonSendsReceivedLetters, _SimonSends, new[] { charG }, new[] { "green" }, new[] { charR, charB }),
            makeQuestion(Question.SimonSendsReceivedLetters, _SimonSends, new[] { charB }, new[] { "blue" }, new[] { charR, charG }));
    }

    private static readonly string[] _SimonSings_Notes = { "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B" };

    private IEnumerable<object> ProcessSimonSings(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSingsModule");
        var fldCurStage = GetField<int>(comp, "_curStage");
        var fldFlashingColors = GetField<int[]>(comp, "_flashingColors");

        if (comp == null || fldCurStage == null || fldFlashingColors == null)
            yield break;

        yield return null;

        var flashingColorSequences = new List<int[]> { fldFlashingColors.Get() };
        var curStage = fldCurStage.Get();
        if (curStage != 0)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Sings because it started at stage {1} instead of 1.", _moduleId, curStage + 1);
            yield break;
        }
        while (curStage < 3)
        {
            yield return new WaitForSeconds(.1f);
            var newStage = fldCurStage.Get();
            if (newStage != curStage && newStage < 3)
            {
                var newColors = fldFlashingColors.Get();
                if (ReferenceEquals(newColors, flashingColorSequences.Last()))
                {
                    Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Sings because stage {1} gave me the same color sequence array (reference equals) as the previous stage.", _moduleId, newStage + 1);
                    yield break;
                }
                flashingColorSequences.Add(newColors);
            }
            curStage = newStage;
        }

        if (flashingColorSequences.Any(seq => seq.Any(col => col < 0 || col >= _SimonSings_Notes.Length)))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Sings because one of the flashing “colors” is out of range (values from 0–11 expected): [{1}].", _moduleId, flashingColorSequences.Select(seq => string.Format("[{0}]", seq.JoinString(", "))).JoinString("; "));
            yield break;
        }

        _modulesSolved.IncSafe(_SimonSings);
        addQuestions(module, flashingColorSequences.SelectMany((seq, stage) => seq.Select((col, ix) => makeQuestion(Question.SimonSingsFlashing, _SimonSings, new[] { _SimonSings_Notes[col] }, new[] { ordinal(ix + 1), ordinal(stage + 1) }))));
    }

    private IEnumerable<object> ProcessSimonStates(KMBombModule module)
    {
        var comp = GetComponent(module, "AdvancedSimon");
        var fldPuzzleDisplay = GetField<bool[][]>(comp, "PuzzleDisplay");
        var fldAnswer = GetField<int[]>(comp, "Answer");
        var fldProgress = GetField<int>(comp, "Progress");

        if (comp == null || fldPuzzleDisplay == null || fldAnswer == null || fldProgress == null)
            yield break;

        bool[][] puzzleDisplay;
        while ((puzzleDisplay = fldPuzzleDisplay.Get(nullAllowed: true)) == null)
            yield return new WaitForSeconds(.1f);

        if (puzzleDisplay.Length != 4 || puzzleDisplay.Any(arr => arr.Length != 4))
        {
            Debug.LogFormat("<Souvenir #{1}> Abandoning Simon States because PuzzleDisplay has an unexpected length or value: [{0}]",
                puzzleDisplay.Select(arr => arr == null ? "null" : "[" + arr.JoinString(", ") + "]").JoinString("; "), _moduleId);
            yield break;
        }

        var colorNames = new[] { "Red", "Yellow", "Green", "Blue" };
        Debug.LogFormat("<Souvenir #{1}> Simon States: PuzzleDisplay = [{0}]",
            puzzleDisplay.Select(arr => arr.Select((v, i) => v ? colorNames[i] : null).Where(x => x != null).JoinString(", ")).JoinString("; ", "[", "]"), _moduleId);

        while (fldProgress.Get() < 4)
            yield return new WaitForSeconds(.1f);
        // Consistency check
        if (fldPuzzleDisplay.Get(nullAllowed: true) != null)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon States because PuzzleDisplay was expected to be null when Progress reached 4, but wasn’t.", _moduleId);
            yield break;
        }

        _modulesSolved.IncSafe(_SimonStates);

        var qs = new List<QandA>();
        for (int i = 0; i < 4; i++)
        {
            var c = puzzleDisplay[i].Count(b => b);
            if (c != 3)
                qs.Add(makeQuestion(Question.SimonStatesDisplay, _SimonStates,
                    new[] { c == 4 ? "all 4" : puzzleDisplay[i].Select((v, j) => v ? colorNames[j] : null).Where(x => x != null).JoinString(", ") },
                    new[] { "color(s) flashed", ordinal(i + 1) }));
            if (c != 1)
                qs.Add(makeQuestion(Question.SimonStatesDisplay, _SimonStates,
                    new[] { c == 4 ? "none" : puzzleDisplay[i].Select((v, j) => v ? null : colorNames[j]).Where(x => x != null).JoinString(", ") },
                    new[] { "color(s) didn’t flash", ordinal(i + 1) }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSkewedSlots(KMBombModule module)
    {
        var comp = GetComponent(module, "SkewedModule");
        var fldNumbers = GetField<int[]>(comp, "Numbers");
        var fldModuleActivated = GetField<bool>(comp, "moduleActivated");
        var fldSolved = GetField<bool>(comp, "solved");

        if (comp == null || fldNumbers == null || fldModuleActivated == null || fldSolved == null)
            yield break;

        var originalNumbers = new List<string>();

        while (true)
        {
            // Skewed Slots sets moduleActivated to false while the slots are spinning.
            // If there was a correct answer, it will set solved to true, otherwise it will set moduleActivated to true.
            while (!fldModuleActivated.Get() && !fldSolved.Get())
                yield return new WaitForSeconds(.1f);

            if (fldSolved.Get())
                break;

            // Get the current original digits.
            var numbers = fldNumbers.Get();
            if (numbers == null)
                yield break;
            if (numbers.Length != 3 || numbers.Any(n => n < 0 || n > 9))
            {
                Debug.LogFormat("<Souvenir #{0}> Abandoning Skewed Slots because numbers has unexpected length (3) or a number outside expected range (0–9): [{1}].", _moduleId, numbers.JoinString(", "));
                yield break;
            }
            originalNumbers.Add(numbers.JoinString());

            // When the user presses anything, Skewed Slots sets moduleActivated to false while the slots are spinning.
            while (fldModuleActivated.Get())
                yield return new WaitForSeconds(.1f);
        }

        _modulesSolved.IncSafe(_SkewedSlots);
        addQuestions(module, originalNumbers.Select((origNum, i) => makeQuestion(Question.SkewedSlotsOriginalNumbers, _SkewedSlots, new[] { origNum },
                 extraFormatArguments: new[] { originalNumbers.Count == 1 ? "" : ordinal(i + 1) + " " },
                 preferredWrongAnswers: originalNumbers.Concat(Enumerable.Range(0, int.MaxValue).Select(_ => Rnd.Range(0, 1000).ToString("000"))).Where(str => str != origNum).Distinct().Take(5).ToArray())));
    }

    private static readonly string[] _skyrimFieldNames = new[] { "race", "weapon", "enemy", "city" };
    private static readonly string[] _skyrimFieldNames2 = new[] { "correctRace", "correctWeapon", "correctEnemy", "correctCity" };
    private static readonly string[] _skyrimButtonNames = new[] { "cycleUp", "cycleDown", "accept", "submit", "race", "weapon", "enemy", "city", "shout" };
    private KMSelectable.OnInteractHandler getSkyrimButtonHandler(KMSelectable btn)
    {
        return delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btn.transform);
            btn.AddInteractionPunch(.5f);
            return false;
        };
    }
    private IEnumerable<object> ProcessSkyrim(KMBombModule module)
    {
        var comp = GetComponent(module, "skyrimScript");
        var questions = new[] { Question.SkyrimRace, Question.SkyrimWeapon, Question.SkyrimEnemy, Question.SkyrimCity };
        var flds = _skyrimFieldNames.Select(name => GetField<List<Texture>>(comp, name + "Images", isPublic: true)).ToArray();
        var fldsCorrect = _skyrimFieldNames2.Select(name => GetField<Texture>(comp, name)).ToArray();
        var fldShoutNames = GetField<List<string>>(comp, "shoutNameOptions");
        var fldCorrectShoutName = GetField<string>(comp, "shoutName");
        var fldSolved = GetField<bool>(comp, "solved");
        var fldsButtons = _skyrimButtonNames.Select(fieldName => GetField<KMSelectable>(comp, fieldName, isPublic: true)).ToArray();
        if (comp == null || flds.Any(f => f == null) || fldsCorrect.Any(f => f == null) || fldShoutNames == null || fldCorrectShoutName == null || fldSolved == null || fldsButtons.Any(b => b == null))
            yield break;

        yield return null;
        while (!fldSolved.Get())
            // Usually we’d wait 0.1 seconds at a time, but in this case we need to know immediately so that we can hook the buttons
            yield return null;
        _modulesSolved.IncSafe(_Skyrim);

        var btns = fldsButtons.Select(b => b.Get()).ToArray();
        if (btns.Any(b => b == null))
            yield break;
        foreach (var btn in btns)
            btn.OnInteract = getSkyrimButtonHandler(btn);

        var qs = new List<QandA>();
        for (int i = 0; i < _skyrimFieldNames.Length; i++)
        {
            var list = flds[i].Get();
            if (list.Count != 3)
            {
                Debug.LogFormat("<Souvenir #{0}> Abandoning Skyrim because “{1}” array has unexpected length {2} (expected 3).", _moduleId, _skyrimFieldNames[i], list.Count);
                yield break;
            }
            var correct = fldsCorrect[i].Get();
            if (correct == null)
                yield break;
            qs.Add(makeQuestion(questions[i], _Skyrim, list.Except(new[] { correct }).Select(t => t.name.Replace("'", "’")).ToArray()));
        }
        var shoutNames = fldShoutNames.Get();
        if (shoutNames.Count != 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Skyrim because “shoutNameOptions” array has unexpected length {1} (expected 3).", _moduleId, shoutNames.Count);
            yield break;
        }
        var correctShoutName = fldCorrectShoutName.Get();
        if (correctShoutName == null)
            yield break;
        qs.Add(makeQuestion(Question.SkyrimDragonShout, _Skyrim, shoutNames.Except(new[] { correctShoutName }).Select(n => n.Replace("'", "’")).ToArray()));
        addQuestions(module, qs);
    }

    private sealed class SonicPictureInfo { public string Name; public int Stage; }
    private IEnumerable<object> ProcessSonicTheHedgehog(KMBombModule module)
    {
        var comp = GetComponent(module, "sonicScript");
        var fldsButtonSounds = new[] { "boots", "invincible", "life", "rings" }.Select(name => GetField<string>(comp, name + "Press"));
        var fldsPics = Enumerable.Range(0, 3).Select(i => GetField<Texture>(comp, "pic" + (i + 1))).ToArray();
        var fldStage = GetField<int>(comp, "stage");

        if (comp == null || fldsButtonSounds.Any(b => b == null) || fldsPics.Any(p => p == null) || fldStage == null)
            yield break;

        while (fldStage.Get() < 5)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SonicTheHedgehog);

        var soundNameMapping =
            @"boss=Boss Theme;breathe=Breathe;continueSFX=Continue;drown=Drown;emerald=Emerald;extraLife=Extra Life;finalZone=Final Zone;invincibleSFX=Invincibility;jump=Jump;lamppost=Lamppost;marbleZone=Marble Zone;bumper=Bumper;skid=Skid;spikes=Spikes;spin=Spin;spring=Spring"
                .Split(';').Select(str => str.Split('=')).ToDictionary(ar => ar[0], ar => ar[1]);
        var pictureNameMapping =
            @"annoyedSonic=Annoyed Sonic=2;ballhog=Ballhog=1;blueLamppost=Blue Lamppost=3;burrobot=Burrobot=1;buzzBomber=Buzz Bomber=1;crabMeat=Crab Meat=1;deadSonic=Dead Sonic=2;drownedSonic=Drowned Sonic=2;fallingSonic=Falling Sonic=2;motoBug=Moto Bug=1;redLamppost=Red Lamppost=3;redSpring=Red Spring=3;standingSonic=Standing Sonic=2;switch=Switch=3;yellowSpring=Yellow Spring=3"
                .Split(';').Select(str => str.Split('=')).ToDictionary(ar => ar[0], ar => new SonicPictureInfo { Name = ar[1], Stage = int.Parse(ar[2]) - 1 });

        var pics = fldsPics.Select(f => f.Get()).ToArray();
        if (pics.Any(p => p == null || p.name == null || !pictureNameMapping.ContainsKey(p.name)))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Sonic The Hedgehog because a pic was null or not recognized: [{1}]", _moduleId, pics.Select(p => p == null ? "<null>" : "\"" + p.name + "\"").JoinString(", "));
            yield break;
        }
        var sounds = fldsButtonSounds.Select(f => f.Get()).ToArray();
        if (sounds.Any(s => s == null || !soundNameMapping.ContainsKey(s)))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Sonic The Hedgehog because a sound was null: [{1}]", _moduleId, sounds.Select(s => s == null ? "<null>" : "\"" + s + "\"").JoinString(", "));
            yield break;
        }

        addQuestions(module,
            Enumerable.Range(0, 3).Select(i =>
                makeQuestion(
                    Question.SonicTheHedgehogPictures,
                    _SonicTheHedgehog,
                    new[] { pictureNameMapping[pics[i].name].Name },
                    new[] { ordinal(i + 1) },
                    pictureNameMapping.Values.Where(inf => inf.Stage == i).Select(inf => inf.Name).ToArray()))
            .Concat(new[] { "Running Boots", "Invincibility", "Extra Life", "Rings" }.Select((screenName, i) =>
                makeQuestion(
                    Question.SonicTheHedgehogSounds,
                    _SonicTheHedgehog,
                    new[] { soundNameMapping[sounds[i]] },
                    new[] { screenName },
                    sounds.Select(s => soundNameMapping[s]).ToArray()))));
    }

    private IEnumerable<object> ProcessSynonyms(KMBombModule module)
    {
        var comp = GetComponent(module, "Synonyms");
        var fldNumberText = GetField<TextMesh>(comp, "NumberText", isPublic: true);
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldNumberText == null || fldSolved == null)
            yield break;

        yield return null;
        var numberText = fldNumberText.Get();
        if (numberText == null)
            yield break;
        int number;
        if (numberText.text == null || !int.TryParse(numberText.text, out number) || number < 0 || number > 9)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Synonyms because the display text (“{1}”) is not an integer 0–9.", _moduleId, numberText.text ?? "<null>");
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Synonyms);

        addQuestion(module, Question.SynonymsNumber, new[] { number.ToString() });
    }

    private IEnumerable<object> ProcessBulb(KMBombModule module)
    {
        var comp = GetComponent(module, "TheBulbModule");
        var fldButtonPresses = GetField<string>(comp, "_correctButtonPresses");
        var fldStage = GetField<int>(comp, "_stage");

        if (comp == null || fldButtonPresses == null || fldStage == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        while (fldStage.Get() != 0)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Bulb);

        var buttonPresses = fldButtonPresses.Get();
        if (buttonPresses == null || buttonPresses.Length != 3)
        {
            Debug.LogFormat("<Souvenir #{0}> The Bulb: _correctButtonPresses has unexpected value ({1})", _moduleId, buttonPresses == null ? "<null>" : string.Format(@"""{0}""", buttonPresses));
            yield break;
        }

        addQuestion(module, Question.BulbButtonPresses, new[] { buttonPresses });
    }

    private IEnumerable<object> ProcessGamepad(KMBombModule module)
    {
        var comp = GetComponent(module, "GamepadModule");
        var fldX = GetField<int>(comp, "x");
        var fldY = GetField<int>(comp, "y");
        var fldSolved = GetField<bool>(comp, "solved");
        var fldDisplay = GetField<GameObject>(comp, "Input", isPublic: true);
        var fldDigits1 = GetField<GameObject>(comp, "Digits1", isPublic: true);
        var fldDigits2 = GetField<GameObject>(comp, "Digits2", isPublic: true);

        if (comp == null || fldX == null || fldY == null || fldSolved == null || fldDisplay == null || fldDigits1 == null || fldDigits2 == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.05f);

        var x = fldX.Get();
        var y = fldY.Get();
        if (x < 1 || x > 99 || y < 1 || y > 99)
        {
            Debug.LogFormat("<Souvenir #{0}> The Gamepad: x or y has unexpected value (x={1}, y={2})", _moduleId, x, y);
            yield break;
        }

        var display = fldDisplay.Get();
        var digits1 = fldDigits1.Get();
        var digits2 = fldDigits2.Get();
        if (display == null || display.GetComponent<TextMesh>() == null)
        {
            Debug.LogFormat("<Souvenir #{0}> The Gamepad: display is null or not a TextMesh.", _moduleId);
            yield break;
        }
        if (digits1 == null || digits1.GetComponent<TextMesh>() == null)
        {
            Debug.LogFormat("<Souvenir #{0}> The Gamepad: digits1 is null or not a TextMesh.", _moduleId);
            yield break;
        }
        if (digits2 == null || digits2.GetComponent<TextMesh>() == null)
        {
            Debug.LogFormat("<Souvenir #{0}> The Gamepad: digits2 is null or not a TextMesh.", _moduleId);
            yield break;
        }

        _modulesSolved.IncSafe(_Gamepad);
        addQuestions(module, makeQuestion(Question.GamepadNumbers, _Gamepad, new[] { string.Format("{0:00}:{1:00}", x, y) },
            preferredWrongAnswers: Enumerable.Range(0, int.MaxValue).Select(i => string.Format("{0:00}:{1:00}", Rnd.Range(1, 99), Rnd.Range(1, 99))).Distinct().Take(6).ToArray()));
        digits1.GetComponent<TextMesh>().text = "--";
        digits2.GetComponent<TextMesh>().text = "--";
    }

    private IEnumerable<object> ProcessTapCode(KMBombModule module)
    {
        var comp = GetComponent(module, "TapCodeScript");
        var fldSolved = GetField<bool>(comp, "modulepass");
        var fldChosenWord = GetField<string>(comp, "chosenWord");
        var fldWords = GetField<string[]>(comp, "words");

        if (comp == null || fldSolved == null || fldChosenWord == null || fldWords == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_TapCode);

        var words = fldWords.Get();
        if (words == null)
            yield break;

        var chosenWord = fldChosenWord.Get();
        if (chosenWord == null || !words.Contains(chosenWord))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Tap Code because the initial word ({1}) is not in the word bank.", _moduleId, chosenWord ?? "<null>");
            yield break;
        }

        addQuestion(module, Question.TapCodeReceivedWord, new[] { chosenWord }, preferredWrongAnswers: words);
    }

    private IEnumerable<object> ProcessTenButtonColorCode(KMBombModule module)
    {
        var comp = GetComponent(module, "scr_colorCode");
        var fldSolvedFirstStage = GetField<bool>(comp, "solvedFirst");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldColors = GetField<int[]>(comp, "prevColors");

        if (comp == null || fldSolvedFirstStage == null || fldSolved == null || fldColors == null)
            yield break;

        yield return null;  // Just make sure that Start() has run

        var firstStageColors = fldColors.Get();
        if (firstStageColors == null || firstStageColors.Length != 10)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Ten-Button Color Code because “prevColors” has unexpected value {1}.", _moduleId, firstStageColors == null ? "<null>" : string.Format("[{0}]", firstStageColors.JoinString(", ")));
            yield break;
        }
        // Take a copy because the module modifies the same array in the second stage
        firstStageColors = firstStageColors.ToArray();

        while (!fldSolvedFirstStage.Get())
            yield return new WaitForSeconds(.1f);

        var secondStageColors = fldColors.Get();
        if (secondStageColors == null || secondStageColors.Length != 10)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Ten-Button Color Code because “prevColors” has unexpected value {1}.", _moduleId, secondStageColors == null ? "<null>" : string.Format("[{0}]", secondStageColors.JoinString(", ")));
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_TenButtonColorCode);

        var colorNames = new[] { "red", "green", "blue" };
        addQuestions(module, new[] { firstStageColors, secondStageColors }.SelectMany((colors, stage) => Enumerable.Range(0, 10)
            .Select(slot => makeQuestion(Question.TenButtonColorCodeInitialColors, _TenButtonColorCode, new[] { colorNames[colors[slot]] }, new[] { ordinal(slot + 1), ordinal(stage + 1) }))));
    }

    private IEnumerable<object> ProcessTicTacToe(KMBombModule module)
    {
        var comp = GetComponent(module, "TicTacToeModule");
        var fldKeypadButtons = GetField<KMSelectable[]>(comp, "KeypadButtons", isPublic: true);
        var fldKeypadPhysical = GetField<KMSelectable[]>(comp, "_keypadButtonsPhysical");
        var fldPlacedX = GetField<bool?[]>(comp, "_placedX");
        var fldIsInitialized = GetField<bool>(comp, "_isInitialized");
        var fldIsSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldKeypadButtons == null || fldKeypadPhysical == null || fldPlacedX == null || fldIsInitialized == null || fldIsSolved == null)
            yield break;

        while (!fldIsInitialized.Get())
            yield return new WaitForSeconds(.1f);

        var keypadButtons = fldKeypadButtons.Get();
        var keypadPhysical = fldKeypadPhysical.Get();
        var placedX = fldPlacedX.Get();
        if (keypadButtons == null || keypadPhysical == null || placedX == null)
            yield break;
        if (keypadButtons.Length != 9 || keypadPhysical.Length != 9 || placedX.Length != 9)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Tic-Tac-Toe because one of the arrays has an unexpected length (expected 9): {1}, {2}, {3}.", _moduleId, keypadButtons.Length, keypadPhysical.Length, placedX.Length);
            yield break;
        }

        // Take a copy of the placedX array because it changes
        placedX = placedX.ToArray();

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_TicTacToe);

        var buttonNames = new[] { "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };
        addQuestions(module, Enumerable.Range(0, 9).Select(ix => makeQuestion(Question.TicTacToeInitialState, _TicTacToe,
             possibleCorrectAnswers: new[] { placedX[ix] == null ? (ix + 1).ToString() : placedX[ix].Value ? "X" : "O" },
             extraFormatArguments: new[] { buttonNames[Array.IndexOf(keypadPhysical, keypadButtons[ix])] })));
    }

    private IEnumerable<object> ProcessTimezone(KMBombModule module)
    {
        var comp = GetComponent(module, "TimezoneScript");
        var fldFromCity = GetField<string>(comp, "from");
        var fldToCity = GetField<string>(comp, "to");
        var fldTextFromCity = GetField<TextMesh>(comp, "TextFromCity", isPublic: true);
        var fldTextToCity = GetField<TextMesh>(comp, "TextToCity", isPublic: true);
        var fldInputButton = GetField<KMSelectable>(comp, "InputButton", isPublic: true);

        if (comp == null || fldFromCity == null || fldToCity == null || fldTextFromCity == null || fldTextToCity == null || fldInputButton == null)
            yield break;

        yield return null;

        var inputButton = fldInputButton.Get();
        var textFromCity = fldTextFromCity.Get();
        var textToCity = fldTextToCity.Get();
        if (inputButton == null || textFromCity == null || textToCity == null)
            yield break;

        if (fldFromCity.Get() != textFromCity.text || fldToCity.Get() != textToCity.text)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Timezone because the city names don’t match up: “{1}” vs. “{2}” and “{3}” vs. “{4}”.", _moduleId, fldFromCity.Get(), textFromCity.text, fldToCity.Get(), textToCity.text);
            yield break;
        }

        var prevHandler = inputButton.OnInteract;
        var solved = false;
        inputButton.OnInteract = delegate
        {
            var prevSolved = Bomb.GetSolvedModuleNames().Count();
            var result = prevHandler();
            if (Bomb.GetSolvedModuleNames().Count() > prevSolved)
            {
                textFromCity.text = "WELL";
                textToCity.text = "DONE!";
                solved = true;
            }
            return result;
        };

        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Timezone);
        inputButton.OnInteract = prevHandler;
        addQuestions(module,
            makeQuestion(Question.TimezoneCities, _Timezone, new[] { fldFromCity.Get() }, new[] { "departure" }),
            makeQuestion(Question.TimezoneCities, _Timezone, new[] { fldToCity.Get() }, new[] { "destination" }));
    }

    private IEnumerable<object> ProcessTwoBits(KMBombModule module)
    {
        var comp = GetComponent(module, "TwoBitsModule");
        var fldFirstQueryCode = GetField<int>(comp, "firstQueryCode");
        var fldQueryLookups = GetField<Dictionary<int, string>>(comp, "queryLookups");
        var fldQueryResponses = GetField<Dictionary<string, int>>(comp, "queryResponses");
        var fldCurrentState = GetField<object>(comp, "currentState");

        if (comp == null || fldFirstQueryCode == null || fldQueryLookups == null || fldQueryResponses == null || fldCurrentState == null)
            yield break;

        while (fldCurrentState.Get().ToString() != "Complete")
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_TwoBits);

        var queryLookups = fldQueryLookups.Get();
        var queryResponses = fldQueryResponses.Get();
        if (queryLookups == null || queryResponses == null)
            yield break;

        var qs = new List<QandA>();
        try
        {
            var zerothNumCode = fldFirstQueryCode.Get();
            var zerothLetterCode = queryLookups[zerothNumCode];
            var firstResponse = queryResponses[zerothLetterCode];
            var firstLookup = queryLookups[firstResponse];
            var secondResponse = queryResponses[firstLookup];
            var secondLookup = queryLookups[secondResponse];
            var thirdResponse = queryResponses[secondLookup];
            var preferredWrongAnswers = new[] { zerothNumCode.ToString("00"), firstResponse.ToString("00"), secondResponse.ToString("00"), thirdResponse.ToString("00") };
            qs.Add(makeQuestion(Question.TwoBitsResponse, _TwoBits, new[] { firstResponse.ToString("00") }, new[] { "first" }, preferredWrongAnswers));
            qs.Add(makeQuestion(Question.TwoBitsResponse, _TwoBits, new[] { secondResponse.ToString("00") }, new[] { "second" }, preferredWrongAnswers));
            qs.Add(makeQuestion(Question.TwoBitsResponse, _TwoBits, new[] { thirdResponse.ToString("00") }, new[] { "third" }, preferredWrongAnswers));
        }
        catch (Exception e)
        {
            Debug.LogFormat("<Souvenir #{0}> Two Bits: Exception: {1} ({2})", _moduleId, e.Message, e.GetType().FullName);
        }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessUncoloredSquares(KMBombModule module)
    {
        var comp = GetComponent(module, "UncoloredSquaresModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var fldFirstStageColor1 = GetField<object>(comp, "_firstStageColor1");
        var fldFirstStageColor2 = GetField<object>(comp, "_firstStageColor2");

        if (comp == null || fldSolved == null || fldFirstStageColor1 == null || fldFirstStageColor2 == null)
            yield break;

        yield return null;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_UncoloredSquares);
        addQuestions(module,
            makeQuestion(Question.UncoloredSquaresFirstStage, _UncoloredSquares, new[] { fldFirstStageColor1.Get().ToString() }, new[] { "first" }),
            makeQuestion(Question.UncoloredSquaresFirstStage, _UncoloredSquares, new[] { fldFirstStageColor2.Get().ToString() }, new[] { "second" }));
    }

    private IEnumerable<object> ProcessVisualImpairment(KMBombModule module)
    {
        var comp = GetComponent(module, "VisualImpairment");
        var fldRoundsFinished = GetField<int>(comp, "roundsFinished");
        var fldStageCount = GetField<int>(comp, "stageCount");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldColor = GetField<int>(comp, "color");
        var fldAnyPressed = GetField<bool>(comp, "anyPressed");
        var fldPicture = GetField<string[]>(comp, "picture");

        if (comp == null || fldRoundsFinished == null || fldStageCount == null || fldSolved == null || fldColor == null || fldAnyPressed == null || fldPicture == null)
            yield break;

        // Wait for the first picture to be assigned
        while (fldPicture.Get(nullAllowed: true) == null)
            yield return new WaitForSeconds(.1f);

        var stageCount = fldStageCount.Get();
        if (stageCount < 2 || stageCount >= 4)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Visual Impairment because stageCount is not 2 or 3 as expected (it’s {1}).", _moduleId, stageCount);
            yield break;
        }
        var colorsPerStage = new int[stageCount];
        colorsPerStage[0] = fldColor.Get();
        var curStage = 0;

        while (curStage < stageCount)
        {
            yield return new WaitForSeconds(.1f);

            var newStage = fldRoundsFinished.Get();
            if (newStage != curStage)
            {
                if (newStage != curStage + 1)
                {
                    Debug.LogFormat("<Souvenir #{0}> Abandoning Visual Impairment because roundsFinished changed by an amount other than +1 (was: {1}, now: {2}).", _moduleId, curStage, newStage);
                    yield break;
                }

                if (newStage < stageCount)
                {
                    // Wait for the next picture to be assigned
                    while (fldAnyPressed.Get())
                        yield return new WaitForSeconds(.1f);

                    newStage = fldRoundsFinished.Get();
                    if (newStage != curStage + 1)
                    {
                        Debug.LogFormat("<Souvenir #{0}> Abandoning Visual Impairment because roundsFinished changed while I was waiting for anyPressed to return to false (was: {1}, before wait: {2}, now: {3}).", _moduleId, curStage, curStage + 1, newStage);
                        yield break;
                    }

                    colorsPerStage[newStage] = fldColor.Get();
                }
                curStage = newStage;
            }
        }

        var colorNames = new[] { "Blue", "Green", "Red", "White" };
        _modulesSolved.IncSafe(_VisualImpairment);
        addQuestions(module, colorsPerStage.Select((col, ix) => makeQuestion(Question.VisualImpairmentColors, _VisualImpairment, new[] { colorNames[col] }, new[] { ordinal(ix + 1) })));
    }

    private IEnumerable<object> ProcessWire(KMBombModule module)
    {
        var comp = GetComponent(module, "wireScript");
        var fldSolved = GetField<bool>(comp, "moduleDone");
        var fldDials = GetField<Renderer[]>(comp, "renderers", isPublic: true);
        var fldDisplayedNumber = GetField<int>(comp, "displayedNumber");

        if (comp == null || fldSolved == null || fldDials == null || fldDisplayedNumber == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Wire);

        var dials = fldDials.Get();
        if (dials == null || dials.Length != 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning The Wire because ‘renderers’ has unexpected length ({1}, expected 3).", _moduleId, dials == null ? "<null>" : dials.Length.ToString());
            yield break;
        }

        addQuestions(module,
            makeQuestion(Question.WireDialColors, _Wire, new[] { dials[0].material.mainTexture.name.Replace("Mat", "") }, new[] { "top" }),
            makeQuestion(Question.WireDialColors, _Wire, new[] { dials[1].material.mainTexture.name.Replace("Mat", "") }, new[] { "bottom-left" }),
            makeQuestion(Question.WireDialColors, _Wire, new[] { dials[2].material.mainTexture.name.Replace("Mat", "") }, new[] { "bottom-right" }),
            makeQuestion(Question.WireDisplayedNumber, _Wire, new[] { fldDisplayedNumber.Get().ToString() }));
    }

    private IEnumerable<object> ProcessYahtzee(KMBombModule module)
    {
        var comp = GetComponent(module, "YahtzeeModule");
        var fldDiceValues = GetField<int[]>(comp, "_diceValues");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldDiceValues == null || fldSolved == null)
            yield break;

        // Make sure that Yahtzee’s Start method ran, which assigns _diceValues
        yield return null;

        // This array only changes its contents, it’s never reassigned, so we only need to get it once
        var diceValues = fldDiceValues.Get();

        while (diceValues.Any(v => v == 0))
            yield return new WaitForSeconds(.1f);

        string result;

        // Capture the first roll
        if (Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 5))
        {
            Debug.LogFormat("[Souvenir #{0}] No question for Yahtzee because the first roll was a Yahtzee.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }
        else if (diceValues.Contains(2) && diceValues.Contains(3) && diceValues.Contains(4) && diceValues.Contains(5) && (diceValues.Contains(1) || diceValues.Contains(6)))
            result = "large straight";
        else if (diceValues.Contains(3) && diceValues.Contains(4) && (
            (diceValues.Contains(1) && diceValues.Contains(2)) ||
            (diceValues.Contains(2) && diceValues.Contains(5)) ||
            (diceValues.Contains(5) && diceValues.Contains(6))))
            result = "small straight";
        else if (Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 4))
            result = "four of a kind";
        else if (Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 3) && Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 2))
            result = "full house";
        else if (Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 3))
            result = "three of a kind";
        else if (Enumerable.Range(1, 6).Count(i => diceValues.Count(val => val == i) == 2) == 2)
            result = "two pairs";
        else if (Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 2))
            result = "pair";
        else
        {
            Debug.LogFormat("[Souvenir #{0}] No question for Yahtzee because the first roll was nothing.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Yahtzee);
        addQuestion(module, Question.YahtzeeInitialRoll, new[] { result });
    }

    private void addQuestion(KMBombModule module, Question question, string[] possibleCorrectAnswers, string[] extraFormatArguments = null, string[] preferredWrongAnswers = null)
    {
        addQuestions(module, makeQuestion(question, module.ModuleType, possibleCorrectAnswers, extraFormatArguments, preferredWrongAnswers));
    }

    private void addQuestions(KMBombModule module, IEnumerable<QandA> questions)
    {
        var qs = questions.Where(q => q != null).ToArray();
        if (qs.Length == 0)
        {
            Debug.LogFormat("<Souvenir #{0}> Empty question batch provided for {1}.", _moduleId, module.ModuleDisplayName);
            return;
        }
        Debug.LogFormat("<Souvenir #{0}> Adding question batch:\n{1}", _moduleId, qs.Select(q => "    • " + q.DebugString).JoinString("\n"));
        _questions.Add(new QuestionBatch
        {
            NumSolved = Bomb.GetSolvedModuleNames().Count,
            Questions = qs,
            Module = module
        });
    }

    private void addQuestions(KMBombModule module, params QandA[] questions)
    {
        addQuestions(module, (IEnumerable<QandA>) questions);
    }

    private string titleCase(string str)
    {
        return str.Length < 1 ? str : char.ToUpperInvariant(str[0]) + str.Substring(1).ToLowerInvariant();
    }

    private Dictionary<Question, SouvenirQuestionAttribute> _attributes;

    private QandA makeQuestion(Question question, string moduleKey, string[] possibleCorrectAnswers, string[] extraFormatArguments = null, string[] preferredWrongAnswers = null)
    {
        SouvenirQuestionAttribute attr;
        if (!_attributes.TryGetValue(question, out attr))
        {
            Debug.LogFormat("<Souvenir #{1}> Question {0} has no attribute.", question, _moduleId);
            return null;
        }

        var allAnswers = attr.AllAnswers;
        if (allAnswers != null)
        {
            var inconsistency = possibleCorrectAnswers.Except(allAnswers).FirstOrDefault();
            if (inconsistency != null)
            {
                Debug.LogFormat("<Souvenir #{2}> Question {0}: invalid answer: {1}.", question, inconsistency ?? "<null>", _moduleId);
                return null;
            }
            if (preferredWrongAnswers != null)
            {
                var inconsistency2 = preferredWrongAnswers.Except(allAnswers).FirstOrDefault();
                if (inconsistency2 != null)
                {
                    Debug.LogFormat("<Souvenir #{2}> Question {0}: invalid preferred wrong answer: {1}.", question, inconsistency2 ?? "<null>", _moduleId);
                    return null;
                }
            }
        }

        List<string> answers;
        if (allAnswers == null && preferredWrongAnswers == null)
        {
            Debug.LogFormat("<Souvenir #{0}> Question {1}: allAnswers and preferredWrongAnswers are both null.", _moduleId, question);
            return null;
        }
        else if (allAnswers == null)
            answers = preferredWrongAnswers.Except(possibleCorrectAnswers).ToList().Shuffle().Take(attr.NumAnswers - 1).ToList();
        else
        {
            // Pick 𝑛−1 random wrong answers.
            answers = allAnswers.Except(possibleCorrectAnswers).ToList().Shuffle().Take(attr.NumAnswers - 1).ToList();
            // Add the preferred wrong answers, if any. If we had added them earlier, they’d come up too rarely.
            if (preferredWrongAnswers != null)
                answers = answers.Concat(preferredWrongAnswers.Except(answers).Except(possibleCorrectAnswers)).ToList().Shuffle().Take(attr.NumAnswers - 1).ToList();
        }

        var correctIndex = Rnd.Range(0, Math.Min(attr.NumAnswers, answers.Count + 1));
        answers.Insert(correctIndex, possibleCorrectAnswers[Rnd.Range(0, possibleCorrectAnswers.Length)]);

        var numSolved = _modulesSolved.Get(moduleKey);
        if (numSolved < 1)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning {1} ({2}) because you forgot to increment the solve count.", _moduleId, attr.ModuleName, moduleKey);
            return null;
        }
        var formatArguments = new List<string> { _moduleCounts.Get(moduleKey) > 1 ? string.Format("the {0} you solved {1}", attr.ModuleName, ordinal(numSolved)) : attr.AddThe ? "The\u00a0" + attr.ModuleName : attr.ModuleName };
        if (extraFormatArguments != null)
            formatArguments.AddRange(extraFormatArguments);

        return new QandA(string.Format(attr.QuestionText, formatArguments.ToArray()), answers.ToArray(), correctIndex, font(attr.Font), fontTexture(attr.Font));
    }

    private string[] GetAnswers(Question question)
    {
        SouvenirQuestionAttribute attr;
        if (!_attributes.TryGetValue(question, out attr))
        {
            Debug.LogFormat("<Souvenir #{0}> Question {1} is missing from the _attributes dictionary.", _moduleId, question);
            return null;
        }
        return attr.AllAnswers;
    }

    private string ordinal(int number)
    {
        if (number < 0)
            return "(" + number + ")th";

        switch (number)
        {
            case 1: return "first";
            case 2: return "second";
            case 3: return "third";
        }

        switch ((number / 10) % 10 == 1 ? 0 : number % 10)
        {
            case 1: return number + "st";
            case 2: return number + "nd";
            case 3: return number + "rd";
            default: return number + "th";
        }
    }

#pragma warning disable 414
#pragma warning disable IDE0044
    private bool TwitchPlaysActive = false;
    private List<KMBombModule> TwitchAbandonModule = new List<KMBombModule>();
    private readonly string TwitchHelpMessage = @"Submit the correct response with “!{0} answer 3”. Order is from top to bottom, then left to right.";
#pragma warning restore 414
#pragma warning restore IDE0044

    KMSelectable[] ProcessTwitchCommand(string command)
    {
        if (command == "tp" && !TwitchPlaysActive)
        {
            ActivateTwitchPlaysNumbers();
            TwitchPlaysActive = true;
        }
        var m = Regex.Match(command.ToLowerInvariant(), @"\A\s*answer\s+(\d)\s*\z");
        if (!m.Success)
            return null;

        var number = int.Parse(m.Groups[1].Value);
        var btns = Answers4Parent.activeSelf ? Answers4 : Answers6;
        if (number <= 0 || number > btns.Length)
            return null;
        var btn = btns[number - 1];
        if (btn == null || !btn.gameObject.activeSelf)
            return null;
        return new[] { btn };
    }
}

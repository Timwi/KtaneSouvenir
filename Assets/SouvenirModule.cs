using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using KModkit;
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
    public KMBossModule BossModule;
    public KMSelectable[] Answers;
    public GameObject AnswersParent;
    public GameObject[] TpNumbers;
    public Sprite[] ArithmelogicSprites;
    public Sprite[] ExampleSprites;
    public Sprite[] MahjongSprites;
    public Sprite[] PerspectivePegsSprites;
    public Sprite[] PlanetsSprites;
    public Sprite[] SymbolicCoordinatesSprites;
    public Sprite[] WavetappingSprites;
    public Sprite[] FlagsSprites;
    public Sprite[] KudosudokuSprites;

    public TextMesh TextMesh;
    public Renderer TextRenderer;
    public Renderer SurfaceRenderer;
    public Material FontMaterial;
    public Font[] Fonts;
    public Texture[] FontTextures;
    public Mesh HighlightShort;
    public Mesh HighlightLong;

    public static readonly string[] _defaultIgnoredModules = {
        "Souvenir",
        "Forget Everything",
        "Forget Me Not",
        "Forget This",
        "Turn The Key",
        "The Time Keeper",
        "Simon's Stages",
        "Purgatory"
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

    [NonSerialized]
    public double SurfaceSizeFactor;

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
    const string _Arithmelogic = "arithmelogic";
    const string _BigCircle = "BigCircle";
    const string _BinaryLEDs = "BinaryLeds";
    const string _Bitmaps = "BitmapsModule";
    const string _BlindMaze = "BlindMaze";
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
    const string _ColoredSwitches = "ColoredSwitchesModule";
    const string _ColorMorse = "ColorMorseModule";
    const string _Coordinates = "CoordinatesModule";
    const string _Crackbox = "CrackboxModule";
    const string _Creation = "CreationModule";
    const string _DoubleOh = "DoubleOhModule";
    const string _DrDoctor = "DrDoctorModule";
    const string _FastMath = "fastMath";
    const string _Flags = "FlagsModule";
    const string _Functions = "qFunctions";
    const string _Gamepad = "TheGamepadModule";
    const string _GridLock = "GridlockModule";
    const string _Gryphons = "gryphons";
    const string _LogicalButtons = "logicalButtonsModule";
    const string _Hexamaze = "HexamazeModule";
    const string _Hogwarts = "HogwartsModule";
    const string _HumanResources = "HumanResourcesModule";
    const string _Hunting = "hunting";
    const string _IceCream = "iceCreamModule";
    const string _IdentityParade = "identityParade";
    const string _Kudosudoku = "KudosudokuModule";
    const string _LEDEncryption = "LEDEnc";
    const string _LEDMath = "lgndLEDMath";
    const string _Listening = "Listening";
    const string _LogicGates = "logicGates";
    const string _LondonUnderground = "londonUnderground";
    const string _Mafia = "MafiaModule";
    const string _Mahjong = "MahjongModule";
    const string _MaritimeFlags = "MaritimeFlagsModule";
    const string _Microcontroller = "Microcontroller";
    const string _Minesweeper = "MinesweeperModule";
    const string _ModuleMaze = "ModuleMaze";
    const string _MonsplodeFight = "monsplodeFight";
    const string _MonsplodeTradingCards = "monsplodeCards";
    const string _Moon = "moon";
    const string _MorseAMaze = "MorseAMaze";
    const string _Morsematics = "MorseV2";
    const string _MorseWar = "MorseWar";
    const string _MouseInTheMaze = "MouseInTheMaze";
    const string _Murder = "murder";
    const string _MysticSquare = "MysticSquareModule";
    const string _Neutralization = "neutralization";
    const string _Nonogram = "NonogramModule";
    const string _OnlyConnect = "OnlyConnectModule";
    const string _OrientationCube = "OrientationCube";
    const string _PatternCube = "PatternCubeModule";
    const string _PerspectivePegs = "spwizPerspectivePegs";
    const string _Planets = "planets";
    const string _Poetry = "poetry";
    const string _PolyhedralMaze = "PolyhedralMazeModule";
    const string _Probing = "Probing";
    const string _Quintuples = "quintuples";
    const string _Rhythms = "MusicRhythms";
    const string _SchlagDenBomb = "qSchlagDenBomb";
    const string _SeaShells = "SeaShells";
    const string _ShapesBombs = "ShapesBombs";
    const string _ShapeShift = "shapeshift";
    const string _SillySlots = "SillySlots";
    const string _SimonSamples = "simonSamples";
    const string _SimonScreams = "SimonScreamsModule";
    const string _SimonSends = "SimonSendsModule";
    const string _SimonShrieks = "SimonShrieksModule";
    const string _SimonSings = "SimonSingsModule";
    const string _SimonSpeaks = "SimonSpeaksModule";
    const string _SimonsStar = "simonsStar";
    const string _SimonStates = "SimonV2";
    const string _SimonStops = "simonStops";
    const string _SkewedSlots = "SkewedSlotsModule";
    const string _Skyrim = "skyrim";
    const string _Snooker = "snooker";
    const string _SonicTheHedgehog = "sonic";
    const string _Souvenir = "SouvenirModule";
    const string _Switch = "BigSwitch";
    const string _Switches = "switchModule";
    const string _SymbolCycle = "SymbolCycleModule";
    const string _SymbolicCoordinates = "symbolicCoordinates";
    const string _Synonyms = "synonyms";
    const string _TapCode = "tapCode";
    const string _TenButtonColorCode = "TenButtonColorCode";
    const string _TextField = "TextField";
    const string _ThirdBase = "ThirdBase";
    const string _TicTacToe = "TicTacToeModule";
    const string _Timezone = "timezone";
    const string _TurtleRobot = "turtleRobot";
    const string _TwoBits = "TwoBits";
    const string _UncoloredSquares = "UncoloredSquaresModule";
    const string _VisualImpairment = "visual_impairment";
    const string _Wavetapping = "Wavetapping";
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
            { _Arithmelogic, ProcessArithmelogic },
            { _BigCircle, ProcessBigCircle },
            { _BinaryLEDs, ProcessBinaryLEDs },
            { _Bitmaps, ProcessBitmaps },
            { _BlindMaze, ProcessBlindMaze },
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
            { _ColoredSwitches, ProcessColoredSwitches },
            { _ColorMorse, ProcessColorMorse },
            { _Coordinates, ProcessCoordinates },
            { _Crackbox, ProcessCrackbox },
            { _Creation, ProcessCreation },
            { _DoubleOh, ProcessDoubleOh },
            { _DrDoctor, ProcessDrDoctor },
            { _FastMath, ProcessFastMath },
            { _Flags, ProcessFlags },
            { _Functions, ProcessFunctions },
            { _Gamepad, ProcessGamepad },
            { _GridLock, ProcessGridLock },
            { _Gryphons, ProcessGryphons },
            { _Hexamaze, ProcessHexamaze },
            { _Hogwarts, ProcessHogwarts },
            { _HumanResources, ProcessHumanResources },
            { _Hunting, ProcessHunting },
            { _IceCream, ProcessIceCream },
            { _IdentityParade, ProcessIdentityParade },
            { _Kudosudoku, ProcessKudosudoku },
            { _LEDEncryption, ProcessLEDEncryption },
            { _LEDMath, ProcessLEDMath },
            { _Listening, ProcessListening },
            { _LogicalButtons, ProcessLogicalButtons },
            { _LogicGates, ProcessLogicGates },
            { _LondonUnderground, ProcessLondonUnderground },
            { _Mafia, ProcessMafia },
            { _Mahjong, ProcessMahjong },
            { _MaritimeFlags, ProcessMaritimeFlags },
            { _Microcontroller, ProcessMicrocontroller },
            { _Minesweeper, ProcessMinesweeper },
            { _ModuleMaze, ProcessModuleMaze },
            { _MonsplodeFight, ProcessMonsplodeFight },
            { _MonsplodeTradingCards, ProcessMonsplodeTradingCards },
            { _Moon, ProcessMoon },
            { _MorseAMaze, ProcessMorseAMaze },
            { _Morsematics, ProcessMorsematics },
            { _MorseWar, ProcessMorseWar },
            { _MouseInTheMaze, ProcessMouseInTheMaze },
            { _Murder, ProcessMurder },
            { _MysticSquare, ProcessMysticSquare },
            { _Neutralization, ProcessNeutralization },
            { _Nonogram, ProcessNonogram },
            { _OnlyConnect, ProcessOnlyConnect },
            { _OrientationCube, ProcessOrientationCube },
            { _PatternCube, ProcessPatternCube },
            { _PerspectivePegs, ProcessPerspectivePegs },
            { _Planets, ProcessPlanets },
            { _Poetry, ProcessPoetry },
            { _PolyhedralMaze, ProcessPolyhedralMaze },
            { _Probing, ProcessProbing },
            { _Quintuples, ProcessQuintuples },
            { _Rhythms, ProcessRhythms },
            { _SchlagDenBomb, ProcessSchlagDenBomb },
            { _SeaShells, ProcessSeaShells },
            { _ShapesBombs, ProcessShapesAndBombs },
            { _ShapeShift, ProcessShapeShift },
            { _SillySlots, ProcessSillySlots },
            { _SimonSamples, ProcessSimonSamples },
            { _SimonScreams, ProcessSimonScreams },
            { _SimonSends, ProcessSimonSends },
            { _SimonShrieks, ProcessSimonShrieks },
            { _SimonSings, ProcessSimonSings },
            { _SimonSpeaks, ProcessSimonSpeaks },
            { _SimonsStar, ProcessSimonsStar },
            { _SimonStates, ProcessSimonStates },
            { _SimonStops, ProcessSimonStops },
            { _SkewedSlots, ProcessSkewedSlots },
            { _Skyrim, ProcessSkyrim },
            { _Snooker, ProcessSnooker },
            { _SonicTheHedgehog, ProcessSonicTheHedgehog },
            { _Souvenir, ProcessSouvenir },
            { _Switch, ProcessSwitch },
            { _Switches, ProcessSwitches },
            { _SymbolCycle, ProcessSymbolCycle },
            { _SymbolicCoordinates, ProcessSymbolicCoordinates },
            { _Synonyms, ProcessSynonyms },
            { _TapCode, ProcessTapCode },
            { _TenButtonColorCode, ProcessTenButtonColorCode },
            { _TextField, ProcessTextField },
            { _ThirdBase, ProcessThirdBase },
            { _TicTacToe, ProcessTicTacToe },
            { _Timezone, ProcessTimezone },
            { _TurtleRobot, ProcessTurtleRobot },
            { _TwoBits, ProcessTwoBits },
            { _UncoloredSquares, ProcessUncoloredSquares },
            { _VisualImpairment, ProcessVisualImpairment },
            { _Wavetapping, ProcessWavetapping },
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
        SurfaceSizeFactor = SurfaceRenderer.bounds.size.x / (2 * .834) * .9;
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
            "We defuse bombs.",  // “We rob banks.“ (Bonnie and Clyde)
            "Somebody set up us the bomb.",  // direct quote (Zero Wing)
            "Luke, I am your expert." // “Luke, I am your father.“ (Star Wars V: The Empire Strikes Back) (misquote)
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
                        switch (attr.Type)
                        {
                            case AnswerType.Sprites:
                                SetQuestion(new QandASprite(
                                    module: attr.ModuleNameWithThe,
                                    question: string.Format(attr.QuestionText, fmt),
                                    correct: 0,
                                    answers: ExampleSprites));
                                break;

                            default:
                                SetQuestion(new QandAText(
                                    module: attr.ModuleNameWithThe,
                                    question: string.Format(attr.QuestionText, fmt),
                                    correct: 0,
                                    answers: (attr.AllAnswers ?? attr.ExampleAnswers).ToList().Shuffle().Take(attr.NumAnswers).ToArray(),
                                    font: Fonts[(int) attr.Type],
                                    fontTexture: FontTextures[(int) attr.Type],
                                    fontMaterial: FontMaterial));
                                break;
                        }
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

                if (TwitchPlaysActive)
                    ActivateTwitchPlaysNumbers();
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
    }

    void setAnswerHandler(int index, Action<int> handler)
    {
        Answers[index].OnInteract = delegate
        {
            Answers[index].AddInteractionPunch();
            handler(index);
            return false;
        };
    }

    private void disappear()
    {
        TextMesh.gameObject.SetActive(false);
        AnswersParent.SetActive(false);
    }

    private void HandleAnswer(int index)
    {
        if (_animating || _isSolved)
            return;

        if (_currentQuestion == null || index >= _currentQuestion.NumAnswers)
            return;

        Debug.LogFormat("[Souvenir #{0}] Clicked answer #{1} ({2}). {3}.", _moduleId, index + 1, _currentQuestion.DebugAnswers.Skip(index).First(), _currentQuestion.CorrectIndex == index ? "Correct" : "Wrong");

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
        var on = false;
        for (int i = 0; i < 14; i++)
        {
            _currentQuestion.BlinkCorrectAnswer(on, this);
            on = !on;
            yield return new WaitForSeconds(.1f);
        }

        dismissQuestion();
        _animating = false;
    }

    private IEnumerator Play()
    {
        if (TwitchPlaysActive)
            ActivateTwitchPlaysNumbers();

        var ignoredModules = BossModule.GetIgnoredModules(Module, _defaultIgnoredModules);
        Debug.LogFormat(@"<Souvenir #{0}> Ignored modules: {1}", _moduleId, ignoredModules.JoinString(", "));
        var numPlayableModules = Bomb.GetSolvableModuleNames().Count(x => !ignoredModules.Contains(x));

        while (true)
        {
            var numSolved = Bomb.GetSolvedModuleNames().Count(x => !ignoredModules.Contains(x));
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
        AnswersParent.transform.localPosition = new Vector3(.005f, 0, 0);
        foreach (var gobj in TpNumbers)
            gobj.SetActive(true);
    }

    private void SetQuestion(QandA q)
    {
        Debug.LogFormat("[Souvenir #{0}] Asking question: {1}", _moduleId, q.DebugString);
        _currentQuestion = q;
        SetWordWrappedText(q.QuestionText, q.DesiredHeightFactor);
        q.SetAnswers(this);
        AnswersParent.SetActive(true);
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

    private void SetWordWrappedText(string text, double desiredHeightFactor)
    {
        var low = 1;
        var high = 256;
        var desiredHeight = desiredHeightFactor * SurfaceSizeFactor;
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
                    var y = line * heightOfALine / SurfaceSizeFactor;
                    if (line < wrapWidths.Count)
                        return wrapWidths[line];
                    while (wrapWidths.Count < line)
                        wrapWidths.Add(0);
                    var i = 1;
                    while (i < _acceptableWidths.Length && _acceptableWidths[i][0] < y)
                        i++;
                    if (i == _acceptableWidths.Length)
                        wrapWidths.Add(_acceptableWidths[i - 1][1] * SurfaceSizeFactor);
                    else
                    {
                        var lambda = (y - _acceptableWidths[i - 1][0]) / (_acceptableWidths[i][0] - _acceptableWidths[i - 1][0]);
                        wrapWidths.Add((_acceptableWidths[i - 1][1] * (1 - lambda) + _acceptableWidths[i][1] * lambda) * SurfaceSizeFactor);
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
        private readonly object _target;
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

    sealed class PropertyInfo<T>
    {
        private readonly object _target;
        private readonly int _souvenirID;
        public readonly PropertyInfo Property;
        public bool Error { get; private set; }

        public PropertyInfo(object target, PropertyInfo property, int souvenirID)
        {
            _target = target;
            Property = property;
            _souvenirID = souvenirID;
            Error = false;
        }

        public T Get(bool nullAllowed = false)
        {
            // “This value should be null for non-indexed properties.” (MSDN)
            return Get(null, nullAllowed);
        }

        public T Get(object[] index, bool nullAllowed = false)
        {
            try
            {
                var t = (T) Property.GetValue(_target, index);
                if (!nullAllowed && t == null)
                    Debug.LogFormat("<Souvenir #{2}> Property {1}.{0} is null.", Property.Name, Property.DeclaringType.FullName, _souvenirID);
                Error = false;
                return t;
            }
            catch (Exception e)
            {
                Debug.LogFormat("<Souvenir #{2}> Property {1}.{0} could not be fetched with the specified parameters. Exception: {3}\n{4}", Property.Name, Property.DeclaringType.FullName, _souvenirID, e.GetType().FullName, e.StackTrace);
                Error = true;
                return default(T);
            }
        }

        public T GetFrom(object obj, bool nullAllowed = false)
        {
            return GetFrom(obj, null, nullAllowed);
        }

        public T GetFrom(object obj, object[] index, bool nullAllowed = false)
        {
            var t = (T) Property.GetValue(obj, index);
            if (!nullAllowed && t == null)
                Debug.LogFormat("<Souvenir #{2}> Property {1}.{0} is null.", Property.Name, Property.DeclaringType.FullName, _souvenirID);
            return t;
        }

        public void Set(T value, object[] index = null)
        {
            try
            {
                Property.SetValue(_target, value, index);
                Error = false;
            }
            catch (Exception e)
            {
                Debug.LogFormat("<Souvenir #{2}> Property {1}.{0} could not be set with the specified parameters. Exception: {3}\n{4}", Property.Name, Property.DeclaringType.FullName, _souvenirID, e.GetType().FullName, e.StackTrace);
                Error = true;
            }
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

    private PropertyInfo<T> GetProperty<T>(object target, string name, bool isPublic = false)
    {
        if (target == null)
        {
            Debug.LogFormat("<Souvenir #{3}> Attempt to get {1} property {0} of type {2} from a null object.", name, isPublic ? "public" : "non-public", typeof(T).FullName, _moduleId);
            return null;
        }
        return GetPropertyImpl<T>(target, target.GetType(), name, isPublic, BindingFlags.Instance);
    }

    private PropertyInfo<T> GetStaticProperty<T>(Type targetType, string name, bool isPublic = false)
    {
        if (targetType == null)
        {
            Debug.LogFormat("<Souvenir #{0}> Attempt to get {1} static property {2} of type {3} from a null type.", _moduleId, isPublic ? "public" : "non-public", name, typeof(T).FullName);
            return null;
        }
        return GetPropertyImpl<T>(null, targetType, name, isPublic, BindingFlags.Static);
    }

    private PropertyInfo<T> GetPropertyImpl<T>(object target, Type targetType, string name, bool isPublic, BindingFlags bindingFlags)
    {
        var fld = targetType.GetProperty(name, (isPublic ? BindingFlags.Public : BindingFlags.NonPublic) | bindingFlags);
        if (fld == null)
        {
            Debug.LogFormat("<Souvenir #{3}> Type {0} does not contain {1} property {2}. Properties are: {4}", targetType, isPublic ? "public" : "non-public", name, _moduleId,
                targetType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Select(f => string.Format("{0} {1} {2}", f.GetGetMethod().IsPublic ? "public" : "private", f.PropertyType.FullName, f.Name)).JoinString(", "));
            return null;
        }
        if (!typeof(T).IsAssignableFrom(fld.PropertyType))
        {
            Debug.LogFormat("<Souvenir #{5}> Type {0} has {1} field {2} of type {3} but expected type {4}.", targetType, isPublic ? "public" : "non-public", name, fld.PropertyType.FullName, typeof(T).FullName, _moduleId);
            return null;
        }
        return new PropertyInfo<T>(target, fld, _moduleId);
    }

    private IEnumerator ProcessModule(KMBombModule module)
    {
        _coroutinesActive++;
        var moduleType = module.ModuleType;
        _moduleCounts.IncSafe(moduleType);
        var iterator = _moduleProcessors.Get(moduleType, null);

        if (iterator != null)
        {
            Debug.LogFormat("<Souvenir #{1}> Module {0}: Start processing.", moduleType, _moduleId);
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
                Debug.LogFormat("[Souvenir #{0}] There was no question generated for {1}. Please report this to Timwi as this may indicate a bug in Souvenir. Remember to send him this logfile.", _moduleId, module.ModuleDisplayName);
            Debug.LogFormat("<Souvenir #{1}> Module {0}: Finished processing.", moduleType, _moduleId);
        }
        else
        {
            Debug.LogFormat("<Souvenir #{1}> Module {0}: Not supported.", moduleType, _moduleId);
            if (_isTimwisComputer)
            {
                var s = new StringBuilder();
                s.AppendLine("Unrecognized module: " + module.name + ", KMBombModule.ModuleType: " + moduleType);
                foreach (var comp in module.GetComponents(typeof(UnityEngine.Object)))
                    s.AppendLine("    - " + (comp == null ? "<null>" : comp.GetType().FullName));
                lock (_timwiPath)
                    File.AppendAllText(_timwiPath, s.ToString());
            }
        }

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
            makeQuestion(Question._3DMazeMarkings, _3DMaze, correctAnswers: new[] { correctMarkings }),
            makeQuestion(Question._3DMazeBearing, _3DMaze, correctAnswers: new[] { bearing == 'N' ? "North" : bearing == 'S' ? "South" : bearing == 'W' ? "West" : "East" }));
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
        addQuestions(module, targetNodeNames.Select((tn, ix) => makeQuestion(Question._3DTunnelsTargetNode, _3DTunnels, new[] { ordinal(ix + 1) }, new[] { tn }, targetNodeNames)));
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
                qs.Add(() => makeQuestion(Question.AdventureGameCorrectItem, _AdventureGame, new[] { ordinal(itemIndex) }, new[] { titleCase(mthItemName.Invoke(itemUsed)) }));
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
        addQuestions(module, qs.Select(q => q()).Concat(new[] { makeQuestion(Question.AdventureGameEnemy, _AdventureGame, correctAnswers: new[] { enemyName }) }));
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

        addQuestions(module, textures.Take(2).Select((t, ix) => makeQuestion(ix == 0 ? Question.AlgebraEquation1 : Question.AlgebraEquation2, _Algebra, correctAnswers: new[] { t.name.Replace(';', '/') })));
    }

    private IEnumerable<object> ProcessArithmelogic(KMBombModule module)
    {
        var comp = GetComponent(module, "Arithmelogic");
        var fldSymbolNum = GetField<int>(comp, "submitSymbol");
        var fldSelectableValues = GetField<int[][]>(comp, "selectableValues");
        var fldCurrentDisplays = GetField<int[]>(comp, "currentDisplays");
        var fldSolved = GetField<bool>(comp, "isSolved");

        if (comp == null || fldSymbolNum == null || fldSelectableValues == null || fldCurrentDisplays == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Arithmelogic);

        var symbolNum = fldSymbolNum.Get();
        if (symbolNum > 21 || symbolNum < 0)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Arithmelogic because the submit button’s symbol’s ID is {1}, when it should be from 0 to 21.", _moduleId, symbolNum);
            yield break;
        }

        var selVal = fldSelectableValues.Get();
        if (selVal == null || selVal.Length != 3 || selVal.Any(arr => arr == null || arr.Length != 4))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Arithmelogic because the ‘selectableValues’ arrays have unexpected length: {1} (expected 4×3).", _moduleId, selVal == null ? "<null>" : "[" + selVal.Select(arr => arr == null ? "<null>" : arr.Length.ToString()).JoinString(", ") + "]");
            yield break;
        }

        var curDisp = fldCurrentDisplays.Get();
        if (curDisp == null || curDisp.Length != 3 || curDisp.Any(val => val < 0 || val >= 4))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Arithmelogic because ‘currentDisplays’ has unexpected length or values: [{1}] (expected 3 values 0–3).", _moduleId, curDisp == null ? "<null>" : curDisp.JoinString(", "));
            yield break;
        }

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.ArithmelogicSubmit, _Arithmelogic, correctAnswers: new[] { ArithmelogicSprites[symbolNum] }, preferredWrongAnswers: ArithmelogicSprites));
        var screens = new[] { "left", "middle", "right" };
        for (int i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.ArithmelogicNumbers, _Arithmelogic, formatArgs: new[] { screens[i] },
                correctAnswers: Enumerable.Range(0, 4).Where(ix => ix != curDisp[i]).Select(ix => selVal[i][ix].ToString()).ToArray(),
                preferredWrongAnswers: Enumerable.Range(10, 99).Select(j => j.ToString()).ToArray()));
        addQuestions(module, qs);
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
            formatArgs: new[] { ordinal(ix + 1) },
            correctAnswers: new[] { color.ToString() })));
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
            addQuestion(module, Question.BinaryLEDsValue, correctAnswers: new[] { answer.ToString() }, preferredWrongAnswers: Enumerable.Range(0, 32).Select(i => i.ToString()).ToArray());
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
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { "white", "top left" }, new[] { qCounts[0].ToString() }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { "white", "top right" }, new[] { qCounts[1].ToString() }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { "white", "bottom left" }, new[] { qCounts[2].ToString() }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { "white", "bottom right" }, new[] { qCounts[3].ToString() }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { "black", "top left" }, new[] { (16 - qCounts[0]).ToString() }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { "black", "top right" }, new[] { (16 - qCounts[1]).ToString() }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { "black", "bottom left" }, new[] { (16 - qCounts[2]).ToString() }, preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, new[] { "black", "bottom right" }, new[] { (16 - qCounts[3]).ToString() }, preferredWrongAnswers));
    }

    private IEnumerable<object> ProcessBlindMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "BlindMaze");
        // Despite the name “currentMaze”, the field actually contains the number of solved modules when Blind Maze was solved
        var fldNumSolved = GetField<int>(comp, "currentMaze");
        var fldButtonColors = GetField<int[]>(comp, "buttonColors");
        var fldSolved = GetField<bool>(comp, "Solved");

        if (comp == null || fldNumSolved == null || fldButtonColors == null || fldSolved == null)
            yield break;

        yield return null;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BlindMaze);

        var buttonColors = fldButtonColors.Get();
        if (buttonColors == null)
            yield break;
        if (buttonColors.Length != 4 || buttonColors.Any(bc => bc < 0 || bc > 4))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Blind Maze because ‘buttonColors’ has unexpected length or unexpected value (expected 4 values 0–4): [{1}].", _moduleId, buttonColors.JoinString(", "));
            yield break;
        }
        var numSolved = fldNumSolved.Get();
        if (numSolved < 0)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Blind Maze because ‘currentMaze’ is negative ({1}).", _moduleId, numSolved);
            yield break;
        }

        var colorNames = new[] { "Red", "Green", "Blue", "Gray", "Yellow" };
        var buttonNames = new[] { "north", "east", "south", "west" };

        addQuestions(module,
            buttonColors.Select((col, ix) => makeQuestion(Question.BlindMazeColors, _BlindMaze, formatArgs: new[] { buttonNames[ix] }, correctAnswers: new[] { colorNames[col] }))
                .Concat(new[] { makeQuestion(Question.BlindMazeMaze, _BlindMaze, correctAnswers: new[] { ((numSolved + Bomb.GetSerialNumberNumbers().Last()) % 10).ToString() }) }));
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
        addQuestion(module, Question.BrailleWord, correctAnswers: new[] { fldWord.Get() });
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
        addQuestions(module, pressed.Select((p, i) => p.Length == 0 ? null : makeQuestion(Question.BrokenButtons, _BrokenButtons, new[] { ordinal(i + 1) }, new[] { p }, pressed.Except(new[] { "" }).ToArray())));
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
        addQuestions(module, moduleNumber.Select((mn, ix) => makeQuestion(Question.BurglarAlarmDigits, _BurglarAlarm, new[] { ordinal(ix + 1) }, new[] { mn.ToString() }, moduleNumber.Select(n => n.ToString()).ToArray())));
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

        addQuestions(module, colorOccurrences.Select(kvp =>
            makeQuestion(Question.ButtonSequencesColorOccurrences, _ButtonSequences,
                formatArgs: new[] { colorNames[kvp.Key].ToLowerInvariant() },
                correctAnswers: new[] { kvp.Value.ToString() },
                preferredWrongAnswers: colorOccurrences.Values.Select(v => v.ToString()).ToArray())));
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
        addQuestion(module, Question.CalendarLedColor, correctAnswers: new[] { colorblindText.text });
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

        addQuestions(module, paids.Select((p, i) => makeQuestion(Question.CheapCheckoutPaid, _CheapCheckout,
            formatArgs: new[] { paids.Count == 1 ? "" : ordinal(i + 1) + " " },
            correctAnswers: new[] { "$" + p.ToString("N2") },
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
            Debug.LogFormat("<Souvenir #{0}> Abandoning Chess because indexSelected array length is unexpected or one of the values is weird ({1}).", _moduleId, indexSelected.Select(iSel => iSel.ToString()).JoinString(", "));
            yield break;
        }

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Chess);

        addQuestions(module, Enumerable.Range(0, 6).Select(i => makeQuestion(Question.ChessCoordinate, _Chess, new[] { ordinal(i + 1) }, new[] { "" + ((char) (indexSelected[i] / 10 + 'a')) + (indexSelected[i] % 10 + 1) })));
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
            makeQuestion(Question.ChordQualitiesNotes, _ChordQualities, correctAnswers: noteNames),
            makeQuestion(Question.ChordQualitiesQuality, _ChordQualities, correctAnswers: new[] { qualityName }));
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
             colors[stage].Length <= 3 ? makeQuestion(Question.ColorDecodingIndicatorColors, _ColorDecoding, new[] { "appeared", ordinal(stage + 1) }, colors[stage]) : null,
             colors[stage].Length >= 3 ? makeQuestion(Question.ColorDecodingIndicatorColors, _ColorDecoding, new[] { "did not appear", ordinal(stage + 1) }, _ColorDecoding_ColorNameMapping.Keys.Except(colors[stage]).ToArray()) : null,
             makeQuestion(Question.ColorDecodingIndicatorPattern, _ColorDecoding, new[] { ordinal(stage + 1) }, new[] { patterns[stage] }))));
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
        addQuestion(module, Question.ColoredSquaresFirstGroup, correctAnswers: new[] { fldFirstStageColor.Get().ToString() });
    }

    private IEnumerable<object> ProcessColoredSwitches(KMBombModule module)
    {
        var comp = GetComponent(module, "ColoredSwitchesModule");
        var fldSwitches = GetField<int>(comp, "_switchState");
        var fldSolution = GetField<int>(comp, "_solutionState");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        if (comp == null || fldSwitches == null || fldSolved == null)
            yield break;

        yield return null;
        var initial = fldSwitches.Get();
        if (initial < 0 || initial >= (1 << 5))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Colored Switches because _switchState was {1} at the start (expected 0–31).", _moduleId, initial);
            yield break;
        }

        while (fldSolution.Get() == -1)
            yield return null;  // not waiting for .1 seconds this time to make absolutely sure we catch it before the player toggles another switch
        var afterReveal = fldSwitches.Get();
        if (afterReveal < 0 || afterReveal >= (1 << 5))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Colored Switches because _switchState was {1} after the LEDs came on (expected 0–31).", _moduleId, afterReveal);
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ColoredSwitches);
        addQuestions(module,
            makeQuestion(Question.ColoredSwitchesInitialPosition, _ColoredSwitches, correctAnswers: new[] { Enumerable.Range(0, 5).Select(b => (initial & (1 << b)) != 0 ? "Q" : "R").Reverse().JoinString() }),
            makeQuestion(Question.ColoredSwitchesWhenLEDsCameOn, _ColoredSwitches, correctAnswers: new[] { Enumerable.Range(0, 5).Select(b => (afterReveal & (1 << b)) != 0 ? "Q" : "R").Reverse().JoinString() }));
    }

    private IEnumerable<object> ProcessColorMorse(KMBombModule module)
    {
        var comp = GetComponent(module, "ColorMorseModule");
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
             makeQuestion(Question.ColorMorseColor, _ColorMorse, new[] { ordinal(ix + 1) }, new[] { flashedColorNames[ix] }, flashedColorNames),
             makeQuestion(Question.ColorMorseCharacter, _ColorMorse, new[] { ordinal(ix + 1) }, new[] { flashedCharacters[ix] }, flashedCharacters)
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
            makeQuestion(Question.CoordinatesFirstSolution, _Coordinates, correctAnswers: new[] { shortenCoordinate(clueText) }, preferredWrongAnswers: clues.Cast<object>().Select(c => shortenCoordinate(fldClueText.GetFrom(c))).Where(t => t != null).ToArray()),
            sizeClue == null ? null : makeQuestion(Question.CoordinatesSize, _Coordinates, correctAnswers: new[] { fldClueText.GetFrom(sizeClue) }));
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
                qs.Add(makeQuestion(Question.CrackboxInitialState, _Crackbox, new[] { ((char) ('A' + x)).ToString(), (y + 1).ToString() }, new[] { fldIsBlack.GetFrom(obj) ? "black" : !fldIsLocked.GetFrom(obj) ? "white" : fldValue.GetFrom(obj).ToString() }));
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
        addQuestions(module, allWeather.Select((t, i) => makeQuestion(Question.CreationWeather, _Creation, new[] { ordinal(i + 1) }, new[] { t })));
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
        addQuestion(module, Question.DoubleOhSubmitButton, correctAnswers: new[] { "↕↔⇔⇕◆".Substring(submitIndex, 1) });
    }

    private IEnumerable<object> ProcessDrDoctor(KMBombModule module)
    {
        var comp = GetComponent(module, "DrDoctorModule");
        var fldSymptoms = GetField<string[]>(comp, "_selectableSymptoms");
        var fldDiagnoses = GetField<string[]>(comp, "_selectableDiagnoses");
        var fldDiagnoseText = GetField<TextMesh>(comp, "DiagnoseText", isPublic: true);
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldSymptoms == null || fldDiagnoses == null || fldDiagnoseText == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_DrDoctor);

        var diagnoses = fldDiagnoses.Get();
        var symptoms = fldSymptoms.Get();
        var diagnoseText = fldDiagnoseText.Get();

        if (diagnoses == null || symptoms == null || diagnoseText == null)
            yield break;

        addQuestions(module,
            makeQuestion(Question.DrDoctorDiseases, _DrDoctor, correctAnswers: diagnoses.Except(new[] { diagnoseText.text }).ToArray()),
            makeQuestion(Question.DrDoctorSymptoms, _DrDoctor, correctAnswers: symptoms));
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
        addQuestion(module, Question.FastMathLastLetters, correctAnswers: new[] { letters }, preferredWrongAnswers: prevLetters.ToArray());
    }

    private IEnumerable<object> ProcessFlags(KMBombModule module)
    {
        var comp = GetComponent(module, "FlagsModule");
        var fldMainCountry = GetField<object>(comp, "mainCountry");
        var fldCountries = GetField<IList>(comp, "countries");
        var fldNumber = GetField<int>(comp, "number");
        var fldCanInteract = GetField<bool>(comp, "canInteract");

        if (comp == null || fldMainCountry == null || fldCountries == null || fldNumber == null || fldCanInteract == null)
            yield break;

        yield return null;

        var mainCountry = fldMainCountry.Get();
        var countries = fldCountries.Get();
        var number = fldNumber.Get();

        if (mainCountry == null || countries == null)
            yield break;
        if (countries.Count != 7)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Flags because ‘countries’ has length {1} (expected 7).", _moduleId, countries.Count);
            yield break;
        }
        if (number < 1 || number > 7)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Flags because ‘number’ has unexpected value {1} (expected 1–7).", _moduleId, number);
            yield break;
        }

        var propCountryName = GetProperty<string>(mainCountry, "CountryName", isPublic: true);
        var mainCountrySprite = FlagsSprites.FirstOrDefault(spr => spr.name == propCountryName.GetFrom(mainCountry));
        var otherCountrySprites = countries.Cast<object>().Select(country => FlagsSprites.FirstOrDefault(spr => spr.name == propCountryName.GetFrom(country))).ToArray();

        if (mainCountrySprite == null || otherCountrySprites.Any(spr => spr == null))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Flags because one of the countries has a name with no corresponding sprite: main country = {1}, other countries = [{2}].", _moduleId, propCountryName.GetFrom(mainCountry), countries.Cast<object>().Select(country => propCountryName.GetFrom(country)).JoinString(", "));
            yield break;
        }

        while (fldCanInteract.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Flags);

        addQuestions(module,
            // Displayed number
            makeQuestion(Question.FlagsDisplayedNumber, _Flags, correctAnswers: new[] { number.ToString() }),
            // Main country flag
            makeQuestion(Question.FlagsMainCountry, _Flags, correctAnswers: new[] { mainCountrySprite }, preferredWrongAnswers: otherCountrySprites),
            // Rest of the country flags
            makeQuestion(Question.FlagsCountries, _Flags, correctAnswers: otherCountrySprites, preferredWrongAnswers: FlagsSprites));
    }

    private IEnumerable<object> ProcessFunctions(KMBombModule module)
    {
        var comp = GetComponent(module, "qFunctions");
        var fldFirstLastDigit = GetField<int>(comp, "firstLastDigit");
        var fldSolved = GetField<bool>(comp, "isSolved");
        var fldLeftNum = GetField<int>(comp, "numberA");
        var fldRightNum = GetField<int>(comp, "numberB");
        var fldLetter = GetField<string>(comp, "ruleLetter");

        if (comp == null || fldFirstLastDigit == null || fldSolved == null || fldLeftNum == null || fldRightNum == null || fldLetter == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Functions);

        var lastDigit = fldFirstLastDigit.Get();
        if (lastDigit == -1)
        {
            Debug.LogFormat("[Souvenir #{0}] No questions for Functions because it was solved with no queries! This isn’t a bug, just impressive (or cheating).", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }
        else if (lastDigit > 9 || lastDigit < 0)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Functions because the first last digit is {1} when it should be from 0 to 9.", _moduleId, lastDigit);
            yield break;
        }

        var lNum = fldLeftNum.Get();
        var rNum = fldRightNum.Get();
        if (lNum > 999 || lNum < 1)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Functions because the number to the left of the letter {1} when it should have been from 1 to 999.", _moduleId, lNum);
            yield break;
        }
        if (rNum > 999 || rNum < 1)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Functions because the number to the right of the letter {1} when it should have been from 1 to 999.", _moduleId, rNum);
            yield break;
        }
        var theLetter = fldLetter.Get();
        if (theLetter == null || theLetter.Length != 1)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Functions because the displayed letter is not a single letter (it’s {1}).", _moduleId, theLetter ?? "<null>");
            yield break;
        }

        addQuestions(module,
            makeQuestion(Question.FunctionsLastDigit, _Functions, correctAnswers: new[] { lastDigit.ToString() }),
            makeQuestion(Question.FunctionsLeftNumber, _Functions, correctAnswers: new[] { lNum.ToString() }, preferredWrongAnswers:
                Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(1, 999).ToString()).Distinct().Take(6).ToArray()),
            makeQuestion(Question.FunctionsLetter, _Functions, correctAnswers: new[] { theLetter }),
            makeQuestion(Question.FunctionsRightNumber, _Functions, correctAnswers: new[] { rNum.ToString() }, preferredWrongAnswers:
                Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(1, 999).ToString()).Distinct().Take(6).ToArray()));
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
            makeQuestion(Question.GridLockStartingLocation, _GridLock, correctAnswers: new[] { locations[start] }),
            makeQuestion(Question.GridLockEndingLocation, _GridLock, correctAnswers: new[] { locations[solution] }),
            makeQuestion(Question.GridLockStartingColor, _GridLock, correctAnswers: new[] { colors[(pages[0][start] >> 4) - 1] }));
    }

    private IEnumerable<object> ProcessGryphons(KMBombModule module)
    {
        var comp = GetComponent(module, "Gryphons");
        var fldAge = GetField<int>(comp, "age");
        var fldName = GetField<string>(comp, "theirName");
        var fldSolved = GetField<bool>(comp, "isSolved");

        if (comp == null || fldAge == null || fldName == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Gryphons);

        var age = fldAge.Get();
        var name = fldName.Get();

        if (age < 23 || age > 34)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Gryphons because the gryphon's age is {1} when it should be from 23 to 34.", _moduleId, age);
            yield break;
        }

        addQuestions(module,
            makeQuestion(Question.GryphonsName, _Gryphons, correctAnswers: new[] { name }),
            makeQuestion(Question.GryphonsAge, _Gryphons, correctAnswers: new[] { age.ToString() }, preferredWrongAnswers:
                Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(23, 34).ToString()).Distinct().Take(6).ToArray()));
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
                    return fldLabel == null || fldColor == null || fldIndex == null
                        ? null
                        : new { Label = fldLabel.Get(), Color = fldColor.Get(), Index = fldIndex.Get() };
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
            colors.SelectMany((clrs, stage) => clrs.Select((clr, btnIx) => makeQuestion(Question.LogicalButtonsColor, _LogicalButtons, new[] { _logicalButtonsButtonNames[btnIx], ordinal(stage + 1) }, new[] { clr })))
                .Concat(labels.SelectMany((lbls, stage) => lbls.Select((lbl, btnIx) => makeQuestion(Question.LogicalButtonsLabel, _LogicalButtons, new[] { _logicalButtonsButtonNames[btnIx], ordinal(stage + 1) }, new[] { lbl }))))
                .Concat(initialOperators.Select((op, stage) => makeQuestion(Question.LogicalButtonsOperator, _LogicalButtons, new[] { ordinal(stage + 1) }, new[] { op }))));
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

        addQuestion(module, Question.HexamazePawnColor, correctAnswers: new[] { new[] { "Red", "Yellow", "Green", "Cyan", "Blue", "Pink" }[pawnColor] });
    }

    struct HogwartsModuleSubstitutionInfo
    {
        public string AbbrevInAnswer { get; private set; }
        public string AbbrevInQuestion { get; private set; }
        public HogwartsModuleSubstitutionInfo(string abbrevInAnswer, string abbrevInQuestion = null)
        {
            AbbrevInAnswer = abbrevInAnswer;
            AbbrevInQuestion = abbrevInQuestion;
        }
    }
    private static readonly Dictionary<string, HogwartsModuleSubstitutionInfo> _hogwartsModuleNameSubstitutions = new Dictionary<string, HogwartsModuleSubstitutionInfo>
    {
        { "Rock-Paper-Scissors-L.-Sp.", new HogwartsModuleSubstitutionInfo("R.-P.-S.-L.-Sp.", "R.-P.-S.- L.-Sp.") },
        { "Modules Against Humanity", new HogwartsModuleSubstitutionInfo("M.A. Humanity") },
        { "Monsplode Trading Cards", new HogwartsModuleSubstitutionInfo("Monsplode Tr.C.") },
        { "The London Underground", new HogwartsModuleSubstitutionInfo("L. Underground") },
        { "Foreign Exchange Rates", new HogwartsModuleSubstitutionInfo("Foreign Ex. Rates") },
        { "Ten-Button Color Code", new HogwartsModuleSubstitutionInfo("10-B. Color Code") }
    };
    private static string hogwartsModuleNameSubstitution(string moduleName, bool isQuestion)
    {
        HogwartsModuleSubstitutionInfo result;
        return (_hogwartsModuleNameSubstitutions.TryGetValue(moduleName, out result) ? (isQuestion ? result.AbbrevInQuestion : result.AbbrevInAnswer) : null) ?? moduleName.Replace("'", "’");
    }
    private IEnumerable<object> ProcessHogwarts(KMBombModule module)
    {
        var comp = GetComponent(module, "HogwartsModule");
        var fldModuleNames = GetField<IDictionary>(comp, "_moduleNames");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldModuleNames == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Hogwarts);

        var dic = fldModuleNames.Get();
        if (dic == null || dic.Count == 0)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Hogwarts because _moduleNames is {1}.", _moduleId, dic == null ? "null" : "empty");
            yield break;
        }

        addQuestions(module,
            dic.Keys.Cast<object>().Where(house => dic[house] != null).SelectMany(house => Ut.NewArray(
                makeQuestion(Question.HogwartsHouse, _Hogwarts,
                    formatArgs: new[] { hogwartsModuleNameSubstitution(dic[house].ToString(), isQuestion: true) },
                    correctAnswers: new[] { house.ToString() }),
                makeQuestion(Question.HogwartsModule, _Hogwarts,
                    formatArgs: new[] { house.ToString() },
                    correctAnswers: new[] { hogwartsModuleNameSubstitution(dic[house].ToString(), isQuestion: false) },
                    preferredWrongAnswers: Bomb.GetSolvableModuleNames().Select(m => hogwartsModuleNameSubstitution(m, isQuestion: false)).ToArray()))));
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
            makeQuestion(Question.HumanResourcesEmployees, _HumanResources, new[] { "an employee that was not fired" }, names.Take(5).Where(ix => ix != toFire).Select(ix => fldName.GetFrom(people.GetValue(ix))).ToArray()),
            makeQuestion(Question.HumanResourcesEmployees, _HumanResources, new[] { "an applicant that was not hired" }, names.Skip(5).Where(ix => ix != toHire).Select(ix => fldName.GetFrom(people.GetValue(ix))).ToArray()),
            makeQuestion(Question.HumanResourcesDescriptors, _HumanResources, new[] { "red" }, descs.Take(3).Select(ix => fldDesc.GetFrom(people.GetValue(ix))).ToArray()),
            makeQuestion(Question.HumanResourcesDescriptors, _HumanResources, new[] { "green" }, descs.Skip(3).Select(ix => fldDesc.GetFrom(people.GetValue(ix))).ToArray()));
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
                    formatArgs: new[] { row ? "row" : "column", first ? "first" : "second" },
                    correctAnswers: new[] { _attributes[Question.HuntingColumnsRows].AllAnswers[(hasRowFirst[0] ^ row ^ first ? 1 : 0) | (hasRowFirst[1] ^ row ^ first ? 2 : 0) | (hasRowFirst[2] ^ row ^ first ? 4 : 0)] }));
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
        var qs = new List<QandA>();
        _modulesSolved.IncSafe(_IceCream);

        for (var i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.IceCreamFlavour, _IceCream, new[] { "was on offer, but not sold,", ordinal(i + 1) }, flavours[i].Where(ix => ix != solution[i]).Select(ix => flavourNames[ix]).ToArray()));
            qs.Add(makeQuestion(Question.IceCreamFlavour, _IceCream, new[] { "was not on offer", ordinal(i + 1) }, flavourNames.Where((f, ix) => !flavours[i].Contains(ix)).ToArray()));
            if (i != 2)
                qs.Add(makeQuestion(Question.IceCreamCustomer, _IceCream, new[] { ordinal(i + 1) }, new[] { customerNames[customers[i]] }, preferredWrongAnswers: customers.Select(ix => customerNames[ix]).ToArray()));
        }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessIdentityParade(KMBombModule module)
    {
        var comp = GetComponent(module, "identityParadeScript");
        var fldHairEntries = GetField<List<string>>(comp, "hairEntries");
        var fldBuildEntries = GetField<List<string>>(comp, "buildEntries");
        var fldAttireEntries = GetField<List<string>>(comp, "attireEntries");
        var fldButtonsToOverride = new[] { "hairLeft", "hairRight", "buildLeft", "buildRight", "attireLeft", "attireRight", "suspectLeft", "suspectRight", "convictBut" }.Select(fldName => GetField<KMSelectable>(comp, fldName, isPublic: true)).ToArray();
        var fldTextMeshes = new[] { "hairText", "buildText", "attireText", "suspectText" }.Select(fldName => GetField<TextMesh>(comp, fldName, isPublic: true)).ToArray();

        if (comp == null || fldHairEntries == null || fldBuildEntries == null || fldAttireEntries == null || fldButtonsToOverride.Any(b => b == null) || fldTextMeshes.Any(b => b == null))
            yield break;

        yield return null;

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_IdentityParade);

        var buttonsToOverride = fldButtonsToOverride.Select(f => f.Get()).ToArray();
        if (buttonsToOverride.Any(b => b == null))
            yield break;

        foreach (var btn in buttonsToOverride)
        {
            btn.OnInteract = delegate
            {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btn.transform);
                btn.AddInteractionPunch(0.5f);
                return false;
            };
        }

        var textMeshes = fldTextMeshes.Select(f => f.Get()).ToArray();
        if (textMeshes.Any(b => b == null))
            yield break;
        textMeshes[0].text = "Identity";
        textMeshes[1].text = "Parade";
        textMeshes[2].text = "has been";
        textMeshes[3].text = "solved";

        var hairs = fldHairEntries.Get();
        var builds = fldBuildEntries.Get();
        var attires = fldAttireEntries.Get();

        if (hairs == null || builds == null || attires == null)
            yield break;
        if (hairs.Count != 3 || builds.Count != 3 || attires.Count != 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Identity Parade because ‘hairEntries’, ‘buildEntries’ and/or ‘attireEntries’ has unexpected length: {1}/{2}/{3} (expected 3).", _moduleId, hairs.Count, builds.Count, attires.Count);
            yield break;
        }

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

    private IEnumerable<object> ProcessKudosudoku(KMBombModule module)
    {
        var comp = GetComponent(module, "KudosudokuModule");
        var fldShown = GetField<bool[]>(comp, "_shown");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldShown == null || fldSolved == null)
            yield break;

        // Ensure that Start() has run
        yield return null;

        var shown = fldShown.Get();
        if (shown == null || shown.Length != 16)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Kudosudoku because “_shown” {1} (expected length 16).", _moduleId, shown == null ? "is null" : "has length " + shown.Length);
            yield break;
        }
        // Take a copy of the array
        shown = shown.ToArray();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Kudosudoku);

        addQuestions(module,
            makeQuestion(Question.KudosudokuPrefilled, _Kudosudoku, new[] { "pre-filled" },
                preferredWrongAnswers: KudosudokuSprites,
                correctAnswers: Enumerable.Range(0, 16).Where(ix => shown[ix]).Select(coord => KudosudokuSprites.First(k => k.name == (char) ('A' + (coord % 4)) + (coord / 4 + 1).ToString())).ToArray()),
            makeQuestion(Question.KudosudokuPrefilled, _Kudosudoku, new[] { "not pre-filled" },
                preferredWrongAnswers: KudosudokuSprites,
                correctAnswers: Enumerable.Range(0, 16).Where(ix => !shown[ix]).Select(coord => KudosudokuSprites.First(k => k.name == (char) ('A' + (coord % 4)) + (coord / 4 + 1).ToString())).ToArray()));
    }

    private IEnumerable<object> ProcessLEDEncryption(KMBombModule module)
    {
        var comp = GetComponent(module, "LEDEncryption");
        var fldButtons = GetField<KMSelectable[]>(comp, "buttons", true);
        var fldMultipliers = GetField<int[]>(comp, "layerMultipliers");
        var fldStage = GetField<int>(comp, "layer");

        if (comp == null || fldButtons == null || fldMultipliers == null || fldStage == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var buttons = fldButtons.Get();
        var multipliers = fldMultipliers.Get();
        if (buttons == null || multipliers == null)
            yield break;

        if (buttons.Length != 4)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning LED Encryption because there is an unexpected number of buttons: {1} (expected 4).", _moduleId, buttons.Length);
            yield break;
        }

        if (buttons.Any(x => x == null))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning LED Encryption because at least one of the buttons is null.", _moduleId);
            yield break;
        }

        var buttonLabels = buttons.Select(btn => btn.GetComponentInChildren<TextMesh>()).ToArray();
        if (buttonLabels.Any(x => x == null))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning LED Encryption because at least one of the buttons’ TextMesh is null.", _moduleId);
            yield break;
        }

        if (multipliers.Length < 2 || multipliers.Length > 5 || multipliers.Any(multipler => multipler < 2 || multipler > 7))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning LED Encryption because layerMultipliers has unxepected length {1} / Values [{2}] (Expected length 2-5, Expected values 2-7)", _moduleId, multipliers.Length, multipliers.Select(x => x.ToString()).Join(", "));
            yield break;
        }

        var numStages = multipliers.Length;
        var pressedLetters = new string[numStages];
        var wrongLetters = new HashSet<string>();

        while (fldStage.Get() < numStages)
        {
            foreach (var lbl in buttonLabels)
                wrongLetters.Add(lbl.text);

            // LED Encryption re-hooks the buttons at every press, so we have to re-hook it at each stage as well
            for (int i = 0; i < 4; i++)
                LEDEncryptionReassignButton(buttons[i], buttonLabels[i], fldStage, pressedLetters);

            var stage = fldStage.Get();
            while (fldStage.Get() == stage)
                yield return new WaitForSeconds(0.1f);
        }

        _modulesSolved.IncSafe(_LEDEncryption);
        addQuestions(module, Enumerable.Range(0, pressedLetters.Length)
            .Where(i => pressedLetters[i] != null)
            .Select(stage => makeQuestion(Question.LEDEncryptionPressedLetters, _LEDEncryption, new[] { ordinal(stage + 1) }, new[] { pressedLetters[stage] }, wrongLetters.ToArray())));
    }

    private static void LEDEncryptionReassignButton(KMSelectable btn, TextMesh lbl, FieldInfo<int> fldStage, string[] pressedLetters)
    {
        var prev = btn.OnInteract;
        var stage = fldStage.Get();
        btn.OnInteract = delegate
        {
            var label = lbl.text;
            var ret = prev();
            if (fldStage.Get() > stage)
                pressedLetters[stage] = label;
            return ret;
        };
    }

    private IEnumerable<object> ProcessLEDMath(KMBombModule module)
    {
        var comp = GetComponent(module, "LEDMathScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldLedA = GetField<int>(comp, "ledAIndex");
        var fldLedB = GetField<int>(comp, "ledBIndex");
        var fldLedOp = GetField<int>(comp, "ledOpIndex");

        if (comp == null || fldSolved == null || fldLedA == null || fldLedB == null || fldLedOp == null)
            yield break;

        yield return null;

        var ledA = fldLedA.Get();
        var ledB = fldLedB.Get();
        var ledOp = fldLedOp.Get();
        if (ledA < 0 || ledA > 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning LED Math because ledAIndex has an unexpected value: {1} (expected 0-4).", _moduleId, ledA);
            yield break;
        }

        if (ledB < 0 || ledB > 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning LED Math because ledBIndex has an unexpected value: {1} (expected 0-4).", _moduleId, ledB);
            yield break;
        }

        if (ledOp < 0 || ledOp > 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning LED Math because ledOpIndex has an unexpected value: {1} (expected 0-4).", _moduleId, ledOp);
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_LEDMath);

        var ledColors = new[] { "Red", "Blue", "Green", "Yellow" };

        addQuestions(module,
            makeQuestion(Question.LEDMathLights, _LEDMath, new[] { "LED A" }, new[] { ledColors[ledA] }),
            makeQuestion(Question.LEDMathLights, _LEDMath, new[] { "LED B" }, new[] { ledColors[ledB] }),
            makeQuestion(Question.LEDMathLights, _LEDMath, new[] { "the operator LED" }, new[] { ledColors[ledOp] }));
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
        addQuestion(module, Question.Listening, correctAnswers: new[] { correctCode }, preferredWrongAnswers: attr.ExampleAnswers);
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
            qs.Add(makeQuestion(Question.LogicGatesGates, _LogicGates, new[] { "gate " + (char) ('A' + i) }, new[] { gateTypeNames[i] }));
        if (!isDuplicateInvalid)
            qs.Add(makeQuestion(Question.LogicGatesGates, _LogicGates, new[] { "the duplicated gate" }, new[] { duplicate }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessLondonUnderground(KMBombModule module)
    {
        var comp = GetComponent(module, "londonUndergroundScript");
        var fldStage = GetField<int>(comp, "levelsPassed");
        var fldDepartureStation = GetField<string>(comp, "departureStation");
        var fldDestinationStation = GetField<string>(comp, "destinationStation");
        var fldDepartureOptions = GetField<string[]>(comp, "departureOptions");
        var fldDestinationOptions = GetField<string[]>(comp, "destinationOptions");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        if (comp == null || fldStage == null || fldDepartureStation == null || fldDestinationStation == null || fldDepartureOptions == null || fldDestinationOptions == null || fldSolved == null)
            yield break;

        yield return null;

        Func<string, string> firstWord = str =>
        {
            var pos = str.IndexOf(' ');
            return pos == -1 ? str : str.Substring(0, pos);
        };

        var departures = new List<string>();
        var destinations = new List<string>();
        var extraOptions = new HashSet<string>();
        var lastStage = -1;
        while (!fldSolved.Get())
        {
            var stage = fldStage.Get();
            if (stage != lastStage)
            {
                if (stage == 0)
                {
                    // The player got a strike and the module reset
                    departures.Clear();
                    destinations.Clear();
                    extraOptions.Clear();
                }
                departures.Add(firstWord(fldDepartureStation.Get()));
                destinations.Add(firstWord(fldDestinationStation.Get()));

                foreach (var word in fldDepartureOptions.Get())
                    extraOptions.Add(firstWord(word));
                foreach (var word in fldDestinationOptions.Get())
                    extraOptions.Add(firstWord(word));
                lastStage = stage;
            }
            yield return null;
        }
        _modulesSolved.IncSafe(_LondonUnderground);
        var primary = departures.Union(destinations).ToArray();
        if (primary.Length < 4)
            primary = primary.Union(extraOptions).ToArray();

        addQuestions(module,
            departures.Select((dep, ix) => makeQuestion(Question.LondonUndergroundStations, _LondonUnderground, new[] { ordinal(ix + 1), "departure", "from" }, new[] { firstWord(dep) }, primary)).Concat(
            destinations.Select((dest, ix) => makeQuestion(Question.LondonUndergroundStations, _LondonUnderground, new[] { ordinal(ix + 1), "destination", "to" }, new[] { firstWord(dest) }, primary))));
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
        addQuestion(module, Question.MafiaPlayers, correctAnswers: suspects.Cast<object>().Select(obj => obj.ToString()).Except(new[] { godfather.ToString() }).ToArray());
    }

    private IEnumerable<object> ProcessMahjong(KMBombModule module)
    {
        var comp = GetComponent(module, "MahjongModule");
        var fldTaken = GetField<bool[]>(comp, "_taken");
        var fldCountingRow = GetField<int[]>(comp, "_countingRow");
        var fldMatchRow1 = GetField<int[]>(comp, "_matchRow1");
        var fldMatchRow2 = GetField<int[]>(comp, "_matchRow2");
        var fldCountingTile = GetField<MeshRenderer>(comp, "CountingTile", true);
        var fldParticleEffect = GetField<ParticleSystem>(comp, "Smoke1", true);
        var fldAudio = GetField<KMAudio>(comp, "Audio", true);
        var fldTileSelectables = GetField<KMSelectable[]>(comp, "Tiles", true);

        if (comp == null || fldTaken == null || fldCountingRow == null || fldMatchRow1 == null || fldMatchRow2 == null || fldCountingTile == null || fldParticleEffect == null || fldAudio == null || fldTileSelectables == null)
            yield break;

        yield return null;

        // Capture the player’s matching pairs until the module is solved
        var taken = fldTaken.Get();
        if (taken == null)
            yield break;

        var currentTaken = taken.ToArray();
        var matchedTiles = new List<int>();

        while (true)
        {
            yield return null;
            if (!currentTaken.SequenceEqual(taken))
            {
                matchedTiles.AddRange(Enumerable.Range(0, taken.Length).Where(ix => currentTaken[ix] != taken[ix]));
                if (taken.All(x => x))
                    break;
                currentTaken = taken.ToArray();
            }
        }
        _modulesSolved.IncSafe(_Mahjong);

        // Remove the counting tile, complete with smoke animation
        var countingTile = fldCountingTile.Get();
        var smoke = fldParticleEffect.Get();
        var countingRow = fldCountingRow.Get();
        if (countingTile == null || smoke == null || fldAudio.Get() == null || countingRow == null)
            yield break;

        if (countingTile.gameObject.activeSelf)     // Do it only if another Souvenir module on the same bomb hasn’t already done it
        {
            fldAudio.Get().PlaySoundAtTransform("Elimination", countingTile.transform);
            smoke.transform.localPosition = countingTile.transform.localPosition;
            smoke.Play();
            countingTile.gameObject.SetActive(false);
        }

        // Stuff for the “counting tile” question (bottom-left of the module)
        var countingTileName = countingTile.material.mainTexture.name.Replace(" normal", "");
        var countingTileSprite = MahjongSprites.FirstOrDefault(x => x.name == countingTileName);
        if (countingTileSprite == null)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Mahjong because the sprite for the counting tile ({1}) doesn’t exist.", _moduleId, countingTileName);
            yield break;
        }

        // Stuff for the “matching tiles” question
        var matchRow1 = fldMatchRow1.Get();
        var matchRow2 = fldMatchRow2.Get();
        var tileSelectables = fldTileSelectables.Get();
        if (matchRow1 == null || matchRow2 == null || tileSelectables == null)
            yield break;

        var tileSprites = matchRow1.Concat(matchRow2).Select(ix => MahjongSprites[ix]).ToArray();
        var matchedTileSpriteNames = matchedTiles.Select(ix => tileSelectables[ix].GetComponent<MeshRenderer>().material.mainTexture.name.Replace(" normal", "").Replace(" highlighted", "")).ToArray();
        var matchedTileSprites = matchedTileSpriteNames.Select(name => tileSprites.FirstOrDefault(spr => spr.name == name)).ToArray();

        var invalidIx = matchedTileSprites.IndexOf(spr => spr == null);
        if (invalidIx != -1)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Mahjong because the sprite for one of the matched tiles ({1}) doesn’t exist. matchedTileSpriteNames=[{2}], matchedTileSprites=[{3}], countingRow=[{4}], matchRow1=[{5}], matchRow2=[{6}], tileSprites=[{7}]",
                _moduleId, matchedTileSpriteNames[invalidIx], matchedTileSpriteNames.JoinString(", "), matchedTileSprites.Select(spr => spr == null ? "<null>" : spr.name).JoinString(", "),
                countingRow.JoinString(", "), matchRow1.JoinString(", "), matchRow2.JoinString(", "), tileSprites.Select(spr => spr.name).JoinString(", "));
            yield break;
        }

        addQuestions(module,
            makeQuestion(Question.MahjongCountingTile, _Mahjong, correctAnswers: new[] { MahjongSprites.First(x => x.name == countingTileName) }, preferredWrongAnswers: countingRow.Select(ix => MahjongSprites[ix]).ToArray()),
            makeQuestion(Question.MahjongMatches, _Mahjong, new[] { "first" }, correctAnswers: matchedTileSprites.Take(2).ToArray(), preferredWrongAnswers: tileSprites),
            makeQuestion(Question.MahjongMatches, _Mahjong, new[] { "second" }, correctAnswers: matchedTileSprites.Skip(2).Take(2).ToArray(), preferredWrongAnswers: tileSprites));
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
            makeQuestion(Question.MaritimeFlagsBearing, _MaritimeFlags, correctAnswers: new[] { bearing.ToString() }),
            makeQuestion(Question.MaritimeFlagsCallsign, _MaritimeFlags, correctAnswers: new[] { callsign.ToLowerInvariant() }));
    }

    private IEnumerable<object> ProcessMicrocontroller(KMBombModule module)
    {
        var comp = GetComponent(module, "Micro");
        var fldSolved = GetField<int>(comp, "solved");
        var fldLedsOrder = GetField<List<int>>(comp, "LEDorder");
        var fldPositionTranslate = GetField<int[]>(comp, "positionTranslate");

        if (comp == null || fldSolved == null || fldLedsOrder == null || fldPositionTranslate == null)
            yield break;

        while (fldSolved.Get() == 0)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Microcontroller);

        var ledsOrder = fldLedsOrder.Get();
        if (ledsOrder == null || (ledsOrder.Count != 6 && ledsOrder.Count != 8 && ledsOrder.Count != 10))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Microcontroller because ‘LEDorder’ is null or unexpected length (expected 6, 8 or 10): {1}.", _moduleId, ledsOrder == null ? "<null>" : ledsOrder.Count.ToString());
            yield break;
        }
        var positionTranslate = fldPositionTranslate.Get();
        if (positionTranslate == null || positionTranslate.Length != ledsOrder.Count)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Microcontroller because ‘positionTranslate’ is null or unexpected length (expected {1}): {2}.", _moduleId, ledsOrder.Count, positionTranslate == null ? "<null>" : positionTranslate.Length.ToString());
            yield break;
        }

        addQuestions(module, ledsOrder.Select((led, ix) => makeQuestion(Question.MicrocontrollerPinOrder, _Microcontroller,
            formatArgs: new[] { ordinal(ix + 1) },
            correctAnswers: new[] { (positionTranslate[led] + 1).ToString() },
            preferredWrongAnswers: Enumerable.Range(1, ledsOrder.Count).Select(i => i.ToString()).ToArray())));
    }

    private IEnumerable<object> ProcessMinesweeper(KMBombModule module)
    {
        var comp = GetComponent(module, "MinesweeperModule");
        var fldGrid = GetField<object>(comp, "Game");
        var fldStartingCell = GetField<object>(comp, "StartingCell");

        if (comp == null || fldGrid == null || fldStartingCell == null)
            yield break;

        // Wait for activation as the above fields aren’t fully initialized until then
        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var propSolved = GetProperty<bool>(fldGrid.Get(), "Solved", isPublic: true);
        var fldColor = GetField<string>(fldStartingCell.Get(), "Color", isPublic: true);

        if (propSolved == null || fldColor == null)
            yield break;

        var color = fldColor.Get();

        while (!propSolved.Get())
        {
            if (propSolved.Error)
                yield break;
            yield return new WaitForSeconds(0.1f);
        }

        _modulesSolved.IncSafe(_Minesweeper);
        addQuestion(module, Question.MinesweeperStartingColor, correctAnswers: new[] { color });
    }

    private IEnumerable<object> ProcessModuleMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "ModuleMazeModule");
        var fldSprites = GetField<Sprite[]>(comp, "souvenirSprites", true);
        var fldStart = GetField<string>(comp, "souvenirStart", true);

        if (comp == null || fldSprites == null || fldStart == null)
            yield break;

        while (fldSprites.Get().Count() < 6)
            yield return new WaitForSeconds(.1f);

        var sprites = fldSprites.Get();
        var start = fldStart.Get();

        _modulesSolved.IncSafe(_ModuleMaze);

        addQuestions(module,
            makeQuestion(Question.ModuleMazeStartingIcon, _ModuleMaze,
                correctAnswers: new[] { sprites.FirstOrDefault(spr => spr.name == start) }, preferredWrongAnswers: sprites));
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

        string displayedCreature = null;
        string[] displayedMoves = null;
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

                    var ret = origInteracts[j]();

                    if (curCreatureName == null || curMoveNames == null)
                    {
                        Debug.LogFormat("<Souvenir #{0}> Monsplode, Fight!: Abandoning due to error above.", _moduleId);
                        // Set these to null to signal that something went wrong and we need to abort
                        displayedCreature = null;
                        displayedMoves = null;
                        finished = true;
                    }
                    else
                    {
                        // If ‘revive’ is ‘false’, there is not going to be another stage.
                        if (!fldRevive.Get())
                            finished = true;

                        if (curCreatureName != null && curMoveNames != null)
                        {
                            displayedCreature = curCreatureName;
                            displayedMoves = curMoveNames;
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

        addQuestions(module,
            makeQuestion(Question.MonsplodeFightCreature, _MonsplodeFight, correctAnswers: new[] { displayedCreature }),
            makeQuestion(Question.MonsplodeFightMove, _MonsplodeFight, new[] { "was" }, displayedMoves),
            makeQuestion(Question.MonsplodeFightMove, _MonsplodeFight, new[] { "was not" }, _attributes.Get(Question.MonsplodeFightMove).AllAnswers.Except(displayedMoves).ToArray()));
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
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsCards, _MonsplodeTradingCards, new[] { "card on offer" }, new[] { monsplodes[0] }, monsplodeNames));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsCards, _MonsplodeTradingCards, new[] { "first card in your hand" }, new[] { monsplodes[1] }, monsplodeNames));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsCards, _MonsplodeTradingCards, new[] { "second card in your hand" }, new[] { monsplodes[2] }, monsplodeNames));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsCards, _MonsplodeTradingCards, new[] { "third card in your hand" }, new[] { monsplodes[3] }, monsplodeNames));

        var rarityNames = new[] { "common", "uncommon", "rare", "ultra rare" };
        var rarityIds = new[] { fldRarity.Get() }.Concat(deck.Select(card => fldRarity.GetFrom(card))).ToArray();
        if (rarityIds.Any(rarity => rarity < 0 || rarity >= rarityNames.Length))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Monsplode Trading Cards because of an unexpected rarity ({1}). Names are [{2}].", _moduleId, rarityIds.JoinString(", "), rarityNames.JoinString(", "));
            yield break;
        }
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsRarities, _MonsplodeTradingCards, new[] { "card on offer" }, new[] { rarityNames[rarityIds[0]] }));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsRarities, _MonsplodeTradingCards, new[] { "first card in your hand" }, new[] { rarityNames[rarityIds[1]] }));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsRarities, _MonsplodeTradingCards, new[] { "second card in your hand" }, new[] { rarityNames[rarityIds[2]] }));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsRarities, _MonsplodeTradingCards, new[] { "third card in your hand" }, new[] { rarityNames[rarityIds[3]] }));

        var printVersions = new[] { fldPrintChar.Get() + "" + fldPrintDigit.Get() }.Concat(deck.Select(card => fldPrintChar.GetFrom(card) + "" + fldPrintDigit.GetFrom(card))).ToArray();
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsPrintVersions, _MonsplodeTradingCards, new[] { "card on offer" }, new[] { printVersions[0] }, printVersions));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsPrintVersions, _MonsplodeTradingCards, new[] { "first card in your hand" }, new[] { printVersions[1] }, printVersions));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsPrintVersions, _MonsplodeTradingCards, new[] { "second card in your hand" }, new[] { printVersions[2] }, printVersions));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsPrintVersions, _MonsplodeTradingCards, new[] { "third card in your hand" }, new[] { printVersions[3] }, printVersions));

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
        addQuestions(module, Enumerable.Range(0, 8).Select(i => makeQuestion(Question.MoonLitUnlit, _Moon, new[] { qNames[i] }, new[] { aNames[(i + lightIndex) % 8] })));
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
            makeQuestion(Question.MorseAMazeStartingCoordinate, _MorseAMaze, correctAnswers: new[] { start }),
            makeQuestion(Question.MorseAMazeEndingCoordinate, _MorseAMaze, correctAnswers: new[] { end }),
            makeQuestion(Question.MorseAMazeMorseCodeWord, _MorseAMaze, correctAnswers: new[] { word }, preferredWrongAnswers: words));
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
        addQuestions(module, Enumerable.Range(0, 3).Select(i => makeQuestion(Question.MorsematicsReceivedLetters, _Morsematics, new[] { ordinal(i + 1) }, new[] { chars[i] }, chars)));
    }

    private IEnumerable<object> ProcessMorseWar(KMBombModule module)
    {
        var comp = GetComponent(module, "MorseWar");
        var fldWordNum = GetField<int>(comp, "wordNum");
        var fldLights = GetField<string>(comp, "lights");
        var fldWordTable = comp == null ? null : GetStaticField<string[]>(comp.GetType(), "WordTable");
        var fldRowTable = comp == null ? null : GetStaticField<string[]>(comp.GetType(), "RowTable");
        var fldIsSolved = GetField<bool>(comp, "isSolved");

        if (comp == null || fldWordNum == null || fldLights == null || fldWordTable == null || fldRowTable == null || fldIsSolved == null)
            yield break;

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MorseWar);

        var wordNum = fldWordNum.Get();
        var lights = fldLights.Get();
        var wordTable = fldWordTable.Get();
        var rowTable = fldRowTable.Get();
        if (lights == null || wordTable == null || rowTable == null)
            yield break;
        if (wordNum < 0 || wordNum >= wordTable.Length)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Morse War because ‘wordNum’ is out of range ({1}; expected 0–{2}).", _moduleId, wordNum, wordTable.Length - 1);
            yield break;
        }
        if (lights.Length != 3 || lights.Any(ch => ch < '1' || ch > '6'))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Morse War because ‘lights’ has unexpected value: “{1}” (expected 3 characters 1–6).", _moduleId, lights);
            yield break;
        }
        if (rowTable.Length != 6)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Morse War because ‘RowTable’ has unexpected length ({1}; expected 6).", _moduleId, rowTable.Length);
            yield break;
        }

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.MorseWarCode, _MorseWar, correctAnswers: new[] { wordTable[wordNum].ToUpperInvariant() }));
        var rowNames = new[] { "bottom", "middle", "top" };
        for (int i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.MorseWarLeds, _MorseWar, formatArgs: new[] { rowNames[i] }, correctAnswers: new[] { rowTable[lights[i] - '1'] }));

        addQuestions(module, qs);
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
            makeQuestion(Question.MouseInTheMazeSphere, _MouseInTheMaze, correctAnswers: new[] { new[] { "white", "green", "blue", "yellow" }[goalColor] }),
            makeQuestion(Question.MouseInTheMazeTorus, _MouseInTheMaze, correctAnswers: new[] { new[] { "white", "green", "blue", "yellow" }[torusColor] }));
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

        if (comp == null || fldSolved == null || fldSolution == null || fldNames == null || fldSkipDisplay == null || fldSuspects == null || fldWeapons == null || fldBodyFound == null)
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
                new[] { "a suspect but not the murderer" },
                Enumerable.Range(0, 6).Where(suspectIx => skipDisplay[0, suspectIx] == 0 && suspectIx != actualSuspect).Select(suspectIx => names[0, suspectIx]).ToArray()),
            makeQuestion(Question.MurderSuspect, _Murder,
                new[] { "not a suspect" },
                Enumerable.Range(0, 6).Where(suspectIx => skipDisplay[0, suspectIx] == 1).Select(suspectIx => names[0, suspectIx]).ToArray()),

            makeQuestion(Question.MurderWeapon, _Murder,
                new[] { "a potential weapon but not the murder weapon" },
                Enumerable.Range(0, 6).Where(weaponIx => skipDisplay[1, weaponIx] == 0 && weaponIx != actualWeapon).Select(weaponIx => names[1, weaponIx]).ToArray()),
            makeQuestion(Question.MurderWeapon, _Murder,
                new[] { "not a potential weapon" },
                Enumerable.Range(0, 6).Where(weaponIx => skipDisplay[1, weaponIx] == 1).Select(weaponIx => names[1, weaponIx]).ToArray()),

            bodyFound == actualRoom ? null : makeQuestion(Question.MurderBodyFound, _Murder, correctAnswers: new[] { names[2, bodyFound] }));
    }

    private IEnumerable<object> ProcessMysticSquare(KMBombModule module)
    {
        var comp = GetComponent(module, "MysticSquareModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var fldSkull = GetField<Transform>(comp, "Skull", true);
        var fldKnight = GetField<Transform>(comp, "Knight", true);

        var fldIsInDanger = GetField<bool>(comp, "_isInDanger");
        var fldSkullPos = GetField<int>(comp, "_skullPos");
        var fldKnightPos = GetField<int>(comp, "_knightPos");

        if (comp == null || fldSolved == null || fldSkull == null || fldKnight == null || fldIsInDanger == null || fldSkullPos == null || fldKnightPos == null)
            yield break;

        var skull = fldSkull.Get();
        var knight = fldKnight.Get();

        if (skull == null || knight == null)
            yield break;

        while (!skull.gameObject.activeSelf)
            yield return null;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        // Make sure that the last sliding animation finishes
        yield return new WaitForSeconds(0.5f);

        var knightpos = fldKnightPos.Get();
        var skullpos = fldSkullPos.Get();
        if (knightpos < 0 || knightpos > 8)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Mystic Square because knight is in unexpected position {1} (expected 0-8).", _moduleId, knightpos);
            yield break;
        }
        if (skullpos < 0 || skullpos > 8)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Mystic Square because skull is in unexpected position {1} (expected 0-8).", _moduleId, skullpos);
            yield break;
        }

        // Shrink the skull and knight and then disappear them
        const float duration = 1.5f;
        var elapsed = 0f;
        while (elapsed < duration)
        {
            skull.localScale = Vector3.Lerp(new Vector3(0.004f, 0.004f, 0.004f), Vector3.zero, elapsed / duration);
            knight.localScale = Vector3.Lerp(new Vector3(0.004f, 0.004f, 0.004f), Vector3.zero, elapsed / duration);
            yield return null;
            elapsed += Time.deltaTime;
        }

        skull.gameObject.SetActive(false);
        knight.gameObject.SetActive(false);

        _modulesSolved.IncSafe(_MysticSquare);
        var answers = new[] { "top left", "top middle", "top right", "middle left", "center", "middle right", "bottom left", "bottom middle", "bottom right" };

        // Ask about the knight only if it was ever uncovered
        if (!fldIsInDanger.Get())
            addQuestions(module,
                makeQuestion(Question.MysticSquareKnightSkull, _MysticSquare, new[] { "knight" }, new[] { answers[knightpos] }, answers),
                makeQuestion(Question.MysticSquareKnightSkull, _MysticSquare, new[] { "skull" }, new[] { answers[skullpos] }, answers));
        else
            addQuestions(module,
                makeQuestion(Question.MysticSquareKnightSkull, _MysticSquare, new[] { "skull" }, new[] { answers[skullpos] }, answers));

    }

    private IEnumerable<object> ProcessNeutralization(KMBombModule module)
    {
        var comp = GetComponent(module, "neutralization");
        var fldAcidType = GetField<int>(comp, "acidType");
        var fldAcidVol = GetField<int>(comp, "acidVol");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var fldColorText = GetField<GameObject>(comp, "colorText", isPublic: true);

        if (comp == null || fldAcidType == null || fldAcidVol == null || fldSolved == null || fldColorText == null)
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

        var colorText = fldColorText.Get();
        if (colorText != null)
            colorText.SetActive(false);
        addQuestions(module,
            makeQuestion(Question.NeutralizationColor, _Neutralization, correctAnswers: new[] { new[] { "Yellow", "Green", "Red", "Blue" }[acidType] }),
            makeQuestion(Question.NeutralizationVolume, _Neutralization, correctAnswers: new[] { acidVol.ToString() }));
    }

    private IEnumerable<object> ProcessNonogram(KMBombModule module)
    {
        var comp = GetComponent(module, "NonogramModule");
        var fldColors = GetField<List<string>>(comp, "colors");
        if (comp == null || fldColors == null)
            yield break;

        yield return null;

        // Hook into the module’s OnPass handler
        var isSolved = false;
        module.OnPass += delegate { isSolved = true; return false; };
        yield return new WaitUntil(() => isSolved);
        _modulesSolved.IncSafe(_Nonogram);

        var colors = fldColors.Get();
        if (colors == null)
            yield break;
        if (colors.Count != 10 || colors.Any(c => !Regex.IsMatch(c, @"^[ROYGBP] [ROYGBP]$")))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Nonogram because ‘colors’ has unexpected length ({1}; expected 10) or an unexpected entry ([{2}]).", _moduleId, colors.Count, colors.JoinString(", "));
            yield break;
        }

        var colorNames = new Dictionary<string, string> {
            { "R", "Red" },
            { "O", "Orange" },
            { "Y", "Yellow" },
            { "G", "Green" },
            { "B", "Blue" },
            { "P", "Purple" }
        };

        addQuestions(module, colors.Select((color, index) => makeQuestion(Question.NonogramColors, _Nonogram, new[] { ordinal(index % 5 + 1) + (index >= 5 ? " row" : " column") }, color.Split(' ').Select(c => colorNames[c]).ToArray())));
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
        addQuestions(module, positions.Select((p, i) => makeQuestion(Question.OnlyConnectHieroglyphs, _OnlyConnect, new[] { p }, new[] { hieroglyphs[hieroglyphsDisplayed[i]] })));
    }

    private IEnumerable<object> ProcessOrientationCube(KMBombModule module)
    {
        var comp = GetComponent(module, "OrientationModule");
        var fldInitialVirtualViewAngle = GetField<float>(comp, "initialVirtualViewAngle");
        if (comp == null || fldInitialVirtualViewAngle == null)
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

        var solved = false;

        module.OnPass += delegate { solved = true; return false; };

        while (!solved)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_OrientationCube);

        addQuestion(module, Question.OrientationCubeInitialObserverPosition, correctAnswers: new[] { new[] { "front", "left", "back", "right" }[initialAnglePos] });
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
            makeQuestion(Question.PatternCubeHighlightPosition, _PatternCube, new[] { "top" }, new[] { ordinal(highlightPos + 1) }),
            makeQuestion(Question.PatternCubeHighlightPosition, _PatternCube, new[] { "bottom" }, new[] { ordinal(5 - highlightPos) }));
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

        addQuestions(module, Enumerable.Range(0, 3).Select(i => makeQuestion(
             Question.PerspectivePegsSolution,
            _PerspectivePegs,
            formatArgs: new[] { ordinal(i + 1) },
            correctAnswers: new[] { PerspectivePegsSprites[entered[i]] },
            preferredWrongAnswers: PerspectivePegsSprites)));
    }

    private IEnumerable<object> ProcessPlanets(KMBombModule module)
    {
        var comp = GetComponent(module, "planetsModScript");
        var fldPlanet = GetField<int>(comp, "planetShown");
        var fldStrips = GetField<int[]>(comp, "stripColours");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        if (comp == null || fldPlanet == null || fldStrips == null || fldSolved == null)
            yield break;

        yield return null;

        var planetShown = fldPlanet.Get();
        if (planetShown < 0 || planetShown > 9)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Planets because ‘planetShown’ has unexpected value (expected 0-9): {1}", _moduleId, planetShown);
            yield break;
        }
        var stripColors = fldStrips.Get();
        if (stripColors.Length != 5 || stripColors.Any(x => x < 0 || x > 8))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Planets because ‘stripColors’ has unexpected length or one of its elements has unexpected value (expected length 5 and values 0-8): {1}", _moduleId, string.Format("[{0}]", stripColors.JoinString(", ")));
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Planets);

        var stripNames = new[] { "Aqua", "Blue", "Green", "Lime", "Orange", "Red", "Yellow", "White", "Off" };
        addQuestions(module,
            stripColors.Select((strip, count) => makeQuestion(Question.PlanetsStrips, _Planets, new[] { ordinal(count + 1) }, new[] { stripNames[strip] }))
                .Concat(new[] { makeQuestion(Question.PlanetsPlanet, _Planets, correctAnswers: new[] { PlanetsSprites[planetShown] }, preferredWrongAnswers: (DateTime.Now.Month == 4 && DateTime.Now.Day == 1) ? PlanetsSprites : PlanetsSprites.Take(PlanetsSprites.Length - 2).ToArray()) }));
    }

    private IEnumerable<object> ProcessPoetry(KMBombModule module)
    {
        var comp = GetComponent(module, "PoetryModule");
        var fldWordSelectables = GetField<KMSelectable[]>(comp, "wordSelectables", isPublic: true);
        var fldStage = GetField<int>(comp, "currentStage");
        var fldStageCount = GetField<int>(comp, "stageCount", isPublic: true);
        var fldWordTextMeshes = GetField<TextMesh[]>(comp, "words", isPublic: true);

        if (comp == null || fldWordSelectables == null || fldStage == null || fldStageCount == null || fldWordTextMeshes == null)
            yield break;

        yield return null;

        var answers = new List<string>();
        var selectables = fldWordSelectables.Get();
        if (selectables == null)
            yield break;
        if (selectables.Length != 6 || selectables.Any(s => s == null))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Poetry because ‘wordSelectables’ has unexpected length or contains null (expected length 6, got values: [{1}])",
                _moduleId, selectables.Select(s => s == null ? "<null>" : "NOT NULL").JoinString(", "));
            yield break;
        }
        var wordTextMeshes = fldWordTextMeshes.Get();
        if (wordTextMeshes == null)
            yield break;
        if (wordTextMeshes.Length != 6 || wordTextMeshes.Any(s => s == null))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Poetry because ‘words’ has unexpected length or contains null (expected length 6, got values: [{1}])",
                _moduleId, wordTextMeshes.Select(s => s == null ? "<null>" : "NOT NULL").JoinString(", "));
            yield break;
        }

        for (int i = 0; i < 6; i++)
        {
            var j = i;
            var oldHandler = selectables[i].OnInteract;
            selectables[i].OnInteract = delegate
            {
                var prevStage = fldStage.Get();
                var word = wordTextMeshes[j].text;
                var ret = oldHandler();

                if (fldStage.Get() > prevStage)
                    answers.Add(word);

                return ret;
            };
        }

        while (fldStage.Get() < fldStageCount.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Poetry);

        if (answers.Count != fldStageCount.Get())
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Poetry because the number of answers captured is not equal to the number of stages played ({1}). Answers were: [{2}]",
                _moduleId, fldStageCount.Get(), answers.JoinString(", "));
            yield break;
        }

        addQuestions(module, answers.Select((ans, st) => makeQuestion(Question.PoetryAnswers, _Poetry, formatArgs: new[] { ordinal(st + 1) }, correctAnswers: new[] { ans }, preferredWrongAnswers: answers.ToArray())));
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
        addQuestion(module, Question.PolyhedralMazeStartPosition, null, new[] { fldStartFace.Get().ToString() }, Enumerable.Range(0, 62).Select(i => i.ToString()).ToArray());
    }

    private IEnumerable<object> ProcessProbing(KMBombModule module)
    {
        var comp = GetComponent(module, "ProbingModule");
        var fldWires = GetField<Array>(comp, "mWires");
        var fldDisplay = GetField<TextMesh>(comp, "display", isPublic: true);
        var fldSelectables = GetField<KMSelectable[]>(comp, "selectables", isPublic: true);
        var fldSolved = GetField<bool>(comp, "bSolved");

        if (comp == null || fldWires == null || fldDisplay == null || fldSelectables == null || fldSolved == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Probing);

        var display = fldDisplay.Get();
        if (display == null)
            yield break;

        // Blank out the display so that the user cannot see the readout for the solution wires
        display.text = "";

        // Prevent the user from interacting with the wires after solving
        var selectables = fldSelectables.Get();
        if (selectables == null || selectables.Length != 6 || selectables.Any(s => s == null))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Probing because ‘selectables’ is null or has unexpected length (expected 6): {1}",
                _moduleId, selectables == null ? "<null>" : string.Format("[{0}]", selectables.Select(s => s == null ? "<null>" : "SELECTABLE").JoinString(", ")));
            yield break;
        }
        for (int i = 0; i < selectables.Length; i++)
            selectables[i].OnInteract = delegate { return false; };

        var wireNames = new[] { "red-white", "yellow-black", "green", "gray", "yellow-red", "red-blue" };
        var wireFrequenciesRaw = fldWires.Get();
        if (wireFrequenciesRaw == null || wireFrequenciesRaw.Length != 6)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Probing because ‘mWires’ is null or has unexpected length (expected 6): {1}",
                _moduleId, wireFrequenciesRaw == null ? "<nully>" : string.Format("[{0}]", wireFrequenciesRaw.Cast<object>().Select(s => s.ToString()).JoinString("; ")));
            yield break;
        }

        // Retrieve the missing wire frequencies
        var wireFrequencies = wireFrequenciesRaw.Cast<int>().Select((val, ix) =>
        {
            if (val == 7) return "60Hz";
            if (val == 11) return "50Hz";
            if (val == 13) return "22Hz";
            if (val == 14) return "10Hz";
            Debug.LogFormat(@"<Souvenir #{0}> Abandoning Probing because wire #{1} has unexpected value {2} (expected 7, 11, 13, 14).", _moduleId, ix, val);
            return null;
        }).ToArray();
        if (wireFrequencies.Any(wf => wf == null))
            yield break;

        addQuestions(module, wireFrequencies.Select((wf, ix) => makeQuestion(Question.ProbingFrequencies, _Probing, new[] { wireNames[ix] }, new[] { wf })));
    }

    private IEnumerable<object> ProcessQuintuples(KMBombModule module)
    {
        var comp = GetComponent(module, "quintuplesScript");
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
            numbers.Select((n, ix) => makeQuestion(Question.QuintuplesNumbers, _Quintuples, new[] { ordinal(ix % 5 + 1), ordinal(ix / 5 + 1) }, new[] { (n % 10).ToString() })).Concat(
            colors.Select((color, ix) => makeQuestion(Question.QuintuplesColors, _Quintuples, new[] { ordinal(ix % 5 + 1), ordinal(ix / 5 + 1) }, new[] { color }))).Concat(
            colorCounts.Select((cc, ix) => makeQuestion(Question.QuintuplesColorCounts, _Quintuples, new[] { colorNames[ix] }, new[] { cc.ToString() }))));
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
            addQuestion(module, Question.RhythmsColor, correctAnswers: new[] { new[] { "Blue", "Red", "Green", "Yellow" }[color] });
    }

    private IEnumerable<object> ProcessSchlagDenBomb(KMBombModule module)
    {
        var comp = GetComponent(module, "qSchlagDenBomb");
        var fldSolved = GetField<bool>(comp, "isSolved");
        var fldContestant = GetField<string>(comp, "contestantName");
        var fldContScore = GetField<int>(comp, "scoreC");
        var fldBombScore = GetField<int>(comp, "scoreB");

        if (comp == null || fldSolved == null || fldContestant == null || fldContScore == null || fldBombScore == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SchlagDenBomb);

        var contestant = fldContestant.Get();
        if (contestant == null)
            yield break;

        var cScore = fldContScore.Get();
        var bScore = fldBombScore.Get();
        if (cScore > 75 || cScore < 0)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Schlag den Bomb because the contestant’s score was {1} when it should have been from 0 to 75.", _moduleId, cScore);
            yield break;
        }
        if (bScore > 75 || bScore < 0)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Schlag den Bomb because the bomb’s score was {1} when it should have been from 0 to 75.", _moduleId, bScore);
            yield break;
        }

        addQuestions(module,
            makeQuestion(Question.SchlagDenBombContestantName, _SchlagDenBomb, correctAnswers: new[] { contestant }),
            makeQuestion(Question.SchlagDenBombContestantScore, _SchlagDenBomb, correctAnswers: new[] { cScore.ToString() }, preferredWrongAnswers:
               Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(0, 75).ToString()).Distinct().Take(6).ToArray()),
            makeQuestion(Question.SchlagDenBombBombScore, _SchlagDenBomb, correctAnswers: new[] { bScore.ToString() }, preferredWrongAnswers:
               Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(0, 75).ToString()).Distinct().Take(6).ToArray()));
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

        var rows = new int[3];
        var cols = new int[3];
        var keynums = new int[3];
        while (true)
        {
            while (fldDisplay.Get().text == " ")
            {
                yield return new WaitForSeconds(.1f);
                if (fldSolved.Get())
                    goto solved;
            }

            var stage = fldStage.Get();
            if (stage < 0 || stage > 2)
            {
                Debug.LogFormat("<Souvenir #{0}> Abandoning Sea Shells because ‘stage’ has unexpected value (expected 0-2): {1}", _moduleId, stage);
                yield break;
            }
            rows[stage] = fldRow.Get();
            cols[stage] = fldCol.Get();
            keynums[stage] = fldKeynum.Get();

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
        for (int i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.SeaShells1, _SeaShells, new[] { ordinal(i + 1) }, new[] { new[] { "she sells", "she shells", "sea shells", "sea sells" }[rows[i]] }));
            qs.Add(makeQuestion(Question.SeaShells2, _SeaShells, new[] { ordinal(i + 1) }, new[] { new[] { "sea shells", "she shells", "sea sells", "she sells" }[cols[i]] }));
            qs.Add(makeQuestion(Question.SeaShells3, _SeaShells, new[] { ordinal(i + 1) }, new[] { new[] { "sea shore", "she sore", "she sure", "seesaw" }[keynums[i]] }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessShapesAndBombs(KMBombModule module)
    {
        var comp = GetComponent(module, "ShapesBombs");
        var fldLetter = GetField<int>(comp, "selectLetter");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        if (comp == null || fldLetter == null || fldSolved == null)
            yield break;

        yield return null;

        var initialLetter = fldLetter.Get();
        if (initialLetter < 0 || initialLetter > 14)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Shapes And Bombs because ‘initialLetter’ has unexpected value (expected 0-14): {1}", _moduleId, initialLetter);
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_ShapesBombs);
        var letterChars = new[] { "A", "B", "D", "E", "G", "I", "K", "L", "N", "O", "P", "S", "T", "X", "Y" };
        addQuestion(module, Question.ShapesAndBombsInitialLetter, correctAnswers: new[] { letterChars[initialLetter] });
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
            addQuestion(module, Question.ShapeShiftInitialShape, correctAnswers: new[] { ((char) ('A' + stR + (4 * stL))).ToString() }, preferredWrongAnswers: answers.ToArray());
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
                qs.Add(makeQuestion(Question.SillySlots, _SillySlots, new[] { ordinal(slot + 1), ordinal(stage + 1) }, new[] { slotStrings[slot] }, slotStrings));
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

        addQuestions(module, calls.Select((c, ix) => makeQuestion(Question.SimonSamplesSamples, _SimonSamples, new[] { _simonSamplesFAs[ix] }, new[] { (ix == 0 ? c : c.Substring(calls[ix - 1].Length)).Replace("0", "K").Replace("1", "S").Replace("2", "H").Replace("3", "O") })));
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
            qs.Add(makeQuestion(Question.SimonScreamsFlashing, _SimonScreams, new[] { ordinal(i + 1) }, new[] { colors[lastSeq[i]] }));

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
                    new[] { fldRuleName.GetFrom(rules.GetValue(rule)) },
                    new[] { applicableStages.Count == stageRules.Length ? "all of them" : applicableStages.JoinString(", ", lastSeparator: " and ") },
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
            makeQuestion(Question.SimonSendsReceivedLetters, _SimonSends, new[] { "red" }, new[] { charR }, new[] { charG, charB }),
            makeQuestion(Question.SimonSendsReceivedLetters, _SimonSends, new[] { "green" }, new[] { charG }, new[] { charR, charB }),
            makeQuestion(Question.SimonSendsReceivedLetters, _SimonSends, new[] { "blue" }, new[] { charB }, new[] { charR, charG }));
    }

    private IEnumerable<object> ProcessSimonShrieks(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonShrieksModule");
        var fldArrow = GetField<int>(comp, "_arrow");
        var fldButtonColors = GetField<int[]>(comp, "_buttonColors");
        var fldFlashingButtons = GetField<int[]>(comp, "_flashingButtons");
        var fldStage = GetField<int>(comp, "_stage");

        if (comp == null || fldArrow == null || fldButtonColors == null || fldFlashingButtons == null || fldStage == null)
            yield break;

        while (fldStage.Get() < 3)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonShrieks);

        var colorNames = new[] { "Red", "Yellow", "Green", "Cyan", "Blue", "White", "Magenta" };
        var arrow = fldArrow.Get();
        var buttonColors = fldButtonColors.Get();
        var flashingButtons = fldFlashingButtons.Get();

        if (arrow < 0 || arrow > 6)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Shrieks because ‘_arrow’ has an unexpected value ({1}, expected 0–6).", _moduleId, arrow);
            yield break;
        }
        if (buttonColors == null || flashingButtons == null)
            yield break;
        if (buttonColors.Length != 7 || flashingButtons.Length != 8 || buttonColors.Any(b => b < 0 || b > 6) || flashingButtons.Any(b => b < 0 || b > 6))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Shrieks because ‘_buttonColors’ or ‘_flashingButtons’ has an unexpected length or value: [{1}], [{2}], expected length 7/8 and values 0–6.",
                _moduleId, buttonColors.JoinString(", "), flashingButtons.JoinString(", "));
            yield break;
        }

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.SimonShrieksArrow, _SimonShrieks, correctAnswers: new[] { colorNames[buttonColors[arrow]] }));
        for (int i = 0; i < flashingButtons.Length; i++)
            qs.Add(makeQuestion(Question.SimonShrieksFlashingColors, _SimonShrieks, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { colorNames[buttonColors[flashingButtons[i]]] }));
        addQuestions(module, qs);
    }

    private static readonly string[] _SimonSings_Notes = { "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B" };

    private IEnumerable<object> ProcessSimonSings(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSingsModule");
        var fldCurStage = GetField<int>(comp, "_curStage");
        var fldFlashingColors = GetField<int[][]>(comp, "_flashingColors");

        if (comp == null || fldCurStage == null || fldFlashingColors == null)
            yield break;

        while (fldCurStage.Get() < 3)
            yield return new WaitForSeconds(.1f);

        var flashingColorSequences = fldFlashingColors.Get();
        if (flashingColorSequences == null || flashingColorSequences.Length != 3 || flashingColorSequences.Any(seq => seq.Any(col => col < 0 || col >= _SimonSings_Notes.Length)))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Sings because one of the flashing “colors” is out of range (values from 0–11 expected): [{1}].", _moduleId, flashingColorSequences.Select(seq => string.Format("[{0}]", seq.JoinString(", "))).JoinString("; "));
            yield break;
        }

        _modulesSolved.IncSafe(_SimonSings);
        addQuestions(module, flashingColorSequences.SelectMany((seq, stage) => seq.Select((col, ix) => makeQuestion(Question.SimonSingsFlashing, _SimonSings, new[] { ordinal(ix + 1), ordinal(stage + 1) }, new[] { _SimonSings_Notes[col] }))));
    }

    private IEnumerable<object> ProcessSimonSpeaks(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSpeaksModule");
        var fldSequence = GetField<int[]>(comp, "_sequence");
        var fldColors = GetField<int[]>(comp, "_colors");
        var fldWords = GetField<int[]>(comp, "_words");
        var fldLanguages = GetField<int[]>(comp, "_languages");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var fldWordsTable = GetStaticField<string[][]>(comp.GetType(), "_wordsTable");
        var fldPositionNames = GetStaticField<string[]>(comp.GetType(), "_positionNames");

        if (comp == null || fldSequence == null || fldColors == null || fldWords == null || fldLanguages == null || fldSolved == null || fldWordsTable == null || fldPositionNames == null)
            yield break;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonSpeaks);

        var sequence = fldSequence.Get();
        var colors = fldColors.Get();
        var words = fldWords.Get();
        var languages = fldLanguages.Get();
        var wordsTable = fldWordsTable.Get();
        var positionNames = fldPositionNames.Get();
        if (sequence == null || colors == null || words == null || languages == null || wordsTable == null || positionNames == null)
            yield break;
        if (colors.Length != 9 || words.Length != 9 || languages.Length != 9 || wordsTable.Length != 9 || positionNames.Length != 9)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Speaks because one of “_colors” ({1})/“_words” ({2})/“_languages” ({3})/“_wordsTable” ({4})/“_positionNames” ({5}) is not of length 9.", _moduleId, colors.Length, words.Length, languages.Length, wordsTable.Length, positionNames.Length);
            yield break;
        }
        if (sequence.Length != 5)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon Speaks because “_sequence” is of length {1} instead of 5.", _moduleId, sequence.Length);
            yield break;
        }

        addQuestions(module,
            Enumerable.Range(0, 5).Select(ix => makeQuestion(Question.SimonSpeaksPositions, _SimonSpeaks, new[] { ordinal(ix + 1) }, new[] { positionNames[sequence[ix]] })).Concat(
            Enumerable.Range(0, 5).Select(ix => makeQuestion(Question.SimonSpeaksColors, _SimonSpeaks, new[] { ordinal(ix + 1) }, new[] { wordsTable[colors[sequence[ix]]][0] })).Concat(
            Enumerable.Range(0, 5).Select(ix => makeQuestion(Question.SimonSpeaksWords, _SimonSpeaks, new[] { ordinal(ix + 1) }, new[] { wordsTable[words[sequence[ix]]][languages[sequence[ix]]] })))));
    }

    private IEnumerable<object> ProcessSimonsStar(KMBombModule module)
    {
        var comp = GetComponent(module, "simonsStarScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldFlashes = "first,second,third,fourth,fifth".Split(',').Select(n => GetField<string>(comp, n + "FlashColour", isPublic: true)).ToArray();

        if (comp == null || fldSolved == null || fldFlashes.Any(f => f == null))
            yield break;

        yield return null;

        var flashes = fldFlashes.Select(f => f.Get()).ToArray();
        var validColors = new[] { "red", "yellow", "green", "blue", "purple" };

        if (flashes.Any(f => !validColors.Contains(f)))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Simon’s Star because one of the flashes has an unexpected value: [{1}] (expected red, green, yellow, blue, or purple).", _moduleId, flashes.JoinString(", ", @"""", @""""));
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonsStar);

        addQuestions(module, flashes.Select((f, ix) => makeQuestion(Question.SimonsStarColors, _SimonsStar, new[] { ordinal(ix + 1) }, new[] { f })));
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
                    new[] { "color(s) flashed", ordinal(i + 1) },
                    new[] { c == 4 ? "all 4" : puzzleDisplay[i].Select((v, j) => v ? colorNames[j] : null).Where(x => x != null).JoinString(", ") }));
            if (c != 1)
                qs.Add(makeQuestion(Question.SimonStatesDisplay, _SimonStates,
                    new[] { "color(s) didn’t flash", ordinal(i + 1) },
                    new[] { c == 4 ? "none" : puzzleDisplay[i].Select((v, j) => v ? null : colorNames[j]).Where(x => x != null).JoinString(", ") }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSimonStops(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonStops");
        var fldColors = GetField<string[]>(comp, "outputSequence");
        var fldSolved = GetField<bool>(comp, "isSolved");

        if (comp == null || fldColors == null || fldSolved == null)
            yield break;

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonStops);

        var colors = fldColors.Get();
        if (colors == null || colors.Length != 5)
        {
            Debug.LogFormat(@"<Souvenir #{0}> Abandoning Simon Stops because the sequence is [{1}], which is {2}, when we expected five colors.", _moduleId,
                colors == null ? "null" : string.Format("[{0}]", colors.JoinString(", ")), colors == null ? "null" : colors.Length + " colors");
            yield break;
        }

        addQuestions(module, Enumerable.Range(0, 5).Select(ix =>
             makeQuestion(Question.SimonStopsColors, _SimonStops, new[] { ordinal(ix + 1) }, new[] { colors[ix] }, colors)));
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
        addQuestion(module, Question.SkewedSlotsOriginalNumbers, correctAnswers: new[] { originalNumbers.Last() },
            preferredWrongAnswers: originalNumbers.Take(originalNumbers.Count - 1).Concat(Enumerable.Range(0, int.MaxValue).Select(_ => Rnd.Range(0, 1000).ToString("000"))).Where(str => str != originalNumbers.Last()).Distinct().Take(5).ToArray());
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
            qs.Add(makeQuestion(questions[i], _Skyrim, correctAnswers: list.Except(new[] { correct }).Select(t => t.name.Replace("'", "’")).ToArray()));
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
        qs.Add(makeQuestion(Question.SkyrimDragonShout, _Skyrim, correctAnswers: shoutNames.Except(new[] { correctShoutName }).Select(n => n.Replace("'", "’")).ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSnooker(KMBombModule module)
    {
        var comp = GetComponent(module, "snookerScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldActiveReds = GetField<int>(comp, "activeReds");

        if (comp == null || fldSolved == null || fldActiveReds == null)
            yield break;

        yield return null;

        var activeReds = fldActiveReds.Get();
        if (activeReds < 8 || activeReds > 11)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Snooker because activeReds has an unexpected value: {1} (expected 8-11).", _moduleId, activeReds);
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Snooker);

        addQuestion(module, Question.SnookerReds, correctAnswers: new[] { activeReds.ToString() });
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
                    new[] { ordinal(i + 1) },
                    new[] { pictureNameMapping[pics[i].name].Name },
                    pictureNameMapping.Values.Where(inf => inf.Stage == i).Select(inf => inf.Name).ToArray()))
            .Concat(new[] { "Running Boots", "Invincibility", "Extra Life", "Rings" }.Select((screenName, i) =>
                makeQuestion(
                    Question.SonicTheHedgehogSounds,
                    _SonicTheHedgehog,
                    new[] { screenName },
                    new[] { soundNameMapping[sounds[i]] },
                    sounds.Select(s => soundNameMapping[s]).ToArray()))));
    }

    private IEnumerable<object> ProcessSouvenir(KMBombModule module)
    {
        var comp = module.GetComponent<SouvenirModule>();
        if (comp == null || comp == this)
        {
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        yield return null;

        int souvenirCount;
        if (!_moduleCounts.TryGetValue(_Souvenir, out souvenirCount) || souvenirCount != 2)
        {
            if (souvenirCount > 2)
                Debug.LogFormat("[Souvenir #{0}] There are more than two Souvenir modules on this bomb. Not asking any questions about them.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        var modules = _attributes.Where(x => x.Value != null).Select(x => x.Value.ModuleNameWithThe).Distinct().ToArray();
        while (comp._currentQuestion == null)
            yield return new WaitForSeconds(0.1f);

        var firstQuestion = comp._currentQuestion;
        var firstModule = firstQuestion.ModuleNameWithThe;
        if (!modules.Contains(firstModule))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Souvenir because the first question was on “{1}”, which is not a module I recognize.", _moduleId, firstModule);
            yield break;
        }

        // Wait for the user to solve that question before asking about it
        while (comp._currentQuestion == firstQuestion)
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_Souvenir);
        addQuestion(module, Question.SouvenirFirstQuestion, null, new[] { firstModule }, modules);
    }

    private IEnumerable<object> ProcessSwitch(KMBombModule module)
    {
        var comp = GetComponent(module, "Switch");
        var fldSolved = GetField<bool>(comp, "SOLVED");
        var fldBottomColor = GetField<int>(comp, "BottomColor");
        var fldTopColor = GetField<int>(comp, "TopColor");
        var fldSwitch = GetField<KMSelectable>(comp, "FlipperSelectable", isPublic: true);
        var fldFirstSuccess = GetField<bool>(comp, "FirstSuccess");

        if (comp == null || fldSolved == null || fldBottomColor == null || fldTopColor == null || fldSwitch == null || fldFirstSuccess == null)
            yield break;

        yield return null;

        var colorNames = new[] { "red", "orange", "yellow", "green", "blue", "purple" };

        var topColor1 = fldTopColor.Get();
        var bottomColor1 = fldBottomColor.Get();
        var topColor2 = -1;
        var bottomColor2 = -1;

        Debug.LogFormat("<Souvenir #{0}> The Switch: initial colors: {1}/{2}", _moduleId, topColor1, bottomColor1);

        var switchSelectable = fldSwitch.Get();
        if (switchSelectable == null)
            yield break;

        var prevInteract = switchSelectable.OnInteract;
        switchSelectable.OnInteract = delegate
        {
            var ret = prevInteract();
            var firstSuccess = fldFirstSuccess.Get();
            if (!firstSuccess)  // This means the user got a strike. Need to retrieve the new colors
            {
                topColor1 = fldTopColor.Get();
                bottomColor1 = fldBottomColor.Get();
                Debug.LogFormat("<Souvenir #{0}> The Switch: Strike! Initial colors now: {1}/{2}", _moduleId, topColor1, bottomColor1);
            }
            else if (!fldSolved.Get())
            {
                topColor2 = fldTopColor.Get();
                bottomColor2 = fldBottomColor.Get();
                Debug.LogFormat("<Souvenir #{0}> The Switch: Success! Second set of colors now: {1}/{2}", _moduleId, topColor2, bottomColor2);
            }
            return ret;
        };

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Switch);

        if (topColor1 < 1 || topColor1 > 6 || bottomColor1 < 1 || bottomColor1 > 6 || topColor2 < 1 || topColor2 > 6 || bottomColor2 < 1 || bottomColor2 > 6)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning The Switch because topColor1/bottomColor1/topColor2/bottomColor2 has an unexpected value: {1}, {2}, {3}, {4} (expected 1–6).", _moduleId, topColor1, bottomColor1, topColor2, bottomColor2);
            yield break;
        }

        Debug.LogFormat("<Souvenir #{0}> The Switch: Asking questions. Color values: {1}/{2}/{3}/{4}", _moduleId, topColor1, bottomColor1, topColor2, bottomColor2);

        addQuestions(module,
            makeQuestion(Question.SwitchInitialColor, _Switch, new[] { "top", "second-last" }, new[] { colorNames[topColor1 - 1] }),
            makeQuestion(Question.SwitchInitialColor, _Switch, new[] { "bottom", "second-last" }, new[] { colorNames[bottomColor1 - 1] }),
            makeQuestion(Question.SwitchInitialColor, _Switch, new[] { "top", "last" }, new[] { colorNames[topColor2 - 1] }),
            makeQuestion(Question.SwitchInitialColor, _Switch, new[] { "bottom", "last" }, new[] { colorNames[bottomColor2 - 1] }));
    }

    private IEnumerable<object> ProcessSwitches(KMBombModule module)
    {
        var comp = GetComponent(module, "SwitchModule");
        var fldSwitches = GetField<MonoBehaviour[]>(comp, "Switches", isPublic: true);
        var fldGoal = GetField<object>(comp, "goalConfiguration");
        var mthCurConfig = GetMethod<object>(comp, "GetCurrentConfiguration", 0);
        if (comp == null || fldSwitches == null || fldGoal == null || mthCurConfig == null)
            yield break;

        yield return null;
        var switches = fldSwitches.Get();
        if (switches == null || switches.Length != 5 || switches.Any(s => s == null))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Switches because Switches is {1} (expected length 5 and no nulls).", _moduleId,
                switches == null ? "<null>" : string.Format("[{0}]", switches.Select(sw => sw == null ? "null" : "not null").Join(", ")));
            yield break;
        }
        var initialState = switches.Select(sw => sw.GetComponent<Animator>().GetBool("Up") ? "Q" : "R").JoinString();

        while (!fldGoal.Get().Equals(mthCurConfig.Invoke()))
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Switches);
        addQuestion(module, Question.SwitchesInitialPosition, correctAnswers: new[] { initialState });
    }

    private IEnumerable<object> ProcessSymbolCycle(KMBombModule module)
    {
        var comp = GetComponent(module, "SymbolCycleModule");
        var fldCycles = GetField<int[][]>(comp, "_cycles");
        var fldState = GetField<object>(comp, "_state");

        if (comp == null || fldCycles == null || fldState == null)
            yield break;

        yield return null;

        int[][] cycles = null;
        while (fldState.Get().ToString() != "Solved")
        {
            cycles = fldCycles.Get();
            if (cycles == null)
                yield break;
            if (cycles.Length != 2)
            {
                Debug.LogFormat("<Souvenir #{0}> Abandoning Symbol Cycle because the number of screens is unexpected (expected 2, got {1}).", _moduleId, cycles.Length);
                yield break;
            }

            if (cycles.Any(x => x == null || x.Length < 2 || x.Length > 5))
            {
                Debug.LogFormat("<Souvenir #{0}> Abandoning Symbol Cycle because the number of cycles per screen is unexpected (expected 2-5, got {1}).", _moduleId, cycles.Select(x => x == null ? "<null>" : x.Length.ToString()).JoinString(", "));
                yield break;
            }

            while (fldState.Get().ToString() == "Cycling")
                yield return new WaitForSeconds(0.1f);

            while (fldState.Get().ToString() == "Retrotransphasic" || fldState.Get().ToString() == "Anterodiametric")
                yield return new WaitForSeconds(0.1f);
        }

        if (cycles == null)
            yield break;

        _modulesSolved.IncSafe(_SymbolCycle);
        addQuestions(module, new[] { "left", "right" }.Select((screen, ix) => makeQuestion(Question.SymbolCycleSymbolCounts, _SymbolCycle, new[] { screen }, new[] { cycles[ix].Length.ToString() })));
    }

    private IEnumerable<object> ProcessSymbolicCoordinates(KMBombModule module)
    {
        var comp = GetComponent(module, "symbolicCoordinatesScript");
        var fldLetter1 = GetField<string>(comp, "letter1");
        var fldLetter2 = GetField<string>(comp, "letter2");
        var fldLetter3 = GetField<string>(comp, "letter3");
        var fldStage = GetField<int>(comp, "stage");

        if (comp == null || fldLetter1 == null || fldLetter2 == null || fldLetter3 == null || fldStage == null)
            yield break;

        yield return null;

        var letter1 = fldLetter1.Get();
        var letter2 = fldLetter2.Get();
        var letter3 = fldLetter3.Get();

        if (letter1 == null || letter2 == null || letter3 == null)
            yield break;

        var stageLetters = new[] { letter1.Split(' '), letter2.Split(' '), letter3.Split(' ') };

        if (stageLetters.Any(x => x.Length != 3) || stageLetters.SelectMany(x => x).Any(y => !"PLACE".Contains(y)))
        {
            Debug.LogFormat("<Souvenir #{1}> Abandoning Symbolic Coordinates because one of the stages has fewer than 3 symbols or symbols are of unexpected value (expected symbols “ACELP”, got “{0}”).", stageLetters.Select(x => string.Format("“{0}”", x.JoinString())).JoinString(", "), _moduleId);
            yield break;
        }

        while (fldStage.Get() < 4)
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_SymbolicCoordinates);

        var position = new[] { "left", "middle", "right" };
        addQuestions(module, stageLetters.SelectMany((letters, stage) => letters.Select((letter, pos) => makeQuestion(
            Question.SymbolicCoordinateSymbols,
            _SymbolicCoordinates,
            formatArgs: new[] { position[pos], ordinal(stage + 1) },
            correctAnswers: new[] { SymbolicCoordinatesSprites["ACELP".IndexOf(letter, StringComparison.Ordinal)] },
            preferredWrongAnswers: SymbolicCoordinatesSprites))));
    }

    private IEnumerable<object> ProcessSynonyms(KMBombModule module)
    {
        var comp = GetComponent(module, "Synonyms");
        var fldNumberText = GetField<TextMesh>(comp, "NumberText", isPublic: true);
        var fldGoodLabel = GetField<TextMesh>(comp, "GoodLabel", isPublic: true);
        var fldBadLabel = GetField<TextMesh>(comp, "BadLabel", isPublic: true);
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldNumberText == null || fldGoodLabel == null || fldBadLabel == null || fldSolved == null)
            yield break;

        yield return null;
        var numberText = fldNumberText.Get();
        var goodLabel = fldGoodLabel.Get();
        var badLabel = fldBadLabel.Get();
        if (numberText == null || goodLabel == null || badLabel == null)
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
        numberText.gameObject.SetActive(false);
        badLabel.text = "INPUT";
        goodLabel.text = "ACCEPTED";

        addQuestion(module, Question.SynonymsNumber, correctAnswers: new[] { number.ToString() });
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

        addQuestion(module, Question.BulbButtonPresses, correctAnswers: new[] { buttonPresses });
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
        addQuestions(module, makeQuestion(Question.GamepadNumbers, _Gamepad, correctAnswers: new[] { string.Format("{0:00}:{1:00}", x, y) },
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

        addQuestion(module, Question.TapCodeReceivedWord, correctAnswers: new[] { chosenWord }, preferredWrongAnswers: words);
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
            .Select(slot => makeQuestion(Question.TenButtonColorCodeInitialColors, _TenButtonColorCode, new[] { ordinal(slot + 1), ordinal(stage + 1) }, new[] { colorNames[colors[slot]] }))));
    }

    private IEnumerable<object> ProcessTextField(KMBombModule module)
    {
        var comp = GetComponent(module, "TextField");
        var fldDisplay = GetField<TextMesh[]>(comp, "ButtonLabels", true);
        var fldActivated = GetField<bool>(comp, "_lightson");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        if (comp == null || fldDisplay == null || fldActivated == null || fldSolved == null)
            yield break;

        var displayMeshes = fldDisplay.Get();
        if (displayMeshes == null)
            yield break;

        if (displayMeshes.Any(x => x == null))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Text Field because one of the text meshes in ‘ButtonLabels’ is null.", _moduleId);
            yield break;
        }

        if (displayMeshes.Length != 12)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Text Field because ‘ButtonLabels’ has unexpected length {1} (expected 12).", _moduleId, displayMeshes.Length);
            yield break;
        }

        while (!fldActivated.Get())
            yield return new WaitForSeconds(0.1f);

        var answer = displayMeshes.Select(x => x.text).FirstOrDefault(x => x != "✓" && x != "✗");
        var possibleAnswers = new[] { "A", "B", "C", "D", "E", "F" };

        if (!possibleAnswers.Contains(answer))
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Text Field because answer ‘{1}’ is not of expected value ({2}).", _moduleId, answer ?? "<null>", possibleAnswers.Join(", "));
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        for (var i = 0; i < 12; i++)
            if (displayMeshes[i].text == answer)
                displayMeshes[i].text = "✓";

        _modulesSolved.IncSafe(_TextField);
        addQuestion(module, Question.TextFieldDisplay, correctAnswers: new[] { answer });
    }

    private IEnumerable<object> ProcessThirdBase(KMBombModule module)
    {
        var comp = GetComponent(module, "ThirdBaseModule");
        var fldDisplay = GetField<TextMesh>(comp, "Display", isPublic: true);
        var fldStage = GetField<int>(comp, "stage");
        var fldActivated = GetField<bool>(comp, "isActivated");
        var fldSolved = GetField<bool>(comp, "isPassed");

        if (comp == null || fldDisplay == null || fldStage == null || fldActivated == null || fldSolved == null)
            yield break;

        yield return null;

        var displayTextMesh = fldDisplay.Get();
        if (displayTextMesh == null)
            yield break;

        while (!fldActivated.Get())
            yield return new WaitForSeconds(0.1f);

        var displayWords = new string[2];

        for (var i = 0; i < 2; i++)
            while (fldStage.Get() == i)
            {
                while (!fldActivated.Get())
                    yield return new WaitForSeconds(0.1f);

                displayWords[i] = displayTextMesh.text;

                while (fldActivated.Get())
                    yield return new WaitForSeconds(0.1f);
            }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_ThirdBase);
        addQuestions(module, displayWords.Select((word, stage) => makeQuestion(Question.ThirdBaseDisplay, _ThirdBase, new[] { ordinal(stage + 1) }, new[] { word })));
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
            formatArgs: new[] { buttonNames[Array.IndexOf(keypadPhysical, keypadButtons[ix])] },
            correctAnswers: new[] { placedX[ix] == null ? (ix + 1).ToString() : placedX[ix].Value ? "X" : "O" })));
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
            makeQuestion(Question.TimezoneCities, _Timezone, new[] { "departure" }, new[] { fldFromCity.Get() }),
            makeQuestion(Question.TimezoneCities, _Timezone, new[] { "destination" }, new[] { fldToCity.Get() }));
    }

    private IEnumerable<object> ProcessTurtleRobot(KMBombModule module)
    {
        var comp = GetComponent(module, "TurtleRobot");
        var fldCursor = GetField<int>(comp, "_cursor");
        var fldCommands = GetField<IList>(comp, "_commands");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var fldButtonDelete = GetField<KMSelectable>(comp, "ButtonDelete", isPublic: true);
        var mthFormatCommand = GetMethod<string>(comp, "FormatCommand", 2);

        if (comp == null || fldCursor == null || fldCommands == null || fldSolved == null || fldButtonDelete == null || mthFormatCommand == null)
            yield break;

        yield return null;

        var commands = fldCommands.Get();
        var deleteButton = fldButtonDelete.Get();
        if (commands == null || deleteButton == null)
            yield break;

        var codeLines = commands.Cast<object>().Select(cmd => mthFormatCommand.Invoke(cmd, false)).ToArray();
        Debug.LogFormat("<Souvenir #{0}> Turtle Robot lines:\n{1}", _moduleId, codeLines.Select((cl, ix) => string.Format("{0}. {1}", ix, cl)).JoinString("\n"));
        var bugs = new List<string>();
        var bugsMarked = new HashSet<int>();

        var buttonHandler = deleteButton.OnInteract;
        deleteButton.OnInteract = delegate
        {
            var ret = buttonHandler();
            var cursor = fldCursor.Get();
            var command = mthFormatCommand.Invoke(commands[cursor], true);
            Debug.LogFormat("<Souvenir #{0}> Turtle Robot: Delete button pressed on {1} at cursor position {2}", _moduleId, command, cursor);
            if (command.StartsWith("#") && bugsMarked.Add(cursor))
            {
                bugs.Add(codeLines[cursor]);
                Debug.LogFormat("<Souvenir #{0}> Turtle Robot: — Added", _moduleId);
            }
            else
                Debug.LogFormat("<Souvenir #{0}> Turtle Robot: — NOT added", _moduleId);
            return ret;
        };

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        Debug.LogFormat("<Souvenir #{0}> Turtle Robot solved. Bugs:\n{1}", _moduleId, bugs.JoinString("\n"));
        _modulesSolved.IncSafe(_TurtleRobot);
        addQuestions(module, bugs.Take(2).Select((bug, ix) => makeQuestion(Question.TurtleRobotCodeLines, _TurtleRobot, new[] { ordinal(ix + 1) }, new[] { bug }, codeLines)));
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
            qs.Add(makeQuestion(Question.TwoBitsResponse, _TwoBits, new[] { "first" }, new[] { firstResponse.ToString("00") }, preferredWrongAnswers));
            qs.Add(makeQuestion(Question.TwoBitsResponse, _TwoBits, new[] { "second" }, new[] { secondResponse.ToString("00") }, preferredWrongAnswers));
            qs.Add(makeQuestion(Question.TwoBitsResponse, _TwoBits, new[] { "third" }, new[] { thirdResponse.ToString("00") }, preferredWrongAnswers));
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
            makeQuestion(Question.UncoloredSquaresFirstStage, _UncoloredSquares, new[] { "first" }, new[] { fldFirstStageColor1.Get().ToString() }),
            makeQuestion(Question.UncoloredSquaresFirstStage, _UncoloredSquares, new[] { "second" }, new[] { fldFirstStageColor2.Get().ToString() }));
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
        addQuestions(module, colorsPerStage.Select((col, ix) => makeQuestion(Question.VisualImpairmentColors, _VisualImpairment, new[] { ordinal(ix + 1) }, new[] { colorNames[col] })));
    }

    private IEnumerable<object> ProcessWavetapping(KMBombModule module)
    {
        var comp = GetComponent(module, "scr_wavetapping");
        var fldStageColors = GetField<int[]>(comp, "stageColors");
        var fldIntPatterns = GetField<int[]>(comp, "intPatterns");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        if (comp == null || fldStageColors == null || fldIntPatterns == null || fldSolved == null)
            yield break;

        yield return null;

        var stageColors = fldStageColors.Get();
        var intPatterns = fldIntPatterns.Get();
        if (stageColors.Length != 3 || intPatterns.Length != 3)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning Wavetapping because ‘intPatterns/stageColors’ has unexpected length (expected 3): {1}).", _moduleId, string.Format("[{0}] | [{1}]", intPatterns.JoinString(", "), stageColors.JoinString(", ")));
            yield break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        var patternSprites = new Dictionary<int, Sprite[]>();
        var spriteTake = new[] { 4, 4, 3, 2, 2, 2, 2, 2, 9, 4, 40, 13, 4, 8, 21, 38 };
        var spriteSkip = 0;
        for (int i = 0; i < spriteTake.Length; i++)
        {
            patternSprites.Add(i, WavetappingSprites.Skip(spriteSkip).Take(spriteTake[i]).ToArray());
            spriteSkip += spriteTake[i];
        }

        var colorNames = new[] { "Red", "Orange", "Orange-Yellow", "Chartreuse", "Lime", "Green", "Seafoam Green", "Cyan-Green", "Turquoise", "Dark Blue", "Indigo", "Purple", "Purple-Magenta", "Magenta", "Pink", "Gray" };
        _modulesSolved.IncSafe(_Wavetapping);

        var qs = new List<QandA>();

        for (int stage = 0; stage < intPatterns.Length; stage++)
            qs.Add(makeQuestion(Question.WavetappingPatterns, _Wavetapping,
                formatArgs: new[] { ordinal(stage + 1) },
                correctAnswers: new[] { patternSprites[stageColors[stage]][intPatterns[stage]] },
                preferredWrongAnswers: stageColors.SelectMany(stages => patternSprites[stages]).ToArray()));
        for (int stage = 0; stage < 2; stage++)
            qs.Add(makeQuestion(Question.WavetappingColors, _Wavetapping,
                formatArgs: new[] { ordinal(stage + 1) },
                correctAnswers: new[] { colorNames[stageColors[stage]] }));

        addQuestions(module, qs);
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
            makeQuestion(Question.WireDialColors, _Wire, new[] { "top" }, new[] { dials[0].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDialColors, _Wire, new[] { "bottom-left" }, new[] { dials[1].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDialColors, _Wire, new[] { "bottom-right" }, new[] { dials[2].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDisplayedNumber, _Wire, correctAnswers: new[] { fldDisplayedNumber.Get().ToString() }));
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
        addQuestion(module, Question.YahtzeeInitialRoll, correctAnswers: new[] { result });
    }

    private void addQuestion(KMBombModule module, Question question, string[] formatArguments = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null)
    {
        addQuestions(module, makeQuestion(question, module.ModuleType, formatArguments, correctAnswers, preferredWrongAnswers));
    }

    private void addQuestion(KMBombModule module, Question question, string[] formatArguments = null, Sprite[] correctAnswers = null, Sprite[] preferredWrongAnswers = null)
    {
        addQuestions(module, makeQuestion(question, module.ModuleType, formatArguments, correctAnswers, preferredWrongAnswers));
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

    private QandA makeQuestion(Question question, string moduleKey, string[] formatArgs = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null)
    {
        return makeQuestion(question, moduleKey,
            (attr, q, correct, answers) => new QandAText(attr.ModuleNameWithThe, q, correct, answers.ToArray(), Fonts[(int) attr.Type], FontTextures[(int) attr.Type], FontMaterial),
            formatArgs, correctAnswers, preferredWrongAnswers);
    }

    private QandA makeQuestion(Question question, string moduleKey, string[] formatArgs = null, Sprite[] correctAnswers = null, Sprite[] preferredWrongAnswers = null)
    {
        return makeQuestion(question, moduleKey,
            (attr, q, correct, answers) => new QandASprite(attr.ModuleNameWithThe, q, correct, answers.ToArray()),
            formatArgs, correctAnswers, preferredWrongAnswers);
    }

    private QandA makeQuestion<T>(Question question, string moduleKey, Func<SouvenirQuestionAttribute, string, int, T[], QandA> questionConstructor, string[] formatArgs = null, T[] correctAnswers = null, T[] preferredWrongAnswers = null)
    {
        SouvenirQuestionAttribute attr;
        if (!_attributes.TryGetValue(question, out attr))
        {
            Debug.LogFormat("<Souvenir #{1}> Question {0} has no attribute.", question, _moduleId);
            return null;
        }

        var allAnswers = attr.AllAnswers as T[];
        if (allAnswers != null)
        {
            var inconsistency = correctAnswers.Except(allAnswers).FirstOrDefault();
            if (inconsistency != null)
            {
                Debug.LogFormat("<Souvenir #{2}> Question {0}: invalid answer: {1}.", question, inconsistency.ToString() ?? "<null>", _moduleId);
                return null;
            }
            if (preferredWrongAnswers != null)
            {
                var inconsistency2 = preferredWrongAnswers.Except(allAnswers).FirstOrDefault();
                if (inconsistency2 != null)
                {
                    Debug.LogFormat("<Souvenir #{2}> Question {0}: invalid preferred wrong answer: {1}.", question, inconsistency2.ToString() ?? "<null>", _moduleId);
                    return null;
                }
            }
        }

        List<T> answers;
        if (allAnswers == null && preferredWrongAnswers == null)
        {
            Debug.LogFormat("<Souvenir #{0}> Question {1}: allAnswers and preferredWrongAnswers are both null. You must specify either the full set of possible answers in the Question enum’s attribute, or provide possible wrong answers through the “preferredWrongAnswers” parameter.", _moduleId, question);
            return null;
        }
        else if (allAnswers == null)
            answers = preferredWrongAnswers.Distinct().Except(correctAnswers).ToList().Shuffle().Take(attr.NumAnswers - 1).ToList();
        else
        {
            // Pick 𝑛−1 random wrong answers.
            answers = allAnswers.Except(correctAnswers).ToList().Shuffle().Take(attr.NumAnswers - 1).ToList();
            // Add the preferred wrong answers, if any. If we had added them earlier, they’d come up too rarely.
            if (preferredWrongAnswers != null)
                answers = answers.Concat(preferredWrongAnswers.Distinct().Except(answers).Except(correctAnswers)).ToList().Shuffle().Take(attr.NumAnswers - 1).ToList();
        }

        var correctIndex = Rnd.Range(0, Math.Min(attr.NumAnswers + 1, answers.Count + 1));
        answers.Insert(correctIndex, correctAnswers[Rnd.Range(0, correctAnswers.Length)]);

        var numSolved = _modulesSolved.Get(moduleKey);
        if (numSolved < 1)
        {
            Debug.LogFormat("<Souvenir #{0}> Abandoning {1} ({2}) because you forgot to increment the solve count.", _moduleId, attr.ModuleName, moduleKey);
            return null;
        }

        var allFormatArgs = new string[formatArgs != null ? formatArgs.Length + 1 : 1];
        allFormatArgs[0] = _moduleCounts.Get(moduleKey) > 1
            ? string.Format("the {0} you solved {1}", attr.ModuleName, ordinal(numSolved))
            : attr.AddThe ? "The\u00a0" + attr.ModuleName : attr.ModuleName;
        if (formatArgs != null)
            Array.Copy(formatArgs, 0, allFormatArgs, 1, formatArgs.Length);

        return questionConstructor(attr, string.Format(attr.QuestionText, allFormatArgs), correctIndex, answers.ToArray());
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
    private readonly string TwitchHelpMessage = @"!{0} answer 3 [order is from top to bottom, then left to right]";
#pragma warning restore 414
#pragma warning restore IDE0044

    IEnumerator ProcessTwitchCommand(string command)
    {
        if (Application.isEditor && !TwitchPlaysActive && command == "tp")
        {
            ActivateTwitchPlaysNumbers();
            TwitchPlaysActive = true;
            yield break;
        }

        if (Application.isEditor)
        {
            var questions = Ut.GetEnumValues<Question>();
            var i = 0;
            do
            {
                Answers[1].OnInteract();
                i++;
            }
            while ((_currentQuestion == null || !_currentQuestion.QuestionText.ContainsIgnoreCase(command)) && i < questions.Length);
            yield break;
        }

        var m = Regex.Match(command.ToLowerInvariant(), @"\A\s*answer\s+(\d)\s*\z");
        if (!m.Success || _isSolved)
            yield break;

        int number;
        if (_animating || _currentQuestion == null)
        {
            yield return "sendtochaterror {0}, there is no question active right now on module {1} (Souvenir).";
            yield break;
        }
        if (!int.TryParse(m.Groups[1].Value, out number) || number <= 0 || number > Answers.Length || Answers[number - 1] == null || !Answers[number - 1].gameObject.activeSelf)
        {
            yield return string.Format("sendtochaterror {{0}}, that’s not a valid answer; give me a number from 1 to {0}.", Answers.Count(a => a != null && a.gameObject.activeSelf));
            yield break;
        }

        yield return null;
        if (_currentQuestion.CorrectIndex == number - 1)
            yield return "awardpoints 1";
        yield return new[] { Answers[number - 1] };
    }
}

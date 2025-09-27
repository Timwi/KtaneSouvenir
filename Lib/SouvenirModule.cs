using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Souvenir;
using Souvenir.Reflection;
using UnityEngine;
using Rnd = UnityEngine.Random;

/// <summary>
/// On the Subject of Souvenir
/// Created by Timwi
/// </summary>
public partial class SouvenirModule : MonoBehaviour
{
    private const string _version = "8.2";

    #region Fields
    public KMBombInfo Bomb;
    public KMBombModule Module;
    public KMAudio Audio;
    public KMBossModule BossModule;
    public KMSelectable RootSelectable;
    public KMSelectable[] Answers;
    public GameObject AnswersParent;
    public GameObject[] TpNumbers;
    public Sprite[] AlcoholicRampageSprites;
    public Sprite[] ArithmelogicSprites;
    public Sprite[] AudioSprites;
    public Sprite[] AzureButtonSprites;
    public Sprite[] BookOfMarioSprites;
    public Sprite[] CharacterSlotsSprites;
    public Sprite[] ClockCounterSprites;
    public Sprite[] ConnectedMonitorsSprites;
    public Sprite[] CycleModuleThreeSprites;
    public Sprite[] CycleModuleFiveSprites;
    public Sprite[] CycleModuleEightSprites;
    public Sprite[] CycleModuleTwelveSprites;
    public Sprite[] CycleModuleCrypticSprites;
    public Sprite[] EncryptedEquationsSprites;
    public Sprite[] ExampleSprites;
    public Sprite[] FlagsSprites;
    public Sprite[] FuseBoxColorSprites;
    public Sprite[] FuseBoxArrowSprites;
    public Sprite[] GadgetronVendorIconSprites;
    public Sprite[] GadgetronVendorWeaponSprites;
    public Sprite[] HingesSprites;
    public Sprite[] JengaSprites;
    public Sprite[] KeypadSprites;
    public Sprite[] LadderLotterySprites;
    public Sprite[] MahjongSprites;
    public Sprite[] MaroonButtonSprites;
    public Sprite[] MathEmSprites;
    public Sprite[] MemorySprites;
    public Sprite[] MisterSofteeSprites;
    public Sprite[] NonverbalSimonSprites;
    public Sprite[] OffKeysSprites;
    public Sprite[] OrderedKeysSprites;
    public Sprite[] PatternCubeSprites;
    public Sprite[] PlanetsSprites;
    public Sprite[] PolygonsSprites;
    public Sprite[] QuestionMarkSprites;
    public Sprite[] QwirkleSprites;
    public Sprite[] SimonShapesSprites;
    public Sprite[] SimonSignalsSprites;
    public Sprite[] SimonSpeaksSprites;
    public Sprite[] SonicKnucklesBadniksSprites;
    public Sprite[] SonicKnucklesMonitorsSprites;
    public Sprite[] SonicTheHedgehogSprites;
    public Sprite[] SymbolicCoordinatesSprites;
    public Sprite[] SymbolicTashaSprites;
    public Sprite[] TasqueManagingSprites;
    public Sprite[] TeaSetSprites;
    public Sprite[] USACycleSprites;
    public Sprite[] ValvesSprites;
    public Sprite[] WarningSignsSprites;
    public Sprite[] WavetappingSprites;
    public Sprite[] XRingSprites;
    public Sprite[] XYRaySprites;
    public Texture2D[] DigitTextures;
    public Texture2D[] FuseBoxQuestions;
    public Texture2D[] NonverbalSimonQuestions;
    public Texture2D[] TechnicalKeypadQuestions;

    public AudioClip[] AudioMorseAudio;
    public AudioClip[] DialtonesAudio;
    public AudioClip[] ExampleAudio;
    public AudioClip[] FaerieFiresAudio;
    public AudioClip[] ListeningAudio;
    public AudioClip[] SimonSamplesAudio;
    public AudioClip[] SimonSmilesAudio;
    public AudioClip[] SonicTheHedgehogAudio;

    private readonly List<UnityEngine.Object> _unityObjectsToDestroyLater = [];

    public TextMesh TextMesh;
    public Renderer TextRenderer;
    public Renderer SurfaceRenderer;
    public SpriteRenderer QuestionSprite;
    public SpriteRenderer EntireQuestionSprite;
    public GameObject WarningIcon;
    public Material FontMaterial;
    public Material ColorBlitMaterial;
    public Font[] Fonts;
    public Texture[] FontTextures;
    public Mesh HighlightShort; // 6 answers, 3 columns
    public Mesh HighlightLong;  // 4 answers, 2 columns
    public Mesh HighlightVeryLong;  // 4 long answers, 1 column

    private static readonly string[] _defaultIgnoredModuleIDs = ["MemoryV2", "SouvenirModule", "HexiEvilFMN", "simonsStages", "forgetThis", "PurgatoryModule", "troll", "forgetThemAll", "tallorderedKeys", "forgetEnigma", "forgetUsNot", "qkForgetPerspective", "organizationModule", "veryAnnoyingButton", "forgetMeLater", "ubermodule", "qkUCN", "14", "forgetItNot", "simonForgets", "brainf", "ForgetTheColors", "RPSJudging", "TheTwinModule", "iconic", "omegaForget", "kugelblitz", "ANDmodule", "dontTouchAnything", "busyBeaver", "whiteout", "ForgetAnyColor", "KeypadDirectionality", "SecurityCouncil", "ShoddyChessModule", "FloorLights", "blackArrowsModule", "forgetMazeNot", "plus", "soulscream", "qkCubeSynchronization", "OutOfTime", "tetrahedron", "BoardWalk", "gemory", "duckKonundrum", "ConcentrationModule", "TwisterModule", "forgetOurVoices", "soulsong", "idExchange", "GSEight", "SimpleBoss", "SimpleBossNot", "KritGrandPrix", "ForgetMeMaybeModule", "HyperForget", "qkBitwiseOblivion", "damoclesLumber", "top10nums", "queensWarModule", "forget_fractal", "pointerPointerModule", "slightGibberishTwistModule", "PianoParadoxModule", "Omission", "inOrderModule", "nobodysCodeModule", "perspectiveStackingModule", "ReportingAnomalies", "forgetle", "ActionsAndConsequences", "fizzBoss", "WatchTheClock", "solveShift", "BlackoutModule", "hickoryDickoryDockModule", "temporalSequence", "sbemailsongs", "spectatorSport", "limboKeysRB", "clearanceCodeModule", "smashmarrykill", "forgetfulGrid", "GSMarmite", "WAR", "NumericalNightmare", "ForgetMeNoModule", "TurnTheKey", "timeKeeper", "timingIsEverything", "bamboozlingTimeKeeper", "pwDestroyer", "omegaDestroyer", "kataZenerCards", "doomsdayButton", "redLightGreenLight", "repeatAgain", "Crazy", "FalseInfo", "minskMetro"];

    private Config _config;
    private readonly List<QandA> _questions = [];
    private readonly HashSet<KMBombModule> _legitimatelyNoQuestions = [];
    private readonly HashSet<string> _supportedModuleNames = [];
    private readonly HashSet<string> _ignoredModuleIDs = [];
    private bool _isActivated = false;
    private ITranslation _translation;

    private QandA _currentQuestion = null;
    private bool _isSolved = false;
    private bool _animating = false;
    private bool _exploded = false;
    private bool _noUnignoredModulesLeft = false;
    private int _avoidQuestions = 0;   // While this is > 0, temporarily avoid asking questions; currently only used when Souvenir is hidden by a Mystery Module
    private bool _showWarning = false;
    private List<string> _activeProcessors = [];

    [NonSerialized]
    public double SurfaceSizeFactor;

    private readonly Dictionary<string, ModuleTypeInfo> _moduleTypeInfo = [];
    private int _coroutinesActive;

    private static int _moduleIdCounter = 1;
    internal int _moduleId;

    // Used in TestHarness only
    private SouvenirHandlerAttribute[] _exampleModules;
    private int _curExampleModule = 0;
    private SouvenirQuestionAttribute[] _exampleQuestions;
    private int _curExampleQuestion = 0;
    private string[][] _exampleQuestionArguments;
    private int _curExampleQuestionArgument = 0;
    private SouvenirDiscriminatorAttribute[] _exampleDiscriminators;
    private int _curExampleDiscriminator = 0;   // 0 = no discriminator; 1 = solve count (if not boss); 2 onwards = custom
    private string[][] _exampleDiscriminatorArguments;
    private int _curExampleDiscriminatorArgument = 0;
    private bool _showIntros = false;
    private int _curIntro = 0;

#pragma warning disable 649
    private Action<double> TimeModeAwardPoints;
#pragma warning restore 649

    private static readonly string[] _intros = Ut.NewArray(
        "I see dead defusers.",     // “I see dead people.” (Sixth Sense)
        "Welcome... to the real bomb.",     // “Welcome... to the real world.” (The Matrix)
        "I’m gonna make him a bomb he can’t defuse.",   // “I’m gonna make him an offer he can’t refuse.” (The Godfather)
        "Defuse it again, Sam.",    // “Play it again, Sam.” (Casablanca) (misquote)
        "Louis, I think this is the beginning of a beautiful explosion.",   // “Louis, I think this is the beginning of a beautiful friendship.” (Casablanca)
        "Here’s looking at you, defuser.",  // “Here’s looking at you, kid.” (Casablanca)
        "Hey. I could defuse this bomb in ten seconds flat.",   // “Hey. I could clear the sky in ten seconds flat.” (MLP:FiM, Friendship is Magic - Part 1)
        "Go ahead, solve my bomb.", // “Go ahead, make my day.” (Sudden Impact / Dirty Harry series)
        "May the bomb be with you.",    // “May the Force be with you.” (Star Wars IV: A New Hope)
        "I love the smell of explosions in the morning.",   // “I love the smell of napalm in the morning.” (Apocalypse Now)
        "Blowing up means never having to say you’re sorry.",   // “Love means never having to say you’re sorry.” (Love Story)
        "The stuff that bombs are made of.",    // “The Stuff That Dreams Are Made Of” (“Coming Around Again” album by Carly Simon)
        "E.T. defuse bomb.",    // “E.T. phone home.” (E.T. the Extra-Terrestrial)
        "Bomb. James Bomb.",    // “Bond. James Bond.” (Dr. No / James Bond series)
        "You can’t handle the bomb!",   // “You can’t handle the truth!” (A Few Good Men)
        "Blow up the usual suspects.",  // “Round up the usual suspects.” (Casablanca)
        "You’re gonna need a bigger bomb.", // “You’re gonna need a bigger boat.” (Jaws)
        "Bombs are like a box of chocolates. You never know what you’re gonna get.",    // “My mom always said life was like a box of chocolates. You never know what you’re gonna get.” (Forrest Gump)
        "Houston, we have a module.",   // “Houston, we have a problem.” (Apollo 13)
        "Elementary, my dear expert.",  // “Elementary, my dear Watson.” (Sherlock Holmes) (misquote)
        "Forget it, Jake, it’s KTANE.",     // “Forget it, Jake, it’s Chinatown.” (Chinatown)
        "I have always depended on the fitness of experts.",    // “I’ve always depended on the kindness of strangers.” (A Streetcar Named Desire)
        "A bomb. Exploded, not defused.",   // “A martini. Shaken, not stirred.” (Diamonds Are Forever (novel) / James Bond)
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
        "Don’t cross the wires.",   // “Don’t cross the streams.” (Ghostbusters)
        "Wanna hear the most annoying explosion in the world?", // “Wanna hear the most annoying sound in the world?” (Dumb & Dumber)
        "Manuals? Where we’re going, we don’t need manuals.",   // “Roads? Where we’re going, we don’t need roads.” (Back to the Future)
        "On a long enough time line, the survival rate for everyone will drop to zero.", // direct quote (Fight Club (novel))
        "This is your bomb, and it’s ending one minute at a time.", // “This is your life and it’s ending one minute at a time.” (Fight Club)
        "The first rule of defusal is, you keep talking about defusal.",    // “The first rule of Fight Club is, you don’t talk about Fight Club.” (Fight Club)
        "Well, here’s another nice mess you’ve gotten me into!",     // direct quote (Sons of the Desert / Oliver Hardy)
        "You know how to defuse, don’t you, Steve? You just put your wires together and cut.",  // “You know how to whistle, don’t you Steve? You just put your lips together, and blow.” (To Have And Have Not)
        "Mrs. Defuser, you’re trying to disarm me. Aren’t you?",    // “Mrs. Robinson, you’re trying to seduce me. Aren’t you?” (The Graduate)
        "We defuse bombs.",  // “We rob banks.” (Bonnie and Clyde)
        "Somebody set up us the bomb.",  // direct quote (Zero Wing)
        "Luke, I am your expert.", // “Luke, I am your father.” (Star Wars V: The Empire Strikes Back) (misquote)
        "Everybody knows that the best way to learn is under intense life threatening pressure.", // direct quote (Spider-Man: Into the Spider-Verse)
        "It needs to be about 20 percent more exploded.", // “It needs to be about 20 percent cooler.” (MLP:FiM, Suited for Success)
        "I am a bomb. What’s your excuse?", // “I am a child. What’s your excuse?” (Steven Universe, Change your Mind)
        "The same thing we do every night, expert. Try to defuse the bomb!", // “The same thing we do every night, Pinky. Try to take over the world!” (Pinky and the Brain)
        "Anybody order fried defuser?", // “Anybody order fried sauerkraut?” (Once Upon a Time in Hollywood)
        "I’ve got some defusers I need to blow into smithereens!", // “I’ve got some children I need to make into corpses!” (Gravity Falls, Weirdmageddon 3: Take Back The Falls)
        "I imagine blowing up so much it feels more like a memory.", // “I imagine death so much it feels more like a memory.” (Hamilton)
        "I am inevitable.", // direct quote (Avengers: Endgame)
        "Dread it, run from it, bombs still explode.", // “Dread it, run from it, destiny still arrives.” (Avengers: Infinity War)
        "In time, you will know what it’s like to lose. To feel so desperately that you’re right, yet to strike all the same.", // “In time, you will know what it’s like to lose. To feel so desperately that you’re right, yet to fail all the same.” (Avengers: Infinity War)
        "Defuser, I’ve come to bargain.", // “Dormammu, I’ve come to bargain.” (Doctor Strange)
        "I can do this all day.", // direct quote (Captain America: Civil War)
        "There... are... FOUR! BOMBS!!!", // “There... are... FOUR! LIGHTS!!!” (Star Trek TNG, Chain of Command)
        "It’s a beautiful thing, the detonation of bombs.", // “It’s a beautiful thing, the destruction of words.” (1984)
        "Ich bin ein Defuser.", // “Ich bin ein Berliner”, John F. Kennedy, 1963
        "Ask not the double decker how the Centurion solves!", // Ask not the sparrow how the eagle soars! (Kill la Kill)
        "Someone thinks they’re too clever for me. They all think that at first." // Someone thinks they’re too clever for us. They all think that at first. (Invincible)
    );

    /// <summary><code>yield return</code> this to wait for the module to be activated.</summary>
    private static SouvenirInstruction WaitForActivate => WaitForActivateInstruction.Instance;
    /// <summary><code>yield return</code> this to wait for the module to be solved.</summary>
    private static SouvenirInstruction WaitForSolve => WaitForSolveInstruction.Instance;
    /// <summary><code>yield return</code> this to wait for all unignored (that is, non-boss) modules to finish.</summary>
    private static SouvenirInstruction WaitForUnignoredModules => WaitForUnignoredModulesInstruction.Instance;

    #endregion

    #region Souvenir’s own module logic
    private void Start()
    {
        _moduleId = _moduleIdCounter;
        _moduleIdCounter++;

        Debug.Log($"[Souvenir #{_moduleId}] Souvenir version: {_version}");

        // Use Souvenir-settings.txt as opposed to SouvenirSettings.json for existing settings
        var modConfig = new ModSettings<Config>("Souvenir-settings");
        _config = modConfig.Read();
        if (_config.Language is null)
        {
            _config.Language = "en";
            modConfig.Write(_config);
        }

        _ignoredModuleIDs.UnionWith(BossModule.GetIgnoredModuleIDs(Module, _defaultIgnoredModuleIDs));

        if (_config.Language != null && TranslationInfo.AllTranslations.TryGetValue(_config.Language, out var tr))
            _translation = tr;
        Debug.Log($"<Souvenir #{_moduleId}> Language: {_config.Language} ({(_translation == null ? "absent" : "present")})");

        Bomb.OnBombExploded += delegate
        {
            _exploded = true;
            StopAllCoroutines();
            if (!_isSolved)
            {
                if (_questions.Count == 0)
                    Debug.Log($"[Souvenir #{_moduleId}] When bomb exploded, there were no pending questions.");
                else if (_questions.Count == 1)
                    Debug.Log($"[Souvenir #{_moduleId}] When bomb exploded, 1 question was pending for: {_questions.Select(q => q.ModuleName).OrderBy(q => q).JoinString(", ")}.");
                else
                    Debug.Log($"[Souvenir #{_moduleId}] When bomb exploded, {_questions.Count} questions were pending for: {_questions.Select(q => q.ModuleName).OrderBy(q => q).JoinString(", ")}.");
            }
        };
        Bomb.OnBombSolved += delegate
        {
            // This delegate gets invoked when _any_ bomb in the room is solved,
            // so we need to check if the bomb this module is on is actually solved
            if (Bomb.GetSolvedModuleNames().Count == Bomb.GetSolvableModuleNames().Count)
                StopAllCoroutines();
        };

        var origRotation = SurfaceRenderer.transform.rotation;
        SurfaceRenderer.transform.eulerAngles = new Vector3(0, 180, 0);
        SurfaceSizeFactor = SurfaceRenderer.bounds.size.x / (2 * .834) * .9;
        SurfaceRenderer.transform.rotation = origRotation;

        disappear();
        WarningIcon.SetActive(false);

        SetWordWrappedText((_translation?.IntroTexts ?? _intros).PickRandom(), 1.75, useQuestionSprite: false);

        if (transform.parent != null && !Application.isEditor)
        {
            FieldInfo<object> fldType = null;
            for (var i = 0; i < transform.parent.childCount; i++)
            {
                var gameObject = transform.parent.GetChild(i).gameObject;
                if (gameObject.GetComponent<KMBombModule>() is KMBombModule moddedModule)
                    StartCoroutine(ProcessModule(moddedModule));
                else if (!_config.ExcludeVanillaModules && transform.parent.GetChild(i).gameObject.GetComponent("BombComponent") is Component vanillaModule)
                {
                    // For vanilla modules, we will attach a temporary KMBombModule component to the module.
                    // We’ll remove it after the coroutine starts.
                    // The routine will already have a reference to the actual BombComponent by then.
                    fldType ??= GetField<object>(vanillaModule, "ComponentType", isPublic: true);
                    if (fldType == null) continue;
                    var typeCode = (int) fldType.GetFrom(vanillaModule);
                    string type, displayName;
                    switch (typeCode)
                    {
                        case 3: type = "BigButton"; displayName = "The Button"; break;
                        case 5: type = "Simon"; displayName = "Simon Says"; break;
                        case 6: type = "WhosOnFirst"; displayName = "Who’s on First"; break;
                        case 7: type = "Memory"; displayName = "Memory"; break;
                        case 10: type = "WireSequence"; displayName = "Wire Sequence"; break;
                        case 11: type = "Maze"; displayName = "Maze"; break;
                        default: continue;  // Other components are not supported modules.
                    }
                    var kmModule = gameObject.AddComponent<KMBombModule>();
                    kmModule.ModuleType = type;
                    kmModule.ModuleDisplayName = displayName;
                    StartCoroutine(ProcessModule(kmModule));
                }
            }
        }

        _isActivated = false;
        Module.OnActivate += delegate
        {
            _isActivated = true;
            if (Application.isEditor)
            {
                // Testing in Unity
                foreach (var entry in Ut.Attributes)
                {
                    var (q, (hAttr, qAttr, dAttr)) = (entry.Key, entry.Value);
                    var qs = $"{q.GetType().Name}.{q}";

                    if (qAttr != null)
                    {
                        if (qAttr.TranslateArguments != null && qAttr.TranslateArguments.Length != qAttr.ArgumentGroupSize)
                            Debug.LogError($"<Souvenir #{_moduleId}> Question {qs}: The length of the ‘{nameof(qAttr.TranslateArguments)}’ array must match ‘{nameof(qAttr.ArgumentGroupSize)}’.");
                        if (qAttr.SpriteFieldName != null && qAttr.Type != AnswerType.Sprites)
                            Debug.LogError($"<Souvenir #{_moduleId}> Question {qs} (type {qAttr.Type}) specifies a SpriteField. This should only be used for questions of type Sprites.");
                        if (qAttr.AllAnswers != null && qAttr.AnswerGenerators != null)
                            Debug.LogError($"<Souvenir #{_moduleId}> Question {qs} (type {qAttr.Type}) specifies both AllAnswers and an answer generator. When using an answer generator, please set AllAnswers explicitly to null.");
                        if (qAttr.AnswerGenerators is { Length: > 1 } && qAttr.AnswerGenerators.Select(g => g.ElementType).Distinct().Count() is not 1)
                            Debug.LogError($"<Souvenir #{_moduleId}> Question {qs} (type {qAttr.Type}) uses multiple answer generators, but they generate different types of answers. Ensure all answer generators are appropriate for the question.");

                        switch (qAttr.Type)
                        {
                            case AnswerType.Sprites:
                                if (qAttr.AnswerGenerators != null && qAttr.AnswerGenerators.Any(g => g is not AnswerGeneratorAttribute<Sprite>))
                                    Debug.LogError($"<Souvenir #{_moduleId}> Question {qs} (type {qAttr.Type}) uses an answer generator for something other than sprites. Change the answer type or specify a sprite answer generator (e.g. [AnswerGenerator.Grid(4, 4)]).");
                                break;

                            case AnswerType.Audio:
                                if (qAttr.AnswerGenerators != null)
                                    Debug.LogError($"<Souvenir #{_moduleId}> Question {qs} (type {qAttr.Type}) uses an answer generator. This is not supported for audio questions.");
                                break;

                            default:
                                if ((qAttr.AllAnswers == null || qAttr.AllAnswers.Length == 0) && (qAttr.ExampleAnswers == null || qAttr.ExampleAnswers.Length == 0) && (qAttr.AnswerGenerators is null || qAttr.AnswerGenerators.All(g => g is not AnswerGeneratorAttribute<string>)))
                                    Debug.LogError($"<Souvenir #{_moduleId}> Question {qs} has no answers. Specify either AllAnswers (if it’s a reasonable-sized, fixed finite set), or ExampleAnswers (if it’s generated at runtime and passed to allAnswers in makeQuestion()), or add an answer generator (e.g. [AnswerGenerator.Integers(0, 9999)]).");
                                break;
                        }
                    }
                }

                Debug.LogFormat(this, "<Souvenir #{0}> Entering Unity testing mode.", _moduleId);
                _exampleModules = Ut.ModuleHandlers.Values.ToArray();
                setExampleModule(0);

                setAnswerHandler(0, _ => { _showIntros = false; setExampleModule(_curExampleModule + 1); });
                setAnswerHandler(1, _ => { _showIntros = true; showIntro(_curIntro + 1); });
                setAnswerHandler(2, _ => { _showIntros = false; setExampleQuestion(_curExampleQuestion + 1); });
                setAnswerHandler(3, _ => { _showIntros = false; _curExampleQuestionArgument = (_curExampleQuestionArgument + 1) % (_exampleQuestionArguments?.Length ?? 1); showExampleQuestion(); });
                setAnswerHandler(4, _ => { _showIntros = false; setExampleDiscriminator(_curExampleDiscriminator + 1); });
                setAnswerHandler(5, _ => { _showIntros = false; _curExampleDiscriminatorArgument = (_curExampleDiscriminatorArgument + 1) % (_exampleDiscriminatorArguments?.Length ?? 1); showExampleQuestion(); });
            }
            else
            {
                // Playing for real
                for (var i = 0; i < 6; i++)
                    setAnswerHandler(i, HandleAnswer);
                disappear();
                StartCoroutine(Play());
            }
        };

        Sprites.ColorBlit ??= ColorBlitMaterial;
    }

    private void showIntro(int index)
    {
        var intros = _translation?.IntroTexts ?? _intros;
        _curIntro = (index % intros.Length + intros.Length) % intros.Length;
        disappear();
        SetWordWrappedText(intros[_curIntro], 1.75, useQuestionSprite: false);
        foreach (var ans in Answers)
            ans.transform.Find("AnswerText").GetComponent<TextMesh>().text = "";
        AnswersParent.SetActive(true);
    }

    private void setExampleModule(int moduleIndex)
    {
        _curExampleModule = (moduleIndex % _exampleModules.Length + _exampleModules.Length) % _exampleModules.Length;

        var enumType = _exampleModules[_curExampleModule].EnumType;
        _exampleDiscriminators = (_exampleModules[_curExampleModule].IsBossModule ? new SouvenirDiscriminatorAttribute[] { null } : [null, null])
            .Concat(Ut.Attributes.Where(kvp => kvp.Key.GetType() == enumType && kvp.Value.d != null).Select(kvp => kvp.Value.d)).ToArray();
        _curExampleDiscriminator = 0;
        _exampleQuestions = Ut.Attributes.Where(kvp => kvp.Key.GetType() == enumType && kvp.Value.q != null).Select(kvp => kvp.Value.q).ToArray();
        setExampleQuestion(0);
    }

    private void setExampleQuestion(int questionIndex)
    {
        _curExampleQuestion = (questionIndex % _exampleQuestions.Length + _exampleQuestions.Length) % _exampleQuestions.Length;
        var groupSize = _exampleQuestions[_curExampleQuestion].ArgumentGroupSize;
        _exampleQuestionArguments = groupSize == 0 ? null :
            Enumerable.Range(0, _exampleQuestions[_curExampleQuestion].Arguments.Length / groupSize)
                .Select(ix => _exampleQuestions[_curExampleQuestion].Arguments.Skip(ix * groupSize).Take(groupSize).ToArray())
                .ToArray();
        _curExampleQuestionArgument = 0;
        showExampleQuestion();
    }

    private void setExampleDiscriminator(int discriminatorIndex)
    {
        _curExampleDiscriminator = (discriminatorIndex % _exampleDiscriminators.Length + _exampleDiscriminators.Length) % _exampleDiscriminators.Length;

        if (_curExampleDiscriminator == 0)
        {
            // No discriminator
            _exampleDiscriminatorArguments = null;
            _curExampleDiscriminatorArgument = 0;
        }
        else if (_curExampleDiscriminator == 1 && !_exampleModules[_curExampleModule].IsBossModule)
        {
            // Non-boss modules: solve-count discriminator
            _exampleDiscriminatorArguments = [[QandA.Ordinal]];
            _curExampleDiscriminatorArgument = 0;
        }
        else
        {
            // Custom discriminators
            var groupSize = _exampleDiscriminators[_curExampleDiscriminator].ArgumentGroupSize;
            _exampleDiscriminatorArguments = groupSize == 0 ? null :
                Enumerable.Range(0, _exampleDiscriminators[_curExampleDiscriminator].Arguments.Length / groupSize)
                    .Select(ix => _exampleDiscriminators[_curExampleDiscriminator].Arguments.Skip(ix * groupSize).Take(groupSize).ToArray())
                    .ToArray();
            _curExampleDiscriminatorArgument = 0;
        }
        showExampleQuestion();
    }

    private void showExampleQuestion()
    {
        var hAttr = _exampleModules[_curExampleModule];
        var qAttr = _exampleQuestions[_curExampleQuestion];

        var fmt = new object[qAttr.ArgumentGroupSize + 1];
        if (_curExampleDiscriminator == 0)
        {
            // No discriminator
            fmt[0] = formatModuleName(hAttr, false, 1);
        }
        else if (_curExampleDiscriminator == 1 && !hAttr.IsBossModule)
        {
            // Non-boss modules: solve-count discriminator
            fmt[0] = formatModuleName(hAttr, true, Rnd.Range(1, 11));
        }
        else
        {
            // Custom discriminator
            var dAttr = _exampleDiscriminators[_curExampleDiscriminator];
            var dGs = dAttr.ArgumentGroupSize;
            var dFmt = dGs == 0 ? [] : _exampleDiscriminatorArguments[_curExampleDiscriminatorArgument]
                .Select<string, object>((arg, ix) => Snip(dAttr.TranslateArguments != null && dAttr.TranslateArguments[ix] ? TranslateDiscriminatorArgument(dAttr.EnumValue, arg) : arg))
                .ToArray();
            fmt[0] = string.Format(TranslateDiscriminator(dAttr.EnumValue, dAttr.DiscriminatorText), dFmt);
        }

        if (_exampleQuestionArguments != null && _exampleQuestionArguments[_curExampleQuestionArgument] is { } args)
            for (var i = 0; i < args.Length; i++)
                fmt[i + 1] = args[i] == QandA.Ordinal ? Ordinal(Rnd.Range(1, 11)) : Snip(TranslateQuestionArgument(qAttr.EnumValue, args[i]));

        var questionText = qAttr.Gimmicks.Aggregate(string.Format(TranslateQuestion(qAttr.EnumValue), fmt), (prev, gimmick) => gimmick.ApplyGimmick(prev, fmt));

        QuestionBase question = qAttr.IsEntireQuestionSprite
            ? new SpriteQuestion(questionText, WavetappingSprites[0])
            : new TextQuestion(questionText, qAttr.Layout, qAttr.UsesQuestionSprite ? SymbolicCoordinatesSprites[0] : null, 0);

        AnswerSet answerSet;
        switch (qAttr.Type)
        {
            case AnswerType.Audio:
                var audioClips = qAttr.AudioFieldName == null ? ExampleAudio : (AudioClip[]) typeof(SouvenirModule).GetField(qAttr.AudioFieldName, BindingFlags.Instance | BindingFlags.Public).GetValue(this) ?? ExampleAudio;
                audioClips = audioClips?.Shuffle().Take(qAttr.NumAnswers).ToArray();
                answerSet = new AudioAnswerSet(qAttr, audioClips, 0, this);
                break;
            case AnswerType.Sprites:
                var answerSprites = qAttr.SpriteFieldName == null ? ExampleSprites : (Sprite[]) typeof(SouvenirModule).GetField(qAttr.SpriteFieldName, BindingFlags.Instance | BindingFlags.Public).GetValue(this) ?? ExampleSprites;
                answerSprites = answerSprites?.Shuffle().Take(qAttr.NumAnswers).ToArray();
                if (qAttr.AnswerGenerators?.FirstOrDefault() is AnswerGeneratorAttribute<Sprite>)
                    answerSprites = qAttr.AnswerGenerators.Cast<AnswerGeneratorAttribute<Sprite>>().GetAnswers(this).Distinct().Take(qAttr.NumAnswers).ToArray();
                answerSet = new SpriteAnswerSet(qAttr.Layout, answerSprites, 0);
                break;
            default:
                var answers = new List<string>(qAttr.NumAnswers);
                if (qAttr.AllAnswers != null) answers.AddRange(qAttr.AllAnswers);
                else if (qAttr.ExampleAnswers != null) answers.AddRange(qAttr.ExampleAnswers);
                if (answers.Count <= qAttr.NumAnswers)
                {
                    if (qAttr.AnswerGenerators?.FirstOrDefault() is AnswerGeneratorAttribute<string>)
                        answers.AddRange(qAttr.AnswerGenerators.Cast<AnswerGeneratorAttribute<string>>().GetAnswers(this).Except(answers).Distinct().Take(qAttr.NumAnswers - answers.Count));
                    answers.Shuffle();
                }
                else
                {
                    answers.Shuffle();
                    answers.RemoveRange(qAttr.NumAnswers, answers.Count - qAttr.NumAnswers);
                }
                answerSet = new TextAnswerSet(answers.Select(ans => qAttr.TranslateAnswers ? TranslateAnswer(qAttr.EnumValue, ans) : ans).ToArray(), 0, qAttr, new());
                break;
        }
        disappear();
        SetQuestion(new QandA(qAttr.EnumValue, question, answerSet));
    }

    public string TranslateQuestion(Enum enumValue) => _translation?.TranslateQuestion(enumValue)?.Question ?? enumValue.GetQuestionAttribute().QuestionText;
    public string TranslateQuestionArgument(Enum enumValue, string arg) => arg == null ? null : _translation?.TranslateQuestion(enumValue)?.Arguments?.Get(arg, arg) ?? arg;
    public string TranslateQuestionString(Enum enumValue, string arg) => arg == null ? null : _translation?.TranslateQuestion(enumValue)?.Additional?.Get(arg, arg) ?? arg;
    public string TranslateAnswer(Enum enumValue, string answ) => answ == null ? null : _translation?.TranslateQuestion(enumValue)?.Answers?.Get(answ, answ) ?? answ;
    public string TranslateDiscriminator(Enum enumValue, string dcr) => dcr == null ? null : _translation?.TranslateDiscriminator(enumValue)?.Discriminator ?? dcr;
    public string TranslateDiscriminatorArgument(Enum enumValue, string arg) => arg == null ? null : _translation?.TranslateDiscriminator(enumValue)?.Arguments?.Get(arg, arg) ?? arg;
    public string TranslateModuleName(Type enumType, string name = null) => _translation?.TranslateModule(enumType)?.ModuleName ?? name ?? enumType.GetHandlerAttribute().ModuleNameWithThe;
    public static string Snip(string str) => str.IndexOf('\uE003') is int p and not -1 ? str.Substring(0, p) : str;

    private void setAnswerHandler(int index, Action<int> handler) => Answers[index].OnInteract = delegate
    {
        Answers[index].AddInteractionPunch();
        if (!_currentQuestion.OnPress(index))
            handler(index);
        return false;
    };

    private void disappear()
    {
        TextMesh.gameObject.SetActive(false);
        QuestionSprite.gameObject.SetActive(false);
        EntireQuestionSprite.gameObject.SetActive(false);
        AnswersParent.SetActive(false);
    }

    private void HandleAnswer(int index)
    {
        if (_animating || _isSolved)
            return;

        if (_currentQuestion == null || index >= _currentQuestion.NumAnswers)
            return;

        Debug.Log($"[Souvenir #{_moduleId}] Clicked answer #{index + 1} ({_currentQuestion.DebugAnswers.Skip(index).First()}). {(_currentQuestion.Answers.CorrectIndex == index ? "Correct" : "Wrong")}.");

        if (_currentQuestion.Answers.CorrectIndex == index)
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
        TimeModeAwardPoints?.Invoke(1);
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
        for (var i = 0; i < 14; i++)
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
        if (!Application.isEditor && TwitchPlaysActive)
            ActivateTwitchPlaysNumbers();

        var numPlayableModules = Bomb.GetSolvableModuleIDs().Count(x => !_ignoredModuleIDs.Contains(x));

        while (true)
        {
            // A module handler can increment this value temporarily to delay asking questions. (Currently only the Mystery Module handler does this when Souvenir is hidden by a Mystery Module.)
            while (_avoidQuestions > 0)
                yield return new WaitForSeconds(.1f);

            var numSolved = Bomb.GetSolvedModuleIDs().Count(x => !_ignoredModuleIDs.Contains(x));
            _noUnignoredModulesLeft = numSolved >= numPlayableModules;

            if (_questions.Count == 0 && (_noUnignoredModulesLeft || _coroutinesActive == 0))
            {
                // Very rare case: another coroutine could still be waiting to detect that a module is solved and then add another question to the queue
                yield return new WaitForSeconds(.1f);

                // If still no new questions, all supported modules are solved and we’re done. (Or maybe a coroutine is stuck in a loop, but then it’s bugged and we need to cancel it anyway.)
                if (_questions.Count == 0)
                    break;
            }

            IEnumerable<QandA> eligible = _questions;

            // If we reached the end of the bomb, everything is eligible.
            if (!_noUnignoredModulesLeft)
                // Otherwise, make sure there has been another solved module since
                eligible = eligible.Where(e => e.GeneratedAtNumSolved < numSolved);

            var numEligibles = eligible.Count();

            if ((!_noUnignoredModulesLeft && numEligibles < 3) || numEligibles == 0)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }

            var selectedQuestion = eligible.PickRandom();
            _questions.Remove(selectedQuestion);

            SetQuestion(selectedQuestion);
            while (_currentQuestion != null || _animating)
                yield return new WaitForSeconds(.5f);
        }

        Debug.Log($"[Souvenir #{_moduleId}] Questions exhausted. Module solved.");
        _isSolved = true;
        Module.HandlePass();
        WarningIcon.SetActive(_showWarning);
        if (_activeProcessors.Count != 0)
            Debug.LogWarning($"[Souvenir #{_moduleId}] When the module solved, {_activeProcessors.Count} modules were still being processed. This may be a bug. The processors: {_activeProcessors.JoinString(", ")}");
        else
            Debug.Log($"<Souvenir #{_moduleId}> When the module solved, no modules were still being processed.");
    }

    private void SetQuestion(QandA q)
    {
        _currentQuestion = q;
        q.SetQandAs(this);
        Debug.Log($"[Souvenir #{_moduleId}] Asking question: {q.DebugString}");
        AnswersParent.SetActive(true);
        Audio.PlaySoundAtTransform("Question", transform);
    }

    private static readonly double[][] _acceptableWidthsWithoutQuestionSprite = Ut.NewArray<double[]>(
        // First value is y (vertical text advancement), second value is width of the Surface mesh at this y
        [0, 1.1896],
        [0.0712, 1.258],
        [0.1476, 1.258],
        [0.306, 1.3442],
        [0.3888, 1.4958],
        [0.443, 1.668]);

    public void SetWordWrappedText(string text, double desiredHeightFactor, bool useQuestionSprite)
    {
        TextMesh.gameObject.SetActive(true);
        TextMesh.font = Fonts[_translation?.DefaultFontIndex ?? 0];
        TextRenderer.material = FontMaterial;
        TextRenderer.material.mainTexture = FontTextures[_translation?.DefaultFontIndex ?? 0];
        TextMesh.lineSpacing = _translation?.LineSpacing ?? 0.525f;

        var acceptableWidths = _acceptableWidthsWithoutQuestionSprite;
        if (useQuestionSprite)
        {
            acceptableWidths = Ut.NewArray<double[]>(
                [0, 1.1896],
                [0.0712, 1.258],
                [0.1476, 1.258],
                [0.306, 1.3442],
                [0.443, 1.668],
                [0.549, 1.668],
                [0.55, 1.6 - .874 * QuestionSprite.sprite.rect.width / QuestionSprite.sprite.pixelsPerUnit]);
        }

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
            var heightOfALine = size.z / SurfaceSizeFactor;
            var wrapWidths = new List<double>();

            var wrappedSB = new StringBuilder();
            var first = true;
            foreach (var line in Ut.WordWrap(
                text,
                line =>
                {
                    var y = line * heightOfALine;
                    if (line < wrapWidths.Count)
                        return wrapWidths[line];
                    while (wrapWidths.Count < line)
                        wrapWidths.Add(0);
                    var i = 1;
                    while (i < acceptableWidths.Length && acceptableWidths[i][0] < y)
                        i++;
                    if (i == acceptableWidths.Length)
                        wrapWidths.Add(acceptableWidths[i - 1][1] * SurfaceSizeFactor);
                    else
                    {
                        var lambda = (y - acceptableWidths[i - 1][0]) / (acceptableWidths[i][0] - acceptableWidths[i - 1][0]);
                        var acceptableWidth = (acceptableWidths[i - 1][1] * (1 - lambda) + acceptableWidths[i][1] * lambda) * SurfaceSizeFactor;
                        var j = i;
                        while (j < acceptableWidths.Length && acceptableWidths[j][0] < y + heightOfALine)
                        {
                            acceptableWidth = Math.Min(acceptableWidth, acceptableWidths[j][1] * SurfaceSizeFactor);
                            j++;
                        }
                        if (j < acceptableWidths.Length)
                        {
                            var lambda2 = (y + heightOfALine - acceptableWidths[j - 1][0]) / (acceptableWidths[j][0] - acceptableWidths[j - 1][0]);
                            acceptableWidth = Math.Min(acceptableWidth, (acceptableWidths[j - 1][1] * (1 - lambda2) + acceptableWidths[j][1] * lambda2) * SurfaceSizeFactor);
                        }
                        wrapWidths.Add(acceptableWidth);
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

    private IEnumerator ProcessModule(KMBombModule module)
    {
        var moduleType = module.ModuleType;
        if (Ut.ModuleHandlers.Get(moduleType, default) is not { } hAttr)
        {
            Debug.Log($"‹Souvenir #{_moduleId}› Module {moduleType}: Not supported.");
            yield break;
        }

        _coroutinesActive++;

        if (!_moduleTypeInfo.TryGetValue(moduleType, out var info))
            info = _moduleTypeInfo[moduleType] = new();
        info.NumModules++;
        _supportedModuleNames.Add(module.ModuleDisplayName);

        yield return null;  // Ensures that the module’s Start() method has run
        Debug.Log($"‹Souvenir #{_moduleId}› Module {moduleType}: Start processing.");
        var data = new ModuleData { Module = module, Info = info };
        module.OnPass += () =>
        {
            if (data.Unsolved)
                data.SolveIndex = info.NumSolved++;
            data.Unsolved = false;
            return false;
        };

        var questions = new List<QandAStump>();

        try
        {
            // Unfortunately C# doesn’t allow ‘yield return’ inside of a ‘try’ block with a ‘catch’ clause,
            // so we have to instantiate the enumerator and put the ‘try’/‘catch’ around just the e.MoveNext() call
            using var e = (IEnumerator<SouvenirInstruction>) hAttr.Method.Invoke(this, [data]);
            _activeProcessors.Add(module.ModuleDisplayName);
            while (true)
            {
                bool canMoveNext;
                try { canMoveNext = e.MoveNext(); }
                catch (Exception ex)
                {
                    Debug.Log(ex is AbandonModuleException
                        ? $"<Souvenir #{_moduleId}> Abandoning {module.ModuleDisplayName} because: {ex.Message}"
                        : $"<Souvenir #{_moduleId}> The {module.ModuleDisplayName} handler threw an exception ({ex.GetType().FullName}):\n{ex.Message}\n{ex.StackTrace}");
                    _showWarning = true;
                    yield break;
                }
                if (!canMoveNext)
                    break;

                switch (e.Current)
                {
                    case null:
                        yield return null;
                        break;
                    case SouvenirYieldInstruction y:
                        yield return y.Object;
                        break;
                    case WaitForSolveInstruction:
                        yield return new WaitWhile(() => data.Unsolved);
                        break;
                    case WaitForActivateInstruction:
                        yield return new WaitWhile(() => !_isActivated);
                        break;
                    case WaitForUnignoredModulesInstruction:
                        yield return new WaitWhile(() => !_noUnignoredModulesLeft);
                        break;
                    case LegitimatelyNoQuestionInstruction:
                        yield break;
                    case DiscriminatorInstruction d:
                        if (d.Discriminator == null || d.Discriminator.Id == null)
                        {
                            Debug.Log($"<Souvenir #{_moduleId}> Abandoning {module.ModuleDisplayName} because the handler returned a null {(d.Discriminator == null ? "discriminator" : "discriminator ID")}.");
                            _showWarning = true;
                            yield break;
                        }
                        if (info.Discriminators.Get(module)?.ContainsKey(d.Discriminator.Id) == true)
                        {
                            Debug.Log($"<Souvenir #{_moduleId}> Abandoning {module.ModuleDisplayName} because the handler generated multiple discriminators with the same ID “{d.Discriminator.Id}”, which is not allowed.");
                            _showWarning = true;
                            yield break;
                        }
                        info.Discriminators.AddSafe(module, d.Discriminator.Id, d.Discriminator);
                        break;
                    case QandAInstruction q:
                        if (q.Stump == null || q.Stump.QuestionStump == null || q.Stump.AnswerStump == null)
                        {
                            Debug.Log($"<Souvenir #{_moduleId}> Abandoning {module.ModuleDisplayName} because the handler returned a {(q.Stump == null ? "null stump" : q.Stump.QuestionStump == null ? "null question stump" : "null answer stump")}.");
                            _showWarning = true;
                            yield break;
                        }
                        if (q.Stump.QuestionStump.QuestionAttribute.Type == AnswerType.DynamicFont &&
                            q.Stump.AnswerStump is not TextAnswerStump { Info: { Font: not null, FontTexture: not null } })
                        {
                            Debug.Log($"<Souvenir #{_moduleId}> Abandoning {module.ModuleDisplayName} because the question {q.Stump.QuestionStump.EnumValue.GetType().Name}.{q.Stump.QuestionStump.EnumValue} is marked as using a dynamic font, but no font or font texture was specified. You must provide both a font and a font texture by passing a {nameof(TextAnswerInfo)} object.");
                            _showWarning = true;
                            yield break;
                        }
                        questions.Add(q.Stump);
                        break;
                }

                if (TwitchAbandonModule.Contains(module) && !_config.IgnoreTpAutosolvers)
                {
                    Debug.Log($"<Souvenir #{_moduleId}> Abandoning {module.ModuleDisplayName} because Twitch Plays told me to.");
                    yield break;
                }
            }
        }
        finally
        {
            _activeProcessors.Remove(module.ModuleDisplayName);
            _coroutinesActive--;
            info.NumFinished++;
        }

        while (info.NumFinished < info.NumModules)
            yield return null;

        if (questions.Count > 0)
            Debug.Log($"<Souvenir #{_moduleId}> Questions for {module.ModuleDisplayName}:\n{questions.Select(q => $"• {q}").JoinString("\n")}");
        if (info.Discriminators.Get(module) is { Count: > 0 } logDiscr)
            Debug.Log($"<Souvenir #{_moduleId}> Discriminators for {module.ModuleDisplayName}:\n{logDiscr.Select(d => $"• {d.Value}").JoinString("\n")}");

        var bossTried = false;
        tryAgain:
        if (questions.Count == 0)
        {
            if (!_legitimatelyNoQuestions.Contains(module))
            {
                if (bossTried)
                    Debug.Log($"[Souvenir #{_moduleId}] There was no question for {module.ModuleDisplayName} because it is a boss module and there were no applicable discriminators.");
                else
                {
                    Debug.Log($"[Souvenir #{_moduleId}] The handler for {module.ModuleDisplayName} did not generate any questions. Please report this to the contributor for that module as this may indicate a bug in Souvenir. Remember to send them this logfile.");
                    _showWarning = true;
                }
            }
        }
        else
        {
            // Construct the answers first, then pick a discriminator that doesn’t conflict with them
            var q = questions.PickRandom();
            var answerSet = q.AnswerStump.GenerateAnswerSet(q.QuestionStump, this);
            if (answerSet == null)
                // An error message will have already been logged
                yield break;
            var questionHasQuestionSprite = q.QuestionStump is TextQuestionStump { QuestionSprite: { } } or SpriteQuestionStump;
            string moduleFormat = null;
            Sprite questionSpriteFromDiscriminator = null;
            var questionSpriteRotationFromDiscriminator = 0f;
            if (info.NumModules > 1)
            {
                if (info.Discriminators.Get(module) is { Count: > 0 } discrRaw)
                {
                    var discrs = discrRaw.Values.Where(d =>
                            !d.AvoidEntirely &&
                            // avoid discriminators that the question explicitly tells us to avoid
                            q.QuestionStump.DiscriminatorsToAvoid?.Contains(d.EnumValue) != true &&
                            q.QuestionStump.DiscriminatorIdsToAvoid?.Contains(d.Id) != true &&
                            // avoid discriminators that clash with one of the answers we already selected
                            d.AvoidAnswers?.Intersect(answerSet.Answers).Any() != true &&
                            // can’t use a question sprite if the question already uses one
                            (d.QuestionSprite == null || !questionHasQuestionSprite) &&
                            // use this discriminator only if its value is actually unique
                            info.Discriminators.Values.Count(ds => ds.TryGetValue(d.Id, out var cd) && Equals(cd.Value, d.Value)) == 1)
                        .GroupBy(d => d.PriorityFromQuestion?.Invoke(q.QuestionStump.EnumValue) ?? d.Priority)
                        .OrderBy(gr => gr.Key)
                        .FirstOrDefault()?.ToArray();

                    if (discrs == null && hAttr.IsBossModule)
                    {
                        Debug.Log($"<Souvenir #{_moduleId}> No applicable discriminator to ask question {q.QuestionStump.EnumValue.GetType().Name}.{q.QuestionStump.EnumValue} with args {q.QuestionStump.Args.Stringify()} and answers {answerSet.DebugAnswers.ToArray().Stringify()}.");
                        questions.Remove(q);
                        bossTried = true;
                        goto tryAgain;
                    }

                    // If this is false, the solve-count discriminator was picked and ‘moduleFormat’ will default to it later
                    if (discrs?.Concat(hAttr.IsBossModule ? [] : [null]).PickRandom() is { } discr && discr.EnumValue.GetDiscriminatorAttribute() is var dAttr)
                    {
                        moduleFormat = string.Format(
                            TranslateDiscriminator(discr.EnumValue, dAttr.DiscriminatorText),
                            (discr.Arguments ?? discr.ArgumentsFromQuestion?.Invoke(q.QuestionStump.EnumValue) ?? [])
                                .Select<string, object>((arg, ix) => Snip(dAttr.TranslateArguments?[ix] == true ? TranslateDiscriminatorArgument(discr.EnumValue, arg) : arg))
                                .ToArray());
                        questionSpriteFromDiscriminator = discr.QuestionSprite;
                        questionSpriteRotationFromDiscriminator = discr.QuestionSpriteRotation;
                    }
                }

                if (moduleFormat == null && hAttr.IsBossModule)
                {
                    Debug.Log($"[Souvenir #{_moduleId}] No question for {module.ModuleDisplayName} because there was no applicable discriminator.");
                    yield break;
                }
            }
            _questions.Add(q.GenerateQandA(answerSet, moduleFormat ?? formatModuleName(hAttr, info.NumModules > 1, data.SolveIndex + 1), Bomb.GetSolvedModuleIDs().Count, questionSpriteFromDiscriminator, questionSpriteRotationFromDiscriminator));
        }
        Debug.Log($"‹Souvenir #{_moduleId}› Module {moduleType}: Finished processing.");
    }

    private void OnDestroy()
    {
        foreach (var tx in _unityObjectsToDestroyLater)
            Destroy(tx);
        _unityObjectsToDestroyLater.Clear();
    }

    #endregion

    #region Helper methods for Reflection (used by module handlers)
    private Component GetComponent(ModuleData module, string name) => GetComponent(module.Module, name);
    private Component GetComponent(KMBombModule module, string name) => GetComponent(module.gameObject, name);
    private Component GetComponent(GameObject module, string name) => module.GetComponent(name)
        ?? module.GetComponents(typeof(Component)).FirstOrDefault(c => c.GetType().FullName == name)
        ?? throw new AbandonModuleException($"{module.name} game object has no {name} component. Components are: {module.GetComponents(typeof(Component)).Select(c => c.GetType().FullName).JoinString(", ")}");

    private FieldInfo<T> GetField<T>(object target, string name, bool isPublic = false) => new(
        target ?? throw new AbandonModuleException($"Attempt to get {(isPublic ? "public" : "non-public")} field {name} of type {typeof(T).FullName} from a null object."),
        GetFieldImpl<T>(target.GetType(), name, isPublic, BindingFlags.Instance));

    private FieldInfo<T> GetFieldFromType<T>(Type targetType, string name, bool isPublic = false) => new(null, GetFieldImpl<T>(
        targetType ?? throw new AbandonModuleException($"Attempt to get {(isPublic ? "public" : "non-public")} field {name} of type {typeof(T).FullName} from a null type."),
        name, isPublic, BindingFlags.Instance));

    private IntFieldInfo GetIntField(object target, string name, bool isPublic = false) => new(
        target ?? throw new AbandonModuleException($"Attempt to get {(isPublic ? "public" : "non-public")} field {name} of type int from a null object."),
        GetFieldImpl<int>(target.GetType(), name, isPublic, BindingFlags.Instance));

    private ArrayFieldInfo<T> GetArrayField<T>(object target, string name, bool isPublic = false) => new(
        target ?? throw new AbandonModuleException($"Attempt to get {(isPublic ? "public" : "non-public")} field {name} of type {typeof(T).FullName}[] from a null object."),
        GetFieldImpl<T[]>(target.GetType(), name, isPublic, BindingFlags.Instance));

    private ListFieldInfo<T> GetListField<T>(object target, string name, bool isPublic = false) => new(
        target ?? throw new AbandonModuleException($"Attempt to get {(isPublic ? "public" : "non-public")} field {name} of type List<{typeof(T).FullName}> from a null object."),
        GetFieldImpl<List<T>>(target.GetType(), name, isPublic, BindingFlags.Instance));

    private FieldInfo<T> GetStaticField<T>(Type targetType, string name, bool isPublic = false) => new(null, GetFieldImpl<T>(
            targetType ?? throw new AbandonModuleException($"Attempt to get {(isPublic ? "public" : "non-public")} static field {name} of type {typeof(T).FullName} from a null type."),
            name, isPublic, BindingFlags.Static));

    private FieldInfo GetFieldImpl<T>(Type targetType, string name, bool isPublic, BindingFlags bindingFlags, bool noThrow = false)
    {
        FieldInfo fld;
        var type = targetType;
        while (type != null && type != typeof(object))
        {
            if ((fld = type.GetField(name, (isPublic ? BindingFlags.Public : BindingFlags.NonPublic) | bindingFlags)) != null)
                goto found;

            // In case it’s actually an auto-implemented property and not a field.
            if ((fld = type.GetField("<" + name + ">k__BackingField", BindingFlags.NonPublic | bindingFlags)) != null)
                goto found;

            // Reflection won’t return private fields in base classes unless we check those explicitly
            type = type.BaseType;
        }

        return noThrow ? null : throw new AbandonModuleException($"Type {targetType} does not contain {(isPublic ? "public" : "non-public")} field {name}. Fields are: {targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Select(f => $"{(f.IsPublic ? "public" : "private")} {f.FieldType.FullName} {f.Name}").JoinString(", ")}");

        found:
        return
            typeof(T).IsAssignableFrom(fld.FieldType) ? fld :
            noThrow ? null :
            throw new AbandonModuleException($"Type {targetType} has {(isPublic ? "public" : "non-public")} field {name} of type {fld.FieldType.FullName} but expected type {typeof(T).FullName}.");
    }

    private MethodInfo<T> GetMethod<T>(object target, string name, int numParameters, bool isPublic = false) => new(
        target ?? throw new AbandonModuleException($"Attempt to get {(isPublic ? "public" : "non-public")} instance method {name} with return type {typeof(T).FullName} from a null object."),
        GetMethodImpl<T>(typeof(T), target.GetType(), name, numParameters, isPublic));

    private MethodInfo<object> GetMethod(object target, string name, int numParameters, bool isPublic = false) => new(
        target ?? throw new AbandonModuleException($"Attempt to get {(isPublic ? "public" : "non-public")} instance method {name} with return type void from a null object."),
        GetMethodImpl<object>(typeof(void), target.GetType(), name, numParameters, isPublic));

    private MethodInfo<T> GetStaticMethod<T>(Type targetType, string name, int numParameters, bool isPublic = false) => new(null, GetMethodImpl<T>(
        typeof(T), targetType ?? throw new AbandonModuleException($"Attempt to get {(isPublic ? "public" : "non-public")} static method {name} with return type {typeof(T).FullName} from a null type."),
        name, numParameters, isPublic, isStatic: true));

    private MethodInfo<object> GetStaticMethod(Type targetType, string name, int numParameters, bool isPublic = false) => new(null, GetMethodImpl<object>(
        typeof(void), targetType ?? throw new AbandonModuleException($"Attempt to get {(isPublic ? "public" : "non-public")} static method {name} with return type void from a null type."),
        name, numParameters, isPublic, isStatic: true));

    private MethodInfo GetMethodImpl<T>(Type returnType, Type targetType, string name, int numParameters, bool isPublic = false, bool isStatic = false)
    {
        var bindingFlags = (isPublic ? BindingFlags.Public : BindingFlags.NonPublic) | (isStatic ? BindingFlags.Static : BindingFlags.Instance);
        var mths = targetType.GetMethods(bindingFlags).Where(m => m.Name == name && m.GetParameters().Length == numParameters && returnType.IsAssignableFrom(m.ReturnType)).Take(2).ToArray();
        return
            mths.Length == 0 ? throw new AbandonModuleException($"Type {targetType} does not contain a {(isPublic ? "public" : "non-public")} {(isStatic ? "static" : "instance")} method {name} with return type {returnType.FullName} and {numParameters} parameters.") :
            mths.Length > 1 ? throw new AbandonModuleException($"Type {targetType} contains multiple {(isPublic ? "public" : "non-public")} {(isStatic ? "static" : "instance")} methods {name} with return type {returnType.FullName} and {numParameters} parameters.") :
            mths[0];
    }

    private PropertyInfo<T> GetProperty<T>(object target, string name, bool isPublic = false) => GetPropertyImpl<T>(
        target ?? throw new AbandonModuleException($"Attempt to get {(isPublic ? "public" : "non-public")} property {name} of type {typeof(T).FullName} from a null object."),
        target.GetType(), name, isPublic, BindingFlags.Instance);

    private PropertyInfo<T> GetStaticProperty<T>(Type targetType, string name, bool isPublic = false) => GetPropertyImpl<T>(null,
        targetType ?? throw new AbandonModuleException($"Attempt to get {(isPublic ? "public" : "non-public")} static property {name} of type {typeof(T).FullName} from a null type."),
        name, isPublic, BindingFlags.Static);

    private PropertyInfo<T> GetPropertyImpl<T>(object target, Type targetType, string name, bool isPublic, BindingFlags bindingFlags)
    {
        var fld = targetType.GetProperty(name, (isPublic ? BindingFlags.Public : BindingFlags.NonPublic) | bindingFlags)
            ?? throw new AbandonModuleException($"Type {targetType} does not contain {(isPublic ? "public" : "non-public")} property {name}. Properties are: {targetType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Where(f => f.GetGetMethod() != null).Select(f => $"{(f.GetGetMethod().IsPublic ? "public" : "private")} {f.PropertyType.FullName} {f.Name}").JoinString(", ")}");
        return !typeof(T).IsAssignableFrom(fld.PropertyType)
            ? throw new AbandonModuleException($"Type {targetType} has {(isPublic ? "public" : "non-public")} field {name} of type {fld.PropertyType.FullName} but expected type {typeof(T).FullName}.")
            : new PropertyInfo<T>(target, fld);
    }
    #endregion

    #region Methods for adding questions to the pool (used by module handlers)
    private static readonly AnswerType[] _standardAnswerTypes = Ut.GetEnumValues<AnswerType>().Where(a => (int) a >= 0).ToArray();

    private QuestionStump question(Enum question, string[] args = null, Sprite questionSprite = null, float questionSpriteRotation = 0) =>
        new TextQuestionStump(question, this, args, questionSprite, questionSpriteRotation);
    private QuestionStump question(Enum question, Sprite entireQuestionSprite, string[] args = null) =>
        new SpriteQuestionStump(question, this, args, entireQuestionSprite);

    private string formatModuleName(SouvenirHandlerAttribute handler, bool addSolveCount, int numSolved) => _translation != null
        ? _translation.FormatModuleName(handler, addSolveCount, numSolved)
        : addSolveCount ? $"the {handler.ModuleName} you solved {Ordinal(numSolved)}" : handler.ModuleNameWithThe;

    private string titleCase(string str) => str.Length < 1 ? str : char.ToUpperInvariant(str[0]) + str.Substring(1).ToLowerInvariant();

    public string Ordinal(int number) => _translation != null
            ? _translation.Ordinal(number)
            : number < 0
                ? "(" + number + ")th"
                : number switch
                {
                    1 => "first",
                    2 => "second",
                    3 => "third",
                    4 => "fourth",
                    5 => "fifth",
                    6 => "sixth",
                    7 => "seventh",
                    8 => "eighth",
                    9 => "ninth",
                    10 => "tenth",
                    _ => (number / 10 % 10 == 1 ? 0 : number % 10) switch
                    {
                        1 => number + "st",
                        2 => number + "nd",
                        3 => number + "rd",
                        _ => number + "th",
                    },
                };

    /// <summary>
    /// Indicates that this handler can't generate a question, but that that's no an error.
    /// <code>yield return</code>ing this will stop the handler.
    /// </summary>
    private LegitimatelyNoQuestionInstruction legitimatelyNoQuestion(KMBombModule module, string logMessage)
    {
        Debug.Log($"[Souvenir #{_moduleId}] No question for {module.ModuleDisplayName} because: {logMessage}");
        _legitimatelyNoQuestions.Add(module);
        return LegitimatelyNoQuestionInstruction.Instance;
    }
    /// <summary>
    /// Indicates that this handler can't generate a question, but that that's no an error.
    /// <code>yield return</code>ing this will stop the handler.
    /// </summary>
    private LegitimatelyNoQuestionInstruction legitimatelyNoQuestion(ModuleData module, string logMessage) => legitimatelyNoQuestion(module.Module, logMessage);
    #endregion

    #region Twitch Plays
    internal bool TwitchPlaysActive = false;
    private readonly List<KMBombModule> TwitchAbandonModule = [];
#pragma warning disable 649
    private bool TwitchShouldCancelCommand;
#pragma warning restore 649

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} answer 3 [order is from top to bottom, then left to right] | !{0} cycle [play all audio clips]";
#pragma warning restore 414

    private IEnumerator ProcessTwitchCommand(string command)
    {
        Match m;

        if (Application.isEditor)
        {
            yield return null;

            if (command == "tp")
            {
                ActivateTwitchPlaysNumbers();
                yield break;
            }

            if ((m = Regex.Match(command, $@"^\s*lang (en|{TranslationInfo.AllTranslations.Keys.JoinString("|")})\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)).Success)
            {
                _translation = m.Groups[1].Value.Equals("en", StringComparison.InvariantCultureIgnoreCase) ? null : TranslationInfo.AllTranslations[m.Groups[1].Value.ToLowerInvariant()];
                if (_showIntros)
                    showIntro(_curIntro);
                else
                    showExampleQuestion();
                yield break;
            }

            if (Regex.IsMatch(command, @"^\s*intros?\s*"))
            {
                _showIntros = true;
                showIntro(0);
                yield break;
            }

            _showIntros = false;
            var substringMatch = -1;
            for (var i = 0; i < _exampleModules.Length; i++)
            {
                var j = (i + _curExampleModule + 1) % _exampleModules.Length;
                var moduleName = TranslateModuleName(_exampleModules[j].EnumType) ?? _exampleModules[j].EnumType.GetHandlerAttribute().ModuleNameWithThe;
                if (Regex.IsMatch(moduleName, $"^{Regex.Escape(command)}$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                {
                    setExampleModule(j);
                    yield break;
                }

                if (substringMatch == -1 && Regex.IsMatch(moduleName, Regex.Escape(command), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                    substringMatch = j;
            }

            if (substringMatch != -1)
                setExampleModule(substringMatch);
            else
                Debug.LogError($"Module containing “{command}” not found.");
            yield break;
        }

        if (_animating || _currentQuestion == null)
        {
            yield return "sendtochaterror {0}, there is no question active right now on module {1} (Souvenir).";
            yield break;
        }

        if (_currentQuestion.Answers is AudioAnswerSet audio && Regex.IsMatch(command, @"\A\s*cycle\s*\z", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            for (var i = 0; i < audio.NumAnswersAllowed; i++)
            {
                var startTime = Time.time;
                var endTime = startTime + audio.PlaySound(i);
                while (Time.time < endTime)
                {
                    if (TwitchShouldCancelCommand)
                    {
                        audio.StopSound();
                        audio.Deselect();
                        yield return "cancelled";
                        yield break;
                    }
                    yield return null;
                }
            }
            audio.Deselect();
            yield break;
        }

        m = Regex.Match(command, @"\A\s*answer\s+(\d)\s*\z", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        if (!m.Success || _isSolved)
            yield break;

        if (!int.TryParse(m.Groups[1].Value, out var number) || number <= 0 || number > Answers.Length || Answers[number - 1] == null || !Answers[number - 1].gameObject.activeSelf)
        {
            yield return $"sendtochaterror {{0}}, that’s not a valid answer; give me a number from 1 to {Answers.Count(a => a != null && a.gameObject.activeSelf)}.";
            yield break;
        }

        yield return null;
        if (_currentQuestion.Answers.CorrectIndex == number - 1 &&
            (_currentQuestion.Answers is not AudioAnswerSet || _currentQuestion.Answers is AudioAnswerSet { _selected: var sel } && sel == number - 1))
            yield return "awardpoints 1";
        yield return new[] { Answers[number - 1] };
    }

    private IEnumerator TwitchHandleForcedSolve()
    {
        while (true)
        {
            while (_currentQuestion == null)
            {
                if (_isSolved)
                    yield break;
                yield return true;
            }

            Answers[_currentQuestion.Answers.CorrectIndex].OnInteract();
            yield return new WaitForSeconds(.1f);
        }
    }

    private void ActivateTwitchPlaysNumbers()
    {
        AnswersParent.transform.localPosition = new Vector3(.005f, 0, 0);
        foreach (var gobj in TpNumbers)
            gobj.SetActive(true);
    }
    #endregion
}

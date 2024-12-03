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
    #region Fields
    public KMBombInfo Bomb;
    public KMBombModule Module;
    public KMAudio Audio;
    public KMBossModule BossModule;
    public KMSelectable[] Answers;
    public GameObject AnswersParent;
    public GameObject[] TpNumbers;
    public Sprite[] ArithmelogicSprites;
    public Sprite[] AudioSprites;
    public Sprite[] AzureButtonSprites;
    public Sprite[] BookOfMarioSprites;
    public Sprite[] CharacterSlotsSprites;
    public Sprite[] ConnectedMonitorsSprites;
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
    public Sprite[] MahjongSprites;
    public Sprite[] MaroonButtonSprites;
    public Sprite[] MathEmSprites;
    public Sprite[] MemorySprites;
    public Sprite[] MisterSofteeSprites;
    public Sprite[] NonverbalSimonSprites;
    public Sprite[] OrderedKeysSprites;
    public Sprite[] PatternCubeSprites;
    public Sprite[] PlanetsSprites;
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
    public Texture2D[] DigitTextures;
    public Texture2D[] FuseBoxQuestions;
    public Texture2D[] NonverbalSimonQuestions;
    public Texture2D[] TechnicalKeypadQuestions;

    public AudioClip[] ExampleAudio;
    public AudioClip[] ListeningAudio;
    public AudioClip[] SimonSamplesAudio;
    public AudioClip[] SimonSmilesAudio;
    public AudioClip[] SonicTheHedgehogAudio;

    private readonly List<Texture2D> _questionTexturesToDestroyLater = new();

    public TextMesh TextMesh;
    public Renderer TextRenderer;
    public Renderer SurfaceRenderer;
    public SpriteRenderer QuestionSprite;
    public SpriteRenderer EntireQuestionSprite;
    public GameObject WarningIcon;
    public Material FontMaterial;
    public Font[] Fonts;
    public Texture[] FontTextures;
    public Mesh HighlightShort; // 6 answers, 3 columns
    public Mesh HighlightLong;  // 4 answers, 2 columns
    public Mesh HighlightVeryLong;  // 4 long answers, 1 column

    public static readonly string[] _defaultIgnoredModules = { "The Heart", "The Swan", "+", "14", "42", "501", "A>N<D", "Bamboozling Time Keeper", "Black Arrows", "Brainf---", "Busy Beaver", "Cube Synchronization", "Don't Touch Anything", "Floor Lights", "Forget Any Color", "Forget Enigma", "Forget Everything", "Forget Infinity", "Forget Maze Not", "Forget It Not", "Forget Me Not", "Forget Me Later", "Forget Perspective", "Forget The Colors", "Forget This", "Forget Them All", "Forget Us Not", "Iconic", "Keypad Directionality", "Kugelblitz", "Multitask", "OmegaDestroyer", "OmegaForget", "Organization", "Password Destroyer", "Purgatory", "RPS Judging", "Security Council", "Shoddy Chess", "Simon Forgets", "Simon's Stages", "Soulscream", "Souvenir", "Tallordered Keys", "The Time Keeper", "The Troll", "The Twin", "The Very Annoying Button", "Timing is Everything", "Turn The Key", "Ultimate Custom Night", "Whiteout", "Übermodule" };

    private Config _config;
    private readonly List<QuestionBatch> _questions = new();
    private readonly HashSet<KMBombModule> _legitimatelyNoQuestions = new();
    private readonly HashSet<string> _supportedModuleNames = new();
    private readonly HashSet<string> _ignoredModules = new();
    private bool _isActivated = false;
    private ITranslation _translation;

    private QandA _currentQuestion = null;
    private bool _isSolved = false;
    private bool _animating = false;
    private bool _exploded = false;
    private bool _noUnignoredModulesLeft = false;
    private int _avoidQuestions = 0;   // While this is > 0, temporarily avoid asking questions; currently only used when Souvenir is hidden by a Mystery Module
    private bool _showWarning = false;
    private List<string> _activeProcessors = new();

    [NonSerialized]
    public double SurfaceSizeFactor;

    private readonly Dictionary<string, int> _moduleCounts = new();
    private readonly Dictionary<string, int> _modulesSolved = new();
    private int _coroutinesActive;

    private static int _moduleIdCounter = 1;
    internal int _moduleId;
    private Dictionary<string, ModuleHandlerInfo> _moduleProcessors;

    // Used in TestHarness only
    private Question[] _exampleQuestions = null;
    private int _curExampleQuestion = 0;
    private int _curExampleOrdinal = 0;
    private int _curExampleVariant = 0;
    private bool _showIntros = false;

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

    /// <summary><code>yield return</code> this to wait for the module to call <see cref="KMBombModule.HandlePass()"/></summary>
    private static readonly WaitForSolveInstruction WaitForSolve = new();

    #endregion

    #region Souvenir’s own module logic
    void Start()
    {
        _moduleId = _moduleIdCounter;
        _moduleIdCounter++;

        Debug.Log($"[Souvenir #{_moduleId}] Souvenir version: {Version}");

        // Use Souvenir-settings.txt as opposed to SouvenirSettings.json for existing settings
        ModSettings<Config> modConfig = new ModSettings<Config>("Souvenir-settings");
        _config = modConfig.Read();

        var ignoredList = BossModule.GetIgnoredModules(Module, _defaultIgnoredModules);
        Debug.Log($"‹Souvenir #{_moduleId}› Ignored modules: {ignoredList.JoinString(", ")}");
        _ignoredModules.UnionWith(ignoredList);

        if (_config.Language != null && TranslationBase<TranslationInfo>.AllTranslations.ContainsKey(_config.Language))
            _translation = TranslationBase<TranslationInfo>.AllTranslations[_config.Language];
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
                    Debug.Log($"[Souvenir #{_moduleId}] When bomb exploded, 1 question was pending for: {_questions.Select(q => q.Module.ModuleDisplayName).OrderBy(q => q).JoinString(", ")}.");
                else
                    Debug.Log($"[Souvenir #{_moduleId}] When bomb exploded, {_questions.Count} questions were pending for: {_questions.Select(q => q.Module.ModuleDisplayName).OrderBy(q => q).JoinString(", ")}.");
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
            for (int i = 0; i < transform.parent.childCount; i++)
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
                    var (q, attr) = (entry.Key, entry.Value);
                    if (attr.Type != AnswerType.Sprites && attr.Type != AnswerType.Grid && attr.Type != AnswerType.Audio && (attr.AllAnswers == null || attr.AllAnswers.Length == 0) &&
                        (attr.ExampleAnswers == null || attr.ExampleAnswers.Length == 0) && attr.AnswerGenerator == null)
                        Debug.LogError($"<Souvenir #{_moduleId}> Question {q} has no answers. Specify either SouvenirQuestionAttribute.AllAnswers or SouvenirQuestionAttribute.ExampleAnswers (with preferredWrongAnswers in-game), or add an AnswerGeneratorAttribute to the question enum value.");
                    if (attr.TranslateFormatArgs != null && attr.TranslateFormatArgs.Length != attr.ExampleFormatArgumentGroupSize)
                        Debug.LogError($"<Souvenir #{_moduleId}> Question {q}: The length of the ‘{nameof(attr.TranslateFormatArgs)}’ array must match ‘{nameof(attr.ExampleFormatArgumentGroupSize)}’.");
                }

                Debug.LogFormat(this, "<Souvenir #{0}> Entering Unity testing mode.", _moduleId);
                _exampleQuestions = Ut.GetEnumValues<Question>();

                showExampleQuestion();

                setAnswerHandler(0, _ =>
                {
                    var coll = _showIntros ? (_translation?.IntroTexts ?? _intros).Length : _exampleQuestions.Length;
                    _curExampleQuestion = (_curExampleQuestion + coll - 1) % coll;
                    _curExampleVariant = 0;
                    _curExampleOrdinal = 0;
                    showExampleQuestion();
                });
                setAnswerHandler(1, _ =>
                {
                    _curExampleQuestion = (_curExampleQuestion + 1) % (_showIntros ? (_translation?.IntroTexts ?? _intros).Length : _exampleQuestions.Length);
                    _curExampleVariant = 0;
                    _curExampleOrdinal = 0;
                    showExampleQuestion();
                });
                setAnswerHandler(2, _ => { if (_showIntros) return; if (_curExampleOrdinal > 0) _curExampleOrdinal--; showExampleQuestion(); });
                setAnswerHandler(3, _ => { if (_showIntros) return; _curExampleOrdinal++; showExampleQuestion(); });
                setAnswerHandler(4, _ => { if (_showIntros) return; _curExampleVariant--; showExampleQuestion(); });
                setAnswerHandler(5, _ => { if (_showIntros) return; _curExampleVariant++; showExampleQuestion(); });
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

    void showExampleQuestion()
    {
        if (_showIntros)
        {
            disappear();
            SetWordWrappedText((_translation?.IntroTexts ?? _intros)[_curExampleQuestion], 1.75, useQuestionSprite: false);
            foreach (var ans in Answers)
                ans.transform.Find("AnswerText").GetComponent<TextMesh>().text = "";
            AnswersParent.SetActive(true);
            return;
        }

        var q = _exampleQuestions[_curExampleQuestion];
        if (!Ut.TryGetAttribute(q, out var attr))
        {
            Debug.LogError($"<Souvenir #{_moduleId}> Error: Question {q} has no attribute.");
            return;
        }
        if (attr.ExampleFormatArguments != null && attr.ExampleFormatArguments.Length > 0 && attr.ExampleFormatArgumentGroupSize > 0)
        {
            var numExamples = attr.ExampleFormatArguments.Length / attr.ExampleFormatArgumentGroupSize;
            _curExampleVariant = (_curExampleVariant % numExamples + numExamples) % numExamples;
        }
        var fmt = new object[attr.ExampleFormatArgumentGroupSize + 1];
        fmt[0] = formatModuleName(q, _curExampleOrdinal > 0, _curExampleOrdinal);
        for (int i = 0; i < attr.ExampleFormatArgumentGroupSize; i++)
        {
            var arg = attr.ExampleFormatArguments[_curExampleVariant * attr.ExampleFormatArgumentGroupSize + i];
            fmt[i + 1] = arg == QandA.Ordinal ? Ordinal(Rnd.Range(1, 6)) : translateFormatArg(q, arg);
        }
        QandA.QuestionBase question;
        QandA.AnswerSet answerSet;
        try
        {
            var questionText = string.Format(translateQuestion(q), fmt);
            if (attr.IsEntireQuestionSprite)
            {
                var answerSprites = attr.SpriteField == null ? ExampleSprites : (Sprite[]) typeof(SouvenirModule).GetField(attr.SpriteField, BindingFlags.Instance | BindingFlags.Public).GetValue(this) ?? ExampleSprites;
                answerSprites?.Shuffle();
                question = new QandA.SpriteQuestion(questionText, WavetappingSprites[0]);
            }
            else
                question = new QandA.TextQuestion(questionText, attr.Layout, attr.UsesQuestionSprite ? SymbolicCoordinatesSprites[0] : null, 0);
            switch (attr.Type)
            {
                case AnswerType.Audio:
                    var audioClips = attr.AudioField == null ? ExampleAudio : (AudioClip[]) typeof(SouvenirModule).GetField(attr.AudioField, BindingFlags.Instance | BindingFlags.Public).GetValue(this) ?? ExampleAudio;
                    audioClips = audioClips?.Shuffle().Take(attr.NumAnswers).ToArray();
                    answerSet = new QandA.AudioAnswerSet(attr.NumAnswers, attr.Layout, audioClips, this, attr.AudioSizeMultiplier, attr.ForeignAudioID);
                    break;
                case AnswerType.Sprites:
                case AnswerType.Grid:
                    var answerSprites = attr.SpriteField == null ? ExampleSprites : (Sprite[]) typeof(SouvenirModule).GetField(attr.SpriteField, BindingFlags.Instance | BindingFlags.Public).GetValue(this) ?? ExampleSprites;
                    answerSprites = answerSprites?.Shuffle().Take(attr.NumAnswers).ToArray();
                    if (attr.SpriteAnswerGenerator != null)
                        answerSprites = attr.SpriteAnswerGenerator.GetAnswers(this).OrderBy(_ => Rnd.value).Take(attr.NumAnswers).ToArray();
                    answerSet = new QandA.SpriteAnswerSet(attr.NumAnswers, attr.Layout, answerSprites);
                    break;
                default:
                    var answers = new List<string>(attr.NumAnswers);
                    if (attr.AllAnswers != null) answers.AddRange(attr.AllAnswers);
                    else if (attr.ExampleAnswers != null) answers.AddRange(attr.ExampleAnswers);
                    if (answers.Count <= attr.NumAnswers)
                    {
                        if (attr.AnswerGenerator != null)
                            answers.AddRange(attr.AnswerGenerator.GetAnswers(this).Except(answers).Distinct().Take(attr.NumAnswers - answers.Count));
                        answers.Shuffle();
                    }
                    else
                    {
                        answers.Shuffle();
                        answers.RemoveRange(attr.NumAnswers, answers.Count - attr.NumAnswers);
                    }
                    var correctAnswers = answers.Select(ans => attr.TranslateAnswers ? translateAnswer(q, ans) : ans).ToArray();
                    int fontIndex = attr.Type == AnswerType.DynamicFont || attr.Type == AnswerType.Default ? (_translation?.DefaultFontIndex ?? 0) : (int) attr.Type;
                    answerSet = new QandA.TextAnswerSet(attr.NumAnswers, attr.Layout, correctAnswers, Fonts[fontIndex], attr.FontSize, attr.CharacterSize, FontTextures[fontIndex], FontMaterial);
                    break;
            }
            disappear();
            SetQuestion(new QandA(q, attr.ModuleNameWithThe, question, answerSet, 0));
        }
        catch (FormatException e)
        {
            Debug.LogError($"<Souvenir #{_moduleId}> FormatException {e.Message}\nQuestionText={attr.QuestionText}\nfmt=[{fmt.JoinString(", ", "\"", "\"")}]");
        }
    }

    private string translateQuestion(Question question) => _translation?.Translate(question)?.QuestionText ?? question.GetAttribute().QuestionText;
    private string translateFormatArg(Question question, string arg) => arg == null ? null : _translation?.Translate(question)?.FormatArgs?.Get(arg, arg) ?? arg;
    private string translateAnswer(Question question, string answ) => answ == null ? null : _translation?.Translate(question)?.Answers?.Get(answ, answ) ?? answ;
    private string translateString(Question question, string str) => str == null ? null : _translation?.Translate(question)?.TranslatableStrings?.Get(str, str) ?? str;
    private string translateModuleName(Question question, string name = null) => _translation?.Translate(question)?.ModuleName ?? name;

    void setAnswerHandler(int index, Action<int> handler)
    {
        Answers[index].OnInteract = delegate
        {
            Answers[index].AddInteractionPunch();
            if (!_currentQuestion.OnPress(index))
                handler(index);
            return false;
        };
    }

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

        Debug.Log($"[Souvenir #{_moduleId}] Clicked answer #{index + 1} ({_currentQuestion.DebugAnswers.Skip(index).First()}). {(_currentQuestion.CorrectIndex == index ? "Correct" : "Wrong")}.");

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
        if (!Application.isEditor && TwitchPlaysActive)
            ActivateTwitchPlaysNumbers();

        var numPlayableModules = Bomb.GetSolvableModuleNames().Count(x => !_ignoredModules.Contains(x));

        while (true)
        {
            // A module handler can increment this value temporarily to delay asking questions. (Currently only the Mystery Module handler does this when Souvenir is hidden by a Mystery Module.)
            while (_avoidQuestions > 0)
                yield return new WaitForSeconds(.1f);

            var numSolved = Bomb.GetSolvedModuleNames().Count(x => !_ignoredModules.Contains(x));
            _noUnignoredModulesLeft = numSolved >= numPlayableModules;

            if (_questions.Count == 0 && (_noUnignoredModulesLeft || _coroutinesActive == 0))
            {
                // Very rare case: another coroutine could still be waiting to detect that a module is solved and then add another question to the queue
                yield return new WaitForSeconds(.1f);

                // If still no new questions, all supported modules are solved and we’re done. (Or maybe a coroutine is stuck in a loop, but then it’s bugged and we need to cancel it anyway.)
                if (_questions.Count == 0)
                    break;
            }

            IEnumerable<QuestionBatch> eligible = _questions;

            // If we reached the end of the bomb, everything is eligible.
            if (!_noUnignoredModulesLeft)
                // Otherwise, make sure there has been another solved module since
                eligible = eligible.Where(e => e.NumSolved < numSolved);

            var numEligibles = eligible.Count();

            if ((!_noUnignoredModulesLeft && numEligibles < 3) || numEligibles == 0)
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

    private static readonly double[][] _acceptableWidthsWithoutQuestionSprite = Ut.NewArray(
        // First value is y (vertical text advancement), second value is width of the Surface mesh at this y
        new[] { 0.834 - 0.834, 0.834 + 0.3556 },
        new[] { 0.834 - 0.7628, 0.834 + 0.424 },
        new[] { 0.834 - 0.6864, 0.834 + 0.424 },
        new[] { 0.834 - 0.528, 0.834 + 0.5102 },
        new[] { 0.834 - 0.4452, 0.834 + 0.6618 },
        new[] { 0.834 - 0.4452, 0.834 + 0.7745 },
        new[] { 0.834 - 0.391, 0.834 + 0.834 });

    private static readonly double[][] _acceptableWidthsWithQuestionSprite = Ut.NewArray(
        // First value is y (vertical text advancement), second value is width of the Surface mesh at this y
        new[] { 0.834 - 0.834, 0.834 + 0.3556 },
        new[] { 0.834 - 0.7628, 0.834 + 0.424 },
        new[] { 0.834 - 0.6864, 0.834 + 0.424 },
        new[] { 0.834 - 0.528, 0.834 + 0.5102 },
        new[] { 0.834 + 0.255, 0.834 + 0.5102 },
        new[] { 0.834 + 0.256, 0.834 + 0.834 });

    public void SetWordWrappedText(string text, double desiredHeightFactor, bool useQuestionSprite)
    {
        TextMesh.gameObject.SetActive(true);
        TextMesh.font = Fonts[_translation?.DefaultFontIndex ?? 0];
        TextRenderer.material = FontMaterial;
        TextRenderer.material.mainTexture = FontTextures[_translation?.DefaultFontIndex ?? 0];
        TextMesh.lineSpacing = _translation?.LineSpacing ?? 0.525f;

        var acceptableWidths = useQuestionSprite ? _acceptableWidthsWithQuestionSprite : _acceptableWidthsWithoutQuestionSprite;
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
                    while (i < acceptableWidths.Length && acceptableWidths[i][0] < y)
                        i++;
                    if (i == acceptableWidths.Length)
                        wrapWidths.Add(acceptableWidths[i - 1][1] * SurfaceSizeFactor);
                    else
                    {
                        var lambda = (y - acceptableWidths[i - 1][0]) / (acceptableWidths[i][0] - acceptableWidths[i - 1][0]);
                        wrapWidths.Add((acceptableWidths[i - 1][1] * (1 - lambda) + acceptableWidths[i][1] * lambda) * SurfaceSizeFactor);
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
        _coroutinesActive++;
        var moduleType = module.ModuleType;
        _moduleCounts.IncSafe(moduleType);
        var processor = _moduleProcessors.Get(moduleType, default).Processor;

        if (processor != null)
        {
            _supportedModuleNames.Add(module.ModuleDisplayName);
            yield return null;  // Ensures that the module’s Start() method has run
            Debug.Log($"‹Souvenir #{_moduleId}› Module {moduleType}: Start processing.");
            ModuleData data = new() { Module = module };
            module.OnPass += () =>
            {
                data.Unsolved = false;
                data.SolveIndex = _modulesSolved.IncSafe(module.ModuleDisplayName);
                return false;
            };

            // I’d much rather just put a ‘foreach’ loop inside a ‘try’ block, but Unity’s C# version doesn’t allow ‘yield return’ inside of ‘try’ blocks yet
            using (var e = processor(data))
            {
                _activeProcessors.Add(module.ModuleDisplayName);
                while (true)
                {
                    bool canMoveNext;
                    try { canMoveNext = e.MoveNext(); }
                    catch (AbandonModuleException ex)
                    {
                        Debug.Log($"<Souvenir #{_moduleId}> Abandoning {module.ModuleDisplayName} because: {ex.Message}");
                        _showWarning = true;
                        _coroutinesActive--;
                        _activeProcessors.Remove(module.ModuleDisplayName);
                        yield break;
                    }
                    catch (Exception ex)
                    {
                        Debug.Log($"<Souvenir #{_moduleId}> The {module.ModuleDisplayName} handler threw an exception ({ex.GetType().FullName}):\n{ex.Message}\n{ex.StackTrace}");
                        _showWarning = true;
                        _coroutinesActive--;
                        _activeProcessors.Remove(module.ModuleDisplayName);
                        yield break;
                    }
                    if (!canMoveNext)
                        break;

                    if (e.Current is WaitForSolveInstruction)
                        yield return new WaitWhile(() => data.Unsolved);
                    else
                        yield return e.Current;

                    if (TwitchAbandonModule.Contains(module))
                    {
                        Debug.Log($"<Souvenir #{_moduleId}> Abandoning {module.ModuleDisplayName} because Twitch Plays told me to.");
                        _coroutinesActive--;
                        _activeProcessors.Remove(module.ModuleDisplayName);
                        yield break;
                    }
                }
                _activeProcessors.Remove(module.ModuleDisplayName);
            }

            if (!_legitimatelyNoQuestions.Contains(module) && !_questions.Any(q => q.Module == module))
            {
                Debug.Log($"[Souvenir #{_moduleId}] There was no question generated for {module.ModuleDisplayName}. Please report this to Timwi or the implementer for that module as this may indicate a bug in Souvenir. Remember to send them this logfile.");
                _showWarning = true;
            }
            Debug.Log($"‹Souvenir #{_moduleId}› Module {moduleType}: Finished processing.");
        }
        else
        {
            Debug.Log($"‹Souvenir #{_moduleId}› Module {moduleType}: Not supported.");
        }

        _coroutinesActive--;
    }

    private void OnDestroy()
    {
        foreach (var tx in _questionTexturesToDestroyLater)
            Destroy(tx);
        _questionTexturesToDestroyLater.Clear();
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
        Type type = targetType;
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
        if (mths.Length == 0)
            throw new AbandonModuleException($"Type {targetType} does not contain a {(isPublic ? "public" : "non-public")} {(isStatic ? "static" : "instance")} method {name} with return type {returnType.FullName} and {numParameters} parameters.");
        if (mths.Length > 1)
            throw new AbandonModuleException($"Type {targetType} contains multiple {(isPublic ? "public" : "non-public")} {(isStatic ? "static" : "instance")} methods {name} with return type {returnType.FullName} and {numParameters} parameters.");
        return mths[0];
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
        if (!typeof(T).IsAssignableFrom(fld.PropertyType))
            throw new AbandonModuleException($"Type {targetType} has {(isPublic ? "public" : "non-public")} field {name} of type {fld.PropertyType.FullName} but expected type {typeof(T).FullName}.");
        return new PropertyInfo<T>(target, fld);
    }
    #endregion

    #region Methods for adding questions to the pool (used by module handlers)
    private void addQuestion(ModuleData module, Question question, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArguments = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null, string[] allAnswers = null, float questionSpriteRotation = 0)
    {
        addQuestion(module.Module, question, questionSprite, formattedModuleName, formatArguments, correctAnswers, preferredWrongAnswers, allAnswers, questionSpriteRotation, module.SolveIndex);
    }

    private void addQuestion(KMBombModule module, Question question, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArguments = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null, string[] allAnswers = null, float questionSpriteRotation = 0, int solveIx = 0)
    {
        addQuestions(module, makeQuestion(question, module.ModuleType, solveIx, questionSprite, formattedModuleName, formatArguments, correctAnswers, preferredWrongAnswers, allAnswers, questionSpriteRotation));
    }

    private void addQuestion(ModuleData module, Question question, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArguments = null, Sprite[] correctAnswers = null, Sprite[] allAnswers = null, Sprite[] preferredWrongAnswers = null, float questionSpriteRotation = 0)
    {
        addQuestion(module.Module, question, questionSprite, formattedModuleName, formatArguments, correctAnswers, preferredWrongAnswers, allAnswers, questionSpriteRotation, module.SolveIndex);
    }

    private void addQuestion(KMBombModule module, Question question, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArguments = null, Sprite[] correctAnswers = null, Sprite[] allAnswers = null, Sprite[] preferredWrongAnswers = null, float questionSpriteRotation = 0, int solveIx = 0)
    {
        addQuestions(module, makeQuestion(question, module.ModuleType, solveIx, questionSprite, formattedModuleName, formatArguments, correctAnswers, preferredWrongAnswers, allAnswers, questionSpriteRotation));
    }

    private void addQuestion(ModuleData module, Question question, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArguments = null, Coord[] correctAnswers = null, Coord[] preferredWrongAnswers = null, float questionSpriteRotation = 0)
    {
        addQuestion(module.Module, question, questionSprite, formattedModuleName, formatArguments, correctAnswers, preferredWrongAnswers, questionSpriteRotation, module.SolveIndex);
    }

    private void addQuestion(KMBombModule module, Question question, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArguments = null, Coord[] correctAnswers = null, Coord[] preferredWrongAnswers = null, float questionSpriteRotation = 0, int solveIx = 0)
    {
        addQuestions(module, makeQuestion(question, module.ModuleType, solveIx, questionSprite, formattedModuleName, formatArguments, correctAnswers, preferredWrongAnswers, questionSpriteRotation));
    }

    private void addQuestion(ModuleData module, Question question, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArguments = null, AudioClip[] correctAnswers = null, AudioClip[] allAnswers = null, AudioClip[] preferredWrongAnswers = null, float questionSpriteRotation = 0)
    {
        addQuestion(module.Module, question, questionSprite, formattedModuleName, formatArguments, correctAnswers, preferredWrongAnswers, allAnswers, questionSpriteRotation, module.SolveIndex);
    }

    private void addQuestion(KMBombModule module, Question question, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArguments = null, AudioClip[] correctAnswers = null, AudioClip[] allAnswers = null, AudioClip[] preferredWrongAnswers = null, float questionSpriteRotation = 0, int solveIx = 0)
    {
        addQuestions(module, makeQuestion(question, module.ModuleType, solveIx, questionSprite, formattedModuleName, formatArguments, correctAnswers, preferredWrongAnswers, allAnswers, questionSpriteRotation));
    }

    private void addQuestions(KMBombModule module, IEnumerable<QandA> questions)
    {
        if (_config.IsExcluded(module, _ignoredModules))
        {
            Debug.Log($"<Souvenir #{_moduleId}> Discarding questions for {module.ModuleDisplayName} because it is excluded in the mod settings.");
            _legitimatelyNoQuestions.Add(module);
            return;
        }

        if (_questions.Any(qb => qb.Module == module))
        {
            _showWarning = true;
            throw new AbandonModuleException($"The handler for {module.ModuleDisplayName} submitted multiple question batches. Handlers should not call addQuestion/addQuestions multiple times, but instead make a single call to addQuestions() with all of the questions.");
        }

        var qs = questions.Where(q => q != null).ToArray();
        if (qs.Length == 0)
        {
            Debug.Log($"<Souvenir #{_moduleId}> Empty question batch provided for {module.ModuleDisplayName}.");
            return;
        }
        Debug.Log($"‹Souvenir #{_moduleId}› Adding question batch for “{module.ModuleDisplayName}”:\n{qs.Select(q => "    • " + q.DebugString).JoinString("\n")}");
        _questions.Add(new QuestionBatch
        {
            NumSolved = Bomb.GetSolvedModuleNames().Count,
            Questions = qs,
            Module = module
        });
    }

    private void addQuestions(KMBombModule module, params QandA[] questions) => addQuestions(module, (IEnumerable<QandA>) questions);
    private void addQuestions(ModuleData module, params QandA[] questions) => addQuestions(module.Module, (IEnumerable<QandA>) questions);
    private void addQuestions(ModuleData module, IEnumerable<QandA> questions) => addQuestions(module.Module, questions);

    private static readonly AnswerType[] _standardAnswerTypes = Ut.GetEnumValues<AnswerType>().Where(a => (int) a >= 0).ToArray();

    private QandA makeQuestion(Question question, ModuleData data, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArgs = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null, string[] allAnswers = null, float questionSpriteRotation = 0) =>
        makeQuestion(question, data.Module.ModuleType, data.SolveIndex, questionSprite, formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers, questionSpriteRotation);
    private QandA makeQuestion(Question question, ModuleData data, Font font, Texture fontTexture, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArgs = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null, string[] allAnswers = null, float questionSpriteRotation = 0) =>
        makeQuestion(question, data.Module.ModuleType, data.SolveIndex, font, fontTexture, questionSprite, formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers, questionSpriteRotation);
    private QandA makeQuestion(Question question, ModuleData data, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArgs = null, Sprite[] correctAnswers = null, Sprite[] preferredWrongAnswers = null, Sprite[] allAnswers = null, float questionSpriteRotation = 0) =>
        makeQuestion(question, data.Module.ModuleType, data.SolveIndex, questionSprite, formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers, questionSpriteRotation);
    private QandA makeQuestion(Question question, ModuleData data, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArgs = null, Coord[] correctAnswers = null, Coord[] preferredWrongAnswers = null, float questionSpriteRotation = 0) =>
        makeQuestion(question, data.Module.ModuleType, data.SolveIndex, questionSprite, formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, questionSpriteRotation);
    private QandA makeQuestion(Question question, ModuleData data, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArgs = null, AudioClip[] correctAnswers = null, AudioClip[] preferredWrongAnswers = null, AudioClip[] allAnswers = null, float questionSpriteRotation = 0) =>
        makeQuestion(question, data.Module.ModuleType, data.SolveIndex, questionSprite, formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers, questionSpriteRotation);

    private QandA makeQuestion(Question question, string moduleId, int solveIx, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArgs = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null, string[] allAnswers = null, float questionSpriteRotation = 0) =>
        makeQuestion(question, moduleId, solveIx,
            (attr, q) => new QandA.TextQuestion(q, attr.Layout, questionSprite, questionSpriteRotation),
            (attr, num, answers) => new QandA.TextAnswerSet(num, attr.Layout, answers, Fonts[attr.Type == AnswerType.Default ? (_translation?.DefaultFontIndex ?? 0) : (int) attr.Type], attr.FontSize, attr.CharacterSize, FontTextures[attr.Type == AnswerType.Default ? (_translation?.DefaultFontIndex ?? 0) : (int) attr.Type], FontMaterial),
            formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers, _standardAnswerTypes);

    private QandA makeQuestion(Question question, string moduleId, int solveIx, Font font, Texture fontTexture, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArgs = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null, string[] allAnswers = null, float questionSpriteRotation = 0) =>
        makeQuestion(question, moduleId, solveIx,
            (attr, q) => new QandA.TextQuestion(q, attr.Layout, questionSprite, questionSpriteRotation),
            (attr, num, answers) => new QandA.TextAnswerSet(num, attr.Layout, answers, font, attr.FontSize, attr.CharacterSize, fontTexture, FontMaterial),
            formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers, AnswerType.DynamicFont);

    private QandA makeQuestion(Question question, string moduleId, int solveIx, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArgs = null, Sprite[] correctAnswers = null, Sprite[] preferredWrongAnswers = null, Sprite[] allAnswers = null, float questionSpriteRotation = 0) =>
        makeQuestion(question, moduleId, solveIx,
            (attr, q) => new QandA.TextQuestion(q, attr.Layout, questionSprite, questionSpriteRotation),
            (attr, num, answers) => new QandA.SpriteAnswerSet(num, attr.Layout, answers),
            formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers ?? GetAllSprites(question), AnswerType.Sprites);

    private QandA makeSpriteQuestion(Sprite questionSprite, Question question, ModuleData data, string formattedModuleName = null, string[] formatArgs = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null, string[] allAnswers = null) =>
        makeSpriteQuestion(questionSprite, question, data.Module.ModuleType, data.SolveIndex, formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers);

    private QandA makeSpriteQuestion(Sprite questionSprite, Question question, string moduleId, int solveIx, string formattedModuleName = null, string[] formatArgs = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null, string[] allAnswers = null) =>
        makeQuestion(question, moduleId, solveIx,
            (attr, q) => new QandA.SpriteQuestion(q, questionSprite),
            (attr, num, answers) => new QandA.TextAnswerSet(num, attr.Layout, answers, Fonts[attr.Type == AnswerType.Default ? (_translation?.DefaultFontIndex ?? 0) : (int) attr.Type], attr.FontSize, attr.CharacterSize, FontTextures[attr.Type == AnswerType.Default ? (_translation?.DefaultFontIndex ?? 0) : (int) attr.Type], FontMaterial),
            formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers, _standardAnswerTypes);

    private QandA makeSpriteQuestion(Sprite questionSprite, Question question, ModuleData data, string formattedModuleName = null, string[] formatArgs = null, AudioClip[] correctAnswers = null, AudioClip[] preferredWrongAnswers = null, AudioClip[] allAnswers = null) =>
        makeSpriteQuestion(questionSprite, question, data.Module.ModuleType, data.SolveIndex, formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers);

    private QandA makeSpriteQuestion(Sprite questionSprite, Question question, string moduleId, int solveIx, string formattedModuleName = null, string[] formatArgs = null, Sprite[] correctAnswers = null, Sprite[] preferredWrongAnswers = null, Sprite[] allAnswers = null) =>
        makeQuestion(question, moduleId, solveIx,
            (attr, q) => new QandA.SpriteQuestion(q, questionSprite),
            (attr, num, answers) => new QandA.SpriteAnswerSet(num, attr.Layout, answers),
            formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers ?? GetAllSprites(question), AnswerType.Sprites);

    private QandA makeSpriteQuestion(Sprite questionSprite, Question question, ModuleData data, string formattedModuleName = null, string[] formatArgs = null, Sprite[] correctAnswers = null, Sprite[] preferredWrongAnswers = null, Sprite[] allAnswers = null) =>
        makeSpriteQuestion(questionSprite, question, data.Module.ModuleType, data.SolveIndex, formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers);

    private QandA makeSpriteQuestion(Sprite questionSprite, Question question, string moduleId, int solveIx, string formattedModuleName = null, string[] formatArgs = null, AudioClip[] correctAnswers = null, AudioClip[] preferredWrongAnswers = null, AudioClip[] allAnswers = null) =>
    makeQuestion(question, moduleId, solveIx,
        (attr, q) => new QandA.SpriteQuestion(q, questionSprite),
        (attr, num, answers) => new QandA.AudioAnswerSet(num, attr.Layout, answers, this, attr.AudioSizeMultiplier, attr.ForeignAudioID),
        formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers ?? GetAllSounds(question), AnswerType.Audio);

    private QandA makeQuestion(Question question, string moduleId, int solveIx, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArgs = null, Coord[] correctAnswers = null, Coord[] preferredWrongAnswers = null, float questionSpriteRotation = 0)
    {
        var w = correctAnswers[0].Width;
        var h = correctAnswers[0].Height;
        if (correctAnswers.Concat(preferredWrongAnswers ?? Enumerable.Empty<Coord>()).Any(c => c.Width != w || c.Height != h))
        {
            Debug.LogError($"<Souvenir #{_moduleId}> The module handler for {moduleId} provided grid coordinates for different sizes of grids.");
            throw new InvalidOperationException();
        }
        return makeQuestion(question, moduleId, solveIx,
            (attr, q) => new QandA.TextQuestion(q, attr.Layout, questionSprite, questionSpriteRotation),
            (attr, num, answers) => new QandA.SpriteAnswerSet(num, attr.Layout, answers.Select(ans => Sprites.GenerateGridSprite(ans, 1)).ToArray()), formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, Enumerable.Range(0, w * h).Select(ix => new Coord(w, h, ix)).ToArray(), AnswerType.Grid);
    }
    private QandA makeQuestion(Question question, string moduleId, int solveIx, Sprite questionSprite = null, string formattedModuleName = null, string[] formatArgs = null, AudioClip[] correctAnswers = null, AudioClip[] preferredWrongAnswers = null, AudioClip[] allAnswers = null, float questionSpriteRotation = 0)
    {
        return makeQuestion(question, moduleId, solveIx,
            (attr, q) => new QandA.TextQuestion(q, attr.Layout, questionSprite, questionSpriteRotation),
            (attr, num, answers) => new QandA.AudioAnswerSet(num, attr.Layout, answers, this, attr.AudioSizeMultiplier, attr.ForeignAudioID),
            formattedModuleName, formatArgs, correctAnswers, preferredWrongAnswers, allAnswers ?? GetAllSounds(question), AnswerType.Audio);
    }

    private QandA makeQuestion<T>(Question question, string moduleId, int solveIx, Func<SouvenirQuestionAttribute, string, QandA.QuestionBase> questionConstructor,
        Func<SouvenirQuestionAttribute, int, T[], QandA.AnswerSet> answerSetConstructor, string formattedModuleName = null,
        string[] formatArgs = null, T[] correctAnswers = null, T[] preferredWrongAnswers = null, T[] allAnswers = null, params AnswerType[] acceptableTypes)
    {
        if (!Ut.TryGetAttribute(question, out var attr))
        {
            Debug.LogError($"<Souvenir #{_moduleId}> Question {question} has no SouvenirQuestionAttribute.");
            return null;
        }
        if (!acceptableTypes.Contains(attr.Type))
        {
            Debug.LogError($"<Souvenir #{_moduleId}> The module handler for {attr.ModuleName} attempted to generate question {question} (type={attr.Type}) but used the wrong answer type.");
            return null;
        }

        var allAnswersWasNull = allAnswers == null;
        allAnswers ??= attr.AllAnswers as T[];
        if (allAnswers != null)
        {
            var inconsistency = correctAnswers.Except(allAnswers).FirstOrDefault();
            if (inconsistency != null)
            {
                Debug.LogError($"<Souvenir #{_moduleId}> Question {question}: invalid answer: {inconsistency.ToString() ?? "<null>"}.\nallAnswers: {(allAnswersWasNull ? "was null" : "was not null")}; [{allAnswers.Select(s => $"{s} ({s.GetHashCode()})").JoinString(", ")}]\ncorrectAnswers: [{correctAnswers.Select(s => $"{s} ({s.GetHashCode()})").JoinString(", ")}]");
                return null;
            }
            if (preferredWrongAnswers != null)
            {
                var inconsistency2 = preferredWrongAnswers.Except(allAnswers).FirstOrDefault();
                if (inconsistency2 != null)
                {
                    Debug.LogError($"<Souvenir #{_moduleId}> Question {question}: invalid preferred wrong answer: {inconsistency2.ToString() ?? "<null>"}.");
                    return null;
                }
            }
        }

        var answers = new List<T>(attr.NumAnswers);
        if (allAnswers == null && attr.AnswerGenerator == null && (attr.SpriteAnswerGenerator == null || typeof(T) != typeof(Sprite)))
        {
            if (preferredWrongAnswers == null || preferredWrongAnswers.Length == 0)
            {
                Debug.LogError($"<Souvenir #{_moduleId}> Question {question} has no answers. You must specify either the full set of possible answers in SouvenirQuestionAttribute.AllAnswers, provide answers through the preferredWrongAnswers or allAnswers parameters, or add an AnswerGeneratorAttribute to the question enum value.");
                return null;
            }
            answers.AddRange(preferredWrongAnswers.Except(correctAnswers).Distinct());
        }
        else if (allAnswers != null || attr.AnswerGenerator != null)
        {
            // Pick 𝑛−1 random wrong answers.
            if (allAnswers != null)
                answers.AddRange(allAnswers.Except(correctAnswers));
            if (answers.Count <= attr.NumAnswers - 1)
            {
                if (attr.AnswerGenerator != null && typeof(T) == typeof(string))
                    answers.AddRange(attr.AnswerGenerator.GetAnswers(this).Except(answers.Concat(correctAnswers) as IEnumerable<string>).Distinct().Take(attr.NumAnswers - 1 - answers.Count) as IEnumerable<T>);
                if (answers.Count == 0 && (preferredWrongAnswers == null || preferredWrongAnswers.Length == 0))
                {
                    Debug.LogError($"<Souvenir #{_moduleId}> Question {question}’s answer generator did not generate any answers.");
                    return null;
                }
            }
            else
            {
                answers.Shuffle();
                answers.RemoveRange(attr.NumAnswers - 1, answers.Count - (attr.NumAnswers - 1));
            }
            // Add the preferred wrong answers, if any. If we had added them earlier, they’d come up too rarely.
            if (preferredWrongAnswers != null)
                answers.AddRange(preferredWrongAnswers.Except(answers.Concat(correctAnswers)).Distinct());
        }
        else
        {
            if (attr.SpriteAnswerGenerator != null && typeof(T) == typeof(Sprite))
                answers.AddRange(attr.SpriteAnswerGenerator.GetAnswers(this).Except(answers.Concat(correctAnswers) as IEnumerable<Sprite>).Distinct().Take(attr.NumAnswers - 1 - answers.Count) as IEnumerable<T>);
            if (answers.Count == 0 && (preferredWrongAnswers == null || preferredWrongAnswers.Length == 0))
            {
                Debug.LogError($"<Souvenir #{_moduleId}> Question {question}’s answer generator did not generate any answers.");
                return null;
            }
            if (preferredWrongAnswers != null)
                answers.AddRange(preferredWrongAnswers.Except(answers.Concat(correctAnswers)).Distinct());
        }
        answers.Shuffle();
        if (answers.Count >= attr.NumAnswers)
            answers.RemoveRange(attr.NumAnswers - 1, answers.Count - (attr.NumAnswers - 1));

        var correctIndex = Rnd.Range(0, answers.Count + 1);
        answers.Insert(correctIndex, correctAnswers.PickRandom());
        if (answers[0] is string && attr.TranslateAnswers)
            for (var i = 0; i < answers.Count; i++)
                answers[i] = (T) (object) translateAnswer(question, (string) (object) answers[i]);

        if (solveIx < 1 && formattedModuleName is null)
        {
            Debug.LogError($"<Souvenir #{_moduleId}> Abandoning {attr.ModuleName} because it wasn't solved. Make sure to `yield return WaitForSolve;`. If this is intentional, either specify the solve index yourself, or specify the formatted module name.");
            return null;
        }

        var allFormatArgs = new string[formatArgs != null ? formatArgs.Length + 1 : 1];
        allFormatArgs[0] = formattedModuleName ?? formatModuleName(question, _moduleCounts.Get(moduleId, 0) > 1, solveIx);

        if (formatArgs != null)
            for (var i = 0; i < formatArgs.Length; i++)
                allFormatArgs[i + 1] = translateFormatArg(question, formatArgs[i]);

        var qQuestion = questionConstructor(attr, string.Format(translateQuestion(question), allFormatArgs));
        var qAnswerSet = answerSetConstructor(attr, answers.Count, answers.ToArray());
        return new QandA(question, attr.ModuleNameWithThe, qQuestion, qAnswerSet, correctIndex);
    }

    private string formatModuleName(Question question, bool addSolveCount, int numSolved) => _translation != null
        ? _translation.FormatModuleName(question, addSolveCount, numSolved)
        : addSolveCount ? $"the {question.GetAttribute().ModuleName} you solved {Ordinal(numSolved)}" : question.GetAttribute().ModuleNameWithThe;

    public string[] GetAnswers(Question question) => !Ut.TryGetAttribute(question, out var attr)
        ? throw new InvalidOperationException($"<Souvenir #{_moduleId}> Question {question} is missing from the _attributes dictionary.")
        : attr.AllAnswers;

    private Sprite[] GetAllSprites(Question question)
    {
        var attr = question.GetAttribute();
        if (attr.Type != AnswerType.Sprites)
            throw new AbandonModuleException("GetAllSprites() was called on a question that doesn’t use sprites or doesn’t have an associated sprites field.");
        return attr.SpriteField == null ? null : GetField<Sprite[]>(this, attr.SpriteField, isPublic: true).Get();
    }

    private AudioClip[] GetAllSounds(Question question)
    {
        var attr = question.GetAttribute();
        if (attr.Type != AnswerType.Audio || attr.AudioField == null)
            throw new AbandonModuleException("GetAllSounds() was called on a question that doesn’t use sounds or doesn’t have an associated sounds field.");
        return GetField<AudioClip[]>(this, attr.AudioField, isPublic: true).Get();
    }

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

    private void legitimatelyNoQuestion(KMBombModule module, string logMessage)
    {
        Debug.Log($"[Souvenir #{_moduleId}] No question for {module.ModuleDisplayName} because: {logMessage}");
        _legitimatelyNoQuestions.Add(module);
    }
    #endregion

    #region Twitch Plays
    internal bool TwitchPlaysActive = false;
    private readonly List<KMBombModule> TwitchAbandonModule = new();
#pragma warning disable 649
    private bool TwitchShouldCancelCommand;
#pragma warning restore 649

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} answer 3 [order is from top to bottom, then left to right] | !{0} cycle [play all audio clips]";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
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

            if ((m = Regex.Match(command, $@"^\s*lang (en|{TranslationBase<TranslationInfo>.AllTranslations.Keys.JoinString("|")})\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)).Success)
            {
                _translation = m.Groups[1].Value.Equals("en", StringComparison.InvariantCultureIgnoreCase) ? null : TranslationBase<TranslationInfo>.AllTranslations[m.Groups[1].Value.ToLowerInvariant()];
                if (_showIntros)
                    _curExampleQuestion = 0;
                showExampleQuestion();
                yield break;
            }

            if (Regex.IsMatch(command, @"^\s*intros?\s*"))
            {
                _showIntros = true;
                _curExampleQuestion = 0;
                showExampleQuestion();
                yield break;
            }

            _showIntros = false;
            int substringMatch = -1;
            for (var i = 0; i < _exampleQuestions.Length; i++)
            {
                var j = (i + _curExampleQuestion + 1) % _exampleQuestions.Length;
                if (Regex.IsMatch(_translation?.Translate(_exampleQuestions[j]).ModuleName ?? _exampleQuestions[j].GetAttribute().ModuleNameWithThe, $"^{Regex.Escape(command)}$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                {
                    _curExampleQuestion = j;
                    showExampleQuestion();
                    yield break;
                }

                if (substringMatch == -1 && Regex.IsMatch(_translation?.Translate(_exampleQuestions[j]).ModuleName ?? _exampleQuestions[j].GetAttribute().ModuleNameWithThe, Regex.Escape(command), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                    substringMatch = j;
            }

            if (substringMatch != -1)
            {
                _curExampleQuestion = substringMatch;
                showExampleQuestion();
            }
            else
                Debug.LogError($"Question containing “{command}” not found.");
            yield break;
        }

        if (_currentQuestion.Answers is QandA.AudioAnswerSet audio && Regex.IsMatch(command.ToLowerInvariant(), @"\A\s*cycle\s*\z"))
        {
            yield return null;
            for (int i = 0; i < audio.NumAnswers; i++)
            {
                float startTime = Time.time;
                float endTime = startTime + audio.PlaySound(i);
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

        m = Regex.Match(command.ToLowerInvariant(), @"\A\s*answer\s+(\d)\s*\z");
        if (!m.Success || _isSolved)
            yield break;

        if (_animating || _currentQuestion == null)
        {
            yield return "sendtochaterror {0}, there is no question active right now on module {1} (Souvenir).";
            yield break;
        }
        if (!int.TryParse(m.Groups[1].Value, out var number) || number <= 0 || number > Answers.Length || Answers[number - 1] == null || !Answers[number - 1].gameObject.activeSelf)
        {
            yield return $"sendtochaterror {{0}}, that’s not a valid answer; give me a number from 1 to {Answers.Count(a => a != null && a.gameObject.activeSelf)}.";
            yield break;
        }

        yield return null;
        if (_currentQuestion.CorrectIndex == number - 1 &&
            (_currentQuestion.Answers is not QandA.AudioAnswerSet || _currentQuestion.Answers is QandA.AudioAnswerSet { _selected: var sel } && sel == number - 1))
            yield return "awardpoints 1";
        yield return new[] { Answers[number - 1] };
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        while (true)
        {
            while (_currentQuestion == null)
            {
                if (_isSolved)
                    yield break;
                yield return true;
            }

            Answers[_currentQuestion.CorrectIndex].OnInteract();
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

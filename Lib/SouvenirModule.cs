using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
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
    public KMModSettings ModSettings;
    public KMSelectable[] Answers;
    public GameObject AnswersParent;
    public GameObject[] TpNumbers;
    public Sprite[] KeypadSprites;
    public Sprite[] MemorySprites;
    public Sprite[] ArithmelogicSprites;
    public Sprite[] ExampleSprites;
    public Sprite[] MahjongSprites;
    public Sprite[] PatternCubeSprites;
    public Sprite[] PlanetsSprites;
    public Sprite[] SymbolicCoordinatesSprites;
    public Sprite[] SymbolicTashaSprites;
    public Sprite[] WavetappingSprites;
    public Sprite[] FlagsSprites;
    public Sprite[] EncryptedEquationsSprites;
    public Sprite[] SimonSpeaksSprites;
    public Sprite[] MathEmSprites;
    public Sprite[] USACycleSprites;
    public Sprite[] TasqueManagingSprites;
    public Sprite[] SimonShapesSprites;
    public Sprite[] JengaSprites;
    public Sprite[] AzureButtonSprites;
    public Sprite[] NonverbalSimonSprites;
    public Texture2D[] NonverbalSimonQuestions;
    private readonly List<Texture2D> _temporaryQuestions = new List<Texture2D>();

    public TextMesh TextMesh;
    public Renderer TextRenderer;
    public Renderer SurfaceRenderer;
    public SpriteRenderer QuestionSprite;
    public SpriteRenderer EntireQuestionSprite;
    public GameObject WarningIcon;
    public Material FontMaterial;
    public Font[] Fonts;
    public Texture[] FontTextures;
    public Mesh HighlightShort; // 6 answers, 2 columns
    public Mesh HighlightLong;  // 4 answers, 2 columns
    public Mesh HighlightVeryLong;  // 4 long answers, 1 column

    public static readonly string[] _defaultIgnoredModules = { "The Heart", "The Swan", "+", "14", "42", "501", "A>N<D", "Bamboozling Time Keeper", "Black Arrows", "Brainf---", "Busy Beaver", "Cube Synchronization", "Don't Touch Anything", "Floor Lights", "Forget Any Color", "Forget Enigma", "Forget Everything", "Forget Infinity", "Forget Maze Not", "Forget It Not", "Forget Me Not", "Forget Me Later", "Forget Perspective", "Forget The Colors", "Forget This", "Forget Them All", "Forget Us Not", "Iconic", "Keypad Directionality", "Kugelblitz", "Multitask", "OmegaDestroyer", "OmegaForget", "Organization", "Password Destroyer", "Purgatory", "RPS Judging", "Security Council", "Shoddy Chess", "Simon Forgets", "Simon's Stages", "Soulscream", "Souvenir", "Tallordered Keys", "The Time Keeper", "The Troll", "The Twin", "The Very Annoying Button", "Timing is Everything", "Turn The Key", "Ultimate Custom Night", "Whiteout", "Übermodule" };

    private Config _config;
    private readonly List<QuestionBatch> _questions = new List<QuestionBatch>();
    private readonly HashSet<KMBombModule> _legitimatelyNoQuestions = new HashSet<KMBombModule>();
    private readonly HashSet<string> _supportedModuleNames = new HashSet<string>();
    private readonly HashSet<string> _ignoredModules = new HashSet<string>();
    private bool _isActivated = false;
    private Translation _translation;

    private QandA _currentQuestion = null;
    private bool _isSolved = false;
    private bool _animating = false;
    private bool _exploded = false;
    private int _avoidQuestions = 0;   // While this is > 0, temporarily avoid asking questions; currently only used when Souvenir is hidden by a Mystery Module
    private bool _showWarning = false;

    [NonSerialized]
    public double SurfaceSizeFactor;

    private readonly Dictionary<string, int> _moduleCounts = new Dictionary<string, int>();
    private readonly Dictionary<string, int> _modulesSolved = new Dictionary<string, int>();
    private int _coroutinesActive;

    private static int _moduleIdCounter = 1;
    private int _moduleId;
    private Dictionary<string, Func<KMBombModule, IEnumerable<object>>> _moduleProcessors;
    private Dictionary<Question, SouvenirQuestionAttribute> _attributes;

    // Used in TestHarness only
    private Question[] _exampleQuestions = null;
    private int _curExampleQuestion = 0;
    private int _curExampleOrdinal = 0;
    private int _curExampleVariant = 0;

#pragma warning disable 649
    private Action<double> TimeModeAwardPoints;
#pragma warning restore 649

    #endregion

    #region Souvenir’s own module logic
    void Start()
    {
        _moduleId = _moduleIdCounter;
        _moduleIdCounter++;

        Debug.LogFormat(@"[Souvenir #{0}] Souvenir version: {1}", _moduleId, Version);

        if (!string.IsNullOrEmpty(ModSettings.Settings))
        {
            bool rewriteFile;
            try
            {
                _config = JsonConvert.DeserializeObject<Config>(ModSettings.Settings);
                if (_config != null)
                {
                    var dictionary = JsonConvert.DeserializeObject<IDictionary<string, object>>(ModSettings.Settings);
                    object key;
                    // Rewrite the file if any keys have been added or removed in TweaksEditorSettings
                    var listings = ((List<Dictionary<string, object>>) Config.TweaksEditorSettings[0]["Listings"]);
                    rewriteFile = listings.Any(o => o.TryGetValue("Key", out key) && !dictionary.ContainsKey((string) key)) ||
                        dictionary.Any(p => !listings.Any(o => o.TryGetValue("Key", out key) && key.Equals(p.Key)));
                }
                else
                {
                    _config = new Config();
                    rewriteFile = true;
                }
            }
            catch (JsonSerializationException ex)
            {
                Debug.LogErrorFormat("<Souvenir #{0}> The mod settings file is invalid.", _moduleId);
                Debug.LogException(ex, this);
                _config = new Config();
                rewriteFile = true;
            }
            if (rewriteFile && !string.IsNullOrEmpty(ModSettings.SettingsPath) && !Application.isEditor)
            {
                using var writer = new StreamWriter(ModSettings.SettingsPath);
                new JsonSerializer() { Formatting = Formatting.Indented }.Serialize(writer, _config);
            }
        }
        else
            _config = new Config();

        var ignoredList = BossModule.GetIgnoredModules(Module, _defaultIgnoredModules);
        Debug.LogFormat(@"<Souvenir #{0}> Ignored modules: {1}", _moduleId, ignoredList.JoinString(", "));
        _ignoredModules.UnionWith(ignoredList);

        if (_config.Language != null && Translation.AllTranslations.ContainsKey(_config.Language))
            _translation = Translation.AllTranslations[_config.Language];
        Debug.LogFormat(@"<Souvenir #{0}> Language: {1} ({2})", _moduleId, _config.Language, _translation == null ? "absent" : "present");

        Bomb.OnBombExploded += delegate
        {
            _exploded = true;
            StopAllCoroutines();
            if (!_isSolved)
            {
                if (_questions.Count == 0)
                    Debug.LogFormat(@"[Souvenir #{0}] When bomb exploded, there were no pending questions.", _moduleId);
                else if (_questions.Count == 1)
                    Debug.LogFormat(@"[Souvenir #{0}] When bomb exploded, 1 question was pending for: {1}.", _moduleId, _questions.Select(q => q.Module.ModuleDisplayName).OrderBy(q => q).JoinString(", "));
                else
                    Debug.LogFormat(@"[Souvenir #{0}] When bomb exploded, {1} questions were pending for: {2}.", _moduleId, _questions.Count, _questions.Select(q => q.Module.ModuleDisplayName).OrderBy(q => q).JoinString(", "));
            }
        };
        Bomb.OnBombSolved += delegate
        {
            // This delegate gets invoked when _any_ bomb in the room is solved,
            // so we need to check if the bomb this module is on is actually solved
            if (Bomb.GetSolvedModuleNames().Count == Bomb.GetSolvableModuleNames().Count)
                StopAllCoroutines();
        };

        _attributes = typeof(Question).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => Ut.KeyValuePair((Question) f.GetValue(null), GetQuestionAttribute(f)))
            .Where(kvp => kvp.Value != null)
            .ToDictionary();

        var origRotation = SurfaceRenderer.transform.rotation;
        SurfaceRenderer.transform.eulerAngles = new Vector3(0, 180, 0);
        SurfaceSizeFactor = SurfaceRenderer.bounds.size.x / (2 * .834) * .9;
        SurfaceRenderer.transform.rotation = origRotation;

        disappear();
        WarningIcon.SetActive(false);
        SetWordWrappedText(Ut.NewArray(
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
            "Someone thinks they’re too clever for me. They all think that at first." // Someone thinks they’re too clever for us. They all thin that at first (Invincible)

        ).PickRandom(), 1.75, useQuestionSprite: false);

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
                    if (fldType == null) fldType = GetField<object>(vanillaModule.GetType(), "ComponentType", true);
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
                var sb = new StringBuilder();
                foreach (var entry in _attributes)
                {
                    var (q, attr) = (entry.Key, entry.Value);
                    if (attr.Type != AnswerType.Sprites && attr.Type != AnswerType.Grid && (attr.AllAnswers == null || attr.AllAnswers.Length == 0) &&
                        (attr.ExampleAnswers == null || attr.ExampleAnswers.Length == 0) && attr.AnswerGenerator == null)
                    {
                        Debug.LogErrorFormat("<Souvenir #{0}> Question {1} has no answers. Specify either SouvenirQuestionAttribute.AllAnswers or SouvenirQuestionAttribute.ExampleAnswers (with preferredWrongAnswers in-game), or add an AnswerGeneratorAttribute to the question enum value.", _moduleId, q);
                        sb.AppendLine($@"""{Regex.Replace(Regex.Escape(attr.QuestionText), @"\\\{\d+\}", m => m.Value == @"\{0}" ? attr.ModuleNameWithThe : ".*")}"",");
                    }
                    if (attr.TranslateFormatArgs != null && attr.TranslateFormatArgs.Length != attr.ExampleExtraFormatArgumentGroupSize)
                        Debug.LogErrorFormat("<Souvenir #{0}> Question {1}: The length of the ‘{2}’ array must match ‘{3}’.", _moduleId, q, nameof(attr.TranslateFormatArgs), nameof(attr.ExampleExtraFormatArgumentGroupSize));
                }
                if (sb.Length > 0)
                    Debug.Log(sb.ToString());

                Debug.LogFormat(this, "<Souvenir #{0}> Entering Unity testing mode.", _moduleId);
                _exampleQuestions = Ut.GetEnumValues<Question>();

                showExampleQuestion();

                setAnswerHandler(0, _ =>
                {
                    _curExampleQuestion = (_curExampleQuestion + _exampleQuestions.Length - 1) % _exampleQuestions.Length;
                    _curExampleVariant = 0;
                    _curExampleOrdinal = 0;
                    showExampleQuestion();
                });
                setAnswerHandler(1, _ =>
                {
                    _curExampleQuestion = (_curExampleQuestion + 1) % _exampleQuestions.Length;
                    _curExampleVariant = 0;
                    _curExampleOrdinal = 0;
                    showExampleQuestion();
                });
                setAnswerHandler(2, _ => { if (_curExampleOrdinal > 0) _curExampleOrdinal--; showExampleQuestion(); });
                setAnswerHandler(3, _ => { _curExampleOrdinal++; showExampleQuestion(); });
                setAnswerHandler(4, _ => { _curExampleVariant--; showExampleQuestion(); });
                setAnswerHandler(5, _ => { _curExampleVariant++; showExampleQuestion(); });

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

    void showExampleQuestion()
    {
        if (!_attributes.TryGetValue(_exampleQuestions[_curExampleQuestion], out var attr))
        {
            Debug.LogErrorFormat("<Souvenir #{1}> Error: Question {0} has no attribute.", _exampleQuestions[_curExampleQuestion], _moduleId);
            return;
        }
        if (attr.IsEntireQuestionSprite)
        {
            var answerSprites = attr.SpriteField == null ? ExampleSprites : (Sprite[]) typeof(SouvenirModule).GetField(attr.SpriteField, BindingFlags.Instance | BindingFlags.Public).GetValue(this) ?? ExampleSprites;
            if (answerSprites != null)
                answerSprites.Shuffle();
            SetQuestion(new QandAEntireSprite(attr.QuestionText, attr.ModuleName, 0, answerSprites, WavetappingSprites[0]));
            return;
        }
        if (attr.ExampleExtraFormatArguments != null && attr.ExampleExtraFormatArguments.Length > 0 && attr.ExampleExtraFormatArgumentGroupSize > 0)
        {
            var numExamples = attr.ExampleExtraFormatArguments.Length / attr.ExampleExtraFormatArgumentGroupSize;
            _curExampleVariant = (_curExampleVariant % numExamples + numExamples) % numExamples;
        }
        var fmt = new object[attr.ExampleExtraFormatArgumentGroupSize + 1];
        fmt[0] = formatModuleName(attr.ModuleName, _curExampleOrdinal > 0, _curExampleOrdinal, attr.AddThe);
        for (int i = 0; i < attr.ExampleExtraFormatArgumentGroupSize; i++)
        {
            var arg = attr.ExampleExtraFormatArguments[_curExampleVariant * attr.ExampleExtraFormatArgumentGroupSize + i];
            fmt[i + 1] = arg == QandA.Ordinal ? ordinal(Rnd.Range(1, 6)) : translateFormatArg(_exampleQuestions[_curExampleQuestion], arg);
        }
        try
        {
            switch (attr.Type)
            {
                case AnswerType.Sprites:
                case AnswerType.Grid:
                    var answerSprites = attr.SpriteField == null ? ExampleSprites : (Sprite[]) typeof(SouvenirModule).GetField(attr.SpriteField, BindingFlags.Instance | BindingFlags.Public).GetValue(this) ?? ExampleSprites;
                    if (answerSprites != null)
                        answerSprites.Shuffle();
                    if (attr.SpriteAnswerGenerator != null)
                        answerSprites = attr.SpriteAnswerGenerator.GetAnswers(this).OrderBy(_ => Rnd.value).Take(attr.NumAnswers).ToArray();
                    SetQuestion(new QandASprite(
                        module: attr.ModuleNameWithThe,
                        question: string.Format(attr.QuestionText, fmt),
                        correct: 0,
                        answers: answerSprites,
                        questionSprite: attr.UsesQuestionSprite ? SymbolicCoordinatesSprites[0] : null));
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
                    SetQuestion(new QandAText(
                        module: translateModuleNameWithThe(_exampleQuestions[_curExampleQuestion]),
                        question: string.Format(translateQuestion(_exampleQuestions[_curExampleQuestion]), fmt),
                        correct: 0,
                        answers: answers.Select(ans => attr.TranslateAnswers ? translateAnswer(_exampleQuestions[_curExampleQuestion], ans) : ans).ToArray(),
                        font: Fonts[attr.Type == AnswerType.DynamicFont || attr.Type == AnswerType.Default ? (_translation?.DefaultFontIndex ?? 0) : (int) attr.Type],
                        fontSize: attr.FontSize,
                        fontTexture: FontTextures[attr.Type == AnswerType.DynamicFont || attr.Type == AnswerType.Default ? (_translation?.DefaultFontIndex ?? 0) : (int) attr.Type],
                        fontMaterial: FontMaterial,
                        layout: attr.Layout,
                        questionSprite: attr.UsesQuestionSprite ? SymbolicCoordinatesSprites[0] : null));
                    break;
            }
        }
        catch (FormatException e)
        {
            Debug.LogErrorFormat("<Souvenir #{3}> FormatException {0}\nQuestionText={1}\nfmt=[{2}]", e.Message, attr.QuestionText, fmt.JoinString(", ", "\"", "\""), _moduleId);
        }
    }

    private string translateModuleName(Question question) => _translation?.Translations[question].ModuleName ?? _attributes[question].ModuleName;
    private string translateModuleNameWithThe(Question question) => _translation?.Translations[question].ModuleNameWithThe ?? _translation?.Translations[question].ModuleName ?? _attributes[question].ModuleNameWithThe;
    private string translateQuestion(Question question) => _translation?.Translations[question].QuestionText ?? _attributes[question].QuestionText;
    private string translateFormatArg(Question question, string arg) => arg == null ? null : _translation?.Translations[question].FormatArgs?.Get(arg, arg) ?? arg;
    private string translateAnswer(Question question, string answ) => answ == null ? null : _translation?.Translations[question].Answers?.Get(answ, answ) ?? answ;

    private static SouvenirQuestionAttribute GetQuestionAttribute(FieldInfo field)
    {
        var attribute = field.GetCustomAttribute<SouvenirQuestionAttribute>();
        if (attribute != null)
        {
            attribute.AnswerGenerator = field.GetCustomAttribute<AnswerGeneratorAttribute>();
            attribute.SpriteAnswerGenerator = field.GetCustomAttribute<SpriteAnswerGeneratorAttribute>();
        }
        return attribute;
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
        if (TwitchPlaysActive)
            ActivateTwitchPlaysNumbers();

        var numPlayableModules = Bomb.GetSolvableModuleNames().Count(x => !_ignoredModules.Contains(x));

        while (true)
        {
            // A module handler can increment this value temporarily to delay asking questions. (Currently only the Mystery Module handler does this when Souvenir is hidden by a Mystery Module.)
            while (_avoidQuestions > 0)
                yield return new WaitForSeconds(.1f);

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
        WarningIcon.SetActive(_showWarning);
    }

    private void SetQuestion(QandA q)
    {
        Debug.Log($"[Souvenir #{_moduleId}] Asking question: {q.DebugString}");
        _currentQuestion = q;
        if (q is QandAEntireSprite qes)
        {
            TextMesh.gameObject.SetActive(false);
            EntireQuestionSprite.gameObject.SetActive(true);
            EntireQuestionSprite.sprite = qes.QuestionSprite;
        }
        else
        {
            EntireQuestionSprite.gameObject.SetActive(false);
            TextMesh.gameObject.SetActive(true);
            TextMesh.font = Fonts[_translation?.DefaultFontIndex ?? 0];
            TextMesh.GetComponent<MeshRenderer>().material = FontMaterial;
            TextMesh.GetComponent<MeshRenderer>().material.mainTexture = FontTextures[_translation?.DefaultFontIndex ?? 0];
            TextMesh.lineSpacing = _translation?.LineSpacing ?? 0.525f;
            SetWordWrappedText(q.QuestionText, q.DesiredHeightFactor, q.QuestionSprite != null);
            QuestionSprite.gameObject.SetActive(q.QuestionSprite != null);
            QuestionSprite.sprite = q.QuestionSprite;
            QuestionSprite.transform.localEulerAngles = new Vector3(90f, q.QuestionSpriteRotation, 0f);
        }
        q.SetAnswers(this);
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

    private void SetWordWrappedText(string text, double desiredHeightFactor, bool useQuestionSprite)
    {
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
        var iterator = _moduleProcessors.Get(moduleType, null);

        if (iterator != null)
        {
            _supportedModuleNames.Add(module.ModuleDisplayName);
            yield return null;  // Ensures that the module’s Start() method has run
            Debug.LogFormat("<Souvenir #{1}> Module {0}: Start processing.", moduleType, _moduleId);

            // I’d much rather just put a ‘foreach’ loop inside a ‘try’ block, but Unity’s C# version doesn’t allow ‘yield return’ inside of ‘try’ blocks yet
            using (var e = iterator(module).GetEnumerator())
            {
                while (true)
                {
                    bool canMoveNext;
                    try { canMoveNext = e.MoveNext(); }
                    catch (AbandonModuleException ex)
                    {
                        Debug.LogFormat("<Souvenir #{0}> Abandoning {1} because: {2}", _moduleId, module.ModuleDisplayName, ex.Message);
                        _showWarning = true;
                        _coroutinesActive--;
                        yield break;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogFormat("<Souvenir #{0}> The {1} handler threw an exception ({2}):\n{3}", _moduleId, module.ModuleDisplayName, ex.GetType().FullName, ex.StackTrace);
                        _showWarning = true;
                        _coroutinesActive--;
                        yield break;
                    }
                    if (!canMoveNext)
                        break;
                    yield return e.Current;

                    if (TwitchAbandonModule.Contains(module) && Environment.MachineName != "CORNFLOWER")    // CORNFLOWER = Timwi’s computer
                    {
                        Debug.LogFormat("<Souvenir #{0}> Abandoning {1} because Twitch Plays told me to.", _moduleId, module.ModuleDisplayName);
                        _coroutinesActive--;
                        yield break;
                    }
                }
            }

            if (!_legitimatelyNoQuestions.Contains(module) && !_questions.Any(q => q.Module == module))
            {
                Debug.LogFormat("[Souvenir #{0}] There was no question generated for {1}. Please report this to Timwi or the implementer for that module as this may indicate a bug in Souvenir. Remember to send them this logfile.", _moduleId, module.ModuleDisplayName);
                _showWarning = true;
            }
            Debug.LogFormat("<Souvenir #{1}> Module {0}: Finished processing.", moduleType, _moduleId);
        }
        else
        {
            Debug.LogFormat("<Souvenir #{1}> Module {0}: Not supported.", moduleType, _moduleId);
        }

        _coroutinesActive--;
    }

    private void OnDestroy()
    {
        foreach (var tx in _temporaryQuestions)
            Destroy(tx);
        _temporaryQuestions.Clear();
    }
    #endregion

    #region Helper methods for Reflection (used by module handlers)
    private Component GetComponent(KMBombModule module, string name)
    {
        return GetComponent(module.gameObject, name);
    }
    private Component GetComponent(GameObject module, string name)
    {
        var comp = module.GetComponent(name);
        if (comp == null)
        {
            comp = module.GetComponents(typeof(Component)).FirstOrDefault(c => c.GetType().FullName == name);
            if (comp == null)
                throw new AbandonModuleException("{0} game object has no {1} component. Components are: {2}", module.name, name, module.GetComponents(typeof(Component)).Select(c => c.GetType().FullName).JoinString(", "));
        }
        return comp;
    }

    private FieldInfo<T> GetField<T>(object target, string name, bool isPublic = false)
    {
        if (target == null)
            throw new AbandonModuleException("Attempt to get {1} field {0} of type {2} from a null object.", name, isPublic ? "public" : "non-public", typeof(T).FullName);
        return new FieldInfo<T>(target, GetFieldImpl<T>(target.GetType(), name, isPublic, BindingFlags.Instance));
    }

    private FieldInfo<T> GetField<T>(Type targetType, string name, bool isPublic = false, bool noThrow = false)
    {
        if (targetType == null && !noThrow)
            throw new AbandonModuleException("Attempt to get {0} field {1} of type {2} from a null type.", isPublic ? "public" : "non-public", name, typeof(T).FullName);
        return new FieldInfo<T>(null, GetFieldImpl<T>(targetType, name, isPublic, BindingFlags.Instance, noThrow));
    }

    private IntFieldInfo GetIntField(object target, string name, bool isPublic = false)
    {
        if (target == null)
            throw new AbandonModuleException("Attempt to get {0} field {1} of type int from a null object.", isPublic ? "public" : "non-public", name);
        return new IntFieldInfo(target, GetFieldImpl<int>(target.GetType(), name, isPublic, BindingFlags.Instance));
    }

    private ArrayFieldInfo<T> GetArrayField<T>(object target, string name, bool isPublic = false)
    {
        if (target == null)
            throw new AbandonModuleException("Attempt to get {0} field {1} of type {2}[] from a null object.", isPublic ? "public" : "non-public", name, typeof(T).FullName);
        return new ArrayFieldInfo<T>(target, GetFieldImpl<T[]>(target.GetType(), name, isPublic, BindingFlags.Instance));
    }

    private ListFieldInfo<T> GetListField<T>(object target, string name, bool isPublic = false)
    {
        if (target == null)
            throw new AbandonModuleException("Attempt to get {0} field {1} of type List<{2}> from a null object.", isPublic ? "public" : "non-public", name, typeof(T).FullName);
        return new ListFieldInfo<T>(target, GetFieldImpl<List<T>>(target.GetType(), name, isPublic, BindingFlags.Instance));
    }

    private FieldInfo<T> GetStaticField<T>(Type targetType, string name, bool isPublic = false)
    {
        if (targetType == null)
            throw new AbandonModuleException("Attempt to get {0} static field {1} of type {2} from a null type.", isPublic ? "public" : "non-public", name, typeof(T).FullName);
        return new FieldInfo<T>(null, GetFieldImpl<T>(targetType, name, isPublic, BindingFlags.Static));
    }

    private FieldInfo GetFieldImpl<T>(Type targetType, string name, bool isPublic, BindingFlags bindingFlags, bool noThrow = false)
    {
        FieldInfo fld; Type type = targetType;
        while (type != null && type != typeof(object))
        {
            fld = type.GetField(name, (isPublic ? BindingFlags.Public : BindingFlags.NonPublic) | bindingFlags);
            if (fld != null)
                goto found;

            // In case it’s actually an auto-implemented property and not a field.
            fld = type.GetField("<" + name + ">k__BackingField", BindingFlags.NonPublic | bindingFlags);
            if (fld != null)
                goto found;

            // Reflection won’t return private fields in base classes unless we check those explicitly
            type = type.BaseType;
        }

        if (noThrow)
            return null;
        throw new AbandonModuleException("Type {0} does not contain {1} field {2}. Fields are: {3}", targetType, isPublic ? "public" : "non-public", name,
            targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Select(f => string.Format("{0} {1} {2}", f.IsPublic ? "public" : "private", f.FieldType.FullName, f.Name)).JoinString(", "));

    found:
        if (!typeof(T).IsAssignableFrom(fld.FieldType))
        {
            if (noThrow)
                return null;
            throw new AbandonModuleException("Type {0} has {1} field {2} of type {3} but expected type {4}.", targetType, isPublic ? "public" : "non-public", name, fld.FieldType.FullName, typeof(T).FullName);
        }
        return fld;
    }

    private MethodInfo<T> GetMethod<T>(object target, string name, int numParameters, bool isPublic = false)
    {
        return GetMethodImpl<T>(typeof(T), target, name, numParameters, isPublic);
    }

    private MethodInfo<object> GetMethod(object target, string name, int numParameters, bool isPublic = false)
    {
        return GetMethodImpl<object>(typeof(void), target, name, numParameters, isPublic);
    }

    private MethodInfo<T> GetMethodImpl<T>(Type returnType, object target, string name, int numParameters, bool isPublic = false)
    {
        if (target == null)
            throw new AbandonModuleException("Attempt to get {1} method {0} of return type {2} from a null object.", name, isPublic ? "public" : "non-public", returnType.FullName);

        var bindingFlags = (isPublic ? BindingFlags.Public : BindingFlags.NonPublic) | BindingFlags.Instance;
        var targetType = target.GetType();
        var mths = targetType.GetMethods(bindingFlags).Where(m => m.Name == name && m.GetParameters().Length == numParameters && returnType.IsAssignableFrom(m.ReturnType)).Take(2).ToArray();
        if (mths.Length == 0)
            throw new AbandonModuleException("Type {0} does not contain {1} method {2} with return type {3} and {4} parameters.", targetType, isPublic ? "public" : "non-public", name, returnType.FullName, numParameters);
        if (mths.Length > 1)
            throw new AbandonModuleException("Type {0} contains multiple {1} methods {2} with return type {3} and {4} parameters.", targetType, isPublic ? "public" : "non-public", name, returnType.FullName, numParameters);
        return new MethodInfo<T>(target, mths[0]);
    }

    private PropertyInfo<T> GetProperty<T>(object target, string name, bool isPublic = false)
    {
        if (target == null)
            throw new AbandonModuleException("Attempt to get {1} property {0} of type {2} from a null object.", name, isPublic ? "public" : "non-public", typeof(T).FullName);
        return GetPropertyImpl<T>(target, target.GetType(), name, isPublic, BindingFlags.Instance);
    }

    private PropertyInfo<T> GetStaticProperty<T>(Type targetType, string name, bool isPublic = false)
    {
        if (targetType == null)
            throw new AbandonModuleException("Attempt to get {0} static property {1} of type {2} from a null type.", isPublic ? "public" : "non-public", name, typeof(T).FullName);
        return GetPropertyImpl<T>(null, targetType, name, isPublic, BindingFlags.Static);
    }

    private PropertyInfo<T> GetPropertyImpl<T>(object target, Type targetType, string name, bool isPublic, BindingFlags bindingFlags)
    {
        var fld = targetType.GetProperty(name, (isPublic ? BindingFlags.Public : BindingFlags.NonPublic) | bindingFlags);
        if (fld == null)
            throw new AbandonModuleException("Type {0} does not contain {1} property {2}. Properties are: {3}", targetType, isPublic ? "public" : "non-public", name,
                targetType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Where(f => f.GetGetMethod() != null).Select(f => string.Format("{0} {1} {2}", f.GetGetMethod().IsPublic ? "public" : "private", f.PropertyType.FullName, f.Name)).JoinString(", "));
        if (!typeof(T).IsAssignableFrom(fld.PropertyType))
            throw new AbandonModuleException("Type {0} has {1} field {2} of type {3} but expected type {4}.", targetType, isPublic ? "public" : "non-public", name, fld.PropertyType.FullName, typeof(T).FullName, _moduleId);
        return new PropertyInfo<T>(target, fld);
    }
    #endregion

    #region Methods for adding questions to the pool (used by module handlers)
    private void addQuestion(KMBombModule module, Question question, Sprite questionSprite = null, string[] formatArguments = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null, float spriteRotation = 0f)
    {
        addQuestions(module, makeQuestion(question, module.ModuleType, questionSprite, formatArguments, correctAnswers, preferredWrongAnswers, spriteRotation));
    }

    private void addQuestion(KMBombModule module, Question question, Sprite questionSprite = null, string[] formatArguments = null, Sprite[] correctAnswers = null, Sprite[] preferredWrongAnswers = null)
    {
        addQuestions(module, makeQuestion(question, module.ModuleType, questionSprite, formatArguments, correctAnswers, preferredWrongAnswers));
    }

    private void addQuestion(KMBombModule module, Question question, Sprite questionSprite = null, string[] formatArguments = null, Coord[] correctAnswers = null, Coord[] preferredWrongAnswers = null)
    {
        addQuestions(module, makeQuestion(question, module.ModuleType, questionSprite, formatArguments, correctAnswers, preferredWrongAnswers));
    }

    private void addQuestions(KMBombModule module, IEnumerable<QandA> questions)
    {
        if (_config.IsExcluded(module, _ignoredModules))
        {
            Debug.LogFormat("<Souvenir #{0}> Discarding questions for {1} because it is excluded in the mod settings.", _moduleId, module.ModuleDisplayName);
            _legitimatelyNoQuestions.Add(module);
            return;
        }

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

    private static readonly AnswerType[] _standardAnswerTypes = Ut.GetEnumValues<AnswerType>().Where(a => (int) a >= 0).ToArray();

    private QandA makeQuestion(Question question, string moduleKey, Sprite questionSprite = null, string[] formatArgs = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null, float questionSpriteRotation = 0f) =>
        makeQuestion(question, moduleKey,
            (attr, q, correct, answers) => new QandAText(attr.ModuleNameWithThe, q, correct, answers.ToArray(), Fonts[attr.Type == AnswerType.Default ? (_translation?.DefaultFontIndex ?? 0) : (int) attr.Type], attr.FontSize, FontTextures[attr.Type == AnswerType.Default ? (_translation?.DefaultFontIndex ?? 0) : (int) attr.Type], FontMaterial, attr.Layout, questionSprite, questionSpriteRotation),
            formatArgs, correctAnswers, preferredWrongAnswers, null, _standardAnswerTypes);

    private QandA makeQuestion(Question question, string moduleKey, Font font, Texture fontTexture, Sprite questionSprite = null, string[] formatArgs = null, string[] correctAnswers = null, string[] preferredWrongAnswers = null) =>
        makeQuestion(question, moduleKey,
            (attr, q, correct, answers) => new QandAText(attr.ModuleNameWithThe, q, correct, answers.ToArray(), font, attr.FontSize, fontTexture, FontMaterial, attr.Layout, questionSprite),
            formatArgs, correctAnswers, preferredWrongAnswers, null, AnswerType.DynamicFont);

    private QandA makeQuestion(Question question, string moduleKey, Sprite questionSprite = null, string[] formatArgs = null, Sprite[] correctAnswers = null, Sprite[] preferredWrongAnswers = null) =>
        makeQuestion(question, moduleKey,
            (attr, q, correct, answers) => new QandASprite(attr.ModuleNameWithThe, q, correct, answers.ToArray(), questionSprite),
            formatArgs, correctAnswers, preferredWrongAnswers, GetAllSprites(question), AnswerType.Sprites);
   
    private QandA makeQuestion(Sprite entireQuestionSprite, Question debugQuestion, string moduleKey, string[] formatArgs = null, Sprite[] correctAnswers = null, Sprite[] preferredWrongAnswers = null) =>
        makeQuestion(debugQuestion, moduleKey,
            (attr, q, correct, answers) => new QandAEntireSprite(q, attr.ModuleNameWithThe, correct, answers.ToArray(), entireQuestionSprite),
            formatArgs, correctAnswers, preferredWrongAnswers, GetAllSprites(debugQuestion), AnswerType.Sprites);

    private QandA makeQuestion(Question question, string moduleKey, Sprite questionSprite = null, string[] formatArgs = null, Coord[] correctAnswers = null, Coord[] preferredWrongAnswers = null)
    {
        var w = correctAnswers[0].Width;
        var h = correctAnswers[0].Height;
        if (correctAnswers.Concat(preferredWrongAnswers ?? Enumerable.Empty<Coord>()).Any(c => c.Width != w || c.Height != h))
        {
            Debug.LogErrorFormat("<Souvenir #{0}> The module handler for {1} provided grid coordinates for different sizes of grids.", _moduleId, moduleKey);
            throw new InvalidOperationException();
        }
        return makeQuestion(question, moduleKey,
            (attr, q, correct, answers) => new QandASprite(attr.ModuleNameWithThe, q, correct, answers.Select(ans => Grid.GenerateGridSprite(ans, 1)).ToArray(), questionSprite),
            formatArgs, correctAnswers, preferredWrongAnswers, Enumerable.Range(0, w * h).Select(ix => new Coord(w, h, ix)).ToArray(), AnswerType.Grid);
    }

    private QandA makeQuestion<T>(Question question, string moduleKey, Func<SouvenirQuestionAttribute, string, int, T[], QandA> questionConstructor,
        string[] formatArgs, T[] correctAnswers, T[] preferredWrongAnswers, T[] allAnswers, params AnswerType[] acceptableTypes)
    {
        if (!_attributes.TryGetValue(question, out var attr))
        {
            Debug.LogErrorFormat("<Souvenir #{1}> Question {0} has no SouvenirQuestionAttribute.", question, _moduleId);
            return null;
        }
        if (!acceptableTypes.Contains(attr.Type))
        {
            Debug.LogErrorFormat("<Souvenir #{0}> The module handler for {1} attempted to generate question {2} (type={3}) but used the wrong answer type.", _moduleId, moduleKey, question, attr.Type);
            return null;
        }

        allAnswers ??= attr.AllAnswers as T[];
        if (allAnswers != null)
        {
            var inconsistency = correctAnswers.Except(allAnswers).FirstOrDefault();
            if (inconsistency != null)
            {
                Debug.LogErrorFormat("<Souvenir #{2}> Question {0}: invalid answer: {1}.", question, inconsistency.ToString() ?? "<null>", _moduleId);
                return null;
            }
            if (preferredWrongAnswers != null)
            {
                var inconsistency2 = preferredWrongAnswers.Except(allAnswers).FirstOrDefault();
                if (inconsistency2 != null)
                {
                    Debug.LogErrorFormat("<Souvenir #{2}> Question {0}: invalid preferred wrong answer: {1}.", question, inconsistency2.ToString() ?? "<null>", _moduleId);
                    return null;
                }
            }
        }

        var answers = new List<T>(attr.NumAnswers);
        if (allAnswers == null && attr.AnswerGenerator == null && (attr.SpriteAnswerGenerator == null || typeof(T) != typeof(Sprite)))
        {
            if (preferredWrongAnswers == null || preferredWrongAnswers.Length == 0)
            {
                Debug.LogErrorFormat("<Souvenir #{0}> Question {1} has no answers. You must specify either the full set of possible answers in SouvenirQuestionAttribute.AllAnswers, provide possible wrong answers through the preferredWrongAnswers parameter, or add an AnswerGeneratorAttribute to the question enum value.", _moduleId, question);
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
                    Debug.LogErrorFormat("<Souvenir #{0}> Question {1}’s answer generator did not generate any answers.", _moduleId, question);
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
                Debug.LogErrorFormat("<Souvenir #{0}> Question {1}’s answer generator did not generate any answers.", _moduleId, question);
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

        var numSolved = _modulesSolved.Get(moduleKey);
        if (numSolved < 1)
        {
            Debug.LogErrorFormat("<Souvenir #{0}> Abandoning {1} ({2}) because you forgot to increment the solve count.", _moduleId, attr.ModuleName, moduleKey);
            return null;
        }

        var allFormatArgs = new string[formatArgs != null ? formatArgs.Length + 1 : 1];
        allFormatArgs[0] = formatModuleName(attr.ModuleName, _moduleCounts.Get(moduleKey) > 1, numSolved, attr.AddThe);
        if (formatArgs != null)
            for (var i = 0; i < formatArgs.Length; i++)
                allFormatArgs[i + 1] = translateFormatArg(question, formatArgs[i]);

        return questionConstructor(attr, string.Format(translateQuestion(question), allFormatArgs), correctIndex, answers.ToArray());
    }

    private string formatModuleName(string moduleName, bool addSolveCount, int numSolved, bool addThe) => _translation != null
        ? _translation.FormatModuleName(moduleName, addSolveCount, numSolved, addThe)
        : addSolveCount ? string.Format("the {0} you solved {1}", moduleName, ordinal(numSolved)) : addThe ? "The\u00a0" + moduleName : moduleName;

    public string[] GetAnswers(Question question) => !_attributes.TryGetValue(question, out var attr)
        ? throw new InvalidOperationException(string.Format("<Souvenir #{0}> Question {1} is missing from the _attributes dictionary.", _moduleId, question))
        : attr.AllAnswers;

    private static readonly Dictionary<string, Sprite[]> _spritesCache = new Dictionary<string, Sprite[]>();
    private Sprite[] GetAllSprites(Question question)
    {
        var attr = _attributes[question];
        if (attr.Type != AnswerType.Sprites)
            throw new InvalidOperationException("GetAllSprites() was called on a question that doesn’t use sprites or doesn’t have an associated sprites field.");
        if (attr.SpriteField == null)
            return null;
        if (!_spritesCache.TryGetValue(attr.SpriteField, out var result))
            _spritesCache[attr.SpriteField] = result = GetField<Sprite[]>(this, attr.SpriteField, isPublic: true).Get();
        return result;
    }

    private string titleCase(string str) => str.Length < 1 ? str : char.ToUpperInvariant(str[0]) + str.Substring(1).ToLowerInvariant();

    private string ordinal(int number) => _translation != null
            ? _translation.Ordinal(number)
            : number < 0
                ? "(" + number + ")th"
                : number switch
                {
                    1 => "first",
                    2 => "second",
                    3 => "third",
                    _ => ((number / 10) % 10 == 1 ? 0 : number % 10) switch
                    {
                        1 => number + "st",
                        2 => number + "nd",
                        3 => number + "rd",
                        _ => number + "th",
                    },
                };
    #endregion

    #region Twitch Plays
    private bool TwitchPlaysActive = false;
    private readonly List<KMBombModule> TwitchAbandonModule = new List<KMBombModule>();

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} answer 3 [order is from top to bottom, then left to right]";
#pragma warning restore 414

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
            for (var i = 0; i < _exampleQuestions.Length; i++)
            {
                var j = (i + _curExampleQuestion + 1) % _exampleQuestions.Length;
                if (Regex.IsMatch(_attributes[_exampleQuestions[j]].ModuleNameWithThe, Regex.Escape(command), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                {
                    _curExampleQuestion = j;
                    showExampleQuestion();
                    yield break;
                }
            }
            Debug.LogError($"Question containing “{command}” not found.");
            yield break;
        }

        var m = Regex.Match(command.ToLowerInvariant(), @"\A\s*answer\s+(\d)\s*\z");
        if (!m.Success || _isSolved)
            yield break;

        if (_animating || _currentQuestion == null)
        {
            yield return "sendtochaterror {0}, there is no question active right now on module {1} (Souvenir).";
            yield break;
        }
        if (!int.TryParse(m.Groups[1].Value, out var number) || number <= 0 || number > Answers.Length || Answers[number - 1] == null || !Answers[number - 1].gameObject.activeSelf)
        {
            yield return string.Format("sendtochaterror {{0}}, that’s not a valid answer; give me a number from 1 to {0}.", Answers.Count(a => a != null && a.gameObject.activeSelf));
            yield break;
        }

        yield return null;
        if (_currentQuestion.CorrectIndex == number - 1)
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

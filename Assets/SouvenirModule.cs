using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
    public KMSelectable[] Answers;

    public GameObject Text;
    public GameObject AnswersParent;

    private bool _isTimwisComputer = Environment.GetEnvironmentVariable("COMPUTERNAME") == "TEKELIA";
    private string _timwiPath = @"D:\c\KTANE\Souvenir modules.txt";
    private Dictionary<string, List<QuestionBase>> _questions = new Dictionary<string, List<QuestionBase>>();

    void Start()
    {
        Debug.Log("[Souvenir] Started.");
        Module.OnActivate += ActivateModule;
        Bomb.OnBombExploded += delegate { StopAllCoroutines(); };
        Bomb.OnBombSolved += delegate { StopAllCoroutines(); };

        _attributes = typeof(Question).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => Ut.KeyValuePair((Question) f.GetValue(null), f.GetCustomAttribute<SouvenirQuestionAttribute>()))
            .Where(kvp => kvp.Value != null)
            .ToDictionary();
    }

    void ActivateModule()
    {
        if (_isTimwisComputer)
            lock (_timwiPath)
                File.WriteAllText(_timwiPath, "");

        foreach (var module in Enumerable.Range(0, transform.parent.childCount)
                .Select(i => transform.parent.GetChild(i).gameObject)
                .Where(i => i.GetComponent<KMBombModule>() != null))
        {
            StartCoroutine(ProcessModule(module));
        }
    }

    void ShowAnswers(string[] answers)
    {
        if (answers != null && answers.Length > 1 && answers.Length < 5)
        {
            Answers[2].transform.localPosition = Answers[2].transform.localPosition.SetX(.005f);
            Answers[3].transform.localPosition = Answers[3].transform.localPosition.SetX(.005f);
            for (int i = answers.Length; i < Answers.Length; i++)
                Answers[i].gameObject.SetActive(false);
        }
        else if (answers != null && answers.Length > 1 && answers.Length < 7)
        {
            Answers[2].transform.localPosition = Answers[2].transform.localPosition.SetX(-.02f);
            Answers[3].transform.localPosition = Answers[3].transform.localPosition.SetX(-.02f);
            Answers[4].transform.localPosition = Answers[4].transform.localPosition.SetX(.0325f);
            Answers[5].transform.localPosition = Answers[5].transform.localPosition.SetX(.0325f);
            for (int i = answers.Length; i < Answers.Length; i++)
                Answers[i].gameObject.SetActive(false);
        }
        else
        {
            Debug.LogFormat("[Souvenir] Something went wrong setting answers. length={0}, answers=[{1}]", answers == null ? "null" : answers.Length.ToString(), answers == null ? "null" : string.Join(", ", answers));
            PassAndTurnOff("Error.");
        }
    }

    private void PassAndTurnOff(string message = null)
    {
        Module.HandlePass();
        if (message == null)
            Text.SetActive(false);
        else
            Text.GetComponent<TextMesh>().text = message;
        AnswersParent.SetActive(false);
    }

    sealed class FieldInfo<T>
    {
        private object _target;
        public FieldInfo Field { get; private set; }

        public FieldInfo(object target, FieldInfo field)
        {
            _target = target;
            Field = field;
        }

        public T Get(bool nullAllowed = false)
        {
            var t = (T) Field.GetValue(_target);
            if (!nullAllowed && t == null)
                Debug.LogFormat("[Souvenir] {0} field {1} is null.", _target.GetType().FullName, Field.Name);
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

    private Component GetComponent(GameObject module, string name)
    {
        var comp = module.GetComponent(name);
        if (comp == null)
        {
            Debug.LogFormat("[Souvenir] {0} game object has no {1} component.", module.name, name);
            return null;
        }
        return comp;
    }

    private FieldInfo<T> GetField<T>(object target, string name, bool isPublic = false)
    {
        if (target == null)
        {
            Debug.LogFormat("[Souvenir] Attempt to get {1} field {0} of type {2} from a null object.", name, isPublic ? "public" : "non-public", typeof(T).FullName);
            return null;
        }
        var bindingFlags = (isPublic ? BindingFlags.Public : BindingFlags.NonPublic) | BindingFlags.Instance;
        var targetType = target.GetType();
        var fld = targetType.GetField(name, bindingFlags);
        if (fld == null)
        {
            Debug.LogFormat("[Souvenir] Type {0} does not contain {1} field {2}.", targetType, isPublic ? "public" : "non-public", name);
            return null;
        }
        if (!typeof(T).IsAssignableFrom(fld.FieldType))
        {
            Debug.LogFormat("[Souvenir] Type {0} has {1} field {2} of type {3} but expected type {4}.", targetType, isPublic ? "public" : "non-public", name, fld.FieldType.FullName, typeof(T).FullName);
            return null;
        }
        return new FieldInfo<T>(target, fld);
    }

    private MethodInfo<T> GetMethod<T>(object target, string name, int numParameters, bool isPublic = false)
    {
        if (target == null)
        {
            Debug.LogFormat("[Souvenir] Attempt to get {1} method {0} of return type {2} from a null object.", name, isPublic ? "public" : "non-public", typeof(T).FullName);
            return null;
        }
        var bindingFlags = (isPublic ? BindingFlags.Public : BindingFlags.NonPublic) | BindingFlags.Instance;
        var targetType = target.GetType();
        var mth = targetType.GetMethods(bindingFlags).FirstOrDefault(m => m.GetParameters().Length == numParameters && typeof(T).IsAssignableFrom(m.ReturnType));
        if (mth == null)
        {
            Debug.LogFormat("[Souvenir] Type {0} does not contain {1} method {2} with return type {3} and {4} parameters.", targetType, isPublic ? "public" : "non-public", name, typeof(T).FullName, numParameters);
            return null;
        }
        return new MethodInfo<T>(target, mth);
    }

    private Dictionary<string, int> _moduleCounts = new Dictionary<string, int>();
    private Dictionary<string, int> _modulesSolved = new Dictionary<string, int>();

    private IEnumerator ProcessModule(GameObject module)
    {
        yield return new WaitForSeconds(.05f + Rnd.Range(0, .05f));

        const string _3DMaze = "ThreeDMazeModule(Clone)";
        const string _AdjacentLetters = "AdjacentLettersModule(Clone)";
        const string _AdventureGame = "AdventureGameModule(Clone)";
        const string _TheBulb = "TheBulbModule(Clone)";
        const string _TwoBits = "TwoBitsModule(Clone)";

        // 𝐍𝐨𝐭 𝐠𝐨𝐧𝐧𝐚 𝐝𝐨:
        var ignore = new[] {
            "SouvenirModule(Clone)",

            // Anagrams
            "Anagrams_Module(Clone)",
            // Astrology
            // Bitmaps
            // The Button
            // Caesar Cipher
            // Complicated Wires
            // Crazy Talk
            // Cryptography
            // Emoji Math
            // Foreign Exchange Rates
            // The Gamepad
            // Piano Keys
            // Plumbing
            // Probing
            // Resistors
            // Square Button
            "AdvancedButton(Clone)"
            // Turn The Key
            // Turn The Keys
            // Wires
        };

        // 𝐒𝐭𝐫𝐢𝐤𝐞𝐬 𝐨𝐧𝐥𝐲:
        // Blind Alley
        // Chess — Chess Module(Clone)/ChessBehaviour
        // Friendship
        // Lettered Keys
        // Listening
        // Logic
        // Murder — MurderModule(Clone)/MurderModule
        // Rock-Paper-Scissors-Lizard-Spock
        // Round Keypad

        // 𝐂𝐚𝐧𝐝𝐢𝐝𝐚𝐭𝐞𝐬:
        // The Bulb
        // Colored Squares
        // Connection Check — GraphModule(Clone)/GraphModule
        // English Test
        // Follow the Leader
        // Forget Me Not
        // Hexamaze — HexamazeModule(Clone)/HexamazeModule
        // Laundry
        // Mazes
        // Memory
        // Microcontroller
        // Monsplode, Fight!
        // Morse Code
        // Morsematics
        // Mouse In The Maze
        // Mystic Square
        // Number Pad
        // Orientation Cube
        // Passwords
        // Perspective Pegs
        // Safety Safe
        // Sea Shells — SeaShellsModule(Clone)/SeaShellsModule
        // Shape Shift
        // Silly Slots
        // Simon Says
        // Simon States
        // Skewed Slots
        // Switches
        // Third Base
        // Tic-Tac-Toe — TicTacToeModule(Clone)/TicTacToeModule
        // Who’s on First

        // 𝐏𝐨𝐬𝐬𝐢𝐛𝐥𝐞 𝐟𝐮𝐭𝐮𝐫𝐞 𝐜𝐚𝐧𝐝𝐢𝐝𝐚𝐭𝐞𝐬:
        // Color Flash
        // Combination Lock
        // Keypads (strikes only)
        // Alphabet (strikes only)
        // Semaphore — SemaphoreModule(Clone)/SemaphoreModule
        // Wire Sequences

        _moduleCounts.IncSafe(module.name);

        switch (module.name)
        {
            case _3DMaze:
                {
                    var comp = GetComponent(module, "ThreeDMazeModule");
                    var fldMap = GetField<object>(comp, "map");
                    var fldIsComplete = GetField<bool>(comp, "isComplete");
                    if (comp == null || fldMap == null || fldIsComplete == null)
                        yield break;

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
                        Debug.LogFormat("[Souvenir] 3D maze wrong size ({0},{1}, expected 8,8).", mapData.GetLength(0), mapData.GetLength(1));
                        yield break;
                    }
                    var fldLabel = GetField<char>(mapData.GetValue(0, 0), "label", isPublic: true);
                    if (fldLabel == null)
                        yield break;
                    var chars = new HashSet<char>();
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            var ch = (char) fldLabel.Field.GetValue(mapData.GetValue(i, j));
                            if ("ABCDH".Contains(ch))
                                chars.Add(ch);
                        }
                    var correctMarkings = string.Join("", chars.OrderBy(c => c).Select(c => c.ToString()).ToArray());

                    while (!fldIsComplete.Get())
                        yield return new WaitForSeconds(.1f);

                    _modulesSolved.IncSafe(_3DMaze);
                    addQuestion(Question._3DMazeMarkings, _3DMaze, correctMarkings);
                    break;
                }

            case _AdjacentLetters:
                {
                    var comp = GetComponent(module, "AdjacentLettersModule");
                    var fldSubmitButton = GetField<KMSelectable>(comp, "SubmitButton", isPublic: true);
                    var fldLetters = GetField<char[]>(comp, "_letters");
                    var fldSolved = GetField<bool>(comp, "_isSolved");
                    var fldPushed = GetField<bool[]>(comp, "_pushed");

                    if (comp == null || fldSubmitButton == null || fldLetters == null || fldSolved == null || fldPushed == null)
                        yield break;

                    var letters = fldLetters.Get();
                    if (letters == null)
                        yield break;
                    if (letters.Length != 12)
                    {
                        Debug.LogFormat("[Souvenir] Adjacent Letters: _letters is {0}.", letters == null ? "null" : "of unexpected length " + letters.Length);
                        yield break;
                    }

                    var submitButton = fldSubmitButton.Get();
                    if (submitButton == null)
                        yield break;

                    var prevInteract = submitButton.OnInteract;
                    if (prevInteract == null)
                    {
                        Debug.Log("[Souvenir] Adjacent Letters: SubmitButton.OnInteract is null.");
                        yield break;
                    }

                    var incorrectSolutions = new List<bool[]>();
                    bool[] correctSolution = null;
                    submitButton.OnInteract = delegate
                    {
                        var ret = prevInteract();
                        var pushed = fldPushed.Get();
                        if (pushed == null || pushed.Length != 12)
                        {
                            Debug.LogFormat("[Souvenir] Adjacent Letters: _pushed is {0}.", letters == null ? "null" : "of unexpected length " + pushed.Length);
                            return ret;
                        }

                        // If the module is not solved, the entered solution was incorrect.
                        // Make sure to take a copy of the array.
                        if (!fldSolved.Get())
                            incorrectSolutions.Add(pushed.ToArray());
                        else
                            correctSolution = pushed.ToArray();
                        return ret;
                    };

                    while (!fldSolved.Get())
                        yield return new WaitForSeconds(.1f);

                    _modulesSolved.IncSafe(_AdjacentLetters);

                    if (correctSolution == null)
                    {
                        Debug.Log("[Souvenir] Adjacent Letters: correct solution is null.");
                        yield break;
                    }

                    for (int q = 0; q < incorrectSolutions.Count; q++)
                    {
                        addQuestion(Question.AdjacentLettersWrong, _AdjacentLetters,
                            Enumerable.Range(0, letters.Length).Where(i => correctSolution[i] != incorrectSolutions[q][i]).Select(i => letters[i].ToString()).ToArray(),
                            incorrectSolutions.Count == 1 ? "a" : "your " + ordinal(q));
                    }

                    break;
                }

            case _AdventureGame:
                {
                    var comp = GetComponent(module, "AdventureGameModule");
                    var fldButtonUse = GetField<KMSelectable>(comp, "ButtonUse", isPublic: true);
                    var fldStatValues = GetField<int[]>(comp, "StatValues");
                    var fldInvValues = GetField<IList>(comp, "InvValues"); // actually List<AdventureGameModule.ITEM>
                    var fldInvWeaponCount = GetField<int>(comp, "InvWeaponCount");
                    var fldSelectedItem = GetField<int>(comp, "SelectedItem");
                    var fldNumWeapons = GetField<int>(comp, "NumWeapons");
                    var mthItemName = GetMethod<string>(comp, "ItemName", 1);
                    var mthShouldUseItem = GetMethod<bool>(comp, "ShouldUseItem", 1);

                    if (comp == null || fldButtonUse == null || fldStatValues == null || fldInvValues == null || fldInvWeaponCount == null || fldSelectedItem == null || fldNumWeapons == null || mthItemName == null)
                        yield break;

                    var statValues = fldStatValues.Get();
                    var invValues = fldInvValues.Get();
                    var buttonUse = fldButtonUse.Get();
                    if (statValues == null || invValues == null || buttonUse == null)
                        yield break;

                    var invWeaponCount = fldInvWeaponCount.Get();
                    var numWeapons = fldNumWeapons.Get();
                    if (invWeaponCount == 0 || numWeapons == 0)
                    {
                        Debug.LogFormat("[Souvenir] {0} field {1} is 0 (zero).", comp.GetType().FullName, invWeaponCount == 0 ? fldInvWeaponCount.Field.Name : fldNumWeapons.Field.Name);
                        yield break;
                    }

                    var prevInteract = buttonUse.OnInteract;
                    if (prevInteract == null)
                    {
                        Debug.Log("[Souvenir] Adventure Game: ButtonUse.OnInteract is null.");
                        yield break;
                    }

                    var origStatValues = statValues.ToArray();
                    var origInvValues = new List<int>(invValues.Cast<int>());
                    var correctItemsUsed = 0;
                    var wrongItemsUsed = 0;
                    var qs = new List<Func<int, QuestionBase>>();
                    var solved = false;

                    buttonUse.OnInteract = delegate
                    {
                        var selectedItem = fldSelectedItem.Get();
                        var itemUsed = origInvValues[selectedItem];
                        var shouldUse = mthShouldUseItem.Invoke(selectedItem);
                        for (int j = invWeaponCount; j < invValues.Count; j++)
                            shouldUse &= !mthShouldUseItem.Invoke(j);
                        var ret = prevInteract();

                        // If the stat values have changed, the user took a potion.
                        if (statValues[0] != origStatValues[0])
                            qs.Add(questionMaker(Question.AdventureGamePotion, _AdventureGame, new[] { origStatValues[0].ToString() }, new[] { "strength" }));
                        if (statValues[1] != origStatValues[1])
                            qs.Add(questionMaker(Question.AdventureGamePotion, _AdventureGame, new[] { origStatValues[1].ToString() }, new[] { "intelligence" }));
                        if (statValues[2] != origStatValues[2])
                            qs.Add(questionMaker(Question.AdventureGamePotion, _AdventureGame, new[] { origStatValues[2].ToString() }, new[] { "dexterity" }));
                        Array.Copy(statValues, origStatValues, statValues.Length);

                        if (invValues.Count != origInvValues.Count)
                        {
                            // If the length of the inventory has changed, the user used a correct non-weapon item.
                            correctItemsUsed++;
                            qs.Add(questionMaker(Question.AdventureGameCorrectItem, _AdventureGame, new[] { titleCase(mthItemName.Invoke(itemUsed)) }, new[] { ordinal(correctItemsUsed) }));
                            origInvValues.Clear();
                            origInvValues.AddRange(invValues.Cast<int>());
                        }
                        else if (!shouldUse)
                        {
                            // The user used an incorrect item and got a strike.
                            wrongItemsUsed++;
                            qs.Add(questionMaker(Question.AdventureGameWrongItem, _AdventureGame, new[] { titleCase(mthItemName.Invoke(itemUsed)) }, new[] { ordinal(wrongItemsUsed) }));
                        }
                        else
                        {
                            // The user solved the module.
                            solved = true;
                        }

                        return ret;
                    };

                    while (!solved)
                        yield return new WaitForSeconds(.1f);

                    buttonUse.OnInteract = prevInteract;
                    _modulesSolved.IncSafe(_AdventureGame);
                    foreach (var q in qs)
                        _questions.AddSafe(_AdventureGame, q(_modulesSolved.Get(_AdventureGame)));

                    break;
                }

            case _TheBulb:
                {
                    var comp = GetComponent(module, "TheBulbModule");
                    var fldButtonO = GetField<KMSelectable>(comp, "ButtonO", isPublic: true);
                    var fldButtonI = GetField<KMSelectable>(comp, "ButtonI", isPublic: true);

                    var fldInitiallyOn = GetField<bool>(comp, "_initiallyOn");
                    var fldOffAt1 = GetField<bool>(comp, "_wentOffAtStep1");
                    var fldOAt1 = GetField<bool>(comp, "_pressedOAtStep1");
                    var fldWentOnAtScrewIn = GetField<bool>(comp, "_wentOnAtScrewIn");
                    var fldStage = GetField<int>(comp, "_stage");

                    if (comp == null || fldButtonO == null || fldButtonI == null || fldInitiallyOn == null || fldOffAt1 == null || fldOAt1 == null || fldWentOnAtScrewIn == null || fldStage == null)
                        yield break;

                    var btnO = fldButtonO.Get();
                    var btnI = fldButtonI.Get();
                    if (btnO == null || btnI == null)
                        yield break;
                    var buttonsPressed = "";

                    var prevO = btnO.OnInteract;
                    btnO.OnInteract = delegate { buttonsPressed += "O"; return prevO(); };
                    var prevI = btnI.OnInteract;
                    btnI.OnInteract = delegate { buttonsPressed += "I"; return prevI(); };

                    while (fldStage.Get() != 0)
                        yield return new WaitForSeconds(.1f);

                    btnO.OnInteract = prevO;
                    btnI.OnInteract = prevI;

                    _modulesSolved.IncSafe(_TheBulb);
                    addQuestion(Question.TheBulbButtonPresses, _TheBulb, new[] { buttonsPressed }, null,
                        Enumerable.Range(0, 1 << buttonsPressed.Length).Select(i => Convert.ToString(i, 2).Replace("0", "O").Replace("1", "I").PadLeft(buttonsPressed.Length, 'O')).ToArray());
                    break;
                }

            case _TwoBits:
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

                    var zerothNumCode = fldFirstQueryCode.Get();
                    var zerothLetterCode = queryLookups[zerothNumCode];
                    var firstResponse = queryResponses[zerothLetterCode];
                    var firstLookup = queryLookups[zerothNumCode];
                    var secondResponse = queryResponses[zerothLetterCode];
                    var secondLookup = queryLookups[zerothNumCode];
                    var thirdResponse = queryResponses[zerothLetterCode];
                    var preferredWrongAnswers = new[] { zerothNumCode.ToString("00"), firstResponse.ToString("00"), secondResponse.ToString("00"), thirdResponse.ToString("00") };
                    addQuestion(Question.TwoBitsResponse, _TwoBits, new[] { firstResponse.ToString("00") }, new[] { "first" }, preferredWrongAnswers);
                    addQuestion(Question.TwoBitsResponse, _TwoBits, new[] { secondResponse.ToString("00") }, new[] { "second" }, preferredWrongAnswers);
                    addQuestion(Question.TwoBitsResponse, _TwoBits, new[] { thirdResponse.ToString("00") }, new[] { "third" }, preferredWrongAnswers);

                    break;
                }

            default:
                if (_isTimwisComputer && !ignore.Contains(module.name))
                {
                    var s = new StringBuilder();
                    s.AppendLine("Unrecognized module: " + module.name);
                    foreach (var comp in module.GetComponents(typeof(UnityEngine.Object)))
                        s.AppendLine("    - " + comp.GetType().FullName);
                    lock (_timwiPath)
                        File.AppendAllText(_timwiPath, s.ToString());
                }
                break;
        }
    }

    private string titleCase(string str)
    {
        return str.Length < 1 ? str : char.ToUpperInvariant(str[0]) + str.Substring(1).ToLowerInvariant();
    }

    private Dictionary<Question, SouvenirQuestionAttribute> _attributes;

    private void addQuestion(Question question, string moduleKey, params string[] possibleCorrectAnswers)
    {
        addQuestion(question, moduleKey, possibleCorrectAnswers, null);
    }

    private void addQuestion(Question question, string moduleKey, string[] possibleCorrectAnswers, params string[] extraFormatArguments)
    {
        addQuestion(question, moduleKey, possibleCorrectAnswers, extraFormatArguments, null);
    }

    private void addQuestion(Question question, string moduleKey, string[] possibleCorrectAnswers, string[] extraFormatArguments, params string[] preferredWrongAnswers)
    {
        _questions.AddSafe(moduleKey, questionMaker(question, moduleKey, possibleCorrectAnswers, extraFormatArguments, preferredWrongAnswers)(_modulesSolved.Get(moduleKey)));
    }

    private Func<int, QuestionBase> questionMaker(Question question, string moduleKey, string[] possibleCorrectAnswers, string[] extraFormatArguments = null, string[] preferredWrongAnswers = null)
    {
        SouvenirQuestionAttribute attr;
        if (!_attributes.TryGetValue(question, out attr))
        {
            Debug.LogFormat("[Souvenir] Question {0} has no attribute.", question);
            return null;
        }

        var allAnswers = attr.AllAnswers;
        if (allAnswers != null)
        {
            var inconsistency = possibleCorrectAnswers.FirstOrDefault(pca => !allAnswers.Contains(pca));
            if (inconsistency != null)
            {
                Debug.LogFormat("[Souvenir] Question {0}: invalid answer: {1}.", question, inconsistency);
                return null;
            }
            if (preferredWrongAnswers != null)
            {
                var inconsistency2 = preferredWrongAnswers.FirstOrDefault(pca => !allAnswers.Contains(pca));
                if (inconsistency2 != null)
                {
                    Debug.LogFormat("[Souvenir] Question {0}: invalid preferred answer: {1}.", question, inconsistency2);
                    return null;
                }
            }
        }

        return solvedOrd =>
        {
            var num = _moduleCounts.Get(moduleKey);

            List<string> answers;
            if (allAnswers == null)
                answers = preferredWrongAnswers.Except(possibleCorrectAnswers).ToList().Shuffle().Take(attr.NumAnswers - 1).ToList();
            else
            {
                // Pick 𝑛−1 random wrong answers.
                answers = allAnswers.Except(possibleCorrectAnswers).ToList().Shuffle().Take(attr.NumAnswers - 1).ToList();
                // Add the preferred wrong answers, if any. If we added them earlier, they’d come up too rarely.
                if (preferredWrongAnswers != null)
                    answers = answers.Concat(preferredWrongAnswers.Except(possibleCorrectAnswers)).ToList().Shuffle().Take(attr.NumAnswers - 1).ToList();
            }

            var correctIndex = Rnd.Range(0, attr.NumAnswers);
            answers.Insert(correctIndex, possibleCorrectAnswers[Rnd.Range(0, possibleCorrectAnswers.Length)]);

            var formatArguments = new List<string> { num > 1 ? string.Format("the {0} you solved {1}", attr.ModuleName, ordinal(solvedOrd)) : attr.AddThe ? "the " + attr.ModuleName : attr.ModuleName };
            if (extraFormatArguments != null)
                formatArguments.AddRange(extraFormatArguments);

            var q = new QuestionText(
                string.Format(attr.QuestionText, formatArguments.ToArray()),
                answers.ToArray(),
                correctIndex,
                Bomb.GetSolvedModuleNames().Count + 2);

            Debug.LogFormat("[Souvenir] Making question:\nINPUT: question={0}, moduleKey={1}, possibleCorrectAnswers=[{2}], extraFormatArguments=[{3}], preferredWrongAnswers=[{4}], solvedOrd={5}\nOUTPUT: {6}",
                /* {0} */ question,
                /* {1} */ moduleKey,
                /* {2} */ possibleCorrectAnswers == null ? "null" : string.Join(", ", possibleCorrectAnswers),
                /* {3} */ extraFormatArguments == null ? "null" : string.Join(", ", extraFormatArguments),
                /* {4} */ preferredWrongAnswers == null ? "null" : string.Join(", ", preferredWrongAnswers),
                /* {5} */ solvedOrd,
                /* {6} */ q.DebugString);

            return q;
        };
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
}

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
    private List<QuestionBase> _questions = new List<QuestionBase>();

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

        public T Get() { return (T) Field.GetValue(_target); }
        public void Set(T value) { Field.SetValue(_target, value); }
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

    private Dictionary<string, int> _moduleCounts = new Dictionary<string, int>();
    private Dictionary<string, int> _modulesSolved = new Dictionary<string, int>();

    private IEnumerator ProcessModule(GameObject module)
    {
        yield return new WaitForSeconds(.05f + Rnd.Range(0, .05f));

        const string _3DMaze = "ThreeDMazeModule(Clone)";
        const string _AdjacentLetters = "AdjacentLettersModule(Clone)";

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

                    while (fldMap.Get() == null)
                        yield return new WaitForSeconds(.1f);

                    var map = fldMap.Get();
                    var fldMapData = GetField<Array>(map, "mapData");
                    if (fldMapData == null)
                        yield break;
                    var mapData = fldMapData.Get();
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
                    if (fldSubmitButton.Get() == null)
                    {
                        Debug.Log("[Souvenir] Adjacent Letters: SubmitButton is null.");
                        yield break;
                    }
                    if (letters == null || letters.Length != 12)
                    {
                        Debug.LogFormat("[Souvenir] Adjacent Letters: _letters is {0}.", letters == null ? "null" : "of unexpected length " + letters.Length);
                        yield break;
                    }

                    var prevInteract = fldSubmitButton.Get().OnInteract;
                    if (prevInteract == null)
                    {
                        Debug.Log("[Souvenir] Adjacent Letters: SubmitButton.OnInteract is null.");
                        yield break;
                    }

                    var incorrectSolutions = new List<bool[]>();
                    bool[] correctSolution = null;
                    fldSubmitButton.Get().OnInteract = delegate
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

            default:
                if (_isTimwisComputer)
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

    private Dictionary<Question, SouvenirQuestionAttribute> _attributes;

    private void addQuestion(Question question, string moduleKey, params string[] possibleCorrectAnswers)
    {
        addQuestion(question, moduleKey, possibleCorrectAnswers, null);
    }

    private void addQuestion(Question question, string moduleKey, string[] possibleCorrectAnswers, params string[] extraFormatArguments)
    {
        SouvenirQuestionAttribute attr;
        if (!_attributes.TryGetValue(question, out attr))
            return;

        var allAnswers = attr.AllAnswers;
        var inconsistency = possibleCorrectAnswers.FirstOrDefault(pca => !allAnswers.Contains(pca));
        if (inconsistency != null)
        {
            Debug.LogFormat("[Souvenir] Question {0}: invalid answer: {1}.", question, inconsistency);
            return;
        }

        var num = _moduleCounts.Get(moduleKey);
        var answers = allAnswers.Except(possibleCorrectAnswers).ToList().Shuffle().Take(attr.NumAnswers - 1).ToList();
        var correctIndex = Rnd.Range(0, attr.NumAnswers);
        answers.Insert(correctIndex, possibleCorrectAnswers[Rnd.Range(0, possibleCorrectAnswers.Length)]);

        var formatArguments = new List<string> { num > 1 ? string.Format("the {0} you solved {1}", attr.ModuleName, ordinal(_modulesSolved.Get(moduleKey))) : attr.ModuleName };
        if (extraFormatArguments != null)
            formatArguments.AddRange(extraFormatArguments);

        _questions.Add(new QuestionText(
            string.Format(attr.QuestionText, formatArguments.ToArray()),
            answers.ToArray(),
            correctIndex,
            Bomb.GetSolvedModuleNames().Count + 2));
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

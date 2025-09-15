using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWorkingTitle
{
    [SouvenirQuestion("What was the label shown in {0}?", OneColumn4Answers, "foo", "foobar", "quuz", "garply", "plugh", "wibble", "flob", "fuga", "toto", "tutu", "eggs", "alice", "lorem ipsum", "widget", "eek", "bat", "haystack", "blarg", "kalaa", "sub", "momo", "change this", "hi", "thing", "xyz", "bar", "qux", "corge", "waldo", "xyzzy", "wobble", "hoge", "hogera", "tata", "spam", "raboof", "bob", "do stuff", "bla", "moof", "shme", "beekeeper", "dothestuff", "mum", "temp", "var", "placeholder", "hello", "stuff", "text", "baz", "quux", "grault", "fred", "thud", "wubble", "piyo", "hogehoge", "titi", "ham", "fruit", "john doe", "data", "gadget", "gleep", "needle", "blah", "grault", "puppu", "test", "change", "null", "hey", "something", "abc")]
    Label
}

public partial class SouvenirModule
{
    [SouvenirHandler("workingTitle", "Working Title", typeof(SWorkingTitle), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessWorkingTitle(ModuleData module)
    {
        var comp = GetComponent(module, "workingTitleCode");

        var correctAnswer = GetField<TextMesh>(comp, "screenText", isPublic: true).Get().text;

        yield return WaitForSolve;

        addQuestion(module, Question.WorkingTitleLabel, correctAnswers: new[] { correctAnswer });
    }
}
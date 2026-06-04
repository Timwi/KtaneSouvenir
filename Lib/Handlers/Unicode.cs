using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using Souvenir.Reflection;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SUnicode
{
    [Question("What was the {1} symbol in {0}?", ThreeColumns6Answers, "§", "¶", "Ħ", "Ӕ", "ſ", "Ƕ", "Ƿ", "⁂", "ͼ", "ς", "Ћ", "₪", "Ю", "Ѡ", "Ѭ", "₰", "∯", "∫", "╩", "Ӭ", "☊", "Ҧ", "☦", "ﬡ", "ш", "Ω", "փ", "▒", "╋", "⌘", "∴", "∅", "℄", "Ҩ", "★", "ƛ", "Ϫ", "ت", "ټ", "غ", "ں", "þ", "Ɣ", "ȹ", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Symbols
}

public partial class SouvenirModule
{
    [Handler("UnicodeModule", "Unicode", typeof(SUnicode), "Espik")]
    [ManualQuestion("What were the symbols?")]
    private IEnumerator<SouvenirInstruction> ProcessUnicode(ModuleData module)
    {
        var comp = GetComponent(module, "UnicodeScript");

        var displayedSymbols = GetArrayField<TextMesh>(comp, "SymbolsScreen", isPublic: true).Get(expectedLength: 4);
        var selectedSymbols = new string[4];

        if (displayedSymbols.Count() != 4)
            throw new AbandonModuleException($"‘SymbolsScreen’ has unexpected length {displayedSymbols.Count()} (expected 4).");

        yield return WaitForActivate;

        for (var i = 0; i < 4; i++)
            selectedSymbols[i] = displayedSymbols[i].text;

        yield return WaitForSolve;

        for (var i = 0; i < 4; i++)
            yield return question(SUnicode.Symbols, args: [Ordinal(i + 1)]).Answers(selectedSymbols[i]);
    }
}

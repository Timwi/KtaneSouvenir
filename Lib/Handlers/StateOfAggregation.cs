using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SStateOfAggregation
{
    [SouvenirQuestion("What was the element shown in {0}?", ThreeColumns6Answers, "H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne", "Na", "Mg", "Al", "Si", "P", "S", "Cl", "Ar", "K", "Ca", "Sc", "Ti", "V", "Cr", "Mn", "Fe", "Co", "Ni", "Cu", "Zn", "Ga", "Ge", "As", "Se", "Br", "Kr", "Rb", "Sr", "Y", "Zr", "Nb", "Mo", "Tc", "Ru", "Rh", "Pd", "Ag", "Cd", "In", "Sn", "Sb", "Te", "I", "Xe", "Cs", "Ba", "La", "Ce", "Pr", "Nd", "Pm", "Sm", "Eu", "Gd", "Tb", "Dy", "Ho", "Er", "Tm", "Yb", "Lu", "Hf", "Ta", "W", "Re", "Os", "Ir", "Pt", "Au", "Hg", "Tl", "Pb", "Bi", "Po", "At", "Rn", "Fr", "Ra", "Ac", "Th", "Pa", "U", "Np", "Pu", "Am", "Cm", "Bk", "Cf", "Es", "Fm", "Md", "No", "Lr", "Rf", "Db", "Sg", "Bh", "Hs", "Mt", "Ds", "Rg", "Cn", "Nh", "Fl", "Mc", "Lv", "Ts", "Og")]
    Element
}

public partial class SouvenirModule
{
    [SouvenirHandler("stateOfAggregation", "State of Aggregation", typeof(SStateOfAggregation), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessStateOfAggregation(ModuleData module)
    {
        var comp = GetComponent(module, "StateOfAggregation");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var element = GetField<TextMesh>(comp, "Element", isPublic: true).Get().text;

        yield return WaitForSolve;

        // Convert to proper case.
        yield return question(SStateOfAggregation.Element).Answers(element.Substring(0, 1).ToUpperInvariant() + element.Substring(1).ToLowerInvariant());
    }
}
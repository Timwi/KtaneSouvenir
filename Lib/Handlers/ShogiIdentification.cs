using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SShogiIdentification
{
    [SouvenirQuestion("What was the displayed piece in {0}?", TwoColumns4Answers, "Go-Between", "Pawn", "Side Mover", "Vertical Mover", "Bishop", "Rook", "Dragon Horse", "Dragon King", "Lance", "Reverse Chariot", "Blind Tiger", "Ferocious Leopard", "Copper General", "Silver General", "Gold General", "Drunk Elephant", "Kirin", "Phoenix", "Queen", "Flying Stag", "Flying Ox", "Free Boar", "Whale", "White Horse", "King", "Prince", "Horned Falcon", "Soaring Eagle", "Lion", TranslateAnswers = true)]
    Piece
}

public partial class SouvenirModule
{
    [SouvenirHandler("shogiIdentification", "Shogi Identification", typeof(SShogiIdentification), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessShogiIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "ShogiIdentificationScript");
        yield return WaitForSolve;

        var fldPiece = GetField<object>(comp, "chosenPiece");
        var propName = GetProperty<string>(fldPiece.Get(), "name", isPublic: true);

        yield return question(SShogiIdentification.Piece).Answers(propName.Get());
    }
}
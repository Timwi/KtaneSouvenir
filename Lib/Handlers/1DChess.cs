using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum S1DChess
{
    [SouvenirQuestion("What was {1} in {0}?", ThreeColumns6Answers, "B a→c", "B a→e", "B a→g", "B a→i", "B a→k", "B b→d", "B b→f", "B b→h", "B b→j", "B c→a", "B c→e", "B c→g", "B c→i", "B c→k", "B d→b", "B d→f", "B d→h", "B d→j", "B e→a", "B e→c", "B e→g", "B e→i", "B e→k", "B f→b", "B f→d", "B f→h", "B f→j", "B g→a", "B g→c", "B g→e", "B g→i", "B g→k", "B h→b", "B h→d", "B h→f", "B h→j", "B i→a", "B i→c", "B i→e", "B i→g", "B i→k", "B j→b", "B j→d", "B j→f", "B j→h", "B k→a", "B k→c", "B k→e", "B k→g", "B k→i", "K a→b", "K b→a", "K b→c", "K c→b", "K c→d", "K d→c", "K d→e", "K e→d", "K e→f", "K f→e", "K f→g", "K g→f", "K g→h", "K h→g", "K h→i", "K i→h", "K i→j", "K j→i", "K j→k", "K k→j", "N a→c", "N b→d", "N c→a", "N c→e", "N d→b", "N d→f", "N e→c", "N e→g", "N f→d", "N f→h", "N g→e", "N g→i", "N h→f", "N h→j", "N i→g", "N i→k", "N j→h", "N k→i", "P a→b", "P a→c", "P b→a", "P b→c", "P b→d", "P c→a", "P c→b", "P c→d", "P c→e", "P d→b", "P d→c", "P d→e", "P d→f", "P e→c", "P e→d", "P e→f", "P e→g", "P f→d", "P f→e", "P f→g", "P f→h", "P g→e", "P g→f", "P g→h", "P g→i", "P h→f", "P h→g", "P h→i", "P h→j", "P i→g", "P i→h", "P i→j", "P i→k", "P j→h", "P j→i", "P j→k", "P k→i", "P k→j", "Q a→b", "Q a→c", "Q a→d", "Q a→e", "Q a→f", "Q a→g", "Q a→h", "Q a→i", "Q a→j", "Q a→k", "Q b→a", "Q b→c", "Q b→d", "Q b→e", "Q b→f", "Q b→g", "Q b→h", "Q b→i", "Q b→j", "Q b→k", "Q c→a", "Q c→b", "Q c→d", "Q c→e", "Q c→f", "Q c→g", "Q c→h", "Q c→i", "Q c→j", "Q c→k", "Q d→a", "Q d→b", "Q d→c", "Q d→e", "Q d→f", "Q d→g", "Q d→h", "Q d→i", "Q d→j", "Q d→k", "Q e→a", "Q e→b", "Q e→c", "Q e→d", "Q e→f", "Q e→g", "Q e→h", "Q e→i", "Q e→j", "Q e→k", "Q f→a", "Q f→b", "Q f→c", "Q f→d", "Q f→e", "Q f→g", "Q f→h", "Q f→i", "Q f→j", "Q f→k", "Q g→a", "Q g→b", "Q g→c", "Q g→d", "Q g→e", "Q g→f", "Q g→h", "Q g→i", "Q g→j", "Q g→k", "Q h→a", "Q h→b", "Q h→c", "Q h→d", "Q h→e", "Q h→f", "Q h→g", "Q h→i", "Q h→j", "Q h→k", "Q i→a", "Q i→b", "Q i→c", "Q i→d", "Q i→e", "Q i→f", "Q i→g", "Q i→h", "Q i→j", "Q i→k", "Q j→a", "Q j→b", "Q j→c", "Q j→d", "Q j→e", "Q j→f", "Q j→g", "Q j→h", "Q j→i", "Q j→k", "Q k→a", "Q k→b", "Q k→c", "Q k→d", "Q k→e", "Q k→f", "Q k→g", "Q k→h", "Q k→i", "Q k→j", "R a→b", "R a→d", "R a→f", "R a→h", "R a→j", "R b→a", "R b→c", "R b→e", "R b→g", "R b→i", "R b→k", "R c→b", "R c→d", "R c→f", "R c→h", "R c→j", "R d→a", "R d→c", "R d→e", "R d→g", "R d→i", "R d→k", "R e→b", "R e→d", "R e→f", "R e→h", "R e→j", "R f→a", "R f→c", "R f→e", "R f→g", "R f→i", "R f→k", "R g→b", "R g→d", "R g→f", "R g→h", "R g→j", "R h→a", "R h→c", "R h→e", "R h→g", "R h→i", "R h→k", "R i→b", "R i→d", "R i→f", "R i→h", "R i→j", "R j→a", "R j→c", "R j→e", "R j→g", "R j→i", "R j→k", "R k→b", "R k→d", "R k→f", "R k→h", "R k→j", Arguments = ["your first move", "Rustmate’s first move", "your second move", "Rustmate’s second move", "your third move", "Rustmate’s third move", "your fourth move", "Rustmate’s fourth move", "your fifth move", "Rustmate’s fifth move", "your sixth move", "Rustmate’s sixth move", "your seventh move", "Rustmate’s seventh move", "your eighth move", "Rustmate’s eighth move"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Moves,

    [SouvenirDiscriminator("the 1D Chess where {1} was {0}", Arguments = ["B a→c", "your first move", "B a→e", "Rustmate’s first move", "B a→g", "your second move", "B a→i", "Rustmate’s second move", "B a→k", "your third move", "B b→d", "Rustmate’s third move", "B b→f", "your fourth move", "B b→h", "Rustmate’s fourth move", "B b→j", "your fifth move", "B c→a", "Rustmate’s fifth move", "B c→e", "your sixth move", "B c→g", "Rustmate’s sixth move", "B c→i", "your seventh move", "B c→k", "Rustmate’s seventh move", "B d→b", "your eighth move", "B d→f", "Rustmate’s eighth move"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("1DChess", "1D Chess", typeof(S1DChess), "Emik")]
    [SouvenirManualQuestion("What were your and Rustmate’s moves?")]
    private IEnumerator<SouvenirInstruction> Process1DChess(ModuleData module)
    {
        var comp = GetComponent(module, "OneDimensionalChessScript");
        yield return WaitForSolve;

        var moves = GetListField<string>(comp, "souvenirPositions").Get().Select(move => Regex.Replace(move, @"^\[|\]$", "")).ToArray();
        var strings = new[] { "your first move", "Rustmate’s first move", "your second move", "Rustmate’s second move", "your third move", "Rustmate’s third move", "your fourth move", "Rustmate’s fourth move", "your fifth move", "Rustmate’s fifth move", "your sixth move", "Rustmate’s sixth move", "your seventh move", "Rustmate’s seventh move", "your eighth move", "Rustmate’s eighth move" };
        for (var ix = 0; ix < moves.Length; ix++)
        {
            yield return new Discriminator(S1DChess.Discriminator, $"stage{ix}", moves[ix], args: [moves[ix], strings[ix]]);
            yield return question(S1DChess.Moves, args: [strings[ix]]).AvoidDiscriminators($"stage{ix}").Answers(moves[ix]);
        }
    }
}

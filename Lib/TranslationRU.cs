using System;
using System.Collections.Generic;
using static Souvenir.Translation_ru.Conjugation;

namespace Souvenir;

public class Translation_ru : TranslationBase<Translation_ru.TranslationInfo_ru>
{
    public sealed class TranslationInfo_ru : TranslationInfo
    {
        public Conjugation Conjugation = в_PrepositiveMascNeuter;
    }

    public enum Conjugation
    {
        // The preposition в is automatically added in front of the module name, so omit it from the question text
        в_PrepositiveMascNeuter,
        в_PrepositiveFeminine,
        в_PrepositivePlural,

        // The preposition во is automatically added in front of the module name, so omit it from the question text
        во_PrepositiveMascNeuter,
        во_PrepositiveFeminine,
        во_PrepositivePlural,

        // No preposition is automatically added in front of the module name, so include it in the question text
        PrepositiveMascNeuter,
        PrepositiveFeminine,
        PrepositivePlural,
        NominativeMasculine,
        NominativeNeuter,
        NominativeFeminine,
        NominativePlural,
        GenitiveMascNeuter,
        GenitiveFeminine,
        GenitivePlural,
        AccusativeMascNeuter,
        AccusativeFeminine,
        AccusativePlural,
        InstrumentalMascNeuter,
        InstrumentalFeminine,
        InstrumentalPlural,
        DativeMascNeuter,
        DativeFeminine,
        DativePlural,
    }

    public override string FormatModuleName(SouvenirHandlerAttribute handler, bool addSolveCount, int numSolved) =>
        _translations.Get(handler.EnumType) is not TranslationInfo_ru tr ? base.FormatModuleName(handler, addSolveCount, numSolved) :
        addSolveCount ? tr.Conjugation switch
        {
            NominativeMasculine => $"{Ordinal(numSolved)}-й решённый {tr.ModuleName}",
            NominativeFeminine => $"{Ordinal(numSolved)}-я решённая {tr.ModuleName}",
            NominativeNeuter => $"{Ordinal(numSolved)}-е решённое {tr.ModuleName}",
            NominativePlural => $"{Ordinal(numSolved)}-е решённые {tr.ModuleName}",
            GenitiveMascNeuter => $"{Ordinal(numSolved)}-го решённого {tr.ModuleName}",
            GenitiveFeminine => $"{Ordinal(numSolved)}-й решённой {tr.ModuleName}",
            GenitivePlural => $"{Ordinal(numSolved)}-х решённых {tr.ModuleName}",
            PrepositiveMascNeuter => $"{Ordinal(numSolved)}-м решённом {tr.ModuleName}",
            PrepositiveFeminine => $"{Ordinal(numSolved)}-й решённой {tr.ModuleName}",
            PrepositivePlural => $"{Ordinal(numSolved)}-х решённых {tr.ModuleName}",
            InstrumentalMascNeuter => $"{Ordinal(numSolved)}-м решённым {tr.ModuleName}",
            InstrumentalFeminine => $"{Ordinal(numSolved)}-й решённой {tr.ModuleName}",
            InstrumentalPlural => $"{Ordinal(numSolved)}-ми решёнными {tr.ModuleName}",
            DativeMascNeuter => $"{Ordinal(numSolved)}-му решённому {tr.ModuleName}",
            DativeFeminine => $"{Ordinal(numSolved)}-й решённой {tr.ModuleName}",
            DativePlural => $"{Ordinal(numSolved)}-м решённым {tr.ModuleName}",
            в_PrepositiveMascNeuter or во_PrepositiveMascNeuter => $"{(numSolved == 2 ? "во" : "в")} {Ordinal(numSolved)}-м решённом {tr.ModuleName}",
            в_PrepositiveFeminine or во_PrepositiveFeminine => $"{(numSolved == 2 ? "во" : "в")} {Ordinal(numSolved)}-й решённой {tr.ModuleName}",
            в_PrepositivePlural or во_PrepositivePlural => $"{(numSolved == 2 ? "во" : "в")} {Ordinal(numSolved)}-х решённых {tr.ModuleName}",
            _ => throw new InvalidOperationException($"Unknown conjugation: {tr.Conjugation}")
        } :
        tr.Conjugation switch
        {
            в_PrepositiveMascNeuter or в_PrepositiveFeminine or в_PrepositivePlural => $"в {tr.ModuleName}",
            во_PrepositiveMascNeuter or во_PrepositiveFeminine or во_PrepositivePlural => $"во {tr.ModuleName}",
            _ => tr.ModuleName,
        };

    public override string Ordinal(int number) => number.ToString();

    protected override Dictionary<Type, TranslationInfo_ru> _translations => new()
    {
        #region Translatable strings
        [typeof(S0)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S0.Number] = new()
                {
                    // English: What was the initially displayed number in {0}?
                    Question = "What was the initially displayed number in {0}?",
                },
            },
        },

        [typeof(S1000Words)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S1000Words.Words] = new()
                {
                    // English: What was the {1} word shown in {0}?
                    // Example: What was the first word shown in 1000 Words?
                    Question = "What was the {1} word shown in {0}?",
                },
            },
        },

        [typeof(S100LevelsOfDefusal)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S100LevelsOfDefusal.Letters] = new()
                {
                    // English: What was the {1} displayed letter in {0}?
                    // Example: What was the first displayed letter in 100 Levels of Defusal?
                    Question = "What was the {1} displayed letter in {0}?",
                },
            },
        },

        [typeof(S123Game)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S123Game.Profile] = new()
                {
                    // English: Who was the opponent in {0}?
                    Question = "Who was the opponent in {0}?",
                },
                [S123Game.Name] = new()
                {
                    // English: Who was the opponent in {0}?
                    Question = "Who was the opponent in {0}?",
                },
            },
        },

        [typeof(S1DChess)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S1DChess.Moves] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was your first move in 1D Chess?
                    Question = "What was {1} in {0}?",
                    Arguments = new()
                    {
                        ["your first move"] = "your first move",
                        ["Rustmate’s first move"] = "Rustmate’s first move",
                        ["your second move"] = "your second move",
                        ["Rustmate’s second move"] = "Rustmate’s second move",
                        ["your third move"] = "your third move",
                        ["Rustmate’s third move"] = "Rustmate’s third move",
                        ["your fourth move"] = "your fourth move",
                        ["Rustmate’s fourth move"] = "Rustmate’s fourth move",
                        ["your fifth move"] = "your fifth move",
                        ["Rustmate’s fifth move"] = "Rustmate’s fifth move",
                        ["your sixth move"] = "your sixth move",
                        ["Rustmate’s sixth move"] = "Rustmate’s sixth move",
                        ["your seventh move"] = "your seventh move",
                        ["Rustmate’s seventh move"] = "Rustmate’s seventh move",
                        ["your eighth move"] = "your eighth move",
                        ["Rustmate’s eighth move"] = "Rustmate’s eighth move",
                    },
                },
            },
        },

        [typeof(S21)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S21.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "What was the displayed number in {0}?",
                },
            },
        },

        [typeof(S3DMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S3DMaze.Markings] = new()
                {
                    // English: What were the markings in {0}?
                    Question = "What were the markings in {0}?",
                },
                [S3DMaze.Bearing] = new()
                {
                    // English: What was the cardinal direction in {0}?
                    Question = "What was the cardinal direction in {0}?",
                },
            },
        },

        [typeof(S3DTapCode)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S3DTapCode.Word] = new()
                {
                    // English: What was the received word in {0}?
                    Question = "What was the received word in {0}?",
                },
            },
        },

        [typeof(S3DTunnels)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S3DTunnels.TargetNode] = new()
                {
                    // English: What was the {1} goal node in {0}?
                    // Example: What was the first goal node in 3D Tunnels?
                    Question = "What was the {1} goal node in {0}?",
                },
            },
        },

        [typeof(S3LEDs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S3LEDs.InitialState] = new()
                {
                    // English: What was the initial state of the LEDs in {0} (in reading order)?
                    Question = "What was the initial state of the LEDs in {0} (in reading order)?",
                },
            },
        },

        [typeof(S3NPlus1)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S3NPlus1.Question] = new()
                {
                    // English: What number was initially displayed in {0}?
                    Question = "What number was initially displayed in {0}?",
                },
            },
        },

        [typeof(S64)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S64.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "What was the displayed number in {0}?",
                },
            },
        },

        [typeof(S7)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S7.InitialValues] = new()
                {
                    // English: What was the {1} channel’s initial value in {0}?
                    // Example: What was the red channel’s initial value in 7?
                    Question = "What was the {1} channel’s initial value in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
                [S7.LedColors] = new()
                {
                    // English: What LED color was shown in stage {1} of {0}?
                    // Example: What LED color was shown in stage 0 of 7?
                    Question = "What LED color was shown in stage {1} of {0}?",
                },
            },
        },

        [typeof(S9Ball)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [S9Ball.Letters] = new()
                {
                    // English: What was the number of ball {1} in {0}?
                    // Example: What was the number of ball A in 9-Ball?
                    Question = "What was the number of ball {1} in {0}?",
                },
                [S9Ball.Numbers] = new()
                {
                    // English: What was the letter of ball {1} in {0}?
                    // Example: What was the letter of ball 2 in 9-Ball?
                    Question = "What was the letter of ball {1} in {0}?",
                },
            },
        },

        [typeof(SAbyss)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAbyss.Seed] = new()
                {
                    // English: What was the {1} character displayed on {0}?
                    // Example: What was the first character displayed on Abyss?
                    Question = "What was the {1} character displayed on {0}?",
                },
            },
        },

        [typeof(SAccumulation)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAccumulation.BorderColor] = new()
                {
                    // English: What was the border color in {0}?
                    Question = "What was the border color in {0}?",
                },
                [SAccumulation.BackgroundColor] = new()
                {
                    // English: What was the background color on the {1} stage in {0}?
                    // Example: What was the background color on the first stage in Accumulation?
                    Question = "What was the background color on the {1} stage in {0}?",
                },
            },
        },

        [typeof(SAdventureGame)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAdventureGame.CorrectItem] = new()
                {
                    // English: Which item was the {1} correct item you used in {0}?
                    // Example: Which item was the first correct item you used in Adventure Game?
                    Question = "Which item was the {1} correct item you used in {0}?",
                },
                [SAdventureGame.Enemy] = new()
                {
                    // English: What enemy were you fighting in {0}?
                    Question = "What enemy were you fighting in {0}?",
                },
            },
        },

        [typeof(SAffineCycle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAffineCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Affine Cycle?
                    Question = "Which direction was the {1} dial pointing in {0}?",
                },
                [SAffineCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Affine Cycle?
                    Question = "What letter was written on the {1} dial in {0}?",
                },
            },
        },

        [typeof(SAlcoholicRampage)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAlcoholicRampage.Mercenaries] = new()
                {
                    // English: Who was the {1} mercenary displayed in {0}?
                    // Example: Who was the first mercenary displayed in Alcoholic Rampage?
                    Question = "Who was the {1} mercenary displayed in {0}?",
                },
            },
        },

        [typeof(SALetter)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SALetter.InitialLetter] = new()
                {
                    // English: What was the initial letter in {0}?
                    Question = "What was the initial letter in {0}?",
                },
            },
        },

        [typeof(SAlfaBravo)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAlfaBravo.PressedLetter] = new()
                {
                    // English: Which letter was pressed in {0}?
                    Question = "Which letter was pressed in {0}?",
                },
                [SAlfaBravo.LeftPressedLetter] = new()
                {
                    // English: Which letter was to the left of the pressed one in {0}?
                    Question = "Which letter was to the left of the pressed one in {0}?",
                },
                [SAlfaBravo.RightPressedLetter] = new()
                {
                    // English: Which letter was to the right of the pressed one in {0}?
                    Question = "Which letter was to the right of the pressed one in {0}?",
                },
                [SAlfaBravo.Digit] = new()
                {
                    // English: What was the last digit on the small display in {0}?
                    Question = "What was the last digit on the small display in {0}?",
                },
            },
        },

        [typeof(SAlgebra)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAlgebra.Equation1] = new()
                {
                    // English: What was the first equation in {0}?
                    Question = "What was the first equation in {0}?",
                },
                [SAlgebra.Equation2] = new()
                {
                    // English: What was the second equation in {0}?
                    Question = "What was the second equation in {0}?",
                },
            },
        },

        [typeof(SAlgorithmia)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAlgorithmia.Positions] = new()
                {
                    // English: Which position was the {1} position in {0}?
                    // Example: Which position was the starting position in Algorithmia?
                    Question = "Which position was the {1} position in {0}?",
                    Arguments = new()
                    {
                        ["starting"] = "starting",
                        ["goal"] = "goal",
                    },
                },
                [SAlgorithmia.Color] = new()
                {
                    // English: What was the color of the colored bulb in {0}?
                    Question = "What was the color of the colored bulb in {0}?",
                },
                [SAlgorithmia.Seed] = new()
                {
                    // English: Which number was present in the seed in {0}?
                    Question = "Which number was present in the seed in {0}?",
                },
            },
        },

        [typeof(SAlphabeticalRuling)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAlphabeticalRuling.Letter] = new()
                {
                    // English: What was the letter displayed in the {1} stage of {0}?
                    // Example: What was the letter displayed in the first stage of Alphabetical Ruling?
                    Question = "What was the letter displayed in the {1} stage of {0}?",
                },
                [SAlphabeticalRuling.Number] = new()
                {
                    // English: What was the number displayed in the {1} stage of {0}?
                    // Example: What was the number displayed in the first stage of Alphabetical Ruling?
                    Question = "What was the number displayed in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SAlphabetNumbers)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAlphabetNumbers.DisplayedNumbers] = new()
                {
                    // English: Which of these numbers was on one of the buttons in the {1} stage of {0}?
                    // Example: Which of these numbers was on one of the buttons in the first stage of Alphabet Numbers?
                    Question = "Which of these numbers was on one of the buttons in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SAlphabetTiles)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAlphabetTiles.Cycle] = new()
                {
                    // English: What was the {1} letter shown during the cycle in {0}?
                    // Example: What was the first letter shown during the cycle in Alphabet Tiles?
                    Question = "What was the {1} letter shown during the cycle in {0}?",
                },
                [SAlphabetTiles.MissingLetter] = new()
                {
                    // English: What was the missing letter in {0}?
                    Question = "What was the missing letter in {0}?",
                },
            },
        },

        [typeof(SAlphaBits)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAlphaBits.DisplayedCharacters] = new()
                {
                    // English: What character was displayed on the {1} screen on the {2} in {0}?
                    // Example: What character was displayed on the first screen on the left in Alpha-Bits?
                    Question = "What character was displayed on the {1} screen on the {2} in {0}?",
                    Arguments = new()
                    {
                        ["left"] = "left",
                        ["right"] = "right",
                    },
                },
            },
        },

        [typeof(SAMessage)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAMessage.AMessage] = new()
                {
                    // English: What was the initial message in {0}?
                    Question = "What was the initial message in {0}?",
                },
            },
        },

        [typeof(SAmusementParks)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAmusementParks.Rides] = new()
                {
                    // English: Which ride was available in {0}?
                    Question = "Which ride was available in {0}?",
                },
            },
        },

        [typeof(SAngelHernandez)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAngelHernandez.MainLetter] = new()
                {
                    // English: What letter was shown by the raised buttons on the {1} stage on {0}?
                    // Example: What letter was shown by the raised buttons on the first stage on Ángel Hernández?
                    Question = "What letter was shown by the raised buttons on the {1} stage on {0}?",
                },
            },
        },

        [typeof(SArena)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SArena.Damage] = new()
                {
                    // English: What was the maximum weapon damage of the attack phase in {0}?
                    Question = "What was the maximum weapon damage of the attack phase in {0}?",
                },
                [SArena.Enemies] = new()
                {
                    // English: Which enemy was present in the defend phase of {0}?
                    Question = "Which enemy was present in the defend phase of {0}?",
                },
                [SArena.Numbers] = new()
                {
                    // English: Which was a number present in the grab phase of {0}?
                    Question = "Which was a number present in the grab phase of {0}?",
                },
            },
        },

        [typeof(SArithmelogic)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SArithmelogic.Submit] = new()
                {
                    // English: What was the symbol on the submit button in {0}?
                    Question = "What was the symbol on the submit button in {0}?",
                },
                [SArithmelogic.Numbers] = new()
                {
                    // English: Which number was selectable, but not the solution, in the {1} screen on {0}?
                    // Example: Which number was selectable, but not the solution, in the left screen on Arithmelogic?
                    Question = "Which number was selectable, but not the solution, in the {1} screen on {0}?",
                    Arguments = new()
                    {
                        ["left"] = "left",
                        ["middle"] = "middle",
                        ["right"] = "right",
                    },
                },
            },
        },

        [typeof(SASCIIMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SASCIIMaze.Characters] = new()
                {
                    // English: What was the {1} character displayed on {0}?
                    // Example: What was the first character displayed on ASCII Maze?
                    Question = "What was the {1} character displayed on {0}?",
                },
            },
        },

        [typeof(SASquare)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SASquare.IndexColors] = new()
                {
                    // English: Which of these was an index color in {0}?
                    Question = "Which of these was an index color in {0}?",
                },
                [SASquare.CorrectColors] = new()
                {
                    // English: Which color was submitted {1} in {0}?
                    // Example: Which color was submitted first in A Square?
                    Question = "Which color was submitted {1} in {0}?",
                },
            },
        },

        [typeof(SAudioMorse)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAudioMorse.Sound] = new()
                {
                    // English: What was signaled in {0}?
                    Question = "What was signaled in {0}?",
                },
            },
        },

        [typeof(SAzureButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SAzureButton.QDecoyArrowDirection] = new()
                {
                    // English: What was the {1} direction in the decoy arrow in {0}?
                    // Example: What was the first direction in the decoy arrow in Azure Button?
                    Question = "What was the {1} direction in the decoy arrow in {0}?",
                },
                [SAzureButton.QNonDecoyArrowDirection] = new()
                {
                    // English: What was the {1} direction in the {2} non-decoy arrow in {0}?
                    // Example: What was the first direction in the first non-decoy arrow in Azure Button?
                    Question = "What was the {1} direction in the {2} non-decoy arrow in {0}?",
                },
                [SAzureButton.QT] = new()
                {
                    // English: What was T in {0}?
                    Question = "What was T in {0}?",
                },
                [SAzureButton.QNotT] = new()
                {
                    // English: Which of these cards was shown in Stage 1, but not T, in {0}?
                    Question = "Which of these cards was shown in Stage 1, but not T, in {0}?",
                },
                [SAzureButton.QM] = new()
                {
                    // English: What was M in {0}?
                    Question = "What was M in {0}?",
                },
            },
            Discriminators = new()
            {
                [SAzureButton.DCard] = new()
                {
                    // English: the Azure Button that had this card in Stage 1
                    Discriminator = "the Azure Button that had this card in Stage 1",
                },
                [SAzureButton.DM] = new()
                {
                    // English: the Azure Button where M was {0}
                    // Example: the Azure Button where M was 1
                    Discriminator = "the Azure Button where M was {0}",
                },
                [SAzureButton.DDecoyArrowDirection] = new()
                {
                    // English: the Azure Button where the decoy arrow went {0} at some point
                    // Example: the Azure Button where the decoy arrow went north at some point
                    Discriminator = "the Azure Button where the decoy arrow went {0} at some point",
                },
                [SAzureButton.DNonDecoyArrowDirection] = new()
                {
                    // English: the Azure Button where the {1} non-decoy arrow went {0} at some point
                    // Example: the Azure Button where the first non-decoy arrow went north at some point
                    Discriminator = "the Azure Button where the {1} non-decoy arrow went {0} at some point",
                },
            },
        },

        [typeof(SBakery)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBakery.Items] = new()
                {
                    // English: Which menu item was present in {0}?
                    Question = "Which menu item was present in {0}?",
                },
            },
        },

        [typeof(SBamboozledAgain)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBamboozledAgain.ButtonText] = new()
                {
                    // English: What was the text on the {1} correct button in {0}?
                    // Example: What was the text on the first correct button in Bamboozled Again?
                    Question = "What was the text on the {1} correct button in {0}?",
                },
                [SBamboozledAgain.ButtonColor] = new()
                {
                    // English: What color was the {1} correct button in {0}?
                    // Example: What color was the first correct button in Bamboozled Again?
                    Question = "What color was the {1} correct button in {0}?",
                },
                [SBamboozledAgain.DisplayTexts1] = new()
                {
                    // English: What was the {1} decrypted text on the display in {0}?
                    // Example: What was the first decrypted text on the display in Bamboozled Again?
                    Question = "What was the {1} decrypted text on the display in {0}?",
                },
                [SBamboozledAgain.DisplayTexts2] = new()
                {
                    // English: What was the {1} decrypted text on the display in {0}?
                    // Example: What was the first decrypted text on the display in Bamboozled Again?
                    Question = "What was the {1} decrypted text on the display in {0}?",
                },
                [SBamboozledAgain.DisplayColor] = new()
                {
                    // English: What color was the {1} text on the display in {0}?
                    // Example: What color was the first text on the display in Bamboozled Again?
                    Question = "What color was the {1} text on the display in {0}?",
                },
            },
        },

        [typeof(SBamboozlingButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBamboozlingButton.Color] = new()
                {
                    // English: What color was the button in the {1} stage of {0}?
                    // Example: What color was the button in the first stage of Bamboozling Button?
                    Question = "What color was the button in the {1} stage of {0}?",
                },
                [SBamboozlingButton.DisplayColor] = new()
                {
                    // English: What was the color of the {2} display in the {1} stage of {0}?
                    // Example: What was the color of the first display in the first stage of Bamboozling Button?
                    Question = "What was the color of the {2} display in the {1} stage of {0}?",
                },
                [SBamboozlingButton.Display] = new()
                {
                    // English: What was the {2} display in the {1} stage of {0}?
                    // Example: What was the first display in the first stage of Bamboozling Button?
                    Question = "What was the {2} display in the {1} stage of {0}?",
                },
                [SBamboozlingButton.Label] = new()
                {
                    // English: What was the {2} label on the button in the {1} stage of {0}?
                    // Example: What was the top label on the button in the first stage of Bamboozling Button?
                    Question = "What was the {2} label on the button in the {1} stage of {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SBarCharts)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBarCharts.Category] = new()
                {
                    // English: What was the category of {0}?
                    Question = "What was the category of {0}?",
                },
                [SBarCharts.Unit] = new()
                {
                    // English: What was the unit of {0}?
                    Question = "What was the unit of {0}?",
                },
                [SBarCharts.Label] = new()
                {
                    // English: What was the label of the {1} bar in {0}?
                    // Example: What was the label of the first bar in Bar Charts?
                    Question = "What was the label of the {1} bar in {0}?",
                },
                [SBarCharts.Color] = new()
                {
                    // English: What was the color of the {1} bar in {0}?
                    // Example: What was the color of the first bar in Bar Charts?
                    Question = "What was the color of the {1} bar in {0}?",
                },
                [SBarCharts.Height] = new()
                {
                    // English: What was the position of the {1} bar in {0}?
                    // Example: What was the position of the shortest bar in Bar Charts?
                    Question = "What was the position of the {1} bar in {0}?",
                    Arguments = new()
                    {
                        ["shortest"] = "shortest",
                        ["second shortest"] = "second shortest",
                        ["second tallest"] = "second tallest",
                        ["tallest"] = "tallest",
                    },
                },
            },
        },

        [typeof(SBarcodeCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBarcodeCipher.ScreenNumber] = new()
                {
                    // English: What was the screen number in {0}?
                    Question = "What was the screen number in {0}?",
                },
                [SBarcodeCipher.BarcodeEdgework] = new()
                {
                    // English: What was the edgework represented by the {1} barcode in {0}?
                    // Example: What was the edgework represented by the first barcode in Barcode Cipher?
                    Question = "What was the edgework represented by the {1} barcode in {0}?",
                },
                [SBarcodeCipher.BarcodeAnswers] = new()
                {
                    // English: What was the answer for the {1} barcode in {0}?
                    // Example: What was the answer for the first barcode in Barcode Cipher?
                    Question = "What was the answer for the {1} barcode in {0}?",
                },
            },
        },

        [typeof(SBartending)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBartending.Ingredients] = new()
                {
                    // English: Which ingredient was in the {1} position on {0}?
                    // Example: Which ingredient was in the first position on Bartending?
                    Question = "Which ingredient was in the {1} position on {0}?",
                },
            },
        },

        [typeof(SBeans)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBeans.Colors] = new()
                {
                    // English: What was this bean in {0}?
                    Question = "What was this bean in {0}?",
                },
            },
        },

        [typeof(SBeanSprouts)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBeanSprouts.Colors] = new()
                {
                    // English: What was sprout {1} in {0}?
                    // Example: What was sprout 1 in Bean Sprouts?
                    Question = "What was sprout {1} in {0}?",
                },
                [SBeanSprouts.Beans] = new()
                {
                    // English: What bean was on sprout {1} in {0}?
                    // Example: What bean was on sprout 1 in Bean Sprouts?
                    Question = "What bean was on sprout {1} in {0}?",
                },
            },
        },

        [typeof(SBigBean)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBigBean.Color] = new()
                {
                    // English: What was the bean in {0}?
                    Question = "What was the bean in {0}?",
                },
            },
        },

        [typeof(SBigCircle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBigCircle.Colors] = new()
                {
                    // English: What color was {1} in the solution to {0}?
                    // Example: What color was first in the solution to Big Circle?
                    Question = "What color was {1} in the solution to {0}?",
                },
            },
        },

        [typeof(SBinary)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBinary.Word] = new()
                {
                    // English: What word was displayed in {0}?
                    Question = "What word was displayed in {0}?",
                },
            },
        },

        [typeof(SBinaryLEDs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBinaryLEDs.Value] = new()
                {
                    // English: At which numeric value did you cut the correct wire in {0}?
                    Question = "At which numeric value did you cut the correct wire in {0}?",
                },
            },
        },

        [typeof(SBinaryShift)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBinaryShift.InitialNumber] = new()
                {
                    // English: What was the {1} initial number in {0}?
                    // Example: What was the top-left initial number in Binary Shift?
                    Question = "What was the {1} initial number in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-middle"] = "top-middle",
                        ["top-right"] = "top-right",
                        ["left-middle"] = "left-middle",
                        ["center"] = "center",
                        ["right-middle"] = "right-middle",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-middle"] = "bottom-middle",
                        ["bottom-right"] = "bottom-right",
                    },
                },
                [SBinaryShift.SelectedNumberPossition] = new()
                {
                    // English: What number was selected at stage {1} in {0}?
                    // Example: What number was selected at stage 0 in Binary Shift?
                    Question = "What number was selected at stage {1} in {0}?",
                },
                [SBinaryShift.NotSelectedNumberPossition] = new()
                {
                    // English: What number was not selected at stage {1} in {0}?
                    // Example: What number was not selected at stage 0 in Binary Shift?
                    Question = "What number was not selected at stage {1} in {0}?",
                },
            },
        },

        [typeof(SBitmaps)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBitmaps.Question] = new()
                {
                    // English: How many pixels were {1} in the {2} quadrant in {0}?
                    // Example: How many pixels were white in the top left quadrant in Bitmaps?
                    Question = "How many pixels were {1} in the {2} quadrant in {0}?",
                    Arguments = new()
                    {
                        ["white"] = "white",
                        ["top left"] = "top left",
                        ["top right"] = "top right",
                        ["bottom left"] = "bottom left",
                        ["bottom right"] = "bottom right",
                        ["black"] = "black",
                    },
                },
            },
        },

        [typeof(SBlackCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBlackCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Black Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SBlindfoldedYahtzee)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBlindfoldedYahtzee.Claim] = new()
                {
                    // English: What roll did the module claim in the {1} stage of {0}?
                    // Example: What roll did the module claim in the first stage of Blindfolded Yahtzee?
                    Question = "What roll did the module claim in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SBlindMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBlindMaze.Colors] = new()
                {
                    // English: What color was the {1} button in {0}?
                    // Example: What color was the north button in Blind Maze?
                    Question = "What color was the {1} button in {0}?",
                    Arguments = new()
                    {
                        ["north"] = "north",
                        ["east"] = "east",
                        ["west"] = "west",
                        ["south"] = "south",
                    },
                },
                [SBlindMaze.Maze] = new()
                {
                    // English: Which maze did you solve {0} on?
                    Question = "Which maze did you solve {0} on?",
                },
            },
        },

        [typeof(SBlinkingNotes)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBlinkingNotes.Song] = new()
                {
                    // English: What song was flashed in {0}?
                    Question = "What song was flashed in {0}?",
                },
            },
        },

        [typeof(SBlinkstop)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBlinkstop.NumberOfFlashes] = new()
                {
                    // English: How many times did the LED flash in {0}?
                    Question = "How many times did the LED flash in {0}?",
                },
                [SBlinkstop.FewestFlashedColor] = new()
                {
                    // English: Which color did the LED flash the fewest times in {0}?
                    Question = "Which color did the LED flash the fewest times in {0}?",
                },
            },
        },

        [typeof(SBlockbusters)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBlockbusters.LastLetter] = new()
                {
                    // English: What was the last letter pressed on {0}?
                    Question = "What was the last letter pressed on {0}?",
                },
            },
        },

        [typeof(SBlueArrows)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBlueArrows.InitialCharacters] = new()
                {
                    // English: What were the characters on the screen in {0}?
                    Question = "What were the characters on the screen in {0}?",
                },
            },
        },

        [typeof(SBlueButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBlueButton.D] = new()
                {
                    // English: What was D in {0}?
                    Question = "What was D in {0}?",
                },
                [SBlueButton.EFGH] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was E in Blue Button?
                    Question = "What was {1} in {0}?",
                },
                [SBlueButton.M] = new()
                {
                    // English: What was M in {0}?
                    Question = "What was M in {0}?",
                },
                [SBlueButton.N] = new()
                {
                    // English: What was N in {0}?
                    Question = "What was N in {0}?",
                },
                [SBlueButton.P] = new()
                {
                    // English: What was P in {0}?
                    Question = "What was P in {0}?",
                },
                [SBlueButton.Q] = new()
                {
                    // English: What was Q in {0}?
                    Question = "What was Q in {0}?",
                },
                [SBlueButton.X] = new()
                {
                    // English: What was X in {0}?
                    Question = "What was X in {0}?",
                },
            },
            Discriminators = new()
            {
                [SBlueButton.DQ] = new()
                {
                    // English: the Blue Button where Q was {0}
                    // Example: the Blue Button where Q was Blue
                    Discriminator = "the Blue Button where Q was {0}",
                    Arguments = new()
                    {
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Cyan"] = "Cyan",
                        ["Red"] = "Red",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                    },
                },
                [SBlueButton.DOther] = new()
                {
                    // English: the Blue Button where {0} was {1}
                    Discriminator = "the Blue Button where {0} was {1}",
                },
            },
        },

        [typeof(SBlueCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBlueCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Blue Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SBobBarks)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBobBarks.Indicators] = new()
                {
                    // English: What was the {1} indicator label in {0}?
                    // Example: What was the top left indicator label in Bob Barks?
                    Question = "What was the {1} indicator label in {0}?",
                    Arguments = new()
                    {
                        ["top left"] = "top left",
                        ["top right"] = "top right",
                        ["bottom left"] = "bottom left",
                        ["bottom right"] = "bottom right",
                    },
                },
                [SBobBarks.Positions] = new()
                {
                    // English: Which button flashed {1} in sequence in {0}?
                    // Example: Which button flashed first in sequence in Bob Barks?
                    Question = "Which button flashed {1} in sequence in {0}?",
                },
            },
        },

        [typeof(SBoggle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBoggle.Letters] = new()
                {
                    // English: What letter was initially visible on {0}?
                    Question = "What letter was initially visible on {0}?",
                },
            },
        },

        [typeof(SBombDiffusal)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBombDiffusal.LicenseNumber] = new()
                {
                    // English: What was the license number in {0}?
                    Question = "What was the license number in {0}?",
                },
            },
        },

        [typeof(SBoneAppleTea)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBoneAppleTea.Phrase] = new()
                {
                    // English: Which phrase was shown on {0}?
                    Question = "Which phrase was shown on {0}?",
                },
            },
        },

        [typeof(SBoobTube)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBoobTube.Word] = new()
                {
                    // English: Which word was shown on {0}?
                    Question = "Which word was shown on {0}?",
                },
            },
        },

        [typeof(SBookOfMario)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBookOfMario.Pictures] = new()
                {
                    // English: Who said the {1} quote in {0}?
                    // Example: Who said the first quote in Book of Mario?
                    Question = "Who said the {1} quote in {0}?",
                },
                [SBookOfMario.Quotes] = new()
                {
                    // English: What did {1} say in the {2} stage of {0}?
                    // Example: What did Goombell say in the first stage of Book of Mario?
                    Question = "What did {1} say in the {2} stage of {0}?",
                },
            },
        },

        [typeof(SBooleanWires)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBooleanWires.EnteredOperators] = new()
                {
                    // English: Which operator did you submit in the {1} stage of {0}?
                    // Example: Which operator did you submit in the first stage of Boolean Wires?
                    Question = "Which operator did you submit in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SBoomtarTheGreat)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBoomtarTheGreat.Rules] = new()
                {
                    // English: What was rule {1} in {0}?
                    // Example: What was rule one in Boomtar the Great?
                    Question = "What was rule {1} in {0}?",
                    Arguments = new()
                    {
                        ["one"] = "one",
                        ["two"] = "two",
                    },
                },
            },
        },

        [typeof(SBorderedKeys)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBorderedKeys.BorderColor] = new()
                {
                    // English: What was the {1} key’s border color when it was pressed in {0}?
                    // Example: What was the first key’s border color when it was pressed in Bordered Keys?
                    Question = "What was the {1} key’s border color when it was pressed in {0}?",
                },
                [SBorderedKeys.Digit] = new()
                {
                    // English: What was the digit displayed when the {1} key was pressed in {0}?
                    // Example: What was the digit displayed when the first key was pressed in Bordered Keys?
                    Question = "What was the digit displayed when the {1} key was pressed in {0}?",
                },
                [SBorderedKeys.KeyColor] = new()
                {
                    // English: What was the {1} key’s key color when it was pressed in {0}?
                    // Example: What was the first key’s key color when it was pressed in Bordered Keys?
                    Question = "What was the {1} key’s key color when it was pressed in {0}?",
                },
                [SBorderedKeys.Label] = new()
                {
                    // English: What was the {1} key’s label when it was pressed in {0}?
                    // Example: What was the first key’s label when it was pressed in Bordered Keys?
                    Question = "What was the {1} key’s label when it was pressed in {0}?",
                },
                [SBorderedKeys.LabelColor] = new()
                {
                    // English: What was the {1} key’s label color when it was pressed in {0}?
                    // Example: What was the first key’s label color when it was pressed in Bordered Keys?
                    Question = "What was the {1} key’s label color when it was pressed in {0}?",
                },
            },
        },

        [typeof(SBottomGear)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBottomGear.Tweet] = new()
                {
                    // English: What tweet was shown in {0}?
                    Question = "What tweet was shown in {0}?",
                },
            },
        },

        [typeof(SBoxing)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBoxing.StrengthByContestant] = new()
                {
                    // English: What was {1}’s strength rating on {0}?
                    // Example: What was Muhammad’s strength rating on Boxing?
                    Question = "What was {1}’s strength rating on {0}?",
                },
                [SBoxing.ContestantByStrength] = new()
                {
                    // English: What was the {1} of the contestant with strength rating {2} on {0}?
                    // Example: What was the first name of the contestant with strength rating 0 on Boxing?
                    Question = "What was the {1} of the contestant with strength rating {2} on {0}?",
                    Arguments = new()
                    {
                        ["first name"] = "first name",
                        ["last name"] = "last name",
                        ["substitute’s first name"] = "substitute’s first name",
                        ["substitute’s last name"] = "substitute’s last name",
                    },
                },
                [SBoxing.Names] = new()
                {
                    // English: Which {1} appeared on {0}?
                    // Example: Which contestant’s first name appeared on Boxing?
                    Question = "Which {1} appeared on {0}?",
                    Arguments = new()
                    {
                        ["contestant’s first name"] = "contestant’s first name",
                        ["contestant’s last name"] = "contestant’s last name",
                        ["substitute’s first name"] = "substitute’s first name",
                        ["substitute’s last name"] = "substitute’s last name",
                    },
                },
            },
        },

        [typeof(SBraille)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBraille.Pattern] = new()
                {
                    // English: What was the {1} pattern in {0}?
                    // Example: What was the first pattern in Braille?
                    Question = "What was the {1} pattern in {0}?",
                },
            },
        },

        [typeof(SBreakfastEgg)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBreakfastEgg.Color] = new()
                {
                    // English: Which color appeared on the egg in {0}?
                    Question = "Which color appeared on the egg in {0}?",
                },
            },
        },

        [typeof(SBrokenButtons)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBrokenButtons.Question] = new()
                {
                    // English: What was the {1} correct button you pressed in {0}?
                    // Example: What was the first correct button you pressed in Broken Buttons?
                    Question = "What was the {1} correct button you pressed in {0}?",
                },
            },
        },

        [typeof(SBrokenGuitarChords)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBrokenGuitarChords.DisplayedChord] = new()
                {
                    // English: What was the displayed chord in {0}?
                    Question = "What was the displayed chord in {0}?",
                },
                [SBrokenGuitarChords.MutedString] = new()
                {
                    // English: In which position, from left to right, was the broken string in {0}?
                    Question = "In which position, from left to right, was the broken string in {0}?",
                },
            },
        },

        [typeof(SBrownCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBrownCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Brown Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SBrushStrokes)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBrushStrokes.MiddleColor] = new()
                {
                    // English: What was the color of the middle contact point in {0}?
                    Question = "What was the color of the middle contact point in {0}?",
                },
            },
        },

        [typeof(SBulb)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBulb.InitialState] = new()
                {
                    // English: Was the bulb initially lit in {0}?
                    Question = "Was the bulb initially lit in {0}?",
                },
            },
        },

        [typeof(SBurgerAlarm)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBurgerAlarm.Digits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Burger Alarm?
                    Question = "What was the {1} displayed digit in {0}?",
                },
                [SBurgerAlarm.OrderNumbers] = new()
                {
                    // English: What was the {1} order number in {0}?
                    // Example: What was the first order number in Burger Alarm?
                    Question = "What was the {1} order number in {0}?",
                },
            },
        },

        [typeof(SBurglarAlarm)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBurglarAlarm.Digits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Burglar Alarm?
                    Question = "What was the {1} displayed digit in {0}?",
                },
            },
        },

        [typeof(SButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SButton.LightColor] = new()
                {
                    // English: What color did the light glow in {0}?
                    Question = "What color did the light glow in {0}?",
                },
            },
        },

        [typeof(SButtonage)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SButtonage.Buttons] = new()
                {
                    // English: How many {1} buttons were there on {0}?
                    // Example: How many red buttons were there on Buttonage?
                    Question = "How many {1} buttons were there on {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["orange"] = "orange",
                        ["blue"] = "blue",
                        ["pink"] = "pink",
                        ["white"] = "white",
                        ["black"] = "black",
                        ["white-bordered"] = "white-bordered",
                        ["pink-bordered"] = "pink-bordered",
                        ["gray-bordered"] = "gray-bordered",
                        ["red-bordered"] = "red-bordered",
                        ["“P”"] = "“P”",
                        ["special"] = "special",
                    },
                },
            },
        },

        [typeof(SButtonSequence)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SButtonSequence.sColorOccurrences] = new()
                {
                    // English: How many of the buttons in {0} were {1}?
                    // Example: How many of the buttons in Button Sequence were red?
                    Question = "How many of the buttons in {0} were {1}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["blue"] = "blue",
                        ["yellow"] = "yellow",
                        ["white"] = "white",
                    },
                },
            },
        },

        [typeof(SCactisConundrum)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCactisConundrum.Color] = new()
                {
                    // English: What color was the LED in the {1} stage of {0}?
                    // Example: What color was the LED in the first stage of Cacti’s Conundrum?
                    Question = "What color was the LED in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SCaesarCycle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCaesarCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Caesar Cycle?
                    Question = "Which direction was the {1} dial pointing in {0}?",
                },
                [SCaesarCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Caesar Cycle?
                    Question = "What letter was written on the {1} dial in {0}?",
                },
            },
        },

        [typeof(SCaesarPsycho)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCaesarPsycho.ScreenTexts] = new()
                {
                    // English: What text was on the top display in the {1} stage of {0}?
                    // Example: What text was on the top display in the first stage of Caesar Psycho?
                    Question = "What text was on the top display in the {1} stage of {0}?",
                },
                [SCaesarPsycho.ScreenColor] = new()
                {
                    // English: What color was the text on the top display in the second stage of {0}?
                    Question = "What color was the text on the top display in the second stage of {0}?",
                },
            },
        },

        [typeof(SCalendar)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCalendar.LedColor] = new()
                {
                    // English: What was the LED color in {0}?
                    Question = "What was the LED color in {0}?",
                },
            },
        },

        [typeof(SCARPS)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCARPS.Cell] = new()
                {
                    // English: What color was this cell initially in {0}?
                    Question = "What color was this cell initially in {0}?",
                },
            },
        },

        [typeof(SCartinese)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCartinese.ButtonColors] = new()
                {
                    // English: What color was the {1} button in {0}?
                    // Example: What color was the up button in Cartinese?
                    Question = "What color was the {1} button in {0}?",
                    Arguments = new()
                    {
                        ["up"] = "up",
                        ["right"] = "right",
                        ["down"] = "down",
                        ["left"] = "left",
                    },
                },
                [SCartinese.Lyrics] = new()
                {
                    // English: What lyric was played by the {1} button in {0}?
                    // Example: What lyric was played by the up button in Cartinese?
                    Question = "What lyric was played by the {1} button in {0}?",
                    Arguments = new()
                    {
                        ["up"] = "up",
                        ["right"] = "right",
                        ["down"] = "down",
                        ["left"] = "left",
                    },
                },
            },
        },

        [typeof(SCatchphrase)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCatchphrase.Colour] = new()
                {
                    // English: What was the colour of the {1} panel in {0}?
                    // Example: What was the colour of the top-left panel in Catchphrase?
                    Question = "What was the colour of the {1} panel in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-right"] = "top-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
            },
        },

        [typeof(SChallengeAndContact)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SChallengeAndContact.Answers] = new()
                {
                    // English: What was the {1} submitted answer in {0}?
                    // Example: What was the first submitted answer in Challenge & Contact?
                    Question = "What was the {1} submitted answer in {0}?",
                },
            },
        },

        [typeof(SCharacterCodes)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCharacterCodes.Character] = new()
                {
                    // English: What was the {1} character in {0}?
                    // Example: What was the first character in Character Codes?
                    Question = "What was the {1} character in {0}?",
                },
            },
        },

        [typeof(SCharacterShift)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCharacterShift.Letters] = new()
                {
                    // English: Which letter was present but not submitted on the left slider of {0}?
                    Question = "Which letter was present but not submitted on the left slider of {0}?",
                },
                [SCharacterShift.Digits] = new()
                {
                    // English: Which digit was present but not submitted on the right slider of {0}?
                    Question = "Which digit was present but not submitted on the right slider of {0}?",
                },
            },
        },

        [typeof(SCharacterSlots)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCharacterSlots.DisplayedCharacters] = new()
                {
                    // English: Who was displayed in the {1} slot in the {2} stage of {0}?
                    // Example: Who was displayed in the first slot in the first stage of Character Slots?
                    Question = "Who was displayed in the {1} slot in the {2} stage of {0}?",
                },
            },
        },

        [typeof(SCheapCheckout)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCheapCheckout.Paid] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was the paid amount in Cheap Checkout?
                    Question = "What was {1} in {0}?",
                    Arguments = new()
                    {
                        ["the paid amount"] = "the paid amount",
                        ["the first paid amount"] = "the first paid amount",
                        ["the second paid amount"] = "the second paid amount",
                    },
                },
            },
        },

        [typeof(SCheatCheckout)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCheatCheckout.Currency] = new()
                {
                    // English: What was the cryptocurrency of {0}?
                    Question = "What was the cryptocurrency of {0}?",
                },
                [SCheatCheckout.Hack] = new()
                {
                    // English: What was the hack method for the {1} hack of {0}?
                    // Example: What was the hack method for the first hack of Cheat Checkout?
                    Question = "What was the hack method for the {1} hack of {0}?",
                },
                [SCheatCheckout.Site] = new()
                {
                    // English: What was the site for the {1} hack of {0}?
                    // Example: What was the site for the first hack of Cheat Checkout?
                    Question = "What was the site for the {1} hack of {0}?",
                },
            },
        },

        [typeof(SCheepCheckout)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCheepCheckout.Birds] = new()
                {
                    // English: Which bird {1} present in {0}?
                    // Example: Which bird was present in Cheep Checkout?
                    Question = "Which bird {1} present in {0}?",
                    Arguments = new()
                    {
                        ["was"] = "was",
                        ["was not"] = "was not",
                    },
                },
            },
        },

        [typeof(SChess)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SChess.Coordinate] = new()
                {
                    // English: What was the {1} coordinate in {0}?
                    // Example: What was the first coordinate in Chess?
                    Question = "What was the {1} coordinate in {0}?",
                },
            },
        },

        [typeof(SChineseCounting)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SChineseCounting.LED] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the left LED in Chinese Counting?
                    Question = "What color was the {1} LED in {0}?",
                    Arguments = new()
                    {
                        ["left"] = "left",
                        ["right"] = "right",
                    },
                },
            },
        },

        [typeof(SChineseRemainderTheorem)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SChineseRemainderTheorem.Equations] = new()
                {
                    // English: Which equation was used in {0}?
                    Question = "Which equation was used in {0}?",
                },
            },
        },

        [typeof(SChordQualities)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SChordQualities.Notes] = new()
                {
                    // English: Which note was part of the given chord in {0}?
                    Question = "Which note was part of the given chord in {0}?",
                },
            },
        },

        [typeof(SClockCounter)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SClockCounter.Arrows] = new()
                {
                    // English: Which arrow was shown in {0}?
                    Question = "Which arrow was shown in {0}?",
                },
            },
        },

        [typeof(SCode)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCode.DisplayNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "What was the displayed number in {0}?",
                },
            },
        },

        [typeof(SCodenames)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCodenames.Answers] = new()
                {
                    // English: Which of these words was submitted in {0}?
                    Question = "Which of these words was submitted in {0}?",
                },
            },
        },

        [typeof(SCoffeeBeans)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCoffeeBeans.Movements] = new()
                {
                    // English: What was the {1} movement in {0}?
                    // Example: What was the first movement in Coffee Beans?
                    Question = "What was the {1} movement in {0}?",
                },
            },
        },

        [typeof(SCoffeebucks)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCoffeebucks.Coffee] = new()
                {
                    // English: What was the last served coffee in {0}?
                    Question = "What was the last served coffee in {0}?",
                },
            },
        },

        [typeof(SCoinage)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCoinage.Flip] = new()
                {
                    // English: Which coin was flipped in {0}?
                    Question = "Which coin was flipped in {0}?",
                },
            },
        },

        [typeof(SColorAddition)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SColorAddition.Numbers] = new()
                {
                    // English: What was {1}’s number in {0}?
                    // Example: What was red’s number in Color Addition?
                    Question = "What was {1}’s number in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
            },
        },

        [typeof(SColorBraille)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SColorBraille.Color] = new()
                {
                    // English: What color was this dot in {0}?
                    Question = "What color was this dot in {0}?",
                },
            },
        },

        [typeof(SColorDecoding)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SColorDecoding.IndicatorColors] = new()
                {
                    // English: Which color {1} in the {2}-stage indicator pattern in {0}?
                    // Example: Which color appeared in the first-stage indicator pattern in Color Decoding?
                    Question = "Which color {1} in the {2}-stage indicator pattern in {0}?",
                    Arguments = new()
                    {
                        ["appeared"] = "appeared",
                        ["did not appear"] = "did not appear",
                    },
                },
                [SColorDecoding.IndicatorPattern] = new()
                {
                    // English: What was the {1}-stage indicator pattern in {0}?
                    // Example: What was the first-stage indicator pattern in Color Decoding?
                    Question = "What was the {1}-stage indicator pattern in {0}?",
                },
            },
        },

        [typeof(SColoredKeys)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SColoredKeys.DisplayWord] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "What was the displayed word in {0}?",
                },
                [SColoredKeys.DisplayWordColor] = new()
                {
                    // English: What was the displayed word’s color in {0}?
                    Question = "What was the displayed word’s color in {0}?",
                },
                [SColoredKeys.KeyLetter] = new()
                {
                    // English: What letter was on the {1} key in {0}?
                    // Example: What letter was on the top-left key in Colored Keys?
                    Question = "What letter was on the {1} key in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-right"] = "top-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
                [SColoredKeys.KeyColor] = new()
                {
                    // English: What was the color of the {1} key in {0}?
                    // Example: What was the color of the top-left key in Colored Keys?
                    Question = "What was the color of the {1} key in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-right"] = "top-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
            },
        },

        [typeof(SColoredSquares)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SColoredSquares.FirstGroup] = new()
                {
                    // English: What was the first color group in {0}?
                    Question = "What was the first color group in {0}?",
                },
            },
        },

        [typeof(SColoredSwitches)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SColoredSwitches.InitialPosition] = new()
                {
                    // English: What was the initial position of the switches in {0}?
                    Question = "What was the initial position of the switches in {0}?",
                },
                [SColoredSwitches.WhenLEDsCameOn] = new()
                {
                    // English: What was the position of the switches when the LEDs came on in {0}?
                    Question = "What was the position of the switches when the LEDs came on in {0}?",
                },
            },
        },

        [typeof(SColorMorse)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SColorMorse.Color] = new()
                {
                    // English: What was the color of the {1} LED in {0}?
                    // Example: What was the color of the first LED in Color Morse?
                    Question = "What was the color of the {1} LED in {0}?",
                },
                [SColorMorse.Character] = new()
                {
                    // English: What character was flashed by the {1} LED in {0}?
                    // Example: What character was flashed by the first LED in Color Morse?
                    Question = "What character was flashed by the {1} LED in {0}?",
                },
            },
        },

        [typeof(SColorOneTwo)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SColorOneTwo.Color] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the left LED in Color One Two?
                    Question = "What color was the {1} LED in {0}?",
                    Arguments = new()
                    {
                        ["left"] = "left",
                        ["right"] = "right",
                    },
                },
            },
        },

        [typeof(SColorsMaximization)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SColorsMaximization.ColorCount] = new()
                {
                    // English: How many buttons were {1} in {0}?
                    // Example: How many buttons were red in Colors Maximization?
                    Question = "How many buttons were {1} in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
            },
        },

        [typeof(SColouredCubes)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SColouredCubes.Colours] = new()
                {
                    // English: What was the colour of this {1} in the {2} stage of {0}?
                    // Example: What was the colour of this cube in the first stage of Coloured Cubes?
                    Question = "What was the colour of this {1} in the {2} stage of {0}?",
                    Arguments = new()
                    {
                        ["cube"] = "cube",
                        ["stage light"] = "stage light",
                    },
                },
            },
        },

        [typeof(SColouredCylinder)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SColouredCylinder.Colours] = new()
                {
                    // English: What was the {1} colour flashed on the cylinder in {0}?
                    // Example: What was the first colour flashed on the cylinder in Coloured Cylinder?
                    Question = "What was the {1} colour flashed on the cylinder in {0}?",
                },
            },
        },

        [typeof(SColourFlash)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SColourFlash.LastColor] = new()
                {
                    // English: What was the color of the last word in the sequence in {0}?
                    Question = "What was the color of the last word in the sequence in {0}?",
                },
            },
        },

        [typeof(SConcentration)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SConcentration.StartingDigit] = new()
                {
                    // English: What number began here in {0}?
                    Question = "What number began here in {0}?",
                },
            },
            Discriminators = new()
            {
                [SConcentration.Discriminator] = new()
                {
                    // English: the Concentration which began with {1} in the {0} position (in reading order)
                    // Example: the Concentration which began with 1 in the first position (in reading order)
                    Discriminator = "the Concentration which began with {1} in the {0} position (in reading order)",
                },
            },
        },

        [typeof(SConditionalButtons)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SConditionalButtons.Colors] = new()
                {
                    // English: What was the color of this button in {0}?
                    Question = "What was the color of this button in {0}?",
                },
            },
        },

        [typeof(SConnectedMonitors)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SConnectedMonitors.Number] = new()
                {
                    // English: What number was initially displayed on this screen in {0}?
                    Question = "What number was initially displayed on this screen in {0}?",
                },
                [SConnectedMonitors.SingleIndicator] = new()
                {
                    // English: What colour was the indicator on this screen in {0}?
                    Question = "What colour was the indicator on this screen in {0}?",
                },
                [SConnectedMonitors.OrdinalIndicator] = new()
                {
                    // English: What colour was the {1} indicator on this screen in {0}?
                    // Example: What colour was the first indicator on this screen in Connected Monitors?
                    Question = "What colour was the {1} indicator on this screen in {0}?",
                },
            },
        },

        [typeof(SConnectionCheck)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SConnectionCheck.Numbers] = new()
                {
                    // English: What pair of numbers was present in {0}?
                    Question = "What pair of numbers was present in {0}?",
                },
            },
            Discriminators = new()
            {
                [SConnectionCheck.NoNs] = new()
                {
                    // English: the Connection Check with no {0}s
                    // Example: the Connection Check with no 1s
                    Discriminator = "the Connection Check with no {0}s",
                },
                [SConnectionCheck.OneN] = new()
                {
                    // English: the Connection Check with one {0}
                    // Example: the Connection Check with one 1
                    Discriminator = "the Connection Check with one {0}",
                },
                [SConnectionCheck.TwoNs] = new()
                {
                    // English: the Connection Check with two {0}s
                    // Example: the Connection Check with two 1s
                    Discriminator = "the Connection Check with two {0}s",
                },
                [SConnectionCheck.ThreeNs] = new()
                {
                    // English: the Connection Check with three {0}s
                    // Example: the Connection Check with three 1s
                    Discriminator = "the Connection Check with three {0}s",
                },
                [SConnectionCheck.FourNs] = new()
                {
                    // English: the Connection Check with four {0}s
                    // Example: the Connection Check with four 1s
                    Discriminator = "the Connection Check with four {0}s",
                },
            },
        },

        [typeof(SCoordinates)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCoordinates.FirstSolution] = new()
                {
                    // English: What was the solution you selected first in {0}?
                    Question = "What was the solution you selected first in {0}?",
                },
                [SCoordinates.Size] = new()
                {
                    // English: What was the grid size in {0}?
                    Question = "What was the grid size in {0}?",
                },
            },
        },

        [typeof(SCoordination)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCoordination.Label] = new()
                {
                    // English: What was the label of the starting coordinate in {0}?
                    Question = "What was the label of the starting coordinate in {0}?",
                },
                [SCoordination.Position] = new()
                {
                    // English: Where was the starting coordinate in {0}?
                    Question = "Where was the starting coordinate in {0}?",
                },
            },
        },

        [typeof(SCoralCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCoralCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Coral Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SCorners)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCorners.Colors] = new()
                {
                    // English: What was the color of the {1} corner in {0}?
                    // Example: What was the color of the top-left corner in Corners?
                    Question = "What was the color of the {1} corner in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-right"] = "top-right",
                        ["bottom-right"] = "bottom-right",
                        ["bottom-left"] = "bottom-left",
                    },
                },
                [SCorners.ColorCount] = new()
                {
                    // English: How many corners in {0} were {1}?
                    // Example: How many corners in Corners were red?
                    Question = "How many corners in {0} were {1}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["yellow"] = "yellow",
                    },
                },
            },
        },

        [typeof(SCornflowerCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCornflowerCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Cornflower Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SCosmic)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCosmic.Number] = new()
                {
                    // English: What was the number initially shown in {0}?
                    Question = "What was the number initially shown in {0}?",
                },
            },
        },

        [typeof(SCrazyHamburger)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCrazyHamburger.Ingredient] = new()
                {
                    // English: What was the {1} ingredient shown in {0}?
                    // Example: What was the first ingredient shown in Crazy Hamburger?
                    Question = "What was the {1} ingredient shown in {0}?",
                },
            },
        },

        [typeof(SCrazyMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCrazyMaze.StartOrGoal] = new()
                {
                    // English: What was the {1} location in {0}?
                    // Example: What was the starting location in Crazy Maze?
                    Question = "What was the {1} location in {0}?",
                    Arguments = new()
                    {
                        ["starting"] = "starting",
                        ["goal"] = "goal",
                    },
                },
            },
        },

        [typeof(SCreamCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCreamCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Cream Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SCreation)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCreation.Weather] = new()
                {
                    // English: What were the weather conditions on the {1} day in {0}?
                    // Example: What were the weather conditions on the first day in Creation?
                    Question = "What were the weather conditions on the {1} day in {0}?",
                },
            },
        },

        [typeof(SCrimsonCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCrimsonCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Crimson Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SCritters)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCritters.Color] = new()
                {
                    // English: What was the color in {0}?
                    Question = "What was the color in {0}?",
                },
            },
        },

        [typeof(SCruelBinary)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCruelBinary.DisplayedWord] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "What was the displayed word in {0}?",
                },
            },
        },

        [typeof(SCruelKeypads)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCruelKeypads.Colors] = new()
                {
                    // English: What was the color of the bar in the {1} stage of {0}?
                    // Example: What was the color of the bar in the first stage of Cruel Keypads?
                    Question = "What was the color of the bar in the {1} stage of {0}?",
                },
                [SCruelKeypads.DisplayedSymbols] = new()
                {
                    // English: Which of these characters appeared in the {1} stage of {0}?
                    // Example: Which of these characters appeared in the first stage of Cruel Keypads?
                    Question = "Which of these characters appeared in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SCRule)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCRule.SymbolPair] = new()
                {
                    // English: Which symbol pair was here in {0}?
                    Question = "Which symbol pair was here in {0}?",
                },
                [SCRule.SymbolPairCell] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was ♤♤ in cRule?
                    Question = "Where was {1} in {0}?",
                },
                [SCRule.SymbolPairPresent] = new()
                {
                    // English: Which symbol pair was present on {0}?
                    Question = "Which symbol pair was present on {0}?",
                },
                [SCRule.Prefilled] = new()
                {
                    // English: Which cell was pre-filled at the start of {0}?
                    Question = "Which cell was pre-filled at the start of {0}?",
                },
            },
        },

        [typeof(SCrypticCycle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCrypticCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Cryptic Cycle?
                    Question = "Which direction was the {1} dial pointing in {0}?",
                },
                [SCrypticCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Cryptic Cycle?
                    Question = "What letter was written on the {1} dial in {0}?",
                },
            },
        },

        [typeof(SCrypticKeypad)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCrypticKeypad.Labels] = new()
                {
                    // English: What was the label of the {1} key in {0}?
                    // Example: What was the label of the top-left key in Cryptic Keypad?
                    Question = "What was the label of the {1} key in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-right"] = "top-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
                [SCrypticKeypad.Rotations] = new()
                {
                    // English: Which cardinal direction was the {1} key rotated to in {0}?
                    // Example: Which cardinal direction was the top-left key rotated to in Cryptic Keypad?
                    Question = "Which cardinal direction was the {1} key rotated to in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-right"] = "top-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
            },
        },

        [typeof(SCube)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCube.Rotations] = new()
                {
                    // English: What was the {1} cube rotation in {0}?
                    // Example: What was the first cube rotation in Cube?
                    Question = "What was the {1} cube rotation in {0}?",
                },
            },
        },

        [typeof(SCursedDoubleOh)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCursedDoubleOh.InitialPosition] = new()
                {
                    // English: What was the first digit of the initially displayed number in {0}?
                    Question = "What was the first digit of the initially displayed number in {0}?",
                },
            },
        },

        [typeof(SCustomerIdentification)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCustomerIdentification.Customer] = new()
                {
                    // English: Who was the {1} customer in {0}?
                    // Example: Who was the first customer in Customer Identification?
                    Question = "Who was the {1} customer in {0}?",
                },
            },
        },

        [typeof(SCyanButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCyanButton.Positions] = new()
                {
                    // English: Where was the button at the {1} stage in {0}?
                    // Example: Where was the button at the first stage in Cyan Button?
                    Question = "Where was the button at the {1} stage in {0}?",
                },
            },
        },

        [typeof(SDACHMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDACHMaze.Origin] = new()
                {
                    // English: Which region did you depart from in {0}?
                    Question = "Which region did you depart from in {0}?",
                },
            },
        },

        [typeof(SDeafAlley)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDeafAlley.Shape] = new()
                {
                    // English: What was the shape generated in {0}?
                    Question = "What was the shape generated in {0}?",
                },
            },
        },

        [typeof(SDeckOfManyThings)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDeckOfManyThings.FirstCard] = new()
                {
                    // English: What deck did the first card of {0} belong to?
                    Question = "What deck did the first card of {0} belong to?",
                },
            },
        },

        [typeof(SDecoloredSquares)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDecoloredSquares.StartingPos] = new()
                {
                    // English: What was the starting {1} defining color in {0}?
                    // Example: What was the starting column defining color in Decolored Squares?
                    Question = "What was the starting {1} defining color in {0}?",
                    Arguments = new()
                    {
                        ["column"] = "column",
                        ["row"] = "row",
                    },
                },
            },
        },

        [typeof(SDecolourFlash)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDecolourFlash.Goal] = new()
                {
                    // English: What was the {1} of the {2} goal in {0}?
                    // Example: What was the colour of the first goal in Decolour Flash?
                    Question = "What was the {1} of the {2} goal in {0}?",
                    Arguments = new()
                    {
                        ["colour"] = "colour",
                        ["word"] = "word",
                    },
                },
            },
        },

        [typeof(SDenialDisplays)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDenialDisplays.Displays] = new()
                {
                    // English: What number was initially shown on display {1} in {0}?
                    // Example: What number was initially shown on display A in Denial Displays?
                    Question = "What number was initially shown on display {1} in {0}?",
                },
            },
        },

        [typeof(SDetoNATO)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDetoNATO.Display] = new()
                {
                    // English: What was the {1} display in {0}?
                    // Example: What was the first display in DetoNATO?
                    Question = "What was the {1} display in {0}?",
                },
            },
        },

        [typeof(SDevilishEggs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDevilishEggs.Rotations] = new()
                {
                    // English: What was the {1} egg’s {2} rotation in {0}?
                    // Example: What was the top egg’s first rotation in Devilish Eggs?
                    Question = "What was the {1} egg’s {2} rotation in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["bottom"] = "bottom",
                    },
                },
                [SDevilishEggs.Numbers] = new()
                {
                    // English: What was the {1} digit in the string of numbers on {0}?
                    // Example: What was the first digit in the string of numbers on Devilish Eggs?
                    Question = "What was the {1} digit in the string of numbers on {0}?",
                },
                [SDevilishEggs.Letters] = new()
                {
                    // English: What was the {1} letter in the string of letters on {0}?
                    // Example: What was the first letter in the string of letters on Devilish Eggs?
                    Question = "What was the {1} letter in the string of letters on {0}?",
                },
            },
        },

        [typeof(SDialtones)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDialtones.Dialtones] = new()
                {
                    // English: What dialtones were heard in {0}?
                    Question = "What dialtones were heard in {0}?",
                },
            },
        },

        [typeof(SDigisibility)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDigisibility.DisplayedNumber] = new()
                {
                    // English: What was the number on the {1} button in {0}?
                    // Example: What was the number on the first button in Digisibility?
                    Question = "What was the number on the {1} button in {0}?",
                },
            },
        },

        [typeof(SDigitString)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDigitString.InitialNumber] = new()
                {
                    // English: What was the initial number in {0}?
                    Question = "What was the initial number in {0}?",
                },
            },
        },

        [typeof(SDimensionDisruption)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDimensionDisruption.VisibleLetters] = new()
                {
                    // English: Which of these was a visible character in {0}?
                    Question = "Which of these was a visible character in {0}?",
                },
            },
        },

        [typeof(SDirectionalButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDirectionalButton.ButtonCount] = new()
                {
                    // English: How many times did you press the button in the {1} stage of {0}?
                    // Example: How many times did you press the button in the first stage of Directional Button?
                    Question = "How many times did you press the button in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SDiscoloredSquares)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDiscoloredSquares.RememberedPositions] = new()
                {
                    // English: What was {1}’s remembered position in {0}?
                    // Example: What was Blue’s remembered position in Discolored Squares?
                    Question = "What was {1}’s remembered position in {0}?",
                    Arguments = new()
                    {
                        ["Blue"] = "Blue",
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Magenta"] = "Magenta",
                    },
                },
            },
        },

        [typeof(SDisorderedKeys)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDisorderedKeys.MissingInfo] = new()
                {
                    // English: What was the missing information for the {1} key in {0}?
                    // Example: What was the missing information for the first key in Disordered Keys?
                    Question = "What was the missing information for the {1} key in {0}?",
                },
                [SDisorderedKeys.UnrevealedKeyColor] = new()
                {
                    // English: What was the unrevealed key color for the {1} key in {0}?
                    // Example: What was the unrevealed key color for the first key in Disordered Keys?
                    Question = "What was the unrevealed key color for the {1} key in {0}?",
                },
                [SDisorderedKeys.UnrevealedLabelColor] = new()
                {
                    // English: What was the unrevealed label color for the {1} key in {0}?
                    // Example: What was the unrevealed label color for the first key in Disordered Keys?
                    Question = "What was the unrevealed label color for the {1} key in {0}?",
                },
                [SDisorderedKeys.UnrevealedKeyLabel] = new()
                {
                    // English: What was the unrevealed label for the {1} key in {0}?
                    // Example: What was the unrevealed label for the first key in Disordered Keys?
                    Question = "What was the unrevealed label for the {1} key in {0}?",
                },
                [SDisorderedKeys.RevealedKeyColor] = new()
                {
                    // English: What was the revealed key color for the {1} key in {0}?
                    // Example: What was the revealed key color for the first key in Disordered Keys?
                    Question = "What was the revealed key color for the {1} key in {0}?",
                },
                [SDisorderedKeys.RevealedLabelColor] = new()
                {
                    // English: What was the revealed label color for the {1} key in {0}?
                    // Example: What was the revealed label color for the first key in Disordered Keys?
                    Question = "What was the revealed label color for the {1} key in {0}?",
                },
                [SDisorderedKeys.RevealedLabel] = new()
                {
                    // English: What was the revealed label for the {1} key in {0}?
                    // Example: What was the revealed label for the first key in Disordered Keys?
                    Question = "What was the revealed label for the {1} key in {0}?",
                },
            },
        },

        [typeof(SDividedSquares)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDividedSquares.Color] = new()
                {
                    // English: What color was {1} while pressing it in {0}?
                    // Example: What color was the square while pressing it in Divided Squares?
                    Question = "What color was {1} while pressing it in {0}?",
                    Arguments = new()
                    {
                        ["the square"] = "the square",
                        ["the correct square"] = "the correct square",
                    },
                },
            },
        },

        [typeof(SDivisibleNumbers)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDivisibleNumbers.Numbers] = new()
                {
                    // English: What was the {1} stage’s number in {0}?
                    // Example: What was the first stage’s number in Divisible Numbers?
                    Question = "What was the {1} stage’s number in {0}?",
                },
            },
        },

        [typeof(SDoofenshmirtzEvilInc)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDoofenshmirtzEvilInc.Jingles] = new()
                {
                    // English: What jingle played in {0}?
                    Question = "What jingle played in {0}?",
                },
                [SDoofenshmirtzEvilInc.Inators] = new()
                {
                    // English: Which image was shown in {0}?
                    Question = "Which image was shown in {0}?",
                },
            },
        },

        [typeof(SDoubleArrows)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDoubleArrows.Start] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "What was the starting position in {0}?",
                },
                [SDoubleArrows.Movement] = new()
                {
                    // English: Which direction in the grid did the {1} arrow move in {0}?
                    // Example: Which direction in the grid did the inner up arrow move in Double Arrows?
                    Question = "Which direction in the grid did the {1} arrow move in {0}?",
                    Arguments = new()
                    {
                        ["inner up"] = "inner up",
                        ["inner down"] = "inner down",
                        ["inner left"] = "inner left",
                        ["inner right"] = "inner right",
                        ["outer up"] = "outer up",
                        ["outer down"] = "outer down",
                        ["outer left"] = "outer left",
                        ["outer right"] = "outer right",
                    },
                },
                [SDoubleArrows.Arrow] = new()
                {
                    // English: Which {1} arrow moved {2} in the grid in {0}?
                    // Example: Which inner arrow moved up in the grid in Double Arrows?
                    Question = "Which {1} arrow moved {2} in the grid in {0}?",
                    Arguments = new()
                    {
                        ["inner"] = "inner",
                        ["up"] = "up",
                        ["outer"] = "outer",
                        ["down"] = "down",
                        ["left"] = "left",
                        ["right"] = "right",
                    },
                },
            },
        },

        [typeof(SDoubleColor)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDoubleColor.Colors] = new()
                {
                    // English: What was the screen color on the {1} stage of {0}?
                    // Example: What was the screen color on the first stage of Double Color?
                    Question = "What was the screen color on the {1} stage of {0}?",
                },
            },
        },

        [typeof(SDoubleDigits)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDoubleDigits.Displays] = new()
                {
                    // English: What was the digit on the {1} display in {0}?
                    // Example: What was the digit on the left display in Double Digits?
                    Question = "What was the digit on the {1} display in {0}?",
                    Arguments = new()
                    {
                        ["left"] = "left",
                        ["right"] = "right",
                    },
                },
            },
        },

        [typeof(SDoubleExpert)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDoubleExpert.StartingKeyNumber] = new()
                {
                    // English: What was the starting key number in {0}?
                    Question = "What was the starting key number in {0}?",
                },
                [SDoubleExpert.SubmittedWord] = new()
                {
                    // English: What was the word you submitted in {0}?
                    Question = "What was the word you submitted in {0}?",
                },
            },
        },

        [typeof(SDoubleListening)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDoubleListening.Sounds] = new()
                {
                    // English: What clip was played in {0}?
                    Question = "What clip was played in {0}?",
                },
            },
        },

        [typeof(SDoubleOh)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDoubleOh.SubmitButton] = new()
                {
                    // English: Which button was the submit button in {0}?
                    Question = "Which button was the submit button in {0}?",
                },
            },
        },

        [typeof(SDoubleScreen)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDoubleScreen.Colors] = new()
                {
                    // English: What color was the {1} screen in the {2} stage of {0}?
                    // Example: What color was the top screen in the first stage of Double Screen?
                    Question = "What color was the {1} screen in the {2} stage of {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SDrDoctor)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDrDoctor.Diseases] = new()
                {
                    // English: Which of these diseases was listed on {0}, but not the one treated?
                    Question = "Which of these diseases was listed on {0}, but not the one treated?",
                },
                [SDrDoctor.Symptoms] = new()
                {
                    // English: Which of these symptoms was listed on {0}?
                    Question = "Which of these symptoms was listed on {0}?",
                },
            },
        },

        [typeof(SDreamcipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDreamcipher.Word] = new()
                {
                    // English: What was the decrypted word in {0}?
                    Question = "What was the decrypted word in {0}?",
                },
            },
        },

        [typeof(SDuck)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDuck.CurtainColor] = new()
                {
                    // English: What was the color of the curtain in {0}?
                    Question = "What was the color of the curtain in {0}?",
                },
            },
        },

        [typeof(SDumbWaiters)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDumbWaiters.PlayerAvailable] = new()
                {
                    // English: Which player {1} present in {0}?
                    // Example: Which player was present in Dumb Waiters?
                    Question = "Which player {1} present in {0}?",
                    Arguments = new()
                    {
                        ["was"] = "was",
                        ["was not"] = "was not",
                    },
                },
            },
        },

        [typeof(SEarthbound)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEarthbound.Background] = new()
                {
                    // English: What was the background in {0}?
                    Question = "What was the background in {0}?",
                },
                [SEarthbound.Monster] = new()
                {
                    // English: Which monster was displayed in {0}?
                    Question = "Which monster was displayed in {0}?",
                },
            },
        },

        [typeof(SEeBgnillepS)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEeBgnillepS.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "What word was asked to be spelled in {0}?",
                },
            },
        },

        [typeof(SEight)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEight.LastSmallDisplayDigit] = new()
                {
                    // English: What was the last digit on the small display in {0}?
                    Question = "What was the last digit on the small display in {0}?",
                },
                [SEight.LastBrokenDigitPosition] = new()
                {
                    // English: What was the position of the last broken digit in {0}?
                    Question = "What was the position of the last broken digit in {0}?",
                },
                [SEight.LastResultingDigits] = new()
                {
                    // English: What were the last resulting digits in {0}?
                    Question = "What were the last resulting digits in {0}?",
                },
                [SEight.LastDisplayedNumber] = new()
                {
                    // English: What was the last displayed number in {0}?
                    Question = "What was the last displayed number in {0}?",
                },
            },
        },

        [typeof(SElderFuthark)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SElderFuthark.Runes] = new()
                {
                    // English: What was the {1} rune shown on {0}?
                    // Example: What was the first rune shown on Elder Futhark?
                    Question = "What was the {1} rune shown on {0}?",
                },
            },
        },

        [typeof(SEmoji)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEmoji.Emoji] = new()
                {
                    // English: What was the {1} emoji in {0}?
                    // Example: What was the left emoji in Emoji?
                    Question = "What was the {1} emoji in {0}?",
                    Arguments = new()
                    {
                        ["left"] = "left",
                        ["right"] = "right",
                    },
                },
            },
        },

        [typeof(SEnaCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEnaCipher.KeywordAnswer] = new()
                {
                    // English: What was the {1} keyword in {0}?
                    // Example: What was the first keyword in ƎNA Cipher?
                    Question = "What was the {1} keyword in {0}?",
                },
                [SEnaCipher.ExtAnswer] = new()
                {
                    // English: What was the transposition key in {0}?
                    Question = "What was the transposition key in {0}?",
                },
                [SEnaCipher.EncryptedAnswer] = new()
                {
                    // English: What was the encrypted word in {0}?
                    Question = "What was the encrypted word in {0}?",
                },
            },
        },

        [typeof(SEncryptedDice)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEncryptedDice.Question] = new()
                {
                    // English: Which of these numbers appeared on a die in the {1} stage of {0}?
                    // Example: Which of these numbers appeared on a die in the first stage of Encrypted Dice?
                    Question = "Which of these numbers appeared on a die in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SEncryptedEquations)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEncryptedEquations.Shapes] = new()
                {
                    // English: Which shape was the {1} operand in {0}?
                    // Example: Which shape was the first operand in Encrypted Equations?
                    Question = "Which shape was the {1} operand in {0}?",
                },
            },
        },

        [typeof(SEncryptedHangman)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEncryptedHangman.Module] = new()
                {
                    // English: What module name was encrypted by {0}?
                    Question = "What module name was encrypted by {0}?",
                },
                [SEncryptedHangman.EncryptionMethod] = new()
                {
                    // English: What method of encryption was used by {0}?
                    Question = "What method of encryption was used by {0}?",
                },
            },
        },

        [typeof(SEncryptedMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEncryptedMaze.Symbols] = new()
                {
                    // English: Which symbol on {0} was spinning {1}?
                    // Example: Which symbol on Encrypted Maze was spinning clockwise?
                    Question = "Which symbol on {0} was spinning {1}?",
                    Arguments = new()
                    {
                        ["clockwise"] = "clockwise",
                        ["counter-clockwise"] = "counter-clockwise",
                    },
                },
            },
        },

        [typeof(SEncryptedMorse)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEncryptedMorse.CallResponse] = new()
                {
                    // English: What was the {1} on {0}?
                    // Example: What was the received call on Encrypted Morse?
                    Question = "What was the {1} on {0}?",
                    Arguments = new()
                    {
                        ["received call"] = "received call",
                        ["sent response"] = "sent response",
                    },
                },
            },
        },

        [typeof(SEncryptionBingo)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEncryptionBingo.Encoding] = new()
                {
                    // English: What was the first encoding used in {0}?
                    Question = "What was the first encoding used in {0}?",
                },
            },
        },

        [typeof(SEnglishEntries)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEnglishEntries.Display] = new()
                {
                    // English: What was the displayed quote on {0}?
                    Question = "What was the displayed quote on {0}?",
                },
            },
        },

        [typeof(SEnigmaCycle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEnigmaCycle.DialDirectionsThree] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Enigma Cycle?
                    Question = "Which direction was the {1} dial pointing in {0}?",
                },
                [SEnigmaCycle.DialDirectionsTwelve] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Enigma Cycle?
                    Question = "Which direction was the {1} dial pointing in {0}?",
                },
                [SEnigmaCycle.DialDirectionsEight] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Enigma Cycle?
                    Question = "Which direction was the {1} dial pointing in {0}?",
                },
                [SEnigmaCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Enigma Cycle?
                    Question = "What letter was written on the {1} dial in {0}?",
                },
            },
        },

        [typeof(SEntryNumberFour)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEntryNumberFour.Digits] = new()
                {
                    // English: What was the {1} digit in the {2} number shown in {0}?
                    // Example: What was the first digit in the first number shown in Entry Number Four?
                    Question = "What was the {1} digit in the {2} number shown in {0}?",
                },
            },
        },

        [typeof(SEntryNumberOne)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEntryNumberOne.Digits] = new()
                {
                    // English: What was the {1} digit in the {2} number shown in {0}?
                    // Example: What was the first digit in the first number shown in Entry Number One?
                    Question = "What was the {1} digit in the {2} number shown in {0}?",
                },
            },
        },

        [typeof(SEquationsX)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEquationsX.Symbols] = new()
                {
                    // English: What was the displayed symbol in {0}?
                    Question = "What was the displayed symbol in {0}?",
                },
            },
        },

        [typeof(SErrorCodes)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SErrorCodes.ActiveError] = new()
                {
                    // English: What was the active error code in {0}?
                    Question = "What was the active error code in {0}?",
                },
            },
        },

        [typeof(SEtterna)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEtterna.Number] = new()
                {
                    // English: What was the beat for the {1} arrow from the bottom in {0}?
                    // Example: What was the beat for the first arrow from the bottom in Etterna?
                    Question = "What was the beat for the {1} arrow from the bottom in {0}?",
                },
            },
        },

        [typeof(SExoplanets)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SExoplanets.StartingTargetPlanet] = new()
                {
                    // English: What was the starting target planet in {0}?
                    Question = "What was the starting target planet in {0}?",
                },
                [SExoplanets.StartingTargetDigit] = new()
                {
                    // English: What was the starting target digit in {0}?
                    Question = "What was the starting target digit in {0}?",
                },
                [SExoplanets.TargetPlanet] = new()
                {
                    // English: What was the final target planet in {0}?
                    Question = "What was the final target planet in {0}?",
                },
                [SExoplanets.TargetDigit] = new()
                {
                    // English: What was the final target digit in {0}?
                    Question = "What was the final target digit in {0}?",
                },
            },
        },

        [typeof(SFactoringMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFactoringMaze.ChosenPrimes] = new()
                {
                    // English: What was one of the prime numbers chosen in {0}?
                    Question = "What was one of the prime numbers chosen in {0}?",
                },
            },
        },

        [typeof(SFactoryMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFactoryMaze.StartRoom] = new()
                {
                    // English: What room did you start in in {0}?
                    Question = "What room did you start in in {0}?",
                },
            },
        },

        [typeof(SFaerieFires)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFaerieFires.Color] = new()
                {
                    // English: What color was the {1} faerie in {0}?
                    // Example: What color was the first faerie in Faerie Fires?
                    Question = "What color was the {1} faerie in {0}?",
                },
                [SFaerieFires.PitchOrdinal] = new()
                {
                    // English: What pitch did the {1} faerie sing in {0}?
                    // Example: What pitch did the first faerie sing in Faerie Fires?
                    Question = "What pitch did the {1} faerie sing in {0}?",
                },
                [SFaerieFires.PitchColor] = new()
                {
                    // English: What pitch did the {1} faerie sing in {0}?
                    // Example: What pitch did the red faerie sing in Faerie Fires?
                    Question = "What pitch did the {1} faerie sing in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["yellow"] = "yellow",
                        ["cyan"] = "cyan",
                        ["magenta"] = "magenta",
                    },
                },
            },
        },

        [typeof(SFastMath)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFastMath.LastLetters] = new()
                {
                    // English: What was the last pair of letters in {0}?
                    Question = "What was the last pair of letters in {0}?",
                },
            },
        },

        [typeof(SFastPlayfairCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFastPlayfairCipher.LastMessage] = new()
                {
                    // English: What was the last displayed message in {0}?
                    Question = "What was the last displayed message in {0}?",
                },
            },
        },

        [typeof(SFaultyButtons)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFaultyButtons.ReferredToThisButton] = new()
                {
                    // English: Which button referred to the {1} button in reading order in {0}?
                    // Example: Which button referred to the first button in reading order in Faulty Buttons?
                    Question = "Which button referred to the {1} button in reading order in {0}?",
                },
                [SFaultyButtons.ThisButtonReferredTo] = new()
                {
                    // English: Which button did the {1} button in reading order refer to in {0}?
                    // Example: Which button did the first button in reading order refer to in Faulty Buttons?
                    Question = "Which button did the {1} button in reading order refer to in {0}?",
                },
            },
        },

        [typeof(SFaultyRGBMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFaultyRGBMaze.Keys] = new()
                {
                    // English: Where was the {1} key in {0}?
                    // Example: Where was the red key in Faulty RGB Maze?
                    Question = "Where was the {1} key in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
                [SFaultyRGBMaze.Number] = new()
                {
                    // English: Which maze number was the {1} maze in {0}?
                    // Example: Which maze number was the red maze in Faulty RGB Maze?
                    Question = "Which maze number was the {1} maze in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
                [SFaultyRGBMaze.Exit] = new()
                {
                    // English: What was the exit coordinate in {0}?
                    Question = "What was the exit coordinate in {0}?",
                },
            },
        },

        [typeof(SFindTheDate)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFindTheDate.Month] = new()
                {
                    // English: What was the month displayed in the {1} stage of {0}?
                    // Example: What was the month displayed in the first stage of Find The Date?
                    Question = "What was the month displayed in the {1} stage of {0}?",
                },
                [SFindTheDate.Day] = new()
                {
                    // English: What was the day displayed in the {1} stage of {0}?
                    // Example: What was the day displayed in the first stage of Find The Date?
                    Question = "What was the day displayed in the {1} stage of {0}?",
                },
                [SFindTheDate.Year] = new()
                {
                    // English: What was the year displayed in the {1} stage of {0}?
                    // Example: What was the year displayed in the first stage of Find The Date?
                    Question = "What was the year displayed in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SFiveLetterWords)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFiveLetterWords.DisplayedWords] = new()
                {
                    // English: Which of these words was on the display in {0}?
                    Question = "Which of these words was on the display in {0}?",
                },
            },
        },

        [typeof(SFizzBuzz)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFizzBuzz.DisplayedNumbers] = new()
                {
                    // English: What was the {1} digit on the {2} display of {0}?
                    // Example: What was the first digit on the top display of FizzBuzz?
                    Question = "What was the {1} digit on the {2} display of {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SFlags)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFlags.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "What was the displayed number in {0}?",
                },
                [SFlags.MainCountry] = new()
                {
                    // English: What was the main country flag in {0}?
                    Question = "What was the main country flag in {0}?",
                },
                [SFlags.Countries] = new()
                {
                    // English: Which of these country flags was shown, but not the main country flag, in {0}?
                    Question = "Which of these country flags was shown, but not the main country flag, in {0}?",
                },
            },
        },

        [typeof(SFlashingArrows)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFlashingArrows.DisplayedValue] = new()
                {
                    // English: What number was displayed on {0}?
                    Question = "What number was displayed on {0}?",
                },
                [SFlashingArrows.ReferredArrow] = new()
                {
                    // English: What color flashed {1} black on the relevant arrow in {0}?
                    // Example: What color flashed before black on the relevant arrow in Flashing Arrows?
                    Question = "What color flashed {1} black on the relevant arrow in {0}?",
                    Arguments = new()
                    {
                        ["before"] = "before",
                        ["after"] = "after",
                    },
                },
            },
        },

        [typeof(SFlashingLights)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFlashingLights.LEDFrequency] = new()
                {
                    // English: How many times did the {1} LED flash {2} on {0}?
                    // Example: How many times did the top LED flash cyan on Flashing Lights?
                    Question = "How many times did the {1} LED flash {2} on {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["cyan"] = "cyan",
                        ["green"] = "green",
                        ["red"] = "red",
                        ["purple"] = "purple",
                        ["orange"] = "orange",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SFlavorText)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFlavorText.Module] = new()
                {
                    // English: Which module’s flavor text was shown in {0}?
                    Question = "Which module’s flavor text was shown in {0}?",
                },
            },
        },

        [typeof(SFlavorTextEX)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFlavorTextEX.Module] = new()
                {
                    // English: Which module’s flavor text was shown in the {1} stage of {0}?
                    // Example: Which module’s flavor text was shown in the first stage of Flavor Text EX?
                    Question = "Which module’s flavor text was shown in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SFlyswatting)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFlyswatting.Unpressed] = new()
                {
                    // English: Which fly was present, but not in the solution in {0}?
                    Question = "Which fly was present, but not in the solution in {0}?",
                },
            },
        },

        [typeof(SFollowMe)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFollowMe.DisplayedPath] = new()
                {
                    // English: What was the {1} flashing direction in {0}?
                    // Example: What was the first flashing direction in Follow Me?
                    Question = "What was the {1} flashing direction in {0}?",
                },
            },
        },

        [typeof(SForestCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SForestCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Forest Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SForgetAnyColor)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SForgetAnyColor.QCylinder] = new()
                {
                    // English: What colors were the cylinders during the {1} stage of {0}?
                    // Example: What colors were the cylinders during the first stage of Forget Any Color?
                    Question = "What colors were the cylinders during the {1} stage of {0}?",
                    Additional = new()
                    {
                        ["{0}, {1}, {2}"] = "{0}, {1}, {2}",
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Cyan"] = "Cyan",
                        ["Blue"] = "Blue",
                        ["Purple"] = "Purple",
                        ["White"] = "White",
                        ["L"] = "L",
                        ["M"] = "M",
                        ["R"] = "R",
                    },
                },
                [SForgetAnyColor.QFigure] = new()
                {
                    // English: Which figure was used during the {1} stage of {0}?
                    // Example: Which figure was used during the first stage of Forget Any Color?
                    Question = "Which figure was used during the {1} stage of {0}?",
                },
            },
            Discriminators = new()
            {
                [SForgetAnyColor.DCylinder] = new()
                {
                    // English: the Forget Any Color whose cylinders in the {1} stage were {0}
                    // Example: the Forget Any Color whose cylinders in the first stage were Orange, Yellow, Green
                    Discriminator = "the Forget Any Color whose cylinders in the {1} stage were {0}",
                },
                [SForgetAnyColor.DFigure] = new()
                {
                    // English: the Forget Any Color which used figure {0} in the {1} stage
                    // Example: the Forget Any Color which used figure LLLMR in the first stage
                    Discriminator = "the Forget Any Color which used figure {0} in the {1} stage",
                },
            },
        },

        [typeof(SForgetEverything)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SForgetEverything.QStageOneDisplay] = new()
                {
                    // English: What was the {1} displayed digit in the first stage of {0}?
                    // Example: What was the first displayed digit in the first stage of Forget Everything?
                    Question = "What was the {1} displayed digit in the first stage of {0}?",
                },
            },
            Discriminators = new()
            {
                [SForgetEverything.DStageOneDisplay] = new()
                {
                    // English: the Forget Everything whose {0} displayed digit in that stage was {1}
                    // Example: the Forget Everything whose first displayed digit in that stage was 1
                    Discriminator = "the Forget Everything whose {0} displayed digit in that stage was {1}",
                },
            },
        },

        [typeof(SForgetMe)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SForgetMe.InitialState] = new()
                {
                    // English: What number was in the {1} position of the initial puzzle in {0}?
                    // Example: What number was in the top-left position of the initial puzzle in Forget Me?
                    Question = "What number was in the {1} position of the initial puzzle in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-middle"] = "top-middle",
                        ["top-right"] = "top-right",
                        ["middle-left"] = "middle-left",
                        ["center"] = "center",
                        ["middle-right"] = "middle-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-middle"] = "bottom-middle",
                        ["bottom-right"] = "bottom-right",
                    },
                },
            },
        },

        [typeof(SForgetMeNot)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SForgetMeNot.Question] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Forget Me Not?
                    Question = "What was the digit displayed in the {1} stage of {0}?",
                },
            },
            Discriminators = new()
            {
                [SForgetMeNot.Discriminator] = new()
                {
                    // English: the Forget Me Not which displayed a {0} in the {1} stage
                    // Example: the Forget Me Not which displayed a 1 in the first stage
                    Discriminator = "the Forget Me Not which displayed a {0} in the {1} stage",
                },
            },
        },

        [typeof(SForgetMeNow)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SForgetMeNow.DisplayedDigits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Forget Me Now?
                    Question = "What was the {1} displayed digit in {0}?",
                },
            },
        },

        [typeof(SForgetOurVoices)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SForgetOurVoices.Voice] = new()
                {
                    // English: What was played in the {1} stage of {0}?
                    // Example: What was played in the first stage of Forget Our Voices?
                    Question = "What was played in the {1} stage of {0}?",
                },
            },
            Discriminators = new()
            {
                [SForgetOurVoices.Discriminator] = new()
                {
                    // English: the Forget Our Voices which played a {0} in {1}’s voice in the {2} stage
                    // Example: the Forget Our Voices which played a 1 in Umbra Moruka’s voice in the first stage
                    Discriminator = "the Forget Our Voices which played a {0} in {1}’s voice in the {2} stage",
                    Arguments = new()
                    {
                        ["Umbra Moruka"] = "Umbra Moruka",
                        ["Dicey"] = "Dicey",
                        ["MásQuéÉlite"] = "MásQuéÉlite",
                        ["Obvious"] = "Obvious",
                        ["1254"] = "1254",
                        ["Dbros1000"] = "Dbros1000",
                        ["Bomberjack"] = "Bomberjack",
                        ["Danielstigman"] = "Danielstigman",
                        ["Depresso"] = "Depresso",
                        ["ktane1"] = "ktane1",
                        ["OEGamer"] = "OEGamer",
                        ["jTIS"] = "jTIS",
                        ["Krispy"] = "Krispy",
                        ["Grunkle Squeaky"] = "Grunkle Squeaky",
                        ["Arceus"] = "Arceus",
                        ["ScopingLandscape"] = "ScopingLandscape",
                        ["Emik"] = "Emik",
                        ["GhostSalt"] = "GhostSalt",
                        ["Short_c1rcuit"] = "Short_c1rcuit",
                        ["Eltrick"] = "Eltrick",
                        ["Axodeau"] = "Axodeau",
                        ["Asew"] = "Asew",
                        ["Cooldoom"] = "Cooldoom",
                        ["Piissii"] = "Piissii",
                        ["CrazyCaleb"] = "CrazyCaleb",
                    },
                },
            },
        },

        [typeof(SForgetsUltimateShowdown)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SForgetsUltimateShowdown.Answer] = new()
                {
                    // English: What was the {1} digit of the answer in {0}?
                    // Example: What was the first digit of the answer in Forget’s Ultimate Showdown?
                    Question = "What was the {1} digit of the answer in {0}?",
                },
                [SForgetsUltimateShowdown.Bottom] = new()
                {
                    // English: What was the {1} digit of the bottom number in {0}?
                    // Example: What was the first digit of the bottom number in Forget’s Ultimate Showdown?
                    Question = "What was the {1} digit of the bottom number in {0}?",
                },
                [SForgetsUltimateShowdown.Initial] = new()
                {
                    // English: What was the {1} digit of the initial number in {0}?
                    // Example: What was the first digit of the initial number in Forget’s Ultimate Showdown?
                    Question = "What was the {1} digit of the initial number in {0}?",
                },
                [SForgetsUltimateShowdown.Method] = new()
                {
                    // English: What was the {1} method used in {0}?
                    // Example: What was the first method used in Forget’s Ultimate Showdown?
                    Question = "What was the {1} method used in {0}?",
                },
            },
        },

        [typeof(SForgetTheColors)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SForgetTheColors.QGearNumber] = new()
                {
                    // English: What number was on the gear during stage {1} of {0}?
                    // Example: What number was on the gear during stage 0 of Forget The Colors?
                    Question = "What number was on the gear during stage {1} of {0}?",
                },
                [SForgetTheColors.QLargeDisplay] = new()
                {
                    // English: What number was on the large display during stage {1} of {0}?
                    // Example: What number was on the large display during stage 0 of Forget The Colors?
                    Question = "What number was on the large display during stage {1} of {0}?",
                },
                [SForgetTheColors.QSineNumber] = new()
                {
                    // English: What was the last decimal in the sine number received during stage {1} of {0}?
                    // Example: What was the last decimal in the sine number received during stage 0 of Forget The Colors?
                    Question = "What was the last decimal in the sine number received during stage {1} of {0}?",
                },
                [SForgetTheColors.QGearColor] = new()
                {
                    // English: What color was the gear during stage {1} of {0}?
                    // Example: What color was the gear during stage 0 of Forget The Colors?
                    Question = "What color was the gear during stage {1} of {0}?",
                },
                [SForgetTheColors.QRuleColor] = new()
                {
                    // English: Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?
                    // Example: Which edgework-based rule was applied to the sum of nixies and gear during stage 0 of Forget The Colors?
                    Question = "Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?",
                },
            },
            Discriminators = new()
            {
                [SForgetTheColors.DGearNumber] = new()
                {
                    // English: the Forget The Colors whose gear number was {0} in stage {1}
                    // Example: the Forget The Colors whose gear number was 1 in stage 1
                    Discriminator = "the Forget The Colors whose gear number was {0} in stage {1}",
                },
                [SForgetTheColors.DLargeDisplay] = new()
                {
                    // English: the Forget The Colors which had {0} on its large display in stage {1}
                    // Example: the Forget The Colors which had 426 on its large display in stage 1
                    Discriminator = "the Forget The Colors which had {0} on its large display in stage {1}",
                },
                [SForgetTheColors.DSineNumber] = new()
                {
                    // English: the Forget The Colors whose received sine number in stage {1} ended with a {0}
                    // Example: the Forget The Colors whose received sine number in stage 1 ended with a 0
                    Discriminator = "the Forget The Colors whose received sine number in stage {1} ended with a {0}",
                },
                [SForgetTheColors.DColor] = new()
                {
                    // English: the Forget The Colors whose {2} was {0} in stage {1}
                    // Example: the Forget The Colors whose gear color was Red in stage 1
                    Discriminator = "the Forget The Colors whose {2} was {0} in stage {1}",
                    Arguments = new()
                    {
                        ["Red"] = "Red",
                        ["gear color"] = "gear color",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Cyan"] = "Cyan",
                        ["rule color"] = "rule color",
                        ["Blue"] = "Blue",
                        ["Purple"] = "Purple",
                        ["Pink"] = "Pink",
                        ["Maroon"] = "Maroon",
                        ["White"] = "White",
                        ["Gray"] = "Gray",
                    },
                },
            },
        },

        [typeof(SForgetThis)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SForgetThis.QColors] = new()
                {
                    // English: What color was the LED in the {1} stage of {0}?
                    // Example: What color was the LED in the first stage of Forget This?
                    Question = "What color was the LED in the {1} stage of {0}?",
                },
                [SForgetThis.QDigits] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Forget This?
                    Question = "What was the digit displayed in the {1} stage of {0}?",
                },
            },
            Discriminators = new()
            {
                [SForgetThis.DColors] = new()
                {
                    // English: the Forget This whose LED was {0} in the {1} stage
                    // Example: the Forget This whose LED was cyan in the first stage
                    Discriminator = "the Forget This whose LED was {0} in the {1} stage",
                    Arguments = new()
                    {
                        ["cyan"] = "cyan",
                        ["magenta"] = "magenta",
                        ["yellow"] = "yellow",
                        ["black"] = "black",
                        ["white"] = "white",
                        ["green"] = "green",
                    },
                },
                [SForgetThis.DDigits] = new()
                {
                    // English: the Forget This which displayed {0} in the {1} stage
                    // Example: the Forget This which displayed A in the first stage
                    Discriminator = "the Forget This which displayed {0} in the {1} stage",
                },
            },
        },

        [typeof(SForgetUsNot)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SForgetUsNot.Stage] = new()
                {
                    // English: Which module name was used for stage {1} in {0}?
                    // Example: Which module name was used for stage 1 in Forget Us Not?
                    Question = "Which module name was used for stage {1} in {0}?",
                },
            },
            Discriminators = new()
            {
                [SForgetUsNot.Discriminator] = new()
                {
                    // English: the Forget Us Not in which {0} was used for stage {1}
                    // Example: the Forget Us Not in which 1 was used for stage 1
                    Discriminator = "the Forget Us Not in which {0} was used for stage {1}",
                },
            },
        },

        [typeof(SFreeParking)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFreeParking.Token] = new()
                {
                    // English: What was the player token in {0}?
                    Question = "What was the player token in {0}?",
                },
            },
        },

        [typeof(SFunctions)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFunctions.LastDigit] = new()
                {
                    // English: What was the last digit of your first query’s result in {0}?
                    Question = "What was the last digit of your first query’s result in {0}?",
                },
                [SFunctions.LeftNumber] = new()
                {
                    // English: What number was to the left of the displayed letter in {0}?
                    Question = "What number was to the left of the displayed letter in {0}?",
                },
                [SFunctions.Letter] = new()
                {
                    // English: What letter was displayed in {0}?
                    Question = "What letter was displayed in {0}?",
                },
                [SFunctions.RightNumber] = new()
                {
                    // English: What number was to the right of the displayed letter in {0}?
                    Question = "What number was to the right of the displayed letter in {0}?",
                },
            },
        },

        [typeof(SFuseBox)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFuseBox.Flashes] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "What color flashed {1} in {0}?",
                },
                [SFuseBox.Arrows] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "What arrow was shown {1} in {0}?",
                },
            },
        },

        [typeof(SGadgetronVendor)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGadgetronVendor.CurrentWeapon] = new()
                {
                    // English: What was your current weapon in {0}?
                    Question = "What was your current weapon in {0}?",
                },
                [SGadgetronVendor.WeaponForSale] = new()
                {
                    // English: What was the weapon up for sale in {0}?
                    Question = "What was the weapon up for sale in {0}?",
                },
            },
        },

        [typeof(SGameOfLifeCruel)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGameOfLifeCruel.Colors] = new()
                {
                    // English: Which of these was a color combination that occurred in {0}?
                    Question = "Which of these was a color combination that occurred in {0}?",
                },
            },
        },

        [typeof(SGamepad)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGamepad.Numbers] = new()
                {
                    // English: What were the numbers on {0}?
                    Question = "What were the numbers on {0}?",
                },
            },
        },

        [typeof(SGarfieldKart)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGarfieldKart.Track] = new()
                {
                    // English: What was the track in {0}?
                    Question = "What was the track in {0}?",
                },
                [SGarfieldKart.PuzzleCount] = new()
                {
                    // English: How many puzzle pieces did {0} have?
                    Question = "How many puzzle pieces did {0} have?",
                },
            },
        },

        [typeof(SGarnetThief)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGarnetThief.Claim] = new()
                {
                    // English: Which faction did {1} claim to be in {0}?
                    // Example: Which faction did Jungmoon claim to be in Garnet Thief?
                    Question = "Which faction did {1} claim to be in {0}?",
                },
            },
        },

        [typeof(SGhostMovement)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGhostMovement.Position] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was Inky in Ghost Movement?
                    Question = "Where was {1} in {0}?",
                },
            },
        },

        [typeof(SGirlfriend)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGirlfriend.Language] = new()
                {
                    // English: What was the language sung in {0}?
                    Question = "What was the language sung in {0}?",
                },
            },
        },

        [typeof(SGlitchedButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGlitchedButton.Sequence] = new()
                {
                    // English: What was the cycling bit sequence in {0}?
                    Question = "What was the cycling bit sequence in {0}?",
                },
            },
        },

        [typeof(SGoofysGame)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGoofysGame.Number] = new()
                {
                    // English: What number was flashed by the {1} LED in {0}?
                    // Example: What number was flashed by the left LED in Goofy’s Game?
                    Question = "What number was flashed by the {1} LED in {0}?",
                    Arguments = new()
                    {
                        ["left"] = "left",
                        ["right"] = "right",
                        ["center"] = "center",
                    },
                },
            },
        },

        [typeof(SGrandPiano)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGrandPiano.Key] = new()
                {
                    // English: Which key was part of the {1} set in {0}?
                    // Example: Which key was part of the first set in Grand Piano?
                    Question = "Which key was part of the {1} set in {0}?",
                },
                [SGrandPiano.FinalKey] = new()
                {
                    // English: Which key was the fifth set in {0}?
                    Question = "Which key was the fifth set in {0}?",
                },
            },
        },

        [typeof(SGrayButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGrayButton.Coordinates] = new()
                {
                    // English: What was the {1} coordinate on the display in {0}?
                    // Example: What was the horizontal coordinate on the display in Gray Button?
                    Question = "What was the {1} coordinate on the display in {0}?",
                    Arguments = new()
                    {
                        ["horizontal"] = "horizontal",
                        ["vertical"] = "vertical",
                    },
                },
            },
        },

        [typeof(SGrayCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGrayCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Gray Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SGreatVoid)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGreatVoid.Digit] = new()
                {
                    // English: What was the {1} digit in {0}?
                    // Example: What was the first digit in Great Void?
                    Question = "What was the {1} digit in {0}?",
                },
                [SGreatVoid.Color] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in Great Void?
                    Question = "What was the {1} color in {0}?",
                },
            },
        },

        [typeof(SGreenArrows)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGreenArrows.LastScreen] = new()
                {
                    // English: What was the last number on the display on {0}?
                    Question = "What was the last number on the display on {0}?",
                },
            },
        },

        [typeof(SGreenButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGreenButton.Word] = new()
                {
                    // English: What was the word submitted in {0}?
                    Question = "What was the word submitted in {0}?",
                },
            },
        },

        [typeof(SGreenCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGreenCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Green Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SGridLock)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGridLock.StartingColor] = new()
                {
                    // English: What was the starting color in {0}?
                    Question = "What was the starting color in {0}?",
                },
                [SGridLock.StartingLocation] = new()
                {
                    // English: What was the starting location in {0}?
                    Question = "What was the starting location in {0}?",
                },
                [SGridLock.EndingLocation] = new()
                {
                    // English: What was the ending location in {0}?
                    Question = "What was the ending location in {0}?",
                },
            },
        },

        [typeof(SGroceryStore)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGroceryStore.FirstItem] = new()
                {
                    // English: What was the first item shown in {0}?
                    Question = "What was the first item shown in {0}?",
                },
            },
        },

        [typeof(SGryphons)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGryphons.Name] = new()
                {
                    // English: What was the gryphon’s name in {0}?
                    Question = "What was the gryphon’s name in {0}?",
                },
                [SGryphons.Age] = new()
                {
                    // English: What was the gryphon’s age in {0}?
                    Question = "What was the gryphon’s age in {0}?",
                },
            },
        },

        [typeof(SGuessWho)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGuessWho.Colors] = new()
                {
                    // English: Did {1} flash “YES” in {0}?
                    Question = "Did {1} flash “YES” in {0}?",
                },
            },
        },

        [typeof(SGyromaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGyromaze.LEDColor] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the top LED in Gyromaze?
                    Question = "What color was the {1} LED in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SH)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SH.Letter] = new()
                {
                    // English: What was the transmitted letter in {0}?
                    Question = "What was the transmitted letter in {0}?",
                },
            },
        },

        [typeof(SHalliGalli)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHalliGalli.Fruit] = new()
                {
                    // English: Which fruit were there five of in {0}?
                    Question = "Which fruit were there five of in {0}?",
                },
                [SHalliGalli.Counts] = new()
                {
                    // English: What were the relevant counts in {0}?
                    Question = "What were the relevant counts in {0}?",
                },
            },
        },

        [typeof(SHereditaryBaseNotation)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHereditaryBaseNotation.InitialNumber] = new()
                {
                    // English: What was the given number in {0}?
                    Question = "What was the given number in {0}?",
                },
            },
        },

        [typeof(SHexabutton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHexabutton.Label] = new()
                {
                    // English: What label was printed on {0}?
                    Question = "What label was printed on {0}?",
                },
            },
        },

        [typeof(SHexamaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHexamaze.PawnColor] = new()
                {
                    // English: What was the color of the pawn in {0}?
                    Question = "What was the color of the pawn in {0}?",
                },
            },
        },

        [typeof(SHexOrbits)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHexOrbits.Shape] = new()
                {
                    // English: What was the {1} shape for the {2} display in {0}?
                    // Example: What was the fast shape for the first display in hexOrbits?
                    Question = "What was the {1} shape for the {2} display in {0}?",
                    Arguments = new()
                    {
                        ["fast"] = "fast",
                        ["slow"] = "slow",
                    },
                },
            },
        },

        [typeof(SHexOS)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHexOS.OctCipher] = new()
                {
                    // English: What was the deciphered phrase in {0}?
                    Question = "What was the deciphered phrase in {0}?",
                },
                [SHexOS.Cipher] = new()
                {
                    // English: What were the deciphered letters in {0}?
                    Question = "What were the deciphered letters in {0}?",
                },
                [SHexOS.Sum] = new()
                {
                    // English: What were the rhythm values in {0}?
                    Question = "What were the rhythm values in {0}?",
                },
                [SHexOS.Screen] = new()
                {
                    // English: What was the {1} 3-digit number cycled by the screen in {0}?
                    // Example: What was the first 3-digit number cycled by the screen in hexOS?
                    Question = "What was the {1} 3-digit number cycled by the screen in {0}?",
                },
            },
        },

        [typeof(SHickoryDickoryDock)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHickoryDickoryDock.Time] = new()
                {
                    // English: What time was shown when the clock struck {1} on {0}?
                    // Example: What time was shown when the clock struck 1:00 on Hickory Dickory Dock?
                    Question = "What time was shown when the clock struck {1} on {0}?",
                },
            },
            Discriminators = new()
            {
                [SHickoryDickoryDock.Discriminator] = new()
                {
                    // English: the Hickory Dickory Dock which showed {0} when it struck {1}
                    // Example: the Hickory Dickory Dock which showed 1:30 when it struck 2:00
                    Discriminator = "the Hickory Dickory Dock which showed {0} when it struck {1}",
                },
            },
        },

        [typeof(SHiddenColors)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHiddenColors.LED] = new()
                {
                    // English: What was the color of the main LED in {0}?
                    Question = "What was the color of the main LED in {0}?",
                },
            },
        },

        [typeof(SHiddenValue)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHiddenValue.Display] = new()
                {
                    // English: What was displayed on {0}?
                    Question = "What was displayed on {0}?",
                    Additional = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["White"] = "White",
                        ["Yellow"] = "Yellow",
                        ["Magenta"] = "Magenta",
                        ["Cyan"] = "Cyan",
                        ["Purple"] = "Purple",
                        ["{0} {1}"] = "{0} {1}",
                    },
                },
            },
        },

        [typeof(SHighScore)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHighScore.Position] = new()
                {
                    // English: What was the position of the player in {0}?
                    Question = "What was the position of the player in {0}?",
                },
                [SHighScore.Score] = new()
                {
                    // English: What was the score of the player in {0}?
                    Question = "What was the score of the player in {0}?",
                },
            },
        },

        [typeof(SHillCycle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHillCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Hill Cycle?
                    Question = "Which direction was the {1} dial pointing in {0}?",
                },
                [SHillCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Hill Cycle?
                    Question = "What letter was written on the {1} dial in {0}?",
                },
            },
        },

        [typeof(SHinges)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHinges.Initial] = new()
                {
                    // English: Which of these hinges was initially {1} {0}?
                    // Example: Which of these hinges was initially present on Hinges?
                    Question = "Which of these hinges was initially {1} {0}?",
                    Arguments = new()
                    {
                        ["present on"] = "present on",
                        ["absent from"] = "absent from",
                    },
                },
            },
            Discriminators = new()
            {
                [SHinges.Discriminator] = new()
                {
                    // English: the Hinges where this hinge was initally {0}
                    // Example: the Hinges where this hinge was initally present
                    Discriminator = "the Hinges where this hinge was initally {0}",
                    Arguments = new()
                    {
                        ["present"] = "present",
                        ["absent"] = "absent",
                    },
                },
            },
        },

        [typeof(SHogwarts)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHogwarts.House] = new()
                {
                    // English: Which House was {1} solved for in {0}?
                    // Example: Which House was Binary Puzzle solved for in Hogwarts?
                    Question = "Which House was {1} solved for in {0}?",
                },
                [SHogwarts.Module] = new()
                {
                    // English: Which module was solved for {1} in {0}?
                    // Example: Which module was solved for Gryffindor in Hogwarts?
                    Question = "Which module was solved for {1} in {0}?",
                    Arguments = new()
                    {
                        ["Gryffindor"] = "Gryffindor",
                        ["Hufflepuff"] = "Hufflepuff",
                        ["Slytherin"] = "Slytherin",
                        ["Ravenclaw"] = "Ravenclaw",
                    },
                },
            },
        },

        [typeof(SHoldUps)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHoldUps.Shadows] = new()
                {
                    // English: What was the name of the {1} shadow shown in {0}?
                    // Example: What was the name of the first shadow shown in Hold Ups?
                    Question = "What was the name of the {1} shadow shown in {0}?",
                },
            },
        },

        [typeof(SHomophones)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHomophones.DisplayedPhrases] = new()
                {
                    // English: What was the {1} displayed phrase in {0}?
                    // Example: What was the first displayed phrase in Homophones?
                    Question = "What was the {1} displayed phrase in {0}?",
                },
            },
        },

        [typeof(SHorribleMemory)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHorribleMemory.Positions] = new()
                {
                    // English: In what position was the button pressed on the {1} stage of {0}?
                    // Example: In what position was the button pressed on the first stage of Horrible Memory?
                    Question = "In what position was the button pressed on the {1} stage of {0}?",
                },
                [SHorribleMemory.Labels] = new()
                {
                    // English: What was the label of the button pressed on the {1} stage of {0}?
                    // Example: What was the label of the button pressed on the first stage of Horrible Memory?
                    Question = "What was the label of the button pressed on the {1} stage of {0}?",
                },
                [SHorribleMemory.Colors] = new()
                {
                    // English: What color was the button pressed on the {1} stage of {0}?
                    // Example: What color was the button pressed on the first stage of Horrible Memory?
                    Question = "What color was the button pressed on the {1} stage of {0}?",
                },
            },
        },

        [typeof(SHumanResources)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHumanResources.Descriptors] = new()
                {
                    // English: Which was a descriptor shown in {1} in {0}?
                    // Example: Which was a descriptor shown in red in Human Resources?
                    Question = "Which was a descriptor shown in {1} in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                    },
                },
                [SHumanResources.HiredFired] = new()
                {
                    // English: Who was {1} in {0}?
                    // Example: Who was fired in Human Resources?
                    Question = "Who was {1} in {0}?",
                    Arguments = new()
                    {
                        ["fired"] = "fired",
                        ["hired"] = "hired",
                    },
                },
            },
        },

        [typeof(SHunting)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHunting.ColumnsRows] = new()
                {
                    // English: Which of the first three stages of {0} had the {1} symbol {2}?
                    // Example: Which of the first three stages of Hunting had the column symbol first?
                    Question = "Which of the first three stages of {0} had the {1} symbol {2}?",
                    Arguments = new()
                    {
                        ["column"] = "column",
                        ["row"] = "row",
                    },
                },
            },
        },

        [typeof(SHypercube)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHypercube.Rotations] = new()
                {
                    // English: What was the {1} rotation in {0}?
                    // Example: What was the first rotation in Hypercube?
                    Question = "What was the {1} rotation in {0}?",
                },
            },
        },

        [typeof(SHyperForget)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHyperForget.Rotations] = new()
                {
                    // English: What was the rotation for the {1} stage in {0}?
                    // Example: What was the rotation for the first stage in HyperForget?
                    Question = "What was the rotation for the {1} stage in {0}?",
                },
            },
            Discriminators = new()
            {
                [SHyperForget.Discriminator] = new()
                {
                    // English: the HyperForget whose rotation in the {1} stage was {0}
                    // Example: the HyperForget whose rotation in the first stage was XY
                    Discriminator = "the HyperForget whose rotation in the {1} stage was {0}",
                },
            },
        },

        [typeof(SHyperlink)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHyperlink.Characters] = new()
                {
                    // English: What was the {1} character of the hyperlink in {0}?
                    // Example: What was the first character of the hyperlink in Hyperlink?
                    Question = "What was the {1} character of the hyperlink in {0}?",
                },
                [SHyperlink.Answer] = new()
                {
                    // English: Which module was referenced on {0}?
                    Question = "Which module was referenced on {0}?",
                },
            },
        },

        [typeof(SIceCream)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SIceCream.Flavour] = new()
                {
                    // English: Which one of these flavours {1} to the {2} customer in {0}?
                    // Example: Which one of these flavours was on offer, but not sold, to the first customer in Ice Cream?
                    Question = "Which one of these flavours {1} to the {2} customer in {0}?",
                    Arguments = new()
                    {
                        ["was on offer, but not sold,"] = "was on offer, but not sold,",
                        ["was not on offer"] = "was not on offer",
                    },
                },
                [SIceCream.Customer] = new()
                {
                    // English: Who was the {1} customer in {0}?
                    // Example: Who was the first customer in Ice Cream?
                    Question = "Who was the {1} customer in {0}?",
                },
            },
        },

        [typeof(SIdentificationCrisis)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SIdentificationCrisis.Shape] = new()
                {
                    // English: What was the {1} shape used in {0}?
                    // Example: What was the first shape used in Identification Crisis?
                    Question = "What was the {1} shape used in {0}?",
                },
                [SIdentificationCrisis.Dataset] = new()
                {
                    // English: What was the {1} identification module used in {0}?
                    // Example: What was the first identification module used in Identification Crisis?
                    Question = "What was the {1} identification module used in {0}?",
                },
            },
        },

        [typeof(SIdentityParade)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SIdentityParade.HairColors] = new()
                {
                    // English: Which hair color {1} listed in {0}?
                    // Example: Which hair color was listed in Identity Parade?
                    Question = "Which hair color {1} listed in {0}?",
                    Arguments = new()
                    {
                        ["was"] = "was",
                        ["was not"] = "was not",
                    },
                },
                [SIdentityParade.Builds] = new()
                {
                    // English: Which build {1} listed in {0}?
                    // Example: Which build was listed in Identity Parade?
                    Question = "Which build {1} listed in {0}?",
                    Arguments = new()
                    {
                        ["was"] = "was",
                        ["was not"] = "was not",
                    },
                },
                [SIdentityParade.Attires] = new()
                {
                    // English: Which attire {1} listed in {0}?
                    // Example: Which attire was listed in Identity Parade?
                    Question = "Which attire {1} listed in {0}?",
                    Arguments = new()
                    {
                        ["was"] = "was",
                        ["was not"] = "was not",
                    },
                },
            },
        },

        [typeof(SImpostor)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SImpostor.Disguise] = new()
                {
                    // English: Which module was {0} pretending to be?
                    Question = "Which module was {0} pretending to be?",
                },
            },
        },

        [typeof(SIndigoCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SIndigoCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Indigo Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SInfiniteLoop)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SInfiniteLoop.SelectedWord] = new()
                {
                    // English: What was the selected word in {0}?
                    Question = "What was the selected word in {0}?",
                },
            },
        },

        [typeof(SIngredients)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SIngredients.Ingredients] = new()
                {
                    // English: Which ingredient was used in {0}?
                    Question = "Which ingredient was used in {0}?",
                },
                [SIngredients.NonIngredients] = new()
                {
                    // English: Which ingredient was listed but not used in {0}?
                    Question = "Which ingredient was listed but not used in {0}?",
                },
            },
        },

        [typeof(SInnerConnections)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SInnerConnections.LED] = new()
                {
                    // English: What color was the LED in {0}?
                    Question = "What color was the LED in {0}?",
                },
                [SInnerConnections.Morse] = new()
                {
                    // English: What was the digit flashed in Morse in {0}?
                    Question = "What was the digit flashed in Morse in {0}?",
                },
            },
        },

        [typeof(SInterpunct)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SInterpunct.Display] = new()
                {
                    // English: What was the symbol displayed in the {1} stage of {0}?
                    // Example: What was the symbol displayed in the first stage of Interpunct?
                    Question = "What was the symbol displayed in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SIPA)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SIPA.Sound] = new()
                {
                    // English: What sound played in {0}?
                    Question = "What sound played in {0}?",
                },
            },
        },

        [typeof(SiPhone)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SiPhone.Digits] = new()
                {
                    // English: What was the {1} PIN digit in {0}?
                    // Example: What was the first PIN digit in iPhone?
                    Question = "What was the {1} PIN digit in {0}?",
                },
            },
        },

        [typeof(SJenga)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SJenga.FirstBlock] = new()
                {
                    // English: Which symbol was on the first correctly pulled block in {0}?
                    Question = "Which symbol was on the first correctly pulled block in {0}?",
                },
            },
        },

        [typeof(SJewelVault)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SJewelVault.Wheels] = new()
                {
                    // English: What number was wheel {1} in {0}?
                    // Example: What number was wheel A in Jewel Vault?
                    Question = "What number was wheel {1} in {0}?",
                },
            },
        },

        [typeof(SJumbleCycle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SJumbleCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Jumble Cycle?
                    Question = "Which direction was the {1} dial pointing in {0}?",
                },
                [SJumbleCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Jumble Cycle?
                    Question = "What letter was written on the {1} dial in {0}?",
                },
            },
        },

        [typeof(SJuxtacoloredSquares)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SJuxtacoloredSquares.ColorsByPosition] = new()
                {
                    // English: What was the color of this square in {0}?
                    Question = "What was the color of this square in {0}?",
                },
                [SJuxtacoloredSquares.PositionsByColor] = new()
                {
                    // English: Which square was {1} in {0}?
                    // Example: Which square was red in Juxtacolored Squares?
                    Question = "Which square was {1} in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["blue"] = "blue",
                        ["yellow"] = "yellow",
                        ["green"] = "green",
                        ["magenta"] = "magenta",
                        ["orange"] = "orange",
                        ["cyan"] = "cyan",
                        ["purple"] = "purple",
                        ["chestnut"] = "chestnut",
                        ["brown"] = "brown",
                        ["mauve"] = "mauve",
                        ["azure"] = "azure",
                        ["jade"] = "jade",
                        ["forest"] = "forest",
                        ["gray"] = "gray",
                        ["black"] = "black",
                    },
                },
            },
        },

        [typeof(SKanji)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKanji.DisplayedWords] = new()
                {
                    // English: What was the displayed word in the {1} stage of {0}?
                    // Example: What was the displayed word in the first stage of Kanji?
                    Question = "What was the displayed word in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SKanyeEncounter)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKanyeEncounter.Foods] = new()
                {
                    // English: What was a food item displayed in {0}?
                    Question = "What was a food item displayed in {0}?",
                },
            },
        },

        [typeof(SKayMazeyTalk)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKayMazeyTalk.QWord] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the starting word in KayMazey Talk?
                    Question = "What was the {1} word in {0}?",
                    Arguments = new()
                    {
                        ["starting"] = "starting",
                        ["goal"] = "goal",
                    },
                },
            },
            Discriminators = new()
            {
                [SKayMazeyTalk.DWord] = new()
                {
                    // English: the KayMazey Talk whose {0} word was {1}
                    // Example: the KayMazey Talk whose starting word was Knit
                    Discriminator = "the KayMazey Talk whose {0} word was {1}",
                    Arguments = new()
                    {
                        ["starting"] = "starting",
                        ["goal"] = "goal",
                    },
                },
            },
        },

        [typeof(SKeypadCombination)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKeypadCombination.WrongNumbers] = new()
                {
                    // English: Which number was displayed on the {1} button, but not part of the answer on {0}?
                    // Example: Which number was displayed on the first button, but not part of the answer on Keypad Combinations?
                    Question = "Which number was displayed on the {1} button, but not part of the answer on {0}?",
                },
            },
        },

        [typeof(SKeypadMagnified)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKeypadMagnified.LED] = new()
                {
                    // English: What was the position of the LED in {0}?
                    Question = "What was the position of the LED in {0}?",
                },
            },
        },

        [typeof(SKeypadMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKeypadMaze.Yellow] = new()
                {
                    // English: Which of these cells was yellow in {0}?
                    Question = "Which of these cells was yellow in {0}?",
                },
            },
        },

        [typeof(SKeypadSequence)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKeypadSequence.Labels] = new()
                {
                    // English: What was this key’s label on the {1} panel in {0}?
                    // Example: What was this key’s label on the first panel in Keypad Sequence?
                    Question = "What was this key’s label on the {1} panel in {0}?",
                },
            },
        },

        [typeof(SKeywords)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKeywords.DisplayedKey] = new()
                {
                    // English: What were the first four letters on the display in {0}?
                    Question = "What were the first four letters on the display in {0}?",
                },
            },
        },

        [typeof(SKlaxon)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKlaxon.FirstModule] = new()
                {
                    // English: What was the first module to set off {0}?
                    Question = "What was the first module to set off {0}?",
                },
            },
        },

        [typeof(SKnowYourWay)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKnowYourWay.Arrow] = new()
                {
                    // English: Which way was the arrow pointing in {0}?
                    Question = "Which way was the arrow pointing in {0}?",
                },
                [SKnowYourWay.Led] = new()
                {
                    // English: Which LED was green in {0}?
                    Question = "Which LED was green in {0}?",
                },
            },
        },

        [typeof(SKookyKeypad)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKookyKeypad.Color] = new()
                {
                    // English: What color was the {1} button’s LED in {0}?
                    // Example: What color was the top-left button’s LED in Kooky Keypad?
                    Question = "What color was the {1} button’s LED in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-right"] = "top-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
            },
        },

        [typeof(SKudosudoku)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKudosudoku.Prefilled] = new()
                {
                    // English: Which square was {1} in {0}?
                    // Example: Which square was pre-filled in Kudosudoku?
                    Question = "Which square was {1} in {0}?",
                    Arguments = new()
                    {
                        ["pre-filled"] = "pre-filled",
                        ["not pre-filled"] = "not pre-filled",
                    },
                },
            },
        },

        [typeof(SKugelblitz)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKugelblitz.BlackOrangeYellowIndigoViolet] = new()
                {
                    // English: Which particles were present for the {1} stage of {0}?
                    // Example: Which particles were present for the first stage of Kugelblitz?
                    Question = "Which particles were present for the {1} stage of {0}?",
                },
                [SKugelblitz.RedGreenBlue] = new()
                {
                    // English: What were the particles’ values for the {1} stage of {0}?
                    // Example: What were the particles’ values for the first stage of Kugelblitz?
                    Question = "What were the particles’ values for the {1} stage of {0}?",
                    Additional = new()
                    {
                        ["R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}"] = "R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}",
                    },
                },
            },
            Discriminators = new()
            {
                [SKugelblitz.Color] = new()
                {
                    // English: the {0} Kugelblitz
                    // Example: the black Kugelblitz
                    Discriminator = "the {0} Kugelblitz",
                    Arguments = new()
                    {
                        ["black"] = "black",
                        ["red"] = "red",
                        ["orange"] = "orange",
                        ["yellow"] = "yellow",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["indigo"] = "indigo",
                        ["violet"] = "violet",
                    },
                },
                [SKugelblitz.NoLinks] = new()
                {
                    // English: the Kugelblitz linked with no other Kugelblitzes
                    Discriminator = "the Kugelblitz linked with no other Kugelblitzes",
                },
                [SKugelblitz.Links] = new()
                {
                    // English: the {0} Kugelblitz linked with {1}
                    // Example: the black Kugelblitz linked with one other Kugelblitz
                    Discriminator = "the {0} Kugelblitz linked with {1}",
                    Arguments = new()
                    {
                        ["black"] = "black",
                        ["one other Kugelblitz"] = "one other Kugelblitz",
                        ["red"] = "red",
                        ["two other Kugelblitzes"] = "two other Kugelblitzes",
                        ["orange"] = "orange",
                        ["three other Kugelblitzes"] = "three other Kugelblitzes",
                        ["yellow"] = "yellow",
                        ["four other Kugelblitzes"] = "four other Kugelblitzes",
                        ["green"] = "green",
                        ["five other Kugelblitzes"] = "five other Kugelblitzes",
                        ["blue"] = "blue",
                        ["six other Kugelblitzes"] = "six other Kugelblitzes",
                        ["indigo"] = "indigo",
                        ["seven other Kugelblitzes"] = "seven other Kugelblitzes",
                        ["violet"] = "violet",
                    },
                },
            },
        },

        [typeof(SKuro)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKuro.Mood] = new()
                {
                    // English: What was Kuro’s mood in {0}?
                    Question = "What was Kuro’s mood in {0}?",
                },
            },
        },

        [typeof(SLabyrinth)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLabyrinth.PortalLocations] = new()
                {
                    // English: Where was one of the portals in layer {1} in {0}?
                    // Example: Where was one of the portals in layer 1 (Red) in Labyrinth?
                    Question = "Where was one of the portals in layer {1} in {0}?",
                    Arguments = new()
                    {
                        ["1 (Red)"] = "1 (Red)",
                        ["2 (Orange)"] = "2 (Orange)",
                        ["3 (Yellow)"] = "3 (Yellow)",
                        ["4 (Green)"] = "4 (Green)",
                        ["5 (Blue)"] = "5 (Blue)",
                    },
                },
                [SLabyrinth.PortalStage] = new()
                {
                    // English: In which layer was this portal in {0}?
                    Question = "In which layer was this portal in {0}?",
                },
            },
        },

        [typeof(SLadderLottery)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLadderLottery.LightOn] = new()
                {
                    // English: Which light was on in {0}?
                    Question = "Which light was on in {0}?",
                },
            },
        },

        [typeof(SLadders)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLadders.Stage2Colors] = new()
                {
                    // English: Which color was present on the second ladder in {0}?
                    Question = "Which color was present on the second ladder in {0}?",
                },
                [SLadders.Stage3Missing] = new()
                {
                    // English: What color was missing on the third ladder in {0}?
                    Question = "What color was missing on the third ladder in {0}?",
                },
            },
        },

        [typeof(SLangtonsAnteater)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLangtonsAnteater.InitialState] = new()
                {
                    // English: Which of these squares was initially {1} in {0}?
                    // Example: Which of these squares was initially black in Langton’s Anteater?
                    Question = "Which of these squares was initially {1} in {0}?",
                    Arguments = new()
                    {
                        ["black"] = "black",
                        ["white"] = "white",
                    },
                },
            },
        },

        [typeof(SLasers)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLasers.Hatches] = new()
                {
                    // English: What was the number on the {1} hatch on {0}?
                    // Example: What was the number on the top-left hatch on Lasers?
                    Question = "What was the number on the {1} hatch on {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-middle"] = "top-middle",
                        ["top-right"] = "top-right",
                        ["middle-left"] = "middle-left",
                        ["center"] = "center",
                        ["middle-right"] = "middle-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-middle"] = "bottom-middle",
                        ["bottom-right"] = "bottom-right",
                    },
                },
            },
        },

        [typeof(SLEDEncryption)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLEDEncryption.PressedLetters] = new()
                {
                    // English: What was the correct letter you pressed in the {1} stage of {0}?
                    // Example: What was the correct letter you pressed in the first stage of LED Encryption?
                    Question = "What was the correct letter you pressed in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SLEDGrid)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLEDGrid.NumBlack] = new()
                {
                    // English: How many LEDs were unlit in {0}?
                    Question = "How many LEDs were unlit in {0}?",
                },
            },
        },

        [typeof(SLEDMath)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLEDMath.Lights] = new()
                {
                    // English: What color was {1} in {0}?
                    // Example: What color was LED A in LED Math?
                    Question = "What color was {1} in {0}?",
                    Arguments = new()
                    {
                        ["LED A"] = "LED A",
                        ["LED B"] = "LED B",
                        ["the operator LED"] = "the operator LED",
                    },
                },
            },
        },

        [typeof(SLEDs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLEDs.OriginalColor] = new()
                {
                    // English: What was the initial color of the changed LED in {0}?
                    Question = "What was the initial color of the changed LED in {0}?",
                },
            },
        },

        [typeof(SLEGOs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLEGOs.PieceDimensions] = new()
                {
                    // English: What were the dimensions of the {1} piece in {0}?
                    // Example: What were the dimensions of the red piece in LEGOs?
                    Question = "What were the dimensions of the {1} piece in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["cyan"] = "cyan",
                        ["magenta"] = "magenta",
                        ["yellow"] = "yellow",
                    },
                },
            },
        },

        [typeof(SLetterMath)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLetterMath.Display] = new()
                {
                    // English: What was the letter on the {1} display in {0}?
                    // Example: What was the letter on the left display in Letter Math?
                    Question = "What was the letter on the {1} display in {0}?",
                    Arguments = new()
                    {
                        ["left"] = "left",
                        ["right"] = "right",
                    },
                },
            },
        },

        [typeof(SLightBulbs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLightBulbs.Colors] = new()
                {
                    // English: What was the color of the {1} bulb in {0}?
                    // Example: What was the color of the left bulb in Light Bulbs?
                    Question = "What was the color of the {1} bulb in {0}?",
                    Arguments = new()
                    {
                        ["left"] = "left",
                        ["right"] = "right",
                    },
                },
            },
        },

        [typeof(SLinq)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLinq.Function] = new()
                {
                    // English: What was the {1} function in {0}?
                    // Example: What was the first function in Linq?
                    Question = "What was the {1} function in {0}?",
                },
            },
            Discriminators = new()
            {
                [SLinq.Discriminator] = new()
                {
                    // English: the Linq whose {0} function was {1}
                    // Example: the Linq whose first function was First
                    Discriminator = "the Linq whose {0} function was {1}",
                },
            },
        },

        [typeof(SLionsShare)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLionsShare.Year] = new()
                {
                    // English: Which year was displayed on {0}?
                    Question = "Which year was displayed on {0}?",
                },
                [SLionsShare.RemovedLions] = new()
                {
                    // English: Which lion was present but removed in {0}?
                    Question = "Which lion was present but removed in {0}?",
                },
            },
        },

        [typeof(SListening)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SListening.Sound] = new()
                {
                    // English: What clip was played in {0}?
                    Question = "What clip was played in {0}?",
                },
            },
        },

        [typeof(SLiteralMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLiteralMaze.Letter] = new()
                {
                    // English: Which letter was in this position in {0}?
                    Question = "Which letter was in this position in {0}?",
                },
            },
        },

        [typeof(SLogicalButtons)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLogicalButtons.Color] = new()
                {
                    // English: What was the color of the {1} button in the {2} stage of {0}?
                    // Example: What was the color of the top button in the first stage of Logical Buttons?
                    Question = "What was the color of the {1} button in the {2} stage of {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
                [SLogicalButtons.Label] = new()
                {
                    // English: What was the label on the {1} button in the {2} stage of {0}?
                    // Example: What was the label on the top button in the first stage of Logical Buttons?
                    Question = "What was the label on the {1} button in the {2} stage of {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
                [SLogicalButtons.Operator] = new()
                {
                    // English: What was the final operator in the {1} stage of {0}?
                    // Example: What was the final operator in the first stage of Logical Buttons?
                    Question = "What was the final operator in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SLogicGates)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLogicGates.Gates] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was gate A in Logic Gates?
                    Question = "What was {1} in {0}?",
                    Arguments = new()
                    {
                        ["gate A"] = "gate A",
                        ["gate B"] = "gate B",
                        ["gate C"] = "gate C",
                        ["gate D"] = "gate D",
                        ["gate E"] = "gate E",
                        ["gate F"] = "gate F",
                        ["gate G"] = "gate G",
                        ["the duplicated gate"] = "the duplicated gate",
                    },
                },
            },
        },

        [typeof(SLombaxCubes)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLombaxCubes.Letters] = new()
                {
                    // English: What was the {1} letter on the button in {0}?
                    // Example: What was the first letter on the button in Lombax Cubes?
                    Question = "What was the {1} letter on the button in {0}?",
                },
            },
        },

        [typeof(SLondonUnderground)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLondonUnderground.Stations] = new()
                {
                    // English: Where did the {1} journey on {0} {2}?
                    // Example: Where did the first journey on London Underground depart from?
                    Question = "Where did the {1} journey on {0} {2}?",
                    Arguments = new()
                    {
                        ["depart from"] = "depart from",
                        ["arrive to"] = "arrive to",
                    },
                },
            },
        },

        [typeof(SLongWords)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLongWords.Word] = new()
                {
                    // English: What was the word on the top display on {0}?
                    Question = "What was the word on the top display on {0}?",
                },
            },
        },

        [typeof(SMadMemory)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMadMemory.Displays] = new()
                {
                    // English: What was on the display in the {1} stage of {0}?
                    // Example: What was on the display in the first stage of Mad Memory?
                    Question = "What was on the display in the {1} stage of {0}?",
                    Arguments = new()
                    {
                        ["first"] = "first",
                        ["second"] = "second",
                        ["third"] = "third",
                        ["4th"] = "4th",
                    },
                },
            },
        },

        [typeof(SMafia)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMafia.Players] = new()
                {
                    // English: Who was a player, but not the Godfather, in {0}?
                    Question = "Who was a player, but not the Godfather, in {0}?",
                },
            },
        },

        [typeof(SMagentaCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMagentaCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Magenta Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SMahjong)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMahjong.CountingTile] = new()
                {
                    // English: Which tile was shown in the bottom-left of {0}?
                    Question = "Which tile was shown in the bottom-left of {0}?",
                },
                [SMahjong.Matches] = new()
                {
                    // English: Which tile was part of the {1} matched pair in {0}?
                    // Example: Which tile was part of the first matched pair in Mahjong?
                    Question = "Which tile was part of the {1} matched pair in {0}?",
                },
            },
        },

        [typeof(SMainPage)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMainPage.ButtonEffectOrigin] = new()
                {
                    // English: Which main page did the {1} button’s effect come from in {0}?
                    // Example: Which main page did the toons button’s effect come from in Main Page?
                    Question = "Which main page did the {1} button’s effect come from in {0}?",
                    Arguments = new()
                    {
                        ["toons"] = "toons",
                        ["games"] = "games",
                        ["characters"] = "characters",
                        ["downloads"] = "downloads",
                        ["store"] = "store",
                        ["email"] = "email",
                    },
                },
                [SMainPage.HomestarBackgroundOrigin] = new()
                {
                    // English: Which main page did {1} come from in {0}?
                    // Example: Which main page did Homestar come from in Main Page?
                    Question = "Which main page did {1} come from in {0}?",
                    Arguments = new()
                    {
                        ["Homestar"] = "Homestar",
                        ["the background"] = "the background",
                    },
                },
                [SMainPage.BubbleColors] = new()
                {
                    // English: Which color did the bubble not display in {0}?
                    Question = "Which color did the bubble not display in {0}?",
                },
                [SMainPage.BubbleMessages] = new()
                {
                    // English: Which of the following messages did the bubble {1} in {0}?
                    // Example: Which of the following messages did the bubble display in Main Page?
                    Question = "Which of the following messages did the bubble {1} in {0}?",
                    Arguments = new()
                    {
                        ["display"] = "display",
                        ["not display"] = "not display",
                    },
                },
            },
        },

        [typeof(SMandMs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMandMs.Colors] = new()
                {
                    // English: What color was the text on the {1} button in {0}?
                    // Example: What color was the text on the first button in M&Ms?
                    Question = "What color was the text on the {1} button in {0}?",
                },
                [SMandMs.Labels] = new()
                {
                    // English: What was the text on the {1} button in {0}?
                    // Example: What was the text on the first button in M&Ms?
                    Question = "What was the text on the {1} button in {0}?",
                },
            },
        },

        [typeof(SMandNs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMandNs.Colors] = new()
                {
                    // English: What color was the text on the {1} button in {0}?
                    // Example: What color was the text on the first button in M&Ns?
                    Question = "What color was the text on the {1} button in {0}?",
                },
                [SMandNs.Label] = new()
                {
                    // English: What was the text on the correct button in {0}?
                    Question = "What was the text on the correct button in {0}?",
                },
            },
        },

        [typeof(SMaritimeFlags)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMaritimeFlags.Bearing] = new()
                {
                    // English: What bearing was signalled in {0}?
                    Question = "What bearing was signalled in {0}?",
                },
                [SMaritimeFlags.Callsign] = new()
                {
                    // English: Which callsign was signalled in {0}?
                    Question = "Which callsign was signalled in {0}?",
                },
            },
        },

        [typeof(SMaritimeSemaphore)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMaritimeSemaphore.Dummy] = new()
                {
                    // English: In which position was the dummy in {0}?
                    Question = "In which position was the dummy in {0}?",
                },
                [SMaritimeSemaphore.Letter] = new()
                {
                    // English: Which letter was shown by the {2} in the {1} position in {0}?
                    // Example: Which letter was shown by the left flag in the first position in Maritime Semaphore?
                    Question = "Which letter was shown by the {2} in the {1} position in {0}?",
                    Arguments = new()
                    {
                        ["left flag"] = "left flag",
                        ["right flag"] = "right flag",
                        ["semaphore"] = "semaphore",
                    },
                },
            },
        },

        [typeof(SMaroonButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMaroonButton.A] = new()
                {
                    // English: What was A in {0}?
                    Question = "What was A in {0}?",
                },
            },
        },

        [typeof(SMaroonCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMaroonCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Maroon Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SMashematics)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMashematics.Answer] = new()
                {
                    // English: What was the answer in {0}?
                    Question = "What was the answer in {0}?",
                },
                [SMashematics.Calculation] = new()
                {
                    // English: What was the {1} number in the equation on {0}?
                    // Example: What was the first number in the equation on Mashematics?
                    Question = "What was the {1} number in the equation on {0}?",
                },
            },
        },

        [typeof(SMasterTapes)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMasterTapes.PlayedSong] = new()
                {
                    // English: Which song was played in {0}?
                    Question = "Which song was played in {0}?",
                },
            },
        },

        [typeof(SMatchRefereeing)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMatchRefereeing.Planet] = new()
                {
                    // English: Which planet was present in the {1} stage of {0}?
                    // Example: Which planet was present in the first stage of Match Refereeing?
                    Question = "Which planet was present in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SMathEm)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMathEm.Color] = new()
                {
                    // English: What was the color of this tile before the shuffle on {0}?
                    Question = "What was the color of this tile before the shuffle on {0}?",
                },
                [SMathEm.Label] = new()
                {
                    // English: What was the design on this tile before the shuffle on {0}?
                    Question = "What was the design on this tile before the shuffle on {0}?",
                },
            },
        },

        [typeof(SMatrix)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMatrix.AccessCode] = new()
                {
                    // English: Which word was part of the latest access code in {0}?
                    Question = "Which word was part of the latest access code in {0}?",
                },
                [SMatrix.GlitchWord] = new()
                {
                    // English: What was the glitched word in {0}?
                    Question = "What was the glitched word in {0}?",
                },
            },
        },

        [typeof(SMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMaze.StartingPosition] = new()
                {
                    // English: In which {1} was the starting position in {0}, counting from the {2}?
                    // Example: In which column was the starting position in Maze, counting from the left?
                    Question = "In which {1} was the starting position in {0}, counting from the {2}?",
                    Arguments = new()
                    {
                        ["column"] = "column",
                        ["left"] = "left",
                        ["row"] = "row",
                        ["top"] = "top",
                    },
                },
            },
        },

        [typeof(SMaze3)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMaze3.StartingFace] = new()
                {
                    // English: What was the color of the starting face in {0}?
                    Question = "What was the color of the starting face in {0}?",
                },
            },
        },

        [typeof(SMazeIdentification)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMazeIdentification.Seed] = new()
                {
                    // English: What was the seed of the maze in {0}?
                    Question = "What was the seed of the maze in {0}?",
                },
                [SMazeIdentification.Num] = new()
                {
                    // English: What was the function of button {1} in {0}?
                    // Example: What was the function of button 1 in Maze Identification?
                    Question = "What was the function of button {1} in {0}?",
                    Answers = new()
                    {
                        ["forwards"] = "forwards",
                        ["clockwise"] = "clockwise",
                        ["backwards"] = "backwards",
                        ["counter-clockwise"] = "counter-clockwise",
                    },
                },
                [SMazeIdentification.Func] = new()
                {
                    // English: Which button {1} in {0}?
                    // Example: Which button moved you forwards in Maze Identification?
                    Question = "Which button {1} in {0}?",
                    Arguments = new()
                    {
                        ["moved you forwards"] = "moved you forwards",
                        ["turned you clockwise"] = "turned you clockwise",
                        ["moved you backwards"] = "moved you backwards",
                        ["turned you counter-clockwise"] = "turned you counter-clockwise",
                    },
                },
            },
        },

        [typeof(SMazematics)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMazematics.Value] = new()
                {
                    // English: Which was the {1} value in {0}?
                    // Example: Which was the initial value in Mazematics?
                    Question = "Which was the {1} value in {0}?",
                    Arguments = new()
                    {
                        ["initial"] = "initial",
                        ["goal"] = "goal",
                    },
                },
            },
        },

        [typeof(SMazeScrambler)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMazeScrambler.Start] = new()
                {
                    // English: What was the starting position on {0}?
                    Question = "What was the starting position on {0}?",
                },
                [SMazeScrambler.Goal] = new()
                {
                    // English: What was the goal on {0}?
                    Question = "What was the goal on {0}?",
                },
                [SMazeScrambler.Indicators] = new()
                {
                    // English: Which of these positions was a maze marking on {0}?
                    Question = "Which of these positions was a maze marking on {0}?",
                },
            },
        },

        [typeof(SMazeseeker)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMazeseeker.Cell] = new()
                {
                    // English: How many walls surrounded this cell in {0}?
                    Question = "How many walls surrounded this cell in {0}?",
                },
                [SMazeseeker.Start] = new()
                {
                    // English: Where was the start in {0}?
                    Question = "Where was the start in {0}?",
                },
                [SMazeseeker.Goal] = new()
                {
                    // English: Where was the goal in {0}?
                    Question = "Where was the goal in {0}?",
                },
            },
        },

        [typeof(SMazeSwap)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMazeSwap.Position] = new()
                {
                    // English: Where was the {1} position in {0}?
                    // Example: Where was the starting position in Maze Swap?
                    Question = "Where was the {1} position in {0}?",
                    Arguments = new()
                    {
                        ["starting"] = "starting",
                        ["goal"] = "goal",
                    },
                },
            },
        },

        [typeof(SMegaMan2)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMegaMan2.Master] = new()
                {
                    // English: Which master was shown in {0}?
                    Question = "Which master was shown in {0}?",
                },
                [SMegaMan2.Weapon] = new()
                {
                    // English: Which weapon was shown in {0}?
                    Question = "Which weapon was shown in {0}?",
                },
            },
        },

        [typeof(SMelodySequencer)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMelodySequencer.Parts] = new()
                {
                    // English: Which slot contained part #{1} at the start of {0}?
                    // Example: Which slot contained part #1 at the start of Melody Sequencer?
                    Question = "Which slot contained part #{1} at the start of {0}?",
                },
                [SMelodySequencer.Slots] = new()
                {
                    // English: Which part was in slot #{1} at the start of {0}?
                    // Example: Which part was in slot #1 at the start of Melody Sequencer?
                    Question = "Which part was in slot #{1} at the start of {0}?",
                },
            },
        },

        [typeof(SMemorableButtons)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMemorableButtons.Symbols] = new()
                {
                    // English: What was the {1} correct symbol pressed in {0}?
                    // Example: What was the first correct symbol pressed in Memorable Buttons?
                    Question = "What was the {1} correct symbol pressed in {0}?",
                },
            },
        },

        [typeof(SMemory)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMemory.Display] = new()
                {
                    // English: What was the displayed number in the {1} stage of {0}?
                    // Example: What was the displayed number in the first stage of Memory?
                    Question = "What was the displayed number in the {1} stage of {0}?",
                },
                [SMemory.Position] = new()
                {
                    // English: In what position was the button that you pressed in the {1} stage of {0}?
                    // Example: In what position was the button that you pressed in the first stage of Memory?
                    Question = "In what position was the button that you pressed in the {1} stage of {0}?",
                },
                [SMemory.Label] = new()
                {
                    // English: What was the label of the button that you pressed in the {1} stage of {0}?
                    // Example: What was the label of the button that you pressed in the first stage of Memory?
                    Question = "What was the label of the button that you pressed in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SMemoryWires)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMemoryWires.WireColours] = new()
                {
                    // English: What was the colour of wire {1} in {0}?
                    // Example: What was the colour of wire 1 in Memory Wires?
                    Question = "What was the colour of wire {1} in {0}?",
                },
                [SMemoryWires.DisplayedDigits] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Memory Wires?
                    Question = "What was the digit displayed in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SMetamorse)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMetamorse.ExtractedLetter] = new()
                {
                    // English: What was the extracted letter in {0}?
                    Question = "What was the extracted letter in {0}?",
                },
            },
        },

        [typeof(SMetapuzzle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMetapuzzle.Answer] = new()
                {
                    // English: What was the final answer in {0}?
                    Question = "What was the final answer in {0}?",
                },
            },
        },

        [typeof(SMinskMetro)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMinskMetro.Station] = new()
                {
                    // English: What was the name of starting station in {0}?
                    Question = "What was the name of starting station in {0}?",
                },
            },
        },

        [typeof(SMirror)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMirror.Word] = new()
                {
                    // English: What was the second word written by the original ghost in {0}?
                    Question = "What was the second word written by the original ghost in {0}?",
                },
            },
        },

        [typeof(SMisterSoftee)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMisterSoftee.SpongebobPosition] = new()
                {
                    // English: Where was the SpongeBob Bar on {0}?
                    Question = "Where was the SpongeBob Bar on {0}?",
                },
                [SMisterSoftee.TreatsPresent] = new()
                {
                    // English: Which treat was present on {0}?
                    Question = "Which treat was present on {0}?",
                },
            },
        },

        [typeof(SMixometer)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMixometer.SubmitButton] = new()
                {
                    // English: What was the position of the submit button in {0}?
                    Question = "What was the position of the submit button in {0}?",
                },
            },
        },

        [typeof(SModernCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SModernCipher.Word] = new()
                {
                    // English: What was the decrypted word of the {1} stage in {0}?
                    // Example: What was the decrypted word of the first stage in Modern Cipher?
                    Question = "What was the decrypted word of the {1} stage in {0}?",
                },
            },
        },

        [typeof(SModuleListening)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SModuleListening.ButtonAudio] = new()
                {
                    // English: Which sound did the {1} button play in {0}?
                    // Example: Which sound did the red button play in Module Listening?
                    Question = "Which sound did the {1} button play in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["yellow"] = "yellow",
                    },
                },
                [SModuleListening.AnyAudio] = new()
                {
                    // English: Which sound played in {0}?
                    Question = "Which sound played in {0}?",
                },
            },
        },

        [typeof(SModuleManeuvers)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SModuleManeuvers.Goal] = new()
                {
                    // English: What was the goal location in {0}?
                    Question = "What was the goal location in {0}?",
                    Additional = new()
                    {
                        ["{0}, {1}"] = "{0}, {1}",
                    },
                },
            },
        },

        [typeof(SModuleMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SModuleMaze.StartingIcon] = new()
                {
                    // English: Which of the following was the starting icon for {0}?
                    Question = "Which of the following was the starting icon for {0}?",
                },
            },
        },

        [typeof(SModuleMovements)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SModuleMovements.Display] = new()
                {
                    // English: What was the {1} module shown in {0}?
                    // Example: What was the first module shown in Module Movements?
                    Question = "What was the {1} module shown in {0}?",
                },
            },
        },

        [typeof(SMoneyGame)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMoneyGame.Question1] = new()
                {
                    // English: What were the first and second words in the {1} phrase in {0}?
                    // Example: What were the first and second words in the first phrase in Money Game?
                    Question = "What were the first and second words in the {1} phrase in {0}?",
                },
                [SMoneyGame.Question2] = new()
                {
                    // English: What were the third and fourth words in the {1} phrase in {0}?
                    // Example: What were the third and fourth words in the first phrase in Money Game?
                    Question = "What were the third and fourth words in the {1} phrase in {0}?",
                },
                [SMoneyGame.Question3] = new()
                {
                    // English: What was the end of the {1} phrase in {0}?
                    // Example: What was the end of the first phrase in Money Game?
                    Question = "What was the end of the {1} phrase in {0}?",
                },
            },
        },

        [typeof(SMonsplodeFight)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMonsplodeFight.Creature] = new()
                {
                    // English: Which creature was displayed in {0}?
                    Question = "Which creature was displayed in {0}?",
                },
                [SMonsplodeFight.Move] = new()
                {
                    // English: Which one of these moves {1} selectable in {0}?
                    // Example: Which one of these moves was selectable in Monsplode, Fight!?
                    Question = "Which one of these moves {1} selectable in {0}?",
                    Arguments = new()
                    {
                        ["was"] = "was",
                        ["was not"] = "was not",
                    },
                },
            },
        },

        [typeof(SMonsplodeTradingCards)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMonsplodeTradingCards.Cards] = new()
                {
                    // English: What was the {1} before the last action in {0}?
                    // Example: What was the first card in your hand before the last action in Monsplode Trading Cards?
                    Question = "What was the {1} before the last action in {0}?",
                    Arguments = new()
                    {
                        ["first card in your hand"] = "first card in your hand",
                        ["second card in your hand"] = "second card in your hand",
                        ["third card in your hand"] = "third card in your hand",
                        ["card on offer"] = "card on offer",
                    },
                },
                [SMonsplodeTradingCards.PrintVersions] = new()
                {
                    // English: What was the print version of the {1} before the last action in {0}?
                    // Example: What was the print version of the first card in your hand before the last action in Monsplode Trading Cards?
                    Question = "What was the print version of the {1} before the last action in {0}?",
                    Arguments = new()
                    {
                        ["first card in your hand"] = "first card in your hand",
                        ["second card in your hand"] = "second card in your hand",
                        ["third card in your hand"] = "third card in your hand",
                        ["card on offer"] = "card on offer",
                    },
                },
            },
        },

        [typeof(SMoon)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMoon.LitUnlit] = new()
                {
                    // English: What was the {1} set in clockwise order in {0}?
                    // Example: What was the first initially lit set in clockwise order in Moon?
                    Question = "What was the {1} set in clockwise order in {0}?",
                    Arguments = new()
                    {
                        ["first initially lit"] = "first initially lit",
                        ["second initially lit"] = "second initially lit",
                        ["third initially lit"] = "third initially lit",
                        ["fourth initially lit"] = "fourth initially lit",
                        ["first initially unlit"] = "first initially unlit",
                        ["second initially unlit"] = "second initially unlit",
                        ["third initially unlit"] = "third initially unlit",
                        ["fourth initially unlit"] = "fourth initially unlit",
                    },
                },
            },
        },

        [typeof(SMoreCode)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMoreCode.Word] = new()
                {
                    // English: What was the flashing word in {0}?
                    Question = "What was the flashing word in {0}?",
                },
            },
        },

        [typeof(SMorseAMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMorseAMaze.StartingCoordinate] = new()
                {
                    // English: What was the starting location in {0}?
                    Question = "What was the starting location in {0}?",
                },
                [SMorseAMaze.EndingCoordinate] = new()
                {
                    // English: What was the ending location in {0}?
                    Question = "What was the ending location in {0}?",
                },
                [SMorseAMaze.MorseCodeWord] = new()
                {
                    // English: What was the word shown as Morse code in {0}?
                    Question = "What was the word shown as Morse code in {0}?",
                },
            },
        },

        [typeof(SMorseButtons)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMorseButtons.ButtonLabel] = new()
                {
                    // English: What was the character flashed by the {1} button in {0}?
                    // Example: What was the character flashed by the first button in Morse Buttons?
                    Question = "What was the character flashed by the {1} button in {0}?",
                },
                [SMorseButtons.ButtonColor] = new()
                {
                    // English: What was the color flashed by the {1} button in {0}?
                    // Example: What was the color flashed by the first button in Morse Buttons?
                    Question = "What was the color flashed by the {1} button in {0}?",
                },
            },
        },

        [typeof(SMorsematics)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMorsematics.ReceivedLetters] = new()
                {
                    // English: What was the {1} received letter in {0}?
                    // Example: What was the first received letter in Morsematics?
                    Question = "What was the {1} received letter in {0}?",
                },
            },
        },

        [typeof(SMorseWar)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMorseWar.Code] = new()
                {
                    // English: What code was transmitted in {0}?
                    Question = "What code was transmitted in {0}?",
                },
                [SMorseWar.Leds] = new()
                {
                    // English: What were the LEDs in the {1} row in {0} (1 = on, 0 = off)?
                    // Example: What were the LEDs in the bottom row in Morse War (1 = on, 0 = off)?
                    Question = "What were the LEDs in the {1} row in {0} (1 = on, 0 = off)?",
                    Arguments = new()
                    {
                        ["bottom"] = "bottom",
                        ["middle"] = "middle",
                        ["top"] = "top",
                    },
                },
            },
        },

        [typeof(SMorseWoF)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMorseWoF.Displays] = new()
                {
                    // English: What was the display in the {1} stage on {0}?
                    // Example: What was the display in the first stage on .--/---/..-.?
                    Question = "What was the display in the {1} stage on {0}?",
                },
            },
        },

        [typeof(SMouseInTheMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMouseInTheMaze.Sphere] = new()
                {
                    // English: Which color sphere was the goal in {0}?
                    Question = "Which color sphere was the goal in {0}?",
                },
                [SMouseInTheMaze.Torus] = new()
                {
                    // English: What color was the torus in {0}?
                    Question = "What color was the torus in {0}?",
                },
            },
        },

        [typeof(SMSeq)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMSeq.Obtained] = new()
                {
                    // English: What was the {1} obtained digit in {0}?
                    // Example: What was the first obtained digit in M-Seq?
                    Question = "What was the {1} obtained digit in {0}?",
                },
                [SMSeq.Submitted] = new()
                {
                    // English: What was the final number from the iteration process in {0}?
                    Question = "What was the final number from the iteration process in {0}?",
                },
            },
        },

        [typeof(SMssngvWls)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMssngvWls.MssNgvwL] = new()
                {
                    // English: {1}
                    // Example: Whc hvw lwsm ssn gn {0}?
                    Question = "{1}",
                    Additional = new()
                    {
                        ["Which vowel was missing in {0}?"] = "Which vowel was missing in {0}?",
                        ["AEIOU"] = "AEIOU",
                    },
                },
            },
        },

        [typeof(SMulticoloredSwitches)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMulticoloredSwitches.LedColor] = new()
                {
                    // English: What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?
                    // Example: What color was the first LED on the top row when the tiny LED was lit in Multicolored Switches?
                    Question = "What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["lit"] = "lit",
                        ["bottom"] = "bottom",
                        ["unlit"] = "unlit",
                    },
                },
            },
        },

        [typeof(SMurder)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMurder.Suspect] = new()
                {
                    // English: Which of these was {1} in {0}?
                    // Example: Which of these was a suspect but not the murderer in Murder?
                    Question = "Which of these was {1} in {0}?",
                    Arguments = new()
                    {
                        ["a suspect but not the murderer"] = "a suspect but not the murderer",
                        ["not a suspect"] = "not a suspect",
                    },
                },
                [SMurder.Weapon] = new()
                {
                    // English: Which of these was {1} in {0}?
                    // Example: Which of these was a potential weapon but not the murder weapon in Murder?
                    Question = "Which of these was {1} in {0}?",
                    Arguments = new()
                    {
                        ["a potential weapon but not the murder weapon"] = "a potential weapon but not the murder weapon",
                        ["not a potential weapon"] = "not a potential weapon",
                    },
                },
                [SMurder.BodyFound] = new()
                {
                    // English: Where was the body found in {0}?
                    Question = "Where was the body found in {0}?",
                },
            },
        },

        [typeof(SMysteryModule)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMysteryModule.FirstKey] = new()
                {
                    // English: Which module was the first requested to be solved by {0}?
                    Question = "Which module was the first requested to be solved by {0}?",
                },
                [SMysteryModule.HiddenModule] = new()
                {
                    // English: Which module was hidden by {0}?
                    Question = "Which module was hidden by {0}?",
                },
            },
        },

        [typeof(SMysticSquare)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMysticSquare.Skull] = new()
                {
                    // English: Where was the skull in {0}?
                    Question = "Where was the skull in {0}?",
                },
            },
        },

        [typeof(SNameCodes)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNameCodes.Indices] = new()
                {
                    // English: What was the {1} index in {0}?
                    // Example: What was the left index in Name Codes?
                    Question = "What was the {1} index in {0}?",
                    Arguments = new()
                    {
                        ["left"] = "left",
                        ["right"] = "right",
                    },
                },
            },
        },

        [typeof(SNamingConventions)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNamingConventions.Object] = new()
                {
                    // English: What was the label of the first button in {0}?
                    Question = "What was the label of the first button in {0}?",
                },
            },
        },

        [typeof(SNandMs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNandMs.Answer] = new()
                {
                    // English: What was the label of the correct button in {0}?
                    Question = "What was the label of the correct button in {0}?",
                },
            },
        },

        [typeof(SNandNs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNandNs.Label] = new()
                {
                    // English: Which label was present in the {1} stage of {0}?
                    // Example: Which label was present in the first stage of N&Ns?
                    Question = "Which label was present in the {1} stage of {0}?",
                },
                [SNandNs.Color] = new()
                {
                    // English: Which color was missing in the third stage of {0}?
                    Question = "Which color was missing in the third stage of {0}?",
                },
            },
        },

        [typeof(SNavigationDetermination)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNavigationDetermination.Color] = new()
                {
                    // English: What was the color of the maze in {0}?
                    Question = "What was the color of the maze in {0}?",
                },
                [SNavigationDetermination.Label] = new()
                {
                    // English: What was the label of the maze in {0}?
                    Question = "What was the label of the maze in {0}?",
                },
            },
        },

        [typeof(SNavinums)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNavinums.DirectionalButtons] = new()
                {
                    // English: What was the {1} directional button pressed in {0}?
                    // Example: What was the first directional button pressed in Navinums?
                    Question = "What was the {1} directional button pressed in {0}?",
                },
                [SNavinums.MiddleDigit] = new()
                {
                    // English: What was the initial middle digit in {0}?
                    Question = "What was the initial middle digit in {0}?",
                },
            },
        },

        [typeof(SNavyButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNavyButton.QGreekLetters] = new()
                {
                    // English: Which Greek letter appeared on {0} (case-sensitive)?
                    Question = "Which Greek letter appeared on {0} (case-sensitive)?",
                },
                [SNavyButton.QGiven] = new()
                {
                    // English: What was the {1} of the given in {0}?
                    // Example: What was the (0-indexed) column of the given in Navy Button?
                    Question = "What was the {1} of the given in {0}?",
                    Arguments = new()
                    {
                        ["(0-indexed) column"] = "(0-indexed) column",
                        ["(0-indexed) row"] = "(0-indexed) row",
                        ["value"] = "value",
                    },
                },
            },
            Discriminators = new()
            {
                [SNavyButton.DGreekLettersNV] = new()
                {
                    // English: the Navy Button that had a {0} on it
                    // Example: the Navy Button that had a Β on it
                    Discriminator = "the Navy Button that had a {0} on it",
                },
                [SNavyButton.DGreekLettersV] = new()
                {
                    // English: the Navy Button that had an {0} on it
                    // Example: the Navy Button that had an Α on it
                    Discriminator = "the Navy Button that had an {0} on it",
                },
                [SNavyButton.DGiven] = new()
                {
                    // English: the Navy Button where the {0} of the given was {1}
                    // Example: the Navy Button where the (0-indexed) column of the given was 1
                    Discriminator = "the Navy Button where the {0} of the given was {1}",
                    Arguments = new()
                    {
                        ["(0-indexed) column"] = "(0-indexed) column",
                        ["(0-indexed) row"] = "(0-indexed) row",
                        ["value"] = "value",
                    },
                },
            },
        },

        [typeof(SNecronomicon)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNecronomicon.Chapters] = new()
                {
                    // English: What was the chapter number of the {1} page in {0}?
                    // Example: What was the chapter number of the first page in Necronomicon?
                    Question = "What was the chapter number of the {1} page in {0}?",
                },
            },
        },

        [typeof(SNegativity)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNegativity.SubmittedValue] = new()
                {
                    // English: In base 10, what was the value submitted in {0}?
                    Question = "In base 10, what was the value submitted in {0}?",
                },
                [SNegativity.SubmittedTernary] = new()
                {
                    // English: Excluding 0s, what was the submitted balanced ternary in {0}?
                    Question = "Excluding 0s, what was the submitted balanced ternary in {0}?",
                },
            },
        },

        [typeof(SNeptune)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNeptune.Star] = new()
                {
                    // English: Which star was displayed in {0}?
                    Question = "Which star was displayed in {0}?",
                },
            },
        },

        [typeof(SNeutralization)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNeutralization.Color] = new()
                {
                    // English: What was the acid’s color in {0}?
                    Question = "What was the acid’s color in {0}?",
                },
                [SNeutralization.Volume] = new()
                {
                    // English: What was the acid’s volume in {0}?
                    Question = "What was the acid’s volume in {0}?",
                },
            },
        },

        [typeof(SNextInLine)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNextInLine.FirstWire] = new()
                {
                    // English: What color was the first wire in {0}?
                    Question = "What color was the first wire in {0}?",
                },
            },
        },

        [typeof(SNonverbalSimon)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNonverbalSimon.Flashes] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "Which button flashed in the {1} stage in {0}?",
                },
            },
        },

        [typeof(SNotColoredSquares)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotColoredSquares.InitialPosition] = new()
                {
                    // English: What was the position of the square you initially pressed in {0}?
                    Question = "What was the position of the square you initially pressed in {0}?",
                },
            },
        },

        [typeof(SNotColoredSwitches)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotColoredSwitches.Word] = new()
                {
                    // English: What was the encrypted word in {0}?
                    Question = "What was the encrypted word in {0}?",
                },
            },
        },

        [typeof(SNotColourFlash)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotColourFlash.InitialWord] = new()
                {
                    // English: What was {1} in the displayed word sequence in {0}?
                    // Example: What was first in the displayed word sequence in Not Colour Flash?
                    Question = "What was {1} in the displayed word sequence in {0}?",
                },
                [SNotColourFlash.InitialColour] = new()
                {
                    // English: What was {1} in the displayed colour sequence in {0}?
                    // Example: What was first in the displayed colour sequence in Not Colour Flash?
                    Question = "What was {1} in the displayed colour sequence in {0}?",
                },
            },
        },

        [typeof(SNotConnectionCheck)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotConnectionCheck.Flashes] = new()
                {
                    // English: What symbol flashed on the {1} button in {0}?
                    // Example: What symbol flashed on the top left button in Not Connection Check?
                    Question = "What symbol flashed on the {1} button in {0}?",
                    Arguments = new()
                    {
                        ["top left"] = "top left",
                        ["top right"] = "top right",
                        ["bottom left"] = "bottom left",
                        ["bottom right"] = "bottom right",
                    },
                },
                [SNotConnectionCheck.Values] = new()
                {
                    // English: What was the value of the {1} button in {0}?
                    // Example: What was the value of the top left button in Not Connection Check?
                    Question = "What was the value of the {1} button in {0}?",
                    Arguments = new()
                    {
                        ["top left"] = "top left",
                        ["top right"] = "top right",
                        ["bottom left"] = "bottom left",
                        ["bottom right"] = "bottom right",
                    },
                },
            },
        },

        [typeof(SNotCoordinates)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotCoordinates.SquareCoords] = new()
                {
                    // English: Which coordinate was part of the square in {0}?
                    Question = "Which coordinate was part of the square in {0}?",
                },
            },
        },

        [typeof(SNotDoubleOh)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotDoubleOh.Position] = new()
                {
                    // English: What was the {1} displayed position in the second stage of {0}?
                    // Example: What was the first displayed position in the second stage of Not Double-Oh?
                    Question = "What was the {1} displayed position in the second stage of {0}?",
                },
            },
        },

        [typeof(SNotKeypad)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotKeypad.Color] = new()
                {
                    // English: What color flashed {1} in the final sequence in {0}?
                    // Example: What color flashed first in the final sequence in Not Keypad?
                    Question = "What color flashed {1} in the final sequence in {0}?",
                },
                [SNotKeypad.Symbol] = new()
                {
                    // English: Which symbol was on the button that flashed {1} in the final sequence in {0}?
                    // Example: Which symbol was on the button that flashed first in the final sequence in Not Keypad?
                    Question = "Which symbol was on the button that flashed {1} in the final sequence in {0}?",
                },
            },
        },

        [typeof(SNotMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotMaze.StartingDistance] = new()
                {
                    // English: What was the starting distance in {0}?
                    Question = "What was the starting distance in {0}?",
                },
            },
        },

        [typeof(SNotMorseCode)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotMorseCode.Word] = new()
                {
                    // English: What was the {1} correct word you submitted in {0}?
                    // Example: What was the first correct word you submitted in Not Morse Code?
                    Question = "What was the {1} correct word you submitted in {0}?",
                },
            },
        },

        [typeof(SNotMorsematics)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotMorsematics.Word] = new()
                {
                    // English: What was the transmitted word on {0}?
                    Question = "What was the transmitted word on {0}?",
                },
            },
        },

        [typeof(SNotMurder)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotMurder.Room] = new()
                {
                    // English: What room was {1} in initially on {0}?
                    // Example: What room was Miss Scarlett in initially on Not Murder?
                    Question = "What room was {1} in initially on {0}?",
                    Arguments = new()
                    {
                        ["Miss Scarlett"] = "Miss Scarlett",
                        ["Colonel Mustard"] = "Colonel Mustard",
                        ["Reverend Green"] = "Reverend Green",
                        ["Mrs Peacock"] = "Mrs Peacock",
                        ["Professor Plum"] = "Professor Plum",
                        ["Mrs White"] = "Mrs White",
                    },
                },
                [SNotMurder.Weapon] = new()
                {
                    // English: What weapon did {1} possess initially on {0}?
                    // Example: What weapon did Miss Scarlett possess initially on Not Murder?
                    Question = "What weapon did {1} possess initially on {0}?",
                    Arguments = new()
                    {
                        ["Miss Scarlett"] = "Miss Scarlett",
                        ["Colonel Mustard"] = "Colonel Mustard",
                        ["Reverend Green"] = "Reverend Green",
                        ["Mrs Peacock"] = "Mrs Peacock",
                        ["Professor Plum"] = "Professor Plum",
                        ["Mrs White"] = "Mrs White",
                    },
                },
            },
            Discriminators = new()
            {
                [SNotMurder.Present] = new()
                {
                    // English: the Not Murder where {0} was present
                    // Example: the Not Murder where he was present
                    Discriminator = "the Not Murder where {0} was present",
                    Arguments = new()
                    {
                        ["he"] = "he",
                        ["she"] = "she",
                    },
                },
                [SNotMurder.InitialWeapon] = new()
                {
                    // English: the Not Murder where {0} initially held the {1}
                    // Example: the Not Murder where he initially held the Candlestick
                    Discriminator = "the Not Murder where {0} initially held the {1}",
                    Arguments = new()
                    {
                        ["he"] = "he",
                        ["Candlestick"] = "Candlestick",
                        ["Dagger"] = "Dagger",
                        ["she"] = "she",
                        ["Lead Pipe"] = "Lead Pipe",
                        ["Revolver"] = "Revolver",
                    },
                },
                [SNotMurder.InitialRoom] = new()
                {
                    // English: the Not Murder where {0} started in the {1}
                    // Example: the Not Murder where he started in the Ballroom
                    Discriminator = "the Not Murder where {0} started in the {1}",
                    Arguments = new()
                    {
                        ["he"] = "he",
                        ["Ballroom"] = "Ballroom",
                        ["Billiard Room"] = "Billiard Room",
                        ["she"] = "she",
                        ["Conservatory"] = "Conservatory",
                        ["Dining Room"] = "Dining Room",
                    },
                },
            },
        },

        [typeof(SNotNumberPad)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotNumberPad.Flashes] = new()
                {
                    // English: Which of these numbers {1} at the {2} stage of {0}?
                    // Example: Which of these numbers flashed at the first stage of Not Number Pad?
                    Question = "Which of these numbers {1} at the {2} stage of {0}?",
                    Arguments = new()
                    {
                        ["flashed"] = "flashed",
                        ["did not flash"] = "did not flash",
                    },
                },
            },
        },

        [typeof(SNotPassword)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotPassword.Letter] = new()
                {
                    // English: Which letter was missing from {0}?
                    Question = "Which letter was missing from {0}?",
                },
            },
        },

        [typeof(SNotPerspectivePegs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotPerspectivePegs.Position] = new()
                {
                    // English: What was the position of the {1} flashing peg on {0}?
                    // Example: What was the position of the first flashing peg on Not Perspective Pegs?
                    Question = "What was the position of the {1} flashing peg on {0}?",
                },
                [SNotPerspectivePegs.Perspective] = new()
                {
                    // English: From what perspective did the {1} peg flash on {0}?
                    // Example: From what perspective did the first peg flash on Not Perspective Pegs?
                    Question = "From what perspective did the {1} peg flash on {0}?",
                },
                [SNotPerspectivePegs.Color] = new()
                {
                    // English: What was the color of the {1} flashing peg on {0}?
                    // Example: What was the color of the first flashing peg on Not Perspective Pegs?
                    Question = "What was the color of the {1} flashing peg on {0}?",
                },
            },
        },

        [typeof(SNotPianoKeys)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotPianoKeys.FirstSymbol] = new()
                {
                    // English: What was the first displayed symbol on {0}?
                    Question = "What was the first displayed symbol on {0}?",
                },
                [SNotPianoKeys.SecondSymbol] = new()
                {
                    // English: What was the second displayed symbol on {0}?
                    Question = "What was the second displayed symbol on {0}?",
                },
                [SNotPianoKeys.ThirdSymbol] = new()
                {
                    // English: What was the third displayed symbol on {0}?
                    Question = "What was the third displayed symbol on {0}?",
                },
            },
        },

        [typeof(SNotRedArrows)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotRedArrows.Start] = new()
                {
                    // English: What was the starting number in {0}?
                    Question = "What was the starting number in {0}?",
                },
            },
        },

        [typeof(SNotSimaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotSimaze.Maze] = new()
                {
                    // English: Which maze was used in {0}?
                    Question = "Which maze was used in {0}?",
                },
                [SNotSimaze.Start] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "What was the starting position in {0}?",
                },
                [SNotSimaze.Goal] = new()
                {
                    // English: What was the goal position in {0}?
                    Question = "What was the goal position in {0}?",
                },
            },
        },

        [typeof(SNotTextField)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotTextField.BackgroundLetter] = new()
                {
                    // English: Which letter appeared 9 times at the start of {0}?
                    Question = "Which letter appeared 9 times at the start of {0}?",
                },
                [SNotTextField.InitialPresses] = new()
                {
                    // English: Which letter was pressed in the first stage of {0}?
                    Question = "Which letter was pressed in the first stage of {0}?",
                },
            },
        },

        [typeof(SNotTheBulb)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotTheBulb.Word] = new()
                {
                    // English: What word flashed on {0}?
                    Question = "What word flashed on {0}?",
                },
                [SNotTheBulb.Color] = new()
                {
                    // English: What color was the bulb on {0}?
                    Question = "What color was the bulb on {0}?",
                },
                [SNotTheBulb.ScrewCap] = new()
                {
                    // English: What was the material of the screw cap on {0}?
                    Question = "What was the material of the screw cap on {0}?",
                },
            },
        },

        [typeof(SNotTheButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotTheButton.LightColor] = new()
                {
                    // English: What colors did the light glow in {0}?
                    Question = "What colors did the light glow in {0}?",
                },
            },
        },

        [typeof(SNotThePlungerButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotThePlungerButton.Background] = new()
                {
                    // English: What color did the background flash in {0}?
                    Question = "What color did the background flash in {0}?",
                },
            },
        },

        [typeof(SNotTheScrew)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotTheScrew.InitialPosition] = new()
                {
                    // English: What was the initial position in {0}?
                    Question = "What was the initial position in {0}?",
                },
            },
        },

        [typeof(SNotWhosOnFirst)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotWhosOnFirst.PressedPosition] = new()
                {
                    // English: In which position was the button you pressed in the {1} stage on {0}?
                    // Example: In which position was the button you pressed in the first stage on Not Who’s on First?
                    Question = "In which position was the button you pressed in the {1} stage on {0}?",
                },
                [SNotWhosOnFirst.PressedLabel] = new()
                {
                    // English: What was the label on the button you pressed in the {1} stage on {0}?
                    // Example: What was the label on the button you pressed in the first stage on Not Who’s on First?
                    Question = "What was the label on the button you pressed in the {1} stage on {0}?",
                },
                [SNotWhosOnFirst.ReferencePosition] = new()
                {
                    // English: In which position was the reference button in the {1} stage on {0}?
                    // Example: In which position was the reference button in the first stage on Not Who’s on First?
                    Question = "In which position was the reference button in the {1} stage on {0}?",
                },
                [SNotWhosOnFirst.ReferenceLabel] = new()
                {
                    // English: What was the label on the reference button in the {1} stage on {0}?
                    // Example: What was the label on the reference button in the first stage on Not Who’s on First?
                    Question = "What was the label on the reference button in the {1} stage on {0}?",
                },
                [SNotWhosOnFirst.Sum] = new()
                {
                    // English: What was the calculated number in the second stage on {0}?
                    Question = "What was the calculated number in the second stage on {0}?",
                },
            },
        },

        [typeof(SNotWordSearch)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotWordSearch.Missing] = new()
                {
                    // English: Which of these consonants was missing in {0}?
                    Question = "Which of these consonants was missing in {0}?",
                },
                [SNotWordSearch.FirstPress] = new()
                {
                    // English: What was the first correctly pressed letter in {0}?
                    Question = "What was the first correctly pressed letter in {0}?",
                },
            },
        },

        [typeof(SNotX01)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotX01.SectorValues] = new()
                {
                    // English: Which sector value {1} present on {0}?
                    // Example: Which sector value was present on Not X01?
                    Question = "Which sector value {1} present on {0}?",
                    Arguments = new()
                    {
                        ["was"] = "was",
                        ["was not"] = "was not",
                    },
                },
            },
        },

        [typeof(SNotXRay)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNotXRay.ScannerColor] = new()
                {
                    // English: What was the scanner color in {0}?
                    Question = "What was the scanner color in {0}?",
                },
                [SNotXRay.Table] = new()
                {
                    // English: What table were we in in {0} (numbered 1–8 in reading order in the manual)?
                    Question = "What table were we in in {0} (numbered 1–8 in reading order in the manual)?",
                },
                [SNotXRay.Directions] = new()
                {
                    // English: What direction was button {1} in {0}?
                    // Example: What direction was button 1 in Not X-Ray?
                    Question = "What direction was button {1} in {0}?",
                },
                [SNotXRay.Buttons] = new()
                {
                    // English: Which button went {1} in {0}?
                    // Example: Which button went up in Not X-Ray?
                    Question = "Which button went {1} in {0}?",
                    Arguments = new()
                    {
                        ["up"] = "up",
                        ["right"] = "right",
                        ["down"] = "down",
                        ["left"] = "left",
                    },
                },
            },
        },

        [typeof(SNumberedButtons)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNumberedButtons.Buttons] = new()
                {
                    // English: Which number was correctly pressed on {0}?
                    Question = "Which number was correctly pressed on {0}?",
                },
            },
        },

        [typeof(SNumberGame)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNumberGame.Maximum] = new()
                {
                    // English: What was the maximum number in {0}?
                    Question = "What was the maximum number in {0}?",
                },
            },
        },

        [typeof(SNumbers)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNumbers.TwoDigit] = new()
                {
                    // English: What two-digit number was given in {0}?
                    Question = "What two-digit number was given in {0}?",
                },
            },
        },

        [typeof(SNumpath)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNumpath.Color] = new()
                {
                    // English: What was the color of the number on {0}?
                    Question = "What was the color of the number on {0}?",
                },
                [SNumpath.Digit] = new()
                {
                    // English: What was the number displayed on {0}?
                    Question = "What was the number displayed on {0}?",
                },
            },
        },

        [typeof(SObjectShows)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SObjectShows.Contestants] = new()
                {
                    // English: Which of these was a contestant on {0}?
                    Question = "Which of these was a contestant on {0}?",
                },
            },
        },

        [typeof(SOctadecayotton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOctadecayotton.Sphere] = new()
                {
                    // English: What was the starting sphere in {0}?
                    Question = "What was the starting sphere in {0}?",
                },
                [SOctadecayotton.Rotations] = new()
                {
                    // English: What was one of the subrotations in the {1} rotation in {0}?
                    // Example: What was one of the subrotations in the first rotation in Octadecayotton?
                    Question = "What was one of the subrotations in the {1} rotation in {0}?",
                },
            },
        },

        [typeof(SOddOneOut)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOddOneOut.Button] = new()
                {
                    // English: What was the button you pressed in the {1} stage of {0}?
                    // Example: What was the button you pressed in the first stage of Odd One Out?
                    Question = "What was the button you pressed in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SOffKeys)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOffKeys.IncorrectPitch] = new()
                {
                    // English: Which of these keys played at an incorrect pitch in {0}?
                    Question = "Which of these keys played at an incorrect pitch in {0}?",
                },
                [SOffKeys.Runes] = new()
                {
                    // English: Which of these runes was displayed in {0}?
                    Question = "Which of these runes was displayed in {0}?",
                },
            },
        },

        [typeof(SOldAI)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOldAI.Group] = new()
                {
                    // English: What was the {1} of the numbers shown in {0}?
                    // Example: What was the group of the numbers shown in Old AI?
                    Question = "What was the {1} of the numbers shown in {0}?",
                    Arguments = new()
                    {
                        ["group"] = "group",
                        ["sub-group"] = "sub-group",
                    },
                },
            },
        },

        [typeof(SOldFogey)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOldFogey.StartingColor] = new()
                {
                    // English: What was the initial color of the status light in {0}?
                    Question = "What was the initial color of the status light in {0}?",
                },
            },
        },

        [typeof(SOneLinksToAll)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOneLinksToAll.Start] = new()
                {
                    // English: What was the starting article in {0}?
                    Question = "What was the starting article in {0}?",
                },
                [SOneLinksToAll.End] = new()
                {
                    // English: What was the ending article in {0}?
                    Question = "What was the ending article in {0}?",
                },
            },
        },

        [typeof(SOnlyConnect)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOnlyConnect.Hieroglyphs] = new()
                {
                    // English: Which Egyptian hieroglyph was in the {1} in {0}?
                    // Example: Which Egyptian hieroglyph was in the top left in Only Connect?
                    Question = "Which Egyptian hieroglyph was in the {1} in {0}?",
                    Arguments = new()
                    {
                        ["top left"] = "top left",
                        ["top middle"] = "top middle",
                        ["top right"] = "top right",
                        ["bottom left"] = "bottom left",
                        ["bottom middle"] = "bottom middle",
                        ["bottom right"] = "bottom right",
                    },
                },
            },
        },

        [typeof(SOrangeArrows)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOrangeArrows.Sequences] = new()
                {
                    // English: What was the {1} arrow on the display of the {2} stage of {0}?
                    // Example: What was the first arrow on the display of the first stage of Orange Arrows?
                    Question = "What was the {1} arrow on the display of the {2} stage of {0}?",
                },
            },
        },

        [typeof(SOrangeCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOrangeCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Orange Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SOrderedKeys)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOrderedKeys.Colors] = new()
                {
                    // English: What color was this key in the {1} stage of {0}?
                    // Example: What color was this key in the first stage of Ordered Keys?
                    Question = "What color was this key in the {1} stage of {0}?",
                },
                [SOrderedKeys.Labels] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}?
                    // Example: What was the label of this key in the first stage of Ordered Keys?
                    Question = "What was the label of this key in the {1} stage of {0}?",
                },
                [SOrderedKeys.LabelColors] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}?
                    // Example: What color was the label of this key in the first stage of Ordered Keys?
                    Question = "What color was the label of this key in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SOrderPicking)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOrderPicking.Order] = new()
                {
                    // English: What was the order ID in the {1} order of {0}?
                    // Example: What was the order ID in the first order of Order Picking?
                    Question = "What was the order ID in the {1} order of {0}?",
                },
                [SOrderPicking.Product] = new()
                {
                    // English: What was the product ID in the {1} order of {0}?
                    // Example: What was the product ID in the first order of Order Picking?
                    Question = "What was the product ID in the {1} order of {0}?",
                },
                [SOrderPicking.Pallet] = new()
                {
                    // English: What was the pallet in the {1} order of {0}?
                    // Example: What was the pallet in the first order of Order Picking?
                    Question = "What was the pallet in the {1} order of {0}?",
                },
            },
        },

        [typeof(SOrientationCube)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOrientationCube.InitialObserverPosition] = new()
                {
                    // English: What was the observer’s initial position in {0}?
                    Question = "What was the observer’s initial position in {0}?",
                },
            },
        },

        [typeof(SOrientationHypercube)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOrientationHypercube.InitialFaceColour] = new()
                {
                    // English: What was the initial colour of the {1} face in {0}?
                    // Example: What was the initial colour of the right face in Orientation Hypercube?
                    Question = "What was the initial colour of the {1} face in {0}?",
                    Arguments = new()
                    {
                        ["right"] = "right",
                        ["left"] = "left",
                        ["top"] = "top",
                        ["bottom"] = "bottom",
                        ["back"] = "back",
                        ["front"] = "front",
                        ["zag"] = "zag",
                        ["zig"] = "zig",
                    },
                },
                [SOrientationHypercube.InitialObserverPosition] = new()
                {
                    // English: What was the observer’s initial position in {0}?
                    Question = "What was the observer’s initial position in {0}?",
                },
            },
        },

        [typeof(SPalindromes)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPalindromes.Numbers] = new()
                {
                    // English: What was {1}’s {2} digit from the right in {0}?
                    // Example: What was X’s first digit from the right in Palindromes?
                    Question = "What was {1}’s {2} digit from the right in {0}?",
                    Arguments = new()
                    {
                        ["X"] = "X",
                        ["Y"] = "Y",
                        ["Z"] = "Z",
                        ["the screen"] = "the screen",
                    },
                },
            },
        },

        [typeof(SPapasPizzeria)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPapasPizzeria.Letter] = new()
                {
                    // English: What was the letter in the order number on {0}?
                    Question = "What was the letter in the order number on {0}?",
                },
                [SPapasPizzeria.Digit] = new()
                {
                    // English: What was the {1} digit in the order number on {0}?
                    // Example: What was the first digit in the order number on Papa’s Pizzeria?
                    Question = "What was the {1} digit in the order number on {0}?",
                },
            },
        },

        [typeof(SParity)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SParity.Display] = new()
                {
                    // English: What was shown on the display on {0}?
                    Question = "What was shown on the display on {0}?",
                },
            },
        },

        [typeof(SPartialDerivatives)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPartialDerivatives.LedColors] = new()
                {
                    // English: What was the LED color in the {1} stage of {0}?
                    // Example: What was the LED color in the first stage of Partial Derivatives?
                    Question = "What was the LED color in the {1} stage of {0}?",
                },
                [SPartialDerivatives.Terms] = new()
                {
                    // English: What was the {1} term in {0}?
                    // Example: What was the first term in Partial Derivatives?
                    Question = "What was the {1} term in {0}?",
                },
            },
        },

        [typeof(SPassportControl)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPassportControl.Passenger] = new()
                {
                    // English: What was the passport expiration year of the {1} inspected passenger in {0}?
                    // Example: What was the passport expiration year of the first inspected passenger in Passport Control?
                    Question = "What was the passport expiration year of the {1} inspected passenger in {0}?",
                },
            },
        },

        [typeof(SPasswordDestroyer)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPasswordDestroyer.TwoFactorV2] = new()
                {
                    // English: What was the 2FAST™ value when you solved {0}?
                    Question = "What was the 2FAST™ value when you solved {0}?",
                },
            },
        },

        [typeof(SPatternCube)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPatternCube.HighlightedSymbol] = new()
                {
                    // English: Which symbol was highlighted in {0}?
                    Question = "Which symbol was highlighted in {0}?",
                },
            },
        },

        [typeof(SPentabutton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPentabutton.BaseColor] = new()
                {
                    // English: What was the base colour in {0}?
                    Question = "What was the base colour in {0}?",
                },
            },
            Discriminators = new()
            {
                [SPentabutton.Label] = new()
                {
                    // English: the Pentabutton labelled “{0}”
                    // Example: the Pentabutton labelled “press”
                    Discriminator = "the Pentabutton labelled “{0}”",
                },
            },
        },

        [typeof(SPeriodicWords)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPeriodicWords.DisplayedWords] = new()
                {
                    // English: What word was on the display in the {1} stage of {0}?
                    // Example: What word was on the display in the first stage of Periodic Words?
                    Question = "What word was on the display in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SPerspectivePegs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPerspectivePegs.ColorSequence] = new()
                {
                    // English: What was the {1} color in the initial sequence in {0}?
                    // Example: What was the first color in the initial sequence in Perspective Pegs?
                    Question = "What was the {1} color in the initial sequence in {0}?",
                },
            },
        },

        [typeof(SPhosphorescence)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPhosphorescence.ButtonPresses] = new()
                {
                    // English: What was the {1} button press in {0}?
                    // Example: What was the first button press in Phosphorescence?
                    Question = "What was the {1} button press in {0}?",
                },
                [SPhosphorescence.Offset] = new()
                {
                    // English: What was the offset in {0}?
                    Question = "What was the offset in {0}?",
                },
            },
        },

        [typeof(SPickupIdentification)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPickupIdentification.Item] = new()
                {
                    // English: What pickup was shown in the {1} stage of {0}?
                    // Example: What pickup was shown in the first stage of Pickup Identification?
                    Question = "What pickup was shown in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SPictionary)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPictionary.Code] = new()
                {
                    // English: What was the code in {0}?
                    Question = "What was the code in {0}?",
                },
            },
        },

        [typeof(SPie)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPie.Digits] = new()
                {
                    // English: What was the {1} digit of the displayed number in {0}?
                    // Example: What was the first digit of the displayed number in Pie?
                    Question = "What was the {1} digit of the displayed number in {0}?",
                },
            },
        },

        [typeof(SPieFlash)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPieFlash.Digits] = new()
                {
                    // English: What number was not displayed in {0}?
                    Question = "What number was not displayed in {0}?",
                },
            },
        },

        [typeof(SPigpenCycle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPigpenCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Pigpen Cycle?
                    Question = "Which direction was the {1} dial pointing in {0}?",
                },
                [SPigpenCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Pigpen Cycle?
                    Question = "What letter was written on the {1} dial in {0}?",
                },
            },
        },

        [typeof(SPinkButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPinkButton.Words] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the first word in Pink Button?
                    Question = "What was the {1} word in {0}?",
                },
                [SPinkButton.Colors] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in Pink Button?
                    Question = "What was the {1} color in {0}?",
                },
            },
        },

        [typeof(SPinpoint)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPinpoint.Points] = new()
                {
                    // English: Which point occurred in {0}?
                    Question = "Which point occurred in {0}?",
                },
                [SPinpoint.Distances] = new()
                {
                    // English: Which distance occurred in {0}?
                    Question = "Which distance occurred in {0}?",
                },
            },
        },

        [typeof(SPixelCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPixelCipher.Keyword] = new()
                {
                    // English: What was the keyword in {0}?
                    Question = "What was the keyword in {0}?",
                },
            },
        },

        [typeof(SPlaceholderTalk)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPlaceholderTalk.FirstPhrase] = new()
                {
                    // English: What was the first half of the first phrase in {0}?
                    Question = "What was the first half of the first phrase in {0}?",
                },
                [SPlaceholderTalk.Ordinal] = new()
                {
                    // English: What was the last half of the first phrase in {0}?
                    Question = "What was the last half of the first phrase in {0}?",
                },
            },
        },

        [typeof(SPlacementRoulette)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPlacementRoulette.Char] = new()
                {
                    // English: What was the character listed on the information display in {0}?
                    Question = "What was the character listed on the information display in {0}?",
                },
                [SPlacementRoulette.Track] = new()
                {
                    // English: What was the track listed on the information display in {0}?
                    Question = "What was the track listed on the information display in {0}?",
                },
                [SPlacementRoulette.Vehicle] = new()
                {
                    // English: What was the vehicle listed on the information display in {0}?
                    Question = "What was the vehicle listed on the information display in {0}?",
                },
            },
        },

        [typeof(SPlanets)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPlanets.Strips] = new()
                {
                    // English: What was the color of the {1} strip (from the top) in {0}?
                    // Example: What was the color of the first strip (from the top) in Planets?
                    Question = "What was the color of the {1} strip (from the top) in {0}?",
                },
                [SPlanets.Planet] = new()
                {
                    // English: What was the planet shown in {0}?
                    Question = "What was the planet shown in {0}?",
                },
            },
        },

        [typeof(SPlayfairCycle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPlayfairCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Playfair Cycle?
                    Question = "Which direction was the {1} dial pointing in {0}?",
                },
                [SPlayfairCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Playfair Cycle?
                    Question = "What letter was written on the {1} dial in {0}?",
                },
            },
        },

        [typeof(SPoetry)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPoetry.Answers] = new()
                {
                    // English: What was the {1} correct answer you pressed in {0}?
                    // Example: What was the first correct answer you pressed in Poetry?
                    Question = "What was the {1} correct answer you pressed in {0}?",
                },
            },
        },

        [typeof(SPointlessMachines)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPointlessMachines.Flashes] = new()
                {
                    // English: What color flashed {1} in {0}?
                    // Example: What color flashed first in Pointless Machines?
                    Question = "What color flashed {1} in {0}?",
                },
            },
        },

        [typeof(SPolygons)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPolygons.Polygon] = new()
                {
                    // English: Which polygon was present on {0}?
                    Question = "Which polygon was present on {0}?",
                },
            },
        },

        [typeof(SPolyhedralMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPolyhedralMaze.StartPosition] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "What was the starting position in {0}?",
                },
            },
            Discriminators = new()
            {
                [SPolyhedralMaze.MazeShape] = new()
                {
                    // English: {0}
                    // Example: the 4-truncated deltoidal icositetrahedral Polyhedral Maze
                    Discriminator = "{0}",
                },
            },
        },

        [typeof(SPrimeEncryption)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPrimeEncryption.DisplayedValue] = new()
                {
                    // English: What was the number shown in {0}?
                    Question = "What was the number shown in {0}?",
                },
            },
        },

        [typeof(SPrisonBreak)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPrisonBreak.Prisoner] = new()
                {
                    // English: Which cell did the prisoner start in in {0}?
                    Question = "Which cell did the prisoner start in in {0}?",
                },
                [SPrisonBreak.Defuser] = new()
                {
                    // English: Where did you start in {0}?
                    Question = "Where did you start in {0}?",
                },
            },
        },

        [typeof(SProbing)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SProbing.Frequencies] = new()
                {
                    // English: What was the missing frequency in the {1} wire in {0}?
                    // Example: What was the missing frequency in the red-white wire in Probing?
                    Question = "What was the missing frequency in the {1} wire in {0}?",
                    Arguments = new()
                    {
                        ["red-white"] = "red-white",
                        ["yellow-black"] = "yellow-black",
                        ["green"] = "green",
                        ["gray"] = "gray",
                        ["yellow-red"] = "yellow-red",
                        ["red-blue"] = "red-blue",
                    },
                },
            },
        },

        [typeof(SProceduralMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SProceduralMaze.InitialSeed] = new()
                {
                    // English: What was the initial seed in {0}?
                    Question = "What was the initial seed in {0}?",
                },
            },
        },

        [typeof(SPunctuationMarks)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPunctuationMarks.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "What was the displayed number in {0}?",
                },
            },
        },

        [typeof(SPurpleArrows)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPurpleArrows.Finish] = new()
                {
                    // English: What was the target word on {0}?
                    Question = "What was the target word on {0}?",
                },
            },
        },

        [typeof(SPurpleButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPurpleButton.Numbers] = new()
                {
                    // English: What was the {1} number in the cyclic sequence on {0}?
                    // Example: What was the first number in the cyclic sequence on Purple Button?
                    Question = "What was the {1} number in the cyclic sequence on {0}?",
                },
            },
        },

        [typeof(SPuzzleIdentification)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPuzzleIdentification.Num] = new()
                {
                    // English: What was the {1} puzzle number in {0}?
                    // Example: What was the first puzzle number in Puzzle Identification?
                    Question = "What was the {1} puzzle number in {0}?",
                },
                [SPuzzleIdentification.Game] = new()
                {
                    // English: What game was the {1} puzzle in {0} from?
                    // Example: What game was the first puzzle in Puzzle Identification from?
                    Question = "What game was the {1} puzzle in {0} from?",
                },
                [SPuzzleIdentification.Name] = new()
                {
                    // English: What was the {1} puzzle in {0}?
                    // Example: What was the first puzzle in Puzzle Identification?
                    Question = "What was the {1} puzzle in {0}?",
                },
            },
        },

        [typeof(SPuzzlingHexabuttons)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPuzzlingHexabuttons.Letter] = new()
                {
                    // English: What letter was displayed on the {1} hexabutton when submitting in {0}?
                    // Example: What letter was displayed on the top-left hexabutton when submitting in Puzzling Hexabuttons?
                    Question = "What letter was displayed on the {1} hexabutton when submitting in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-right"] = "top-right",
                        ["middle-left"] = "middle-left",
                        ["center"] = "center",
                        ["middle-right"] = "middle-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
            },
        },

        [typeof(SQnA)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQnA.Questions] = new()
                {
                    // English: What was the {1} question asked in {0}?
                    // Example: What was the first question asked in Q & A?
                    Question = "What was the {1} question asked in {0}?",
                },
            },
        },

        [typeof(SQuadrants)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQuadrants.Buttons] = new()
                {
                    // English: What was on the {1} button of the {2} stage in {0}?
                    // Example: What was on the first button of the first stage in Quadrants?
                    Question = "What was on the {1} button of the {2} stage in {0}?",
                },
            },
        },

        [typeof(SQuantumPasswords)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQuantumPasswords.Word] = new()
                {
                    // English: Which word was used in {0}?
                    Question = "Which word was used in {0}?",
                },
            },
        },

        [typeof(SQuantumTernaryConverter)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQuantumTernaryConverter.Number] = new()
                {
                    // English: Which number was shown in {0}?
                    Question = "Which number was shown in {0}?",
                },
            },
        },

        [typeof(SQuaver)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQuaver.Arrows] = new()
                {
                    // English: What was the {1} sequence’s answer in {0}?
                    // Example: What was the first sequence’s answer in Quaver?
                    Question = "What was the {1} sequence’s answer in {0}?",
                },
            },
        },

        [typeof(SQuestionMark)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQuestionMark.FlashedSymbols] = new()
                {
                    // English: Which of these symbols was part of the flashing sequence in {0}?
                    Question = "Which of these symbols was part of the flashing sequence in {0}?",
                },
            },
        },

        [typeof(SQuickArithmetic)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQuickArithmetic.Colors] = new()
                {
                    // English: What was the {1} color in the primary sequence in {0}?
                    // Example: What was the first color in the primary sequence in Quick Arithmetic?
                    Question = "What was the {1} color in the primary sequence in {0}?",
                },
                [SQuickArithmetic.PrimSecDigits] = new()
                {
                    // English: What was the {1} digit in the {2} sequence in {0}?
                    // Example: What was the first digit in the primary sequence in Quick Arithmetic?
                    Question = "What was the {1} digit in the {2} sequence in {0}?",
                    Arguments = new()
                    {
                        ["primary"] = "primary",
                        ["secondary"] = "secondary",
                    },
                },
            },
        },

        [typeof(SQuintuples)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQuintuples.Numbers] = new()
                {
                    // English: What was the {1} digit in the {2} slot in {0}?
                    // Example: What was the first digit in the first slot in Quintuples?
                    Question = "What was the {1} digit in the {2} slot in {0}?",
                },
                [SQuintuples.Colors] = new()
                {
                    // English: What color was the {1} digit in the {2} slot in {0}?
                    // Example: What color was the first digit in the first slot in Quintuples?
                    Question = "What color was the {1} digit in the {2} slot in {0}?",
                },
                [SQuintuples.ColorCounts] = new()
                {
                    // English: How many numbers were {1} in {0}?
                    // Example: How many numbers were red in Quintuples?
                    Question = "How many numbers were {1} in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["blue"] = "blue",
                        ["orange"] = "orange",
                        ["green"] = "green",
                        ["pink"] = "pink",
                    },
                },
            },
        },

        [typeof(SQuiplash)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQuiplash.Number] = new()
                {
                    // English: What number was shown on {0}?
                    Question = "What number was shown on {0}?",
                },
            },
        },

        [typeof(SQuizBuzz)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQuizBuzz.StartingNumber] = new()
                {
                    // English: What was the number initially on the display in {0}?
                    Question = "What was the number initially on the display in {0}?",
                },
            },
        },

        [typeof(SQwirkle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQwirkle.TilesPlaced] = new()
                {
                    // English: What tile did you place {1} in {0}?
                    // Example: What tile did you place first in Qwirkle?
                    Question = "What tile did you place {1} in {0}?",
                },
            },
        },

        [typeof(SRaidingTemples)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRaidingTemples.StartingCommonPool] = new()
                {
                    // English: How many jewels were in the starting common pool in {0}?
                    Question = "How many jewels were in the starting common pool in {0}?",
                },
            },
        },

        [typeof(SRailwayCargoLoading)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRailwayCargoLoading.Cars] = new()
                {
                    // English: What was the {1} car in {0}?
                    // Example: What was the first car in Railway Cargo Loading?
                    Question = "What was the {1} car in {0}?",
                },
                [SRailwayCargoLoading.FreightTableRules] = new()
                {
                    // English: Which freight table rule {1} in {0}?
                    // Example: Which freight table rule was met in Railway Cargo Loading?
                    Question = "Which freight table rule {1} in {0}?",
                    Arguments = new()
                    {
                        ["was met"] = "was met",
                        ["wasn’t met"] = "wasn’t met",
                    },
                },
            },
        },

        [typeof(SRainbowArrows)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRainbowArrows.Number] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "What was the displayed number in {0}?",
                },
            },
        },

        [typeof(SRecoloredSwitches)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRecoloredSwitches.LedColors] = new()
                {
                    // English: What was the color of the {1} LED in {0}?
                    // Example: What was the color of the first LED in Recolored Switches?
                    Question = "What was the color of the {1} LED in {0}?",
                },
            },
        },

        [typeof(SRecursivePassword)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRecursivePassword.NonPasswordWords] = new()
                {
                    // English: Which of these words appeared, but was not the password, in {0}?
                    Question = "Which of these words appeared, but was not the password, in {0}?",
                },
                [SRecursivePassword.Password] = new()
                {
                    // English: What was the password in {0}?
                    Question = "What was the password in {0}?",
                },
            },
        },

        [typeof(SRedArrows)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRedArrows.StartNumber] = new()
                {
                    // English: What was the starting number in {0}?
                    Question = "What was the starting number in {0}?",
                },
            },
        },

        [typeof(SRedButtont)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRedButtont.Word] = new()
                {
                    // English: What was the word before “SUBMIT” in {0}?
                    Question = "What was the word before “SUBMIT” in {0}?",
                },
            },
        },

        [typeof(SRedCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRedCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Red Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SRedHerring)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRedHerring.FirstFlash] = new()
                {
                    // English: What was the first color flashed by {0}?
                    Question = "What was the first color flashed by {0}?",
                },
            },
        },

        [typeof(SReformedRoleReversal)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SReformedRoleReversal.Condition] = new()
                {
                    // English: Which condition was the solving condition in {0}?
                    Question = "Which condition was the solving condition in {0}?",
                },
                [SReformedRoleReversal.Wire] = new()
                {
                    // English: What color was the {1} wire in {0}?
                    // Example: What color was the first wire in Reformed Role Reversal?
                    Question = "What color was the {1} wire in {0}?",
                },
            },
        },

        [typeof(SReGretBFiltering)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SReGretBFiltering.Operator] = new()
                {
                    // English: Which calculation was used for the {1} stage of {0}?
                    // Example: Which calculation was used for the first stage of ReGret-B Filtering?
                    Question = "Which calculation was used for the {1} stage of {0}?",
                },
            },
        },

        [typeof(SRegularCrazyTalk)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRegularCrazyTalk.Digit] = new()
                {
                    // English: What was the displayed digit that corresponded to the solution phrase in {0}?
                    Question = "What was the displayed digit that corresponded to the solution phrase in {0}?",
                },
                [SRegularCrazyTalk.Modifier] = new()
                {
                    // English: What was the embellishment of the solution phrase in {0}?
                    Question = "What was the embellishment of the solution phrase in {0}?",
                },
            },
        },

        [typeof(SReorderedKeys)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SReorderedKeys.KeyColor] = new()
                {
                    // English: What color was this key in the {1} stage of {0}?
                    // Example: What color was this key in the first stage of Reordered Keys?
                    Question = "What color was this key in the {1} stage of {0}?",
                },
                [SReorderedKeys.LabelColor] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}?
                    // Example: What color was the label of this key in the first stage of Reordered Keys?
                    Question = "What color was the label of this key in the {1} stage of {0}?",
                },
                [SReorderedKeys.Label] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}?
                    // Example: What was the label of this key in the first stage of Reordered Keys?
                    Question = "What was the label of this key in the {1} stage of {0}?",
                },
                [SReorderedKeys.Pivot] = new()
                {
                    // English: Which key was the pivot in the {1} stage of {0}?
                    Question = "Which key was the pivot in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SRetirement)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRetirement.Houses] = new()
                {
                    // English: Which one of these houses was on offer, but not chosen by Bob in {0}?
                    Question = "Which one of these houses was on offer, but not chosen by Bob in {0}?",
                },
            },
        },

        [typeof(SReverseMorse)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SReverseMorse.Characters] = new()
                {
                    // English: What was the {1} character in the {2} message of {0}?
                    // Example: What was the first character in the first message of Reverse Morse?
                    Question = "What was the {1} character in the {2} message of {0}?",
                },
            },
        },

        [typeof(SReversePolishNotation)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SReversePolishNotation.Character] = new()
                {
                    // English: What character was used in the {1} round of {0}?
                    // Example: What character was used in the first round of Reverse Polish Notation?
                    Question = "What character was used in the {1} round of {0}?",
                },
            },
        },

        [typeof(SRGBMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRGBMaze.Keys] = new()
                {
                    // English: Where was the {1} key in {0}?
                    // Example: Where was the red key in RGB Maze?
                    Question = "Where was the {1} key in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
                [SRGBMaze.Number] = new()
                {
                    // English: Which maze number was the {1} maze in {0}?
                    // Example: Which maze number was the red maze in RGB Maze?
                    Question = "Which maze number was the {1} maze in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
                [SRGBMaze.Exit] = new()
                {
                    // English: What was the exit coordinate in {0}?
                    Question = "What was the exit coordinate in {0}?",
                },
            },
        },

        [typeof(SRGBSequences)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRGBSequences.Display] = new()
                {
                    // English: What was the color of the {1} LED in {0}?
                    // Example: What was the color of the first LED in RGB Sequences?
                    Question = "What was the color of the {1} LED in {0}?",
                },
            },
        },

        [typeof(SRhythms)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRhythms.Color] = new()
                {
                    // English: What was the color in {0}?
                    Question = "What was the color in {0}?",
                },
            },
        },

        [typeof(SRNGCrystal)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRNGCrystal.Taps] = new()
                {
                    // English: Which bit had a tap in {0} (the output after shifting is at bit 0)?
                    Question = "Which bit had a tap in {0} (the output after shifting is at bit 0)?",
                },
            },
        },

        [typeof(SRoboScanner)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRoboScanner.EmptyCell] = new()
                {
                    // English: Where was the empty cell in {0}?
                    Question = "Where was the empty cell in {0}?",
                },
            },
        },

        [typeof(SRobotProgramming)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRobotProgramming.Color] = new()
                {
                    // English: What was the color of the {1} robot in {0}?
                    // Example: What was the color of the first robot in Robot Programming?
                    Question = "What was the color of the {1} robot in {0}?",
                },
                [SRobotProgramming.Shape] = new()
                {
                    // English: What was the shape of the {1} robot in {0}?
                    // Example: What was the shape of the first robot in Robot Programming?
                    Question = "What was the shape of the {1} robot in {0}?",
                },
            },
        },

        [typeof(SRoger)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRoger.Seed] = new()
                {
                    // English: What four-digit number was given in {0}?
                    Question = "What four-digit number was given in {0}?",
                },
            },
        },

        [typeof(SRoleReversal)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRoleReversal.Wires] = new()
                {
                    // English: How many {1} wires were there in {0}?
                    // Example: How many warm-colored wires were there in Role Reversal?
                    Question = "How many {1} wires were there in {0}?",
                    Arguments = new()
                    {
                        ["warm-colored"] = "warm-colored",
                        ["cold-colored"] = "cold-colored",
                        ["primary-colored"] = "primary-colored",
                        ["secondary-colored"] = "secondary-colored",
                    },
                },
                [SRoleReversal.Number] = new()
                {
                    // English: What was the number corresponding to the correct condition in {0}?
                    Question = "What was the number corresponding to the correct condition in {0}?",
                },
            },
        },

        [typeof(SRPSJudging)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRPSJudging.QWinner] = new()
                {
                    // English: Which round did the {1} team {2} in {0}?
                    // Example: Which round did the red team win in RPS Judging?
                    Question = "Which round did the {1} team {2} in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["win"] = "win",
                        ["blue"] = "blue",
                        ["lose"] = "lose",
                    },
                },
                [SRPSJudging.QDraw] = new()
                {
                    // English: Which round was a draw in {0}?
                    Question = "Which round was a draw in {0}?",
                },
            },
            Discriminators = new()
            {
                [SRPSJudging.DWinner] = new()
                {
                    // English: the RPS Judging where the {0} team {1} the {2} round
                    // Example: the RPS Judging where the red team won the first round
                    Discriminator = "the RPS Judging where the {0} team {1} the {2} round",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["won"] = "won",
                        ["lost"] = "lost",
                        ["blue"] = "blue",
                    },
                },
                [SRPSJudging.DDraw] = new()
                {
                    // English: the RPS Judging with a draw in the {0} round
                    // Example: the RPS Judging with a draw in the first round
                    Discriminator = "the RPS Judging with a draw in the {0} round",
                },
            },
        },

        [typeof(SRule)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRule.Number] = new()
                {
                    // English: What was the rule number in {0}?
                    Question = "What was the rule number in {0}?",
                },
            },
        },

        [typeof(SRuleOfThree)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRuleOfThree.Coordinates] = new()
                {
                    // English: What was the {1} coordinate of the {2} vertex in {0}?
                    // Example: What was the X coordinate of the red vertex in Rule of Three?
                    Question = "What was the {1} coordinate of the {2} vertex in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["yellow"] = "yellow",
                        ["blue"] = "blue",
                    },
                },
                [SRuleOfThree.Cycles] = new()
                {
                    // English: What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?
                    // Example: What was the position of the red sphere on the X axis in the first cycle in Rule of Three?
                    Question = "What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["yellow"] = "yellow",
                        ["blue"] = "blue",
                    },
                },
            },
        },

        [typeof(SSafetySquare)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSafetySquare.Digits] = new()
                {
                    // English: What was the digit displayed on the {1} diamond in {0}?
                    // Example: What was the digit displayed on the red diamond in Safety Square?
                    Question = "What was the digit displayed on the {1} diamond in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["yellow"] = "yellow",
                        ["blue"] = "blue",
                    },
                },
                [SSafetySquare.SpecialRule] = new()
                {
                    // English: What was the special rule displayed on the white diamond in {0}?
                    Question = "What was the special rule displayed on the white diamond in {0}?",
                },
            },
        },

        [typeof(SSamsung)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSamsung.AppPositions] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was Duolingo in Samsung?
                    Question = "Where was {1} in {0}?",
                },
            },
        },

        [typeof(SSaturn)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSaturn.Goal] = new()
                {
                    // English: Where was the goal in {0}?
                    Question = "Where was the goal in {0}?",
                },
            },
        },

        [typeof(SSbemailSongs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSbemailSongs.Songs] = new()
                {
                    // English: What was the displayed song for stage {1} (hexadecimal) of {0}?
                    // Example: What was the displayed song for stage 01 (hexadecimal) of Sbemail Songs?
                    Question = "What was the displayed song for stage {1} (hexadecimal) of {0}?",
                },
            },
            Discriminators = new()
            {
                [SSbemailSongs.Digits] = new()
                {
                    // English: the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)
                    // Example: the Sbemail Songs which displayed ‘Oh, who is the guy that…’ in stage 01 (hexadecimal)
                    Discriminator = "the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)",
                },
            },
        },

        [typeof(SScavengerHunt)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SScavengerHunt.KeySquare] = new()
                {
                    // English: Which tile was correctly submitted in the first stage of {0}?
                    Question = "Which tile was correctly submitted in the first stage of {0}?",
                },
                [SScavengerHunt.ColoredTiles] = new()
                {
                    // English: Which of these tiles was {1} in the first stage of {0}?
                    // Example: Which of these tiles was red in the first stage of Scavenger Hunt?
                    Question = "Which of these tiles was {1} in the first stage of {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
            },
        },

        [typeof(SSchlagDenBomb)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSchlagDenBomb.ContestantName] = new()
                {
                    // English: What was the contestant’s name in {0}?
                    Question = "What was the contestant’s name in {0}?",
                },
                [SSchlagDenBomb.ContestantScore] = new()
                {
                    // English: What was the contestant’s score in {0}?
                    Question = "What was the contestant’s score in {0}?",
                },
                [SSchlagDenBomb.BombScore] = new()
                {
                    // English: What was the bomb’s score in {0}?
                    Question = "What was the bomb’s score in {0}?",
                },
            },
        },

        [typeof(SScramboozledEggain)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SScramboozledEggain.Word] = new()
                {
                    // English: What was the {1} encrypted word in {0}?
                    // Example: What was the first encrypted word in Scramboozled Eggain?
                    Question = "What was the {1} encrypted word in {0}?",
                },
            },
        },

        [typeof(SScripting)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SScripting.VariableDataType] = new()
                {
                    // English: What was the submitted data type of the variable in {0}?
                    Question = "What was the submitted data type of the variable in {0}?",
                },
            },
        },

        [typeof(SScrutinySquares)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SScrutinySquares.FirstDifference] = new()
                {
                    // English: What was the modified property of the first display in {0}?
                    Question = "What was the modified property of the first display in {0}?",
                },
            },
        },

        [typeof(SSeaShells)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSeaShells.Question1] = new()
                {
                    // English: What were the first and second words in the {1} phrase in {0}?
                    // Example: What were the first and second words in the first phrase in Sea Shells?
                    Question = "What were the first and second words in the {1} phrase in {0}?",
                },
                [SSeaShells.Question2] = new()
                {
                    // English: What were the third and fourth words in the {1} phrase in {0}?
                    // Example: What were the third and fourth words in the first phrase in Sea Shells?
                    Question = "What were the third and fourth words in the {1} phrase in {0}?",
                },
                [SSeaShells.Question3] = new()
                {
                    // English: What was the end of the {1} phrase in {0}?
                    // Example: What was the end of the first phrase in Sea Shells?
                    Question = "What was the end of the {1} phrase in {0}?",
                },
            },
        },

        [typeof(SSemamorse)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSemamorse.Color] = new()
                {
                    // English: What was the color of the display involved in the starting value in {0}?
                    Question = "What was the color of the display involved in the starting value in {0}?",
                },
                [SSemamorse.Letters] = new()
                {
                    // English: What was the {1} letter involved in the starting value in {0}?
                    // Example: What was the Morse letter involved in the starting value in Semamorse?
                    Question = "What was the {1} letter involved in the starting value in {0}?",
                    Arguments = new()
                    {
                        ["Morse"] = "Morse",
                        ["semaphore"] = "semaphore",
                    },
                },
            },
        },

        [typeof(SSequencyclopedia)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSequencyclopedia.Sequence] = new()
                {
                    // English: What sequence was used in {0}?
                    Question = "What sequence was used in {0}?",
                },
            },
        },

        [typeof(SSetTheory)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSetTheory.Equations] = new()
                {
                    // English: What equation was shown in the {1} stage of {0}?
                    // Example: What equation was shown in the first stage of S.E.T. Theory?
                    Question = "What equation was shown in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SShapesAndBombs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SShapesAndBombs.InitialLetter] = new()
                {
                    // English: What was the initial letter in {0}?
                    Question = "What was the initial letter in {0}?",
                },
            },
        },

        [typeof(SShapeShift)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SShapeShift.InitialShape] = new()
                {
                    // English: What was the initial shape in {0}?
                    Question = "What was the initial shape in {0}?",
                },
            },
        },

        [typeof(SShiftedMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SShiftedMaze.Colors] = new()
                {
                    // English: What color was the {1} marker in {0}?
                    // Example: What color was the top-left marker in Shifted Maze?
                    Question = "What color was the {1} marker in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-right"] = "top-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
            },
        },

        [typeof(SShiftingMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SShiftingMaze.Seed] = new()
                {
                    // English: What was the seed in {0}?
                    Question = "What was the seed in {0}?",
                },
            },
        },

        [typeof(SShogiIdentification)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SShogiIdentification.Piece] = new()
                {
                    // English: What was the displayed piece in {0}?
                    Question = "What was the displayed piece in {0}?",
                },
            },
        },

        [typeof(SSignLanguage)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSignLanguage.Word] = new()
                {
                    // English: What was the deciphered word in {0}?
                    Question = "What was the deciphered word in {0}?",
                },
            },
        },

        [typeof(SSillySlots)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSillySlots.Question] = new()
                {
                    // English: What was the {1} slot in the {2} stage in {0}?
                    // Example: What was the first slot in the first stage in Silly Slots?
                    Question = "What was the {1} slot in the {2} stage in {0}?",
                },
            },
        },

        [typeof(SSiloAuthorization)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSiloAuthorization.MessageType] = new()
                {
                    // English: What was the message type in {0}?
                    Question = "What was the message type in {0}?",
                },
                [SSiloAuthorization.EncryptedMessage] = new()
                {
                    // English: What was the {1} part of the encrypted message in {0}?
                    // Example: What was the first part of the encrypted message in Silo Authorization?
                    Question = "What was the {1} part of the encrypted message in {0}?",
                },
                [SSiloAuthorization.AuthCode] = new()
                {
                    // English: What was the received authentication code in {0}?
                    Question = "What was the received authentication code in {0}?",
                },
            },
        },

        [typeof(SSimonSaid)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSaid.Flashes] = new()
                {
                    // English: What color flashed {1} in the final sequence of {0}?
                    // Example: What color flashed first in the final sequence of Simon Said?
                    Question = "What color flashed {1} in the final sequence of {0}?",
                },
            },
        },

        [typeof(SSimonSamples)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSamples.Samples] = new()
                {
                    // English: What were the call samples {1} of {0}?
                    // Example: What were the call samples played in the first stage of Simon Samples?
                    Question = "What were the call samples {1} of {0}?",
                    Arguments = new()
                    {
                        ["played in the first stage"] = "played in the first stage",
                        ["added in the second stage"] = "added in the second stage",
                        ["added in the third stage"] = "added in the third stage",
                    },
                },
            },
        },

        [typeof(SSimonSays)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSays.Flash] = new()
                {
                    // English: What color flashed {1} in the final sequence in {0}?
                    // Example: What color flashed first in the final sequence in Simon Says?
                    Question = "What color flashed {1} in the final sequence in {0}?",
                },
            },
        },

        [typeof(SSimonScrambles)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonScrambles.Colors] = new()
                {
                    // English: What color flashed {1} in {0}?
                    // Example: What color flashed first in Simon Scrambles?
                    Question = "What color flashed {1} in {0}?",
                },
            },
        },

        [typeof(SSimonScreams)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonScreams.Flashing] = new()
                {
                    // English: Which color flashed {1} in the final sequence in {0}?
                    // Example: Which color flashed first in the final sequence in Simon Screams?
                    Question = "Which color flashed {1} in the final sequence in {0}?",
                },
                [SSimonScreams.RuleSimple] = new()
                {
                    // English: In which stage(s) of {0} was “{1}” the applicable rule?
                    // Example: In which stage(s) of Simon Screams was “a color flashed, then a color two away, then the first again” the applicable rule?
                    Question = "In which stage(s) of {0} was “{1}” the applicable rule?",
                    Arguments = new()
                    {
                        ["a color flashed, then a color two away, then the first again"] = "a color flashed, then a color two away, then the first again",
                        ["a color flashed, then a color two away, then the one opposite that"] = "a color flashed, then a color two away, then the one opposite that",
                        ["a color flashed, then a color two away, then the one opposite the first"] = "a color flashed, then a color two away, then the one opposite the first",
                        ["a color flashed, then an adjacent color, then the first again"] = "a color flashed, then an adjacent color, then the first again",
                        ["a color flashed, then another color, then the first"] = "a color flashed, then another color, then the first",
                        ["a color flashed, then one adjacent, then the one opposite that"] = "a color flashed, then one adjacent, then the one opposite that",
                        ["a color flashed, then one adjacent, then the one opposite the first"] = "a color flashed, then one adjacent, then the one opposite the first",
                        ["a color flashed, then the one opposite, then one adjacent to that"] = "a color flashed, then the one opposite, then one adjacent to that",
                        ["a color flashed, then the one opposite, then one adjacent to the first"] = "a color flashed, then the one opposite, then one adjacent to the first",
                        ["a color flashed, then the one opposite, then the first again"] = "a color flashed, then the one opposite, then the first again",
                        ["every color flashed at least once"] = "every color flashed at least once",
                        ["exactly one color flashed exactly twice"] = "exactly one color flashed exactly twice",
                        ["exactly one color flashed more than once"] = "exactly one color flashed more than once",
                        ["exactly two colors flashed exactly twice"] = "exactly two colors flashed exactly twice",
                        ["exactly two colors flashed more than once"] = "exactly two colors flashed more than once",
                        ["no color flashed exactly twice"] = "no color flashed exactly twice",
                        ["no color flashed more than once"] = "no color flashed more than once",
                        ["no two adjacent colors flashed in clockwise order"] = "no two adjacent colors flashed in clockwise order",
                        ["no two adjacent colors flashed in counter-clockwise order"] = "no two adjacent colors flashed in counter-clockwise order",
                        ["no two colors two apart flashed in clockwise order"] = "no two colors two apart flashed in clockwise order",
                        ["no two colors two apart flashed in counter-clockwise order"] = "no two colors two apart flashed in counter-clockwise order",
                        ["the colors flashing first and last are adjacent"] = "the colors flashing first and last are adjacent",
                        ["the colors flashing first and last are different and not adjacent"] = "the colors flashing first and last are different and not adjacent",
                        ["the colors flashing first and last are the same"] = "the colors flashing first and last are the same",
                        ["the number of distinct colors that flashed is even"] = "the number of distinct colors that flashed is even",
                        ["the number of distinct colors that flashed is odd"] = "the number of distinct colors that flashed is odd",
                        ["there are at least three colors that didn’t flash"] = "there are at least three colors that didn’t flash",
                        ["there are exactly two colors that didn’t flash"] = "there are exactly two colors that didn’t flash",
                        ["there are two colors adjacent to each other that didn’t flash"] = "there are two colors adjacent to each other that didn’t flash",
                        ["there are two colors opposite each other that didn’t flash"] = "there are two colors opposite each other that didn’t flash",
                        ["there are two colors two away from each other that didn’t flash"] = "there are two colors two away from each other that didn’t flash",
                        ["there is exactly one color that didn’t flash"] = "there is exactly one color that didn’t flash",
                        ["three adjacent colors did not flash"] = "three adjacent colors did not flash",
                        ["three adjacent colors flashed in clockwise order"] = "three adjacent colors flashed in clockwise order",
                        ["three adjacent colors flashed in counter-clockwise order"] = "three adjacent colors flashed in counter-clockwise order",
                        ["three colors, each two apart, flashed in clockwise order"] = "three colors, each two apart, flashed in clockwise order",
                        ["three colors, each two apart, flashed in counter-clockwise order"] = "three colors, each two apart, flashed in counter-clockwise order",
                        ["two adjacent colors flashed in clockwise order"] = "two adjacent colors flashed in clockwise order",
                        ["two adjacent colors flashed in counter-clockwise order"] = "two adjacent colors flashed in counter-clockwise order",
                        ["two colors two apart flashed in clockwise order"] = "two colors two apart flashed in clockwise order",
                        ["two colors two apart flashed in counter-clockwise order"] = "two colors two apart flashed in counter-clockwise order",
                    },
                },
                [SSimonScreams.RuleComplex] = new()
                {
                    // English: In which stage(s) of {0} was “{1} flashed out of {2}, {3}, and {4}” the applicable rule?
                    // Example: In which stage(s) of Simon Screams was “at most one color flashed out of Red, Orange, and Yellow” the applicable rule?
                    Question = "In which stage(s) of {0} was “{1} flashed out of {2}, {3}, and {4}” the applicable rule?",
                    Arguments = new()
                    {
                        ["at most one color"] = "at most one color",
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["at least two colors"] = "at least two colors",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Purple"] = "Purple",
                    },
                },
            },
        },

        [typeof(SSimonSelects)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSelects.Order] = new()
                {
                    // English: Which color flashed {1} in the {2} stage of {0}?
                    // Example: Which color flashed first in the first stage of Simon Selects?
                    Question = "Which color flashed {1} in the {2} stage of {0}?",
                },
            },
        },

        [typeof(SSimonSends)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSends.ReceivedLetters] = new()
                {
                    // English: What was the {1} received letter in {0}?
                    // Example: What was the red received letter in Simon Sends?
                    Question = "What was the {1} received letter in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
            },
        },

        [typeof(SSimonServes)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonServes.Flash] = new()
                {
                    // English: Who flashed {1} in course {2} of {0}?
                    // Example: Who flashed first in course 1 of Simon Serves?
                    Question = "Who flashed {1} in course {2} of {0}?",
                },
                [SSimonServes.Food] = new()
                {
                    // English: Which item was not served in course {1} of {0}?
                    // Example: Which item was not served in course 1 of Simon Serves?
                    Question = "Which item was not served in course {1} of {0}?",
                },
            },
        },

        [typeof(SSimonShapes)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonShapes.SubmittedShape] = new()
                {
                    // English: What was the shape submitted at the end of {0}?
                    Question = "What was the shape submitted at the end of {0}?",
                },
            },
        },

        [typeof(SSimonShouts)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonShouts.FlashingLetter] = new()
                {
                    // English: Which letter flashed on the {1} button in {0}?
                    // Example: Which letter flashed on the top button in Simon Shouts?
                    Question = "Which letter flashed on the {1} button in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["left"] = "left",
                        ["right"] = "right",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SSimonShrieks)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonShrieks.FlashingButton] = new()
                {
                    // English: How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?
                    // Example: How many spaces clockwise from the arrow was the first flash in the final sequence in Simon Shrieks?
                    Question = "How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?",
                },
            },
        },

        [typeof(SSimonShuffles)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonShuffles.Flashes] = new()
                {
                    // English: What was the {1} flash of the {2} stage of {0}?
                    // Example: What was the first flash of the first stage of Simon Shuffles?
                    Question = "What was the {1} flash of the {2} stage of {0}?",
                },
            },
        },

        [typeof(SSimonSignals)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSignals.ColorToShape] = new()
                {
                    // English: What shape was the {1} arrow in {0}?
                    // Example: What shape was the red arrow in Simon Signals?
                    Question = "What shape was the {1} arrow in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["gray"] = "gray",
                    },
                },
                [SSimonSignals.ColorToRotations] = new()
                {
                    // English: How many directions did the {1} arrow in {0} have?
                    // Example: How many directions did the red arrow in Simon Signals have?
                    Question = "How many directions did the {1} arrow in {0} have?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["gray"] = "gray",
                    },
                },
                [SSimonSignals.ShapeToColor] = new()
                {
                    // English: What color was the arrow with this shape in {0}?
                    Question = "What color was the arrow with this shape in {0}?",
                },
                [SSimonSignals.ShapeToRotations] = new()
                {
                    // English: How many directions did the arrow with this shape have in {0}?
                    Question = "How many directions did the arrow with this shape have in {0}?",
                },
                [SSimonSignals.RotationsToColor] = new()
                {
                    // English: What color was the arrow with {1} possible directions in {0}?
                    // Example: What color was the arrow with 3 possible directions in Simon Signals?
                    Question = "What color was the arrow with {1} possible directions in {0}?",
                },
                [SSimonSignals.RotationsToShape] = new()
                {
                    // English: What shape was the arrow with {1} possible directions in {0}?
                    // Example: What shape was the arrow with 3 possible directions in Simon Signals?
                    Question = "What shape was the arrow with {1} possible directions in {0}?",
                },
            },
        },

        [typeof(SSimonSimons)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSimons.FlashingColors] = new()
                {
                    // English: What was the {1} flash in the final sequence in {0}?
                    // Example: What was the first flash in the final sequence in Simon Simons?
                    Question = "What was the {1} flash in the final sequence in {0}?",
                },
            },
        },

        [typeof(SSimonSings)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSings.Flashing] = new()
                {
                    // English: Which key’s color flashed {1} in the {2} stage of {0}?
                    // Example: Which key’s color flashed first in the first stage of Simon Sings?
                    Question = "Which key’s color flashed {1} in the {2} stage of {0}?",
                },
            },
        },

        [typeof(SSimonSmiles)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSmiles.Sounds] = new()
                {
                    // English: What sound did the {1} button press make {0}?
                    // Example: What sound did the first button press make Simon Smiles?
                    Question = "What sound did the {1} button press make {0}?",
                },
            },
        },

        [typeof(SSimonSmothers)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSmothers.Colors] = new()
                {
                    // English: What was the color of the {1} flash in {0}?
                    // Example: What was the color of the first flash in Simon Smothers?
                    Question = "What was the color of the {1} flash in {0}?",
                },
                [SSimonSmothers.Directions] = new()
                {
                    // English: What was the direction of the {1} flash in {0}?
                    // Example: What was the direction of the first flash in Simon Smothers?
                    Question = "What was the direction of the {1} flash in {0}?",
                },
            },
        },

        [typeof(SSimonSounds)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSounds.FlashingColors] = new()
                {
                    // English: Which sample button sounded {1} in the final sequence in {0}?
                    // Example: Which sample button sounded first in the final sequence in Simon Sounds?
                    Question = "Which sample button sounded {1} in the final sequence in {0}?",
                },
            },
        },

        [typeof(SSimonSpeaks)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSpeaks.Positions] = new()
                {
                    // English: Which bubble flashed first in {0}?
                    Question = "Which bubble flashed first in {0}?",
                },
                [SSimonSpeaks.Shapes] = new()
                {
                    // English: Which bubble flashed second in {0}?
                    Question = "Which bubble flashed second in {0}?",
                },
                [SSimonSpeaks.Languages] = new()
                {
                    // English: Which language was the bubble that flashed third in {0} in?
                    Question = "Which language was the bubble that flashed third in {0} in?",
                },
                [SSimonSpeaks.Words] = new()
                {
                    // English: Which word was in the bubble that flashed fourth in {0}?
                    Question = "Which word was in the bubble that flashed fourth in {0}?",
                },
                [SSimonSpeaks.Colors] = new()
                {
                    // English: What color was the bubble that flashed fifth in {0}?
                    Question = "What color was the bubble that flashed fifth in {0}?",
                },
            },
        },

        [typeof(SSimonsStar)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonsStar.Colors] = new()
                {
                    // English: Which color flashed {1} in {0}?
                    // Example: Which color flashed first in Simon’s Star?
                    Question = "Which color flashed {1} in {0}?",
                },
            },
        },

        [typeof(SSimonStacks)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonStacks.Colors] = new()
                {
                    // English: Which color flashed in the {1} stage of {0}?
                    // Example: Which color flashed in the first stage of Simon Stacks?
                    Question = "Which color flashed in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SSimonStages)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonStages.Indicator] = new()
                {
                    // English: What color was the indicator in the {1} stage in {0}?
                    // Example: What color was the indicator in the first stage in Simon Stages?
                    Question = "What color was the indicator in the {1} stage in {0}?",
                },
                [SSimonStages.Flashes] = new()
                {
                    // English: Which color flashed {1} in the {2} stage in {0}?
                    // Example: Which color flashed first in the first stage in Simon Stages?
                    Question = "Which color flashed {1} in the {2} stage in {0}?",
                },
            },
        },

        [typeof(SSimonStates)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonStates.Display] = new()
                {
                    // English: Which {1} in the {2} stage in {0}?
                    // Example: Which color(s) flashed in the first stage in Simon States?
                    Question = "Which {1} in the {2} stage in {0}?",
                    Arguments = new()
                    {
                        ["color(s) flashed"] = "color(s) flashed",
                        ["color(s) didn’t flash"] = "color(s) didn’t flash",
                    },
                },
            },
        },

        [typeof(SSimonStops)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonStops.Colors] = new()
                {
                    // English: Which color flashed {1} in the output sequence in {0}?
                    // Example: Which color flashed first in the output sequence in Simon Stops?
                    Question = "Which color flashed {1} in the output sequence in {0}?",
                },
            },
        },

        [typeof(SSimonStores)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonStores.Colors] = new()
                {
                    // English: Which color {1} {2} in the final sequence of {0}?
                    // Example: Which color flashed first in the final sequence of Simon Stores?
                    Question = "Which color {1} {2} in the final sequence of {0}?",
                    Arguments = new()
                    {
                        ["flashed"] = "flashed",
                        ["was among the colors flashed"] = "was among the colors flashed",
                    },
                },
            },
        },

        [typeof(SSimonSubdivides)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSubdivides.Button] = new()
                {
                    // English: What color was the button at this position in {0}?
                    Question = "What color was the button at this position in {0}?",
                },
            },
        },

        [typeof(SSimonSupports)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSupports.Topics] = new()
                {
                    // English: What was the {1} topic in {0}?
                    // Example: What was the first topic in Simon Supports?
                    Question = "What was the {1} topic in {0}?",
                },
            },
        },

        [typeof(SSimonSwizzles)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSwizzles.Button] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was OFF in Simon Swizzles?
                    Question = "Where was {1} in {0}?",
                    Arguments = new()
                    {
                        ["OFF"] = "OFF",
                        ["ON"] = "ON",
                    },
                },
                [SSimonSwizzles.Number] = new()
                {
                    // English: What was the hidden number in {0}?
                    Question = "What was the hidden number in {0}?",
                },
            },
        },

        [typeof(SSimplySimon)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimplySimon.Flash] = new()
                {
                    // English: What were the flashes in the {1} stage of {0}?
                    // Example: What were the flashes in the first stage of Simply Simon?
                    Question = "What were the flashes in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SSimultaneousSimons)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimultaneousSimons.Flash] = new()
                {
                    // English: What color flashed {1} on the {2} Simon in {0}?
                    // Example: What color flashed first on the first Simon in Simultaneous Simons?
                    Question = "What color flashed {1} on the {2} Simon in {0}?",
                },
            },
        },

        [typeof(SSkewedSlots)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSkewedSlots.OriginalNumbers] = new()
                {
                    // English: What were the original numbers in {0}?
                    Question = "What were the original numbers in {0}?",
                },
            },
        },

        [typeof(SSkewers)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSkewers.Color] = new()
                {
                    // English: What color was this gem in {0}?
                    Question = "What color was this gem in {0}?",
                },
            },
        },

        [typeof(SSkyrim)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSkyrim.Race] = new()
                {
                    // English: Which race was selectable, but not the solution, in {0}?
                    Question = "Which race was selectable, but not the solution, in {0}?",
                },
                [SSkyrim.Weapon] = new()
                {
                    // English: Which weapon was selectable, but not the solution, in {0}?
                    Question = "Which weapon was selectable, but not the solution, in {0}?",
                },
                [SSkyrim.Enemy] = new()
                {
                    // English: Which enemy was selectable, but not the solution, in {0}?
                    Question = "Which enemy was selectable, but not the solution, in {0}?",
                },
                [SSkyrim.City] = new()
                {
                    // English: Which city was selectable, but not the solution, in {0}?
                    Question = "Which city was selectable, but not the solution, in {0}?",
                },
                [SSkyrim.DragonShout] = new()
                {
                    // English: Which dragon shout was selectable, but not the solution, in {0}?
                    Question = "Which dragon shout was selectable, but not the solution, in {0}?",
                },
            },
        },

        [typeof(SSlowMath)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSlowMath.LastLetters] = new()
                {
                    // English: What was the last triplet of letters in {0}?
                    Question = "What was the last triplet of letters in {0}?",
                },
            },
        },

        [typeof(SSmallCircle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSmallCircle.Shift] = new()
                {
                    // English: How much did the sequence shift by in {0}?
                    Question = "How much did the sequence shift by in {0}?",
                },
                [SSmallCircle.Wedge] = new()
                {
                    // English: Which wedge made the different noise in the beginning of {0}?
                    Question = "Which wedge made the different noise in the beginning of {0}?",
                },
                [SSmallCircle.Solution] = new()
                {
                    // English: Which color was {1} in the solution to {0}?
                    // Example: Which color was first in the solution to Small Circle?
                    Question = "Which color was {1} in the solution to {0}?",
                },
            },
        },

        [typeof(SSmallTalk)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSmallTalk.Displays] = new()
                {
                    // English: What was on the display in the {1} stage of {0}?
                    // Example: What was on the display in the first stage of Small Talk?
                    Question = "What was on the display in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SSmashMarryKill)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSmashMarryKill.Category] = new()
                {
                    // English: In what category was {1} for {0}?
                    // Example: In what category was The Button for Smash, Marry, Kill?
                    Question = "In what category was {1} for {0}?",
                },
                [SSmashMarryKill.Module] = new()
                {
                    // English: Which module was in the {1} category for {0}?
                    // Example: Which module was in the SMASH category for Smash, Marry, Kill?
                    Question = "Which module was in the {1} category for {0}?",
                },
            },
            Discriminators = new()
            {
                [SSmashMarryKill.NullDiscriminator] = new()
                {
                    // English: Smash, Marry, Kill
                    Discriminator = "Smash, Marry, Kill",
                },
            },
        },

        [typeof(SSnooker)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSnooker.Reds] = new()
                {
                    // English: How many red balls were there at the start of {0}?
                    Question = "How many red balls were there at the start of {0}?",
                },
            },
        },

        [typeof(SSnowflakes)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSnowflakes.DisplayedSnowflakes] = new()
                {
                    // English: Which snowflake was on the {1} button of {0}?
                    // Example: Which snowflake was on the top button of Snowflakes?
                    Question = "Which snowflake was on the {1} button of {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["right"] = "right",
                        ["bottom"] = "bottom",
                        ["left"] = "left",
                    },
                },
            },
        },

        [typeof(SSonicKnuckles)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSonicKnuckles.Sounds] = new()
                {
                    // English: Which sound was played but not featured in the chosen zone in {0}?
                    Question = "Which sound was played but not featured in the chosen zone in {0}?",
                },
                [SSonicKnuckles.Badnik] = new()
                {
                    // English: Which badnik was shown in {0}?
                    Question = "Which badnik was shown in {0}?",
                },
                [SSonicKnuckles.Monitor] = new()
                {
                    // English: Which monitor was shown in {0}?
                    Question = "Which monitor was shown in {0}?",
                },
            },
        },

        [typeof(SSonicTheHedgehog)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSonicTheHedgehog.Pictures] = new()
                {
                    // English: What was the {1} picture on {0}?
                    // Example: What was the first picture on Sonic the Hedgehog?
                    Question = "What was the {1} picture on {0}?",
                },
                [SSonicTheHedgehog.Sounds] = new()
                {
                    // English: Which sound was played by the {1} screen on {0}?
                    // Example: Which sound was played by the Running Boots screen on Sonic the Hedgehog?
                    Question = "Which sound was played by the {1} screen on {0}?",
                    Arguments = new()
                    {
                        ["Running Boots"] = "Running Boots",
                        ["Invincibility"] = "Invincibility",
                        ["Extra Life"] = "Extra Life",
                        ["Rings"] = "Rings",
                    },
                },
            },
        },

        [typeof(SSorting)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSorting.LastSwap] = new()
                {
                    // English: What positions were the last swap used to solve {0}?
                    Question = "What positions were the last swap used to solve {0}?",
                },
            },
        },

        [typeof(SSouvenir)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSouvenir.FirstQuestion] = new()
                {
                    // English: What was the first module asked about in the other Souvenir on this bomb?
                    Question = "What was the first module asked about in the other Souvenir on this bomb?",
                },
            },
            Discriminators = new()
            {
                [SSouvenir.NullDiscriminator] = new()
                {
                    // English: Souvenir
                    Discriminator = "Souvenir",
                },
            },
        },

        [typeof(SSpaceTraders)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSpaceTraders.MaxTax] = new()
                {
                    // English: What was the maximum tax amount per vessel in {0}?
                    Question = "What was the maximum tax amount per vessel in {0}?",
                },
            },
        },

        [typeof(SSpellingBee)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSpellingBee.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "What word was asked to be spelled in {0}?",
                },
            },
        },

        [typeof(SSphere)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSphere.Colors] = new()
                {
                    // English: What was the {1} flashed color in {0}?
                    // Example: What was the first flashed color in Sphere?
                    Question = "What was the {1} flashed color in {0}?",
                },
            },
        },

        [typeof(SSplittingTheLoot)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSplittingTheLoot.ColoredBag] = new()
                {
                    // English: What bag was initially colored in {0}?
                    Question = "What bag was initially colored in {0}?",
                },
            },
        },

        [typeof(SSpongebobBirthdayIdentification)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSpongebobBirthdayIdentification.Children] = new()
                {
                    // English: Who was the {1} child displayed in {0}?
                    // Example: Who was the first child displayed in Spongebob Birthday Identification?
                    Question = "Who was the {1} child displayed in {0}?",
                },
            },
        },

        [typeof(SStability)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStability.LedColors] = new()
                {
                    // English: What was the color of the {1} lit LED in {0}?
                    // Example: What was the color of the first lit LED in Stability?
                    Question = "What was the color of the {1} lit LED in {0}?",
                },
                [SStability.IdNumber] = new()
                {
                    // English: What was the identification number in {0}?
                    Question = "What was the identification number in {0}?",
                },
            },
        },

        [typeof(SStableTimeSignatures)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStableTimeSignatures.Signatures] = new()
                {
                    // English: What was the {1} time signature in {0}?
                    // Example: What was the first time signature in Stable Time Signatures?
                    Question = "What was the {1} time signature in {0}?",
                },
            },
        },

        [typeof(SStackedSequences)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStackedSequences.Question] = new()
                {
                    // English: Which of these is the length of a sequence in {0}?
                    Question = "Which of these is the length of a sequence in {0}?",
                },
            },
        },

        [typeof(SStars)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStars.Center] = new()
                {
                    // English: What was the digit in the center of {0}?
                    Question = "What was the digit in the center of {0}?",
                },
            },
        },

        [typeof(SStarstruck)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStarstruck.Star] = new()
                {
                    // English: Which star was present on {0}?
                    Question = "Which star was present on {0}?",
                },
            },
        },

        [typeof(SStateOfAggregation)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStateOfAggregation.Element] = new()
                {
                    // English: What was the element shown in {0}?
                    Question = "What was the element shown in {0}?",
                },
            },
        },

        [typeof(SStellar)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStellar.Letters] = new()
                {
                    // English: What was the {1} letter in {0}?
                    // Example: What was the Morse code letter in Stellar?
                    Question = "What was the {1} letter in {0}?",
                    Arguments = new()
                    {
                        ["Morse code"] = "Morse code",
                        ["tap code"] = "tap code",
                        ["Braille"] = "Braille",
                    },
                },
            },
        },

        [typeof(SStroopsTest)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStroopsTest.Word] = new()
                {
                    // English: What was the {1} submitted word in {0}?
                    // Example: What was the first submitted word in Stroop’s Test?
                    Question = "What was the {1} submitted word in {0}?",
                },
                [SStroopsTest.Color] = new()
                {
                    // English: What was the {1} submitted word’s color in {0}?
                    // Example: What was the first submitted word’s color in Stroop’s Test?
                    Question = "What was the {1} submitted word’s color in {0}?",
                },
            },
        },

        [typeof(SStupidSlots)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStupidSlots.Values] = new()
                {
                    // English: What was the value of the {1} arrow in {0}?
                    // Example: What was the value of the top-left arrow in Stupid Slots?
                    Question = "What was the value of the {1} arrow in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-middle"] = "top-middle",
                        ["top-right"] = "top-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-middle"] = "bottom-middle",
                        ["bottom-right"] = "bottom-right",
                    },
                },
            },
        },

        [typeof(SSubblyJubbly)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSubblyJubbly.Substitutions] = new()
                {
                    // English: What was a substitution word in {0}?
                    Question = "What was a substitution word in {0}?",
                },
            },
        },

        [typeof(SSubscribeToPewdiepie)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSubscribeToPewdiepie.SubCount] = new()
                {
                    // English: How many subscribers does {1} have in {0}?
                    // Example: How many subscribers does PewDiePie have in Subscribe to Pewdiepie?
                    Question = "How many subscribers does {1} have in {0}?",
                    Arguments = new()
                    {
                        ["PewDiePie"] = "PewDiePie",
                        ["T-Series"] = "T-Series",
                    },
                },
            },
        },

        [typeof(SSubway)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSubway.Bread] = new()
                {
                    // English: Which bread did the customer ask for in {0}?
                    Question = "Which bread did the customer ask for in {0}?",
                },
                [SSubway.Items] = new()
                {
                    // English: Which of these was not asked for in {0}?
                    Question = "Which of these was not asked for in {0}?",
                },
            },
        },

        [typeof(SSugarSkulls)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSugarSkulls.Skull] = new()
                {
                    // English: What skull was shown on the {1} square in {0}?
                    // Example: What skull was shown on the top square in Sugar Skulls?
                    Question = "What skull was shown on the {1} square in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
                [SSugarSkulls.Availability] = new()
                {
                    // English: Which skull {1} present in {0}?
                    // Example: Which skull was present in Sugar Skulls?
                    Question = "Which skull {1} present in {0}?",
                    Arguments = new()
                    {
                        ["was"] = "was",
                        ["was not"] = "was not",
                    },
                },
            },
        },

        [typeof(SSuitsAndColours)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSuitsAndColours.Colour] = new()
                {
                    // English: What was the colour of this cell in {0}?
                    // Example: What was the colour of this cell in Suits and Colours?
                    Question = "What was the colour of this cell in {0}?",
                },
                [SSuitsAndColours.Suit] = new()
                {
                    // English: What was the suit of this cell in {0}?
                    // Example: What was the suit of this cell in Suits and Colours?
                    Question = "What was the suit of this cell in {0}?",
                },
            },
        },

        [typeof(SSuperparsing)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSuperparsing.Displayed] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "What was the displayed word in {0}?",
                },
            },
        },

        [typeof(SSUSadmin)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSUSadmin.Security] = new()
                {
                    // English: Which security protocol was installed in {0}?
                    Question = "Which security protocol was installed in {0}?",
                },
            },
        },

        [typeof(SSwitch)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSwitch.InitialColor] = new()
                {
                    // English: What color was the {1} LED on the {2} flip of {0}?
                    // Example: What color was the top LED on the first flip of Switch?
                    Question = "What color was the {1} LED on the {2} flip of {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SSwitches)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSwitches.InitialPosition] = new()
                {
                    // English: What was the initial position of the switches in {0}?
                    Question = "What was the initial position of the switches in {0}?",
                },
            },
        },

        [typeof(SSwitchingMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSwitchingMaze.Seed] = new()
                {
                    // English: What was the seed in {0}?
                    Question = "What was the seed in {0}?",
                },
                [SSwitchingMaze.Color] = new()
                {
                    // English: What was the starting maze color in {0}?
                    Question = "What was the starting maze color in {0}?",
                },
            },
        },

        [typeof(SSymbolCycle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSymbolCycle.SymbolCounts] = new()
                {
                    // English: How many symbols were cycling on the {1} screen in {0}?
                    // Example: How many symbols were cycling on the left screen in Symbol Cycle?
                    Question = "How many symbols were cycling on the {1} screen in {0}?",
                    Arguments = new()
                    {
                        ["left"] = "left",
                        ["right"] = "right",
                    },
                },
            },
        },

        [typeof(SSymbolicCoordinates)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSymbolicCoordinates.ymbols] = new()
                {
                    // English: What was the {1} symbol in the {2} stage of {0}?
                    // Example: What was the left symbol in the first stage of Symbolic Coordinates?
                    Question = "What was the {1} symbol in the {2} stage of {0}?",
                    Arguments = new()
                    {
                        ["left"] = "left",
                        ["middle"] = "middle",
                        ["right"] = "right",
                    },
                },
            },
        },

        [typeof(SSymbolicTasha)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSymbolicTasha.Flashes] = new()
                {
                    // English: Which button flashed {1} in the final sequence of {0}?
                    // Example: Which button flashed first in the final sequence of Symbolic Tasha?
                    Question = "Which button flashed {1} in the final sequence of {0}?",
                },
                [SSymbolicTasha.Symbols] = new()
                {
                    // English: Which symbol was on the {1} button in {0}?
                    // Example: Which symbol was on the top button in Symbolic Tasha?
                    Question = "Which symbol was on the {1} button in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["right"] = "right",
                        ["bottom"] = "bottom",
                        ["left"] = "left",
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["yellow"] = "yellow",
                        ["pink"] = "pink",
                    },
                },
            },
        },

        [typeof(SSynapseSays)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSynapseSays.Displays] = new()
                {
                    // English: What number was displayed in the {1} stage of {0}?
                    // Example: What number was displayed in the first stage of Synapse Says?
                    Question = "What number was displayed in the {1} stage of {0}?",
                },
                [SSynapseSays.Flashes] = new()
                {
                    // English: What color flashed {1} in the {2} stage of {0}?
                    // Example: What color flashed first in the first stage of Synapse Says?
                    Question = "What color flashed {1} in the {2} stage of {0}?",
                },
                [SSynapseSays.Positions] = new()
                {
                    // English: What color was in the {1} position of the {2} stage of {0}?
                    // Example: What color was in the first position of the first stage of Synapse Says?
                    Question = "What color was in the {1} position of the {2} stage of {0}?",
                },
            },
        },

        [typeof(SSync125_3)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSync125_3.Word] = new()
                {
                    // English: What was displayed on the screen in the {1} stage of {0}?
                    // Example: What was displayed on the screen in the first stage of SYNC-125 [3]?
                    Question = "What was displayed on the screen in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SSynonyms)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSynonyms.Number] = new()
                {
                    // English: Which number was displayed on {0}?
                    Question = "Which number was displayed on {0}?",
                },
            },
        },

        [typeof(SSysadmin)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSysadmin.FixedErrorCodes] = new()
                {
                    // English: What error code did you fix in {0}?
                    Question = "What error code did you fix in {0}?",
                },
            },
        },

        [typeof(STAC)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STAC.SwappedCard] = new()
                {
                    // English: Which card was {1} in the swap in {0}?
                    // Example: Which card was given away in the swap in TAC?
                    Question = "Which card was {1} in the swap in {0}?",
                    Arguments = new()
                    {
                        ["given away"] = "given away",
                        ["received"] = "received",
                    },
                },
                [STAC.HeldCard] = new()
                {
                    // English: Which card was in your hand in {0}?
                    Question = "Which card was in your hand in {0}?",
                },
            },
        },

        [typeof(STapCode)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STapCode.ReceivedWord] = new()
                {
                    // English: What was the received word in {0}?
                    Question = "What was the received word in {0}?",
                },
            },
        },

        [typeof(STashaSqueals)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STashaSqueals.Colors] = new()
                {
                    // English: What was the {1} flashed color in {0}?
                    // Example: What was the first flashed color in Tasha Squeals?
                    Question = "What was the {1} flashed color in {0}?",
                },
            },
        },

        [typeof(STasqueManaging)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STasqueManaging.StartingPos] = new()
                {
                    // English: Where was the starting position in {0}?
                    Question = "Where was the starting position in {0}?",
                },
            },
        },

        [typeof(STeaSet)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STeaSet.DisplayedIngredients] = new()
                {
                    // English: Which ingredient was displayed {1}, from left to right, in {0}?
                    // Example: Which ingredient was displayed first, from left to right, in Tea Set?
                    Question = "Which ingredient was displayed {1}, from left to right, in {0}?",
                },
            },
        },

        [typeof(STechnicalKeypad)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STechnicalKeypad.DisplayedDigits] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "What was the {1} displayed digit in {0}?",
                },
            },
        },

        [typeof(STenButtonColorCode)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STenButtonColorCode.InitialColors] = new()
                {
                    // English: What was the initial color of the {1} button in the {2} stage of {0}?
                    // Example: What was the initial color of the first button in the first stage of Ten-Button Color Code?
                    Question = "What was the initial color of the {1} button in the {2} stage of {0}?",
                },
            },
        },

        [typeof(STenpins)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STenpins.Splits] = new()
                {
                    // English: What was the {1} split in {0}?
                    // Example: What was the red split in Tenpins?
                    Question = "What was the {1} split in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
            },
        },

        [typeof(STetriamonds)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STetriamonds.PulsingColours] = new()
                {
                    // English: What colour triangle pulsed {1} in {0}?
                    // Example: What colour triangle pulsed first in Tetriamonds?
                    Question = "What colour triangle pulsed {1} in {0}?",
                },
            },
        },

        [typeof(STextField)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STextField.Display] = new()
                {
                    // English: What was the displayed letter in {0}?
                    Question = "What was the displayed letter in {0}?",
                },
            },
        },

        [typeof(SThinkingWires)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SThinkingWires.FirstWire] = new()
                {
                    // English: What was the position from top to bottom of the first wire needing to be cut in {0}?
                    Question = "What was the position from top to bottom of the first wire needing to be cut in {0}?",
                },
                [SThinkingWires.SecondWire] = new()
                {
                    // English: What color did the second valid wire to cut have to have in {0}?
                    Question = "What color did the second valid wire to cut have to have in {0}?",
                },
                [SThinkingWires.DisplayNumber] = new()
                {
                    // English: What was the display number in {0}?
                    Question = "What was the display number in {0}?",
                },
            },
        },

        [typeof(SThirdBase)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SThirdBase.Display] = new()
                {
                    // English: What was the display word in the {1} stage on {0}?
                    // Example: What was the display word in the first stage on Third Base?
                    Question = "What was the display word in the {1} stage on {0}?",
                },
            },
        },

        [typeof(SThirtyDollarModule)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SThirtyDollarModule.Sounds] = new()
                {
                    // English: Which sound was used in {0}?
                    Question = "Which sound was used in {0}?",
                },
            },
        },

        [typeof(STicTacToe)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STicTacToe.InitialState] = new()
                {
                    // English: What was on the {1} button at the start of {0}?
                    // Example: What was on the top-left button at the start of Tic Tac Toe?
                    Question = "What was on the {1} button at the start of {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-middle"] = "top-middle",
                        ["top-right"] = "top-right",
                        ["middle-left"] = "middle-left",
                        ["middle-center"] = "middle-center",
                        ["middle-right"] = "middle-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-middle"] = "bottom-middle",
                        ["bottom-right"] = "bottom-right",
                    },
                },
            },
        },

        [typeof(STimeSignatures)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STimeSignatures.Signatures] = new()
                {
                    // English: What was the {1} time signature in {0}?
                    // Example: What was the first time signature in Time Signatures?
                    Question = "What was the {1} time signature in {0}?",
                },
            },
        },

        [typeof(STimezone)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STimezone.Cities] = new()
                {
                    // English: What was the {1} city in {0}?
                    // Example: What was the departure city in Timezone?
                    Question = "What was the {1} city in {0}?",
                    Arguments = new()
                    {
                        ["departure"] = "departure",
                        ["destination"] = "destination",
                    },
                },
            },
        },

        [typeof(STipToe)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STipToe.SafeSquares] = new()
                {
                    // English: Which of these squares was safe in row {1} in {0}?
                    // Example: Which of these squares was safe in row 9 in Tip Toe?
                    Question = "Which of these squares was safe in row {1} in {0}?",
                },
            },
        },

        [typeof(STopsyTurvy)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STopsyTurvy.Word] = new()
                {
                    // English: What was the word initially shown in {0}?
                    Question = "What was the word initially shown in {0}?",
                },
            },
        },

        [typeof(STouchTransmission)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STouchTransmission.Word] = new()
                {
                    // English: What was the transmitted word in {0}?
                    Question = "What was the transmitted word in {0}?",
                },
                [STouchTransmission.Order] = new()
                {
                    // English: In what order was the Braille read in {0}?
                    Question = "In what order was the Braille read in {0}?",
                },
            },
        },

        [typeof(STransmittedMorse)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STransmittedMorse.Message] = new()
                {
                    // English: What was the {1} received message in {0}?
                    // Example: What was the first received message in Transmitted Morse?
                    Question = "What was the {1} received message in {0}?",
                },
            },
        },

        [typeof(STriamonds)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STriamonds.PulsingColours] = new()
                {
                    // English: What colour triangle pulsed {1} in {0}?
                    // Example: What colour triangle pulsed first in Triamonds?
                    Question = "What colour triangle pulsed {1} in {0}?",
                },
            },
        },

        [typeof(STribalCouncil)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STribalCouncil.Name] = new()
                {
                    // English: What was the {1} name in {0}?
                    // Example: What was the northeast name in Tribal Council?
                    Question = "What was the {1} name in {0}?",
                    Arguments = new()
                    {
                        ["northeast"] = "northeast",
                        ["southwest"] = "southwest",
                    },
                },
            },
        },

        [typeof(STripleTerm)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STripleTerm.Passwords] = new()
                {
                    // English: Which of these was one of the passwords in {0}?
                    Question = "Which of these was one of the passwords in {0}?",
                },
            },
        },

        [typeof(STurtleRobot)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STurtleRobot.CodeLines] = new()
                {
                    // English: What was the {1} line you commented out in {0}?
                    // Example: What was the first line you commented out in Turtle Robot?
                    Question = "What was the {1} line you commented out in {0}?",
                },
            },
        },

        [typeof(STwoBits)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STwoBits.Response] = new()
                {
                    // English: What was the {1} correct query response from {0}?
                    // Example: What was the first correct query response from Two Bits?
                    Question = "What was the {1} correct query response from {0}?",
                },
            },
        },

        [typeof(SUltimateCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUltimateCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Ultimate Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SUltimateCycle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUltimateCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Ultimate Cycle?
                    Question = "Which direction was the {1} dial pointing in {0}?",
                },
                [SUltimateCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Ultimate Cycle?
                    Question = "What letter was written on the {1} dial in {0}?",
                },
            },
        },

        [typeof(SUltracube)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUltracube.Rotations] = new()
                {
                    // English: What was the {1} rotation in {0}?
                    // Example: What was the first rotation in Ultracube?
                    Question = "What was the {1} rotation in {0}?",
                },
            },
        },

        [typeof(SUltraStores)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUltraStores.SingleRotation] = new()
                {
                    // English: What was the {1} rotation in the {2} stage of {0}?
                    // Example: What was the first rotation in the first stage of UltraStores?
                    Question = "What was the {1} rotation in the {2} stage of {0}?",
                },
                [SUltraStores.MultiRotation] = new()
                {
                    // English: What was the {1} rotation in the {2} stage of {0}?
                    // Example: What was the first rotation in the first stage of UltraStores?
                    Question = "What was the {1} rotation in the {2} stage of {0}?",
                },
            },
        },

        [typeof(SUncoloredSquares)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUncoloredSquares.FirstStage] = new()
                {
                    // English: What was the {1} color in reading order used in the first stage of {0}?
                    // Example: What was the first color in reading order used in the first stage of Uncolored Squares?
                    Question = "What was the {1} color in reading order used in the first stage of {0}?",
                },
            },
        },

        [typeof(SUncoloredSwitches)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUncoloredSwitches.InitialState] = new()
                {
                    // English: What was the initial state of the switches in {0}?
                    Question = "What was the initial state of the switches in {0}?",
                },
                [SUncoloredSwitches.LedColors] = new()
                {
                    // English: What color was the {1} LED in reading order in {0}?
                    // Example: What color was the first LED in reading order in Uncolored Switches?
                    Question = "What color was the {1} LED in reading order in {0}?",
                },
            },
        },

        [typeof(SUncolourFlash)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUncolourFlash.Displays] = new()
                {
                    // English: What was the {1} in the {2} position of the {3} sequence of {0}?
                    // Example: What was the word in the first position of the “YES” sequence of Uncolour Flash?
                    Question = "What was the {1} in the {2} position of the {3} sequence of {0}?",
                },
            },
        },

        [typeof(SUnfairCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUnfairCipher.Instructions] = new()
                {
                    // English: What was the {1} received instruction in {0}?
                    // Example: What was the first received instruction in Unfair Cipher?
                    Question = "What was the {1} received instruction in {0}?",
                },
            },
        },

        [typeof(SUnfairsRevenge)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUnfairsRevenge.Instructions] = new()
                {
                    // English: What was the {1} decrypted instruction in {0}?
                    // Example: What was the first decrypted instruction in Unfair’s Revenge?
                    Question = "What was the {1} decrypted instruction in {0}?",
                },
            },
        },

        [typeof(SUnicode)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUnicode.SortedAnswer] = new()
                {
                    // English: What was the {1} submitted code in {0}?
                    // Example: What was the first submitted code in Unicode?
                    Question = "What was the {1} submitted code in {0}?",
                },
            },
        },

        [typeof(SUNO)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUNO.InitialCard] = new()
                {
                    // English: What was the initial card in {0}?
                    Question = "What was the initial card in {0}?",
                },
            },
        },

        [typeof(SUnorderedKeys)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUnorderedKeys.KeyColor] = new()
                {
                    // English: What color was this key in the {1} stage of {0}?
                    // Example: What color was this key in the first stage of Unordered Keys?
                    Question = "What color was this key in the {1} stage of {0}?",
                },
                [SUnorderedKeys.LabelColor] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}?
                    // Example: What color was the label of this key in the first stage of Unordered Keys?
                    Question = "What color was the label of this key in the {1} stage of {0}?",
                },
                [SUnorderedKeys.Label] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}?
                    // Example: What was the label of this key in the first stage of Unordered Keys?
                    Question = "What was the label of this key in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SUnownCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUnownCipher.Answers] = new()
                {
                    // English: What was the {1} submitted letter in {0}?
                    // Example: What was the first submitted letter in Unown Cipher?
                    Question = "What was the {1} submitted letter in {0}?",
                },
            },
        },

        [typeof(SUnpleasantSquares)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUnpleasantSquares.Color] = new()
                {
                    // English: What was the color of this square in {0}?
                    Question = "What was the color of this square in {0}?",
                },
            },
        },

        [typeof(SUpdog)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUpdog.Word] = new()
                {
                    // English: What was the text on {0}?
                    Question = "What was the text on {0}?",
                },
                [SUpdog.Color] = new()
                {
                    // English: What was the {1} color in the sequence on {0}?
                    // Example: What was the first color in the sequence on Updog?
                    Question = "What was the {1} color in the sequence on {0}?",
                    Arguments = new()
                    {
                        ["first"] = "first",
                        ["last"] = "last",
                    },
                },
            },
        },

        [typeof(SUSACycle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUSACycle.Displayed] = new()
                {
                    // English: Which state was displayed in {0}?
                    Question = "Which state was displayed in {0}?",
                },
            },
        },

        [typeof(SUSAMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUSAMaze.Origin] = new()
                {
                    // English: Which state did you depart from in {0}?
                    Question = "Which state did you depart from in {0}?",
                },
            },
        },

        [typeof(SV)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SV.Words] = new()
                {
                    // English: Which word {1} shown in {0}?
                    // Example: Which word was shown in V?
                    Question = "Which word {1} shown in {0}?",
                    Arguments = new()
                    {
                        ["was"] = "was",
                        ["was not"] = "was not",
                    },
                },
            },
        },

        [typeof(SValves)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SValves.InitialState] = new()
                {
                    // English: What was the initial state of {0}?
                    Question = "What was the initial state of {0}?",
                },
            },
        },

        [typeof(SVaricoloredSquares)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SVaricoloredSquares.InitialColor] = new()
                {
                    // English: What was the initially pressed color on {0}?
                    Question = "What was the initially pressed color on {0}?",
                },
            },
        },

        [typeof(SVaricolourFlash)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SVaricolourFlash.Words] = new()
                {
                    // English: What was the word of the {1} goal in {0}?
                    // Example: What was the word of the first goal in Varicolour Flash?
                    Question = "What was the word of the {1} goal in {0}?",
                },
                [SVaricolourFlash.Colors] = new()
                {
                    // English: What was the color of the {1} goal in {0}?
                    // Example: What was the color of the first goal in Varicolour Flash?
                    Question = "What was the color of the {1} goal in {0}?",
                },
            },
        },

        [typeof(SVariety)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SVariety.LED] = new()
                {
                    // English: What color was the LED flashing in {0}?
                    Question = "What color was the LED flashing in {0}?",
                },
                [SVariety.DigitDisplay] = new()
                {
                    // English: What digit was displayed, but not the answer, for the digit display in {0}?
                    Question = "What digit was displayed, but not the answer, for the digit display in {0}?",
                },
                [SVariety.LetterDisplay] = new()
                {
                    // English: What word could be formed, but was not the answer, for the letter display in {0}?
                    Question = "What word could be formed, but was not the answer, for the letter display in {0}?",
                },
                [SVariety.Timer] = new()
                {
                    // English: What was the maximum display for the {1} in {0}?
                    // Example: What was the maximum display for the timer in Variety?
                    Question = "What was the maximum display for the {1} in {0}?",
                    Arguments = new()
                    {
                        ["timer"] = "timer",
                        ["ascending timer"] = "ascending timer",
                        ["descending timer"] = "descending timer",
                    },
                },
                [SVariety.ColoredKnob] = new()
                {
                    // English: What was n for the {1} in {0}?
                    // Example: What was n for the knob in Variety?
                    Question = "What was n for the {1} in {0}?",
                    Arguments = new()
                    {
                        ["knob"] = "knob",
                        ["colored knob"] = "colored knob",
                        ["red knob"] = "red knob",
                        ["black knob"] = "black knob",
                        ["blue knob"] = "blue knob",
                        ["yellow knob"] = "yellow knob",
                    },
                },
                [SVariety.Bulb] = new()
                {
                    // English: What was n for the {1} in {0}?
                    // Example: What was n for the bulb in Variety?
                    Question = "What was n for the {1} in {0}?",
                    Arguments = new()
                    {
                        ["bulb"] = "bulb",
                        ["red bulb"] = "red bulb",
                        ["yellow bulb"] = "yellow bulb",
                    },
                },
            },
            Discriminators = new()
            {
                [SVariety.Has] = new()
                {
                    // English: the Variety that has {0}
                    // Example: the Variety that has one (LED)
                    Discriminator = "the Variety that has {0}",
                },
            },
        },

        [typeof(SVcrcs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SVcrcs.Word] = new()
                {
                    // English: What was the word in {0}?
                    Question = "What was the word in {0}?",
                },
            },
        },

        [typeof(SVectors)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SVectors.Colors] = new()
                {
                    // English: What was the color of the {1} vector in {0}?
                    // Example: What was the color of the first vector in Vectors?
                    Question = "What was the color of the {1} vector in {0}?",
                    Arguments = new()
                    {
                        ["first"] = "first",
                        ["second"] = "second",
                        ["third"] = "third",
                        ["only"] = "only",
                    },
                },
            },
        },

        [typeof(SVexillology)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SVexillology.Colors] = new()
                {
                    // English: What was the {1} flagpole color on {0}?
                    // Example: What was the first flagpole color on Vexillology?
                    Question = "What was the {1} flagpole color on {0}?",
                },
            },
        },

        [typeof(SVioletCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SVioletCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Violet Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SVisualImpairment)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SVisualImpairment.Colors] = new()
                {
                    // English: What was the desired color in the {1} stage on {0}?
                    // Example: What was the desired color in the first stage on Visual Impairment?
                    Question = "What was the desired color in the {1} stage on {0}?",
                },
            },
        },

        [typeof(SWalkingCube)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWalkingCube.Path] = new()
                {
                    // English: Which of these cells was part of the cube’s path in {0}?
                    Question = "Which of these cells was part of the cube’s path in {0}?",
                },
            },
        },

        [typeof(SWarningSigns)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWarningSigns.DisplayedSign] = new()
                {
                    // English: What was the displayed sign in {0}?
                    Question = "What was the displayed sign in {0}?",
                },
            },
        },

        [typeof(SWasd)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWasd.DisplayedLocation] = new()
                {
                    // English: What was the location displayed in {0}?
                    Question = "What was the location displayed in {0}?",
                },
            },
        },

        [typeof(SWatchingPaintDry)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWatchingPaintDry.StrokeCount] = new()
                {
                    // English: How many brush strokes were heard in {0}?
                    Question = "How many brush strokes were heard in {0}?",
                },
            },
        },

        [typeof(SWavetapping)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWavetapping.Patterns] = new()
                {
                    // English: What was the correct pattern on the {1} stage in {0}?
                    // Example: What was the correct pattern on the first stage in Wavetapping?
                    Question = "What was the correct pattern on the {1} stage in {0}?",
                },
                [SWavetapping.Colors] = new()
                {
                    // English: What was the color on the {1} stage in {0}?
                    // Example: What was the color on the first stage in Wavetapping?
                    Question = "What was the color on the {1} stage in {0}?",
                },
            },
        },

        [typeof(SWeakestLink)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWeakestLink.Elimination] = new()
                {
                    // English: Who did you eliminate in {0}?
                    Question = "Who did you eliminate in {0}?",
                },
                [SWeakestLink.MoneyPhaseName] = new()
                {
                    // English: Who made it to the Money Phase with you in {0}?
                    Question = "Who made it to the Money Phase with you in {0}?",
                },
                [SWeakestLink.Skill] = new()
                {
                    // English: What was {1}’s skill in {0}?
                    // Example: What was Annie’s skill in Weakest Link?
                    Question = "What was {1}’s skill in {0}?",
                },
                [SWeakestLink.Ratio] = new()
                {
                    // English: What ratio did {1} get in the Question Phase in {0}?
                    // Example: What ratio did Annie get in the Question Phase in Weakest Link?
                    Question = "What ratio did {1} get in the Question Phase in {0}?",
                },
            },
        },

        [typeof(SWhatsOnSecond)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWhatsOnSecond.DisplayText] = new()
                {
                    // English: What was the display text in the {1} stage of {0}?
                    // Example: What was the display text in the first stage of What’s on Second?
                    Question = "What was the display text in the {1} stage of {0}?",
                },
                [SWhatsOnSecond.DisplayColor] = new()
                {
                    // English: What was the display text color in the {1} stage of {0}?
                    // Example: What was the display text color in the first stage of What’s on Second?
                    Question = "What was the display text color in the {1} stage of {0}?",
                },
            },
        },

        [typeof(SWhiteArrows)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWhiteArrows.Arrows] = new()
                {
                    // English: What was the {1} non-white arrow in {0}?
                    // Example: What was the first non-white arrow in White Arrows?
                    Question = "What was the {1} non-white arrow in {0}?",
                    Additional = new()
                    {
                        ["Blue"] = "Blue",
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Purple"] = "Purple",
                        ["Orange"] = "Orange",
                        ["Cyan"] = "Cyan",
                        ["Teal"] = "Teal",
                        ["Up"] = "Up",
                        ["Right"] = "Right",
                        ["Down"] = "Down",
                        ["Left"] = "Left",
                        ["{0} {1}"] = "{0} {1}",
                    },
                },
            },
        },

        [typeof(SWhiteCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWhiteCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in White Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SWhoOF)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWhoOF.Display] = new()
                {
                    // English: What was the display in the {1} stage on {0}?
                    // Example: What was the display in the first stage on WhoOF?
                    Question = "What was the display in the {1} stage on {0}?",
                },
            },
        },

        [typeof(SWhosOnFirst)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWhosOnFirst.Display] = new()
                {
                    // English: What was the display in the {1} stage on {0}?
                    // Example: What was the display in the first stage on Who’s on First?
                    Question = "What was the display in the {1} stage on {0}?",
                },
            },
        },

        [typeof(SWhosOnGas)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWhosOnGas.Display] = new()
                {
                    // English: What was the display in the first phase of the {1} stage on {0}?
                    // Example: What was the display in the first phase of the first stage on Who’s on Gas?
                    Question = "What was the display in the first phase of the {1} stage on {0}?",
                },
            },
        },

        [typeof(SWhosOnMorse)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWhosOnMorse.TransmitDisplay] = new()
                {
                    // English: What word was transmitted in the {1} stage on {0}?
                    // Example: What word was transmitted in the first stage on Who’s on Morse?
                    Question = "What word was transmitted in the {1} stage on {0}?",
                },
            },
        },

        [typeof(SWire)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWire.DialColors] = new()
                {
                    // English: What was the color of the {1} dial in {0}?
                    // Example: What was the color of the top dial in Wire?
                    Question = "What was the color of the {1} dial in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
                [SWire.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "What was the displayed number in {0}?",
                },
            },
        },

        [typeof(SWireOrdering)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWireOrdering.DisplayColor] = new()
                {
                    // English: What color was the {1} display from the left in {0}?
                    // Example: What color was the first display from the left in Wire Ordering?
                    Question = "What color was the {1} display from the left in {0}?",
                },
                [SWireOrdering.DisplayNumber] = new()
                {
                    // English: What number was on the {1} display from the left in {0}?
                    // Example: What number was on the first display from the left in Wire Ordering?
                    Question = "What number was on the {1} display from the left in {0}?",
                },
                [SWireOrdering.WireColor] = new()
                {
                    // English: What color was the {1} wire from the left in {0}?
                    // Example: What color was the first wire from the left in Wire Ordering?
                    Question = "What color was the {1} wire from the left in {0}?",
                },
            },
        },

        [typeof(SWireSequence)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWireSequence.ColorCount] = new()
                {
                    // English: How many {1} wires were there in {0}?
                    // Example: How many red wires were there in Wire Sequence?
                    Question = "How many {1} wires were there in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["blue"] = "blue",
                        ["black"] = "black",
                    },
                },
            },
        },

        [typeof(SWolfGoatAndCabbage)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWolfGoatAndCabbage.Animals] = new()
                {
                    // English: Which of these was {1} on {0}?
                    // Example: Which of these was present on Wolf, Goat, and Cabbage?
                    Question = "Which of these was {1} on {0}?",
                    Arguments = new()
                    {
                        ["present"] = "present",
                        ["not present"] = "not present",
                    },
                },
                [SWolfGoatAndCabbage.BoatSize] = new()
                {
                    // English: What was the boat size in {0}?
                    Question = "What was the boat size in {0}?",
                },
            },
        },

        [typeof(SWordCount)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWordCount.Number] = new()
                {
                    // English: What was the displayed number on {0}?
                    Question = "What was the displayed number on {0}?",
                },
            },
        },

        [typeof(SWorkingTitle)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWorkingTitle.Label] = new()
                {
                    // English: What was the label shown in {0}?
                    Question = "What was the label shown in {0}?",
                },
            },
        },

        [typeof(SWumbo)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWumbo.Number] = new()
                {
                    // English: What was the number in {0}?
                    Question = "What was the number in {0}?",
                },
            },
        },

        [typeof(SXenocryst)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SXenocryst.Question] = new()
                {
                    // English: What was the color of the {1} flash in {0}?
                    // Example: What was the color of the first flash in Xenocryst?
                    Question = "What was the color of the {1} flash in {0}?",
                },
            },
        },

        [typeof(SXmORseCode)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SXmORseCode.Word] = new()
                {
                    // English: What word did you decrypt in {0}?
                    Question = "What word did you decrypt in {0}?",
                },
                [SXmORseCode.DisplayedLetters] = new()
                {
                    // English: What was the {1} displayed letter (in reading order) in {0}?
                    // Example: What was the first displayed letter (in reading order) in XmORse Code?
                    Question = "What was the {1} displayed letter (in reading order) in {0}?",
                },
            },
        },

        [typeof(SXobekuJehT)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SXobekuJehT.Song] = new()
                {
                    // English: What song was played on {0}?
                    Question = "What song was played on {0}?",
                },
            },
        },

        [typeof(SXRing)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SXRing.Symbol] = new()
                {
                    // English: Which symbol was scanned in {0}?
                    Question = "Which symbol was scanned in {0}?",
                },
            },
        },

        [typeof(SXYRay)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SXYRay.Shapes] = new()
                {
                    // English: Which shape was scanned by {0}?
                    Question = "Which shape was scanned by {0}?",
                },
            },
        },

        [typeof(SYahtzee)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SYahtzee.InitialRoll] = new()
                {
                    // English: What was the initial roll on {0}?
                    Question = "What was the initial roll on {0}?",
                },
            },
        },

        [typeof(SYellowArrows)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SYellowArrows.StartingRow] = new()
                {
                    // English: What was the starting row letter in {0}?
                    Question = "What was the starting row letter in {0}?",
                },
            },
        },

        [typeof(SYellowButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SYellowButton.Colors] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in Yellow Button?
                    Question = "What was the {1} color in {0}?",
                },
            },
        },

        [typeof(SYellowButtont)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SYellowButtont.Filename] = new()
                {
                    // English: What was the filename in {0}?
                    Question = "What was the filename in {0}?",
                },
            },
        },

        [typeof(SYellowCipher)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SYellowCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Yellow Cipher?
                    Question = "What was on the {1} screen on page {2} in {0}?",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["middle"] = "middle",
                        ["bottom"] = "bottom",
                    },
                },
            },
        },

        [typeof(SZeroZero)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SZeroZero.Squares] = new()
                {
                    // English: Where was the {1} square in {0}?
                    // Example: Where was the red square in Zero, Zero?
                    Question = "Where was the {1} square in {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
                [SZeroZero.StarColors] = new()
                {
                    // English: What color was the {1} star in {0}?
                    // Example: What color was the top-left star in Zero, Zero?
                    Question = "What color was the {1} star in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-right"] = "top-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
                [SZeroZero.StarPoints] = new()
                {
                    // English: How many points were on the {1} star in {0}?
                    // Example: How many points were on the top-left star in Zero, Zero?
                    Question = "How many points were on the {1} star in {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "top-left",
                        ["top-right"] = "top-right",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                    },
                },
            },
        },

        [typeof(SZoni)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SZoni.Words] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the first word in Zoni?
                    Question = "What was the {1} word in {0}?",
                },
            },
        },

        [typeof(SÉpelleMoiÇa)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SÉpelleMoiÇa.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "What word was asked to be spelled in {0}?",
                },
            },
        },

        #endregion
    };

    public override string[] IntroTexts => Ut.NewArray(
        // Russian translations of English-language quotes
        "Я вижу мёртвых сапёров.",     // “I see dead people.” (Sixth Sense)
        "Добро пожаловать... в настоящую бомбу.",     // “Welcome... to the real world.” (The Matrix)
        "Я собираюсь сделать бомбу, которую он не сможет обезвредить.",   // “I’m gonna make him an offer he can’t refuse.” (The Godfather)
        "Луис, я думаю, это начало прекрасного взрыва.",   // “Louis, I think this is the beginning of a beautiful friendship.” (Casablanca)
        "Эй. Я мог бы обезвредить эту бомбу ровно за десять секунд.",   // “Hey. I could clear the sky in ten seconds flat.” (MLP:FiM, Friendship is Magic - Part 1)
        "Да пребудет с тобой бомба.",    // “May the Force be with you.” (Star Wars IV: A New Hope)
        "Люблю запах взрывов по утрам.",   // “I love the smell of napalm in the morning.” (Apocalypse Now)
        "Алло? Да, я сейчас обезвреживаю бомбу.",    // “E.T. phone home.” (E.T. the Extra-Terrestrial)
        "Бомб. Джеймс Бомб.",    // “Bond. James Bond.” (Dr. No / James Bond series)
        "Бомба тебе не по зубам!",   // “You can’t handle the truth!” (A Few Good Men)
        "Тебе понадо- бится бомба побольше.", // “You’re gonna need a bigger boat.” (Jaws)
        "Бомбы – это как коробка шоколадных конфет. Никогда не знаешь, что попадётся.",    // “My mom always said life was like a box of chocolates. You never know what you’re gonna get.” (Forrest Gump)
        "Хьюстон, у нас бомба.",   // “Houston, we have a problem.” (Apollo 13)
        "Элементарно, мой дорогой эксперт.",  // “Elementary, my dear Watson.” (Sherlock Holmes) (misquote)
        "Забудь об этом, Джейк, это КТАНЕ.",     // “Forget it, Jake, it’s Chinatown.” (Chinatown)
        "Я всегда полагался на компетент- ность экспертов.",    // “I’ve always depended on the kindness of strangers.” (A Streetcar Named Desire)
        "Бомба. Взорванная, а не обезвреженная.",   // “A martini. Shaken, not stirred.” (Diamonds Are Forever (novel) / James Bond)
        "Ябба- дабба- бум!",    // “Yabba dabba doo!” (Flintstones)
        "Эта бомба взорвётся через пять секунд.",    // “This tape will self-destruct in five seconds.” (Mission: Impossible)
        "Обезвре- живание бесполезно.",  // “Resistance is futile.” (Star Trek: The Next Generation)
        "Это твой окончатель- ный ответ?",   // direct quote (Who Wants to be a Millionaire?)
        "Лучший друг бомбы – это её сапёр.", // “A man’s best friend is his dog.” (attorney George Graham Vest, 1870 Warrensburg)
        "Держи своих экспертов близко, но свою бомбу – ещё ближе.",   // “Keep your friends close and your enemies closer.” (The Prince / Machiavelli)
        "Пристегните ремни безопасности. Это будет взрывная ночь.",   // “Fasten your seat belts, it’s going to be a bumpy night.” (All About Eve)
        "Ты сапёр, Гарри.", // “You’re a wizard, Harry.” (Harry Potter and the Philosopher’s Stone)
        "Либо ты умираешь сапёром, либо живёшь до тех пор, пока не становишься экспертом.", // “Well, I guess you either die a hero or you live long enough to see yourself become the villain.” (The Dark Knight)
        "Это не обезврежи- вание. Это взрыв... со стилем.",    // “This isn’t flying. This is falling... with style.” (Toy Story)
        "Вы что, перепутали провода?", // “Have you got your lions crossed?” (The Lion King)
        "Не перепутай провода.",   // “Don’t cross the streams.” (Ghostbusters)
        "Хотите услышать самый мощный и громкий взрыв в мире?", // “Wanna hear the most annoying sound in the world?” (Dumb & Dumber)
        "Руковод- ства? Там, куда мы идём, они нам не нужны.",   // “Roads? Where we’re going, we don’t need roads.” (Back to the Future)
        "Первое правило обезвреживания заключается в том, что вы продолжаете говорить об обезвреживании.",    // “The first rule of Fight Club is, you don’t talk about Fight Club.” (Fight Club)
        "Мы обезвреживаем бомбы.",  // “We rob banks.” (Bonnie and Clyde)
        "Кто-то подложил нам бомбу.",  // direct quote (Zero Wing)
        "Люк, я твой эксперт.", // “Luke, I am your father.” (Star Wars V: The Empire Strikes Back) (misquote)
        "Она должна быть примерно на 20% более взрывоопасной.", // “It needs to be about 20 percent cooler.” (MLP:FiM, Suited for Success)
        "То же самое, что мы делаем каждый вечер, эксперт. Попробуем обезвредить бомбу!", // “The same thing we do every night, Pinky. Try to take over the world!” (Pinky and the Brain)
        "Кто-нибудь заказывал жареного сапёра?", // “Anybody order fried sauerkraut?” (Once Upon a Time in Hollywood)
        "У меня есть несколько сапёров, которых нужно разнести в пух и прах!", // “I’ve got some children I need to make into corpses!” (Gravity Falls, Weirdmageddon 3: Take Back The Falls)
        "Я – неизбеж- ность.", // direct quote (Avengers: Endgame)
        "Бойтесь, бегите! Бомбы все равно взорвутся.", // “Dread it, run from it, destiny still arrives.” (Avengers: Infinity War)
        "Это прекрасная вещь – детонация бомб.", // “It’s a beautiful thing, the destruction of words.” (1984)
        "Кто-то считает себя слишком умным для меня. Они все так думают поначалу.", // Someone thinks they’re too clever for us. They all think that at first. (Invincible)

        // Specific to Russian culture
        "Хочешь, я взорву все бомбы, что мешают спать?",  // Хочешь я взорву все звёзды, что мешают спать? — Song, Земфира - Хочешь?
        "И у бомбы нашей села батарейка.",  // И у любви нашей села батарейка. — Song, Жуки - Батарейка
        "Какая гадость, какая гадость эта ваша бомба...",  // Какая гадость, какая гадость эта ваша заливная рыба... — Movie "Ирония судьбы, или С лёгким паром!"
        "Научиться бы не взрываться по пустякам.",  // Научиться бы не париться по пустякам. — Song, Градусы - Научиться бы не париться
        "Астрологи объявили неделю бомб. Количество взрывов увеличилось вдвое.",  // Астрологи объявили неделю Глицина. Количество Y увеличилось вдвое. — Game, Heroes of Might and Magic III
        "Охлади своё взрывание.",  // Охлади своё трахание. — Game, bootleg GTA translation
        "Он взорвётся скоро, надо только ждать.",  // Он наступит скоро, надо только ждать. — Song, Егор Летов - Всё идет по плану
        "Мой серийный номер – на рукаве.",  // Мой порядковый номер - на рукаве. — Song, Кино - Группа крови
        "Экспертов нет, но вы держитесь, всего доброго.",  // Денег нет, но вы держитесь, всего доброго. — Dmitry Medvedev's infamous speech
        "Ух, бомба-то какая! Лепота!",  // Ух, красота-то какая! Лепота! — Movie "Иван Васильевич меняет профессию"
        "Семь раз отмерь, один отрежь.",  // Семь раз отмерь, один отрежь. — Russian proverb
        "Надо, сапёр, надо!",  // Надо, Федя, надо! — Movie "Операция Ы"
        "Может быть, тебе дать ещё ключ от квартиры, где руководства лежат?",  // Может быть, тебе дать ещё ключ от квартиры, где деньги лежат? — Movie "12 стульев"
        "Всё... Взрыва не будет... Электричество кончилось.",  // Всё... Кина не будет... Электричество кончилось. — Movie "Брилиантовая рука"
        "Сапёр ошибается только один раз.",  // Сапёр ошибается только один раз. — Common saying/Joke
        "Я надеюсь, что я не пострадаю.",  // Я надеюсь, что я не пострадаю. — Internet meme
        "Ты... не взрывайся, если что!...",  // Ты... заходи, если что!... — Movie "Жил был пёс"
        "Решение видишь? Вот и я не вижу. А оно есть.",  // Суслика видишь? Вот и я не вижу. А он есть. — Movie "ДМБ"
        "Шах и мат, эксперты!",  // Шах и мат, атеисты! — Internet meme
        "Хороший модуль. Решать я его, конечно, не буду.",  // Сильное заявление. Проверять его я, конечно, не буду. — Show, Необъяснимо, но факт
        "Ты кнопку нажал, должен был 6 секунд держать! Почему так мало?",  // Ты на пенёк сел, должен был косарь отдать! Почему так мало? — Internet meme
        "Слово «бомба» и слово «смерть» для вас означают одно и то же.",  // Слово "Ром" и слово "Смерть" для вас означают одно и то же. — Movie, Остров сокровищ
        "Неправильно, попробуй ещё раз.",  // Неправильно, попробуй ещё раз. — Internet meme
        "Сапёр хороший, эксперты плохие.",  // Царь хороший, бояре плохие. — Political catchphrase
        "Стар- туем!",  // Стартуем! — Internet meme
        "Неправильно ты, дядя Фёдор, бомбу обезвреживаешь.",  // Неправильно ты, дядя Фёдор, бутерброд ешь. — Cartoon "Простоквашино"
        "Я почему вредный был? Потому что у меня инструкций не было!",  // Я почему вредный был? Потому что у меня велосипеда не было! — Cartoon "Простоквашино"
        "У меня есть мысль, и я её думаю.",  // У меня есть мысль, и я её думаю. — Cartoon, 38 попугаев
        "Ну, бомба! Ну, погоди!",  // Ну, заяц! Ну, погоди! — Cartoon "Ну, погоди!"
        "Чудо враждебной техники!",  // Чудо враждебной техники! — Movie "Тайна третьей планеты"
        "Нельзя просто так взять, и обезвредить бомбу.",  // Нельзя просто так взять и войти в Мордор. — Meme from "The Lord of the Rings"
        "Я в своём обезврежи- вании настолько преисполнился.",  // Я в своём познании настолько преисполнился. — Internet meme
        "Бомба замини- рована.",  // Тапок заминирован — Internet meme
        "Это бомба, братан.",  // Это фиаско, братан. — Internet meme
        "Выпьем за бомбу!",  // Выпьем за любовь! — Toast phrase/Song Игорь Николаев - Выпьем за любовь
        "Укуси меня бомба!",  // Укуси меня пчела! — Cartoon "Смешарики"
        "Взорвать нельзя обезвредить.",  // Казнить нельзя помиловать. — Famous grammatical exercise
        "Египет- ская бомба!",  // Египетская сила! — Sitcom, Воронины
        "И мы взорваны!",  // И мы счастливы! — Sitcom, Счастливы вместе
        "Всё поймать стремится. Бомбу!",  // Всё поймать стремится молнию! — Song, КиШ - Дурак и молния
        "Что нас ждёт, бомба хранит молчанье.",  // Что нас ждёт, море хранит молчанье. — Song, Ария - Штиль
        "Сапёр ли я дрожащий или право имею...",  // Тварь ли я дрожащая или право имею... — Novel, Преступление и наказание (Фёдор Достоевский)
        "Порядок у модулей в киоске был взят.",  // Порядок у карт в киоске был взят — Internet meme
        "Короче, Сапёр. Я всё обезвредил, и в благородство играть не буду.",  // Короче, Меченый, я тебя спас и в благородство играть не буду. — Game, Stalker reference
        "Вы допустили потерю дорогостоящего модуля!",  // Вы допустили потерю дорогостоящего обмундирования! — Game, Fallout 2
        "Не брат ты мне, сапёр.",  // Не брат ты мне, гнида черножопая. — Movie, Брат
        "А жаренных проводов не хочешь?",  // А жаренных гвоздей не хочешь? — Cartoon, TMNT
        "Невежда обезвреживает бомбы руками, а мастер – силой своего духа.",  // Невежда передвигает предметы руками, а мастер - силой своего духа. — Game, Gothic, telekinesis reference
        "Незабудка – твой любимый цветок.",  // Незабудка – твой любимый цветок. — Pop song, Тима Белорусских - Незабудка
        "Эксперт, у нас босс. Возможно Сувенир. По коням!",  // Андрюх, у нас труп. Возможно криминал. По коням! — Series, Улицы разбитых фонарей
        "Бомба слабее торпеды и ракеты, но в цирке бомба не выступает."  // Волк слабее льва и тигра, но в цирке Волк не выступает. — Internet meme
    );
}
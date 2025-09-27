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
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [S0.Number] = new()
                {
                    // English: What was the initially displayed number in {0}?
                    Question = "Какое число было изначально показано на {0}?",
                },
            },
        },

        [typeof(S1000Words)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "1000 слов",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [S1000Words.Words] = new()
                {
                    // English: What was the {1} word shown in {0}?
                    // Example: What was the first word shown in 1000 Words?
                    Question = "Какое было {1}-е показанное слово {0}?",
                },
            },
            Discriminators = new()
            {
                [S1000Words.Discriminator] = new()
                {
                    // English: the 1000 Words where the {0} word was {1}
                    // Example: the 1000 Words where the first word was Baken
                    Discriminator = "in the 1000 Words where the {0} word was {1}?",
                },
            },
        },

        [typeof(S100LevelsOfDefusal)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "100 уровнях обезвреживания",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [S100LevelsOfDefusal.Letters] = new()
                {
                    // English: What was the {1} displayed letter in {0}?
                    // Example: What was the first displayed letter in 100 Levels of Defusal?
                    Question = "Какая была {1}-я показанная буква {0}?",
                },
            },
            Discriminators = new()
            {
                [S100LevelsOfDefusal.Discriminator] = new()
                {
                    // English: the 100 Levels of Defusal where the {0} displayed letter was {1}
                    // Example: the 100 Levels of Defusal where the first displayed letter was B
                    Discriminator = "the 100 Levels of Defusal where the {0} displayed letter was {1}",
                },
            },
        },

        [typeof(S123Game)] = new()
        {
            Questions = new()
            {
                [S123Game.Profile] = new()
                {
                    // English: Who was the opponent in {0}?
                    Question = "Кто был вашим оппонентом {0}?",
                },
                [S123Game.Name] = new()
                {
                    // English: Who was the opponent in {0}?
                    Question = "Кто был вашим оппонентом {0}?",
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
                    Question = "Каким был {1} {0}?",
                    Arguments = new()
                    {
                        ["your first move"] = "ваш 1-й ход",
                        ["Rustmate’s first move"] = "1-й ход Rustmate",
                        ["your second move"] = "ваш 2-й ход",
                        ["Rustmate’s second move"] = "2-й ход Rustmate",
                        ["your third move"] = "ваш 3-й ход",
                        ["Rustmate’s third move"] = "3-й ход Rustmate",
                        ["your fourth move"] = "ваш 4-й ход",
                        ["Rustmate’s fourth move"] = "4-й ход Rustmate",
                        ["your fifth move"] = "ваш 5-й ход",
                        ["Rustmate’s fifth move"] = "5-й ход Rustmate",
                        ["your sixth move"] = "ваш 6-й ход",
                        ["Rustmate’s sixth move"] = "6-й ход Rustmate",
                        ["your seventh move"] = "ваш 7-й ход",
                        ["Rustmate’s seventh move"] = "7-й ход Rustmate",
                        ["your eighth move"] = "ваш 8-й ход",
                        ["Rustmate’s eighth move"] = "8-й ход Rustmate",
                    },
                },
            },
            Discriminators = new()
            {
                [S1DChess.Discriminator] = new()
                {
                    // English: the 1D Chess where {1} was {0}
                    // Example: the 1D Chess where your first move was B a→c
                    Discriminator = "the 1D Chess where {1} was {0}",
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
            ModuleName = "3D лабиринт",
            Conjugation = Conjugation.NominativeMasculine,
            Questions = new()
            {
                [S3DMaze.QMarkings] = new()
                {
                    // English: What were the markings in {0}?
                    Question = "Какими буквами был обозначен ваш {0}?",
                },
                [S3DMaze.QBearing] = new()
                {
                    // English: What was the cardinal direction in {0}?
                    Question = "Какое было направление нужной стены {0}?",
                    Answers = new()
                    {
                        ["North"] = "Север",
                        ["South"] = "Юг",
                        ["West"] = "Запад",
                        ["East"] = "Восток",
                    },
                },
            },
            Discriminators = new()
            {
                [S3DMaze.DMarkings] = new()
                {
                    // English: the 3D Maze whose markings were {0}
                    // Example: the 3D Maze whose markings were ABC
                    Discriminator = "the 3D Maze whose markings were {0}",
                },
                [S3DMaze.DBearing] = new()
                {
                    // English: the 3D Maze whose cardinal direction was {0}
                    // Example: the 3D Maze whose cardinal direction was North
                    Discriminator = "the 3D Maze whose cardinal direction was {0}",
                    Arguments = new()
                    {
                        ["North"] = "North",
                        ["South"] = "South",
                        ["West"] = "West",
                        ["East"] = "East",
                    },
                },
            },
        },

        [typeof(S3DTapCode)] = new()
        {
            Questions = new()
            {
                [S3DTapCode.Word] = new()
                {
                    // English: What was the received word in {0}?
                    Question = "Какое слово было получено {0}?",
                },
            },
        },

        [typeof(S3DTunnels)] = new()
        {
            ModuleName = "3D тоннелях",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [S3DTunnels.TargetNode] = new()
                {
                    // English: What was the {1} goal node in {0}?
                    // Example: What was the first goal node in 3D Tunnels?
                    Question = "Какой символ был вашей {1}-й целью {0}?",
                },
            },
        },

        [typeof(S3LEDs)] = new()
        {
            Conjugation = Conjugation.GenitivePlural,
            Questions = new()
            {
                [S3LEDs.InitialState] = new()
                {
                    // English: What was the initial state of the LEDs in {0} (in reading order)?
                    Question = "Какое было исходное состояние у {0} (в порядке чтения)?",
                    Answers = new()
                    {
                        ["off/off/off"] = "выкл/выкл/выкл",
                        ["off/off/on"] = "выкл/выкл/вкл",
                        ["off/on/off"] = "выкл/вкл/выкл",
                        ["off/on/on"] = "выкл/вкл/вкл",
                        ["on/off/off"] = "вкл/выкл/выкл",
                        ["on/off/on"] = "вкл/выкл/вкл",
                        ["on/on/off"] = "вкл/вкл/выкл",
                        ["on/on/on"] = "вкл/вкл/вкл",
                    },
                },
            },
        },

        [typeof(S3NPlus1)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [S3NPlus1.Question] = new()
                {
                    // English: What number was initially displayed in {0}?
                    Question = "Какое число было изначально показано на {0}?",
                },
            },
        },

        [typeof(S64)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [S64.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Какое число было показано на {0}?",
                },
            },
        },

        [typeof(S7)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [S7.QInitialValues] = new()
                {
                    // English: What was the {1} channel’s initial value in {0}?
                    // Example: What was the red channel’s initial value in 7?
                    Question = "Какое было начальное значение {1} канала у {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красного",
                        ["green"] = "зелёного",
                        ["blue"] = "синего",
                    },
                },
                [S7.QLedColors] = new()
                {
                    // English: What LED color was shown in stage {1} of {0}?
                    // Example: What LED color was shown in stage 1 of 7?
                    Question = "Какой цвет был у светодиода на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красный",
                        ["blue"] = "Синий",
                        ["green"] = "Зелёный",
                        ["white"] = "Белый",
                    },
                },
            },
            Discriminators = new()
            {
                [S7.DInitialValues] = new()
                {
                    // English: the 7 whose {0} channel’s initial value was {1}
                    // Example: the 7 whose red channel’s initial value was -9
                    Discriminator = "the 7 whose {1} channel’s initial value was {0}",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
                [S7.DLedColors] = new()
                {
                    // English: the 7 whose stage {0} LED color was {1}
                    // Example: the 7 whose stage 1 LED color was red
                    Discriminator = "the 7 whose stage {0} LED color was {1}",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["white"] = "white",
                    },
                },
            },
        },

        [typeof(S9Ball)] = new()
        {
            Questions = new()
            {
                [S9Ball.Letters] = new()
                {
                    // English: What was the number of ball {1} in {0}?
                    // Example: What was the number of ball A in 9-Ball?
                    Question = "Какой был номер у шара \"{1}\" {0}?",
                },
                [S9Ball.Numbers] = new()
                {
                    // English: What was the letter of ball {1} in {0}?
                    // Example: What was the letter of ball 2 in 9-Ball?
                    Question = "Какая была буква у шара \"{1}\" {0}?",
                },
            },
        },

        [typeof(SAbyss)] = new()
        {
            Questions = new()
            {
                [SAbyss.Seed] = new()
                {
                    // English: What was the {1} character displayed on {0}?
                    // Example: What was the first character displayed on Abyss?
                    Question = "Какой был {1}-й показанный символ {0}?",
                },
            },
        },

        [typeof(SAccumulation)] = new()
        {
            ModuleName = "Накопления",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SAccumulation.BorderColor] = new()
                {
                    // English: What was the border color in {0}?
                    Question = "Какого цвета было обрамление у {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Синего",
                        ["Brown"] = "Коричневого",
                        ["Green"] = "Зелёного",
                        ["Grey"] = "Серого",
                        ["Lime"] = "Салатового",
                        ["Orange"] = "Оранжевого",
                        ["Pink"] = "Розового",
                        ["Red"] = "Красного",
                        ["White"] = "Белого",
                        ["Yellow"] = "Жёлтого",
                    },
                },
                [SAccumulation.BackgroundColor] = new()
                {
                    // English: What was the background color on the {1} stage in {0}?
                    // Example: What was the background color on the first stage in Accumulation?
                    Question = "Какого цвета была подложка на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Синего",
                        ["Brown"] = "Коричневого",
                        ["Green"] = "Зелёного",
                        ["Grey"] = "Серого",
                        ["Lime"] = "Салатового",
                        ["Orange"] = "Оранжевого",
                        ["Pink"] = "Розового",
                        ["Red"] = "Красного",
                        ["White"] = "Белого",
                        ["Yellow"] = "Жёлтого",
                    },
                },
            },
        },

        [typeof(SAdventureGame)] = new()
        {
            ModuleName = "Приключении",
            Questions = new()
            {
                [SAdventureGame.CorrectItem] = new()
                {
                    // English: Which item was the {1} correct item you used in {0}?
                    // Example: Which item was the first correct item you used in Adventure Game?
                    Question = "Какой был {1}-й правильный предмет, который вы использовали {0}?",
                },
                [SAdventureGame.Enemy] = new()
                {
                    // English: What enemy were you fighting in {0}?
                    Question = "С каким врагом вы сражались {0}?",
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
                    Question = "Who was the {1} mercenary you killed in {0}?",
                },
            },
        },

        [typeof(SALetter)] = new()
        {
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SALetter.InitialLetter] = new()
                {
                    // English: What was the initial letter in {0}?
                    Question = "Какая была начальная буква {0}?",
                },
            },
        },

        [typeof(SAlfaBravo)] = new()
        {
            Questions = new()
            {
                [SAlfaBravo.PressedLetter] = new()
                {
                    // English: Which letter was pressed in {0}?
                    Question = "Какая буква была нажата {0}?",
                },
                [SAlfaBravo.LeftPressedLetter] = new()
                {
                    // English: Which letter was to the left of the pressed one in {0}?
                    Question = "Какая буква была слева от нажатой {0}?",
                },
                [SAlfaBravo.RightPressedLetter] = new()
                {
                    // English: Which letter was to the right of the pressed one in {0}?
                    Question = "Какая буква была справа от нажатой {0}?",
                },
                [SAlfaBravo.Digit] = new()
                {
                    // English: What was the last digit on the small display in {0}?
                    Question = "Какая была последняя цифра на маленьком экране {0}?",
                },
            },
        },

        [typeof(SAlgebra)] = new()
        {
            ModuleName = "Алгебре",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SAlgebra.Equation1] = new()
                {
                    // English: What was the first equation in {0}?
                    Question = "Какое было первое уравнение {0}?",
                },
                [SAlgebra.Equation2] = new()
                {
                    // English: What was the second equation in {0}?
                    Question = "Какое было второе уравнение {0}?",
                },
            },
        },

        [typeof(SAlgorithmia)] = new()
        {
            Questions = new()
            {
                [SAlgorithmia.Positions] = new()
                {
                    // English: Which position was the {1} position in {0}?
                    // Example: Which position was the starting position in Algorithmia?
                    Question = "Какая позиция была {1} {0}?",
                    Arguments = new()
                    {
                        ["starting"] = "начальной",
                        ["goal"] = "целевой",
                    },
                },
                [SAlgorithmia.Color] = new()
                {
                    // English: What was the color of the colored bulb in {0}?
                    Question = "Какого цвета была цветная лампочка {0}?",
                },
                [SAlgorithmia.Seed] = new()
                {
                    // English: Which number was present in the seed in {0}?
                    Question = "Какое число присутствовало в зерне {0}?",
                },
            },
        },

        [typeof(SAlphabeticalRuling)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SAlphabeticalRuling.Letter] = new()
                {
                    // English: What was the letter displayed in the {1} stage of {0}?
                    // Example: What was the letter displayed in the first stage of Alphabetical Ruling?
                    Question = "Какая буква была показана на {1}-м этапе {0}?",
                },
                [SAlphabeticalRuling.Number] = new()
                {
                    // English: What was the number displayed in the {1} stage of {0}?
                    // Example: What was the number displayed in the first stage of Alphabetical Ruling?
                    Question = "Какое число было показано на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SAlphabetNumbers)] = new()
        {
            Questions = new()
            {
                [SAlphabetNumbers.DisplayedNumbers] = new()
                {
                    // English: Which of these numbers was on one of the buttons in the {1} stage of {0}?
                    // Example: Which of these numbers was on one of the buttons in the first stage of Alphabet Numbers?
                    Question = "Какое из этих чисел было на одной из кнопок на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SAlphabetTiles)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SAlphabetTiles.Cycle] = new()
                {
                    // English: What was the {1} letter shown during the cycle in {0}?
                    // Example: What was the first letter shown during the cycle in Alphabet Tiles?
                    Question = "В цикле {0}, какая была {1}-я буква?",
                },
                [SAlphabetTiles.MissingLetter] = new()
                {
                    // English: What was the missing letter in {0}?
                    Question = "Какая буква отсутствовала {0}?",
                },
            },
        },

        [typeof(SAlphaBits)] = new()
        {
            Questions = new()
            {
                [SAlphaBits.DisplayedCharacters] = new()
                {
                    // English: What character was displayed on the {1} screen on the {2} in {0}?
                    // Example: What character was displayed on the first screen on the left in Alpha-Bits?
                    Question = "Какой символ был на {1}-м экране {2} {0}?",
                    Arguments = new()
                    {
                        ["left"] = "слева",
                        ["right"] = "справа",
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
            Questions = new()
            {
                [SAmusementParks.Rides] = new()
                {
                    // English: Which ride was available in {0}?
                    Question = "Какой аттракцион был доступен {0}?",
                },
            },
        },

        [typeof(SAngelHernandez)] = new()
        {
            Questions = new()
            {
                [SAngelHernandez.MainLetter] = new()
                {
                    // English: What letter was shown by the raised buttons on the {1} stage on {0}?
                    // Example: What letter was shown by the raised buttons on the first stage on Ángel Hernández?
                    Question = "Какая буква была показана поднятой кнопкой на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SArena)] = new()
        {
            Questions = new()
            {
                [SArena.Damage] = new()
                {
                    // English: What was the maximum weapon damage of the attack phase in {0}?
                    Question = "Какой был максимальный урон оружия в фазе атаки {0}?",
                },
                [SArena.Enemies] = new()
                {
                    // English: Which enemy was present in the defend phase of {0}?
                    Question = "Какой враг присутствовал в фазе защиты {0}?",
                },
                [SArena.Numbers] = new()
                {
                    // English: Which was a number present in the grab phase of {0}?
                    Question = "Какое число присутствовало в фазе захвата {0}?",
                },
            },
        },

        [typeof(SArithmelogic)] = new()
        {
            Questions = new()
            {
                [SArithmelogic.Submit] = new()
                {
                    // English: What was the symbol on the submit button in {0}?
                    Question = "Какой символ был на кнопке отправки ответа {0}?",
                },
                [SArithmelogic.Numbers] = new()
                {
                    // English: Which number was selectable, but not the solution, in the {1} screen on {0}?
                    // Example: Which number was selectable, but not the solution, in the left screen on Arithmelogic?
                    Question = "Какое число присутствовало (но не являлось решением) на {1} экране {0}?",
                    Arguments = new()
                    {
                        ["left"] = "левом",
                        ["middle"] = "центральном",
                        ["right"] = "правом",
                    },
                },
            },
        },

        [typeof(SASCIIMaze)] = new()
        {
            Questions = new()
            {
                [SASCIIMaze.Characters] = new()
                {
                    // English: What was the {1} character displayed on {0}?
                    // Example: What was the first character displayed on ASCII Maze?
                    Question = "Какой был {1}-й символ, отображённый {0}?",
                },
            },
        },

        [typeof(SASquare)] = new()
        {
            Questions = new()
            {
                [SASquare.IndexColors] = new()
                {
                    // English: Which of these was an index color in {0}?
                    Question = "Какой из этих цветов был индексным {0}?",
                },
                [SASquare.CorrectColors] = new()
                {
                    // English: Which color was submitted {1} in {0}?
                    // Example: Which color was submitted first in A Square?
                    Question = "Какой цвет был отправлен {1}-м {0}?",
                },
            },
        },

        [typeof(SAudioMorse)] = new()
        {
            Questions = new()
            {
                [SAudioMorse.Sound] = new()
                {
                    // English: What was signaled in {0}?
                    Question = "Что было в сигнале {0}?",
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
                    Question = "Какое было {1}-е направление у стрелки-ловушки {0}?",
                },
                [SAzureButton.QNonDecoyArrowDirection] = new()
                {
                    // English: What was the {1} direction in the {2} non-decoy arrow in {0}?
                    // Example: What was the first direction in the first non-decoy arrow in Azure Button?
                    Question = "Какое было {1}-е направление у {2}-й стрелки (не ловушки) {0}?",
                },
                [SAzureButton.QT] = new()
                {
                    // English: What was T in {0}?
                    Question = "Какое была карта T {0}?",
                },
                [SAzureButton.QNotT] = new()
                {
                    // English: Which of these cards was shown in Stage 1, but not T, in {0}?
                    Question = "Какая из этих карт была показана на первом этапе (но не T) {0}?",
                },
                [SAzureButton.QM] = new()
                {
                    // English: What was M in {0}?
                    Question = "Какое значение было у M {0}?",
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
                    Arguments = new()
                    {
                        ["north"] = "north",
                        ["north-east"] = "north-east",
                        ["east"] = "east",
                        ["south-east"] = "south-east",
                        ["south"] = "south",
                        ["south-west"] = "south-west",
                        ["west"] = "west",
                        ["north-west"] = "north-west",
                    },
                },
                [SAzureButton.DNonDecoyArrowDirection] = new()
                {
                    // English: the Azure Button where the {1} non-decoy arrow went {0} at some point
                    // Example: the Azure Button where the first non-decoy arrow went north at some point
                    Discriminator = "the Azure Button where the {1} non-decoy arrow went {0} at some point",
                    Arguments = new()
                    {
                        ["north"] = "north",
                        ["north-east"] = "north-east",
                        ["east"] = "east",
                        ["south-east"] = "south-east",
                        ["south"] = "south",
                        ["south-west"] = "south-west",
                        ["west"] = "west",
                        ["north-west"] = "north-west",
                    },
                },
            },
        },

        [typeof(SBakery)] = new()
        {
            Questions = new()
            {
                [SBakery.Items] = new()
                {
                    // English: Which menu item was present in {0}?
                    Question = "Какая позиция меню присутствовала {0}?",
                },
            },
        },

        [typeof(SBamboozledAgain)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Повторном надувательстве",
            Questions = new()
            {
                [SBamboozledAgain.ButtonText] = new()
                {
                    // English: What text was initially shown on this button in {0}? (+ sprite)
                    // Example: What text was initially shown on this button in Bamboozled Again? (+ sprite)
                    Question = "What text was initially shown on this button in {0}?",
                },
                [SBamboozledAgain.ButtonColor] = new()
                {
                    // English: What was the initial color of this button in {0}? (+ sprite)
                    // Example: What was the initial color of this button in Bamboozled Again? (+ sprite)
                    Question = "What was the initial color of this button in {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Orange"] = "Оранжевого",
                        ["Yellow"] = "Жёлтого",
                        ["Lime"] = "Лаймового",
                        ["Green"] = "Зелёного",
                        ["Jade"] = "Нефритового",
                        ["Cyan"] = "Голубого",
                        ["Azure"] = "Лазурного",
                        ["Blue"] = "Синего",
                        ["Violet"] = "Фиолетового",
                        ["Magenta"] = "Пурпурного",
                        ["Rose"] = "Розового",
                        ["White"] = "Белого",
                        ["Grey"] = "Серого",
                        ["Black"] = "Чёрного",
                    },
                },
                [SBamboozledAgain.DisplayTexts1] = new()
                {
                    // English: What was the {1} decrypted text on the display in {0}?
                    // Example: What was the first decrypted text on the display in Bamboozled Again?
                    Question = "Какой был {1}-й расшифрованный текст на экране {0}?",
                },
                [SBamboozledAgain.DisplayTexts2] = new()
                {
                    // English: What was the {1} decrypted text on the display in {0}?
                    // Example: What was the first decrypted text on the display in Bamboozled Again?
                    Question = "Какой был {1}-й расшифрованный текст на экране {0}?",
                },
                [SBamboozledAgain.DisplayColor] = new()
                {
                    // English: What color was the {1} text on the display in {0}?
                    // Example: What color was the first text on the display in Bamboozled Again?
                    Question = "Какого цвета был {1}-й текст на экране {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Orange"] = "Оранжевого",
                        ["Yellow"] = "Жёлтого",
                        ["Lime"] = "Лаймового",
                        ["Green"] = "Зелёного",
                        ["Jade"] = "Нефритового",
                        ["Cyan"] = "Голубого",
                        ["Azure"] = "Лазурного",
                        ["Blue"] = "Синего",
                        ["Violet"] = "Фиолетового",
                        ["Magenta"] = "Пурпурного",
                        ["Rose"] = "Розового",
                        ["White"] = "Белого",
                        ["Grey"] = "Серого",
                    },
                },
            },
        },

        [typeof(SBamboozlingButton)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SBamboozlingButton.Color] = new()
                {
                    // English: What color was the button in the {1} stage of {0}?
                    // Example: What color was the button in the first stage of Bamboozling Button?
                    Question = "Какого цвета была кнопка на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Lime"] = "Лаймовый",
                        ["Green"] = "Зелёный",
                        ["Jade"] = "Нефритовый",
                        ["Cyan"] = "Голубой",
                        ["Azure"] = "Лазурный",
                        ["Blue"] = "Синий",
                        ["Violet"] = "Фиолетовый",
                        ["Magenta"] = "Пурпурный",
                        ["Rose"] = "Розовый",
                        ["White"] = "Белый",
                        ["Grey"] = "Серый",
                        ["Black"] = "Чёрный",
                    },
                },
                [SBamboozlingButton.DisplayColor] = new()
                {
                    // English: What was the color of the {2} display in the {1} stage of {0}?
                    // Example: What was the color of the first display in the first stage of Bamboozling Button?
                    Question = "Какого цвета был {2}-й экран на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Lime"] = "Лаймовый",
                        ["Green"] = "Зелёный",
                        ["Jade"] = "Нефритовый",
                        ["Cyan"] = "Голубой",
                        ["Azure"] = "Лазуритовый",
                        ["Blue"] = "Синий",
                        ["Violet"] = "Фиолетовый",
                        ["Magenta"] = "Пурпурный",
                        ["Rose"] = "Розовый",
                        ["White"] = "Белый",
                        ["Grey"] = "Серый",
                    },
                },
                [SBamboozlingButton.Display] = new()
                {
                    // English: What was the {2} display in the {1} stage of {0}?
                    // Example: What was the first display in the first stage of Bamboozling Button?
                    Question = "Какой был {2}-й экран на {1}-м этапе {0}?",
                },
                [SBamboozlingButton.Label] = new()
                {
                    // English: What was the {2} label on the button in the {1} stage of {0}?
                    // Example: What was the top label on the button in the first stage of Bamboozling Button?
                    Question = "Какая была {2} надпись на кнопке на {1}-м этапе {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхняя",
                        ["bottom"] = "нижняя",
                    },
                },
            },
        },

        [typeof(SBarCharts)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SBarCharts.Category] = new()
                {
                    // English: What was the category of {0}?
                    Question = "Какая была категория у {0}?",
                },
                [SBarCharts.Unit] = new()
                {
                    // English: What was the unit of {0}?
                    Question = "Какая была единица измерения у {0}?",
                },
                [SBarCharts.Label] = new()
                {
                    // English: What was the label of the {1} bar in {0}?
                    // Example: What was the label of the first bar in Bar Charts?
                    Question = "Какая надпись была у {1}-го столбца {0}?",
                },
                [SBarCharts.Color] = new()
                {
                    // English: What was the color of the {1} bar in {0}?
                    // Example: What was the color of the first bar in Bar Charts?
                    Question = "Какого цвета был {1}-й столбец {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Yellow"] = "Жёлтого",
                        ["Green"] = "Зелёного",
                        ["Blue"] = "Синего",
                    },
                },
                [SBarCharts.Height] = new()
                {
                    // English: What was the position of the {1} bar in {0}?
                    // Example: What was the position of the shortest bar in Bar Charts?
                    Question = "Где находился {1} столбец {0}?",
                    Arguments = new()
                    {
                        ["shortest"] = "самый короткий",
                        ["second shortest"] = "третий по высоте",
                        ["second tallest"] = "второй по высоте",
                        ["tallest"] = "самый высокий",
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
                    Question = "Какой был номер экрана {0}?",
                },
                [SBarcodeCipher.BarcodeEdgework] = new()
                {
                    // English: What was the edgework represented by the {1} barcode in {0}?
                    // Example: What was the edgework represented by the first barcode in Barcode Cipher?
                    Question = "Какой компонент бомбы был представлен {1}-м штрихкодом {0}?",
                    Answers = new()
                    {
                        ["SERIAL NUMBER"] = "SERIAL NUMBER",
                        ["BATTERIES"] = "BATTERIES",
                        ["BATTERY HOLDERS"] = "BATTERY HOLDERS",
                        ["PORTS"] = "PORTS",
                        ["PORT PLATES"] = "PORT PLATES",
                        ["LIT INDICATORS"] = "LIT INDICATORS",
                        ["UNLIT INDICATORS"] = "UNLIT INDICATORS",
                        ["INDICATORS"] = "INDICATORS",
                    },
                },
                [SBarcodeCipher.BarcodeAnswers] = new()
                {
                    // English: What was the answer for the {1} barcode in {0}?
                    // Example: What was the answer for the first barcode in Barcode Cipher?
                    Question = "Какой был ответ на {1}-й штрихкод {0}?",
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
                    Question = "Какой ингредиент был на {1}-й позиции {0}?",
                    Answers = new()
                    {
                        ["Adelhyde"] = "Adelhyde",
                        ["Flanergide"] = "Flanergide",
                        ["Bronson Extract"] = "Bronson Extract",
                        ["Karmotrine"] = "Karmotrine",
                        ["Powdered Delta"] = "Powdered Delta",
                    },
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
                    // English: What was this bean in {0}? (+ sprite)
                    Question = "Каким был данный боб {0}?",
                    Answers = new()
                    {
                        ["Wobbly Orange"] = "Wobbly Orange",
                        ["Wobbly Yellow"] = "Wobbly Yellow",
                        ["Wobbly Green"] = "Wobbly Green",
                        ["Not Wobbly Orange"] = "Not Wobbly Orange",
                        ["Not Wobbly Yellow"] = "Not Wobbly Yellow",
                        ["Not Wobbly Green"] = "Not Wobbly Green",
                    },
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
                    Question = "Каким был росток {1} {0}?",
                    Answers = new()
                    {
                        ["Raw"] = "Raw",
                        ["Cooked"] = "Cooked",
                        ["Burnt"] = "Burnt",
                        ["Fake"] = "Fake",
                    },
                },
                [SBeanSprouts.Beans] = new()
                {
                    // English: What bean was on sprout {1} in {0}?
                    // Example: What bean was on sprout 1 in Bean Sprouts?
                    Question = "Какой боб был на {1}-м ростке {0}?",
                    Answers = new()
                    {
                        ["Left"] = "Левый",
                        ["Right"] = "Правый",
                        ["None"] = "Никакой",
                        ["Both"] = "Оба",
                    },
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
                    Question = "Каким был боб {0}?",
                    Answers = new()
                    {
                        ["Wobbly Orange"] = "Wobbly Orange",
                        ["Wobbly Yellow"] = "Wobbly Yellow",
                        ["Wobbly Green"] = "Wobbly Green",
                        ["Not Wobbly Orange"] = "Not Wobbly Orange",
                        ["Not Wobbly Yellow"] = "Not Wobbly Yellow",
                        ["Not Wobbly Green"] = "Not Wobbly Green",
                    },
                },
            },
        },

        [typeof(SBigCircle)] = new()
        {
            ModuleName = "Большом круге",
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SBigCircle.Colors] = new()
                {
                    // English: What color was {1} in the solution to {0}?
                    // Example: What color was first in the solution to Big Circle?
                    Question = "Какой правильный цвет был {1}-м на {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Magenta"] = "Пурпурный",
                        ["White"] = "Белый",
                        ["Black"] = "Чёрный",
                    },
                },
            },
        },

        [typeof(SBinary)] = new()
        {
            ModuleName = "Двоичных светодиодах",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SBinary.Word] = new()
                {
                    // English: What word was displayed in {0}?
                    Question = "Какое слово было отображено на {0}?",
                },
            },
        },

        [typeof(SBinaryLEDs)] = new()
        {
            ModuleName = "Двоичных светодиодах",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SBinaryLEDs.Value] = new()
                {
                    // English: At which numeric value did you cut the correct wire in {0}?
                    Question = "На каком числе вы перерезали верный провод {0}?",
                },
            },
        },

        [typeof(SBinaryShift)] = new()
        {
            Questions = new()
            {
                [SBinaryShift.InitialNumber] = new()
                {
                    // English: What was the {1} initial number in {0}?
                    // Example: What was the top-left initial number in Binary Shift?
                    Question = "Какое было начальное число {1} {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "сверху слева",
                        ["top-middle"] = "сверху посередине",
                        ["top-right"] = "сверху справа",
                        ["left-middle"] = "посередине слева",
                        ["center"] = "в центре",
                        ["right-middle"] = "посередине справа",
                        ["bottom-left"] = "снизу слева",
                        ["bottom-middle"] = "снизу посередине",
                        ["bottom-right"] = "снизу справа",
                    },
                },
                [SBinaryShift.SelectedNumberPossition] = new()
                {
                    // English: What number was selected at stage {1} in {0}?
                    // Example: What number was selected at stage 0 in Binary Shift?
                    Question = "Какое число было выбрано на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["top-left"] = "Сверху слева",
                        ["top-middle"] = "Сверху посередине",
                        ["top-right"] = "Сверху справа",
                        ["left-middle"] = "Посередине слева",
                        ["center"] = "В центре",
                        ["right-middle"] = "Посередине справа",
                        ["bottom-left"] = "Снизу слева",
                        ["bottom-middle"] = "Снизу посередине",
                        ["bottom-right"] = "Снизу справа",
                    },
                },
                [SBinaryShift.NotSelectedNumberPossition] = new()
                {
                    // English: What number was not selected at stage {1} in {0}?
                    // Example: What number was not selected at stage 0 in Binary Shift?
                    Question = "Какое число не было выбрано на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["top-left"] = "Сверху слева",
                        ["top-middle"] = "Сверху посередине",
                        ["top-right"] = "Сверху справа",
                        ["left-middle"] = "Посередине слева",
                        ["center"] = "В центре",
                        ["right-middle"] = "Посередине справа",
                        ["bottom-left"] = "Снизу слева",
                        ["bottom-middle"] = "Снизу посередине",
                        ["bottom-right"] = "Снизу справа",
                    },
                },
            },
        },

        [typeof(SBitmaps)] = new()
        {
            ModuleName = "Битовых изображениях",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SBitmaps.Question] = new()
                {
                    // English: How many pixels were {1} in the {2} quadrant in {0}?
                    // Example: How many pixels were white in the top left quadrant in Bitmaps?
                    Question = "Сколько было {1} пикселей в {2} квадранте {0}?",
                    Arguments = new()
                    {
                        ["white"] = "белых",
                        ["top left"] = "левом верхнем",
                        ["top right"] = "правом верхнем",
                        ["bottom left"] = "нижнем левом",
                        ["bottom right"] = "нижнем правом",
                        ["black"] = "чёрных",
                    },
                },
            },
        },

        [typeof(SBlackCipher)] = new()
        {
            Questions = new()
            {
                [SBlackCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Black Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
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
                    Answers = new()
                    {
                        ["Yahtzee"] = "Yahtzee",
                        ["Large Straight"] = "Large Straight",
                        ["Small Straight"] = "Small Straight",
                        ["Full House"] = "Full House",
                        ["Four of a Kind"] = "Four of a Kind",
                        ["Chance"] = "Chance",
                        ["Three of a Kind"] = "Three of a Kind",
                        ["1s"] = "1s",
                        ["2s"] = "2s",
                        ["3s"] = "3s",
                        ["4s"] = "4s",
                        ["5s"] = "5s",
                        ["6s"] = "6s",
                    },
                },
            },
        },

        [typeof(SBlindMaze)] = new()
        {
            Questions = new()
            {
                [SBlindMaze.Colors] = new()
                {
                    // English: What color was the {1} button in {0}?
                    // Example: What color was the north button in Blind Maze?
                    Question = "Какого цвета была {1} кнопка {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Green"] = "Зелёного",
                        ["Blue"] = "Синего",
                        ["Gray"] = "Серого",
                        ["Yellow"] = "Жёлтого",
                    },
                    Arguments = new()
                    {
                        ["north"] = "северная",
                        ["east"] = "восточная",
                        ["west"] = "западная",
                        ["south"] = "южная",
                    },
                },
                [SBlindMaze.Maze] = new()
                {
                    // English: Which maze did you solve {0} on?
                    Question = "Какой лабиринт вы прошли {0}?",
                },
            },
        },

        [typeof(SBlinkingNotes)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SBlinkingNotes.Song] = new()
                {
                    // English: What song was flashed in {0}?
                    Question = "Какая песня мигала на {0}?",
                },
            },
        },

        [typeof(SBlinkstop)] = new()
        {
            Questions = new()
            {
                [SBlinkstop.NumberOfFlashes] = new()
                {
                    // English: How many times did the LED flash in {0}?
                    Question = "Сколько раз мигал светодиод {0}?",
                },
                [SBlinkstop.FewestFlashedColor] = new()
                {
                    // English: Which color did the LED flash the fewest times in {0}?
                    Question = "Каким цветом светодиод мигал наименьшее количество раз {0}?",
                    Answers = new()
                    {
                        ["Purple"] = "Фиолетовый",
                        ["Cyan"] = "Голубой",
                        ["Yellow"] = "Жёлтый",
                        ["Multicolor"] = "Разноцветный",
                    },
                },
            },
        },

        [typeof(SBlockbusters)] = new()
        {
            Questions = new()
            {
                [SBlockbusters.LastLetter] = new()
                {
                    // English: What was the last letter pressed on {0}?
                    Question = "Какая буква была нажата последней {0}?",
                },
            },
        },

        [typeof(SBlueArrows)] = new()
        {
            Questions = new()
            {
                [SBlueArrows.InitialCharacters] = new()
                {
                    // English: What were the characters on the screen in {0}?
                    Question = "Какие символы были на экране {0}?",
                },
            },
        },

        [typeof(SBlueButton)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SBlueButton.D] = new()
                {
                    // English: What was D in {0}?
                    Question = "Какое значение было у D на {0}?",
                },
                [SBlueButton.EFGH] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was E in Blue Button?
                    Question = "Какое значение было у {1} на {0}?",
                },
                [SBlueButton.M] = new()
                {
                    // English: What was M in {0}?
                    Question = "Какое значение было у M на {0}?",
                },
                [SBlueButton.N] = new()
                {
                    // English: What was N in {0}?
                    Question = "Какое значение было у N на {0}?",
                },
                [SBlueButton.P] = new()
                {
                    // English: What was P in {0}?
                    Question = "Какое значение было у P на {0}?",
                },
                [SBlueButton.Q] = new()
                {
                    // English: What was Q in {0}?
                    Question = "Какое значение было у D на {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Cyan"] = "Cyan",
                        ["Red"] = "Red",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                    },
                },
                [SBlueButton.X] = new()
                {
                    // English: What was X in {0}?
                    Question = "Какое значение было у X на {0}?",
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
            Questions = new()
            {
                [SBlueCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Blue Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SBobBarks)] = new()
        {
            Questions = new()
            {
                [SBobBarks.Indicators] = new()
                {
                    // English: What was the {1} indicator label in {0}?
                    // Example: What was the top left indicator label in Bob Barks?
                    Question = "Какая была надпись {1} индикатора {0}?",
                    Arguments = new()
                    {
                        ["top left"] = "верхнего левого",
                        ["top right"] = "верхнего правого",
                        ["bottom left"] = "нижнего левого",
                        ["bottom right"] = "нижнего правого",
                    },
                },
                [SBobBarks.Positions] = new()
                {
                    // English: Which button flashed {1} in sequence in {0}?
                    // Example: Which button flashed first in sequence in Bob Barks?
                    Question = "Какая кнопка была {1}-й в последовательности вспышек {0}?",
                    Answers = new()
                    {
                        ["top left"] = "Верхняя левая",
                        ["top right"] = "Верхняя правая",
                        ["bottom left"] = "Нижняя левая",
                        ["bottom right"] = "Нижняя правая",
                    },
                },
            },
        },

        [typeof(SBoggle)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SBoggle.Letters] = new()
                {
                    // English: What letter was initially visible on {0}?
                    Question = "Какая буква была изначально видна на {0}?",
                },
            },
        },

        [typeof(SBombDiffusal)] = new()
        {
            Questions = new()
            {
                [SBombDiffusal.LicenseNumber] = new()
                {
                    // English: What was the license number in {0}?
                    Question = "Какой был номер лицензии {0}?",
                },
            },
        },

        [typeof(SBoneAppleTea)] = new()
        {
            ModuleName = "Еле-еле ели ели",
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SBoneAppleTea.Phrase] = new()
                {
                    // English: Which phrase was shown on {0}?
                    Question = "Какая фраза была показана на {0}?",
                },
            },
        },

        [typeof(SBoobTube)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SBoobTube.Word] = new()
                {
                    // English: Which word was shown on {0}?
                    Question = "Какое слово было показано на {0}?",
                },
            },
        },

        [typeof(SBookOfMario)] = new()
        {
            Questions = new()
            {
                [SBookOfMario.Pictures] = new()
                {
                    // English: Who said the {1} quote in {0}?
                    // Example: Who said the first quote in Book of Mario?
                    Question = "Кто сказал {1}-ю цитату {0}?",
                },
                [SBookOfMario.Quotes] = new()
                {
                    // English: What did {1} say in the {2} stage of {0}?
                    // Example: What did Goombell say in the first stage of Book of Mario?
                    Question = "Что сказал {1} на {2}-м этапе {0}?",
                },
            },
        },

        [typeof(SBooleanWires)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SBooleanWires.EnteredOperators] = new()
                {
                    // English: Which operator did you submit in the {1} stage of {0}?
                    // Example: Which operator did you submit in the first stage of Boolean Wires?
                    Question = "Какой оператор был ответом на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SBoomtarTheGreat)] = new()
        {
            Questions = new()
            {
                [SBoomtarTheGreat.Rules] = new()
                {
                    // English: What was rule {1} in {0}?
                    // Example: What was rule one in Boomtar the Great?
                    Question = "Какое было {1} правило {0}?",
                    Arguments = new()
                    {
                        ["one"] = "первое",
                        ["two"] = "второе",
                    },
                },
            },
        },

        [typeof(SBorderedKeys)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SBorderedKeys.BorderColor] = new()
                {
                    // English: What was the {1} key’s border color when it was pressed in {0}?
                    // Example: What was the first key’s border color when it was pressed in Bordered Keys?
                    Question = "Какого цвета была рамка, когда вы нажали {1}-ю клавишу {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                    },
                },
                [SBorderedKeys.Digit] = new()
                {
                    // English: What was the digit displayed when the {1} key was pressed in {0}?
                    // Example: What was the digit displayed when the first key was pressed in Bordered Keys?
                    Question = "Какая цифра отображалась на дисплее, когда вы нажали {1}-ю клавишу {0}?",
                },
                [SBorderedKeys.KeyColor] = new()
                {
                    // English: What was the {1} key’s key color when it was pressed in {0}?
                    // Example: What was the first key’s key color when it was pressed in Bordered Keys?
                    Question = "Какого цвета была клавиша, когда вы нажали {1}-ю клавишу {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                    },
                },
                [SBorderedKeys.Label] = new()
                {
                    // English: What was the {1} key’s label when it was pressed in {0}?
                    // Example: What was the first key’s label when it was pressed in Bordered Keys?
                    Question = "Какая была надпись, когда вы нажали {1}-ю клавишу {0}?",
                },
                [SBorderedKeys.LabelColor] = new()
                {
                    // English: What was the {1} key’s label color when it was pressed in {0}?
                    // Example: What was the first key’s label color when it was pressed in Bordered Keys?
                    Question = "Какого цвета была надпись, когда вы нажали {1}-ю клавишу {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                    },
                },
            },
        },

        [typeof(SBottomGear)] = new()
        {
            Questions = new()
            {
                [SBottomGear.Tweet] = new()
                {
                    // English: What tweet was shown in {0}?
                    Question = "Какой твит был показан {0}?",
                },
            },
        },

        [typeof(SBoxing)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SBoxing.StrengthByContestant] = new()
                {
                    // English: What was {1}’s strength rating on {0}?
                    // Example: What was Muhammad’s strength rating on Boxing?
                    Question = "Какая была оценка силы у {1} {0}?",
                },
                [SBoxing.ContestantByStrength] = new()
                {
                    // English: What was the {1} of the contestant with strength rating {2} on {0}?
                    // Example: What was the first name of the contestant with strength rating 0 on Boxing?
                    Question = "{1} участника с оценкой силы {2} {0}?",
                    Arguments = new()
                    {
                        ["first name"] = "Какое было имя",
                        ["last name"] = "Какая была фамилия",
                        ["substitute’s first name"] = "Какое было имя запасного",
                        ["substitute’s last name"] = "Какая была фамилия запасного",
                    },
                },
                [SBoxing.Names] = new()
                {
                    // English: Which {1} appeared on {0}?
                    // Example: Which contestant’s first name appeared on Boxing?
                    Question = "{1} было показано на {0}?",
                    Arguments = new()
                    {
                        ["contestant’s first name"] = "Какое имя участника",
                        ["contestant’s last name"] = "Какая фамилия участника",
                        ["substitute’s first name"] = "Какое имя запасного участника",
                        ["substitute’s last name"] = "Какая фамилия запасного участника",
                    },
                },
            },
        },

        [typeof(SBraille)] = new()
        {
            ModuleName = "Шрифта Брайля",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SBraille.Pattern] = new()
                {
                    // English: What was the {1} pattern in {0}?
                    // Example: What was the first pattern in Braille?
                    Question = "Какой был {1}-й паттерн {0}?",
                },
            },
        },

        [typeof(SBreakfastEgg)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SBreakfastEgg.Color] = new()
                {
                    // English: Which color appeared on the egg in {0}?
                    Question = "Какой был цвет у {0}?",
                    Answers = new()
                    {
                        ["Crimson"] = "Crimson",
                        ["Orange"] = "Orange",
                        ["Pink"] = "Pink",
                        ["Beige"] = "Beige",
                        ["Cyan"] = "Cyan",
                        ["Lime"] = "Lime",
                        ["Petrol"] = "Petrol",
                    },
                },
            },
        },

        [typeof(SBrokenButtons)] = new()
        {
            ModuleName = "Сломанных кнопках",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SBrokenButtons.Question] = new()
                {
                    // English: What was the {1} correct button you pressed in {0}?
                    // Example: What was the first correct button you pressed in Broken Buttons?
                    Question = "Какая была {1}-я правильная нажатая кнопка {0}?",
                },
            },
        },

        [typeof(SBrokenGuitarChords)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SBrokenGuitarChords.DisplayedChord] = new()
                {
                    // English: What was the displayed chord in {0}?
                    Question = "Какой аккорд был показан на {0}?",
                },
                [SBrokenGuitarChords.MutedString] = new()
                {
                    // English: In which position, from left to right, was the broken string in {0}?
                    Question = "На какой позиции (слева направо) была сломанная струна {0}?",
                },
            },
        },

        [typeof(SBrownCipher)] = new()
        {
            Questions = new()
            {
                [SBrownCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Brown Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SBrushStrokes)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SBrushStrokes.MiddleColor] = new()
                {
                    // English: What was the color of the middle contact point in {0}?
                    Question = "Какого цвета была центральная точка {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Lime"] = "Lime",
                        ["Green"] = "Green",
                        ["Cyan"] = "Cyan",
                        ["Sky"] = "Sky",
                        ["Blue"] = "Blue",
                        ["Purple"] = "Purple",
                        ["Magenta"] = "Magenta",
                        ["Brown"] = "Brown",
                        ["White"] = "White",
                        ["Gray"] = "Gray",
                        ["Black"] = "Black",
                        ["Pink"] = "Pink",
                    },
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
            Questions = new()
            {
                [SBurgerAlarm.Digits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Burger Alarm?
                    Question = "Какая была {1}-я цифра {0}?",
                },
                [SBurgerAlarm.OrderNumbers] = new()
                {
                    // English: What was the {1} order number in {0}?
                    // Example: What was the first order number in Burger Alarm?
                    Question = "Какой был номер {1}-го заказа {0}?",
                },
            },
        },

        [typeof(SBurglarAlarm)] = new()
        {
            ModuleName = "Сигнализации",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SBurglarAlarm.Digits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Burglar Alarm?
                    Question = "Какая была {1}-я цифра {0}?",
                },
            },
        },

        [typeof(SButton)] = new()
        {
            ModuleName = "Кнопки",
            Conjugation = Conjugation.GenitiveFeminine,
            Questions = new()
            {
                [SButton.LightColor] = new()
                {
                    // English: What color did the light glow in {0}?
                    Question = "Каким цветом горела цветная полоска {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красным",
                        ["blue"] = "Синим",
                        ["yellow"] = "Жёлтым",
                        ["white"] = "Белым",
                    },
                },
            },
        },

        [typeof(SButtonage)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SButtonage.Buttons] = new()
                {
                    // English: How many {1} buttons were there on {0}?
                    // Example: How many red buttons were there on Buttonage?
                    Question = "Сколько {1} было на {0}?",
                    Arguments = new()
                    {
                        ["red"] = "кнопок красного цвета",
                        ["green"] = "кнопок зелёного цвета",
                        ["orange"] = "кнопок оранжевого цвета",
                        ["blue"] = "кнопок синего цвета",
                        ["pink"] = "кнопок розового цвета",
                        ["white"] = "кнопок белого цвета",
                        ["black"] = "кнопок чёрного цвета",
                        ["white-bordered"] = "кнопок с белой рамкой",
                        ["pink-bordered"] = "кнопок с розовой рамкой",
                        ["gray-bordered"] = "кнопок с серой рамкой",
                        ["red-bordered"] = "кнопок с красной рамкой",
                        ["“P”"] = "кнопок с буквой \"P\"",
                        ["special"] = "специальных кнопок",
                    },
                },
            },
        },

        [typeof(SButtonSequence)] = new()
        {
            ModuleName = "Последовательности кнопок",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SButtonSequence.sColorOccurrences] = new()
                {
                    // English: How many of the buttons in {0} were {1}?
                    // Example: How many of the buttons in Button Sequence were red?
                    Question = "Сколько было {1} кнопок {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красных",
                        ["blue"] = "синих",
                        ["yellow"] = "жёлтых",
                        ["white"] = "белых",
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
                    Question = "Какого цвета был светодиод на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Blue",
                        ["Lime"] = "Lime",
                        ["Orange"] = "Orange",
                        ["Red"] = "Red",
                    },
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
            Questions = new()
            {
                [SCaesarPsycho.ScreenTexts] = new()
                {
                    // English: What text was on the top display in the {1} stage of {0}?
                    // Example: What text was on the top display in the first stage of Caesar Psycho?
                    Question = "Какой текст был на верхнем экране на {1}-м этапе {0}?",
                },
                [SCaesarPsycho.ScreenColor] = new()
                {
                    // English: What color was the text on the top display in the second stage of {0}?
                    Question = "Какого цвета был текст на верхнем экране на втором этапе {0}?",
                },
            },
        },

        [typeof(SCalendar)] = new()
        {
            ModuleName = "Календаре",
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SCalendar.LedColor] = new()
                {
                    // English: What was the LED color in {0}?
                    Question = "Какого цвета был индикатор на {0}?",
                    Answers = new()
                    {
                        ["Green"] = "Зелёный",
                        ["Yellow"] = "Жёлтый",
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                    },
                },
            },
        },

        [typeof(SCARPS)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SCARPS.Cell] = new()
                {
                    // English: What color was this cell initially in {0}? (+ sprite)
                    Question = "Какого цвета была эта клетка в начале {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Black"] = "Black",
                    },
                },
            },
        },

        [typeof(SCartinese)] = new()
        {
            Questions = new()
            {
                [SCartinese.ButtonColors] = new()
                {
                    // English: What color was the {1} button in {0}?
                    // Example: What color was the up button in Cartinese?
                    Question = "Какого цвета была кнопка \"{1}\" {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Yellow"] = "Жёлтого",
                        ["Green"] = "Зелёного",
                        ["Blue"] = "Синего",
                    },
                    Arguments = new()
                    {
                        ["up"] = "вверх",
                        ["right"] = "вправо",
                        ["down"] = "вниз",
                        ["left"] = "влево",
                    },
                },
                [SCartinese.Lyrics] = new()
                {
                    // English: What lyric was played by the {1} button in {0}?
                    // Example: What lyric was played by the up button in Cartinese?
                    Question = "Какая лирика прозвучала при нажатии кнопки \"{1}\" {0}?",
                    Arguments = new()
                    {
                        ["up"] = "вверх",
                        ["right"] = "вправо",
                        ["down"] = "вниз",
                        ["left"] = "влево",
                    },
                },
            },
        },

        [typeof(SCatchphrase)] = new()
        {
            Questions = new()
            {
                [SCatchphrase.Colour] = new()
                {
                    // English: What was the colour of the {1} panel in {0}?
                    // Example: What was the colour of the top-left panel in Catchphrase?
                    Question = "Какого цвета была панель {1} {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Orange"] = "Оранжевый",
                        ["Purple"] = "Фиолетовый",
                        ["Yellow"] = "Жёлтый",
                    },
                    Arguments = new()
                    {
                        ["top-left"] = "сверху слева",
                        ["top-right"] = "сверху справа",
                        ["bottom-left"] = "снизу слева",
                        ["bottom-right"] = "снизу справа",
                    },
                },
            },
        },

        [typeof(SChallengeAndContact)] = new()
        {
            Questions = new()
            {
                [SChallengeAndContact.Answers] = new()
                {
                    // English: What was the {1} submitted answer in {0}?
                    // Example: What was the first submitted answer in Challenge & Contact?
                    Question = "Какой был {1}-й введённый ответ {0}?",
                },
            },
        },

        [typeof(SCharacterCodes)] = new()
        {
            Questions = new()
            {
                [SCharacterCodes.Character] = new()
                {
                    // English: What was the {1} character in {0}?
                    // Example: What was the first character in Character Codes?
                    Question = "Какой был {1}-й символ {0}?",
                },
            },
        },

        [typeof(SCharacterShift)] = new()
        {
            Questions = new()
            {
                [SCharacterShift.Letters] = new()
                {
                    // English: Which letter was present but not submitted on the left slider of {0}?
                    Question = "Какой символ присутствовал, но не был введён на левом ползунке {0}?",
                },
                [SCharacterShift.Digits] = new()
                {
                    // English: Which digit was present but not submitted on the right slider of {0}?
                    Question = "Какая цифра присутствовала, но не была введён на правом ползунке {0}?",
                },
            },
        },

        [typeof(SCharacterSlots)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SCharacterSlots.DisplayedCharacters] = new()
                {
                    // English: Who was displayed in the {1} slot in the {2} stage of {0}?
                    // Example: Who was displayed in the first slot in the first stage of Character Slots?
                    Question = "Кто был показан в {1}-м слоте на {2}-м этапе {0}?",
                },
            },
        },

        [typeof(SCheapCheckout)] = new()
        {
            ModuleName = "Свободной кассе",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SCheapCheckout.Paid] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was the paid amount in Cheap Checkout?
                    Question = "{1} {0}?",
                    Arguments = new()
                    {
                        ["the paid amount"] = "Сколько всего денег было заплачено",
                        ["the first paid amount"] = "Каким был первый платёж",
                        ["the second paid amount"] = "Каким был второй платёж",
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
                    Question = "Какая птица {1} {0}?",
                    Answers = new()
                    {
                        ["Auklet"] = "Auklet",
                        ["Bluebird"] = "Bluebird",
                        ["Chickadee"] = "Chickadee",
                        ["Dove"] = "Dove",
                        ["Egret"] = "Egret",
                        ["Finch"] = "Finch",
                        ["Godwit"] = "Godwit",
                        ["Hummingbird"] = "Hummingbird",
                        ["Ibis"] = "Ibis",
                        ["Jay"] = "Jay",
                        ["Kinglet"] = "Kinglet",
                        ["Loon"] = "Loon",
                        ["Magpie"] = "Magpie",
                        ["Nuthatch"] = "Nuthatch",
                        ["Oriole"] = "Oriole",
                        ["Pipit"] = "Pipit",
                        ["Quail"] = "Quail",
                        ["Raven"] = "Raven",
                        ["Shrike"] = "Shrike",
                        ["Thrush"] = "Thrush",
                        ["Umbrellabird"] = "Umbrellabird",
                        ["Vireo"] = "Vireo",
                        ["Warbler"] = "Warbler",
                        ["Xantus’s Hummingbird"] = "Xantus’s Hummingbird",
                        ["Yellowlegs"] = "Yellowlegs",
                        ["Zigzag Heron"] = "Zigzag Heron",
                    },
                    Arguments = new()
                    {
                        ["was"] = "присутствовала",
                        ["was not"] = "отсутствовала",
                    },
                },
            },
        },

        [typeof(SChess)] = new()
        {
            ModuleName = "Шахматах",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SChess.Coordinate] = new()
                {
                    // English: What was the {1} coordinate in {0}?
                    // Example: What was the first coordinate in Chess?
                    Question = "Какие были {1}-е координаты {0}?",
                },
            },
        },

        [typeof(SChineseCounting)] = new()
        {
            Questions = new()
            {
                [SChineseCounting.LED] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the left LED in Chinese Counting?
                    Question = "Какой был цвет {1} светодиода {0}?",
                    Answers = new()
                    {
                        ["White"] = "Белый",
                        ["Red"] = "Красный",
                        ["Green"] = "Зелёный",
                        ["Orange"] = "Оранжевый",
                    },
                    Arguments = new()
                    {
                        ["left"] = "левого",
                        ["right"] = "правого",
                    },
                },
            },
        },

        [typeof(SChineseRemainderTheorem)] = new()
        {
            Questions = new()
            {
                [SChineseRemainderTheorem.Equations] = new()
                {
                    // English: Which equation was used in {0}?
                    Question = "Какое уравнение было использовано {0}?",
                },
            },
        },

        [typeof(SChordQualities)] = new()
        {
            ModuleName = "Аккордных ладах",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SChordQualities.Notes] = new()
                {
                    // English: Which note was part of the given chord in {0}?
                    Question = "Какая нота присутствовала в начальном аккорде {0}?",
                },
            },
        },

        [typeof(SClockCounter)] = new()
        {
            Questions = new()
            {
                [SClockCounter.Arrows] = new()
                {
                    // English: Which arrow was shown in {0}?
                    Question = "Какая стрелка была показана {0}?",
                },
            },
        },

        [typeof(SCode)] = new()
        {
            ModuleName = "Коде",
            Questions = new()
            {
                [SCode.DisplayNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Какое было показанное число {0}?",
                },
            },
        },

        [typeof(SCodenames)] = new()
        {
            Questions = new()
            {
                [SCodenames.Answers] = new()
                {
                    // English: Which of these words was submitted in {0}?
                    Question = "Какое из слов было введено {0}?",
                },
            },
        },

        [typeof(SCoffeeBeans)] = new()
        {
            Questions = new()
            {
                [SCoffeeBeans.Movements] = new()
                {
                    // English: What was the {1} movement in {0}?
                    // Example: What was the first movement in Coffee Beans?
                    Question = "Какое было {1}-е движение {0}?",
                    Answers = new()
                    {
                        ["Horizontal"] = "Горизонтальное",
                        ["Vertical"] = "Вертикальное",
                        ["Diagonal"] = "Диагональное",
                        ["Nothing"] = "Никакое",
                    },
                },
            },
        },

        [typeof(SCoffeebucks)] = new()
        {
            Questions = new()
            {
                [SCoffeebucks.Coffee] = new()
                {
                    // English: What was the last served coffee in {0}?
                    Question = "Какое было последне поданное кофе {0}?",
                },
            },
        },

        [typeof(SCoinage)] = new()
        {
            Questions = new()
            {
                [SCoinage.Flip] = new()
                {
                    // English: Which coin was flipped in {0}?
                    Question = "Какая монета была перевёрнута {0}?",
                },
            },
        },

        [typeof(SColorAddition)] = new()
        {
            Questions = new()
            {
                [SColorAddition.Numbers] = new()
                {
                    // English: What was {1}’s number in {0}?
                    // Example: What was red’s number in Color Addition?
                    Question = "Какое было {1} число {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красное",
                        ["green"] = "зелёное",
                        ["blue"] = "синее",
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
                    // English: What color was this dot in {0}? (+ sprite)
                    Question = "Какого цвета была эта точка {0}?",
                    Answers = new()
                    {
                        ["Black"] = "Black",
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Cyan"] = "Cyan",
                        ["Red"] = "Red",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                        ["White"] = "White",
                    },
                },
            },
        },

        [typeof(SColorDecoding)] = new()
        {
            ModuleName = "Расшифровки цветов",
            Conjugation = Conjugation.GenitiveFeminine,
            Questions = new()
            {
                [SColorDecoding.IndicatorColors] = new()
                {
                    // English: Which color {1} in the {2}-stage indicator pattern in {0}?
                    // Example: Which color appeared in the first-stage indicator pattern in Color Decoding?
                    Question = "Какой цвет {1} на узоре индикатора на {2}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Green"] = "Зелёный",
                        ["Purple"] = "Фиолетовый",
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                        ["Yellow"] = "Жёлтый",
                    },
                    Arguments = new()
                    {
                        ["appeared"] = "присутствовал",
                        ["did not appear"] = "отсутствовал",
                    },
                },
                [SColorDecoding.IndicatorPattern] = new()
                {
                    // English: What was the {1}-stage indicator pattern in {0}?
                    // Example: What was the first-stage indicator pattern in Color Decoding?
                    Question = "Какой был узор индикатора на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Checkered"] = "Шахматный",
                        ["Horizontal"] = "Горизонтальный",
                        ["Vertical"] = "Вертикальный",
                        ["Solid"] = "Сплошной",
                    },
                },
            },
        },

        [typeof(SColoredKeys)] = new()
        {
            ModuleName = "Цветных кнопках",
            Conjugation = Conjugation.PrepositivePlural,
            Questions = new()
            {
                [SColoredKeys.DisplayWord] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "Какое слово было отображено на дисплее на {0}?",
                    Answers = new()
                    {
                        ["red"] = "Red",
                        ["blue"] = "Blue",
                        ["green"] = "Green",
                        ["yellow"] = "Yellow",
                        ["purple"] = "Purple",
                        ["white"] = "White",
                    },
                },
                [SColoredKeys.DisplayWordColor] = new()
                {
                    // English: What was the displayed word’s color in {0}?
                    Question = "Какого цвета было отображённое слово {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["blue"] = "Синего",
                        ["green"] = "Зелёного",
                        ["yellow"] = "Жёлтого",
                        ["purple"] = "Фиолетового",
                        ["white"] = "Белого",
                    },
                },
                [SColoredKeys.KeyLetter] = new()
                {
                    // English: What letter was on the {1} key in {0}?
                    // Example: What letter was on the top-left key in Colored Keys?
                    Question = "Какая буква была на {1} кнопке на {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "верхней левой",
                        ["top-right"] = "верхней правой",
                        ["bottom-left"] = "нижней левой",
                        ["bottom-right"] = "нижней правой",
                    },
                },
                [SColoredKeys.KeyColor] = new()
                {
                    // English: What was the color of the {1} key in {0}?
                    // Example: What was the color of the top-left key in Colored Keys?
                    Question = "Какого цвета была {1} кнопка {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["blue"] = "Синего",
                        ["green"] = "Зелёного",
                        ["yellow"] = "Жёлтого",
                        ["purple"] = "Фиолетового",
                        ["white"] = "Белого",
                    },
                    Arguments = new()
                    {
                        ["top-left"] = "верхняя левая",
                        ["top-right"] = "верхняя правая",
                        ["bottom-left"] = "нижняя левая",
                        ["bottom-right"] = "нижняя правая",
                    },
                },
            },
        },

        [typeof(SColoredSquares)] = new()
        {
            ModuleName = "Цветных квадратах",
            Conjugation = Conjugation.PrepositivePlural,
            Questions = new()
            {
                [SColoredSquares.FirstGroup] = new()
                {
                    // English: What was the first color group in {0}?
                    Question = "Какого цвета была первая группа на {0}?",
                    Answers = new()
                    {
                        ["White"] = "Белая",
                        ["Red"] = "Красная",
                        ["Blue"] = "Синяя",
                        ["Green"] = "Зелёная",
                        ["Yellow"] = "Жёлтая",
                        ["Magenta"] = "Розовая",
                    },
                },
            },
        },

        [typeof(SColoredSwitches)] = new()
        {
            ModuleName = "Цветных переключателей",
            Conjugation = Conjugation.GenitivePlural,
            Questions = new()
            {
                [SColoredSwitches.InitialPosition] = new()
                {
                    // English: What was the initial position of the switches in {0}?
                    Question = "Какое было начальное положение {0}?",
                },
                [SColoredSwitches.WhenLEDsCameOn] = new()
                {
                    // English: What was the position of the switches when the LEDs came on in {0}?
                    Question = "Какое было положение у {0}, когда загорелись светодиоды?",
                },
            },
        },

        [typeof(SColorMorse)] = new()
        {
            ModuleName = "Цветной азбуке Морзе",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SColorMorse.Color] = new()
                {
                    // English: What was the color of the {1} LED in {0}?
                    // Example: What was the color of the first LED in Color Morse?
                    Question = "Какой был цвет {1}-го светодиода {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Синий",
                        ["Green"] = "Зелёный",
                        ["Orange"] = "Оранжевый",
                        ["Purple"] = "Фиолетовый",
                        ["Red"] = "Красный",
                        ["Yellow"] = "Жёлтый",
                        ["White"] = "Белый",
                    },
                },
                [SColorMorse.Character] = new()
                {
                    // English: What character was flashed by the {1} LED in {0}?
                    // Example: What character was flashed by the first LED in Color Morse?
                    Question = "Какой символ передавался через Морзе {1}-м светодиодом {0}?",
                },
            },
        },

        [typeof(SColorOneTwo)] = new()
        {
            ModuleName = "\"Цвет раз два\"",
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SColorOneTwo.Color] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the left LED in Color One Two?
                    Question = "Какого цвета был {1} светодиод на {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                        ["Green"] = "Зелёный",
                        ["Yellow"] = "Жёлтый",
                    },
                    Arguments = new()
                    {
                        ["left"] = "левый",
                        ["right"] = "правый",
                    },
                },
            },
        },

        [typeof(SColorsMaximization)] = new()
        {
            Questions = new()
            {
                [SColorsMaximization.ColorCount] = new()
                {
                    // English: How many buttons were {1} in {0}?
                    // Example: How many buttons were red in Colors Maximization?
                    Question = "Сколько было {1} кнопок {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красных",
                        ["green"] = "зелёных",
                        ["blue"] = "синих",
                    },
                },
            },
        },

        [typeof(SColouredCubes)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SColouredCubes.Colours] = new()
                {
                    // English: What was the colour of this {1} in the {2} stage of {0}? (+ sprite)
                    // Example: What was the colour of this cube in the first stage of Coloured Cubes? (+ sprite)
                    Question = "Какой был цвет данного {1} на {2}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Black"] = "Black",
                        ["Indigo"] = "Indigo",
                        ["Blue"] = "Blue",
                        ["Forest"] = "Forest",
                        ["Teal"] = "Teal",
                        ["Azure"] = "Azure",
                        ["Green"] = "Green",
                        ["Jade"] = "Jade",
                        ["Cyan"] = "Cyan",
                        ["Maroon"] = "Maroon",
                        ["Plum"] = "Plum",
                        ["Violet"] = "Violet",
                        ["Olive"] = "Olive",
                        ["Grey"] = "Grey",
                        ["Maya"] = "Maya",
                        ["Lime"] = "Lime",
                        ["Mint"] = "Mint",
                        ["Aqua"] = "Aqua",
                        ["Red"] = "Red",
                        ["Rose"] = "Rose",
                        ["Magenta"] = "Magenta",
                        ["Orange"] = "Orange",
                        ["Salmon"] = "Salmon",
                        ["Pink"] = "Pink",
                        ["Yellow"] = "Yellow",
                        ["Cream"] = "Cream",
                        ["White"] = "White",
                    },
                    Arguments = new()
                    {
                        ["cube"] = "куба",
                        ["stage light"] = "индикатора этапа",
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
                    Question = "Какой цвет мигал {1}-м на цилиндре {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                        ["Magenta"] = "Magenta",
                        ["White"] = "White",
                        ["Black"] = "Black",
                    },
                },
            },
        },

        [typeof(SColourFlash)] = new()
        {
            ModuleName = "Цветной вспышки",
            Conjugation = Conjugation.GenitiveFeminine,
            Questions = new()
            {
                [SColourFlash.LastColor] = new()
                {
                    // English: What was the color of the last word in the sequence in {0}?
                    Question = "Какого цвета было последнее слово в последовательности {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Magenta"] = "Розовый",
                        ["White"] = "Белый",
                    },
                },
            },
        },

        [typeof(SConcentration)] = new()
        {
            Questions = new()
            {
                [SConcentration.StartingDigit] = new()
                {
                    // English: What number began here in {0}? (+ sprite)
                    Question = "Какое число было здесь изначально {0}?",
                },
            },
            Discriminators = new()
            {
                [SConcentration.Discriminator] = new()
                {
                    // English: the Concentration which began with {1} in the {0} position (in reading order)
                    // Example: the Concentration which began with 1 in the first position (in reading order)
                    Discriminator = "в Concentration, где \"{1}\" изначально было в {0}-й позиции (в порядке чтения)",
                },
            },
        },

        [typeof(SConditionalButtons)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SConditionalButtons.Colors] = new()
                {
                    // English: What was the color of this button in {0}? (+ sprite)
                    Question = "Какого цвета была эта кнопка на {0}?",
                    Answers = new()
                    {
                        ["black"] = "Чёрного",
                        ["blue"] = "Синего",
                        ["dark green"] = "Тёмно-зелёного",
                        ["light green"] = "Светло-зелёного",
                        ["orange"] = "Оранжевого",
                        ["pink"] = "Розового",
                        ["purple"] = "Фиолетового",
                        ["red"] = "Красного",
                        ["white"] = "Белого",
                        ["yellow"] = "Жёлтого",
                    },
                },
            },
        },

        [typeof(SConnectedMonitors)] = new()
        {
            Questions = new()
            {
                [SConnectedMonitors.Number] = new()
                {
                    // English: What number was initially displayed on this screen in {0}? (+ sprite)
                    Question = "Какое число было изначально отображено на данном экране {0}?",
                },
                [SConnectedMonitors.SingleIndicator] = new()
                {
                    // English: What colour was the indicator on this screen in {0}? (+ sprite)
                    Question = "Какого цвета был индикатор на данном экране {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Purple"] = "Фиолетовый",
                        ["White"] = "Белый",
                    },
                },
                [SConnectedMonitors.OrdinalIndicator] = new()
                {
                    // English: What colour was the {1} indicator on this screen in {0}? (+ sprite)
                    // Example: What colour was the first indicator on this screen in Connected Monitors? (+ sprite)
                    Question = "Какого цвета был {1}-й индикатор на данном экране {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Purple"] = "Фиолетовый",
                        ["White"] = "Белый",
                    },
                },
            },
        },

        [typeof(SConnectionCheck)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Проверке соединения",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SConnectionCheck.Numbers] = new()
                {
                    // English: What pair of numbers was present in {0}?
                    Question = "Какая пара чисел присутствовала {0}?",
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
            ModuleName = "Координатах",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SCoordinates.FirstSolution] = new()
                {
                    // English: What was the solution you selected first in {0}?
                    Question = "Какую координату вы выбрали первой {0}?",
                },
                [SCoordinates.Size] = new()
                {
                    // English: What was the grid size in {0}?
                    Question = "В каком формате был указан размер сетки {0}?",
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
            Questions = new()
            {
                [SCoralCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Coral Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SCorners)] = new()
        {
            ModuleName = "Углах",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SCorners.Colors] = new()
                {
                    // English: What was the color of the {1} corner in {0}?
                    // Example: What was the color of the top-left corner in Corners?
                    Question = "Какого цвета был {1} угол {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["green"] = "Зелёного",
                        ["blue"] = "Синего",
                        ["yellow"] = "Жёлтого",
                    },
                    Arguments = new()
                    {
                        ["top-left"] = "верхний левый",
                        ["top-right"] = "верхний правый",
                        ["bottom-right"] = "нижний правый",
                        ["bottom-left"] = "нижний левый",
                    },
                },
                [SCorners.ColorCount] = new()
                {
                    // English: How many corners in {0} were {1}?
                    // Example: How many corners in Corners were red?
                    Question = "Сколько было {1} углов {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красных",
                        ["green"] = "зелёных",
                        ["blue"] = "синих",
                        ["yellow"] = "жёлтых",
                    },
                },
            },
        },

        [typeof(SCornflowerCipher)] = new()
        {
            Questions = new()
            {
                [SCornflowerCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Cornflower Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SCosmic)] = new()
        {
            Questions = new()
            {
                [SCosmic.Number] = new()
                {
                    // English: What was the number initially shown in {0}?
                    Question = "Какое число было изначально показано {0}?",
                },
            },
        },

        [typeof(SCrazyHamburger)] = new()
        {
            Questions = new()
            {
                [SCrazyHamburger.Ingredient] = new()
                {
                    // English: What was the {1} ingredient shown in {0}?
                    // Example: What was the first ingredient shown in Crazy Hamburger?
                    Question = "Какой был {1}-й показанный ингредиент {0}?",
                },
            },
        },

        [typeof(SCrazyMaze)] = new()
        {
            Questions = new()
            {
                [SCrazyMaze.StartOrGoal] = new()
                {
                    // English: What was the {1} location in {0}?
                    // Example: What was the starting location in Crazy Maze?
                    Question = "Какая была {1} позиция {0}?",
                    Arguments = new()
                    {
                        ["starting"] = "начальная",
                        ["goal"] = "конечная",
                    },
                },
            },
        },

        [typeof(SCreamCipher)] = new()
        {
            Questions = new()
            {
                [SCreamCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Cream Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SCreation)] = new()
        {
            ModuleName = "Творения",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SCreation.Weather] = new()
                {
                    // English: What were the weather conditions on the {1} day in {0}?
                    // Example: What were the weather conditions on the first day in Creation?
                    Question = "Какая погода была на {1}-м дне {0}?",
                    Answers = new()
                    {
                        ["Clear"] = "Ясно",
                        ["Heat Wave"] = "Жара",
                        ["Meteor Shower"] = "Метеор. дождь",
                        ["Rain"] = "Дождь",
                        ["Windy"] = "Ветер",
                    },
                },
            },
        },

        [typeof(SCrimsonCipher)] = new()
        {
            Questions = new()
            {
                [SCrimsonCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Crimson Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SCritters)] = new()
        {
            Questions = new()
            {
                [SCritters.Color] = new()
                {
                    // English: What was the color in {0}?
                    Question = "Какой цвет использовался {0}?",
                    Answers = new()
                    {
                        ["Yellow"] = "Жёлтый",
                        ["Pink"] = "Розовый",
                        ["Blue"] = "Синий",
                        ["White"] = "Белый",
                    },
                },
            },
        },

        [typeof(SCruelBinary)] = new()
        {
            Questions = new()
            {
                [SCruelBinary.DisplayedWord] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "Какое слово было показано {0}?",
                },
            },
        },

        [typeof(SCruelKeypads)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SCruelKeypads.Colors] = new()
                {
                    // English: What was the color of the bar in the {1} stage of {0}?
                    // Example: What was the color of the bar in the first stage of Cruel Keypads?
                    Question = "Какого цвета была шкала на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Magenta"] = "Magenta",
                        ["White"] = "White",
                    },
                },
                [SCruelKeypads.DisplayedSymbols] = new()
                {
                    // English: Which of these characters appeared in the {1} stage of {0}?
                    // Example: Which of these characters appeared in the first stage of Cruel Keypads?
                    Question = "Какой из этих символов появился на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SCRule)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SCRule.SymbolPair] = new()
                {
                    // English: Which symbol pair was here in {0}? (+ sprite)
                    Question = "Какая пара символов была здесь {0}?",
                },
                [SCRule.SymbolPairCell] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was ♤♤ in cRule?
                    Question = "Где находилось {1} {0}?",
                },
                [SCRule.SymbolPairPresent] = new()
                {
                    // English: Which symbol pair was present on {0}?
                    Question = "Какая пара символов присутствовала {0}?",
                },
                [SCRule.Prefilled] = new()
                {
                    // English: Which cell was pre-filled at the start of {0}?
                    Question = "Какая клетка была уже заполнена в начале {0}?",
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
            Questions = new()
            {
                [SCrypticKeypad.Labels] = new()
                {
                    // English: What was the label of the {1} key in {0}?
                    // Example: What was the label of the top-left key in Cryptic Keypad?
                    Question = "Какой был символ на {1} кнопке {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "верхней левой",
                        ["top-right"] = "верхней правой",
                        ["bottom-left"] = "нижней левой",
                        ["bottom-right"] = "нижней правой",
                    },
                },
                [SCrypticKeypad.Rotations] = new()
                {
                    // English: Which cardinal direction was the {1} key rotated to in {0}?
                    // Example: Which cardinal direction was the top-left key rotated to in Cryptic Keypad?
                    Question = "В какую сторону света была повёрнута {1} кнопка {0}?",
                    Answers = new()
                    {
                        ["North"] = "Север",
                        ["East"] = "Восток",
                        ["South"] = "Юг",
                        ["West"] = "Запад",
                    },
                    Arguments = new()
                    {
                        ["top-left"] = "верхняя левая",
                        ["top-right"] = "верхняя правая",
                        ["bottom-left"] = "нижняя левая",
                        ["bottom-right"] = "нижняя правая",
                    },
                },
            },
        },

        [typeof(SCube)] = new()
        {
            ModuleName = "Куба",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SCube.Rotations] = new()
                {
                    // English: What was the {1} cube rotation in {0}?
                    // Example: What was the first cube rotation in Cube?
                    Question = "Какое было {1}-е вращение у {0}?",
                    Answers = new()
                    {
                        ["rotate cw"] = "поворот по часовой",
                        ["tip left"] = "наклон влево",
                        ["tip backwards"] = "наклон назад",
                        ["rotate ccw"] = "поворот против часовой",
                        ["tip right"] = "наклон вправо",
                        ["tip forwards"] = "наклон вперёд",
                    },
                },
            },
        },

        [typeof(SCursedDoubleOh)] = new()
        {
            Questions = new()
            {
                [SCursedDoubleOh.InitialPosition] = new()
                {
                    // English: What was the first digit of the initially displayed number in {0}?
                    Question = "Какая была первая цифра изначально отображённого числа {0}?",
                },
            },
        },

        [typeof(SCustomerIdentification)] = new()
        {
            Questions = new()
            {
                [SCustomerIdentification.Customer] = new()
                {
                    // English: Who was the {1} customer in {0}?
                    // Example: Who was the first customer in Customer Identification?
                    Question = "Кто был {1}-м посетителем {0}?",
                },
            },
        },

        [typeof(SCyanButton)] = new()
        {
            Conjugation = Conjugation.NominativeMasculine,
            Questions = new()
            {
                [SCyanButton.Positions] = new()
                {
                    // English: Where was the button at the {1} stage in {0}?
                    // Example: Where was the button at the first stage in Cyan Button?
                    Question = "Где был {0} на своём {1}-м этапе?",
                    Answers = new()
                    {
                        ["top left"] = "Сверху слева",
                        ["top middle"] = "Сверху посередине",
                        ["top right"] = "Сверху справа",
                        ["bottom left"] = "Снизу слева",
                        ["bottom middle"] = "Снизу посередине",
                        ["bottom right"] = "Снизу справа",
                    },
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
                    Question = "Откуда вы отправились {0}?",
                    Answers = new()
                    {
                        ["Burgenland, A"] = "Burgenland, A",
                        ["Carinthia, A"] = "Carinthia, A",
                        ["Lower Austria, A"] = "Lower Austria, A",
                        ["North Tyrol, A"] = "North Tyrol, A",
                        ["Upper Austria, A"] = "Upper Austria, A",
                        ["East Tyrol, A"] = "East Tyrol, A",
                        ["Salzburg, A"] = "Salzburg, A",
                        ["Styria, A"] = "Styria, A",
                        ["Vorarlberg, A"] = "Vorarlberg, A",
                        ["Vienna, A"] = "Vienna, A",
                        ["Aargau, CH"] = "Aargau, CH",
                        ["Appenzell Inner Rhodes, CH"] = "Appenzell Inner Rhodes, CH",
                        ["Appenzell Outer Rhodes, CH"] = "Appenzell Outer Rhodes, CH",
                        ["Basel Country, CH"] = "Basel Country, CH",
                        ["Bern, CH"] = "Bern, CH",
                        ["Basel City, CH"] = "Basel City, CH",
                        ["Fribourg, CH"] = "Fribourg, CH",
                        ["Geneva, CH"] = "Geneva, CH",
                        ["Glarus, CH"] = "Glarus, CH",
                        ["Grisons, CH"] = "Grisons, CH",
                        ["Jura, CH"] = "Jura, CH",
                        ["Luzern, CH"] = "Luzern, CH",
                        ["Nidwalden, CH"] = "Nidwalden, CH",
                        ["Neuchâtel, CH"] = "Neuchâtel, CH",
                        ["Obwalden, CH"] = "Obwalden, CH",
                        ["Schaffhausen, CH"] = "Schaffhausen, CH",
                        ["St. Gallen, CH"] = "St. Gallen, CH",
                        ["Solothurn, CH"] = "Solothurn, CH",
                        ["Schwyz, CH"] = "Schwyz, CH",
                        ["Thurgau, CH"] = "Thurgau, CH",
                        ["Ticino, CH"] = "Ticino, CH",
                        ["Uri, CH"] = "Uri, CH",
                        ["Vaud, CH"] = "Vaud, CH",
                        ["Valais, CH"] = "Valais, CH",
                        ["Zug, CH"] = "Zug, CH",
                        ["Zürich, CH"] = "Zürich, CH",
                        ["Brandenburg, D"] = "Brandenburg, D",
                        ["Berlin, D"] = "Berlin, D",
                        ["Baden-Württemberg, D"] = "Baden-Württemberg, D",
                        ["Bavaria, D"] = "Bavaria, D",
                        ["Bremen, D"] = "Bremen, D",
                        ["Hesse, D"] = "Hesse, D",
                        ["Hamburg, D"] = "Hamburg, D",
                        ["Mecklenburg-Vorpommern, D"] = "Mecklenburg-Vorpommern, D",
                        ["Lower Saxony, D"] = "Lower Saxony, D",
                        ["North Rhine-Westphalia, D"] = "North Rhine-Westphalia, D",
                        ["Rhineland-Palatinate, D"] = "Rhineland-Palatinate, D",
                        ["Schleswig-Holstein, D"] = "Schleswig-Holstein, D",
                        ["Saarland, D"] = "Saarland, D",
                        ["Saxony, D"] = "Saxony, D",
                        ["Saxony-Anhalt, D"] = "Saxony-Anhalt, D",
                        ["Thuringia, D"] = "Thuringia, D",
                        ["Liechtenstein"] = "Liechtenstein",
                    },
                },
            },
        },

        [typeof(SDeafAlley)] = new()
        {
            Questions = new()
            {
                [SDeafAlley.Shape] = new()
                {
                    // English: What was the shape generated in {0}?
                    Question = "Какой символ был сгенерирован {0}?",
                },
            },
        },

        [typeof(SDeckOfManyThings)] = new()
        {
            Questions = new()
            {
                [SDeckOfManyThings.FirstCard] = new()
                {
                    // English: What deck did the first card of {0} belong to?
                    Question = "Какой колоде принадлежала первая карта {0}?",
                },
            },
        },

        [typeof(SDecoloredSquares)] = new()
        {
            ModuleName = "Обесцвеченных квадратах",
            Conjugation = Conjugation.PrepositivePlural,
            Questions = new()
            {
                [SDecoloredSquares.StartingPos] = new()
                {
                    // English: What was the starting {1} defining color in {0}?
                    // Example: What was the starting column defining color in Decolored Squares?
                    Question = "Какой цвет определил {1} схемы на {0}?",
                    Answers = new()
                    {
                        ["White"] = "Белый",
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                        ["Green"] = "Зелёный",
                        ["Yellow"] = "Жёлтый",
                        ["Magenta"] = "Розовый",
                    },
                    Arguments = new()
                    {
                        ["column"] = "начальный столбец",
                        ["row"] = "начальную строку",
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
                    Question = "{1} у {2}-й цели {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Red"] = "Red",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                        ["White"] = "White",
                    },
                    Arguments = new()
                    {
                        ["colour"] = "Какой был цвет",
                        ["word"] = "Какое было слово",
                    },
                },
            },
        },

        [typeof(SDenialDisplays)] = new()
        {
            Questions = new()
            {
                [SDenialDisplays.Displays] = new()
                {
                    // English: What number was initially shown on display {1} in {0}?
                    // Example: What number was initially shown on display A in Denial Displays?
                    Question = "Какое число было показано на экране {1} {0}?",
                },
            },
        },

        [typeof(SDetoNATO)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SDetoNATO.Display] = new()
                {
                    // English: What was the {1} display in {0}?
                    // Example: What was the first display in DetoNATO?
                    Question = "Что было на дисплее на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SDevilishEggs)] = new()
        {
            Questions = new()
            {
                [SDevilishEggs.Rotations] = new()
                {
                    // English: What was the {1} egg’s {2} rotation in {0}?
                    // Example: What was the top egg’s first rotation in Devilish Eggs?
                    Question = "Какой был {2}-й поворот у {1} яйца {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнего",
                        ["bottom"] = "нижнего",
                    },
                },
                [SDevilishEggs.Numbers] = new()
                {
                    // English: What was the {1} digit in the string of numbers on {0}?
                    // Example: What was the first digit in the string of numbers on Devilish Eggs?
                    Question = "Какая была {1}-я цифра в строке чисел на {0}?",
                },
                [SDevilishEggs.Letters] = new()
                {
                    // English: What was the {1} letter in the string of letters on {0}?
                    // Example: What was the first letter in the string of letters on Devilish Eggs?
                    Question = "Какая была {1}-я буква в строке букв на {0}?",
                },
            },
        },

        [typeof(SDialtones)] = new()
        {
            Questions = new()
            {
                [SDialtones.Dialtones] = new()
                {
                    // English: What dialtones were heard in {0}?
                    Question = "Какие тональные сигналы играли {0}?",
                },
            },
        },

        [typeof(SDigisibility)] = new()
        {
            Questions = new()
            {
                [SDigisibility.DisplayedNumber] = new()
                {
                    // English: What was the number on the {1} button in {0}?
                    // Example: What was the number on the first button in Digisibility?
                    Question = "Какое число было на {1}-й кнопке {0}?",
                },
            },
        },

        [typeof(SDigitString)] = new()
        {
            Questions = new()
            {
                [SDigitString.InitialNumber] = new()
                {
                    // English: What was the initial number in {0}?
                    Question = "Какое было исходное число {0}?",
                },
            },
        },

        [typeof(SDimensionDisruption)] = new()
        {
            Questions = new()
            {
                [SDimensionDisruption.VisibleLetters] = new()
                {
                    // English: Which of these was a visible character in {0}?
                    Question = "Что из этого было видимым символом {0}?",
                },
            },
        },

        [typeof(SDirectionalButton)] = new()
        {
            ModuleName = "Направляющей кнопки",
            Conjugation = Conjugation.GenitiveFeminine,
            Questions = new()
            {
                [SDirectionalButton.ButtonCount] = new()
                {
                    // English: How many times did you press the button in the {1} stage of {0}?
                    // Example: How many times did you press the button in the first stage of Directional Button?
                    Question = "Сколько раз вы нажали кнопку на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SDiscoloredSquares)] = new()
        {
            ModuleName = "Бесцветных квадратах",
            Conjugation = Conjugation.PrepositivePlural,
            Questions = new()
            {
                [SDiscoloredSquares.RememberedPositions] = new()
                {
                    // English: What was {1}’s remembered position in {0}?
                    // Example: What was Blue’s remembered position in Discolored Squares?
                    Question = "В какой позиции находился {1} квадрат в самом начале на {0}?",
                    Arguments = new()
                    {
                        ["Blue"] = "синий",
                        ["Red"] = "красный",
                        ["Yellow"] = "жёлтый",
                        ["Green"] = "зелёный",
                        ["Magenta"] = "розовый",
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
                    Question = "Какой информации недоставало {1}-й клавише {0}?",
                    Answers = new()
                    {
                        ["Key color"] = "Цвета клавиши",
                        ["Label color"] = "Цвета надписи",
                        ["Label"] = "Надписи",
                    },
                },
                [SDisorderedKeys.UnrevealedKeyColor] = new()
                {
                    // English: What was the unrevealed key color for the {1} key in {0}?
                    // Example: What was the unrevealed key color for the first key in Disordered Keys?
                    Question = "Каким был нераскрытый цвет {1}-й клавиши {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                    },
                },
                [SDisorderedKeys.UnrevealedLabelColor] = new()
                {
                    // English: What was the unrevealed label color for the {1} key in {0}?
                    // Example: What was the unrevealed label color for the first key in Disordered Keys?
                    Question = "Каким был нераскрытый цвет надписи {1}-й клавиши {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                    },
                },
                [SDisorderedKeys.UnrevealedKeyLabel] = new()
                {
                    // English: What was the unrevealed label for the {1} key in {0}?
                    // Example: What was the unrevealed label for the first key in Disordered Keys?
                    Question = "Какая была нераскрытая надпись {1}-й клавиши {0}?",
                },
                [SDisorderedKeys.RevealedKeyColor] = new()
                {
                    // English: What was the revealed key color for the {1} key in {0}?
                    // Example: What was the revealed key color for the first key in Disordered Keys?
                    Question = "Каким был раскрытый цвет {1}-й клавиши {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                    },
                },
                [SDisorderedKeys.RevealedLabelColor] = new()
                {
                    // English: What was the revealed label color for the {1} key in {0}?
                    // Example: What was the revealed label color for the first key in Disordered Keys?
                    Question = "Каким был раскрытый цвет надписи {1}-й клавиши {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                    },
                },
                [SDisorderedKeys.RevealedLabel] = new()
                {
                    // English: What was the revealed label for the {1} key in {0}?
                    // Example: What was the revealed label for the first key in Disordered Keys?
                    Question = "Какая была раскрытая надпись {1}-й клавиши {0}?",
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Black"] = "Black",
                        ["White"] = "White",
                    },
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
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SDivisibleNumbers.Numbers] = new()
                {
                    // English: What was the {1} stage’s number in {0}?
                    // Example: What was the first stage’s number in Divisible Numbers?
                    Question = "Какое было число {1}-го этапа {0}?",
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
            ModuleName = "Двойных стрелках",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SDoubleArrows.Start] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "Какая была начальная позиция {0}?",
                },
                [SDoubleArrows.Movement] = new()
                {
                    // English: Which direction in the grid did the {1} arrow move in {0}?
                    // Example: Which direction in the grid did the inner up arrow move in Double Arrows?
                    Question = "В какую сторону вас переместила стрелка {1} {0}?",
                    Answers = new()
                    {
                        ["Up"] = "Вверх",
                        ["Right"] = "Вправо",
                        ["Left"] = "Влево",
                        ["Down"] = "Вниз",
                    },
                    Arguments = new()
                    {
                        ["inner up"] = "внутри вверх",
                        ["inner down"] = "внутри вниз",
                        ["inner left"] = "внутри влево",
                        ["inner right"] = "внутри вправо",
                        ["outer up"] = "снаружи вверх",
                        ["outer down"] = "снаружи вниз",
                        ["outer left"] = "снаружи влево",
                        ["outer right"] = "снаружи вправо",
                    },
                },
                [SDoubleArrows.Arrow] = new()
                {
                    // English: Which {1} arrow moved {2} in the grid in {0}?
                    // Example: Which inner arrow moved up in the grid in Double Arrows?
                    Question = "Которая стрелка {1} переместила вас {2} {0}?",
                    Answers = new()
                    {
                        ["Up"] = "Вверх",
                        ["Right"] = "Вправо",
                        ["Left"] = "Влево",
                        ["Down"] = "Вниз",
                    },
                    Arguments = new()
                    {
                        ["inner"] = "внутри",
                        ["up"] = "вверх",
                        ["outer"] = "снаружи",
                        ["down"] = "вниз",
                        ["left"] = "влево",
                        ["right"] = "вправо",
                    },
                },
            },
        },

        [typeof(SDoubleColor)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SDoubleColor.Colors] = new()
                {
                    // English: What was the screen color on the {1} stage of {0}?
                    // Example: What was the screen color on the first stage of Double Color?
                    Question = "Какого цвета был экран на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Green"] = "Зелёного",
                        ["Blue"] = "Синего",
                        ["Red"] = "Красного",
                        ["Pink"] = "Розового",
                        ["Yellow"] = "Жёлтого",
                    },
                },
            },
        },

        [typeof(SDoubleDigits)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SDoubleDigits.Displays] = new()
                {
                    // English: What was the digit on the {1} display in {0}?
                    // Example: What was the digit on the left display in Double Digits?
                    Question = "Какая цифра была на {1} дисплее {0}?",
                    Arguments = new()
                    {
                        ["left"] = "левом",
                        ["right"] = "правом",
                    },
                },
            },
        },

        [typeof(SDoubleExpert)] = new()
        {
            Questions = new()
            {
                [SDoubleExpert.StartingKeyNumber] = new()
                {
                    // English: What was the starting key number in {0}?
                    Question = "Какое было начальное ключевое число {0}?",
                },
                [SDoubleExpert.SubmittedWord] = new()
                {
                    // English: What was the word you submitted in {0}?
                    Question = "Какое было отправленное слово {0}?",
                },
            },
        },

        [typeof(SDoubleListening)] = new()
        {
            Questions = new()
            {
                [SDoubleListening.Sounds] = new()
                {
                    // English: What clip was played in {0}?
                    Question = "Какой звук был воспроизведён {0}?",
                },
            },
        },

        [typeof(SDoubleOh)] = new()
        {
            ModuleName = "Агент Ноль-ноль",
            Questions = new()
            {
                [SDoubleOh.SubmitButton] = new()
                {
                    // English: Which button was the submit button in {0}?
                    Question = "Какая кнопка была кнопкой отправки {0}?",
                },
            },
        },

        [typeof(SDoubleScreen)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SDoubleScreen.Colors] = new()
                {
                    // English: What color was the {1} screen in the {2} stage of {0}?
                    // Example: What color was the top screen in the first stage of Double Screen?
                    Question = "Какого цвета был {1} экран на {2}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                    },
                    Arguments = new()
                    {
                        ["top"] = "верхний",
                        ["bottom"] = "нижний",
                    },
                },
            },
        },

        [typeof(SDrDoctor)] = new()
        {
            Questions = new()
            {
                [SDrDoctor.Diseases] = new()
                {
                    // English: Which of these diseases was listed on {0}, but not the one treated?
                    Question = "Какая из этих болезней присутствовала на {0}, но не была вылечена?",
                },
                [SDrDoctor.Symptoms] = new()
                {
                    // English: Which of these symptoms was listed on {0}?
                    Question = "Какой из этих симптомов присутствовал {0}?",
                },
            },
        },

        [typeof(SDreamcipher)] = new()
        {
            Questions = new()
            {
                [SDreamcipher.Word] = new()
                {
                    // English: What was the decrypted word in {0}?
                    Question = "Какое было расшифрованное слово {0}?",
                },
            },
        },

        [typeof(SDuck)] = new()
        {
            Questions = new()
            {
                [SDuck.CurtainColor] = new()
                {
                    // English: What was the color of the curtain in {0}?
                    Question = "Какого цвета был занавески {0}?",
                    Answers = new()
                    {
                        ["blue"] = "синий",
                        ["yellow"] = "жёлтый",
                        ["green"] = "зелёный",
                        ["orange"] = "оранжевый",
                        ["red"] = "красный",
                    },
                },
            },
        },

        [typeof(SDumbWaiters)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SDumbWaiters.PlayerAvailable] = new()
                {
                    // English: Which player {1} present in {0}?
                    // Example: Which player was present in Dumb Waiters?
                    Question = "Какой игрок {1} на {0}?",
                    Arguments = new()
                    {
                        ["was"] = "присутствовал",
                        ["was not"] = "отсутствовал",
                    },
                },
            },
        },

        [typeof(SEarthbound)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SEarthbound.Background] = new()
                {
                    // English: What was the background in {0}?
                    Question = "Какой был фон на {0}?",
                },
                [SEarthbound.Monster] = new()
                {
                    // English: Which monster was displayed in {0}?
                    Question = "Какой монстр был показан на {0}?",
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
            Questions = new()
            {
                [SEight.LastSmallDisplayDigit] = new()
                {
                    // English: What was the last digit on the small display in {0}?
                    Question = "Какая была последняя цифра на малом экране {0}?",
                },
                [SEight.LastBrokenDigitPosition] = new()
                {
                    // English: What was the position of the last broken digit in {0}?
                    Question = "Какая была позиция последней сломанной цифры {0}?",
                },
                [SEight.LastResultingDigits] = new()
                {
                    // English: What were the last resulting digits in {0}?
                    Question = "Какие были посление цифры перед отправкой {0}?",
                },
                [SEight.LastDisplayedNumber] = new()
                {
                    // English: What was the last displayed number in {0}?
                    Question = "Какие были последние отображённые цифры на {0}?",
                },
            },
        },

        [typeof(SElderFuthark)] = new()
        {
            Questions = new()
            {
                [SElderFuthark.Runes] = new()
                {
                    // English: What was the {1} rune shown on {0}?
                    // Example: What was the first rune shown on Elder Futhark?
                    Question = "Какая была {1}-я показанная руна {0}?",
                },
            },
        },

        [typeof(SEmoji)] = new()
        {
            Questions = new()
            {
                [SEmoji.Emoji] = new()
                {
                    // English: What was the {1} emoji in {0}?
                    // Example: What was the left emoji in Emoji?
                    Question = "Какой был {1} эмодзи {0}?",
                    Arguments = new()
                    {
                        ["left"] = "левый",
                        ["right"] = "правый",
                    },
                },
            },
        },

        [typeof(SEnaCipher)] = new()
        {
            Questions = new()
            {
                [SEnaCipher.KeywordAnswer] = new()
                {
                    // English: What was the {1} keyword in {0}?
                    // Example: What was the first keyword in ƎNA Cipher?
                    Question = "Какое было {1}-е ключевое слово {0}?",
                },
                [SEnaCipher.ExtAnswer] = new()
                {
                    // English: What was the transposition key in {0}?
                    Question = "Какой был ключ перестановки {0}?",
                },
                [SEnaCipher.EncryptedAnswer] = new()
                {
                    // English: What was the encrypted word in {0}?
                    Question = "Какое слово было зашифрованно {0}",
                },
            },
        },

        [typeof(SEncryptedDice)] = new()
        {
            Questions = new()
            {
                [SEncryptedDice.Question] = new()
                {
                    // English: Which of these numbers appeared on a die in the {1} stage of {0}?
                    // Example: Which of these numbers appeared on a die in the first stage of Encrypted Dice?
                    Question = "Какие из этих чисел были на костях на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SEncryptedEquations)] = new()
        {
            Questions = new()
            {
                [SEncryptedEquations.Shapes] = new()
                {
                    // English: Which shape was the {1} operand in {0}?
                    // Example: Which shape was the first operand in Encrypted Equations?
                    Question = "Какая фигура была {1}-й переменной {0}?",
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
                    Question = "Какое название модуля было зашифрованно {0}?",
                },
                [SEncryptedHangman.EncryptionMethod] = new()
                {
                    // English: What method of encryption was used by {0}?
                    Question = "Какой метод шифрования был применён {0}?",
                    Answers = new()
                    {
                        ["Caesar Cipher"] = "Caesar Cipher",
                        ["Atbash Cipher"] = "Atbash Cipher",
                        ["Rot-13 Cipher"] = "Rot-13 Cipher",
                        ["Affine Cipher"] = "Affine Cipher",
                        ["Modern Cipher"] = "Modern Cipher",
                        ["Vigenère Cipher"] = "Vigenère Cipher",
                        ["Playfair Cipher"] = "Playfair Cipher",
                    },
                },
            },
        },

        [typeof(SEncryptedMaze)] = new()
        {
            Questions = new()
            {
                [SEncryptedMaze.Symbols] = new()
                {
                    // English: Which symbol on {0} was spinning {1}?
                    // Example: Which symbol on Encrypted Maze was spinning clockwise?
                    Question = "Какой символ {0} крутился {1}?",
                    Arguments = new()
                    {
                        ["clockwise"] = "по часовой",
                        ["counter-clockwise"] = "против часовой",
                    },
                },
            },
        },

        [typeof(SEncryptedMorse)] = new()
        {
            Questions = new()
            {
                [SEncryptedMorse.CallResponse] = new()
                {
                    // English: What was the {1} on {0}?
                    // Example: What was the received call on Encrypted Morse?
                    Question = "Какое было {1} {0}?",
                    Arguments = new()
                    {
                        ["received call"] = "полученное сообщение",
                        ["sent response"] = "отправленное сообщение",
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
                    Question = "Какая шифровка была первой {0}?",
                    Answers = new()
                    {
                        ["Morse Code"] = "Morse Code",
                        ["Tap Code"] = "Tap Code",
                        ["Maritime Flags"] = "Maritime Flags",
                        ["Semaphore"] = "Semaphore",
                        ["Pigpen"] = "Pigpen",
                        ["Lombax"] = "Lombax",
                        ["Braille"] = "Braille",
                        ["Wingdings"] = "Wingdings",
                        ["Zoni"] = "Zoni",
                        ["Galatic Alphabet"] = "Galatic Alphabet",
                        ["Arrow"] = "Arrow",
                        ["Listening"] = "Listening",
                        ["Regular Number"] = "Regular Number",
                        ["Chinese Number"] = "Chinese Number",
                        ["Cube Symbols"] = "Cube Symbols",
                        ["Runes"] = "Runes",
                        ["New York Point"] = "New York Point",
                        ["Fontana"] = "Fontana",
                        ["ASCII Hex Code"] = "ASCII Hex Code",
                    },
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
                    Question = "What was the{1} digit in the {2} number shown in {0}?",
                },
            },
        },

        [typeof(SEquationsX)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SEquationsX.Symbols] = new()
                {
                    // English: What was the displayed symbol in {0}?
                    Question = "Какой символ был показан на {0}?",
                },
            },
        },

        [typeof(SErrorCodes)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SErrorCodes.ActiveError] = new()
                {
                    // English: What was the active error code in {0}?
                    Question = "Какой код был активным на {0}?",
                },
            },
        },

        [typeof(SEtterna)] = new()
        {
            Questions = new()
            {
                [SEtterna.Number] = new()
                {
                    // English: What was the beat for the {1} arrow from the bottom in {0}?
                    // Example: What was the beat for the first arrow from the bottom in Etterna?
                    Question = "Какой бит был у {1}-й стрелки снизу вверх {0}?",
                },
            },
        },

        [typeof(SExoplanets)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SExoplanets.StartingTargetPlanet] = new()
                {
                    // English: What was the starting target planet in {0}?
                    Question = "Какая была начальная целевая планета из {0}?",
                    Answers = new()
                    {
                        ["outer"] = "Внешняя",
                        ["middle"] = "Средняя",
                        ["inner"] = "Внутреняя",
                        ["none"] = "Никакая",
                    },
                },
                [SExoplanets.StartingTargetDigit] = new()
                {
                    // English: What was the starting target digit in {0}?
                    Question = "Какая была начальная целевая цифра {0}?",
                },
                [SExoplanets.TargetPlanet] = new()
                {
                    // English: What was the final target planet in {0}?
                    Question = "Какая была финальная целевая планета из {0}?",
                    Answers = new()
                    {
                        ["outer"] = "Внешняя",
                        ["middle"] = "Средняя",
                        ["inner"] = "Внутреняя",
                        ["none"] = "Никакая",
                    },
                },
                [SExoplanets.TargetDigit] = new()
                {
                    // English: What was the final target digit in {0}?
                    Question = "Какая была финальная целевая цифра {0}?",
                },
            },
        },

        [typeof(SFactoringMaze)] = new()
        {
            Questions = new()
            {
                [SFactoringMaze.ChosenPrimes] = new()
                {
                    // English: What was one of the prime numbers chosen in {0}?
                    Question = "Какое из простых чисел было выбрано {0}?",
                },
            },
        },

        [typeof(SFactoryMaze)] = new()
        {
            Questions = new()
            {
                [SFactoryMaze.StartRoom] = new()
                {
                    // English: What room did you start in in {0}?
                    Question = "Какая была начальная комната {0}?",
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                    },
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
            ModuleName = "Быстрой математике",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SFastMath.LastLetters] = new()
                {
                    // English: What was the last pair of letters in {0}?
                    Question = "Какая пара букв была последней {0}?",
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
                    // English: Which button referred to this button in {0}? (+ sprite)
                    // Example: Which button referred to this button in Faulty Buttons? (+ sprite)
                    Question = "Which button referred to this button in {0}?",
                },
                [SFaultyButtons.ThisButtonReferredTo] = new()
                {
                    // English: Which button did this button refer to in {0}? (+ sprite)
                    // Example: Which button did this button refer to in Faulty Buttons? (+ sprite)
                    Question = "Which button did this button refer to in {0}?",
                },
            },
        },

        [typeof(SFaultyRGBMaze)] = new()
        {
            Questions = new()
            {
                [SFaultyRGBMaze.Keys] = new()
                {
                    // English: Where was the {1} key in {0}?
                    // Example: Where was the red key in Faulty RGB Maze?
                    Question = "Где был {1} ключ {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красный",
                        ["green"] = "зелёный",
                        ["blue"] = "синий",
                    },
                },
                [SFaultyRGBMaze.Number] = new()
                {
                    // English: Which maze number was the {1} maze in {0}?
                    // Example: Which maze number was the red maze in Faulty RGB Maze?
                    Question = "Какой {1} лабиринт был {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красный",
                        ["green"] = "зелёный",
                        ["blue"] = "синий",
                    },
                },
                [SFaultyRGBMaze.Exit] = new()
                {
                    // English: What was the exit coordinate in {0}?
                    Question = "На каких координатах был выход {0}?",
                },
            },
        },

        [typeof(SFindTheDate)] = new()
        {
            Questions = new()
            {
                [SFindTheDate.Month] = new()
                {
                    // English: What was the month displayed in the {1} stage of {0}?
                    // Example: What was the month displayed in the first stage of Find The Date?
                    Question = "Какой месяц был показан на {1}-м этапе {0}?",
                },
                [SFindTheDate.Day] = new()
                {
                    // English: What was the day displayed in the {1} stage of {0}?
                    // Example: What was the day displayed in the first stage of Find The Date?
                    Question = "Какой день был показан на {1}-м этапе {0}?",
                },
                [SFindTheDate.Year] = new()
                {
                    // English: What was the year displayed in the {1} stage of {0}?
                    // Example: What was the year displayed in the first stage of Find The Date?
                    Question = "Какой год был показан на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SFiveLetterWords)] = new()
        {
            Questions = new()
            {
                [SFiveLetterWords.DisplayedWords] = new()
                {
                    // English: Which of these words was on the display in {0}?
                    Question = "Какое из этих слов было на экране {0}?",
                },
            },
        },

        [typeof(SFizzBuzz)] = new()
        {
            Questions = new()
            {
                [SFizzBuzz.DisplayedNumbers] = new()
                {
                    // English: What was the {1} digit on the {2} display of {0}?
                    // Example: What was the first digit on the top display of FizzBuzz?
                    Question = "Какая была {1}-я цифра на {2} экране {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "среднем",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SFlags)] = new()
        {
            Questions = new()
            {
                [SFlags.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Какое число было показано на экране {0}?",
                },
                [SFlags.MainCountry] = new()
                {
                    // English: What was the main country flag in {0}?
                    Question = "Какой главный флаг отображался {0}?",
                },
                [SFlags.Countries] = new()
                {
                    // English: Which of these country flags was shown, but not the main country flag, in {0}?
                    Question = "Какой из этих флагов был показан (но не являлся главным) {0}?",
                },
            },
        },

        [typeof(SFlashingArrows)] = new()
        {
            Questions = new()
            {
                [SFlashingArrows.DisplayedValue] = new()
                {
                    // English: What number was displayed on {0}?
                    Question = "Какое число было показано {0}?",
                },
                [SFlashingArrows.ReferredArrow] = new()
                {
                    // English: What color flashed {1} black on the relevant arrow in {0}?
                    // Example: What color flashed before black on the relevant arrow in Flashing Arrows?
                    Question = "Какой цвет мигнул {1} на соответствующей стрелке {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Purple"] = "Фиолетовый",
                        ["White"] = "Белый",
                    },
                    Arguments = new()
                    {
                        ["before"] = "перед чёрным",
                        ["after"] = "после чёрного",
                    },
                },
            },
        },

        [typeof(SFlashingLights)] = new()
        {
            Questions = new()
            {
                [SFlashingLights.LEDFrequency] = new()
                {
                    // English: How many times did the {1} LED flash {2} on {0}?
                    // Example: How many times did the top LED flash cyan on Flashing Lights?
                    Question = "Сколько раз {1} светодиод мигал {2} {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхний",
                        ["cyan"] = "голубым",
                        ["green"] = "зелёным",
                        ["red"] = "красным",
                        ["purple"] = "фиолетовым",
                        ["orange"] = "оранжевым",
                        ["bottom"] = "нижний",
                    },
                },
            },
        },

        [typeof(SFlavorText)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SFlavorText.Module] = new()
                {
                    // English: Which module’s flavor text was shown in {0}?
                    Question = "К какому модулю был показан флейвор текст на {0}?",
                },
            },
        },

        [typeof(SFlavorTextEX)] = new()
        {
            Questions = new()
            {
                [SFlavorTextEX.Module] = new()
                {
                    // English: Which module’s flavor text was shown in the {1} stage of {0}?
                    // Example: Which module’s flavor text was shown in the first stage of Flavor Text EX?
                    Question = "К какому модулю был показан флейвор текст на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SFlyswatting)] = new()
        {
            Questions = new()
            {
                [SFlyswatting.Unpressed] = new()
                {
                    // English: Which fly was present, but not in the solution in {0}?
                    Question = "Какая муха присутствовала, но не была частью решения {0}?",
                },
            },
        },

        [typeof(SFollowMe)] = new()
        {
            Questions = new()
            {
                [SFollowMe.DisplayedPath] = new()
                {
                    // English: What was the {1} flashing direction in {0}?
                    // Example: What was the first flashing direction in Follow Me?
                    Question = "Какое было {1}-е мигающее направление {0}?",
                    Answers = new()
                    {
                        ["Up"] = "Вверх",
                        ["Down"] = "Вниз",
                        ["Left"] = "Влево",
                        ["Right"] = "Вправо",
                    },
                },
            },
        },

        [typeof(SForestCipher)] = new()
        {
            Questions = new()
            {
                [SForestCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Forest Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SForgetAnyColor)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SForgetAnyColor.QCylinder] = new()
                {
                    // English: What colors were the cylinders during the {1} stage of {0}?
                    // Example: What colors were the cylinders during the first stage of Forget Any Color?
                    Question = "Какие были цилиндры на {1}-м этапе {0}?",
                    Additional = new()
                    {
                        ["{0}, {1}, {2}"] = "{0}, {1}, {2}",
                        ["Red"] = "красный",
                        ["Orange"] = "оранжевый",
                        ["Yellow"] = "жёлтый",
                        ["Green"] = "зелёный",
                        ["Cyan"] = "голубой",
                        ["Blue"] = "синий",
                        ["Purple"] = "фиолетовый",
                        ["White"] = "белый",
                        ["L"] = "Л",
                        ["M"] = "Ц",
                        ["R"] = "П",
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
                    Discriminator = "Forget Any Color, где цвета цилиндров на {1}-м этапе были: {0}",
                },
                [SForgetAnyColor.DFigure] = new()
                {
                    // English: the Forget Any Color which used figure {0} in the {1} stage
                    // Example: the Forget Any Color which used figure LLLMR in the first stage
                    Discriminator = "Forget Any Color, где на {1}-м этапе была применена строка {0}",
                },
            },
        },

        [typeof(SForgetEverything)] = new()
        {
            ModuleName = "Полного забвения",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SForgetEverything.QStageOneDisplay] = new()
                {
                    // English: What was the {1} displayed digit in the first stage of {0}?
                    // Example: What was the first displayed digit in the first stage of Forget Everything?
                    Question = "Какая была {1}-я отображённая цифра на первом этапе {0}?",
                },
            },
            Discriminators = new()
            {
                [SForgetEverything.DStageOneDisplay] = new()
                {
                    // English: the Forget Everything whose {0} displayed digit in that stage was {1}
                    // Example: the Forget Everything whose first displayed digit in that stage was 1
                    Discriminator = "Полного забвения, {0}-я отображённая цифра которого на том этапе была {1}",
                },
            },
        },

        [typeof(SForgetMe)] = new()
        {
            Questions = new()
            {
                [SForgetMe.InitialState] = new()
                {
                    // English: What number was in the {1} position of the initial puzzle in {0}?
                    // Example: What number was in the top-left position of the initial puzzle in Forget Me?
                    Question = "Какое число было на {1} кнопке изначального пазла {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "верхней левой",
                        ["top-middle"] = "верхней средней",
                        ["top-right"] = "верхней правой",
                        ["middle-left"] = "средней левой",
                        ["center"] = "центральной",
                        ["middle-right"] = "средней правой",
                        ["bottom-left"] = "нижней левой",
                        ["bottom-middle"] = "нижней средней",
                        ["bottom-right"] = "нижней правой",
                    },
                },
            },
        },

        [typeof(SForgetMeNot)] = new()
        {
            ModuleName = "Незабудки",
            Conjugation = Conjugation.GenitiveFeminine,
            Questions = new()
            {
                [SForgetMeNot.Question] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Forget Me Not?
                    Question = "Какая цифра была отображена на {1}-м этапе {0}?",
                },
            },
            Discriminators = new()
            {
                [SForgetMeNot.Discriminator] = new()
                {
                    // English: the Forget Me Not which displayed a {0} in the {1} stage
                    // Example: the Forget Me Not which displayed a 1 in the first stage
                    Discriminator = "Незабудки, на которой была отображена {0} на {1}-м этапе",
                },
            },
        },

        [typeof(SForgetMeNow)] = new()
        {
            ModuleName = "Забудке",
            Conjugation = Conjugation.PrepositiveFeminine,
            Questions = new()
            {
                [SForgetMeNow.DisplayedDigits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Forget Me Now?
                    Question = "Какая была {1}-я отображённая цифра на {0}?",
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
            ModuleName = "Финальной битве забвения",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SForgetsUltimateShowdown.Answer] = new()
                {
                    // English: What was the {1} digit of the answer in {0}?
                    // Example: What was the first digit of the answer in Forget’s Ultimate Showdown?
                    Question = "Какая была {1}-я цифра финального кода {0}?",
                },
                [SForgetsUltimateShowdown.Bottom] = new()
                {
                    // English: What was the {1} digit of the bottom number in {0}?
                    // Example: What was the first digit of the bottom number in Forget’s Ultimate Showdown?
                    Question = "Какая была {1}-я цифра нижнего числа {0}?",
                },
                [SForgetsUltimateShowdown.Initial] = new()
                {
                    // English: What was the {1} digit of the initial number in {0}?
                    // Example: What was the first digit of the initial number in Forget’s Ultimate Showdown?
                    Question = "Какая была {1}-я цифра изначального кодового числа {0}?",
                },
                [SForgetsUltimateShowdown.Method] = new()
                {
                    // English: What was the {1} method used in {0}?
                    // Example: What was the first method used in Forget’s Ultimate Showdown?
                    Question = "Какой был {1}-й использованный метод {0}?",
                    Answers = new()
                    {
                        ["Forget Me Not"] = "Незабудка",
                        ["Simon’s Stages"] = "Выступление Саймона",
                        ["Forget Me Later"] = "Забудемся позже",
                        ["Forget Infinity"] = "Бесконечное забвение",
                        ["A>N<D"] = "＞И＜",
                        ["Forget Me Now"] = "Забудка",
                        ["Forget Everything"] = "Полное забвение",
                        ["Forget Us Not"] = "Незабудки",
                    },
                },
            },
        },

        [typeof(SForgetTheColors)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "\"Забудь цвета\"",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SForgetTheColors.QGearNumber] = new()
                {
                    // English: What number was on the gear during stage {1} of {0}?
                    // Example: What number was on the gear during stage 0 of Forget The Colors?
                    Question = "Какое число было на шестерёнке на {1}-м этапе {0}?",
                },
                [SForgetTheColors.QLargeDisplay] = new()
                {
                    // English: What number was on the large display during stage {1} of {0}?
                    // Example: What number was on the large display during stage 0 of Forget The Colors?
                    Question = "Какое число было на большом экране на {1}-м этапе {0}?",
                },
                [SForgetTheColors.QSineNumber] = new()
                {
                    // English: What was the last decimal in the sine number received during stage {1} of {0}?
                    // Example: What was the last decimal in the sine number received during stage 0 of Forget The Colors?
                    Question = "Какая была последняя дробная цифра полученного числа синуса на {1}-м этапе {0}?",
                },
                [SForgetTheColors.QGearColor] = new()
                {
                    // English: What color was the gear during stage {1} of {0}?
                    // Example: What color was the gear during stage 0 of Forget The Colors?
                    Question = "Какого цвета была шестерёнка на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Cyan"] = "Голубой",
                        ["Blue"] = "Синий",
                        ["Purple"] = "Фиолетоывй",
                        ["Pink"] = "Розовый",
                        ["Maroon"] = "Бордовый",
                        ["White"] = "Белый",
                        ["Gray"] = "Серый",
                    },
                },
                [SForgetTheColors.QRuleColor] = new()
                {
                    // English: Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?
                    // Example: Which edgework-based rule was applied to the sum of nixies and gear during stage 0 of Forget The Colors?
                    Question = "Какого цвета было правило, по которому вы сложили числа на лампах и шестерёнках на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Cyan"] = "Голубой",
                        ["Blue"] = "Синий",
                        ["Purple"] = "Фиолетоывй",
                        ["Pink"] = "Розовый",
                        ["Maroon"] = "Бордовый",
                        ["White"] = "Белый",
                        ["Gray"] = "Серый",
                    },
                },
            },
            Discriminators = new()
            {
                [SForgetTheColors.DGearNumber] = new()
                {
                    // English: the Forget The Colors whose gear number was {0} in stage {1}
                    // Example: the Forget The Colors whose gear number was 1 in stage 1
                    Discriminator = "\"Забудь цвета\", где на {1}-м этапе на шестерёнке было {0}",
                },
                [SForgetTheColors.DLargeDisplay] = new()
                {
                    // English: the Forget The Colors which had {0} on its large display in stage {1}
                    // Example: the Forget The Colors which had 426 on its large display in stage 1
                    Discriminator = "\"Забудь цвета\", где на {1}-м этапе на большом экране было {0}",
                },
                [SForgetTheColors.DSineNumber] = new()
                {
                    // English: the Forget The Colors whose received sine number in stage {1} ended with a {0}
                    // Example: the Forget The Colors whose received sine number in stage 1 ended with a 0
                    Discriminator = "\"Забудь цвета\", где на {1}-м этапе полученное число синуса оканчивалось на {0}",
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
                    Question = "Какого цвета был светодиод на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                        ["Black"] = "Black",
                        ["White"] = "White",
                        ["Green"] = "Green",
                    },
                },
                [SForgetThis.QDigits] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Forget This?
                    Question = "Какая цифра была показана на {1}-м этапе {0}?",
                },
            },
            Discriminators = new()
            {
                [SForgetThis.DColors] = new()
                {
                    // English: the Forget This whose LED was {0} in the {1} stage
                    // Example: the Forget This whose LED was cyan in the first stage
                    Discriminator = "Forget This, на котором был {0} светодиод на {1}-м этапе",
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
                    Discriminator = "Forget This, который показывал {0} на {1}-м этапе",
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
                    // Example: the Forget Us Not in which Memory was used for stage 1
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
                    Question = "Какой был жетон игрока {0}?",
                    Answers = new()
                    {
                        ["Dog"] = "Dog",
                        ["Wheelbarrow"] = "Wheelbarrow",
                        ["Cat"] = "Cat",
                        ["Iron"] = "Iron",
                        ["Top Hat"] = "Top Hat",
                        ["Car"] = "Car",
                        ["Battleship"] = "Battleship",
                    },
                },
            },
        },

        [typeof(SFunctions)] = new()
        {
            Questions = new()
            {
                [SFunctions.LastDigit] = new()
                {
                    // English: What was the last digit of your first query’s result in {0}?
                    Question = "Какая была последняя цифра результата вашего первого запроса {0}?",
                },
                [SFunctions.LeftNumber] = new()
                {
                    // English: What number was to the left of the displayed letter in {0}?
                    Question = "Какое число было слева от отображённой буквы {0}?",
                },
                [SFunctions.Letter] = new()
                {
                    // English: What letter was displayed in {0}?
                    Question = "Какая буква была отображена {0}?",
                },
                [SFunctions.RightNumber] = new()
                {
                    // English: What number was to the right of the displayed letter in {0}?
                    Question = "Какое число было справа от отображённой буквы {0}?",
                },
            },
        },

        [typeof(SFuseBox)] = new()
        {
            Questions = new()
            {
                [SFuseBox.Flashes] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "Какой цвет горел {1}-м {0}?",
                },
                [SFuseBox.Arrows] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "Какая стрелка была показана {1}-й {0}?",
                },
            },
        },

        [typeof(SGadgetronVendor)] = new()
        {
            Questions = new()
            {
                [SGadgetronVendor.CurrentWeapon] = new()
                {
                    // English: What was your current weapon in {0}?
                    Question = "Какое оружие было текущим {0}?",
                },
                [SGadgetronVendor.WeaponForSale] = new()
                {
                    // English: What was the weapon up for sale in {0}?
                    Question = "Какое оружие продавалось {0}?",
                },
            },
        },

        [typeof(SGameOfLifeCruel)] = new()
        {
            Questions = new()
            {
                [SGameOfLifeCruel.Colors] = new()
                {
                    // English: Which of these was a color combination that occurred in {0}?
                    Question = "Какие комбинации цветов присутствовали {0}?",
                },
            },
        },

        [typeof(SGamepad)] = new()
        {
            ModuleName = "Геймпада",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SGamepad.Numbers] = new()
                {
                    // English: What were the numbers on {0}?
                    Question = "Какие числа были на экране {0}?",
                },
            },
        },

        [typeof(SGarfieldKart)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SGarfieldKart.Track] = new()
                {
                    // English: What was the track in {0}?
                    Question = "Какая была трасса на {0}?",
                },
                [SGarfieldKart.PuzzleCount] = new()
                {
                    // English: How many puzzle pieces did {0} have?
                    Question = "Сколько было частей пазла на {0}?",
                },
            },
        },

        [typeof(SGarnetThief)] = new()
        {
            Questions = new()
            {
                [SGarnetThief.Claim] = new()
                {
                    // English: Which faction did {1} claim to be in {0}?
                    // Example: Which faction did Jungmoon claim to be in Garnet Thief?
                    Question = "К какой фракции {1} заявлял, что он принадлежит {0}?",
                },
            },
        },

        [typeof(SGhostMovement)] = new()
        {
            Questions = new()
            {
                [SGhostMovement.Position] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was Inky in Ghost Movement?
                    Question = "Где был {1} {0}?",
                },
            },
        },

        [typeof(SGirlfriend)] = new()
        {
            Questions = new()
            {
                [SGirlfriend.Language] = new()
                {
                    // English: What was the language sung in {0}?
                    Question = "На каком языке была песня {0}?",
                },
            },
        },

        [typeof(SGlitchedButton)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SGlitchedButton.Sequence] = new()
                {
                    // English: What was the cycling bit sequence in {0}?
                    Question = "Какая последовательность битов повторялась на {0}?",
                },
            },
        },

        [typeof(SGoofysGame)] = new()
        {
            Questions = new()
            {
                [SGoofysGame.Number] = new()
                {
                    // English: What number was flashed by the {1} LED in {0}?
                    // Example: What number was flashed by the left LED in Goofy’s Game?
                    Question = "Какое число мигало на {1} светодиоде {0}?",
                    Arguments = new()
                    {
                        ["left"] = "левом",
                        ["right"] = "правом",
                        ["center"] = "центральном",
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
                    Question = "Which key made up the fifth set in {0}?",
                },
            },
        },

        [typeof(SGrayButton)] = new()
        {
            Questions = new()
            {
                [SGrayButton.Coordinates] = new()
                {
                    // English: What was the {1} coordinate on the display in {0}?
                    // Example: What was the horizontal coordinate on the display in Gray Button?
                    Question = "Какие были {1} координаты на экране {0}?",
                    Arguments = new()
                    {
                        ["horizontal"] = "горизонтальные",
                        ["vertical"] = "вертикальные",
                    },
                },
            },
        },

        [typeof(SGrayCipher)] = new()
        {
            Questions = new()
            {
                [SGrayCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Gray Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
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
                    Question = "Какая была {1}-я цифра {0}?",
                },
                [SGreatVoid.Color] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in Great Void?
                    Question = "Какой был {1}-й цвет {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                        ["Cyan"] = "Cyan",
                        ["White"] = "White",
                    },
                },
            },
        },

        [typeof(SGreenArrows)] = new()
        {
            ModuleName = "Зелёных стрелках",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SGreenArrows.LastScreen] = new()
                {
                    // English: What was the last number on the display on {0}?
                    Question = "Какое последнее число было показано на экране {0}?",
                },
            },
        },

        [typeof(SGreenButton)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SGreenButton.Word] = new()
                {
                    // English: What was the word submitted in {0}?
                    Question = "Какое слово было введено на {0}?",
                },
            },
        },

        [typeof(SGreenCipher)] = new()
        {
            Questions = new()
            {
                [SGreenCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Green Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SGridlock)] = new()
        {
            Questions = new()
            {
                [SGridlock.StartingColor] = new()
                {
                    // English: What was the starting color in {0}?
                    Question = "Какой был начальный цвет {0}?",
                    Answers = new()
                    {
                        ["Green"] = "Зелёный",
                        ["Yellow"] = "Жёлтый",
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                    },
                },
                [SGridlock.StartingLocation] = new()
                {
                    // English: What was the starting location in {0}?
                    Question = "Какая была начальная позиция {0}?",
                },
                [SGridlock.EndingLocation] = new()
                {
                    // English: What was the ending location in {0}?
                    Question = "Какая была конечная позиция {0}?",
                },
            },
        },

        [typeof(SGroceryStore)] = new()
        {
            Questions = new()
            {
                [SGroceryStore.FirstItem] = new()
                {
                    // English: What was the first item shown in {0}?
                    Question = "Какой товар был показан первым {0}?",
                },
            },
        },

        [typeof(SGryphons)] = new()
        {
            Questions = new()
            {
                [SGryphons.Name] = new()
                {
                    // English: What was the gryphon’s name in {0}?
                    Question = "Какое было имя у грифона {0}?",
                },
                [SGryphons.Age] = new()
                {
                    // English: What was the gryphon’s age in {0}?
                    Question = "Сколько лет было грифону {0}?",
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
            Questions = new()
            {
                [SGyromaze.LEDColor] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the top LED in Gyromaze?
                    Question = "Какого цвета был {1} светодиод {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                        ["Green"] = "Зелёный",
                        ["Yellow"] = "Жёлтый",
                    },
                    Arguments = new()
                    {
                        ["top"] = "верхний",
                        ["bottom"] = "нижний",
                    },
                },
            },
        },

        [typeof(SH)] = new()
        {
            Questions = new()
            {
                [SH.Letter] = new()
                {
                    // English: What was the transmitted letter in {0}?
                    Question = "Какая буква передавалась в \"{0}\"?",
                },
            },
        },

        [typeof(SHalliGalli)] = new()
        {
            Questions = new()
            {
                [SHalliGalli.Fruit] = new()
                {
                    // English: Which fruit were there five of in {0}?
                    Question = "Каких фруктов было пять {0}?",
                    Answers = new()
                    {
                        ["Strawberries"] = "Клубники",
                        ["Melons"] = "Арбузов",
                        ["Lemons"] = "Лемонов",
                        ["Raspberries"] = "Малины",
                        ["Bananas"] = "Бананов",
                    },
                },
                [SHalliGalli.Counts] = new()
                {
                    // English: What were the relevant counts in {0}?
                    Question = "Какие количества имели значение в {0}?",
                },
            },
        },

        [typeof(SHereditaryBaseNotation)] = new()
        {
            Questions = new()
            {
                [SHereditaryBaseNotation.InitialNumber] = new()
                {
                    // English: What was the given number in {0}?
                    Question = "Какое число было дано {0}?",
                },
            },
        },

        [typeof(SHexabutton)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SHexabutton.Label] = new()
                {
                    // English: What label was printed on {0}?
                    Question = "Какая была надпись на {0}?",
                },
            },
        },

        [typeof(SHexamaze)] = new()
        {
            ModuleName = "Гексабиринте",
            Questions = new()
            {
                [SHexamaze.PawnColor] = new()
                {
                    // English: What was the color of the pawn in {0}?
                    Question = "Какого цвета была фигурка {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красная",
                        ["Yellow"] = "Жёлтая",
                        ["Green"] = "Зелёная",
                        ["Cyan"] = "Голубая",
                        ["Blue"] = "Синяя",
                        ["Pink"] = "Розовая",
                    },
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
            Questions = new()
            {
                [SHexOS.OctCipher] = new()
                {
                    // English: What was the deciphered phrase in {0}?
                    Question = "Какая фраза была расшифрованна {0}?",
                },
                [SHexOS.Cipher] = new()
                {
                    // English: What were the deciphered letters in {0}?
                    Question = "Какие буквы были расшифрованны {0}?",
                },
                [SHexOS.Sum] = new()
                {
                    // English: What were the rhythm values in {0}?
                    Question = "Какие были значения ритмов {0}?",
                },
                [SHexOS.Screen] = new()
                {
                    // English: What was the {1} 3-digit number cycled by the screen in {0}?
                    // Example: What was the first 3-digit number cycled by the screen in hexOS?
                    Question = "Какое было {1}-е трёхзначное число в последовательности на экране {0}?",
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
                    Question = "Какого цвета был главный светодиод {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                        ["Orange"] = "Orange",
                        ["Purple"] = "Purple",
                        ["Magenta"] = "Magenta",
                        ["White"] = "White",
                    },
                },
            },
        },

        [typeof(SHiddenValue)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SHiddenValue.Display] = new()
                {
                    // English: What was displayed on {0}?
                    Question = "Что было отображено на {0}?",
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
            Questions = new()
            {
                [SHighScore.Position] = new()
                {
                    // English: What was the position of the player in {0}?
                    Question = "На котором месте был игрок {0}?",
                },
                [SHighScore.Score] = new()
                {
                    // English: What was the score of the player in {0}?
                    Question = "Какой был счёт у игрока {0}?",
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
            ModuleName = "Петлях",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SHinges.Initial] = new()
                {
                    // English: Which of these hinges was initially {1} {0}?
                    // Example: Which of these hinges was initially present on Hinges?
                    Question = "Какие из петель изначально {1} {0}?",
                    Arguments = new()
                    {
                        ["present on"] = "присутствовали",
                        ["absent from"] = "отсутствовали",
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
                    Question = "Какому дому зачли обезвреживание модуля \"{1}\" {0}?",
                    Answers = new()
                    {
                        ["Gryffindor"] = "Gryffindor",
                        ["Hufflepuff"] = "Hufflepuff",
                        ["Slytherin"] = "Slytherin",
                        ["Ravenclaw"] = "Ravenclaw",
                    },
                },
                [SHogwarts.Module] = new()
                {
                    // English: Which module was solved for {1} in {0}?
                    // Example: Which module was solved for Gryffindor in Hogwarts?
                    Question = "Обезвреживание какого модуля зачли {1} {0}?",
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
            Questions = new()
            {
                [SHoldUps.Shadows] = new()
                {
                    // English: What was the name of the {1} shadow shown in {0}?
                    // Example: What was the name of the first shadow shown in Hold Ups?
                    Question = "Какое было имя у {1}-й тени {0}?",
                },
            },
        },

        [typeof(SHomophones)] = new()
        {
            Questions = new()
            {
                [SHomophones.DisplayedPhrases] = new()
                {
                    // English: What was the {1} displayed phrase in {0}?
                    // Example: What was the first displayed phrase in Homophones?
                    Question = "Какая была {1}-я показанная фраза {0}?",
                },
            },
        },

        [typeof(SHorribleMemory)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SHorribleMemory.Positions] = new()
                {
                    // English: In what position was the button pressed on the {1} stage of {0}?
                    // Example: In what position was the button pressed on the first stage of Horrible Memory?
                    Question = "Какая позиция была нажата на {1}-м этапе {0}?",
                },
                [SHorribleMemory.Labels] = new()
                {
                    // English: What was the label of the button pressed on the {1} stage of {0}?
                    // Example: What was the label of the button pressed on the first stage of Horrible Memory?
                    Question = "Какая была надпись у нажатой кнопки на {1}-м этапе {0}?",
                },
                [SHorribleMemory.Colors] = new()
                {
                    // English: What color was the button pressed on the {1} stage of {0}?
                    // Example: What color was the button pressed on the first stage of Horrible Memory?
                    Question = "Какого цвета была нажатая кнопка на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["blue"] = "синий",
                        ["green"] = "зелёный",
                        ["red"] = "красный",
                        ["orange"] = "оранжевый",
                        ["purple"] = "фиолетовый",
                        ["pink"] = "розовый",
                    },
                },
            },
        },

        [typeof(SHumanResources)] = new()
        {
            Questions = new()
            {
                [SHumanResources.Descriptors] = new()
                {
                    // English: Which was a descriptor shown in {1} in {0}?
                    // Example: Which was a descriptor shown in red in Human Resources?
                    Question = "Какие описания {0} были показаны {1} цветом?",
                    Arguments = new()
                    {
                        ["red"] = "красным",
                        ["green"] = "зелёным",
                    },
                },
                [SHumanResources.HiredFired] = new()
                {
                    // English: Who was {1} in {0}?
                    // Example: Who was fired in Human Resources?
                    Question = "Кто из {0} был {1}?",
                    Arguments = new()
                    {
                        ["fired"] = "уволен",
                        ["hired"] = "нанят",
                    },
                },
            },
        },

        [typeof(SHunting)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SHunting.ColumnsRows] = new()
                {
                    // English: Which of the first three stages of {0} had the {1} symbol {2}?
                    // Example: Which of the first three stages of Hunting had the column symbol first?
                    Question = "На каком из первых трёх этапов {0} символ {1} был {2}-м?",
                    Answers = new()
                    {
                        ["none"] = "Ни на каком",
                        ["first"] = "На 1-м",
                        ["second"] = "На 2-м",
                        ["first two"] = "На 1-м и 2-м",
                        ["third"] = "На 3-м",
                        ["first & third"] = "На 1-м и 3-м",
                        ["second & third"] = "На 2-м и 3-м",
                        ["all three"] = "На всех трёх",
                    },
                    Arguments = new()
                    {
                        ["column"] = "столбца",
                        ["row"] = "строки",
                    },
                },
            },
        },

        [typeof(SHypercube)] = new()
        {
            ModuleName = "Гиперкуба",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SHypercube.Rotations] = new()
                {
                    // English: What was the {1} rotation in {0}?
                    // Example: What was the first rotation in Hypercube?
                    Question = "Каким было {1}-е вращение {0}?",
                },
            },
        },

        [typeof(SHyperForget)] = new()
        {
            Questions = new()
            {
                [SHyperForget.Rotations] = new()
                {
                    // English: What was the rotation for the {1} stage in {0}?
                    // Example: What was the rotation for the first stage in HyperForget?
                    Question = "Какое было вращение на {1}-м этапе {0}?",
                },
            },
            Discriminators = new()
            {
                [SHyperForget.Discriminator] = new()
                {
                    // English: the HyperForget whose rotation in the {1} stage was {0}
                    // Example: the HyperForget whose rotation in the first stage was XY
                    Discriminator = "в HyperForget, где на {1}-м этапе было вращение {0}",
                },
            },
        },

        [typeof(SHyperlink)] = new()
        {
            Questions = new()
            {
                [SHyperlink.Characters] = new()
                {
                    // English: What was the {1} character of the hyperlink in {0}?
                    // Example: What was the first character of the hyperlink in Hyperlink?
                    Question = "Какой был {1}-й символ ссылки {0}?",
                },
                [SHyperlink.Answer] = new()
                {
                    // English: Which module was referenced on {0}?
                    Question = "На какой модуль ссылался {0}?",
                },
            },
        },

        [typeof(SIceCream)] = new()
        {
            ModuleName = "Мороженого",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SIceCream.Flavour] = new()
                {
                    // English: Which one of these flavours {1} to the {2} customer in {0}?
                    // Example: Which one of these flavours was on offer, but not sold, to the first customer in Ice Cream?
                    Question = "Какой из этих вкусов {0} {1} {2}-му посетителю?",
                    Arguments = new()
                    {
                        ["was on offer, but not sold,"] = "был предложен, но не продан",
                        ["was not on offer"] = "не был предложен",
                    },
                },
                [SIceCream.Customer] = new()
                {
                    // English: Who was the {1} customer in {0}?
                    // Example: Who was the first customer in Ice Cream?
                    Question = "Кто был {1}-м посетителем {0}?",
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
                    Question = "Какая была {1}-я фигура {0}?",
                },
                [SIdentificationCrisis.Dataset] = new()
                {
                    // English: What was the {1} identification module used in {0}?
                    // Example: What was the first identification module used in Identification Crisis?
                    Question = "Какой был {1}-й использованный модуль {0}?",
                    Answers = new()
                    {
                        ["Morse Identification"] = "Morse Identification",
                        ["Boozleglyph Identification"] = "Boozleglyph Identification",
                        ["Plant Identification"] = "Plant Identification",
                        ["Pickup Identification"] = "Pickup Identification",
                        ["Emotiguy Identification"] = "Emotiguy Identification",
                        ["Ars Goetia Identification"] = "Ars Goetia Identification",
                        ["Mii Identification"] = "Mii Identification",
                        ["Customer Identification"] = "Customer Identification",
                        ["Spongebob Birthday Identification"] = "Spongebob Birthday Identification",
                        ["VTuber Identification"] = "VTuber Identification",
                    },
                },
            },
        },

        [typeof(SIdentityParade)] = new()
        {
            Questions = new()
            {
                [SIdentityParade.HairColors] = new()
                {
                    // English: Which hair color {1} listed in {0}?
                    // Example: Which hair color was listed in Identity Parade?
                    Question = "Какой цвет волос {1} {0}?",
                    Arguments = new()
                    {
                        ["was"] = "присутствовал",
                        ["was not"] = "отсутствовал",
                    },
                },
                [SIdentityParade.Builds] = new()
                {
                    // English: Which build {1} listed in {0}?
                    // Example: Which build was listed in Identity Parade?
                    Question = "Какое телосложение {1} {0}?",
                    Arguments = new()
                    {
                        ["was"] = "присутствовало",
                        ["was not"] = "отсутствовало",
                    },
                },
                [SIdentityParade.Attires] = new()
                {
                    // English: Which attire {1} listed in {0}?
                    // Example: Which attire was listed in Identity Parade?
                    Question = "Какой наряд {1} {0}?",
                    Arguments = new()
                    {
                        ["was"] = "присутствовал",
                        ["was not"] = "отсутствовал",
                    },
                },
            },
        },

        [typeof(SImpostor)] = new()
        {
            Conjugation = Conjugation.NominativeMasculine,
            Questions = new()
            {
                [SImpostor.Disguise] = new()
                {
                    // English: Which module was {0} pretending to be?
                    Question = "Каким модулем притворялся {0}?",
                },
            },
        },

        [typeof(SIndigoCipher)] = new()
        {
            Questions = new()
            {
                [SIndigoCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Indigo Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SInfiniteLoop)] = new()
        {
            Questions = new()
            {
                [SInfiniteLoop.SelectedWord] = new()
                {
                    // English: What was the selected word in {0}?
                    Question = "Какое слово было выбрано {0}?",
                },
            },
        },

        [typeof(SIngredients)] = new()
        {
            Questions = new()
            {
                [SIngredients.Ingredients] = new()
                {
                    // English: Which ingredient was used in {0}?
                    Question = "Какие ингредиенты были использованны {0}?",
                },
                [SIngredients.NonIngredients] = new()
                {
                    // English: Which ingredient was listed but not used in {0}?
                    Question = "Какие ингредиенты были указаны, но не были использованы {0}?",
                },
            },
        },

        [typeof(SInnerConnections)] = new()
        {
            Questions = new()
            {
                [SInnerConnections.LED] = new()
                {
                    // English: What color was the LED in {0}?
                    Question = "Какого цвета был светодиод {0}?",
                    Answers = new()
                    {
                        ["Black"] = "Чёрный",
                        ["Blue"] = "Синий",
                        ["Red"] = "Красный",
                        ["White"] = "Белый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                    },
                },
                [SInnerConnections.Morse] = new()
                {
                    // English: What was the digit flashed in Morse in {0}?
                    Question = "Какая цифра была передана кодом Морзе {0}?",
                },
            },
        },

        [typeof(SInterpunct)] = new()
        {
            Questions = new()
            {
                [SInterpunct.Display] = new()
                {
                    // English: What was the symbol displayed in the {1} stage of {0}?
                    // Example: What was the symbol displayed in the first stage of Interpunct?
                    Question = "Какой символ был показан на {1}-м этапе {0}?",
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
            Questions = new()
            {
                [SiPhone.Digits] = new()
                {
                    // English: What was the {1} PIN digit in {0}?
                    // Example: What was the first PIN digit in iPhone?
                    Question = "Какая была {1}-я цифра пинкода {0}?",
                },
            },
        },

        [typeof(SJenga)] = new()
        {
            Questions = new()
            {
                [SJenga.FirstBlock] = new()
                {
                    // English: Which symbol was on the first correctly pulled block in {0}?
                    Question = "Какой символ был на первом правильно вытянутом блоке {0}?",
                },
            },
        },

        [typeof(SJewelVault)] = new()
        {
            ModuleName = "Хранилище драгоценностей",
            Questions = new()
            {
                [SJewelVault.Wheels] = new()
                {
                    // English: What number was wheel {1} in {0}?
                    // Example: What number was wheel A in Jewel Vault?
                    Question = "Какой был номер у колеса {1} {0}?",
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
            ModuleName = "Смежных цветных квадратах",
            Conjugation = Conjugation.PrepositivePlural,
            Questions = new()
            {
                [SJuxtacoloredSquares.ColorsByPosition] = new()
                {
                    // English: What was the color of this square in {0}? (+ sprite)
                    Question = "Какого цвета был этот квадрат на {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Blue"] = "Синего",
                        ["Yellow"] = "Жёлтого",
                        ["Green"] = "Зелёного",
                        ["Magenta"] = "Пурпурного",
                        ["Orange"] = "Оранжевого",
                        ["Cyan"] = "Голубого",
                        ["Purple"] = "Фиолетового",
                        ["Chestnut"] = "Каштанового",
                        ["Brown"] = "Коричневого",
                        ["Mauve"] = "Лилового",
                        ["Azure"] = "Лазурного",
                        ["Jade"] = "Нефритового",
                        ["Forest"] = "Лесного",
                        ["Gray"] = "Серого",
                        ["Black"] = "Чёрного",
                    },
                },
                [SJuxtacoloredSquares.PositionsByColor] = new()
                {
                    // English: Which square was {1} in {0}?
                    // Example: Which square was red in Juxtacolored Squares?
                    Question = "Какой квадрат был {1} цвета на {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красного",
                        ["blue"] = "синего",
                        ["yellow"] = "жёлтого",
                        ["green"] = "зелёного",
                        ["magenta"] = "пурпурного",
                        ["orange"] = "оранжевого",
                        ["cyan"] = "голубого",
                        ["purple"] = "фиолетового",
                        ["chestnut"] = "каштанового",
                        ["brown"] = "коричневого",
                        ["mauve"] = "лилового",
                        ["azure"] = "лазурного",
                        ["jade"] = "нефритового",
                        ["forest"] = "лесного",
                        ["gray"] = "серого",
                        ["black"] = "чёрного",
                    },
                },
            },
        },

        [typeof(SKanji)] = new()
        {
            ModuleName = "Кандзи",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SKanji.DisplayedWords] = new()
                {
                    // English: What was the displayed word in the {1} stage of {0}?
                    // Example: What was the displayed word in the first stage of Kanji?
                    Question = "Какое слово было показано на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SKanyeEncounter)] = new()
        {
            Questions = new()
            {
                [SKanyeEncounter.Foods] = new()
                {
                    // English: What was a food item displayed in {0}?
                    Question = "Какая еда была показана {0}?",
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
                    Question = "What was the {1} phrase in {0}?",
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
            Questions = new()
            {
                [SKeypadCombination.WrongNumbers] = new()
                {
                    // English: Which number was displayed on the {1} button, but not part of the answer on {0}?
                    // Example: Which number was displayed on the first button, but not part of the answer on Keypad Combinations?
                    Question = "Какое число было показано на {1}-й кнопке, но не являлось частью решения {0}?",
                },
            },
        },

        [typeof(SKeypadMagnified)] = new()
        {
            Questions = new()
            {
                [SKeypadMagnified.LED] = new()
                {
                    // English: What was the position of the LED in {0}?
                    Question = "Где был светодиод {0}?",
                    Answers = new()
                    {
                        ["Top-left"] = "Сверху слева",
                        ["Top-right"] = "Сверху справа",
                        ["Bottom-left"] = "Снизу слева",
                        ["Bottom-right"] = "Снизу справа",
                    },
                },
            },
        },

        [typeof(SKeypadMaze)] = new()
        {
            Questions = new()
            {
                [SKeypadMaze.Yellow] = new()
                {
                    // English: Which of these cells was yellow in {0}?
                    Question = "Какая из этих клеток была жёлтой {0}?",
                },
            },
        },

        [typeof(SKeypadSequence)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SKeypadSequence.Labels] = new()
                {
                    // English: What was this key’s label on the {1} panel in {0}? (+ sprite)
                    // Example: What was this key’s label on the first panel in Keypad Sequence? (+ sprite)
                    Question = "Какая была надпись на этой кнопке на {1}-й панели {0}?",
                },
            },
        },

        [typeof(SKeywords)] = new()
        {
            Questions = new()
            {
                [SKeywords.DisplayedKey] = new()
                {
                    // English: What were the first four letters on the display in {0}?
                    Question = "Какие были первые четыре буквы на экране {0}?",
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
            Questions = new()
            {
                [SKnowYourWay.Arrow] = new()
                {
                    // English: Which way was the arrow pointing in {0}?
                    Question = "В какую сторону была направлена стрелка {0}?",
                    Answers = new()
                    {
                        ["Up"] = "Вверх",
                        ["Down"] = "Вниз",
                        ["Left"] = "Влево",
                        ["Right"] = "Вправо",
                    },
                },
                [SKnowYourWay.Led] = new()
                {
                    // English: Which LED was green in {0}?
                    Question = "Какой светодиод был зелёным {0}?",
                    Answers = new()
                    {
                        ["Top"] = "Верхний",
                        ["Bottom"] = "Нижний",
                        ["Right"] = "Правый",
                        ["Left"] = "Левый",
                    },
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
                    Question = "Какого цвета был светодиод на {1} кнопке {0}?",
                    Answers = new()
                    {
                        ["Crimson"] = "Crimson",
                        ["Red"] = "Red",
                        ["Coral"] = "Coral",
                        ["Orange"] = "Orange",
                        ["Lemon Chiffon"] = "Lemon Chiffon",
                        ["Medium Spring Green"] = "Medium Spring Green",
                        ["Deep Sea Green"] = "Deep Sea Green",
                        ["Cadet Blue"] = "Cadet Blue",
                        ["Slate Blue"] = "Slate Blue",
                        ["Dark Magenta"] = "Dark Magenta",
                        ["Unlit"] = "Unlit",
                    },
                    Arguments = new()
                    {
                        ["top-left"] = "верхней левой",
                        ["top-right"] = "верхней правой",
                        ["bottom-left"] = "нижней левой",
                        ["bottom-right"] = "нижней правой",
                    },
                },
            },
        },

        [typeof(SKudosudoku)] = new()
        {
            ModuleName = "Кудосудоку",
            Questions = new()
            {
                [SKudosudoku.Prefilled] = new()
                {
                    // English: Which square was {1} in {0}?
                    // Example: Which square was pre-filled in Kudosudoku?
                    Question = "Какой квадрат {1} {0}?",
                    Arguments = new()
                    {
                        ["pre-filled"] = "был изначально заполнен",
                        ["not pre-filled"] = "не был изначально заполнен",
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
                    Additional = new()
                    {
                        ["{0}{1}{2}{3}{4}{5}{6}"] = "{0}{1}{2}{3}{4}{5}{6}",
                        ["None"] = "None",
                    },
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
            Questions = new()
            {
                [SKuro.Mood] = new()
                {
                    // English: What was Kuro’s mood in {0}?
                    Question = "Какое было настроение у Kuro {0}?",
                },
            },
        },

        [typeof(SLabyrinth)] = new()
        {
            Questions = new()
            {
                [SLabyrinth.PortalLocations] = new()
                {
                    // English: Where was one of the portals in layer {1} in {0}?
                    // Example: Where was one of the portals in layer 1 (Red) in Labyrinth?
                    Question = "Где находился один из порталов на {1} слое {0}?",
                    Arguments = new()
                    {
                        ["1 (Red)"] = "1-м (красном)",
                        ["2 (Orange)"] = "2-м (оранжевом)",
                        ["3 (Yellow)"] = "3-м (жёлтом)",
                        ["4 (Green)"] = "4-м (зелёном)",
                        ["5 (Blue)"] = "5-м (синем)",
                    },
                },
                [SLabyrinth.PortalStage] = new()
                {
                    // English: In which layer was this portal in {0}? (+ sprite)
                    Question = "На каком слое находился этот портал {0}?",
                    Answers = new()
                    {
                        ["1 (Red)"] = "1-м (красном)",
                        ["2 (Orange)"] = "2-м (оранжевом)",
                        ["3 (Yellow)"] = "3-м (жёлтом)",
                        ["4 (Green)"] = "4-м (зелёном)",
                        ["5 (Blue)"] = "5-м (синем)",
                    },
                },
            },
        },

        [typeof(SLadderLottery)] = new()
        {
            Questions = new()
            {
                [SLadderLottery.LightOn] = new()
                {
                    // English: Which light was on in {0}?
                    Question = "Какая лампочка была включена {0}?",
                },
            },
        },

        [typeof(SLadders)] = new()
        {
            Questions = new()
            {
                [SLadders.Stage2Colors] = new()
                {
                    // English: Which color was present on the second ladder in {0}?
                    Question = "Какой цвет присутствовал на второй лестнице {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Cyan"] = "Голубой",
                        ["Purple"] = "Фиолетовый",
                        ["Gray"] = "Серый",
                    },
                },
                [SLadders.Stage3Missing] = new()
                {
                    // English: What color was missing on the third ladder in {0}?
                    Question = "Какой цвет отсутствовал на третьей лестнице {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Cyan"] = "Голубой",
                        ["Purple"] = "Фиолетовый",
                        ["Gray"] = "Серый",
                    },
                },
            },
        },

        [typeof(SLangtonsAnteater)] = new()
        {
            Questions = new()
            {
                [SLangtonsAnteater.InitialState] = new()
                {
                    // English: Which of these squares was initially {1} in {0}?
                    // Example: Which of these squares was initially black in Langton’s Anteater?
                    Question = "Какой из этих квадратов изначально был {1} {0}?",
                    Arguments = new()
                    {
                        ["black"] = "чёрным",
                        ["white"] = "белым",
                    },
                },
            },
        },

        [typeof(SLasers)] = new()
        {
            Questions = new()
            {
                [SLasers.Hatches] = new()
                {
                    // English: What was the number on the {1} hatch on {0}?
                    // Example: What was the number on the top-left hatch on Lasers?
                    Question = "Какое число было на {1} люке {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "верхнем левом",
                        ["top-middle"] = "верхнем среднем",
                        ["top-right"] = "верхнем правом",
                        ["middle-left"] = "среднем левом",
                        ["center"] = "центральном",
                        ["middle-right"] = "среднем правом",
                        ["bottom-left"] = "нижнем левом",
                        ["bottom-middle"] = "нижнем среднем",
                        ["bottom-right"] = "нижнем правом",
                    },
                },
            },
        },

        [typeof(SLEDEncryption)] = new()
        {
            ModuleName = "Шифра светодиодов",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SLEDEncryption.PressedLetters] = new()
                {
                    // English: What was the correct letter you pressed in the {1} stage of {0}?
                    // Example: What was the correct letter you pressed in the first stage of LED Encryption?
                    Question = "Какая правильная буква была нажата на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SLEDGrid)] = new()
        {
            ModuleName = "Сетке светодиодов",
            Conjugation = Conjugation.PrepositiveFeminine,
            Questions = new()
            {
                [SLEDGrid.NumBlack] = new()
                {
                    // English: How many LEDs were unlit in {0}?
                    Question = "Сколько светодиодов не горело на {0}?",
                },
            },
        },

        [typeof(SLEDMath)] = new()
        {
            Questions = new()
            {
                [SLEDMath.Lights] = new()
                {
                    // English: What color was {1} in {0}?
                    // Example: What color was LED A in LED Math?
                    Question = "Какого цвета был {1} {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Blue"] = "Синего",
                        ["Yellow"] = "Жёлтого",
                        ["Green"] = "Зелёного",
                    },
                    Arguments = new()
                    {
                        ["LED A"] = "светодиод A",
                        ["LED B"] = "светодиод B",
                        ["the operator LED"] = "светодиод-оператор",
                    },
                },
            },
        },

        [typeof(SLEDs)] = new()
        {
            Questions = new()
            {
                [SLEDs.OriginalColor] = new()
                {
                    // English: What was the initial color of the changed LED in {0}?
                    Question = "Какой был начальный цвет изменённого светодиода {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Purple"] = "Фиолетовый",
                        ["Black"] = "Чёрный",
                        ["White"] = "Белый",
                    },
                },
            },
        },

        [typeof(SLEGOs)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SLEGOs.PieceDimensions] = new()
                {
                    // English: What were the dimensions of the {1} piece in {0}?
                    // Example: What were the dimensions of the red piece in LEGOs?
                    Question = "Каких размеров была {1} деталь {0}?",
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
            Questions = new()
            {
                [SLetterMath.Display] = new()
                {
                    // English: What was the letter on the {1} display in {0}?
                    // Example: What was the letter on the left display in Letter Math?
                    Question = "Какая буква была на {1} экране {0}?",
                    Arguments = new()
                    {
                        ["left"] = "левом",
                        ["right"] = "правом",
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
                    Question = "Какой был цвет {1} лампочки {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Purple"] = "Purple",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                    },
                    Arguments = new()
                    {
                        ["left"] = "левой",
                        ["right"] = "правой",
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
                    Question = "Какая была {1}-я функция {0}?",
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
            Questions = new()
            {
                [SLionsShare.Year] = new()
                {
                    // English: Which year was displayed on {0}?
                    Question = "Какой год был показан {0}?",
                },
                [SLionsShare.RemovedLions] = new()
                {
                    // English: Which lion was present but removed in {0}?
                    Question = "Какой лев изначально присутствовал, но потом был убран {0}?",
                },
            },
        },

        [typeof(SListening)] = new()
        {
            ModuleName = "Прослушке",
            Questions = new()
            {
                [SListening.Sound] = new()
                {
                    // English: What clip was played in {0}?
                    Question = "Какой звук был воспроизведён {0}?",
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
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SLogicalButtons.Color] = new()
                {
                    // English: What was the color of the {1} button in the {2} stage of {0}?
                    // Example: What was the color of the top button in the first stage of Logical Buttons?
                    Question = "Какого цвета была {1} кнопка на {2}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красная",
                        ["Blue"] = "Синяя",
                        ["Green"] = "Зелёная",
                        ["Yellow"] = "Жёлтая",
                        ["Purple"] = "Фиолетовая",
                        ["White"] = "Белая",
                        ["Orange"] = "Оранжевая",
                        ["Cyan"] = "Голубая",
                        ["Grey"] = "Серая",
                    },
                    Arguments = new()
                    {
                        ["top"] = "верхняя",
                        ["bottom-left"] = "нижняя левая",
                        ["bottom-right"] = "нижняя правая",
                    },
                },
                [SLogicalButtons.Label] = new()
                {
                    // English: What was the label on the {1} button in the {2} stage of {0}?
                    // Example: What was the label on the top button in the first stage of Logical Buttons?
                    Question = "Какая была надпись на {1} кнопке на {2}-м этапе {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхней",
                        ["bottom-left"] = "нижней левой",
                        ["bottom-right"] = "нижней правой",
                    },
                },
                [SLogicalButtons.Operator] = new()
                {
                    // English: What was the final operator in the {1} stage of {0}?
                    // Example: What was the final operator in the first stage of Logical Buttons?
                    Question = "Какой был конечный оператор на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SLogicGates)] = new()
        {
            ModuleName = "Логических элементах",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SLogicGates.Gates] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was gate A in Logic Gates?
                    Question = "Каким был {1} {0}?",
                    Arguments = new()
                    {
                        ["gate A"] = "элемент A",
                        ["gate B"] = "элемент B",
                        ["gate C"] = "элемент C",
                        ["gate D"] = "элемент D",
                        ["gate E"] = "элемент E",
                        ["gate F"] = "элемент F",
                        ["gate G"] = "элемент G",
                        ["the duplicated gate"] = "элемент-дубликат",
                    },
                },
            },
        },

        [typeof(SLombaxCubes)] = new()
        {
            Questions = new()
            {
                [SLombaxCubes.Letters] = new()
                {
                    // English: What was the {1} letter on the button in {0}?
                    // Example: What was the first letter on the button in Lombax Cubes?
                    Question = "Какая была {1}-я буква на кнопке {0}?",
                },
            },
        },

        [typeof(SLondonUnderground)] = new()
        {
            Questions = new()
            {
                [SLondonUnderground.Stations] = new()
                {
                    // English: Where did the {1} journey on {0} {2}?
                    // Example: Where did the first journey on London Underground depart from?
                    Question = "{2} отправился {1}-й рейс {0}?",
                    Arguments = new()
                    {
                        ["depart from"] = "Откуда",
                        ["arrive to"] = "Куда",
                    },
                },
            },
        },

        [typeof(SLongWords)] = new()
        {
            Questions = new()
            {
                [SLongWords.Word] = new()
                {
                    // English: What was the word on the top display on {0}?
                    Question = "Какое слово было на верхнем экране {0}?",
                },
            },
        },

        [typeof(SMadMemory)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SMadMemory.Displays] = new()
                {
                    // English: What was on the display in the {1} stage of {0}?
                    // Example: What was on the display in the first stage of Mad Memory?
                    Question = "Что было на экране на {1} этапе {0}?",
                    Arguments = new()
                    {
                        ["first"] = "первом",
                        ["second"] = "втором",
                        ["third"] = "третьем",
                        ["4th"] = "четвёртом",
                    },
                },
            },
        },

        [typeof(SMafia)] = new()
        {
            ModuleName = "Мафии",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SMafia.Players] = new()
                {
                    // English: Who was a player, but not the Godfather, in {0}?
                    Question = "Кто был игроком, но не являлся крёстным отцом {0}?",
                },
            },
        },

        [typeof(SMagentaCipher)] = new()
        {
            Questions = new()
            {
                [SMagentaCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Magenta Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SMahjong)] = new()
        {
            Questions = new()
            {
                [SMahjong.CountingTile] = new()
                {
                    // English: Which tile was shown in the bottom-left of {0}?
                    Question = "Какая кость была показана снизу слева {0}?",
                },
                [SMahjong.Matches] = new()
                {
                    // English: Which tile was part of the {1} matched pair in {0}?
                    // Example: Which tile was part of the first matched pair in Mahjong?
                    Question = "Какая кость была частью {1}-й сопоставленной пары {0}?",
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
                    Question = "Какой главной странице соответствовал эффект кнопки {1} {0}?",
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
                    Question = "С какой главной страницы был взят {1} {0}?",
                    Arguments = new()
                    {
                        ["Homestar"] = "Homestar",
                        ["the background"] = "фон",
                    },
                },
                [SMainPage.BubbleColors] = new()
                {
                    // English: Which color did the bubble not display in {0}?
                    Question = "Какого цвета не было во фразах {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Синий",
                        ["Green"] = "Зелёный",
                        ["Red"] = "Красный",
                        ["Yellow"] = "Жёлтый",
                    },
                },
                [SMainPage.BubbleMessages] = new()
                {
                    // English: Which of the following messages did the bubble {1} in {0}?
                    // Example: Which of the following messages did the bubble display in Main Page?
                    Question = "Какая фраза {1} на {0}?",
                    Arguments = new()
                    {
                        ["display"] = "присутствовала",
                        ["not display"] = "отсутствовала",
                    },
                },
            },
        },

        [typeof(SMandMs)] = new()
        {
            Questions = new()
            {
                [SMandMs.Colors] = new()
                {
                    // English: What color was the text on the {1} button in {0}?
                    // Example: What color was the text on the first button in M&Ms?
                    Question = "Какого цвета была надпись на {1}-й кнопке {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["green"] = "Зелёного",
                        ["orange"] = "Оранжевого",
                        ["blue"] = "Синего",
                        ["yellow"] = "Жёлтого",
                        ["brown"] = "Коричневого",
                    },
                },
                [SMandMs.Labels] = new()
                {
                    // English: What was the text on the {1} button in {0}?
                    // Example: What was the text on the first button in M&Ms?
                    Question = "Какая надпись была на {1}-й кнопке {0}?",
                },
            },
        },

        [typeof(SMandNs)] = new()
        {
            Questions = new()
            {
                [SMandNs.Colors] = new()
                {
                    // English: What color was the text on the {1} button in {0}?
                    // Example: What color was the text on the first button in M&Ns?
                    Question = "Какого цвета была надпись на {1}-й кнопке {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["green"] = "Зелёного",
                        ["orange"] = "Оранжевого",
                        ["blue"] = "Синего",
                        ["yellow"] = "Жёлтого",
                        ["brown"] = "Коричневого",
                    },
                },
                [SMandNs.Label] = new()
                {
                    // English: What was the text on the correct button in {0}?
                    Question = "Какая надпись была на правильной кнопке {0}?",
                },
            },
        },

        [typeof(SMaritimeFlags)] = new()
        {
            Questions = new()
            {
                [SMaritimeFlags.Bearing] = new()
                {
                    // English: What bearing was signalled in {0}?
                    Question = "Какой пеленг был обозначен {0}?",
                },
                [SMaritimeFlags.Callsign] = new()
                {
                    // English: Which callsign was signalled in {0}?
                    Question = "Какой позывной был обозначен {0}?",
                },
            },
        },

        [typeof(SMaritimeSemaphore)] = new()
        {
            Questions = new()
            {
                [SMaritimeSemaphore.Dummy] = new()
                {
                    // English: In which position was the dummy in {0}?
                    Question = "В какой позиции была фиктивная конфигурация {0}?",
                },
                [SMaritimeSemaphore.Letter] = new()
                {
                    // English: Which letter was shown by the {2} in the {1} position in {0}?
                    // Example: Which letter was shown by the left flag in the first position in Maritime Semaphore?
                    Question = "Какая буква была показана {2} в {1}-й позиции {0}?",
                    Arguments = new()
                    {
                        ["left flag"] = "левым флагом",
                        ["right flag"] = "правым флагом",
                        ["semaphore"] = "семафором",
                    },
                },
            },
        },

        [typeof(SMaroonButton)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SMaroonButton.A] = new()
                {
                    // English: What was A in {0}?
                    Question = "Какой был флаг А на {0}?",
                },
            },
        },

        [typeof(SMaroonCipher)] = new()
        {
            Questions = new()
            {
                [SMaroonCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Maroon Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SMashematics)] = new()
        {
            Questions = new()
            {
                [SMashematics.Answer] = new()
                {
                    // English: What was the answer in {0}?
                    Question = "Какой был верный ответ {0}?",
                },
                [SMashematics.Calculation] = new()
                {
                    // English: What was the {1} number in the equation on {0}?
                    // Example: What was the first number in the equation on Mashematics?
                    Question = "Какое было {1}-е число в уравнении {0}?",
                },
            },
        },

        [typeof(SMasterTapes)] = new()
        {
            Questions = new()
            {
                [SMasterTapes.PlayedSong] = new()
                {
                    // English: Which song was played in {0}?
                    Question = "Какая песня была проиграна {0}?",
                },
            },
        },

        [typeof(SMatchRefereeing)] = new()
        {
            Questions = new()
            {
                [SMatchRefereeing.Planet] = new()
                {
                    // English: Which planet was present in the {1} stage of {0}?
                    // Example: Which planet was present in the first stage of Match Refereeing?
                    Question = "Какая планета присутствовала на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SMathEm)] = new()
        {
            Questions = new()
            {
                [SMathEm.Color] = new()
                {
                    // English: What was the color of this tile before the shuffle on {0}? (+ sprite)
                    Question = "Какого цвета была эта плитка до перемешивания {0}?",
                    Answers = new()
                    {
                        ["White"] = "Белый",
                        ["Bronze"] = "Бронзовый",
                        ["Silver"] = "Серебрянный",
                        ["Gold"] = "Золотой",
                    },
                },
                [SMathEm.Label] = new()
                {
                    // English: What was the design on this tile before the shuffle on {0}? (+ sprite)
                    Question = "Какой узор был на этой плитке до перемешивания {0}?",
                },
            },
        },

        [typeof(SMatrix)] = new()
        {
            Questions = new()
            {
                [SMatrix.AccessCode] = new()
                {
                    // English: Which word was part of the latest access code in {0}?
                    Question = "Какое слово было частью последнего кода доступа {0}?",
                },
                [SMatrix.GlitchWord] = new()
                {
                    // English: What was the glitched word in {0}?
                    Question = "Какое слово было глючным {0}?",
                },
            },
        },

        [typeof(SMaze)] = new()
        {
            ModuleName = "Лабиринте",
            Questions = new()
            {
                [SMaze.StartingPosition] = new()
                {
                    // English: In which {1} was the starting position in {0}, counting from the {2}?
                    // Example: In which column was the starting position in Maze, counting from the left?
                    Question = "В {1} (считая {2}) была начальная позиция {0}?",
                    Arguments = new()
                    {
                        ["column"] = "каком столбце",
                        ["left"] = "слева направо",
                        ["row"] = "какой строке",
                        ["top"] = "сверху вниз",
                    },
                },
            },
        },

        [typeof(SMaze3)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SMaze3.StartingFace] = new()
                {
                    // English: What was the color of the starting face in {0}?
                    Question = "Какой цвет был у начальной стороны {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Magenta"] = "Magenta",
                        ["Orange"] = "Orange",
                    },
                },
            },
        },

        [typeof(SMazeIdentification)] = new()
        {
            Questions = new()
            {
                [SMazeIdentification.Seed] = new()
                {
                    // English: What was the seed of the maze in {0}?
                    Question = "Какое было зерно у лабиринта {0}?",
                },
                [SMazeIdentification.Num] = new()
                {
                    // English: What was the function of button {1} in {0}?
                    // Example: What was the function of button 1 in Maze Identification?
                    Question = "Какая была функция у кнопки {1} {0}?",
                    Answers = new()
                    {
                        ["Forwards"] = "Вперёд",
                        ["Clockwise"] = "90° по часовой",
                        ["Backwards"] = "Назад",
                        ["Counter-clockwise"] = "90° против часовой",
                    },
                },
                [SMazeIdentification.Func] = new()
                {
                    // English: Which button {1} in {0}?
                    // Example: Which button moved you forwards in Maze Identification?
                    Question = "Какая кнопка {1} {0}?",
                    Arguments = new()
                    {
                        ["moved you forwards"] = "передвинула вас вперёд",
                        ["turned you clockwise"] = "повернула вас по часовой",
                        ["moved you backwards"] = "передвинула вас назад",
                        ["turned you counter-clockwise"] = "повернула вас против часовой",
                    },
                },
            },
        },

        [typeof(SMazematics)] = new()
        {
            ModuleName = "Матебиринте",
            Questions = new()
            {
                [SMazematics.Value] = new()
                {
                    // English: Which was the {1} value in {0}?
                    // Example: Which was the initial value in Mazematics?
                    Question = "Какая была {1} величина {0}?",
                    Arguments = new()
                    {
                        ["initial"] = "начальная",
                        ["goal"] = "целевая",
                    },
                },
            },
        },

        [typeof(SMazeScrambler)] = new()
        {
            Questions = new()
            {
                [SMazeScrambler.Start] = new()
                {
                    // English: What was the starting position on {0}?
                    Question = "Какая была начальная позиция {0}?",
                    Answers = new()
                    {
                        ["top-left"] = "Сверху слева",
                        ["top-middle"] = "Сверху посередине",
                        ["top-right"] = "Сверху справа",
                        ["middle-left"] = "Посередине слева",
                        ["middle-middle"] = "В центре",
                        ["middle-right"] = "Посередине справа",
                        ["bottom-left"] = "Снизу слева",
                        ["bottom-middle"] = "Снизу посередине",
                        ["bottom-right"] = "Снизу справа",
                    },
                },
                [SMazeScrambler.Goal] = new()
                {
                    // English: What was the goal on {0}?
                    Question = "Где была цель {0}?",
                    Answers = new()
                    {
                        ["top-left"] = "Сверху слева",
                        ["top-middle"] = "Сверху посередине",
                        ["top-right"] = "Сверху справа",
                        ["middle-left"] = "Посередине слева",
                        ["middle-middle"] = "В центре",
                        ["middle-right"] = "Посередине справа",
                        ["bottom-left"] = "Снизу слева",
                        ["bottom-middle"] = "Снизу посередине",
                        ["bottom-right"] = "Снизу справа",
                    },
                },
                [SMazeScrambler.Indicators] = new()
                {
                    // English: Which of these positions was a maze marking on {0}?
                    Question = "На какой позиции было обозначение лабиринта {0}?",
                    Answers = new()
                    {
                        ["top-left"] = "Сверху слева",
                        ["top-middle"] = "Сверху посередине",
                        ["top-right"] = "Сверху справа",
                        ["middle-left"] = "Посередине слева",
                        ["center"] = "Центр",
                        ["middle-right"] = "Посередине справа",
                        ["bottom-left"] = "Снизу слева",
                        ["bottom-middle"] = "Снизу посередине",
                        ["bottom-right"] = "Снизу справа",
                    },
                },
            },
        },

        [typeof(SMazeseeker)] = new()
        {
            Questions = new()
            {
                [SMazeseeker.Cell] = new()
                {
                    // English: How many walls surrounded this cell in {0}? (+ sprite)
                    Question = "Сколько стен окружало эту клетку {0}?",
                },
                [SMazeseeker.Start] = new()
                {
                    // English: Where was the start in {0}?
                    Question = "Где было начало {0}?",
                },
                [SMazeseeker.Goal] = new()
                {
                    // English: Where was the goal in {0}?
                    Question = "Где была цель {0}?",
                },
            },
        },

        [typeof(SMazeSwap)] = new()
        {
            Questions = new()
            {
                [SMazeSwap.Position] = new()
                {
                    // English: Where was the {1} position in {0}?
                    // Example: Where was the starting position in Maze Swap?
                    Question = "Где {1} {0}?",
                    Arguments = new()
                    {
                        ["starting"] = "было начало",
                        ["goal"] = "была цель",
                    },
                },
            },
        },

        [typeof(SMegaMan2)] = new()
        {
            Questions = new()
            {
                [SMegaMan2.Master] = new()
                {
                    // English: Which master was shown in {0}?
                    Question = "Какой мастер был показан {0}?",
                },
                [SMegaMan2.Weapon] = new()
                {
                    // English: Which weapon was shown in {0}?
                    Question = "Какое оружие было показано {0}?",
                },
            },
        },

        [typeof(SMelodySequencer)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SMelodySequencer.Parts] = new()
                {
                    // English: Which slot contained part #{1} at the start of {0}?
                    // Example: Which slot contained part #1 at the start of Melody Sequencer?
                    Question = "Какой слот содержал часть №{1} в начале {0}?",
                },
                [SMelodySequencer.Slots] = new()
                {
                    // English: Which part was in slot #{1} at the start of {0}?
                    // Example: Which part was in slot #1 at the start of Melody Sequencer?
                    Question = "Какая часть была в слоту №{1} в начале {0}?",
                },
            },
        },

        [typeof(SMemorableButtons)] = new()
        {
            Questions = new()
            {
                [SMemorableButtons.Symbols] = new()
                {
                    // English: What was the {1} correct symbol pressed in {0}?
                    // Example: What was the first correct symbol pressed in Memorable Buttons?
                    Question = "Какой был {1}-й верно нажатый символ {0}?",
                },
            },
        },

        [typeof(SMemory)] = new()
        {
            ModuleName = "Памяти",
            Conjugation = Conjugation.GenitiveFeminine,
            Questions = new()
            {
                [SMemory.Display] = new()
                {
                    // English: What was the displayed number in the {1} stage of {0}?
                    // Example: What was the displayed number in the first stage of Memory?
                    Question = "Какая цифра была на экране на {1}-м этапе {0}?",
                },
                [SMemory.Position] = new()
                {
                    // English: In what position was the button that you pressed in the {1} stage of {0}?
                    // Example: In what position was the button that you pressed in the first stage of Memory?
                    Question = "На какой позиции была кнопка, которую вы нажали на {1}-м этапе {0}?",
                },
                [SMemory.Label] = new()
                {
                    // English: What was the label of the button that you pressed in the {1} stage of {0}?
                    // Example: What was the label of the button that you pressed in the first stage of Memory?
                    Question = "С каким значением была кнопка, которую вы нажали на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SMemoryWires)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SMemoryWires.WireColours] = new()
                {
                    // English: What was the colour of wire {1} in {0}?
                    // Example: What was the colour of wire 1 in Memory Wires?
                    Question = "Какого цвета был {1}-й провод {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Yellow"] = "Жёлтый",
                        ["Blue"] = "Синий",
                        ["White"] = "Белый",
                        ["Black"] = "Чёрный",
                    },
                },
                [SMemoryWires.DisplayedDigits] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Memory Wires?
                    Question = "Какая цифра была показана на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SMetamorse)] = new()
        {
            Questions = new()
            {
                [SMetamorse.ExtractedLetter] = new()
                {
                    // English: What was the extracted letter in {0}?
                    Question = "Какая была извлечённая буква {0}?",
                },
            },
        },

        [typeof(SMetapuzzle)] = new()
        {
            Questions = new()
            {
                [SMetapuzzle.Answer] = new()
                {
                    // English: What was the final answer in {0}?
                    Question = "Какой был финальный ответ {0}?",
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
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SMirror.Word] = new()
                {
                    // English: What was the second word written by the original ghost in {0}?
                    Question = "Какое было второе слово, написанное оригинальным призраком на {0}?",
                },
            },
        },

        [typeof(SMisterSoftee)] = new()
        {
            Questions = new()
            {
                [SMisterSoftee.SpongebobPosition] = new()
                {
                    // English: Where was the SpongeBob Bar on {0}?
                    Question = "Где был SpongeBob Bar {0}?",
                },
                [SMisterSoftee.TreatsPresent] = new()
                {
                    // English: Which treat was present on {0}?
                    Question = "Какая сладость присутствовала на {0}?",
                },
            },
        },

        [typeof(SMixometer)] = new()
        {
            Questions = new()
            {
                [SMixometer.SubmitButton] = new()
                {
                    // English: What was the position of the submit button in {0}?
                    Question = "В какой позиции была кнопка отправки {0}?",
                },
            },
        },

        [typeof(SModernCipher)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SModernCipher.Word] = new()
                {
                    // English: What was the decrypted word of the {1} stage in {0}?
                    // Example: What was the decrypted word of the first stage in Modern Cipher?
                    Question = "Какое слово было расшифровано на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SModuleListening)] = new()
        {
            Questions = new()
            {
                [SModuleListening.ButtonAudio] = new()
                {
                    // English: Which sound did the {1} button play in {0}?
                    // Example: Which sound did the red button play in Module Listening?
                    Question = "Какой звук воспроизводился {1} кнопкой {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красной",
                        ["green"] = "зелёной",
                        ["blue"] = "синей",
                        ["yellow"] = "жёлтой",
                    },
                },
                [SModuleListening.AnyAudio] = new()
                {
                    // English: Which sound played in {0}?
                    Question = "Какой звук был воспроизведён {0}?",
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
            ModuleName = "Модульном лабиринте",
            Questions = new()
            {
                [SModuleMaze.StartingIcon] = new()
                {
                    // English: Which of the following was the starting icon for {0}?
                    Question = "Какая была начальная иконка модуля {0}?",
                },
            },
        },

        [typeof(SModuleMovements)] = new()
        {
            Questions = new()
            {
                [SModuleMovements.Display] = new()
                {
                    // English: What was the {1} module shown in {0}?
                    // Example: What was the first module shown in Module Movements?
                    Question = "Какой был {1}-й показанный модуль {0}?",
                },
            },
        },

        [typeof(SMoneyGame)] = new()
        {
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
            ModuleName = "\"Монсплоды, в атаку!\"",
            Questions = new()
            {
                [SMonsplodeFight.Creature] = new()
                {
                    // English: Which creature was displayed in {0}?
                    Question = "Какое существо было показано {0}?",
                },
                [SMonsplodeFight.Move] = new()
                {
                    // English: Which one of these moves {1} selectable in {0}?
                    // Example: Which one of these moves was selectable in Monsplode, Fight!?
                    Question = "Какой один из этих приёмов {1} доступен {0}?",
                    Arguments = new()
                    {
                        ["was"] = "был",
                        ["was not"] = "не был",
                    },
                },
            },
        },

        [typeof(SMonsplodeTradingCards)] = new()
        {
            ModuleName = "Коллекционных карточках по Монсплодам",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SMonsplodeTradingCards.Cards] = new()
                {
                    // English: What was the {1} before the last action in {0}?
                    // Example: What was the first card in your hand before the last action in Monsplode Trading Cards?
                    Question = "Какая была {1} перед последним действием {0}?",
                    Arguments = new()
                    {
                        ["first card in your hand"] = "первая карта в вашей руке",
                        ["second card in your hand"] = "вторая карта в вашей руке",
                        ["third card in your hand"] = "третья карта в вашей руке",
                        ["card on offer"] = "карта на обмен",
                    },
                },
                [SMonsplodeTradingCards.PrintVersions] = new()
                {
                    // English: What was the print version of the {1} before the last action in {0}?
                    // Example: What was the print version of the first card in your hand before the last action in Monsplode Trading Cards?
                    Question = "Какое было издание {1} перед последним действием {0}?",
                    Arguments = new()
                    {
                        ["first card in your hand"] = "первой карты в вашей руке",
                        ["second card in your hand"] = "второй карты в вашей руке",
                        ["third card in your hand"] = "третьей карты в вашей руке",
                        ["card on offer"] = "карты на обмен",
                    },
                },
            },
        },

        [typeof(SMoon)] = new()
        {
            Questions = new()
            {
                [SMoon.LitUnlit] = new()
                {
                    // English: What was the {1} set in clockwise order in {0}?
                    // Example: What was the first initially lit set in clockwise order in Moon?
                    Question = "Какой {1} по часовой стрелке {0}?",
                    Answers = new()
                    {
                        ["south"] = "Южный",
                        ["south-west"] = "Юго-западный",
                        ["west"] = "Западный",
                        ["north-west"] = "Северо-западный",
                        ["north"] = "Северный",
                        ["north-east"] = "Северо-восточный",
                        ["east"] = "Восточный",
                        ["south-east"] = "Юго-восточный",
                    },
                    Arguments = new()
                    {
                        ["first initially lit"] = "1-й светодиод горел",
                        ["second initially lit"] = "2-й светодиод горел",
                        ["third initially lit"] = "3-й светодиод горел",
                        ["fourth initially lit"] = "4-й светодиод горел",
                        ["first initially unlit"] = "1-й светодиод не горел",
                        ["second initially unlit"] = "2-й светодиод не горел",
                        ["third initially unlit"] = "3-й светодиод не горел",
                        ["fourth initially unlit"] = "4-й светодиод не горел",
                    },
                },
            },
        },

        [typeof(SMoreCode)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SMoreCode.Word] = new()
                {
                    // English: What was the flashing word in {0}?
                    Question = "Какое слово передовалось на {0}?",
                },
            },
        },

        [typeof(SMorseAMaze)] = new()
        {
            Questions = new()
            {
                [SMorseAMaze.StartingCoordinate] = new()
                {
                    // English: What was the starting location in {0}?
                    Question = "Какая была начальная позиция {0}?",
                },
                [SMorseAMaze.EndingCoordinate] = new()
                {
                    // English: What was the ending location in {0}?
                    Question = "Какая была конечная позиция {0}?",
                },
                [SMorseAMaze.MorseCodeWord] = new()
                {
                    // English: What was the word shown as Morse code in {0}?
                    Question = "Какое кодовое слово было передано через Морзе {0}?",
                },
            },
        },

        [typeof(SMorseButtons)] = new()
        {
            Questions = new()
            {
                [SMorseButtons.ButtonLabel] = new()
                {
                    // English: What was the character flashed by the {1} button in {0}?
                    // Example: What was the character flashed by the first button in Morse Buttons?
                    Question = "Какой символ передавался {1}-й кнопкой {0}?",
                },
                [SMorseButtons.ButtonColor] = new()
                {
                    // English: What was the color flashed by the {1} button in {0}?
                    // Example: What was the color flashed by the first button in Morse Buttons?
                    Question = "Каким цветом мигала {1}-я кнопка {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красным",
                        ["blue"] = "Синим",
                        ["green"] = "Зелёным",
                        ["yellow"] = "Жёлтым",
                        ["orange"] = "Оранжевым",
                        ["purple"] = "Фиолетовым",
                    },
                },
            },
        },

        [typeof(SMorsematics)] = new()
        {
            ModuleName = "Морзематике",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SMorsematics.ReceivedLetters] = new()
                {
                    // English: What was the {1} received letter in {0}?
                    // Example: What was the first received letter in Morsematics?
                    Question = "Какая была {1}-я полученная буква {0}?",
                },
            },
        },

        [typeof(SMorseWar)] = new()
        {
            Questions = new()
            {
                [SMorseWar.Code] = new()
                {
                    // English: What code was transmitted in {0}?
                    Question = "Какой код был передан {0}?",
                },
                [SMorseWar.Leds] = new()
                {
                    // English: What were the LEDs in the {1} row in {0} (1 = on, 0 = off)?
                    // Example: What were the LEDs in the bottom row in Morse War (1 = on, 0 = off)?
                    Question = "Какими были светодиоды в {1} ряду {0} (1 = вкл, 0 = выкл)?",
                    Arguments = new()
                    {
                        ["bottom"] = "нижнем",
                        ["middle"] = "центральном",
                        ["top"] = "верхнем",
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
            ModuleName = "Мыши в лабиринте",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SMouseInTheMaze.Sphere] = new()
                {
                    // English: Which color sphere was the goal in {0}?
                    Question = "Какого цвета была целевая сфера {0}?",
                    Answers = new()
                    {
                        ["white"] = "Белая",
                        ["green"] = "Зелёная",
                        ["blue"] = "Синяя",
                        ["yellow"] = "Жёлтая",
                    },
                },
                [SMouseInTheMaze.Torus] = new()
                {
                    // English: What color was the torus in {0}?
                    Question = "Какого цвета было кольцо {0}?",
                    Answers = new()
                    {
                        ["white"] = "Белое",
                        ["green"] = "Зелёное",
                        ["blue"] = "Синее",
                        ["yellow"] = "Жёлтое",
                    },
                },
            },
        },

        [typeof(SMSeq)] = new()
        {
            Questions = new()
            {
                [SMSeq.Obtained] = new()
                {
                    // English: What was the {1} obtained digit in {0}?
                    // Example: What was the first obtained digit in M-Seq?
                    Question = "Какая была {1}-я полученная цифра {0}?",
                },
                [SMSeq.Submitted] = new()
                {
                    // English: What was the final number from the iteration process in {0}?
                    Question = "Какое было финальное число итерационного процесса {0}?",
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
                    // English: Which vowel was missing in {0}?
                    // Example: Which vowel was missing in Mssngv Wls?
                    Question = "Какая гласная отсутствовала {0}?",
                    Arguments = new()
                    {
                        ["AEIOU"] = "АИОУЭЯЫЁЮЕ",
                    },
                },
            },
        },

        [typeof(SMulticoloredSwitches)] = new()
        {
            ModuleName = "Многоцветных переключателях",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SMulticoloredSwitches.LedColor] = new()
                {
                    // English: What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?
                    // Example: What color was the first LED on the top row when the tiny LED was lit in Multicolored Switches?
                    Question = "Какого цвета был {1}-й светодиод на {2} ряду, когда маленький светодиод {3} {0}?",
                    Answers = new()
                    {
                        ["black"] = "Чёрный",
                        ["red"] = "Красный",
                        ["green"] = "Зелёный",
                        ["yellow"] = "Жёлтый",
                        ["blue"] = "Синий",
                        ["magenta"] = "Пурпурный",
                        ["cyan"] = "Голубой",
                        ["white"] = "Белый",
                    },
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["lit"] = "горел",
                        ["bottom"] = "нижнем",
                        ["unlit"] = "не горел",
                    },
                },
            },
        },

        [typeof(SMurder)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Убийстве",
            Questions = new()
            {
                [SMurder.Suspect] = new()
                {
                    // English: Which of these was {1} in {0}?
                    // Example: Which of these was a suspect but not the murderer in Murder?
                    Question = "Кто {1} {0}?",
                    Answers = new()
                    {
                        ["Miss Scarlett"] = "Miss Scarlett",
                        ["Professor Plum"] = "Professor Plum",
                        ["Mrs Peacock"] = "Mrs Peacock",
                        ["Reverend Green"] = "Reverend Green",
                        ["Colonel Mustard"] = "Colonel Mustard",
                        ["Mrs White"] = "Mrs White",
                    },
                    Arguments = new()
                    {
                        ["a suspect but not the murderer"] = "не являлся убийцей, но был среди подозреваемых",
                        ["not a suspect"] = "не был подозреваемым",
                    },
                },
                [SMurder.Weapon] = new()
                {
                    // English: Which of these was {1} in {0}?
                    // Example: Which of these was a potential weapon but not the murder weapon in Murder?
                    Question = "{1} {0}?",
                    Answers = new()
                    {
                        ["Candlestick"] = "Candlestick",
                        ["Dagger"] = "Dagger",
                        ["Lead Pipe"] = "Lead Pipe",
                        ["Revolver"] = "Revolver",
                        ["Rope"] = "Rope",
                        ["Spanner"] = "Spanner",
                    },
                    Arguments = new()
                    {
                        ["a potential weapon but not the murder weapon"] = "Какое орудие было найдено, но не являлось орудием убийства",
                        ["not a potential weapon"] = "Какого орудия не было среди найденных возможных орудий",
                    },
                },
                [SMurder.BodyFound] = new()
                {
                    // English: Where was the body found in {0}?
                    Question = "Где было найдено тело {0}?",
                    Answers = new()
                    {
                        ["Dining Room"] = "Dining Room",
                        ["Study"] = "Study",
                        ["Kitchen"] = "Kitchen",
                        ["Lounge"] = "Lounge",
                        ["Billiard Room"] = "Billiard Room",
                        ["Conservatory"] = "Conservatory",
                        ["Ballroom"] = "Ballroom",
                        ["Hall"] = "Hall",
                        ["Library"] = "Library",
                    },
                },
            },
        },

        [typeof(SMysteryModule)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SMysteryModule.FirstKey] = new()
                {
                    // English: Which module was the first requested to be solved by {0}?
                    Question = "Какой модуль надо было обезвредить первым на {0}?",
                },
                [SMysteryModule.HiddenModule] = new()
                {
                    // English: Which module was hidden by {0}?
                    Question = "Какой модуль был спрятан за {0}?",
                },
            },
        },

        [typeof(SMysticSquare)] = new()
        {
            ModuleName = "Загадочном квадрате",
            Questions = new()
            {
                [SMysticSquare.Skull] = new()
                {
                    // English: Where was the skull in {0}?
                    Question = "Где находился череп {0}?",
                    Answers = new()
                    {
                        ["top left"] = "сверху слева",
                        ["top middle"] = "сверху посередине",
                        ["top right"] = "сверху справа",
                        ["middle left"] = "посередине слева",
                        ["center"] = "в центре",
                        ["middle right"] = "посередине справа",
                        ["bottom left"] = "снизу слева",
                        ["bottom middle"] = "снизу посередине",
                        ["bottom right"] = "снизу справа",
                    },
                },
            },
        },

        [typeof(SNameCodes)] = new()
        {
            Questions = new()
            {
                [SNameCodes.Indices] = new()
                {
                    // English: What was the {1} index in {0}?
                    // Example: What was the left index in Name Codes?
                    Question = "Какой был {1} индекс {0}?",
                    Arguments = new()
                    {
                        ["left"] = "левый",
                        ["right"] = "правый",
                    },
                },
            },
        },

        [typeof(SNamingConventions)] = new()
        {
            Questions = new()
            {
                [SNamingConventions.Object] = new()
                {
                    // English: What was the label of the first button in {0}?
                    Question = "Какая была надпись на 1й кнопке {0}?",
                },
            },
        },

        [typeof(SNandMs)] = new()
        {
            ModuleName = "N и M",
            Questions = new()
            {
                [SNandMs.Answer] = new()
                {
                    // English: What was the label of the correct button in {0}?
                    Question = "Какая надпись была на правильной кнопке {0}?",
                },
            },
        },

        [typeof(SNandNs)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SNandNs.Label] = new()
                {
                    // English: Which label was present in the {1} stage of {0}?
                    // Example: Which label was present in the first stage of N&Ns?
                    Question = "Какая надпись присутствовала на {1}-м этапе {0}?",
                },
                [SNandNs.Color] = new()
                {
                    // English: Which color was missing in the third stage of {0}?
                    Question = "Какой цвет отсутствовал на 3-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Green"] = "Зелёный",
                        ["Orange"] = "Оранжевый",
                        ["Blue"] = "Синий",
                        ["Yellow"] = "Жёлтый",
                        ["Brown"] = "Коричневый",
                    },
                },
            },
        },

        [typeof(SNavigationDetermination)] = new()
        {
            Questions = new()
            {
                [SNavigationDetermination.Color] = new()
                {
                    // English: What was the color of the maze in {0}?
                    Question = "Какого цвета был лабиринт {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Yellow"] = "Жёлтого",
                        ["Green"] = "Зелёного",
                        ["Blue"] = "Синего",
                    },
                },
                [SNavigationDetermination.Label] = new()
                {
                    // English: What was the label of the maze in {0}?
                    Question = "Какой буквой был обозначен лабиринт {0}?",
                },
            },
        },

        [typeof(SNavinums)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SNavinums.DirectionalButtons] = new()
                {
                    // English: What was the {1} directional button pressed in {0}?
                    // Example: What was the first directional button pressed in Navinums?
                    Question = "Какая кнопка направления была нажата {1}-й {0}?",
                    Answers = new()
                    {
                        ["up"] = "вверх",
                        ["left"] = "влево",
                        ["right"] = "вправо",
                        ["down"] = "вниз",
                    },
                },
                [SNavinums.MiddleDigit] = new()
                {
                    // English: What was the initial middle digit in {0}?
                    Question = "Какая цифра была изначально в центре {0}?",
                },
            },
        },

        [typeof(SNavyButton)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SNavyButton.QGreekLetters] = new()
                {
                    // English: Which Greek letter appeared on {0} (case-sensitive)?
                    Question = "Какая греческая буква (с учётом регистра) появилась на {0}?",
                },
                [SNavyButton.QGiven] = new()
                {
                    // English: What was the {1} of the given in {0}?
                    // Example: What was the (0-indexed) column of the given in Navy Button?
                    Question = "{1} (с индексом 0) на {0}?",
                    Arguments = new()
                    {
                        ["(0-indexed) column"] = "(0-indexed) column",
                        ["(0-indexed) row"] = "(0-indexed) row",
                        ["value"] = "Какое значение было указано",
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
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SNecronomicon.Chapters] = new()
                {
                    // English: What was the chapter number of the {1} page in {0}?
                    // Example: What was the chapter number of the first page in Necronomicon?
                    Question = "Какой был номер главы {1}-й страницы {0}?",
                },
            },
        },

        [typeof(SNegativity)] = new()
        {
            Questions = new()
            {
                [SNegativity.SubmittedValue] = new()
                {
                    // English: In base 10, what was the value submitted in {0}?
                    Question = "Какое значение было введено (в десятиричной системе) {0}?",
                },
                [SNegativity.SubmittedTernary] = new()
                {
                    // English: Excluding 0s, what was the submitted balanced ternary in {0}?
                    Question = "Какой сбалансированный троичный код был введён {0} (исключая нули)?",
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
            ModuleName = "Нейтрализации",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SNeutralization.Color] = new()
                {
                    // English: What was the acid’s color in {0}?
                    Question = "Какой цвет был у кислоты {0}?",
                    Answers = new()
                    {
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                    },
                },
                [SNeutralization.Volume] = new()
                {
                    // English: What was the acid’s volume in {0}?
                    Question = "Какой был объём кислоты {0}?",
                },
            },
        },

        [typeof(SNextInLine)] = new()
        {
            Questions = new()
            {
                [SNextInLine.FirstWire] = new()
                {
                    // English: What color was the first wire in {0}?
                    Question = "Какого цвета был первый провод {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Black"] = "Чёрный",
                        ["White"] = "Белый",
                        ["Gray"] = "Серый",
                    },
                },
            },
        },

        [typeof(SNonverbalSimon)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SNonverbalSimon.Flashes] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "Какой кнопка горела на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SNotColoredSquares)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SNotColoredSquares.InitialPosition] = new()
                {
                    // English: What was the position of the square you initially pressed in {0}?
                    Question = "Какая была позиция квадрата, который вы изначально нажали на {0}?",
                },
            },
        },

        [typeof(SNotColoredSwitches)] = new()
        {
            Questions = new()
            {
                [SNotColoredSwitches.Word] = new()
                {
                    // English: What was the encrypted word in {0}?
                    Question = "Какое было зашифрованное слово {0}?",
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
                    Question = "What was the initial word on {0}?",
                },
                [SNotColourFlash.InitialColour] = new()
                {
                    // English: What was {1} in the displayed colour sequence in {0}?
                    // Example: What was first in the displayed colour sequence in Not Colour Flash?
                    Question = "What was the initial colour of the word on {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                        ["White"] = "White",
                    },
                },
            },
        },

        [typeof(SNotConnectionCheck)] = new()
        {
            Questions = new()
            {
                [SNotConnectionCheck.Flashes] = new()
                {
                    // English: What symbol flashed on the {1} button in {0}?
                    // Example: What symbol flashed on the top left button in Not Connection Check?
                    Question = "Какой символ мигал на {1} кнопке {0}?",
                    Arguments = new()
                    {
                        ["top left"] = "верхней левой",
                        ["top right"] = "верхней правой",
                        ["bottom left"] = "нижней левой",
                        ["bottom right"] = "нижней правой",
                    },
                },
                [SNotConnectionCheck.Values] = new()
                {
                    // English: What was the value of the {1} button in {0}?
                    // Example: What was the value of the top left button in Not Connection Check?
                    Question = "Какое было значение {1} кнопки {0}?",
                    Arguments = new()
                    {
                        ["top left"] = "верхней левой",
                        ["top right"] = "верхней правой",
                        ["bottom left"] = "нижней левой",
                        ["bottom right"] = "нижней правой",
                    },
                },
            },
        },

        [typeof(SNotCoordinates)] = new()
        {
            Questions = new()
            {
                [SNotCoordinates.SquareCoords] = new()
                {
                    // English: Which coordinate was part of the square in {0}?
                    Question = "Какая координата была частью квадрата {0}?",
                },
            },
        },

        [typeof(SNotDoubleOh)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SNotDoubleOh.Position] = new()
                {
                    // English: What was the {1} displayed position in the second stage of {0}?
                    // Example: What was the first displayed position in the second stage of Not Double-Oh?
                    Question = "Какая позиция была показана {1}-й на втором этапе {0}?",
                },
            },
        },

        [typeof(SNotKeypad)] = new()
        {
            ModuleName = "Не клавиатуре",
            Conjugation = Conjugation.PrepositiveFeminine,
            Questions = new()
            {
                [SNotKeypad.Color] = new()
                {
                    // English: What color flashed {1} in the final sequence in {0}?
                    // Example: What color flashed first in the final sequence in Not Keypad?
                    Question = "Какой цвет горел {1}-м в последовательности на {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красный",
                        ["orange"] = "Оранжевый",
                        ["yellow"] = "Жёлтый",
                        ["green"] = "Зелёный",
                        ["cyan"] = "Голубой",
                        ["blue"] = "Синий",
                        ["purple"] = "Фиолетовый",
                        ["magenta"] = "Пурпурный",
                        ["pink"] = "Розовый",
                        ["brown"] = "Коричневый",
                        ["grey"] = "Серый",
                        ["white"] = "Белый",
                    },
                },
                [SNotKeypad.Symbol] = new()
                {
                    // English: Which symbol was on the button that flashed {1} in the final sequence in {0}?
                    // Example: Which symbol was on the button that flashed first in the final sequence in Not Keypad?
                    Question = "Какой символ был на кнопке, которая горела {1}-й в последовательности на {0}?",
                },
            },
        },

        [typeof(SNotMaze)] = new()
        {
            Questions = new()
            {
                [SNotMaze.StartingDistance] = new()
                {
                    // English: What was the starting distance in {0}?
                    Question = "Какая была начальная дистанция {0}?",
                },
            },
        },

        [typeof(SNotMorseCode)] = new()
        {
            Questions = new()
            {
                [SNotMorseCode.Word] = new()
                {
                    // English: What was the {1} correct word you submitted in {0}?
                    // Example: What was the first correct word you submitted in Not Morse Code?
                    Question = "Какое было {1}-е верное слово, которое вы отправили {0}?",
                },
            },
        },

        [typeof(SNotMorsematics)] = new()
        {
            Questions = new()
            {
                [SNotMorsematics.Word] = new()
                {
                    // English: What was the transmitted word on {0}?
                    Question = "Какое слово было передано {0}?",
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
                    Question = "В какой комнате изначально находился(-ась) {1} {0}?",
                    Answers = new()
                    {
                        ["Ballroom"] = "Ballroom",
                        ["Billiard Room"] = "Billiard Room",
                        ["Conservatory"] = "Conservatory",
                        ["Dining Room"] = "Dining Room",
                        ["Hall"] = "Hall",
                        ["Kitchen"] = "Kitchen",
                        ["Library"] = "Library",
                        ["Lounge"] = "Lounge",
                        ["Study"] = "Study",
                    },
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
                    Question = "Каким орудием изначально обладал(-а) {1} {0}?",
                    Answers = new()
                    {
                        ["Candlestick"] = "Candlestick",
                        ["Dagger"] = "Dagger",
                        ["Lead Pipe"] = "Lead Pipe",
                        ["Revolver"] = "Revolver",
                        ["Rope"] = "Rope",
                        ["Spanner"] = "Spanner",
                    },
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
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SNotNumberPad.Flashes] = new()
                {
                    // English: Which of these numbers {1} at the {2} stage of {0}?
                    // Example: Which of these numbers flashed at the first stage of Not Number Pad?
                    Question = "Какая их этих цифр {1} на {2}-м этапе {0}?",
                    Arguments = new()
                    {
                        ["flashed"] = "мигала",
                        ["did not flash"] = "не мигала",
                    },
                },
            },
        },

        [typeof(SNotPassword)] = new()
        {
            Questions = new()
            {
                [SNotPassword.Letter] = new()
                {
                    // English: Which letter was missing from {0}?
                    Question = "Какая буква отсутствовала {0}?",
                },
            },
        },

        [typeof(SNotPerspectivePegs)] = new()
        {
            Questions = new()
            {
                [SNotPerspectivePegs.Position] = new()
                {
                    // English: What was the position of the {1} flashing peg on {0}?
                    // Example: What was the position of the first flashing peg on Not Perspective Pegs?
                    Question = "В какой позиции находился {1}-й мигающий колышек {0}?",
                    Answers = new()
                    {
                        ["top"] = "Сверху",
                        ["top-right"] = "Сверху справа",
                        ["bottom-right"] = "Снизу справа",
                        ["bottom-left"] = "Снизу слева",
                        ["top-left"] = "Сверху слева",
                    },
                },
                [SNotPerspectivePegs.Perspective] = new()
                {
                    // English: From what perspective did the {1} peg flash on {0}?
                    // Example: From what perspective did the first peg flash on Not Perspective Pegs?
                    Question = "С какого ракурса мигнул {1}-й колышек {0}?",
                    Answers = new()
                    {
                        ["top"] = "С верхнего",
                        ["top-right"] = "С верх. правого",
                        ["bottom-right"] = "С ниж. правого",
                        ["bottom-left"] = "С ниж. левого",
                        ["top-left"] = "С верх. левого",
                    },
                },
                [SNotPerspectivePegs.Color] = new()
                {
                    // English: What was the color of the {1} flashing peg on {0}?
                    // Example: What was the color of the first flashing peg on Not Perspective Pegs?
                    Question = "Какого цвета был {1}-й мигающий колышек {0}?",
                    Answers = new()
                    {
                        ["blue"] = "Синего",
                        ["green"] = "Зелёного",
                        ["purple"] = "Фиолетового",
                        ["red"] = "Красного",
                        ["yellow"] = "Жёлтого",
                    },
                },
            },
        },

        [typeof(SNotPianoKeys)] = new()
        {
            Questions = new()
            {
                [SNotPianoKeys.FirstSymbol] = new()
                {
                    // English: What was the first displayed symbol on {0}?
                    Question = "Какой был первый изображённый символ {0}?",
                },
                [SNotPianoKeys.SecondSymbol] = new()
                {
                    // English: What was the second displayed symbol on {0}?
                    Question = "Какой был второй изображённый символ {0}?",
                },
                [SNotPianoKeys.ThirdSymbol] = new()
                {
                    // English: What was the third displayed symbol on {0}?
                    Question = "Какой был третий изображённый символ {0}?",
                },
            },
        },

        [typeof(SNotRedArrows)] = new()
        {
            Questions = new()
            {
                [SNotRedArrows.Start] = new()
                {
                    // English: What was the starting number in {0}?
                    Question = "Какое было исходное число {0}?",
                },
            },
        },

        [typeof(SNotSimaze)] = new()
        {
            Questions = new()
            {
                [SNotSimaze.Maze] = new()
                {
                    // English: Which maze was used in {0}?
                    Question = "Какой лабиринт был использован {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красный",
                        ["orange"] = "Оранжевый",
                        ["yellow"] = "Жёлтый",
                        ["green"] = "Зелёный",
                        ["blue"] = "Синий",
                        ["purple"] = "Фиолетовый",
                    },
                },
                [SNotSimaze.Start] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "Какая была начальная позиция {0}?",
                    Answers = new()
                    {
                        ["(red, red)"] = "(К, К)",
                        ["(red, orange)"] = "(К, О)",
                        ["(red, yellow)"] = "(К, Ж)",
                        ["(red, green)"] = "(К, З)",
                        ["(red, blue)"] = "(К, С)",
                        ["(red, purple)"] = "(К, Ф)",
                        ["(orange, red)"] = "(О, К)",
                        ["(orange, orange)"] = "(О, О)",
                        ["(orange, yellow)"] = "(О, Ж)",
                        ["(orange, green)"] = "(О, З)",
                        ["(orange, blue)"] = "(О, С)",
                        ["(orange, purple)"] = "(О, Ф)",
                        ["(yellow, red)"] = "(Ж, К)",
                        ["(yellow, orange)"] = "(Ж, О)",
                        ["(yellow, yellow)"] = "(Ж, Ж)",
                        ["(yellow, green)"] = "(Ж, З)",
                        ["(yellow, blue)"] = "(Ж, С)",
                        ["(yellow, purple)"] = "(Ж, Ф)",
                        ["(green, red)"] = "(З, К)",
                        ["(green, orange)"] = "(З, О)",
                        ["(green, yellow)"] = "(З, Ж)",
                        ["(green, green)"] = "(З, З)",
                        ["(green, blue)"] = "(З, С)",
                        ["(green, purple)"] = "(З, Ф)",
                        ["(blue, red)"] = "(С, К)",
                        ["(blue, orange)"] = "(С, О)",
                        ["(blue, yellow)"] = "(С, Ж)",
                        ["(blue, green)"] = "(С, З)",
                        ["(blue, blue)"] = "(С, С)",
                        ["(blue, purple)"] = "(С, Ф)",
                        ["(purple, red)"] = "(Ф, К)",
                        ["(purple, orange)"] = "(Ф, О)",
                        ["(purple, yellow)"] = "(Ф, Ж)",
                        ["(purple, green)"] = "(Ф, З)",
                        ["(purple, blue)"] = "(Ф, С)",
                        ["(purple, purple)"] = "(Ф, Ф)",
                    },
                },
                [SNotSimaze.Goal] = new()
                {
                    // English: What was the goal position in {0}?
                    Question = "Какая была конечная позиция {0}?",
                    Answers = new()
                    {
                        ["(red, red)"] = "(К, К)",
                        ["(red, orange)"] = "(К, О)",
                        ["(red, yellow)"] = "(К, Ж)",
                        ["(red, green)"] = "(К, З)",
                        ["(red, blue)"] = "(К, С)",
                        ["(red, purple)"] = "(К, Ф)",
                        ["(orange, red)"] = "(О, К)",
                        ["(orange, orange)"] = "(О, О)",
                        ["(orange, yellow)"] = "(О, Ж)",
                        ["(orange, green)"] = "(О, З)",
                        ["(orange, blue)"] = "(О, С)",
                        ["(orange, purple)"] = "(О, Ф)",
                        ["(yellow, red)"] = "(Ж, К)",
                        ["(yellow, orange)"] = "(Ж, О)",
                        ["(yellow, yellow)"] = "(Ж, Ж)",
                        ["(yellow, green)"] = "(Ж, З)",
                        ["(yellow, blue)"] = "(Ж, С)",
                        ["(yellow, purple)"] = "(Ж, Ф)",
                        ["(green, red)"] = "(З, К)",
                        ["(green, orange)"] = "(З, О)",
                        ["(green, yellow)"] = "(З, Ж)",
                        ["(green, green)"] = "(З, З)",
                        ["(green, blue)"] = "(З, С)",
                        ["(green, purple)"] = "(З, Ф)",
                        ["(blue, red)"] = "(С, К)",
                        ["(blue, orange)"] = "(С, О)",
                        ["(blue, yellow)"] = "(С, Ж)",
                        ["(blue, green)"] = "(С, З)",
                        ["(blue, blue)"] = "(С, С)",
                        ["(blue, purple)"] = "(С, Ф)",
                        ["(purple, red)"] = "(Ф, К)",
                        ["(purple, orange)"] = "(Ф, О)",
                        ["(purple, yellow)"] = "(Ф, Ж)",
                        ["(purple, green)"] = "(Ф, З)",
                        ["(purple, blue)"] = "(Ф, С)",
                        ["(purple, purple)"] = "(Ф, Ф)",
                    },
                },
            },
        },

        [typeof(SNotTextField)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SNotTextField.BackgroundLetter] = new()
                {
                    // English: Which letter appeared 9 times at the start of {0}?
                    Question = "Какая буква появилась 9 раз в начале на {0}?",
                },
                [SNotTextField.InitialPresses] = new()
                {
                    // English: Which letter was pressed in the first stage of {0}?
                    Question = "Какая буква была нажата на первом этапе на {0}?",
                },
            },
        },

        [typeof(SNotTheBulb)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SNotTheBulb.Word] = new()
                {
                    // English: What word flashed on {0}?
                    Question = "Какое слово мигало на {0}?",
                },
                [SNotTheBulb.Color] = new()
                {
                    // English: What color was the bulb on {0}?
                    Question = "Какого цвета была лампочка на {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красная",
                        ["Green"] = "Зелёная",
                        ["Blue"] = "Синяя",
                        ["Yellow"] = "Жёлтая",
                        ["Purple"] = "Фиолетовая",
                        ["White"] = "Белая",
                    },
                },
                [SNotTheBulb.ScrewCap] = new()
                {
                    // English: What was the material of the screw cap on {0}?
                    Question = "Из какого материала был сделан цоколь лампочки на {0}?",
                    Answers = new()
                    {
                        ["Copper"] = "Медь",
                        ["Silver"] = "Серебро",
                        ["Gold"] = "Золото",
                        ["Plastic"] = "Пластик",
                        ["Carbon Fibre"] = "Углеволокно",
                        ["Ceramic"] = "Керамика",
                    },
                },
            },
        },

        [typeof(SNotTheButton)] = new()
        {
            ModuleName = "Не кнопки",
            Conjugation = Conjugation.GenitiveFeminine,
            Questions = new()
            {
                [SNotTheButton.LightColor] = new()
                {
                    // English: What colors did the light glow in {0}?
                    Question = "Какими цветами горела цветная полоска {0}?",
                    Answers = new()
                    {
                        ["white"] = "Белым",
                        ["red"] = "Красным",
                        ["yellow"] = "Жёлтым",
                        ["green"] = "Зелёным",
                        ["blue"] = "Синим",
                        ["white/red"] = "Красно-белым",
                        ["white/yellow"] = "Жёлто-белым",
                        ["white/green"] = "Зелёно-белым",
                        ["white/blue"] = "Сине-белым",
                        ["red/yellow"] = "Красно-жёлтым",
                        ["red/green"] = "Красно-зелёным",
                        ["red/blue"] = "Красно-синим",
                        ["yellow/green"] = "Жёлто-зелёным",
                        ["yellow/blue"] = "Жёлто-синим",
                        ["green/blue"] = "Сине-зелёным",
                    },
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
                    Question = "Каким цветом мигал фон {0}?",
                    Answers = new()
                    {
                        ["Black"] = "Black",
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                        ["White"] = "White",
                    },
                },
            },
        },

        [typeof(SNotTheScrew)] = new()
        {
            Questions = new()
            {
                [SNotTheScrew.InitialPosition] = new()
                {
                    // English: What was the initial position in {0}?
                    Question = "Какая была начальная позиция {0}?",
                },
            },
        },

        [typeof(SNotWhosOnFirst)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SNotWhosOnFirst.PressedPosition] = new()
                {
                    // English: In which position was the button you pressed in the {1} stage on {0}?
                    // Example: In which position was the button you pressed in the first stage on Not Who’s on First?
                    Question = "На какой позиции была кнопка, которую вы нажали на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["top left"] = "сверху слева",
                        ["top right"] = "сверху справа",
                        ["middle left"] = "посередине слева",
                        ["middle right"] = "посередине справа",
                        ["bottom left"] = "снизу слева",
                        ["bottom right"] = "снизу справа",
                    },
                },
                [SNotWhosOnFirst.PressedLabel] = new()
                {
                    // English: What was the label on the button you pressed in the {1} stage on {0}?
                    // Example: What was the label on the button you pressed in the first stage on Not Who’s on First?
                    Question = "Что было написано на кнопке, которую вы нажали на {1}-м этапе {0}?",
                },
                [SNotWhosOnFirst.ReferencePosition] = new()
                {
                    // English: In which position was the reference button in the {1} stage on {0}?
                    // Example: In which position was the reference button in the first stage on Not Who’s on First?
                    Question = "На какой позиции была кнопка-ссылка на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["top left"] = "сверху слева",
                        ["top right"] = "сверху справа",
                        ["middle left"] = "посередине слева",
                        ["middle right"] = "посередине справа",
                        ["bottom left"] = "снизу слева",
                        ["bottom right"] = "снизу справа",
                    },
                },
                [SNotWhosOnFirst.ReferenceLabel] = new()
                {
                    // English: What was the label on the reference button in the {1} stage on {0}?
                    // Example: What was the label on the reference button in the first stage on Not Who’s on First?
                    Question = "Что было написано на кнопке-ссылке на {1}-м этапе {0}?",
                },
                [SNotWhosOnFirst.Sum] = new()
                {
                    // English: What was the calculated number in the second stage on {0}?
                    Question = "Какое было рассчитанное число на втором этапе {0}?",
                },
            },
        },

        [typeof(SNotWordSearch)] = new()
        {
            Questions = new()
            {
                [SNotWordSearch.Missing] = new()
                {
                    // English: Which of these consonants was missing in {0}?
                    Question = "Какая из этих согласных букв отсутствовала {0}?",
                },
                [SNotWordSearch.FirstPress] = new()
                {
                    // English: What was the first correctly pressed letter in {0}?
                    Question = "Какая была первая правильно нажатая буква {0}?",
                },
            },
        },

        [typeof(SNotX01)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SNotX01.SectorValues] = new()
                {
                    // English: Which sector value {1} present on {0}?
                    // Example: Which sector value was present on Not X01?
                    Question = "Какое значение сектора {1} на {0}?",
                    Arguments = new()
                    {
                        ["was"] = "присутствовало",
                        ["was not"] = "отсутствовало",
                    },
                },
            },
        },

        [typeof(SNotXRay)] = new()
        {
            Questions = new()
            {
                [SNotXRay.ScannerColor] = new()
                {
                    // English: What was the scanner color in {0}?
                    Question = "Какой был цвет сканера {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Yellow"] = "Жёлтый",
                        ["Blue"] = "Синий",
                        ["White"] = "Белый",
                    },
                },
                [SNotXRay.Table] = new()
                {
                    // English: What table were we in in {0} (numbered 1–8 in reading order in the manual)?
                    Question = "В какой таблице вы находились {0} (пронумерованных от 1 до 8 в порядке чтения в инструкции)?",
                },
                [SNotXRay.Directions] = new()
                {
                    // English: What direction was button {1} in {0}?
                    // Example: What direction was button 1 in Not X-Ray?
                    Question = "За какое направление отвечала кнопка \"{1}\" {0}?",
                    Answers = new()
                    {
                        ["Up"] = "Вверх",
                        ["Right"] = "Вправо",
                        ["Down"] = "Вних",
                        ["Left"] = "Влево",
                    },
                },
                [SNotXRay.Buttons] = new()
                {
                    // English: Which button went {1} in {0}?
                    // Example: Which button went up in Not X-Ray?
                    Question = "Какая кнопка отвечала за направление \"{1}\" {0}?",
                    Arguments = new()
                    {
                        ["up"] = "вверх",
                        ["right"] = "вправо",
                        ["down"] = "вниз",
                        ["left"] = "влево",
                    },
                },
            },
        },

        [typeof(SNumberedButtons)] = new()
        {
            Questions = new()
            {
                [SNumberedButtons.Buttons] = new()
                {
                    // English: Which number was correctly pressed on {0}?
                    Question = "Какое было правильно нажатое число {0}?",
                },
            },
        },

        [typeof(SNumberGame)] = new()
        {
            Questions = new()
            {
                [SNumberGame.Maximum] = new()
                {
                    // English: What was the maximum number in {0}?
                    Question = "Какое было максимальное число {0}?",
                },
            },
        },

        [typeof(SNumbers)] = new()
        {
            Questions = new()
            {
                [SNumbers.TwoDigit] = new()
                {
                    // English: What two-digit number was given in {0}?
                    Question = "Какое двухзначное число было дано {0}?",
                },
            },
        },

        [typeof(SNumpath)] = new()
        {
            Questions = new()
            {
                [SNumpath.Color] = new()
                {
                    // English: What was the color of the number on {0}?
                    Question = "Какого цвета было число {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Purple"] = "Фиолетовый",
                    },
                },
                [SNumpath.Digit] = new()
                {
                    // English: What was the number displayed on {0}?
                    Question = "Какое число было показано на {0}?",
                },
            },
        },

        [typeof(SObjectShows)] = new()
        {
            ModuleName = "Обджект-шоу",
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SObjectShows.Contestants] = new()
                {
                    // English: Which of these was a contestant on {0}?
                    Question = "Кто среди этих участников присутствовал на {0}?",
                },
            },
        },

        [typeof(SOctadecayotton)] = new()
        {
            Questions = new()
            {
                [SOctadecayotton.Sphere] = new()
                {
                    // English: What was the starting sphere in {0}?
                    Question = "Какая была начальная сфера {0}?",
                },
                [SOctadecayotton.Rotations] = new()
                {
                    // English: What was one of the subrotations in the {1} rotation in {0}?
                    // Example: What was one of the subrotations in the first rotation in Octadecayotton?
                    Question = "Каким было одно из промежуточных вращений в {1}-м вращении {0}?",
                },
            },
        },

        [typeof(SOddOneOut)] = new()
        {
            Questions = new()
            {
                [SOddOneOut.Button] = new()
                {
                    // English: What was the button you pressed in the {1} stage of {0}?
                    // Example: What was the button you pressed in the first stage of Odd One Out?
                    Question = "Какую кнопку вы нажали на {1}-м этапе {0}?",
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
                    Question = "Which of these runes was displayed in {0]?",
                },
            },
        },

        [typeof(SOldAI)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SOldAI.Group] = new()
                {
                    // English: What was the {1} of the numbers shown in {0}?
                    // Example: What was the group of the numbers shown in Old AI?
                    Question = "Какая {1} чисел была показана на {0}?",
                    Arguments = new()
                    {
                        ["group"] = "группа",
                        ["sub-group"] = "подгруппа",
                    },
                },
            },
        },

        [typeof(SOldFogey)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SOldFogey.StartingColor] = new()
                {
                    // English: What was the initial color of the status light in {0}?
                    Question = "Какой был исходный цвет индикатора на {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                        ["Cyan"] = "Cyan",
                        ["White"] = "White",
                    },
                },
            },
        },

        [typeof(SOneLinksToAll)] = new()
        {
            Questions = new()
            {
                [SOneLinksToAll.Start] = new()
                {
                    // English: What was the starting article in {0}?
                    Question = "Какая была начальная статья {0}?",
                },
                [SOneLinksToAll.End] = new()
                {
                    // English: What was the ending article in {0}?
                    Question = "Какая была последняя статья {0}?",
                },
            },
        },

        [typeof(SOnlyConnect)] = new()
        {
            ModuleName = "\"Лишь Соедините!\"",
            Questions = new()
            {
                [SOnlyConnect.Hieroglyphs] = new()
                {
                    // English: Which Egyptian hieroglyph was in the {1} in {0}?
                    // Example: Which Egyptian hieroglyph was in the top left in Only Connect?
                    Question = "Какой египетский иероглиф был {1} {0}?",
                    Answers = new()
                    {
                        ["Two Reeds"] = "Два тростника",
                        ["Lion"] = "Лев",
                        ["Twisted Flax"] = "Скрученный лён",
                        ["Horned Viper"] = "Рогатая гадюка",
                        ["Water"] = "Вода",
                        ["Eye of Horus"] = "Глаз Гора",
                    },
                    Arguments = new()
                    {
                        ["top left"] = "слева сверху",
                        ["top middle"] = "сверху посередине",
                        ["top right"] = "справа сверху",
                        ["bottom left"] = "слева снизу",
                        ["bottom middle"] = "снизу посередине",
                        ["bottom right"] = "справа снизу",
                    },
                },
            },
        },

        [typeof(SOrangeArrows)] = new()
        {
            ModuleName = "Оранжевых стрелок",
            Conjugation = Conjugation.GenitivePlural,
            Questions = new()
            {
                [SOrangeArrows.Sequences] = new()
                {
                    // English: What was the {1} arrow on the display of the {2} stage of {0}?
                    // Example: What was the first arrow on the display of the first stage of Orange Arrows?
                    Question = "Какая была {1}-я стрелка на экране на {2}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Up"] = "Вверх",
                        ["Right"] = "Вправо",
                        ["Down"] = "Вниз",
                        ["Left"] = "Влево",
                    },
                },
            },
        },

        [typeof(SOrangeCipher)] = new()
        {
            Questions = new()
            {
                [SOrangeCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Orange Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SOrderedKeys)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SOrderedKeys.Colors] = new()
                {
                    // English: What color was this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "Какого цвета была эта клавиша на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                    },
                },
                [SOrderedKeys.Labels] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What was the label of this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "Какая была надпись на этой клавише на {1}-м этапе {0}?",
                },
                [SOrderedKeys.LabelColors] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was the label of this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "Какого цвета была надпись на этой клавише на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                    },
                },
            },
        },

        [typeof(SOrderPicking)] = new()
        {
            Questions = new()
            {
                [SOrderPicking.Order] = new()
                {
                    // English: What was the order ID in the {1} order of {0}?
                    // Example: What was the order ID in the first order of Order Picking?
                    Question = "Какой был ID у {1}-го заказа {0}?",
                },
                [SOrderPicking.Product] = new()
                {
                    // English: What was the product ID in the {1} order of {0}?
                    // Example: What was the product ID in the first order of Order Picking?
                    Question = "Какой был ID продукта в {1}-м заказе {0}?",
                },
                [SOrderPicking.Pallet] = new()
                {
                    // English: What was the pallet in the {1} order of {0}?
                    // Example: What was the pallet in the first order of Order Picking?
                    Question = "Какой был паллет на {1}-м заказе {0}?",
                },
            },
        },

        [typeof(SOrientationCube)] = new()
        {
            ModuleName = "Ориентации куба",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SOrientationCube.InitialObserverPosition] = new()
                {
                    // English: What was the observer’s initial position in {0}?
                    Question = "Какая была начальная позиция у наблюдателя {0}?",
                    Answers = new()
                    {
                        ["front"] = "Спереди",
                        ["left"] = "Слева",
                        ["back"] = "Сзади",
                        ["right"] = "Справа",
                    },
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
                    Question = "Какой был начальный цвет {1} {0}?",
                    Answers = new()
                    {
                        ["black"] = "black",
                        ["red"] = "red",
                        ["green"] = "green",
                        ["yellow"] = "yellow",
                        ["blue"] = "blue",
                        ["magenta"] = "magenta",
                        ["cyan"] = "cyan",
                        ["white"] = "white",
                    },
                    Arguments = new()
                    {
                        ["right"] = "правой грани",
                        ["left"] = "левой грани",
                        ["top"] = "верхней грани",
                        ["bottom"] = "нижней грани",
                        ["back"] = "задней грани",
                        ["front"] = "фронтальной грани",
                        ["zag"] = "заг-грани",
                        ["zig"] = "зиг-грани",
                    },
                },
                [SOrientationHypercube.InitialObserverPosition] = new()
                {
                    // English: What was the observer’s initial position in {0}?
                    Question = "Какая была начальная позиция у наблюдателя {0}?",
                    Answers = new()
                    {
                        ["front"] = "Спереди",
                        ["left"] = "Слева",
                        ["back"] = "Сзади",
                        ["right"] = "Справа",
                    },
                },
            },
        },

        [typeof(SPalindromes)] = new()
        {
            Questions = new()
            {
                [SPalindromes.Numbers] = new()
                {
                    // English: What was {1}’s {2} digit from the right in {0}?
                    // Example: What was X’s first digit from the right in Palindromes?
                    Question = "Какая была {2}-я цифра справа {1} {0}?",
                    Arguments = new()
                    {
                        ["X"] = "у X",
                        ["Y"] = "у Y",
                        ["Z"] = "у Z",
                        ["the screen"] = "на экране",
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
            Questions = new()
            {
                [SParity.Display] = new()
                {
                    // English: What was shown on the display on {0}?
                    Question = "Что было показано на экране {0}?",
                },
            },
        },

        [typeof(SPartialDerivatives)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SPartialDerivatives.LedColors] = new()
                {
                    // English: What was the LED color in the {1} stage of {0}?
                    // Example: What was the LED color in the first stage of Partial Derivatives?
                    Question = "Какой был цвет светодиода на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["blue"] = "синий",
                        ["green"] = "зелёный",
                        ["orange"] = "оранжевый",
                        ["purple"] = "фиолетовый",
                        ["red"] = "красный",
                        ["yellow"] = "жёлтый",
                    },
                },
                [SPartialDerivatives.Terms] = new()
                {
                    // English: What was the {1} term in {0}?
                    // Example: What was the first term in Partial Derivatives?
                    Question = "Какой был {1}-й член {0}?",
                },
            },
        },

        [typeof(SPassportControl)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SPassportControl.Passenger] = new()
                {
                    // English: What was the passport expiration year of the {1} inspected passenger in {0}?
                    // Example: What was the passport expiration year of the first inspected passenger in Passport Control?
                    Question = "Какой был год истечения паспорта у {1}-го пассажира на {0}?",
                },
            },
        },

        [typeof(SPasswordDestroyer)] = new()
        {
            Conjugation = Conjugation.AccusativeMascNeuter,
            Questions = new()
            {
                [SPasswordDestroyer.TwoFactorV2] = new()
                {
                    // English: What was the 2FAST™ value when you solved {0}?
                    Question = "Чему был равен 2FAST™ когда вы обезвредили {0}?",
                },
            },
        },

        [typeof(SPatternCube)] = new()
        {
            ModuleName = "Развёртке куба",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SPatternCube.HighlightedSymbol] = new()
                {
                    // English: Which symbol was highlighted in {0}?
                    Question = "Какой символ был подсвечен {0}?",
                },
            },
        },

        [typeof(SPentabutton)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SPentabutton.BaseColor] = new()
                {
                    // English: What was the base colour in {0}?
                    Question = "Какой был цвет у основания {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Purple"] = "Фиолетовый",
                        ["White"] = "Белый",
                    },
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
            Questions = new()
            {
                [SPeriodicWords.DisplayedWords] = new()
                {
                    // English: What word was on the display in the {1} stage of {0}?
                    // Example: What word was on the display in the first stage of Periodic Words?
                    Question = "Какое слово было на экране в {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SPerspectivePegs)] = new()
        {
            ModuleName = "Взгляде на колышках",
            Conjugation = Conjugation.во_PrepositiveMascNeuter,
            Questions = new()
            {
                [SPerspectivePegs.ColorSequence] = new()
                {
                    // English: What was the {1} color in the initial sequence in {0}?
                    // Example: What was the first color in the initial sequence in Perspective Pegs?
                    Question = "Какой цвет был {1}-м в начальной последовательности {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красный",
                        ["yellow"] = "Жёлтый",
                        ["green"] = "Зелёный",
                        ["blue"] = "Синий",
                        ["purple"] = "Фиолетовый",
                    },
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
                    Question = "Какая была {1}-я нажатая кнопка {0}?",
                    Answers = new()
                    {
                        ["Azure"] = "Azure",
                        ["Blue"] = "Blue",
                        ["Crimson"] = "Crimson",
                        ["Diamond"] = "Diamond",
                        ["Emerald"] = "Emerald",
                        ["Fuchsia"] = "Fuchsia",
                        ["Green"] = "Green",
                        ["Hazel"] = "Hazel",
                        ["Ice"] = "Ice",
                        ["Jade"] = "Jade",
                        ["Kiwi"] = "Kiwi",
                        ["Lime"] = "Lime",
                        ["Magenta"] = "Magenta",
                        ["Navy"] = "Navy",
                        ["Orange"] = "Orange",
                        ["Purple"] = "Purple",
                        ["Quartz"] = "Quartz",
                        ["Red"] = "Red",
                        ["Salmon"] = "Salmon",
                        ["Tan"] = "Tan",
                        ["Ube"] = "Ube",
                        ["Vibe"] = "Vibe",
                        ["White"] = "White",
                        ["Xotic"] = "Xotic",
                        ["Yellow"] = "Yellow",
                        ["Zen"] = "Zen",
                    },
                },
                [SPhosphorescence.Offset] = new()
                {
                    // English: What was the offset in {0}?
                    Question = "Какое было смещение {0}?",
                },
            },
        },

        [typeof(SPickupIdentification)] = new()
        {
            Questions = new()
            {
                [SPickupIdentification.Item] = new()
                {
                    // English: What pickup was shown in the {1} stage of {0}?
                    // Example: What pickup was shown in the first stage of Pickup Identification?
                    Question = "Какой предмет был показан на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SPictionary)] = new()
        {
            Questions = new()
            {
                [SPictionary.Code] = new()
                {
                    // English: What was the code in {0}?
                    Question = "Какой был код {0}?",
                },
            },
        },

        [typeof(SPie)] = new()
        {
            Questions = new()
            {
                [SPie.Digits] = new()
                {
                    // English: What was the {1} digit of the displayed number in {0}?
                    // Example: What was the first digit of the displayed number in Pie?
                    Question = "Какая была {1}-я цифра числа, показанного {0}?",
                },
            },
        },

        [typeof(SPieFlash)] = new()
        {
            Questions = new()
            {
                [SPieFlash.Digits] = new()
                {
                    // English: What number was not displayed in {0}?
                    Question = "Какое число не было показано {0}?",
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
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SPinkButton.Words] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the first word in Pink Button?
                    Question = "Какое было {1}-е слово {0}?",
                },
                [SPinkButton.Colors] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in Pink Button?
                    Question = "Какой был {1}-й цвет на {0}?",
                    Answers = new()
                    {
                        ["black"] = "black",
                        ["red"] = "red",
                        ["green"] = "green",
                        ["yellow"] = "yellow",
                        ["blue"] = "blue",
                        ["magenta"] = "magenta",
                        ["cyan"] = "cyan",
                        ["white"] = "white",
                    },
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
            Questions = new()
            {
                [SPixelCipher.Keyword] = new()
                {
                    // English: What was the keyword in {0}?
                    Question = "Какое было ключевое слово {0}?",
                },
            },
        },

        [typeof(SPlaceholderTalk)] = new()
        {
            Questions = new()
            {
                [SPlaceholderTalk.FirstPhrase] = new()
                {
                    // English: What was the first half of the first phrase in {0}?
                    Question = "Какая была первая половина первой фразы {0}?",
                },
                [SPlaceholderTalk.Ordinal] = new()
                {
                    // English: What was the last half of the first phrase in {0}?
                    Question = "Какая была вторая половина первой фразы {0}?",
                },
            },
        },

        [typeof(SPlacementRoulette)] = new()
        {
            Questions = new()
            {
                [SPlacementRoulette.Char] = new()
                {
                    // English: What was the character listed on the information display in {0}?
                    Question = "Какой персонаж присуствовал на экране {0}?",
                },
                [SPlacementRoulette.Track] = new()
                {
                    // English: What was the track listed on the information display in {0}?
                    Question = "Какая трасса присутствовала на экране {0}?",
                },
                [SPlacementRoulette.Vehicle] = new()
                {
                    // English: What was the vehicle listed on the information display in {0}?
                    Question = "Какая машина присутствовала на экране {0}?",
                },
            },
        },

        [typeof(SPlanets)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SPlanets.Strips] = new()
                {
                    // English: What was the color of the {1} strip (from the top) in {0}?
                    // Example: What was the color of the first strip (from the top) in Planets?
                    Question = "Какой был цвет у {1}-й полоски (начиная сверху) на {0}?",
                    Answers = new()
                    {
                        ["Aqua"] = "Aqua",
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Lime"] = "Lime",
                        ["Orange"] = "Orange",
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["White"] = "White",
                        ["Off"] = "Off",
                    },
                },
                [SPlanets.Planet] = new()
                {
                    // English: What was the planet shown in {0}?
                    Question = "Какая планета была показана на {0}?",
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
            ModuleName = "Поэзии",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SPoetry.Answers] = new()
                {
                    // English: What was the {1} correct answer you pressed in {0}?
                    // Example: What was the first correct answer you pressed in Poetry?
                    Question = "Какое было {1}-е правильное слово, которое вы нажали {0}?",
                },
            },
        },

        [typeof(SPointlessMachines)] = new()
        {
            Questions = new()
            {
                [SPointlessMachines.Flashes] = new()
                {
                    // English: What color flashed {1} in {0}?
                    // Example: What color flashed first in Pointless Machines?
                    Question = "Какого цвета была {1}-я вспышка {0}?",
                    Answers = new()
                    {
                        ["White"] = "Белый",
                        ["Purple"] = "Фиолетовый",
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                        ["Yellow"] = "Жёлтый",
                    },
                },
            },
        },

        [typeof(SPolygons)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SPolygons.Polygon] = new()
                {
                    // English: Which polygon was present on {0}?
                    Question = "Какой многоугольник присутствовал на {0}?",
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
                    Question = "Какая была начальная позиция {0}?",
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
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SPrimeEncryption.DisplayedValue] = new()
                {
                    // English: What was the number shown in {0}?
                    Question = "Какое число было показано на {0}?",
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
                    Question = "Where did the prisoner start in {0}?",
                },
                [SPrisonBreak.Defuser] = new()
                {
                    // English: Where did you start in {0}?
                    Question = "Где вы начали {0}?",
                },
            },
        },

        [typeof(SProbing)] = new()
        {
            ModuleName = "Прозвонке",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SProbing.Frequencies] = new()
                {
                    // English: What was the missing frequency in the {1} wire in {0}?
                    // Example: What was the missing frequency in the red-white wire in Probing?
                    Question = "Какая частота отсутствовала в {1} проводе {0}?",
                    Arguments = new()
                    {
                        ["red-white"] = "красно-белом",
                        ["yellow-black"] = "жёлто-чёрном",
                        ["green"] = "зелёном",
                        ["gray"] = "сером",
                        ["yellow-red"] = "красно-жёлтом",
                        ["red-blue"] = "красно-синем",
                    },
                },
            },
        },

        [typeof(SProceduralMaze)] = new()
        {
            Questions = new()
            {
                [SProceduralMaze.InitialSeed] = new()
                {
                    // English: What was the initial seed in {0}?
                    Question = "Какое было изначальное зерно {0}?",
                },
            },
        },

        [typeof(SPunctuationMarks)] = new()
        {
            Questions = new()
            {
                [SPunctuationMarks.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Какое было показанное число в {0}?",
                },
            },
        },

        [typeof(SPurpleArrows)] = new()
        {
            ModuleName = "Фиолетовых стрелках",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SPurpleArrows.Finish] = new()
                {
                    // English: What was the target word on {0}?
                    Question = "Какое было целевое слово {0}?",
                },
            },
        },

        [typeof(SPurpleButton)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SPurpleButton.Numbers] = new()
                {
                    // English: What was the {1} number in the cyclic sequence on {0}?
                    // Example: What was the first number in the cyclic sequence on Purple Button?
                    Question = "Какое было {1}-е число в зацикленной последовательности {0}?",
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
                    Question = "Какое было {1}-е число пазла {0}?",
                },
                [SPuzzleIdentification.Game] = new()
                {
                    // English: What game was the {1} puzzle in {0} from?
                    // Example: What game was the first puzzle in Puzzle Identification from?
                    Question = "Из какой игры был {1}-й пазл {0}?",
                    Answers = new()
                    {
                        ["Professor Layton and the Curious Village"] = "Professor Layton and the Curious Village",
                        ["Professor Layton and Pandora's Box"] = "Professor Layton and Pandora's Box",
                        ["Professor Layton and the Lost Future"] = "Professor Layton and the Lost Future",
                        ["Professor Layton and the Spectre's Call"] = "Professor Layton and the Spectre's Call",
                        ["Professor Layton and the Miracle Mask"] = "Professor Layton and the Miracle Mask",
                        ["Professor Layton and the Azran Legacy"] = "Professor Layton and the Azran Legacy",
                        ["Layton's Mystery Journey: Katrielle and the Millionaire's Conspiracy"] = "Layton's Mystery Journey: Katrielle and the Millionaire's Conspiracy",
                        ["Professor Layton vs. Phoenix Wright: Ace Attorney"] = "Professor Layton vs. Phoenix Wright: Ace Attorney",
                    },
                },
                [SPuzzleIdentification.Name] = new()
                {
                    // English: What was the {1} puzzle in {0}?
                    // Example: What was the first puzzle in Puzzle Identification?
                    Question = "Какой был {1}-й пазл {0}?",
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
            Questions = new()
            {
                [SQnA.Questions] = new()
                {
                    // English: What was the {1} question asked in {0}?
                    // Example: What was the first question asked in Q & A?
                    Question = "Какой был {1}-й вопрос {0}?",
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
            Questions = new()
            {
                [SQuantumPasswords.Word] = new()
                {
                    // English: Which word was used in {0}?
                    Question = "Какое слово было использовано {0}?",
                },
            },
        },

        [typeof(SQuantumTernaryConverter)] = new()
        {
            Questions = new()
            {
                [SQuantumTernaryConverter.Number] = new()
                {
                    // English: Which number was shown in {0}?
                    Question = "Какое число было показано {0}?",
                },
            },
        },

        [typeof(SQuaver)] = new()
        {
            Questions = new()
            {
                [SQuaver.Arrows] = new()
                {
                    // English: What was the {1} sequence’s answer in {0}?
                    // Example: What was the first sequence’s answer in Quaver?
                    Question = "Какой был {1}-й ответ последовательности {0}?",
                },
            },
        },

        [typeof(SQuestionMark)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SQuestionMark.FlashedSymbols] = new()
                {
                    // English: Which of these symbols was part of the flashing sequence in {0}?
                    Question = "Какой из этих символов был частью мигающей последовательности {0}?",
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
                    Question = "Какой был {1}-й цвет в основной последовательности {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красный",
                        ["blue"] = "Синий",
                        ["green"] = "Зелёный",
                        ["yellow"] = "Жёлтый",
                        ["white"] = "Белый",
                        ["black"] = "Чёрный",
                        ["orange"] = "Оранжевый",
                        ["pink"] = "Розовый",
                        ["purple"] = "Фиолетовый",
                        ["cyan"] = "Голубой",
                        ["brown"] = "Коричневый",
                    },
                },
                [SQuickArithmetic.PrimSecDigits] = new()
                {
                    // English: What was the {1} digit in the {2} sequence in {0}?
                    // Example: What was the first digit in the primary sequence in Quick Arithmetic?
                    Question = "Какое было {1}-е число в {2} последовательности {0}?",
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
            Questions = new()
            {
                [SQuintuples.Numbers] = new()
                {
                    // English: What was the {1} digit in the {2} slot in {0}?
                    // Example: What was the first digit in the first slot in Quintuples?
                    Question = "Какая была {1}-я цифра в {2}-м слоте {0}?",
                },
                [SQuintuples.Colors] = new()
                {
                    // English: What color was the {1} digit in the {2} slot in {0}?
                    // Example: What color was the first digit in the first slot in Quintuples?
                    Question = "Какого цвета была {1}-я цифра в {2}-м слоте {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красный",
                        ["blue"] = "Синий",
                        ["orange"] = "Оранжевый",
                        ["green"] = "Зелёный",
                        ["pink"] = "Розовый",
                    },
                },
                [SQuintuples.ColorCounts] = new()
                {
                    // English: How many numbers were {1} in {0}?
                    // Example: How many numbers were red in Quintuples?
                    Question = "Скольк было {1} чисел {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красных",
                        ["blue"] = "синих",
                        ["orange"] = "оранжевых",
                        ["green"] = "зелёных",
                        ["pink"] = "розовых",
                    },
                },
            },
        },

        [typeof(SQuiplash)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SQuiplash.Number] = new()
                {
                    // English: What number was shown on {0}?
                    Question = "Какое число было показано на {0}?",
                },
            },
        },

        [typeof(SQuizBuzz)] = new()
        {
            Questions = new()
            {
                [SQuizBuzz.StartingNumber] = new()
                {
                    // English: What was the number initially on the display in {0}?
                    Question = "Какое было исходное число на экране {0}?",
                },
            },
        },

        [typeof(SQwirkle)] = new()
        {
            Questions = new()
            {
                [SQwirkle.TilesPlaced] = new()
                {
                    // English: What tile did you place {1} in {0}?
                    // Example: What tile did you place first in Qwirkle?
                    Question = "Какую плитку вы положили {1}-й {0}?",
                },
            },
        },

        [typeof(SRaidingTemples)] = new()
        {
            Questions = new()
            {
                [SRaidingTemples.StartingCommonPool] = new()
                {
                    // English: How many jewels were in the starting common pool in {0}?
                    Question = "Сколько драгоценностей было в начальном общем схроне {0}?",
                },
            },
        },

        [typeof(SRailwayCargoLoading)] = new()
        {
            ModuleName = "Загрузке ЖД состава",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SRailwayCargoLoading.Cars] = new()
                {
                    // English: What was the {1} car in {0}?
                    // Example: What was the first car in Railway Cargo Loading?
                    Question = "Какой вагон был присоединён {1}-м {0}?",
                },
                [SRailwayCargoLoading.FreightTableRules] = new()
                {
                    // English: Which freight table rule {1} in {0}?
                    // Example: Which freight table rule was met in Railway Cargo Loading?
                    Question = "Какое правило из таблицы грузовых вагонов {1} {0}?",
                    Arguments = new()
                    {
                        ["was met"] = "было применено",
                        ["wasn’t met"] = "не было применено",
                    },
                },
            },
        },

        [typeof(SRainbowArrows)] = new()
        {
            Questions = new()
            {
                [SRainbowArrows.Number] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Какое число было показано {0}?",
                },
            },
        },

        [typeof(SRecoloredSwitches)] = new()
        {
            ModuleName = "Перекрашенных переключателей",
            Conjugation = Conjugation.GenitivePlural,
            Questions = new()
            {
                [SRecoloredSwitches.LedColors] = new()
                {
                    // English: What was the color of the {1} LED in {0}?
                    // Example: What was the color of the first LED in Recolored Switches?
                    Question = "Какой был цвет {1}-го светодиода {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красный",
                        ["green"] = "Зелёный",
                        ["blue"] = "Синий",
                        ["cyan"] = "Голубой",
                        ["orange"] = "Оранжевый",
                        ["purple"] = "Фиолетовый",
                        ["white"] = "Белый",
                    },
                },
            },
        },

        [typeof(SRecursivePassword)] = new()
        {
            Questions = new()
            {
                [SRecursivePassword.NonPasswordWords] = new()
                {
                    // English: Which of these words appeared, but was not the password, in {0}?
                    Question = "Какое из этих слов присутствовало, но не являлось верным ответом {0}?",
                },
                [SRecursivePassword.Password] = new()
                {
                    // English: What was the password in {0}?
                    Question = "Какой пароль был верным ответом {0}?",
                },
            },
        },

        [typeof(SRedArrows)] = new()
        {
            ModuleName = "Красных стрелках",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SRedArrows.StartNumber] = new()
                {
                    // English: What was the starting number in {0}?
                    Question = "Какое было начальное число {0}?",
                },
            },
        },

        [typeof(SRedButtont)] = new()
        {
            Questions = new()
            {
                [SRedButtont.Word] = new()
                {
                    // English: What was the word before “SUBMIT” in {0}?
                    Question = "Какое слово было перед 'SUBMIT' {0}?",
                },
            },
        },

        [typeof(SRedCipher)] = new()
        {
            Questions = new()
            {
                [SRedCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Red Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SRedHerring)] = new()
        {
            Questions = new()
            {
                [SRedHerring.FirstFlash] = new()
                {
                    // English: What was the first color flashed by {0}?
                    Question = "Какой был первый мигающий цвет {0}?",
                },
            },
        },

        [typeof(SReformedRoleReversal)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.NominativeMasculine,
            Questions = new()
            {
                [SReformedRoleReversal.Condition] = new()
                {
                    // English: Which condition was the solving condition in {0}?
                    Question = "На каком условии был обезврежен {0}?",
                    Answers = new()
                    {
                        ["second"] = "2-м",
                        ["third"] = "3-м",
                        ["4th"] = "4-м",
                        ["5th"] = "5-м",
                        ["6th"] = "6-м",
                        ["7th"] = "7-м",
                        ["8th"] = "8-м",
                    },
                },
                [SReformedRoleReversal.Wire] = new()
                {
                    // English: What color was the {1} wire in {0}?
                    // Example: What color was the first wire in Reformed Role Reversal?
                    Question = "Какого цвета был {1}-й провод {0}?",
                    Answers = new()
                    {
                        ["Navy"] = "Navy",
                        ["Lapis"] = "Lapis",
                        ["Blue"] = "Blue",
                        ["Sky"] = "Sky",
                        ["Teal"] = "Teal",
                        ["Plum"] = "Plum",
                        ["Violet"] = "Violet",
                        ["Purple"] = "Purple",
                        ["Magenta"] = "Magenta",
                        ["Lavender"] = "Lavender",
                    },
                },
            },
        },

        [typeof(SReGretBFiltering)] = new()
        {
            Questions = new()
            {
                [SReGretBFiltering.Operator] = new()
                {
                    // English: Which calculation was used for the {1} stage of {0}?
                    // Example: Which calculation was used for the first stage of ReGret-B Filtering?
                    Question = "Какой оператор был применён на {1}-м этапе {0}?",
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
                    Question = "Какая показанная цифра соответствовала решению {0}?",
                },
                [SRegularCrazyTalk.Modifier] = new()
                {
                    // English: What was the embellishment of the solution phrase in {0}?
                    Question = "Какое было дополнение у фразы решения {0}?",
                    Answers = new()
                    {
                        ["[PHRASE]"] = "[PHRASE]",
                        ["It says: [PHRASE]"] = "It says: [PHRASE]",
                        ["Quote: [PHRASE] End quote"] = "Quote: [PHRASE] End quote",
                        ["“[PHRASE]”"] = "“[PHRASE]”",
                        ["It says: “[PHRASE]”"] = "It says: “[PHRASE]”",
                        ["“It says: [PHRASE]”"] = "“It says: [PHRASE]”",
                    },
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
                    // English: What color was this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was this key in the first stage of Reordered Keys? (+ sprite)
                    Question = "What color was this key in the {1} stage of {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                    },
                },
                [SReorderedKeys.LabelColor] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was the label of this key in the first stage of Reordered Keys? (+ sprite)
                    Question = "What color was the label of this key in the {1} stage of {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                    },
                },
                [SReorderedKeys.Label] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What was the label of this key in the first stage of Reordered Keys? (+ sprite)
                    Question = "What color was the label of this key in the {1} stage of {0}?",
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
            Questions = new()
            {
                [SRetirement.Houses] = new()
                {
                    // English: Which one of these houses was on offer, but not chosen by Bob in {0}?
                    Question = "Какой из этих домов предлагался, но не был выбран Бобом {0}?",
                },
            },
        },

        [typeof(SReverseMorse)] = new()
        {
            Questions = new()
            {
                [SReverseMorse.Characters] = new()
                {
                    // English: What was the {1} character in the {2} message of {0}?
                    // Example: What was the first character in the first message of Reverse Morse?
                    Question = "Какой был {1}-й символ в {2}-м сообщении {0}?",
                },
            },
        },

        [typeof(SReversePolishNotation)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SReversePolishNotation.Character] = new()
                {
                    // English: What character was used in the {1} round of {0}?
                    // Example: What character was used in the first round of Reverse Polish Notation?
                    Question = "Какой символ был использован на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SRGBMaze)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SRGBMaze.Keys] = new()
                {
                    // English: Where was the {1} key in {0}?
                    // Example: Where was the red key in RGB Maze?
                    Question = "Где был {1} ключ {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красный",
                        ["green"] = "зелёный",
                        ["blue"] = "синий",
                    },
                },
                [SRGBMaze.Number] = new()
                {
                    // English: Which maze number was the {1} maze in {0}?
                    // Example: Which maze number was the red maze in RGB Maze?
                    Question = "Какой {1} лабиринт был {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красный",
                        ["green"] = "зелёный",
                        ["blue"] = "синий",
                    },
                },
                [SRGBMaze.Exit] = new()
                {
                    // English: What was the exit coordinate in {0}?
                    Question = "Какая была координата выхода из {0}?",
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
                    Question = "Какой был цвет {1}-го светодиода {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                        ["Cyan"] = "Cyan",
                        ["Yellow"] = "Yellow",
                        ["White"] = "White",
                    },
                },
            },
        },

        [typeof(SRhythms)] = new()
        {
            ModuleName = "Ритмах",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SRhythms.Color] = new()
                {
                    // English: What was the color in {0}?
                    Question = "Какого цвета был светодиод {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Синего",
                        ["Red"] = "Красного",
                        ["Green"] = "Зелёного",
                        ["Yellow"] = "Жёлтого",
                    },
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
            Questions = new()
            {
                [SRoboScanner.EmptyCell] = new()
                {
                    // English: Where was the empty cell in {0}?
                    Question = "Где была пустая ячейка {0}?",
                },
            },
        },

        [typeof(SRobotProgramming)] = new()
        {
            Questions = new()
            {
                [SRobotProgramming.Color] = new()
                {
                    // English: What was the color of the {1} robot in {0}?
                    // Example: What was the color of the first robot in Robot Programming?
                    Question = "Какого цвета был {1}-й робот {0}?",
                },
                [SRobotProgramming.Shape] = new()
                {
                    // English: What was the shape of the {1} robot in {0}?
                    // Example: What was the shape of the first robot in Robot Programming?
                    Question = "Какой формы был {1}-й робот {0}?",
                },
            },
        },

        [typeof(SRoger)] = new()
        {
            Questions = new()
            {
                [SRoger.Seed] = new()
                {
                    // English: What four-digit number was given in {0}?
                    Question = "Какое четырёхзначное число было дано {0}?",
                },
            },
        },

        [typeof(SRoleReversal)] = new()
        {
            Questions = new()
            {
                [SRoleReversal.Wires] = new()
                {
                    // English: How many {1} wires were there in {0}?
                    // Example: How many warm-colored wires were there in Role Reversal?
                    Question = "Сколько проводов, окрашенных в {1} было {0}?",
                    Arguments = new()
                    {
                        ["warm-colored"] = "тёплые цвета",
                        ["cold-colored"] = "холодные цвета",
                        ["primary-colored"] = "основные цвета",
                        ["secondary-colored"] = "вторичные цвета",
                    },
                },
                [SRoleReversal.Number] = new()
                {
                    // English: What was the number corresponding to the correct condition in {0}?
                    Question = "Какое был номер у верного условия {0}?",
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
            Questions = new()
            {
                [SRule.Number] = new()
                {
                    // English: What was the rule number in {0}?
                    Question = "Какой был номер правила {0}?",
                },
            },
        },

        [typeof(SRuleOfThree)] = new()
        {
            Questions = new()
            {
                [SRuleOfThree.Coordinates] = new()
                {
                    // English: What was the {1} coordinate of the {2} vertex in {0}?
                    // Example: What was the X coordinate of the red vertex in Rule of Three?
                    Question = "Какая была {1} координата у {2} оси {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красной",
                        ["yellow"] = "жёлтой",
                        ["blue"] = "синей",
                    },
                },
                [SRuleOfThree.Cycles] = new()
                {
                    // English: What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?
                    // Example: What was the position of the red sphere on the X axis in the first cycle in Rule of Three?
                    Question = "Где находилась {1} сфера на {2} оси в {3}-м цикле {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красная",
                        ["yellow"] = "жёлтая",
                        ["blue"] = "синяя",
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
                    Question = "Какая цифра была показана на {1} ромбе {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красном",
                        ["yellow"] = "жёлтом",
                        ["blue"] = "синем",
                    },
                },
                [SSafetySquare.SpecialRule] = new()
                {
                    // English: What was the special rule displayed on the white diamond in {0}?
                    Question = "Какое правило было показано на белом ромбе {0}?",
                    Answers = new()
                    {
                        ["No special rule"] = "No special rule",
                        ["Reacts with water"] = "Reacts with water",
                        ["Simple asphyxiant"] = "Simple asphyxiant",
                        ["Oxidizer"] = "Oxidizer",
                    },
                },
            },
        },

        [typeof(SSamsung)] = new()
        {
            Questions = new()
            {
                [SSamsung.AppPositions] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was Duolingo in Samsung?
                    Question = "Где было приложение {1} {0}?",
                },
            },
        },

        [typeof(SSaturn)] = new()
        {
            Questions = new()
            {
                [SSaturn.Goal] = new()
                {
                    // English: Where was the goal in {0}?
                    Question = "Где была цель {0}?",
                },
            },
        },

        [typeof(SSbemailSongs)] = new()
        {
            Questions = new()
            {
                [SSbemailSongs.Songs] = new()
                {
                    // English: What was the displayed song for stage {1} (hexadecimal) of {0}?
                    // Example: What was the displayed song for stage 01 (hexadecimal) of Sbemail Songs?
                    Question = "Какая песня была показана на этапе {1} (16-ричное число) {0}?",
                },
            },
            Discriminators = new()
            {
                [SSbemailSongs.Digits] = new()
                {
                    // English: the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)
                    // Example: the Sbemail Songs which displayed ‘Oh, who is the guy that…’ in stage 01 (hexadecimal)
                    Discriminator = "в Sbemail Songs, где на этапе {1} (16-ричное число) была показана песня {0}",
                },
            },
        },

        [typeof(SScavengerHunt)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SScavengerHunt.KeySquare] = new()
                {
                    // English: Which tile was correctly submitted in the first stage of {0}?
                    Question = "Какая плитка была верным ответом на первом этапе {0}?",
                },
                [SScavengerHunt.ColoredTiles] = new()
                {
                    // English: Which of these tiles was {1} in the first stage of {0}?
                    // Example: Which of these tiles was red in the first stage of Scavenger Hunt?
                    Question = "Какая из этих плиток была {1} на первом этапе {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красной",
                        ["green"] = "зелёной",
                        ["blue"] = "синей",
                    },
                },
            },
        },

        [typeof(SSchlagDenBomb)] = new()
        {
            Questions = new()
            {
                [SSchlagDenBomb.ContestantName] = new()
                {
                    // English: What was the contestant’s name in {0}?
                    Question = "Какое было имя у участника {0}?",
                },
                [SSchlagDenBomb.ContestantScore] = new()
                {
                    // English: What was the contestant’s score in {0}?
                    Question = "Какой был счёт у участника {0}?",
                },
                [SSchlagDenBomb.BombScore] = new()
                {
                    // English: What was the bomb’s score in {0}?
                    Question = "Какой был счёт у бомбы {0}?",
                },
            },
        },

        [typeof(SScramboozledEggain)] = new()
        {
            Questions = new()
            {
                [SScramboozledEggain.Word] = new()
                {
                    // English: What was the {1} encrypted word in {0}?
                    // Example: What was the first encrypted word in Scramboozled Eggain?
                    Question = "Какое было {1}-е зашифрованное слово {0}?",
                },
            },
        },

        [typeof(SScripting)] = new()
        {
            Questions = new()
            {
                [SScripting.VariableDataType] = new()
                {
                    // English: What was the submitted data type of the variable in {0}?
                    Question = "Какой был верный тип данных у переменной {0}?",
                },
            },
        },

        [typeof(SScrutinySquares)] = new()
        {
            Questions = new()
            {
                [SScrutinySquares.FirstDifference] = new()
                {
                    // English: What was the modified property of the first display in {0}?
                    Question = "Какое свойство отличалось на первом экране {0}?",
                    Answers = new()
                    {
                        ["Word"] = "Слово",
                        ["Color around word"] = "Цвет вокруг слова",
                        ["Color of background"] = "Цвет фона",
                        ["Color of word"] = "Цвет слова",
                    },
                },
            },
        },

        [typeof(SSeaShells)] = new()
        {
            ModuleName = "Морских ракушках",
            Conjugation = Conjugation.в_PrepositivePlural,
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
            Questions = new()
            {
                [SSemamorse.Color] = new()
                {
                    // English: What was the color of the display involved in the starting value in {0}?
                    Question = "Какого цвета светодиоды использовались в начальном значении {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["green"] = "Зелёного",
                        ["cyan"] = "Голубого",
                        ["indigo"] = "Индиго",
                        ["pink"] = "Розового",
                    },
                },
                [SSemamorse.Letters] = new()
                {
                    // English: What was the {1} letter involved in the starting value in {0}?
                    // Example: What was the Morse letter involved in the starting value in Semamorse?
                    Question = "Какая была буква {1}, использованная в начальном значении {0}?",
                    Arguments = new()
                    {
                        ["Morse"] = "Морзе",
                        ["semaphore"] = "семафора",
                    },
                },
            },
        },

        [typeof(SSequencyclopedia)] = new()
        {
            Questions = new()
            {
                [SSequencyclopedia.Sequence] = new()
                {
                    // English: What sequence was used in {0}?
                    Question = "Какая была последовательность {0}?",
                },
            },
        },

        [typeof(SSetTheory)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SSetTheory.Equations] = new()
                {
                    // English: What equation was shown in the {1} stage of {0}?
                    // Example: What equation was shown in the first stage of S.E.T. Theory?
                    Question = "Какое уравнение было показано на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SShapesAndBombs)] = new()
        {
            Questions = new()
            {
                [SShapesAndBombs.InitialLetter] = new()
                {
                    // English: What was the initial letter in {0}?
                    Question = "Какая была начальная буква {0}?",
                },
            },
        },

        [typeof(SShapeShift)] = new()
        {
            ModuleName = "Изменении формы",
            Questions = new()
            {
                [SShapeShift.InitialShape] = new()
                {
                    // English: What was the initial shape in {0}?
                    Question = "Какая была изначальная фигура {0}?",
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
                    Question = "Какого цвета был {1} маркер {0}?",
                    Answers = new()
                    {
                        ["White"] = "White",
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                        ["Magenta"] = "Magenta",
                        ["Green"] = "Green",
                    },
                    Arguments = new()
                    {
                        ["top-left"] = "верхний левый",
                        ["top-right"] = "верхний правый",
                        ["bottom-left"] = "нижний левый",
                        ["bottom-right"] = "нижний правый",
                    },
                },
            },
        },

        [typeof(SShiftingMaze)] = new()
        {
            Questions = new()
            {
                [SShiftingMaze.Seed] = new()
                {
                    // English: What was the seed in {0}?
                    Question = "Какое было зерно {0}?",
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
                    Question = "Какая фигура была показана {0}?",
                    Answers = new()
                    {
                        ["Go-Between"] = "Go-Between",
                        ["Pawn"] = "Pawn",
                        ["Side Mover"] = "Side Mover",
                        ["Vertical Mover"] = "Vertical Mover",
                        ["Bishop"] = "Bishop",
                        ["Rook"] = "Rook",
                        ["Dragon Horse"] = "Dragon Horse",
                        ["Dragon King"] = "Dragon King",
                        ["Lance"] = "Lance",
                        ["Reverse Chariot"] = "Reverse Chariot",
                        ["Blind Tiger"] = "Blind Tiger",
                        ["Ferocious Leopard"] = "Ferocious Leopard",
                        ["Copper General"] = "Copper General",
                        ["Silver General"] = "Silver General",
                        ["Gold General"] = "Gold General",
                        ["Drunk Elephant"] = "Drunk Elephant",
                        ["Kirin"] = "Kirin",
                        ["Phoenix"] = "Phoenix",
                        ["Queen"] = "Queen",
                        ["Flying Stag"] = "Flying Stag",
                        ["Flying Ox"] = "Flying Ox",
                        ["Free Boar"] = "Free Boar",
                        ["Whale"] = "Whale",
                        ["White Horse"] = "White Horse",
                        ["King"] = "King",
                        ["Prince"] = "Prince",
                        ["Horned Falcon"] = "Horned Falcon",
                        ["Soaring Eagle"] = "Soaring Eagle",
                        ["Lion"] = "Lion",
                    },
                },
            },
        },

        [typeof(SSignLanguage)] = new()
        {
            Questions = new()
            {
                [SSignLanguage.Word] = new()
                {
                    // English: What was the deciphered word in {0}?
                    Question = "Какое слово было расшифровано {0}?",
                },
            },
        },

        [typeof(SSillySlots)] = new()
        {
            ModuleName = "Однорукого бандита",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SSillySlots.Question] = new()
                {
                    // English: What was the {1} slot in the {2} stage in {0}?
                    // Example: What was the first slot in the first stage in Silly Slots?
                    Question = "Какой был {1}-й слот на {2}-м этапе {0}?",
                    Answers = new()
                    {
                        ["red bomb"] = "Красная бомба",
                        ["red cherry"] = "Красная вишня",
                        ["red coin"] = "Красная монета",
                        ["red grape"] = "Красная гроздь",
                        ["green bomb"] = "Зелёная бомба",
                        ["green cherry"] = "Зелёная вишня",
                        ["green coin"] = "Зелёная монета",
                        ["green grape"] = "Зелёная гроздь",
                        ["blue bomb"] = "Синяя бомба",
                        ["blue cherry"] = "Синяя вишня",
                        ["blue coin"] = "Синяя монета",
                        ["blue grape"] = "Синяя гроздь",
                    },
                },
            },
        },

        [typeof(SSiloAuthorization)] = new()
        {
            Questions = new()
            {
                [SSiloAuthorization.MessageType] = new()
                {
                    // English: What was the message type in {0}?
                    Question = "Какой был тип сообщения {0}?",
                },
                [SSiloAuthorization.EncryptedMessage] = new()
                {
                    // English: What was the {1} part of the encrypted message in {0}?
                    // Example: What was the first part of the encrypted message in Silo Authorization?
                    Question = "Какая была {1}-я часть зашифрованного сообщения {0}?",
                },
                [SSiloAuthorization.AuthCode] = new()
                {
                    // English: What was the received authentication code in {0}?
                    Question = "Какой код авторизации был получен {0}?",
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                    },
                },
            },
        },

        [typeof(SSimonSamples)] = new()
        {
            Questions = new()
            {
                [SSimonSamples.Samples] = new()
                {
                    // English: What were the call samples {1} of {0}?
                    // Example: What were the call samples played in the first stage of Simon Samples?
                    Question = "Какие семплы были {1} {0}?",
                    Arguments = new()
                    {
                        ["played in the first stage"] = "проиграны на 1-м этапе",
                        ["added in the second stage"] = "добавлены на 2-м этапе",
                        ["added in the third stage"] = "добавлены на 3-м этапе",
                    },
                },
            },
        },

        [typeof(SSimonSays)] = new()
        {
            ModuleName = "\"Саймон говорит\"",
            Questions = new()
            {
                [SSimonSays.Flash] = new()
                {
                    // English: What color flashed {1} in the final sequence in {0}?
                    // Example: What color flashed first in the final sequence in Simon Says?
                    Question = "Какой цвет горел {1}-м в последовательности {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красный",
                        ["yellow"] = "Жёлтый",
                        ["green"] = "Зелёный",
                        ["blue"] = "Синий",
                    },
                },
            },
        },

        [typeof(SSimonScrambles)] = new()
        {
            Questions = new()
            {
                [SSimonScrambles.Colors] = new()
                {
                    // English: What color flashed {1} in {0}?
                    // Example: What color flashed first in Simon Scrambles?
                    Question = "Какой цвет горел {1}-м {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Yellow"] = "Жёлтый",
                    },
                },
            },
        },

        [typeof(SSimonScreams)] = new()
        {
            ModuleName = "\"Саймон кричит\"",
            Questions = new()
            {
                [SSimonScreams.Flashing] = new()
                {
                    // English: Which color flashed {1} in the final sequence in {0}?
                    // Example: Which color flashed first in the final sequence in Simon Screams?
                    Question = "Какой цвет горел {1}-м в полной последовательности {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Purple"] = "Фиолетовый",
                    },
                },
                [SSimonScreams.RuleSimple] = new()
                {
                    // English: In which stage(s) of {0} was “{1}” the applicable rule?
                    // Example: In which stage(s) of Simon Screams was “a color flashed, then a color two away, then the first again” the applicable rule?
                    Question = "На каком(-их) этапе(-ах) {0} {1}?",
                    Answers = new()
                    {
                        ["first"] = "На 1-м",
                        ["second"] = "На 2-м",
                        ["third"] = "На 3-м",
                        ["first and second"] = "На 1-м и 2-м",
                        ["first and third"] = "На 1-м и 3-м",
                        ["second and third"] = "На 2-м и 3-м",
                        ["all of them"] = "На всех",
                    },
                    Arguments = new()
                    {
                        ["a color flashed, then a color two away, then the first again"] = "горела кнопка, затем другая через одну, а после снова первая",
                        ["a color flashed, then a color two away, then the one opposite that"] = "горела кнопка, затем другая через одну, а после кнопка, стоящая напротив второй",
                        ["a color flashed, then a color two away, then the one opposite the first"] = "горела кнопка, затем другая через одну, а после кнопка, стоящая напротив первой",
                        ["a color flashed, then an adjacent color, then the first again"] = "горела кнопка, затем соседняя, а после снова первая",
                        ["a color flashed, then another color, then the first"] = "горела кнопка, затем другая, а после снова первая",
                        ["a color flashed, then one adjacent, then the one opposite that"] = "горела кнопка, затем соседняя, а после кнопка, стоящая напротив этой (второй)",
                        ["a color flashed, then one adjacent, then the one opposite the first"] = "горела кнопка, затем соседняя, а после кнопка, стоящая напротив первой",
                        ["a color flashed, then the one opposite, then one adjacent to that"] = "горела кнопка, затем стоящая напротив неё, а после соседняя со второй",
                        ["a color flashed, then the one opposite, then one adjacent to the first"] = "горела кнопка, затем стоящая напротив неё, а после соседняя с первой",
                        ["a color flashed, then the one opposite, then the first again"] = "горела кнопка, затем стоящая напротив неё, а после снова первая",
                        ["every color flashed at least once"] = "каждая кнопка горела как минимум один раз",
                        ["exactly one color flashed exactly twice"] = "ровно одна кнопка горела ровно дважды",
                        ["exactly one color flashed more than once"] = "ровно одна кнопка горела более одного раза",
                        ["exactly two colors flashed exactly twice"] = "ровно 2 кнопки горели ровно дважды",
                        ["exactly two colors flashed more than once"] = "ровно 2 кнопки горели более одного раза",
                        ["no color flashed exactly twice"] = "ни одна кнопка не горела ровно дважды",
                        ["no color flashed more than once"] = "ни одна кнопка не горела более одного раза",
                        ["no two adjacent colors flashed in clockwise order"] = "никакие 2 соседних кнопки не горели по часовой стрелке",
                        ["no two adjacent colors flashed in counter-clockwise order"] = "никакие 2 соседних кнопки не горели против часовой стрелки",
                        ["no two colors two apart flashed in clockwise order"] = "никакие 2 кнопки, расположенные через одну, не горели по часовой стрелке",
                        ["no two colors two apart flashed in counter-clockwise order"] = "никакие 2 кнопки, расположенные через одну, не горели против часовой стрелки",
                        ["the colors flashing first and last are adjacent"] = "первая и последняя горящие кнопки были соседними",
                        ["the colors flashing first and last are different and not adjacent"] = "первая и последняя горящие кнопки были разными, но не соседними",
                        ["the colors flashing first and last are the same"] = "первая горящая кнопка была той же самой, что и последняя горящая",
                        ["the number of distinct colors that flashed is even"] = "количество различных горящих кнопок было чётным",
                        ["the number of distinct colors that flashed is odd"] = "количество различных горящих кнопок было нечётным",
                        ["there are at least three colors that didn’t flash"] = "было как минимум 3 негорящих кнопки",
                        ["there are exactly two colors that didn’t flash"] = "было ровно 2 негорящих кнопки",
                        ["there are two colors adjacent to each other that didn’t flash"] = "было 2 соседних негорящих кнопки",
                        ["there are two colors opposite each other that didn’t flash"] = "было 2 негорящих кнопки, стоящих друг напротив друга",
                        ["there are two colors two away from each other that didn’t flash"] = "было 2 негорящих кнопки, расположенных через одну",
                        ["there is exactly one color that didn’t flash"] = "была ровно 1 негорящая кнопка",
                        ["three adjacent colors did not flash"] = "было 3 соседних негорящих кнопки",
                        ["three adjacent colors flashed in clockwise order"] = "3 соседние кнопки горели по часовой стрелке",
                        ["three adjacent colors flashed in counter-clockwise order"] = "3 соседние кнопки горели против часовой стрелки",
                        ["three colors, each two apart, flashed in clockwise order"] = "3 кнопки, расположенные через одну, горели по часовой стрелке",
                        ["three colors, each two apart, flashed in counter-clockwise order"] = "3 кнопки, расположенные через одну, горели против часовой стрелки",
                        ["two adjacent colors flashed in clockwise order"] = "2 соседние кнопки горели по часовой стрелке",
                        ["two adjacent colors flashed in counter-clockwise order"] = "2 соседние кнопки горели против часовой стрелки",
                        ["two colors two apart flashed in clockwise order"] = "2 кнопки, расположенные через одну, горели по часовой стрелке",
                        ["two colors two apart flashed in counter-clockwise order"] = "2 кнопки, расположенные через одну, горели против часовой стрелки",
                    },
                },
                [SSimonScreams.RuleComplex] = new()
                {
                    // English: In which stage(s) of {0} was “{1} flashed out of {2}, {3}, and {4}” the applicable rule?
                    // Example: In which stage(s) of Simon Screams was “at most one color flashed out of Red, Orange, and Yellow” the applicable rule?
                    Question = "На каком(-их) этапе(-ах) {0} среди кнопок {2}, {3} и {4} цвета {1}?",
                    Answers = new()
                    {
                        ["first"] = "На 1-м",
                        ["second"] = "На 2-м",
                        ["third"] = "На 3-м",
                        ["first and second"] = "На 1-м и 2-м",
                        ["first and third"] = "На 1-м и 3-м",
                        ["second and third"] = "На 2-м и 3-м",
                        ["all of them"] = "На всех",
                    },
                    Arguments = new()
                    {
                        ["at most one color"] = "горела максимум одна",
                        ["Red"] = "красного",
                        ["Orange"] = "оранжевого",
                        ["Yellow"] = "жёлтого",
                        ["at least two colors"] = "горели как минимум две",
                        ["Green"] = "зелёного",
                        ["Blue"] = "синего",
                        ["Purple"] = "фиолетового",
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
                    Question = "Какой цвет горел {1}-м на {2}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Purple"] = "Purple",
                        ["Magenta"] = "Magenta",
                        ["Cyan"] = "Cyan",
                    },
                },
            },
        },

        [typeof(SSimonSends)] = new()
        {
            Questions = new()
            {
                [SSimonSends.ReceivedLetters] = new()
                {
                    // English: What was the {1} received letter in {0}?
                    // Example: What was the red received letter in Simon Sends?
                    Question = "Какая была {1} полученная буква {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красная",
                        ["green"] = "зелёная",
                        ["blue"] = "синяя",
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
                    Question = "Кто горел {1}-м на {2}-й подаче в {0}?",
                },
                [SSimonServes.Food] = new()
                {
                    // English: Which item was not served in course {1} of {0}?
                    // Example: Which item was not served in course 1 of Simon Serves?
                    Question = "Что не подавалось гостям на {1}-й подаче в {0}?",
                    Answers = new()
                    {
                        ["Baked Batterys"] = "Baked Batterys",
                        ["Bamboozling Waffles"] = "Bamboozling Waffles",
                        ["Big Boom Tortellini"] = "Big Boom Tortellini",
                        ["Blast Shrimps"] = "Blast Shrimps",
                        ["Blastwave Compote"] = "Blastwave Compote",
                        ["Bomb Brûlée"] = "Bomb Brûlée",
                        ["Boolean Waffles"] = "Boolean Waffles",
                        ["Boom Lager Beer"] = "Boom Lager Beer",
                        ["Caesar Salad"] = "Caesar Salad",
                        ["Centurion Wings"] = "Centurion Wings",
                        ["Colored Spare Ribs"] = "Colored Spare Ribs",
                        ["Cruelo Juice"] = "Cruelo Juice",
                        ["Defuse Juice"] = "Defuse Juice",
                        ["Defuse au Chocolat"] = "Defuse au Chocolat",
                        ["Deto Bull"] = "Deto Bull",
                        ["Edgework Toast"] = "Edgework Toast",
                        ["Forget Cocktail"] = "Forget Cocktail",
                        ["Forghetti Bombognese"] = "Forghetti Bombognese",
                        ["Indicator Tar Tar"] = "Indicator Tar Tar",
                        ["Morse Soup"] = "Morse Soup",
                        ["NATO Shrimps"] = "NATO Shrimps",
                        ["Not Ice Cream"] = "Not Ice Cream",
                        ["Omelette au Bombage"] = "Omelette au Bombage",
                        ["Simon’s Special Mix"] = "Simon’s Special Mix",
                        ["Solve Cake"] = "Solve Cake",
                        ["Status Light Rolls"] = "Status Light Rolls",
                        ["Strike Pie"] = "Strike Pie",
                        ["Tasha’s Drink"] = "Tasha’s Drink",
                        ["Ticking Timecakes"] = "Ticking Timecakes",
                        ["Veggie Blast Plate"] = "Veggie Blast Plate",
                        ["Wire Shake"] = "Wire Shake",
                        ["Wire Spaghetti"] = "Wire Spaghetti",
                    },
                },
            },
        },

        [typeof(SSimonShapes)] = new()
        {
            Questions = new()
            {
                [SSimonShapes.SubmittedShape] = new()
                {
                    // English: What was the shape submitted at the end of {0}?
                    Question = "Какая фигура была введена в конце {0}?",
                },
            },
        },

        [typeof(SSimonShouts)] = new()
        {
            Questions = new()
            {
                [SSimonShouts.FlashingLetter] = new()
                {
                    // English: Which letter flashed on the {1} button in {0}?
                    // Example: Which letter flashed on the top button in Simon Shouts?
                    Question = " Какая буква горела на {1} кнопке в {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхней",
                        ["left"] = "левой",
                        ["right"] = "правой",
                        ["bottom"] = "нижней",
                    },
                },
            },
        },

        [typeof(SSimonShrieks)] = new()
        {
            Questions = new()
            {
                [SSimonShrieks.FlashingButton] = new()
                {
                    // English: How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?
                    // Example: How many spaces clockwise from the arrow was the first flash in the final sequence in Simon Shrieks?
                    Question = "В скольки кнопках от стрелки (по часовой) была {1}-я вспышка в финальной последовательности в {0}?",
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
            Questions = new()
            {
                [SSimonSignals.ColorToShape] = new()
                {
                    // English: What shape was the {1} arrow in {0}?
                    // Example: What shape was the red arrow in Simon Signals?
                    Question = "Какой формы была {1} стрелка в {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красная",
                        ["green"] = "зелёная",
                        ["blue"] = "синяя",
                        ["gray"] = "серая",
                    },
                },
                [SSimonSignals.ColorToRotations] = new()
                {
                    // English: How many directions did the {1} arrow in {0} have?
                    // Example: How many directions did the red arrow in Simon Signals have?
                    Question = "Сколько направлений было у {1} стрелки в {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красной",
                        ["green"] = "зелёной",
                        ["blue"] = "синей",
                        ["gray"] = "серой",
                    },
                },
                [SSimonSignals.ShapeToColor] = new()
                {
                    // English: What color was the arrow with this shape in {0}? (+ sprite)
                    Question = "Какого цвета была стрелка этой формы в {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["green"] = "Зелёного",
                        ["blue"] = "Синего",
                        ["gray"] = "Серого",
                    },
                },
                [SSimonSignals.ShapeToRotations] = new()
                {
                    // English: How many directions did the arrow with this shape have in {0}? (+ sprite)
                    Question = "Сколько направлений было у стрелки с этой формой в {0}?",
                },
                [SSimonSignals.RotationsToColor] = new()
                {
                    // English: What color was the arrow with {1} possible directions in {0}?
                    // Example: What color was the arrow with 3 possible directions in Simon Signals?
                    Question = "Какого цвета была стрелка с {1}-мя возможными направлениями в {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["green"] = "Зелёного",
                        ["blue"] = "Синего",
                        ["gray"] = "Серого",
                    },
                },
                [SSimonSignals.RotationsToShape] = new()
                {
                    // English: What shape was the arrow with {1} possible directions in {0}?
                    // Example: What shape was the arrow with 3 possible directions in Simon Signals?
                    Question = "Какой формы была стрелка с {1}-мя возможными направлениями {0}?",
                },
            },
        },

        [typeof(SSimonSimons)] = new()
        {
            Questions = new()
            {
                [SSimonSimons.FlashingColors] = new()
                {
                    // English: What was the {1} flash in the final sequence in {0}?
                    // Example: What was the first flash in the final sequence in Simon Simons?
                    Question = "Какая была {1}-я вспышка в полной последовательности в {0}?",
                },
            },
        },

        [typeof(SSimonSings)] = new()
        {
            Questions = new()
            {
                [SSimonSings.Flashing] = new()
                {
                    // English: Which key’s color flashed {1} in the {2} stage of {0}?
                    // Example: Which key’s color flashed first in the first stage of Simon Sings?
                    Question = "Какой цвет кнопки горел {1}-м на {2}-м этапе {0}?",
                },
            },
        },

        [typeof(SSimonSmiles)] = new()
        {
            Questions = new()
            {
                [SSimonSmiles.Sounds] = new()
                {
                    // English: What sound did the {1} button press make in {0}?
                    // Example: What sound did the first button press make in Simon Smiles?
                    Question = "Как звук был у {1}-й кнопки {0}?",
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
                    Question = "Какой был цвет у {1}-й вспышки в {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                        ["Cyan"] = "Cyan",
                    },
                },
                [SSimonSmothers.Directions] = new()
                {
                    // English: What was the direction of the {1} flash in {0}?
                    // Example: What was the direction of the first flash in Simon Smothers?
                    Question = "Какое направление было у {1}-й вспышки в {0}?",
                    Answers = new()
                    {
                        ["Up"] = "Вверх",
                        ["Down"] = "Вниз",
                        ["Left"] = "Влево",
                        ["Right"] = "Вправо",
                    },
                },
            },
        },

        [typeof(SSimonSounds)] = new()
        {
            Questions = new()
            {
                [SSimonSounds.FlashingColors] = new()
                {
                    // English: Which sample button sounded {1} in the final sequence in {0}?
                    // Example: Which sample button sounded first in the final sequence in Simon Sounds?
                    Question = "Какая кнопка семпла звучала {1}-й в полной последовательности в {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["blue"] = "Синего",
                        ["yellow"] = "Жёлтого",
                        ["green"] = "Зелёного",
                    },
                },
            },
        },

        [typeof(SSimonSpeaks)] = new()
        {
            ModuleName = "\"Саймон общается\"",
            Questions = new()
            {
                [SSimonSpeaks.Positions] = new()
                {
                    // English: Which bubble flashed first in {0}?
                    Question = "Какое диалоговое облако горело первым {0}?",
                    Answers = new()
                    {
                        ["top-left"] = "Сверху слева",
                        ["top-middle"] = "Сверху посередине",
                        ["top-right"] = "Сверху справа",
                        ["middle-left"] = "Посередине слева",
                        ["middle-center"] = "Центральное",
                        ["middle-right"] = "Посередине справа",
                        ["bottom-left"] = "Снизу слева",
                        ["bottom-middle"] = "Снизу посередине",
                        ["bottom-right"] = "Снизу справа",
                    },
                },
                [SSimonSpeaks.Shapes] = new()
                {
                    // English: Which bubble flashed second in {0}?
                    Question = "Какое диалоговое облако загорелось вторым {0}?",
                },
                [SSimonSpeaks.Languages] = new()
                {
                    // English: Which language was the bubble that flashed third in {0} in?
                    Question = "Какого языка была надпись на третьем загоревшемся диалоговом облаке {0}?",
                },
                [SSimonSpeaks.Words] = new()
                {
                    // English: Which word was in the bubble that flashed fourth in {0}?
                    Question = "Какое слово было в четвёртом загоревшемся диалоговом окне {0}?",
                },
                [SSimonSpeaks.Colors] = new()
                {
                    // English: What color was the bubble that flashed fifth in {0}?
                    Question = "Какого цвета было пятое загоревшееся диалоговое окно {0}?",
                    Answers = new()
                    {
                        ["black"] = "Чёрный",
                        ["blue"] = "Синий",
                        ["green"] = "Зелёный",
                        ["cyan"] = "Голубой",
                        ["red"] = "Красный",
                        ["purple"] = "Фиолетовый",
                        ["yellow"] = "Жёлтый",
                        ["white"] = "Белый",
                        ["gray"] = "Серый",
                    },
                },
            },
        },

        [typeof(SSimonsStar)] = new()
        {
            Questions = new()
            {
                [SSimonsStar.Colors] = new()
                {
                    // English: Which color flashed {1} in {0}?
                    // Example: Which color flashed first in Simon’s Star?
                    Question = "Какой цвет горел {1}-м в {0}?",
                    Answers = new()
                    {
                        ["red"] = "красный",
                        ["yellow"] = "жёлтый",
                        ["green"] = "зелёный",
                        ["blue"] = "синий",
                        ["purple"] = "фиолетовый",
                    },
                },
            },
        },

        [typeof(SSimonStacks)] = new()
        {
            Questions = new()
            {
                [SSimonStacks.Colors] = new()
                {
                    // English: Which color flashed in the {1} stage of {0}?
                    // Example: Which color flashed in the first stage of Simon Stacks?
                    Question = "Какой цвет горел на {1}-м этапе в {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Yellow"] = "Жёлтый",
                    },
                },
            },
        },

        [typeof(SSimonStages)] = new()
        {
            ModuleName = "\"Саймон выступает\"",
            Questions = new()
            {
                [SSimonStages.Indicator] = new()
                {
                    // English: What color was the indicator in the {1} stage in {0}?
                    // Example: What color was the indicator in the first stage in Simon Stages?
                    Question = "Какого цвета был индикатор на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["blue"] = "Синего",
                        ["yellow"] = "Жёлтого",
                        ["orange"] = "Оранжевого",
                        ["magenta"] = "Маджента",
                        ["green"] = "Зелёного",
                        ["pink"] = "Розового",
                        ["lime"] = "Лаймового",
                        ["cyan"] = "Голубого",
                        ["white"] = "Белого",
                    },
                },
                [SSimonStages.Flashes] = new()
                {
                    // English: Which color flashed {1} in the {2} stage in {0}?
                    // Example: Which color flashed first in the first stage in Simon Stages?
                    Question = "Какой цвет горел {1}-м на {2}-м этапе {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красный",
                        ["blue"] = "Синий",
                        ["yellow"] = "Жёлтый",
                        ["orange"] = "Оранжевый",
                        ["magenta"] = "Пурпурный",
                        ["green"] = "Зелёный",
                        ["pink"] = "Розовый",
                        ["lime"] = "Лаймовый",
                        ["cyan"] = "Голубой",
                        ["white"] = "Белый",
                    },
                },
            },
        },

        [typeof(SSimonStates)] = new()
        {
            ModuleName = "\"Саймон утверждает\"",
            Questions = new()
            {
                [SSimonStates.Display] = new()
                {
                    // English: Which {1} in the {2} stage in {0}?
                    // Example: Which color(s) flashed in the first stage in Simon States?
                    Question = "Какой(-ие) цвет(а) {1} на {2}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Red, Yellow"] = "К, Ж",
                        ["Red, Green"] = "К, З",
                        ["Red, Blue"] = "К, С",
                        ["Yellow, Green"] = "Ж, З",
                        ["Yellow, Blue"] = "Ж, С",
                        ["Green, Blue"] = "З, С",
                        ["all 4"] = "Все 4",
                        ["none"] = "Никакой",
                    },
                    Arguments = new()
                    {
                        ["color(s) flashed"] = "горел(и)",
                        ["color(s) didn’t flash"] = "не горел(и)",
                    },
                },
            },
        },

        [typeof(SSimonStops)] = new()
        {
            Questions = new()
            {
                [SSimonStops.Colors] = new()
                {
                    // English: Which color flashed {1} in the output sequence in {0}?
                    // Example: Which color flashed first in the output sequence in Simon Stops?
                    Question = "Какой цвет горел {1}-м в последовательности вспышек в {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Orange"] = "Оранжевый",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Violet"] = "Фиолетовый",
                    },
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
                    Question = "Какой цвет {1} {2} в последовательности в {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                    },
                    Arguments = new()
                    {
                        ["flashed"] = "горел",
                        ["was among the colors flashed"] = "был среди цветов на этапе",
                    },
                },
            },
        },

        [typeof(SSimonSubdivides)] = new()
        {
            Questions = new()
            {
                [SSimonSubdivides.Button] = new()
                {
                    // English: What color was the button at this position in {0}? (+ sprite)
                    Question = "Какого цвета была кнопка на этой позиции в {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Синий",
                        ["Green"] = "Зелёный",
                        ["Red"] = "Красный",
                        ["Violet"] = "Фиолетовый",
                    },
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
                    Question = "Какая была {1}-я тема в {0}?",
                    Answers = new()
                    {
                        ["Boss"] = "Boss",
                        ["Cruel"] = "Cruel",
                        ["Faulty"] = "Faulty",
                        ["Lookalike"] = "Lookalike",
                        ["Puzzle"] = "Puzzle",
                        ["Simon"] = "Simon",
                        ["Time-Based"] = "Time-Based",
                        ["Translated"] = "Translated",
                    },
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
                    Question = "Где было {1} in {0}?",
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
            Questions = new()
            {
                [SSimultaneousSimons.Flash] = new()
                {
                    // English: What color flashed {1} on the {2} Simon in {0}?
                    // Example: What color flashed first on the first Simon in Simultaneous Simons?
                    Question = "Какой цвет горел {1}-м на {2}-м Саймоне в {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Синий",
                        ["Yellow"] = "Жёлтый",
                        ["Red"] = "Красный",
                        ["Green"] = "Зелёный",
                    },
                },
            },
        },

        [typeof(SSkewedSlots)] = new()
        {
            ModuleName = "Искажённых слотах",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SSkewedSlots.OriginalNumbers] = new()
                {
                    // English: What were the original numbers in {0}?
                    Question = "Какие были изначальные цифры {0}?",
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
                    // English: What color was this gem in {0}? (+ sprite)
                    Question = "Какого цвета был этот камень {0}?",
                    Answers = new()
                    {
                        ["Black"] = "Black",
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                        ["Cyan"] = "Cyan",
                        ["White"] = "White",
                    },
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
                    Question = "Какая раса присутствовала, но не являлась решением {0}?",
                    Answers = new()
                    {
                        ["Nord"] = "Nord",
                        ["Khajiit"] = "Khajiit",
                        ["Breton"] = "Breton",
                        ["Argonian"] = "Argonian",
                        ["Dunmer"] = "Dunmer",
                        ["Altmer"] = "Altmer",
                        ["Redguard"] = "Redguard",
                        ["Orc"] = "Orc",
                        ["Imperial"] = "Imperial",
                    },
                },
                [SSkyrim.Weapon] = new()
                {
                    // English: Which weapon was selectable, but not the solution, in {0}?
                    Question = "Какое оружие присутствовало, но не являлось решением {0}?",
                    Answers = new()
                    {
                        ["Axe of Whiterun"] = "Axe of Whiterun",
                        ["Dawnbreaker"] = "Dawnbreaker",
                        ["Windshear"] = "Windshear",
                        ["Blade of Woe"] = "Blade of Woe",
                        ["Firiniel’s End"] = "Firiniel’s End",
                        ["Bow of the Hunt"] = "Bow of the Hunt",
                        ["Volendrung"] = "Volendrung",
                        ["Chillrend"] = "Chillrend",
                        ["Mace of Molag Bal"] = "Mace of Molag Bal",
                    },
                },
                [SSkyrim.Enemy] = new()
                {
                    // English: Which enemy was selectable, but not the solution, in {0}?
                    Question = "Какой враг присутствовал, но не являлся решением {0}?",
                    Answers = new()
                    {
                        ["Alduin"] = "Alduin",
                        ["Blood Dragon"] = "Blood Dragon",
                        ["Cave Bear"] = "Cave Bear",
                        ["Dragon Priest"] = "Dragon Priest",
                        ["Draugr"] = "Draugr",
                        ["Draugr Overlord"] = "Draugr Overlord",
                        ["Frost Troll"] = "Frost Troll",
                        ["Frostbite Spider"] = "Frostbite Spider",
                        ["Mudcrab"] = "Mudcrab",
                    },
                },
                [SSkyrim.City] = new()
                {
                    // English: Which city was selectable, but not the solution, in {0}?
                    Question = "Какой город присутствовал, но не являлся решением {0}?",
                    Answers = new()
                    {
                        ["Dawnstar"] = "Dawnstar",
                        ["Ivarstead"] = "Ivarstead",
                        ["Markarth"] = "Markarth",
                        ["Riverwood"] = "Riverwood",
                        ["Rorikstead"] = "Rorikstead",
                        ["Solitude"] = "Solitude",
                        ["Whiterun"] = "Whiterun",
                        ["Windhelm"] = "Windhelm",
                        ["Winterhold"] = "Winterhold",
                    },
                },
                [SSkyrim.DragonShout] = new()
                {
                    // English: Which dragon shout was selectable, but not the solution, in {0}?
                    Question = "Какой крик дракона присутствовал, но не являлся решением {0}?",
                },
            },
        },

        [typeof(SSlowMath)] = new()
        {
            Questions = new()
            {
                [SSlowMath.LastLetters] = new()
                {
                    // English: What was the last triplet of letters in {0}?
                    Question = "Какие три буквы были последними {0}?",
                },
            },
        },

        [typeof(SSmallCircle)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SSmallCircle.Shift] = new()
                {
                    // English: How much did the sequence shift by in {0}?
                    Question = "На сколько сместилась последовательность на {0}?",
                },
                [SSmallCircle.Wedge] = new()
                {
                    // English: Which wedge made the different noise in the beginning of {0}?
                    Question = "Какой сегмент {0} издал другой звук в начале?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                        ["White"] = "White",
                        ["Black"] = "Black",
                    },
                },
                [SSmallCircle.Solution] = new()
                {
                    // English: Which color was {1} in the solution to {0}?
                    // Example: Which color was first in the solution to Small Circle?
                    Question = "Какой цвет был {1}-м в решении на {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                        ["White"] = "White",
                        ["Black"] = "Black",
                    },
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
            Questions = new()
            {
                [SSmashMarryKill.Category] = new()
                {
                    // English: In what category was {1} for {0}?
                    // Example: In what category was The Button for Smash, Marry, Kill?
                    Question = "В какой категории был {1} {0}?",
                },
                [SSmashMarryKill.Module] = new()
                {
                    // English: Which module was in the {1} category for {0}?
                    // Example: Which module was in the SMASH category for Smash, Marry, Kill?
                    Question = "Какой модуль был в {1} категории {0}?",
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
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SSnooker.Reds] = new()
                {
                    // English: How many red balls were there at the start of {0}?
                    Question = "Сколько красных шаров было в начале {0}?",
                },
            },
        },

        [typeof(SSnowflakes)] = new()
        {
            Questions = new()
            {
                [SSnowflakes.DisplayedSnowflakes] = new()
                {
                    // English: Which snowflake was on the {1} button of {0}?
                    // Example: Which snowflake was on the top button of Snowflakes?
                    Question = "Какая снежинка была на {1} кнопке {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхней",
                        ["right"] = "правой",
                        ["bottom"] = "нижней",
                        ["left"] = "левой",
                    },
                },
            },
        },

        [typeof(SSonicKnuckles)] = new()
        {
            Questions = new()
            {
                [SSonicKnuckles.Sounds] = new()
                {
                    // English: Which sound was played but not featured in the chosen zone in {0}?
                    Question = "Какой звук воспроизводился, но не присутствовал в выбранной зоне {0}?",
                },
                [SSonicKnuckles.Badnik] = new()
                {
                    // English: Which badnik was shown in {0}?
                    Question = "Какой бадник был показан {0}?",
                },
                [SSonicKnuckles.Monitor] = new()
                {
                    // English: Which monitor was shown in {0}?
                    Question = "Какой монитор был показан {0}?",
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
                    Question = "Какая была {1}-я картинка {0}?",
                },
                [SSonicTheHedgehog.Sounds] = new()
                {
                    // English: Which sound was played by the {1} screen on {0}?
                    // Example: Which sound was played by the Running Boots screen on Sonic the Hedgehog?
                    Question = "Какой звук воспроизводился на экране \"{1}\" {0}?",
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
            ModuleName = "Сортировке",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SSorting.LastSwap] = new()
                {
                    // English: What positions were the last swap used to solve {0}?
                    Question = "Какие позиции участвовали в последней замене чисел {0}?",
                },
            },
        },

        [typeof(SSouvenir)] = new()
        {
            ModuleName = "Сувенире",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SSouvenir.FirstQuestion] = new()
                {
                    // English: What was the first module asked about in the other Souvenir on this bomb?
                    Question = "О каком модуле был первый вопрос на другом Сувенире?",
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
            Questions = new()
            {
                [SSpaceTraders.MaxTax] = new()
                {
                    // English: What was the maximum tax amount per vessel in {0}?
                    Question = "Какой был максимальный налог за каждое судно {0}?",
                },
            },
        },

        [typeof(SSpellingBee)] = new()
        {
            ModuleName = "Правописании",
            Questions = new()
            {
                [SSpellingBee.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "Какое слово нужно было произнести {0}?",
                },
            },
        },

        [typeof(SSphere)] = new()
        {
            Questions = new()
            {
                [SSphere.Colors] = new()
                {
                    // English: What was the {1} flashed color in {0}?
                    // Example: What was the first flashed color in Sphere?
                    Question = "Какой цвет загорелся {1}-м {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красный",
                        ["blue"] = "Синий",
                        ["green"] = "Зелёный",
                        ["orange"] = "Оранжевый",
                        ["pink"] = "Розовый",
                        ["purple"] = "Фиолетовый",
                        ["grey"] = "Серый",
                        ["white"] = "Белый",
                    },
                },
            },
        },

        [typeof(SSplittingTheLoot)] = new()
        {
            ModuleName = "Разделении добычи",
            Questions = new()
            {
                [SSplittingTheLoot.ColoredBag] = new()
                {
                    // English: What bag was initially colored in {0}?
                    Question = "Какой мешок был изначально окрашен {0}?",
                },
            },
        },

        [typeof(SSpongebobBirthdayIdentification)] = new()
        {
            Questions = new()
            {
                [SSpongebobBirthdayIdentification.Children] = new()
                {
                    // English: Who was the {1} child displayed in {0}?
                    // Example: Who was the first child displayed in Spongebob Birthday Identification?
                    Question = "Кто был {1}-м показаным ребёнком {0}?",
                },
            },
        },

        [typeof(SStability)] = new()
        {
            Questions = new()
            {
                [SStability.LedColors] = new()
                {
                    // English: What was the color of the {1} lit LED in {0}?
                    // Example: What was the color of the first lit LED in Stability?
                    Question = "Какого цвета был {1}-й горящик светодиод {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Yellow"] = "Жёлтый",
                        ["Blue"] = "Синий",
                    },
                },
                [SStability.IdNumber] = new()
                {
                    // English: What was the identification number in {0}?
                    Question = "Какое было идентификационное число {0}?",
                },
            },
        },

        [typeof(SStableTimeSignatures)] = new()
        {
            Questions = new()
            {
                [SStableTimeSignatures.Signatures] = new()
                {
                    // English: What was the {1} time signature in {0}?
                    // Example: What was the first time signature in Stable Time Signatures?
                    Question = "Какая была {1}-я сигнатура времени {0}?",
                },
            },
        },

        [typeof(SStackedSequences)] = new()
        {
            Questions = new()
            {
                [SStackedSequences.Question] = new()
                {
                    // English: Which of these is the length of a sequence in {0}?
                    Question = "Который ответ является длиной последовательности {0}?",
                },
            },
        },

        [typeof(SStars)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SStars.Center] = new()
                {
                    // English: What was the digit in the center of {0}?
                    Question = "Какая цифра была в центре {0}?",
                },
            },
        },

        [typeof(SStarstruck)] = new()
        {
            Questions = new()
            {
                [SStarstruck.Star] = new()
                {
                    // English: Which star was present on {0}?
                    Question = "Какая звезда присутствовала {0}?",
                },
            },
        },

        [typeof(SStateOfAggregation)] = new()
        {
            Questions = new()
            {
                [SStateOfAggregation.Element] = new()
                {
                    // English: What was the element shown in {0}?
                    Question = "Какой элемент был отображён {0}?",
                },
            },
        },

        [typeof(SStellar)] = new()
        {
            Questions = new()
            {
                [SStellar.Letters] = new()
                {
                    // English: What was the {1} letter in {0}?
                    // Example: What was the Morse code letter in Stellar?
                    Question = "Какая была буква в {1} {0}?",
                    Arguments = new()
                    {
                        ["Morse code"] = "коде Морзе",
                        ["tap code"] = "коде стука",
                        ["Braille"] = "Браилле",
                    },
                },
            },
        },

        [typeof(SStroopsTest)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStroopsTest.QWord] = new()
                {
                    // English: What was the {1} submitted word in {0}?
                    // Example: What was the first submitted word in Stroop’s Test?
                    Question = "What was the {1} submitted word in {0}?",
                },
                [SStroopsTest.QColor] = new()
                {
                    // English: What was the {1} submitted word’s color in {0}?
                    // Example: What was the first submitted word’s color in Stroop’s Test?
                    Question = "What was the {1} submitted word’s color in {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                        ["White"] = "White",
                    },
                },
            },
            Discriminators = new()
            {
                [SStroopsTest.DWord] = new()
                {
                    // English: the Stroop’s Test whose {0} submitted word was “{1}”
                    // Example: the Stroop’s Test whose first submitted word was “red”
                    Discriminator = "the Stroop’s Test whose {0} submitted word was “{1}”",
                },
                [SStroopsTest.DColor] = new()
                {
                    // English: the Stroop’s Test whose {0} submitted word’s color was {1}
                    // Example: the Stroop’s Test whose first submitted word’s color was red
                    Discriminator = "the Stroop’s Test whose {0} submitted word’s color was {1}",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["yellow"] = "yellow",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["magenta"] = "magenta",
                        ["white"] = "white",
                    },
                },
            },
        },

        [typeof(SStupidSlots)] = new()
        {
            Questions = new()
            {
                [SStupidSlots.Values] = new()
                {
                    // English: What was the value of the {1} arrow in {0}?
                    // Example: What was the value of the top-left arrow in Stupid Slots?
                    Question = "Какое было значение {1} стрелки {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "верхней левой",
                        ["top-middle"] = "верхней средней",
                        ["top-right"] = "верхней правой",
                        ["bottom-left"] = "нижней левой",
                        ["bottom-middle"] = "нижней средней",
                        ["bottom-right"] = "нижней правой",
                    },
                },
            },
        },

        [typeof(SSubblyJubbly)] = new()
        {
            Questions = new()
            {
                [SSubblyJubbly.Substitutions] = new()
                {
                    // English: What was a substitution word in {0}?
                    Question = "На какое слово была замена {0}?",
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
                    Question = "Сколько подписчиков было у {1} {0}?",
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
            Questions = new()
            {
                [SSubway.Bread] = new()
                {
                    // English: Which bread did the customer ask for in {0}?
                    Question = "Какой хлеб попросил покупатель {0}?",
                },
                [SSubway.Items] = new()
                {
                    // English: Which of these was not asked for in {0}?
                    Question = "Что из этого покупатель не просил {0}?",
                },
            },
        },

        [typeof(SSugarSkulls)] = new()
        {
            Questions = new()
            {
                [SSugarSkulls.Skull] = new()
                {
                    // English: What skull was shown on the {1} square in {0}?
                    // Example: What skull was shown on the top square in Sugar Skulls?
                    Question = "Какой череп был показан на {1} квадрате {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["bottom-left"] = "нижнем левом",
                        ["bottom-right"] = "нижнем правом",
                    },
                },
                [SSugarSkulls.Availability] = new()
                {
                    // English: Which skull {1} present in {0}?
                    // Example: Which skull was present in Sugar Skulls?
                    Question = "Какой череп {1} {0}?",
                    Arguments = new()
                    {
                        ["was"] = "присутствовал",
                        ["was not"] = "отсутствовал",
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
                    // English: What was the colour of this cell in {0}? (+ sprite)
                    // Example: What was the colour of this cell in Suits and Colours? (+ sprite)
                    Question = "What was the colour of this cell in {0}?",
                },
                [SSuitsAndColours.Suit] = new()
                {
                    // English: What was the suit of this cell in {0}? (+ sprite)
                    // Example: What was the suit of this cell in Suits and Colours? (+ sprite)
                    Question = "What was the suit of this cell in {0}?",
                },
            },
        },

        [typeof(SSuperparsing)] = new()
        {
            Questions = new()
            {
                [SSuperparsing.Displayed] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "Какое слово было показано {0}?",
                },
            },
        },

        [typeof(SSUSadmin)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SSUSadmin.Security] = new()
                {
                    // English: Which security protocol was installed in {0}?
                    Question = "Какой протокол безопасности был установлен на {0}?",
                },
                [SSUSadmin.Version] = new()
                {
                    // English: What was the version number in {0}?
                    Question = "What was the version number in {0}?",
                },
            },
        },

        [typeof(SSwitch)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SSwitch.InitialColor] = new()
                {
                    // English: What color was the {1} LED on the {2} flip of {0}?
                    // Example: What color was the top LED on the first flip of Switch?
                    Question = "Какого цвета был {1} светодиод при {2}-м нажатии {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["orange"] = "Оранжевого",
                        ["yellow"] = "Жёлтого",
                        ["green"] = "Зелёного",
                        ["blue"] = "Синего",
                        ["purple"] = "Фиолетового",
                    },
                    Arguments = new()
                    {
                        ["top"] = "верхний",
                        ["bottom"] = "нижний",
                    },
                },
            },
        },

        [typeof(SSwitches)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SSwitches.InitialPosition] = new()
                {
                    // English: What was the initial position of the switches in {0}?
                    Question = "Какое было начальное положение {0}?",
                },
            },
        },

        [typeof(SSwitchingMaze)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SSwitchingMaze.Seed] = new()
                {
                    // English: What was the seed in {0}?
                    Question = "Какое было зерно у {0}?",
                },
                [SSwitchingMaze.Color] = new()
                {
                    // English: What was the starting maze color in {0}?
                    Question = "Какой был цвет начального {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                        ["Cyan"] = "Cyan",
                        ["Yellow"] = "Yellow",
                        ["Black"] = "Black",
                        ["White"] = "White",
                        ["Gray"] = "Gray",
                        ["Orange"] = "Orange",
                        ["Pink"] = "Pink",
                        ["Brown"] = "Brown",
                    },
                },
            },
        },

        [typeof(SSymbolCycle)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SSymbolCycle.SymbolCounts] = new()
                {
                    // English: How many symbols were cycling on the {1} screen in {0}?
                    // Example: How many symbols were cycling on the left screen in Symbol Cycle?
                    Question = "Сколько символов было на {1} экране {0}?",
                    Arguments = new()
                    {
                        ["left"] = "левом",
                        ["right"] = "правом",
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
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SSymbolicTasha.DirectionFlashes] = new()
                {
                    // English: Which button flashed {1} in the final sequence of {0}?
                    // Example: Which button flashed first in the final sequence of Symbolic Tasha?
                    Question = "Какая кнопка горела {1}-й в финальной последовательности {0}?",
                    Answers = new()
                    {
                        ["Top"] = "Верхняя",
                        ["Right"] = "Правая",
                        ["Bottom"] = "Нижняя",
                        ["Left"] = "Левая",
                    },
                },
                [SSymbolicTasha.ColorFlashes] = new()
                {
                    // English: Which button flashed {1} in the final sequence of {0}?
                    // Example: Which button flashed first in the final sequence of Symbolic Tasha?
                    Question = "Какая кнопка горела {1}-й в финальной последовательности {0}?",
                    Answers = new()
                    {
                        ["Pink"] = "Розовая",
                        ["Green"] = "Зелёная",
                        ["Yellow"] = "Жёлтая",
                        ["Blue"] = "Синяя",
                    },
                },
                [SSymbolicTasha.Symbols] = new()
                {
                    // English: Which symbol was on the {1} button in {0}?
                    // Example: Which symbol was on the top button in Symbolic Tasha?
                    Question = "Какой символ был на {1} кнопке {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхней",
                        ["right"] = "правой",
                        ["bottom"] = "нижней",
                        ["left"] = "левой",
                        ["blue"] = "синей",
                        ["green"] = "зелёной",
                        ["yellow"] = "жёлтой",
                        ["pink"] = "розовой",
                    },
                },
            },
        },

        [typeof(SSynapseSays)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
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
            Questions = new()
            {
                [SSync125_3.Word] = new()
                {
                    // English: What was displayed on the screen in the {1} stage of {0}?
                    // Example: What was displayed on the screen in the first stage of SYNC-125 [3]?
                    Question = "Что было на экране на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SSynonyms)] = new()
        {
            Questions = new()
            {
                [SSynonyms.Number] = new()
                {
                    // English: Which number was displayed on {0}?
                    Question = "Какое число было отображено {0}?",
                },
            },
        },

        [typeof(SSysadmin)] = new()
        {
            Questions = new()
            {
                [SSysadmin.FixedErrorCodes] = new()
                {
                    // English: What error code did you fix in {0}?
                    Question = "Какой код ошибки вы исправили {0}?",
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
                    Question = "Which card was {1} your partner in {0}?",
                    Answers = new()
                    {
                        ["1"] = "1",
                        ["2"] = "2",
                        ["3"] = "3",
                        ["4"] = "4",
                        ["5"] = "5",
                        ["6"] = "6",
                        ["7"] = "7",
                        ["8"] = "8",
                        ["9"] = "9",
                        ["10"] = "10",
                        ["11"] = "11",
                        ["12"] = "12",
                        ["13"] = "13",
                        ["backwards 3"] = "backwards 3",
                        ["backwards 4"] = "backwards 4",
                        ["backwards 5"] = "backwards 5",
                        ["single-step 6"] = "single-step 6",
                        ["single-step 7"] = "single-step 7",
                        ["8 or discard"] = "8 or discard",
                        ["9 or discard"] = "9 or discard",
                        ["10 or discard"] = "10 or discard",
                        ["Warrior"] = "Warrior",
                        ["Trickster"] = "Trickster",
                    },
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
                    Answers = new()
                    {
                        ["1"] = "1",
                        ["2"] = "2",
                        ["3"] = "3",
                        ["4"] = "4",
                        ["5"] = "5",
                        ["6"] = "6",
                        ["7"] = "7",
                        ["8"] = "8",
                        ["9"] = "9",
                        ["10"] = "10",
                        ["11"] = "11",
                        ["12"] = "12",
                        ["13"] = "13",
                        ["backwards 3"] = "backwards 3",
                        ["backwards 4"] = "backwards 4",
                        ["backwards 5"] = "backwards 5",
                        ["single-step 6"] = "single-step 6",
                        ["single-step 7"] = "single-step 7",
                        ["8 or discard"] = "8 or discard",
                        ["9 or discard"] = "9 or discard",
                        ["10 or discard"] = "10 or discard",
                        ["Warrior"] = "Warrior",
                        ["Trickster"] = "Trickster",
                    },
                },
            },
        },

        [typeof(STapCode)] = new()
        {
            Questions = new()
            {
                [STapCode.ReceivedWord] = new()
                {
                    // English: What was the received word in {0}?
                    Question = "Какое слово было передано {0}?",
                },
            },
        },

        [typeof(STashaSqueals)] = new()
        {
            Questions = new()
            {
                [STashaSqueals.Colors] = new()
                {
                    // English: What was the {1} flashed color in {0}?
                    // Example: What was the first flashed color in Tasha Squeals?
                    Question = "Какой цвет горел {1}-м {0}?",
                    Answers = new()
                    {
                        ["Pink"] = "Розовый",
                        ["Green"] = "Зелёный",
                        ["Yellow"] = "Жёлтый",
                        ["Blue"] = "Синий",
                    },
                },
            },
        },

        [typeof(STasqueManaging)] = new()
        {
            Questions = new()
            {
                [STasqueManaging.StartingPos] = new()
                {
                    // English: Where was the starting position in {0}?
                    Question = "Где была начальная позиция {0}?",
                },
            },
        },

        [typeof(STeaSet)] = new()
        {
            Questions = new()
            {
                [STeaSet.DisplayedIngredients] = new()
                {
                    // English: Which ingredient was displayed {1}, from left to right, in {0}?
                    // Example: Which ingredient was displayed first, from left to right, in Tea Set?
                    Question = "Какой ингридиент был показан {1}-м, слева направо {0}?",
                },
            },
        },

        [typeof(STechnicalKeypad)] = new()
        {
            Questions = new()
            {
                [STechnicalKeypad.DisplayedDigits] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "Какая была {1}-я отображённая цифра {0}?",
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
                    Question = "Какой был начальный цвет {1}-й кнопки на {2}-м этапе {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красный",
                        ["green"] = "Зелёный",
                        ["blue"] = "Синий",
                    },
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
                    Question = "Какой был {1} сплит {0}?",
                    Answers = new()
                    {
                        ["Goal Posts"] = "Goal Posts",
                        ["Cincinnati"] = "Cincinnati",
                        ["Woolworth Store"] = "Woolworth Store",
                        ["Lily"] = "Lily",
                        ["3-7 Split"] = "3-7 Split",
                        ["Cocked Hat"] = "Cocked Hat",
                        ["4-7-10 Split"] = "4-7-10 Split",
                        ["Big Four"] = "Big Four",
                        ["Greek Church"] = "Greek Church",
                        ["Big Five"] = "Big Five",
                        ["Big Six"] = "Big Six",
                        ["HOW"] = "HOW",
                    },
                    Arguments = new()
                    {
                        ["red"] = "красный",
                        ["green"] = "зелёный",
                        ["blue"] = "синий",
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
                    Question = "Какой цветной треугольник пульсировал {1}-м {0}?",
                    Answers = new()
                    {
                        ["orange"] = "orange",
                        ["lime"] = "lime",
                        ["jade"] = "jade",
                        ["azure"] = "azure",
                        ["violet"] = "violet",
                        ["rose"] = "rose",
                        ["grey"] = "grey",
                    },
                },
            },
        },

        [typeof(STextField)] = new()
        {
            ModuleName = "Поле из букв",
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [STextField.Display] = new()
                {
                    // English: What was the displayed letter in {0}?
                    Question = "Какая буква присутствовала на {0}?",
                },
            },
        },

        [typeof(SThinkingWires)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SThinkingWires.FirstWire] = new()
                {
                    // English: What was the position from top to bottom of the first wire needing to be cut in {0}?
                    Question = "Где находился первый провод который нужно было перерезать (сверху вниз) на {0}?",
                },
                [SThinkingWires.SecondWire] = new()
                {
                    // English: What color did the second valid wire to cut have to have in {0}?
                    Question = "Какой цвет был у второго верно порезаного провода {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                        ["White"] = "White",
                        ["Black"] = "Black",
                        ["Any"] = "Any",
                    },
                },
                [SThinkingWires.DisplayNumber] = new()
                {
                    // English: What was the display number in {0}?
                    Question = "Какое было число на экране {0}?",
                },
            },
        },

        [typeof(SThirdBase)] = new()
        {
            ModuleName = "\"А меня – Сава\"",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SThirdBase.Display] = new()
                {
                    // English: What was the display word in the {1} stage on {0}?
                    // Example: What was the display word in the first stage on Third Base?
                    Question = "Какое слово было на экране на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SThirtyDollarModule)] = new()
        {
            Questions = new()
            {
                [SThirtyDollarModule.Sounds] = new()
                {
                    // English: Which sound was used in {0}?
                    Question = "Какой звук был использован {0}?",
                },
            },
        },

        [typeof(STicTacToe)] = new()
        {
            ModuleName = "Крестиках-ноликах",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [STicTacToe.InitialState] = new()
                {
                    // English: What was on the {1} button at the start of {0}?
                    // Example: What was on the top-left button at the start of Tic Tac Toe?
                    Question = "Что было на {1} кнопке в начале игры {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "верхней левой",
                        ["top-middle"] = "верхней средней",
                        ["top-right"] = "верхней правой",
                        ["middle-left"] = "средней левой",
                        ["middle-center"] = "центральной",
                        ["middle-right"] = "средней правой",
                        ["bottom-left"] = "нижней левой",
                        ["bottom-middle"] = "нижней средней",
                        ["bottom-right"] = "нижней правой",
                    },
                },
            },
        },

        [typeof(STimeSignatures)] = new()
        {
            Questions = new()
            {
                [STimeSignatures.Signatures] = new()
                {
                    // English: What was the {1} time signature in {0}?
                    // Example: What was the first time signature in Time Signatures?
                    Question = "Какая была {1}-я сигнатура времени {0}?",
                },
            },
        },

        [typeof(STimezone)] = new()
        {
            Questions = new()
            {
                [STimezone.Cities] = new()
                {
                    // English: What was the {1} city in {0}?
                    // Example: What was the departure city in Timezone?
                    Question = "Какой был город {1} {0}?",
                    Arguments = new()
                    {
                        ["departure"] = "отправления",
                        ["destination"] = "прибытия",
                    },
                },
            },
        },

        [typeof(STipToe)] = new()
        {
            Questions = new()
            {
                [STipToe.SafeSquares] = new()
                {
                    // English: Which of these squares was safe in row {1} in {0}?
                    // Example: Which of these squares was safe in row 9 in Tip Toe?
                    Question = "Какой из этих квадратов был безопасным в {1}-м ряду {0}?",
                },
            },
        },

        [typeof(STopsyTurvy)] = new()
        {
            Questions = new()
            {
                [STopsyTurvy.Word] = new()
                {
                    // English: What was the word initially shown in {0}?
                    Question = "Какое было начальное слово {0}?",
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
                    Question = "Какое слово было передано {0}?",
                },
                [STouchTransmission.Order] = new()
                {
                    // English: In what order was the Braille read in {0}?
                    Question = "Какой порядок чтения был у Браилля {0}?",
                    Answers = new()
                    {
                        ["Standard Braille Order"] = "Standard Braille Order",
                        ["Individual Reading Order"] = "Individual Reading Order",
                        ["Merged Reading Order"] = "Merged Reading Order",
                        ["Chinese Reading Order"] = "Chinese Reading Order",
                    },
                },
            },
        },

        [typeof(STransmittedMorse)] = new()
        {
            Questions = new()
            {
                [STransmittedMorse.Message] = new()
                {
                    // English: What was the {1} received message in {0}?
                    // Example: What was the first received message in Transmitted Morse?
                    Question = "Какое было {1}-е полученное сообщение {0}?",
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
                    Question = "Какого цвета был {1}-й пульсирующий треугольник {0}?",
                    Answers = new()
                    {
                        ["black"] = "black",
                        ["red"] = "red",
                        ["green"] = "green",
                        ["yellow"] = "yellow",
                        ["blue"] = "blue",
                        ["magenta"] = "magenta",
                        ["cyan"] = "cyan",
                        ["white"] = "white",
                    },
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
                    Question = "Who was your closest ally in {0}?",
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
            Questions = new()
            {
                [STripleTerm.Passwords] = new()
                {
                    // English: Which of these was one of the passwords in {0}?
                    Question = "Что из этого было одним из паролей {0}?",
                },
            },
        },

        [typeof(STurtleRobot)] = new()
        {
            Questions = new()
            {
                [STurtleRobot.CodeLines] = new()
                {
                    // English: What was the {1} line you commented out in {0}?
                    // Example: What was the first line you commented out in Turtle Robot?
                    Question = "Какую строку вы закомментировали {1}-й {0}?",
                },
            },
        },

        [typeof(STwoBits)] = new()
        {
            ModuleName = "Двух битах",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [STwoBits.Response] = new()
                {
                    // English: What was the {1} correct query response from {0}?
                    // Example: What was the first correct query response from Two Bits?
                    Question = "Какой был ответ на {1}-й запрос {0}?",
                },
            },
        },

        [typeof(SUltimateCipher)] = new()
        {
            Questions = new()
            {
                [SUltimateCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Ultimate Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "среднем",
                        ["bottom"] = "нижнем",
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
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SUltracube.Rotations] = new()
                {
                    // English: What was the {1} rotation in {0}?
                    // Example: What was the first rotation in Ultracube?
                    Question = "Каким было {1}-е вращение {0}?",
                },
            },
        },

        [typeof(SUltraStores)] = new()
        {
            Questions = new()
            {
                [SUltraStores.SingleRotation] = new()
                {
                    // English: What was the {1} rotation in the {2} stage of {0}?
                    // Example: What was the first rotation in the first stage of UltraStores?
                    Question = "Какой был {1}-й поворот на {2}-м этапе {0}?",
                },
                [SUltraStores.MultiRotation] = new()
                {
                    // English: What was the {1} rotation in the {2} stage of {0}?
                    // Example: What was the first rotation in the first stage of UltraStores?
                    Question = "Какой был {1}-й поворот на {2}-м этапе {0}?",
                },
            },
        },

        [typeof(SUncoloredSquares)] = new()
        {
            ModuleName = "Неокрашенных квадратов",
            Conjugation = Conjugation.GenitivePlural,
            Questions = new()
            {
                [SUncoloredSquares.FirstStage] = new()
                {
                    // English: What was the {1} color in reading order used in the first stage of {0}?
                    // Example: What was the first color in reading order used in the first stage of Uncolored Squares?
                    Question = "Какой был {1}-й цвет в порядке чтения, использованный на первом этапе {0}?",
                    Answers = new()
                    {
                        ["White"] = "Белый",
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                        ["Green"] = "Зелёный",
                        ["Yellow"] = "Жёлтый",
                        ["Magenta"] = "Розовый",
                    },
                },
            },
        },

        [typeof(SUncoloredSwitches)] = new()
        {
            ModuleName = "Бесцветных переключателей",
            Conjugation = Conjugation.GenitivePlural,
            Questions = new()
            {
                [SUncoloredSwitches.InitialState] = new()
                {
                    // English: What was the initial state of the switches in {0}?
                    Question = "Какое было исходное состояние {0}?",
                },
                [SUncoloredSwitches.LedColors] = new()
                {
                    // English: What color was the {1} LED in reading order in {0}?
                    // Example: What color was the first LED in reading order in Uncolored Switches?
                    Question = "Какого цвета был {1}-й светодиод в порядке чтения {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["green"] = "Зелёного",
                        ["blue"] = "Синего",
                        ["turquoise"] = "Голубого",
                        ["orange"] = "Оранжевого",
                        ["purple"] = "Фиолетового",
                        ["white"] = "Белого",
                        ["black"] = "Чёрного",
                    },
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
                    Question = "What was the {1} displayed in the {2} sequence of {0}?",
                },
            },
        },

        [typeof(SUnfairCipher)] = new()
        {
            Questions = new()
            {
                [SUnfairCipher.Instructions] = new()
                {
                    // English: What was the {1} received instruction in {0}?
                    // Example: What was the first received instruction in Unfair Cipher?
                    Question = "Какая {1}-я инструкция была зашифрована {0}?",
                },
            },
        },

        [typeof(SUnfairsRevenge)] = new()
        {
            Questions = new()
            {
                [SUnfairsRevenge.Instructions] = new()
                {
                    // English: What was the {1} decrypted instruction in {0}?
                    // Example: What was the first decrypted instruction in Unfair’s Revenge?
                    Question = "Какая {1}-я инструкция была зашифрована {0}?",
                },
            },
        },

        [typeof(SUnicode)] = new()
        {
            Questions = new()
            {
                [SUnicode.SortedAnswer] = new()
                {
                    // English: What was the {1} submitted code in {0}?
                    // Example: What was the first submitted code in Unicode?
                    Question = "Какой был {1}-й отправленный ответ {0}?",
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
                    Answers = new()
                    {
                        ["Red 0"] = "Red 0",
                        ["Red 1"] = "Red 1",
                        ["Red 2"] = "Red 2",
                        ["Red 3"] = "Red 3",
                        ["Red 4"] = "Red 4",
                        ["Red 5"] = "Red 5",
                        ["Red 6"] = "Red 6",
                        ["Red 7"] = "Red 7",
                        ["Red 8"] = "Red 8",
                        ["Red 9"] = "Red 9",
                        ["Red +2"] = "Red +2",
                        ["Red Skip"] = "Red Skip",
                        ["Red Reverse"] = "Red Reverse",
                        ["Green 0"] = "Green 0",
                        ["Green 1"] = "Green 1",
                        ["Green 2"] = "Green 2",
                        ["Green 3"] = "Green 3",
                        ["Green 4"] = "Green 4",
                        ["Green 5"] = "Green 5",
                        ["Green 6"] = "Green 6",
                        ["Green 7"] = "Green 7",
                        ["Green 8"] = "Green 8",
                        ["Green 9"] = "Green 9",
                        ["Green +2"] = "Green +2",
                        ["Green Skip"] = "Green Skip",
                        ["Green Reverse"] = "Green Reverse",
                        ["Yellow 0"] = "Yellow 0",
                        ["Yellow 1"] = "Yellow 1",
                        ["Yellow 2"] = "Yellow 2",
                        ["Yellow 3"] = "Yellow 3",
                        ["Yellow 4"] = "Yellow 4",
                        ["Yellow 5"] = "Yellow 5",
                        ["Yellow 6"] = "Yellow 6",
                        ["Yellow 7"] = "Yellow 7",
                        ["Yellow 8"] = "Yellow 8",
                        ["Yellow 9"] = "Yellow 9",
                        ["Yellow +2"] = "Yellow +2",
                        ["Yellow Skip"] = "Yellow Skip",
                        ["Yellow Reverse"] = "Yellow Reverse",
                        ["Blue 0"] = "Blue 0",
                        ["Blue 1"] = "Blue 1",
                        ["Blue 2"] = "Blue 2",
                        ["Blue 3"] = "Blue 3",
                        ["Blue 4"] = "Blue 4",
                        ["Blue 5"] = "Blue 5",
                        ["Blue 6"] = "Blue 6",
                        ["Blue 7"] = "Blue 7",
                        ["Blue 8"] = "Blue 8",
                        ["Blue 9"] = "Blue 9",
                        ["Blue +2"] = "Blue +2",
                        ["Blue Skip"] = "Blue Skip",
                        ["Blue Reverse"] = "Blue Reverse",
                        ["+4"] = "+4",
                        ["Wild"] = "Wild",
                    },
                },
            },
        },

        [typeof(SUnorderedKeys)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SUnorderedKeys.KeyColor] = new()
                {
                    // English: What color was this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "Какого цвета была эта клавиша на {1}-м этапе {0}?",
                },
                [SUnorderedKeys.LabelColor] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was the label of this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "Какого цвета была надпись на этой клавише на {1}-м этапе {0}?",
                },
                [SUnorderedKeys.Label] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What was the label of this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "Какого цвета была надпись на этой клавише на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SUnownCipher)] = new()
        {
            Questions = new()
            {
                [SUnownCipher.Answers] = new()
                {
                    // English: What was the {1} submitted letter in {0}?
                    // Example: What was the first submitted letter in Unown Cipher?
                    Question = "Какая буква была отправлена {1}-й {0}?",
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
                    // English: What was the color of this square in {0}? (+ sprite)
                    Question = "What was the color of this square in {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Jade"] = "Jade",
                        ["Azure"] = "Azure",
                        ["Violet"] = "Violet",
                    },
                },
            },
        },

        [typeof(SUpdog)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SUpdog.Word] = new()
                {
                    // English: What was the text on {0}?
                    Question = "Какой был текст на {0}?",
                },
                [SUpdog.Color] = new()
                {
                    // English: What was the {1} color in the sequence on {0}?
                    // Example: What was the first color in the sequence on Updog?
                    Question = "Какой был {1} цвет в последовательности {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Yellow"] = "Жёлтый",
                        ["Orange"] = "Оранжевый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Purple"] = "Фиолетовый",
                    },
                    Arguments = new()
                    {
                        ["first"] = "первый",
                        ["last"] = "последний",
                    },
                },
            },
        },

        [typeof(SUSACycle)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SUSACycle.Displayed] = new()
                {
                    // English: Which state was displayed in {0}?
                    Question = "Какой штат был показан на {0}?",
                },
            },
        },

        [typeof(SUSAMaze)] = new()
        {
            ModuleName = "Американском лабиринте",
            Questions = new()
            {
                [SUSAMaze.Origin] = new()
                {
                    // English: Which state did you depart from in {0}?
                    Question = "Из какого штата вы отправились {0}?",
                },
            },
        },

        [typeof(SV)] = new()
        {
            Questions = new()
            {
                [SV.Words] = new()
                {
                    // English: Which word {1} shown in {0}?
                    // Example: Which word was shown in V?
                    Question = "Какое слово {1} показано {0}?",
                    Arguments = new()
                    {
                        ["was"] = "было",
                        ["was not"] = "не было",
                    },
                },
            },
        },

        [typeof(SValves)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SValves.InitialState] = new()
                {
                    // English: What was the initial state of {0}?
                    Question = "Какое было начальное состояние {0}?",
                },
            },
        },

        [typeof(SVaricoloredSquares)] = new()
        {
            ModuleName = "Разноцветных квадратах",
            Conjugation = Conjugation.PrepositivePlural,
            Questions = new()
            {
                [SVaricoloredSquares.InitialColor] = new()
                {
                    // English: What was the initially pressed color on {0}?
                    Question = "Какой был первый нажатый цвет на {0}?",
                    Answers = new()
                    {
                        ["White"] = "Белый",
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                        ["Green"] = "Зелёный",
                        ["Yellow"] = "Жёлтый",
                        ["Magenta"] = "Розовый",
                    },
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
                    Question = "Какое было слово у {1}-й цели {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                        ["White"] = "White",
                    },
                },
                [SVaricolourFlash.Colors] = new()
                {
                    // English: What was the color of the {1} goal in {0}?
                    // Example: What was the color of the first goal in Varicolour Flash?
                    Question = "Какой был цвет у {1}-й цели {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                        ["White"] = "White",
                    },
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
                    Question = "Каким цветом горел светодиод {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красным",
                        ["Yellow"] = "Жёлтым",
                        ["Blue"] = "Синим",
                        ["White"] = "Белым",
                        ["Black"] = "Чёрным",
                    },
                },
                [SVariety.DigitDisplay] = new()
                {
                    // English: What digit was displayed, but not the answer, for the digit display in {0}?
                    Question = "Какая цифра была показана на цифровом экране, но не была для него ответом {0}?",
                },
                [SVariety.LetterDisplay] = new()
                {
                    // English: What word could be formed, but was not the answer, for the letter display in {0}?
                    Question = "Какое слово могло быть составленно на буквенном экране, но не было для него ответом {0}?",
                },
                [SVariety.Timer] = new()
                {
                    // English: What was the maximum display for the {1} in {0}?
                    // Example: What was the maximum display for the timer in Variety?
                    Question = "Какой был максимальный экран на {1}таймере {0}?",
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
                    Question = "Чему было равно n у {1}ручки {0}?",
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
                    Question = "Чему было равно n у {1}лампочки {0}?",
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
                    // Example: the Variety that has one
                    Discriminator = "the Variety that has {0}",
                    Arguments = new()
                    {
                        ["one\uE003 (LED)"] = "one",
                        ["one\uE003 (digit display)"] = "one",
                        ["one\uE003 (letter display)"] = "one",
                        ["one\uE003 (timer)"] = "one",
                        ["one\uE003 (knob)"] = "one",
                        ["one\uE003 (colored knob)"] = "one",
                        ["one\uE003 (redknob)"] = "one",
                        ["one\uE003 (yellowknob)"] = "one",
                        ["one\uE003 (blueknob)"] = "one",
                        ["one\uE003 (blackknob)"] = "one",
                        ["one\uE003 (bulb)"] = "one",
                        ["one\uE003 (redbulb)"] = "one",
                        ["one\uE003 (yellowbulb)"] = "one",
                        ["a knob"] = "a knob",
                        ["a colored knob"] = "a colored knob",
                        ["a white knob"] = "a white knob",
                        ["a red knob"] = "a red knob",
                        ["a black knob"] = "a black knob",
                        ["a blue knob"] = "a blue knob",
                        ["a yellow knob"] = "a yellow knob",
                        ["a keypad"] = "a keypad",
                        ["a white keypad"] = "a white keypad",
                        ["a red keypad"] = "a red keypad",
                        ["a yellow keypad"] = "a yellow keypad",
                        ["a blue keypad"] = "a blue keypad",
                        ["a slider"] = "a slider",
                        ["a horizontal slider"] = "a horizontal slider",
                        ["a vertical slider"] = "a vertical slider",
                        ["an LED"] = "an LED",
                        ["a digit display"] = "a digit display",
                        ["a wire"] = "a wire",
                        ["a black wire"] = "a black wire",
                        ["a blue wire"] = "a blue wire",
                        ["a red wire"] = "a red wire",
                        ["a yellow wire"] = "a yellow wire",
                        ["a white wire"] = "a white wire",
                        ["a button"] = "a button",
                        ["a red button"] = "a red button",
                        ["a yellow button"] = "a yellow button",
                        ["a blue button"] = "a blue button",
                        ["a white button"] = "a white button",
                        ["a letter display"] = "a letter display",
                        ["a Braille display"] = "a Braille display",
                        ["a key-in-lock"] = "a key-in-lock",
                        ["a switch"] = "a switch",
                        ["a red switch"] = "a red switch",
                        ["a yellow switch"] = "a yellow switch",
                        ["a blue switch"] = "a blue switch",
                        ["a white switch"] = "a white switch",
                        ["a timer"] = "a timer",
                        ["an ascending timer"] = "an ascending timer",
                        ["a descending timer"] = "a descending timer",
                        ["a die"] = "a die",
                        ["a light-on-dark die"] = "a light-on-dark die",
                        ["a dark-on-light die"] = "a dark-on-light die",
                        ["a bulb"] = "a bulb",
                        ["a red bulb"] = "a red bulb",
                        ["a yellow bulb"] = "a yellow bulb",
                        ["a maze"] = "a maze",
                        ["a 3×3 maze"] = "a 3×3 maze",
                        ["a 3×4 maze"] = "a 3×4 maze",
                        ["a 4×3 maze"] = "a 4×3 maze",
                        ["a 4×4 maze"] = "a 4×4 maze",
                    },
                },
            },
        },

        [typeof(SVcrcs)] = new()
        {
            Questions = new()
            {
                [SVcrcs.Word] = new()
                {
                    // English: What was the word in {0}?
                    Question = "Какое было слово {0}?",
                },
            },
        },

        [typeof(SVectors)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SVectors.Colors] = new()
                {
                    // English: What was the color of the {1} vector in {0}?
                    // Example: What was the color of the first vector in Vectors?
                    Question = "Какого цвета был {1} вектор из {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Orange"] = "Оранжевого",
                        ["Yellow"] = "Жёлтого",
                        ["Green"] = "Зелёного",
                        ["Blue"] = "Синего",
                        ["Purple"] = "Фиолетового",
                    },
                    Arguments = new()
                    {
                        ["first"] = "1-й",
                        ["second"] = "2-й",
                        ["third"] = "3-й",
                        ["only"] = "единственный",
                    },
                },
            },
        },

        [typeof(SVexillology)] = new()
        {
            Questions = new()
            {
                [SVexillology.Colors] = new()
                {
                    // English: What was the {1} flagpole color on {0}?
                    // Example: What was the first flagpole color on Vexillology?
                    Question = "Какого цвета был {1}-й флагшток {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Orange"] = "Оранжевого",
                        ["Green"] = "Зелёного",
                        ["Yellow"] = "Жёлтого",
                        ["Blue"] = "Синего",
                        ["Aqua"] = "Аква",
                        ["White"] = "Белого",
                        ["Black"] = "Чёрного",
                    },
                },
            },
        },

        [typeof(SVioletCipher)] = new()
        {
            Questions = new()
            {
                [SVioletCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Violet Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SVisualImpairment)] = new()
        {
            ModuleName = "Повреждённого зрения",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SVisualImpairment.Colors] = new()
                {
                    // English: What was the desired color in the {1} stage on {0}?
                    // Example: What was the desired color in the first stage on Visual Impairment?
                    Question = "Какой был целевой цвет на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Синий",
                        ["Green"] = "Зелёный",
                        ["Red"] = "Красный",
                        ["White"] = "Белый",
                    },
                },
            },
        },

        [typeof(SWalkingCube)] = new()
        {
            Questions = new()
            {
                [SWalkingCube.Path] = new()
                {
                    // English: Which of these cells was part of the cube’s path in {0}?
                    Question = "Какая из этих клеток была частью пути кубика {0}?",
                },
            },
        },

        [typeof(SWarningSigns)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SWarningSigns.DisplayedSign] = new()
                {
                    // English: What was the displayed sign in {0}?
                    Question = "Какой знак был показан на {0}?",
                },
            },
        },

        [typeof(SWasd)] = new()
        {
            Questions = new()
            {
                [SWasd.DisplayedLocation] = new()
                {
                    // English: What was the location displayed in {0}?
                    Question = "Какая локация была показана {0}?",
                },
            },
        },

        [typeof(SWatchingPaintDry)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SWatchingPaintDry.StrokeCount] = new()
                {
                    // English: How many brush strokes were heard in {0}?
                    Question = "Сколько мазков кистью было слышно на {0}?",
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
                    Question = "Какой был верный узор на {1}-м этапе {0}?",
                },
                [SWavetapping.Colors] = new()
                {
                    // English: What was the color on the {1} stage in {0}?
                    // Example: What was the color on the first stage in Wavetapping?
                    Question = "Какой цвет был на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Orange-Yellow"] = "Orange-Yellow",
                        ["Chartreuse"] = "Chartreuse",
                        ["Lime"] = "Lime",
                        ["Green"] = "Green",
                        ["Seafoam Green"] = "Seafoam Green",
                        ["Cyan-Green"] = "Cyan-Green",
                        ["Turquoise"] = "Turquoise",
                        ["Dark Blue"] = "Dark Blue",
                        ["Indigo"] = "Indigo",
                        ["Purple"] = "Purple",
                        ["Purple-Magenta"] = "Purple-Magenta",
                        ["Magenta"] = "Magenta",
                        ["Pink"] = "Pink",
                        ["Gray"] = "Gray",
                    },
                },
            },
        },

        [typeof(SWeakestLink)] = new()
        {
            Questions = new()
            {
                [SWeakestLink.Elimination] = new()
                {
                    // English: Who did you eliminate in {0}?
                    Question = "Кого вы устранили {0}?",
                },
                [SWeakestLink.MoneyPhaseName] = new()
                {
                    // English: Who made it to the Money Phase with you in {0}?
                    Question = "Кто дошёл до Money Phase с вами {0}?",
                },
                [SWeakestLink.Skill] = new()
                {
                    // English: What was {1}’s skill in {0}?
                    // Example: What was Annie’s skill in Weakest Link?
                    Question = "Какой навык был у {1} {0}?",
                },
                [SWeakestLink.Ratio] = new()
                {
                    // English: What ratio did {1} get in the Question Phase in {0}?
                    // Example: What ratio did Annie get in the Question Phase in Weakest Link?
                    Question = "На какой процент вопросов ответил(а) {1} в Question Phase {0}?",
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
                    Question = "Какой текст был на {1}-м этапе {0}?",
                },
                [SWhatsOnSecond.DisplayColor] = new()
                {
                    // English: What was the display text color in the {1} stage of {0}?
                    // Example: What was the display text color in the first stage of What’s on Second?
                    Question = "Какого цвета был текст на {1}-м этапе {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Blue",
                        ["Cyan"] = "Cyan",
                        ["Green"] = "Green",
                        ["Magenta"] = "Magenta",
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                    },
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
            Questions = new()
            {
                [SWhiteCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in White Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
                    },
                },
            },
        },

        [typeof(SWhoOF)] = new()
        {
            Questions = new()
            {
                [SWhoOF.Display] = new()
                {
                    // English: What was the display in the {1} stage on {0}?
                    // Example: What was the display in the first stage on WhoOF?
                    Question = "Что было на экране на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SWhosOnFirst)] = new()
        {
            ModuleName = "\"Меня зовут Авас, а Вас\"",
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SWhosOnFirst.Display] = new()
                {
                    // English: What was the display in the {1} stage on {0}?
                    // Example: What was the display in the first stage on Who’s on First?
                    Question = "Какое слово было на экране на {1}-м этапе {0}?",
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
                    Question = "What was the display in the {1} stage on {0}?",
                },
            },
        },

        [typeof(SWhosOnMorse)] = new()
        {
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SWhosOnMorse.TransmitDisplay] = new()
                {
                    // English: What word was transmitted in the {1} stage on {0}?
                    // Example: What word was transmitted in the first stage on Who’s on Morse?
                    Question = "Какое слово было передано на {1}-м этапе {0}?",
                },
            },
        },

        [typeof(SWire)] = new()
        {
            Questions = new()
            {
                [SWire.DialColors] = new()
                {
                    // English: What was the color of the {1} dial in {0}?
                    // Example: What was the color of the top dial in Wire?
                    Question = "Какого цвета был {1} диск {0}?",
                    Answers = new()
                    {
                        ["blue"] = "Синего",
                        ["green"] = "Зелёного",
                        ["grey"] = "Серого",
                        ["orange"] = "Оранжевого",
                        ["purple"] = "Фиолетового",
                        ["red"] = "Красного",
                    },
                    Arguments = new()
                    {
                        ["top"] = "верхний",
                        ["bottom-left"] = "нижний левый",
                        ["bottom-right"] = "нижний правый",
                    },
                },
                [SWire.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Какое было отображённое число {0}?",
                },
            },
        },

        [typeof(SWireOrdering)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SWireOrdering.DisplayColor] = new()
                {
                    // English: What color was the {1} display from the left in {0}?
                    // Example: What color was the first display from the left in Wire Ordering?
                    Question = "Какого цвета был {1}-й экран слева на {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["orange"] = "Оранжевого",
                        ["yellow"] = "Жёлтого",
                        ["green"] = "Зелёного",
                        ["blue"] = "Синего",
                        ["purple"] = "Фиолетового",
                        ["white"] = "Белого",
                        ["black"] = "Чёрного",
                    },
                },
                [SWireOrdering.DisplayNumber] = new()
                {
                    // English: What number was on the {1} display from the left in {0}?
                    // Example: What number was on the first display from the left in Wire Ordering?
                    Question = "Какое число было на {1}-м экране слева на {0}?",
                },
                [SWireOrdering.WireColor] = new()
                {
                    // English: What color was the {1} wire from the left in {0}?
                    // Example: What color was the first wire from the left in Wire Ordering?
                    Question = "Какого цвета был {1}-й провод слева на {0}?",
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["orange"] = "Оранжевого",
                        ["yellow"] = "Жёлтого",
                        ["green"] = "Зелёного",
                        ["blue"] = "Синего",
                        ["purple"] = "Фиолетового",
                        ["white"] = "Белого",
                        ["black"] = "Чёрного",
                    },
                },
            },
        },

        [typeof(SWireSequence)] = new()
        {
            ModuleName = "Последовательности проводов",
            Conjugation = Conjugation.в_PrepositiveFeminine,
            Questions = new()
            {
                [SWireSequence.ColorCount] = new()
                {
                    // English: How many {1} wires were there in {0}?
                    // Example: How many red wires were there in Wire Sequence?
                    Question = "Сколько было {1} проводов {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красных",
                        ["blue"] = "синих",
                        ["black"] = "чёрных",
                    },
                },
            },
        },

        [typeof(SWolfGoatAndCabbage)] = new()
        {
            Questions = new()
            {
                [SWolfGoatAndCabbage.Animals] = new()
                {
                    // English: Which of these was {1} on {0}?
                    // Example: Which of these was present on Wolf, Goat, and Cabbage?
                    Question = "Что из этого {1} {0}?",
                    Arguments = new()
                    {
                        ["present"] = "присутствовало",
                        ["not present"] = "отсутствовало",
                    },
                },
                [SWolfGoatAndCabbage.BoatSize] = new()
                {
                    // English: What was the boat size in {0}?
                    Question = "Какого размера была лодка {0}?",
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
            ModuleName = "Рабочем названии",
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SWorkingTitle.Label] = new()
                {
                    // English: What was the label shown in {0}?
                    Question = "Какая надпись была показана на {0}?",
                },
            },
        },

        [typeof(SWumbo)] = new()
        {
            Questions = new()
            {
                [SWumbo.Number] = new()
                {
                    // English: What was the number in {0}?
                    Question = "Какое было число {0}?",
                },
            },
        },

        [typeof(SXenocryst)] = new()
        {
            Questions = new()
            {
                [SXenocryst.Question] = new()
                {
                    // English: What was the color of the {1} flash in {0}?
                    // Example: What was the color of the first flash in Xenocryst?
                    Question = "Какого цвета была {1}-я вспышка {0}?",
                },
            },
        },

        [typeof(SXmORseCode)] = new()
        {
            Questions = new()
            {
                [SXmORseCode.Word] = new()
                {
                    // English: What word did you decrypt in {0}?
                    Question = "Какое слово вы расшифровали {0}?",
                },
                [SXmORseCode.DisplayedLetters] = new()
                {
                    // English: What was the {1} displayed letter (in reading order) in {0}?
                    // Example: What was the first displayed letter (in reading order) in XmORse Code?
                    Question = "Какая была {1}-я показанная буква (в порядке чтения) {0}?",
                },
            },
        },

        [typeof(SXobekuJehT)] = new()
        {
            Questions = new()
            {
                [SXobekuJehT.Song] = new()
                {
                    // English: What song was played on {0}?
                    Question = "Какая песня звучала {0}?",
                },
            },
        },

        [typeof(SXRing)] = new()
        {
            Questions = new()
            {
                [SXRing.Symbol] = new()
                {
                    // English: Which symbol was scanned in {0}?
                    Question = "Какой символ был просканирован {0}?",
                },
            },
        },

        [typeof(SXYRay)] = new()
        {
            Questions = new()
            {
                [SXYRay.Shapes] = new()
                {
                    // English: Which shape was scanned by {0}?
                    Question = "Какая фигура была просканирована {0}?",
                },
            },
        },

        [typeof(SYahtzee)] = new()
        {
            ModuleName = "Покере на костях",
            Questions = new()
            {
                [SYahtzee.InitialRoll] = new()
                {
                    // English: What was the initial roll on {0}?
                    Question = "Какой был первый бросок {0}?",
                    Answers = new()
                    {
                        ["Yahtzee"] = "Покер",
                        ["large straight"] = "Большой стрит",
                        ["small straight"] = "Малый стрит",
                        ["four of a kind"] = "Каре",
                        ["full house"] = "Фулл-хаус",
                        ["three of a kind"] = "Тройка",
                        ["two pairs"] = "Две пары",
                        ["pair"] = "Пара",
                    },
                },
            },
        },

        [typeof(SYellowArrows)] = new()
        {
            ModuleName = "Жёлтых стрелках",
            Conjugation = Conjugation.в_PrepositivePlural,
            Questions = new()
            {
                [SYellowArrows.StartingRow] = new()
                {
                    // English: What was the starting row letter in {0}?
                    Question = "Какая была буква у начальной строки {0}?",
                },
            },
        },

        [typeof(SYellowButton)] = new()
        {
            NeedsTranslation = true,
            Conjugation = Conjugation.GenitiveMascNeuter,
            Questions = new()
            {
                [SYellowButton.Colors] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in Yellow Button?
                    Question = "Какой был {1}-й цвет в последовательности {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Cyan"] = "Cyan",
                        ["Blue"] = "Blue",
                        ["Magenta"] = "Magenta",
                    },
                },
            },
        },

        [typeof(SYellowButtont)] = new()
        {
            Questions = new()
            {
                [SYellowButtont.Filename] = new()
                {
                    // English: What was the filename in {0}?
                    Question = "Какое было название файла {0}?",
                },
            },
        },

        [typeof(SYellowCipher)] = new()
        {
            Questions = new()
            {
                [SYellowCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Yellow Cipher?
                    Question = "Что было на {1} экране на {2}-й странице {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["middle"] = "центральном",
                        ["bottom"] = "нижнем",
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
                    Question = "Где был {1} квадрат {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красный",
                        ["green"] = "зелёный",
                        ["blue"] = "синий",
                    },
                },
                [SZeroZero.StarColors] = new()
                {
                    // English: What color was the {1} star in {0}?
                    // Example: What color was the top-left star in Zero, Zero?
                    Question = "Какого цвета была {1} звезда {0}?",
                    Answers = new()
                    {
                        ["black"] = "black",
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["cyan"] = "cyan",
                        ["red"] = "red",
                        ["magenta"] = "magenta",
                        ["yellow"] = "yellow",
                        ["white"] = "white",
                    },
                    Arguments = new()
                    {
                        ["top-left"] = "верхняя левая",
                        ["top-right"] = "верхняя правая",
                        ["bottom-left"] = "нижняя левая",
                        ["bottom-right"] = "нижняя правая",
                    },
                },
                [SZeroZero.StarPoints] = new()
                {
                    // English: How many points were on the {1} star in {0}?
                    // Example: How many points were on the top-left star in Zero, Zero?
                    Question = "Сколько вершин было у {1} звезды {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "верхней левой",
                        ["top-right"] = "верхней правой",
                        ["bottom-left"] = "нижней левой",
                        ["bottom-right"] = "нижней правой",
                    },
                },
            },
        },

        [typeof(SZoni)] = new()
        {
            Questions = new()
            {
                [SZoni.Words] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the first word in Zoni?
                    Question = "Какое было {1}-е расшифрованное слово {0}?",
                },
            },
        },

        [typeof(SÉpelleMoiÇa)] = new()
        {
            Conjugation = Conjugation.PrepositiveMascNeuter,
            Questions = new()
            {
                [SÉpelleMoiÇa.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "Какое слово нужно было написать на {0}?",
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
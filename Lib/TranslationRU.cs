using System;
using System.Collections.Generic;
using static Souvenir.Translation_ru.Conjugation;

namespace Souvenir;

public class Translation_ru : TranslationBase<TranslationInfo<Translation_ru.QuestionTranslationInfo_ru>>
{
    public sealed class QuestionTranslationInfo_ru : QuestionTranslationInfo
    {
        public string ModuleName;
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

    public override string FormatModuleName(SouvenirQuestionAttribute qAttr, bool addSolveCount, int numSolved)
    {
        if (_translations.Get(qAttr.Handler.EnumType) is not { } tr || tr.Questions.Get(qAttr.EnumValue) is not { } qtr)
            return base.FormatModuleName(qAttr, addSolveCount, numSolved);
        var moduleName = qtr.ModuleName ?? tr.ModuleName ?? qAttr.Handler.ModuleNameWithThe;
        return
            addSolveCount ? qtr.Conjugation switch
            {
                NominativeMasculine => $"{Ordinal(numSolved)}-й решённый {moduleName}",
                NominativeFeminine => $"{Ordinal(numSolved)}-я решённая {moduleName}",
                NominativeNeuter => $"{Ordinal(numSolved)}-е решённое {moduleName}",
                NominativePlural => $"{Ordinal(numSolved)}-е решённые {moduleName}",
                GenitiveMascNeuter => $"{Ordinal(numSolved)}-го решённого {moduleName}",
                GenitiveFeminine => $"{Ordinal(numSolved)}-й решённой {moduleName}",
                GenitivePlural => $"{Ordinal(numSolved)}-х решённых {moduleName}",
                PrepositiveMascNeuter => $"{Ordinal(numSolved)}-м решённом {moduleName}",
                PrepositiveFeminine => $"{Ordinal(numSolved)}-й решённой {moduleName}",
                PrepositivePlural => $"{Ordinal(numSolved)}-х решённых {moduleName}",
                InstrumentalMascNeuter => $"{Ordinal(numSolved)}-м решённым {moduleName}",
                InstrumentalFeminine => $"{Ordinal(numSolved)}-й решённой {moduleName}",
                InstrumentalPlural => $"{Ordinal(numSolved)}-ми решёнными {moduleName}",
                DativeMascNeuter => $"{Ordinal(numSolved)}-му решённому {moduleName}",
                DativeFeminine => $"{Ordinal(numSolved)}-й решённой {moduleName}",
                DativePlural => $"{Ordinal(numSolved)}-м решённым {moduleName}",
                в_PrepositiveMascNeuter or во_PrepositiveMascNeuter => $"{(numSolved == 2 ? "во" : "в")} {Ordinal(numSolved)}-м решённом {moduleName}",
                в_PrepositiveFeminine or во_PrepositiveFeminine => $"{(numSolved == 2 ? "во" : "в")} {Ordinal(numSolved)}-й решённой {moduleName}",
                в_PrepositivePlural or во_PrepositivePlural => $"{(numSolved == 2 ? "во" : "в")} {Ordinal(numSolved)}-х решённых {moduleName}",
                _ => throw new InvalidOperationException($"Unknown conjugation: {qtr.Conjugation}")
            } :
            qtr.Conjugation switch
            {
                в_PrepositiveMascNeuter or в_PrepositiveFeminine or в_PrepositivePlural => $"в {moduleName}",
                во_PrepositiveMascNeuter or во_PrepositiveFeminine or во_PrepositivePlural => $"во {moduleName}",
                _ => moduleName
            };
    }

    public override string Ordinal(int number) => number.ToString();
    public override bool TranslateManualQuestions => true;
    public override object ManualQuestionSortBy(TranslationInfo<QuestionTranslationInfo_ru> info, string originalModuleName) =>
        (info?.ManualModuleName == null && info?.ModuleName == null, info?.ManualModuleName ?? info?.ModuleName);

    protected override Dictionary<Type, TranslationInfo<QuestionTranslationInfo_ru>> _translations => new()
    {
        #region Translatable strings
        // 0
        [typeof(S0)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the starting number?"] = "Какое число было изначально показано?",
            },
            Questions = new()
            {
                [S0.Number] = new()
                {
                    // English: What was the initially displayed number in {0}?
                    Question = "Какое число было изначально показано на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // 1000 Words
        [typeof(S1000Words)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "1000 слов",
            ManualQuestions = new()
            {
                ["What were the words shown?"] = "Какие слова были показаны?",
            },
            Questions = new()
            {
                [S1000Words.Words] = new()
                {
                    // English: What was the {1} word shown in {0}?
                    // Example: What was the first word shown in 1000 Words?
                    Question = "Какое было {1}-е показанное слово {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
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

        // 100 Levels of Defusal
        [typeof(S100LevelsOfDefusal)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "100 уровнях обезвреживания",
            ManualQuestions = new()
            {
                ["What were the displayed letters?"] = "Какие буквы были показаны?",
            },
            Questions = new()
            {
                [S100LevelsOfDefusal.Letters] = new()
                {
                    // English: What was the {1} displayed letter in {0}?
                    // Example: What was the first displayed letter in 100 Levels of Defusal?
                    Question = "Какая была {1}-я показанная буква {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
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

        // The 1, 2, 3 Game
        [typeof(S123Game)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the opponent’s avatar and name?"] = "What was the opponent’s avatar and name?",
            },
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

        // 1D Chess
        [typeof(S1DChess)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were your and Rustmate’s moves?"] = "Какие были ходы у вас и Rustmate?",
            },
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

        // 21
        [typeof(S21)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the displayed number?"] = "What was the displayed number?",
            },
            Questions = new()
            {
                [S21.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "What was the displayed number in {0}?",
                },
            },
        },

        // 3D Maze
        [typeof(S3DMaze)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "3D лабиринт",
            ManualQuestions = new()
            {
                ["What were the markings?"] = "Какими буквами был обозначен лабиринт?",
                ["What was the cardinal direction?"] = "Какое было направление нужной стены?",
            },
            Questions = new()
            {
                [S3DMaze.QMarkings] = new()
                {
                    // English: What were the markings in {0}?
                    Question = "Какими буквами был обозначен ваш {0}?",
                    Conjugation = Conjugation.NominativeMasculine,
                },
                [S3DMaze.QBearing] = new()
                {
                    // English: What was the cardinal direction in {0}?
                    Question = "Какое было направление нужной стены {0}?",
                    Conjugation = Conjugation.NominativeMasculine,
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

        // 3D Tap Code
        [typeof(S3DTapCode)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the received word?"] = "Какое слово было получено?",
            },
            Questions = new()
            {
                [S3DTapCode.Word] = new()
                {
                    // English: What was the received word in {0}?
                    Question = "Какое слово было получено {0}?",
                },
            },
        },

        // 3D Tunnels
        [typeof(S3DTunnels)] = new()
        {
            ModuleName = "3D тоннелях",
            ManualQuestions = new()
            {
                ["What were the goal symbols?"] = "Какие символы были вашими целями?",
            },
            Questions = new()
            {
                [S3DTunnels.TargetNode] = new()
                {
                    // English: What was the {1} goal node in {0}?
                    // Example: What was the first goal node in 3D Tunnels?
                    Question = "Какой символ был вашей {1}-й целью {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
        },

        // 3 LEDs
        [typeof(S3LEDs)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the initial state of the LEDs?"] = "Какое было исходное состояние у светодиодов?",
            },
            Questions = new()
            {
                [S3LEDs.InitialState] = new()
                {
                    // English: What was the initial state of the LEDs in {0} (in reading order)?
                    Question = "Какое было исходное состояние у {0} (в порядке чтения)?",
                    Conjugation = Conjugation.GenitivePlural,
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

        // 3N+1
        [typeof(S3NPlus1)] = new()
        {
            ManualQuestions = new()
            {
                ["What number was initially displayed?"] = "Какое число было изначально показано?",
            },
            Questions = new()
            {
                [S3NPlus1.Question] = new()
                {
                    // English: What number was initially displayed in {0}?
                    Question = "Какое число было изначально показано на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // 4D Tunnels
        [typeof(S4DTunnels)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "4D тоннелях",
            ManualQuestions = new()
            {
                ["What were the goal symbols?"] = "What were the goal symbols?",
            },
            Questions = new()
            {
                [S4DTunnels.TargetNode] = new()
                {
                    // English: What was the {1} goal node in {0}?
                    // Example: What was the first goal node in 4D Tunnels?
                    Question = "Какой символ был вашей {1}-й целью {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
        },

        // 64
        [typeof(S64)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed number?"] = "Какое число было показано?",
            },
            Questions = new()
            {
                [S64.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Какое число было показано на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // 7
        [typeof(S7)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the LED colors for each stage?"] = "Какой был цвет у светодиода на каждом этапе?",
                ["What was each channel’s initial value?"] = "Какие были начальные величины у каждого цветового канала?",
            },
            Questions = new()
            {
                [S7.QInitialValues] = new()
                {
                    // English: What was the {1} channel’s initial value in {0}?
                    // Example: What was the red channel’s initial value in 7?
                    Question = "Какое было начальное значение {1} канала у {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // 9-Ball
        [typeof(S9Ball)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the numbers on each ball?"] = "Какие числа были на каждом шаре?",
            },
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

        // Abyss
        [typeof(SAbyss)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the characters displayed?"] = "Какие символы были показаны?",
            },
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

        // Accumulation
        [typeof(SAccumulation)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Накопления",
            ManualQuestions = new()
            {
                ["What were the background colors at each stage?"] = "Какого цвета были подложка и обрамление на каждом этапе?",
                ["What was the border color?"] = "What was the border color?",
            },
            Questions = new()
            {
                [SAccumulation.QBorderColor] = new()
                {
                    // English: What was the border color in {0}?
                    Question = "Какого цвета было обрамление у {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                [SAccumulation.QBackgroundColor] = new()
                {
                    // English: What was the background color in the {1} stage in {0}?
                    // Example: What was the background color in the first stage in Accumulation?
                    Question = "Какого цвета была подложка на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
            Discriminators = new()
            {
                [SAccumulation.DBorderColor] = new()
                {
                    // English: the Accumulation whose border was {0}
                    // Example: the Accumulation whose border was blue
                    Discriminator = "the Accumulation whose border was {0}",
                    Arguments = new()
                    {
                        ["blue"] = "blue",
                        ["brown"] = "brown",
                        ["green"] = "green",
                        ["grey"] = "grey",
                        ["lime"] = "lime",
                        ["orange"] = "orange",
                        ["pink"] = "pink",
                        ["red"] = "red",
                        ["white"] = "white",
                        ["yellow"] = "yellow",
                    },
                },
                [SAccumulation.DBackgroundColor] = new()
                {
                    // English: the Accumulation whose background in the {1} stage was {0}
                    // Example: the Accumulation whose background in the first stage was blue
                    Discriminator = "the Accumulation whose background in the {1} stage was {0}",
                    Arguments = new()
                    {
                        ["blue"] = "blue",
                        ["brown"] = "brown",
                        ["green"] = "green",
                        ["grey"] = "grey",
                        ["lime"] = "lime",
                        ["orange"] = "orange",
                        ["pink"] = "pink",
                        ["red"] = "red",
                        ["white"] = "white",
                        ["yellow"] = "yellow",
                    },
                },
            },
        },

        // Adventure Game
        [typeof(SAdventureGame)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Приключении",
            ManualQuestions = new()
            {
                ["Which items were present?"] = "Which items were present?",
            },
            Questions = new()
            {
                [SAdventureGame.QPresentItem] = new()
                {
                    // English: Which item was present in {0}?
                    Question = "Which item was present in {0}?",
                },
            },
            Discriminators = new()
            {
                [SAdventureGame.DPresentItem] = new()
                {
                    // English: the Adventure Game where the {0} was present
                    // Example: the Adventure Game where the Balloon was present
                    Discriminator = "the Adventure Game where the {0} was present",
                },
            },
        },

        // Affine Cycle
        [typeof(SAffineCycle)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which direction were the dials pointing?"] = "Какое было исходное сообщение и ответ?",
                ["What was written on each dial?"] = "What was written on each dial?",
            },
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
            Discriminators = new()
            {
                [SAffineCycle.LabelDiscriminator] = new()
                {
                    // English: the Affine Cycle that had the letter {0} on a dial
                    // Example: the Affine Cycle that had the letter A on a dial
                    Discriminator = "the Affine Cycle that had the letter {0} on a dial",
                },
            },
        },

        // Alcoholic Rampage
        [typeof(SAlcoholicRampage)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which mercenaries were displayed?"] = "Which mercenaries were displayed?",
            },
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

        // A Letter
        [typeof(SALetter)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the initially displayed letter?"] = "Какая была начальная буква?",
            },
            Questions = new()
            {
                [SALetter.InitialLetter] = new()
                {
                    // English: What was the initial letter in {0}?
                    Question = "Какая была начальная буква {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
            },
        },

        // Alfa-Bravo
        [typeof(SAlfaBravo)] = new()
        {
            ManualQuestions = new()
            {
                ["What final letter was pressed?"] = "Какая буква была нажата последней?",
                ["What letters were to the left and right of the final one?"] = "Какие буквы были слева и справа от последней нажатой?",
                ["What was the last digit on the small display?"] = "Какая последняя цифра была на маленьком экране?",
            },
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

        // Algebra
        [typeof(SAlgebra)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Алгебре",
            ManualQuestions = new()
            {
                ["What were the first two equations?"] = "Какими были первые два уравнения?",
            },
            Questions = new()
            {
                [SAlgebra.Equation1] = new()
                {
                    // English: What was the first equation in {0}?
                    Question = "Какое было первое уравнение {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
                [SAlgebra.Equation2] = new()
                {
                    // English: What was the second equation in {0}?
                    Question = "Какое было второе уравнение {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
            },
            Discriminators = new()
            {
                [SAlgebra.Discriminator1] = new()
                {
                    // English: the Algebra where the first equation was {0}
                    // Example: the Algebra where the first equation was a=3z
                    Discriminator = "the Algebra where the first equation was {0}",
                },
                [SAlgebra.Discriminator2] = new()
                {
                    // English: the Algebra where the second equation was {0}
                    // Example: the Algebra where the second equation was b=(2x/10)-y
                    Discriminator = "the Algebra where the second equation was {0}",
                },
            },
        },

        // Algorithmia
        [typeof(SAlgorithmia)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the starting and goal positions?"] = "Какие были начальная и целевая позиции?",
                ["What color was the bulb?"] = "Какого цвета был светодиод?",
                ["Which numbers were present in the seed?"] = "Какие числа присутствовали в зерне?",
            },
            Questions = new()
            {
                [SAlgorithmia.QPositions] = new()
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
                [SAlgorithmia.QColor] = new()
                {
                    // English: What was the color of the colored bulb in {0}?
                    Question = "Какого цвета была цветная лампочка {0}?",
                },
                [SAlgorithmia.QSeed] = new()
                {
                    // English: Which number was present in the seed in {0}?
                    Question = "Какое число присутствовало в зерне {0}?",
                },
            },
            Discriminators = new()
            {
                [SAlgorithmia.DPositions] = new()
                {
                    // English: the Algorithmia where this was the {0} position
                    // Example: the Algorithmia where this was the starting position
                    Discriminator = "the Algorithmia where this was the {0} position",
                    Arguments = new()
                    {
                        ["starting"] = "starting",
                        ["goal"] = "goal",
                    },
                },
                [SAlgorithmia.DColor] = new()
                {
                    // English: the Algorithmia whose colored bulb was {0}
                    // Example: the Algorithmia whose colored bulb was red
                    Discriminator = "the Algorithmia whose bulb was {0}",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["cyan"] = "cyan",
                        ["yellow"] = "yellow",
                        ["magenta"] = "magenta",
                    },
                },
                [SAlgorithmia.DSeed] = new()
                {
                    // English: the Algorithmia that had a {0} in the seed
                    // Example: the Algorithmia that had a 01 in the seed
                    Discriminator = "the Algorithmia that had a {0} in the seed",
                },
            },
        },

        // Alphabetical Ruling
        [typeof(SAlphabeticalRuling)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the letters and numbers displayed in each stage?"] = "Какие буквы и числа были показаны на каждом этапе?",
            },
            Questions = new()
            {
                [SAlphabeticalRuling.Letter] = new()
                {
                    // English: What was the letter displayed in the {1} stage of {0}?
                    // Example: What was the letter displayed in the first stage of Alphabetical Ruling?
                    Question = "Какая буква была показана на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SAlphabeticalRuling.Number] = new()
                {
                    // English: What was the number displayed in the {1} stage of {0}?
                    // Example: What was the number displayed in the first stage of Alphabetical Ruling?
                    Question = "Какое число было показано на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Alphabet Numbers
        [typeof(SAlphabetNumbers)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the numbers on the buttons in each stage?"] = "Какие числа были на кнопках на каждом этапе?",
            },
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

        // Alphabet Tiles
        [typeof(SAlphabetTiles)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the letters shown during each cycle?"] = "Какие буквы были показаны в каждом цикле?",
                ["What was the missing letter?"] = "Какая буква отсутствовала?",
            },
            Questions = new()
            {
                [SAlphabetTiles.QCycle] = new()
                {
                    // English: What was the {1} letter shown during the cycle in {0}?
                    // Example: What was the first letter shown during the cycle in Alphabet Tiles?
                    Question = "В цикле {0}, какая была {1}-я буква?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SAlphabetTiles.QMissingLetter] = new()
                {
                    // English: What was the missing letter in {0}?
                    Question = "Какая буква отсутствовала {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
            Discriminators = new()
            {
                [SAlphabetTiles.DCycle] = new()
                {
                    // English: the Alphabet Tiles where the {1} letter in the cycle was {0}
                    // Example: the Alphabet Tiles where the X letter in the cycle was first
                    Discriminator = "the Alphabet Tiles where the {1} letter in the cycle was {0}",
                },
                [SAlphabetTiles.DMissingLetter] = new()
                {
                    // English: the Alphabet Tiles whose missing letter was {0}
                    // Example: the Alphabet Tiles whose missing letter was A
                    Discriminator = "the Alphabet Tiles whose missing letter was {0}",
                },
            },
        },

        // Alpha-Bits
        [typeof(SAlphaBits)] = new()
        {
            ManualQuestions = new()
            {
                ["What characters were displayed on each screen?"] = "Какие символы были показаны на каждом экране?",
            },
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

        // A Message
        [typeof(SAMessage)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the received message?"] = "What was the received message?",
            },
            Questions = new()
            {
                [SAMessage.AMessage] = new()
                {
                    // English: What was the initial message in {0}?
                    Question = "What was the initial message in {0}?",
                },
            },
        },

        // Amusement Parks
        [typeof(SAmusementParks)] = new()
        {
            ManualQuestions = new()
            {
                ["Which rides were available?"] = "Какие аттракционы были доступны?",
            },
            Questions = new()
            {
                [SAmusementParks.Rides] = new()
                {
                    // English: Which ride was available in {0}?
                    Question = "Какой аттракцион был доступен {0}?",
                },
            },
        },

        // Ángel Hernández
        [typeof(SAngelHernandez)] = new()
        {
            ManualQuestions = new()
            {
                ["What letter was shown by the raised buttons in each stage?"] = "Какая буква была показана поднятой кнопкой на каждом этапе?",
            },
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

        // The Arena
        [typeof(SArena)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the maximum weapon damage of the attack phase?"] = "What was the maximum weapon damage of the attack phase?",
                ["What enemies were present in the defend phase?"] = "What enemies were present in the defend phase?",
                ["What numbers were present in the grab phase?"] = "What numbers were present in the grab phase?",
            },
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

        // Arithmelogic
        [typeof(SArithmelogic)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the symbol on the submit button?"] = "Какой символ был на кнопке отправки?",
            },
            Questions = new()
            {
                [SArithmelogic.Submit] = new()
                {
                    // English: What was the symbol on the submit button in {0}?
                    Question = "Какой символ был на кнопке отправки ответа {0}?",
                },
            },
        },

        // Arithmetic Cipher
        [typeof(SArithmeticCipher)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "What was on each screen?",
            },
            Questions = new()
            {
                [SArithmeticCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Arithmetic Cipher?
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

        // ASCII Maze
        [typeof(SASCIIMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["What characters were displayed?"] = "Какие символы были показаны?",
            },
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

        // A Square
        [typeof(SASquare)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the index colors?"] = "Какие были цвета-индексы?",
                ["What were the submitted colors?"] = "Какие цвета были введены?",
            },
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

        // Audio Morse
        [typeof(SAudioMorse)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the Morse code?"] = "Что было передано по Морзе?",
            },
            Questions = new()
            {
                [SAudioMorse.Sound] = new()
                {
                    // English: What was signaled in {0}?
                    Question = "Что было в сигнале {0}?",
                },
            },
        },

        // The Azure Button
        [typeof(SAzureButton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were T and the other displayed cards?"] = "What were T and the other displayed cards?",
                ["What was M?"] = "What was M?",
                ["What were the arrows?"] = "What were the arrows?",
            },
            Questions = new()
            {
                [SAzureButton.QDecoyArrowDirection] = new()
                {
                    // English: What was the {1} direction in the decoy arrow in {0}?
                    // Example: What was the first direction in the decoy arrow in The Azure Button?
                    Question = "Какое было {1}-е направление у стрелки-ловушки {0}?",
                },
                [SAzureButton.QNonDecoyArrowDirection] = new()
                {
                    // English: What was the {1} direction in the {2} non-decoy arrow in {0}?
                    // Example: What was the first direction in the first non-decoy arrow in The Azure Button?
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

        // Bakery
        [typeof(SBakery)] = new()
        {
            ManualQuestions = new()
            {
                ["Which menu items were present?"] = "Какая выпечка присутствовала?",
            },
            Questions = new()
            {
                [SBakery.Items] = new()
                {
                    // English: Which menu item was present in {0}?
                    Question = "Какая позиция меню присутствовала {0}?",
                },
            },
        },

        // Bamboozled Again
        [typeof(SBamboozledAgain)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Повторном надувательстве",
            ManualQuestions = new()
            {
                ["What were the initial labels and colors of each button?"] = "Какой текст и цвет были на каждой верной кнопке?",
                ["What were the decrypted text and color of each displayed message?"] = "Какой расшифрованный текст и цвет были у каждого показанного сообщения?",
            },
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

        // Bamboozling Button
        [typeof(SBamboozlingButton)] = new()
        {
            ManualQuestions = new()
            {
                ["What color was the button in each stage?"] = "Какого цвета была кнопка на каждом этапе?",
                ["What were the labels on the button in each stage?"] = "Какие надписи были на кнопке на каждом этапе?",
                ["What were the displays and their colors in each stage?"] = "Какие сообщения были на экране на каждом этапе и какого цвета?",
            },
            Questions = new()
            {
                [SBamboozlingButton.Color] = new()
                {
                    // English: What color was the button in the {1} stage of {0}?
                    // Example: What color was the button in the first stage of Bamboozling Button?
                    Question = "Какого цвета была кнопка на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SBamboozlingButton.Label] = new()
                {
                    // English: What was the {2} label on the button in the {1} stage of {0}?
                    // Example: What was the top label on the button in the first stage of Bamboozling Button?
                    Question = "Какая была {2} надпись на кнопке на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                    Arguments = new()
                    {
                        ["top"] = "верхняя",
                        ["bottom"] = "нижняя",
                    },
                },
            },
        },

        // Bar Charts
        [typeof(SBarCharts)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the bars’ heights, colors, and labels?"] = "Какая была высота, цвет и надпись у каждого столбца?",
                ["What was the category?"] = "Какая была категория?",
                ["What was the unit?"] = "Какая была единица измерения?",
            },
            Questions = new()
            {
                [SBarCharts.Category] = new()
                {
                    // English: What was the category of {0}?
                    Question = "Какая была категория у {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SBarCharts.Unit] = new()
                {
                    // English: What was the unit of {0}?
                    Question = "Какая была единица измерения у {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SBarCharts.Label] = new()
                {
                    // English: What was the label of the {1} bar in {0}?
                    // Example: What was the label of the first bar in Bar Charts?
                    Question = "Какая надпись была у {1}-го столбца {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SBarCharts.Color] = new()
                {
                    // English: What was the color of the {1} bar in {0}?
                    // Example: What was the color of the first bar in Bar Charts?
                    Question = "Какого цвета был {1}-й столбец {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Barcode Cipher
        [typeof(SBarcodeCipher)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the screen number?"] = "Какое число было на экране?",
                ["What was the edgework represented by each barcode?"] = "Какой компонент бомбы был представлен каждым штрих кодом?",
                ["What was the answer for each barcode?"] = "Какой был ответ для каждого штрих кода?",
            },
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

        // Bartending
        [typeof(SBartending)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which ingredient was in which position?"] = "Какие ингредиенты были в какой позиции?",
            },
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

        // Beans
        [typeof(SBeans)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What color were the eaten beans?"] = "Каких цветов были съеденные бобы?",
                ["Which beans were wobbling?"] = "Какие бобы были Wobbly?",
            },
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

        // Bean Sprouts
        [typeof(SBeanSprouts)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What colors were the eaten sprouts?"] = "Каких цветов были съеденные ростки?",
                ["Where were the eaten beans?"] = "Где были съеденные бобы?",
            },
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

        // Big Bean
        [typeof(SBigBean)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What color and wobbliness was the bean?"] = "Какого цвета был боб и был ли он Wobbly?",
            },
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

        // Big Circle
        [typeof(SBigCircle)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which direction was the circle spinning?"] = "Какие цвета присутствовали в решении?",
            },
            Questions = new()
            {
                [SBigCircle.SpinDirection] = new()
                {
                    // English: Which direction was the circle spinning in {0}?
                    Question = "Which direction was the circle spinning in {0}?",
                    Answers = new()
                    {
                        ["clockwise"] = "clockwise",
                        ["counterclockwise"] = "counterclockwise",
                    },
                },
            },
        },

        // Binary
        [typeof(SBinary)] = new()
        {
            ModuleName = "Двоичных светодиодах",
            ManualQuestions = new()
            {
                ["What word was displayed?"] = "Какое слово было показано?",
            },
            Questions = new()
            {
                [SBinary.Word] = new()
                {
                    // English: What word was displayed in {0}?
                    Question = "Какое слово было отображено на {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
        },

        // Binary Shift
        [typeof(SBinaryShift)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the initial numbers?"] = "Какие были начальные числа?",
                ["What number was selected at each stage?"] = "Какое число было выбрано на каждом этапе?",
            },
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

        // Bitmaps
        [typeof(SBitmaps)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Битовых изображениях",
            ManualQuestions = new()
            {
                ["How many pixels were black/white in each quadrant?"] = "Сколько чёрных/белых пикселей было в каждом квадранте?",
            },
            Questions = new()
            {
                [SBitmaps.Question] = new()
                {
                    // English: How many pixels were {1} in the {2} quadrant in {0}?
                    // Example: How many pixels were white in the top left quadrant in Bitmaps?
                    Question = "Сколько было {1} пикселей в {2} квадранте {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                    Arguments = new()
                    {
                        ["white"] = "белых",
                        ["black"] = "чёрных",
                        ["top left"] = "левом верхнем",
                        ["top right"] = "правом верхнем",
                        ["bottom left"] = "нижнем левом",
                        ["bottom right"] = "нижнем правом",
                    },
                },
            },
            Discriminators = new()
            {
                [SBitmaps.Discriminator] = new()
                {
                    // English: the Bitmap where the {2} pixel count in the {1} quadrant was {0}
                    // Example: the Bitmap where the white pixel count in the top left quadrant was 1
                    Discriminator = "the Bitmap where {0} of the pixels in the {1} quadrant were {2}",
                    Arguments = new()
                    {
                        ["top left"] = "top left",
                        ["top right"] = "top right",
                        ["bottom left"] = "bottom left",
                        ["bottom right"] = "bottom right",
                        ["white"] = "white",
                        ["black"] = "black",
                    },
                },
            },
        },

        // Black Cipher
        [typeof(SBlackCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Blindfolded Yahtzee
        [typeof(SBlindfoldedYahtzee)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which rolls did the module claim?"] = "Which rolls did the module claim?",
            },
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

        // Blind Maze
        [typeof(SBlindMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["What colors were the buttons?"] = "Какого цвета были кнопки?",
            },
            Questions = new()
            {
                [SBlindMaze.Colors] = new()
                {
                    // English: What color was the {1} button in {0}?
                    // Example: What color was the north button in Blind Maze?
                    Question = "Какого цвета была {1} кнопка {0}?",
                    Arguments = new()
                    {
                        ["north"] = "северная",
                        ["east"] = "восточная",
                        ["west"] = "западная",
                        ["south"] = "южная",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Green"] = "Зелёного",
                        ["Blue"] = "Синего",
                        ["Gray"] = "Серого",
                        ["Yellow"] = "Жёлтого",
                    },
                },
            },
        },

        // Blinking Notes
        [typeof(SBlinkingNotes)] = new()
        {
            ManualQuestions = new()
            {
                ["What song played?"] = "Какая песня была проиграна?",
            },
            Questions = new()
            {
                [SBlinkingNotes.Song] = new()
                {
                    // English: What song was flashed in {0}?
                    Question = "Какая песня мигала на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Blinkstop
        [typeof(SBlinkstop)] = new()
        {
            ManualQuestions = new()
            {
                ["How many times did the LED flash?"] = "Сколько раз мигал светодиод?",
                ["Which color did the LED flash the least?"] = "Каким цветом светодиод мигал наименьшее кол-во раз?",
            },
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

        // Blockbusters
        [typeof(SBlockbusters)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What letters were in the leftmost column at the start?"] = "What letters were in the leftmost column at the start?",
            },
            Questions = new()
            {
                [SBlockbusters.FirstLetters] = new()
                {
                    // English: Which letter was in the leftmost column at the start of {0}?
                    Question = "Which letter was in the leftmost column at the start of {0}?",
                },
            },
        },

        // Blue Arrows
        [typeof(SBlueArrows)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the characters on the screen?"] = "Какие символы были на экране?",
            },
            Questions = new()
            {
                [SBlueArrows.InitialCharacters] = new()
                {
                    // English: What were the characters on the screen in {0}?
                    Question = "Какие символы были на экране {0}?",
                },
            },
        },

        // The Blue Button
        [typeof(SBlueButton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were D, E, F, G, H, M, N, P, Q, and X?"] = "What were D, E, F, G, H, M, N, P, Q, and X?",
            },
            Questions = new()
            {
                [SBlueButton.D] = new()
                {
                    // English: What was D in {0}?
                    Question = "Какое значение было у D на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SBlueButton.EFGH] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was E in The Blue Button?
                    Question = "Какое значение было у {1} на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SBlueButton.M] = new()
                {
                    // English: What was M in {0}?
                    Question = "Какое значение было у M на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SBlueButton.N] = new()
                {
                    // English: What was N in {0}?
                    Question = "Какое значение было у N на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SBlueButton.P] = new()
                {
                    // English: What was P in {0}?
                    Question = "Какое значение было у P на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SBlueButton.Q] = new()
                {
                    // English: What was Q in {0}?
                    Question = "Какое значение было у D на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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

        // Blue Cipher
        [typeof(SBlueCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Blue Huffman Cipher
        [typeof(SBlueHuffmanCipher)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "What was on each screen?",
            },
            Questions = new()
            {
                [SBlueHuffmanCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Blue Huffman Cipher?
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

        // Bob Barks
        [typeof(SBobBarks)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the indicator labels?"] = "Какие были названия индикаторов?",
                ["Which buttons flashed in sequence?"] = "В какой последовательности загорались кнопки?",
            },
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

        // Boggle
        [typeof(SBoggle)] = new()
        {
            ManualQuestions = new()
            {
                ["Which letters were initially visible?"] = "Какие исходные буквы были показаны?",
            },
            Questions = new()
            {
                [SBoggle.Letters] = new()
                {
                    // English: What letter was initially visible on {0}?
                    Question = "Какая буква была изначально видна на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Bomb Diffusal
        [typeof(SBombDiffusal)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the license number?"] = "Какой был номер лицензии?",
            },
            Questions = new()
            {
                [SBombDiffusal.LicenseNumber] = new()
                {
                    // English: What was the license number in {0}?
                    Question = "Какой был номер лицензии {0}?",
                },
            },
        },

        // Bone Apple Tea
        [typeof(SBoneAppleTea)] = new()
        {
            ModuleName = "Еле-еле ели ели",
            ManualQuestions = new()
            {
                ["What were the phrases?"] = "Какие фразы были показаны?",
            },
            Questions = new()
            {
                [SBoneAppleTea.Phrase] = new()
                {
                    // English: Which phrase was shown on {0}?
                    Question = "Какая фраза была показана на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Boob Tube
        [typeof(SBoobTube)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the words?"] = "Какие слова были показаны?",
            },
            Questions = new()
            {
                [SBoobTube.Word] = new()
                {
                    // English: Which word was shown on {0}?
                    Question = "Какое слово было показано на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Book of Mario
        [typeof(SBookOfMario)] = new()
        {
            ManualQuestions = new()
            {
                ["Who said something in each stage?"] = "Кто сказал что-то и на каком этапе?",
                ["What was each character’s quote?"] = "Какие цитаты были у персонажей?",
            },
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

        // Boolean Wires
        [typeof(SBooleanWires)] = new()
        {
            ManualQuestions = new()
            {
                ["Which operators were submitted in each stage?"] = "Какой оператор был ответом на каждом этапе?",
            },
            Questions = new()
            {
                [SBooleanWires.EnteredOperators] = new()
                {
                    // English: Which operator did you submit in the {1} stage of {0}?
                    // Example: Which operator did you submit in the first stage of Boolean Wires?
                    Question = "Какой оператор был ответом на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Boomtar the Great
        [typeof(SBoomtarTheGreat)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the first and second rules?"] = "Какое было первое и второе правило?",
            },
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

        // Bordered Keys
        [typeof(SBorderedKeys)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the border color, displayed digit, key color, label and label color when you pressed each key?"] = "Какие были цвет рамки, показанное число, цвет клавиши, надпись и цвет надписи при каждом нажатии?",
            },
            Questions = new()
            {
                [SBorderedKeys.BorderColor] = new()
                {
                    // English: What was the {1} key’s border color when it was pressed in {0}?
                    // Example: What was the first key’s border color when it was pressed in Bordered Keys?
                    Question = "Какого цвета была рамка, когда вы нажали {1}-ю клавишу {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SBorderedKeys.KeyColor] = new()
                {
                    // English: What was the {1} key’s key color when it was pressed in {0}?
                    // Example: What was the first key’s key color when it was pressed in Bordered Keys?
                    Question = "Какого цвета была клавиша, когда вы нажали {1}-ю клавишу {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SBorderedKeys.LabelColor] = new()
                {
                    // English: What was the {1} key’s label color when it was pressed in {0}?
                    // Example: What was the first key’s label color when it was pressed in Bordered Keys?
                    Question = "Какого цвета была надпись, когда вы нажали {1}-ю клавишу {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Bottom Gear
        [typeof(SBottomGear)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the initially shown tweet?"] = "Какой твит был в начале?",
            },
            Questions = new()
            {
                [SBottomGear.Tweet] = new()
                {
                    // English: What tweet was shown in {0}?
                    Question = "Какой твит был показан {0}?",
                },
            },
        },

        // Boxing
        [typeof(SBoxing)] = new()
        {
            ManualQuestions = new()
            {
                ["Which contestants and substitutes (first and last names) were shown?"] = "Какие участники и замены(имена и фамилии) были показаны?",
                ["Who had which punch strength rating?"] = "Кто имел какую силу удара?",
            },
            Questions = new()
            {
                [SBoxing.StrengthByContestant] = new()
                {
                    // English: What was {1}’s strength rating on {0}?
                    // Example: What was Muhammad’s strength rating on Boxing?
                    Question = "Какая была оценка силы у {1} {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SBoxing.ContestantByStrength] = new()
                {
                    // English: What was the {1} of the contestant with strength rating {2} on {0}?
                    // Example: What was the first name of the contestant with strength rating 0 on Boxing?
                    Question = "{1} участника с оценкой силы {2} {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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

        // Braille
        [typeof(SBraille)] = new()
        {
            ModuleName = "Шрифта Брайля",
            ManualQuestions = new()
            {
                ["Which Braille patterns were present?"] = "Какие были буквы (паттерны)?",
            },
            Questions = new()
            {
                [SBraille.Pattern] = new()
                {
                    // English: What was the {1} pattern in {0}?
                    // Example: What was the first pattern in Braille?
                    Question = "Какой был {1}-й паттерн {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Breakfast Egg
        [typeof(SBreakfastEgg)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which colors appeared on the egg?"] = "Какие цвета были показаны на яйце?",
            },
            Questions = new()
            {
                [SBreakfastEgg.Color] = new()
                {
                    // English: Which color appeared on the egg in {0}?
                    Question = "Какой был цвет у {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Broken Guitar Chords
        [typeof(SBrokenGuitarChords)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed chord?"] = "Какой аккорд был показан?",
                ["Which string was broken?"] = "Которая струна была сломана?",
            },
            Questions = new()
            {
                [SBrokenGuitarChords.DisplayedChord] = new()
                {
                    // English: What was the displayed chord in {0}?
                    Question = "Какой аккорд был показан на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SBrokenGuitarChords.MutedString] = new()
                {
                    // English: In which position, from left to right, was the broken string in {0}?
                    Question = "На какой позиции (слева направо) была сломанная струна {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Brown Cipher
        [typeof(SBrownCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // The Bulb
        [typeof(SBulb)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Was the bulb initially lit?"] = "Какие правильные кнопки были нажаты?",
            },
            Questions = new()
            {
                [SBulb.InitialState] = new()
                {
                    // English: Was the bulb initially lit in {0}?
                    Question = "Was the bulb initially lit in {0}?",
                    Answers = new()
                    {
                        ["Yes"] = "Yes",
                        ["No"] = "No",
                    },
                },
            },
        },

        // Burger Alarm
        [typeof(SBurgerAlarm)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the displayed digits?"] = "Какие цифры были показаны?",
                ["What were the order numbers?"] = "Какие были номера заказов?",
            },
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

        // Burglar Alarm
        [typeof(SBurglarAlarm)] = new()
        {
            ModuleName = "Сигнализации",
            ManualQuestions = new()
            {
                ["What were the displayed digits?"] = "Какие цифры были показаны?",
            },
            Questions = new()
            {
                [SBurglarAlarm.Digits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Burglar Alarm?
                    Question = "Какая была {1}-я цифра {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
            },
        },

        // The Button
        [typeof(SButton)] = new()
        {
            ModuleName = "Кнопки",
            ManualQuestions = new()
            {
                ["What color did the light glow?"] = "Какого цвета был цветной индикатор?",
            },
            Questions = new()
            {
                [SButton.LightColor] = new()
                {
                    // English: What color did the light glow in {0}?
                    Question = "Каким цветом горела цветная полоска {0}?",
                    Conjugation = Conjugation.GenitiveFeminine,
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

        // Buttonage
        [typeof(SButtonage)] = new()
        {
            ManualQuestions = new()
            {
                ["How many of each color and border color were there?"] = "Сколько было каждого цвета и каждой рамки?",
                ["How many special buttons were there?"] = "Сколько было специальных кнопок?",
                ["How many buttons had a P?"] = "На скольки кнопках было P?",
            },
            Questions = new()
            {
                [SButtonage.Buttons] = new()
                {
                    // English: How many {1} buttons were there on {0}?
                    // Example: How many red buttons were there on Buttonage?
                    Question = "Сколько {1} было на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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

        // Button Sequence
        [typeof(SButtonSequence)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Последовательности кнопок",
            ManualQuestions = new()
            {
                ["How many times did each color occur?"] = "Сколько раз встречался каждый цвет?",
            },
            Questions = new()
            {
                [SButtonSequence.QColorOccurrences] = new()
                {
                    // English: How many {1} buttons were there in {0}?
                    // Example: How many red buttons were there in Button Sequence?
                    Question = "Сколько было {1} кнопок {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                    Arguments = new()
                    {
                        ["red"] = "красных",
                        ["blue"] = "синих",
                        ["yellow"] = "жёлтых",
                        ["white"] = "белых",
                    },
                },
            },
            Discriminators = new()
            {
                [SButtonSequence.DColorOccurrences] = new()
                {
                    // English: the Button Sequence that had {0} {1}
                    // Example: the Button Sequence that had 1 red button
                    Discriminator = "the Button Sequence that had {0} {1}",
                    Arguments = new()
                    {
                        ["red button"] = "red button",
                        ["blue button"] = "blue button",
                        ["yellow button"] = "yellow button",
                        ["white button"] = "white button",
                        ["red buttons"] = "red buttons",
                        ["blue buttons"] = "blue buttons",
                        ["yellow buttons"] = "yellow buttons",
                        ["white buttons"] = "white buttons",
                    },
                },
            },
        },

        // Cacti’s Conundrum
        [typeof(SCactisConundrum)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What colors was the LED?"] = "Какие цвета были на светодиоде?",
            },
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

        // Caesar Cycle
        [typeof(SCaesarCycle)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which direction were the dials pointing?"] = "Какое было сообщение и ответ?",
                ["What was written on each dial?"] = "What was written on each dial?",
            },
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
            Discriminators = new()
            {
                [SCaesarCycle.LabelDiscriminator] = new()
                {
                    // English: the Caesar Cycle that had the letter {0} on a dial
                    // Example: the Caesar Cycle that had the letter A on a dial
                    Discriminator = "the Caesar Cycle that had the letter {0} on a dial",
                },
            },
        },

        // Caesar Psycho
        [typeof(SCaesarPsycho)] = new()
        {
            ManualQuestions = new()
            {
                ["What text and color were on the top display in each stage?"] = "Какие текст и цвет были на верхнем экране на каждом этапе?",
            },
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

        // Caesar's Maths
        [typeof(SCaesarsMaths)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the colors of each LED?"] = "What were the colors of each LED?",
            },
            Questions = new()
            {
                [SCaesarsMaths.LED] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the first LED in Caesar's Maths?
                    Question = "What color was the {1} LED in {0}?",
                    Answers = new()
                    {
                        ["Yellow"] = "Yellow",
                        ["Blue"] = "Blue",
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                    },
                },
            },
        },

        // Calendar
        [typeof(SCalendar)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the holiday?"] = "Какого цвета был индикатор?",
            },
            Questions = new()
            {
                [SCalendar.Holiday] = new()
                {
                    // English: What was the holiday in {0}?
                    Question = "What was the holiday in {0}?",
                },
            },
        },

        // CA-RPS
        [typeof(SCARPS)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the starting colors?"] = "Какие были начальные цвета?",
            },
            Questions = new()
            {
                [SCARPS.Cell] = new()
                {
                    // English: What color was this cell initially in {0}? (+ sprite)
                    Question = "Какого цвета была эта клетка в начале {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Cartinese
        [typeof(SCartinese)] = new()
        {
            ManualQuestions = new()
            {
                ["What lyrics were played by each button?"] = "Какой текст был проигран каждой кнопкой?",
                ["What color was each button?"] = "Какого цвета была каждая кнопка?",
            },
            Questions = new()
            {
                [SCartinese.ButtonColors] = new()
                {
                    // English: What color was the {1} button in {0}?
                    // Example: What color was the up button in Cartinese?
                    Question = "Какого цвета была кнопка \"{1}\" {0}?",
                    Arguments = new()
                    {
                        ["up"] = "вверх",
                        ["right"] = "вправо",
                        ["down"] = "вниз",
                        ["left"] = "влево",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Yellow"] = "Жёлтого",
                        ["Green"] = "Зелёного",
                        ["Blue"] = "Синего",
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

        // Catchphrase
        [typeof(SCatchphrase)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the colours of the panels?"] = "Каких цветов были панели?",
            },
            Questions = new()
            {
                [SCatchphrase.Colour] = new()
                {
                    // English: What was the colour of the {1} panel in {0}?
                    // Example: What was the colour of the top-left panel in Catchphrase?
                    Question = "Какого цвета была панель {1} {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "сверху слева",
                        ["top-right"] = "сверху справа",
                        ["bottom-left"] = "снизу слева",
                        ["bottom-right"] = "снизу справа",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Orange"] = "Оранжевый",
                        ["Purple"] = "Фиолетовый",
                        ["Yellow"] = "Жёлтый",
                    },
                },
            },
        },

        // Challenge & Contact
        [typeof(SChallengeAndContact)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the displayed letters?"] = "What were the displayed letters?",
            },
            Questions = new()
            {
                [SChallengeAndContact.Letters] = new()
                {
                    // English: What was the {1} displayed letter in {0}?
                    // Example: What was the first displayed letter in Challenge & Contact?
                    Question = "What was the {1} displayed letter in {0}?",
                },
            },
        },

        // Character Codes
        [typeof(SCharacterCodes)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the characters?"] = "Какие были символы?",
            },
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

        // Character Shift
        [typeof(SCharacterShift)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the characters on the sliders?"] = "Какие были символы на ползунках?",
            },
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

        // Character Slots
        [typeof(SCharacterSlots)] = new()
        {
            ManualQuestions = new()
            {
                ["Who was displayed in each slot for each stage?"] = "Кто был показан в каждом слоте на каждом этапе?",
            },
            Questions = new()
            {
                [SCharacterSlots.DisplayedCharacters] = new()
                {
                    // English: Who was displayed in the {1} slot in the {2} stage of {0}?
                    // Example: Who was displayed in the first slot in the first stage of Character Slots?
                    Question = "Кто был показан в {1}-м слоте на {2}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Cheap Checkout
        [typeof(SCheapCheckout)] = new()
        {
            ModuleName = "Свободной кассе",
            ManualQuestions = new()
            {
                ["What were the paid amounts?"] = "Сколько конкретно денег было заплачено?",
            },
            Questions = new()
            {
                [SCheapCheckout.Paid] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was the paid amount in Cheap Checkout?
                    Question = "{1} {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                    Arguments = new()
                    {
                        ["the paid amount"] = "Сколько всего денег было заплачено",
                        ["the first paid amount"] = "Каким был первый платёж",
                        ["the second paid amount"] = "Каким был второй платёж",
                    },
                },
            },
        },

        // Cheat Checkout
        [typeof(SCheatCheckout)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the cryptocurrency?"] = "What was the cryptocurrency?",
                ["What was the site and hack method for each hack?"] = "What was the site and hack method for each hack?",
            },
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

        // Cheep Checkout
        [typeof(SCheepCheckout)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which birds were present?"] = "Какие птицы присутствовали?",
            },
            Questions = new()
            {
                [SCheepCheckout.Birds] = new()
                {
                    // English: Which bird {1} present in {0}?
                    // Example: Which bird was present in Cheep Checkout?
                    Question = "Какая птица {1} {0}?",
                    Arguments = new()
                    {
                        ["was"] = "присутствовала",
                        ["was not"] = "отсутствовала",
                    },
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
                },
            },
        },

        // Chess
        [typeof(SChess)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Шахматах",
            ManualQuestions = new()
            {
                ["What were the coordinates?"] = "Какие координаты присутствовали на модуле?",
            },
            Questions = new()
            {
                [SChess.QCoordinate] = new()
                {
                    // English: What was the {1} coordinate in {0}?
                    // Example: What was the first coordinate in Chess?
                    Question = "Какие были {1}-е координаты {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
            Discriminators = new()
            {
                [SChess.DCoordinate] = new()
                {
                    // English: the Chess where the {1} coordinate was {0}
                    // Example: the Chess where the first coordinate was a1
                    Discriminator = "the Chess where the {1} coordinate was {0}",
                },
            },
        },

        // Chinese Counting
        [typeof(SChineseCounting)] = new()
        {
            ManualQuestions = new()
            {
                ["What colors were the LEDs?"] = "Каких цветов были светодиоды?",
            },
            Questions = new()
            {
                [SChineseCounting.LED] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the left LED in Chinese Counting?
                    Question = "Какой был цвет {1} светодиода {0}?",
                    Arguments = new()
                    {
                        ["left"] = "левого",
                        ["right"] = "правого",
                    },
                    Answers = new()
                    {
                        ["White"] = "Белый",
                        ["Red"] = "Красный",
                        ["Green"] = "Зелёный",
                        ["Orange"] = "Оранжевый",
                    },
                },
            },
        },

        // Chinese Remainder Theorem
        [typeof(SChineseRemainderTheorem)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the equations?"] = "Какие были уравнения?",
            },
            Questions = new()
            {
                [SChineseRemainderTheorem.Equations] = new()
                {
                    // English: Which equation was used in {0}?
                    Question = "Какое уравнение было использовано {0}?",
                },
            },
        },

        // Chord Qualities
        [typeof(SChordQualities)] = new()
        {
            ModuleName = "Аккордных ладах",
            ManualQuestions = new()
            {
                ["What notes were in the given chord?"] = "Какие ноты присутствовали в начальном аккорде?",
            },
            Questions = new()
            {
                [SChordQualities.Notes] = new()
                {
                    // English: Which note was part of the given chord in {0}?
                    Question = "Какая нота присутствовала в начальном аккорде {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
        },

        // ↻↺
        [typeof(SClockCounter)] = new()
        {
            ManualQuestions = new()
            {
                ["Which arrows were shown?"] = "Какие стрелки были показаны?",
            },
            Questions = new()
            {
                [SClockCounter.Arrows] = new()
                {
                    // English: Which arrow was shown in {0}?
                    Question = "Какая стрелка была показана {0}?",
                },
            },
        },

        // The Code
        [typeof(SCode)] = new()
        {
            ModuleName = "Коде",
            ManualQuestions = new()
            {
                ["What was the displayed number?"] = "Какое число было показано на экране?",
            },
            Questions = new()
            {
                [SCode.DisplayNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Какое было показанное число {0}?",
                },
            },
        },

        // Codenames
        [typeof(SCodenames)] = new()
        {
            ManualQuestions = new()
            {
                ["Which words were submitted?"] = "Какие слова были введены?",
            },
            Questions = new()
            {
                [SCodenames.Answers] = new()
                {
                    // English: Which of these words was submitted in {0}?
                    Question = "Какое из слов было введено {0}?",
                },
            },
        },

        // Coffee Beans
        [typeof(SCoffeeBeans)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the movements?"] = "Какие были движения?",
            },
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

        // Coffeebucks
        [typeof(SCoffeebucks)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the last customer’s preferred sugar content, time of day, stress level, and size?"] = "Какое кофе было подано последним?",
            },
            Questions = new()
            {
                [SCoffeebucks.Sugar] = new()
                {
                    // English: What was the last customer’s preferred sugar content in {0}?
                    Question = "What was the last customer's preferred sugar content in {0}?",
                },
                [SCoffeebucks.Time] = new()
                {
                    // English: What was the last customer’s preferred time of day in {0}?
                    Question = "What was the last customer's preferred time of day in {0}?",
                },
                [SCoffeebucks.Stress] = new()
                {
                    // English: What was the last customer’s preferred stress-level in {0}?
                    Question = "What was the last customer's preferred stress-level in {0}?",
                },
                [SCoffeebucks.Size] = new()
                {
                    // English: What was the last customer’s preferred size in {0}?
                    Question = "What was the last customer's preferred size in {0}?",
                },
            },
        },

        // Coinage
        [typeof(SCoinage)] = new()
        {
            ManualQuestions = new()
            {
                ["Which coin was flipped?"] = "Какая монета была подкинута?",
            },
            Questions = new()
            {
                [SCoinage.Flip] = new()
                {
                    // English: Which coin was flipped in {0}?
                    Question = "Какая монета была перевёрнута {0}?",
                },
            },
        },

        // Color Addition
        [typeof(SColorAddition)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the three numbers?"] = "Какие были три числа?",
            },
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

        // Color Braille
        [typeof(SColorBraille)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What color was each dot?"] = "Какого цвета была каждая точка?",
            },
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

        // Color Decoding
        [typeof(SColorDecoding)] = new()
        {
            ModuleName = "Расшифровки цветов",
            ManualQuestions = new()
            {
                ["What were the indicator patterns and colors in each stage?"] = "Какой узор и каких цветов был на каждом этапе?",
            },
            Questions = new()
            {
                [SColorDecoding.IndicatorColors] = new()
                {
                    // English: Which color {1} in the {2}-stage indicator pattern in {0}?
                    // Example: Which color appeared in the first-stage indicator pattern in Color Decoding?
                    Question = "Какой цвет {1} на узоре индикатора на {2}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveFeminine,
                    Arguments = new()
                    {
                        ["appeared"] = "присутствовал",
                        ["did not appear"] = "отсутствовал",
                    },
                    Answers = new()
                    {
                        ["Green"] = "Зелёный",
                        ["Purple"] = "Фиолетовый",
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                        ["Yellow"] = "Жёлтый",
                    },
                },
                [SColorDecoding.IndicatorPattern] = new()
                {
                    // English: What was the {1}-stage indicator pattern in {0}?
                    // Example: What was the first-stage indicator pattern in Color Decoding?
                    Question = "Какой был узор индикатора на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveFeminine,
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

        // Colored Keys
        [typeof(SColoredKeys)] = new()
        {
            ModuleName = "Цветных кнопках",
            ManualQuestions = new()
            {
                ["What was the displayed word and its color?"] = "Какое слово и какого цвета было показано?",
                ["What were the colors and letters on each key?"] = "Какие цвета и буквы были на каждой клавише?",
            },
            Questions = new()
            {
                [SColoredKeys.DisplayWord] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "Какое слово было отображено на дисплее на {0}?",
                    Conjugation = Conjugation.PrepositivePlural,
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
                    Conjugation = Conjugation.PrepositivePlural,
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
                    Conjugation = Conjugation.PrepositivePlural,
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
                    Conjugation = Conjugation.PrepositivePlural,
                    Arguments = new()
                    {
                        ["top-left"] = "верхняя левая",
                        ["top-right"] = "верхняя правая",
                        ["bottom-left"] = "нижняя левая",
                        ["bottom-right"] = "нижняя правая",
                    },
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
            },
        },

        // Colored Squares
        [typeof(SColoredSquares)] = new()
        {
            ModuleName = "Цветных квадратах",
            ManualQuestions = new()
            {
                ["What was the first color group?"] = "Какого цвета была первая группа?",
            },
            Questions = new()
            {
                [SColoredSquares.FirstGroup] = new()
                {
                    // English: What was the first color group in {0}?
                    Question = "Какого цвета была первая группа на {0}?",
                    Conjugation = Conjugation.PrepositivePlural,
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

        // Colored Switches
        [typeof(SColoredSwitches)] = new()
        {
            ModuleName = "Цветных переключателей",
            ManualQuestions = new()
            {
                ["What was the initial position of the switches?"] = "Какое было начальное положение переключателей?",
            },
            Questions = new()
            {
                [SColoredSwitches.InitialPosition] = new()
                {
                    // English: What was the initial position of the switches in {0}?
                    Question = "Какое было начальное положение {0}?",
                    Conjugation = Conjugation.GenitivePlural,
                },
            },
        },

        // Color Morse
        [typeof(SColorMorse)] = new()
        {
            ModuleName = "Цветной азбуке Морзе",
            ManualQuestions = new()
            {
                ["What were the colors of the LEDs?"] = "Какие цвета были у светодиодов?",
                ["What characters were flashed by the LEDs?"] = "Какие символы были представлены светодиодами?",
            },
            Questions = new()
            {
                [SColorMorse.Color] = new()
                {
                    // English: What was the color of the {1} LED in {0}?
                    // Example: What was the color of the first LED in Color Morse?
                    Question = "Какой был цвет {1}-го светодиода {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
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
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
            },
        },

        // Color One Two
        [typeof(SColorOneTwo)] = new()
        {
            ModuleName = "\"Цвет раз два\"",
            ManualModuleName = "Цвет раз два",
            ManualQuestions = new()
            {
                ["What colors were the LEDs?"] = "Какие цвета были на светодиодах?",
            },
            Questions = new()
            {
                [SColorOneTwo.Color] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the left LED in Color One Two?
                    Question = "Какого цвета был {1} светодиод на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                    Arguments = new()
                    {
                        ["left"] = "левый",
                        ["right"] = "правый",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                        ["Green"] = "Зелёный",
                        ["Yellow"] = "Жёлтый",
                    },
                },
            },
        },

        // Colors Maximization
        [typeof(SColorsMaximization)] = new()
        {
            ManualQuestions = new()
            {
                ["How many buttons were there of each color?"] = "Сколько было кнопок каждого цвета?",
            },
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

        // Coloured Cubes
        [typeof(SColouredCubes)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the colours of the cubes and stage lights in each stage?"] = "Какие цвета куба и индикатора этапа были на каждом этапе?",
            },
            Questions = new()
            {
                [SColouredCubes.Colours] = new()
                {
                    // English: What was the colour of this {1} in the {2} stage of {0}? (+ sprite)
                    // Example: What was the colour of this cube in the first stage of Coloured Cubes? (+ sprite)
                    Question = "Какой был цвет данного {1} на {2}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                    Arguments = new()
                    {
                        ["cube"] = "куба",
                        ["stage light"] = "индикатора этапа",
                    },
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
                },
            },
        },

        // Coloured Cylinder
        [typeof(SColouredCylinder)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What colours did the cylinder flash?"] = "Какими цветами мигал цилиндр?",
            },
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

        // Colour Flash
        [typeof(SColourFlash)] = new()
        {
            ModuleName = "Цветной вспышки",
            ManualQuestions = new()
            {
                ["What was the color of the last word in the sequence?"] = "Какого цвета было последнее слово в последовательности?",
            },
            Questions = new()
            {
                [SColourFlash.LastColor] = new()
                {
                    // English: What was the color of the last word in the sequence in {0}?
                    Question = "Какого цвета было последнее слово в последовательности {0}?",
                    Conjugation = Conjugation.GenitiveFeminine,
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

        // Concentration
        [typeof(SConcentration)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the initial layout?"] = "Как карты были выложены в самом начале?",
            },
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

        // Conditional Buttons
        [typeof(SConditionalButtons)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the color of each button?"] = "Какие цвета были на каждой кнопке?",
            },
            Questions = new()
            {
                [SConditionalButtons.Colors] = new()
                {
                    // English: What was the color of this button in {0}? (+ sprite)
                    Question = "Какого цвета была эта кнопка на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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

        // Connected Monitors
        [typeof(SConnectedMonitors)] = new()
        {
            ManualQuestions = new()
            {
                ["What numbers were initially displayed?"] = "Какие числа были изначально показаны?",
                ["What colors were the indicators?"] = "Каких цветов были индикаторы?",
            },
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

        // Connection Check
        [typeof(SConnectionCheck)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Проверке соединения",
            ManualQuestions = new()
            {
                ["What number pairs were present?"] = "Какие пары чисел присутствовали?",
            },
            Questions = new()
            {
                [SConnectionCheck.Numbers] = new()
                {
                    // English: What pair of numbers was present in {0}?
                    Question = "Какая пара чисел присутствовала {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
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

        // Coordinates
        [typeof(SCoordinates)] = new()
        {
            ModuleName = "Координатах",
            ManualQuestions = new()
            {
                ["What was the grid size?"] = "Какой был размер сетки?",
            },
            Questions = new()
            {
                [SCoordinates.Size] = new()
                {
                    // English: What was the grid size in {0}?
                    Question = "В каком формате был указан размер сетки {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
        },

        // Coordination
        [typeof(SCoordination)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the label and position of the starting coordinate?"] = "What was the label and position of the starting coordinate?",
            },
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

        // Coral Cipher
        [typeof(SCoralCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Corners
        [typeof(SCorners)] = new()
        {
            ModuleName = "Углах",
            ManualQuestions = new()
            {
                ["What were the colors of the corners?"] = "Каких цветов были углы?",
            },
            Questions = new()
            {
                [SCorners.Colors] = new()
                {
                    // English: What was the color of the {1} corner in {0}?
                    // Example: What was the color of the top-left corner in Corners?
                    Question = "Какого цвета был {1} угол {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                    Arguments = new()
                    {
                        ["top-left"] = "верхний левый",
                        ["top-right"] = "верхний правый",
                        ["bottom-right"] = "нижний правый",
                        ["bottom-left"] = "нижний левый",
                    },
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["green"] = "Зелёного",
                        ["blue"] = "Синего",
                        ["yellow"] = "Жёлтого",
                    },
                },
                [SCorners.ColorCount] = new()
                {
                    // English: How many corners in {0} were {1}?
                    // Example: How many corners in Corners were red?
                    Question = "Сколько было {1} углов {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
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

        // Cornflower Cipher
        [typeof(SCornflowerCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Cosmic
        [typeof(SCosmic)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the number initially shown?"] = "Какое было исходное число?",
            },
            Questions = new()
            {
                [SCosmic.Number] = new()
                {
                    // English: What was the number initially shown in {0}?
                    Question = "Какое число было изначально показано {0}?",
                },
            },
        },

        // Crazy Hamburger
        [typeof(SCrazyHamburger)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the displayed ingredients?"] = "Какие ингредиенты были показаны?",
            },
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

        // Crazy Maze
        [typeof(SCrazyMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the starting and goal positions?"] = "Где были начало и цель?",
            },
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

        // Cream Cipher
        [typeof(SCreamCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Creation
        [typeof(SCreation)] = new()
        {
            ModuleName = "Творения",
            ManualQuestions = new()
            {
                ["What was the weather condition on the first day?"] = "Какая была погода в каждый день?",
            },
            Questions = new()
            {
                [SCreation.Weather] = new()
                {
                    // English: What were the weather conditions on the {1} day in {0}?
                    // Example: What were the weather conditions on the first day in Creation?
                    Question = "Какая погода была на {1}-м дне {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Crimson Cipher
        [typeof(SCrimsonCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Critters
        [typeof(SCritters)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the alteration color?"] = "Какой был цвет?",
            },
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

        // Cruel Binary
        [typeof(SCruelBinary)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed word?"] = "Какое слово было показано?",
            },
            Questions = new()
            {
                [SCruelBinary.DisplayedWord] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "Какое слово было показано {0}?",
                },
            },
        },

        // Cruel Keypads
        [typeof(SCruelKeypads)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the displayed symbols and what was the color of the bar in each stage?"] = "Какие символы были показаны и каких цветов была шкала на каждом этапе?",
            },
            Questions = new()
            {
                [SCruelKeypads.Colors] = new()
                {
                    // English: What was the color of the bar in the {1} stage of {0}?
                    // Example: What was the color of the bar in the first stage of Cruel Keypads?
                    Question = "Какого цвета была шкала на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // The cRule
        [typeof(SCRule)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which cells were prefilled at the start?"] = "Which cells were prefilled at the start?",
                ["Which symbol pair appeared where?"] = "Which symbol pair appeared where?",
            },
            Questions = new()
            {
                [SCRule.SymbolPair] = new()
                {
                    // English: Which symbol pair was here in {0}? (+ sprite)
                    Question = "Какая пара символов была здесь {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SCRule.SymbolPairCell] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was ♤♤ in The cRule?
                    Question = "Где находилось {1} {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SCRule.SymbolPairPresent] = new()
                {
                    // English: Which symbol pair was present on {0}?
                    Question = "Какая пара символов присутствовала {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SCRule.Prefilled] = new()
                {
                    // English: Which cell was pre-filled at the start of {0}?
                    Question = "Какая клетка была уже заполнена в начале {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Cryptic Cycle
        [typeof(SCrypticCycle)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which direction were the dials pointing?"] = "Какое было сообщение и ответ?",
                ["What was written on each dial?"] = "What was written on each dial?",
            },
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
            Discriminators = new()
            {
                [SCrypticCycle.LabelDiscriminator] = new()
                {
                    // English: the Cryptic Cycle that had the letter {0} on a dial
                    // Example: the Cryptic Cycle that had the letter A on a dial
                    Discriminator = "the Cryptic Cycle that had the letter {0} on a dial",
                },
            },
        },

        // Cryptic Keypad
        [typeof(SCrypticKeypad)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the labels and cardinal directions of each key?"] = "Какие символы и стороны света были указаны на клавишах?",
            },
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
                    Arguments = new()
                    {
                        ["top-left"] = "верхняя левая",
                        ["top-right"] = "верхняя правая",
                        ["bottom-left"] = "нижняя левая",
                        ["bottom-right"] = "нижняя правая",
                    },
                    Answers = new()
                    {
                        ["North"] = "Север",
                        ["East"] = "Восток",
                        ["South"] = "Юг",
                        ["West"] = "Запад",
                    },
                },
            },
        },

        // The Cube
        [typeof(SCube)] = new()
        {
            ModuleName = "Куба",
            ManualQuestions = new()
            {
                ["What were the cube rotations?"] = "Какие повороты были у куба?",
            },
            Questions = new()
            {
                [SCube.Rotations] = new()
                {
                    // English: What was the {1} cube rotation in {0}?
                    // Example: What was the first cube rotation in The Cube?
                    Question = "Какое было {1}-е вращение у {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Cursed Double-Oh
        [typeof(SCursedDoubleOh)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the first digit of the initial number?"] = "Какая была первая цифра начального числа?",
            },
            Questions = new()
            {
                [SCursedDoubleOh.InitialPosition] = new()
                {
                    // English: What was the first digit of the initially displayed number in {0}?
                    Question = "Какая была первая цифра изначально отображённого числа {0}?",
                },
            },
        },

        // Customer Identification
        [typeof(SCustomerIdentification)] = new()
        {
            ManualQuestions = new()
            {
                ["Which customers were displayed?"] = "Какие посетители были показаны?",
            },
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

        // The Cyan Button
        [typeof(SCyanButton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Where was the button at each stage?"] = "Where was the button at each stage?",
            },
            Questions = new()
            {
                [SCyanButton.QPositions] = new()
                {
                    // English: Where was the button at in the {1} stage of {0}?
                    // Example: Where was the button at in the first stage of The Cyan Button?
                    Question = "Где был {0} на своём {1}-м этапе?",
                    Conjugation = Conjugation.NominativeMasculine,
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
            Discriminators = new()
            {
                [SCyanButton.DPositions] = new()
                {
                    // English: the Cyan Button where the button in the {0} stage was at the {1}
                    // Example: the Cyan Button where the button in the first stage was at the top left
                    Discriminator = "the Cyan Button where the button at the {0} stage was in the {1}",
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

        // DACH Maze
        [typeof(SDACHMaze)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which region did you depart from?"] = "Откуда вы отправились?",
            },
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

        // Deaf Alley
        [typeof(SDeafAlley)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the shape generated?"] = "Какой символ был сгенерирован?",
            },
            Questions = new()
            {
                [SDeafAlley.Shape] = new()
                {
                    // English: What was the shape generated in {0}?
                    Question = "Какой символ был сгенерирован {0}?",
                },
            },
        },

        // The Deck of Many Things
        [typeof(SDeckOfManyThings)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What deck did the first card belong to?"] = "What deck did the first card belong to?",
            },
            Questions = new()
            {
                [SDeckOfManyThings.FirstCard] = new()
                {
                    // English: What deck did the first card of {0} belong to?
                    Question = "Какой колоде принадлежала первая карта {0}?",
                    Answers = new()
                    {
                        ["Standard"] = "Standard",
                        ["Metropolitan"] = "Metropolitan",
                        ["Maritime"] = "Maritime",
                        ["Arctic"] = "Arctic",
                        ["Tropical"] = "Tropical",
                        ["Oasis"] = "Oasis",
                        ["Celestial"] = "Celestial",
                    },
                },
            },
        },

        // Decolored Squares
        [typeof(SDecoloredSquares)] = new()
        {
            ModuleName = "Обесцвеченных квадратах",
            ManualQuestions = new()
            {
                ["What were the colors defining the starting row and column?"] = "Какие цвета отвечали за начальную строку и столбец?",
            },
            Questions = new()
            {
                [SDecoloredSquares.StartingPos] = new()
                {
                    // English: What was the starting {1} defining color in {0}?
                    // Example: What was the starting column defining color in Decolored Squares?
                    Question = "Какой цвет определил {1} схемы на {0}?",
                    Conjugation = Conjugation.PrepositivePlural,
                    Arguments = new()
                    {
                        ["column"] = "начальный столбец",
                        ["row"] = "начальную строку",
                    },
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

        // Decolour Flash
        [typeof(SDecolourFlash)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the words and colours of each goal?"] = "Какие были слова и цвета у каждой цели?",
            },
            Questions = new()
            {
                [SDecolourFlash.QGoal] = new()
                {
                    // English: What was the {1} of the {2} goal in {0}?
                    // Example: What was the colour of the first goal in Decolour Flash?
                    Question = "{1} у {2}-й цели {0}?",
                    Arguments = new()
                    {
                        ["colour"] = "Какой был цвет",
                        ["word"] = "Какое было слово",
                    },
                    Answers = new()
                    {
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Red"] = "Red",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Yellow",
                        ["White"] = "White",
                    },
                },
            },
            Discriminators = new()
            {
                [SDecolourFlash.DGoal] = new()
                {
                    // English: the Decolour Flash where the {0} of the {1} goal was {2}
                    // Example: the Decolour Flash where the word of the first goal was blue
                    Discriminator = "the Decolour Flash where the {0} of the {1} goal was {2}",
                    Arguments = new()
                    {
                        ["word"] = "word",
                        ["colour"] = "colour",
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["red"] = "red",
                        ["magenta"] = "magenta",
                        ["yellow"] = "yellow",
                        ["white"] = "white",
                    },
                },
            },
        },

        // Denial Displays
        [typeof(SDenialDisplays)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was initially on the displays?"] = "Что изначально было на экранах?",
            },
            Questions = new()
            {
                [SDenialDisplays.QDisplays] = new()
                {
                    // English: What number was initially shown on Display {1} in {0}?
                    // Example: What number was initially shown on Display A in Denial Displays?
                    Question = "Какое число было показано на экране {1} {0}?",
                },
            },
            Discriminators = new()
            {
                [SDenialDisplays.DDisplays] = new()
                {
                    // English: the Denial Displays where {0} was initially shown on Display {1}
                    // Example: the Denial Displays where 0 was initially shown on Display A
                    Discriminator = "the Denial Displays where {0} was initially shown on Display {1}",
                },
            },
        },

        // DetoNATO
        [typeof(SDetoNATO)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed word for each stage?"] = "Какое слово было показано на каждом этапе?",
            },
            Questions = new()
            {
                [SDetoNATO.Display] = new()
                {
                    // English: What was the {1} display in {0}?
                    // Example: What was the first display in DetoNATO?
                    Question = "Что было на дисплее на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Devilish Eggs
        [typeof(SDevilishEggs)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the rotations?"] = "Какие повороты были у яиц?",
                ["What were the numbers and letters shown on the prism?"] = "Какие числа и буквы были указаны на призме?",
            },
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

        // Dialtones
        [typeof(SDialtones)] = new()
        {
            ManualQuestions = new()
            {
                ["Which tones played?"] = "Какие тональные сигналы были проиграны?",
            },
            Questions = new()
            {
                [SDialtones.Dialtones] = new()
                {
                    // English: What dialtones were heard in {0}?
                    Question = "Какие тональные сигналы играли {0}?",
                },
            },
        },

        // Digisibility
        [typeof(SDigisibility)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the numbers on the buttons?"] = "Какие числа были на кнопках?",
            },
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

        // Digit String
        [typeof(SDigitString)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed digit string?"] = "Какая строка цифр была в начале?",
            },
            Questions = new()
            {
                [SDigitString.InitialNumber] = new()
                {
                    // English: What was the initial number in {0}?
                    Question = "Какое было исходное число {0}?",
                },
            },
        },

        // Dimension Disruption
        [typeof(SDimensionDisruption)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the visible characters?"] = "Какие символы можно было рассмотреть на модуле?",
            },
            Questions = new()
            {
                [SDimensionDisruption.QVisibleLetters] = new()
                {
                    // English: Which of these was a visible character in {0}?
                    Question = "Что из этого было видимым символом {0}?",
                },
            },
            Discriminators = new()
            {
                [SDimensionDisruption.DVisibleLetters] = new()
                {
                    // English: the Dimension Disruption where {0} was a visible character
                    // Example: the Dimension Disruption where A was a visible character
                    Discriminator = "the Dimension Disruption where {0} was a visible character",
                },
            },
        },

        // Directional Button
        [typeof(SDirectionalButton)] = new()
        {
            ModuleName = "Направляющей кнопки",
            ManualQuestions = new()
            {
                ["How many times did you press the button in each stage?"] = "Сколько раз вы нажали кнопку на каждом этапе?",
            },
            Questions = new()
            {
                [SDirectionalButton.ButtonCount] = new()
                {
                    // English: How many times did you press the button in the {1} stage of {0}?
                    // Example: How many times did you press the button in the first stage of Directional Button?
                    Question = "Сколько раз вы нажали кнопку на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveFeminine,
                },
            },
        },

        // Discolored Squares
        [typeof(SDiscoloredSquares)] = new()
        {
            ModuleName = "Бесцветных квадратах",
            ManualQuestions = new()
            {
                ["What was the remembered position for each color?"] = "В какой позиции находился каждый начальный цвет?",
            },
            Questions = new()
            {
                [SDiscoloredSquares.RememberedPositions] = new()
                {
                    // English: What was {1}’s remembered position in {0}?
                    // Example: What was Blue’s remembered position in Discolored Squares?
                    Question = "В какой позиции находился {1} квадрат в самом начале на {0}?",
                    Conjugation = Conjugation.PrepositivePlural,
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

        // Disordered Keys
        [typeof(SDisorderedKeys)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the missing information and the revealed/unrevealed key color, label, and label color of each key?"] = "Какой информации недоставало и какие раскрытый/нераскрытый цвет клавиши, надпись и цвет надписи были на каждом ключе?",
            },
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

        // Divided Squares
        [typeof(SDividedSquares)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What color was shown when the correct square was pressed?"] = "What color was shown when the correct square was pressed?",
            },
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Black"] = "Black",
                        ["White"] = "White",
                    },
                },
            },
        },

        // Divisible Numbers
        [typeof(SDivisibleNumbers)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the numbers in each stage?"] = "Какие числа были на каждом этапе?",
            },
            Questions = new()
            {
                [SDivisibleNumbers.Numbers] = new()
                {
                    // English: What was the {1} stage’s number in {0}?
                    // Example: What was the first stage’s number in Divisible Numbers?
                    Question = "Какое было число {1}-го этапа {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // DNA Mutation
        [typeof(SDNAMutation)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the DNA strands’ colors?"] = "What were the DNA strands’ colors?",
                ["What were the given nucleotides’ letters and colors?"] = "What were the given nucleotides’ letters and colors?",
            },
            Questions = new()
            {
                [SDNAMutation.Letter] = new()
                {
                    // English: What was the letter of the {1} given nucleotide in {0}?
                    // Example: What was the letter of the first given nucleotide in DNA Mutation?
                    Question = "What was the letter of the {1} given nucleotide in {0}?",
                },
                [SDNAMutation.NucleotideColor] = new()
                {
                    // English: What was the color of the {1} given nucleotide in {0}?
                    // Example: What was the color of the first given nucleotide in DNA Mutation?
                    Question = "What was the color of the {1} given nucleotide in {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                    },
                },
                [SDNAMutation.StrandColor] = new()
                {
                    // English: What was the color of the {1} given DNA strand in {0}?
                    // Example: What was the color of the first given DNA strand in DNA Mutation?
                    Question = "What was the color of the {1} given DNA strand in {0}?",
                    Answers = new()
                    {
                        ["Green"] = "Green",
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Blue"] = "Blue",
                    },
                },
            },
        },

        // Doofenshmirtz Evil Inc.
        [typeof(SDoofenshmirtzEvilInc)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What jingles played?"] = "What jingles played?",
                ["What images were shown?"] = "What images were shown?",
            },
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

        // Double Arrows
        [typeof(SDoubleArrows)] = new()
        {
            ModuleName = "Двойных стрелках",
            ManualQuestions = new()
            {
                ["What was the starting position?"] = "Где была начальная позиция?",
                ["Which buttons moved in which directions in the grid?"] = "Какие кнопки двигали в каком направлении?",
            },
            Questions = new()
            {
                [SDoubleArrows.Start] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "Какая была начальная позиция {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
                [SDoubleArrows.Movement] = new()
                {
                    // English: Which direction in the grid did the {1} arrow move in {0}?
                    // Example: Which direction in the grid did the inner up arrow move in Double Arrows?
                    Question = "В какую сторону вас переместила стрелка {1} {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
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
                    Answers = new()
                    {
                        ["Up"] = "Вверх",
                        ["Right"] = "Вправо",
                        ["Left"] = "Влево",
                        ["Down"] = "Вниз",
                    },
                },
                [SDoubleArrows.Arrow] = new()
                {
                    // English: Which {1} arrow moved {2} in the grid in {0}?
                    // Example: Which inner arrow moved up in the grid in Double Arrows?
                    Question = "Которая стрелка {1} переместила вас {2} {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                    Arguments = new()
                    {
                        ["inner"] = "внутри",
                        ["outer"] = "снаружи",
                        ["up"] = "вверх",
                        ["down"] = "вниз",
                        ["left"] = "влево",
                        ["right"] = "вправо",
                    },
                    Answers = new()
                    {
                        ["Up"] = "Вверх",
                        ["Right"] = "Вправо",
                        ["Left"] = "Влево",
                        ["Down"] = "Вниз",
                    },
                },
            },
        },

        // Double Color
        [typeof(SDoubleColor)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the screen color in each stage?"] = "Какой был цвет экрана на каждом этапе?",
            },
            Questions = new()
            {
                [SDoubleColor.Colors] = new()
                {
                    // English: What was the screen color on the {1} stage of {0}?
                    // Example: What was the screen color on the first stage of Double Color?
                    Question = "Какого цвета был экран на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Double Digits
        [typeof(SDoubleDigits)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the numbers on the displays?"] = "Какие числа были на экранах?",
            },
            Questions = new()
            {
                [SDoubleDigits.Displays] = new()
                {
                    // English: What was the digit on the {1} display in {0}?
                    // Example: What was the digit on the left display in Double Digits?
                    Question = "Какая цифра была на {1} дисплее {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                    Arguments = new()
                    {
                        ["left"] = "левом",
                        ["right"] = "правом",
                    },
                },
            },
        },

        // Double Expert
        [typeof(SDoubleExpert)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the starting key number?"] = "Какое было начальное ключевое число?",
            },
            Questions = new()
            {
                [SDoubleExpert.StartingKeyNumber] = new()
                {
                    // English: What was the starting key number in {0}?
                    Question = "Какое было начальное ключевое число {0}?",
                },
            },
        },

        // Double Listening
        [typeof(SDoubleListening)] = new()
        {
            ManualQuestions = new()
            {
                ["What sounds played?"] = "Какие звуки были проиграны?",
            },
            Questions = new()
            {
                [SDoubleListening.Sounds] = new()
                {
                    // English: What clip was played in {0}?
                    Question = "Какой звук был воспроизведён {0}?",
                },
            },
        },

        // Double Screen
        [typeof(SDoubleScreen)] = new()
        {
            ManualQuestions = new()
            {
                ["What colors were the screens in each stage?"] = "Каких цветов были экраны на каждом этапе?",
            },
            Questions = new()
            {
                [SDoubleScreen.Colors] = new()
                {
                    // English: What color was the {1} screen in the {2} stage of {0}?
                    // Example: What color was the top screen in the first stage of Double Screen?
                    Question = "Какого цвета был {1} экран на {2}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                    Arguments = new()
                    {
                        ["top"] = "верхний",
                        ["bottom"] = "нижний",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Yellow"] = "Жёлтый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                    },
                },
            },
        },

        // Dr. Doctor
        [typeof(SDrDoctor)] = new()
        {
            ManualQuestions = new()
            {
                ["Which diseases and symptoms were listed?"] = "Какие болезни и симптомы присутствовали на модуле?",
            },
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

        // Dreamcipher
        [typeof(SDreamcipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the decrypted word?"] = "Какое слово было расшифровано?",
            },
            Questions = new()
            {
                [SDreamcipher.Word] = new()
                {
                    // English: What was the decrypted word in {0}?
                    Question = "Какое было расшифрованное слово {0}?",
                },
            },
        },

        // The Duck
        [typeof(SDuck)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the color of the curtain?"] = "What was the color of the curtain?",
                ["How was the duck approached?"] = "How was the duck approached?",
            },
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

        // Dumb Waiters
        [typeof(SDumbWaiters)] = new()
        {
            ManualQuestions = new()
            {
                ["Which players were present?"] = "Какие игроки присутствовали?",
            },
            Questions = new()
            {
                [SDumbWaiters.PlayerAvailable] = new()
                {
                    // English: Which player {1} present in {0}?
                    // Example: Which player was present in Dumb Waiters?
                    Question = "Какой игрок {1} на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                    Arguments = new()
                    {
                        ["was"] = "присутствовал",
                        ["was not"] = "отсутствовал",
                    },
                },
            },
        },

        // Earthbound
        [typeof(SEarthbound)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the background?"] = "Какой был фон?",
                ["Which monster was shown?"] = "Какой монстр был показан?",
            },
            Questions = new()
            {
                [SEarthbound.Background] = new()
                {
                    // English: What was the background in {0}?
                    Question = "Какой был фон на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SEarthbound.Monster] = new()
                {
                    // English: Which monster was displayed in {0}?
                    Question = "Какой монстр был показан на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // eeB gnillepS
        [typeof(SEeBgnillepS)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What word was asked to be spelled?"] = "Какое слово нужно было прописать?",
            },
            Questions = new()
            {
                [SEeBgnillepS.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "What word was asked to be spelled in {0}?",
                },
            },
        },

        // Eight
        [typeof(SEight)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the last digit on the small display?"] = "Какая была последняя цифра на маленьком экране?",
                ["What was the position of the last broken digit?"] = "Какая была позиция у последней сломанной цифры?",
                ["What were the last resulting digits?"] = "Какие были две итоговые последние цифры?",
                ["What was the last displayed number?"] = "Какое было последнее показанное число?",
            },
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

        // Elder Futhark
        [typeof(SElderFuthark)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the runes shown?"] = "Какие руны были показаны?",
            },
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

        // Emoji
        [typeof(SEmoji)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the emoji?"] = "Какие были эмодзи?",
            },
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

        // ƎNA Cipher
        [typeof(SEnaCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the keyword, the transposition key and the encrypted word?"] = "Какое было ключевое слово, ключ транспозиции и зашифрованное слово?",
            },
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

        // Encrypted Dice
        [typeof(SEncryptedDice)] = new()
        {
            ManualQuestions = new()
            {
                ["Which numbers were rolled in each stage?"] = "Какие числа были на костях на каждом этапе?",
            },
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

        // Encrypted Equations
        [typeof(SEncryptedEquations)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the main shapes of the three operands?"] = "Какие были основные фигуры у трёх переменных?",
            },
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

        // Encrypted Hangman
        [typeof(SEncryptedHangman)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the encrypted module name and encryption method?"] = "Какой модуль был зашифрован и каким методом?",
            },
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

        // Encrypted Maze
        [typeof(SEncryptedMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["Which symbol was spinning which way?"] = "Какой символ крутился в какую сторону?",
            },
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

        // Encrypted Morse
        [typeof(SEncryptedMorse)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the received key?"] = "Какое сообщение было получено и отправлено?",
            },
            Questions = new()
            {
                [SEncryptedMorse.Key] = new()
                {
                    // English: What was the received key in {0}?
                    Question = "What was the received key in {0}?",
                },
            },
        },

        // Encryption Bingo
        [typeof(SEncryptionBingo)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the first encoding used?"] = "Какая кодировка была применена первой?",
            },
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
                        ["Galactic Alphabet"] = "Galactic Alphabet",
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

        // English Entries
        [typeof(SEnglishEntries)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the displayed quote?"] = "What was the displayed quote?",
            },
            Questions = new()
            {
                [SEnglishEntries.Display] = new()
                {
                    // English: What was the displayed quote on {0}?
                    Question = "What was the displayed quote on {0}?",
                },
            },
        },

        // Enigma Cycle
        [typeof(SEnigmaCycle)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which direction were the dials pointing?"] = "Какое было сообщение и ответ?",
                ["What was written on each dial?"] = "What was written on each dial?",
            },
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
            Discriminators = new()
            {
                [SEnigmaCycle.LabelDiscriminator] = new()
                {
                    // English: the Enigma Cycle that had the letter {0} on a dial
                    // Example: the Enigma Cycle that had the letter A on a dial
                    Discriminator = "the Enigma Cycle that had the letter {0} on a dial",
                },
            },
        },

        // Entry Number Four
        [typeof(SEntryNumberFour)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the numbers shown?"] = "Какие числа были показаны, ожидаемый четвёртый ввод и коэффициент константы?",
            },
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

        // Entry Number One
        [typeof(SEntryNumberOne)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the numbers shown?"] = "Какие числа были показаны, ожидаемый первый ввод и коэффициент константы?",
            },
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

        // Equations X
        [typeof(SEquationsX)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed symbol?"] = "Какой символ был показан?",
            },
            Questions = new()
            {
                [SEquationsX.Symbols] = new()
                {
                    // English: What was the displayed symbol in {0}?
                    Question = "Какой символ был показан на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Error Codes
        [typeof(SErrorCodes)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the active error code?"] = "Какой был активный код ошибки?",
            },
            Questions = new()
            {
                [SErrorCodes.ActiveError] = new()
                {
                    // English: What was the active error code in {0}?
                    Question = "Какой код был активным на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Etterna
        [typeof(SEtterna)] = new()
        {
            ManualQuestions = new()
            {
                ["What beat was the input for each arrow?"] = "На каком бите была введена каждая стрелка?",
            },
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

        // Exoplanets
        [typeof(SExoplanets)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the starting and final target planet and digit?"] = "Какая была начальная и целевая планета и цифра?",
            },
            Questions = new()
            {
                [SExoplanets.StartingTargetPlanet] = new()
                {
                    // English: What was the starting target planet in {0}?
                    Question = "Какая была начальная целевая планета из {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SExoplanets.TargetPlanet] = new()
                {
                    // English: What was the final target planet in {0}?
                    Question = "Какая была финальная целевая планета из {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Factoring Maze
        [typeof(SFactoringMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the prime numbers used?"] = "Какие простые числа были использованы?",
            },
            Questions = new()
            {
                [SFactoringMaze.ChosenPrimes] = new()
                {
                    // English: What was one of the prime numbers chosen in {0}?
                    Question = "Какое из простых чисел было выбрано {0}?",
                },
            },
        },

        // Factory Maze
        [typeof(SFactoryMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["What room did you start in?"] = "С какой комнаты вы начали свой путь?",
            },
            Questions = new()
            {
                [SFactoryMaze.StartRoom] = new()
                {
                    // English: What room did you start in in {0}?
                    Question = "Какая была начальная комната {0}?",
                },
            },
        },

        // Faerie Fires
        [typeof(SFaerieFires)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the faeries' colors and pitches?"] = "What were the faeries' colors and pitches?",
            },
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

        // Fast Math
        [typeof(SFastMath)] = new()
        {
            ModuleName = "Быстрой математике",
            ManualQuestions = new()
            {
                ["What was the last pair of letters?"] = "Какая пара букв была последней?",
            },
            Questions = new()
            {
                [SFastMath.LastLetters] = new()
                {
                    // English: What was the last pair of letters in {0}?
                    Question = "Какая пара букв была последней {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
            },
        },

        // Fast Playfair Cipher
        [typeof(SFastPlayfairCipher)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the last displayed message?"] = "What was the last displayed message?",
            },
            Questions = new()
            {
                [SFastPlayfairCipher.LastMessage] = new()
                {
                    // English: What was the last displayed message in {0}?
                    Question = "What was the last displayed message in {0}?",
                },
            },
        },

        // Faulty Buttons
        [typeof(SFaultyButtons)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which button did each button refer to?"] = "На какую кнопку ссылалась каждая кнопка?",
            },
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

        // Faulty RGB Maze
        [typeof(SFaultyRGBMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["Where were the exit and keys of the maze?"] = "Где были выход и ключи лабиринта?",
                ["What was the maze number for each maze?"] = "Какой был номер у каждого лабиринта?",
            },
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

        // Find The Date
        [typeof(SFindTheDate)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the day, month, and year for each stage?"] = "What were the day, month, and year for each stage?",
            },
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

        // Five Letter Words
        [typeof(SFiveLetterWords)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the displayed words?"] = "Какие слова были показаны?",
            },
            Questions = new()
            {
                [SFiveLetterWords.DisplayedWords] = new()
                {
                    // English: Which of these words was on the display in {0}?
                    Question = "Какое из этих слов было на экране {0}?",
                },
            },
        },

        // FizzBuzz
        [typeof(SFizzBuzz)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the displayed numbers?"] = "Какие числа были показаны?",
            },
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

        // Flags
        [typeof(SFlags)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the country flags, the main flag, and the displayed number?"] = "Какие были флаги стран, главный флаг и показанное число?",
            },
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

        // Flashing Arrows
        [typeof(SFlashingArrows)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the number shown on the display?"] = "Какое число было показано на экране?",
                ["What were the relevant arrow’s colors?"] = "Какие цвета мигали на соответствующих стрелках?",
            },
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
                    Arguments = new()
                    {
                        ["before"] = "перед чёрным",
                        ["after"] = "после чёрного",
                    },
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
        },

        // Flashing Lights
        [typeof(SFlashingLights)] = new()
        {
            ManualQuestions = new()
            {
                ["How many times did each LED flash each color?"] = "Сколько раз каждый цвет загорелся на каждом светодиоде",
            },
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
                        ["bottom"] = "нижний",
                        ["cyan"] = "голубым",
                        ["green"] = "зелёным",
                        ["red"] = "красным",
                        ["purple"] = "фиолетовым",
                        ["orange"] = "оранжевым",
                    },
                },
            },
        },

        // Flavor Text EX
        [typeof(SFlavorTextEX)] = new()
        {
            ManualQuestions = new()
            {
                ["Which module’s flavor text was presented in each stage?"] = "От какого модуля был приведён флейвор текст на каждом этапе?",
            },
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

        // Flyswatting
        [typeof(SFlyswatting)] = new()
        {
            ManualQuestions = new()
            {
                ["Which flies were present, but not in the solution?"] = "Какие мухи присутствовали, но не в решении?",
            },
            Questions = new()
            {
                [SFlyswatting.Unpressed] = new()
                {
                    // English: Which fly was present, but not in the solution in {0}?
                    Question = "Какая муха присутствовала, но не была частью решения {0}?",
                },
            },
        },

        // Follow Me
        [typeof(SFollowMe)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the flashing directions?"] = "Какие направления мигали?",
            },
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

        // Forest Cipher
        [typeof(SForestCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Forget Any Color
        [typeof(SForgetAnyColor)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What colors were the cylinders in each stage?"] = "Каких цветов были цилиндры на каждом этапе?",
                ["Which figure was used in each stage?"] = "Какая строка была применена на каждом этапе?",
            },
            Questions = new()
            {
                [SForgetAnyColor.QCylinder] = new()
                {
                    // English: What colors were the cylinders during the {1} stage of {0}?
                    // Example: What colors were the cylinders during the first stage of Forget Any Color?
                    Question = "Какие были цилиндры на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                    // Refer to translations.md to understand the weird strings
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Forget Everything
        [typeof(SForgetEverything)] = new()
        {
            ModuleName = "Полного забвения",
            ManualQuestions = new()
            {
                ["What were the displayed digits in stage one?"] = "Какие цифры были на первом этапе?",
            },
            Questions = new()
            {
                [SForgetEverything.QStageOneDisplay] = new()
                {
                    // English: What was the {1} displayed digit in the first stage of {0}?
                    // Example: What was the first displayed digit in the first stage of Forget Everything?
                    Question = "Какая была {1}-я отображённая цифра на первом этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Forget Me
        [typeof(SForgetMe)] = new()
        {
            ManualQuestions = new()
            {
                ["What numbers were in which positions in the initial puzzle?"] = "Какие числа были в какой позиции в начале пазла?",
            },
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

        // Forget Me Not
        [typeof(SForgetMeNot)] = new()
        {
            ModuleName = "Незабудки",
            ManualQuestions = new()
            {
                ["What were the displayed digits in each stage?"] = "Какие цифры были показаны на каждом этапе?",
            },
            Questions = new()
            {
                [SForgetMeNot.Question] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Forget Me Not?
                    Question = "Какая цифра была отображена на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveFeminine,
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

        // Forget Me Now
        [typeof(SForgetMeNow)] = new()
        {
            ModuleName = "Забудке",
            ManualQuestions = new()
            {
                ["What were the displayed digits?"] = "Какие цифры были показаны?",
            },
            Questions = new()
            {
                [SForgetMeNow.DisplayedDigits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Forget Me Now?
                    Question = "Какая была {1}-я отображённая цифра на {0}?",
                    Conjugation = Conjugation.PrepositiveFeminine,
                },
            },
        },

        // Forget Our Voices
        [typeof(SForgetOurVoices)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was said by whom each stage?"] = "What was said by whom each stage?",
            },
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
                },
            },
        },

        // Forget’s Ultimate Showdown
        [typeof(SForgetsUltimateShowdown)] = new()
        {
            ModuleName = "Финальной битве забвения",
            ManualQuestions = new()
            {
                ["What was the initial number?"] = "Какое было исходное число?",
                ["What was the bottom number?"] = "Какое число было снизу?",
                ["What was the answer?"] = "Какой был ответ?",
                ["What were the encryption methods used?"] = "Какие методы шифрования были использованы?",
            },
            Questions = new()
            {
                [SForgetsUltimateShowdown.Answer] = new()
                {
                    // English: What was the {1} digit of the answer in {0}?
                    // Example: What was the first digit of the answer in Forget’s Ultimate Showdown?
                    Question = "Какая была {1}-я цифра финального кода {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
                [SForgetsUltimateShowdown.Bottom] = new()
                {
                    // English: What was the {1} digit of the bottom number in {0}?
                    // Example: What was the first digit of the bottom number in Forget’s Ultimate Showdown?
                    Question = "Какая была {1}-я цифра нижнего числа {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
                [SForgetsUltimateShowdown.Initial] = new()
                {
                    // English: What was the {1} digit of the initial number in {0}?
                    // Example: What was the first digit of the initial number in Forget’s Ultimate Showdown?
                    Question = "Какая была {1}-я цифра изначального кодового числа {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
                [SForgetsUltimateShowdown.Method] = new()
                {
                    // English: What was the {1} method used in {0}?
                    // Example: What was the first method used in Forget’s Ultimate Showdown?
                    Question = "Какой был {1}-й использованный метод {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
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

        // Forget The Colors
        [typeof(SForgetTheColors)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "\"Забудь цвета\"",
            ManualModuleName = "Забудь цвета",
            ManualQuestions = new()
            {
                ["What were the large display, gear and the sine number’s last digit in each stage?"] = "Какая последняя цифра была указана на экране/шестерёнке/синусе на каждом этапе?",
                ["Which edgework-based rule was applied in each stage?"] = "Какая информация с бомбы была применена на каждом этапе?",
            },
            Questions = new()
            {
                [SForgetTheColors.QGearNumber] = new()
                {
                    // English: What number was on the gear during stage {1} of {0}?
                    // Example: What number was on the gear during stage 0 of Forget The Colors?
                    Question = "Какое число было на шестерёнке на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SForgetTheColors.QLargeDisplay] = new()
                {
                    // English: What number was on the large display during stage {1} of {0}?
                    // Example: What number was on the large display during stage 0 of Forget The Colors?
                    Question = "Какое число было на большом экране на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SForgetTheColors.QSineNumber] = new()
                {
                    // English: What was the last decimal in the sine number received during stage {1} of {0}?
                    // Example: What was the last decimal in the sine number received during stage 0 of Forget The Colors?
                    Question = "Какая была последняя дробная цифра полученного числа синуса на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SForgetTheColors.QGearColor] = new()
                {
                    // English: What color was the gear during stage {1} of {0}?
                    // Example: What color was the gear during stage 0 of Forget The Colors?
                    Question = "Какого цвета была шестерёнка на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Cyan"] = "Cyan",
                        ["Blue"] = "Blue",
                        ["Purple"] = "Purple",
                        ["Pink"] = "Pink",
                        ["Maroon"] = "Maroon",
                        ["White"] = "White",
                        ["gear color"] = "gear color",
                        ["rule color"] = "rule color",
                    },
                },
            },
        },

        // Forget This
        [typeof(SForgetThis)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the displayed digit and color in each stage?"] = "Какая цифра и цвет были показаны на каждом этапе?",
            },
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

        // Forget Us Not
        [typeof(SForgetUsNot)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which module was solved for each stage?"] = "Which module was solved for each stage?",
            },
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

        // Free Parking
        [typeof(SFreeParking)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the player token?"] = "Какой был жетон игрока?",
            },
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

        // The French Republican Calendar
        [typeof(SFrenchRepublicanCalendar)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the color of the LED?"] = "What was the color of the LED?",
            },
            Questions = new()
            {
                [SFrenchRepublicanCalendar.LEDColor] = new()
                {
                    // English: What was the color of the LED in {0}?
                    Question = "What was the color of the LED in {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                    },
                },
            },
        },

        // Functions
        [typeof(SFunctions)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the last digit of the first query result?"] = "Какая последняя цифра была у результата первого запроса?",
                ["What were the numbers and letter shown at the bottom?"] = "Какие числа и буквы были показаны снизу?",
            },
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

        // The Fuse Box
        [typeof(SFuseBox)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What colors flashed?"] = "What colors flashed?",
                ["What arrows were correct?"] = "What arrows were correct?",
            },
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

        // Gadgetron Vendor
        [typeof(SGadgetronVendor)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the displayed weapons?"] = "Какие оружия были показаны на модуле?",
            },
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

        // Game of Life Cruel
        [typeof(SGameOfLifeCruel)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which color combinations occurred?"] = "Which color combinations occurred?",
            },
            Questions = new()
            {
                [SGameOfLifeCruel.Colors] = new()
                {
                    // English: Which of these was a color combination that occurred in {0}?
                    Question = "Какие комбинации цветов присутствовали {0}?",
                },
            },
        },

        // The Gamepad
        [typeof(SGamepad)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Геймпада",
            ManualQuestions = new()
            {
                ["What were the numbers?"] = "Какие числа были на экране?",
            },
            Questions = new()
            {
                [SGamepad.QNumbers] = new()
                {
                    // English: What was the {1} digit on the display on {0}?
                    // Example: What was the first digit on the display on The Gamepad?
                    Question = "What was the {1} digit on the display on {0}?",
                },
            },
            Discriminators = new()
            {
                [SGamepad.DNumbers] = new()
                {
                    // English: the Gamepad whose {1} digit on the display was {0}
                    // Example: the Gamepad whose first digit on the display was 0
                    Discriminator = "the Gamepad whose {1} digit on the display was {0}",
                },
            },
        },

        // Garfield Kart
        [typeof(SGarfieldKart)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the name of the track?"] = "Какое было название трассы?",
                ["How many puzzle pieces were there?"] = "Сколько было частей пазла?",
            },
            Questions = new()
            {
                [SGarfieldKart.Track] = new()
                {
                    // English: What was the track in {0}?
                    Question = "Какая была трасса на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SGarfieldKart.PuzzleCount] = new()
                {
                    // English: How many puzzle pieces did {0} have?
                    Question = "Сколько было частей пазла на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // The Garnet Thief
        [typeof(SGarnetThief)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which faction did each competitor claim to have chosen?"] = "Which faction did each competitor claim to have chosen?",
            },
            Questions = new()
            {
                [SGarnetThief.Claim] = new()
                {
                    // English: Which faction did {1} claim to be in {0}?
                    // Example: Which faction did Jungmoon claim to be in The Garnet Thief?
                    Question = "К какой фракции {1} заявлял, что он принадлежит {0}?",
                },
            },
        },

        // Ghost Movement
        [typeof(SGhostMovement)] = new()
        {
            ManualQuestions = new()
            {
                ["Where were Pac-Man and the ghosts?"] = "Где был Pac-Man и призраки?",
            },
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

        // Giants Cipher
        [typeof(SGiantsCipher)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the displayed keyword?"] = "What was the displayed keyword?",
            },
            Questions = new()
            {
                [SGiantsCipher.Keywords] = new()
                {
                    // English: What was the displayed keyword in {0}?
                    Question = "What was the displayed keyword in {0}?",
                },
            },
        },

        // Girlfriend
        [typeof(SGirlfriend)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the chosen language?"] = "Какой язык был выбран?",
            },
            Questions = new()
            {
                [SGirlfriend.Language] = new()
                {
                    // English: What was the language sung in {0}?
                    Question = "На каком языке была песня {0}?",
                },
            },
        },

        // The Glitched Button
        [typeof(SGlitchedButton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the cycling bit sequence?"] = "What was the cycling bit sequence?",
            },
            Questions = new()
            {
                [SGlitchedButton.Sequence] = new()
                {
                    // English: What was the cycling bit sequence in {0}?
                    Question = "Какая последовательность битов повторялась на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Goofy’s Game
        [typeof(SGoofysGame)] = new()
        {
            ManualQuestions = new()
            {
                ["What numbers were shown in Morse code?"] = "Какие числа были показаны в Морзе?",
            },
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

        // Grand Piano
        [typeof(SGrandPiano)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the given notes?"] = "Какие ноты были даны?",
            },
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

        // The Gray Button
        [typeof(SGrayButton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the coordinates on the display?"] = "What were the coordinates on the display?",
            },
            Questions = new()
            {
                [SGrayButton.Coordinates] = new()
                {
                    // English: What was the {1} coordinate on the display in {0}?
                    // Example: What was the horizontal coordinate on the display in The Gray Button?
                    Question = "Какие были {1} координаты на экране {0}?",
                    Arguments = new()
                    {
                        ["horizontal"] = "горизонтальные",
                        ["vertical"] = "вертикальные",
                    },
                },
            },
        },

        // Gray Cipher
        [typeof(SGrayCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // The Great Void
        [typeof(SGreatVoid)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the digits and colors?"] = "What were the digits and colors?",
            },
            Questions = new()
            {
                [SGreatVoid.Digit] = new()
                {
                    // English: What was the {1} digit in {0}?
                    // Example: What was the first digit in The Great Void?
                    Question = "Какая была {1}-я цифра {0}?",
                },
                [SGreatVoid.Color] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in The Great Void?
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

        // Green Arrows
        [typeof(SGreenArrows)] = new()
        {
            ModuleName = "Зелёных стрелках",
            ManualQuestions = new()
            {
                ["What was the last number on the display?"] = "Какое последнее число было показано на экране?",
            },
            Questions = new()
            {
                [SGreenArrows.LastScreen] = new()
                {
                    // English: What was the last number on the display on {0}?
                    Question = "Какое последнее число было показано на экране {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
        },

        // The Green Button
        [typeof(SGreenButton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the submitted word?"] = "What was the submitted word?",
            },
            Questions = new()
            {
                [SGreenButton.Word] = new()
                {
                    // English: What was the word submitted in {0}?
                    Question = "Какое слово было введено на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Green Cipher
        [typeof(SGreenCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом этапе?",
            },
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

        // Gridlock
        [typeof(SGridlock)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the starting color?"] = "Какой был начальный цвет?",
                ["What was the starting position?"] = "Где было начало и конец?",
            },
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
            },
        },

        // Grocery Store
        [typeof(SGroceryStore)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the first item shown?"] = "Какой первый товар был показан?",
            },
            Questions = new()
            {
                [SGroceryStore.FirstItem] = new()
                {
                    // English: What was the first item shown in {0}?
                    Question = "Какой товар был показан первым {0}?",
                },
            },
        },

        // Gryphons
        [typeof(SGryphons)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the gryphon’s name and age?"] = "Какое имя и возраст были у грифона??",
            },
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

        // Guess Who?
        [typeof(SGuessWho)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which colors flashed “YES”?"] = "Сколько раз горело YES?",
            },
            Questions = new()
            {
                [SGuessWho.Colors] = new()
                {
                    // English: Did {1} flash “YES” in {0}?
                    // Example: Did Red flash “YES” in Guess Who??
                    Question = "Did {1} flash “YES” in {0}?",
                    Arguments = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Violet"] = "Violet",
                        ["Cyan"] = "Cyan",
                        ["Pink"] = "Pink",
                    },
                    Answers = new()
                    {
                        ["Yes"] = "Yes",
                        ["No"] = "No",
                    },
                },
            },
        },

        // Gyromaze
        [typeof(SGyromaze)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the LED colors?"] = "Каких цветов были светодиоды?",
            },
            Questions = new()
            {
                [SGyromaze.LEDColor] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the top LED in Gyromaze?
                    Question = "Какого цвета был {1} светодиод {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхний",
                        ["bottom"] = "нижний",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Blue"] = "Синий",
                        ["Green"] = "Зелёный",
                        ["Yellow"] = "Жёлтый",
                    },
                },
            },
        },

        // h
        [typeof(SH)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the transmitted letter?"] = "Какая буква передавалась модулем?",
            },
            Questions = new()
            {
                [SH.Letter] = new()
                {
                    // English: What was the transmitted letter in {0}?
                    Question = "Какая буква передавалась в \"{0}\"?",
                },
            },
        },

        // Halli Galli
        [typeof(SHalliGalli)] = new()
        {
            ManualQuestions = new()
            {
                ["Which fruit was there five of and in what counts?"] = "Каких фруктов было пять?",
            },
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

        // Hereditary Base Notation
        [typeof(SHereditaryBaseNotation)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the number on the bottom display?"] = "Какое число было на нижнем экране?",
            },
            Questions = new()
            {
                [SHereditaryBaseNotation.InitialNumber] = new()
                {
                    // English: What was the given number in {0}?
                    Question = "Какое число было дано {0}?",
                },
            },
        },

        // The Hexabutton
        [typeof(SHexabutton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What color was the button when held?"] = "What color was the button when held?",
                ["What Morse Code letter was transmitted?"] = "What Morse Code letter was transmitted?",
            },
            Questions = new()
            {
                [SHexabutton.Color] = new()
                {
                    // English: What was {1} of {0} when it was held?
                    // Example: What was the color of The Hexabutton when it was held?
                    Question = "What was the color of {0} when it was held?",
                    Arguments = new()
                    {
                        ["the color"] = "the color",
                        ["the flickering color"] = "the flickering color",
                    },
                },
                [SHexabutton.Letter] = new()
                {
                    // English: What Morse Code letter was transmitted by {0}?
                    Question = "What Morse Code letter was transmitted by {0}?",
                },
            },
        },

        // Hexamaze
        [typeof(SHexamaze)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Гексабиринте",
            ManualQuestions = new()
            {
                ["What was the color of the pawn?"] = "Какого цвета была пешка?",
            },
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

        // hexOrbits
        [typeof(SHexOrbits)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What shapes were displayed?"] = "What shapes were displayed?",
            },
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

        // hexOS
        [typeof(SHexOS)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the deciphered letters or phrase?"] = "Какие буквы или фраза были расшифрованы??",
                ["What were the 3-digit numbers cycled by the screen?"] = "Какие трёхзначные числа повторялись на экране?",
                ["What were the rhythm values?"] = "Какие значения были у ритмов?",
            },
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

        // Hickory Dickory Dock
        [typeof(SHickoryDickoryDock)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What times were shown when the clock struck?"] = "What times were shown when the clock struck?",
            },
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

        // Hidden Colors
        [typeof(SHiddenColors)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the main LED’s color?"] = "Какой цвет был у главного светодиода?",
            },
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

        // The Hidden Value
        [typeof(SHiddenValue)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What numbers and colors were displayed?"] = "What numbers and colors were displayed?",
            },
            Questions = new()
            {
                [SHiddenValue.Display] = new()
                {
                    // English: What was displayed on {0}?
                    Question = "Что было отображено на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                    // Refer to translations.md to understand the weird strings
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

        // The High Score
        [typeof(SHighScore)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the player’s position and score?"] = "What was the player’s position and score?",
            },
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

        // Hill Cycle
        [typeof(SHillCycle)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which direction were the dials pointing?"] = "Какое было сообщение и ответ?",
                ["What was written on each dial?"] = "What was written on each dial?",
            },
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
            Discriminators = new()
            {
                [SHillCycle.LabelDiscriminator] = new()
                {
                    // English: the Hill Cycle that had the letter {0} on a dial
                    // Example: the Hill Cycle that had the letter A on a dial
                    Discriminator = "the Hill Cycle that had the letter {0} on a dial",
                },
            },
        },

        // Hinges
        [typeof(SHinges)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Петлях",
            ManualQuestions = new()
            {
                ["What were the initially present hinges?"] = "Какие петли изначально присутствовали?",
            },
            Questions = new()
            {
                [SHinges.Initial] = new()
                {
                    // English: Which of these hinges was initially {1} {0}?
                    // Example: Which of these hinges was initially present on Hinges?
                    Question = "Какие из петель изначально {1} {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
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

        // Hogwarts
        [typeof(SHogwarts)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which House was each module solved for?"] = "Для каких из домов был решён каждый модуль?",
            },
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

        // Hold Ups
        [typeof(SHoldUps)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the name of each shadow shown?"] = "Какое было имя у каждой показанной тени?",
            },
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

        // Holographic Memory
        [typeof(SHolographicMemory)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which side did each symbol appear?"] = "Which side did each symbol appear?",
            },
            Questions = new()
            {
                [SHolographicMemory.InitialGrid] = new()
                {
                    // English: Which side did this symbol appear in {0}? (+ sprite)
                    Question = "Which side did this symbol appear in {0}?",
                    Answers = new()
                    {
                        ["Light"] = "Light",
                        ["Dark"] = "Dark",
                    },
                },
            },
        },

        // Homophones
        [typeof(SHomophones)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the displayed phrases?"] = "Какие фразы были показаны?",
            },
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

        // Horrible Memory
        [typeof(SHorribleMemory)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the colors and labels of each button in each stage?"] = "Какие позиции, надписи и цвета были у нажатых кнопок на каждом этапе?",
                ["What digit was displayed in each stage?"] = "What digit was displayed in each stage?",
            },
            Questions = new()
            {
                [SHorribleMemory.QColorsByPosition] = new()
                {
                    // English: What was the color of the button in the {1} position in the {2} stage of {0}?
                    // Example: What was the color of the button in the first position in the first stage of Horrible Memory?
                    Question = "What color was the button in the {1} position of the {2} stage of {0}?",
                    Answers = new()
                    {
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["red"] = "red",
                        ["orange"] = "orange",
                        ["purple"] = "purple",
                        ["pink"] = "pink",
                    },
                },
                [SHorribleMemory.QColorsByLabel] = new()
                {
                    // English: What was the color of the button labeled {1} in the {2} stage of {0}?
                    // Example: What was the color of the button labeled 1 in the first stage of Horrible Memory?
                    Question = "What color was the button labeled {1} in the {2} stage of {0}?",
                    Answers = new()
                    {
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["red"] = "red",
                        ["orange"] = "orange",
                        ["purple"] = "purple",
                        ["pink"] = "pink",
                    },
                },
                [SHorribleMemory.QLabelsByPosition] = new()
                {
                    // English: What was the label of the button in the {1} position in the {2} stage of {0}?
                    // Example: What was the label of the button in the first position in the first stage of Horrible Memory?
                    Question = "What was the label of the button in the {1} position of the {2} stage of {0}?",
                },
                [SHorribleMemory.QLabelsByColor] = new()
                {
                    // English: What was the label of the {1} button in the {2} stage of {0}?
                    // Example: What was the label of the blue button in the first stage of Horrible Memory?
                    Question = "What was the label of the {1} button in the {2} stage of {0}?",
                    Arguments = new()
                    {
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["red"] = "red",
                        ["orange"] = "orange",
                        ["purple"] = "purple",
                        ["pink"] = "pink",
                    },
                },
                [SHorribleMemory.QPositionsByColor] = new()
                {
                    // English: What was the position of the {1} button in the {2} stage of {0}?
                    // Example: What was the position of the blue button in the first stage of Horrible Memory?
                    Question = "What was the position of the {1} button in the {2} stage of {0}?",
                    Arguments = new()
                    {
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["red"] = "red",
                        ["orange"] = "orange",
                        ["purple"] = "purple",
                        ["pink"] = "pink",
                    },
                    Answers = new()
                    {
                        ["first"] = "first",
                        ["second"] = "second",
                        ["third"] = "third",
                        ["fourth"] = "fourth",
                        ["fifth"] = "fifth",
                        ["sixth"] = "sixth",
                    },
                },
                [SHorribleMemory.QPositionsByLabel] = new()
                {
                    // English: What was the position of the button labeled {1} in the {2} stage of {0}?
                    // Example: What was the position of the button labeled 1 in the first stage of Horrible Memory?
                    Question = "What was the position of the button labeled {1} in the {2} stage of {0}?",
                    Answers = new()
                    {
                        ["first"] = "first",
                        ["second"] = "second",
                        ["third"] = "third",
                        ["fourth"] = "fourth",
                        ["fifth"] = "fifth",
                        ["sixth"] = "sixth",
                    },
                },
                [SHorribleMemory.QDisplays] = new()
                {
                    // English: What number was displayed in the {1} stage of {0}?
                    // Example: What number was displayed in the first stage of Horrible Memory?
                    Question = "What number was displayed in the {1} stage of {0}?",
                },
            },
            Discriminators = new()
            {
                [SHorribleMemory.DColorAndPosition] = new()
                {
                    // English: the Horrible Memory that had a {0} button in the {1} position in the {2} stage
                    // Example: the Horrible Memory that had a blue button in the first position in the first stage
                    Discriminator = "the Horrible Memory that had a {0} button in the {1} position in the {2} stage",
                    Arguments = new()
                    {
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["red"] = "red",
                        ["orange"] = "orange",
                        ["purple"] = "purple",
                        ["pink"] = "pink",
                    },
                },
                [SHorribleMemory.DLabelAndPosition] = new()
                {
                    // English: the Horrible Memory that had a button labeled {0} in the {1} position in the {2} stage
                    // Example: the Horrible Memory that had a button labeled 1 in the first position in the first stage
                    Discriminator = "the Horrible Memory that had a button labeled {0} in the {1} position in the {2} stage",
                },
                [SHorribleMemory.DColorAndLabel] = new()
                {
                    // English: the Horrible Memory that had a {0} button labeled {1} in the {2} stage
                    // Example: the Horrible Memory that had a blue button labeled 1 in the first stage
                    Discriminator = "the Horrible Memory that had a {0} button labeled {1} in the {2} stage",
                    Arguments = new()
                    {
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["red"] = "red",
                        ["orange"] = "orange",
                        ["purple"] = "purple",
                        ["pink"] = "pink",
                    },
                },
                [SHorribleMemory.DDisplay] = new()
                {
                    // English: the Horrible Memory that displayed a {0} in the {1} stage
                    // Example: the Horrible Memory that displayed a 1 in the first stage
                    Discriminator = "the Horrible Memory that displayed a {0} in the {1} stage",
                },
            },
        },

        // Human Resources
        [typeof(SHumanResources)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which employees and applicants were present?"] = "Кто был уволен и нанят?",
                ["Which descriptors were shown in red and green?"] = "Какие описания были показаны в красном и зелёном?",
            },
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
                [SHumanResources.Employees] = new()
                {
                    // English: Who was {1} at the start of {0}?
                    // Example: Who was an employee at the start of Human Resources?
                    Question = "Who was {1} at the start of {0}?",
                    Arguments = new()
                    {
                        ["an employee"] = "an employee",
                        ["an applicant"] = "an applicant",
                    },
                },
            },
        },

        // Hunting
        [typeof(SHunting)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which pictograms were displayed?"] = "На каких этапах вначале были символы столбца или строки?",
            },
            Questions = new()
            {
                [SHunting.DisplayedSymbols] = new()
                {
                    // English: Which of these symbols was displayed in the {1} stage of {0}?
                    // Example: Which of these symbols was displayed in the first stage of Hunting?
                    Question = "Which of these symbols was displayed in the {1} stage of {0}?",
                },
            },
        },

        // The Hypercube
        [typeof(SHypercube)] = new()
        {
            ModuleName = "Гиперкуба",
            ManualQuestions = new()
            {
                ["What were the rotations?"] = "Какие были повороты у куба?",
            },
            Questions = new()
            {
                [SHypercube.Rotations] = new()
                {
                    // English: What was the {1} rotation in {0}?
                    // Example: What was the first rotation in The Hypercube?
                    Question = "Каким было {1}-е вращение {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // HyperForget
        [typeof(SHyperForget)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the rotations?"] = "What were the rotations?",
            },
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

        // The Hyperlink
        [typeof(SHyperlink)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the hyperlink?"] = "What was the hyperlink?",
                ["Which module was referenced?"] = "Which module was referenced?",
            },
            Questions = new()
            {
                [SHyperlink.Characters] = new()
                {
                    // English: What was the {1} character of the hyperlink in {0}?
                    // Example: What was the first character of the hyperlink in The Hyperlink?
                    Question = "Какой был {1}-й символ ссылки {0}?",
                },
                [SHyperlink.Answer] = new()
                {
                    // English: Which module was referenced on {0}?
                    Question = "На какой модуль ссылался {0}?",
                },
            },
        },

        // Ice Cream
        [typeof(SIceCream)] = new()
        {
            ModuleName = "Мороженого",
            ManualQuestions = new()
            {
                ["Who were the customers?"] = "Кем были ваши посетители?",
                ["Which flavors were on offer to each customer?"] = "Какие вкусы были предложены каждому посетителю?",
            },
            Questions = new()
            {
                [SIceCream.Flavour] = new()
                {
                    // English: Which one of these flavours {1} to the {2} customer in {0}?
                    // Example: Which one of these flavours was on offer, but not sold, to the first customer in Ice Cream?
                    Question = "Какой из этих вкусов {0} {1} {2}-му посетителю?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Identification Crisis
        [typeof(SIdentificationCrisis)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the shapes and identification modules used?"] = "Какие фигуры и опозновательные модули использованы на модуле?",
            },
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

        // Identity Parade
        [typeof(SIdentityParade)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the hair colors, builds and attires listed on the module?"] = "Какие цвета волос, строения тела и одеяния присутствовали?",
            },
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

        // Indigo Cipher
        [typeof(SIndigoCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Infinite Loop
        [typeof(SInfiniteLoop)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the selected word?"] = "Какое слово было выбрано?",
            },
            Questions = new()
            {
                [SInfiniteLoop.SelectedWord] = new()
                {
                    // English: What was the selected word in {0}?
                    Question = "Какое слово было выбрано {0}?",
                },
            },
        },

        // Ingredients
        [typeof(SIngredients)] = new()
        {
            ManualQuestions = new()
            {
                ["Which ingredients were listed and which were used?"] = "Какие ингредиенты были указаны и какие были использованы?",
            },
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

        // Inner Connections
        [typeof(SInnerConnections)] = new()
        {
            ManualQuestions = new()
            {
                ["What color was the LED?"] = "Какого цвета был светодиод?",
                ["What was the digit flashed in Morse?"] = "Какая цифра была передана при помощи Морзе?",
            },
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

        // Interpunct
        [typeof(SInterpunct)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the symbol displayed in each stage?"] = "Какой символ был показан на каждом этапе?",
            },
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

        // IPA
        [typeof(SIPA)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the correct symbol?"] = "Какой звук был проигран?",
            },
            Questions = new()
            {
                [SIPA.Sound] = new()
                {
                    // English: What sound played in {0}?
                    Question = "What sound played in {0}?",
                },
            },
        },

        // The iPhone
        [typeof(SiPhone)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the PIN?"] = "Какой был пинкод?",
            },
            Questions = new()
            {
                [SiPhone.Digits] = new()
                {
                    // English: What was the {1} PIN digit in {0}?
                    // Example: What was the first PIN digit in The iPhone?
                    Question = "Какая была {1}-я цифра пинкода {0}?",
                },
            },
        },

        // Jenga
        [typeof(SJenga)] = new()
        {
            ManualQuestions = new()
            {
                ["What symbols were on the first correctly pulled block?"] = "Какие символы были на первом правильно вытянутом блоке?",
            },
            Questions = new()
            {
                [SJenga.FirstBlock] = new()
                {
                    // English: Which symbol was on the first correctly pulled block in {0}?
                    Question = "Какой символ был на первом правильно вытянутом блоке {0}?",
                },
            },
        },

        // The Jewel Vault
        [typeof(SJewelVault)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which wheel spun another wheel, and which one did it spin?"] = "Какие числа были у колёс A–D?",
            },
            Questions = new()
            {
                [SJewelVault.WheelTurns] = new()
                {
                    // English: Which wheel turned as a result of turning wheel {1} in {0}?
                    // Example: Which wheel turned as a result of turning wheel 1 in The Jewel Vault?
                    Question = "Which wheel turned as a result of turning Wheel {1} in {0}?",
                    Answers = new()
                    {
                        ["1"] = "1",
                        ["2"] = "2",
                        ["3"] = "3",
                        ["4"] = "4",
                        ["none"] = "none",
                    },
                },
            },
        },

        // Jumble Cycle
        [typeof(SJumbleCycle)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which direction were the dials pointing?"] = "Какое было сообщение и ответ?",
                ["What was written on each dial?"] = "What was written on each dial?",
            },
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
            Discriminators = new()
            {
                [SJumbleCycle.LabelDiscriminator] = new()
                {
                    // English: the Jumble Cycle that had the letter {0} on a dial
                    // Example: the Jumble Cycle that had the letter A on a dial
                    Discriminator = "the Jumble Cycle that had the letter {0} on a dial",
                },
            },
        },

        // Juxtacolored Squares
        [typeof(SJuxtacoloredSquares)] = new()
        {
            ModuleName = "Смежных цветных квадратах",
            ManualQuestions = new()
            {
                ["What was each square’s color?"] = "Какой был цвет каждого квадрата?",
            },
            Questions = new()
            {
                [SJuxtacoloredSquares.ColorsByPosition] = new()
                {
                    // English: What was the color of this square in {0}? (+ sprite)
                    Question = "Какого цвета был этот квадрат на {0}?",
                    Conjugation = Conjugation.PrepositivePlural,
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
                    Conjugation = Conjugation.PrepositivePlural,
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

        // Kanji
        [typeof(SKanji)] = new()
        {
            ModuleName = "Кандзи",
            ManualQuestions = new()
            {
                ["What was the displayed word in each stage?"] = "Какое слово было показано на каждом этапе?",
            },
            Questions = new()
            {
                [SKanji.DisplayedWords] = new()
                {
                    // English: What was the displayed word in the {1} stage of {0}?
                    // Example: What was the displayed word in the first stage of Kanji?
                    Question = "Какое слово было показано на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // The Kanye Encounter
        [typeof(SKanyeEncounter)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What food items were shown?"] = "What food items were shown?",
            },
            Questions = new()
            {
                [SKanyeEncounter.Foods] = new()
                {
                    // English: What was a food item displayed in {0}?
                    Question = "Какая еда была показана {0}?",
                },
            },
        },

        // KayMazey Talk
        [typeof(SKayMazeyTalk)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the starting and goal words?"] = "What were the starting and goal words?",
            },
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

        // Keypad Combinations
        [typeof(SKeypadCombination)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the numbers, but not part of the answer?"] = "Какие были числа которые не являлись частью решения?",
            },
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

        // Keypad Magnified
        [typeof(SKeypadMagnified)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the position of the LED?"] = "Какая позиция была у светодиода?",
            },
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

        // Keypad Maze
        [typeof(SKeypadMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["Where were the yellow cells?"] = "Где были жёлтые клетки?",
            },
            Questions = new()
            {
                [SKeypadMaze.Yellow] = new()
                {
                    // English: Which of these cells was yellow in {0}?
                    Question = "Какая из этих клеток была жёлтой {0}?",
                },
            },
        },

        // Keypad Sequence
        [typeof(SKeypadSequence)] = new()
        {
            ManualQuestions = new()
            {
                ["What symbols were on the keys?"] = "Какие символы были на клавишах?",
            },
            Questions = new()
            {
                [SKeypadSequence.Labels] = new()
                {
                    // English: What was this key’s label on the {1} panel in {0}? (+ sprite)
                    // Example: What was this key’s label on the first panel in Keypad Sequence? (+ sprite)
                    Question = "Какая была надпись на этой кнопке на {1}-й панели {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Keywords
        [typeof(SKeywords)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the letters on the display?"] = "Какие буквы были на экране?",
            },
            Questions = new()
            {
                [SKeywords.DisplayedKey] = new()
                {
                    // English: What were the first four letters on the display in {0}?
                    Question = "Какие были первые четыре буквы на экране {0}?",
                },
            },
        },

        // The Klaxon
        [typeof(SKlaxon)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the first module to set off the klaxon?"] = "What was the first module to set off the klaxon?",
            },
            Questions = new()
            {
                [SKlaxon.FirstModule] = new()
                {
                    // English: What was the first module to set off {0}?
                    Question = "What was the first module to set off {0}?",
                },
            },
        },

        // Know Your Way
        [typeof(SKnowYourWay)] = new()
        {
            ManualQuestions = new()
            {
                ["Which way was the arrow pointing?"] = "Куда указывала стрелка?",
                ["Which LED was green?"] = "Какой светодиод горел зелёным?",
            },
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

        // Kooky Keypad
        [typeof(SKookyKeypad)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What colors were the LEDs?"] = "Каких цветов были светодиоды?",
            },
            Questions = new()
            {
                [SKookyKeypad.Color] = new()
                {
                    // English: What color was the {1} button’s LED in {0}?
                    // Example: What color was the top-left button’s LED in Kooky Keypad?
                    Question = "Какого цвета был светодиод на {1} кнопке {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "верхней левой",
                        ["top-right"] = "верхней правой",
                        ["bottom-left"] = "нижней левой",
                        ["bottom-right"] = "нижней правой",
                    },
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
                },
            },
        },

        // Kudosudoku
        [typeof(SKudosudoku)] = new()
        {
            ModuleName = "Кудосудоку",
            ManualQuestions = new()
            {
                ["Which squares were initially pre-filled?"] = "Which squares were initially pre-filled?",
            },
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

        // Kugelblitz
        [typeof(SKugelblitz)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which particles were present during each stage?"] = "Which particles were present during each stage?",
                ["What were the particles’ values during each stage?"] = "What were the particles’ values during each stage?",
            },
            Questions = new()
            {
                [SKugelblitz.BlackOrangeYellowIndigoViolet] = new()
                {
                    // English: Which particles were present for the {1} stage of {0}?
                    // Example: Which particles were present for the first stage of Kugelblitz?
                    Question = "Which particles were present for the {1} stage of {0}?",
                    // Refer to translations.md to understand the weird strings
                    Additional = new()
                    {
                        ["R"] = "R",
                        ["O"] = "O",
                        ["Y"] = "Y",
                        ["G"] = "G",
                        ["B"] = "B",
                        ["I"] = "I",
                        ["V"] = "V",
                        ["{0}{1}{2}{3}{4}{5}{6}"] = "{0}{1}{2}{3}{4}{5}{6}",
                        ["None"] = "None",
                    },
                },
                [SKugelblitz.RedGreenBlue] = new()
                {
                    // English: What were the particles’ values for the {1} stage of {0}?
                    // Example: What were the particles’ values for the first stage of Kugelblitz?
                    Question = "What were the particles’ values for the {1} stage of {0}?",
                    // Refer to translations.md to understand the weird strings
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
                        ["red"] = "red",
                        ["orange"] = "orange",
                        ["yellow"] = "yellow",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["indigo"] = "indigo",
                        ["violet"] = "violet",
                        ["one other Kugelblitz"] = "one other Kugelblitz",
                        ["two other Kugelblitzes"] = "two other Kugelblitzes",
                        ["three other Kugelblitzes"] = "three other Kugelblitzes",
                        ["four other Kugelblitzes"] = "four other Kugelblitzes",
                        ["five other Kugelblitzes"] = "five other Kugelblitzes",
                        ["six other Kugelblitzes"] = "six other Kugelblitzes",
                        ["seven other Kugelblitzes"] = "seven other Kugelblitzes",
                    },
                },
            },
        },

        // Kuro
        [typeof(SKuro)] = new()
        {
            ManualQuestions = new()
            {
                ["What was Kuro’s mood?"] = "Какое было настроение у Kuro?",
            },
            Questions = new()
            {
                [SKuro.Mood] = new()
                {
                    // English: What was Kuro’s mood in {0}?
                    Question = "Какое было настроение у Kuro {0}?",
                },
            },
        },

        // The Labyrinth
        [typeof(SLabyrinth)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Where were the portals, and which layers were they on?"] = "Where were the portals, and which layers were they on?",
            },
            Questions = new()
            {
                [SLabyrinth.PortalLocations] = new()
                {
                    // English: Where was one of the portals in layer {1} in {0}?
                    // Example: Where was one of the portals in layer 1 (Red) in The Labyrinth?
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

        // Ladder Lottery
        [typeof(SLadderLottery)] = new()
        {
            ManualQuestions = new()
            {
                ["Which light was on?"] = "Какая лампочка горела?",
            },
            Questions = new()
            {
                [SLadderLottery.LightOn] = new()
                {
                    // English: Which light was on in {0}?
                    Question = "Какая лампочка была включена {0}?",
                },
            },
        },

        // Ladders
        [typeof(SLadders)] = new()
        {
            ManualQuestions = new()
            {
                ["Which colors were present on the second ladder?"] = "Какие были цвета на второй лестнице?",
                ["What color was missing on the third ladder?"] = "Какой цвет отсутствовал на третьей лестнице?",
            },
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

        // Langton’s Anteater
        [typeof(SLangtonsAnteater)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the initial state of the grid?"] = "Какое было исходное состояние поля?",
            },
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

        // LED Encryption
        [typeof(SLEDEncryption)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which letters were present at each stage"] = "Какие правильные кнопки были нажаты?",
            },
            Questions = new()
            {
                [SLEDEncryption.PresentLetters] = new()
                {
                    // English: Which of these letters was present in the {1} stage of {0}?
                    // Example: Which of these letters was present in the first stage of LED Encryption?
                    Question = "What was the correct letter you pressed in the {1} stage of {0}?",
                },
            },
        },

        // LED Grid
        [typeof(SLEDGrid)] = new()
        {
            ModuleName = "Сетке светодиодов",
            ManualQuestions = new()
            {
                ["How many LEDs were unlit?"] = "Сколько светодиодов не горели?",
            },
            Questions = new()
            {
                [SLEDGrid.NumBlack] = new()
                {
                    // English: How many LEDs were unlit in {0}?
                    Question = "Сколько светодиодов не горело на {0}?",
                    Conjugation = Conjugation.PrepositiveFeminine,
                },
            },
        },

        // LED Math
        [typeof(SLEDMath)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the LED colors?"] = "Какие цвета были у светодиодов?",
            },
            Questions = new()
            {
                [SLEDMath.Lights] = new()
                {
                    // English: What color was {1} in {0}?
                    // Example: What color was LED A in LED Math?
                    Question = "Какого цвета был {1} {0}?",
                    Arguments = new()
                    {
                        ["LED A"] = "светодиод A",
                        ["LED B"] = "светодиод B",
                        ["the operator LED"] = "светодиод-оператор",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Blue"] = "Синего",
                        ["Yellow"] = "Жёлтого",
                        ["Green"] = "Зелёного",
                    },
                },
            },
        },

        // LEDs
        [typeof(SLEDs)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the initial color of the changed LED?"] = "Какой был исходный цвет у изменённого светодиода?",
            },
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

        // LEGOs
        [typeof(SLEGOs)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the dimensions of each color piece?"] = "Каких размеров и какого цвета была каждая деталь?",
            },
            Questions = new()
            {
                [SLEGOs.PieceDimensions] = new()
                {
                    // English: What were the dimensions of the {1} piece in {0}?
                    // Example: What were the dimensions of the red piece in LEGOs?
                    Question = "Каких размеров была {1} деталь {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Lempel-Ziv Cipher
        [typeof(SLempelZivCipher)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "What was on each screen?",
            },
            Questions = new()
            {
                [SLempelZivCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Lempel-Ziv Cipher?
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

        // Letter Math
        [typeof(SLetterMath)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the letters on the display?"] = "Какие буквы были на экране?",
            },
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

        // Light Bulbs
        [typeof(SLightBulbs)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the colors of the bulbs?"] = "Каких цветов были лампочки?",
            },
            Questions = new()
            {
                [SLightBulbs.Colors] = new()
                {
                    // English: What was the color of the {1} bulb in {0}?
                    // Example: What was the color of the left bulb in Light Bulbs?
                    Question = "Какой был цвет {1} лампочки {0}?",
                    Arguments = new()
                    {
                        ["left"] = "левой",
                        ["right"] = "правой",
                    },
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
                },
            },
        },

        // Lime Arrows
        [typeof(SLimeArrows)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the starting coordinate?"] = "What was the starting coordinate?",
            },
            Questions = new()
            {
                [SLimeArrows.Coordinates] = new()
                {
                    // English: What was the starting coordinate in {0}?
                    Question = "What was the starting coordinate in {0}?",
                },
            },
        },

        // Linq
        [typeof(SLinq)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the functions?"] = "Какие были функции?",
            },
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

        // Lion’s Share
        [typeof(SLionsShare)] = new()
        {
            ManualQuestions = new()
            {
                ["Which year was displayed?"] = "Какой год был показан?",
                ["Which lions were present but removed?"] = "Какие львы были изначально, но потом убраны?",
            },
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

        // Listening
        [typeof(SListening)] = new()
        {
            ModuleName = "Прослушке",
            ManualQuestions = new()
            {
                ["What sound played?"] = "Какая запись была проиграна?",
            },
            Questions = new()
            {
                [SListening.Sound] = new()
                {
                    // English: What clip was played in {0}?
                    Question = "Какой звук был воспроизведён {0}?",
                },
            },
        },

        // Literal Maze
        [typeof(SLiteralMaze)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which letter was in each position?"] = "Which letter was in each position?",
            },
            Questions = new()
            {
                [SLiteralMaze.Letter] = new()
                {
                    // English: Which letter was in this position in {0}?
                    Question = "Which letter was in this position in {0}?",
                },
            },
        },

        // Logical Buttons
        [typeof(SLogicalButtons)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the colors and labels of each button in each stage?"] = "Какие цвета и надписи были у кнопок на каждом этапе?",
                ["What were the final operators in each stage?"] = "Какая была финальная операция на каждом этапе?",
            },
            Questions = new()
            {
                [SLogicalButtons.Color] = new()
                {
                    // English: What was the color of the {1} button in the {2} stage of {0}?
                    // Example: What was the color of the top button in the first stage of Logical Buttons?
                    Question = "Какого цвета была {1} кнопка на {2}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                    Arguments = new()
                    {
                        ["top"] = "верхняя",
                        ["bottom-left"] = "нижняя левая",
                        ["bottom-right"] = "нижняя правая",
                    },
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
                },
                [SLogicalButtons.Label] = new()
                {
                    // English: What was the label on the {1} button in the {2} stage of {0}?
                    // Example: What was the label on the top button in the first stage of Logical Buttons?
                    Question = "Какая была надпись на {1} кнопке на {2}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Logic Gates
        [typeof(SLogicGates)] = new()
        {
            ModuleName = "Логических элементах",
            ManualQuestions = new()
            {
                ["What were logic gates A-D?"] = "Какие логические элементы присутствовали?",
            },
            Questions = new()
            {
                [SLogicGates.Gates] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was gate A in Logic Gates?
                    Question = "Каким был {1} {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                    Arguments = new()
                    {
                        ["gate A"] = "элемент A",
                        ["gate B"] = "элемент B",
                        ["gate C"] = "элемент C",
                        ["gate D"] = "элемент D",
                    },
                },
            },
        },

        // Lombax Cubes
        [typeof(SLombaxCubes)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the letters on the button?"] = "Какие буквы были на кнопке?",
            },
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

        // The London Underground
        [typeof(SLondonUnderground)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the departure and destination stations?"] = "What were the departure and destination stations?",
            },
            Questions = new()
            {
                [SLondonUnderground.Stations] = new()
                {
                    // English: Where did the {1} journey on {0} {2}?
                    // Example: Where did the first journey on The London Underground depart from?
                    Question = "{2} отправился {1}-й рейс {0}?",
                    Arguments = new()
                    {
                        ["depart from"] = "Откуда",
                        ["arrive to"] = "Куда",
                    },
                },
            },
        },

        // Long Words
        [typeof(SLongWords)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the word on the top display?"] = "Какое слово было на верхнем экране?",
            },
            Questions = new()
            {
                [SLongWords.Word] = new()
                {
                    // English: What was the word on the top display on {0}?
                    Question = "Какое слово было на верхнем экране {0}?",
                },
            },
        },

        // Mad Memory
        [typeof(SMadMemory)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on the display in each stage?"] = "Что было на экране на каждом этапе?",
            },
            Questions = new()
            {
                [SMadMemory.Displays] = new()
                {
                    // English: What was on the display in the {1} stage of {0}?
                    // Example: What was on the display in the first stage of Mad Memory?
                    Question = "Что было на экране на {1} этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Mafia
        [typeof(SMafia)] = new()
        {
            ModuleName = "Мафии",
            ManualQuestions = new()
            {
                ["Who was a player, but not the Godfather?"] = "Кто был игроком, но не был крёстным отцом?",
            },
            Questions = new()
            {
                [SMafia.Players] = new()
                {
                    // English: Who was a player, but not the Godfather, in {0}?
                    Question = "Кто был игроком, но не являлся крёстным отцом {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
            },
        },

        // Magenta Cipher
        [typeof(SMagentaCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Mahjong
        [typeof(SMahjong)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the bottom-left tile?"] = "Какие были две первые сопоставленные пары?",
            },
            Questions = new()
            {
                [SMahjong.CountingTile] = new()
                {
                    // English: Which tile was shown in the bottom-left of {0}?
                    Question = "Какая кость была показана снизу слева {0}?",
                },
            },
        },

        // Main Page
        [typeof(SMainPage)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which color and message did the bubble (not) display?"] = "Какие цвета и фразы присутствовали И отсутствовали на модуле?",
                ["Which main page did Homestar, the background, or any of the buttons’ effects come from?"] = "Откуда были взяты Homestar, фон и эффекты кнопок?",
            },
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

        // M&Ms
        [typeof(SMandMs)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the labels on the buttons and their colors?"] = "What were the labels on the buttons and their colors?",
            },
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

        // M&Ns
        [typeof(SMandNs)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the colors of the labels on the buttons?"] = "What were the colors of the labels on the buttons?",
                ["What was the label of the correct button?"] = "What was the label of the correct button?",
            },
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

        // Maritime Flags
        [typeof(SMaritimeFlags)] = new()
        {
            ManualQuestions = new()
            {
                ["What bearing was signaled?"] = "Какой пеленг был передан?",
                ["What callsign was signaled?"] = "Какой позывной был передан?",
            },
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

        // Maritime Semaphore
        [typeof(SMaritimeSemaphore)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Where was the dummy?"] = "Где была фиктивная конфигурация?",
                ["Which letters were signaled?"] = "Какие буквы были переданы?",
            },
            Questions = new()
            {
                [SMaritimeSemaphore.Dummy] = new()
                {
                    // English: In which position was the dummy in {0}?
                    Question = "В какой позиции была фиктивная конфигурация {0}?",
                },
                [SMaritimeSemaphore.Letter] = new()
                {
                    // English: Which letter was shown {2} in the {1} position in {0}?
                    // Example: Which letter was shown as a maritime flag in the first position in Maritime Semaphore?
                    Question = "Which letter was shown {2} in the {1} position in {0}?",
                    Arguments = new()
                    {
                        ["as a maritime flag"] = "as a maritime flag",
                        ["in semaphore"] = "in semaphore",
                    },
                },
            },
        },

        // The Maroon Button
        [typeof(SMaroonButton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was A?"] = "What was A?",
            },
            Questions = new()
            {
                [SMaroonButton.A] = new()
                {
                    // English: What was A in {0}?
                    Question = "Какой был флаг А на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Maroon Cipher
        [typeof(SMaroonCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Mashematics
        [typeof(SMashematics)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the numbers in the equation?"] = "Какие числа были в уравнении?",
            },
            Questions = new()
            {
                [SMashematics.Calculation] = new()
                {
                    // English: What was the {1} number in the equation on {0}?
                    // Example: What was the first number in the equation on Mashematics?
                    Question = "Какое было {1}-е число в уравнении {0}?",
                },
            },
        },

        // Master Tapes
        [typeof(SMasterTapes)] = new()
        {
            ManualQuestions = new()
            {
                ["Which song was played?"] = "Какая песня была проиграна?",
            },
            Questions = new()
            {
                [SMasterTapes.PlayedSong] = new()
                {
                    // English: Which song was played in {0}?
                    Question = "Какая песня была проиграна {0}?",
                },
            },
        },

        // Match Refereeing
        [typeof(SMatchRefereeing)] = new()
        {
            ManualQuestions = new()
            {
                ["Which planets were present in each stage?"] = "Какие планеты присутствовали на каждом этапе?",
            },
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

        // Math ’em
        [typeof(SMathEm)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the color and design of each tile before the shuffle?"] = "Где была какая плитка до перемешивания?",
            },
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

        // The Matrix
        [typeof(SMatrix)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which word was part of the latest access code?"] = "Which word was part of the latest access code?",
                ["What was the glitched word?"] = "What was the glitched word?",
            },
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

        // Maze
        [typeof(SMaze)] = new()
        {
            ModuleName = "Лабиринте",
            ManualQuestions = new()
            {
                ["What was the starting position?"] = "Где была начальная позиция?",
            },
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
                        ["row"] = "какой строке",
                        ["left"] = "слева направо",
                        ["top"] = "сверху вниз",
                    },
                },
            },
        },

        // Maze³
        [typeof(SMaze3)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the color of the starting face?"] = "Какой цвет был у начальной стороны?",
            },
            Questions = new()
            {
                [SMaze3.StartingFace] = new()
                {
                    // English: What was the color of the starting face in {0}?
                    Question = "Какой цвет был у начальной стороны {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Maze Identification
        [typeof(SMazeIdentification)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the seed?"] = "Какое было зерно?",
                ["What function did each button have?"] = "Какая функция была у каждой кнопки?",
            },
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

        // Mazematics
        [typeof(SMazematics)] = new()
        {
            ModuleName = "Матебиринте",
            ManualQuestions = new()
            {
                ["What were the initial and goal values?"] = "Какими были начальная и целевая величины?",
            },
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

        // Maze Scrambler
        [typeof(SMazeScrambler)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the starting position?"] = "Где была начальная позиция?",
                ["What was the goal position?"] = "Где была цель?",
                ["Which positions were the maze markings?"] = "Где были обозначения лабиринта?",
            },
            Questions = new()
            {
                [SMazeScrambler.QStart] = new()
                {
                    // English: What was the starting position on {0}?
                    Question = "Какая была начальная позиция {0}?",
                    Answers = new()
                    {
                        ["top-left"] = "Сверху слева",
                        ["top-middle"] = "Сверху посередине",
                        ["top-right"] = "Сверху справа",
                        ["middle-left"] = "Посередине слева",
                        ["center"] = "center",
                        ["middle-right"] = "Посередине справа",
                        ["bottom-left"] = "Снизу слева",
                        ["bottom-middle"] = "Снизу посередине",
                        ["bottom-right"] = "Снизу справа",
                    },
                },
                [SMazeScrambler.QGoal] = new()
                {
                    // English: What was the goal on {0}?
                    Question = "Где была цель {0}?",
                    Answers = new()
                    {
                        ["top-left"] = "Сверху слева",
                        ["top-middle"] = "Сверху посередине",
                        ["top-right"] = "Сверху справа",
                        ["middle-left"] = "Посередине слева",
                        ["center"] = "center",
                        ["middle-right"] = "Посередине справа",
                        ["bottom-left"] = "Снизу слева",
                        ["bottom-middle"] = "Снизу посередине",
                        ["bottom-right"] = "Снизу справа",
                    },
                },
                [SMazeScrambler.QIndicators] = new()
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
            Discriminators = new()
            {
                [SMazeScrambler.DStart] = new()
                {
                    // English: the Maze Scramber where the starting position was at the {0}
                    // Example: the Maze Scramber where the starting position was at the top-left
                    Discriminator = "the Maze Scramber where the starting position was at the {0}",
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
                [SMazeScrambler.DGoal] = new()
                {
                    // English: the Maze Scramber where the goal was at the {0}
                    // Example: the Maze Scramber where the goal was at the top-left
                    Discriminator = "the Maze Scramber where the goal was at the {0}",
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
                [SMazeScrambler.DIndicators] = new()
                {
                    // English: the Maze Scramber with a maze marking at the {0}
                    // Example: the Maze Scramber with a maze marking at the top-left
                    Discriminator = "the Maze Scramber where a maze marking was at the {0}",
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

        // Mazeseeker
        [typeof(SMazeseeker)] = new()
        {
            ManualQuestions = new()
            {
                ["How many walls surrounded each cell?"] = "Сколько стен окружало каждую клетку?",
                ["What were the starting and goal positions?"] = "Где были начальная и целевая позиции?",
            },
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

        // Maze Swap
        [typeof(SMazeSwap)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the starting and goal positions?"] = "Где были начальная и целевая позиции?",
            },
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

        // Memorable Buttons
        [typeof(SMemorableButtons)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the symbols on the correct buttons?"] = "Какие символы были на правильных кнопках?",
            },
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

        // Memory
        [typeof(SMemory)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Памяти",
            ManualQuestions = new()
            {
                ["What was the display in each stage?"] = "Что было на экране на каждом этапе?",
            },
            Questions = new()
            {
                [SMemory.QDisplay] = new()
                {
                    // English: What was the displayed number in the {1} stage of {0}?
                    // Example: What was the displayed number in the first stage of Memory?
                    Question = "Какая цифра была на экране на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveFeminine,
                },
            },
            Discriminators = new()
            {
                [SMemory.DDisplay] = new()
                {
                    // English: the Memory that displayed a {0} in the {1} stage
                    // Example: the Memory that displayed a 1 in the first stage
                    Discriminator = "the Memory that displayed a {0} in the {1} stage",
                },
            },
        },

        // Memory Wires
        [typeof(SMemoryWires)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the wire colours?"] = "Какие цвета были у проводов?",
                ["What were the displayed digits?"] = "Какие цифры были показаны?",
            },
            Questions = new()
            {
                [SMemoryWires.QWireColours] = new()
                {
                    // English: What was the colour of wire {1} in {0}?
                    // Example: What was the colour of wire 1 in Memory Wires?
                    Question = "Какого цвета был {1}-й провод {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Yellow"] = "Жёлтый",
                        ["Blue"] = "Синий",
                        ["White"] = "Белый",
                        ["Black"] = "Чёрный",
                    },
                },
                [SMemoryWires.QDisplayedDigits] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Memory Wires?
                    Question = "Какая цифра была показана на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
            Discriminators = new()
            {
                [SMemoryWires.DWireColours] = new()
                {
                    // English: the Memory Wires where the colour of wire {0} was {1}
                    // Example: the Memory Wires where the colour of wire 1 was red
                    Discriminator = "the Memory Wires where the colour of wire {0} was {1}",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["yellow"] = "yellow",
                        ["blue"] = "blue",
                        ["white"] = "white",
                        ["black"] = "black",
                    },
                },
                [SMemoryWires.DDisplayedDigits] = new()
                {
                    // English: the Memory Wires where the digit displayed in the {0} stage was {1}
                    // Example: the Memory Wires where the digit displayed in the first stage was 1
                    Discriminator = "the Memory Wires where the digit displayed in the {0} stage was {1}",
                },
            },
        },

        // Metamorse
        [typeof(SMetamorse)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the extracted letter?"] = "Какая буквы была извлечена?",
            },
            Questions = new()
            {
                [SMetamorse.ExtractedLetter] = new()
                {
                    // English: What was the extracted letter in {0}?
                    Question = "Какая была извлечённая буква {0}?",
                },
            },
        },

        // Metapuzzle
        [typeof(SMetapuzzle)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the final answer?"] = "Какой был финальный ответ?",
            },
            Questions = new()
            {
                [SMetapuzzle.Answer] = new()
                {
                    // English: What was the final answer in {0}?
                    Question = "Какой был финальный ответ {0}?",
                },
            },
        },

        // Minsk Metro
        [typeof(SMinskMetro)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the starting station?"] = "What was the starting station?",
            },
            Questions = new()
            {
                [SMinskMetro.Station] = new()
                {
                    // English: What was the name of starting station in {0}?
                    Question = "What was the name of starting station in {0}?",
                },
            },
        },

        // Mirror
        [typeof(SMirror)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the second word written by the original ghost?"] = "Какое второе слово было написано оригинальным призраком?",
            },
            Questions = new()
            {
                [SMirror.Word] = new()
                {
                    // English: What was the second word written by the original ghost in {0}?
                    Question = "Какое было второе слово, написанное оригинальным призраком на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // The Missing Letter
        [typeof(SMissingLetter)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What letter was missing?"] = "What letter was missing?",
            },
            Questions = new()
            {
                [SMissingLetter.MissingLetter] = new()
                {
                    // English: What letter was missing in {0}?
                    Question = "What letter was missing in {0}?",
                },
            },
        },

        // Mister Softee
        [typeof(SMisterSoftee)] = new()
        {
            ManualQuestions = new()
            {
                ["Where was the SpongeBob Bar?"] = "Где был the SpongeBob Bar?",
                ["Which treats were present?"] = "Какие сладости присутствовали?",
            },
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

        // Mixometer
        [typeof(SMixometer)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the position of the submit button?"] = "Где была кнопка отправки?",
            },
            Questions = new()
            {
                [SMixometer.SubmitButton] = new()
                {
                    // English: What was the position of the submit button in {0}?
                    Question = "В какой позиции была кнопка отправки {0}?",
                },
            },
        },

        // Module Listening
        [typeof(SModuleListening)] = new()
        {
            ManualQuestions = new()
            {
                ["What sounds played?"] = "Какие звуки присутствовали?",
                ["What sounds did each button play?"] = "Какой звук издавала каждая кнопка?",
            },
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

        // Module Maneuvers
        [typeof(SModuleManeuvers)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the goal location?"] = "What was the goal location?",
            },
            Questions = new()
            {
                [SModuleManeuvers.Goal] = new()
                {
                    // English: What was the goal location in {0}?
                    Question = "What was the goal location in {0}?",
                    // Refer to translations.md to understand the weird strings
                    Additional = new()
                    {
                        ["{0}, {1}"] = "{0}, {1}",
                    },
                },
            },
        },

        // Module Maze
        [typeof(SModuleMaze)] = new()
        {
            ModuleName = "Модульном лабиринте",
            ManualQuestions = new()
            {
                ["What was the starting icon?"] = "Какая была начальная иконка?",
            },
            Questions = new()
            {
                [SModuleMaze.StartingIcon] = new()
                {
                    // English: Which of the following was the starting icon for {0}?
                    Question = "Какая была начальная иконка модуля {0}?",
                },
            },
        },

        // Module Movements
        [typeof(SModuleMovements)] = new()
        {
            ManualQuestions = new()
            {
                ["What module was shown for each stage?"] = "Какой модуль был показан на каждом этапе?",
            },
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

        // Money Game
        [typeof(SMoneyGame)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the phrases?"] = "Какие были фразы?",
            },
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

        // Monsplode, Fight!
        [typeof(SMonsplodeFight)] = new()
        {
            ModuleName = "\"Монсплоды, в атаку!\"",
            ManualModuleName = "Монсплоды, в атаку!",
            ManualQuestions = new()
            {
                ["Which creature was displayed?"] = "Какое существо было показано на экране?",
                ["Which moves were selectable?"] = "Какие приёмы были доступны?",
            },
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

        // Monsplode Trading Cards
        [typeof(SMonsplodeTradingCards)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Коллекционных карточках по Монсплодам",
            ManualQuestions = new()
            {
                ["What were the names and print versions of the three cards in your hand before the final trade/keep?"] = "Какие были имена и издания трёх начальных и последней предложенной карты?",
            },
            Questions = new()
            {
                [SMonsplodeTradingCards.CardsAny] = new()
                {
                    // English: Which of these cards was in your hand before the last action in {0}?
                    Question = "Which of these cards was in your hand before the last action in {0}?",
                },
                [SMonsplodeTradingCards.PrintVersionsAny] = new()
                {
                    // English: Which of these print versions was present on a card in your hand before the last action in {0}?
                    Question = "Which of these print versions was present on a card in your hand before the last action in {0}?",
                },
            },
        },

        // The Moon
        [typeof(SMoon)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which sets were initially lit/unlit?"] = "Which sets were initially lit/unlit?",
            },
            Questions = new()
            {
                [SMoon.LitUnlit] = new()
                {
                    // English: What was the {1} set in clockwise order in {0}?
                    // Example: What was the first initially lit set in clockwise order in The Moon?
                    Question = "Какой {1} по часовой стрелке {0}?",
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
                },
            },
        },

        // More Code
        [typeof(SMoreCode)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the flashing word?"] = "Какое слово передавалось модулем?",
            },
            Questions = new()
            {
                [SMoreCode.Word] = new()
                {
                    // English: What was the flashing word in {0}?
                    Question = "Какое слово передовалось на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Morse-A-Maze
        [typeof(SMorseAMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the starting and ending locations?"] = "Где были начальная и целевые позиции?",
                ["What was the Morse code word played?"] = "Какое было кодовое слово?",
            },
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

        // Morse Buttons
        [typeof(SMorseButtons)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the characters and colors flashed by each button?"] = "Какие символы и цвета мигали на каждой кнопке?",
            },
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

        // Morsematics
        [typeof(SMorsematics)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the received letters?"] = "Какие буквы были получены?",
            },
            Questions = new()
            {
                [SMorsematics.QReceivedLetters] = new()
                {
                    // English: Which of these letters was {1} in {0}?
                    // Example: Which of these letters was present in Morsematics?
                    Question = "Which of these letters was {1} in {0}?",
                    Arguments = new()
                    {
                        ["present"] = "present",
                        ["not present"] = "not present",
                    },
                },
            },
            Discriminators = new()
            {
                [SMorsematics.DReceivedLetters] = new()
                {
                    // English: the Morsematics that displayed the letter {0}
                    // Example: the Morsematics that displayed the letter A
                    Discriminator = "the Morsematics that displayed a {0}",
                },
            },
        },

        // Morse War
        [typeof(SMorseWar)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the LEDs?"] = "Какое было состояние светодиодов?",
                ["What code was transmitted?"] = "Какой код был передан?",
            },
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

        // .--/---/..-.
        [typeof(SMorseWoF)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the display words?"] = "What were the display words?",
            },
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

        // Mouse In The Maze
        [typeof(SMouseInTheMaze)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Мыши в лабиринте",
            ManualQuestions = new()
            {
                ["What color was the torus?"] = "What color was the torus?",
            },
            Questions = new()
            {
                [SMouseInTheMaze.Torus] = new()
                {
                    // English: What color was the torus in {0}?
                    Question = "Какого цвета было кольцо {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
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

        // M-Seq
        [typeof(SMSeq)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the obtained digits?"] = "Какие цифры были получены?",
                ["What was the final number from the iteration process?"] = "Какое число было финальным в итерационном процессе?",
            },
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

        // Mssngv Wls
        [typeof(SMssngvWls)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the missing vowel?"] = "Какая гласная отсутствовала?",
            },
            Questions = new()
            {
                [SMssngvWls.MssNgvwL] = new()
                {
                    // English: Which vowel was missing in {0}?
                    // Example: Which vowel was missing in Mssngv Wls?
                    Question = "Какая гласная отсутствовала {0}?",
                    // Refer to translations.md to understand the weird strings
                    Arguments = new()
                    {
                        ["AEIOU"] = "АИОУЭЯЫЁЮЕ",
                    },
                },
            },
        },

        // Multicolored Switches
        [typeof(SMulticoloredSwitches)] = new()
        {
            ModuleName = "Многоцветных переключателях",
            ManualQuestions = new()
            {
                ["What were the colors of the LEDs in both cycles?"] = "Какого цвета были светодиоды во всех последовательностях?",
            },
            Questions = new()
            {
                [SMulticoloredSwitches.LedColor] = new()
                {
                    // English: What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?
                    // Example: What color was the first LED on the top row when the tiny LED was lit in Multicolored Switches?
                    Question = "Какого цвета был {1}-й светодиод на {2} ряду, когда маленький светодиод {3} {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                    Arguments = new()
                    {
                        ["top"] = "верхнем",
                        ["bottom"] = "нижнем",
                        ["lit"] = "горел",
                        ["unlit"] = "не горел",
                    },
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
                },
            },
        },

        // The Multiverse Hotline
        [typeof(SMultiverseHotline)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the universe origin and its initial number?"] = "What was the universe origin and its initial number?",
            },
            Questions = new()
            {
                [SMultiverseHotline.OriginUniverse] = new()
                {
                    // English: What was the origin universe in {0}?
                    Question = "What was the universe origin in {0}?",
                },
                [SMultiverseHotline.OriginUniverseInitNumber] = new()
                {
                    // English: What was the origin universe’s initial number in {0}?
                    Question = "What was the universe origin's initial number in {0}?",
                },
            },
        },

        // Murder
        [typeof(SMurder)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Убийстве",
            ManualQuestions = new()
            {
                ["Which were the suspects and weapons?"] = "Кем были подозреваемые и какие оружия были найдены?",
                ["Where was the body found?"] = "Где было найдено тело?",
            },
            Questions = new()
            {
                [SMurder.Suspect] = new()
                {
                    // English: Which of these was {1} in {0}?
                    // Example: Which of these was a suspect but not the murderer in Murder?
                    Question = "Кто {1} {0}?",
                    Arguments = new()
                    {
                        ["a suspect but not the murderer"] = "не являлся убийцей, но был среди подозреваемых",
                        ["not a suspect"] = "не был подозреваемым",
                    },
                    Answers = new()
                    {
                        ["Miss Scarlett"] = "Miss Scarlett",
                        ["Professor Plum"] = "Professor Plum",
                        ["Mrs Peacock"] = "Mrs Peacock",
                        ["Reverend Green"] = "Reverend Green",
                        ["Colonel Mustard"] = "Colonel Mustard",
                        ["Mrs White"] = "Mrs White",
                    },
                },
                [SMurder.Weapon] = new()
                {
                    // English: Which of these was {1} in {0}?
                    // Example: Which of these was a potential weapon but not the murder weapon in Murder?
                    Question = "{1} {0}?",
                    Arguments = new()
                    {
                        ["a potential weapon but not the murder weapon"] = "Какое орудие было найдено, но не являлось орудием убийства",
                        ["not a potential weapon"] = "Какого орудия не было среди найденных возможных орудий",
                    },
                    Answers = new()
                    {
                        ["Candlestick"] = "Candlestick",
                        ["Dagger"] = "Dagger",
                        ["Lead Pipe"] = "Lead Pipe",
                        ["Revolver"] = "Revolver",
                        ["Rope"] = "Rope",
                        ["Spanner"] = "Spanner",
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

        // Mystery Module
        [typeof(SMysteryModule)] = new()
        {
            ManualQuestions = new()
            {
                ["Which module was hidden?"] = "Какой модуль был спрятан?",
                ["Which module was the first requested to be solved?"] = "Какой модуль требовалось решить первым?",
            },
            Questions = new()
            {
                [SMysteryModule.FirstKey] = new()
                {
                    // English: Which module was the first requested to be solved by {0}?
                    Question = "Какой модуль надо было обезвредить первым на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SMysteryModule.HiddenModule] = new()
                {
                    // English: Which module was hidden by {0}?
                    Question = "Какой модуль был спрятан за {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Mystic Square
        [typeof(SMysticSquare)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What digit was initially in the center?"] = "Где находился череп?",
            },
            Questions = new()
            {
                [SMysticSquare.CenterTile] = new()
                {
                    // English: What digit was initially in the center in {0}?
                    Question = "What digit was initially in the center in {0}?",
                },
            },
        },

        // Name Codes
        [typeof(SNameCodes)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the left and right indices?"] = "Какие индексы были слева и справа?",
            },
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

        // Naming Conventions
        [typeof(SNamingConventions)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the label of the first button?"] = "Какая была надпись на первой кнопке?",
            },
            Questions = new()
            {
                [SNamingConventions.Object] = new()
                {
                    // English: What was the label of the first button in {0}?
                    Question = "Какая была надпись на 1й кнопке {0}?",
                },
            },
        },

        // N&Ms
        [typeof(SNandMs)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "N и M",
            ManualQuestions = new()
            {
                ["What was the label of the correct button?"] = "What was the label of the correct button?",
            },
            Questions = new()
            {
                [SNandMs.Answer] = new()
                {
                    // English: What was the label of the correct button in {0}?
                    Question = "Какая надпись была на правильной кнопке {0}?",
                },
            },
        },

        // N&Ns
        [typeof(SNandNs)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the labels in stage 2 and 3?"] = "What were the labels in stage 2 and 3?",
                ["What was the missing color in stage 3?"] = "What was the missing color in stage 3?",
            },
            Questions = new()
            {
                [SNandNs.Label] = new()
                {
                    // English: Which label was present in the {1} stage of {0}?
                    // Example: Which label was present in the first stage of N&Ns?
                    Question = "Какая надпись присутствовала на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SNandNs.Color] = new()
                {
                    // English: Which color was missing in the third stage of {0}?
                    Question = "Какой цвет отсутствовал на 3-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Navigation Determination
        [typeof(SNavigationDetermination)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the color and label of the maze?"] = "Какой цвет и надпись были у лабиринта?",
            },
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

        // Navinums
        [typeof(SNavinums)] = new()
        {
            ManualQuestions = new()
            {
                ["Which directional buttons were pressed?"] = "Какие направления были нажаты?",
                ["What was the initial middle digit?"] = "Какая была исходная цифра посередине?",
            },
            Questions = new()
            {
                [SNavinums.DirectionalButtons] = new()
                {
                    // English: What was the {1} directional button pressed in {0}?
                    // Example: What was the first directional button pressed in Navinums?
                    Question = "Какая кнопка направления была нажата {1}-й {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // The Navy Button
        [typeof(SNavyButton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which Greek letters appeared?"] = "Which Greek letters appeared?",
                ["What were the coordinates and value of the given?"] = "What were the coordinates and value of the given?",
            },
            Questions = new()
            {
                [SNavyButton.QGreekLetters] = new()
                {
                    // English: Which Greek letter appeared on {0} (case-sensitive)?
                    Question = "Какая греческая буква (с учётом регистра) появилась на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SNavyButton.QGiven] = new()
                {
                    // English: What was the {1} of the given in {0}?
                    // Example: What was the (0-indexed) column of the given in The Navy Button?
                    Question = "{1} (с индексом 0) на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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

        // The Necronomicon
        [typeof(SNecronomicon)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the chapter number of each page?"] = "What was the chapter number of each page?",
            },
            Questions = new()
            {
                [SNecronomicon.Chapters] = new()
                {
                    // English: What was the chapter number of the {1} page in {0}?
                    // Example: What was the chapter number of the first page in The Necronomicon?
                    Question = "Какой был номер главы {1}-й страницы {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Negativity
        [typeof(SNegativity)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the submitted value (in base 10 and in balanced ternary)?"] = "Какая величина была отправления (в десятеричной и сбалансированной троичной системе)?",
            },
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

        // Neptune
        [typeof(SNeptune)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which star was displayed?"] = "Which star was displayed?",
            },
            Questions = new()
            {
                [SNeptune.Star] = new()
                {
                    // English: Which star was displayed in {0}?
                    Question = "Which star was displayed in {0}?",
                },
            },
        },

        // Neutralization
        [typeof(SNeutralization)] = new()
        {
            ModuleName = "Нейтрализации",
            ManualQuestions = new()
            {
                ["What was the acid’s color/volume?"] = "Какой объём/цвет был у кислоты?",
            },
            Questions = new()
            {
                [SNeutralization.Color] = new()
                {
                    // English: What was the acid’s color in {0}?
                    Question = "Какой цвет был у кислоты {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
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
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
            },
        },

        // Next In Line
        [typeof(SNextInLine)] = new()
        {
            ManualQuestions = new()
            {
                ["What color was the first wire?"] = "Какого цвета был первый провод?",
            },
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

        // Nim
        [typeof(SNim)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["How many matches were in each row?"] = "How many matches were in each row?",
            },
            Questions = new()
            {
                [SNim.MatchCountFirstRow] = new()
                {
                    // English: How many matches were in the {1} row in {0}?
                    // Example: How many matches were in the first row in Nim?
                    Question = "How many matches were in the {1} row in {0}?",
                },
                [SNim.MatchCountOtherRows] = new()
                {
                    // English: How many matches were in the {1} row in {0}?
                    // Example: How many matches were in the first row in Nim?
                    Question = "How many matches were in the {1} row in {0}?",
                },
            },
        },

        // Noise Identification
        [typeof(SNoiseIdentification)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the first displayed noise type?"] = "What was the first displayed noise type?",
            },
            Questions = new()
            {
                [SNoiseIdentification.Noises] = new()
                {
                    // English: What was the first displayed noise type in {0}?
                    Question = "What was the first displayed noise type in {0}?",
                },
            },
        },

        // ❖
        [typeof(SNonverbalSimon)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the flashes?"] = "Какие были вспышки?",
            },
            Questions = new()
            {
                [SNonverbalSimon.Flashes] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "Какой кнопка горела на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Not Colored Squares
        [typeof(SNotColoredSquares)] = new()
        {
            ManualQuestions = new()
            {
                ["What position did you initially press?"] = "Какую позицию вы нажали первой?",
            },
            Questions = new()
            {
                [SNotColoredSquares.InitialPosition] = new()
                {
                    // English: What was the position of the square you initially pressed in {0}?
                    Question = "Какая была позиция квадрата, который вы изначально нажали на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Not Colored Switches
        [typeof(SNotColoredSwitches)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the encrypted word?"] = "Какое слово было зашифровано?",
            },
            Questions = new()
            {
                [SNotColoredSwitches.Word] = new()
                {
                    // English: What was the encrypted word in {0}?
                    Question = "Какое было зашифрованное слово {0}?",
                },
            },
        },

        // Not Colour Flash
        [typeof(SNotColourFlash)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the displayed word and colour sequences?"] = "What were the displayed word and colour sequences?",
            },
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

        // Not Connection Check
        [typeof(SNotConnectionCheck)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the flashing symbols?"] = "Какие символы были переданы вспышками?",
                ["What were the button values?"] = "Какие значения были у каждой кнопки?",
            },
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

        // Not Coordinates
        [typeof(SNotCoordinates)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the vertices of the square?"] = "Какие были вершины у квадрата?",
            },
            Questions = new()
            {
                [SNotCoordinates.SquareCoords] = new()
                {
                    // English: Which coordinate was part of the square in {0}?
                    Question = "Какая координата была частью квадрата {0}?",
                },
            },
        },

        // Not Double-Oh
        [typeof(SNotDoubleOh)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the displayed positions in the second stage?"] = "Какие позиции были показаны на втором этапе?",
            },
            Questions = new()
            {
                [SNotDoubleOh.QPosition] = new()
                {
                    // English: What was the {1} displayed position in the second stage of {0}?
                    // Example: What was the first displayed position in the second stage of Not Double-Oh?
                    Question = "Какая позиция была показана {1}-й на втором этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
            Discriminators = new()
            {
                [SNotDoubleOh.DPosition] = new()
                {
                    // English: the Not Double-Oh where the {0} displayed position was {1}
                    // Example: the Not Double-Oh where the first displayed position was AA
                    Discriminator = "the Not Double-Oh where the {0} displayed position was {1}",
                },
            },
        },

        // Not Keypad
        [typeof(SNotKeypad)] = new()
        {
            ModuleName = "Не клавиатуре",
            ManualQuestions = new()
            {
                ["Which colours flashed in the final sequence?"] = "Какие цвета горели на последнем этапе?",
                ["Which symbols were on the buttons that flashed in the final sequence?"] = "Какие символы были на горящих кнопках в последнем этапе?",
            },
            Questions = new()
            {
                [SNotKeypad.Color] = new()
                {
                    // English: What color flashed {1} in the final sequence in {0}?
                    // Example: What color flashed first in the final sequence in Not Keypad?
                    Question = "Какой цвет горел {1}-м в последовательности на {0}?",
                    Conjugation = Conjugation.PrepositiveFeminine,
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
                    Conjugation = Conjugation.PrepositiveFeminine,
                },
            },
        },

        // Not Maze
        [typeof(SNotMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the starting distance?"] = "Какая была начальная дистанция?",
            },
            Questions = new()
            {
                [SNotMaze.StartingDistance] = new()
                {
                    // English: What was the starting distance in {0}?
                    Question = "Какая была начальная дистанция {0}?",
                },
            },
        },

        // Not Morse Code
        [typeof(SNotMorseCode)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the sequence of words you submitted?"] = "Какую последовательность слов вы отправили?",
            },
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

        // Not Morsematics
        [typeof(SNotMorsematics)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the transmitted word?"] = "Какое слово было передано?",
            },
            Questions = new()
            {
                [SNotMorsematics.Word] = new()
                {
                    // English: What was the transmitted word on {0}?
                    Question = "Какое слово было передано {0}?",
                },
            },
        },

        // Not Murder
        [typeof(SNotMurder)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What room were the suspects in initially?"] = "Какое было исходное положение подозреваемых?",
                ["What weapons did the suspects possess initially?"] = "Какое исходное оружие было у какого подозреваемого?",
            },
            Questions = new()
            {
                [SNotMurder.Room] = new()
                {
                    // English: What room was {1} in initially on {0}?
                    // Example: What room was Miss Scarlett in initially on Not Murder?
                    Question = "В какой комнате изначально находился(-ась) {1} {0}?",
                    Arguments = new()
                    {
                        ["Miss Scarlett"] = "Miss Scarlett",
                        ["Colonel Mustard"] = "Colonel Mustard",
                        ["Reverend Green"] = "Reverend Green",
                        ["Mrs Peacock"] = "Mrs Peacock",
                        ["Professor Plum"] = "Professor Plum",
                        ["Mrs White"] = "Mrs White",
                    },
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
                },
                [SNotMurder.Weapon] = new()
                {
                    // English: What weapon did {1} possess initially on {0}?
                    // Example: What weapon did Miss Scarlett possess initially on Not Murder?
                    Question = "Каким орудием изначально обладал(-а) {1} {0}?",
                    Arguments = new()
                    {
                        ["Miss Scarlett"] = "Miss Scarlett",
                        ["Colonel Mustard"] = "Colonel Mustard",
                        ["Reverend Green"] = "Reverend Green",
                        ["Mrs Peacock"] = "Mrs Peacock",
                        ["Professor Plum"] = "Professor Plum",
                        ["Mrs White"] = "Mrs White",
                    },
                    Answers = new()
                    {
                        ["Candlestick"] = "Candlestick",
                        ["Dagger"] = "Dagger",
                        ["Lead Pipe"] = "Lead Pipe",
                        ["Revolver"] = "Revolver",
                        ["Rope"] = "Rope",
                        ["Spanner"] = "Spanner",
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
                        ["she"] = "she",
                        ["Candlestick"] = "Candlestick",
                        ["Dagger"] = "Dagger",
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
                        ["she"] = "she",
                        ["Ballroom"] = "Ballroom",
                        ["Billiard Room"] = "Billiard Room",
                        ["Conservatory"] = "Conservatory",
                        ["Dining Room"] = "Dining Room",
                    },
                },
            },
        },

        // Not Number Pad
        [typeof(SNotNumberPad)] = new()
        {
            ManualQuestions = new()
            {
                ["Which numbers flashed at each stage?"] = "Какое число мигало на каждом этапе?",
            },
            Questions = new()
            {
                [SNotNumberPad.Flashes] = new()
                {
                    // English: Which of these numbers {1} at the {2} stage of {0}?
                    // Example: Which of these numbers flashed at the first stage of Not Number Pad?
                    Question = "Какая их этих цифр {1} на {2}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                    Arguments = new()
                    {
                        ["flashed"] = "мигала",
                        ["did not flash"] = "не мигала",
                    },
                },
            },
        },

        // Not Password
        [typeof(SNotPassword)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the missing letter?"] = "Какая буква отсутствовала?",
            },
            Questions = new()
            {
                [SNotPassword.Letter] = new()
                {
                    // English: Which letter was missing from {0}?
                    Question = "Какая буква отсутствовала {0}?",
                },
            },
        },

        // Not Perspective Pegs
        [typeof(SNotPerspectivePegs)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the positions, perspectives, and colors of each flash?"] = "Какая была позиция, ракурс и цвет каждой вспышки?",
            },
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

        // Not Piano Keys
        [typeof(SNotPianoKeys)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the displayed symbols?"] = "Какие символы были показаны?",
            },
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

        // Not Red Arrows
        [typeof(SNotRedArrows)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the starting number?"] = "Какое было начальное число?",
            },
            Questions = new()
            {
                [SNotRedArrows.Start] = new()
                {
                    // English: What was the starting number in {0}?
                    Question = "Какое было исходное число {0}?",
                },
            },
        },

        // Not Simaze
        [typeof(SNotSimaze)] = new()
        {
            ManualQuestions = new()
            {
                ["Which maze was used?"] = "Какой был лабиринт?",
                ["What were the starting and goal positions?"] = "Какая была начальная и конечная позиции?",
            },
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

        // Not Text Field
        [typeof(SNotTextField)] = new()
        {
            ManualQuestions = new()
            {
                ["Which letter appeared 9 times at the start?"] = "Какая буква появилась 9 раз в начале?",
                ["Which letters were pressed in the first stage?"] = "Какие буквы были нажаты на первом этапе?",
            },
            Questions = new()
            {
                [SNotTextField.BackgroundLetter] = new()
                {
                    // English: Which letter appeared 9 times at the start of {0}?
                    Question = "Какая буква появилась 9 раз в начале на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SNotTextField.InitialPresses] = new()
                {
                    // English: Which letter was pressed in the first stage of {0}?
                    Question = "Какая буква была нажата на первом этапе на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Not The Bulb
        [typeof(SNotTheBulb)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the transmitted word?"] = "Какое слово было передано модулем?",
            },
            Questions = new()
            {
                [SNotTheBulb.Word] = new()
                {
                    // English: What word flashed on {0}?
                    Question = "Какое слово мигало на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SNotTheBulb.Color] = new()
                {
                    // English: What color was the bulb on {0}?
                    Question = "Какого цвета была лампочка на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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

        // Not the Button
        [typeof(SNotTheButton)] = new()
        {
            ModuleName = "Не кнопки",
            ManualQuestions = new()
            {
                ["What color did the light glow?"] = "Какого цвета была цветная полоска?",
            },
            Questions = new()
            {
                [SNotTheButton.LightColor] = new()
                {
                    // English: What colors did the light glow in {0}?
                    Question = "Какими цветами горела цветная полоска {0}?",
                    Conjugation = Conjugation.GenitiveFeminine,
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

        // Not The Plunger Button
        [typeof(SNotThePlungerButton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What colors did the background flash?"] = "Какими цветами мигал фон?",
            },
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

        // Not The Screw
        [typeof(SNotTheScrew)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the initial position?"] = "Какая была начальная позиция?",
            },
            Questions = new()
            {
                [SNotTheScrew.InitialPosition] = new()
                {
                    // English: What was the initial position in {0}?
                    Question = "Какая была начальная позиция {0}?",
                },
            },
        },

        // Not Who’s on First
        [typeof(SNotWhosOnFirst)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the positions and labels of the correct buttons you pressed and the reference buttons?"] = "Какие кнопки в какой позиции вы нажали; ссылаясь на какие кнопки?",
                ["What was the calculated number in the second stage?"] = "Какая величина была вычислена во втором этапе?",
            },
            Questions = new()
            {
                [SNotWhosOnFirst.PressedPosition] = new()
                {
                    // English: In which position was the button you pressed in the {1} stage on {0}?
                    // Example: In which position was the button you pressed in the first stage on Not Who’s on First?
                    Question = "На какой позиции была кнопка, которую вы нажали на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SNotWhosOnFirst.ReferencePosition] = new()
                {
                    // English: In which position was the reference button in the {1} stage on {0}?
                    // Example: In which position was the reference button in the first stage on Not Who’s on First?
                    Question = "На какой позиции была кнопка-ссылка на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SNotWhosOnFirst.Sum] = new()
                {
                    // English: What was the calculated number in the second stage on {0}?
                    Question = "Какое было рассчитанное число на втором этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Not Word Search
        [typeof(SNotWordSearch)] = new()
        {
            ManualQuestions = new()
            {
                ["Which consonants were missing?"] = "Какие согласные отсутствовали?",
                ["What was the first correctly pressed letter?"] = "Какая первая верная буква была нажата?",
            },
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

        // Not X01
        [typeof(SNotX01)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the sector values?"] = "Какие величины были у секторов?",
            },
            Questions = new()
            {
                [SNotX01.SectorValues] = new()
                {
                    // English: Which sector value {1} present on {0}?
                    // Example: Which sector value was present on Not X01?
                    Question = "Какое значение сектора {1} на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                    Arguments = new()
                    {
                        ["was"] = "присутствовало",
                        ["was not"] = "отсутствовало",
                    },
                },
            },
        },

        // Not X-Ray
        [typeof(SNotXRay)] = new()
        {
            ManualQuestions = new()
            {
                ["What table were we in?"] = "В какой таблице вы находились?",
                ["Which button went which direction?"] = "Какие кнопки отвечали за какое направление?",
                ["What was the scanner color?"] = "Какой был цвет сканера?",
            },
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

        // Numbered Buttons
        [typeof(SNumberedButtons)] = new()
        {
            ManualQuestions = new()
            {
                ["Which numbers were correctly pressed?"] = "Какие правильные числа были нажаты?",
            },
            Questions = new()
            {
                [SNumberedButtons.Buttons] = new()
                {
                    // English: Which number was correctly pressed on {0}?
                    Question = "Какое было правильно нажатое число {0}?",
                },
            },
        },

        // The Number Game
        [typeof(SNumberGame)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the maximum number?"] = "What was the maximum number?",
            },
            Questions = new()
            {
                [SNumberGame.Maximum] = new()
                {
                    // English: What was the maximum number in {0}?
                    Question = "Какое было максимальное число {0}?",
                },
            },
        },

        // Numbers
        [typeof(SNumbers)] = new()
        {
            ManualQuestions = new()
            {
                ["What two-digit number was given?"] = "Какое двухзначное число было дано?",
            },
            Questions = new()
            {
                [SNumbers.TwoDigit] = new()
                {
                    // English: What two-digit number was given in {0}?
                    Question = "Какое двухзначное число было дано {0}?",
                },
            },
        },

        // Numpath
        [typeof(SNumpath)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the number and its color?"] = "Какое было число и его цвет?",
            },
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

        // Object Shows
        [typeof(SObjectShows)] = new()
        {
            ModuleName = "Обджект-шоу",
            ManualQuestions = new()
            {
                ["What contestants were shown?"] = "Какие участники были показаны, но проиграли?",
            },
            Questions = new()
            {
                [SObjectShows.Contestants] = new()
                {
                    // English: Which of these was a contestant on {0}?
                    Question = "Кто среди этих участников присутствовал на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // The Octadecayotton
        [typeof(SOctadecayotton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the starting sphere?"] = "What was the starting sphere?",
                ["What were the subtransformations in each transformation?"] = "What were the subtransformations in each transformation?",
            },
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
                    // Example: What was one of the subrotations in the first rotation in The Octadecayotton?
                    Question = "Каким было одно из промежуточных вращений в {1}-м вращении {0}?",
                },
            },
        },

        // Odd One Out
        [typeof(SOddOneOut)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the correct button presses?"] = "Какие правильные кнопки были нажаты?",
            },
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

        // Off Keys
        [typeof(SOffKeys)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the displayed runes?"] = "What were the displayed runes?",
                ["Which keys played at an incorrect pitch?"] = "Which keys played at an incorrect pitch?",
            },
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

        // Off-White Cipher
        [typeof(SOffWhiteCipher)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was on each display?"] = "What was on each display?",
            },
            Questions = new()
            {
                [SOffWhiteCipher.TopDisplay] = new()
                {
                    // English: What was on the top display in {0}?
                    Question = "What was on the top display in {0}?",
                },
                [SOffWhiteCipher.MiddleDisplay] = new()
                {
                    // English: What was on the middle display in {0}?
                    Question = "What was on the middle display in {0}?",
                },
                [SOffWhiteCipher.BottomDisplay] = new()
                {
                    // English: What was on the bottom display in {0}?
                    Question = "What was on the bottom display in {0}?",
                },
            },
        },

        // Old AI
        [typeof(SOldAI)] = new()
        {
            ManualQuestions = new()
            {
                ["Which condition did the displayed numbers follow?"] = "Какому условию следовало число на экране?",
            },
            Questions = new()
            {
                [SOldAI.Group] = new()
                {
                    // English: What was the {1} of the numbers shown in {0}?
                    // Example: What was the group of the numbers shown in Old AI?
                    Question = "Какая {1} чисел была показана на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                    Arguments = new()
                    {
                        ["group"] = "группа",
                        ["sub-group"] = "подгруппа",
                    },
                },
            },
        },

        // Old Fogey
        [typeof(SOldFogey)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the initial color of the status light?"] = "Какой был начальный цвет у индикатора?",
            },
            Questions = new()
            {
                [SOldFogey.StartingColor] = new()
                {
                    // English: What was the initial color of the status light in {0}?
                    Question = "Какой был исходный цвет индикатора на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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

        // One Links To All
        [typeof(SOneLinksToAll)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the initially displayed articles?"] = "Какие статьи были указаны в начале?",
            },
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

        // Only Connect
        [typeof(SOnlyConnect)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "\"Лишь Соедините!\"",
            ManualModuleName = "Лишь Соедините!",
            ManualQuestions = new()
            {
                ["What were the positions of the Egyptian hieroglyphs?"] = "Какое положение было у египетских иероглифов?",
            },
            Questions = new()
            {
                [SOnlyConnect.QHieroglyphs] = new()
                {
                    // English: Which Egyptian hieroglyph was in the {1} in {0}?
                    // Example: Which Egyptian hieroglyph was in the top left in Only Connect?
                    Question = "Какой египетский иероглиф был {1} {0}?",
                    Arguments = new()
                    {
                        ["top left"] = "слева сверху",
                        ["top middle"] = "сверху посередине",
                        ["top right"] = "справа сверху",
                        ["bottom left"] = "слева снизу",
                        ["bottom middle"] = "снизу посередине",
                        ["bottom right"] = "справа снизу",
                    },
                    Answers = new()
                    {
                        ["Two Reeds"] = "Два тростника",
                        ["Lion"] = "Лев",
                        ["Twisted Flax"] = "Скрученный лён",
                        ["Horned Viper"] = "Рогатая гадюка",
                        ["Water"] = "Вода",
                        ["Eye of Horus"] = "Глаз Гора",
                    },
                },
            },
            Discriminators = new()
            {
                [SOnlyConnect.DHieroglyphs] = new()
                {
                    // English: the Only Connect where {0} was in the {1}
                    // Example: the Only Connect where Two Reeds was in the top left
                    Discriminator = "the Only Connect where {0} was in the {1}",
                    Arguments = new()
                    {
                        ["Two Reeds"] = "Two Reeds",
                        ["Lion"] = "Lion",
                        ["Twisted Flax"] = "Twisted Flax",
                        ["Horned Viper"] = "Horned Viper",
                        ["Water"] = "Water",
                        ["Eye of Horus"] = "Eye of Horus",
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

        // Orange Arrows
        [typeof(SOrangeArrows)] = new()
        {
            ModuleName = "Оранжевых стрелок",
            ManualQuestions = new()
            {
                ["What were the first three arrows on the display in each stage?"] = "Какие первые три стрелки были показаны на каждом этапе?",
            },
            Questions = new()
            {
                [SOrangeArrows.Sequences] = new()
                {
                    // English: What was the {1} arrow on the display of the {2} stage of {0}?
                    // Example: What was the first arrow on the display of the first stage of Orange Arrows?
                    Question = "Какая была {1}-я стрелка на экране на {2}-м этапе {0}?",
                    Conjugation = Conjugation.GenitivePlural,
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

        // Orange Cipher
        [typeof(SOrangeCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Ordered Keys
        [typeof(SOrderedKeys)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the labels, their colors, and the colors of the keys in each stage?"] = "Какие были надписи, их цвета и цвета клавиш на каждом этапе?",
            },
            Questions = new()
            {
                [SOrderedKeys.Colors] = new()
                {
                    // English: What color was this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "Какого цвета была эта клавиша на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SOrderedKeys.LabelColors] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was the label of this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "Какого цвета была надпись на этой клавише на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Order Picking
        [typeof(SOrderPicking)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the order ID, product ID and pallet for each order?"] = "Какой был ID заказа, ID продукта и паллет в каждом заказе?",
            },
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

        // Orientation Cube
        [typeof(SOrientationCube)] = new()
        {
            ModuleName = "Ориентации куба",
            ManualQuestions = new()
            {
                ["What was the observer’s initial position?"] = "Какая была начальная позиция у наблюдателя?",
            },
            Questions = new()
            {
                [SOrientationCube.InitialObserverPosition] = new()
                {
                    // English: What was the observer’s initial position in {0}?
                    Question = "Какая была начальная позиция у наблюдателя {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
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

        // Orientation Hypercube
        [typeof(SOrientationHypercube)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the observer’s initial position?"] = "Какая была начальная позиция наблюдателя?",
                ["What was the initial colour of each face?"] = "Какой был начальный цвет каждой стороны?",
            },
            Questions = new()
            {
                [SOrientationHypercube.InitialFaceColour] = new()
                {
                    // English: What was the initial colour of the {1} face in {0}?
                    // Example: What was the initial colour of the right face in Orientation Hypercube?
                    Question = "Какой был начальный цвет {1} {0}?",
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

        // Painting Cube
        [typeof(SPaintingCube)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What color was missing?"] = "What color was missing?",
            },
            Questions = new()
            {
                [SPaintingCube.MissingColor] = new()
                {
                    // English: What color was missing in {0}?
                    Question = "What color is missing in {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Indigo"] = "Indigo",
                        ["Violet"] = "Violet",
                    },
                },
            },
        },

        // Palindromes
        [typeof(SPalindromes)] = new()
        {
            ManualQuestions = new()
            {
                ["What number was X, Y, Z, and the screen display?"] = "Что было в X, Y, Z и на экране?",
            },
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

        // Papa’s Pizzeria
        [typeof(SPapasPizzeria)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the order code?"] = "What was the order code?",
            },
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

        // Parity
        [typeof(SParity)] = new()
        {
            ManualQuestions = new()
            {
                ["What was shown on the display?"] = "Что было показано на экране?",
            },
            Questions = new()
            {
                [SParity.Display] = new()
                {
                    // English: What was shown on the display on {0}?
                    Question = "Что было показано на экране {0}?",
                },
            },
        },

        // Partial Derivatives
        [typeof(SPartialDerivatives)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the terms of the function?"] = "Из каких членов состояла функция?",
                ["What were the LED colors in each stage?"] = "Какой был цвет светодиода на каждом этапе?",
            },
            Questions = new()
            {
                [SPartialDerivatives.LedColors] = new()
                {
                    // English: What was the LED color in the {1} stage of {0}?
                    // Example: What was the LED color in the first stage of Partial Derivatives?
                    Question = "Какой был цвет светодиода на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Passport Control
        [typeof(SPassportControl)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the passport expiration years of each passenger?"] = "Какая была дата истечения паспорта у каждого пассажира?",
            },
            Questions = new()
            {
                [SPassportControl.Passenger] = new()
                {
                    // English: What was the passport expiration year of the {1} inspected passenger in {0}?
                    // Example: What was the passport expiration year of the first inspected passenger in Passport Control?
                    Question = "Какой был год истечения паспорта у {1}-го пассажира на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Password Destroyer
        [typeof(SPasswordDestroyer)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the 2FAST™ value?"] = "Какая была начальная величина, фактор инкремента, TFA₁, TFA₂ и 2FAST™, и также процент решённых модулей который использовался в финальных вычислениях?",
            },
            Questions = new()
            {
                [SPasswordDestroyer.TwoFactorV2] = new()
                {
                    // English: What was the 2FAST™ value when you solved {0}?
                    Question = "Чему был равен 2FAST™ когда вы обезвредили {0}?",
                    Conjugation = Conjugation.AccusativeMascNeuter,
                },
            },
        },

        // Pattern Cube
        [typeof(SPatternCube)] = new()
        {
            ModuleName = "Развёртке куба",
            ManualQuestions = new()
            {
                ["Which symbol was highlighted?"] = "Какой символ был выделен?",
            },
            Questions = new()
            {
                [SPatternCube.HighlightedSymbol] = new()
                {
                    // English: Which symbol was highlighted in {0}?
                    Question = "Какой символ был подсвечен {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
            },
        },

        // Pattern Recognition
        [typeof(SPatternRecognition)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the repeating pattern?"] = "What was the repeating pattern?",
            },
            Questions = new()
            {
                [SPatternRecognition.Pattern] = new()
                {
                    // English: What was the repeating pattern in {0}?
                    Question = "What was the repeating pattern in {0}?",
                },
            },
        },

        // The Pentabutton
        [typeof(SPentabutton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the base colour?"] = "What was the base colour?",
            },
            Questions = new()
            {
                [SPentabutton.BaseColor] = new()
                {
                    // English: What was the base colour in {0}?
                    Question = "Какой был цвет у основания {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Periodic Words
        [typeof(SPeriodicWords)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed word in each stage?"] = "Какое слово было показано на каждом этапе?",
            },
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

        // Perspective Pegs
        [typeof(SPerspectivePegs)] = new()
        {
            ModuleName = "Взгляде на колышках",
            ManualQuestions = new()
            {
                ["What was the initial color sequence?"] = "Какая была начальная последовательность цветов?",
            },
            Questions = new()
            {
                [SPerspectivePegs.ColorSequence] = new()
                {
                    // English: What was the {1} color in the initial sequence in {0}?
                    // Example: What was the first color in the initial sequence in Perspective Pegs?
                    Question = "Какой цвет был {1}-м в начальной последовательности {0}?",
                    Conjugation = Conjugation.во_PrepositiveMascNeuter,
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

        // Phosphorescence
        [typeof(SPhosphorescence)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the button presses and the offset?"] = "Какие кнопки были нажаты и какое было смещение?",
            },
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

        // Pickup Identification
        [typeof(SPickupIdentification)] = new()
        {
            ManualQuestions = new()
            {
                ["What pickups were shown?"] = "Какие предметы были показаны?",
            },
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

        // Pictionary
        [typeof(SPictionary)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the code?"] = "Какой был код?",
            },
            Questions = new()
            {
                [SPictionary.Code] = new()
                {
                    // English: What was the code in {0}?
                    Question = "Какой был код {0}?",
                },
            },
        },

        // Pie
        [typeof(SPie)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the digits shown?"] = "Какие цифры были показаны?",
            },
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

        // Pie Flash
        [typeof(SPieFlash)] = new()
        {
            ManualQuestions = new()
            {
                ["What numbers were displayed?"] = "Какие числа были показаны?",
            },
            Questions = new()
            {
                [SPieFlash.Digits] = new()
                {
                    // English: What number was not displayed in {0}?
                    Question = "Какое число не было показано {0}?",
                },
            },
        },

        // Pigpen Cycle
        [typeof(SPigpenCycle)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which direction were the dials pointing?"] = "Какое было сообщение и ответ?",
                ["What was written on each dial?"] = "What was written on each dial?",
            },
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
            Discriminators = new()
            {
                [SPigpenCycle.LabelDiscriminator] = new()
                {
                    // English: the Pigpen Cycle that had the letter {0} on a dial
                    // Example: the Pigpen Cycle that had the letter A on a dial
                    Discriminator = "the Pigpen Cycle that had the letter {0} on a dial",
                },
            },
        },

        // The Pink Button
        [typeof(SPinkButton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the displayed words and their colors?"] = "What were the displayed words and their colors?",
            },
            Questions = new()
            {
                [SPinkButton.Words] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the first word in The Pink Button?
                    Question = "Какое было {1}-е слово {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SPinkButton.Colors] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in The Pink Button?
                    Question = "Какой был {1}-й цвет на {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Pinpoint
        [typeof(SPinpoint)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the given point coordinates and distances?"] = "What were the given point coordinates and distances?",
            },
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

        // Pixel Cipher
        [typeof(SPixelCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the keyword?"] = "Какое было ключевое слово?",
            },
            Questions = new()
            {
                [SPixelCipher.Keyword] = new()
                {
                    // English: What was the keyword in {0}?
                    Question = "Какое было ключевое слово {0}?",
                },
            },
        },

        // Placeholder Talk
        [typeof(SPlaceholderTalk)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the entire first phrase?"] = "Какая была первая фраза?",
                ["What was the calculated value for second phrase?"] = "Какая величина была у второй фразы??",
            },
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

        // Placement Roulette
        [typeof(SPlacementRoulette)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the character, track, and vehicle listed?"] = "Какие персонажи, трассы и транспортные средства присутствовали?",
            },
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

        // Planets
        [typeof(SPlanets)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the planet shown?"] = "Какая планета была показана?",
                ["What was the color for each strip?"] = "What was the color for each strip?",
            },
            Questions = new()
            {
                [SPlanets.Strips] = new()
                {
                    // English: What was the color of the {1} strip (from the top) in {0}?
                    // Example: What was the color of the first strip (from the top) in Planets?
                    Question = "Какой был цвет у {1}-й полоски (начиная сверху) на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Playfair Cycle
        [typeof(SPlayfairCycle)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which direction were the dials pointing?"] = "Какое было сообщение и ответ?",
                ["What was written on each dial?"] = "What was written on each dial?",
            },
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
            Discriminators = new()
            {
                [SPlayfairCycle.LabelDiscriminator] = new()
                {
                    // English: the Playfair Cycle that had the letter {0} on a dial
                    // Example: the Playfair Cycle that had the letter A on a dial
                    Discriminator = "the Playfair Cycle that had the letter {0} on a dial",
                },
            },
        },

        // Pointless Machines
        [typeof(SPointlessMachines)] = new()
        {
            ManualQuestions = new()
            {
                ["What colors flashed?"] = "Какие цвета мигали?",
            },
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

        // Pokémon Sprite Cipher
        [typeof(SPokémonSpriteCipher)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "What was on each screen?",
            },
            Questions = new()
            {
                [SPokémonSpriteCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Pokémon Sprite Cipher?
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

        // Polygons
        [typeof(SPolygons)] = new()
        {
            ManualQuestions = new()
            {
                ["Which polygons were present?"] = "Какие фигуры присутствовали?",
            },
            Questions = new()
            {
                [SPolygons.Polygon] = new()
                {
                    // English: Which polygon was present on {0}?
                    Question = "Какой многоугольник присутствовал на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Polyhedral Maze
        [typeof(SPolyhedralMaze)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the starting position?"] = "Где была начальная позиция?",
            },
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
                    Arguments = new()
                    {
                        ["the 4-truncated deltoidal icositetrahedral Polyhedral Maze"] = "the 4-truncated deltoidal icositetrahedral Polyhedral Maze",
                        ["the chamfered dodecahedral Polyhedral Maze"] = "the chamfered dodecahedral Polyhedral Maze",
                        ["the chamfered icosahedral Polyhedral Maze"] = "the chamfered icosahedral Polyhedral Maze",
                        ["the deltoidal hexecontahedral Polyhedral Maze"] = "the deltoidal hexecontahedral Polyhedral Maze",
                        ["the disdyakis dodecahedral Polyhedral Maze"] = "the disdyakis dodecahedral Polyhedral Maze",
                        ["the joined snub cubic Polyhedral Maze"] = "the joined snub cubic Polyhedral Maze",
                        ["the joined rhombicuboctahedral Polyhedral Maze"] = "the joined rhombicuboctahedral Polyhedral Maze",
                        ["the pentagonal hexecontahedral Polyhedral Maze"] = "the pentagonal hexecontahedral Polyhedral Maze",
                        ["the orthokis propello cubic Polyhedral Maze"] = "the orthokis propello cubic Polyhedral Maze",
                        ["the pentakis dodecahedral Polyhedral Maze"] = "the pentakis dodecahedral Polyhedral Maze",
                        ["the rectified rhombicuboctahedral Polyhedral Maze"] = "the rectified rhombicuboctahedral Polyhedral Maze",
                        ["the triakis icosahedral Polyhedral Maze"] = "the triakis icosahedral Polyhedral Maze",
                        ["the rhombicosidodecahedral Polyhedral Maze"] = "the rhombicosidodecahedral Polyhedral Maze",
                        ["the canonical rectified snub cubic Polyhedral Maze"] = "the canonical rectified snub cubic Polyhedral Maze",
                    },
                },
            },
        },

        // Prime Encryption
        [typeof(SPrimeEncryption)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed number?"] = "Какое число было показано?",
            },
            Questions = new()
            {
                [SPrimeEncryption.DisplayedValue] = new()
                {
                    // English: What was the number shown in {0}?
                    Question = "Какое число было показано на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Prison Break
        [typeof(SPrisonBreak)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Where did the prisoner and defuser start?"] = "Where did the prisoner and defuser start?",
            },
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

        // Probing
        [typeof(SProbing)] = new()
        {
            ModuleName = "Прозвонке",
            ManualQuestions = new()
            {
                ["What were the missing frequencies in each wire?"] = "Какие частоты отсутствовали в каждом проводе?",
            },
            Questions = new()
            {
                [SProbing.Frequencies] = new()
                {
                    // English: What was the missing frequency in the {1} wire in {0}?
                    // Example: What was the missing frequency in the red-white wire in Probing?
                    Question = "Какая частота отсутствовала в {1} проводе {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
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

        // Procedural Maze
        [typeof(SProceduralMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the initial seed?"] = "Какое было изначальное зерно?",
            },
            Questions = new()
            {
                [SProceduralMaze.InitialSeed] = new()
                {
                    // English: What was the initial seed in {0}?
                    Question = "Какое было изначальное зерно {0}?",
                },
            },
        },

        // ...?
        [typeof(SPunctuationMarks)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed number?"] = "Какое число было показано?",
            },
            Questions = new()
            {
                [SPunctuationMarks.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Какое было показанное число в {0}?",
                },
            },
        },

        // Purple Arrows
        [typeof(SPurpleArrows)] = new()
        {
            ModuleName = "Фиолетовых стрелках",
            ManualQuestions = new()
            {
                ["What was the target word?"] = "Какое слово было зашифровано?",
            },
            Questions = new()
            {
                [SPurpleArrows.Finish] = new()
                {
                    // English: What was the target word on {0}?
                    Question = "Какое было целевое слово {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
        },

        // The Purple Button
        [typeof(SPurpleButton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the numbers in the cyclic sequence?"] = "What were the numbers in the cyclic sequence?",
            },
            Questions = new()
            {
                [SPurpleButton.Numbers] = new()
                {
                    // English: What was the {1} number in the cyclic sequence on {0}?
                    // Example: What was the first number in the cyclic sequence on The Purple Button?
                    Question = "Какое было {1}-е число в зацикленной последовательности {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Puzzle Identification
        [typeof(SPuzzleIdentification)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What game did each puzzle come from?"] = "Из какой игры был каждый пазл?",
                ["What was the name and number of each puzzle?"] = "Какой был номер и название каждого пазла?",
            },
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

        // Puzzling Hexabuttons
        [typeof(SPuzzlingHexabuttons)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What letters were shown during submission?"] = "What letters were shown during submission?",
            },
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

        // Q & A
        [typeof(SQnA)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What questions were asked?"] = "What questions were asked?",
            },
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

        // Quadrants
        [typeof(SQuadrants)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was on each button in each stage?"] = "What was on each button in each stage?",
            },
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

        // Quantum Passwords
        [typeof(SQuantumPasswords)] = new()
        {
            ManualQuestions = new()
            {
                ["Which words were used?"] = "Какие слова были использованы?",
            },
            Questions = new()
            {
                [SQuantumPasswords.Word] = new()
                {
                    // English: Which word was used in {0}?
                    Question = "Какое слово было использовано {0}?",
                },
            },
        },

        // Quantum Ternary Converter
        [typeof(SQuantumTernaryConverter)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the numbers?"] = "Какие были числа?",
            },
            Questions = new()
            {
                [SQuantumTernaryConverter.Number] = new()
                {
                    // English: Which number was shown in {0}?
                    Question = "Какое число было показано {0}?",
                },
            },
        },

        // Quaver
        [typeof(SQuaver)] = new()
        {
            ManualQuestions = new()
            {
                ["What was each sequence’s answer?"] = "Какой был ответ к каждой последовательности?",
            },
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

        // Question Mark
        [typeof(SQuestionMark)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the flashing symbols?"] = "Какие символы мигали?",
            },
            Questions = new()
            {
                [SQuestionMark.FlashedSymbols] = new()
                {
                    // English: Which of these symbols was part of the flashing sequence in {0}?
                    Question = "Какой из этих символов был частью мигающей последовательности {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Quick Arithmetic
        [typeof(SQuickArithmetic)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the digits and colors in both sequences?"] = "Какие были цифры и цвета в обеих последовательностях?",
            },
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

        // Quintuples
        [typeof(SQuintuples)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the numbers and colors in every slot?"] = "Какие были числа и цвета в каждом слоте?",
                ["How many numbers were there of each color?"] = "Сколько было чисел каждого цвета?",
            },
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

        // Quiplash
        [typeof(SQuiplash)] = new()
        {
            ManualQuestions = new()
            {
                ["What number was shown?"] = "Какое число было показано?",
            },
            Questions = new()
            {
                [SQuiplash.Number] = new()
                {
                    // English: What number was shown on {0}?
                    Question = "Какое число было показано на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Quiz Buzz
        [typeof(SQuizBuzz)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the number initially on the display?"] = "Какое число было изначально на экране?",
            },
            Questions = new()
            {
                [SQuizBuzz.StartingNumber] = new()
                {
                    // English: What was the number initially on the display in {0}?
                    Question = "Какое было исходное число на экране {0}?",
                },
            },
        },

        // Qwirkle
        [typeof(SQwirkle)] = new()
        {
            ManualQuestions = new()
            {
                ["Which tiles did you place?"] = "Какие плитки вы положили?",
            },
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

        // Raiding Temples
        [typeof(SRaidingTemples)] = new()
        {
            ManualQuestions = new()
            {
                ["How many jewels were initially in the common pool?"] = "Сколько драгоценностей было в начальном общем схроне?",
            },
            Questions = new()
            {
                [SRaidingTemples.StartingCommonPool] = new()
                {
                    // English: How many jewels were in the starting common pool in {0}?
                    Question = "Сколько драгоценностей было в начальном общем схроне {0}?",
                },
            },
        },

        // Railway Cargo Loading
        [typeof(SRailwayCargoLoading)] = new()
        {
            ModuleName = "Загрузке ЖД состава",
            ManualQuestions = new()
            {
                ["What were the coupled cars?"] = "Какие вагоны были присоединены?",
                ["Which freight table rules applied?"] = "Какие правила из грузовой таблицы были применены?",
            },
            Questions = new()
            {
                [SRailwayCargoLoading.Cars] = new()
                {
                    // English: What was the {1} car in {0}?
                    // Example: What was the first car in Railway Cargo Loading?
                    Question = "Какой вагон был присоединён {1}-м {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
                [SRailwayCargoLoading.FreightTableRules] = new()
                {
                    // English: Which freight table rule {1} in {0}?
                    // Example: Which freight table rule was met in Railway Cargo Loading?
                    Question = "Какое правило из таблицы грузовых вагонов {1} {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                    Arguments = new()
                    {
                        ["was met"] = "было применено",
                        ["wasn’t met"] = "не было применено",
                    },
                },
            },
        },

        // Rainbow Arrows
        [typeof(SRainbowArrows)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed number?"] = "Какое число было на экране?",
            },
            Questions = new()
            {
                [SRainbowArrows.Number] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Какое число было показано {0}?",
                },
            },
        },

        // Recolored Switches
        [typeof(SRecoloredSwitches)] = new()
        {
            ModuleName = "Перекрашенных переключателей",
            ManualQuestions = new()
            {
                ["What were the LED colors?"] = "Какого цвета были светодиоды?",
            },
            Questions = new()
            {
                [SRecoloredSwitches.LedColors] = new()
                {
                    // English: What was the color of the {1} LED in {0}?
                    // Example: What was the color of the first LED in Recolored Switches?
                    Question = "Какой был цвет {1}-го светодиода {0}?",
                    Conjugation = Conjugation.GenitivePlural,
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

        // Recursive Password
        [typeof(SRecursivePassword)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the incomplete words?"] = "Какие были обрывки слов?",
                ["What was the password?"] = "Какой был пароль?",
            },
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

        // Red Arrows
        [typeof(SRedArrows)] = new()
        {
            ModuleName = "Красных стрелках",
            ManualQuestions = new()
            {
                ["What was the starting number?"] = "Какое было начальное число?",
            },
            Questions = new()
            {
                [SRedArrows.StartNumber] = new()
                {
                    // English: What was the starting number in {0}?
                    Question = "Какое было начальное число {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
        },

        // Red Button’t
        [typeof(SRedButtont)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed word?"] = "Какое слово было показано?",
            },
            Questions = new()
            {
                [SRedButtont.Word] = new()
                {
                    // English: What was the word before “SUBMIT” in {0}?
                    Question = "Какое слово было перед 'SUBMIT' {0}?",
                },
            },
        },

        // Red Cipher
        [typeof(SRedCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Red Herring
        [typeof(SRedHerring)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the first color flashed?"] = "Какой цвет мигал первым?",
            },
            Questions = new()
            {
                [SRedHerring.FirstFlash] = new()
                {
                    // English: What was the first color flashed by {0}?
                    Question = "Какой был первый мигающий цвет {0}?",
                },
            },
        },

        // Reformed Role Reversal
        [typeof(SReformedRoleReversal)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What did the solving condition state?"] = "Что было указано в финальном условии?",
                ["What were the wire colors?"] = "What were the wire colors?",
            },
            Questions = new()
            {
                [SReformedRoleReversal.Condition] = new()
                {
                    // English: Which condition was the solving condition in {0}?
                    Question = "На каком условии был обезврежен {0}?",
                    Conjugation = Conjugation.NominativeMasculine,
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
                    Conjugation = Conjugation.NominativeMasculine,
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

        // ReGret-B Filtering
        [typeof(SReGretBFiltering)] = new()
        {
            ManualQuestions = new()
            {
                ["Which calculations were used?"] = "Какие вычисления были использованы?",
            },
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

        // Regular Crazy Talk
        [typeof(SRegularCrazyTalk)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the displayed digit that corresponded to the solution phrase?"] = "Какое число на экране соответствовало верной фразе?",
                ["What was the embellishment of the solution phrase?"] = "Какое обрамление было у верной фразы?",
            },
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

        // Reordered Keys
        [typeof(SReorderedKeys)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the labels, their colors, and the colors of the keys initially in each stage?"] = "What were the labels, their colors, and the colors of the keys initially in each stage?",
            },
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
            },
        },

        // Retirement
        [typeof(SRetirement)] = new()
        {
            ManualQuestions = new()
            {
                ["Which houses were on offer, but not chosen by Bob?"] = "Какие дома предлагались, но не были выбраны Бобом?",
            },
            Questions = new()
            {
                [SRetirement.Houses] = new()
                {
                    // English: Which one of these houses was on offer, but not chosen by Bob in {0}?
                    Question = "Какой из этих домов предлагался, но не был выбран Бобом {0}?",
                },
            },
        },

        // Reverse Morse
        [typeof(SReverseMorse)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the displayed symbols and their colors?"] = "Какие символы были в каждом сообщении?",
            },
            Questions = new()
            {
                [SReverseMorse.Symbols] = new()
                {
                    // English: What was the {1} symbol in the {2} message of {0}?
                    // Example: What was the first symbol in the first message of Reverse Morse?
                    Question = "What was the {1} symbol in the {2} message of {0}?",
                },
                [SReverseMorse.Colors] = new()
                {
                    // English: What was the color of the {1} symbol in the {2} message of {0}?
                    // Example: What was the color of the first symbol in the first message of Reverse Morse?
                    Question = "What was the color of the {1} symbol in the {2} message of {0}?",
                },
            },
        },

        // Reverse Polish Notation
        [typeof(SReversePolishNotation)] = new()
        {
            ManualQuestions = new()
            {
                ["What characters were used in each round?"] = "Какие символы использовались на каждом этапе?",
            },
            Questions = new()
            {
                [SReversePolishNotation.Character] = new()
                {
                    // English: What character was used in the {1} round of {0}?
                    // Example: What character was used in the first round of Reverse Polish Notation?
                    Question = "Какой символ был использован на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // RGB Encryption
        [typeof(SRGBEncryption)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the Morse code and color sequences?"] = "What were the Morse code and color sequences?",
            },
            Questions = new()
            {
                [SRGBEncryption.MorseSequence] = new()
                {
                    // English: What was the {1} Morse code sequence in {0}?
                    // Example: What was the first Morse code sequence in RGB Encryption?
                    Question = "What was the {1} Morse code sequence in {0}?",
                },
                [SRGBEncryption.ColorSequence] = new()
                {
                    // English: What was the {1} color sequence in {0}?
                    // Example: What was the first color sequence in RGB Encryption?
                    Question = "What was the {1} color sequence in {0}?",
                },
            },
        },

        // RGB Maze
        [typeof(SRGBMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["Where were the exit and keys of the maze?"] = "Где был выход и ключи лабиринта?",
                ["What was the maze number for each maze?"] = "Какой номер был у каждого лабиринта?",
            },
            Questions = new()
            {
                [SRGBMaze.Keys] = new()
                {
                    // English: Where was the {1} key in {0}?
                    // Example: Where was the red key in RGB Maze?
                    Question = "Где был {1} ключ {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // RGB Sequences
        [typeof(SRGBSequences)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the colors of each button?"] = "Какие цвета были на каждой кнопке?",
            },
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

        // Rhythms
        [typeof(SRhythms)] = new()
        {
            ModuleName = "Ритмах",
            ManualQuestions = new()
            {
                ["What was the color?"] = "Какой был цвет?",
            },
            Questions = new()
            {
                [SRhythms.Color] = new()
                {
                    // English: What was the color in {0}?
                    Question = "Какого цвета был светодиод {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
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

        // RNG Crystal
        [typeof(SRNGCrystal)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Where were the taps?"] = "Where were the taps?",
            },
            Questions = new()
            {
                [SRNGCrystal.Taps] = new()
                {
                    // English: Which bit had a tap in {0}? (The output after shifting is at bit 0.)
                    Question = "Which bit had a tap in {0} (the output after shifting is at bit 0)?",
                },
            },
        },

        // Robo-Scanner
        [typeof(SRoboScanner)] = new()
        {
            ManualQuestions = new()
            {
                ["Where was the empty cell?"] = "Где была пустая ячейка?",
            },
            Questions = new()
            {
                [SRoboScanner.EmptyCell] = new()
                {
                    // English: Where was the empty cell in {0}?
                    Question = "Где была пустая ячейка {0}?",
                },
            },
        },

        // Robot Programming
        [typeof(SRobotProgramming)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the shapes and colors of the robots?"] = "Каких форм и цветов были роботы?",
            },
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

        // Roger
        [typeof(SRoger)] = new()
        {
            ManualQuestions = new()
            {
                ["What four-digit number was given?"] = "Какое 4-хзначное число было дано?",
            },
            Questions = new()
            {
                [SRoger.Seed] = new()
                {
                    // English: What four-digit number was given in {0}?
                    Question = "Какое четырёхзначное число было дано {0}?",
                },
            },
        },

        // Role Reversal
        [typeof(SRoleReversal)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the condition digit that solved the module?"] = "Какой номер условия обезвредил модуль?",
                ["What colors were the internal wires?"] = "Какого цвета были внутренние провода?",
            },
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

        // RPS Judging
        [typeof(SRPSJudging)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which rounds did each team win/lose/tie?"] = "Which rounds did each team win/lose/tie?",
            },
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
                        ["blue"] = "blue",
                        ["win"] = "win",
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
                        ["blue"] = "blue",
                        ["won"] = "won",
                        ["lost"] = "lost",
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

        // The Rule
        [typeof(SRule)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the rule number?"] = "What was the rule number?",
            },
            Questions = new()
            {
                [SRule.Number] = new()
                {
                    // English: What was the rule number in {0}?
                    Question = "Какой был номер правила {0}?",
                },
            },
        },

        // Rule of Three
        [typeof(SRuleOfThree)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the positions of each sphere on each axis in each cycle?"] = "Какие позиции были у каждой сферы на каждой оси на каждом этапе?",
                ["What were the coordinates of the vertices?"] = "Какие координаты были у вершин?",
            },
            Questions = new()
            {
                [SRuleOfThree.QCoordinates] = new()
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
                [SRuleOfThree.QCycles] = new()
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
            Discriminators = new()
            {
                [SRuleOfThree.DCoordinates] = new()
                {
                    // English: the Rule of Three where the {1} coordinate of the {2} vertex was {0}
                    // Example: the Rule of Three where the X coordinate of the red vertex was 0
                    Discriminator = "the Rule of Three where the {1} coordinate of the {2} vertex was {0}?",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["yellow"] = "yellow",
                        ["blue"] = "blue",
                    },
                },
                [SRuleOfThree.DCycles] = new()
                {
                    // English: the Rule of Three where the {1} sphere was {0} on the {2} axis in the {3} cycle
                    // Example: the Rule of Three where the red sphere was positive on the X axis in the first cycle
                    Discriminator = "the Rule of Three where the {1} sphere was {0} on the {2} axis in the {3} cycle",
                    Arguments = new()
                    {
                        ["positive"] = "positive",
                        ["negative"] = "negative",
                        ["zero"] = "zero",
                        ["red"] = "red",
                        ["yellow"] = "yellow",
                        ["blue"] = "blue",
                    },
                },
            },
        },

        // Safety Square
        [typeof(SSafetySquare)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the displayed digits?"] = "Какие цифры были показаны?",
                ["What was the special rule?"] = "Какое было специальное правило?",
            },
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

        // The Samsung
        [typeof(SSamsung)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Where was each app?"] = "Where was each app?",
            },
            Questions = new()
            {
                [SSamsung.AppPositions] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was Duolingo in The Samsung?
                    Question = "Где было приложение {1} {0}?",
                },
            },
        },

        // Saturn
        [typeof(SSaturn)] = new()
        {
            ManualQuestions = new()
            {
                ["Where was the goal?"] = "Где была цель?",
            },
            Questions = new()
            {
                [SSaturn.Goal] = new()
                {
                    // English: Where was the goal in {0}?
                    Question = "Где была цель {0}?",
                },
            },
        },

        // Sbemail Songs
        [typeof(SSbemailSongs)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the displayed songs in each stage?"] = "Какие песни были показаны на каждом этапе?",
            },
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

        // Scavenger Hunt
        [typeof(SScavengerHunt)] = new()
        {
            ManualQuestions = new()
            {
                ["Which tile was correctly submitted in the first stage?"] = "Какая плитка была правильно выбрана на первом этапе?",
                ["Where were the red, green, and blue tiles in the first stage?"] = "Где была красная, зелёная и синяя плитка на первом этапе?",
            },
            Questions = new()
            {
                [SScavengerHunt.KeySquare] = new()
                {
                    // English: Which tile was correctly submitted in the first stage of {0}?
                    Question = "Какая плитка была верным ответом на первом этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SScavengerHunt.ColoredTiles] = new()
                {
                    // English: Which of these tiles was {1} in the first stage of {0}?
                    // Example: Which of these tiles was red in the first stage of Scavenger Hunt?
                    Question = "Какая из этих плиток была {1} на первом этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                    Arguments = new()
                    {
                        ["red"] = "красной",
                        ["green"] = "зелёной",
                        ["blue"] = "синей",
                    },
                },
            },
        },

        // Schlag den Bomb
        [typeof(SSchlagDenBomb)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the contestant’s name and both scores?"] = "Какие были имена участников и их счёт?",
            },
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

        // Scramboozled Eggain
        [typeof(SScramboozledEggain)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the encrypted words?"] = "Какие слова были зашифрованы?",
            },
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

        // Scrutiny Squares
        [typeof(SScrutinySquares)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the modified property of the first displayed square?"] = "Какое свойство первого квадрата было изменено?",
            },
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

        // Sea Shells
        [typeof(SSeaShells)] = new()
        {
            ModuleName = "Морских ракушках",
            ManualQuestions = new()
            {
                ["What were the phrases?"] = "Какие были фразы?",
            },
            Questions = new()
            {
                [SSeaShells.Question1] = new()
                {
                    // English: What were the first and second words in the {1} phrase in {0}?
                    // Example: What were the first and second words in the first phrase in Sea Shells?
                    Question = "What were the first and second words in the {1} phrase in {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
                [SSeaShells.Question2] = new()
                {
                    // English: What were the third and fourth words in the {1} phrase in {0}?
                    // Example: What were the third and fourth words in the first phrase in Sea Shells?
                    Question = "What were the third and fourth words in the {1} phrase in {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
                [SSeaShells.Question3] = new()
                {
                    // English: What was the end of the {1} phrase in {0}?
                    // Example: What was the end of the first phrase in Sea Shells?
                    Question = "What was the end of the {1} phrase in {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
        },

        // Semamorse
        [typeof(SSemamorse)] = new()
        {
            ManualQuestions = new()
            {
                ["What were Morse and semaphore letters and color used for the starting value?"] = "Какие буквы Морзе, буквы семафора и какой цвет были использованы в начальном значении?",
            },
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

        // The Sequencyclopedia
        [typeof(SSequencyclopedia)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What sequence was used?"] = "What sequence was used?",
            },
            Questions = new()
            {
                [SSequencyclopedia.Sequence] = new()
                {
                    // English: What sequence was used in {0}?
                    Question = "Какая была последовательность {0}?",
                },
            },
        },

        // S.E.T. Theory
        [typeof(SSetTheory)] = new()
        {
            ManualQuestions = new()
            {
                ["What equations were displayed in each stage?"] = "Какие уравнения были показаны на каждом этапе?",
            },
            Questions = new()
            {
                [SSetTheory.Equations] = new()
                {
                    // English: What equation was shown in the {1} stage of {0}?
                    // Example: What equation was shown in the first stage of S.E.T. Theory?
                    Question = "Какое уравнение было показано на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Shapes And Bombs
        [typeof(SShapesAndBombs)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the initial letter?"] = "Какая была начальная буква?",
            },
            Questions = new()
            {
                [SShapesAndBombs.InitialLetter] = new()
                {
                    // English: What was the initial letter in {0}?
                    Question = "Какая была начальная буква {0}?",
                },
            },
        },

        // Shape Shift
        [typeof(SShapeShift)] = new()
        {
            ModuleName = "Изменении формы",
            ManualQuestions = new()
            {
                ["What was the initial shape?"] = "Какая была начальная фигура?",
            },
            Questions = new()
            {
                [SShapeShift.InitialShape] = new()
                {
                    // English: What was the initial shape in {0}?
                    Question = "Какая была изначальная фигура {0}?",
                },
            },
        },

        // Shifted Maze
        [typeof(SShiftedMaze)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the colors of the markers?"] = "Каких цветов были маркеры?",
            },
            Questions = new()
            {
                [SShiftedMaze.Colors] = new()
                {
                    // English: What color was the {1} marker in {0}?
                    // Example: What color was the top-left marker in Shifted Maze?
                    Question = "Какого цвета был {1} маркер {0}?",
                    Arguments = new()
                    {
                        ["top-left"] = "верхний левый",
                        ["top-right"] = "верхний правый",
                        ["bottom-left"] = "нижний левый",
                        ["bottom-right"] = "нижний правый",
                    },
                    Answers = new()
                    {
                        ["White"] = "White",
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                        ["Magenta"] = "Magenta",
                        ["Green"] = "Green",
                    },
                },
            },
        },

        // Shifting Maze
        [typeof(SShiftingMaze)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the seed?"] = "Какое было зерно?",
            },
            Questions = new()
            {
                [SShiftingMaze.Seed] = new()
                {
                    // English: What was the seed in {0}?
                    Question = "Какое было зерно {0}?",
                },
            },
        },

        // Shogi Identification
        [typeof(SShogiIdentification)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the displayed piece?"] = "Какая фигура была показана?",
            },
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

        // Sign Language
        [typeof(SSignLanguage)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the deciphered word?"] = "Какое слово было расшифровано?",
            },
            Questions = new()
            {
                [SSignLanguage.Word] = new()
                {
                    // English: What was the deciphered word in {0}?
                    Question = "Какое слово было расшифровано {0}?",
                },
            },
        },

        // Silly Slots
        [typeof(SSillySlots)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Однорукого бандита",
            ManualQuestions = new()
            {
                ["What were the slots in each stage?"] = "Какие слоты были на каждом этапе?",
            },
            Questions = new()
            {
                [SSillySlots.QSlot] = new()
                {
                    // English: What was the {1} slot in the {2} stage in {0}?
                    // Example: What was the first slot in the first stage in Silly Slots?
                    Question = "Какой был {1}-й слот на {2}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
            Discriminators = new()
            {
                [SSillySlots.DSlot] = new()
                {
                    // English: the Silly Slots where the {0} slot in the {1} stage was a {2}
                    // Example: the Silly Slots where the first slot in the first stage was a red bomb
                    Discriminator = "the Silly Slots where the {1} slot in the {2} stage was {0}",
                    Arguments = new()
                    {
                        ["red bomb"] = "red bomb",
                        ["red cherry"] = "red cherry",
                        ["red coin"] = "red coin",
                        ["red grape"] = "red grape",
                        ["green bomb"] = "green bomb",
                        ["green cherry"] = "green cherry",
                        ["green coin"] = "green coin",
                        ["green grape"] = "green grape",
                        ["blue bomb"] = "blue bomb",
                        ["blue cherry"] = "blue cherry",
                        ["blue coin"] = "blue coin",
                        ["blue grape"] = "blue grape",
                    },
                },
            },
        },

        // Silo Authorization
        [typeof(SSiloAuthorization)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the message type, encrypted message, and received authorization code?"] = "Какой был тип сообщения, зашифрованное сообщение и полученные код авторизации?",
            },
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

        // Simon Said
        [typeof(SSimonSaid)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which colors flashed in the final sequence?"] = "Какая была финальная последовательность нажатий?",
            },
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

        // Simon Samples
        [typeof(SSimonSamples)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the call samples in each stage?"] = "Какие сэмплы были на каждом этапе?",
            },
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

        // Simon Says
        [typeof(SSimonSays)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "\"Саймон говорит\"",
            ManualModuleName = "Саймон говорит",
            ManualQuestions = new()
            {
                ["Which colors flashed in the final sequence?"] = "Какие цвета горели на последнем этапе?",
            },
            Questions = new()
            {
                [SSimonSays.QFlash] = new()
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
            Discriminators = new()
            {
                [SSimonSays.DFlash] = new()
                {
                    // English: the Simon Says whose {0} flash was {1}
                    // Example: the Simon Says whose first flash was red
                    Discriminator = "the Simon Says whose {0} flash was {1}",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["yellow"] = "yellow",
                        ["green"] = "green",
                        ["blue"] = "blue",
                    },
                },
            },
        },

        // Simon Scrambles
        [typeof(SSimonScrambles)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the flashing color sequence?"] = "Какая последовательность цветов мигала?",
            },
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

        // Simon Screams
        [typeof(SSimonScreams)] = new()
        {
            ModuleName = "\"Саймон кричит\"",
            ManualModuleName = "Саймон кричит",
            ManualQuestions = new()
            {
                ["What flashing color was used in each stage?"] = "Какой цвет вспышки был использован на каждом этапе?",
                ["Which rules applied in which stage(s)?"] = "Какие правила были применены на каком этапе?",
            },
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
                },
                [SSimonScreams.RuleComplex] = new()
                {
                    // English: In which stage(s) of {0} was “{1} flashed out of {2}, {3}, and {4}” the applicable rule?
                    // Example: In which stage(s) of Simon Screams was “at most one color flashed out of Red, Orange, and Yellow” the applicable rule?
                    Question = "На каком(-их) этапе(-ах) {0} среди кнопок {2}, {3} и {4} цвета {1}?",
                    Arguments = new()
                    {
                        ["at most one color"] = "горела максимум одна",
                        ["at least two colors"] = "горели как минимум две",
                        ["Red"] = "красного",
                        ["Green"] = "зелёного",
                        ["Orange"] = "оранжевого",
                        ["Blue"] = "синего",
                        ["Yellow"] = "жёлтого",
                        ["Purple"] = "фиолетового",
                    },
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
                },
            },
        },

        // Simon Selects
        [typeof(SSimonSelects)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which colors flashed in each stage?"] = "Какие цвета мигали на каждом этапе?",
            },
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

        // Simon Sends
        [typeof(SSimonSends)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the red, green, and blue received letters?"] = "Какая красная, зелёная и синяя буква была получена?",
            },
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

        // Simon Serves
        [typeof(SSimonServes)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Who flashed in each course?"] = "Кто горел на каждой подаче?",
                ["Which items were not served in each course?"] = "Что не подавалось гостям на каждой подаче?",
            },
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

        // Simon Shapes
        [typeof(SSimonShapes)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the shape submitted at the end?"] = "Какая фигура была введена в конце?",
            },
            Questions = new()
            {
                [SSimonShapes.SubmittedShape] = new()
                {
                    // English: What was the shape submitted at the end of {0}?
                    Question = "Какая фигура была введена в конце {0}?",
                },
            },
        },

        // Simon Shouts
        [typeof(SSimonShouts)] = new()
        {
            ManualQuestions = new()
            {
                ["What letters flashed on each button?"] = "Какие буквы горели на каждой кнопке?",
            },
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

        // Simon Shrieks
        [typeof(SSimonShrieks)] = new()
        {
            ManualQuestions = new()
            {
                ["How many spaces clockwise from the arrow was each flash in the final sequence?"] = "Сколько пробелов по часовой стрелке было между каждой вспышкой в финальной последовательности?",
            },
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

        // Simon Shuffles
        [typeof(SSimonShuffles)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which buttons flashed at each stage?"] = "Which buttons flashed at each stage?",
            },
            Questions = new()
            {
                [SSimonShuffles.Flashes] = new()
                {
                    // English: What was the {1} flash of the {2} stage of {0}?
                    // Example: What was the first flash of the first stage of Simon Shuffles?
                    Question = "What was the {1} flash of the {2} stage of {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Cyan"] = "Cyan",
                        ["Blue"] = "Blue",
                        ["Purple"] = "Purple",
                        ["Magenta"] = "Magenta",
                        ["White"] = "White",
                    },
                },
            },
        },

        // Simon Signals
        [typeof(SSimonSignals)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the colors, shapes and number of directions of each arrow?"] = "Какие были цвета, формы и число направлений у каждой стрелки?",
            },
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

        // Simon Simons
        [typeof(SSimonSimons)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which buttons flashed?"] = "Which buttons flashed?",
            },
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

        // Simon Sings
        [typeof(SSimonSings)] = new()
        {
            ManualQuestions = new()
            {
                ["Which keys’ colors flashed in each stage?"] = "Какие цвета клавиш горели на каждом этапе?",
            },
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

        // Simon Smiles
        [typeof(SSimonSmiles)] = new()
        {
            ManualQuestions = new()
            {
                ["What sounds did each button press play?"] = "Какой звук был проигран каждой нажатой кнопкой?",
            },
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

        // Simon Smothers
        [typeof(SSimonSmothers)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which colors and directions flashed in each stage?"] = "Какие цвета и направления горели на каждом этапе?",
            },
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

        // Simon Sounds
        [typeof(SSimonSounds)] = new()
        {
            ManualQuestions = new()
            {
                ["Which sample buttons sounded in each stage?"] = "Какая кнопка семпла звучала на каждом этапе?",
            },
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

        // Simon Speaks
        [typeof(SSimonSpeaks)] = new()
        {
            ModuleName = "\"Саймон общается\"",
            ManualModuleName = "Саймон общается",
            ManualQuestions = new()
            {
                ["What were the relevant attributes of the flashing bubbles?"] = "Какие атрибуты горящих облаков были необходимы?",
            },
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

        // Simon’s Star
        [typeof(SSimonsStar)] = new()
        {
            ManualQuestions = new()
            {
                ["Which colors flashed in each stage?"] = "Какие цвета горели на каждом этапе?",
            },
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

        // Simon Stacks
        [typeof(SSimonStacks)] = new()
        {
            ManualQuestions = new()
            {
                ["Which colors flashed in each stage?"] = "Какие цвета горели на каждом этапе?",
            },
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

        // Simon Stages
        [typeof(SSimonStages)] = new()
        {
            ModuleName = "\"Саймон выступает\"",
            ManualModuleName = "Саймон выступает",
            ManualQuestions = new()
            {
                ["Which colors flashed in each stage?"] = "Какие цвета горели на каждом этапе?",
                ["What color was the indicator in each stage?"] = "Какого цвета был индикатор на каждом этапе?",
            },
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

        // Simon States
        [typeof(SSimonStates)] = new()
        {
            ModuleName = "\"Саймон утверждает\"",
            ManualModuleName = "Саймон утверждает",
            ManualQuestions = new()
            {
                ["Which color(s) flashed in the first three stages?"] = "Какие цвета горели на первых трёх этапах?",
            },
            Questions = new()
            {
                [SSimonStates.Display] = new()
                {
                    // English: Which {1} in the {2} stage in {0}?
                    // Example: Which color(s) flashed in the first stage in Simon States?
                    Question = "Какой(-ие) цвет(а) {1} на {2}-м этапе {0}?",
                    Arguments = new()
                    {
                        ["color(s) flashed"] = "горел(и)",
                        ["color(s) didn’t flash"] = "не горел(и)",
                    },
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
                },
            },
        },

        // Simon Stops
        [typeof(SSimonStops)] = new()
        {
            ManualQuestions = new()
            {
                ["Which colors flashed in the output sequence?"] = "Какие цвета горели в последовательности?",
            },
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

        // Simon Stores
        [typeof(SSimonStores)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which colors flashed in the final sequence?"] = "Какие цвета горели в финальной последовательности?",
            },
            Questions = new()
            {
                [SSimonStores.QFlashes] = new()
                {
                    // English: Which color {2} {1} in the final sequence of {0}?
                    // Example: Which color flashed first in the final sequence of Simon Stores?
                    Question = "Какой цвет {2} {1} в последовательности в {0}?",
                    // Refer to translations.md to understand the weird strings
                    Arguments = new()
                    {
                        ["flashed"] = "горел",
                        ["was among the colors that flashed"] = "был среди цветов на этапе",
                        ["flashed ({3})"] = "",
                        ["was among the colors that flashed ({3})"] = "",
                        ["flashed ({4})"] = "",
                        ["was among the colors that flashed ({4})"] = "",
                    },
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
            Discriminators = new()
            {
                [SSimonStores.DFlashes] = new()
                {
                    // English: the Simon Stores where {0} {2} {1} in the final sequence
                    // Example: the Simon Stores where red flashed first in the final sequence
                    Discriminator = "the Simon Stores where {0} {2} {1} in the final sequence",
                    // Refer to translations.md to understand the weird strings
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["cyan"] = "cyan",
                        ["magenta"] = "magenta",
                        ["yellow"] = "yellow",
                        ["flashed"] = "flashed",
                        ["was among the colors that flashed"] = "was among the colors that flashed",
                        ["flashed ({3})"] = "flashed ({3})",
                        ["was among the colors that flashed ({3})"] = "was among the colors that flashed ({3})",
                        ["flashed ({4})"] = "flashed ({4})",
                        ["was among the colors that flashed ({4})"] = "was among the colors that flashed ({4})",
                    },
                },
            },
        },

        // Simon Subdivides
        [typeof(SSimonSubdivides)] = new()
        {
            ManualQuestions = new()
            {
                ["What colors were the cells that subdivided?"] = "Каких цветов были клетки которые разделились?",
            },
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

        // Simon Supports
        [typeof(SSimonSupports)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the 3 topics?"] = "Какие были 3 темы рассуждений?",
            },
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

        // Simon Swizzles
        [typeof(SSimonSwizzles)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Where were ON and OFF?"] = "Где были ON и OFF?",
                ["What was the hidden binary number?"] = "Какое двоичное число было спрятано?",
            },
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

        // Simply Simon
        [typeof(SSimplySimon)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which colors flashed in each stage?"] = "Which colors flashed in each stage?",
            },
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

        // Simultaneous Simons
        [typeof(SSimultaneousSimons)] = new()
        {
            ManualQuestions = new()
            {
                ["Which colors flashed on each of the Simons?"] = "Какие цвета горели на каждом Саймоне?",
            },
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

        // Skewed Slots
        [typeof(SSkewedSlots)] = new()
        {
            ModuleName = "Искажённых слотах",
            ManualQuestions = new()
            {
                ["What were the original numbers?"] = "Какие три цифры были указаны в начале?",
            },
            Questions = new()
            {
                [SSkewedSlots.OriginalNumbers] = new()
                {
                    // English: What were the original numbers in {0}?
                    Question = "Какие были изначальные цифры {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
        },

        // Skewers
        [typeof(SSkewers)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What colors were the gems?"] = "Каких цветов были драгоценности?",
            },
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

        // Skyrim
        [typeof(SSkyrim)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which races, weapons, enemies, cities, and dragon shouts were selectable, but not the solution?"] = "Какие расы, оружия, враги, города и крики дракона можно было выбрать, но не являлись решением?",
            },
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

        // Slow Math
        [typeof(SSlowMath)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the last triplet of letters?"] = "Какая была последняя тройка букв?",
            },
            Questions = new()
            {
                [SSlowMath.LastLetters] = new()
                {
                    // English: What was the last triplet of letters in {0}?
                    Question = "Какие три буквы были последними {0}?",
                },
            },
        },

        // Small Circle
        [typeof(SSmallCircle)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["How much did the sequence shift by?"] = "На сколько сместилась последовательность?",
                ["Which wedge made the different noise in the beginning?"] = "Какой сегмент звучал иначе в начале?",
                ["Which colors were in the solution?"] = "Какие цвета были в решении?",
            },
            Questions = new()
            {
                [SSmallCircle.Shift] = new()
                {
                    // English: How much did the sequence shift by in {0}?
                    Question = "На сколько сместилась последовательность на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SSmallCircle.Wedge] = new()
                {
                    // English: Which wedge made the different noise in the beginning of {0}?
                    Question = "Какой сегмент {0} издал другой звук в начале?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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

        // Small Talk
        [typeof(SSmallTalk)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the display words?"] = "What were the display words?",
            },
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

        // Smash, Marry, Kill
        [typeof(SSmashMarryKill)] = new()
        {
            ManualQuestions = new()
            {
                ["What category was each module in?"] = "В какой категории был каждый модуль?",
            },
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

        // Snooker
        [typeof(SSnooker)] = new()
        {
            ManualQuestions = new()
            {
                ["How many reds were there initially?"] = "Сколько изначально было красных шаров?",
            },
            Questions = new()
            {
                [SSnooker.Reds] = new()
                {
                    // English: How many red balls were there at the start of {0}?
                    Question = "Сколько красных шаров было в начале {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Snowflakes
        [typeof(SSnowflakes)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the displayed snowflakes?"] = "Какие снежинки были показаны?",
            },
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

        // Sonic & Knuckles
        [typeof(SSonicKnuckles)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which monitor and badnik were shown?"] = "Which monitor and badnik were shown?",
            },
            Questions = new()
            {
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

        // Sonic the Hedgehog
        [typeof(SSonicTheHedgehog)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which sound was played by each screen?"] = "Which sound was played by each screen?",
                ["What were the pictures?"] = "What were the pictures?",
            },
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

        // Sorting
        [typeof(SSorting)] = new()
        {
            ModuleName = "Сортировке",
            ManualQuestions = new()
            {
                ["Which positions were involved in the final swap?"] = "Какие позиции участвовали в последней замене чисел?",
            },
            Questions = new()
            {
                [SSorting.LastSwap] = new()
                {
                    // English: What positions were the last swap used to solve {0}?
                    Question = "Какие позиции участвовали в последней замене чисел {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                },
            },
        },

        // Souvenir
        [typeof(SSouvenir)] = new()
        {
            ModuleName = "Сувенире",
            ManualQuestions = new()
            {
                ["What was the first module the other Souvenir asked a question about?"] = "О каком модуле был первый вопрос на другом Сувенире?",
            },
            Questions = new()
            {
                [SSouvenir.FirstQuestion] = new()
                {
                    // English: What was the first module asked about in the other Souvenir on this bomb?
                    Question = "О каком модуле был первый вопрос на другом Сувенире?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Space Traders
        [typeof(SSpaceTraders)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the maximum tax amount per vessel?"] = "Какой был максимальный налог за каждое судно?",
            },
            Questions = new()
            {
                [SSpaceTraders.MaxTax] = new()
                {
                    // English: What was the maximum tax amount per vessel in {0}?
                    Question = "Какой был максимальный налог за каждое судно {0}?",
                },
            },
        },

        // Spelling Bee
        [typeof(SSpellingBee)] = new()
        {
            ModuleName = "Правописании",
            ManualQuestions = new()
            {
                ["What word was asked to be spelled?"] = "Какое слово нужно было прописать?",
            },
            Questions = new()
            {
                [SSpellingBee.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "Какое слово нужно было произнести {0}?",
                },
            },
        },

        // The Sphere
        [typeof(SSphere)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the flashed colors?"] = "What were the flashed colors?",
            },
            Questions = new()
            {
                [SSphere.Colors] = new()
                {
                    // English: What was the {1} flashed color in {0}?
                    // Example: What was the first flashed color in The Sphere?
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

        // Splitting The Loot
        [typeof(SSplittingTheLoot)] = new()
        {
            ModuleName = "Разделении добычи",
            ManualQuestions = new()
            {
                ["Which bag was initially colored?"] = "Какой мешок изначально был окрашен?",
            },
            Questions = new()
            {
                [SSplittingTheLoot.ColoredBag] = new()
                {
                    // English: What bag was initially colored in {0}?
                    Question = "Какой мешок был изначально окрашен {0}?",
                },
            },
        },

        // Spongebob Birthday Identification
        [typeof(SSpongebobBirthdayIdentification)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the submitted names?"] = "Какие имена были введены?",
            },
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

        // Stability
        [typeof(SStability)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the lit LEDs’ colors?"] = "Какими цветами горели светодиоды?",
                ["What was the identification number?"] = "Какой был идентификационный номер?",
            },
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

        // Stable Time Signatures
        [typeof(SStableTimeSignatures)] = new()
        {
            ManualQuestions = new()
            {
                ["What time signatures played?"] = "What time signatures played?",
            },
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

        // Stacked Sequences
        [typeof(SStackedSequences)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the lengths of the sequences?"] = "Какие длины были у последовательностей?",
            },
            Questions = new()
            {
                [SStackedSequences.Question] = new()
                {
                    // English: Which of these is the length of a sequence in {0}?
                    Question = "Который ответ является длиной последовательности {0}?",
                },
            },
        },

        // Stars
        [typeof(SStars)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the digit in the center?"] = "Какая цифра была по центру?",
            },
            Questions = new()
            {
                [SStars.Center] = new()
                {
                    // English: What was the digit in the center of {0}?
                    Question = "Какая цифра была в центре {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Starstruck
        [typeof(SStarstruck)] = new()
        {
            ManualQuestions = new()
            {
                ["Which stars were shown?"] = "Какие звёзды были показаны?",
            },
            Questions = new()
            {
                [SStarstruck.Star] = new()
                {
                    // English: Which star was present on {0}?
                    Question = "Какая звезда присутствовала {0}?",
                },
            },
        },

        // State of Aggregation
        [typeof(SStateOfAggregation)] = new()
        {
            ManualQuestions = new()
            {
                ["What element was shown?"] = "Какой элемент был показан?",
            },
            Questions = new()
            {
                [SStateOfAggregation.Element] = new()
                {
                    // English: What was the element shown in {0}?
                    Question = "Какой элемент был отображён {0}?",
                },
            },
        },

        // Stellar
        [typeof(SStellar)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the letters?"] = "Какие были буквы?",
            },
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

        // Stroop’s Test
        [typeof(SStroopsTest)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was each submitted word/color?"] = "What were the submitted slides?",
            },
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

        // Stupid Slots
        [typeof(SStupidSlots)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the values of each arrow?"] = "Какие значения были у каждой стрелки?",
            },
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

        // Subbly Jubbly
        [typeof(SSubblyJubbly)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the replacement phrases?"] = "Какие были замены?",
            },
            Questions = new()
            {
                [SSubblyJubbly.Substitutions] = new()
                {
                    // English: What was a substitution word in {0}?
                    Question = "На какое слово была замена {0}?",
                },
            },
        },

        // Subscribe to Pewdiepie
        [typeof(SSubscribeToPewdiepie)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["How many subscribers do Pewdiepie and T-Series have?"] = "Сколько подписчиков было у Pewdiepie и T-Series?",
            },
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

        // Subway
        [typeof(SSubway)] = new()
        {
            ManualQuestions = new()
            {
                ["Which bread did the customer ask for?"] = "Какой хлеб просил покупатель?",
                ["Which item was not asked for?"] = "Чего не просил покупатель?",
            },
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

        // Sugar Skulls
        [typeof(SSugarSkulls)] = new()
        {
            ManualQuestions = new()
            {
                ["What skulls were shown?"] = "Какие черепа были показаны?",
            },
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

        // Suits and Colours
        [typeof(SSuitsAndColours)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the colours and suits of each cell?"] = "Какие цвета и масти были в каждой клетке?",
            },
            Questions = new()
            {
                [SSuitsAndColours.Colour] = new()
                {
                    // English: What was the colour of this cell in {0}? (+ sprite)
                    // Example: What was the colour of this cell in Suits and Colours? (+ sprite)
                    Question = "What was the colour of this cell in {0}?",
                    Answers = new()
                    {
                        ["yellow"] = "yellow",
                        ["green"] = "green",
                        ["orange"] = "orange",
                        ["red"] = "red",
                    },
                },
                [SSuitsAndColours.Suit] = new()
                {
                    // English: What was the suit of this cell in {0}? (+ sprite)
                    // Example: What was the suit of this cell in Suits and Colours? (+ sprite)
                    Question = "What was the suit of this cell in {0}?",
                    Answers = new()
                    {
                        ["spades"] = "spades",
                        ["hearts"] = "hearts",
                        ["clubs"] = "clubs",
                        ["diamonds"] = "diamonds",
                    },
                },
            },
        },

        // Superparsing
        [typeof(SSuperparsing)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed word?"] = "Какое слово было показано?",
            },
            Questions = new()
            {
                [SSuperparsing.Displayed] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "Какое слово было показано {0}?",
                },
            },
        },

        // SUSadmin
        [typeof(SSUSadmin)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which security protocols were installed?"] = "Какие протоколы безопасности были установлены?",
                ["What was the version number?"] = "What was the version number?",
            },
            Questions = new()
            {
                [SSUSadmin.Security] = new()
                {
                    // English: Which security protocol was installed in {0}?
                    Question = "Какой протокол безопасности был установлен на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SSUSadmin.Version] = new()
                {
                    // English: What was the version number in {0}?
                    Question = "What was the version number in {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // The Switch
        [typeof(SSwitch)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What color were the LEDs?"] = "What color were the LEDs?",
            },
            Questions = new()
            {
                [SSwitch.InitialColor] = new()
                {
                    // English: What color was the {1} LED on the {2} flip of {0}?
                    // Example: What color was the top LED on the first flip of The Switch?
                    Question = "Какого цвета был {1} светодиод при {2}-м нажатии {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                    Arguments = new()
                    {
                        ["top"] = "верхний",
                        ["bottom"] = "нижний",
                    },
                    Answers = new()
                    {
                        ["red"] = "Красного",
                        ["orange"] = "Оранжевого",
                        ["yellow"] = "Жёлтого",
                        ["green"] = "Зелёного",
                        ["blue"] = "Синего",
                        ["purple"] = "Фиолетового",
                    },
                },
            },
        },

        // Switches
        [typeof(SSwitches)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the initial position of the switches?"] = "Какая была начальная позиция переключателей?",
            },
            Questions = new()
            {
                [SSwitches.InitialPosition] = new()
                {
                    // English: What was the initial position of the switches in {0}?
                    Question = "Какое было начальное положение {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Switching Maze
        [typeof(SSwitchingMaze)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the seed?"] = "Какое было зерно?",
                ["What was the starting maze color?"] = "Какой был цвет начального лабиринта?",
            },
            Questions = new()
            {
                [SSwitchingMaze.Seed] = new()
                {
                    // English: What was the seed in {0}?
                    Question = "Какое было зерно у {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SSwitchingMaze.Color] = new()
                {
                    // English: What was the starting maze color in {0}?
                    Question = "Какой был цвет начального {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Symbol Cycle
        [typeof(SSymbolCycle)] = new()
        {
            ManualQuestions = new()
            {
                ["How many symbols were cycling on each screen?"] = "Сколько повторяющихся символов было на каждом экране?",
            },
            Questions = new()
            {
                [SSymbolCycle.SymbolCounts] = new()
                {
                    // English: How many symbols were cycling on the {1} screen in {0}?
                    // Example: How many symbols were cycling on the left screen in Symbol Cycle?
                    Question = "Сколько символов было на {1} экране {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                    Arguments = new()
                    {
                        ["left"] = "левом",
                        ["right"] = "правом",
                    },
                },
            },
        },

        // Symbolic Coordinates
        [typeof(SSymbolicCoordinates)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which symbols were present in each stage?"] = "Какие символы присутствовали на каждом этапе?",
            },
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

        // Symbolic Tasha
        [typeof(SSymbolicTasha)] = new()
        {
            ManualQuestions = new()
            {
                ["What symbols were on the buttons?"] = "Какие символы были на кнопках?",
                ["Which buttons flashed?"] = "Какие кнопки горели?",
            },
            Questions = new()
            {
                [SSymbolicTasha.DirectionFlashes] = new()
                {
                    // English: Which button flashed {1} in the final sequence of {0}?
                    // Example: Which button flashed first in the final sequence of Symbolic Tasha?
                    Question = "Какая кнопка горела {1}-й в финальной последовательности {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
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
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Synapse Says
        [typeof(SSynapseSays)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What number was displayed at each stage?"] = "What number was displayed at each stage?",
                ["What positions were the colors in at each stage?"] = "What positions were the colors in at each stage?",
                ["What colors flashed at each stage?"] = "What colors flashed at each stage?",
            },
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

        // SYNC-125 [3]
        [typeof(SSync125_3)] = new()
        {
            ManualQuestions = new()
            {
                ["What was displayed on the screen in each stage?"] = "Что было на экране на каждом этапе?",
            },
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

        // Synonyms
        [typeof(SSynonyms)] = new()
        {
            ManualQuestions = new()
            {
                ["Which number was displayed?"] = "Какое число было показано?",
            },
            Questions = new()
            {
                [SSynonyms.Number] = new()
                {
                    // English: Which number was displayed on {0}?
                    Question = "Какое число было отображено {0}?",
                },
            },
        },

        // Sysadmin
        [typeof(SSysadmin)] = new()
        {
            ManualQuestions = new()
            {
                ["What error code did you fix?"] = "Какой код ошибки вы исправили?",
            },
            Questions = new()
            {
                [SSysadmin.FixedErrorCodes] = new()
                {
                    // English: What error code did you fix in {0}?
                    Question = "Какой код ошибки вы исправили {0}?",
                },
            },
        },

        // TAC
        [typeof(STAC)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which cards were swapped?"] = "Which cards were swapped?",
                ["Which cards were in your hand?"] = "Which cards were in your hand?",
            },
            Questions = new()
            {
                [STAC.SwappedCard] = new()
                {
                    // English: Which card was {1} in the swap in {0}?
                    // Example: Which card was given away in the swap in TAC?
                    Question = "Which card was {1} your partner in {0}?",
                    Arguments = new()
                    {
                        ["given away"] = "given away",
                        ["received"] = "received",
                    },
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

        // Tap Code
        [typeof(STapCode)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the received word?"] = "Какое слово было получено?",
            },
            Questions = new()
            {
                [STapCode.ReceivedWord] = new()
                {
                    // English: What was the received word in {0}?
                    Question = "Какое слово было передано {0}?",
                },
            },
        },

        // Tasha Squeals
        [typeof(STashaSqueals)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the flashed colors?"] = "Какие цвета горели?",
            },
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

        // Tasque Managing
        [typeof(STasqueManaging)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the starting position?"] = "Где была начальная позиция?",
            },
            Questions = new()
            {
                [STasqueManaging.StartingPos] = new()
                {
                    // English: Where was the starting position in {0}?
                    Question = "Где была начальная позиция {0}?",
                },
            },
        },

        // The Tea Set
        [typeof(STeaSet)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["In what order were the ingredients displayed?"] = "In what order were the ingredients displayed?",
            },
            Questions = new()
            {
                [STeaSet.DisplayedIngredients] = new()
                {
                    // English: Which ingredient was displayed {1}, from left to right, in {0}?
                    // Example: Which ingredient was displayed first, from left to right, in The Tea Set?
                    Question = "Какой ингридиент был показан {1}-м, слева направо {0}?",
                },
            },
        },

        // Technical Keypad
        [typeof(STechnicalKeypad)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the displayed digits?"] = "Какие цифры были показаны?",
            },
            Questions = new()
            {
                [STechnicalKeypad.DisplayedDigits] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "Какая была {1}-я отображённая цифра {0}?",
                },
            },
        },

        // Ten-Button Color Code
        [typeof(STenButtonColorCode)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the buttons’ initial colors in each stage?"] = "Какой был начальный цвет каждой кнопки на каждом этапе?",
            },
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

        // Tenpins
        [typeof(STenpins)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the splits?"] = "Какие были сплиты?",
            },
            Questions = new()
            {
                [STenpins.Splits] = new()
                {
                    // English: What was the {1} split in {0}?
                    // Example: What was the red split in Tenpins?
                    Question = "Какой был {1} сплит {0}?",
                    Arguments = new()
                    {
                        ["red"] = "красный",
                        ["green"] = "зелёный",
                        ["blue"] = "синий",
                    },
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
                },
            },
        },

        // Tetriamonds
        [typeof(STetriamonds)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the pulsing colour sequence?"] = "Какая была последовательность пульсирующих цветов?",
            },
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

        // Text Field
        [typeof(STextField)] = new()
        {
            ModuleName = "Поле из букв",
            ManualQuestions = new()
            {
                ["What was the displayed letter?"] = "Какая буква была показана?",
            },
            Questions = new()
            {
                [STextField.Display] = new()
                {
                    // English: What was the displayed letter in {0}?
                    Question = "Какая буква присутствовала на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Thinking Wires
        [typeof(SThinkingWires)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which was the first wire needing to be cut?"] = "Какой первый провод нужно было перерезать?",
                ["What color was the second valid wire to cut?"] = "Какой был цвет второго верного перерезанного провода?",
                ["What was the display number?"] = "Какое число было на экране?",
            },
            Questions = new()
            {
                [SThinkingWires.FirstWire] = new()
                {
                    // English: What was the position from top to bottom of the first wire needing to be cut in {0}?
                    Question = "Где находился первый провод который нужно было перерезать (сверху вниз) на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SThinkingWires.SecondWire] = new()
                {
                    // English: What color did the second valid wire to cut have to have in {0}?
                    Question = "Какой цвет был у второго верно порезаного провода {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Third Base
        [typeof(SThirdBase)] = new()
        {
            ModuleName = "\"А меня – Сава\"",
            ManualModuleName = "А меня – Сава",
            ManualQuestions = new()
            {
                ["What were the display words?"] = "Какие слова были показаны?",
            },
            Questions = new()
            {
                [SThirdBase.Display] = new()
                {
                    // English: What was the display word in the {1} stage on {0}?
                    // Example: What was the display word in the first stage on Third Base?
                    Question = "Какое слово было на экране на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Thirty Dollar Module
        [typeof(SThirtyDollarModule)] = new()
        {
            ManualQuestions = new()
            {
                ["What sounds were played?"] = "Какие звуки были проиграны?",
            },
            Questions = new()
            {
                [SThirtyDollarModule.Sounds] = new()
                {
                    // English: Which sound was used in {0}?
                    Question = "Какой звук был использован {0}?",
                },
            },
        },

        // Tic Tac Toe
        [typeof(STicTacToe)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Крестиках-ноликах",
            ManualQuestions = new()
            {
                ["What was the initial state of the field?"] = "Какое было начальное состояние поля?",
            },
            Questions = new()
            {
                [STicTacToe.QButton] = new()
                {
                    // English: What was on the {1} button at the start of {0}?
                    // Example: What was on the top-left button at the start of Tic Tac Toe?
                    Question = "Что было на {1} кнопке в начале игры {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
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
            Discriminators = new()
            {
                [STicTacToe.DButton] = new()
                {
                    // English: the Tic Tac Toe where the {0} button was {1}
                    // Example: the Tic Tac Toe where the top-left button was 1
                    Discriminator = "the Tic Tac Toe where the {0} button was {1}",
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

        // Time Signatures
        [typeof(STimeSignatures)] = new()
        {
            ManualQuestions = new()
            {
                ["What time signatures played?"] = "Какие сигнатуры были проиграны?",
            },
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

        // Timezone
        [typeof(STimezone)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the departure and destination city?"] = "Какой был город отбытия и прибытия?",
            },
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

        // Tip Toe
        [typeof(STipToe)] = new()
        {
            ManualQuestions = new()
            {
                ["Which squares were safe in rows 9 and 10?"] = "Какие квадраты были безопасны в ряду 9 и 10?",
            },
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

        // Topsy Turvy
        [typeof(STopsyTurvy)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the word initially shown?"] = "Какое слово было изначально показано?",
            },
            Questions = new()
            {
                [STopsyTurvy.Word] = new()
                {
                    // English: What was the word initially shown in {0}?
                    Question = "Какое было начальное слово {0}?",
                },
            },
        },

        // Touch Transmission
        [typeof(STouchTransmission)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the transmitted word?"] = "Какое слово было передано?",
                ["In what order was the Braille read?"] = "В каком порядке читался шрифт Брайля?",
            },
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

        // Toxic Crystals
        [typeof(SToxicCrystals)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What letter was written on the casing when solving?"] = "What letter was written on the casing when solving?",
            },
            Questions = new()
            {
                [SToxicCrystals.Letter] = new()
                {
                    // English: What letter was written on the casing when solving {0}?
                    Question = "What letter was written on the casing when solving {0}?",
                },
            },
        },

        // Transmitted Morse
        [typeof(STransmittedMorse)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the received messages?"] = "Какие сообщения были получены?",
            },
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

        // Triamonds
        [typeof(STriamonds)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the pulsing colour sequence?"] = "Какая была последовательность пульсирующих цветов?",
            },
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

        // Tribal Council
        [typeof(STribalCouncil)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the northeast and southwest names?"] = "Какие имена были на северо-востоке и юго-западе?",
            },
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

        // Tricky Tetris Pieces
        [typeof(STrickyTetrisPieces)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the the shape of the first piece you pressed?"] = "What was the the shape of the first piece you pressed?",
                ["What was the second color palette that the pieces converted to?"] = "What was the second color palette that the pieces converted to?",
            },
            Questions = new()
            {
                [STrickyTetrisPieces.FirstShape] = new()
                {
                    // English: What was the first piece you pressed in {0}?
                    Question = "What was the shape of the first piece you pressed in {0}?",
                },
                [STrickyTetrisPieces.SecondPalette] = new()
                {
                    // English: What was the second color palette the pieces converted to in {0}?
                    Question = "What was the second color palette the pieces converted to in {0}?",
                },
            },
        },

        // Triple Term
        [typeof(STripleTerm)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the passwords?"] = "Какие были пароли?",
            },
            Questions = new()
            {
                [STripleTerm.Passwords] = new()
                {
                    // English: Which of these was one of the passwords in {0}?
                    Question = "Что из этого было одним из паролей {0}?",
                },
            },
        },

        // Two Bits
        [typeof(STwoBits)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Двух битах",
            ManualQuestions = new()
            {
                ["What were the correct three query responses?"] = "Какие были три правильных ответа на запросы?",
            },
            Questions = new()
            {
                [STwoBits.QResponse] = new()
                {
                    // English: What was the {1} correct query response from {0}?
                    // Example: What was the first correct query response from Two Bits?
                    Question = "Какой был ответ на {1}-й запрос {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
            Discriminators = new()
            {
                [STwoBits.DResponse] = new()
                {
                    // English: the Two Bits where the {0} correct query was {1}
                    // Example: the Two Bits where the first correct query was 00
                    Discriminator = "the Two Bits where the {0} correct query was {1}",
                },
            },
        },

        // Twodoku
        [typeof(STwodoku)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Where were the digits, symbols, and highlights?"] = "Where were the digits, symbols, and highlights?",
            },
            Questions = new()
            {
                [STwodoku.Givens] = new()
                {
                    // English: Which of these squares in {0} was {1}?
                    // Example: Which of these squares in Twodoku was a given digit?
                    Question = "Which of these squares in {0} was {1}?",
                    Arguments = new()
                    {
                        ["a given digit"] = "a given digit",
                        ["a given shape"] = "a given shape",
                        ["highlighted"] = "highlighted",
                    },
                },
                [STwodoku.GridPositions] = new()
                {
                    // English: What was in this grid position in {0}? (+ sprite)
                    Question = "What was in this grid position in {0}?",
                },
            },
        },

        // UFO Satellites
        [typeof(SUfoSatellites)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which numbers were present?"] = "Which numbers were present?",
            },
            Questions = new()
            {
                [SUfoSatellites.Numbers] = new()
                {
                    // English: Which number was not present on {0}?
                    Question = "Which number was not present on {0}?",
                },
            },
        },

        // Ultimate Cipher
        [typeof(SUltimateCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Ultimate Cycle
        [typeof(SUltimateCycle)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which direction were the dials pointing?"] = "Какое было сообщение и ответ?",
                ["What was written on each dial?"] = "What was written on each dial?",
            },
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
            Discriminators = new()
            {
                [SUltimateCycle.LabelDiscriminator] = new()
                {
                    // English: the Ultimate Cycle that had the letter {0} on a dial
                    // Example: the Ultimate Cycle that had the letter A on a dial
                    Discriminator = "the Ultimate Cycle that had the letter {0} on a dial",
                },
            },
        },

        // The Ultracube
        [typeof(SUltracube)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the rotations?"] = "What were the rotations?",
            },
            Questions = new()
            {
                [SUltracube.Rotations] = new()
                {
                    // English: What was the {1} rotation in {0}?
                    // Example: What was the first rotation in The Ultracube?
                    Question = "Каким было {1}-е вращение {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // UltraStores
        [typeof(SUltraStores)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the rotations?"] = "Какие были повороты?",
            },
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

        // Uncolored Squares
        [typeof(SUncoloredSquares)] = new()
        {
            ModuleName = "Неокрашенных квадратов",
            ManualQuestions = new()
            {
                ["What were the colors used in the first stage?"] = "Какие цвета были использованы на первом этапе?",
            },
            Questions = new()
            {
                [SUncoloredSquares.FirstStage] = new()
                {
                    // English: What was the {1} color in reading order used in the first stage of {0}?
                    // Example: What was the first color in reading order used in the first stage of Uncolored Squares?
                    Question = "Какой был {1}-й цвет в порядке чтения, использованный на первом этапе {0}?",
                    Conjugation = Conjugation.GenitivePlural,
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

        // Uncolored Switches
        [typeof(SUncoloredSwitches)] = new()
        {
            ModuleName = "Бесцветных переключателей",
            ManualQuestions = new()
            {
                ["What was the initial switch state?"] = "Какое было начальное положение переключателей?",
                ["What were the LED colors?"] = "Какого цвета были светодиоды?",
            },
            Questions = new()
            {
                [SUncoloredSwitches.InitialState] = new()
                {
                    // English: What was the initial state of the switches in {0}?
                    Question = "Какое было исходное состояние {0}?",
                    Conjugation = Conjugation.GenitivePlural,
                },
                [SUncoloredSwitches.LedColors] = new()
                {
                    // English: What color was the {1} LED in reading order in {0}?
                    // Example: What color was the first LED in reading order in Uncolored Switches?
                    Question = "Какого цвета был {1}-й светодиод в порядке чтения {0}?",
                    Conjugation = Conjugation.GenitivePlural,
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

        // Uncolour Flash
        [typeof(SUncolourFlash)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the initial “YES” and “NO” sequences?"] = "What were the initial “YES” and “NO” sequences?",
            },
            Questions = new()
            {
                [SUncolourFlash.Displays] = new()
                {
                    // English: What was the {1} in the {2} position of the “{3}” sequence of {0}?
                    // Example: What was the word in the first position of the “YES” sequence of Uncolour Flash?
                    Question = "What was the {1} displayed in the {2} sequence of {0}?",
                    Arguments = new()
                    {
                        ["word"] = "word",
                        ["colour of the word"] = "colour of the word",
                    },
                },
            },
            Discriminators = new()
            {
                [SUncolourFlash.Discriminator] = new()
                {
                    // English: the Uncolour Flash where the {0} in the {1} position of the “{2}” sequence was {3}
                    // Example: the Uncolour Flash where the word in the first position of the “YES” sequence was Red
                    Discriminator = "the Uncolour Flash where the {0} in the {1} position of the {2} sequence was {3}",
                    Arguments = new()
                    {
                        ["word"] = "word",
                        ["colour of the word"] = "colour of the word",
                    },
                },
            },
        },

        // Undertunneling
        [typeof(SUndertunneling)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the position in the maze after completing the first phase?"] = "What was the position in the maze after completing the first phase?",
            },
            Questions = new()
            {
                [SUndertunneling.PositionInMazeAfterPhaseOne] = new()
                {
                    // English: What was the position in the maze after the first phase in {0}?
                    Question = "What was the position in the maze after the first phase in {0}?",
                },
            },
        },

        // Unfair Cipher
        [typeof(SUnfairCipher)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the encrypted message?"] = "Какие инструкции были зашифрованы модулем?",
            },
            Questions = new()
            {
                [SUnfairCipher.Letters] = new()
                {
                    // English: What was the {1} letter of the encrypted message in {0}?
                    // Example: What was the first letter of the encrypted message in Unfair Cipher?
                    Question = "What was the {1} letter of the encrypted message in {0}?",
                },
            },
        },

        // Unfair's Cruel Revenge
        [typeof(SUnfairsCruelRevenge)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the decrypted instructions?"] = "What were the decrypted instructions?",
            },
            Questions = new()
            {
                [SUnfairsCruelRevenge.Instructions] = new()
                {
                    // English: What was the {1} decrypted instruction in {0}?
                    // Example: What was the first decrypted instruction in Unfair's Cruel Revenge?
                    Question = "What was the {1} decrypted instruction in {0}?",
                },
                [SUnfairsCruelRevenge.InstructionsLegacy] = new()
                {
                    // English: What was the {1} decrypted instruction in {0}?
                    // Example: What was the first decrypted instruction in Unfair's Cruel Revenge?
                    Question = "What was the {1} decrypted instruction in {0}?",
                },
            },
        },

        // Unfair’s Revenge
        [typeof(SUnfairsRevenge)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the decrypted instructions?"] = "Какие инструкции были расшифрованы?",
            },
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

        // Unicode
        [typeof(SUnicode)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the submitted codes?"] = "Какие коды были введены?",
            },
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

        // UNO!
        [typeof(SUNO)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the starting card?"] = "Какая была начальная карта?",
            },
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

        // Unordered Keys
        [typeof(SUnorderedKeys)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the labels, their colors, and the colors of the keys in each stage?"] = "Какие были надписи, их цвета и цвета клавиш на каждом этапе?",
            },
            Questions = new()
            {
                [SUnorderedKeys.KeyColor] = new()
                {
                    // English: What color was this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "Какого цвета была эта клавиша на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SUnorderedKeys.LabelColor] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was the label of this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "Какого цвета была надпись на этой клавише на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
                [SUnorderedKeys.Label] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What was the label of this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "Какого цвета была надпись на этой клавише на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Unown Cipher
        [typeof(SUnownCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the submitted letters?"] = "Какие буквы были введены?",
            },
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

        // Unpleasant Squares
        [typeof(SUnpleasantSquares)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What color was each square?"] = "What color was each square?",
            },
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

        // Updog
        [typeof(SUpdog)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed word?"] = "Какое слово было показано?",
                ["What were the flashing colors?"] = "Какие цвета мигали?",
            },
            Questions = new()
            {
                [SUpdog.Word] = new()
                {
                    // English: What was the text on {0}?
                    Question = "Какой был текст на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SUpdog.Color] = new()
                {
                    // English: What was the {1} color in the sequence on {0}?
                    // Example: What was the first color in the sequence on Updog?
                    Question = "Какой был {1} цвет в последовательности {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                    Arguments = new()
                    {
                        ["first"] = "первый",
                        ["last"] = "последний",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Красный",
                        ["Yellow"] = "Жёлтый",
                        ["Orange"] = "Оранжевый",
                        ["Green"] = "Зелёный",
                        ["Blue"] = "Синий",
                        ["Purple"] = "Фиолетовый",
                    },
                },
            },
        },

        // USA Cycle
        [typeof(SUSACycle)] = new()
        {
            ManualQuestions = new()
            {
                ["Which states were displayed?"] = "Какие штаты были показаны?",
            },
            Questions = new()
            {
                [SUSACycle.Displayed] = new()
                {
                    // English: Which state was displayed in {0}?
                    Question = "Какой штат был показан на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // USA Maze
        [typeof(SUSAMaze)] = new()
        {
            ModuleName = "Американском лабиринте",
            ManualQuestions = new()
            {
                ["Which state did you depart from?"] = "С какого штата вы начали свой путь?",
            },
            Questions = new()
            {
                [SUSAMaze.Origin] = new()
                {
                    // English: Which state did you depart from in {0}?
                    Question = "Из какого штата вы отправились {0}?",
                },
            },
        },

        // V
        [typeof(SV)] = new()
        {
            ManualQuestions = new()
            {
                ["Which words were shown?"] = "Какие слова были показаны?",
            },
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

        // Valves
        [typeof(SValves)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the initial state?"] = "Какое было начальное состояние?",
            },
            Questions = new()
            {
                [SValves.InitialState] = new()
                {
                    // English: What was the initial state of {0}?
                    Question = "Какое было начальное состояние {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // Varicolored Squares
        [typeof(SVaricoloredSquares)] = new()
        {
            ModuleName = "Разноцветных квадратах",
            ManualQuestions = new()
            {
                ["What was the initially pressed color?"] = "Какой был первый нажатый цвет?",
            },
            Questions = new()
            {
                [SVaricoloredSquares.InitialColor] = new()
                {
                    // English: What was the initially pressed color on {0}?
                    Question = "Какой был первый нажатый цвет на {0}?",
                    Conjugation = Conjugation.PrepositivePlural,
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

        // Varicolour Flash
        [typeof(SVaricolourFlash)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the words and colours of each goal?"] = "Какие слова и каких цветов были на каждой цели?",
            },
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

        // Variety
        [typeof(SVariety)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the LED colors?"] = "Каких цветов были светодиоды?",
                ["What was n for the colored knobs and bulbs?"] = "Чему было равно n на цветных ручках и лампочках?",
                ["What digits were displayed on the digit display?"] = "Какие цифры были показаны на цифровом экране?",
                ["What words were formable on the letter display?"] = "Какие слова можно было составить на экране с буквами?",
                ["What were the maximum values of the timers?"] = "Какие максимальные значения были у таймеров?",
            },
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
                    // Refer to translations.md to understand the weird strings
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

        // Vcrcs
        [typeof(SVcrcs)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed word?"] = "Какое слово было показано?",
            },
            Questions = new()
            {
                [SVcrcs.Word] = new()
                {
                    // English: What was the word in {0}?
                    Question = "Какое было слово {0}?",
                },
            },
        },

        // Vectors
        [typeof(SVectors)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the colors of the vectors?"] = "Какие были цвета векторов?",
            },
            Questions = new()
            {
                [SVectors.Colors] = new()
                {
                    // English: What was the color of the {1} vector in {0}?
                    // Example: What was the color of the first vector in Vectors?
                    Question = "Какого цвета был {1} вектор из {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                    Arguments = new()
                    {
                        ["first"] = "1-й",
                        ["second"] = "2-й",
                        ["third"] = "3-й",
                        ["only"] = "единственный",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Красного",
                        ["Orange"] = "Оранжевого",
                        ["Yellow"] = "Жёлтого",
                        ["Green"] = "Зелёного",
                        ["Blue"] = "Синего",
                        ["Purple"] = "Фиолетового",
                    },
                },
            },
        },

        // Vexillology
        [typeof(SVexillology)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the flagpole colors?"] = "Какие были цвета на флагштоке?",
            },
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

        // Violet Cipher
        [typeof(SVioletCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Visual Impairment
        [typeof(SVisualImpairment)] = new()
        {
            ModuleName = "Повреждённого зрения",
            ManualQuestions = new()
            {
                ["What were the desired colors?"] = "Какие были целевые цвета?",
            },
            Questions = new()
            {
                [SVisualImpairment.Colors] = new()
                {
                    // English: What was the desired color in the {1} stage on {0}?
                    // Example: What was the desired color in the first stage on Visual Impairment?
                    Question = "Какой был целевой цвет на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Walking Cube
        [typeof(SWalkingCube)] = new()
        {
            ManualQuestions = new()
            {
                ["Which cells did the cube walk to?"] = "По каким клеткам прошёлся куб?",
            },
            Questions = new()
            {
                [SWalkingCube.Path] = new()
                {
                    // English: Which of these cells was part of the cube’s path in {0}?
                    Question = "Какая из этих клеток была частью пути кубика {0}?",
                },
            },
        },

        // Warning Signs
        [typeof(SWarningSigns)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed sign?"] = "Какой знак был показан?",
            },
            Questions = new()
            {
                [SWarningSigns.DisplayedSign] = new()
                {
                    // English: What was the displayed sign in {0}?
                    Question = "Какой знак был показан на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // WASD
        [typeof(SWasd)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the displayed location?"] = "Какое место было показано?",
            },
            Questions = new()
            {
                [SWasd.DisplayedLocation] = new()
                {
                    // English: What was the location displayed in {0}?
                    Question = "Какая локация была показана {0}?",
                },
            },
        },

        // Watching Paint Dry
        [typeof(SWatchingPaintDry)] = new()
        {
            ManualQuestions = new()
            {
                ["How many brush strokes were there?"] = "Сколько было мазков кистью на модуле?",
            },
            Questions = new()
            {
                [SWatchingPaintDry.StrokeCount] = new()
                {
                    // English: How many brush strokes were heard in {0}?
                    Question = "Сколько мазков кистью было слышно на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Wavetapping
        [typeof(SWavetapping)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the color in the first two stages?"] = "Какой был верный узор на каждом этапе?",
            },
            Questions = new()
            {
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
                        ["Grey"] = "Grey",
                    },
                },
            },
        },

        // The Weakest Link
        [typeof(SWeakestLink)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Who was eliminated?"] = "Who was eliminated?",
                ["Who joined you in the Money Phase?"] = "Who joined you in the Money Phase?",
                ["What were the other contestants’ skills and correct answer ratios?"] = "What were the other contestants’ skills and correct answer ratios?",
            },
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
                    // Example: What was Annie’s skill in The Weakest Link?
                    Question = "Какой навык был у {1} {0}?",
                },
                [SWeakestLink.Ratio] = new()
                {
                    // English: What ratio did {1} get in the Question Phase in {0}?
                    // Example: What ratio did Annie get in the Question Phase in The Weakest Link?
                    Question = "На какой процент вопросов ответил(а) {1} в Question Phase {0}?",
                },
            },
        },

        // What’s on Second
        [typeof(SWhatsOnSecond)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the display text and color in each stage?"] = "Какой текст и какого цвета был на экране на каждом этапе?",
            },
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

        // White Arrows
        [typeof(SWhiteArrows)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the non-white arrows?"] = "Какие стрелки были не белыми?",
            },
            Questions = new()
            {
                [SWhiteArrows.Arrows] = new()
                {
                    // English: What was the {1} non-white arrow in {0}?
                    // Example: What was the first non-white arrow in White Arrows?
                    Question = "What was the {1} non-white arrow in {0}?",
                    // Refer to translations.md to understand the weird strings
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

        // White Cipher
        [typeof(SWhiteCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // WhoOF
        [typeof(SWhoOF)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on the display in each stage?"] = "Что было на экране на каждом этапе?",
            },
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

        // Who’s on First
        [typeof(SWhosOnFirst)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "\"Меня зовут Авас, а Вас\"",
            ManualModuleName = "Меня зовут Авас, а Вас",
            ManualQuestions = new()
            {
                ["What were the display words?"] = "Какие слова были показаны на экране?",
            },
            Questions = new()
            {
                [SWhosOnFirst.QDisplay] = new()
                {
                    // English: What was the display in the {1} stage on {0}?
                    // Example: What was the display in the first stage on Who’s on First?
                    Question = "Какое слово было на экране на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
            Discriminators = new()
            {
                [SWhosOnFirst.DDisplay] = new()
                {
                    // English: the Who’s on First that had {0} in the display in the {1} stage
                    // Example: the Who’s on First that had BLANK in the display in the first stage
                    Discriminator = "the Who’s on First that had {0} in the display in the {1} stage",
                },
            },
        },

        // Who’s on Gas
        [typeof(SWhosOnGas)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was displayed in the first phase of each stage?"] = "What was displayed in the first phase of each stage?",
            },
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

        // Who’s on Morse
        [typeof(SWhosOnMorse)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What word was transmitted in each stage?"] = "What word was transmitted in each stage?",
            },
            Questions = new()
            {
                [SWhosOnMorse.TransmitDisplay] = new()
                {
                    // English: What word was transmitted in the {1} stage on {0}?
                    // Example: What word was transmitted in the first stage on Who’s on Morse?
                    Question = "Какое слово было передано на {1}-м этапе {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
                },
            },
        },

        // The Wire
        [typeof(SWire)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the colors of the dials?"] = "What were the colors of the dials?",
                ["What was the displayed number?"] = "What was the displayed number?",
            },
            Questions = new()
            {
                [SWire.QDialColors] = new()
                {
                    // English: What was the color of the {1} dial in {0}?
                    // Example: What was the color of the top dial in The Wire?
                    Question = "Какого цвета был {1} диск {0}?",
                    Arguments = new()
                    {
                        ["top"] = "верхний",
                        ["bottom-left"] = "нижний левый",
                        ["bottom-right"] = "нижний правый",
                    },
                    Answers = new()
                    {
                        ["blue"] = "Синего",
                        ["green"] = "Зелёного",
                        ["grey"] = "Серого",
                        ["orange"] = "Оранжевого",
                        ["purple"] = "Фиолетового",
                        ["red"] = "Красного",
                    },
                },
                [SWire.QDisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Какое было отображённое число {0}?",
                },
            },
            Discriminators = new()
            {
                [SWire.DDialColors] = new()
                {
                    // English: the Wire whose {0} dial was {1}
                    // Example: the Wire whose top dial was blue
                    Discriminator = "the Wire whose {0} dial was {1}",
                    Arguments = new()
                    {
                        ["top"] = "top",
                        ["bottom-left"] = "bottom-left",
                        ["bottom-right"] = "bottom-right",
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["grey"] = "grey",
                        ["orange"] = "orange",
                        ["purple"] = "purple",
                        ["red"] = "red",
                    },
                },
                [SWire.DDisplayedNumber] = new()
                {
                    // English: the Wire whose displayed number was {0}
                    // Example: the Wire whose displayed number was 0
                    Discriminator = "the Wire whose displayed number was {0}",
                },
            },
        },

        // Wire Ordering
        [typeof(SWireOrdering)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the colors and numbers on the displays?"] = "Какие цвета и числа были на экранах?",
                ["What were the colors of the wires?"] = "Какие цвета были на проводах?",
            },
            Questions = new()
            {
                [SWireOrdering.DisplayColor] = new()
                {
                    // English: What color was the {1} display from the left in {0}?
                    // Example: What color was the first display from the left in Wire Ordering?
                    Question = "Какого цвета был {1}-й экран слева на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
                [SWireOrdering.WireColor] = new()
                {
                    // English: What color was the {1} wire from the left in {0}?
                    // Example: What color was the first wire from the left in Wire Ordering?
                    Question = "Какого цвета был {1}-й провод слева на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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

        // Wire Sequence
        [typeof(SWireSequence)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Последовательности проводов",
            ManualQuestions = new()
            {
                ["How many wires of each color were there?"] = "Сколько проводов было каждого цвета?",
            },
            Questions = new()
            {
                [SWireSequence.QColorCount] = new()
                {
                    // English: How many {1} wires were there in {0}?
                    // Example: How many red wires were there in Wire Sequence?
                    Question = "Сколько было {1} проводов {0}?",
                    Conjugation = Conjugation.в_PrepositiveFeminine,
                    Arguments = new()
                    {
                        ["red"] = "красных",
                        ["blue"] = "синих",
                        ["black"] = "чёрных",
                    },
                },
            },
            Discriminators = new()
            {
                [SWireSequence.DColorCount] = new()
                {
                    // English: the Wire Sequence that had {0} {1}
                    // Example: the Wire Sequence that had 1 red wire
                    Discriminator = "the Wire Sequence in which there were {0} {1} wires?",
                    Arguments = new()
                    {
                        ["red wire"] = "red wire",
                        ["blue wire"] = "blue wire",
                        ["black wire"] = "black wire",
                        ["red wires"] = "red wires",
                        ["blue wires"] = "blue wires",
                        ["black wires"] = "black wires",
                    },
                },
            },
        },

        // Wolf, Goat, and Cabbage
        [typeof(SWolfGoatAndCabbage)] = new()
        {
            ManualQuestions = new()
            {
                ["Which creatures were present?"] = "Какие существа присутствовали?",
                ["What size was the boat?"] = "Какого размера была лодка?",
            },
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

        // Word Count
        [typeof(SWordCount)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the displayed number?"] = "What was the displayed number?",
            },
            Questions = new()
            {
                [SWordCount.Number] = new()
                {
                    // English: What was the displayed number on {0}?
                    Question = "What was the displayed number on {0}?",
                },
            },
        },

        // Working Title
        [typeof(SWorkingTitle)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Рабочем названии",
            ManualQuestions = new()
            {
                ["What was the label shown?"] = "Какие надписи были показаны?",
            },
            Questions = new()
            {
                [SWorkingTitle.Display] = new()
                {
                    // English: What was on the display in {0}?
                    Question = "Какая надпись была показана на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
                },
            },
        },

        // Wumbo
        [typeof(SWumbo)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the number?"] = "Какое было число?",
            },
            Questions = new()
            {
                [SWumbo.Number] = new()
                {
                    // English: What was the number in {0}?
                    Question = "Какое было число {0}?",
                },
            },
        },

        // The Xenocryst
        [typeof(SXenocryst)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was the color of each flash?"] = "What was the color of each flash?",
            },
            Questions = new()
            {
                [SXenocryst.Question] = new()
                {
                    // English: What was the color of the {1} flash in {0}?
                    // Example: What was the color of the first flash in The Xenocryst?
                    Question = "Какого цвета была {1}-я вспышка {0}?",
                },
            },
        },

        // XmORse Code
        [typeof(SXmORseCode)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the displayed letters?"] = "Какие буквы были показаны?",
                ["What word did you decrypt?"] = "Какое слово было расшифровано?",
            },
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

        // xobekuJ ehT
        [typeof(SXobekuJehT)] = new()
        {
            ManualQuestions = new()
            {
                ["What song was played?"] = "Какая песня была проиграна?",
            },
            Questions = new()
            {
                [SXobekuJehT.Song] = new()
                {
                    // English: What song was played on {0}?
                    Question = "Какая песня звучала {0}?",
                },
            },
        },

        // X-Ring
        [typeof(SXRing)] = new()
        {
            ManualQuestions = new()
            {
                ["What symbols were scanned?"] = "Какие символы были просканированы?",
            },
            Questions = new()
            {
                [SXRing.Symbol] = new()
                {
                    // English: Which symbol was scanned in {0}?
                    Question = "Какой символ был просканирован {0}?",
                },
            },
        },

        // X-Rotor
        [typeof(SXRotor)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["Which symbols were scanned?"] = "Which symbols were scanned?",
            },
            Questions = new()
            {
                [SXRotor.Symbol] = new()
                {
                    // English: Which symbol was scanned in {0}?
                    Question = "Which symbol was scanned in {0}?",
                },
            },
        },

        // XY-Ray
        [typeof(SXYRay)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the scanned shapes?"] = "Какие фигуры были просканированы?",
            },
            Questions = new()
            {
                [SXYRay.Shapes] = new()
                {
                    // English: Which shape was scanned by {0}?
                    Question = "Какая фигура была просканирована {0}?",
                },
            },
        },

        // Yahtzee
        [typeof(SYahtzee)] = new()
        {
            ModuleName = "Покере на костях",
            ManualQuestions = new()
            {
                ["What was the first roll?"] = "Какой был первый бросок?",
            },
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

        // Yellow Arrows
        [typeof(SYellowArrows)] = new()
        {
            ModuleName = "Жёлтых стрелках",
            ManualQuestions = new()
            {
                ["What was the starting row letter?"] = "Какая была буква у начальной строки?",
            },
            Questions = new()
            {
                [SYellowArrows.StartingRow] = new()
                {
                    // English: What was the starting row letter in {0}?
                    Question = "Какая была буква у начальной строки {0}?",
                    Conjugation = Conjugation.в_PrepositivePlural,
                },
            },
        },

        // The Yellow Button
        [typeof(SYellowButton)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the colors?"] = "What were the colors?",
            },
            Questions = new()
            {
                [SYellowButton.Colors] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in The Yellow Button?
                    Question = "Какой был {1}-й цвет в последовательности {0}?",
                    Conjugation = Conjugation.GenitiveMascNeuter,
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

        // Yellow Button’t
        [typeof(SYellowButtont)] = new()
        {
            ManualQuestions = new()
            {
                ["What was the filename?"] = "Какое было имя файла?",
            },
            Questions = new()
            {
                [SYellowButtont.Filename] = new()
                {
                    // English: What was the filename in {0}?
                    Question = "Какое было название файла {0}?",
                },
            },
        },

        // Yellow Cipher
        [typeof(SYellowCipher)] = new()
        {
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "Что было на каждом экране?",
            },
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

        // Yellow Huffman Cipher
        [typeof(SYellowHuffmanCipher)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What was on each screen?"] = "What was on each screen?",
            },
            Questions = new()
            {
                [SYellowHuffmanCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Yellow Huffman Cipher?
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

        // Zero, Zero
        [typeof(SZeroZero)] = new()
        {
            NeedsTranslation = true,
            ManualQuestions = new()
            {
                ["What were the colors and number of points on each star?"] = "Какой цвет и сколько вершин было у каждой звезды?",
                ["Where were the colored squares?"] = "Какие цветные квадраты присутствовали?",
            },
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
                    Arguments = new()
                    {
                        ["top-left"] = "верхняя левая",
                        ["top-right"] = "верхняя правая",
                        ["bottom-left"] = "нижняя левая",
                        ["bottom-right"] = "нижняя правая",
                    },
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

        // Zoni
        [typeof(SZoni)] = new()
        {
            ManualQuestions = new()
            {
                ["What were the words?"] = "Какие слова были расшифрованы?",
            },
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

        // Épelle-moi Ça
        [typeof(SÉpelleMoiÇa)] = new()
        {
            ManualQuestions = new()
            {
                ["What word was asked to be spelled?"] = "Какое слово нужно было прописать?",
            },
            Questions = new()
            {
                [SÉpelleMoiÇa.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "Какое слово нужно было написать на {0}?",
                    Conjugation = Conjugation.PrepositiveMascNeuter,
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

using System.Collections.Generic;
using static Souvenir.Translation_ru.Conjugation;

namespace Souvenir
{
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

        public override string FormatModuleName(Question question, bool addSolveCount, int numSolved)
        {
            if (_translations.Get(question) is not TranslationInfo_ru tr)
                return base.FormatModuleName(question, addSolveCount, numSolved);

            return addSolveCount
                ? tr.Conjugation switch
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
                    _ => throw new System.InvalidOperationException($"Unknown conjugation: {tr.Conjugation}")
                }
                : tr.Conjugation switch
                {
                    в_PrepositiveMascNeuter or в_PrepositiveFeminine or в_PrepositivePlural => $"в {tr.ModuleName}",
                    во_PrepositiveMascNeuter or во_PrepositiveFeminine or во_PrepositivePlural => $"во {tr.ModuleName}",
                    _ => tr.ModuleName,
                };
        }

        public override string Ordinal(int number) => number.ToString();

        protected override Dictionary<Question, TranslationInfo_ru> _translations => new()
        {
            #region Translatable strings
            // .--/---/.--.
            // What was the display in the {1} stage on {0}?
            // What was the display in the first stage on .--/---/.--.?
            [Question.MorseWoFDisplays] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the display in the {1} stage on {0}?",
            },

            // 0
            // What was the initially displayed number in {0}?
            // What was the initially displayed number in 0?
            [Question._0Number] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое число было изначально показано на {0}?",
            },

            // 1000 Words
            // What was the {1} word shown in {0}?
            // What was the first word shown in 1000 Words?
            [Question._1000WordsWords] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какое было {1}-е показанное слово {0}?",
                ModuleName = "1000 слов",
            },

            // 100 Levels of Defusal
            // What was the {1} displayed letter in {0}?
            // What was the first displayed letter in 100 Levels of Defusal?
            [Question._100LevelsOfDefusalLetters] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какая была {1}-я показанная буква {0}?",
                ModuleName = "100 уровнях обезвреживания",
            },

            // The 1, 2, 3 Game
            // Who was the opponent in {0}?
            // Who was the opponent in The 1, 2, 3 Game?
            [Question._123GameProfile] = new()
            {
                QuestionText = "Кто был вашим оппонентом {0}?",
            },
            // Who was the opponent in {0}?
            // Who was the opponent in The 1, 2, 3 Game?
            [Question._123GameName] = new()
            {
                QuestionText = "Кто был вашим оппонентом {0}?",
            },

            // 1D Chess
            // What was {1} in {0}?
            // What was your first move in 1D Chess?
            [Question._1DChessMoves] = new()
            {
                QuestionText = "Каким был {1} {0}?",
                FormatArgs = new Dictionary<string, string>
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

            // 3D Maze
            // What were the markings in {0}?
            // What were the markings in 3D Maze?
            [Question._3DMazeMarkings] = new()
            {
                Conjugation = Conjugation.NominativeMasculine,
                QuestionText = "Какими буквами был обозначен ваш {0}?",
                ModuleName = "3D лабиринт",
            },
            // What was the cardinal direction in {0}?
            // What was the cardinal direction in 3D Maze?
            [Question._3DMazeBearing] = new()
            {
                QuestionText = "Какое было направление нужной стены {0}?",
                ModuleName = "3D лабиринте",
                Answers = new Dictionary<string, string>
                {
                    ["North"] = "Север",
                    ["South"] = "Юг",
                    ["West"] = "Запад",
                    ["East"] = "Восток",
                },
            },

            // 3D Tap Code
            // What was the received word in {0}?
            // What was the received word in 3D Tap Code?
            [Question._3DTapCodeWord] = new()
            {
                QuestionText = "Какое слово было получено {0}?",
            },

            // 3D Tunnels
            // What was the {1} goal node in {0}?
            // What was the first goal node in 3D Tunnels?
            [Question._3DTunnelsTargetNode] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какой символ был вашей {1}-й целью {0}?",
                ModuleName = "3D тоннелях",
            },

            // 3 LEDs
            // What was the initial state of the LEDs in {0} (in reading order)?
            // What was the initial state of the LEDs in 3 LEDs (in reading order)?
            [Question._3LEDsInitialState] = new()
            {
                Conjugation = Conjugation.GenitivePlural,
                QuestionText = "Какое было исходное состояние у {0} (в порядке чтения)?",
                Answers = new Dictionary<string, string>
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

            // 3N+1
            // What number was initially displayed in {0}?
            // What number was initially displayed in 3N+1?
            [Question._3NPlus1] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое число было изначально показано на {0}?",
            },

            // 64
            // What was the displayed number in {0}?
            // What was the displayed number in 64?
            [Question._64DisplayedNumber] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое число было показано на {0}?",
            },

            // 7
            // What was the {1} channel’s initial value in {0}?
            // What was the red channel’s initial value in 7?
            [Question._7InitialValues] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое было начальное значение {1} канала у {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красного",
                    ["green"] = "зелёного",
                    ["blue"] = "синего",
                },
            },
            // What LED color was shown in stage {1} of {0}?
            // What LED color was shown in stage 0 of 7?
            [Question._7LedColors] = new()
            {
                QuestionText = "Какой цвет был у светодиода на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красный",
                    ["blue"] = "Синий",
                    ["green"] = "Зелёный",
                    ["white"] = "Белый",
                },
            },

            // 9-Ball
            // What was the number of ball {1} in {0}?
            // What was the number of ball A in 9-Ball?
            [Question._9BallLetters] = new()
            {
                QuestionText = "Какой был номер у шара \"{1}\" {0}?",
            },
            // What was the letter of ball {1} in {0}?
            // What was the letter of ball 2 in 9-Ball?
            [Question._9BallNumbers] = new()
            {
                QuestionText = "Какая была буква у шара \"{1}\" {0}?",
            },

            // Abyss
            // What was the {1} character displayed on {0}?
            // What was the first character displayed on Abyss?
            [Question.AbyssSeed] = new()
            {
                QuestionText = "Какой был {1}-й показанный символ {0}?",
            },

            // Accumulation
            // What was the background color on the {1} stage in {0}?
            // What was the background color on the first stage in Accumulation?
            [Question.AccumulationBackgroundColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была подложка на {1}-м этапе {0}?",
                ModuleName = "Накопления",
                Answers = new Dictionary<string, string>
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
            // What was the border color in {0}?
            // What was the border color in Accumulation?
            [Question.AccumulationBorderColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета было обрамление у {0}?",
                ModuleName = "Накопления",
                Answers = new Dictionary<string, string>
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

            // Adventure Game
            // Which item was the {1} correct item you used in {0}?
            // Which item was the first correct item you used in Adventure Game?
            [Question.AdventureGameCorrectItem] = new()
            {
                QuestionText = "Какой был {1}-й правильный предмет, который вы использовали {0}?",
                ModuleName = "Приключении",
            },
            // What enemy were you fighting in {0}?
            // What enemy were you fighting in Adventure Game?
            [Question.AdventureGameEnemy] = new()
            {
                QuestionText = "С каким врагом вы сражались {0}?",
                ModuleName = "Приключении",
            },

            // Affine Cycle
            // Which direction was the {1} dial pointing in {0}?
            // Which direction was the first dial pointing in Affine Cycle?
            [Question.AffineCycleDialDirections] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which direction was the {1} dial pointing in {0}?",
            },
            // What letter was written on the {1} dial in {0}?
            // What letter was written on the first dial in Affine Cycle?
            [Question.AffineCycleDialLabels] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What letter was written on the {1} dial in {0}?",
            },

            // Alcoholic Rampage
            // Who was the {1} mercenary displayed in {0}?
            // Who was the first mercenary displayed in Alcoholic Rampage?
            [Question.AlcoholicRampageMercenaries] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Who was the {1} mercenary you killed in {0}?",
            },

            // A Letter
            // What was the initial letter in {0}?
            // What was the initial letter in A Letter?
            [Question.ALetterInitialLetter] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какая была начальная буква {0}?",
            },

            // Alfa-Bravo
            // Which letter was pressed in {0}?
            // Which letter was pressed in Alfa-Bravo?
            [Question.AlfaBravoPressedLetter] = new()
            {
                QuestionText = "Какая буква была нажата {0}?",
            },
            // Which letter was to the left of the pressed one in {0}?
            // Which letter was to the left of the pressed one in Alfa-Bravo?
            [Question.AlfaBravoLeftPressedLetter] = new()
            {
                QuestionText = "Какая буква была слева от нажатой {0}?",
            },
            // Which letter was to the right of the pressed one in {0}?
            // Which letter was to the right of the pressed one in Alfa-Bravo?
            [Question.AlfaBravoRightPressedLetter] = new()
            {
                QuestionText = "Какая буква была справа от нажатой {0}?",
            },
            // What was the last digit on the small display in {0}?
            // What was the last digit on the small display in Alfa-Bravo?
            [Question.AlfaBravoDigit] = new()
            {
                QuestionText = "Какая была последняя цифра на маленьком экране {0}?",
            },

            // Algebra
            // What was the first equation in {0}?
            // What was the first equation in Algebra?
            [Question.AlgebraEquation1] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какое было первое уравнение {0}?",
                ModuleName = "Алгебре",
            },
            // What was the second equation in {0}?
            // What was the second equation in Algebra?
            [Question.AlgebraEquation2] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какое было второе уравнение {0}?",
                ModuleName = "Алгебре",
            },

            // Algorithmia
            // Which position was the {1} position in {0}?
            // Which position was the starting position in Algorithmia?
            [Question.AlgorithmiaPositions] = new()
            {
                QuestionText = "Какая позиция была {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["starting"] = "начальной",
                    ["goal"] = "целевой",
                },
            },
            // What was the color of the colored bulb in {0}?
            // What was the color of the colored bulb in Algorithmia?
            [Question.AlgorithmiaColor] = new()
            {
                QuestionText = "Какого цвета была цветная лампочка {0}?",
            },
            // Which number was present in the seed in {0}?
            // Which number was present in the seed in Algorithmia?
            [Question.AlgorithmiaSeed] = new()
            {
                QuestionText = "Какое число присутствовало в зерне {0}?",
            },

            // Alphabetical Ruling
            // What was the letter displayed in the {1} stage of {0}?
            // What was the letter displayed in the first stage of Alphabetical Ruling?
            [Question.AlphabeticalRulingLetter] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая буква была показана на {1}-м этапе {0}?",
            },
            // What was the number displayed in the {1} stage of {0}?
            // What was the number displayed in the first stage of Alphabetical Ruling?
            [Question.AlphabeticalRulingNumber] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое число было показано на {1}-м этапе {0}?",
            },

            // Alphabet Numbers
            // Which of these numbers was on one of the buttons in the {1} stage of {0}?
            // Which of these numbers was on one of the buttons in the first stage of Alphabet Numbers?
            [Question.AlphabetNumbersDisplayedNumbers] = new()
            {
                QuestionText = "Какое из этих чисел было на одной из кнопок на {1}-м этапе {0}?",
            },

            // Alphabet Tiles
            // What was the {1} letter shown during the cycle in {0}?
            // What was the first letter shown during the cycle in Alphabet Tiles?
            [Question.AlphabetTilesCycle] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "В цикле {0}, какая была {1}-я буква?",
            },
            // What was the missing letter in {0}?
            // What was the missing letter in Alphabet Tiles?
            [Question.AlphabetTilesMissingLetter] = new()
            {
                QuestionText = "Какая буква отсутствовала {0}?",
            },

            // Alpha-Bits
            // What character was displayed on the {1} screen on the {2} in {0}?
            // What character was displayed on the first screen on the left in Alpha-Bits?
            [Question.AlphaBitsDisplayedCharacters] = new()
            {
                QuestionText = "Какой символ был на {1}-м экране {2} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "слева",
                    ["right"] = "справа",
                },
            },

            // Amusement Parks
            // Which ride was available in {0}?
            // Which ride was available in Amusement Parks?
            [Question.AmusementParksRides] = new()
            {
                QuestionText = "Какой аттракцион был доступен {0}?",
            },

            // Ángel Hernández
            // What letter was shown by the raised buttons on the {1} stage on {0}?
            // What letter was shown by the raised buttons on the first stage on Ángel Hernández?
            [Question.AngelHernandezMainLetter] = new()
            {
                QuestionText = "Какая буква была показана поднятой кнопкой на {1}-м этапе {0}?",
            },

            // The Arena
            // What was the maximum weapon damage of the attack phase in {0}?
            // What was the maximum weapon damage of the attack phase in The Arena?
            [Question.ArenaDamage] = new()
            {
                QuestionText = "Какой был максимальный урон оружия в фазе атаки {0}?",
            },
            // Which enemy was present in the defend phase of {0}?
            // Which enemy was present in the defend phase of The Arena?
            [Question.ArenaEnemies] = new()
            {
                QuestionText = "Какой враг присутствовал в фазе защиты {0}?",
            },
            // Which was a number present in the grab phase of {0}?
            // Which was a number present in the grab phase of The Arena?
            [Question.ArenaNumbers] = new()
            {
                QuestionText = "Какое число присутствовало в фазе захвата {0}?",
            },

            // Arithmelogic
            // What was the symbol on the submit button in {0}?
            // What was the symbol on the submit button in Arithmelogic?
            [Question.ArithmelogicSubmit] = new()
            {
                QuestionText = "Какой символ был на кнопке отправки ответа {0}?",
            },
            // Which number was selectable, but not the solution, in the {1} screen on {0}?
            // Which number was selectable, but not the solution, in the left screen on Arithmelogic?
            [Question.ArithmelogicNumbers] = new()
            {
                QuestionText = "Какое число присутствовало (но не являлось решением) на {1} экране {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левом",
                    ["middle"] = "центральном",
                    ["right"] = "правом",
                },
            },

            // ASCII Maze
            // What was the {1} character displayed on {0}?
            // What was the first character displayed on ASCII Maze?
            [Question.ASCIIMazeCharacters] = new()
            {
                QuestionText = "Какой был {1}-й символ, отображённый {0}?",
            },

            // A Square
            // Which of these was an index color in {0}?
            // Which of these was an index color in A Square?
            [Question.ASquareIndexColors] = new()
            {
                QuestionText = "Какой из этих цветов был индексным {0}?",
            },
            // Which color was submitted {1} in {0}?
            // Which color was submitted first in A Square?
            [Question.ASquareCorrectColors] = new()
            {
                QuestionText = "Какой цвет был отправлен {1}-м {0}?",
            },

            // Audio Morse
            // What was signaled in {0}?
            // What was signaled in Audio Morse?
            [Question.AudioMorseSound] = new()
            {
                QuestionText = "Что было в сигнале {0}?",
            },

            // The Azure Button
            // What was T in {0}?
            // What was T in The Azure Button?
            [Question.AzureButtonT] = new()
            {
                QuestionText = "Какое была карта T {0}?",
            },
            // Which of these cards was shown in Stage 1, but not T, in {0}?
            // Which of these cards was shown in Stage 1, but not T, in The Azure Button?
            [Question.AzureButtonNotT] = new()
            {
                QuestionText = "Какая из этих карт была показана на первом этапе (но не T) {0}?",
            },
            // What was M in {0}?
            // What was M in The Azure Button?
            [Question.AzureButtonM] = new()
            {
                QuestionText = "Какое значение было у M {0}?",
            },
            // What was the {1} direction in the decoy arrow in {0}?
            // What was the first direction in the decoy arrow in The Azure Button?
            [Question.AzureButtonDecoyArrowDirection] = new()
            {
                QuestionText = "Какое было {1}-е направление у стрелки-ловушки {0}?",
            },
            // What was the {1} direction in the {2} non-decoy arrow in {0}?
            // What was the first direction in the first non-decoy arrow in The Azure Button?
            [Question.AzureButtonNonDecoyArrowDirection] = new()
            {
                QuestionText = "Какое было {1}-е направление у {2}-й стрелки (не ловушки) {0}?",
            },

            // Bakery
            // Which menu item was present in {0}?
            // Which menu item was present in Bakery?
            [Question.BakeryItems] = new()
            {
                QuestionText = "Какая позиция меню присутствовала {0}?",
            },

            // Bamboozled Again
            // What color was the {1} correct button in {0}?
            // What color was the first correct button in Bamboozled Again?
            [Question.BamboozledAgainButtonColor] = new()
            {
                QuestionText = "Какого цвета была {1}-я правильная кнопка {0}?",
                ModuleName = "Повторном надувательстве",
                Answers = new Dictionary<string, string>
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
            // What was the text on the {1} correct button in {0}?
            // What was the text on the first correct button in Bamboozled Again?
            [Question.BamboozledAgainButtonText] = new()
            {
                QuestionText = "Какая была надпись на {1}-й правильной кнопке {0}?",
                ModuleName = "Повторном надувательстве",
            },
            // What was the {1} decrypted text on the display in {0}?
            // What was the first decrypted text on the display in Bamboozled Again?
            [Question.BamboozledAgainDisplayTexts1] = new()
            {
                QuestionText = "Какой был {1}-й расшифрованный текст на экране {0}?",
                ModuleName = "Повторном надувательстве",
            },
            // What was the {1} decrypted text on the display in {0}?
            // What was the first decrypted text on the display in Bamboozled Again?
            [Question.BamboozledAgainDisplayTexts2] = new()
            {
                QuestionText = "Какой был {1}-й расшифрованный текст на экране {0}?",
                ModuleName = "Повторном надувательстве",
            },
            // What color was the {1} text on the display in {0}?
            // What color was the first text on the display in Bamboozled Again?
            [Question.BamboozledAgainDisplayColor] = new()
            {
                QuestionText = "Какого цвета был {1}-й текст на экране {0}?",
                ModuleName = "Повторном надувательстве",
                Answers = new Dictionary<string, string>
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

            // Bamboozling Button
            // What color was the button in the {1} stage of {0}?
            // What color was the button in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была кнопка на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
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
            // What was the {2} label on the button in the {1} stage of {0}?
            // What was the top label on the button in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonLabel] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была {2} надпись на кнопке на {1}-м этапе {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхняя",
                    ["bottom"] = "нижняя",
                },
            },
            // What was the {2} display in the {1} stage of {0}?
            // What was the first display in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonDisplay] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был {2}-й экран на {1}-м этапе {0}?",
            },
            // What was the color of the {2} display in the {1} stage of {0}?
            // What was the color of the first display in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonDisplayColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета был {2}-й экран на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
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

            // Bar Charts
            // What was the category of {0}?
            // What was the category of Bar Charts?
            [Question.BarChartsCategory] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была категория у {0}?",
            },
            // What was the color of the {1} bar in {0}?
            // What was the color of the first bar in Bar Charts?
            [Question.BarChartsColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета был {1}-й столбец {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красного",
                    ["Yellow"] = "Жёлтого",
                    ["Green"] = "Зелёного",
                    ["Blue"] = "Синего",
                },
            },
            // What was the position of the {1} bar in {0}?
            // What was the position of the shortest bar in Bar Charts?
            [Question.BarChartsHeight] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Где находился {1} столбец {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["shortest"] = "самый короткий",
                    ["second shortest"] = "третий по высоте",
                    ["second tallest"] = "второй по высоте",
                    ["tallest"] = "самый высокий",
                },
            },
            // What was the label of the {1} bar in {0}?
            // What was the label of the first bar in Bar Charts?
            [Question.BarChartsLabel] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая надпись была у {1}-го столбца {0}?",
            },
            // What was the unit of {0}?
            // What was the unit of Bar Charts?
            [Question.BarChartsUnit] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была единица измерения у {0}?",
            },

            // Barcode Cipher
            // What was the screen number in {0}?
            // What was the screen number in Barcode Cipher?
            [Question.BarcodeCipherScreenNumber] = new()
            {
                QuestionText = "Какой был номер экрана {0}?",
            },
            // What was the edgework represented by the {1} barcode in {0}?
            // What was the edgework represented by the first barcode in Barcode Cipher?
            [Question.BarcodeCipherBarcodeEdgework] = new()
            {
                QuestionText = "Какой компонент бомбы был представлен {1}-м штрихкодом {0}?",
                Answers = new Dictionary<string, string>
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
            // What was the answer for the {1} barcode in {0}?
            // What was the answer for the first barcode in Barcode Cipher?
            [Question.BarcodeCipherBarcodeAnswers] = new()
            {
                QuestionText = "Какой был ответ на {1}-й штрихкод {0}?",
            },

            // Bartending
            // Which ingredient was in the {1} position on {0}?
            // Which ingredient was in the first position on Bartending?
            [Question.BartendingIngredients] = new()
            {
                QuestionText = "Какой ингредиент был на {1}-й позиции {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Adelhyde"] = "Adelhyde",
                    ["Flanergide"] = "Flanergide",
                    ["Bronson Extract"] = "Bronson Extract",
                    ["Karmotrine"] = "Karmotrine",
                    ["Powdered Delta"] = "Powdered Delta",
                },
            },

            // Beans
            // What was this bean in {0}?
            // What was this bean in Beans?
            [Question.BeansColors] = new()
            {
                QuestionText = "Каким был данный боб {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Wobbly Orange"] = "Wobbly Orange",
                    ["Wobbly Yellow"] = "Wobbly Yellow",
                    ["Wobbly Green"] = "Wobbly Green",
                    ["Not Wobbly Orange"] = "Not Wobbly Orange",
                    ["Not Wobbly Yellow"] = "Not Wobbly Yellow",
                    ["Not Wobbly Green"] = "Not Wobbly Green",
                },
            },

            // Bean Sprouts
            // What was sprout {1} in {0}?
            // What was sprout 1 in Bean Sprouts?
            [Question.BeanSproutsColors] = new()
            {
                QuestionText = "Каким был росток {1} {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Raw"] = "Raw",
                    ["Cooked"] = "Cooked",
                    ["Burnt"] = "Burnt",
                    ["Fake"] = "Fake",
                },
            },
            // What bean was on sprout {1} in {0}?
            // What bean was on sprout 1 in Bean Sprouts?
            [Question.BeanSproutsBeans] = new()
            {
                QuestionText = "Какой боб был на {1}-м ростке {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Left"] = "Левый",
                    ["Right"] = "Правый",
                    ["None"] = "Никакой",
                    ["Both"] = "Оба",
                },
            },

            // Big Bean
            // What was the bean in {0}?
            // What was the bean in Big Bean?
            [Question.BigBeanColor] = new()
            {
                QuestionText = "Каким был боб {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Wobbly Orange"] = "Wobbly Orange",
                    ["Wobbly Yellow"] = "Wobbly Yellow",
                    ["Wobbly Green"] = "Wobbly Green",
                    ["Not Wobbly Orange"] = "Not Wobbly Orange",
                    ["Not Wobbly Yellow"] = "Not Wobbly Yellow",
                    ["Not Wobbly Green"] = "Not Wobbly Green",
                },
            },

            // Big Circle
            // What color was {1} in the solution to {0}?
            // What color was first in the solution to Big Circle?
            [Question.BigCircleColors] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой правильный цвет был {1}-м на {0}?",
                ModuleName = "Большом круге",
                Answers = new Dictionary<string, string>
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

            // Binary LEDs
            // At which numeric value did you cut the correct wire in {0}?
            // At which numeric value did you cut the correct wire in Binary LEDs?
            [Question.BinaryLEDsValue] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "На каком числе вы перерезали верный провод {0}?",
                ModuleName = "Двоичных светодиодах",
            },

            // Binary Shift
            // What was the {1} initial number in {0}?
            // What was the top-left initial number in Binary Shift?
            [Question.BinaryShiftInitialNumber] = new()
            {
                QuestionText = "Какое было начальное число {1} {0}?",
                FormatArgs = new Dictionary<string, string>
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
            // What number was selected at stage {1} in {0}?
            // What number was selected at stage 0 in Binary Shift?
            [Question.BinaryShiftSelectedNumberPossition] = new()
            {
                QuestionText = "Какое число было выбрано на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
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
            // What number was not selected at stage {1} in {0}?
            // What number was not selected at stage 0 in Binary Shift?
            [Question.BinaryShiftNotSelectedNumberPossition] = new()
            {
                QuestionText = "Какое число не было выбрано на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
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

            // Binary
            // What word was displayed in {0}?
            // What word was displayed in Binary?
            [Question.BinaryWord] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое слово было отображено на {0}?",
            },

            // Bitmaps
            // How many pixels were {1} in the {2} quadrant in {0}?
            // How many pixels were white in the top left quadrant in Bitmaps?
            [Question.Bitmaps] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Сколько было {1} пикселей в {2} квадранте {0}?",
                ModuleName = "Битовых изображениях",
                FormatArgs = new Dictionary<string, string>
                {
                    ["white"] = "белых",
                    ["top left"] = "левом верхнем",
                    ["top right"] = "правом верхнем",
                    ["bottom left"] = "нижнем левом",
                    ["bottom right"] = "нижнем правом",
                    ["black"] = "чёрных",
                },
            },

            // Black Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Black Cipher?
            [Question.BlackCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Blind Maze
            // What color was the {1} button in {0}?
            // What color was the north button in Blind Maze?
            [Question.BlindMazeColors] = new()
            {
                QuestionText = "Какого цвета была {1} кнопка {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["north"] = "северная",
                    ["east"] = "восточная",
                    ["west"] = "западная",
                    ["south"] = "южная",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красного",
                    ["Green"] = "Зелёного",
                    ["Blue"] = "Синего",
                    ["Gray"] = "Серого",
                    ["Yellow"] = "Жёлтого",
                },
            },
            // Which maze did you solve {0} on?
            // Which maze did you solve Blind Maze on?
            [Question.BlindMazeMaze] = new()
            {
                QuestionText = "Какой лабиринт вы прошли {0}?",
            },

            // Blinking Notes
            // What song was flashed in {0}?
            // What song was flashed in Blinking Notes?
            [Question.BlinkingNotesSong] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая песня мигала на {0}?",
            },

            // Blinkstop
            // How many times did the LED flash in {0}?
            // How many times did the LED flash in Blinkstop?
            [Question.BlinkstopNumberOfFlashes] = new()
            {
                QuestionText = "Сколько раз мигал светодиод {0}?",
            },
            // Which color did the LED flash the fewest times in {0}?
            // Which color did the LED flash the fewest times in Blinkstop?
            [Question.BlinkstopFewestFlashedColor] = new()
            {
                QuestionText = "Каким цветом светодиод мигал наименьшее количество раз {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Purple"] = "Фиолетовый",
                    ["Cyan"] = "Голубой",
                    ["Yellow"] = "Жёлтый",
                    ["Multicolor"] = "Разноцветный",
                },
            },

            // Blockbusters
            // What was the last letter pressed on {0}?
            // What was the last letter pressed on Blockbusters?
            [Question.BlockbustersLastLetter] = new()
            {
                QuestionText = "Какая буква была нажата последней {0}?",
            },

            // Blue Arrows
            // What were the characters on the screen in {0}?
            // What were the characters on the screen in Blue Arrows?
            [Question.BlueArrowsInitialCharacters] = new()
            {
                QuestionText = "Какие символы были на экране {0}?",
            },

            // The Blue Button
            // What was D in {0}?
            // What was D in The Blue Button?
            [Question.BlueButtonD] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое значение было у D на {0}?",
            },
            // What was {1} in {0}?
            // What was E in The Blue Button?
            [Question.BlueButtonEFGH] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое значение было у {1} на {0}?",
            },
            // What was M in {0}?
            // What was M in The Blue Button?
            [Question.BlueButtonM] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое значение было у M на {0}?",
            },
            // What was N in {0}?
            // What was N in The Blue Button?
            [Question.BlueButtonN] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое значение было у N на {0}?",
            },
            // What was P in {0}?
            // What was P in The Blue Button?
            [Question.BlueButtonP] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое значение было у P на {0}?",
            },
            // What was Q in {0}?
            // What was Q in The Blue Button?
            [Question.BlueButtonQ] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое значение было у Q на {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blue",
                    ["Green"] = "Green",
                    ["Cyan"] = "Cyan",
                    ["Red"] = "Red",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                },
            },
            // What was X in {0}?
            // What was X in The Blue Button?
            [Question.BlueButtonX] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое значение было у X на {0}?",
            },

            // Blue Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Blue Cipher?
            [Question.BlueCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Bob Barks
            // What was the {1} indicator label in {0}?
            // What was the top left indicator label in Bob Barks?
            [Question.BobBarksIndicators] = new()
            {
                QuestionText = "Какая была надпись {1} индикатора {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top left"] = "верхнего левого",
                    ["top right"] = "верхнего правого",
                    ["bottom left"] = "нижнего левого",
                    ["bottom right"] = "нижнего правого",
                },
            },
            // Which button flashed {1} in sequence in {0}?
            // Which button flashed first in sequence in Bob Barks?
            [Question.BobBarksPositions] = new()
            {
                QuestionText = "Какая кнопка была {1}-й в последовательности вспышек {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "Верхняя левая",
                    ["top right"] = "Верхняя правая",
                    ["bottom left"] = "Нижняя левая",
                    ["bottom right"] = "Нижняя правая",
                },
            },

            // Boggle
            // What letter was initially visible on {0}?
            // What letter was initially visible on Boggle?
            [Question.BoggleLetters] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая буква была изначально видна на {0}?",
            },

            // Bomb Diffusal
            // What was the license number in {0}?
            // What was the license number in Bomb Diffusal?
            [Question.BombDiffusalLicenseNumber] = new()
            {
                QuestionText = "Какой был номер лицензии {0}?",
            },

            // Bone Apple Tea
            // Which phrase was shown on {0}?
            // Which phrase was shown on Bone Apple Tea?
            [Question.BoneAppleTeaPhrase] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая фраза была показана на {0}?",
                ModuleName = "Еле-еле ели ели",
            },

            // Boob Tube
            // Which word was shown on {0}?
            // Which word was shown on Boob Tube?
            [Question.BoobTubeWord] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое слово было показано на {0}?",
            },

            // Book of Mario
            // Who said the {1} quote in {0}?
            // Who said the first quote in Book of Mario?
            [Question.BookOfMarioPictures] = new()
            {
                QuestionText = "Кто сказал {1}-ю цитату {0}?",
            },
            // What did {1} say in the {2} stage of {0}?
            // What did Goombell say in the first stage of Book of Mario?
            [Question.BookOfMarioQuotes] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Что сказал {1} на {2}-м этапе {0}?",
            },

            // Boolean Wires
            // Which operator did you submit in the {1} stage of {0}?
            // Which operator did you submit in the first stage of Boolean Wires?
            [Question.BooleanWiresEnteredOperators] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой оператор был ответом на {1}-м этапе {0}?",
            },

            // Boomtar the Great
            // What was rule {1} in {0}?
            // What was rule one in Boomtar the Great?
            [Question.BoomtarTheGreatRules] = new()
            {
                QuestionText = "Какое было {1} правило {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["one"] = "первое",
                    ["two"] = "второе",
                },
            },

            // Bordered Keys
            // What was the border color when you pressed the {1} key in {0}?
            // What was the border color when you pressed the first key in Bordered Keys?
            [Question.BorderedKeysBorderColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была рамка, когда вы нажали {1}-ю клавишу {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                },
            },
            // What was the digit displayed when you pressed the {1} key in {0}?
            // What was the digit displayed when you pressed the first key in Bordered Keys?
            [Question.BorderedKeysDigit] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая цифра отображалась на дисплее, когда вы нажали {1}-ю клавишу {0}?",
            },
            // What was the key color when you pressed the {1} key in {0}?
            // What was the key color when you pressed the first key in Bordered Keys?
            [Question.BorderedKeysKeyColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была клавиша, когда вы нажали {1}-ю клавишу {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                },
            },
            // What was the label when you pressed the {1} key in {0}?
            // What was the label when you pressed the first key in Bordered Keys?
            [Question.BorderedKeysLabel] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была надпись, когда вы нажали {1}-ю клавишу {0}?",
            },
            // What was the label color when you pressed the {1} key in {0}?
            // What was the label color when you pressed the first key in Bordered Keys?
            [Question.BorderedKeysLabelColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была надпись, когда вы нажали {1}-ю клавишу {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                },
            },

            // Bottom Gear
            // What tweet was shown in {0}?
            // What tweet was shown in Bottom Gear?
            [Question.BottomGearTweet] = new()
            {
                QuestionText = "Какой твит был показан {0}?",
            },

            // Boxing
            // Which {1} appeared on {0}?
            // Which contestant’s first name appeared on Boxing?
            [Question.BoxingNames] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "{1} было показано на {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["contestant’s first name"] = "Какое имя участника",
                    ["contestant’s last name"] = "Какая фамилия участника",
                    ["substitute’s first name"] = "Какое имя запасного участника",
                    ["substitute’s last name"] = "Какая фамилия запасного участника",
                },
            },
            // What was the {1} of the contestant with strength rating {2} on {0}?
            // What was the first name of the contestant with strength rating 0 on Boxing?
            [Question.BoxingContestantByStrength] = new()
            {
                QuestionText = "{1} участника с оценкой силы {2} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first name"] = "Какое было имя",
                    ["last name"] = "Какая была фамилия",
                    ["substitute’s first name"] = "Какое было имя запасного",
                    ["substitute’s last name"] = "Какая была фамилия запасного",
                },
            },
            // What was {1}’s strength rating on {0}?
            // What was Muhammad’s strength rating on Boxing?
            [Question.BoxingStrengthByContestant] = new()
            {
                QuestionText = "Какая была оценка силы у {1} {0}?",
            },

            // Braille
            // What was the {1} pattern in {0}?
            // What was the first pattern in Braille?
            [Question.BraillePattern] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был {1}-й паттерн {0}?",
                ModuleName = "Шрифта Брайля",
            },

            // Breakfast Egg
            // Which color appeared on the egg in {0}?
            // Which color appeared on the egg in Breakfast Egg?
            [Question.BreakfastEggColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был цвет у {0}?",
                Answers = new Dictionary<string, string>
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

            // Broken Buttons
            // What was the {1} correct button you pressed in {0}?
            // What was the first correct button you pressed in Broken Buttons?
            [Question.BrokenButtons] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какая была {1}-я правильная нажатая кнопка {0}?",
                ModuleName = "Сломанных кнопках",
            },

            // Broken Guitar Chords
            // What was the displayed chord in {0}?
            // What was the displayed chord in Broken Guitar Chords?
            [Question.BrokenGuitarChordsDisplayedChord] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой аккорд был показан на {0}?",
            },
            // In which position, from left to right, was the broken string in {0}?
            // In which position, from left to right, was the broken string in Broken Guitar Chords?
            [Question.BrokenGuitarChordsMutedString] = new()
            {
                QuestionText = "На какой позиции (слева направо) была сломанная струна {0}?",
            },

            // Brown Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Brown Cipher?
            [Question.BrownCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Brush Strokes
            // What was the color of the middle contact point in {0}?
            // What was the color of the middle contact point in Brush Strokes?
            [Question.BrushStrokesMiddleColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была центральная точка {0}?",
                Answers = new Dictionary<string, string>
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

            // The Bulb
            // Was the bulb initially lit in {0}?
            // Was the bulb initially lit in The Bulb?
            [Question.BulbInitialState] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Was the bulb initially lit in {0}?",
            },

            // Burger Alarm
            // What was the {1} displayed digit in {0}?
            // What was the first displayed digit in Burger Alarm?
            [Question.BurgerAlarmDigits] = new()
            {
                QuestionText = "Какая была {1}-я цифра {0}?",
            },
            // What was the {1} order number in {0}?
            // What was the first order number in Burger Alarm?
            [Question.BurgerAlarmOrderNumbers] = new()
            {
                QuestionText = "Какой был номер {1}-го заказа {0}?",
            },

            // Burglar Alarm
            // What was the {1} displayed digit in {0}?
            // What was the first displayed digit in Burglar Alarm?
            [Question.BurglarAlarmDigits] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какая была {1}-я цифра {0}?",
                ModuleName = "Сигнализации",
            },

            // The Button
            // What color did the light glow in {0}?
            // What color did the light glow in The Button?
            [Question.ButtonLightColor] = new()
            {
                Conjugation = Conjugation.GenitiveFeminine,
                QuestionText = "Каким цветом горела цветная полоска {0}?",
                ModuleName = "Кнопки",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красным",
                    ["blue"] = "Синим",
                    ["yellow"] = "Жёлтым",
                    ["white"] = "Белым",
                },
            },

            // Buttonage
            // How many {1} buttons were there on {0}?
            // How many red buttons were there on Buttonage?
            [Question.ButtonageButtons] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Сколько {1} было на {0}?",
                FormatArgs = new Dictionary<string, string>
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

            // Button Sequence
            // How many of the buttons in {0} were {1}?
            // How many of the buttons in Button Sequence were red?
            [Question.ButtonSequencesColorOccurrences] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Сколько было {1} кнопок {0}?",
                ModuleName = "Последовательности кнопок",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красных",
                    ["blue"] = "синих",
                    ["yellow"] = "жёлтых",
                    ["white"] = "белых",
                },
            },

            // Cacti’s Conundrum
            // What color was the LED in the {1} stage of {0}?
            // What color was the LED in the first stage of Cacti’s Conundrum?
            [Question.CactisConundrumColor] = new()
            {
                QuestionText = "Какого цвета был светодиод на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blue",
                    ["Lime"] = "Lime",
                    ["Orange"] = "Orange",
                    ["Red"] = "Red",
                },
            },

            // Caesar Cycle
            // Which direction was the {1} dial pointing in {0}?
            // Which direction was the first dial pointing in Caesar Cycle?
            [Question.CaesarCycleDialDirections] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which direction was the {1} dial pointing in {0}?",
            },
            // What letter was written on the {1} dial in {0}?
            // What letter was written on the first dial in Caesar Cycle?
            [Question.CaesarCycleDialLabels] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What letter was written on the {1} dial in {0}?",
            },

            // Caesar Psycho
            // What text was on the top display in the {1} stage of {0}?
            // What text was on the top display in the first stage of Caesar Psycho?
            [Question.CaesarPsychoScreenTexts] = new()
            {
                QuestionText = "Какой текст был на верхнем экране на {1}-м этапе {0}?",
            },
            // What color was the text on the top display in the second stage of {0}?
            // What color was the text on the top display in the second stage of Caesar Psycho?
            [Question.CaesarPsychoScreenColor] = new()
            {
                QuestionText = "Какого цвета был текст на верхнем экране на втором этапе {0}?",
            },

            // Calendar
            // What was the LED color in {0}?
            // What was the LED color in Calendar?
            [Question.CalendarLedColor] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какого цвета был индикатор на {0}?",
                ModuleName = "Календаре",
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "Зелёный",
                    ["Yellow"] = "Жёлтый",
                    ["Red"] = "Красный",
                    ["Blue"] = "Синий",
                },
            },

            // CA-RPS
            // What color was this cell initially in {0}?
            // What color was this cell initially in CA-RPS?
            [Question.CARPSCell] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была эта клетка в начале {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Black"] = "Black",
                },
            },

            // Cartinese
            // What color was the {1} button in {0}?
            // What color was the up button in Cartinese?
            [Question.CartineseButtonColors] = new()
            {
                QuestionText = "Какого цвета была кнопка \"{1}\" {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["up"] = "вверх",
                    ["right"] = "вправо",
                    ["down"] = "вниз",
                    ["left"] = "влево",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красного",
                    ["Yellow"] = "Жёлтого",
                    ["Green"] = "Зелёного",
                    ["Blue"] = "Синего",
                },
            },
            // What lyric was played by the {1} button in {0}?
            // What lyric was played by the up button in Cartinese?
            [Question.CartineseLyrics] = new()
            {
                QuestionText = "Какая лирика прозвучала при нажатии кнопки \"{1}\" {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["up"] = "вверх",
                    ["right"] = "вправо",
                    ["down"] = "вниз",
                    ["left"] = "влево",
                },
            },

            // Catchphrase
            // What was the colour of the {1} panel in {0}?
            // What was the colour of the top-left panel in Catchphrase?
            [Question.CatchphraseColour] = new()
            {
                QuestionText = "Какого цвета была панель {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "сверху слева",
                    ["top-right"] = "сверху справа",
                    ["bottom-left"] = "снизу слева",
                    ["bottom-right"] = "снизу справа",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                    ["Orange"] = "Оранжевый",
                    ["Purple"] = "Фиолетовый",
                    ["Yellow"] = "Жёлтый",
                },
            },

            // Challenge & Contact
            // What was the {1} submitted answer in {0}?
            // What was the first submitted answer in Challenge & Contact?
            [Question.ChallengeAndContactAnswers] = new()
            {
                QuestionText = "Какой был {1}-й введённый ответ {0}?",
            },

            // Character Codes
            // What was the {1} character in {0}?
            // What was the first character in Character Codes?
            [Question.CharacterCodesCharacter] = new()
            {
                QuestionText = "Какой был {1}-й символ {0}?",
            },

            // Character Shift
            // Which letter was present but not submitted on the left slider of {0}?
            // Which letter was present but not submitted on the left slider of Character Shift?
            [Question.CharacterShiftLetters] = new()
            {
                QuestionText = "Какой символ присутствовал, но не был введён на левом ползунке {0}?",
            },
            // Which digit was present but not submitted on the right slider of {0}?
            // Which digit was present but not submitted on the right slider of Character Shift?
            [Question.CharacterShiftDigits] = new()
            {
                QuestionText = "Какая цифра присутствовала, но не была введён на правом ползунке {0}?",
            },

            // Character Slots
            // Who was displayed in the {1} slot in the {2} stage of {0}?
            // Who was displayed in the first slot in the first stage of Character Slots?
            [Question.CharacterSlotsDisplayedCharacters] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Кто был показан в {1}-м слоте на {2}-м этапе {0}?",
            },

            // Cheap Checkout
            // What was {1} in {0}?
            // What was the paid amount in Cheap Checkout?
            [Question.CheapCheckoutPaid] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "{1} {0}?",
                ModuleName = "Свободной кассе",
                FormatArgs = new Dictionary<string, string>
                {
                    ["the paid amount"] = "Сколько всего денег было заплачено",
                    ["the first paid amount"] = "Каким был первый платёж",
                    ["the second paid amount"] = "Каким был второй платёж",
                },
            },

            // Cheep Checkout
            // Which bird {1} present in {0}?
            // Which bird was present in Cheep Checkout?
            [Question.CheepCheckoutBirds] = new()
            {
                QuestionText = "Какая птица {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "присутствовала",
                    ["was not"] = "отсутствовала",
                },
                Answers = new Dictionary<string, string>
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

            // Chess
            // What was the {1} coordinate in {0}?
            // What was the first coordinate in Chess?
            [Question.ChessCoordinate] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какие были {1}-е координаты {0}?",
                ModuleName = "Шахматах",
            },

            // Chinese Counting
            // What color was the {1} LED in {0}?
            // What color was the left LED in Chinese Counting?
            [Question.ChineseCountingLED] = new()
            {
                QuestionText = "Какой был цвет {1} светодиода {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левого",
                    ["right"] = "правого",
                },
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Белый",
                    ["Red"] = "Красный",
                    ["Green"] = "Зелёный",
                    ["Orange"] = "Оранжевый",
                },
            },

            // Chinese Remainder Theorem
            // Which equation was used in {0}?
            // Which equation was used in Chinese Remainder Theorem?
            [Question.ChineseRemainderTheoremEquations] = new()
            {
                QuestionText = "Какое уравнение было использовано {0}?",
            },

            // Chord Qualities
            // Which note was part of the given chord in {0}?
            // Which note was part of the given chord in Chord Qualities?
            [Question.ChordQualitiesNotes] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какая нота присутствовала в начальном аккорде {0}?",
                ModuleName = "Аккордных ладах",
            },

            // ↻↺
            // Which arrow was shown in {0}?
            // Which arrow was shown in ↻↺?
            [Question.ClockCounterArrows] = new()
            {
                QuestionText = "Какая стрелка была показана {0}?",
            },

            // The Code
            // What was the displayed number in {0}?
            // What was the displayed number in The Code?
            [Question.CodeDisplayNumber] = new()
            {
                QuestionText = "Какое было показанное число {0}?",
                ModuleName = "Коде",
            },

            // Codenames
            // Which of these words was submitted in {0}?
            // Which of these words was submitted in Codenames?
            [Question.CodenamesAnswers] = new()
            {
                QuestionText = "Какое из слов было введено {0}?",
            },

            // Coffee Beans
            // What was the {1} movement in {0}?
            // What was the first movement in Coffee Beans?
            [Question.CoffeeBeansMovements] = new()
            {
                QuestionText = "Какое было {1}-е движение {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Horizontal"] = "Горизонтальное",
                    ["Vertical"] = "Вертикальное",
                    ["Diagonal"] = "Диагональное",
                    ["Nothing"] = "Никакое",
                },
            },

            // Coffeebucks
            // What was the last served coffee in {0}?
            // What was the last served coffee in Coffeebucks?
            [Question.CoffeebucksCoffee] = new()
            {
                QuestionText = "Какое было последне поданное кофе {0}?",
            },

            // Coinage
            // Which coin was flipped in {0}?
            // Which coin was flipped in Coinage?
            [Question.CoinageFlip] = new()
            {
                QuestionText = "Какая монета была перевёрнута {0}?",
            },

            // Color Addition
            // What was {1}’s number in {0}?
            // What was red’s number in Color Addition?
            [Question.ColorAdditionNumbers] = new()
            {
                QuestionText = "Какое было {1} число {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красное",
                    ["green"] = "зелёное",
                    ["blue"] = "синее",
                },
            },

            // Color Braille
            // What color was this dot in {0}?
            // What color was this dot in Color Braille?
            [Question.ColorBrailleColor] = new()
            {
                QuestionText = "Какого цвета была эта точка {0}?",
                Answers = new Dictionary<string, string>
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

            // Color Decoding
            // What was the {1}-stage indicator pattern in {0}?
            // What was the first-stage indicator pattern in Color Decoding?
            [Question.ColorDecodingIndicatorPattern] = new()
            {
                Conjugation = Conjugation.GenitiveFeminine,
                QuestionText = "Какой был узор индикатора на {1}-м этапе {0}?",
                ModuleName = "Расшифровки цветов",
                Answers = new Dictionary<string, string>
                {
                    ["Checkered"] = "Шахматный",
                    ["Horizontal"] = "Горизонтальный",
                    ["Vertical"] = "Вертикальный",
                    ["Solid"] = "Сплошной",
                },
            },
            // Which color {1} in the {2}-stage indicator pattern in {0}?
            // Which color appeared in the first-stage indicator pattern in Color Decoding?
            [Question.ColorDecodingIndicatorColors] = new()
            {
                Conjugation = Conjugation.GenitiveFeminine,
                QuestionText = "Какой цвет {1} на узоре индикатора на {2}-м этапе {0}?",
                ModuleName = "Расшифровки цветов",
                FormatArgs = new Dictionary<string, string>
                {
                    ["appeared"] = "присутствовал",
                    ["did not appear"] = "отсутствовал",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "Зелёный",
                    ["Purple"] = "Фиолетовый",
                    ["Red"] = "Красный",
                    ["Blue"] = "Синий",
                    ["Yellow"] = "Жёлтый",
                },
            },

            // Colored Keys
            // What was the displayed word in {0}?
            // What was the displayed word in Colored Keys?
            [Question.ColoredKeysDisplayWord] = new()
            {
                Conjugation = Conjugation.PrepositivePlural,
                QuestionText = "Какое слово было отображено на дисплее на {0}?",
                ModuleName = "Цветных кнопках",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Red",
                    ["blue"] = "Blue",
                    ["green"] = "Green",
                    ["yellow"] = "Yellow",
                    ["purple"] = "Purple",
                    ["white"] = "White",
                },
            },
            // What was the displayed word’s color in {0}?
            // What was the displayed word’s color in Colored Keys?
            [Question.ColoredKeysDisplayWordColor] = new()
            {
                QuestionText = "Какого цвета было отображённое слово {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красного",
                    ["blue"] = "Синего",
                    ["green"] = "Зелёного",
                    ["yellow"] = "Жёлтого",
                    ["purple"] = "Фиолетового",
                    ["white"] = "Белого",
                },
            },
            // What was the color of the {1} key in {0}?
            // What was the color of the top-left key in Colored Keys?
            [Question.ColoredKeysKeyColor] = new()
            {
                QuestionText = "Какого цвета была {1} кнопка {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхняя левая",
                    ["top-right"] = "верхняя правая",
                    ["bottom-left"] = "нижняя левая",
                    ["bottom-right"] = "нижняя правая",
                },
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красного",
                    ["blue"] = "Синего",
                    ["green"] = "Зелёного",
                    ["yellow"] = "Жёлтого",
                    ["purple"] = "Фиолетового",
                    ["white"] = "Белого",
                },
            },
            // What letter was on the {1} key in {0}?
            // What letter was on the top-left key in Colored Keys?
            [Question.ColoredKeysKeyLetter] = new()
            {
                QuestionText = "Какая буква была на {1} кнопке на {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхней левой",
                    ["top-right"] = "верхней правой",
                    ["bottom-left"] = "нижней левой",
                    ["bottom-right"] = "нижней правой",
                },
            },

            // Colored Squares
            // What was the first color group in {0}?
            // What was the first color group in Colored Squares?
            [Question.ColoredSquaresFirstGroup] = new()
            {
                Conjugation = Conjugation.PrepositivePlural,
                QuestionText = "Какого цвета была первая группа на {0}?",
                ModuleName = "Цветных квадратах",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Белая",
                    ["Red"] = "Красная",
                    ["Blue"] = "Синяя",
                    ["Green"] = "Зелёная",
                    ["Yellow"] = "Жёлтая",
                    ["Magenta"] = "Розовая",
                },
            },

            // Colored Switches
            // What was the initial position of the switches in {0}?
            // What was the initial position of the switches in Colored Switches?
            [Question.ColoredSwitchesInitialPosition] = new()
            {
                Conjugation = Conjugation.GenitivePlural,
                QuestionText = "Какое было начальное положение {0}?",
                ModuleName = "Цветных переключателей",
            },
            // What was the position of the switches when the LEDs came on in {0}?
            // What was the position of the switches when the LEDs came on in Colored Switches?
            [Question.ColoredSwitchesWhenLEDsCameOn] = new()
            {
                Conjugation = Conjugation.GenitivePlural,
                QuestionText = "Какое было положение у {0}, когда загорелись светодиоды?",
                ModuleName = "Цветных переключателей",
            },

            // Color Morse
            // What was the color of the {1} LED in {0}?
            // What was the color of the first LED in Color Morse?
            [Question.ColorMorseColor] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какой был цвет {1}-го светодиода {0}?",
                ModuleName = "Цветной азбуке Морзе",
                Answers = new Dictionary<string, string>
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
            // What character was flashed by the {1} LED in {0}?
            // What character was flashed by the first LED in Color Morse?
            [Question.ColorMorseCharacter] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какой символ передавался через Морзе {1}-м светодиодом {0}?",
                ModuleName = "Цветной азбуке Морзе",
            },

            // Color One Two
            // What color was the {1} LED in {0}?
            // What color was the left LED in Color One Two?
            [Question.ColorOneTwoColor] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какого цвета был {1} светодиод на {0}?",
                ModuleName = "\"Цвет раз два\"",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левый",
                    ["right"] = "правый",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Blue"] = "Синий",
                    ["Green"] = "Зелёный",
                    ["Yellow"] = "Жёлтый",
                },
            },

            // Colors Maximization
            // How many buttons were {1} in {0}?
            // How many buttons were red in Colors Maximization?
            [Question.ColorsMaximizationColorCount] = new()
            {
                QuestionText = "Сколько было {1} кнопок {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красных",
                    ["green"] = "зелёных",
                    ["blue"] = "синих",
                },
            },

            // Coloured Cubes
            // What was the colour of this {1} in the {2} stage of {0}?
            // What was the colour of this cube in the first stage of Coloured Cubes?
            [Question.ColouredCubesColours] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был цвет данного {1} на {2}-м этапе {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["cube"] = "куба",
                    ["stage light"] = "индикатора этапа",
                },
                Answers = new Dictionary<string, string>
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

            // Coloured Cylinder
            // What was the {1} colour flashed on the cylinder in {0}?
            // What was the first colour flashed on the cylinder in Coloured Cylinder?
            [Question.ColouredCylinderColours] = new()
            {
                QuestionText = "Какой цвет мигал {1}-м на цилиндре {0}?",
                Answers = new Dictionary<string, string>
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

            // Colour Flash
            // What was the color of the last word in the sequence in {0}?
            // What was the color of the last word in the sequence in Colour Flash?
            [Question.ColourFlashLastColor] = new()
            {
                Conjugation = Conjugation.GenitiveFeminine,
                QuestionText = "Какого цвета было последнее слово в последовательности {0}?",
                ModuleName = "Цветной вспышки",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Yellow"] = "Жёлтый",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                    ["Magenta"] = "Розовый",
                    ["White"] = "Белый",
                },
            },

            // Concentration
            // What number began here in {0}?
            // What number began here in Concentration?
            [Question.ConcentrationStartingDigit] = new()
            {
                QuestionText = "Какое число было здесь изначально {0}?",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the Concentration which began with {1} in the {0} position (in reading order)"] = "в Concentration, где \"{1}\" изначально было в {0}-й позиции (в порядке чтения)",
                },
            },

            // Conditional Buttons
            // What was the color of this button in {0}?
            // What was the color of this button in Conditional Buttons?
            [Question.ConditionalButtonsColors] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какого цвета была эта кнопка на {0}?",
                Answers = new Dictionary<string, string>
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

            // Connected Monitors
            // What number was initially displayed on this screen in {0}?
            // What number was initially displayed on this screen in Connected Monitors?
            [Question.ConnectedMonitorsNumber] = new()
            {
                QuestionText = "Какое число было изначально отображено на данном экране {0}?",
            },
            // What colour was the indicator on this screen in {0}?
            // What colour was the indicator on this screen in Connected Monitors?
            [Question.ConnectedMonitorsSingleIndicator] = new()
            {
                QuestionText = "Какого цвета был индикатор на данном экране {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Orange"] = "Оранжевый",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                    ["Purple"] = "Фиолетовый",
                    ["White"] = "Белый",
                },
            },
            // What colour was the {1} indicator on this screen in {0}?
            // What colour was the first indicator on this screen in Connected Monitors?
            [Question.ConnectedMonitorsOrdinalIndicator] = new()
            {
                QuestionText = "Какого цвета был {1}-й индикатор на данном экране {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Orange"] = "Оранжевый",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                    ["Purple"] = "Фиолетовый",
                    ["White"] = "Белый",
                },
            },

            // Connection Check
            // What pair of numbers was present in {0}?
            // What pair of numbers was present in Connection Check?
            [Question.ConnectionCheckNumbers] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какая пара чисел присутствовала {0}?",
                ModuleName = "Проверке соединения",
            },

            // Coordinates
            // What was the solution you selected first in {0}?
            // What was the solution you selected first in Coordinates?
            [Question.CoordinatesFirstSolution] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какую координату вы выбрали первой {0}?",
                ModuleName = "Координатах",
            },
            // What was the grid size in {0}?
            // What was the grid size in Coordinates?
            [Question.CoordinatesSize] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "В каком формате был указан размер сетки {0}?",
                ModuleName = "Координатах",
            },

            // Coordination
            // What was the label of the starting coordinate in {0}?
            // What was the label of the starting coordinate in Coordination?
            [Question.CoordinationLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label of the starting coordinate in {0}?",
            },
            // Where was the starting coordinate in {0}?
            // Where was the starting coordinate in Coordination?
            [Question.CoordinationPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was the starting coordinate in {0}?",
            },

            // Coral Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Coral Cipher?
            [Question.CoralCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Corners
            // What was the color of the {1} corner in {0}?
            // What was the color of the top-left corner in Corners?
            [Question.CornersColors] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какого цвета был {1} угол {0}?",
                ModuleName = "Углах",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхний левый",
                    ["top-right"] = "верхний правый",
                    ["bottom-right"] = "нижний правый",
                    ["bottom-left"] = "нижний левый",
                },
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красного",
                    ["green"] = "Зелёного",
                    ["blue"] = "Синего",
                    ["yellow"] = "Жёлтого",
                },
            },
            // How many corners in {0} were {1}?
            // How many corners in Corners were red?
            [Question.CornersColorCount] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Сколько было {1} углов {0}?",
                ModuleName = "Углах",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красных",
                    ["green"] = "зелёных",
                    ["blue"] = "синих",
                    ["yellow"] = "жёлтых",
                },
            },

            // Cornflower Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Cornflower Cipher?
            [Question.CornflowerCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Cosmic
            // What was the number initially shown in {0}?
            // What was the number initially shown in Cosmic?
            [Question.CosmicNumber] = new()
            {
                QuestionText = "Какое число было изначально показано {0}?",
            },

            // Crazy Hamburger
            // What was the {1} ingredient shown in {0}?
            // What was the first ingredient shown in Crazy Hamburger?
            [Question.CrazyHamburgerIngredient] = new()
            {
                QuestionText = "Какой был {1}-й показанный ингредиент {0}?",
            },

            // Crazy Maze
            // What was the {1} location in {0}?
            // What was the starting location in Crazy Maze?
            [Question.CrazyMazeStartOrGoal] = new()
            {
                QuestionText = "Какая была {1} позиция {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["starting"] = "начальная",
                    ["goal"] = "конечная",
                },
            },

            // Cream Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Cream Cipher?
            [Question.CreamCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Creation
            // What were the weather conditions on the {1} day in {0}?
            // What were the weather conditions on the first day in Creation?
            [Question.CreationWeather] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая погода была на {1}-м дне {0}?",
                ModuleName = "Творения",
                Answers = new Dictionary<string, string>
                {
                    ["Clear"] = "Ясно",
                    ["Heat Wave"] = "Жара",
                    ["Meteor Shower"] = "Метеор. дождь",
                    ["Rain"] = "Дождь",
                    ["Windy"] = "Ветер",
                },
            },

            // Crimson Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Crimson Cipher?
            [Question.CrimsonCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Critters
            // What was the color in {0}?
            // What was the color in Critters?
            [Question.CrittersColor] = new()
            {
                QuestionText = "Какой цвет использовался {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Yellow"] = "Жёлтый",
                    ["Pink"] = "Розовый",
                    ["Blue"] = "Синий",
                    ["White"] = "Белый",
                },
            },

            // Cruel Binary
            // What was the displayed word in {0}?
            // What was the displayed word in Cruel Binary?
            [Question.CruelBinaryDisplayedWord] = new()
            {
                QuestionText = "Какое слово было показано {0}?",
            },

            // Cruel Keypads
            // Which of these characters appeared in the {1} stage of {0}?
            // Which of these characters appeared in the first stage of Cruel Keypads?
            [Question.CruelKeypadsDisplayedSymbols] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой из этих символов появился на {1}-м этапе {0}?",
            },
            // What was the color of the bar in the {1} stage of {0}?
            // What was the color of the bar in the first stage of Cruel Keypads?
            [Question.CruelKeypadsColors] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была шкала на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Blue"] = "Blue",
                    ["Yellow"] = "Yellow",
                    ["Green"] = "Green",
                    ["Magenta"] = "Magenta",
                    ["White"] = "White",
                },
            },

            // The cRule
            // Which cell was pre-filled at the start of {0}?
            // Which cell was pre-filled at the start of The cRule?
            [Question.CRulePrefilled] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая клетка была уже заполнена в начале {0}?",
            },
            // Which symbol pair was here in {0}?
            // Which symbol pair was here in The cRule?
            [Question.CRuleSymbolPair] = new()
            {
                QuestionText = "Какая пара символов была здесь {0}?",
            },
            // Which symbol pair was present on {0}?
            // Which symbol pair was present on The cRule?
            [Question.CRuleSymbolPairPresent] = new()
            {
                QuestionText = "Какая пара символов присутствовала {0}?",
            },
            // Where was {1} in {0}?
            // Where was ♤♤ in The cRule?
            [Question.CRuleSymbolPairCell] = new()
            {
                QuestionText = "Где находилось {1} {0}?",
            },

            // Cryptic Cycle
            // Which direction was the {1} dial pointing in {0}?
            // Which direction was the first dial pointing in Cryptic Cycle?
            [Question.CrypticCycleDialDirections] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which direction was the {1} dial pointing in {0}?",
            },
            // What letter was written on the {1} dial in {0}?
            // What letter was written on the first dial in Cryptic Cycle?
            [Question.CrypticCycleDialLabels] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What letter was written on the {1} dial in {0}?",
            },

            // Cryptic Keypad
            // What was the label of the {1} key in {0}?
            // What was the label of the top-left key in Cryptic Keypad?
            [Question.CrypticKeypadLabels] = new()
            {
                QuestionText = "Какой был символ на {1} кнопке {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхней левой",
                    ["top-right"] = "верхней правой",
                    ["bottom-left"] = "нижней левой",
                    ["bottom-right"] = "нижней правой",
                },
            },
            // Which cardinal direction was the {1} key rotated to in {0}?
            // Which cardinal direction was the top-left key rotated to in Cryptic Keypad?
            [Question.CrypticKeypadRotations] = new()
            {
                QuestionText = "В какую сторону света была повёрнута {1} кнопка {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхняя левая",
                    ["top-right"] = "верхняя правая",
                    ["bottom-left"] = "нижняя левая",
                    ["bottom-right"] = "нижняя правая",
                },
                Answers = new Dictionary<string, string>
                {
                    ["North"] = "Север",
                    ["East"] = "Восток",
                    ["South"] = "Юг",
                    ["West"] = "Запад",
                },
            },

            // The Cube
            // What was the {1} cube rotation in {0}?
            // What was the first cube rotation in The Cube?
            [Question.CubeRotations] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое было {1}-е вращение у {0}?",
                ModuleName = "Куба",
                Answers = new Dictionary<string, string>
                {
                    ["rotate cw"] = "поворот по часовой",
                    ["tip left"] = "наклон влево",
                    ["tip backwards"] = "наклон назад",
                    ["rotate ccw"] = "поворот против часовой",
                    ["tip right"] = "наклон вправо",
                    ["tip forwards"] = "наклон вперёд",
                },
            },

            // Cursed Double-Oh
            // What was the first digit of the initially displayed number in {0}?
            // What was the first digit of the initially displayed number in Cursed Double-Oh?
            [Question.CursedDoubleOhInitialPosition] = new()
            {
                QuestionText = "Какая была первая цифра изначально отображённого числа {0}?",
            },

            // Customer Identification
            // Who was the {1} customer in {0}?
            // Who was the first customer in Customer Identification?
            [Question.CustomerIdentificationCustomer] = new()
            {
                QuestionText = "Кто был {1}-м посетителем {0}?",
            },

            // The Cyan Button
            // Where was the button at the {1} stage in {0}?
            // Where was the button at the first stage in The Cyan Button?
            [Question.CyanButtonPositions] = new()
            {
                Conjugation = Conjugation.NominativeMasculine,
                QuestionText = "Где был {0} на своём {1}-м этапе?",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "Сверху слева",
                    ["top middle"] = "Сверху посередине",
                    ["top right"] = "Сверху справа",
                    ["bottom left"] = "Снизу слева",
                    ["bottom middle"] = "Снизу посередине",
                    ["bottom right"] = "Снизу справа",
                },
            },

            // DACH Maze
            // Which region did you depart from in {0}?
            // Which region did you depart from in DACH Maze?
            [Question.DACHMazeOrigin] = new()
            {
                QuestionText = "Откуда вы отправились {0}?",
                Answers = new Dictionary<string, string>
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

            // Deaf Alley
            // What was the shape generated in {0}?
            // What was the shape generated in Deaf Alley?
            [Question.DeafAlleyShape] = new()
            {
                QuestionText = "Какой символ был сгенерирован {0}?",
            },

            // The Deck of Many Things
            // What deck did the first card of {0} belong to?
            // What deck did the first card of The Deck of Many Things belong to?
            [Question.DeckOfManyThingsFirstCard] = new()
            {
                QuestionText = "Какой колоде принадлежала первая карта {0}?",
            },

            // Decolored Squares
            // What was the starting {1} defining color in {0}?
            // What was the starting column defining color in Decolored Squares?
            [Question.DecoloredSquaresStartingPos] = new()
            {
                Conjugation = Conjugation.PrepositivePlural,
                QuestionText = "Какой цвет определил {1} схемы на {0}?",
                ModuleName = "Обесцвеченных квадратах",
                FormatArgs = new Dictionary<string, string>
                {
                    ["column"] = "начальный столбец",
                    ["row"] = "начальную строку",
                },
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Белый",
                    ["Red"] = "Красный",
                    ["Blue"] = "Синий",
                    ["Green"] = "Зелёный",
                    ["Yellow"] = "Жёлтый",
                    ["Magenta"] = "Розовый",
                },
            },

            // Decolour Flash
            // What was the {1} of the {2} goal in {0}?
            // What was the colour of the first goal in Decolour Flash?
            [Question.DecolourFlashGoal] = new()
            {
                QuestionText = "{1} у {2}-й цели {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["colour"] = "Какой был цвет",
                    ["word"] = "Какое было слово",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blue",
                    ["Green"] = "Green",
                    ["Red"] = "Red",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                    ["White"] = "White",
                },
            },

            // Denial Displays
            // What number was initially shown on display {1} in {0}?
            // What number was initially shown on display A in Denial Displays?
            [Question.DenialDisplaysDisplays] = new()
            {
                QuestionText = "Какое число было показано на экране {1} {0}?",
            },

            // DetoNATO
            // What was the {1} display in {0}?
            // What was the first display in DetoNATO?
            [Question.DetoNATODisplay] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Что было на дисплее на {1}-м этапе {0}?",
            },

            // Devilish Eggs
            // What was the {1} egg’s {2} rotation in {0}?
            // What was the top egg’s first rotation in Devilish Eggs?
            [Question.DevilishEggsRotations] = new()
            {
                QuestionText = "Какой был {2}-й поворот у {1} яйца {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнего",
                    ["bottom"] = "нижнего",
                },
            },
            // What was the {1} digit in the string of numbers on {0}?
            // What was the first digit in the string of numbers on Devilish Eggs?
            [Question.DevilishEggsNumbers] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая была {1}-я цифра в строке чисел на {0}?",
            },
            // What was the {1} letter in the string of letters on {0}?
            // What was the first letter in the string of letters on Devilish Eggs?
            [Question.DevilishEggsLetters] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая была {1}-я буква в строке букв на {0}?",
            },

            // Dialtones
            // What dialtones were heard in {0}?
            // What dialtones were heard in Dialtones?
            [Question.DialtonesDialtones] = new()
            {
                QuestionText = "Какие тональные сигналы играли {0}?",
            },

            // Digisibility
            // What was the number on the {1} button in {0}?
            // What was the number on the first button in Digisibility?
            [Question.DigisibilityDisplayedNumber] = new()
            {
                QuestionText = "Какое число было на {1}-й кнопке {0}?",
            },

            // Digit String
            // What was the initial number in {0}?
            // What was the initial number in Digit String?
            [Question.DigitStringInitialNumber] = new()
            {
                QuestionText = "Какое было исходное число {0}?",
            },

            // Dimension Disruption
            // Which of these was a visible character in {0}?
            // Which of these was a visible character in Dimension Disruption?
            [Question.DimensionDisruptionVisibleLetters] = new()
            {
                QuestionText = "Что из этого было видимым символом {0}?",
            },

            // Directional Button
            // How many times did you press the button in the {1} stage of {0}?
            // How many times did you press the button in the first stage of Directional Button?
            [Question.DirectionalButtonButtonCount] = new()
            {
                Conjugation = Conjugation.GenitiveFeminine,
                QuestionText = "Сколько раз вы нажали кнопку на {1}-м этапе {0}?",
                ModuleName = "Направляющей кнопки",
            },

            // Discolored Squares
            // What was {1}’s remembered position in {0}?
            // What was Blue’s remembered position in Discolored Squares?
            [Question.DiscoloredSquaresRememberedPositions] = new()
            {
                Conjugation = Conjugation.PrepositivePlural,
                QuestionText = "В какой позиции находился {1} квадрат в самом начале на {0}?",
                ModuleName = "Бесцветных квадратах",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Blue"] = "синий",
                    ["Red"] = "красный",
                    ["Yellow"] = "жёлтый",
                    ["Green"] = "зелёный",
                    ["Magenta"] = "розовый",
                },
            },

            // Disordered Keys
            // What was the missing information for the {1} key in {0}?
            // What was the missing information for the first key in Disordered Keys?
            [Question.DisorderedKeysMissingInfo] = new()
            {
                QuestionText = "Какой информации недоставало {1}-й клавише {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Key color"] = "Цвета клавиши",
                    ["Label color"] = "Цвета надписи",
                    ["Label"] = "Надписи",
                },
            },
            // What was the revealed key color for the {1} key in {0}?
            // What was the revealed key color for the first key in Disordered Keys?
            [Question.DisorderedKeysRevealedKeyColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Каким был раскрытый цвет {1}-й клавиши {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                },
            },
            // What was the revealed label for the {1} key in {0}?
            // What was the revealed label for the first key in Disordered Keys?
            [Question.DisorderedKeysRevealedLabel] = new()
            {
                QuestionText = "Какая была раскрытая надпись {1}-й клавиши {0}?",
            },
            // What was the revealed label color for the {1} key in {0}?
            // What was the revealed label color for the first key in Disordered Keys?
            [Question.DisorderedKeysRevealedLabelColor] = new()
            {
                QuestionText = "Каким был раскрытый цвет надписи {1}-й клавиши {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                },
            },
            // What was the unrevealed key color for the {1} key in {0}?
            // What was the unrevealed key color for the first key in Disordered Keys?
            [Question.DisorderedKeysUnrevealedKeyColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Каким был нераскрытый цвет {1}-й клавиши {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                },
            },
            // What was the unrevealed label for the {1} key in {0}?
            // What was the unrevealed label for the first key in Disordered Keys?
            [Question.DisorderedKeysUnrevealedKeyLabel] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была нераскрытая надпись {1}-й клавиши {0}?",
            },
            // What was the unrevealed label color for the {1} key in {0}?
            // What was the unrevealed label color for the first key in Disordered Keys?
            [Question.DisorderedKeysUnrevealedLabelColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Каким был нераскрытый цвет надписи {1}-й клавиши {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                },
            },

            // Divisible Numbers
            // What was the {1} stage’s number in {0}?
            // What was the first stage’s number in Divisible Numbers?
            [Question.DivisibleNumbersNumbers] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое было число {1}-го этапа {0}?",
            },

            // Doofenshmirtz Evil Inc.
            // What jingle played in {0}?
            // What jingle played in Doofenshmirtz Evil Inc.?
            [Question.DoofenshmirtzEvilIncJingles] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What jingle played in {0}?",
            },
            // Which image was shown in {0}?
            // Which image was shown in Doofenshmirtz Evil Inc.?
            [Question.DoofenshmirtzEvilIncInators] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which image was shown in {0}?",
            },

            // Double Arrows
            // What was the starting position in {0}?
            // What was the starting position in Double Arrows?
            [Question.DoubleArrowsStart] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какая была начальная позиция {0}?",
                ModuleName = "Двойных стрелках",
            },
            // Which {1} arrow moved {2} in the grid in {0}?
            // Which inner arrow moved up in the grid in Double Arrows?
            [Question.DoubleArrowsArrow] = new()
            {
                QuestionText = "Которая стрелка {1} переместила вас {2} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["inner"] = "внутри",
                    ["up"] = "вверх",
                    ["outer"] = "снаружи",
                    ["down"] = "вниз",
                    ["left"] = "влево",
                    ["right"] = "вправо",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Вверх",
                    ["Right"] = "Вправо",
                    ["Left"] = "Влево",
                    ["Down"] = "Вниз",
                },
            },
            // Which direction in the grid did the {1} arrow move in {0}?
            // Which direction in the grid did the inner up arrow move in Double Arrows?
            [Question.DoubleArrowsMovement] = new()
            {
                QuestionText = "В какую сторону вас переместила стрелка {1} {0}?",
                FormatArgs = new Dictionary<string, string>
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
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Вверх",
                    ["Right"] = "Вправо",
                    ["Left"] = "Влево",
                    ["Down"] = "Вниз",
                },
            },

            // Double Color
            // What was the screen color on the {1} stage of {0}?
            // What was the screen color on the first stage of Double Color?
            [Question.DoubleColorColors] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета был экран на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "Зелёного",
                    ["Blue"] = "Синего",
                    ["Red"] = "Красного",
                    ["Pink"] = "Розового",
                    ["Yellow"] = "Жёлтого",
                },
            },

            // Double Digits
            // What was the digit on the {1} display in {0}?
            // What was the digit on the left display in Double Digits?
            [Question.DoubleDigitsDisplays] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая цифра была на {1} дисплее {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левом",
                    ["right"] = "правом",
                },
            },

            // Double Expert
            // What was the starting key number in {0}?
            // What was the starting key number in Double Expert?
            [Question.DoubleExpertStartingKeyNumber] = new()
            {
                QuestionText = "Какое было начальное ключевое число {0}?",
            },
            // What was the word you submitted in {0}?
            // What was the word you submitted in Double Expert?
            [Question.DoubleExpertSubmittedWord] = new()
            {
                QuestionText = "Какое было отправленное слово {0}?",
            },

            // Double Listening
            // What clip was played in {0}?
            // What clip was played in Double Listening?
            [Question.DoubleListeningSounds] = new()
            {
                QuestionText = "Какой звук был воспроизведён {0}?",
            },

            // Double-Oh
            // Which button was the submit button in {0}?
            // Which button was the submit button in Double-Oh?
            [Question.DoubleOhSubmitButton] = new()
            {
                QuestionText = "Какая кнопка была кнопкой отправки {0}?",
                ModuleName = "Агент Ноль-ноль",
            },

            // Double Screen
            // What color was the {1} screen in the {2} stage of {0}?
            // What color was the top screen in the first stage of Double Screen?
            [Question.DoubleScreenColors] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета был {1} экран на {2}-м этапе {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхний",
                    ["bottom"] = "нижний",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Yellow"] = "Жёлтый",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                },
            },

            // Dr. Doctor
            // Which of these symptoms was listed on {0}?
            // Which of these symptoms was listed on Dr. Doctor?
            [Question.DrDoctorSymptoms] = new()
            {
                QuestionText = "Какой из этих симптомов присутствовал {0}?",
            },
            // Which of these diseases was listed on {0}, but not the one treated?
            // Which of these diseases was listed on Dr. Doctor, but not the one treated?
            [Question.DrDoctorDiseases] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая из этих болезней присутствовала на {0}, но не была вылечена?",
            },

            // Dreamcipher
            // What was the decrypted word in {0}?
            // What was the decrypted word in Dreamcipher?
            [Question.DreamcipherWord] = new()
            {
                QuestionText = "Какое было расшифрованное слово {0}?",
            },

            // The Duck
            // What was the color of the curtain in {0}?
            // What was the color of the curtain in The Duck?
            [Question.DuckCurtainColor] = new()
            {
                QuestionText = "Какого цвета был занавески {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "синий",
                    ["yellow"] = "жёлтый",
                    ["green"] = "зелёный",
                    ["orange"] = "оранжевый",
                    ["red"] = "красный",
                },
            },

            // Dumb Waiters
            // Which player {1} present in {0}?
            // Which player was present in Dumb Waiters?
            [Question.DumbWaitersPlayerAvailable] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой игрок {1} на {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "присутствовал",
                    ["was not"] = "отсутствовал",
                },
            },

            // Earthbound
            // What was the background in {0}?
            // What was the background in Earthbound?
            [Question.EarthboundBackground] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой был фон на {0}?",
            },
            // Which monster was displayed in {0}?
            // Which monster was displayed in Earthbound?
            [Question.EarthboundMonster] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой монстр был показан на {0}?",
            },

            // eeB gnillepS
            // What word was asked to be spelled in {0}?
            // What word was asked to be spelled in eeB gnillepS?
            [Question.eeBgnillepSWord] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое слово нужно было прописать на {0}?",
            },

            // Eight
            // What was the last digit on the small display in {0}?
            // What was the last digit on the small display in Eight?
            [Question.EightLastSmallDisplayDigit] = new()
            {
                QuestionText = "Какая была последняя цифра на малом экране {0}?",
            },
            // What was the position of the last broken digit in {0}?
            // What was the position of the last broken digit in Eight?
            [Question.EightLastBrokenDigitPosition] = new()
            {
                QuestionText = "Какая была позиция последней сломанной цифры {0}?",
            },
            // What were the last resulting digits in {0}?
            // What were the last resulting digits in Eight?
            [Question.EightLastResultingDigits] = new()
            {
                QuestionText = "Какие были посление цифры перед отправкой {0}?",
            },
            // What was the last displayed number in {0}?
            // What was the last displayed number in Eight?
            [Question.EightLastDisplayedNumber] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какие были последние отображённые цифры на {0}?",
            },

            // Elder Futhark
            // What was the {1} rune shown on {0}?
            // What was the first rune shown on Elder Futhark?
            [Question.ElderFutharkRunes] = new()
            {
                QuestionText = "Какая была {1}-я показанная руна {0}?",
            },

            // Emoji
            // What was the {1} emoji in {0}?
            // What was the left emoji in Emoji?
            [Question.EmojiEmoji] = new()
            {
                QuestionText = "Какой был {1} эмодзи {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левый",
                    ["right"] = "правый",
                },
            },

            // ENA Cipher
            // What was the {1} keyword in {0}?
            // What was the first keyword in ENA Cipher?
            [Question.EnaCipherKeywordAnswer] = new()
            {
                QuestionText = "Какое было {1}-е ключевое слово {0}?",
            },
            // What was the transposition key in {0}?
            // What was the transposition key in ENA Cipher?
            [Question.EnaCipherExtAnswer] = new()
            {
                QuestionText = "Какой был ключ перестановки {0}?",
            },
            // What was the encrypted word in {0}?
            // What was the encrypted word in ENA Cipher?
            [Question.EnaCipherEncryptedAnswer] = new()
            {
                QuestionText = "Какое слово было зашифрованно {0}",
            },

            // Encrypted Dice
            // Which of these numbers appeared on a die in the {1} stage of {0}?
            // Which of these numbers appeared on a die in the first stage of Encrypted Dice?
            [Question.EncryptedDice] = new()
            {
                QuestionText = "Какие из этих чисел были на костях на {1}-м этапе {0}?",
            },

            // Encrypted Equations
            // Which shape was the {1} operand in {0}?
            // Which shape was the first operand in Encrypted Equations?
            [Question.EncryptedEquationsShapes] = new()
            {
                QuestionText = "Какая фигура была {1}-й переменной {0}?",
            },

            // Encrypted Hangman
            // What method of encryption was used by {0}?
            // What method of encryption was used by Encrypted Hangman?
            [Question.EncryptedHangmanEncryptionMethod] = new()
            {
                QuestionText = "Какой метод шифрования был применён {0}?",
                Answers = new Dictionary<string, string>
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
            // What module name was encrypted by {0}?
            // What module name was encrypted by Encrypted Hangman?
            [Question.EncryptedHangmanModule] = new()
            {
                QuestionText = "Какое название модуля было зашифрованно {0}?",
            },

            // Encrypted Maze
            // Which symbol on {0} was spinning {1}?
            // Which symbol on Encrypted Maze was spinning clockwise?
            [Question.EncryptedMazeSymbols] = new()
            {
                QuestionText = "Какой символ {0} крутился {1}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["clockwise"] = "по часовой",
                    ["counter-clockwise"] = "против часовой",
                },
            },

            // Encrypted Morse
            // What was the {1} on {0}?
            // What was the received call on Encrypted Morse?
            [Question.EncryptedMorseCallResponse] = new()
            {
                QuestionText = "Какое было {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["received call"] = "полученное сообщение",
                    ["sent response"] = "отправленное сообщение",
                },
            },

            // Encryption Bingo
            // What was the first encoding used in {0}?
            // What was the first encoding used in Encryption Bingo?
            [Question.EncryptionBingoEncoding] = new()
            {
                QuestionText = "Какая шифровка была первой {0}?",
                Answers = new Dictionary<string, string>
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

            // Enigma Cycle
            // What was the {1} in {0}?
            // What was the message in Enigma Cycle?
            [Question.EnigmaCycleWords] = new()
            {
                QuestionText = "{1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Какое было сообщение",
                    ["response"] = "Какой был ответ",
                },
            },

            // English Entries
            // What was the displayed quote on {0}?
            // What was the displayed quote on English Entries?
            [Question.EnglishEntriesDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed quote on {0}?",
            },

            // Entry Number Four
            // What was the {1} digit in the {2} number shown in {0}?
            // What was the first digit in the first number shown in Entry Number Four?
            [Question.EntryNumberFourDigits] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} digit in the {2} number shown in {0}?",
            },

            // Entry Number One
            // What was the {1} digit in the {2} number shown in {0}?
            // What was the first digit in the first number shown in Entry Number One?
            [Question.EntryNumberOneDigits] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the{1} digit in the {2} number shown in {0}?",
            },

            // Épelle-moi Ça
            // What word was asked to be spelled in {0}?
            // What word was asked to be spelled in Épelle-moi Ça?
            [Question.ÉpelleMoiÇaWord] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое слово нужно было написать на {0}?",
            },

            // Equations X
            // What was the displayed symbol in {0}?
            // What was the displayed symbol in Equations X?
            [Question.EquationsXSymbols] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой символ был показан на {0}?",
            },

            // Error Codes
            // What was the active error code in {0}?
            // What was the active error code in Error Codes?
            [Question.ErrorCodesActiveError] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой код был активным на {0}?",
            },

            // Etterna
            // What was the beat for the {1} arrow from the bottom in {0}?
            // What was the beat for the first arrow from the bottom in Etterna?
            [Question.EtternaNumber] = new()
            {
                QuestionText = "Какой бит был у {1}-й стрелки снизу вверх {0}?",
            },

            // Exoplanets
            // What was the starting target planet in {0}?
            // What was the starting target planet in Exoplanets?
            [Question.ExoplanetsStartingTargetPlanet] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была начальная целевая планета из {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["outer"] = "Внешняя",
                    ["middle"] = "Средняя",
                    ["inner"] = "Внутреняя",
                    ["none"] = "Никакая",
                },
            },
            // What was the starting target digit in {0}?
            // What was the starting target digit in Exoplanets?
            [Question.ExoplanetsStartingTargetDigit] = new()
            {
                QuestionText = "Какая была начальная целевая цифра {0}?",
            },
            // What was the final target planet in {0}?
            // What was the final target planet in Exoplanets?
            [Question.ExoplanetsTargetPlanet] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была финальная целевая планета из {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["outer"] = "Внешняя",
                    ["middle"] = "Средняя",
                    ["inner"] = "Внутреняя",
                    ["none"] = "Никакая",
                },
            },
            // What was the final target digit in {0}?
            // What was the final target digit in Exoplanets?
            [Question.ExoplanetsTargetDigit] = new()
            {
                QuestionText = "Какая была финальная целевая цифра {0}?",
            },

            // Factoring Maze
            // What was one of the prime numbers chosen in {0}?
            // What was one of the prime numbers chosen in Factoring Maze?
            [Question.FactoringMazeChosenPrimes] = new()
            {
                QuestionText = "Какое из простых чисел было выбрано {0}?",
            },

            // Factory Maze
            // What room did you start in in {0}?
            // What room did you start in in Factory Maze?
            [Question.FactoryMazeStartRoom] = new()
            {
                QuestionText = "Какая была начальная комната {0}?",
            },

            // Faerie Fires
            // What pitch did the {1} faerie sing in {0}?
            // What pitch did the first faerie sing in Faerie Fires?
            [Question.FaerieFiresPitchOrdinal] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What pitch did the {1} faerie sing in {0}?",
            },
            // What pitch did the {1} faerie sing in {0}?
            // What pitch did the red faerie sing in Faerie Fires?
            [Question.FaerieFiresPitchColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What pitch did the {1} faerie sing in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                    ["yellow"] = "yellow",
                    ["cyan"] = "cyan",
                    ["magenta"] = "magenta",
                },
            },
            // What color was the {1} faerie in {0}?
            // What color was the first faerie in Faerie Fires?
            [Question.FaerieFiresColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} faerie in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Yellow"] = "Yellow",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                },
            },

            // Fast Math
            // What was the last pair of letters in {0}?
            // What was the last pair of letters in Fast Math?
            [Question.FastMathLastLetters] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какая пара букв была последней {0}?",
                ModuleName = "Быстрой математике",
            },

            // Fast Playfair Cipher
            // What was the last displayed message in {0}?
            // What was the last displayed message in Fast Playfair Cipher?
            [Question.FastPlayfairCipherLastMessage] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the last displayed message in {0}?",
            },

            // Faulty Buttons
            // Which button referred to the {1} button in reading order in {0}?
            // Which button referred to the first button in reading order in Faulty Buttons?
            [Question.FaultyButtonsReferredToThisButton] = new()
            {
                QuestionText = "Какая кнопка ссылалась на {1}-ю кнопку в порядке чтения {0}?",
            },
            // Which button did the {1} button in reading order refer to in {0}?
            // Which button did the first button in reading order refer to in Faulty Buttons?
            [Question.FaultyButtonsThisButtonReferredTo] = new()
            {
                QuestionText = "На какую кнопку ссылалась {1}-я кнопка в порядке чтения {0}?",
            },

            // Faulty RGB Maze
            // What was the exit coordinate in {0}?
            // What was the exit coordinate in Faulty RGB Maze?
            [Question.FaultyRGBMazeExit] = new()
            {
                QuestionText = "На каких координатах был выход {0}?",
            },
            // Where was the {1} key in {0}?
            // Where was the red key in Faulty RGB Maze?
            [Question.FaultyRGBMazeKeys] = new()
            {
                QuestionText = "Где был {1} ключ {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красный",
                    ["green"] = "зелёный",
                    ["blue"] = "синий",
                },
            },
            // Which maze number was the {1} maze in {0}?
            // Which maze number was the red maze in Faulty RGB Maze?
            [Question.FaultyRGBMazeNumber] = new()
            {
                QuestionText = "Какой {1} лабиринт был {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красный",
                    ["green"] = "зелёный",
                    ["blue"] = "синий",
                },
            },

            // Find The Date
            // What was the day displayed in the {1} stage of {0}?
            // What was the day displayed in the first stage of Find The Date?
            [Question.FindTheDateDay] = new()
            {
                QuestionText = "Какой день был показан на {1}-м этапе {0}?",
            },
            // What was the month displayed in the {1} stage of {0}?
            // What was the month displayed in the first stage of Find The Date?
            [Question.FindTheDateMonth] = new()
            {
                QuestionText = "Какой месяц был показан на {1}-м этапе {0}?",
            },
            // What was the year displayed in the {1} stage of {0}?
            // What was the year displayed in the first stage of Find The Date?
            [Question.FindTheDateYear] = new()
            {
                QuestionText = "Какой год был показан на {1}-м этапе {0}?",
            },

            // Five Letter Words
            // Which of these words was on the display in {0}?
            // Which of these words was on the display in Five Letter Words?
            [Question.FiveLetterWordsDisplayedWords] = new()
            {
                QuestionText = "Какое из этих слов было на экране {0}?",
            },

            // FizzBuzz
            // What was the {1} digit on the {2} display of {0}?
            // What was the first digit on the top display of FizzBuzz?
            [Question.FizzBuzzDisplayedNumbers] = new()
            {
                QuestionText = "Какая была {1}-я цифра на {2} экране {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "среднем",
                    ["bottom"] = "нижнем",
                },
            },

            // Flags
            // What was the displayed number in {0}?
            // What was the displayed number in Flags?
            [Question.FlagsDisplayedNumber] = new()
            {
                QuestionText = "Какое число было показано на экране {0}?",
            },
            // What was the main country flag in {0}?
            // What was the main country flag in Flags?
            [Question.FlagsMainCountry] = new()
            {
                QuestionText = "Какой главный флаг отображался {0}?",
            },
            // Which of these country flags was shown, but not the main country flag, in {0}?
            // Which of these country flags was shown, but not the main country flag, in Flags?
            [Question.FlagsCountries] = new()
            {
                QuestionText = "Какой из этих флагов был показан (но не являлся главным) {0}?",
            },

            // Flashing Arrows
            // What number was displayed on {0}?
            // What number was displayed on Flashing Arrows?
            [Question.FlashingArrowsDisplayedValue] = new()
            {
                QuestionText = "Какое число было показано {0}?",
            },
            // What color flashed {1} black on the relevant arrow in {0}?
            // What color flashed before black on the relevant arrow in Flashing Arrows?
            [Question.FlashingArrowsReferredArrow] = new()
            {
                QuestionText = "Какой цвет мигнул {1} на соответствующей стрелке {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["before"] = "перед чёрным",
                    ["after"] = "после чёрного",
                },
                Answers = new Dictionary<string, string>
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

            // Flashing Lights
            // How many times did the {1} LED flash {2} on {0}?
            // How many times did the top LED flash cyan on Flashing Lights?
            [Question.FlashingLightsLEDFrequency] = new()
            {
                QuestionText = "Сколько раз {1} светодиод мигал {2} {0}?",
                FormatArgs = new Dictionary<string, string>
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

            // Flavor Text
            // Which module’s flavor text was shown in {0}?
            // Which module’s flavor text was shown in Flavor Text?
            [Question.FlavorTextModule] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "К какому модулю был показан флейвор текст на {0}?",
            },

            // Flavor Text EX
            // Which module’s flavor text was shown in the {1} stage of {0}?
            // Which module’s flavor text was shown in the first stage of Flavor Text EX?
            [Question.FlavorTextEXModule] = new()
            {
                QuestionText = "К какому модулю был показан флейвор текст на {1}-м этапе {0}?",
            },

            // Flyswatting
            // Which fly was present, but not in the solution in {0}?
            // Which fly was present, but not in the solution in Flyswatting?
            [Question.FlyswattingUnpressed] = new()
            {
                QuestionText = "Какая муха присутствовала, но не была частью решения {0}?",
            },

            // Follow Me
            // What was the {1} flashing direction in {0}?
            // What was the first flashing direction in Follow Me?
            [Question.FollowMeDisplayedPath] = new()
            {
                QuestionText = "Какое было {1}-е мигающее направление {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Вверх",
                    ["Down"] = "Вниз",
                    ["Left"] = "Влево",
                    ["Right"] = "Вправо",
                },
            },

            // Forest Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Forest Cipher?
            [Question.ForestCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Forget Any Color
            // What colors were the cylinders during the {1} stage of {0}?
            // What colors were the cylinders during the first stage of Forget Any Color?
            [Question.ForgetAnyColorCylinder] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какие были цилиндры на {1}-м этапе {0}?",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
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
                    ["the Forget Any Color which used figure {0} in the {1} stage"] = "Forget Any Color, где на {1}-м этапе была применена строка {0}",
                    ["the Forget Any Color whose cylinders in the {1} stage were {0}"] = "Forget Any Color, где цвета цилиндров на {1}-м этапе были: {0}",
                },
            },
            // Which figure was used during the {1} stage of {0}?
            // Which figure was used during the first stage of Forget Any Color?
            [Question.ForgetAnyColorSequence] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая строка была применена на {1}-м этапе {0}?",
            },

            // Forget Everything
            // What was the {1} displayed digit in the first stage of {0}?
            // What was the first displayed digit in the first stage of Forget Everything?
            [Question.ForgetEverythingStageOneDisplay] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была {1}-я отображённая цифра на первом этапе {0}?",
                ModuleName = "Полного забвения",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the Forget Everything whose {0} displayed digit in that stage was {1}"] = "Полного забвения, {0}-я отображённая цифра которого на том этапе была {1}",
                },
            },

            // Forget Me
            // What number was in the {1} position of the initial puzzle in {0}?
            // What number was in the top-left position of the initial puzzle in Forget Me?
            [Question.ForgetMeInitialState] = new()
            {
                QuestionText = "Какое число было на {1} кнопке изначального пазла {0}?",
                FormatArgs = new Dictionary<string, string>
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

            // Forget Me Not
            // What was the digit displayed in the {1} stage of {0}?
            // What was the digit displayed in the first stage of Forget Me Not?
            [Question.ForgetMeNotDisplayedDigits] = new()
            {
                Conjugation = Conjugation.GenitiveFeminine,
                QuestionText = "Какая цифра была отображена на {1}-м этапе {0}?",
                ModuleName = "Незабудки",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the Forget Me Not which displayed a {0} in the {1} stage"] = "Незабудки, на которой была отображена {0} на {1}-м этапе",
                },
            },

            // Forget Me Now
            // What was the {1} displayed digit in {0}?
            // What was the first displayed digit in Forget Me Now?
            [Question.ForgetMeNowDisplayedDigits] = new()
            {
                Conjugation = Conjugation.PrepositiveFeminine,
                QuestionText = "Какая была {1}-я отображённая цифра на {0}?",
                ModuleName = "Забудке",
            },

            // Forget’s Ultimate Showdown
            // What was the {1} digit of the answer in {0}?
            // What was the first digit of the answer in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownAnswer] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какая была {1}-я цифра финального кода {0}?",
                ModuleName = "Финальной битве забвения",
            },
            // What was the {1} digit of the initial number in {0}?
            // What was the first digit of the initial number in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownInitial] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какая была {1}-я цифра изначального кодового числа {0}?",
                ModuleName = "Финальной битве забвения",
            },
            // What was the {1} digit of the bottom number in {0}?
            // What was the first digit of the bottom number in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownBottom] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какая была {1}-я цифра нижнего числа {0}?",
                ModuleName = "Финальной битве забвения",
            },
            // What was the {1} method used in {0}?
            // What was the first method used in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownMethod] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какой был {1}-й использованный метод {0}?",
                ModuleName = "Финальной битве забвения",
                Answers = new Dictionary<string, string>
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

            // Forget The Colors
            // What number was on the gear during stage {1} of {0}?
            // What number was on the gear during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsGearNumber] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое число было на шестерёнке на {1}-м этапе {0}?",
                ModuleName = "\"Забудь цвета\"",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the Forget The Colors whose gear number was {0} in stage {1}"] = "\"Забудь цвета\", где на {1}-м этапе на шестерёнке было {0}",
                    ["the Forget The Colors which had {0} on its large display in stage {1}"] = "\"Забудь цвета\", где на {1}-м этапе на большом экране было {0}",
                    ["the Forget The Colors whose received sine number in stage {1} ended with a {0}"] = "\"Забудь цвета\", где на {1}-м этапе полученное число синуса оканчивалось на {0}",
                    ["the Forget The Colors whose gear color was {0} in stage {1}"] = "\"Забудь цвета\", где на {1}-м этапе цвет шестерёнки был {0}",
                    ["the Forget The Colors whose rule color was {0} in stage {1}"] = "\"Забудь цвета\", где на {1}-м этапе цвет правила был {0}",
                },
            },
            // What number was on the large display during stage {1} of {0}?
            // What number was on the large display during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsLargeDisplay] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое число было на большом экране на {1}-м этапе {0}?",
                ModuleName = "\"Забудь цвета\"",
            },
            // What was the last decimal in the sine number received during stage {1} of {0}?
            // What was the last decimal in the sine number received during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsSineNumber] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была последняя дробная цифра полученного числа синуса на {1}-м этапе {0}?",
                ModuleName = "\"Забудь цвета\"",
            },
            // What color was the gear during stage {1} of {0}?
            // What color was the gear during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsGearColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была шестерёнка на {1}-м этапе {0}?",
                ModuleName = "\"Забудь цвета\"",
                Answers = new Dictionary<string, string>
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
            // Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?
            // Which edgework-based rule was applied to the sum of nixies and gear during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsRuleColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета было правило, по которому вы сложили числа на лампах и шестерёнках на {1}-м этапе {0}?",
                ModuleName = "\"Забудь цвета\"",
                Answers = new Dictionary<string, string>
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

            // Forget This
            // What color was the LED in the {1} stage of {0}?
            // What color was the LED in the first stage of Forget This?
            [Question.ForgetThisColors] = new()
            {
                QuestionText = "Какого цвета был светодиод на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                    ["Black"] = "Black",
                    ["White"] = "White",
                    ["Green"] = "Green",
                },
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the Forget This whose LED was {0} in the {1} stage"] = "Forget This, на котором был {0} светодиод на {1}-м этапе",
                    ["the Forget This which displayed {0} in the {1} stage"] = "Forget This, который показывал {0} на {1}-м этапе",
                },
            },
            // What was the digit displayed in the {1} stage of {0}?
            // What was the digit displayed in the first stage of Forget This?
            [Question.ForgetThisDigits] = new()
            {
                QuestionText = "Какая цифра была показана на {1}-м этапе {0}?",
            },

            // Free Parking
            // What was the player token in {0}?
            // What was the player token in Free Parking?
            [Question.FreeParkingToken] = new()
            {
                QuestionText = "Какой был жетон игрока {0}?",
                Answers = new Dictionary<string, string>
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

            // Functions
            // What was the last digit of your first query’s result in {0}?
            // What was the last digit of your first query’s result in Functions?
            [Question.FunctionsLastDigit] = new()
            {
                QuestionText = "Какая была последняя цифра результата вашего первого запроса {0}?",
            },
            // What number was to the left of the displayed letter in {0}?
            // What number was to the left of the displayed letter in Functions?
            [Question.FunctionsLeftNumber] = new()
            {
                QuestionText = "Какое число было слева от отображённой буквы {0}?",
            },
            // What letter was displayed in {0}?
            // What letter was displayed in Functions?
            [Question.FunctionsLetter] = new()
            {
                QuestionText = "Какая буква была отображена {0}?",
            },
            // What number was to the right of the displayed letter in {0}?
            // What number was to the right of the displayed letter in Functions?
            [Question.FunctionsRightNumber] = new()
            {
                QuestionText = "Какое число было справа от отображённой буквы {0}?",
            },

            // The Fuse Box
            // What color flashed {1} in {0}?
            // What color flashed first in The Fuse Box?
            // Note: This question is depicted visually, rather than with words. A translation here will only be used for logging.
            [Question.FuseBoxFlashes] = new()
            {
                QuestionText = "Какой цвет горел {1}-м {0}?",
            },
            // What arrow was shown {1} in {0}?
            // What arrow was shown first in The Fuse Box?
            // Note: This question is depicted visually, rather than with words. A translation here will only be used for logging.
            [Question.FuseBoxArrows] = new()
            {
                QuestionText = "Какая стрелка была показана {1}-й {0}?",
            },

            // Gadgetron Vendor
            // What was your current weapon in {0}?
            // What was your current weapon in Gadgetron Vendor?
            [Question.GadgetronVendorCurrentWeapon] = new()
            {
                QuestionText = "Какое оружие было текущим {0}?",
            },
            // What was the weapon up for sale in {0}?
            // What was the weapon up for sale in Gadgetron Vendor?
            [Question.GadgetronVendorWeaponForSale] = new()
            {
                QuestionText = "Какое оружие продавалось {0}?",
            },

            // Game of Life Cruel
            // Which of these was a color combination that occurred in {0}?
            // Which of these was a color combination that occurred in Game of Life Cruel?
            [Question.GameOfLifeCruelColors] = new()
            {
                QuestionText = "Какие комбинации цветов присутствовали {0}?",
            },

            // The Gamepad
            // What were the numbers on {0}?
            // What were the numbers on The Gamepad?
            [Question.GamepadNumbers] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какие числа были на экране {0}?",
                ModuleName = "Геймпада",
            },

            // Garfield Kart
            // How many puzzle pieces did {0} have?
            // How many puzzle pieces did Garfield Kart have?
            [Question.GarfieldKartPuzzleCount] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Сколько было частей пазла на {0}?",
            },
            // What was the track in {0}?
            // What was the track in Garfield Kart?
            [Question.GarfieldKartTrack] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая была трасса на {0}?",
            },

            // The Garnet Thief
            // Which faction did {1} claim to be in {0}?
            // Which faction did Jungmoon claim to be in The Garnet Thief?
            [Question.GarnetThiefClaim] = new()
            {
                QuestionText = "К какой фракции {1} заявлял, что он принадлежит {0}?",
            },

            // Ghost Movement
            // Where was {1} in {0}?
            // Where was Inky in Ghost Movement?
            [Question.GhostMovementPosition] = new()
            {
                QuestionText = "Где был {1} {0}?",
            },

            // Girlfriend
            // What was the language sung in {0}?
            // What was the language sung in Girlfriend?
            [Question.GirlfriendLanguage] = new()
            {
                QuestionText = "На каком языке была песня {0}?",
            },

            // The Glitched Button
            // What was the cycling bit sequence in {0}?
            // What was the cycling bit sequence in The Glitched Button?
            [Question.GlitchedButtonSequence] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая последовательность битов повторялась на {0}?",
            },

            // Goofy’s Game
            // What number was flashed by the {1} LED in {0}?
            // What number was flashed by the left LED in Goofy’s Game?
            [Question.GoofysGameNumber] = new()
            {
                QuestionText = "Какое число мигало на {1} светодиоде {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левом",
                    ["right"] = "правом",
                    ["center"] = "центральном",
                },
            },

            // Grand Piano
            // Which key was part of the {1} set in {0}?
            // Which key was part of the first set in Grand Piano?
            [Question.GrandPianoKey] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which key was part of the {1} set in {0}?",
            },
            // Which key was the fifth set in {0}?
            // Which key was the fifth set in Grand Piano?
            [Question.GrandPianoFinalKey] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which key made up the fifth set in {0}?",
            },

            // The Gray Button
            // What was the {1} coordinate on the display in {0}?
            // What was the horizontal coordinate on the display in The Gray Button?
            [Question.GrayButtonCoordinates] = new()
            {
                QuestionText = "Какие были {1} координаты на экране {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["horizontal"] = "горизонтальные",
                    ["vertical"] = "вертикальные",
                },
            },

            // Gray Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Gray Cipher?
            [Question.GrayCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // The Great Void
            // What was the {1} color in {0}?
            // What was the first color in The Great Void?
            [Question.GreatVoidColor] = new()
            {
                QuestionText = "Какой был {1}-й цвет {0}?",
                Answers = new Dictionary<string, string>
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
            // What was the {1} digit in {0}?
            // What was the first digit in The Great Void?
            [Question.GreatVoidDigit] = new()
            {
                QuestionText = "Какая была {1}-я цифра {0}?",
            },

            // Green Arrows
            // What was the last number on the display on {0}?
            // What was the last number on the display on Green Arrows?
            [Question.GreenArrowsLastScreen] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какое последнее число было показано на экране {0}?",
                ModuleName = "Зелёных стрелках",
            },

            // The Green Button
            // What was the word submitted in {0}?
            // What was the word submitted in The Green Button?
            [Question.GreenButtonWord] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое слово было введено на {0}?",
            },

            // Green Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Green Cipher?
            [Question.GreenCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Gridlock
            // What was the starting location in {0}?
            // What was the starting location in Gridlock?
            [Question.GridLockStartingLocation] = new()
            {
                QuestionText = "Какая была начальная позиция {0}?",
            },
            // What was the ending location in {0}?
            // What was the ending location in Gridlock?
            [Question.GridLockEndingLocation] = new()
            {
                QuestionText = "Какая была конечная позиция {0}?",
            },
            // What was the starting color in {0}?
            // What was the starting color in Gridlock?
            [Question.GridLockStartingColor] = new()
            {
                QuestionText = "Какой был начальный цвет {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "Зелёный",
                    ["Yellow"] = "Жёлтый",
                    ["Red"] = "Красный",
                    ["Blue"] = "Синий",
                },
            },

            // Grocery Store
            // What was the first item shown in {0}?
            // What was the first item shown in Grocery Store?
            [Question.GroceryStoreFirstItem] = new()
            {
                QuestionText = "Какой товар был показан первым {0}?",
            },

            // Gryphons
            // What was the gryphon’s name in {0}?
            // What was the gryphon’s name in Gryphons?
            [Question.GryphonsName] = new()
            {
                QuestionText = "Какое было имя у грифона {0}?",
            },
            // What was the gryphon’s age in {0}?
            // What was the gryphon’s age in Gryphons?
            [Question.GryphonsAge] = new()
            {
                QuestionText = "Сколько лет было грифону {0}?",
            },

            // Guess Who?
            // Did {1} flash “YES” in {0}?
            [Question.GuessWhoColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Did {1} flash “YES” in {0}?",
            },

            // Gyromaze
            // What color was the {1} LED in {0}?
            // What color was the top LED in Gyromaze?
            [Question.GyromazeLEDColor] = new()
            {
                QuestionText = "Какого цвета был {1} светодиод {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхний",
                    ["bottom"] = "нижний",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Blue"] = "Синий",
                    ["Green"] = "Зелёный",
                    ["Yellow"] = "Жёлтый",
                },
            },

            // h
            // What was the transmitted letter in {0}?
            // What was the transmitted letter in h?
            [Question.HLetter] = new()
            {
                QuestionText = "Какая буква передавалась в \"{0}\"?",
            },

            // Halli Galli
            // Which fruit were there five of in {0}?
            // Which fruit were there five of in Halli Galli?
            [Question.HalliGalliFruit] = new()
            {
                QuestionText = "Каких фруктов было пять {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Strawberries"] = "Клубники",
                    ["Melons"] = "Арбузов",
                    ["Lemons"] = "Лемонов",
                    ["Raspberries"] = "Малины",
                    ["Bananas"] = "Бананов",
                },
            },
            // What were the relevant counts in {0}?
            // What were the relevant counts in Halli Galli?
            [Question.HalliGalliCounts] = new()
            {
                QuestionText = "Какие количества имели значение в {0}?",
            },

            // Hereditary Base Notation
            // What was the given number in {0}?
            // What was the given number in Hereditary Base Notation?
            [Question.HereditaryBaseNotationInitialNumber] = new()
            {
                QuestionText = "Какое число было дано {0}?",
            },

            // The Hexabutton
            // What label was printed on {0}?
            // What label was printed on The Hexabutton?
            [Question.HexabuttonLabel] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая была надпись на {0}?",
            },

            // Hexamaze
            // What was the color of the pawn in {0}?
            // What was the color of the pawn in Hexamaze?
            [Question.HexamazePawnColor] = new()
            {
                QuestionText = "Какого цвета была фигурка {0}?",
                ModuleName = "Гексабиринте",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красная",
                    ["Yellow"] = "Жёлтая",
                    ["Green"] = "Зелёная",
                    ["Cyan"] = "Голубая",
                    ["Blue"] = "Синяя",
                    ["Pink"] = "Розовая",
                },
            },

            // hexOrbits
            // What was the {1} shape for the {2} display in {0}?
            // What was the fast shape for the first display in hexOrbits?
            [Question.HexOrbitsShape] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} shape for the {2} display in {0}?",
            },

            // hexOS
            // What were the deciphered letters in {0}?
            // What were the deciphered letters in hexOS?
            [Question.HexOSCipher] = new()
            {
                QuestionText = "Какие буквы были расшифрованны {0}?",
            },
            // What was the deciphered phrase in {0}?
            // What was the deciphered phrase in hexOS?
            [Question.HexOSOctCipher] = new()
            {
                QuestionText = "Какая фраза была расшифрованна {0}?",
            },
            // What was the {1} 3-digit number cycled by the screen in {0}?
            // What was the first 3-digit number cycled by the screen in hexOS?
            [Question.HexOSScreen] = new()
            {
                QuestionText = "Какое было {1}-е трёхзначное число в последовательности на экране {0}?",
            },
            // What were the rhythm values in {0}?
            // What were the rhythm values in hexOS?
            [Question.HexOSSum] = new()
            {
                QuestionText = "Какие были значения ритмов {0}?",
            },

            // Hidden Colors
            // What was the color of the main LED in {0}?
            // What was the color of the main LED in Hidden Colors?
            [Question.HiddenColorsLED] = new()
            {
                QuestionText = "Какого цвета был главный светодиод {0}?",
                Answers = new Dictionary<string, string>
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

            // The Hidden Value
            // What was displayed on {0}?
            // What was displayed on The Hidden Value?
            [Question.HiddenValueDisplay] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Что было отображено на {0}?",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
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

            // The High Score
            // What was the position of the player in {0}?
            // What was the position of the player in The High Score?
            [Question.HighScorePosition] = new()
            {
                QuestionText = "На котором месте был игрок {0}?",
            },
            // What was the score of the player in {0}?
            // What was the score of the player in The High Score?
            [Question.HighScoreScore] = new()
            {
                QuestionText = "Какой был счёт у игрока {0}?",
            },

            // Hill Cycle
            // Which direction was the {1} dial pointing in {0}?
            // Which direction was the first dial pointing in Hill Cycle?
            [Question.HillCycleDialDirections] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which direction was the {1} dial pointing in {0}?",
            },
            // What letter was written on the {1} dial in {0}?
            // What letter was written on the first dial in Hill Cycle?
            [Question.HillCycleDialLabels] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What letter was written on the {1} dial in {0}?",
            },

            // Hinges
            // Which of these hinges was initially {1} {0}?
            // Which of these hinges was initially present on Hinges?
            [Question.HingesInitialHinges] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какие из петель изначально {1} {0}?",
                ModuleName = "Петлях",
                FormatArgs = new Dictionary<string, string>
                {
                    ["present on"] = "присутствовали",
                    ["absent from"] = "отсутствовали",
                },
            },

            // Hogwarts
            // Which House was {1} solved for in {0}?
            // Which House was Binary Puzzle solved for in Hogwarts?
            [Question.HogwartsHouse] = new()
            {
                QuestionText = "Какому дому зачли обезвреживание модуля \"{1}\" {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Gryffindor"] = "Gryffindor",
                    ["Hufflepuff"] = "Hufflepuff",
                    ["Slytherin"] = "Slytherin",
                    ["Ravenclaw"] = "Ravenclaw",
                },
            },
            // Which module was solved for {1} in {0}?
            // Which module was solved for Gryffindor in Hogwarts?
            [Question.HogwartsModule] = new()
            {
                QuestionText = "Обезвреживание какого модуля зачли {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Gryffindor"] = "Gryffindor",
                    ["Hufflepuff"] = "Hufflepuff",
                    ["Slytherin"] = "Slytherin",
                    ["Ravenclaw"] = "Ravenclaw",
                },
            },

            // Hold Ups
            // What was the name of the {1} shadow shown in {0}?
            // What was the name of the first shadow shown in Hold Ups?
            [Question.HoldUpsShadows] = new()
            {
                QuestionText = "Какое было имя у {1}-й тени {0}?",
            },

            // Homophones
            // What was the {1} displayed phrase in {0}?
            // What was the first displayed phrase in Homophones?
            [Question.HomophonesDisplayedPhrases] = new()
            {
                QuestionText = "Какая была {1}-я показанная фраза {0}?",
            },

            // Horrible Memory
            // In what position was the button pressed on the {1} stage of {0}?
            // In what position was the button pressed on the first stage of Horrible Memory?
            [Question.HorribleMemoryPositions] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая позиция была нажата на {1}-м этапе {0}?",
            },
            // What was the label of the button pressed on the {1} stage of {0}?
            // What was the label of the button pressed on the first stage of Horrible Memory?
            [Question.HorribleMemoryLabels] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была надпись у нажатой кнопки на {1}-м этапе {0}?",
            },
            // What color was the button pressed on the {1} stage of {0}?
            // What color was the button pressed on the first stage of Horrible Memory?
            [Question.HorribleMemoryColors] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была нажатая кнопка на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "синий",
                    ["green"] = "зелёный",
                    ["red"] = "красный",
                    ["orange"] = "оранжевый",
                    ["purple"] = "фиолетовый",
                    ["pink"] = "розовый",
                },
            },

            // Human Resources
            // Which was a descriptor shown in {1} in {0}?
            // Which was a descriptor shown in red in Human Resources?
            [Question.HumanResourcesDescriptors] = new()
            {
                QuestionText = "Какие описания {0} были показаны {1} цветом?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красным",
                    ["green"] = "зелёным",
                },
            },
            // Who was {1} in {0}?
            // Who was fired in Human Resources?
            [Question.HumanResourcesHiredFired] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Кто из {0} был {1}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["fired"] = "уволен",
                    ["hired"] = "нанят",
                },
            },

            // Hunting
            // Which of the first three stages of {0} had the {1} symbol {2}?
            // Which of the first three stages of Hunting had the column symbol first?
            [Question.HuntingColumnsRows] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "На каком из первых трёх этапов {0} символ {1} был {2}-м?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["column"] = "столбца",
                    ["row"] = "строки",
                },
                Answers = new Dictionary<string, string>
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
            },

            // The Hypercube
            // What was the {1} rotation in {0}?
            // What was the first rotation in The Hypercube?
            [Question.HypercubeRotations] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Каким было {1}-е вращение {0}?",
                ModuleName = "Гиперкуба",
            },

            // HyperForget
            // What was the rotation for the {1} stage in {0}?
            // What was the rotation for the first stage in HyperForget?
            [Question.HyperForgetRotations] = new()
            {
                QuestionText = "Какое было вращение на {1}-м этапе {0}?",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the HyperForget whose rotation in the {1} stage was {0}"] = "в HyperForget, где на {1}-м этапе было вращение {0}",
                },
            },

            // The Hyperlink
            // What was the {1} character of the hyperlink in {0}?
            // What was the first character of the hyperlink in The Hyperlink?
            [Question.HyperlinkCharacters] = new()
            {
                QuestionText = "Какой был {1}-й символ ссылки {0}?",
            },
            // Which module was referenced on {0}?
            // Which module was referenced on The Hyperlink?
            [Question.HyperlinkAnswer] = new()
            {
                Conjugation = Conjugation.NominativeMasculine,
                QuestionText = "На какой модуль ссылался {0}?",
            },

            // Ice Cream
            // Which one of these flavours {1} to the {2} customer in {0}?
            // Which one of these flavours was on offer, but not sold, to the first customer in Ice Cream?
            [Question.IceCreamFlavour] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой из этих вкусов {0} {1} {2}-му посетителю?",
                ModuleName = "Мороженого",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was on offer, but not sold,"] = "был предложен, но не продан",
                    ["was not on offer"] = "не был предложен",
                },
            },
            // Who was the {1} customer in {0}?
            // Who was the first customer in Ice Cream?
            [Question.IceCreamCustomer] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Кто был {1}-м посетителем {0}?",
                ModuleName = "Мороженого",
            },

            // Identification Crisis
            // What was the {1} shape used in {0}?
            // What was the first shape used in Identification Crisis?
            [Question.IdentificationCrisisShape] = new()
            {
                QuestionText = "Какая была {1}-я фигура {0}?",
            },
            // What was the {1} identification module used in {0}?
            // What was the first identification module used in Identification Crisis?
            [Question.IdentificationCrisisDataset] = new()
            {
                QuestionText = "Какой был {1}-й использованный модуль {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Morse Identification"] = "Morse Identification",
                    ["Boozleglyph Identification"] = "Boozleglyph Identification",
                    ["Plant Identification"] = "Plant Identification",
                    ["Pickup Identification"] = "Pickup Identification",
                    ["Emotiguy Identification"] = "Emotiguy Identification",
                    ["Ars Goetia Identification"] = "Ars Goetia Identification",
                    ["Mii Identification"] = "Mii Identification",
                    ["Customer identification"] = "Customer identification",
                    ["Spongebob Birthday Identification"] = "Spongebob Birthday Identification",
                    ["VTuber Identification"] = "VTuber Identification",
                },
            },

            // Identity Parade
            // Which hair color {1} listed in {0}?
            // Which hair color was listed in Identity Parade?
            [Question.IdentityParadeHairColors] = new()
            {
                QuestionText = "Какой цвет волос {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "присутствовал",
                    ["was not"] = "отсутствовал",
                },
            },
            // Which build {1} listed in {0}?
            // Which build was listed in Identity Parade?
            [Question.IdentityParadeBuilds] = new()
            {
                QuestionText = "Какое телосложение {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "присутствовало",
                    ["was not"] = "отсутствовало",
                },
            },
            // Which attire {1} listed in {0}?
            // Which attire was listed in Identity Parade?
            [Question.IdentityParadeAttires] = new()
            {
                QuestionText = "Какой наряд {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "присутствовал",
                    ["was not"] = "отсутствовал",
                },
            },

            // The Impostor
            // Which module was {0} pretending to be?
            // Which module was The Impostor pretending to be?
            [Question.ImpostorDisguise] = new()
            {
                Conjugation = Conjugation.NominativeMasculine,
                QuestionText = "Каким модулем притворялся {0}?",
            },

            // Indigo Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Indigo Cipher?
            [Question.IndigoCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Infinite Loop
            // What was the selected word in {0}?
            // What was the selected word in Infinite Loop?
            [Question.InfiniteLoopSelectedWord] = new()
            {
                QuestionText = "Какое слово было выбрано {0}?",
            },

            // Ingredients
            // Which ingredient was used in {0}?
            // Which ingredient was used in Ingredients?
            [Question.IngredientsIngredients] = new()
            {
                QuestionText = "Какие ингредиенты были использованны {0}?",
            },
            // Which ingredient was listed but not used in {0}?
            // Which ingredient was listed but not used in Ingredients?
            [Question.IngredientsNonIngredients] = new()
            {
                QuestionText = "Какие ингредиенты были указаны, но не были использованы {0}?",
            },

            // Inner Connections
            // What color was the LED in {0}?
            // What color was the LED in Inner Connections?
            [Question.InnerConnectionsLED] = new()
            {
                QuestionText = "Какого цвета был светодиод {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Black"] = "Чёрный",
                    ["Blue"] = "Синий",
                    ["Red"] = "Красный",
                    ["White"] = "Белый",
                    ["Yellow"] = "Жёлтый",
                    ["Green"] = "Зелёный",
                },
            },
            // What was the digit flashed in Morse in {0}?
            // What was the digit flashed in Morse in Inner Connections?
            [Question.InnerConnectionsMorse] = new()
            {
                QuestionText = "Какая цифра была передана кодом Морзе {0}?",
            },

            // Interpunct
            // What was the symbol displayed in the {1} stage of {0}?
            // What was the symbol displayed in the first stage of Interpunct?
            [Question.InterpunctDisplay] = new()
            {
                QuestionText = "Какой символ был показан на {1}-м этапе {0}?",
            },

            // IPA
            // What sound played in {0}?
            // What sound played in IPA?
            [Question.IpaSound] = new()
            {
                QuestionText = "Какой звук был воспроизведён {0}?",
            },

            // The iPhone
            // What was the {1} PIN digit in {0}?
            // What was the first PIN digit in The iPhone?
            [Question.iPhoneDigits] = new()
            {
                QuestionText = "Какая была {1}-я цифра пинкода {0}?",
                ModuleName = "iPhone",
            },

            // Jenga
            // Which symbol was on the first correctly pulled block in {0}?
            // Which symbol was on the first correctly pulled block in Jenga?
            [Question.JengaFirstBlock] = new()
            {
                QuestionText = "Какой символ был на первом правильно вытянутом блоке {0}?",
            },

            // The Jewel Vault
            // What number was wheel {1} in {0}?
            // What number was wheel A in The Jewel Vault?
            [Question.JewelVaultWheels] = new()
            {
                QuestionText = "Какой был номер у колеса {1} {0}?",
                ModuleName = "Хранилище драгоценностей",
            },

            // Jumble Cycle
            // Which direction was the {1} dial pointing in {0}?
            // Which direction was the first dial pointing in Jumble Cycle?
            [Question.JumbleCycleDialDirections] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which direction was the {1} dial pointing in {0}?",
            },
            // What letter was written on the {1} dial in {0}?
            // What letter was written on the first dial in Jumble Cycle?
            [Question.JumbleCycleDialLabels] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What letter was written on the {1} dial in {0}?",
            },

            // Juxtacolored Squares
            // What was the color of this square in {0}?
            // What was the color of this square in Juxtacolored Squares?
            [Question.JuxtacoloredSquaresColorsByPosition] = new()
            {
                Conjugation = Conjugation.PrepositivePlural,
                QuestionText = "Какого цвета был этот квадрат на {0}?",
                ModuleName = "Смежных цветных квадратах",
                Answers = new Dictionary<string, string>
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
            // Which square was {1} in {0}?
            // Which square was red in Juxtacolored Squares?
            [Question.JuxtacoloredSquaresPositionsByColor] = new()
            {
                Conjugation = Conjugation.PrepositivePlural,
                QuestionText = "Какой квадрат был {1} цвета на {0}?",
                ModuleName = "Смежных цветных квадратах",
                FormatArgs = new Dictionary<string, string>
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

            // Kanji
            // What was the displayed word in the {1} stage of {0}?
            // What was the displayed word in the first stage of Kanji?
            [Question.KanjiDisplayedWords] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое слово было показано на {1}-м этапе {0}?",
                ModuleName = "Кандзи",
            },

            // The Kanye Encounter
            // What was a food item displayed in {0}?
            // What was a food item displayed in The Kanye Encounter?
            [Question.KanyeEncounterFoods] = new()
            {
                QuestionText = "Какая еда была показана {0}?",
            },

            // Keypad Combinations
            // Which number was displayed on the {1} button, but not part of the answer on {0}?
            // Which number was displayed on the first button, but not part of the answer on Keypad Combinations?
            [Question.KeypadCombinationWrongNumbers] = new()
            {
                QuestionText = "Какое число было показано на {1}-й кнопке, но не являлось частью решения {0}?",
            },

            // Keypad Magnified
            // What was the position of the LED in {0}?
            // What was the position of the LED in Keypad Magnified?
            [Question.KeypadMagnifiedLED] = new()
            {
                QuestionText = "Где был светодиод {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Top-left"] = "Сверху слева",
                    ["Top-right"] = "Сверху справа",
                    ["Bottom-left"] = "Снизу слева",
                    ["Bottom-right"] = "Снизу справа",
                },
            },

            // Keypad Maze
            // Which of these cells was yellow in {0}?
            // Which of these cells was yellow in Keypad Maze?
            [Question.KeypadMazeYellow] = new()
            {
                QuestionText = "Какая из этих клеток была жёлтой {0}?",
            },

            // Keypad Sequence
            // What was this key’s label on the {1} panel in {0}?
            // What was this key’s label on the first panel in Keypad Sequence?
            [Question.KeypadSequenceLabels] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была надпись на этой кнопке на {1}-й панели {0}?",
            },

            // Keywords
            // What were the first four letters on the display in {0}?
            // What were the first four letters on the display in Keywords?
            [Question.KeywordsDisplayedKey] = new()
            {
                QuestionText = "Какие были первые четыре буквы на экране {0}?",
            },

            // Know Your Way
            // Which way was the arrow pointing in {0}?
            // Which way was the arrow pointing in Know Your Way?
            [Question.KnowYourWayArrow] = new()
            {
                QuestionText = "В какую сторону была направлена стрелка {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Вверх",
                    ["Down"] = "Вниз",
                    ["Left"] = "Влево",
                    ["Right"] = "Вправо",
                },
            },
            // Which LED was green in {0}?
            // Which LED was green in Know Your Way?
            [Question.KnowYourWayLed] = new()
            {
                QuestionText = "Какой светодиод был зелёным {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Top"] = "Верхний",
                    ["Bottom"] = "Нижний",
                    ["Right"] = "Правый",
                    ["Left"] = "Левый",
                },
            },

            // Kooky Keypad
            // What color was the {1} button’s LED in {0}?
            // What color was the top-left button’s LED in Kooky Keypad?
            [Question.KookyKeypadColor] = new()
            {
                QuestionText = "Какого цвета был светодиод на {1} кнопке {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхней левой",
                    ["top-right"] = "верхней правой",
                    ["bottom-left"] = "нижней левой",
                    ["bottom-right"] = "нижней правой",
                },
                Answers = new Dictionary<string, string>
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

            // Kudosudoku
            // Which square was {1} in {0}?
            // Which square was pre-filled in Kudosudoku?
            [Question.KudosudokuPrefilled] = new()
            {
                QuestionText = "Какой квадрат {1} {0}?",
                ModuleName = "Кудосудоку",
                FormatArgs = new Dictionary<string, string>
                {
                    ["pre-filled"] = "был изначально заполнен",
                    ["not pre-filled"] = "не был изначально заполнен",
                },
            },

            // Kuro
            // What was Kuro’s mood in {0}?
            // What was Kuro’s mood in Kuro?
            [Question.KuroMood] = new()
            {
                QuestionText = "Какое было настроение у Kuro {0}?",
            },

            // The Labyrinth
            // Where was one of the portals in layer {1} in {0}?
            // Where was one of the portals in layer 1 (Red) in The Labyrinth?
            [Question.LabyrinthPortalLocations] = new()
            {
                QuestionText = "Где находился один из порталов на {1} слое {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["1 (Red)"] = "1-м (красном)",
                    ["2 (Orange)"] = "2-м (оранжевом)",
                    ["3 (Yellow)"] = "3-м (жёлтом)",
                    ["4 (Green)"] = "4-м (зелёном)",
                    ["5 (Blue)"] = "5-м (синем)",
                },
            },
            // In which layer was this portal in {0}?
            // In which layer was this portal in The Labyrinth?
            [Question.LabyrinthPortalStage] = new()
            {
                QuestionText = "На каком слое находился этот портал {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["1 (Red)"] = "1-м (красном)",
                    ["2 (Orange)"] = "2-м (оранжевом)",
                    ["3 (Yellow)"] = "3-м (жёлтом)",
                    ["4 (Green)"] = "4-м (зелёном)",
                    ["5 (Blue)"] = "5-м (синем)",
                },
            },

            // Ladder Lottery
            // Which light was on in {0}?
            // Which light was on in Ladder Lottery?
            [Question.LadderLotteryLightOn] = new()
            {
                QuestionText = "Какая лампочка была включена {0}?",
            },

            // Ladders
            // Which color was present on the second ladder in {0}?
            // Which color was present on the second ladder in Ladders?
            [Question.LaddersStage2Colors] = new()
            {
                QuestionText = "Какой цвет присутствовал на второй лестнице {0}?",
                Answers = new Dictionary<string, string>
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
            // What color was missing on the third ladder in {0}?
            // What color was missing on the third ladder in Ladders?
            [Question.LaddersStage3Missing] = new()
            {
                QuestionText = "Какой цвет отсутствовал на третьей лестнице {0}?",
                Answers = new Dictionary<string, string>
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

            // Langton’s Anteater
            // Which of these squares was initially {1} in {0}?
            // Which of these squares was initially black in Langton’s Anteater?
            [Question.LangtonsAnteaterInitialState] = new()
            {
                QuestionText = "Какой из этих квадратов изначально был {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["black"] = "чёрным",
                    ["white"] = "белым",
                },
            },

            // Lasers
            // What was the number on the {1} hatch on {0}?
            // What was the number on the top-left hatch on Lasers?
            [Question.LasersHatches] = new()
            {
                QuestionText = "Какое число было на {1} люке {0}?",
                FormatArgs = new Dictionary<string, string>
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

            // LED Encryption
            // What was the correct letter you pressed in the {1} stage of {0}?
            // What was the correct letter you pressed in the first stage of LED Encryption?
            [Question.LEDEncryptionPressedLetters] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая правильная буква была нажата на {1}-м этапе {0}?",
                ModuleName = "Шифра светодиодов",
            },

            // LED Grid
            // How many LEDs were unlit in {0}?
            // How many LEDs were unlit in LED Grid?
            [Question.LEDGridNumBlack] = new()
            {
                Conjugation = Conjugation.PrepositiveFeminine,
                QuestionText = "Сколько светодиодов не горело на {0}?",
                ModuleName = "Сетке светодиодов",
            },

            // LED Math
            // What color was {1} in {0}?
            // What color was LED A in LED Math?
            [Question.LEDMathLights] = new()
            {
                QuestionText = "Какого цвета был {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["LED A"] = "светодиод A",
                    ["LED B"] = "светодиод B",
                    ["the operator LED"] = "светодиод-оператор",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красного",
                    ["Blue"] = "Синего",
                    ["Yellow"] = "Жёлтого",
                    ["Green"] = "Зелёного",
                },
            },

            // LEDs
            // What was the initial color of the changed LED in {0}?
            // What was the initial color of the changed LED in LEDs?
            [Question.LEDsOriginalColor] = new()
            {
                QuestionText = "Какой был начальный цвет изменённого светодиода {0}?",
                Answers = new Dictionary<string, string>
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

            // LEGOs
            // What were the dimensions of the {1} piece in {0}?
            // What were the dimensions of the red piece in LEGOs?
            [Question.LEGOsPieceDimensions] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Каких размеров была {1} деталь {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                    ["cyan"] = "cyan",
                    ["magenta"] = "magenta",
                    ["yellow"] = "yellow",
                },
            },

            // Letter Math
            // What was the letter on the {1} display in {0}?
            // What was the letter on the left display in Letter Math?
            [Question.LetterMathDisplay] = new()
            {
                QuestionText = "Какая буква была на {1} экране {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левом",
                    ["right"] = "правом",
                },
            },

            // Light Bulbs
            // What was the color of the {1} bulb in {0}?
            // What was the color of the left bulb in Light Bulbs?
            [Question.LightBulbsColors] = new()
            {
                QuestionText = "Какой был цвет {1} лампочки {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левой",
                    ["right"] = "правой",
                },
                Answers = new Dictionary<string, string>
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

            // Linq
            // What was the {1} function in {0}?
            // What was the first function in Linq?
            [Question.LinqFunction] = new()
            {
                QuestionText = "Какая была {1}-я функция {0}?",
            },

            // Lion’s Share
            // Which year was displayed on {0}?
            // Which year was displayed on Lion’s Share?
            [Question.LionsShareYear] = new()
            {
                QuestionText = "Какой год был показан {0}?",
            },
            // Which lion was present but removed in {0}?
            // Which lion was present but removed in Lion’s Share?
            [Question.LionsShareRemovedLions] = new()
            {
                QuestionText = "Какой лев изначально присутствовал, но потом был убран {0}?",
            },

            // Listening
            // What clip was played in {0}?
            // What clip was played in Listening?
            [Question.ListeningSound] = new()
            {
                QuestionText = "Какой звук был воспроизведён {0}?",
                ModuleName = "Прослушке",
            },

            // Logical Buttons
            // What was the color of the {1} button in the {2} stage of {0}?
            // What was the color of the top button in the first stage of Logical Buttons?
            [Question.LogicalButtonsColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была {1} кнопка на {2}-м этапе {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхняя",
                    ["bottom-left"] = "нижняя левая",
                    ["bottom-right"] = "нижняя правая",
                },
                Answers = new Dictionary<string, string>
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
            // What was the label on the {1} button in the {2} stage of {0}?
            // What was the label on the top button in the first stage of Logical Buttons?
            [Question.LogicalButtonsLabel] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была надпись на {1} кнопке на {2}-м этапе {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхней",
                    ["bottom-left"] = "нижней левой",
                    ["bottom-right"] = "нижней правой",
                },
            },
            // What was the final operator in the {1} stage of {0}?
            // What was the final operator in the first stage of Logical Buttons?
            [Question.LogicalButtonsOperator] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был конечный оператор на {1}-м этапе {0}?",
            },

            // Logic Gates
            // What was {1} in {0}?
            // What was gate A in Logic Gates?
            [Question.LogicGatesGates] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Каким был {1} {0}?",
                ModuleName = "Логических элементах",
                FormatArgs = new Dictionary<string, string>
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

            // Lombax Cubes
            // What was the {1} letter on the button in {0}?
            // What was the first letter on the button in Lombax Cubes?
            [Question.LombaxCubesLetters] = new()
            {
                QuestionText = "Какая была {1}-я буква на кнопке {0}?",
            },

            // The London Underground
            // Where did the {1} journey on {0} {2}?
            // Where did the first journey on The London Underground depart from?
            [Question.LondonUndergroundStations] = new()
            {
                QuestionText = "{2} отправился {1}-й рейс {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["depart from"] = "Откуда",
                    ["arrive to"] = "Куда",
                },
            },

            // Long Words
            // What was the word on the top display on {0}?
            // What was the word on the top display on Long Words?
            [Question.LongWordsWord] = new()
            {
                QuestionText = "Какое слово было на верхнем экране {0}?",
            },

            // Mad Memory
            // What was on the display in the {1} stage of {0}?
            // What was on the display in the first stage of Mad Memory?
            [Question.MadMemoryDisplays] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Что было на экране на {1} этапе {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first"] = "первом",
                    ["second"] = "втором",
                    ["third"] = "третьем",
                    ["4th"] = "четвёртом",
                },
            },

            // Mafia
            // Who was a player, but not the Godfather, in {0}?
            // Who was a player, but not the Godfather, in Mafia?
            [Question.MafiaPlayers] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Кто был игроком, но не являлся крёстным отцом {0}?",
                ModuleName = "Мафии",
            },

            // Magenta Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Magenta Cipher?
            [Question.MagentaCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Mahjong
            // Which tile was part of the {1} matched pair in {0}?
            // Which tile was part of the first matched pair in Mahjong?
            [Question.MahjongMatches] = new()
            {
                QuestionText = "Какая кость была частью {1}-й сопоставленной пары {0}?",
            },
            // Which tile was shown in the bottom-left of {0}?
            // Which tile was shown in the bottom-left of Mahjong?
            [Question.MahjongCountingTile] = new()
            {
                QuestionText = "Какая кость была показана снизу слева {0}?",
            },

            // Main Page
            // Which color did the bubble not display in {0}?
            // Which color did the bubble not display in Main Page?
            [Question.MainPageBubbleColors] = new()
            {
                QuestionText = "Какого цвета не было во фразах {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Синий",
                    ["Green"] = "Зелёный",
                    ["Red"] = "Красный",
                    ["Yellow"] = "Жёлтый",
                },
            },
            // Which main page did the {1} button’s effect come from in {0}?
            // Which main page did the toons button’s effect come from in Main Page?
            [Question.MainPageButtonEffectOrigin] = new()
            {
                QuestionText = "Какой главной странице соответствовал эффект кнопки {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["toons"] = "toons",
                    ["games"] = "games",
                    ["characters"] = "characters",
                    ["downloads"] = "downloads",
                    ["store"] = "store",
                    ["email"] = "email",
                },
            },
            // Which of the following messages did the bubble {1} in {0}?
            // Which of the following messages did the bubble display in Main Page?
            [Question.MainPageBubbleMessages] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая фраза {1} на {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["display"] = "присутствовала",
                    ["not display"] = "отсутствовала",
                },
            },
            // Which main page did {1} come from in {0}?
            // Which main page did Homestar come from in Main Page?
            [Question.MainPageHomestarBackgroundOrigin] = new()
            {
                QuestionText = "С какой главной страницы был взят {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Homestar"] = "Homestar",
                    ["the background"] = "фон",
                },
            },

            // M&Ms
            // What color was the text on the {1} button in {0}?
            // What color was the text on the first button in M&Ms?
            [Question.MandMsColors] = new()
            {
                QuestionText = "Какого цвета была надпись на {1}-й кнопке {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красного",
                    ["green"] = "Зелёного",
                    ["orange"] = "Оранжевого",
                    ["blue"] = "Синего",
                    ["yellow"] = "Жёлтого",
                    ["brown"] = "Коричневого",
                },
            },
            // What was the text on the {1} button in {0}?
            // What was the text on the first button in M&Ms?
            [Question.MandMsLabels] = new()
            {
                QuestionText = "Какая надпись была на {1}-й кнопке {0}?",
            },

            // M&Ns
            // What color was the text on the {1} button in {0}?
            // What color was the text on the first button in M&Ns?
            [Question.MandNsColors] = new()
            {
                QuestionText = "Какого цвета была надпись на {1}-й кнопке {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красного",
                    ["green"] = "Зелёного",
                    ["orange"] = "Оранжевого",
                    ["blue"] = "Синего",
                    ["yellow"] = "Жёлтого",
                    ["brown"] = "Коричневого",
                },
            },
            // What was the text on the correct button in {0}?
            // What was the text on the correct button in M&Ns?
            [Question.MandNsLabel] = new()
            {
                QuestionText = "Какая надпись была на правильной кнопке {0}?",
            },

            // Maritime Flags
            // What bearing was signalled in {0}?
            // What bearing was signalled in Maritime Flags?
            [Question.MaritimeFlagsBearing] = new()
            {
                QuestionText = "Какой пеленг был обозначен {0}?",
            },
            // Which callsign was signalled in {0}?
            // Which callsign was signalled in Maritime Flags?
            [Question.MaritimeFlagsCallsign] = new()
            {
                QuestionText = "Какой позывной был обозначен {0}?",
            },

            // Maritime Semaphore
            // In which position was the dummy in {0}?
            // In which position was the dummy in Maritime Semaphore?
            [Question.MaritimeSemaphoreDummy] = new()
            {
                QuestionText = "В какой позиции была фиктивная конфигурация {0}?",
            },
            // Which letter was shown by the {2} in the {1} position in {0}?
            // Which letter was shown by the left flag in the first position in Maritime Semaphore?
            [Question.MaritimeSemaphoreLetter] = new()
            {
                QuestionText = "Какая буква была показана {2} в {1}-й позиции {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left flag"] = "левым флагом",
                    ["right flag"] = "правым флагом",
                    ["semaphore"] = "семафором",
                },
            },

            // The Maroon Button
            // What was A in {0}?
            // What was A in The Maroon Button?
            [Question.MaroonButtonA] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой был флаг А на {0}?",
            },

            // Maroon Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Maroon Cipher?
            [Question.MaroonCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Mashematics
            // What was the answer in {0}?
            // What was the answer in Mashematics?
            [Question.MashematicsAnswer] = new()
            {
                QuestionText = "Какой был верный ответ {0}?",
            },
            // What was the {1} number in the equation on {0}?
            // What was the first number in the equation on Mashematics?
            [Question.MashematicsCalculation] = new()
            {
                QuestionText = "Какое было {1}-е число в уравнении {0}?",
            },

            // Master Tapes
            // Which song was played in {0}?
            // Which song was played in Master Tapes?
            [Question.MasterTapesPlayedSong] = new()
            {
                QuestionText = "Какая песня была проиграна {0}?",
            },

            // Match Refereeing
            // Which planet was present in the {1} stage of {0}?
            // Which planet was present in the first stage of Match Refereeing?
            [Question.MatchRefereeingPlanet] = new()
            {
                QuestionText = "Какая планета присутствовала на {1}-м этапе {0}?",
            },

            // Math ’em
            // What was the color of this tile before the shuffle on {0}?
            // What was the color of this tile before the shuffle on Math ’em?
            [Question.MathEmColor] = new()
            {
                QuestionText = "Какого цвета была эта плитка до перемешивания {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Белый",
                    ["Bronze"] = "Бронзовый",
                    ["Silver"] = "Серебрянный",
                    ["Gold"] = "Золотой",
                },
            },
            // What was the design on this tile before the shuffle on {0}?
            // What was the design on this tile before the shuffle on Math ’em?
            [Question.MathEmLabel] = new()
            {
                QuestionText = "Какой узор был на этой плитке до перемешивания {0}?",
            },

            // The Matrix
            // Which word was part of the latest access code in {0}?
            // Which word was part of the latest access code in The Matrix?
            [Question.MatrixAccessCode] = new()
            {
                QuestionText = "Какое слово было частью последнего кода доступа {0}?",
            },
            // What was the glitched word in {0}?
            // What was the glitched word in The Matrix?
            [Question.MatrixGlitchWord] = new()
            {
                QuestionText = "Какое слово было глючным {0}?",
            },

            // Maze
            // In which {1} was the starting position in {0}, counting from the {2}?
            // In which column was the starting position in Maze, counting from the left?
            [Question.MazeStartingPosition] = new()
            {
                QuestionText = "В {1} (считая {2}) была начальная позиция {0}?",
                ModuleName = "Лабиринте",
                FormatArgs = new Dictionary<string, string>
                {
                    ["column"] = "каком столбце",
                    ["left"] = "слева направо",
                    ["row"] = "какой строке",
                    ["top"] = "сверху вниз",
                },
            },

            // Maze³
            // What was the color of the starting face in {0}?
            // What was the color of the starting face in Maze³?
            [Question.Maze3StartingFace] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой цвет был у начальной стороны {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Blue"] = "Blue",
                    ["Yellow"] = "Yellow",
                    ["Green"] = "Green",
                    ["Magenta"] = "Magenta",
                    ["Orange"] = "Orange",
                },
            },

            // Maze Identification
            // What was the seed of the maze in {0}?
            // What was the seed of the maze in Maze Identification?
            [Question.MazeIdentificationSeed] = new()
            {
                QuestionText = "Какое было зерно у лабиринта {0}?",
            },
            // What was the function of button {1} in {0}?
            // What was the function of button 1 in Maze Identification?
            [Question.MazeIdentificationNum] = new()
            {
                QuestionText = "Какая была функция у кнопки {1} {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Forwards"] = "Вперёд",
                    ["Clockwise"] = "90° по часовой",
                    ["Backwards"] = "Назад",
                    ["Counter-clockwise"] = "90° против часовой",
                },
            },
            // Which button {1} in {0}?
            // Which button moved you forwards in Maze Identification?
            [Question.MazeIdentificationFunc] = new()
            {
                QuestionText = "Какая кнопка {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["moved you forwards"] = "передвинула вас вперёд",
                    ["turned you clockwise"] = "повернула вас по часовой",
                    ["moved you backwards"] = "передвинула вас назад",
                    ["turned you counter-clockwise"] = "повернула вас против часовой",
                },
            },

            // Mazematics
            // Which was the {1} value in {0}?
            // Which was the initial value in Mazematics?
            [Question.MazematicsValue] = new()
            {
                QuestionText = "Какая была {1} величина {0}?",
                ModuleName = "Матебиринте",
                FormatArgs = new Dictionary<string, string>
                {
                    ["initial"] = "начальная",
                    ["goal"] = "целевая",
                },
            },

            // Maze Scrambler
            // What was the starting position on {0}?
            // What was the starting position on Maze Scrambler?
            [Question.MazeScramblerStart] = new()
            {
                QuestionText = "Какая была начальная позиция {0}?",
                Answers = new Dictionary<string, string>
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
            // What was the goal on {0}?
            // What was the goal on Maze Scrambler?
            [Question.MazeScramblerGoal] = new()
            {
                QuestionText = "Где была цель {0}?",
                Answers = new Dictionary<string, string>
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
            // Which of these positions was a maze marking on {0}?
            // Which of these positions was a maze marking on Maze Scrambler?
            [Question.MazeScramblerIndicators] = new()
            {
                QuestionText = "На какой позиции было обозначение лабиринта {0}?",
                Answers = new Dictionary<string, string>
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

            // Mazeseeker
            // How many walls surrounded this cell in {0}?
            // How many walls surrounded this cell in Mazeseeker?
            [Question.MazeseekerCell] = new()
            {
                QuestionText = "Сколько стен окружало эту клетку {0}?",
            },
            // Where was the start in {0}?
            // Where was the start in Mazeseeker?
            [Question.MazeseekerStart] = new()
            {
                QuestionText = "Где было начало {0}?",
            },
            // Where was the goal in {0}?
            // Where was the goal in Mazeseeker?
            [Question.MazeseekerGoal] = new()
            {
                QuestionText = "Где была цель {0}?",
            },

            // Maze Swap
            // Where was the {1} position in {0}?
            // Where was the starting position in Maze Swap?
            [Question.MazeSwapPosition] = new()
            {
                QuestionText = "Где {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["starting"] = "было начало",
                    ["goal"] = "была цель",
                },
            },

            // Mega Man 2
            // Which master was shown in {0}?
            // Which master was shown in Mega Man 2?
            [Question.MegaMan2Master] = new()
            {
                QuestionText = "Какой мастер был показан {0}?",
            },
            // Which weapon was shown in {0}?
            // Which weapon was shown in Mega Man 2?
            [Question.MegaMan2Weapon] = new()
            {
                QuestionText = "Какое оружие было показано {0}?",
            },

            // Melody Sequencer
            // Which part was in slot #{1} at the start of {0}?
            // Which part was in slot #1 at the start of Melody Sequencer?
            [Question.MelodySequencerSlots] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая часть была в слоту №{1} в начале {0}?",
            },
            // Which slot contained part #{1} at the start of {0}?
            // Which slot contained part #1 at the start of Melody Sequencer?
            [Question.MelodySequencerParts] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой слот содержал часть №{1} в начале {0}?",
            },

            // Memorable Buttons
            // What was the {1} correct symbol pressed in {0}?
            // What was the first correct symbol pressed in Memorable Buttons?
            [Question.MemorableButtonsSymbols] = new()
            {
                QuestionText = "Какой был {1}-й верно нажатый символ {0}?",
            },

            // Memory
            // What was the displayed number in the {1} stage of {0}?
            // What was the displayed number in the first stage of Memory?
            [Question.MemoryDisplay] = new()
            {
                Conjugation = Conjugation.GenitiveFeminine,
                QuestionText = "Какая цифра была на экране на {1}-м этапе {0}?",
                ModuleName = "Памяти",
            },
            // In what position was the button that you pressed in the {1} stage of {0}?
            // In what position was the button that you pressed in the first stage of Memory?
            [Question.MemoryPosition] = new()
            {
                Conjugation = Conjugation.GenitiveFeminine,
                QuestionText = "На какой позиции была кнопка, которую вы нажали на {1}-м этапе {0}?",
                ModuleName = "Памяти",
            },
            // What was the label of the button that you pressed in the {1} stage of {0}?
            // What was the label of the button that you pressed in the first stage of Memory?
            [Question.MemoryLabel] = new()
            {
                Conjugation = Conjugation.GenitiveFeminine,
                QuestionText = "С каким значением была кнопка, которую вы нажали на {1}-м этапе {0}?",
                ModuleName = "Памяти",
            },

            // Memory Wires
            // What was the digit displayed in the {1} stage of {0}?
            // What was the digit displayed in the first stage of Memory Wires?
            [Question.MemoryWiresDisplayedDigits] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая цифра была показана на {1}-м этапе {0}?",
            },
            // What was the colour of wire {1} in {0}?
            // What was the colour of wire 1 in Memory Wires?
            [Question.MemoryWiresWireColours] = new()
            {
                QuestionText = "Какого цвета был {1}-й провод {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Yellow"] = "Жёлтый",
                    ["Blue"] = "Синий",
                    ["White"] = "Белый",
                    ["Black"] = "Чёрный",
                },
            },

            // Metamorse
            // What was the extracted letter in {0}?
            // What was the extracted letter in Metamorse?
            [Question.MetamorseExtractedLetter] = new()
            {
                QuestionText = "Какая была извлечённая буква {0}?",
            },

            // Metapuzzle
            // What was the final answer in {0}?
            // What was the final answer in Metapuzzle?
            [Question.MetapuzzleAnswer] = new()
            {
                QuestionText = "Какой был финальный ответ {0}?",
            },

            // Mirror
            // What was the second word written by the original ghost in {0}?
            // What was the second word written by the original ghost in Mirror?
            [Question.MirrorWord] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое было второе слово, написанное оригинальным призраком на {0}?",
            },

            // Mister Softee
            // Where was the SpongeBob Bar on {0}?
            // Where was the SpongeBob Bar on Mister Softee?
            [Question.MisterSofteeSpongebobPosition] = new()
            {
                QuestionText = "Где был SpongeBob Bar {0}?",
                Answers = new Dictionary<string, string>
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
            // Which treat was present on {0}?
            // Which treat was present on Mister Softee?
            [Question.MisterSofteeTreatsPresent] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая сладость присутствовала на {0}?",
            },

            // Mixometer
            // What was the position of the submit button in {0}?
            // What was the position of the submit button in Mixometer?
            [Question.MixometerSubmitButton] = new()
            {
                QuestionText = "В какой позиции была кнопка отправки {0}?",
            },

            // Modern Cipher
            // What was the decrypted word of the {1} stage in {0}?
            // What was the decrypted word of the first stage in Modern Cipher?
            [Question.ModernCipherWord] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое слово было расшифровано на {1}-м этапе {0}?",
            },

            // Module Listening
            // Which sound did the {1} button play in {0}?
            // Which sound did the red button play in Module Listening?
            [Question.ModuleListeningButtonAudio] = new()
            {
                QuestionText = "Какой звук воспроизводился {1} кнопкой {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красной",
                    ["green"] = "зелёной",
                    ["blue"] = "синей",
                    ["yellow"] = "жёлтой",
                },
            },
            // Which sound played in {0}?
            // Which sound played in Module Listening?
            [Question.ModuleListeningAnyAudio] = new()
            {
                QuestionText = "Какой звук был воспроизведён {0}?",
            },

            // Module Maze
            // Which of the following was the starting icon for {0}?
            // Which of the following was the starting icon for Module Maze?
            [Question.ModuleMazeStartingIcon] = new()
            {
                QuestionText = "Какая была начальная иконка модуля {0}?",
                ModuleName = "Модульном лабиринте",
            },

            // Module Movements
            // What was the {1} module shown in {0}?
            // What was the first module shown in Module Movements?
            [Question.ModuleMovementsDisplay] = new()
            {
                QuestionText = "Какой был {1}-й показанный модуль {0}?",
            },

            // Money Game
            // What were the first and second words in the {1} phrase in {0}?
            // What were the first and second words in the first phrase in Money Game?
            [Question.MoneyGame1] = new()
            {
                QuestionText = "Какое было первое и второе слово в {1}-й фразе {0}?",
            },
            // What were the third and fourth words in the {1} phrase in {0}?
            // What were the third and fourth words in the first phrase in Money Game?
            [Question.MoneyGame2] = new()
            {
                QuestionText = "Какое было третье и четвёртое слово в {1}-й фразе {0}?",
            },
            // What was the end of the {1} phrase in {0}?
            // What was the end of the first phrase in Money Game?
            [Question.MoneyGame3] = new()
            {
                QuestionText = "На что оканчивалась {1}-я фраза {0}?",
            },

            // Monsplode, Fight!
            // Which creature was displayed in {0}?
            // Which creature was displayed in Monsplode, Fight!?
            [Question.MonsplodeFightCreature] = new()
            {
                QuestionText = "Какое существо было показано {0}?",
                ModuleName = "\"Монсплоды, в атаку!\"",
            },
            // Which one of these moves {1} selectable in {0}?
            // Which one of these moves was selectable in Monsplode, Fight!?
            [Question.MonsplodeFightMove] = new()
            {
                QuestionText = "Какой один из этих приёмов {1} доступен {0}?",
                ModuleName = "\"Монсплоды, в атаку!\"",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "был",
                    ["was not"] = "не был",
                },
            },

            // Monsplode Trading Cards
            // What was the {1} before the last action in {0}?
            // What was the first card in your hand before the last action in Monsplode Trading Cards?
            [Question.MonsplodeTradingCardsCards] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какая была {1} перед последним действием {0}?",
                ModuleName = "Коллекционных карточках по Монсплодам",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first card in your hand"] = "первая карта в вашей руке",
                    ["second card in your hand"] = "вторая карта в вашей руке",
                    ["third card in your hand"] = "третья карта в вашей руке",
                    ["card on offer"] = "карта на обмен",
                },
            },
            // What was the print version of the {1} before the last action in {0}?
            // What was the print version of the first card in your hand before the last action in Monsplode Trading Cards?
            [Question.MonsplodeTradingCardsPrintVersions] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какое было издание {1} перед последним действием {0}?",
                ModuleName = "Коллекционных карточках по Монсплодам",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first card in your hand"] = "первой карты в вашей руке",
                    ["second card in your hand"] = "второй карты в вашей руке",
                    ["third card in your hand"] = "третьей карты в вашей руке",
                    ["card on offer"] = "карты на обмен",
                },
            },

            // The Moon
            // What was the {1} set in clockwise order in {0}?
            // What was the first initially lit set in clockwise order in The Moon?
            [Question.MoonLitUnlit] = new()
            {
                QuestionText = "Какой {1} по часовой стрелке {0}?",
                FormatArgs = new Dictionary<string, string>
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
                Answers = new Dictionary<string, string>
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

            // More Code
            // What was the flashing word in {0}?
            // What was the flashing word in More Code?
            [Question.MoreCodeWord] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое слово передовалось на {0}?",
            },

            // Morse-A-Maze
            // What was the starting location in {0}?
            // What was the starting location in Morse-A-Maze?
            [Question.MorseAMazeStartingCoordinate] = new()
            {
                QuestionText = "Какая была начальная позиция {0}?",
            },
            // What was the ending location in {0}?
            // What was the ending location in Morse-A-Maze?
            [Question.MorseAMazeEndingCoordinate] = new()
            {
                QuestionText = "Какая была конечная позиция {0}?",
            },
            // What was the word shown as Morse code in {0}?
            // What was the word shown as Morse code in Morse-A-Maze?
            [Question.MorseAMazeMorseCodeWord] = new()
            {
                QuestionText = "Какое кодовое слово было передано через Морзе {0}?",
            },

            // Morse Buttons
            // What was the character flashed by the {1} button in {0}?
            // What was the character flashed by the first button in Morse Buttons?
            [Question.MorseButtonsButtonLabel] = new()
            {
                QuestionText = "Какой символ передавался {1}-й кнопкой {0}?",
            },
            // What was the color flashed by the {1} button in {0}?
            // What was the color flashed by the first button in Morse Buttons?
            [Question.MorseButtonsButtonColor] = new()
            {
                QuestionText = "Каким цветом мигала {1}-я кнопка {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красным",
                    ["blue"] = "Синим",
                    ["green"] = "Зелёным",
                    ["yellow"] = "Жёлтым",
                    ["orange"] = "Оранжевым",
                    ["purple"] = "Фиолетовым",
                },
            },

            // Morsematics
            // What was the {1} received letter in {0}?
            // What was the first received letter in Morsematics?
            [Question.MorsematicsReceivedLetters] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какая была {1}-я полученная буква {0}?",
                ModuleName = "Морзематике",
            },

            // Morse War
            // What were the LEDs in the {1} row in {0} (1 = on, 0 = off)?
            // What were the LEDs in the bottom row in Morse War (1 = on, 0 = off)?
            [Question.MorseWarLeds] = new()
            {
                QuestionText = "Какими были светодиоды в {1} ряду {0} (1 = вкл, 0 = выкл)?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["bottom"] = "нижнем",
                    ["middle"] = "центральном",
                    ["top"] = "верхнем",
                },
            },
            // What code was transmitted in {0}?
            // What code was transmitted in Morse War?
            [Question.MorseWarCode] = new()
            {
                QuestionText = "Какой код был передан {0}?",
            },

            // Mouse in the Maze
            // What color was the torus in {0}?
            // What color was the torus in Mouse in the Maze?
            [Question.MouseInTheMazeTorus] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какого цвета было кольцо {0}?",
                ModuleName = "Мыши в лабиринте",
                Answers = new Dictionary<string, string>
                {
                    ["white"] = "Белое",
                    ["green"] = "Зелёное",
                    ["blue"] = "Синее",
                    ["yellow"] = "Жёлтое",
                },
            },
            // Which color sphere was the goal in {0}?
            // Which color sphere was the goal in Mouse in the Maze?
            [Question.MouseInTheMazeSphere] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какого цвета была целевая сфера {0}?",
                ModuleName = "Мыши в лабиринте",
                Answers = new Dictionary<string, string>
                {
                    ["white"] = "Белая",
                    ["green"] = "Зелёная",
                    ["blue"] = "Синяя",
                    ["yellow"] = "Жёлтая",
                },
            },

            // M-Seq
            // What was the {1} obtained digit in {0}?
            // What was the first obtained digit in M-Seq?
            [Question.MSeqObtained] = new()
            {
                QuestionText = "Какая была {1}-я полученная цифра {0}?",
            },
            // What was the final number from the iteration process in {0}?
            // What was the final number from the iteration process in M-Seq?
            [Question.MSeqSubmitted] = new()
            {
                QuestionText = "Какое было финальное число итерационного процесса {0}?",
            },

            // Mssngv Wls
            // Which vowel was missing in {0}?
            // Which vowel was missing in \uE001Mssngv Wls\uE002?
            [Question.MssngvWlsMssNgvwL] = new()
            {
                QuestionText = "Какая гласная отсутствовала {0}?",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["AEIOU"] = "АИОУЭЯЫЁЮЕ",
                },
            },

            // Multicolored Switches
            // What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?
            // What color was the first LED on the top row when the tiny LED was lit in Multicolored Switches?
            [Question.MulticoloredSwitchesLedColor] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какого цвета был {1}-й светодиод на {2} ряду, когда маленький светодиод {3} {0}?",
                ModuleName = "Многоцветных переключателях",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["lit"] = "горел",
                    ["bottom"] = "нижнем",
                    ["unlit"] = "не горел",
                },
                Answers = new Dictionary<string, string>
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

            // Murder
            // Where was the body found in {0}?
            // Where was the body found in Murder?
            [Question.MurderBodyFound] = new()
            {
                QuestionText = "Где было найдено тело {0}?",
                ModuleName = "Убийстве",
                Answers = new Dictionary<string, string>
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
            // Which of these was {1} in {0}?
            // Which of these was a suspect but not the murderer in Murder?
            [Question.MurderSuspect] = new()
            {
                QuestionText = "Кто {1} {0}?",
                ModuleName = "Убийстве",
                FormatArgs = new Dictionary<string, string>
                {
                    ["a suspect but not the murderer"] = "не являлся убийцей, но был среди подозреваемых",
                    ["not a suspect"] = "не был подозреваемым",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Miss Scarlett"] = "Miss Scarlett",
                    ["Professor Plum"] = "Professor Plum",
                    ["Mrs Peacock"] = "Mrs Peacock",
                    ["Reverend Green"] = "Reverend Green",
                    ["Colonel Mustard"] = "Colonel Mustard",
                    ["Mrs White"] = "Mrs White",
                },
            },
            // Which of these was {1} in {0}?
            // Which of these was a potential weapon but not the murder weapon in Murder?
            [Question.MurderWeapon] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "{1} {0}?",
                ModuleName = "Убийства",
                FormatArgs = new Dictionary<string, string>
                {
                    ["a potential weapon but not the murder weapon"] = "Какое орудие было найдено, но не являлось орудием убийства",
                    ["not a potential weapon"] = "Какого орудия не было среди найденных возможных орудий",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Candlestick"] = "Candlestick",
                    ["Dagger"] = "Dagger",
                    ["Lead Pipe"] = "Lead Pipe",
                    ["Revolver"] = "Revolver",
                    ["Rope"] = "Rope",
                    ["Spanner"] = "Spanner",
                },
            },

            // Mystery Module
            // Which module was the first requested to be solved by {0}?
            // Which module was the first requested to be solved by Mystery Module?
            [Question.MysteryModuleFirstKey] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой модуль надо было обезвредить первым на {0}?",
            },
            // Which module was hidden by {0}?
            // Which module was hidden by Mystery Module?
            [Question.MysteryModuleHiddenModule] = new()
            {
                Conjugation = Conjugation.InstrumentalMascNeuter,
                QuestionText = "Какой модуль был спрятан за {0}?",
            },

            // Mystic Square
            // Where was the skull in {0}?
            // Where was the skull in Mystic Square?
            [Question.MysticSquareSkull] = new()
            {
                QuestionText = "Где находился череп {0}?",
                ModuleName = "Загадочном квадрате",
                Answers = new Dictionary<string, string>
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

            // Name Codes
            // What was the {1} index in {0}?
            // What was the left index in Name Codes?
            [Question.NameCodesIndices] = new()
            {
                QuestionText = "Какой был {1} индекс {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левый",
                    ["right"] = "правый",
                },
            },

            // Naming Conventions
            // What was the label of the first button in {0}?
            // What was the label of the first button in Naming Conventions?
            [Question.NamingConventionsObject] = new()
            {
                QuestionText = "Какая была надпись на 1й кнопке {0}?",
            },

            // N&Ms
            // What was the label of the correct button in {0}?
            // What was the label of the correct button in N&Ms?
            [Question.NandMsAnswer] = new()
            {
                QuestionText = "Какая надпись была на правильной кнопке {0}?",
                ModuleName = "N и M",
            },

            // N&Ns
            // Which label was present in the {1} stage of {0}?
            // Which label was present in the first stage of N&Ns?
            [Question.NandNsLabel] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая надпись присутствовала на {1}-м этапе {0}?",
            },
            // Which color was missing in the third stage of {0}?
            // Which color was missing in the third stage of N&Ns?
            [Question.NandNsColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой цвет отсутствовал на 3-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Green"] = "Зелёный",
                    ["Orange"] = "Оранжевый",
                    ["Blue"] = "Синий",
                    ["Yellow"] = "Жёлтый",
                    ["Brown"] = "Коричневый",
                },
            },

            // Navigation Determination
            // What was the color of the maze in {0}?
            // What was the color of the maze in Navigation Determination?
            [Question.NavigationDeterminationColor] = new()
            {
                QuestionText = "Какого цвета был лабиринт {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красного",
                    ["Yellow"] = "Жёлтого",
                    ["Green"] = "Зелёного",
                    ["Blue"] = "Синего",
                },
            },
            // What was the label of the maze in {0}?
            // What was the label of the maze in Navigation Determination?
            [Question.NavigationDeterminationLabel] = new()
            {
                QuestionText = "Какой буквой был обозначен лабиринт {0}?",
            },

            // Navinums
            // What was the initial middle digit in {0}?
            // What was the initial middle digit in Navinums?
            [Question.NavinumsMiddleDigit] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая цифра была изначально в центре {0}?",
            },
            // What was the {1} directional button pressed in {0}?
            // What was the first directional button pressed in Navinums?
            [Question.NavinumsDirectionalButtons] = new()
            {
                QuestionText = "Какая кнопка направления была нажата {1}-й {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["up"] = "вверх",
                    ["left"] = "влево",
                    ["right"] = "вправо",
                    ["down"] = "вниз",
                },
            },

            // The Navy Button
            // Which Greek letter appeared on {0} (case-sensitive)?
            // Which Greek letter appeared on The Navy Button (case-sensitive)?
            [Question.NavyButtonGreekLetters] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая греческая буква (с учётом регистра) появилась на {0}?",
            },
            // What was the {1} of the given in {0} (0-indexed)?
            // What was the column of the given in The Navy Button (0-indexed)?
            [Question.NavyButtonGiven] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "{1} (с индексом 0) на {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["column"] = "Какой столбец был указан",
                    ["row"] = "Какая строка была указана",
                    ["value"] = "Какое значение было указано",
                },
            },

            // The Necronomicon
            // What was the chapter number of the {1} page in {0}?
            // What was the chapter number of the first page in The Necronomicon?
            [Question.NecronomiconChapters] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был номер главы {1}-й страницы {0}?",
            },

            // Negativity
            // In base 10, what was the value submitted in {0}?
            // In base 10, what was the value submitted in Negativity?
            [Question.NegativitySubmittedValue] = new()
            {
                QuestionText = "Какое значение было введено (в десятиричной системе) {0}?",
            },
            // Excluding 0s, what was the submitted balanced ternary in {0}?
            // Excluding 0s, what was the submitted balanced ternary in Negativity?
            [Question.NegativitySubmittedTernary] = new()
            {
                QuestionText = "Какой сбалансированный троичный код был введён {0} (исключая нули)?",
            },

            // Neptune
            // Which star was displayed in {0}?
            // Which star was displayed in Neptune?
            [Question.NeptuneStar] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which star was displayed in {0}?",
            },

            // Neutralization
            // What was the acid’s color in {0}?
            // What was the acid’s color in Neutralization?
            [Question.NeutralizationColor] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какой цвет был у кислоты {0}?",
                ModuleName = "Нейтрализации",
                Answers = new Dictionary<string, string>
                {
                    ["Yellow"] = "Жёлтый",
                    ["Green"] = "Зелёный",
                    ["Red"] = "Красный",
                    ["Blue"] = "Синий",
                },
            },
            // What was the acid’s volume in {0}?
            // What was the acid’s volume in Neutralization?
            [Question.NeutralizationVolume] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какой был объём кислоты {0}?",
                ModuleName = "Нейтрализации",
            },

            // Next In Line
            // What color was the first wire in {0}?
            // What color was the first wire in Next In Line?
            [Question.NextInLineFirstWire] = new()
            {
                QuestionText = "Какого цвета был первый провод {0}?",
                Answers = new Dictionary<string, string>
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

            // ❖
            // Which button flashed in the {1} stage in {0}?
            // Which button flashed in the first stage in ❖?
            // Note: This question is depicted visually, rather than with words. A translation here will only be used for logging.
            [Question.NonverbalSimonFlashes] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой кнопка горела на {1}-м этапе {0}?",
            },

            // Not Colored Squares
            // What was the position of the square you initially pressed in {0}?
            // What was the position of the square you initially pressed in Not Colored Squares?
            [Question.NotColoredSquaresInitialPosition] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая была позиция квадрата, который вы изначально нажали на {0}?",
            },

            // Not Colored Switches
            // What was the encrypted word in {0}?
            // What was the encrypted word in Not Colored Switches?
            [Question.NotColoredSwitchesWord] = new()
            {
                QuestionText = "Какое было зашифрованное слово {0}?",
            },

            // Not Colour Flash
            // What was {1} in the displayed word sequence in {0}?
            // What was first in the displayed word sequence in Not Colour Flash?
            [Question.NotColourFlashInitialWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial word on {0}?",
            },
            // What was {1} in the displayed colour sequence in {0}?
            // What was first in the displayed colour sequence in Not Colour Flash?
            [Question.NotColourFlashInitialColour] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial colour of the word on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                    ["White"] = "White",
                },
            },

            // Not Connection Check
            // What symbol flashed on the {1} button in {0}?
            // What symbol flashed on the top left button in Not Connection Check?
            [Question.NotConnectionCheckFlashes] = new()
            {
                QuestionText = "Какой символ мигал на {1} кнопке {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top left"] = "верхней левой",
                    ["top right"] = "верхней правой",
                    ["bottom left"] = "нижней левой",
                    ["bottom right"] = "нижней правой",
                },
            },
            // What was the value of the {1} button in {0}?
            // What was the value of the top left button in Not Connection Check?
            [Question.NotConnectionCheckValues] = new()
            {
                QuestionText = "Какое было значение {1} кнопки {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top left"] = "верхней левой",
                    ["top right"] = "верхней правой",
                    ["bottom left"] = "нижней левой",
                    ["bottom right"] = "нижней правой",
                },
            },

            // Not Coordinates
            // Which coordinate was part of the square in {0}?
            // Which coordinate was part of the square in Not Coordinates?
            [Question.NotCoordinatesSquareCoords] = new()
            {
                QuestionText = "Какая координата была частью квадрата {0}?",
            },

            // Not Double-Oh
            // What was the {1} displayed position in the second stage of {0}?
            // What was the first displayed position in the second stage of Not Double-Oh?
            [Question.NotDoubleOhPosition] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая позиция была показана {1}-й на втором этапе {0}?",
            },

            // Not Keypad
            // What color flashed {1} in the final sequence in {0}?
            // What color flashed first in the final sequence in Not Keypad?
            [Question.NotKeypadColor] = new()
            {
                Conjugation = Conjugation.PrepositiveFeminine,
                QuestionText = "Какой цвет горел {1}-м в последовательности на {0}?",
                ModuleName = "Не клавиатуре",
                Answers = new Dictionary<string, string>
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
            // Which symbol was on the button that flashed {1} in the final sequence in {0}?
            // Which symbol was on the button that flashed first in the final sequence in Not Keypad?
            [Question.NotKeypadSymbol] = new()
            {
                Conjugation = Conjugation.PrepositiveFeminine,
                QuestionText = "Какой символ был на кнопке, которая горела {1}-й в последовательности на {0}?",
                ModuleName = "Не клавиатуре",
            },

            // Not Maze
            // What was the starting distance in {0}?
            // What was the starting distance in Not Maze?
            [Question.NotMazeStartingDistance] = new()
            {
                QuestionText = "Какая была начальная дистанция {0}?",
            },

            // Not Morse Code
            // What was the {1} correct word you submitted in {0}?
            // What was the first correct word you submitted in Not Morse Code?
            [Question.NotMorseCodeWord] = new()
            {
                QuestionText = "Какое было {1}-е верное слово, которое вы отправили {0}?",
            },

            // Not Morsematics
            // What was the transmitted word on {0}?
            // What was the transmitted word on Not Morsematics?
            [Question.NotMorsematicsWord] = new()
            {
                QuestionText = "Какое слово было передано {0}?",
            },

            // Not Murder
            // What room was {1} in initially on {0}?
            // What room was Miss Scarlett in initially on Not Murder?
            [Question.NotMurderRoom] = new()
            {
                QuestionText = "В какой комнате изначально находился(-ась) {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Miss Scarlett"] = "Miss Scarlett",
                    ["Colonel Mustard"] = "Colonel Mustard",
                    ["Reverend Green"] = "Reverend Green",
                    ["Mrs Peacock"] = "Mrs Peacock",
                    ["Professor Plum"] = "Professor Plum",
                    ["Mrs White"] = "Mrs White",
                },
                Answers = new Dictionary<string, string>
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
            // What weapon did {1} possess initially on {0}?
            // What weapon did Miss Scarlett possess initially on Not Murder?
            [Question.NotMurderWeapon] = new()
            {
                QuestionText = "Каким орудием изначально обладал(-а) {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Miss Scarlett"] = "Miss Scarlett",
                    ["Colonel Mustard"] = "Colonel Mustard",
                    ["Reverend Green"] = "Reverend Green",
                    ["Mrs Peacock"] = "Mrs Peacock",
                    ["Professor Plum"] = "Professor Plum",
                    ["Mrs White"] = "Mrs White",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Candlestick"] = "Candlestick",
                    ["Dagger"] = "Dagger",
                    ["Lead Pipe"] = "Lead Pipe",
                    ["Revolver"] = "Revolver",
                    ["Rope"] = "Rope",
                    ["Spanner"] = "Spanner",
                },
            },

            // Not Number Pad
            // Which of these numbers {1} at the {2} stage of {0}?
            // Which of these numbers flashed at the first stage of Not Number Pad?
            [Question.NotNumberPadFlashes] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая их этих цифр {1} на {2}-м этапе {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["flashed"] = "мигала",
                    ["did not flash"] = "не мигала",
                },
            },

            // Not Password
            // Which letter was missing from {0}?
            // Which letter was missing from Not Password?
            [Question.NotPasswordLetter] = new()
            {
                QuestionText = "Какая буква отсутствовала {0}?",
            },

            // Not Perspective Pegs
            // What was the position of the {1} flashing peg on {0}?
            // What was the position of the first flashing peg on Not Perspective Pegs?
            [Question.NotPerspectivePegsPosition] = new()
            {
                QuestionText = "В какой позиции находился {1}-й мигающий колышек {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["top"] = "Сверху",
                    ["top-right"] = "Сверху справа",
                    ["bottom-right"] = "Снизу справа",
                    ["bottom-left"] = "Снизу слева",
                    ["top-left"] = "Сверху слева",
                },
            },
            // From what perspective did the {1} peg flash on {0}?
            // From what perspective did the first peg flash on Not Perspective Pegs?
            [Question.NotPerspectivePegsPerspective] = new()
            {
                QuestionText = "С какого ракурса мигнул {1}-й колышек {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["top"] = "С верхнего",
                    ["top-right"] = "С верх. правого",
                    ["bottom-right"] = "С ниж. правого",
                    ["bottom-left"] = "С ниж. левого",
                    ["top-left"] = "С верх. левого",
                },
            },
            // What was the color of the {1} flashing peg on {0}?
            // What was the color of the first flashing peg on Not Perspective Pegs?
            [Question.NotPerspectivePegsColor] = new()
            {
                QuestionText = "Какого цвета был {1}-й мигающий колышек {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "Синего",
                    ["green"] = "Зелёного",
                    ["purple"] = "Фиолетового",
                    ["red"] = "Красного",
                    ["yellow"] = "Жёлтого",
                },
            },

            // Not Piano Keys
            // What was the first displayed symbol on {0}?
            // What was the first displayed symbol on Not Piano Keys?
            [Question.NotPianoKeysFirstSymbol] = new()
            {
                QuestionText = "Какой был первый изображённый символ {0}?",
            },
            // What was the second displayed symbol on {0}?
            // What was the second displayed symbol on Not Piano Keys?
            [Question.NotPianoKeysSecondSymbol] = new()
            {
                QuestionText = "Какой был второй изображённый символ {0}?",
            },
            // What was the third displayed symbol on {0}?
            // What was the third displayed symbol on Not Piano Keys?
            [Question.NotPianoKeysThirdSymbol] = new()
            {
                QuestionText = "Какой был третий изображённый символ {0}?",
            },

            // Not Red Arrows
            // What was the starting number in {0}?
            // What was the starting number in Not Red Arrows?
            [Question.NotRedArrowsStart] = new()
            {
                QuestionText = "Какое было исходное число {0}?",
            },

            // Not Simaze
            // Which maze was used in {0}?
            // Which maze was used in Not Simaze?
            [Question.NotSimazeMaze] = new()
            {
                QuestionText = "Какой лабиринт был использован {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красный",
                    ["orange"] = "Оранжевый",
                    ["yellow"] = "Жёлтый",
                    ["green"] = "Зелёный",
                    ["blue"] = "Синий",
                    ["purple"] = "Фиолетовый",
                },
            },
            // What was the starting position in {0}?
            // What was the starting position in Not Simaze?
            [Question.NotSimazeStart] = new()
            {
                QuestionText = "Какая была начальная позиция {0}?",
                Answers = new Dictionary<string, string>
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
            // What was the goal position in {0}?
            // What was the goal position in Not Simaze?
            [Question.NotSimazeGoal] = new()
            {
                QuestionText = "Какая была конечная позиция {0}?",
                Answers = new Dictionary<string, string>
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

            // Not Text Field
            // Which letter was pressed in the first stage of {0}?
            // Which letter was pressed in the first stage of Not Text Field?
            [Question.NotTextFieldInitialPresses] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая буква была нажата на первом этапе на {0}?",
            },
            // Which letter appeared 9 times at the start of {0}?
            // Which letter appeared 9 times at the start of Not Text Field?
            [Question.NotTextFieldBackgroundLetter] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая буква появилась 9 раз в начале на {0}?",
            },

            // Not The Bulb
            // What word flashed on {0}?
            // What word flashed on Not The Bulb?
            [Question.NotTheBulbWord] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое слово мигало на {0}?",
            },
            // What color was the bulb on {0}?
            // What color was the bulb on Not The Bulb?
            [Question.NotTheBulbColor] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какого цвета была лампочка на {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красная",
                    ["Green"] = "Зелёная",
                    ["Blue"] = "Синяя",
                    ["Yellow"] = "Жёлтая",
                    ["Purple"] = "Фиолетовая",
                    ["White"] = "Белая",
                },
            },
            // What was the material of the screw cap on {0}?
            // What was the material of the screw cap on Not The Bulb?
            [Question.NotTheBulbScrewCap] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Из какого материала был сделан цоколь лампочки на {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Copper"] = "Медь",
                    ["Silver"] = "Серебро",
                    ["Gold"] = "Золото",
                    ["Plastic"] = "Пластик",
                    ["Carbon Fibre"] = "Углеволокно",
                    ["Ceramic"] = "Керамика",
                },
            },

            // Not the Button
            // What colors did the light glow in {0}?
            // What colors did the light glow in Not the Button?
            [Question.NotTheButtonLightColor] = new()
            {
                Conjugation = Conjugation.GenitiveFeminine,
                QuestionText = "Какими цветами горела цветная полоска {0}?",
                ModuleName = "Не кнопки",
                Answers = new Dictionary<string, string>
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

            // Not The Plunger Button
            // What color did the background flash in {0}?
            // What color did the background flash in Not The Plunger Button?
            [Question.NotThePlungerButtonBackground] = new()
            {
                QuestionText = "Каким цветом мигал фон {0}?",
                Answers = new Dictionary<string, string>
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

            // Not the Screw
            // What was the initial position in {0}?
            // What was the initial position in Not the Screw?
            [Question.NotTheScrewInitialPosition] = new()
            {
                QuestionText = "Какая была начальная позиция {0}?",
            },

            // Not Who’s on First
            // In which position was the button you pressed in the {1} stage on {0}?
            // In which position was the button you pressed in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstPressedPosition] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "На какой позиции была кнопка, которую вы нажали на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "сверху слева",
                    ["top right"] = "сверху справа",
                    ["middle left"] = "посередине слева",
                    ["middle right"] = "посередине справа",
                    ["bottom left"] = "снизу слева",
                    ["bottom right"] = "снизу справа",
                },
            },
            // What was the label on the button you pressed in the {1} stage on {0}?
            // What was the label on the button you pressed in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstPressedLabel] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Что было написано на кнопке, которую вы нажали на {1}-м этапе {0}?",
            },
            // In which position was the reference button in the {1} stage on {0}?
            // In which position was the reference button in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstReferencePosition] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "На какой позиции была кнопка-ссылка на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "сверху слева",
                    ["top right"] = "сверху справа",
                    ["middle left"] = "посередине слева",
                    ["middle right"] = "посередине справа",
                    ["bottom left"] = "снизу слева",
                    ["bottom right"] = "снизу справа",
                },
            },
            // What was the label on the reference button in the {1} stage on {0}?
            // What was the label on the reference button in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstReferenceLabel] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Что было написано на кнопке-ссылке на {1}-м этапе {0}?",
            },
            // What was the calculated number in the second stage on {0}?
            // What was the calculated number in the second stage on Not Who’s on First?
            [Question.NotWhosOnFirstSum] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое было рассчитанное число на втором этапе {0}?",
            },

            // Not Word Search
            // Which of these consonants was missing in {0}?
            // Which of these consonants was missing in Not Word Search?
            [Question.NotWordSearchMissing] = new()
            {
                QuestionText = "Какая из этих согласных букв отсутствовала {0}?",
            },
            // What was the first correctly pressed letter in {0}?
            // What was the first correctly pressed letter in Not Word Search?
            [Question.NotWordSearchFirstPress] = new()
            {
                QuestionText = "Какая была первая правильно нажатая буква {0}?",
            },

            // Not X01
            // Which sector value {1} present on {0}?
            // Which sector value was present on Not X01?
            [Question.NotX01SectorValues] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое значение сектора {1} на {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "присутствовало",
                    ["was not"] = "отсутствовало",
                },
            },

            // Not X-Ray
            // What table were we in in {0} (numbered 1–8 in reading order in the manual)?
            // What table were we in in Not X-Ray (numbered 1–8 in reading order in the manual)?
            [Question.NotXRayTable] = new()
            {
                QuestionText = "В какой таблице вы находились {0} (пронумерованных от 1 до 8 в порядке чтения в инструкции)?",
            },
            // What direction was button {1} in {0}?
            // What direction was button 1 in Not X-Ray?
            [Question.NotXRayDirections] = new()
            {
                QuestionText = "За какое направление отвечала кнопка \"{1}\" {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Вверх",
                    ["Right"] = "Вправо",
                    ["Down"] = "Вних",
                    ["Left"] = "Влево",
                },
            },
            // Which button went {1} in {0}?
            // Which button went up in Not X-Ray?
            [Question.NotXRayButtons] = new()
            {
                QuestionText = "Какая кнопка отвечала за направление \"{1}\" {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["up"] = "вверх",
                    ["right"] = "вправо",
                    ["down"] = "вниз",
                    ["left"] = "влево",
                },
            },
            // What was the scanner color in {0}?
            // What was the scanner color in Not X-Ray?
            [Question.NotXRayScannerColor] = new()
            {
                QuestionText = "Какой был цвет сканера {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Yellow"] = "Жёлтый",
                    ["Blue"] = "Синий",
                    ["White"] = "Белый",
                },
            },

            // Numbered Buttons
            // Which number was correctly pressed on {0}?
            // Which number was correctly pressed on Numbered Buttons?
            [Question.NumberedButtonsButtons] = new()
            {
                QuestionText = "Какое было правильно нажатое число {0}?",
            },

            // The Number Game
            // What was the maximum number in {0}?
            // What was the maximum number in The Number Game?
            [Question.NumberGameMaximum] = new()
            {
                QuestionText = "Какое было максимальное число {0}?",
            },

            // Numbers
            // What two-digit number was given in {0}?
            // What two-digit number was given in Numbers?
            [Question.NumbersTwoDigit] = new()
            {
                QuestionText = "Какое двухзначное число было дано {0}?",
            },

            // Numpath
            // What was the color of the number on {0}?
            // What was the color of the number on Numpath?
            [Question.NumpathColor] = new()
            {
                QuestionText = "Какого цвета было число {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Orange"] = "Оранжевый",
                    ["Yellow"] = "Жёлтый",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                    ["Purple"] = "Фиолетовый",
                },
            },
            // What was the number displayed on {0}?
            // What was the number displayed on Numpath?
            [Question.NumpathDigit] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое число было показано на {0}?",
            },

            // Object Shows
            // Which of these was a contestant on {0}?
            // Which of these was a contestant on Object Shows?
            [Question.ObjectShowsContestants] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Кто среди этих участников присутствовал на {0}?",
                ModuleName = "Обджект-шоу",
            },

            // The Octadecayotton
            // What was the starting sphere in {0}?
            // What was the starting sphere in The Octadecayotton?
            [Question.OctadecayottonSphere] = new()
            {
                QuestionText = "Какая была начальная сфера {0}?",
            },
            // What was one of the subrotations in the {1} rotation in {0}?
            // What was one of the subrotations in the first rotation in The Octadecayotton?
            [Question.OctadecayottonRotations] = new()
            {
                QuestionText = "Каким было одно из промежуточных вращений в {1}-м вращении {0}?",
            },

            // Odd One Out
            // What was the button you pressed in the {1} stage of {0}?
            // What was the button you pressed in the first stage of Odd One Out?
            [Question.OddOneOutButton] = new()
            {
                QuestionText = "Какую кнопку вы нажали на {1}-м этапе {0}?",
            },

            // Off Keys
            // Which of these keys played at an incorrect pitch in {0}?
            // Which of these keys played at an incorrect pitch in Off Keys?
            [Question.OffKeysIncorrectPitch] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these keys played at an incorrect pitch in {0}?",
            },
            // Which of these runes was displayed in {0}?
            // Which of these runes was displayed in Off Keys?
            [Question.OffKeysRunes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these runes was displayed in {0]?",
            },

            // Old AI
            // What was the {1} of the numbers shown in {0}?
            // What was the group of the numbers shown in Old AI?
            [Question.OldAIGroup] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая {1} чисел была показана на {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["group"] = "группа",
                    ["sub-group"] = "подгруппа",
                },
            },

            // Old Fogey
            // What was the initial color of the status light in {0}?
            // What was the initial color of the status light in Old Fogey?
            [Question.OldFogeyStartingColor] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой был исходный цвет индикатора на {0}?",
                Answers = new Dictionary<string, string>
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

            // One Links To All
            // What was the starting article in {0}?
            // What was the starting article in One Links To All?
            [Question.OneLinksToAllStart] = new()
            {
                QuestionText = "Какая была начальная статья {0}?",
            },
            // What was the ending article in {0}?
            // What was the ending article in One Links To All?
            [Question.OneLinksToAllEnd] = new()
            {
                QuestionText = "Какая была последняя статья {0}?",
            },

            // Only Connect
            // Which Egyptian hieroglyph was in the {1} in {0}?
            // Which Egyptian hieroglyph was in the top left in Only Connect?
            [Question.OnlyConnectHieroglyphs] = new()
            {
                QuestionText = "Какой египетский иероглиф был {1} {0}?",
                ModuleName = "\"Лишь Соедините!\"",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top left"] = "слева сверху",
                    ["top middle"] = "сверху посередине",
                    ["top right"] = "справа сверху",
                    ["bottom left"] = "слева снизу",
                    ["bottom middle"] = "снизу посередине",
                    ["bottom right"] = "справа снизу",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Two Reeds"] = "Два тростника",
                    ["Lion"] = "Лев",
                    ["Twisted Flax"] = "Скрученный лён",
                    ["Horned Viper"] = "Рогатая гадюка",
                    ["Water"] = "Вода",
                    ["Eye of Horus"] = "Глаз Гора",
                },
            },

            // Orange Arrows
            // What was the {1} arrow on the display of the {2} stage of {0}?
            // What was the first arrow on the display of the first stage of Orange Arrows?
            [Question.OrangeArrowsSequences] = new()
            {
                Conjugation = Conjugation.GenitivePlural,
                QuestionText = "Какая была {1}-я стрелка на экране на {2}-м этапе {0}?",
                ModuleName = "Оранжевых стрелок",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Вверх",
                    ["Right"] = "Вправо",
                    ["Down"] = "Вниз",
                    ["Left"] = "Влево",
                },
            },

            // Orange Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Orange Cipher?
            [Question.OrangeCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Ordered Keys
            // What color was this key in the {1} stage of {0}?
            // What color was this key in the first stage of Ordered Keys?
            [Question.OrderedKeysColors] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была эта клавиша на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Blue"] = "Blue",
                    ["Green"] = "Green",
                    ["Yellow"] = "Yellow",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                },
            },
            // What was the label of this key in the {1} stage of {0}?
            // What was the label of this key in the first stage of Ordered Keys?
            [Question.OrderedKeysLabels] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была надпись на этой клавише на {1}-м этапе {0}?",
            },
            // What color was the label of this key in the {1} stage of {0}?
            // What color was the label of this key in the first stage of Ordered Keys?
            [Question.OrderedKeysLabelColors] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была надпись на этой клавише на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Blue"] = "Blue",
                    ["Green"] = "Green",
                    ["Yellow"] = "Yellow",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                },
            },

            // Order Picking
            // What was the order ID in the {1} order of {0}?
            // What was the order ID in the first order of Order Picking?
            [Question.OrderPickingOrder] = new()
            {
                QuestionText = "Какой был ID у {1}-го заказа {0}?",
            },
            // What was the product ID in the {1} order of {0}?
            // What was the product ID in the first order of Order Picking?
            [Question.OrderPickingProduct] = new()
            {
                QuestionText = "Какой был ID продукта в {1}-м заказе {0}?",
            },
            // What was the pallet in the {1} order of {0}?
            // What was the pallet in the first order of Order Picking?
            [Question.OrderPickingPallet] = new()
            {
                QuestionText = "Какой был паллет на {1}-м заказе {0}?",
            },

            // Orientation Cube
            // What was the observer’s initial position in {0}?
            // What was the observer’s initial position in Orientation Cube?
            [Question.OrientationCubeInitialObserverPosition] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какая была начальная позиция у наблюдателя {0}?",
                ModuleName = "Ориентации куба",
                Answers = new Dictionary<string, string>
                {
                    ["front"] = "Спереди",
                    ["left"] = "Слева",
                    ["back"] = "Сзади",
                    ["right"] = "Справа",
                },
            },

            // Orientation Hypercube
            // What was the observer’s initial position in {0}?
            // What was the observer’s initial position in Orientation Hypercube?
            [Question.OrientationHypercubeInitialObserverPosition] = new()
            {
                QuestionText = "Какая была начальная позиция у наблюдателя {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["front"] = "Спереди",
                    ["left"] = "Слева",
                    ["back"] = "Сзади",
                    ["right"] = "Справа",
                },
            },
            // What was the initial colour of the {1} face in {0}?
            // What was the initial colour of the right face in Orientation Hypercube?
            [Question.OrientationHypercubeInitialFaceColour] = new()
            {
                QuestionText = "Какой был начальный цвет {1} {0}?",
                FormatArgs = new Dictionary<string, string>
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
                Answers = new Dictionary<string, string>
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

            // Palindromes
            // What was {1}’s {2} digit from the right in {0}?
            // What was X’s first digit from the right in Palindromes?
            [Question.PalindromesNumbers] = new()
            {
                QuestionText = "Какая была {2}-я цифра справа {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["X"] = "у X",
                    ["Y"] = "у Y",
                    ["Z"] = "у Z",
                    ["the screen"] = "на экране",
                },
            },

            // Papa’s Pizzeria
            // What was the {1} digit in the order number on {0}?
            // What was the first digit in the order number on Papa’s Pizzeria?
            [Question.PapasPizzeriaDigit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} digit in the order number on {0}?",
            },
            // What was the letter in the order number on {0}?
            // What was the letter in the order number on Papa’s Pizzeria?
            [Question.PapasPizzeriaLetter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the letter in the order number on {0}?",
            },

            // Parity
            // What was shown on the display on {0}?
            // What was shown on the display on Parity?
            [Question.ParityDisplay] = new()
            {
                QuestionText = "Что было показано на экране {0}?",
            },

            // Partial Derivatives
            // What was the LED color in the {1} stage of {0}?
            // What was the LED color in the first stage of Partial Derivatives?
            [Question.PartialDerivativesLedColors] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был цвет светодиода на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "синий",
                    ["green"] = "зелёный",
                    ["orange"] = "оранжевый",
                    ["purple"] = "фиолетовый",
                    ["red"] = "красный",
                    ["yellow"] = "жёлтый",
                },
            },
            // What was the {1} term in {0}?
            // What was the first term in Partial Derivatives?
            [Question.PartialDerivativesTerms] = new()
            {
                QuestionText = "Какой был {1}-й член {0}?",
            },

            // Passport Control
            // What was the passport expiration year of the {1} inspected passenger in {0}?
            // What was the passport expiration year of the first inspected passenger in Passport Control?
            [Question.PassportControlPassenger] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой был год истечения паспорта у {1}-го пассажира на {0}?",
            },

            // Password Destroyer
            // What was the 2FAST™ value when you solved {0}?
            // What was the 2FAST™ value when you solved Password Destroyer?
            [Question.PasswordDestroyerTwoFactorV2] = new()
            {
                Conjugation = Conjugation.AccusativeMascNeuter,
                QuestionText = "Чему был равен 2FAST™ когда вы обезвредили {0}?",
            },

            // Pattern Cube
            // Which symbol was highlighted in {0}?
            // Which symbol was highlighted in Pattern Cube?
            [Question.PatternCubeHighlightedSymbol] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какой символ был подсвечен {0}?",
                ModuleName = "Развёртке куба",
            },

            // The Pentabutton
            // What was the base colour in {0}?
            // What was the base colour in The Pentabutton?
            [Question.PentabuttonBaseColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был цвет у основания {0}?",
                Answers = new Dictionary<string, string>
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

            // Periodic Words
            // What word was on the display in the {1} stage of {0}?
            // What word was on the display in the first stage of Periodic Words?
            [Question.PeriodicWordsDisplayedWords] = new()
            {
                QuestionText = "Какое слово было на экране в {1}-м этапе {0}?",
            },

            // Perspective Pegs
            // What was the {1} color in the initial sequence in {0}?
            // What was the first color in the initial sequence in Perspective Pegs?
            [Question.PerspectivePegsColorSequence] = new()
            {
                Conjugation = Conjugation.во_PrepositiveMascNeuter,
                QuestionText = "Какой цвет был {1}-м в начальной последовательности {0}?",
                ModuleName = "Взгляде на колышках",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красный",
                    ["yellow"] = "Жёлтый",
                    ["green"] = "Зелёный",
                    ["blue"] = "Синий",
                    ["purple"] = "Фиолетовый",
                },
            },

            // Phosphorescence
            // What was the offset in {0}?
            // What was the offset in Phosphorescence?
            [Question.PhosphorescenceOffset] = new()
            {
                QuestionText = "Какое было смещение {0}?",
            },
            // What was the {1} button press in {0}?
            // What was the first button press in Phosphorescence?
            [Question.PhosphorescenceButtonPresses] = new()
            {
                QuestionText = "Какая была {1}-я нажатая кнопка {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Azure"] = "Azure",
                    ["Blue"] = "Blue",
                    ["Crimson"] = "Crimson",
                    ["Diamond"] = "Diamond",
                    ["Emerald"] = "Emerald",
                    ["Fuchsia"] = "Fuchsia",
                    ["Green"] = "Green",
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

            // Pickup Identification
            // What pickup was shown in the {1} stage of {0}?
            // What pickup was shown in the first stage of Pickup Identification?
            [Question.PickupIdentificationItem] = new()
            {
                QuestionText = "Какой предмет был показан на {1}-м этапе {0}?",
            },

            // Pictionary
            // What was the code in {0}?
            // What was the code in Pictionary?
            [Question.PictionaryCode] = new()
            {
                QuestionText = "Какой был код {0}?",
            },

            // Pie
            // What was the {1} digit of the displayed number in {0}?
            // What was the first digit of the displayed number in Pie?
            [Question.PieDigits] = new()
            {
                QuestionText = "Какая была {1}-я цифра числа, показанного {0}?",
            },

            // Pie Flash
            // What number was not displayed in {0}?
            // What number was not displayed in Pie Flash?
            [Question.PieFlashDigits] = new()
            {
                QuestionText = "Какое число не было показано {0}?",
            },

            // Pigpen Cycle
            // Which direction was the {1} dial pointing in {0}?
            // Which direction was the first dial pointing in Pigpen Cycle?
            [Question.PigpenCycleDialDirections] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which direction was the {1} dial pointing in {0}?",
            },
            // What letter was written on the {1} dial in {0}?
            // What letter was written on the first dial in Pigpen Cycle?
            [Question.PigpenCycleDialLabels] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What letter was written on the {1} dial in {0}?",
            },

            // The Pink Button
            // What was the {1} word in {0}?
            // What was the first word in The Pink Button?
            [Question.PinkButtonWords] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое было {1}-е слово {0}?",
            },
            // What was the {1} color in {0}?
            // What was the first color in The Pink Button?
            [Question.PinkButtonColors] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой был {1}-й цвет на {0}?",
                Answers = new Dictionary<string, string>
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

            // Pixel Cipher
            // What was the keyword in {0}?
            // What was the keyword in Pixel Cipher?
            [Question.PixelCipherKeyword] = new()
            {
                QuestionText = "Какое было ключевое слово {0}?",
            },

            // Placeholder Talk
            // What was the first half of the first phrase in {0}?
            // What was the first half of the first phrase in Placeholder Talk?
            [Question.PlaceholderTalkFirstPhrase] = new()
            {
                QuestionText = "Какая была первая половина первой фразы {0}?",
            },
            // What was the last half of the first phrase in {0}?
            // What was the last half of the first phrase in Placeholder Talk?
            [Question.PlaceholderTalkOrdinal] = new()
            {
                QuestionText = "Какая была вторая половина первой фразы {0}?",
            },

            // Placement Roulette
            // What was the character listed on the information display in {0}?
            // What was the character listed on the information display in Placement Roulette?
            [Question.PlacementRouletteChar] = new()
            {
                QuestionText = "Какой персонаж присуствовал на экране {0}?",
            },
            // What was the track listed on the information display in {0}?
            // What was the track listed on the information display in Placement Roulette?
            [Question.PlacementRouletteTrack] = new()
            {
                QuestionText = "Какая трасса присутствовала на экране {0}?",
            },
            // What was the vehicle listed on the information display in {0}?
            // What was the vehicle listed on the information display in Placement Roulette?
            [Question.PlacementRouletteVehicle] = new()
            {
                QuestionText = "Какая машина присутствовала на экране {0}?",
            },

            // Planets
            // What was the planet shown in {0}?
            // What was the planet shown in Planets?
            [Question.PlanetsPlanet] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая планета была показана на {0}?",
            },
            // What was the color of the {1} strip (from the top) in {0}?
            // What was the color of the first strip (from the top) in Planets?
            [Question.PlanetsStrips] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой был цвет у {1}-й полоски (начиная сверху) на {0}?",
                Answers = new Dictionary<string, string>
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

            // Playfair Cycle
            // Which direction was the {1} dial pointing in {0}?
            // Which direction was the first dial pointing in Playfair Cycle?
            [Question.PlayfairCycleDialDirections] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which direction was the {1} dial pointing in {0}?",
            },
            // What letter was written on the {1} dial in {0}?
            // What letter was written on the first dial in Playfair Cycle?
            [Question.PlayfairCycleDialLabels] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What letter was written on the {1} dial in {0}?",
            },

            // Poetry
            // What was the {1} correct answer you pressed in {0}?
            // What was the first correct answer you pressed in Poetry?
            [Question.PoetryAnswers] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какое было {1}-е правильное слово, которое вы нажали {0}?",
                ModuleName = "Поэзии",
            },

            // Pointless Machines
            // What color flashed {1} in {0}?
            // What color flashed first in Pointless Machines?
            [Question.PointlessMachinesFlashes] = new()
            {
                QuestionText = "Какого цвета была {1}-я вспышка {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Белый",
                    ["Purple"] = "Фиолетовый",
                    ["Red"] = "Красный",
                    ["Blue"] = "Синий",
                    ["Yellow"] = "Жёлтый",
                },
            },

            // Polygons
            // Which polygon was present on {0}?
            // Which polygon was present on Polygons?
            [Question.PolygonsPolygon] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой многоугольник присутствовал на {0}?",
            },

            // Polyhedral Maze
            // What was the starting position in {0}?
            // What was the starting position in Polyhedral Maze?
            [Question.PolyhedralMazeStartPosition] = new()
            {
                QuestionText = "Какая была начальная позиция {0}?",
            },

            // Prime Encryption
            // What was the number shown in {0}?
            // What was the number shown in Prime Encryption?
            [Question.PrimeEncryptionDisplayedValue] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое число было показано на {0}?",
            },

            // Prison Break
            // Which cell did the prisoner start in in {0}?
            // Which cell did the prisoner start in in Prison Break?
            [Question.PrisonBreakPrisoner] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where did the prisoner start in {0}?",
            },
            // Where did you start in {0}?
            // Where did you start in Prison Break?
            [Question.PrisonBreakDefuser] = new()
            {
                QuestionText = "Где вы начали {0}?",
            },

            // Probing
            // What was the missing frequency in the {1} wire in {0}?
            // What was the missing frequency in the red-white wire in Probing?
            [Question.ProbingFrequencies] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какая частота отсутствовала в {1} проводе {0}?",
                ModuleName = "Прозвонке",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red-white"] = "красно-белом",
                    ["yellow-black"] = "жёлто-чёрном",
                    ["green"] = "зелёном",
                    ["gray"] = "сером",
                    ["yellow-red"] = "красно-жёлтом",
                    ["red-blue"] = "красно-синем",
                },
            },

            // Procedural Maze
            // What was the initial seed in {0}?
            // What was the initial seed in Procedural Maze?
            [Question.ProceduralMazeInitialSeed] = new()
            {
                QuestionText = "Какое было изначальное зерно {0}?",
            },

            // ...?
            // What was the displayed number in {0}?
            // What was the displayed number in ...??
            [Question.PunctuationMarksDisplayedNumber] = new()
            {
                QuestionText = "Какое было показанное число в {0}?",
            },

            // Purple Arrows
            // What was the target word on {0}?
            // What was the target word on Purple Arrows?
            [Question.PurpleArrowsFinish] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какое было целевое слово {0}?",
                ModuleName = "Фиолетовых стрелках",
            },

            // The Purple Button
            // What was the {1} number in the cyclic sequence on {0}?
            // What was the first number in the cyclic sequence on The Purple Button?
            [Question.PurpleButtonNumbers] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое было {1}-е число в зацикленной последовательности {0}?",
            },

            // Puzzle Identification
            // What was the {1} puzzle number in {0}?
            // What was the first puzzle number in Puzzle Identification?
            [Question.PuzzleIdentificationNum] = new()
            {
                QuestionText = "Какое было {1}-е число пазла {0}?",
            },
            // What game was the {1} puzzle in {0} from?
            // What game was the first puzzle in Puzzle Identification from?
            [Question.PuzzleIdentificationGame] = new()
            {
                QuestionText = "Из какой игры был {1}-й пазл {0}?",
                Answers = new Dictionary<string, string>
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
            // What was the {1} puzzle in {0}?
            // What was the first puzzle in Puzzle Identification?
            [Question.PuzzleIdentificationName] = new()
            {
                QuestionText = "Какой был {1}-й пазл {0}?",
            },

            // Puzzling Hexabuttons
            // What letter was displayed on the {1} hexabutton when submitting in {0}?
            // What letter was displayed on the top-left hexabutton when submitting in Puzzling Hexabuttons?
            [Question.PuzzlingHexabuttonsLetter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What letter was displayed on the {1} hexabutton when submitting in {0}?",
                FormatArgs = new Dictionary<string, string>
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

            // Q & A
            // What was the {1} question asked in {0}?
            // What was the first question asked in Q & A?
            [Question.QnAQuestions] = new()
            {
                QuestionText = "Какой был {1}-й вопрос {0}?",
            },

            // Quadrants
            // What was on the {1} button of the {2} stage in {0}?
            // What was on the first button of the first stage in Quadrants?
            [Question.QuadrantsButtons] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was on the {1} button of the {2} stage in {0}?",
            },

            // Quantum Passwords
            // Which word was used in {0}?
            // Which word was used in Quantum Passwords?
            [Question.QuantumPasswordsWord] = new()
            {
                QuestionText = "Какое слово было использовано {0}?",
            },

            // Quantum Ternary Converter
            // Which number was shown in {0}?
            // Which number was shown in Quantum Ternary Converter?
            [Question.QuantumTernaryConverterNumber] = new()
            {
                QuestionText = "Какое число было показано {0}?",
            },

            // Quaver
            // What was the {1} sequence’s answer in {0}?
            // What was the first sequence’s answer in Quaver?
            [Question.QuaverArrows] = new()
            {
                QuestionText = "Какой был {1}-й ответ последовательности {0}?",
            },

            // Question Mark
            // Which of these symbols was part of the flashing sequence in {0}?
            // Which of these symbols was part of the flashing sequence in Question Mark?
            [Question.QuestionMarkFlashedSymbols] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой из этих символов был частью мигающей последовательности {0}?",
            },

            // Quick Arithmetic
            // What was the {1} color in the primary sequence in {0}?
            // What was the first color in the primary sequence in Quick Arithmetic?
            [Question.QuickArithmeticColors] = new()
            {
                QuestionText = "Какой был {1}-й цвет в основной последовательности {0}?",
                Answers = new Dictionary<string, string>
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
            // What was the {1} digit in the {2} sequence in {0}?
            // What was the first digit in the primary sequence in Quick Arithmetic?
            [Question.QuickArithmeticPrimSecDigits] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Какое было {1}-е число в {2} последовательности {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["primary"] = "primary",
                    ["secondary"] = "secondary",
                },
            },

            // Quintuples
            // What was the {1} digit in the {2} slot in {0}?
            // What was the first digit in the first slot in Quintuples?
            [Question.QuintuplesNumbers] = new()
            {
                QuestionText = "Какая была {1}-я цифра в {2}-м слоте {0}?",
            },
            // What color was the {1} digit in the {2} slot in {0}?
            // What color was the first digit in the first slot in Quintuples?
            [Question.QuintuplesColors] = new()
            {
                QuestionText = "Какого цвета была {1}-я цифра в {2}-м слоте {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красный",
                    ["blue"] = "Синий",
                    ["orange"] = "Оранжевый",
                    ["green"] = "Зелёный",
                    ["pink"] = "Розовый",
                },
            },
            // How many numbers were {1} in {0}?
            // How many numbers were red in Quintuples?
            [Question.QuintuplesColorCounts] = new()
            {
                QuestionText = "Скольк было {1} чисел {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красных",
                    ["blue"] = "синих",
                    ["orange"] = "оранжевых",
                    ["green"] = "зелёных",
                    ["pink"] = "розовых",
                },
            },

            // Quiplash
            // What number was shown on {0}?
            // What number was shown on Quiplash?
            [Question.QuiplashNumber] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое число было показано на {0}?",
            },

            // Quiz Buzz
            // What was the number initially on the display in {0}?
            // What was the number initially on the display in Quiz Buzz?
            [Question.QuizBuzzStartingNumber] = new()
            {
                QuestionText = "Какое было исходное число на экране {0}?",
            },

            // Qwirkle
            // What tile did you place {1} in {0}?
            // What tile did you place first in Qwirkle?
            [Question.QwirkleTilesPlaced] = new()
            {
                QuestionText = "Какую плитку вы положили {1}-й {0}?",
            },

            // Raiding Temples
            // How many jewels were in the starting common pool in {0}?
            // How many jewels were in the starting common pool in Raiding Temples?
            [Question.RaidingTemplesStartingCommonPool] = new()
            {
                QuestionText = "Сколько драгоценностей было в начальном общем схроне {0}?",
            },

            // Railway Cargo Loading
            // What was the {1} car in {0}?
            // What was the first car in Railway Cargo Loading?
            [Question.RailwayCargoLoadingCars] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какой вагон был присоединён {1}-м {0}?",
                ModuleName = "Загрузке ЖД состава",
            },
            // Which freight table rule {1} in {0}?
            // Which freight table rule was met in Railway Cargo Loading?
            [Question.RailwayCargoLoadingFreightTableRules] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какое правило из таблицы грузовых вагонов {1} {0}?",
                ModuleName = "Загрузке ЖД состава",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was met"] = "было применено",
                    ["wasn’t met"] = "не было применено",
                },
            },

            // Rainbow Arrows
            // What was the displayed number in {0}?
            // What was the displayed number in Rainbow Arrows?
            [Question.RainbowArrowsNumber] = new()
            {
                QuestionText = "Какое число было показано {0}?",
            },

            // Recolored Switches
            // What was the color of the {1} LED in {0}?
            // What was the color of the first LED in Recolored Switches?
            [Question.RecoloredSwitchesLedColors] = new()
            {
                Conjugation = Conjugation.GenitivePlural,
                QuestionText = "Какой был цвет {1}-го светодиода {0}?",
                ModuleName = "Перекрашенных переключателей",
                Answers = new Dictionary<string, string>
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

            // Recursive Password
            // Which of these words appeared, but was not the password, in {0}?
            // Which of these words appeared, but was not the password, in Recursive Password?
            [Question.RecursivePasswordNonPasswordWords] = new()
            {
                QuestionText = "Какое из этих слов присутствовало, но не являлось верным ответом {0}?",
            },
            // What was the password in {0}?
            // What was the password in Recursive Password?
            [Question.RecursivePasswordPassword] = new()
            {
                QuestionText = "Какой пароль был верным ответом {0}?",
            },

            // Red Arrows
            // What was the starting number in {0}?
            // What was the starting number in Red Arrows?
            [Question.RedArrowsStartNumber] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какое было начальное число {0}?",
                ModuleName = "Красных стрелках",
            },

            // Red Button’t
            // What was the word before “SUBMIT” in {0}?
            // What was the word before “SUBMIT” in Red Button’t?
            [Question.RedButtontWord] = new()
            {
                QuestionText = "Какое слово было перед 'SUBMIT' {0}?",
            },

            // Red Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Red Cipher?
            [Question.RedCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Red Herring
            // What was the first color flashed by {0}?
            // What was the first color flashed by Red Herring?
            [Question.RedHerringFirstFlash] = new()
            {
                QuestionText = "Какой был первый мигающий цвет {0}?",
            },

            // Reformed Role Reversal
            // Which condition was the solving condition in {0}?
            // Which condition was the solving condition in Reformed Role Reversal?
            [Question.ReformedRoleReversalCondition] = new()
            {
                Conjugation = Conjugation.NominativeMasculine,
                QuestionText = "На каком условии был обезврежен {0}?",
                Answers = new Dictionary<string, string>
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
            // What color was the {1} wire in {0}?
            // What color was the first wire in Reformed Role Reversal?
            [Question.ReformedRoleReversalWire] = new()
            {
                QuestionText = "Какого цвета был {1}-й провод {0}?",
                Answers = new Dictionary<string, string>
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

            // ReGret-B Filtering
            // Which calculation was used for the {1} stage of {0}?
            // Which calculation was used for the first stage of ReGret-B Filtering?
            [Question.ReGretBFilteringOperator] = new()
            {
                QuestionText = "Какой оператор был применён на {1}-м этапе {0}?",
            },

            // Regular Crazy Talk
            // What was the displayed digit that corresponded to the solution phrase in {0}?
            // What was the displayed digit that corresponded to the solution phrase in Regular Crazy Talk?
            [Question.RegularCrazyTalkDigit] = new()
            {
                QuestionText = "Какая показанная цифра соответствовала решению {0}?",
            },
            // What was the embellishment of the solution phrase in {0}?
            // What was the embellishment of the solution phrase in Regular Crazy Talk?
            [Question.RegularCrazyTalkModifier] = new()
            {
                QuestionText = "Какое было дополнение у фразы решения {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["[PHRASE]"] = "[PHRASE]",
                    ["It says: [PHRASE]"] = "It says: [PHRASE]",
                    ["Quote: [PHRASE] End quote"] = "Quote: [PHRASE] End quote",
                    ["“[PHRASE]”"] = "“[PHRASE]”",
                    ["It says: “[PHRASE]”"] = "It says: “[PHRASE]”",
                    ["“It says: [PHRASE]”"] = "“It says: [PHRASE]”",
                },
            },

            // Reordered Keys
            // Which key was the pivot in the {1} stage of {0}?
            [Question.ReorderedKeysPivot] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which key was the pivot in the {1} stage of {0}?",
            },
            // What color was this key in the {1} stage of {0}?
            // What color was this key in the first stage of Reordered Keys?
            [Question.ReorderedKeysKeyColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was this key in the {1} stage of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                },
            },
            // What color was the label of this key in the {1} stage of {0}?
            // What color was the label of this key in the first stage of Reordered Keys?
            [Question.ReorderedKeysLabelColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the label of this key in the {1} stage of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                },
            },
            // What was the label of this key in the {1} stage of {0}?
            // What was the label of this key in the first stage of Reordered Keys?
            [Question.ReorderedKeysLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label of this key in the {1} stage of {0}?",
            },

            // Retirement
            // Which one of these houses was on offer, but not chosen by Bob in {0}?
            // Which one of these houses was on offer, but not chosen by Bob in Retirement?
            [Question.RetirementHouses] = new()
            {
                QuestionText = "Какой из этих домов предлагался, но не был выбран Бобом {0}?",
            },

            // Reverse Morse
            // What was the {1} character in the {2} message of {0}?
            // What was the first character in the first message of Reverse Morse?
            [Question.ReverseMorseCharacters] = new()
            {
                QuestionText = "Какой был {1}-й символ в {2}-м сообщении {0}?",
            },

            // Reverse Polish Notation
            // What character was used in the {1} round of {0}?
            // What character was used in the first round of Reverse Polish Notation?
            [Question.ReversePolishNotationCharacter] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой символ был использован на {1}-м этапе {0}?",
            },

            // RGB Maze
            // What was the exit coordinate in {0}?
            // What was the exit coordinate in RGB Maze?
            [Question.RGBMazeExit] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была координата выхода из {0}?",
            },
            // Where was the {1} key in {0}?
            // Where was the red key in RGB Maze?
            [Question.RGBMazeKeys] = new()
            {
                QuestionText = "Где был {1} ключ {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красный",
                    ["green"] = "зелёный",
                    ["blue"] = "синий",
                },
            },
            // Which maze number was the {1} maze in {0}?
            // Which maze number was the red maze in RGB Maze?
            [Question.RGBMazeNumber] = new()
            {
                QuestionText = "Какой {1} лабиринт был {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красный",
                    ["green"] = "зелёный",
                    ["blue"] = "синий",
                },
            },

            // RGB Sequences
            // What was the color of the {1} LED in {0}?
            // What was the color of the first LED in RGB Sequences?
            [Question.RGBSequencesDisplay] = new()
            {
                QuestionText = "Какой был цвет {1}-го светодиода {0}?",
                Answers = new Dictionary<string, string>
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

            // Rhythms
            // What was the color in {0}?
            // What was the color in Rhythms?
            [Question.RhythmsColor] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какого цвета был светодиод {0}?",
                ModuleName = "Ритмах",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Синего",
                    ["Red"] = "Красного",
                    ["Green"] = "Зелёного",
                    ["Yellow"] = "Жёлтого",
                },
            },

            // RNG Crystal
            // Which bit had a tap in {0} (the output after shifting is at bit 0)?
            // Which bit had a tap in RNG Crystal (the output after shifting is at bit 0)?
            [Question.RNGCrystalTaps] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which bit had a tap in {0} (the output after shifting is at bit 0)?",
            },

            // Robo-Scanner
            // Where was the empty cell in {0}?
            // Where was the empty cell in Robo-Scanner?
            [Question.RoboScannerEmptyCell] = new()
            {
                QuestionText = "Где была пустая ячейка {0}?",
            },

            // Robot Programming
            // What was the color of the {1} robot in {0}?
            // What was the color of the first robot in Robot Programming?
            [Question.RobotProgrammingColor] = new()
            {
                QuestionText = "Какого цвета был {1}-й робот {0}?",
            },
            // What was the shape of the {1} robot in {0}?
            // What was the shape of the first robot in Robot Programming?
            [Question.RobotProgrammingShape] = new()
            {
                QuestionText = "Какой формы был {1}-й робот {0}?",
            },

            // Roger
            // What four-digit number was given in {0}?
            // What four-digit number was given in Roger?
            [Question.RogerSeed] = new()
            {
                QuestionText = "Какое четырёхзначное число было дано {0}?",
            },

            // Role Reversal
            // What was the number to the correct condition in {0}?
            // What was the number to the correct condition in Role Reversal?
            [Question.RoleReversalNumber] = new()
            {
                QuestionText = "Какое был номер у верного условия {0}?",
            },
            // How many {1} wires were there in {0}?
            // How many warm-colored wires were there in Role Reversal?
            [Question.RoleReversalWires] = new()
            {
                QuestionText = "Сколько проводов, окрашенных в {1} было {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["warm-colored"] = "тёплые цвета",
                    ["cold-colored"] = "холодные цвета",
                    ["primary-colored"] = "основные цвета",
                    ["secondary-colored"] = "вторичные цвета",
                },
            },

            // The Rule
            // What was the rule number in {0}?
            // What was the rule number in The Rule?
            [Question.RuleNumber] = new()
            {
                QuestionText = "Какой был номер правила {0}?",
            },

            // Rule of Three
            // What was the {1} coordinate of the {2} vertex in {0}?
            // What was the X coordinate of the red vertex in Rule of Three?
            [Question.RuleOfThreeCoordinates] = new()
            {
                QuestionText = "Какая была {1} координата у {2} оси {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красной",
                    ["yellow"] = "жёлтой",
                    ["blue"] = "синей",
                },
            },
            // What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?
            // What was the position of the red sphere on the X axis in the first cycle in Rule of Three?
            [Question.RuleOfThreeCycles] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Где находилась {1} сфера на {2} оси в {3}-м цикле {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красная",
                    ["yellow"] = "жёлтая",
                    ["blue"] = "синяя",
                },
            },

            // Safety Square
            // What was the digit displayed on the {1} diamond in {0}?
            // What was the digit displayed on the red diamond in Safety Square?
            [Question.SafetySquareDigits] = new()
            {
                QuestionText = "Какая цифра была показана на {1} ромбе {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красном",
                    ["yellow"] = "жёлтом",
                    ["blue"] = "синем",
                },
            },
            // What was the special rule displayed on the white diamond in {0}?
            // What was the special rule displayed on the white diamond in Safety Square?
            [Question.SafetySquareSpecialRule] = new()
            {
                QuestionText = "Какое правило было показано на белом ромбе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["No special rule"] = "No special rule",
                    ["Reacts with water"] = "Reacts with water",
                    ["Simple asphyxiant"] = "Simple asphyxiant",
                    ["Oxidizer"] = "Oxidizer",
                },
            },

            // The Samsung
            // Where was {1} in {0}?
            // Where was Duolingo in The Samsung?
            [Question.SamsungAppPositions] = new()
            {
                QuestionText = "Где было приложение {1} {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["TL"] = "Сверху слева",
                    ["TM"] = "Сверху посередине",
                    ["TR"] = "Сверху справа",
                    ["ML"] = "Посередине слева",
                    ["MM"] = "В центре",
                    ["MR"] = "Посередине справа",
                    ["BL"] = "Снизу слева",
                    ["BM"] = "Снизу посередине",
                    ["BR"] = "Снизу справа",
                },
            },

            // Saturn
            // Where was the goal in {0}?
            // Where was the goal in Saturn?
            [Question.SaturnGoal] = new()
            {
                QuestionText = "Где была цель {0}?",
            },

            // Sbemail Songs
            // What was the displayed song for stage {1} (hexadecimal) of {0}?
            // What was the displayed song for stage 01 (hexadecimal) of Sbemail Songs?
            [Question.SbemailSongsSongs] = new()
            {
                QuestionText = "Какая песня была показана на этапе {1} (16-ричное число) {0}?",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)"] = "в Sbemail Songs, где на этапе {1} (16-ричное число) была показана песня {0}",
                },
            },

            // Scavenger Hunt
            // Which tile was correctly submitted in the first stage of {0}?
            // Which tile was correctly submitted in the first stage of Scavenger Hunt?
            [Question.ScavengerHuntKeySquare] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая плитка была верным ответом на первом этапе {0}?",
            },
            // Which of these tiles was {1} in the first stage of {0}?
            // Which of these tiles was red in the first stage of Scavenger Hunt?
            [Question.ScavengerHuntColoredTiles] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая из этих плиток была {1} на первом этапе {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красной",
                    ["green"] = "зелёной",
                    ["blue"] = "синей",
                },
            },

            // Schlag den Bomb
            // What was the contestant’s name in {0}?
            // What was the contestant’s name in Schlag den Bomb?
            [Question.SchlagDenBombContestantName] = new()
            {
                QuestionText = "Какое было имя у участника {0}?",
            },
            // What was the contestant’s score in {0}?
            // What was the contestant’s score in Schlag den Bomb?
            [Question.SchlagDenBombContestantScore] = new()
            {
                QuestionText = "Какой был счёт у участника {0}?",
            },
            // What was the bomb’s score in {0}?
            // What was the bomb’s score in Schlag den Bomb?
            [Question.SchlagDenBombBombScore] = new()
            {
                QuestionText = "Какой был счёт у бомбы {0}?",
            },

            // Scramboozled Eggain
            // What was the {1} encrypted word in {0}?
            // What was the first encrypted word in Scramboozled Eggain?
            [Question.ScramboozledEggainWord] = new()
            {
                QuestionText = "Какое было {1}-е зашифрованное слово {0}?",
            },

            // Scripting
            // What was the submitted data type of the variable in {0}?
            // What was the submitted data type of the variable in Scripting?
            [Question.ScriptingVariableDataType] = new()
            {
                QuestionText = "Какой был верный тип данных у переменной {0}?",
            },

            // Scrutiny Squares
            // What was the modified property of the first display in {0}?
            // What was the modified property of the first display in Scrutiny Squares?
            [Question.ScrutinySquaresFirstDifference] = new()
            {
                QuestionText = "Какое свойство отличалось на первом экране {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Word"] = "Слово",
                    ["Color around word"] = "Цвет вокруг слова",
                    ["Color of background"] = "Цвет фона",
                    ["Color of word"] = "Цвет слова",
                },
            },

            // Sea Shells
            // What were the first and second words in the {1} phrase in {0}?
            // What were the first and second words in the first phrase in Sea Shells?
            [Question.SeaShells1] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какими были первое и второе слово {1}-й фразы {0}?",
                ModuleName = "Морских ракушках",
            },
            // What were the third and fourth words in the {1} phrase in {0}?
            // What were the third and fourth words in the first phrase in Sea Shells?
            [Question.SeaShells2] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какими были третье и четвёртое слово {1}-й фразы {0}?",
                ModuleName = "Морских ракушках",
            },
            // What was the end of the {1} phrase in {0}?
            // What was the end of the first phrase in Sea Shells?
            [Question.SeaShells3] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Каким был конец {1}-й фразы {0}?",
                ModuleName = "Морских ракушках",
            },

            // Semamorse
            // What was the {1} letter involved in the starting value in {0}?
            // What was the Morse letter involved in the starting value in Semamorse?
            [Question.SemamorseLetters] = new()
            {
                QuestionText = "Какая была буква {1}, использованная в начальном значении {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Morse"] = "Морзе",
                    ["semaphore"] = "семафора",
                },
            },
            // What was the color of the display involved in the starting value in {0}?
            // What was the color of the display involved in the starting value in Semamorse?
            [Question.SemamorseColor] = new()
            {
                QuestionText = "Какого цвета светодиоды использовались в начальном значении {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красного",
                    ["green"] = "Зелёного",
                    ["cyan"] = "Голубого",
                    ["indigo"] = "Индиго",
                    ["pink"] = "Розового",
                },
            },

            // The Sequencyclopedia
            // What sequence was used in {0}?
            // What sequence was used in The Sequencyclopedia?
            [Question.SequencyclopediaSequence] = new()
            {
                QuestionText = "Какая была последовательность {0}?",
            },

            // S.E.T. Theory
            // What equation was shown in the {1} stage of {0}?
            // What equation was shown in the first stage of S.E.T. Theory?
            [Question.SetTheoryEquations] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое уравнение было показано на {1}-м этапе {0}?",
            },

            // Shapes And Bombs
            // What was the initial letter in {0}?
            // What was the initial letter in Shapes And Bombs?
            [Question.ShapesAndBombsInitialLetter] = new()
            {
                QuestionText = "Какая была начальная буква {0}?",
            },

            // Shape Shift
            // What was the initial shape in {0}?
            // What was the initial shape in Shape Shift?
            [Question.ShapeShiftInitialShape] = new()
            {
                QuestionText = "Какая была изначальная фигура {0}?",
                ModuleName = "Изменении формы",
            },

            // Shifted Maze
            // What color was the {1} marker in {0}?
            // What color was the top-left marker in Shifted Maze?
            [Question.ShiftedMazeColors] = new()
            {
                QuestionText = "Какого цвета был {1} маркер {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхний левый",
                    ["top-right"] = "верхний правый",
                    ["bottom-left"] = "нижний левый",
                    ["bottom-right"] = "нижний правый",
                },
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "White",
                    ["Blue"] = "Blue",
                    ["Yellow"] = "Yellow",
                    ["Magenta"] = "Magenta",
                    ["Green"] = "Green",
                },
            },

            // Shifting Maze
            // What was the seed in {0}?
            // What was the seed in Shifting Maze?
            [Question.ShiftingMazeSeed] = new()
            {
                QuestionText = "Какое было зерно {0}?",
            },

            // Shogi Identification
            // What was the displayed piece in {0}?
            // What was the displayed piece in Shogi Identification?
            [Question.ShogiIdentificationPiece] = new()
            {
                QuestionText = "Какая фигура была показана {0}?",
                Answers = new Dictionary<string, string>
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

            // Sign Language
            // What was the deciphered word in {0}?
            // What was the deciphered word in Sign Language?
            [Question.SignLanguageWord] = new()
            {
                QuestionText = "Какое слово было расшифровано {0}?",
            },

            // Silly Slots
            // What was the {1} slot in the {2} stage in {0}?
            // What was the first slot in the first stage in Silly Slots?
            [Question.SillySlots] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был {1}-й слот на {2}-м этапе {0}?",
                ModuleName = "Однорукого бандита",
                Answers = new Dictionary<string, string>
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

            // Silo Authorization
            // What was the message type in {0}?
            // What was the message type in Silo Authorization?
            [Question.SiloAuthorizationMessageType] = new()
            {
                QuestionText = "Какой был тип сообщения {0}?",
            },
            // What was the {1} part of the encrypted message in {0}?
            // What was the first part of the encrypted message in Silo Authorization?
            [Question.SiloAuthorizationEncryptedMessage] = new()
            {
                QuestionText = "Какая была {1}-я часть зашифрованного сообщения {0}?",
            },
            // What was the received authentication code in {0}?
            // What was the received authentication code in Silo Authorization?
            [Question.SiloAuthorizationAuthCode] = new()
            {
                QuestionText = "Какой код авторизации был получен {0}?",
            },

            // Simon Said
            // What color flashed {1} in the final sequence of {0}?
            // What color flashed first in the final sequence of Simon Said?
            [Question.SimonSaidFlashes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color flashed {1} in the final sequence of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                    ["Yellow"] = "Жёлтый",
                },
            },

            // Simon Samples
            // What were the call samples {1} of {0}?
            // What were the call samples played in the first stage of Simon Samples?
            [Question.SimonSamplesSamples] = new()
            {
                QuestionText = "Какие семплы были {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["played in the first stage"] = "проиграны на 1-м этапе",
                    ["added in the second stage"] = "добавлены на 2-м этапе",
                    ["added in the third stage"] = "добавлены на 3-м этапе",
                },
            },

            // Simon Says
            // What color flashed {1} in the final sequence in {0}?
            // What color flashed first in the final sequence in Simon Says?
            [Question.SimonSaysFlash] = new()
            {
                QuestionText = "Какой цвет горел {1}-м в последовательности {0}?",
                ModuleName = "\"Саймон говорит\"",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красный",
                    ["yellow"] = "Жёлтый",
                    ["green"] = "Зелёный",
                    ["blue"] = "Синий",
                },
            },

            // Simon Scrambles
            // What color flashed {1} in {0}?
            // What color flashed first in Simon Scrambles?
            [Question.SimonScramblesColors] = new()
            {
                QuestionText = "Какой цвет горел {1}-м {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                    ["Yellow"] = "Жёлтый",
                },
            },

            // Simon Screams
            // Which color flashed {1} in the final sequence in {0}?
            // Which color flashed first in the final sequence in Simon Screams?
            [Question.SimonScreamsFlashing] = new()
            {
                QuestionText = "Какой цвет горел {1}-м в полной последовательности {0}?",
                ModuleName = "\"Саймон кричит\"",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Orange"] = "Оранжевый",
                    ["Yellow"] = "Жёлтый",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                    ["Purple"] = "Фиолетовый",
                },
            },
            // In which stage(s) of {0} was “{1}” the applicable rule?
            // In which stage(s) of Simon Screams was “a color flashed, then a color two away, then the first again” the applicable rule?
            [Question.SimonScreamsRuleSimple] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "На каком(-их) этапе(-ах) {0} {1}?",
                ModuleName = "\"Саймон кричит\"",
                FormatArgs = new Dictionary<string, string>
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
                Answers = new Dictionary<string, string>
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
            // In which stage(s) of {0} was “{1} flashed out of {2}, {3}, and {4}” the applicable rule?
            // In which stage(s) of Simon Screams was “at most one color flashed out of Red, Orange, and Yellow” the applicable rule?
            [Question.SimonScreamsRuleComplex] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "На каком(-их) этапе(-ах) {0} среди кнопок {2}, {3} и {4} цвета {1}?",
                ModuleName = "\"Саймон кричит\"",
                FormatArgs = new Dictionary<string, string>
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
                Answers = new Dictionary<string, string>
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

            // Simon Selects
            // Which color flashed {1} in the {2} stage of {0}?
            // Which color flashed first in the first stage of Simon Selects?
            [Question.SimonSelectsOrder] = new()
            {
                QuestionText = "Какой цвет горел {1}-м на {2}-м этапе {0}?",
                Answers = new Dictionary<string, string>
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

            // Simon Sends
            // What was the {1} received letter in {0}?
            // What was the red received letter in Simon Sends?
            [Question.SimonSendsReceivedLetters] = new()
            {
                QuestionText = "Какая была {1} полученная буква {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красная",
                    ["green"] = "зелёная",
                    ["blue"] = "синяя",
                },
            },

            // Simon Serves
            // Who flashed {1} in course {2} of {0}?
            // Who flashed first in course 1 of Simon Serves?
            [Question.SimonServesFlash] = new()
            {
                QuestionText = "Кто горел {1}-м на {2}-й подаче в {0}?",
            },
            // Which item was not served in course {1} of {0}?
            // Which item was not served in course 1 of Simon Serves?
            [Question.SimonServesFood] = new()
            {
                QuestionText = "Что не подавалось гостям на {1}-й подаче в {0}?",
                Answers = new Dictionary<string, string>
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

            // Simon Shapes
            // What was the shape submitted at the end of {0}?
            // What was the shape submitted at the end of Simon Shapes?
            [Question.SimonShapesSubmittedShape] = new()
            {
                QuestionText = "Какая фигура была введена в конце {0}?",
            },

            // Simon Shouts
            // Which letter flashed on the {1} button in {0}?
            // Which letter flashed on the top button in Simon Shouts?
            [Question.SimonShoutsFlashingLetter] = new()
            {
                QuestionText = " Какая буква горела на {1} кнопке в {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхней",
                    ["left"] = "левой",
                    ["right"] = "правой",
                    ["bottom"] = "нижней",
                },
            },

            // Simon Shrieks
            // How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?
            // How many spaces clockwise from the arrow was the first flash in the final sequence in Simon Shrieks?
            [Question.SimonShrieksFlashingButton] = new()
            {
                QuestionText = "В скольки кнопках от стрелки (по часовой) была {1}-я вспышка в финальной последовательности в {0}?",
            },

            // Simon Shuffles
            // What was the {1} flash of the {2} stage of {0}?
            // What was the first flash of the first stage of Simon Shuffles?
            [Question.SimonShufflesFlashes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} flash of the {2} stage of {0}?",
            },

            // Simon Signals
            // What shape was the {1} arrow in {0}?
            // What shape was the red arrow in Simon Signals?
            [Question.SimonSignalsColorToShape] = new()
            {
                QuestionText = "Какой формы была {1} стрелка в {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красная",
                    ["green"] = "зелёная",
                    ["blue"] = "синяя",
                    ["gray"] = "серая",
                },
            },
            // How many directions did the {1} arrow in {0} have?
            // How many directions did the red arrow in Simon Signals have?
            [Question.SimonSignalsColorToRotations] = new()
            {
                QuestionText = "Сколько направлений было у {1} стрелки в {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красной",
                    ["green"] = "зелёной",
                    ["blue"] = "синей",
                    ["gray"] = "серой",
                },
            },
            // What color was the arrow with this shape in {0}?
            // What color was the arrow with this shape in Simon Signals?
            [Question.SimonSignalsShapeToColor] = new()
            {
                QuestionText = "Какого цвета была стрелка этой формы в {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красного",
                    ["green"] = "Зелёного",
                    ["blue"] = "Синего",
                    ["gray"] = "Серого",
                },
            },
            // How many directions did the arrow with this shape have in {0}?
            // How many directions did the arrow with this shape have in Simon Signals?
            [Question.SimonSignalsShapeToRotations] = new()
            {
                QuestionText = "Сколько направлений было у стрелки с этой формой в {0}?",
            },
            // What color was the arrow with {1} possible directions in {0}?
            // What color was the arrow with 3 possible directions in Simon Signals?
            [Question.SimonSignalsRotationsToColor] = new()
            {
                QuestionText = "Какого цвета была стрелка с {1}-мя возможными направлениями в {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красного",
                    ["green"] = "Зелёного",
                    ["blue"] = "Синего",
                    ["gray"] = "Серого",
                },
            },
            // What shape was the arrow with {1} possible directions in {0}?
            // What shape was the arrow with 3 possible directions in Simon Signals?
            [Question.SimonSignalsRotationsToShape] = new()
            {
                QuestionText = "Какой формы была стрелка с {1}-мя возможными направлениями {0}?",
            },

            // Simon Simons
            // What was the {1} flash in the final sequence in {0}?
            // What was the first flash in the final sequence in Simon Simons?
            [Question.SimonSimonsFlashingColors] = new()
            {
                QuestionText = "Какая была {1}-я вспышка в полной последовательности в {0}?",
            },

            // Simon Sings
            // Which key’s color flashed {1} in the {2} stage of {0}?
            // Which key’s color flashed first in the first stage of Simon Sings?
            [Question.SimonSingsFlashing] = new()
            {
                QuestionText = "Какой цвет кнопки горел {1}-м на {2}-м этапе {0}?",
            },

            // Simon Smiles
            // What sound did the {1} button press make {0}?
            // What sound did the first button press make Simon Smiles?
            [Question.SimonSmilesSounds] = new()
            {
                QuestionText = "Как звук был у {1}-й кнопки {0}?",
            },

            // Simon Smothers
            // What was the color of the {1} flash in {0}?
            // What was the color of the first flash in Simon Smothers?
            [Question.SimonSmothersColors] = new()
            {
                QuestionText = "Какой был цвет у {1}-й вспышки в {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Yellow"] = "Yellow",
                    ["Blue"] = "Blue",
                    ["Magenta"] = "Magenta",
                    ["Cyan"] = "Cyan",
                },
            },
            // What was the direction of the {1} flash in {0}?
            // What was the direction of the first flash in Simon Smothers?
            [Question.SimonSmothersDirections] = new()
            {
                QuestionText = "Какое направление было у {1}-й вспышки в {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Вверх",
                    ["Down"] = "Вниз",
                    ["Left"] = "Влево",
                    ["Right"] = "Вправо",
                },
            },

            // Simon Sounds
            // Which sample button sounded {1} in the final sequence in {0}?
            // Which sample button sounded first in the final sequence in Simon Sounds?
            [Question.SimonSoundsFlashingColors] = new()
            {
                QuestionText = "Какая кнопка семпла звучала {1}-й в полной последовательности в {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красного",
                    ["blue"] = "Синего",
                    ["yellow"] = "Жёлтого",
                    ["green"] = "Зелёного",
                },
            },

            // Simon Speaks
            // Which bubble flashed first in {0}?
            // Which bubble flashed first in Simon Speaks?
            [Question.SimonSpeaksPositions] = new()
            {
                QuestionText = "Какое диалоговое облако горело первым {0}?",
                ModuleName = "\"Саймон общается\"",
                Answers = new Dictionary<string, string>
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
            // Which bubble flashed second in {0}?
            // Which bubble flashed second in Simon Speaks?
            [Question.SimonSpeaksShapes] = new()
            {
                QuestionText = "Какое диалоговое облако загорелось вторым {0}?",
                ModuleName = "\"Саймон общается\"",
            },
            // Which language was the bubble that flashed third in {0} in?
            // Which language was the bubble that flashed third in Simon Speaks in?
            [Question.SimonSpeaksLanguages] = new()
            {
                QuestionText = "Какого языка была надпись на третьем загоревшемся диалоговом облаке {0}?",
                ModuleName = "\"Саймон общается\"",
            },
            // Which word was in the bubble that flashed fourth in {0}?
            // Which word was in the bubble that flashed fourth in Simon Speaks?
            [Question.SimonSpeaksWords] = new()
            {
                QuestionText = "Какое слово было в четвёртом загоревшемся диалоговом окне {0}?",
                ModuleName = "\"Саймон общается\"",
            },
            // What color was the bubble that flashed fifth in {0}?
            // What color was the bubble that flashed fifth in Simon Speaks?
            [Question.SimonSpeaksColors] = new()
            {
                QuestionText = "Какого цвета было пятое загоревшееся диалоговое окно {0}?",
                ModuleName = "\"Саймон общается\"",
                Answers = new Dictionary<string, string>
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

            // Simon’s Star
            // Which color flashed {1} in sequence in {0}?
            // Which color flashed first in sequence in Simon’s Star?
            [Question.SimonsStarColors] = new()
            {
                QuestionText = "Какой цвет горел {1}-м в последовательности в {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "красный",
                    ["yellow"] = "жёлтый",
                    ["green"] = "зелёный",
                    ["blue"] = "синий",
                    ["purple"] = "фиолетовый",
                },
            },

            // Simon Stacks
            // Which color flashed in the {1} stage of {0}?
            // Which color flashed in the first stage of Simon Stacks?
            [Question.SimonStacksColors] = new()
            {
                QuestionText = "Какой цвет горел на {1}-м этапе в {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                    ["Yellow"] = "Жёлтый",
                },
            },

            // Simon Stages
            // Which color flashed {1} in the {2} stage in {0}?
            // Which color flashed first in the first stage in Simon Stages?
            [Question.SimonStagesFlashes] = new()
            {
                QuestionText = "Какой цвет горел {1}-м на {2}-м этапе {0}?",
                ModuleName = "\"Саймон выступает\"",
                Answers = new Dictionary<string, string>
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
            // What color was the indicator in the {1} stage in {0}?
            // What color was the indicator in the first stage in Simon Stages?
            [Question.SimonStagesIndicator] = new()
            {
                QuestionText = "Какого цвета был индикатор на {1}-м этапе {0}?",
                ModuleName = "\"Саймон выступает\"",
                Answers = new Dictionary<string, string>
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

            // Simon States
            // Which {1} in the {2} stage in {0}?
            // Which color(s) flashed in the first stage in Simon States?
            [Question.SimonStatesDisplay] = new()
            {
                QuestionText = "Какой(-ие) цвет(а) {1} на {2}-м этапе {0}?",
                ModuleName = "\"Саймон утверждает\"",
                FormatArgs = new Dictionary<string, string>
                {
                    ["color(s) flashed"] = "горел(и)",
                    ["color(s) didn’t flash"] = "не горел(и)",
                },
                Answers = new Dictionary<string, string>
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

            // Simon Stops
            // Which color flashed {1} in the output sequence in {0}?
            // Which color flashed first in the output sequence in Simon Stops?
            [Question.SimonStopsColors] = new()
            {
                QuestionText = "Какой цвет горел {1}-м в последовательности вспышек в {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Orange"] = "Оранжевый",
                    ["Yellow"] = "Жёлтый",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                    ["Violet"] = "Фиолетовый",
                },
            },

            // Simon Stores
            // Which color {1} {2} in the final sequence of {0}?
            // Which color flashed first in the final sequence of Simon Stores?
            [Question.SimonStoresColors] = new()
            {
                QuestionText = "Какой цвет {1} {2} в последовательности в {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["flashed"] = "горел",
                    ["was among the colors flashed"] = "был среди цветов на этапе",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                },
            },

            // Simon Subdivides
            // What color was the button at this position in {0}?
            // What color was the button at this position in Simon Subdivides?
            [Question.SimonSubdividesButton] = new()
            {
                QuestionText = "Какого цвета была кнопка на этой позиции в {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Синий",
                    ["Green"] = "Зелёный",
                    ["Red"] = "Красный",
                    ["Violet"] = "Фиолетовый",
                },
            },

            // Simon Supports
            // What was the {1} topic in {0}?
            // What was the first topic in Simon Supports?
            [Question.SimonSupportsTopics] = new()
            {
                QuestionText = "Какая была {1}-я тема в {0}?",
                Answers = new Dictionary<string, string>
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

            // Simon Swizzles
            // Where was {1} in {0}?
            // Where was OFF in Simon Swizzles?
            [Question.SimonSwizzlesButton] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Где было {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["OFF"] = "OFF",
                    ["ON"] = "ON",
                },
            },
            // What was the hidden number in {0}?
            // What was the hidden number in Simon Swizzles?
            [Question.SimonSwizzlesNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the hidden number in {0}?",
            },

            // Simultaneous Simons
            // What color flashed {1} on the {2} Simon in {0}?
            // What color flashed first on the first Simon in Simultaneous Simons?
            [Question.SimultaneousSimonsFlash] = new()
            {
                QuestionText = "Какой цвет горел {1}-м на {2}-м Саймоне в {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Синий",
                    ["Yellow"] = "Жёлтый",
                    ["Red"] = "Красный",
                    ["Green"] = "Зелёный",
                },
            },

            // Skewed Slots
            // What were the original numbers in {0}?
            // What were the original numbers in Skewed Slots?
            [Question.SkewedSlotsOriginalNumbers] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какие были изначальные цифры {0}?",
                ModuleName = "Искажённых слотах",
            },

            // Skewers
            // What color was this gem in {0}?
            // What color was this gem in Skewers?
            [Question.SkewersColor] = new()
            {
                QuestionText = "Какого цвета был этот камень {0}?",
                Answers = new Dictionary<string, string>
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

            // Skyrim
            // Which race was selectable, but not the solution, in {0}?
            // Which race was selectable, but not the solution, in Skyrim?
            [Question.SkyrimRace] = new()
            {
                QuestionText = "Какая раса присутствовала, но не являлась решением {0}?",
                Answers = new Dictionary<string, string>
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
            // Which weapon was selectable, but not the solution, in {0}?
            // Which weapon was selectable, but not the solution, in Skyrim?
            [Question.SkyrimWeapon] = new()
            {
                QuestionText = "Какое оружие присутствовало, но не являлось решением {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Axe of Whiterun"] = "Axe of Whiterun",
                    ["Dawnbreaker"] = "Dawnbreaker",
                    ["Windshear"] = "Windshear",
                    ["Blade of Woe"] = "Blade of Woe",
                    ["Firiniel’s End"] = "Firiniel’s End",
                    ["Bow of Hunt"] = "Bow of Hunt",
                    ["Volendrung"] = "Volendrung",
                    ["Chillrend"] = "Chillrend",
                    ["Mace of Molag Bal"] = "Mace of Molag Bal",
                },
            },
            // Which enemy was selectable, but not the solution, in {0}?
            // Which enemy was selectable, but not the solution, in Skyrim?
            [Question.SkyrimEnemy] = new()
            {
                QuestionText = "Какой враг присутствовал, но не являлся решением {0}?",
                Answers = new Dictionary<string, string>
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
            // Which city was selectable, but not the solution, in {0}?
            // Which city was selectable, but not the solution, in Skyrim?
            [Question.SkyrimCity] = new()
            {
                QuestionText = "Какой город присутствовал, но не являлся решением {0}?",
                Answers = new Dictionary<string, string>
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
            // Which dragon shout was selectable, but not the solution, in {0}?
            // Which dragon shout was selectable, but not the solution, in Skyrim?
            [Question.SkyrimDragonShout] = new()
            {
                QuestionText = "Какой крик дракона присутствовал, но не являлся решением {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Disarm"] = "Disarm",
                    ["Dismay"] = "Dismay",
                    ["Dragonrend"] = "Dragonrend",
                    ["Fire Breath"] = "Fire Breath",
                    ["Ice Form"] = "Ice Form",
                    ["Kyne’s Peace"] = "Kyne’s Peace",
                    ["Slow Time"] = "Slow Time",
                    ["Unrelenting Force"] = "Unrelenting Force",
                    ["Whirlwind Sprint"] = "Whirlwind Sprint",
                },
            },

            // Slow Math
            // What was the last triplet of letters in {0}?
            // What was the last triplet of letters in Slow Math?
            [Question.SlowMathLastLetters] = new()
            {
                QuestionText = "Какие три буквы были последними {0}?",
            },

            // Small Circle
            // How much did the sequence shift by in {0}?
            // How much did the sequence shift by in Small Circle?
            [Question.SmallCircleShift] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "На сколько сместилась последовательность на {0}?",
            },
            // Which wedge made the different noise in the beginning of {0}?
            // Which wedge made the different noise in the beginning of Small Circle?
            [Question.SmallCircleWedge] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой сегмент {0} издал другой звук в начале?",
                Answers = new Dictionary<string, string>
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
            // Which color was {1} in the solution to {0}?
            // Which color was first in the solution to Small Circle?
            [Question.SmallCircleSolution] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой цвет был {1}-м в решении на {0}?",
                Answers = new Dictionary<string, string>
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

            // Small Talk
            // What was on the display in the {1} stage of {0}?
            // What was on the display in the first stage of Small Talk?
            [Question.SmallTalkDisplays] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was on the display in the {1} stage of {0}?",
            },

            // Smash, Marry, Kill
            // In what category was {1} for {0}?
            // In what category was The Button for Smash, Marry, Kill?
            [Question.SmashMarryKillCategory] = new()
            {
                QuestionText = "В какой категории был {1} {0}?",
            },
            // Which module was in the {1} category for {0}?
            // Which module was in the SMASH category for Smash, Marry, Kill?
            [Question.SmashMarryKillModule] = new()
            {
                QuestionText = "Какой модуль был в {1} категории {0}?",
            },

            // Snooker
            // How many red balls were there at the start of {0}?
            // How many red balls were there at the start of Snooker?
            [Question.SnookerReds] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Сколько красных шаров было в начале {0}?",
            },

            // Snowflakes
            // Which snowflake was on the {1} button of {0}?
            // Which snowflake was on the top button of Snowflakes?
            [Question.SnowflakesDisplayedSnowflakes] = new()
            {
                QuestionText = "Какая снежинка была на {1} кнопке {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхней",
                    ["right"] = "правой",
                    ["bottom"] = "нижней",
                    ["left"] = "левой",
                },
            },

            // Sonic & Knuckles
            // Which sound was played but not featured in the chosen zone in {0}?
            // Which sound was played but not featured in the chosen zone in Sonic & Knuckles?
            [Question.SonicKnucklesSounds] = new()
            {
                QuestionText = "Какой звук воспроизводился, но не присутствовал в выбранной зоне {0}?",
            },
            // Which badnik was shown in {0}?
            // Which badnik was shown in Sonic & Knuckles?
            [Question.SonicKnucklesBadnik] = new()
            {
                QuestionText = "Какой бадник был показан {0}?",
            },
            // Which monitor was shown in {0}?
            // Which monitor was shown in Sonic & Knuckles?
            [Question.SonicKnucklesMonitor] = new()
            {
                QuestionText = "Какой монитор был показан {0}?",
            },

            // Sonic The Hedgehog
            // What was the {1} picture on {0}?
            // What was the first picture on Sonic The Hedgehog?
            [Question.SonicTheHedgehogPictures] = new()
            {
                QuestionText = "Какая была {1}-я картинка {0}?",
            },
            // Which sound was played by the {1} screen on {0}?
            // Which sound was played by the Running Boots screen on Sonic The Hedgehog?
            [Question.SonicTheHedgehogSounds] = new()
            {
                QuestionText = "Какой звук воспроизводился на экране \"{1}\" {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Running Boots"] = "Running Boots",
                    ["Invincibility"] = "Invincibility",
                    ["Extra Life"] = "Extra Life",
                    ["Rings"] = "Rings",
                },
            },

            // Sorting
            // What positions were the last swap used to solve {0}?
            // What positions were the last swap used to solve Sorting?
            [Question.SortingLastSwap] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Какие позиции участвовали в последней замене чисел {0}?",
                ModuleName = "Сортировке",
            },

            // Souvenir
            // What was the first module asked about in the other Souvenir on this bomb?
            // What was the first module asked about in the other Souvenir on this bomb?
            [Question.SouvenirFirstQuestion] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "О каком модуле был первый вопрос на другом Сувенире?",
                ModuleName = "Сувенире",
            },

            // Space Traders
            // What was the maximum tax amount per vessel in {0}?
            // What was the maximum tax amount per vessel in Space Traders?
            [Question.SpaceTradersMaxTax] = new()
            {
                QuestionText = "Какой был максимальный налог за каждое судно {0}?",
            },

            // Spelling Bee
            // What word was asked to be spelled in {0}?
            // What word was asked to be spelled in Spelling Bee?
            [Question.SpellingBeeWord] = new()
            {
                QuestionText = "Какое слово нужно было произнести {0}?",
                ModuleName = "Правописании",
            },

            // The Sphere
            // What was the {1} flashed color in {0}?
            // What was the first flashed color in The Sphere?
            [Question.SphereColors] = new()
            {
                QuestionText = "Какой цвет загорелся {1}-м {0}?",
                Answers = new Dictionary<string, string>
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

            // Splitting The Loot
            // What bag was initially colored in {0}?
            // What bag was initially colored in Splitting The Loot?
            [Question.SplittingTheLootColoredBag] = new()
            {
                QuestionText = "Какой мешок был изначально окрашен {0}?",
                ModuleName = "Разделении добычи",
            },

            // Spongebob Birthday Identification
            // Who was the {1} child displayed in {0}?
            // Who was the first child displayed in Spongebob Birthday Identification?
            [Question.SpongebobBirthdayIdentificationChildren] = new()
            {
                QuestionText = "Кто был {1}-м показаным ребёнком {0}?",
            },

            // Stability
            // What was the color of the {1} lit LED in {0}?
            // What was the color of the first lit LED in Stability?
            [Question.StabilityLedColors] = new()
            {
                QuestionText = "Какого цвета был {1}-й горящик светодиод {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Yellow"] = "Жёлтый",
                    ["Blue"] = "Синий",
                },
            },
            // What was the identification number in {0}?
            // What was the identification number in Stability?
            [Question.StabilityIdNumber] = new()
            {
                QuestionText = "Какое было идентификационное число {0}?",
            },

            // Stable Time Signatures
            // What was the {1} time signature in {0}?
            // What was the first time signature in Stable Time Signatures?
            [Question.StableTimeSignaturesSignatures] = new()
            {
                QuestionText = "Какая была {1}-я сигнатура времени {0}?",
            },

            // Stacked Sequences
            // Which of these is the length of a sequence in {0}?
            // Which of these is the length of a sequence in Stacked Sequences?
            [Question.StackedSequences] = new()
            {
                QuestionText = "Который ответ является длиной последовательности {0}?",
            },

            // Stars
            // What was the digit in the center of {0}?
            // What was the digit in the center of Stars?
            [Question.StarsCenter] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая цифра была в центре {0}?",
            },

            // Starstruck
            // Which star was present on {0}?
            // Which star was present on Starstruck?
            [Question.StarstruckStar] = new()
            {
                QuestionText = "Какая звезда присутствовала {0}?",
            },

            // State of Aggregation
            // What was the element shown in {0}?
            // What was the element shown in State of Aggregation?
            [Question.StateOfAggregationElement] = new()
            {
                QuestionText = "Какой элемент был отображён {0}?",
            },

            // Stellar
            // What was the {1} letter in {0}?
            // What was the Morse code letter in Stellar?
            [Question.StellarLetters] = new()
            {
                QuestionText = "Какая была буква в {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Morse code"] = "коде Морзе",
                    ["tap code"] = "коде стука",
                    ["Braille"] = "Браилле",
                },
            },

            // Stroop’s Test
            // What was the {1} submitted word in {0}?
            // What was the first submitted word in Stroop’s Test?
            [Question.StroopsTestWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} submitted word in {0}?",
            },
            // What was the {1} submitted word’s color in {0}?
            // What was the first submitted word’s color in Stroop’s Test?
            [Question.StroopsTestColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} submitted word’s color in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Yellow"] = "Yellow",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Magenta"] = "Magenta",
                    ["White"] = "White",
                },
            },

            // Stupid Slots
            // What was the value of the {1} arrow in {0}?
            // What was the value of the top-left arrow in Stupid Slots?
            [Question.StupidSlotsValues] = new()
            {
                QuestionText = "Какое было значение {1} стрелки {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхней левой",
                    ["top-middle"] = "верхней средней",
                    ["top-right"] = "верхней правой",
                    ["bottom-left"] = "нижней левой",
                    ["bottom-middle"] = "нижней средней",
                    ["bottom-right"] = "нижней правой",
                },
            },

            // Subbly Jubbly
            // What was a substitution word in {0}?
            // What was a substitution word in Subbly Jubbly?
            [Question.SubblyJubblySubstitutions] = new()
            {
                QuestionText = "На какое слово была замена {0}?",
            },

            // Subscribe to Pewdiepie
            // How many subscribers does {1} have in {0}?
            // How many subscribers does PewDiePie have in Subscribe to Pewdiepie?
            [Question.SubscribeToPewdiepieSubCount] = new()
            {
                QuestionText = "Сколько подписчиков было у {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["PewDiePie"] = "PewDiePie",
                    ["T-Series"] = "T-Series",
                },
            },

            // Subway
            // Which bread did the customer ask for in {0}?
            // Which bread did the customer ask for in Subway?
            [Question.SubwayBread] = new()
            {
                QuestionText = "Какой хлеб попросил покупатель {0}?",
            },
            // Which of these was not asked for in {0}?
            // Which of these was not asked for in Subway?
            [Question.SubwayItems] = new()
            {
                QuestionText = "Что из этого покупатель не просил {0}?",
            },

            // Sugar Skulls
            // What skull was shown on the {1} square in {0}?
            // What skull was shown on the top square in Sugar Skulls?
            [Question.SugarSkullsSkull] = new()
            {
                QuestionText = "Какой череп был показан на {1} квадрате {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["bottom-left"] = "нижнем левом",
                    ["bottom-right"] = "нижнем правом",
                },
            },
            // Which skull {1} present in {0}?
            // Which skull was present in Sugar Skulls?
            [Question.SugarSkullsAvailability] = new()
            {
                QuestionText = "Какой череп {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "присутствовал",
                    ["was not"] = "отсутствовал",
                },
            },

            // Suits And Colours
            // What was the colour of this cell in {0}?
            // What was the colour of this cell in Suits And Colours?
            [Question.SuitsAndColourColour] = new()
            {
                QuestionText = "Какой был цвет этой клетки {0}?",
            },
            // What was the suit of this cell in {0}?
            // What was the suit of this cell in Suits And Colours?
            [Question.SuitsAndColourSuit] = new()
            {
                QuestionText = "Какая была масть этой клетки {0}?",
            },

            // Superparsing
            // What was the displayed word in {0}?
            // What was the displayed word in Superparsing?
            [Question.SuperparsingDisplayed] = new()
            {
                QuestionText = "Какое слово было показано {0}?",
            },

            // SUSadmin
            // Which security protocol was installed in {0}?
            // Which security protocol was installed in SUSadmin?
            [Question.SUSadminSecurity] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой протокол безопасности был установлен на {0}?",
            },

            // The Switch
            // What color was the {1} LED on the {2} flip of {0}?
            // What color was the top LED on the first flip of The Switch?
            [Question.SwitchInitialColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета был {1} светодиод при {2}-м нажатии {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхний",
                    ["bottom"] = "нижний",
                },
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красного",
                    ["orange"] = "Оранжевого",
                    ["yellow"] = "Жёлтого",
                    ["green"] = "Зелёного",
                    ["blue"] = "Синего",
                    ["purple"] = "Фиолетового",
                },
            },

            // Switches
            // What was the initial position of the switches in {0}?
            // What was the initial position of the switches in Switches?
            [Question.SwitchesInitialPosition] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое было начальное положение {0}?",
            },

            // Switching Maze
            // What was the seed in {0}?
            // What was the seed in Switching Maze?
            [Question.SwitchingMazeSeed] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое было зерно у {0}?",
            },
            // What was the starting maze color in {0}?
            // What was the starting maze color in Switching Maze?
            [Question.SwitchingMazeColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был цвет начального {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Magenta"] = "Magenta",
                    ["Orange"] = "Orange",
                    ["Red"] = "Red",
                    ["White"] = "White",
                },
            },

            // Symbol Cycle
            // How many symbols were cycling on the {1} screen in {0}?
            // How many symbols were cycling on the left screen in Symbol Cycle?
            [Question.SymbolCycleSymbolCounts] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Сколько символов было на {1} экране {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левом",
                    ["right"] = "правом",
                },
            },

            // Symbolic Coordinates
            // What was the {1} symbol in the {2} stage of {0}?
            // What was the left symbol in the first stage of Symbolic Coordinates?
            [Question.SymbolicCoordinateSymbols] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был {1} символ на {2}-м этапе {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левый",
                    ["middle"] = "центральный",
                    ["right"] = "правый",
                },
            },

            // Symbolic Tasha
            // Which button flashed {1} in the final sequence of {0}?
            // Which button flashed first in the final sequence of Symbolic Tasha?
            [Question.SymbolicTashaFlashes] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая кнопка горела {1}-й в финальной последовательности {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Top"] = "Верхняя",
                    ["Right"] = "Правая",
                    ["Bottom"] = "Нижняя",
                    ["Left"] = "Левая",
                    ["Pink"] = "Розовая",
                    ["Green"] = "Зелёная",
                    ["Yellow"] = "Жёлтая",
                    ["Blue"] = "Синяя",
                },
            },
            // Which symbol was on the {1} button in {0}?
            // Which symbol was on the top button in Symbolic Tasha?
            [Question.SymbolicTashaSymbols] = new()
            {
                QuestionText = "Какой символ был на {1} кнопке {0}?",
                FormatArgs = new Dictionary<string, string>
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

            // Synapse Says
            // What color flashed {1} in the {2} stage of {0}?
            // What color flashed first in the first stage of Synapse Says?
            [Question.SynapseSaysFlashes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color flashed {1} in the {2} stage of {0}?",
            },
            // What color was in the {1} position of the {2} stage of {0}?
            // What color was in the first position of the first stage of Synapse Says?
            [Question.SynapseSaysPositions] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was in the {1} position of the {2} stage of {0}?",
            },
            // What number was displayed in the {1} stage of {0}?
            // What number was displayed in the first stage of Synapse Says?
            [Question.SynapseSaysDisplays] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What number was displayed in the {1} stage of {0}?",
            },

            // SYNC-125 [3]
            // What was displayed on the screen in the {1} stage of {0}?
            // What was displayed on the screen in the first stage of SYNC-125 [3]?
            [Question.Sync125_3Word] = new()
            {
                QuestionText = "Что было на экране на {1}-м этапе {0}?",
            },

            // Synonyms
            // Which number was displayed on {0}?
            // Which number was displayed on Synonyms?
            [Question.SynonymsNumber] = new()
            {
                QuestionText = "Какое число было отображено {0}?",
            },

            // Sysadmin
            // What error code did you fix in {0}?
            // What error code did you fix in Sysadmin?
            [Question.SysadminFixedErrorCodes] = new()
            {
                QuestionText = "Какой код ошибки вы исправили {0}?",
            },

            // Tap Code
            // What was the received word in {0}?
            // What was the received word in Tap Code?
            [Question.TapCodeReceivedWord] = new()
            {
                QuestionText = "Какое слово было передано {0}?",
            },

            // Tasha Squeals
            // What was the {1} flashed color in {0}?
            // What was the first flashed color in Tasha Squeals?
            [Question.TashaSquealsColors] = new()
            {
                QuestionText = "Какой цвет горел {1}-м {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Pink"] = "Розовый",
                    ["Green"] = "Зелёный",
                    ["Yellow"] = "Жёлтый",
                    ["Blue"] = "Синий",
                },
            },

            // Tasque Managing
            // Where was the starting position in {0}?
            // Where was the starting position in Tasque Managing?
            [Question.TasqueManagingStartingPos] = new()
            {
                QuestionText = "Где была начальная позиция {0}?",
            },

            // The Tea Set
            // Which ingredient was displayed {1}, from left to right, in {0}?
            // Which ingredient was displayed first, from left to right, in The Tea Set?
            [Question.TeaSetDisplayedIngredients] = new()
            {
                QuestionText = "Какой ингридиент был показан {1}-м, слева направо {0}?",
            },

            // Technical Keypad
            // What was the {1} displayed digit in {0}?
            // What was the first displayed digit in Technical Keypad?
            // Note: This question is depicted visually, rather than with words. A translation here will only be used for logging.
            [Question.TechnicalKeypadDisplayedDigits] = new()
            {
                QuestionText = "Какая была {1}-я отображённая цифра {0}?",
            },

            // Ten-Button Color Code
            // What was the initial color of the {1} button in the {2} stage of {0}?
            // What was the initial color of the first button in the first stage of Ten-Button Color Code?
            [Question.TenButtonColorCodeInitialColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Какой был начальный цвет {1}-й кнопки на {2}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красный",
                    ["green"] = "Зелёный",
                    ["blue"] = "Синий",
                },
            },

            // Tenpins
            // What was the {1} split in {0}?
            // What was the red split in Tenpins?
            [Question.TenpinsSplits] = new()
            {
                QuestionText = "Какой был {1} сплит {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красный",
                    ["green"] = "зелёный",
                    ["blue"] = "синий",
                },
                Answers = new Dictionary<string, string>
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

            // Tetriamonds
            // What colour triangle pulsed {1} in {0}?
            // What colour triangle pulsed first in Tetriamonds?
            [Question.TetriamondsPulsingColours] = new()
            {
                QuestionText = "Какой цветной треугольник пульсировал {1}-м {0}?",
                Answers = new Dictionary<string, string>
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

            // Text Field
            // What was the displayed letter in {0}?
            // What was the displayed letter in Text Field?
            [Question.TextFieldDisplay] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая буква присутствовала на {0}?",
                ModuleName = "Поле из букв",
            },

            // Thinking Wires
            // What was the position from top to bottom of the first wire needing to be cut in {0}?
            // What was the position from top to bottom of the first wire needing to be cut in Thinking Wires?
            [Question.ThinkingWiresFirstWire] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Где находился первый провод который нужно было перерезать (сверху вниз) на {0}?",
            },
            // What color did the second valid wire to cut have to have in {0}?
            // What color did the second valid wire to cut have to have in Thinking Wires?
            [Question.ThinkingWiresSecondWire] = new()
            {
                QuestionText = "Какой цвет был у второго верно порезаного провода {0}?",
                Answers = new Dictionary<string, string>
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
            // What was the display number in {0}?
            // What was the display number in Thinking Wires?
            [Question.ThinkingWiresDisplayNumber] = new()
            {
                QuestionText = "Какое было число на экране {0}?",
            },

            // Third Base
            // What was the display word in the {1} stage on {0}?
            // What was the display word in the first stage on Third Base?
            [Question.ThirdBaseDisplay] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое слово было на экране на {1}-м этапе {0}?",
                ModuleName = "\"А меня – Сава\"",
            },

            // Thirty Dollar Module
            // Which sound was used in {0}?
            // Which sound was used in Thirty Dollar Module?
            [Question.ThirtyDollarModuleSounds] = new()
            {
                QuestionText = "Какой звук был использован {0}?",
            },

            // Tic Tac Toe
            // What was on the {1} button at the start of {0}?
            // What was on the top-left button at the start of Tic Tac Toe?
            [Question.TicTacToeInitialState] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Что было на {1} кнопке в начале игры {0}?",
                ModuleName = "Крестиках-ноликах",
                FormatArgs = new Dictionary<string, string>
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

            // Time Signatures
            // What was the {1} time signature in {0}?
            // What was the first time signature in Time Signatures?
            [Question.TimeSignaturesSignatures] = new()
            {
                QuestionText = "Какая была {1}-я сигнатура времени {0}?",
            },

            // Timezone
            // What was the {1} city in {0}?
            // What was the departure city in Timezone?
            [Question.TimezoneCities] = new()
            {
                QuestionText = "Какой был город {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["departure"] = "отправления",
                    ["destination"] = "прибытия",
                },
            },

            // Tip Toe
            // Which of these squares was safe in row {1} in {0}?
            // Which of these squares was safe in row 9 in Tip Toe?
            [Question.TipToeSafeSquares] = new()
            {
                QuestionText = "Какой из этих квадратов был безопасным в {1}-м ряду {0}?",
            },

            // Topsy Turvy
            // What was the word initially shown in {0}?
            // What was the word initially shown in Topsy Turvy?
            [Question.TopsyTurvyWord] = new()
            {
                QuestionText = "Какое было начальное слово {0}?",
            },

            // Touch Transmission
            // What was the transmitted word in {0}?
            // What was the transmitted word in Touch Transmission?
            [Question.TouchTransmissionWord] = new()
            {
                QuestionText = "Какое слово было передано {0}?",
            },
            // In what order was the Braille read in {0}?
            // In what order was the Braille read in Touch Transmission?
            [Question.TouchTransmissionOrder] = new()
            {
                QuestionText = "Какой порядок чтения был у Браилля {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Standard Braille Order"] = "Standard Braille Order",
                    ["Individual Reading Order"] = "Individual Reading Order",
                    ["Merged Reading Order"] = "Merged Reading Order",
                    ["Chinese Reading Order"] = "Chinese Reading Order",
                },
            },

            // Transmitted Morse
            // What was the {1} received message in {0}?
            // What was the first received message in Transmitted Morse?
            [Question.TransmittedMorseMessage] = new()
            {
                QuestionText = "Какое было {1}-е полученное сообщение {0}?",
            },

            // Triamonds
            // What colour triangle pulsed {1} in {0}?
            // What colour triangle pulsed first in Triamonds?
            [Question.TriamondsPulsingColours] = new()
            {
                QuestionText = "Какого цвета был {1}-й пульсирующий треугольник {0}?",
                Answers = new Dictionary<string, string>
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

            // Tribal Council
            // What was the {1} name in {0}?
            // What was the northeast name in Tribal Council?
            [Question.TribalCouncilName] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Who was your closest ally in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["northeast"] = "northeast",
                    ["southwest"] = "southwest",
                },
            },

            // Triple Term
            // Which of these was one of the passwords in {0}?
            // Which of these was one of the passwords in Triple Term?
            [Question.TripleTermPasswords] = new()
            {
                QuestionText = "Что из этого было одним из паролей {0}?",
            },

            // Turtle Robot
            // What was the {1} line you commented out in {0}?
            // What was the first line you commented out in Turtle Robot?
            [Question.TurtleRobotCodeLines] = new()
            {
                QuestionText = "Какую строку вы закомментировали {1}-й {0}?",
            },

            // Two Bits
            // What was the {1} correct query response from {0}?
            // What was the first correct query response from Two Bits?
            [Question.TwoBitsResponse] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какой был ответ на {1}-й запрос {0}?",
                ModuleName = "Двух битах",
            },

            // Ultimate Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Ultimate Cipher?
            [Question.UltimateCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "среднем",
                    ["bottom"] = "нижнем",
                },
            },

            // Ultimate Cycle
            // Which direction was the {1} dial pointing in {0}?
            // Which direction was the first dial pointing in Ultimate Cycle?
            [Question.UltimateCycleDialDirections] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which direction was the {1} dial pointing in {0}?",
            },
            // What letter was written on the {1} dial in {0}?
            // What letter was written on the first dial in Ultimate Cycle?
            [Question.UltimateCycleDialLabels] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What letter was written on the {1} dial in {0}?",
            },

            // The Ultracube
            // What was the {1} rotation in {0}?
            // What was the first rotation in The Ultracube?
            [Question.UltracubeRotations] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Каким было {1}-е вращение {0}?",
            },

            // UltraStores
            // What was the {1} rotation in the {2} stage of {0}?
            // What was the first rotation in the first stage of UltraStores?
            [Question.UltraStoresSingleRotation] = new()
            {
                QuestionText = "Какой был {1}-й поворот на {2}-м этапе {0}?",
            },
            // What was the {1} rotation in the {2} stage of {0}?
            // What was the first rotation in the first stage of UltraStores?
            [Question.UltraStoresMultiRotation] = new()
            {
                QuestionText = "Какой был {1}-й поворот на {2}-м этапе {0}?",
            },

            // Uncolored Squares
            // What was the {1} color in reading order used in the first stage of {0}?
            // What was the first color in reading order used in the first stage of Uncolored Squares?
            [Question.UncoloredSquaresFirstStage] = new()
            {
                Conjugation = Conjugation.GenitivePlural,
                QuestionText = "Какой был {1}-й цвет в порядке чтения, использованный на первом этапе {0}?",
                ModuleName = "Неокрашенных квадратов",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Белый",
                    ["Red"] = "Красный",
                    ["Blue"] = "Синий",
                    ["Green"] = "Зелёный",
                    ["Yellow"] = "Жёлтый",
                    ["Magenta"] = "Розовый",
                },
            },

            // Uncolored Switches
            // What was the initial state of the switches in {0}?
            // What was the initial state of the switches in Uncolored Switches?
            [Question.UncoloredSwitchesInitialState] = new()
            {
                Conjugation = Conjugation.GenitivePlural,
                QuestionText = "Какое было исходное состояние {0}?",
                ModuleName = "Бесцветных переключателей",
            },
            // What color was the {1} LED in reading order in {0}?
            // What color was the first LED in reading order in Uncolored Switches?
            [Question.UncoloredSwitchesLedColors] = new()
            {
                Conjugation = Conjugation.GenitivePlural,
                QuestionText = "Какого цвета был {1}-й светодиод в порядке чтения {0}?",
                ModuleName = "Бесцветных переключателей",
                Answers = new Dictionary<string, string>
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

            // Uncolour Flash
            // What was the {1} in the {2} position of the {3} sequence of {0}?
            // What was the word in the first position of the “YES” sequence of Uncolour Flash?
            [Question.UncolourFlashDisplays] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} displayed in the {2} sequence of {0}?",
            },

            // Unfair Cipher
            // What was the {1} received instruction in {0}?
            // What was the first received instruction in Unfair Cipher?
            [Question.UnfairCipherInstructions] = new()
            {
                QuestionText = "Какая {1}-я инструкция была зашифрована {0}?",
            },

            // Unfair’s Revenge
            // What was the {1} decrypted instruction in {0}?
            // What was the first decrypted instruction in Unfair’s Revenge?
            [Question.UnfairsRevengeInstructions] = new()
            {
                QuestionText = "Какая {1}-я инструкция была зашифрована {0}?",
            },

            // Unicode
            // What was the {1} submitted code in {0}?
            // What was the first submitted code in Unicode?
            [Question.UnicodeSortedAnswer] = new()
            {
                QuestionText = "Какой был {1}-й отправленный ответ {0}?",
            },

            // UNO!
            // What was the initial card in {0}?
            // What was the initial card in UNO!?
            [Question.UnoInitialCard] = new()
            {
                QuestionText = "Какая была начальная карта {0}?",
                Answers = new Dictionary<string, string>
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

            // Unordered Keys
            // What color was this key in the {1} stage of {0}?
            // What color was this key in the first stage of Unordered Keys?
            [Question.UnorderedKeysKeyColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была эта клавиша на {1}-м этапе {0}?",
            },
            // What color was the label of this key in the {1} stage of {0}?
            // What color was the label of this key in the first stage of Unordered Keys?
            [Question.UnorderedKeysLabelColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета была надпись на этой клавише на {1}-м этапе {0}?",
            },
            // What was the label of this key in the {1} stage of {0}?
            // What was the label of this key in the first stage of Unordered Keys?
            [Question.UnorderedKeysLabel] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какая была надпись на этой клавише на {1}-м этапе {0}?",
            },

            // Unown Cipher
            // What was the {1} submitted letter in {0}?
            // What was the first submitted letter in Unown Cipher?
            [Question.UnownCipherAnswers] = new()
            {
                QuestionText = "Какая буква была отправлена {1}-й {0}?",
            },

            // Unpleasant Squares
            // What was the color of this square in {0}?
            // What was the color of this square in Unpleasant Squares?
            [Question.UnpleasantSquaresColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of this square in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Yellow"] = "Yellow",
                    ["Jade"] = "Jade",
                    ["Azure"] = "Azure",
                    ["Violet"] = "Violet",
                },
            },

            // Updog
            // What was the text on {0}?
            // What was the text on Updog?
            [Question.UpdogWord] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой был текст на {0}?",
            },
            // What was the {1} color in the sequence on {0}?
            // What was the first color in the sequence on Updog?
            [Question.UpdogColor] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был {1} цвет в последовательности {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first"] = "первый",
                    ["last"] = "последний",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Yellow"] = "Жёлтый",
                    ["Orange"] = "Оранжевый",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                    ["Purple"] = "Фиолетовый",
                },
            },

            // USA Cycle
            // Which state was displayed in {0}?
            // Which state was displayed in USA Cycle?
            [Question.USACycleDisplayed] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой штат был показан на {0}?",
            },

            // USA Maze
            // Which state did you depart from in {0}?
            // Which state did you depart from in USA Maze?
            [Question.USAMazeOrigin] = new()
            {
                QuestionText = "Из какого штата вы отправились {0}?",
                ModuleName = "Американском лабиринте",
            },

            // V
            // Which word {1} shown in {0}?
            // Which word was shown in V?
            [Question.VWords] = new()
            {
                QuestionText = "Какое слово {1} показано {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "было",
                    ["was not"] = "не было",
                },
            },

            // Valves
            // What was the initial state of {0}?
            // What was the initial state of Valves?
            [Question.ValvesInitialState] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое было начальное состояние {0}?",
            },

            // Varicolored Squares
            // What was the initially pressed color on {0}?
            // What was the initially pressed color on Varicolored Squares?
            [Question.VaricoloredSquaresInitialColor] = new()
            {
                Conjugation = Conjugation.PrepositivePlural,
                QuestionText = "Какой был первый нажатый цвет на {0}?",
                ModuleName = "Разноцветных квадратах",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Белый",
                    ["Red"] = "Красный",
                    ["Blue"] = "Синий",
                    ["Green"] = "Зелёный",
                    ["Yellow"] = "Жёлтый",
                    ["Magenta"] = "Розовый",
                },
            },

            // Varicolour Flash
            // What was the word of the {1} goal in {0}?
            // What was the word of the first goal in Varicolour Flash?
            [Question.VaricolourFlashWords] = new()
            {
                QuestionText = "Какое было слово у {1}-й цели {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                    ["White"] = "White",
                },
            },
            // What was the color of the {1} goal in {0}?
            // What was the color of the first goal in Varicolour Flash?
            [Question.VaricolourFlashColors] = new()
            {
                QuestionText = "Какой был цвет у {1}-й цели {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Yellow",
                    ["White"] = "White",
                },
            },

            // Variety
            // What color was the LED flashing in {0}?
            // What color was the LED flashing in Variety?
            [Question.VarietyLED] = new()
            {
                QuestionText = "Каким цветом горел светодиод {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красным",
                    ["Yellow"] = "Жёлтым",
                    ["Blue"] = "Синим",
                    ["White"] = "Белым",
                    ["Black"] = "Чёрным",
                },
            },
            // What digit was displayed but not the answer for the digit display in {0}?
            // What digit was displayed but not the answer for the digit display in Variety?
            [Question.VarietyDigitDisplay] = new()
            {
                QuestionText = "Какая цифра была показана на цифровом экране, но не была для него ответом {0}?",
            },
            // What word could be formed but was not the answer for the letter display in {0}?
            // What word could be formed but was not the answer for the letter display in Variety?
            [Question.VarietyLetterDisplay] = new()
            {
                QuestionText = "Какое слово могло быть составленно на буквенном экране, но не было для него ответом {0}?",
            },
            // What was the maximum display for the {1}timer in {0}?
            // What was the maximum display for the timer in Variety?
            [Question.VarietyTimer] = new()
            {
                QuestionText = "Какой был максимальный экран на {1}таймере {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    [""] = "",
                    ["ascending "] = "возрастающем ",
                    ["descending "] = "убывающем ",
                },
            },
            // What was n for the {1}knob in {0}?
            // What was n for the knob in Variety?
            [Question.VarietyColoredKnob] = new()
            {
                QuestionText = "Чему было равно n у {1}ручки {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    [""] = "",
                    ["colored "] = "цветной ",
                    ["red "] = "красной ",
                    ["black "] = "чёрной ",
                    ["blue "] = "синей ",
                    ["yellow "] = "жёлтой ",
                },
            },
            // What was n for the {1}bulb in {0}?
            // What was n for the bulb in Variety?
            [Question.VarietyBulb] = new()
            {
                QuestionText = "Чему было равно n у {1}лампочки {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    [""] = "",
                    ["red "] = "красной ",
                    ["yellow "] = "жёлтой ",
                },
            },

            // Vcrcs
            // What was the word in {0}?
            // What was the word in Vcrcs?
            [Question.VcrcsWord] = new()
            {
                QuestionText = "Какое было слово {0}?",
            },

            // Vectors
            // What was the color of the {1} vector in {0}?
            // What was the color of the first vector in Vectors?
            [Question.VectorsColors] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какого цвета был {1} вектор из {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first"] = "1-й",
                    ["second"] = "2-й",
                    ["third"] = "3-й",
                    ["only"] = "единственный",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красного",
                    ["Orange"] = "Оранжевого",
                    ["Yellow"] = "Жёлтого",
                    ["Green"] = "Зелёного",
                    ["Blue"] = "Синего",
                    ["Purple"] = "Фиолетового",
                },
            },

            // Vexillology
            // What was the {1} flagpole color on {0}?
            // What was the first flagpole color on Vexillology?
            [Question.VexillologyColors] = new()
            {
                QuestionText = "Какого цвета был {1}-й флагшток {0}?",
                Answers = new Dictionary<string, string>
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

            // Violet Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Violet Cipher?
            [Question.VioletCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Visual Impairment
            // What was the desired color in the {1} stage on {0}?
            // What was the desired color in the first stage on Visual Impairment?
            [Question.VisualImpairmentColors] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был целевой цвет на {1}-м этапе {0}?",
                ModuleName = "Повреждённого зрения",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Синий",
                    ["Green"] = "Зелёный",
                    ["Red"] = "Красный",
                    ["White"] = "Белый",
                },
            },

            // Walking Cube
            // Which of these cells was part of the cube’s path in {0}?
            // Which of these cells was part of the cube’s path in Walking Cube?
            [Question.WalkingCubePath] = new()
            {
                QuestionText = "Какая из этих клеток была частью пути кубика {0}?",
            },

            // Warning Signs
            // What was the displayed sign in {0}?
            // What was the displayed sign in Warning Signs?
            [Question.WarningSignsDisplayedSign] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какой знак был показан на {0}?",
            },

            // WASD
            // What was the location displayed in {0}?
            // What was the location displayed in WASD?
            [Question.WasdDisplayedLocation] = new()
            {
                QuestionText = "Какая локация была показана {0}?",
            },

            // Watching Paint Dry
            // How many brush strokes were heard in {0}?
            // How many brush strokes were heard in Watching Paint Dry?
            [Question.WatchingPaintDryStrokeCount] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Сколько мазков кистью было слышно на {0}?",
            },

            // Wavetapping
            // What was the color on the {1} stage in {0}?
            // What was the color on the first stage in Wavetapping?
            [Question.WavetappingColors] = new()
            {
                QuestionText = "Какой цвет был на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
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
            // What was the correct pattern on the {1} stage in {0}?
            // What was the correct pattern on the first stage in Wavetapping?
            [Question.WavetappingPatterns] = new()
            {
                QuestionText = "Какой был верный узор на {1}-м этапе {0}?",
            },

            // The Weakest Link
            // Who did you eliminate in {0}?
            // Who did you eliminate in The Weakest Link?
            [Question.WeakestLinkElimination] = new()
            {
                QuestionText = "Кого вы устранили {0}?",
            },
            // Who made it to the Money Phase with you in {0}?
            // Who made it to the Money Phase with you in The Weakest Link?
            [Question.WeakestLinkMoneyPhaseName] = new()
            {
                QuestionText = "Кто дошёл до Money Phase с вами {0}?",
            },
            // What ratio did {1} get in the Question Phase in {0}?
            // What ratio did Annie get in the Question Phase in The Weakest Link?
            [Question.WeakestLinkRatio] = new()
            {
                QuestionText = "На какой процент вопросов ответил(а) {1} в Question Phase {0}?",
            },
            // What was {1}’s skill in {0}?
            // What was Annie’s skill in The Weakest Link?
            [Question.WeakestLinkSkill] = new()
            {
                QuestionText = "Какой навык был у {1} {0}?",
            },

            // What’s on Second
            // What was the display text in the {1} stage of {0}?
            // What was the display text in the first stage of What’s on Second?
            [Question.WhatsOnSecondDisplayText] = new()
            {
                QuestionText = "Какой текст был на {1}-м этапе {0}?",
            },
            // What was the display text color in the {1} stage of {0}?
            // What was the display text color in the first stage of What’s on Second?
            [Question.WhatsOnSecondDisplayColor] = new()
            {
                QuestionText = "Какого цвета был текст на {1}-м этапе {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Green"] = "Green",
                    ["Magenta"] = "Magenta",
                    ["Red"] = "Red",
                    ["Yellow"] = "Yellow",
                },
            },

            // White Arrows
            // What was the {1} non-white arrow in {0}?
            // What was the first non-white arrow in White Arrows?
            [Question.WhiteArrowsArrows] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} non-white arrow in {0}?",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
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

            // White Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in White Cipher?
            [Question.WhiteCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // WhoOF
            // What was the display in the {1} stage on {0}?
            // What was the display in the first stage on WhoOF?
            [Question.WhoOFDisplay] = new()
            {
                QuestionText = "Что было на экране на {1}-м этапе {0}?",
            },

            // Who’s on First
            // What was the display in the {1} stage on {0}?
            // What was the display in the first stage on Who’s on First?
            [Question.WhosOnFirstDisplay] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое слово было на экране на {1}-м этапе {0}?",
                ModuleName = "\"Меня зовут Авас, а Вас\"",
            },

            // Who’s on Gas
            // What was the display in the first phase of the {1} stage on {0}?
            // What was the display in the first phase of the first stage on Who’s on Gas?
            [Question.WhosOnGasDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the display in the {1} stage on {0}?",
            },

            // Who’s on Morse
            // What word was transmitted in the {1} stage on {0}?
            // What word was transmitted in the first stage on Who’s on Morse?
            [Question.WhosOnMorseTransmitDisplay] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какое слово было передано на {1}-м этапе {0}?",
            },

            // The Wire
            // What was the color of the {1} dial in {0}?
            // What was the color of the top dial in The Wire?
            [Question.WireDialColors] = new()
            {
                QuestionText = "Какого цвета был {1} диск {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхний",
                    ["bottom-left"] = "нижний левый",
                    ["bottom-right"] = "нижний правый",
                },
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "Синего",
                    ["green"] = "Зелёного",
                    ["grey"] = "Серого",
                    ["orange"] = "Оранжевого",
                    ["purple"] = "Фиолетового",
                    ["red"] = "Красного",
                },
            },
            // What was the displayed number in {0}?
            // What was the displayed number in The Wire?
            [Question.WireDisplayedNumber] = new()
            {
                QuestionText = "Какое было отображённое число {0}?",
            },

            // Wire Ordering
            // What color was the {1} display from the left in {0}?
            // What color was the first display from the left in Wire Ordering?
            [Question.WireOrderingDisplayColor] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какого цвета был {1}-й экран слева на {0}?",
                Answers = new Dictionary<string, string>
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
            // What number was on the {1} display from the left in {0}?
            // What number was on the first display from the left in Wire Ordering?
            [Question.WireOrderingDisplayNumber] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какое число было на {1}-м экране слева на {0}?",
            },
            // What color was the {1} wire from the left in {0}?
            // What color was the first wire from the left in Wire Ordering?
            [Question.WireOrderingWireColor] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какого цвета был {1}-й провод слева на {0}?",
                Answers = new Dictionary<string, string>
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

            // Wire Sequence
            // How many {1} wires were there in {0}?
            // How many red wires were there in Wire Sequence?
            [Question.WireSequenceColorCount] = new()
            {
                Conjugation = Conjugation.в_PrepositiveFeminine,
                QuestionText = "Сколько было {1} проводов {0}?",
                ModuleName = "Последовательности проводов",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красных",
                    ["blue"] = "синих",
                    ["black"] = "чёрных",
                },
            },

            // Wolf, Goat, and Cabbage
            // Which of these was {1} on {0}?
            // Which of these was present on Wolf, Goat, and Cabbage?
            [Question.WolfGoatAndCabbageAnimals] = new()
            {
                QuestionText = "Что из этого {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["present"] = "присутствовало",
                    ["not present"] = "отсутствовало",
                },
            },
            // What was the boat size in {0}?
            // What was the boat size in Wolf, Goat, and Cabbage?
            [Question.WolfGoatAndCabbageBoatSize] = new()
            {
                QuestionText = "Какого размера была лодка {0}?",
            },

            // Word Count
            // What was the displayed number on {0}?
            // What was the displayed number on Word Count?
            [Question.WordCountNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed number on {0}?",
            },

            // Working Title
            // What was the label shown in {0}?
            // What was the label shown in Working Title?
            [Question.WorkingTitleLabel] = new()
            {
                Conjugation = Conjugation.PrepositiveMascNeuter,
                QuestionText = "Какая надпись была показана на {0}?",
                ModuleName = "Рабочем названии",
            },

            // Wumbo
            // What was the number in {0}?
            // What was the number in Wumbo?
            [Question.WumboNumber] = new()
            {
                QuestionText = "Какое было число {0}?",
            },

            // The Xenocryst
            // What was the color of the {1} flash in {0}?
            // What was the color of the first flash in The Xenocryst?
            [Question.Xenocryst] = new()
            {
                QuestionText = "Какого цвета была {1}-я вспышка {0}?",
            },

            // XmORse Code
            // What was the {1} displayed letter (in reading order) in {0}?
            // What was the first displayed letter (in reading order) in XmORse Code?
            [Question.XmORseCodeDisplayedLetters] = new()
            {
                QuestionText = "Какая была {1}-я показанная буква (в порядке чтения) {0}?",
            },
            // What word did you decrypt in {0}?
            // What word did you decrypt in XmORse Code?
            [Question.XmORseCodeWord] = new()
            {
                QuestionText = "Какое слово вы расшифровали {0}?",
            },

            // xobekuJ ehT
            // What song was played on {0}?
            // What song was played on xobekuJ ehT?
            [Question.XobekuJehTSong] = new()
            {
                QuestionText = "Какая песня звучала {0}?",
            },

            // X-Ring
            // Which symbol was scanned in {0}?
            // Which symbol was scanned in X-Ring?
            [Question.XRingSymbol] = new()
            {
                QuestionText = "Какой символ был просканирован {0}?",
            },

            // XY-Ray
            // Which shape was scanned by {0}?
            // Which shape was scanned by XY-Ray?
            [Question.XYRayShapes] = new()
            {
                QuestionText = "Какая фигура была просканирована {0}?",
            },

            // Yahtzee
            // What was the initial roll on {0}?
            // What was the initial roll on Yahtzee?
            [Question.YahtzeeInitialRoll] = new()
            {
                QuestionText = "Какой был первый бросок {0}?",
                ModuleName = "Покере на костях",
                Answers = new Dictionary<string, string>
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

            // Yellow Arrows
            // What was the starting row letter in {0}?
            // What was the starting row letter in Yellow Arrows?
            [Question.YellowArrowsStartingRow] = new()
            {
                Conjugation = Conjugation.в_PrepositivePlural,
                QuestionText = "Какая была буква у начальной строки {0}?",
                ModuleName = "Жёлтых стрелках",
            },

            // The Yellow Button
            // What was the {1} color in {0}?
            // What was the first color in The Yellow Button?
            [Question.YellowButtonColors] = new()
            {
                Conjugation = Conjugation.GenitiveMascNeuter,
                QuestionText = "Какой был {1}-й цвет в последовательности {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Yellow"] = "Yellow",
                    ["Green"] = "Green",
                    ["Cyan"] = "Cyan",
                    ["Blue"] = "Blue",
                    ["Magenta"] = "Magenta",
                },
            },

            // Yellow Button’t
            // What was the filename in {0}?
            // What was the filename in Yellow Button’t?
            [Question.YellowButtontFilename] = new()
            {
                QuestionText = "Какое было название файла {0}?",
            },

            // Yellow Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Yellow Cipher?
            [Question.YellowCipherScreen] = new()
            {
                QuestionText = "Что было на {1} экране на {2}-й странице {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Zero, Zero
            // What color was the {1} star in {0}?
            // What color was the top-left star in Zero, Zero?
            [Question.ZeroZeroStarColors] = new()
            {
                QuestionText = "Какого цвета была {1} звезда {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхняя левая",
                    ["top-right"] = "верхняя правая",
                    ["bottom-left"] = "нижняя левая",
                    ["bottom-right"] = "нижняя правая",
                },
                Answers = new Dictionary<string, string>
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
            // How many points were on the {1} star in {0}?
            // How many points were on the top-left star in Zero, Zero?
            [Question.ZeroZeroStarPoints] = new()
            {
                QuestionText = "Сколько вершин было у {1} звезды {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхней левой",
                    ["top-right"] = "верхней правой",
                    ["bottom-left"] = "нижней левой",
                    ["bottom-right"] = "нижней правой",
                },
            },
            // Where was the {1} square in {0}?
            // Where was the red square in Zero, Zero?
            [Question.ZeroZeroSquares] = new()
            {
                QuestionText = "Где был {1} квадрат {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красный",
                    ["green"] = "зелёный",
                    ["blue"] = "синий",
                },
            },

            // Zoni
            // What was the {1} word in {0}?
            // What was the first word in Zoni?
            [Question.ZoniWords] = new()
            {
                QuestionText = "Какое было {1}-е расшифрованное слово {0}?",
                ModuleName = "Zoni",
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
}
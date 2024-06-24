using System.Collections.Generic;

namespace Souvenir
{
    public class Translation_ru : Translation
    {
        public override string FormatModuleName(string moduleNameWithoutThe, string moduleNameWithThe, bool addSolveCount, int numSolved) =>
            addSolveCount ? $"{moduleNameWithoutThe} (в {Ordinal(numSolved)}-м решённом модуле)" : moduleNameWithThe;

        public override string Ordinal(int number) => number.ToString();

        #region Translatable strings
        protected override Dictionary<Question, TranslationInfo> _translations => new()
        {
            // 1000 Words
            // What was the {1} word shown in {0}?
            // What was the first word shown in 1000 Words?
            [Question._1000WordsWords] = new TranslationInfo
            {
                QuestionText = "Какое было {1}-е показанное слово в «{0}»?",
                ModuleName = "1000 слов",
            },

            // 100 Levels of Defusal
            // What was the {1} displayed letter in {0}?
            // What was the first displayed letter in 100 Levels of Defusal?
            [Question._100LevelsOfDefusalLetters] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я показанная буква в «{0}»?",
                ModuleName = "100 уровней обезвреживания",
            },

            // 1D Chess
            // What was {1} in {0}?
            // What was your first move in 1D Chess?
            [Question._1DChessMoves] = new TranslationInfo
            {
                QuestionText = "Каким был {1} в «{0}»?",
                ModuleName = "Одномерных шахматах",
                FormatArgs = new Dictionary<string, string>
                {
                    ["your first move"] = "ваш 1-й ход",
                    ["Rustmate’s first move"] = "1-й ход Растмейта",
                    ["your second move"] = "ваш 2-й ход",
                    ["Rustmate’s second move"] = "2-й ход Растмейта",
                    ["your third move"] = "ваш 3-й ход",
                    ["Rustmate’s third move"] = "3-й ход Растмейта",
                    ["your fourth move"] = "ваш 4-й ход",
                    ["Rustmate’s fourth move"] = "4-й ход Растмейта",
                    ["your fifth move"] = "ваш 5-й ход",
                    ["Rustmate’s fifth move"] = "5-й ход Растмейта",
                    ["your sixth move"] = "ваш 6-й ход",
                    ["Rustmate’s sixth move"] = "6-й ход Растмейта",
                    ["your seventh move"] = "ваш 7-й ход",
                    ["Rustmate’s seventh move"] = "7-й ход Растмейта",
                    ["your eighth move"] = "ваш 8-й ход",
                    ["Rustmate’s eighth move"] = "8-й ход Растмейта",
                },
            },

            // 3D Maze
            // What were the markings in {0}?
            // What were the markings in 3D Maze?
            [Question._3DMazeMarkings] = new TranslationInfo
            {
                QuestionText = "Какими буквами был обозначен ваш «{0}»?",
                ModuleName = "3D-лабиринт",
            },
            // What was the cardinal direction in {0}?
            // What was the cardinal direction in 3D Maze?
            [Question._3DMazeBearing] = new TranslationInfo
            {
                QuestionText = "Какая была целевая сторона света в «{0}e»?",
                ModuleName = "3D-лабиринт",
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
            [Question._3DTapCodeWord] = new TranslationInfo
            {
                QuestionText = "Какое слово было передано в «{0}»?",
                ModuleName = "3D-нажимном коде",
            },

            // 3D Tunnels
            // What was the {1} goal node in {0}?
            // What was the first goal node in 3D Tunnels?
            [Question._3DTunnelsTargetNode] = new TranslationInfo
            {
                QuestionText = "Какой символ был {1}-й вашей целью в «{0}»?",
                ModuleName = "3D-туннелях",
            },

            // 3 LEDs
            // What was the initial state of the LEDs in {0} (in reading order)?
            // What was the initial state of the LEDs in 3 LEDs (in reading order)?
            [Question._3LEDsInitialState] = new TranslationInfo
            {
                QuestionText = "Какое было исходное состояние «{0}» (в порядке чтения)?",
                ModuleName = "Трёх светодиодов",
            },

            // 3N+1
            // What number was initially displayed in {0}?
            // What number was initially displayed in 3N+1?
            [Question._3NPlus1] = new TranslationInfo
            {
                QuestionText = "Какое число было изначально показано в модуле «{0}»?",
            },

            // 64
            // What was the displayed number in {0}?
            // What was the displayed number in 64?
            [Question._64DisplayedNumber] = new TranslationInfo
            {
                QuestionText = "Какое число было показано в модуле «{0}»?",
            },

            // 7
            // What was the {1} channel’s initial value in {0}?
            // What was the red channel’s initial value in 7?
            [Question._7InitialValues] = new TranslationInfo
            {
                QuestionText = "Какое было начальное значение {1} канала в модуле «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красного",
                    ["green"] = "зелёного",
                    ["blue"] = "синего",
                },
            },
            // What LED color was shown in stage {1} of {0}?
            // What LED color was shown in stage 0 of 7?
            [Question._7LedColors] = new TranslationInfo
            {
                QuestionText = "Какой цвет светодиода был показан на этапе {1} в модуле «{0}»?",
            },

            // 9-Ball
            // What was the number of ball {1} in {0}?
            // What was the number of ball A in 9-Ball?
            [Question._9BallLetters] = new TranslationInfo
            {
                QuestionText = "What was the number of ball {1} in «{0}»?",
            },
            // What was the letter of ball {1} in {0}?
            // What was the letter of ball 2 in 9-Ball?
            [Question._9BallNumbers] = new TranslationInfo
            {
                QuestionText = "What was the letter of ball {1} in «{0}»?",
            },

            // Abyss
            // What was the {1} character displayed on {0}?
            // What was the first character displayed on Abyss?
            [Question.AbyssSeed] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й показанный символ в «{0}»?",
                ModuleName = "Бездне",
            },

            // Accumulation
            // What was the background color on the {1} stage in {0}?
            // What was the background color on the first stage in Accumulation?
            [Question.AccumulationBackgroundColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета была подложка (фоновый цвет) модуля на {1}-м этапе в «{0}»?",
                ModuleName = "Накоплении",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Синий",
                    ["Brown"] = "Коричневый",
                    ["Green"] = "Зелёный",
                    ["Grey"] = "Серый",
                    ["Lime"] = "Лаймовый",
                    ["Orange"] = "Оранжевый",
                    ["Pink"] = "Розовый",
                    ["Red"] = "Красный",
                    ["White"] = "Белый",
                    ["Yellow"] = "Жёлтый",
                },
            },
            // What was the border color in {0}?
            // What was the border color in Accumulation?
            [Question.AccumulationBorderColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета была рамка модуля в «{0}»?",
                ModuleName = "Накоплении",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Синяя",
                    ["Brown"] = "Коричневая",
                    ["Green"] = "Зелёная",
                    ["Grey"] = "Серая",
                    ["Lime"] = "Лаймовая",
                    ["Orange"] = "Оранжевая",
                    ["Pink"] = "Розовая",
                    ["Red"] = "Красная",
                    ["White"] = "Белая",
                    ["Yellow"] = "Жёлтая",
                },
            },

            // Adventure Game
            // Which item was the {1} correct item you used in {0}?
            // Which item was the first correct item you used in Adventure Game?
            [Question.AdventureGameCorrectItem] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й правильный предмет, который вы использовали в «{0}»?",
                ModuleName = "Приключенческой игре",
            },
            // What enemy were you fighting in {0}?
            // What enemy were you fighting in Adventure Game?
            [Question.AdventureGameEnemy] = new TranslationInfo
            {
                QuestionText = "С каким врагом вы сражались в «{0}»?",
                ModuleName = "Приключенческой игре",
            },

            // Affine Cycle
            // What was the {1} in {0}?
            // What was the message in Affine Cycle?
            [Question.AffineCycleWord] = new TranslationInfo
            {
                QuestionText = "{1} в «{0}»?",
                ModuleName = "Аффинном цикле",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Какое было сообщение",
                    ["response"] = "Какой был ответ",
                },
            },

            // A Letter
            // What was the initial letter in {0}?
            // What was the initial letter in A Letter?
            [Question.ALetterInitialLetter] = new TranslationInfo
            {
                QuestionText = "Какая была начальная буква в модуле «{0}»?",
                ModuleName = "Буква",
            },

            // Alfa-Bravo
            // Which letter was pressed in {0}?
            // Which letter was pressed in Alfa-Bravo?
            [Question.AlfaBravoPressedLetter] = new TranslationInfo
            {
                QuestionText = "Какая буква была нажата в модуле «{0}»?",
            },
            // Which letter was to the left of the pressed one in {0}?
            // Which letter was to the left of the pressed one in Alfa-Bravo?
            [Question.AlfaBravoLeftPressedLetter] = new TranslationInfo
            {
                QuestionText = "Какая буква была слева от нажатой в модуле «{0}»?",
            },
            // Which letter was to the right of the pressed one in {0}?
            // Which letter was to the right of the pressed one in Alfa-Bravo?
            [Question.AlfaBravoRightPressedLetter] = new TranslationInfo
            {
                QuestionText = "Какая буква была справа от нажатой в модуле «{0}»?",
            },
            // What was the last digit on the small display in {0}?
            // What was the last digit on the small display in Alfa-Bravo?
            [Question.AlfaBravoDigit] = new TranslationInfo
            {
                QuestionText = "Какая была последняя цифра на маленьком экране в модуле «{0}»?",
            },

            // Algebra
            // What was the first equation in {0}?
            // What was the first equation in Algebra?
            [Question.AlgebraEquation1] = new TranslationInfo
            {
                QuestionText = "Какое было первое уравнение в «{0}»?",
                ModuleName = "Алгебре",
            },
            // What was the second equation in {0}?
            // What was the second equation in Algebra?
            [Question.AlgebraEquation2] = new TranslationInfo
            {
                QuestionText = "Какое было второе уравнение в «{0}»?",
                ModuleName = "Алгебре",
            },

            // Algorithmia
            // Which position was the {1} position in {0}?
            // Which position was the starting position in Algorithmia?
            [Question.AlgorithmiaPositions] = new TranslationInfo
            {
                QuestionText = "Which position was the {1} position in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["starting"] = "starting",
                    ["goal"] = "goal",
                },
            },
            // What was the color of the colored bulb in {0}?
            // What was the color of the colored bulb in Algorithmia?
            [Question.AlgorithmiaColor] = new TranslationInfo
            {
                QuestionText = "What was the color of the colored bulb in «{0}»?",
            },
            // Which number was present in the seed in {0}?
            // Which number was present in the seed in Algorithmia?
            [Question.AlgorithmiaSeed] = new TranslationInfo
            {
                QuestionText = "Which number was present in the seed in «{0}»?",
            },

            // Alphabetical Ruling
            // What was the letter displayed in the {1} stage of {0}?
            // What was the letter displayed in the first stage of Alphabetical Ruling?
            [Question.AlphabeticalRulingLetter] = new TranslationInfo
            {
                QuestionText = "What was the letter displayed in the {1} stage of «{0}»?",
            },
            // What was the number displayed in the {1} stage of {0}?
            // What was the number displayed in the first stage of Alphabetical Ruling?
            [Question.AlphabeticalRulingNumber] = new TranslationInfo
            {
                QuestionText = "What was the number displayed in the {1} stage of «{0}»?",
            },

            // Alphabet Numbers
            // Which of these numbers was on one of the buttons in the {1} stage of {0}?
            // Which of these numbers was on one of the buttons in the first stage of Alphabet Numbers?
            [Question.AlphabetNumbersDisplayedNumbers] = new TranslationInfo
            {
                QuestionText = "Какая из этих цифр была на одной из кнопок на {1}-м этапе в «{0}»?",
                ModuleName = "Алфавитных числах",
            },

            // Alphabet Tiles
            // What was the {1} letter shown during the cycle in {0}?
            // What was the first letter shown during the cycle in Alphabet Tiles?
            [Question.AlphabetTilesCycle] = new TranslationInfo
            {
                QuestionText = "What was the {1} letter shown during the cycle in «{0}»?",
            },
            // What was the missing letter in {0}?
            // What was the missing letter in Alphabet Tiles?
            [Question.AlphabetTilesMissingLetter] = new TranslationInfo
            {
                QuestionText = "What was the missing letter in «{0}»?",
            },

            // Alpha-Bits
            // What character was displayed on the {1} screen on the {2} in {0}?
            // What character was displayed on the first screen on the left in Alpha-Bits?
            [Question.AlphaBitsDisplayedCharacters] = new TranslationInfo
            {
                QuestionText = "What character was displayed on the {1} screen on the {2} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "left",
                    ["right"] = "right",
                },
            },

            // Ángel Hernández
            // What letter was shown by the raised buttons on the {1} stage on {0}?
            // What letter was shown by the raised buttons on the first stage on Ángel Hernández?
            [Question.AngelHernandezMainLetter] = new TranslationInfo
            {
                QuestionText = "What letter was shown by the raised buttons on the {1} stage on «{0}»?",
            },

            // The Arena
            // What was the maximum weapon damage of the attack phase in {0}?
            // What was the maximum weapon damage of the attack phase in The Arena?
            [Question.ArenaDamage] = new TranslationInfo
            {
                QuestionText = "What was the maximum weapon damage of the attack phase in «{0}»?",
            },
            // Which enemy was present in the defend phase of {0}?
            // Which enemy was present in the defend phase of The Arena?
            [Question.ArenaEnemies] = new TranslationInfo
            {
                QuestionText = "Which enemy was present in the defend phase of «{0}»?",
            },
            // Which was a number present in the grab phase of {0}?
            // Which was a number present in the grab phase of The Arena?
            [Question.ArenaNumbers] = new TranslationInfo
            {
                QuestionText = "Which was a number present in the grab phase of «{0}»?",
            },

            // Arithmelogic
            // What was the symbol on the submit button in {0}?
            // What was the symbol on the submit button in Arithmelogic?
            [Question.ArithmelogicSubmit] = new TranslationInfo
            {
                QuestionText = "Какой символ был на кнопке отправки в «{0}»?",
                ModuleName = "Арифмологии",
            },
            // Which number was selectable, but not the solution, in the {1} screen on {0}?
            // Which number was selectable, but not the solution, in the left screen on Arithmelogic?
            [Question.ArithmelogicNumbers] = new TranslationInfo
            {
                QuestionText = "Какое число присутствовало (но не являлось решением) на {1} экране в «{0}»?",
                ModuleName = "Арифмологии",
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
            [Question.ASCIIMazeCharacters] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й символ, отображённый в «{0}»?",
                ModuleName = "Лабиринте ASCII",
            },

            // A Square
            // Which of these was an index color in {0}?
            // Which of these was an index color in A Square?
            [Question.ASquareIndexColors] = new TranslationInfo
            {
                QuestionText = "Какой из этих цветов был индексным цветом в модуле «{0}»?",
                ModuleName = "Квадрат",
            },
            // Which color was submitted {1} in {0}?
            // Which color was submitted first in A Square?
            [Question.ASquareCorrectColors] = new TranslationInfo
            {
                QuestionText = "Какой цвет был отправлен {1}-м в модуле «{0}»?",
                ModuleName = "Квадрат",
            },

            // The Azure Button
            // What was T in {0}?
            // What was T in The Azure Button?
            [Question.AzureButtonT] = new TranslationInfo
            {
                QuestionText = "Какое значение было у T в «{0}»?",
                ModuleName = "Лазурной кнопке",
            },
            // Which of these cards was shown in Stage 1, but not T, in {0}?
            // Which of these cards was shown in Stage 1, but not T, in The Azure Button?
            [Question.AzureButtonNotT] = new TranslationInfo
            {
                QuestionText = "Какая из этих карт была показана на первом этапе (но не T) в «{0}»?",
                ModuleName = "Лазурной кнопке",
            },
            // What was M in {0}?
            // What was M in The Azure Button?
            [Question.AzureButtonM] = new TranslationInfo
            {
                QuestionText = "Какое значение было у M в «{0}»?",
                ModuleName = "Лазурной кнопке",
            },
            // What was the {1} direction in the decoy arrow in {0}?
            // What was the first direction in the decoy arrow in The Azure Button?
            [Question.AzureButtonDecoyArrowDirection] = new TranslationInfo
            {
                QuestionText = "Какое было {1}-е направление у стрелки-приманки в «{0}»?",
                ModuleName = "Лазурной кнопке",
            },
            // What was the {1} direction in the {2} non-decoy arrow in {0}?
            // What was the first direction in the first non-decoy arrow in The Azure Button?
            [Question.AzureButtonNonDecoyArrowDirection] = new TranslationInfo
            {
                QuestionText = "Какое было {1}-е направление у {2}-й стрелки (не приманки) в «{0}»?",
                ModuleName = "Лазурной кнопке",
            },

            // Bakery
            // Which menu item was present in {0}?
            // Which menu item was present in Bakery?
            [Question.BakeryItems] = new TranslationInfo
            {
                QuestionText = "Какая выпечка присутствовала в «{0}»?",
                ModuleName = "Пекарне",
            },

            // Bamboozled Again
            // What color was the {1} correct button in {0}?
            // What color was the first correct button in Bamboozled Again?
            [Question.BamboozledAgainButtonColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета была {1}-я правильная кнопка в «{0}»?",
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
                    ["Magenta"] = "Мадженты",
                    ["Rose"] = "Розового",
                    ["White"] = "Белого",
                    ["Grey"] = "Серого",
                    ["Black"] = "Чёрного",
                },
            },
            // What was the text on the {1} correct button in {0}?
            // What was the text on the first correct button in Bamboozled Again?
            [Question.BamboozledAgainButtonText] = new TranslationInfo
            {
                QuestionText = "Какая была надпись на {1}-й правильной кнопке в «{0}»?",
                ModuleName = "Повторном надувательстве",
            },
            // What was the {1} decrypted text on the display in {0}?
            // What was the first decrypted text on the display in Bamboozled Again?
            [Question.BamboozledAgainDisplayTexts1] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й расшифрованный текст на экране в «{0}»?",
                ModuleName = "Повторном надувательстве",
            },
            // What was the {1} decrypted text on the display in {0}?
            // What was the first decrypted text on the display in Bamboozled Again?
            [Question.BamboozledAgainDisplayTexts2] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й расшифрованный текст на экране в «{0}»?",
                ModuleName = "Повторном надувательстве",
            },
            // What color was the {1} text on the display in {0}?
            // What color was the first text on the display in Bamboozled Again?
            [Question.BamboozledAgainDisplayColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета был {1}-й текст на экране в «{0}»?",
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
                    ["Magenta"] = "Мадженты",
                    ["Rose"] = "Розового",
                    ["White"] = "Белого",
                    ["Grey"] = "Серого",
                },
            },

            // Bamboozling Button
            // What color was the button in the {1} stage of {0}?
            // What color was the button in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonColor] = new TranslationInfo
            {
                QuestionText = "What color was the button in the {1} stage of «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Yellow",
                    ["Lime"] = "Lime",
                    ["Green"] = "Green",
                    ["Jade"] = "Jade",
                    ["Cyan"] = "Cyan",
                    ["Azure"] = "Azure",
                    ["Blue"] = "Blue",
                    ["Violet"] = "Violet",
                    ["Magenta"] = "Magenta",
                    ["Rose"] = "Rose",
                    ["White"] = "White",
                    ["Grey"] = "Grey",
                    ["Black"] = "Black",
                },
            },
            // What was the {2} label on the button in the {1} stage of {0}?
            // What was the top label on the button in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonLabel] = new TranslationInfo
            {
                QuestionText = "What was the {2} label on the button in the {1} stage of «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["bottom"] = "bottom",
                },
            },
            // What was the {2} display in the {1} stage of {0}?
            // What was the first display in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonDisplay] = new TranslationInfo
            {
                QuestionText = "What was the {2} display in the {1} stage of «{0}»?",
            },
            // What was the color of the {2} display in the {1} stage of {0}?
            // What was the color of the first display in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonDisplayColor] = new TranslationInfo
            {
                QuestionText = "What was the color of the {2} display in the {1} stage of «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Yellow",
                    ["Lime"] = "Lime",
                    ["Green"] = "Green",
                    ["Jade"] = "Jade",
                    ["Cyan"] = "Cyan",
                    ["Azure"] = "Azure",
                    ["Blue"] = "Blue",
                    ["Violet"] = "Violet",
                    ["Magenta"] = "Magenta",
                    ["Rose"] = "Rose",
                    ["White"] = "White",
                    ["Grey"] = "Grey",
                },
            },

            // Barcode Cipher
            // What was the screen number in {0}?
            // What was the screen number in Barcode Cipher?
            [Question.BarcodeCipherScreenNumber] = new TranslationInfo
            {
                QuestionText = "What was the screen number in «{0}»?",
            },
            // What was the edgework represented by the {1} barcode in {0}?
            // What was the edgework represented by the first barcode in Barcode Cipher?
            [Question.BarcodeCipherBarcodeEdgework] = new TranslationInfo
            {
                QuestionText = "What was the edgework represented by the {1} barcode in «{0}»?",
            },
            // What was the answer for the {1} barcode in {0}?
            // What was the answer for the first barcode in Barcode Cipher?
            [Question.BarcodeCipherBarcodeAnswers] = new TranslationInfo
            {
                QuestionText = "What was the answer for the {1} barcode in «{0}»?",
            },

            // Bartending
            // Which ingredient was in the {1} position on {0}?
            // Which ingredient was in the first position on Bartending?
            [Question.BartendingIngredients] = new TranslationInfo
            {
                QuestionText = "Which ingredient was in the {1} position on «{0}»?",
            },

            // Beans
            // What was this bean in {0}?
            // What was this bean in Beans?
            [Question.BeansColors] = new TranslationInfo
            {
                QuestionText = "What was this bean in «{0}»?",
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
            [Question.BeanSproutsColors] = new TranslationInfo
            {
                QuestionText = "What was sprout {1} in «{0}»?",
            },
            // What bean was on sprout {1} in {0}?
            // What bean was on sprout 1 in Bean Sprouts?
            [Question.BeanSproutsBeans] = new TranslationInfo
            {
                QuestionText = "What bean was on sprout {1} in «{0}»?",
            },

            // Big Bean
            // What was the bean in {0}?
            // What was the bean in Big Bean?
            [Question.BigBeanColor] = new TranslationInfo
            {
                QuestionText = "What was the bean in «{0}»?",
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
            [Question.BigCircleColors] = new TranslationInfo
            {
                QuestionText = "Какой цвет был {1}-м в решении в «{0}»?",
                ModuleName = "Большом круге",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Orange"] = "Оранжевый",
                    ["Yellow"] = "Жёлтый",
                    ["Green"] = "Зелёный",
                    ["Blue"] = "Синий",
                    ["Magenta"] = "Розовый",
                    ["White"] = "Белый",
                    ["Black"] = "Чёрный",
                },
            },

            // Binary LEDs
            // At which numeric value did you cut the correct wire in {0}?
            // At which numeric value did you cut the correct wire in Binary LEDs?
            [Question.BinaryLEDsValue] = new TranslationInfo
            {
                QuestionText = "На каком числе вы перерезали верный провод в «{0}»?",
                ModuleName = "Двоичных светодиодах",
            },

            // Binary Shift
            // What was the {1} initial number in {0}?
            // What was the top-left initial number in Binary Shift?
            [Question.BinaryShiftInitialNumber] = new TranslationInfo
            {
                QuestionText = "Какое было начальное число {1} в «{0}»?",
                ModuleName = "Двоичном сдвиге",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "сверху слева",
                    ["top-middle"] = "сверху по центру",
                    ["top-right"] = "сверху справа",
                    ["left-middle"] = "слева по центру",
                    ["center"] = "в центре",
                    ["right-middle"] = "справа по центру",
                    ["bottom-left"] = "снизу слева",
                    ["bottom-middle"] = "снизу по центру",
                    ["bottom-right"] = "снизу справа",
                },
            },
            // What number was selected at stage {1} in {0}?
            // What number was selected at stage 0 in Binary Shift?
            [Question.BinaryShiftSelectedNumberPossition] = new TranslationInfo
            {
                QuestionText = "Какое число было выбрано на этапе {1} в «{0}»?",
                ModuleName = "Двоичном сдвиге",
            },
            // What number was not selected at stage {1} in {0}?
            // What number was not selected at stage 0 in Binary Shift?
            [Question.BinaryShiftNotSelectedNumberPossition] = new TranslationInfo
            {
                QuestionText = "Какое число не было выбрано на этапе {1} в «{0}»?",
                ModuleName = "Двоичном сдвиге",
            },

            // Binary
            // What word was displayed in {0}?
            // What word was displayed in Binary?
            [Question.BinaryWord] = new TranslationInfo
            {
                QuestionText = "What word was displayed in «{0}»?",
            },

            // Bitmaps
            // How many pixels were {1} in the {2} quadrant in {0}?
            // How many pixels were white in the top left quadrant in Bitmaps?
            [Question.Bitmaps] = new TranslationInfo
            {
                QuestionText = "Сколько было {1} пикселей в {2} квадранте в «{0}»?",
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
            [Question.BlackCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Чёрном шифре",
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
            [Question.BlindMazeColors] = new TranslationInfo
            {
                QuestionText = "Какого цвета была {1} кнопка в «{0}»?",
                ModuleName = "Слепом лабиринте",
                FormatArgs = new Dictionary<string, string>
                {
                    ["north"] = "северная",
                    ["east"] = "восточная",
                    ["west"] = "западная",
                    ["south"] = "южная",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красная",
                    ["Green"] = "Зелёная",
                    ["Blue"] = "Синяя",
                    ["Gray"] = "Серая",
                    ["Yellow"] = "Жёлтая",
                },
            },
            // Which maze did you solve {0} on?
            // Which maze did you solve Blind Maze on?
            [Question.BlindMazeMaze] = new TranslationInfo
            {
                QuestionText = "Какой лабиринт вы прошли в «{0}»?",
                ModuleName = "Слепом лабиринте",
            },

            // Blinkstop
            // How many times did the LED flash in {0}?
            // How many times did the LED flash in Blinkstop?
            [Question.BlinkstopNumberOfFlashes] = new TranslationInfo
            {
                QuestionText = "How many times did the LED flash in «{0}»?",
            },
            // Which color did the LED flash the fewest times in {0}?
            // Which color did the LED flash the fewest times in Blinkstop?
            [Question.BlinkstopFewestFlashedColor] = new TranslationInfo
            {
                QuestionText = "Which color did the LED flash the fewest times in «{0}»?",
            },

            // Blockbusters
            // What was the last letter pressed on {0}?
            // What was the last letter pressed on Blockbusters?
            [Question.BlockbustersLastLetter] = new TranslationInfo
            {
                QuestionText = "What was the last letter pressed on «{0}»?",
            },

            // Blue Arrows
            // What were the letters on the screen in {0}?
            // What were the letters on the screen in Blue Arrows?
            [Question.BlueArrowsInitialLetters] = new TranslationInfo
            {
                QuestionText = "Какие были буквы на экране в «{0}»?",
                ModuleName = "Синих стрелках",
            },

            // The Blue Button
            // What was D in {0}?
            // What was D in The Blue Button?
            [Question.BlueButtonD] = new TranslationInfo
            {
                QuestionText = "Какое значение было у D в «{0}»?",
                ModuleName = "Синей кнопке",
            },
            // What was {1} in {0}?
            // What was E in The Blue Button?
            [Question.BlueButtonEFGH] = new TranslationInfo
            {
                QuestionText = "Какое значение было у {1} в «{0}»?",
                ModuleName = "Синей кнопке",
            },
            // What was M in {0}?
            // What was M in The Blue Button?
            [Question.BlueButtonM] = new TranslationInfo
            {
                QuestionText = "Какое значение было у M в «{0}»?",
                ModuleName = "Синей кнопке",
            },
            // What was N in {0}?
            // What was N in The Blue Button?
            [Question.BlueButtonN] = new TranslationInfo
            {
                QuestionText = "Какое значение было у N в «{0}»?",
                ModuleName = "Синей кнопке",
            },
            // What was P in {0}?
            // What was P in The Blue Button?
            [Question.BlueButtonP] = new TranslationInfo
            {
                QuestionText = "Какое значение было у P в «{0}»?",
                ModuleName = "Синей кнопке",
            },
            // What was Q in {0}?
            // What was Q in The Blue Button?
            [Question.BlueButtonQ] = new TranslationInfo
            {
                QuestionText = "Какое значение было у Q в «{0}»?",
                ModuleName = "Синей кнопке",
            },
            // What was X in {0}?
            // What was X in The Blue Button?
            [Question.BlueButtonX] = new TranslationInfo
            {
                QuestionText = "Какое значение было у X в «{0}»?",
                ModuleName = "Синей кнопке",
            },

            // Blue Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Blue Cipher?
            [Question.BlueCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Синем шифре",
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
            [Question.BobBarksIndicators] = new TranslationInfo
            {
                QuestionText = "What was the {1} indicator label in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top left"] = "top left",
                    ["top right"] = "top right",
                    ["bottom left"] = "bottom left",
                    ["bottom right"] = "bottom right",
                },
            },
            // Which button flashed {1} in sequence in {0}?
            // Which button flashed first in sequence in Bob Barks?
            [Question.BobBarksPositions] = new TranslationInfo
            {
                QuestionText = "Which button flashed {1} in sequence in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "top left",
                    ["top right"] = "top right",
                    ["bottom left"] = "bottom left",
                    ["bottom right"] = "bottom right",
                },
            },

            // Boggle
            // What letter was initially visible on {0}?
            // What letter was initially visible on Boggle?
            [Question.BoggleLetters] = new TranslationInfo
            {
                QuestionText = "What letter was initially visible on «{0}»?",
            },

            // Bomb Diffusal
            // What was the license number in {0}?
            // What was the license number in Bomb Diffusal?
            [Question.BombDiffusalLicenseNumber] = new TranslationInfo
            {
                QuestionText = "What was the license number in «{0}»?",
            },

            // Book of Mario
            // Who said the {1} quote in {0}?
            // Who said the first quote in Book of Mario?
            [Question.BookOfMarioPictures] = new TranslationInfo
            {
                QuestionText = "Who said the {1} quote in «{0}»?",
            },
            // What did {1} say in the {2} stage of {0}?
            // What did Goombell say in the first stage of Book of Mario?
            [Question.BookOfMarioQuotes] = new TranslationInfo
            {
                QuestionText = "What did {1} say in the {2} stage of «{0}»?",
            },

            // Boolean Wires
            // Which operator did you submit in the {1} stage of {0}?
            // Which operator did you submit in the first stage of Boolean Wires?
            [Question.BooleanWiresEnteredOperators] = new TranslationInfo
            {
                QuestionText = "Which operator did you submit in the {1} stage of «{0}»?",
            },

            // Boomtar the Great
            // What was rule {1} in {0}?
            // What was rule one in Boomtar the Great?
            [Question.BoomtarTheGreatRules] = new TranslationInfo
            {
                QuestionText = "What was rule {1} in «{0}»?",
            },

            // Boxing
            // Which {1} appeared on {0}?
            // Which contestant’s first name appeared on Boxing?
            [Question.BoxingNames] = new TranslationInfo
            {
                QuestionText = "Which {1} appeared on «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["contestant’s first name"] = "contestant’s first name",
                    ["contestant’s last name"] = "contestant’s last name",
                    ["substitute’s first name"] = "substitute’s first name",
                    ["substitute’s last name"] = "substitute’s last name",
                },
            },
            // What was the {1} of the contestant with strength rating {2} on {0}?
            // What was the first name of the contestant with strength rating 0 on Boxing?
            [Question.BoxingContestantByStrength] = new TranslationInfo
            {
                QuestionText = "What was the {1} of the contestant with strength rating {2} on «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first name"] = "first name",
                    ["last name"] = "last name",
                    ["substitute’s first name"] = "substitute’s first name",
                    ["substitute’s last name"] = "substitute’s last name",
                },
            },
            // What was {1}’s strength rating on {0}?
            // What was Muhammad’s strength rating on Boxing?
            [Question.BoxingStrengthByContestant] = new TranslationInfo
            {
                QuestionText = "What was {1}’s strength rating on «{0}»?",
            },

            // Braille
            // What was the solution word in {0}?
            // What was the solution word in Braille?
            [Question.BrailleWord] = new TranslationInfo
            {
                QuestionText = "Какое слово являлось решением «{0}»?",
                ModuleName = "Шрифта Брайля",
            },

            // Breakfast Egg
            // Which color appeared on the egg in {0}?
            // Which color appeared on the egg in Breakfast Egg?
            [Question.BreakfastEggColor] = new TranslationInfo
            {
                QuestionText = "Какой цвет был у «{0}»?",
                ModuleName = "Яйца на завтрак",
            },

            // Broken Buttons
            // What was the {1} correct button you pressed in {0}?
            // What was the first correct button you pressed in Broken Buttons?
            [Question.BrokenButtons] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я правильная нажатая кнопка в «{0}»?",
                ModuleName = "Сломанных кнопках",
            },

            // Broken Guitar Chords
            // What was the displayed chord in {0}?
            // What was the displayed chord in Broken Guitar Chords?
            [Question.BrokenGuitarChordsDisplayedChord] = new TranslationInfo
            {
                QuestionText = "What was the displayed chord in «{0}»?",
            },
            // In which position, from left to right, was the broken string in {0}?
            // In which position, from left to right, was the broken string in Broken Guitar Chords?
            [Question.BrokenGuitarChordsMutedString] = new TranslationInfo
            {
                QuestionText = "In which position, from left to right, was the broken string in «{0}»?",
            },

            // Brown Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Brown Cipher?
            [Question.BrownCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Коричневом шифре",
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
            [Question.BrushStrokesMiddleColor] = new TranslationInfo
            {
                QuestionText = "What was the color of the middle contact point in «{0}»?",
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
            // What were the correct button presses in {0}?
            // What were the correct button presses in The Bulb?
            [Question.BulbButtonPresses] = new TranslationInfo
            {
                QuestionText = "Какие правильные кнопки были нажаты в модуле «{0}»?",
                ModuleName = "Лампочка",
            },

            // Burger Alarm
            // What was the {1} displayed digit in {0}?
            // What was the first displayed digit in Burger Alarm?
            [Question.BurgerAlarmDigits] = new TranslationInfo
            {
                QuestionText = "What was the {1} displayed digit in «{0}»?",
            },
            // What was the {1} order number in {0}?
            // What was the first order number in Burger Alarm?
            [Question.BurgerAlarmOrderNumbers] = new TranslationInfo
            {
                QuestionText = "What was the {1} order number in «{0}»?",
            },

            // Burglar Alarm
            // What was the {1} displayed digit in {0}?
            // What was the first displayed digit in Burglar Alarm?
            [Question.BurglarAlarmDigits] = new TranslationInfo
            {
                QuestionText = "What was the {1} displayed digit in «{0}»?",
            },

            // The Button
            // What color did the light glow in {0}?
            // What color did the light glow in The Button?
            [Question.ButtonLightColor] = new TranslationInfo
            {
                QuestionText = "Каким цветом горела цветная полоска в модуле «{0}»?",
                ModuleName = "Кнопка",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красным",
                    ["blue"] = "Синим",
                    ["yellow"] = "Жёлтым",
                    ["white"] = "Белым",
                },
            },

            // Button Sequence
            // How many of the buttons in {0} were {1}?
            // How many of the buttons in Button Sequence were red?
            [Question.ButtonSequencesColorOccurrences] = new TranslationInfo
            {
                QuestionText = "Сколько было {1} кнопок в «{0}»?",
                ModuleName = "Последовательности кнопок",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красных",
                    ["blue"] = "синих",
                    ["yellow"] = "жёлтых",
                    ["white"] = "белых",
                },
            },

            // Caesar Cycle
            // What was the {1} in {0}?
            // What was the message in Caesar Cycle?
            [Question.CaesarCycleWord] = new TranslationInfo
            {
                QuestionText = "{1} в «{0}»?",
                ModuleName = "Цикле Цезаря",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Какое было сообщение",
                    ["response"] = "Какой был ответ",
                },
            },

            // Caesar Psycho
            // What text was on the top display in the {1} stage of {0}?
            // What text was on the top display in the first stage of Caesar Psycho?
            [Question.CaesarPsychoScreenTexts] = new TranslationInfo
            {
                QuestionText = "What text was on the top display in the {1} stage of «{0}»?",
            },
            // What color was the text on the top display in the second stage of {0}?
            // What color was the text on the top display in the second stage of Caesar Psycho?
            [Question.CaesarPsychoScreenColor] = new TranslationInfo
            {
                QuestionText = "What color was the text on the top display in the second stage of «{0}»?",
            },

            // Calendar
            // What was the LED color in {0}?
            // What was the LED color in Calendar?
            [Question.CalendarLedColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета был индикатор в «{0}»?",
                ModuleName = "Календаре",
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "Зелёный",
                    ["Yellow"] = "Жёлтый",
                    ["Red"] = "Красный",
                    ["Blue"] = "Синий",
                },
            },

            // Cartinese
            // What color was the {1} button in {0}?
            // What color was the up button in Cartinese?
            [Question.CartineseButtonColors] = new TranslationInfo
            {
                QuestionText = "What color was the {1} button in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["up"] = "up",
                    ["right"] = "right",
                    ["down"] = "down",
                    ["left"] = "left",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Yellow"] = "Yellow",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                },
            },
            // What lyric was played by the {1} button in {0}?
            // What lyric was played by the up button in Cartinese?
            [Question.CartineseLyrics] = new TranslationInfo
            {
                QuestionText = "What lyric was played by the {1} button in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["up"] = "up",
                    ["right"] = "right",
                    ["down"] = "down",
                    ["left"] = "left",
                },
            },

            // Catchphrase
            // What was the colour of the {1} panel in {0}?
            // What was the colour of the top-left panel in Catchphrase?
            [Question.CatchphraseColour] = new TranslationInfo
            {
                QuestionText = "What was the colour of the {1} panel in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-right"] = "top-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Orange"] = "Orange",
                    ["Purple"] = "Purple",
                    ["Yellow"] = "Yellow",
                },
            },

            // Challenge & Contact
            // What was the {1} submitted answer in {0}?
            // What was the first submitted answer in Challenge & Contact?
            [Question.ChallengeAndContactAnswers] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й введённый ответ в «{0}»?",
                ModuleName = "Вызове и контакте",
            },

            // Character Codes
            // What was the {1} character in {0}?
            // What was the first character in Character Codes?
            [Question.CharacterCodesCharacter] = new TranslationInfo
            {
                QuestionText = "What was the {1} character in «{0}»?",
            },

            // Character Shift
            // Which letter was present but not submitted on the left slider of {0}?
            // Which letter was present but not submitted on the left slider of Character Shift?
            [Question.CharacterShiftLetters] = new TranslationInfo
            {
                QuestionText = "Which letter was present but not submitted on the left slider of «{0}»?",
            },
            // Which digit was present but not submitted on the right slider of {0}?
            // Which digit was present but not submitted on the right slider of Character Shift?
            [Question.CharacterShiftDigits] = new TranslationInfo
            {
                QuestionText = "Which digit was present but not submitted on the right slider of «{0}»?",
            },

            // Character Slots
            // Who was displayed in the {1} slot in the {2} stage of {0}?
            // Who was displayed in the first slot in the first stage of Character Slots?
            [Question.CharacterSlotsDisplayedCharacters] = new TranslationInfo
            {
                QuestionText = "Who was displayed in the {1} slot in the {2} stage of «{0}»?",
            },

            // Cheap Checkout
            // What was {1} in {0}?
            // What was the paid amount in Cheap Checkout?
            [Question.CheapCheckoutPaid] = new TranslationInfo
            {
                QuestionText = "{1} в «{0}»?",
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
            [Question.CheepCheckoutBirds] = new TranslationInfo
            {
                QuestionText = "Which bird {1} present in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "was",
                    ["was not"] = "was not",
                },
            },

            // Chess
            // What was the {1} coordinate in {0}?
            // What was the first coordinate in Chess?
            [Question.ChessCoordinate] = new TranslationInfo
            {
                QuestionText = "Какие были {1}-е координаты в «{0}»?",
                ModuleName = "Шахматах",
            },

            // Chinese Counting
            // What color was the {1} LED in {0}?
            // What color was the left LED in Chinese Counting?
            [Question.ChineseCountingLED] = new TranslationInfo
            {
                QuestionText = "What color was the {1} LED in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "left",
                    ["right"] = "right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "White",
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Orange"] = "Orange",
                },
            },

            // Chord Qualities
            // Which note was part of the given chord in {0}?
            // Which note was part of the given chord in Chord Qualities?
            [Question.ChordQualitiesNotes] = new TranslationInfo
            {
                QuestionText = "Какая нота присутствовала в начальном аккорде в «{0}»?",
                ModuleName = "Аккордных ладах",
            },
            // What was the given chord quality in {0}?
            // What was the given chord quality in Chord Qualities?
            [Question.ChordQualitiesQuality] = new TranslationInfo
            {
                QuestionText = "Какой был исходный лад начального аккорда в «{0}»?",
                ModuleName = "Аккордных ладах",
            },

            // The Code
            // What was the displayed number in {0}?
            // What was the displayed number in The Code?
            [Question.CodeDisplayNumber] = new TranslationInfo
            {
                QuestionText = "Какое было показанное число в «{0}»?",
                ModuleName = "Коде",
            },

            // Codenames
            // Which of these words was submitted in {0}?
            // Which of these words was submitted in Codenames?
            [Question.CodenamesAnswers] = new TranslationInfo
            {
                QuestionText = "Which of these words was submitted in «{0}»?",
            },

            // Coffeebucks
            // What was the last served coffee in {0}?
            // What was the last served coffee in Coffeebucks?
            [Question.CoffeebucksCoffee] = new TranslationInfo
            {
                QuestionText = "What was the last served coffee in «{0}»?",
            },

            // Coinage
            // Which coin was flipped in {0}?
            // Which coin was flipped in Coinage?
            [Question.CoinageFlip] = new TranslationInfo
            {
                QuestionText = "Which coin was flipped in «{0}»?",
            },

            // Color Addition
            // What was {1}'s number in {0}?
            // What was red's number in Color Addition?
            [Question.ColorAdditionNumbers] = new TranslationInfo
            {
                QuestionText = "Какое было {1} число в «{0}»?",
                ModuleName = "Смешении цветов",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красное",
                    ["green"] = "зелёное",
                    ["blue"] = "синее",
                },
            },

            // Color Braille
            // What mangling was applied in {0}?
            // What mangling was applied in Color Braille?
            [Question.ColorBrailleMangling] = new TranslationInfo
            {
                QuestionText = "What mangling was applied in «{0}»?",
            },
            // What was the {1} word in {0}?
            // What was the red word in Color Braille?
            [Question.ColorBrailleWords] = new TranslationInfo
            {
                QuestionText = "What was the {1} word in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                },
            },

            // Color Decoding
            // What was the {1}-stage indicator pattern in {0}?
            // What was the first-stage indicator pattern in Color Decoding?
            [Question.ColorDecodingIndicatorPattern] = new TranslationInfo
            {
                QuestionText = "What was the {1}-stage indicator pattern in «{0}»?",
            },
            // Which color {1} in the {2}-stage indicator pattern in {0}?
            // Which color appeared in the first-stage indicator pattern in Color Decoding?
            [Question.ColorDecodingIndicatorColors] = new TranslationInfo
            {
                QuestionText = "Which color {1} in the {2}-stage indicator pattern in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["appeared"] = "appeared",
                    ["did not appear"] = "did not appear",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "Green",
                    ["Purple"] = "Purple",
                    ["Red"] = "Red",
                    ["Blue"] = "Blue",
                    ["Yellow"] = "Yellow",
                },
            },

            // Colored Keys
            // What was the displayed word in {0}?
            // What was the displayed word in Colored Keys?
            [Question.ColoredKeysDisplayWord] = new TranslationInfo
            {
                QuestionText = "Какое было показанное слово в «{0}»?",
                ModuleName = "Цветных кнопках",
            },
            // What was the displayed word’s color in {0}?
            // What was the displayed word’s color in Colored Keys?
            [Question.ColoredKeysDisplayWordColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета было показанное слово в «{0}»?",
                ModuleName = "Цветных кнопках",
            },
            // What was the color of the {1} key in {0}?
            // What was the color of the top-left key in Colored Keys?
            [Question.ColoredKeysKeyColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета была {1} кнопка в «{0}»?",
                ModuleName = "Цветных кнопках",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхняя левая",
                    ["top-right"] = "верхняя правая",
                    ["bottom-left"] = "нижняя левая",
                    ["bottom-right"] = "нижняя правая",
                },
            },
            // What letter was on the {1} key in {0}?
            // What letter was on the top-left key in Colored Keys?
            [Question.ColoredKeysKeyLetter] = new TranslationInfo
            {
                QuestionText = "Какая буква была на {1} кнопке в «{0}»?",
                ModuleName = "Цветных кнопках",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхняя левая",
                    ["top-right"] = "верхняя правая",
                    ["bottom-left"] = "нижняя левая",
                    ["bottom-right"] = "нижняя правая",
                },
            },

            // Colored Squares
            // What was the first color group in {0}?
            // What was the first color group in Colored Squares?
            [Question.ColoredSquaresFirstGroup] = new TranslationInfo
            {
                QuestionText = "Какого цвета была первая группа в «{0}»?",
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
            [Question.ColoredSwitchesInitialPosition] = new TranslationInfo
            {
                QuestionText = "Какое было начальное положение «{0}»?",
                ModuleName = "Цветных переключателей",
            },
            // What was the position of the switches when the LEDs came on in {0}?
            // What was the position of the switches when the LEDs came on in Colored Switches?
            [Question.ColoredSwitchesWhenLEDsCameOn] = new TranslationInfo
            {
                QuestionText = "Какое было положение «{0}», когда загорелись светодиоды?",
                ModuleName = "Цветных переключателей",
            },

            // Color Morse
            // What was the color of the {1} LED in {0}?
            // What was the color of the first LED in Color Morse?
            [Question.ColorMorseColor] = new TranslationInfo
            {
                QuestionText = "Какой был цвет {1}-го светодиода в «{0}»?",
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
            [Question.ColorMorseCharacter] = new TranslationInfo
            {
                QuestionText = "Какой символ передавался через Морзе {1}-м светодиодом в «{0}»?",
                ModuleName = "Цветной азбуке Морзе",
            },

            // Colors Maximization
            // What was the submitted score in {0}?
            // What was the submitted score in Colors Maximization?
            [Question.ColorsMaximizationSubmittedScore] = new TranslationInfo
            {
                QuestionText = "Какое количество очков было отправлено в «{0}»?",
                ModuleName = "Максимизации цветов",
            },
            // What color {1} submitted as part of the solution in {0}?
            // What color was submitted as part of the solution in Colors Maximization?
            [Question.ColorsMaximizationSubmittedColor] = new TranslationInfo
            {
                QuestionText = "Какой цвет {1} отправлен как часть решения в «{0}»?",
                ModuleName = "Максимизации цветов",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "был",
                    ["was not"] = "не был",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Синий",
                    ["Green"] = "Зелёный",
                    ["Magenta"] = "Маджента",
                    ["Red"] = "Красный",
                    ["White"] = "Белый",
                    ["Yellow"] = "Жёлтый",
                },
            },
            // How many buttons were {1} in {0}?
            // How many buttons were red in Colors Maximization?
            [Question.ColorsMaximizationColorCount] = new TranslationInfo
            {
                QuestionText = "Сколько было {1} кнопок в «{0}»?",
                ModuleName = "Максимизации цветов",
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
            [Question.ColouredCubesColours] = new TranslationInfo
            {
                QuestionText = "What was the colour of this {1} in the {2} stage of «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["cube"] = "cube",
                    ["stage light"] = "stage light",
                },
            },

            // Colour Flash
            // What was the color of the last word in the sequence in {0}?
            // What was the color of the last word in the sequence in Colour Flash?
            [Question.ColourFlashLastColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета было последнее слово в последовательности в «{0}»?",
                ModuleName = "Цветной вспышке",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красное",
                    ["Yellow"] = "Жёлтое",
                    ["Green"] = "Зелёное",
                    ["Blue"] = "Синее",
                    ["Magenta"] = "Мадженты",
                    ["White"] = "Белое",
                },
            },

            // Connection Check
            // What pair of numbers was present in {0}?
            // What pair of numbers was present in Connection Check?
            [Question.ConnectionCheckNumbers] = new TranslationInfo
            {
                QuestionText = "Какая пара чисел присутствовала в «{0}»?",
                ModuleName = "Проверке соединения",
            },

            // Coordinates
            // What was the solution you selected first in {0}?
            // What was the solution you selected first in Coordinates?
            [Question.CoordinatesFirstSolution] = new TranslationInfo
            {
                QuestionText = "Какую координату вы выбрали первой в «{0}»?",
                ModuleName = "Координатах",
            },
            // What was the grid size in {0}?
            // What was the grid size in Coordinates?
            [Question.CoordinatesSize] = new TranslationInfo
            {
                QuestionText = "В каком формате был указан размер сетки в «{0}»?",
                ModuleName = "Координатах",
            },

            // Coral Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Coral Cipher?
            [Question.CoralCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Коралловом шифре",
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
            [Question.CornersColors] = new TranslationInfo
            {
                QuestionText = "Какого цвета был {1} угол в модуле «{0}»?",
                ModuleName = "Углы",
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
            [Question.CornersColorCount] = new TranslationInfo
            {
                QuestionText = "Сколько было {1} углов в модуле «{0}»?",
                ModuleName = "Углы",
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
            [Question.CornflowerCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Васильковом шифре",
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
            [Question.CosmicNumber] = new TranslationInfo
            {
                QuestionText = "What was the number initially shown in «{0}»?",
            },

            // Crazy Hamburger
            // What was the {1} ingredient shown in {0}?
            // What was the first ingredient shown in Crazy Hamburger?
            [Question.CrazyHamburgerIngredient] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й показанный ингредиент в «{0}»?",
                ModuleName = "Сумасшедшем гамбургере",
            },

            // Crazy Maze
            // What was the {1} location in {0}?
            // What was the starting location in Crazy Maze?
            [Question.CrazyMazeStartOrGoal] = new TranslationInfo
            {
                QuestionText = "Какая была {1} позиция в «{0}»?",
                ModuleName = "Сумасшедшем лабиринте",
                FormatArgs = new Dictionary<string, string>
                {
                    ["starting"] = "начальная",
                    ["goal"] = "конечная",
                },
            },

            // Cream Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Cream Cipher?
            [Question.CreamCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Кремовом шифре",
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
            [Question.CreationWeather] = new TranslationInfo
            {
                QuestionText = "Какая погода была на {1}-м дне в «{0}»?",
                ModuleName = "Создании",
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
            [Question.CrimsonCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Багровом шифре",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Critters
            // What was the alteration color used in {0}?
            // What was the alteration color used in Critters?
            [Question.CrittersAlterationColor] = new TranslationInfo
            {
                QuestionText = "What was the alteration color used in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["Yellow"] = "Yellow",
                    ["Pink"] = "Pink",
                    ["Blue"] = "Blue",
                    ["White"] = "White",
                },
            },

            // Cruel Binary
            // What was the displayed word in {0}?
            // What was the displayed word in Cruel Binary?
            [Question.CruelBinaryDisplayedWord] = new TranslationInfo
            {
                QuestionText = "What was the displayed word in «{0}»?",
            },

            // Cruel Keypads
            // Which of these characters appeared in the {1} stage of {0}?
            // Which of these characters appeared in the first stage of Cruel Keypads?
            [Question.CruelKeypadsDisplayedSymbols] = new TranslationInfo
            {
                QuestionText = "Какой из этих символов появился на {1}-м этапе в «{0}»?",
                ModuleName = "Жестокой клавиатуре",
            },
            // What was the color of the bar in the {1} stage of {0}?
            // What was the color of the bar in the first stage of Cruel Keypads?
            [Question.CruelKeypadsColors] = new TranslationInfo
            {
                QuestionText = "Какого цвета была шкала на {1}-м этапе в «{0}»?",
                ModuleName = "Жестокой клавиатуре",
            },

            // The cRule
            // Which cell was pre-filled at the start of {0}?
            // Which cell was pre-filled at the start of The cRule?
            [Question.CRulePrefilled] = new TranslationInfo
            {
                QuestionText = "Which cell was pre-filled at the start of «{0}»?",
            },
            // Which symbol pair was here in {0}?
            // Which symbol pair was here in The cRule?
            [Question.CRuleSymbolPair] = new TranslationInfo
            {
                QuestionText = "Which symbol pair was here in «{0}»?",
            },
            // Which symbol pair was present on {0}?
            // Which symbol pair was present on The cRule?
            [Question.CRuleSymbolPairPresent] = new TranslationInfo
            {
                QuestionText = "Which symbol pair was present on «{0}»?",
            },
            // Where was {1} in {0}?
            // Where was ♤♤ in The cRule?
            [Question.CRuleSymbolPairCell] = new TranslationInfo
            {
                QuestionText = "Where was {1} in «{0}»?",
            },

            // Cryptic Cycle
            // What was the {1} in {0}?
            // What was the message in Cryptic Cycle?
            [Question.CrypticCycleWord] = new TranslationInfo
            {
                QuestionText = "{1} в «{0}»?",
                ModuleName = "Зашифрованном цикле",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Какое было сообщение",
                    ["response"] = "Какой был ответ",
                },
            },

            // Cryptic Keypad
            // What was the label of the {1} key in {0}?
            // What was the label of the top-left key in Cryptic Keypad?
            [Question.CrypticKeypadLabels] = new TranslationInfo
            {
                QuestionText = "What was the label of the {1} key in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-right"] = "top-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
            },
            // Which cardinal direction was the {1} key rotated to in {0}?
            // Which cardinal direction was the top-left key rotated to in Cryptic Keypad?
            [Question.CrypticKeypadRotations] = new TranslationInfo
            {
                QuestionText = "Which cardinal direction was the {1} key rotated to in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-right"] = "top-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["North"] = "North",
                    ["East"] = "East",
                    ["South"] = "South",
                    ["West"] = "West",
                },
            },

            // The Cube
            // What was the {1} cube rotation in {0}?
            // What was the first cube rotation in The Cube?
            [Question.CubeRotations] = new TranslationInfo
            {
                QuestionText = "Какое было {1}-е вращение «{0}»?",
                ModuleName = "Куба",
                FormatArgs = new Dictionary<string, string>
                {
                    ["�ordinal"] = "�ordinal",
                },
            },

            // Cursed Double-Oh
            // What was the first digit of the initially displayed number in {0}?
            // What was the first digit of the initially displayed number in Cursed Double-Oh?
            [Question.CursedDoubleOhInitialPosition] = new TranslationInfo
            {
                QuestionText = "Какая была первая цифра изначально показаного числа в модуле «{0}»?",
                ModuleName = "Проклятый ноль-ноль",
            },

            // Customer Identification
            // Who was the {1} customer in {0}?
            // Who was the first customer in Customer Identification?
            [Question.CustomerIdentificationCustomer] = new TranslationInfo
            {
                QuestionText = "Who was the {1} customer in «{0}»?",
            },

            // The Cyan Button
            // Where was the button at the {1} stage in {0}?
            // Where was the button at the first stage in The Cyan Button?
            [Question.CyanButtonPositions] = new TranslationInfo
            {
                QuestionText = "Где была кнопка на {1}-м этапе в «{0}»?",
                ModuleName = "Голубой кнопке",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "Левый верх",
                    ["top middle"] = "Верхний центр",
                    ["top right"] = "Правый верх",
                    ["bottom left"] = "Левый низ",
                    ["bottom middle"] = "Нижний центр",
                    ["bottom right"] = "Правый низ",
                },
            },

            // DACH Maze
            // Which region did you depart from in {0}?
            // Which region did you depart from in DACH Maze?
            [Question.DACHMazeOrigin] = new TranslationInfo
            {
                QuestionText = "Which region did you depart from in «{0}»?",
            },

            // Deaf Alley
            // What was the shape generated in {0}?
            // What was the shape generated in Deaf Alley?
            [Question.DeafAlleyShape] = new TranslationInfo
            {
                QuestionText = "Какая фигура была сгенерирована в «{0}»?",
                ModuleName = "Глухой аллее",
            },

            // The Deck of Many Things
            // What deck did the first card of {0} belong to?
            // What deck did the first card of The Deck of Many Things belong to?
            [Question.DeckOfManyThingsFirstCard] = new TranslationInfo
            {
                QuestionText = "What deck did the first card of «{0}» belong to?",
            },

            // Decolored Squares
            // What was the starting {1} defining color in {0}?
            // What was the starting column defining color in Decolored Squares?
            [Question.DecoloredSquaresStartingPos] = new TranslationInfo
            {
                QuestionText = "Какой цвет определил {1} блок-схемы в «{0}»?",
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
            [Question.DecolourFlashGoal] = new TranslationInfo
            {
                QuestionText = "What was the {1} of the {2} goal in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["colour"] = "colour",
                    ["word"] = "word",
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
            [Question.DenialDisplaysDisplays] = new TranslationInfo
            {
                QuestionText = "What number was initially shown on display {1} in «{0}»?",
            },

            // Devilish Eggs
            // What was the {1} egg’s {2} rotation in {0}?
            // What was the top egg’s first rotation in Devilish Eggs?
            [Question.DevilishEggsRotations] = new TranslationInfo
            {
                QuestionText = "What was the {1} egg’s {2} rotation in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["bottom"] = "bottom",
                },
            },
            // What was the {1} digit in the string of numbers on {0}?
            // What was the first digit in the string of numbers on Devilish Eggs?
            [Question.DevilishEggsNumbers] = new TranslationInfo
            {
                QuestionText = "What was the {1} digit in the string of numbers on «{0}»?",
            },
            // What was the {1} letter in the string of letters on {0}?
            // What was the first letter in the string of letters on Devilish Eggs?
            [Question.DevilishEggsLetters] = new TranslationInfo
            {
                QuestionText = "What was the {1} letter in the string of letters on «{0}»?",
            },

            // Digisibility
            // What was the number on the {1} button in {0}?
            // What was the number on the first button in Digisibility?
            [Question.DigisibilityDisplayedNumber] = new TranslationInfo
            {
                QuestionText = "What was the number on the {1} button in «{0}»?",
            },

            // Digit String
            // What was the initial number in {0}?
            // What was the initial number in Digit String?
            [Question.DigitStringInitialNumber] = new TranslationInfo
            {
                QuestionText = "Какое было изначальное число в «{0}»?",
                ModuleName = "Цифровой строке",
            },

            // Dimension Disruption
            // Which of these was a visible character in {0}?
            // Which of these was a visible character in Dimension Disruption?
            [Question.DimensionDisruptionVisibleLetters] = new TranslationInfo
            {
                QuestionText = "Что из этого было видимым символом в «{0}»?",
                ModuleName = "Разрушении измерений",
            },

            // Directional Button
            // How many times did you press the button in the {1} stage of {0}?
            // How many times did you press the button in the first stage of Directional Button?
            [Question.DirectionalButtonButtonCount] = new TranslationInfo
            {
                QuestionText = "Сколько раз вы нажали кнопку на {1}-м этапе в «{0}»?",
                ModuleName = "Направляющей кнопке",
            },

            // Discolored Squares
            // What was {1}’s remembered position in {0}?
            // What was Blue’s remembered position in Discolored Squares?
            [Question.DiscoloredSquaresRememberedPositions] = new TranslationInfo
            {
                QuestionText = "В какой позиции находился {1} начальный цвет в «{0}»?",
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

            // Divisible Numbers
            // What were the correct button presses in {0}?
            // What were the correct button presses in Divisible Numbers?
            [Question.DivisibleNumbersAnswers] = new TranslationInfo
            {
                QuestionText = "Какие были правильные нажатия кнопок в «{0}»?",
                ModuleName = "Делимых числах",
            },
            // What was the {1} stage’s number in {0}?
            // What was the first stage’s number in Divisible Numbers?
            [Question.DivisibleNumbersNumbers] = new TranslationInfo
            {
                QuestionText = "Какой был номер {1}-го этапа в «{0}»?",
                ModuleName = "Делимых числах",
            },

            // Double Arrows
            // What was the starting position in {0}?
            // What was the starting position in Double Arrows?
            [Question.DoubleArrowsStart] = new TranslationInfo
            {
                QuestionText = "What was the starting position in «{0}»?",
            },
            // Which {1} arrow moved {2} in the grid in {0}?
            // Which inner arrow moved up in the grid in Double Arrows?
            [Question.DoubleArrowsArrow] = new TranslationInfo
            {
                QuestionText = "Which {1} arrow moved {2} in the grid in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["inner"] = "inner",
                    ["up"] = "up",
                    ["outer"] = "outer",
                    ["down"] = "down",
                    ["left"] = "left",
                    ["riight"] = "riight",
                    ["right"] = "right",
                },
            },
            // Which direction in the grid did the {1} arrow move in {0}?
            // Which direction in the grid did the inner up arrow move in Double Arrows?
            [Question.DoubleArrowsMovement] = new TranslationInfo
            {
                QuestionText = "Which direction in the grid did the {1} arrow move in «{0}»?",
                FormatArgs = new Dictionary<string, string>
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

            // Double Color
            // What was the screen color on the {1} stage of {0}?
            // What was the screen color on the first stage of Double Color?
            [Question.DoubleColorColors] = new TranslationInfo
            {
                QuestionText = "Какого цвета был экран на {1}-м этапе в «{0}»?",
                ModuleName = "Двойных цветах",
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
            // What was the most recent digit on the {1} display in {0}?
            // What was the most recent digit on the left display in Double Digits?
            [Question.DoubleDigitsDisplays] = new TranslationInfo
            {
                QuestionText = "Какая цифра была последней на {1} дисплее в «{0}»?",
                ModuleName = "Двойных цифрах",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левом",
                    ["right"] = "правом",
                },
            },

            // Double Expert
            // What was the starting key number in {0}?
            // What was the starting key number in Double Expert?
            [Question.DoubleExpertStartingKeyNumber] = new TranslationInfo
            {
                QuestionText = "Какое было начальное ключевое число в «{0}»?",
                ModuleName = "Двойном эксперте",
            },
            // What was the word you submitted in {0}?
            // What was the word you submitted in Double Expert?
            [Question.DoubleExpertSubmittedWord] = new TranslationInfo
            {
                QuestionText = "Какое было отправленное слово в «{0}»?",
                ModuleName = "Двойном эксперте",
            },

            // Double-Oh
            // Which button was the submit button in {0}?
            // Which button was the submit button in Double-Oh?
            [Question.DoubleOhSubmitButton] = new TranslationInfo
            {
                QuestionText = "Какая кнопка была кнопкой отправки в модуле «{0}»?",
                ModuleName = "Ноль-ноль",
            },

            // Dr. Doctor
            // Which of these symptoms was listed on {0}?
            // Which of these symptoms was listed on Dr. Doctor?
            [Question.DrDoctorSymptoms] = new TranslationInfo
            {
                QuestionText = "Which of these symptoms was listed on «{0}»?",
            },
            // Which of these diseases was listed on {0}, but not the one treated?
            // Which of these diseases was listed on Dr. Doctor, but not the one treated?
            [Question.DrDoctorDiseases] = new TranslationInfo
            {
                QuestionText = "Which of these diseases was listed on «{0}», but not the one treated?",
            },

            // Dreamcipher
            // What was the decrypted word in {0}?
            // What was the decrypted word in Dreamcipher?
            [Question.DreamcipherWord] = new TranslationInfo
            {
                QuestionText = "What was the decrypted word in «{0}»?",
            },

            // The Duck
            // How did you approach the duck in {0}?
            // How did you approach the duck in The Duck?
            [Question.DuckApproach] = new TranslationInfo
            {
                QuestionText = "How did you approach the duck in «{0}»?",
            },
            // What was the color of the curtain in {0}?
            // What was the color of the curtain in The Duck?
            [Question.DuckCurtainColor] = new TranslationInfo
            {
                QuestionText = "What was the color of the curtain in «{0}»?",
            },

            // Dumb Waiters
            // Which player {1} present in {0}?
            // Which player was present in Dumb Waiters?
            [Question.DumbWaitersPlayerAvailable] = new TranslationInfo
            {
                QuestionText = "Which player {1} present in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "was",
                    ["was not"] = "was not",
                },
            },

            // Earthbound
            // What was the background number in {0}?
            // What was the background number in Earthbound?
            [Question.EarthboundBackground] = new TranslationInfo
            {
                QuestionText = "What was the background number in «{0}»?",
            },
            // Which monster was displayed in {0}?
            // Which monster was displayed in Earthbound?
            [Question.EarthboundMonster] = new TranslationInfo
            {
                QuestionText = "Which monster was displayed in «{0}»?",
            },

            // eeB gnillepS
            // What word was asked to be spelled in {0}?
            // What word was asked to be spelled in eeB gnillepS?
            [Question.eeBgnillepSWord] = new TranslationInfo
            {
                QuestionText = "What word was asked to be spelled in «{0}»?",
            },

            // Eight
            // What was the last digit on the small display in {0}?
            // What was the last digit on the small display in Eight?
            [Question.EightLastSmallDisplayDigit] = new TranslationInfo
            {
                QuestionText = "What was the last digit on the small display in «{0}»?",
            },
            // What was the position of the last broken digit in {0}?
            // What was the position of the last broken digit in Eight?
            [Question.EightLastBrokenDigitPosition] = new TranslationInfo
            {
                QuestionText = "What was the position of the last broken digit in «{0}»?",
            },
            // What were the last resulting digits in {0}?
            // What were the last resulting digits in Eight?
            [Question.EightLastResultingDigits] = new TranslationInfo
            {
                QuestionText = "What were the last resulting digits in «{0}»?",
            },
            // What was the last displayed number in {0}?
            // What was the last displayed number in Eight?
            [Question.EightLastDisplayedNumber] = new TranslationInfo
            {
                QuestionText = "What was the last displayed number in «{0}»?",
            },

            // Elder Futhark
            // What was the {1} rune shown on {0}?
            // What was the first rune shown on Elder Futhark?
            [Question.ElderFutharkRunes] = new TranslationInfo
            {
                QuestionText = "What was the {1} rune shown on «{0}»?",
            },

            // ENA Cipher
            // What was the {1} keyword in {0}?
            // What was the first keyword in ENA Cipher?
            [Question.EnaCipherKeywordAnswer] = new TranslationInfo
            {
                QuestionText = "What was the {1} keyword in «{0}»?",
            },
            // What was the transposition key in {0}?
            // What was the transposition key in ENA Cipher?
            [Question.EnaCipherExtAnswer] = new TranslationInfo
            {
                QuestionText = "What was the transposition key in «{0}»?",
            },
            // What was the encrypted word in {0}?
            // What was the encrypted word in ENA Cipher?
            [Question.EnaCipherEncryptedAnswer] = new TranslationInfo
            {
                QuestionText = "What was the encrypted word in «{0}»?",
            },

            // Encrypted Dice
            // Which of these numbers appeared on a die in the {1} stage of {0}?
            // Which of these numbers appeared on a die in the first stage of Encrypted Dice?
            [Question.EncryptedDice] = new TranslationInfo
            {
                QuestionText = "Which of these numbers appeared on a die in the {1} stage of «{0}»?",
            },

            // Encrypted Equations
            // Which shape was the {1} operand in {0}?
            // Which shape was the first operand in Encrypted Equations?
            [Question.EncryptedEquationsShapes] = new TranslationInfo
            {
                QuestionText = "Which shape was the {1} operand in «{0}»?",
            },

            // Encrypted Hangman
            // What method of encryption was used by {0}?
            // What method of encryption was used by Encrypted Hangman?
            [Question.EncryptedHangmanEncryptionMethod] = new TranslationInfo
            {
                QuestionText = "What method of encryption was used by «{0}»?",
            },
            // What module name was encrypted by {0}?
            // What module name was encrypted by Encrypted Hangman?
            [Question.EncryptedHangmanModule] = new TranslationInfo
            {
                QuestionText = "What module name was encrypted by «{0}»?",
            },

            // Encrypted Maze
            // Which symbol on {0} was spinning {1}?
            // Which symbol on Encrypted Maze was spinning clockwise?
            [Question.EncryptedMazeSymbols] = new TranslationInfo
            {
                QuestionText = "Which symbol on «{0}» was spinning {1}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["clockwise"] = "clockwise",
                    ["counter-clockwise"] = "counter-clockwise",
                },
            },

            // Encrypted Morse
            // What was the {1} on {0}?
            // What was the received call on Encrypted Morse?
            [Question.EncryptedMorseCallResponse] = new TranslationInfo
            {
                QuestionText = "What was the {1} on «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["received call"] = "received call",
                    ["sent response"] = "sent response",
                },
            },

            // Encryption Bingo
            // What was the first encoding used in {0}?
            // What was the first encoding used in Encryption Bingo?
            [Question.EncryptionBingoEncoding] = new TranslationInfo
            {
                QuestionText = "What was the first encoding used in «{0}»?",
            },

            // Enigma Cycle
            // What was the {1} in {0}?
            // What was the message in Enigma Cycle?
            [Question.EnigmaCycleWords] = new TranslationInfo
            {
                QuestionText = "What was the {1} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "message",
                    ["response"] = "response",
                },
            },

            // Entry Number Four
            // What was the first number shown in {0}?
            // What was the first number shown in Entry Number Four?
            [Question.EntryNumberFourNumber1] = new TranslationInfo
            {
                QuestionText = "What was the first number shown in «{0}»?",
            },
            // What was the second number shown in {0}?
            // What was the second number shown in Entry Number Four?
            [Question.EntryNumberFourNumber2] = new TranslationInfo
            {
                QuestionText = "What was the second number shown in «{0}»?",
            },
            // What was the third number shown in {0}?
            // What was the third number shown in Entry Number Four?
            [Question.EntryNumberFourNumber3] = new TranslationInfo
            {
                QuestionText = "What was the third number shown in «{0}»?",
            },
            // What was the expected fourth entry in {0}?
            // What was the expected fourth entry in Entry Number Four?
            [Question.EntryNumberFourExpected] = new TranslationInfo
            {
                QuestionText = "What was the expected fourth entry in «{0}»?",
            },
            // What was the constant coefficient in {0}?
            // What was the constant coefficient in Entry Number Four?
            [Question.EntryNumberFourCoeff] = new TranslationInfo
            {
                QuestionText = "What was the constant coefficient in «{0}»?",
            },

            // Entry Number One
            // What was the {1} number shown in {0}?
            // What was the first number shown in Entry Number One?
            [Question.EntryNumberOneNumbers] = new TranslationInfo
            {
                QuestionText = "What was the {1} number shown in «{0}»?",
            },
            // What was the expected first entry in {0}?
            // What was the expected first entry in Entry Number One?
            [Question.EntryNumberOneExpected] = new TranslationInfo
            {
                QuestionText = "What was the expected first entry in «{0}»?",
            },
            // What was the constant coefficient in {0}?
            // What was the constant coefficient in Entry Number One?
            [Question.EntryNumberOneCoeff] = new TranslationInfo
            {
                QuestionText = "What was the constant coefficient in «{0}»?",
            },

            // Épelle-moi Ça
            // What word was asked to be spelled in {0}?
            // What word was asked to be spelled in Épelle-moi Ça?
            [Question.EpelleMoiCaWord] = new TranslationInfo
            {
                QuestionText = "What word was asked to be spelled in «{0}»?",
            },

            // Equations X
            // What was the displayed symbol in {0}?
            // What was the displayed symbol in Equations X?
            [Question.EquationsXSymbols] = new TranslationInfo
            {
                QuestionText = "What was the displayed symbol in «{0}»?",
            },

            // Etterna
            // What was the beat for the {1} arrow from the bottom in {0}?
            // What was the beat for the first arrow from the bottom in Etterna?
            [Question.EtternaNumber] = new TranslationInfo
            {
                QuestionText = "What was the beat for the {1} arrow from the bottom in «{0}»?",
            },

            // Exoplanets
            // What was the starting target planet in {0}?
            // What was the starting target planet in Exoplanets?
            [Question.ExoplanetsStartingTargetPlanet] = new TranslationInfo
            {
                QuestionText = "What was the starting target planet in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["outer"] = "outer",
                    ["middle"] = "middle",
                    ["inner"] = "inner",
                    ["none"] = "none",
                },
            },
            // What was the starting target digit in {0}?
            // What was the starting target digit in Exoplanets?
            [Question.ExoplanetsStartingTargetDigit] = new TranslationInfo
            {
                QuestionText = "What was the starting target digit in «{0}»?",
            },
            // What was the final target planet in {0}?
            // What was the final target planet in Exoplanets?
            [Question.ExoplanetsTargetPlanet] = new TranslationInfo
            {
                QuestionText = "What was the final target planet in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["outer"] = "outer",
                    ["middle"] = "middle",
                    ["inner"] = "inner",
                    ["none"] = "none",
                },
            },
            // What was the final target digit in {0}?
            // What was the final target digit in Exoplanets?
            [Question.ExoplanetsTargetDigit] = new TranslationInfo
            {
                QuestionText = "What was the final target digit in «{0}»?",
            },

            // Factoring Maze
            // What was one of the prime numbers chosen in {0}?
            // What was one of the prime numbers chosen in Factoring Maze?
            [Question.FactoringMazeChosenPrimes] = new TranslationInfo
            {
                QuestionText = "What was one of the prime numbers chosen in «{0}»?",
            },

            // Factory Maze
            // What room did you start in in {0}?
            // What room did you start in in Factory Maze?
            [Question.FactoryMazeStartRoom] = new TranslationInfo
            {
                QuestionText = "Какая была начальная (первая) комната в «{0}»?",
                ModuleName = "Заводском лабиринте",
            },

            // Fast Math
            // What was the last pair of letters in {0}?
            // What was the last pair of letters in Fast Math?
            [Question.FastMathLastLetters] = new TranslationInfo
            {
                QuestionText = "Какая пара букв была последней в «{0}»?",
                ModuleName = "Быстрой математике",
            },

            // Faulty Buttons
            // Which button referred to the {1} button in reading order in {0}?
            // Which button referred to the first button in reading order in Faulty Buttons?
            [Question.FaultyButtonsReferredToThisButton] = new TranslationInfo
            {
                QuestionText = "Which button referred to the {1} button in reading order in «{0}»?",
            },
            // Which button did the {1} button in reading order refer to in {0}?
            // Which button did the first button in reading order refer to in Faulty Buttons?
            [Question.FaultyButtonsThisButtonReferredTo] = new TranslationInfo
            {
                QuestionText = "Which button did the {1} button in reading order refer to in «{0}»?",
            },

            // Faulty RGB Maze
            // What was the exit coordinate in {0}?
            // What was the exit coordinate in Faulty RGB Maze?
            [Question.FaultyRGBMazeExit] = new TranslationInfo
            {
                QuestionText = "What was the exit coordinate in «{0}»?",
            },
            // Where was the {1} key in {0}?
            // Where was the red key in Faulty RGB Maze?
            [Question.FaultyRGBMazeKeys] = new TranslationInfo
            {
                QuestionText = "Where was the {1} key in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                },
            },
            // Which maze number was the {1} maze in {0}?
            // Which maze number was the red maze in Faulty RGB Maze?
            [Question.FaultyRGBMazeNumber] = new TranslationInfo
            {
                QuestionText = "Which maze number was the {1} maze in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                },
            },

            // Find The Date
            // What was the day displayed in the {1} stage of {0}?
            // What was the day displayed in the first stage of Find The Date?
            [Question.FindTheDateDay] = new TranslationInfo
            {
                QuestionText = "Какой день был показан на {1}-м этапе в модуле «{0}»?",
                ModuleName = "Найди дату",
            },
            // What was the month displayed in the {1} stage of {0}?
            // What was the month displayed in the first stage of Find The Date?
            [Question.FindTheDateMonth] = new TranslationInfo
            {
                QuestionText = "Какой месяц был показан на {1}-м этапе в модуле «{0}»?",
                ModuleName = "Найди дату",
            },
            // What was the year displayed in the {1} stage of {0}?
            // What was the year displayed in the first stage of Find The Date?
            [Question.FindTheDateYear] = new TranslationInfo
            {
                QuestionText = "Какой год был показан на {1}-м этапе в модуле «{0}»?",
                ModuleName = "Найди дату",
            },

            // Five Letter Words
            // Which of these words was on the display in {0}?
            // Which of these words was on the display in Five Letter Words?
            [Question.FiveLetterWordsDisplayedWords] = new TranslationInfo
            {
                QuestionText = "Какое из этих слов было на дисплее в «{0}»?",
                ModuleName = "Пятибуквенных словах",
            },

            // FizzBuzz
            // What was the {1} digit on the {2} display of {0}?
            // What was the first digit on the top display of FizzBuzz?
            [Question.FizzBuzzDisplayedNumbers] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я цифра на {2} дисплее в модуле «{0}»?",
                ModuleName = "FizzBuzz",
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
            [Question.FlagsDisplayedNumber] = new TranslationInfo
            {
                QuestionText = "Какое число было показано на экране во «{0}»?",
                ModuleName = "Флагах",
            },
            // What was the main country flag in {0}?
            // What was the main country flag in Flags?
            [Question.FlagsMainCountry] = new TranslationInfo
            {
                QuestionText = "Какой главный флаг отображался во «{0}»?",
                ModuleName = "Флагах",
            },
            // Which of these country flags was shown, but not the main country flag, in {0}?
            // Which of these country flags was shown, but not the main country flag, in Flags?
            [Question.FlagsCountries] = new TranslationInfo
            {
                QuestionText = "Какой из этих флагов был показан (но не являлся главным флагом) во «{0}»?",
                ModuleName = "Флагах",
            },

            // Flashing Arrows
            // What number was displayed on {0}?
            // What number was displayed on Flashing Arrows?
            [Question.FlashingArrowsDisplayedValue] = new TranslationInfo
            {
                QuestionText = "Какое число было показано в «{0}»?",
                ModuleName = "Мигающих стрелках",
            },
            // What color flashed {1} black on the relevant arrow in {0}?
            // What color flashed before black on the relevant arrow in Flashing Arrows?
            [Question.FlashingArrowsReferredArrow] = new TranslationInfo
            {
                QuestionText = "Какой цвет мигнул {1} на соответствующей стрелке в «{0}»?",
                ModuleName = "Мигающих стрелках",
                FormatArgs = new Dictionary<string, string>
                {
                    ["before"] = "перед чёрным",
                    ["after"] = "после чёрного",
                },
            },

            // Flashing Lights
            // How many times did the {1} LED flash {2} on {0}?
            // How many times did the top LED flash cyan on Flashing Lights?
            [Question.FlashingLightsLEDFrequency] = new TranslationInfo
            {
                QuestionText = "How many times did the {1} LED flash {2} on «{0}»?",
                FormatArgs = new Dictionary<string, string>
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

            // Flavor Text
            // Which module’s flavor text was shown in {0}?
            // Which module’s flavor text was shown in Flavor Text?
            [Question.FlavorTextModule] = new TranslationInfo
            {
                QuestionText = "Which module’s flavor text was shown in «{0}»?",
            },

            // Flavor Text EX
            // Which module’s flavor text was shown in the {1} stage of {0}?
            // Which module’s flavor text was shown in the first stage of Flavor Text EX?
            [Question.FlavorTextEXModule] = new TranslationInfo
            {
                QuestionText = "Which module’s flavor text was shown in the {1} stage of «{0}»?",
            },

            // Flyswatting
            // Which fly was present, but not in the solution in {0}?
            // Which fly was present, but not in the solution in Flyswatting?
            [Question.FlyswattingUnpressed] = new TranslationInfo
            {
                QuestionText = "Which fly was present, but not in the solution in «{0}»?",
            },

            // Follow Me
            // What was the {1} flashing direction in {0}?
            // What was the first flashing direction in Follow Me?
            [Question.FollowMeDisplayedPath] = new TranslationInfo
            {
                QuestionText = "What was the {1} flashing direction in «{0}»?",
            },

            // Forest Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Forest Cipher?
            [Question.ForestCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Лесном шифре",
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
            [Question.ForgetAnyColorCylinder] = new TranslationInfo
            {
                QuestionText = "What were the cylinders during stage {1} in «{0}»?",
            },
            // Which figure was used during the {1} stage of {0}?
            // Which figure was used during the first stage of Forget Any Color?
            [Question.ForgetAnyColorSequence] = new TranslationInfo
            {
                QuestionText = "What figure was used during stage {1} in «{0}»?",
            },

            // Forget Everything
            // What was the {1} displayed digit in the first stage of {0}?
            // What was the first displayed digit in the first stage of Forget Everything?
            [Question.ForgetEverythingStageOneDisplay] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я отображённая цифра на первом этапе в «{0}»?",
                ModuleName = "Полном забвении",
            },

            // Forget Me
            // What number was in the {1} position of the initial puzzle in {0}?
            // What number was in the top-left position of the initial puzzle in Forget Me?
            [Question.ForgetMeInitialState] = new TranslationInfo
            {
                QuestionText = "Какое число было на {1} кнопке изначального пазла в модуле «{0}»?",
                ModuleName = "Забудь меня",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхней левой",
                    ["top-middle"] = "верхней центральной",
                    ["top-right"] = "верхней правой",
                    ["middle-left"] = "центральной левой",
                    ["center"] = "центральной",
                    ["middle-right"] = "центральной правой",
                    ["bottom-left"] = "нижней левой",
                    ["bottom-middle"] = "нижней центральной",
                    ["bottom-right"] = "нижней правой",
                },
            },

            // Forget Me Not
            // What was the digit displayed in the {1} stage of {0}?
            // What was the digit displayed in the first stage of Forget Me Not?
            [Question.ForgetMeNotDisplayedDigits] = new TranslationInfo
            {
                QuestionText = "Какая цифра была отображена на {1}-м этапе в «{0}»?",
                ModuleName = "Незабудке",
            },

            // Forget Me Now
            // What was the {1} displayed digit in {0}?
            // What was the first displayed digit in Forget Me Now?
            [Question.ForgetMeNowDisplayedDigits] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я отображённая цифра в «{0}»?",
                ModuleName = "Забудке",
            },

            // Forget’s Ultimate Showdown
            // What was the {1} digit of the answer in {0}?
            // What was the first digit of the answer in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownAnswer] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я цифра ответа в «{0}»?",
                ModuleName = "Финальной битве забвения",
            },
            // What was the {1} digit of the initial number in {0}?
            // What was the first digit of the initial number in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownInitial] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я цифра изначального числа в «{0}»?",
                ModuleName = "Финальной битве забвения",
            },
            // What was the {1} digit of the bottom number in {0}?
            // What was the first digit of the bottom number in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownBottom] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я цифра нижнего числа в «{0}»?",
                ModuleName = "Финальной битве забвения",
            },
            // What was the {1} method used in {0}?
            // What was the first method used in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownMethod] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й использованный метод в «{0}»?",
                ModuleName = "Финальной битве забвения",
            },

            // Forget The Colors
            // What number was on the gear during stage {1} of {0}?
            // What number was on the gear during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsGearNumber] = new TranslationInfo
            {
                QuestionText = "What number was on the gear during stage {1} in «{0}»?",
            },
            // What number was on the large display during stage {1} of {0}?
            // What number was on the large display during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsLargeDisplay] = new TranslationInfo
            {
                QuestionText = "What number was on the large display during stage {1} in «{0}»?",
            },
            // What was the last decimal in the sine number received during stage {1} of {0}?
            // What was the last decimal in the sine number received during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsSineNumber] = new TranslationInfo
            {
                QuestionText = "What was the last decimal in the sine number received during stage {1} in «{0}»?",
            },
            // What color was the gear during stage {1} of {0}?
            // What color was the gear during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsGearColor] = new TranslationInfo
            {
                QuestionText = "What color was the gear during stage {1} in «{0}»?",
                Answers = new Dictionary<string, string>
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
                    ["Gray"] = "Gray",
                },
            },
            // Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?
            // Which edgework-based rule was applied to the sum of nixies and gear during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsRuleColor] = new TranslationInfo
            {
                QuestionText = "Which edgework-based rule was applied to the sum of nixies and gear during stage {1} in «{0}»?",
                Answers = new Dictionary<string, string>
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
                    ["Gray"] = "Gray",
                },
            },

            // Forget This
            // What color was the LED in the {1} stage of {0}?
            // What color was the LED in the first stage of Forget This?
            [Question.ForgetThisColors] = new TranslationInfo
            {
                QuestionText = "What color was the LED in the {1} stage of «{0}»?",
            },
            // What was the digit displayed in the {1} stage of {0}?
            // What was the digit displayed in the first stage of Forget This?
            [Question.ForgetThisDigits] = new TranslationInfo
            {
                QuestionText = "What was the digit displayed in the {1} stage of «{0}»?",
            },

            // Free Parking
            // What was the player token in {0}?
            // What was the player token in Free Parking?
            [Question.FreeParkingToken] = new TranslationInfo
            {
                QuestionText = "Какой был жетон игрока в «{0}»?",
                ModuleName = "Бесплатной парковке",
            },

            // Functions
            // What was the last digit of your first query’s result in {0}?
            // What was the last digit of your first query’s result in Functions?
            [Question.FunctionsLastDigit] = new TranslationInfo
            {
                QuestionText = "Какая была последняя цифра результата вашего первого запроса в модуле «{0}»?",
                ModuleName = "Функции",
            },
            // What number was to the left of the displayed letter in {0}?
            // What number was to the left of the displayed letter in Functions?
            [Question.FunctionsLeftNumber] = new TranslationInfo
            {
                QuestionText = "Какое число было слева от отображённой буквы в модуле «{0}»?",
                ModuleName = "Функции",
            },
            // What letter was displayed in {0}?
            // What letter was displayed in Functions?
            [Question.FunctionsLetter] = new TranslationInfo
            {
                QuestionText = "Какая буква была отображена в модуле «{0}»?",
                ModuleName = "Функции",
            },
            // What number was to the right of the displayed letter in {0}?
            // What number was to the right of the displayed letter in Functions?
            [Question.FunctionsRightNumber] = new TranslationInfo
            {
                QuestionText = "Какое число было справа от отображённой буквы в модуле «{0}»?",
                ModuleName = "Функции",
            },

            // The Fuse Box
            // What color flashed {1} in {0}?
            // What color flashed first in The Fuse Box?
            [Question.FuseBoxFlashes] = new TranslationInfo
            {
                QuestionText = "Какой цвет горел {1}-м в «{0}»?",
                ModuleName = "Блоке предохранителей",
            },
            // What arrow was shown {1} in {0}?
            // What arrow was shown first in The Fuse Box?
            [Question.FuseBoxArrows] = new TranslationInfo
            {
                QuestionText = "Какая стрелка была показана {1}-й в «{0}»?",
                ModuleName = "Блоке предохранителей",
            },

            // Gadgetron Vendor
            // What was your current weapon in {0}?
            // What was your current weapon in Gadgetron Vendor?
            [Question.GadgetronVendorCurrentWeapon] = new TranslationInfo
            {
                QuestionText = "What was your current weapon in «{0}»?",
            },
            // What was the weapon up for sale in {0}?
            // What was the weapon up for sale in Gadgetron Vendor?
            [Question.GadgetronVendorWeaponForSale] = new TranslationInfo
            {
                QuestionText = "What was the weapon up for sale in «{0}»?",
            },

            // Game of Life Cruel
            // Which of these was a color combination that occurred in {0}?
            // Which of these was a color combination that occurred in Game of Life Cruel?
            [Question.GameOfLifeCruelColors] = new TranslationInfo
            {
                QuestionText = "Какие комбинации цветов присутствовали в «{0}»?",
                ModuleName = "Жестокой игре Жизнь",
            },

            // The Gamepad
            // What were the numbers on {0}?
            // What were the numbers on The Gamepad?
            [Question.GamepadNumbers] = new TranslationInfo
            {
                QuestionText = "Какие числа были на экране в «{0}»?",
                ModuleName = "Геймпаде",
            },

            // The Garnet Thief
            // Which faction did {1} claim to be in {0}?
            // Which faction did Jungmoon claim to be in The Garnet Thief?
            [Question.GarnetThiefClaim] = new TranslationInfo
            {
                QuestionText = "Which faction did {1} claim to be in «{0}»?",
            },

            // Girlfriend
            // What was the language sung in {0}?
            // What was the language sung in Girlfriend?
            [Question.GirlfriendLanguage] = new TranslationInfo
            {
                QuestionText = "What was the language sung in «{0}»?",
            },

            // The Glitched Button
            // What was the cycling bit sequence in {0}?
            // What was the cycling bit sequence in The Glitched Button?
            [Question.GlitchedButtonSequence] = new TranslationInfo
            {
                QuestionText = "Какая последовательность битов повторялась в «{0}»?",
                ModuleName = "Глитч-кнопке",
            },

            // The Gray Button
            // What was the {1} coordinate on the display in {0}?
            // What was the horizontal coordinate on the display in The Gray Button?
            [Question.GrayButtonCoordinates] = new TranslationInfo
            {
                QuestionText = "Какие были {1} координаты на экране в «{0}»?",
                ModuleName = "Серой кнопке",
                FormatArgs = new Dictionary<string, string>
                {
                    ["horizontal"] = "горизонтальные",
                    ["vertical"] = "вертикальные",
                },
            },

            // Gray Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Gray Cipher?
            [Question.GrayCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Сером шифре",
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
            [Question.GreatVoidColor] = new TranslationInfo
            {
                QuestionText = "What was the {1} color in «{0}»?",
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
            [Question.GreatVoidDigit] = new TranslationInfo
            {
                QuestionText = "What was the {1} digit in «{0}»?",
            },

            // Green Arrows
            // What was the last number on the display on {0}?
            // What was the last number on the display on Green Arrows?
            [Question.GreenArrowsLastScreen] = new TranslationInfo
            {
                QuestionText = "Какое последнее число было показано на экране в «{0}»?",
                ModuleName = "Зелёных стрелках",
            },

            // The Green Button
            // What was the word submitted in {0}?
            // What was the word submitted in The Green Button?
            [Question.GreenButtonWord] = new TranslationInfo
            {
                QuestionText = "Какое слово было введено в «{0}»?",
                ModuleName = "Зелёной кнопке",
            },

            // Green Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Green Cipher?
            [Question.GreenCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Зелёном шифре",
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
            [Question.GridLockStartingLocation] = new TranslationInfo
            {
                QuestionText = "Какая была начальная позиция в «{0}»?",
                ModuleName = "Тупике",
            },
            // What was the ending location in {0}?
            // What was the ending location in Gridlock?
            [Question.GridLockEndingLocation] = new TranslationInfo
            {
                QuestionText = "Какая была конечная позиция в «{0}»?",
                ModuleName = "Тупике",
            },
            // What was the starting color in {0}?
            // What was the starting color in Gridlock?
            [Question.GridLockStartingColor] = new TranslationInfo
            {
                QuestionText = "Какой был начальный цвет в «{0}»?",
                ModuleName = "Тупике",
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
            [Question.GroceryStoreFirstItem] = new TranslationInfo
            {
                QuestionText = "Какой товар был показан первым в «{0}»?",
                ModuleName = "Продуктовом магазине",
            },

            // Gryphons
            // What was the gryphon’s name in {0}?
            // What was the gryphon’s name in Gryphons?
            [Question.GryphonsName] = new TranslationInfo
            {
                QuestionText = "What was the gryphon’s name in «{0}»?",
            },
            // What was the gryphon’s age in {0}?
            // What was the gryphon’s age in Gryphons?
            [Question.GryphonsAge] = new TranslationInfo
            {
                QuestionText = "What was the gryphon’s age in «{0}»?",
            },

            // Guess Who?
            // Who was the person recalled in {0}?
            // Who was the person recalled in Guess Who??
            [Question.GuessWhoPerson] = new TranslationInfo
            {
                QuestionText = "Какого человека вспомнили в модуле «{0}»?",
                ModuleName = "Угадай, кто?",
            },

            // h
            // What was the transmitted letter in {0}?
            // What was the transmitted letter in h?
            [Question.HLetter] = new TranslationInfo
            {
                QuestionText = "What was the transmitted letter in «{0}»?",
            },

            // Hereditary Base Notation
            // What was the given number in {0}?
            // What was the given number in Hereditary Base Notation?
            [Question.HereditaryBaseNotationInitialNumber] = new TranslationInfo
            {
                QuestionText = "What was the given number in «{0}»?",
            },

            // The Hexabutton
            // What label was printed on {0}?
            // What label was printed on The Hexabutton?
            [Question.HexabuttonLabel] = new TranslationInfo
            {
                QuestionText = "Какая была надпись на «{0}»?",
                ModuleName = "Гексакнопке",
            },

            // Hexamaze
            // What was the color of the pawn in {0}?
            // What was the color of the pawn in Hexamaze?
            [Question.HexamazePawnColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета была фигурка в «{0}»?",
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

            // hexOS
            // What were the deciphered letters in {0}?
            // What were the deciphered letters in hexOS?
            [Question.HexOSCipher] = new TranslationInfo
            {
                QuestionText = "What were the deciphered letters in «{0}»?",
            },
            // What was the deciphered phrase in {0}?
            // What was the deciphered phrase in hexOS?
            [Question.HexOSOctCipher] = new TranslationInfo
            {
                QuestionText = "What was the deciphered phrase in «{0}»?",
            },
            // What was the {1} 3-digit number cycled by the screen in {0}?
            // What was the first 3-digit number cycled by the screen in hexOS?
            [Question.HexOSScreen] = new TranslationInfo
            {
                QuestionText = "What was the {1} 3-digit number cycled by the screen in «{0}»?",
            },
            // What were the rhythm values in {0}?
            // What were the rhythm values in hexOS?
            [Question.HexOSSum] = new TranslationInfo
            {
                QuestionText = "What were the rhythm values in «{0}»?",
            },

            // Hidden Colors
            // What was the color of the main LED in {0}?
            // What was the color of the main LED in Hidden Colors?
            [Question.HiddenColorsLED] = new TranslationInfo
            {
                QuestionText = "Какого цвета был главный светодиод в «{0}»?",
                ModuleName = "Скрытых цветах",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красного",
                    ["Blue"] = "Синего",
                    ["Green"] = "Зелёного",
                    ["Yellow"] = "Жёлтого",
                    ["Orange"] = "Оранжевого",
                    ["Purple"] = "Фиолетового",
                    ["Magenta"] = "Розового",
                    ["White"] = "Белого",
                },
            },

            // The High Score
            // What was the position of the player in {0}?
            // What was the position of the player in The High Score?
            [Question.HighScorePosition] = new TranslationInfo
            {
                QuestionText = "What was the position of the player in «{0}»?",
            },
            // What was the score of the player in {0}?
            // What was the score of the player in The High Score?
            [Question.HighScoreScore] = new TranslationInfo
            {
                QuestionText = "What was the score of the player in «{0}»?",
            },

            // Hill Cycle
            // What was the {1} in {0}?
            // What was the message in Hill Cycle?
            [Question.HillCycleWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "message",
                    ["response"] = "response",
                },
            },

            // Hinges
            // Which of these hinges was initially {1} {0}?
            // Which of these hinges was initially present on Hinges?
            [Question.HingesInitialHinges] = new TranslationInfo
            {
                QuestionText = "Which of these hinges was initially {1} «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["present on"] = "present on",
                    ["absent from"] = "absent from",
                },
            },

            // Hogwarts
            // Which House was {1} solved for in {0}?
            // Which House was Binary Puzzle solved for in Hogwarts?
            [Question.HogwartsHouse] = new TranslationInfo
            {
                QuestionText = "Which House was {1} solved for in «{0}»?",
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
            [Question.HogwartsModule] = new TranslationInfo
            {
                QuestionText = "Which module was solved for {1} in «{0}»?",
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
            [Question.HoldUpsShadows] = new TranslationInfo
            {
                QuestionText = "What was the name of the {1} shadow shown in «{0}»?",
            },

            // Horrible Memory
            // In what position was the button pressed on the {1} stage of {0}?
            // In what position was the button pressed on the first stage of Horrible Memory?
            [Question.HorribleMemoryPositions] = new TranslationInfo
            {
                QuestionText = "In what position was the button pressed on the {1} stage of «{0}»?",
            },
            // What was the label of the button pressed on the {1} stage of {0}?
            // What was the label of the button pressed on the first stage of Horrible Memory?
            [Question.HorribleMemoryLabels] = new TranslationInfo
            {
                QuestionText = "What was the label of the button pressed on the {1} stage of «{0}»?",
            },
            // What color was the button pressed on the {1} stage of {0}?
            // What color was the button pressed on the first stage of Horrible Memory?
            [Question.HorribleMemoryColors] = new TranslationInfo
            {
                QuestionText = "What color was the button pressed on the {1} stage of «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "blue",
                    ["green"] = "green",
                    ["red"] = "red",
                    ["orange"] = "orange",
                    ["purple"] = "purple",
                    ["pink"] = "pink",
                },
            },

            // Homophones
            // What was the {1} displayed phrase in {0}?
            // What was the first displayed phrase in Homophones?
            [Question.HomophonesDisplayedPhrases] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я показанная фраза в «{0}»?",
                ModuleName = "Омофонах",
            },

            // Human Resources
            // Which was a descriptor shown in {1} in {0}?
            // Which was a descriptor shown in red in Human Resources?
            [Question.HumanResourcesDescriptors] = new TranslationInfo
            {
                QuestionText = "Какие описания «{0}» были показаны {1} цветом?",
                ModuleName = "Человеческих ресурсов",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красным",
                    ["green"] = "зелёным",
                },
            },
            // Who was {1} in {0}?
            // Who was fired in Human Resources?
            [Question.HumanResourcesHiredFired] = new TranslationInfo
            {
                QuestionText = "Кто из «{0}» был {1}?",
                ModuleName = "Человеческих ресурсов",
                FormatArgs = new Dictionary<string, string>
                {
                    ["fired"] = "уволен",
                    ["hired"] = "нанят",
                },
            },

            // Hunting
            // Which of the first three stages of {0} had the {1} symbol {2}?
            // Which of the first three stages of Hunting had the column symbol first?
            [Question.HuntingColumnsRows] = new TranslationInfo
            {
                QuestionText = "На каком из первых трёх этапов «{0}» символ {1} был {2}-м?",
                ModuleName = "Охоты",
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
            [Question.HypercubeRotations] = new TranslationInfo
            {
                QuestionText = "Каким было {1}-е вращение «{0}»?",
                ModuleName = "Гиперкуба",
            },

            // The Hyperlink
            // What was the {1} character of the hyperlink in {0}?
            // What was the first character of the hyperlink in The Hyperlink?
            [Question.HyperlinkCharacters] = new TranslationInfo
            {
                QuestionText = "What was the {1} character of the hyperlink in «{0}»?",
            },
            // Which module was referenced on {0}?
            // Which module was referenced on The Hyperlink?
            [Question.HyperlinkAnswer] = new TranslationInfo
            {
                QuestionText = "Which module was referenced on «{0}»?",
            },

            // Ice Cream
            // Which one of these flavours {1} to the {2} customer in {0}?
            // Which one of these flavours was on offer, but not sold, to the first customer in Ice Cream?
            [Question.IceCreamFlavour] = new TranslationInfo
            {
                QuestionText = "Какой из этих вкусов {1} {2}-му посетителю в «{0}»?",
                ModuleName = "Мороженом",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was on offer, but not sold,"] = "был предложен, но не продан",
                    ["was not on offer"] = "не был предложен",
                },
            },
            // Who was the {1} customer in {0}?
            // Who was the first customer in Ice Cream?
            [Question.IceCreamCustomer] = new TranslationInfo
            {
                QuestionText = "Кто был {1}-м посетителем в «{0}»?",
                ModuleName = "Мороженом",
            },

            // Identification Crisis
            // What was the {1} shape used in {0}?
            // What was the first shape used in Identification Crisis?
            [Question.IdentificationCrisisShape] = new TranslationInfo
            {
                QuestionText = "What was the {1} shape used in «{0}»?",
            },
            // What was the {1} identification module used in {0}?
            // What was the first identification module used in Identification Crisis?
            [Question.IdentificationCrisisDataset] = new TranslationInfo
            {
                QuestionText = "What was the {1} identification module used in «{0}»?",
            },

            // Identity Parade
            // Which hair color {1} listed in {0}?
            // Which hair color was listed in Identity Parade?
            [Question.IdentityParadeHairColors] = new TranslationInfo
            {
                QuestionText = "Какой цвет волос {1} в «{0}»?",
                ModuleName = "Параде идентичности",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "присутствовал",
                    ["was not"] = "отсутствовал",
                },
            },
            // Which build {1} listed in {0}?
            // Which build was listed in Identity Parade?
            [Question.IdentityParadeBuilds] = new TranslationInfo
            {
                QuestionText = "Какое телосложение {1} в «{0}»?",
                ModuleName = "Параде идентичности",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "присутствовало",
                    ["was not"] = "отсутствовало",
                },
            },
            // Which attire {1} listed in {0}?
            // Which attire was listed in Identity Parade?
            [Question.IdentityParadeAttires] = new TranslationInfo
            {
                QuestionText = "Какой наряд {1} в «{0}»?",
                ModuleName = "Параде идентичности",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "присутствовал",
                    ["was not"] = "отсутствовал",
                },
            },

            // The Impostor
            // Which module was {0} pretending to be?
            // Which module was The Impostor pretending to be?
            [Question.ImpostorDisguise] = new TranslationInfo
            {
                QuestionText = "Каким модулем притворялся «{0}»?",
                ModuleName = "Самозванец",
            },

            // Indigo Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Indigo Cipher?
            [Question.IndigoCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Шифре индиго",
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
            [Question.InfiniteLoopSelectedWord] = new TranslationInfo
            {
                QuestionText = "What was the selected word in «{0}»?",
            },

            // Ingredients
            // Which ingredient was used in {0}?
            // Which ingredient was used in Ingredients?
            [Question.IngredientsIngredients] = new TranslationInfo
            {
                QuestionText = "Какие ингредиенты были использованы в «{0}»?",
                ModuleName = "Ингредиентах",
            },
            // Which ingredient was listed but not used in {0}?
            // Which ingredient was listed but not used in Ingredients?
            [Question.IngredientsNonIngredients] = new TranslationInfo
            {
                QuestionText = "Какие ингредиенты были указаны, но не были использованы в «{0}»?",
                ModuleName = "Ингредиентах",
            },

            // Inner Connections
            // What color was the LED in {0}?
            // What color was the LED in Inner Connections?
            [Question.InnerConnectionsLED] = new TranslationInfo
            {
                QuestionText = "What color was the LED in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["Black"] = "Black",
                    ["Blue"] = "Blue",
                    ["Red"] = "Red",
                    ["White"] = "White",
                    ["Yellow"] = "Yellow",
                    ["Green"] = "Green",
                },
            },
            // What was the digit flashed in Morse in {0}?
            // What was the digit flashed in Morse in Inner Connections?
            [Question.InnerConnectionsMorse] = new TranslationInfo
            {
                QuestionText = "What was the digit flashed in Morse in «{0}»?",
            },

            // Interpunct
            // What was the symbol displayed in the {1} stage of {0}?
            // What was the symbol displayed in the first stage of Interpunct?
            [Question.InterpunctDisplay] = new TranslationInfo
            {
                QuestionText = "What was the symbol displayed in the {1} stage of «{0}»?",
            },

            // IPA
            // What symbol was the correct answer in {0}?
            // What symbol was the correct answer in IPA?
            [Question.IpaSymbol] = new TranslationInfo
            {
                QuestionText = "What symbol was the correct answer in «{0}»?",
            },

            // The iPhone
            // What was the {1} PIN digit in {0}?
            // What was the first PIN digit in The iPhone?
            [Question.iPhoneDigits] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я цифра пинкода в «{0}»?",
                ModuleName = "iPhone",
            },

            // Jenga
            // Which symbol was on the first correctly pulled block in {0}?
            // Which symbol was on the first correctly pulled block in Jenga?
            [Question.JengaFirstBlock] = new TranslationInfo
            {
                QuestionText = "Какой символ был на первом правильно вытянутом блоке в «{0}»?",
                ModuleName = "Дженге",
            },

            // The Jewel Vault
            // What number was wheel {1} in {0}?
            // What number was wheel A in The Jewel Vault?
            [Question.JewelVaultWheels] = new TranslationInfo
            {
                QuestionText = "Какой был номер у колеса {1} в «{0}»?",
                ModuleName = "Сейфе сокровищ",
            },

            // Jumble Cycle
            // What was the {1} in {0}?
            // What was the message in Jumble Cycle?
            [Question.JumbleCycleWord] = new TranslationInfo
            {
                QuestionText = "{1} в «{0}»?",
                ModuleName = "Беспорядочном цикле",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Какое было сообщение",
                    ["response"] = "Какой был ответ",
                },
            },

            // Juxtacolored Squares
            // What was the color of this square in {0}?
            // What was the color of this square in Juxtacolored Squares?
            [Question.JuxtacoloredSquaresColorsByPosition] = new TranslationInfo
            {
                QuestionText = "Какого цвета был этот квадрат в «{0}»?",
                ModuleName = "Смежно-цветных квадратах",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красного",
                    ["Blue"] = "Синего",
                    ["Yellow"] = "Жёлтого",
                    ["Green"] = "Зелёного",
                    ["Magenta"] = "Мадженты",
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
            [Question.JuxtacoloredSquaresPositionsByColor] = new TranslationInfo
            {
                QuestionText = "Какой квадрат был {1} в «{0}»?",
                ModuleName = "Смежно-цветных квадратах",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красного цвета",
                    ["blue"] = "синего цвета",
                    ["yellow"] = "жёлтого цвета",
                    ["green"] = "зелёного цвета",
                    ["magenta"] = "цвета мадженты",
                    ["orange"] = "оранжевого цвета",
                    ["cyan"] = "голубого цвета",
                    ["purple"] = "фиолетового цвета",
                    ["chestnut"] = "каштанового цвета",
                    ["brown"] = "коричневого цвета",
                    ["mauve"] = "лилового цвета",
                    ["azure"] = "лазурного цвета",
                    ["jade"] = "нефритового цвета",
                    ["forest"] = "лесного цвета",
                    ["gray"] = "серого цвета",
                    ["black"] = "чёрного цвета",
                },
            },

            // Kanji
            // What was the displayed word in the {1} stage of {0}?
            // What was the displayed word in the first stage of Kanji?
            [Question.KanjiDisplayedWords] = new TranslationInfo
            {
                QuestionText = "What was the displayed word in the {1} stage of «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first"] = "first",
                    ["second"] = "second",
                    ["third"] = "third",
                },
            },

            // The Kanye Encounter
            // What was a food item displayed in {0}?
            // What was a food item displayed in The Kanye Encounter?
            [Question.KanyeEncounterFoods] = new TranslationInfo
            {
                QuestionText = "What was a food item displayed in «{0}»?",
            },

            // Keypad Combinations
            // Which number was displayed on the {1} button, but not part of the answer on {0}?
            // Which number was displayed on the first button, but not part of the answer on Keypad Combinations?
            [Question.KeypadCombinationWrongNumbers] = new TranslationInfo
            {
                QuestionText = "Which number was displayed on the {1} button, but not part of the answer on «{0}»?",
            },

            // Keypad Magnified
            // What was the position of the LED in {0}?
            // What was the position of the LED in Keypad Magnified?
            [Question.KeypadMagnifiedLED] = new TranslationInfo
            {
                QuestionText = "What was the position of the LED in «{0}»?",
            },

            // Keywords
            // What were the first four letters on the display in {0}?
            // What were the first four letters on the display in Keywords?
            [Question.KeywordsDisplayedKey] = new TranslationInfo
            {
                QuestionText = "What were the first four letters on the display in «{0}»?",
            },

            // Know Your Way
            // Which way was the arrow pointing in {0}?
            // Which way was the arrow pointing in Know Your Way?
            [Question.KnowYourWayArrow] = new TranslationInfo
            {
                QuestionText = "Which way was the arrow pointing in «{0}»?",
            },
            // Which LED was green in {0}?
            // Which LED was green in Know Your Way?
            [Question.KnowYourWayLed] = new TranslationInfo
            {
                QuestionText = "Which LED was green in «{0}»?",
            },

            // Kudosudoku
            // Which square was {1} in {0}?
            // Which square was pre-filled in Kudosudoku?
            [Question.KudosudokuPrefilled] = new TranslationInfo
            {
                QuestionText = "Какой квадрат {1} в «{0}»?",
                ModuleName = "Кудосудоку",
                FormatArgs = new Dictionary<string, string>
                {
                    ["pre-filled"] = "был изначально заполнен",
                    ["not pre-filled"] = "не был изначально заполнен",
                },
            },

            // The Labyrinth
            // Where was one of the portals in layer {1} in {0}?
            // Where was one of the portals in layer 1 (Red) in The Labyrinth?
            [Question.LabyrinthPortalLocations] = new TranslationInfo
            {
                QuestionText = "Где находился один из порталов на {1} слое в «{0}»?",
                ModuleName = "Многослойном лабиринте",
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
            [Question.LabyrinthPortalStage] = new TranslationInfo
            {
                QuestionText = "На каком слое находился этот портал в «{0}»?",
                ModuleName = "Многослойном лабиринте",
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
            [Question.LadderLotteryLightOn] = new TranslationInfo
            {
                QuestionText = "Which light was on in «{0}»?",
            },

            // Ladders
            // Which color was present on the second ladder in {0}?
            // Which color was present on the second ladder in Ladders?
            [Question.LaddersStage2Colors] = new TranslationInfo
            {
                QuestionText = "Which color was present on the second ladder in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Yellow",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Purple"] = "Purple",
                    ["Gray"] = "Gray",
                },
            },
            // What color was missing on the third ladder in {0}?
            // What color was missing on the third ladder in Ladders?
            [Question.LaddersStage3Missing] = new TranslationInfo
            {
                QuestionText = "What color was missing on the third ladder in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Yellow",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Cyan"] = "Cyan",
                    ["Purple"] = "Purple",
                    ["Gray"] = "Gray",
                },
            },

            // Langton’s Anteater
            // Which of these squares was initially {1} in {0}?
            // Which of these squares was initially black in Langton’s Anteater?
            [Question.LangtonsAnteaterInitialState] = new TranslationInfo
            {
                QuestionText = "Какой из этих квадратов изначально был {1} в «{0}»?",
                ModuleName = "Муравьеде Лэнгтона",
                FormatArgs = new Dictionary<string, string>
                {
                    ["black"] = "чёрным",
                    ["white"] = "белым",
                },
            },

            // Lasers
            // What was the number on the {1} hatch on {0}?
            // What was the number on the top-left hatch on Lasers?
            [Question.LasersHatches] = new TranslationInfo
            {
                QuestionText = "Какое число было на {1} люке в «{0}»?",
                ModuleName = "Лазерах",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхнем левом",
                    ["top-middle"] = "центральном верхнем",
                    ["top-right"] = "верхнем правом",
                },
            },

            // LED Encryption
            // What was the correct letter you pressed in the {1} stage of {0}?
            // What was the correct letter you pressed in the first stage of LED Encryption?
            [Question.LEDEncryptionPressedLetters] = new TranslationInfo
            {
                QuestionText = "Какая правильная буква была нажата на {1}-м этапе в «{0}»?",
                ModuleName = "Шифре светодиодов",
            },

            // LED Math
            // What color was {1} in {0}?
            // What color was LED A in LED Math?
            [Question.LEDMathLights] = new TranslationInfo
            {
                QuestionText = "Какого цвета был {1} в «{0}»?",
                ModuleName = "Светодиодной математике",
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
            [Question.LEDsOriginalColor] = new TranslationInfo
            {
                QuestionText = "Какой был начальный цвет изменённого светодиода в «{0}»?",
                ModuleName = "Ромбо-светодиодах",
            },

            // LEGOs
            // What were the dimensions of the {1} piece in {0}?
            // What were the dimensions of the red piece in LEGOs?
            [Question.LEGOsPieceDimensions] = new TranslationInfo
            {
                QuestionText = "Каких размеров была {1} деталь «{0}»?",
                ModuleName = "Лего",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "красная",
                    ["green"] = "зелёная",
                    ["blue"] = "синяя",
                    ["cyan"] = "голубая",
                    ["magenta"] = "розовая",
                    ["yellow"] = "жёлтая",
                },
            },

            // Letter Math
            // What was the letter on the {1} display in {0}?
            // What was the letter on the left display in Letter Math?
            [Question.LetterMathDisplay] = new TranslationInfo
            {
                QuestionText = "Какая буква была на {1} экране в «{0}»?",
                ModuleName = "Буквенной математике",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левом",
                    ["right"] = "правом",
                },
            },

            // Light Bulbs
            // What was the color of the {1} bulb in {0}?
            // What was the color of the left bulb in Light Bulbs?
            [Question.LightBulbsColors] = new TranslationInfo
            {
                QuestionText = "Какой был цвет {1} лампочки в «{0}»?",
                ModuleName = "Световых лампочках",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левой",
                    ["right"] = "правой",
                },
            },

            // Linq
            // What was the {1} function in {0}?
            // What was the first function in Linq?
            [Question.LinqFunction] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я функция в модуле «{0}»?",
                ModuleName = "Linq",
            },

            // Lion’s Share
            // Which year was displayed on {0}?
            // Which year was displayed on Lion’s Share?
            [Question.LionsShareYear] = new TranslationInfo
            {
                QuestionText = "Какой год был показан в «{0}»?",
                ModuleName = "Львиной доле",
            },
            // Which lion was present but removed in {0}?
            // Which lion was present but removed in Lion’s Share?
            [Question.LionsShareRemovedLions] = new TranslationInfo
            {
                QuestionText = "Какой лев изначально присутствовал, но потом был убран в «{0}»?",
                ModuleName = "Львиной доле",
            },

            // Logical Buttons
            // What was the color of the {1} button in the {2} stage of {0}?
            // What was the color of the top button in the first stage of Logical Buttons?
            [Question.LogicalButtonsColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета была {1} кнопка на {2}-м этапе в «{0}»?",
                ModuleName = "Логических кнопках",
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
            [Question.LogicalButtonsLabel] = new TranslationInfo
            {
                QuestionText = "Какая была надпись на {1} кнопке на {2}-м этапе в «{0}»?",
                ModuleName = "Логических кнопках",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхней",
                    ["bottom-left"] = "нижней левой",
                    ["bottom-right"] = "нижней правой",
                },
            },
            // What was the final operator in the {1} stage of {0}?
            // What was the final operator in the first stage of Logical Buttons?
            [Question.LogicalButtonsOperator] = new TranslationInfo
            {
                QuestionText = "Какой был конечный оператор на {1}-м этапе в «{0}»?",
                ModuleName = "Логических кнопках",
            },

            // Logic Gates
            // What was {1} in {0}?
            // What was gate A in Logic Gates?
            [Question.LogicGatesGates] = new TranslationInfo
            {
                QuestionText = "Каким был {1} в «{0}»?",
                ModuleName = "Логических элементах",
                FormatArgs = new Dictionary<string, string>
                {
                    ["gate A"] = "гейт A",
                    ["gate B"] = "гейт B",
                    ["gate C"] = "гейт C",
                    ["gate D"] = "гейт D",
                    ["gate E"] = "гейт E",
                    ["gate F"] = "гейт F",
                    ["gate G"] = "гейт G",
                    ["the duplicated gate"] = "дублированный гейт",
                },
            },

            // Lombax Cubes
            // What was the {1} letter on the button in {0}?
            // What was the first letter on the button in Lombax Cubes?
            [Question.LombaxCubesLetters] = new TranslationInfo
            {
                QuestionText = "What was the {1} letter on the button in «{0}»?",
            },

            // The London Underground
            // Where did the {1} journey on {0} {2}?
            // Where did the first journey on The London Underground depart from?
            [Question.LondonUndergroundStations] = new TranslationInfo
            {
                QuestionText = "Where did the {1} journey on «{0}» {2}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["depart from"] = "depart from",
                    ["arrive to"] = "arrive to",
                },
            },

            // Long Words
            // What was the word on the top display on {0}?
            // What was the word on the top display on Long Words?
            [Question.LongWordsWord] = new TranslationInfo
            {
                QuestionText = "Какое слово было на верхнем экране в «{0}»?",
                ModuleName = "Длинных словах",
            },

            // Mad Memory
            // What was on the display in the {1} stage of {0}?
            // What was on the display in the first stage of Mad Memory?
            [Question.MadMemoryDisplays] = new TranslationInfo
            {
                QuestionText = "Что было на экране на {1} этапе в «{0}»?",
                ModuleName = "Безумной памяти",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first"] = "1-м",
                    ["second"] = "2-м",
                    ["third"] = "3-м",
                    ["4th"] = "4-м",
                },
            },

            // Mahjong
            // Which tile was part of the {1} matched pair in {0}?
            // Which tile was part of the first matched pair in Mahjong?
            [Question.MahjongMatches] = new TranslationInfo
            {
                QuestionText = "Which tile was part of the {1} matched pair in «{0}»?",
            },
            // Which tile was shown in the bottom-left of {0}?
            // Which tile was shown in the bottom-left of Mahjong?
            [Question.MahjongCountingTile] = new TranslationInfo
            {
                QuestionText = "Which tile was shown in the bottom-left of «{0}»?",
            },

            // Mafia
            // Who was a player, but not the Godfather, in {0}?
            // Who was a player, but not the Godfather, in Mafia?
            [Question.MafiaPlayers] = new TranslationInfo
            {
                QuestionText = "Кто был игроком, но не являлся крёстным отцом в «{0}»?",
                ModuleName = "Мафии",
            },

            // Magenta Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Magenta Cipher?
            [Question.MagentaCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Шифре мадженты",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Main Page
            // Which color did the bubble not display in {0}?
            // Which color did the bubble not display in Main Page?
            [Question.MainPageBubbleColors] = new TranslationInfo
            {
                QuestionText = "Which color did the bubble not display in {0}?",
            },
            // Which main page did the {1} button's effect come from in {0}?
            // Which main page did the toons button's effect come from in Main Page?
            [Question.MainPageButtonEffectOrigin] = new TranslationInfo
            {
                QuestionText = "Which main page did the {1} button's effect come from in {0}?",
            },
            // Which of the following messages did the bubble {1} in {0}?
            // Which of the following messages did the bubble display in Main Page?
            [Question.MainPageBubbleMessages] = new TranslationInfo
            {
                QuestionText = "Which of the following messages did the bubble {1} in {0}?",
            },
            // Which main page did {1} come from in {0}?
            // Which main page did Homestar come from in Main Page?
            [Question.MainPageHomestarBackgroundOrigin] = new TranslationInfo
            {
                QuestionText = "Which main page did {1} come from in {0}?",
            },

            // M&Ms
            // What color was the text on the {1} button in {0}?
            // What color was the text on the first button in M&Ms?
            [Question.MandMsColors] = new TranslationInfo
            {
                QuestionText = "Какого цвета была надпись на {1}-й кнопке в модуле «{0}»?",
                ModuleName = "M&Ms",
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
            [Question.MandMsLabels] = new TranslationInfo
            {
                QuestionText = "Какая надпись была на {1}-й кнопке в модуле «{0}»?",
                ModuleName = "M&Ms",
            },

            // M&Ns
            // What color was the text on the {1} button in {0}?
            // What color was the text on the first button in M&Ns?
            [Question.MandNsColors] = new TranslationInfo
            {
                QuestionText = "Какого цвета была надпись на {1}-й кнопке в модуле «{0}»?",
                ModuleName = "M&Ns",
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
            [Question.MandNsLabel] = new TranslationInfo
            {
                QuestionText = "Какая надпись была на правильной кнопке в модуле «{0}»?",
                ModuleName = "M&Ns",
            },

            // Maritime Flags
            // What bearing was signalled in {0}?
            // What bearing was signalled in Maritime Flags?
            [Question.MaritimeFlagsBearing] = new TranslationInfo
            {
                QuestionText = "Какой пеленг был обозначен в «{0}»?",
                ModuleName = "Морских флагах",
            },
            // Which callsign was signalled in {0}?
            // Which callsign was signalled in Maritime Flags?
            [Question.MaritimeFlagsCallsign] = new TranslationInfo
            {
                QuestionText = "Какой позывной был обозначен в «{0}»?",
                ModuleName = "Морских флагах",
            },

            // Maroon Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Maroon Cipher?
            [Question.MaroonCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Бордовом шифре",
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
            [Question.MashematicsAnswer] = new TranslationInfo
            {
                QuestionText = "Какой был верный ответ в «{0}»?",
                ModuleName = "Нажиматике",
            },
            // What was the {1} number in the equation on {0}?
            // What was the first number in the equation on Mashematics?
            [Question.MashematicsCalculation] = new TranslationInfo
            {
                QuestionText = "Какое было {1}-е число в уравнении в «{0}»?",
                ModuleName = "Нажиматике",
            },

            // Master Tapes
            // Which song was played in {0}?
            // Which song was played in Master Tapes?
            [Question.MasterTapesPlayedSong] = new TranslationInfo
            {
                QuestionText = "Which song was played in «{0}»?",
            },

            // Match Refereeing
            // Which planet was present in the {1} stage of {0}?
            // Which planet was present in the first stage of Match Refereeing?
            [Question.MatchRefereeingPlanet] = new TranslationInfo
            {
                QuestionText = "Which planet was present in the {1} stage of «{0}»?",
            },

            // Math ’em
            // What was the color of this tile before the shuffle on {0}?
            // What was the color of this tile before the shuffle on Math ’em?
            [Question.MathEmColor] = new TranslationInfo
            {
                QuestionText = "What was the color of this tile before the shuffle on «{0}»?",
            },
            // What was the design on this tile before the shuffle on {0}?
            // What was the design on this tile before the shuffle on Math ’em?
            [Question.MathEmLabel] = new TranslationInfo
            {
                QuestionText = "What was the design on this tile before the shuffle on «{0}»?",
            },

            // The Matrix
            // Which word was part of the latest access code in {0}?
            // Which word was part of the latest access code in The Matrix?
            [Question.MatrixAccessCode] = new TranslationInfo
            {
                QuestionText = "Which word was part of the latest access code in «{0}»?",
            },
            // What was the glitched word in {0}?
            // What was the glitched word in The Matrix?
            [Question.MatrixGlitchWord] = new TranslationInfo
            {
                QuestionText = "What was the glitched word in «{0}»?",
            },

            // Maze
            // In which {1} was the starting position in {0}, counting from the {2}?
            // In which column was the starting position in Maze, counting from the left?
            [Question.MazeStartingPosition] = new TranslationInfo
            {
                QuestionText = "В {1} была начальная позиция в «{0}», считая {2}?",
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
            [Question.Maze3StartingFace] = new TranslationInfo
            {
                QuestionText = "Какой цвет был у начальной стороны в «{0}»?",
                ModuleName = "Лабиринте³",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Blue"] = "Синий",
                    ["Yellow"] = "Жёлтый",
                    ["Green"] = "Зелёный",
                    ["Magenta"] = "Розовый",
                    ["Orange"] = "Оранжевый",
                },
            },

            // Maze Identification
            // What was the seed of the maze in {0}?
            // What was the seed of the maze in Maze Identification?
            [Question.MazeIdentificationSeed] = new TranslationInfo
            {
                QuestionText = "What was the seed of the maze in «{0}»?",
            },
            // What was the function of button {1} in {0}?
            // What was the function of button 1 in Maze Identification?
            [Question.MazeIdentificationNum] = new TranslationInfo
            {
                QuestionText = "What was the function of button {1} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["1"] = "1",
                    ["2"] = "2",
                    ["3"] = "3",
                    ["4"] = "4",
                },
            },
            // Which button {1} in {0}?
            // Which button moved you forwards in Maze Identification?
            [Question.MazeIdentificationFunc] = new TranslationInfo
            {
                QuestionText = "Which button {1} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["moved you forwards"] = "moved you forwards",
                    ["turned you clockwise"] = "turned you clockwise",
                    ["moved you backwards"] = "moved you backwards",
                    ["turned you counter-clockwise"] = "turned you counter-clockwise",
                },
            },

            // Mazematics
            // Which was the {1} value in {0}?
            // Which was the initial value in Mazematics?
            [Question.MazematicsValue] = new TranslationInfo
            {
                QuestionText = "Какая была {1} величина в «{0}»?",
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
            [Question.MazeScramblerStart] = new TranslationInfo
            {
                QuestionText = "What was the starting position on «{0}»?",
            },
            // What was the goal on {0}?
            // What was the goal on Maze Scrambler?
            [Question.MazeScramblerGoal] = new TranslationInfo
            {
                QuestionText = "What was the goal on «{0}»?",
            },
            // Which of these positions was a maze marking on {0}?
            // Which of these positions was a maze marking on Maze Scrambler?
            [Question.MazeScramblerIndicators] = new TranslationInfo
            {
                QuestionText = "Which of these positions was a maze marking on «{0}»?",
            },

            // Mazeseeker
            // How many walls surrounded this cell in {0}?
            // How many walls surrounded this cell in Mazeseeker?
            [Question.MazeseekerCell] = new TranslationInfo
            {
                QuestionText = "How many walls surrounded this cell in «{0}»?",
            },
            // Where was the start in {0}?
            // Where was the start in Mazeseeker?
            [Question.MazeseekerStart] = new TranslationInfo
            {
                QuestionText = "Where was the start in «{0}»?",
            },
            // Where was the goal in {0}?
            // Where was the goal in Mazeseeker?
            [Question.MazeseekerGoal] = new TranslationInfo
            {
                QuestionText = "Where was the goal in «{0}»?",
            },

            // Mega Man 2
            // Who was the master shown in {0}?
            // Who was the master shown in Mega Man 2?
            [Question.MegaMan2SelectedMaster] = new TranslationInfo
            {
                QuestionText = "Who was the master shown in «{0}»?",
            },
            // Whose weapon was shown in {0}?
            // Whose weapon was shown in Mega Man 2?
            [Question.MegaMan2SelectedWeapon] = new TranslationInfo
            {
                QuestionText = "Whose weapon was shown in «{0}»?",
            },

            // Melody Sequencer
            // Which part was in slot #{1} at the start of {0}?
            // Which part was in slot #1 at the start of Melody Sequencer?
            [Question.MelodySequencerSlots] = new TranslationInfo
            {
                QuestionText = "Which part was in slot #{1} at the start of «{0}»?",
            },
            // Which slot contained part #{1} at the start of {0}?
            // Which slot contained part #1 at the start of Melody Sequencer?
            [Question.MelodySequencerParts] = new TranslationInfo
            {
                QuestionText = "Which slot contained part #{1} at the start of «{0}»?",
            },

            // Memorable Buttons
            // What was the {1} correct symbol pressed in {0}?
            // What was the first correct symbol pressed in Memorable Buttons?
            [Question.MemorableButtonsSymbols] = new TranslationInfo
            {
                QuestionText = "What was the {1} correct symbol pressed in «{0}»?",
            },

            // Memory
            // What was the displayed number in the {1} stage of {0}?
            // What was the displayed number in the first stage of Memory?
            [Question.MemoryDisplay] = new TranslationInfo
            {
                QuestionText = "Какая цифра была на экране на {1}-м этапе в «{0}»?",
                ModuleName = "Памяти",
            },
            // In what position was the button that you pressed in the {1} stage of {0}?
            // In what position was the button that you pressed in the first stage of Memory?
            [Question.MemoryPosition] = new TranslationInfo
            {
                QuestionText = "На какой позиции была кнопка, которую вы нажали на {1}-м этапе в «{0}»?",
                ModuleName = "Памяти",
            },
            // What was the label of the button that you pressed in the {1} stage of {0}?
            // What was the label of the button that you pressed in the first stage of Memory?
            [Question.MemoryLabel] = new TranslationInfo
            {
                QuestionText = "С каким значением была кнопка, которую вы нажали на {1}-м этапе в «{0}»?",
                ModuleName = "Памяти",
            },

            // Memory Wires
            // What was the digit displayed in the {1} stage of {0}?
            // What was the digit displayed in the first stage of Memory Wires?
            [Question.MemoryWiresDisplayedDigits] = new TranslationInfo
            {
                QuestionText = "What was the digit displayed in the {1} stage of «{0}»?",
            },
            // What was the colour of wire {1} in {0}?
            // What was the colour of wire 1 in Memory Wires?
            [Question.MemoryWiresWireColours] = new TranslationInfo
            {
                QuestionText = "What was the colour of wire {1} in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Yellow"] = "Yellow",
                    ["Blue"] = "Blue",
                    ["White"] = "White",
                    ["Black"] = "Black",
                },
            },

            // Metamorse
            // What was the extracted letter in {0}?
            // What was the extracted letter in Metamorse?
            [Question.MetamorseExtractedLetter] = new TranslationInfo
            {
                QuestionText = "Какая была извлечённая буква в «{0}»?",
                ModuleName = "Метаморзе",
            },

            // Metapuzzle
            // What was the final answer in {0}?
            // What was the final answer in Metapuzzle?
            [Question.MetapuzzleAnswer] = new TranslationInfo
            {
                QuestionText = "Какой был финальный ответ в «{0}»?",
                ModuleName = "Метапазле",
            },

            // Microcontroller
            // Which pin lit up {1} in {0}?
            // Which pin lit up first in Microcontroller?
            [Question.MicrocontrollerPinOrder] = new TranslationInfo
            {
                QuestionText = "Какой контакт загорелся {1}-м в «{0}»?",
                ModuleName = "Микроконтроллере",
            },

            // Minesweeper
            // What was the color of the starting cell in {0}?
            // What was the color of the starting cell in Minesweeper?
            [Question.MinesweeperStartingColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета была начальная клетка в «{0}»?",
                ModuleName = "Сапёре",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красная",
                    ["orange"] = "Оранжевая",
                    ["yellow"] = "Жёлтая",
                    ["green"] = "Зелёная",
                    ["blue"] = "Синяя",
                    ["purple"] = "Фиолетовая",
                    ["black"] = "Чёрная",
                },
            },

            // Mirror
            // What was the second word written by the original ghost in {0}?
            // What was the second word written by the original ghost in Mirror?
            [Question.MirrorWord] = new TranslationInfo
            {
                QuestionText = "Какое было второе слово, написанное призраком в «{0}»?",
                ModuleName = "Зеркале",
            },

            // Mister Softee
            // Where was the SpongeBob Bar on {0}?
            // Where was the SpongeBob Bar on Mister Softee?
            [Question.MisterSofteeSpongebobPosition] = new TranslationInfo
            {
                QuestionText = "Where was the SpongeBob Bar on «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-middle"] = "top-middle",
                    ["top-right"] = "top-right",
                    ["middle-left"] = "middle-left",
                    ["middle-middle"] = "middle-middle",
                    ["middle-right"] = "middle-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-middle"] = "bottom-middle",
                    ["bottom-right"] = "bottom-right",
                },
            },
            // Which treat was present on {0}?
            // Which treat was present on Mister Softee?
            [Question.MisterSofteeTreatsPresent] = new TranslationInfo
            {
                QuestionText = "Which treat was present on «{0}»?",
            },

            // Modern Cipher
            // What was the decrypted word of the {1} stage in {0}?
            // What was the decrypted word of the first stage in Modern Cipher?
            [Question.ModernCipherWord] = new TranslationInfo
            {
                QuestionText = "Какое слово было расшифровано на {1}-м этапе в «{0}»?",
                ModuleName = "Современном шифре",
            },

            // Module Maze
            // Which of the following was the starting icon for {0}?
            // Which of the following was the starting icon for Module Maze?
            [Question.ModuleMazeStartingIcon] = new TranslationInfo
            {
                QuestionText = "Какая была начальная иконка модуля в «{0}»?",
                ModuleName = "Модульном лабиринте",
            },

            // Module Movements
            // What was the {1} module shown in {0}?
            // What was the first module shown in Module Movements?
            [Question.ModuleMovementsDisplay] = new TranslationInfo
            {
                QuestionText = "What was the {1} module shown in «{0}»?",
            },

            // Monsplode, Fight!
            // Which creature was displayed in {0}?
            // Which creature was displayed in Monsplode, Fight!?
            [Question.MonsplodeFightCreature] = new TranslationInfo
            {
                QuestionText = "Какое существо было показано на экране в модуле «{0}»?",
                ModuleName = "Монсплоды, в атаку!",
            },
            // Which one of these moves {1} selectable in {0}?
            // Which one of these moves was selectable in Monsplode, Fight!?
            [Question.MonsplodeFightMove] = new TranslationInfo
            {
                QuestionText = "Какой один из этих приёмов {1} доступен в модуле «{0}»?",
                ModuleName = "Монсплоды, в атаку!",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "был",
                    ["was not"] = "не был",
                },
            },

            // Monsplode Trading Cards
            // What was the {1} before the last action in {0}?
            // What was the first card in your hand before the last action in Monsplode Trading Cards?
            [Question.MonsplodeTradingCardsCards] = new TranslationInfo
            {
                QuestionText = "Какая была {1} перед последним действием в «{0}»?",
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
            [Question.MonsplodeTradingCardsPrintVersions] = new TranslationInfo
            {
                QuestionText = "Какая была печатная версия {1} перед последним действием в «{0}»?",
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
            [Question.MoonLitUnlit] = new TranslationInfo
            {
                QuestionText = "What was the {1} set in clockwise order in «{0}»?",
                FormatArgs = new Dictionary<string, string>
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
                Answers = new Dictionary<string, string>
                {
                    ["south"] = "south",
                    ["south-west"] = "south-west",
                    ["west"] = "west",
                    ["north-west"] = "north-west",
                    ["north"] = "north",
                    ["north-east"] = "north-east",
                    ["east"] = "east",
                    ["south-east"] = "south-east",
                },
            },

            // More Code
            // What was the flashing word in {0}?
            // What was the flashing word in More Code?
            [Question.MoreCodeWord] = new TranslationInfo
            {
                QuestionText = "What was the flashing word in «{0}»?",
            },

            // Morse-A-Maze
            // What was the starting location in {0}?
            // What was the starting location in Morse-A-Maze?
            [Question.MorseAMazeStartingCoordinate] = new TranslationInfo
            {
                QuestionText = "Какая была начальная позиция в «{0}»?",
                ModuleName = "Лабиринте Морзе",
            },
            // What was the ending location in {0}?
            // What was the ending location in Morse-A-Maze?
            [Question.MorseAMazeEndingCoordinate] = new TranslationInfo
            {
                QuestionText = "Какая была целевая (конечная) позиция в «{0}»?",
                ModuleName = "Лабиринте Морзе",
            },
            // What was the word shown as Morse code in {0}?
            // What was the word shown as Morse code in Morse-A-Maze?
            [Question.MorseAMazeMorseCodeWord] = new TranslationInfo
            {
                QuestionText = "Какое кодовое слово было передано через Морзе в «{0}»?",
                ModuleName = "Лабиринте Морзе",
            },

            // Morse Buttons
            // What was the character flashed by the {1} button in {0}?
            // What was the character flashed by the first button in Morse Buttons?
            [Question.MorseButtonsButtonLabel] = new TranslationInfo
            {
                QuestionText = "Какой символ передавался {1}-й кнопкой в «{0}»?",
                ModuleName = "Кнопках Морзе",
            },
            // What was the color flashed by the {1} button in {0}?
            // What was the color flashed by the first button in Morse Buttons?
            [Question.MorseButtonsButtonColor] = new TranslationInfo
            {
                QuestionText = "Каким цветом мигала {1}-я кнопка в «{0}»?",
                ModuleName = "Кнопках Морзе",
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
            [Question.MorsematicsReceivedLetters] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я полученная буква в «{0}»?",
                ModuleName = "Морзематике",
            },

            // Morse War
            // What were the LEDs in the {1} row in {0} (1 = on, 0 = off)?
            // What were the LEDs in the bottom row in Morse War (1 = on, 0 = off)?
            [Question.MorseWarLeds] = new TranslationInfo
            {
                QuestionText = "Какими были светодиоды в {1} ряду в «{0}» (1 = включен, 0 = выключен)?",
                ModuleName = "Войне Морзе",
                FormatArgs = new Dictionary<string, string>
                {
                    ["bottom"] = "нижнем",
                    ["middle"] = "центральном",
                    ["top"] = "верхнем",
                },
            },
            // What code was transmitted in {0}?
            // What code was transmitted in Morse War?
            [Question.MorseWarCode] = new TranslationInfo
            {
                QuestionText = "Какой код был передан в «{0}»?",
                ModuleName = "Войне Морзе",
            },

            // Mouse in the Maze
            // What color was the torus in {0}?
            // What color was the torus in Mouse in the Maze?
            [Question.MouseInTheMazeTorus] = new TranslationInfo
            {
                QuestionText = "Какого цвета было кольцо в модуле «{0}»?",
                ModuleName = "Мышь в лабиринте",
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
            [Question.MouseInTheMazeSphere] = new TranslationInfo
            {
                QuestionText = "Какого цвета была правильная целевая сфера в модуле «{0}»?",
                ModuleName = "Мышь в лабиринте",
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
            [Question.MSeqObtained] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я полученная цифра в модуле «{0}»?",
            },
            // What was the final number from the iteration process in {0}?
            // What was the final number from the iteration process in M-Seq?
            [Question.MSeqSubmitted] = new TranslationInfo
            {
                QuestionText = "Какое было финальное число итерационного процесса в модуле «{0}»?",
            },

            // Multicolored Switches
            // What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?
            // What color was the first LED on the top row when the tiny LED was lit in Multicolored Switches?
            [Question.MulticoloredSwitchesLedColor] = new TranslationInfo
            {
                QuestionText = "What color was the {1} LED on the {2} row when the tiny LED was {3} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["lit"] = "lit",
                    ["bottom"] = "bottom",
                    ["unlit"] = "unlit",
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

            // Murder
            // Where was the body found in {0}?
            // Where was the body found in Murder?
            [Question.MurderBodyFound] = new TranslationInfo
            {
                QuestionText = "Где было найдено тело в «{0}»?",
                ModuleName = "Убийстве",
                Answers = new Dictionary<string, string>
                {
                    ["Dining Room"] = "Столовая",
                    ["Study"] = "Кабинет",
                    ["Kitchen"] = "Кухня",
                    ["Lounge"] = "Гостиная",
                    ["Billiard Room"] = "Бильярдная",
                    ["Conservatory"] = "Зимний сад",
                    ["Ballroom"] = "Танцевальный зал",
                    ["Hall"] = "Холл",
                    ["Library"] = "Библиотека",
                },
            },
            // Which of these was {1} in {0}?
            // Which of these was a suspect but not the murderer in Murder?
            [Question.MurderSuspect] = new TranslationInfo
            {
                QuestionText = "Кто {1} в «{0}»?",
                ModuleName = "Убийстве",
                FormatArgs = new Dictionary<string, string>
                {
                    ["a suspect but not the murderer"] = "не являлся убийцей, но был среди подозреваемых",
                    ["not a suspect"] = "не был подозреваемым",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Miss Scarlett"] = "Мисс Скарлетт",
                    ["Professor Plum"] = "Профессор Плам",
                    ["Mrs Peacock"] = "Миссис Пикок",
                    ["Reverend Green"] = "Преподобный Грин",
                    ["Colonel Mustard"] = "Полковник Мастард",
                    ["Mrs White"] = "Миссис Уайт",
                },
            },
            // Which of these was {1} in {0}?
            // Which of these was a potential weapon but not the murder weapon in Murder?
            [Question.MurderWeapon] = new TranslationInfo
            {
                QuestionText = "{1} «{0}»?",
                ModuleName = "Убийства",
                FormatArgs = new Dictionary<string, string>
                {
                    ["a potential weapon but not the murder weapon"] = "Какое орудие было найдено, но не являлось орудием",
                    ["not a potential weapon"] = "Какого орудия не было среди найденных возможных орудий",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Candlestick"] = "Подсвечник",
                    ["Dagger"] = "Нож",
                    ["Lead Pipe"] = "Свинцовая труба",
                    ["Revolver"] = "Револьвер",
                    ["Rope"] = "Верёвка",
                    ["Spanner"] = "Гаечный ключ",
                },
            },

            // Mystery Module
            // Which module was the first requested to be solved by {0}?
            // Which module was the first requested to be solved by Mystery Module?
            [Question.MysteryModuleFirstKey] = new TranslationInfo
            {
                QuestionText = "Which module was the first requested to be solved by «{0}»?",
            },
            // Which module was hidden by {0}?
            // Which module was hidden by Mystery Module?
            [Question.MysteryModuleHiddenModule] = new TranslationInfo
            {
                QuestionText = "Which module was hidden by «{0}»?",
            },

            // Mystic Square
            // Where was the skull in {0}?
            // Where was the skull in Mystic Square?
            [Question.MysticSquareSkull] = new TranslationInfo
            {
                QuestionText = "Где находился череп в «{0}»?",
                ModuleName = "Загадочном квадрате",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "Левый верх",
                    ["top middle"] = "Верхний центр",
                    ["top right"] = "Правый верх",
                    ["middle left"] = "Левый центр",
                    ["center"] = "Центр",
                    ["middle right"] = "Правый центр",
                    ["bottom left"] = "Левый низ",
                    ["bottom middle"] = "Нижний центр",
                    ["bottom right"] = "Правый низ",
                },
            },

            // N&Ms
            // What was the label of the correct button in {0}?
            // What was the label of the correct button in N&Ms?
            [Question.NandMsAnswer] = new TranslationInfo
            {
                QuestionText = "Какая надпись была на правильной кнопке в модуле «{0}»?",
                ModuleName = "N&Ms",
            },

            // Name Codes
            // What was the {1} index in {0}?
            // What was the left index in Name Codes?
            [Question.NameCodesIndices] = new TranslationInfo
            {
                QuestionText = "What was the {1} index in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "left",
                    ["right"] = "right",
                },
            },

            // Navigation Determination
            // What was the color of the maze in {0}?
            // What was the color of the maze in Navigation Determination?
            [Question.NavigationDeterminationColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета был лабиринт в «{0}»?",
                ModuleName = "Определении навигации",
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
            [Question.NavigationDeterminationLabel] = new TranslationInfo
            {
                QuestionText = "Какой буквой был обозначен лабиринт в «{0}»?",
                ModuleName = "Определении навигации",
            },

            // Navinums
            // What was the initial middle digit in {0}?
            // What was the initial middle digit in Navinums?
            [Question.NavinumsMiddleDigit] = new TranslationInfo
            {
                QuestionText = "What was the initial middle digit in «{0}»?",
            },
            // What was the {1} directional button pressed in {0}?
            // What was the first directional button pressed in Navinums?
            [Question.NavinumsDirectionalButtons] = new TranslationInfo
            {
                QuestionText = "What was the {1} directional button pressed in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["up"] = "up",
                    ["left"] = "left",
                    ["right"] = "right",
                    ["down"] = "down",
                },
            },

            // The Navy Button
            // Which Greek letter appeared on {0} (case-sensitive)?
            // Which Greek letter appeared on The Navy Button (case-sensitive)?
            [Question.NavyButtonGreekLetters] = new TranslationInfo
            {
                QuestionText = "Какая греческая буква появилась в «{0}» (с учётом регистра)?",
                ModuleName = "Тёмно-синей кнопке",
            },
            // What was the {1} of the given in {0} (0-indexed)?
            // What was the column of the given in The Navy Button (0-indexed)?
            [Question.NavyButtonGiven] = new TranslationInfo
            {
                QuestionText = "{1} в «{0}» (с индексом 0)?",
                ModuleName = "Тёмно-синей кнопке",
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
            [Question.NecronomiconChapters] = new TranslationInfo
            {
                QuestionText = "Какой был номер главы {1}-й страницы «{0}»?",
                ModuleName = "Некрономикона",
            },

            // Negativity
            // In base 10, what was the value submitted in {0}?
            // In base 10, what was the value submitted in Negativity?
            [Question.NegativitySubmittedValue] = new TranslationInfo
            {
                QuestionText = "In base 10, what was the value submitted in «{0}»?",
            },
            // Excluding 0s, what was the submitted balanced ternary in {0}?
            // Excluding 0s, what was the submitted balanced ternary in Negativity?
            [Question.NegativitySubmittedTernary] = new TranslationInfo
            {
                QuestionText = "Excluding 0s, what was the submitted balanced ternary in «{0}»?",
            },

            // Neutralization
            // What was the acid’s color in {0}?
            // What was the acid’s color in Neutralization?
            [Question.NeutralizationColor] = new TranslationInfo
            {
                QuestionText = "Какой был цвет у кислоты в «{0}»?",
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
            [Question.NeutralizationVolume] = new TranslationInfo
            {
                QuestionText = "Какой был объём кислоты в «{0}»?",
                ModuleName = "Нейтрализации",
            },

            // ❖
            // Which button flashed in the {1} stage in {0}?
            // Which button flashed in the first stage in ❖?
            [Question.NonverbalSimonFlashes] = new TranslationInfo
            {
                QuestionText = "Какой кнопка горела на {1}-м этапе в модуле «{0}»?",
            },

            // Not Colored Squares
            // What was the position of the square you initially pressed in {0}?
            // What was the position of the square you initially pressed in Not Colored Squares?
            [Question.NotColoredSquaresInitialPosition] = new TranslationInfo
            {
                QuestionText = "Какая была позиция квадрата, который вы изначально нажали в «{0}»?",
                ModuleName = "НЕ-цветных квадратах",
            },

            // Not Colored Switches
            // What was the encrypted word in {0}?
            // What was the encrypted word in Not Colored Switches?
            [Question.NotColoredSwitchesWord] = new TranslationInfo
            {
                QuestionText = "Какое было зашифрованное слово в «{0}»?",
                ModuleName = "НЕ-цветных переключателях",
            },

            // Not Connection Check
            // What symbol flashed on the {1} button in {0}?
            // What symbol flashed on the top left button in Not Connection Check?
            [Question.NotConnectionCheckFlashes] = new TranslationInfo
            {
                QuestionText = "Какой символ мигал на {1} кнопке в «{0}»?",
                ModuleName = "НЕ-проверке соединения",
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
            [Question.NotConnectionCheckValues] = new TranslationInfo
            {
                QuestionText = "Какое было значение {1} кнопки в «{0}»?",
                ModuleName = "НЕ-проверке соединения",
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
            [Question.NotCoordinatesSquareCoords] = new TranslationInfo
            {
                QuestionText = "Какая координата была частью квадрата в «{0}»?",
                ModuleName = "НЕ-координатах",
            },

            // Not Keypad
            // What color flashed {1} in the final sequence in {0}?
            // What color flashed first in the final sequence in Not Keypad?
            [Question.NotKeypadColor] = new TranslationInfo
            {
                QuestionText = "Какой цвет горел {1}-м в исходной последовательности в «{0}»?",
                ModuleName = "НЕ-клавиатуре",
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
            [Question.NotKeypadSymbol] = new TranslationInfo
            {
                QuestionText = "Какой символ был на кнопке, которая горела {1}-й в исходной последовательности в «{0}»?",
                ModuleName = "НЕ-клавиатуре",
            },

            // Not Maze
            // What was the starting distance in {0}?
            // What was the starting distance in Not Maze?
            [Question.NotMazeStartingDistance] = new TranslationInfo
            {
                QuestionText = "Какая была начальная дистанция в «{0}»?",
                ModuleName = "НЕ-лабиринте",
            },

            // Not Morse Code
            // What was the {1} correct word you submitted in {0}?
            // What was the first correct word you submitted in Not Morse Code?
            [Question.NotMorseCodeWord] = new TranslationInfo
            {
                QuestionText = "Какое было {1}-е верное слово, которое вы отправили в «{0}»?",
                ModuleName = "НЕ-азбуке Морзе",
            },

            // Not Morsematics
            // What was the transmitted word on {0}?
            // What was the transmitted word on Not Morsematics?
            [Question.NotMorsematicsWord] = new TranslationInfo
            {
                QuestionText = "Какое слово было передано в «{0}»?",
                ModuleName = "НЕ-Морзематике",
            },

            // Not Murder
            // What room was {1} in initially on {0}?
            // What room was Miss Scarlett in initially on Not Murder?
            [Question.NotMurderRoom] = new TranslationInfo
            {
                QuestionText = "В какой комнате изначально находился(-ась) {1} в «{0}»?",
                ModuleName = "НЕ-убийстве",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Miss Scarlett"] = "Мисс Скарлетт",
                    ["Colonel Mustard"] = "Полковник Мастард",
                    ["Reverend Green"] = "Преподобный Грин",
                    ["Mrs Peacock"] = "Миссис Пикок",
                    ["Professor Plum"] = "Профессор Плам",
                    ["Mrs White"] = "Миссис Уайт",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Ballroom"] = "Танцевальный зал",
                    ["Billiard Room"] = "Бильярдная",
                    ["Conservatory"] = "Зимний сад",
                    ["Dining Room"] = "Столовая",
                    ["Hall"] = "Холл",
                    ["Kitchen"] = "Кухня",
                    ["Library"] = "Библиотека",
                    ["Lounge"] = "Гостиная",
                    ["Study"] = "Кабинет",
                },
            },
            // What weapon did {1} possess initially on {0}?
            // What weapon did Miss Scarlett possess initially on Not Murder?
            [Question.NotMurderWeapon] = new TranslationInfo
            {
                QuestionText = "Каким орудием изначально обладал(-а) {1} в «{0}»?",
                ModuleName = "НЕ-убийстве",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Miss Scarlett"] = "Мисс Скарлетт",
                    ["Colonel Mustard"] = "Полковник Мастард",
                    ["Reverend Green"] = "Преподобный Грин",
                    ["Mrs Peacock"] = "Миссис Пикок",
                    ["Professor Plum"] = "Профессор Плам",
                    ["Mrs White"] = "Миссис Уайт",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Candlestick"] = "Подсвечник",
                    ["Dagger"] = "Нож",
                    ["Lead Pipe"] = "Свинцовая труба",
                    ["Revolver"] = "Револьвер",
                    ["Rope"] = "Верёвка",
                    ["Spanner"] = "Гаечный ключ",
                },
            },

            // Not Number Pad
            // Which of these numbers {1} at the {2} stage of {0}?
            // Which of these numbers flashed at the first stage of Not Number Pad?
            [Question.NotNumberPadFlashes] = new TranslationInfo
            {
                QuestionText = "Какая их этих цифр {1} на {2}-м этапе в «{0}»?",
                ModuleName = "НЕ-цифровой клавиатуре",
                FormatArgs = new Dictionary<string, string>
                {
                    ["flashed"] = "мигала",
                    ["did not flash"] = "не мигала",
                },
            },

            // Not Perspective Pegs
            // What was the position of the {1} flashing peg on {0}?
            // What was the position of the first flashing peg on Not Perspective Pegs?
            [Question.NotPerspectivePegsPosition] = new TranslationInfo
            {
                QuestionText = "В какой позиции находился {1}-й мигающий колышек в «{0}»?",
                ModuleName = "НЕ-взгляде на колышках",
            },
            // From what perspective did the {1} peg flash on {0}?
            // From what perspective did the first peg flash on Not Perspective Pegs?
            [Question.NotPerspectivePegsPerspective] = new TranslationInfo
            {
                QuestionText = "С какого ракурса мигнул {1}-й колышек в «{0}»?",
                ModuleName = "НЕ-взгляде на колышках",
            },
            // What was the color of the {1} flashing peg on {0}?
            // What was the color of the first flashing peg on Not Perspective Pegs?
            [Question.NotPerspectivePegsColor] = new TranslationInfo
            {
                QuestionText = "Какой был цвет {1}-го мигающего колышка в «{0}»?",
                ModuleName = "НЕ-взгляде на колышках",
            },

            // Not Piano Keys
            // What was the first displayed symbol on {0}?
            // What was the first displayed symbol on Not Piano Keys?
            [Question.NotPianoKeysFirstSymbol] = new TranslationInfo
            {
                QuestionText = "Какой был первый изображённый символ в «{0}»?",
                ModuleName = "НЕ-пианино",
            },
            // What was the second displayed symbol on {0}?
            // What was the second displayed symbol on Not Piano Keys?
            [Question.NotPianoKeysSecondSymbol] = new TranslationInfo
            {
                QuestionText = "Какой был второй изображённый символ в «{0}»?",
                ModuleName = "НЕ-пианино",
            },
            // What was the third displayed symbol on {0}?
            // What was the third displayed symbol on Not Piano Keys?
            [Question.NotPianoKeysThirdSymbol] = new TranslationInfo
            {
                QuestionText = "Какой был третий изображённый символ в «{0}»?",
                ModuleName = "НЕ-пианино",
            },

            // Not Simaze
            // Which maze was used in {0}?
            // Which maze was used in Not Simaze?
            [Question.NotSimazeMaze] = new TranslationInfo
            {
                QuestionText = "Какой лабиринт был использован в «{0}»?",
                ModuleName = "НЕ-Саймоне",
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
            [Question.NotSimazeStart] = new TranslationInfo
            {
                QuestionText = "Какая была начальная позиция в «{0}»?",
                ModuleName = "НЕ-Саймоне",
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
            [Question.NotSimazeGoal] = new TranslationInfo
            {
                QuestionText = "Какая была целевая (конечная) позиция в «{0}»?",
                ModuleName = "НЕ-Саймоне",
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
            [Question.NotTextFieldInitialPresses] = new TranslationInfo
            {
                QuestionText = "Какая буква была нажата на первом этапе на «{0}»?",
                ModuleName = "НЕ-поле из букв",
            },
            // Which letter appeared 9 times at the start of {0}?
            // Which letter appeared 9 times at the start of Not Text Field?
            [Question.NotTextFieldBackgroundLetter] = new TranslationInfo
            {
                QuestionText = "Какая буква появилась 9 раз в начале на «{0}»?",
                ModuleName = "НЕ-поле из букв",
            },

            // Not The Bulb
            // What word flashed on {0}?
            // What word flashed on Not The Bulb?
            [Question.NotTheBulbWord] = new TranslationInfo
            {
                QuestionText = "Какое слово мигало «{0}»?",
                ModuleName = "НЕ-лампочкой",
            },
            // What color was the bulb on {0}?
            // What color was the bulb on Not The Bulb?
            [Question.NotTheBulbColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета была «{0}»?",
                ModuleName = "НЕ-лампочка",
            },
            // What was the material of the screw cap on {0}?
            // What was the material of the screw cap on Not The Bulb?
            [Question.NotTheBulbScrewCap] = new TranslationInfo
            {
                QuestionText = "Из какого материала был сделан цоколь «{0}»?",
                ModuleName = "НЕ-лампочки",
            },

            // Not the Button
            // What colors did the light glow in {0}?
            // What colors did the light glow in Not the Button?
            [Question.NotTheButtonLightColor] = new TranslationInfo
            {
                QuestionText = "Какими цветами горела цветная полоска в модуле «{0}»?",
                ModuleName = "НЕ-кнопка",
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

            // Not the Screw
            // What was the initial position in {0}?
            // What was the initial position in Not the Screw?
            [Question.NotTheScrewInitialPosition] = new TranslationInfo
            {
                QuestionText = "Какая была начальная позиция в «{0}»?",
                ModuleName = "НЕ-винте",
            },

            // Not Who’s on First
            // In which position was the button you pressed in the {1} stage on {0}?
            // In which position was the button you pressed in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstPressedPosition] = new TranslationInfo
            {
                QuestionText = "На какой позиции была кнопка, которую вы нажали на {1}-м этапе в модуле «{0}»?",
                ModuleName = "НЕ-Меня зовут Авас, а Вас",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "Левый верх",
                    ["top right"] = "Правый верх",
                    ["middle left"] = "Левый центр",
                    ["middle right"] = "Правый центр",
                    ["bottom left"] = "Левый низ",
                    ["bottom right"] = "Правый низ",
                },
            },
            // What was the label on the button you pressed in the {1} stage on {0}?
            // What was the label on the button you pressed in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstPressedLabel] = new TranslationInfo
            {
                QuestionText = "Что было написано на кнопке, которую вы нажали на {1}-м этапе в модуле «{0}»?",
                ModuleName = "НЕ-Меня зовут Авас, а Вас",
            },
            // In which position was the reference button in the {1} stage on {0}?
            // In which position was the reference button in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstReferencePosition] = new TranslationInfo
            {
                QuestionText = "На какой позиции была кнопка-ссылка на {1}-м этапе в модуле «{0}»?",
                ModuleName = "НЕ-Меня зовут Авас, а Вас",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "Левый верх",
                    ["top right"] = "Правый верх",
                    ["middle left"] = "Левый центр",
                    ["middle right"] = "Правый центр",
                    ["bottom left"] = "Левый низ",
                    ["bottom right"] = "Правый низ",
                },
            },
            // What was the label on the reference button in the {1} stage on {0}?
            // What was the label on the reference button in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstReferenceLabel] = new TranslationInfo
            {
                QuestionText = "Что было написано на кнопке-ссылке на {1}-м этапе в модуле «{0}»?",
                ModuleName = "НЕ-Меня зовут Авас, а Вас",
            },
            // What was the calculated number in the second stage on {0}?
            // What was the calculated number in the second stage on Not Who’s on First?
            [Question.NotWhosOnFirstSum] = new TranslationInfo
            {
                QuestionText = "Какое было рассчитанное число на втором этапе в модуле «{0}»?",
                ModuleName = "НЕ-Меня зовут Авас, а Вас",
            },

            // Not Word Search
            // Which of these consonants was missing in {0}?
            // Which of these consonants was missing in Not Word Search?
            [Question.NotWordSearchMissing] = new TranslationInfo
            {
                QuestionText = "Какая из этих согласных букв отсутствовала в «{0}»?",
                ModuleName = "НЕ-поиске слова",
            },
            // What was the first correctly pressed letter in {0}?
            // What was the first correctly pressed letter in Not Word Search?
            [Question.NotWordSearchFirstPress] = new TranslationInfo
            {
                QuestionText = "Какая была первая правильно нажатая буква в «{0}»?",
                ModuleName = "НЕ-поиске слова",
            },

            // Not X01
            // Which sector value {1} present on {0}?
            // Which sector value was present on Not X01?
            [Question.NotX01SectorValues] = new TranslationInfo
            {
                QuestionText = "Какое значение сектора {1} на модуле «{0}»?",
                ModuleName = "НЕ-X01",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "присутствовало",
                    ["was not"] = "отсутствовало",
                },
            },

            // Not X-Ray
            // What table were we in in {0} (numbered 1–8 in reading order in the manual)?
            // What table were we in in Not X-Ray (numbered 1–8 in reading order in the manual)?
            [Question.NotXRayTable] = new TranslationInfo
            {
                QuestionText = "В какой таблице вы находились в «{0}» (пронумерованных от 1 до 8 в порядке чтения в руководстве)?",
                ModuleName = "НЕ-рентгене",
            },
            // What direction was button {1} in {0}?
            // What direction was button 1 in Not X-Ray?
            [Question.NotXRayDirections] = new TranslationInfo
            {
                QuestionText = "За какое направление отвечала кнопка “{1}” в «{0}»?",
                ModuleName = "НЕ-рентгене",
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
            [Question.NotXRayButtons] = new TranslationInfo
            {
                QuestionText = "Какая кнопка отвечала за направление “{1}” в «{0}»?",
                ModuleName = "НЕ-рентгене",
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
            [Question.NotXRayScannerColor] = new TranslationInfo
            {
                QuestionText = "Какой был цвет сканера в «{0}»?",
                ModuleName = "НЕ-рентгене",
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
            [Question.NumberedButtonsButtons] = new TranslationInfo
            {
                QuestionText = "Какое было правильно нажатое число в «{0}»?",
                ModuleName = "Пронумерованных кнопках",
            },

            // Numbers
            // What two-digit number was given in {0}?
            // What two-digit number was given in Numbers?
            [Question.NumbersTwoDigit] = new TranslationInfo
            {
                QuestionText = "What two-digit number was given in «{0}»?",
            },

            // Numpath
            // What was the color of the number on {0}?
            // What was the color of the number on Numpath?
            [Question.NumpathColor] = new TranslationInfo
            {
                QuestionText = "What was the color of the number on «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Yellow",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Purple"] = "Purple",
                },
            },
            // What was the number displayed on {0}?
            // What was the number displayed on Numpath?
            [Question.NumpathDigit] = new TranslationInfo
            {
                QuestionText = "What was the number displayed on «{0}»?",
            },

            // Object Shows
            // Which of these was a contestant on {0}?
            // Which of these was a contestant on Object Shows?
            [Question.ObjectShowsContestants] = new TranslationInfo
            {
                QuestionText = "Кто среди этих участников присутствовал в «{0}», но не был финальным победителем?",
                ModuleName = "Обджект-шоу",
            },

            // The Octadecayotton
            // What was the starting sphere in {0}?
            // What was the starting sphere in The Octadecayotton?
            [Question.OctadecayottonSphere] = new TranslationInfo
            {
                QuestionText = "Какая была начальная точка (сфера) в «{0}»?",
                ModuleName = "Октадекаиоттоне",
            },
            // What was one of the subrotations in the {1} rotation in {0}?
            // What was one of the subrotations in the first rotation in The Octadecayotton?
            [Question.OctadecayottonRotations] = new TranslationInfo
            {
                QuestionText = "Каким было одно из промежуточных вращений в {1}-м вращении в «{0}»?",
                ModuleName = "Октадекаиоттоне",
            },

            // Odd One Out
            // What was the button you pressed in the {1} stage of {0}?
            // What was the button you pressed in the first stage of Odd One Out?
            [Question.OddOneOutButton] = new TranslationInfo
            {
                QuestionText = "What was the button you pressed in the {1} stage of «{0}»?",
            },

            // Old AI
            // What was the {1} of the numbers shown in {0}?
            // What was the group of the numbers shown in Old AI?
            [Question.OldAIGroup] = new TranslationInfo
            {
                QuestionText = "What was the {1} of the numbers shown in «{0}»?",
            },

            // Old Fogey
            // What was the initial color of the status light in {0}?
            // What was the initial color of the status light in Old Fogey?
            [Question.OldFogeyStartingColor] = new TranslationInfo
            {
                QuestionText = "What was the initial color of the status light in «{0}»?",
            },

            // Only Connect
            // Which Egyptian hieroglyph was in the {1} in {0}?
            // Which Egyptian hieroglyph was in the top left in Only Connect?
            [Question.OnlyConnectHieroglyphs] = new TranslationInfo
            {
                QuestionText = "Какой египетский иероглиф был {1} в модуле «{0}»?",
                ModuleName = "Лишь Соедините!",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top left"] = "слева сверху",
                    ["top middle"] = "сверху по центру",
                    ["top right"] = "справа сверху",
                    ["bottom left"] = "слева снизу",
                    ["bottom middle"] = "снизу по центру",
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
            [Question.OrangeArrowsSequences] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я стрелка на экране на {2}-м этапе в «{0}»?",
                ModuleName = "Оранжевых стрелках",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Верхняя",
                    ["Right"] = "Правая",
                    ["Down"] = "Нижняя",
                    ["Left"] = "Левая",
                },
            },

            // Orange Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Orange Cipher?
            [Question.OrangeCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Оранжевом шифре",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "верхнем",
                    ["middle"] = "центральном",
                    ["bottom"] = "нижнем",
                },
            },

            // Ordered Keys
            // What color was the {2} key in the {1} stage of {0}?
            // What color was the first key in the first stage of Ordered Keys?
            [Question.OrderedKeysColors] = new TranslationInfo
            {
                QuestionText = "Какого цвета была {2}-я клавиша на {1}-м этапе в «{0}»?",
                ModuleName = "Упорядоченных клавишах",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красная",
                    ["Blue"] = "Синяя",
                    ["Green"] = "Зелёная",
                    ["Yellow"] = "Жёлтая",
                    ["Cyan"] = "Голубая",
                    ["Magenta"] = "Розовая",
                },
            },
            // What was the label on the {2} key in the {1} stage of {0}?
            // What was the label on the first key in the first stage of Ordered Keys?
            [Question.OrderedKeysLabels] = new TranslationInfo
            {
                QuestionText = "Какая была надпись на {2}-й клавише на {1}-м этапе в «{0}»?",
                ModuleName = "Упорядоченных клавишах",
            },
            // What color was the label of the {2} key in the {1} stage of {0}?
            // What color was the label of the first key in the first stage of Ordered Keys?
            [Question.OrderedKeysLabelColors] = new TranslationInfo
            {
                QuestionText = "Какого цвета была надпись на {2}-й клавише на {1}-м этапе в «{0}»?",
                ModuleName = "Упорядоченных клавишах",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красная",
                    ["Blue"] = "Синяя",
                    ["Green"] = "Зелёная",
                    ["Yellow"] = "Жёлтая",
                    ["Cyan"] = "Голубая",
                    ["Magenta"] = "Розовая",
                },
            },

            // Order Picking
            // What was the order ID in the {1} order of {0}?
            // What was the order ID in the first order of Order Picking?
            [Question.OrderPickingOrder] = new TranslationInfo
            {
                QuestionText = "What was the order ID in the {1} order of «{0}»?",
            },
            // What was the product ID in the {1} order of {0}?
            // What was the product ID in the first order of Order Picking?
            [Question.OrderPickingProduct] = new TranslationInfo
            {
                QuestionText = "What was the product ID in the {1} order of «{0}»?",
            },
            // What was the pallet in the {1} order of {0}?
            // What was the pallet in the first order of Order Picking?
            [Question.OrderPickingPallet] = new TranslationInfo
            {
                QuestionText = "What was the pallet in the {1} order of «{0}»?",
            },

            // Orientation Cube
            // What was the observer’s initial position in {0}?
            // What was the observer’s initial position in Orientation Cube?
            [Question.OrientationCubeInitialObserverPosition] = new TranslationInfo
            {
                QuestionText = "Какая была начальная позиция у наблюдателя в «{0}»?",
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
            [Question.OrientationHypercubeInitialObserverPosition] = new TranslationInfo
            {
                QuestionText = "Какая была начальная позиция у наблюдателя в «{0}»?",
                ModuleName = "Ориентации гиперкуба",
            },
            // What was the initial colour of the {1} face in {0}?
            // What was the initial colour of the right face in Orientation Hypercube?
            [Question.OrientationHypercubeInitialFaceColour] = new TranslationInfo
            {
                QuestionText = "Какой был начальный цвет {1} в «{0}»?",
                ModuleName = "Ориентации гиперкуба",
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
            },

            // Palindromes
            // What was {1}’s {2} digit from the right in {0}?
            // What was X’s first digit from the right in Palindromes?
            [Question.PalindromesNumbers] = new TranslationInfo
            {
                QuestionText = "What was {1}’s {2} digit from the right in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["X"] = "X",
                    ["Y"] = "Y",
                    ["Z"] = "Z",
                    ["the screen"] = "the screen",
                },
            },

            // Parity
            // What was shown on the display on {0}?
            // What was shown on the display on Parity?
            [Question.ParityDisplay] = new TranslationInfo
            {
                QuestionText = "What was shown on the display on «{0}»?",
            },

            // Partial Derivatives
            // What was the LED color in the {1} stage of {0}?
            // What was the LED color in the first stage of Partial Derivatives?
            [Question.PartialDerivativesLedColors] = new TranslationInfo
            {
                QuestionText = "What was the LED color in the {1} stage of «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "blue",
                    ["green"] = "green",
                    ["orange"] = "orange",
                    ["purple"] = "purple",
                    ["red"] = "red",
                    ["yellow"] = "yellow",
                },
            },
            // What was the {1} term in {0}?
            // What was the first term in Partial Derivatives?
            [Question.PartialDerivativesTerms] = new TranslationInfo
            {
                QuestionText = "What was the {1} term in «{0}»?",
            },

            // Passport Control
            // What was the passport expiration year of the {1} inspected passenger in {0}?
            // What was the passport expiration year of the first inspected passenger in Passport Control?
            [Question.PassportControlPassenger] = new TranslationInfo
            {
                QuestionText = "What was the passport expiration year of the {1} inspected passenger in «{0}»?",
            },

            // Password Destroyer
            // What was the starting value when you solved {0}?
            // What was the starting value when you solved Password Destroyer?
            [Question.PasswordDestroyerStartingValue] = new TranslationInfo
            {
                QuestionText = "What was the raw value when you solved «{0}»?",
            },
            // What was the increase factor when you solved {0}?
            // What was the increase factor when you solved Password Destroyer?
            [Question.PasswordDestroyerIncreaseFactor] = new TranslationInfo
            {
                QuestionText = "What was the increase factor when you solved «{0}»?",
            },
            // What was the TFA₁ value when you solved {0}?
            // What was the TFA₁ value when you solved Password Destroyer?
            [Question.PasswordDestroyerTF1] = new TranslationInfo
            {
                QuestionText = "What was the TFA₁ value when you solved «{0}»?",
            },
            // What was the TFA₂ value when you solved {0}?
            // What was the TFA₂ value when you solved Password Destroyer?
            [Question.PasswordDestroyerTF2] = new TranslationInfo
            {
                QuestionText = "What was the TFA₂ value when you solved «{0}»?",
            },
            // What was the 2FAST™ value when you solved {0}?
            // What was the 2FAST™ value when you solved Password Destroyer?
            [Question.PasswordDestroyerTwoFactorV2] = new TranslationInfo
            {
                QuestionText = "What was the 2FAST™ value when you solved «{0}»?",
            },
            // What was the percentage of solved modules used in the final calculation when you solved {0}?
            // What was the percentage of solved modules used in the final calculation when you solved Password Destroyer?
            [Question.PasswordDestroyerSolvePercentage] = new TranslationInfo
            {
                QuestionText = "What was the percentage of solved modules used in the final calculation when you solved «{0}»?",
            },

            // Pattern Cube
            // Which symbol was highlighted in {0}?
            // Which symbol was highlighted in Pattern Cube?
            [Question.PatternCubeHighlightedSymbol] = new TranslationInfo
            {
                QuestionText = "Which symbol was highlighted in «{0}»?",
            },

            // Periodic Words
            // What word was on the display in the {1} stage of {0}?
            // What word was on the display in the first stage of Periodic Words?
            [Question.PeriodicWordsDisplayedWords] = new TranslationInfo
            {
                QuestionText = "What word was on the display in the {1} stage of «{0}»?",
            },

            // Perspective Pegs
            // What was the {1} color in the initial sequence in {0}?
            // What was the first color in the initial sequence in Perspective Pegs?
            [Question.PerspectivePegsColorSequence] = new TranslationInfo
            {
                QuestionText = "Какой цвет был {1}-м в начальной последовательности во «{0}»?",
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
            [Question.PhosphorescenceOffset] = new TranslationInfo
            {
                QuestionText = "What was the offset in «{0}»?",
            },
            // What was the {1} button press in {0}?
            // What was the first button press in Phosphorescence?
            [Question.PhosphorescenceButtonPresses] = new TranslationInfo
            {
                QuestionText = "What was the {1} button press in «{0}»?",
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

            // Pictionary
            // What was the code in {0}?
            // What was the code in Pictionary?
            [Question.PictionaryCode] = new TranslationInfo
            {
                QuestionText = "What was the code in «{0}»?",
            },

            // Pie
            // What was the {1} digit of the displayed number in {0}?
            // What was the first digit of the displayed number in Pie?
            [Question.PieDigits] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я цифра числа, показанного в «{0}»?",
                ModuleName = "Числе Пи",
            },

            // Pie Flash
            // What number was not displayed in {0}?
            // What number was not displayed in Pie Flash?
            [Question.PieFlashDigits] = new TranslationInfo
            {
                QuestionText = "What number was not displayed in «{0}»?",
            },

            // Pigpen Cycle
            // What was the {1} in {0}?
            // What was the message in Pigpen Cycle?
            [Question.PigpenCycleWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "message",
                    ["response"] = "response",
                },
            },

            // The Pink Button
            // What was the {1} word in {0}?
            // What was the first word in The Pink Button?
            [Question.PinkButtonWords] = new TranslationInfo
            {
                QuestionText = "Какое было {1}-е слово в «{0}»?",
                ModuleName = "Розовой кнопке",
            },
            // What was the {1} color in {0}?
            // What was the first color in The Pink Button?
            [Question.PinkButtonColors] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й цвет в «{0}»?",
                ModuleName = "Розовой кнопке",
                Answers = new Dictionary<string, string>
                {
                    ["black"] = "Чёрный",
                    ["red"] = "Красный",
                    ["green"] = "Зелёный",
                    ["yellow"] = "Жёлтый",
                    ["blue"] = "Синий",
                    ["magenta"] = "Розовый",
                    ["cyan"] = "Голубой",
                    ["white"] = "Белый",
                },
            },

            // Pixel Cipher
            // What was the keyword in {0}?
            // What was the keyword in Pixel Cipher?
            [Question.PixelCipherKeyword] = new TranslationInfo
            {
                QuestionText = "Какое ключевое слово было в «{0}»?",
                ModuleName = "Пиксельном шифре",
            },

            // Placeholder Talk
            // What was the first half of the first phrase in {0}?
            // What was the first half of the first phrase in Placeholder Talk?
            [Question.PlaceholderTalkFirstPhrase] = new TranslationInfo
            {
                QuestionText = "What was the first half of the first phrase in «{0}»?",
            },
            // What was the last half of the first phrase in {0}?
            // What was the last half of the first phrase in Placeholder Talk?
            [Question.PlaceholderTalkOrdinal] = new TranslationInfo
            {
                QuestionText = "What was the last half of the first phrase in «{0}»?",
            },
            // What was the second phrase’s calculated value in {0}?
            // What was the second phrase’s calculated value in Placeholder Talk?
            [Question.PlaceholderTalkSecondPhrase] = new TranslationInfo
            {
                QuestionText = "What was the second phrase’s calculated value in «{0}»?",
            },

            // Placement Roulette
            // What was the character listed on the information display in {0}?
            // What was the character listed on the information display in Placement Roulette?
            [Question.PlacementRouletteChar] = new TranslationInfo
            {
                QuestionText = "What was the character listed on the information display in «{0}»?",
            },
            // What was the drift type listed on the information display in {0}?
            // What was the drift type listed on the information display in Placement Roulette?
            [Question.PlacementRouletteDrift] = new TranslationInfo
            {
                QuestionText = "What was the drift type listed on the information display in «{0}»?",
            },
            // What was the track listed on the information display in {0}?
            // What was the track listed on the information display in Placement Roulette?
            [Question.PlacementRouletteTrack] = new TranslationInfo
            {
                QuestionText = "What was the track listed on the information display in «{0}»?",
            },
            // What was the track type of the track listed on the information display in {0}?
            // What was the track type of the track listed on the information display in Placement Roulette?
            [Question.PlacementRouletteTrackType] = new TranslationInfo
            {
                QuestionText = "What was the track type of the track listed on the information display in «{0}»?",
            },
            // What was the vehicle listed on the information display in {0}?
            // What was the vehicle listed on the information display in Placement Roulette?
            [Question.PlacementRouletteVehicle] = new TranslationInfo
            {
                QuestionText = "What was the vehicle listed on the information display in «{0}»?",
            },
            // What was the vehicle type of the vehicle listed on the information display in {0}?
            // What was the vehicle type of the vehicle listed on the information display in Placement Roulette?
            [Question.PlacementRouletteVehicleType] = new TranslationInfo
            {
                QuestionText = "What was the vehicle type of the vehicle listed on the information display in «{0}»?",
            },

            // Planets
            // What was the planet shown in {0}?
            // What was the planet shown in Planets?
            [Question.PlanetsPlanet] = new TranslationInfo
            {
                QuestionText = "What was the planet shown in «{0}»?",
            },
            // What was the color of the {1} strip (from the top) in {0}?
            // What was the color of the first strip (from the top) in Planets?
            [Question.PlanetsStrips] = new TranslationInfo
            {
                QuestionText = "What was the color of the {1} strip (from the top) in «{0}»?",
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
            // What was the {1} in {0}?
            // What was the message in Playfair Cycle?
            [Question.PlayfairCycleWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "message",
                    ["response"] = "response",
                },
            },

            // Poetry
            // What was the {1} correct answer you pressed in {0}?
            // What was the first correct answer you pressed in Poetry?
            [Question.PoetryAnswers] = new TranslationInfo
            {
                QuestionText = "Какое было {1}-е правильное слово, которое вы нажали в «{0}»?",
                ModuleName = "Поэзии",
            },

            // Polyhedral Maze
            // What was the starting position in {0}?
            // What was the starting position in Polyhedral Maze?
            [Question.PolyhedralMazeStartPosition] = new TranslationInfo
            {
                QuestionText = "Какая была начальная позиция в «{0}»?",
                ModuleName = "Многогранном лабиринте",
            },

            // Prime Encryption
            // What was the number shown in {0}?
            // What was the number shown in Prime Encryption?
            [Question.PrimeEncryptionDisplayedValue] = new TranslationInfo
            {
                QuestionText = "What was the number shown in «{0}»?",
            },

            // Probing
            // What was the missing frequency in the {1} wire in {0}?
            // What was the missing frequency in the red-white wire in Probing?
            [Question.ProbingFrequencies] = new TranslationInfo
            {
                QuestionText = "Какая частота отсутствовала в {1} проводе в «{0}»?",
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
            [Question.ProceduralMazeInitialSeed] = new TranslationInfo
            {
                QuestionText = "Какое было изначальное семя в «{0}»?",
                ModuleName = "Процедурном лабиринте",
            },

            // ...?
            // What was the displayed number in {0}?
            // What was the displayed number in ...??
            [Question.PunctuationMarksDisplayedNumber] = new TranslationInfo
            {
                QuestionText = "Какое было показанное число в модуле «{0}»?",
            },

            // Purple Arrows
            // What was the target word on {0}?
            // What was the target word on Purple Arrows?
            [Question.PurpleArrowsFinish] = new TranslationInfo
            {
                QuestionText = "Какое было целевое слово в «{0}»?",
                ModuleName = "Фиолетовых стрелках",
            },

            // The Purple Button
            // What was the {1} number in the cyclic sequence on {0}?
            // What was the first number in the cyclic sequence on The Purple Button?
            [Question.PurpleButtonNumbers] = new TranslationInfo
            {
                QuestionText = "Какое было {1}-е число в зацикленной последовательности в «{0}»?",
                ModuleName = "Фиолетовой кнопке",
            },

            // Puzzle Identification
            // What was the {1} puzzle number in {0}?
            // What was the first puzzle number in Puzzle Identification?
            [Question.PuzzleIdentificationNum] = new TranslationInfo
            {
                QuestionText = "What was the {1} puzzle number in «{0}»?",
            },
            // What game was the {1} puzzle in {0} from?
            // What game was the first puzzle in Puzzle Identification from?
            [Question.PuzzleIdentificationGame] = new TranslationInfo
            {
                QuestionText = "What game was the {1} puzzle in «{0}» from?",
            },
            // What was the {1} puzzle in {0}?
            // What was the first puzzle in Puzzle Identification?
            [Question.PuzzleIdentificationName] = new TranslationInfo
            {
                QuestionText = "What was the {1} puzzle in «{0}»?",
            },

            // Quaver
            // What was the {1} sequence’s answer in {0}?
            // What was the first sequence’s answer in Quaver?
            [Question.QuaverArrows] = new TranslationInfo
            {
                QuestionText = "What was the {1} sequence’s answer in «{0}»?",
            },

            // Question Mark
            // Which of these symbols was part of the flashing sequence in {0}?
            // Which of these symbols was part of the flashing sequence in Question Mark?
            [Question.QuestionMarkFlashedSymbols] = new TranslationInfo
            {
                QuestionText = "Какой из этих символов был частью мигающей последовательности в «{0}»?",
                ModuleName = "Вопросительном знаке",
            },

            // Quick Arithmetic
            // What was the {1} color in the primary sequence in {0}?
            // What was the first color in the primary sequence in Quick Arithmetic?
            [Question.QuickArithmeticColors] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й цвет в основной последовательности в «{0}»?",
                ModuleName = "Быстрой арифметике",
            },
            // What was the {1} digit in the {2} sequence in {0}?
            // What was the first digit in the primary sequence in Quick Arithmetic?
            [Question.QuickArithmeticPrimSecDigits] = new TranslationInfo
            {
                QuestionText = "Какое было {1}-е число в {2} последовательности в «{0}»?",
                ModuleName = "Быстрой арифметике",
            },

            // Quintuples
            // What was the {1} digit in the {2} slot in {0}?
            // What was the first digit in the first slot in Quintuples?
            [Question.QuintuplesNumbers] = new TranslationInfo
            {
                QuestionText = "What was the {1} digit in the {2} slot in «{0}»?",
            },
            // What color was the {1} digit in the {2} slot in {0}?
            // What color was the first digit in the first slot in Quintuples?
            [Question.QuintuplesColors] = new TranslationInfo
            {
                QuestionText = "What color was the {1} digit in the {2} slot in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["blue"] = "blue",
                    ["orange"] = "orange",
                    ["green"] = "green",
                    ["pink"] = "pink",
                },
            },
            // How many numbers were {1} in {0}?
            // How many numbers were red in Quintuples?
            [Question.QuintuplesColorCounts] = new TranslationInfo
            {
                QuestionText = "How many numbers were {1} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["blue"] = "blue",
                    ["orange"] = "orange",
                    ["green"] = "green",
                    ["pink"] = "pink",
                },
            },

            // Quiz Buzz
            // What was the number initially on the display in {0}?
            // What was the number initially on the display in Quiz Buzz?
            [Question.QuizBuzzStartingNumber] = new TranslationInfo
            {
                QuestionText = "What was the number initially on the display in «{0}»?",
            },

            // Qwirkle
            // What tile did you place {1} in {0}?
            // What tile did you place first in Qwirkle?
            [Question.QwirkleTilesPlaced] = new TranslationInfo
            {
                QuestionText = "What tile did you place {1} in «{0}»?",
            },

            // Raiding Temples
            // How many jewels were in the starting common pool in {0}?
            // How many jewels were in the starting common pool in Raiding Temples?
            [Question.RaidingTemplesStartingCommonPool] = new TranslationInfo
            {
                QuestionText = "How many jewels were in the starting common pool in «{0}»?",
            },

            // Railway Cargo Loading
            // What was the {1} car in {0}?
            // What was the first car in Railway Cargo Loading?
            [Question.RailwayCargoLoadingCars] = new TranslationInfo
            {
                QuestionText = "What was the {1} coupled car in «{0}»?",
            },
            // Which freight table rule {1} in {0}?
            // Which freight table rule was met in Railway Cargo Loading?
            [Question.RailwayCargoLoadingFreightTableRules] = new TranslationInfo
            {
                QuestionText = "Which freight table rule {1} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was met"] = "was met",
                    ["wasn’t met"] = "wasn’t met",
                },
            },

            // Rainbow Arrows
            // What was the displayed number in {0}?
            // What was the displayed number in Rainbow Arrows?
            [Question.RainbowArrowsNumber] = new TranslationInfo
            {
                QuestionText = "Какое число было показано в «{0}»?",
                ModuleName = "Радужных стрелках",
            },

            // Recolored Switches
            // What was the color of the {1} LED in {0}?
            // What was the color of the first LED in Recolored Switches?
            [Question.RecoloredSwitchesLedColors] = new TranslationInfo
            {
                QuestionText = "What was the color of the {1} LED in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                    ["cyan"] = "cyan",
                    ["orange"] = "orange",
                    ["purple"] = "purple",
                    ["white"] = "white",
                },
            },

            // Recursive Password
            // Which of these words appeared, but was not the password, in {0}?
            // Which of these words appeared, but was not the password, in Recursive Password?
            [Question.RecursivePasswordNonPasswordWords] = new TranslationInfo
            {
                QuestionText = "Какое из этих слов присутствовало, но не являлось верным ответом «{0}»?",
                ModuleName = "Рекурсивного пароля",
            },
            // What was the password in {0}?
            // What was the password in Recursive Password?
            [Question.RecursivePasswordPassword] = new TranslationInfo
            {
                QuestionText = "Какой пароль был верным ответом «{0}»?",
                ModuleName = "Рекурсивного пароля",
            },

            // Red Arrows
            // What was the starting number in {0}?
            // What was the starting number in Red Arrows?
            [Question.RedArrowsStartNumber] = new TranslationInfo
            {
                QuestionText = "Какое было начальное число в «{0}»?",
                ModuleName = "Красных стрелках",
            },

            // Red Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Red Cipher?
            [Question.RedCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Красном шифре",
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
            [Question.RedHerringFirstFlash] = new TranslationInfo
            {
                QuestionText = "Какой был первый мигающий цвет в «{0}»?",
                ModuleName = "Отвлекающем манёвре",
            },

            // Reformed Role Reversal
            // Which condition was the solving condition in {0}?
            // Which condition was the solving condition in Reformed Role Reversal?
            [Question.ReformedRoleReversalCondition] = new TranslationInfo
            {
                QuestionText = "Which condition was the solving condition in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["second"] = "second",
                    ["third"] = "third",
                    ["4th"] = "4th",
                    ["5th"] = "5th",
                    ["6th"] = "6th",
                    ["7th"] = "7th",
                    ["8th"] = "8th",
                },
            },
            // What color was the {1} wire in {0}?
            // What color was the first wire in Reformed Role Reversal?
            [Question.ReformedRoleReversalWire] = new TranslationInfo
            {
                QuestionText = "What color was the {1} wire in «{0}»?",
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

            // Regular Crazy Talk
            // What was the displayed digit that corresponded to the solution phrase in {0}?
            // What was the displayed digit that corresponded to the solution phrase in Regular Crazy Talk?
            [Question.RegularCrazyTalkDigit] = new TranslationInfo
            {
                QuestionText = "What was the displayed digit that corresponded to the solution phrase in «{0}»?",
            },
            // What was the embellishment of the solution phrase in {0}?
            // What was the embellishment of the solution phrase in Regular Crazy Talk?
            [Question.RegularCrazyTalkModifier] = new TranslationInfo
            {
                QuestionText = "What was the embellishment of the solution phrase in «{0}»?",
            },

            // Retirement
            // Which one of these houses was on offer, but not chosen by Bob in {0}?
            // Which one of these houses was on offer, but not chosen by Bob in Retirement?
            [Question.RetirementHouses] = new TranslationInfo
            {
                QuestionText = "Which one of these houses was on offer, but not chosen by Bob in «{0}»?",
            },

            // Reverse Morse
            // What was the {1} character in the {2} message of {0}?
            // What was the first character in the first message of Reverse Morse?
            [Question.ReverseMorseCharacters] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й символ в {2}-м сообщении в «{0}»?",
                ModuleName = "Обратной азбуке Морзе",
            },

            // Reverse Polish Notation
            // What character was used in the {1} round of {0}?
            // What character was used in the first round of Reverse Polish Notation?
            [Question.ReversePolishNotationCharacter] = new TranslationInfo
            {
                QuestionText = "What character was used in the {1} round of «{0}»?",
            },

            // RGB Maze
            // What was the exit coordinate in {0}?
            // What was the exit coordinate in RGB Maze?
            [Question.RGBMazeExit] = new TranslationInfo
            {
                QuestionText = "What was the exit coordinate in «{0}»?",
            },
            // Where was the {1} key in {0}?
            // Where was the red key in RGB Maze?
            [Question.RGBMazeKeys] = new TranslationInfo
            {
                QuestionText = "Where was the {1} key in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                },
            },
            // Which maze number was the {1} maze in {0}?
            // Which maze number was the red maze in RGB Maze?
            [Question.RGBMazeNumber] = new TranslationInfo
            {
                QuestionText = "Which maze number was the {1} maze in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                },
            },

            // RGB Sequences
            // What was the color of the {1} LED in {0}?
            // What was the color of the first LED in RGB Sequences?
            [Question.RGBSequencesDisplay] = new TranslationInfo
            {
                QuestionText = "What was the color of the {1} LED in «{0}»?",
            },

            // Rhythms
            // What was the color in {0}?
            // What was the color in Rhythms?
            [Question.RhythmsColor] = new TranslationInfo
            {
                QuestionText = "Каким был цвет светодиода в «{0}»?",
                ModuleName = "Музыкальном ритме",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Синий",
                    ["Red"] = "Красный",
                    ["Green"] = "Зелёный",
                    ["Yellow"] = "Жёлтый",
                },
            },

            // Robo-Scanner
            // Where was the empty cell in {0}?
            // Where was the empty cell in Robo-Scanner?
            [Question.RoboScannerEmptyCell] = new TranslationInfo
            {
                QuestionText = "Где была пустая ячейка в «{0}»?",
                ModuleName = "Робо-сканере",
            },

            // Robot Programming
            // What was the name of the robot in the {1} position of {0}?
            // What was the name of the robot in the first position of Robot Programming?
            [Question.RobotProgrammingName] = new TranslationInfo
            {
                QuestionText = "Какое было имя робота на {1}-й позиции в «{0}»?",
                ModuleName = "Программировании робота",
            },

            // Roger
            // What four-digit number was given in {0}?
            // What four-digit number was given in Roger?
            [Question.RogerSeed] = new TranslationInfo
            {
                QuestionText = "What four-digit number was given in «{0}»?",
            },

            // Role Reversal
            // What was the number to the correct condition in {0}?
            // What was the number to the correct condition in Role Reversal?
            [Question.RoleReversalNumber] = new TranslationInfo
            {
                QuestionText = "What was the number to the correct condition in «{0}»?",
            },
            // How many {1} wires were there in {0}?
            // How many warm-colored wires were there in Role Reversal?
            [Question.RoleReversalWires] = new TranslationInfo
            {
                QuestionText = "How many {1} wires were there in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["warm-colored"] = "warm-colored",
                    ["cold-colored"] = "cold-colored",
                    ["primary-colored"] = "primary-colored",
                    ["secondary-colored"] = "secondary-colored",
                },
            },

            // The Rule
            // What was the rule number in {0}?
            // What was the rule number in The Rule?
            [Question.RuleNumber] = new TranslationInfo
            {
                QuestionText = "What was the rule number in «{0}»?",
            },

            // Rule of Three
            // What was the {1} coordinate of the {2} vertex in {0}?
            // What was the X coordinate of the red vertex in Rule of Three?
            [Question.RuleOfThreeCoordinates] = new TranslationInfo
            {
                QuestionText = "What was the {1} coordinate of the {2} vertex in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["yellow"] = "yellow",
                    ["blue"] = "blue",
                },
            },
            // What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?
            // What was the position of the red sphere on the X axis in the first cycle in Rule of Three?
            [Question.RuleOfThreeCycles] = new TranslationInfo
            {
                QuestionText = "What was the position of the {1} sphere on the {2} axis in the {3} cycle in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["yellow"] = "yellow",
                    ["blue"] = "blue",
                },
            },

            // Safety Square
            // What was the digit displayed on the {1} diamond in {0}?
            // What was the digit displayed on the red diamond in Safety Square?
            [Question.SafetySquareDigits] = new TranslationInfo
            {
                QuestionText = "What was the digit displayed on the {1} diamond in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["yellow"] = "yellow",
                    ["blue"] = "blue",
                },
            },
            // What was the special rule displayed on the white diamond in {0}?
            // What was the special rule displayed on the white diamond in Safety Square?
            [Question.SafetySquareSpecialRule] = new TranslationInfo
            {
                QuestionText = "What was the special rule displayed on the white diamond in «{0}»?",
            },

            // The Samsung
            // Where was {1} in {0}?
            // Where was Duolingo in The Samsung?
            [Question.SamsungAppPositions] = new TranslationInfo
            {
                QuestionText = "Где было приложение {1} в «{0}»?",
                ModuleName = "Samsung",
            },

            // Scavenger Hunt
            // Which tile was correctly submitted in the first stage of {0}?
            // Which tile was correctly submitted in the first stage of Scavenger Hunt?
            [Question.ScavengerHuntKeySquare] = new TranslationInfo
            {
                QuestionText = "Which tile was correctly submitted in the first stage of «{0}»?",
            },
            // Which of these tiles was {1} in the first stage of {0}?
            // Which of these tiles was red in the first stage of Scavenger Hunt?
            [Question.ScavengerHuntColoredTiles] = new TranslationInfo
            {
                QuestionText = "Which of these tiles was {1} in the first stage of «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                },
            },

            // Schlag den Bomb
            // What was the contestant’s name in {0}?
            // What was the contestant’s name in Schlag den Bomb?
            [Question.SchlagDenBombContestantName] = new TranslationInfo
            {
                QuestionText = "What was the contestant’s name in «{0}»?",
            },
            // What was the contestant’s score in {0}?
            // What was the contestant’s score in Schlag den Bomb?
            [Question.SchlagDenBombContestantScore] = new TranslationInfo
            {
                QuestionText = "What was the contestant’s score in «{0}»?",
            },
            // What was the bomb’s score in {0}?
            // What was the bomb’s score in Schlag den Bomb?
            [Question.SchlagDenBombBombScore] = new TranslationInfo
            {
                QuestionText = "What was the bomb’s score in «{0}»?",
            },

            // Scramboozled Eggain
            // What was the {1} encrypted word in {0}?
            // What was the first encrypted word in Scramboozled Eggain?
            [Question.ScramboozledEggainWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} encrypted word in «{0}»?",
            },

            // Scripting
            // What was the submitted data type of the variable in {0}?
            // What was the submitted data type of the variable in Scripting?
            [Question.ScriptingVariableDataType] = new TranslationInfo
            {
                QuestionText = "Какой был верный тип данных переменной в «{0}»?",
                ModuleName = "Скриптинге",
            },

            // Scrutiny Squares
            // What was the modified property of the first display in {0}?
            // What was the modified property of the first display in Scrutiny Squares?
            [Question.ScrutinySquaresFirstDifference] = new TranslationInfo
            {
                QuestionText = "What was the modified property of the first display in «{0}»?",
            },

            // Sea Shells
            // What were the first and second words in the {1} phrase in {0}?
            // What were the first and second words in the first phrase in Sea Shells?
            [Question.SeaShells1] = new TranslationInfo
            {
                QuestionText = "Какими были первое и второе слова {1}-й фразы в «{0}»?",
                ModuleName = "Морских ракушках",
            },
            // What were the third and fourth words in the {1} phrase in {0}?
            // What were the third and fourth words in the first phrase in Sea Shells?
            [Question.SeaShells2] = new TranslationInfo
            {
                QuestionText = "Какими были третье и четвёртое слова {1}-й фразы в «{0}»?",
                ModuleName = "Морских ракушках",
            },
            // What was the end of the {1} phrase in {0}?
            // What was the end of the first phrase in Sea Shells?
            [Question.SeaShells3] = new TranslationInfo
            {
                QuestionText = "Каким был конец {1}-й фразы в «{0}»?",
                ModuleName = "Морских ракушках",
            },

            // Semamorse
            // What was the {1} letter involved in the starting value in {0}?
            // What was the Morse letter involved in the starting value in Semamorse?
            [Question.SemamorseLetters] = new TranslationInfo
            {
                QuestionText = "Какая была буква {1}, использованная в начальном значении в «{0}»?",
                ModuleName = "Семаморзе",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Morse"] = "Морзе",
                    ["semaphore"] = "семафора",
                },
            },
            // What was the color of the display involved in the starting value in {0}?
            // What was the color of the display involved in the starting value in Semamorse?
            [Question.SemamorseColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета светодиоды использовались в начальном значении в «{0}»?",
                ModuleName = "Семаморзе",
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
            [Question.SequencyclopediaSequence] = new TranslationInfo
            {
                QuestionText = "What sequence was used in «{0}»?",
            },

            // S.E.T. Theory
            // What equation was shown in the {1} stage of {0}?
            // What equation was shown in the first stage of S.E.T. Theory?
            [Question.SetTheoryEquations] = new TranslationInfo
            {
                QuestionText = "What equation was shown in the {1} stage of «{0}»?",
            },

            // Shapes And Bombs
            // What was the initial letter in {0}?
            // What was the initial letter in Shapes And Bombs?
            [Question.ShapesAndBombsInitialLetter] = new TranslationInfo
            {
                QuestionText = "What was the initial letter in «{0}»?",
            },

            // Shape Shift
            // What was the initial shape in {0}?
            // What was the initial shape in Shape Shift?
            [Question.ShapeShiftInitialShape] = new TranslationInfo
            {
                QuestionText = "Какая была изначальная фигура в «{0}»?",
                ModuleName = "Изменении формы",
            },

            // Shifted Maze
            // What color was the {1} marker in {0}?
            // What color was the top-left marker in Shifted Maze?
            [Question.ShiftedMazeColors] = new TranslationInfo
            {
                QuestionText = "What color was the {1} marker in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-right"] = "top-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
            },

            // Shifting Maze
            // What was the seed in {0}?
            // What was the seed in Shifting Maze?
            [Question.ShiftingMazeSeed] = new TranslationInfo
            {
                QuestionText = "Какое было семя в «{0}»?",
                ModuleName = "Сдвинутом лабиринте",
            },

            // Shogi Identification
            // What was the displayed piece in {0}?
            // What was the displayed piece in Shogi Identification?
            [Question.ShogiIdentificationPiece] = new TranslationInfo
            {
                QuestionText = "What was the displayed piece in «{0}»?",
            },

            // Silly Slots
            // What was the {1} slot in the {2} stage in {0}?
            // What was the first slot in the first stage in Silly Slots?
            [Question.SillySlots] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й слот на {2}-м этапе в «{0}»?",
                ModuleName = "Одноруком бандите",
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

            // Sign Language
            // What was the deciphered word in {0}?
            // What was the deciphered word in Sign Language?
            [Question.SignLanguageWord] = new TranslationInfo
            {
                QuestionText = "Какое слово было расшифровано на «{0}»?",
                ModuleName = "Языке знаков",
            },

            // Silo Authorization
            // What was the message type in {0}?
            // What was the message type in Silo Authorization?
            [Question.SiloAuthorizationMessageType] = new TranslationInfo
            {
                QuestionText = "What was the message type in «{0}»?",
            },
            // What was the {1} part of the encrypted message in {0}?
            // What was the first part of the encrypted message in Silo Authorization?
            [Question.SiloAuthorizationEncryptedMessage] = new TranslationInfo
            {
                QuestionText = "What was the {1} part of the encrypted message in «{0}»?",
            },
            // What was the received authentication code in {0}?
            // What was the received authentication code in Silo Authorization?
            [Question.SiloAuthorizationAuthCode] = new TranslationInfo
            {
                QuestionText = "What was the received authentication code in «{0}»?",
            },

            // Simon Said
            // What color was pressed {1} in the final sequence of {0}?
            // What color was pressed first in the final sequence of Simon Said?
            [Question.SimonSaidPresses] = new TranslationInfo
            {
                QuestionText = "What color was pressed in the {1} stage of «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Yellow"] = "Yellow",
                },
            },

            // Simon Samples
            // What were the call samples {1} of {0}?
            // What were the call samples played in the first stage of Simon Samples?
            [Question.SimonSamplesSamples] = new TranslationInfo
            {
                QuestionText = "Какие семплы были проиграны на {1}-м этапе в исходной последовательности в «{0}»?",
                ModuleName = "Семплах Саймона",
                FormatArgs = new Dictionary<string, string>
                {
                    ["played in the first stage"] = "1",
                    ["added in the second stage"] = "2",
                    ["added in the third stage"] = "3",
                },
                Answers = new Dictionary<string, string>
                {
                    ["KKSS"] = "ББММ",
                    ["KKSH"] = "ББМХ",
                    ["KSSH"] = "БММХ",
                    ["KHSS"] = "БХММ",
                    ["KHSH"] = "БХМХ",
                    ["KHSO"] = "БХМО",
                    ["KHOH"] = "БХОХ",
                    ["KOSH"] = "БОМХ",
                    ["KOSO"] = "БОМО",
                    ["SKSK"] = "МБМБ",
                    ["SHHS"] = "МХХМ",
                },
            },

            // Simon Says
            // What color flashed {1} in the final sequence in {0}?
            // What color flashed first in the final sequence in Simon Says?
            [Question.SimonSaysFlash] = new TranslationInfo
            {
                QuestionText = "Какой цвет горел {1}-м в исходной последовательности в модуле «{0}»?",
                ModuleName = "Саймон говорит",
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
            [Question.SimonScramblesColors] = new TranslationInfo
            {
                QuestionText = "Какой цвет горел {1}-м в модуле «{0}»?",
                ModuleName = "Саймон перемешивает",
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
            [Question.SimonScreamsFlashing] = new TranslationInfo
            {
                QuestionText = "Какой цвет горел {1}-м в исходной последовательности в модуле «{0}»?",
                ModuleName = "Саймон кричит",
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
            [Question.SimonScreamsRuleSimple] = new TranslationInfo
            {
                QuestionText = "На каком(-их) этапе(-ах) в модуле «{0}» {1}?",
                ModuleName = "Саймон кричит",
                FormatArgs = new Dictionary<string, string>
                {
                    ["a color flashed, then a color two away, then the first again"] = "горела кнопка, затем другая через одну, а после снова первая",
                    ["a color flashed, then a color two away, then the one opposite that"] = "горела кнопка, затем другая через одну, а после кнопка, стоящая напротив этой (второй)",
                    ["a color flashed, then a color two away, then the one opposite the first"] = "горела кнопка, затем другая через одну, а после кнопка, стоящая напротив первой",
                    ["a color flashed, then an adjacent color, then the first again"] = "горела кнопка, затем соседняя, а после снова первая",
                    ["a color flashed, then another color, then the first"] = "горела кнопка, затем другая, а после снова первая",
                    ["a color flashed, then one adjacent, then the one opposite that"] = "горела кнопка, затем соседняя, а после кнопка, стоящая напротив этой (второй)",
                    ["a color flashed, then one adjacent, then the one opposite the first"] = "горела кнопка, затем соседняя, а после кнопка, стоящая напротив первой",
                    ["a color flashed, then the one opposite, then one adjacent to that"] = "горела кнопка, затем стоящая напротив неё, а после соседняя с ней (со второй)",
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
            [Question.SimonScreamsRuleComplex] = new TranslationInfo
            {
                QuestionText = "На каком(-их) этапе(-ах) в модуле «{0}» среди кнопок {2}, {3} и {4} цвета {1}?",
                ModuleName = "Саймон кричит",
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
            [Question.SimonSelectsOrder] = new TranslationInfo
            {
                QuestionText = "Which color flashed {1} in the {2} stage of «{0}»?",
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
            [Question.SimonSendsReceivedLetters] = new TranslationInfo
            {
                QuestionText = "Какая была {1} полученная буква в модуле «{0}»?",
                ModuleName = "Саймон отправляет",
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
            [Question.SimonServesFlash] = new TranslationInfo
            {
                QuestionText = "Who flashed {1} in course {2} of {0}?",
            },
            // Which item was not served in course {1} of {0}?
            // Which item was not served in course 1 of Simon Serves?
            [Question.SimonServesFood] = new TranslationInfo
            {
                QuestionText = "Which item was not served in course {1} of {0}?",
            },

            // Simon Shapes
            // What was the shape submitted at the end of {0}?
            // What was the shape submitted at the end of Simon Shapes?
            [Question.SimonShapesSubmittedShape] = new TranslationInfo
            {
                QuestionText = "What was the shape submitted at the end of «{0}»?",
            },

            // Simon Simons
            // What was the {1} flash in the final sequence in {0}?
            // What was the first flash in the final sequence in Simon Simons?
            [Question.SimonSimonsFlashingColors] = new TranslationInfo
            {
                QuestionText = "What was the {1} flash in the final sequence in «{0}»?",
            },

            // Simon Sings
            // Which key’s color flashed {1} in the {2} stage of {0}?
            // Which key’s color flashed first in the first stage of Simon Sings?
            [Question.SimonSingsFlashing] = new TranslationInfo
            {
                QuestionText = "Which key’s color flashed {1} in the {2} stage of «{0}»?",
            },

            // Simon Shouts
            // Which letter flashed on the {1} button in {0}?
            // Which letter flashed on the top button in Simon Shouts?
            [Question.SimonShoutsFlashingLetter] = new TranslationInfo
            {
                QuestionText = "Which letter flashed on the {1} button in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["left"] = "left",
                    ["right"] = "right",
                    ["bottom"] = "bottom",
                },
            },

            // Simon Shrieks
            // How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?
            // How many spaces clockwise from the arrow was the first flash in the final sequence in Simon Shrieks?
            [Question.SimonShrieksFlashingButton] = new TranslationInfo
            {
                QuestionText = "How many spaces clockwise from the arrow was the {1} flash in the final sequence in «{0}»?",
            },

            // Simon Signals
            // What shape was the {1} arrow in {0}?
            // What shape was the red arrow in Simon Signals?
            [Question.SimonSignalsColorToShape] = new TranslationInfo
            {
                QuestionText = "What shape was the {1} arrow in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                    ["gray"] = "gray",
                },
            },
            // How many directions did the {1} arrow in {0} have?
            // How many directions did the red arrow in Simon Signals have?
            [Question.SimonSignalsColorToRotations] = new TranslationInfo
            {
                QuestionText = "How many directions did the {1} arrow in «{0}» have?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                    ["gray"] = "gray",
                },
            },
            // What color was the arrow with this shape in {0}?
            // What color was the arrow with this shape in Simon Signals?
            [Question.SimonSignalsShapeToColor] = new TranslationInfo
            {
                QuestionText = "What color was the arrow with this shape in «{0}»?",
            },
            // How many directions did the arrow with this shape have in {0}?
            // How many directions did the arrow with this shape have in Simon Signals?
            [Question.SimonSignalsShapeToRotations] = new TranslationInfo
            {
                QuestionText = "How many directions did the arrow with this shape have in «{0}»?",
            },
            // What color was the arrow with {1} possible directions in {0}?
            // What color was the arrow with 3 possible directions in Simon Signals?
            [Question.SimonSignalsRotationsToColor] = new TranslationInfo
            {
                QuestionText = "What color was the arrow with {1} possible directions in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                    ["gray"] = "gray",
                },
            },
            // What shape was the arrow with {1} possible directions in {0}?
            // What shape was the arrow with 3 possible directions in Simon Signals?
            [Question.SimonSignalsRotationsToShape] = new TranslationInfo
            {
                QuestionText = "What shape was the arrow with {1} possible directions in «{0}»?",
            },

            // Simon Smothers
            // What was the color of the {1} flash in {0}?
            // What was the color of the first flash in Simon Smothers?
            [Question.SimonSmothersColors] = new TranslationInfo
            {
                QuestionText = "What was the color of the {1} flash in «{0}»?",
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
            [Question.SimonSmothersDirections] = new TranslationInfo
            {
                QuestionText = "What was the direction of the {1} flash in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Up",
                    ["Down"] = "Down",
                    ["Left"] = "Left",
                    ["Right"] = "Right",
                },
            },

            // Simon Sounds
            // Which sample button sounded {1} in the final sequence in {0}?
            // Which sample button sounded first in the final sequence in Simon Sounds?
            [Question.SimonSoundsFlashingColors] = new TranslationInfo
            {
                QuestionText = "Which sample button sounded {1} in the final sequence in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["blue"] = "blue",
                    ["yellow"] = "yellow",
                    ["green"] = "green",
                },
            },

            // Simon Speaks
            // Which bubble flashed first in {0}?
            // Which bubble flashed first in Simon Speaks?
            [Question.SimonSpeaksPositions] = new TranslationInfo
            {
                QuestionText = "Which bubble flashed first in «{0}»?",
            },
            // Which bubble flashed second in {0}?
            // Which bubble flashed second in Simon Speaks?
            [Question.SimonSpeaksShapes] = new TranslationInfo
            {
                QuestionText = "Which bubble flashed second in «{0}»?",
            },
            // Which language was the bubble that flashed third in {0} in?
            // Which language was the bubble that flashed third in Simon Speaks in?
            [Question.SimonSpeaksLanguages] = new TranslationInfo
            {
                QuestionText = "Which language was the bubble that flashed third in «{0}» in?",
            },
            // Which word was in the bubble that flashed fourth in {0}?
            // Which word was in the bubble that flashed fourth in Simon Speaks?
            [Question.SimonSpeaksWords] = new TranslationInfo
            {
                QuestionText = "Which word was in the bubble that flashed fourth in «{0}»?",
            },
            // What color was the bubble that flashed fifth in {0}?
            // What color was the bubble that flashed fifth in Simon Speaks?
            [Question.SimonSpeaksColors] = new TranslationInfo
            {
                QuestionText = "What color was the bubble that flashed fifth in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["black"] = "black",
                    ["blue"] = "blue",
                    ["green"] = "green",
                    ["cyan"] = "cyan",
                    ["red"] = "red",
                    ["purple"] = "purple",
                    ["yellow"] = "yellow",
                    ["white"] = "white",
                    ["gray"] = "gray",
                },
            },

            // Simon’s Star
            // Which color flashed {1} in sequence in {0}?
            // Which color flashed first in sequence in Simon’s Star?
            [Question.SimonsStarColors] = new TranslationInfo
            {
                QuestionText = "Which color flashed {1} in sequence in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["yellow"] = "yellow",
                    ["green"] = "green",
                    ["blue"] = "blue",
                    ["purple"] = "purple",
                },
            },

            // Simon Stacks
            // Which color flashed in the {1} stage of {0}?
            // Which color flashed in the first stage of Simon Stacks?
            [Question.SimonStacksColors] = new TranslationInfo
            {
                QuestionText = "Which color flashed in the {1} stage of «{0}»?",
            },

            // Simon Stages
            // Which color flashed {1} in the {2} stage in {0}?
            // Which color flashed first in the first stage in Simon Stages?
            [Question.SimonStagesFlashes] = new TranslationInfo
            {
                QuestionText = "Какой цвет горел {1}-м на {2}-м этапе в модуле «{0}»?",
                ModuleName = "Саймон выступает",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красный",
                    ["blue"] = "Синий",
                    ["yellow"] = "Жёлтый",
                    ["orange"] = "Оранжевый",
                    ["magenta"] = "Мадженты",
                    ["green"] = "Зелёный",
                    ["pink"] = "Розовый",
                    ["lime"] = "Лаймовый",
                    ["cyan"] = "Голубой",
                    ["white"] = "Белый",
                },
            },
            // What color was the indicator in the {1} stage in {0}?
            // What color was the indicator in the first stage in Simon Stages?
            [Question.SimonStagesIndicator] = new TranslationInfo
            {
                QuestionText = "Какого цвета был индикатор на {1}-м этапе в модуле «{0}»?",
                ModuleName = "Саймон выступает",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "Красного",
                    ["blue"] = "Синего",
                    ["yellow"] = "Жёлтого",
                    ["orange"] = "Оранжевого",
                    ["magenta"] = "Мадженты",
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
            [Question.SimonStatesDisplay] = new TranslationInfo
            {
                QuestionText = "Какой(-ие) цвет(а) {1} на {2}-м этапе в модуле «{0}»?",
                ModuleName = "Саймон утверждает",
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
            [Question.SimonStopsColors] = new TranslationInfo
            {
                QuestionText = "Which color flashed {1} in the output sequence in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Yellow",
                    ["Green"] = "Green",
                    ["Blue"] = "Blue",
                    ["Violet"] = "Violet",
                },
            },

            // Simon Stores
            // Which color {1} {2} in the final sequence of {0}?
            // Which color flashed first in the final sequence of Simon Stores?
            [Question.SimonStoresColors] = new TranslationInfo
            {
                QuestionText = "Which color {1} {2} in the final sequence of «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["flashed"] = "flashed",
                    ["was among the colors flashed"] = "was among the colors flashed",
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
            [Question.SimonSubdividesButton] = new TranslationInfo
            {
                QuestionText = "What color was the button at this position in «{0}»?",
            },

            // Simon Supports
            // What was the {1} topic in {0}?
            // What was the first topic in Simon Supports?
            [Question.SimonSupportsTopics] = new TranslationInfo
            {
                QuestionText = "What was the {1} topic in «{0}»?",
            },

            // Simultaneous Simons
            // What color flashed {1} on the {2} Simon in {0}?
            // What color flashed first on the first Simon in Simultaneous Simons?
            [Question.SimultaneousSimonsFlash] = new TranslationInfo
            {
                QuestionText = "What color flashed {1} on the {2} Simon in «{0}»?",
            },

            // Skewed Slots
            // What were the original numbers in {0}?
            // What were the original numbers in Skewed Slots?
            [Question.SkewedSlotsOriginalNumbers] = new TranslationInfo
            {
                QuestionText = "Какие были изначальные цифры в «{0}»?",
                ModuleName = "Искажённых слотах",
            },

            // Skyrim
            // Which race was selectable, but not the solution, in {0}?
            // Which race was selectable, but not the solution, in Skyrim?
            [Question.SkyrimRace] = new TranslationInfo
            {
                QuestionText = "Какая раса присутствовала (но не являлась решением) в «{0}»?",
                ModuleName = "Скайриме",
            },
            // Which weapon was selectable, but not the solution, in {0}?
            // Which weapon was selectable, but not the solution, in Skyrim?
            [Question.SkyrimWeapon] = new TranslationInfo
            {
                QuestionText = "Какое оружие присутствовало (но не являлось решением) в «{0}»?",
                ModuleName = "Скайриме",
            },
            // Which enemy was selectable, but not the solution, in {0}?
            // Which enemy was selectable, but not the solution, in Skyrim?
            [Question.SkyrimEnemy] = new TranslationInfo
            {
                QuestionText = "Какой враг присутствовал (но не являлся решением) в «{0}»?",
                ModuleName = "Скайриме",
            },
            // Which city was selectable, but not the solution, in {0}?
            // Which city was selectable, but not the solution, in Skyrim?
            [Question.SkyrimCity] = new TranslationInfo
            {
                QuestionText = "Какой город присутствовал (но не являлся решением) в «{0}»?",
                ModuleName = "Скайриме",
            },
            // Which dragon shout was selectable, but not the solution, in {0}?
            // Which dragon shout was selectable, but not the solution, in Skyrim?
            [Question.SkyrimDragonShout] = new TranslationInfo
            {
                QuestionText = "Какой крик дракона присутствовал (но не являлся решением) в «{0}»?",
                ModuleName = "Скайриме",
            },

            // Slow Math
            // What was the last triplet of letters in {0}?
            // What was the last triplet of letters in Slow Math?
            [Question.SlowMathLastLetters] = new TranslationInfo
            {
                QuestionText = "Какие три буквы были последними в «{0}»?",
                ModuleName = "Медленной математике",
            },

            // Small Circle
            // How much did the sequence shift by in {0}?
            // How much did the sequence shift by in Small Circle?
            [Question.SmallCircleShift] = new TranslationInfo
            {
                QuestionText = "Насколько сместилась последовательность в «{0}»?",
                ModuleName = "Маленьком круге",
            },
            // Which wedge made the different noise in the beginning of {0}?
            // Which wedge made the different noise in the beginning of Small Circle?
            [Question.SmallCircleWedge] = new TranslationInfo
            {
                QuestionText = "Какой сегмент круга издал другой звук в начале в «{0}»?",
                ModuleName = "Маленьком круге",
            },
            // Which color was {1} in the solution to {0}?
            // Which color was first in the solution to Small Circle?
            [Question.SmallCircleSolution] = new TranslationInfo
            {
                QuestionText = "Какой цвет был {1}-м в решении в «{0}»?",
                ModuleName = "Маленьком круге",
            },

            // Snooker
            // How many red balls were there at the start of {0}?
            // How many red balls were there at the start of Snooker?
            [Question.SnookerReds] = new TranslationInfo
            {
                QuestionText = "How many red balls were there at the start of «{0}»?",
            },

            // Snowflakes
            // Which snowflake was on the {1} button of {0}?
            // Which snowflake was on the top button of Snowflakes?
            [Question.SnowflakesDisplayedSnowflakes] = new TranslationInfo
            {
                QuestionText = "Какая снежинка была на {1} кнопке в «{0}»?",
                ModuleName = "Снежинках",
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
            [Question.SonicKnucklesSounds] = new TranslationInfo
            {
                QuestionText = "Какой звук воспроизводился, но не присутствовал в выбранной зоне в модуле «{0}»?",
                ModuleName = "Соник и Наклз",
            },
            // Which badnik was shown in {0}?
            // Which badnik was shown in Sonic & Knuckles?
            [Question.SonicKnucklesBadnik] = new TranslationInfo
            {
                QuestionText = "Какой бадник был показан в модуле «{0}»?",
                ModuleName = "Соник и Наклз",
            },
            // Which monitor was shown in {0}?
            // Which monitor was shown in Sonic & Knuckles?
            [Question.SonicKnucklesMonitor] = new TranslationInfo
            {
                QuestionText = "Какой монитор был показан в модуле «{0}»?",
                ModuleName = "Соник и Наклз",
            },

            // Sonic The Hedgehog
            // What was the {1} picture on {0}?
            // What was the first picture on Sonic The Hedgehog?
            [Question.SonicTheHedgehogPictures] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я картинка в «{0}»?",
                ModuleName = "Сонике",
            },
            // Which sound was played by the {1} screen on {0}?
            // Which sound was played by the Running Boots screen on Sonic The Hedgehog?
            [Question.SonicTheHedgehogSounds] = new TranslationInfo
            {
                QuestionText = "Какой звук воспроизводился на экране “{1}” в «{0}»?",
                ModuleName = "Сонике",
            },

            // Sorting
            // What positions were the last swap used to solve {0}?
            // What positions were the last swap used to solve Sorting?
            [Question.SortingLastSwap] = new TranslationInfo
            {
                QuestionText = "Какие позиции участвовали в последней замене чисел в «{0}»?",
                ModuleName = "Сортировке",
            },

            // Souvenir
            // What was the first module asked about in the other Souvenir on this bomb?
            // What was the first module asked about in the other Souvenir on this bomb?
            [Question.SouvenirFirstQuestion] = new TranslationInfo
            {
                QuestionText = "О каком модуле был первый вопрос на другом Сувенире?",
                ModuleName = "Сувенире",
            },

            // Space Traders
            // What was the maximum tax amount per vessel in {0}?
            // What was the maximum tax amount per vessel in Space Traders?
            [Question.SpaceTradersMaxTax] = new TranslationInfo
            {
                QuestionText = "Какой был максимальный налог за каждое судно в «{0}»?",
                ModuleName = "Космических торговцах",
            },

            // The Sphere
            // What was the {1} flashed color in {0}?
            // What was the first flashed color in The Sphere?
            [Question.SphereColors] = new TranslationInfo
            {
                QuestionText = "Какой цвет загорелся {1}-м в модуле «{0}»?",
                ModuleName = "Сфера",
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

            // Spelling Bee
            // What word was asked to be spelled in {0}?
            // What word was asked to be spelled in Spelling Bee?
            [Question.SpellingBeeWord] = new TranslationInfo
            {
                QuestionText = "What word was asked to be spelled in «{0}»?",
            },

            // Splitting The Loot
            // What bag was initially colored in {0}?
            // What bag was initially colored in Splitting The Loot?
            [Question.SplittingTheLootColoredBag] = new TranslationInfo
            {
                QuestionText = "Какой мешок был изначально окрашен в «{0}»?",
                ModuleName = "Разделении добычи",
            },

            // Spongebob Birthday Identification
            // Who was the {1} child displayed in {0}?
            // Who was the first child displayed in Spongebob Birthday Identification?
            [Question.SpongebobBirthdayIdentificationChildren] = new TranslationInfo
            {
                QuestionText = "Who was the {1} child displayed in «{0}»?",
            },

            // Stability
            // What was the color of the {1} lit LED in {0}?
            // What was the color of the first lit LED in Stability?
            [Question.StabilityLedColors] = new TranslationInfo
            {
                QuestionText = "What was the color of the {1} lit LED in «{0}»?",
            },
            // What was the identification number in {0}?
            // What was the identification number in Stability?
            [Question.StabilityIdNumber] = new TranslationInfo
            {
                QuestionText = "What was the identification number in «{0}»?",
            },

            // Stacked Sequences
            // Which of these is the length of a sequence in {0}?
            // Which of these is the length of a sequence in Stacked Sequences?
            [Question.StackedSequences] = new TranslationInfo
            {
                QuestionText = "Which of these is the length of a sequence in «{0}»?",
            },

            // Stars
            // What was the digit in the center of {0}?
            // What was the digit in the center of Stars?
            [Question.StarsCenter] = new TranslationInfo
            {
                QuestionText = "Какая цифра была в центре в модуле «{0}»?",
                ModuleName = "Звёзды",
            },

            // State of Aggregation
            // What was the element shown in {0}?
            // What was the element shown in State of Aggregation?
            [Question.StateOfAggregationElement] = new TranslationInfo
            {
                QuestionText = "Какой элемент был отображён в «{0}»?",
                ModuleName = "Агрегатном состоянии",
            },

            // Stellar
            // What was the {1} letter in {0}?
            // What was the Morse code letter in Stellar?
            [Question.StellarLetters] = new TranslationInfo
            {
                QuestionText = "What was the {1} letter in «{0}»?",
            },

            // Stupid Slots
            // What was the value of the {1} arrow in {0}?
            // What was the value of the top-left arrow in Stupid Slots?
            [Question.StupidSlotsValues] = new TranslationInfo
            {
                QuestionText = "What was the value of the {1} arrow in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-middle"] = "top-middle",
                    ["top-right"] = "top-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-middle"] = "bottom-middle",
                    ["bottom-right"] = "bottom-right",
                },
            },

            // Subscribe to Pewdiepie
            // How many subscribers does {1} have in {0}?
            // How many subscribers does PewDiePie have in Subscribe to Pewdiepie?
            [Question.SubscribeToPewdiepieSubCount] = new TranslationInfo
            {
                QuestionText = "Сколько подписчиков было у {1} в модуле «{0}»?",
                ModuleName = "Подпишись на Пьюдипая",
                FormatArgs = new Dictionary<string, string>
                {
                    ["PewDiePie"] = "Пьюдипая",
                    ["T-Series"] = "T-Series",
                },
            },

            // Subway
            // Which bread did the customer ask for in {0}?
            // Which bread did the customer ask for in Subway?
            [Question.SubwayBread] = new TranslationInfo
            {
                QuestionText = "Which bread did the customer ask for in {0}?",
            },
            // Which of these was not asked for in {0}?
            // Which of these was not asked for in Subway?
            [Question.SubwayItems] = new TranslationInfo
            {
                QuestionText = "Which of these was not asked for in {0}?",
            },

            // Sugar Skulls
            // What skull was shown on the {1} square in {0}?
            // What skull was shown on the top square in Sugar Skulls?
            [Question.SugarSkullsSkull] = new TranslationInfo
            {
                QuestionText = "What skull was shown on the {1} square in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
            },
            // Which skull {1} present in {0}?
            // Which skull was present in Sugar Skulls?
            [Question.SugarSkullsAvailability] = new TranslationInfo
            {
                QuestionText = "Which skull {1} present in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "was",
                    ["was not"] = "was not",
                },
            },

            // Superparsing
            // What was the displayed word in {0}?
            // What was the displayed word in Superparsing?
            [Question.SuperparsingDisplayed] = new TranslationInfo
            {
                QuestionText = "What was the displayed word in «{0}»?",
            },

            // The Switch
            // What color was the {1} LED on the {2} flip of {0}?
            // What color was the top LED on the first flip of The Switch?
            [Question.SwitchInitialColor] = new TranslationInfo
            {
                QuestionText = "Какого цвета был {1} светодиод при {2}-м повороте «{0}»?",
                ModuleName = "Переключателя",
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
            [Question.SwitchesInitialPosition] = new TranslationInfo
            {
                QuestionText = "Какое было начальное положение «{0}»?",
                ModuleName = "Переключателей",
            },

            // Switching Maze
            // What was the seed in {0}?
            // What was the seed in Switching Maze?
            [Question.SwitchingMazeSeed] = new TranslationInfo
            {
                QuestionText = "Какое было семя в «{0}»?",
                ModuleName = "Переключающемся лабиринте",
            },
            // What was the starting maze color in {0}?
            // What was the starting maze color in Switching Maze?
            [Question.SwitchingMazeColor] = new TranslationInfo
            {
                QuestionText = "Какой был начальный цвет лабиринта в «{0}»?",
                ModuleName = "Переключающемся лабиринте",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Синий",
                    ["Cyan"] = "Голубой",
                    ["Magenta"] = "Маджента",
                    ["Orange"] = "Оранжевый",
                    ["Red"] = "Красный",
                    ["White"] = "Белый",
                },
            },

            // Symbol Cycle
            // How many symbols were cycling on the {1} screen in {0}?
            // How many symbols were cycling on the left screen in Symbol Cycle?
            [Question.SymbolCycleSymbolCounts] = new TranslationInfo
            {
                QuestionText = "Сколько символов было на {1} экране в «{0}»?",
                ModuleName = "Символьном цикле",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "левом",
                    ["right"] = "правом",
                },
            },

            // Symbolic Coordinates
            // What was the {1} symbol in the {2} stage of {0}?
            // What was the left symbol in the first stage of Symbolic Coordinates?
            [Question.SymbolicCoordinateSymbols] = new TranslationInfo
            {
                QuestionText = "Какой был {1} символ на {2}-м этапе в «{0}»?",
                ModuleName = "Символьных координатах",
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
            [Question.SymbolicTashaFlashes] = new TranslationInfo
            {
                QuestionText = "Which button flashed {1} in the final sequence of «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["�ordinal"] = "�ordinal",
                },
            },
            // Which symbol was on the {1} button in {0}?
            // Which symbol was on the top button in Symbolic Tasha?
            [Question.SymbolicTashaSymbols] = new TranslationInfo
            {
                QuestionText = "Which symbol was on the {1} button in «{0}»?",
                FormatArgs = new Dictionary<string, string>
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

            // SYNC-125 [3]
            // What was displayed on the screen in the {1} stage of {0}?
            // What was displayed on the screen in the first stage of SYNC-125 [3]?
            [Question.Sync125_3Word] = new TranslationInfo
            {
                QuestionText = "What was displayed on the screen in stage {1} of «{0}»?",
            },

            // Synonyms
            // Which number was displayed on {0}?
            // Which number was displayed on Synonyms?
            [Question.SynonymsNumber] = new TranslationInfo
            {
                QuestionText = "Какое число было отображено в «{0}»?",
                ModuleName = "Синонимах",
            },

            // Sysadmin
            // What error code did you fix in {0}?
            // What error code did you fix in Sysadmin?
            [Question.SysadminFixedErrorCodes] = new TranslationInfo
            {
                QuestionText = "Какой код ошибки вы исправили в «{0}»?",
                ModuleName = "Сисадмине",
            },

            // Tap Code
            // What was the received word in {0}?
            // What was the received word in Tap Code?
            [Question.TapCodeReceivedWord] = new TranslationInfo
            {
                QuestionText = "Какое слово было передано в «{0}»?",
                ModuleName = "Нажимном коде",
            },

            // Tasha Squeals
            // What was the {1} flashed color in {0}?
            // What was the first flashed color in Tasha Squeals?
            [Question.TashaSquealsColors] = new TranslationInfo
            {
                QuestionText = "What was the {1} flashed color in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["Pink"] = "Pink",
                    ["Green"] = "Green",
                    ["Yellow"] = "Yellow",
                    ["Blue"] = "Blue",
                },
            },

            // Tasque Managing
            // Where was the starting position in {0}?
            // Where was the starting position in Tasque Managing?
            [Question.TasqueManagingStartingPos] = new TranslationInfo
            {
                QuestionText = "Where was the starting position in «{0}»?",
            },

            // The Tea Set
            // Which ingredient was displayed {1}, from left to right, in {0}?
            // Which ingredient was displayed first, from left to right, in The Tea Set?
            [Question.TeaSetDisplayedIngredients] = new TranslationInfo
            {
                QuestionText = "Which ingredient was displayed {1}, from left to right, in «{0}»?",
            },

            // Technical Keypad
            // What was the {1} displayed digit in {0}?
            // What was the first displayed digit in Technical Keypad?
            [Question.TechnicalKeypadDisplayedDigits] = new TranslationInfo
            {
                QuestionText = "Какая была {1}-я отображённая цифра в «{0}»?",
                ModuleName = "Технической клавиатуре",
            },

            // Ten-Button Color Code
            // What was the initial color of the {1} button in the {2} stage of {0}?
            // What was the initial color of the first button in the first stage of Ten-Button Color Code?
            [Question.TenButtonColorCodeInitialColors] = new TranslationInfo
            {
                QuestionText = "What was the initial color of the {1} button in the {2} stage of «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                    ["yellow"] = "yellow",
                },
            },

            // Tenpins
            // What was the {1} split in {0}?
            // What was the red split in Tenpins?
            [Question.TenpinsSplits] = new TranslationInfo
            {
                QuestionText = "What was the {1} split in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                },
            },

            // Tetriamonds
            // What colour triangle pulsed {1} in {0}?
            // What colour triangle pulsed first in Tetriamonds?
            [Question.TetriamondsPulsingColours] = new TranslationInfo
            {
                QuestionText = "What colour triangle pulsed {1} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first"] = "first",
                    ["second"] = "second",
                    ["third"] = "third",
                },
            },

            // Text Field
            // What was the displayed letter in {0}?
            // What was the displayed letter in Text Field?
            [Question.TextFieldDisplay] = new TranslationInfo
            {
                QuestionText = "Какая буква присутствовала на «{0}»?",
                ModuleName = "Поле из букв",
            },

            // Thinking Wires
            // What was the position from top to bottom of the first wire needing to be cut in {0}?
            // What was the position from top to bottom of the first wire needing to be cut in Thinking Wires?
            [Question.ThinkingWiresFirstWire] = new TranslationInfo
            {
                QuestionText = "What was the position from top to bottom of the first wire needing to be cut in «{0}»?",
            },
            // What color did the second valid wire to cut have to have in {0}?
            // What color did the second valid wire to cut have to have in Thinking Wires?
            [Question.ThinkingWiresSecondWire] = new TranslationInfo
            {
                QuestionText = "What color did the second valid wire to cut have to have in «{0}»?",
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
            [Question.ThinkingWiresDisplayNumber] = new TranslationInfo
            {
                QuestionText = "What was the display number in «{0}»?",
            },

            // Third Base
            // What was the display word in the {1} stage on {0}?
            // What was the display word in the first stage on Third Base?
            [Question.ThirdBaseDisplay] = new TranslationInfo
            {
                QuestionText = "Какое слово было на экране на {1}-м этапе в модуле «{0}»?",
                ModuleName = "А доцент тупой!",
            },

            // Tic Tac Toe
            // What was on the {1} button at the start of {0}?
            // What was on the top-left button at the start of Tic Tac Toe?
            [Question.TicTacToeInitialState] = new TranslationInfo
            {
                QuestionText = "Что было на {1} кнопке в начале игры в «{0}»?",
                ModuleName = "Крестиках-ноликах",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "верхней левой",
                    ["top-middle"] = "верхней центральной",
                    ["top-right"] = "верхней правой",
                    ["middle-left"] = "центральной левой",
                    ["middle-center"] = "центральной",
                    ["middle-right"] = "центральной правой",
                    ["bottom-left"] = "нижней левой",
                    ["bottom-middle"] = "нижней центральной",
                    ["bottom-right"] = "нижней правой",
                },
            },

            // Timezone
            // What was the {1} city in {0}?
            // What was the departure city in Timezone?
            [Question.TimezoneCities] = new TranslationInfo
            {
                QuestionText = "Какой был город {1} в «{0}»?",
                ModuleName = "Часовых поясах",
                FormatArgs = new Dictionary<string, string>
                {
                    ["departure"] = "отправления",
                    ["destination"] = "прибытия",
                },
            },

            // Tip Toe
            // Which of these squares was safe in row {1} in {0}?
            // Which of these squares was safe in row 9 in Tip Toe?
            [Question.TipToeSafeSquares] = new TranslationInfo
            {
                QuestionText = "Which of these squares was safe in row {1} in «{0}»?",
            },

            // Topsy Turvy
            // What was the word initially shown in {0}?
            // What was the word initially shown in Topsy Turvy?
            [Question.TopsyTurvyWord] = new TranslationInfo
            {
                QuestionText = "What was the word initially shown in «{0}»?",
            },

            // Touch Transmission
            // What was the transmitted word in {0}?
            // What was the transmitted word in Touch Transmission?
            [Question.TouchTransmissionWord] = new TranslationInfo
            {
                QuestionText = "What was the transmitted word in «{0}»?",
            },
            // In what order was the Braille read in {0}?
            // In what order was the Braille read in Touch Transmission?
            [Question.TouchTransmissionOrder] = new TranslationInfo
            {
                QuestionText = "In what order was the Braille read in «{0}»?",
            },

            // Trajectory
            // Which function did the {1} button perform in {0}?
            // Which function did the A button perform in Trajectory?
            [Question.TrajectoryButtonFunctions] = new TranslationInfo
            {
                QuestionText = "Which function did the {1} button perform in «{0}»?",
            },

            // Transmitted Morse
            // What was the {1} received message in {0}?
            // What was the first received message in Transmitted Morse?
            [Question.TransmittedMorseMessage] = new TranslationInfo
            {
                QuestionText = "What was the {1} received message in «{0}»?",
            },

            // Triamonds
            // What colour triangle pulsed {1} in {0}?
            // What colour triangle pulsed first in Triamonds?
            [Question.TriamondsPulsingColours] = new TranslationInfo
            {
                QuestionText = "What colour triangle pulsed {1} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first"] = "first",
                    ["second"] = "second",
                    ["third"] = "third",
                },
            },

            // Triple Term
            // Which of these was one of the passwords in {0}?
            // Which of these was one of the passwords in Triple Term?
            [Question.TripleTermPasswords] = new TranslationInfo
            {
                QuestionText = "Which of these was one of the passwords in «{0}»?",
            },

            // Turtle Robot
            // What was the {1} line you commented out in {0}?
            // What was the first line you commented out in Turtle Robot?
            [Question.TurtleRobotCodeLines] = new TranslationInfo
            {
                QuestionText = "Какую строку вы закомментировали {1}-й в «{0}»?",
                ModuleName = "Роботе черепашке",
            },

            // Two Bits
            // What was the {1} correct query response from {0}?
            // What was the first correct query response from Two Bits?
            [Question.TwoBitsResponse] = new TranslationInfo
            {
                QuestionText = "Какой был ответ на {1}-й запрос в «{0}»?",
                ModuleName = "Двух битах",
            },

            // Ultimate Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Ultimate Cipher?
            [Question.UltimateCipherScreen] = new TranslationInfo
            {
                QuestionText = "What was on the {1} screen on page {2} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["middle"] = "middle",
                    ["bottom"] = "bottom",
                },
            },

            // Ultimate Cycle
            // What was the {1} in {0}?
            // What was the message in Ultimate Cycle?
            [Question.UltimateCycleWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "message",
                    ["response"] = "response",
                },
            },

            // The Ultracube
            // What was the {1} rotation in {0}?
            // What was the first rotation in The Ultracube?
            [Question.UltracubeRotations] = new TranslationInfo
            {
                QuestionText = "Каким было {1}-е вращение «{0}»?",
                ModuleName = "Ультракуба",
            },

            // UltraStores
            // What was the {1} rotation in the {2} stage of {0}?
            // What was the first rotation in the first stage of UltraStores?
            [Question.UltraStoresSingleRotation] = new TranslationInfo
            {
                QuestionText = "What was the {1} rotation in the {2} stage of «{0}»?",
            },
            // What was the {1} rotation in the {2} stage of {0}?
            // What was the first rotation in the first stage of UltraStores?
            [Question.UltraStoresMultiRotation] = new TranslationInfo
            {
                QuestionText = "What was the {1} rotation in the {2} stage of «{0}»?",
            },

            // Uncolored Squares
            // What was the {1} color in reading order used in the first stage of {0}?
            // What was the first color in reading order used in the first stage of Uncolored Squares?
            [Question.UncoloredSquaresFirstStage] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й цвет в порядке чтения, использованный на первом этапе в «{0}»?",
                ModuleName = "Неокрашенных квадратах",
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
            [Question.UncoloredSwitchesInitialState] = new TranslationInfo
            {
                QuestionText = "What was the initial state of the switches in «{0}»?",
            },
            // What color was the {1} LED in reading order in {0}?
            // What color was the first LED in reading order in Uncolored Switches?
            [Question.UncoloredSwitchesLedColors] = new TranslationInfo
            {
                QuestionText = "What color was the {1} LED in reading order in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                    ["turquoise"] = "turquoise",
                    ["orange"] = "orange",
                    ["purple"] = "purple",
                    ["white"] = "white",
                    ["black"] = "black",
                },
            },

            // Unfair Cipher
            // What was the {1} received instruction in {0}?
            // What was the first received instruction in Unfair Cipher?
            [Question.UnfairCipherInstructions] = new TranslationInfo
            {
                QuestionText = "Какая {1}-я инструкция была зашифрована в «{0}»?",
                ModuleName = "Нечестном шифре",
            },

            // Unfair’s Revenge
            // What was the {1} decrypted instruction in {0}?
            // What was the first decrypted instruction in Unfair’s Revenge?
            [Question.UnfairsRevengeInstructions] = new TranslationInfo
            {
                QuestionText = "Какая {1}-я инструкция была зашифрована в «{0}»?",
                ModuleName = "Нечестной месте шифра",
            },

            // Unicode
            // What was the {1} submitted code in {0}?
            // What was the first submitted code in Unicode?
            [Question.UnicodeSortedAnswer] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й отправленный ответ в «{0}»?",
                ModuleName = "Юникоде",
            },

            // UNO!
            // What was the initial card in {0}?
            // What was the initial card in UNO!?
            [Question.UnoInitialCard] = new TranslationInfo
            {
                QuestionText = "Какая была начальная карта в «{0}»?",
            },

            // Unown Cipher
            // What was the {1} submitted letter in {0}?
            // What was the first submitted letter in Unown Cipher?
            [Question.UnownCipherAnswers] = new TranslationInfo
            {
                QuestionText = "Какая буква была отправлена {1}-й в «{0}»?",
                ModuleName = "Шифре Аноуна",
            },

            // USA Cycle
            // Which state was displayed in {0}?
            // Which state was displayed in USA Cycle?
            [Question.USACycleDisplayed] = new TranslationInfo
            {
                QuestionText = "Which state was displayed in «{0}»?",
            },

            // USA Maze
            // Which state did you depart from in {0}?
            // Which state did you depart from in USA Maze?
            [Question.USAMazeOrigin] = new TranslationInfo
            {
                QuestionText = "Which state did you depart from in «{0}»?",
            },

            // V
            // Which word {1} shown in {0}?
            // Which word was shown in V?
            [Question.VWords] = new TranslationInfo
            {
                QuestionText = "Какое слово {1} показано в «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "было",
                    ["was not"] = "не было",
                },
            },

            // Valves
            // What was the initial state of {0}?
            // What was the initial state of Valves?
            [Question.ValvesInitialState] = new TranslationInfo
            {
                QuestionText = "Какое было начальное состояние «{0}»?",
                ModuleName = "Клапанов",
            },

            // Varicolored Squares
            // What was the initially pressed color on {0}?
            // What was the initially pressed color on Varicolored Squares?
            [Question.VaricoloredSquaresInitialColor] = new TranslationInfo
            {
                QuestionText = "Какой был первый нажатый цвет в «{0}»?",
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
            [Question.VaricolourFlashWords] = new TranslationInfo
            {
                QuestionText = "What was the word of the {1} goal in «{0}»?",
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
            [Question.VaricolourFlashColors] = new TranslationInfo
            {
                QuestionText = "What was the colour of the {1} goal in «{0}»?",
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

            // Vcrcs
            // What was the word in {0}?
            // What was the word in Vcrcs?
            [Question.VcrcsWord] = new TranslationInfo
            {
                QuestionText = "What was the word in «{0}»?",
            },

            // Vectors
            // What was the color of the {1} vector in {0}?
            // What was the color of the first vector in Vectors?
            [Question.VectorsColors] = new TranslationInfo
            {
                QuestionText = "Какого цвета был {1} вектор в «{0}»?",
                ModuleName = "Векторах",
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
            [Question.VexillologyColors] = new TranslationInfo
            {
                QuestionText = "Какого цвета был {1}-й флагшток в «{0}»?",
                ModuleName = "Вексиллологии",
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
            [Question.VioletCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Фиолетовом шифре",
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
            [Question.VisualImpairmentColors] = new TranslationInfo
            {
                QuestionText = "Какой был целевой цвет на {1}-м этапе в «{0}»?",
                ModuleName = "Повреждённом зрении",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Синий",
                    ["Green"] = "Зелёный",
                    ["Red"] = "Красный",
                    ["White"] = "Белый",
                },
            },

            // Warning Signs
            // What was the displayed sign in {0}?
            // What was the displayed sign in Warning Signs?
            [Question.WarningSignsDisplayedSign] = new TranslationInfo
            {
                QuestionText = "Какой знак был показан в «{0}»?",
                ModuleName = "Предупреждающих знаках",
            },

            // WASD
            // What was the location displayed in {0}?
            // What was the location displayed in WASD?
            [Question.WasdDisplayedLocation] = new TranslationInfo
            {
                QuestionText = "Какая локация была показана в модуле «{0}»?",
            },

            // Wavetapping
            // What was the color on the {1} stage in {0}?
            // What was the color on the first stage in Wavetapping?
            [Question.WavetappingColors] = new TranslationInfo
            {
                QuestionText = "What was the color on the {1} stage in «{0}»?",
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
            [Question.WavetappingPatterns] = new TranslationInfo
            {
                QuestionText = "What was the correct pattern on the {1} stage in «{0}»?",
            },

            // The Weakest Link
            // Who did you eliminate in {0}?
            // Who did you eliminate in The Weakest Link?
            [Question.WeakestLinkElimination] = new TranslationInfo
            {
                QuestionText = "Who did you eliminate in «{0}»?",
            },
            // Who made it to the Money Phase with you in {0}?
            // Who made it to the Money Phase with you in The Weakest Link?
            [Question.WeakestLinkMoneyPhaseName] = new TranslationInfo
            {
                QuestionText = "Who made it to the Money Phase with you in «{0}»?",
            },
            // What ratio did {1} get in the Question Phase in {0}?
            // What ratio did Annie get in the Question Phase in The Weakest Link?
            [Question.WeakestLinkRatio] = new TranslationInfo
            {
                QuestionText = "What ratio did {1} get in the Question Phase in «{0}»?",
            },
            // What was {1}’s skill in {0}?
            // What was Annie’s skill in The Weakest Link?
            [Question.WeakestLinkSkill] = new TranslationInfo
            {
                QuestionText = "What was {1}’s skill in «{0}»?",
            },

            // What’s on Second
            // What was the display text in the {1} stage of {0}?
            // What was the display text in the first stage of What’s on Second?
            [Question.WhatsOnSecondDisplayText] = new TranslationInfo
            {
                QuestionText = "What was the display text in the {1} stage of «{0}»?",
            },
            // What was the display text color in the {1} stage of {0}?
            // What was the display text color in the first stage of What’s on Second?
            [Question.WhatsOnSecondDisplayColor] = new TranslationInfo
            {
                QuestionText = "What was the display text color in the {1} stage of «{0}»?",
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

            // White Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in White Cipher?
            [Question.WhiteCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Белом шифре",
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
            [Question.WhoOFDisplay] = new TranslationInfo
            {
                QuestionText = "What was the display in the {1} stage on «{0}»?",
            },

            // Who’s on First
            // What was the display in the {1} stage on {0}?
            // What was the display in the first stage on Who’s on First?
            [Question.WhosOnFirstDisplay] = new TranslationInfo
            {
                QuestionText = "Какое слово было на экране на {1}-м этапе в модуле «{0}»?",
                ModuleName = "Меня зовут Авас, а Вас",
            },

            // Who’s on Morse
            // What word was transmitted in the {1} stage on {0}?
            // What word was transmitted in the first stage on Who’s on Morse?
            [Question.WhosOnMorseTransmitDisplay] = new TranslationInfo
            {
                QuestionText = "Какое слово было передано на {1}-м этапе в модуле «{0}»?",
                ModuleName = "Меня зовут Морзе",
            },

            // The Wire
            // What was the color of the {1} dial in {0}?
            // What was the color of the top dial in The Wire?
            [Question.WireDialColors] = new TranslationInfo
            {
                QuestionText = "Какого цвета был {1} диск в модуле «{0}»?",
                ModuleName = "Провод",
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
            [Question.WireDisplayedNumber] = new TranslationInfo
            {
                QuestionText = "Какое было отображённое число в модуле «{0}»?",
                ModuleName = "Провод",
            },

            // Wire Ordering
            // What color was the {1} display from the left in {0}?
            // What color was the first display from the left in Wire Ordering?
            [Question.WireOrderingDisplayColor] = new TranslationInfo
            {
                QuestionText = "What color was the {1} display from the left in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["orange"] = "orange",
                    ["yellow"] = "yellow",
                    ["green"] = "green",
                    ["blue"] = "blue",
                    ["purple"] = "purple",
                    ["white"] = "white",
                    ["black"] = "black",
                },
            },
            // What number was on the {1} display from the left in {0}?
            // What number was on the first display from the left in Wire Ordering?
            [Question.WireOrderingDisplayNumber] = new TranslationInfo
            {
                QuestionText = "What number was on the {1} display from the left in «{0}»?",
            },
            // What color was the {1} wire from the left in {0}?
            // What color was the first wire from the left in Wire Ordering?
            [Question.WireOrderingWireColor] = new TranslationInfo
            {
                QuestionText = "What color was the {1} wire from the left in «{0}»?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["orange"] = "orange",
                    ["yellow"] = "yellow",
                    ["green"] = "green",
                    ["blue"] = "blue",
                    ["purple"] = "purple",
                    ["white"] = "white",
                    ["black"] = "black",
                },
            },

            // Wire Sequence
            // How many {1} wires were there in {0}?
            // How many red wires were there in Wire Sequence?
            [Question.WireSequenceColorCount] = new TranslationInfo
            {
                QuestionText = "Сколько было {1} проводов в «{0}»?",
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
            [Question.WolfGoatAndCabbageAnimals] = new TranslationInfo
            {
                QuestionText = "Что из этого {1} в модуле «{0}»?",
                ModuleName = "Волк, коза и капуста",
                FormatArgs = new Dictionary<string, string>
                {
                    ["present"] = "присутствовало",
                    ["not present"] = "отсутствовало",
                },
            },
            // What was the boat size in {0}?
            // What was the boat size in Wolf, Goat, and Cabbage?
            [Question.WolfGoatAndCabbageBoatSize] = new TranslationInfo
            {
                QuestionText = "Какого размера была лодка в модуле «{0}»?",
                ModuleName = "Волк, коза и капуста",
            },

            // Working Title
            // What was the label shown in {0}?
            // What was the label shown in Working Title?
            [Question.WorkingTitleLabel] = new TranslationInfo
            {
                QuestionText = "What was the label shown in «{0}»?",
            },

            // The Xenocryst
            // What was the color of the {1} flash in {0}?
            // What was the color of the first flash in The Xenocryst?
            [Question.Xenocryst] = new TranslationInfo
            {
                QuestionText = "What was the color of the {1} flash in «{0}»?",
            },

            // XmORse Code
            // What was the {1} displayed letter (in reading order) in {0}?
            // What was the first displayed letter (in reading order) in XmORse Code?
            [Question.XmORseCodeDisplayedLetters] = new TranslationInfo
            {
                QuestionText = "What was the {1} displayed letter (in reading order) in «{0}»?",
            },
            // What word did you decrypt in {0}?
            // What word did you decrypt in XmORse Code?
            [Question.XmORseCodeWord] = new TranslationInfo
            {
                QuestionText = "What word did you decrypt in «{0}»?",
            },

            // xobekuJ ehT
            // What song was played on {0}?
            // What song was played on xobekuJ ehT?
            [Question.XobekuJehTSong] = new TranslationInfo
            {
                QuestionText = "What song was played on «{0}»?",
            },

            // Yahtzee
            // What was the initial roll on {0}?
            // What was the initial roll on Yahtzee?
            [Question.YahtzeeInitialRoll] = new TranslationInfo
            {
                QuestionText = "Какой был первый бросок в «{0}»?",
                ModuleName = "Покере на костях",
                Answers = new Dictionary<string, string>
                {
                    ["Yahtzee"] = "Покер",
                    ["large straight"] = "Большой стрит",
                    ["small straight"] = "Малый стрит",
                    ["four of a kind"] = "Каре",
                    ["full house"] = "Фулхаус",
                    ["three of a kind"] = "Тройка",
                    ["two pairs"] = "Две пары",
                    ["pair"] = "Пара",
                },
            },

            // Yellow Arrows
            // What was the starting row letter in {0}?
            // What was the starting row letter in Yellow Arrows?
            [Question.YellowArrowsStartingRow] = new TranslationInfo
            {
                QuestionText = "Какая была буква у начальной строки в «{0}»?",
                ModuleName = "Жёлтых стрелках",
            },

            // The Yellow Button
            // What was the {1} color in {0}?
            // What was the first color in The Yellow Button?
            [Question.YellowButtonColors] = new TranslationInfo
            {
                QuestionText = "Какой был {1}-й цвет в «{0}»?",
                ModuleName = "Жёлтой кнопке",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Красный",
                    ["Yellow"] = "Жёлтый",
                    ["Green"] = "Зелёный",
                    ["Cyan"] = "Голубой",
                    ["Blue"] = "Синий",
                    ["Magenta"] = "Розовый",
                },
            },

            // Yellow Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Yellow Cipher?
            [Question.YellowCipherScreen] = new TranslationInfo
            {
                QuestionText = "Что было на {1} экране на {2}-й странице в «{0}»?",
                ModuleName = "Жёлтом шифре",
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
            [Question.ZeroZeroStarColors] = new TranslationInfo
            {
                QuestionText = "What color was the {1} star in «{0}»?",
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
            [Question.ZeroZeroStarPoints] = new TranslationInfo
            {
                QuestionText = "How many points were on the {1} star in «{0}»?",
            },
            // Where was the {1} square in {0}?
            // Where was the red square in Zero, Zero?
            [Question.ZeroZeroSquares] = new TranslationInfo
            {
                QuestionText = "Where was the {1} square in «{0}»?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["blue"] = "blue",
                },
            },

            // Zoni
            // What was the {1} word in {0}?
            // What was the first word in Zoni?
            [Question.ZoniWords] = new TranslationInfo
            {
                QuestionText = "Какое было {1}-е расшифрованное слово в модуле «{0}»?",
                ModuleName = "Zoni",
            },

        };
        #endregion

        public override string[] IntroTexts => Ut.NewArray(
            "Я вижу мёртвых сапёров.",     // “I see dead people.” (Sixth Sense)
            "Добро пожаловать... в настоящую бомбу.",     // “Welcome... to the real world.” (The Matrix)
            "Я собираюсь сделать бомбу, которую он не сможет обезвредить.",   // “I’m gonna make him an offer he can’t refuse.” (The Godfather)
            "Обезвредь её ещё раз, Сэм.",    // “Play it again, Sam.” (Casablanca) (misquote)
            "Луис, я думаю, это начало прекрасного взрыва.",   // “Louis, I think this is the beginning of a beautiful friendship.” (Casablanca)
            "За тебя, сапёр.",  // “Here’s looking at you, kid.” (Casablanca)
            "Я вижу тебя, сапёр.",
            "Эй. Я мог бы обезвредить эту бомбу ровно за десять секунд.",   // “Hey. I could clear the sky in ten seconds flat.” (MLP:FiM, Friendship is Magic - Part 1)
            "Давай, обезвредь мою бомбу.", // “Go ahead, make my day.” (Sudden Impact / Dirty Harry series)
            "Да пребудет с тобой бомба.",    // “May the Force be with you.” (Star Wars IV: A New Hope)
            "Люблю запах взрывов по утрам.",   // “I love the smell of napalm in the morning.” (Apocalypse Now)
            "Взорваться – значит никогда не просить прощения.",   // “Love means never having to say you’re sorry.” (Love Story)
            "Алло? Да, я сейчас обезвреживаю бомбу.",    // “E.T. phone home.” (E.T. the Extra-Terrestrial)
            "Бомб. Джеймс Бомб.",    // “Bond. James Bond.” (Dr. No / James Bond series)
            "Бомба тебе не по зубам!",   // “You can’t handle the truth!” (A Few Good Men)
            "Взорвать всех подозреваемых.",  // “Round up the usual suspects.” (Casablanca)
            "Тебе понадобится бомба побольше.", // “You’re gonna need a bigger boat.” (Jaws)
            "Бомбы – это как коробка шоколадных конфет. Никогда не знаешь, что попадётся.",    // “My mom always said life was like a box of chocolates. You never know what you’re gonna get.” (Forrest Gump)
            "Хьюстон, у нас бомба.",   // “Houston, we have a problem.” (Apollo 13)
            "Элементарно, мой дорогой эксперт.",  // “Elementary, my dear Watson.” (Sherlock Holmes) (misquote)
            "Забудь об этом, Джейк, это КТАНЕ.",     // “Forget it, Jake, it’s Chinatown.” (Chinatown)
            "Я всегда полагался на компетентность экспертов.",    // “I’ve always depended on the kindness of strangers.” (A Streetcar Named Desire)
            "Бомба. Взорванная, а не обезвреженная.",   // “A martini. Shaken, not stirred.” (Diamonds Are Forever (novel) / James Bond)
            "Я король бомбы!",    // “I’m the king of the world!” (Titanic)
            "Взорви меня, Скотти.",  // “Beam me up, Scotty!” (Star Trek misquote)
            "Ябба- дабба- бум!",    // “Yabba dabba doo!” (Flintstones)
            "Эта бомба самоуничтожится через пять секунд.",    // “This tape will self-destruct in five seconds.” (Mission: Impossible)
            "Обезвреживание бесполезно.",  // “Resistance is futile.” (Star Trek: The Next Generation)
            "Это твой окончательный ответ?",   // direct quote (Who Wants to be a Millionaire?)
            "Лучший друг бомбы – это её сапёр.", // “A man’s best friend is his dog.” (attorney George Graham Vest, 1870 Warrensburg)
            "Держи своих экспертов близко, но свою бомбу – ещё ближе.",   // “Keep your friends close and your enemies closer.” (The Prince / Machiavelli)
            "Пристегните ремни безопасности. Это будет взрывная ночь.",   // “Fasten your seat belts, it’s going to be a bumpy night.” (All About Eve)
            "Покажи мне модули!", // “Show me the money!” (Jerry Maguire)
            "У нас всегда будут батарейки.", // “We’ll always have Paris.” (Casablanca)
            "Поздоровайся с моей маленькой бомбочкой.", // “Say hello to my little friend!” (Scarface)
            "Ты сапёр, Гарри.", // “You’re a wizard, Harry.” (Harry Potter and the Philosopher’s Stone)
            "Мне жаль, Дэйв. Боюсь, я не смогу это обезвредить.", // “I’m sorry, Dave. I’m afraid I can’t do that.” (2001: A Space Odyssey)
            "Ты либо умрешь сапёром, либо проживешь достаточно долго, чтобы увидеть, как становишься экспертом.", // “Well, I guess you either die a hero or you live long enough to see yourself become the villain.” (The Dark Knight)
            "Это не обезвреживание. Это взрыв... со стилем.",    // “This isn’t flying. This is falling... with style.” (Toy Story)
            "Не могли бы Вы описать модуль, сэр?",  // “Could you describe the ruckus, sir?” (The Breakfast Club)
            "Вам нужны виджеты? У меня есть двадцать.",  // “You want thingamabobs? I got twenty.” (The Little Mermaid)
            "Скажи мне корпус бомбы ещё раз, чёрт возьми.",  // “Say what one more goddamn time.” (Pulp Fiction)
            "Как вам нравятся эти модули?",    // “How do you like them apples?” (Good Will Hunting)
            "Представляем: Двухъярусную... Бомбу!",   // “Introducing: The Double... Decker... Couch!” (The LEGO Movie)
            "Вы что, перепутали провода?", // “Have you got your lions crossed?” (The Lion King)
            "Не перепутай провода.",   // “Don’t cross the streams.” (Ghostbusters)
            "Хотите услышать самый раздражающий взрыв в мире?", // “Wanna hear the most annoying sound in the world?” (Dumb & Dumber)
            "Руководства? Там, куда мы идём, они нам не нужны.",   // “Roads? Where we’re going, we don’t need roads.” (Back to the Future)
            "На достаточно длинном таймлайне выживаемость всех людей упадёт до нуля.", // direct quote (Fight Club (novel))
            "Это твоя бомба, и она закончится минута в минуту.", // “This is your life and it’s ending one minute at a time.” (Fight Club)
            "Первое правило обезвреживания заключается в том, что вы продолжаете говорить об обезвреживании.",    // “The first rule of Fight Club is, you don’t talk about Fight Club.” (Fight Club)
            "Что ж, вот ещё одна приятная неприятность, в которую ты меня втянул!",     // direct quote (Sons of the Desert / Oliver Hardy)
            "Ты ведь знаешь, как обезвреживать, правда, Стив? Ты просто соединяешь провода вместе и режешь.",  // “You know how to whistle, don’t you Steve? You just put your lips together, and blow.” (To Have And Have Not)
            "Госпожа Сапёр, вы пытаетесь меня обезвредить. Не так ли?",    // “Mrs. Robinson, you’re trying to seduce me. Aren’t you?” (The Graduate)
            "Мы обезвреживаем бомбы.",  // “We rob banks.” (Bonnie and Clyde)
            "Кто-то подложил нам бомбу.",  // direct quote (Zero Wing)
            "Люк, я твой эксперт.", // “Luke, I am your father.” (Star Wars V: The Empire Strikes Back) (misquote)
            "Всем известно, что лучший способ обучения – это обучение под сильным давлением, угрожающим жизни.", // direct quote (Spider-Man: Into the Spider-Verse)
            "Она должна быть примерно на 20% более взрывоопасной.", // “It needs to be about 20 percent cooler.” (MLP:FiM, Suited for Success)
            "То же самое, что мы делаем каждый вечер, эксперт. Попробуем обезвредить бомбу!", // “The same thing we do every night, Pinky. Try to take over the world!” (Pinky and the Brain)
            "Кто-нибудь заказывал жареного сапёра?", // “Anybody order fried sauerkraut?” (Once Upon a Time in Hollywood)
            "У меня есть несколько сапёров, которых нужно разнести в пух и прах!", // “I’ve got some children I need to make into corpses!” (Gravity Falls, Weirdmageddon 3: Take Back The Falls)
            "Я представляю себе такой взрыв, что это больше похоже на воспоминания.", // “I imagine death so much it feels more like a memory.” (Hamilton)
            "Я – неизбежность.", // direct quote (Avengers: Endgame)
            "Бойтесь, бегите! Бомбы все равно взорвутся.", // “Dread it, run from it, destiny still arrives.” (Avengers: Infinity War)
            "Со временем вы узнаете, каково это – проигрывать. Так отчаянно чувствовать, что ты прав, но все равно получать ошибки.", // “In time, you will know what it’s like to lose. To feel so desperately that you’re right, yet to fail all the same.” (Avengers: Infinity War)
            "Я могу заниматься этим целый день.", // direct quote (Captain America: Civil War)
            "Там... есть... ЧЕТЫРЕ! БОМБЫ!!!", // “There... are... FOUR! LIGHTS!!!” (Star Trek TNG, Chain of Command)
            "Это прекрасная вещь – детонация бомб.", // “It’s a beautiful thing, the destruction of words.” (1984)
            "Я есть сапёр.", // “Ich bin ein Berliner”, John F. Kennedy, 1963
            "Не спрашивай двойную бомбу, как решать бомбу-Центурион!", // Ask not the sparrow how the eagle soars! (Kill la Kill)
            "Кто-то считает себя слишком умным для меня. Они все так думают поначалу." // Someone thinks they’re too clever for us. They all think that at first. (Invincible)
        );
    }
}
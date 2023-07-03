using System.Collections.Generic;

namespace Souvenir
{
    public class Translation_ja : Translation
    {
        public override string Ordinal(int number) => number.ToString();
        public override string FormatModuleName(string moduleName, bool addSolveCount, int numSolved, bool addThe) => addSolveCount
            ? string.Format("{1}番目に解除された{0}", moduleName, Ordinal(numSolved))
            : addThe ? "\u00a0" + moduleName : moduleName;
        public override int DefaultFontIndex => 8;
        public override float LineSpacing => 0.7f;

        #region Translatable strings
        public override Dictionary<Question, TranslationInfo> Translations => new Dictionary<Question, TranslationInfo>
        {
            // 1000 Words
            // What was the {1} word shown in {0}?
            // What was the first word shown in 1000 Words?
            [Question._1000WordsWords] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の単語は何？",
            },

            // 100 Levels of Defusal
            // What was the {1} displayed letter in {0}?
            // What was the first displayed letter in 100 Levels of Defusal?
            [Question._100LevelsOfDefusalLetters] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に表示された文字は何？",
            },

            // 1D Chess
            // What was {1} in {0}?
            // What was your first move in 1D Chess?
            [Question._1DChessMoves] = new TranslationInfo
            {
                QuestionText = "{0}で{1}はどれだったか？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["your first move"] = "あなたの最初の移動",
                    ["Rustmate’s first move"] = "Rustmateの最初の移動",
                    ["your second move"] = "あなたの2回目の移動",
                    ["Rustmate’s second move"] = "Rustmateの2回目の移動e",
                    ["your third move"] = "your3回目の移動",
                    ["Rustmate’s third move"] = "Rustmateの3回目の移動",
                    ["your fourth move"] = "あなたの4回目の移動",
                    ["Rustmate’s fourth move"] = "Rustmateの4回目の移動",
                    ["your fifth move"] = "あなたの5回目の移動",
                    ["Rustmate’s fifth move"] = "Rustmateの5回目の移動",
                    ["your sixth move"] = "あなたの6回目の移動",
                    ["Rustmate’s sixth move"] = "Rustmateの6回目の移動",
                    ["your seventh move"] = "あなたの7回目の移動",
                    ["Rustmate’s seventh move"] = "Rustmateの7回目の移動",
                    ["your eighth move"] = "あなたの8回目の移動",
                    ["Rustmate’s eighth move"] = "Rustmateの8回目の移動",
                },
            },

            // 3D Maze
            // What were the markings in {0}?
            // What were the markings in 3D Maze?
            [Question._3DMazeMarkings] = new TranslationInfo
            {
                QuestionText = "{0}の迷路の文字は何？",
            },
            // What was the cardinal direction in {0}?
            // What was the cardinal direction in 3D Maze?
            [Question._3DMazeBearing] = new TranslationInfo
            {
                QuestionText = "{0}のゴールの方向はどこ？",
                Answers = new Dictionary<string, string>
                {
                    ["North"] = "北",
                    ["South"] = "南",
                    ["West"] = "西",
                    ["East"] = "東",
                },
            },

            // 3D Tap Code
            // What was the received word in {0}?
            // What was the received word in 3D Tap Code?
            [Question._3DTapCodeWord] = new TranslationInfo
            {
                QuestionText = "{0}で受信した単語は？",
            },

            // 3D Tunnels
            // What was the {1} goal node in {0}?
            // What was the first goal node in 3D Tunnels?
            [Question._3DTunnelsTargetNode] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のゴールの目印は何？",
            },

            // 3 LEDs
            // What was the initial state of the LEDs in {0} (in reading order)?
            // What was the initial state of the LEDs in 3 LEDs (in reading order)?
            [Question._3LEDsInitialState] = new TranslationInfo
            {
                QuestionText = "{0}の初期のLEDの状態は(読み順)？",
            },

            // 7
            // What was the {1} channel’s initial value in {0}?
            // What was the red channel’s initial value in 7?
            [Question._7InitialValues] = new TranslationInfo
            {
                QuestionText = "{0}の{1}チャンネルの初期値は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                },
            },
            // What LED color was shown in stage {1} of {0}?
            // What LED color was shown in stage 1 of 7?
            [Question._7LedColors] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}のLEDの色は何？",
            },

            // 9-Ball
            // What was the number of ball {1} in {0}?
            // What was the number of ball A in 9-Ball?
            [Question._9BallLetters] = new TranslationInfo
            {
                QuestionText = "{0}のボール{1}の数字は？",
            },
            // What was the letter of ball {1} in {0}?
            // What was the letter of ball 2 in 9-Ball?
            [Question._9BallNumbers] = new TranslationInfo
            {
                QuestionText = "{0}のボール{1}の文字は？",
            },

            // Abyss
            // What was the {1} character displayed on {0}?
            // What was the first character displayed on Abyss?
            [Question.AbyssSeed] = new TranslationInfo
            {
                QuestionText = "What was the {1} character displayed on {0}?",
            },

            // Accumulation
            // What was the background color on the {1} stage in {0}?
            // What was the background color on the first stage in Accumulation?
            [Question.AccumulationBackgroundColor] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}の背景の色は何？",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "青",
                    ["Brown"] = "茶",
                    ["Green"] = "緑",
                    ["Grey"] = "灰",
                    ["Lime"] = "黄緑",
                    ["Orange"] = "オレンジ",
                    ["Pink"] = "ピンク",
                    ["Red"] = "赤",
                    ["White"] = "白",
                    ["Yellow"] = "黄",
                },
            },
            // What was the border color in {0}?
            // What was the border color in Accumulation?
            [Question.AccumulationBorderColor] = new TranslationInfo
            {
                QuestionText = "{0}の境界線の色は何？",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "青",
                    ["Brown"] = "茶",
                    ["Green"] = "緑",
                    ["Grey"] = "灰",
                    ["Lime"] = "黄緑",
                    ["Orange"] = "オレンジ",
                    ["Pink"] = "ピンク",
                    ["Red"] = "赤",
                    ["White"] = "白",
                    ["Yellow"] = "黄",
                },
            },

            // Adventure Game
            // Which item was the {1} correct item you used in {0}?
            // Which item was the first correct item you used in Adventure Game?
            [Question.AdventureGameCorrectItem] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に正しく使用したアイテムはどれ？",
            },
            // What enemy were you fighting in {0}?
            // What enemy were you fighting in Adventure Game?
            [Question.AdventureGameEnemy] = new TranslationInfo
            {
                QuestionText = "{0}でどの敵と戦ったか？",
            },

            // Affine Cycle
            // What was the {1} in {0}?
            // What was the message in Affine Cycle?
            [Question.AffineCycleWord] = new TranslationInfo
            {
                QuestionText = "{0}のメッセージとは何だった？",
            },

            // Alfa-Bravo
            // Which letter was pressed in {0}?
            // Which letter was pressed in Alfa-Bravo?
            [Question.AlfaBravoPressedLetter] = new TranslationInfo
            {
                QuestionText = "{0}でどの文字を押した？",
            },
            // Which letter was to the left of the pressed one in {0}?
            // Which letter was to the left of the pressed one in Alfa-Bravo?
            [Question.AlfaBravoLeftPressedLetter] = new TranslationInfo
            {
                QuestionText = "{0}で押した文字の左にあった文字は何？",
            },
            // Which letter was to the right of the pressed one in {0}?
            // Which letter was to the right of the pressed one in Alfa-Bravo?
            [Question.AlfaBravoRightPressedLetter] = new TranslationInfo
            {
                QuestionText = "{0}押した文字の右にあった文字は何？",
            },
            // What was the last digit on the small display in {0}?
            // What was the last digit on the small display in Alfa-Bravo?
            [Question.AlfaBravoDigit] = new TranslationInfo
            {
                QuestionText = "{0}の小さなディスプレーの最後の数字は何？",
            },

            // Algebra
            // What was the first equation in {0}?
            // What was the first equation in Algebra?
            [Question.AlgebraEquation1] = new TranslationInfo
            {
                QuestionText = "{0}の最初の方程式は何？",
            },
            // What was the second equation in {0}?
            // What was the second equation in Algebra?
            [Question.AlgebraEquation2] = new TranslationInfo
            {
                QuestionText = "{0}の二番目の方程式は何？",
            },

            // Algorithmia
            // Which position was the {1} position in {0}?
            // Which position was the starting position in Algorithmia?
            [Question.AlgorithmiaPositions] = new TranslationInfo
            {
                QuestionText = "{0}の{1}位置は？",
            },
            // What was the color of the colored bulb in {0}?
            // What was the color of the colored bulb in Algorithmia?
            [Question.AlgorithmiaColor] = new TranslationInfo
            {
                QuestionText = "{0}の色付き電球の色は？",
            },
            // Which number was present in the seed in {0}?
            // Which number was present in the seed in Algorithmia?
            [Question.AlgorithmiaSeed] = new TranslationInfo
            {
                QuestionText = "{0}のシード内にある数字は？",
            },

            // Alphabetical Ruling
            // What was the letter displayed in the {1} stage of {0}?
            // What was the letter displayed in the first stage of Alphabetical Ruling?
            [Question.AlphabeticalRulingLetter] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で表示された文字は何？",
            },
            // What was the number displayed in the {1} stage of {0}?
            // What was the number displayed in the first stage of Alphabetical Ruling?
            [Question.AlphabeticalRulingNumber] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で表示された数字は何？",
            },

            // Alphabet Tiles
            // What was the {1} letter shown during the cycle in {0}?
            // What was the first letter shown during the cycle in Alphabet Tiles?
            [Question.AlphabetTilesCycle] = new TranslationInfo
            {
                QuestionText = "{0}のサイクルで{1}番目に表示された文字は？",
            },
            // What was the missing letter in {0}?
            // What was the missing letter in Alphabet Tiles?
            [Question.AlphabetTilesMissingLetter] = new TranslationInfo
            {
                QuestionText = "{0}で隠されている文字は何？",
            },

            // Alpha-Bits
            // What character was displayed on the {1} screen on the {2} in {0}?
            // What character was displayed on the first screen on the left in Alpha-Bits?
            [Question.AlphaBitsDisplayedCharacters] = new TranslationInfo
            {
                QuestionText = "{0}で{2}の{1}つめの画面に表示されているキャラクターは何？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "左",
                },
            },

            // Ángel Hernández
            // What letter was shown by the raised buttons on the {1} stage on {0}?
            // What letter was shown by the raised buttons on the first stage on Ángel Hernández?
            [Question.AngelHernandezMainLetter] = new TranslationInfo
            {
                QuestionText = "What letter was shown by the raised buttons on the {1} stage on {0}?",
            },

            // Arithmelogic
            // What was the symbol on the submit button in {0}?
            // What was the symbol on the submit button in Arithmelogic?
            [Question.ArithmelogicSubmit] = new TranslationInfo
            {
                QuestionText = "{0}のsubmitボタンの目印は何？",
            },
            // Which number was selectable, but not the solution, in the {1} screen on {0}?
            // Which number was selectable, but not the solution, in the left screen on Arithmelogic?
            [Question.ArithmelogicNumbers] = new TranslationInfo
            {
                QuestionText = "{0}の{1}の画面で選択できる、答え以外の数字はいくつ？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "左",
                    ["middle"] = "中央",
                    ["right"] = "右",
                },
            },

            // ASCII Maze
            // What was the {1} character displayed on {0}?
            // What was the first character displayed on ASCII Maze?
            [Question.ASCIIMazeCharacters] = new TranslationInfo
            {
                QuestionText = "What was the {1} character displayed on {0}？",
            },

            // A Square
            // Which of these was an index color in {0}?
            // Which of these was an index color in A Square?
            [Question.ASquareIndexColors] = new TranslationInfo
            {
                QuestionText = "{0}で一致した色は？",
            },
            // Which color was submitted {1} in {0}?
            [Question.ASquareCorrectColors] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に送信した色は？",
            },

            // The Azure Button
            // What was T in {0}?
            // What was T in The Azure Button?
            [Question.AzureButtonT] = new TranslationInfo
            {
                QuestionText = "What was T in {0}?",
            },
            // Which of these cards was shown in Stage 1, but not T, in {0}?
            // Which of these cards was shown in Stage 1, but not T, in The Azure Button?
            [Question.AzureButtonNotT] = new TranslationInfo
            {
                QuestionText = "Which of these cards was shown in Stage 1, but not T, in {0}?",
            },
            // What was M in {0}?
            // What was M in The Azure Button?
            [Question.AzureButtonM] = new TranslationInfo
            {
                QuestionText = "What was M in {0}?",
            },
            // What was the {1} direction in the decoy arrow in {0}?
            // What was the first direction in the decoy arrow in The Azure Button?
            [Question.AzureButtonDecoyArrowDirection] = new TranslationInfo
            {
                QuestionText = "What was the {1} direction in the decoy arrow in {0}?",
            },
            // What was the {1} direction in the {2} non-decoy arrow in {0}?
            // What was the first direction in the first non-decoy arrow in The Azure Button?
            [Question.AzureButtonNonDecoyArrowDirection] = new TranslationInfo
            {
                QuestionText = "What was the {1} direction in the {2} non-decoy arrow in {0}?",
            },

            // Bamboozled Again
            // What color was the {1} correct button in {0}?
            // What color was the first correct button in Bamboozled Again?
            [Question.BamboozledAgainButtonColor] = new TranslationInfo
            {
                QuestionText = "What color was the {1} correct button in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Lime"] = "黄緑",
                    ["Green"] = "緑",
                    ["Jade"] = "翡翠",
                    ["Cyan"] = "シアン",
                    ["Azure"] = "空",
                    ["Blue"] = "青",
                    ["Violet"] = "紫",
                    ["Magenta"] = "マゼンタ",
                    ["Rose"] = "薔薇",
                    ["White"] = "白",
                    ["Grey"] = "灰",
                    ["Black"] = "黒",
                },
            },
            // What was the text on the {1} correct button in {0}?
            // What was the text on the first correct button in Bamboozled Again?
            [Question.BamboozledAgainButtonText] = new TranslationInfo
            {
                QuestionText = "What was the text on the {1} correct button in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["THE LETTER"] = "THE LETTER",
                    ["ONE LETTER"] = "ONE LETTER",
                    ["THE COLOUR"] = "THE COLOUR",
                    ["ONE COLOUR"] = "ONE COLOUR",
                    ["THE PHRASE"] = "THE PHRASE",
                    ["ONE PHRASE"] = "ONE PHRASE",
                    ["ALPHA"] = "ALPHA",
                    ["BRAVO"] = "BRAVO",
                    ["CHARLIE"] = "CHARLIE",
                    ["DELTA"] = "DELTA",
                    ["ECHO"] = "ECHO",
                    ["GOLF"] = "GOLF",
                    ["KILO"] = "KILO",
                    ["QUEBEC"] = "QUEBEC",
                    ["TANGO"] = "TANGO",
                    ["WHISKEY"] = "WHISKEY",
                    ["VICTOR"] = "VICTOR",
                    ["YANKEE"] = "YANKEE",
                    ["ECHO ECHO"] = "ECHO ECHO",
                    ["E THEN E"] = "E THEN E",
                    ["ALPHA PAPA"] = "ALPHA PAPA",
                    ["PAPA ALPHA"] = "PAPA ALPHA",
                    ["PAPHA ALPA"] = "PAPHA ALPA",
                    ["T GOLF"] = "T GOLF",
                    ["TANGOLF"] = "TANGOLF",
                    ["WHISKEE"] = "WHISKEE",
                    ["WHISKY"] = "WHISKY",
                    ["CHARLIE C"] = "CHARLIE C",
                    ["C CHARLIE"] = "C CHARLIE",
                    ["YANGO"] = "YANGO",
                    ["DELTA NEXT"] = "DELTA NEXT",
                    ["CUEBEQ"] = "CUEBEQ",
                    ["MILO"] = "MILO",
                    ["KI LO"] = "KI LO",
                    ["HI-LO"] = "HI-LO",
                    ["VVICTOR"] = "VVICTOR",
                    ["VICTORR"] = "VICTORR",
                    ["LIME BRAVO"] = "LIME BRAVO",
                    ["BLUE BRAVO"] = "BLUE BRAVO",
                    ["G IN JADE"] = "G IN JADE",
                    ["G IN ROSE"] = "G IN ROSE",
                    ["BLUE IN RED"] = "BLUE IN RED",
                    ["YES BUT NO"] = "YES BUT NO",
                    ["COLOUR"] = "COLOUR",
                    ["MESSAGE"] = "MESSAGE",
                    ["CIPHER"] = "CIPHER",
                    ["BUTTON"] = "BUTTON",
                    ["TWO BUTTONS"] = "TWO BUTTONS",
                    ["SIX BUTTONS"] = "SIX BUTTONS",
                    ["I GIVE UP"] = "I GIVE UP",
                    ["ONE ELEVEN"] = "ONE ELEVEN",
                    ["ONE ONE ONE"] = "ONE ONE ONE",
                    ["THREE ONES"] = "THREE ONES",
                    ["WHAT?"] = "WHAT?",
                    ["THIS?"] = "THIS?",
                    ["THAT?"] = "THAT?",
                    ["BLUE!"] = "BLUE!",
                    ["ECHO!"] = "ECHO!",
                    ["BLANK"] = "BLANK",
                    ["BLANK?!"] = "BLANK?!",
                    ["NOTHING"] = "NOTHING",
                    ["YELLOW TEXT"] = "YELLOW TEXT",
                    ["BLACK TEXT?"] = "BLACK TEXT?",
                    ["QUOTE V"] = "QUOTE V",
                    ["END QUOTE"] = "END QUOTE",
                    ["\"QUOTE K\""] = "\"QUOTE K\"",
                    ["IN RED"] = "IN RED",
                    ["ORANGE"] = "ORANGE",
                    ["IN YELLOW"] = "IN YELLOW",
                    ["LIME"] = "LIME",
                    ["IN GREEN"] = "IN GREEN",
                    ["JADE"] = "JADE",
                    ["IN CYAN"] = "IN CYAN",
                    ["AZURE"] = "AZURE",
                    ["IN BLUE"] = "IN BLUE",
                    ["VIOLET"] = "VIOLET",
                    ["IN MAGENTA"] = "IN MAGENTA",
                    ["ROSE"] = "ROSE",
                },
            },
            // What was the {1} decrypted text on the display in {0}?
            // What was the first decrypted text on the display in Bamboozled Again?
            [Question.BamboozledAgainDisplayTexts1] = new TranslationInfo
            {
                QuestionText = "What was the {1} decrypted text on the display in {0}？",
            },
            // What was the {1} decrypted text on the display in {0}?
            // What was the first decrypted text on the display in Bamboozled Again?
            [Question.BamboozledAgainDisplayTexts2] = new TranslationInfo
            {
                QuestionText = "What was the {1} decrypted text on the display in {0}？",
            },
            // What color was the {1} text on the display in {0}?
            // What color was the first text on the display in Bamboozled Again?
            [Question.BamboozledAgainDisplayColor] = new TranslationInfo
            {
                QuestionText = "What color was the {1} text on the display in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Lime"] = "黄緑",
                    ["Green"] = "緑",
                    ["Jade"] = "翡翠",
                    ["Cyan"] = "シアン",
                    ["Azure"] = "空",
                    ["Blue"] = "青",
                    ["Violet"] = "紫",
                    ["Magenta"] = "マゼンタ",
                    ["Rose"] = "薔薇",
                    ["White"] = "白",
                    ["Grey"] = "灰",
                },
            },

            // Bamboozling Button
            // What color was the button in the {1} stage of {0}?
            // What color was the button in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonColor] = new TranslationInfo
            {
                QuestionText = "What color was the button in the {1} stage of {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Lime"] = "黄緑",
                    ["Green"] = "緑",
                    ["Jade"] = "翡翠",
                    ["Cyan"] = "シアン",
                    ["Azure"] = "空",
                    ["Blue"] = "青",
                    ["Violet"] = "紫",
                    ["Magenta"] = "マゼンタ",
                    ["Rose"] = "薔薇",
                    ["White"] = "白",
                    ["Grey"] = "灰",
                    ["Black"] = "黒",
                },
            },
            // What was the {2} label on the button in the {1} stage of {0}?
            // What was the top label on the button in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonLabel] = new TranslationInfo
            {
                QuestionText = "What was the {2} label on the button in the {1} stage of {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "上",
                },
            },
            // What was the {2} display in the {1} stage of {0}?
            // What was the first display in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonDisplay] = new TranslationInfo
            {
                QuestionText = "What was the {2} display in the {1} stage of {0}？",
            },
            // What was the color of the {2} display in the {1} stage of {0}?
            // What was the color of the first display in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonDisplayColor] = new TranslationInfo
            {
                QuestionText = "What was the color of the {2} display in the {1} stage of {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Lime"] = "黄緑",
                    ["Green"] = "緑",
                    ["Jade"] = "翡翠",
                    ["Cyan"] = "シアン",
                    ["Azure"] = "空",
                    ["Blue"] = "青",
                    ["Violet"] = "紫",
                    ["Magenta"] = "マゼンタ",
                    ["Rose"] = "薔薇",
                    ["White"] = "白",
                    ["Grey"] = "灰",
                },
            },

            // Bakery
            // Which menu item was present in {0}?
            // Which menu item was present in Bakery?
            [Question.BakeryItems] = new TranslationInfo
            {
                QuestionText = "Which menu item was present in {0}？",
            },

            // Barcode Cipher
            // What was the screen number in {0}?
            // What was the screen number in Barcode Cipher?
            [Question.BarcodeCipherScreenNumber] = new TranslationInfo
            {
                QuestionText = "What was the screen number in {0}？",
            },
            // What was the edgework represented by the {1} barcode in {0}?
            // What was the edgework represented by the first barcode in Barcode Cipher?
            [Question.BarcodeCipherBarcodeEdgework] = new TranslationInfo
            {
                QuestionText = "What was the edgework represented by the {1} barcode in {0}？",
            },
            // What was the answer for the {1} barcode in {0}?
            // What was the answer for the first barcode in Barcode Cipher?
            [Question.BarcodeCipherBarcodeAnswers] = new TranslationInfo
            {
                QuestionText = "What was the answer for the {1} barcode in {0}？",
            },

            // Bartending
            // Which ingredient was in the {1} position on {0}?
            // Which ingredient was in the first position on Bartending?
            [Question.BartendingIngredients] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目の位置にあった材料は？",
            },

            // Big Circle
            // What color was {1} in the solution to {0}?
            // What color was first in the solution to Big Circle?
            [Question.BigCircleColors] = new TranslationInfo
            {
                QuestionText = "{0}の解法において{1}番目の色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Magenta"] = "マゼンタ",
                    ["White"] = "白",
                    ["Black"] = "黒",
                },
            },

            // Binary LEDs
            // At which numeric value did you cut the correct wire in {0}?
            // At which numeric value did you cut the correct wire in Binary LEDs?
            [Question.BinaryLEDsValue] = new TranslationInfo
            {
                QuestionText = "{0}でどの数字の時に正しいワイヤーを切った？",
            },

            // Binary Shift
            // What was the {1} initial number in {0}?
            // What was the top-left initial number in Binary Shift?
            [Question.BinaryShiftInitialNumber] = new TranslationInfo
            {
                QuestionText = "What was the {1} initial number in {0}？",
            },
            // What number was selected at stage {1} in {0}?
            // What number was selected at stage 0 in Binary Shift?
            [Question.BinaryShiftSelectedNumberPossition] = new TranslationInfo
            {
                QuestionText = "What number was selected at stage {1} in {0}？",
            },
            // What number was not selected at stage {1} in {0}?
            // What number was not selected at stage 0 in Binary Shift?
            [Question.BinaryShiftNotSelectedNumberPossition] = new TranslationInfo
            {
                QuestionText = "What number was not selected at stage {1} in {0}？",
            },

            // Binary
            // What word was displayed in {0}?
            // What word was displayed in Binary?
            [Question.BinaryWord] = new TranslationInfo
            {
                QuestionText = "{0}で表示された単語は？",
            },

            // Bitmaps
            // How many pixels were {1} in the {2} quadrant in {0}?
            // How many pixels were white in the top left quadrant in Bitmaps?
            [Question.Bitmaps] = new TranslationInfo
            {
                QuestionText = "{0}で{2}区域の{1}ピクセル数は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["white"] = "白",
                    ["top left"] = "左上",
                    ["top right"] = "右上",
                    ["bottom left"] = "左下",
                    ["bottom right"] = "右下",
                    ["black"] = "黒",
                },
            },

            // Black Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Black Cipher?
            [Question.BlackCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // Blind Maze
            // What color was the {1} button in {0}?
            // What color was the north button in Blind Maze?
            [Question.BlindMazeColors] = new TranslationInfo
            {
                QuestionText = "What color was the {1} button in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Gray"] = "灰",
                    ["Yellow"] = "黄",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["north"] = "北",
                    ["east"] = "東",
                    ["west"] = "西",
                    ["south"] = "南",
                },
            },
            // Which maze did you solve {0} on?
            // Which maze did you solve Blind Maze on?
            [Question.BlindMazeMaze] = new TranslationInfo
            {
                QuestionText = "Which maze did you solve {0} on？",
            },

            // Blockbusters
            // What was the last letter pressed on {0}?
            // What was the last letter pressed on Blockbusters?
            [Question.BlockbustersLastLetter] = new TranslationInfo
            {
                QuestionText = "{0}で最後に押した文字は何？",
            },

            // Blue Arrows
            // What were the letters on the screen in {0}?
            // What were the letters on the screen in Blue Arrows?
            [Question.BlueArrowsInitialLetters] = new TranslationInfo
            {
                QuestionText = "{0}でスクリーンに表示された文字は何？",
            },

            // The Blue Button
            // What was D in {0}?
            // What was D in The Blue Button?
            [Question.BlueButtonD] = new TranslationInfo
            {
                QuestionText = "What was D in {0}？",
            },
            // What was {1} in {0}?
            // What was E in The Blue Button?
            [Question.BlueButtonEFGH] = new TranslationInfo
            {
                QuestionText = "{0}でEはどれだったか？",
            },
            // What was M in {0}?
            // What was M in The Blue Button?
            [Question.BlueButtonM] = new TranslationInfo
            {
                QuestionText = "{0}でMはどれだったか？",
            },
            // What was N in {0}?
            // What was N in The Blue Button?
            [Question.BlueButtonN] = new TranslationInfo
            {
                QuestionText = "{0}でNはどれだったか？",
            },
            // What was P in {0}?
            // What was P in The Blue Button?
            [Question.BlueButtonP] = new TranslationInfo
            {
                QuestionText = "{0}でPはどれだったか？",
            },
            // What was Q in {0}?
            // What was Q in The Blue Button?
            [Question.BlueButtonQ] = new TranslationInfo
            {
                QuestionText = "{0}でQはどれだったか？",
            },
            // What was X in {0}?
            // What was X in The Blue Button?
            [Question.BlueButtonX] = new TranslationInfo
            {
                QuestionText = "{0}でXはどれだったか？",
            },

            // Blue Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Blue Cipher?
            [Question.BlueCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // Bob Barks
            // What was the {1} indicator label in {0}?
            // What was the top left indicator label in Bob Barks?
            [Question.BobBarksIndicators] = new TranslationInfo
            {
                QuestionText = "What was the {1} indicator label in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top left"] = "左上",
                    ["top right"] = "右上",
                    ["bottom left"] = "左下",
                    ["bottom right"] = "右下",
                },
            },
            // Which button flashed {1} in sequence in {0}?
            // Which button flashed first in sequence in Bob Barks?
            [Question.BobBarksPositions] = new TranslationInfo
            {
                QuestionText = "Which button flashed {1} in sequence in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "左上",
                    ["top right"] = "右上",
                    ["bottom left"] = "左下",
                    ["bottom right"] = "右下",
                },
            },

            // Boggle
            // What letter was initially visible on {0}?
            // What letter was initially visible on Boggle?
            [Question.BoggleLetters] = new TranslationInfo
            {
                QuestionText = "{0}で初めに表示された文字は？",
            },

            // Boxing
            // Which {1} appeared on {0}?
            // Which contestant’s first name appeared on Boxing?
            [Question.BoxingNames] = new TranslationInfo
            {
                QuestionText = "Which {1} appeared on {0}？",
            },
            // What was the {1} of the contestant with strength rating {2} on {0}?
            // What was the first name of the contestant with strength rating 0 on Boxing?
            [Question.BoxingContestantByStrength] = new TranslationInfo
            {
                QuestionText = "What was the {1} of the contestant with strength rating {2} on {0}？",
            },
            // What was {1}’s strength rating on {0}?
            // What was Muhammad’s strength rating on Boxing?
            [Question.BoxingStrengthByContestant] = new TranslationInfo
            {
                QuestionText = "What was {1}’s strength rating on {0}？",
            },

            // Braille
            // What was the solution word in {0}?
            // What was the solution word in Braille?
            [Question.BrailleWord] = new TranslationInfo
            {
                QuestionText = "{0}の答えの単語は何？",
            },

            // Breakfast Egg
            // Which color appeared on the egg in {0}?
            // Which color appeared on the egg in Breakfast Egg?
            [Question.BreakfastEggColor] = new TranslationInfo
            {
                QuestionText = "Which color appeared on the egg in {0}？",
            },

            // Broken Buttons
            // What was the {1} correct button you pressed in {0}?
            // What was the first correct button you pressed in Broken Buttons?
            [Question.BrokenButtons] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に押したボタンはどれ？",
                Answers = new Dictionary<string, string>
                {
                    ["bomb"] = "bomb",
                    ["blast"] = "blast",
                    ["boom"] = "boom",
                    ["burst"] = "burst",
                    ["wire"] = "wire",
                    ["button"] = "button",
                    ["module"] = "module",
                    ["light"] = "light",
                    ["led"] = "led",
                    ["switch"] = "switch",
                    ["RJ-45"] = "RJ-45",
                    ["DVI-D"] = "DVI-D",
                    ["RCA"] = "RCA",
                    ["PS/2"] = "PS/2",
                    ["serial"] = "serial",
                    ["port"] = "port",
                    ["row"] = "row",
                    ["column"] = "column",
                    ["one"] = "one",
                    ["two"] = "two",
                    ["three"] = "three",
                    ["four"] = "four",
                    ["five"] = "five",
                    ["six"] = "six",
                    ["seven"] = "seven",
                    ["eight"] = "eight",
                    ["size"] = "size",
                    ["this"] = "this",
                    ["that"] = "that",
                    ["other"] = "other",
                    ["submit"] = "submit",
                    ["abort"] = "abort",
                    ["drop"] = "drop",
                    ["thing"] = "thing",
                    ["blank"] = "blank",
                    ["broken"] = "broken",
                    ["too"] = "too",
                    ["to"] = "to",
                    ["yes"] = "yes",
                    ["see"] = "see",
                    ["sea"] = "sea",
                    ["c"] = "c",
                    ["wait"] = "wait",
                    ["word"] = "word",
                    ["bob"] = "bob",
                    ["no"] = "no",
                    ["not"] = "not",
                    ["first"] = "1",
                    ["hold"] = "hold",
                    ["late"] = "late",
                    ["fail"] = "fail",
                },
            },

            // Brown Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Brown Cipher?
            [Question.BrownCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // Brush Strokes
            // What was the color of the middle contact point in {0}?
            // What was the color of the middle contact point in Brush Strokes?
            [Question.BrushStrokesMiddleColor] = new TranslationInfo
            {
                QuestionText = "What was the color of the middle contact point in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Lime"] = "黄緑",
                    ["Green"] = "緑",
                    ["Cyan"] = "シアン",
                    ["Sky"] = "空",
                    ["Blue"] = "青",
                    ["Purple"] = "紫",
                    ["Magenta"] = "マゼンタ",
                    ["Brown"] = "茶",
                    ["White"] = "白",
                    ["Gray"] = "灰",
                    ["Black"] = "黒",
                    ["Pink"] = "ピンク",
                },
            },

            // The Bulb
            // What were the correct button presses in {0}?
            // What were the correct button presses in The Bulb?
            [Question.BulbButtonPresses] = new TranslationInfo
            {
                QuestionText = "{0}のボタンの押し順はどれ？",
            },

            // Burglar Alarm
            // What was the {1} displayed digit in {0}?
            // What was the first displayed digit in Burglar Alarm?
            [Question.BurglarAlarmDigits] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に表示された数字は何？",
            },

            // The Button
            // What color did the light glow in {0}?
            // What color did the light glow in The Button?
            [Question.ButtonLightColor] = new TranslationInfo
            {
                QuestionText = "{0}で光ったライトの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                    ["white"] = "白",
                },
            },

            // Button Sequence
            // How many of the buttons in {0} were {1}?
            // How many of the buttons in Button Sequence were red?
            [Question.ButtonSequencesColorOccurrences] = new TranslationInfo
            {
                QuestionText = "{0}内の{1}色のボタンはいくつ？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                    ["white"] = "白",
                },
            },

            // Caesar Cycle
            // What was the {1} in {0}?
            // What was the message in Caesar Cycle?
            [Question.CaesarCycleWord] = new TranslationInfo
            {
                QuestionText = "{0}の{1}は？",
            },

            // Calendar
            // What was the LED color in {0}?
            // What was the LED color in Calendar?
            [Question.CalendarLedColor] = new TranslationInfo
            {
                QuestionText = "{0}のLEDの色は何？",
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                },
            },

            // Cartinese
            // What color was the {1} button in {0}?
            // What color was the up button in Cartinese?
            [Question.CartineseButtonColors] = new TranslationInfo
            {
                QuestionText = "What color was the {1} button in {0}？",
            },
            // What lyric was played by the {1} button in {0}?
            // What lyric was played by the up button in Cartinese?
            [Question.CartineseLyrics] = new TranslationInfo
            {
                QuestionText = "What lyric was played by the {1} button in {0}？",
            },

            // Challenge & Contact
            // What was the {1} submitted answer in {0}?
            // What was the first submitted answer in Challenge & Contact?
            [Question.ChallengeAndContactAnswers] = new TranslationInfo
            {
                QuestionText = "What was the {1} submitted answer in {0}？",
            },

            // Character Codes
            // What was the {1} character in {0}?
            // What was the first character in Character Codes?
            [Question.CharacterCodesCharacter] = new TranslationInfo
            {
                QuestionText = "What was the {1} character in {0}?",
            },

            // Cheap Checkout
            // What was the {1}paid amount in {0}?
            // What was the paid amount in Cheap Checkout?
            [Question.CheapCheckoutPaid] = new TranslationInfo
            {
                QuestionText = "What was the {1}paid amount in {0}？",
            },

            // Cheep Checkout
            // Which bird {1} present in {0}?
            // Which bird was present in Cheep Checkout?
            [Question.CheepCheckoutBirds] = new TranslationInfo
            {
                QuestionText = "Which bird {1} present in {0}？",
            },

            // Chess
            // What was the {1} coordinate in {0}?
            // What was the first coordinate in Chess?
            [Question.ChessCoordinate] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の座標は何？",
            },

            // Chinese Counting
            // What color was the {1} LED in {0}?
            // What color was the left LED in Chinese Counting?
            [Question.ChineseCountingLED] = new TranslationInfo
            {
                QuestionText = "{0}の{1}のLEDの色は何？",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "白",
                    ["Red"] = "赤",
                    ["Green"] = "緑",
                    ["Orange"] = "オレンジ",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "左",
                    ["right"] = "右",
                },
            },

            // Chord Qualities
            // Which note was part of the given chord in {0}?
            // Which note was part of the given chord in Chord Qualities?
            [Question.ChordQualitiesNotes] = new TranslationInfo
            {
                QuestionText = "{0}で与えられたコードの一部にある音は何？",
            },
            // What was the given chord quality in {0}?
            // What was the given chord quality in Chord Qualities?
            [Question.ChordQualitiesQuality] = new TranslationInfo
            {
                QuestionText = "{0}で与えられたコードクオリティーは何？",
            },

            // The Code
            // What was the displayed number in {0}?
            // What was the displayed number in The Code?
            [Question.CodeDisplayNumber] = new TranslationInfo
            {
                QuestionText = "{0}で表示された数字は何？",
            },

            // Codenames
            // Which of these words was submitted in {0}?
            // Which of these words was submitted in Codenames?
            [Question.CodenamesAnswers] = new TranslationInfo
            {
                QuestionText = "Which of these words was submitted in {0}？",
            },

            // Coffeebucks
            // What was the last served coffee in {0}?
            // What was the last served coffee in Coffeebucks?
            [Question.CoffeebucksCoffee] = new TranslationInfo
            {
                QuestionText = "What was the last served coffee in {0}？",
            },

            // Coinage
            // Which coin was flipped in {0}?
            // Which coin was flipped in Coinage?
            [Question.CoinageFlip] = new TranslationInfo
            {
                QuestionText = "Which coin was flipped in {0}？",
            },

            // Color Addition
            // What was {1}'s number in {0}?
            // What was red's number in Color Addition?
            [Question.ColorAdditionNumbers] = new TranslationInfo
            {
                QuestionText = "What was {1}'s number in {0}?",
            },

            // Color Braille
            // What mangling was applied in {0}?
            // What mangling was applied in Color Braille?
            [Question.ColorBrailleMangling] = new TranslationInfo
            {
                QuestionText = "What mangling was applied in {0}？",
            },
            // What was the {1} word in {0}?
            // What was the red word in Color Braille?
            [Question.ColorBrailleWords] = new TranslationInfo
            {
                QuestionText = "What was the {1} word in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                },
            },

            // Color Decoding
            // What was the {1}-stage indicator pattern in {0}?
            // What was the first-stage indicator pattern in Color Decoding?
            [Question.ColorDecodingIndicatorPattern] = new TranslationInfo
            {
                QuestionText = "{0}でステージ{1}のインジケーターのパターンは？",
            },
            // Which color {1} in the {2}-stage indicator pattern in {0}?
            // Which color appeared in the first-stage indicator pattern in Color Decoding?
            [Question.ColorDecodingIndicatorColors] = new TranslationInfo
            {
                QuestionText = "Which color {1} in the {2}-stage indicator pattern in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "Green",
                    ["Purple"] = "Purple",
                    ["Red"] = "Red",
                    ["Blue"] = "Blue",
                    ["Yellow"] = "Yellow",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["appeared"] = "appeared",
                    ["did not appear"] = "did not appear",
                },
            },

            // Colored Keys
            // What was the displayed word in {0}?
            // What was the displayed word in Colored Keys?
            [Question.ColoredKeysDisplayWord] = new TranslationInfo
            {
                QuestionText = "{0}で表示された単語は？",
            },
            // What was the displayed word’s color in {0}?
            // What was the displayed word’s color in Colored Keys?
            [Question.ColoredKeysDisplayWordColor] = new TranslationInfo
            {
                QuestionText = "{0}で表示された単語の色は？",
            },
            // What was the color of the {1} key in {0}?
            // What was the color of the top-left key in Colored Keys?
            [Question.ColoredKeysKeyColor] = new TranslationInfo
            {
                QuestionText = "What was the color of the {1} key in {0}？",
            },
            // What letter was on the {1} key in {0}?
            // What letter was on the top-left key in Colored Keys?
            [Question.ColoredKeysKeyLetter] = new TranslationInfo
            {
                QuestionText = "{0}の{1}のキーパッドの文字は？",
            },

            // Colored Squares
            // What was the first color group in {0}?
            // What was the first color group in Colored Squares?
            [Question.ColoredSquaresFirstGroup] = new TranslationInfo
            {
                QuestionText = "{0}の最初の色グループは？",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "白",
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                    ["Magenta"] = "マゼンタ",
                },
            },

            // Colored Switches
            // What was the initial position of the switches in {0}?
            // What was the initial position of the switches in Colored Switches?
            [Question.ColoredSwitchesInitialPosition] = new TranslationInfo
            {
                QuestionText = "{0}の初期配置は？",
            },
            // What was the position of the switches when the LEDs came on in {0}?
            // What was the position of the switches when the LEDs came on in Colored Switches?
            [Question.ColoredSwitchesWhenLEDsCameOn] = new TranslationInfo
            {
                QuestionText = "{0}のLEDが示したスイッチの位置は？",
            },

            // Color Morse
            // What was the color of the {1} LED in {0}?
            // What was the color of the first LED in Color Morse?
            [Question.ColorMorseColor] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のLEDは何色？",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Orange"] = "オレンジ",
                    ["Purple"] = "紫",
                    ["Red"] = "赤",
                    ["Yellow"] = "黄",
                    ["White"] = "白",
                },
            },
            // What character was flashed by the {1} LED in {0}?
            // What character was flashed by the first LED in Color Morse?
            [Question.ColorMorseCharacter] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のLEDが示す文字は？",
            },

            // Colors Maximization
            // What was the submitted score in {0}?
            // What was the submitted score in Colors Maximization?
            [Question.ColorsMaximizationSubmittedScore] = new TranslationInfo
            {
                QuestionText = "What was the submitted score in {0}？",
            },
            // What color {1} submitted as part of the solution in {0}?
            // What color was submitted as part of the solution in Colors Maximization?
            [Question.ColorsMaximizationSubmittedColor] = new TranslationInfo
            {
                QuestionText = "What color {1} submitted as part of the solution in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Magenta"] = "マゼンタ",
                    ["Red"] = "赤",
                    ["White"] = "白",
                    ["Yellow"] = "黄",
                },
            },
            // How many buttons were {1} in {0}?
            // How many buttons were red in Colors Maximization?
            [Question.ColorsMaximizationColorCount] = new TranslationInfo
            {
                QuestionText = "How many buttons were {1} in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                },
            },

            // Colour Flash
            // What was the color of the last word in the sequence in {0}?
            // What was the color of the last word in the sequence in Colour Flash?
            [Question.ColourFlashLastColor] = new TranslationInfo
            {
                QuestionText = "{0}のシーケンスの最後の単語は何色？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Magenta"] = "マゼンタ",
                    ["White"] = "白",
                },
            },

            // Connection Check
            // What pair of numbers was present in {0}?
            // What pair of numbers was present in Connection Check?
            [Question.ConnectionCheckNumbers] = new TranslationInfo
            {
                QuestionText = "What pair of numbers was present in {0}?",
            },

            // Coordinates
            // What was the solution you selected first in {0}?
            // What was the solution you selected first in Coordinates?
            [Question.CoordinatesFirstSolution] = new TranslationInfo
            {
                QuestionText = "{0}で最初に選んだ解法は？",
            },
            // What was the grid size in {0}?
            // What was the grid size in Coordinates?
            [Question.CoordinatesSize] = new TranslationInfo
            {
                QuestionText = "{0}のグリッドのサイズは？",
            },

            // Coral Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Coral Cipher?
            [Question.CoralCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was on the {1} screen on page {2} in {0}?",
            },

            // Corners
            // What was the color of the {1} corner in {0}?
            // What was the color of the top-left corner in Corners?
            [Question.CornersColors] = new TranslationInfo
            {
                QuestionText = "{0}の{1}の角は何色？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                },
            },
            // How many corners in {0} were {1}?
            // How many corners in Corners were red?
            [Question.CornersColorCount] = new TranslationInfo
            {
                QuestionText = "{0}の{1}色の角はいくつ？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                },
            },

            // Cornflower Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Cornflower Cipher?
            [Question.CornflowerCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was on the {1} screen on page {2} in {0}?",
            },

            // Cosmic
            // What was the number initially shown in {0}?
            // What was the number initially shown in Cosmic?
            [Question.CosmicNumber] = new TranslationInfo
            {
                QuestionText = "What was the number initially shown in {0}？",
            },

            // Cream Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Cream Cipher?
            [Question.CreamCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was on the {1} screen on page {2} in {0}?",
            },

            // Creation
            // What were the weather conditions on the {1} day in {0}?
            // What were the weather conditions on the first day in Creation?
            [Question.CreationWeather] = new TranslationInfo
            {
                QuestionText = "What were the weather conditions on the {1} day in {0}？",
            },

            // Crimson Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Crimson Cipher?
            [Question.CrimsonCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was on the {1} screen on page {2} in {0}?",
            },

            // Critters
            // What was the alteration color used in {0}?
            // What was the alteration color used in Critters?
            [Question.CrittersAlterationColor] = new TranslationInfo
            {
                QuestionText = "What was the alteration color used in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Yellow"] = "黄",
                    ["Pink"] = "ピンク",
                    ["Blue"] = "青",
                    ["White"] = "白",
                },
            },

            // Cryptic Cycle
            // What was the {1} in {0}?
            // What was the message in Cryptic Cycle?
            [Question.CrypticCycleWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} in {0}？",
            },

            // Cryptic Keypad
            // What was the label of the {1} key in {0}?
            // What was the label of the top-left key in Cryptic Keypad?
            [Question.CrypticKeypadLabels] = new TranslationInfo
            {
                QuestionText = "{0}で{1}のキーパッドのラベルは？",
            },
            // Which cardinal direction was the {1} key rotated to in {0}?
            // Which cardinal direction was the top-left key rotated to in Cryptic Keypad?
            [Question.CrypticKeypadRotations] = new TranslationInfo
            {
                QuestionText = "{0}で{1}のキーパッドの回転方向は？",
                Answers = new Dictionary<string, string>
                {
                    ["North"] = "北",
                    ["East"] = "東",
                    ["South"] = "南",
                    ["West"] = "西",
                },
            },

            // The Cube
            // What was the {1} cube rotation in {0}?
            // What was the first cube rotation in The Cube?
            [Question.CubeRotations] = new TranslationInfo
            {
                QuestionText = "What was the {1} cube rotation in {0}？",
            },

            // The Cyan Button
            // Where was the button at the {1} stage in {0}?
            // Where was the button at the first stage in The Cyan Button?
            [Question.CyanButtonPositions] = new TranslationInfo
            {
                QuestionText = "Where was the button at the {1} stage in {0}？",
            },

            // DACH Maze
            // Which region did you depart from in {0}?
            // Which region did you depart from in DACH Maze?
            [Question.DACHMazeOrigin] = new TranslationInfo
            {
                QuestionText = "{0}の出発点は？",
            },

            // Deaf Alley
            // What was the shape generated in {0}?
            // What was the shape generated in Deaf Alley?
            [Question.DeafAlleyShape] = new TranslationInfo
            {
                QuestionText = "{0}で生成された文字は？",
            },

            // The Deck of Many Things
            // What deck did the first card of {0} belong to?
            // What deck did the first card of The Deck of Many Things belong to?
            [Question.DeckOfManyThingsFirstCard] = new TranslationInfo
            {
                QuestionText = "What deck did the first card of {0} belong to？",
            },

            // Decolored Squares
            // What was the starting {1} defining color in {0}?
            // What was the starting column defining color in Decolored Squares?
            [Question.DecoloredSquaresStartingPos] = new TranslationInfo
            {
                QuestionText = "{0}の開始位置の{1}は何色？",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "白",
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                    ["Magenta"] = "マゼンタ",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["column"] = "列",
                    ["row"] = "段",
                },
            },

            // Decolour Flash
            // What was the {1} of the {2} goal in {0}?
            [Question.DecolourFlashGoal] = new TranslationInfo
            {
                QuestionText = "What was the {1} of the {2} goal in {0}?",
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

            // Devilish Eggs
            // What was the {1} egg’s {2} rotation in {0}?
            // What was the top egg’s first rotation in Devilish Eggs?
            [Question.DevilishEggsRotations] = new TranslationInfo
            {
                QuestionText = "What was the {1} egg’s {2} rotation in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "上",
                    ["bottom"] = "下",
                },
            },
            // What was the {1} digit in the string of numbers on {0}?
            // What was the first digit in the string of numbers on Devilish Eggs?
            [Question.DevilishEggsNumbers] = new TranslationInfo
            {
                QuestionText = "What was the {1} digit in the string of numbers on {0}？",
            },
            // What was the {1} letter in the string of letters on {0}?
            // What was the first letter in the string of letters on Devilish Eggs?
            [Question.DevilishEggsLetters] = new TranslationInfo
            {
                QuestionText = "What was the {1} letter in the string of letters on {0}？",
            },

            // Digisibility
            // What was the number on the {1} button in {0}?
            // What was the number on the first button in Digisibility?
            [Question.DigisibilityDisplayedNumber] = new TranslationInfo
            {
                QuestionText = "What was the number on the {1} button in {0}？",
            },

            // Discolored Squares
            // What was {1}’s remembered position in {0}?
            // What was Blue’s remembered position in Discolored Squares?
            [Question.DiscoloredSquaresRememberedPositions] = new TranslationInfo
            {
                QuestionText = "{0}で{1}の覚えた位置は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Blue"] = "青",
                    ["Red"] = "赤",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Magenta"] = "マゼンタ",
                },
            },

            // Divisible Numbers
            // What were the correct button presses in {0}?
            // What were the correct button presses in Divisible Numbers?
            [Question.DivisibleNumbersAnswers] = new TranslationInfo
            {
                QuestionText = "{0}で押した正しいボタンはどれ？？",
            },
            // What was the {1} stage’s number in {0}?
            // What was the first stage’s number in Divisible Numbers?
            [Question.DivisibleNumbersNumbers] = new TranslationInfo
            {
                QuestionText = "{0}でのステージ{1}の数字は？",
            },

            // Double Arrows
            // What was the starting position in {0}?
            // What was the starting position in Double Arrows?
            [Question.DoubleArrowsStart] = new TranslationInfo
            {
                QuestionText = "What was the starting position in {0}?",
            },
            // Which {1} arrow moved {2} in the grid in {0}?
            // Which inner arrow moved up in the grid in Double Arrows?
            [Question.DoubleArrowsArrow] = new TranslationInfo
            {
                QuestionText = "Which {1} arrow moved {2} in the grid in {0}?",
            },
            // Which direction in the grid did the {1} arrow move in {0}?
            // Which direction in the grid did the inner up arrow move in Double Arrows?
            [Question.DoubleArrowsMovement] = new TranslationInfo
            {
                QuestionText = "Which direction in the grid did the {1} arrow move in {0}?",
            },

            // Double Color
            // What was the screen color on the {1} stage of {0}?
            // What was the screen color on the first stage of Double Color?
            [Question.DoubleColorColors] = new TranslationInfo
            {
                QuestionText = "{0}でのステージ{1}の画面の色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Red"] = "赤",
                    ["Pink"] = "ピンク",
                    ["Yellow"] = "黄",
                },
            },

            // Double Digits
            // What was the most recent digit on the {1} display in {0}?
            // What was the most recent digit on the left display in Double Digits?
            [Question.DoubleDigitsDisplays] = new TranslationInfo
            {
                QuestionText = "{0}の{1}画面上の数字は？",
            },

            // Double-Oh
            // Which button was the submit button in {0}?
            // Which button was the submit button in Double-Oh?
            [Question.DoubleOhSubmitButton] = new TranslationInfo
            {
                QuestionText = "{0}の送信ボタンは？",
            },

            // Dr. Doctor
            // Which of these symptoms was listed on {0}?
            // Which of these symptoms was listed on Dr. Doctor?
            [Question.DrDoctorSymptoms] = new TranslationInfo
            {
                QuestionText = "Which of these symptoms was listed on {0}？",
            },
            // Which of these diseases was listed on {0}, but not the one treated?
            // Which of these diseases was listed on Dr. Doctor, but not the one treated?
            [Question.DrDoctorDiseases] = new TranslationInfo
            {
                QuestionText = "Which of these diseases was listed on {0}, but not the one treated？",
            },

            // Dreamcipher
            // What was the decrypted word in {0}?
            // What was the decrypted word in Dreamcipher?
            [Question.DreamcipherWord] = new TranslationInfo
            {
                QuestionText = "What was the decrypted word in {0}？",
            },

            // Dumb Waiters
            // Which player {1} present in {0}?
            // Which player was present in Dumb Waiters?
            [Question.DumbWaitersPlayerAvailable] = new TranslationInfo
            {
                QuestionText = "Which player {1} present in {0}？",
            },

            // eeB gnillepS
            // What word was asked to be spelled in {0}?
            // What word was asked to be spelled in eeB gnillepS?
            [Question.eeBgnillepSWord] = new TranslationInfo
            {
                QuestionText = "{0}において綴りを尋ねられた単語は？",
            },

            // Eight
            // What was the last digit on the small display in {0}?
            // What was the last digit on the small display in Eight?
            [Question.EightLastSmallDisplayDigit] = new TranslationInfo
            {
                QuestionText = "What was the last digit on the small display in {0}？",
            },
            // What was the position of the last broken digit in {0}?
            // What was the position of the last broken digit in Eight?
            [Question.EightLastBrokenDigitPosition] = new TranslationInfo
            {
                QuestionText = "What was the position of the last broken digit in {0}？",
            },
            // What were the last resulting digits in {0}?
            // What were the last resulting digits in Eight?
            [Question.EightLastResultingDigits] = new TranslationInfo
            {
                QuestionText = "What were the last resulting digits in {0}？",
            },
            // What was the last displayed number in {0}?
            // What was the last displayed number in Eight?
            [Question.EightLastDisplayedNumber] = new TranslationInfo
            {
                QuestionText = "What was the last displayed number in {0}？",
            },

            // Elder Futhark
            // What was the {1} rune shown on {0}?
            // What was the first rune shown on Elder Futhark?
            [Question.ElderFutharkRunes] = new TranslationInfo
            {
                QuestionText = "What was the {1} rune shown on {0}？",
            },

            // ENA Cipher
            // What was the {1} keyword in {0}?
            // What was the 1st keyword in ENA Cipher?
            [Question.EnaCipherKeywordAnswer] = new TranslationInfo
            {
                QuestionText = "What was the {1} keyword in {0}?",
            },
            // What was the transposition key in {0}?
            // What was the transposition key in ENA Cipher?
            [Question.EnaCipherExtAnswer] = new TranslationInfo
            {
                QuestionText = "What was the transposition key in {0}?",
            },
            // What was the encrypted word in {0}?
            // What was the encrypted word in ENA Cipher?
            [Question.EnaCipherEncryptedAnswer] = new TranslationInfo
            {
                QuestionText = "What was the encrypted word in {0}?",
            },

            // Encrypted Equations
            // Which shape was the {1} operand in {0}?
            // Which shape was the first operand in Encrypted Equations?
            [Question.EncryptedEquationsShapes] = new TranslationInfo
            {
                QuestionText = "Which shape was the {1} operand in {0}？",
            },

            // Encrypted Hangman
            // What method of encryption was used by {0}?
            // What method of encryption was used by Encrypted Hangman?
            [Question.EncryptedHangmanEncryptionMethod] = new TranslationInfo
            {
                QuestionText = "What method of encryption was used by {0}？",
            },
            // What module name was encrypted by {0}?
            // What module name was encrypted by Encrypted Hangman?
            [Question.EncryptedHangmanModule] = new TranslationInfo
            {
                QuestionText = "What module name was encrypted by {0}？",
            },

            // Encrypted Maze
            // Which symbol on {0} was spinning {1}?
            // Which symbol on Encrypted Maze was spinning clockwise?
            [Question.EncryptedMazeSymbols] = new TranslationInfo
            {
                QuestionText = "Which symbol on {0} was spinning {1}？",
            },

            // Encrypted Morse
            // What was the {1} on {0}?
            // What was the received call on Encrypted Morse?
            [Question.EncryptedMorseCallResponse] = new TranslationInfo
            {
                QuestionText = "What was the {1} on {0}？",
            },

            // Encryption Bingo
            // What was the first encoding used in {0}?
            // What was the first encoding used in Encryption Bingo?
            [Question.EncryptionBingoEncoding] = new TranslationInfo
            {
                QuestionText = "What was the first encoding used in {0}？",
            },

            // Enigma Cycle
            // What was the {1} in {0}?
            // What was the message in Enigma Cycle?
            [Question.EnigmaCycleWords] = new TranslationInfo
            {
                QuestionText = "What was the {1} in {0}?",
            },

            // Entry Number Four
            // What was the first number shown in {0}?
            // What was the first number shown in Entry Number Four?
            [Question.EntryNumberFourNumber1] = new TranslationInfo
            {
                QuestionText = "What was the first number shown in {0}？",
            },
            // What was the second number shown in {0}?
            // What was the second number shown in Entry Number Four?
            [Question.EntryNumberFourNumber2] = new TranslationInfo
            {
                QuestionText = "What was the second number shown in {0}？",
            },
            // What was the third number shown in {0}?
            // What was the third number shown in Entry Number Four?
            [Question.EntryNumberFourNumber3] = new TranslationInfo
            {
                QuestionText = "What was the third number shown in {0}？",
            },
            // What was the expected fourth entry in {0}?
            // What was the expected fourth entry in Entry Number Four?
            [Question.EntryNumberFourExpected] = new TranslationInfo
            {
                QuestionText = "What was the expected fourth entry in {0}？",
            },
            // What was the constant coefficient in {0}?
            // What was the constant coefficient in Entry Number Four?
            [Question.EntryNumberFourCoeff] = new TranslationInfo
            {
                QuestionText = "What was the constant coefficient in {0}？",
            },

            // Entry Number One
            // What was the {1} number shown in {0}?
            // What was the second number shown in Entry Number One?
            [Question.EntryNumberOneNumbers] = new TranslationInfo
            {
                QuestionText = "What was the {1} number shown in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["second"] = "2",
                    ["third"] = "3",
                    ["fourth"] = "4",
                },
            },
            // What was the expected first entry in {0}?
            // What was the expected first entry in Entry Number One?
            [Question.EntryNumberOneExpected] = new TranslationInfo
            {
                QuestionText = "What was the expected first entry in {0}？",
            },
            // What was the constant coefficient in {0}?
            // What was the constant coefficient in Entry Number One?
            [Question.EntryNumberOneCoeff] = new TranslationInfo
            {
                QuestionText = "What was the constant coefficient in {0}？",
            },

            // Equations X
            // What was the displayed symbol in {0}?
            // What was the displayed symbol in Equations X?
            [Question.EquationsXSymbols] = new TranslationInfo
            {
                QuestionText = "What was the displayed symbol in {0}？",
            },

            // Etterna
            // What was the beat for the {1} arrow from the bottom in {0}?
            // What was the beat for the first arrow from the bottom in Etterna?
            [Question.EtternaNumber] = new TranslationInfo
            {
                QuestionText = "{0}の下から{1}番目の矢印のビートは？",
            },

            // Exoplanets
            // What was the starting target planet in {0}?
            // What was the starting target planet in Exoplanets?
            [Question.ExoplanetsStartingTargetPlanet] = new TranslationInfo
            {
                QuestionText = "What was the starting target planet in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["outer"] = "outer",
                    ["middle"] = "中央",
                    ["inner"] = "inner",
                    ["none"] = "なし",
                },
            },
            // What was the starting target digit in {0}?
            // What was the starting target digit in Exoplanets?
            [Question.ExoplanetsStartingTargetDigit] = new TranslationInfo
            {
                QuestionText = "What was the starting target digit in {0}？",
            },
            // What was the final target planet in {0}?
            // What was the final target planet in Exoplanets?
            [Question.ExoplanetsTargetPlanet] = new TranslationInfo
            {
                QuestionText = "What was the final target planet in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["outer"] = "outer",
                    ["middle"] = "中央",
                    ["inner"] = "inner",
                    ["none"] = "なし",
                },
            },
            // What was the final target digit in {0}?
            // What was the final target digit in Exoplanets?
            [Question.ExoplanetsTargetDigit] = new TranslationInfo
            {
                QuestionText = "What was the final target digit in {0}？",
            },

            // Factoring Maze
            // What was one of the prime numbers chosen in {0}?
            // What was one of the prime numbers chosen in Factoring Maze?
            [Question.FactoringMazeChosenPrimes] = new TranslationInfo
            {
                QuestionText = "{0}で選ばれた素因数の一つにあるのはどれ？",
            },

            // Factory Maze
            // What room did you start in in {0}?
            // What room did you start in in Factory Maze?
            [Question.FactoryMazeStartRoom] = new TranslationInfo
            {
                QuestionText = "What room did you start in in {0}？",
            },

            // Fast Math
            // What was the last pair of letters in {0}?
            // What was the last pair of letters in Fast Math?
            [Question.FastMathLastLetters] = new TranslationInfo
            {
                QuestionText = "{0}の最後の英字のペアは？",
            },

            // Faulty RGB Maze
            // What was the exit coordinate in {0}?
            // What was the exit coordinate in Faulty RGB Maze?
            [Question.FaultyRGBMazeExit] = new TranslationInfo
            {
                QuestionText = "What was the exit coordinate in {0}？",
            },
            // Where was the {1} key in {0}?
            // Where was the red key in Faulty RGB Maze?
            [Question.FaultyRGBMazeKeys] = new TranslationInfo
            {
                QuestionText = "Where was the {1} key in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                },
            },
            // Which maze number was the {1} maze in {0}?
            // Which maze number was the red maze in Faulty RGB Maze?
            [Question.FaultyRGBMazeNumber] = new TranslationInfo
            {
                QuestionText = "Which maze number was the {1} maze in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                },
            },

            // Flags
            // What was the displayed number in {0}?
            // What was the displayed number in Flags?
            [Question.FlagsDisplayedNumber] = new TranslationInfo
            {
                QuestionText = "{0}で表示された数字は？",
            },
            // What was the main country flag in {0}?
            // What was the main country flag in Flags?
            [Question.FlagsMainCountry] = new TranslationInfo
            {
                QuestionText = "What was the main country flag in {0}？",
            },
            // Which of these country flags was shown, but not the main country flag, in {0}?
            // Which of these country flags was shown, but not the main country flag, in Flags?
            [Question.FlagsCountries] = new TranslationInfo
            {
                QuestionText = "Which of these country flags was shown, but not the main country flag, in {0}？",
            },

            // Flashing Arrows
            // What number was displayed on {0}?
            // What number was displayed on Flashing Arrows?
            [Question.FlashingArrowsDisplayedValue] = new TranslationInfo
            {
                QuestionText = "What number was displayed on {0}？",
            },
            // What color flashed {1} black on the relevant arrow in {0}?
            // What color flashed before black on the relevant arrow in Flashing Arrows?
            [Question.FlashingArrowsReferredArrow] = new TranslationInfo
            {
                QuestionText = "What color flashed {1} black on the relevant arrow in {0}？",
            },

            // Flashing Lights
            // How many times did the {1} LED flash {2} on {0}?
            // How many times did the top LED flash cyan on Flashing Lights?
            [Question.FlashingLightsLEDFrequency] = new TranslationInfo
            {
                QuestionText = "{0}で{1}のLEDは{2}色に何回光った？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "上",
                    ["cyan"] = "cyan",
                    ["green"] = "緑",
                    ["red"] = "赤",
                    ["purple"] = "紫",
                    ["orange"] = "オレンジ",
                    ["bottom"] = "下",
                },
            },

            // Flyswatting
            // Which fly was present, but not in the solution in {0}?
            // Which fly was present, but not in the solution in Flyswatting?
            [Question.FlyswattingUnpressed] = new TranslationInfo
            {
                QuestionText = "Which fly was present, but not in the solution in {0}？",
            },

            // Forest Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Forest Cipher?
            [Question.ForestCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was on the {1} screen on page {2} in {0}?",
            },

            // Forget Any Color
            // What were the cylinders during stage {1} in {0}?
            // What were the cylinders during stage 1 in Forget Any Color?
            [Question.ForgetAnyColorCylinder] = new TranslationInfo
            {
                QuestionText = "What were the cylinders during stage {1} in {0}？",
            },
            // What figure was used during stage {1} in {0}?
            // What figure was used during stage 1 in Forget Any Color?
            [Question.ForgetAnyColorSequence] = new TranslationInfo
            {
                QuestionText = "What figure was used during stage {1} in {0}？",
            },

            // Forget Me
            // What number was in the {1} position of the initial puzzle in {0}?
            // What number was in the top-left position of the initial puzzle in Forget Me?
            [Question.ForgetMeInitialState] = new TranslationInfo
            {
                QuestionText = "What number was in the {1} position of the initial puzzle in {0}？",
            },

            // Forget’s Ultimate Showdown
            // What was the {1} digit of the answer in {0}?
            // What was the first digit of the answer in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownAnswer] = new TranslationInfo
            {
                QuestionText = "What was the {1} digit of the answer in {0}？",
            },
            // What was the {1} digit of the initial number in {0}?
            // What was the first digit of the initial number in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownInitial] = new TranslationInfo
            {
                QuestionText = "What was the {1} digit of the initial number in {0}？",
            },
            // What was the {1} digit of the bottom number in {0}?
            // What was the first digit of the bottom number in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownBottom] = new TranslationInfo
            {
                QuestionText = "What was the {1} digit of the bottom number in {0}？",
            },
            // What was the {1} method used in {0}?
            // What was the first method used in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownMethod] = new TranslationInfo
            {
                QuestionText = "What was the {1} method used in {0}？",
            },

            // Forget the Colors
            // What number was on the gear during stage {1} in {0}?
            // What number was on the gear during stage 0 in Forget the Colors?
            [Question.ForgetTheColorsGearNumber] = new TranslationInfo
            {
                QuestionText = "What number was on the gear during stage {1} in {0}？",
            },
            // What number was on the large display during stage {1} in {0}?
            // What number was on the large display during stage 0 in Forget the Colors?
            [Question.ForgetTheColorsLargeDisplay] = new TranslationInfo
            {
                QuestionText = "What number was on the large display during stage {1} in {0}？",
            },
            // What was the last decimal in the sine number received during stage {1} in {0}?
            // What was the last decimal in the sine number received during stage 0 in Forget the Colors?
            [Question.ForgetTheColorsSineNumber] = new TranslationInfo
            {
                QuestionText = "What was the last decimal in the sine number received during stage {1} in {0}？",
            },
            // What color was the gear during stage {1} in {0}?
            // What color was the gear during stage 0 in Forget the Colors?
            [Question.ForgetTheColorsGearColor] = new TranslationInfo
            {
                QuestionText = "What color was the gear during stage {1} in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Cyan"] = "シアン",
                    ["Blue"] = "青",
                    ["Purple"] = "紫",
                    ["Pink"] = "ピンク",
                    ["Maroon"] = "栗",
                    ["White"] = "白",
                    ["Gray"] = "灰",
                },
            },
            // Which edgework-based rule was applied to the sum of nixies and gear during stage {1} in {0}?
            // Which edgework-based rule was applied to the sum of nixies and gear during stage 0 in Forget the Colors?
            [Question.ForgetTheColorsRuleColor] = new TranslationInfo
            {
                QuestionText = "Which edgework-based rule was applied to the sum of nixies and gear during stage {1} in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Cyan"] = "シアン",
                    ["Blue"] = "青",
                    ["Purple"] = "紫",
                    ["Pink"] = "ピンク",
                    ["Maroon"] = "栗",
                    ["White"] = "白",
                    ["Gray"] = "灰",
                },
            },

            // Free Parking
            // What was the player token in {0}?
            // What was the player token in Free Parking?
            [Question.FreeParkingToken] = new TranslationInfo
            {
                QuestionText = "What was the player token in {0}？",
            },

            // Functions
            // What was the last digit of your first query’s result in {0}?
            // What was the last digit of your first query’s result in Functions?
            [Question.FunctionsLastDigit] = new TranslationInfo
            {
                QuestionText = "What was the last digit of your first query’s result in {0}？",
            },
            // What number was to the left of the displayed letter in {0}?
            // What number was to the left of the displayed letter in Functions?
            [Question.FunctionsLeftNumber] = new TranslationInfo
            {
                QuestionText = "What number was to the left of the displayed letter in {0}？",
            },
            // What letter was displayed in {0}?
            // What letter was displayed in Functions?
            [Question.FunctionsLetter] = new TranslationInfo
            {
                QuestionText = "What letter was displayed in {0}？",
            },
            // What number was to the right of the displayed letter in {0}?
            // What number was to the right of the displayed letter in Functions?
            [Question.FunctionsRightNumber] = new TranslationInfo
            {
                QuestionText = "What number was to the right of the displayed letter in {0}？",
            },

            // Game of Life Cruel
            // Which of these was a color combination that occurred in {0}?
            // Which of these was a color combination that occurred in Game of Life Cruel?
            [Question.GameOfLifeCruelColors] = new TranslationInfo
            {
                QuestionText = "Which of these was a color combination that occurred in {0}？",
            },

            // The Gamepad
            // What were the numbers on {0}?
            // What were the numbers on The Gamepad?
            [Question.GamepadNumbers] = new TranslationInfo
            {
                QuestionText = "{0}の数字は？",
            },

            // The Glitched Button
            // What was the cycling bit sequence in {0}?
            // What was the cycling bit sequence in The Glitched Button?
            [Question.GlitchedButtonSequence] = new TranslationInfo
            {
                QuestionText = "What was the cycling bit sequence in {0}？",
            },

            // The Gray Button
            // What was the {1} coordinate on the display in {0}?
            // What was the horizontal coordinate on the display in The Gray Button?
            [Question.GrayButtonCoordinates] = new TranslationInfo
            {
                QuestionText = "What was the {1} coordinate on the display in {0}？",
            },

            // Gray Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Gray Cipher?
            [Question.GrayCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // The Great Void
            // What was the {1} color in {0}?
            // What was the first color in The Great Void?
            [Question.GreatVoidColor] = new TranslationInfo
            {
                QuestionText = "What was the {1} color in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Magenta"] = "マゼンタ",
                    ["Yellow"] = "黄",
                    ["Cyan"] = "シアン",
                    ["White"] = "白",
                },
            },
            // What was the {1} digit in {0}?
            // What was the first digit in The Great Void?
            [Question.GreatVoidDigit] = new TranslationInfo
            {
                QuestionText = "What was the {1} digit in {0}？",
            },

            // Green Arrows
            // What was the last number on the display on {0}?
            // What was the last number on the display on Green Arrows?
            [Question.GreenArrowsLastScreen] = new TranslationInfo
            {
                QuestionText = "What was the last number on the display on {0}？",
            },

            // The Green Button
            // What was the word submitted in {0}?
            // What was the word submitted in The Green Button?
            [Question.GreenButtonWord] = new TranslationInfo
            {
                QuestionText = "What was the word submitted in {0}？",
            },

            // Green Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Green Cipher?
            [Question.GreenCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // Gridlock
            // What was the starting location in {0}?
            // What was the starting location in Gridlock?
            [Question.GridLockStartingLocation] = new TranslationInfo
            {
                QuestionText = "{0}の開始位置は？",
            },
            // What was the ending location in {0}?
            // What was the ending location in Gridlock?
            [Question.GridLockEndingLocation] = new TranslationInfo
            {
                QuestionText = "{0}の終了位置は？",
            },
            // What was the starting color in {0}?
            // What was the starting color in Gridlock?
            [Question.GridLockStartingColor] = new TranslationInfo
            {
                QuestionText = "{0}の開始地点は何色？",
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                },
            },

            // Grocery Store
            // What was the first item shown in {0}?
            // What was the first item shown in Grocery Store?
            [Question.GroceryStoreFirstItem] = new TranslationInfo
            {
                QuestionText = "{0}で最初に表示された商品は？",
            },

            // Gryphons
            // What was the gryphon’s name in {0}?
            // What was the gryphon’s name in Gryphons?
            [Question.GryphonsName] = new TranslationInfo
            {
                QuestionText = "What was the gryphon’s name in {0}？",
            },
            // What was the gryphon’s age in {0}?
            // What was the gryphon’s age in Gryphons?
            [Question.GryphonsAge] = new TranslationInfo
            {
                QuestionText = "What was the gryphon’s age in {0}？",
            },

            // Guess Who?
            // Who was the person recalled in {0}?
            // Who was the person recalled in Guess Who??
            [Question.GuessWhoPerson] = new TranslationInfo
            {
                QuestionText = "Who was the person recalled in {0}？",
            },

            // Hereditary Base Notation
            // What was the given number in {0}?
            // What was the given number in Hereditary Base Notation?
            [Question.HereditaryBaseNotationInitialNumber] = new TranslationInfo
            {
                QuestionText = "What was the given number in {0}？",
            },

            // The Hexabutton
            // What label was printed on {0}?
            // What label was printed on The Hexabutton?
            [Question.HexabuttonLabel] = new TranslationInfo
            {
                QuestionText = "What label was printed on {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Jump"] = "Jump",
                    ["Boom"] = "Boom",
                    ["Claim"] = "Claim",
                    ["Button"] = "Button",
                    ["Hold"] = "Hold",
                    ["Blue"] = "Blue",
                },
            },

            // Hexamaze
            // What was the color of the pawn in {0}?
            // What was the color of the pawn in Hexamaze?
            [Question.HexamazePawnColor] = new TranslationInfo
            {
                QuestionText = "{0}のコマの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Cyan"] = "シアン",
                    ["Blue"] = "青",
                    ["Pink"] = "ピンク",
                },
            },

            // hexOS
            // What were the deciphered letters in {0}?
            // What were the deciphered letters in hexOS?
            [Question.HexOSCipher] = new TranslationInfo
            {
                QuestionText = "What were the deciphered letters in {0}？",
            },
            // What was the deciphered phrase in {0}?
            // What was the deciphered phrase in hexOS?
            [Question.HexOSOctCipher] = new TranslationInfo
            {
                QuestionText = "What was the deciphered phrase in {0}？",
            },
            // What was the {1} 3-digit number cycled by the screen in {0}?
            // What was the first 3-digit number cycled by the screen in hexOS?
            [Question.HexOSScreen] = new TranslationInfo
            {
                QuestionText = "What was the {1} 3-digit number cycled by the screen in {0}？",
            },
            // What were the rhythm values in {0}?
            // What were the rhythm values in hexOS?
            [Question.HexOSSum] = new TranslationInfo
            {
                QuestionText = "What were the rhythm values in {0}？",
            },

            // Hidden Colors
            // What was the color of the main LED in {0}?
            // What was the color of the main LED in Hidden Colors?
            [Question.HiddenColorsLED] = new TranslationInfo
            {
                QuestionText = "What was the color of the main LED in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                    ["Orange"] = "オレンジ",
                    ["Purple"] = "紫",
                    ["Magenta"] = "マゼンタ",
                    ["White"] = "白",
                },
            },

            // Hill Cycle
            // What was the {1} in {0}?
            // What was the message in Hill Cycle?
            [Question.HillCycleWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} in {0}？",
            },

            // Hogwarts
            // Which House was {1} solved for in {0}?
            // Which House was Binary Puzzle solved for in Hogwarts?
            [Question.HogwartsHouse] = new TranslationInfo
            {
                QuestionText = "Which House was {1} solved for in {0}？",
            },
            // Which module was solved for {1} in {0}?
            // Which module was solved for Gryffindor in Hogwarts?
            [Question.HogwartsModule] = new TranslationInfo
            {
                QuestionText = "Which module was solved for {1} in {0}？",
            },

            // Hold Ups
            // What was the name of the {1} shadow shown in {0}?
            // What was the name of the first shadow shown in Hold Ups?
            [Question.HoldUpsShadows] = new TranslationInfo
            {
                QuestionText = "What was the name of the {1} shadow shown in {0}？",
            },

            // Horrible Memory
            // In what position was the button pressed on the {1} stage of {0}?
            // In what position was the button pressed on the first stage of Horrible Memory?
            [Question.HorribleMemoryPositions] = new TranslationInfo
            {
                QuestionText = "In what position was the button pressed on the {1} stage of {0}？",
            },
            // What was the label of the button pressed on the {1} stage of {0}?
            // What was the label of the button pressed on the first stage of Horrible Memory?
            [Question.HorribleMemoryLabels] = new TranslationInfo
            {
                QuestionText = "What was the label of the button pressed on the {1} stage of {0}？",
            },
            // What color was the button pressed on the {1} stage of {0}?
            // What color was the button pressed on the first stage of Horrible Memory?
            [Question.HorribleMemoryColors] = new TranslationInfo
            {
                QuestionText = "What color was the button pressed on the {1} stage of {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "青",
                    ["green"] = "緑",
                    ["red"] = "赤",
                    ["orange"] = "オレンジ",
                    ["purple"] = "紫",
                    ["pink"] = "ピンク",
                },
            },

            // Homophones
            // What was the {1} displayed phrase in {0}?
            // What was the first displayed phrase in Homophones?
            [Question.HomophonesDisplayedPhrases] = new TranslationInfo
            {
                QuestionText = "What was the {1} displayed phrase in {0}？",
            },

            // Human Resources
            // Which was a descriptor shown in {1} in {0}?
            // Which was a descriptor shown in red in Human Resources?
            [Question.HumanResourcesDescriptors] = new TranslationInfo
            {
                QuestionText = "Which was a descriptor shown in {1} in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                },
            },
            // Who was {1} in {0}?
            // Who was fired in Human Resources?
            [Question.HumanResourcesHiredFired] = new TranslationInfo
            {
                QuestionText = "Who was {1} in {0}？",
            },

            // Hunting
            // Which of the first three stages of {0} had the {1} symbol {2}?
            // Which of the first three stages of Hunting had the column symbol first?
            [Question.HuntingColumnsRows] = new TranslationInfo
            {
                QuestionText = "{0}の最初3つのステージのうち、{2}番目の{1}シンボルを持っていたのはどれ？",
                Answers = new Dictionary<string, string>
                {
                    ["none"] = "なし",
                    ["first"] = "1",
                    ["second"] = "2",
                    ["first two"] = "最初と2番目",
                    ["third"] = "3",
                    ["first & third"] = "最初と最後",
                    ["second & third"] = "2番目と最後",
                    ["all three"] = "3つ全て",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["column"] = "列",
                    ["row"] = "段",
                },
            },

            // The Hypercube
            // What was the {1} rotation in {0}?
            // What was the first rotation in The Hypercube?
            [Question.HypercubeRotations] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の回転方向は？",
            },

            // The Hyperlink
            // What was the {1} character of the hyperlink in {0}?
            // What was the first character of the hyperlink in The Hyperlink?
            [Question.HyperlinkCharacters] = new TranslationInfo
            {
                QuestionText = "What was the {1} character of the hyperlink in {0}？",
            },
            // Which module was referenced on {0}?
            // Which module was referenced on The Hyperlink?
            [Question.HyperlinkAnswer] = new TranslationInfo
            {
                QuestionText = "Which module was referenced on {0}？",
            },

            // Ice Cream
            // Which one of these flavours {1} to the {2} customer in {0}?
            // Which one of these flavours was on offer, but not sold, to the first customer in Ice Cream?
            [Question.IceCreamFlavour] = new TranslationInfo
            {
                QuestionText = "{0}で{2}番目の客が{1}商品の一つにあるのは？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was on offer, but not sold,"] = "注文したが売らなかった",
                    ["was not on offer"] = "注文していない",
                },
            },
            // Who was the {1} customer in {0}?
            // Who was the first customer in Ice Cream?
            [Question.IceCreamCustomer] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の客は？",
            },

            // Identification Crisis
            // What was the {1} shape used in {0}?
            // What was the first shape used in Identification Crisis?
            [Question.IdentificationCrisisShape] = new TranslationInfo
            {
                QuestionText = "What was the {1} shape used in {0}？",
            },
            // What was the {1} identification module used in {0}?
            // What was the first identification module used in Identification Crisis?
            [Question.IdentificationCrisisDataset] = new TranslationInfo
            {
                QuestionText = "What was the {1} identification module used in {0}？",
            },

            // Identity Parade
            // Which hair color {1} listed in {0}?
            // Which hair color was listed in Identity Parade?
            [Question.IdentityParadeHairColors] = new TranslationInfo
            {
                QuestionText = "Which hair color {1} listed in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Black"] = "黒",
                    ["Blonde"] = "ブロンド",
                    ["Brown"] = "茶",
                    ["Grey"] = "灰",
                    ["Red"] = "赤",
                    ["White"] = "白",
                },
            },
            // Which build {1} listed in {0}?
            // Which build was listed in Identity Parade?
            [Question.IdentityParadeBuilds] = new TranslationInfo
            {
                QuestionText = "{0}のリストに{1}のはどの身体的特徴？",
            },
            // Which attire {1} listed in {0}?
            // Which attire was listed in Identity Parade?
            [Question.IdentityParadeAttires] = new TranslationInfo
            {
                QuestionText = "{0}のリストに{1}のはどの服装？",
            },

            // Indigo Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Indigo Cipher?
            [Question.IndigoCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // Infinite Loop
            // What was the selected word in {0}?
            // What was the selected word in Infinite Loop?
            [Question.InfiniteLoopSelectedWord] = new TranslationInfo
            {
                QuestionText = "What was the selected word in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["anchor"] = "anchor",
                    ["axions"] = "axions",
                    ["brutal"] = "brutal",
                    ["bunker"] = "bunker",
                    ["ceased"] = "ceased",
                    ["cypher"] = "cypher",
                    ["demote"] = "demote",
                    ["devoid"] = "devoid",
                    ["ejects"] = "ejects",
                    ["expend"] = "expend",
                    ["fixate"] = "fixate",
                    ["fondly"] = "fondly",
                    ["geyser"] = "geyser",
                    ["guitar"] = "guitar",
                    ["hexing"] = "hexing",
                    ["hybrid"] = "hybrid",
                    ["incite"] = "incite",
                    ["inject"] = "inject",
                    ["jacked"] = "jacked",
                    ["jigsaw"] = "jigsaw",
                    ["kayaks"] = "kayaks",
                    ["komodo"] = "komodo",
                    ["lazuli"] = "lazuli",
                    ["logjam"] = "logjam",
                    ["maimed"] = "maimed",
                    ["musket"] = "musket",
                    ["nebula"] = "nebula",
                    ["nuking"] = "nuking",
                    ["overdo"] = "overdo",
                    ["oblong"] = "oblong",
                    ["photon"] = "photon",
                    ["probed"] = "probed",
                    ["quartz"] = "quartz",
                    ["quebec"] = "quebec",
                    ["refute"] = "refute",
                    ["regime"] = "regime",
                    ["sierra"] = "sierra",
                    ["swerve"] = "swerve",
                    ["tenacy"] = "tenacy",
                    ["thymes"] = "thymes",
                    ["ultima"] = "ultima",
                    ["utopia"] = "utopia",
                    ["valved"] = "valved",
                    ["viable"] = "viable",
                    ["wither"] = "wither",
                    ["wrench"] = "wrench",
                    ["xenons"] = "xenons",
                    ["xylose"] = "xylose",
                    ["yanked"] = "yanked",
                    ["yellow"] = "yellow",
                    ["zigged"] = "zigged",
                    ["zodiac"] = "zodiac",
                },
            },

            // Ingredients
            // Which ingredient was used in {0}?
            // Which ingredient was used in Ingredients?
            [Question.IngredientsIngredients] = new TranslationInfo
            {
                QuestionText = "Which ingredient was used in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Veal"] = "Veal",
                    ["Beef"] = "Beef",
                    ["Quail"] = "Quail",
                    ["FiletMignon"] = "FiletMignon",
                    ["Crab"] = "Crab",
                    ["Scallop"] = "Scallop",
                    ["Lobster"] = "Lobster",
                    ["Sole"] = "Sole",
                    ["Eel"] = "Eel",
                    ["SeaBass"] = "SeaBass",
                    ["Mussel"] = "Mussel",
                    ["Cod"] = "Cod",
                    ["Pumpkin"] = "Pumpkin",
                    ["Zucchini"] = "Zucchini",
                    ["Onion"] = "Onion",
                    ["Tomato"] = "Tomato",
                    ["Eggplant"] = "Eggplant",
                    ["Carrot"] = "Carrot",
                    ["Garlic"] = "Garlic",
                    ["Celery"] = "Celery",
                    ["Morel"] = "Morel",
                    ["Porcini"] = "Porcini",
                    ["Chanterelle"] = "Chanterelle",
                    ["Portobello"] = "Portobello",
                    ["BlackTruffle"] = "BlackTruffle",
                    ["KingOysterMushroom"] = "KingOysterMushroom",
                    ["BlackTrumpet"] = "BlackTrumpet",
                    ["MillerMushroom"] = "MillerMushroom",
                    ["Cloves"] = "Cloves",
                    ["Rosemary"] = "Rosemary",
                    ["Thyme"] = "Thyme",
                    ["BayLeaf"] = "BayLeaf",
                    ["Basil"] = "Basil",
                    ["Dill"] = "Dill",
                    ["Parsley"] = "Parsley",
                    ["Saffron"] = "Saffron",
                    ["Apricot"] = "Apricot",
                    ["Gooseberry"] = "Gooseberry",
                    ["Lemon"] = "Lemon",
                    ["Orange"] = "オレンジ",
                    ["Raspberry"] = "Raspberry",
                    ["Pear"] = "Pear",
                    ["Blackberry"] = "Blackberry",
                    ["Apple"] = "Apple",
                    ["Cheese"] = "Cheese",
                    ["Chocolate"] = "Chocolate",
                    ["Caviar"] = "Caviar",
                    ["Butter"] = "Butter",
                    ["OliveOil"] = "OliveOil",
                    ["Cornichon"] = "Cornichon",
                    ["Rice"] = "Rice",
                    ["Honey"] = "Honey",
                    ["SourCherry"] = "SourCherry",
                    ["Strawberry"] = "Strawberry",
                    ["BloodOrange"] = "BloodOrange",
                    ["Banana"] = "Banana",
                    ["Grapes"] = "Grapes",
                    ["Melon"] = "Melon",
                    ["Watermelon"] = "Watermelon",
                },
            },
            // Which ingredient was listed but not used in {0}?
            // Which ingredient was listed but not used in Ingredients?
            [Question.IngredientsNonIngredients] = new TranslationInfo
            {
                QuestionText = "Which ingredient was listed but not used in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Veal"] = "Veal",
                    ["Beef"] = "Beef",
                    ["Quail"] = "Quail",
                    ["FiletMignon"] = "FiletMignon",
                    ["Crab"] = "Crab",
                    ["Scallop"] = "Scallop",
                    ["Lobster"] = "Lobster",
                    ["Sole"] = "Sole",
                    ["Eel"] = "Eel",
                    ["SeaBass"] = "SeaBass",
                    ["Mussel"] = "Mussel",
                    ["Cod"] = "Cod",
                    ["Pumpkin"] = "Pumpkin",
                    ["Zucchini"] = "Zucchini",
                    ["Onion"] = "Onion",
                    ["Tomato"] = "Tomato",
                    ["Eggplant"] = "Eggplant",
                    ["Carrot"] = "Carrot",
                    ["Garlic"] = "Garlic",
                    ["Celery"] = "Celery",
                    ["Morel"] = "Morel",
                    ["Porcini"] = "Porcini",
                    ["Chanterelle"] = "Chanterelle",
                    ["Portobello"] = "Portobello",
                    ["BlackTruffle"] = "BlackTruffle",
                    ["KingOysterMushroom"] = "KingOysterMushroom",
                    ["BlackTrumpet"] = "BlackTrumpet",
                    ["MillerMushroom"] = "MillerMushroom",
                    ["Cloves"] = "Cloves",
                    ["Rosemary"] = "Rosemary",
                    ["Thyme"] = "Thyme",
                    ["BayLeaf"] = "BayLeaf",
                    ["Basil"] = "Basil",
                    ["Dill"] = "Dill",
                    ["Parsley"] = "Parsley",
                    ["Saffron"] = "Saffron",
                    ["Apricot"] = "Apricot",
                    ["Gooseberry"] = "Gooseberry",
                    ["Lemon"] = "Lemon",
                    ["Orange"] = "Orange",
                    ["Raspberry"] = "Raspberry",
                    ["Pear"] = "Pear",
                    ["Blackberry"] = "Blackberry",
                    ["Apple"] = "Apple",
                    ["Cheese"] = "Cheese",
                    ["Chocolate"] = "Chocolate",
                    ["Caviar"] = "Caviar",
                    ["Butter"] = "Butter",
                    ["OliveOil"] = "OliveOil",
                    ["Cornichon"] = "Cornichon",
                    ["Rice"] = "Rice",
                    ["Honey"] = "Honey",
                    ["SourCherry"] = "SourCherry",
                    ["Strawberry"] = "Strawberry",
                    ["BloodOrange"] = "BloodOrange",
                    ["Banana"] = "Banana",
                    ["Grapes"] = "Grapes",
                    ["Melon"] = "Melon",
                    ["Watermelon"] = "Watermelon",
                },
            },

            // Inner Connections
            // What color was the LED in {0}?
            // What color was the LED in Inner Connections?
            [Question.InnerConnectionsLED] = new TranslationInfo
            {
                QuestionText = "What color was the LED in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Black"] = "黒",
                    ["Blue"] = "青",
                    ["Red"] = "赤",
                    ["White"] = "白",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                },
            },
            // What was the digit flashed in Morse in {0}?
            // What was the digit flashed in Morse in Inner Connections?
            [Question.InnerConnectionsMorse] = new TranslationInfo
            {
                QuestionText = "What was the digit flashed in Morse in {0}？",
            },

            // Interpunct
            // What was the symbol displayed in stage {1} of {0}?
            // What was the symbol displayed in stage 1 of Interpunct?
            [Question.InterpunctDisplay] = new TranslationInfo
            {
                QuestionText = "What was the symbol displayed in stage {1} of {0}？",
            },

            // IPA
            // What symbol was the correct answer in {0}?
            // What symbol was the correct answer in IPA?
            [Question.IpaSymbol] = new TranslationInfo
            {
                QuestionText = "What symbol was the correct answer in {0}？",
            },

            // The iPhone
            // What was the {1} PIN digit in {0}?
            // What was the first PIN digit in The iPhone?
            [Question.iPhoneDigits] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のPINの数字は？",
            },

            // Jenga
            // Which symbol was on the first correctly pulled block in {0}?
            // Which symbol was on the first correctly pulled block in Jenga?
            [Question.JengaFirstBlock] = new TranslationInfo
            {
                QuestionText = "Which symbol was on the first correctly pulled block in {0}?",
            },

            // The Jewel Vault
            // What number was wheel {1} in {0}?
            // What number was wheel A in The Jewel Vault?
            [Question.JewelVaultWheels] = new TranslationInfo
            {
                QuestionText = "What number was wheel {1} in {0}？",
            },

            // Jumble Cycle
            // What was the {1} in {0}?
            // What was the message in Jumble Cycle?
            [Question.JumbleCycleWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} in {0}？",
            },

            // The Kanye Encounter
            // What was a food item displayed in {0}?
            // What was a food item displayed in The Kanye Encounter?
            [Question.KanyeEncounterFoods] = new TranslationInfo
            {
                QuestionText = "What was a food item displayed in {0}？",
            },

            // Keypad Magnified
            // What was the position of the LED in {0}?
            // What was the position of the LED in Keypad Magnified?
            [Question.KeypadMagnifiedLED] = new TranslationInfo
            {
                QuestionText = "What was the position of the LED in {0}？",
            },

            // Kudosudoku
            // Which square was {1} in {0}?
            // Which square was pre-filled in Kudosudoku?
            [Question.KudosudokuPrefilled] = new TranslationInfo
            {
                QuestionText = "{0}で最初に{1}四角はどれ？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["pre-filled"] = "埋められていた",
                    ["not pre-filled"] = "埋められていなかった",
                },
            },

            // The Labyrinth
            // Where was one of the portals in layer {1} in {0}?
            // Where was one of the portals in layer 1 (Red) in The Labyrinth?
            [Question.LabyrinthPortalLocations] = new TranslationInfo
            {
                QuestionText = "Where was one of the portals in layer {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["1 (Red)"] = "1 (Red)",
                    ["2 (Orange)"] = "2 (Orange)",
                    ["3 (Yellow)"] = "3 (Yellow)",
                    ["4 (Green)"] = "4 (Green)",
                    ["5 (Blue)"] = "5 (Blue)",
                },
            },
            // In which layer was this portal in {0}?
            // In which layer was this portal in The Labyrinth?
            [Question.LabyrinthPortalStage] = new TranslationInfo
            {
                QuestionText = "In which layer was this portal in {0}?",
            },

            // Ladders
            // Which color was present on the second ladder in {0}?
            // Which color was present on the second ladder in Ladders?
            [Question.LaddersStage2Colors] = new TranslationInfo
            {
                QuestionText = "Which color was present on the second ladder in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Cyan"] = "シアン",
                    ["Purple"] = "紫",
                    ["Gray"] = "灰",
                },
            },
            // What color was missing on the third ladder in {0}?
            // What color was missing on the third ladder in Ladders?
            [Question.LaddersStage3Missing] = new TranslationInfo
            {
                QuestionText = "What color was missing on the third ladder in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Cyan"] = "シアン",
                    ["Purple"] = "紫",
                    ["Gray"] = "灰",
                },
            },

            // Lasers
            // What was the number on the {1} hatch on {0}?
            // What was the number on the top-left hatch on Lasers?
            [Question.LasersHatches] = new TranslationInfo
            {
                QuestionText = "What was the number on the {1} hatch on {0}？",
            },

            // LED Encryption
            // What was the correct letter you pressed in the {1} stage of {0}?
            // What was the correct letter you pressed in the first stage of LED Encryption?
            [Question.LEDEncryptionPressedLetters] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で押した正しい文字は？",
            },

            // LED Math
            // What color was {1} in {0}?
            // What color was LED A in LED Math?
            [Question.LEDMathLights] = new TranslationInfo
            {
                QuestionText = "What color was {1} in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                },
            },

            // LEDs
            // What was the initial color of the changed LED in {0}?
            // What was the initial color of the changed LED in LEDs?
            [Question.LEDsOriginalColor] = new TranslationInfo
            {
                QuestionText = "What was the initial color of the changed LED in {0}？",
            },

            // LEGOs
            // What were the dimensions of the {1} piece in {0}?
            // What were the dimensions of the red piece in LEGOs?
            [Question.LEGOsPieceDimensions] = new TranslationInfo
            {
                QuestionText = "What were the dimensions of the {1} piece in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["cyan"] = "シアン",
                    ["magenta"] = "マゼンタ",
                    ["yellow"] = "黄",
                },
            },

            // Letter Math
            // What was the letter on the {1} display in {0}?
            // What was the letter on the left display in Letter Math?
            [Question.LetterMathDisplay] = new TranslationInfo
            {
                QuestionText = "What was the letter on the {1} display in {0}？",
            },

            // Linq
            // What was the {1} function in {0}?
            // What was the first function in Linq?
            [Question.LinqFunction] = new TranslationInfo
            {
                QuestionText = "What was the {1} function in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["First"] = "First",
                    ["Last"] = "Last",
                    ["Min"] = "Min",
                    ["Max"] = "Max",
                    ["Distinct"] = "Distinct",
                    ["Skip"] = "Skip",
                    ["SkipLast"] = "SkipLast",
                    ["Take"] = "Take",
                    ["TakeLast"] = "TakeLast",
                    ["ElementAt"] = "ElementAt",
                    ["Except"] = "Except",
                    ["Intersect"] = "Intersect",
                    ["Concat"] = "Concat",
                    ["Append"] = "Append",
                    ["Prepend"] = "Prepend",
                },
            },

            // Lion’s Share
            // Which year was displayed on {0}?
            // Which year was displayed on Lion’s Share?
            [Question.LionsShareYear] = new TranslationInfo
            {
                QuestionText = "Which year was displayed on {0}？",
            },
            // Which lion was present but removed in {0}?
            // Which lion was present but removed in Lion’s Share?
            [Question.LionsShareRemovedLions] = new TranslationInfo
            {
                QuestionText = "Which lion was present but removed in {0}？",
            },

            // Listening
            // What was the correct code you entered in {0}?
            // What was the correct code you entered in Listening?
            [Question.ListeningCode] = new TranslationInfo
            {
                QuestionText = "{0}で入力した正しいコードは？",
            },

            // Logical Buttons
            // What was the color of the {1} button in the {2} stage of {0}?
            // What was the color of the top button in the first stage of Logical Buttons?
            [Question.LogicalButtonsColor] = new TranslationInfo
            {
                QuestionText = "What was the color of the {1} button in the {2} stage of {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                    ["Purple"] = "紫",
                    ["White"] = "白",
                    ["Orange"] = "オレンジ",
                    ["Cyan"] = "シアン",
                    ["Grey"] = "灰",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "上",
                    ["bottom-left"] = "左下",
                },
            },
            // What was the label on the {1} button in the {2} stage of {0}?
            // What was the label on the top button in the first stage of Logical Buttons?
            [Question.LogicalButtonsLabel] = new TranslationInfo
            {
                QuestionText = "What was the label on the {1} button in the {2} stage of {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "上",
                    ["bottom-left"] = "左下",
                },
            },
            // What was the final operator in the {1} stage of {0}?
            // What was the final operator in the first stage of Logical Buttons?
            [Question.LogicalButtonsOperator] = new TranslationInfo
            {
                QuestionText = "What was the final operator in the {1} stage of {0}？",
            },

            // Logic Gates
            // What was {1} in {0}?
            // What was gate A in Logic Gates?
            [Question.LogicGatesGates] = new TranslationInfo
            {
                QuestionText = "{0}で{1}はどれだったか？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["gate A"] = "ゲートA",
                    ["gate B"] = "ゲートB",
                    ["gate C"] = "ゲートC",
                    ["gate D"] = "ゲートD",
                    ["gate E"] = "ゲートE",
                    ["gate F"] = "ゲートF",
                    ["gate G"] = "ゲートG",
                    ["the duplicated gate"] = "重複したゲート",
                },
            },

            // Lombax Cubes
            // What was the {1} letter on the button in {0}?
            // What was the first letter on the button in Lombax Cubes?
            [Question.LombaxCubesLetters] = new TranslationInfo
            {
                QuestionText = "What was the {1} letter on the button in {0}？",
            },

            // The London Underground
            // Where did the {1} journey on {0} {2}?
            // Where did the first journey on The London Underground depart from?
            [Question.LondonUndergroundStations] = new TranslationInfo
            {
                QuestionText = "Where did the {1} journey on {0} {2}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["depart from"] = "depart from",
                },
            },

            // Mahjong
            // Which tile was part of the {1} matched pair in {0}?
            // Which tile was part of the first matched pair in Mahjong?
            [Question.MahjongMatches] = new TranslationInfo
            {
                QuestionText = "Which tile was part of the {1} matched pair in {0}？",
            },
            // Which tile was shown in the bottom-left of {0}?
            // Which tile was shown in the bottom-left of Mahjong?
            [Question.MahjongCountingTile] = new TranslationInfo
            {
                QuestionText = "Which tile was shown in the bottom-left of {0}？",
            },

            // Mafia
            // Who was a player, but not the Godfather, in {0}?
            // Who was a player, but not the Godfather, in Mafia?
            [Question.MafiaPlayers] = new TranslationInfo
            {
                QuestionText = "Who was a player, but not the Godfather, in {0}？",
            },

            // Magenta Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Magenta Cipher?
            [Question.MagentaCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was on the {1} screen on page {2} in {0}?",
            },

            // M&Ms
            // What color was the text on the {1} button in {0}?
            // What color was the text on the first button in M&Ms?
            [Question.MandMsColors] = new TranslationInfo
            {
                QuestionText = "What color was the text on the {1} button in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["orange"] = "オレンジ",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                    ["brown"] = "茶",
                },
            },
            // What was the text on the {1} button in {0}?
            // What was the text on the first button in M&Ms?
            [Question.MandMsLabels] = new TranslationInfo
            {
                QuestionText = "What was the text on the {1} button in {0}？",
            },

            // M&Ns
            // What color was the text on the {1} button in {0}?
            // What color was the text on the first button in M&Ns?
            [Question.MandNsColors] = new TranslationInfo
            {
                QuestionText = "What color was the text on the {1} button in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["orange"] = "オレンジ",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                    ["brown"] = "茶",
                },
            },
            // What was the text on the correct button in {0}?
            // What was the text on the correct button in M&Ns?
            [Question.MandNsLabel] = new TranslationInfo
            {
                QuestionText = "What was the text on the correct button in {0}？",
            },

            // Maritime Flags
            // What bearing was signalled in {0}?
            // What bearing was signalled in Maritime Flags?
            [Question.MaritimeFlagsBearing] = new TranslationInfo
            {
                QuestionText = "What bearing was signalled in {0}？",
            },
            // Which callsign was signalled in {0}?
            // Which callsign was signalled in Maritime Flags?
            [Question.MaritimeFlagsCallsign] = new TranslationInfo
            {
                QuestionText = "Which callsign was signalled in {0}？",
            },

            // Maroon Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Maroon Cipher?
            [Question.MaroonCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was on the {1} screen on page {2} in {0}?",
            },

            // Mashematics
            // What was the answer in {0}?
            // What was the answer in Mashematics?
            [Question.MashematicsAnswer] = new TranslationInfo
            {
                QuestionText = "{0}の解は？",
            },
            // What was the {1} number in the equation on {0}?
            // What was the first number in the equation on Mashematics?
            [Question.MashematicsCalculation] = new TranslationInfo
            {
                QuestionText = "{0}の問題内にあった{1}番目の数字は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["�ordinal"] = "�ordinal",
                },
            },

            // Math ’em
            // What was the color of this tile before the shuffle on {0}?
            // What was the color of this tile before the shuffle on Math ’em?
            [Question.MathEmColor] = new TranslationInfo
            {
                QuestionText = "What was the color of this tile before the shuffle on {0}?",
            },
            // What was the design on this tile before the shuffle on {0}?
            // What was the design on this tile before the shuffle on Math ’em?
            [Question.MathEmLabel] = new TranslationInfo
            {
                QuestionText = "What was the design on this tile before the shuffle on {0}?",
            },

            // The Matrix
            // Which word was part of the latest access code in {0}?
            // Which word was part of the latest access code in The Matrix?
            [Question.MatrixAccessCode] = new TranslationInfo
            {
                QuestionText = "Which word was part of the latest access code in {0}？",
            },
            // What was the glitched word in {0}?
            // What was the glitched word in The Matrix?
            [Question.MatrixGlitchWord] = new TranslationInfo
            {
                QuestionText = "What was the glitched word in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["headjack"] = "headjack",
                    ["phone"] = "phone",
                    ["dystopia"] = "dystopia",
                    ["control"] = "control",
                    ["paradise"] = "paradise",
                    ["utopia"] = "utopia",
                    ["version"] = "version",
                    ["nebuchadnezzar"] = "nebuchadnezzar",
                    ["zion"] = "zion",
                    ["fight"] = "fight",
                    ["utopia"] = "utopia",
                    ["mind"] = "mind",
                    ["squiddy"] = "squiddy",
                    ["guns"] = "guns",
                    ["trace"] = "trace",
                    ["spoon"] = "spoon",
                    ["machine"] = "machine",
                    ["red"] = "red",
                    ["white"] = "white",
                    ["paradise"] = "paradise",
                    ["metacortex"] = "metacortex",
                    ["flint"] = "flint",
                    ["nova"] = "nova",
                    ["white"] = "white",
                    ["rabbit"] = "rabbit",
                    ["follow"] = "follow",
                    ["matrix"] = "matrix",
                    ["free"] = "free",
                    ["neural"] = "neural",
                    ["mind"] = "mind",
                    ["fight"] = "fight",
                    ["free"] = "free",
                    ["nova"] = "nova",
                    ["blue"] = "blue",
                    ["fields"] = "fields",
                    ["choice"] = "choice",
                    ["battery"] = "battery",
                    ["program"] = "program",
                    ["flint"] = "flint",
                    ["headjack"] = "headjack",
                    ["kungfu"] = "kungfu",
                    ["choi"] = "choi",
                    ["red"] = "red",
                    ["blue"] = "blue",
                    ["pill"] = "pill",
                    ["jump"] = "jump",
                    ["program"] = "program",
                    ["agent"] = "agent",
                    ["sentient"] = "sentient",
                    ["squiddy"] = "squiddy",
                    ["dystopia"] = "dystopia",
                    ["rabbit"] = "rabbit",
                    ["jump"] = "jump",
                    ["code"] = "code",
                    ["mirror"] = "mirror",
                    ["cookie"] = "cookie",
                    ["human"] = "human",
                    ["pill"] = "pill",
                    ["follow"] = "follow",
                    ["version"] = "version",
                    ["sentinel"] = "sentinel",
                    ["machine"] = "machine",
                    ["prison"] = "prison",
                    ["human"] = "human",
                    ["fields"] = "fields",
                    ["battery"] = "battery",
                    ["code"] = "code",
                    ["training"] = "training",
                    ["guns"] = "guns",
                    ["hel"] = "hel",
                    ["elevator"] = "elevator",
                    ["sentinel"] = "sentinel",
                    ["choi"] = "choi",
                    ["matrix"] = "matrix",
                    ["nebuchadnezzar"] = "nebuchadnezzar",
                    ["control"] = "control",
                    ["metacortex"] = "metacortex",
                    ["sentient"] = "sentient",
                    ["unplug"] = "unplug",
                    ["hardwire"] = "hardwire",
                    ["trainman"] = "trainman",
                    ["spoon"] = "spoon",
                    ["cookie"] = "cookie",
                    ["elevator"] = "elevator",
                    ["hardwire"] = "hardwire",
                    ["choice"] = "choice",
                    ["trace"] = "trace",
                    ["mirror"] = "mirror",
                    ["unplug"] = "unplug",
                    ["interface"] = "interface",
                    ["prison"] = "prison",
                    ["kungfu"] = "kungfu",
                    ["interface"] = "interface",
                    ["neural"] = "neural",
                    ["trainman"] = "trainman",
                    ["hel"] = "hel",
                    ["agent"] = "agent",
                    ["training"] = "training",
                    ["zion"] = "zion",
                    ["phone"] = "phone",
                },
            },

            // Maze
            // In which {1} was the starting position in {0}, counting from the {2}?
            // In which column was the starting position in Maze, counting from the left?
            [Question.MazeStartingPosition] = new TranslationInfo
            {
                QuestionText = "{0}のスタート地点の{1}は{2}から何番目？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["column"] = "列",
                    ["left"] = "左",
                    ["row"] = "段",
                    ["top"] = "上",
                },
            },

            // Maze³
            // What was the color of the starting face in {0}?
            // What was the color of the starting face in Maze³?
            [Question.Maze3StartingFace] = new TranslationInfo
            {
                QuestionText = "What was the color of the starting face in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Magenta"] = "マゼンタ",
                    ["Orange"] = "オレンジ",
                },
            },

            // Maze Identification
            // What was the seed of the maze in {0}?
            // What was the seed of the maze in Maze Identification?
            [Question.MazeIdentificationSeed] = new TranslationInfo
            {
                QuestionText = "What was the seed of the maze in {0}？",
            },
            // What was the function of button {1} in {0}?
            // What was the function of button 1 in Maze Identification?
            [Question.MazeIdentificationNum] = new TranslationInfo
            {
                QuestionText = "What was the function of button {1} in {0}？",
            },
            // Which button {1} in {0}?
            // Which button moved you forwards in Maze Identification?
            [Question.MazeIdentificationFunc] = new TranslationInfo
            {
                QuestionText = "Which button {1} in {0}？",
            },

            // Mazematics
            // Which was the {1} value in {0}?
            // Which was the initial value in Mazematics?
            [Question.MazematicsValue] = new TranslationInfo
            {
                QuestionText = "Which was the {1} value in {0}？",
            },

            // Maze Scrambler
            // What was the starting position on {0}?
            // What was the starting position on Maze Scrambler?
            [Question.MazeScramblerStart] = new TranslationInfo
            {
                QuestionText = "{0}の開始位置は？",
            },
            // What was the goal on {0}?
            // What was the goal on Maze Scrambler?
            [Question.MazeScramblerGoal] = new TranslationInfo
            {
                QuestionText = "{0}のゴール位置は？",
            },
            // Which of these positions was a maze marking on {0}?
            // Which of these positions was a maze marking on Maze Scrambler?
            [Question.MazeScramblerIndicators] = new TranslationInfo
            {
                QuestionText = "{0}の迷路を求めるマークの位置はどれ？",
            },

            // Mazeseeker
            // How many walls surrounded this cell in {0}?
            // How many walls surrounded this cell in Mazeseeker?
            [Question.MazeseekerCell] = new TranslationInfo
            {
                QuestionText = "How many walls surrounded this cell in {0}？",
            },
            // Where was the start in {0}?
            // Where was the start in Mazeseeker?
            [Question.MazeseekerStart] = new TranslationInfo
            {
                QuestionText = "Where was the start in {0}？",
            },
            // Where was the goal in {0}?
            // Where was the goal in Mazeseeker?
            [Question.MazeseekerGoal] = new TranslationInfo
            {
                QuestionText = "Where was the goal in {0}？",
            },

            // Mega Man 2
            // Who was the master shown in {0}?
            // Who was the master shown in Mega Man 2?
            [Question.MegaMan2SelectedMaster] = new TranslationInfo
            {
                QuestionText = "Who was the master shown in {0}？",
            },
            // Whose weapon was shown in {0}?
            // Whose weapon was shown in Mega Man 2?
            [Question.MegaMan2SelectedWeapon] = new TranslationInfo
            {
                QuestionText = "Whose weapon was shown in {0}？",
            },

            // Melody Sequencer
            // Which part was in slot #{1} at the start of {0}?
            // Which part was in slot #1 at the start of Melody Sequencer?
            [Question.MelodySequencerSlots] = new TranslationInfo
            {
                QuestionText = "Which part was in slot #{1} at the start of {0}？",
            },
            // Which slot contained part #{1} at the start of {0}?
            // Which slot contained part #1 at the start of Melody Sequencer?
            [Question.MelodySequencerParts] = new TranslationInfo
            {
                QuestionText = "Which slot contained part #{1} at the start of {0}？",
            },

            // Memorable Buttons
            // What was the {1} correct symbol pressed in {0}?
            // What was the first correct symbol pressed in Memorable Buttons?
            [Question.MemorableButtonsSymbols] = new TranslationInfo
            {
                QuestionText = "What was the {1} correct symbol pressed in {0}？",
            },

            // Memory
            // What was the displayed number in the {1} stage of {0}?
            // What was the displayed number in the first stage of Memory?
            [Question.MemoryDisplay] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で表示された数は？",
            },
            // In what position was the button that you pressed in the {1} stage of {0}?
            // In what position was the button that you pressed in the first stage of Memory?
            [Question.MemoryPosition] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で押したボタンの位置は？",
            },
            // What was the label of the button that you pressed in the {1} stage of {0}?
            // What was the label of the button that you pressed in the first stage of Memory?
            [Question.MemoryLabel] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で押したボタンのラベルは？",
            },

            // Metamorse
            // What was the extracted letter in {0}?
            // What was the extracted letter in Metamorse?
            [Question.MetamorseExtractedLetter] = new TranslationInfo
            {
                QuestionText = "What was the extracted letter in {0}？",
            },

            // Microcontroller
            // Which pin lit up {1} in {0}?
            // Which pin lit up first in Microcontroller?
            [Question.MicrocontrollerPinOrder] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に点灯したピンは？",
            },

            // Minesweeper
            // What was the color of the starting cell in {0}?
            // What was the color of the starting cell in Minesweeper?
            [Question.MinesweeperStartingColor] = new TranslationInfo
            {
                QuestionText = "{0}の開始のマスは何色？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["orange"] = "オレンジ",
                    ["yellow"] = "黄",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["purple"] = "紫",
                    ["black"] = "黒",
                },
            },

            // Mirror
            // What was the second word written by the original ghost in {0}?
            // What was the second word written by the original ghost in Mirror?
            [Question.MirrorWord] = new TranslationInfo
            {
                QuestionText = "What was the second word written by the original ghost in {0}？",
            },

            // Mister Softee
            // Where was the SpongeBob Bar on {0}?
            // Where was the SpongeBob Bar on Mister Softee?
            [Question.MisterSofteeSpongebobPosition] = new TranslationInfo
            {
                QuestionText = "Where was the SpongeBob Bar on {0}？",
            },
            // Which treat was present on {0}?
            // Which treat was present on Mister Softee?
            [Question.MisterSofteeTreatsPresent] = new TranslationInfo
            {
                QuestionText = "Which treat was present on {0}？",
            },

            // Modern Cipher
            // What was the decrypted word of the {1} stage in {0}?
            // What was the decrypted word of the first stage in Modern Cipher?
            [Question.ModernCipherWord] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で復号された単語は？",
                Answers = new Dictionary<string, string>
                {
                    ["Absent"] = "Absent",
                    ["Abstract"] = "Abstract",
                    ["Abysmal"] = "Abysmal",
                    ["Accident"] = "Accident",
                    ["Activate"] = "Activate",
                    ["Adjacent"] = "Adjacent",
                    ["Afraid"] = "Afraid",
                    ["Agenda"] = "Agenda",
                    ["Agony"] = "Agony",
                    ["Alchemy"] = "Alchemy",
                    ["Alcohol"] = "Alcohol",
                    ["Alive"] = "Alive",
                    ["Allergic"] = "Allergic",
                    ["Allergy"] = "Allergy",
                    ["Alpha"] = "Alpha",
                    ["Alphabet"] = "Alphabet",
                    ["Already"] = "Already",
                    ["Amethyst"] = "Amethyst",
                    ["Amnesty"] = "Amnesty",
                    ["Amperage"] = "Amperage",
                    ["Ancient"] = "Ancient",
                    ["Animals"] = "Animals",
                    ["Animate"] = "Animate",
                    ["Anthrax"] = "Anthrax",
                    ["Anxious"] = "Anxious",
                    ["Aquarium"] = "Aquarium",
                    ["Aquarius"] = "Aquarius",
                    ["Arcade"] = "Arcade",
                    ["Arrange"] = "Arrange",
                    ["Arrow"] = "Arrow",
                    ["Artefact"] = "Artefact",
                    ["Asterisk"] = "Asterisk",
                    ["Atrophy"] = "Atrophy",
                    ["Audio"] = "Audio",
                    ["Author"] = "Author",
                    ["Avoid"] = "Avoid",
                    ["Awesome"] = "Awesome",
                    ["Balance"] = "Balance",
                    ["Banana"] = "Banana",
                    ["Bandit"] = "Bandit",
                    ["Bankrupt"] = "Bankrupt",
                    ["Basket"] = "Basket",
                    ["Battle"] = "Battle",
                    ["Bazaar"] = "Bazaar",
                    ["Beard"] = "Beard",
                    ["Beauty"] = "Beauty",
                    ["Beaver"] = "Beaver",
                    ["Becoming"] = "Becoming",
                    ["Beetle"] = "Beetle",
                    ["Beseech"] = "Beseech",
                    ["Between"] = "Between",
                    ["Bicycle"] = "Bicycle",
                    ["Bigger"] = "Bigger",
                    ["Biggest"] = "Biggest",
                    ["Biology"] = "Biology",
                    ["Birthday"] = "Birthday",
                    ["Bistro"] = "Bistro",
                    ["Bites"] = "Bites",
                    ["Blight"] = "Blight",
                    ["Blockade"] = "Blockade",
                    ["Blubber"] = "Blubber",
                    ["Bomb"] = "Bomb",
                    ["Bonobo"] = "Bonobo",
                    ["Books"] = "Books",
                    ["Bottle"] = "Bottle",
                    ["Brazil"] = "Brazil",
                    ["Brief"] = "Brief",
                    ["Broccoli"] = "Broccoli",
                    ["Broken"] = "Broken",
                    ["Brother"] = "Brother",
                    ["Bubble"] = "Bubble",
                    ["Budget"] = "Budget",
                    ["Bulkhead"] = "Bulkhead",
                    ["Bumper"] = "Bumper",
                    ["Bunny"] = "Bunny",
                    ["Button"] = "Button",
                    ["Bytes"] = "Bytes",
                    ["Cables"] = "Cables",
                    ["Caliber"] = "Caliber",
                    ["Campaign"] = "Campaign",
                    ["Canada"] = "Canada",
                    ["Canister"] = "Canister",
                    ["Caption"] = "Caption",
                    ["Caution"] = "Caution",
                    ["Cavity"] = "Cavity",
                    ["Chalk"] = "Chalk",
                    ["Chamber"] = "Chamber",
                    ["Chamfer"] = "Chamfer",
                    ["Champion"] = "Champion",
                    ["Changes"] = "Changes",
                    ["Chicken"] = "Chicken",
                    ["Children"] = "Children",
                    ["Chlorine"] = "Chlorine",
                    ["Chord"] = "Chord",
                    ["Chronic"] = "Chronic",
                    ["Church"] = "Church",
                    ["Cinnamon"] = "Cinnamon",
                    ["Civic"] = "Civic",
                    ["Cleric"] = "Cleric",
                    ["Clock"] = "Clock",
                    ["Cocoon"] = "Cocoon",
                    ["Combat"] = "Combat",
                    ["Combine"] = "Combine",
                    ["Comedy"] = "Comedy",
                    ["Comics"] = "Comics",
                    ["Comma"] = "Comma",
                    ["Command"] = "Command",
                    ["Comment"] = "Comment",
                    ["Compost"] = "Compost",
                    ["Computer"] = "Computer",
                    ["Condom"] = "Condom",
                    ["Conflict"] = "Conflict",
                    ["Consider"] = "Consider",
                    ["Contour"] = "Contour",
                    ["Control"] = "Control",
                    ["Corrupt"] = "Corrupt",
                    ["Costume"] = "Costume",
                    ["Criminal"] = "Criminal",
                    ["Crunch"] = "Crunch",
                    ["Cryptic"] = "Cryptic",
                    ["Cuboid"] = "Cuboid",
                    ["Cypher"] = "Cypher",
                    ["Daddy"] = "Daddy",
                    ["Dancer"] = "Dancer",
                    ["Dancing"] = "Dancing",
                    ["Daughter"] = "Daughter",
                    ["Dead"] = "Dead",
                    ["Decapod"] = "Decapod",
                    ["Decay"] = "Decay",
                    ["Decoy"] = "Decoy",
                    ["Defeat"] = "Defeat",
                    ["Defuser"] = "Defuser",
                    ["Degree"] = "Degree",
                    ["Delay"] = "Delay",
                    ["Demigod"] = "Demigod",
                    ["Dentist"] = "Dentist",
                    ["Desert"] = "Desert",
                    ["Design"] = "Design",
                    ["Desire"] = "Desire",
                    ["Dessert"] = "Dessert",
                    ["Detail"] = "Detail",
                    ["Develop"] = "Develop",
                    ["Device"] = "Device",
                    ["Diamond"] = "Diamond",
                    ["Dictate"] = "Dictate",
                    ["Diffuse"] = "Diffuse",
                    ["Dilemma"] = "Dilemma",
                    ["Dingy"] = "Dingy",
                    ["Dinosaur"] = "Dinosaur",
                    ["Disease"] = "Disease",
                    ["Disgust"] = "Disgust",
                    ["Document"] = "Document",
                    ["Doubled"] = "Doubled",
                    ["Doubt"] = "Doubt",
                    ["Downbeat"] = "Downbeat",
                    ["Dragon"] = "Dragon",
                    ["Drawer"] = "Drawer",
                    ["Dream"] = "Dream",
                    ["Drink"] = "Drink",
                    ["Drunken"] = "Drunken",
                    ["Dungeon"] = "Dungeon",
                    ["Dynasty"] = "Dynasty",
                    ["Dyslexia"] = "Dyslexia",
                    ["Eclipse"] = "Eclipse",
                    ["Eldritch"] = "Eldritch",
                    ["Email"] = "Email",
                    ["Emulator"] = "Emulator",
                    ["Encrypt"] = "Encrypt",
                    ["England"] = "England",
                    ["Enlist"] = "Enlist",
                    ["Enough"] = "Enough",
                    ["Ensure"] = "Ensure",
                    ["Equality"] = "Equality",
                    ["Equation"] = "Equation",
                    ["Eruption"] = "Eruption",
                    ["Eternity"] = "Eternity",
                    ["Euphoria"] = "Euphoria",
                    ["Exact"] = "Exact",
                    ["Exclaim"] = "Exclaim",
                    ["Exhaust"] = "Exhaust",
                    ["Expert"] = "Expert",
                    ["Expertly"] = "Expertly",
                    ["Explain"] = "Explain",
                    ["Explodes"] = "Explodes",
                    ["Fabric"] = "Fabric",
                    ["Factory"] = "Factory",
                    ["Faded"] = "Faded",
                    ["Faint"] = "Faint",
                    ["Fair"] = "Fair",
                    ["False"] = "False",
                    ["Falter"] = "Falter",
                    ["Famous"] = "Famous",
                    ["Fantasy"] = "Fantasy",
                    ["Farm"] = "Farm",
                    ["Father"] = "Father",
                    ["Faucet"] = "Faucet",
                    ["Faulty"] = "Faulty",
                    ["Fearsome"] = "Fearsome",
                    ["Feast"] = "Feast",
                    ["February"] = "February",
                    ["Feint"] = "Feint",
                    ["Festival"] = "Festival",
                    ["Fiction"] = "Fiction",
                    ["Fighter"] = "Fighter",
                    ["Figure"] = "Figure",
                    ["Finish"] = "Finish",
                    ["Fireman"] = "Fireman",
                    ["Firework"] = "Firework",
                    ["First"] = "1",
                    ["Fixture"] = "Fixture",
                    ["Flagrant"] = "Flagrant",
                    ["Flagship"] = "Flagship",
                    ["Flamingo"] = "Flamingo",
                    ["Flesh"] = "Flesh",
                    ["Flipper"] = "Flipper",
                    ["Fluorine"] = "Fluorine",
                    ["Flush"] = "Flush",
                    ["Foreign"] = "Foreign",
                    ["Forensic"] = "Forensic",
                    ["Fractal"] = "Fractal",
                    ["Fragrant"] = "Fragrant",
                    ["France"] = "France",
                    ["Frantic"] = "Frantic",
                    ["Freak"] = "Freak",
                    ["Friction"] = "Friction",
                    ["Friday"] = "Friday",
                    ["Friendly"] = "Friendly",
                    ["Frighten"] = "Frighten",
                    ["Furor"] = "Furor",
                    ["Fused"] = "Fused",
                    ["Garage"] = "Garage",
                    ["Genes"] = "Genes",
                    ["Genetic"] = "Genetic",
                    ["Genius"] = "Genius",
                    ["Gentle"] = "Gentle",
                    ["Glacier"] = "Glacier",
                    ["Glitch"] = "Glitch",
                    ["Goat"] = "Goat",
                    ["Golden"] = "Golden",
                    ["Granular"] = "Granular",
                    ["Graphics"] = "Graphics",
                    ["Graphite"] = "Graphite",
                    ["Grateful"] = "Grateful",
                    ["Gridlock"] = "Gridlock",
                    ["Ground"] = "Ground",
                    ["Guitar"] = "Guitar",
                    ["Gumption"] = "Gumption",
                    ["Halogen"] = "Halogen",
                    ["Harmony"] = "Harmony",
                    ["Hawk"] = "Hawk",
                    ["Headache"] = "Headache",
                    ["Heard"] = "Heard",
                    ["Hedgehog"] = "Hedgehog",
                    ["Heinous"] = "Heinous",
                    ["Herd"] = "Herd",
                    ["Heretic"] = "Heretic",
                    ["Hexagon"] = "Hexagon",
                    ["Hiccup"] = "Hiccup",
                    ["Highway"] = "Highway",
                    ["Holiday"] = "Holiday",
                    ["Home"] = "Home",
                    ["Homesick"] = "Homesick",
                    ["Honest"] = "Honest",
                    ["Horror"] = "Horror",
                    ["Horse"] = "Horse",
                    ["House"] = "House",
                    ["Huge"] = "Huge",
                    ["Humanity"] = "Humanity",
                    ["Hungry"] = "Hungry",
                    ["Hydrogen"] = "Hydrogen",
                    ["Hysteria"] = "Hysteria",
                    ["Imagine"] = "Imagine",
                    ["Industry"] = "Industry",
                    ["Infamous"] = "Infamous",
                    ["Inside"] = "Inside",
                    ["Integral"] = "Integral",
                    ["Interest"] = "Interest",
                    ["Ironclad"] = "Ironclad",
                    ["Issue"] = "Issue",
                    ["Italic"] = "Italic",
                    ["Italy"] = "Italy",
                    ["Itch"] = "Itch",
                    ["Jaundice"] = "Jaundice",
                    ["Jeans"] = "Jeans",
                    ["Jeopardy"] = "Jeopardy",
                    ["Joyful"] = "Joyful",
                    ["Joystick"] = "Joystick",
                    ["Juice"] = "Juice",
                    ["Juncture"] = "Juncture",
                    ["Jungle"] = "Jungle",
                    ["Junkyard"] = "Junkyard",
                    ["Justice"] = "Justice",
                    ["Keep"] = "Keep",
                    ["Keyboard"] = "Keyboard",
                    ["Kilobyte"] = "Kilobyte",
                    ["Kilogram"] = "Kilogram",
                    ["Kingdom"] = "Kingdom",
                    ["Kitchen"] = "Kitchen",
                    ["Kitten"] = "Kitten",
                    ["Knife"] = "Knife",
                    ["Krypton"] = "Krypton",
                    ["Ladylike"] = "Ladylike",
                    ["Language"] = "Language",
                    ["Large"] = "Large",
                    ["Laughter"] = "Laughter",
                    ["Launch"] = "Launch",
                    ["Leaders"] = "Leaders",
                    ["Learn"] = "Learn",
                    ["Leave"] = "Leave",
                    ["Leopard"] = "Leopard",
                    ["Level"] = "Level",
                    ["Liberal"] = "Liberal",
                    ["Liberty"] = "Liberty",
                    ["Lifeboat"] = "Lifeboat",
                    ["Ligament"] = "Ligament",
                    ["Light"] = "Light",
                    ["Liquid"] = "Liquid",
                    ["Listen"] = "Listen",
                    ["Little"] = "Little",
                    ["Lobster"] = "Lobster",
                    ["Logical"] = "Logical",
                    ["Love"] = "Love",
                    ["Lucky"] = "Lucky",
                    ["Lulled"] = "Lulled",
                    ["Lunatic"] = "Lunatic",
                    ["Lurks"] = "Lurks",
                    ["Machine"] = "Machine",
                    ["Madam"] = "Madam",
                    ["Magnetic"] = "Magnetic",
                    ["Manager"] = "Manager",
                    ["Manual"] = "Manual",
                    ["Marina"] = "Marina",
                    ["Marine"] = "Marine",
                    ["Martian"] = "Martian",
                    ["Master"] = "Master",
                    ["Matrix"] = "Matrix",
                    ["Measure"] = "Measure",
                    ["Meaty"] = "Meaty",
                    ["Meddle"] = "Meddle",
                    ["Medical"] = "Medical",
                    ["Mental"] = "Mental",
                    ["Menu"] = "Menu",
                    ["Meow"] = "Meow",
                    ["Merchant"] = "Merchant",
                    ["Message"] = "Message",
                    ["Messes"] = "Messes",
                    ["Metal"] = "Metal",
                    ["Method"] = "Method",
                    ["Mettle"] = "Mettle",
                    ["Militant"] = "Militant",
                    ["Minim"] = "Minim",
                    ["Minimum"] = "Minimum",
                    ["Miracle"] = "Miracle",
                    ["Mirror"] = "Mirror",
                    ["Misjudge"] = "Misjudge",
                    ["Misplace"] = "Misplace",
                    ["Misses"] = "Misses",
                    ["Mistake"] = "Mistake",
                    ["Mixture"] = "Mixture",
                    ["Mnemonic"] = "Mnemonic",
                    ["Mobile"] = "Mobile",
                    ["Modern"] = "Modern",
                    ["Modest"] = "Modest",
                    ["Module"] = "Module",
                    ["Moist"] = "Moist",
                    ["Money"] = "Money",
                    ["Morning"] = "Morning",
                    ["Most"] = "Most",
                    ["Mother"] = "Mother",
                    ["Movies"] = "Movies",
                    ["Multiple"] = "Multiple",
                    ["Munch"] = "Munch",
                    ["Musical"] = "Musical",
                    ["Mustache"] = "Mustache",
                    ["Mystery"] = "Mystery",
                    ["Mystic"] = "Mystic",
                    ["Mystique"] = "Mystique",
                    ["Mythic"] = "Mythic",
                    ["Narcotic"] = "Narcotic",
                    ["Nasty"] = "Nasty",
                    ["Nature"] = "Nature",
                    ["Navigate"] = "Navigate",
                    ["Network"] = "Network",
                    ["Neutral"] = "Neutral",
                    ["Nobelium"] = "Nobelium",
                    ["Nobody"] = "Nobody",
                    ["Noise"] = "Noise",
                    ["Notice"] = "Notice",
                    ["Noun"] = "Noun",
                    ["Nuclear"] = "Nuclear",
                    ["Numeral"] = "Numeral",
                    ["Nutrient"] = "Nutrient",
                    ["Nymph"] = "Nymph",
                    ["Obelisk"] = "Obelisk",
                    ["Obstacle"] = "Obstacle",
                    ["Obvious"] = "Obvious",
                    ["Octopus"] = "Octopus",
                    ["Offset"] = "Offset",
                    ["Omega"] = "Omega",
                    ["Opaque"] = "Opaque",
                    ["Opinion"] = "Opinion",
                    ["Orange"] = "オレンジ",
                    ["Organic"] = "Organic",
                    ["Ouch"] = "Ouch",
                    ["Outbreak"] = "Outbreak",
                    ["Outdo"] = "Outdo",
                    ["Overcast"] = "Overcast",
                    ["Overlaps"] = "Overlaps",
                    ["Package"] = "Package",
                    ["Padlock"] = "Padlock",
                    ["Pancake"] = "Pancake",
                    ["Panda"] = "Panda",
                    ["Panic"] = "Panic",
                    ["Paper"] = "Paper",
                    ["Papers"] = "Papers",
                    ["Parent"] = "Parent",
                    ["Park"] = "Park",
                    ["Particle"] = "Particle",
                    ["Passive"] = "Passive",
                    ["Patented"] = "Patented",
                    ["Pathetic"] = "Pathetic",
                    ["Patient"] = "Patient",
                    ["Peace"] = "Peace",
                    ["Peasant"] = "Peasant",
                    ["Penalty"] = "Penalty",
                    ["Pencil"] = "Pencil",
                    ["Penguin"] = "Penguin",
                    ["Perfect"] = "Perfect",
                    ["Person"] = "Person",
                    ["Persuade"] = "Persuade",
                    ["Perusing"] = "Perusing",
                    ["Phone"] = "Phone",
                    ["Physical"] = "Physical",
                    ["Piano"] = "Piano",
                    ["Picture"] = "Picture",
                    ["Piglet"] = "Piglet",
                    ["Pilfer"] = "Pilfer",
                    ["Pillage"] = "Pillage",
                    ["Pinch"] = "Pinch",
                    ["Pirate"] = "Pirate",
                    ["Pitcher"] = "Pitcher",
                    ["Pizza"] = "Pizza",
                    ["Plane"] = "Plane",
                    ["Planet"] = "Planet",
                    ["Platonic"] = "Platonic",
                    ["Player"] = "Player",
                    ["Please"] = "Please",
                    ["Plucky"] = "Plucky",
                    ["Plunder"] = "Plunder",
                    ["Plurals"] = "Plurals",
                    ["Pocket"] = "Pocket",
                    ["Police"] = "Police",
                    ["Portrait"] = "Portrait",
                    ["Potato"] = "Potato",
                    ["Potently"] = "Potently",
                    ["Pounce"] = "Pounce",
                    ["Poverty"] = "Poverty",
                    ["Practice"] = "Practice",
                    ["Predict"] = "Predict",
                    ["Prefect"] = "Prefect",
                    ["Premium"] = "Premium",
                    ["Present"] = "Present",
                    ["Prince"] = "Prince",
                    ["Printer"] = "Printer",
                    ["Prison"] = "Prison",
                    ["Profit"] = "Profit",
                    ["Promise"] = "Promise",
                    ["Prophet"] = "Prophet",
                    ["Protein"] = "Protein",
                    ["Province"] = "Province",
                    ["Psalm"] = "Psalm",
                    ["Psychic"] = "Psychic",
                    ["Puddle"] = "Puddle",
                    ["Punchbag"] = "Punchbag",
                    ["Pungent"] = "Pungent",
                    ["Punish"] = "Punish",
                    ["Purchase"] = "Purchase",
                    ["Quagmire"] = "Quagmire",
                    ["Qualify"] = "Qualify",
                    ["Quantify"] = "Quantify",
                    ["Quantize"] = "Quantize",
                    ["Quarter"] = "Quarter",
                    ["Querying"] = "Querying",
                    ["Queue"] = "Queue",
                    ["Quiche"] = "Quiche",
                    ["Quick"] = "Quick",
                    ["Rabbit"] = "Rabbit",
                    ["Racoon"] = "Racoon",
                    ["Radar"] = "Radar",
                    ["Radical"] = "Radical",
                    ["Rainbow"] = "Rainbow",
                    ["Random"] = "Random",
                    ["Rattle"] = "Rattle",
                    ["Ravenous"] = "Ravenous",
                    ["Reason"] = "Reason",
                    ["Rebuke"] = "Rebuke",
                    ["Refine"] = "Refine",
                    ["Regular"] = "Regular",
                    ["Reindeer"] = "Reindeer",
                    ["Request"] = "Request",
                    ["Resort"] = "Resort",
                    ["Respect"] = "Respect",
                    ["Retire"] = "Retire",
                    ["Revolt"] = "Revolt",
                    ["Reward"] = "Reward",
                    ["Rhapsody"] = "Rhapsody",
                    ["Rhenium"] = "Rhenium",
                    ["Rhodium"] = "Rhodium",
                    ["Rhomboid"] = "Rhomboid",
                    ["Rhyme"] = "Rhyme",
                    ["Rhythm"] = "Rhythm",
                    ["Ridicule"] = "Ridicule",
                    ["Roadwork"] = "Roadwork",
                    ["Roar"] = "Roar",
                    ["Roast"] = "Roast",
                    ["Room"] = "Room",
                    ["Rooster"] = "Rooster",
                    ["Roster"] = "Roster",
                    ["Rotor"] = "Rotor",
                    ["Rotunda"] = "Rotunda",
                    ["Royal"] = "Royal",
                    ["Ruler"] = "Ruler",
                    ["Rural"] = "Rural",
                    ["Sailor"] = "Sailor",
                    ["Sainted"] = "Sainted",
                    ["Sales"] = "Sales",
                    ["Sally"] = "Sally",
                    ["Satisfy"] = "Satisfy",
                    ["Saunter"] = "Saunter",
                    ["Scale"] = "Scale",
                    ["Scandal"] = "Scandal",
                    ["Schedule"] = "Schedule",
                    ["School"] = "School",
                    ["Science"] = "Science",
                    ["Scratch"] = "Scratch",
                    ["Screen"] = "Screen",
                    ["Sensible"] = "Sensible",
                    ["Separate"] = "Separate",
                    ["Serious"] = "Serious",
                    ["Several"] = "Several",
                    ["Shampoo"] = "Shampoo",
                    ["Shares"] = "Shares",
                    ["Shelter"] = "Shelter",
                    ["Shift"] = "Shift",
                    ["Ship"] = "Ship",
                    ["Shirt"] = "Shirt",
                    ["Shiver"] = "Shiver",
                    ["Shorten"] = "Shorten",
                    ["Showcase"] = "Showcase",
                    ["Shuffle"] = "Shuffle",
                    ["Silent"] = "Silent",
                    ["Similar"] = "Similar",
                    ["Sister"] = "Sister",
                    ["Sixth"] = "Sixth",
                    ["Sixty"] = "Sixty",
                    ["Skater"] = "Skater",
                    ["Skyward"] = "Skyward",
                    ["Slander"] = "Slander",
                    ["Slayer"] = "Slayer",
                    ["Sleek"] = "Sleek",
                    ["Slipper"] = "Slipper",
                    ["Smart"] = "Smart",
                    ["Smeared"] = "Smeared",
                    ["Soccer"] = "Soccer",
                    ["Society"] = "Society",
                    ["Source"] = "Source",
                    ["Spain"] = "Spain",
                    ["Spare"] = "Spare",
                    ["Spark"] = "Spark",
                    ["Spatula"] = "Spatula",
                    ["Speaker"] = "Speaker",
                    ["Special"] = "Special",
                    ["Spectate"] = "Spectate",
                    ["Spectrum"] = "Spectrum",
                    ["Spicy"] = "Spicy",
                    ["Spinach"] = "Spinach",
                    ["Spiral"] = "Spiral",
                    ["Splendid"] = "Splendid",
                    ["Splinter"] = "Splinter",
                    ["Sprayed"] = "Sprayed",
                    ["Spread"] = "Spread",
                    ["Spring"] = "Spring",
                    ["Squadron"] = "Squadron",
                    ["Squander"] = "Squander",
                    ["Squash"] = "Squash",
                    ["Squib"] = "Squib",
                    ["Squid"] = "Squid",
                    ["Squish"] = "Squish",
                    ["Stake"] = "Stake",
                    ["Stalking"] = "Stalking",
                    ["Steak"] = "Steak",
                    ["Steam"] = "Steam",
                    ["Sticker"] = "Sticker",
                    ["Stinky"] = "Stinky",
                    ["Stocking"] = "Stocking",
                    ["Stone"] = "Stone",
                    ["Store"] = "Store",
                    ["Stormy"] = "Stormy",
                    ["Strange"] = "Strange",
                    ["Strike"] = "Strike",
                    ["Stutter"] = "Stutter",
                    ["Subway"] = "Subway",
                    ["Suffer"] = "Suffer",
                    ["Supreme"] = "Supreme",
                    ["Surf"] = "Surf",
                    ["Surplus"] = "Surplus",
                    ["Survey"] = "Survey",
                    ["Switch"] = "Switch",
                    ["Symbol"] = "Symbol",
                    ["System"] = "System",
                    ["Systemic"] = "Systemic",
                    ["Table"] = "Table",
                    ["Tadpole"] = "Tadpole",
                    ["Talking"] = "Talking",
                    ["Tangle"] = "Tangle",
                    ["Tank"] = "Tank",
                    ["Tapeworm"] = "Tapeworm",
                    ["Target"] = "Target",
                    ["Tarot"] = "Tarot",
                    ["Teach"] = "Teach",
                    ["Teamwork"] = "Teamwork",
                    ["Terminal"] = "Terminal",
                    ["Terminus"] = "Terminus",
                    ["Terror"] = "Terror",
                    ["Testify"] = "Testify",
                    ["Their"] = "Their",
                    ["There"] = "There",
                    ["Thick"] = "Thick",
                    ["Thief"] = "Thief",
                    ["Think"] = "Think",
                    ["Throat"] = "Throat",
                    ["Through"] = "Through",
                    ["Thunder"] = "Thunder",
                    ["Thyme"] = "Thyme",
                    ["Ticket"] = "Ticket",
                    ["Time"] = "Time",
                    ["Toaster"] = "Toaster",
                    ["Tomato"] = "Tomato",
                    ["Tone"] = "Tone",
                    ["Torque"] = "Torque",
                    ["Tortoise"] = "Tortoise",
                    ["Touchy"] = "Touchy",
                    ["Toupe"] = "Toupe",
                    ["Tower"] = "Tower",
                    ["Transfix"] = "Transfix",
                    ["Transit"] = "Transit",
                    ["Trash"] = "Trash",
                    ["Trauma"] = "Trauma",
                    ["Treason"] = "Treason",
                    ["Treasure"] = "Treasure",
                    ["Trick"] = "Trick",
                    ["Tripod"] = "Tripod",
                    ["Trouble"] = "Trouble",
                    ["Truck"] = "Truck",
                    ["Trumpet"] = "Trumpet",
                    ["Turtle"] = "Turtle",
                    ["Twinkle"] = "Twinkle",
                    ["Ugly"] = "Ugly",
                    ["Ultra"] = "Ultra",
                    ["Umbrella"] = "Umbrella",
                    ["Underway"] = "Underway",
                    ["Unique"] = "Unique",
                    ["Unknown"] = "Unknown",
                    ["Unsteady"] = "Unsteady",
                    ["Untoward"] = "Untoward",
                    ["Unwashed"] = "Unwashed",
                    ["Upgrade"] = "Upgrade",
                    ["Urban"] = "Urban",
                    ["Used"] = "Used",
                    ["Useless"] = "Useless",
                    ["Utopia"] = "Utopia",
                    ["Vacuum"] = "Vacuum",
                    ["Vampire"] = "Vampire",
                    ["Vanish"] = "Vanish",
                    ["Vanquish"] = "Vanquish",
                    ["Various"] = "Various",
                    ["Vast"] = "Vast",
                    ["Velocity"] = "Velocity",
                    ["Vendor"] = "Vendor",
                    ["Verb"] = "Verb",
                    ["Verbatim"] = "Verbatim",
                    ["Verdict"] = "Verdict",
                    ["Vexation"] = "Vexation",
                    ["Vicious"] = "Vicious",
                    ["Victim"] = "Victim",
                    ["Victory"] = "Victory",
                    ["Video"] = "Video",
                    ["View"] = "View",
                    ["Viking"] = "Viking",
                    ["Village"] = "Village",
                    ["Violent"] = "Violent",
                    ["Violin"] = "Violin",
                    ["Virulent"] = "Virulent",
                    ["Visceral"] = "Visceral",
                    ["Vision"] = "Vision",
                    ["Volatile"] = "Volatile",
                    ["Voltage"] = "Voltage",
                    ["Vortex"] = "Vortex",
                    ["Vulgar"] = "Vulgar",
                    ["Warden"] = "Warden",
                    ["Warlock"] = "Warlock",
                    ["Warning"] = "Warning",
                    ["Wealth"] = "Wealth",
                    ["Weapon"] = "Weapon",
                    ["Wedding"] = "Wedding",
                    ["Weight"] = "Weight",
                    ["Whack"] = "Whack",
                    ["Wharf"] = "Wharf",
                    ["What"] = "What",
                    ["When"] = "When",
                    ["Whisk"] = "Whisk",
                    ["Whistle"] = "Whistle",
                    ["Wicked"] = "Wicked",
                    ["Window"] = "Window",
                    ["Winter"] = "Winter",
                    ["Witness"] = "Witness",
                    ["Wizard"] = "Wizard",
                    ["Wrench"] = "Wrench",
                    ["Wretch"] = "Wretch",
                    ["Wrinkle"] = "Wrinkle",
                    ["Writer"] = "Writer",
                    ["Xanthous"] = "Xanthous",
                    ["Yacht"] = "Yacht",
                    ["Yarn"] = "Yarn",
                    ["Yawn"] = "Yawn",
                    ["Yeah"] = "Yeah",
                    ["Yearlong"] = "Yearlong",
                    ["Yearn"] = "Yearn",
                    ["Yeoman"] = "Yeoman",
                    ["Yodel"] = "Yodel",
                    ["Yoga"] = "Yoga",
                    ["Yonder"] = "Yonder",
                    ["Youngest"] = "Youngest",
                    ["Yourself"] = "Yourself",
                    ["Zealot"] = "Zealot",
                    ["Zebra"] = "Zebra",
                    ["Zenith"] = "Zenith",
                    ["Zither"] = "Zither",
                    ["Zodiac"] = "Zodiac",
                    ["Zombie"] = "Zombie",
                },
            },

            // Module Listening
            // Which module did the sound played by the {1} button belong to in {0}?
            // Which module did the sound played by the red button belong to in Module Listening?
            [Question.ModuleListeningSounds] = new TranslationInfo
            {
                QuestionText = "Which module did the sound played by the {1} button belong to in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                },
            },

            // Module Maze
            // Which of the following was the starting icon for {0}?
            // Which of the following was the starting icon for Module Maze?
            [Question.ModuleMazeStartingIcon] = new TranslationInfo
            {
                QuestionText = "Which of the following was the starting icon for {0}？",
            },

            // Monsplode, Fight!
            // Which creature was displayed in {0}?
            // Which creature was displayed in Monsplode, Fight!?
            [Question.MonsplodeFightCreature] = new TranslationInfo
            {
                QuestionText = "Which creature was displayed in {0}？",
            },
            // Which one of these moves {1} selectable in {0}?
            // Which one of these moves was selectable in Monsplode, Fight!?
            [Question.MonsplodeFightMove] = new TranslationInfo
            {
                QuestionText = "Which one of these moves {1} selectable in {0}？",
            },

            // Monsplode Trading Cards
            // What was the {1} before the last action in {0}?
            // What was the first card in your hand before the last action in Monsplode Trading Cards?
            [Question.MonsplodeTradingCardsCards] = new TranslationInfo
            {
                QuestionText = "What was the {1} before the last action in {0}？",
            },
            // What was the print version of the {1} before the last action in {0}?
            // What was the print version of the first card in your hand before the last action in Monsplode Trading Cards?
            [Question.MonsplodeTradingCardsPrintVersions] = new TranslationInfo
            {
                QuestionText = "What was the print version of the {1} before the last action in {0}？",
            },

            // The Moon
            // What was the {1} set in clockwise order in {0}?
            // What was the first initially lit set in clockwise order in The Moon?
            [Question.MoonLitUnlit] = new TranslationInfo
            {
                QuestionText = "What was the {1} set in clockwise order in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["south"] = "南",
                    ["south-west"] = "南西",
                    ["west"] = "西",
                    ["north-west"] = "北西",
                    ["north"] = "北",
                    ["north-east"] = "北東",
                    ["east"] = "東",
                    ["south-east"] = "南東",
                },
            },

            // More Code
            // What was the flashing word in {0}?
            // What was the flashing word in More Code?
            [Question.MoreCodeWord] = new TranslationInfo
            {
                QuestionText = "What was the flashing word in {0}？",
            },

            // Morse-A-Maze
            // What was the starting location in {0}?
            // What was the starting location in Morse-A-Maze?
            [Question.MorseAMazeStartingCoordinate] = new TranslationInfo
            {
                QuestionText = "What was the starting location in {0}？",
            },
            // What was the ending location in {0}?
            // What was the ending location in Morse-A-Maze?
            [Question.MorseAMazeEndingCoordinate] = new TranslationInfo
            {
                QuestionText = "What was the ending location in {0}？",
            },
            // What was the word shown as Morse code in {0}?
            // What was the word shown as Morse code in Morse-A-Maze?
            [Question.MorseAMazeMorseCodeWord] = new TranslationInfo
            {
                QuestionText = "What was the word shown as Morse code in {0}？",
            },

            // Morse Buttons
            // What was the character flashed by the {1} button in {0}?
            // What was the character flashed by the first button in Morse Buttons?
            [Question.MorseButtonsButtonLabel] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のボタンで点滅した文字は？",
            },
            // What was the color flashed by the {1} button in {0}?
            // What was the color flashed by the first button in Morse Buttons?
            [Question.MorseButtonsButtonColor] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のボタンで点滅した色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["blue"] = "青",
                    ["green"] = "緑",
                    ["yellow"] = "黄",
                    ["orange"] = "オレンジ",
                    ["purple"] = "紫",
                },
            },

            // Morsematics
            // What was the {1} received letter in {0}?
            // What was the first received letter in Morsematics?
            [Question.MorsematicsReceivedLetters] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に受信した文字は？",
            },

            // Morse War
            // What were the LEDs in the {1} row in {0} (1 = on, 0 = off)?
            // What were the LEDs in the bottom row in Morse War (1 = on, 0 = off)?
            [Question.MorseWarLeds] = new TranslationInfo
            {
                QuestionText = "{0}で{1}段のLEDの状態は(1=オン、0=オフ)？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["bottom"] = "下",
                    ["middle"] = "中央",
                    ["top"] = "上",
                },
            },
            // What code was transmitted in {0}?
            // What code was transmitted in Morse War?
            [Question.MorseWarCode] = new TranslationInfo
            {
                QuestionText = "0}で変換した符号は？",
            },

            // Mouse in the Maze
            // What color was the torus in {0}?
            // What color was the torus in Mouse in the Maze?
            [Question.MouseInTheMazeTorus] = new TranslationInfo
            {
                QuestionText = "What color was the torus in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["white"] = "白",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                },
            },
            // Which color sphere was the goal in {0}?
            // Which color sphere was the goal in Mouse in the Maze?
            [Question.MouseInTheMazeSphere] = new TranslationInfo
            {
                QuestionText = "Which color sphere was the goal in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["white"] = "白",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                },
            },

            // M-Seq
            // What was the {1} obtained digit in {0}?
            // What was the first obtained digit in M-Seq?
            [Question.MSeqObtained] = new TranslationInfo
            {
                QuestionText = "What was the {1} obtained digit in {0}？",
            },
            // What was the final number from the iteration process in {0}?
            // What was the final number from the iteration process in M-Seq?
            [Question.MSeqSubmitted] = new TranslationInfo
            {
                QuestionText = "What was the final number from the iteration process in {0}？",
            },

            // Multicolored Switches
            // What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?
            // What color was the first LED on the top row when the tiny LED was lit in Multicolored Switches?
            [Question.MulticoloredSwitchesLedColor] = new TranslationInfo
            {
                QuestionText = "{0}で小さなLEDが{3}時の{2}段LEDの{1}番目は？",
                Answers = new Dictionary<string, string>
                {
                    ["black"] = "黒",
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["yellow"] = "黄",
                    ["blue"] = "青",
                    ["magenta"] = "マゼンタ",
                    ["cyan"] = "シアン",
                    ["white"] = "白",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "上",
                    ["lit"] = "点灯した",
                    ["unlit"] = "点灯していない",
                },
            },

            // Murder
            // Where was the body found in {0}?
            // Where was the body found in Murder?
            [Question.MurderBodyFound] = new TranslationInfo
            {
                QuestionText = "{0}の死体はどこで見つかった？",
            },
            // Which of these was {1} in {0}?
            // Which of these was a suspect, but not the murderer, in Murder?
            [Question.MurderSuspect] = new TranslationInfo
            {
                QuestionText = "Which of these was {1} in {0}？",
            },
            // Which of these was {1} in {0}?
            // Which of these was a potential weapon, but not the murder weapon, in Murder?
            [Question.MurderWeapon] = new TranslationInfo
            {
                QuestionText = "Which of these was {1} in {0}？",
            },

            // Mystery Module
            // Which module was the first requested to be solved by {0}?
            // Which module was the first requested to be solved by Mystery Module?
            [Question.MysteryModuleFirstKey] = new TranslationInfo
            {
                QuestionText = " {0}で最初に解除するように指示されたモジュールは？",
            },
            // Which module was hidden by {0}?
            // Which module was hidden by Mystery Module?
            [Question.MysteryModuleHiddenModule] = new TranslationInfo
            {
                QuestionText = "{0}で隠されていたモジュールは？",
            },

            // Mystic Square
            // Where was the skull in {0}?
            // Where was the skull in Mystic Square?
            [Question.MysticSquareSkull] = new TranslationInfo
            {
                QuestionText = "{0}のどくろの位置は？",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "左上",
                    ["top middle"] = "上中央",
                    ["top right"] = "右上",
                    ["middle left"] = "左中央",
                    ["center"] = "中央",
                    ["middle right"] = "右中央",
                    ["bottom left"] = "左下",
                    ["bottom middle"] = "下中央",
                    ["bottom right"] = "右下",
                },
            },

            // N&Ms
            // What was the label of the correct button in {0}?
            // What was the label of the correct button in N&Ms?
            [Question.NandMsAnswer] = new TranslationInfo
            {
                QuestionText = "What was the label of the correct button in {0}？",
            },

            // Name Codes
            // What was the {1} index in {0}?
            // What was the left index in Name Codes?
            [Question.NameCodesIndices] = new TranslationInfo
            {
                QuestionText = "What was the {1} index in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "左",
                    ["right"] = "右",
                },
            },

            // Navinums
            // What was the initial middle digit in {0}?
            // What was the initial middle digit in Navinums?
            [Question.NavinumsMiddleDigit] = new TranslationInfo
            {
                QuestionText = "What was the initial middle digit in {0}？",
            },
            // What was the {1} directional button pressed in {0}?
            // What was the first directional button pressed in Navinums?
            [Question.NavinumsDirectionalButtons] = new TranslationInfo
            {
                QuestionText = "What was the {1} directional button pressed in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["up"] = "下",
                    ["left"] = "左",
                    ["right"] = "右",
                    ["down"] = "下",
                },
            },

            // The Navy Button
            // Which Greek letter appeared on {0} (case-sensitive)?
            // Which Greek letter appeared on The Navy Button (case-sensitive)?
            [Question.NavyButtonGreekLetters] = new TranslationInfo
            {
                QuestionText = "Which Greek letter appeared on {0} (case-sensitive)？",
            },
            // What was the {1} of the given in {0} (0-indexed)?
            // What was the column of the given in The Navy Button (0-indexed)?
            [Question.NavyButtonGiven] = new TranslationInfo
            {
                QuestionText = "What was the {1} of the given in {0} (0-indexed)？",
            },

            // The Necronomicon
            // What was the chapter number of the {1} page in {0}?
            // What was the chapter number of the first page in The Necronomicon?
            [Question.NecronomiconChapters] = new TranslationInfo
            {
                QuestionText = "What was the chapter number of the {1} page in {0}？",
            },

            // Negativity
            // In base 10, what was the value submitted in {0}?
            // In base 10, what was the value submitted in Negativity?
            [Question.NegativitySubmittedValue] = new TranslationInfo
            {
                QuestionText = "In base 10, what was the value submitted in {0}？",
            },
            // Excluding 0s, what was the submitted balanced ternary in {0}?
            // Excluding 0s, what was the submitted balanced ternary in Negativity?
            [Question.NegativitySubmittedTernary] = new TranslationInfo
            {
                QuestionText = "Excluding 0s, what was the submitted balanced ternary in {0}？",
            },

            // Neutralization
            // What was the acid’s color in {0}?
            // What was the acid’s color in Neutralization?
            [Question.NeutralizationColor] = new TranslationInfo
            {
                QuestionText = "What was the acid’s color in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                },
            },
            // What was the acid’s volume in {0}?
            // What was the acid’s volume in Neutralization?
            [Question.NeutralizationVolume] = new TranslationInfo
            {
                QuestionText = "What was the acid’s volume in {0}？",
            },

            // ❖
            // Which button flashed for the {1} stage in {0}?
            [Question.NonverbalSimonFlashes] = new TranslationInfo
            {
                QuestionText = "Which button flashed for stage {1} in {0}?",
            },

            // Not Colored Switches
            // What was the encrypted word in {0}?
            // What was the encrypted word in Not Colored Switches?
            [Question.NotColoredSwitchesWord] = new TranslationInfo
            {
                QuestionText = "What was the encrypted word in {0}?",
            },

            // Not Connection Check
            // What symbol flashed on the {1} button in {0}?
            // What symbol flashed on the top left button in Not Connection Check?
            [Question.NotConnectionCheckFlashes] = new TranslationInfo
            {
                QuestionText = "What symbol flashed on the {1} button in {0}？",
            },
            // What was the value of the {1} button in {0}?
            // What was the value of the top left button in Not Connection Check?
            [Question.NotConnectionCheckValues] = new TranslationInfo
            {
                QuestionText = "What was the value of the {1} button in {0}？",
            },

            // Not Coordinates
            // Which coordinate was part of the square in {0}?
            // Which coordinate was part of the square in Not Coordinates?
            [Question.NotCoordinatesSquareCoords] = new TranslationInfo
            {
                QuestionText = "Which coordinate was part of the square in {0}？",
            },

            // Not Keypad
            // What color flashed {1} in the final sequence in {0}?
            // What color flashed first in the final sequence in Not Keypad?
            [Question.NotKeypadColor] = new TranslationInfo
            {
                QuestionText = "What color flashed {1} in the final sequence in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["orange"] = "オレンジ",
                    ["yellow"] = "黄",
                    ["green"] = "緑",
                    ["cyan"] = "シアン",
                    ["blue"] = "青",
                    ["purple"] = "紫",
                    ["magenta"] = "マゼンタ",
                    ["pink"] = "ピンク",
                    ["brown"] = "茶",
                    ["grey"] = "灰",
                    ["white"] = "白",
                },
            },
            // Which symbol was on the button that flashed {1} in the final sequence in {0}?
            // Which symbol was on the button that flashed first in the final sequence in Not Keypad?
            [Question.NotKeypadSymbol] = new TranslationInfo
            {
                QuestionText = "Which symbol was on the button that flashed {1} in the final sequence in {0}？",
            },

            // Not Maze
            // What was the starting distance in {0}?
            // What was the starting distance in Not Maze?
            [Question.NotMazeStartingDistance] = new TranslationInfo
            {
                QuestionText = "What was the starting distance in {0}？",
            },

            // Not Morse Code
            // What was the {1} correct word you submitted in {0}?
            // What was the first correct word you submitted in Not Morse Code?
            [Question.NotMorseCodeWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} correct word you submitted in {0}？",
            },

            // Not Morsematics
            // What was the transmitted word on {0}?
            // What was the transmitted word on Not Morsematics?
            [Question.NotMorsematicsWord] = new TranslationInfo
            {
                QuestionText = "What was the transmitted word on {0}？",
            },

            // Not Murder
            // What room was {1} in initially on {0}?
            // What room was Miss Scarlett in initially on Not Murder?
            [Question.NotMurderRoom] = new TranslationInfo
            {
                QuestionText = "What room was {1} in during {2} on {0}？",
            },
            // What weapon did {1} possess initially on {0}?
            // What weapon did Miss Scarlett possess initially on Not Murder?
            [Question.NotMurderWeapon] = new TranslationInfo
            {
                QuestionText = "What weapon did {1} possess during {2} on {0}？",
            },

            // Not Number Pad
            // Which of these numbers {1} at the {2} stage of {0}?
            // Which of these numbers flashed at the first stage of Not Number Pad?
            [Question.NotNumberPadFlashes] = new TranslationInfo
            {
                QuestionText = "Which of these numbers {1} at the {2} stage of {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["flashed"] = "点滅",
                    ["did not flash"] = "did not flash",
                },
            },

            // Not Perspective Pegs
            // What was the position of the {1} flashing peg on {0}?
            // What was the position of the first flashing peg on Not Perspective Pegs?
            [Question.NotPerspectivePegsPosition] = new TranslationInfo
            {
                QuestionText = "What was the position of the {1} flashing peg on {0}?",
            },
            // From what perspective did the {1} peg flash on {0}?
            // From what perspective did the first peg flash on Not Perspective Pegs?
            [Question.NotPerspectivePegsPerspective] = new TranslationInfo
            {
                QuestionText = "From what perspective did the {1} peg flash on {0}?",
            },
            // What was the color of the {1} flashing peg on {0}?
            // What was the color of the first flashing peg on Not Perspective Pegs?
            [Question.NotPerspectivePegsColor] = new TranslationInfo
            {
                QuestionText = "What was the color of the {1} flashing peg on {0}?",
            },

            // Not Piano Keys
            // What was the first displayed symbol on {0}?
            // What was the first displayed symbol on Not Piano Keys?
            [Question.NotPianoKeysFirstSymbol] = new TranslationInfo
            {
                QuestionText = "What was the first displayed symbol on {0}？",
            },
            // What was the second displayed symbol on {0}?
            // What was the second displayed symbol on Not Piano Keys?
            [Question.NotPianoKeysSecondSymbol] = new TranslationInfo
            {
                QuestionText = "What was the second displayed symbol on {0}？",
            },
            // What was the third displayed symbol on {0}?
            // What was the third displayed symbol on Not Piano Keys?
            [Question.NotPianoKeysThirdSymbol] = new TranslationInfo
            {
                QuestionText = "What was the third displayed symbol on {0}？",
            },

            // Not Simaze
            // Which maze was used in {0}?
            // Which maze was used in Not Simaze?
            [Question.NotSimazeMaze] = new TranslationInfo
            {
                QuestionText = "Which maze was used in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["orange"] = "オレンジ",
                    ["yellow"] = "黄",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["purple"] = "紫",
                },
            },
            // What was the starting position in {0}?
            // What was the starting position in Not Simaze?
            [Question.NotSimazeStart] = new TranslationInfo
            {
                QuestionText = "What was the starting position in {0}？",
            },
            // What was the goal position in {0}?
            // What was the goal position in Not Simaze?
            [Question.NotSimazeGoal] = new TranslationInfo
            {
                QuestionText = "What was the goal position in {0}？",
            },

            // Not Text Field
            // Which letter was pressed in the first stage of {0}?
            // Which letter was pressed in the first stage of Not Text Field?
            [Question.NotTextFieldInitialPresses] = new TranslationInfo
            {
                QuestionText = "Which letter was pressed in the first stage of {0}？",
            },
            // Which letter appeared 9 times at the start of {0}?
            // Which letter appeared 9 times at the start of Not Text Field?
            [Question.NotTextFieldBackgroundLetter] = new TranslationInfo
            {
                QuestionText = "Which letter appeared 9 times at the start of {0}？",
            },

            // Not The Bulb
            // What word flashed on {0}?
            // What word flashed on Not The Bulb?
            [Question.NotTheBulbWord] = new TranslationInfo
            {
                QuestionText = "What word flashed on {0}？",
            },
            // What color was the bulb on {0}?
            // What color was the bulb on Not The Bulb?
            [Question.NotTheBulbColor] = new TranslationInfo
            {
                QuestionText = "What color was the bulb on {0}？",
            },
            // What was the material of the screw cap on {0}?
            // What was the material of the screw cap on Not The Bulb?
            [Question.NotTheBulbScrewCap] = new TranslationInfo
            {
                QuestionText = "What was the material of the screw cap on {0}？",
            },

            // Not the Button
            // What colors did the light glow in {0}?
            // What colors did the light glow in Not the Button?
            [Question.NotTheButtonLightColor] = new TranslationInfo
            {
                QuestionText = "What colors did the light glow in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["white"] = "白",
                    ["red"] = "赤",
                    ["yellow"] = "黄",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["white/red"] = "白/赤",
                    ["white/yellow"] = "白/黄",
                    ["white/green"] = "白/緑",
                    ["white/blue"] = "白/青",
                    ["red/yellow"] = "赤/黄",
                    ["red/green"] = "赤/緑",
                    ["red/blue"] = "赤/青",
                    ["yellow/green"] = "黄/緑",
                    ["yellow/blue"] = "黄/青",
                    ["green/blue"] = "緑/青",
                },
            },

            // Not the Screw
            // What was the initial position in {0}?
            // What was the initial position in Not the Screw?
            [Question.NotTheScrewInitialPosition] = new TranslationInfo
            {
                QuestionText = "What was the initial position in {0}？",
            },

            // Not Who’s on First
            // In which position was the button you pressed in the {1} stage on {0}?
            // In which position was the button you pressed in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstPressedPosition] = new TranslationInfo
            {
                QuestionText = "In which position was the button you pressed in the {1} stage on {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "左上",
                    ["top right"] = "右上",
                    ["middle left"] = "左中央",
                    ["middle right"] = "右中央",
                    ["bottom left"] = "左下",
                    ["bottom right"] = "右下",
                },
            },
            // What was the label on the button you pressed in the {1} stage on {0}?
            // What was the label on the button you pressed in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstPressedLabel] = new TranslationInfo
            {
                QuestionText = "What was the label on the button you pressed in the {1} stage on {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["BLANK"] = "BLANK",
                    ["DONE"] = "DONE",
                    ["FIRST"] = "1",
                    ["HOLD"] = "HOLD",
                    ["LEFT"] = "LEFT",
                    ["LIKE"] = "LIKE",
                    ["MIDDLE"] = "MIDDLE",
                    ["NEXT"] = "NEXT",
                    ["NO"] = "NO",
                    ["NOTHING"] = "NOTHING",
                    ["OKAY"] = "OKAY",
                    ["PRESS"] = "PRESS",
                    ["READY"] = "READY",
                    ["RIGHT"] = "RIGHT",
                    ["SURE"] = "SURE",
                    ["U"] = "U",
                    ["UH HUH"] = "UH HUH",
                    ["UH UH"] = "UH UH",
                    ["UHHH"] = "UHHH",
                    ["UR"] = "UR",
                    ["WAIT"] = "WAIT",
                    ["WHAT"] = "WHAT",
                    ["WHAT?"] = "WHAT?",
                    ["YES"] = "YES",
                    ["YOU"] = "YOU",
                    ["YOU ARE"] = "YOU ARE",
                    ["YOU'RE"] = "YOU'RE",
                    ["YOUR"] = "YOUR",
                },
            },
            // In which position was the reference button in the {1} stage on {0}?
            // In which position was the reference button in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstReferencePosition] = new TranslationInfo
            {
                QuestionText = "In which position was the reference button in the {1} stage on {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "左上",
                    ["top right"] = "右上",
                    ["middle left"] = "左中央",
                    ["middle right"] = "右中央",
                    ["bottom left"] = "左下",
                    ["bottom right"] = "右下",
                },
            },
            // What was the label on the reference button in the {1} stage on {0}?
            // What was the label on the reference button in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstReferenceLabel] = new TranslationInfo
            {
                QuestionText = "What was the label on the reference button in the {1} stage on {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["BLANK"] = "BLANK",
                    ["DONE"] = "DONE",
                    ["FIRST"] = "1",
                    ["HOLD"] = "HOLD",
                    ["LEFT"] = "LEFT",
                    ["LIKE"] = "LIKE",
                    ["MIDDLE"] = "MIDDLE",
                    ["NEXT"] = "NEXT",
                    ["NO"] = "NO",
                    ["NOTHING"] = "NOTHING",
                    ["OKAY"] = "OKAY",
                    ["PRESS"] = "PRESS",
                    ["READY"] = "READY",
                    ["RIGHT"] = "RIGHT",
                    ["SURE"] = "SURE",
                    ["U"] = "U",
                    ["UH HUH"] = "UH HUH",
                    ["UH UH"] = "UH UH",
                    ["UHHH"] = "UHHH",
                    ["UR"] = "UR",
                    ["WAIT"] = "WAIT",
                    ["WHAT"] = "WHAT",
                    ["WHAT?"] = "WHAT?",
                    ["YES"] = "YES",
                    ["YOU"] = "YOU",
                    ["YOU ARE"] = "YOU ARE",
                    ["YOU'RE"] = "YOU'RE",
                    ["YOUR"] = "YOUR",
                },
            },
            // What was the calculated number in the second stage on {0}?
            // What was the calculated number in the second stage on Not Who’s on First?
            [Question.NotWhosOnFirstSum] = new TranslationInfo
            {
                QuestionText = "What was the calculated number in the second stage on {0}？",
            },

            // Not Word Search
            // Which of these consonants was missing in {0}?
            // Which of these consonants was missing in Not Word Search?
            [Question.NotWordSearchMissing] = new TranslationInfo
            {
                QuestionText = "Which of these consonants was missing in {0}？",
            },
            // What was the first correctly pressed letter in {0}?
            // What was the first correctly pressed letter in Not Word Search?
            [Question.NotWordSearchFirstPress] = new TranslationInfo
            {
                QuestionText = "What was the first correctly pressed letter in {0}？",
            },

            // Not X01
            // Which sector value {1} present on {0}?
            // Which sector value was present on Not X01?
            [Question.NotX01SectorValues] = new TranslationInfo
            {
                QuestionText = "Which sector value {1} present on {0}？",
            },

            // Not X-Ray
            // What table were we in in {0} (numbered 1–8 in reading order in the manual)?
            // What table were we in in Not X-Ray (numbered 1–8 in reading order in the manual)?
            [Question.NotXRayTable] = new TranslationInfo
            {
                QuestionText = "What table were we in in {0} (numbered 1–8 in reading order in the manual)？",
            },
            // What direction was button {1} in {0}?
            // What direction was button 1 in Not X-Ray?
            [Question.NotXRayDirections] = new TranslationInfo
            {
                QuestionText = "What direction was button {1} in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "下",
                    ["Right"] = "右",
                    ["Down"] = "下",
                    ["Left"] = "左",
                },
            },
            // Which button went {1} in {0}?
            // Which button went up in Not X-Ray?
            [Question.NotXRayButtons] = new TranslationInfo
            {
                QuestionText = "Which button went {1} in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["up"] = "下",
                    ["right"] = "右",
                    ["down"] = "下",
                    ["left"] = "左",
                },
            },
            // What was the scanner color in {0}?
            // What was the scanner color in Not X-Ray?
            [Question.NotXRayScannerColor] = new TranslationInfo
            {
                QuestionText = "What was the scanner color in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Yellow"] = "黄",
                    ["Blue"] = "青",
                    ["White"] = "白",
                },
            },

            // Numbered Buttons
            // Which number was correctly pressed on {0}?
            // Which number was correctly pressed on Numbered Buttons?
            [Question.NumberedButtonsButtons] = new TranslationInfo
            {
                QuestionText = "Which number was correctly pressed on {0}？",
            },

            // Numbers
            // What two-digit number was given in {0}?
            // What two-digit number was given in Numbers?
            [Question.NumbersTwoDigit] = new TranslationInfo
            {
                QuestionText = "{0}で与えられた二桁の数字は？",
            },

            // Numpath
            // What was the color of the number on {0}?
            // What was the color of the number on Numpath?
            [Question.NumpathColor] = new TranslationInfo
            {
                QuestionText = "What was the color of the number on {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Purple"] = "紫",
                },
            },
            // What was the number displayed on {0}?
            // What was the number displayed on Numpath?
            [Question.NumpathDigit] = new TranslationInfo
            {
                QuestionText = "What was the number displayed on {0}？",
            },

            // Object Shows
            // Which of these was a contestant on {0}?
            // Which of these was a contestant on Object Shows?
            [Question.ObjectShowsContestants] = new TranslationInfo
            {
                QuestionText = "Which of these was a contestant on {0} but not the final winner？",
            },

            // The Octadecayotton
            // What was the starting sphere in {0}?
            // What was the starting sphere in The Octadecayotton?
            [Question.OctadecayottonSphere] = new TranslationInfo
            {
                QuestionText = "{0}のスタートボールは？",
            },
            // What was one of the subrotations in the {1} rotation in {0}?
            // What was one of the subrotations in the first rotation in The Octadecayotton?
            [Question.OctadecayottonRotations] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目の回転の二次変形の一つであるのは？",
            },

            // Odd One Out
            // What was the button you pressed in the {1} stage of {0}?
            // What was the button you pressed in the first stage of Odd One Out?
            [Question.OddOneOutButton] = new TranslationInfo
            {
                QuestionText = "What was the button you pressed in the {1} stage of {0}？",
            },

            // Only Connect
            // Which Egyptian hieroglyph was in the {1} in {0}?
            // Which Egyptian hieroglyph was in the top left in Only Connect?
            [Question.OnlyConnectHieroglyphs] = new TranslationInfo
            {
                QuestionText = "{0}の{1}のヒエログリフは？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top left"] = "左上",
                    ["top middle"] = "上中央",
                    ["top right"] = "右上",
                    ["bottom left"] = "左下",
                    ["bottom middle"] = "下中央",
                    ["bottom right"] = "右下",
                },
            },

            // Orange Arrows
            // What was the {1} arrow on the display of the {2} stage of {0}?
            // What was the first arrow on the display of the first stage of Orange Arrows?
            [Question.OrangeArrowsSequences] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}における{1}番目の矢印は？",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "下",
                    ["Right"] = "右",
                    ["Down"] = "下",
                    ["Left"] = "左",
                },
            },

            // Orange Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Orange Cipher?
            [Question.OrangeCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // Ordered Keys
            // What color was the {2} key in the {1} stage of {0}?
            // What color was the first key in the first stage of Ordered Keys?
            [Question.OrderedKeysColors] = new TranslationInfo
            {
                QuestionText = "What color was the {2} key in the {1} stage of {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                    ["Cyan"] = "シアン",
                    ["Magenta"] = "マゼンタ",
                },
            },
            // What was the label on the {2} key in the {1} stage of {0}?
            // What was the label on the first key in the first stage of Ordered Keys?
            [Question.OrderedKeysLabels] = new TranslationInfo
            {
                QuestionText = "What was the label on the {2} key in the {1} stage of {0}？",
            },
            // What color was the label of the {2} key in the {1} stage of {0}?
            // What color was the label of the first key in the first stage of Ordered Keys?
            [Question.OrderedKeysLabelColors] = new TranslationInfo
            {
                QuestionText = "What color was the label of the {2} key in the {1} stage of {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                    ["Cyan"] = "シアン",
                    ["Magenta"] = "マゼンタ",
                },
            },

            // Order Picking
            // What was the order ID in the {1} order of {0}?
            // What was the order ID in the first order of Order Picking?
            [Question.OrderPickingOrder] = new TranslationInfo
            {
                QuestionText = "What was the order ID in the {1} order of {0}？",
            },
            // What was the product ID in the {1} order of {0}?
            // What was the product ID in the first order of Order Picking?
            [Question.OrderPickingProduct] = new TranslationInfo
            {
                QuestionText = "What was the product ID in the {1} order of {0}？",
            },
            // What was the pallet in the {1} order of {0}?
            // What was the pallet in the first order of Order Picking?
            [Question.OrderPickingPallet] = new TranslationInfo
            {
                QuestionText = "What was the pallet in the {1} order of {0}？",
            },

            // Orientation Cube
            // What was the observer’s initial position in {0}?
            // What was the observer’s initial position in Orientation Cube?
            [Question.OrientationCubeInitialObserverPosition] = new TranslationInfo
            {
                QuestionText = "{0}の最初の観測者の位置は？",
                Answers = new Dictionary<string, string>
                {
                    ["front"] = "正面",
                    ["left"] = "左",
                    ["back"] = "後",
                    ["right"] = "右",
                },
            },

            // Orientation Hypercube
            // What was the observer’s initial position in {0}?
            // What was the observer’s initial position in Orientation Hypercube?
            [Question.OrientationHypercubeInitialObserverPosition] = new TranslationInfo
            {
                QuestionText = "What was the observer’s initial position in {0}?",
            },
            // What was the initial colour of the {1} face in {0}?
            [Question.OrientationHypercubeInitialFaceColour] = new TranslationInfo
            {
                QuestionText = "What was the initial colour of the {1} face in {0}?",
            },

            // Palindromes
            // What was {1}’s {2} digit from the right in {0}?
            // What was X’s first digit from the right in Palindromes?
            [Question.PalindromesNumbers] = new TranslationInfo
            {
                QuestionText = "What was {1}’s {2} digit from the right in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["X"] = "X",
                    ["Y"] = "Y",
                    ["the screen"] = "the screen",
                },
            },

            // Parity
            // What was shown on the display on {0}?
            // What was shown on the display on Parity?
            [Question.ParityDisplay] = new TranslationInfo
            {
                QuestionText = "What was shown on the display on {0}?",
            },

            // Partial Derivatives
            // What was the LED color in the {1} stage of {0}?
            // What was the LED color in the first stage of Partial Derivatives?
            [Question.PartialDerivativesLedColors] = new TranslationInfo
            {
                QuestionText = "What was the LED color in the {1} stage of {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "青",
                    ["green"] = "緑",
                    ["orange"] = "オレンジ",
                    ["purple"] = "紫",
                    ["red"] = "赤",
                    ["yellow"] = "黄",
                },
            },
            // What was the {1} term in {0}?
            // What was the first term in Partial Derivatives?
            [Question.PartialDerivativesTerms] = new TranslationInfo
            {
                QuestionText = "What was the {1} term in {0}？",
            },

            // Passport Control
            // What was the passport expiration year of the {1} inspected passenger in {0}?
            // What was the passport expiration year of the first inspected passenger in Passport Control?
            [Question.PassportControlPassenger] = new TranslationInfo
            {
                QuestionText = "What was the passport expiration year of the {1} inspected passenger in {0}？",
            },

            // Password Destroyer
            // What was the starting value when you solved {0}?
            // What was the starting value when you solved Password Destroyer?
            [Question.PasswordDestroyerStartingValue] = new TranslationInfo
            {
                QuestionText = "What was the raw value when you solved {0}？",
            },
            // What was the increase factor when you solved {0}?
            // What was the increase factor when you solved Password Destroyer?
            [Question.PasswordDestroyerIncreaseFactor] = new TranslationInfo
            {
                QuestionText = "What was the increase factor when you solved {0}？",
            },
            // What was the TFA₁ value when you solved {0}?
            // What was the TFA₁ value when you solved Password Destroyer?
            [Question.PasswordDestroyerTF1] = new TranslationInfo
            {
                QuestionText = "What was the TFA₁ value when you solved {0}？",
            },
            // What was the TFA₂ value when you solved {0}?
            // What was the TFA₂ value when you solved Password Destroyer?
            [Question.PasswordDestroyerTF2] = new TranslationInfo
            {
                QuestionText = "What was the TFA₂ value when you solved {0}？",
            },
            // What was the 2FAST™ value when you solved {0}?
            // What was the 2FAST™ value when you solved Password Destroyer?
            [Question.PasswordDestroyerTwoFactorV2] = new TranslationInfo
            {
                QuestionText = "What was the 2FAST™ value when you solved {0}？",
            },
            // What was the percentage of solved modules used in the final calculation when you solved {0}?
            // What was the percentage of solved modules used in the final calculation when you solved Password Destroyer?
            [Question.PasswordDestroyerSolvePercentage] = new TranslationInfo
            {
                QuestionText = "What was the percentage of solved modules used in the final calculation when you solved {0}？",
            },

            // Pattern Cube
            // Which symbol was highlighted in {0}?
            // Which symbol was highlighted in Pattern Cube?
            [Question.PatternCubeHighlightedSymbol] = new TranslationInfo
            {
                QuestionText = "Which symbol was highlighted in {0}？",
            },

            // Perspective Pegs
            // What was the {1} color in the initial sequence in {0}?
            // What was the first color in the initial sequence in Perspective Pegs?
            [Question.PerspectivePegsColorSequence] = new TranslationInfo
            {
                QuestionText = "{0}の最初の色シーケンスで{1}番目の色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["yellow"] = "黄",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["purple"] = "紫",
                },
            },

            // Phosphorescence
            // What was the offset in {0}?
            // What was the offset in Phosphorescence?
            [Question.PhosphorescenceOffset] = new TranslationInfo
            {
                QuestionText = "What was the offset in {0}？",
            },
            // What was the {1} button press in {0}?
            // What was the first button press in Phosphorescence?
            [Question.PhosphorescenceButtonPresses] = new TranslationInfo
            {
                QuestionText = "What was the {1} button press in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["Azure"] = "空",
                    ["Blue"] = "青",
                    ["Crimson"] = "紅",
                    ["Diamond"] = "水色",
                    ["Emerald"] = "Emerald",
                    ["Fuchsia"] = "Fuchsia",
                    ["Green"] = "緑",
                    ["Ice"] = "Ice",
                    ["Jade"] = "翡翠",
                    ["Kiwi"] = "Kiwi",
                    ["Lime"] = "黄緑",
                    ["Magenta"] = "マゼンタ",
                    ["Navy"] = "Navy",
                    ["Orange"] = "オレンジ",
                    ["Purple"] = "紫",
                    ["Quartz"] = "Quartz",
                    ["Red"] = "赤",
                    ["Salmon"] = "Salmon",
                    ["Tan"] = "Tan",
                    ["Ube"] = "Ube",
                    ["Vibe"] = "Vibe",
                    ["White"] = "白",
                    ["Xotic"] = "Xotic",
                    ["Yellow"] = "黄",
                    ["Zen"] = "Zen",
                },
            },

            // Pie
            // What was the {1} digit of the displayed number in {0}?
            // What was the first digit of the displayed number in Pie?
            [Question.PieDigits] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に表示された数字は？",
            },

            // Pie Flash
            // What number was not displayed in {0}?
            // What number was not displayed in Pie Flash?
            [Question.PieFlashDigits] = new TranslationInfo
            {
                QuestionText = "What number was NOT displayed in {0}?",
            },

            // Pigpen Cycle
            // What was the {1} in {0}?
            // What was the message in Pigpen Cycle?
            [Question.PigpenCycleWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} in {0}？",
            },

            // The Pink Button
            // What was the {1} word in {0}?
            // What was the first word in The Pink Button?
            [Question.PinkButtonWords] = new TranslationInfo
            {
                QuestionText = "What was the {1} word in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["BLK"] = "BLK",
                    ["RED"] = "赤",
                    ["GRN"] = "GRN",
                    ["YLW"] = "YLW",
                    ["BLU"] = "BLU",
                    ["MGT"] = "MGT",
                    ["CYN"] = "CYN",
                    ["WHT"] = "WHT",
                },
            },
            // What was the {1} color in {0}?
            // What was the first color in The Pink Button?
            [Question.PinkButtonColors] = new TranslationInfo
            {
                QuestionText = "What was the {1} color in {0}？",
                Answers = new Dictionary<string, string>
                {
                    ["black"] = "黒",
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["yellow"] = "黄",
                    ["blue"] = "青",
                    ["magenta"] = "マゼンタ",
                    ["cyan"] = "シアン",
                    ["white"] = "白",
                },
            },

            // Pixel Cipher
            // What was the keyword in {0}?
            // What was the keyword in Pixel Cipher?
            [Question.PixelCipherKeyword] = new TranslationInfo
            {
                QuestionText = "What was the keyword in {0}？",
            },

            // Placeholder Talk
            // What was the first half of the first phrase in {0}?
            // What was the first half of the first phrase in Placeholder Talk?
            [Question.PlaceholderTalkFirstPhrase] = new TranslationInfo
            {
                QuestionText = "What was the first half of the first phrase in {0}？",
            },
            // What was the last half of the first phrase in {0}?
            // What was the last half of the first phrase in Placeholder Talk?
            [Question.PlaceholderTalkOrdinal] = new TranslationInfo
            {
                QuestionText = "What was the last half of the first phrase in {0}？",
            },
            // What was the second phrase’s calculated value in {0}?
            // What was the second phrase’s calculated value in Placeholder Talk?
            [Question.PlaceholderTalkSecondPhrase] = new TranslationInfo
            {
                QuestionText = "What was the second phrase’s calculated value in {0}？",
            },

            // Placement Roulette
            // What was the character listed on the information display in {0}?
            // What was the character listed on the information display in Placement Roulette?
            [Question.PlacementRouletteChar] = new TranslationInfo
            {
                QuestionText = "What was the character listed on the information display in {0}？",
            },
            // What was the drift type listed on the information display in {0}?
            // What was the drift type listed on the information display in Placement Roulette?
            [Question.PlacementRouletteDrift] = new TranslationInfo
            {
                QuestionText = "What was the drift type listed on the information display in {0}？",
            },
            // What was the track listed on the information display in {0}?
            // What was the track listed on the information display in Placement Roulette?
            [Question.PlacementRouletteTrack] = new TranslationInfo
            {
                QuestionText = "What was the track listed on the information display in {0}？",
            },
            // What was the track type of the track listed on the information display in {0}?
            // What was the track type of the track listed on the information display in Placement Roulette?
            [Question.PlacementRouletteTrackType] = new TranslationInfo
            {
                QuestionText = "What was the track type of the track listed on the information display in {0}？",
            },
            // What was the vehicle listed on the information display in {0}?
            // What was the vehicle listed on the information display in Placement Roulette?
            [Question.PlacementRouletteVehicle] = new TranslationInfo
            {
                QuestionText = "What was the vehicle listed on the information display in {0}？",
            },
            // What was the vehicle type of the vehicle listed on the information display in {0}?
            // What was the vehicle type of the vehicle listed on the information display in Placement Roulette?
            [Question.PlacementRouletteVehicleType] = new TranslationInfo
            {
                QuestionText = "What was the vehicle type of the vehicle listed on the information display in {0}？",
            },

            // Planets
            // What was the planet shown in {0}?
            // What was the planet shown in Planets?
            [Question.PlanetsPlanet] = new TranslationInfo
            {
                QuestionText = "{0}には何の惑星が表示されていた？",
            },
            // What was the color of the {1} strip (from the top) in {0}?
            // What was the color of the first strip (from the top) in Planets?
            [Question.PlanetsStrips] = new TranslationInfo
            {
                QuestionText = "{0}の上から{1}番目のストリップの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Aqua"] = "アクア",
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Lime"] = "黄緑",
                    ["Orange"] = "オレンジ",
                    ["Red"] = "赤",
                    ["Yellow"] = "黄",
                    ["White"] = "白",
                    ["Off"] = "オフ",
                },
            },

            // Playfair Cycle
            // What was the {1} in {0}?
            // What was the message in Playfair Cycle?
            [Question.PlayfairCycleWord] = new TranslationInfo
            {
                QuestionText = "{0}の{1}は？",
            },

            // Poetry
            // What was the {1} correct answer you pressed in {0}?
            // What was the first correct answer you pressed in Poetry?
            [Question.PoetryAnswers] = new TranslationInfo
            {
                QuestionText = "{0}において、{1}番目に押して正解だったフレーズは？",
            },

            // Polyhedral Maze
            // What was the starting position in {0}?
            // What was the starting position in Polyhedral Maze?
            [Question.PolyhedralMazeStartPosition] = new TranslationInfo
            {
                QuestionText = "{0}の開始番号は？",
            },

            // Prime Encryption
            // What was the number shown in {0}?
            // What was the number shown in Prime Encryption?
            [Question.PrimeEncryptionDisplayedValue] = new TranslationInfo
            {
                QuestionText = "{0}に表示されていた数字は？",
            },

            // Probing
            // What was the missing frequency in the {1} wire in {0}?
            // What was the missing frequency in the red-white wire in Probing?
            [Question.ProbingFrequencies] = new TranslationInfo
            {
                QuestionText = "{0}において、{1}のワイヤーに含まれていない周波数は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red-white"] = "赤白",
                    ["yellow-black"] = "黄黒",
                    ["green"] = "緑",
                    ["gray"] = "灰",
                    ["yellow-red"] = "黄赤",
                    ["red-blue"] = "赤青",
                },
            },

            // Purple Arrows
            // What was the target word on {0}?
            // What was the target word on Purple Arrows?
            [Question.PurpleArrowsFinish] = new TranslationInfo
            {
                QuestionText = "{0}のターゲット単語は？",
            },

            // The Purple Button
            // What was the {1} number in the cyclic sequence on {0}?
            // What was the first number in the cyclic sequence on The Purple Button?
            [Question.PurpleButtonNumbers] = new TranslationInfo
            {
                QuestionText = "{0}におけるサイクリックシークエンスの{1}番目の数字は？",
            },

            // Puzzle Identification
            // What was the {1} puzzle number in {0}?
            // What was the first puzzle number in Puzzle Identification?
            [Question.PuzzleIdentificationNum] = new TranslationInfo
            {
                QuestionText = "{0}の{1}回目の数字は？",
            },
            // What game was the {1} puzzle in {0} from?
            // What game was the first puzzle in Puzzle Identification from?
            [Question.PuzzleIdentificationGame] = new TranslationInfo
            {
                QuestionText = "{0}の{1}回目に使用されたゲームの種類は？",
            },
            // What was the {1} puzzle in {0}?
            // What was the first puzzle in Puzzle Identification?
            [Question.PuzzleIdentificationName] = new TranslationInfo
            {
                QuestionText = "{0}の{1}回目のパズル名は？",
            },

            // Quaver
            // What was the {1} sequence’s answer in {0}?
            // What was the first sequence’s answer in Quaver?
            [Question.QuaverArrows] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のシークエンスの回答は？",
            },

            // Quick Arithmetic
            // What was the {1} color in the primary sequence in {0}?
            // What was the first color in the primary sequence in Quick Arithmetic?
            [Question.QuickArithmeticColors] = new TranslationInfo
            {
                QuestionText = "What was the {1} color in the primary sequence in {0}?",
            },
            // What was the {1} digit in the {2} sequence in {0}?
            // What was the first digit in the primary sequence in Quick Arithmetic?
            [Question.QuickArithmeticPrimSecDigits] = new TranslationInfo
            {
                QuestionText = "What was the {1} digit in the {2} sequence in {0}?",
            },

            // Quintuples
            // What was the {1} digit in the {2} slot in {0}?
            // What was the first digit in the first slot in Quintuples?
            [Question.QuintuplesNumbers] = new TranslationInfo
            {
                QuestionText = "{0}の{2}番目のスロットの{1}番目の数字は？",
            },
            // What color was the {1} digit in the {2} slot in {0}?
            // What color was the first digit in the first slot in Quintuples?
            [Question.QuintuplesColors] = new TranslationInfo
            {
                QuestionText = "{0}の{2}番目のスロットの{1}番目の数字の色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["blue"] = "青",
                    ["orange"] = "オレンジ",
                    ["green"] = "緑",
                    ["pink"] = "ピンク",
                },
            },
            // How many numbers were {1} in {0}?
            // How many numbers were red in Quintuples?
            [Question.QuintuplesColorCounts] = new TranslationInfo
            {
                QuestionText = "How many numbers were {1} in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["blue"] = "青",
                    ["orange"] = "オレンジ",
                    ["green"] = "緑",
                    ["pink"] = "ピンク",
                },
            },

            // Railway Cargo Loading
            // What was the {1} coupled car in {0}?
            // What was the second coupled car in Railway Cargo Loading?
            [Question.RailwayCargoLoadingCars] = new TranslationInfo
            {
                QuestionText = "{0}の第{1}連結車両とは？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["second"] = "2",
                    ["third"] = "3",
                    ["4th"] = "4",
                    ["5th"] = "5",
                    ["6th"] = "6",
                    ["7th"] = "7",
                    ["8th"] = "8",
                    ["9th"] = "9",
                    ["10th"] = "10",
                    ["11th"] = "11th",
                    ["12th"] = "12th",
                    ["13th"] = "13th",
                    ["14th"] = "14",
                },
            },
            // Which freight table rule {1} in {0}?
            // Which freight table rule was met in Railway Cargo Loading?
            [Question.RailwayCargoLoadingFreightTableRules] = new TranslationInfo
            {
                QuestionText = "Which freight table rule {1} in {0}？",
            },

            // Rainbow Arrows
            // What was the displayed number in {0}?
            // What was the displayed number in Rainbow Arrows?
            [Question.RainbowArrowsNumber] = new TranslationInfo
            {
                QuestionText = "{0}のディスプレイの数字は？",
            },

            // Recolored Switches
            // What was the color of the {1} LED in {0}?
            // What was the color of the first LED in Recolored Switches?
            [Question.RecoloredSwitchesLedColors] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の位置にあるLEDの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["cyan"] = "シアン",
                    ["orange"] = "オレンジ",
                    ["purple"] = "紫",
                    ["white"] = "白",
                },
            },

            // Red Arrows
            // What was the starting number in {0}?
            // What was the starting number in Red Arrows?
            [Question.RedArrowsStartNumber] = new TranslationInfo
            {
                QuestionText = "{0}の開始地点の数字は？",
            },

            // Red Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Red Cipher?
            [Question.RedCipherAnswer] = new TranslationInfo
            {
                QuestionText = "{0}の回答は？",
            },

            // Red Herring
            // What was the first color flashed by {0}?
            // What was the first color flashed by Red Herring?
            [Question.RedHerringFirstFlash] = new TranslationInfo
            {
                QuestionText = "{0}において、最初に点滅した色は？",
            },

            // Reformed Role Reversal
            // Which condition was the solving condition in {0}?
            // Which condition was the solving condition in Reformed Role Reversal?
            [Question.ReformedRoleReversalCondition] = new TranslationInfo
            {
                QuestionText = "{0}の解除条件は？",
                Answers = new Dictionary<string, string>
                {
                    ["second"] = "2",
                    ["third"] = "3",
                    ["4th"] = "4",
                    ["5th"] = "5",
                    ["6th"] = "6",
                    ["7th"] = "7",
                    ["8th"] = "8",
                },
            },
            // What color was the {1} wire in {0}?
            // What color was the first wire in Reformed Role Reversal?
            [Question.ReformedRoleReversalWire] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のワイヤーの色は？",
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
                QuestionText = "{0}において、回答のフレーズに表示されていた数字は？",
            },
            // What was the embellishment of the solution phrase in {0}?
            // What was the embellishment of the solution phrase in Regular Crazy Talk?
            [Question.RegularCrazyTalkModifier] = new TranslationInfo
            {
                QuestionText = "{0}の回答のフレーズの装飾は？",
            },

            // Retirement
            // Which one of these houses was on offer, but not chosen by Bob in {0}?
            // Which one of these houses was on offer, but not chosen by Bob in Retirement?
            [Question.RetirementHouses] = new TranslationInfo
            {
                QuestionText = "{0}において、これらのうちBOBが定年後に選択しなかった家は？",
            },

            // Reverse Morse
            // What was the {1} character in the {2} message of {0}?
            // What was the first character in the first message of Reverse Morse?
            [Question.ReverseMorseCharacters] = new TranslationInfo
            {
                QuestionText = "{0}の{2}つめのメッセージの{1}文字目は？",
            },

            // Reverse Polish Notation
            // What character was used in the {1} round of {0}?
            // What character was used in the first round of Reverse Polish Notation?
            [Question.ReversePolishNotationCharacter] = new TranslationInfo
            {
                QuestionText = "{0}のラウンド{1}で使用された文字は？",
            },

            // RGB Maze
            // What was the exit coordinate in {0}?
            // What was the exit coordinate in RGB Maze?
            [Question.RGBMazeExit] = new TranslationInfo
            {
                QuestionText = "{0}の出口の座標は？",
            },
            // Where was the {1} key in {0}?
            // Where was the red key in RGB Maze?
            [Question.RGBMazeKeys] = new TranslationInfo
            {
                QuestionText = "{0}における{1}色のキーはどこ？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                },
            },
            // Which maze number was the {1} maze in {0}?
            // Which maze number was the red maze in RGB Maze?
            [Question.RGBMazeNumber] = new TranslationInfo
            {
                QuestionText = "Which maze number was the {1} maze in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                },
            },

            // Rhythms
            // What was the color in {0}?
            // What was the color in Rhythms?
            [Question.RhythmsColor] = new TranslationInfo
            {
                QuestionText = "{0}のLEDの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "青",
                    ["Red"] = "赤",
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                },
            },

            // The Rule
            // What was the rule number in {0}?
            // What was the rule number in The Rule?
            [Question.RuleNumber] = new TranslationInfo
            {
                QuestionText = "{0}のルール番号は？",
            },

            // Robo-Scanner
            // Where was the empty cell in {0}?
            // Where was the empty cell in Robo-Scanner?
            [Question.RoboScannerEmptyCell] = new TranslationInfo
            {
                QuestionText = "Where was the empty cell in {0}?",
            },

            // Roger
            // What four-digit number was given in {0}?
            // What four-digit number was given in Roger?
            [Question.RogerSeed] = new TranslationInfo
            {
                QuestionText = "{0}から与えられた4桁の数字は？",
            },

            // Role Reversal
            // What was the number to the correct condition in {0}?
            // What was the number to the correct condition in Role Reversal?
            [Question.RoleReversalNumber] = new TranslationInfo
            {
                QuestionText = "{0}の正しい状態の数字は？",
            },
            // How many {1} wires were there in {0}?
            // How many warm-colored wires were there in Role Reversal?
            [Question.RoleReversalWires] = new TranslationInfo
            {
                QuestionText = "{0}における{1}系のワイヤーの総数は？",
            },

            // Rule of Three
            // What was the {1} coordinate of the {2} vertex in {0}?
            // What was the X coordinate of the red vertex in Rule of Three?
            [Question.RuleOfThreeCoordinates] = new TranslationInfo
            {
                QuestionText = "{0}の{2}色の頂点の{1}座標は？",
            },
            // What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?
            // What was the position of the red sphere on the X axis in the first cycle in Rule of Three?
            [Question.RuleOfThreeCycles] = new TranslationInfo
            {
                QuestionText = "{0}の{3}回目のサイクルにおける{1}色の球の{2}軸上の位置は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["yellow"] = "黄",
                    ["Z"] = "Z",
                },
            },

            // The Samsung
            // Where was {1} in {0}?
            [Question.SamsungAppPositions] = new TranslationInfo
            {
                QuestionText = "{0}の{1}はどこ？",
            },

            // Scavenger Hunt
            // Which tile was correctly submitted in the first stage of {0}?
            // Which tile was correctly submitted in the first stage of Scavenger Hunt?
            [Question.ScavengerHuntKeySquare] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で正しく送信されたタイルは？",
            },
            // Which of these tiles was {1} in the first stage of {0}?
            // Which of these tiles was red in the first stage of Scavenger Hunt?
            [Question.ScavengerHuntColoredTiles] = new TranslationInfo
            {
                QuestionText = "{0}の最初のステージで{1}色だったタイルは？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                },
            },

            // Schlag den Bomb
            // What was the contestant’s name in {0}?
            // What was the contestant’s name in Schlag den Bomb?
            [Question.SchlagDenBombContestantName] = new TranslationInfo
            {
                QuestionText = "{0}の出場者の名前は？",
            },
            // What was the contestant’s score in {0}?
            // What was the contestant’s score in Schlag den Bomb?
            [Question.SchlagDenBombContestantScore] = new TranslationInfo
            {
                QuestionText = "{0}の出場者のスコアは？",
            },
            // What was the bomb’s score in {0}?
            // What was the bomb’s score in Schlag den Bomb?
            [Question.SchlagDenBombBombScore] = new TranslationInfo
            {
                QuestionText = "{0}の爆弾のスコアは？",
            },

            // Scramboozled Eggain
            // What was the {1} encrypted word in {0}?
            // What was the first encrypted word in Scramboozled Eggain?
            [Question.ScramboozledEggainWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} encrypted word in {0}?",
            },

            // Sea Shells
            // What were the first and second words in the {1} phrase in {0}?
            // What were the first and second words in the first phrase in Sea Shells?
            [Question.SeaShells1] = new TranslationInfo
            {
                QuestionText = "{0}の{1}フレーズ目で使用された1,2番目の単語は？",
            },
            // What were the third and fourth words in the {1} phrase in {0}?
            // What were the third and fourth words in the first phrase in Sea Shells?
            [Question.SeaShells2] = new TranslationInfo
            {
                QuestionText = "{0}の{1}フレーズ目で使用された3,4番目の単語は？",
            },
            // What was the end of the {1} phrase in {0}?
            // What was the end of the first phrase in Sea Shells?
            [Question.SeaShells3] = new TranslationInfo
            {
                QuestionText = "{0}の{1}フレーズ目で使用された最後の単語は？",
            },

            // Semamorse
            // What was the {1} letter involved in the starting value in {0}?
            // What was the Morse letter involved in the starting value in Semamorse?
            [Question.SemamorseLetters] = new TranslationInfo
            {
                QuestionText = "{0}の初期値を求める際に使用した表示のうち{1}の英字は？",
            },
            // What was the color of the display involved in the starting value in {0}?
            // What was the color of the display involved in the starting value in Semamorse?
            [Question.SemamorseColor] = new TranslationInfo
            {
                QuestionText = "{0}の初期値を求める際に使用した表示の色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["cyan"] = "シアン",
                    ["indigo"] = "藍色",
                    ["pink"] = "ピンク",
                },
            },

            // The Sequencyclopedia
            // What sequence was used in {0}?
            // What sequence was used in The Sequencyclopedia?
            [Question.SequencyclopediaSequence] = new TranslationInfo
            {
                QuestionText = "{0}では何のシークエンスが使用された？",
            },

            // Shapes And Bombs
            // What was the initial letter in {0}?
            // What was the initial letter in Shapes And Bombs?
            [Question.ShapesAndBombsInitialLetter] = new TranslationInfo
            {
                QuestionText = "{0}の初期の英字は？",
            },

            // Shape Shift
            // What was the initial shape in {0}?
            // What was the initial shape in Shape Shift?
            [Question.ShapeShiftInitialShape] = new TranslationInfo
            {
                QuestionText = "{0}の最初の図形は？",
            },

            // Shell Game
            // What was the final position of the initial cup in {0}?
            // What was the final position of the initial cup in Shell Game?
            [Question.ShellGameInitialCupFinalPosition] = new TranslationInfo
            {
                QuestionText = "{0}の初期カップの最終位置は？",
                Answers = new Dictionary<string, string>
                {
                    ["Left"] = "左",
                    ["Middle"] = "中央",
                    ["Right"] = "右",
                },
            },

            // Shifted Maze
            // What color was the {1} marker in {0}?
            // What color was the top-left marker in Shifted Maze?
            [Question.ShiftedMazeColors] = new TranslationInfo
            {
                QuestionText = "{0}の{1}にあるマークの色は？",
            },

            // Shifting Maze
            // What was the seed in {0}?
            // What was the seed in Shifting Maze?
            [Question.ShiftingMazeSeed] = new TranslationInfo
            {
                QuestionText = "{0}のシード値は？",
            },

            // Shogi Identification
            // What was the displayed piece in {0}?
            // What was the displayed piece in Shogi Identification?
            [Question.ShogiIdentificationPiece] = new TranslationInfo
            {
                QuestionText = "{0}に表示された駒は？",
            },

            // Silly Slots
            // What was the {1} slot in the {2} stage in {0}?
            // What was the first slot in the first stage in Silly Slots?
            [Question.SillySlots] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}において、{1}回目のスロットは？",
            },

            // Silo Authorization
            // What was the message type in {0}?
            // What was the message type in Silo Authorization?
            [Question.SiloAuthorizationMessageType] = new TranslationInfo
            {
                QuestionText = "What was the message type in {0}?",
            },
            // What was the {1} part of the encrypted message in {0}?
            // What was the first part of the encrypted message in Silo Authorization?
            [Question.SiloAuthorizationEncryptedMessage] = new TranslationInfo
            {
                QuestionText = "What was the {1} part of the encrypted message in {0}?",
            },
            // What was the received authentication code in {0}?
            // What was the received authentication code in Silo Authorization?
            [Question.SiloAuthorizationAuthCode] = new TranslationInfo
            {
                QuestionText = "What was the received authentication code in {0}?",
            },

            // Simon Said
            // What color was pressed {1} in the final sequence of {0}?
            // What color was pressed first in the final sequence of Simon Said?
            [Question.SimonSaidPresses] = new TranslationInfo
            {
                QuestionText = "What color was pressed in the {1} stage of {0}?",
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
                QuestionText = "{0}のステージ{1}の呼び出しは？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["played in the first stage"] = "played in the first stage",
                    ["added in the second stage"] = "added in the second stage",
                    ["added in the third stage"] = "added in the third stage",
                },
            },

            // Simon Says
            // What color flashed {1} in the final sequence in {0}?
            // What color flashed first in the final sequence in Simon Says?
            [Question.SimonSaysFlash] = new TranslationInfo
            {
                QuestionText = "{0}の最終シークエンスにおいて、{1}番目に点滅した色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["yellow"] = "黄",
                    ["green"] = "緑",
                    ["blue"] = "青",
                },
            },

            // Simon Scrambles
            // What color flashed {1} in {0}?
            // What color flashed first in Simon Scrambles?
            [Question.SimonScramblesColors] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の点滅は？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Yellow"] = "黄",
                },
            },

            // Simon Screams
            // Which color flashed {1} in the final sequence in {0}?
            // Which color flashed first in the final sequence in Simon Screams?
            [Question.SimonScreamsFlashing] = new TranslationInfo
            {
                QuestionText = "{0}の最終シークエンスにおいて、{1}番目に点滅した色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Purple"] = "紫",
                },
            },
            // In which stage(s) of {0} was “{1}” the applicable rule?
            // In which stage(s) of Simon Screams was “three adjacent colors flashing in clockwise order” the applicable rule?
            [Question.SimonScreamsRule] = new TranslationInfo
            {
                QuestionText = "In which stage(s) of {0} was “{1}” the applicable rule？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["three adjacent colors flashing in clockwise order"] = "three adjacent colors flashing in clockwise order",
                    ["a color flashing, then an adjacent color, then the first again"] = "a color flashing, then an adjacent color, then the first again",
                    ["at most one color flashing out of red, yellow, and blue"] = "at most one color flashing out of red, yellow, and blue",
                    ["two colors opposite each other that didn’t flash"] = "two colors opposite each other that didn’t flash",
                    ["two (but not three) adjacent colors flashing in clockwise order"] = "two (but not three) adjacent colors flashing in clockwise order",
                },
            },

            // Simon Selects
            // Which color flashed {1} in the {2} stage of {0}?
            // Which color flashed first in the first stage of Simon Selects?
            [Question.SimonSelectsOrder] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}において、{1}番目に点滅した色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Purple"] = "紫",
                    ["Magenta"] = "マゼンタ",
                    ["Cyan"] = "シアン",
                },
            },

            // Simon Sends
            // What was the {1} received letter in {0}?
            // What was the red received letter in Simon Sends?
            [Question.SimonSendsReceivedLetters] = new TranslationInfo
            {
                QuestionText = "{0}で{1}色が受け取った英字は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                },
            },

            // Simon Shapes
            // What was the shape submitted at the end of {0}?
            // What was the shape submitted at the end of Simon Shapes?
            [Question.SimonShapesSubmittedShape] = new TranslationInfo
            {
                QuestionText = "What was the shape submitted at the end of {0}?",
            },

            // Simon Simons
            // What was the {1} flash in the final sequence in {0}?
            // What was the first flash in the final sequence in Simon Simons?
            [Question.SimonSimonsFlashingColors] = new TranslationInfo
            {
                QuestionText = "{0}の最終シークエンスにおいて、{1}番目に点滅した色は？",
            },

            // Simon Sings
            // Which key’s color flashed {1} in the {2} stage of {0}?
            // Which key’s color flashed first in the first stage of Simon Sings?
            [Question.SimonSingsFlashing] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}において、{1}番目に点滅したキーは？",
            },

            // Simon Shouts
            // Which letter flashed on the {1} button in {0}?
            // Which letter flashed on the top button in Simon Shouts?
            [Question.SimonShoutsFlashingLetter] = new TranslationInfo
            {
                QuestionText = "{0}の{1}の位置が点滅した英字は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "上",
                    ["left"] = "左",
                    ["right"] = "右",
                    ["bottom"] = "下",
                },
            },

            // Simon Shrieks
            // How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?
            // How many spaces clockwise from the arrow was the first flash in the final sequence in Simon Shrieks?
            [Question.SimonShrieksFlashingButton] = new TranslationInfo
            {
                QuestionText = "{0}の最終シークエンスにおいて、{1}番目の点滅は矢印から時計回りに何番目のスペースにある？",
            },

            // Simon Sounds
            // Which sample button sounded {1} in the final sequence in {0}?
            // Which sample button sounded first in the final sequence in Simon Sounds?
            [Question.SimonSoundsFlashingColors] = new TranslationInfo
            {
                QuestionText = "{0}の最終シークエンスにおいて、{1}番目に再生されたサンプルボタンの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                    ["green"] = "緑",
                },
            },

            // Simon Speaks
            // Which bubble flashed first in {0}?
            // Which bubble flashed first in Simon Speaks?
            [Question.SimonSpeaksPositions] = new TranslationInfo
            {
                QuestionText = "{0}の1番目の点滅の吹き出しの色は？",
            },
            // Which bubble flashed second in {0}?
            // Which bubble flashed second in Simon Speaks?
            [Question.SimonSpeaksShapes] = new TranslationInfo
            {
                QuestionText = "{0}の2番目の点滅の吹き出しの色は？",
            },
            // Which language was the bubble that flashed third in {0} in?
            // Which language was the bubble that flashed third in Simon Speaks in?
            [Question.SimonSpeaksLanguages] = new TranslationInfo
            {
                QuestionText = "{0}の3回目の点滅の言語は？",
            },
            // Which word was in the bubble that flashed fourth in {0}?
            // Which word was in the bubble that flashed fourth in Simon Speaks?
            [Question.SimonSpeaksWords] = new TranslationInfo
            {
                QuestionText = "{0}の4番目の点滅の単語は？",
                Answers = new Dictionary<string, string>
                {
                    ["black"] = "black",
                    ["sort"] = "sort",
                    ["zwart"] = "zwart",
                    ["nigra"] = "nigra",
                    ["musta"] = "musta",
                    ["noir"] = "noir",
                    ["schwarz"] = "schwarz",
                    ["fekete"] = "fekete",
                    ["nero"] = "nero",
                    ["blue"] = "blue",
                    ["blå"] = "blå",
                    ["blauw"] = "blauw",
                    ["blua"] = "blua",
                    ["sininen"] = "sininen",
                    ["bleu"] = "bleu",
                    ["blau"] = "blau",
                    ["kék"] = "kék",
                    ["blu"] = "blu",
                    ["green"] = "green",
                    ["grøn"] = "grøn",
                    ["groen"] = "groen",
                    ["verda"] = "verda",
                    ["vihreä"] = "vihreä",
                    ["vert"] = "vert",
                    ["grün"] = "grün",
                    ["zöld"] = "zöld",
                    ["verde"] = "verde",
                    ["cyan"] = "cyan",
                    ["turkis"] = "turkis",
                    ["turkoois"] = "turkoois",
                    ["turkisa"] = "turkisa",
                    ["turkoosi"] = "turkoosi",
                    ["turquoise"] = "turquoise",
                    ["türkis"] = "türkis",
                    ["türkiz"] = "türkiz",
                    ["turchese"] = "turchese",
                    ["red"] = "red",
                    ["rød"] = "rød",
                    ["rood"] = "rood",
                    ["ruĝa"] = "ruĝa",
                    ["punainen"] = "punainen",
                    ["rouge"] = "rouge",
                    ["rot"] = "rot",
                    ["piros"] = "piros",
                    ["rosso"] = "rosso",
                    ["purple"] = "purple",
                    ["lilla"] = "lilla",
                    ["purper"] = "purper",
                    ["purpura"] = "purpura",
                    ["purppura"] = "purppura",
                    ["pourpre"] = "pourpre",
                    ["lila"] = "lila",
                    ["bíbor"] = "bíbor",
                    ["porpora"] = "porpora",
                    ["yellow"] = "yellow",
                    ["gul"] = "gul",
                    ["geel"] = "geel",
                    ["flava"] = "flava",
                    ["keltainen"] = "keltainen",
                    ["jaune"] = "jaune",
                    ["gelb"] = "gelb",
                    ["sárga"] = "sárga",
                    ["giallo"] = "giallo",
                    ["white"] = "white",
                    ["hvid"] = "hvid",
                    ["wit"] = "wit",
                    ["blanka"] = "blanka",
                    ["valkoinen"] = "valkoinen",
                    ["blanc"] = "blanc",
                    ["weiß"] = "weiß",
                    ["fehér"] = "fehér",
                    ["bianco"] = "bianco",
                    ["gray"] = "gray",
                    ["grå"] = "grå",
                    ["grijs"] = "grijs",
                    ["griza"] = "griza",
                    ["harmaa"] = "harmaa",
                    ["gris"] = "gris",
                    ["grau"] = "grau",
                    ["szürke"] = "szürke",
                    ["grigio"] = "grigio",
                },
            },
            // What color was the bubble that flashed fifth in {0}?
            // What color was the bubble that flashed fifth in Simon Speaks?
            [Question.SimonSpeaksColors] = new TranslationInfo
            {
                QuestionText = "{0}の5番目の点滅の吹き出しの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["black"] = "黒",
                    ["blue"] = "青",
                    ["green"] = "緑",
                    ["cyan"] = "シアン",
                    ["red"] = "赤",
                    ["purple"] = "紫",
                    ["yellow"] = "黄",
                    ["white"] = "白",
                    ["gray"] = "灰",
                },
            },

            // Simon’s Star
            // Which color flashed {1} in sequence in {0}?
            // Which color flashed first in sequence in Simon’s Star?
            [Question.SimonsStarColors] = new TranslationInfo
            {
                QuestionText = "{0}のシークエンスにおいて、{1}番目に点滅した色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["yellow"] = "黄",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["purple"] = "紫",
                },
            },

            // Simon Stages
            // Which color flashed {1} in the {2} stage in {0}?
            // Which color flashed first in the first stage in Simon Stages?
            [Question.SimonStagesFlashes] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}における{1}番目の点滅した色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                    ["orange"] = "オレンジ",
                    ["magenta"] = "マゼンタ",
                    ["green"] = "緑",
                    ["pink"] = "ピンク",
                    ["lime"] = "黄緑",
                    ["cyan"] = "シアン",
                    ["white"] = "白",
                },
            },
            // What color was the indicator in the {1} stage in {0}?
            // What color was the indicator in the first stage in Simon Stages?
            [Question.SimonStagesIndicator] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}におけるインジケーターの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                    ["orange"] = "オレンジ",
                    ["magenta"] = "マゼンタ",
                    ["green"] = "緑",
                    ["pink"] = "ピンク",
                    ["lime"] = "黄緑",
                    ["cyan"] = "シアン",
                    ["white"] = "白",
                },
            },

            // Simon States
            // Which {1} in the {2} stage in {0}?
            // Which color(s) flashed in the first stage in Simon States?
            [Question.SimonStatesDisplay] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}ではどの{1}？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Red, Yellow"] = "赤,黄",
                    ["Red, Green"] = "赤,緑",
                    ["Red, Blue"] = "赤,青",
                    ["Yellow, Green"] = "黄,緑",
                    ["Yellow, Blue"] = "黄,青",
                    ["Green, Blue"] = "緑, 青",
                    ["all 4"] = "4色すべて",
                    ["none"] = "なし",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["color(s) flashed"] = "色が点滅した",
                    ["color(s) didn’t flash"] = "色が点滅しなかった",
                },
            },

            // Simon Stops
            // Which color flashed {1} in the output sequence in {0}?
            // Which color flashed first in the output sequence in Simon Stops?
            [Question.SimonStopsColors] = new TranslationInfo
            {
                QuestionText = "{0}の出力シークエンスにおいて、{1}番目に点滅した色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Violet"] = "紫",
                },
            },

            // Simon Stores
            // Which color {1} {2} in the final sequence of {0}?
            // Which color flashed first in the final sequence of Simon Stores?
            [Question.SimonStoresColors] = new TranslationInfo
            {
                QuestionText = "{0}の最終シークエンスにおいて、{2}番目に{1}色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Cyan"] = "シアン",
                    ["Magenta"] = "マゼンタ",
                    ["Yellow"] = "黄",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["flashed"] = "点滅した",
                    ["was among the colors flashed"] = "点滅した色の中の",
                },
            },

            // Simon Subdivides
            // What color was the button at this position in {0}?
            // What color was the button at this position in Simon Subdivides?
            [Question.SimonSubdividesButton] = new TranslationInfo
            {
                QuestionText = "What color was the button at this position in {0}?",
            },

            // Simon Supports
            // What was the {1} topic in {0}?
            // What was the first topic in Simon Supports?
            [Question.SimonSupportsTopics] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のトピックは？",
            },

            // Skewed Slots
            // What were the original numbers in {0}?
            // What were the original numbers in Skewed Slots?
            [Question.SkewedSlotsOriginalNumbers] = new TranslationInfo
            {
                QuestionText = "{0}の初期値は？",
            },

            // Skyrim
            // Which race was selectable, but not the solution, in {0}?
            // Which race was selectable, but not the solution, in Skyrim?
            [Question.SkyrimRace] = new TranslationInfo
            {
                QuestionText = "{0}において、選択可能だが解除策ではなかった人種は？",
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
            [Question.SkyrimWeapon] = new TranslationInfo
            {
                QuestionText = "{0}において、選択可能だが解除策ではなかった武器は？",
            },
            // Which enemy was selectable, but not the solution, in {0}?
            // Which enemy was selectable, but not the solution, in Skyrim?
            [Question.SkyrimEnemy] = new TranslationInfo
            {
                QuestionText = "{0}において、選択可能だが解除策ではなかったエネミーは？",
            },
            // Which city was selectable, but not the solution, in {0}?
            // Which city was selectable, but not the solution, in Skyrim?
            [Question.SkyrimCity] = new TranslationInfo
            {
                QuestionText = "{0}において、選択可能だが解除策ではなかった都市は？",
            },
            // Which dragon shout was selectable, but not the solution, in {0}?
            // Which dragon shout was selectable, but not the solution, in Skyrim?
            [Question.SkyrimDragonShout] = new TranslationInfo
            {
                QuestionText = "{0}において、選択可能だが解除策ではなかったドラゴンは？",
            },

            // Slow Math
            // What was the last triplet of letters in {0}?
            // What was the last triplet of letters in Slow Math?
            [Question.SlowMathLastLetters] = new TranslationInfo
            {
                QuestionText = "What was the last pair of letters in {0}?",
            },

            // Small Circle
            // How much did the sequence shift by in {0}?
            // How much did the sequence shift by in Small Circle?
            [Question.SmallCircleShift] = new TranslationInfo
            {
                QuestionText = "{0}におけるシークエンスのシフト量は？",
            },
            // Which wedge made the different noise in the beginning of {0}?
            // Which wedge made the different noise in the beginning of Small Circle?
            [Question.SmallCircleWedge] = new TranslationInfo
            {
                QuestionText = "{0}の初期時点で音が違っていたのは？",
            },
            // Which color was {1} in the solution to {0}?
            // Which color was first in the solution to Small Circle?
            [Question.SmallCircleSolution] = new TranslationInfo
            {
                QuestionText = "{0}の解除シークエンスの{1}番目の色は？",
            },

            // Snooker
            // How many red balls were there at the start of {0}?
            // How many red balls were there at the start of Snooker?
            [Question.SnookerReds] = new TranslationInfo
            {
                QuestionText = "{0}の開始時点での赤いボールの数は？",
            },

            // Sorting
            // What positions were the last swap used to solve {0}?
            // What positions were the last swap used to solve Sorting?
            [Question.SortingLastSwap] = new TranslationInfo
            {
                QuestionText = "{0}を解く際の最後のスワップはどの位置で行われた？",
            },

            // Souvenir
            // What was the first module asked about in the other Souvenir on this bomb?
            // What was the first module asked about in the other Souvenir on this bomb?
            [Question.SouvenirFirstQuestion] = new TranslationInfo
            {
                QuestionText = "他の「思い出」モジュールが最初に質問したのは、何のモジュールについて？",
            },

            // Space Traders
            // What was the maximum tax amount per vessel in {0}?
            // What was the maximum tax amount per vessel in Space Traders?
            [Question.SpaceTradersMaxTax] = new TranslationInfo
            {
                QuestionText = "{0}での1隻当たりの最大税額は？",
            },

            // Sonic The Hedgehog
            // What was the {1} picture on {0}?
            // What was the first picture on Sonic The Hedgehog?
            [Question.SonicTheHedgehogPictures] = new TranslationInfo
            {
                QuestionText = "{0}における{1}番目の画像は？",
            },
            // Which sound was played by the {1} screen on {0}?
            // Which sound was played by the Running Boots screen on Sonic The Hedgehog?
            [Question.SonicTheHedgehogSounds] = new TranslationInfo
            {
                QuestionText = "{0}において、{1}のスクリーンで流れていたサウンドは？",
            },

            // The Sphere
            // What was the {1} flashed color in {0}?
            // What was the first flashed color in The Sphere?
            [Question.SphereColors] = new TranslationInfo
            {
                QuestionText = "{0}にて{1}番目に点滅した色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["blue"] = "青",
                    ["green"] = "緑",
                    ["orange"] = "オレンジ",
                    ["pink"] = "ピンク",
                    ["purple"] = "紫",
                    ["grey"] = "灰",
                    ["white"] = "白",
                },
            },

            // Spelling Bee
            // What word was asked to be spelled in {0}?
            // What word was asked to be spelled in Spelling Bee?
            [Question.SpellingBeeWord] = new TranslationInfo
            {
                QuestionText = "{0}で打ち込んだ単語は？",
            },

            // Splitting The Loot
            // What bag was initially colored in {0}?
            // What bag was initially colored in Splitting The Loot?
            [Question.SplittingTheLootColoredBag] = new TranslationInfo
            {
                QuestionText = "{0}にて初期から色付けされていた袋は？",
            },

            // Stability
            // What was the color of the {1} lit LED in {0}?
            // What was the color of the first lit LED in Stability?
            [Question.StabilityLedColors] = new TranslationInfo
            {
                QuestionText = "What was the color of the {1} lit LED in {0}?",
            },
            // What was the identification number in {0}?
            // What was the identification number in Stability?
            [Question.StabilityIdNumber] = new TranslationInfo
            {
                QuestionText = "What was the identification number in {0}?",
            },

            // Stacked Sequences
            // Which of these is the length of a sequence in {0}?
            // Which of these is the length of a sequence in Stacked Sequences?
            [Question.StackedSequences] = new TranslationInfo
            {
                QuestionText = "{0}のシークエンスの長さは？",
            },

            // Stars
            // What was the digit in the center of {0}?
            // What was the digit in the center of Stars?
            [Question.StarsCenter] = new TranslationInfo
            {
                QuestionText = "{0}の中心の数字は？",
            },

            // State of Aggregation
            // What was the element shown in {0}?
            // What was the element shown in State of Aggregation?
            [Question.StateOfAggregationElement] = new TranslationInfo
            {
                QuestionText = "{0}に表示された要素は？",
            },

            // Stellar
            // What was the {1} letter in {0}?
            // What was the Morse code letter in Stellar?
            [Question.StellarLetters] = new TranslationInfo
            {
                QuestionText = "{0}における{1}の英字は？",
            },

            // Stupid Slots
            // What was the value of the {1} arrow in {0}?
            // What was the value of the top-left arrow in Stupid Slots?
            [Question.StupidSlotsValues] = new TranslationInfo
            {
                QuestionText = "{0}の{1}にある矢印の値は？",
            },

            // Subscribe to Pewdiepie
            // How many subscribers does {1} have in {0}?
            // How many subscribers does PewDiePie have in Subscribe to Pewdiepie?
            [Question.SubscribeToPewdiepieSubCount] = new TranslationInfo
            {
                QuestionText = "{0}における{1}のサブスクライバーの数は？",
            },

            // Sugar Skulls
            // What skull was shown on the {1} square in {0}?
            // What skull was shown on the top square in Sugar Skulls?
            [Question.SugarSkullsSkull] = new TranslationInfo
            {
                QuestionText = "{0}にて{1}の位置に表示された骸骨は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "上",
                    ["bottom-left"] = "左下",
                    ["bottom-right"] = "右下",
                },
            },
            // Which skull {1} present in {0}?
            // Which skull was present in Sugar Skulls?
            [Question.SugarSkullsAvailability] = new TranslationInfo
            {
                QuestionText = "{0}に表示されて{1}骸骨は？",
            },

            // Superparsing
            // What was the displayed word in {0}?
            // What was the displayed word in Superparsing?
            [Question.SuperparsingDisplayed] = new TranslationInfo
            {
                QuestionText = "{0}で表示された単語は？",
            },

            // The Switch
            // What color was the {1} LED on the {2} flip of {0}?
            // What color was the top LED on the first flip of The Switch?
            [Question.SwitchInitialColor] = new TranslationInfo
            {
                QuestionText = "{0}の{2}回目の切り替え時の{1}部のLEDの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["orange"] = "オレンジ",
                    ["yellow"] = "黄",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["purple"] = "紫",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "上",
                    ["bottom"] = "下",
                },
            },

            // Switches
            // What was the initial position of the switches in {0}?
            // What was the initial position of the switches in Switches?
            [Question.SwitchesInitialPosition] = new TranslationInfo
            {
                QuestionText = "{0}の最初の状態は？",
            },

            // Switching Maze
            // What was the seed in {0}?
            // What was the seed in Switching Maze?
            [Question.SwitchingMazeSeed] = new TranslationInfo
            {
                QuestionText = "{0}のシード値は？",
            },
            // What was the starting maze color in {0}?
            // What was the starting maze color in Switching Maze?
            [Question.SwitchingMazeColor] = new TranslationInfo
            {
                QuestionText = "{0}の開始迷路の色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "青",
                    ["Cyan"] = "シアン",
                    ["Magenta"] = "マゼンタ",
                    ["Orange"] = "オレンジ",
                    ["Red"] = "赤",
                    ["White"] = "白",
                },
            },

            // Symbol Cycle
            // How many symbols were cycling on the {1} screen in {0}?
            // How many symbols were cycling on the left screen in Symbol Cycle?
            [Question.SymbolCycleSymbolCounts] = new TranslationInfo
            {
                QuestionText = "{0}にて{1}側のディスプレイに表示されたシンボルの数は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "左",
                    ["right"] = "右",
                },
            },

            // Symbolic Coordinates
            // What was the {1} symbol in the {2} stage of {0}?
            // What was the left symbol in the first stage of Symbolic Coordinates?
            [Question.SymbolicCoordinateSymbols] = new TranslationInfo
            {
                QuestionText = "Symbolic Coordinatesのステージ1における左のシンボルは？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "左",
                    ["middle"] = "中央",
                },
            },

            // Symbolic Tasha
            // Which button flashed {1} in the final sequence of {0}?
            // Which button flashed first in the final sequence of Symbolic Tasha?
            [Question.SymbolicTashaFlashes] = new TranslationInfo
            {
                QuestionText = "{0}の最後のシークエンスで{1}番目に点滅したものは？",
            },
            // Which symbol was on the {1} button in {0}?
            // Which symbol was on the top button in Symbolic Tasha?
            [Question.SymbolicTashaSymbols] = new TranslationInfo
            {
                QuestionText = "{0}の{1}の位置のシンボルは？",
            },

            // SYNC-125 [3]
            // What was displayed on the screen in stage {1} of {0}?
            // What was displayed on the screen in stage 1 of SYNC-125 [3]?
            [Question.Sync125_3Word] = new TranslationInfo
            {
                QuestionText = "{0}にてステージ{1}でスクリーンに表示されたものは？",
            },

            // Synonyms
            // Which number was displayed on {0}?
            // Which number was displayed on Synonyms?
            [Question.SynonymsNumber] = new TranslationInfo
            {
                QuestionText = "{0}のディスプレイの数字は？",
            },

            // Sysadmin
            // What error code did you fix in {0}?
            // What error code did you fix in Sysadmin?
            [Question.SysadminFixedErrorCodes] = new TranslationInfo
            {
                QuestionText = "{0}で修正したエラーコードは？",
            },

            // Tap Code
            // What was the received word in {0}?
            // What was the received word in Tap Code?
            [Question.TapCodeReceivedWord] = new TranslationInfo
            {
                QuestionText = "{0}で受信した単語は？",
            },

            // Tasha Squeals
            // What was the {1} flashed color in {0}?
            // What was the first flashed color in Tasha Squeals?
            [Question.TashaSquealsColors] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に点滅した色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Pink"] = "ピンク",
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                    ["Blue"] = "青",
                },
            },

            // Tasque Managing
            // Where was the starting position in {0}?
            // Where was the starting position in Tasque Managing?
            [Question.TasqueManagingStartingPos] = new TranslationInfo
            {
                QuestionText = "Where was the starting position in {0}?",
            },

            // Ten-Button Color Code
            // What was the initial color of the {1} button in the {2} stage of {0}?
            // What was the initial color of the first button in the first stage of Ten-Button Color Code?
            [Question.TenButtonColorCodeInitialColors] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}における{1}番目のボタンの初期の色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["yellow"] = "黄",
                },
            },

            // Tenpins
            // What was the {1} split in {0}?
            // What was the red split in Tenpins?
            [Question.TenpinsSplits] = new TranslationInfo
            {
                QuestionText = "{0}の{1}のスプリットは？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                },
            },

            // Text Field
            // What was the displayed letter in {0}?
            // What was the displayed letter in Text Field?
            [Question.TextFieldDisplay] = new TranslationInfo
            {
                QuestionText = "{0}で表示された文字は？",
            },

            // Thinking Wires
            // What was the position from top to bottom of the first wire needing to be cut in {0}?
            // What was the position from top to bottom of the first wire needing to be cut in Thinking Wires?
            [Question.ThinkingWiresFirstWire] = new TranslationInfo
            {
                QuestionText = "{0}において最初に切る必要のあるワイヤーの位置(上から下)は？",
            },
            // What color did the second valid wire to cut have to have in {0}?
            // What color did the second valid wire to cut have to have in Thinking Wires?
            [Question.ThinkingWiresSecondWire] = new TranslationInfo
            {
                QuestionText = "{0}において2番目に切った有効なワイヤーの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Cyan"] = "シアン",
                    ["Magenta"] = "マゼンタ",
                    ["Yellow"] = "黄",
                    ["White"] = "白",
                    ["Black"] = "黒",
                    ["Any"] = "Any",
                },
            },
            // What was the display number in {0}?
            // What was the display number in Thinking Wires?
            [Question.ThinkingWiresDisplayNumber] = new TranslationInfo
            {
                QuestionText = "{0}のディスプレイの数字は？",
            },

            // Third Base
            // What was the display word in the {1} stage on {0}?
            // What was the display word in the first stage on Third Base?
            [Question.ThirdBaseDisplay] = new TranslationInfo
            {
                QuestionText = "{0}にてステージ{1}で表示された単語は？",
            },

            // Tic Tac Toe
            // What was on the {1} button at the start of {0}?
            // What was on the top-left button at the start of Tic Tac Toe?
            [Question.TicTacToeInitialState] = new TranslationInfo
            {
                QuestionText = "{0}の{1}のボタンの初期状態は？",
            },

            // Timezone
            // What was the {1} city in {0}?
            // What was the departure city in Timezone?
            [Question.TimezoneCities] = new TranslationInfo
            {
                QuestionText = "{0}の{1}都市は？",
            },

            // Topsy Turvy
            // What was the word initially shown in {0}?
            // What was the word initially shown in Topsy Turvy?
            [Question.TopsyTurvyWord] = new TranslationInfo
            {
                QuestionText = "{0}で最初に表示された単語は？",
            },

            // Touch Transmission
            // What was the transmitted word in {0}?
            // What was the transmitted word in Touch Transmission?
            [Question.TouchTransmissionWord] = new TranslationInfo
            {
                QuestionText = "{0}で送信した単語は？",
            },
            // In what order was the Braille read in {0}?
            // In what order was the Braille read in Touch Transmission?
            [Question.TouchTransmissionOrder] = new TranslationInfo
            {
                QuestionText = "{0}では点字をどのような順序で読んだ？",
            },

            // Trajectory
            // Which function did the {1} button perform in {0}?
            // Which function did the A button perform in Trajectory?
            [Question.TrajectoryButtonFunctions] = new TranslationInfo
            {
                QuestionText = "{0}でのボタン{1}の役割は？",
            },

            // Transmitted Morse
            // What was the {1} received message in {0}?
            // What was the first received message in Transmitted Morse?
            [Question.TransmittedMorseMessage] = new TranslationInfo
            {
                QuestionText = "{0}にて{1}番目に受け取ったメッセージは？",
                Answers = new Dictionary<string, string>
                {
                    ["BOMBS"] = "BOMBS",
                    ["SHORT"] = "SHORT",
                    ["UNDERSTOOD"] = "UNDERSTOOD",
                    ["W1RES"] = "W1RES",
                    ["SOS"] = "SOS",
                    ["MANUAL"] = "MANUAL",
                    ["STRIKED"] = "STRIKED",
                    ["WEREDEAD"] = "WEREDEAD",
                    ["GOTASOUV"] = "GOTASOUV",
                    ["EXPLOSION"] = "EXPLOSION",
                    ["EXPERT"] = "EXPERT",
                    ["RIP"] = "RIP",
                    ["LISTEN"] = "LISTEN",
                    ["DETONATE"] = "DETONATE",
                    ["ROGER"] = "ROGER",
                    ["WELOSTBRO"] = "WELOSTBRO",
                    ["AMIDEAF"] = "AMIDEAF",
                    ["KEYPAD"] = "KEYPAD",
                    ["DEFUSER"] = "DEFUSER",
                    ["NUCLEARWEAPONS"] = "NUCLEARWEAPONS",
                    ["KAPPA"] = "KAPPA",
                    ["DELTA"] = "DELTA",
                    ["PI3"] = "PI3",
                    ["SMOKE"] = "SMOKE",
                    ["SENDHELP"] = "SENDHELP",
                    ["LOST"] = "LOST",
                    ["SWAN"] = "SWAN",
                    ["NOMNOM"] = "NOMNOM",
                    ["BLUE"] = "BLUE",
                    ["BOOM"] = "BOOM",
                    ["CANCEL"] = "CANCEL",
                    ["DEFUSED"] = "DEFUSED",
                    ["BROKEN"] = "BROKEN",
                    ["MEMORY"] = "MEMORY",
                    ["R6S8T"] = "R6S8T",
                    ["TRANSMISSION"] = "TRANSMISSION",
                    ["UMWHAT"] = "UMWHAT",
                    ["GREEN"] = "GREEN",
                    ["EQUATIONSX"] = "EQUATIONSX",
                    ["RED"] = "RED",
                    ["ENERGY"] = "ENERGY",
                    ["JESTER"] = "JESTER",
                    ["CONTACT"] = "CONTACT",
                    ["LONG"] = "LONG",
                },
            },

            // Triple Term
            // Which of these was one of the passwords in {0}?
            // Which of these was one of the passwords in Triple Term?
            [Question.TripleTermPasswords] = new TranslationInfo
            {
                QuestionText = "Which of these was one of the passwords in {0}?",
            },

            // Turtle Robot
            // What was the {1} line you commented out in {0}?
            // What was the first line you commented out in Turtle Robot?
            [Question.TurtleRobotCodeLines] = new TranslationInfo
            {
                QuestionText = "{0}でコメントアウトした行の{1}番目は？",
            },

            // Two Bits
            // What was the {1} correct query response from {0}?
            // What was the first correct query response from Two Bits?
            [Question.TwoBitsResponse] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目のクエリの返答は？",
            },

            // Ultimate Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Ultimate Cipher?
            [Question.UltimateCipherAnswer] = new TranslationInfo
            {
                QuestionText = "{0}の回答は？",
            },

            // Ultimate Cycle
            // What was the {1} in {0}?
            // What was the message in Ultimate Cycle?
            [Question.UltimateCycleWord] = new TranslationInfo
            {
                QuestionText = "{0}の{1}は？",
            },

            // The Ultracube
            // What was the {1} rotation in {0}?
            // What was the first rotation in The Ultracube?
            [Question.UltracubeRotations] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の回転方向は？",
            },

            // UltraStores
            // What was the {1} rotation in the {2} stage of {0}?
            // What was the first rotation in the first stage of UltraStores?
            [Question.UltraStoresSingleRotation] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}における{1}番目の回転方向は？",
            },
            // What was the {1} rotation in the {2} stage of {0}?
            // What was the first rotation in the first stage of UltraStores?
            [Question.UltraStoresMultiRotation] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}における{1}番目の回転方向は？",
            },

            // Uncolored Squares
            // What was the {1} color in reading order used in the first stage of {0}?
            // What was the first color in reading order used in the first stage of Uncolored Squares?
            [Question.UncoloredSquaresFirstStage] = new TranslationInfo
            {
                QuestionText = "{0}の最初のステージで利用したもののうち読み順で{1}番目の色は何色？",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "白",
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                    ["Magenta"] = "マゼンタ",
                },
            },

            // Uncolored Switches
            // What was the initial state of the switches in {0}?
            // What was the initial state of the switches in Uncolored Switches?
            [Question.UncoloredSwitchesInitialState] = new TranslationInfo
            {
                QuestionText = "{0}の最初のスイッチの状態は？",
            },
            // What color was the {1} LED in reading order in {0}?
            // What color was the first LED in reading order in Uncolored Switches?
            [Question.UncoloredSwitchesLedColors] = new TranslationInfo
            {
                QuestionText = "{0}の読み順で{1}番目のLEDは何色？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["turquoise"] = "水色",
                    ["orange"] = "オレンジ",
                    ["purple"] = "紫",
                    ["white"] = "白",
                    ["black"] = "黒",
                },
            },

            // Unfair Cipher
            // What was the {1} received instruction in {0}?
            // What was the first received instruction in Unfair Cipher?
            [Question.UnfairCipherInstructions] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に受け取った指示は？",
            },

            // Unfair’s Revenge
            // What was the {1} decrypted instruction in {0}?
            // What was the first decrypted instruction in Unfair’s Revenge?
            [Question.UnfairsRevengeInstructions] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に解読した指示は？",
            },

            // Unicode
            // What was the {1} submitted code in {0}?
            // What was the first submitted code in Unicode?
            [Question.UnicodeSortedAnswer] = new TranslationInfo
            {
                QuestionText = "{0}にて{1}番目に送信したコードは？",
            },

            // Unown Cipher
            // What was the {1} submitted letter in {0}?
            // What was the first submitted letter in Unown Cipher?
            [Question.UnownCipherAnswers] = new TranslationInfo
            {
                QuestionText = "{0}にて送信した単語の{1}番目の英字は？",
            },

            // USA Cycle
            // Which state was displayed in {0}?
            // Which state was displayed in USA Cycle?
            [Question.USACycleDisplayed] = new TranslationInfo
            {
                QuestionText = "Which state was displayed in {0}?",
            },

            // USA Maze
            // Which state did you depart from in {0}?
            // Which state did you depart from in USA Maze?
            [Question.USAMazeOrigin] = new TranslationInfo
            {
                QuestionText = "{0}の開始地点は？",
            },

            // V
            // Which word {1} shown in {0}?
            // Which word was shown in V?
            [Question.VWords] = new TranslationInfo
            {
                QuestionText = "{0}が表示{1}単語は？",
            },

            // Varicolored Squares
            // What was the initially pressed color on {0}?
            // What was the initially pressed color on Varicolored Squares?
            [Question.VaricoloredSquaresInitialColor] = new TranslationInfo
            {
                QuestionText = "{0}で最初に押した色は？",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "白",
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                    ["Magenta"] = "マゼンタ",
                },
            },

            // Varicolour Flash
            // What was the word of the {1} goal in {0}?
            // What was the word of the first goal in Varicolour Flash?
            [Question.VaricolourFlashWords] = new TranslationInfo
            {
                QuestionText = "What was the word of the {1} goal in {0}?",
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
                QuestionText = "What was the colour of the {1} goal in {0}?",
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
                QuestionText = "{0}で表示された単語は？",
            },

            // Vectors
            // What was the color of the {1} vector in {0}?
            // What was the color of the first vector in Vectors?
            [Question.VectorsColors] = new TranslationInfo
            {
                QuestionText = "{0}にて{1}番目のベクターの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Yellow"] = "黄",
                    ["Green"] = "緑",
                    ["Blue"] = "青",
                    ["Purple"] = "紫",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["first"] = "1",
                    ["second"] = "2",
                    ["third"] = "3",
                    ["only"] = "only",
                },
            },

            // Vexillology
            // What was the {1} flagpole color on {0}?
            // What was the first flagpole color on Vexillology?
            [Question.VexillologyColors] = new TranslationInfo
            {
                QuestionText = "{0}にてポールの色の{1}番目の色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Green"] = "緑",
                    ["Yellow"] = "黄",
                    ["Blue"] = "青",
                    ["Aqua"] = "アクア",
                    ["White"] = "白",
                    ["Black"] = "黒",
                },
            },

            // Violet Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Violet Cipher?
            [Question.VioletCipherAnswer] = new TranslationInfo
            {
                QuestionText = "{0}の回答は？",
            },

            // Visual Impairment
            // What was the desired color in the {1} stage on {0}?
            // What was the desired color in the first stage on Visual Impairment?
            [Question.VisualImpairmentColors] = new TranslationInfo
            {
                QuestionText = "{0}にてステージ{1}で押す必要のあった色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Red"] = "赤",
                    ["White"] = "白",
                },
            },

            // Wavetapping
            // What was the color on the {1} stage in {0}?
            // What was the color on the first stage in Wavetapping?
            [Question.WavetappingColors] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}の色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "赤",
                    ["Orange"] = "オレンジ",
                    ["Orange-Yellow"] = "Orange-Yellow",
                    ["Chartreuse"] = "Chartreuse",
                    ["Lime"] = "黄緑",
                    ["Green"] = "緑",
                    ["Seafoam Green"] = "Seafoam Green",
                    ["Cyan-Green"] = "Cyan-Green",
                    ["Turquoise"] = "Turquoise",
                    ["Dark Blue"] = "Dark Blue",
                    ["Indigo"] = "藍色",
                    ["Purple"] = "紫",
                    ["Purple-Magenta"] = "Purple-Magenta",
                    ["Magenta"] = "マゼンタ",
                    ["Pink"] = "ピンク",
                    ["Gray"] = "灰",
                },
            },
            // What was the correct pattern on the {1} stage in {0}?
            // What was the correct pattern on the first stage in Wavetapping?
            [Question.WavetappingPatterns] = new TranslationInfo
            {
                QuestionText = "{0}にてステージ{1}の正しいパターンは？",
            },

            // What’s on Second
            // What was the display text in the {1} stage of {0}?
            // What was the display text in the first stage of What’s on Second?
            [Question.WhatsOnSecondDisplayText] = new TranslationInfo
            {
                QuestionText = "{0}にてステージ{1}で表示されたテキストは？",
                Answers = new Dictionary<string, string>
                {
                    ["got it"] = "got it",
                    ["says"] = "says",
                    ["display"] = "display",
                    ["leed"] = "leed",
                    ["their"] = "their",
                    ["blank"] = "blank",
                    ["right"] = "right",
                    ["reed"] = "reed",
                    ["hold"] = "hold",
                    ["they are"] = "they are",
                    ["louder"] = "louder",
                    ["lead"] = "lead",
                    ["repeat"] = "repeat",
                    ["ready"] = "ready",
                    ["none"] = "none",
                    ["led"] = "led",
                    ["ur"] = "ur",
                    ["you’re"] = "you’re",
                    ["no"] = "no",
                    ["you"] = "you",
                    ["nothing"] = "nothing",
                    ["middle"] = "middle",
                    ["done"] = "done",
                    ["empty"] = "empty",
                    ["your"] = "your",
                    ["hold on"] = "hold on",
                    ["like"] = "like",
                    ["read"] = "read",
                    ["wait"] = "wait",
                    ["left"] = "left",
                    ["press"] = "press",
                    ["what?"] = "what?",
                    ["uh uh"] = "uh uh",
                    ["they’re"] = "they’re",
                    ["uhhh"] = "uhhh",
                    ["c"] = "c",
                    ["error"] = "error",
                    ["you are"] = "you are",
                    ["next"] = "next",
                    ["yes"] = "yes",
                    ["u"] = "u",
                    ["sure"] = "sure",
                    ["okay"] = "okay",
                    ["what"] = "what",
                    ["cee"] = "cee",
                    ["first"] = "1",
                    ["see"] = "see",
                    ["uh huh"] = "uh huh",
                    ["there"] = "there",
                    ["red"] = "red",
                },
            },
            // What was the display text color in the {1} stage of {0}?
            // What was the display text color in the first stage of What’s on Second?
            [Question.WhatsOnSecondDisplayColor] = new TranslationInfo
            {
                QuestionText = "{0}にてステージ{1}で表示されたテキストの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "青",
                    ["Cyan"] = "シアン",
                    ["Green"] = "緑",
                    ["Magenta"] = "マゼンタ",
                    ["Red"] = "赤",
                    ["Yellow"] = "黄",
                },
            },

            // White Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in White Cipher?
            [Question.WhiteCipherAnswer] = new TranslationInfo
            {
                QuestionText = "{0}の回答は？",
            },

            // WhoOF
            // What was the display in the {1} stage on {0}?
            // What was the display in the first stage on WhoOF?
            [Question.WhoOFDisplay] = new TranslationInfo
            {
                QuestionText = "What was the display in the {1} stage on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["FIRST"] = "FIRST",
                    ["OKAY"] = "OKAY",
                    ["C"] = "C",
                    ["BLANK"] = "BLANK",
                    ["YOU"] = "YOU",
                    ["READ"] = "READ",
                    ["YOUR"] = "YOUR",
                    ["UR"] = "UR",
                    ["YES"] = "YES",
                    ["LED"] = "LED",
                    ["THEIR"] = "THEIR",
                    ["RED"] = "RED",
                    ["HIRE"] = "HIRE",
                    ["THERE"] = "THERE",
                    ["THEY"] = "THEY",
                    ["THING"] = "THING",
                    ["CEE"] = "CEE",
                    ["LEED"] = "LEED",
                    ["NO"] = "NO",
                    ["HOLD"] = "HOLD",
                    ["PLAY"] = "PLAY",
                    ["LEAD"] = "LEAD",
                    ["HARE"] = "HARE",
                    ["HERE"] = "HERE",
                    [" "] = " ",
                    ["REED"] = "REED",
                    ["SAYS"] = "SAYS",
                    ["SEE"] = "SEE",
                },
            },

            // Who’s on First
            // What was the display in the {1} stage on {0}?
            // What was the display in the first stage on Who’s on First?
            [Question.WhosOnFirstDisplay] = new TranslationInfo
            {
                QuestionText = "{0}にてステージ{1}で表示された単語は？",
                Answers = new Dictionary<string, string>
                {
                    [""] = "",
                    ["BLANK"] = "BLANK",
                    ["C"] = "C",
                    ["CEE"] = "CEE",
                    ["DISPLAY"] = "DISPLAY",
                    ["FIRST"] = "1",
                    ["HOLD ON"] = "HOLD ON",
                    ["LEAD"] = "LEAD",
                    ["LED"] = "LED",
                    ["LEED"] = "LEED",
                    ["NO"] = "NO",
                    ["NOTHING"] = "NOTHING",
                    ["OK"] = "OK",
                    ["OKAY"] = "OKAY",
                    ["READ"] = "READ",
                    ["RED"] = "RED",
                    ["REED"] = "REED",
                    ["SAY"] = "SAY",
                    ["SAYS"] = "SAYS",
                    ["SEE"] = "SEE",
                    ["THEIR"] = "THEIR",
                    ["THERE"] = "THERE",
                    ["THEY ARE"] = "THEY ARE",
                    ["THEY’RE"] = "THEY’RE",
                    ["U"] = "U",
                    ["UR"] = "UR",
                    ["YES"] = "YES",
                    ["YOU"] = "YOU",
                    ["YOU ARE"] = "YOU ARE",
                    ["YOU’RE"] = "YOU’RE",
                    ["YOUR"] = "YOUR",
                },
            },

            // Who’s on Morse
            // What word was transmitted in the {1} stage on {0}?
            // What word was transmitted in the first stage on Who’s on Morse?
            [Question.WhosOnMorseTransmitDisplay] = new TranslationInfo
            {
                QuestionText = "What word was transmitted in the {1} stage on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["SHELL"] = "SHELL",
                    ["HALLS"] = "HALLS",
                    ["SLICK"] = "SLICK",
                    ["TRICK"] = "TRICK",
                    ["BOXES"] = "BOXES",
                    ["LEAKS"] = "LEAKS",
                    ["STROBE"] = "STROBE",
                    ["BISTRO"] = "BISTRO",
                    ["FLICK"] = "FLICK",
                    ["BOMBS"] = "BOMBS",
                    ["BREAK"] = "BREAK",
                    ["BRICK"] = "BRICK",
                    ["STEAK"] = "STEAK",
                    ["STING"] = "STING",
                    ["VECTOR"] = "VECTOR",
                    ["BEATS"] = "BEATS",
                    ["CURSE"] = "CURSE",
                    ["NICE"] = "NICE",
                    ["VERB"] = "VERB",
                    ["NEARLY"] = "NEARLY",
                    ["CREEK"] = "CREEK",
                    ["TRIBE"] = "TRIBE",
                    ["CYBER"] = "CYBER",
                    ["CINEMA"] = "CINEMA",
                    ["KOALA"] = "KOALA",
                    ["WATER"] = "WATER",
                    ["WHISK"] = "WHISK",
                    ["MATTER"] = "MATTER",
                    ["KEYS"] = "KEYS",
                    ["STUCK"] = "STUCK",
                },
            },

            // The Wire
            // What was the color of the {1} dial in {0}?
            // What was the color of the top dial in The Wire?
            [Question.WireDialColors] = new TranslationInfo
            {
                QuestionText = "{0}の{1}の位置にあったダイヤルの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "青",
                    ["green"] = "緑",
                    ["grey"] = "灰",
                    ["orange"] = "オレンジ",
                    ["purple"] = "紫",
                    ["red"] = "赤",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "上",
                    ["bottom-left"] = "左下",
                    ["bottom-right"] = "右下",
                },
            },
            // What was the displayed number in {0}?
            // What was the displayed number in The Wire?
            [Question.WireDisplayedNumber] = new TranslationInfo
            {
                QuestionText = "What was the displayed number in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "上",
                    ["bottom-left"] = "左下",
                    ["bottom-right"] = "右下",
                },
            },

            // Wire Ordering
            // What color was the {1} display from the left in {0}?
            // What color was the first display from the left in Wire Ordering?
            [Question.WireOrderingDisplayColor] = new TranslationInfo
            {
                QuestionText = "{0}の左から{1}番目のディスプレイの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["orange"] = "オレンジ",
                    ["yellow"] = "黄",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["purple"] = "紫",
                    ["white"] = "白",
                    ["black"] = "黒",
                },
            },
            // What number was on the {1} display from the left in {0}?
            // What number was on the first display from the left in Wire Ordering?
            [Question.WireOrderingDisplayNumber] = new TranslationInfo
            {
                QuestionText = "{0}の左から{1}番目のディスプレイの数字は？",
            },
            // What color was the {1} wire from the left in {0}?
            // What color was the first wire from the left in Wire Ordering?
            [Question.WireOrderingWireColor] = new TranslationInfo
            {
                QuestionText = "{0}の左から{1}番目のワイヤーの色は？",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["orange"] = "オレンジ",
                    ["yellow"] = "黄",
                    ["green"] = "緑",
                    ["blue"] = "青",
                    ["purple"] = "紫",
                    ["white"] = "白",
                    ["black"] = "黒",
                },
            },

            // Wire Sequence
            // How many {1} wires were there in {0}?
            // How many red wires were there in Wire Sequence?
            [Question.WireSequenceColorCount] = new TranslationInfo
            {
                QuestionText = "(0)の{1}色のワイヤーの総数は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["blue"] = "青",
                    ["black"] = "黒",
                },
            },

            // Wolf, Goat, and Cabbage
            // Which of these was {1} on {0}?
            // Which of these was present on Wolf, Goat, and Cabbage?
            [Question.WolfGoatAndCabbageAnimals] = new TranslationInfo
            {
                QuestionText = "{0}に{1}のはどれ？",
            },
            // What was the boat size in {0}?
            // What was the boat size in Wolf, Goat, and Cabbage?
            [Question.WolfGoatAndCabbageBoatSize] = new TranslationInfo
            {
                QuestionText = "{0}のボートのサイズは？",
            },

            // Working Title
            // What was the label shown in {0}?
            // What was the label shown in Working Title?
            [Question.WorkingTitleLabel] = new TranslationInfo
            {
                QuestionText = "{0}にて表示されたラベルは？",
            },

            // The Xenocryst
            // What was the color of the {1} flash in {0}?
            // What was the color of the first flash in The Xenocryst?
            [Question.Xenocryst] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の点滅の色は？",
            },

            // XmORse Code
            // What was the {1} displayed letter (in reading order) in {0}?
            // What was the first displayed letter (in reading order) in XmORse Code?
            [Question.XmORseCodeDisplayedLetters] = new TranslationInfo
            {
                QuestionText = "{0}で表示された単語の{1}番目の位置(読み順)にある英字は？",
            },
            // What word did you decrypt in {0}?
            // What word did you decrypt in XmORse Code?
            [Question.XmORseCodeWord] = new TranslationInfo
            {
                QuestionText = "{0}で解読した単語は？",
            },

            // xobekuJ ehT
            // What song was played on {0}?
            // What song was played on xobekuJ ehT?
            [Question.XobekuJehTSong] = new TranslationInfo
            {
                QuestionText = "What song was played on {0}?",
            },

            // Yahtzee
            // What was the initial roll on {0}?
            // What was the initial roll on Yahtzee?
            [Question.YahtzeeInitialRoll] = new TranslationInfo
            {
                QuestionText = "{0}の最初のロール後の状態は？",
                Answers = new Dictionary<string, string>
                {
                    ["Yahtzee"] = "ヤッツィー",
                    ["large straight"] = "大ストレート",
                    ["small straight"] = "小ストレート",
                    ["four of a kind"] = "フォーオブカインド",
                    ["full house"] = "フルハウス",
                    ["three of a kind"] = "スリーオブカインド",
                    ["two pairs"] = "ツーペア",
                    ["pair"] = "ワンペア",
                },
            },

            // Yellow Arrows
            // What was the starting row letter in {0}?
            // What was the starting row letter in Yellow Arrows?
            [Question.YellowArrowsStartingRow] = new TranslationInfo
            {
                QuestionText = "{0}の開始行の英字は？",
            },

            // The Yellow Button
            // What was the {1} color in {0}?
            // What was the first color in The Yellow Button?
            [Question.YellowButtonColors] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の色は？",
            },

            // Yellow Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Yellow Cipher?
            [Question.YellowCipherAnswer] = new TranslationInfo
            {
                QuestionText = "{0}の回答は？",
            },

            // Zero, Zero
            // What color was the {1} star in {0}?
            // What color was the top-left star in Zero, Zero?
            [Question.ZeroZeroStarColors] = new TranslationInfo
            {
                QuestionText = "{0}の{1}の位置の星の色は？",
                Answers = new Dictionary<string, string>
                {
                    ["black"] = "黒",
                    ["blue"] = "青",
                    ["green"] = "緑",
                    ["cyan"] = "シアン",
                    ["red"] = "赤",
                    ["magenta"] = "マゼンタ",
                    ["yellow"] = "黄",
                    ["white"] = "白",
                },
            },
            // How many points were on the {1} star in {0}?
            // How many points were on the top-left star in Zero, Zero?
            [Question.ZeroZeroStarPoints] = new TranslationInfo
            {
                QuestionText = "{0}の{1}の位置の星のポイントは？",
            },
            // Where was the {1} square in {0}?
            // Where was the red square in Zero, Zero?
            [Question.ZeroZeroSquares] = new TranslationInfo
            {
                QuestionText = "{0}の{1}色の正方形の場所は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "赤",
                    ["green"] = "緑",
                    ["blue"] = "青",
                },
            },

            // Zoni
            // What was the {1} word in {0}?
            // What was the first word in Zoni?
            [Question.ZoniWords] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に解読した単語は？",
            },

        };
        #endregion
    }
}
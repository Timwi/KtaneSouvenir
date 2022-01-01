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
            //"#"は日本語マニュアル未対応(プロファイルver1.29時点)
            //"#" indicates the Japanese manual is not supported as of ver. 1.29.

            // 1000単語
            // 1000単語の{1}番目の単語は何？
            // 1000単語の1番目の単語は何？
            [Question._1000WordsWords] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の単語は何？",
            },

            // #100 Levels of Defusal
            // What was the {1} displayed letter in {0}?
            // What was the first displayed letter in 100 Levels of Defusal?
            [Question._100LevelsOfDefusalLetters] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に表示された文字は何？",
            },
            // #1D Chess
            // {0}で{1}はどれだったか？
            // 1D Chessであなたの最初の移動はどれだったか？
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

            // 3D迷路
            // {0}のゴールの方向はどこ？
            // 3D迷路のゴールの方向はどこ？
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

            // #3D Tap Code
            // {0}で受信した単語は？
            // 3D Tap Codeで受信した単語は？
            [Question._3DTapCodeWord] = new TranslationInfo
            {
                QuestionText = "{0}で受信した単語は？",
            },

            // 3Dトンネル
            // What was the {1} goal node in {0}?
            // What was the first goal node in 3D Tunnels?
            [Question._3DTunnelsTargetNode] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のゴールの目印は何？",
            },

            // #3 LEDs
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

            // 9ボール
            // {0}のボール{1}の数字は？
            // 9ボールのボールAの数字は？
            [Question._9BallLetters] = new TranslationInfo
            {
                QuestionText = "{0}のボール{1}の数字は？",
            },
            // {0}のボール{1}の文字は？
            // 9ボールのボール2の文字は？
            [Question._9BallNumbers] = new TranslationInfo
            {
                QuestionText = "{0}のボール{1}の文字は？",
            },

            // 蓄積
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

            // アドベンチャーゲーム
            // {0}で{1}番目に正しく使用したアイテムはどれ？
            // アドベンチャーゲームで1番目に正しく使用したアイテムはどれ？
            [Question.AdventureGameCorrectItem] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に正しく使用したアイテムはどれ？",
            },

            // アドベンチャーゲーム
            // {0}でどの敵と戦ったか？
            // アドベンチャーゲームでどの敵と戦ったか？
            [Question.AdventureGameEnemy] = new TranslationInfo
            {
                QuestionText = "{0}でどの敵と戦ったか？",
            },

            // #Affine Cycle
            // What was the {1} in {0}?
            // What was the message in Affine Cycle?
            [Question.AffineCycleWord] = new TranslationInfo
            {
                QuestionText = "{0}のメッセージとは何だった？",
            },

            // #Alfa-Bravo
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

            // 方程式
            // {0}の最初の方程式は何？
            // 方程式の最初の方程式は何？
            [Question.AlgebraEquation1] = new TranslationInfo
            {
                QuestionText = "{0}の最初の方程式は何？",
            },
            // {0}の二番目の方程式は何？
            // 方程式の二番目の方程式は何？
            [Question.AlgebraEquation2] = new TranslationInfo
            {
                QuestionText = "{0}の二番目の方程式は何？",
            },

            // #Algorithmia
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

            // アルファベットルール
            // {0}のステージ{1}で表示された文字は何？
            // アルファベットルールのステージ1で表示された文字は何？
            [Question.AlphabeticalRulingLetter] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で表示された文字は何？",
            },
            // {0}のステージ{1}で表示された数字は何？
            // アルファベットルールのステージ1で表示された数字は何？
            [Question.AlphabeticalRulingNumber] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で表示された数字は何？",
            },

            // #Alphabet Tiles
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

            // #Alpha-Bits
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

            // #Arithmelogic
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

            // #ASCII Maze
            // What was the {1} character displayed on {0}?
            // What was the first character displayed on ASCII Maze?
            [Question.ASCIIMazeCharacters] = new TranslationInfo
            {
                QuestionText = "What was the {1} character displayed on {0}？",
            },

            // 正方形
            // {0}で一致した色は？
            // 正方形で一致した色は？
            [Question.ASquareIndexColors] = new TranslationInfo
            {
                QuestionText = "{0}で一致した色は？",
            },
            // {0}で{1}番目に送信した色は？
            // 正方形で1番目に送信した色は？
            [Question.ASquareCorrectColors] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に送信した色は？",
            },

            // "Bamboozled Again
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
                    ["BLACK TEXT？"] = "BLACK TEXT？",
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

            // #Bakery
            // Which menu item was present in {0}?
            // Which menu item was present in Bakery?
            [Question.BakeryItems] = new TranslationInfo
            {
                QuestionText = "Which menu item was present in {0}？",
            },

            // #Barcode Cipher
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

            // #Bartending
            // Which ingredient was in the {1} position on {0}?
            // Which ingredient was in the first position on Bartending?
            [Question.BartendingIngredients] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目の位置にあった材料は？",
            },

            // ビッグサークル
            // {0}の解法において{1}番目の色は？
            // ビッグサークルの解法において1番目の色は？
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

            // 二進法LED
            // {0}でどの数字の時に正しいワイヤーを切った？
            // 二進法LEDでどの数字の時に正しいワイヤーを切った？
            [Question.BinaryLEDsValue] = new TranslationInfo
            {
                QuestionText = "{0}でどの数字の時に正しいワイヤーを切った？",
            },

            // #Binary Shift
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

            // 二進数
            // {0}で表示された単語は？
            // 二進数で表示された単語は？
            [Question.BinaryWord] = new TranslationInfo
            {
                QuestionText = "{0}で表示された単語は？",
            },

            // ビットマップ
            // {0}で{2}区域の{1}ピクセル数は？?
            // ビットマップで左上区域の白ピクセル数は？
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

            // #Black Cipher
            // What was the answer in {0}?
            // What was the answer in Black Cipher?
            [Question.BlackCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // #Blind Maze
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

            // ブロックバスター
            // {0}で最後に押した文字は何？
            // ブロックバスターで最後に押した文字は何？
            [Question.BlockbustersLastLetter] = new TranslationInfo
            {
                QuestionText = "{0}で最後に押した文字は何？",
            },

            // 青色矢印
            // {0}でスクリーンに表示された文字は何？
            // 青色矢印でスクリーンに表示された文字は何？
            [Question.BlueArrowsInitialLetters] = new TranslationInfo
            {
                QuestionText = "{0}でスクリーンに表示された文字は何？",
            },

            // #The Blue Button
            // What was D in {0}?
            // What was D in The Blue Button?
            [Question.BlueButtonD] = new TranslationInfo
            {
                QuestionText = "What was D in {0}？",
            },
            // {0}でEはどれだったか？
            // The Blue ButtonでEはどれだったか？
            [Question.BlueButtonEFGH] = new TranslationInfo
            {
                QuestionText = "{0}でEはどれだったか？",
            },
            // {0}でMはどれだったか？
            // The Blue ButtonでMはどれだったか？
            [Question.BlueButtonM] = new TranslationInfo
            {
                QuestionText = "{0}でMはどれだったか？",
            },
            // {0}でNはどれだったか？
            // The Blue ButtonでNはどれだったか？
            [Question.BlueButtonN] = new TranslationInfo
            {
                QuestionText = "{0}でNはどれだったか？",
            },
            // {0}でPはどれだったか？
            // The Blue ButtonでPはどれだったか？
            [Question.BlueButtonP] = new TranslationInfo
            {
                QuestionText = "{0}でPはどれだったか？",
            },
            // {0}でQはどれだったか？
            // The Blue ButtonでQはどれだったか？
            [Question.BlueButtonQ] = new TranslationInfo
            {
                QuestionText = "{0}でQはどれだったか？",
            },
            // {0}でXはどれだったか？
            // The Blue ButtonでXはどれだったか？
            [Question.BlueButtonX] = new TranslationInfo
            {
                QuestionText = "{0}でXはどれだったか？",
            },

            // #Blue Cipher
            // What was the answer in {0}?
            // What was the answer in Blue Cipher?
            [Question.BlueCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // #Bob Barks
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

            // ボグル
            // {0}で初めに表示された文字は？
            // ボグルで初めに表示された文字は？
            [Question.BoggleLetters] = new TranslationInfo
            {
                QuestionText = "{0}で初めに表示された文字は？",
            },

            // #Boxing
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

            // 点字
            // {0}の答えの単語は何？
            // 点字の答えの単語は何？
            [Question.BrailleWord] = new TranslationInfo
            {
                QuestionText = "{0}の答えの単語は何？",
            },

            // #Breakfast Egg
            // Which color appeared on the egg in {0}?
            // Which color appeared on the egg in Breakfast Egg?
            [Question.BreakfastEggColor] = new TranslationInfo
            {
                QuestionText = "Which color appeared on the egg in {0}？",
            },

            // 壊れたボタン
            // {0}で{1}番目に押したボタンはどれ？
            // 壊れたボタンで1番目に押したボタンはどれ？
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

            // #Brown Cipher
            // What was the answer in {0}?
            // What was the answer in Brown Cipher?
            [Question.BrownCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // #Brush Strokes
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

            // ザ・電球
            // {0}のボタンの押し順はどれ？
            // ザ・電球のボタンの押し順はどれ？
            [Question.BulbButtonPresses] = new TranslationInfo
            {
                QuestionText = "{0}のボタンの押し順はどれ？",
            },

            // 盗難警報
            // {0}で{1}番目に表示された数字は何？
            // 盗難警報で1番目に表示された数字は何？
            [Question.BurglarAlarmDigits] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に表示された数字は何？",
            },

            // ボタン
            // {0}で光ったライトの色は？
            // ボタンで光ったライトの色は？
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

            // 順番ボタン
            // {0}内の{1}色のボタンはいくつ？
            // 順番ボタン内の赤色のボタンはいくつ？
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

            // カエサルサイクル
            // {0}の{1}は？
            // カエサルサイクルのメッセージは？
            [Question.CaesarCycleWord] = new TranslationInfo
            {
                QuestionText = "{0}の{1}は？",
            },

            // カレンダー
            // {0}のLEDの色は何？
            // カレンダーのLEDの色は何？
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

            // #Cartinese
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

            // #Challenge & Contact
            // What was the {1} submitted answer in {0}?
            // What was the first submitted answer in Challenge & Contact?
            [Question.ChallengeAndContactAnswers] = new TranslationInfo
            {
                QuestionText = "What was the {1} submitted answer in {0}？",
            },

            // #Cheap Checkout
            // What was the {1}paid amount in {0}?
            // What was the paid amount in Cheap Checkout?
            [Question.CheapCheckoutPaid] = new TranslationInfo
            {
                QuestionText = "What was the {1}paid amount in {0}？",
            },

            // #Cheep Checkout
            // Which bird {1} present in {0}?
            // Which bird was present in Cheep Checkout?
            [Question.CheepCheckoutBirds] = new TranslationInfo
            {
                QuestionText = "Which bird {1} present in {0}？",
            },

            // チェス
            // {0}の{1}番目の座標は何？
            // チェスの1番目のの座標は何？
            [Question.ChessCoordinate] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の座標は何？",
            },

            // 中国の数え方
            // {0}の{1}のLEDの色は何？
            // 中国の数え方の左のLEDの色は何？
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

            // コードクオリティー
            // {0}で与えられたコードの一部にある音は何？
            // コードクオリティーで与えられたコードの一部にある音は何？
            [Question.ChordQualitiesNotes] = new TranslationInfo
            {
                QuestionText = "{0}で与えられたコードの一部にある音は何？",
            },
            // {0}で与えられたコードクオリティーは何？
            // コードクオリティーで与えられたコードクオリティーは何？
            [Question.ChordQualitiesQuality] = new TranslationInfo
            {
                QuestionText = "{0}で与えられたコードクオリティーは何？",
            },

            // コード
            // {0}で表示された数字は何？
            // コードで表示された数字は何？
            [Question.CodeDisplayNumber] = new TranslationInfo
            {
                QuestionText = "{0}で表示された数字は何？",
            },

            // #Codenames
            // Which of these words was submitted in {0}?
            // Which of these words was submitted in Codenames?
            [Question.CodenamesAnswers] = new TranslationInfo
            {
                QuestionText = "Which of these words was submitted in {0}？",
            },

            // #Coffeebucks
            // What was the last served coffee in {0}?
            // What was the last served coffee in Coffeebucks?
            [Question.CoffeebucksCoffee] = new TranslationInfo
            {
                QuestionText = "What was the last served coffee in {0}？",
            },

            // #Coinage
            // Which coin was flipped in {0}?
            // Which coin was flipped in Coinage?
            [Question.CoinageFlip] = new TranslationInfo
            {
                QuestionText = "Which coin was flipped in {0}？",
            },

            // #Color Braille
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

            // 色の解読
            // {0}でステージ{1}のインジケーターのパターンは？
            // 色の解読でステージ1のインジケーターのパターンは？
            [Question.ColorDecodingIndicatorPattern] = new TranslationInfo
            {
                QuestionText = "{0}でステージ{1}のインジケーターのパターンは？",
            },
            // {0}でステージ{1}のインジケーターのパターンに{1}のは何色？
            // 色の解読でステージ1のインジケーターのパターンに現れたのは何色？
                Answers = new Dictionary<string, string>
                {

                    ["Green"] = "緑",
                    ["Purple"] = "紫",
                    ["Red"] = "赤",
                    ["Blue"] = "青",
                    ["Yellow"] = "黄",
                },
                FormatArgs = new Dictionary<string, string>
                {
                    ["appeared"] = "現れた",
                    ["did not appear"] = "現れなかった",
                },
            },

            // 色付きキーパッド
            // {0}で表示された単語は？
            // 色付きキーパッドで表示された単語は？
            [Question.ColoredKeysDisplayWord] = new TranslationInfo
            {
                QuestionText = "{0}で表示された単語は？",
            },
            // {0}で表示された単語の色は？
            // 色付きキーパッドで表示された単語の色は？
            [Question.ColoredKeysDisplayWordColor] = new TranslationInfo
            {
                QuestionText = "{0}で表示された単語の色は？",
            },
            // {0}の{1}のキーパッドの色は？
            // 色付きキーパッドの左上のキーパッドの色は？
            [Question.ColoredKeysKeyColor]o = new TranslationInfo
            {
                QuestionText = "What was the color of the {1} key in {0}？",
            },
            // {0}の{1}のキーパッドの文字は？
            // 色付きキーパッドの左上のキーパッドの文字は？
            [Question.ColoredKeysKeyLetter] = new TranslationInfo
            {
                QuestionText = "{0}の{1}のキーパッドの文字は？",
            },

            // 色付き格子
            // {0}の最初の色のグループは？
            // 色付き格子の最初の色のグループは？
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

            // 色付きスイッチ
            // {0}の初期配置は？
            // 色付きスイッチの初期配置は？
            [Question.ColoredSwitchesInitialPosition] = new TranslationInfo
            {
                QuestionText = "{0}の初期配置は？",
            },
            // {0}のLEDが示したスイッチの位置は？
            // 色付きスイッチのLEDが示したスイッチの位置は？
            [Question.ColoredSwitchesWhenLEDsCameOn] = new TranslationInfo
            {
                QuestionText = "{0}のLEDが示したスイッチの位置は？",
            },

            // カラーモールス
            // {0}の{1}番目のLEDの色は？
            // カラーモールスの1番目のLEDの色は？
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
            // {0}の{1}番目のLEDが示す文字は？
            // カラーモールスの1番目のLEDが示す文字は？
            [Question.ColorMorseCharacter] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のLEDが示す文字は？",
            },

            // #Colors Maximization
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

            // カラーフラッシュ
            // {0}のシーケンスの最後の単語は何色？
            // カラーフラッシュのシーケンスの最後の単語は何色？
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

            // 座標
            //{0}で最初に選んだ解法は？
            // 座標で最初に選んだ解法は？
            [Question.CoordinatesFirstSolution] = new TranslationInfo
            {
                QuestionText = "{0}で最初に選んだ解法は？",
            },
            // {0}のグリッドのサイズは？
            // 座標のグリッドのサイズは？
            [Question.CoordinatesSize] = new TranslationInfo
            {
                QuestionText = "{0}のグリッドのサイズは？",
            },

            // コーナー
            // {0}の{1}の角は何色？
            // コーナーの左上の角は何色？
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
            // {0}の{1}色の角はいくつ？
            // コーナーの赤色の角はいくつ？
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

            // #Cosmic
            // What was the number initially shown in {0}?
            // What was the number initially shown in Cosmic?
            [Question.CosmicNumber] = new TranslationInfo
            {
                QuestionText = "What was the number initially shown in {0}？",
            },

            // #Creation
            // What were the weather conditions on the {1} day in {0}?
            // What were the weather conditions on the first day in Creation?
            [Question.CreationWeather] = new TranslationInfo
            {
                QuestionText = "What were the weather conditions on the {1} day in {0}？",
            },

            // #Critters
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

            // #Cryptic Cycle
            // What was the {1} in {0}?
            // What was the message in Cryptic Cycle?
            [Question.CrypticCycleWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} in {0}？",
            },

            // 暗号キーパッド
            // {0}で{1}のキーパッドのラベルは？
            // 暗号キーパッドで左上のキーパッドのラベルは？
            [Question.CrypticKeypadLabels] = new TranslationInfo
            {
                QuestionText = "{0}で{1}のキーパッドのラベルは？",
            },
            // {0}で{1}のキーパッドの回転方向は？
            // 暗号キーパッドで左上のキーパッドの回転方向は？
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

            // #The Cube
            // What was the {1} cube rotation in {0}?
            // What was the first cube rotation in The Cube?
            [Question.CubeRotations] = new TranslationInfo
            {
                QuestionText = "What was the {1} cube rotation in {0}？",
            },

            // #The Cyan Button
            // Where was the button at the {1} stage in {0}?
            // Where was the button at the first stage in The Cyan Button?
            [Question.CyanButtonPositions] = new TranslationInfo
            {
                QuestionText = "Where was the button at the {1} stage in {0}？",
            },

            // DACH迷路
            // {0}の出発点は？
            // DACH迷路の出発点は？
            [Question.DACHMazeOrigin] = new TranslationInfo
            {
                QuestionText = "{0}の出発点は？",
            },

            // デフ・アレイ
            // {0}で生成された文字は？
            // デフ・アレイで生成された文字は？
            [Question.DeafAlleyShape] = new TranslationInfo
            {
                QuestionText = "{0}で生成された文字は？",
            },

            // #The Deck of Many Things
            // What deck did the first card of {0} belong to?
            // What deck did the first card of The Deck of Many Things belong to?
            [Question.DeckOfManyThingsFirstCard] = new TranslationInfo
            {
                QuestionText = "What deck did the first card of {0} belong to？",
            },

            // 色抜き格子
            // {0}で開始した{1}に該当する色は？
            // 色抜き格子で開始した列に該当する色は？
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

            // #Devilish Eggs
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

            // #Digisibility
            // What was the number on the {1} button in {0}?
            // What was the number on the first button in Digisibility?
            [Question.DigisibilityDisplayedNumber] = new TranslationInfo
            {
                QuestionText = "What was the number on the {1} button in {0}？",
            },

            // 色変え格子
            // {0}で{1}の覚えた位置は？
            // 色変え格子で青の覚えた位置は？
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

            // 割り切れる数字
            // {0}で押した正しいボタンはどれ？
            // 割り切れる数字で押した正しいボタンはどれ？
            [Question.DivisibleNumbersAnswers] = new TranslationInfo
            {
                QuestionText = "{0}で押した正しいボタンはどれ？？",
            },
            // {0}でのステージ{1}の数字は？
            // 割り切れる数字でのステージ{1}の数字は？
            [Question.DivisibleNumbersNumbers] = new TranslationInfo
            {
                QuestionText = "{0}でのステージ{1}の数字は？",
            },

            // 二色
            // {0}でのステージ{1}の画面の色は？
            // 二色でのステージ1の画面の色は？
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

            // 二桁
            // {0}の{1}画面上の数字は？
            // 二桁の左画面上の数字は？
            [Question.DoubleDigitsDisplays] = new TranslationInfo
            {
                QuestionText = "{0}の{1}画面上の数字は？",
            },

            // ダブル・オー
            // {0}の送信ボタンは？
            // ダブル・オーの送信ボタンは？
            [Question.DoubleOhSubmitButton] = new TranslationInfo
            {
                QuestionText = "{0}の送信ボタンは？",
            },

            // #Dr. Doctor
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

            // #Dreamcipher
            // What was the decrypted word in {0}?
            // What was the decrypted word in Dreamcipher?
            [Question.DreamcipherWord] = new TranslationInfo
            {
                QuestionText = "What was the decrypted word in {0}？",
            },

            // #Dumb Waiters
            // Which player {1} present in {0}?
            // Which player was present in Dumb Waiters?
            [Question.DumbWaitersPlayerAvailable] = new TranslationInfo
            {
                QuestionText = "Which player {1} present in {0}？",
            },

            // ーピ・グンリペス
            // {0}において綴りを尋ねられた単語は？
            // ーピ・グンリペスにおいて綴りを尋ねられた単語は？
            [Question.eeBgnillepSWord] = new TranslationInfo
            {
                QuestionText = "{0}において綴りを尋ねられた単語は？",
            },

            // #Eight
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

            // #Elder Futhark
            // What was the {1} rune shown on {0}?
            // What was the first rune shown on Elder Futhark?
            [Question.ElderFutharkRunes] = new TranslationInfo
            {
                QuestionText = "What was the {1} rune shown on {0}？",
            },

            // #Encrypted Equations
            // Which shape was the {1} operand in {0}?
            // Which shape was the first operand in Encrypted Equations?
            [Question.EncryptedEquationsShapes] = new TranslationInfo
            {
                QuestionText = "Which shape was the {1} operand in {0}？",
            },

            // #Encrypted Hangman
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

            // #Encrypted Maze
            // Which symbol on {0} was spinning {1}?
            // Which symbol on Encrypted Maze was spinning clockwise?
            [Question.EncryptedMazeSymbols] = new TranslationInfo
            {
                QuestionText = "Which symbol on {0} was spinning {1}？",
            },

            // #Encrypted Morse
            // What was the {1} on {0}?
            // What was the received call on Encrypted Morse?
            [Question.EncryptedMorseCallResponse] = new TranslationInfo
            {
                QuestionText = "What was the {1} on {0}？",
            },

            // #Encryption Bingo
            // What was the first encoding used in {0}?
            // What was the first encoding used in Encryption Bingo?
            [Question.EncryptionBingoEncoding] = new TranslationInfo
            {
                QuestionText = "What was the first encoding used in {0}？",
            },

            // #Entry Number Four
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

            // #Entry Number One
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

            // #Equations X
            // What was the displayed symbol in {0}?
            // What was the displayed symbol in Equations X?
            [Question.EquationsXSymbols] = new TranslationInfo
            {
                QuestionText = "What was the displayed symbol in {0}？",
            },

            // エテルナ
            // {0}の下から{1}番目の矢印のビートは？
            // エテルナの下から1番目の矢印のビートは？
            [Question.EtternaNumber] = new TranslationInfo
            {
                QuestionText = "{0}の下から{1}番目の矢印のビートは？",
            },

            // #Exoplanets
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

            // 因数迷路
            // {0}で選ばれた素因数の一つにあるのはどれ？
            // 因数迷路で選ばれた素因数の一つにあるのはどれ？
            [Question.FactoringMazeChosenPrimes] = new TranslationInfo
            {
                QuestionText = "{0}で選ばれた素因数の一つにあるのはどれ？",
            },

            // #Factory Maze
            // What room did you start in in {0}?
            // What room did you start in in Factory Maze?
            [Question.FactoryMazeStartRoom] = new TranslationInfo
            {
                QuestionText = "What room did you start in in {0}？",
            },

            // 速算
            // {0}の最後の英字のペアは？
            // 速算の最後の英字のペアは？
            [Question.FastMathLastLetters] = new TranslationInfo
            {
                QuestionText = "{0}の最後の英字のペアは？",
            },

            // #Faulty RGB Maze
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

            // 国旗
            // {0}で表示された数字は？
            // 国旗で表示された数字は？
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

            // #Flashing Arrows
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

            // 点滅ライト
            // {0}で{1}のLEDは{2}色に何回光った？
            // 点滅ライトで上のLEDは青色に何回光った？
            [Question.FlashingLightsLEDFrequency] = new TranslationInfo
            {
                QuestionText = "{0}で{1}のLEDは{2}色に何回光った？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "上",
                    ["blue"] = "青",
                    ["green"] = "緑",
                    ["red"] = "赤",
                    ["purple"] = "紫",
                    ["orange"] = "オレンジ",
                    ["bottom"] = "下",
                },
            },

            // #Flyswatting
            // Which fly was present, but not in the solution in {0}?
            // Which fly was present, but not in the solution in Flyswatting?
            [Question.FlyswattingUnpressed] = new TranslationInfo
            {
                QuestionText = "Which fly was present, but not in the solution in {0}？",
            },

            // #Forget Any Color
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

            // #Forget Me
            // What number was in the {1} position of the initial puzzle in {0}?
            // What number was in the top-left position of the initial puzzle in Forget Me?
            [Question.ForgetMeInitialState] = new TranslationInfo
            {
                QuestionText = "What number was in the {1} position of the initial puzzle in {0}？",
            },

            // #Forget’s Ultimate Showdown
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

            // #Forget the Colors
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

            // #Free Parking
            // What was the player token in {0}?
            // What was the player token in Free Parking?
            [Question.FreeParkingToken] = new TranslationInfo
            {
                QuestionText = "What was the player token in {0}？",
            },

            // #Functions
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

            // #Game of Life Cruel
            // Which of these was a color combination that occurred in {0}?
            // Which of these was a color combination that occurred in Game of Life Cruel?
            [Question.GameOfLifeCruelColors] = new TranslationInfo
            {
                QuestionText = "Which of these was a color combination that occurred in {0}？",
            },

            // ゲームパッド
            // {0}の数字は？
            // ゲームパッドの数字は？
            [Question.GamepadNumbers] = new TranslationInfo
            {
                QuestionText = "{0}の数字は？",
            },

            // #The Glitched Button
            // What was the cycling bit sequence in {0}?
            // What was the cycling bit sequence in The Glitched Button?
            [Question.GlitchedButtonSequence] = new TranslationInfo
            {
                QuestionText = "What was the cycling bit sequence in {0}？",
            },

            // #he Gray Button
            // What was the {1} coordinate on the display in {0}?
            // What was the horizontal coordinate on the display in The Gray Button?
            [Question.GrayButtonCoordinates] = new TranslationInfo
            {
                QuestionText = "What was the {1} coordinate on the display in {0}？",
            },

            // #Gray Cipher
            // What was the answer in {0}?
            // What was the answer in Gray Cipher?
            [Question.GrayCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // #The Great Void
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

            // 緑色矢印
            // What was the last number on the display on {0}?
            // What was the last number on the display on Green Arrows?
            [Question.GreenArrowsLastScreen] = new TranslationInfo
            {
                QuestionText = "What was the last number on the display on {0}？",
            },

            // #The Green Button
            // What was the word submitted in {0}?
            // What was the word submitted in The Green Button?
            [Question.GreenButtonWord] = new TranslationInfo
            {
                QuestionText = "What was the word submitted in {0}？",
            },

            // #Green Cipher
            // What was the answer in {0}?
            // What was the answer in Green Cipher?
            [Question.GreenCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // グリッドロック
            // {0}の開始位置は？
            // グリッドロックの開始位置は？
            [Question.GridLockStartingLocation] = new TranslationInfo
            {
                QuestionText = "{0}の開始位置は？",
            },
            // {0}の終了位置は？
            // グリッドロックの終了位置は？
            [Question.GridLockEndingLocation] = new TranslationInfo
            {
                QuestionText = "{0}の終了位置は？",
            },
            // {0}の開始地点は何色？
            // グリッドロックの開始地点は何色？
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

            // 食料品店
            // {0}で最初に表示された商品は？
            // 食料品店で最初に表示された商品は？
            [Question.GroceryStoreFirstItem] = new TranslationInfo
            {
                QuestionText = "{0}で最初に表示された商品は？",
            },

            // #Gryphons
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

            // #Guess Who?
            // Who was the person recalled in {0}?
            // Who was the person recalled in Guess Who??
            [Question.GuessWhoPerson] = new TranslationInfo
            {
                QuestionText = "Who was the person recalled in {0}？",
            },

            // #Hereditary Base Notation
            // What was the given number in {0}?
            // What was the given number in Hereditary Base Notation?
            [Question.HereditaryBaseNotationInitialNumber] = new TranslationInfo
            {
                QuestionText = "What was the given number in {0}？",
            },

            // #The Hexabutton
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

            // 六角迷路
            // {0}のコマの色は？
            // 六角迷路のコマの色は？
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

            // #hexOS
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

            // #Hidden Colors
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

            // #Hill Cycle
            // What was the {1} in {0}?
            // What was the message in Hill Cycle?
            [Question.HillCycleWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} in {0}？",
            },

            // #Hogwarts
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

            // #Hold Ups
            // What was the name of the {1} shadow shown in {0}?
            // What was the name of the first shadow shown in Hold Ups?
            [Question.HoldUpsShadows] = new TranslationInfo
            {
                QuestionText = "What was the name of the {1} shadow shown in {0}？",
            },

            // #Horrible Memory
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

            // #Homophones
            // What was the {1} displayed phrase in {0}?
            // What was the first displayed phrase in Homophones?
            [Question.HomophonesDisplayedPhrases] = new TranslationInfo
            {
                QuestionText = "What was the {1} displayed phrase in {0}？",
            },

            // #Human Resources
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

            // 狩猟
            // {0}の最初3つのステージのうち、{2}番目の{1}シンボルを持っていたのはどれ？
            // 狩猟の最初3つのステージのうち、1番目の列シンボルを持っていたのはどれ？
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

            // 超立方体
            // {0}の{1}番目の回転方向は？
            // 超立方体の1番目の回転方向は？
            [Question.HypercubeRotations] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の回転方向は？",
            },

            // #The Hyperlink
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

            // アイスクリーム
            // {0}で{2}番目の客が{1}商品の一つにあるのは？
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
            // {0}の{1}番目の客は？
            // アイスクリームの1番目の客は？
            [Question.IceCreamCustomer] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の客は？",
            },

            // #Identification Crisis
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

            // 容疑者特定
            // {0}のリストに{1}のはどの髪色？
            // 容疑者特定のリストにあるのはどの髪色？
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
                 FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "ある",
                    ["was not"] = "ない",
                },
            },
            // {0}のリストに{1}のはどの身体的特徴？
            // 容疑者特定のリストにあるのはどの身体的特徴？
            [Question.IdentityParadeBuilds] = new TranslationInfo
            {
                QuestionText = "{0}のリストに{1}のはどの身体的特徴？",
            },
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "ある",
                    ["was not"] = "ない",
                },
            // {0}のリストに{1}のはどの服装？
            // 容疑者特定のリストにあるのはどの服装？
            [Question.IdentityParadeAttires] = new TranslationInfo
            {
                QuestionText = "{0}のリストに{1}のはどの服装？",

            },  
                 FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "ある",
                    ["was not"] = "ない",
                },


            // #Indigo Cipher
            // What was the answer in {0}?
            // What was the answer in Indigo Cipher?
            [Question.IndigoCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // #Infinite Loop
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

            // #Ingredients
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

            // #Inner Connections
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

            // #Interpunct
            // What was the symbol displayed in stage {1} of {0}?
            // What was the symbol displayed in stage 1 of Interpunct?
            [Question.InterpunctDisplay] = new TranslationInfo
            {
                QuestionText = "What was the symbol displayed in stage {1} of {0}？",
            },

            // #IPA
            // What symbol was the correct answer in {0}?
            // What symbol was the correct answer in IPA?
            [Question.IpaSymbol] = new TranslationInfo
            {
                QuestionText = "What symbol was the correct answer in {0}？",
            },

            // アイフォン
            // {0}の{1}番目のPINの数字は？
            // アイフォンの1番目のPINの数字は？
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

            // #The Jewel Vault
            // What number was wheel {1} in {0}?
            // What number was wheel A in The Jewel Vault?
            [Question.JewelVaultWheels] = new TranslationInfo
            {
                QuestionText = "What number was wheel {1} in {0}？",
            },

            // #Jumble Cycle
            // What was the {1} in {0}?
            // What was the message in Jumble Cycle?
            [Question.JumbleCycleWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} in {0}？",
            },

            // #The Kanye Encounter
            // What was a food item displayed in {0}?
            // What was a food item displayed in The Kanye Encounter?
            [Question.KanyeEncounterFoods] = new TranslationInfo
            {
                QuestionText = "What was a food item displayed in {0}？",
            },

            // #Keypad Magnified
            // What was the position of the LED in {0}?
            // What was the position of the LED in Keypad Magnified?
            [Question.KeypadMagnifiedLED] = new TranslationInfo
            {
                QuestionText = "What was the position of the LED in {0}？",
            },

            // クド数独
            // {0}で最初に{1}四角はどれ？
            // クド数独で最初に埋められていた四角はどれ？
            [Question.KudosudokuPrefilled] = new TranslationInfo
            {
                QuestionText = "{0}で最初に{1}四角はどれ？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["pre-filled"] = "埋められていた",
                    ["not pre-filled"] = "埋められていなかった",
                },
            },

            // #Ladders
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

            // #Lasers
            // What was the number on the {1} hatch on {0}?
            // What was the number on the top-left hatch on Lasers?
            [Question.LasersHatches] = new TranslationInfo
            {
                QuestionText = "What was the number on the {1} hatch on {0}？",
            },

            // 暗号化LED
            // {0}のステージ{1}で押した正しい文字は？
            // 暗号化LEDのステージ1で押した正しい文字は？
            [Question.LEDEncryptionPressedLetters] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で押した正しい文字は？",
            },

            // #LED Math
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

            // #LEDs
            // What was the initial color of the changed LED in {0}?
            // What was the initial color of the changed LED in LEDs?
            [Question.LEDsOriginalColor] = new TranslationInfo
            {
                QuestionText = "What was the initial color of the changed LED in {0}？",
            },

            // #LEGOs
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

            // #Letter Math
            // What was the letter on the {1} display in {0}?
            // What was the letter on the left display in Letter Math?
            [Question.LetterMathDisplay] = new TranslationInfo
            {
                QuestionText = "What was the letter on the {1} display in {0}？",
            },

            // #Linq
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

            // #Lion’s Share
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

            // リスニング
            // {0}で入力した正しいコードは？
            // リスニングで入力した正しいコードは？
            [Question.ListeningCode] = new TranslationInfo
            {
                QuestionText = "{0}で入力した正しいコードは？",
            },

            // #Logical Buttons
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

            // 論理ゲート
            // {0}で{1}はどれだったか？
            // 論理ゲートでゲートAはどれだったか？
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

            // #Lombax Cubes
            // What was the {1} letter on the button in {0}?
            // What was the first letter on the button in Lombax Cubes?
            [Question.LombaxCubesLetters] = new TranslationInfo
            {
                QuestionText = "What was the {1} letter on the button in {0}？",
            },

            // #The London Underground
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

            // #Mahjong
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

            // #Mafia
            // Who was a player, but not the Godfather, in {0}?
            // Who was a player, but not the Godfather, in Mafia?
            [Question.MafiaPlayers] = new TranslationInfo
            {
                QuestionText = "Who was a player, but not the Godfather, in {0}？",
            },

            // #M&Ms
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

            // #M&Ns
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

            // #Maritime Flags
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

            // 連打算数
            // {0}の解は？
            // 連打算数の解は？
            [Question.MashematicsAnswer] = new TranslationInfo
            {
                QuestionText = "{0}の解は？",
            },
            // {0}の問題内にあった{1}番目の数字は？
            // 連打算数の問題内にあった1番目の数字は？
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
            
            // #The Matrix
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

            // 迷路
            // {0}のスタート地点の{1}は{2}から何番目？
            // 迷路のスタート地点の列は、左から何番目？
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

            // #Maze³
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

            // #Maze Identification
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

            // #Mazematics
            // Which was the {1} value in {0}?
            // Which was the initial value in Mazematics?
            [Question.MazematicsValue] = new TranslationInfo
            {
                QuestionText = "Which was the {1} value in {0}？",
            },

            // 迷路スクランブラー
            // {0}のスタート位置は？
            // 迷路スクランブラーのスタート位置は？
            [Question.MazeScramblerStart] = new TranslationInfo
            {
                QuestionText = "{0}の開始位置は？",
            },
            // {0}のゴール位置は？
            // 迷路スクランブラーのゴール位置は？
            [Question.MazeScramblerGoal] = new TranslationInfo
            {
                QuestionText = "{0}のゴール位置は？",
            },
            // {0}の迷路を求めるマークの位置はどれ？
            // 迷路スクランブラーの迷路を求めるマークの位置はどれ？
            [Question.MazeScramblerIndicators] = new TranslationInfo
            {
                QuestionText = "{0}の迷路を求めるマークの位置はどれ？",
            },

            // #Mazeseeker
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

            // #Mega Man 2
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

            // #Melody Sequencer
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

            // #Memorable Buttons
            // What was the {1} correct symbol pressed in {0}?
            // What was the first correct symbol pressed in Memorable Buttons?
            [Question.MemorableButtonsSymbols] = new TranslationInfo
            {
                QuestionText = "What was the {1} correct symbol pressed in {0}？",
            },

            // 記憶
            // {0}のステージ{1}で表示された数は？
            // 記憶のステージ1で表示された数は？
            [Question.MemoryDisplay] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で表示された数は？",
            },
            // {0}のステージ{1}で押したボタンの位置は？
            // 記憶のステージ1で押したボタンの位置は？
            [Question.MemoryPosition] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で押したボタンの位置は？",
            },
            // {0}のステージ{1}で押したボタンのラベルは？
            // 記憶のステージ1で押したボタンのラベルは？
            [Question.MemoryLabel] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で押したボタンのラベルは？",
            },

            // #Metamorse
            // What was the extracted letter in {0}?
            // What was the extracted letter in Metamorse?
            [Question.MetamorseExtractedLetter] = new TranslationInfo
            {
                QuestionText = "What was the extracted letter in {0}？",
            },

            // マイクロコントローラー
            // {0}で{1}番目に点灯したピンは？
            // マイクロコントローラーで1番目に点灯したピンは？
            [Question.MicrocontrollerPinOrder] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に点灯したピンは？",
            },

            // マインスイーパー
            // {0}の開始のマスは何色？
            // マインスイーパーの開始のマスは何色？
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

            // #Mirror
            // What was the second word written by the original ghost in {0}?
            // What was the second word written by the original ghost in Mirror?
            [Question.MirrorWord] = new TranslationInfo
            {
                QuestionText = "What was the second word written by the original ghost in {0}？",
            },

            // #Mister Softee
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

            // 現代暗号
            // {0}のステージ{1}で復号された単語は？
            // 現代暗号のステージ1で復号された単語は？
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

            // #Module Listening
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

            // #Module Maze
            // Which of the following was the starting icon for {0}?
            // Which of the following was the starting icon for Module Maze?
            [Question.ModuleMazeStartingIcon] = new TranslationInfo
            {
                QuestionText = "Which of the following was the starting icon for {0}？",
            },

            // #Monsplode, Fight!
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

            // #Monsplode Trading Cards
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

            // #The Moon
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

            // #More Code
            // What was the flashing word in {0}?
            // What was the flashing word in More Code?
            [Question.MoreCodeWord] = new TranslationInfo
            {
                QuestionText = "What was the flashing word in {0}？",
            },

            // #Morse-A-Maze
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

            // モールスボタン
            // {0}の{1}番目のボタンで点滅した文字は？
            // モールスボタンの1番目のボタンで点滅した文字は？
            [Question.MorseButtonsButtonLabel] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のボタンで点滅した文字は？",
            },
            // {0}の{1}番目のボタンで点滅した色は？
            // モールスボタンの1番目のボタンで点滅した色は？
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

            // モールスマティック
            // {0}で{1}番目に受信した文字は？
            // モールスマティックで1番目に受信した文字は？
            [Question.MorsematicsReceivedLetters] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に受信した文字は？",
            },

            // モールス戦争
            // {0}で{1}段のLEDの状態は(1=オン、0=オフ)？
            // モールス戦争で下段のLEDの状態は(1=オン、0=オフ)？
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
            // {0}で変換した符号は？
            // モールス戦争で変換した符号は？
            [Question.MorseWarCode] = new TranslationInfo
            {
                QuestionText = "0}で変換した符号は？",
            },

            // #Mouse in the Maze
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

            // #M-Seq
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

            // 色どりスイッチ
            // {0}で小さなLEDが{3}時の{2}段LEDの{1}番目は？
            // 色どりスイッチで小さなLEDが点灯した時の上段LEDの1番目は？
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
                    ["bottom"] = "下",
                    ["lit"] = "点灯した",
                    ["unlit"] = "点灯していない",
                },
            },

            // 殺人
            // {0}の死体はどこで見つかった？
            // 殺人の死体はどこで見つかった？
            [Question.MurderBodyFound] = new TranslationInfo
            {
                QuestionText = "{0}の死体はどこで見つかった？",
            },
            // {0}で{1}人物は？
            // 殺人で容疑者だが殺人犯ではない人物は？
            [Question.MurderSuspect] = new TranslationInfo
            {
                QuestionText = "Which of these was {1} in {0}？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["a suspect but not the murderer"] = "容疑者だが殺人犯ではない",
                    ["not a suspect"]="容疑者ではない",
                },
            },
            // {0}で{1}のは？
            // 殺人で武器になり得るが凶器ではないのは？
            [Question.MurderWeapon] = new TranslationInfo
            {
                QuestionText = "Which of these was {1} in {0}？",
               FormatArgs = new Dictionary<string, string>
                {
                    ["a potential weapon but not the murder weapon"] = "武器になり得るが凶器ではない",
                    ["not a potential weapon"]="リストから除外されている",
                },
 
            },

            // ミステリーモジュール
            // {0}で最初に解除するように指示されたモジュールは？
            // ミステリーモジュールで最初に解除するように指示されたモジュールは？
            [Question.MysteryModuleFirstKey] = new TranslationInfo
            {
                QuestionText = " {0}で最初に解除するように指示されたモジュールは？",
            },
            // {0}で隠されていたモジュールは？
            // ミステリーモジュールで隠されていたモジュールは？
            [Question.MysteryModuleHiddenModule] = new TranslationInfo
            {
                QuestionText = "{0}で隠されていたモジュールは？",
            },

            // 神秘スクエア
            // {0}のどくろの位置は？
            // 神秘スクエアのどくろの位置は？
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

            // The Necronomicon
            // What was the chapter number of the {1} page in {0}?
            // What was the chapter number of the first page in The Necronomicon?
            [Question.NecronomiconChapters] = new TranslationInfo
            {
                QuestionText = "What was the chapter number of the {1} page in {0}？",
            },

            // #Negativity
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

            // #Neutralization
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

            // #N&Ms
            // What was the label of the correct button in {0}?
            // What was the label of the correct button in N&Ms?
            [Question.NandMsAnswer] = new TranslationInfo
            {
                QuestionText = "What was the label of the correct button in {0}？",
            },

            // #Name Codes
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

            // #Navinums
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

            // #The Navy Button
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

            // #Not Connection Check
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

            // #Not Coordinates
            // Which coordinate was part of the square in {0}?
            // Which coordinate was part of the square in Not Coordinates?
            [Question.NotCoordinatesSquareCoords] = new TranslationInfo
            {
                QuestionText = "Which coordinate was part of the square in {0}？",
            },
            // What was the function of the {1} button on an {2} digit in {0}?
            // What was the function of the left button on an odd digit in Not Coordinates?
            [Question.NotCoordinatesButtonFuncs] = new TranslationInfo
            {
                QuestionText = "What was the function of the {1} button on an {2} digit in {0}？",
            },

            // #Not Keypad
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

            // #Not Maze
            // What was the starting distance in {0}?
            // What was the starting distance in Not Maze?
            [Question.NotMazeStartingDistance] = new TranslationInfo
            {
                QuestionText = "What was the starting distance in {0}？",
            },

            // #Not Morse Code
            // What was the {1} correct word you submitted in {0}?
            // What was the first correct word you submitted in Not Morse Code?
            [Question.NotMorseCodeWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} correct word you submitted in {0}？",
            },

            // #Not Morsematics
            // What was the transmitted word on {0}?
            // What was the transmitted word on Not Morsematics?
            [Question.NotMorsematicsWord] = new TranslationInfo
            {
                QuestionText = "What was the transmitted word on {0}？",
            },

            // #Not Murder
            // What room was {1} in during {2} on {0}?
            // What room was Miss Scarlett in during the initial state on Not Murder?
            [Question.NotMurderRoom] = new TranslationInfo
            {
                QuestionText = "What room was {1} in during {2} on {0}？",
            },
            // What weapon did {1} possess during {2} on {0}?
            // What weapon did Miss Scarlett possess during the initial state on Not Murder?
            [Question.NotMurderWeapon] = new TranslationInfo
            {
                QuestionText = "What weapon did {1} possess during {2} on {0}？",
            },

            // #Not Number Pad
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

            // #Not Piano Keys
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

            // #Not Simaze
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

            // #Not Text Field
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

            // #Not The Bulb
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

            // #Not the Button
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

            // #Not the Screw
            // What was the initial position in {0}?
            // What was the initial position in Not the Screw?
            [Question.NotTheScrewInitialPosition] = new TranslationInfo
            {
                QuestionText = "What was the initial position in {0}？",
            },

            // #Not Who’s on First
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
                    ["WHAT？"] = "WHAT？",
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

            // #Not Word Search
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

            // #Not X01
            // Which sector value {1} present on {0}?
            // Which sector value was present on Not X01?
            [Question.NotX01SectorValues] = new TranslationInfo
            {
                QuestionText = "Which sector value {1} present on {0}？",
            },

            // #Not X-Ray
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

            // #Numbered Buttons
            // Which number was correctly pressed on {0}?
            // Which number was correctly pressed on Numbered Buttons?
            [Question.NumberedButtonsButtons] = new TranslationInfo
            {
                QuestionText = "Which number was correctly pressed on {0}？",
            },

            // ナンバー
            // {0}で与えられた二桁の数字は？
            // ナンバーで与えられた二桁の数字は？
            [Question.NumbersTwoDigit] = new TranslationInfo
            {
                QuestionText = "{0}で与えられた二桁の数字は？",
            },

            // #Numpath
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

            // #Object Shows
            // Which of these was a contestant on {0} but not the final winner?
            // Which of these was a contestant on Object Shows but not the final winner?
            [Question.ObjectShowsContestants] = new TranslationInfo
            {
                QuestionText = "Which of these was a contestant on {0} but not the final winner？",
            },

            // 9次元超立方体
            // {0}のスタートボールは？
            // 9次元超立方体のスタートボールは？
            [Question.OctadecayottonSphere] = new TranslationInfo
            {
                QuestionText = "{0}のスタートボールは？",
            },

            // {0}で1番目の回転の二次変形の一つであるのは？
            // 9次元超立方体で1番目の回転の二次変形の一つであるのは？
            [Question.OctadecayottonRotations] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目の回転の二次変形の一つであるのは？",
            },

            // #Odd One Out
            // What was the button you pressed in the {1} stage of {0}?
            // What was the button you pressed in the first stage of Odd One Out?
            [Question.OddOneOutButton] = new TranslationInfo
            {
                QuestionText = "What was the button you pressed in the {1} stage of {0}？",
            },

            // オンリーコネクト
            // {0}の{1}のヒエログリフは？
            // オンリーコネクトの左上のヒエログリフは？
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

            // 橙色矢印
            // {0}のステージ{2}における{1}番目の矢印は？
            // 橙色矢印のステージ1における1番目の矢印は？
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

            // #Orange Cipher
            // What was the answer in {0}?
            // What was the answer in Orange Cipher?
            [Question.OrangeCipherAnswer] = new TranslationInfo
            {
                QuestionText = "What was the answer in {0}？",
            },

            // #Ordered Keys
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

            // #Order Picking
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

            // 方向キューブ
            // {0}の最初の観測者の位置は？
            // 方向キューブの最初の観測者の位置は？
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

            // #Palindromes
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

            // #Partial Derivatives
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

            // #Passport Control
            // What was the passport expiration year of the {1} inspected passenger in {0}?
            // What was the passport expiration year of the first inspected passenger in Passport Control?
            [Question.PassportControlPassenger] = new TranslationInfo
            {
                QuestionText = "What was the passport expiration year of the {1} inspected passenger in {0}？",
            },

            // #Password Destroyer
            // What was the raw value when you solved {0}?
            // What was the raw value when you solved Password Destroyer?
            [Question.PasswordDestroyerRawValue] = new TranslationInfo
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

            // #Pattern Cube
            // Which symbol was highlighted in {0}?
            // Which symbol was highlighted in Pattern Cube?
            [Question.PatternCubeHighlightedSymbol] = new TranslationInfo
            {
                QuestionText = "Which symbol was highlighted in {0}？",
            },

            // 奥行きペグ
            // {0}の最初の色シーケンスで{1}番目の色は？
            // 奥行きペグの最初の色シーケンスで1番目の色は？
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

            // #Phosphorescence
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

            // パイ
            // {0}で{1}番目に表示された数字は？
            // パイで1番目に表示された数字は？
            [Question.PieDigits] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に表示された数字は？",
            },

            // #Pigpen Cycle
            // What was the {1} in {0}?
            // What was the message in Pigpen Cycle?
            [Question.PigpenCycleWord] = new TranslationInfo
            {
                QuestionText = "What was the {1} in {0}？",
            },

            // #The Pink Button
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

            // #Pixel Cipher
            // What was the keyword in {0}?
            // What was the keyword in Pixel Cipher?
            [Question.PixelCipherKeyword] = new TranslationInfo
            {
                QuestionText = "What was the keyword in {0}？",
            },

            // #Placeholder Talk
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

            // #Placement Roulette
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

            // #Planets
            // {0}には何の惑星が表示されていた？
            // Planetsには何の惑星が表示されていた？
            [Question.PlanetsPlanet] = new TranslationInfo
            {
                QuestionText = "{0}には何の惑星が表示されていた？",
            },
            // {0}の上から{1}番目のストリップの色は？
            // Planetsの上から1番目のストリップの色は？
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

            // #Playfair Cycle
            // {0}の{1}は？
            // Playfair Cycleのメッセージは？
            [Question.PlayfairCycleWord] = new TranslationInfo
            {
                QuestionText = "{0}の{1}は？",
            },

            // 詩
            // {0}において、{1}番目に押して正解だったフレーズは？
            // 詩において、1番目に押して正解だったフレーズは？
            [Question.PoetryAnswers] = new TranslationInfo
            {
                QuestionText = "{0}において、{1}番目に押して正解だったフレーズは？",
            },

            // #Polyhedral Maze
            // {0}の開始番号は？
            // Polyhedral Mazeの開始番号は？
            [Question.PolyhedralMazeStartPosition] = new TranslationInfo
            {
                QuestionText = "{0}の開始番号は？",
            },

            // 素数暗号
            // {0}に表示されていた数字は？
            // 素数暗号に表示されていた数字は？
            [Question.PrimeEncryptionDisplayedValue] = new TranslationInfo
            {
                QuestionText = "{0}に表示されていた数字は？",
            },

            // 回路接続
            // {0}において、{1}のワイヤーに含まれていない周波数は？
            // 回路接続において、赤白のワイヤーに含まれていない周波数は？
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

            // 紫色矢印
            // {0}のターゲット単語は？
            // 紫色矢印のターゲット単語は？
            [Question.PurpleArrowsFinish] = new TranslationInfo
            {
                QuestionText = "{0}のターゲット単語は？",
            },

            // #The Purple Button
            // {0}におけるサイクリックシークエンスの{1}番目の数字は？
            // The Purple Buttonにおけるサイクリックシークエンスの1番目の数字は？
            [Question.PurpleButtonNumbers] = new TranslationInfo
            {
                QuestionText = "{0}におけるサイクリックシークエンスの{1}番目の数字は？",
            },

            // #Puzzle Identification
            // {0}の{1}回目の数字は？
            // Puzzle Identificationの1回目の数字は？
            [Question.PuzzleIdentificationNum] = new TranslationInfo
            {
                QuestionText = "{0}の{1}回目の数字は？",
            },
            // {0}の{1}回目に使用されたゲームの種類は？
            // Puzzle Identificationの1回目に使用されたゲームの種類は？
            [Question.PuzzleIdentificationGame] = new TranslationInfo
            {
                QuestionText = "{0}の{1}回目に使用されたゲームの種類は？",
            },
            // {0}の{1}回目のパズル名は？
            // Puzzle Identificationの1回目のパズル名は？
            [Question.PuzzleIdentificationName] = new TranslationInfo
            {
                QuestionText = "{0}の{1}回目のパズル名は？",
            },

            // #Quaver
            // {0}の{1}番目のシークエンスの回答は？
            // Quaverの1番目のシークエンスの回答は？
            [Question.QuaverArrows] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のシークエンスの回答は？",
            },

            // #Quintuples
            // {0}の{2}番目のスロットの{1}番目の数字は？
            // Quintuplesの1番目のスロットの1番目の数字は？
            [Question.QuintuplesNumbers] = new TranslationInfo
            {
                QuestionText = "{0}の{2}番目のスロットの{1}番目の数字は？",
            },
            // {0}の{2}番目のスロットの{1}番目の数字の色は？
            // Quintuplesの1番目のスロットの1番目の数字の色は？
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

            // #Railway Cargo Loading
            // {0}の第{1}連結車両とは？
            // Railway Cargo Loadingの第2連結車両とは？
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

            // 虹色矢印
            // {0}のディスプレイの数字は？
            // 虹色矢印のディスプレイの数字は？
            [Question.RainbowArrowsNumber] = new TranslationInfo
            {
                QuestionText = "{0}のディスプレイの数字は？",
            },

            // 色変えスイッチ
            // {0}の{1}番目の位置にあるLEDの色は？
            // 色変えスイッチの1番目の位置にあるLEDの色は？
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

            // 赤色矢印
            // {0}の開始地点の数字は？
            // 赤色矢印の開始地点の数字は？
            [Question.RedArrowsStartNumber] = new TranslationInfo
            {
                QuestionText = "{0}の開始地点の数字は？",
            },

            // #Red Cipher
            // {0}の回答は？
            // Red Cipherの回答は？
            [Question.RedCipherAnswer] = new TranslationInfo
            {
                QuestionText = "{0}の回答は？",
            },

            // #Red Herring
            // {0}において、最初に点滅した色は？
            // Red Herringにおいて、最初に点滅した色は？
            [Question.RedHerringFirstFlash] = new TranslationInfo
            {
                QuestionText = "{0}において、最初に点滅した色は？",
            },

            // #Reformed Role Reversal
            // {0}の解除条件は？
            // Reformed Role Reversalの解除条件は？
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
            // {0}の{1}番目のワイヤーの色は？
            // Reformed Role Reversalの1番目のワイヤーの色は？
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

            // #Regular Crazy Talk
            // {0}において、回答のフレーズに表示されていた数字は？
            // Regular Crazy Talkにおいて、回答のフレーズに表示されていた数字は？
            [Question.RegularCrazyTalkDigit] = new TranslationInfo
            {
                QuestionText = "{0}において、回答のフレーズに表示されていた数字は？",
            },
            // {0}の回答のフレーズの装飾は？
            // Regular Crazy Talkの回答のフレーズの装飾は？
            [Question.RegularCrazyTalkModifier] = new TranslationInfo
            {
                QuestionText = "{0}の回答のフレーズの装飾は？",
            },

            // #Retirement
            // {0}において、これらのうちBOBが定年後に選択しなかった家は？
            // Retirementにおいて、これらのうちBOBが定年後に選択しなかった家は？
            [Question.RetirementHouses] = new TranslationInfo
            {
                QuestionText = "{0}において、これらのうちBOBが定年後に選択しなかった家は？",
            },

            // 逆モールス信号
            // {0}の{2}つめのメッセージの{1}文字目は？
            // 逆モールス信号の1つめのメッセージの1文字目は？
            [Question.ReverseMorseCharacters] = new TranslationInfo
            {
                QuestionText = "{0}の{2}つめのメッセージの{1}文字目は？",
            },

            // #Reverse Polish Notation
            // {0}のラウンド{1}で使用された文字は？
            // Reverse Polish Notationのラウンド1で使用された文字は？
            [Question.ReversePolishNotationCharacter] = new TranslationInfo
            {
                QuestionText = "{0}のラウンド{1}で使用された文字は？",
            },

            // #RGB Maze
            // {0}の出口の座標は？
            // RGB Mazeの出口の座標は？
            [Question.RGBMazeExit] = new TranslationInfo
            {
                QuestionText = "{0}の出口の座標は？",
            },
            // {0}における{1}色のキーはどこ？
            // RGB Mazeにおける{1}色のキーはどこ？
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

            // #Rhythms
            // {0}のLEDの色は？
            // RhythmsのLEDの色は？
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

            // #The Rule
            // {0}のルール番号は？
            // The Ruleのルール番号は？
            [Question.RuleNumber] = new TranslationInfo
            {
                QuestionText = "{0}のルール番号は？",
            },

            // #Roger
            // {0}から与えられた4桁の数字は？
            // Rogerから与えられた4桁の数字は？
            [Question.RogerSeed] = new TranslationInfo
            {
                QuestionText = "{0}から与えられた4桁の数字は？",
            },

            // #Role Reversal
            // {0}の正しい状態の数字は？
            // Role Reversalの正しい状態の数字は？
            [Question.RoleReversalNumber] = new TranslationInfo
            {
                QuestionText = "{0}の正しい状態の数字は？",
            },
            // {0}における{1}系のワイヤーの総数は？
            // Role Reversalにおける暖色系のワイヤーの総数は？
            [Question.RoleReversalWires] = new TranslationInfo
            {
                QuestionText = "{0}における{1}系のワイヤーの総数は？",
            },

            // #Rule of Three
            // {0}の{2}色の頂点の{1}座標は？
            // Rule of Threeの{2}色の頂点の{1}座標は？
            [Question.RuleOfThreeCoordinates] = new TranslationInfo
            {
                QuestionText = "{0}の{2}色の頂点の{1}座標は？",
            },
            // {0}の{3}回目のサイクルにおける{1}色の球の{2}軸上の位置は？
            // Rule of Threeの{3}回目のサイクルにおける{1}色の球の{2}軸上の位置は？
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
            // {0}の{1}はどこ？
            [Question.SamsungAppPositions] = new TranslationInfo
            {
                QuestionText = "{0}の{1}はどこ？",
            },

            // #Scavenger Hunt
            // {0}のステージ{1}で正しく送信されたタイルは？
            // Scavenger Huntのステージ1で正しく送信されたタイルは？
            [Question.ScavengerHuntKeySquare] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{1}で正しく送信されたタイルは？",
            },
            // {0}の最初のステージで{1}色だったタイルは？
            // Scavenger Huntの最初のステージで赤色だったタイルは？
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

            // #Schlag den Bomb
            // {0}の出場者の名前は？
            // Schlag den Bombの出場者の名前は？
            [Question.SchlagDenBombContestantName] = new TranslationInfo
            {
                QuestionText = "{0}の出場者の名前は？",
            },
            // {0}の出場者のスコアは？
            // Schlag den Bombの出場者のスコアは？
            [Question.SchlagDenBombContestantScore] = new TranslationInfo
            {
                QuestionText = "{0}の出場者のスコアは？",
            },
            // {0}の爆弾のスコアは？
            // Schlag den Bombの爆弾のスコアは？
            [Question.SchlagDenBombBombScore] = new TranslationInfo
            {
                QuestionText = "{0}の爆弾のスコアは？",
            },

            // #Sea Shells
            // {0}の{1}フレーズ目で使用された1,2番目の単語は？
            // Sea Shellsの1フレーズ目で使用された1,2番目の単語は？
            [Question.SeaShells1] = new TranslationInfo
            {
                QuestionText = "{0}の{1}フレーズ目で使用された1,2番目の単語は？",
            },
            // {0}の{1}フレーズ目で使用された3,4番目の単語は？
            // Sea Shellsの1フレーズ目で使用された3,4番目の単語は？
            [Question.SeaShells2] = new TranslationInfo
            {
                QuestionText = "{0}の{1}フレーズ目で使用された3,4番目の単語は？",
            },
            // {0}の{1}フレーズ目で使用された最後の単語は？
            // Sea Shellsの1フレーズ目で使用された最後の単語は？
            [Question.SeaShells3] = new TranslationInfo
            {
                QuestionText = "{0}の{1}フレーズ目で使用された最後の単語は？",
            },

            // Semamorse
            // {0}の初期値を求める際に使用した表示のうち{1}の英字は？
            // Semamorseの初期値を求める際に使用した表示のうちモールス信号の英字は？
            [Question.SemamorseLetters] = new TranslationInfo
            {
                QuestionText = "{0}の初期値を求める際に使用した表示のうち{1}の英字は？",
            },
            // {0}の初期値を求める際に使用した表示の色は？
            // Semamorseの初期値を求める際に使用した表示の色は？
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

            // #The Sequencyclopedia
            // {0}では何のシークエンスが使用された？
            // The Sequencyclopediaでは何のシークエンスが使用された？
            [Question.SequencyclopediaSequence] = new TranslationInfo
            {
                QuestionText = "{0}では何のシークエンスが使用された？",
            },

            // #Shapes And Bombs
            // {0}の初期の英字は？
            // Shapes And Bombsの初期の英字は？
            [Question.ShapesAndBombsInitialLetter] = new TranslationInfo
            {
                QuestionText = "{0}の初期の英字は？",
            },

            // 形状変化
            // {0}の最初の図形は？
            // 形状変化の最初の図形は？
            [Question.ShapeShiftInitialShape] = new TranslationInfo
            {
                QuestionText = "{0}の最初の図形は？",
            },

            // #Shell Game
            // {0}の初期カップの最終位置は？
            // Shell Gameの初期カップの最終位置は？
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

            // #Shifted Maze
            // {0}の{1}にあるマークの色は？
            // Shifted Mazeの左上にあるマークの色は？
            [Question.ShiftedMazeColors] = new TranslationInfo
            {
                QuestionText = "{0}の{1}にあるマークの色は？",
            },

            // #Shifting Maze
            // {0}のシード値は？
            // Shifting Mazeのシード値は？
            [Question.ShiftingMazeSeed] = new TranslationInfo
            {
                QuestionText = "{0}のシード値は？",
            },

            // #Shogi Identification
            // {0}に表示された駒は？
            // Shogi Identificationに表示された駒は？
            [Question.ShogiIdentificationPiece] = new TranslationInfo
            {
                QuestionText = "{0}に表示された駒は？",
            },

            // #Silly Slots
            // {0}のステージ{2}において、{1}回目のスロットは？
            // Silly Slotsのステージ1において、1回目のスロットは？
            [Question.SillySlots] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}において、{1}回目のスロットは？",
            },

            // #Simon Samples
            // {0}のステージ{1}の呼び出しは？
            // Simon Samplesのステージ1の呼び出しは？
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

            // サイモンゲーム
            // {0}の最終シークエンスにおいて、{1}番目に点滅した色は？
            // サイモンゲームの最終シークエンスにおいて、1番目に点滅した色は？
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

            // #Simon Scrambles
            // {0}の{1}番目の点滅は？
            // Simon Scramblesの1番目の点滅は？
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

            // #Simon Screams
            // {0}の最終シークエンスにおいて、{1}番目に点滅した色は？
            // Simon Screamsの最終シークエンスにおいて、1番目に点滅した色は？
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

            // #Simon Selects
            // {0}のステージ{2}において、{1}番目に点滅した色は？
            // Simon Selectsのステージ1において、1番目に点滅した色は？
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
            // {0}で{1}色が受け取った英字は？
            // {0}で{1}色が受け取った英字は？
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
            // #Simon Simons
            // {0}の最終シークエンスにおいて、{1}番目に点滅した色は？
            // Simon Simonsの最終シークエンスにおいて、1番目に点滅した色は？
            [Question.SimonSimonsFlashingColors] = new TranslationInfo
            {
                QuestionText = "{0}の最終シークエンスにおいて、{1}番目に点滅した色は？",
            },

            // #simon sings
            // {0}のステージ{2}において、{1}番目に点滅したキーは？
            // simon singsのステージ1において、1番目に点滅したキーは？
            [Question.SimonSingsFlashing] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}において、{1}番目に点滅したキーは？",
            },

            // #Simon Shouts
            // {0}の{1}の位置が点滅した英字は？
            // Simon Shoutsの上の位置が点滅した英字は？
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

            // #Simon Shrieks
            // {0}の最終シークエンスにおいて、{1}番目の点滅は矢印から時計回りに何番目のスペースにある？
            // Simon Shrieksの最終シークエンスにおいて、1番目の点滅は矢印から時計回りに何番目のスペースにある？
            [Question.SimonShrieksFlashingButton] = new TranslationInfo
            {
                QuestionText = "{0}の最終シークエンスにおいて、{1}番目の点滅は矢印から時計回りに何番目のスペースにある？",
            },

            // サイモンの響き
            // {0}の最終シークエンスにおいて、{1}番目に再生されたサンプルボタンの色は？
            // サイモンの響きの最終シークエンスにおいて、1番目に再生されたサンプルボタンの色は？
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

            // #Simon Speaks
            // {0}の1番目の点滅の吹き出しの色は？
            // Simon Speaksの1番目の点滅の吹き出しの色は？
            [Question.SimonSpeaksPositions] = new TranslationInfo
            {
                QuestionText = "{0}の1番目の点滅の吹き出しの色は？",
            },
            // {0}の2番目の点滅の吹き出しの色は？
            // Simon Speaksの2番目の点滅の吹き出しの色は？
            [Question.SimonSpeaksShapes] = new TranslationInfo
            {
                QuestionText = "{0}の2番目の点滅の吹き出しの色は？",
            },
            // {0}の3番目の点滅の言語は？
            // Simon Speaksの3回目の点滅の言語は？
            [Question.SimonSpeaksLanguages] = new TranslationInfo
            {
                QuestionText = "{0}の3回目の点滅の言語は？",
            },
            // {0}の4番目の点滅の単語は？
            // Simon Speaksの4番目の点滅の単語は？
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
            // {0}の5番目の点滅の吹き出しの色は？
            // Simon Speaksの5番目の点滅の吹き出しの色は？
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

            // サイモンの星
            // {0}のシークエンスにおいて、{1}番目に点滅した色は？
            // サイモンの星のシークエンスにおいて、1番目に点滅した色は？
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

            // #Simon Stages
            // {0}のステージ{2}における{1}番目の点滅した色は？
            // Simon Stagesのステージ1における1番目の点滅した色は？
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
            // {0}のステージ{1}におけるインジケーターの色は？
            // Simon Stagesのステージ1におけるインジケーターの色は？
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

            // サイモンの陳述
            // {0}のステージ{2}ではどの{1}？
            // サイモンの陳述のステージ1ではどの{1}？
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

            // #Simon Stops
            // {0}の出力シークエンスにおいて、{1}番目に点滅した色は？
            // Simon Stopsの出力シークエンスにおいて、{1}番目に点滅した色は？
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

            // #Simon Stores
            // {0}の最終シークエンスにおいて、{2}番目に{1}色は？
            // {0}の最終シークエンスにおいて、{2}番目に{1}色は？
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

            // #Simon Supports
            // {0}の{1}番目のトピックは？
            // Simon Supportsの1番目のトピックは？
            [Question.SimonSupportsTopics] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目のトピックは？",
            },

            // 歪曲スロット
            // {0}の初期値は？
            // 歪曲スロットの初期値は？
            [Question.SkewedSlotsOriginalNumbers] = new TranslationInfo
            {
                QuestionText = "{0}の初期値は？",
            },

            // #Skyrim
            // {0}において、選択可能だが解除策ではなかった人種は？
            // Skyrimにおいて、選択可能だが解除策ではなかった人種は？
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
            // {0}において、選択可能だが解除策ではなかった武器は？
            // Skyrimにおいて、選択可能だが解除策ではなかった武器は？
            [Question.SkyrimWeapon] = new TranslationInfo
            {
                QuestionText = "{0}において、選択可能だが解除策ではなかった武器は？",
            },
            // {0}において、選択可能だが解除策ではなかったエネミーは？
            // Skyrimにおいて、選択可能だが解除策ではなかったエネミーは？
            [Question.SkyrimEnemy] = new TranslationInfo
            {
                QuestionText = "{0}において、選択可能だが解除策ではなかったエネミーは？",
            },
            // {0}において、選択可能だが解除策ではなかった都市は？
            // Skyrimにおいて、選択可能だが解除策ではなかった都市は？
            [Question.SkyrimCity] = new TranslationInfo
            {
                QuestionText = "{0}において、選択可能だが解除策ではなかった都市は？",
            },
            // {0}において、選択可能だが解除策ではなかったドラゴンは？
            // Skyrimにおいて、選択可能だが解除策ではなかったドラゴンは？
            [Question.SkyrimDragonShout] = new TranslationInfo
            {
                QuestionText = "{0}において、選択可能だが解除策ではなかったドラゴンは？",
            },

            // #Small Circle
            // {0}におけるシークエンスのシフト量は？
            // Small Circleにおけるシークエンスのシフト量は？
            [Question.SmallCircleShift] = new TranslationInfo
            {
                QuestionText = "{0}におけるシークエンスのシフト量は？",
            },
            // {0}の初期時点で音が違っていたのは？
            // Small Circleの初期時点で音が違っていたのは？
            [Question.SmallCircleWedge] = new TranslationInfo
            {
                QuestionText = "{0}の初期時点で音が違っていたのは？",
            },
            // {0}の解除シークエンスの{1}番目の色は？
            // Small Circleの解除シークエンスの1番目の色は？
            [Question.SmallCircleSolution] = new TranslationInfo
            {
                QuestionText = "{0}の解除シークエンスの{1}番目の色は？",
            },

            // #Snooker
            // {0}の開始時点での赤いボールの数は？
            // Snookerの開始時点での赤いボールの数は？
            [Question.SnookerReds] = new TranslationInfo
            {
                QuestionText = "{0}の開始時点での赤いボールの数は？",
            },

            // #Sorting
            // {0}を解く際の最後のスワップはどの位置で行われた？
            // Sortingを解く際の最後のスワップはどの位置で行われた？
            [Question.SortingLastSwap] = new TranslationInfo
            {
                QuestionText = "{0}を解く際の最後のスワップはどの位置で行われた？",
            },

            // 思い出
            // 他の「思い出」モジュールが最初に質問したのは、何のモジュールについて？
            // 他の「思い出」モジュールが最初に質問したのは、何のモジュールについて？
            [Question.SouvenirFirstQuestion] = new TranslationInfo
            {
                QuestionText = "他の「思い出」モジュールが最初に質問したのは、何のモジュールについて？",
            },

            // #Space Traders
            // {0}での1隻当たりの最大税額は？
            // Space Tradersでの1隻当たりの最大税額は？
            [Question.SpaceTradersMaxTax] = new TranslationInfo
            {
                QuestionText = "{0}での1隻当たりの最大税額は？",
            },

            // #Sonic The Hedgehog
            // {0}における{1}番目の画像は？
            // Sonic The Hedgehogにおける1番目の画像は？
            [Question.SonicTheHedgehogPictures] = new TranslationInfo
            {
                QuestionText = "{0}における{1}番目の画像は？",
            },
            // {0}において、{1}のスクリーンで流れていたサウンドは？
            // Sonic The Hedgehogにおいて、Running Bootsのスクリーンで流れていたサウンドは？
            [Question.SonicTheHedgehogSounds] = new TranslationInfo
            {
                QuestionText = "{0}において、{1}のスクリーンで流れていたサウンドは？",
            },

            // #The Sphere
            // {0}にて{1}番目に点滅した色は？
            // The Sphereにて1番目に点滅した色は？
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

            // #Spelling Bee
            // {0}で打ち込んだ単語は？
            // Spelling Beeで打ち込んだ単語は？
            [Question.SpellingBeeWord] = new TranslationInfo
            {
                QuestionText = "{0}で打ち込んだ単語は？",
            },

            // #Splitting The Loot
            // {0}にて初期から色付けされていた袋は？
            // Splitting The Lootにて初期から色付けされていた袋は？
            [Question.SplittingTheLootColoredBag] = new TranslationInfo
            {
                QuestionText = "{0}にて初期から色付けされていた袋は？",
            },

            // #Spot the Difference
            // {0}における間違ったな球の色は？
            // Spot the Differenceにおける間違った球の色は？
            [Question.SpotTheDifferenceFaultyBall] = new TranslationInfo
            {
                QuestionText = "{0}における間違ったな球の色は？",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "青",
                    ["Green"] = "緑",
                    ["Orange"] = "オレンジ",
                    ["Red"] = "赤",
                },
            },

            // #Stacked Sequences
            // {0}のシークエンスの長さは？
            // Stacked Sequencesのシークエンスの長さは？
            [Question.StackedSequences] = new TranslationInfo
            {
                QuestionText = "{0}のシークエンスの長さは？",
            },

            // #Stars
            // {0}の中心の数字は？
            // Starsの中心の数字は？
            [Question.StarsCenter] = new TranslationInfo
            {
                QuestionText = "{0}の中心の数字は？",
            },

            // #State of Aggregation
            // {0}に表示された要素は？
            // State of Aggregationに表示された要素は？
            [Question.StateOfAggregationElement] = new TranslationInfo
            {
                QuestionText = "{0}に表示された要素は？",
            },

            // #Stellar
            // {0}における{1}の英字は？
            // Stellarにおけるモールス信号の英字は？
            [Question.StellarLetters] = new TranslationInfo
            {
                QuestionText = "{0}における{1}の英字は？",
            },

            // #Stupid Slots
            // {0}の{1}にある矢印の値は？
            // Stupid Slotsの左上にある矢印の値は？
            [Question.StupidSlotsValues] = new TranslationInfo
            {
                QuestionText = "{0}の{1}にある矢印の値は？",
            },

            // #Subscribe to Pewdiepie
            // {0}における{1}のサブスクライバーの数は？
            // Subscribe to PewdiepieにおけるPewDiePieのサブスクライバーの数は？
            [Question.SubscribeToPewdiepieSubCount] = new TranslationInfo
            {
                QuestionText = "{0}における{1}のサブスクライバーの数は？",
            },

            // #Sugar Skulls
            // {0}にて{1}の位置に表示された骸骨は？
            // Sugar Skullsにて上の正方形に表示された骸骨は？
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
            // {0}に表示されて{1}骸骨は？
            // Sugar Skullsに表示されていた骸骨は？
            [Question.SugarSkullsAvailability] = new TranslationInfo
            {
                QuestionText = "{0}に表示されて{1}骸骨は？",
            },

            // #Superparsing
            // {0}で表示された単語は？
            // Superparsingで表示された単語は？
            [Question.SuperparsingDisplayed] = new TranslationInfo
            {
                QuestionText = "{0}で表示された単語は？",
            },

            // #The Switch
            // {0}の{2}回目の切り替え時の{1}部のLEDの色は？
            // The Switchの1回目の切り替え時の上部のLEDの色は？
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

            // スイッチ
            // {0}の最初の状態は？
            // スイッチの最初の状態は？
            [Question.SwitchesInitialPosition] = new TranslationInfo
            {
                QuestionText = "{0}の最初の状態は？",
            },

            // #Switching Maze
            // {0}のシード値は？
            // Switching Mazeのシード値は？
            [Question.SwitchingMazeSeed] = new TranslationInfo
            {
                QuestionText = "{0}のシード値は？",
            },
            // {0}の開始迷路の色は？
            // Switching Mazeの開始迷路の色は？
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

            // #Symbol Cycle
            // {0}にて{1}側のディスプレイに表示されたシンボルの数は？
            // Symbol Cycleにて左側のディスプレイに表示されたシンボルの数は？
            [Question.SymbolCycleSymbolCounts] = new TranslationInfo
            {
                QuestionText = "{0}にて{1}側のディスプレイに表示されたシンボルの数は？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "左",
                    ["right"] = "右",
                },
            },

            // #Symbolic Coordinates
            // {0}のステージ{2}における{1}のシンボルは？
            // Symbolic Coordinatesのステージ1における左のシンボルは？
            [Question.SymbolicCoordinateSymbols] = new TranslationInfo
            {
                QuestionText = "Symbolic Coordinatesのステージ1における左のシンボルは？",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "左",
                    ["middle"] = "中央",
                },
            },

            // #Symbolic Tasha
            // {0}の最後のシークエンスで{1}番目に点滅したものは？
            // Symbolic Tashaの最後のシークエンスで1番目に点滅したものは？
            [Question.SymbolicTashaFlashes] = new TranslationInfo
            {
                QuestionText = "{0}の最後のシークエンスで{1}番目に点滅したものは？",
            },
            // {0}の{1}の位置のシンボルは？
            // Symbolic Tashaの上の位置のシンボルは？
            [Question.SymbolicTashaSymbols] = new TranslationInfo
            {
                QuestionText = "{0}の{1}の位置のシンボルは？",
            },

            // #SYNC-125 [3]
            // {0}にてステージ{1}でスクリーンに表示されたものは？
            // SYNC-125 [3]にてステージ1でスクリーンに表示されたものは？
            [Question.Sync125_3Word] = new TranslationInfo
            {
                QuestionText = "{0}にてステージ{1}でスクリーンに表示されたものは？",
            },

            // #Synonyms
            // {0}のディスプレイの数字は？
            // Synonymsのディスプレイの数字は？
            [Question.SynonymsNumber] = new TranslationInfo
            {
                QuestionText = "{0}のディスプレイの数字は？",
            },

            // #Sysadmin
            // {0}で修正したエラーコードは？
            // Sysadminで修正したエラーコードは？
            [Question.SysadminFixedErrorCodes] = new TranslationInfo
            {
                QuestionText = "{0}で修正したエラーコードは？",
            },

            // タップ・コード
            // {0}で受信した単語は？
            // タップ・コードで受信した単語は？
            [Question.TapCodeReceivedWord] = new TranslationInfo
            {
                QuestionText = "{0}で受信した単語は？",
            },

            // #Tasha Squeals
            // {0}で{1}番目に点滅した色は？
            // Tasha Squealsで1番目に点滅した色は？
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

            // #Tenpins
            // {0}の{1}のスプリットは？
            // Tenpinsの赤のスプリットは？
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

            // テキストフィールド
            // {0}で表示された文字は？
            // テキストフィールドで表示された文字は？
            [Question.TextFieldDisplay] = new TranslationInfo
            {
                QuestionText = "{0}で表示された文字は？",
            },

            // #Thinking Wires
            // {0}において最初に切る必要のあるワイヤーの位置(上から下)は？
            // Thinking Wiresにおいて最初に切る必要のあるワイヤーの位置(上から下)は？
            [Question.ThinkingWiresFirstWire] = new TranslationInfo
            {
                QuestionText = "{0}において最初に切る必要のあるワイヤーの位置(上から下)は？",
            },
            // {0}において2番目に切った有効なワイヤーの色は？
            // Thinking Wiresにおいて2番目に切った有効なワイヤーの色は？
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
            // {0}のディスプレイの数字は？
            // Thinking Wiresのディスプレイの数字は？
            [Question.ThinkingWiresDisplayNumber] = new TranslationInfo
            {
                QuestionText = "{0}のディスプレイの数字は？",
            },

            // サードベース
            // {0}にてステージ{1}で表示された単語は？
            // サードベースにてステージ1で表示された単語は？
            [Question.ThirdBaseDisplay] = new TranslationInfo
            {
                QuestionText = "{0}にてステージ{1}で表示された単語は？",
            },

            // #Tic Tac Toe
            // {0}の{1}のボタンの初期状態は？
            // Tic Tac Toeの左上のボタンの初期状態は？
            [Question.TicTacToeInitialState] = new TranslationInfo
            {
                QuestionText = "{0}の{1}のボタンの初期状態は？",
            },

            // #Timezone
            // {0}の{1}都市は？
            // Timezoneの開始都市は？
            [Question.TimezoneCities] = new TranslationInfo
            {
                QuestionText = "{0}の{1}都市は？",
            },

            // #Topsy Turvy
            // {0}で最初に表示された単語は？
            // Topsy Turvyで最初に表示された単語は？
            [Question.TopsyTurvyWord] = new TranslationInfo
            {
                QuestionText = "{0}で最初に表示された単語は？",
            },

            // #Touch Transmission
            // {0}で送信した単語は？
            // Touch Transmissionで送信した単語は？
            [Question.TouchTransmissionWord] = new TranslationInfo
            {
                QuestionText = "{0}で送信した単語は？",
            },
            // {0}では点字をどのような順序で読んだ？
            // Touch Transmissionでは点字をどのような順序で読んだ？
            [Question.TouchTransmissionOrder] = new TranslationInfo
            {
                QuestionText = "{0}では点字をどのような順序で読んだ？",
            },

            // 軌跡
            // {0}でのボタン{1}の役割は？
            // 軌跡でのボタンAの役割は？
            [Question.TrajectoryButtonFunctions] = new TranslationInfo
            {
                QuestionText = "{0}でのボタン{1}の役割は？",
            },

            // #Transmitted Morse
            // {0}にて{1}番目に受け取ったメッセージは？
            // Transmitted Morseにて1番目に受け取ったメッセージは？
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

            // #Turtle Robot
            // {0}でコメントアウトした行の{1}番目は？
            // Turtle Robotでコメントアウトした行の1番目は？
            [Question.TurtleRobotCodeLines] = new TranslationInfo
            {
                QuestionText = "{0}でコメントアウトした行の{1}番目は？",
            },

            // ツービッツ
            // {0}で{1}番目のクエリの返答は？
            // ツービッツで1番目のクエリの返答は？
            [Question.TwoBitsResponse] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目のクエリの返答は？",
            },

            // #Ultimate Cipher
            // {0}の回答は？
            // Ultimate Cipherの回答は？
            [Question.UltimateCipherAnswer] = new TranslationInfo
            {
                QuestionText = "{0}の回答は？",
            },

            // #Ultimate Cycle
            // {0}の{1}は？
            // Ultimate Cycleのメッセージは？
            [Question.UltimateCycleWord] = new TranslationInfo
            {
                QuestionText = "{0}の{1}は？",
            },

            // 極立方体
            // {0}の{1}番目の回転方向は？
            // 極立方体の1番目の回転方向は？
            [Question.UltracubeRotations] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の回転方向は？",
            },

            // #UltraStores
            // {0}のステージ{2}における{1}番目の回転方向は？
            // UltraStoresのステージ1における1番目の回転方向は？
            [Question.UltraStoresSingleRotation] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}における{1}番目の回転方向は？",
            },
            // {0}のステージ{2}における{1}番目の回転方向は？
            // UltraStoresのステージ1における1番目の回転方向は？
            [Question.UltraStoresMultiRotation] = new TranslationInfo
            {
                QuestionText = "{0}のステージ{2}における{1}番目の回転方向は？",
            },

            // 色無し格子
            // {0}の最初のステージで利用したもののうち読み順で{1}番目の色は何色？
            // 色無し格子の最初のステージで利用したもののうち読み順で1番目の色は何色？
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

            // 色無しスイッチ
            // {0}の最初のスイッチの状態は？
            // 色無しスイッチの最初のスイッチの状態は？
            [Question.UncoloredSwitchesInitialState] = new TranslationInfo
            {
                QuestionText = "{0}の最初のスイッチの状態は？",
            },

            // 色無しスイッチ
            // {0}の読み順で{1}番目のLEDは何色？
            // 色無しスイッチの読み順で1番目のLEDは何色？
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

            // #Unfair Cipher
            // {0}で{1}番目に受け取った指示は？
            // Unfair Cipherで{1番目に受け取った指示は？
            [Question.UnfairCipherInstructions] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に受け取った指示は？",
            },

            // #Unfair’s Revenge
            // {0}で{1}番目に解読した指示は？
            // Unfair’s Revengeで1番目に解読した指示は？
            [Question.UnfairsRevengeInstructions] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に解読した指示は？",
            },

            // ユニコード
            // {0}にて{1}番目に送信したコードは？
            // ユニコードにて1番目に送信したコードは？
            [Question.UnicodeSortedAnswer] = new TranslationInfo
            {
                QuestionText = "{0}にて{1}番目に送信したコードは？",
            },

            // #Unown Cipher
            // {0}にて送信した単語の{1}番目の英字は？
            // Unown Cipherにて送信した単語の1番目の英字は？
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

            // #USA Maze
            // {0}の開始地点は？
            // USA Mazeの開始地点は？
            [Question.USAMazeOrigin] = new TranslationInfo
            {
                QuestionText = "{0}の開始地点は？",
            },

            // #V
            // {0}が表示{1}単語は？
            // Vが表示した単語は？
            [Question.VWords] = new TranslationInfo
            {
                QuestionText = "{0}が表示{1}単語は？",
            },

            // 色どり格子
            // {0}で最初に押した色は？
            // 色どり格子で最初に押した色は？
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

            // #Vcrcs
            // {0}で表示された単語は？
            // Vcrcsで表示された単語は？
            [Question.VcrcsWord] = new TranslationInfo
            {
                QuestionText = "{0}で表示された単語は？",
            },

            // #Vectors
            // {0}にて{1}番目のベクターの色は？
            // Vectorsにて1番目のベクターの色は？
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

            // #Vexillology
            // {0}にてポールの色の{1}番目の色は？
            // Vexillologyにてポールの色の1番目の色は？
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

            // #Violet Cipher
            // {0}の回答は？
            // Violet Cipherの回答は？
            [Question.VioletCipherAnswer] = new TranslationInfo
            {
                QuestionText = "{0}の回答は？",
            },

            // 視覚障害
            // {0}にてステージ{1}で押す必要のあった色は？
            // 視覚障害にてステージ1で押す必要のあった色は？
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

            // #Wavetapping
            // {0}のステージ{1}の色は？
            // Wavetappingのステージ1の色は？
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
            // {0}にてステージ{1}の正しいパターンは？
            // Wavetappingにてステージ1の正しいパターンは？
            [Question.WavetappingPatterns] = new TranslationInfo
            {
                QuestionText = "{0}にてステージ{1}の正しいパターンは？",
            },

            // #What’s on Second
            // {0}にてステージ{1}で表示されたテキストは？
            // What’s on Secondにてステージ1で表示されたテキストは？
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
                    ["what？"] = "what？",
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
            // {0}にてステージ{1}で表示されたテキストの色は？
            // What’s on Secondにてステージ1で表示されたテキストの色は？
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

            // #White Cipher
            // {0}の回答は？
            // White Cipherの回答は？
            [Question.WhiteCipherAnswer] = new TranslationInfo
            {
                QuestionText = "{0}の回答は？",
            },

            // 表比較
            // {0}にてステージ{1}で表示された単語は？
            // Who’s on Firstにてステージ1で表示された単語は？
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

            // #The Wire
            // {0}の{1}の位置にあったダイヤルの色は？
            // The Wireの上の位置にあったダイヤルの色は？
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

            // #Wire Ordering
            // {0}の左から{1}番目のディスプレイの色は？
            // Wire Orderingの左から1番目のディスプレイの色は？
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
            // {0}の左から{1}番目のディスプレイの数字は？
            // Wire Orderingの左から1番目のディスプレイの数字は？
            [Question.WireOrderingDisplayNumber] = new TranslationInfo
            {
                QuestionText = "{0}の左から{1}番目のディスプレイの数字は？",
            },
            // {0}の左から{1}番目のワイヤーの色は？
            // Wire Orderingの左から1番目のワイヤーの色は？
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

            // 順番ワイヤ
            // (0)の{1}色のワイヤーの総数は？
            // 順番ワイヤの赤色のワイヤーの総数は？
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

            // #Wolf, Goat, and Cabbage
            // {0}に{1}のはどれ？
            // Wolf, Goat, and Cabbageにあるのはどれ？
            [Question.WolfGoatAndCabbageAnimals] = new TranslationInfo
            {
                QuestionText = "{0}に{1}のはどれ？",
            },
            // {0}のボートのサイズは？
            // Wolf, Goat, and Cabbageのボートのサイズは？
            [Question.WolfGoatAndCabbageBoatSize] = new TranslationInfo
            {
                QuestionText = "{0}のボートのサイズは？",
            },

            // #Working Title
            // {0}にて表示されたラベルは？
            // Working Titleにて表示されたラベルは？
            [Question.WorkingTitleLabel] = new TranslationInfo
            {
                QuestionText = "{0}にて表示されたラベルは？",
            },

            // #XmORse Code
            // {0}で表示された単語の{1}番目の位置(読み順)にある英字は？
            // XmORse Codeで表示された単語の1番目の位置(読み順)にある英字は？
            [Question.XmORseCodeDisplayedLetters] = new TranslationInfo
            {
                QuestionText = "{0}で表示された単語の{1}番目の位置(読み順)にある英字は？",
            },
            // {0}で解読した単語は？
            // XmORse Codeで解読した単語は？
            [Question.XmORseCodeWord] = new TranslationInfo
            {
                QuestionText = "{0}で解読した単語は？",
            },

            // #The Xenocryst
            // {0}の{1}番目の点滅の色は？
            // Xenocrystの1番目の点滅の色は？
            [Question.Xenocryst] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の点滅の色は？",
            },

            // ヤッツィー
            // {0}の最初のロール後の状態は？
            // ヤッツィーの最初のロール後の状態は？
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

            // 黄色矢印
            // {0}の開始行の英字は？
            // 黄色矢印の開始行の英字は？
            [Question.YellowArrowsStartingRow] = new TranslationInfo
            {
                QuestionText = "{0}の開始行の英字は？",
            },

            // #The Yellow Button
            // {0}の{1}番目の色は？
            // The Yellow Buttonの1番目の色は？
            [Question.YellowButtonColors] = new TranslationInfo
            {
                QuestionText = "{0}の{1}番目の色は？",
            },

            // #Yellow Cipher
            // {0}の回答は？
            // Yellow Cipherの回答は？
            [Question.YellowCipherAnswer] = new TranslationInfo
            {
                QuestionText = "{0}の回答は？",
            },

            // #Zero, Zero
            // {0}の{1}の位置の星の色は？
            // Zero, Zeroの左上の位置の星の色は？
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
            // {0}の{1}の位置の星のポイントはいくつ？
            // Zero, Zeroの左上の位置の星のポイントはいくつ？
            [Question.ZeroZeroStarPoints] = new TranslationInfo
            {
                QuestionText = "{0}の{1}の位置の星のポイントは？",
            },
            // {0}の{1}色の正方形の場所は？
            // Zero, Zeroの赤色の正方形の場所は?
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

            // #zoni
            // {0}で{1}番目に解読した単語は？
            // zoniで1番目に解読した単語は？
            [Question.ZoniWords] = new TranslationInfo
            {
                QuestionText = "{0}で{1}番目に解読した単語は？",
            },

        };
        #endregion
    }
}

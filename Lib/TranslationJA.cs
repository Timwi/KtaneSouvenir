using System.Collections.Generic;

namespace Souvenir;

public class Translation_ja : TranslationBase<TranslationInfo>
{
    public override string FormatModuleName(Question question, bool addSolveCount, int numSolved) => addSolveCount
        ? $"{Ordinal(numSolved)}番目に解除された{_translations[question].ModuleName ?? Ut.GetAttribute(question).ModuleName}"
        : _translations[question].ModuleName ?? Ut.GetAttribute(question).ModuleNameWithThe;

    public override string Ordinal(int number) => number.ToString();
    public override int DefaultFontIndex => 8;
    public override float LineSpacing => 0.7f;

    protected override Dictionary<Question, TranslationInfo> _translations => new()
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
            QuestionText = "{0}の初期状態の数字は？",
        },

        // 1000 Words
        // What was the {1} word shown in {0}?
        // What was the first word shown in 1000 Words?
        [Question._1000WordsWords] = new()
        {
            QuestionText = "{0}の{1}番目の単語は何？",
            ModuleName = "1000単語",
        },

        // 100 Levels of Defusal
        // What was the {1} displayed letter in {0}?
        // What was the first displayed letter in 100 Levels of Defusal?
        [Question._100LevelsOfDefusalLetters] = new()
        {
            QuestionText = "{0}で{1}番目に表示された文字は何？",
            ModuleName = "解除レベル100",
        },

        // The 1, 2, 3 Game
        // Who was the opponent in {0}?
        // Who was the opponent in The 1, 2, 3 Game?
        [Question._123GameProfile] = new()
        {
            QuestionText = "{0}の相手は？",
            ModuleName = "123ゲーム",
        },
        // Who was the opponent in {0}?
        // Who was the opponent in The 1, 2, 3 Game?
        [Question._123GameName] = new()
        {
            QuestionText = "{0}の相手は？",
            ModuleName = "123ゲーム",
        },

        // 1D Chess
        // What was {1} in {0}?
        // What was your first move in 1D Chess?
        [Question._1DChessMoves] = new()
        {
            QuestionText = "{0}で{1}はどれだったか？",
            ModuleName = "1Dチェス",
            FormatArgs = new Dictionary<string, string>
            {
                ["your first move"] = "あなたの最初の移動",
                ["Rustmate’s first move"] = "Rustmateの最初の移動",
                ["your second move"] = "あなたの2回目の移動",
                ["Rustmate’s second move"] = "Rustmateの2回目の移動e",
                ["your third move"] = "あなたの3回目の移動",
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

        // 21
        // What was the displayed number in {0}?
        // What was the displayed number in 21?
        [Question._21DisplayedNumber] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What was the displayed number in {0}?",
        },

        // 3D Maze
        // What were the markings in {0}?
        // What were the markings in 3D Maze?
        [Question._3DMazeMarkings] = new()
        {
            QuestionText = "{0}の迷路の文字は何？",
            ModuleName = "3D迷路",
        },
        // What was the cardinal direction in {0}?
        // What was the cardinal direction in 3D Maze?
        [Question._3DMazeBearing] = new()
        {
            QuestionText = "{0}のゴールの方向はどこ？",
            ModuleName = "3D迷路",
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
        [Question._3DTapCodeWord] = new()
        {
            QuestionText = "{0}で受信した単語は？",
            ModuleName = "3Dタップ・コード",
        },

        // 3D Tunnels
        // What was the {1} goal node in {0}?
        // What was the first goal node in 3D Tunnels?
        [Question._3DTunnelsTargetNode] = new()
        {
            QuestionText = "{0}の{1}番目のゴールの目印は何？",
            ModuleName = "3Dトンネル",
        },

        // 3 LEDs
        // What was the initial state of the LEDs in {0} (in reading order)?
        // What was the initial state of the LEDs in 3 LEDs (in reading order)?
        [Question._3LEDsInitialState] = new()
        {
            QuestionText = "{0}の初期のLEDの状態は(読み順)？",
            ModuleName = "3つのLED",
            Answers = new Dictionary<string, string>
            {
                ["off/off/off"] = "オフオフオフ",
                ["off/off/on"] = "オフオフオン",
                ["off/on/off"] = "オフオンオフ",
                ["off/on/on"] = "オフオンオン",
                ["on/off/off"] = "オンオフオフ",
                ["on/off/on"] = "オンオフオン",
                ["on/on/off"] = "オンオンオフ",
                ["on/on/on"] = "オンオンオン",
            },
        },

        // 3N+1
        // What number was initially displayed in {0}?
        // What number was initially displayed in 3N+1?
        [Question._3NPlus1] = new()
        {
            QuestionText = "{0}の最初に表示された番号は？",
        },

        // 64
        // What was the displayed number in {0}?
        // What was the displayed number in 64?
        [Question._64DisplayedNumber] = new()
        {
            QuestionText = "{0}のディスプレー上の数字は？",
        },

        // 7
        // What was the {1} channel’s initial value in {0}?
        // What was the red channel’s initial value in 7?
        [Question._7InitialValues] = new()
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
        // What LED color was shown in stage 0 of 7?
        [Question._7LedColors] = new()
        {
            QuestionText = "{0}のステージ{1}のLEDの色は何？",
            Answers = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["blue"] = "青",
                ["green"] = "緑",
                ["white"] = "白",
            },
        },

        // 9-Ball
        // What was the number of ball {1} in {0}?
        // What was the number of ball A in 9-Ball?
        [Question._9BallLetters] = new()
        {
            QuestionText = "{0}のボール{1}の数字は？",
            ModuleName = "9ボール",
        },
        // What was the letter of ball {1} in {0}?
        // What was the letter of ball 2 in 9-Ball?
        [Question._9BallNumbers] = new()
        {
            QuestionText = "{0}のボール{1}の文字は？",
            ModuleName = "9ボール",
        },

        // Abyss
        // What was the {1} character displayed on {0}?
        // What was the first character displayed on Abyss?
        [Question.AbyssSeed] = new()
        {
            QuestionText = "{0}の{1}番目に表示された文字は？",
            ModuleName = "アビス",
        },

        // Accumulation
        // What was the background color on the {1} stage in {0}?
        // What was the background color on the first stage in Accumulation?
        [Question.AccumulationBackgroundColor] = new()
        {
            QuestionText = "{0}のステージ{1}の背景の色は何？",
            ModuleName = "蓄積",
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
        [Question.AccumulationBorderColor] = new()
        {
            QuestionText = "{0}の境界線の色は何？",
            ModuleName = "蓄積",
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
        [Question.AdventureGameCorrectItem] = new()
        {
            QuestionText = "{0}で{1}番目に正しく使用したアイテムはどれ？",
            ModuleName = "アドベンチャーゲーム",
        },
        // What enemy were you fighting in {0}?
        // What enemy were you fighting in Adventure Game?
        [Question.AdventureGameEnemy] = new()
        {
            QuestionText = "{0}で戦った敵は？",
            ModuleName = "アドベンチャーゲーム",
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
            QuestionText = "{0}の最初の英字は？",
            ModuleName = "英字",
        },

        // Alfa-Bravo
        // Which letter was pressed in {0}?
        // Which letter was pressed in Alfa-Bravo?
        [Question.AlfaBravoPressedLetter] = new()
        {
            QuestionText = "{0}でどの文字を押した？",
            ModuleName = "アルファブラボー",
        },
        // Which letter was to the left of the pressed one in {0}?
        // Which letter was to the left of the pressed one in Alfa-Bravo?
        [Question.AlfaBravoLeftPressedLetter] = new()
        {
            QuestionText = "{0}で押した文字の左にあった文字は何？",
            ModuleName = "アルファブラボー",
        },
        // Which letter was to the right of the pressed one in {0}?
        // Which letter was to the right of the pressed one in Alfa-Bravo?
        [Question.AlfaBravoRightPressedLetter] = new()
        {
            QuestionText = "{0}で押した文字の右にあった文字は何？",
            ModuleName = "アルファブラボー",
        },
        // What was the last digit on the small display in {0}?
        // What was the last digit on the small display in Alfa-Bravo?
        [Question.AlfaBravoDigit] = new()
        {
            QuestionText = "{0}の小さなディスプレーの最後の数字は何？",
            ModuleName = "アルファブラボー",
        },

        // Algebra
        // What was the first equation in {0}?
        // What was the first equation in Algebra?
        [Question.AlgebraEquation1] = new()
        {
            QuestionText = "{0}の最初の方程式は何？",
            ModuleName = "方程式",
        },
        // What was the second equation in {0}?
        // What was the second equation in Algebra?
        [Question.AlgebraEquation2] = new()
        {
            QuestionText = "{0}の二番目の方程式は何？",
            ModuleName = "方程式",
        },

        // Algorithmia
        // Which position was the {1} position in {0}?
        // Which position was the starting position in Algorithmia?
        [Question.AlgorithmiaPositions] = new()
        {
            QuestionText = "{0}の{1}位置は？",
            ModuleName = "アルゴリズム",
            FormatArgs = new Dictionary<string, string>
            {
                ["starting"] = "開始",
                ["goal"] = "終了",
            },
        },
        // What was the color of the colored bulb in {0}?
        // What was the color of the colored bulb in Algorithmia?
        [Question.AlgorithmiaColor] = new()
        {
            QuestionText = "{0}の色付き電球の色は？",
            ModuleName = "アルゴリズム",
        },
        // Which number was present in the seed in {0}?
        // Which number was present in the seed in Algorithmia?
        [Question.AlgorithmiaSeed] = new()
        {
            QuestionText = "{0}のシード内にある数字は？",
            ModuleName = "アルゴリズム",
        },

        // Alphabetical Ruling
        // What was the letter displayed in the {1} stage of {0}?
        // What was the letter displayed in the first stage of Alphabetical Ruling?
        [Question.AlphabeticalRulingLetter] = new()
        {
            QuestionText = "{0}のステージ{1}で表示された文字は何？",
            ModuleName = "アルファベットルール",
        },
        // What was the number displayed in the {1} stage of {0}?
        // What was the number displayed in the first stage of Alphabetical Ruling?
        [Question.AlphabeticalRulingNumber] = new()
        {
            QuestionText = "{0}のステージ{1}で表示された数字は何？",
            ModuleName = "アルファベットルール",
        },

        // Alphabet Numbers
        // Which of these numbers was on one of the buttons in the {1} stage of {0}?
        // Which of these numbers was on one of the buttons in the first stage of Alphabet Numbers?
        [Question.AlphabetNumbersDisplayedNumbers] = new()
        {
            QuestionText = "{0}のステージ{1}でボタン上にあった数字に含まれるのは？",
            ModuleName = "アルファベット番号",
        },

        // Alphabet Tiles
        // What was the {1} letter shown during the cycle in {0}?
        // What was the first letter shown during the cycle in Alphabet Tiles?
        [Question.AlphabetTilesCycle] = new()
        {
            QuestionText = "{0}のサイクルで{1}番目に表示された文字は？",
            ModuleName = "アルファベットタイル",
        },
        // What was the missing letter in {0}?
        // What was the missing letter in Alphabet Tiles?
        [Question.AlphabetTilesMissingLetter] = new()
        {
            QuestionText = "{0}で隠されている文字は何？",
            ModuleName = "アルファベットタイル",
        },

        // Alpha-Bits
        // What character was displayed on the {1} screen on the {2} in {0}?
        // What character was displayed on the first screen on the left in Alpha-Bits?
        [Question.AlphaBitsDisplayedCharacters] = new()
        {
            QuestionText = "{0}で{2}の{1}つ目の画面に表示されている文字は何？",
            ModuleName = "アルファビッツ",
            FormatArgs = new Dictionary<string, string>
            {
                ["left"] = "左",
                ["right"] = "右",
            },
        },

        // A Message
        // What was the initial message in {0}?
        // What was the initial message in A Message?
        [Question.AMessageAMessage] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What was the initial message in {0}?",
        },

        // Amusement Parks
        // Which ride was available in {0}?
        // Which ride was available in Amusement Parks?
        [Question.AmusementParksRides] = new()
        {
            QuestionText = "{0}で利用可能だったアトラクションは？",
            ModuleName = "遊園地",
        },

        // Ángel Hernández
        // What letter was shown by the raised buttons on the {1} stage on {0}?
        // What letter was shown by the raised buttons on the first stage on Ángel Hernández?
        [Question.AngelHernandezMainLetter] = new()
        {
            QuestionText = "{0}のステージ{1}で、点字で表示されていた英字は？",
            ModuleName = "アンヘル・エルナンデス",
        },

        // The Arena
        // What was the maximum weapon damage of the attack phase in {0}?
        // What was the maximum weapon damage of the attack phase in The Arena?
        [Question.ArenaDamage] = new()
        {
            QuestionText = "{0}の攻撃フェーズにおける最大ダメージ数は？",
            ModuleName = "アリーナ",
        },
        // Which enemy was present in the defend phase of {0}?
        // Which enemy was present in the defend phase of The Arena?
        [Question.ArenaEnemies] = new()
        {
            QuestionText = "{0}の防御フェーズで現れた敵は？",
            ModuleName = "アリーナ",
        },
        // Which was a number present in the grab phase of {0}?
        // Which was a number present in the grab phase of The Arena?
        [Question.ArenaNumbers] = new()
        {
            QuestionText = "{0}の獲得フェーズで現れた数字は？",
            ModuleName = "アリーナ",
        },

        // Arithmelogic
        // What was the symbol on the submit button in {0}?
        // What was the symbol on the submit button in Arithmelogic?
        [Question.ArithmelogicSubmit] = new()
        {
            QuestionText = "{0}の送信ボタンの記号は何？",
            ModuleName = "計算論理",
        },
        // Which number was selectable, but not the solution, in the {1} screen on {0}?
        // Which number was selectable, but not the solution, in the left screen on Arithmelogic?
        [Question.ArithmelogicNumbers] = new()
        {
            QuestionText = "{0}の{1}の画面で選択できる、答え以外の数字はどれ？",
            ModuleName = "計算論理",
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
        [Question.ASCIIMazeCharacters] = new()
        {
            QuestionText = "{0}の{1}番目に表示された文字は？",
            ModuleName = "アスキー迷路",
        },

        // A Square
        // Which of these was an index color in {0}?
        // Which of these was an index color in A Square?
        [Question.ASquareIndexColors] = new()
        {
            QuestionText = "{0}で一致した色に含まれるのは？",
            ModuleName = "正方型",
        },
        // Which color was submitted {1} in {0}?
        // Which color was submitted first in A Square?
        [Question.ASquareCorrectColors] = new()
        {
            QuestionText = "{0}で{1}番目に送信した色は？",
            ModuleName = "正方型",
        },

        // Audio Morse
        // What was signaled in {0}?
        // What was signaled in Audio Morse?
        [Question.AudioMorseSound] = new()
        {
            QuestionText = "{0}で送信されたものは？",
            ModuleName = "音声モールス",
        },

        // The Azure Button
        // What was T in {0}?
        // What was T in The Azure Button?
        [Question.AzureButtonT] = new()
        {
            QuestionText = "{0}のTはどれ？",
            ModuleName = "空色ボタン",
        },
        // Which of these cards was shown in Stage 1, but not T, in {0}?
        // Which of these cards was shown in Stage 1, but not T, in The Azure Button?
        [Question.AzureButtonNotT] = new()
        {
            QuestionText = "{0}のステージ1で表示されたT以外のカードに含まれるのは？",
            ModuleName = "空色ボタン",
        },
        // What was M in {0}?
        // What was M in The Azure Button?
        [Question.AzureButtonM] = new()
        {
            QuestionText = "{0}のMは？",
            ModuleName = "空色ボタン",
        },
        // What was the {1} direction in the decoy arrow in {0}?
        // What was the first direction in the decoy arrow in The Azure Button?
        [Question.AzureButtonDecoyArrowDirection] = new()
        {
            QuestionText = "{0}の囮の矢印が{1}番目に示した方向は？",
            ModuleName = "空色ボタン",
        },
        // What was the {1} direction in the {2} non-decoy arrow in {0}?
        // What was the first direction in the first non-decoy arrow in The Azure Button?
        [Question.AzureButtonNonDecoyArrowDirection] = new()
        {
            QuestionText = "{0}の囮ではない{2}番目の矢印が{1}番目に示した方向は？",
            ModuleName = "空色ボタン",
        },

        // Bakery
        // Which menu item was present in {0}?
        // Which menu item was present in Bakery?
        [Question.BakeryItems] = new()
        {
            QuestionText = "{0}で存在したメニュー商品は？",
            ModuleName = "クッキー屋",
        },

        // Bamboozled Again
        // What color was the {1} correct button in {0}?
        // What color was the first correct button in Bamboozled Again?
        [Question.BamboozledAgainButtonColor] = new()
        {
            QuestionText = "{0}の{1}番目に押した正しいボタンの色は？",
            ModuleName = "再錯綜",
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
        [Question.BamboozledAgainButtonText] = new()
        {
            QuestionText = "{0}の{1}番目に押した正しいボタンのテキストは？",
            ModuleName = "再錯綜",
        },
        // What was the {1} decrypted text on the display in {0}?
        // What was the first decrypted text on the display in Bamboozled Again?
        [Question.BamboozledAgainDisplayTexts1] = new()
        {
            QuestionText = "{0}のディスプレーで{1}番目の解読したテキストは？",
            ModuleName = "再錯綜",
        },
        // What was the {1} decrypted text on the display in {0}?
        // What was the first decrypted text on the display in Bamboozled Again?
        [Question.BamboozledAgainDisplayTexts2] = new()
        {
            QuestionText = "{0}のディスプレーで{1}番目の解読したテキストは？",
            ModuleName = "再錯綜",
        },
        // What color was the {1} text on the display in {0}?
        // What color was the first text on the display in Bamboozled Again?
        [Question.BamboozledAgainDisplayColor] = new()
        {
            QuestionText = "{0}のディスプレーで{1}番目のテキストの色は？",
            ModuleName = "再錯綜",
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
        [Question.BamboozlingButtonColor] = new()
        {
            QuestionText = "{0}のステージ{1}におけるボタンの色は？",
            ModuleName = "錯綜ボタン",
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
        [Question.BamboozlingButtonLabel] = new()
        {
            QuestionText = "{0}のステージ{1}における{2}のラベルは？",
            ModuleName = "錯綜ボタン",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["bottom"] = "下",
            },
        },
        // What was the {2} display in the {1} stage of {0}?
        // What was the first display in the first stage of Bamboozling Button?
        [Question.BamboozlingButtonDisplay] = new()
        {
            QuestionText = "{0}のステージ{1}における{2}番目の内容は？",
            ModuleName = "錯綜ボタン",
        },
        // What was the color of the {2} display in the {1} stage of {0}?
        // What was the color of the first display in the first stage of Bamboozling Button?
        [Question.BamboozlingButtonDisplayColor] = new()
        {
            QuestionText = "{0}のステージ{1}における{2}番目の内容の色は？",
            ModuleName = "錯綜ボタン",
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

        // Bar Charts
        // What was the category of {0}?
        // What was the category of Bar Charts?
        [Question.BarChartsCategory] = new()
        {
            QuestionText = "{0}のカテゴリは？",
        },
        // What was the color of the {1} bar in {0}?
        // What was the color of the first bar in Bar Charts?
        [Question.BarChartsColor] = new()
        {
            QuestionText = "{0}の{1}本目の棒は何色だった？",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Blue"] = "青",
            },
        },
        // What was the position of the {1} bar in {0}?
        // What was the position of the shortest bar in Bar Charts?
        [Question.BarChartsHeight] = new()
        {
            QuestionText = "{0}で{1}棒の位置は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["shortest"] = "最も短かった",
                ["second shortest"] = "二番目に短かった",
                ["second tallest"] = "二番目に長かった",
                ["tallest"] = "最も長かった",
            },
        },
        // What was the label of the {1} bar in {0}?
        // What was the label of the first bar in Bar Charts?
        [Question.BarChartsLabel] = new()
        {
            QuestionText = "{0}の{1}番目の棒のラベルは？",
        },
        // What was the unit of {0}?
        // What was the unit of Bar Charts?
        [Question.BarChartsUnit] = new()
        {
            QuestionText = "{0}の単位は？",
        },

        // Barcode Cipher
        // What was the screen number in {0}?
        // What was the screen number in Barcode Cipher?
        [Question.BarcodeCipherScreenNumber] = new()
        {
            QuestionText = "{0}の画面上の数字は？",
            ModuleName = "バーコード暗号",
        },
        // What was the edgework represented by the {1} barcode in {0}?
        // What was the edgework represented by the first barcode in Barcode Cipher?
        [Question.BarcodeCipherBarcodeEdgework] = new()
        {
            QuestionText = "{0}の{1}番目のバーコードが示していたエッジワークは？",
            ModuleName = "バーコード暗号",
            Answers = new Dictionary<string, string>
            {
                ["SERIAL NUMBER"] = "シリアルナンバー",
                ["BATTERIES"] = "バッテリー",
                ["BATTERY HOLDERS"] = "バッテリーホルダー",
                ["PORTS"] = "ポート",
                ["PORT PLATES"] = "ポートプレート",
                ["LIT INDICATORS"] = "点灯したインジケーター",
                ["UNLIT INDICATORS"] = "点灯していないインジケーター",
                ["INDICATORS"] = "インジケーター",
            },
        },
        // What was the answer for the {1} barcode in {0}?
        // What was the answer for the first barcode in Barcode Cipher?
        [Question.BarcodeCipherBarcodeAnswers] = new()
        {
            QuestionText = "{0}の{1}番目の回答は？",
            ModuleName = "バーコード暗号",
        },

        // Bartending
        // Which ingredient was in the {1} position on {0}?
        // Which ingredient was in the first position on Bartending?
        [Question.BartendingIngredients] = new()
        {
            QuestionText = "{0}で{1}番目の位置にあった材料は？",
            ModuleName = "バーテンディング",
            Answers = new Dictionary<string, string>
            {
                ["Adelhyde"] = "アデルハイド",
                ["Flanergide"] = "フラナガイド",
                ["Bronson Extract"] = "ブロンソンエキス",
                ["Karmotrine"] = "カルモトリン",
                ["Powdered Delta"] = "デルタパウダー",
            },
        },

        // Beans
        // What was this bean in {0}?
        // What was this bean in Beans?
        [Question.BeansColors] = new()
        {
            QuestionText = "{0}のこの豆はどんな豆だった？",
            ModuleName = "豆",
            Answers = new Dictionary<string, string>
            {
                ["Wobbly Orange"] = "揺れオレンジ",
                ["Wobbly Yellow"] = "揺れ黄",
                ["Wobbly Green"] = "揺れ緑",
                ["Not Wobbly Orange"] = "静止オレンジ",
                ["Not Wobbly Yellow"] = "静止黄",
                ["Not Wobbly Green"] = "静止緑",
            },
        },

        // Bean Sprouts
        // What was sprout {1} in {0}?
        // What was sprout 1 in Bean Sprouts?
        [Question.BeanSproutsColors] = new()
        {
            QuestionText = "{0}の{1}番目のもやしは？",
            ModuleName = "もやし",
            Answers = new Dictionary<string, string>
            {
                ["Raw"] = "生",
                ["Cooked"] = "調理済み",
                ["Burnt"] = "焦げている",
                ["Fake"] = "偽物",
            },
        },
        // What bean was on sprout {1} in {0}?
        // What bean was on sprout 1 in Bean Sprouts?
        [Question.BeanSproutsBeans] = new()
        {
            QuestionText = "{0}の{1}番目のもやしの豆は？",
            ModuleName = "もやし",
            Answers = new Dictionary<string, string>
            {
                ["Left"] = "左",
                ["Right"] = "右",
                ["None"] = "無し",
                ["Both"] = "両方",
            },
        },

        // Big Bean
        // What was the bean in {0}?
        // What was the bean in Big Bean?
        [Question.BigBeanColor] = new()
        {
            QuestionText = "{0}の豆の状態は？",
            ModuleName = "大きい豆",
            Answers = new Dictionary<string, string>
            {
                ["Wobbly Orange"] = "揺れオレンジ",
                ["Wobbly Yellow"] = "揺れ黄",
                ["Wobbly Green"] = "揺れ緑",
                ["Not Wobbly Orange"] = "静止オレンジ",
                ["Not Wobbly Yellow"] = "静止黄",
                ["Not Wobbly Green"] = "静止緑",
            },
        },

        // Big Circle
        // What color was {1} in the solution to {0}?
        // What color was first in the solution to Big Circle?
        [Question.BigCircleColors] = new()
        {
            QuestionText = "{0}の解法において{1}番目の色は？",
            ModuleName = "ビッグサークル",
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
        [Question.BinaryLEDsValue] = new()
        {
            QuestionText = "{0}でどの数字の時に正しいワイヤを切った？",
            ModuleName = "二進法LED",
        },

        // Binary Shift
        // What was the {1} initial number in {0}?
        // What was the top-left initial number in Binary Shift?
        [Question.BinaryShiftInitialNumber] = new()
        {
            QuestionText = "{0}の{1}の初期値は？",
            ModuleName = "二進数シフト",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-middle"] = "上",
                ["top-right"] = "右上",
                ["left-middle"] = "左",
                ["center"] = "中央",
                ["right-middle"] = "右",
                ["bottom-left"] = "左下",
                ["bottom-middle"] = "下",
                ["bottom-right"] = "右下",
            },
        },
        // What number was selected at stage {1} in {0}?
        // What number was selected at stage 0 in Binary Shift?
        [Question.BinaryShiftSelectedNumberPossition] = new()
        {
            QuestionText = "{0}でステージ{1}で選択した数字は？",
            ModuleName = "二進数シフト",
            Answers = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-middle"] = "上",
                ["top-right"] = "右上",
                ["left-middle"] = "左",
                ["center"] = "中央",
                ["right-middle"] = "右",
                ["bottom-left"] = "左下",
                ["bottom-middle"] = "下",
                ["bottom-right"] = "右下",
            },
        },
        // What number was not selected at stage {1} in {0}?
        // What number was not selected at stage 0 in Binary Shift?
        [Question.BinaryShiftNotSelectedNumberPossition] = new()
        {
            QuestionText = "{0}でステージ{1}で選択していない数字は？",
            ModuleName = "二進数シフト",
            Answers = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-middle"] = "上",
                ["top-right"] = "右上",
                ["left-middle"] = "左",
                ["center"] = "中央",
                ["right-middle"] = "右",
                ["bottom-left"] = "左下",
                ["bottom-middle"] = "下",
                ["bottom-right"] = "右下",
            },
        },

        // Binary
        // What word was displayed in {0}?
        // What word was displayed in Binary?
        [Question.BinaryWord] = new()
        {
            QuestionText = "{0}で表示された単語は？",
            ModuleName = "二進数",
        },

        // Bitmaps
        // How many pixels were {1} in the {2} quadrant in {0}?
        // How many pixels were white in the top left quadrant in Bitmaps?
        [Question.Bitmaps] = new()
        {
            QuestionText = "{0}で{2}の区域の{1}ピクセル数は？",
            ModuleName = "ビットマップ",
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
        [Question.BlackCipherScreen] = new()
        {
            QuestionText = "{0}の答えは？",
            ModuleName = "黒色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Blindfolded Yahtzee
        // What roll did the module claim in the {1} stage of {0}?
        // What roll did the module claim in the first stage of Blindfolded Yahtzee?
        [Question.BlindfoldedYahtzeeClaim] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What roll did the module claim in the {1} stage of {0}?",
            Answers = new Dictionary<string, string>
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

        // Blind Maze
        // What color was the {1} button in {0}?
        // What color was the north button in Blind Maze?
        [Question.BlindMazeColors] = new()
        {
            QuestionText = "{0}の{1}のボタンの色は？",
            ModuleName = "ブラインド迷路",
            FormatArgs = new Dictionary<string, string>
            {
                ["north"] = "北",
                ["east"] = "東",
                ["west"] = "西",
                ["south"] = "南",
            },
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Gray"] = "灰",
                ["Yellow"] = "黄",
            },
        },
        // Which maze did you solve {0} on?
        // Which maze did you solve Blind Maze on?
        [Question.BlindMazeMaze] = new()
        {
            QuestionText = "{0}で解除に使用した迷路は？",
            ModuleName = "ブラインド迷路",
        },

        // Blinking Notes
        // What song was flashed in {0}?
        // What song was flashed in Blinking Notes?
        [Question.BlinkingNotesSong] = new()
        {
            QuestionText = "{0}の点滅が再生した曲は？",
            ModuleName = "点滅音符",
        },

        // Blinkstop
        // How many times did the LED flash in {0}?
        // How many times did the LED flash in Blinkstop?
        [Question.BlinkstopNumberOfFlashes] = new()
        {
            QuestionText = "{0}のLEDが点滅した回数は？",
            ModuleName = "瞬きの停止",
        },
        // Which color did the LED flash the fewest times in {0}?
        // Which color did the LED flash the fewest times in Blinkstop?
        [Question.BlinkstopFewestFlashedColor] = new()
        {
            QuestionText = "{0}で最も点滅回数が少なかったのはどの色だった？",
            ModuleName = "瞬きの停止",
            Answers = new Dictionary<string, string>
            {
                ["Purple"] = "紫",
                ["Cyan"] = "シアン",
                ["Yellow"] = "黄",
                ["Multicolor"] = "虹色",
            },
        },

        // Blockbusters
        // What was the last letter pressed on {0}?
        // What was the last letter pressed on Blockbusters?
        [Question.BlockbustersLastLetter] = new()
        {
            QuestionText = "{0}で最後に押した文字は何？",
            ModuleName = "ブロックバスター",
        },

        // Blue Arrows
        // What were the characters on the screen in {0}?
        // What were the characters on the screen in Blue Arrows?
        [Question.BlueArrowsInitialCharacters] = new()
        {
            QuestionText = "{0}でスクリーンに表示された文字は何？",
            ModuleName = "青色矢印",
        },

        // The Blue Button
        // What was D in {0}?
        // What was D in The Blue Button?
        [Question.BlueButtonD] = new()
        {
            QuestionText = "{0}のDはどれだったか？",
            ModuleName = "青色ボタン",
        },
        // What was {1} in {0}?
        // What was E in The Blue Button?
        [Question.BlueButtonEFGH] = new()
        {
            QuestionText = "{0}のEはどれだったか？",
            ModuleName = "青色ボタン",
        },
        // What was M in {0}?
        // What was M in The Blue Button?
        [Question.BlueButtonM] = new()
        {
            QuestionText = "{0}のMはどれだったか？",
            ModuleName = "青色ボタン",
        },
        // What was N in {0}?
        // What was N in The Blue Button?
        [Question.BlueButtonN] = new()
        {
            QuestionText = "{0}のNはどれだったか？",
            ModuleName = "青色ボタン",
        },
        // What was P in {0}?
        // What was P in The Blue Button?
        [Question.BlueButtonP] = new()
        {
            QuestionText = "{0}のPはどれだったか？",
            ModuleName = "青色ボタン",
        },
        // What was Q in {0}?
        // What was Q in The Blue Button?
        [Question.BlueButtonQ] = new()
        {
            QuestionText = "{0}のQはどれだったか？",
            ModuleName = "青色ボタン",
            Answers = new Dictionary<string, string>
            {
                ["Blue"] = "青",
                ["Green"] = "緑",
                ["Cyan"] = "シアン",
                ["Red"] = "赤",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
            },
        },
        // What was X in {0}?
        // What was X in The Blue Button?
        [Question.BlueButtonX] = new()
        {
            QuestionText = "{0}のXはどれだったか？",
            ModuleName = "青色ボタン",
        },

        // Blue Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Blue Cipher?
        [Question.BlueCipherScreen] = new()
        {
            QuestionText = "{0}の答えは？",
            ModuleName = "青色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Bob Barks
        // What was the {1} indicator label in {0}?
        // What was the top left indicator label in Bob Barks?
        [Question.BobBarksIndicators] = new()
        {
            QuestionText = "{0}の{1}のインジケーターは？",
            ModuleName = "ボブの咆哮",
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
        [Question.BobBarksPositions] = new()
        {
            QuestionText = "{0}の{1}番目に点滅したボタンは？",
            ModuleName = "ボブの咆哮",
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
        [Question.BoggleLetters] = new()
        {
            QuestionText = "{0}で初めに表示された文字は？",
            ModuleName = "ボグル",
        },

        // Bomb Diffusal
        // What was the license number in {0}?
        // What was the license number in Bomb Diffusal?
        [Question.BombDiffusalLicenseNumber] = new()
        {
            QuestionText = "{0}のライセンス番号は？",
            ModuleName = "爆弾拡散",
        },

        // Bone Apple Tea
        // Which phrase was shown on {0}?
        // Which phrase was shown on Bone Apple Tea?
        [Question.BoneAppleTeaPhrase] = new()
        {
            QuestionText = "{0}で表示されたフレーズは？",
            ModuleName = "ボーンアップルティー",
        },

        // Boob Tube
        // Which word was shown on {0}?
        // Which word was shown on Boob Tube?
        [Question.BoobTubeWord] = new()
        {
            QuestionText = "{0}で表示された単語は？",
            ModuleName = "ブーブチューブ",
        },

        // Book of Mario
        // Who said the {1} quote in {0}?
        // Who said the first quote in Book of Mario?
        [Question.BookOfMarioPictures] = new()
        {
            QuestionText = "{0}の{1}番目の文章を発言したのは？",
            ModuleName = "ブック・オブ・マリオ",
        },
        // What did {1} say in the {2} stage of {0}?
        // What did Goombell say in the first stage of Book of Mario?
        [Question.BookOfMarioQuotes] = new()
        {
            QuestionText = "{0}のステージ{2}で{1}が発言した内容は？",
            ModuleName = "ブック・オブ・マリオ",
        },

        // Boolean Wires
        // Which operator did you submit in the {1} stage of {0}?
        // Which operator did you submit in the first stage of Boolean Wires?
        [Question.BooleanWiresEnteredOperators] = new()
        {
            QuestionText = "{0}のステージ{1}で送信した演算子は？",
            ModuleName = "論理ワイヤ",
        },

        // Boomtar the Great
        // What was rule {1} in {0}?
        // What was rule one in Boomtar the Great?
        [Question.BoomtarTheGreatRules] = new()
        {
            QuestionText = "{0}のルール{1}は？",
            ModuleName = "偉大なるブームター",
            FormatArgs = new Dictionary<string, string>
            {
                ["one"] = "1",
                ["two"] = "2",
            },
        },

        // Bordered Keys
        // What was the {1} key’s border color when it was pressed in {0}?
        // What was the first key’s border color when it was pressed in Bordered Keys?
        [Question.BorderedKeysBorderColor] = new()
        {
            QuestionText = "{0}で{1}番目の音板を押した時、縁は何色だった？",
            ModuleName = "境界音板",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
            },
        },
        // What was the digit displayed when the {1} key was pressed in {0}?
        // What was the digit displayed when the first key was pressed in Bordered Keys?
        [Question.BorderedKeysDigit] = new()
        {
            QuestionText = "{0}で{1}番目の音板を押した時、ディスプレーの数字は何だった？",
            ModuleName = "境界音板",
        },
        // What was the {1} key’s key color when it was pressed in {0}?
        // What was the first key’s key color when it was pressed in Bordered Keys?
        [Question.BorderedKeysKeyColor] = new()
        {
            QuestionText = "{0}で{1}番目の音板を押した時、音板は何色だった？",
            ModuleName = "境界音板",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
            },
        },
        // What was the {1} key’s label when it was pressed in {0}?
        // What was the first key’s label when it was pressed in Bordered Keys?
        [Question.BorderedKeysLabel] = new()
        {
            QuestionText = "{0}で{1}番目の音板を押した時、ラベルは何だった？",
            ModuleName = "境界音板",
        },
        // What was the {1} key’s label color when it was pressed in {0}?
        // What was the first key’s label color when it was pressed in Bordered Keys?
        [Question.BorderedKeysLabelColor] = new()
        {
            QuestionText = "{0}で{1}番目の音板を押した時、ラベルの色は何色だった？",
            ModuleName = "境界音板",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
            },
        },

        // Bottom Gear
        // What tweet was shown in {0}?
        // What tweet was shown in Bottom Gear?
        [Question.BottomGearTweet] = new()
        {
            QuestionText = "{0}に表示されたツイートは？",
        },

        // Boxing
        // Which {1} appeared on {0}?
        // Which contestant’s first name appeared on Boxing?
        [Question.BoxingNames] = new()
        {
            QuestionText = "{0}の{1}は？",
            ModuleName = "ボクシング",
            FormatArgs = new Dictionary<string, string>
            {
                ["contestant’s first name"] = "出場者の氏名",
                ["contestant’s last name"] = "出場者の姓名",
                ["substitute’s first name"] = "補欠選手の氏名",
                ["substitute’s last name"] = "補欠選手の姓名",
            },
        },
        // What was the {1} of the contestant with strength rating {2} on {0}?
        // What was the first name of the contestant with strength rating 0 on Boxing?
        [Question.BoxingContestantByStrength] = new()
        {
            QuestionText = "{0}で強さ{2}の出場者の{1}は？",
            ModuleName = "ボクシング",
            FormatArgs = new Dictionary<string, string>
            {
                ["first name"] = "氏名",
                ["last name"] = "姓名",
                ["substitute’s first name"] = "補欠選手の氏名",
                ["substitute’s last name"] = "補欠選手の姓名",
            },
        },
        // What was {1}’s strength rating on {0}?
        // What was Muhammad’s strength rating on Boxing?
        [Question.BoxingStrengthByContestant] = new()
        {
            QuestionText = "{0}で{1}のパンチ力は？",
            ModuleName = "ボクシング",
        },

        // Braille
        // What was the {1} pattern in {0}?
        // What was the first pattern in Braille?
        [Question.BraillePattern] = new()
        {
            QuestionText = "{0}の{1}番目のパターンは？",
            ModuleName = "点字",
        },

        // Breakfast Egg
        // Which color appeared on the egg in {0}?
        // Which color appeared on the egg in Breakfast Egg?
        [Question.BreakfastEggColor] = new()
        {
            QuestionText = "{0}の黄身の色は？",
            ModuleName = "目玉焼き",
            Answers = new Dictionary<string, string>
            {
                ["Crimson"] = "真紅",
                ["Orange"] = "オレンジ",
                ["Pink"] = "ピンク",
                ["Beige"] = "ベージュ",
                ["Cyan"] = "シアン",
                ["Lime"] = "ライム",
                ["Petrol"] = "濃青緑",
            },
        },

        // Broken Buttons
        // What was the {1} correct button you pressed in {0}?
        // What was the first correct button you pressed in Broken Buttons?
        [Question.BrokenButtons] = new()
        {
            QuestionText = "{0}で{1}番目に押したボタンはどれ？",
            ModuleName = "壊れたボタン",
        },

        // Broken Guitar Chords
        // What was the displayed chord in {0}?
        // What was the displayed chord in Broken Guitar Chords?
        [Question.BrokenGuitarChordsDisplayedChord] = new()
        {
            QuestionText = "{0}で表示されたコードは？",
            ModuleName = "壊れたギター・コード",
        },
        // In which position, from left to right, was the broken string in {0}?
        // In which position, from left to right, was the broken string in Broken Guitar Chords?
        [Question.BrokenGuitarChordsMutedString] = new()
        {
            QuestionText = "{0}の壊れた弦があったのは、左から数えて何番目？",
            ModuleName = "壊れたギター・コード",
        },

        // Brown Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Brown Cipher?
        [Question.BrownCipherScreen] = new()
        {
            QuestionText = "{0}の答えは？",
            ModuleName = "茶色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Brush Strokes
        // What was the color of the middle contact point in {0}?
        // What was the color of the middle contact point in Brush Strokes?
        [Question.BrushStrokesMiddleColor] = new()
        {
            QuestionText = "{0}の中央の接点の色は？",
            ModuleName = "ブラシストローク",
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
            QuestionText = "{0}でディスプレーの{1}番目の数字は？",
            ModuleName = "ハンバーガー警報",
        },
        // What was the {1} order number in {0}?
        // What was the first order number in Burger Alarm?
        [Question.BurgerAlarmOrderNumbers] = new()
        {
            QuestionText = "{0}の{1}番目の注文番号は？",
            ModuleName = "ハンバーガー警報",
        },

        // Burglar Alarm
        // What was the {1} displayed digit in {0}?
        // What was the first displayed digit in Burglar Alarm?
        [Question.BurglarAlarmDigits] = new()
        {
            QuestionText = "{0}で{1}番目に表示された数字は何？",
            ModuleName = "盗難警報",
        },

        // The Button
        // What color did the light glow in {0}?
        // What color did the light glow in The Button?
        [Question.ButtonLightColor] = new()
        {
            QuestionText = "{0}で光ったライトの色は？",
            ModuleName = "ボタン",
            Answers = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["blue"] = "青",
                ["yellow"] = "黄",
                ["white"] = "白",
            },
        },

        // Buttonage
        // How many {1} buttons were there on {0}?
        // How many red buttons were there on Buttonage?
        [Question.ButtonageButtons] = new()
        {
            QuestionText = "{0}に{1}ボタンは何個あった？",
            ModuleName = "大量ボタン",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤の",
                ["green"] = "緑の",
                ["orange"] = "オレンジの",
                ["blue"] = "青の",
                ["pink"] = "ピンクの",
                ["white"] = "白の",
                ["black"] = "黒の",
                ["white-bordered"] = "白の縁取り",
                ["pink-bordered"] = "ピンクの縁取り",
                ["gray-bordered"] = "グレーの縁取り",
                ["red-bordered"] = "赤の縁取り",
                ["“P”"] = "「P」の",
                ["special"] = "特殊",
            },
        },

        // Button Sequence
        // How many of the buttons in {0} were {1}?
        // How many of the buttons in Button Sequence were red?
        [Question.ButtonSequencesColorOccurrences] = new()
        {
            QuestionText = "{0}内の{1}色のボタンはいくつ？",
            ModuleName = "順番ボタン",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["blue"] = "青",
                ["yellow"] = "黄",
                ["white"] = "白",
            },
        },

        // Cacti’s Conundrum
        // What color was the LED in the {1} stage of {0}?
        // What color was the LED in the first stage of Cacti’s Conundrum?
        [Question.CactisConundrumColor] = new()
        {
            QuestionText = "{0}のステージ{1}におけるLEDの色は？",
            ModuleName = "サボテン難問",
            Answers = new Dictionary<string, string>
            {
                ["Blue"] = "青",
                ["Lime"] = "黄緑",
                ["Orange"] = "オレンジ",
                ["Red"] = "赤",
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
            QuestionText = "{0}のステージ{1}における上のディスプレーに表示された単語は？",
        },
        // What color was the text on the top display in the second stage of {0}?
        // What color was the text on the top display in the second stage of Caesar Psycho?
        [Question.CaesarPsychoScreenColor] = new()
        {
            QuestionText = "{0}のステージ2における上のディスプレーに表示された単語の色は？",
        },

        // Calendar
        // What was the LED color in {0}?
        // What was the LED color in Calendar?
        [Question.CalendarLedColor] = new()
        {
            QuestionText = "{0}のLEDの色は何？",
            ModuleName = "カレンダー",
            Answers = new Dictionary<string, string>
            {
                ["Green"] = "緑",
                ["Yellow"] = "黄",
                ["Red"] = "赤",
                ["Blue"] = "青",
            },
        },

        // CA-RPS
        // What color was this cell initially in {0}?
        // What color was this cell initially in CA-RPS?
        [Question.CARPSCell] = new()
        {
            QuestionText = "{0}のこのセルの初期色は？",
            ModuleName = "じゃんけんグリッド",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Black"] = "黒",
            },
        },

        // Cartinese
        // What color was the {1} button in {0}?
        // What color was the up button in Cartinese?
        [Question.CartineseButtonColors] = new()
        {
            QuestionText = "{0}の{1}のボタンの色は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["up"] = "上",
                ["right"] = "右",
                ["down"] = "下",
                ["left"] = "左",
            },
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Blue"] = "青",
            },
        },
        // What lyric was played by the {1} button in {0}?
        // What lyric was played by the up button in Cartinese?
        [Question.CartineseLyrics] = new()
        {
            QuestionText = "{0}の{1}のボタンから再生された歌詞は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["up"] = "上",
                ["right"] = "右",
                ["down"] = "下",
                ["left"] = "左",
            },
        },

        // Catchphrase
        // What was the colour of the {1} panel in {0}?
        // What was the colour of the top-left panel in Catchphrase?
        [Question.CatchphraseColour] = new()
        {
            QuestionText = "{0}の{1}のパネルの色は？",
            ModuleName = "キャッチフレーズ",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-right"] = "右上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Orange"] = "オレンジ",
                ["Purple"] = "紫",
                ["Yellow"] = "黄",
            },
        },

        // Challenge & Contact
        // What was the {1} submitted answer in {0}?
        // What was the first submitted answer in Challenge & Contact?
        [Question.ChallengeAndContactAnswers] = new()
        {
            QuestionText = "{0}の{1}番目に送信された回答は？",
            ModuleName = "チャレンジ＆コンタクト",
        },

        // Character Codes
        // What was the {1} character in {0}?
        // What was the first character in Character Codes?
        [Question.CharacterCodesCharacter] = new()
        {
            QuestionText = "{0}の{1}番目の文字は？",
            ModuleName = "文字コード",
        },

        // Character Shift
        // Which letter was present but not submitted on the left slider of {0}?
        // Which letter was present but not submitted on the left slider of Character Shift?
        [Question.CharacterShiftLetters] = new()
        {
            QuestionText = "{0}の左スライダーにあった送信していない英字は？",
            ModuleName = "文字シフト",
        },
        // Which digit was present but not submitted on the right slider of {0}?
        // Which digit was present but not submitted on the right slider of Character Shift?
        [Question.CharacterShiftDigits] = new()
        {
            QuestionText = "{0}の右スライダーにあった送信していない英字は？",
            ModuleName = "文字シフト",
        },

        // Character Slots
        // Who was displayed in the {1} slot in the {2} stage of {0}?
        // Who was displayed in the first slot in the first stage of Character Slots?
        [Question.CharacterSlotsDisplayedCharacters] = new()
        {
            QuestionText = "{0}でステージ{2}の{2}のスロットに表示されていたのは誰？",
        },

        // Cheap Checkout
        // What was {1} in {0}?
        // What was the paid amount in Cheap Checkout?
        [Question.CheapCheckoutPaid] = new()
        {
            QuestionText = "{0}の{1}は？",
            ModuleName = "安勘定",
            FormatArgs = new Dictionary<string, string>
            {
                ["the paid amount"] = "支払金額",
                ["the first paid amount"] = "最初の支払金額",
                ["the second paid amount"] = "二回目の支払金額",
            },
        },

        // Cheat Checkout
        // What was the cryptocurrency of {0}?
        // What was the cryptocurrency of Cheat Checkout?
        [Question.CheatCheckoutCurrency] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What was the cryptocurrency of {0}?",
        },
        // What was the hack method for the {1} hack of {0}?
        // What was the hack method for the first hack of Cheat Checkout?
        [Question.CheatCheckoutHack] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What was the hack method for the {1} hack of {0}?",
        },
        // What was the site for the {1} hack of {0}?
        // What was the site for the first hack of Cheat Checkout?
        [Question.CheatCheckoutSite] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What was the site for the {1} hack of {0}?",
        },

        // Cheep Checkout
        // Which bird {1} present in {0}?
        // Which bird was present in Cheep Checkout?
        [Question.CheepCheckoutBirds] = new()
        {
            QuestionText = "{0}に存在して{1}のは？",
            ModuleName = "鳥勘定",
            FormatArgs = new Dictionary<string, string>
            {
                ["was"] = "いた",
                ["was not"] = "いなかった",
            },
            Answers = new Dictionary<string, string>
            {
                ["Auklet"] = "ウミスズメ",
                ["Bluebird"] = "ルリツグミ",
                ["Chickadee"] = "アメリカコガラ",
                ["Dove"] = "ハト",
                ["Egret"] = "シラサギ",
                ["Finch"] = "フィンチ",
                ["Godwit"] = "オグロシギ",
                ["Hummingbird"] = "ハチドリ",
                ["Ibis"] = "トキ",
                ["Jay"] = "カケス",
                ["Kinglet"] = "キクイタダキ",
                ["Loon"] = "アビ",
                ["Magpie"] = "カササギ",
                ["Nuthatch"] = "ゴジュウカラ",
                ["Oriole"] = "アメリカムクドリモドキ",
                ["Pipit"] = "ビンズイ",
                ["Quail"] = "ウズラ",
                ["Raven"] = "ワタリガラス",
                ["Shrike"] = "モズ",
                ["Thrush"] = "ツグミ",
                ["Umbrellabird"] = "カサドリ",
                ["Vireo"] = "モズモドキ",
                ["Warbler"] = "ムシクイ",
                ["Xantus’s Hummingbird"] = "クロビタイサファイアハチドリ",
                ["Yellowlegs"] = "オオキアシシギ",
                ["Zigzag Heron"] = "コビトサギ",
            },
        },

        // Chess
        // What was the {1} coordinate in {0}?
        // What was the first coordinate in Chess?
        [Question.ChessCoordinate] = new()
        {
            QuestionText = "{0}の{1}番目の座標は何？",
            ModuleName = "チェス",
        },

        // Chinese Counting
        // What color was the {1} LED in {0}?
        // What color was the left LED in Chinese Counting?
        [Question.ChineseCountingLED] = new()
        {
            QuestionText = "{0}の{1}のLEDの色は何？",
            ModuleName = "中国の数え方",
            FormatArgs = new Dictionary<string, string>
            {
                ["left"] = "左",
                ["right"] = "右",
            },
            Answers = new Dictionary<string, string>
            {
                ["White"] = "白",
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Orange"] = "オレンジ",
            },
        },

        // Chinese Remainder Theorem
        // Which equation was used in {0}?
        // Which equation was used in Chinese Remainder Theorem?
        [Question.ChineseRemainderTheoremEquations] = new()
        {
            QuestionText = "{0}で使用された式は？",
        },

        // Chord Qualities
        // Which note was part of the given chord in {0}?
        // Which note was part of the given chord in Chord Qualities?
        [Question.ChordQualitiesNotes] = new()
        {
            QuestionText = "{0}で与えられたコードの一部にある音は何？",
            ModuleName = "コードクオリティー",
        },

        // ↻↺
        // Which arrow was shown in {0}?
        // Which arrow was shown in ↻↺?
        [Question.ClockCounterArrows] = new()
        {
            QuestionText = "{0}で表示された矢印は？",
        },

        // The Code
        // What was the displayed number in {0}?
        // What was the displayed number in The Code?
        [Question.CodeDisplayNumber] = new()
        {
            QuestionText = "{0}で表示された数字は何？",
            ModuleName = "コード",
        },

        // Codenames
        // Which of these words was submitted in {0}?
        // Which of these words was submitted in Codenames?
        [Question.CodenamesAnswers] = new()
        {
            QuestionText = "{0}で送信された単語に含まれるのは？",
            ModuleName = "コードネーム",
        },

        // Coffee Beans
        // What was the {1} movement in {0}?
        // What was the first movement in Coffee Beans?
        [Question.CoffeeBeansMovements] = new()
        {
            QuestionText = "{0}の{1}番目の動きは？",
            ModuleName = "コーヒー豆",
            Answers = new Dictionary<string, string>
            {
                ["Horizontal"] = "水平",
                ["Vertical"] = "垂直",
                ["Diagonal"] = "対角",
                ["Nothing"] = "無し",
            },
        },

        // Coffeebucks
        // What was the last served coffee in {0}?
        // What was the last served coffee in Coffeebucks?
        [Question.CoffeebucksCoffee] = new()
        {
            QuestionText = "{0}の最後に提供したコーヒーは？",
            ModuleName = "コーヒーバックス",
        },

        // Coinage
        // Which coin was flipped in {0}?
        // Which coin was flipped in Coinage?
        [Question.CoinageFlip] = new()
        {
            QuestionText = "{0}で裏返したコインは？",
            ModuleName = "大量コイン",
        },

        // Color Addition
        // What was {1}’s number in {0}?
        // What was red’s number in Color Addition?
        [Question.ColorAdditionNumbers] = new()
        {
            QuestionText = "{0}の{1}の数字は？",
            ModuleName = "色の加算",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
            },
        },

        // Color Braille
        // What color was this dot in {0}?
        // What color was this dot in Color Braille?
        [Question.ColorBrailleColor] = new()
        {
            QuestionText = "{0}のこの点の色は？",
            ModuleName = "色付き点字",
            Answers = new Dictionary<string, string>
            {
                ["Black"] = "黒",
                ["Blue"] = "青",
                ["Green"] = "緑",
                ["Cyan"] = "シアン",
                ["Red"] = "赤",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
                ["White"] = "白",
            },
        },

        // Color Decoding
        // What was the {1}-stage indicator pattern in {0}?
        // What was the first-stage indicator pattern in Color Decoding?
        [Question.ColorDecodingIndicatorPattern] = new()
        {
            QuestionText = "{0}でステージ{1}のインジケーターのパターンは？",
            ModuleName = "色の解読",
            Answers = new Dictionary<string, string>
            {
                ["Checkered"] = "チェック",
                ["Horizontal"] = "ボーダー",
                ["Vertical"] = "ストライプ",
                ["Solid"] = "一色",
            },
        },
        // Which color {1} in the {2}-stage indicator pattern in {0}?
        // Which color appeared in the first-stage indicator pattern in Color Decoding?
        [Question.ColorDecodingIndicatorColors] = new()
        {
            QuestionText = "{0}のステージ{2}で表示されて{1}色は？",
            ModuleName = "色の解読",
            FormatArgs = new Dictionary<string, string>
            {
                ["appeared"] = "いた",
                ["did not appear"] = "いなかった",
            },
            Answers = new Dictionary<string, string>
            {
                ["Green"] = "緑",
                ["Purple"] = "紫",
                ["Red"] = "赤",
                ["Blue"] = "青",
                ["Yellow"] = "黄",
            },
        },

        // Colored Keys
        // What was the displayed word in {0}?
        // What was the displayed word in Colored Keys?
        [Question.ColoredKeysDisplayWord] = new()
        {
            QuestionText = "{0}で表示された単語は？",
            ModuleName = "色付きキーパッド",
            Answers = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["blue"] = "青",
                ["green"] = "緑",
                ["yellow"] = "黄",
                ["purple"] = "紫",
                ["white"] = "白",
            },
        },
        // What was the displayed word’s color in {0}?
        // What was the displayed word’s color in Colored Keys?
        [Question.ColoredKeysDisplayWordColor] = new()
        {
            QuestionText = "{0}で表示された単語の色は？",
            ModuleName = "色付きキーパッド",
            Answers = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["blue"] = "青",
                ["green"] = "緑",
                ["yellow"] = "黄",
                ["purple"] = "紫",
                ["white"] = "白",
            },
        },
        // What was the color of the {1} key in {0}?
        // What was the color of the top-left key in Colored Keys?
        [Question.ColoredKeysKeyColor] = new()
        {
            QuestionText = "{0}の{1}のキーの色は？",
            ModuleName = "色付きキーパッド",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-right"] = "右上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
            Answers = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["blue"] = "青",
                ["green"] = "緑",
                ["yellow"] = "黄",
                ["purple"] = "紫",
                ["white"] = "白",
            },
        },
        // What letter was on the {1} key in {0}?
        // What letter was on the top-left key in Colored Keys?
        [Question.ColoredKeysKeyLetter] = new()
        {
            QuestionText = "{0}の{1}のキーパッドの文字は？",
            ModuleName = "色付きキーパッド",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-right"] = "右上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
        },

        // Colored Squares
        // What was the first color group in {0}?
        // What was the first color group in Colored Squares?
        [Question.ColoredSquaresFirstGroup] = new()
        {
            QuestionText = "{0}の最初の色グループは？",
            ModuleName = "色付き格子",
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
        [Question.ColoredSwitchesInitialPosition] = new()
        {
            QuestionText = "{0}の初期配置は？",
            ModuleName = "色付きスイッチ",
        },
        // What was the position of the switches when the LEDs came on in {0}?
        // What was the position of the switches when the LEDs came on in Colored Switches?
        [Question.ColoredSwitchesWhenLEDsCameOn] = new()
        {
            QuestionText = "{0}のLEDが示したスイッチの位置は？",
            ModuleName = "色付きスイッチ",
        },

        // Color Morse
        // What was the color of the {1} LED in {0}?
        // What was the color of the first LED in Color Morse?
        [Question.ColorMorseColor] = new()
        {
            QuestionText = "{0}の{1}番目のLEDは何色？",
            ModuleName = "カラーモールス",
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
        [Question.ColorMorseCharacter] = new()
        {
            QuestionText = "{0}の{1}番目のLEDが示す文字は？",
            ModuleName = "カラーモールス",
        },

        // Color One Two
        // What color was the {1} LED in {0}?
        // What color was the left LED in Color One Two?
        [Question.ColorOneTwoColor] = new()
        {
            QuestionText = "{0}の{1}側のLEDの色は？",
            ModuleName = "色の1と2",
            FormatArgs = new Dictionary<string, string>
            {
                ["left"] = "左",
                ["right"] = "右",
            },
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Blue"] = "青",
                ["Green"] = "緑",
                ["Yellow"] = "黄",
            },
        },

        // Colors Maximization
        // How many buttons were {1} in {0}?
        // How many buttons were red in Colors Maximization?
        [Question.ColorsMaximizationColorCount] = new()
        {
            QuestionText = "{0}で{1}色だったボタンは何個？",
            ModuleName = "最大色",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
            },
        },

        // Coloured Cubes
        // What was the colour of this {1} in the {2} stage of {0}?
        // What was the colour of this cube in the first stage of Coloured Cubes?
        [Question.ColouredCubesColours] = new()
        {
            QuestionText = "{0}のステージ{2}におけるこの{1}の色は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["cube"] = "キューブ",
                ["stage light"] = "ステータスライト",
            },
            Answers = new Dictionary<string, string>
            {
                ["Black"] = "黒",
                ["Indigo"] = "藍",
                ["Blue"] = "青",
                ["Forest"] = "深緑",
                ["Teal"] = "青緑",
                ["Azure"] = "空",
                ["Green"] = "緑",
                ["Jade"] = "翡翠",
                ["Cyan"] = "シアン",
                ["Maroon"] = "栗",
                ["Plum"] = "梅",
                ["Violet"] = "紫",
                ["Olive"] = "オリーブ",
                ["Grey"] = "灰",
                ["Maya"] = "マヤブルー",
                ["Lime"] = "黄緑",
                ["Mint"] = "若緑",
                ["Aqua"] = "水色",
                ["Red"] = "赤",
                ["Rose"] = "薔薇",
                ["Magenta"] = "マゼンタ",
                ["Orange"] = "オレンジ",
                ["Salmon"] = "サーモンピンク",
                ["Pink"] = "ピンク",
                ["Yellow"] = "黄",
                ["Cream"] = "クリーム",
                ["White"] = "白",
            },
        },

        // Coloured Cylinder
        // What was the {1} colour flashed on the cylinder in {0}?
        // What was the first colour flashed on the cylinder in Coloured Cylinder?
        [Question.ColouredCylinderColours] = new()
        {
            QuestionText = "{0}でシリンダーが{1}番目に光った色は？",
            ModuleName = "色付きシリンダー",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Yellow"] = "黄",
                ["Magenta"] = "マゼンタ",
                ["White"] = "白",
                ["Black"] = "黒",
            },
        },

        // Colour Flash
        // What was the color of the last word in the sequence in {0}?
        // What was the color of the last word in the sequence in Colour Flash?
        [Question.ColourFlashLastColor] = new()
        {
            QuestionText = "{0}のシーケンスの最後の単語は何色？",
            ModuleName = "カラーフラッシュ",
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

        // Concentration
        // What number began here in {0}?
        // What number began here in Concentration?
        [Question.ConcentrationStartingDigit] = new()
        {
            QuestionText = "{0}で、初期状態でこの場所にあった数字は？",
            ModuleName = "集中",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Concentration which began with {1} in the {0} position (in reading order)"] = "初期状態で読み順で{0}番目に{1}があった集中",
            },
        },

        // Conditional Buttons
        // What was the color of this button in {0}?
        // What was the color of this button in Conditional Buttons?
        [Question.ConditionalButtonsColors] = new()
        {
            QuestionText = "{0}のこのボタンの色は？",
            ModuleName = "条件ボタン",
            Answers = new Dictionary<string, string>
            {
                ["black"] = "黒",
                ["blue"] = "青",
                ["dark green"] = "深緑",
                ["light green"] = "黄緑",
                ["orange"] = "オレンジ",
                ["pink"] = "ピンク",
                ["purple"] = "紫",
                ["red"] = "赤",
                ["white"] = "白",
                ["yellow"] = "黄",
            },
        },

        // Connected Monitors
        // What number was initially displayed on this screen in {0}?
        // What number was initially displayed on this screen in Connected Monitors?
        [Question.ConnectedMonitorsNumber] = new()
        {
            QuestionText = "{0}のこの画面に最初表示された数字は？",
        },
        // What colour was the indicator on this screen in {0}?
        // What colour was the indicator on this screen in Connected Monitors?
        [Question.ConnectedMonitorsSingleIndicator] = new()
        {
            QuestionText = "{0}のこの画面にあったインジケーターの色は？",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Orange"] = "オレンジ",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Purple"] = "紫",
                ["White"] = "白",
            },
        },
        // What colour was the {1} indicator on this screen in {0}?
        // What colour was the first indicator on this screen in Connected Monitors?
        [Question.ConnectedMonitorsOrdinalIndicator] = new()
        {
            QuestionText = "{0}のこの画面にあったインジケーター{1}の色は？",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Orange"] = "オレンジ",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Purple"] = "紫",
                ["White"] = "白",
            },
        },

        // Connection Check
        // What pair of numbers was present in {0}?
        // What pair of numbers was present in Connection Check?
        [Question.ConnectionCheckNumbers] = new()
        {
            NeedsTranslation = true,
            QuestionText = "{0}内に存在していたペアは？",
            ModuleName = "接続確認",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Connection Check with no {0}s"] = "the Connection Check with no {0}s",
                ["the Connection Check with one {0}"] = "the Connection Check with one {0}",
                ["the Connection Check with two {0}s"] = "the Connection Check with two {0}s",
                ["the Connection Check with three {0}s"] = "the Connection Check with three {0}s",
                ["the Connection Check with four {0}s"] = "the Connection Check with four {0}s",
            },
        },

        // Coordinates
        // What was the solution you selected first in {0}?
        // What was the solution you selected first in Coordinates?
        [Question.CoordinatesFirstSolution] = new()
        {
            QuestionText = "{0}で最初に選んだ回答は？",
            ModuleName = "座標",
        },
        // What was the grid size in {0}?
        // What was the grid size in Coordinates?
        [Question.CoordinatesSize] = new()
        {
            QuestionText = "{0}のグリッドのサイズは？",
            ModuleName = "座標",
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
            QuestionText = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
            ModuleName = "珊瑚色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Corners
        // What was the color of the {1} corner in {0}?
        // What was the color of the top-left corner in Corners?
        [Question.CornersColors] = new()
        {
            QuestionText = "{0}の{1}の角は何色？",
            ModuleName = "コーナー",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-right"] = "右上",
                ["bottom-right"] = "右下",
                ["bottom-left"] = "左下",
            },
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
        [Question.CornersColorCount] = new()
        {
            QuestionText = "{0}の{1}色の角はいくつ？",
            ModuleName = "コーナー",
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
        [Question.CornflowerCipherScreen] = new()
        {
            QuestionText = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Cosmic
        // What was the number initially shown in {0}?
        // What was the number initially shown in Cosmic?
        [Question.CosmicNumber] = new()
        {
            QuestionText = "{0}の最初に表示された番号は？",
            ModuleName = "宇宙",
        },

        // Crazy Hamburger
        // What was the {1} ingredient shown in {0}?
        // What was the first ingredient shown in Crazy Hamburger?
        [Question.CrazyHamburgerIngredient] = new()
        {
            QuestionText = "{0}の{1}番目の材料は？",
            ModuleName = "クレイジーハンバーガー",
        },

        // Crazy Maze
        // What was the {1} location in {0}?
        // What was the starting location in Crazy Maze?
        [Question.CrazyMazeStartOrGoal] = new()
        {
            QuestionText = "{0}の{1}位置は？",
            ModuleName = "クレイジー迷路",
            FormatArgs = new Dictionary<string, string>
            {
                ["starting"] = "開始",
                ["goal"] = "ゴール",
            },
        },

        // Cream Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Cream Cipher?
        [Question.CreamCipherScreen] = new()
        {
            QuestionText = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
            ModuleName = "鳥子色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Creation
        // What were the weather conditions on the {1} day in {0}?
        // What were the weather conditions on the first day in Creation?
        [Question.CreationWeather] = new()
        {
            QuestionText = "{0}の{1}日目における天気は？",
            ModuleName = "クリエーション",
            Answers = new Dictionary<string, string>
            {
                ["Clear"] = "晴れ",
                ["Heat Wave"] = "猛暑",
                ["Meteor Shower"] = "流星群",
                ["Rain"] = "雨",
                ["Windy"] = "強風",
            },
        },

        // Crimson Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Crimson Cipher?
        [Question.CrimsonCipherScreen] = new()
        {
            QuestionText = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
            ModuleName = "紅色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Critters
        // What was the color in {0}?
        // What was the color in Critters?
        [Question.CrittersColor] = new()
        {
            QuestionText = "{0}で使用した変化した色は？",
            Answers = new Dictionary<string, string>
            {
                ["Yellow"] = "黄",
                ["Pink"] = "ピンク",
                ["Blue"] = "青",
                ["White"] = "白",
            },
        },

        // Cruel Binary
        // What was the displayed word in {0}?
        // What was the displayed word in Cruel Binary?
        [Question.CruelBinaryDisplayedWord] = new()
        {
            QuestionText = "{0}で表示された単語は？",
            ModuleName = "残忍二進数",
        },

        // Cruel Keypads
        // Which of these characters appeared in the {1} stage of {0}?
        // Which of these characters appeared in the first stage of Cruel Keypads?
        [Question.CruelKeypadsDisplayedSymbols] = new()
        {
            QuestionText = "{0}のステージ{1}で表示された文字に含まれるのは？",
            ModuleName = "残忍キーパッド",
        },
        // What was the color of the bar in the {1} stage of {0}?
        // What was the color of the bar in the first stage of Cruel Keypads?
        [Question.CruelKeypadsColors] = new()
        {
            QuestionText = "{0}のステージ{1}におけるバーの色は？",
            ModuleName = "残忍キーパッド",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Blue"] = "青",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Magenta"] = "マゼンタ",
                ["White"] = "白",
            },
        },

        // The cRule
        // Which cell was pre-filled at the start of {0}?
        // Which cell was pre-filled at the start of The cRule?
        [Question.CRulePrefilled] = new()
        {
            QuestionText = "{0}の開始時点でどのセルが予め埋められていた？",
        },
        // Which symbol pair was here in {0}?
        // Which symbol pair was here in The cRule?
        [Question.CRuleSymbolPair] = new()
        {
            QuestionText = "{0}のこの位置にあったシンボルのペアは？",
        },
        // Which symbol pair was present on {0}?
        // Which symbol pair was present on The cRule?
        [Question.CRuleSymbolPairPresent] = new()
        {
            QuestionText = "{0}に存在していたシンボルのペアは？",
        },
        // Where was {1} in {0}?
        // Where was ♤♤ in The cRule?
        [Question.CRuleSymbolPairCell] = new()
        {
            QuestionText = "{0}で{1}はどこにあった？",
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
            QuestionText = "{0}で{1}のキーパッドのラベルは？",
            ModuleName = "暗号化キーパッド",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-right"] = "右上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
        },
        // Which cardinal direction was the {1} key rotated to in {0}?
        // Which cardinal direction was the top-left key rotated to in Cryptic Keypad?
        [Question.CrypticKeypadRotations] = new()
        {
            QuestionText = "{0}で{1}のキーパッドの回転方向は？",
            ModuleName = "暗号化キーパッド",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-right"] = "右上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
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
        [Question.CubeRotations] = new()
        {
            QuestionText = "{0}の{1}回目のキューブの回転は？",
            ModuleName = "キューブ",
            Answers = new Dictionary<string, string>
            {
                ["rotate cw"] = "時計回り",
                ["tip left"] = "左回転",
                ["tip backwards"] = "上回転",
                ["rotate ccw"] = "反時計回り",
                ["tip right"] = "右回転",
                ["tip forwards"] = "下回転",
            },
        },

        // Cursed Double-Oh
        // What was the first digit of the initially displayed number in {0}?
        // What was the first digit of the initially displayed number in Cursed Double-Oh?
        [Question.CursedDoubleOhInitialPosition] = new()
        {
            QuestionText = "{0}の初期状態に表示されていた上一桁は？",
            ModuleName = "呪いのダブル・オー",
        },

        // Customer Identification
        // Who was the {1} customer in {0}?
        // Who was the first customer in Customer Identification?
        [Question.CustomerIdentificationCustomer] = new()
        {
            QuestionText = "{0}の{1}番目のお客さんは誰？",
            ModuleName = "顧客識別",
        },

        // The Cyan Button
        // Where was the button at the {1} stage in {0}?
        // Where was the button at the first stage in The Cyan Button?
        [Question.CyanButtonPositions] = new()
        {
            QuestionText = "{0}のステージ{1}のボタンはどこにあった？",
            ModuleName = "シアンボタン",
            Answers = new Dictionary<string, string>
            {
                ["top left"] = "左上",
                ["top middle"] = "上",
                ["top right"] = "右",
                ["bottom left"] = "左下",
                ["bottom middle"] = "下",
                ["bottom right"] = "右下",
            },
        },

        // DACH Maze
        // Which region did you depart from in {0}?
        // Which region did you depart from in DACH Maze?
        [Question.DACHMazeOrigin] = new()
        {
            QuestionText = "{0}の出発点は？",
            ModuleName = "DACH迷路",
            Answers = new Dictionary<string, string>
            {
                ["Burgenland, A"] = "ブルゲンラント(A)",
                ["Carinthia, A"] = "ケルンテン(A)",
                ["Lower Austria, A"] = " ニーダーエステライヒ(A)",
                ["North Tyrol, A"] = "北チロル(A)",
                ["Upper Austria, A"] = "オーバーエステライヒ(A)",
                ["East Tyrol, A"] = "東チロル(A)",
                ["Salzburg, A"] = "ザルツブルク(A)",
                ["Styria, A"] = "シュタイアーマルク(A)",
                ["Vorarlberg, A"] = "フォアアールベルク(A)",
                ["Vienna, A"] = "ウィーン(A)",
                ["Aargau, CH"] = "アールガウ(CH)",
                ["Appenzell Inner Rhodes, CH"] = "アッペンツェル・インナーローデン(CH)",
                ["Appenzell Outer Rhodes, CH"] = "アッペンツェル・アウサーローデン(CH)",
                ["Basel Country, CH"] = "バーゼル＝ラント(CH)",
                ["Bern, CH"] = "ベルン(CH)",
                ["Basel City, CH"] = "バーゼル＝シュタット(CH)",
                ["Fribourg, CH"] = "フリブール(CH)",
                ["Geneva, CH"] = "ジュネーヴ(CH)",
                ["Glarus, CH"] = "グラールス(CH)",
                ["Grisons, CH"] = "グラウビュンデン(CH)",
                ["Jura, CH"] = "ジュラ(CH)",
                ["Luzern, CH"] = "ルツェルン(CH)",
                ["Nidwalden, CH"] = "ヌーシャテル(CH)",
                ["Neuchâtel, CH"] = "ニトヴァルデン(CH)",
                ["Obwalden, CH"] = "オプヴァルデン(CH)",
                ["Schaffhausen, CH"] = "ザンクト・ガレン(CH)",
                ["St. Gallen, CH"] = "シャフハウゼン(CH)",
                ["Solothurn, CH"] = "ゾロトゥルン(CH)",
                ["Schwyz, CH"] = "シュヴィーツ(CH)",
                ["Thurgau, CH"] = "トゥールガウ(CH)",
                ["Ticino, CH"] = "ティチーノ(CH)",
                ["Uri, CH"] = "ウーリ(CH)",
                ["Vaud, CH"] = "ヴォー(CH)",
                ["Valais, CH"] = "ヴァレー(CH)",
                ["Zug, CH"] = "ツーク(CH)",
                ["Zürich, CH"] = "チューリヒ(CH)",
                ["Brandenburg, D"] = "ブランデンブルク(D)",
                ["Berlin, D"] = "ベルリン(D)",
                ["Baden-Württemberg, D"] = "バーデン＝ヴュルテンベルク(D)",
                ["Bavaria, D"] = "バイエルン(D)",
                ["Bremen, D"] = "ブレーメン(D)",
                ["Hesse, D"] = "ヘッセン(D)",
                ["Hamburg, D"] = "ハンブルク(D)",
                ["Mecklenburg-Vorpommern, D"] = "メクレンブルク＝フォアポンメルン(D)",
                ["Lower Saxony, D"] = "ニーダーザクセン(D)",
                ["North Rhine-Westphalia, D"] = "ノルトライン＝ヴェストファーレン(D)",
                ["Rhineland-Palatinate, D"] = "ラインラント＝プファルツ(D)",
                ["Schleswig-Holstein, D"] = "シュレースヴィヒ＝ホルシュタイン(D)",
                ["Saarland, D"] = "ザールラント(D)",
                ["Saxony, D"] = "ザクセン(D)",
                ["Saxony-Anhalt, D"] = "ザクセン＝アンハルト(D)",
                ["Thuringia, D"] = "テューリンゲン(D)",
                ["Liechtenstein"] = "リヒテンシュタイン",
            },
        },

        // Deaf Alley
        // What was the shape generated in {0}?
        // What was the shape generated in Deaf Alley?
        [Question.DeafAlleyShape] = new()
        {
            QuestionText = "{0}で生成された文字は？",
            ModuleName = "デフ・アレイ",
        },

        // The Deck of Many Things
        // What deck did the first card of {0} belong to?
        // What deck did the first card of The Deck of Many Things belong to?
        [Question.DeckOfManyThingsFirstCard] = new()
        {
            QuestionText = "{0}の最初のカードが属していたデッキは？",
            ModuleName = "多種デッキ",
        },

        // Decolored Squares
        // What was the starting {1} defining color in {0}?
        // What was the starting column defining color in Decolored Squares?
        [Question.DecoloredSquaresStartingPos] = new()
        {
            QuestionText = "{0}の開始位置の{1}は何色？",
            ModuleName = "色抜き格子",
            FormatArgs = new Dictionary<string, string>
            {
                ["column"] = "列",
                ["row"] = "段",
            },
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

        // Decolour Flash
        // What was the {1} of the {2} goal in {0}?
        // What was the colour of the first goal in Decolour Flash?
        [Question.DecolourFlashGoal] = new()
        {
            QuestionText = "{0}で{2}番目のゴールの{1}は？",
            ModuleName = "デカラーフラッシュ",
            FormatArgs = new Dictionary<string, string>
            {
                ["colour"] = "色",
                ["word"] = "単語",
            },
            Answers = new Dictionary<string, string>
            {
                ["Blue"] = "青",
                ["Green"] = "緑",
                ["Red"] = "赤",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
                ["White"] = "白",
            },
        },

        // Denial Displays
        // What number was initially shown on display {1} in {0}?
        // What number was initially shown on display A in Denial Displays?
        [Question.DenialDisplaysDisplays] = new()
        {
            QuestionText = "{0}のディスプレー{1}に最初表示されていた数字は？",
        },

        // DetoNATO
        // What was the {1} display in {0}?
        // What was the first display in DetoNATO?
        [Question.DetoNATODisplay] = new()
        {
            QuestionText = "{0}で{1}番目に表示されていた内容は？",
            ModuleName = "デトナト",
        },

        // Devilish Eggs
        // What was the {1} egg’s {2} rotation in {0}?
        // What was the top egg’s first rotation in Devilish Eggs?
        [Question.DevilishEggsRotations] = new()
        {
            QuestionText = "{0}で{1}の卵の{2}回目の回転は？",
            ModuleName = "悪魔の卵",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["bottom"] = "下",
            },
        },
        // What was the {1} digit in the string of numbers on {0}?
        // What was the first digit in the string of numbers on Devilish Eggs?
        [Question.DevilishEggsNumbers] = new()
        {
            QuestionText = "{0}の数列の{1}桁目は？",
            ModuleName = "悪魔の卵",
        },
        // What was the {1} letter in the string of letters on {0}?
        // What was the first letter in the string of letters on Devilish Eggs?
        [Question.DevilishEggsLetters] = new()
        {
            QuestionText = "{0}の英字文字列の{1}文字目は？",
            ModuleName = "悪魔の卵",
        },

        // Dialtones
        // What dialtones were heard in {0}?
        // What dialtones were heard in Dialtones?
        [Question.DialtonesDialtones] = new()
        {
            QuestionText = "{0}で聞こえたダイヤルトーンは？",
            ModuleName = "ダイヤル音声",
        },

        // Digisibility
        // What was the number on the {1} button in {0}?
        // What was the number on the first button in Digisibility?
        [Question.DigisibilityDisplayedNumber] = new()
        {
            QuestionText = "{0}の{1}番目のボタンに書かれた数字は？",
        },

        // Digit String
        // What was the initial number in {0}?
        // What was the initial number in Digit String?
        [Question.DigitStringInitialNumber] = new()
        {
            QuestionText = "{0}の初期値は？",
            ModuleName = "数字列",
        },

        // Dimension Disruption
        // Which of these was a visible character in {0}?
        // Which of these was a visible character in Dimension Disruption?
        [Question.DimensionDisruptionVisibleLetters] = new()
        {
            QuestionText = "{0}で見えていた文字は次のうちどれ？",
            ModuleName = "次元破壊",
        },

        // Directional Button
        // How many times did you press the button in the {1} stage of {0}?
        // How many times did you press the button in the first stage of Directional Button?
        [Question.DirectionalButtonButtonCount] = new()
        {
            QuestionText = "{0}のステージ{1}で押したボタンの回数は？",
            ModuleName = "方向ボタン",
        },

        // Discolored Squares
        // What was {1}’s remembered position in {0}?
        // What was Blue’s remembered position in Discolored Squares?
        [Question.DiscoloredSquaresRememberedPositions] = new()
        {
            QuestionText = "{0}で{1}の覚えた位置は？",
            ModuleName = "色変え格子",
            FormatArgs = new Dictionary<string, string>
            {
                ["Blue"] = "青",
                ["Red"] = "赤",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Magenta"] = "マゼンタ",
            },
        },

        // Disordered Keys
        // What was the missing information for the {1} key in {0}?
        // What was the missing information for the first key in Disordered Keys?
        [Question.DisorderedKeysMissingInfo] = new()
        {
            QuestionText = "{0}の{1}番目の音板に欠けていた情報は？",
            ModuleName = "欠陥順番音板",
            Answers = new Dictionary<string, string>
            {
                ["Key color"] = "音板の色",
                ["Label color"] = "ラベルの色",
                ["Label"] = "ラベル",
            },
        },
        // What was the revealed key color for the {1} key in {0}?
        // What was the revealed key color for the first key in Disordered Keys?
        [Question.DisorderedKeysRevealedKeyColor] = new()
        {
            QuestionText = "{0}の{1}番目の完全な音板の色は？",
            ModuleName = "欠陥順番音板",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
            },
        },
        // What was the revealed label for the {1} key in {0}?
        // What was the revealed label for the first key in Disordered Keys?
        [Question.DisorderedKeysRevealedLabel] = new()
        {
            QuestionText = "{0}の{1}番目の完全な音板のラベルは？",
            ModuleName = "欠陥順番音板",
        },
        // What was the revealed label color for the {1} key in {0}?
        // What was the revealed label color for the first key in Disordered Keys?
        [Question.DisorderedKeysRevealedLabelColor] = new()
        {
            QuestionText = "{0}の{1}番目の完全な音板のラベルの色は？",
            ModuleName = "欠陥順番音板",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
            },
        },
        // What was the unrevealed key color for the {1} key in {0}?
        // What was the unrevealed key color for the first key in Disordered Keys?
        [Question.DisorderedKeysUnrevealedKeyColor] = new()
        {
            QuestionText = "{0}の{1}番目の不完全な音板のラベルの色は？",
            ModuleName = "欠陥順番音板",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
            },
        },
        // What was the unrevealed label for the {1} key in {0}?
        // What was the unrevealed label for the first key in Disordered Keys?
        [Question.DisorderedKeysUnrevealedKeyLabel] = new()
        {
            QuestionText = "{0}の{1}番目の不完全な音板のラベルは？",
            ModuleName = "欠陥順番音板",
        },
        // What was the unrevealed label color for the {1} key in {0}?
        // What was the unrevealed label color for the first key in Disordered Keys?
        [Question.DisorderedKeysUnrevealedLabelColor] = new()
        {
            QuestionText = "{0}の{1}番目の不完全な音板のラベルの色は？",
            ModuleName = "欠陥順番音板",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
            },
        },

        // Divided Squares
        // What color was {1} while pressing it in {0}?
        // What color was the square while pressing it in Divided Squares?
        [Question.DividedSquaresColor] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What color was {1} while pressing it in {0}?",
            FormatArgs = new Dictionary<string, string>
            {
                ["the square"] = "the square",
                ["the correct square"] = "the correct square",
            },
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "Red",
                ["Yellow"] = "Yellow",
                ["Green"] = "Green",
                ["Blue"] = "Blue",
                ["Black"] = "Black",
                ["White"] = "White",
            },
        },

        // Divisible Numbers
        // What was the {1} stage’s number in {0}?
        // What was the first stage’s number in Divisible Numbers?
        [Question.DivisibleNumbersNumbers] = new()
        {
            QuestionText = "{0}でのステージ{1}の数字は？",
            ModuleName = "割り切れる数字",
        },

        // Doofenshmirtz Evil Inc.
        // What jingle played in {0}?
        // What jingle played in Doofenshmirtz Evil Inc.?
        [Question.DoofenshmirtzEvilIncJingles] = new()
        {
            QuestionText = "{0}で流れたジングルは？",
        },
        // Which image was shown in {0}?
        // Which image was shown in Doofenshmirtz Evil Inc.?
        [Question.DoofenshmirtzEvilIncInators] = new()
        {
            QuestionText = "{0}で表示された画像は？",
        },

        // Double Arrows
        // What was the starting position in {0}?
        // What was the starting position in Double Arrows?
        [Question.DoubleArrowsStart] = new()
        {
            QuestionText = "{0}の開始位置は？",
            ModuleName = "ダブル矢印",
        },
        // Which {1} arrow moved {2} in the grid in {0}?
        // Which inner arrow moved up in the grid in Double Arrows?
        [Question.DoubleArrowsArrow] = new()
        {
            QuestionText = "{0}で{2}に移動する{1}の矢印はどれ？",
            ModuleName = "ダブル矢印",
            FormatArgs = new Dictionary<string, string>
            {
                ["inner"] = "内側",
                ["up"] = "上",
                ["outer"] = "外側",
                ["down"] = "下",
                ["left"] = "左",
                ["right"] = "右",
            },
            Answers = new Dictionary<string, string>
            {
                ["Up"] = "上",
                ["Right"] = "右",
                ["Left"] = "左",
                ["Down"] = "下",
            },
        },
        // Which direction in the grid did the {1} arrow move in {0}?
        // Which direction in the grid did the inner up arrow move in Double Arrows?
        [Question.DoubleArrowsMovement] = new()
        {
            QuestionText = "{0}で{1}矢印を押すとどの方向に進んだ？",
            ModuleName = "ダブル矢印",
            FormatArgs = new Dictionary<string, string>
            {
                ["inner up"] = "内側の上",
                ["inner down"] = "内側の下",
                ["inner left"] = "内側の左",
                ["inner right"] = "内側の右",
                ["outer up"] = "外側の上",
                ["outer down"] = "外側の下",
                ["outer left"] = "外側の左",
                ["outer right"] = "外側の右",
            },
            Answers = new Dictionary<string, string>
            {
                ["Up"] = "上",
                ["Right"] = "右",
                ["Left"] = "左",
                ["Down"] = "下",
            },
        },

        // Double Color
        // What was the screen color on the {1} stage of {0}?
        // What was the screen color on the first stage of Double Color?
        [Question.DoubleColorColors] = new()
        {
            QuestionText = "{0}でのステージ{1}の画面の色は？",
            ModuleName = "二色",
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
        // What was the digit on the {1} display in {0}?
        // What was the digit on the left display in Double Digits?
        [Question.DoubleDigitsDisplays] = new()
        {
            QuestionText = "{0}の{1}の画面上の数字は？",
            ModuleName = "二桁",
            FormatArgs = new Dictionary<string, string>
            {
                ["left"] = "左",
                ["right"] = "右",
            },
        },

        // Double Expert
        // What was the starting key number in {0}?
        // What was the starting key number in Double Expert?
        [Question.DoubleExpertStartingKeyNumber] = new()
        {
            QuestionText = "{0}の初期キー番号は？",
            ModuleName = "ダブル・エキスパート",
        },
        // What was the word you submitted in {0}?
        // What was the word you submitted in Double Expert?
        [Question.DoubleExpertSubmittedWord] = new()
        {
            QuestionText = "{0}で送信した単語は？",
            ModuleName = "ダブル・エキスパート",
        },

        // Double Listening
        // What clip was played in {0}?
        // What clip was played in Double Listening?
        [Question.DoubleListeningSounds] = new()
        {
            QuestionText = "{0}で再生されたクリップは？",
            ModuleName = "ダブルリスニング",
        },

        // Double-Oh
        // Which button was the submit button in {0}?
        // Which button was the submit button in Double-Oh?
        [Question.DoubleOhSubmitButton] = new()
        {
            QuestionText = "{0}の送信ボタンは？",
            ModuleName = "ダブル・オー",
        },

        // Double Screen
        // What color was the {1} screen in the {2} stage of {0}?
        // What color was the top screen in the first stage of Double Screen?
        [Question.DoubleScreenColors] = new()
        {
            QuestionText = "{0}でステージ{2}の{1}画面の色は？",
            ModuleName = "二画面",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["bottom"] = "下",
            },
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Blue"] = "青",
            },
        },

        // Dr. Doctor
        // Which of these symptoms was listed on {0}?
        // Which of these symptoms was listed on Dr. Doctor?
        [Question.DrDoctorSymptoms] = new()
        {
            QuestionText = "{0}に存在した症状に含まれるのはどれ？",
            ModuleName = "医学博士",
        },
        // Which of these diseases was listed on {0}, but not the one treated?
        // Which of these diseases was listed on Dr. Doctor, but not the one treated?
        [Question.DrDoctorDiseases] = new()
        {
            QuestionText = "{0}に存在したが治療しなかった病気はどれ？",
            ModuleName = "医学博士",
        },

        // Dreamcipher
        // What was the decrypted word in {0}?
        // What was the decrypted word in Dreamcipher?
        [Question.DreamcipherWord] = new()
        {
            QuestionText = "{0}で解読した単語は？",
            ModuleName = "夢想暗号",
        },

        // The Duck
        // What was the color of the curtain in {0}?
        // What was the color of the curtain in The Duck?
        [Question.DuckCurtainColor] = new()
        {
            QuestionText = "{0}のカーテンの色は？",
            ModuleName = "アヒル",
            Answers = new Dictionary<string, string>
            {
                ["blue"] = "青",
                ["yellow"] = "黄",
                ["green"] = "緑",
                ["orange"] = "橙",
                ["red"] = "赤",
            },
        },

        // Dumb Waiters
        // Which player {1} present in {0}?
        // Which player was present in Dumb Waiters?
        [Question.DumbWaitersPlayerAvailable] = new()
        {
            QuestionText = "{0}に存在して{1}選手は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["was"] = "いた",
                ["was not"] = "いなかった",
            },
        },

        // Earthbound
        // What was the background in {0}?
        // What was the background in Earthbound?
        [Question.EarthboundBackground] = new()
        {
            QuestionText = "{0}の背景の数字は？",
            ModuleName = "MOTHER",
        },
        // Which monster was displayed in {0}?
        // Which monster was displayed in Earthbound?
        [Question.EarthboundMonster] = new()
        {
            QuestionText = "{0}で表示されたモンスターは？",
            ModuleName = "MOTHER",
        },

        // eeB gnillepS
        // What word was asked to be spelled in {0}?
        // What word was asked to be spelled in eeB gnillepS?
        [Question.eeBgnillepSWord] = new()
        {
            QuestionText = "{0}において綴りを尋ねられた単語は？",
            ModuleName = "ービ・グンリペス",
        },

        // Eight
        // What was the last digit on the small display in {0}?
        // What was the last digit on the small display in Eight?
        [Question.EightLastSmallDisplayDigit] = new()
        {
            QuestionText = "{0}の小さなディスプレーの下一桁は？",
        },
        // What was the position of the last broken digit in {0}?
        // What was the position of the last broken digit in Eight?
        [Question.EightLastBrokenDigitPosition] = new()
        {
            QuestionText = "{0}の最後に壊された桁の位置は？",
        },
        // What were the last resulting digits in {0}?
        // What were the last resulting digits in Eight?
        [Question.EightLastResultingDigits] = new()
        {
            QuestionText = "{0}の最終的な数字は？",
        },
        // What was the last displayed number in {0}?
        // What was the last displayed number in Eight?
        [Question.EightLastDisplayedNumber] = new()
        {
            QuestionText = "{0}の最後に表示された数字は？",
        },

        // Elder Futhark
        // What was the {1} rune shown on {0}?
        // What was the first rune shown on Elder Futhark?
        [Question.ElderFutharkRunes] = new()
        {
            QuestionText = "{0}の{1}番目に表示されたルーンは？",
            ModuleName = "エルダー・フサルク",
        },

        // Emoji
        // What was the {1} emoji in {0}?
        // What was the left emoji in Emoji?
        [Question.EmojiEmoji] = new()
        {
            QuestionText = "{0}の{1}の絵文字は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["left"] = "左",
                ["right"] = "右",
            },
        },

        // ƎNA Cipher
        // What was the {1} keyword in {0}?
        // What was the first keyword in ƎNA Cipher?
        [Question.EnaCipherKeywordAnswer] = new()
        {
            QuestionText = "{0}の{1}番目のキーワードは？",
            ModuleName = "エナ暗号",
        },
        // What was the transposition key in {0}?
        // What was the transposition key in ƎNA Cipher?
        [Question.EnaCipherExtAnswer] = new()
        {
            QuestionText = "{0}の転移キーは？",
            ModuleName = "エナ暗号",
        },
        // What was the encrypted word in {0}?
        // What was the encrypted word in ƎNA Cipher?
        [Question.EnaCipherEncryptedAnswer] = new()
        {
            QuestionText = "{0}で解読した単語は？",
            ModuleName = "エナ暗号",
        },

        // Encrypted Dice
        // Which of these numbers appeared on a die in the {1} stage of {0}?
        // Which of these numbers appeared on a die in the first stage of Encrypted Dice?
        [Question.EncryptedDice] = new()
        {
            QuestionText = "{0}のステージ{1}で表示された目に含まれるのは？",
            ModuleName = "暗号化ダイス",
        },

        // Encrypted Equations
        // Which shape was the {1} operand in {0}?
        // Which shape was the first operand in Encrypted Equations?
        [Question.EncryptedEquationsShapes] = new()
        {
            QuestionText = "{0}の{1}の演算子の図形は？",
            ModuleName = "暗号化方程式",
        },

        // Encrypted Hangman
        // What method of encryption was used by {0}?
        // What method of encryption was used by Encrypted Hangman?
        [Question.EncryptedHangmanEncryptionMethod] = new()
        {
            QuestionText = "{0}で使われた暗号化方式は？",
            ModuleName = "暗号化ハングマン",
            Answers = new Dictionary<string, string>
            {
                ["Caesar Cipher"] = "カエサル暗号",
                ["Atbash Cipher"] = "Atbash暗号",
                ["Rot-13 Cipher"] = "ROT-13暗号",
                ["Affine Cipher"] = "アフィン暗号",
                ["Modern Cipher"] = "現代暗号",
                ["Vigenère Cipher"] = "ヴィジュネル暗号",
                ["Playfair Cipher"] = "プレイフェア暗号",
            },
        },
        // What module name was encrypted by {0}?
        // What module name was encrypted by Encrypted Hangman?
        [Question.EncryptedHangmanModule] = new()
        {
            QuestionText = "{0}で暗号化されていたモジュール名は？",
            ModuleName = "暗号化ハングマン",
        },

        // Encrypted Maze
        // Which symbol on {0} was spinning {1}?
        // Which symbol on Encrypted Maze was spinning clockwise?
        [Question.EncryptedMazeSymbols] = new()
        {
            QuestionText = "{0}で{1}に回転していたシンボルは？",
            ModuleName = "暗号化迷路",
            FormatArgs = new Dictionary<string, string>
            {
                ["clockwise"] = "時計回り",
                ["counter-clockwise"] = "反時計回り",
            },
        },

        // Encrypted Morse
        // What was the {1} on {0}?
        // What was the received call on Encrypted Morse?
        [Question.EncryptedMorseCallResponse] = new()
        {
            QuestionText = "{0}の{1}は？",
            ModuleName = "暗号化モールス信号",
            FormatArgs = new Dictionary<string, string>
            {
                ["received call"] = "受信した信号",
                ["sent response"] = "送信した返答",
            },
        },

        // Encryption Bingo
        // What was the first encoding used in {0}?
        // What was the first encoding used in Encryption Bingo?
        [Question.EncryptionBingoEncoding] = new()
        {
            QuestionText = "{0}の最初の復号方式は？",
            ModuleName = "暗号化ビンゴ",
            Answers = new Dictionary<string, string>
            {
                ["Morse Code"] = "モールス信号",
                ["Tap Code"] = "タップ・コード",
                ["Maritime Flags"] = "海上旗",
                ["Semaphore"] = "セマフォア信号",
                ["Pigpen"] = "ピッグペン暗号",
                ["Lombax"] = "ロンバックス",
                ["Braille"] = "点字",
                ["Wingdings"] = "Wingdings",
                ["Zoni"] = "ゾ二",
                ["Galatic Alphabet"] = "銀河標準語",
                ["Arrow"] = "矢印",
                ["Listening"] = "リスニング",
                ["Regular Number"] = "数字",
                ["Chinese Number"] = "漢数字",
                ["Cube Symbols"] = "キューブ記号",
                ["Runes"] = "ルーン文字",
                ["New York Point"] = "旧式点字",
                ["Fontana"] = "Fontana",
                ["ASCII Hex Code"] = "ASCII十六進数",
            },
        },

        // Enigma Cycle
        // Which direction was the {1} dial pointing in {0}?
        // Which direction was the first dial pointing in Enigma Cycle?
        [Question.EnigmaCycleDialDirectionsThree] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which direction was the {1} dial pointing in {0}?",
        },
        // Which direction was the {1} dial pointing in {0}?
        // Which direction was the first dial pointing in Enigma Cycle?
        [Question.EnigmaCycleDialDirectionsTwelve] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which direction was the {1} dial pointing in {0}?",
        },
        // Which direction was the {1} dial pointing in {0}?
        // Which direction was the first dial pointing in Enigma Cycle?
        [Question.EnigmaCycleDialDirectionsEight] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which direction was the {1} dial pointing in {0}?",
        },
        // What letter was written on the {1} dial in {0}?
        // What letter was written on the first dial in Enigma Cycle?
        [Question.EnigmaCycleDialLabels] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What letter was written on the {1} dial in {0}?",
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
            QuestionText = "{0}において綴りを尋ねられた単語は？",
        },

        // Equations X
        // What was the displayed symbol in {0}?
        // What was the displayed symbol in Equations X?
        [Question.EquationsXSymbols] = new()
        {
            QuestionText = "{0}に表示された記号は？",
            ModuleName = "方程式X",
        },

        // Error Codes
        // What was the active error code in {0}?
        // What was the active error code in Error Codes?
        [Question.ErrorCodesActiveError] = new()
        {
            QuestionText = "{0}で有効だったエラーコードは？",
            ModuleName = "エラーコード",
        },

        // Etterna
        // What was the beat for the {1} arrow from the bottom in {0}?
        // What was the beat for the first arrow from the bottom in Etterna?
        [Question.EtternaNumber] = new()
        {
            QuestionText = "{0}の下から{1}番目の矢印のビートは？",
            ModuleName = "エテルナ",
        },

        // Exoplanets
        // What was the starting target planet in {0}?
        // What was the starting target planet in Exoplanets?
        [Question.ExoplanetsStartingTargetPlanet] = new()
        {
            QuestionText = "{0}の開始ターゲット惑星は？",
            ModuleName = "太陽系外惑星",
            Answers = new Dictionary<string, string>
            {
                ["outer"] = "外側",
                ["middle"] = "中央",
                ["inner"] = "内側",
                ["none"] = "なし",
            },
        },
        // What was the starting target digit in {0}?
        // What was the starting target digit in Exoplanets?
        [Question.ExoplanetsStartingTargetDigit] = new()
        {
            QuestionText = "{0}の開始ターゲット値は？",
            ModuleName = "太陽系外惑星",
        },
        // What was the final target planet in {0}?
        // What was the final target planet in Exoplanets?
        [Question.ExoplanetsTargetPlanet] = new()
        {
            QuestionText = "{0}の最終ターゲット惑星は？",
            ModuleName = "太陽系外惑星",
            Answers = new Dictionary<string, string>
            {
                ["outer"] = "外側",
                ["middle"] = "中央",
                ["inner"] = "内側",
                ["none"] = "なし",
            },
        },
        // What was the final target digit in {0}?
        // What was the final target digit in Exoplanets?
        [Question.ExoplanetsTargetDigit] = new()
        {
            QuestionText = "{0}の最終ターゲット値は？",
            ModuleName = "太陽系外惑星",
        },

        // Factoring Maze
        // What was one of the prime numbers chosen in {0}?
        // What was one of the prime numbers chosen in Factoring Maze?
        [Question.FactoringMazeChosenPrimes] = new()
        {
            QuestionText = "{0}で選ばれた素因数の一つにあるのはどれ？",
            ModuleName = "因数迷路",
        },

        // Factory Maze
        // What room did you start in in {0}?
        // What room did you start in in Factory Maze?
        [Question.FactoryMazeStartRoom] = new()
        {
            QuestionText = "{0}の開始場所の部屋は？",
            ModuleName = "工場迷路",
        },

        // Faerie Fires
        // What pitch did the {1} faerie sing in {0}?
        // What pitch did the first faerie sing in Faerie Fires?
        [Question.FaerieFiresPitchOrdinal] = new()
        {
            QuestionText = "{0}の{1}番目の妖精が歌った音の高さは？",
            ModuleName = "妖精の火",
        },
        // What pitch did the {1} faerie sing in {0}?
        // What pitch did the red faerie sing in Faerie Fires?
        [Question.FaerieFiresPitchColor] = new()
        {
            QuestionText = "{0}の{1}の妖精が歌った音の高さは？",
            ModuleName = "妖精の火",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
                ["yellow"] = "黄",
                ["cyan"] = "シアン",
                ["magenta"] = "マゼンタ",
            },
        },
        // What color was the {1} faerie in {0}?
        // What color was the first faerie in Faerie Fires?
        [Question.FaerieFiresColor] = new()
        {
            QuestionText = "{0}の{1}番目の妖精の色は？",
            ModuleName = "妖精の火",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Yellow"] = "黄",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
            },
        },

        // Fast Math
        // What was the last pair of letters in {0}?
        // What was the last pair of letters in Fast Math?
        [Question.FastMathLastLetters] = new()
        {
            QuestionText = "{0}の最後の英字のペアは？",
            ModuleName = "速算",
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
            QuestionText = "{0}の読み順で{1}番目のボタンを参照していたボタンは？",
            ModuleName = "欠陥ボタン",
        },
        // Which button did the {1} button in reading order refer to in {0}?
        // Which button did the first button in reading order refer to in Faulty Buttons?
        [Question.FaultyButtonsThisButtonReferredTo] = new()
        {
            QuestionText = "{0}の読み順で{1}番目のボタンが参照していたボタンは？",
            ModuleName = "欠陥ボタン",
        },

        // Faulty RGB Maze
        // What was the exit coordinate in {0}?
        // What was the exit coordinate in Faulty RGB Maze?
        [Question.FaultyRGBMazeExit] = new()
        {
            QuestionText = "{0}の出口の座標は？",
        },
        // Where was the {1} key in {0}?
        // Where was the red key in Faulty RGB Maze?
        [Question.FaultyRGBMazeKeys] = new()
        {
            QuestionText = "{0}の{1}色の鍵はどこにあった？",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
            },
        },
        // Which maze number was the {1} maze in {0}?
        // Which maze number was the red maze in Faulty RGB Maze?
        [Question.FaultyRGBMazeNumber] = new()
        {
            QuestionText = "{0}の{1}色迷路の番号は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
            },
        },

        // Find The Date
        // What was the day displayed in the {1} stage of {0}?
        // What was the day displayed in the first stage of Find The Date?
        [Question.FindTheDateDay] = new()
        {
            QuestionText = "{0}のステージ{1}で表示された日は？",
            ModuleName = "曜日の特定",
        },
        // What was the month displayed in the {1} stage of {0}?
        // What was the month displayed in the first stage of Find The Date?
        [Question.FindTheDateMonth] = new()
        {
            QuestionText = "{0}のステージ{1}で表示された月は？",
            ModuleName = "曜日の特定",
        },
        // What was the year displayed in the {1} stage of {0}?
        // What was the year displayed in the first stage of Find The Date?
        [Question.FindTheDateYear] = new()
        {
            QuestionText = "{0}のステージ{1}で表示された年は？",
            ModuleName = "曜日の特定",
        },

        // Five Letter Words
        // Which of these words was on the display in {0}?
        // Which of these words was on the display in Five Letter Words?
        [Question.FiveLetterWordsDisplayedWords] = new()
        {
            QuestionText = "{0}に表示された単語に含まれるのは？",
            ModuleName = "5文字の単語",
        },

        // FizzBuzz
        // What was the {1} digit on the {2} display of {0}?
        // What was the first digit on the top display of FizzBuzz?
        [Question.FizzBuzzDisplayedNumbers] = new()
        {
            QuestionText = "{0}の{2}ディスプレーにあった数の{1}桁目は？",
            ModuleName = "フィズバズ",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Flags
        // What was the displayed number in {0}?
        // What was the displayed number in Flags?
        [Question.FlagsDisplayedNumber] = new()
        {
            QuestionText = "{0}で表示された数字は？",
            ModuleName = "国旗",
        },
        // What was the main country flag in {0}?
        // What was the main country flag in Flags?
        [Question.FlagsMainCountry] = new()
        {
            QuestionText = "{0}のメイン国旗は？",
            ModuleName = "国旗",
        },
        // Which of these country flags was shown, but not the main country flag, in {0}?
        // Which of these country flags was shown, but not the main country flag, in Flags?
        [Question.FlagsCountries] = new()
        {
            QuestionText = "{0}に表示されたメイン国旗以外の国旗は？",
            ModuleName = "国旗",
        },

        // Flashing Arrows
        // What number was displayed on {0}?
        // What number was displayed on Flashing Arrows?
        [Question.FlashingArrowsDisplayedValue] = new()
        {
            QuestionText = "{0}に表示されていた数字は？",
            ModuleName = "点滅矢印",
        },
        // What color flashed {1} black on the relevant arrow in {0}?
        // What color flashed before black on the relevant arrow in Flashing Arrows?
        [Question.FlashingArrowsReferredArrow] = new()
        {
            QuestionText = "{0}で関連する矢印について黒色の{1}に点滅した色は？",
            ModuleName = "点滅矢印",
            FormatArgs = new Dictionary<string, string>
            {
                ["before"] = "前",
                ["after"] = "後",
            },
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Orange"] = "オレンジ",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Purple"] = "紫",
                ["White"] = "白",
            },
        },

        // Flashing Lights
        // How many times did the {1} LED flash {2} on {0}?
        // How many times did the top LED flash cyan on Flashing Lights?
        [Question.FlashingLightsLEDFrequency] = new()
        {
            QuestionText = "{0}で{1}のLEDは{2}色に何回光った？",
            ModuleName = "点滅ライト",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["cyan"] = "シアン",
                ["green"] = "緑",
                ["red"] = "赤",
                ["purple"] = "紫",
                ["orange"] = "オレンジ",
                ["bottom"] = "下",
            },
        },

        // Flavor Text
        // Which module’s flavor text was shown in {0}?
        // Which module’s flavor text was shown in Flavor Text?
        [Question.FlavorTextModule] = new()
        {
            QuestionText = "{0}で表示されたフレーバーテキストは、どのモジュールのもの(英名)？",
            ModuleName = "フレーバーテキスト",
        },

        // Flavor Text EX
        // Which module’s flavor text was shown in the {1} stage of {0}?
        // Which module’s flavor text was shown in the first stage of Flavor Text EX?
        [Question.FlavorTextEXModule] = new()
        {
            QuestionText = "{0}のステージ{1}で表示されたフレーバーテキストは、どのモジュールのもの(英名)？",
            ModuleName = "フレーバーテキストEX",
        },

        // Flyswatting
        // Which fly was present, but not in the solution in {0}?
        // Which fly was present, but not in the solution in Flyswatting?
        [Question.FlyswattingUnpressed] = new()
        {
            QuestionText = "{0}に存在したが、答えに含まれていないハエは？",
            ModuleName = "ハエ叩き",
        },

        // Follow Me
        // What was the {1} flashing direction in {0}?
        // What was the first flashing direction in Follow Me?
        [Question.FollowMeDisplayedPath] = new()
        {
            QuestionText = "{0}で{1}番目に点滅した方向は？",
            Answers = new Dictionary<string, string>
            {
                ["Up"] = "上",
                ["Down"] = "下",
                ["Left"] = "左",
                ["Right"] = "右",
            },
        },

        // Forest Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Forest Cipher?
        [Question.ForestCipherScreen] = new()
        {
            QuestionText = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
            ModuleName = "柚葉色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Forget Any Color
        // What colors were the cylinders during the {1} stage of {0}?
        // What colors were the cylinders during the first stage of Forget Any Color?
        [Question.ForgetAnyColorCylinder] = new()
        {
            QuestionText = "{0}のステージ{1}におけるシリンダーは？",
            ModuleName = "全色忘る",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["{0}, {1}, {2}"] = "{0}, {1}, {2}",
                ["Red"] = "赤",
                ["Orange"] = "橙",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Cyan"] = "シアン",
                ["Blue"] = "青",
                ["Purple"] = "紫",
                ["White"] = "白",
                ["L"] = "左",
                ["M"] = "中",
                ["R"] = "右",
                ["the Forget Any Color which used figure {0} in the {1} stage"] = "ステージ{1}で図{0}を使用した全色忘る",
                ["the Forget Any Color whose cylinders in the {1} stage were {0}"] = "ステージ{0}のシリンダーに{1}があった全色忘る",
            },
        },
        // Which figure was used during the {1} stage of {0}?
        // Which figure was used during the first stage of Forget Any Color?
        [Question.ForgetAnyColorSequence] = new()
        {
            QuestionText = "{0}のステージ{1}で使用した表は？",
            ModuleName = "全色忘る",
        },

        // Forget Everything
        // What was the {1} displayed digit in the first stage of {0}?
        // What was the first displayed digit in the first stage of Forget Everything?
        [Question.ForgetEverythingStageOneDisplay] = new()
        {
            QuestionText = "{0}の最初のステージにあった数字の{1}桁目は？",
            ModuleName = "須く忘る",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Forget Everything whose {0} displayed digit in that stage was {1}"] = "最初のステージで{0}番目の数字が{1}だった須く忘る",
            },
        },

        // Forget Me
        // What number was in the {1} position of the initial puzzle in {0}?
        // What number was in the top-left position of the initial puzzle in Forget Me?
        [Question.ForgetMeInitialState] = new()
        {
            QuestionText = "{0}の初期状態のパズルにおける{1}の数字は？",
            ModuleName = "我忘る",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-middle"] = "上",
                ["top-right"] = "右上",
                ["middle-left"] = "左",
                ["center"] = "中央",
                ["middle-right"] = "右",
                ["bottom-left"] = "左下",
                ["bottom-middle"] = "下",
                ["bottom-right"] = "右下",
            },
        },

        // Forget Me Not
        // What was the digit displayed in the {1} stage of {0}?
        // What was the digit displayed in the first stage of Forget Me Not?
        [Question.ForgetMeNotDisplayedDigits] = new()
        {
            QuestionText = "{0}のステージ{1}で表示されていた数字は？",
            ModuleName = "我忘る勿かれ",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Forget Me Not which displayed a {0} in the {1} stage"] = "ステージ{1}の数字が{0}だった須く忘る",
            },
        },

        // Forget Me Now
        // What was the {1} displayed digit in {0}?
        // What was the first displayed digit in Forget Me Now?
        [Question.ForgetMeNowDisplayedDigits] = new()
        {
            QuestionText = "{0}の{1}番目に表示された数字は？",
            ModuleName = "我忘るる",
        },

        // Forget Our Voices
        // What was played in the {1} stage of {0}?
        // What was played in the first stage of Forget Our Voices?
        [Question.ForgetOurVoicesVoice] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What was played in the {1} stage of {0}?",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Forget Our Voices which played a {0} in {1}’s voice in the {2} stage"] = "the Forget Our Voices which played a {0} in {1}’s voice in the {2} stage",
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

        // Forget’s Ultimate Showdown
        // What was the {1} digit of the answer in {0}?
        // What was the first digit of the answer in Forget’s Ultimate Showdown?
        [Question.ForgetsUltimateShowdownAnswer] = new()
        {
            QuestionText = "{0}の答えの{1}桁目は？",
            ModuleName = "忘る者の究極の対決",
        },
        // What was the {1} digit of the initial number in {0}?
        // What was the first digit of the initial number in Forget’s Ultimate Showdown?
        [Question.ForgetsUltimateShowdownInitial] = new()
        {
            QuestionText = "{0}の初期状態の{1}桁目は？",
            ModuleName = "忘る者の究極の対決",
        },
        // What was the {1} digit of the bottom number in {0}?
        // What was the first digit of the bottom number in Forget’s Ultimate Showdown?
        [Question.ForgetsUltimateShowdownBottom] = new()
        {
            QuestionText = "{0}の下側の数字の{1}桁目は？",
            ModuleName = "忘る者の究極の対決",
        },
        // What was the {1} method used in {0}?
        // What was the first method used in Forget’s Ultimate Showdown?
        [Question.ForgetsUltimateShowdownMethod] = new()
        {
            QuestionText = "{0}の{1}回目に使用した変換方法は？",
            ModuleName = "忘る者の究極の対決",
            Answers = new Dictionary<string, string>
            {
                ["Forget Me Not"] = "我忘る勿かれ",
                ["Simon’s Stages"] = "サイモンのステージ",
                ["Forget Me Later"] = "後に我忘る",
                ["Forget Infinity"] = "永遠に忘る",
                ["A>N<D"] = "AND",
                ["Forget Me Now"] = "我忘るる",
                ["Forget Everything"] = "須く忘る",
                ["Forget Us Not"] = "我等忘る勿かれ",
            },
        },

        // Forget The Colors
        // What number was on the gear during stage {1} of {0}?
        // What number was on the gear during stage 0 of Forget The Colors?
        [Question.ForgetTheColorsGearNumber] = new()
        {
            QuestionText = "{0}のステージ{1}におけるギアの数字は？",
            ModuleName = "色忘る",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Forget The Colors whose gear number was {0} in stage {1}"] = "ステージ{1}の歯車の数字が{0}だった色忘る",
                ["the Forget The Colors which had {0} on its large display in stage {1}"] = "ステージ{1}の大きなディスプレーの数字が{0}であった色忘る",
                ["the Forget The Colors whose received sine number in stage {1} ended with a {0}"] = "ステージ{1}のsin値の末尾が{0}であった色忘る",
                ["the Forget The Colors whose gear color was {0} in stage {1}"] = "ステージ{1}の歯車の色が{0}であった色忘る",
                ["the Forget The Colors whose rule color was {0} in stage {1}"] = "ステージ{1}で使用したルールが{0}色であった色忘る",
            },
        },
        // What number was on the large display during stage {1} of {0}?
        // What number was on the large display during stage 0 of Forget The Colors?
        [Question.ForgetTheColorsLargeDisplay] = new()
        {
            QuestionText = "{0}のステージ{1}における大きなディスプレーはの数字は？",
            ModuleName = "色忘る",
        },
        // What was the last decimal in the sine number received during stage {1} of {0}?
        // What was the last decimal in the sine number received during stage 0 of Forget The Colors?
        [Question.ForgetTheColorsSineNumber] = new()
        {
            QuestionText = "{0}のステージ{1}で取得したsin値の下一桁は？",
            ModuleName = "色忘る",
        },
        // What color was the gear during stage {1} of {0}?
        // What color was the gear during stage 0 of Forget The Colors?
        [Question.ForgetTheColorsGearColor] = new()
        {
            QuestionText = "{0}のステージ{1}におけるギアの色は？",
            ModuleName = "色忘る",
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
        // Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?
        // Which edgework-based rule was applied to the sum of nixies and gear during stage 0 of Forget The Colors?
        [Question.ForgetTheColorsRuleColor] = new()
        {
            QuestionText = "{0}のステージ{1}におけるエッジワーク修正後のニキシー管の合計は？",
            ModuleName = "色忘る",
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

        // Forget This
        // What color was the LED in the {1} stage of {0}?
        // What color was the LED in the first stage of Forget This?
        [Question.ForgetThisColors] = new()
        {
            QuestionText = "{0}のステージ{1}におけるLEDの色は？",
            ModuleName = "之忘る",
            Answers = new Dictionary<string, string>
            {
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
                ["Black"] = "黒",
                ["White"] = "白",
                ["Green"] = "緑",
            },
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Forget This whose LED was {0} in the {1} stage"] = "ステージ{1}のLEDが{0}であった之忘る",
                ["the Forget This which displayed {0} in the {1} stage"] = "ステージ{1}で表示された文字が{0}であった之忘る",
            },
        },
        // What was the digit displayed in the {1} stage of {0}?
        // What was the digit displayed in the first stage of Forget This?
        [Question.ForgetThisDigits] = new()
        {
            QuestionText = "{0}のステージ{1}における文字は？",
            ModuleName = "之忘る",
        },

        // Forget Us Not
        // Which module name was used for stage {1} in {0}?
        // Which module name was used for stage 1 in Forget Us Not?
        [Question.ForgetUsNotStage] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which module name was used for stage {1} in {0}?",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Forget Us Not in which {0} was used for stage {1}"] = "the Forget Us Not in which {0} was used for stage {1}",
            },
        },

        // Free Parking
        // What was the player token in {0}?
        // What was the player token in Free Parking?
        [Question.FreeParkingToken] = new()
        {
            QuestionText = "{0}のプレイヤーのコマは？",
            ModuleName = "無料駐車場",
            Answers = new Dictionary<string, string>
            {
                ["Dog"] = "犬",
                ["Wheelbarrow"] = "手押し車",
                ["Cat"] = "猫",
                ["Iron"] = "アイロン",
                ["Top Hat"] = "帽子",
                ["Car"] = "車",
                ["Battleship"] = "軍艦",
            },
        },

        // Functions
        // What was the last digit of your first query’s result in {0}?
        // What was the last digit of your first query’s result in Functions?
        [Question.FunctionsLastDigit] = new()
        {
            QuestionText = "{0}の最初の問い合わせ結果の下一桁は？",
            ModuleName = "関数",
        },
        // What number was to the left of the displayed letter in {0}?
        // What number was to the left of the displayed letter in Functions?
        [Question.FunctionsLeftNumber] = new()
        {
            QuestionText = "{0}で英字の左隣のディスプレーに表示された数字は？",
            ModuleName = "関数",
        },
        // What letter was displayed in {0}?
        // What letter was displayed in Functions?
        [Question.FunctionsLetter] = new()
        {
            QuestionText = "{0}に表示された英字は？",
            ModuleName = "関数",
        },
        // What number was to the right of the displayed letter in {0}?
        // What number was to the right of the displayed letter in Functions?
        [Question.FunctionsRightNumber] = new()
        {
            QuestionText = "{0}で英字の右隣のディスプレーに表示された数字は？",
            ModuleName = "関数",
        },

        // The Fuse Box
        // What color flashed {1} in {0}?
        // What color flashed first in The Fuse Box?
        // Note: This question is depicted visually, rather than with words. A translation here will only be used for logging.
        [Question.FuseBoxFlashes] = new()
        {
            QuestionText = "{0}で{1}番目に点滅した色は？",
            ModuleName = "ヒューズボックス",
        },
        // What arrow was shown {1} in {0}?
        // What arrow was shown first in The Fuse Box?
        // Note: This question is depicted visually, rather than with words. A translation here will only be used for logging.
        [Question.FuseBoxArrows] = new()
        {
            QuestionText = "{0}で{1}番目に表示された矢印は？",
            ModuleName = "ヒューズボックス",
        },

        // Gadgetron Vendor
        // What was your current weapon in {0}?
        // What was your current weapon in Gadgetron Vendor?
        [Question.GadgetronVendorCurrentWeapon] = new()
        {
            QuestionText = "{0}で所持していた武器は？",
            ModuleName = "ガラクトロン・ベンダー",
        },
        // What was the weapon up for sale in {0}?
        // What was the weapon up for sale in Gadgetron Vendor?
        [Question.GadgetronVendorWeaponForSale] = new()
        {
            QuestionText = "{0}で販売されている武器は？",
            ModuleName = "ガラクトロン・ベンダー",
        },

        // Game of Life Cruel
        // Which of these was a color combination that occurred in {0}?
        // Which of these was a color combination that occurred in Game of Life Cruel?
        [Question.GameOfLifeCruelColors] = new()
        {
            QuestionText = "{0}に出現した色の組み合わせに含まれるのは？",
            ModuleName = "残忍ライフゲーム",
        },

        // The Gamepad
        // What were the numbers on {0}?
        // What were the numbers on The Gamepad?
        [Question.GamepadNumbers] = new()
        {
            QuestionText = "{0}の数字は？",
            ModuleName = "ゲームパッド",
        },

        // Garfield Kart
        // How many puzzle pieces did {0} have?
        // How many puzzle pieces did Garfield Kart have?
        [Question.GarfieldKartPuzzleCount] = new()
        {
            QuestionText = "{0}にあったパズルのピースの数は？",
            ModuleName = "ガーフィールドカート",
        },
        // What was the track in {0}?
        // What was the track in Garfield Kart?
        [Question.GarfieldKartTrack] = new()
        {
            QuestionText = "{0}のトラックは？",
            ModuleName = "ガーフィールドカート",
        },

        // The Garnet Thief
        // Which faction did {1} claim to be in {0}?
        // Which faction did Jungmoon claim to be in The Garnet Thief?
        [Question.GarnetThiefClaim] = new()
        {
            QuestionText = "{0}の{1}が所属を主張していた派閥は？",
            ModuleName = "宝石泥棒",
        },

        // Ghost Movement
        // Where was {1} in {0}?
        // Where was Inky in Ghost Movement?
        [Question.GhostMovementPosition] = new()
        {
            QuestionText = "{0}で{1}はどこにいた？",
        },

        // Girlfriend
        // What was the language sung in {0}?
        // What was the language sung in Girlfriend?
        [Question.GirlfriendLanguage] = new()
        {
            QuestionText = "{0}で歌っていた言語は？",
        },

        // The Glitched Button
        // What was the cycling bit sequence in {0}?
        // What was the cycling bit sequence in The Glitched Button?
        [Question.GlitchedButtonSequence] = new()
        {
            QuestionText = "{0}で循環表示されていたビットのシーケンスは？",
            ModuleName = "グリッチボタン",
        },

        // Goofy’s Game
        // What number was flashed by the {1} LED in {0}?
        // What number was flashed by the left LED in Goofy’s Game?
        [Question.GoofysGameNumber] = new()
        {
            QuestionText = "{0}の{1}のLEDが点滅した数字は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["left"] = "左",
                ["right"] = "右",
                ["center"] = "中央",
            },
        },

        // Grand Piano
        // Which key was part of the {1} set in {0}?
        // Which key was part of the first set in Grand Piano?
        [Question.GrandPianoKey] = new()
        {
            QuestionText = "{0}の{1}番目のセットに含まれていた鍵盤は？",
            ModuleName = "グランドピアノ",
        },
        // Which key was the fifth set in {0}?
        // Which key was the fifth set in Grand Piano?
        [Question.GrandPianoFinalKey] = new()
        {
            QuestionText = "{0}の5番目のセットに含まれていた鍵盤は？",
            ModuleName = "グランドピアノ",
        },

        // The Gray Button
        // What was the {1} coordinate on the display in {0}?
        // What was the horizontal coordinate on the display in The Gray Button?
        [Question.GrayButtonCoordinates] = new()
        {
            QuestionText = "{0}のディスプレー上に表示された{1}の座標は？",
            ModuleName = "灰色ボタン",
            FormatArgs = new Dictionary<string, string>
            {
                ["horizontal"] = "段",
                ["vertical"] = "列",
            },
        },

        // Gray Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Gray Cipher?
        [Question.GrayCipherScreen] = new()
        {
            QuestionText = "{0}の答えは？",
            ModuleName = "灰色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // The Great Void
        // What was the {1} color in {0}?
        // What was the first color in The Great Void?
        [Question.GreatVoidColor] = new()
        {
            QuestionText = "{0}の{1}番目の色は？",
            ModuleName = "超空洞",
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
        [Question.GreatVoidDigit] = new()
        {
            QuestionText = "{0}の{1}番目の数字は？",
            ModuleName = "超空洞",
        },

        // Green Arrows
        // What was the last number on the display on {0}?
        // What was the last number on the display on Green Arrows?
        [Question.GreenArrowsLastScreen] = new()
        {
            QuestionText = "{0}の最後に表示された数字は？",
            ModuleName = "緑色矢印",
        },

        // The Green Button
        // What was the word submitted in {0}?
        // What was the word submitted in The Green Button?
        [Question.GreenButtonWord] = new()
        {
            QuestionText = "{0}で送信した単語は？",
            ModuleName = "緑色ボタン",
        },

        // Green Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Green Cipher?
        [Question.GreenCipherScreen] = new()
        {
            QuestionText = "{0}の答えは？",
            ModuleName = "緑色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Gridlock
        // What was the starting location in {0}?
        // What was the starting location in Gridlock?
        [Question.GridLockStartingLocation] = new()
        {
            QuestionText = "{0}の開始位置は？",
            ModuleName = "グリッドロック",
        },
        // What was the ending location in {0}?
        // What was the ending location in Gridlock?
        [Question.GridLockEndingLocation] = new()
        {
            QuestionText = "{0}の終了位置は？",
            ModuleName = "グリッドロック",
        },
        // What was the starting color in {0}?
        // What was the starting color in Gridlock?
        [Question.GridLockStartingColor] = new()
        {
            QuestionText = "{0}の開始地点は何色？",
            ModuleName = "グリッドロック",
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
        [Question.GroceryStoreFirstItem] = new()
        {
            QuestionText = "{0}で最初に表示された商品は？",
            ModuleName = "食料品店",
        },

        // Gryphons
        // What was the gryphon’s name in {0}?
        // What was the gryphon’s name in Gryphons?
        [Question.GryphonsName] = new()
        {
            QuestionText = "{0}にいたグリフォンの名前は？",
            ModuleName = "グリフォン",
        },
        // What was the gryphon’s age in {0}?
        // What was the gryphon’s age in Gryphons?
        [Question.GryphonsAge] = new()
        {
            QuestionText = "{0}にいたグリフォンの年齢は？",
            ModuleName = "グリフォン",
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
            QuestionText = "{0}の{1}のLEDの色は？",
            ModuleName = "ジャイロ迷路",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["bottom"] = "下",
            },
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Blue"] = "青",
                ["Green"] = "緑",
                ["Yellow"] = "黄",
            },
        },

        // h
        // What was the transmitted letter in {0}?
        // What was the transmitted letter in h?
        [Question.HLetter] = new()
        {
            QuestionText = "{0}で送信した英字は？",
            ModuleName = "H",
        },

        // Halli Galli
        // Which fruit were there five of in {0}?
        // Which fruit were there five of in Halli Galli?
        [Question.HalliGalliFruit] = new()
        {
            QuestionText = "{0}に5個あった果物は？",
            ModuleName = "ハリガリ",
            Answers = new Dictionary<string, string>
            {
                ["Strawberries"] = "イチゴ",
                ["Melons"] = "メロン",
                ["Lemons"] = "レモン",
                ["Raspberries"] = "ラズベリー",
                ["Bananas"] = "バナナ",
            },
        },
        // What were the relevant counts in {0}?
        // What were the relevant counts in Halli Galli?
        [Question.HalliGalliCounts] = new()
        {
            QuestionText = "{0}で答えを求めるために使用した分け方は？",
            ModuleName = "ハリガリ",
        },

        // Hereditary Base Notation
        // What was the given number in {0}?
        // What was the given number in Hereditary Base Notation?
        [Question.HereditaryBaseNotationInitialNumber] = new()
        {
            QuestionText = "{0}で得られた数字は？",
            ModuleName = "遺伝的基数表記",
        },

        // The Hexabutton
        // What label was printed on {0}?
        // What label was printed on The Hexabutton?
        [Question.HexabuttonLabel] = new()
        {
            QuestionText = "{0}に記されたラベルは？",
            ModuleName = "六角形ボタン",
        },

        // Hexamaze
        // What was the color of the pawn in {0}?
        // What was the color of the pawn in Hexamaze?
        [Question.HexamazePawnColor] = new()
        {
            QuestionText = "{0}のコマの色は？",
            ModuleName = "六角迷路",
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

        // hexOrbits
        // What was the {1} shape for the {2} display in {0}?
        // What was the fast shape for the first display in hexOrbits?
        [Question.HexOrbitsShape] = new()
        {
            QuestionText = "{0}の{2}番目の表示で、速度が{1}方の図形は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["fast"] = "速い",
                ["slow"] = "遅い",
            },
        },

        // hexOS
        // What were the deciphered letters in {0}?
        // What were the deciphered letters in hexOS?
        [Question.HexOSCipher] = new()
        {
            QuestionText = "{0}で解読した英字は？",
        },
        // What was the deciphered phrase in {0}?
        // What was the deciphered phrase in hexOS?
        [Question.HexOSOctCipher] = new()
        {
            QuestionText = "{0}で解読したフレーズは？",
        },
        // What was the {1} 3-digit number cycled by the screen in {0}?
        // What was the first 3-digit number cycled by the screen in hexOS?
        [Question.HexOSScreen] = new()
        {
            QuestionText = "{0}の{1}回目に表示された三桁の数字は？",
        },
        // What were the rhythm values in {0}?
        // What were the rhythm values in hexOS?
        [Question.HexOSSum] = new()
        {
            QuestionText = "{0}のリズムの値は？",
        },

        // Hickory Dickory Dock
        // What time was shown when the clock struck {1} on {0}?
        // What time was shown when the clock struck 1:00 on Hickory Dickory Dock?
        [Question.HickoryDickoryDockTime] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What time was shown when the clock struck {1} on {0}?",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Hickory Dickory Dock which showed {0}:{1:00} when it struck {2}"] = "the Hickory Dickory Dock which showed {0}:{1:00} when it struck {2}",
            },
        },

        // Hidden Colors
        // What was the color of the main LED in {0}?
        // What was the color of the main LED in Hidden Colors?
        [Question.HiddenColorsLED] = new()
        {
            QuestionText = "{0}のメインLEDの色は？",
            ModuleName = "隠し色",
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

        // The Hidden Value
        // What was displayed on {0}?
        // What was displayed on The Hidden Value?
        [Question.HiddenValueDisplay] = new()
        {
            QuestionText = "{0}に表示されたのは？",
            ModuleName = "隠し値",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["White"] = "白",
                ["Yellow"] = "黄",
                ["Magenta"] = "マゼンタ",
                ["Cyan"] = "シアン",
                ["Purple"] = "紫",
                ["{0} {1}"] = "{0}の{1}",
            },
        },

        // The High Score
        // What was the position of the player in {0}?
        // What was the position of the player in The High Score?
        [Question.HighScorePosition] = new()
        {
            QuestionText = "{0}のプレイヤーの位置は？",
            ModuleName = "ハイスコア",
        },
        // What was the score of the player in {0}?
        // What was the score of the player in The High Score?
        [Question.HighScoreScore] = new()
        {
            QuestionText = "{0}のプレイヤーのスコアは？",
            ModuleName = "ハイスコア",
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
            NeedsTranslation = true,
            QuestionText = "{0}の初期状態で存在して{1}蝶番に含まれるのは？",
            ModuleName = "蝶番",
            FormatArgs = new Dictionary<string, string>
            {
                ["present on"] = "いた",
                ["absent from"] = "いなかった",
            },
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Hinges where this hinge was initally present"] = "the Hinges where this hinge was initally present",
                ["the Hinges where this hinge was initally absent"] = "the Hinges where this hinge was initally absent",
            },
        },

        // Hogwarts
        // Which House was {1} solved for in {0}?
        // Which House was Binary Puzzle solved for in Hogwarts?
        [Question.HogwartsHouse] = new()
        {
            QuestionText = "{0}で{1}を解除したのはどの寮？",
            ModuleName = "ホグワーツ",
            Answers = new Dictionary<string, string>
            {
                ["Gryffindor"] = "グリフォンドール",
                ["Hufflepuff"] = "ハッフルパフ",
                ["Slytherin"] = "スリザリン",
                ["Ravenclaw"] = "レイブンクロー",
            },
        },
        // Which module was solved for {1} in {0}?
        // Which module was solved for Gryffindor in Hogwarts?
        [Question.HogwartsModule] = new()
        {
            QuestionText = "{0}で{1}が解除したのはどのモジュール(英名)？",
            ModuleName = "ホグワーツ",
            FormatArgs = new Dictionary<string, string>
            {
                ["Gryffindor"] = "グリフォンドール",
                ["Hufflepuff"] = "ハッフルパフ",
                ["Slytherin"] = "スリザリン",
                ["Ravenclaw"] = "レイブンクロー",
            },
        },

        // Hold Ups
        // What was the name of the {1} shadow shown in {0}?
        // What was the name of the first shadow shown in Hold Ups?
        [Question.HoldUpsShadows] = new()
        {
            QuestionText = "{0}の{1}番目に表示されたシャドウの名前は？",
            ModuleName = "ホールドアップ",
        },

        // Homophones
        // What was the {1} displayed phrase in {0}?
        // What was the first displayed phrase in Homophones?
        [Question.HomophonesDisplayedPhrases] = new()
        {
            QuestionText = "{0}の{1}番目に表示されたフレーズは？",
            ModuleName = "同音異義語",
        },

        // Horrible Memory
        // In what position was the button pressed on the {1} stage of {0}?
        // In what position was the button pressed on the first stage of Horrible Memory?
        [Question.HorribleMemoryPositions] = new()
        {
            QuestionText = "{0}のステージ{1}で押されたボタンの位置は？",
            ModuleName = "恐怖記憶",
        },
        // What was the label of the button pressed on the {1} stage of {0}?
        // What was the label of the button pressed on the first stage of Horrible Memory?
        [Question.HorribleMemoryLabels] = new()
        {
            QuestionText = "{0}のステージ{1}で押されたボタンのラベルは？",
            ModuleName = "恐怖記憶",
        },
        // What color was the button pressed on the {1} stage of {0}?
        // What color was the button pressed on the first stage of Horrible Memory?
        [Question.HorribleMemoryColors] = new()
        {
            QuestionText = "{0}のステージ{1}で押されたボタンの色は？",
            ModuleName = "恐怖記憶",
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

        // Human Resources
        // Which was a descriptor shown in {1} in {0}?
        // Which was a descriptor shown in red in Human Resources?
        [Question.HumanResourcesDescriptors] = new()
        {
            QuestionText = "{0}で{1}色で表示された識別語は？",
            ModuleName = "人事部",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
            },
        },
        // Who was {1} in {0}?
        // Who was fired in Human Resources?
        [Question.HumanResourcesHiredFired] = new()
        {
            QuestionText = "{0}で{1}のは誰？",
            ModuleName = "人事部",
            FormatArgs = new Dictionary<string, string>
            {
                ["fired"] = "解雇した",
                ["hired"] = "雇用した",
            },
        },

        // Hunting
        // Which of the first three stages of {0} had the {1} symbol {2}?
        // Which of the first three stages of Hunting had the column symbol first?
        [Question.HuntingColumnsRows] = new()
        {
            QuestionText = "{0}の最初3つのステージのうち、{2}番目に{1}シンボルを持っていたのはどれ？",
            ModuleName = "狩猟",
            FormatArgs = new Dictionary<string, string>
            {
                ["column"] = "列",
                ["row"] = "段",
            },
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
        },

        // The Hypercube
        // What was the {1} rotation in {0}?
        // What was the first rotation in The Hypercube?
        [Question.HypercubeRotations] = new()
        {
            QuestionText = "{0}の{1}番目の回転方向は？",
            ModuleName = "超立方体",
        },

        // HyperForget
        // What was the rotation for the {1} stage in {0}?
        // What was the rotation for the first stage in HyperForget?
        [Question.HyperForgetRotations] = new()
        {
            QuestionText = "{0}の{1}番目の回転方向は？",
            ModuleName = "超忘る",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the HyperForget whose rotation in the {1} stage was {0}"] = "{1}番目の回転方向が{0}だった超忘る",
            },
        },

        // The Hyperlink
        // What was the {1} character of the hyperlink in {0}?
        // What was the first character of the hyperlink in The Hyperlink?
        [Question.HyperlinkCharacters] = new()
        {
            QuestionText = "{0}のリンクの{1}文字目は？",
            ModuleName = "ハイパーリンク",
        },
        // Which module was referenced on {0}?
        // Which module was referenced on The Hyperlink?
        [Question.HyperlinkAnswer] = new()
        {
            QuestionText = "{0}が参照していたモジュールは(英名)？",
            ModuleName = "ハイパーリンク",
        },

        // Ice Cream
        // Which one of these flavours {1} to the {2} customer in {0}?
        // Which one of these flavours was on offer, but not sold, to the first customer in Ice Cream?
        [Question.IceCreamFlavour] = new()
        {
            QuestionText = "{0}で{2}番目の客が{1}商品に含まれるのは？",
            ModuleName = "アイスクリーム",
            FormatArgs = new Dictionary<string, string>
            {
                ["was on offer, but not sold,"] = "注文したが売らなかった",
                ["was not on offer"] = "注文していない",
            },
        },
        // Who was the {1} customer in {0}?
        // Who was the first customer in Ice Cream?
        [Question.IceCreamCustomer] = new()
        {
            QuestionText = "{0}の{1}番目の客は？",
            ModuleName = "アイスクリーム",
        },

        // Identification Crisis
        // What was the {1} shape used in {0}?
        // What was the first shape used in Identification Crisis?
        [Question.IdentificationCrisisShape] = new()
        {
            QuestionText = "{0}で使用された{1}番目の図形は？",
            ModuleName = "識別危機",
        },
        // What was the {1} identification module used in {0}?
        // What was the first identification module used in Identification Crisis?
        [Question.IdentificationCrisisDataset] = new()
        {
            QuestionText = "{0}で使用された{1}番目の識別モジュールは？",
            ModuleName = "識別危機",
            Answers = new Dictionary<string, string>
            {
                ["Morse Identification"] = "モールス識別",
                ["Boozleglyph Identification"] = "ブーズル文字識別",
                ["Plant Identification"] = "植物識別",
                ["Pickup Identification"] = "アイテム識別",
                ["Emotiguy Identification"] = "エモティガイ識別",
                ["Ars Goetia Identification"] = "アルス・ゴエティア識別",
                ["Mii Identification"] = "Mii識別",
                ["Customer identification"] = "顧客識別",
                ["Spongebob Birthday Identification"] = "スポンジ・ボブ誕生日カード識別",
                ["VTuber Identification"] = "VTuber識別",
            },
        },

        // Identity Parade
        // Which hair color {1} listed in {0}?
        // Which hair color was listed in Identity Parade?
        [Question.IdentityParadeHairColors] = new()
        {
            QuestionText = "{0}のリストに{1}のはどの髪色？",
            ModuleName = "容疑者特定",
            FormatArgs = new Dictionary<string, string>
            {
                ["was"] = "存在した",
                ["was not"] = "存在しなかった",
            },
        },
        // Which build {1} listed in {0}?
        // Which build was listed in Identity Parade?
        [Question.IdentityParadeBuilds] = new()
        {
            QuestionText = "{0}のリストに{1}のはどの身体的特徴？",
            ModuleName = "容疑者特定",
            FormatArgs = new Dictionary<string, string>
            {
                ["was"] = "存在した",
                ["was not"] = "存在しなかった",
            },
        },
        // Which attire {1} listed in {0}?
        // Which attire was listed in Identity Parade?
        [Question.IdentityParadeAttires] = new()
        {
            QuestionText = "{0}のリストに{1}のはどの服装？",
            ModuleName = "容疑者特定",
            FormatArgs = new Dictionary<string, string>
            {
                ["was"] = "存在した",
                ["was not"] = "存在しなかった",
            },
        },

        // The Impostor
        // Which module was {0} pretending to be?
        // Which module was The Impostor pretending to be?
        [Question.ImpostorDisguise] = new()
        {
            QuestionText = "{0}が化けていたのはどのモジュール(英名)？",
            ModuleName = "ニセモノ",
        },

        // Indigo Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Indigo Cipher?
        [Question.IndigoCipherScreen] = new()
        {
            QuestionText = "{0}の答えは？",
            ModuleName = "藍色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Infinite Loop
        // What was the selected word in {0}?
        // What was the selected word in Infinite Loop?
        [Question.InfiniteLoopSelectedWord] = new()
        {
            QuestionText = "{0}で選択された単語は？",
            ModuleName = "無限ループ",
        },

        // Ingredients
        // Which ingredient was used in {0}?
        // Which ingredient was used in Ingredients?
        [Question.IngredientsIngredients] = new()
        {
            QuestionText = "{0}で使用された食材は？",
            ModuleName = "食材",
        },
        // Which ingredient was listed but not used in {0}?
        // Which ingredient was listed but not used in Ingredients?
        [Question.IngredientsNonIngredients] = new()
        {
            QuestionText = "{0}の一覧にあったが使用されていない食材は？",
            ModuleName = "食材",
        },

        // Inner Connections
        // What color was the LED in {0}?
        // What color was the LED in Inner Connections?
        [Question.InnerConnectionsLED] = new()
        {
            QuestionText = "{0}のLEDの色は？",
            ModuleName = "内部接続",
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
        [Question.InnerConnectionsMorse] = new()
        {
            QuestionText = "{0}のモールス信号で点滅した数字は？",
            ModuleName = "内部接続",
        },

        // Interpunct
        // What was the symbol displayed in the {1} stage of {0}?
        // What was the symbol displayed in the first stage of Interpunct?
        [Question.InterpunctDisplay] = new()
        {
            QuestionText = "{0}のステージ{1}で表示された記号は？",
            ModuleName = "句読点",
        },

        // IPA
        // What sound played in {0}?
        // What sound played in IPA?
        [Question.IpaSound] = new()
        {
            QuestionText = "{0}で再生された音は？",
        },

        // The iPhone
        // What was the {1} PIN digit in {0}?
        // What was the first PIN digit in The iPhone?
        [Question.iPhoneDigits] = new()
        {
            QuestionText = "{0}の{1}番目のPINの数字は？",
            ModuleName = "アイフォン",
        },

        // Jenga
        // Which symbol was on the first correctly pulled block in {0}?
        // Which symbol was on the first correctly pulled block in Jenga?
        [Question.JengaFirstBlock] = new()
        {
            QuestionText = "{0}で最初に正しく引き抜かれたブロックの記号は？",
            ModuleName = "ジェンガ",
        },

        // The Jewel Vault
        // What number was wheel {1} in {0}?
        // What number was wheel A in The Jewel Vault?
        [Question.JewelVaultWheels] = new()
        {
            QuestionText = "{0}の輪{1}の数字は？",
            ModuleName = "宝石金庫",
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
            QuestionText = "{0}のこの位置にあった正方形の色は？",
            ModuleName = "色比べ格子",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Blue"] = "青",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Magenta"] = "マゼンタ",
                ["Orange"] = "オレンジ",
                ["Cyan"] = "シアン",
                ["Purple"] = "紫",
                ["Chestnut"] = "栗",
                ["Brown"] = "茶",
                ["Mauve"] = "薄藤",
                ["Azure"] = "空",
                ["Jade"] = "若緑",
                ["Forest"] = "深緑",
                ["Gray"] = "灰",
                ["Black"] = "黒",
            },
        },
        // Which square was {1} in {0}?
        // Which square was red in Juxtacolored Squares?
        [Question.JuxtacoloredSquaresPositionsByColor] = new()
        {
            QuestionText = "{0}の{1}色の正方形はどれ？",
            ModuleName = "色比べ格子",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["blue"] = "青",
                ["yellow"] = "黄",
                ["green"] = "緑",
                ["magenta"] = "マゼンタ",
                ["orange"] = "オレンジ",
                ["cyan"] = "シアン",
                ["purple"] = "紫",
                ["chestnut"] = "栗",
                ["brown"] = "茶",
                ["mauve"] = "薄藤",
                ["azure"] = "空",
                ["jade"] = "若緑",
                ["forest"] = "深緑",
                ["gray"] = "灰",
                ["black"] = "黒",
            },
        },

        // Kanji
        // What was the displayed word in the {1} stage of {0}?
        // What was the displayed word in the first stage of Kanji?
        [Question.KanjiDisplayedWords] = new()
        {
            QuestionText = "{0}のステージ{1}で表示された単語は？",
            ModuleName = "漢字",
        },

        // The Kanye Encounter
        // What was a food item displayed in {0}?
        // What was a food item displayed in The Kanye Encounter?
        [Question.KanyeEncounterFoods] = new()
        {
            QuestionText = "{0}で表示された食品は？",
            ModuleName = "カニエ・ウエストとの遭遇",
        },

        // KayMazey Talk
        // What was the {1} word in {0}?
        // What was the starting word in KayMazey Talk?
        [Question.KayMazeyTalkWord] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What was the {1} phrase in {0}?",
            FormatArgs = new Dictionary<string, string>
            {
                ["starting"] = "starting",
                ["goal"] = "goal",
            },
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the KayMazey Talk whose starting word was {0}"] = "the KayMazey Talk whose starting word was {0}",
                ["the KayMazey Talk whose goal word was {0}"] = "the KayMazey Talk whose goal word was {0}",
            },
        },

        // Keypad Combinations
        // Which number was displayed on the {1} button, but not part of the answer on {0}?
        // Which number was displayed on the first button, but not part of the answer on Keypad Combinations?
        [Question.KeypadCombinationWrongNumbers] = new()
        {
            QuestionText = "{0}の{1}番目のボタンに存在した、答え以外の数字は？",
            ModuleName = "キーパッドコンビネーション",
        },

        // Keypad Magnified
        // What was the position of the LED in {0}?
        // What was the position of the LED in Keypad Magnified?
        [Question.KeypadMagnifiedLED] = new()
        {
            QuestionText = "{0}のLEDの位置は？",
            ModuleName = "拡大キーパッド",
            Answers = new Dictionary<string, string>
            {
                ["Top-left"] = "左上",
                ["Top-right"] = "右上",
                ["Bottom-left"] = "左下",
                ["Bottom-right"] = "右下",
            },
        },

        // Keypad Maze
        // Which of these cells was yellow in {0}?
        // Which of these cells was yellow in Keypad Maze?
        [Question.KeypadMazeYellow] = new()
        {
            QuestionText = "{0}の黄色いマスはどのセル？",
            ModuleName = "キーパッド迷路",
        },

        // Keypad Sequence
        // What was this key’s label on the {1} panel in {0}?
        // What was this key’s label on the first panel in Keypad Sequence?
        [Question.KeypadSequenceLabels] = new()
        {
            QuestionText = "{0}の{1}番目のパネルにあったこのキーのラベルは？",
            ModuleName = "順番キーパッド",
        },

        // Keywords
        // What were the first four letters on the display in {0}?
        // What were the first four letters on the display in Keywords?
        [Question.KeywordsDisplayedKey] = new()
        {
            QuestionText = "{0}のディスプレー上にあった先頭四文字の英字は？",
            ModuleName = "キーワード",
        },

        // The Klaxon
        // What was the first module to set off {0}?
        // What was the first module to set off The Klaxon?
        [Question.KlaxonFirstModule] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What was the first module to set off {0}?",
        },

        // Know Your Way
        // Which way was the arrow pointing in {0}?
        // Which way was the arrow pointing in Know Your Way?
        [Question.KnowYourWayArrow] = new()
        {
            QuestionText = "{0}の矢印が指していたのはどの方向？",
            ModuleName = "方向感覚",
            Answers = new Dictionary<string, string>
            {
                ["Up"] = "上",
                ["Down"] = "下",
                ["Left"] = "左",
                ["Right"] = "右",
            },
        },
        // Which LED was green in {0}?
        // Which LED was green in Know Your Way?
        [Question.KnowYourWayLed] = new()
        {
            QuestionText = "{0}でLEDが緑色だったのはどれ？",
            ModuleName = "方向感覚",
            Answers = new Dictionary<string, string>
            {
                ["Top"] = "上",
                ["Bottom"] = "下",
                ["Right"] = "右",
                ["Left"] = "左",
            },
        },

        // Kooky Keypad
        // What color was the {1} button’s LED in {0}?
        // What color was the top-left button’s LED in Kooky Keypad?
        [Question.KookyKeypadColor] = new()
        {
            QuestionText = "{0}の{1}のボタンのLEDの色は？",
            ModuleName = "狂ったキーパッド",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-right"] = "右上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
            Answers = new Dictionary<string, string>
            {
                ["Crimson"] = "紅",
                ["Red"] = "赤",
                ["Coral"] = "珊瑚",
                ["Orange"] = "オレンジ",
                ["Lemon Chiffon"] = "レモンシフォン",
                ["Medium Spring Green"] = "エメラルドグリーン",
                ["Deep Sea Green"] = "灰緑",
                ["Cadet Blue"] = "錆浅葱",
                ["Slate Blue"] = "群青",
                ["Dark Magenta"] = "赤紫",
                ["Unlit"] = "消灯",
            },
        },

        // Kudosudoku
        // Which square was {1} in {0}?
        // Which square was pre-filled in Kudosudoku?
        [Question.KudosudokuPrefilled] = new()
        {
            QuestionText = "{0}で最初に{1}四角はどれ？",
            ModuleName = "クド数独",
            FormatArgs = new Dictionary<string, string>
            {
                ["pre-filled"] = "埋められていた",
                ["not pre-filled"] = "埋められていなかった",
            },
        },

        // Kugelblitz
        // Which particles were present for the {1} stage of {0}?
        // Which particles were present for the first stage of Kugelblitz?
        [Question.KugelblitzBlackOrangeYellowIndigoViolet] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which particles were present for the {1} stage of {0}?",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the {0} Kugelblitz"] = "the {0} Kugelblitz",
                ["black"] = "black",
                ["red"] = "red",
                ["orange"] = "orange",
                ["yellow"] = "yellow",
                ["green"] = "green",
                ["blue"] = "blue",
                ["indigo"] = "indigo",
                ["violet"] = "violet",
                ["the Kugelblitz linked with no other Kugelblitzes"] = "the Kugelblitz linked with no other Kugelblitzes",
                ["the {0} Kugelblitz linked with one other Kugelblitz"] = "the {0} Kugelblitz linked with one other Kugelblitz",
                ["the {0} Kugelblitz linked with two other Kugelblitzes"] = "the {0} Kugelblitz linked with two other Kugelblitzes",
                ["the {0} Kugelblitz linked with three other Kugelblitzes"] = "the {0} Kugelblitz linked with three other Kugelblitzes",
                ["the {0} Kugelblitz linked with four other Kugelblitzes"] = "the {0} Kugelblitz linked with four other Kugelblitzes",
                ["the {0} Kugelblitz linked with five other Kugelblitzes"] = "the {0} Kugelblitz linked with five other Kugelblitzes",
                ["the {0} Kugelblitz linked with six other Kugelblitzes"] = "the {0} Kugelblitz linked with six other Kugelblitzes",
                ["the {0} Kugelblitz linked with seven other Kugelblitzes"] = "the {0} Kugelblitz linked with seven other Kugelblitzes",
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
        // What were the particles’ values for the {1} stage of {0}?
        // What were the particles’ values for the first stage of Kugelblitz?
        [Question.KugelblitzRedGreenBlue] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What were the particles’ values for the {1} stage of {0}?",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}"] = "R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}",
            },
        },

        // Kuro
        // What was Kuro’s mood in {0}?
        // What was Kuro’s mood in Kuro?
        [Question.KuroMood] = new()
        {
            QuestionText = "{0}におけるKuroの気分は？",
        },

        // The Labyrinth
        // Where was one of the portals in layer {1} in {0}?
        // Where was one of the portals in layer 1 (Red) in The Labyrinth?
        [Question.LabyrinthPortalLocations] = new()
        {
            QuestionText = "{0}の層{1}にあったポータルの一つは？",
            ModuleName = "迷宮",
            FormatArgs = new Dictionary<string, string>
            {
                ["1 (Red)"] = "1 (赤)",
                ["2 (Orange)"] = "2 (橙)",
                ["3 (Yellow)"] = "3 (黄)",
                ["4 (Green)"] = "4 (緑)",
                ["5 (Blue)"] = "5 (青)",
            },
        },
        // In which layer was this portal in {0}?
        // In which layer was this portal in The Labyrinth?
        [Question.LabyrinthPortalStage] = new()
        {
            QuestionText = "{0}でこのポータルがあったのはどの層？",
            ModuleName = "迷宮",
            Answers = new Dictionary<string, string>
            {
                ["1 (Red)"] = "1 (赤)",
                ["2 (Orange)"] = "2 (橙)",
                ["3 (Yellow)"] = "3 (黄)",
                ["4 (Green)"] = "4 (緑)",
                ["5 (Blue)"] = "5 (青)",
            },
        },

        // Ladder Lottery
        // Which light was on in {0}?
        // Which light was on in Ladder Lottery?
        [Question.LadderLotteryLightOn] = new()
        {
            QuestionText = "{0}で点灯していたのはどのライト？",
            ModuleName = "あみだくじ",
        },

        // Ladders
        // Which color was present on the second ladder in {0}?
        // Which color was present on the second ladder in Ladders?
        [Question.LaddersStage2Colors] = new()
        {
            QuestionText = "{0}の二番目の梯子に存在した色は？",
            ModuleName = "梯子",
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
        [Question.LaddersStage3Missing] = new()
        {
            QuestionText = "{0}の三番目の梯子に存在しなかった色は？",
            ModuleName = "梯子",
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

        // Langton’s Anteater
        // Which of these squares was initially {1} in {0}?
        // Which of these squares was initially black in Langton’s Anteater?
        [Question.LangtonsAnteaterInitialState] = new()
        {
            QuestionText = "{0}の初期状態で{1}になっていたのはどのセル？",
            FormatArgs = new Dictionary<string, string>
            {
                ["black"] = "黒",
                ["white"] = "白",
            },
        },

        // Lasers
        // What was the number on the {1} hatch on {0}?
        // What was the number on the top-left hatch on Lasers?
        [Question.LasersHatches] = new()
        {
            QuestionText = "{0}の{1}のハッチの番号は？ ",
            ModuleName = "レーザー",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-middle"] = "上",
                ["top-right"] = "右上",
                ["middle-left"] = "左",
                ["center"] = "中央",
                ["middle-right"] = "右",
                ["bottom-left"] = "左下",
                ["bottom-middle"] = "下",
                ["bottom-right"] = "右下",
            },
        },

        // LED Encryption
        // What was the correct letter you pressed in the {1} stage of {0}?
        // What was the correct letter you pressed in the first stage of LED Encryption?
        [Question.LEDEncryptionPressedLetters] = new()
        {
            QuestionText = "{0}のステージ{1}で押した正しい文字は？",
            ModuleName = "暗号化LED",
        },

        // LED Grid
        // How many LEDs were unlit in {0}?
        // How many LEDs were unlit in LED Grid?
        [Question.LEDGridNumBlack] = new()
        {
            QuestionText = "{0}で消灯していたLEDの数は？",
            ModuleName = "LEDグリッド",
        },

        // LED Math
        // What color was {1} in {0}?
        // What color was LED A in LED Math?
        [Question.LEDMathLights] = new()
        {
            QuestionText = "{0}における{1}の色は？",
            ModuleName = "LED算",
            FormatArgs = new Dictionary<string, string>
            {
                ["LED A"] = "LED A",
                ["LED B"] = "LED B",
                ["the operator LED"] = "演算子のLED",
            },
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
        [Question.LEDsOriginalColor] = new()
        {
            QuestionText = "{0}のLEDの初期色は？",
            ModuleName = "LEDセット",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Orange"] = "オレンジ",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Purple"] = "紫",
                ["Black"] = "黒",
                ["White"] = "白",
            },
        },

        // LEGOs
        // What were the dimensions of the {1} piece in {0}?
        // What were the dimensions of the red piece in LEGOs?
        [Question.LEGOsPieceDimensions] = new()
        {
            QuestionText = "{0}の{1}のピースの大きさは？",
            ModuleName = "LEGO",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤色",
                ["green"] = "緑色",
                ["blue"] = "青色",
                ["cyan"] = "シアン",
                ["magenta"] = "マゼンタ",
                ["yellow"] = "黄色",
            },
        },

        // Letter Math
        // What was the letter on the {1} display in {0}?
        // What was the letter on the left display in Letter Math?
        [Question.LetterMathDisplay] = new()
        {
            QuestionText = "{0}のディスプレーの{1}に表示されていた英字は？",
            ModuleName = "英字数学",
            FormatArgs = new Dictionary<string, string>
            {
                ["left"] = "左",
                ["right"] = "右",
            },
        },

        // Light Bulbs
        // What was the color of the {1} bulb in {0}?
        // What was the color of the left bulb in Light Bulbs?
        [Question.LightBulbsColors] = new()
        {
            QuestionText = "{0}の{1}の電球の色は？",
            ModuleName = "電球セット",
            FormatArgs = new Dictionary<string, string>
            {
                ["left"] = "左",
                ["right"] = "右",
            },
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Orange"] = "オレンジ",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Purple"] = "紫",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
            },
        },

        // Linq
        // What was the {1} function in {0}?
        // What was the first function in Linq?
        [Question.LinqFunction] = new()
        {
            NeedsTranslation = true,
            QuestionText = "{0}の{1}番目の関数は？",
            ModuleName = "リンク",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Linq whose {0} function was {1}"] = "the Linq whose {0} function was {1}",
            },
        },

        // Lion’s Share
        // Which year was displayed on {0}?
        // Which year was displayed on Lion’s Share?
        [Question.LionsShareYear] = new()
        {
            QuestionText = "{0}に表示された年は？",
            ModuleName = "ライオンの分け前",
        },
        // Which lion was present but removed in {0}?
        // Which lion was present but removed in Lion’s Share?
        [Question.LionsShareRemovedLions] = new()
        {
            QuestionText = "{0}に存在したが除外されたライオンは？",
            ModuleName = "ライオンの分け前",
        },

        // Listening
        // What clip was played in {0}?
        // What clip was played in Listening?
        [Question.ListeningSound] = new()
        {
            QuestionText = "{0}で再生されたクリップは？",
            ModuleName = "リスニング",
        },

        // Literal Maze
        // Which letter was in this position in {0}?
        // Which letter was in this position in Literal Maze?
        [Question.LiteralMazeLetter] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which letter was in this position in {0}?",
        },

        // Logical Buttons
        // What was the color of the {1} button in the {2} stage of {0}?
        // What was the color of the top button in the first stage of Logical Buttons?
        [Question.LogicalButtonsColor] = new()
        {
            QuestionText = "{0}のステージ{2}における{1}のボタンの色は？",
            ModuleName = "論理ボタン",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
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
        },
        // What was the label on the {1} button in the {2} stage of {0}?
        // What was the label on the top button in the first stage of Logical Buttons?
        [Question.LogicalButtonsLabel] = new()
        {
            QuestionText = "{0}のステージ{2}における{1}のボタンのラベルは？",
            ModuleName = "論理ボタン",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
        },
        // What was the final operator in the {1} stage of {0}?
        // What was the final operator in the first stage of Logical Buttons?
        [Question.LogicalButtonsOperator] = new()
        {
            QuestionText = "{0}のステージ{1}で最終的に使用した演算子は？",
            ModuleName = "論理ボタン",
        },

        // Logic Gates
        // What was {1} in {0}?
        // What was gate A in Logic Gates?
        [Question.LogicGatesGates] = new()
        {
            QuestionText = "{0}で{1}はどれだったか？",
            ModuleName = "論理ゲート",
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
        [Question.LombaxCubesLetters] = new()
        {
            QuestionText = "{0}の{1}番目の英字は？",
            ModuleName = "ロンバックスキューブ",
        },

        // The London Underground
        // Where did the {1} journey on {0} {2}?
        // Where did the first journey on The London Underground depart from?
        [Question.LondonUndergroundStations] = new()
        {
            QuestionText = "{0}の{1}番目の経路における{2}は？",
            ModuleName = "ロンドン地下鉄",
            FormatArgs = new Dictionary<string, string>
            {
                ["depart from"] = "出発駅",
                ["arrive to"] = "到着駅",
            },
        },

        // Long Words
        // What was the word on the top display on {0}?
        // What was the word on the top display on Long Words?
        [Question.LongWordsWord] = new()
        {
            QuestionText = "{0}の上のディスプレーに表示された単語は？",
        },

        // Mad Memory
        // What was on the display in the {1} stage of {0}?
        // What was on the display in the first stage of Mad Memory?
        [Question.MadMemoryDisplays] = new()
        {
            QuestionText = "{0}のステージ{1}における表示は？",
            ModuleName = "狂気記憶",
            FormatArgs = new Dictionary<string, string>
            {
                ["first"] = "1",
                ["second"] = "2",
                ["third"] = "3",
                ["4th"] = "4",
            },
        },

        // Mafia
        // Who was a player, but not the Godfather, in {0}?
        // Who was a player, but not the Godfather, in Mafia?
        [Question.MafiaPlayers] = new()
        {
            QuestionText = "{0}でゴッドファーザーではなかったがプレイヤーだったのは？",
            ModuleName = "マフィア",
        },

        // Magenta Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Magenta Cipher?
        [Question.MagentaCipherScreen] = new()
        {
            QuestionText = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
            ModuleName = "マゼンタ暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Mahjong
        // Which tile was part of the {1} matched pair in {0}?
        // Which tile was part of the first matched pair in Mahjong?
        [Question.MahjongMatches] = new()
        {
            QuestionText = "{0}の{1}番目にマッチした牌のペアに含まれるのは？",
            ModuleName = "麻雀パズル",
        },
        // Which tile was shown in the bottom-left of {0}?
        // Which tile was shown in the bottom-left of Mahjong?
        [Question.MahjongCountingTile] = new()
        {
            QuestionText = "{0}の左下に表示された牌は？",
            ModuleName = "麻雀パズル",
        },

        // Main Page
        // Which color did the bubble not display in {0}?
        // Which color did the bubble not display in Main Page?
        [Question.MainPageBubbleColors] = new()
        {
            QuestionText = "{0}で表示されなかった吹き出しの色は？",
            Answers = new Dictionary<string, string>
            {
                ["Blue"] = "青",
                ["Green"] = "緑",
                ["Red"] = "赤",
                ["Yellow"] = "黄",
            },
        },
        // Which main page did the {1} button’s effect come from in {0}?
        // Which main page did the toons button’s effect come from in Main Page?
        [Question.MainPageButtonEffectOrigin] = new()
        {
            QuestionText = "{0}で{1}ボタンのエフェクトの元となったメインページは？",
            FormatArgs = new Dictionary<string, string>
            {
                ["toons"] = "トゥーン",
                ["games"] = "ゲーム",
                ["characters"] = "キャラクター",
                ["downloads"] = "ダウンロード",
                ["store"] = "ストア",
                ["email"] = "メール",
            },
        },
        // Which of the following messages did the bubble {1} in {0}?
        // Which of the following messages did the bubble display in Main Page?
        [Question.MainPageBubbleMessages] = new()
        {
            QuestionText = "{0}で吹き出しに表示され{1}メッセージは？",
            FormatArgs = new Dictionary<string, string>
            {
                ["display"] = "た",
                ["not display"] = "なかった",
            },
        },
        // Which main page did {1} come from in {0}?
        // Which main page did Homestar come from in Main Page?
        [Question.MainPageHomestarBackgroundOrigin] = new()
        {
            QuestionText = "{0}で{1}の元となったメインページは？",
            FormatArgs = new Dictionary<string, string>
            {
                ["Homestar"] = "Homestar",
                ["the background"] = "背景",
            },
        },

        // M&Ms
        // What color was the text on the {1} button in {0}?
        // What color was the text on the first button in M&Ms?
        [Question.MandMsColors] = new()
        {
            QuestionText = "{0}の{1}番目のボタンの色は？",
            ModuleName = "MとM",
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
        [Question.MandMsLabels] = new()
        {
            QuestionText = "{0}の{1}番目のボタン上にあったテキストは？",
            ModuleName = "MとM",
        },

        // M&Ns
        // What color was the text on the {1} button in {0}?
        // What color was the text on the first button in M&Ns?
        [Question.MandNsColors] = new()
        {
            QuestionText = "{0}の{1}番目のボタン上にあったテキストの色は？",
            ModuleName = "MとN",
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
        [Question.MandNsLabel] = new()
        {
            QuestionText = "{0}の正しいボタンのテキストは？",
            ModuleName = "MとN",
        },

        // Maritime Flags
        // What bearing was signalled in {0}?
        // What bearing was signalled in Maritime Flags?
        [Question.MaritimeFlagsBearing] = new()
        {
            QuestionText = "{0}が示していた方角は？",
            ModuleName = "海上旗",
        },
        // Which callsign was signalled in {0}?
        // Which callsign was signalled in Maritime Flags?
        [Question.MaritimeFlagsCallsign] = new()
        {
            QuestionText = "{0}で送信されたコールサインは？",
            ModuleName = "海上旗",
        },

        // Maritime Semaphore
        // In which position was the dummy in {0}?
        // In which position was the dummy in Maritime Semaphore?
        [Question.MaritimeSemaphoreDummy] = new()
        {
            QuestionText = "{0}でダミーの信号があった位置は？",
            ModuleName = "海上セマフォア信号",
        },
        // Which letter was shown by the {2} in the {1} position in {0}?
        // Which letter was shown by the left flag in the first position in Maritime Semaphore?
        [Question.MaritimeSemaphoreLetter] = new()
        {
            QuestionText = "{0}の{1}番目の{2}が表示した英字は？",
            ModuleName = "海上セマフォア信号",
            FormatArgs = new Dictionary<string, string>
            {
                ["left flag"] = "左の旗",
                ["right flag"] = "右の旗",
                ["semaphore"] = "セマフォア信号",
            },
        },

        // The Maroon Button
        // What was A in {0}?
        // What was A in The Maroon Button?
        [Question.MaroonButtonA] = new()
        {
            QuestionText = "{0}のAは？",
        },

        // Maroon Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Maroon Cipher?
        [Question.MaroonCipherScreen] = new()
        {
            QuestionText = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
            ModuleName = "栗色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Mashematics
        // What was the answer in {0}?
        // What was the answer in Mashematics?
        [Question.MashematicsAnswer] = new()
        {
            QuestionText = "{0}の答えは？",
            ModuleName = "連打算数",
        },
        // What was the {1} number in the equation on {0}?
        // What was the first number in the equation on Mashematics?
        [Question.MashematicsCalculation] = new()
        {
            QuestionText = "{0}の方程式にあった{1}番目の数字は？",
            ModuleName = "連打算数",
        },

        // Master Tapes
        // Which song was played in {0}?
        // Which song was played in Master Tapes?
        [Question.MasterTapesPlayedSong] = new()
        {
            QuestionText = "{0}で再生された歌は？",
            ModuleName = "マスターテープ",
        },

        // Match Refereeing
        // Which planet was present in the {1} stage of {0}?
        // Which planet was present in the first stage of Match Refereeing?
        [Question.MatchRefereeingPlanet] = new()
        {
            QuestionText = "{0}のステージ{1}で存在した惑星は？",
        },

        // Math ’em
        // What was the color of this tile before the shuffle on {0}?
        // What was the color of this tile before the shuffle on Math ’em?
        [Question.MathEmColor] = new()
        {
            QuestionText = "{0}のシャッフル前におけるこのタイルの色は？",
            ModuleName = "計算神経衰弱",
            Answers = new Dictionary<string, string>
            {
                ["White"] = "白",
                ["Bronze"] = "銅",
                ["Silver"] = "銀",
                ["Gold"] = "金",
            },
        },
        // What was the design on this tile before the shuffle on {0}?
        // What was the design on this tile before the shuffle on Math ’em?
        [Question.MathEmLabel] = new()
        {
            QuestionText = "{0}のシャッフル前におけるこのタイルのデザインは？",
            ModuleName = "計算神経衰弱",
        },

        // The Matrix
        // Which word was part of the latest access code in {0}?
        // Which word was part of the latest access code in The Matrix?
        [Question.MatrixAccessCode] = new()
        {
            QuestionText = "{0}における最後のアクセスコードの一部であった単語は？",
            ModuleName = "マトリックス",
        },
        // What was the glitched word in {0}?
        // What was the glitched word in The Matrix?
        [Question.MatrixGlitchWord] = new()
        {
            QuestionText = "{0}でグリッチされていた単語は？",
            ModuleName = "マトリックス",
        },

        // Maze
        // In which {1} was the starting position in {0}, counting from the {2}?
        // In which column was the starting position in Maze, counting from the left?
        [Question.MazeStartingPosition] = new()
        {
            QuestionText = "{0}のスタート地点の{1}は{2}から何番目？",
            ModuleName = "迷路",
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
        [Question.Maze3StartingFace] = new()
        {
            QuestionText = "{0}の開始面の色は？",
            ModuleName = "迷路³",
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
        [Question.MazeIdentificationSeed] = new()
        {
            QuestionText = "{0}の迷路のシード値は？",
        },
        // What was the function of button {1} in {0}?
        // What was the function of button 1 in Maze Identification?
        [Question.MazeIdentificationNum] = new()
        {
            QuestionText = "{0}のボタン{1}の機能は？",
            Answers = new Dictionary<string, string>
            {
                ["Forwards"] = "前進",
                ["Clockwise"] = "時計回り",
                ["Backwards"] = "後退",
                ["Counter-clockwise"] = "反時計回り",
            },
        },
        // Which button {1} in {0}?
        // Which button moved you forwards in Maze Identification?
        [Question.MazeIdentificationFunc] = new()
        {
            QuestionText = "{0}で{1}ボタンは？",
            FormatArgs = new Dictionary<string, string>
            {
                ["moved you forwards"] = "前進する",
                ["turned you clockwise"] = "時計回りに回る",
                ["moved you backwards"] = "後退する",
                ["turned you counter-clockwise"] = "反時計回りに回る",
            },
        },

        // Mazematics
        // Which was the {1} value in {0}?
        // Which was the initial value in Mazematics?
        [Question.MazematicsValue] = new()
        {
            QuestionText = "{0}の{1}値は？",
            ModuleName = "計算迷路",
            FormatArgs = new Dictionary<string, string>
            {
                ["initial"] = "初期",
                ["goal"] = "目的",
            },
        },

        // Maze Scrambler
        // What was the starting position on {0}?
        // What was the starting position on Maze Scrambler?
        [Question.MazeScramblerStart] = new()
        {
            QuestionText = "{0}の開始位置は？",
            ModuleName = "迷路スクランブラー",
            Answers = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-middle"] = "上",
                ["top-right"] = "右上",
                ["middle-left"] = "左",
                ["middle-middle"] = "中央",
                ["middle-right"] = "右",
                ["bottom-left"] = "左下",
                ["bottom-middle"] = "下",
                ["bottom-right"] = "右下",
            },
        },
        // What was the goal on {0}?
        // What was the goal on Maze Scrambler?
        [Question.MazeScramblerGoal] = new()
        {
            QuestionText = "{0}のゴール位置は？",
            ModuleName = "迷路スクランブラー",
            Answers = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-middle"] = "上",
                ["top-right"] = "右上",
                ["middle-left"] = "左",
                ["middle-middle"] = "中央",
                ["middle-right"] = "右",
                ["bottom-left"] = "左下",
                ["bottom-middle"] = "下",
                ["bottom-right"] = "右下",
            },
        },
        // Which of these positions was a maze marking on {0}?
        // Which of these positions was a maze marking on Maze Scrambler?
        [Question.MazeScramblerIndicators] = new()
        {
            QuestionText = "{0}の迷路を求めるマークの位置はどれ？",
            ModuleName = "迷路スクランブラー",
            Answers = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-middle"] = "上",
                ["top-right"] = "右上",
                ["middle-left"] = "左",
                ["center"] = "中央",
                ["middle-right"] = "右",
                ["bottom-left"] = "左下",
                ["bottom-middle"] = "下",
                ["bottom-right"] = "右下",
            },
        },

        // Mazeseeker
        // How many walls surrounded this cell in {0}?
        // How many walls surrounded this cell in Mazeseeker?
        [Question.MazeseekerCell] = new()
        {
            QuestionText = "{0}でこのセルの周囲にあった壁は？",
        },
        // Where was the start in {0}?
        // Where was the start in Mazeseeker?
        [Question.MazeseekerStart] = new()
        {
            QuestionText = "{0}の開始地点は？",
        },
        // Where was the goal in {0}?
        // Where was the goal in Mazeseeker?
        [Question.MazeseekerGoal] = new()
        {
            QuestionText = "{0}のゴール地点は？",
        },

        // Maze Swap
        // Where was the {1} position in {0}?
        // Where was the starting position in Maze Swap?
        [Question.MazeSwapPosition] = new()
        {
            QuestionText = "{0}の{1}位置は？",
            ModuleName = "入れ替え迷路",
            FormatArgs = new Dictionary<string, string>
            {
                ["starting"] = "スタート",
                ["goal"] = "ゴール",
            },
        },

        // Mega Man 2
        // Which master was shown in {0}?
        // Which master was shown in Mega Man 2?
        [Question.MegaMan2Master] = new()
        {
            QuestionText = "{0}で表示されたマスターは？",
            ModuleName = "ロックマン2",
        },
        // Which weapon was shown in {0}?
        // Which weapon was shown in Mega Man 2?
        [Question.MegaMan2Weapon] = new()
        {
            QuestionText = "{0}で表示された武器は？",
            ModuleName = "ロックマン2",
        },

        // Melody Sequencer
        // Which part was in slot #{1} at the start of {0}?
        // Which part was in slot #1 at the start of Melody Sequencer?
        [Question.MelodySequencerSlots] = new()
        {
            QuestionText = "{0}の開始時にスロット{1}に入っていたのは？",
            ModuleName = "メロディーシークエンス",
        },
        // Which slot contained part #{1} at the start of {0}?
        // Which slot contained part #1 at the start of Melody Sequencer?
        [Question.MelodySequencerParts] = new()
        {
            QuestionText = "{0}の開始時にパート{1}が入っていたスロットは？",
            ModuleName = "メロディーシークエンス",
        },

        // Memorable Buttons
        // What was the {1} correct symbol pressed in {0}?
        // What was the first correct symbol pressed in Memorable Buttons?
        [Question.MemorableButtonsSymbols] = new()
        {
            QuestionText = "{0}の{1}番目の正しいシンボルは？",
            ModuleName = "記憶ボタン",
        },

        // Memory
        // What was the displayed number in the {1} stage of {0}?
        // What was the displayed number in the first stage of Memory?
        [Question.MemoryDisplay] = new()
        {
            QuestionText = "{0}のステージ{1}で表示された数は？",
            ModuleName = "記憶",
        },
        // In what position was the button that you pressed in the {1} stage of {0}?
        // In what position was the button that you pressed in the first stage of Memory?
        [Question.MemoryPosition] = new()
        {
            QuestionText = "{0}のステージ{1}で押したボタンの位置は？",
            ModuleName = "記憶",
        },
        // What was the label of the button that you pressed in the {1} stage of {0}?
        // What was the label of the button that you pressed in the first stage of Memory?
        [Question.MemoryLabel] = new()
        {
            QuestionText = "{0}のステージ{1}で押したボタンのラベルは？",
            ModuleName = "記憶",
        },

        // Memory Wires
        // What was the digit displayed in the {1} stage of {0}?
        // What was the digit displayed in the first stage of Memory Wires?
        [Question.MemoryWiresDisplayedDigits] = new()
        {
            QuestionText = "{0}のステージ{1}で表示された数字は？",
            ModuleName = "記憶ワイヤ",
        },
        // What was the colour of wire {1} in {0}?
        // What was the colour of wire 1 in Memory Wires?
        [Question.MemoryWiresWireColours] = new()
        {
            QuestionText = "{0}のワイヤ{1}の色は？",
            ModuleName = "記憶ワイヤ",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Yellow"] = "黄",
                ["Blue"] = "青",
                ["White"] = "白",
                ["Black"] = "黒",
            },
        },

        // Metamorse
        // What was the extracted letter in {0}?
        // What was the extracted letter in Metamorse?
        [Question.MetamorseExtractedLetter] = new()
        {
            QuestionText = "{0}で抽出された英字は？",
            ModuleName = "メタモールス",
        },

        // Metapuzzle
        // What was the final answer in {0}?
        // What was the final answer in Metapuzzle?
        [Question.MetapuzzleAnswer] = new()
        {
            QuestionText = "{0}の最終的な答えは？",
            ModuleName = "メタパズル",
        },

        // Minsk Metro
        // What was the name of starting station in {0}?
        // What was the name of starting station in Minsk Metro?
        [Question.MinskMetroStation] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What was the name of starting station in {0}?",
        },

        // Mirror
        // What was the second word written by the original ghost in {0}?
        // What was the second word written by the original ghost in Mirror?
        [Question.MirrorWord] = new()
        {
            QuestionText = "{0}の幽霊が二回目に書いた単語は？",
            ModuleName = "鏡",
        },

        // Mister Softee
        // Where was the SpongeBob Bar on {0}?
        // Where was the SpongeBob Bar on Mister Softee?
        [Question.MisterSofteeSpongebobPosition] = new()
        {
            QuestionText = "{0}のスポンジボブ・バーがあった場所は？",
            ModuleName = "ミスター・ソフティー",
            Answers = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-middle"] = "上",
                ["top-right"] = "右上",
                ["middle-left"] = "左",
                ["middle-middle"] = "中央",
                ["middle-right"] = "右",
                ["bottom-left"] = "左下",
                ["bottom-middle"] = "下",
                ["bottom-right"] = "右下",
            },
        },
        // Which treat was present on {0}?
        // Which treat was present on Mister Softee?
        [Question.MisterSofteeTreatsPresent] = new()
        {
            QuestionText = "{0}に存在していたお菓子は？",
            ModuleName = "ミスター・ソフティー",
        },

        // Mixometer
        // What was the position of the submit button in {0}?
        // What was the position of the submit button in Mixometer?
        [Question.MixometerSubmitButton] = new()
        {
            QuestionText = "{0}で送信ボタンがあった位置は？",
        },

        // Modern Cipher
        // What was the decrypted word of the {1} stage in {0}?
        // What was the decrypted word of the first stage in Modern Cipher?
        [Question.ModernCipherWord] = new()
        {
            QuestionText = "{0}のステージ{1}で復号された単語は？",
            ModuleName = "現代暗号",
        },

        // Module Listening
        // Which sound did the {1} button play in {0}?
        // Which sound did the red button play in Module Listening?
        [Question.ModuleListeningButtonAudio] = new()
        {
            QuestionText = "{0}で{1}色のボタンが再生した音は？",
            ModuleName = "モジュールリスニング",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
                ["yellow"] = "黄",
            },
        },
        // Which sound played in {0}?
        // Which sound played in Module Listening?
        [Question.ModuleListeningAnyAudio] = new()
        {
            QuestionText = "{0}で再生された音は？",
            ModuleName = "モジュールリスニング",
        },

        // Module Maneuvers
        // What was the goal location in {0}?
        // What was the goal location in Module Maneuvers?
        [Question.ModuleManeuversGoal] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What was the goal location in {0}?",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["{0}, {1}"] = "{0}, {1}",
            },
        },

        // Module Maze
        // Which of the following was the starting icon for {0}?
        // Which of the following was the starting icon for Module Maze?
        [Question.ModuleMazeStartingIcon] = new()
        {
            QuestionText = "{0}の開始アイコンは？",
            ModuleName = "モジュール迷路",
        },

        // Module Movements
        // What was the {1} module shown in {0}?
        // What was the first module shown in Module Movements?
        [Question.ModuleMovementsDisplay] = new()
        {
            QuestionText = "{0}の{1}番目のモジュールは(英名)？",
            ModuleName = "モジュール追跡",
        },

        // Money Game
        // What were the first and second words in the {1} phrase in {0}?
        // What were the first and second words in the first phrase in Money Game?
        [Question.MoneyGame1] = new()
        {
            QuestionText = "{0}の{1}フレーズ目で使用された1,2番目の単語は？",
            ModuleName = "マネーゲーム",
        },
        // What were the third and fourth words in the {1} phrase in {0}?
        // What were the third and fourth words in the first phrase in Money Game?
        [Question.MoneyGame2] = new()
        {
            QuestionText = "{0}の{1}フレーズ目で使用された3,4番目の単語は？",
            ModuleName = "マネーゲーム",
        },
        // What was the end of the {1} phrase in {0}?
        // What was the end of the first phrase in Money Game?
        [Question.MoneyGame3] = new()
        {
            QuestionText = "{0}の{1}フレーズ目で使用された最後の単語は？",
            ModuleName = "マネーゲーム",
        },

        // Monsplode, Fight!
        // Which creature was displayed in {0}?
        // Which creature was displayed in Monsplode, Fight!?
        [Question.MonsplodeFightCreature] = new()
        {
            QuestionText = "{0}で表示されたモンスターは？",
            ModuleName = "モンスプロード・ファイト！",
        },
        // Which one of these moves {1} selectable in {0}?
        // Which one of these moves was selectable in Monsplode, Fight!?
        [Question.MonsplodeFightMove] = new()
        {
            QuestionText = "{0}で{1}わざに含まれるのは？",
            ModuleName = "モンスプロード・ファイト！",
            FormatArgs = new Dictionary<string, string>
            {
                ["was"] = "存在した",
                ["was not"] = "存在しなかった",
            },
        },

        // Monsplode Trading Cards
        // What was the {1} before the last action in {0}?
        // What was the first card in your hand before the last action in Monsplode Trading Cards?
        [Question.MonsplodeTradingCardsCards] = new()
        {
            QuestionText = "{0}の最後の行動前における{1}は？",
            ModuleName = "モンスプロード・カードゲーム",
            FormatArgs = new Dictionary<string, string>
            {
                ["first card in your hand"] = "手札の1枚目",
                ["second card in your hand"] = "手札の2枚目",
                ["third card in your hand"] = "手札の3枚目",
                ["card on offer"] = "相手のカード",
            },
        },
        // What was the print version of the {1} before the last action in {0}?
        // What was the print version of the first card in your hand before the last action in Monsplode Trading Cards?
        [Question.MonsplodeTradingCardsPrintVersions] = new()
        {
            QuestionText = "{0}の最後の行動前における{1}に書かれた印刷バージョンは？",
            ModuleName = "モンスプロード・カードゲーム",
            FormatArgs = new Dictionary<string, string>
            {
                ["first card in your hand"] = "手札の1枚目",
                ["second card in your hand"] = "手札の2枚目",
                ["third card in your hand"] = "手札の3枚目",
                ["card on offer"] = "相手のカード",
            },
        },

        // The Moon
        // What was the {1} set in clockwise order in {0}?
        // What was the first initially lit set in clockwise order in The Moon?
        [Question.MoonLitUnlit] = new()
        {
            QuestionText = "{0}で時計回りに見て{1}セットは？",
            ModuleName = "月",
            FormatArgs = new Dictionary<string, string>
            {
                ["first initially lit"] = "1番目の点灯",
                ["second initially lit"] = "2番目の点灯",
                ["third initially lit"] = "3番目の点灯",
                ["fourth initially lit"] = "4番目の点灯",
                ["first initially unlit"] = "1番目の消灯",
                ["second initially unlit"] = "2番目の消灯",
                ["third initially unlit"] = "3番目の消灯",
                ["fourth initially unlit"] = "4番目の消灯",
            },
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
        [Question.MoreCodeWord] = new()
        {
            QuestionText = "{0}で点滅した単語は？",
            ModuleName = "新モールス信号",
        },

        // Morse-A-Maze
        // What was the starting location in {0}?
        // What was the starting location in Morse-A-Maze?
        [Question.MorseAMazeStartingCoordinate] = new()
        {
            QuestionText = "{0}の開始位置は？",
            ModuleName = "モールス迷路",
        },
        // What was the ending location in {0}?
        // What was the ending location in Morse-A-Maze?
        [Question.MorseAMazeEndingCoordinate] = new()
        {
            QuestionText = "{0}のゴール位置は？",
            ModuleName = "モールス迷路",
        },
        // What was the word shown as Morse code in {0}?
        // What was the word shown as Morse code in Morse-A-Maze?
        [Question.MorseAMazeMorseCodeWord] = new()
        {
            QuestionText = "{0}でモールス信号で表示された単語は？",
            ModuleName = "モールス迷路",
        },

        // Morse Buttons
        // What was the character flashed by the {1} button in {0}?
        // What was the character flashed by the first button in Morse Buttons?
        [Question.MorseButtonsButtonLabel] = new()
        {
            QuestionText = "{0}の{1}番目のボタンで点滅した文字は？",
            ModuleName = "モールスボタン",
        },
        // What was the color flashed by the {1} button in {0}?
        // What was the color flashed by the first button in Morse Buttons?
        [Question.MorseButtonsButtonColor] = new()
        {
            QuestionText = "{0}の{1}番目のボタンで点滅した色は？",
            ModuleName = "モールスボタン",
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
        [Question.MorsematicsReceivedLetters] = new()
        {
            QuestionText = "{0}で{1}番目に受信した文字は？",
            ModuleName = "モールスマティック",
        },

        // Morse War
        // What were the LEDs in the {1} row in {0} (1 = on, 0 = off)?
        // What were the LEDs in the bottom row in Morse War (1 = on, 0 = off)?
        [Question.MorseWarLeds] = new()
        {
            QuestionText = "{0}で{1}段のLEDの状態は(1=オン、0=オフ)？",
            ModuleName = "モールス戦争",
            FormatArgs = new Dictionary<string, string>
            {
                ["bottom"] = "下",
                ["middle"] = "中央",
                ["top"] = "上",
            },
        },
        // What code was transmitted in {0}?
        // What code was transmitted in Morse War?
        [Question.MorseWarCode] = new()
        {
            QuestionText = "{0}で変換した符号は？",
            ModuleName = "モールス戦争",
        },

        // Mouse in the Maze
        // What color was the torus in {0}?
        // What color was the torus in Mouse in the Maze?
        [Question.MouseInTheMazeTorus] = new()
        {
            QuestionText = "{0}の輪の色は？",
            ModuleName = "迷路のネズミ",
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
        [Question.MouseInTheMazeSphere] = new()
        {
            QuestionText = "{0}のゴールの球の色は？",
            ModuleName = "迷路のネズミ",
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
        [Question.MSeqObtained] = new()
        {
            QuestionText = "{0}の{1}番目に獲得した数字は？",
            ModuleName = "リズム正方型",
        },
        // What was the final number from the iteration process in {0}?
        // What was the final number from the iteration process in M-Seq?
        [Question.MSeqSubmitted] = new()
        {
            QuestionText = "{0}の最後の繰り返しで得た最終値は？",
            ModuleName = "リズム正方型",
        },

        // Mssngv Wls
        // Which vowel was missing in {0}?
        // Which vowel was missing in \uE001Mssngv Wls\uE002?
        [Question.MssngvWlsMssNgvwL] = new()
        {
            QuestionText = "{0} de kaketeita boin ha?",
            ModuleName = "欠落母音",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["AEIOU"] = "AEIOU",
            },
        },

        // Multicolored Switches
        // What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?
        // What color was the first LED on the top row when the tiny LED was lit in Multicolored Switches?
        [Question.MulticoloredSwitchesLedColor] = new()
        {
            QuestionText = "{0}で小さなLEDが{3}時の{2}段LEDの{1}番目は？",
            ModuleName = "色どりスイッチ",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["lit"] = "点灯した",
                ["bottom"] = "下",
                ["unlit"] = "点灯していない",
            },
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

        // Murder
        // Where was the body found in {0}?
        // Where was the body found in Murder?
        [Question.MurderBodyFound] = new()
        {
            QuestionText = "{0}の死体はどこで見つかった？",
            ModuleName = "殺人",
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
            QuestionText = "{0}の{1}に含まれるのは？",
            ModuleName = "殺人",
            FormatArgs = new Dictionary<string, string>
            {
                ["a suspect but not the murderer"] = "殺人鬼ではなかった容疑者",
                ["not a suspect"] = "容疑者ではなかった人物",
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
            QuestionText = "{0}の{1}に含まれるのは？",
            ModuleName = "殺人",
            FormatArgs = new Dictionary<string, string>
            {
                ["a potential weapon but not the murder weapon"] = "凶器ではない候補にあった武器",
                ["not a potential weapon"] = "候補に無かった武器",
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
            QuestionText = "{0}で最初に解除するように指示されたモジュールは(英名)？",
            ModuleName = "ミステリーモジュール",
        },
        // Which module was hidden by {0}?
        // Which module was hidden by Mystery Module?
        [Question.MysteryModuleHiddenModule] = new()
        {
            QuestionText = "{0}で隠されていたモジュールは(英名)？",
            ModuleName = "ミステリーモジュール",
        },

        // Mystic Square
        // Where was the skull in {0}?
        // Where was the skull in Mystic Square?
        [Question.MysticSquareSkull] = new()
        {
            QuestionText = "{0}のどくろの位置は？",
            ModuleName = "神秘スクエア",
            Answers = new Dictionary<string, string>
            {
                ["top left"] = "左上",
                ["top middle"] = "上",
                ["top right"] = "右上",
                ["middle left"] = "左",
                ["center"] = "中央",
                ["middle right"] = "右",
                ["bottom left"] = "左下",
                ["bottom middle"] = "下",
                ["bottom right"] = "右下",
            },
        },

        // Name Codes
        // What was the {1} index in {0}?
        // What was the left index in Name Codes?
        [Question.NameCodesIndices] = new()
        {
            QuestionText = "{0}の{1}のインデックスは？",
            FormatArgs = new Dictionary<string, string>
            {
                ["left"] = "左",
                ["right"] = "右",
            },
        },

        // Naming Conventions
        // What was the label of the first button in {0}?
        // What was the label of the first button in Naming Conventions?
        [Question.NamingConventionsObject] = new()
        {
            QuestionText = "{0}の最初のボタンに書かれたラベルは？",
            ModuleName = "命名規則",
        },

        // N&Ms
        // What was the label of the correct button in {0}?
        // What was the label of the correct button in N&Ms?
        [Question.NandMsAnswer] = new()
        {
            QuestionText = "{0}の正しいボタンのラベルは？",
            ModuleName = "NとM",
        },

        // N&Ns
        // Which label was present in the {1} stage of {0}?
        // Which label was present in the first stage of N&Ns?
        [Question.NandNsLabel] = new()
        {
            QuestionText = "{0}の{1}ステージで表示されたラベルは？",
            ModuleName = "NとN",
        },
        // Which color was missing in the third stage of {0}?
        // Which color was missing in the third stage of N&Ns?
        [Question.NandNsColor] = new()
        {
            QuestionText = "{0}のステージ3で表示された色は？",
            ModuleName = "NとN",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Orange"] = "オレンジ",
                ["Blue"] = "青",
                ["Yellow"] = "黄",
                ["Brown"] = "茶色",
            },
        },

        // Navigation Determination
        // What was the color of the maze in {0}?
        // What was the color of the maze in Navigation Determination?
        [Question.NavigationDeterminationColor] = new()
        {
            QuestionText = "{0}の迷路の色は？",
            ModuleName = "ナビ決定",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Blue"] = "青",
            },
        },
        // What was the label of the maze in {0}?
        // What was the label of the maze in Navigation Determination?
        [Question.NavigationDeterminationLabel] = new()
        {
            QuestionText = "{0}の迷路のラベルは？",
            ModuleName = "ナビ決定",
        },

        // Navinums
        // What was the initial middle digit in {0}?
        // What was the initial middle digit in Navinums?
        [Question.NavinumsMiddleDigit] = new()
        {
            QuestionText = "{0}の最初の中央の値は？",
            ModuleName = "ナビ数字",
        },
        // What was the {1} directional button pressed in {0}?
        // What was the first directional button pressed in Navinums?
        [Question.NavinumsDirectionalButtons] = new()
        {
            QuestionText = "{0}の{1}番目に押したボタンの方向は？",
            ModuleName = "ナビ数字",
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
        [Question.NavyButtonGreekLetters] = new()
        {
            QuestionText = "{0}に表示されたギリシャ文字は(大文字小文字区別あり)？",
        },
        // What was the {1} of the given in {0} (0-indexed)?
        // What was the column of the given in The Navy Button (0-indexed)?
        [Question.NavyButtonGiven] = new()
        {
            QuestionText = "{0}で得られた{1}は(0から開始)？",
            FormatArgs = new Dictionary<string, string>
            {
                ["column"] = "列",
                ["row"] = "段",
                ["value"] = "値",
            },
        },

        // The Necronomicon
        // What was the chapter number of the {1} page in {0}?
        // What was the chapter number of the first page in The Necronomicon?
        [Question.NecronomiconChapters] = new()
        {
            QuestionText = "{0}の{1}番目のページの章番号は？",
            ModuleName = "ネクロノミコン",
        },

        // Negativity
        // In base 10, what was the value submitted in {0}?
        // In base 10, what was the value submitted in Negativity?
        [Question.NegativitySubmittedValue] = new()
        {
            QuestionText = "{0}で送信した値は十進数でいくつ？",
            ModuleName = "負極性",
        },
        // Excluding 0s, what was the submitted balanced ternary in {0}?
        // Excluding 0s, what was the submitted balanced ternary in Negativity?
        [Question.NegativitySubmittedTernary] = new()
        {
            QuestionText = "0を除き、{0}で送信した値は均衡三進数でいくつ？",
            ModuleName = "負極性",
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
            QuestionText = "{0}の酸の色は？",
            ModuleName = "中和滴定",
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
        [Question.NeutralizationVolume] = new()
        {
            QuestionText = "{0}の酸の量は？",
            ModuleName = "中和滴定",
        },

        // Next In Line
        // What color was the first wire in {0}?
        // What color was the first wire in Next In Line?
        [Question.NextInLineFirstWire] = new()
        {
            QuestionText = "{0}の最初のワイヤの色は？",
            ModuleName = "ネクストライン",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Orange"] = "オレンジ",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Black"] = "黒",
                ["White"] = "白",
                ["Gray"] = "灰色",
            },
        },

        // ❖
        // Which button flashed in the {1} stage in {0}?
        // Which button flashed in the first stage in ❖?
        // Note: This question is depicted visually, rather than with words. A translation here will only be used for logging.
        [Question.NonverbalSimonFlashes] = new()
        {
            QuestionText = "{0}のステージ{1}で点滅したボタンは？",
        },

        // Not Colored Squares
        // What was the position of the square you initially pressed in {0}?
        // What was the position of the square you initially pressed in Not Colored Squares?
        [Question.NotColoredSquaresInitialPosition] = new()
        {
            QuestionText = "{0}の最初に押した正方形の位置は？",
            ModuleName = "偽色付き格子",
        },

        // Not Colored Switches
        // What was the encrypted word in {0}?
        // What was the encrypted word in Not Colored Switches?
        [Question.NotColoredSwitchesWord] = new()
        {
            QuestionText = "{0}で暗号化されていた単語は？",
            ModuleName = "偽色付きスイッチ",
        },

        // Not Colour Flash
        // What was {1} in the displayed word sequence in {0}?
        // What was first in the displayed word sequence in Not Colour Flash?
        [Question.NotColourFlashInitialWord] = new()
        {
            QuestionText = "{0}の単語シーケンスで{1}番目に表示された単語は？",
            ModuleName = "偽カラーフラッシュ",
        },
        // What was {1} in the displayed colour sequence in {0}?
        // What was first in the displayed colour sequence in Not Colour Flash?
        [Question.NotColourFlashInitialColour] = new()
        {
            QuestionText = "{0}の色シーケンスで{1}番目に表示された色は？",
            ModuleName = "偽カラーフラッシュ",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
                ["White"] = "白",
            },
        },

        // Not Connection Check
        // What symbol flashed on the {1} button in {0}?
        // What symbol flashed on the top left button in Not Connection Check?
        [Question.NotConnectionCheckFlashes] = new()
        {
            QuestionText = "{0}の{1}のボタンが示した記号は？",
            ModuleName = "偽接続確認",
            FormatArgs = new Dictionary<string, string>
            {
                ["top left"] = "左上",
                ["top right"] = "右上",
                ["bottom left"] = "左下",
                ["bottom right"] = "右下",
            },
        },
        // What was the value of the {1} button in {0}?
        // What was the value of the top left button in Not Connection Check?
        [Question.NotConnectionCheckValues] = new()
        {
            QuestionText = "{0}の{1}のボタンの値は？",
            ModuleName = "偽接続確認",
            FormatArgs = new Dictionary<string, string>
            {
                ["top left"] = "左上",
                ["top right"] = "右上",
                ["bottom left"] = "左下",
                ["bottom right"] = "右下",
            },
        },

        // Not Coordinates
        // Which coordinate was part of the square in {0}?
        // Which coordinate was part of the square in Not Coordinates?
        [Question.NotCoordinatesSquareCoords] = new()
        {
            QuestionText = "{0}の正方形の一部に含まれた座標は？",
            ModuleName = "偽座標",
        },

        // Not Double-Oh
        // What was the {1} displayed position in the second stage of {0}?
        // What was the first displayed position in the second stage of Not Double-Oh?
        [Question.NotDoubleOhPosition] = new()
        {
            QuestionText = "{0}の第2ステージで{1}番目に表示された図形の位置は？",
            ModuleName = "偽ダブル・オー",
        },

        // Not Keypad
        // What color flashed {1} in the final sequence in {0}?
        // What color flashed first in the final sequence in Not Keypad?
        [Question.NotKeypadColor] = new()
        {
            QuestionText = "{0}の最終的なシーケンスにおける{1}番目の点滅の色は？",
            ModuleName = "偽キーパッド",
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
        [Question.NotKeypadSymbol] = new()
        {
            QuestionText = "{0}の最終的なシーケンスにおける{1}番目の点滅の記号は？",
            ModuleName = "偽座標",
        },

        // Not Maze
        // What was the starting distance in {0}?
        // What was the starting distance in Not Maze?
        [Question.NotMazeStartingDistance] = new()
        {
            QuestionText = "{0}の開始時の距離は？",
            ModuleName = "偽迷路",
        },

        // Not Morse Code
        // What was the {1} correct word you submitted in {0}?
        // What was the first correct word you submitted in Not Morse Code?
        [Question.NotMorseCodeWord] = new()
        {
            QuestionText = "{0}の{1}番目に送信した単語は？",
            ModuleName = "偽モールス信号",
        },

        // Not Morsematics
        // What was the transmitted word on {0}?
        // What was the transmitted word on Not Morsematics?
        [Question.NotMorsematicsWord] = new()
        {
            QuestionText = "{0}で送信した単語は？",
            ModuleName = "偽モールスマティック",
        },

        // Not Murder
        // What room was {1} in initially on {0}?
        // What room was Miss Scarlett in initially on Not Murder?
        [Question.NotMurderRoom] = new()
        {
            NeedsTranslation = true,
            QuestionText = "{0}で{1}が最初にいたのはどの部屋？",
            ModuleName = "偽殺人",
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
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Not Murder where he initially held the {0}"] = "the Not Murder where he initially held the {0}",
                ["the Not Murder where she initially held the {0}"] = "the Not Murder where she initially held the {0}",
                ["the Not Murder where he started in the {0}"] = "the Not Murder where he started in the {0}",
                ["the Not Murder where she started in the {0}"] = "the Not Murder where she started in the {0}",
                ["the Not Murder where he was present"] = "the Not Murder where he was present",
                ["the Not Murder where she was present"] = "the Not Murder where she was present",
                ["Candlestick"] = "Candlestick",
                ["Dagger"] = "Dagger",
                ["Lead Pipe"] = "Lead Pipe",
                ["Revolver"] = "Revolver",
                ["Rope"] = "Rope",
                ["Spanner"] = "Spanner",
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
            QuestionText = "{0}で{1}が最初に所持していたのはどの武器？",
            ModuleName = "偽殺人",
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
            QuestionText = "{0}のステージ{2}で{1}数字は？",
            ModuleName = "偽ナンバーパッド",
            FormatArgs = new Dictionary<string, string>
            {
                ["flashed"] = "点滅した",
                ["did not flash"] = "点滅しなかった",
            },
        },

        // Not Password
        // Which letter was missing from {0}?
        // Which letter was missing from Not Password?
        [Question.NotPasswordLetter] = new()
        {
            QuestionText = "{0}で欠けていた文字は？",
            ModuleName = "偽パスワード",
        },

        // Not Perspective Pegs
        // What was the position of the {1} flashing peg on {0}?
        // What was the position of the first flashing peg on Not Perspective Pegs?
        [Question.NotPerspectivePegsPosition] = new()
        {
            QuestionText = "{0}の{1}番目に点滅したペグの位置は？",
            ModuleName = "偽奥行きペグ",
            Answers = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["top-right"] = "右上",
                ["bottom-right"] = "右下",
                ["bottom-left"] = "左下",
                ["top-left"] = "左上",
            },
        },
        // From what perspective did the {1} peg flash on {0}?
        // From what perspective did the first peg flash on Not Perspective Pegs?
        [Question.NotPerspectivePegsPerspective] = new()
        {
            QuestionText = "{0}の{1}番目に点滅したペグの面は？",
            ModuleName = "偽奥行きペグ",
            Answers = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["top-right"] = "右上",
                ["bottom-right"] = "右下",
                ["bottom-left"] = "左下",
                ["top-left"] = "左上",
            },
        },
        // What was the color of the {1} flashing peg on {0}?
        // What was the color of the first flashing peg on Not Perspective Pegs?
        [Question.NotPerspectivePegsColor] = new()
        {
            QuestionText = "{0}の{1}番目に点滅したペグの色は？",
            ModuleName = "偽奥行きペグ",
            Answers = new Dictionary<string, string>
            {
                ["blue"] = "青",
                ["green"] = "緑",
                ["purple"] = "紫",
                ["red"] = "赤",
                ["yellow"] = "黄",
            },
        },

        // Not Piano Keys
        // What was the first displayed symbol on {0}?
        // What was the first displayed symbol on Not Piano Keys?
        [Question.NotPianoKeysFirstSymbol] = new()
        {
            QuestionText = "{0}に表示された記号の一つ目は？",
            ModuleName = "偽鍵盤",
        },
        // What was the second displayed symbol on {0}?
        // What was the second displayed symbol on Not Piano Keys?
        [Question.NotPianoKeysSecondSymbol] = new()
        {
            QuestionText = "{0}に表示された記号の二つ目は？",
            ModuleName = "偽鍵盤",
        },
        // What was the third displayed symbol on {0}?
        // What was the third displayed symbol on Not Piano Keys?
        [Question.NotPianoKeysThirdSymbol] = new()
        {
            QuestionText = "{0}に表示された記号の三つ目は？",
            ModuleName = "偽鍵盤",
        },

        // Not Red Arrows
        // What was the starting number in {0}?
        // What was the starting number in Not Red Arrows?
        [Question.NotRedArrowsStart] = new()
        {
            QuestionText = "{0}の初期値は？",
            ModuleName = "偽赤色矢印",
        },

        // Not Simaze
        // Which maze was used in {0}?
        // Which maze was used in Not Simaze?
        [Question.NotSimazeMaze] = new()
        {
            QuestionText = "{0}で使用した迷路は？",
            ModuleName = "偽サイモンゲーム",
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
        [Question.NotSimazeStart] = new()
        {
            QuestionText = "{0}の開始位置は？",
            ModuleName = "偽サイモンゲーム",
            Answers = new Dictionary<string, string>
            {
                ["(red, red)"] = "(赤, 赤)",
                ["(red, orange)"] = "(赤, 橙)",
                ["(red, yellow)"] = "(赤, 黄)",
                ["(red, green)"] = "(赤, 緑)",
                ["(red, blue)"] = "(赤, 青)",
                ["(red, purple)"] = "(赤, 紫)",
                ["(orange, red)"] = "(橙, 赤)",
                ["(orange, orange)"] = "(橙, 橙)",
                ["(orange, yellow)"] = "(橙, 黄)",
                ["(orange, green)"] = "(橙, 緑)",
                ["(orange, blue)"] = "(橙, 青)",
                ["(orange, purple)"] = "(橙, 紫)",
                ["(yellow, red)"] = "(黄, 赤)",
                ["(yellow, orange)"] = "(黄, 橙)",
                ["(yellow, yellow)"] = "(黄, 黄)",
                ["(yellow, green)"] = "(黄, 緑)",
                ["(yellow, blue)"] = "(黄, 青)",
                ["(yellow, purple)"] = "(黄, 紫)",
                ["(green, red)"] = "(緑, 赤)",
                ["(green, orange)"] = "(緑, 橙)",
                ["(green, yellow)"] = "(緑, 黄)",
                ["(green, green)"] = "(緑, 緑)",
                ["(green, blue)"] = "(緑, 青)",
                ["(green, purple)"] = "(緑, 紫)",
                ["(blue, red)"] = "(青, 赤)",
                ["(blue, orange)"] = "(青, 橙)",
                ["(blue, yellow)"] = "(青, 黄)",
                ["(blue, green)"] = "(青, 緑)",
                ["(blue, blue)"] = "(青, 青)",
                ["(blue, purple)"] = "(青, 紫)",
                ["(purple, red)"] = "(紫, 赤)",
                ["(purple, orange)"] = "(紫, 橙)",
                ["(purple, yellow)"] = "(紫, 黄)",
                ["(purple, green)"] = "(紫, 緑)",
                ["(purple, blue)"] = "(紫, 青)",
                ["(purple, purple)"] = "(紫, 紫)",
            },
        },
        // What was the goal position in {0}?
        // What was the goal position in Not Simaze?
        [Question.NotSimazeGoal] = new()
        {
            QuestionText = "{0}のゴールの位置は？",
            ModuleName = "偽サイモンゲーム",
            Answers = new Dictionary<string, string>
            {
                ["(red, red)"] = "(赤, 赤)",
                ["(red, orange)"] = "(赤, 橙)",
                ["(red, yellow)"] = "(赤, 黄)",
                ["(red, green)"] = "(赤, 緑)",
                ["(red, blue)"] = "(赤, 青)",
                ["(red, purple)"] = "(赤, 紫)",
                ["(orange, red)"] = "(橙, 赤)",
                ["(orange, orange)"] = "(橙, 橙)",
                ["(orange, yellow)"] = "(橙, 黄)",
                ["(orange, green)"] = "(橙, 緑)",
                ["(orange, blue)"] = "(橙, 青)",
                ["(orange, purple)"] = "(橙, 紫)",
                ["(yellow, red)"] = "(黄, 赤)",
                ["(yellow, orange)"] = "(黄, 橙)",
                ["(yellow, yellow)"] = "(黄, 黄)",
                ["(yellow, green)"] = "(黄, 緑)",
                ["(yellow, blue)"] = "(黄, 青)",
                ["(yellow, purple)"] = "(黄, 紫)",
                ["(green, red)"] = "(緑, 赤)",
                ["(green, orange)"] = "(緑, 橙)",
                ["(green, yellow)"] = "(緑, 黄)",
                ["(green, green)"] = "(緑, 緑)",
                ["(green, blue)"] = "(緑, 青)",
                ["(green, purple)"] = "(緑, 紫)",
                ["(blue, red)"] = "(青, 赤)",
                ["(blue, orange)"] = "(青, 橙)",
                ["(blue, yellow)"] = "(青, 黄)",
                ["(blue, green)"] = "(青, 緑)",
                ["(blue, blue)"] = "(青, 青)",
                ["(blue, purple)"] = "(青, 紫)",
                ["(purple, red)"] = "(紫, 赤)",
                ["(purple, orange)"] = "(紫, 橙)",
                ["(purple, yellow)"] = "(紫, 黄)",
                ["(purple, green)"] = "(紫, 緑)",
                ["(purple, blue)"] = "(紫, 青)",
                ["(purple, purple)"] = "(紫, 紫)",
            },
        },

        // Not Text Field
        // Which letter was pressed in the first stage of {0}?
        // Which letter was pressed in the first stage of Not Text Field?
        [Question.NotTextFieldInitialPresses] = new()
        {
            QuestionText = "{0}ステージ1で押したのはどの英字？",
            ModuleName = "偽テキストフィールド",
        },
        // Which letter appeared 9 times at the start of {0}?
        // Which letter appeared 9 times at the start of Not Text Field?
        [Question.NotTextFieldBackgroundLetter] = new()
        {
            QuestionText = "{0}の最初に9つ表示されていた英字は？",
            ModuleName = "偽テキストフィールド",
        },

        // Not The Bulb
        // What word flashed on {0}?
        // What word flashed on Not The Bulb?
        [Question.NotTheBulbWord] = new()
        {
            QuestionText = "{0}で点滅していた単語は？",
            ModuleName = "偽電球",
        },
        // What color was the bulb on {0}?
        // What color was the bulb on Not The Bulb?
        [Question.NotTheBulbColor] = new()
        {
            QuestionText = "{0}の電球の色は？",
            ModuleName = "偽電球",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Yellow"] = "黄",
                ["Purple"] = "紫",
                ["White"] = "白",
            },
        },
        // What was the material of the screw cap on {0}?
        // What was the material of the screw cap on Not The Bulb?
        [Question.NotTheBulbScrewCap] = new()
        {
            QuestionText = "{0}の口金の素材は？",
            ModuleName = "偽電球",
            Answers = new Dictionary<string, string>
            {
                ["Copper"] = "銅",
                ["Silver"] = "銀",
                ["Gold"] = "金",
                ["Plastic"] = "プラスチック",
                ["Carbon Fibre"] = "カーボンファイバー",
                ["Ceramic"] = "セラミック",
            },
        },

        // Not the Button
        // What colors did the light glow in {0}?
        // What colors did the light glow in Not the Button?
        [Question.NotTheButtonLightColor] = new()
        {
            QuestionText = "{0}の光の色は？",
            ModuleName = "偽ボタン",
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

        // Not The Plunger Button
        // What color did the background flash in {0}?
        // What color did the background flash in Not The Plunger Button?
        [Question.NotThePlungerButtonBackground] = new()
        {
            QuestionText = "{0}の背景の色は？",
            ModuleName = "偽プランジャーボタン",
            Answers = new Dictionary<string, string>
            {
                ["Black"] = "黒",
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
                ["White"] = "白",
            },
        },

        // Not the Screw
        // What was the initial position in {0}?
        // What was the initial position in Not the Screw?
        [Question.NotTheScrewInitialPosition] = new()
        {
            QuestionText = "{0}の初期位置は？",
            ModuleName = "偽ザ・ネジ",
        },

        // Not Who’s on First
        // In which position was the button you pressed in the {1} stage on {0}?
        // In which position was the button you pressed in the first stage on Not Who’s on First?
        [Question.NotWhosOnFirstPressedPosition] = new()
        {
            QuestionText = "{0}のステージ{1}で押したボタンの位置は？",
            ModuleName = "偽表比較",
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
        [Question.NotWhosOnFirstPressedLabel] = new()
        {
            QuestionText = "{0}のステージ{1}で押したボタンのラベルは？",
            ModuleName = "偽表比較",
        },
        // In which position was the reference button in the {1} stage on {0}?
        // In which position was the reference button in the first stage on Not Who’s on First?
        [Question.NotWhosOnFirstReferencePosition] = new()
        {
            QuestionText = "{0}のステージ{1}で参照したボタンの位置は？",
            ModuleName = "偽表比較",
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
        [Question.NotWhosOnFirstReferenceLabel] = new()
        {
            QuestionText = "{0}のステージ{1}で参照したボタンのラベルは？",
            ModuleName = "偽表比較",
        },
        // What was the calculated number in the second stage on {0}?
        // What was the calculated number in the second stage on Not Who’s on First?
        [Question.NotWhosOnFirstSum] = new()
        {
            QuestionText = "{0}のステージ2で計算した値は？",
            ModuleName = "偽表比較",
        },

        // Not Word Search
        // Which of these consonants was missing in {0}?
        // Which of these consonants was missing in Not Word Search?
        [Question.NotWordSearchMissing] = new()
        {
            QuestionText = "{0}で欠けていた子音は？",
            ModuleName = "偽ワードサーチ",
        },
        // What was the first correctly pressed letter in {0}?
        // What was the first correctly pressed letter in Not Word Search?
        [Question.NotWordSearchFirstPress] = new()
        {
            QuestionText = "{0}の最初に正しく押した英字は？",
            ModuleName = "偽ワードサーチ",
        },

        // Not X01
        // Which sector value {1} present on {0}?
        // Which sector value was present on Not X01?
        [Question.NotX01SectorValues] = new()
        {
            QuestionText = "{0}に存在{1}セクターは？",
            ModuleName = "偽ダーツ",
            FormatArgs = new Dictionary<string, string>
            {
                ["was"] = "した",
                ["was not"] = "しなかった",
            },
        },

        // Not X-Ray
        // What table were we in in {0} (numbered 1–8 in reading order in the manual)?
        // What table were we in in Not X-Ray (numbered 1–8 in reading order in the manual)?
        [Question.NotXRayTable] = new()
        {
            QuestionText = "{0}で使用した表は(マニュアルの読み順で)？",
            ModuleName = "偽レントゲン",
        },
        // What direction was button {1} in {0}?
        // What direction was button 1 in Not X-Ray?
        [Question.NotXRayDirections] = new()
        {
            QuestionText = "{0}のボタン{1}の方向は？",
            ModuleName = "偽レントゲン",
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
        [Question.NotXRayButtons] = new()
        {
            QuestionText = "{0}で{1}方向のボタンは？",
            ModuleName = "偽レントゲン",
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
        [Question.NotXRayScannerColor] = new()
        {
            QuestionText = "{0}のスキャナーの色は？",
            ModuleName = "偽レントゲン",
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
        [Question.NumberedButtonsButtons] = new()
        {
            QuestionText = "{0}で正しく押された番号は？",
            ModuleName = "番号ボタン",
        },

        // The Number Game
        // What was the maximum number in {0}?
        // What was the maximum number in The Number Game?
        [Question.NumberGameMaximum] = new()
        {
            QuestionText = "{0}の最大値は？",
        },

        // Numbers
        // What two-digit number was given in {0}?
        // What two-digit number was given in Numbers?
        [Question.NumbersTwoDigit] = new()
        {
            QuestionText = "{0}で与えられた二桁の数字は？",
            ModuleName = "ナンバー",
        },

        // Numpath
        // What was the color of the number on {0}?
        // What was the color of the number on Numpath?
        [Question.NumpathColor] = new()
        {
            QuestionText = "{0}の数字の色は？",
            ModuleName = "ナンパス",
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
        [Question.NumpathDigit] = new()
        {
            QuestionText = "{0}に表示された数字は？",
            ModuleName = "ナンパス",
        },

        // Object Shows
        // Which of these was a contestant on {0}?
        // Which of these was a contestant on Object Shows?
        [Question.ObjectShowsContestants] = new()
        {
            QuestionText = "{0}の参加者に含まれるのは？",
            ModuleName = "オブジェクトショー",
        },

        // The Octadecayotton
        // What was the starting sphere in {0}?
        // What was the starting sphere in The Octadecayotton?
        [Question.OctadecayottonSphere] = new()
        {
            QuestionText = "{0}のスタートボールは？",
            ModuleName = "9次元超立方体",
        },
        // What was one of the subrotations in the {1} rotation in {0}?
        // What was one of the subrotations in the first rotation in The Octadecayotton?
        [Question.OctadecayottonRotations] = new()
        {
            QuestionText = "{0}で{1}番目の回転の二次変形の一つであるのは？",
            ModuleName = "9次元超立方体",
        },

        // Odd One Out
        // What was the button you pressed in the {1} stage of {0}?
        // What was the button you pressed in the first stage of Odd One Out?
        [Question.OddOneOutButton] = new()
        {
            QuestionText = "{0}のステージ{1}で押されたボタンは？",
            ModuleName = "仲間外れ",
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
            QuestionText = "{0}で表示された数字の{1}は？",
            ModuleName = "オールドAI",
            FormatArgs = new Dictionary<string, string>
            {
                ["group"] = "グループ",
                ["sub-group"] = "サブグループ",
            },
        },

        // Old Fogey
        // What was the initial color of the status light in {0}?
        // What was the initial color of the status light in Old Fogey?
        [Question.OldFogeyStartingColor] = new()
        {
            QuestionText = "{0}の初期状態におけるステータスライトの色は？",
            ModuleName = "耄碌爺",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Yellow"] = "黄色",
                ["Blue"] = "青",
                ["Magenta"] = "マゼンタ",
                ["Cyan"] = "シアン",
                ["White"] = "白",
            },
        },

        // One Links To All
        // What was the starting article in {0}?
        // What was the starting article in One Links To All?
        [Question.OneLinksToAllStart] = new()
        {
            QuestionText = "{0}の初期記事は？",
        },
        // What was the ending article in {0}?
        // What was the ending article in One Links To All?
        [Question.OneLinksToAllEnd] = new()
        {
            QuestionText = "{0}の最終記事は？",
        },

        // Only Connect
        // Which Egyptian hieroglyph was in the {1} in {0}?
        // Which Egyptian hieroglyph was in the top left in Only Connect?
        [Question.OnlyConnectHieroglyphs] = new()
        {
            QuestionText = "{0}の{1}のヒエログリフは？",
            ModuleName = "オンリーコネクト",
            FormatArgs = new Dictionary<string, string>
            {
                ["top left"] = "左上",
                ["top middle"] = "上",
                ["top right"] = "右上",
                ["bottom left"] = "左下",
                ["bottom middle"] = "下",
                ["bottom right"] = "右下",
            },
            Answers = new Dictionary<string, string>
            {
                ["Two Reeds"] = "二本の葦",
                ["Lion"] = "ライオン",
                ["Twisted Flax"] = "よりあわせた亜麻糸",
                ["Horned Viper"] = "ヘビ",
                ["Water"] = "水",
                ["Eye of Horus"] = "ホルスの目",
            },
        },

        // Orange Arrows
        // What was the {1} arrow on the display of the {2} stage of {0}?
        // What was the first arrow on the display of the first stage of Orange Arrows?
        [Question.OrangeArrowsSequences] = new()
        {
            QuestionText = "{0}のステージ{2}における{1}番目の矢印は？",
            ModuleName = "橙色矢印",
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
        [Question.OrangeCipherScreen] = new()
        {
            QuestionText = "{0}の答えは？",
            ModuleName = "橙色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Ordered Keys
        // What color was this key in the {1} stage of {0}?
        // What color was this key in the first stage of Ordered Keys?
        [Question.OrderedKeysColors] = new()
        {
            QuestionText = "{0}のステージ{1}におけるこの音板の色は？",
            ModuleName = "順番音板",
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
        // What was the label of this key in the {1} stage of {0}?
        // What was the label of this key in the first stage of Ordered Keys?
        [Question.OrderedKeysLabels] = new()
        {
            QuestionText = "{0}のステージ{1}におけるこの音板のラベルは？",
            ModuleName = "順番音板",
        },
        // What color was the label of this key in the {1} stage of {0}?
        // What color was the label of this key in the first stage of Ordered Keys?
        [Question.OrderedKeysLabelColors] = new()
        {
            QuestionText = "{0}のステージ{1}におけるこの音板のラベルの色は？",
            ModuleName = "順番音板",
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
        [Question.OrderPickingOrder] = new()
        {
            QuestionText = "{0}の{1}番目の注文IDは？",
            ModuleName = "注文ピッキング",
        },
        // What was the product ID in the {1} order of {0}?
        // What was the product ID in the first order of Order Picking?
        [Question.OrderPickingProduct] = new()
        {
            QuestionText = "{0}の{1}番目の製品IDは？",
            ModuleName = "注文ピッキング",
        },
        // What was the pallet in the {1} order of {0}?
        // What was the pallet in the first order of Order Picking?
        [Question.OrderPickingPallet] = new()
        {
            QuestionText = "{0}の{1}番目の注文パレットは？",
            ModuleName = "注文ピッキング",
        },

        // Orientation Cube
        // What was the observer’s initial position in {0}?
        // What was the observer’s initial position in Orientation Cube?
        [Question.OrientationCubeInitialObserverPosition] = new()
        {
            QuestionText = "{0}の最初の観測者の位置は？",
            ModuleName = "方向キューブ",
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
        [Question.OrientationHypercubeInitialObserverPosition] = new()
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
        // What was the initial colour of the {1} face in {0}?
        // What was the initial colour of the right face in Orientation Hypercube?
        [Question.OrientationHypercubeInitialFaceColour] = new()
        {
            QuestionText = "{0}の初期状態における{1}面の色は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["right"] = "右",
                ["left"] = "左",
                ["top"] = "上",
                ["bottom"] = "下",
                ["back"] = "後",
                ["front"] = "前",
                ["zag"] = "甲",
                ["zig"] = "乙",
            },
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

        // Palindromes
        // What was {1}’s {2} digit from the right in {0}?
        // What was X’s first digit from the right in Palindromes?
        [Question.PalindromesNumbers] = new()
        {
            QuestionText = "{0}で{1}の右から{2}桁目は？",
            ModuleName = "回文",
            FormatArgs = new Dictionary<string, string>
            {
                ["X"] = "X",
                ["Y"] = "Y",
                ["Z"] = "Z",
                ["the screen"] = "ディスプレー",
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
            QuestionText = "{0}に表示されたのは？",
            ModuleName = "偶奇性",
        },

        // Partial Derivatives
        // What was the LED color in the {1} stage of {0}?
        // What was the LED color in the first stage of Partial Derivatives?
        [Question.PartialDerivativesLedColors] = new()
        {
            QuestionText = "{0}のステージ{1}におけるLEDの色は？",
            ModuleName = "偏微分",
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
        [Question.PartialDerivativesTerms] = new()
        {
            QuestionText = "{0}の{1}番目の項は？",
            ModuleName = "偏微分",
        },

        // Passport Control
        // What was the passport expiration year of the {1} inspected passenger in {0}?
        // What was the passport expiration year of the first inspected passenger in Passport Control?
        [Question.PassportControlPassenger] = new()
        {
            QuestionText = "{0}で{1}番目に搭乗した乗客のパスポート有効期限は？",
            ModuleName = "パスポートコントロール",
        },

        // Password Destroyer
        // What was the 2FAST™ value when you solved {0}?
        // What was the 2FAST™ value when you solved Password Destroyer?
        [Question.PasswordDestroyerTwoFactorV2] = new()
        {
            QuestionText = "{0}を解除した時点の2FAST™の値は？",
            ModuleName = "パスワード破壊",
        },

        // Pattern Cube
        // Which symbol was highlighted in {0}?
        // Which symbol was highlighted in Pattern Cube?
        [Question.PatternCubeHighlightedSymbol] = new()
        {
            QuestionText = "{0}でハイライトされた記号は？",
            ModuleName = "パターンキューブ",
        },

        // The Pentabutton
        // What was the base colour in {0}?
        // What was the base colour in The Pentabutton?
        [Question.PentabuttonBaseColor] = new()
        {
            NeedsTranslation = true,
            QuestionText = "{0}のベースの色は？",
            ModuleName = "五角形ボタン",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Orange"] = "オレンジ",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Purple"] = "紫",
                ["White"] = "白",
            },
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Pentabutton labelled “{0}”"] = "the Pentabutton labelled “{0}”",
            },
        },

        // Periodic Words
        // What word was on the display in the {1} stage of {0}?
        // What word was on the display in the first stage of Periodic Words?
        [Question.PeriodicWordsDisplayedWords] = new()
        {
            QuestionText = "{0}のステージ{1}で表示された単語は？",
            ModuleName = "周期単語",
        },

        // Perspective Pegs
        // What was the {1} color in the initial sequence in {0}?
        // What was the first color in the initial sequence in Perspective Pegs?
        [Question.PerspectivePegsColorSequence] = new()
        {
            QuestionText = "{0}の初期色シーケンスで{1}番目の色は？",
            ModuleName = "奥行きペグ",
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
        [Question.PhosphorescenceOffset] = new()
        {
            QuestionText = "{0}のオフセットは？",
            ModuleName = "燐光",
        },
        // What was the {1} button press in {0}?
        // What was the first button press in Phosphorescence?
        [Question.PhosphorescenceButtonPresses] = new()
        {
            NeedsTranslation = true,
            QuestionText = "{0}の{1}番目に押したボタンは？",
            ModuleName = "燐光",
            Answers = new Dictionary<string, string>
            {
                ["Azure"] = "空",
                ["Blue"] = "青",
                ["Crimson"] = "紅",
                ["Diamond"] = "水色",
                ["Emerald"] = "エメラルド",
                ["Fuchsia"] = "躑躅",
                ["Green"] = "緑",
                ["Hazel"] = "榛",
                ["Ice"] = "氷",
                ["Jade"] = "翡翠",
                ["Kiwi"] = "キウイ",
                ["Lime"] = "黄緑",
                ["Magenta"] = "マゼンタ",
                ["Navy"] = "藍",
                ["Orange"] = "オレンジ",
                ["Purple"] = "紫",
                ["Quartz"] = "鉛",
                ["Red"] = "赤",
                ["Salmon"] = "鮭",
                ["Tan"] = "革",
                ["Ube"] = "芋",
                ["Vibe"] = "熱",
                ["White"] = "白",
                ["Xotic"] = "茶",
                ["Yellow"] = "黄",
                ["Zen"] = "女郎花",
            },
        },

        // Pickup Identification
        // What pickup was shown in the {1} stage of {0}?
        // What pickup was shown in the first stage of Pickup Identification?
        [Question.PickupIdentificationItem] = new()
        {
            QuestionText = "{0}のステージ{1}で表示されたアイテムは？",
            ModuleName = "アイテム識別",
        },

        // Pictionary
        // What was the code in {0}?
        // What was the code in Pictionary?
        [Question.PictionaryCode] = new()
        {
            QuestionText = "{0}のコードは？",
            ModuleName = "画像ロジック",
        },

        // Pie
        // What was the {1} digit of the displayed number in {0}?
        // What was the first digit of the displayed number in Pie?
        [Question.PieDigits] = new()
        {
            QuestionText = "{0}で{1}番目に表示された数字は？",
            ModuleName = "パイ",
        },

        // Pie Flash
        // What number was not displayed in {0}?
        // What number was not displayed in Pie Flash?
        [Question.PieFlashDigits] = new()
        {
            QuestionText = "{0}で表示されていない番号は？",
            ModuleName = "点滅パイ",
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

        // Pinpoint
        // Which distance occurred in {0}?
        // Which distance occurred in Pinpoint?
        [Question.PinpointDistances] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which distance occurred in {0}?",
        },
        // Which point occurred in {0}?
        // Which point occurred in Pinpoint?
        [Question.PinpointPoints] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which point occurred in {0}?",
        },

        // The Pink Button
        // What was the {1} word in {0}?
        // What was the first word in The Pink Button?
        [Question.PinkButtonWords] = new()
        {
            QuestionText = "{0}の{1}番目の単語は？",
            ModuleName = "桃色ボタン",
        },
        // What was the {1} color in {0}?
        // What was the first color in The Pink Button?
        [Question.PinkButtonColors] = new()
        {
            QuestionText = "{0}の{1}番目の色は？",
            ModuleName = "桃色ボタン",
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
        [Question.PixelCipherKeyword] = new()
        {
            QuestionText = "{0}のキーワードは？",
            ModuleName = "ピクセル暗号",
        },

        // Placeholder Talk
        // What was the first half of the first phrase in {0}?
        // What was the first half of the first phrase in Placeholder Talk?
        [Question.PlaceholderTalkFirstPhrase] = new()
        {
            QuestionText = "{0}の一つ目のフレーズの前半は？",
            ModuleName = "プレースホルダートーク",
        },
        // What was the last half of the first phrase in {0}?
        // What was the last half of the first phrase in Placeholder Talk?
        [Question.PlaceholderTalkOrdinal] = new()
        {
            QuestionText = "{0}の一つ目のフレーズの後半は？",
            ModuleName = "プレースホルダートーク",
        },

        // Placement Roulette
        // What was the character listed on the information display in {0}?
        // What was the character listed on the information display in Placement Roulette?
        [Question.PlacementRouletteChar] = new()
        {
            QuestionText = "{0}の情報一覧に掲載されていたキャラクターは？",
            ModuleName = "マリオカート",
        },
        // What was the track listed on the information display in {0}?
        // What was the track listed on the information display in Placement Roulette?
        [Question.PlacementRouletteTrack] = new()
        {
            QuestionText = "{0}の情報一覧に掲載されていたトラックは？",
            ModuleName = "マリオカート",
        },
        // What was the vehicle listed on the information display in {0}?
        // What was the vehicle listed on the information display in Placement Roulette?
        [Question.PlacementRouletteVehicle] = new()
        {
            QuestionText = "{0}の情報一覧に掲載されていた車両は？",
            ModuleName = "マリオカート",
        },

        // Planets
        // What was the planet shown in {0}?
        // What was the planet shown in Planets?
        [Question.PlanetsPlanet] = new()
        {
            QuestionText = "{0}には何の惑星が表示されていた？",
            ModuleName = "惑星",
        },
        // What was the color of the {1} strip (from the top) in {0}?
        // What was the color of the first strip (from the top) in Planets?
        [Question.PlanetsStrips] = new()
        {
            QuestionText = "{0}の上から{1}番目のストリップの色は？",
            ModuleName = "惑星",
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
            QuestionText = "{0}において、{1}番目に押して正解だったフレーズは？",
            ModuleName = "詩",
        },

        // Pointless Machines
        // What color flashed {1} in {0}?
        // What color flashed first in Pointless Machines?
        [Question.PointlessMachinesFlashes] = new()
        {
            QuestionText = "{0}で{1}番目に点滅した色は？",
            ModuleName = "無意味なマシーン",
            Answers = new Dictionary<string, string>
            {
                ["White"] = "白",
                ["Purple"] = "紫",
                ["Red"] = "赤",
                ["Blue"] = "青",
                ["Yellow"] = "黄",
            },
        },

        // Polygons
        // Which polygon was present on {0}?
        // Which polygon was present on Polygons?
        [Question.PolygonsPolygon] = new()
        {
            QuestionText = "{0}で表示された図形は？",
            ModuleName = "多角形",
        },

        // Polyhedral Maze
        // What was the starting position in {0}?
        // What was the starting position in Polyhedral Maze?
        [Question.PolyhedralMazeStartPosition] = new()
        {
            NeedsTranslation = true,
            QuestionText = "{0}の開始番号は？",
            ModuleName = "多面体迷路",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
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

        // Prime Encryption
        // What was the number shown in {0}?
        // What was the number shown in Prime Encryption?
        [Question.PrimeEncryptionDisplayedValue] = new()
        {
            QuestionText = "{0}に表示されていた数字は？",
            ModuleName = "素数暗号",
        },

        // Prison Break
        // Which cell did the prisoner start in in {0}?
        // Which cell did the prisoner start in in Prison Break?
        [Question.PrisonBreakPrisoner] = new()
        {
            QuestionText = "{0}で囚人がスタートした部屋は？",
        },
        // Where did you start in {0}?
        // Where did you start in Prison Break?
        [Question.PrisonBreakDefuser] = new()
        {
            QuestionText = "{0}であなたがスタートした部屋は？",
        },

        // Probing
        // What was the missing frequency in the {1} wire in {0}?
        // What was the missing frequency in the red-white wire in Probing?
        [Question.ProbingFrequencies] = new()
        {
            QuestionText = "{0}において、{1}のワイヤに含まれていなかった周波数は？",
            ModuleName = "回路接続",
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

        // Procedural Maze
        // What was the initial seed in {0}?
        // What was the initial seed in Procedural Maze?
        [Question.ProceduralMazeInitialSeed] = new()
        {
            QuestionText = "{0}の初期シードは？",
        },

        // ...?
        // What was the displayed number in {0}?
        // What was the displayed number in ...??
        [Question.PunctuationMarksDisplayedNumber] = new()
        {
            QuestionText = "{0}で最初に表示された数字は？",
        },

        // Purple Arrows
        // What was the target word on {0}?
        // What was the target word on Purple Arrows?
        [Question.PurpleArrowsFinish] = new()
        {
            QuestionText = "{0}のターゲット単語は？",
            ModuleName = "紫色矢印",
        },

        // The Purple Button
        // What was the {1} number in the cyclic sequence on {0}?
        // What was the first number in the cyclic sequence on The Purple Button?
        [Question.PurpleButtonNumbers] = new()
        {
            QuestionText = "{0}におけるシーケンスの{1}番目の数字は？",
            ModuleName = "紫色ボタン",
        },

        // Puzzle Identification
        // What was the {1} puzzle number in {0}?
        // What was the first puzzle number in Puzzle Identification?
        [Question.PuzzleIdentificationNum] = new()
        {
            QuestionText = "{0}の{1}回目の数字は？",
            ModuleName = "パズル識別",
        },
        // What game was the {1} puzzle in {0} from?
        // What game was the first puzzle in Puzzle Identification from?
        [Question.PuzzleIdentificationGame] = new()
        {
            QuestionText = "{0}の{1}回目に使用されたゲームの種類は？",
            ModuleName = "パズル識別",
            Answers = new Dictionary<string, string>
            {
                ["Professor Layton and the Curious Village"] = "レイトン教授と不思議な町",
                ["Professor Layton and Pandora's Box"] = "レイトン教授と悪魔の箱",
                ["Professor Layton and the Lost Future"] = "レイトン教授と最後の時間旅行",
                ["Professor Layton and the Spectre's Call"] = "レイトン教授と魔神の笛",
                ["Professor Layton and the Miracle Mask"] = "レイトン教授と奇跡の仮面",
                ["Professor Layton and the Azran Legacy"] = "レイトン教授と超文明Aの遺産",
                ["Layton's Mystery Journey: Katrielle and the Millionaire's Conspiracy"] = "レイトン ミステリージャーニー カトリーエイルと大富豪の陰謀",
                ["Professor Layton vs. Phoenix Wright: Ace Attorney"] = "レイトン教授VS逆転裁判",
            },
        },
        // What was the {1} puzzle in {0}?
        // What was the first puzzle in Puzzle Identification?
        [Question.PuzzleIdentificationName] = new()
        {
            QuestionText = "{0}の{1}回目のパズル名は？",
            ModuleName = "パズル識別",
        },

        // Puzzling Hexabuttons
        // What letter was displayed on the {1} hexabutton when submitting in {0}?
        // What letter was displayed on the top-left hexabutton when submitting in Puzzling Hexabuttons?
        [Question.PuzzlingHexabuttonsLetter] = new()
        {
            NeedsTranslation = true,
            QuestionText = "{0}の{1}の六角形ボタンに表示されていた英字は？",
            ModuleName = "困惑六角形ボタン",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-right"] = "右上",
                ["middle-left"] = "左",
                ["center"] = "中央",
                ["middle-right"] = "右",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
        },

        // Q & A
        // What was the {1} question asked in {0}?
        // What was the first question asked in Q & A?
        [Question.QnAQuestions] = new()
        {
            QuestionText = "{0}の{1}番目の質問は？",
            ModuleName = "Q & A",
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
            QuestionText = "{0}で使用された単語は？",
            ModuleName = "量子パスワード",
        },

        // Quantum Ternary Converter
        // Which number was shown in {0}?
        // Which number was shown in Quantum Ternary Converter?
        [Question.QuantumTernaryConverterNumber] = new()
        {
            QuestionText = "{0}に表示された数字は？",
        },

        // Quaver
        // What was the {1} sequence’s answer in {0}?
        // What was the first sequence’s answer in Quaver?
        [Question.QuaverArrows] = new()
        {
            QuestionText = "{0}の{1}番目のシークエンスの回答は？",
        },

        // Question Mark
        // Which of these symbols was part of the flashing sequence in {0}?
        // Which of these symbols was part of the flashing sequence in Question Mark?
        [Question.QuestionMarkFlashedSymbols] = new()
        {
            QuestionText = "{0}の点滅したシークエンスの一部に含まれるのは？",
            ModuleName = "疑問符",
        },

        // Quick Arithmetic
        // What was the {1} color in the primary sequence in {0}?
        // What was the first color in the primary sequence in Quick Arithmetic?
        [Question.QuickArithmeticColors] = new()
        {
            QuestionText = "{0}の初期シーケンスにおける{1}番目の色は？",
            ModuleName = "瞬速計算",
            Answers = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["blue"] = "青",
                ["green"] = "緑",
                ["yellow"] = "黄",
                ["white"] = "白",
                ["black"] = "黒",
                ["orange"] = "オレンジ",
                ["pink"] = "ピンク",
                ["purple"] = "紫",
                ["cyan"] = "シアン",
                ["brown"] = "茶",
            },
        },
        // What was the {1} digit in the {2} sequence in {0}?
        // What was the first digit in the primary sequence in Quick Arithmetic?
        [Question.QuickArithmeticPrimSecDigits] = new()
        {
            QuestionText = "{0}の{2}シーケンスにおける{1}番目の数字は？",
            ModuleName = "瞬速計算",
            FormatArgs = new Dictionary<string, string>
            {
                ["primary"] = "一次",
                ["secondary"] = "二次",
            },
        },

        // Quintuples
        // What was the {1} digit in the {2} slot in {0}?
        // What was the first digit in the first slot in Quintuples?
        [Question.QuintuplesNumbers] = new()
        {
            QuestionText = "{0}の{2}番目のスロットの{1}番目の数字は？",
            ModuleName = "五重",
        },
        // What color was the {1} digit in the {2} slot in {0}?
        // What color was the first digit in the first slot in Quintuples?
        [Question.QuintuplesColors] = new()
        {
            QuestionText = "{0}の{2}番目のスロットの{1}番目の数字の色は？",
            ModuleName = "五重",
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
        [Question.QuintuplesColorCounts] = new()
        {
            QuestionText = "{0}で{1}は何回出現した？",
            ModuleName = "五重",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["blue"] = "青",
                ["orange"] = "オレンジ",
                ["green"] = "緑",
                ["pink"] = "ピンク",
            },
        },

        // Quiplash
        // What number was shown on {0}?
        // What number was shown on Quiplash?
        [Question.QuiplashNumber] = new()
        {
            QuestionText = "{0}に表示された数字は？",
            ModuleName = "クイプラッシュ",
        },

        // Quiz Buzz
        // What was the number initially on the display in {0}?
        // What was the number initially on the display in Quiz Buzz?
        [Question.QuizBuzzStartingNumber] = new()
        {
            QuestionText = "{0}のディスプレーに最初に表示された数字は？",
            ModuleName = "クィズバズ",
        },

        // Qwirkle
        // What tile did you place {1} in {0}?
        // What tile did you place first in Qwirkle?
        [Question.QwirkleTilesPlaced] = new()
        {
            QuestionText = "{0}で{1}番目に置いたタイルは？",
            ModuleName = "クワークル",
        },

        // Raiding Temples
        // How many jewels were in the starting common pool in {0}?
        // How many jewels were in the starting common pool in Raiding Temples?
        [Question.RaidingTemplesStartingCommonPool] = new()
        {
            QuestionText = "{0}で初期の共有財産にあった宝石の数は？",
            ModuleName = "神殿探検",
        },

        // Railway Cargo Loading
        // What was the {1} car in {0}?
        // What was the first car in Railway Cargo Loading?
        [Question.RailwayCargoLoadingCars] = new()
        {
            QuestionText = "{0}の{1}両目は？",
            ModuleName = "鉄道貨物積載センター",
        },
        // Which freight table rule {1} in {0}?
        // Which freight table rule was met in Railway Cargo Loading?
        [Question.RailwayCargoLoadingFreightTableRules] = new()
        {
            QuestionText = "{0}の貨車検索表で{1}ルールは？",
            ModuleName = "鉄道貨物積載センター",
            FormatArgs = new Dictionary<string, string>
            {
                ["was met"] = "合致した",
                ["wasn’t met"] = "合致しなかった",
            },
        },

        // Rainbow Arrows
        // What was the displayed number in {0}?
        // What was the displayed number in Rainbow Arrows?
        [Question.RainbowArrowsNumber] = new()
        {
            QuestionText = "{0}のディスプレーの数字は？",
            ModuleName = "虹色矢印",
        },

        // Recolored Switches
        // What was the color of the {1} LED in {0}?
        // What was the color of the first LED in Recolored Switches?
        [Question.RecoloredSwitchesLedColors] = new()
        {
            QuestionText = "{0}の{1}番目の位置にあるLEDの色は？",
            ModuleName = "色変えスイッチ",
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

        // Recursive Password
        // Which of these words appeared, but was not the password, in {0}?
        // Which of these words appeared, but was not the password, in Recursive Password?
        [Question.RecursivePasswordNonPasswordWords] = new()
        {
            QuestionText = "{0}で出現したがパスワードではなかった単語は？",
            ModuleName = "桃色ボタン",
        },
        // What was the password in {0}?
        // What was the password in Recursive Password?
        [Question.RecursivePasswordPassword] = new()
        {
            QuestionText = "{0}のパスワードは？",
            ModuleName = "桃色ボタン",
        },

        // Red Arrows
        // What was the starting number in {0}?
        // What was the starting number in Red Arrows?
        [Question.RedArrowsStartNumber] = new()
        {
            QuestionText = "{0}の開始地点の数字は？",
            ModuleName = "赤色矢印",
        },

        // Red Button’t
        // What was the word before “SUBMIT” in {0}?
        // What was the word before “SUBMIT” in Red Button’t?
        [Question.RedButtontWord] = new()
        {
            QuestionText = "{0}で「SUBMIT」の直前に表示された単語は？",
            ModuleName = "偽赤色ボタン",
        },

        // Red Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Red Cipher?
        [Question.RedCipherScreen] = new()
        {
            QuestionText = "{0}の回答は？",
            ModuleName = "赤色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Red Herring
        // What was the first color flashed by {0}?
        // What was the first color flashed by Red Herring?
        [Question.RedHerringFirstFlash] = new()
        {
            QuestionText = "{0}において、最初に点滅した色は？",
            ModuleName = "レッドヘリング",
        },

        // Reformed Role Reversal
        // Which condition was the solving condition in {0}?
        // Which condition was the solving condition in Reformed Role Reversal?
        [Question.ReformedRoleReversalCondition] = new()
        {
            NeedsTranslation = true,
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
        [Question.ReformedRoleReversalWire] = new()
        {
            QuestionText = "{0}の{1}番目のワイヤの色は？",
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
            QuestionText = "{0}のステージ{1}で使用された計算は？",
        },

        // Regular Crazy Talk
        // What was the displayed digit that corresponded to the solution phrase in {0}?
        // What was the displayed digit that corresponded to the solution phrase in Regular Crazy Talk?
        [Question.RegularCrazyTalkDigit] = new()
        {
            QuestionText = "{0}において、回答のフレーズに表示されていた数字は？",
            ModuleName = "レギュラークレイジートーク",
        },
        // What was the embellishment of the solution phrase in {0}?
        // What was the embellishment of the solution phrase in Regular Crazy Talk?
        [Question.RegularCrazyTalkModifier] = new()
        {
            QuestionText = "{0}の回答のフレーズの装飾は？",
            ModuleName = "レギュラークレイジートーク",
            Answers = new Dictionary<string, string>
            {
                ["[PHRASE]"] = "[フレーズ]",
                ["It says: [PHRASE]"] = "It says: [フレーズ]",
                ["Quote: [PHRASE] End quote"] = "Quote: [フレーズ] End quote",
                ["“[PHRASE]”"] = "“[フレーズ]”",
                ["It says: “[PHRASE]”"] = "It says: “[フレーズ]”",
                ["“It says: [PHRASE]”"] = "“It says: [フレーズ]”",
            },
        },

        // Reordered Keys
        // Which key was the pivot in the {1} stage of {0}?
        [Question.ReorderedKeysPivot] = new()
        {
            QuestionText = "{0}のステージ{1}の軸は？",
            ModuleName = "順番替え音板",
        },
        // What color was this key in the {1} stage of {0}?
        // What color was this key in the first stage of Reordered Keys?
        [Question.ReorderedKeysKeyColor] = new()
        {
            QuestionText = "{0}のステージ{1}におけるこの音板の色は？",
            ModuleName = "順番替え音板",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
            },
        },
        // What color was the label of this key in the {1} stage of {0}?
        // What color was the label of this key in the first stage of Reordered Keys?
        [Question.ReorderedKeysLabelColor] = new()
        {
            QuestionText = "{0}のステージ{1}におけるこの音板のラベルの色は？",
            ModuleName = "順番替え音板",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
            },
        },
        // What was the label of this key in the {1} stage of {0}?
        // What was the label of this key in the first stage of Reordered Keys?
        [Question.ReorderedKeysLabel] = new()
        {
            QuestionText = "{0}のステージ{1}におけるこの音板のラベルは？",
            ModuleName = "順番替え音板",
        },

        // Retirement
        // Which one of these houses was on offer, but not chosen by Bob in {0}?
        // Which one of these houses was on offer, but not chosen by Bob in Retirement?
        [Question.RetirementHouses] = new()
        {
            QuestionText = "{0}において、これらのうちBOBが定年後に選択しなかった家は？",
            ModuleName = "退職",
        },

        // Reverse Morse
        // What was the {1} character in the {2} message of {0}?
        // What was the first character in the first message of Reverse Morse?
        [Question.ReverseMorseCharacters] = new()
        {
            QuestionText = "{0}の{2}つ目のメッセージの{1}文字目は？",
            ModuleName = "逆モールス信号",
        },

        // Reverse Polish Notation
        // What character was used in the {1} round of {0}?
        // What character was used in the first round of Reverse Polish Notation?
        [Question.ReversePolishNotationCharacter] = new()
        {
            QuestionText = "{0}のラウンド{1}で使用された文字は？",
            ModuleName = "逆ポーランド記法",
        },

        // RGB Maze
        // What was the exit coordinate in {0}?
        // What was the exit coordinate in RGB Maze?
        [Question.RGBMazeExit] = new()
        {
            QuestionText = "{0}の出口の座標は？",
            ModuleName = "RGB迷路",
        },
        // Where was the {1} key in {0}?
        // Where was the red key in RGB Maze?
        [Question.RGBMazeKeys] = new()
        {
            QuestionText = "{0}における{1}色の鍵はどこ？",
            ModuleName = "RGB迷路",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
            },
        },
        // Which maze number was the {1} maze in {0}?
        // Which maze number was the red maze in RGB Maze?
        [Question.RGBMazeNumber] = new()
        {
            QuestionText = "{0}の{1}色迷路の番号は？",
            ModuleName = "RGB迷路",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
            },
        },

        // RGB Sequences
        // What was the color of the {1} LED in {0}?
        // What was the color of the first LED in RGB Sequences?
        [Question.RGBSequencesDisplay] = new()
        {
            QuestionText = "{0}の{1}番目のLEDの色は？",
            ModuleName = "RGBシークエンス",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Magenta"] = "マゼンタ",
                ["Cyan"] = "シアン",
                ["Yellow"] = "黄",
                ["White"] = "白",
            },
        },

        // Rhythms
        // What was the color in {0}?
        // What was the color in Rhythms?
        [Question.RhythmsColor] = new()
        {
            QuestionText = "{0}のLEDの色は？",
            ModuleName = "リズム",
            Answers = new Dictionary<string, string>
            {
                ["Blue"] = "青",
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Yellow"] = "黄",
            },
        },

        // RNG Crystal
        // Which bit had a tap in {0} (the output after shifting is at bit 0)?
        // Which bit had a tap in RNG Crystal (the output after shifting is at bit 0)?
        [Question.RNGCrystalTaps] = new()
        {
            QuestionText = "{0}でタップがあったビットは(点線の位置はビット0)？",
            ModuleName = "乱数クリスタル",
        },

        // Robo-Scanner
        // Where was the empty cell in {0}?
        // Where was the empty cell in Robo-Scanner?
        [Question.RoboScannerEmptyCell] = new()
        {
            QuestionText = "{0}の空のセルはどこ？",
        },

        // Robot Programming
        // What was the color of the {1} robot in {0}?
        // What was the color of the first robot in Robot Programming?
        [Question.RobotProgrammingColor] = new()
        {
            QuestionText = "{0}の{1}番目のロボットの色は？",
            ModuleName = "ロボットプログラミング",
        },
        // What was the shape of the {1} robot in {0}?
        // What was the shape of the first robot in Robot Programming?
        [Question.RobotProgrammingShape] = new()
        {
            QuestionText = "{0}の{1}番目のロボットの形は？",
            ModuleName = "ロボットプログラミング",
        },

        // Roger
        // What four-digit number was given in {0}?
        // What four-digit number was given in Roger?
        [Question.RogerSeed] = new()
        {
            QuestionText = "{0}で得られた4桁の数字は？",
            ModuleName = "ロジャー",
        },

        // Role Reversal
        // What was the number corresponding to the correct condition in {0}?
        // What was the number corresponding to the correct condition in Role Reversal?
        [Question.RoleReversalNumber] = new()
        {
            QuestionText = "{0}の正しい状態の数字は？",
        },
        // How many {1} wires were there in {0}?
        // How many warm-colored wires were there in Role Reversal?
        [Question.RoleReversalWires] = new()
        {
            QuestionText = "{0}における{1}系のワイヤの総数は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["warm-colored"] = "暖色",
                ["cold-colored"] = "寒色",
                ["primary-colored"] = "原色",
                ["secondary-colored"] = "二次色",
            },
        },

        // RPS Judging
        // Which round did the {1} team {2} in {0}?
        // Which round did the red team win in RPS Judging?
        [Question.RPSJudgingWinner] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which round did the {1} team {2} in {0}?",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "red",
                ["win"] = "win",
                ["blue"] = "blue",
                ["lose"] = "lose",
            },
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the RPS Judging where the {0} team {1} the {2} round"] = "the RPS Judging where the {0} team {1} the {2} round",
                ["won"] = "won",
                ["lost"] = "lost",
                ["the RPS Judging with a draw in the {0} round"] = "the RPS Judging with a draw in the {0} round",
            },
        },
        // Which round was a draw in {0}?
        // Which round was a draw in RPS Judging?
        [Question.RPSJudgingDraw] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which round was a draw in {0}?",
        },

        // The Rule
        // What was the rule number in {0}?
        // What was the rule number in The Rule?
        [Question.RuleNumber] = new()
        {
            QuestionText = "{0}のルール番号は？",
            ModuleName = "ザ・ルール",
        },

        // Rule of Three
        // What was the {1} coordinate of the {2} vertex in {0}?
        // What was the X coordinate of the red vertex in Rule of Three?
        [Question.RuleOfThreeCoordinates] = new()
        {
            QuestionText = "{0}の{2}色の頂点の{1}座標は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["yellow"] = "黄",
                ["blue"] = "青",
            },
        },
        // What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?
        // What was the position of the red sphere on the X axis in the first cycle in Rule of Three?
        [Question.RuleOfThreeCycles] = new()
        {
            QuestionText = "{0}の{3}回目のサイクルにおける{1}色の球の{2}軸上の位置は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["yellow"] = "黄",
                ["blue"] = "青",
            },
        },

        // Safety Square
        // What was the digit displayed on the {1} diamond in {0}?
        // What was the digit displayed on the red diamond in Safety Square?
        [Question.SafetySquareDigits] = new()
        {
            QuestionText = "{0}の{1}色のダイヤに表示された数字は？",
            ModuleName = "セイフティスクエア",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["yellow"] = "黄",
                ["blue"] = "青",
            },
        },
        // What was the special rule displayed on the white diamond in {0}?
        // What was the special rule displayed on the white diamond in Safety Square?
        [Question.SafetySquareSpecialRule] = new()
        {
            QuestionText = "{0}の白に表示された特殊ルールは？",
            ModuleName = "セイフティスクエア",
            Answers = new Dictionary<string, string>
            {
                ["No special rule"] = "特殊ルールなし",
                ["Reacts with water"] = "水と過剰に反応",
                ["Simple asphyxiant"] = "単純窒息性ガス",
                ["Oxidizer"] = "空気供給なしで燃焼",
            },
        },

        // The Samsung
        // Where was {1} in {0}?
        // Where was Duolingo in The Samsung?
        [Question.SamsungAppPositions] = new()
        {
            QuestionText = "{0}の{1}はどこ？",
            Answers = new Dictionary<string, string>
            {
                ["TL"] = "左上",
                ["TM"] = "上",
                ["TR"] = "右上",
                ["ML"] = "左",
                ["MM"] = "中央",
                ["MR"] = "右",
                ["BL"] = "左下",
                ["BM"] = "下",
                ["BR"] = "右下",
            },
        },

        // Saturn
        // Where was the goal in {0}?
        // Where was the goal in Saturn?
        [Question.SaturnGoal] = new()
        {
            QuestionText = "{0}のゴールはどこ？",
            ModuleName = "土星",
        },

        // Sbemail Songs
        // What was the displayed song for stage {1} (hexadecimal) of {0}?
        // What was the displayed song for stage 01 (hexadecimal) of Sbemail Songs?
        [Question.SbemailSongsSongs] = new()
        {
            QuestionText = "{0}のステージ{1}(十六進数)で再生された音は？",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)"] = "ステージ{1}(十六進数)で「{0}」が表示されたSbemail Song",
            },
        },

        // Scavenger Hunt
        // Which tile was correctly submitted in the first stage of {0}?
        // Which tile was correctly submitted in the first stage of Scavenger Hunt?
        [Question.ScavengerHuntKeySquare] = new()
        {
            QuestionText = "{0}のステージ{1}で正しく送信されたタイルは？",
            ModuleName = "宝探し",
        },
        // Which of these tiles was {1} in the first stage of {0}?
        // Which of these tiles was red in the first stage of Scavenger Hunt?
        [Question.ScavengerHuntColoredTiles] = new()
        {
            QuestionText = "{0}の最初のステージで{1}色だったタイルは？",
            ModuleName = "宝探し",
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
        [Question.SchlagDenBombContestantName] = new()
        {
            QuestionText = "{0}の出場者の名前は？",
            ModuleName = "シュラグ・デン・ボム",
        },
        // What was the contestant’s score in {0}?
        // What was the contestant’s score in Schlag den Bomb?
        [Question.SchlagDenBombContestantScore] = new()
        {
            QuestionText = "{0}の出場者のスコアは？",
            ModuleName = "シュラグ・デン・ボム",
        },
        // What was the bomb’s score in {0}?
        // What was the bomb’s score in Schlag den Bomb?
        [Question.SchlagDenBombBombScore] = new()
        {
            QuestionText = "{0}の爆弾のスコアは？",
            ModuleName = "シュラグ・デン・ボム",
        },

        // Scramboozled Eggain
        // What was the {1} encrypted word in {0}?
        // What was the first encrypted word in Scramboozled Eggain?
        [Question.ScramboozledEggainWord] = new()
        {
            QuestionText = "{0}で解読した{1}番目の単語は？",
            ModuleName = "再卵炒",
        },

        // Scripting
        // What was the submitted data type of the variable in {0}?
        // What was the submitted data type of the variable in Scripting?
        [Question.ScriptingVariableDataType] = new()
        {
            QuestionText = "{0}で送信した変数の型は？",
            ModuleName = "スクリプト修正",
        },

        // Scrutiny Squares
        // What was the modified property of the first display in {0}?
        // What was the modified property of the first display in Scrutiny Squares?
        [Question.ScrutinySquaresFirstDifference] = new()
        {
            QuestionText = "{0}の最初の表示内容で修正されていたのはどの要素？",
            ModuleName = "正方形精査",
            Answers = new Dictionary<string, string>
            {
                ["Word"] = "単語",
                ["Color around word"] = "単語の周りの四角の色",
                ["Color of background"] = "背景の色",
                ["Color of word"] = "単語の色",
            },
        },

        // Sea Shells
        // What were the first and second words in the {1} phrase in {0}?
        // What were the first and second words in the first phrase in Sea Shells?
        [Question.SeaShells1] = new()
        {
            QuestionText = "{0}の{1}フレーズ目で使用された1,2番目の単語は？",
            ModuleName = "シーシェル",
        },
        // What were the third and fourth words in the {1} phrase in {0}?
        // What were the third and fourth words in the first phrase in Sea Shells?
        [Question.SeaShells2] = new()
        {
            QuestionText = "{0}の{1}フレーズ目で使用された3,4番目の単語は？",
            ModuleName = "シーシェル",
        },
        // What was the end of the {1} phrase in {0}?
        // What was the end of the first phrase in Sea Shells?
        [Question.SeaShells3] = new()
        {
            QuestionText = "{0}の{1}フレーズ目で使用された最後の単語は？",
            ModuleName = "シーシェル",
        },

        // Semamorse
        // What was the {1} letter involved in the starting value in {0}?
        // What was the Morse letter involved in the starting value in Semamorse?
        [Question.SemamorseLetters] = new()
        {
            QuestionText = "{0}の初期値を求める際に使用した表示のうち{1}の英字は？",
            ModuleName = "セマモールス",
            FormatArgs = new Dictionary<string, string>
            {
                ["Morse"] = "モールス",
                ["semaphore"] = "セマフォア",
            },
        },
        // What was the color of the display involved in the starting value in {0}?
        // What was the color of the display involved in the starting value in Semamorse?
        [Question.SemamorseColor] = new()
        {
            QuestionText = "{0}の初期値を求める際に使用した表示の色は？",
            ModuleName = "セマモールス",
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
        [Question.SequencyclopediaSequence] = new()
        {
            QuestionText = "{0}では何のシークエンスが使用された？",
        },

        // S.E.T. Theory
        // What equation was shown in the {1} stage of {0}?
        // What equation was shown in the first stage of S.E.T. Theory?
        [Question.SetTheoryEquations] = new()
        {
            QuestionText = "{0}のステージ{1}で表示されたのは？",
        },

        // Shapes And Bombs
        // What was the initial letter in {0}?
        // What was the initial letter in Shapes And Bombs?
        [Question.ShapesAndBombsInitialLetter] = new()
        {
            QuestionText = "{0}の初期の英字は？",
            ModuleName = "形と爆弾",
        },

        // Shape Shift
        // What was the initial shape in {0}?
        // What was the initial shape in Shape Shift?
        [Question.ShapeShiftInitialShape] = new()
        {
            QuestionText = "{0}の最初の図形は？",
            ModuleName = "形状変化",
        },

        // Shifted Maze
        // What color was the {1} marker in {0}?
        // What color was the top-left marker in Shifted Maze?
        [Question.ShiftedMazeColors] = new()
        {
            QuestionText = "{0}の{1}にあるマークの色は？",
            ModuleName = "シフト迷路",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-right"] = "右上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
            Answers = new Dictionary<string, string>
            {
                ["White"] = "白",
                ["Blue"] = "青",
                ["Yellow"] = "黄",
                ["Magenta"] = "マゼンタ",
                ["Green"] = "緑",
            },
        },

        // Shifting Maze
        // What was the seed in {0}?
        // What was the seed in Shifting Maze?
        [Question.ShiftingMazeSeed] = new()
        {
            QuestionText = "{0}のシード値は？",
            ModuleName = "シフト中迷路",
        },

        // Shogi Identification
        // What was the displayed piece in {0}?
        // What was the displayed piece in Shogi Identification?
        [Question.ShogiIdentificationPiece] = new()
        {
            QuestionText = "{0}に表示された駒は？",
            ModuleName = "将棋識別",
            Answers = new Dictionary<string, string>
            {
                ["Go-Between"] = "仲",
                ["Pawn"] = "歩",
                ["Side Mover"] = "横",
                ["Vertical Mover"] = "堅",
                ["Bishop"] = "角",
                ["Rook"] = "飛",
                ["Dragon Horse"] = "馬",
                ["Dragon King"] = "龍",
                ["Lance"] = "香",
                ["Reverse Chariot"] = "反",
                ["Blind Tiger"] = "虎",
                ["Ferocious Leopard"] = "豹",
                ["Copper General"] = "鋼",
                ["Silver General"] = "銀",
                ["Gold General"] = "金",
                ["Drunk Elephant"] = "蔵",
                ["Kirin"] = "麒",
                ["Phoenix"] = "鳳",
                ["Queen"] = "奔",
                ["Flying Stag"] = "鹿",
                ["Flying Ox"] = "牛",
                ["Free Boar"] = "猪",
                ["Whale"] = "鯨",
                ["White Horse"] = "駒",
                ["King"] = "王",
                ["Prince"] = "太",
                ["Horned Falcon"] = "鷹",
                ["Soaring Eagle"] = "鷲",
                ["Lion"] = "獅",
            },
        },

        // Sign Language
        // What was the deciphered word in {0}?
        // What was the deciphered word in Sign Language?
        [Question.SignLanguageWord] = new()
        {
            QuestionText = "{0}で解読した単語は？",
            ModuleName = "手話",
        },

        // Silly Slots
        // What was the {1} slot in the {2} stage in {0}?
        // What was the first slot in the first stage in Silly Slots?
        [Question.SillySlots] = new()
        {
            QuestionText = "{0}のステージ{2}において、{1}列目のスロットは？",
            ModuleName = "ヘンテコスロット",
            Answers = new Dictionary<string, string>
            {
                ["red bomb"] = "赤色の爆弾",
                ["red cherry"] = "赤色のチェリー",
                ["red coin"] = "赤色のコイン",
                ["red grape"] = "赤色のブドウ",
                ["green bomb"] = "緑色の爆弾",
                ["green cherry"] = "緑色のチェリー",
                ["green coin"] = "緑色のコイン",
                ["green grape"] = "緑色のブドウ",
                ["blue bomb"] = "青色の爆弾",
                ["blue cherry"] = "青色のチェリー",
                ["blue coin"] = "青色のコイン",
                ["blue grape"] = "青色のブドウ",
            },
        },

        // Silo Authorization
        // What was the message type in {0}?
        // What was the message type in Silo Authorization?
        [Question.SiloAuthorizationMessageType] = new()
        {
            QuestionText = "{0}のメッセージの種類は？",
            ModuleName = "シロ認証",
        },
        // What was the {1} part of the encrypted message in {0}?
        // What was the first part of the encrypted message in Silo Authorization?
        [Question.SiloAuthorizationEncryptedMessage] = new()
        {
            QuestionText = "{0}の暗号メッセージでパート{1}は？",
            ModuleName = "シロ認証",
        },
        // What was the received authentication code in {0}?
        // What was the received authentication code in Silo Authorization?
        [Question.SiloAuthorizationAuthCode] = new()
        {
            QuestionText = "{0}で受信した認証コードは？",
            ModuleName = "シロ認証",
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
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Yellow"] = "黄",
            },
        },

        // Simon Samples
        // What were the call samples {1} of {0}?
        // What were the call samples played in the first stage of Simon Samples?
        [Question.SimonSamplesSamples] = new()
        {
            QuestionText = "{0}の{1}音は？",
            ModuleName = "サイモンの音源",
            FormatArgs = new Dictionary<string, string>
            {
                ["played in the first stage"] = "ステージ1で演奏された",
                ["added in the second stage"] = "ステージ2で演奏された",
                ["added in the third stage"] = "ステージ3で演奏された",
            },
        },

        // Simon Says
        // What color flashed {1} in the final sequence in {0}?
        // What color flashed first in the final sequence in Simon Says?
        [Question.SimonSaysFlash] = new()
        {
            QuestionText = "{0}の最終シークエンスにおいて、{1}番目に点滅した色は？",
            ModuleName = "サイモンゲーム",
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
        [Question.SimonScramblesColors] = new()
        {
            QuestionText = "{0}の{1}番目の点滅は？",
            ModuleName = "サイモンの撹拌",
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
        [Question.SimonScreamsFlashing] = new()
        {
            QuestionText = "{0}の最終シークエンスにおいて、{1}番目に点滅した色は？",
            ModuleName = "サイモンの絶叫",
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
        // In which stage(s) of Simon Screams was “a color flashed, then a color two away, then the first again” the applicable rule?
        [Question.SimonScreamsRuleSimple] = new()
        {
            QuestionText = "{0}で「{1}」のルールが当てはまったのはどのステージ？",
            ModuleName = "サイモンの絶叫",
            FormatArgs = new Dictionary<string, string>
            {
                ["a color flashed, then a color two away, then the first again"] = "ある色が点滅した直後、2つ離れた色が点滅し、その直後に最初の色が再び点滅した",
                ["a color flashed, then a color two away, then the one opposite that"] = "ある色が点滅した直後、2つ離れた色が点滅し、その直後に直前の色の対角線上にある色が点滅した",
                ["a color flashed, then a color two away, then the one opposite the first"] = "ある色が点滅した直後、2つ離れた色が点滅し、その直後に最初の色の対角線上にある色が点滅した",
                ["a color flashed, then an adjacent color, then the first again"] = "ある色が点滅した直後、それに隣接した色が点滅し、その直後に再度元の色が点滅した",
                ["a color flashed, then another color, then the first"] = "ある色が点滅した直後、別の色が点滅し、その直後に最初の色が点滅した",
                ["a color flashed, then one adjacent, then the one opposite that"] = "ある色が点滅した直後、それに隣接した色が点滅し、その直後に直前の色の対角線上にある色が点滅した",
                ["a color flashed, then one adjacent, then the one opposite the first"] = "ある色が点滅した直後、隣接した色が点滅し、その直後に最初の色の対角線上にある色が点滅した",
                ["a color flashed, then the one opposite, then one adjacent to that"] = "ある色が点滅した直後、その対角線上にある色が点滅し、その直後に直前の色に隣接した色が点滅した",
                ["a color flashed, then the one opposite, then one adjacent to the first"] = "ある色が点滅した直後、その対角線上にある色が点滅し、 その直後に最初の色に隣接する色が点滅した",
                ["a color flashed, then the one opposite, then the first again"] = "ある色が点滅した直後、その対角線上にある色が点滅し、その直後に再度元の色が点滅した",
                ["every color flashed at least once"] = "全ての色が少なくとも1回点滅した",
                ["exactly one color flashed exactly twice"] = "ちょうど2回点滅した色が、ちょうど1色ある",
                ["exactly one color flashed more than once"] = "1回より多く点滅した色が、ちょうど1色ある",
                ["exactly two colors flashed exactly twice"] = "ちょうど2回点滅した色が、ちょうど2色ある",
                ["exactly two colors flashed more than once"] = "1回より多く点滅した色が、ちょうど2色ある",
                ["no color flashed exactly twice"] = "ちょうど2回点滅した色が存在しない",
                ["no color flashed more than once"] = "1回より多く点滅した色が存在しない",
                ["no two adjacent colors flashed in clockwise order"] = "どの隣接した2色も、時計回りに点滅していない",
                ["no two adjacent colors flashed in counter-clockwise order"] = "どの隣接した2色も、反時計回りに点滅していない",
                ["no two colors two apart flashed in clockwise order"] = "どの2つ離れた2色も、時計回りに点滅していない",
                ["no two colors two apart flashed in counter-clockwise order"] = "どの2つ離れた2色も、反時計回りに点滅していない",
                ["the colors flashing first and last are adjacent"] = "最初に点滅した色と最後に点滅した色が隣接している",
                ["the colors flashing first and last are different and not adjacent"] = "最初に点滅した色と最後に点滅した色が異なり、かつ隣接していない",
                ["the colors flashing first and last are the same"] = "最初に点滅した色と最後に点滅した色が同じである",
                ["the number of distinct colors that flashed is even"] = "点滅した異なる色の数が、偶数である",
                ["the number of distinct colors that flashed is odd"] = "点滅した異なる色の数が、奇数である",
                ["there are at least three colors that didn’t flash"] = "少なくとも3色が、点滅していない",
                ["there are exactly two colors that didn’t flash"] = "点滅していない色が、ちょうど2色ある",
                ["there are two colors adjacent to each other that didn’t flash"] = "点滅していない隣接した2色がある",
                ["there are two colors opposite each other that didn’t flash"] = "点滅していない対角線上にある2色がある",
                ["there are two colors two away from each other that didn’t flash"] = "点滅していない2つ離れた2色がある",
                ["there is exactly one color that didn’t flash"] = "点滅していない色が、ちょうど1色ある",
                ["three adjacent colors did not flash"] = "点滅していない隣接した3色がある",
                ["three adjacent colors flashed in clockwise order"] = "隣接する3色が、時計回りに点滅した",
                ["three adjacent colors flashed in counter-clockwise order"] = "隣接する3色が、反時計回りに点滅した",
                ["three colors, each two apart, flashed in clockwise order"] = "2つ離れた3色が、時計回りに点滅した",
                ["three colors, each two apart, flashed in counter-clockwise order"] = "2つ離れた3色が、反時計回りに点滅した",
                ["two adjacent colors flashed in clockwise order"] = "隣接した2色が、時計回りに点滅した",
                ["two adjacent colors flashed in counter-clockwise order"] = "隣接した2色が、反時計回りに点滅した",
                ["two colors two apart flashed in clockwise order"] = "2つ離れた2色が、時計回りに点滅した",
                ["two colors two apart flashed in counter-clockwise order"] = "2つ離れた2色が、反時計回りに点滅した",
            },
            Answers = new Dictionary<string, string>
            {
                ["first"] = "1",
                ["second"] = "2",
                ["third"] = "3",
                ["first and second"] = "1と2",
                ["first and third"] = "1と3",
                ["second and third"] = "2と3",
                ["all of them"] = "すべて",
            },
        },
        // In which stage(s) of {0} was “{1} flashed out of {2}, {3}, and {4}” the applicable rule?
        // In which stage(s) of Simon Screams was “at most one color flashed out of Red, Orange, and Yellow” the applicable rule?
        [Question.SimonScreamsRuleComplex] = new()
        {
            QuestionText = "{0}「flashed out of {2}、 {3}、{4}のうち{1}」のルールが当てはまったのはどのステージ？",
            ModuleName = "サイモンの絶叫",
            FormatArgs = new Dictionary<string, string>
            {
                ["at most one color"] = "多くとも1色しか点滅していない",
                ["Red"] = "赤",
                ["Orange"] = "橙",
                ["Yellow"] = "黄",
                ["at least two colors"] = "少なくとも2色が点滅した",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Purple"] = "紫",
            },
            Answers = new Dictionary<string, string>
            {
                ["first"] = "1",
                ["second"] = "2",
                ["third"] = "3",
                ["first and second"] = "1と2",
                ["first and third"] = "1と3",
                ["second and third"] = "2と3",
                ["all of them"] = "すべて",
            },
        },

        // Simon Selects
        // Which color flashed {1} in the {2} stage of {0}?
        // Which color flashed first in the first stage of Simon Selects?
        [Question.SimonSelectsOrder] = new()
        {
            QuestionText = "{0}のステージ{2}において、{1}番目に点滅した色は？",
            ModuleName = "サイモンの選択",
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
        [Question.SimonSendsReceivedLetters] = new()
        {
            QuestionText = "{0}で{1}色が受け取った英字は？",
            ModuleName = "サイモンの送信",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
            },
        },

        // Simon Serves
        // Who flashed {1} in course {2} of {0}?
        // Who flashed first in course 1 of Simon Serves?
        [Question.SimonServesFlash] = new()
        {
            QuestionText = "{0}の{2}品目で{1}番目に点滅したのは？",
            ModuleName = "サイモンの給仕",
        },
        // Which item was not served in course {1} of {0}?
        // Which item was not served in course 1 of Simon Serves?
        [Question.SimonServesFood] = new()
        {
            QuestionText = "{0}の{1}品目で提供されなかった商品は？",
            ModuleName = "サイモンの給仕",
            Answers = new Dictionary<string, string>
            {
                ["Baked Batterys"] = "ベイクドバッテリー",
                ["Bamboozling Waffles"] = "錯綜ワッフル",
                ["Big Boom Tortellini"] = "大爆発トルテリーニ",
                ["Blast Shrimps"] = "粉々のエビ",
                ["Blastwave Compote"] = "旬の果物のコンポート",
                ["Bomb Brûlée"] = "ボムブリュレ",
                ["Boolean Waffles"] = "ブーリアンワッフル",
                ["Boom Lager Beer"] = "ドーン ラガービール",
                ["Caesar Salad"] = "カエサルサラダ",
                ["Centurion Wings"] = "センチュリオンチキンウィング",
                ["Colored Spare Ribs"] = "色付きスペアリブステーキ",
                ["Cruelo Juice"] = "残忍ジュース",
                ["Defuse Juice"] = "解除(した気分になれる)ジュース",
                ["Defuse au Chocolat"] = "ディフューズ・オ・ショコラ",
                ["Deto Bull"] = "『Deto Bull』",
                ["Edgework Toast"] = "爆弾ケース風トースト",
                ["Forget Cocktail"] = "カクテル『忘る』",
                ["Forghetti Bombognese"] = "フォゲッティ・ボンネーゼ",
                ["Indicator Tar Tar"] = "インジケーター風 タルタルステーキ",
                ["Morse Soup"] = "モールススープ",
                ["NATO Shrimps"] = "NATO推奨調理法のエビ",
                ["Not Ice Cream"] = "偽アイスクリーム",
                ["Omelette au Bombage"] = "オムレツ・オ・ボムマージュ",
                ["Simon’s Special Mix"] = "サイモンのスペシャルミックス",
                ["Solve Cake"] = "ソルブケーキ",
                ["Status Light Rolls"] = "ステータスライトロール",
                ["Strike Pie"] = "ミスったパイ",
                ["Tasha’s Drink"] = "ターシャの飲み物",
                ["Ticking Timecakes"] = "時を刻むタイマーケーキ",
                ["Veggie Blast Plate"] = "野菜の粉々プレート",
                ["Wire Shake"] = "ワイヤミルクセーキ",
                ["Wire Spaghetti"] = "ワイヤスパゲティ",
            },
        },

        // Simon Shapes
        // What was the shape submitted at the end of {0}?
        // What was the shape submitted at the end of Simon Shapes?
        [Question.SimonShapesSubmittedShape] = new()
        {
            QuestionText = "{0}で最終的に送信した図形は？",
            ModuleName = "サイモンの形状",
        },

        // Simon Shouts
        // Which letter flashed on the {1} button in {0}?
        // Which letter flashed on the top button in Simon Shouts?
        [Question.SimonShoutsFlashingLetter] = new()
        {
            QuestionText = "{0}の{1}の位置が点滅した英字は？",
            ModuleName = "サイモンの叫び",
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
        [Question.SimonShrieksFlashingButton] = new()
        {
            QuestionText = "{0}の最終シークエンスにおいて、{1}番目の点滅は矢印から時計回りに何番目のスペースにある？",
            ModuleName = "サイモンの悲鳴",
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
            QuestionText = "{0}の{1}色矢印の形は？",
            ModuleName = "サイモンの信号",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
                ["gray"] = "灰",
            },
        },
        // How many directions did the {1} arrow in {0} have?
        // How many directions did the red arrow in Simon Signals have?
        [Question.SimonSignalsColorToRotations] = new()
        {
            QuestionText = "{0}の{1}色矢印は何個あった？",
            ModuleName = "サイモンの信号",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
                ["gray"] = "灰",
            },
        },
        // What color was the arrow with this shape in {0}?
        // What color was the arrow with this shape in Simon Signals?
        [Question.SimonSignalsShapeToColor] = new()
        {
            QuestionText = "{0}で、この形の矢印は何色だった？",
            ModuleName = "サイモンの信号",
            Answers = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
                ["gray"] = "灰",
            },
        },
        // How many directions did the arrow with this shape have in {0}?
        // How many directions did the arrow with this shape have in Simon Signals?
        [Question.SimonSignalsShapeToRotations] = new()
        {
            QuestionText = "{0}で、この形の矢印は何回出現した？",
            ModuleName = "サイモンの信号",
        },
        // What color was the arrow with {1} possible directions in {0}?
        // What color was the arrow with 3 possible directions in Simon Signals?
        [Question.SimonSignalsRotationsToColor] = new()
        {
            QuestionText = "{0}で{1}方向を指していた矢印の色は？",
            ModuleName = "サイモンの信号",
            Answers = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
                ["gray"] = "灰",
            },
        },
        // What shape was the arrow with {1} possible directions in {0}?
        // What shape was the arrow with 3 possible directions in Simon Signals?
        [Question.SimonSignalsRotationsToShape] = new()
        {
            QuestionText = "{0}で{1}方向を指していた矢印の形は？",
            ModuleName = "サイモンの信号",
        },

        // Simon Simons
        // What was the {1} flash in the final sequence in {0}?
        // What was the first flash in the final sequence in Simon Simons?
        [Question.SimonSimonsFlashingColors] = new()
        {
            QuestionText = "{0}の最終シークエンスにおいて、{1}番目に点滅した色は？",
            ModuleName = "サイモンのサイモン",
        },

        // Simon Sings
        // Which key’s color flashed {1} in the {2} stage of {0}?
        // Which key’s color flashed first in the first stage of Simon Sings?
        [Question.SimonSingsFlashing] = new()
        {
            QuestionText = "{0}のステージ{2}において、{1}番目に点滅したキーは？",
            ModuleName = "サイモンの歌唱",
        },

        // Simon Smiles
        // What sound did the {1} button press make {0}?
        // What sound did the first button press make Simon Smiles?
        [Question.SimonSmilesSounds] = new()
        {
            QuestionText = "{0}で{1}番目に押したボタンが鳴らした音は？",
            ModuleName = "サイモンの笑顔",
        },

        // Simon Smothers
        // What was the color of the {1} flash in {0}?
        // What was the color of the first flash in Simon Smothers?
        [Question.SimonSmothersColors] = new()
        {
            QuestionText = "{0}の{1}番目に点滅した色は？",
            ModuleName = "サイモンの隠匿",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Yellow"] = "黄",
                ["Blue"] = "青",
                ["Magenta"] = "マゼンタ",
                ["Cyan"] = "シアン",
            },
        },
        // What was the direction of the {1} flash in {0}?
        // What was the direction of the first flash in Simon Smothers?
        [Question.SimonSmothersDirections] = new()
        {
            QuestionText = "{0}の{1}番目に点滅した方向は？",
            ModuleName = "サイモンの隠匿",
            Answers = new Dictionary<string, string>
            {
                ["Up"] = "上",
                ["Down"] = "下",
                ["Left"] = "左",
                ["Right"] = "右",
            },
        },

        // Simon Sounds
        // Which sample button sounded {1} in the final sequence in {0}?
        // Which sample button sounded first in the final sequence in Simon Sounds?
        [Question.SimonSoundsFlashingColors] = new()
        {
            QuestionText = "{0}の最終シークエンスにおいて、{1}番目に再生されたサンプルボタンの色は？",
            ModuleName = "サイモンの響き",
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
        [Question.SimonSpeaksPositions] = new()
        {
            QuestionText = "{0}の1番目の点滅の吹き出しは？",
            ModuleName = "サイモンの発話",
            Answers = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-middle"] = "上",
                ["top-right"] = "右上",
                ["middle-left"] = "左",
                ["middle-center"] = "中央",
                ["middle-right"] = "右",
                ["bottom-left"] = "左下",
                ["bottom-middle"] = "下",
                ["bottom-right"] = "右下",
            },
        },
        // Which bubble flashed second in {0}?
        // Which bubble flashed second in Simon Speaks?
        [Question.SimonSpeaksShapes] = new()
        {
            QuestionText = "{0}の2番目の点滅の吹き出しは？",
            ModuleName = "サイモンの発話",
        },
        // Which language was the bubble that flashed third in {0} in?
        // Which language was the bubble that flashed third in Simon Speaks in?
        [Question.SimonSpeaksLanguages] = new()
        {
            QuestionText = "{0}の3回目の点滅の言語は？",
            ModuleName = "サイモンの発話",
        },
        // Which word was in the bubble that flashed fourth in {0}?
        // Which word was in the bubble that flashed fourth in Simon Speaks?
        [Question.SimonSpeaksWords] = new()
        {
            QuestionText = "{0}の4番目の点滅の単語は？",
            ModuleName = "サイモンの発話",
        },
        // What color was the bubble that flashed fifth in {0}?
        // What color was the bubble that flashed fifth in Simon Speaks?
        [Question.SimonSpeaksColors] = new()
        {
            QuestionText = "{0}の5番目の点滅の吹き出しの色は？",
            ModuleName = "サイモンの発話",
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
        // Which color flashed {1} in {0}?
        // Which color flashed first in Simon’s Star?
        [Question.SimonsStarColors] = new()
        {
            NeedsTranslation = true,
            QuestionText = "{0}のシークエンスにおいて、{1}番目に点滅した色は？",
            ModuleName = "サイモンの星",
            Answers = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["yellow"] = "黄",
                ["green"] = "緑",
                ["blue"] = "青",
                ["purple"] = "紫",
            },
        },

        // Simon Stacks
        // Which color flashed in the {1} stage of {0}?
        // Which color flashed in the first stage of Simon Stacks?
        [Question.SimonStacksColors] = new()
        {
            QuestionText = "{0}のステージ{1}で点滅した色は？",
            ModuleName = "六角形サイモン",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Yellow"] = "黄",
            },
        },

        // Simon Stages
        // Which color flashed {1} in the {2} stage in {0}?
        // Which color flashed first in the first stage in Simon Stages?
        [Question.SimonStagesFlashes] = new()
        {
            QuestionText = "{0}のステージ{2}における{1}番目の点滅した色は？",
            ModuleName = "サイモンステージ",
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
        [Question.SimonStagesIndicator] = new()
        {
            QuestionText = "{0}のステージ{1}におけるインジケーターの色は？",
            ModuleName = "サイモンステージ",
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
        [Question.SimonStatesDisplay] = new()
        {
            QuestionText = "{0}のステージ{2}ではどの{1}？",
            ModuleName = "サイモンの陳述",
            FormatArgs = new Dictionary<string, string>
            {
                ["color(s) flashed"] = "色が点滅した",
                ["color(s) didn’t flash"] = "色が点滅しなかった",
            },
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
        },

        // Simon Stops
        // Which color flashed {1} in the output sequence in {0}?
        // Which color flashed first in the output sequence in Simon Stops?
        [Question.SimonStopsColors] = new()
        {
            QuestionText = "{0}の出力シークエンスにおいて、{1}番目に点滅した色は？",
            ModuleName = "サイモンの停止",
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
        [Question.SimonStoresColors] = new()
        {
            QuestionText = "{0}の最終シークエンスにおいて、{2}番目に{1}色は？",
            ModuleName = "サイモンの貯留",
            FormatArgs = new Dictionary<string, string>
            {
                ["flashed"] = "点滅した",
                ["was among the colors flashed"] = "点滅した色に含まれる",
            },
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Cyan"] = "シアン",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
            },
        },

        // Simon Subdivides
        // What color was the button at this position in {0}?
        // What color was the button at this position in Simon Subdivides?
        [Question.SimonSubdividesButton] = new()
        {
            QuestionText = "{0}のこの位置にあったボタンの色は？",
            ModuleName = "サイモンの分割",
            Answers = new Dictionary<string, string>
            {
                ["Blue"] = "",
                ["Green"] = "",
                ["Red"] = "",
                ["Violet"] = "",
            },
        },

        // Simon Supports
        // What was the {1} topic in {0}?
        // What was the first topic in Simon Supports?
        [Question.SimonSupportsTopics] = new()
        {
            QuestionText = "{0}の{1}番目のトピックは？",
            ModuleName = "サイモンの支持",
            Answers = new Dictionary<string, string>
            {
                ["Boss"] = "ボス",
                ["Cruel"] = "残忍",
                ["Faulty"] = "欠陥",
                ["Lookalike"] = "類似",
                ["Puzzle"] = "パズル",
                ["Simon"] = "サイモン",
                ["Time-Based"] = "時間依存",
                ["Translated"] = "翻訳",
            },
        },

        // Simon Swizzles
        // Where was {1} in {0}?
        // Where was OFF in Simon Swizzles?
        [Question.SimonSwizzlesButton] = new()
        {
            QuestionText = "{0}の{1}はどこにあった？",
            FormatArgs = new Dictionary<string, string>
            {
                ["OFF"] = "オフ",
                ["ON"] = "オン",
            },
        },
        // What was the hidden number in {0}?
        // What was the hidden number in Simon Swizzles?
        [Question.SimonSwizzlesNumber] = new()
        {
            QuestionText = "{0}で隠された数字は？",
        },

        // Simply Simon
        // What were the flashes in the {1} stage of {0}?
        // What were the flashes in the first stage of Simply Simon?
        [Question.SimplySimonFlash] = new()
        {
            NeedsTranslation = true,
            QuestionText = "What were the flashes in the {1} stage of {0}?",
        },

        // Simultaneous Simons
        // What color flashed {1} on the {2} Simon in {0}?
        // What color flashed first on the first Simon in Simultaneous Simons?
        [Question.SimultaneousSimonsFlash] = new()
        {
            QuestionText = "{0}の{2}番目のサイモンにおける{1}番目の点滅は？",
            ModuleName = "同時サイモン",
            Answers = new Dictionary<string, string>
            {
                ["Blue"] = "青",
                ["Yellow"] = "黄",
                ["Red"] = "赤",
                ["Green"] = "緑",
            },
        },

        // Skewed Slots
        // What were the original numbers in {0}?
        // What were the original numbers in Skewed Slots?
        [Question.SkewedSlotsOriginalNumbers] = new()
        {
            QuestionText = "{0}の初期値は？",
            ModuleName = "歪曲スロット",
        },

        // Skewers
        // What color was this gem in {0}?
        // What color was this gem in Skewers?
        [Question.SkewersColor] = new()
        {
            QuestionText = "{0}のこの宝石の色は？",
            ModuleName = "剣刺し",
            Answers = new Dictionary<string, string>
            {
                ["Black"] = "黒",
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Yellow"] = "黄",
                ["Blue"] = "青",
                ["Magenta"] = "マゼンタ",
                ["Cyan"] = "シアン",
                ["White"] = "白",
            },
        },

        // Skyrim
        // Which race was selectable, but not the solution, in {0}?
        // Which race was selectable, but not the solution, in Skyrim?
        [Question.SkyrimRace] = new()
        {
            QuestionText = "{0}において、選択可能だが回答ではなかった種族は？",
            ModuleName = "スカイリム",
            Answers = new Dictionary<string, string>
            {
                ["Nord"] = "ノルド",
                ["Khajiit"] = "カジート",
                ["Breton"] = "ブレトン",
                ["Argonian"] = "アルゴニアン",
                ["Dunmer"] = "ダークエルフ",
                ["Altmer"] = "ハイエルフ",
                ["Redguard"] = "レッドガード",
                ["Orc"] = "オーク",
                ["Imperial"] = "インペリアル",
            },
        },
        // Which weapon was selectable, but not the solution, in {0}?
        // Which weapon was selectable, but not the solution, in Skyrim?
        [Question.SkyrimWeapon] = new()
        {
            QuestionText = "{0}において、選択可能だが回答ではなかった武器は？",
            ModuleName = "スカイリム",
            Answers = new Dictionary<string, string>
            {
                ["Axe of Whiterun"] = "ホワイトランの斧",
                ["Dawnbreaker"] = "ドーンブレイカー",
                ["Windshear"] = "ウィンドシア",
                ["Blade of Woe"] = "悲痛の短剣",
                ["Firiniel’s End"] = "フィリニエルズエンド",
                ["Bow of Hunt"] = "狩りの弓",
                ["Volendrung"] = "ヴォレンドラング",
                ["Chillrend"] = "チルレンド",
                ["Mace of Molag Bal"] = "モラグ・バルのメイス",
            },
        },
        // Which enemy was selectable, but not the solution, in {0}?
        // Which enemy was selectable, but not the solution, in Skyrim?
        [Question.SkyrimEnemy] = new()
        {
            QuestionText = "{0}において、選択可能だが解除策ではなかった敵は？",
            ModuleName = "スカイリム",
            Answers = new Dictionary<string, string>
            {
                ["Alduin"] = "アルドゥイン",
                ["Blood Dragon"] = "ブラッド・ドラゴン",
                ["Cave Bear"] = "ホラアナグマ",
                ["Dragon Priest"] = "ドラゴン・プリースト",
                ["Draugr"] = "ドラウグル",
                ["Draugr Overlord"] = "ドラウグル・オーバーロード",
                ["Frost Troll"] = "フロスト・トロール",
                ["Frostbite Spider"] = "フロストバイトスパイダー",
                ["Mudcrab"] = "マッドクラブ",
            },
        },
        // Which city was selectable, but not the solution, in {0}?
        // Which city was selectable, but not the solution, in Skyrim?
        [Question.SkyrimCity] = new()
        {
            QuestionText = "{0}において、選択可能だが回答ではなかった故郷は？",
            ModuleName = "スカイリム",
            Answers = new Dictionary<string, string>
            {
                ["Dawnstar"] = "ドーンスター",
                ["Ivarstead"] = "イヴァルステッド",
                ["Markarth"] = "マルカルス",
                ["Riverwood"] = "リバーウッド",
                ["Rorikstead"] = "ロリクステッド",
                ["Solitude"] = "ソリチュード",
                ["Whiterun"] = "ホワイトラン",
                ["Windhelm"] = "ウィンドヘルム",
                ["Winterhold"] = "ウィンターホールド",
            },
        },
        // Which dragon shout was selectable, but not the solution, in {0}?
        // Which dragon shout was selectable, but not the solution, in Skyrim?
        [Question.SkyrimDragonShout] = new()
        {
            QuestionText = "{0}において、選択可能だが解除策ではなかったドラゴンシャウトは？",
            ModuleName = "スカイリム",
            Answers = new Dictionary<string, string>
            {
                ["Disarm"] = "武装解除",
                ["Dismay"] = "不安",
                ["Dragonrend"] = "ドラゴンレンド",
                ["Fire Breath"] = "ファイアブレス",
                ["Ice Form"] = "氷晶",
                ["Kyne’s Peace"] = "カイネの安らぎ",
                ["Slow Time"] = "時間減速",
                ["Unrelenting Force"] = "揺るぎ無き力",
                ["Whirlwind Sprint"] = "旋風の疾走",
            },
        },

        // Slow Math
        // What was the last triplet of letters in {0}?
        // What was the last triplet of letters in Slow Math?
        [Question.SlowMathLastLetters] = new()
        {
            QuestionText = "{0}の最後の英字三組は？",
        },

        // Small Circle
        // How much did the sequence shift by in {0}?
        // How much did the sequence shift by in Small Circle?
        [Question.SmallCircleShift] = new()
        {
            QuestionText = "{0}におけるシークエンスのシフト量は？",
            ModuleName = "スモールサークル",
        },
        // Which wedge made the different noise in the beginning of {0}?
        // Which wedge made the different noise in the beginning of Small Circle?
        [Question.SmallCircleWedge] = new()
        {
            QuestionText = "{0}の初期時点で音が違っていたのは？",
            ModuleName = "スモールサークル",
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
        // Which color was {1} in the solution to {0}?
        // Which color was first in the solution to Small Circle?
        [Question.SmallCircleSolution] = new()
        {
            QuestionText = "{0}の解除シークエンスの{1}番目の色は？",
            ModuleName = "スモールサークル",
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
            QuestionText = "{0}で{1}が属していたカテゴリーは？",
            ModuleName = "SMASH・MARRY・KILL",
        },
        // Which module was in the {1} category for {0}?
        // Which module was in the SMASH category for Smash, Marry, Kill?
        [Question.SmashMarryKillModule] = new()
        {
            QuestionText = "{0}で{1}のカテゴリーに属していたモジュールは？",
            ModuleName = "SMASH・MARRY・KILL",
        },

        // Snooker
        // How many red balls were there at the start of {0}?
        // How many red balls were there at the start of Snooker?
        [Question.SnookerReds] = new()
        {
            QuestionText = "{0}の開始時点での赤いボールの数は？",
            ModuleName = "スヌーカー",
        },

        // Snowflakes
        // Which snowflake was on the {1} button of {0}?
        // Which snowflake was on the top button of Snowflakes?
        [Question.SnowflakesDisplayedSnowflakes] = new()
        {
            QuestionText = "{0}の{1}のボタンにあった結晶は？",
            ModuleName = "雪の結晶",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["right"] = "右",
                ["bottom"] = "下",
                ["left"] = "左",
            },
        },

        // Sonic & Knuckles
        // Which sound was played but not featured in the chosen zone in {0}?
        // Which sound was played but not featured in the chosen zone in Sonic & Knuckles?
        [Question.SonicKnucklesSounds] = new()
        {
            QuestionText = "{0}で選ばれたゾーンに含まれなかったが再生された音は？",
            ModuleName = "ソニック＆ナックルズ",
        },
        // Which badnik was shown in {0}?
        // Which badnik was shown in Sonic & Knuckles?
        [Question.SonicKnucklesBadnik] = new()
        {
            QuestionText = "{0}に表示されたバドニクは？",
            ModuleName = "ソニック＆ナックルズ",
        },
        // Which monitor was shown in {0}?
        // Which monitor was shown in Sonic & Knuckles?
        [Question.SonicKnucklesMonitor] = new()
        {
            QuestionText = "{0}に表示されたモニターは？",
            ModuleName = "ソニック＆ナックルズ",
        },

        // Sonic The Hedgehog
        // What was the {1} picture on {0}?
        // What was the first picture on Sonic The Hedgehog?
        [Question.SonicTheHedgehogPictures] = new()
        {
            QuestionText = "{0}における{1}番目の画像は？",
            ModuleName = "ソニック・ザ・ヘッジホッグ",
        },
        // Which sound was played by the {1} screen on {0}?
        // Which sound was played by the Running Boots screen on Sonic The Hedgehog?
        [Question.SonicTheHedgehogSounds] = new()
        {
            QuestionText = "{0}において、{1}のモニターで流れていたサウンドは？",
            ModuleName = "ソニック・ザ・ヘッジホッグ",
            FormatArgs = new Dictionary<string, string>
            {
                ["Running Boots"] = "ハイスピード",
                ["Invincibility"] = "無敵",
                ["Extra Life"] = "1up",
                ["Rings"] = "リング",
            },
        },

        // Sorting
        // What positions were the last swap used to solve {0}?
        // What positions were the last swap used to solve Sorting?
        [Question.SortingLastSwap] = new()
        {
            QuestionText = "{0}を解く際の最後の入れ替えはどの位置で行われた？",
            ModuleName = "並び替え",
        },

        // Souvenir
        // What was the first module asked about in the other Souvenir on this bomb?
        // What was the first module asked about in the other Souvenir on this bomb?
        [Question.SouvenirFirstQuestion] = new()
        {
            QuestionText = "他の「思い出」モジュールが最初に質問したのは、何のモジュールについて(英名)？",
            ModuleName = "思い出",
        },

        // Space Traders
        // What was the maximum tax amount per vessel in {0}?
        // What was the maximum tax amount per vessel in Space Traders?
        [Question.SpaceTradersMaxTax] = new()
        {
            QuestionText = "{0}での1隻当たりの最大税額は？",
        },

        // Spelling Bee
        // What word was asked to be spelled in {0}?
        // What word was asked to be spelled in Spelling Bee?
        [Question.SpellingBeeWord] = new()
        {
            QuestionText = "{0}で打ち込んだ単語は？",
            ModuleName = "スペリング・ビー",
        },

        // The Sphere
        // What was the {1} flashed color in {0}?
        // What was the first flashed color in The Sphere?
        [Question.SphereColors] = new()
        {
            QuestionText = "{0}にて{1}番目に点滅した色は？",
            ModuleName = "球",
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

        // Splitting The Loot
        // What bag was initially colored in {0}?
        // What bag was initially colored in Splitting The Loot?
        [Question.SplittingTheLootColoredBag] = new()
        {
            QuestionText = "{0}にて初期から色付けされていた袋は？",
            ModuleName = "戦利品分割",
        },

        // Spongebob Birthday Identification
        // Who was the {1} child displayed in {0}?
        // Who was the first child displayed in Spongebob Birthday Identification?
        [Question.SpongebobBirthdayIdentificationChildren] = new()
        {
            QuestionText = "{0}で{1}番目に表示された人物は？",
            ModuleName = "スポンジ・ボブ誕生日カード識別",
        },

        // Stability
        // What was the color of the {1} lit LED in {0}?
        // What was the color of the first lit LED in Stability?
        [Question.StabilityLedColors] = new()
        {
            QuestionText = "{0}の{1}番目に点灯したLEDの色は？",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Yellow"] = "黄",
                ["Blue"] = "青",
            },
        },
        // What was the identification number in {0}?
        // What was the identification number in Stability?
        [Question.StabilityIdNumber] = new()
        {
            QuestionText = "{0}の判別番号は？",
        },

        // Stable Time Signatures
        // What was the {1} time signature in {0}?
        // What was the first time signature in Stable Time Signatures?
        [Question.StableTimeSignaturesSignatures] = new()
        {
            QuestionText = "{0}の{1}番目の拍子記号は？",
            ModuleName = "安定拍子記号",
        },

        // Stacked Sequences
        // Which of these is the length of a sequence in {0}?
        // Which of these is the length of a sequence in Stacked Sequences?
        [Question.StackedSequences] = new()
        {
            QuestionText = "{0}のシークエンスの長さは？",
        },

        // Stars
        // What was the digit in the center of {0}?
        // What was the digit in the center of Stars?
        [Question.StarsCenter] = new()
        {
            QuestionText = "{0}の中心の数字は？",
            ModuleName = "星",
        },

        // Starstruck
        // Which star was present on {0}?
        // Which star was present on Starstruck?
        [Question.StarstruckStar] = new()
        {
            QuestionText = "{0}に表示された星は？",
        },

        // State of Aggregation
        // What was the element shown in {0}?
        // What was the element shown in State of Aggregation?
        [Question.StateOfAggregationElement] = new()
        {
            QuestionText = "{0}に表示された要素は？",
            ModuleName = "元素状態",
        },

        // Stellar
        // What was the {1} letter in {0}?
        // What was the Morse code letter in Stellar?
        [Question.StellarLetters] = new()
        {
            QuestionText = "{0}における{1}の英字は？",
            ModuleName = "星型十二面体",
            FormatArgs = new Dictionary<string, string>
            {
                ["Morse code"] = "モールス信号",
                ["tap code"] = "タップ・コード",
                ["Braille"] = "点字",
            },
        },

        // Stroop’s Test
        // What was the {1} submitted word in {0}?
        // What was the first submitted word in Stroop’s Test?
        [Question.StroopsTestWord] = new()
        {
            QuestionText = "{0}で{1}番目に送信した単語は？",
            ModuleName = "ストループ検査",
        },
        // What was the {1} submitted word’s color in {0}?
        // What was the first submitted word’s color in Stroop’s Test?
        [Question.StroopsTestColor] = new()
        {
            QuestionText = "{0}で{1}番目に送信した単語の色は？",
            ModuleName = "ストループ検査",
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

        // Stupid Slots
        // What was the value of the {1} arrow in {0}?
        // What was the value of the top-left arrow in Stupid Slots?
        [Question.StupidSlotsValues] = new()
        {
            QuestionText = "{0}の{1}にある矢印の値は？",
            ModuleName = "馬鹿スロット",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-middle"] = "上",
                ["top-right"] = "右上",
                ["bottom-left"] = "左下",
                ["bottom-middle"] = "下",
                ["bottom-right"] = "右下",
            },
        },

        // Subbly Jubbly
        // What was a substitution word in {0}?
        // What was a substitution word in Subbly Jubbly?
        [Question.SubblyJubblySubstitutions] = new()
        {
            QuestionText = "{0}の代替語は？",
        },

        // Subscribe to Pewdiepie
        // How many subscribers does {1} have in {0}?
        // How many subscribers does PewDiePie have in Subscribe to Pewdiepie?
        [Question.SubscribeToPewdiepieSubCount] = new()
        {
            QuestionText = "{0}における{1}の登録者の数は？",
            ModuleName = "ピューディパイの登録",
            FormatArgs = new Dictionary<string, string>
            {
                ["PewDiePie"] = "ピューディパイ",
                ["T-Series"] = "Tシリーズ",
            },
        },

        // Subway
        // Which bread did the customer ask for in {0}?
        // Which bread did the customer ask for in Subway?
        [Question.SubwayBread] = new()
        {
            QuestionText = "{0}で客が注文したパンは？",
            ModuleName = "SUBWAY",
        },
        // Which of these was not asked for in {0}?
        // Which of these was not asked for in Subway?
        [Question.SubwayItems] = new()
        {
            QuestionText = "{0}で客が頼まなかったものは？",
            ModuleName = "SUBWAY",
        },

        // Sugar Skulls
        // What skull was shown on the {1} square in {0}?
        // What skull was shown on the top square in Sugar Skulls?
        [Question.SugarSkullsSkull] = new()
        {
            QuestionText = "{0}にて{1}の位置に表示された骸骨は？",
            ModuleName = "シュガースカル",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
        },
        // Which skull {1} present in {0}?
        // Which skull was present in Sugar Skulls?
        [Question.SugarSkullsAvailability] = new()
        {
            QuestionText = "{0}に表示されて{1}骸骨は？",
            ModuleName = "シュガースカル",
            FormatArgs = new Dictionary<string, string>
            {
                ["was"] = "いた",
                ["was not"] = "いなかった",
            },
        },

        // Suits And Colours
        // What was the colour of this cell in {0}?
        // What was the colour of this cell in Suits And Colours?
        [Question.SuitsAndColourColour] = new()
        {
            QuestionText = "{0}のこのセルの色は？",
            ModuleName = "スートと色",
        },
        // What was the suit of this cell in {0}?
        // What was the suit of this cell in Suits And Colours?
        [Question.SuitsAndColourSuit] = new()
        {
            QuestionText = "{0}のこのセルにあったスートは？",
            ModuleName = "スートと色",
        },

        // Superparsing
        // What was the displayed word in {0}?
        // What was the displayed word in Superparsing?
        [Question.SuperparsingDisplayed] = new()
        {
            QuestionText = "{0}で表示された単語は？",
            ModuleName = "超解析",
        },

        // SUSadmin
        // Which security protocol was installed in {0}?
        // Which security protocol was installed in SUSadmin?
        [Question.SUSadminSecurity] = new()
        {
            QuestionText = "{0}にインストールされたセキュリティプロトコルは？",
            ModuleName = "システム侵入者",
        },

        // The Switch
        // What color was the {1} LED on the {2} flip of {0}?
        // What color was the top LED on the first flip of The Switch?
        [Question.SwitchInitialColor] = new()
        {
            QuestionText = "{0}の{2}回目の切り替え時の{1}部のLEDの色は？",
            ModuleName = "ザ・スイッチ",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["bottom"] = "下",
            },
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

        // Switches
        // What was the initial position of the switches in {0}?
        // What was the initial position of the switches in Switches?
        [Question.SwitchesInitialPosition] = new()
        {
            QuestionText = "{0}の最初の状態は？",
            ModuleName = "スイッチ",
        },

        // Switching Maze
        // What was the seed in {0}?
        // What was the seed in Switching Maze?
        [Question.SwitchingMazeSeed] = new()
        {
            QuestionText = "{0}のシード値は？",
            ModuleName = "切り替え迷路",
        },
        // What was the starting maze color in {0}?
        // What was the starting maze color in Switching Maze?
        [Question.SwitchingMazeColor] = new()
        {
            QuestionText = "{0}の開始迷路の色は？",
            ModuleName = "切り替え迷路",
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
        [Question.SymbolCycleSymbolCounts] = new()
        {
            QuestionText = "{0}にて{1}側のディスプレーに表示されたシンボルの数は？",
            ModuleName = "シンボルサイクル",
            FormatArgs = new Dictionary<string, string>
            {
                ["left"] = "左",
                ["right"] = "右",
            },
        },

        // Symbolic Coordinates
        // What was the {1} symbol in the {2} stage of {0}?
        // What was the left symbol in the first stage of Symbolic Coordinates?
        [Question.SymbolicCoordinateSymbols] = new()
        {
            QuestionText = "{0}のステージ{2}における{1}のシンボルは？",
            ModuleName = "シンボル座標",
            FormatArgs = new Dictionary<string, string>
            {
                ["left"] = "左",
                ["middle"] = "中央",
                ["right"] = "右",
            },
        },

        // Symbolic Tasha
        // Which button flashed {1} in the final sequence of {0}?
        // Which button flashed first in the final sequence of Symbolic Tasha?
        [Question.SymbolicTashaFlashes] = new()
        {
            QuestionText = "{0}の最後のシークエンスで{1}番目に点滅したものは？",
            ModuleName = "シンボルターシャ",
            Answers = new Dictionary<string, string>
            {
                ["Top"] = "上",
                ["Right"] = "右",
                ["Bottom"] = "下",
                ["Left"] = "左",
                ["Pink"] = "ピンク",
                ["Green"] = "緑",
                ["Yellow"] = "黄",
                ["Blue"] = "青",
            },
        },
        // Which symbol was on the {1} button in {0}?
        // Which symbol was on the top button in Symbolic Tasha?
        [Question.SymbolicTashaSymbols] = new()
        {
            QuestionText = "{0}の{1}の位置のシンボルは？",
            ModuleName = "シンボルターシャ",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["right"] = "右",
                ["bottom"] = "下",
                ["left"] = "左",
                ["blue"] = "青",
                ["green"] = "緑",
                ["yellow"] = "黄色",
                ["pink"] = "ピンク",
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
            QuestionText = "{0}にてステージ{1}でスクリーンに表示されたものは？",
        },

        // Synonyms
        // Which number was displayed on {0}?
        // Which number was displayed on Synonyms?
        [Question.SynonymsNumber] = new()
        {
            QuestionText = "{0}のディスプレーの数字は？",
            ModuleName = "同義語",
        },

        // Sysadmin
        // What error code did you fix in {0}?
        // What error code did you fix in Sysadmin?
        [Question.SysadminFixedErrorCodes] = new()
        {
            QuestionText = "{0}で修正したエラーコードは？",
            ModuleName = "システム管理者",
        },

        // TAC
        // Which card was {1} in the swap in {0}?
        // Which card was given away in the swap in TAC?
        [Question.TACSwappedCard] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which card was {1} your partner in {0}?",
            FormatArgs = new Dictionary<string, string>
            {
                ["given away"] = "given away",
                ["received"] = "received",
            },
            Answers = new Dictionary<string, string>
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
        // Which card was in your hand in {0}?
        // Which card was in your hand in TAC?
        [Question.TACHeldCard] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which card was in your hand in {0}?",
            Answers = new Dictionary<string, string>
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

        // Tap Code
        // What was the received word in {0}?
        // What was the received word in Tap Code?
        [Question.TapCodeReceivedWord] = new()
        {
            QuestionText = "{0}で受信した単語は？",
            ModuleName = "タップ・コード",
        },

        // Tasha Squeals
        // What was the {1} flashed color in {0}?
        // What was the first flashed color in Tasha Squeals?
        [Question.TashaSquealsColors] = new()
        {
            QuestionText = "{0}で{1}番目に点滅した色は？",
            ModuleName = "ターシャの悲鳴",
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
        [Question.TasqueManagingStartingPos] = new()
        {
            QuestionText = "{0}の開始位置は？",
            ModuleName = "タスク管理",
        },

        // The Tea Set
        // Which ingredient was displayed {1}, from left to right, in {0}?
        // Which ingredient was displayed first, from left to right, in The Tea Set?
        [Question.TeaSetDisplayedIngredients] = new()
        {
            QuestionText = "{0}で左から{1}番目に表示された材料は？",
        },

        // Technical Keypad
        // What was the {1} displayed digit in {0}?
        // What was the first displayed digit in Technical Keypad?
        // Note: This question is depicted visually, rather than with words. A translation here will only be used for logging.
        [Question.TechnicalKeypadDisplayedDigits] = new()
        {
            QuestionText = "{0}で{1}番目に表示された数字は？",
            ModuleName = "テクニックキーパッド",
        },

        // Ten-Button Color Code
        // What was the initial color of the {1} button in the {2} stage of {0}?
        // What was the initial color of the first button in the first stage of Ten-Button Color Code?
        [Question.TenButtonColorCodeInitialColors] = new()
        {
            NeedsTranslation = true,
            QuestionText = "{0}のステージ{2}における{1}番目のボタンの初期の色は？",
            ModuleName = "10ボタン色コード",
            Answers = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
            },
        },

        // Tenpins
        // What was the {1} split in {0}?
        // What was the red split in Tenpins?
        [Question.TenpinsSplits] = new()
        {
            QuestionText = "{0}の{1}のスプリットは？",
            ModuleName = "テンピン",
            FormatArgs = new Dictionary<string, string>
            {
                ["red"] = "赤",
                ["green"] = "緑",
                ["blue"] = "青",
            },
            Answers = new Dictionary<string, string>
            {
                ["Goal Posts"] = "ゴールポスト",
                ["Cincinnati"] = "シンシナティ",
                ["Woolworth Store"] = "ダイムストア",
                ["Lily"] = "リリー",
                ["3-7 Split"] = "3-7 スプリット",
                ["Cocked Hat"] = "コックト・ハット",
                ["4-7-10 Split"] = "4-7-10 スプリット",
                ["Big Four"] = "ビッグフォー",
                ["Greek Church"] = "ギリシャ教会",
                ["Big Five"] = "ビッグファイブ",
                ["Big Six"] = "ビッグシックス",
                ["HOW"] = "HOW",
            },
        },

        // Tetriamonds
        // What colour triangle pulsed {1} in {0}?
        // What colour triangle pulsed first in Tetriamonds?
        [Question.TetriamondsPulsingColours] = new()
        {
            QuestionText = "{0}で{1}番目に動いた三角形の色は？",
            Answers = new Dictionary<string, string>
            {
                ["orange"] = "オレンジ",
                ["lime"] = "黄緑",
                ["jade"] = "翡翠",
                ["azure"] = "空",
                ["violet"] = "紫",
                ["rose"] = "薔薇",
                ["grey"] = "灰",
            },
        },

        // Text Field
        // What was the displayed letter in {0}?
        // What was the displayed letter in Text Field?
        [Question.TextFieldDisplay] = new()
        {
            QuestionText = "{0}で表示された文字は？",
            ModuleName = "テキストフィールド",
        },

        // Thinking Wires
        // What was the position from top to bottom of the first wire needing to be cut in {0}?
        // What was the position from top to bottom of the first wire needing to be cut in Thinking Wires?
        [Question.ThinkingWiresFirstWire] = new()
        {
            QuestionText = "{0}において最初に切る必要のあるワイヤの位置(上から下)は？",
            ModuleName = "思考ワイヤ",
        },
        // What color did the second valid wire to cut have to have in {0}?
        // What color did the second valid wire to cut have to have in Thinking Wires?
        [Question.ThinkingWiresSecondWire] = new()
        {
            QuestionText = "{0}において2番目に切った有効なワイヤの色は？",
            ModuleName = "思考ワイヤ",
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
                ["Any"] = "任意",
            },
        },
        // What was the display number in {0}?
        // What was the display number in Thinking Wires?
        [Question.ThinkingWiresDisplayNumber] = new()
        {
            QuestionText = "{0}のディスプレーの数字は？",
            ModuleName = "思考ワイヤ",
        },

        // Third Base
        // What was the display word in the {1} stage on {0}?
        // What was the display word in the first stage on Third Base?
        [Question.ThirdBaseDisplay] = new()
        {
            QuestionText = "{0}にてステージ{1}で表示された単語は？",
            ModuleName = "サードベース",
        },

        // Thirty Dollar Module
        // Which sound was used in {0}?
        // Which sound was used in Thirty Dollar Module?
        [Question.ThirtyDollarModuleSounds] = new()
        {
            QuestionText = "{0}で使用された音は？",
        },

        // Tic Tac Toe
        // What was on the {1} button at the start of {0}?
        // What was on the top-left button at the start of Tic Tac Toe?
        [Question.TicTacToeInitialState] = new()
        {
            QuestionText = "{0}の{1}のボタンの初期状態は？",
            ModuleName = "○×ゲーム",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-middle"] = "上",
                ["top-right"] = "右上",
                ["middle-left"] = "左",
                ["middle-center"] = "中央",
                ["middle-right"] = "右",
                ["bottom-left"] = "左下",
                ["bottom-middle"] = "下",
                ["bottom-right"] = "右下",
            },
        },

        // Time Signatures
        // What was the {1} time signature in {0}?
        // What was the first time signature in Time Signatures?
        [Question.TimeSignaturesSignatures] = new()
        {
            QuestionText = "{0}の{1}番目の拍子記号は？",
            ModuleName = "拍子記号",
        },

        // Timezone
        // What was the {1} city in {0}?
        // What was the departure city in Timezone?
        [Question.TimezoneCities] = new()
        {
            QuestionText = "{0}の{1}都市は？",
            ModuleName = "タイムゾーン",
            FormatArgs = new Dictionary<string, string>
            {
                ["departure"] = "上の",
                ["destination"] = "下の",
            },
        },

        // Tip Toe
        // Which of these squares was safe in row {1} in {0}?
        // Which of these squares was safe in row 9 in Tip Toe?
        [Question.TipToeSafeSquares] = new()
        {
            QuestionText = "{0}の段{1}で安全だったマスは？",
        },

        // Topsy Turvy
        // What was the word initially shown in {0}?
        // What was the word initially shown in Topsy Turvy?
        [Question.TopsyTurvyWord] = new()
        {
            QuestionText = "{0}で最初に表示された単語は？",
            ModuleName = "トプシー・タービー",
        },

        // Touch Transmission
        // What was the transmitted word in {0}?
        // What was the transmitted word in Touch Transmission?
        [Question.TouchTransmissionWord] = new()
        {
            QuestionText = "{0}で送信した単語は？",
            ModuleName = "接触送信",
        },
        // In what order was the Braille read in {0}?
        // In what order was the Braille read in Touch Transmission?
        [Question.TouchTransmissionOrder] = new()
        {
            QuestionText = "{0}では点字をどのような順序で読んだ？",
            ModuleName = "接触送信",
            Answers = new Dictionary<string, string>
            {
                ["Standard Braille Order"] = "標準",
                ["Individual Reading Order"] = "個別読み順",
                ["Merged Reading Order"] = "全体読み順",
                ["Chinese Reading Order"] = "漢字読み順",
            },
        },

        // Transmitted Morse
        // What was the {1} received message in {0}?
        // What was the first received message in Transmitted Morse?
        [Question.TransmittedMorseMessage] = new()
        {
            QuestionText = "{0}にて{1}番目に受け取ったメッセージは？",
            ModuleName = "送信モールス",
        },

        // Triamonds
        // What colour triangle pulsed {1} in {0}?
        // What colour triangle pulsed first in Triamonds?
        [Question.TriamondsPulsingColours] = new()
        {
            QuestionText = "{0}で{1}番目に動いた三角形の色は？",
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

        // Tribal Council
        // What was the {1} name in {0}?
        // What was the northeast name in Tribal Council?
        [Question.TribalCouncilName] = new()
        {
            QuestionText = "{0}の{1}の名前は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["northeast"] = "北東",
                ["southwest"] = "南西",
            },
        },

        // Triple Term
        // Which of these was one of the passwords in {0}?
        // Which of these was one of the passwords in Triple Term?
        [Question.TripleTermPasswords] = new()
        {
            QuestionText = "{0}のパスワードの一つであるのは？",
            ModuleName = "三単語",
        },

        // Turtle Robot
        // What was the {1} line you commented out in {0}?
        // What was the first line you commented out in Turtle Robot?
        [Question.TurtleRobotCodeLines] = new()
        {
            QuestionText = "{0}で{1}番目にコメントアウトした行は？",
            ModuleName = "亀型ロボット",
        },

        // Two Bits
        // What was the {1} correct query response from {0}?
        // What was the first correct query response from Two Bits?
        [Question.TwoBitsResponse] = new()
        {
            QuestionText = "{0}で{1}番目のクエリの返答は？",
            ModuleName = "ツービッツ",
        },

        // Ultimate Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Ultimate Cipher?
        [Question.UltimateCipherScreen] = new()
        {
            QuestionText = "{0}の回答は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Ultimate Cycle
        // Which direction was the {1} dial pointing in {0}?
        // Which direction was the first dial pointing in Ultimate Cycle?
        [Question.UltimateCycleDialDirections] = new()
        {
            NeedsTranslation = true,
            QuestionText = "Which direction was the {1} dial pointing in {0}?",
            ModuleName = "究極サイクル",
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
            QuestionText = "{0}の{1}番目の回転方向は？",
            ModuleName = "極立方体",
        },

        // UltraStores
        // What was the {1} rotation in the {2} stage of {0}?
        // What was the first rotation in the first stage of UltraStores?
        [Question.UltraStoresSingleRotation] = new()
        {
            QuestionText = "{0}のステージ{2}における{1}番目の回転方向は？",
            ModuleName = "極貯留",
        },
        // What was the {1} rotation in the {2} stage of {0}?
        // What was the first rotation in the first stage of UltraStores?
        [Question.UltraStoresMultiRotation] = new()
        {
            QuestionText = "{0}のステージ{2}における{1}番目の回転方向は？",
            ModuleName = "極貯留",
        },

        // Uncolored Squares
        // What was the {1} color in reading order used in the first stage of {0}?
        // What was the first color in reading order used in the first stage of Uncolored Squares?
        [Question.UncoloredSquaresFirstStage] = new()
        {
            QuestionText = "{0}の最初のステージで利用したもののうち読み順で{1}番目の色は何色？",
            ModuleName = "色無し格子",
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
        [Question.UncoloredSwitchesInitialState] = new()
        {
            QuestionText = "{0}の最初のスイッチの状態は？",
            ModuleName = "色無しスイッチ",
        },
        // What color was the {1} LED in reading order in {0}?
        // What color was the first LED in reading order in Uncolored Switches?
        [Question.UncoloredSwitchesLedColors] = new()
        {
            QuestionText = "{0}の読み順で{1}番目のLEDは何色？",
            ModuleName = "色無しスイッチ",
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
            QuestionText = "{0}で{1}番目に受け取った指示は？",
            ModuleName = "アンフェア暗号",
        },

        // Unfair’s Revenge
        // What was the {1} decrypted instruction in {0}?
        // What was the first decrypted instruction in Unfair’s Revenge?
        [Question.UnfairsRevengeInstructions] = new()
        {
            QuestionText = "{0}で{1}番目に解読した指示は？",
            ModuleName = "アンフェアの逆襲",
        },

        // Unicode
        // What was the {1} submitted code in {0}?
        // What was the first submitted code in Unicode?
        [Question.UnicodeSortedAnswer] = new()
        {
            QuestionText = "{0}にて{1}番目に送信したコードは？",
            ModuleName = "ユニコード",
        },

        // UNO!
        // What was the initial card in {0}?
        // What was the initial card in UNO!?
        [Question.UnoInitialCard] = new()
        {
            QuestionText = "{0}の初期カードは？",
            ModuleName = "UNO",
            Answers = new Dictionary<string, string>
            {
                ["Red 0"] = "赤の0",
                ["Red 1"] = "赤の1",
                ["Red 2"] = "赤の2",
                ["Red 3"] = "赤の3",
                ["Red 4"] = "赤の4",
                ["Red 5"] = "赤の5",
                ["Red 6"] = "赤の6",
                ["Red 7"] = "赤の7",
                ["Red 8"] = "赤の8",
                ["Red 9"] = "赤の9",
                ["Red +2"] = "赤の+2",
                ["Red Skip"] = "赤のスキップ",
                ["Red Reverse"] = "赤のリバース",
                ["Green 0"] = "緑の0",
                ["Green 1"] = "緑の1",
                ["Green 2"] = "緑の2",
                ["Green 3"] = "緑の3",
                ["Green 4"] = "緑の4",
                ["Green 5"] = "緑の5",
                ["Green 6"] = "緑の6",
                ["Green 7"] = "緑の7",
                ["Green 8"] = "緑の8",
                ["Green 9"] = "緑の9",
                ["Green +2"] = "緑の+2",
                ["Green Skip"] = "緑のスキップ",
                ["Green Reverse"] = "緑のリバース",
                ["Yellow 0"] = "黄の0",
                ["Yellow 1"] = "黄の1",
                ["Yellow 2"] = "黄の2",
                ["Yellow 3"] = "黄の3",
                ["Yellow 4"] = "黄の4",
                ["Yellow 5"] = "黄の5",
                ["Yellow 6"] = "黄の6",
                ["Yellow 7"] = "黄の7",
                ["Yellow 8"] = "黄の8",
                ["Yellow 9"] = "黄の9",
                ["Yellow +2"] = "黄の+2",
                ["Yellow Skip"] = "黄のスキップ",
                ["Yellow Reverse"] = "黄のリバース",
                ["Blue 0"] = "青の0",
                ["Blue 1"] = "青の1",
                ["Blue 2"] = "青の2",
                ["Blue 3"] = "青の3",
                ["Blue 4"] = "青の4",
                ["Blue 5"] = "青の5",
                ["Blue 6"] = "青の6",
                ["Blue 7"] = "青の7",
                ["Blue 8"] = "青の8",
                ["Blue 9"] = "青の9",
                ["Blue +2"] = "青の+2",
                ["Blue Skip"] = "青のスキップ",
                ["Blue Reverse"] = "青のリバース",
                ["+4"] = "+4",
                ["Wild"] = "ワイルド",
            },
        },

        // Unordered Keys
        // What color was this key in the {1} stage of {0}?
        // What color was this key in the first stage of Unordered Keys?
        [Question.UnorderedKeysKeyColor] = new()
        {
            QuestionText = "{0}のステージ{1}におけるこの音板の色は？",
            ModuleName = "順番無し音板",
        },
        // What color was the label of this key in the {1} stage of {0}?
        // What color was the label of this key in the first stage of Unordered Keys?
        [Question.UnorderedKeysLabelColor] = new()
        {
            QuestionText = "{0}のステージ{1}におけるこの音板のラベルの色は？",
            ModuleName = "順番無し音板",
        },
        // What was the label of this key in the {1} stage of {0}?
        // What was the label of this key in the first stage of Unordered Keys?
        [Question.UnorderedKeysLabel] = new()
        {
            QuestionText = "{0}のステージ{1}におけるこの音板のラベルは？",
            ModuleName = "順番無し音板",
        },

        // Unown Cipher
        // What was the {1} submitted letter in {0}?
        // What was the first submitted letter in Unown Cipher?
        [Question.UnownCipherAnswers] = new()
        {
            QuestionText = "{0}にて送信した単語の{1}番目の英字は？",
            ModuleName = "アンノーン暗号",
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
            QuestionText = "{0}のテキストは？",
            ModuleName = "うざいイヌ",
        },
        // What was the {1} color in the sequence on {0}?
        // What was the first color in the sequence on Updog?
        [Question.UpdogColor] = new()
        {
            QuestionText = "{0}のシーケンスにおける{1}の色は？",
            ModuleName = "うざいイヌ",
            FormatArgs = new Dictionary<string, string>
            {
                ["first"] = "最初",
                ["last"] = "最後",
            },
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Yellow"] = "黄",
                ["Orange"] = "橙",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Purple"] = "紫",
            },
        },

        // USA Cycle
        // Which state was displayed in {0}?
        // Which state was displayed in USA Cycle?
        [Question.USACycleDisplayed] = new()
        {
            QuestionText = "{0}で表示された州は？",
            ModuleName = "USAサイクル",
        },

        // USA Maze
        // Which state did you depart from in {0}?
        // Which state did you depart from in USA Maze?
        [Question.USAMazeOrigin] = new()
        {
            QuestionText = "{0}の開始地点は？",
            ModuleName = "USA迷路",
        },

        // V
        // Which word {1} shown in {0}?
        // Which word was shown in V?
        [Question.VWords] = new()
        {
            QuestionText = "{0}で表示{1}単語は？",
            FormatArgs = new Dictionary<string, string>
            {
                ["was"] = "された",
                ["was not"] = "されなかった",
            },
        },

        // Valves
        // What was the initial state of {0}?
        // What was the initial state of Valves?
        [Question.ValvesInitialState] = new()
        {
            QuestionText = "{0}の初期状態は？",
            ModuleName = "バルブ",
        },

        // Varicolored Squares
        // What was the initially pressed color on {0}?
        // What was the initially pressed color on Varicolored Squares?
        [Question.VaricoloredSquaresInitialColor] = new()
        {
            QuestionText = "{0}で最初に押した色は？",
            ModuleName = "色どり格子",
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
        [Question.VaricolourFlashWords] = new()
        {
            QuestionText = "{0}の{1}番目のゴールの単語は？",
            ModuleName = "バリカラーフラッシュ",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
                ["White"] = "白",
            },
        },
        // What was the color of the {1} goal in {0}?
        // What was the color of the first goal in Varicolour Flash?
        [Question.VaricolourFlashColors] = new()
        {
            QuestionText = "{0}の{1}番目のゴールの色は？",
            ModuleName = "バリカラーフラッシュ",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Green"] = "緑",
                ["Blue"] = "青",
                ["Magenta"] = "マゼンタ",
                ["Yellow"] = "黄",
                ["White"] = "白",
            },
        },

        // Variety
        // What color was the LED flashing in {0}?
        // What color was the LED flashing in Variety?
        [Question.VarietyLED] = new()
        {
            NeedsTranslation = true,
            QuestionText = "{0}で点滅したLEDの色は？",
            ModuleName = "寄せ集め",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Yellow"] = "黄",
                ["Blue"] = "青",
                ["White"] = "白",
                ["Black"] = "黒",
            },
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["the Variety that has one"] = "the Variety that has one",
                ["the Variety that has one (LED)"] = "the Variety that has one (LED)",
                ["the Variety that has one (digit display)"] = "the Variety that has one (digit display)",
                ["the Variety that has one (letter display)"] = "the Variety that has one (letter display)",
                ["the Variety that has one (timer)"] = "the Variety that has one (timer)",
                ["the Variety that has one (ascendingtimer)"] = "the Variety that has one (ascendingtimer)",
                ["the Variety that has one (descendingtimer)"] = "the Variety that has one (descendingtimer)",
                ["the Variety that has one (knob)"] = "the Variety that has one (knob)",
                ["the Variety that has one (coloredknob)"] = "the Variety that has one (coloredknob)",
                ["the Variety that has one (redknob)"] = "the Variety that has one (redknob)",
                ["the Variety that has one (yellowknob)"] = "the Variety that has one (yellowknob)",
                ["the Variety that has one (blueknob)"] = "the Variety that has one (blueknob)",
                ["the Variety that has one (blackknob)"] = "the Variety that has one (blackknob)",
                ["the Variety that has one (bulb)"] = "the Variety that has one (bulb)",
                ["the Variety that has one (redbulb)"] = "the Variety that has one (redbulb)",
                ["the Variety that has one (yellowbulb)"] = "the Variety that has one (yellowbulb)",
                ["the Variety that has {0}"] = "the Variety that has {0}",
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
        // What digit was displayed but not the answer for the digit display in {0}?
        // What digit was displayed but not the answer for the digit display in Variety?
        [Question.VarietyDigitDisplay] = new()
        {
            QuestionText = "{0}の数字ディスプレーに表示されたが回答ではないものは？",
            ModuleName = "寄せ集め",
        },
        // What word could be formed but was not the answer for the letter display in {0}?
        // What word could be formed but was not the answer for the letter display in Variety?
        [Question.VarietyLetterDisplay] = new()
        {
            QuestionText = "{0}の英字ディスプレーから形成できるが回答ではないものは？",
            ModuleName = "寄せ集め",
        },
        // What was the maximum display for the {1}timer in {0}?
        // What was the maximum display for the timer in Variety?
        [Question.VarietyTimer] = new()
        {
            QuestionText = "{0}の{1}タイマーが表示できた最大値は？",
            ModuleName = "寄せ集め",
            FormatArgs = new Dictionary<string, string>
            {
                [""] = "",
                ["ascending "] = "カウントアップ",
                ["descending "] = "カウントダウン",
            },
        },
        // What was n for the {1}knob in {0}?
        // What was n for the knob in Variety?
        [Question.VarietyColoredKnob] = new()
        {
            QuestionText = "{0}の{1}ダイヤルに対応するNは？",
            ModuleName = "寄せ集め",
            FormatArgs = new Dictionary<string, string>
            {
                [""] = "",
                ["colored "] = "色付き",
                ["red "] = "赤色",
                ["black "] = "黒色",
                ["blue "] = "青色",
                ["yellow "] = "黄色",
            },
        },
        // What was n for the {1}bulb in {0}?
        // What was n for the bulb in Variety?
        [Question.VarietyBulb] = new()
        {
            QuestionText = "{0}の{1}電球に対するNは？",
            ModuleName = "寄せ集め",
            FormatArgs = new Dictionary<string, string>
            {
                [""] = "",
                ["red "] = "赤色",
                ["yellow "] = "黄色",
            },
        },

        // Vcrcs
        // What was the word in {0}?
        // What was the word in Vcrcs?
        [Question.VcrcsWord] = new()
        {
            QuestionText = "{0}で表示された単語は？",
            ModuleName = "単語識別",
        },

        // Vectors
        // What was the color of the {1} vector in {0}?
        // What was the color of the first vector in Vectors?
        [Question.VectorsColors] = new()
        {
            QuestionText = "{0}にあった{1}のベクトルの色は？",
            ModuleName = "ベクトル",
            FormatArgs = new Dictionary<string, string>
            {
                ["first"] = "1番目",
                ["second"] = "2番目",
                ["third"] = "3番目",
                ["only"] = "唯一",
            },
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

        // Vexillology
        // What was the {1} flagpole color on {0}?
        // What was the first flagpole color on Vexillology?
        [Question.VexillologyColors] = new()
        {
            QuestionText = "{0}にてポールの色の{1}番目の色は？",
            ModuleName = "旗章学",
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
        [Question.VioletCipherScreen] = new()
        {
            QuestionText = "{0}の回答は？",
            ModuleName = "紫色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Visual Impairment
        // What was the desired color in the {1} stage on {0}?
        // What was the desired color in the first stage on Visual Impairment?
        [Question.VisualImpairmentColors] = new()
        {
            QuestionText = "{0}にてステージ{1}で押す必要のあった色は？",
            ModuleName = "視覚障害",
            Answers = new Dictionary<string, string>
            {
                ["Blue"] = "青",
                ["Green"] = "緑",
                ["Red"] = "赤",
                ["White"] = "白",
            },
        },

        // Walking Cube
        // Which of these cells was part of the cube’s path in {0}?
        // Which of these cells was part of the cube’s path in Walking Cube?
        [Question.WalkingCubePath] = new()
        {
            QuestionText = "{0}のキューブの通り道に含まれるセルは？",
        },

        // Warning Signs
        // What was the displayed sign in {0}?
        // What was the displayed sign in Warning Signs?
        [Question.WarningSignsDisplayedSign] = new()
        {
            QuestionText = "{0}で表示された標識は？",
            ModuleName = "警戒標識",
        },

        // WASD
        // What was the location displayed in {0}?
        // What was the location displayed in WASD?
        [Question.WasdDisplayedLocation] = new()
        {
            QuestionText = "{0}で表示された位置は？",
        },

        // Watching Paint Dry
        // How many brush strokes were heard in {0}?
        // How many brush strokes were heard in Watching Paint Dry?
        [Question.WatchingPaintDryStrokeCount] = new()
        {
            QuestionText = "{0}で聞こえたブラシのストローク数は？",
            ModuleName = "ペンキの乾燥観察",
        },

        // Wavetapping
        // What was the color on the {1} stage in {0}?
        // What was the color on the first stage in Wavetapping?
        [Question.WavetappingColors] = new()
        {
            QuestionText = "{0}のステージ{1}の色は？",
            ModuleName = "ウェーブタッピング",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Orange"] = "オレンジ",
                ["Orange-Yellow"] = "黄橙",
                ["Chartreuse"] = "黄",
                ["Lime"] = "黄緑",
                ["Green"] = "緑",
                ["Seafoam Green"] = "翡翠",
                ["Cyan-Green"] = "青銅",
                ["Turquoise"] = "シアン",
                ["Dark Blue"] = "青",
                ["Indigo"] = "藍",
                ["Purple"] = "紫",
                ["Purple-Magenta"] = "赤紫",
                ["Magenta"] = "マゼンタ",
                ["Pink"] = "ピンク",
                ["Gray"] = "灰",
            },
        },
        // What was the correct pattern on the {1} stage in {0}?
        // What was the correct pattern on the first stage in Wavetapping?
        [Question.WavetappingPatterns] = new()
        {
            QuestionText = "{0}にてステージ{1}の正しいパターンは？",
            ModuleName = "ウェーブタッピング",
        },

        // The Weakest Link
        // Who did you eliminate in {0}?
        // Who did you eliminate in The Weakest Link?
        [Question.WeakestLinkElimination] = new()
        {
            QuestionText = "{0}で退場させられたのは？",
        },
        // Who made it to the Money Phase with you in {0}?
        // Who made it to the Money Phase with you in The Weakest Link?
        [Question.WeakestLinkMoneyPhaseName] = new()
        {
            QuestionText = "{0}であなたと一緒にマネーフェイズに進出したのは？",
        },
        // What ratio did {1} get in the Question Phase in {0}?
        // What ratio did Annie get in the Question Phase in The Weakest Link?
        [Question.WeakestLinkRatio] = new()
        {
            QuestionText = "{0}の質問フェイズで{1}が獲得した倍率は？",
        },
        // What was {1}’s skill in {0}?
        // What was Annie’s skill in The Weakest Link?
        [Question.WeakestLinkSkill] = new()
        {
            QuestionText = "{0}における{1}のスキルは？",
        },

        // What’s on Second
        // What was the display text in the {1} stage of {0}?
        // What was the display text in the first stage of What’s on Second?
        [Question.WhatsOnSecondDisplayText] = new()
        {
            QuestionText = "{0}にてステージ{1}で表示されたテキストは？",
            ModuleName = "色付き表比較",
        },
        // What was the display text color in the {1} stage of {0}?
        // What was the display text color in the first stage of What’s on Second?
        [Question.WhatsOnSecondDisplayColor] = new()
        {
            QuestionText = "{0}にてステージ{1}で表示されたテキストの色は？",
            ModuleName = "色付き表比較",
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

        // White Arrows
        // What was the {1} non-white arrow in {0}?
        // What was the first non-white arrow in White Arrows?
        [Question.WhiteArrowsArrows] = new()
        {
            QuestionText = "{0}における{1}番目の白ではない矢印は？",
            ModuleName = "白色矢印",
            TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
            {
                ["Blue"] = "青",
                ["Red"] = "赤",
                ["Yellow"] = "黄",
                ["Green"] = "緑",
                ["Purple"] = "紫",
                ["Orange"] = "オレンジ",
                ["Cyan"] = "シアン",
                ["Teal"] = "青緑",
                ["Up"] = "上",
                ["Right"] = "右",
                ["Down"] = "下",
                ["Left"] = "左",
                ["{0} {1}"] = "{0} {1}",
            },
        },

        // White Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in White Cipher?
        [Question.WhiteCipherScreen] = new()
        {
            QuestionText = "{0}の回答は？",
            ModuleName = "白色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // WhoOF
        // What was the display in the {1} stage on {0}?
        // What was the display in the first stage on WhoOF?
        [Question.WhoOFDisplay] = new()
        {
            QuestionText = "{0}にてステージ{1}で表示されたのは？",
            ModuleName = "表比",
        },

        // Who’s on First
        // What was the display in the {1} stage on {0}?
        // What was the display in the first stage on Who’s on First?
        [Question.WhosOnFirstDisplay] = new()
        {
            QuestionText = "{0}にてステージ{1}で表示されたのは？",
            ModuleName = "表比較",
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
            QuestionText = "{0}にてステージ{1}で送信された単語は？",
            ModuleName = "モールス比較",
        },

        // The Wire
        // What was the color of the {1} dial in {0}?
        // What was the color of the top dial in The Wire?
        [Question.WireDialColors] = new()
        {
            QuestionText = "{0}の{1}の位置にあったダイヤルの色は？",
            ModuleName = "ザ・ワイヤ",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
            Answers = new Dictionary<string, string>
            {
                ["blue"] = "青",
                ["green"] = "緑",
                ["grey"] = "灰",
                ["orange"] = "オレンジ",
                ["purple"] = "紫",
                ["red"] = "赤",
            },
        },
        // What was the displayed number in {0}?
        // What was the displayed number in The Wire?
        [Question.WireDisplayedNumber] = new()
        {
            QuestionText = "{0}で表示された数字は？",
            ModuleName = "ザ・ワイヤ",
        },

        // Wire Ordering
        // What color was the {1} display from the left in {0}?
        // What color was the first display from the left in Wire Ordering?
        [Question.WireOrderingDisplayColor] = new()
        {
            QuestionText = "{0}の左から{1}番目のディスプレーの色は？",
            ModuleName = "順序ワイヤ",
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
        [Question.WireOrderingDisplayNumber] = new()
        {
            QuestionText = "{0}の左から{1}番目のディスプレーの数字は？",
            ModuleName = "順序ワイヤ",
        },
        // What color was the {1} wire from the left in {0}?
        // What color was the first wire from the left in Wire Ordering?
        [Question.WireOrderingWireColor] = new()
        {
            QuestionText = "{0}の左から{1}番目のワイヤの色は？",
            ModuleName = "順序ワイヤ",
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
        [Question.WireSequenceColorCount] = new()
        {
            QuestionText = "{0}の{1}色のワイヤの総数は？",
            ModuleName = "順番ワイヤ",
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
        [Question.WolfGoatAndCabbageAnimals] = new()
        {
            QuestionText = "{0}に{1}のはどれ？",
            ModuleName = "川渡り問題",
            FormatArgs = new Dictionary<string, string>
            {
                ["present"] = "存在した",
                ["not present"] = "存在しなかった",
            },
        },
        // What was the boat size in {0}?
        // What was the boat size in Wolf, Goat, and Cabbage?
        [Question.WolfGoatAndCabbageBoatSize] = new()
        {
            QuestionText = "{0}のボートのサイズは？",
            ModuleName = "川渡り問題",
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
            QuestionText = "{0}にて表示されたラベルは？",
            ModuleName = "ワーキングタイトル",
        },

        // Wumbo
        // What was the number in {0}?
        // What was the number in Wumbo?
        [Question.WumboNumber] = new()
        {
            QuestionText = "{0}の数字は？",
        },

        // The Xenocryst
        // What was the color of the {1} flash in {0}?
        // What was the color of the first flash in The Xenocryst?
        [Question.Xenocryst] = new()
        {
            QuestionText = "{0}の{1}番目の点滅の色は？",
            ModuleName = "ゼノクリスト",
        },

        // XmORse Code
        // What was the {1} displayed letter (in reading order) in {0}?
        // What was the first displayed letter (in reading order) in XmORse Code?
        [Question.XmORseCodeDisplayedLetters] = new()
        {
            QuestionText = "{0}で表示された単語の{1}番目の位置(読み順)にある英字は？",
            ModuleName = "Xモールス信号",
        },
        // What word did you decrypt in {0}?
        // What word did you decrypt in XmORse Code?
        [Question.XmORseCodeWord] = new()
        {
            QuestionText = "{0}で解読した単語は？",
            ModuleName = "Xモールス信号",
        },

        // xobekuJ ehT
        // What song was played on {0}?
        // What song was played on xobekuJ ehT?
        [Question.XobekuJehTSong] = new()
        {
            QuestionText = "{0}で再生された曲は？",
            ModuleName = "スクッボクーュジ",
        },

        // X-Ring
        // Which symbol was scanned in {0}?
        // Which symbol was scanned in X-Ring?
        [Question.XRingSymbol] = new()
        {
            QuestionText = "{0}で読み取られたシンボルは？",
            ModuleName = "円形レントゲン",
        },

        // XY-Ray
        // Which shape was scanned by {0}?
        // Which shape was scanned by XY-Ray?
        [Question.XYRayShapes] = new()
        {
            QuestionText = "{0}で読み取った図形は？",
            ModuleName = "XYレントゲン",
        },

        // Yahtzee
        // What was the initial roll on {0}?
        // What was the initial roll on Yahtzee?
        [Question.YahtzeeInitialRoll] = new()
        {
            QuestionText = "{0}の最初のロール後の状態は？",
            ModuleName = "ヤッツィー",
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
        [Question.YellowArrowsStartingRow] = new()
        {
            QuestionText = "{0}の開始行の英字は？",
            ModuleName = "黄色矢印",
        },

        // The Yellow Button
        // What was the {1} color in {0}?
        // What was the first color in The Yellow Button?
        [Question.YellowButtonColors] = new()
        {
            QuestionText = "{0}の{1}番目の色は？",
            ModuleName = "黄色ボタン",
            Answers = new Dictionary<string, string>
            {
                ["Red"] = "赤",
                ["Yellow"] = "黄色",
                ["Green"] = ",緑",
                ["Cyan"] = "シアン",
                ["Blue"] = "青",
                ["Magenta"] = "マゼンタ",
            },
        },

        // Yellow Button’t
        // What was the filename in {0}?
        // What was the filename in Yellow Button’t?
        [Question.YellowButtontFilename] = new()
        {
            QuestionText = "{0}のファイル名は？",
            ModuleName = "偽黄色ボタン",
        },

        // Yellow Cipher
        // What was on the {1} screen on page {2} in {0}?
        // What was on the top screen on page 1 in Yellow Cipher?
        [Question.YellowCipherScreen] = new()
        {
            QuestionText = "{0}の回答は？",
            ModuleName = "黄色暗号",
            FormatArgs = new Dictionary<string, string>
            {
                ["top"] = "上部",
                ["middle"] = "中央",
                ["bottom"] = "下部",
            },
        },

        // Zero, Zero
        // What color was the {1} star in {0}?
        // What color was the top-left star in Zero, Zero?
        [Question.ZeroZeroStarColors] = new()
        {
            QuestionText = "{0}の{1}の位置の星の色は？",
            ModuleName = "ゼロ・ゼロ",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-right"] = "右上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
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
        [Question.ZeroZeroStarPoints] = new()
        {
            QuestionText = "{0}の{1}の位置の星の頂点数は？",
            ModuleName = "ゼロ・ゼロ",
            FormatArgs = new Dictionary<string, string>
            {
                ["top-left"] = "左上",
                ["top-right"] = "右上",
                ["bottom-left"] = "左下",
                ["bottom-right"] = "右下",
            },
        },
        // Where was the {1} square in {0}?
        // Where was the red square in Zero, Zero?
        [Question.ZeroZeroSquares] = new()
        {
            QuestionText = "{0}の{1}色の正方形の場所は？",
            ModuleName = "ゼロ・ゼロ",
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
        [Question.ZoniWords] = new()
        {
            QuestionText = "{0}で{1}番目に解読した単語は？",
            ModuleName = "ゾニ文字",
        },

        #endregion
    };

    public override string[] IntroTexts => Ut.NewArray(
            "あなたを詐欺罪と器物損壊罪で訴えます！理由はもちろんおわかりですね？",
            "この爆弾は、わしが育てた。",
            "そんな爆弾で大丈夫か？",
            "見た目は分析担当、頭脳は処理担当！",
            "爆弾はいいぞ。",
            "僕と契約して、爆弾処理班になってよ！",
            "ワイヤは青かった。",
            "このモジュールの戦闘力は53万です。",
            "バルス！",
            "あきらめたらそこで爆発だよ。",
            "解除の反対は爆発ではなく「やらないこと」だ。",
            "みんなも爆弾ゲットじゃぞ〜！",
            "クラムボムはかぷかぷ笑ったよ。",
            "爆弾はともだち！こわくないよ！",
            "ファイナルアンサー？",
            "爆発は春の季語じゃよ。",
            "頭の中に爆弾が！",
            "『なんで、赤を選んだ？』『……梅干しの色』",
            "目の前の爆弾解けんヤツに その先はないっちゅーことや!!",
            "僕は一生爆弾解除します。",
            "芸術は爆発だ！",
            "Nice bomb.",
            "我輩は爆弾である、名前はまだ無い。",
            "爆弾処理班はピンチなときほどふてぶてしく笑うものよ。",
            "よろしい、ならば爆弾だ。",
            "起爆スイッチを押させるなァーッ！",
            "爆弾に絶対はないが、そのモジュールには絶対がある。",
            "解けねぇ爆弾はただの爆弾だ。",
            "おまえのモジュールはおれのもの、おれのモジュールもおれのもの。",
            "爆弾解除、行きまーす！",
            "ボタン博士！お許しください！",
            "逃げちゃダメだ、逃げちゃダメだ、逃げちゃダメだ",
            "爆弾は命より重い…",
            "爆弾解除いつやるの？今でしょ！",
            "正しいことをやりましょう。歳をとっても新しい爆弾に挑戦してください。",
            "ヤツはとんでもない物を盗んでいきました。それはあなたのエッジワークです。",
            "解除王におれはなる！！！",
            "解除に不思議の解除あり。爆発に不思議の爆発なし。",
            "このバッテリーが目に入らぬか！",
            "切れよ…好きな色を切れ…",
            "僕は死にません！僕は死にません！爆弾が好きだから！",
            "爆弾かな？爆弾じゃないよ 爆弾だよ(爆弾)",
            "神は乗り越えられる爆弾しか与えない。",
            "神は言っている。ここで死ぬ定めではないと。",
            "鳴かぬなら 爆破してまえ ホトトギス",
            "爆弾に大きいも小さいもない。",
            "赤と青の線が仲良く並んでいる。",
            "難易度イージーが許されるのは小学生までだよねー？",
            "じゃあ、あれか…俺のケツの下に…爆弾があるのか？",
            "あっ、湾岸署の恩田です。至急爆発物処理班を回してください。",
            "爆弾解くのに、こんな面倒なことするのなんでだろう～？",
            "順番ワイヤと思ったら〜、パスワードでした〜。チクショー！",
            "お前はもう、死んでいる。",
            "『このモジュールがいいね』と君が言ったから十月九日は爆弾記念日。",
            "マニュアル読まずに爆弾触ると～爆発！ あたりまえ体操～",
            "おお 爆弾処理班よ！ しんでしまうとは なさけない…。",
            "ももたろ社長は 時限爆弾カードを 8枚 手に入れた！ 哀悼！",
            "安心してください、解けてますよ。",
            "もしかしてだけど～もしかしてだけど～今日の爆弾難しいんじゃないの～？",
            "今のままではいけない。だからこそKTaNEは今のままではいけないと思っている。",
            "ばくだんのちからってすげー！",
            "その点爆弾ってすげぇよな、最後までモジュールたっぷりだもん。",
            "もう、爆発してもいいよね…？",
            "爆弾の喜びを知りやがって！",
            "すっげえキモい爆弾だな！",
            "悪いなのび太、この爆弾は三人用なんだ。",
            "せっかくだから、俺はこの赤のワイヤを選ぶぜ！",
            "爆弾の法則が乱れる！",
            "おい、爆弾解除しろよ。",
            "なぜ笑うんだい？彼のマニュアル読みは上手だよ。",
            "爆弾じゃないよ。仮に爆弾だとしても、爆弾という名の紳士だよ。",
            "もう爆弾っていうレベルじゃねぇぞ！オイ！",
            "俺モジュールだけど何か質問ある？",
            "子供に「ワイヤ、ワイヤ」って、言いたくないですよね。",
            "楽しい仲間がぽぽぽぽ～ん！",
            "ば～くだ～ん、ば～くだ～ん、た～っぷ～り～、ば～くだ～ん",
            "玄関開けたら2分で爆弾。",
            "ぜんぶ爆弾のせいだ。",
            "爆弾を相手のゴールにシュゥゥゥーッ！超！エキサイティン！",
            "爆弾はリセットできないがモジュールはリセットできる。ソースは俺。",
            "おまえは今まで解いた爆弾の個数をおぼえているのか?",
            "『モジュール「ワイヤ」に存在する色の名前、5つ挙げてください』『走って！』",
            "やはり爆弾…‼爆弾はすべてを解決する…‼",
            "私のおじいさんがくれた、初めての爆弾。",
            "タイム連打も試してみたけど爆弾相手じゃ意味がない！",
            "選ばれたのは、爆弾でした。",
            "今野、そこにポートはあるんか？",
            "三分間待ってやる！",
            "ドナルドは今、爆弾解除に夢中なんだ。ほらね。自然に体が動いちゃうんだ。",
            "メロンです。爆弾です。",
            "おこしにつけたボムだんご ひとつ私に下さいな。",
            "ごめん、同級会には行けません。いま、爆弾のある密室にいます。",
            "爆弾と共にあらんことを。",
            "ば・く・は・つ Death‼",
            "NO BOMB, NO LIFE.",
            "まだ会ったことのない爆弾を、探している。",
            "あなたが落としたのは、この爆弾ですか？",
            "押すなよ！絶対に押すなよ！",
            "フジ、只今悩み中〜。僕は爆弾回して遊んでる。",
            "どうも、爆弾お兄さんでございますよ。",
            "この爆弾では常識に囚われてはいけないのですね！",
            "ようし！こうなったら……、「地球破壊爆弾」を！",
            "オレが爆弾？…違う…オレは悪魔だ！",
            "俺、この爆弾が終わったら結婚するんだ。",
            "一人で10モジュールぐらい解除すればいけるか…？",
            "つべこべ言わずに解きなさい！",
            "ありがとう。いい爆弾です。",
            "君は完璧で究極の爆弾！",
            "ば く だ ん のせいなのね そうなのね！"
    );
}
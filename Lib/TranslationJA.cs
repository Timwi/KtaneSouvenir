using System;
using System.Collections.Generic;

namespace Souvenir;

public class Translation_ja : TranslationBase<TranslationInfo<QuestionTranslationInfo>>
{
    public override string FormatModuleName(SouvenirQuestionAttribute qAttr, bool addSolveCount, int numSolved) => addSolveCount
        ? $"{Ordinal(numSolved)}番目に解除された{_translations.Get(qAttr.Handler.EnumType)?.ModuleName ?? qAttr.Handler.ModuleName}"
        : _translations.Get(qAttr.Handler.EnumType)?.ModuleName ?? qAttr.Handler.ModuleNameWithThe;

    public override string Ordinal(int number) => number.ToString();
    public override int DefaultFontIndex => 8;
    public override float LineSpacing => 0.7f;

    protected override Dictionary<Type, TranslationInfo<QuestionTranslationInfo>> _translations => new()
    {
        #region Translatable strings
        // 0
        [typeof(S0)] = new()
        {
            Questions = new()
            {
                [S0.Number] = new()
                {
                    // English: What was the initially displayed number in {0}?
                    Question = "{0}の初期状態の数字は？",
                },
            },
        },

        // 1000 Words
        [typeof(S1000Words)] = new()
        {
            ModuleName = "1000単語",
            Questions = new()
            {
                [S1000Words.Words] = new()
                {
                    // English: What was the {1} word shown in {0}?
                    // Example: What was the first word shown in 1000 Words?
                    Question = "{0}の{1}番目の単語は？",
                },
            },
            Discriminators = new()
            {
                [S1000Words.Discriminator] = new()
                {
                    // English: the 1000 Words where the {0} word was {1}
                    // Example: the 1000 Words where the first word was Baken
                    Discriminator = "{0}番目の単語が{1}だった1000単語",
                },
            },
        },

        // 100 Levels of Defusal
        [typeof(S100LevelsOfDefusal)] = new()
        {
            ModuleName = "解除レベル100",
            Questions = new()
            {
                [S100LevelsOfDefusal.Letters] = new()
                {
                    // English: What was the {1} displayed letter in {0}?
                    // Example: What was the first displayed letter in 100 Levels of Defusal?
                    Question = "{0}で{1}番目に表示された文字は？",
                },
            },
            Discriminators = new()
            {
                [S100LevelsOfDefusal.Discriminator] = new()
                {
                    // English: the 100 Levels of Defusal where the {0} displayed letter was {1}
                    // Example: the 100 Levels of Defusal where the first displayed letter was B
                    Discriminator = "表示された{0}番目の英字が{1}だった解除レベル100",
                },
            },
        },

        // The 1, 2, 3 Game
        [typeof(S123Game)] = new()
        {
            ModuleName = "123ゲーム",
            Questions = new()
            {
                [S123Game.Profile] = new()
                {
                    // English: Who was the opponent in {0}?
                    Question = "{0}の相手は？",
                },
                [S123Game.Name] = new()
                {
                    // English: Who was the opponent in {0}?
                    Question = "{0}の相手は？",
                },
            },
        },

        // 1D Chess
        [typeof(S1DChess)] = new()
        {
            ModuleName = "1Dチェス",
            Questions = new()
            {
                [S1DChess.Moves] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was your first move in 1D Chess?
                    Question = "{0}で{1}はどれだったか？",
                    Arguments = new()
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
            },
            Discriminators = new()
            {
                [S1DChess.Discriminator] = new()
                {
                    // English: the 1D Chess where {1} was {0}
                    // Example: the 1D Chess where your first move was B a→c
                    Discriminator = "{0}が{1}だった1Dチェス",
                    Arguments = new()
                    {
                        ["your first move"] = "あなたの最初の移動",
                        ["Rustmate’s first move"] = "Rustmateの最初の移動",
                        ["your second move"] = "あなたの2回目の移動",
                        ["Rustmate’s second move"] = "Rustmateの2回目の移動",
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
            },
        },

        // 21
        [typeof(S21)] = new()
        {
            Questions = new()
            {
                [S21.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "{0}の表示された数字は？",
                },
            },
        },

        // 3D Maze
        [typeof(S3DMaze)] = new()
        {
            ModuleName = "3D迷路",
            Questions = new()
            {
                [S3DMaze.QMarkings] = new()
                {
                    // English: What were the markings in {0}?
                    Question = "{0}の迷路の文字は？",
                },
                [S3DMaze.QBearing] = new()
                {
                    // English: What was the cardinal direction in {0}?
                    Question = "{0}のゴールの方向はどこ？",
                    Answers = new()
                    {
                        ["North"] = "北",
                        ["South"] = "南",
                        ["West"] = "西",
                        ["East"] = "東",
                    },
                },
            },
            Discriminators = new()
            {
                [S3DMaze.DMarkings] = new()
                {
                    // English: the 3D Maze whose markings were {0}
                    // Example: the 3D Maze whose markings were ABC
                    Discriminator = "目印が{0}だった3D迷路",
                },
                [S3DMaze.DBearing] = new()
                {
                    // English: the 3D Maze whose cardinal direction was {0}
                    // Example: the 3D Maze whose cardinal direction was North
                    Discriminator = "目的の方向が{0}だった3D迷路",
                    Arguments = new()
                    {
                        ["North"] = "北",
                        ["South"] = "南",
                        ["West"] = "西",
                        ["East"] = "東",
                    },
                },
            },
        },

        // 3D Tap Code
        [typeof(S3DTapCode)] = new()
        {
            ModuleName = "3Dタップ・コード",
            Questions = new()
            {
                [S3DTapCode.Word] = new()
                {
                    // English: What was the received word in {0}?
                    Question = "{0}で受信した単語は？",
                },
            },
        },

        // 3D Tunnels
        [typeof(S3DTunnels)] = new()
        {
            ModuleName = "3Dトンネル",
            Questions = new()
            {
                [S3DTunnels.TargetNode] = new()
                {
                    // English: What was the {1} goal node in {0}?
                    // Example: What was the first goal node in 3D Tunnels?
                    Question = "{0}の{1}番目のゴールの目印は？",
                },
            },
        },

        // 3 LEDs
        [typeof(S3LEDs)] = new()
        {
            ModuleName = "3つのLED",
            Questions = new()
            {
                [S3LEDs.InitialState] = new()
                {
                    // English: What was the initial state of the LEDs in {0} (in reading order)?
                    Question = "{0}の初期のLEDの状態は(読み順)？",
                    Answers = new()
                    {
                        ["off/off/off"] = "オフ/オフ/オフ",
                        ["off/off/on"] = "オフ/オフ/オン",
                        ["off/on/off"] = "オフ/オン/オフ",
                        ["off/on/on"] = "オフ/オン/オン",
                        ["on/off/off"] = "オン/オフ/オフ",
                        ["on/off/on"] = "オン/オフ/オン",
                        ["on/on/off"] = "オン/オン/オフ",
                        ["on/on/on"] = "オン/オン/オン",
                    },
                },
            },
        },

        // 3N+1
        [typeof(S3NPlus1)] = new()
        {
            Questions = new()
            {
                [S3NPlus1.Question] = new()
                {
                    // English: What number was initially displayed in {0}?
                    Question = "{0}の最初に表示された番号は？",
                },
            },
        },

        // 4D Tunnels
        [typeof(S4DTunnels)] = new()
        {
            ModuleName = "4Dトンネル",
            Questions = new()
            {
                [S4DTunnels.TargetNode] = new()
                {
                    // English: What was the {1} goal node in {0}?
                    // Example: What was the first goal node in 4D Tunnels?
                    Question = "{0}の{1}番目のゴールの目印は？",
                },
            },
        },

        // 64
        [typeof(S64)] = new()
        {
            Questions = new()
            {
                [S64.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "{0}のディスプレー上の数字は？",
                },
            },
        },

        // 7
        [typeof(S7)] = new()
        {
            Questions = new()
            {
                [S7.QInitialValues] = new()
                {
                    // English: What was the {1} channel’s initial value in {0}?
                    // Example: What was the red channel’s initial value in 7?
                    Question = "{0}の{1}チャンネルの初期値は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
                [S7.QLedColors] = new()
                {
                    // English: What LED color was shown in stage {1} of {0}?
                    // Example: What LED color was shown in stage 1 of 7?
                    Question = "{0}のステージ{1}のLEDの色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["green"] = "緑",
                        ["white"] = "白",
                    },
                },
            },
            Discriminators = new()
            {
                [S7.DInitialValues] = new()
                {
                    // English: the 7 whose {0} channel’s initial value was {1}
                    // Example: the 7 whose red channel’s initial value was -9
                    Discriminator = "{0}チャンネルの初期値が{1}だった7",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
                [S7.DLedColors] = new()
                {
                    // English: the 7 whose stage {0} LED color was {1}
                    // Example: the 7 whose stage 1 LED color was red
                    Discriminator = "ステージ{0}のLEDの色が{1}だった7",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["green"] = "緑",
                        ["white"] = "白",
                    },
                },
            },
        },

        // 9-Ball
        [typeof(S9Ball)] = new()
        {
            ModuleName = "9ボール",
            Questions = new()
            {
                [S9Ball.Letters] = new()
                {
                    // English: What was the number of ball {1} in {0}?
                    // Example: What was the number of ball A in 9-Ball?
                    Question = "{0}のボール{1}の数字は？",
                },
                [S9Ball.Numbers] = new()
                {
                    // English: What was the letter of ball {1} in {0}?
                    // Example: What was the letter of ball 2 in 9-Ball?
                    Question = "{0}のボール{1}の文字は？",
                },
            },
        },

        // Abyss
        [typeof(SAbyss)] = new()
        {
            ModuleName = "アビス",
            Questions = new()
            {
                [SAbyss.Seed] = new()
                {
                    // English: What was the {1} character displayed on {0}?
                    // Example: What was the first character displayed on Abyss?
                    Question = "{0}の{1}番目に表示された文字は？",
                },
            },
        },

        // Accumulation
        [typeof(SAccumulation)] = new()
        {
            ModuleName = "蓄積",
            Questions = new()
            {
                [SAccumulation.QBorderColor] = new()
                {
                    // English: What was the border color in {0}?
                    Question = "{0}の境界線の色は？",
                    Answers = new()
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
                [SAccumulation.QBackgroundColor] = new()
                {
                    // English: What was the background color in the {1} stage in {0}?
                    // Example: What was the background color in the first stage in Accumulation?
                    Question = "{0}のステージ{1}の背景の色は？",
                    Answers = new()
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
            },
            Discriminators = new()
            {
                [SAccumulation.DBorderColor] = new()
                {
                    // English: the Accumulation whose border was {0}
                    // Example: the Accumulation whose border was blue
                    Discriminator = "境界線の色が{0}だった蓄積",
                    Arguments = new()
                    {
                        ["blue"] = "青",
                        ["brown"] = "茶",
                        ["green"] = "緑",
                        ["grey"] = "灰",
                        ["lime"] = "黄緑",
                        ["orange"] = "オレンジ",
                        ["pink"] = "ピンク",
                        ["red"] = "赤",
                        ["white"] = "白",
                        ["yellow"] = "黄",
                    },
                },
                [SAccumulation.DBackgroundColor] = new()
                {
                    // English: the Accumulation whose background in the {1} stage was {0}
                    // Example: the Accumulation whose background in the first stage was blue
                    Discriminator = "ステージ{1}の背景色が{0}だった蓄積",
                    Arguments = new()
                    {
                        ["blue"] = "青",
                        ["brown"] = "茶",
                        ["green"] = "緑",
                        ["grey"] = "灰",
                        ["lime"] = "黄緑",
                        ["orange"] = "オレンジ",
                        ["pink"] = "ピンク",
                        ["red"] = "赤",
                        ["white"] = "白",
                        ["yellow"] = "黄",
                    },
                },
            },
        },

        // Adventure Game
        [typeof(SAdventureGame)] = new()
        {
            ModuleName = "アドベンチャーゲーム",
            Questions = new()
            {
                [SAdventureGame.QCorrectItem] = new()
                {
                    // English: Which item was the {1} correct item you used in {0}?
                    // Example: Which item was the first correct item you used in Adventure Game?
                    Question = "{0}で{1}番目に正しく使用したアイテムはどれ？",
                },
                [SAdventureGame.QEnemy] = new()
                {
                    // English: What enemy were you fighting in {0}?
                    Question = "{0}で戦った敵は？",
                },
            },
            Discriminators = new()
            {
                [SAdventureGame.DCorrectItem] = new()
                {
                    // English: the Adventure Game where the {0} was used
                    // Example: the Adventure Game where the Broadsword was used
                    Discriminator = "{0}が使用されたアドベンチャーゲーム",
                },
                [SAdventureGame.DEnemy] = new()
                {
                    // English: the Adventure Game where the enemy was {0}
                    // Example: the Adventure Game where the enemy was Dragon
                    Discriminator = "{0}が敵だったアドベンチャーゲーム",
                },
            },
        },

        // Affine Cycle
        [typeof(SAffineCycle)] = new()
        {
            ModuleName = "アフィンサイクル",
            Questions = new()
            {
                [SAffineCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Affine Cycle?
                    Question = "{0}のダイヤル{1}はどの方向を指していた？",
                },
                [SAffineCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Affine Cycle?
                    Question = "{0}のダイヤル{1}に書かれていた文字は？",
                },
            },
            Discriminators = new()
            {
                [SAffineCycle.LabelDiscriminator] = new()
                {
                    // English: the Affine Cycle that had the letter {0} on a dial
                    // Example: the Affine Cycle that had the letter A on a dial
                    Discriminator = "ダイヤルに{0}の文字があったAffine Cycle",
                },
            },
        },

        // Alcoholic Rampage
        [typeof(SAlcoholicRampage)] = new()
        {
            ModuleName = "アルコール中毒",
            Questions = new()
            {
                [SAlcoholicRampage.Mercenaries] = new()
                {
                    // English: Who was the {1} mercenary displayed in {0}?
                    // Example: Who was the first mercenary displayed in Alcoholic Rampage?
                    Question = "{0}で表示された{1}番目の傭兵は？",
                },
            },
        },

        // A Letter
        [typeof(SALetter)] = new()
        {
            ModuleName = "英字",
            Questions = new()
            {
                [SALetter.InitialLetter] = new()
                {
                    // English: What was the initial letter in {0}?
                    Question = "{0}の最初の英字は？",
                },
            },
        },

        // Alfa-Bravo
        [typeof(SAlfaBravo)] = new()
        {
            ModuleName = "アルファブラボー",
            Questions = new()
            {
                [SAlfaBravo.PressedLetter] = new()
                {
                    // English: Which letter was pressed in {0}?
                    Question = "{0}でどの文字を押した？",
                },
                [SAlfaBravo.LeftPressedLetter] = new()
                {
                    // English: Which letter was to the left of the pressed one in {0}?
                    Question = "{0}で押した文字の左にあった文字は？",
                },
                [SAlfaBravo.RightPressedLetter] = new()
                {
                    // English: Which letter was to the right of the pressed one in {0}?
                    Question = "{0}で押した文字の右にあった文字は？",
                },
                [SAlfaBravo.Digit] = new()
                {
                    // English: What was the last digit on the small display in {0}?
                    Question = "{0}の小さなディスプレーの最後の数字は？",
                },
            },
        },

        // Algebra
        [typeof(SAlgebra)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "方程式",
            Questions = new()
            {
                [SAlgebra.Equation1] = new()
                {
                    // English: What was the first equation in {0}?
                    Question = "{0}の最初の方程式は？",
                },
                [SAlgebra.Equation2] = new()
                {
                    // English: What was the second equation in {0}?
                    Question = "{0}の二番目の方程式は？",
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
            ModuleName = "アルゴリズム",
            Questions = new()
            {
                [SAlgorithmia.QPositions] = new()
                {
                    // English: Which position was the {1} position in {0}?
                    // Example: Which position was the starting position in Algorithmia?
                    Question = "{0}の{1}位置は？",
                    Arguments = new()
                    {
                        ["starting"] = "開始",
                        ["goal"] = "終了",
                    },
                },
                [SAlgorithmia.QColor] = new()
                {
                    // English: What was the color of the colored bulb in {0}?
                    Question = "{0}の色付き電球の色は？",
                },
                [SAlgorithmia.QSeed] = new()
                {
                    // English: Which number was present in the seed in {0}?
                    Question = "{0}のシード内にある数字は？",
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
            ModuleName = "アルファベットルール",
            Questions = new()
            {
                [SAlphabeticalRuling.Letter] = new()
                {
                    // English: What was the letter displayed in the {1} stage of {0}?
                    // Example: What was the letter displayed in the first stage of Alphabetical Ruling?
                    Question = "{0}のステージ{1}で表示された文字は？",
                },
                [SAlphabeticalRuling.Number] = new()
                {
                    // English: What was the number displayed in the {1} stage of {0}?
                    // Example: What was the number displayed in the first stage of Alphabetical Ruling?
                    Question = "{0}のステージ{1}で表示された数字は？",
                },
            },
        },

        // Alphabet Numbers
        [typeof(SAlphabetNumbers)] = new()
        {
            ModuleName = "アルファベット番号",
            Questions = new()
            {
                [SAlphabetNumbers.DisplayedNumbers] = new()
                {
                    // English: Which of these numbers was on one of the buttons in the {1} stage of {0}?
                    // Example: Which of these numbers was on one of the buttons in the first stage of Alphabet Numbers?
                    Question = "{0}のステージ{1}でボタン上にあった数字に含まれるのは？",
                },
            },
        },

        // Alphabet Tiles
        [typeof(SAlphabetTiles)] = new()
        {
            ModuleName = "アルファベットタイル",
            Questions = new()
            {
                [SAlphabetTiles.QCycle] = new()
                {
                    // English: What was the {1} letter shown during the cycle in {0}?
                    // Example: What was the first letter shown during the cycle in Alphabet Tiles?
                    Question = "{0}のサイクルで{1}番目に表示された文字は？",
                },
                [SAlphabetTiles.QMissingLetter] = new()
                {
                    // English: What was the missing letter in {0}?
                    Question = "{0}で隠されている文字は？",
                },
            },
            Discriminators = new()
            {
                [SAlphabetTiles.DCycle] = new()
                {
                    // English: the Alphabet Tiles where the {1} letter in the cycle was {0}
                    // Example: the Alphabet Tiles where the X letter in the cycle was first
                    Discriminator = "点滅内の{1}番目の文字が{0}だったアルファベットタイル",
                },
                [SAlphabetTiles.DMissingLetter] = new()
                {
                    // English: the Alphabet Tiles whose missing letter was {0}
                    // Example: the Alphabet Tiles whose missing letter was A
                    Discriminator = "隠されている文字が{0}だったアルファベットタイル",
                },
            },
        },

        // Alpha-Bits
        [typeof(SAlphaBits)] = new()
        {
            ModuleName = "アルファビッツ",
            Questions = new()
            {
                [SAlphaBits.DisplayedCharacters] = new()
                {
                    // English: What character was displayed on the {1} screen on the {2} in {0}?
                    // Example: What character was displayed on the first screen on the left in Alpha-Bits?
                    Question = "{0}で{2}の{1}つ目の画面に表示されている文字は？",
                    Arguments = new()
                    {
                        ["left"] = "左",
                        ["right"] = "右",
                    },
                },
            },
        },

        // A Message
        [typeof(SAMessage)] = new()
        {
            ModuleName = "Aメッセージ",
            Questions = new()
            {
                [SAMessage.AMessage] = new()
                {
                    // English: What was the initial message in {0}?
                    Question = "{0}の最初のメッセージは？",
                },
            },
        },

        // Amusement Parks
        [typeof(SAmusementParks)] = new()
        {
            ModuleName = "遊園地",
            Questions = new()
            {
                [SAmusementParks.Rides] = new()
                {
                    // English: Which ride was available in {0}?
                    Question = "{0}で利用可能だったアトラクションは？",
                },
            },
        },

        // Ángel Hernández
        [typeof(SAngelHernandez)] = new()
        {
            ModuleName = "アンヘル・エルナンデス",
            Questions = new()
            {
                [SAngelHernandez.MainLetter] = new()
                {
                    // English: What letter was shown by the raised buttons on the {1} stage on {0}?
                    // Example: What letter was shown by the raised buttons on the first stage on Ángel Hernández?
                    Question = "{0}のステージ{1}で、点字で表示されていた英字は？",
                },
            },
        },

        // The Arena
        [typeof(SArena)] = new()
        {
            ModuleName = "アリーナ",
            Questions = new()
            {
                [SArena.Damage] = new()
                {
                    // English: What was the maximum weapon damage of the attack phase in {0}?
                    Question = "{0}の攻撃フェーズにおける最大ダメージ数は？",
                },
                [SArena.Enemies] = new()
                {
                    // English: Which enemy was present in the defend phase of {0}?
                    Question = "{0}の防御フェーズで現れた敵は？",
                },
                [SArena.Numbers] = new()
                {
                    // English: Which was a number present in the grab phase of {0}?
                    Question = "{0}の獲得フェーズで現れた数字は？",
                },
            },
        },

        // Arithmelogic
        [typeof(SArithmelogic)] = new()
        {
            ModuleName = "計算論理",
            Questions = new()
            {
                [SArithmelogic.Submit] = new()
                {
                    // English: What was the symbol on the submit button in {0}?
                    Question = "{0}の送信ボタンの記号は？",
                },
                [SArithmelogic.Numbers] = new()
                {
                    // English: Which number was selectable, but not the solution, in the {1} screen on {0}?
                    // Example: Which number was selectable, but not the solution, in the left screen on Arithmelogic?
                    Question = "{0}の{1}の画面で選択できる、答え以外の数字はどれ？",
                    Arguments = new()
                    {
                        ["left"] = "左",
                        ["middle"] = "中央",
                        ["right"] = "右",
                    },
                },
            },
        },

        // ASCII Maze
        [typeof(SASCIIMaze)] = new()
        {
            ModuleName = "アスキー迷路",
            Questions = new()
            {
                [SASCIIMaze.Characters] = new()
                {
                    // English: What was the {1} character displayed on {0}?
                    // Example: What was the first character displayed on ASCII Maze?
                    Question = "{0}の{1}番目に表示された文字は？",
                },
            },
        },

        // A Square
        [typeof(SASquare)] = new()
        {
            ModuleName = "正方型",
            Questions = new()
            {
                [SASquare.IndexColors] = new()
                {
                    // English: Which of these was an index color in {0}?
                    Question = "{0}で一致した色に含まれるのは？",
                },
                [SASquare.CorrectColors] = new()
                {
                    // English: Which color was submitted {1} in {0}?
                    // Example: Which color was submitted first in A Square?
                    Question = "{0}で{1}番目に送信した色は？",
                },
            },
        },

        // Audio Morse
        [typeof(SAudioMorse)] = new()
        {
            ModuleName = "音声モールス",
            Questions = new()
            {
                [SAudioMorse.Sound] = new()
                {
                    // English: What was signaled in {0}?
                    Question = "{0}で送信されたものは？",
                },
            },
        },

        // The Azure Button
        [typeof(SAzureButton)] = new()
        {
            ModuleName = "空色ボタン",
            Questions = new()
            {
                [SAzureButton.QDecoyArrowDirection] = new()
                {
                    // English: What was the {1} direction in the decoy arrow in {0}?
                    // Example: What was the first direction in the decoy arrow in The Azure Button?
                    Question = "{0}の囮の矢印が{1}番目に示した方向は？",
                },
                [SAzureButton.QNonDecoyArrowDirection] = new()
                {
                    // English: What was the {1} direction in the {2} non-decoy arrow in {0}?
                    // Example: What was the first direction in the first non-decoy arrow in The Azure Button?
                    Question = "{0}の囮ではない{2}番目の矢印が{1}番目に示した方向は？",
                },
                [SAzureButton.QT] = new()
                {
                    // English: What was T in {0}?
                    Question = "{0}のTはどれ？",
                },
                [SAzureButton.QNotT] = new()
                {
                    // English: Which of these cards was shown in Stage 1, but not T, in {0}?
                    Question = "{0}のステージ1で表示されたT以外のカードに含まれるのは？",
                },
                [SAzureButton.QM] = new()
                {
                    // English: What was M in {0}?
                    Question = "{0}のMは？",
                },
            },
            Discriminators = new()
            {
                [SAzureButton.DCard] = new()
                {
                    // English: the Azure Button that had this card in Stage 1
                    Discriminator = "ステージ1でこのカードがあった空色ボタン",
                },
                [SAzureButton.DM] = new()
                {
                    // English: the Azure Button where M was {0}
                    // Example: the Azure Button where M was 1
                    Discriminator = "Mが{0}だった空色ボタン",
                },
                [SAzureButton.DDecoyArrowDirection] = new()
                {
                    // English: the Azure Button where the decoy arrow went {0} at some point
                    // Example: the Azure Button where the decoy arrow went north at some point
                    Discriminator = "ある時点で囮の矢印が{0}を指していた空色ボタン",
                    Arguments = new()
                    {
                        ["north"] = "北",
                        ["north-east"] = "北東",
                        ["east"] = "東",
                        ["south-east"] = "南東",
                        ["south"] = "南",
                        ["south-west"] = "南西",
                        ["west"] = "西",
                        ["north-west"] = "北西",
                    },
                },
                [SAzureButton.DNonDecoyArrowDirection] = new()
                {
                    // English: the Azure Button where the {1} non-decoy arrow went {0} at some point
                    // Example: the Azure Button where the first non-decoy arrow went north at some point
                    Discriminator = "ある時点で囮ではない{1}番目の矢印が{0}を指していた空色ボタン",
                    Arguments = new()
                    {
                        ["north"] = "北",
                        ["north-east"] = "北東",
                        ["east"] = "東",
                        ["south-east"] = "南東",
                        ["south"] = "南",
                        ["south-west"] = "南西",
                        ["west"] = "西",
                        ["north-west"] = "北西",
                    },
                },
            },
        },

        // Bakery
        [typeof(SBakery)] = new()
        {
            ModuleName = "クッキー屋",
            Questions = new()
            {
                [SBakery.Items] = new()
                {
                    // English: Which menu item was present in {0}?
                    Question = "{0}で存在したメニュー商品は？",
                },
            },
        },

        // Bamboozled Again
        [typeof(SBamboozledAgain)] = new()
        {
            ModuleName = "再錯綜",
            Questions = new()
            {
                [SBamboozledAgain.ButtonText] = new()
                {
                    // English: What text was initially shown on this button in {0}? (+ sprite)
                    // Example: What text was initially shown on this button in Bamboozled Again? (+ sprite)
                    Question = "{0}のこのボタンに最初に表示されたテキストは？",
                },
                [SBamboozledAgain.ButtonColor] = new()
                {
                    // English: What was the initial color of this button in {0}? (+ sprite)
                    // Example: What was the initial color of this button in Bamboozled Again? (+ sprite)
                    Question = "{0}のこのボタンの初期色は？",
                    Answers = new()
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
                [SBamboozledAgain.DisplayTexts1] = new()
                {
                    // English: What was the {1} decrypted text on the display in {0}?
                    // Example: What was the first decrypted text on the display in Bamboozled Again?
                    Question = "{0}のディスプレーで{1}番目の解読したテキストは？",
                },
                [SBamboozledAgain.DisplayTexts2] = new()
                {
                    // English: What was the {1} decrypted text on the display in {0}?
                    // Example: What was the first decrypted text on the display in Bamboozled Again?
                    Question = "{0}のディスプレーで{1}番目の解読したテキストは？",
                },
                [SBamboozledAgain.DisplayColor] = new()
                {
                    // English: What color was the {1} text on the display in {0}?
                    // Example: What color was the first text on the display in Bamboozled Again?
                    Question = "{0}のディスプレーで{1}番目のテキストの色は？",
                    Answers = new()
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
            },
        },

        // Bamboozling Button
        [typeof(SBamboozlingButton)] = new()
        {
            ModuleName = "錯綜ボタン",
            Questions = new()
            {
                [SBamboozlingButton.Color] = new()
                {
                    // English: What color was the button in the {1} stage of {0}?
                    // Example: What color was the button in the first stage of Bamboozling Button?
                    Question = "{0}のステージ{1}におけるボタンの色は？",
                    Answers = new()
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
                [SBamboozlingButton.DisplayColor] = new()
                {
                    // English: What was the color of the {2} display in the {1} stage of {0}?
                    // Example: What was the color of the first display in the first stage of Bamboozling Button?
                    Question = "{0}のステージ{1}における{2}番目の内容の色は？",
                    Answers = new()
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
                [SBamboozlingButton.Display] = new()
                {
                    // English: What was the {2} display in the {1} stage of {0}?
                    // Example: What was the first display in the first stage of Bamboozling Button?
                    Question = "{0}のステージ{1}における{2}番目の内容は？",
                },
                [SBamboozlingButton.Label] = new()
                {
                    // English: What was the {2} label on the button in the {1} stage of {0}?
                    // Example: What was the top label on the button in the first stage of Bamboozling Button?
                    Question = "{0}のステージ{1}における{2}のラベルは？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["bottom"] = "下",
                    },
                },
            },
        },

        // Bar Charts
        [typeof(SBarCharts)] = new()
        {
            ModuleName = "棒グラフ",
            Questions = new()
            {
                [SBarCharts.Category] = new()
                {
                    // English: What was the category of {0}?
                    Question = "{0}のカテゴリは？",
                },
                [SBarCharts.Unit] = new()
                {
                    // English: What was the unit of {0}?
                    Question = "{0}の単位は？",
                },
                [SBarCharts.Label] = new()
                {
                    // English: What was the label of the {1} bar in {0}?
                    // Example: What was the label of the first bar in Bar Charts?
                    Question = "{0}の{1}番目の棒のラベルは？",
                },
                [SBarCharts.Color] = new()
                {
                    // English: What was the color of the {1} bar in {0}?
                    // Example: What was the color of the first bar in Bar Charts?
                    Question = "{0}の{1}本目の棒は何色だった？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                    },
                },
                [SBarCharts.Height] = new()
                {
                    // English: What was the position of the {1} bar in {0}?
                    // Example: What was the position of the shortest bar in Bar Charts?
                    Question = "{0}で{1}棒の位置は？",
                    Arguments = new()
                    {
                        ["shortest"] = "最も短かった",
                        ["second shortest"] = "二番目に短かった",
                        ["second tallest"] = "二番目に長かった",
                        ["tallest"] = "最も長かった",
                    },
                },
            },
        },

        // Barcode Cipher
        [typeof(SBarcodeCipher)] = new()
        {
            ModuleName = "バーコード暗号",
            Questions = new()
            {
                [SBarcodeCipher.ScreenNumber] = new()
                {
                    // English: What was the screen number in {0}?
                    Question = "{0}の画面上の数字は？",
                },
                [SBarcodeCipher.BarcodeEdgework] = new()
                {
                    // English: What was the edgework represented by the {1} barcode in {0}?
                    // Example: What was the edgework represented by the first barcode in Barcode Cipher?
                    Question = "{0}の{1}番目のバーコードが示していたエッジワークは？",
                    Answers = new()
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
                [SBarcodeCipher.BarcodeAnswers] = new()
                {
                    // English: What was the answer for the {1} barcode in {0}?
                    // Example: What was the answer for the first barcode in Barcode Cipher?
                    Question = "{0}の{1}番目の回答は？",
                },
            },
        },

        // Bartending
        [typeof(SBartending)] = new()
        {
            ModuleName = "バーテンディング",
            Questions = new()
            {
                [SBartending.Ingredients] = new()
                {
                    // English: Which ingredient was in the {1} position on {0}?
                    // Example: Which ingredient was in the first position on Bartending?
                    Question = "{0}で{1}番目の位置にあった材料は？",
                    Answers = new()
                    {
                        ["Adelhyde"] = "アデルハイド",
                        ["Flanergide"] = "フラナガイド",
                        ["Bronson Extract"] = "ブロンソンエキス",
                        ["Karmotrine"] = "カルモトリン",
                        ["Powdered Delta"] = "デルタパウダー",
                    },
                },
            },
        },

        // Beans
        [typeof(SBeans)] = new()
        {
            ModuleName = "豆",
            Questions = new()
            {
                [SBeans.Colors] = new()
                {
                    // English: What was this bean in {0}? (+ sprite)
                    Question = "{0}のこの豆はどんな豆だった？",
                    Answers = new()
                    {
                        ["Wobbly Orange"] = "揺れオレンジ",
                        ["Wobbly Yellow"] = "揺れ黄",
                        ["Wobbly Green"] = "揺れ緑",
                        ["Not Wobbly Orange"] = "静止オレンジ",
                        ["Not Wobbly Yellow"] = "静止黄",
                        ["Not Wobbly Green"] = "静止緑",
                    },
                },
            },
        },

        // Bean Sprouts
        [typeof(SBeanSprouts)] = new()
        {
            ModuleName = "もやし",
            Questions = new()
            {
                [SBeanSprouts.Colors] = new()
                {
                    // English: What was sprout {1} in {0}?
                    // Example: What was sprout 1 in Bean Sprouts?
                    Question = "{0}の{1}番目のもやしは？",
                    Answers = new()
                    {
                        ["Raw"] = "生",
                        ["Cooked"] = "調理済み",
                        ["Burnt"] = "焦げている",
                        ["Fake"] = "偽物",
                    },
                },
                [SBeanSprouts.Beans] = new()
                {
                    // English: What bean was on sprout {1} in {0}?
                    // Example: What bean was on sprout 1 in Bean Sprouts?
                    Question = "{0}の{1}番目のもやしの豆は？",
                    Answers = new()
                    {
                        ["Left"] = "左",
                        ["Right"] = "右",
                        ["None"] = "無し",
                        ["Both"] = "両方",
                    },
                },
            },
        },

        // Big Bean
        [typeof(SBigBean)] = new()
        {
            ModuleName = "大きい豆",
            Questions = new()
            {
                [SBigBean.Color] = new()
                {
                    // English: What was the bean in {0}?
                    Question = "{0}の豆の状態は？",
                    Answers = new()
                    {
                        ["Wobbly Orange"] = "揺れオレンジ",
                        ["Wobbly Yellow"] = "揺れ黄",
                        ["Wobbly Green"] = "揺れ緑",
                        ["Not Wobbly Orange"] = "静止オレンジ",
                        ["Not Wobbly Yellow"] = "静止黄",
                        ["Not Wobbly Green"] = "静止緑",
                    },
                },
            },
        },

        // Big Circle
        [typeof(SBigCircle)] = new()
        {
            ModuleName = "ビッグサークル",
            Questions = new()
            {
                [SBigCircle.Colors] = new()
                {
                    // English: What color was {1} in the solution to {0}?
                    // Example: What color was first in the solution to Big Circle?
                    Question = "{0}の解法において{1}番目の色は？",
                    Answers = new()
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
            },
        },

        // Binary
        [typeof(SBinary)] = new()
        {
            ModuleName = "二進法LED",
            Questions = new()
            {
                [SBinary.Word] = new()
                {
                    // English: What word was displayed in {0}?
                    Question = "{0}で表示された単語は？",
                },
            },
        },

        // Binary LEDs
        [typeof(SBinaryLEDs)] = new()
        {
            ModuleName = "二進法LED",
            Questions = new()
            {
                [SBinaryLEDs.Value] = new()
                {
                    // English: At which numeric value did you cut the correct wire in {0}?
                    Question = "{0}でどの数字の時に正しいワイヤを切った？",
                },
            },
        },

        // Binary Shift
        [typeof(SBinaryShift)] = new()
        {
            ModuleName = "二進数シフト",
            Questions = new()
            {
                [SBinaryShift.InitialNumber] = new()
                {
                    // English: What was the {1} initial number in {0}?
                    // Example: What was the top-left initial number in Binary Shift?
                    Question = "{0}の{1}の初期値は？",
                    Arguments = new()
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
                [SBinaryShift.SelectedNumberPossition] = new()
                {
                    // English: What number was selected at stage {1} in {0}?
                    // Example: What number was selected at stage 0 in Binary Shift?
                    Question = "{0}でステージ{1}で選択した数字は？",
                    Answers = new()
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
                [SBinaryShift.NotSelectedNumberPossition] = new()
                {
                    // English: What number was not selected at stage {1} in {0}?
                    // Example: What number was not selected at stage 0 in Binary Shift?
                    Question = "{0}でステージ{1}で選択していない数字は？",
                    Answers = new()
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
            },
        },

        // Bitmaps
        [typeof(SBitmaps)] = new()
        {
            ModuleName = "ビットマップ",
            Questions = new()
            {
                [SBitmaps.Question] = new()
                {
                    // English: How many pixels were {1} in the {2} quadrant in {0}?
                    // Example: How many pixels were white in the top left quadrant in Bitmaps?
                    Question = "{0}で{2}の区域の{1}ピクセル数は？",
                    Arguments = new()
                    {
                        ["white"] = "白",
                        ["black"] = "黒",
                        ["top left"] = "左上",
                        ["top right"] = "右上",
                        ["bottom left"] = "左下",
                        ["bottom right"] = "右下",
                    },
                },
            },
        },

        // Black Cipher
        [typeof(SBlackCipher)] = new()
        {
            ModuleName = "黒色暗号",
            Questions = new()
            {
                [SBlackCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Black Cipher?
                    Question = "{0}の答えは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Blindfolded Yahtzee
        [typeof(SBlindfoldedYahtzee)] = new()
        {
            ModuleName = "目隠しヤッツィー",
            Questions = new()
            {
                [SBlindfoldedYahtzee.Claim] = new()
                {
                    // English: What roll did the module claim in the {1} stage of {0}?
                    // Example: What roll did the module claim in the first stage of Blindfolded Yahtzee?
                    Question = "{0}のステージ{1}でモジュールが主張したロールは？",
                    Answers = new()
                    {
                        ["Yahtzee"] = "ヤッツィー",
                        ["Large Straight"] = "大ストレート",
                        ["Small Straight"] = "小ストレート",
                        ["Full House"] = "フルハウス",
                        ["Four of a Kind"] = "フォーオブカインド",
                        ["Chance"] = "チャンス",
                        ["Three of a Kind"] = "スリーオブカインド",
                        ["1s"] = "1の目",
                        ["2s"] = "2の目",
                        ["3s"] = "3の目",
                        ["4s"] = "4の目",
                        ["5s"] = "5の目",
                        ["6s"] = "6の目",
                    },
                },
            },
        },

        // Blind Maze
        [typeof(SBlindMaze)] = new()
        {
            ModuleName = "ブラインド迷路",
            Questions = new()
            {
                [SBlindMaze.Colors] = new()
                {
                    // English: What color was the {1} button in {0}?
                    // Example: What color was the north button in Blind Maze?
                    Question = "{0}の{1}のボタンの色は？",
                    Arguments = new()
                    {
                        ["north"] = "北",
                        ["east"] = "東",
                        ["west"] = "西",
                        ["south"] = "南",
                    },
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Gray"] = "灰",
                        ["Yellow"] = "黄",
                    },
                },
                [SBlindMaze.Maze] = new()
                {
                    // English: Which maze did you solve {0} on?
                    Question = "{0}で解除に使用した迷路は？",
                },
            },
        },

        // Blinking Notes
        [typeof(SBlinkingNotes)] = new()
        {
            ModuleName = "点滅音符",
            Questions = new()
            {
                [SBlinkingNotes.Song] = new()
                {
                    // English: What song was flashed in {0}?
                    Question = "{0}の点滅が再生した曲は？",
                },
            },
        },

        // Blinkstop
        [typeof(SBlinkstop)] = new()
        {
            ModuleName = "瞬きの停止",
            Questions = new()
            {
                [SBlinkstop.NumberOfFlashes] = new()
                {
                    // English: How many times did the LED flash in {0}?
                    Question = "{0}のLEDが点滅した回数は？",
                },
                [SBlinkstop.FewestFlashedColor] = new()
                {
                    // English: Which color did the LED flash the fewest times in {0}?
                    Question = "{0}で最も点滅回数が少なかったのはどの色だった？",
                    Answers = new()
                    {
                        ["Purple"] = "紫",
                        ["Cyan"] = "シアン",
                        ["Yellow"] = "黄",
                        ["Multicolor"] = "虹色",
                    },
                },
            },
        },

        // Blockbusters
        [typeof(SBlockbusters)] = new()
        {
            ModuleName = "ブロックバスター",
            Questions = new()
            {
                [SBlockbusters.LastLetter] = new()
                {
                    // English: What was the last letter pressed on {0}?
                    Question = "{0}で最後に押した文字は？",
                },
            },
        },

        // Blue Arrows
        [typeof(SBlueArrows)] = new()
        {
            ModuleName = "青色矢印",
            Questions = new()
            {
                [SBlueArrows.InitialCharacters] = new()
                {
                    // English: What were the characters on the screen in {0}?
                    Question = "{0}でスクリーンに表示された文字は？",
                },
            },
        },

        // The Blue Button
        [typeof(SBlueButton)] = new()
        {
            ModuleName = "青色ボタン",
            Questions = new()
            {
                [SBlueButton.D] = new()
                {
                    // English: What was D in {0}?
                    Question = "{0}のDはどれだったか？",
                },
                [SBlueButton.EFGH] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was E in The Blue Button?
                    Question = "{0}のEはどれだったか？",
                },
                [SBlueButton.M] = new()
                {
                    // English: What was M in {0}?
                    Question = "{0}のMはどれだったか？",
                },
                [SBlueButton.N] = new()
                {
                    // English: What was N in {0}?
                    Question = "{0}のNはどれだったか？",
                },
                [SBlueButton.P] = new()
                {
                    // English: What was P in {0}?
                    Question = "{0}のPはどれだったか？",
                },
                [SBlueButton.Q] = new()
                {
                    // English: What was Q in {0}?
                    Question = "{0}のDはどれだったか？",
                    Answers = new()
                    {
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Cyan"] = "シアン",
                        ["Red"] = "赤",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                    },
                },
                [SBlueButton.X] = new()
                {
                    // English: What was X in {0}?
                    Question = "{0}のXはどれだったか？",
                },
            },
            Discriminators = new()
            {
                [SBlueButton.DQ] = new()
                {
                    // English: the Blue Button where Q was {0}
                    // Example: the Blue Button where Q was Blue
                    Discriminator = "Qが{0}だった青色ボタン",
                    Arguments = new()
                    {
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Cyan"] = "シアン",
                        ["Red"] = "赤",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                    },
                },
                [SBlueButton.DOther] = new()
                {
                    // English: the Blue Button where {0} was {1}
                    Discriminator = "{0}が{1}だった青色ボタン",
                },
            },
        },

        // Blue Cipher
        [typeof(SBlueCipher)] = new()
        {
            ModuleName = "青色暗号",
            Questions = new()
            {
                [SBlueCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Blue Cipher?
                    Question = "{0}の答えは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Bob Barks
        [typeof(SBobBarks)] = new()
        {
            ModuleName = "ボブの咆哮",
            Questions = new()
            {
                [SBobBarks.Indicators] = new()
                {
                    // English: What was the {1} indicator label in {0}?
                    // Example: What was the top left indicator label in Bob Barks?
                    Question = "{0}の{1}のインジケーターは？",
                    Arguments = new()
                    {
                        ["top left"] = "左上",
                        ["top right"] = "右上",
                        ["bottom left"] = "左下",
                        ["bottom right"] = "右下",
                    },
                },
                [SBobBarks.Positions] = new()
                {
                    // English: Which button flashed {1} in sequence in {0}?
                    // Example: Which button flashed first in sequence in Bob Barks?
                    Question = "{0}の{1}番目に点滅したボタンは？",
                    Answers = new()
                    {
                        ["top left"] = "左上",
                        ["top right"] = "右上",
                        ["bottom left"] = "左下",
                        ["bottom right"] = "右下",
                    },
                },
            },
        },

        // Boggle
        [typeof(SBoggle)] = new()
        {
            ModuleName = "ボグル",
            Questions = new()
            {
                [SBoggle.Letters] = new()
                {
                    // English: What letter was initially visible on {0}?
                    Question = "{0}で初めに表示された文字は？",
                },
            },
        },

        // Bomb Diffusal
        [typeof(SBombDiffusal)] = new()
        {
            ModuleName = "爆弾拡散",
            Questions = new()
            {
                [SBombDiffusal.LicenseNumber] = new()
                {
                    // English: What was the license number in {0}?
                    Question = "{0}のライセンス番号は？",
                },
            },
        },

        // Bone Apple Tea
        [typeof(SBoneAppleTea)] = new()
        {
            ModuleName = "ボーンアップルティー",
            Questions = new()
            {
                [SBoneAppleTea.Phrase] = new()
                {
                    // English: Which phrase was shown on {0}?
                    Question = "{0}で表示されたフレーズは？",
                },
            },
        },

        // Boob Tube
        [typeof(SBoobTube)] = new()
        {
            ModuleName = "ブーブチューブ",
            Questions = new()
            {
                [SBoobTube.Word] = new()
                {
                    // English: Which word was shown on {0}?
                    Question = "{0}で表示された単語は？",
                },
            },
        },

        // Book of Mario
        [typeof(SBookOfMario)] = new()
        {
            ModuleName = "ブック・オブ・マリオ",
            Questions = new()
            {
                [SBookOfMario.Pictures] = new()
                {
                    // English: Who said the {1} quote in {0}?
                    // Example: Who said the first quote in Book of Mario?
                    Question = "{0}の{1}番目の文章を発言したのは？",
                },
                [SBookOfMario.Quotes] = new()
                {
                    // English: What did {1} say in the {2} stage of {0}?
                    // Example: What did Goombell say in the first stage of Book of Mario?
                    Question = "{0}のステージ{2}で{1}が発言した内容は？",
                },
            },
        },

        // Boolean Wires
        [typeof(SBooleanWires)] = new()
        {
            ModuleName = "論理ワイヤ",
            Questions = new()
            {
                [SBooleanWires.EnteredOperators] = new()
                {
                    // English: Which operator did you submit in the {1} stage of {0}?
                    // Example: Which operator did you submit in the first stage of Boolean Wires?
                    Question = "{0}のステージ{1}で送信した演算子は？",
                },
            },
        },

        // Boomtar the Great
        [typeof(SBoomtarTheGreat)] = new()
        {
            ModuleName = "偉大なるブームター",
            Questions = new()
            {
                [SBoomtarTheGreat.Rules] = new()
                {
                    // English: What was rule {1} in {0}?
                    // Example: What was rule one in Boomtar the Great?
                    Question = "{0}のルール{1}は？",
                    Arguments = new()
                    {
                        ["one"] = "1",
                        ["two"] = "2",
                    },
                },
            },
        },

        // Bordered Keys
        [typeof(SBorderedKeys)] = new()
        {
            ModuleName = "境界音板",
            Questions = new()
            {
                [SBorderedKeys.BorderColor] = new()
                {
                    // English: What was the {1} key’s border color when it was pressed in {0}?
                    // Example: What was the first key’s border color when it was pressed in Bordered Keys?
                    Question = "{0}で{1}番目の音板を押した時、縁は何色だった？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                    },
                },
                [SBorderedKeys.Digit] = new()
                {
                    // English: What was the digit displayed when the {1} key was pressed in {0}?
                    // Example: What was the digit displayed when the first key was pressed in Bordered Keys?
                    Question = "{0}で{1}番目の音板を押した時、ディスプレーの数字は何だった？",
                },
                [SBorderedKeys.KeyColor] = new()
                {
                    // English: What was the {1} key’s key color when it was pressed in {0}?
                    // Example: What was the first key’s key color when it was pressed in Bordered Keys?
                    Question = "{0}で{1}番目の音板を押した時、音板は何色だった？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                    },
                },
                [SBorderedKeys.Label] = new()
                {
                    // English: What was the {1} key’s label when it was pressed in {0}?
                    // Example: What was the first key’s label when it was pressed in Bordered Keys?
                    Question = "{0}で{1}番目の音板を押した時、ラベルは何だった？",
                },
                [SBorderedKeys.LabelColor] = new()
                {
                    // English: What was the {1} key’s label color when it was pressed in {0}?
                    // Example: What was the first key’s label color when it was pressed in Bordered Keys?
                    Question = "{0}で{1}番目の音板を押した時、ラベルの色は何色だった？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                    },
                },
            },
        },

        // Bottom Gear
        [typeof(SBottomGear)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SBottomGear.Tweet] = new()
                {
                    // English: What tweet was shown in {0}?
                    Question = "{0}に表示されたツイートは？",
                },
            },
        },

        // Boxing
        [typeof(SBoxing)] = new()
        {
            ModuleName = "ボクシング",
            Questions = new()
            {
                [SBoxing.StrengthByContestant] = new()
                {
                    // English: What was {1}’s strength rating on {0}?
                    // Example: What was Muhammad’s strength rating on Boxing?
                    Question = "{0}で{1}のパンチ力は？",
                },
                [SBoxing.ContestantByStrength] = new()
                {
                    // English: What was the {1} of the contestant with strength rating {2} on {0}?
                    // Example: What was the first name of the contestant with strength rating 0 on Boxing?
                    Question = "{0}で強さ{2}の出場者の{1}は？",
                    Arguments = new()
                    {
                        ["first name"] = "氏名",
                        ["last name"] = "姓名",
                        ["substitute’s first name"] = "補欠選手の氏名",
                        ["substitute’s last name"] = "補欠選手の姓名",
                    },
                },
                [SBoxing.Names] = new()
                {
                    // English: Which {1} appeared on {0}?
                    // Example: Which contestant’s first name appeared on Boxing?
                    Question = "{0}の{1}は？",
                    Arguments = new()
                    {
                        ["contestant’s first name"] = "出場者の氏名",
                        ["contestant’s last name"] = "出場者の姓名",
                        ["substitute’s first name"] = "補欠選手の氏名",
                        ["substitute’s last name"] = "補欠選手の姓名",
                    },
                },
            },
        },

        // Braille
        [typeof(SBraille)] = new()
        {
            ModuleName = "点字",
            Questions = new()
            {
                [SBraille.Pattern] = new()
                {
                    // English: What was the {1} pattern in {0}?
                    // Example: What was the first pattern in Braille?
                    Question = "{0}の{1}番目のパターンは？",
                },
            },
        },

        // Breakfast Egg
        [typeof(SBreakfastEgg)] = new()
        {
            ModuleName = "目玉焼き",
            Questions = new()
            {
                [SBreakfastEgg.Color] = new()
                {
                    // English: Which color appeared on the egg in {0}?
                    Question = "{0}の黄身の色は？",
                    Answers = new()
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
            },
        },

        // Broken Buttons
        [typeof(SBrokenButtons)] = new()
        {
            ModuleName = "壊れたボタン",
            Questions = new()
            {
                [SBrokenButtons.Question] = new()
                {
                    // English: What was the {1} correct button you pressed in {0}?
                    // Example: What was the first correct button you pressed in Broken Buttons?
                    Question = "{0}で{1}番目に押したボタンはどれ？",
                },
            },
        },

        // Broken Guitar Chords
        [typeof(SBrokenGuitarChords)] = new()
        {
            ModuleName = "壊れたギター・コード",
            Questions = new()
            {
                [SBrokenGuitarChords.DisplayedChord] = new()
                {
                    // English: What was the displayed chord in {0}?
                    Question = "{0}で表示されたコードは？",
                },
                [SBrokenGuitarChords.MutedString] = new()
                {
                    // English: In which position, from left to right, was the broken string in {0}?
                    Question = "{0}の壊れた弦があったのは、左から数えて何番目？",
                },
            },
        },

        // Brown Cipher
        [typeof(SBrownCipher)] = new()
        {
            ModuleName = "茶色暗号",
            Questions = new()
            {
                [SBrownCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Brown Cipher?
                    Question = "{0}の答えは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Brush Strokes
        [typeof(SBrushStrokes)] = new()
        {
            ModuleName = "ブラシストローク",
            Questions = new()
            {
                [SBrushStrokes.MiddleColor] = new()
                {
                    // English: What was the color of the middle contact point in {0}?
                    Question = "{0}の中央の接点の色は？",
                    Answers = new()
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
            },
        },

        // The Bulb
        [typeof(SBulb)] = new()
        {
            ModuleName = "ザ・電球",
            Questions = new()
            {
                [SBulb.InitialState] = new()
                {
                    // English: Was the bulb initially lit in {0}?
                    Question = "{0}の電球は初期状態で点灯していた？",
                    Answers = new()
                    {
                        ["Yes"] = "はい",
                        ["No"] = "いいえ",
                    },
                },
            },
        },

        // Burger Alarm
        [typeof(SBurgerAlarm)] = new()
        {
            ModuleName = "ハンバーガー警報",
            Questions = new()
            {
                [SBurgerAlarm.Digits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Burger Alarm?
                    Question = "{0}でディスプレーの{1}番目の数字は？",
                },
                [SBurgerAlarm.OrderNumbers] = new()
                {
                    // English: What was the {1} order number in {0}?
                    // Example: What was the first order number in Burger Alarm?
                    Question = "{0}の{1}番目の注文番号は？",
                },
            },
        },

        // Burglar Alarm
        [typeof(SBurglarAlarm)] = new()
        {
            ModuleName = "盗難警報",
            Questions = new()
            {
                [SBurglarAlarm.Digits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Burglar Alarm?
                    Question = "{0}で{1}番目に表示された数字は？",
                },
            },
        },

        // The Button
        [typeof(SButton)] = new()
        {
            ModuleName = "ボタン",
            Questions = new()
            {
                [SButton.LightColor] = new()
                {
                    // English: What color did the light glow in {0}?
                    Question = "{0}で光ったライトの色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["yellow"] = "黄",
                        ["white"] = "白",
                    },
                },
            },
        },

        // Buttonage
        [typeof(SButtonage)] = new()
        {
            ModuleName = "大量ボタン",
            Questions = new()
            {
                [SButtonage.Buttons] = new()
                {
                    // English: How many {1} buttons were there on {0}?
                    // Example: How many red buttons were there on Buttonage?
                    Question = "{0}に{1}ボタンは何個あった？",
                    Arguments = new()
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
            },
        },

        // Button Sequence
        [typeof(SButtonSequence)] = new()
        {
            ModuleName = "順番ボタン",
            Questions = new()
            {
                [SButtonSequence.sColorOccurrences] = new()
                {
                    // English: How many of the buttons in {0} were {1}?
                    // Example: How many of the buttons in Button Sequence were red?
                    Question = "{0}内の{1}色のボタンはいくつ？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["yellow"] = "黄",
                        ["white"] = "白",
                    },
                },
            },
        },

        // Cacti’s Conundrum
        [typeof(SCactisConundrum)] = new()
        {
            ModuleName = "サボテン難問",
            Questions = new()
            {
                [SCactisConundrum.Color] = new()
                {
                    // English: What color was the LED in the {1} stage of {0}?
                    // Example: What color was the LED in the first stage of Cacti’s Conundrum?
                    Question = "{0}のステージ{1}におけるLEDの色は？",
                    Answers = new()
                    {
                        ["Blue"] = "青",
                        ["Lime"] = "黄緑",
                        ["Orange"] = "オレンジ",
                        ["Red"] = "赤",
                    },
                },
            },
        },

        // Caesar Cycle
        [typeof(SCaesarCycle)] = new()
        {
            ModuleName = "カエサルサイクル",
            Questions = new()
            {
                [SCaesarCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Caesar Cycle?
                    Question = "{0}の{1}番目のダイヤルはどの方向を指していた？",
                },
                [SCaesarCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Caesar Cycle?
                    Question = "{0}の{1}番目のダイヤルに書かれていた文字は？",
                },
            },
            Discriminators = new()
            {
                [SCaesarCycle.LabelDiscriminator] = new()
                {
                    // English: the Caesar Cycle that had the letter {0} on a dial
                    // Example: the Caesar Cycle that had the letter A on a dial
                    Discriminator = "ダイヤルに{0}の文字があったカエサルサイクル",
                },
            },
        },

        // Caesar Psycho
        [typeof(SCaesarPsycho)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCaesarPsycho.ScreenTexts] = new()
                {
                    // English: What text was on the top display in the {1} stage of {0}?
                    // Example: What text was on the top display in the first stage of Caesar Psycho?
                    Question = "{0}のステージ{1}における上のディスプレーに表示された単語は？",
                },
                [SCaesarPsycho.ScreenColor] = new()
                {
                    // English: What color was the text on the top display in the second stage of {0}?
                    Question = "{0}のステージ2における上のディスプレーに表示された単語の色は？",
                },
            },
        },

        // Caesar's Maths
        [typeof(SCaesarsMaths)] = new()
        {
            ModuleName = "カエサル数学",
            Questions = new()
            {
                [SCaesarsMaths.LED] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the first LED in Caesar's Maths?
                    Question = "{0}の{1}番目のLEDの色は？",
                    Answers = new()
                    {
                        ["Yellow"] = "黄",
                        ["Blue"] = "青",
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                    },
                },
            },
        },

        // Calendar
        [typeof(SCalendar)] = new()
        {
            ModuleName = "カレンダー",
            Questions = new()
            {
                [SCalendar.LedColor] = new()
                {
                    // English: What was the LED color in {0}?
                    Question = "{0}のLEDの色は？",
                    Answers = new()
                    {
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                    },
                },
            },
        },

        // CA-RPS
        [typeof(SCARPS)] = new()
        {
            ModuleName = "じゃんけんグリッド",
            Questions = new()
            {
                [SCARPS.Cell] = new()
                {
                    // English: What color was this cell initially in {0}? (+ sprite)
                    Question = "{0}のこのセルの初期色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Black"] = "黒",
                    },
                },
            },
        },

        // Cartinese
        [typeof(SCartinese)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCartinese.ButtonColors] = new()
                {
                    // English: What color was the {1} button in {0}?
                    // Example: What color was the up button in Cartinese?
                    Question = "{0}の{1}のボタンの色は？",
                    Arguments = new()
                    {
                        ["up"] = "上",
                        ["right"] = "右",
                        ["down"] = "下",
                        ["left"] = "左",
                    },
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                    },
                },
                [SCartinese.Lyrics] = new()
                {
                    // English: What lyric was played by the {1} button in {0}?
                    // Example: What lyric was played by the up button in Cartinese?
                    Question = "{0}の{1}のボタンから再生された歌詞は？",
                    Arguments = new()
                    {
                        ["up"] = "上",
                        ["right"] = "右",
                        ["down"] = "下",
                        ["left"] = "左",
                    },
                },
            },
        },

        // Catchphrase
        [typeof(SCatchphrase)] = new()
        {
            ModuleName = "キャッチフレーズ",
            Questions = new()
            {
                [SCatchphrase.Colour] = new()
                {
                    // English: What was the colour of the {1} panel in {0}?
                    // Example: What was the colour of the top-left panel in Catchphrase?
                    Question = "{0}の{1}のパネルの色は？",
                    Arguments = new()
                    {
                        ["top-left"] = "左上",
                        ["top-right"] = "右上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Orange"] = "オレンジ",
                        ["Purple"] = "紫",
                        ["Yellow"] = "黄",
                    },
                },
            },
        },

        // Challenge & Contact
        [typeof(SChallengeAndContact)] = new()
        {
            ModuleName = "チャレンジ＆コンタクト",
            Questions = new()
            {
                [SChallengeAndContact.Answers] = new()
                {
                    // English: What was the {1} submitted answer in {0}?
                    // Example: What was the first submitted answer in Challenge & Contact?
                    Question = "{0}の{1}番目に送信された回答は？",
                },
            },
        },

        // Character Codes
        [typeof(SCharacterCodes)] = new()
        {
            ModuleName = "文字コード",
            Questions = new()
            {
                [SCharacterCodes.Character] = new()
                {
                    // English: What was the {1} character in {0}?
                    // Example: What was the first character in Character Codes?
                    Question = "{0}の{1}番目の文字は？",
                },
            },
        },

        // Character Shift
        [typeof(SCharacterShift)] = new()
        {
            ModuleName = "文字シフト",
            Questions = new()
            {
                [SCharacterShift.Letters] = new()
                {
                    // English: Which letter was present but not submitted on the left slider of {0}?
                    Question = "{0}の左スライダーにあった送信していない英字は？",
                },
                [SCharacterShift.Digits] = new()
                {
                    // English: Which digit was present but not submitted on the right slider of {0}?
                    Question = "{0}の右スライダーにあった送信していない英字は？",
                },
            },
        },

        // Character Slots
        [typeof(SCharacterSlots)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCharacterSlots.DisplayedCharacters] = new()
                {
                    // English: Who was displayed in the {1} slot in the {2} stage of {0}?
                    // Example: Who was displayed in the first slot in the first stage of Character Slots?
                    Question = "{0}でステージ{2}の{2}のスロットに表示されていたのは誰？",
                },
            },
        },

        // Cheap Checkout
        [typeof(SCheapCheckout)] = new()
        {
            ModuleName = "安勘定",
            Questions = new()
            {
                [SCheapCheckout.Paid] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was the paid amount in Cheap Checkout?
                    Question = "{0}の{1}は？",
                    Arguments = new()
                    {
                        ["the paid amount"] = "支払金額",
                        ["the first paid amount"] = "最初の支払金額",
                        ["the second paid amount"] = "二回目の支払金額",
                    },
                },
            },
        },

        // Cheat Checkout
        [typeof(SCheatCheckout)] = new()
        {
            ModuleName = "狡勘定",
            Questions = new()
            {
                [SCheatCheckout.Currency] = new()
                {
                    // English: What was the cryptocurrency of {0}?
                    Question = "{0}の暗号通貨は？",
                },
                [SCheatCheckout.Hack] = new()
                {
                    // English: What was the hack method for the {1} hack of {0}?
                    // Example: What was the hack method for the first hack of Cheat Checkout?
                    Question = "{0}の{1}番目のハッキングで使用されたハッキング方法は？",
                },
                [SCheatCheckout.Site] = new()
                {
                    // English: What was the site for the {1} hack of {0}?
                    // Example: What was the site for the first hack of Cheat Checkout?
                    Question = "{0}の{1}番目のハッキング先のサイトは？",
                },
            },
        },

        // Cheep Checkout
        [typeof(SCheepCheckout)] = new()
        {
            ModuleName = "鳥勘定",
            Questions = new()
            {
                [SCheepCheckout.Birds] = new()
                {
                    // English: Which bird {1} present in {0}?
                    // Example: Which bird was present in Cheep Checkout?
                    Question = "{0}に存在して{1}のは？",
                    Arguments = new()
                    {
                        ["was"] = "いた",
                        ["was not"] = "いなかった",
                    },
                    Answers = new()
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
            },
        },

        // Chess
        [typeof(SChess)] = new()
        {
            ModuleName = "チェス",
            Questions = new()
            {
                [SChess.Coordinate] = new()
                {
                    // English: What was the {1} coordinate in {0}?
                    // Example: What was the first coordinate in Chess?
                    Question = "{0}の{1}番目の座標は？",
                },
            },
        },

        // Chinese Counting
        [typeof(SChineseCounting)] = new()
        {
            ModuleName = "中国の数え方",
            Questions = new()
            {
                [SChineseCounting.LED] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the left LED in Chinese Counting?
                    Question = "{0}の{1}のLEDの色は？",
                    Arguments = new()
                    {
                        ["left"] = "左",
                        ["right"] = "右",
                    },
                    Answers = new()
                    {
                        ["White"] = "白",
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Orange"] = "オレンジ",
                    },
                },
            },
        },

        // Chinese Remainder Theorem
        [typeof(SChineseRemainderTheorem)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SChineseRemainderTheorem.Equations] = new()
                {
                    // English: Which equation was used in {0}?
                    Question = "{0}で使用された式は？",
                },
            },
        },

        // Chord Qualities
        [typeof(SChordQualities)] = new()
        {
            ModuleName = "コードクオリティー",
            Questions = new()
            {
                [SChordQualities.Notes] = new()
                {
                    // English: Which note was part of the given chord in {0}?
                    Question = "{0}で与えられたコードの一部にある音は？",
                },
            },
        },

        // ↻↺
        [typeof(SClockCounter)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SClockCounter.Arrows] = new()
                {
                    // English: Which arrow was shown in {0}?
                    Question = "{0}で表示された矢印は？",
                },
            },
        },

        // The Code
        [typeof(SCode)] = new()
        {
            ModuleName = "コード",
            Questions = new()
            {
                [SCode.DisplayNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "{0}で表示された数字は？",
                },
            },
        },

        // Codenames
        [typeof(SCodenames)] = new()
        {
            ModuleName = "コードネーム",
            Questions = new()
            {
                [SCodenames.Answers] = new()
                {
                    // English: Which of these words was submitted in {0}?
                    Question = "{0}で送信された単語に含まれるのは？",
                },
            },
        },

        // Coffee Beans
        [typeof(SCoffeeBeans)] = new()
        {
            ModuleName = "コーヒー豆",
            Questions = new()
            {
                [SCoffeeBeans.Movements] = new()
                {
                    // English: What was the {1} movement in {0}?
                    // Example: What was the first movement in Coffee Beans?
                    Question = "{0}の{1}番目の動きは？",
                    Answers = new()
                    {
                        ["Horizontal"] = "水平",
                        ["Vertical"] = "垂直",
                        ["Diagonal"] = "対角",
                        ["Nothing"] = "無し",
                    },
                },
            },
        },

        // Coffeebucks
        [typeof(SCoffeebucks)] = new()
        {
            ModuleName = "コーヒーバックス",
            Questions = new()
            {
                [SCoffeebucks.Coffee] = new()
                {
                    // English: What was the last served coffee in {0}?
                    Question = "{0}の最後に提供したコーヒーは？",
                },
            },
        },

        // Coinage
        [typeof(SCoinage)] = new()
        {
            ModuleName = "大量コイン",
            Questions = new()
            {
                [SCoinage.Flip] = new()
                {
                    // English: Which coin was flipped in {0}?
                    Question = "{0}で裏返したコインは？",
                },
            },
        },

        // Color Addition
        [typeof(SColorAddition)] = new()
        {
            ModuleName = "色の加算",
            Questions = new()
            {
                [SColorAddition.Numbers] = new()
                {
                    // English: What was {1}’s number in {0}?
                    // Example: What was red’s number in Color Addition?
                    Question = "{0}の{1}の数字は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
            },
        },

        // Color Braille
        [typeof(SColorBraille)] = new()
        {
            ModuleName = "色付き点字",
            Questions = new()
            {
                [SColorBraille.Color] = new()
                {
                    // English: What color was this dot in {0}? (+ sprite)
                    Question = "{0}のこの点の色は？",
                    Answers = new()
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
            },
        },

        // Color Decoding
        [typeof(SColorDecoding)] = new()
        {
            ModuleName = "色の解読",
            Questions = new()
            {
                [SColorDecoding.IndicatorColors] = new()
                {
                    // English: Which color {1} in the {2}-stage indicator pattern in {0}?
                    // Example: Which color appeared in the first-stage indicator pattern in Color Decoding?
                    Question = "{0}のステージ{2}で表示されて{1}色は？",
                    Arguments = new()
                    {
                        ["appeared"] = "いた",
                        ["did not appear"] = "いなかった",
                    },
                    Answers = new()
                    {
                        ["Green"] = "緑",
                        ["Purple"] = "紫",
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                    },
                },
                [SColorDecoding.IndicatorPattern] = new()
                {
                    // English: What was the {1}-stage indicator pattern in {0}?
                    // Example: What was the first-stage indicator pattern in Color Decoding?
                    Question = "{0}でステージ{1}のインジケーターのパターンは？",
                    Answers = new()
                    {
                        ["Checkered"] = "チェック",
                        ["Horizontal"] = "ボーダー",
                        ["Vertical"] = "ストライプ",
                        ["Solid"] = "一色",
                    },
                },
            },
        },

        // Colored Keys
        [typeof(SColoredKeys)] = new()
        {
            ModuleName = "色付きキーパッド",
            Questions = new()
            {
                [SColoredKeys.DisplayWord] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "{0}で表示された単語は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["green"] = "緑",
                        ["yellow"] = "黄",
                        ["purple"] = "紫",
                        ["white"] = "白",
                    },
                },
                [SColoredKeys.DisplayWordColor] = new()
                {
                    // English: What was the displayed word’s color in {0}?
                    Question = "{0}で表示された単語の色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["green"] = "緑",
                        ["yellow"] = "黄",
                        ["purple"] = "紫",
                        ["white"] = "白",
                    },
                },
                [SColoredKeys.KeyLetter] = new()
                {
                    // English: What letter was on the {1} key in {0}?
                    // Example: What letter was on the top-left key in Colored Keys?
                    Question = "{0}の{1}のキーパッドの文字は？",
                    Arguments = new()
                    {
                        ["top-left"] = "左上",
                        ["top-right"] = "右上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                },
                [SColoredKeys.KeyColor] = new()
                {
                    // English: What was the color of the {1} key in {0}?
                    // Example: What was the color of the top-left key in Colored Keys?
                    Question = "{0}の{1}のキーの色は？",
                    Arguments = new()
                    {
                        ["top-left"] = "左上",
                        ["top-right"] = "右上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["green"] = "緑",
                        ["yellow"] = "黄",
                        ["purple"] = "紫",
                        ["white"] = "白",
                    },
                },
            },
        },

        // Colored Squares
        [typeof(SColoredSquares)] = new()
        {
            ModuleName = "色付き格子",
            Questions = new()
            {
                [SColoredSquares.FirstGroup] = new()
                {
                    // English: What was the first color group in {0}?
                    Question = "{0}の最初の色グループは？",
                    Answers = new()
                    {
                        ["White"] = "白",
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                        ["Magenta"] = "マゼンタ",
                    },
                },
            },
        },

        // Colored Switches
        [typeof(SColoredSwitches)] = new()
        {
            ModuleName = "色付きスイッチ",
            Questions = new()
            {
                [SColoredSwitches.InitialPosition] = new()
                {
                    // English: What was the initial position of the switches in {0}?
                    Question = "{0}の初期配置は？",
                },
                [SColoredSwitches.WhenLEDsCameOn] = new()
                {
                    // English: What was the position of the switches when the LEDs came on in {0}?
                    Question = "{0}のLEDが示したスイッチの位置は？",
                },
            },
        },

        // Color Morse
        [typeof(SColorMorse)] = new()
        {
            ModuleName = "カラーモールス",
            Questions = new()
            {
                [SColorMorse.Color] = new()
                {
                    // English: What was the color of the {1} LED in {0}?
                    // Example: What was the color of the first LED in Color Morse?
                    Question = "{0}の{1}番目のLEDは何色？",
                    Answers = new()
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
                [SColorMorse.Character] = new()
                {
                    // English: What character was flashed by the {1} LED in {0}?
                    // Example: What character was flashed by the first LED in Color Morse?
                    Question = "{0}の{1}番目のLEDが示す文字は？",
                },
            },
        },

        // Color One Two
        [typeof(SColorOneTwo)] = new()
        {
            ModuleName = "色の1と2",
            Questions = new()
            {
                [SColorOneTwo.Color] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the left LED in Color One Two?
                    Question = "{0}の{1}側のLEDの色は？",
                    Arguments = new()
                    {
                        ["left"] = "左",
                        ["right"] = "右",
                    },
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                    },
                },
            },
        },

        // Colors Maximization
        [typeof(SColorsMaximization)] = new()
        {
            ModuleName = "最大色",
            Questions = new()
            {
                [SColorsMaximization.ColorCount] = new()
                {
                    // English: How many buttons were {1} in {0}?
                    // Example: How many buttons were red in Colors Maximization?
                    Question = "{0}で{1}色だったボタンは何個？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
            },
        },

        // Coloured Cubes
        [typeof(SColouredCubes)] = new()
        {
            ModuleName = "色付きキューブ",
            Questions = new()
            {
                [SColouredCubes.Colours] = new()
                {
                    // English: What was the colour of this {1} in the {2} stage of {0}? (+ sprite)
                    // Example: What was the colour of this cube in the first stage of Coloured Cubes? (+ sprite)
                    Question = "{0}のステージ{2}におけるこの{1}の色は？",
                    Arguments = new()
                    {
                        ["cube"] = "キューブ",
                        ["stage light"] = "ステータスライト",
                    },
                    Answers = new()
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
            },
        },

        // Coloured Cylinder
        [typeof(SColouredCylinder)] = new()
        {
            ModuleName = "色付きシリンダー",
            Questions = new()
            {
                [SColouredCylinder.Colours] = new()
                {
                    // English: What was the {1} colour flashed on the cylinder in {0}?
                    // Example: What was the first colour flashed on the cylinder in Coloured Cylinder?
                    Question = "{0}でシリンダーが{1}番目に光った色は？",
                    Answers = new()
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
            },
        },

        // Colour Flash
        [typeof(SColourFlash)] = new()
        {
            ModuleName = "カラーフラッシュ",
            Questions = new()
            {
                [SColourFlash.LastColor] = new()
                {
                    // English: What was the color of the last word in the sequence in {0}?
                    Question = "{0}のシーケンスの最後の単語は何色？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Magenta"] = "マゼンタ",
                        ["White"] = "白",
                    },
                },
            },
        },

        // Concentration
        [typeof(SConcentration)] = new()
        {
            ModuleName = "集中",
            Questions = new()
            {
                [SConcentration.StartingDigit] = new()
                {
                    // English: What number began here in {0}? (+ sprite)
                    Question = "{0}で、初期状態でこの場所にあった数字は？",
                },
            },
            Discriminators = new()
            {
                [SConcentration.Discriminator] = new()
                {
                    // English: the Concentration which began with {1} in the {0} position (in reading order)
                    // Example: the Concentration which began with 1 in the first position (in reading order)
                    Discriminator = "初期状態で読み順で{0}番目に{1}があった集中",
                },
            },
        },

        // Conditional Buttons
        [typeof(SConditionalButtons)] = new()
        {
            ModuleName = "条件ボタン",
            Questions = new()
            {
                [SConditionalButtons.Colors] = new()
                {
                    // English: What was the color of this button in {0}? (+ sprite)
                    Question = "{0}のこのボタンの色は？",
                    Answers = new()
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
            },
        },

        // Connected Monitors
        [typeof(SConnectedMonitors)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SConnectedMonitors.Number] = new()
                {
                    // English: What number was initially displayed on this screen in {0}? (+ sprite)
                    Question = "{0}のこの画面に最初表示された数字は？",
                },
                [SConnectedMonitors.SingleIndicator] = new()
                {
                    // English: What colour was the indicator on this screen in {0}? (+ sprite)
                    Question = "{0}のこの画面にあったインジケーターの色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Orange"] = "オレンジ",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Purple"] = "紫",
                        ["White"] = "白",
                    },
                },
                [SConnectedMonitors.OrdinalIndicator] = new()
                {
                    // English: What colour was the {1} indicator on this screen in {0}? (+ sprite)
                    // Example: What colour was the first indicator on this screen in Connected Monitors? (+ sprite)
                    Question = "{0}のこの画面にあったインジケーター{1}の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Orange"] = "オレンジ",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Purple"] = "紫",
                        ["White"] = "白",
                    },
                },
            },
        },

        // Connection Check
        [typeof(SConnectionCheck)] = new()
        {
            ModuleName = "接続確認",
            Questions = new()
            {
                [SConnectionCheck.Numbers] = new()
                {
                    // English: What pair of numbers was present in {0}?
                    Question = "{0}内に存在していたペアは？",
                },
            },
            Discriminators = new()
            {
                [SConnectionCheck.NoNs] = new()
                {
                    // English: the Connection Check with no {0}s
                    // Example: the Connection Check with no 1s
                    Discriminator = "{0}が無い接続確認",
                },
                [SConnectionCheck.OneN] = new()
                {
                    // English: the Connection Check with one {0}
                    // Example: the Connection Check with one 1
                    Discriminator = "{0}が一つだけある接続確認",
                },
                [SConnectionCheck.TwoNs] = new()
                {
                    // English: the Connection Check with two {0}s
                    // Example: the Connection Check with two 1s
                    Discriminator = "{0}が二つだけある接続確認",
                },
                [SConnectionCheck.ThreeNs] = new()
                {
                    // English: the Connection Check with three {0}s
                    // Example: the Connection Check with three 1s
                    Discriminator = "{0}が三つだけある接続確認",
                },
                [SConnectionCheck.FourNs] = new()
                {
                    // English: the Connection Check with four {0}s
                    // Example: the Connection Check with four 1s
                    Discriminator = "{0}が四つだけある接続確認",
                },
            },
        },

        // Coordinates
        [typeof(SCoordinates)] = new()
        {
            ModuleName = "座標",
            Questions = new()
            {
                [SCoordinates.FirstSolution] = new()
                {
                    // English: What was the solution you selected first in {0}?
                    Question = "{0}で最初に選んだ回答は？",
                },
                [SCoordinates.Size] = new()
                {
                    // English: What was the grid size in {0}?
                    Question = "{0}のグリッドのサイズは？",
                },
            },
        },

        // Coordination
        [typeof(SCoordination)] = new()
        {
            ModuleName = "座標グリッド",
            Questions = new()
            {
                [SCoordination.Label] = new()
                {
                    // English: What was the label of the starting coordinate in {0}?
                    Question = "{0}の開始座標のラベルは？",
                },
                [SCoordination.Position] = new()
                {
                    // English: Where was the starting coordinate in {0}?
                    Question = "{0}の開始座標はどこ？",
                },
            },
        },

        // Coral Cipher
        [typeof(SCoralCipher)] = new()
        {
            ModuleName = "珊瑚色暗号",
            Questions = new()
            {
                [SCoralCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Coral Cipher?
                    Question = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Corners
        [typeof(SCorners)] = new()
        {
            ModuleName = "コーナー",
            Questions = new()
            {
                [SCorners.Colors] = new()
                {
                    // English: What was the color of the {1} corner in {0}?
                    // Example: What was the color of the top-left corner in Corners?
                    Question = "{0}の{1}の角は何色？",
                    Arguments = new()
                    {
                        ["top-left"] = "左上",
                        ["top-right"] = "右上",
                        ["bottom-right"] = "右下",
                        ["bottom-left"] = "左下",
                    },
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["yellow"] = "黄",
                    },
                },
                [SCorners.ColorCount] = new()
                {
                    // English: How many corners in {0} were {1}?
                    // Example: How many corners in Corners were red?
                    Question = "{0}の{1}色の角はいくつ？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["yellow"] = "黄",
                    },
                },
            },
        },

        // Cornflower Cipher
        [typeof(SCornflowerCipher)] = new()
        {
            ModuleName = "矢車菊色暗号",
            Questions = new()
            {
                [SCornflowerCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Cornflower Cipher?
                    Question = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Cosmic
        [typeof(SCosmic)] = new()
        {
            ModuleName = "宇宙",
            Questions = new()
            {
                [SCosmic.Number] = new()
                {
                    // English: What was the number initially shown in {0}?
                    Question = "{0}の最初に表示された番号は？",
                },
            },
        },

        // Crazy Hamburger
        [typeof(SCrazyHamburger)] = new()
        {
            ModuleName = "クレイジーハンバーガー",
            Questions = new()
            {
                [SCrazyHamburger.Ingredient] = new()
                {
                    // English: What was the {1} ingredient shown in {0}?
                    // Example: What was the first ingredient shown in Crazy Hamburger?
                    Question = "{0}の{1}番目の材料は？",
                },
            },
        },

        // Crazy Maze
        [typeof(SCrazyMaze)] = new()
        {
            ModuleName = "クレイジー迷路",
            Questions = new()
            {
                [SCrazyMaze.StartOrGoal] = new()
                {
                    // English: What was the {1} location in {0}?
                    // Example: What was the starting location in Crazy Maze?
                    Question = "{0}の{1}位置は？",
                    Arguments = new()
                    {
                        ["starting"] = "開始",
                        ["goal"] = "ゴール",
                    },
                },
            },
        },

        // Cream Cipher
        [typeof(SCreamCipher)] = new()
        {
            ModuleName = "鳥子色暗号",
            Questions = new()
            {
                [SCreamCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Cream Cipher?
                    Question = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Creation
        [typeof(SCreation)] = new()
        {
            ModuleName = "クリエーション",
            Questions = new()
            {
                [SCreation.Weather] = new()
                {
                    // English: What were the weather conditions on the {1} day in {0}?
                    // Example: What were the weather conditions on the first day in Creation?
                    Question = "{0}の{1}日目における天気は？",
                    Answers = new()
                    {
                        ["Clear"] = "晴れ",
                        ["Heat Wave"] = "猛暑",
                        ["Meteor Shower"] = "流星群",
                        ["Rain"] = "雨",
                        ["Windy"] = "強風",
                    },
                },
            },
        },

        // Crimson Cipher
        [typeof(SCrimsonCipher)] = new()
        {
            ModuleName = "紅色暗号",
            Questions = new()
            {
                [SCrimsonCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Crimson Cipher?
                    Question = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Critters
        [typeof(SCritters)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCritters.Color] = new()
                {
                    // English: What was the color in {0}?
                    Question = "{0}で使用した変化した色は？",
                    Answers = new()
                    {
                        ["Yellow"] = "黄",
                        ["Pink"] = "ピンク",
                        ["Blue"] = "青",
                        ["White"] = "白",
                    },
                },
            },
        },

        // Cruel Binary
        [typeof(SCruelBinary)] = new()
        {
            ModuleName = "残忍二進数",
            Questions = new()
            {
                [SCruelBinary.DisplayedWord] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "{0}で表示された単語は？",
                },
            },
        },

        // Cruel Keypads
        [typeof(SCruelKeypads)] = new()
        {
            ModuleName = "残忍キーパッド",
            Questions = new()
            {
                [SCruelKeypads.Colors] = new()
                {
                    // English: What was the color of the bar in the {1} stage of {0}?
                    // Example: What was the color of the bar in the first stage of Cruel Keypads?
                    Question = "{0}のステージ{1}におけるバーの色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Magenta"] = "マゼンタ",
                        ["White"] = "白",
                    },
                },
                [SCruelKeypads.DisplayedSymbols] = new()
                {
                    // English: Which of these characters appeared in the {1} stage of {0}?
                    // Example: Which of these characters appeared in the first stage of Cruel Keypads?
                    Question = "{0}のステージ{1}で表示された文字に含まれるのは？",
                },
            },
        },

        // The cRule
        [typeof(SCRule)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCRule.SymbolPair] = new()
                {
                    // English: Which symbol pair was here in {0}? (+ sprite)
                    Question = "{0}のこの位置にあったシンボルのペアは？",
                },
                [SCRule.SymbolPairCell] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was ♤♤ in The cRule?
                    Question = "{0}で{1}はどこにあった？",
                },
                [SCRule.SymbolPairPresent] = new()
                {
                    // English: Which symbol pair was present on {0}?
                    Question = "{0}に存在していたシンボルのペアは？",
                },
                [SCRule.Prefilled] = new()
                {
                    // English: Which cell was pre-filled at the start of {0}?
                    Question = "{0}の開始時点でどのセルが予め埋められていた？",
                },
            },
        },

        // Cryptic Cycle
        [typeof(SCrypticCycle)] = new()
        {
            ModuleName = "暗号サイクル",
            Questions = new()
            {
                [SCrypticCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Cryptic Cycle?
                    Question = "{0}で{1}のダイヤルはどの方向を指していた？",
                },
                [SCrypticCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Cryptic Cycle?
                    Question = "{0}で{1}のダイヤルに書かれていた文字は？",
                },
            },
            Discriminators = new()
            {
                [SCrypticCycle.LabelDiscriminator] = new()
                {
                    // English: the Cryptic Cycle that had the letter {0} on a dial
                    // Example: the Cryptic Cycle that had the letter A on a dial
                    Discriminator = "ダイヤルに{0}が書かれていた暗号サイクル",
                },
            },
        },

        // Cryptic Keypad
        [typeof(SCrypticKeypad)] = new()
        {
            ModuleName = "暗号化キーパッド",
            Questions = new()
            {
                [SCrypticKeypad.Labels] = new()
                {
                    // English: What was the label of the {1} key in {0}?
                    // Example: What was the label of the top-left key in Cryptic Keypad?
                    Question = "{0}で{1}のキーパッドのラベルは？",
                    Arguments = new()
                    {
                        ["top-left"] = "左上",
                        ["top-right"] = "右上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                },
                [SCrypticKeypad.Rotations] = new()
                {
                    // English: Which cardinal direction was the {1} key rotated to in {0}?
                    // Example: Which cardinal direction was the top-left key rotated to in Cryptic Keypad?
                    Question = "{0}で{1}のキーパッドの回転方向は？",
                    Arguments = new()
                    {
                        ["top-left"] = "左上",
                        ["top-right"] = "右上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                    Answers = new()
                    {
                        ["North"] = "北",
                        ["East"] = "東",
                        ["South"] = "南",
                        ["West"] = "西",
                    },
                },
            },
        },

        // The Cube
        [typeof(SCube)] = new()
        {
            ModuleName = "キューブ",
            Questions = new()
            {
                [SCube.Rotations] = new()
                {
                    // English: What was the {1} cube rotation in {0}?
                    // Example: What was the first cube rotation in The Cube?
                    Question = "{0}の{1}回目のキューブの回転は？",
                    Answers = new()
                    {
                        ["rotate cw"] = "時計回り",
                        ["tip left"] = "左回転",
                        ["tip backwards"] = "上回転",
                        ["rotate ccw"] = "反時計回り",
                        ["tip right"] = "右回転",
                        ["tip forwards"] = "下回転",
                    },
                },
            },
        },

        // Cursed Double-Oh
        [typeof(SCursedDoubleOh)] = new()
        {
            ModuleName = "呪いのダブル・オー",
            Questions = new()
            {
                [SCursedDoubleOh.InitialPosition] = new()
                {
                    // English: What was the first digit of the initially displayed number in {0}?
                    Question = "{0}の初期状態に表示されていた上一桁は？",
                },
            },
        },

        // Customer Identification
        [typeof(SCustomerIdentification)] = new()
        {
            ModuleName = "顧客識別",
            Questions = new()
            {
                [SCustomerIdentification.Customer] = new()
                {
                    // English: Who was the {1} customer in {0}?
                    // Example: Who was the first customer in Customer Identification?
                    Question = "{0}の{1}番目のお客さんは誰？",
                },
            },
        },

        // The Cyan Button
        [typeof(SCyanButton)] = new()
        {
            ModuleName = "シアンボタン",
            Questions = new()
            {
                [SCyanButton.Positions] = new()
                {
                    // English: Where was the button at the {1} stage in {0}?
                    // Example: Where was the button at the first stage in The Cyan Button?
                    Question = "{0}のステージ{1}のボタンはどこにあった？",
                    Answers = new()
                    {
                        ["top left"] = "左上",
                        ["top middle"] = "上",
                        ["top right"] = "右",
                        ["bottom left"] = "左下",
                        ["bottom middle"] = "下",
                        ["bottom right"] = "右下",
                    },
                },
            },
        },

        // DACH Maze
        [typeof(SDACHMaze)] = new()
        {
            ModuleName = "DACH迷路",
            Questions = new()
            {
                [SDACHMaze.Origin] = new()
                {
                    // English: Which region did you depart from in {0}?
                    Question = "{0}の出発点は？",
                    Answers = new()
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
            },
        },

        // Deaf Alley
        [typeof(SDeafAlley)] = new()
        {
            ModuleName = "デフ・アレイ",
            Questions = new()
            {
                [SDeafAlley.Shape] = new()
                {
                    // English: What was the shape generated in {0}?
                    Question = "{0}で生成された文字は？",
                },
            },
        },

        // The Deck of Many Things
        [typeof(SDeckOfManyThings)] = new()
        {
            ModuleName = "多種デッキ",
            Questions = new()
            {
                [SDeckOfManyThings.FirstCard] = new()
                {
                    // English: What deck did the first card of {0} belong to?
                    Question = "{0}の最初のカードが属していたデッキは？",
                    Answers = new()
                    {
                        ["Standard"] = "標準",
                        ["Metropolitan"] = "都市",
                        ["Maritime"] = "海上",
                        ["Arctic"] = "北極",
                        ["Tropical"] = "熱帯",
                        ["Oasis"] = "オアシス",
                        ["Celestial"] = "天体",
                    },
                },
            },
        },

        // Decolored Squares
        [typeof(SDecoloredSquares)] = new()
        {
            ModuleName = "色抜き格子",
            Questions = new()
            {
                [SDecoloredSquares.StartingPos] = new()
                {
                    // English: What was the starting {1} defining color in {0}?
                    // Example: What was the starting column defining color in Decolored Squares?
                    Question = "{0}の開始位置の{1}は何色？",
                    Arguments = new()
                    {
                        ["column"] = "列",
                        ["row"] = "段",
                    },
                    Answers = new()
                    {
                        ["White"] = "白",
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                        ["Magenta"] = "マゼンタ",
                    },
                },
            },
        },

        // Decolour Flash
        [typeof(SDecolourFlash)] = new()
        {
            ModuleName = "デカラーフラッシュ",
            Questions = new()
            {
                [SDecolourFlash.Goal] = new()
                {
                    // English: What was the {1} of the {2} goal in {0}?
                    // Example: What was the colour of the first goal in Decolour Flash?
                    Question = "{0}で{2}番目のゴールの{1}は？",
                    Arguments = new()
                    {
                        ["colour"] = "色",
                        ["word"] = "単語",
                    },
                    Answers = new()
                    {
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Red"] = "赤",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                        ["White"] = "白",
                    },
                },
            },
        },

        // Denial Displays
        [typeof(SDenialDisplays)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDenialDisplays.Displays] = new()
                {
                    // English: What number was initially shown on display {1} in {0}?
                    // Example: What number was initially shown on display A in Denial Displays?
                    Question = "{0}のディスプレー{1}に最初表示されていた数字は？",
                },
            },
        },

        // DetoNATO
        [typeof(SDetoNATO)] = new()
        {
            ModuleName = "デトナト",
            Questions = new()
            {
                [SDetoNATO.Display] = new()
                {
                    // English: What was the {1} display in {0}?
                    // Example: What was the first display in DetoNATO?
                    Question = "{0}で{1}番目に表示されていた内容は？",
                },
            },
        },

        // Devilish Eggs
        [typeof(SDevilishEggs)] = new()
        {
            ModuleName = "悪魔の卵",
            Questions = new()
            {
                [SDevilishEggs.Rotations] = new()
                {
                    // English: What was the {1} egg’s {2} rotation in {0}?
                    // Example: What was the top egg’s first rotation in Devilish Eggs?
                    Question = "{0}で{1}の卵の{2}回目の回転は？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["bottom"] = "下",
                    },
                },
                [SDevilishEggs.Numbers] = new()
                {
                    // English: What was the {1} digit in the string of numbers on {0}?
                    // Example: What was the first digit in the string of numbers on Devilish Eggs?
                    Question = "{0}の数列の{1}桁目は？",
                },
                [SDevilishEggs.Letters] = new()
                {
                    // English: What was the {1} letter in the string of letters on {0}?
                    // Example: What was the first letter in the string of letters on Devilish Eggs?
                    Question = "{0}の英字文字列の{1}文字目は？",
                },
            },
        },

        // Dialtones
        [typeof(SDialtones)] = new()
        {
            ModuleName = "ダイヤル音声",
            Questions = new()
            {
                [SDialtones.Dialtones] = new()
                {
                    // English: What dialtones were heard in {0}?
                    Question = "{0}で聞こえたダイヤルトーンは？",
                },
            },
        },

        // Digisibility
        [typeof(SDigisibility)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDigisibility.DisplayedNumber] = new()
                {
                    // English: What was the number on the {1} button in {0}?
                    // Example: What was the number on the first button in Digisibility?
                    Question = "{0}の{1}番目のボタンに書かれた数字は？",
                },
            },
        },

        // Digit String
        [typeof(SDigitString)] = new()
        {
            ModuleName = "数字列",
            Questions = new()
            {
                [SDigitString.InitialNumber] = new()
                {
                    // English: What was the initial number in {0}?
                    Question = "{0}の初期値は？",
                },
            },
        },

        // Dimension Disruption
        [typeof(SDimensionDisruption)] = new()
        {
            ModuleName = "次元破壊",
            Questions = new()
            {
                [SDimensionDisruption.VisibleLetters] = new()
                {
                    // English: Which of these was a visible character in {0}?
                    Question = "{0}で見えていた文字は次のうちどれ？",
                },
            },
        },

        // Directional Button
        [typeof(SDirectionalButton)] = new()
        {
            ModuleName = "方向ボタン",
            Questions = new()
            {
                [SDirectionalButton.ButtonCount] = new()
                {
                    // English: How many times did you press the button in the {1} stage of {0}?
                    // Example: How many times did you press the button in the first stage of Directional Button?
                    Question = "{0}のステージ{1}で押したボタンの回数は？",
                },
            },
        },

        // Discolored Squares
        [typeof(SDiscoloredSquares)] = new()
        {
            ModuleName = "色変え格子",
            Questions = new()
            {
                [SDiscoloredSquares.RememberedPositions] = new()
                {
                    // English: What was {1}’s remembered position in {0}?
                    // Example: What was Blue’s remembered position in Discolored Squares?
                    Question = "{0}で{1}の覚えた位置は？",
                    Arguments = new()
                    {
                        ["Blue"] = "青",
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Magenta"] = "マゼンタ",
                    },
                },
            },
        },

        // Disordered Keys
        [typeof(SDisorderedKeys)] = new()
        {
            ModuleName = "欠陥順番音板",
            Questions = new()
            {
                [SDisorderedKeys.MissingInfo] = new()
                {
                    // English: What was the missing information for the {1} key in {0}?
                    // Example: What was the missing information for the first key in Disordered Keys?
                    Question = "{0}の{1}番目の音板に欠けていた情報は？",
                    Answers = new()
                    {
                        ["Key color"] = "音板の色",
                        ["Label color"] = "ラベルの色",
                        ["Label"] = "ラベル",
                    },
                },
                [SDisorderedKeys.UnrevealedKeyColor] = new()
                {
                    // English: What was the unrevealed key color for the {1} key in {0}?
                    // Example: What was the unrevealed key color for the first key in Disordered Keys?
                    Question = "{0}の{1}番目の不完全な音板のラベルの色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                    },
                },
                [SDisorderedKeys.UnrevealedLabelColor] = new()
                {
                    // English: What was the unrevealed label color for the {1} key in {0}?
                    // Example: What was the unrevealed label color for the first key in Disordered Keys?
                    Question = "{0}の{1}番目の不完全な音板のラベルの色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                    },
                },
                [SDisorderedKeys.UnrevealedKeyLabel] = new()
                {
                    // English: What was the unrevealed label for the {1} key in {0}?
                    // Example: What was the unrevealed label for the first key in Disordered Keys?
                    Question = "{0}の{1}番目の不完全な音板のラベルは？",
                },
                [SDisorderedKeys.RevealedKeyColor] = new()
                {
                    // English: What was the revealed key color for the {1} key in {0}?
                    // Example: What was the revealed key color for the first key in Disordered Keys?
                    Question = "{0}の{1}番目の完全な音板の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                    },
                },
                [SDisorderedKeys.RevealedLabelColor] = new()
                {
                    // English: What was the revealed label color for the {1} key in {0}?
                    // Example: What was the revealed label color for the first key in Disordered Keys?
                    Question = "{0}の{1}番目の完全な音板のラベルの色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                    },
                },
                [SDisorderedKeys.RevealedLabel] = new()
                {
                    // English: What was the revealed label for the {1} key in {0}?
                    // Example: What was the revealed label for the first key in Disordered Keys?
                    Question = "{0}の{1}番目の完全な音板のラベルは？",
                },
            },
        },

        // Divided Squares
        [typeof(SDividedSquares)] = new()
        {
            ModuleName = "分割スクエア",
            Questions = new()
            {
                [SDividedSquares.Color] = new()
                {
                    // English: What color was {1} while pressing it in {0}?
                    // Example: What color was the square while pressing it in Divided Squares?
                    Question = "{0}で{1}を押している間の色は？",
                    Arguments = new()
                    {
                        ["the square"] = "正方形",
                        ["the correct square"] = "正しい正方形",
                    },
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Black"] = "黒",
                        ["White"] = "白",
                    },
                },
            },
        },

        // Divisible Numbers
        [typeof(SDivisibleNumbers)] = new()
        {
            ModuleName = "割り切れる数字",
            Questions = new()
            {
                [SDivisibleNumbers.Numbers] = new()
                {
                    // English: What was the {1} stage’s number in {0}?
                    // Example: What was the first stage’s number in Divisible Numbers?
                    Question = "{0}でのステージ{1}の数字は？",
                },
            },
        },

        // DNA Mutation
        [typeof(SDNAMutation)] = new()
        {
            NeedsTranslation = true,
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
                },
                [SDNAMutation.StrandColor] = new()
                {
                    // English: What was the color of the {1} given DNA strand in {0}?
                    // Example: What was the color of the first given DNA strand in DNA Mutation?
                    Question = "What was the color of the {1} given DNA strand in {0}?",
                },
            },
        },

        // Doofenshmirtz Evil Inc.
        [typeof(SDoofenshmirtzEvilInc)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SDoofenshmirtzEvilInc.Jingles] = new()
                {
                    // English: What jingle played in {0}?
                    Question = "{0}で流れたジングルは？",
                },
                [SDoofenshmirtzEvilInc.Inators] = new()
                {
                    // English: Which image was shown in {0}?
                    Question = "{0}で表示された画像は？",
                },
            },
        },

        // Double Arrows
        [typeof(SDoubleArrows)] = new()
        {
            ModuleName = "ダブル矢印",
            Questions = new()
            {
                [SDoubleArrows.Start] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "{0}の開始位置は？",
                },
                [SDoubleArrows.Movement] = new()
                {
                    // English: Which direction in the grid did the {1} arrow move in {0}?
                    // Example: Which direction in the grid did the inner up arrow move in Double Arrows?
                    Question = "{0}で{1}矢印を押すとどの方向に進んだ？",
                    Arguments = new()
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
                    Answers = new()
                    {
                        ["Up"] = "上",
                        ["Right"] = "右",
                        ["Left"] = "左",
                        ["Down"] = "下",
                    },
                },
                [SDoubleArrows.Arrow] = new()
                {
                    // English: Which {1} arrow moved {2} in the grid in {0}?
                    // Example: Which inner arrow moved up in the grid in Double Arrows?
                    Question = "{0}で{2}に移動する{1}の矢印はどれ？",
                    Arguments = new()
                    {
                        ["inner"] = "内側",
                        ["outer"] = "外側",
                        ["up"] = "上",
                        ["down"] = "下",
                        ["left"] = "左",
                        ["right"] = "右",
                    },
                    Answers = new()
                    {
                        ["Up"] = "上",
                        ["Right"] = "右",
                        ["Left"] = "左",
                        ["Down"] = "下",
                    },
                },
            },
        },

        // Double Color
        [typeof(SDoubleColor)] = new()
        {
            ModuleName = "二色",
            Questions = new()
            {
                [SDoubleColor.Colors] = new()
                {
                    // English: What was the screen color on the {1} stage of {0}?
                    // Example: What was the screen color on the first stage of Double Color?
                    Question = "{0}でのステージ{1}の画面の色は？",
                    Answers = new()
                    {
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Red"] = "赤",
                        ["Pink"] = "ピンク",
                        ["Yellow"] = "黄",
                    },
                },
            },
        },

        // Double Digits
        [typeof(SDoubleDigits)] = new()
        {
            ModuleName = "二桁",
            Questions = new()
            {
                [SDoubleDigits.Displays] = new()
                {
                    // English: What was the digit on the {1} display in {0}?
                    // Example: What was the digit on the left display in Double Digits?
                    Question = "{0}の{1}の画面上の数字は？",
                    Arguments = new()
                    {
                        ["left"] = "左",
                        ["right"] = "右",
                    },
                },
            },
        },

        // Double Expert
        [typeof(SDoubleExpert)] = new()
        {
            ModuleName = "ダブル・エキスパート",
            Questions = new()
            {
                [SDoubleExpert.StartingKeyNumber] = new()
                {
                    // English: What was the starting key number in {0}?
                    Question = "{0}の初期キー番号は？",
                },
                [SDoubleExpert.SubmittedWord] = new()
                {
                    // English: What was the word you submitted in {0}?
                    Question = "{0}で送信した単語は？",
                },
            },
        },

        // Double Listening
        [typeof(SDoubleListening)] = new()
        {
            ModuleName = "ダブルリスニング",
            Questions = new()
            {
                [SDoubleListening.Sounds] = new()
                {
                    // English: What clip was played in {0}?
                    Question = "{0}で再生されたクリップは？",
                },
            },
        },

        // Double-Oh
        [typeof(SDoubleOh)] = new()
        {
            ModuleName = "ダブル・オー",
            Questions = new()
            {
                [SDoubleOh.SubmitButton] = new()
                {
                    // English: Which button was the submit button in {0}?
                    Question = "{0}の送信ボタンは？",
                },
            },
        },

        // Double Screen
        [typeof(SDoubleScreen)] = new()
        {
            ModuleName = "二画面",
            Questions = new()
            {
                [SDoubleScreen.Colors] = new()
                {
                    // English: What color was the {1} screen in the {2} stage of {0}?
                    // Example: What color was the top screen in the first stage of Double Screen?
                    Question = "{0}でステージ{2}の{1}画面の色は？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["bottom"] = "下",
                    },
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                    },
                },
            },
        },

        // Dr. Doctor
        [typeof(SDrDoctor)] = new()
        {
            ModuleName = "医学博士",
            Questions = new()
            {
                [SDrDoctor.Diseases] = new()
                {
                    // English: Which of these diseases was listed on {0}, but not the one treated?
                    Question = "{0}に存在したが治療しなかった病気はどれ？",
                },
                [SDrDoctor.Symptoms] = new()
                {
                    // English: Which of these symptoms was listed on {0}?
                    Question = "{0}に存在した症状に含まれるのはどれ？",
                },
            },
        },

        // Dreamcipher
        [typeof(SDreamcipher)] = new()
        {
            ModuleName = "夢想暗号",
            Questions = new()
            {
                [SDreamcipher.Word] = new()
                {
                    // English: What was the decrypted word in {0}?
                    Question = "{0}で解読した単語は？",
                },
            },
        },

        // The Duck
        [typeof(SDuck)] = new()
        {
            ModuleName = "アヒル",
            Questions = new()
            {
                [SDuck.CurtainColor] = new()
                {
                    // English: What was the color of the curtain in {0}?
                    Question = "{0}のカーテンの色は？",
                    Answers = new()
                    {
                        ["blue"] = "青",
                        ["yellow"] = "黄",
                        ["green"] = "緑",
                        ["orange"] = "橙",
                        ["red"] = "赤",
                    },
                },
            },
        },

        // Dumb Waiters
        [typeof(SDumbWaiters)] = new()
        {
            ModuleName = "小荷物専用昇降機",
            Questions = new()
            {
                [SDumbWaiters.PlayerAvailable] = new()
                {
                    // English: Which player {1} present in {0}?
                    // Example: Which player was present in Dumb Waiters?
                    Question = "{0}に存在して{1}プレイヤーは？",
                    Arguments = new()
                    {
                        ["was"] = "いた",
                        ["was not"] = "いなかった",
                    },
                },
            },
        },

        // Earthbound
        [typeof(SEarthbound)] = new()
        {
            ModuleName = "MOTHER",
            Questions = new()
            {
                [SEarthbound.Background] = new()
                {
                    // English: What was the background in {0}?
                    Question = "{0}の背景の数字は？",
                },
                [SEarthbound.Monster] = new()
                {
                    // English: Which monster was displayed in {0}?
                    Question = "{0}で表示されたモンスターは？",
                },
            },
        },

        // eeB gnillepS
        [typeof(SEeBgnillepS)] = new()
        {
            ModuleName = "ービ・グンリペス",
            Questions = new()
            {
                [SEeBgnillepS.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "{0}で綴るように求められた単語は？",
                },
            },
        },

        // Eight
        [typeof(SEight)] = new()
        {
            ModuleName = "八",
            Questions = new()
            {
                [SEight.LastSmallDisplayDigit] = new()
                {
                    // English: What was the last digit on the small display in {0}?
                    Question = "{0}の小さなディスプレーの下一桁は？",
                },
                [SEight.LastBrokenDigitPosition] = new()
                {
                    // English: What was the position of the last broken digit in {0}?
                    Question = "{0}の最後に壊された桁の位置は？",
                },
                [SEight.LastResultingDigits] = new()
                {
                    // English: What were the last resulting digits in {0}?
                    Question = "{0}の最終的な数字は？",
                },
                [SEight.LastDisplayedNumber] = new()
                {
                    // English: What was the last displayed number in {0}?
                    Question = "{0}の最後に表示された数字は？",
                },
            },
        },

        // Elder Futhark
        [typeof(SElderFuthark)] = new()
        {
            ModuleName = "エルダー・フサルク",
            Questions = new()
            {
                [SElderFuthark.Runes] = new()
                {
                    // English: What was the {1} rune shown on {0}?
                    // Example: What was the first rune shown on Elder Futhark?
                    Question = "{0}の{1}番目に表示されたルーンは？",
                },
            },
        },

        // Emoji
        [typeof(SEmoji)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEmoji.Emoji] = new()
                {
                    // English: What was the {1} emoji in {0}?
                    // Example: What was the left emoji in Emoji?
                    Question = "{0}の{1}の絵文字は？",
                    Arguments = new()
                    {
                        ["left"] = "左",
                        ["right"] = "右",
                    },
                },
            },
        },

        // ƎNA Cipher
        [typeof(SEnaCipher)] = new()
        {
            ModuleName = "エナ暗号",
            Questions = new()
            {
                [SEnaCipher.KeywordAnswer] = new()
                {
                    // English: What was the {1} keyword in {0}?
                    // Example: What was the first keyword in ƎNA Cipher?
                    Question = "{0}の{1}番目のキーワードは？",
                },
                [SEnaCipher.ExtAnswer] = new()
                {
                    // English: What was the transposition key in {0}?
                    Question = "{0}の転移キーは？",
                },
                [SEnaCipher.EncryptedAnswer] = new()
                {
                    // English: What was the encrypted word in {0}?
                    Question = "{0}で解読した単語は？",
                },
            },
        },

        // Encrypted Dice
        [typeof(SEncryptedDice)] = new()
        {
            ModuleName = "暗号化ダイス",
            Questions = new()
            {
                [SEncryptedDice.Question] = new()
                {
                    // English: Which of these numbers appeared on a die in the {1} stage of {0}?
                    // Example: Which of these numbers appeared on a die in the first stage of Encrypted Dice?
                    Question = "{0}のステージ{1}で表示された目に含まれるのは？",
                },
            },
        },

        // Encrypted Equations
        [typeof(SEncryptedEquations)] = new()
        {
            ModuleName = "暗号化方程式",
            Questions = new()
            {
                [SEncryptedEquations.Shapes] = new()
                {
                    // English: Which shape was the {1} operand in {0}?
                    // Example: Which shape was the first operand in Encrypted Equations?
                    Question = "{0}の{1}の演算子の図形は？",
                },
            },
        },

        // Encrypted Hangman
        [typeof(SEncryptedHangman)] = new()
        {
            ModuleName = "暗号化ハングマン",
            Questions = new()
            {
                [SEncryptedHangman.Module] = new()
                {
                    // English: What module name was encrypted by {0}?
                    Question = "{0}で暗号化されていたモジュール名は？",
                },
                [SEncryptedHangman.EncryptionMethod] = new()
                {
                    // English: What method of encryption was used by {0}?
                    Question = "{0}で使われた暗号化方式は？",
                    Answers = new()
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
            },
        },

        // Encrypted Maze
        [typeof(SEncryptedMaze)] = new()
        {
            ModuleName = "暗号化迷路",
            Questions = new()
            {
                [SEncryptedMaze.Symbols] = new()
                {
                    // English: Which symbol on {0} was spinning {1}?
                    // Example: Which symbol on Encrypted Maze was spinning clockwise?
                    Question = "{0}で{1}に回転していたシンボルは？",
                    Arguments = new()
                    {
                        ["clockwise"] = "時計回り",
                        ["counter-clockwise"] = "反時計回り",
                    },
                },
            },
        },

        // Encrypted Morse
        [typeof(SEncryptedMorse)] = new()
        {
            ModuleName = "暗号化モールス信号",
            Questions = new()
            {
                [SEncryptedMorse.CallResponse] = new()
                {
                    // English: What was the {1} on {0}?
                    // Example: What was the received call on Encrypted Morse?
                    Question = "{0}の{1}は？",
                    Arguments = new()
                    {
                        ["received call"] = "受信した信号",
                        ["sent response"] = "送信した返答",
                    },
                },
            },
        },

        // Encryption Bingo
        [typeof(SEncryptionBingo)] = new()
        {
            ModuleName = "暗号化ビンゴ",
            Questions = new()
            {
                [SEncryptionBingo.Encoding] = new()
                {
                    // English: What was the first encoding used in {0}?
                    Question = "{0}の最初の復号方式は？",
                    Answers = new()
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
                        ["Galactic Alphabet"] = "銀河標準語",
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
            },
        },

        // English Entries
        [typeof(SEnglishEntries)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SEnglishEntries.Display] = new()
                {
                    // English: What was the displayed quote on {0}?
                    Question = "{0}で表示された引用文は？",
                },
            },
        },

        // Enigma Cycle
        [typeof(SEnigmaCycle)] = new()
        {
            ModuleName = "エニグマサイクル",
            Questions = new()
            {
                [SEnigmaCycle.DialDirectionsThree] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Enigma Cycle?
                    Question = "{0}の{1}番目のダイヤルが指していた方向は？",
                },
                [SEnigmaCycle.DialDirectionsTwelve] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Enigma Cycle?
                    Question = "{0}の{1}番目のダイヤルが指していた方向は？",
                },
                [SEnigmaCycle.DialDirectionsEight] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Enigma Cycle?
                    Question = "{0}の{1}番目のダイヤルが指していた方向は？",
                },
                [SEnigmaCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Enigma Cycle?
                    Question = "{0}の{1}番目のダイヤルに書かれていた文字は？",
                },
            },
            Discriminators = new()
            {
                [SEnigmaCycle.LabelDiscriminator] = new()
                {
                    // English: the Enigma Cycle that had the letter {0} on a dial
                    // Example: the Enigma Cycle that had the letter A on a dial
                    Discriminator = "{0}の文字がダイヤルに書かれていたエニグマサイクル",
                },
            },
        },

        // Entry Number Four
        [typeof(SEntryNumberFour)] = new()
        {
            ModuleName = "エントリーナンバー4",
            Questions = new()
            {
                [SEntryNumberFour.Digits] = new()
                {
                    // English: What was the {1} digit in the {2} number shown in {0}?
                    // Example: What was the first digit in the first number shown in Entry Number Four?
                    Question = "{0}で表示された{2}番目の数字の{1}桁目は？",
                },
            },
        },

        // Entry Number One
        [typeof(SEntryNumberOne)] = new()
        {
            ModuleName = "エントリーナンバー1",
            Questions = new()
            {
                [SEntryNumberOne.Digits] = new()
                {
                    // English: What was the {1} digit in the {2} number shown in {0}?
                    // Example: What was the first digit in the first number shown in Entry Number One?
                    Question = "{0}で表示された{2}番目の数字の{1}桁目は？",
                },
            },
        },

        // Equations X
        [typeof(SEquationsX)] = new()
        {
            ModuleName = "方程式X",
            Questions = new()
            {
                [SEquationsX.Symbols] = new()
                {
                    // English: What was the displayed symbol in {0}?
                    Question = "{0}に表示された記号は？",
                },
            },
        },

        // Error Codes
        [typeof(SErrorCodes)] = new()
        {
            ModuleName = "エラーコード",
            Questions = new()
            {
                [SErrorCodes.ActiveError] = new()
                {
                    // English: What was the active error code in {0}?
                    Question = "{0}で有効だったエラーコードは？",
                },
            },
        },

        // Etterna
        [typeof(SEtterna)] = new()
        {
            ModuleName = "エテルナ",
            Questions = new()
            {
                [SEtterna.Number] = new()
                {
                    // English: What was the beat for the {1} arrow from the bottom in {0}?
                    // Example: What was the beat for the first arrow from the bottom in Etterna?
                    Question = "{0}の下から{1}番目の矢印のビートは？",
                },
            },
        },

        // Exoplanets
        [typeof(SExoplanets)] = new()
        {
            ModuleName = "太陽系外惑星",
            Questions = new()
            {
                [SExoplanets.StartingTargetPlanet] = new()
                {
                    // English: What was the starting target planet in {0}?
                    Question = "{0}の開始ターゲット惑星は？",
                    Answers = new()
                    {
                        ["outer"] = "外側",
                        ["middle"] = "中央",
                        ["inner"] = "内側",
                        ["none"] = "なし",
                    },
                },
                [SExoplanets.StartingTargetDigit] = new()
                {
                    // English: What was the starting target digit in {0}?
                    Question = "{0}の開始ターゲット値は？",
                },
                [SExoplanets.TargetPlanet] = new()
                {
                    // English: What was the final target planet in {0}?
                    Question = "{0}の最終ターゲット惑星は？",
                    Answers = new()
                    {
                        ["outer"] = "外側",
                        ["middle"] = "中央",
                        ["inner"] = "内側",
                        ["none"] = "なし",
                    },
                },
                [SExoplanets.TargetDigit] = new()
                {
                    // English: What was the final target digit in {0}?
                    Question = "{0}の最終ターゲット値は？",
                },
            },
        },

        // Factoring Maze
        [typeof(SFactoringMaze)] = new()
        {
            ModuleName = "因数迷路",
            Questions = new()
            {
                [SFactoringMaze.ChosenPrimes] = new()
                {
                    // English: What was one of the prime numbers chosen in {0}?
                    Question = "{0}で選ばれた素因数の一つにあるのはどれ？",
                },
            },
        },

        // Factory Maze
        [typeof(SFactoryMaze)] = new()
        {
            ModuleName = "工場迷路",
            Questions = new()
            {
                [SFactoryMaze.StartRoom] = new()
                {
                    // English: What room did you start in in {0}?
                    Question = "{0}の開始場所の部屋は？",
                },
            },
        },

        // Faerie Fires
        [typeof(SFaerieFires)] = new()
        {
            ModuleName = "妖精の火",
            Questions = new()
            {
                [SFaerieFires.Color] = new()
                {
                    // English: What color was the {1} faerie in {0}?
                    // Example: What color was the first faerie in Faerie Fires?
                    Question = "{0}の{1}番目の妖精の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                    },
                },
                [SFaerieFires.PitchOrdinal] = new()
                {
                    // English: What pitch did the {1} faerie sing in {0}?
                    // Example: What pitch did the first faerie sing in Faerie Fires?
                    Question = "{0}の{1}番目の妖精が歌った音の高さは？",
                },
                [SFaerieFires.PitchColor] = new()
                {
                    // English: What pitch did the {1} faerie sing in {0}?
                    // Example: What pitch did the red faerie sing in Faerie Fires?
                    Question = "{0}の{1}の妖精が歌った音の高さは？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["yellow"] = "黄",
                        ["cyan"] = "シアン",
                        ["magenta"] = "マゼンタ",
                    },
                },
            },
        },

        // Fast Math
        [typeof(SFastMath)] = new()
        {
            ModuleName = "速算",
            Questions = new()
            {
                [SFastMath.LastLetters] = new()
                {
                    // English: What was the last pair of letters in {0}?
                    Question = "{0}の最後の英字のペアは？",
                },
            },
        },

        // Fast Playfair Cipher
        [typeof(SFastPlayfairCipher)] = new()
        {
            ModuleName = "速プレイフェア暗号",
            Questions = new()
            {
                [SFastPlayfairCipher.LastMessage] = new()
                {
                    // English: What was the last displayed message in {0}?
                    Question = "{0}で最後に表示されたメッセージは？",
                },
            },
        },

        // Faulty Buttons
        [typeof(SFaultyButtons)] = new()
        {
            ModuleName = "欠陥ボタン",
            Questions = new()
            {
                [SFaultyButtons.ReferredToThisButton] = new()
                {
                    // English: Which button referred to this button in {0}? (+ sprite)
                    // Example: Which button referred to this button in Faulty Buttons? (+ sprite)
                    Question = "{0}でどのボタンがこのボタンを指していた？",
                },
                [SFaultyButtons.ThisButtonReferredTo] = new()
                {
                    // English: Which button did this button refer to in {0}? (+ sprite)
                    // Example: Which button did this button refer to in Faulty Buttons? (+ sprite)
                    Question = "{0}でこのボタンはどのボタンを指していた？",
                },
            },
        },

        // Faulty RGB Maze
        [typeof(SFaultyRGBMaze)] = new()
        {
            ModuleName = "欠陥RGB迷路",
            Questions = new()
            {
                [SFaultyRGBMaze.Keys] = new()
                {
                    // English: Where was the {1} key in {0}?
                    // Example: Where was the red key in Faulty RGB Maze?
                    Question = "{0}の{1}色の鍵はどこにあった？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
                [SFaultyRGBMaze.Number] = new()
                {
                    // English: Which maze number was the {1} maze in {0}?
                    // Example: Which maze number was the red maze in Faulty RGB Maze?
                    Question = "{0}の{1}色迷路の番号は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
                [SFaultyRGBMaze.Exit] = new()
                {
                    // English: What was the exit coordinate in {0}?
                    Question = "{0}の出口の座標は？",
                },
            },
        },

        // Find The Date
        [typeof(SFindTheDate)] = new()
        {
            ModuleName = "曜日の特定",
            Questions = new()
            {
                [SFindTheDate.Month] = new()
                {
                    // English: What was the month displayed in the {1} stage of {0}?
                    // Example: What was the month displayed in the first stage of Find The Date?
                    Question = "{0}のステージ{1}で表示された月は？",
                },
                [SFindTheDate.Day] = new()
                {
                    // English: What was the day displayed in the {1} stage of {0}?
                    // Example: What was the day displayed in the first stage of Find The Date?
                    Question = "{0}のステージ{1}で表示された日は？",
                },
                [SFindTheDate.Year] = new()
                {
                    // English: What was the year displayed in the {1} stage of {0}?
                    // Example: What was the year displayed in the first stage of Find The Date?
                    Question = "{0}のステージ{1}で表示された年は？",
                },
            },
        },

        // Five Letter Words
        [typeof(SFiveLetterWords)] = new()
        {
            ModuleName = "5文字の単語",
            Questions = new()
            {
                [SFiveLetterWords.DisplayedWords] = new()
                {
                    // English: Which of these words was on the display in {0}?
                    Question = "{0}に表示された単語に含まれるのは？",
                },
            },
        },

        // FizzBuzz
        [typeof(SFizzBuzz)] = new()
        {
            ModuleName = "フィズバズ",
            Questions = new()
            {
                [SFizzBuzz.DisplayedNumbers] = new()
                {
                    // English: What was the {1} digit on the {2} display of {0}?
                    // Example: What was the first digit on the top display of FizzBuzz?
                    Question = "{0}の{2}ディスプレーにあった数の{1}桁目は？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Flags
        [typeof(SFlags)] = new()
        {
            ModuleName = "国旗",
            Questions = new()
            {
                [SFlags.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "{0}で表示された数字は？",
                },
                [SFlags.MainCountry] = new()
                {
                    // English: What was the main country flag in {0}?
                    Question = "{0}のメイン国旗は？",
                },
                [SFlags.Countries] = new()
                {
                    // English: Which of these country flags was shown, but not the main country flag, in {0}?
                    Question = "{0}に表示されたメイン国旗以外の国旗は？",
                },
            },
        },

        // Flashing Arrows
        [typeof(SFlashingArrows)] = new()
        {
            ModuleName = "点滅矢印",
            Questions = new()
            {
                [SFlashingArrows.DisplayedValue] = new()
                {
                    // English: What number was displayed on {0}?
                    Question = "{0}に表示されていた数字は？",
                },
                [SFlashingArrows.ReferredArrow] = new()
                {
                    // English: What color flashed {1} black on the relevant arrow in {0}?
                    // Example: What color flashed before black on the relevant arrow in Flashing Arrows?
                    Question = "{0}で関連する矢印について黒色の{1}に点滅した色は？",
                    Arguments = new()
                    {
                        ["before"] = "前",
                        ["after"] = "後",
                    },
                    Answers = new()
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
            },
        },

        // Flashing Lights
        [typeof(SFlashingLights)] = new()
        {
            ModuleName = "点滅ライト",
            Questions = new()
            {
                [SFlashingLights.LEDFrequency] = new()
                {
                    // English: How many times did the {1} LED flash {2} on {0}?
                    // Example: How many times did the top LED flash cyan on Flashing Lights?
                    Question = "{0}で{1}のLEDは{2}色に何回光った？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["bottom"] = "下",
                        ["cyan"] = "シアン",
                        ["green"] = "緑",
                        ["red"] = "赤",
                        ["purple"] = "紫",
                        ["orange"] = "オレンジ",
                    },
                },
            },
        },

        // Flavor Text
        [typeof(SFlavorText)] = new()
        {
            ModuleName = "フレーバーテキスト",
            Questions = new()
            {
                [SFlavorText.Module] = new()
                {
                    // English: Which module’s flavor text was shown in {0}?
                    Question = "{0}で表示されたフレーバーテキストは、どのモジュールのもの(英名)？",
                },
            },
        },

        // Flavor Text EX
        [typeof(SFlavorTextEX)] = new()
        {
            ModuleName = "フレーバーテキストEX",
            Questions = new()
            {
                [SFlavorTextEX.Module] = new()
                {
                    // English: Which module’s flavor text was shown in the {1} stage of {0}?
                    // Example: Which module’s flavor text was shown in the first stage of Flavor Text EX?
                    Question = "{0}のステージ{1}で表示されたフレーバーテキストは、どのモジュールのもの(英名)？",
                },
            },
        },

        // Flyswatting
        [typeof(SFlyswatting)] = new()
        {
            ModuleName = "ハエ叩き",
            Questions = new()
            {
                [SFlyswatting.Unpressed] = new()
                {
                    // English: Which fly was present, but not in the solution in {0}?
                    Question = "{0}に存在したが、答えに含まれていないハエは？",
                },
            },
        },

        // Follow Me
        [typeof(SFollowMe)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SFollowMe.DisplayedPath] = new()
                {
                    // English: What was the {1} flashing direction in {0}?
                    // Example: What was the first flashing direction in Follow Me?
                    Question = "{0}で{1}番目に点滅した方向は？",
                    Answers = new()
                    {
                        ["Up"] = "上",
                        ["Down"] = "下",
                        ["Left"] = "左",
                        ["Right"] = "右",
                    },
                },
            },
        },

        // Forest Cipher
        [typeof(SForestCipher)] = new()
        {
            ModuleName = "柚葉色暗号",
            Questions = new()
            {
                [SForestCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Forest Cipher?
                    Question = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Forget Any Color
        [typeof(SForgetAnyColor)] = new()
        {
            ModuleName = "全色忘る",
            Questions = new()
            {
                [SForgetAnyColor.QCylinder] = new()
                {
                    // English: What colors were the cylinders during the {1} stage of {0}?
                    // Example: What colors were the cylinders during the first stage of Forget Any Color?
                    Question = "{0}のステージ{1}におけるシリンダーは？",
                    // Refer to translations.md to understand the weird strings
                    Additional = new()
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
                    },
                },
                [SForgetAnyColor.QFigure] = new()
                {
                    // English: Which figure was used during the {1} stage of {0}?
                    // Example: Which figure was used during the first stage of Forget Any Color?
                    Question = "{0}のステージ{1}で使用された図は？",
                },
            },
            Discriminators = new()
            {
                [SForgetAnyColor.DCylinder] = new()
                {
                    // English: the Forget Any Color whose cylinders in the {1} stage were {0}
                    // Example: the Forget Any Color whose cylinders in the first stage were Orange, Yellow, Green
                    Discriminator = "ステージ{0}のシリンダーに{1}があった全色忘る",
                },
                [SForgetAnyColor.DFigure] = new()
                {
                    // English: the Forget Any Color which used figure {0} in the {1} stage
                    // Example: the Forget Any Color which used figure LLLMR in the first stage
                    Discriminator = "ステージ{1}で図{0}を使用した全色忘る",
                },
            },
        },

        // Forget Everything
        [typeof(SForgetEverything)] = new()
        {
            ModuleName = "須く忘る",
            Questions = new()
            {
                [SForgetEverything.QStageOneDisplay] = new()
                {
                    // English: What was the {1} displayed digit in the first stage of {0}?
                    // Example: What was the first displayed digit in the first stage of Forget Everything?
                    Question = "{0}の最初のステージにあった数字の{1}桁目は？",
                },
            },
            Discriminators = new()
            {
                [SForgetEverything.DStageOneDisplay] = new()
                {
                    // English: the Forget Everything whose {0} displayed digit in that stage was {1}
                    // Example: the Forget Everything whose first displayed digit in that stage was 1
                    Discriminator = "最初のステージで{0}番目の数字が{1}だった須く忘る",
                },
            },
        },

        // Forget Me
        [typeof(SForgetMe)] = new()
        {
            ModuleName = "我忘る",
            Questions = new()
            {
                [SForgetMe.InitialState] = new()
                {
                    // English: What number was in the {1} position of the initial puzzle in {0}?
                    // Example: What number was in the top-left position of the initial puzzle in Forget Me?
                    Question = "{0}の初期状態のパズルにおける{1}の数字は？",
                    Arguments = new()
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
            },
        },

        // Forget Me Not
        [typeof(SForgetMeNot)] = new()
        {
            ModuleName = "我忘る勿かれ",
            Questions = new()
            {
                [SForgetMeNot.Question] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Forget Me Not?
                    Question = "{0}のステージ{1}で表示されていた数字は？",
                },
            },
            Discriminators = new()
            {
                [SForgetMeNot.Discriminator] = new()
                {
                    // English: the Forget Me Not which displayed a {0} in the {1} stage
                    // Example: the Forget Me Not which displayed a 1 in the first stage
                    Discriminator = "ステージ{1}の数字が{0}だった須く忘る",
                },
            },
        },

        // Forget Me Now
        [typeof(SForgetMeNow)] = new()
        {
            ModuleName = "我忘るる",
            Questions = new()
            {
                [SForgetMeNow.DisplayedDigits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Forget Me Now?
                    Question = "{0}の{1}番目に表示された数字は？",
                },
            },
        },

        // Forget Our Voices
        [typeof(SForgetOurVoices)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SForgetOurVoices.Voice] = new()
                {
                    // English: What was played in the {1} stage of {0}?
                    // Example: What was played in the first stage of Forget Our Voices?
                    Question = "{0}のステージ{1}で再生されたのは？",
                },
            },
            Discriminators = new()
            {
                [SForgetOurVoices.Discriminator] = new()
                {
                    // English: the Forget Our Voices which played a {0} in {1}’s voice in the {2} stage
                    // Example: the Forget Our Voices which played a 1 in Umbra Moruka’s voice in the first stage
                    Discriminator = "ステージ{2}で{1}の声で{0}が再生されたForget Our Voices",
                },
            },
        },

        // Forget’s Ultimate Showdown
        [typeof(SForgetsUltimateShowdown)] = new()
        {
            ModuleName = "忘る者の究極の対決",
            Questions = new()
            {
                [SForgetsUltimateShowdown.Answer] = new()
                {
                    // English: What was the {1} digit of the answer in {0}?
                    // Example: What was the first digit of the answer in Forget’s Ultimate Showdown?
                    Question = "{0}の答えの{1}桁目は？",
                },
                [SForgetsUltimateShowdown.Bottom] = new()
                {
                    // English: What was the {1} digit of the bottom number in {0}?
                    // Example: What was the first digit of the bottom number in Forget’s Ultimate Showdown?
                    Question = "{0}の下側の数字の{1}桁目は？",
                },
                [SForgetsUltimateShowdown.Initial] = new()
                {
                    // English: What was the {1} digit of the initial number in {0}?
                    // Example: What was the first digit of the initial number in Forget’s Ultimate Showdown?
                    Question = "{0}の初期状態の{1}桁目は？",
                },
                [SForgetsUltimateShowdown.Method] = new()
                {
                    // English: What was the {1} method used in {0}?
                    // Example: What was the first method used in Forget’s Ultimate Showdown?
                    Question = "{0}の{1}回目に使用した変換方法は？",
                    Answers = new()
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
            },
        },

        // Forget The Colors
        [typeof(SForgetTheColors)] = new()
        {
            ModuleName = "色忘る",
            Questions = new()
            {
                [SForgetTheColors.QGearNumber] = new()
                {
                    // English: What number was on the gear during stage {1} of {0}?
                    // Example: What number was on the gear during stage 0 of Forget The Colors?
                    Question = "{0}のステージ{1}におけるギアの数字は？",
                },
                [SForgetTheColors.QLargeDisplay] = new()
                {
                    // English: What number was on the large display during stage {1} of {0}?
                    // Example: What number was on the large display during stage 0 of Forget The Colors?
                    Question = "{0}のステージ{1}における大きなディスプレーはの数字は？",
                },
                [SForgetTheColors.QSineNumber] = new()
                {
                    // English: What was the last decimal in the sine number received during stage {1} of {0}?
                    // Example: What was the last decimal in the sine number received during stage 0 of Forget The Colors?
                    Question = "{0}のステージ{1}で取得したsin値の下一桁は？",
                },
                [SForgetTheColors.QGearColor] = new()
                {
                    // English: What color was the gear during stage {1} of {0}?
                    // Example: What color was the gear during stage 0 of Forget The Colors?
                    Question = "{0}のステージ{1}におけるギアの色は？",
                    Answers = new()
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
                [SForgetTheColors.QRuleColor] = new()
                {
                    // English: Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?
                    // Example: Which edgework-based rule was applied to the sum of nixies and gear during stage 0 of Forget The Colors?
                    Question = "{0}のステージ{1}におけるエッジワーク修正後のニキシー管の合計は？",
                    Answers = new()
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
            },
            Discriminators = new()
            {
                [SForgetTheColors.DGearNumber] = new()
                {
                    // English: the Forget The Colors whose gear number was {0} in stage {1}
                    // Example: the Forget The Colors whose gear number was 1 in stage 1
                    Discriminator = "ステージ{1}の歯車の数字が{0}だった色忘る",
                },
                [SForgetTheColors.DLargeDisplay] = new()
                {
                    // English: the Forget The Colors which had {0} on its large display in stage {1}
                    // Example: the Forget The Colors which had 426 on its large display in stage 1
                    Discriminator = "ステージ{1}の大きなディスプレーの数字が{0}であった色忘る",
                },
                [SForgetTheColors.DSineNumber] = new()
                {
                    // English: the Forget The Colors whose received sine number in stage {1} ended with a {0}
                    // Example: the Forget The Colors whose received sine number in stage 1 ended with a 0
                    Discriminator = "ステージ{1}のsin値の末尾が{0}であった色忘る",
                },
                [SForgetTheColors.DColor] = new()
                {
                    // English: the Forget The Colors whose {2} was {0} in stage {1}
                    // Example: the Forget The Colors whose gear color was Red in stage 1
                    Discriminator = "ステージ{1}の{2}が{0}であった色忘る",
                    Arguments = new()
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
                        ["gear color"] = "ギアの色",
                        ["rule color"] = "ルールの色",
                    },
                },
            },
        },

        // Forget This
        [typeof(SForgetThis)] = new()
        {
            ModuleName = "之忘る",
            Questions = new()
            {
                [SForgetThis.QColors] = new()
                {
                    // English: What color was the LED in the {1} stage of {0}?
                    // Example: What color was the LED in the first stage of Forget This?
                    Question = "{0}のステージ{1}におけるLEDの色は？",
                    Answers = new()
                    {
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                        ["Black"] = "黒",
                        ["White"] = "白",
                        ["Green"] = "緑",
                    },
                },
                [SForgetThis.QDigits] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Forget This?
                    Question = "{0}のステージ{1}における文字は？",
                },
            },
            Discriminators = new()
            {
                [SForgetThis.DColors] = new()
                {
                    // English: the Forget This whose LED was {0} in the {1} stage
                    // Example: the Forget This whose LED was cyan in the first stage
                    Discriminator = "ステージ{1}のLEDが{0}であった之忘る",
                    Arguments = new()
                    {
                        ["cyan"] = "シアン",
                        ["magenta"] = "マゼンタ",
                        ["yellow"] = "黄",
                        ["black"] = "黒",
                        ["white"] = "白",
                        ["green"] = "緑",
                    },
                },
                [SForgetThis.DDigits] = new()
                {
                    // English: the Forget This which displayed {0} in the {1} stage
                    // Example: the Forget This which displayed A in the first stage
                    Discriminator = "ステージ{1}で表示された文字が{0}であった之忘る",
                },
            },
        },

        // Forget Us Not
        [typeof(SForgetUsNot)] = new()
        {
            ModuleName = "我等忘る勿かれ",
            Questions = new()
            {
                [SForgetUsNot.Stage] = new()
                {
                    // English: Which module name was used for stage {1} in {0}?
                    // Example: Which module name was used for stage 1 in Forget Us Not?
                    Question = "{0}のステージ{1}で使用されたモジュール名は？",
                },
            },
            Discriminators = new()
            {
                [SForgetUsNot.Discriminator] = new()
                {
                    // English: the Forget Us Not in which {0} was used for stage {1}
                    // Example: the Forget Us Not in which Memory was used for stage 1
                    Discriminator = "ステージ{1}で{0}が使用された我等忘る勿かれ",
                },
            },
        },

        // Free Parking
        [typeof(SFreeParking)] = new()
        {
            ModuleName = "無料駐車場",
            Questions = new()
            {
                [SFreeParking.Token] = new()
                {
                    // English: What was the player token in {0}?
                    Question = "{0}のプレイヤーのコマは？",
                    Answers = new()
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
            },
        },

        // The French Republican Calendar
        [typeof(SFrenchRepublicanCalendar)] = new()
        {
            NeedsTranslation = true,
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
            ModuleName = "関数",
            Questions = new()
            {
                [SFunctions.LastDigit] = new()
                {
                    // English: What was the last digit of your first query’s result in {0}?
                    Question = "{0}の最初の問い合わせ結果の下一桁は？",
                },
                [SFunctions.LeftNumber] = new()
                {
                    // English: What number was to the left of the displayed letter in {0}?
                    Question = "{0}で英字の左隣のディスプレーに表示された数字は？",
                },
                [SFunctions.Letter] = new()
                {
                    // English: What letter was displayed in {0}?
                    Question = "{0}に表示された英字は？",
                },
                [SFunctions.RightNumber] = new()
                {
                    // English: What number was to the right of the displayed letter in {0}?
                    Question = "{0}で英字の右隣のディスプレーに表示された数字は？",
                },
            },
        },

        // The Fuse Box
        [typeof(SFuseBox)] = new()
        {
            ModuleName = "ヒューズボックス",
            Questions = new()
            {
                [SFuseBox.Flashes] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "{0}で{1}番目に点滅した色は？",
                },
                [SFuseBox.Arrows] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "{0}で{1}番目に表示された矢印は？",
                },
            },
        },

        // Gadgetron Vendor
        [typeof(SGadgetronVendor)] = new()
        {
            ModuleName = "ガラクトロン・ベンダー",
            Questions = new()
            {
                [SGadgetronVendor.CurrentWeapon] = new()
                {
                    // English: What was your current weapon in {0}?
                    Question = "{0}で所持していた武器は？",
                },
                [SGadgetronVendor.WeaponForSale] = new()
                {
                    // English: What was the weapon up for sale in {0}?
                    Question = "{0}で販売されている武器は？",
                },
            },
        },

        // Game of Life Cruel
        [typeof(SGameOfLifeCruel)] = new()
        {
            ModuleName = "残忍ライフゲーム",
            Questions = new()
            {
                [SGameOfLifeCruel.Colors] = new()
                {
                    // English: Which of these was a color combination that occurred in {0}?
                    Question = "{0}に出現した色の組み合わせに含まれるのは？",
                },
            },
        },

        // The Gamepad
        [typeof(SGamepad)] = new()
        {
            ModuleName = "ゲームパッド",
            Questions = new()
            {
                [SGamepad.Numbers] = new()
                {
                    // English: What were the numbers on {0}?
                    Question = "{0}の数字は？",
                },
            },
        },

        // Garfield Kart
        [typeof(SGarfieldKart)] = new()
        {
            ModuleName = "ガーフィールドカート",
            Questions = new()
            {
                [SGarfieldKart.Track] = new()
                {
                    // English: What was the track in {0}?
                    Question = "{0}のトラックは？",
                },
                [SGarfieldKart.PuzzleCount] = new()
                {
                    // English: How many puzzle pieces did {0} have?
                    Question = "{0}にあったパズルのピースの数は？",
                },
            },
        },

        // The Garnet Thief
        [typeof(SGarnetThief)] = new()
        {
            ModuleName = "宝石泥棒",
            Questions = new()
            {
                [SGarnetThief.Claim] = new()
                {
                    // English: Which faction did {1} claim to be in {0}?
                    // Example: Which faction did Jungmoon claim to be in The Garnet Thief?
                    Question = "{0}の{1}が所属を主張していた派閥は？",
                },
            },
        },

        // Ghost Movement
        [typeof(SGhostMovement)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGhostMovement.Position] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was Inky in Ghost Movement?
                    Question = "{0}で{1}はどこにいた？",
                },
            },
        },

        // Giants Cipher
        [typeof(SGiantsCipher)] = new()
        {
            NeedsTranslation = true,
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
            NeedsTranslation = true,
            Questions = new()
            {
                [SGirlfriend.Language] = new()
                {
                    // English: What was the language sung in {0}?
                    Question = "{0}で歌っていた言語は？",
                },
            },
        },

        // The Glitched Button
        [typeof(SGlitchedButton)] = new()
        {
            ModuleName = "グリッチボタン",
            Questions = new()
            {
                [SGlitchedButton.Sequence] = new()
                {
                    // English: What was the cycling bit sequence in {0}?
                    Question = "{0}で循環表示されていたビットのシーケンスは？",
                },
            },
        },

        // Goofy’s Game
        [typeof(SGoofysGame)] = new()
        {
            ModuleName = "グーフィーのゲーム",
            Questions = new()
            {
                [SGoofysGame.Number] = new()
                {
                    // English: What number was flashed by the {1} LED in {0}?
                    // Example: What number was flashed by the left LED in Goofy’s Game?
                    Question = "{0}の{1}のLEDが点滅した数字は？",
                    Arguments = new()
                    {
                        ["left"] = "左",
                        ["right"] = "右",
                        ["center"] = "中央",
                    },
                },
            },
        },

        // Grand Piano
        [typeof(SGrandPiano)] = new()
        {
            ModuleName = "グランドピアノ",
            Questions = new()
            {
                [SGrandPiano.Key] = new()
                {
                    // English: Which key was part of the {1} set in {0}?
                    // Example: Which key was part of the first set in Grand Piano?
                    Question = "{0}の{1}番目のセットに含まれていた鍵盤は？",
                },
                [SGrandPiano.FinalKey] = new()
                {
                    // English: Which key was the fifth set in {0}?
                    Question = "{0}の5番目のセットに含まれていた鍵盤は？",
                },
            },
        },

        // The Gray Button
        [typeof(SGrayButton)] = new()
        {
            ModuleName = "灰色ボタン",
            Questions = new()
            {
                [SGrayButton.Coordinates] = new()
                {
                    // English: What was the {1} coordinate on the display in {0}?
                    // Example: What was the horizontal coordinate on the display in The Gray Button?
                    Question = "{0}のディスプレー上に表示された{1}の座標は？",
                    Arguments = new()
                    {
                        ["horizontal"] = "段",
                        ["vertical"] = "列",
                    },
                },
            },
        },

        // Gray Cipher
        [typeof(SGrayCipher)] = new()
        {
            ModuleName = "灰色暗号",
            Questions = new()
            {
                [SGrayCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Gray Cipher?
                    Question = "{0}の答えは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // The Great Void
        [typeof(SGreatVoid)] = new()
        {
            ModuleName = "超空洞",
            Questions = new()
            {
                [SGreatVoid.Digit] = new()
                {
                    // English: What was the {1} digit in {0}?
                    // Example: What was the first digit in The Great Void?
                    Question = "{0}の{1}番目の数字は？",
                },
                [SGreatVoid.Color] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in The Great Void?
                    Question = "{0}の{1}番目の色は？",
                    Answers = new()
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
            },
        },

        // Green Arrows
        [typeof(SGreenArrows)] = new()
        {
            ModuleName = "緑色矢印",
            Questions = new()
            {
                [SGreenArrows.LastScreen] = new()
                {
                    // English: What was the last number on the display on {0}?
                    Question = "{0}の最後に表示された数字は？",
                },
            },
        },

        // The Green Button
        [typeof(SGreenButton)] = new()
        {
            ModuleName = "緑色ボタン",
            Questions = new()
            {
                [SGreenButton.Word] = new()
                {
                    // English: What was the word submitted in {0}?
                    Question = "{0}で送信した単語は？",
                },
            },
        },

        // Green Cipher
        [typeof(SGreenCipher)] = new()
        {
            ModuleName = "緑色暗号",
            Questions = new()
            {
                [SGreenCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Green Cipher?
                    Question = "{0}の答えは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Gridlock
        [typeof(SGridlock)] = new()
        {
            ModuleName = "グリッドロック",
            Questions = new()
            {
                [SGridlock.StartingColor] = new()
                {
                    // English: What was the starting color in {0}?
                    Question = "{0}の開始地点は何色？",
                    Answers = new()
                    {
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                    },
                },
                [SGridlock.StartingLocation] = new()
                {
                    // English: What was the starting location in {0}?
                    Question = "{0}の開始位置は？",
                },
                [SGridlock.EndingLocation] = new()
                {
                    // English: What was the ending location in {0}?
                    Question = "{0}の終了位置は？",
                },
            },
        },

        // Grocery Store
        [typeof(SGroceryStore)] = new()
        {
            ModuleName = "食料品店",
            Questions = new()
            {
                [SGroceryStore.FirstItem] = new()
                {
                    // English: What was the first item shown in {0}?
                    Question = "{0}で最初に表示された商品は？",
                },
            },
        },

        // Gryphons
        [typeof(SGryphons)] = new()
        {
            ModuleName = "グリフォン",
            Questions = new()
            {
                [SGryphons.Name] = new()
                {
                    // English: What was the gryphon’s name in {0}?
                    Question = "{0}にいたグリフォンの名前は？",
                },
                [SGryphons.Age] = new()
                {
                    // English: What was the gryphon’s age in {0}?
                    Question = "{0}にいたグリフォンの年齢は？",
                },
            },
        },

        // Guess Who?
        [typeof(SGuessWho)] = new()
        {
            ModuleName = "だーれだ？",
            Questions = new()
            {
                [SGuessWho.Colors] = new()
                {
                    // English: Did {1} flash “YES” in {0}?
                    // Example: Did Red flash “YES” in Guess Who??
                    Question = "{0}で{1}は「YES」と点滅した？",
                    Arguments = new()
                    {
                        ["Red"] = "赤",
                        ["Orange"] = "オレンジ",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Violet"] = "紫",
                        ["Cyan"] = "シアン",
                        ["Pink"] = "ピンク",
                    },
                    Answers = new()
                    {
                        ["Yes"] = "はい",
                        ["No"] = "いいえ",
                    },
                },
            },
        },

        // Gyromaze
        [typeof(SGyromaze)] = new()
        {
            ModuleName = "ジャイロ迷路",
            Questions = new()
            {
                [SGyromaze.LEDColor] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the top LED in Gyromaze?
                    Question = "{0}の{1}のLEDの色は？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["bottom"] = "下",
                    },
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                    },
                },
            },
        },

        // h
        [typeof(SH)] = new()
        {
            ModuleName = "H",
            Questions = new()
            {
                [SH.Letter] = new()
                {
                    // English: What was the transmitted letter in {0}?
                    Question = "{0}で送信した英字は？",
                },
            },
        },

        // Halli Galli
        [typeof(SHalliGalli)] = new()
        {
            ModuleName = "ハリガリ",
            Questions = new()
            {
                [SHalliGalli.Fruit] = new()
                {
                    // English: Which fruit were there five of in {0}?
                    Question = "{0}に5個あった果物は？",
                    Answers = new()
                    {
                        ["Strawberries"] = "イチゴ",
                        ["Melons"] = "メロン",
                        ["Lemons"] = "レモン",
                        ["Raspberries"] = "ラズベリー",
                        ["Bananas"] = "バナナ",
                    },
                },
                [SHalliGalli.Counts] = new()
                {
                    // English: What were the relevant counts in {0}?
                    Question = "{0}で答えを求めるために使用した分け方は？",
                },
            },
        },

        // Hereditary Base Notation
        [typeof(SHereditaryBaseNotation)] = new()
        {
            ModuleName = "遺伝的基数表記",
            Questions = new()
            {
                [SHereditaryBaseNotation.InitialNumber] = new()
                {
                    // English: What was the given number in {0}?
                    Question = "{0}で得られた数字は？",
                },
            },
        },

        // The Hexabutton
        [typeof(SHexabutton)] = new()
        {
            ModuleName = "六角形ボタン",
            Questions = new()
            {
                [SHexabutton.Label] = new()
                {
                    // English: What label was printed on {0}?
                    Question = "{0}に記されたラベルは？",
                },
            },
        },

        // Hexamaze
        [typeof(SHexamaze)] = new()
        {
            ModuleName = "六角迷路",
            Questions = new()
            {
                [SHexamaze.PawnColor] = new()
                {
                    // English: What was the color of the pawn in {0}?
                    Question = "{0}のコマの色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Cyan"] = "シアン",
                        ["Blue"] = "青",
                        ["Pink"] = "ピンク",
                    },
                },
            },
            Discriminators = new()
            {
                [SHexamaze.Discriminator] = new()
                {
                    // English: the Hexamaze that {0} a {1} marking on it
                    // Example: the Hexamaze that has a triangle marking on it
                    Discriminator = "{1}のマークが{0}六角迷路",
                    Arguments = new()
                    {
                        ["has"] = "ある",
                        ["doesn’t have"] = "ない",
                        ["triangle"] = "三角",
                        ["circle"] = "丸",
                        ["hexagon"] = "六角形",
                    },
                },
            },
        },

        // hexOrbits
        [typeof(SHexOrbits)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHexOrbits.Shape] = new()
                {
                    // English: What was the {1} shape for the {2} display in {0}?
                    // Example: What was the fast shape for the first display in hexOrbits?
                    Question = "{0}の{2}番目の表示で、速度が{1}方の図形は？",
                    Arguments = new()
                    {
                        ["fast"] = "速い",
                        ["slow"] = "遅い",
                    },
                },
            },
        },

        // hexOS
        [typeof(SHexOS)] = new()
        {
            ModuleName = "HexOS",
            Questions = new()
            {
                [SHexOS.OctCipher] = new()
                {
                    // English: What was the deciphered phrase in {0}?
                    Question = "{0}で解読したフレーズは？",
                },
                [SHexOS.Cipher] = new()
                {
                    // English: What were the deciphered letters in {0}?
                    Question = "{0}で解読した英字は？",
                },
                [SHexOS.Sum] = new()
                {
                    // English: What were the rhythm values in {0}?
                    Question = "{0}のリズムの値は？",
                },
                [SHexOS.Screen] = new()
                {
                    // English: What was the {1} 3-digit number cycled by the screen in {0}?
                    // Example: What was the first 3-digit number cycled by the screen in hexOS?
                    Question = "{0}の{1}回目に表示された三桁の数字は？",
                },
            },
        },

        // Hickory Dickory Dock
        [typeof(SHickoryDickoryDock)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHickoryDickoryDock.Time] = new()
                {
                    // English: What time was shown when the clock struck {1} on {0}?
                    // Example: What time was shown when the clock struck 1:00 on Hickory Dickory Dock?
                    Question = "{0}で時計が{1}の鐘を鳴らしたときにに表示された時間は？",
                },
            },
            Discriminators = new()
            {
                [SHickoryDickoryDock.Discriminator] = new()
                {
                    // English: the Hickory Dickory Dock which showed {0} when it struck {1}
                    // Example: the Hickory Dickory Dock which showed 1:30 when it struck 2:00
                    Discriminator = "{1}の鐘を鳴らしたときに{0}を表示したHickory Dickory Dock",
                },
            },
        },

        // Hidden Colors
        [typeof(SHiddenColors)] = new()
        {
            ModuleName = "隠し色",
            Questions = new()
            {
                [SHiddenColors.LED] = new()
                {
                    // English: What was the color of the main LED in {0}?
                    Question = "{0}のメインLEDの色は？",
                    Answers = new()
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
            },
        },

        // The Hidden Value
        [typeof(SHiddenValue)] = new()
        {
            ModuleName = "隠し値",
            Questions = new()
            {
                [SHiddenValue.Display] = new()
                {
                    // English: What was displayed on {0}?
                    Question = "{0}に表示されたのは？",
                    // Refer to translations.md to understand the weird strings
                    Additional = new()
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
            },
        },

        // The High Score
        [typeof(SHighScore)] = new()
        {
            ModuleName = "ハイスコア",
            Questions = new()
            {
                [SHighScore.Position] = new()
                {
                    // English: What was the position of the player in {0}?
                    Question = "{0}のプレイヤーの位置は？",
                },
                [SHighScore.Score] = new()
                {
                    // English: What was the score of the player in {0}?
                    Question = "{0}のプレイヤーのスコアは？",
                },
            },
        },

        // Hill Cycle
        [typeof(SHillCycle)] = new()
        {
            ModuleName = "ヒルサイクル",
            Questions = new()
            {
                [SHillCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Hill Cycle?
                    Question = "{0}の{1}番目のダイヤルが指していた方向は？",
                },
                [SHillCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Hill Cycle?
                    Question = "{0}の{1}番目のダイヤルに書かれていた英字は？",
                },
            },
            Discriminators = new()
            {
                [SHillCycle.LabelDiscriminator] = new()
                {
                    // English: the Hill Cycle that had the letter {0} on a dial
                    // Example: the Hill Cycle that had the letter A on a dial
                    Discriminator = "ダイヤルに{0}の英字があったヒルサイクル",
                },
            },
        },

        // Hinges
        [typeof(SHinges)] = new()
        {
            ModuleName = "蝶番",
            Questions = new()
            {
                [SHinges.Initial] = new()
                {
                    // English: Which of these hinges was initially {1} {0}?
                    // Example: Which of these hinges was initially present on Hinges?
                    Question = "{0}の初期状態で存在して{1}蝶番に含まれるのは？",
                    Arguments = new()
                    {
                        ["present on"] = "いた",
                        ["absent from"] = "いなかった",
                    },
                },
            },
            Discriminators = new()
            {
                [SHinges.Discriminator] = new()
                {
                    // English: the Hinges where this hinge was initally {0}
                    // Example: the Hinges where this hinge was initally present
                    Discriminator = "この蝶番が初期状態で{0}蝶番",
                    Arguments = new()
                    {
                        ["present"] = "存在した",
                        ["absent"] = "無かった",
                    },
                },
            },
        },

        // Hogwarts
        [typeof(SHogwarts)] = new()
        {
            ModuleName = "ホグワーツ",
            Questions = new()
            {
                [SHogwarts.House] = new()
                {
                    // English: Which House was {1} solved for in {0}?
                    // Example: Which House was Binary Puzzle solved for in Hogwarts?
                    Question = "{0}で{1}を解除したのはどの寮？",
                    Answers = new()
                    {
                        ["Gryffindor"] = "グリフォンドール",
                        ["Hufflepuff"] = "ハッフルパフ",
                        ["Slytherin"] = "スリザリン",
                        ["Ravenclaw"] = "レイブンクロー",
                    },
                },
                [SHogwarts.Module] = new()
                {
                    // English: Which module was solved for {1} in {0}?
                    // Example: Which module was solved for Gryffindor in Hogwarts?
                    Question = "{0}で{1}が解除したのはどのモジュール(英名)？",
                    Arguments = new()
                    {
                        ["Gryffindor"] = "グリフォンドール",
                        ["Hufflepuff"] = "ハッフルパフ",
                        ["Slytherin"] = "スリザリン",
                        ["Ravenclaw"] = "レイブンクロー",
                    },
                },
            },
        },

        // Hold Ups
        [typeof(SHoldUps)] = new()
        {
            ModuleName = "ホールドアップ",
            Questions = new()
            {
                [SHoldUps.Shadows] = new()
                {
                    // English: What was the name of the {1} shadow shown in {0}?
                    // Example: What was the name of the first shadow shown in Hold Ups?
                    Question = "{0}の{1}番目に表示されたシャドウの名前は？",
                },
            },
        },

        // Holographic Memory
        [typeof(SHolographicMemory)] = new()
        {
            ModuleName = "ホログラム記憶",
            Questions = new()
            {
                [SHolographicMemory.InitialGrid] = new()
                {
                    // English: Which side did this symbol appear in {0}? (+ sprite)
                    Question = "{0}でこのシンボルが現れた面は？",
                    Answers = new()
                    {
                        ["Light"] = "明るい面",
                        ["Dark"] = "暗い面",
                    },
                },
            },
        },

        // Homophones
        [typeof(SHomophones)] = new()
        {
            ModuleName = "同音異義語",
            Questions = new()
            {
                [SHomophones.DisplayedPhrases] = new()
                {
                    // English: What was the {1} displayed phrase in {0}?
                    // Example: What was the first displayed phrase in Homophones?
                    Question = "{0}の{1}番目に表示されたフレーズは？",
                },
            },
        },

        // Horrible Memory
        [typeof(SHorribleMemory)] = new()
        {
            ModuleName = "恐怖記憶",
            Questions = new()
            {
                [SHorribleMemory.Positions] = new()
                {
                    // English: In what position was the button pressed on the {1} stage of {0}?
                    // Example: In what position was the button pressed on the first stage of Horrible Memory?
                    Question = "{0}のステージ{1}で押されたボタンの位置は？",
                },
                [SHorribleMemory.Labels] = new()
                {
                    // English: What was the label of the button pressed on the {1} stage of {0}?
                    // Example: What was the label of the button pressed on the first stage of Horrible Memory?
                    Question = "{0}のステージ{1}で押されたボタンのラベルは？",
                },
                [SHorribleMemory.Colors] = new()
                {
                    // English: What color was the button pressed on the {1} stage of {0}?
                    // Example: What color was the button pressed on the first stage of Horrible Memory?
                    Question = "{0}のステージ{1}で押されたボタンの色は？",
                    Answers = new()
                    {
                        ["blue"] = "青",
                        ["green"] = "緑",
                        ["red"] = "赤",
                        ["orange"] = "オレンジ",
                        ["purple"] = "紫",
                        ["pink"] = "ピンク",
                    },
                },
            },
        },

        // Human Resources
        [typeof(SHumanResources)] = new()
        {
            ModuleName = "人事部",
            Questions = new()
            {
                [SHumanResources.Descriptors] = new()
                {
                    // English: Which was a descriptor shown in {1} in {0}?
                    // Example: Which was a descriptor shown in red in Human Resources?
                    Question = "{0}で{1}色で表示された識別語は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                    },
                },
                [SHumanResources.HiredFired] = new()
                {
                    // English: Who was {1} in {0}?
                    // Example: Who was fired in Human Resources?
                    Question = "{0}で{1}のは誰？",
                    Arguments = new()
                    {
                        ["fired"] = "解雇した",
                        ["hired"] = "雇用した",
                    },
                },
            },
        },

        // Hunting
        [typeof(SHunting)] = new()
        {
            ModuleName = "狩猟",
            Questions = new()
            {
                [SHunting.ColumnsRows] = new()
                {
                    // English: Which of the first three stages of {0} had the {1} symbol {2}?
                    // Example: Which of the first three stages of Hunting had the column symbol first?
                    Question = "{0}の最初3つのステージのうち、{2}番目に{1}シンボルを持っていたのはどれ？",
                    Arguments = new()
                    {
                        ["column"] = "列",
                        ["row"] = "段",
                    },
                    Answers = new()
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
            },
        },

        // The Hypercube
        [typeof(SHypercube)] = new()
        {
            ModuleName = "超立方体",
            Questions = new()
            {
                [SHypercube.Rotations] = new()
                {
                    // English: What was the {1} rotation in {0}?
                    // Example: What was the first rotation in The Hypercube?
                    Question = "{0}の{1}番目の回転方向は？",
                },
            },
        },

        // HyperForget
        [typeof(SHyperForget)] = new()
        {
            ModuleName = "超忘る",
            Questions = new()
            {
                [SHyperForget.Rotations] = new()
                {
                    // English: What was the rotation for the {1} stage in {0}?
                    // Example: What was the rotation for the first stage in HyperForget?
                    Question = "{0}の{1}番目の回転方向は？",
                },
            },
            Discriminators = new()
            {
                [SHyperForget.Discriminator] = new()
                {
                    // English: the HyperForget whose rotation in the {1} stage was {0}
                    // Example: the HyperForget whose rotation in the first stage was XY
                    Discriminator = "{1}番目の回転方向が{0}だった超忘る",
                },
            },
        },

        // The Hyperlink
        [typeof(SHyperlink)] = new()
        {
            ModuleName = "ハイパーリンク",
            Questions = new()
            {
                [SHyperlink.Characters] = new()
                {
                    // English: What was the {1} character of the hyperlink in {0}?
                    // Example: What was the first character of the hyperlink in The Hyperlink?
                    Question = "{0}のリンクの{1}文字目は？",
                },
                [SHyperlink.Answer] = new()
                {
                    // English: Which module was referenced on {0}?
                    Question = "{0}が参照していたモジュールは(英名)？",
                },
            },
        },

        // Ice Cream
        [typeof(SIceCream)] = new()
        {
            ModuleName = "アイスクリーム",
            Questions = new()
            {
                [SIceCream.Flavour] = new()
                {
                    // English: Which one of these flavours {1} to the {2} customer in {0}?
                    // Example: Which one of these flavours was on offer, but not sold, to the first customer in Ice Cream?
                    Question = "{0}で{2}番目の客が{1}商品に含まれるのは？",
                    Arguments = new()
                    {
                        ["was on offer, but not sold,"] = "注文したが売らなかった",
                        ["was not on offer"] = "注文していない",
                    },
                },
                [SIceCream.Customer] = new()
                {
                    // English: Who was the {1} customer in {0}?
                    // Example: Who was the first customer in Ice Cream?
                    Question = "{0}の{1}番目の客は？",
                },
            },
        },

        // Identification Crisis
        [typeof(SIdentificationCrisis)] = new()
        {
            ModuleName = "識別危機",
            Questions = new()
            {
                [SIdentificationCrisis.Shape] = new()
                {
                    // English: What was the {1} shape used in {0}?
                    // Example: What was the first shape used in Identification Crisis?
                    Question = "{0}で使用された{1}番目の図形は？",
                },
                [SIdentificationCrisis.Dataset] = new()
                {
                    // English: What was the {1} identification module used in {0}?
                    // Example: What was the first identification module used in Identification Crisis?
                    Question = "{0}で使用された{1}番目の識別モジュールは？",
                    Answers = new()
                    {
                        ["Morse Identification"] = "モールス識別",
                        ["Boozleglyph Identification"] = "ブーズル文字識別",
                        ["Plant Identification"] = "植物識別",
                        ["Pickup Identification"] = "アイテム識別",
                        ["Emotiguy Identification"] = "エモティガイ識別",
                        ["Ars Goetia Identification"] = "アルス・ゴエティア識別",
                        ["Mii Identification"] = "Mii識別",
                        ["Customer Identification"] = "顧客識別",
                        ["Spongebob Birthday Identification"] = "スポンジ・ボブ誕生日カード識別",
                        ["VTuber Identification"] = "VTuber識別",
                    },
                },
            },
        },

        // Identity Parade
        [typeof(SIdentityParade)] = new()
        {
            ModuleName = "容疑者特定",
            Questions = new()
            {
                [SIdentityParade.HairColors] = new()
                {
                    // English: Which hair color {1} listed in {0}?
                    // Example: Which hair color was listed in Identity Parade?
                    Question = "{0}のリストに{1}のはどの髪色？",
                    Arguments = new()
                    {
                        ["was"] = "存在した",
                        ["was not"] = "存在しなかった",
                    },
                },
                [SIdentityParade.Builds] = new()
                {
                    // English: Which build {1} listed in {0}?
                    // Example: Which build was listed in Identity Parade?
                    Question = "{0}のリストに{1}のはどの身体的特徴？",
                    Arguments = new()
                    {
                        ["was"] = "存在した",
                        ["was not"] = "存在しなかった",
                    },
                },
                [SIdentityParade.Attires] = new()
                {
                    // English: Which attire {1} listed in {0}?
                    // Example: Which attire was listed in Identity Parade?
                    Question = "{0}のリストに{1}のはどの服装？",
                    Arguments = new()
                    {
                        ["was"] = "存在した",
                        ["was not"] = "存在しなかった",
                    },
                },
            },
        },

        // The Impostor
        [typeof(SImpostor)] = new()
        {
            ModuleName = "ニセモノ",
            Questions = new()
            {
                [SImpostor.Disguise] = new()
                {
                    // English: Which module was {0} pretending to be?
                    Question = "{0}が化けていたのはどのモジュール(英名)？",
                },
            },
        },

        // Indigo Cipher
        [typeof(SIndigoCipher)] = new()
        {
            ModuleName = "藍色暗号",
            Questions = new()
            {
                [SIndigoCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Indigo Cipher?
                    Question = "{0}の答えは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Infinite Loop
        [typeof(SInfiniteLoop)] = new()
        {
            ModuleName = "無限ループ",
            Questions = new()
            {
                [SInfiniteLoop.SelectedWord] = new()
                {
                    // English: What was the selected word in {0}?
                    Question = "{0}で選択された単語は？",
                },
            },
        },

        // Ingredients
        [typeof(SIngredients)] = new()
        {
            ModuleName = "食材",
            Questions = new()
            {
                [SIngredients.Ingredients] = new()
                {
                    // English: Which ingredient was used in {0}?
                    Question = "{0}で使用された食材は？",
                },
                [SIngredients.NonIngredients] = new()
                {
                    // English: Which ingredient was listed but not used in {0}?
                    Question = "{0}の一覧にあったが使用されていない食材は？",
                },
            },
        },

        // Inner Connections
        [typeof(SInnerConnections)] = new()
        {
            ModuleName = "内部接続",
            Questions = new()
            {
                [SInnerConnections.LED] = new()
                {
                    // English: What color was the LED in {0}?
                    Question = "{0}のLEDの色は？",
                    Answers = new()
                    {
                        ["Black"] = "黒",
                        ["Blue"] = "青",
                        ["Red"] = "赤",
                        ["White"] = "白",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                    },
                },
                [SInnerConnections.Morse] = new()
                {
                    // English: What was the digit flashed in Morse in {0}?
                    Question = "{0}のモールス信号で点滅した数字は？",
                },
            },
        },

        // Interpunct
        [typeof(SInterpunct)] = new()
        {
            ModuleName = "句読点",
            Questions = new()
            {
                [SInterpunct.Display] = new()
                {
                    // English: What was the symbol displayed in the {1} stage of {0}?
                    // Example: What was the symbol displayed in the first stage of Interpunct?
                    Question = "{0}のステージ{1}で表示された記号は？",
                },
            },
        },

        // IPA
        [typeof(SIPA)] = new()
        {
            Questions = new()
            {
                [SIPA.Sound] = new()
                {
                    // English: What sound played in {0}?
                    Question = "{0}で再生された音は？",
                },
            },
        },

        // The iPhone
        [typeof(SiPhone)] = new()
        {
            ModuleName = "アイフォン",
            Questions = new()
            {
                [SiPhone.Digits] = new()
                {
                    // English: What was the {1} PIN digit in {0}?
                    // Example: What was the first PIN digit in The iPhone?
                    Question = "{0}の{1}番目のPINの数字は？",
                },
            },
        },

        // Jenga
        [typeof(SJenga)] = new()
        {
            ModuleName = "ジェンガ",
            Questions = new()
            {
                [SJenga.FirstBlock] = new()
                {
                    // English: Which symbol was on the first correctly pulled block in {0}?
                    Question = "{0}で最初に正しく引き抜かれたブロックの記号は？",
                },
            },
        },

        // The Jewel Vault
        [typeof(SJewelVault)] = new()
        {
            ModuleName = "宝石金庫",
            Questions = new()
            {
                [SJewelVault.Wheels] = new()
                {
                    // English: What number was wheel {1} in {0}?
                    // Example: What number was wheel A in The Jewel Vault?
                    Question = "{0}の輪{1}の数字は？",
                },
            },
        },

        // Jumble Cycle
        [typeof(SJumbleCycle)] = new()
        {
            ModuleName = "寄せ集めサイクル",
            Questions = new()
            {
                [SJumbleCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Jumble Cycle?
                    Question = "{0}の{1}番目のダイヤルが指していた方向は？",
                },
                [SJumbleCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Jumble Cycle?
                    Question = "{0}の{1}番目のダイヤルに書かれていた英字は？",
                },
            },
            Discriminators = new()
            {
                [SJumbleCycle.LabelDiscriminator] = new()
                {
                    // English: the Jumble Cycle that had the letter {0} on a dial
                    // Example: the Jumble Cycle that had the letter A on a dial
                    Discriminator = "ダイヤルに{0}の英字があった寄せ集めサイクル",
                },
            },
        },

        // Juxtacolored Squares
        [typeof(SJuxtacoloredSquares)] = new()
        {
            ModuleName = "色比べ格子",
            Questions = new()
            {
                [SJuxtacoloredSquares.ColorsByPosition] = new()
                {
                    // English: What was the color of this square in {0}? (+ sprite)
                    Question = "{0}のこの位置にあった正方形の色は？",
                    Answers = new()
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
                [SJuxtacoloredSquares.PositionsByColor] = new()
                {
                    // English: Which square was {1} in {0}?
                    // Example: Which square was red in Juxtacolored Squares?
                    Question = "{0}の{1}色の正方形はどれ？",
                    Arguments = new()
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
            },
        },

        // Kanji
        [typeof(SKanji)] = new()
        {
            ModuleName = "漢字",
            Questions = new()
            {
                [SKanji.DisplayedWords] = new()
                {
                    // English: What was the displayed word in the {1} stage of {0}?
                    // Example: What was the displayed word in the first stage of Kanji?
                    Question = "{0}のステージ{1}で表示された単語は？",
                },
            },
        },

        // The Kanye Encounter
        [typeof(SKanyeEncounter)] = new()
        {
            ModuleName = "カニエ・ウエストとの遭遇",
            Questions = new()
            {
                [SKanyeEncounter.Foods] = new()
                {
                    // English: What was a food item displayed in {0}?
                    Question = "{0}で表示された食品は？",
                },
            },
        },

        // KayMazey Talk
        [typeof(SKayMazeyTalk)] = new()
        {
            ModuleName = "K迷路トーク",
            Questions = new()
            {
                [SKayMazeyTalk.QWord] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the starting word in KayMazey Talk?
                    Question = "{0}の{1}の単語は？",
                    Arguments = new()
                    {
                        ["starting"] = "開始地点",
                        ["goal"] = "目標地点",
                    },
                },
            },
            Discriminators = new()
            {
                [SKayMazeyTalk.DWord] = new()
                {
                    // English: the KayMazey Talk whose {0} word was {1}
                    // Example: the KayMazey Talk whose starting word was Knit
                    Discriminator = "{0}の単語が{1}だったK迷路トーク",
                    Arguments = new()
                    {
                        ["starting"] = "開始地点",
                        ["goal"] = "目標地点",
                    },
                },
            },
        },

        // Keypad Combinations
        [typeof(SKeypadCombination)] = new()
        {
            ModuleName = "キーパッドコンビネーション",
            Questions = new()
            {
                [SKeypadCombination.WrongNumbers] = new()
                {
                    // English: Which number was displayed on the {1} button, but not part of the answer on {0}?
                    // Example: Which number was displayed on the first button, but not part of the answer on Keypad Combinations?
                    Question = "{0}の{1}番目のボタンに存在した、答え以外の数字は？",
                },
            },
        },

        // Keypad Magnified
        [typeof(SKeypadMagnified)] = new()
        {
            ModuleName = "拡大キーパッド",
            Questions = new()
            {
                [SKeypadMagnified.LED] = new()
                {
                    // English: What was the position of the LED in {0}?
                    Question = "{0}のLEDの位置は？",
                    Answers = new()
                    {
                        ["Top-left"] = "左上",
                        ["Top-right"] = "右上",
                        ["Bottom-left"] = "左下",
                        ["Bottom-right"] = "右下",
                    },
                },
            },
        },

        // Keypad Maze
        [typeof(SKeypadMaze)] = new()
        {
            ModuleName = "キーパッド迷路",
            Questions = new()
            {
                [SKeypadMaze.Yellow] = new()
                {
                    // English: Which of these cells was yellow in {0}?
                    Question = "{0}の黄色いマスはどのセル？",
                },
            },
        },

        // Keypad Sequence
        [typeof(SKeypadSequence)] = new()
        {
            ModuleName = "順番キーパッド",
            Questions = new()
            {
                [SKeypadSequence.Labels] = new()
                {
                    // English: What was this key’s label on the {1} panel in {0}? (+ sprite)
                    // Example: What was this key’s label on the first panel in Keypad Sequence? (+ sprite)
                    Question = "{0}の{1}番目のパネルにあったこのキーのラベルは？",
                },
            },
        },

        // Keywords
        [typeof(SKeywords)] = new()
        {
            ModuleName = "キーワード",
            Questions = new()
            {
                [SKeywords.DisplayedKey] = new()
                {
                    // English: What were the first four letters on the display in {0}?
                    Question = "{0}のディスプレー上にあった先頭四文字の英字は？",
                },
            },
        },

        // The Klaxon
        [typeof(SKlaxon)] = new()
        {
            ModuleName = "クラクション",
            Questions = new()
            {
                [SKlaxon.FirstModule] = new()
                {
                    // English: What was the first module to set off {0}?
                    Question = "{0}を最初に鳴らしたモジュールは？",
                },
            },
        },

        // Know Your Way
        [typeof(SKnowYourWay)] = new()
        {
            ModuleName = "方向感覚",
            Questions = new()
            {
                [SKnowYourWay.Arrow] = new()
                {
                    // English: Which way was the arrow pointing in {0}?
                    Question = "{0}の矢印が指していたのはどの方向？",
                    Answers = new()
                    {
                        ["Up"] = "上",
                        ["Down"] = "下",
                        ["Left"] = "左",
                        ["Right"] = "右",
                    },
                },
                [SKnowYourWay.Led] = new()
                {
                    // English: Which LED was green in {0}?
                    Question = "{0}でLEDが緑色だったのはどれ？",
                    Answers = new()
                    {
                        ["Top"] = "上",
                        ["Bottom"] = "下",
                        ["Right"] = "右",
                        ["Left"] = "左",
                    },
                },
            },
        },

        // Kooky Keypad
        [typeof(SKookyKeypad)] = new()
        {
            ModuleName = "狂ったキーパッド",
            Questions = new()
            {
                [SKookyKeypad.Color] = new()
                {
                    // English: What color was the {1} button’s LED in {0}?
                    // Example: What color was the top-left button’s LED in Kooky Keypad?
                    Question = "{0}の{1}のボタンのLEDの色は？",
                    Arguments = new()
                    {
                        ["top-left"] = "左上",
                        ["top-right"] = "右上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                    Answers = new()
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
            },
        },

        // Kudosudoku
        [typeof(SKudosudoku)] = new()
        {
            ModuleName = "クド数独",
            Questions = new()
            {
                [SKudosudoku.Prefilled] = new()
                {
                    // English: Which square was {1} in {0}?
                    // Example: Which square was pre-filled in Kudosudoku?
                    Question = "{0}で最初に{1}四角はどれ？",
                    Arguments = new()
                    {
                        ["pre-filled"] = "埋められていた",
                        ["not pre-filled"] = "埋められていなかった",
                    },
                },
            },
        },

        // Kugelblitz
        [typeof(SKugelblitz)] = new()
        {
            ModuleName = "クーゲルブリッツ",
            Questions = new()
            {
                [SKugelblitz.BlackOrangeYellowIndigoViolet] = new()
                {
                    // English: Which particles were present for the {1} stage of {0}?
                    // Example: Which particles were present for the first stage of Kugelblitz?
                    Question = "{0}の{1}番目のステージで存在した粒子は？",
                    // Refer to translations.md to understand the weird strings
                    Additional = new()
                    {
                        ["R"] = "赤",
                        ["O"] = "橙",
                        ["Y"] = "黄",
                        ["G"] = "緑",
                        ["B"] = "青",
                        ["I"] = "藍",
                        ["V"] = "紫",
                        ["{0}{1}{2}{3}{4}{5}{6}"] = "{0}{1}{2}{3}{4}{5}{6}",
                        ["None"] = "なし",
                    },
                },
                [SKugelblitz.RedGreenBlue] = new()
                {
                    // English: What were the particles’ values for the {1} stage of {0}?
                    // Example: What were the particles’ values for the first stage of Kugelblitz?
                    Question = "{0}の{1}番目のステージでの粒子の値は？",
                    // Refer to translations.md to understand the weird strings
                    Additional = new()
                    {
                        ["R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}"] = "赤={0}, 橙={1}, 黄={2}, 緑={3}, 青={4}, 藍={5}, 紫={6}",
                    },
                },
            },
            Discriminators = new()
            {
                [SKugelblitz.Color] = new()
                {
                    // English: the {0} Kugelblitz
                    // Example: the black Kugelblitz
                    Discriminator = "{0}のクーゲルブリッツ",
                    Arguments = new()
                    {
                        ["black"] = "黒",
                        ["red"] = "赤",
                        ["orange"] = "橙",
                        ["yellow"] = "黄",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["indigo"] = "藍",
                        ["violet"] = "紫",
                    },
                },
                [SKugelblitz.NoLinks] = new()
                {
                    // English: the Kugelblitz linked with no other Kugelblitzes
                    Discriminator = "他のクーゲルブリッツと繋がっていないクーゲルブリッツ",
                },
                [SKugelblitz.Links] = new()
                {
                    // English: the {0} Kugelblitz linked with {1}
                    // Example: the black Kugelblitz linked with one other Kugelblitz
                    Discriminator = "{0}のクーゲルブリッツが{1}と繋がっている",
                    Arguments = new()
                    {
                        ["black"] = "黒",
                        ["red"] = "赤",
                        ["orange"] = "橙",
                        ["yellow"] = "黄",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["indigo"] = "藍",
                        ["violet"] = "紫",
                        ["one other Kugelblitz"] = "他のクーゲルブリッツ1つ",
                        ["two other Kugelblitzes"] = "他のクーゲルブリッツ2つ",
                        ["three other Kugelblitzes"] = "他のクーゲルブリッツ3つ",
                        ["four other Kugelblitzes"] = "他のクーゲルブリッツ4つ",
                        ["five other Kugelblitzes"] = "他のクーゲルブリッツ5つ",
                        ["six other Kugelblitzes"] = "他のクーゲルブリッツ6つ",
                        ["seven other Kugelblitzes"] = "他のクーゲルブリッツ7つ",
                    },
                },
            },
        },

        // Kuro
        [typeof(SKuro)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKuro.Mood] = new()
                {
                    // English: What was Kuro’s mood in {0}?
                    Question = "{0}におけるKuroの気分は？",
                },
            },
        },

        // The Labyrinth
        [typeof(SLabyrinth)] = new()
        {
            ModuleName = "迷宮",
            Questions = new()
            {
                [SLabyrinth.PortalLocations] = new()
                {
                    // English: Where was one of the portals in layer {1} in {0}?
                    // Example: Where was one of the portals in layer 1 (Red) in The Labyrinth?
                    Question = "{0}の層{1}にあったポータルの一つは？",
                    Arguments = new()
                    {
                        ["1 (Red)"] = "1 (赤)",
                        ["2 (Orange)"] = "2 (橙)",
                        ["3 (Yellow)"] = "3 (黄)",
                        ["4 (Green)"] = "4 (緑)",
                        ["5 (Blue)"] = "5 (青)",
                    },
                },
                [SLabyrinth.PortalStage] = new()
                {
                    // English: In which layer was this portal in {0}? (+ sprite)
                    Question = "{0}でこのポータルがあったのはどの層？",
                    Answers = new()
                    {
                        ["1 (Red)"] = "1 (赤)",
                        ["2 (Orange)"] = "2 (橙)",
                        ["3 (Yellow)"] = "3 (黄)",
                        ["4 (Green)"] = "4 (緑)",
                        ["5 (Blue)"] = "5 (青)",
                    },
                },
            },
        },

        // Ladder Lottery
        [typeof(SLadderLottery)] = new()
        {
            ModuleName = "あみだくじ",
            Questions = new()
            {
                [SLadderLottery.LightOn] = new()
                {
                    // English: Which light was on in {0}?
                    Question = "{0}で点灯していたのはどのライト？",
                },
            },
        },

        // Ladders
        [typeof(SLadders)] = new()
        {
            ModuleName = "梯子",
            Questions = new()
            {
                [SLadders.Stage2Colors] = new()
                {
                    // English: Which color was present on the second ladder in {0}?
                    Question = "{0}の二番目の梯子に存在した色は？",
                    Answers = new()
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
                [SLadders.Stage3Missing] = new()
                {
                    // English: What color was missing on the third ladder in {0}?
                    Question = "{0}の三番目の梯子に存在しなかった色は？",
                    Answers = new()
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
            },
        },

        // Langton’s Anteater
        [typeof(SLangtonsAnteater)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLangtonsAnteater.InitialState] = new()
                {
                    // English: Which of these squares was initially {1} in {0}?
                    // Example: Which of these squares was initially black in Langton’s Anteater?
                    Question = "{0}の初期状態で{1}になっていたのはどのセル？",
                    Arguments = new()
                    {
                        ["black"] = "黒",
                        ["white"] = "白",
                    },
                },
            },
        },

        // Lasers
        [typeof(SLasers)] = new()
        {
            ModuleName = "レーザー",
            Questions = new()
            {
                [SLasers.Hatches] = new()
                {
                    // English: What was the number on the {1} hatch on {0}?
                    // Example: What was the number on the top-left hatch on Lasers?
                    Question = "{0}の{1}のハッチの番号は？ ",
                    Arguments = new()
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
            },
        },

        // LED Encryption
        [typeof(SLEDEncryption)] = new()
        {
            ModuleName = "暗号化LED",
            Questions = new()
            {
                [SLEDEncryption.PressedLetters] = new()
                {
                    // English: What was the correct letter you pressed in the {1} stage of {0}?
                    // Example: What was the correct letter you pressed in the first stage of LED Encryption?
                    Question = "{0}のステージ{1}で押した正しい文字は？",
                },
            },
        },

        // LED Grid
        [typeof(SLEDGrid)] = new()
        {
            ModuleName = "LEDグリッド",
            Questions = new()
            {
                [SLEDGrid.NumBlack] = new()
                {
                    // English: How many LEDs were unlit in {0}?
                    Question = "{0}で消灯していたLEDの数は？",
                },
            },
        },

        // LED Math
        [typeof(SLEDMath)] = new()
        {
            ModuleName = "LED算",
            Questions = new()
            {
                [SLEDMath.Lights] = new()
                {
                    // English: What color was {1} in {0}?
                    // Example: What color was LED A in LED Math?
                    Question = "{0}における{1}の色は？",
                    Arguments = new()
                    {
                        ["LED A"] = "LED A",
                        ["LED B"] = "LED B",
                        ["the operator LED"] = "演算子のLED",
                    },
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                    },
                },
            },
        },

        // LEDs
        [typeof(SLEDs)] = new()
        {
            ModuleName = "LEDセット",
            Questions = new()
            {
                [SLEDs.OriginalColor] = new()
                {
                    // English: What was the initial color of the changed LED in {0}?
                    Question = "{0}のLEDの初期色は？",
                    Answers = new()
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
            },
        },

        // LEGOs
        [typeof(SLEGOs)] = new()
        {
            ModuleName = "LEGO",
            Questions = new()
            {
                [SLEGOs.PieceDimensions] = new()
                {
                    // English: What were the dimensions of the {1} piece in {0}?
                    // Example: What were the dimensions of the red piece in LEGOs?
                    Question = "{0}の{1}のピースの大きさは？",
                    Arguments = new()
                    {
                        ["red"] = "赤色",
                        ["green"] = "緑色",
                        ["blue"] = "青色",
                        ["cyan"] = "シアン",
                        ["magenta"] = "マゼンタ",
                        ["yellow"] = "黄色",
                    },
                },
            },
        },

        // Letter Math
        [typeof(SLetterMath)] = new()
        {
            ModuleName = "英字数学",
            Questions = new()
            {
                [SLetterMath.Display] = new()
                {
                    // English: What was the letter on the {1} display in {0}?
                    // Example: What was the letter on the left display in Letter Math?
                    Question = "{0}のディスプレーの{1}に表示されていた英字は？",
                    Arguments = new()
                    {
                        ["left"] = "左",
                        ["right"] = "右",
                    },
                },
            },
        },

        // Light Bulbs
        [typeof(SLightBulbs)] = new()
        {
            ModuleName = "電球セット",
            Questions = new()
            {
                [SLightBulbs.Colors] = new()
                {
                    // English: What was the color of the {1} bulb in {0}?
                    // Example: What was the color of the left bulb in Light Bulbs?
                    Question = "{0}の{1}の電球の色は？",
                    Arguments = new()
                    {
                        ["left"] = "左",
                        ["right"] = "右",
                    },
                    Answers = new()
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
            },
        },

        // Lime Arrows
        [typeof(SLimeArrows)] = new()
        {
            NeedsTranslation = true,
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
            ModuleName = "リンク",
            Questions = new()
            {
                [SLinq.Function] = new()
                {
                    // English: What was the {1} function in {0}?
                    // Example: What was the first function in Linq?
                    Question = "{0}の{1}番目の関数は？",
                },
            },
            Discriminators = new()
            {
                [SLinq.Discriminator] = new()
                {
                    // English: the Linq whose {0} function was {1}
                    // Example: the Linq whose first function was First
                    Discriminator = "{1}の関数が{0}だったリンク",
                },
            },
        },

        // Lion’s Share
        [typeof(SLionsShare)] = new()
        {
            ModuleName = "ライオンの分け前",
            Questions = new()
            {
                [SLionsShare.Year] = new()
                {
                    // English: Which year was displayed on {0}?
                    Question = "{0}に表示された年は？",
                },
                [SLionsShare.RemovedLions] = new()
                {
                    // English: Which lion was present but removed in {0}?
                    Question = "{0}に存在したが除外されたライオンは？",
                },
            },
        },

        // Listening
        [typeof(SListening)] = new()
        {
            ModuleName = "リスニング",
            Questions = new()
            {
                [SListening.Sound] = new()
                {
                    // English: What clip was played in {0}?
                    Question = "{0}で再生されたクリップは？",
                },
            },
        },

        // Literal Maze
        [typeof(SLiteralMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLiteralMaze.Letter] = new()
                {
                    // English: Which letter was in this position in {0}?
                    Question = "{0}のこの位置にあった英字は？",
                },
            },
        },

        // Logical Buttons
        [typeof(SLogicalButtons)] = new()
        {
            ModuleName = "論理ボタン",
            Questions = new()
            {
                [SLogicalButtons.Color] = new()
                {
                    // English: What was the color of the {1} button in the {2} stage of {0}?
                    // Example: What was the color of the top button in the first stage of Logical Buttons?
                    Question = "{0}のステージ{2}における{1}のボタンの色は？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                    Answers = new()
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
                [SLogicalButtons.Label] = new()
                {
                    // English: What was the label on the {1} button in the {2} stage of {0}?
                    // Example: What was the label on the top button in the first stage of Logical Buttons?
                    Question = "{0}のステージ{2}における{1}のボタンのラベルは？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                },
                [SLogicalButtons.Operator] = new()
                {
                    // English: What was the final operator in the {1} stage of {0}?
                    // Example: What was the final operator in the first stage of Logical Buttons?
                    Question = "{0}のステージ{1}で最終的に使用した演算子は？",
                },
            },
        },

        // Logic Gates
        [typeof(SLogicGates)] = new()
        {
            ModuleName = "論理ゲート",
            Questions = new()
            {
                [SLogicGates.Gates] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was gate A in Logic Gates?
                    Question = "{0}で{1}はどれだったか？",
                    Arguments = new()
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
            },
        },

        // Lombax Cubes
        [typeof(SLombaxCubes)] = new()
        {
            ModuleName = "ロンバックスキューブ",
            Questions = new()
            {
                [SLombaxCubes.Letters] = new()
                {
                    // English: What was the {1} letter on the button in {0}?
                    // Example: What was the first letter on the button in Lombax Cubes?
                    Question = "{0}の{1}番目の英字は？",
                },
            },
        },

        // The London Underground
        [typeof(SLondonUnderground)] = new()
        {
            ModuleName = "ロンドン地下鉄",
            Questions = new()
            {
                [SLondonUnderground.Stations] = new()
                {
                    // English: Where did the {1} journey on {0} {2}?
                    // Example: Where did the first journey on The London Underground depart from?
                    Question = "{0}の{1}番目の経路における{2}は？",
                    Arguments = new()
                    {
                        ["depart from"] = "出発駅",
                        ["arrive to"] = "到着駅",
                    },
                },
            },
        },

        // Long Words
        [typeof(SLongWords)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLongWords.Word] = new()
                {
                    // English: What was the word on the top display on {0}?
                    Question = "{0}の上のディスプレーに表示された単語は？",
                },
            },
        },

        // Mad Memory
        [typeof(SMadMemory)] = new()
        {
            ModuleName = "狂気記憶",
            Questions = new()
            {
                [SMadMemory.Displays] = new()
                {
                    // English: What was on the display in the {1} stage of {0}?
                    // Example: What was on the display in the first stage of Mad Memory?
                    Question = "{0}のステージ{1}における表示は？",
                    Arguments = new()
                    {
                        ["first"] = "1",
                        ["second"] = "2",
                        ["third"] = "3",
                        ["4th"] = "4",
                    },
                },
            },
        },

        // Mafia
        [typeof(SMafia)] = new()
        {
            ModuleName = "マフィア",
            Questions = new()
            {
                [SMafia.Players] = new()
                {
                    // English: Who was a player, but not the Godfather, in {0}?
                    Question = "{0}でゴッドファーザーではなかったがプレイヤーだったのは？",
                },
            },
        },

        // Magenta Cipher
        [typeof(SMagentaCipher)] = new()
        {
            ModuleName = "マゼンタ暗号",
            Questions = new()
            {
                [SMagentaCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Magenta Cipher?
                    Question = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Mahjong
        [typeof(SMahjong)] = new()
        {
            ModuleName = "麻雀パズル",
            Questions = new()
            {
                [SMahjong.CountingTile] = new()
                {
                    // English: Which tile was shown in the bottom-left of {0}?
                    Question = "{0}の左下に表示された牌は？",
                },
                [SMahjong.Matches] = new()
                {
                    // English: Which tile was part of the {1} matched pair in {0}?
                    // Example: Which tile was part of the first matched pair in Mahjong?
                    Question = "{0}の{1}番目にマッチした牌のペアに含まれるのは？",
                },
            },
        },

        // Main Page
        [typeof(SMainPage)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMainPage.ButtonEffectOrigin] = new()
                {
                    // English: Which main page did the {1} button’s effect come from in {0}?
                    // Example: Which main page did the toons button’s effect come from in Main Page?
                    Question = "{0}で{1}ボタンのエフェクトの元となったメインページは？",
                    Arguments = new()
                    {
                        ["toons"] = "トゥーン",
                        ["games"] = "ゲーム",
                        ["characters"] = "キャラクター",
                        ["downloads"] = "ダウンロード",
                        ["store"] = "ストア",
                        ["email"] = "メール",
                    },
                },
                [SMainPage.HomestarBackgroundOrigin] = new()
                {
                    // English: Which main page did {1} come from in {0}?
                    // Example: Which main page did Homestar come from in Main Page?
                    Question = "{0}で{1}の元となったメインページは？",
                    Arguments = new()
                    {
                        ["Homestar"] = "Homestar",
                        ["the background"] = "背景",
                    },
                },
                [SMainPage.BubbleColors] = new()
                {
                    // English: Which color did the bubble not display in {0}?
                    Question = "{0}で表示されなかった吹き出しの色は？",
                    Answers = new()
                    {
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                    },
                },
                [SMainPage.BubbleMessages] = new()
                {
                    // English: Which of the following messages did the bubble {1} in {0}?
                    // Example: Which of the following messages did the bubble display in Main Page?
                    Question = "{0}で吹き出しに表示され{1}メッセージは？",
                    Arguments = new()
                    {
                        ["display"] = "た",
                        ["not display"] = "なかった",
                    },
                },
            },
        },

        // M&Ms
        [typeof(SMandMs)] = new()
        {
            ModuleName = "MとM",
            Questions = new()
            {
                [SMandMs.Colors] = new()
                {
                    // English: What color was the text on the {1} button in {0}?
                    // Example: What color was the text on the first button in M&Ms?
                    Question = "{0}の{1}番目のボタンの色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["orange"] = "オレンジ",
                        ["blue"] = "青",
                        ["yellow"] = "黄",
                        ["brown"] = "茶",
                    },
                },
                [SMandMs.Labels] = new()
                {
                    // English: What was the text on the {1} button in {0}?
                    // Example: What was the text on the first button in M&Ms?
                    Question = "{0}の{1}番目のボタン上にあったテキストは？",
                },
            },
        },

        // M&Ns
        [typeof(SMandNs)] = new()
        {
            ModuleName = "MとN",
            Questions = new()
            {
                [SMandNs.Colors] = new()
                {
                    // English: What color was the text on the {1} button in {0}?
                    // Example: What color was the text on the first button in M&Ns?
                    Question = "{0}の{1}番目のボタン上にあったテキストの色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["orange"] = "オレンジ",
                        ["blue"] = "青",
                        ["yellow"] = "黄",
                        ["brown"] = "茶",
                    },
                },
                [SMandNs.Label] = new()
                {
                    // English: What was the text on the correct button in {0}?
                    Question = "{0}の正しいボタンのテキストは？",
                },
            },
        },

        // Maritime Flags
        [typeof(SMaritimeFlags)] = new()
        {
            ModuleName = "海上旗",
            Questions = new()
            {
                [SMaritimeFlags.Bearing] = new()
                {
                    // English: What bearing was signalled in {0}?
                    Question = "{0}が示していた方角は？",
                },
                [SMaritimeFlags.Callsign] = new()
                {
                    // English: Which callsign was signalled in {0}?
                    Question = "{0}で送信されたコールサインは？",
                },
            },
        },

        // Maritime Semaphore
        [typeof(SMaritimeSemaphore)] = new()
        {
            ModuleName = "海上セマフォア信号",
            Questions = new()
            {
                [SMaritimeSemaphore.Dummy] = new()
                {
                    // English: In which position was the dummy in {0}?
                    Question = "{0}でダミーの信号があった位置は？",
                },
                [SMaritimeSemaphore.Letter] = new()
                {
                    // English: Which letter was shown by the {2} in the {1} position in {0}?
                    // Example: Which letter was shown by the left flag in the first position in Maritime Semaphore?
                    Question = "{0}の{1}番目の{2}が表示した英字は？",
                    Arguments = new()
                    {
                        ["left flag"] = "左の旗",
                        ["right flag"] = "右の旗",
                        ["semaphore"] = "セマフォア信号",
                    },
                },
            },
        },

        // The Maroon Button
        [typeof(SMaroonButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMaroonButton.A] = new()
                {
                    // English: What was A in {0}?
                    Question = "{0}のAは？",
                },
            },
        },

        // Maroon Cipher
        [typeof(SMaroonCipher)] = new()
        {
            ModuleName = "栗色暗号",
            Questions = new()
            {
                [SMaroonCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Maroon Cipher?
                    Question = "{0}のページ{2}の{1}ディスプレーに表示されていたのは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Mashematics
        [typeof(SMashematics)] = new()
        {
            ModuleName = "連打算数",
            Questions = new()
            {
                [SMashematics.Answer] = new()
                {
                    // English: What was the answer in {0}?
                    Question = "{0}の答えは？",
                },
                [SMashematics.Calculation] = new()
                {
                    // English: What was the {1} number in the equation on {0}?
                    // Example: What was the first number in the equation on Mashematics?
                    Question = "{0}の方程式にあった{1}番目の数字は？",
                },
            },
        },

        // Master Tapes
        [typeof(SMasterTapes)] = new()
        {
            ModuleName = "マスターテープ",
            Questions = new()
            {
                [SMasterTapes.PlayedSong] = new()
                {
                    // English: Which song was played in {0}?
                    Question = "{0}で再生された歌は？",
                },
            },
        },

        // Match Refereeing
        [typeof(SMatchRefereeing)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMatchRefereeing.Planet] = new()
                {
                    // English: Which planet was present in the {1} stage of {0}?
                    // Example: Which planet was present in the first stage of Match Refereeing?
                    Question = "{0}のステージ{1}で存在した惑星は？",
                },
            },
        },

        // Math ’em
        [typeof(SMathEm)] = new()
        {
            ModuleName = "計算神経衰弱",
            Questions = new()
            {
                [SMathEm.Color] = new()
                {
                    // English: What was the color of this tile before the shuffle on {0}? (+ sprite)
                    Question = "{0}のシャッフル前におけるこのタイルの色は？",
                    Answers = new()
                    {
                        ["White"] = "白",
                        ["Bronze"] = "銅",
                        ["Silver"] = "銀",
                        ["Gold"] = "金",
                    },
                },
                [SMathEm.Label] = new()
                {
                    // English: What was the design on this tile before the shuffle on {0}? (+ sprite)
                    Question = "{0}のシャッフル前におけるこのタイルのデザインは？",
                },
            },
        },

        // The Matrix
        [typeof(SMatrix)] = new()
        {
            ModuleName = "マトリックス",
            Questions = new()
            {
                [SMatrix.AccessCode] = new()
                {
                    // English: Which word was part of the latest access code in {0}?
                    Question = "{0}における最後のアクセスコードの一部であった単語は？",
                },
                [SMatrix.GlitchWord] = new()
                {
                    // English: What was the glitched word in {0}?
                    Question = "{0}でグリッチされていた単語は？",
                },
            },
        },

        // Maze
        [typeof(SMaze)] = new()
        {
            ModuleName = "迷路",
            Questions = new()
            {
                [SMaze.StartingPosition] = new()
                {
                    // English: In which {1} was the starting position in {0}, counting from the {2}?
                    // Example: In which column was the starting position in Maze, counting from the left?
                    Question = "{0}のスタート地点の{1}は{2}から何番目？",
                    Arguments = new()
                    {
                        ["column"] = "列",
                        ["row"] = "段",
                        ["left"] = "左",
                        ["top"] = "上",
                    },
                },
            },
        },

        // Maze³
        [typeof(SMaze3)] = new()
        {
            ModuleName = "迷路³",
            Questions = new()
            {
                [SMaze3.StartingFace] = new()
                {
                    // English: What was the color of the starting face in {0}?
                    Question = "{0}の開始面の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Magenta"] = "マゼンタ",
                        ["Orange"] = "オレンジ",
                    },
                },
            },
        },

        // Maze Identification
        [typeof(SMazeIdentification)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMazeIdentification.Seed] = new()
                {
                    // English: What was the seed of the maze in {0}?
                    Question = "{0}の迷路のシード値は？",
                },
                [SMazeIdentification.Num] = new()
                {
                    // English: What was the function of button {1} in {0}?
                    // Example: What was the function of button 1 in Maze Identification?
                    Question = "{0}のボタン{1}の機能は？",
                    Answers = new()
                    {
                        ["Forwards"] = "前進",
                        ["Clockwise"] = "時計回り",
                        ["Backwards"] = "後退",
                        ["Counter-clockwise"] = "反時計回り",
                    },
                },
                [SMazeIdentification.Func] = new()
                {
                    // English: Which button {1} in {0}?
                    // Example: Which button moved you forwards in Maze Identification?
                    Question = "{0}で{1}ボタンは？",
                    Arguments = new()
                    {
                        ["moved you forwards"] = "前進する",
                        ["turned you clockwise"] = "時計回りに回る",
                        ["moved you backwards"] = "後退する",
                        ["turned you counter-clockwise"] = "反時計回りに回る",
                    },
                },
            },
        },

        // Mazematics
        [typeof(SMazematics)] = new()
        {
            ModuleName = "計算迷路",
            Questions = new()
            {
                [SMazematics.Value] = new()
                {
                    // English: Which was the {1} value in {0}?
                    // Example: Which was the initial value in Mazematics?
                    Question = "{0}の{1}値は？",
                    Arguments = new()
                    {
                        ["initial"] = "初期",
                        ["goal"] = "目的",
                    },
                },
            },
        },

        // Maze Scrambler
        [typeof(SMazeScrambler)] = new()
        {
            ModuleName = "迷路スクランブラー",
            Questions = new()
            {
                [SMazeScrambler.Start] = new()
                {
                    // English: What was the starting position on {0}?
                    Question = "{0}の開始位置は？",
                    Answers = new()
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
                [SMazeScrambler.Goal] = new()
                {
                    // English: What was the goal on {0}?
                    Question = "{0}のゴール位置は？",
                    Answers = new()
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
                [SMazeScrambler.Indicators] = new()
                {
                    // English: Which of these positions was a maze marking on {0}?
                    Question = "{0}の迷路を求めるマークの位置はどれ？",
                    Answers = new()
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
            },
        },

        // Mazeseeker
        [typeof(SMazeseeker)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMazeseeker.Cell] = new()
                {
                    // English: How many walls surrounded this cell in {0}? (+ sprite)
                    Question = "{0}でこのセルの周囲にあった壁は？",
                },
                [SMazeseeker.Start] = new()
                {
                    // English: Where was the start in {0}?
                    Question = "{0}の開始地点は？",
                },
                [SMazeseeker.Goal] = new()
                {
                    // English: Where was the goal in {0}?
                    Question = "{0}のゴール地点は？",
                },
            },
        },

        // Maze Swap
        [typeof(SMazeSwap)] = new()
        {
            ModuleName = "入れ替え迷路",
            Questions = new()
            {
                [SMazeSwap.Position] = new()
                {
                    // English: Where was the {1} position in {0}?
                    // Example: Where was the starting position in Maze Swap?
                    Question = "{0}の{1}位置は？",
                    Arguments = new()
                    {
                        ["starting"] = "スタート",
                        ["goal"] = "ゴール",
                    },
                },
            },
        },

        // Mega Man 2
        [typeof(SMegaMan2)] = new()
        {
            ModuleName = "ロックマン2",
            Questions = new()
            {
                [SMegaMan2.Master] = new()
                {
                    // English: Which master was shown in {0}?
                    Question = "{0}で表示されたマスターは？",
                },
                [SMegaMan2.Weapon] = new()
                {
                    // English: Which weapon was shown in {0}?
                    Question = "{0}で表示された武器は？",
                },
            },
        },

        // Melody Sequencer
        [typeof(SMelodySequencer)] = new()
        {
            ModuleName = "メロディーシークエンス",
            Questions = new()
            {
                [SMelodySequencer.Parts] = new()
                {
                    // English: Which slot contained part #{1} at the start of {0}?
                    // Example: Which slot contained part #1 at the start of Melody Sequencer?
                    Question = "{0}の開始時にパート{1}が入っていたスロットは？",
                },
                [SMelodySequencer.Slots] = new()
                {
                    // English: Which part was in slot #{1} at the start of {0}?
                    // Example: Which part was in slot #1 at the start of Melody Sequencer?
                    Question = "{0}の開始時にスロット{1}に入っていたのは？",
                },
            },
        },

        // Memorable Buttons
        [typeof(SMemorableButtons)] = new()
        {
            ModuleName = "記憶ボタン",
            Questions = new()
            {
                [SMemorableButtons.Symbols] = new()
                {
                    // English: What was the {1} correct symbol pressed in {0}?
                    // Example: What was the first correct symbol pressed in Memorable Buttons?
                    Question = "{0}の{1}番目の正しいシンボルは？",
                },
            },
        },

        // Memory
        [typeof(SMemory)] = new()
        {
            ModuleName = "記憶",
            Questions = new()
            {
                [SMemory.Display] = new()
                {
                    // English: What was the displayed number in the {1} stage of {0}?
                    // Example: What was the displayed number in the first stage of Memory?
                    Question = "{0}のステージ{1}で表示された数は？",
                },
                [SMemory.Position] = new()
                {
                    // English: In what position was the button that you pressed in the {1} stage of {0}?
                    // Example: In what position was the button that you pressed in the first stage of Memory?
                    Question = "{0}のステージ{1}で押したボタンの位置は？",
                },
                [SMemory.Label] = new()
                {
                    // English: What was the label of the button that you pressed in the {1} stage of {0}?
                    // Example: What was the label of the button that you pressed in the first stage of Memory?
                    Question = "{0}のステージ{1}で押したボタンのラベルは？",
                },
            },
        },

        // Memory Wires
        [typeof(SMemoryWires)] = new()
        {
            ModuleName = "記憶ワイヤ",
            Questions = new()
            {
                [SMemoryWires.WireColours] = new()
                {
                    // English: What was the colour of wire {1} in {0}?
                    // Example: What was the colour of wire 1 in Memory Wires?
                    Question = "{0}のワイヤ{1}の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Blue"] = "青",
                        ["White"] = "白",
                        ["Black"] = "黒",
                    },
                },
                [SMemoryWires.DisplayedDigits] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Memory Wires?
                    Question = "{0}のステージ{1}で表示された数字は？",
                },
            },
        },

        // Metamorse
        [typeof(SMetamorse)] = new()
        {
            ModuleName = "メタモールス",
            Questions = new()
            {
                [SMetamorse.ExtractedLetter] = new()
                {
                    // English: What was the extracted letter in {0}?
                    Question = "{0}で抽出された英字は？",
                },
            },
        },

        // Metapuzzle
        [typeof(SMetapuzzle)] = new()
        {
            ModuleName = "メタパズル",
            Questions = new()
            {
                [SMetapuzzle.Answer] = new()
                {
                    // English: What was the final answer in {0}?
                    Question = "{0}の最終的な答えは？",
                },
            },
        },

        // Minsk Metro
        [typeof(SMinskMetro)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMinskMetro.Station] = new()
                {
                    // English: What was the name of starting station in {0}?
                    Question = "{0}の出発駅の名前は？",
                },
            },
        },

        // Mirror
        [typeof(SMirror)] = new()
        {
            ModuleName = "鏡",
            Questions = new()
            {
                [SMirror.Word] = new()
                {
                    // English: What was the second word written by the original ghost in {0}?
                    Question = "{0}の幽霊が二回目に書いた単語は？",
                },
            },
        },

        // The Missing Letter
        [typeof(SMissingLetter)] = new()
        {
            ModuleName = "欠落英字",
            Questions = new()
            {
                [SMissingLetter.MissingLetter] = new()
                {
                    // English: What letter was missing in {0}?
                    Question = "{0}で欠けていた英字は？",
                },
            },
        },

        // Mister Softee
        [typeof(SMisterSoftee)] = new()
        {
            ModuleName = "ミスター・ソフティー",
            Questions = new()
            {
                [SMisterSoftee.SpongebobPosition] = new()
                {
                    // English: Where was the SpongeBob Bar on {0}?
                    Question = "{0}のスポンジボブ・バーがあった場所は？",
                },
                [SMisterSoftee.TreatsPresent] = new()
                {
                    // English: Which treat was present on {0}?
                    Question = "{0}に存在していたお菓子は？",
                },
            },
        },

        // Mixometer
        [typeof(SMixometer)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMixometer.SubmitButton] = new()
                {
                    // English: What was the position of the submit button in {0}?
                    Question = "{0}で送信ボタンがあった位置は？",
                },
            },
        },

        // Modern Cipher
        [typeof(SModernCipher)] = new()
        {
            ModuleName = "現代暗号",
            Questions = new()
            {
                [SModernCipher.Word] = new()
                {
                    // English: What was the decrypted word of the {1} stage in {0}?
                    // Example: What was the decrypted word of the first stage in Modern Cipher?
                    Question = "{0}のステージ{1}で復号された単語は？",
                },
            },
        },

        // Module Listening
        [typeof(SModuleListening)] = new()
        {
            ModuleName = "モジュールリスニング",
            Questions = new()
            {
                [SModuleListening.ButtonAudio] = new()
                {
                    // English: Which sound did the {1} button play in {0}?
                    // Example: Which sound did the red button play in Module Listening?
                    Question = "{0}で{1}色のボタンが再生した音は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["yellow"] = "黄",
                    },
                },
                [SModuleListening.AnyAudio] = new()
                {
                    // English: Which sound played in {0}?
                    Question = "{0}で再生された音は？",
                },
            },
        },

        // Module Maneuvers
        [typeof(SModuleManeuvers)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SModuleManeuvers.Goal] = new()
                {
                    // English: What was the goal location in {0}?
                    Question = "{0}のゴール地点は？",
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
            ModuleName = "モジュール迷路",
            Questions = new()
            {
                [SModuleMaze.StartingIcon] = new()
                {
                    // English: Which of the following was the starting icon for {0}?
                    Question = "{0}の開始アイコンは？",
                },
            },
        },

        // Module Movements
        [typeof(SModuleMovements)] = new()
        {
            ModuleName = "モジュール追跡",
            Questions = new()
            {
                [SModuleMovements.Display] = new()
                {
                    // English: What was the {1} module shown in {0}?
                    // Example: What was the first module shown in Module Movements?
                    Question = "{0}の{1}番目のモジュールは(英名)？",
                },
            },
        },

        // Money Game
        [typeof(SMoneyGame)] = new()
        {
            ModuleName = "マネーゲーム",
            Questions = new()
            {
                [SMoneyGame.Question1] = new()
                {
                    // English: What were the first and second words in the {1} phrase in {0}?
                    // Example: What were the first and second words in the first phrase in Money Game?
                    Question = "{0}の{1}番目のフレーズの最初と二番目の単語は？",
                },
                [SMoneyGame.Question2] = new()
                {
                    // English: What were the third and fourth words in the {1} phrase in {0}?
                    // Example: What were the third and fourth words in the first phrase in Money Game?
                    Question = "{0}の{1}番目のフレーズの三番目と四番目の単語は？",
                },
                [SMoneyGame.Question3] = new()
                {
                    // English: What was the end of the {1} phrase in {0}?
                    // Example: What was the end of the first phrase in Money Game?
                    Question = "{0}の{1}番目のフレーズの最後の部分は？",
                },
            },
        },

        // Monsplode, Fight!
        [typeof(SMonsplodeFight)] = new()
        {
            ModuleName = "モンスプロード・ファイト！",
            Questions = new()
            {
                [SMonsplodeFight.Creature] = new()
                {
                    // English: Which creature was displayed in {0}?
                    Question = "{0}で表示されたモンスターは？",
                },
                [SMonsplodeFight.Move] = new()
                {
                    // English: Which one of these moves {1} selectable in {0}?
                    // Example: Which one of these moves was selectable in Monsplode, Fight!?
                    Question = "{0}で{1}わざに含まれるのは？",
                    Arguments = new()
                    {
                        ["was"] = "存在した",
                        ["was not"] = "存在しなかった",
                    },
                },
            },
        },

        // Monsplode Trading Cards
        [typeof(SMonsplodeTradingCards)] = new()
        {
            ModuleName = "モンスプロード・カードゲーム",
            Questions = new()
            {
                [SMonsplodeTradingCards.Cards] = new()
                {
                    // English: What was the {1} before the last action in {0}?
                    // Example: What was the first card in your hand before the last action in Monsplode Trading Cards?
                    Question = "{0}の最後の行動前における{1}は？",
                    Arguments = new()
                    {
                        ["first card in your hand"] = "手札の1枚目",
                        ["second card in your hand"] = "手札の2枚目",
                        ["third card in your hand"] = "手札の3枚目",
                        ["card on offer"] = "相手のカード",
                    },
                },
                [SMonsplodeTradingCards.PrintVersions] = new()
                {
                    // English: What was the print version of the {1} before the last action in {0}?
                    // Example: What was the print version of the first card in your hand before the last action in Monsplode Trading Cards?
                    Question = "{0}の最後の行動前における{1}に書かれた印刷バージョンは？",
                    Arguments = new()
                    {
                        ["first card in your hand"] = "手札の1枚目",
                        ["second card in your hand"] = "手札の2枚目",
                        ["third card in your hand"] = "手札の3枚目",
                        ["card on offer"] = "相手のカード",
                    },
                },
            },
        },

        // The Moon
        [typeof(SMoon)] = new()
        {
            ModuleName = "月",
            Questions = new()
            {
                [SMoon.LitUnlit] = new()
                {
                    // English: What was the {1} set in clockwise order in {0}?
                    // Example: What was the first initially lit set in clockwise order in The Moon?
                    Question = "{0}で時計回りに見て{1}セットは？",
                    Arguments = new()
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
                    Answers = new()
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
            },
        },

        // More Code
        [typeof(SMoreCode)] = new()
        {
            ModuleName = "新モールス信号",
            Questions = new()
            {
                [SMoreCode.Word] = new()
                {
                    // English: What was the flashing word in {0}?
                    Question = "{0}で点滅した単語は？",
                },
            },
        },

        // Morse-A-Maze
        [typeof(SMorseAMaze)] = new()
        {
            ModuleName = "モールス迷路",
            Questions = new()
            {
                [SMorseAMaze.StartingCoordinate] = new()
                {
                    // English: What was the starting location in {0}?
                    Question = "{0}の開始位置は？",
                },
                [SMorseAMaze.EndingCoordinate] = new()
                {
                    // English: What was the ending location in {0}?
                    Question = "{0}のゴール位置は？",
                },
                [SMorseAMaze.MorseCodeWord] = new()
                {
                    // English: What was the word shown as Morse code in {0}?
                    Question = "{0}でモールス信号で表示された単語は？",
                },
            },
        },

        // Morse Buttons
        [typeof(SMorseButtons)] = new()
        {
            ModuleName = "モールスボタン",
            Questions = new()
            {
                [SMorseButtons.ButtonLabel] = new()
                {
                    // English: What was the character flashed by the {1} button in {0}?
                    // Example: What was the character flashed by the first button in Morse Buttons?
                    Question = "{0}の{1}番目のボタンで点滅した文字は？",
                },
                [SMorseButtons.ButtonColor] = new()
                {
                    // English: What was the color flashed by the {1} button in {0}?
                    // Example: What was the color flashed by the first button in Morse Buttons?
                    Question = "{0}の{1}番目のボタンで点滅した色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["green"] = "緑",
                        ["yellow"] = "黄",
                        ["orange"] = "オレンジ",
                        ["purple"] = "紫",
                    },
                },
            },
        },

        // Morsematics
        [typeof(SMorsematics)] = new()
        {
            ModuleName = "モールスマティック",
            Questions = new()
            {
                [SMorsematics.ReceivedLetters] = new()
                {
                    // English: What was the {1} received letter in {0}?
                    // Example: What was the first received letter in Morsematics?
                    Question = "{0}で{1}番目に受信した文字は？",
                },
            },
        },

        // Morse War
        [typeof(SMorseWar)] = new()
        {
            ModuleName = "モールス戦争",
            Questions = new()
            {
                [SMorseWar.Code] = new()
                {
                    // English: What code was transmitted in {0}?
                    Question = "{0}で変換した符号は？",
                },
                [SMorseWar.Leds] = new()
                {
                    // English: What were the LEDs in the {1} row in {0} (1 = on, 0 = off)?
                    // Example: What were the LEDs in the bottom row in Morse War (1 = on, 0 = off)?
                    Question = "{0}で{1}段のLEDの状態は(1=オン、0=オフ)？",
                    Arguments = new()
                    {
                        ["bottom"] = "下",
                        ["middle"] = "中央",
                        ["top"] = "上",
                    },
                },
            },
        },

        // .--/---/..-.
        [typeof(SMorseWoF)] = new()
        {
            ModuleName = "--..-_--_..-_--..-_.-.._...-",
            Questions = new()
            {
                [SMorseWoF.Displays] = new()
                {
                    // English: What was the display in the {1} stage on {0}?
                    // Example: What was the display in the first stage on .--/---/..-.?
                    Question = "{0}のステージ{1}で表示されたのは？",
                },
            },
        },

        // Mouse In The Maze
        [typeof(SMouseInTheMaze)] = new()
        {
            ModuleName = "迷路のネズミ",
            Questions = new()
            {
                [SMouseInTheMaze.Sphere] = new()
                {
                    // English: Which color sphere was the goal in {0}?
                    Question = "{0}のゴールの球の色は？",
                    Answers = new()
                    {
                        ["white"] = "白",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["yellow"] = "黄",
                    },
                },
                [SMouseInTheMaze.Torus] = new()
                {
                    // English: What color was the torus in {0}?
                    Question = "{0}の輪の色は？",
                    Answers = new()
                    {
                        ["white"] = "白",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["yellow"] = "黄",
                    },
                },
            },
        },

        // M-Seq
        [typeof(SMSeq)] = new()
        {
            ModuleName = "リズム正方型",
            Questions = new()
            {
                [SMSeq.Obtained] = new()
                {
                    // English: What was the {1} obtained digit in {0}?
                    // Example: What was the first obtained digit in M-Seq?
                    Question = "{0}の{1}番目に獲得した数字は？",
                },
                [SMSeq.Submitted] = new()
                {
                    // English: What was the final number from the iteration process in {0}?
                    Question = "{0}の最後の繰り返しで得た最終値は？",
                },
            },
        },

        // Mssngv Wls
        [typeof(SMssngvWls)] = new()
        {
            ModuleName = "欠落母音",
            Questions = new()
            {
                [SMssngvWls.MssNgvwL] = new()
                {
                    // English: Which vowel was missing in {0}?
                    // Example: Which vowel was missing in Mssngv Wls?
                    Question = "{0} de kaketeita boin ha?",
                    // Refer to translations.md to understand the weird strings
                    Arguments = new()
                    {
                        ["AEIOU"] = "AEIOU",
                    },
                },
            },
        },

        // Multicolored Switches
        [typeof(SMulticoloredSwitches)] = new()
        {
            ModuleName = "色どりスイッチ",
            Questions = new()
            {
                [SMulticoloredSwitches.LedColor] = new()
                {
                    // English: What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?
                    // Example: What color was the first LED on the top row when the tiny LED was lit in Multicolored Switches?
                    Question = "{0}で小さなLEDが{3}時の{2}段LEDの{1}番目は？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["bottom"] = "下",
                        ["lit"] = "点灯した",
                        ["unlit"] = "点灯していない",
                    },
                    Answers = new()
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
            },
        },

        // The Multiverse Hotline
        [typeof(SMultiverseHotline)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMultiverseHotline.OriginUniverse] = new()
                {
                    // English: What was the origin universe in {0}?
                    Question = "{0}の発信元の宇宙は？",
                },
                [SMultiverseHotline.OriginUniverseInitNumber] = new()
                {
                    // English: What was the origin universe’s initial number in {0}?
                    Question = "{0}の発信元の宇宙の初期値は？",
                },
            },
        },

        // Murder
        [typeof(SMurder)] = new()
        {
            ModuleName = "殺人",
            Questions = new()
            {
                [SMurder.Suspect] = new()
                {
                    // English: Which of these was {1} in {0}?
                    // Example: Which of these was a suspect but not the murderer in Murder?
                    Question = "{0}の{1}に含まれるのは？",
                    Arguments = new()
                    {
                        ["a suspect but not the murderer"] = "殺人鬼ではなかった容疑者",
                        ["not a suspect"] = "容疑者ではなかった人物",
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
                    Question = "{0}の{1}に含まれるのは？",
                    Arguments = new()
                    {
                        ["a potential weapon but not the murder weapon"] = "凶器ではない候補にあった武器",
                        ["not a potential weapon"] = "候補に無かった武器",
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
                    Question = "{0}の死体はどこで見つかった？",
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
            ModuleName = "ミステリーモジュール",
            Questions = new()
            {
                [SMysteryModule.FirstKey] = new()
                {
                    // English: Which module was the first requested to be solved by {0}?
                    Question = "{0}で最初に解除するように指示されたモジュールは(英名)？",
                },
                [SMysteryModule.HiddenModule] = new()
                {
                    // English: Which module was hidden by {0}?
                    Question = "{0}で隠されていたモジュールは(英名)？",
                },
            },
        },

        // Mystic Square
        [typeof(SMysticSquare)] = new()
        {
            ModuleName = "神秘スクエア",
            Questions = new()
            {
                [SMysticSquare.Skull] = new()
                {
                    // English: Where was the skull in {0}?
                    Question = "{0}のどくろの位置は？",
                    Answers = new()
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
            },
        },

        // Name Codes
        [typeof(SNameCodes)] = new()
        {
            ModuleName = "名前コード",
            Questions = new()
            {
                [SNameCodes.Indices] = new()
                {
                    // English: What was the {1} index in {0}?
                    // Example: What was the left index in Name Codes?
                    Question = "{0}の{1}のインデックスは？",
                    Arguments = new()
                    {
                        ["left"] = "左",
                        ["right"] = "右",
                    },
                },
            },
        },

        // Naming Conventions
        [typeof(SNamingConventions)] = new()
        {
            ModuleName = "命名規則",
            Questions = new()
            {
                [SNamingConventions.Object] = new()
                {
                    // English: What was the label of the first button in {0}?
                    Question = "{0}の最初のボタンに書かれたラベルは？",
                },
            },
        },

        // N&Ms
        [typeof(SNandMs)] = new()
        {
            ModuleName = "NとM",
            Questions = new()
            {
                [SNandMs.Answer] = new()
                {
                    // English: What was the label of the correct button in {0}?
                    Question = "{0}の正しいボタンのラベルは？",
                },
            },
        },

        // N&Ns
        [typeof(SNandNs)] = new()
        {
            ModuleName = "NとN",
            Questions = new()
            {
                [SNandNs.Label] = new()
                {
                    // English: Which label was present in the {1} stage of {0}?
                    // Example: Which label was present in the first stage of N&Ns?
                    Question = "{0}の{1}ステージで表示されたラベルは？",
                },
                [SNandNs.Color] = new()
                {
                    // English: Which color was missing in the third stage of {0}?
                    Question = "{0}のステージ3で表示された色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Orange"] = "オレンジ",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                        ["Brown"] = "茶色",
                    },
                },
            },
        },

        // Navigation Determination
        [typeof(SNavigationDetermination)] = new()
        {
            ModuleName = "ナビ決定",
            Questions = new()
            {
                [SNavigationDetermination.Color] = new()
                {
                    // English: What was the color of the maze in {0}?
                    Question = "{0}の迷路の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                    },
                },
                [SNavigationDetermination.Label] = new()
                {
                    // English: What was the label of the maze in {0}?
                    Question = "{0}の迷路のラベルは？",
                },
            },
        },

        // Navinums
        [typeof(SNavinums)] = new()
        {
            ModuleName = "ナビ数字",
            Questions = new()
            {
                [SNavinums.DirectionalButtons] = new()
                {
                    // English: What was the {1} directional button pressed in {0}?
                    // Example: What was the first directional button pressed in Navinums?
                    Question = "{0}の{1}番目に押したボタンの方向は？",
                    Answers = new()
                    {
                        ["up"] = "下",
                        ["left"] = "左",
                        ["right"] = "右",
                        ["down"] = "下",
                    },
                },
                [SNavinums.MiddleDigit] = new()
                {
                    // English: What was the initial middle digit in {0}?
                    Question = "{0}の最初の中央の値は？",
                },
            },
        },

        // The Navy Button
        [typeof(SNavyButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNavyButton.QGreekLetters] = new()
                {
                    // English: Which Greek letter appeared on {0} (case-sensitive)?
                    Question = "{0}に表示されたギリシャ文字は(大文字小文字区別あり)？",
                },
                [SNavyButton.QGiven] = new()
                {
                    // English: What was the {1} of the given in {0}?
                    // Example: What was the (0-indexed) column of the given in The Navy Button?
                    Question = "{0}で得られた{1}は？",
                    Arguments = new()
                    {
                        ["(0-indexed) column"] = "列(0から開始)",
                        ["(0-indexed) row"] = "段(0から開始)",
                        ["value"] = "値",
                    },
                },
            },
            Discriminators = new()
            {
                [SNavyButton.DGreekLettersNV] = new()
                {
                    // English: the Navy Button that had a {0} on it
                    // Example: the Navy Button that had a Β on it
                    Discriminator = "{0}が書かれたNavy Button",
                },
                [SNavyButton.DGreekLettersV] = new()
                {
                    // English: the Navy Button that had an {0} on it
                    // Example: the Navy Button that had an Α on it
                    Discriminator = "{0}が書かれたNavy Button",
                },
                [SNavyButton.DGiven] = new()
                {
                    // English: the Navy Button where the {0} of the given was {1}
                    // Example: the Navy Button where the (0-indexed) column of the given was 1
                    Discriminator = "{0}が{1}であったNavy Button",
                    Arguments = new()
                    {
                        ["(0-indexed) column"] = "列(0から開始)",
                        ["(0-indexed) row"] = "段(0から開始)",
                        ["value"] = "値",
                    },
                },
            },
        },

        // The Necronomicon
        [typeof(SNecronomicon)] = new()
        {
            ModuleName = "ネクロノミコン",
            Questions = new()
            {
                [SNecronomicon.Chapters] = new()
                {
                    // English: What was the chapter number of the {1} page in {0}?
                    // Example: What was the chapter number of the first page in The Necronomicon?
                    Question = "{0}の{1}番目のページの章番号は？",
                },
            },
        },

        // Negativity
        [typeof(SNegativity)] = new()
        {
            ModuleName = "負極性",
            Questions = new()
            {
                [SNegativity.SubmittedValue] = new()
                {
                    // English: In base 10, what was the value submitted in {0}?
                    Question = "{0}で送信した値は十進数でいくつ？",
                },
                [SNegativity.SubmittedTernary] = new()
                {
                    // English: Excluding 0s, what was the submitted balanced ternary in {0}?
                    Question = "0を除き、{0}で送信した値は均衡三進数でいくつ？",
                },
            },
        },

        // Neptune
        [typeof(SNeptune)] = new()
        {
            ModuleName = "海王星",
            Questions = new()
            {
                [SNeptune.Star] = new()
                {
                    // English: Which star was displayed in {0}?
                    Question = "{0}で表示されたスターは？",
                },
            },
        },

        // Neutralization
        [typeof(SNeutralization)] = new()
        {
            ModuleName = "中和滴定",
            Questions = new()
            {
                [SNeutralization.Color] = new()
                {
                    // English: What was the acid’s color in {0}?
                    Question = "{0}の酸の色は？",
                    Answers = new()
                    {
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                    },
                },
                [SNeutralization.Volume] = new()
                {
                    // English: What was the acid’s volume in {0}?
                    Question = "{0}の酸の量は？",
                },
            },
        },

        // Next In Line
        [typeof(SNextInLine)] = new()
        {
            ModuleName = "ネクストライン",
            Questions = new()
            {
                [SNextInLine.FirstWire] = new()
                {
                    // English: What color was the first wire in {0}?
                    Question = "{0}の最初のワイヤの色は？",
                    Answers = new()
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
            },
        },

        // Nim
        [typeof(SNim)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNim.MatchCountFirstRow] = new()
                {
                    // English: How many matches were in the {1} row in {0}?
                    // Example: How many matches were in the first row in Nim?
                    Question = "{0}の{1}段目にあったマッチの数は？",
                },
                [SNim.MatchCountOtherRows] = new()
                {
                    // English: How many matches were in the {1} row in {0}?
                    // Example: How many matches were in the first row in Nim?
                    Question = "{0}の{1}段目にあったマッチの数は？",
                },
            },
        },

        // Noise Identification
        [typeof(SNoiseIdentification)] = new()
        {
            NeedsTranslation = true,
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
            Questions = new()
            {
                [SNonverbalSimon.Flashes] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "{0}のステージ{1}で点滅したボタンは？",
                },
            },
        },

        // Not Colored Squares
        [typeof(SNotColoredSquares)] = new()
        {
            ModuleName = "偽色付き格子",
            Questions = new()
            {
                [SNotColoredSquares.InitialPosition] = new()
                {
                    // English: What was the position of the square you initially pressed in {0}?
                    Question = "{0}の最初に押した正方形の位置は？",
                },
            },
        },

        // Not Colored Switches
        [typeof(SNotColoredSwitches)] = new()
        {
            ModuleName = "偽色付きスイッチ",
            Questions = new()
            {
                [SNotColoredSwitches.Word] = new()
                {
                    // English: What was the encrypted word in {0}?
                    Question = "{0}で暗号化されていた単語は？",
                },
            },
        },

        // Not Colour Flash
        [typeof(SNotColourFlash)] = new()
        {
            ModuleName = "偽カラーフラッシュ",
            Questions = new()
            {
                [SNotColourFlash.InitialWord] = new()
                {
                    // English: What was {1} in the displayed word sequence in {0}?
                    // Example: What was first in the displayed word sequence in Not Colour Flash?
                    Question = "{0}の単語シーケンスで{1}番目に表示された単語は？",
                },
                [SNotColourFlash.InitialColour] = new()
                {
                    // English: What was {1} in the displayed colour sequence in {0}?
                    // Example: What was first in the displayed colour sequence in Not Colour Flash?
                    Question = "{0}の色シーケンスで{1}番目に表示された色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                        ["White"] = "白",
                    },
                },
            },
        },

        // Not Connection Check
        [typeof(SNotConnectionCheck)] = new()
        {
            ModuleName = "偽接続確認",
            Questions = new()
            {
                [SNotConnectionCheck.Flashes] = new()
                {
                    // English: What symbol flashed on the {1} button in {0}?
                    // Example: What symbol flashed on the top left button in Not Connection Check?
                    Question = "{0}の{1}のボタンが示した記号は？",
                    Arguments = new()
                    {
                        ["top left"] = "左上",
                        ["top right"] = "右上",
                        ["bottom left"] = "左下",
                        ["bottom right"] = "右下",
                    },
                },
                [SNotConnectionCheck.Values] = new()
                {
                    // English: What was the value of the {1} button in {0}?
                    // Example: What was the value of the top left button in Not Connection Check?
                    Question = "{0}の{1}のボタンの値は？",
                    Arguments = new()
                    {
                        ["top left"] = "左上",
                        ["top right"] = "右上",
                        ["bottom left"] = "左下",
                        ["bottom right"] = "右下",
                    },
                },
            },
        },

        // Not Coordinates
        [typeof(SNotCoordinates)] = new()
        {
            ModuleName = "偽座標",
            Questions = new()
            {
                [SNotCoordinates.SquareCoords] = new()
                {
                    // English: Which coordinate was part of the square in {0}?
                    Question = "{0}の正方形の一部に含まれた座標は？",
                },
            },
        },

        // Not Double-Oh
        [typeof(SNotDoubleOh)] = new()
        {
            ModuleName = "偽ダブル・オー",
            Questions = new()
            {
                [SNotDoubleOh.Position] = new()
                {
                    // English: What was the {1} displayed position in the second stage of {0}?
                    // Example: What was the first displayed position in the second stage of Not Double-Oh?
                    Question = "{0}の第2ステージで{1}番目に表示された図形の位置は？",
                },
            },
        },

        // Not Keypad
        [typeof(SNotKeypad)] = new()
        {
            ModuleName = "偽キーパッド",
            Questions = new()
            {
                [SNotKeypad.Color] = new()
                {
                    // English: What color flashed {1} in the final sequence in {0}?
                    // Example: What color flashed first in the final sequence in Not Keypad?
                    Question = "{0}の最終的なシーケンスにおける{1}番目の点滅の色は？",
                    Answers = new()
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
                [SNotKeypad.Symbol] = new()
                {
                    // English: Which symbol was on the button that flashed {1} in the final sequence in {0}?
                    // Example: Which symbol was on the button that flashed first in the final sequence in Not Keypad?
                    Question = "{0}の最終的なシーケンスにおける{1}番目の点滅の記号は？",
                },
            },
        },

        // Not Maze
        [typeof(SNotMaze)] = new()
        {
            ModuleName = "偽迷路",
            Questions = new()
            {
                [SNotMaze.StartingDistance] = new()
                {
                    // English: What was the starting distance in {0}?
                    Question = "{0}の開始時の距離は？",
                },
            },
        },

        // Not Morse Code
        [typeof(SNotMorseCode)] = new()
        {
            ModuleName = "偽モールス信号",
            Questions = new()
            {
                [SNotMorseCode.Word] = new()
                {
                    // English: What was the {1} correct word you submitted in {0}?
                    // Example: What was the first correct word you submitted in Not Morse Code?
                    Question = "{0}の{1}番目に送信した単語は？",
                },
            },
        },

        // Not Morsematics
        [typeof(SNotMorsematics)] = new()
        {
            ModuleName = "偽モールスマティック",
            Questions = new()
            {
                [SNotMorsematics.Word] = new()
                {
                    // English: What was the transmitted word on {0}?
                    Question = "{0}で送信した単語は？",
                },
            },
        },

        // Not Murder
        [typeof(SNotMurder)] = new()
        {
            ModuleName = "偽殺人",
            Questions = new()
            {
                [SNotMurder.Room] = new()
                {
                    // English: What room was {1} in initially on {0}?
                    // Example: What room was Miss Scarlett in initially on Not Murder?
                    Question = "{1}が{0}で最初にいたのはどの部屋？",
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
                    Question = "{1}が{0}で最初に所持していたのはどの武器？",
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
                    Discriminator = "{0}",
                    Arguments = new()
                    {
                        ["he"] = "いた偽殺人",
                        ["she"] = "いた偽殺人",
                    },
                },
                [SNotMurder.InitialWeapon] = new()
                {
                    // English: the Not Murder where {0} initially held the {1}
                    // Example: the Not Murder where he initially held the Candlestick
                    Discriminator = "{1}を{0}偽殺人",
                    Arguments = new()
                    {
                        ["he"] = "持っていた",
                        ["she"] = "持っていた",
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
                    Discriminator = "{1}を{0}偽殺人",
                    Arguments = new()
                    {
                        ["he"] = "持っていた",
                        ["she"] = "持っていた",
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
            ModuleName = "偽ナンバーパッド",
            Questions = new()
            {
                [SNotNumberPad.Flashes] = new()
                {
                    // English: Which of these numbers {1} at the {2} stage of {0}?
                    // Example: Which of these numbers flashed at the first stage of Not Number Pad?
                    Question = "{0}のステージ{2}で{1}数字は？",
                    Arguments = new()
                    {
                        ["flashed"] = "点滅した",
                        ["did not flash"] = "点滅しなかった",
                    },
                },
            },
        },

        // Not Password
        [typeof(SNotPassword)] = new()
        {
            ModuleName = "偽パスワード",
            Questions = new()
            {
                [SNotPassword.Letter] = new()
                {
                    // English: Which letter was missing from {0}?
                    Question = "{0}で欠けていた文字は？",
                },
            },
        },

        // Not Perspective Pegs
        [typeof(SNotPerspectivePegs)] = new()
        {
            ModuleName = "偽奥行きペグ",
            Questions = new()
            {
                [SNotPerspectivePegs.Position] = new()
                {
                    // English: What was the position of the {1} flashing peg on {0}?
                    // Example: What was the position of the first flashing peg on Not Perspective Pegs?
                    Question = "{0}の{1}番目に点滅したペグの位置は？",
                    Answers = new()
                    {
                        ["top"] = "上",
                        ["top-right"] = "右上",
                        ["bottom-right"] = "右下",
                        ["bottom-left"] = "左下",
                        ["top-left"] = "左上",
                    },
                },
                [SNotPerspectivePegs.Perspective] = new()
                {
                    // English: From what perspective did the {1} peg flash on {0}?
                    // Example: From what perspective did the first peg flash on Not Perspective Pegs?
                    Question = "{0}の{1}番目に点滅したペグの面は？",
                    Answers = new()
                    {
                        ["top"] = "上",
                        ["top-right"] = "右上",
                        ["bottom-right"] = "右下",
                        ["bottom-left"] = "左下",
                        ["top-left"] = "左上",
                    },
                },
                [SNotPerspectivePegs.Color] = new()
                {
                    // English: What was the color of the {1} flashing peg on {0}?
                    // Example: What was the color of the first flashing peg on Not Perspective Pegs?
                    Question = "{0}の{1}番目に点滅したペグの色は？",
                    Answers = new()
                    {
                        ["blue"] = "青",
                        ["green"] = "緑",
                        ["purple"] = "紫",
                        ["red"] = "赤",
                        ["yellow"] = "黄",
                    },
                },
            },
        },

        // Not Piano Keys
        [typeof(SNotPianoKeys)] = new()
        {
            ModuleName = "偽鍵盤",
            Questions = new()
            {
                [SNotPianoKeys.FirstSymbol] = new()
                {
                    // English: What was the first displayed symbol on {0}?
                    Question = "{0}に表示された記号の一つ目は？",
                },
                [SNotPianoKeys.SecondSymbol] = new()
                {
                    // English: What was the second displayed symbol on {0}?
                    Question = "{0}に表示された記号の二つ目は？",
                },
                [SNotPianoKeys.ThirdSymbol] = new()
                {
                    // English: What was the third displayed symbol on {0}?
                    Question = "{0}に表示された記号の三つ目は？",
                },
            },
        },

        // Not Red Arrows
        [typeof(SNotRedArrows)] = new()
        {
            ModuleName = "偽赤色矢印",
            Questions = new()
            {
                [SNotRedArrows.Start] = new()
                {
                    // English: What was the starting number in {0}?
                    Question = "{0}の初期値は？",
                },
            },
        },

        // Not Simaze
        [typeof(SNotSimaze)] = new()
        {
            ModuleName = "偽サイモンゲーム",
            Questions = new()
            {
                [SNotSimaze.Maze] = new()
                {
                    // English: Which maze was used in {0}?
                    Question = "{0}で使用した迷路は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["orange"] = "オレンジ",
                        ["yellow"] = "黄",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["purple"] = "紫",
                    },
                },
                [SNotSimaze.Start] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "{0}の開始位置は？",
                    Answers = new()
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
                [SNotSimaze.Goal] = new()
                {
                    // English: What was the goal position in {0}?
                    Question = "{0}のゴールの位置は？",
                    Answers = new()
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
            },
        },

        // Not Text Field
        [typeof(SNotTextField)] = new()
        {
            ModuleName = "偽テキストフィールド",
            Questions = new()
            {
                [SNotTextField.BackgroundLetter] = new()
                {
                    // English: Which letter appeared 9 times at the start of {0}?
                    Question = "{0}の最初に9つ表示されていた英字は？",
                },
                [SNotTextField.InitialPresses] = new()
                {
                    // English: Which letter was pressed in the first stage of {0}?
                    Question = "{0}ステージ1で押したのはどの英字？",
                },
            },
        },

        // Not The Bulb
        [typeof(SNotTheBulb)] = new()
        {
            ModuleName = "偽電球",
            Questions = new()
            {
                [SNotTheBulb.Word] = new()
                {
                    // English: What word flashed on {0}?
                    Question = "{0}で点滅していた単語は？",
                },
                [SNotTheBulb.Color] = new()
                {
                    // English: What color was the bulb on {0}?
                    Question = "{0}の電球の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                        ["Purple"] = "紫",
                        ["White"] = "白",
                    },
                },
                [SNotTheBulb.ScrewCap] = new()
                {
                    // English: What was the material of the screw cap on {0}?
                    Question = "{0}の口金の素材は？",
                    Answers = new()
                    {
                        ["Copper"] = "銅",
                        ["Silver"] = "銀",
                        ["Gold"] = "金",
                        ["Plastic"] = "プラスチック",
                        ["Carbon Fibre"] = "カーボンファイバー",
                        ["Ceramic"] = "セラミック",
                    },
                },
            },
        },

        // Not the Button
        [typeof(SNotTheButton)] = new()
        {
            ModuleName = "偽ボタン",
            Questions = new()
            {
                [SNotTheButton.LightColor] = new()
                {
                    // English: What colors did the light glow in {0}?
                    Question = "{0}の光の色は？",
                    Answers = new()
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
            },
        },

        // Not The Plunger Button
        [typeof(SNotThePlungerButton)] = new()
        {
            ModuleName = "偽プランジャーボタン",
            Questions = new()
            {
                [SNotThePlungerButton.Background] = new()
                {
                    // English: What color did the background flash in {0}?
                    Question = "{0}の背景の色は？",
                    Answers = new()
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
            },
        },

        // Not The Screw
        [typeof(SNotTheScrew)] = new()
        {
            ModuleName = "偽ザ・ネジ",
            Questions = new()
            {
                [SNotTheScrew.InitialPosition] = new()
                {
                    // English: What was the initial position in {0}?
                    Question = "{0}の初期位置は？",
                },
            },
        },

        // Not Who’s on First
        [typeof(SNotWhosOnFirst)] = new()
        {
            ModuleName = "偽表比較",
            Questions = new()
            {
                [SNotWhosOnFirst.PressedPosition] = new()
                {
                    // English: In which position was the button you pressed in the {1} stage on {0}?
                    // Example: In which position was the button you pressed in the first stage on Not Who’s on First?
                    Question = "{0}のステージ{1}で押したボタンの位置は？",
                    Answers = new()
                    {
                        ["top left"] = "左上",
                        ["top right"] = "右上",
                        ["middle left"] = "左中央",
                        ["middle right"] = "右中央",
                        ["bottom left"] = "左下",
                        ["bottom right"] = "右下",
                    },
                },
                [SNotWhosOnFirst.PressedLabel] = new()
                {
                    // English: What was the label on the button you pressed in the {1} stage on {0}?
                    // Example: What was the label on the button you pressed in the first stage on Not Who’s on First?
                    Question = "{0}のステージ{1}で押したボタンのラベルは？",
                },
                [SNotWhosOnFirst.ReferencePosition] = new()
                {
                    // English: In which position was the reference button in the {1} stage on {0}?
                    // Example: In which position was the reference button in the first stage on Not Who’s on First?
                    Question = "{0}のステージ{1}で参照したボタンの位置は？",
                    Answers = new()
                    {
                        ["top left"] = "左上",
                        ["top right"] = "右上",
                        ["middle left"] = "左中央",
                        ["middle right"] = "右中央",
                        ["bottom left"] = "左下",
                        ["bottom right"] = "右下",
                    },
                },
                [SNotWhosOnFirst.ReferenceLabel] = new()
                {
                    // English: What was the label on the reference button in the {1} stage on {0}?
                    // Example: What was the label on the reference button in the first stage on Not Who’s on First?
                    Question = "{0}のステージ{1}で参照したボタンのラベルは？",
                },
                [SNotWhosOnFirst.Sum] = new()
                {
                    // English: What was the calculated number in the second stage on {0}?
                    Question = "{0}のステージ2で計算した値は？",
                },
            },
        },

        // Not Word Search
        [typeof(SNotWordSearch)] = new()
        {
            ModuleName = "偽ワードサーチ",
            Questions = new()
            {
                [SNotWordSearch.Missing] = new()
                {
                    // English: Which of these consonants was missing in {0}?
                    Question = "{0}で欠けていた子音は？",
                },
                [SNotWordSearch.FirstPress] = new()
                {
                    // English: What was the first correctly pressed letter in {0}?
                    Question = "{0}の最初に正しく押した英字は？",
                },
            },
        },

        // Not X01
        [typeof(SNotX01)] = new()
        {
            ModuleName = "偽ダーツ",
            Questions = new()
            {
                [SNotX01.SectorValues] = new()
                {
                    // English: Which sector value {1} present on {0}?
                    // Example: Which sector value was present on Not X01?
                    Question = "{0}に存在{1}セクターは？",
                    Arguments = new()
                    {
                        ["was"] = "した",
                        ["was not"] = "しなかった",
                    },
                },
            },
        },

        // Not X-Ray
        [typeof(SNotXRay)] = new()
        {
            ModuleName = "偽レントゲン",
            Questions = new()
            {
                [SNotXRay.ScannerColor] = new()
                {
                    // English: What was the scanner color in {0}?
                    Question = "{0}のスキャナーの色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Blue"] = "青",
                        ["White"] = "白",
                    },
                },
                [SNotXRay.Table] = new()
                {
                    // English: What table were we in in {0} (numbered 1–8 in reading order in the manual)?
                    Question = "{0}で使用した表は(マニュアルの読み順で)？",
                },
                [SNotXRay.Directions] = new()
                {
                    // English: What direction was button {1} in {0}?
                    // Example: What direction was button 1 in Not X-Ray?
                    Question = "{0}のボタン{1}の方向は？",
                    Answers = new()
                    {
                        ["Up"] = "下",
                        ["Right"] = "右",
                        ["Down"] = "下",
                        ["Left"] = "左",
                    },
                },
                [SNotXRay.Buttons] = new()
                {
                    // English: Which button went {1} in {0}?
                    // Example: Which button went up in Not X-Ray?
                    Question = "{0}で{1}方向のボタンは？",
                    Arguments = new()
                    {
                        ["up"] = "下",
                        ["right"] = "右",
                        ["down"] = "下",
                        ["left"] = "左",
                    },
                },
            },
        },

        // Numbered Buttons
        [typeof(SNumberedButtons)] = new()
        {
            ModuleName = "番号ボタン",
            Questions = new()
            {
                [SNumberedButtons.Buttons] = new()
                {
                    // English: Which number was correctly pressed on {0}?
                    Question = "{0}で正しく押された番号は？",
                },
            },
        },

        // The Number Game
        [typeof(SNumberGame)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNumberGame.Maximum] = new()
                {
                    // English: What was the maximum number in {0}?
                    Question = "{0}の最大値は？",
                },
            },
        },

        // Numbers
        [typeof(SNumbers)] = new()
        {
            ModuleName = "ナンバー",
            Questions = new()
            {
                [SNumbers.TwoDigit] = new()
                {
                    // English: What two-digit number was given in {0}?
                    Question = "{0}で与えられた二桁の数字は？",
                },
            },
        },

        // Numpath
        [typeof(SNumpath)] = new()
        {
            ModuleName = "ナンパス",
            Questions = new()
            {
                [SNumpath.Color] = new()
                {
                    // English: What was the color of the number on {0}?
                    Question = "{0}の数字の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Orange"] = "オレンジ",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Purple"] = "紫",
                    },
                },
                [SNumpath.Digit] = new()
                {
                    // English: What was the number displayed on {0}?
                    Question = "{0}に表示された数字は？",
                },
            },
        },

        // Object Shows
        [typeof(SObjectShows)] = new()
        {
            ModuleName = "オブジェクトショー",
            Questions = new()
            {
                [SObjectShows.Contestants] = new()
                {
                    // English: Which of these was a contestant on {0}?
                    Question = "{0}の参加者に含まれるのは？",
                },
            },
        },

        // The Octadecayotton
        [typeof(SOctadecayotton)] = new()
        {
            ModuleName = "9次元超立方体",
            Questions = new()
            {
                [SOctadecayotton.Sphere] = new()
                {
                    // English: What was the starting sphere in {0}?
                    Question = "{0}のスタートボールは？",
                },
                [SOctadecayotton.Rotations] = new()
                {
                    // English: What was one of the subrotations in the {1} rotation in {0}?
                    // Example: What was one of the subrotations in the first rotation in The Octadecayotton?
                    Question = "{0}で{1}番目の回転の二次変形の一つであるのは？",
                },
            },
        },

        // Odd One Out
        [typeof(SOddOneOut)] = new()
        {
            ModuleName = "仲間外れ",
            Questions = new()
            {
                [SOddOneOut.Button] = new()
                {
                    // English: What was the button you pressed in the {1} stage of {0}?
                    // Example: What was the button you pressed in the first stage of Odd One Out?
                    Question = "{0}のステージ{1}で押されたボタンは？",
                },
            },
        },

        // Off Keys
        [typeof(SOffKeys)] = new()
        {
            ModuleName = "音痴鍵盤",
            Questions = new()
            {
                [SOffKeys.IncorrectPitch] = new()
                {
                    // English: Which of these keys played at an incorrect pitch in {0}?
                    Question = "{0}で不正確な音程で鳴った鍵盤は？",
                },
                [SOffKeys.Runes] = new()
                {
                    // English: Which of these runes was displayed in {0}?
                    Question = "{0}に表示されたルーン文字は？",
                },
            },
        },

        // Off-White Cipher
        [typeof(SOffWhiteCipher)] = new()
        {
            ModuleName = "乳白色暗号",
            Questions = new()
            {
                [SOffWhiteCipher.TopDisplay] = new()
                {
                    // English: What was on the top display in {0}?
                    Question = "{0}の上部ディスプレーに表示されていたのは？",
                },
                [SOffWhiteCipher.MiddleDisplay] = new()
                {
                    // English: What was on the middle display in {0}?
                    Question = "{0}の中央ディスプレーに表示されていたのは？",
                },
                [SOffWhiteCipher.BottomDisplay] = new()
                {
                    // English: What was on the bottom display in {0}?
                    Question = "{0}の下部ディスプレーに表示されていたのは？",
                },
            },
        },

        // Old AI
        [typeof(SOldAI)] = new()
        {
            ModuleName = "オールドAI",
            Questions = new()
            {
                [SOldAI.Group] = new()
                {
                    // English: What was the {1} of the numbers shown in {0}?
                    // Example: What was the group of the numbers shown in Old AI?
                    Question = "{0}で表示された数字の{1}は？",
                    Arguments = new()
                    {
                        ["group"] = "グループ",
                        ["sub-group"] = "サブグループ",
                    },
                },
            },
        },

        // Old Fogey
        [typeof(SOldFogey)] = new()
        {
            ModuleName = "耄碌爺",
            Questions = new()
            {
                [SOldFogey.StartingColor] = new()
                {
                    // English: What was the initial color of the status light in {0}?
                    Question = "{0}の初期状態におけるステータスライトの色は？",
                    Answers = new()
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
            },
        },

        // One Links To All
        [typeof(SOneLinksToAll)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOneLinksToAll.Start] = new()
                {
                    // English: What was the starting article in {0}?
                    Question = "{0}の初期記事は？",
                },
                [SOneLinksToAll.End] = new()
                {
                    // English: What was the ending article in {0}?
                    Question = "{0}の最終記事は？",
                },
            },
        },

        // Only Connect
        [typeof(SOnlyConnect)] = new()
        {
            ModuleName = "オンリーコネクト",
            Questions = new()
            {
                [SOnlyConnect.Hieroglyphs] = new()
                {
                    // English: Which Egyptian hieroglyph was in the {1} in {0}?
                    // Example: Which Egyptian hieroglyph was in the top left in Only Connect?
                    Question = "{0}の{1}のヒエログリフは？",
                    Arguments = new()
                    {
                        ["top left"] = "左上",
                        ["top middle"] = "上",
                        ["top right"] = "右上",
                        ["bottom left"] = "左下",
                        ["bottom middle"] = "下",
                        ["bottom right"] = "右下",
                    },
                    Answers = new()
                    {
                        ["Two Reeds"] = "二本の葦",
                        ["Lion"] = "ライオン",
                        ["Twisted Flax"] = "よりあわせた亜麻糸",
                        ["Horned Viper"] = "ヘビ",
                        ["Water"] = "水",
                        ["Eye of Horus"] = "ホルスの目",
                    },
                },
            },
        },

        // Orange Arrows
        [typeof(SOrangeArrows)] = new()
        {
            ModuleName = "橙色矢印",
            Questions = new()
            {
                [SOrangeArrows.Sequences] = new()
                {
                    // English: What was the {1} arrow on the display of the {2} stage of {0}?
                    // Example: What was the first arrow on the display of the first stage of Orange Arrows?
                    Question = "{0}のステージ{2}における{1}番目の矢印は？",
                    Answers = new()
                    {
                        ["Up"] = "下",
                        ["Right"] = "右",
                        ["Down"] = "下",
                        ["Left"] = "左",
                    },
                },
            },
        },

        // Orange Cipher
        [typeof(SOrangeCipher)] = new()
        {
            ModuleName = "橙色暗号",
            Questions = new()
            {
                [SOrangeCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Orange Cipher?
                    Question = "{0}の答えは？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Ordered Keys
        [typeof(SOrderedKeys)] = new()
        {
            ModuleName = "順番音板",
            Questions = new()
            {
                [SOrderedKeys.Colors] = new()
                {
                    // English: What color was this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "{0}のステージ{1}におけるこの音板の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                    },
                },
                [SOrderedKeys.Labels] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What was the label of this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "{0}のステージ{1}におけるこの音板のラベルは？",
                },
                [SOrderedKeys.LabelColors] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was the label of this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "{0}のステージ{1}におけるこの音板のラベルの色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                    },
                },
            },
        },

        // Order Picking
        [typeof(SOrderPicking)] = new()
        {
            ModuleName = "注文ピッキング",
            Questions = new()
            {
                [SOrderPicking.Order] = new()
                {
                    // English: What was the order ID in the {1} order of {0}?
                    // Example: What was the order ID in the first order of Order Picking?
                    Question = "{0}の{1}番目の注文IDは？",
                },
                [SOrderPicking.Product] = new()
                {
                    // English: What was the product ID in the {1} order of {0}?
                    // Example: What was the product ID in the first order of Order Picking?
                    Question = "{0}の{1}番目の製品IDは？",
                },
                [SOrderPicking.Pallet] = new()
                {
                    // English: What was the pallet in the {1} order of {0}?
                    // Example: What was the pallet in the first order of Order Picking?
                    Question = "{0}の{1}番目の注文パレットは？",
                },
            },
        },

        // Orientation Cube
        [typeof(SOrientationCube)] = new()
        {
            ModuleName = "方向キューブ",
            Questions = new()
            {
                [SOrientationCube.InitialObserverPosition] = new()
                {
                    // English: What was the observer’s initial position in {0}?
                    Question = "{0}の最初の観測者の位置は？",
                    Answers = new()
                    {
                        ["front"] = "正面",
                        ["left"] = "左",
                        ["back"] = "後",
                        ["right"] = "右",
                    },
                },
            },
        },

        // Orientation Hypercube
        [typeof(SOrientationHypercube)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOrientationHypercube.InitialFaceColour] = new()
                {
                    // English: What was the initial colour of the {1} face in {0}?
                    // Example: What was the initial colour of the right face in Orientation Hypercube?
                    Question = "{0}の初期状態における{1}面の色は？",
                    Arguments = new()
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
                    Answers = new()
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
                [SOrientationHypercube.InitialObserverPosition] = new()
                {
                    // English: What was the observer’s initial position in {0}?
                    Question = "{0}の最初の観測者の位置は？",
                    Answers = new()
                    {
                        ["front"] = "正面",
                        ["left"] = "左",
                        ["back"] = "後",
                        ["right"] = "右",
                    },
                },
            },
        },

        // Painting Cube
        [typeof(SPaintingCube)] = new()
        {
            ModuleName = "色塗りキューブ",
            Questions = new()
            {
                [SPaintingCube.MissingColor] = new()
                {
                    // English: What color was missing in {0}?
                    Question = "{0}で欠けていた色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Orange"] = "橙",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Indigo"] = "藍",
                        ["Violet"] = "紫",
                    },
                },
            },
        },

        // Palindromes
        [typeof(SPalindromes)] = new()
        {
            ModuleName = "回文",
            Questions = new()
            {
                [SPalindromes.Numbers] = new()
                {
                    // English: What was {1}’s {2} digit from the right in {0}?
                    // Example: What was X’s first digit from the right in Palindromes?
                    Question = "{0}で{1}の右から{2}桁目は？",
                    Arguments = new()
                    {
                        ["X"] = "X",
                        ["Y"] = "Y",
                        ["Z"] = "Z",
                        ["the screen"] = "ディスプレー",
                    },
                },
            },
        },

        // Papa’s Pizzeria
        [typeof(SPapasPizzeria)] = new()
        {
            ModuleName = "パパのピザ屋さん",
            Questions = new()
            {
                [SPapasPizzeria.Letter] = new()
                {
                    // English: What was the letter in the order number on {0}?
                    Question = "{0}の注文番号の英字は？",
                },
                [SPapasPizzeria.Digit] = new()
                {
                    // English: What was the {1} digit in the order number on {0}?
                    // Example: What was the first digit in the order number on Papa’s Pizzeria?
                    Question = "{0}の注文番号の{1}桁目は？",
                },
            },
        },

        // Parity
        [typeof(SParity)] = new()
        {
            ModuleName = "偶奇性",
            Questions = new()
            {
                [SParity.Display] = new()
                {
                    // English: What was shown on the display on {0}?
                    Question = "{0}に表示されたのは？",
                },
            },
        },

        // Partial Derivatives
        [typeof(SPartialDerivatives)] = new()
        {
            ModuleName = "偏微分",
            Questions = new()
            {
                [SPartialDerivatives.LedColors] = new()
                {
                    // English: What was the LED color in the {1} stage of {0}?
                    // Example: What was the LED color in the first stage of Partial Derivatives?
                    Question = "{0}のステージ{1}におけるLEDの色は？",
                    Answers = new()
                    {
                        ["blue"] = "青",
                        ["green"] = "緑",
                        ["orange"] = "オレンジ",
                        ["purple"] = "紫",
                        ["red"] = "赤",
                        ["yellow"] = "黄",
                    },
                },
                [SPartialDerivatives.Terms] = new()
                {
                    // English: What was the {1} term in {0}?
                    // Example: What was the first term in Partial Derivatives?
                    Question = "{0}の{1}番目の項は？",
                },
            },
        },

        // Passport Control
        [typeof(SPassportControl)] = new()
        {
            ModuleName = "パスポートコントロール",
            Questions = new()
            {
                [SPassportControl.Passenger] = new()
                {
                    // English: What was the passport expiration year of the {1} inspected passenger in {0}?
                    // Example: What was the passport expiration year of the first inspected passenger in Passport Control?
                    Question = "{0}で{1}番目に搭乗した乗客のパスポート有効期限は？",
                },
            },
        },

        // Password Destroyer
        [typeof(SPasswordDestroyer)] = new()
        {
            ModuleName = "パスワード破壊",
            Questions = new()
            {
                [SPasswordDestroyer.TwoFactorV2] = new()
                {
                    // English: What was the 2FAST™ value when you solved {0}?
                    Question = "{0}を解除した時点の2FAST™の値は？",
                },
            },
        },

        // Pattern Cube
        [typeof(SPatternCube)] = new()
        {
            ModuleName = "パターンキューブ",
            Questions = new()
            {
                [SPatternCube.HighlightedSymbol] = new()
                {
                    // English: Which symbol was highlighted in {0}?
                    Question = "{0}でハイライトされた記号は？",
                },
            },
        },

        // Pattern Recognition
        [typeof(SPatternRecognition)] = new()
        {
            ModuleName = "パターン識別",
            Questions = new()
            {
                [SPatternRecognition.Pattern] = new()
                {
                    // English: What was the repeating pattern in {0}?
                    Question = "{0}の繰り返しパターンは？",
                },
            },
        },

        // The Pentabutton
        [typeof(SPentabutton)] = new()
        {
            ModuleName = "五角形ボタン",
            Questions = new()
            {
                [SPentabutton.BaseColor] = new()
                {
                    // English: What was the base colour in {0}?
                    Question = "{0}のベースの色は？",
                    Answers = new()
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
            },
            Discriminators = new()
            {
                [SPentabutton.Label] = new()
                {
                    // English: the Pentabutton labelled “{0}”
                    // Example: the Pentabutton labelled “press”
                    Discriminator = "「{0}」が書かれた五角形ボタン",
                },
            },
        },

        // Periodic Words
        [typeof(SPeriodicWords)] = new()
        {
            ModuleName = "周期単語",
            Questions = new()
            {
                [SPeriodicWords.DisplayedWords] = new()
                {
                    // English: What word was on the display in the {1} stage of {0}?
                    // Example: What word was on the display in the first stage of Periodic Words?
                    Question = "{0}のステージ{1}で表示された単語は？",
                },
            },
        },

        // Perspective Pegs
        [typeof(SPerspectivePegs)] = new()
        {
            ModuleName = "奥行きペグ",
            Questions = new()
            {
                [SPerspectivePegs.ColorSequence] = new()
                {
                    // English: What was the {1} color in the initial sequence in {0}?
                    // Example: What was the first color in the initial sequence in Perspective Pegs?
                    Question = "{0}の初期色シーケンスで{1}番目の色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["yellow"] = "黄",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["purple"] = "紫",
                    },
                },
            },
        },

        // Phosphorescence
        [typeof(SPhosphorescence)] = new()
        {
            ModuleName = "燐光",
            Questions = new()
            {
                [SPhosphorescence.ButtonPresses] = new()
                {
                    // English: What was the {1} button press in {0}?
                    // Example: What was the first button press in Phosphorescence?
                    Question = "{0}の{1}番目に押したボタンは？",
                    Answers = new()
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
                [SPhosphorescence.Offset] = new()
                {
                    // English: What was the offset in {0}?
                    Question = "{0}のオフセットは？",
                },
            },
        },

        // Pickup Identification
        [typeof(SPickupIdentification)] = new()
        {
            ModuleName = "アイテム識別",
            Questions = new()
            {
                [SPickupIdentification.Item] = new()
                {
                    // English: What pickup was shown in the {1} stage of {0}?
                    // Example: What pickup was shown in the first stage of Pickup Identification?
                    Question = "{0}のステージ{1}で表示されたアイテムは？",
                },
            },
        },

        // Pictionary
        [typeof(SPictionary)] = new()
        {
            ModuleName = "画像ロジック",
            Questions = new()
            {
                [SPictionary.Code] = new()
                {
                    // English: What was the code in {0}?
                    Question = "{0}のコードは？",
                },
            },
        },

        // Pie
        [typeof(SPie)] = new()
        {
            ModuleName = "パイ",
            Questions = new()
            {
                [SPie.Digits] = new()
                {
                    // English: What was the {1} digit of the displayed number in {0}?
                    // Example: What was the first digit of the displayed number in Pie?
                    Question = "{0}で{1}番目に表示された数字は？",
                },
            },
        },

        // Pie Flash
        [typeof(SPieFlash)] = new()
        {
            ModuleName = "点滅パイ",
            Questions = new()
            {
                [SPieFlash.Digits] = new()
                {
                    // English: What number was not displayed in {0}?
                    Question = "{0}で表示されていない番号は？",
                },
            },
        },

        // Pigpen Cycle
        [typeof(SPigpenCycle)] = new()
        {
            ModuleName = "ピッグペンサイクル",
            Questions = new()
            {
                [SPigpenCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Pigpen Cycle?
                    Question = "{0}の{1}番目のダイヤルが指していた方向は？",
                },
                [SPigpenCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Pigpen Cycle?
                    Question = "{0}の{1}番目のダイヤルに書かれていた文字は？",
                },
            },
            Discriminators = new()
            {
                [SPigpenCycle.LabelDiscriminator] = new()
                {
                    // English: the Pigpen Cycle that had the letter {0} on a dial
                    // Example: the Pigpen Cycle that had the letter A on a dial
                    Discriminator = "ダイヤルに{0}の英字が書かれたピッグペンサイクル",
                },
            },
        },

        // The Pink Button
        [typeof(SPinkButton)] = new()
        {
            ModuleName = "桃色ボタン",
            Questions = new()
            {
                [SPinkButton.Words] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the first word in The Pink Button?
                    Question = "{0}の{1}番目の単語は？",
                },
                [SPinkButton.Colors] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in The Pink Button?
                    Question = "{0}の{1}番目の色は？",
                    Answers = new()
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
            },
        },

        // Pinpoint
        [typeof(SPinpoint)] = new()
        {
            ModuleName = "ピンポイント",
            Questions = new()
            {
                [SPinpoint.Points] = new()
                {
                    // English: Which point occurred in {0}?
                    Question = "{0}で発生した点は？",
                },
                [SPinpoint.Distances] = new()
                {
                    // English: Which distance occurred in {0}?
                    Question = "{0}で発生した距離は？",
                },
            },
        },

        // Pixel Cipher
        [typeof(SPixelCipher)] = new()
        {
            ModuleName = "ピクセル暗号",
            Questions = new()
            {
                [SPixelCipher.Keyword] = new()
                {
                    // English: What was the keyword in {0}?
                    Question = "{0}のキーワードは？",
                },
            },
        },

        // Placeholder Talk
        [typeof(SPlaceholderTalk)] = new()
        {
            ModuleName = "プレースホルダートーク",
            Questions = new()
            {
                [SPlaceholderTalk.FirstPhrase] = new()
                {
                    // English: What was the first half of the first phrase in {0}?
                    Question = "{0}の一つ目のフレーズの前半は？",
                },
                [SPlaceholderTalk.Ordinal] = new()
                {
                    // English: What was the last half of the first phrase in {0}?
                    Question = "{0}の一つ目のフレーズの後半は？",
                },
            },
        },

        // Placement Roulette
        [typeof(SPlacementRoulette)] = new()
        {
            ModuleName = "マリオカート",
            Questions = new()
            {
                [SPlacementRoulette.Char] = new()
                {
                    // English: What was the character listed on the information display in {0}?
                    Question = "{0}の情報一覧に掲載されていたキャラクターは？",
                },
                [SPlacementRoulette.Track] = new()
                {
                    // English: What was the track listed on the information display in {0}?
                    Question = "{0}の情報一覧に掲載されていたトラックは？",
                },
                [SPlacementRoulette.Vehicle] = new()
                {
                    // English: What was the vehicle listed on the information display in {0}?
                    Question = "{0}の情報一覧に掲載されていた車両は？",
                },
            },
        },

        // Planets
        [typeof(SPlanets)] = new()
        {
            ModuleName = "惑星",
            Questions = new()
            {
                [SPlanets.Strips] = new()
                {
                    // English: What was the color of the {1} strip (from the top) in {0}?
                    // Example: What was the color of the first strip (from the top) in Planets?
                    Question = "{0}の上から{1}番目のストリップの色は？",
                    Answers = new()
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
                [SPlanets.Planet] = new()
                {
                    // English: What was the planet shown in {0}?
                    Question = "{0}には何の惑星が表示されていた？",
                },
            },
        },

        // Playfair Cycle
        [typeof(SPlayfairCycle)] = new()
        {
            ModuleName = "プレイフェアサイクル",
            Questions = new()
            {
                [SPlayfairCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Playfair Cycle?
                    Question = "{0}で{1}番目のダイヤルが指していた方向は？",
                },
                [SPlayfairCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Playfair Cycle?
                    Question = "{0}で{1}番目のダイヤルに書かれていた文字は？",
                },
            },
            Discriminators = new()
            {
                [SPlayfairCycle.LabelDiscriminator] = new()
                {
                    // English: the Playfair Cycle that had the letter {0} on a dial
                    // Example: the Playfair Cycle that had the letter A on a dial
                    Discriminator = "ダイヤルに{0}の英字が書かれたプレイフェアサイクル",
                },
            },
        },

        // Poetry
        [typeof(SPoetry)] = new()
        {
            ModuleName = "詩",
            Questions = new()
            {
                [SPoetry.Answers] = new()
                {
                    // English: What was the {1} correct answer you pressed in {0}?
                    // Example: What was the first correct answer you pressed in Poetry?
                    Question = "{0}において、{1}番目に押して正解だったフレーズは？",
                },
            },
        },

        // Pointless Machines
        [typeof(SPointlessMachines)] = new()
        {
            ModuleName = "無意味なマシーン",
            Questions = new()
            {
                [SPointlessMachines.Flashes] = new()
                {
                    // English: What color flashed {1} in {0}?
                    // Example: What color flashed first in Pointless Machines?
                    Question = "{0}で{1}番目に点滅した色は？",
                    Answers = new()
                    {
                        ["White"] = "白",
                        ["Purple"] = "紫",
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                    },
                },
            },
        },

        // Polygons
        [typeof(SPolygons)] = new()
        {
            ModuleName = "多角形",
            Questions = new()
            {
                [SPolygons.Polygon] = new()
                {
                    // English: Which polygon was present on {0}?
                    Question = "{0}で表示された図形は？",
                },
            },
        },

        // Polyhedral Maze
        [typeof(SPolyhedralMaze)] = new()
        {
            ModuleName = "多面体迷路",
            Questions = new()
            {
                [SPolyhedralMaze.StartPosition] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "{0}の開始番号は？",
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
                        ["the 4-truncated deltoidal icositetrahedral Polyhedral Maze"] = "四切頂凧形二十四面体の多面体迷路",
                        ["the chamfered dodecahedral Polyhedral Maze"] = "切稜十二面体の多面体迷路",
                        ["the chamfered icosahedral Polyhedral Maze"] = "切稜二十面体の多面体迷路",
                        ["the deltoidal hexecontahedral Polyhedral Maze"] = "切稜六十面体の多面体迷路",
                        ["the disdyakis dodecahedral Polyhedral Maze"] = "六方二十面体の多面体迷路",
                        ["the joined snub cubic Polyhedral Maze"] = "菱方変形立方体の多面体迷路",
                        ["the joined rhombicuboctahedral Polyhedral Maze"] = "斜方菱方八面体の多面体迷路",
                        ["the pentagonal hexecontahedral Polyhedral Maze"] = "五角六十面体の多面体迷路",
                        ["the orthokis propello cubic Polyhedral Maze"] = "凧方プロペラキューブの多面体迷路",
                        ["the pentakis dodecahedral Polyhedral Maze"] = "五方十二面体の多面体迷路",
                        ["the rectified rhombicuboctahedral Polyhedral Maze"] = "切頂斜方立方八面体の多面体迷路",
                        ["the triakis icosahedral Polyhedral Maze"] = "三方二十面体の多面体迷路",
                        ["the rhombicosidodecahedral Polyhedral Maze"] = "斜方二十・十二面体の多面体迷路",
                        ["the canonical rectified snub cubic Polyhedral Maze"] = "一様切頂変形立方体の多面体迷路",
                    },
                },
            },
        },

        // Prime Encryption
        [typeof(SPrimeEncryption)] = new()
        {
            ModuleName = "素数暗号",
            Questions = new()
            {
                [SPrimeEncryption.DisplayedValue] = new()
                {
                    // English: What was the number shown in {0}?
                    Question = "{0}に表示されていた数字は？",
                },
            },
        },

        // Prison Break
        [typeof(SPrisonBreak)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SPrisonBreak.Prisoner] = new()
                {
                    // English: Which cell did the prisoner start in in {0}?
                    Question = "{0}で囚人がスタートした部屋は？",
                },
                [SPrisonBreak.Defuser] = new()
                {
                    // English: Where did you start in {0}?
                    Question = "{0}であなたがスタートした部屋は？",
                },
            },
        },

        // Probing
        [typeof(SProbing)] = new()
        {
            ModuleName = "回路接続",
            Questions = new()
            {
                [SProbing.Frequencies] = new()
                {
                    // English: What was the missing frequency in the {1} wire in {0}?
                    // Example: What was the missing frequency in the red-white wire in Probing?
                    Question = "{0}において、{1}のワイヤに含まれていなかった周波数は？",
                    Arguments = new()
                    {
                        ["red-white"] = "赤白",
                        ["yellow-black"] = "黄黒",
                        ["green"] = "緑",
                        ["gray"] = "灰",
                        ["yellow-red"] = "黄赤",
                        ["red-blue"] = "赤青",
                    },
                },
            },
        },

        // Procedural Maze
        [typeof(SProceduralMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SProceduralMaze.InitialSeed] = new()
                {
                    // English: What was the initial seed in {0}?
                    Question = "{0}の初期シードは？",
                },
            },
        },

        // ...?
        [typeof(SPunctuationMarks)] = new()
        {
            ModuleName = "...？",
            Questions = new()
            {
                [SPunctuationMarks.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "{0}で最初に表示された数字は？",
                },
            },
        },

        // Purple Arrows
        [typeof(SPurpleArrows)] = new()
        {
            ModuleName = "紫色矢印",
            Questions = new()
            {
                [SPurpleArrows.Finish] = new()
                {
                    // English: What was the target word on {0}?
                    Question = "{0}のターゲット単語は？",
                },
            },
        },

        // The Purple Button
        [typeof(SPurpleButton)] = new()
        {
            ModuleName = "紫色ボタン",
            Questions = new()
            {
                [SPurpleButton.Numbers] = new()
                {
                    // English: What was the {1} number in the cyclic sequence on {0}?
                    // Example: What was the first number in the cyclic sequence on The Purple Button?
                    Question = "{0}におけるシーケンスの{1}番目の数字は？",
                },
            },
        },

        // Puzzle Identification
        [typeof(SPuzzleIdentification)] = new()
        {
            ModuleName = "パズル識別",
            Questions = new()
            {
                [SPuzzleIdentification.Num] = new()
                {
                    // English: What was the {1} puzzle number in {0}?
                    // Example: What was the first puzzle number in Puzzle Identification?
                    Question = "{0}の{1}回目の数字は？",
                },
                [SPuzzleIdentification.Game] = new()
                {
                    // English: What game was the {1} puzzle in {0} from?
                    // Example: What game was the first puzzle in Puzzle Identification from?
                    Question = "{0}の{1}回目に使用されたゲームの種類は？",
                    Answers = new()
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
                [SPuzzleIdentification.Name] = new()
                {
                    // English: What was the {1} puzzle in {0}?
                    // Example: What was the first puzzle in Puzzle Identification?
                    Question = "{0}の{1}回目のパズル名は？",
                },
            },
        },

        // Puzzling Hexabuttons
        [typeof(SPuzzlingHexabuttons)] = new()
        {
            ModuleName = "困惑六角形ボタン",
            Questions = new()
            {
                [SPuzzlingHexabuttons.Letter] = new()
                {
                    // English: What letter was displayed on the {1} hexabutton when submitting in {0}?
                    // Example: What letter was displayed on the top-left hexabutton when submitting in Puzzling Hexabuttons?
                    Question = "{0}の{1}の六角形ボタンに表示されていた英字は？",
                    Arguments = new()
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
            },
        },

        // Q & A
        [typeof(SQnA)] = new()
        {
            ModuleName = "Q&A",
            Questions = new()
            {
                [SQnA.Questions] = new()
                {
                    // English: What was the {1} question asked in {0}?
                    // Example: What was the first question asked in Q & A?
                    Question = "{0}の{1}番目の質問は？",
                },
            },
        },

        // Quadrants
        [typeof(SQuadrants)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQuadrants.Buttons] = new()
                {
                    // English: What was on the {1} button of the {2} stage in {0}?
                    // Example: What was on the first button of the first stage in Quadrants?
                    Question = "{0}のステージ{2}における{1}番目のボタンに書かれていたのは？",
                },
            },
        },

        // Quantum Passwords
        [typeof(SQuantumPasswords)] = new()
        {
            ModuleName = "量子パスワード",
            Questions = new()
            {
                [SQuantumPasswords.Word] = new()
                {
                    // English: Which word was used in {0}?
                    Question = "{0}で使用された単語は？",
                },
            },
        },

        // Quantum Ternary Converter
        [typeof(SQuantumTernaryConverter)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SQuantumTernaryConverter.Number] = new()
                {
                    // English: Which number was shown in {0}?
                    Question = "{0}に表示された数字は？",
                },
            },
        },

        // Quaver
        [typeof(SQuaver)] = new()
        {
            Questions = new()
            {
                [SQuaver.Arrows] = new()
                {
                    // English: What was the {1} sequence’s answer in {0}?
                    // Example: What was the first sequence’s answer in Quaver?
                    Question = "{0}の{1}番目のシークエンスの回答は？",
                },
            },
        },

        // Question Mark
        [typeof(SQuestionMark)] = new()
        {
            ModuleName = "疑問符",
            Questions = new()
            {
                [SQuestionMark.FlashedSymbols] = new()
                {
                    // English: Which of these symbols was part of the flashing sequence in {0}?
                    Question = "{0}の点滅したシークエンスの一部に含まれるのは？",
                },
            },
        },

        // Quick Arithmetic
        [typeof(SQuickArithmetic)] = new()
        {
            ModuleName = "瞬速計算",
            Questions = new()
            {
                [SQuickArithmetic.Colors] = new()
                {
                    // English: What was the {1} color in the primary sequence in {0}?
                    // Example: What was the first color in the primary sequence in Quick Arithmetic?
                    Question = "{0}の初期シーケンスにおける{1}番目の色は？",
                    Answers = new()
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
                [SQuickArithmetic.PrimSecDigits] = new()
                {
                    // English: What was the {1} digit in the {2} sequence in {0}?
                    // Example: What was the first digit in the primary sequence in Quick Arithmetic?
                    Question = "{0}の{2}シーケンスにおける{1}番目の数字は？",
                    Arguments = new()
                    {
                        ["primary"] = "一次",
                        ["secondary"] = "二次",
                    },
                },
            },
        },

        // Quintuples
        [typeof(SQuintuples)] = new()
        {
            ModuleName = "五重",
            Questions = new()
            {
                [SQuintuples.Numbers] = new()
                {
                    // English: What was the {1} digit in the {2} slot in {0}?
                    // Example: What was the first digit in the first slot in Quintuples?
                    Question = "{0}の{2}番目のスロットの{1}番目の数字は？",
                },
                [SQuintuples.Colors] = new()
                {
                    // English: What color was the {1} digit in the {2} slot in {0}?
                    // Example: What color was the first digit in the first slot in Quintuples?
                    Question = "{0}の{2}番目のスロットの{1}番目の数字の色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["orange"] = "オレンジ",
                        ["green"] = "緑",
                        ["pink"] = "ピンク",
                    },
                },
                [SQuintuples.ColorCounts] = new()
                {
                    // English: How many numbers were {1} in {0}?
                    // Example: How many numbers were red in Quintuples?
                    Question = "{0}で{1}は何回出現した？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["orange"] = "オレンジ",
                        ["green"] = "緑",
                        ["pink"] = "ピンク",
                    },
                },
            },
        },

        // Quiplash
        [typeof(SQuiplash)] = new()
        {
            ModuleName = "クイプラッシュ",
            Questions = new()
            {
                [SQuiplash.Number] = new()
                {
                    // English: What number was shown on {0}?
                    Question = "{0}に表示された数字は？",
                },
            },
        },

        // Quiz Buzz
        [typeof(SQuizBuzz)] = new()
        {
            ModuleName = "クィズバズ",
            Questions = new()
            {
                [SQuizBuzz.StartingNumber] = new()
                {
                    // English: What was the number initially on the display in {0}?
                    Question = "{0}のディスプレーに最初に表示された数字は？",
                },
            },
        },

        // Qwirkle
        [typeof(SQwirkle)] = new()
        {
            ModuleName = "クワークル",
            Questions = new()
            {
                [SQwirkle.TilesPlaced] = new()
                {
                    // English: What tile did you place {1} in {0}?
                    // Example: What tile did you place first in Qwirkle?
                    Question = "{0}で{1}番目に置いたタイルは？",
                },
            },
        },

        // Raiding Temples
        [typeof(SRaidingTemples)] = new()
        {
            ModuleName = "神殿探検",
            Questions = new()
            {
                [SRaidingTemples.StartingCommonPool] = new()
                {
                    // English: How many jewels were in the starting common pool in {0}?
                    Question = "{0}で初期の共有財産にあった宝石の数は？",
                },
            },
        },

        // Railway Cargo Loading
        [typeof(SRailwayCargoLoading)] = new()
        {
            ModuleName = "鉄道貨物積載センター",
            Questions = new()
            {
                [SRailwayCargoLoading.Cars] = new()
                {
                    // English: What was the {1} car in {0}?
                    // Example: What was the first car in Railway Cargo Loading?
                    Question = "{0}の{1}両目は？",
                },
                [SRailwayCargoLoading.FreightTableRules] = new()
                {
                    // English: Which freight table rule {1} in {0}?
                    // Example: Which freight table rule was met in Railway Cargo Loading?
                    Question = "{0}の貨車検索表で{1}ルールは？",
                    Arguments = new()
                    {
                        ["was met"] = "合致した",
                        ["wasn’t met"] = "合致しなかった",
                    },
                },
            },
        },

        // Rainbow Arrows
        [typeof(SRainbowArrows)] = new()
        {
            ModuleName = "虹色矢印",
            Questions = new()
            {
                [SRainbowArrows.Number] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "{0}のディスプレーの数字は？",
                },
            },
        },

        // Recolored Switches
        [typeof(SRecoloredSwitches)] = new()
        {
            ModuleName = "色変えスイッチ",
            Questions = new()
            {
                [SRecoloredSwitches.LedColors] = new()
                {
                    // English: What was the color of the {1} LED in {0}?
                    // Example: What was the color of the first LED in Recolored Switches?
                    Question = "{0}の{1}番目の位置にあるLEDの色は？",
                    Answers = new()
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
            },
        },

        // Recursive Password
        [typeof(SRecursivePassword)] = new()
        {
            ModuleName = "桃色ボタン",
            Questions = new()
            {
                [SRecursivePassword.NonPasswordWords] = new()
                {
                    // English: Which of these words appeared, but was not the password, in {0}?
                    Question = "{0}で出現したがパスワードではなかった単語は？",
                },
                [SRecursivePassword.Password] = new()
                {
                    // English: What was the password in {0}?
                    Question = "{0}のパスワードは？",
                },
            },
        },

        // Red Arrows
        [typeof(SRedArrows)] = new()
        {
            ModuleName = "赤色矢印",
            Questions = new()
            {
                [SRedArrows.StartNumber] = new()
                {
                    // English: What was the starting number in {0}?
                    Question = "{0}の開始地点の数字は？",
                },
            },
        },

        // Red Button’t
        [typeof(SRedButtont)] = new()
        {
            ModuleName = "偽赤色ボタン",
            Questions = new()
            {
                [SRedButtont.Word] = new()
                {
                    // English: What was the word before “SUBMIT” in {0}?
                    Question = "{0}で「SUBMIT」の直前に表示された単語は？",
                },
            },
        },

        // Red Cipher
        [typeof(SRedCipher)] = new()
        {
            ModuleName = "赤色暗号",
            Questions = new()
            {
                [SRedCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Red Cipher?
                    Question = "{0}の回答は？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Red Herring
        [typeof(SRedHerring)] = new()
        {
            ModuleName = "レッドヘリング",
            Questions = new()
            {
                [SRedHerring.FirstFlash] = new()
                {
                    // English: What was the first color flashed by {0}?
                    Question = "{0}において、最初に点滅した色は？",
                },
            },
        },

        // Reformed Role Reversal
        [typeof(SReformedRoleReversal)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SReformedRoleReversal.Condition] = new()
                {
                    // English: Which condition was the solving condition in {0}?
                    Question = "{0}の解除条件は？",
                    Answers = new()
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
                [SReformedRoleReversal.Wire] = new()
                {
                    // English: What color was the {1} wire in {0}?
                    // Example: What color was the first wire in Reformed Role Reversal?
                    Question = "{0}の{1}番目のワイヤの色は？",
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
            NeedsTranslation = true,
            Questions = new()
            {
                [SReGretBFiltering.Operator] = new()
                {
                    // English: Which calculation was used for the {1} stage of {0}?
                    // Example: Which calculation was used for the first stage of ReGret-B Filtering?
                    Question = "{0}のステージ{1}で使用された計算は？",
                },
            },
        },

        // Regular Crazy Talk
        [typeof(SRegularCrazyTalk)] = new()
        {
            ModuleName = "レギュラークレイジートーク",
            Questions = new()
            {
                [SRegularCrazyTalk.Digit] = new()
                {
                    // English: What was the displayed digit that corresponded to the solution phrase in {0}?
                    Question = "{0}において、回答のフレーズに表示されていた数字は？",
                },
                [SRegularCrazyTalk.Modifier] = new()
                {
                    // English: What was the embellishment of the solution phrase in {0}?
                    Question = "{0}の回答のフレーズの装飾は？",
                    Answers = new()
                    {
                        ["[PHRASE]"] = "[フレーズ]",
                        ["It says: [PHRASE]"] = "It says: [フレーズ]",
                        ["Quote: [PHRASE] End quote"] = "Quote: [フレーズ] End quote",
                        ["“[PHRASE]”"] = "“[フレーズ]”",
                        ["It says: “[PHRASE]”"] = "It says: “[フレーズ]”",
                        ["“It says: [PHRASE]”"] = "“It says: [フレーズ]”",
                    },
                },
            },
        },

        // Reordered Keys
        [typeof(SReorderedKeys)] = new()
        {
            ModuleName = "順番替え音板",
            Questions = new()
            {
                [SReorderedKeys.KeyColor] = new()
                {
                    // English: What color was this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was this key in the first stage of Reordered Keys? (+ sprite)
                    Question = "{0}のステージ{1}におけるこの音板の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                    },
                },
                [SReorderedKeys.LabelColor] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was the label of this key in the first stage of Reordered Keys? (+ sprite)
                    Question = "{0}のステージ{1}におけるこの音板のラベルの色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                    },
                },
                [SReorderedKeys.Label] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What was the label of this key in the first stage of Reordered Keys? (+ sprite)
                    Question = "{0}のステージ{1}におけるこの音板のラベルの色は？",
                },
                [SReorderedKeys.Pivot] = new()
                {
                    // English: Which key was the pivot in the {1} stage of {0}?
                    Question = "{0}のステージ{1}の軸は？",
                },
            },
        },

        // Retirement
        [typeof(SRetirement)] = new()
        {
            ModuleName = "退職",
            Questions = new()
            {
                [SRetirement.Houses] = new()
                {
                    // English: Which one of these houses was on offer, but not chosen by Bob in {0}?
                    Question = "{0}において、これらのうちBOBが定年後に選択しなかった家は？",
                },
            },
        },

        // Reverse Morse
        [typeof(SReverseMorse)] = new()
        {
            ModuleName = "逆モールス信号",
            Questions = new()
            {
                [SReverseMorse.Characters] = new()
                {
                    // English: What was the {1} character in the {2} message of {0}?
                    // Example: What was the first character in the first message of Reverse Morse?
                    Question = "{0}の{2}つ目のメッセージの{1}文字目は？",
                },
            },
        },

        // Reverse Polish Notation
        [typeof(SReversePolishNotation)] = new()
        {
            ModuleName = "逆ポーランド記法",
            Questions = new()
            {
                [SReversePolishNotation.Character] = new()
                {
                    // English: What character was used in the {1} round of {0}?
                    // Example: What character was used in the first round of Reverse Polish Notation?
                    Question = "{0}のラウンド{1}で使用された文字は？",
                },
            },
        },

        // RGB Encryption
        [typeof(SRGBEncryption)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRGBEncryption.MorseSequence] = new()
                {
                    // English: What was the {1} Morse code sequence in {0}?
                    // Example: What was the first Morse code sequence in RGB Encryption?
                    Question = "{0}におけるモールス信号のシークエンスの{1}番目は？",
                },
                [SRGBEncryption.ColorSequence] = new()
                {
                    // English: What was the {1} color sequence in {0}?
                    // Example: What was the first color sequence in RGB Encryption?
                    Question = "{0}における色のシークエンスの{1}番目は？",
                },
            },
        },

        // RGB Maze
        [typeof(SRGBMaze)] = new()
        {
            ModuleName = "RGB迷路",
            Questions = new()
            {
                [SRGBMaze.Keys] = new()
                {
                    // English: Where was the {1} key in {0}?
                    // Example: Where was the red key in RGB Maze?
                    Question = "{0}における{1}色の鍵はどこ？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
                [SRGBMaze.Number] = new()
                {
                    // English: Which maze number was the {1} maze in {0}?
                    // Example: Which maze number was the red maze in RGB Maze?
                    Question = "{0}の{1}色迷路の番号は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
                [SRGBMaze.Exit] = new()
                {
                    // English: What was the exit coordinate in {0}?
                    Question = "{0}の出口の座標は？",
                },
            },
        },

        // RGB Sequences
        [typeof(SRGBSequences)] = new()
        {
            ModuleName = "RGBシークエンス",
            Questions = new()
            {
                [SRGBSequences.Display] = new()
                {
                    // English: What was the color of the {1} LED in {0}?
                    // Example: What was the color of the first LED in RGB Sequences?
                    Question = "{0}の{1}番目のLEDの色は？",
                    Answers = new()
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
            },
        },

        // Rhythms
        [typeof(SRhythms)] = new()
        {
            ModuleName = "リズム",
            Questions = new()
            {
                [SRhythms.Color] = new()
                {
                    // English: What was the color in {0}?
                    Question = "{0}のLEDの色は？",
                    Answers = new()
                    {
                        ["Blue"] = "青",
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                    },
                },
            },
        },

        // RNG Crystal
        [typeof(SRNGCrystal)] = new()
        {
            ModuleName = "乱数クリスタル",
            Questions = new()
            {
                [SRNGCrystal.Taps] = new()
                {
                    // English: Which bit had a tap in {0} (the output after shifting is at bit 0)?
                    Question = "{0}でタップがあったビットは(点線の位置はビット0)？",
                },
            },
        },

        // Robo-Scanner
        [typeof(SRoboScanner)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRoboScanner.EmptyCell] = new()
                {
                    // English: Where was the empty cell in {0}?
                    Question = "{0}の空のセルはどこ？",
                },
            },
        },

        // Robot Programming
        [typeof(SRobotProgramming)] = new()
        {
            ModuleName = "ロボットプログラミング",
            Questions = new()
            {
                [SRobotProgramming.Color] = new()
                {
                    // English: What was the color of the {1} robot in {0}?
                    // Example: What was the color of the first robot in Robot Programming?
                    Question = "{0}の{1}番目のロボットの色は？",
                },
                [SRobotProgramming.Shape] = new()
                {
                    // English: What was the shape of the {1} robot in {0}?
                    // Example: What was the shape of the first robot in Robot Programming?
                    Question = "{0}の{1}番目のロボットの形は？",
                },
            },
        },

        // Roger
        [typeof(SRoger)] = new()
        {
            ModuleName = "ロジャー",
            Questions = new()
            {
                [SRoger.Seed] = new()
                {
                    // English: What four-digit number was given in {0}?
                    Question = "{0}で得られた4桁の数字は？",
                },
            },
        },

        // Role Reversal
        [typeof(SRoleReversal)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRoleReversal.Wires] = new()
                {
                    // English: How many {1} wires were there in {0}?
                    // Example: How many warm-colored wires were there in Role Reversal?
                    Question = "{0}における{1}系のワイヤの総数は？",
                    Arguments = new()
                    {
                        ["warm-colored"] = "暖色",
                        ["cold-colored"] = "寒色",
                        ["primary-colored"] = "原色",
                        ["secondary-colored"] = "二次色",
                    },
                },
                [SRoleReversal.Number] = new()
                {
                    // English: What was the number corresponding to the correct condition in {0}?
                    Question = "{0}の正しい状態の数字は？",
                },
            },
        },

        // RPS Judging
        [typeof(SRPSJudging)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRPSJudging.QWinner] = new()
                {
                    // English: Which round did the {1} team {2} in {0}?
                    // Example: Which round did the red team win in RPS Judging?
                    Question = "{0}で{1}チームが{2}ラウンドは？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["win"] = "勝った",
                        ["lose"] = "負けた",
                    },
                },
                [SRPSJudging.QDraw] = new()
                {
                    // English: Which round was a draw in {0}?
                    Question = "{0}で引き分けだったラウンドは？",
                },
            },
            Discriminators = new()
            {
                [SRPSJudging.DWinner] = new()
                {
                    // English: the RPS Judging where the {0} team {1} the {2} round
                    // Example: the RPS Judging where the red team won the first round
                    Discriminator = "{0}チームがラウンド{1}で{2}RPS Judging",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["won"] = "勝った",
                        ["lost"] = "負けた",
                    },
                },
                [SRPSJudging.DDraw] = new()
                {
                    // English: the RPS Judging with a draw in the {0} round
                    // Example: the RPS Judging with a draw in the first round
                    Discriminator = "ラウンド{0}で引き分けだったRPS Judging",
                },
            },
        },

        // The Rule
        [typeof(SRule)] = new()
        {
            ModuleName = "ザ・ルール",
            Questions = new()
            {
                [SRule.Number] = new()
                {
                    // English: What was the rule number in {0}?
                    Question = "{0}のルール番号は？",
                },
            },
        },

        // Rule of Three
        [typeof(SRuleOfThree)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SRuleOfThree.QCoordinates] = new()
                {
                    // English: What was the {1} coordinate of the {2} vertex in {0}?
                    // Example: What was the X coordinate of the red vertex in Rule of Three?
                    Question = "{0}の{2}色の頂点の{1}座標は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["yellow"] = "黄",
                        ["blue"] = "青",
                    },
                },
                [SRuleOfThree.QCycles] = new()
                {
                    // English: What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?
                    // Example: What was the position of the red sphere on the X axis in the first cycle in Rule of Three?
                    Question = "{0}の{3}回目のサイクルにおける{1}色の球の{2}軸上の位置は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["yellow"] = "黄",
                        ["blue"] = "青",
                    },
                },
            },
            Discriminators = new()
            {
                [SRuleOfThree.DCoordinates] = new()
                {
                    // English: the Rule of Three where the {1} coordinate of the {2} vertex was {0}
                    // Example: the Rule of Three where the X coordinate of the red vertex was 0
                    Discriminator = "{2}色の頂点の{1}座標が{0}だったRule of Three",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["yellow"] = "黄",
                        ["blue"] = "青",
                    },
                },
                [SRuleOfThree.DCycles] = new()
                {
                    // English: the Rule of Three where the {1} sphere was {0} on the {2} axis in the {3} cycle
                    // Example: the Rule of Three where the red sphere was positive on the X axis in the first cycle
                    Discriminator = "{3}回目のサイクルで{1}色の球の{2}軸が{0}だったRule of Three",
                    Arguments = new()
                    {
                        ["positive"] = "正",
                        ["negative"] = "負",
                        ["zero"] = "ゼロ",
                        ["red"] = "赤",
                        ["yellow"] = "黄",
                        ["blue"] = "青",
                    },
                },
            },
        },

        // Safety Square
        [typeof(SSafetySquare)] = new()
        {
            ModuleName = "セイフティスクエア",
            Questions = new()
            {
                [SSafetySquare.Digits] = new()
                {
                    // English: What was the digit displayed on the {1} diamond in {0}?
                    // Example: What was the digit displayed on the red diamond in Safety Square?
                    Question = "{0}の{1}色のダイヤに表示された数字は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["yellow"] = "黄",
                        ["blue"] = "青",
                    },
                },
                [SSafetySquare.SpecialRule] = new()
                {
                    // English: What was the special rule displayed on the white diamond in {0}?
                    Question = "{0}の白に表示された特殊ルールは？",
                    Answers = new()
                    {
                        ["No special rule"] = "特殊ルールなし",
                        ["Reacts with water"] = "水と過剰に反応",
                        ["Simple asphyxiant"] = "単純窒息性ガス",
                        ["Oxidizer"] = "空気供給なしで燃焼",
                    },
                },
            },
        },

        // The Samsung
        [typeof(SSamsung)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSamsung.AppPositions] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was Duolingo in The Samsung?
                    Question = "{0}の{1}はどこ？",
                },
            },
        },

        // Saturn
        [typeof(SSaturn)] = new()
        {
            ModuleName = "土星",
            Questions = new()
            {
                [SSaturn.Goal] = new()
                {
                    // English: Where was the goal in {0}?
                    Question = "{0}のゴールはどこ？",
                },
            },
        },

        // Sbemail Songs
        [typeof(SSbemailSongs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSbemailSongs.Songs] = new()
                {
                    // English: What was the displayed song for stage {1} (hexadecimal) of {0}?
                    // Example: What was the displayed song for stage 01 (hexadecimal) of Sbemail Songs?
                    Question = "{0}のステージ{1}(十六進数)で再生された音は？",
                },
            },
            Discriminators = new()
            {
                [SSbemailSongs.Digits] = new()
                {
                    // English: the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)
                    // Example: the Sbemail Songs which displayed ‘Oh, who is the guy that…’ in stage 01 (hexadecimal)
                    Discriminator = "ステージ{1}(十六進数)で「{0}」が表示されたSbemail Song",
                },
            },
        },

        // Scavenger Hunt
        [typeof(SScavengerHunt)] = new()
        {
            ModuleName = "宝探し",
            Questions = new()
            {
                [SScavengerHunt.KeySquare] = new()
                {
                    // English: Which tile was correctly submitted in the first stage of {0}?
                    Question = "{0}のステージ{1}で正しく送信されたタイルは？",
                },
                [SScavengerHunt.ColoredTiles] = new()
                {
                    // English: Which of these tiles was {1} in the first stage of {0}?
                    // Example: Which of these tiles was red in the first stage of Scavenger Hunt?
                    Question = "{0}の最初のステージで{1}色だったタイルは？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
            },
        },

        // Schlag den Bomb
        [typeof(SSchlagDenBomb)] = new()
        {
            ModuleName = "シュラグ・デン・ボム",
            Questions = new()
            {
                [SSchlagDenBomb.ContestantName] = new()
                {
                    // English: What was the contestant’s name in {0}?
                    Question = "{0}の出場者の名前は？",
                },
                [SSchlagDenBomb.ContestantScore] = new()
                {
                    // English: What was the contestant’s score in {0}?
                    Question = "{0}の出場者のスコアは？",
                },
                [SSchlagDenBomb.BombScore] = new()
                {
                    // English: What was the bomb’s score in {0}?
                    Question = "{0}の爆弾のスコアは？",
                },
            },
        },

        // Scramboozled Eggain
        [typeof(SScramboozledEggain)] = new()
        {
            ModuleName = "再卵炒",
            Questions = new()
            {
                [SScramboozledEggain.Word] = new()
                {
                    // English: What was the {1} encrypted word in {0}?
                    // Example: What was the first encrypted word in Scramboozled Eggain?
                    Question = "{0}で解読した{1}番目の単語は？",
                },
            },
        },

        // Scripting
        [typeof(SScripting)] = new()
        {
            ModuleName = "スクリプト修正",
            Questions = new()
            {
                [SScripting.VariableDataType] = new()
                {
                    // English: What was the submitted data type of the variable in {0}?
                    Question = "{0}で送信した変数の型は？",
                },
            },
        },

        // Scrutiny Squares
        [typeof(SScrutinySquares)] = new()
        {
            ModuleName = "正方形精査",
            Questions = new()
            {
                [SScrutinySquares.FirstDifference] = new()
                {
                    // English: What was the modified property of the first display in {0}?
                    Question = "{0}の最初の表示内容で修正されていたのはどの要素？",
                    Answers = new()
                    {
                        ["Word"] = "単語",
                        ["Color around word"] = "単語の周りの四角の色",
                        ["Color of background"] = "背景の色",
                        ["Color of word"] = "単語の色",
                    },
                },
            },
        },

        // Sea Shells
        [typeof(SSeaShells)] = new()
        {
            ModuleName = "シーシェル",
            Questions = new()
            {
                [SSeaShells.Question1] = new()
                {
                    // English: What were the first and second words in the {1} phrase in {0}?
                    // Example: What were the first and second words in the first phrase in Sea Shells?
                    Question = "{0}の{1}フレーズの1番目と2番目の単語は？",
                },
                [SSeaShells.Question2] = new()
                {
                    // English: What were the third and fourth words in the {1} phrase in {0}?
                    // Example: What were the third and fourth words in the first phrase in Sea Shells?
                    Question = "{0}の{1}フレーズの3番目と4番目の単語は？",
                },
                [SSeaShells.Question3] = new()
                {
                    // English: What was the end of the {1} phrase in {0}?
                    // Example: What was the end of the first phrase in Sea Shells?
                    Question = "{0}の{1}フレーズの最後の単語は？",
                },
            },
        },

        // Semamorse
        [typeof(SSemamorse)] = new()
        {
            ModuleName = "セマモールス",
            Questions = new()
            {
                [SSemamorse.Color] = new()
                {
                    // English: What was the color of the display involved in the starting value in {0}?
                    Question = "{0}の初期値を求める際に使用した表示の色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["cyan"] = "シアン",
                        ["indigo"] = "藍色",
                        ["pink"] = "ピンク",
                    },
                },
                [SSemamorse.Letters] = new()
                {
                    // English: What was the {1} letter involved in the starting value in {0}?
                    // Example: What was the Morse letter involved in the starting value in Semamorse?
                    Question = "{0}の初期値を求める際に使用した表示のうち{1}の英字は？",
                    Arguments = new()
                    {
                        ["Morse"] = "モールス",
                        ["semaphore"] = "セマフォア",
                    },
                },
            },
        },

        // The Sequencyclopedia
        [typeof(SSequencyclopedia)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSequencyclopedia.Sequence] = new()
                {
                    // English: What sequence was used in {0}?
                    Question = "{0}では何のシークエンスが使用された？",
                },
            },
        },

        // S.E.T. Theory
        [typeof(SSetTheory)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSetTheory.Equations] = new()
                {
                    // English: What equation was shown in the {1} stage of {0}?
                    // Example: What equation was shown in the first stage of S.E.T. Theory?
                    Question = "{0}のステージ{1}で表示されたのは？",
                },
            },
        },

        // Shapes And Bombs
        [typeof(SShapesAndBombs)] = new()
        {
            ModuleName = "形と爆弾",
            Questions = new()
            {
                [SShapesAndBombs.InitialLetter] = new()
                {
                    // English: What was the initial letter in {0}?
                    Question = "{0}の初期の英字は？",
                },
            },
        },

        // Shape Shift
        [typeof(SShapeShift)] = new()
        {
            ModuleName = "形状変化",
            Questions = new()
            {
                [SShapeShift.InitialShape] = new()
                {
                    // English: What was the initial shape in {0}?
                    Question = "{0}の最初の図形は？",
                },
            },
        },

        // Shifted Maze
        [typeof(SShiftedMaze)] = new()
        {
            ModuleName = "シフト迷路",
            Questions = new()
            {
                [SShiftedMaze.Colors] = new()
                {
                    // English: What color was the {1} marker in {0}?
                    // Example: What color was the top-left marker in Shifted Maze?
                    Question = "{0}の{1}にあるマークの色は？",
                    Arguments = new()
                    {
                        ["top-left"] = "左上",
                        ["top-right"] = "右上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                    Answers = new()
                    {
                        ["White"] = "白",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                        ["Magenta"] = "マゼンタ",
                        ["Green"] = "緑",
                    },
                },
            },
        },

        // Shifting Maze
        [typeof(SShiftingMaze)] = new()
        {
            ModuleName = "シフト中迷路",
            Questions = new()
            {
                [SShiftingMaze.Seed] = new()
                {
                    // English: What was the seed in {0}?
                    Question = "{0}のシード値は？",
                },
            },
        },

        // Shogi Identification
        [typeof(SShogiIdentification)] = new()
        {
            ModuleName = "将棋識別",
            Questions = new()
            {
                [SShogiIdentification.Piece] = new()
                {
                    // English: What was the displayed piece in {0}?
                    Question = "{0}に表示された駒は？",
                    Answers = new()
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
            },
        },

        // Sign Language
        [typeof(SSignLanguage)] = new()
        {
            ModuleName = "手話",
            Questions = new()
            {
                [SSignLanguage.Word] = new()
                {
                    // English: What was the deciphered word in {0}?
                    Question = "{0}で解読した単語は？",
                },
            },
        },

        // Silly Slots
        [typeof(SSillySlots)] = new()
        {
            ModuleName = "ヘンテコスロット",
            Questions = new()
            {
                [SSillySlots.Question] = new()
                {
                    // English: What was the {1} slot in the {2} stage in {0}?
                    // Example: What was the first slot in the first stage in Silly Slots?
                    Question = "{0}のステージ{2}において、{1}列目のスロットは？",
                    Answers = new()
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
            },
        },

        // Silo Authorization
        [typeof(SSiloAuthorization)] = new()
        {
            ModuleName = "シロ認証",
            Questions = new()
            {
                [SSiloAuthorization.MessageType] = new()
                {
                    // English: What was the message type in {0}?
                    Question = "{0}のメッセージの種類は？",
                },
                [SSiloAuthorization.EncryptedMessage] = new()
                {
                    // English: What was the {1} part of the encrypted message in {0}?
                    // Example: What was the first part of the encrypted message in Silo Authorization?
                    Question = "{0}の暗号メッセージでパート{1}は？",
                },
                [SSiloAuthorization.AuthCode] = new()
                {
                    // English: What was the received authentication code in {0}?
                    Question = "{0}で受信した認証コードは？",
                },
            },
        },

        // Simon Said
        [typeof(SSimonSaid)] = new()
        {
            ModuleName = "サイモンの残響",
            Questions = new()
            {
                [SSimonSaid.Flashes] = new()
                {
                    // English: What color flashed {1} in the final sequence of {0}?
                    // Example: What color flashed first in the final sequence of Simon Said?
                    Question = "{0}の最終シークエンスにおいて{1}番目に点滅した色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                    },
                },
            },
        },

        // Simon Samples
        [typeof(SSimonSamples)] = new()
        {
            ModuleName = "サイモンの音源",
            Questions = new()
            {
                [SSimonSamples.Samples] = new()
                {
                    // English: What were the call samples {1} of {0}?
                    // Example: What were the call samples played in the first stage of Simon Samples?
                    Question = "{0}の{1}音は？",
                    Arguments = new()
                    {
                        ["played in the first stage"] = "ステージ1で演奏された",
                        ["added in the second stage"] = "ステージ2で演奏された",
                        ["added in the third stage"] = "ステージ3で演奏された",
                    },
                },
            },
        },

        // Simon Says
        [typeof(SSimonSays)] = new()
        {
            ModuleName = "サイモンゲーム",
            Questions = new()
            {
                [SSimonSays.Flash] = new()
                {
                    // English: What color flashed {1} in the final sequence in {0}?
                    // Example: What color flashed first in the final sequence in Simon Says?
                    Question = "{0}の最終シークエンスにおいて、{1}番目に点滅した色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["yellow"] = "黄",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
            },
        },

        // Simon Scrambles
        [typeof(SSimonScrambles)] = new()
        {
            ModuleName = "サイモンの撹拌",
            Questions = new()
            {
                [SSimonScrambles.Colors] = new()
                {
                    // English: What color flashed {1} in {0}?
                    // Example: What color flashed first in Simon Scrambles?
                    Question = "{0}の{1}番目の点滅は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                    },
                },
            },
        },

        // Simon Screams
        [typeof(SSimonScreams)] = new()
        {
            ModuleName = "サイモンの絶叫",
            Questions = new()
            {
                [SSimonScreams.Flashing] = new()
                {
                    // English: Which color flashed {1} in the final sequence in {0}?
                    // Example: Which color flashed first in the final sequence in Simon Screams?
                    Question = "{0}の最終シークエンスにおいて、{1}番目に点滅した色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Orange"] = "オレンジ",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Purple"] = "紫",
                    },
                },
                [SSimonScreams.RuleSimple] = new()
                {
                    // English: In which stage(s) of {0} was “{1}” the applicable rule?
                    // Example: In which stage(s) of Simon Screams was “a color flashed, then a color two away, then the first again” the applicable rule?
                    Question = "{0}で「{1}」のルールが当てはまったのはどのステージ？",
                    Arguments = new()
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
                    Answers = new()
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
                [SSimonScreams.RuleComplex] = new()
                {
                    // English: In which stage(s) of {0} was “{1} flashed out of {2}, {3}, and {4}” the applicable rule?
                    // Example: In which stage(s) of Simon Screams was “at most one color flashed out of Red, Orange, and Yellow” the applicable rule?
                    Question = "{0}「flashed out of {2}、 {3}、{4}のうち{1}」のルールが当てはまったのはどのステージ？",
                    Arguments = new()
                    {
                        ["at most one color"] = "多くとも1色しか点滅していない",
                        ["at least two colors"] = "少なくとも2色が点滅した",
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Orange"] = "橙",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                        ["Purple"] = "紫",
                    },
                    Answers = new()
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
            },
        },

        // Simon Selects
        [typeof(SSimonSelects)] = new()
        {
            ModuleName = "サイモンの選択",
            Questions = new()
            {
                [SSimonSelects.Order] = new()
                {
                    // English: Which color flashed {1} in the {2} stage of {0}?
                    // Example: Which color flashed first in the first stage of Simon Selects?
                    Question = "{0}のステージ{2}において、{1}番目に点滅した色は？",
                    Answers = new()
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
            },
        },

        // Simon Sends
        [typeof(SSimonSends)] = new()
        {
            ModuleName = "サイモンの送信",
            Questions = new()
            {
                [SSimonSends.ReceivedLetters] = new()
                {
                    // English: What was the {1} received letter in {0}?
                    // Example: What was the red received letter in Simon Sends?
                    Question = "{0}で{1}色が受け取った英字は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
            },
        },

        // Simon Serves
        [typeof(SSimonServes)] = new()
        {
            ModuleName = "サイモンの給仕",
            Questions = new()
            {
                [SSimonServes.Flash] = new()
                {
                    // English: Who flashed {1} in course {2} of {0}?
                    // Example: Who flashed first in course 1 of Simon Serves?
                    Question = "{0}の{2}品目で{1}番目に点滅したのは？",
                },
                [SSimonServes.Food] = new()
                {
                    // English: Which item was not served in course {1} of {0}?
                    // Example: Which item was not served in course 1 of Simon Serves?
                    Question = "{0}の{1}品目で提供されなかった商品は？",
                    Answers = new()
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
            },
        },

        // Simon Shapes
        [typeof(SSimonShapes)] = new()
        {
            ModuleName = "サイモンの形状",
            Questions = new()
            {
                [SSimonShapes.SubmittedShape] = new()
                {
                    // English: What was the shape submitted at the end of {0}?
                    Question = "{0}で最終的に送信した図形は？",
                },
            },
        },

        // Simon Shouts
        [typeof(SSimonShouts)] = new()
        {
            ModuleName = "サイモンの叫び",
            Questions = new()
            {
                [SSimonShouts.FlashingLetter] = new()
                {
                    // English: Which letter flashed on the {1} button in {0}?
                    // Example: Which letter flashed on the top button in Simon Shouts?
                    Question = "{0}の{1}の位置が点滅した英字は？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["left"] = "左",
                        ["right"] = "右",
                        ["bottom"] = "下",
                    },
                },
            },
        },

        // Simon Shrieks
        [typeof(SSimonShrieks)] = new()
        {
            ModuleName = "サイモンの悲鳴",
            Questions = new()
            {
                [SSimonShrieks.FlashingButton] = new()
                {
                    // English: How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?
                    // Example: How many spaces clockwise from the arrow was the first flash in the final sequence in Simon Shrieks?
                    Question = "{0}の最終シークエンスにおいて、{1}番目の点滅は矢印から時計回りに何番目のスペースにある？",
                },
            },
        },

        // Simon Shuffles
        [typeof(SSimonShuffles)] = new()
        {
            ModuleName = "サイモンの交雑",
            Questions = new()
            {
                [SSimonShuffles.Flashes] = new()
                {
                    // English: What was the {1} flash of the {2} stage of {0}?
                    // Example: What was the first flash of the first stage of Simon Shuffles?
                    Question = "{0}のステージ{2}において{1}番目に点滅した色は？",
                },
            },
        },

        // Simon Signals
        [typeof(SSimonSignals)] = new()
        {
            ModuleName = "サイモンの信号",
            Questions = new()
            {
                [SSimonSignals.ColorToShape] = new()
                {
                    // English: What shape was the {1} arrow in {0}?
                    // Example: What shape was the red arrow in Simon Signals?
                    Question = "{0}の{1}色矢印の形は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["gray"] = "灰",
                    },
                },
                [SSimonSignals.ColorToRotations] = new()
                {
                    // English: How many directions did the {1} arrow in {0} have?
                    // Example: How many directions did the red arrow in Simon Signals have?
                    Question = "{0}の{1}色矢印は何個あった？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["gray"] = "灰",
                    },
                },
                [SSimonSignals.ShapeToColor] = new()
                {
                    // English: What color was the arrow with this shape in {0}? (+ sprite)
                    Question = "{0}で、この形の矢印は何色だった？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["gray"] = "灰",
                    },
                },
                [SSimonSignals.ShapeToRotations] = new()
                {
                    // English: How many directions did the arrow with this shape have in {0}? (+ sprite)
                    Question = "{0}で、この形の矢印は何回出現した？",
                },
                [SSimonSignals.RotationsToColor] = new()
                {
                    // English: What color was the arrow with {1} possible directions in {0}?
                    // Example: What color was the arrow with 3 possible directions in Simon Signals?
                    Question = "{0}で{1}方向を指していた矢印の色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["gray"] = "灰",
                    },
                },
                [SSimonSignals.RotationsToShape] = new()
                {
                    // English: What shape was the arrow with {1} possible directions in {0}?
                    // Example: What shape was the arrow with 3 possible directions in Simon Signals?
                    Question = "{0}で{1}方向を指していた矢印の形は？",
                },
            },
        },

        // Simon Simons
        [typeof(SSimonSimons)] = new()
        {
            ModuleName = "サイモンのサイモン",
            Questions = new()
            {
                [SSimonSimons.FlashingColors] = new()
                {
                    // English: What was the {1} flash in the final sequence in {0}?
                    // Example: What was the first flash in the final sequence in Simon Simons?
                    Question = "{0}の最終シークエンスにおいて、{1}番目に点滅した色は？",
                },
            },
        },

        // Simon Sings
        [typeof(SSimonSings)] = new()
        {
            ModuleName = "サイモンの歌唱",
            Questions = new()
            {
                [SSimonSings.Flashing] = new()
                {
                    // English: Which key’s color flashed {1} in the {2} stage of {0}?
                    // Example: Which key’s color flashed first in the first stage of Simon Sings?
                    Question = "{0}のステージ{2}において、{1}番目に点滅したキーは？",
                },
            },
        },

        // Simon Smiles
        [typeof(SSimonSmiles)] = new()
        {
            ModuleName = "サイモンの笑顔",
            Questions = new()
            {
                [SSimonSmiles.Sounds] = new()
                {
                    // English: What sound did the {1} button press make in {0}?
                    // Example: What sound did the first button press make in Simon Smiles?
                    Question = "{0}で{1}番目に押したボタンが鳴らした音は？",
                },
            },
        },

        // Simon Smothers
        [typeof(SSimonSmothers)] = new()
        {
            ModuleName = "サイモンの隠匿",
            Questions = new()
            {
                [SSimonSmothers.Colors] = new()
                {
                    // English: What was the color of the {1} flash in {0}?
                    // Example: What was the color of the first flash in Simon Smothers?
                    Question = "{0}の{1}番目に点滅した色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                        ["Blue"] = "青",
                        ["Magenta"] = "マゼンタ",
                        ["Cyan"] = "シアン",
                    },
                },
                [SSimonSmothers.Directions] = new()
                {
                    // English: What was the direction of the {1} flash in {0}?
                    // Example: What was the direction of the first flash in Simon Smothers?
                    Question = "{0}の{1}番目に点滅した方向は？",
                    Answers = new()
                    {
                        ["Up"] = "上",
                        ["Down"] = "下",
                        ["Left"] = "左",
                        ["Right"] = "右",
                    },
                },
            },
        },

        // Simon Sounds
        [typeof(SSimonSounds)] = new()
        {
            ModuleName = "サイモンの響き",
            Questions = new()
            {
                [SSimonSounds.FlashingColors] = new()
                {
                    // English: Which sample button sounded {1} in the final sequence in {0}?
                    // Example: Which sample button sounded first in the final sequence in Simon Sounds?
                    Question = "{0}の最終シークエンスにおいて、{1}番目に再生されたサンプルボタンの色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["yellow"] = "黄",
                        ["green"] = "緑",
                    },
                },
            },
        },

        // Simon Speaks
        [typeof(SSimonSpeaks)] = new()
        {
            ModuleName = "サイモンの発話",
            Questions = new()
            {
                [SSimonSpeaks.Positions] = new()
                {
                    // English: Which bubble flashed first in {0}?
                    Question = "{0}の1番目の点滅の吹き出しは？",
                    Answers = new()
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
                [SSimonSpeaks.Shapes] = new()
                {
                    // English: Which bubble flashed second in {0}?
                    Question = "{0}の2番目の点滅の吹き出しは？",
                },
                [SSimonSpeaks.Languages] = new()
                {
                    // English: Which language was the bubble that flashed third in {0} in?
                    Question = "{0}の3回目の点滅の言語は？",
                },
                [SSimonSpeaks.Words] = new()
                {
                    // English: Which word was in the bubble that flashed fourth in {0}?
                    Question = "{0}の4番目の点滅の単語は？",
                },
                [SSimonSpeaks.Colors] = new()
                {
                    // English: What color was the bubble that flashed fifth in {0}?
                    Question = "{0}の5番目の点滅の吹き出しの色は？",
                    Answers = new()
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
            },
        },

        // Simon’s Star
        [typeof(SSimonsStar)] = new()
        {
            ModuleName = "サイモンの星",
            Questions = new()
            {
                [SSimonsStar.Colors] = new()
                {
                    // English: Which color flashed {1} in {0}?
                    // Example: Which color flashed first in Simon’s Star?
                    Question = "{0}のシークエンスにおいて{1}番目に点滅した色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["yellow"] = "黄",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["purple"] = "紫",
                    },
                },
            },
        },

        // Simon Stacks
        [typeof(SSimonStacks)] = new()
        {
            ModuleName = "六角形サイモン",
            Questions = new()
            {
                [SSimonStacks.Colors] = new()
                {
                    // English: Which color flashed in the {1} stage of {0}?
                    // Example: Which color flashed in the first stage of Simon Stacks?
                    Question = "{0}のステージ{1}で点滅した色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                    },
                },
            },
        },

        // Simon Stages
        [typeof(SSimonStages)] = new()
        {
            ModuleName = "サイモンステージ",
            Questions = new()
            {
                [SSimonStages.Indicator] = new()
                {
                    // English: What color was the indicator in the {1} stage in {0}?
                    // Example: What color was the indicator in the first stage in Simon Stages?
                    Question = "{0}のステージ{1}におけるインジケーターの色は？",
                    Answers = new()
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
                [SSimonStages.Flashes] = new()
                {
                    // English: Which color flashed {1} in the {2} stage in {0}?
                    // Example: Which color flashed first in the first stage in Simon Stages?
                    Question = "{0}のステージ{2}における{1}番目の点滅した色は？",
                    Answers = new()
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
            },
        },

        // Simon States
        [typeof(SSimonStates)] = new()
        {
            ModuleName = "サイモンの陳述",
            Questions = new()
            {
                [SSimonStates.Display] = new()
                {
                    // English: Which {1} in the {2} stage in {0}?
                    // Example: Which color(s) flashed in the first stage in Simon States?
                    Question = "{0}のステージ{2}ではどの{1}？",
                    Arguments = new()
                    {
                        ["color(s) flashed"] = "色が点滅した",
                        ["color(s) didn’t flash"] = "色が点滅しなかった",
                    },
                    Answers = new()
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
            },
        },

        // Simon Stops
        [typeof(SSimonStops)] = new()
        {
            ModuleName = "サイモンの停止",
            Questions = new()
            {
                [SSimonStops.Colors] = new()
                {
                    // English: Which color flashed {1} in the output sequence in {0}?
                    // Example: Which color flashed first in the output sequence in Simon Stops?
                    Question = "{0}の出力シークエンスにおいて、{1}番目に点滅した色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Orange"] = "オレンジ",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Violet"] = "紫",
                    },
                },
            },
        },

        // Simon Stores
        [typeof(SSimonStores)] = new()
        {
            ModuleName = "サイモンの貯留",
            Questions = new()
            {
                [SSimonStores.Colors] = new()
                {
                    // English: Which color {1} {2} in the final sequence of {0}?
                    // Example: Which color flashed first in the final sequence of Simon Stores?
                    Question = "{0}の最終シークエンスにおいて、{2}番目に{1}色は？",
                    Arguments = new()
                    {
                        ["flashed"] = "点滅した",
                        ["was among the colors flashed"] = "点滅した色に含まれる",
                    },
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Cyan"] = "シアン",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                    },
                },
            },
        },

        // Simon Subdivides
        [typeof(SSimonSubdivides)] = new()
        {
            ModuleName = "サイモンの分割",
            Questions = new()
            {
                [SSimonSubdivides.Button] = new()
                {
                    // English: What color was the button at this position in {0}? (+ sprite)
                    Question = "{0}のこの位置にあったボタンの色は？",
                    Answers = new()
                    {
                        ["Blue"] = "",
                        ["Green"] = "",
                        ["Red"] = "",
                        ["Violet"] = "",
                    },
                },
            },
        },

        // Simon Supports
        [typeof(SSimonSupports)] = new()
        {
            ModuleName = "サイモンの支持",
            Questions = new()
            {
                [SSimonSupports.Topics] = new()
                {
                    // English: What was the {1} topic in {0}?
                    // Example: What was the first topic in Simon Supports?
                    Question = "{0}の{1}番目のトピックは？",
                    Answers = new()
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
            },
        },

        // Simon Swizzles
        [typeof(SSimonSwizzles)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSwizzles.Button] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was OFF in Simon Swizzles?
                    Question = "{0}の{1}はどこにあった？",
                    Arguments = new()
                    {
                        ["OFF"] = "オフ",
                        ["ON"] = "オン",
                    },
                },
                [SSimonSwizzles.Number] = new()
                {
                    // English: What was the hidden number in {0}?
                    Question = "{0}で隠された数字は？",
                },
            },
        },

        // Simply Simon
        [typeof(SSimplySimon)] = new()
        {
            ModuleName = "シンプルサイモン",
            Questions = new()
            {
                [SSimplySimon.Flash] = new()
                {
                    // English: What were the flashes in the {1} stage of {0}?
                    // Example: What were the flashes in the first stage of Simply Simon?
                    Question = "{0}のステージ{1}における点滅は？",
                },
            },
        },

        // Simultaneous Simons
        [typeof(SSimultaneousSimons)] = new()
        {
            ModuleName = "同時サイモン",
            Questions = new()
            {
                [SSimultaneousSimons.Flash] = new()
                {
                    // English: What color flashed {1} on the {2} Simon in {0}?
                    // Example: What color flashed first on the first Simon in Simultaneous Simons?
                    Question = "{0}の{2}番目のサイモンにおける{1}番目の点滅は？",
                    Answers = new()
                    {
                        ["Blue"] = "青",
                        ["Yellow"] = "黄",
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                    },
                },
            },
        },

        // Skewed Slots
        [typeof(SSkewedSlots)] = new()
        {
            ModuleName = "歪曲スロット",
            Questions = new()
            {
                [SSkewedSlots.OriginalNumbers] = new()
                {
                    // English: What were the original numbers in {0}?
                    Question = "{0}の初期値は？",
                },
            },
        },

        // Skewers
        [typeof(SSkewers)] = new()
        {
            ModuleName = "剣刺し",
            Questions = new()
            {
                [SSkewers.Color] = new()
                {
                    // English: What color was this gem in {0}? (+ sprite)
                    Question = "{0}のこの宝石の色は？",
                    Answers = new()
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
            },
        },

        // Skyrim
        [typeof(SSkyrim)] = new()
        {
            ModuleName = "スカイリム",
            Questions = new()
            {
                [SSkyrim.Race] = new()
                {
                    // English: Which race was selectable, but not the solution, in {0}?
                    Question = "{0}において、選択可能だが回答ではなかった種族は？",
                    Answers = new()
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
                [SSkyrim.Weapon] = new()
                {
                    // English: Which weapon was selectable, but not the solution, in {0}?
                    Question = "{0}において、選択可能だが回答ではなかった武器は？",
                    Answers = new()
                    {
                        ["Axe of Whiterun"] = "ホワイトランの斧",
                        ["Dawnbreaker"] = "ドーンブレイカー",
                        ["Windshear"] = "ウィンドシア",
                        ["Blade of Woe"] = "悲痛の短剣",
                        ["Firiniel’s End"] = "フィリニエルズエンド",
                        ["Bow of the Hunt"] = "狩りの弓",
                        ["Volendrung"] = "ヴォレンドラング",
                        ["Chillrend"] = "チルレンド",
                        ["Mace of Molag Bal"] = "モラグ・バルのメイス",
                    },
                },
                [SSkyrim.Enemy] = new()
                {
                    // English: Which enemy was selectable, but not the solution, in {0}?
                    Question = "{0}において、選択可能だが解除策ではなかった敵は？",
                    Answers = new()
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
                [SSkyrim.City] = new()
                {
                    // English: Which city was selectable, but not the solution, in {0}?
                    Question = "{0}において、選択可能だが回答ではなかった故郷は？",
                    Answers = new()
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
                [SSkyrim.DragonShout] = new()
                {
                    // English: Which dragon shout was selectable, but not the solution, in {0}?
                    Question = "{0}において、選択可能だが解除策ではなかったドラゴンシャウトは？",
                },
            },
        },

        // Slow Math
        [typeof(SSlowMath)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSlowMath.LastLetters] = new()
                {
                    // English: What was the last triplet of letters in {0}?
                    Question = "{0}の最後の英字三組は？",
                },
            },
        },

        // Small Circle
        [typeof(SSmallCircle)] = new()
        {
            ModuleName = "スモールサークル",
            Questions = new()
            {
                [SSmallCircle.Shift] = new()
                {
                    // English: How much did the sequence shift by in {0}?
                    Question = "{0}におけるシークエンスのシフト量は？",
                },
                [SSmallCircle.Wedge] = new()
                {
                    // English: Which wedge made the different noise in the beginning of {0}?
                    Question = "{0}の初期時点で音が違っていたのは？",
                    Answers = new()
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
                [SSmallCircle.Solution] = new()
                {
                    // English: Which color was {1} in the solution to {0}?
                    // Example: Which color was first in the solution to Small Circle?
                    Question = "{0}の解除シークエンスの{1}番目の色は？",
                    Answers = new()
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
            },
        },

        // Small Talk
        [typeof(SSmallTalk)] = new()
        {
            ModuleName = "スモールトーク",
            Questions = new()
            {
                [SSmallTalk.Displays] = new()
                {
                    // English: What was on the display in the {1} stage of {0}?
                    // Example: What was on the display in the first stage of Small Talk?
                    Question = "{0}のステージ{1}に表示されていたのは？",
                },
            },
        },

        // Smash, Marry, Kill
        [typeof(SSmashMarryKill)] = new()
        {
            ModuleName = "SMASH・MARRY・KILL",
            Questions = new()
            {
                [SSmashMarryKill.Category] = new()
                {
                    // English: In what category was {1} for {0}?
                    // Example: In what category was The Button for Smash, Marry, Kill?
                    Question = "{0}で{1}が属していたカテゴリーは？",
                },
                [SSmashMarryKill.Module] = new()
                {
                    // English: Which module was in the {1} category for {0}?
                    // Example: Which module was in the SMASH category for Smash, Marry, Kill?
                    Question = "{0}で{1}のカテゴリーに属していたモジュールは？",
                },
            },
            Discriminators = new()
            {
                [SSmashMarryKill.NullDiscriminator] = new()
                {
                    // English: Smash, Marry, Kill
                    Discriminator = "SMASH・MARRY・KILL",
                },
            },
        },

        // Snooker
        [typeof(SSnooker)] = new()
        {
            ModuleName = "スヌーカー",
            Questions = new()
            {
                [SSnooker.Reds] = new()
                {
                    // English: How many red balls were there at the start of {0}?
                    Question = "{0}の開始時点での赤いボールの数は？",
                },
            },
        },

        // Snowflakes
        [typeof(SSnowflakes)] = new()
        {
            ModuleName = "雪の結晶",
            Questions = new()
            {
                [SSnowflakes.DisplayedSnowflakes] = new()
                {
                    // English: Which snowflake was on the {1} button of {0}?
                    // Example: Which snowflake was on the top button of Snowflakes?
                    Question = "{0}の{1}のボタンにあった結晶は？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["right"] = "右",
                        ["bottom"] = "下",
                        ["left"] = "左",
                    },
                },
            },
        },

        // Sonic & Knuckles
        [typeof(SSonicKnuckles)] = new()
        {
            ModuleName = "ソニック＆ナックルズ",
            Questions = new()
            {
                [SSonicKnuckles.Sounds] = new()
                {
                    // English: Which sound was played but not featured in the chosen zone in {0}?
                    Question = "{0}で選ばれたゾーンに含まれなかったが再生された音は？",
                },
                [SSonicKnuckles.Badnik] = new()
                {
                    // English: Which badnik was shown in {0}?
                    Question = "{0}に表示されたバドニクは？",
                },
                [SSonicKnuckles.Monitor] = new()
                {
                    // English: Which monitor was shown in {0}?
                    Question = "{0}に表示されたモニターは？",
                },
            },
        },

        // Sonic the Hedgehog
        [typeof(SSonicTheHedgehog)] = new()
        {
            ModuleName = "ソニック・ザ・ヘッジホッグ",
            Questions = new()
            {
                [SSonicTheHedgehog.Pictures] = new()
                {
                    // English: What was the {1} picture on {0}?
                    // Example: What was the first picture on Sonic the Hedgehog?
                    Question = "{0}における{1}番目の画像は？",
                },
                [SSonicTheHedgehog.Sounds] = new()
                {
                    // English: Which sound was played by the {1} screen on {0}?
                    // Example: Which sound was played by the Running Boots screen on Sonic the Hedgehog?
                    Question = "{0}において、{1}のモニターで流れていたサウンドは？",
                    Arguments = new()
                    {
                        ["Running Boots"] = "ハイスピード",
                        ["Invincibility"] = "無敵",
                        ["Extra Life"] = "1up",
                        ["Rings"] = "リング",
                    },
                },
            },
        },

        // Sorting
        [typeof(SSorting)] = new()
        {
            ModuleName = "並び替え",
            Questions = new()
            {
                [SSorting.LastSwap] = new()
                {
                    // English: What positions were the last swap used to solve {0}?
                    Question = "{0}を解く際の最後の入れ替えはどの位置で行われた？",
                },
            },
        },

        // Souvenir
        [typeof(SSouvenir)] = new()
        {
            ModuleName = "思い出",
            Questions = new()
            {
                [SSouvenir.FirstQuestion] = new()
                {
                    // English: What was the first module asked about in the other Souvenir on this bomb?
                    Question = "他の「思い出」モジュールが最初に質問したのは、何のモジュールについて(英名)？",
                },
            },
            Discriminators = new()
            {
                [SSouvenir.NullDiscriminator] = new()
                {
                    // English: Souvenir
                    Discriminator = "思い出",
                },
            },
        },

        // Space Traders
        [typeof(SSpaceTraders)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSpaceTraders.MaxTax] = new()
                {
                    // English: What was the maximum tax amount per vessel in {0}?
                    Question = "{0}での1隻当たりの最大税額は？",
                },
            },
        },

        // Spelling Bee
        [typeof(SSpellingBee)] = new()
        {
            ModuleName = "スペリング・ビー",
            Questions = new()
            {
                [SSpellingBee.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "{0}で打ち込んだ単語は？",
                },
            },
        },

        // The Sphere
        [typeof(SSphere)] = new()
        {
            ModuleName = "球",
            Questions = new()
            {
                [SSphere.Colors] = new()
                {
                    // English: What was the {1} flashed color in {0}?
                    // Example: What was the first flashed color in The Sphere?
                    Question = "{0}にて{1}番目に点滅した色は？",
                    Answers = new()
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
            },
        },

        // Splitting The Loot
        [typeof(SSplittingTheLoot)] = new()
        {
            ModuleName = "戦利品分割",
            Questions = new()
            {
                [SSplittingTheLoot.ColoredBag] = new()
                {
                    // English: What bag was initially colored in {0}?
                    Question = "{0}にて初期から色付けされていた袋は？",
                },
            },
        },

        // Spongebob Birthday Identification
        [typeof(SSpongebobBirthdayIdentification)] = new()
        {
            ModuleName = "スポンジ・ボブ誕生日カード識別",
            Questions = new()
            {
                [SSpongebobBirthdayIdentification.Children] = new()
                {
                    // English: Who was the {1} child displayed in {0}?
                    // Example: Who was the first child displayed in Spongebob Birthday Identification?
                    Question = "{0}で{1}番目に表示された人物は？",
                },
            },
        },

        // Stability
        [typeof(SStability)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStability.LedColors] = new()
                {
                    // English: What was the color of the {1} lit LED in {0}?
                    // Example: What was the color of the first lit LED in Stability?
                    Question = "{0}の{1}番目に点灯したLEDの色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Blue"] = "青",
                    },
                },
                [SStability.IdNumber] = new()
                {
                    // English: What was the identification number in {0}?
                    Question = "{0}の判別番号は？",
                },
            },
        },

        // Stable Time Signatures
        [typeof(SStableTimeSignatures)] = new()
        {
            ModuleName = "安定拍子記号",
            Questions = new()
            {
                [SStableTimeSignatures.Signatures] = new()
                {
                    // English: What was the {1} time signature in {0}?
                    // Example: What was the first time signature in Stable Time Signatures?
                    Question = "{0}の{1}番目の拍子記号は？",
                },
            },
        },

        // Stacked Sequences
        [typeof(SStackedSequences)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStackedSequences.Question] = new()
                {
                    // English: Which of these is the length of a sequence in {0}?
                    Question = "{0}のシークエンスの長さは？",
                },
            },
        },

        // Stars
        [typeof(SStars)] = new()
        {
            ModuleName = "星",
            Questions = new()
            {
                [SStars.Center] = new()
                {
                    // English: What was the digit in the center of {0}?
                    Question = "{0}の中心の数字は？",
                },
            },
        },

        // Starstruck
        [typeof(SStarstruck)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SStarstruck.Star] = new()
                {
                    // English: Which star was present on {0}?
                    Question = "{0}に表示された星は？",
                },
            },
        },

        // State of Aggregation
        [typeof(SStateOfAggregation)] = new()
        {
            ModuleName = "元素状態",
            Questions = new()
            {
                [SStateOfAggregation.Element] = new()
                {
                    // English: What was the element shown in {0}?
                    Question = "{0}に表示された要素は？",
                },
            },
        },

        // Stellar
        [typeof(SStellar)] = new()
        {
            ModuleName = "星型十二面体",
            Questions = new()
            {
                [SStellar.Letters] = new()
                {
                    // English: What was the {1} letter in {0}?
                    // Example: What was the Morse code letter in Stellar?
                    Question = "{0}における{1}の英字は？",
                    Arguments = new()
                    {
                        ["Morse code"] = "モールス信号",
                        ["tap code"] = "タップ・コード",
                        ["Braille"] = "点字",
                    },
                },
            },
        },

        // Stroop’s Test
        [typeof(SStroopsTest)] = new()
        {
            ModuleName = "ストループ検査",
            Questions = new()
            {
                [SStroopsTest.QWord] = new()
                {
                    // English: What was the {1} submitted word in {0}?
                    // Example: What was the first submitted word in Stroop’s Test?
                    Question = "{0}で{1}番目に送信された単語は？",
                },
                [SStroopsTest.QColor] = new()
                {
                    // English: What was the {1} submitted word’s color in {0}?
                    // Example: What was the first submitted word’s color in Stroop’s Test?
                    Question = "{0}で{1}番目に送信された単語の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Magenta"] = "マゼンタ",
                        ["White"] = "白",
                    },
                },
            },
            Discriminators = new()
            {
                [SStroopsTest.DWord] = new()
                {
                    // English: the Stroop’s Test whose {0} submitted word was “{1}”
                    // Example: the Stroop’s Test whose first submitted word was “red”
                    Discriminator = "{0}番目に送信された単語が「{1}」だったストループ検査",
                },
                [SStroopsTest.DColor] = new()
                {
                    // English: the Stroop’s Test whose {0} submitted word’s color was {1}
                    // Example: the Stroop’s Test whose first submitted word’s color was red
                    Discriminator = "{0}番目に送信された単語の色が{1}だったストループ検査",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["yellow"] = "黄",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["magenta"] = "マゼンタ",
                        ["white"] = "白",
                    },
                },
            },
        },

        // Stupid Slots
        [typeof(SStupidSlots)] = new()
        {
            ModuleName = "馬鹿スロット",
            Questions = new()
            {
                [SStupidSlots.Values] = new()
                {
                    // English: What was the value of the {1} arrow in {0}?
                    // Example: What was the value of the top-left arrow in Stupid Slots?
                    Question = "{0}の{1}にある矢印の値は？",
                    Arguments = new()
                    {
                        ["top-left"] = "左上",
                        ["top-middle"] = "上",
                        ["top-right"] = "右上",
                        ["bottom-left"] = "左下",
                        ["bottom-middle"] = "下",
                        ["bottom-right"] = "右下",
                    },
                },
            },
        },

        // Subbly Jubbly
        [typeof(SSubblyJubbly)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSubblyJubbly.Substitutions] = new()
                {
                    // English: What was a substitution word in {0}?
                    Question = "{0}の代替語は？",
                },
            },
        },

        // Subscribe to Pewdiepie
        [typeof(SSubscribeToPewdiepie)] = new()
        {
            ModuleName = "ピューディパイの登録",
            Questions = new()
            {
                [SSubscribeToPewdiepie.SubCount] = new()
                {
                    // English: How many subscribers does {1} have in {0}?
                    // Example: How many subscribers does PewDiePie have in Subscribe to Pewdiepie?
                    Question = "{0}における{1}の登録者の数は？",
                    Arguments = new()
                    {
                        ["PewDiePie"] = "ピューディパイ",
                        ["T-Series"] = "Tシリーズ",
                    },
                },
            },
        },

        // Subway
        [typeof(SSubway)] = new()
        {
            ModuleName = "SUBWAY",
            Questions = new()
            {
                [SSubway.Bread] = new()
                {
                    // English: Which bread did the customer ask for in {0}?
                    Question = "{0}で客が注文したパンは？",
                },
                [SSubway.Items] = new()
                {
                    // English: Which of these was not asked for in {0}?
                    Question = "{0}で客が頼まなかったものは？",
                },
            },
        },

        // Sugar Skulls
        [typeof(SSugarSkulls)] = new()
        {
            ModuleName = "シュガースカル",
            Questions = new()
            {
                [SSugarSkulls.Skull] = new()
                {
                    // English: What skull was shown on the {1} square in {0}?
                    // Example: What skull was shown on the top square in Sugar Skulls?
                    Question = "{0}にて{1}の位置に表示された骸骨は？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                },
                [SSugarSkulls.Availability] = new()
                {
                    // English: Which skull {1} present in {0}?
                    // Example: Which skull was present in Sugar Skulls?
                    Question = "{0}に表示されて{1}骸骨は？",
                    Arguments = new()
                    {
                        ["was"] = "いた",
                        ["was not"] = "いなかった",
                    },
                },
            },
        },

        // Suits and Colours
        [typeof(SSuitsAndColours)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "スートと色",
            Questions = new()
            {
                [SSuitsAndColours.Colour] = new()
                {
                    // English: What was the colour of this cell in {0}? (+ sprite)
                    // Example: What was the colour of this cell in Suits and Colours? (+ sprite)
                    Question = "{0}のこのマスの色は？",
                    Answers = new()
                    {
                        ["yellow"] = "黄",
                        ["green"] = "緑",
                        ["orange"] = "オレンジ",
                        ["red"] = "赤",
                    },
                },
                [SSuitsAndColours.Suit] = new()
                {
                    // English: What was the suit of this cell in {0}? (+ sprite)
                    // Example: What was the suit of this cell in Suits and Colours? (+ sprite)
                    Question = "{0}のこのマスのスートは？",
                    Answers = new()
                    {
                        ["spades"] = "スペード",
                        ["hearts"] = "ハート",
                        ["clubs"] = "クラブ",
                        ["diamonds"] = "ダイヤ",
                    },
                },
            },
        },

        // Superparsing
        [typeof(SSuperparsing)] = new()
        {
            ModuleName = "超解析",
            Questions = new()
            {
                [SSuperparsing.Displayed] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "{0}で表示された単語は？",
                },
            },
        },

        // SUSadmin
        [typeof(SSUSadmin)] = new()
        {
            ModuleName = "システム侵入者",
            Questions = new()
            {
                [SSUSadmin.Security] = new()
                {
                    // English: Which security protocol was installed in {0}?
                    Question = "{0}にインストールされたセキュリティプロトコルは？",
                },
                [SSUSadmin.Version] = new()
                {
                    // English: What was the version number in {0}?
                    Question = "{0}のバージョン番号は？",
                },
            },
        },

        // The Switch
        [typeof(SSwitch)] = new()
        {
            ModuleName = "ザ・スイッチ",
            Questions = new()
            {
                [SSwitch.InitialColor] = new()
                {
                    // English: What color was the {1} LED on the {2} flip of {0}?
                    // Example: What color was the top LED on the first flip of The Switch?
                    Question = "{0}の{2}回目の切り替え時の{1}部のLEDの色は？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["bottom"] = "下",
                    },
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["orange"] = "オレンジ",
                        ["yellow"] = "黄",
                        ["green"] = "緑",
                        ["blue"] = "青",
                        ["purple"] = "紫",
                    },
                },
            },
        },

        // Switches
        [typeof(SSwitches)] = new()
        {
            ModuleName = "スイッチ",
            Questions = new()
            {
                [SSwitches.InitialPosition] = new()
                {
                    // English: What was the initial position of the switches in {0}?
                    Question = "{0}の最初の状態は？",
                },
            },
        },

        // Switching Maze
        [typeof(SSwitchingMaze)] = new()
        {
            ModuleName = "切り替え迷路",
            Questions = new()
            {
                [SSwitchingMaze.Seed] = new()
                {
                    // English: What was the seed in {0}?
                    Question = "{0}のシード値は？",
                },
                [SSwitchingMaze.Color] = new()
                {
                    // English: What was the starting maze color in {0}?
                    Question = "{0}の開始迷路の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Magenta"] = "マゼンタ",
                        ["Cyan"] = "シアン",
                        ["Yellow"] = "黄",
                        ["Black"] = "黒",
                        ["White"] = "白",
                        ["Gray"] = "灰",
                        ["Orange"] = "オレンジ",
                        ["Pink"] = "ピンク",
                        ["Brown"] = "茶",
                    },
                },
            },
        },

        // Symbol Cycle
        [typeof(SSymbolCycle)] = new()
        {
            ModuleName = "シンボルサイクル",
            Questions = new()
            {
                [SSymbolCycle.SymbolCounts] = new()
                {
                    // English: How many symbols were cycling on the {1} screen in {0}?
                    // Example: How many symbols were cycling on the left screen in Symbol Cycle?
                    Question = "{0}にて{1}側のディスプレーに表示されたシンボルの数は？",
                    Arguments = new()
                    {
                        ["left"] = "左",
                        ["right"] = "右",
                    },
                },
            },
        },

        // Symbolic Coordinates
        [typeof(SSymbolicCoordinates)] = new()
        {
            ModuleName = "シンボル座標",
            Questions = new()
            {
                [SSymbolicCoordinates.ymbols] = new()
                {
                    // English: What was the {1} symbol in the {2} stage of {0}?
                    // Example: What was the left symbol in the first stage of Symbolic Coordinates?
                    Question = "{0}のステージ{2}における{1}のシンボルは？",
                    Arguments = new()
                    {
                        ["left"] = "左",
                        ["middle"] = "中央",
                        ["right"] = "右",
                    },
                },
            },
        },

        // Symbolic Tasha
        [typeof(SSymbolicTasha)] = new()
        {
            ModuleName = "シンボルターシャ",
            Questions = new()
            {
                [SSymbolicTasha.DirectionFlashes] = new()
                {
                    // English: Which button flashed {1} in the final sequence of {0}?
                    // Example: Which button flashed first in the final sequence of Symbolic Tasha?
                    Question = "{0}の最後のシークエンスで{1}番目に点滅したものは？",
                    Answers = new()
                    {
                        ["Top"] = "上",
                        ["Right"] = "右",
                        ["Bottom"] = "下",
                        ["Left"] = "左",
                    },
                },
                [SSymbolicTasha.ColorFlashes] = new()
                {
                    // English: Which button flashed {1} in the final sequence of {0}?
                    // Example: Which button flashed first in the final sequence of Symbolic Tasha?
                    Question = "{0}の最後のシークエンスで{1}番目に点滅したものは？",
                    Answers = new()
                    {
                        ["Pink"] = "ピンク",
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                        ["Blue"] = "青",
                    },
                },
                [SSymbolicTasha.Symbols] = new()
                {
                    // English: Which symbol was on the {1} button in {0}?
                    // Example: Which symbol was on the top button in Symbolic Tasha?
                    Question = "{0}の{1}の位置のシンボルは？",
                    Arguments = new()
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
            },
        },

        // Synapse Says
        [typeof(SSynapseSays)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "記憶ゲーム",
            Questions = new()
            {
                [SSynapseSays.Flashes] = new()
                {
                    // English: What color flashed {1} in the {2} stage of {0}?
                    // Example: What color flashed first in the first stage of Synapse Says?
                    Question = "{0}にてステージ{2}で{1}番目に点滅した色は？",
                },
                [SSynapseSays.Positions] = new()
                {
                    // English: What color was in the {1} position of the {2} stage of {0}?
                    // Example: What color was in the first position of the first stage of Synapse Says?
                    Question = "{0}にてステージ{2}の左から{1}番目のボタンの色は？",
                },
            },
        },

        // SYNC-125 [3]
        [typeof(SSync125_3)] = new()
        {
            Questions = new()
            {
                [SSync125_3.Word] = new()
                {
                    // English: What was displayed on the screen in the {1} stage of {0}?
                    // Example: What was displayed on the screen in the first stage of SYNC-125 [3]?
                    Question = "{0}にてステージ{1}でスクリーンに表示されたものは？",
                },
            },
        },

        // Synonyms
        [typeof(SSynonyms)] = new()
        {
            ModuleName = "同義語",
            Questions = new()
            {
                [SSynonyms.Number] = new()
                {
                    // English: Which number was displayed on {0}?
                    Question = "{0}のディスプレーの数字は？",
                },
            },
        },

        // Sysadmin
        [typeof(SSysadmin)] = new()
        {
            ModuleName = "システム管理者",
            Questions = new()
            {
                [SSysadmin.FixedErrorCodes] = new()
                {
                    // English: What error code did you fix in {0}?
                    Question = "{0}で修正したエラーコードは？",
                },
            },
        },

        // TAC
        [typeof(STAC)] = new()
        {
            ModuleName = "タック",
            Questions = new()
            {
                [STAC.SwappedCard] = new()
                {
                    // English: Which card was {1} in the swap in {0}?
                    // Example: Which card was given away in the swap in TAC?
                    Question = "{0}のカード交換で{1}カードは？",
                    Arguments = new()
                    {
                        ["given away"] = "捨てた",
                        ["received"] = "引いた",
                    },
                    Answers = new()
                    {
                        ["1"] = "通常1",
                        ["2"] = "通常2",
                        ["3"] = "通常3",
                        ["4"] = "通常4",
                        ["5"] = "通常5",
                        ["6"] = "通常6",
                        ["7"] = "通常7",
                        ["8"] = "通常8",
                        ["9"] = "通常9",
                        ["10"] = "通常10",
                        ["11"] = "通常11",
                        ["12"] = "通常12",
                        ["13"] = "通常13",
                        ["backwards 3"] = "リバース3",
                        ["backwards 4"] = "リバース4",
                        ["backwards 5"] = "リバース5",
                        ["single-step 6"] = "毎回捕獲可6",
                        ["single-step 7"] = "毎回捕獲可7",
                        ["8 or discard"] = "ダイヤ付き8",
                        ["9 or discard"] = "ダイヤ付き9",
                        ["10 or discard"] = "ダイヤ付き10",
                        ["Warrior"] = "Warrior",
                        ["Trickster"] = "Trickster",
                    },
                },
                [STAC.HeldCard] = new()
                {
                    // English: Which card was in your hand in {0}?
                    Question = "{0}で手札にあったカードは？",
                    Answers = new()
                    {
                        ["1"] = "通常1",
                        ["2"] = "通常2",
                        ["3"] = "通常3",
                        ["4"] = "通常4",
                        ["5"] = "通常5",
                        ["6"] = "通常6",
                        ["7"] = "通常7",
                        ["8"] = "通常8",
                        ["9"] = "通常9",
                        ["10"] = "通常10",
                        ["11"] = "通常11",
                        ["12"] = "通常12",
                        ["13"] = "通常13",
                        ["backwards 3"] = "リバース3",
                        ["backwards 4"] = "リバース4",
                        ["backwards 5"] = "リバース5",
                        ["single-step 6"] = "毎回捕獲可6",
                        ["single-step 7"] = "毎回捕獲可7",
                        ["8 or discard"] = "ダイヤ付き8",
                        ["9 or discard"] = "ダイヤ付き9",
                        ["10 or discard"] = "ダイヤ付き10",
                        ["Warrior"] = "Warrior",
                        ["Trickster"] = "Trickster",
                    },
                },
            },
        },

        // Tap Code
        [typeof(STapCode)] = new()
        {
            ModuleName = "タップ・コード",
            Questions = new()
            {
                [STapCode.ReceivedWord] = new()
                {
                    // English: What was the received word in {0}?
                    Question = "{0}で受信した単語は？",
                },
            },
        },

        // Tasha Squeals
        [typeof(STashaSqueals)] = new()
        {
            ModuleName = "ターシャの悲鳴",
            Questions = new()
            {
                [STashaSqueals.Colors] = new()
                {
                    // English: What was the {1} flashed color in {0}?
                    // Example: What was the first flashed color in Tasha Squeals?
                    Question = "{0}で{1}番目に点滅した色は？",
                    Answers = new()
                    {
                        ["Pink"] = "ピンク",
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                        ["Blue"] = "青",
                    },
                },
            },
        },

        // Tasque Managing
        [typeof(STasqueManaging)] = new()
        {
            ModuleName = "タスク管理",
            Questions = new()
            {
                [STasqueManaging.StartingPos] = new()
                {
                    // English: Where was the starting position in {0}?
                    Question = "{0}の開始位置は？",
                },
            },
        },

        // The Tea Set
        [typeof(STeaSet)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STeaSet.DisplayedIngredients] = new()
                {
                    // English: Which ingredient was displayed {1}, from left to right, in {0}?
                    // Example: Which ingredient was displayed first, from left to right, in The Tea Set?
                    Question = "{0}で左から{1}番目に表示された材料は？",
                },
            },
        },

        // Technical Keypad
        [typeof(STechnicalKeypad)] = new()
        {
            ModuleName = "テクニックキーパッド",
            Questions = new()
            {
                [STechnicalKeypad.DisplayedDigits] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "{0}で{1}番目に表示された数字は？",
                },
            },
        },

        // Ten-Button Color Code
        [typeof(STenButtonColorCode)] = new()
        {
            ModuleName = "10ボタン色コード",
            Questions = new()
            {
                [STenButtonColorCode.InitialColors] = new()
                {
                    // English: What was the initial color of the {1} button in the {2} stage of {0}?
                    // Example: What was the initial color of the first button in the first stage of Ten-Button Color Code?
                    Question = "{0}のステージ{2}における{1}番目のボタンの初期の色は？",
                    Answers = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
            },
        },

        // Tenpins
        [typeof(STenpins)] = new()
        {
            ModuleName = "テンピン",
            Questions = new()
            {
                [STenpins.Splits] = new()
                {
                    // English: What was the {1} split in {0}?
                    // Example: What was the red split in Tenpins?
                    Question = "{0}の{1}のスプリットは？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                    Answers = new()
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
            },
        },

        // Tetriamonds
        [typeof(STetriamonds)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STetriamonds.PulsingColours] = new()
                {
                    // English: What colour triangle pulsed {1} in {0}?
                    // Example: What colour triangle pulsed first in Tetriamonds?
                    Question = "{0}で{1}番目に動いた三角形の色は？",
                    Answers = new()
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
            },
        },

        // Text Field
        [typeof(STextField)] = new()
        {
            ModuleName = "テキストフィールド",
            Questions = new()
            {
                [STextField.Display] = new()
                {
                    // English: What was the displayed letter in {0}?
                    Question = "{0}で表示された文字は？",
                },
            },
        },

        // Thinking Wires
        [typeof(SThinkingWires)] = new()
        {
            ModuleName = "思考ワイヤ",
            Questions = new()
            {
                [SThinkingWires.FirstWire] = new()
                {
                    // English: What was the position from top to bottom of the first wire needing to be cut in {0}?
                    Question = "{0}において最初に切る必要のあるワイヤの位置(上から下)は？",
                },
                [SThinkingWires.SecondWire] = new()
                {
                    // English: What color did the second valid wire to cut have to have in {0}?
                    Question = "{0}において2番目に切った有効なワイヤの色は？",
                    Answers = new()
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
                [SThinkingWires.DisplayNumber] = new()
                {
                    // English: What was the display number in {0}?
                    Question = "{0}のディスプレーの数字は？",
                },
            },
        },

        // Third Base
        [typeof(SThirdBase)] = new()
        {
            ModuleName = "サードベース",
            Questions = new()
            {
                [SThirdBase.Display] = new()
                {
                    // English: What was the display word in the {1} stage on {0}?
                    // Example: What was the display word in the first stage on Third Base?
                    Question = "{0}にてステージ{1}で表示された単語は？",
                },
            },
        },

        // Thirty Dollar Module
        [typeof(SThirtyDollarModule)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SThirtyDollarModule.Sounds] = new()
                {
                    // English: Which sound was used in {0}?
                    Question = "{0}で使用された音は？",
                },
            },
        },

        // Tic Tac Toe
        [typeof(STicTacToe)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "○×ゲーム",
            Questions = new()
            {
                [STicTacToe.InitialState] = new()
                {
                    // English: What was on the {1} button at the start of {0}?
                    // Example: What was on the top-left button at the start of Tic Tac Toe?
                    Question = "{0}の{1}のボタンの初期状態は？",
                    Arguments = new()
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
            },
            Discriminators = new()
            {
                [STicTacToe.Discriminator] = new()
                {
                    // English: the Tic Tac Toe where the {0} button was {1}
                    // Example: the Tic Tac Toe where the top-left button was 1
                    Discriminator = "{0}のボタンが{1}だった○×ゲーム",
                    Arguments = new()
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
            },
        },

        // Time Signatures
        [typeof(STimeSignatures)] = new()
        {
            ModuleName = "拍子記号",
            Questions = new()
            {
                [STimeSignatures.Signatures] = new()
                {
                    // English: What was the {1} time signature in {0}?
                    // Example: What was the first time signature in Time Signatures?
                    Question = "{0}の{1}番目の拍子記号は？",
                },
            },
        },

        // Timezone
        [typeof(STimezone)] = new()
        {
            ModuleName = "タイムゾーン",
            Questions = new()
            {
                [STimezone.Cities] = new()
                {
                    // English: What was the {1} city in {0}?
                    // Example: What was the departure city in Timezone?
                    Question = "{0}の{1}都市は？",
                    Arguments = new()
                    {
                        ["departure"] = "上の",
                        ["destination"] = "下の",
                    },
                },
            },
        },

        // Tip Toe
        [typeof(STipToe)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STipToe.SafeSquares] = new()
                {
                    // English: Which of these squares was safe in row {1} in {0}?
                    // Example: Which of these squares was safe in row 9 in Tip Toe?
                    Question = "{0}の段{1}で安全だったマスは？",
                },
            },
        },

        // Topsy Turvy
        [typeof(STopsyTurvy)] = new()
        {
            ModuleName = "トプシー・タービー",
            Questions = new()
            {
                [STopsyTurvy.Word] = new()
                {
                    // English: What was the word initially shown in {0}?
                    Question = "{0}で最初に表示された単語は？",
                },
            },
        },

        // Touch Transmission
        [typeof(STouchTransmission)] = new()
        {
            ModuleName = "接触送信",
            Questions = new()
            {
                [STouchTransmission.Word] = new()
                {
                    // English: What was the transmitted word in {0}?
                    Question = "{0}で送信した単語は？",
                },
                [STouchTransmission.Order] = new()
                {
                    // English: In what order was the Braille read in {0}?
                    Question = "{0}では点字をどのような順序で読んだ？",
                    Answers = new()
                    {
                        ["Standard Braille Order"] = "標準",
                        ["Individual Reading Order"] = "個別読み順",
                        ["Merged Reading Order"] = "全体読み順",
                        ["Chinese Reading Order"] = "漢字読み順",
                    },
                },
            },
        },

        // Toxic Crystals
        [typeof(SToxicCrystals)] = new()
        {
            NeedsTranslation = true,
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
            ModuleName = "送信モールス",
            Questions = new()
            {
                [STransmittedMorse.Message] = new()
                {
                    // English: What was the {1} received message in {0}?
                    // Example: What was the first received message in Transmitted Morse?
                    Question = "{0}にて{1}番目に受け取ったメッセージは？",
                },
            },
        },

        // Triamonds
        [typeof(STriamonds)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STriamonds.PulsingColours] = new()
                {
                    // English: What colour triangle pulsed {1} in {0}?
                    // Example: What colour triangle pulsed first in Triamonds?
                    Question = "{0}で{1}番目に動いた三角形の色は？",
                    Answers = new()
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
            },
        },

        // Tribal Council
        [typeof(STribalCouncil)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STribalCouncil.Name] = new()
                {
                    // English: What was the {1} name in {0}?
                    // Example: What was the northeast name in Tribal Council?
                    Question = "{0}の{1}の名前は？",
                    Arguments = new()
                    {
                        ["northeast"] = "北東",
                        ["southwest"] = "南西",
                    },
                },
            },
        },

        // Triple Term
        [typeof(STripleTerm)] = new()
        {
            ModuleName = "三単語",
            Questions = new()
            {
                [STripleTerm.Passwords] = new()
                {
                    // English: Which of these was one of the passwords in {0}?
                    Question = "{0}のパスワードの一つであるのは？",
                },
            },
        },

        // Turtle Robot
        [typeof(STurtleRobot)] = new()
        {
            ModuleName = "亀型ロボット",
            Questions = new()
            {
                [STurtleRobot.CodeLines] = new()
                {
                    // English: What was the {1} line you commented out in {0}?
                    // Example: What was the first line you commented out in Turtle Robot?
                    Question = "{0}で{1}番目にコメントアウトした行は？",
                },
            },
        },

        // Two Bits
        [typeof(STwoBits)] = new()
        {
            ModuleName = "ツービッツ",
            Questions = new()
            {
                [STwoBits.Response] = new()
                {
                    // English: What was the {1} correct query response from {0}?
                    // Example: What was the first correct query response from Two Bits?
                    Question = "{0}で{1}番目のクエリの返答は？",
                },
            },
        },

        // Twodoku
        [typeof(STwodoku)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STwodoku.Givens] = new()
                {
                    // English: Which of these squares in {0} was {1}?
                    // Example: Which of these squares in Twodoku was a given digit?
                    Question = "{0}のこれらのマスのうち{1}は？",
                    Arguments = new()
                    {
                        ["a given digit"] = "初期配置にあった数字",
                        ["a given shape"] = "初期配置にあった形",
                        ["highlighted"] = "強調表示されていたマス",
                    },
                },
                [STwodoku.GridPositions] = new()
                {
                    // English: What was in this grid position in {0}? (+ sprite)
                    Question = "{0}のこのマスにあったものは？",
                },
            },
        },

        // UFO Satellites
        [typeof(SUfoSatellites)] = new()
        {
            NeedsTranslation = true,
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
            ModuleName = "究極暗号",
            Questions = new()
            {
                [SUltimateCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Ultimate Cipher?
                    Question = "{0}の回答は？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Ultimate Cycle
        [typeof(SUltimateCycle)] = new()
        {
            ModuleName = "究極サイクル",
            Questions = new()
            {
                [SUltimateCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Ultimate Cycle?
                    Question = "{0}の{1}番目のダイヤルが指していた方向は？",
                },
                [SUltimateCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Ultimate Cycle?
                    Question = "{0}の{1}番目のダイヤルに書かれていた文字は？",
                },
            },
            Discriminators = new()
            {
                [SUltimateCycle.LabelDiscriminator] = new()
                {
                    // English: the Ultimate Cycle that had the letter {0} on a dial
                    // Example: the Ultimate Cycle that had the letter A on a dial
                    Discriminator = "ダイヤルに{0}の文字が書かれていた究極サイクル",
                },
            },
        },

        // The Ultracube
        [typeof(SUltracube)] = new()
        {
            ModuleName = "極立方体",
            Questions = new()
            {
                [SUltracube.Rotations] = new()
                {
                    // English: What was the {1} rotation in {0}?
                    // Example: What was the first rotation in The Ultracube?
                    Question = "{0}の{1}番目の回転方向は？",
                },
            },
        },

        // UltraStores
        [typeof(SUltraStores)] = new()
        {
            ModuleName = "極貯留",
            Questions = new()
            {
                [SUltraStores.SingleRotation] = new()
                {
                    // English: What was the {1} rotation in the {2} stage of {0}?
                    // Example: What was the first rotation in the first stage of UltraStores?
                    Question = "{0}のステージ{2}における{1}番目の回転方向は？",
                },
                [SUltraStores.MultiRotation] = new()
                {
                    // English: What was the {1} rotation in the {2} stage of {0}?
                    // Example: What was the first rotation in the first stage of UltraStores?
                    Question = "{0}のステージ{2}における{1}番目の回転方向は？",
                },
            },
        },

        // Uncolored Squares
        [typeof(SUncoloredSquares)] = new()
        {
            ModuleName = "色無し格子",
            Questions = new()
            {
                [SUncoloredSquares.FirstStage] = new()
                {
                    // English: What was the {1} color in reading order used in the first stage of {0}?
                    // Example: What was the first color in reading order used in the first stage of Uncolored Squares?
                    Question = "{0}の最初のステージで利用したもののうち読み順で{1}番目の色は何色？",
                    Answers = new()
                    {
                        ["White"] = "白",
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                        ["Magenta"] = "マゼンタ",
                    },
                },
            },
        },

        // Uncolored Switches
        [typeof(SUncoloredSwitches)] = new()
        {
            ModuleName = "色無しスイッチ",
            Questions = new()
            {
                [SUncoloredSwitches.InitialState] = new()
                {
                    // English: What was the initial state of the switches in {0}?
                    Question = "{0}の最初のスイッチの状態は？",
                },
                [SUncoloredSwitches.LedColors] = new()
                {
                    // English: What color was the {1} LED in reading order in {0}?
                    // Example: What color was the first LED in reading order in Uncolored Switches?
                    Question = "{0}の読み順で{1}番目のLEDは何色？",
                    Answers = new()
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
            },
        },

        // Uncolour Flash
        [typeof(SUncolourFlash)] = new()
        {
            ModuleName = "アンカラーフラッシュ",
            Questions = new()
            {
                [SUncolourFlash.Displays] = new()
                {
                    // English: What was the {1} in the {2} position of the “{3}” sequence of {0}?
                    // Example: What was the word in the first position of the “YES” sequence of Uncolour Flash?
                    Question = "{0}の{3}シークエンスの{2}番目にあった{1}は？",
                    Arguments = new()
                    {
                        ["word"] = "単語",
                        ["colour of the word"] = "色",
                    },
                },
            },
            Discriminators = new()
            {
                [SUncolourFlash.Discriminator] = new()
                {
                    // English: the Uncolour Flash where the {0} in the {1} position of the “{2}” sequence was {3}
                    // Example: the Uncolour Flash where the word in the first position of the “YES” sequence was Red
                    Discriminator = "{2}シークエンスの{1}番目の{0}が{3}だったアンカラーフラッシュ",
                    Arguments = new()
                    {
                        ["word"] = "単語",
                        ["colour of the word"] = "色",
                    },
                },
            },
        },

        // Undertunneling
        [typeof(SUndertunneling)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SUndertunneling.PositionInMazeAfterPhaseOne] = new()
                {
                    // English: What was the position in the maze after the first phase in {0}?
                    Question = "{0}で最初のフェーズ終了後の迷路内の位置は？",
                },
            },
        },

        // Unfair Cipher
        [typeof(SUnfairCipher)] = new()
        {
            ModuleName = "アンフェア暗号",
            Questions = new()
            {
                [SUnfairCipher.Instructions] = new()
                {
                    // English: What was the {1} received instruction in {0}?
                    // Example: What was the first received instruction in Unfair Cipher?
                    Question = "{0}で{1}番目に受け取った指示は？",
                },
            },
        },

        // Unfair's Cruel Revenge
        [typeof(SUnfairsCruelRevenge)] = new()
        {
            ModuleName = "アンフェアの残忍逆襲",
            Questions = new()
            {
                [SUnfairsCruelRevenge.Instructions] = new()
                {
                    // English: What was the {1} decrypted instruction in {0}?
                    // Example: What was the first decrypted instruction in Unfair's Cruel Revenge?
                    Question = "{0}で解読した指示の{1}番目は？",
                },
                [SUnfairsCruelRevenge.InstructionsLegacy] = new()
                {
                    // English: What was the {1} decrypted instruction in {0}?
                    // Example: What was the first decrypted instruction in Unfair's Cruel Revenge?
                    Question = "{0}で解読した指示の{1}番目は？",
                },
            },
        },

        // Unfair’s Revenge
        [typeof(SUnfairsRevenge)] = new()
        {
            ModuleName = "アンフェアの逆襲",
            Questions = new()
            {
                [SUnfairsRevenge.Instructions] = new()
                {
                    // English: What was the {1} decrypted instruction in {0}?
                    // Example: What was the first decrypted instruction in Unfair’s Revenge?
                    Question = "{0}で{1}番目に解読した指示は？",
                },
            },
        },

        // Unicode
        [typeof(SUnicode)] = new()
        {
            ModuleName = "ユニコード",
            Questions = new()
            {
                [SUnicode.SortedAnswer] = new()
                {
                    // English: What was the {1} submitted code in {0}?
                    // Example: What was the first submitted code in Unicode?
                    Question = "{0}にて{1}番目に送信したコードは？",
                },
            },
        },

        // UNO!
        [typeof(SUNO)] = new()
        {
            ModuleName = "UNO",
            Questions = new()
            {
                [SUNO.InitialCard] = new()
                {
                    // English: What was the initial card in {0}?
                    Question = "{0}の最初のカードは？",
                    Answers = new()
                    {
                        ["Red 0"] = "赤 0",
                        ["Red 1"] = "赤 1",
                        ["Red 2"] = "赤 2",
                        ["Red 3"] = "赤 3",
                        ["Red 4"] = "赤 4",
                        ["Red 5"] = "赤 5",
                        ["Red 6"] = "赤 6",
                        ["Red 7"] = "赤 7",
                        ["Red 8"] = "赤 8",
                        ["Red 9"] = "赤 9",
                        ["Red +2"] = "赤 +2",
                        ["Red Skip"] = "赤 スキップ",
                        ["Red Reverse"] = "赤 リバース",
                        ["Green 0"] = "緑 0",
                        ["Green 1"] = "緑 1",
                        ["Green 2"] = "緑 2",
                        ["Green 3"] = "緑 3",
                        ["Green 4"] = "緑 4",
                        ["Green 5"] = "緑 5",
                        ["Green 6"] = "緑 6",
                        ["Green 7"] = "緑 7",
                        ["Green 8"] = "緑 8",
                        ["Green 9"] = "緑 9",
                        ["Green +2"] = "緑 +2",
                        ["Green Skip"] = "緑 スキップ",
                        ["Green Reverse"] = "緑 リバース",
                        ["Yellow 0"] = "黄 0",
                        ["Yellow 1"] = "黄 1",
                        ["Yellow 2"] = "黄 2",
                        ["Yellow 3"] = "黄 3",
                        ["Yellow 4"] = "黄 4",
                        ["Yellow 5"] = "黄 5",
                        ["Yellow 6"] = "黄 6",
                        ["Yellow 7"] = "黄 7",
                        ["Yellow 8"] = "黄 8",
                        ["Yellow 9"] = "黄 9",
                        ["Yellow +2"] = "黄 +2",
                        ["Yellow Skip"] = "黄 スキップ",
                        ["Yellow Reverse"] = "黄 リバース",
                        ["Blue 0"] = "青 0",
                        ["Blue 1"] = "青 1",
                        ["Blue 2"] = "青 2",
                        ["Blue 3"] = "青 3",
                        ["Blue 4"] = "青 4",
                        ["Blue 5"] = "青 5",
                        ["Blue 6"] = "青 6",
                        ["Blue 7"] = "青 7",
                        ["Blue 8"] = "青 8",
                        ["Blue 9"] = "青 9",
                        ["Blue +2"] = "青 +2",
                        ["Blue Skip"] = "青 スキップ",
                        ["Blue Reverse"] = "青 リバース",
                        ["+4"] = "+4",
                        ["Wild"] = "ワイルド",
                    },
                },
            },
        },

        // Unordered Keys
        [typeof(SUnorderedKeys)] = new()
        {
            ModuleName = "順番無し音板",
            Questions = new()
            {
                [SUnorderedKeys.KeyColor] = new()
                {
                    // English: What color was this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "{0}のステージ{1}におけるこの音板の色は？",
                },
                [SUnorderedKeys.LabelColor] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was the label of this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "{0}のステージ{1}におけるこの音板のラベルの色は？",
                },
                [SUnorderedKeys.Label] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What was the label of this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "{0}のステージ{1}におけるこの音板のラベルの色は？",
                },
            },
        },

        // Unown Cipher
        [typeof(SUnownCipher)] = new()
        {
            ModuleName = "アンノーン暗号",
            Questions = new()
            {
                [SUnownCipher.Answers] = new()
                {
                    // English: What was the {1} submitted letter in {0}?
                    // Example: What was the first submitted letter in Unown Cipher?
                    Question = "{0}にて送信した単語の{1}番目の英字は？",
                },
            },
        },

        // Unpleasant Squares
        [typeof(SUnpleasantSquares)] = new()
        {
            ModuleName = "不愉快な正方形",
            Questions = new()
            {
                [SUnpleasantSquares.Color] = new()
                {
                    // English: What was the color of this square in {0}? (+ sprite)
                    Question = "{0}のこの正方形の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Jade"] = "翡翠",
                        ["Azure"] = "空",
                        ["Violet"] = "紫",
                    },
                },
            },
        },

        // Updog
        [typeof(SUpdog)] = new()
        {
            ModuleName = "うざいイヌ",
            Questions = new()
            {
                [SUpdog.Word] = new()
                {
                    // English: What was the text on {0}?
                    Question = "{0}のテキストは？",
                },
                [SUpdog.Color] = new()
                {
                    // English: What was the {1} color in the sequence on {0}?
                    // Example: What was the first color in the sequence on Updog?
                    Question = "{0}のシーケンスにおける{1}の色は？",
                    Arguments = new()
                    {
                        ["first"] = "最初",
                        ["last"] = "最後",
                    },
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Orange"] = "橙",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Purple"] = "紫",
                    },
                },
            },
        },

        // USA Cycle
        [typeof(SUSACycle)] = new()
        {
            ModuleName = "USAサイクル",
            Questions = new()
            {
                [SUSACycle.Displayed] = new()
                {
                    // English: Which state was displayed in {0}?
                    Question = "{0}で表示された州は？",
                },
            },
        },

        // USA Maze
        [typeof(SUSAMaze)] = new()
        {
            ModuleName = "USA迷路",
            Questions = new()
            {
                [SUSAMaze.Origin] = new()
                {
                    // English: Which state did you depart from in {0}?
                    Question = "{0}の開始地点は？",
                },
            },
        },

        // V
        [typeof(SV)] = new()
        {
            Questions = new()
            {
                [SV.Words] = new()
                {
                    // English: Which word {1} shown in {0}?
                    // Example: Which word was shown in V?
                    Question = "{0}で表示{1}単語は？",
                    Arguments = new()
                    {
                        ["was"] = "された",
                        ["was not"] = "されなかった",
                    },
                },
            },
        },

        // Valves
        [typeof(SValves)] = new()
        {
            ModuleName = "バルブ",
            Questions = new()
            {
                [SValves.InitialState] = new()
                {
                    // English: What was the initial state of {0}?
                    Question = "{0}の初期状態は？",
                },
            },
        },

        // Varicolored Squares
        [typeof(SVaricoloredSquares)] = new()
        {
            ModuleName = "色どり格子",
            Questions = new()
            {
                [SVaricoloredSquares.InitialColor] = new()
                {
                    // English: What was the initially pressed color on {0}?
                    Question = "{0}で最初に押した色は？",
                    Answers = new()
                    {
                        ["White"] = "白",
                        ["Red"] = "赤",
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Yellow"] = "黄",
                        ["Magenta"] = "マゼンタ",
                    },
                },
            },
        },

        // Varicolour Flash
        [typeof(SVaricolourFlash)] = new()
        {
            ModuleName = "バリカラーフラッシュ",
            Questions = new()
            {
                [SVaricolourFlash.Words] = new()
                {
                    // English: What was the word of the {1} goal in {0}?
                    // Example: What was the word of the first goal in Varicolour Flash?
                    Question = "{0}の{1}番目のゴールの単語は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                        ["White"] = "白",
                    },
                },
                [SVaricolourFlash.Colors] = new()
                {
                    // English: What was the color of the {1} goal in {0}?
                    // Example: What was the color of the first goal in Varicolour Flash?
                    Question = "{0}の{1}番目のゴールの色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Magenta"] = "マゼンタ",
                        ["Yellow"] = "黄",
                        ["White"] = "白",
                    },
                },
            },
        },

        // Variety
        [typeof(SVariety)] = new()
        {
            ModuleName = "寄せ集め",
            Questions = new()
            {
                [SVariety.LED] = new()
                {
                    // English: What color was the LED flashing in {0}?
                    Question = "{0}で点滅したLEDの色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                        ["Blue"] = "青",
                        ["White"] = "白",
                        ["Black"] = "黒",
                    },
                },
                [SVariety.DigitDisplay] = new()
                {
                    // English: What digit was displayed, but not the answer, for the digit display in {0}?
                    Question = "{0}の数字ディスプレーに表示されたが回答ではないものは？",
                },
                [SVariety.LetterDisplay] = new()
                {
                    // English: What word could be formed, but was not the answer, for the letter display in {0}?
                    Question = "{0}の英字ディスプレーから形成できるが回答ではないものは？",
                },
                [SVariety.Timer] = new()
                {
                    // English: What was the maximum display for the {1} in {0}?
                    // Example: What was the maximum display for the timer in Variety?
                    Question = "{0}の{1}が表示できた最大値は？",
                    Arguments = new()
                    {
                        ["timer"] = "タイマー",
                        ["ascending timer"] = "カウントアップタイマー",
                        ["descending timer"] = "カウントダウンタイマー",
                    },
                },
                [SVariety.ColoredKnob] = new()
                {
                    // English: What was n for the {1} in {0}?
                    // Example: What was n for the knob in Variety?
                    Question = "{0}の{1}に対応するNは？",
                    Arguments = new()
                    {
                        ["knob"] = "ダイヤル",
                        ["colored knob"] = "色付きダイヤル",
                        ["red knob"] = "赤色ダイヤル",
                        ["black knob"] = "黒色ダイヤル",
                        ["blue knob"] = "青色ダイヤル",
                        ["yellow knob"] = "黄色ダイヤル",
                    },
                },
                [SVariety.Bulb] = new()
                {
                    // English: What was n for the {1} in {0}?
                    // Example: What was n for the bulb in Variety?
                    Question = "{0}の{1}電球に対するNは？",
                    Arguments = new()
                    {
                        ["bulb"] = "電球",
                        ["red bulb"] = "赤色電球",
                        ["yellow bulb"] = "黄色電球",
                    },
                },
            },
            Discriminators = new()
            {
                [SVariety.Has] = new()
                {
                    // English: the Variety that has {0}
                    // Example: the Variety that has one
                    Discriminator = "{0}があった寄せ集め",
                    // Refer to translations.md to understand the weird strings
                    Arguments = new()
                    {
                        ["one\uE003 (LED)"] = "LED",
                        ["one\uE003 (digit display)"] = "数字ディスプレー",
                        ["one\uE003 (letter display)"] = "英字ディスプレー",
                        ["one\uE003 (timer)"] = "タイマー",
                        ["one\uE003 (knob)"] = "ダイヤル",
                        ["one\uE003 (colored knob)"] = "色付きダイヤル",
                        ["one\uE003 (redknob)"] = "赤色ダイヤル",
                        ["one\uE003 (yellowknob)"] = "黄色ダイヤル",
                        ["one\uE003 (blueknob)"] = "青色ダイヤル",
                        ["one\uE003 (blackknob)"] = "黒色ダイヤル",
                        ["one\uE003 (bulb)"] = "電球",
                        ["one\uE003 (redbulb)"] = "赤色電球",
                        ["one\uE003 (yellowbulb)"] = "黄色電球",
                        ["a knob"] = "ダイヤル",
                        ["a colored knob"] = "色付きダイヤル",
                        ["a white knob"] = "白色ダイヤル",
                        ["a red knob"] = "赤色ダイヤル",
                        ["a black knob"] = "黒色ダイヤル",
                        ["a blue knob"] = "青色ダイヤル",
                        ["a yellow knob"] = "黄色ダイヤル",
                        ["a keypad"] = "キーパッド",
                        ["a white keypad"] = "白色キーパッド",
                        ["a red keypad"] = "赤色キーパッド",
                        ["a yellow keypad"] = "黄色キーパッド",
                        ["a blue keypad"] = "青色キーパッド",
                        ["a slider"] = "スライダー",
                        ["a horizontal slider"] = "横スライダー",
                        ["a vertical slider"] = "縦スライダー",
                        ["an LED"] = "LED",
                        ["a digit display"] = "数字ディスプレー",
                        ["a wire"] = "ワイヤ",
                        ["a black wire"] = "黒色ワイヤ",
                        ["a blue wire"] = "青色ワイヤ",
                        ["a red wire"] = "赤色ワイヤ",
                        ["a yellow wire"] = "黄色ワイヤ",
                        ["a white wire"] = "白色ワイヤ",
                        ["a button"] = "ボタン",
                        ["a red button"] = "赤色ボタン",
                        ["a yellow button"] = "黄色ボタン",
                        ["a blue button"] = "青色ボタン",
                        ["a white button"] = "白色ボタン",
                        ["a letter display"] = "英字ディスプレー",
                        ["a Braille display"] = "点字ディスプレー",
                        ["a key-in-lock"] = "鍵付きロック",
                        ["a switch"] = "スイッチ",
                        ["a red switch"] = "赤色スイッチ",
                        ["a yellow switch"] = "黄色スイッチ",
                        ["a blue switch"] = "青色スイッチ",
                        ["a white switch"] = "白色スイッチ",
                        ["a timer"] = "タイマー",
                        ["an ascending timer"] = "カウントアップタイマー",
                        ["a descending timer"] = "カウントダウンタイマー",
                        ["a die"] = "サイコロ",
                        ["a light-on-dark die"] = "明色サイコロ",
                        ["a dark-on-light die"] = "暗色サイコロ",
                        ["a bulb"] = "電球",
                        ["a red bulb"] = "赤色電球",
                        ["a yellow bulb"] = "黄色電球",
                        ["a maze"] = "迷路",
                        ["a 3×3 maze"] = "3×3迷路",
                        ["a 3×4 maze"] = "3×4迷路",
                        ["a 4×3 maze"] = "4×3迷路",
                        ["a 4×4 maze"] = "4×4迷路",
                    },
                },
            },
        },

        // Vcrcs
        [typeof(SVcrcs)] = new()
        {
            ModuleName = "単語識別",
            Questions = new()
            {
                [SVcrcs.Word] = new()
                {
                    // English: What was the word in {0}?
                    Question = "{0}で表示された単語は？",
                },
            },
        },

        // Vectors
        [typeof(SVectors)] = new()
        {
            ModuleName = "ベクトル",
            Questions = new()
            {
                [SVectors.Colors] = new()
                {
                    // English: What was the color of the {1} vector in {0}?
                    // Example: What was the color of the first vector in Vectors?
                    Question = "{0}にあった{1}のベクトルの色は？",
                    Arguments = new()
                    {
                        ["first"] = "1番目",
                        ["second"] = "2番目",
                        ["third"] = "3番目",
                        ["only"] = "唯一",
                    },
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Orange"] = "オレンジ",
                        ["Yellow"] = "黄",
                        ["Green"] = "緑",
                        ["Blue"] = "青",
                        ["Purple"] = "紫",
                    },
                },
            },
        },

        // Vexillology
        [typeof(SVexillology)] = new()
        {
            ModuleName = "旗章学",
            Questions = new()
            {
                [SVexillology.Colors] = new()
                {
                    // English: What was the {1} flagpole color on {0}?
                    // Example: What was the first flagpole color on Vexillology?
                    Question = "{0}にてポールの色の{1}番目の色は？",
                    Answers = new()
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
            },
        },

        // Violet Cipher
        [typeof(SVioletCipher)] = new()
        {
            ModuleName = "紫色暗号",
            Questions = new()
            {
                [SVioletCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Violet Cipher?
                    Question = "{0}の回答は？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Visual Impairment
        [typeof(SVisualImpairment)] = new()
        {
            ModuleName = "視覚障害",
            Questions = new()
            {
                [SVisualImpairment.Colors] = new()
                {
                    // English: What was the desired color in the {1} stage on {0}?
                    // Example: What was the desired color in the first stage on Visual Impairment?
                    Question = "{0}にてステージ{1}で押す必要のあった色は？",
                    Answers = new()
                    {
                        ["Blue"] = "青",
                        ["Green"] = "緑",
                        ["Red"] = "赤",
                        ["White"] = "白",
                    },
                },
            },
        },

        // Walking Cube
        [typeof(SWalkingCube)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWalkingCube.Path] = new()
                {
                    // English: Which of these cells was part of the cube’s path in {0}?
                    Question = "{0}のキューブの通り道に含まれるセルは？",
                },
            },
        },

        // Warning Signs
        [typeof(SWarningSigns)] = new()
        {
            ModuleName = "警戒標識",
            Questions = new()
            {
                [SWarningSigns.DisplayedSign] = new()
                {
                    // English: What was the displayed sign in {0}?
                    Question = "{0}で表示された標識は？",
                },
            },
        },

        // WASD
        [typeof(SWasd)] = new()
        {
            Questions = new()
            {
                [SWasd.DisplayedLocation] = new()
                {
                    // English: What was the location displayed in {0}?
                    Question = "{0}で表示された位置は？",
                },
            },
        },

        // Watching Paint Dry
        [typeof(SWatchingPaintDry)] = new()
        {
            ModuleName = "ペンキの乾燥観察",
            Questions = new()
            {
                [SWatchingPaintDry.StrokeCount] = new()
                {
                    // English: How many brush strokes were heard in {0}?
                    Question = "{0}で聞こえたブラシのストローク数は？",
                },
            },
        },

        // Wavetapping
        [typeof(SWavetapping)] = new()
        {
            ModuleName = "ウェーブタッピング",
            Questions = new()
            {
                [SWavetapping.Patterns] = new()
                {
                    // English: What was the correct pattern on the {1} stage in {0}?
                    // Example: What was the correct pattern on the first stage in Wavetapping?
                    Question = "{0}にてステージ{1}の正しいパターンは？",
                },
                [SWavetapping.Colors] = new()
                {
                    // English: What was the color on the {1} stage in {0}?
                    // Example: What was the color on the first stage in Wavetapping?
                    Question = "{0}のステージ{1}の色は？",
                    Answers = new()
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
            },
        },

        // The Weakest Link
        [typeof(SWeakestLink)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SWeakestLink.Elimination] = new()
                {
                    // English: Who did you eliminate in {0}?
                    Question = "{0}で退場させられたのは？",
                },
                [SWeakestLink.MoneyPhaseName] = new()
                {
                    // English: Who made it to the Money Phase with you in {0}?
                    Question = "{0}であなたと一緒にマネーフェイズに進出したのは？",
                },
                [SWeakestLink.Skill] = new()
                {
                    // English: What was {1}’s skill in {0}?
                    // Example: What was Annie’s skill in The Weakest Link?
                    Question = "{0}における{1}のスキルは？",
                },
                [SWeakestLink.Ratio] = new()
                {
                    // English: What ratio did {1} get in the Question Phase in {0}?
                    // Example: What ratio did Annie get in the Question Phase in The Weakest Link?
                    Question = "{0}の質問フェイズで{1}が獲得した倍率は？",
                },
            },
        },

        // What’s on Second
        [typeof(SWhatsOnSecond)] = new()
        {
            ModuleName = "色付き表比較",
            Questions = new()
            {
                [SWhatsOnSecond.DisplayText] = new()
                {
                    // English: What was the display text in the {1} stage of {0}?
                    // Example: What was the display text in the first stage of What’s on Second?
                    Question = "{0}にてステージ{1}で表示されたテキストは？",
                },
                [SWhatsOnSecond.DisplayColor] = new()
                {
                    // English: What was the display text color in the {1} stage of {0}?
                    // Example: What was the display text color in the first stage of What’s on Second?
                    Question = "{0}にてステージ{1}で表示されたテキストの色は？",
                    Answers = new()
                    {
                        ["Blue"] = "青",
                        ["Cyan"] = "シアン",
                        ["Green"] = "緑",
                        ["Magenta"] = "マゼンタ",
                        ["Red"] = "赤",
                        ["Yellow"] = "黄",
                    },
                },
            },
        },

        // White Arrows
        [typeof(SWhiteArrows)] = new()
        {
            ModuleName = "白色矢印",
            Questions = new()
            {
                [SWhiteArrows.Arrows] = new()
                {
                    // English: What was the {1} non-white arrow in {0}?
                    // Example: What was the first non-white arrow in White Arrows?
                    Question = "{0}における{1}番目の白ではない矢印は？",
                    // Refer to translations.md to understand the weird strings
                    Additional = new()
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
            },
        },

        // White Cipher
        [typeof(SWhiteCipher)] = new()
        {
            ModuleName = "白色暗号",
            Questions = new()
            {
                [SWhiteCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in White Cipher?
                    Question = "{0}の回答は？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // WhoOF
        [typeof(SWhoOF)] = new()
        {
            ModuleName = "表比",
            Questions = new()
            {
                [SWhoOF.Display] = new()
                {
                    // English: What was the display in the {1} stage on {0}?
                    // Example: What was the display in the first stage on WhoOF?
                    Question = "{0}にてステージ{1}で表示されたのは？",
                },
            },
        },

        // Who’s on First
        [typeof(SWhosOnFirst)] = new()
        {
            ModuleName = "表比較",
            Questions = new()
            {
                [SWhosOnFirst.Display] = new()
                {
                    // English: What was the display in the {1} stage on {0}?
                    // Example: What was the display in the first stage on Who’s on First?
                    Question = "{0}にてステージ{1}で表示されたのは？",
                },
            },
        },

        // Who’s on Gas
        [typeof(SWhosOnGas)] = new()
        {
            ModuleName = "ガス比較",
            Questions = new()
            {
                [SWhosOnGas.Display] = new()
                {
                    // English: What was the display in the first phase of the {1} stage on {0}?
                    // Example: What was the display in the first phase of the first stage on Who’s on Gas?
                    Question = "{0}にてステージ{1}の最初のフェーズで表示されたのは？",
                },
            },
        },

        // Who’s on Morse
        [typeof(SWhosOnMorse)] = new()
        {
            ModuleName = "モールス比較",
            Questions = new()
            {
                [SWhosOnMorse.TransmitDisplay] = new()
                {
                    // English: What word was transmitted in the {1} stage on {0}?
                    // Example: What word was transmitted in the first stage on Who’s on Morse?
                    Question = "{0}にてステージ{1}で送信された単語は？",
                },
            },
        },

        // The Wire
        [typeof(SWire)] = new()
        {
            ModuleName = "ザ・ワイヤ",
            Questions = new()
            {
                [SWire.DialColors] = new()
                {
                    // English: What was the color of the {1} dial in {0}?
                    // Example: What was the color of the top dial in The Wire?
                    Question = "{0}の{1}の位置にあったダイヤルの色は？",
                    Arguments = new()
                    {
                        ["top"] = "上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                    Answers = new()
                    {
                        ["blue"] = "青",
                        ["green"] = "緑",
                        ["grey"] = "灰",
                        ["orange"] = "オレンジ",
                        ["purple"] = "紫",
                        ["red"] = "赤",
                    },
                },
                [SWire.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "{0}で表示された数字は？",
                },
            },
        },

        // Wire Ordering
        [typeof(SWireOrdering)] = new()
        {
            ModuleName = "順序ワイヤ",
            Questions = new()
            {
                [SWireOrdering.DisplayColor] = new()
                {
                    // English: What color was the {1} display from the left in {0}?
                    // Example: What color was the first display from the left in Wire Ordering?
                    Question = "{0}の左から{1}番目のディスプレーの色は？",
                    Answers = new()
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
                [SWireOrdering.DisplayNumber] = new()
                {
                    // English: What number was on the {1} display from the left in {0}?
                    // Example: What number was on the first display from the left in Wire Ordering?
                    Question = "{0}の左から{1}番目のディスプレーの数字は？",
                },
                [SWireOrdering.WireColor] = new()
                {
                    // English: What color was the {1} wire from the left in {0}?
                    // Example: What color was the first wire from the left in Wire Ordering?
                    Question = "{0}の左から{1}番目のワイヤの色は？",
                    Answers = new()
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
            },
        },

        // Wire Sequence
        [typeof(SWireSequence)] = new()
        {
            ModuleName = "順番ワイヤ",
            Questions = new()
            {
                [SWireSequence.ColorCount] = new()
                {
                    // English: How many {1} wires were there in {0}?
                    // Example: How many red wires were there in Wire Sequence?
                    Question = "{0}の{1}色のワイヤの総数は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["blue"] = "青",
                        ["black"] = "黒",
                    },
                },
            },
        },

        // Wolf, Goat, and Cabbage
        [typeof(SWolfGoatAndCabbage)] = new()
        {
            ModuleName = "川渡り問題",
            Questions = new()
            {
                [SWolfGoatAndCabbage.Animals] = new()
                {
                    // English: Which of these was {1} on {0}?
                    // Example: Which of these was present on Wolf, Goat, and Cabbage?
                    Question = "{0}に{1}のはどれ？",
                    Arguments = new()
                    {
                        ["present"] = "存在した",
                        ["not present"] = "存在しなかった",
                    },
                },
                [SWolfGoatAndCabbage.BoatSize] = new()
                {
                    // English: What was the boat size in {0}?
                    Question = "{0}のボートのサイズは？",
                },
            },
        },

        // Word Count
        [typeof(SWordCount)] = new()
        {
            ModuleName = "単語カウント",
            Questions = new()
            {
                [SWordCount.Number] = new()
                {
                    // English: What was the displayed number on {0}?
                    Question = "{0}で表示された数字は？",
                },
            },
        },

        // Working Title
        [typeof(SWorkingTitle)] = new()
        {
            ModuleName = "ワーキングタイトル",
            Questions = new()
            {
                [SWorkingTitle.Display] = new()
                {
                    // English: What was on the display in {0}?
                    Question = "{0}にて表示されたラベルは？",
                },
            },
        },

        // Wumbo
        [typeof(SWumbo)] = new()
        {
            ModuleName = "ウンボ",
            Questions = new()
            {
                [SWumbo.Number] = new()
                {
                    // English: What was the number in {0}?
                    Question = "{0}の数字は？",
                },
            },
        },

        // The Xenocryst
        [typeof(SXenocryst)] = new()
        {
            ModuleName = "ゼノクリスト",
            Questions = new()
            {
                [SXenocryst.Question] = new()
                {
                    // English: What was the color of the {1} flash in {0}?
                    // Example: What was the color of the first flash in The Xenocryst?
                    Question = "{0}の{1}番目の点滅の色は？",
                },
            },
        },

        // XmORse Code
        [typeof(SXmORseCode)] = new()
        {
            ModuleName = "Xモールス信号",
            Questions = new()
            {
                [SXmORseCode.Word] = new()
                {
                    // English: What word did you decrypt in {0}?
                    Question = "{0}で解読した単語は？",
                },
                [SXmORseCode.DisplayedLetters] = new()
                {
                    // English: What was the {1} displayed letter (in reading order) in {0}?
                    // Example: What was the first displayed letter (in reading order) in XmORse Code?
                    Question = "{0}で表示された単語の{1}番目の位置(読み順)にある英字は？",
                },
            },
        },

        // xobekuJ ehT
        [typeof(SXobekuJehT)] = new()
        {
            ModuleName = "スクッボクーュジ",
            Questions = new()
            {
                [SXobekuJehT.Song] = new()
                {
                    // English: What song was played on {0}?
                    Question = "{0}で再生された曲は？",
                },
            },
        },

        // X-Ring
        [typeof(SXRing)] = new()
        {
            ModuleName = "円形レントゲン",
            Questions = new()
            {
                [SXRing.Symbol] = new()
                {
                    // English: Which symbol was scanned in {0}?
                    Question = "{0}で読み取られたシンボルは？",
                },
            },
        },

        // X-Rotor
        [typeof(SXRotor)] = new()
        {
            ModuleName = "回転レントゲン",
            Questions = new()
            {
                [SXRotor.Symbol] = new()
                {
                    // English: Which symbol was scanned in {0}?
                    Question = "{0}で読み取られたシンボルは？",
                },
            },
        },

        // XY-Ray
        [typeof(SXYRay)] = new()
        {
            ModuleName = "XYレントゲン",
            Questions = new()
            {
                [SXYRay.Shapes] = new()
                {
                    // English: Which shape was scanned by {0}?
                    Question = "{0}で読み取った図形は？",
                },
            },
        },

        // Yahtzee
        [typeof(SYahtzee)] = new()
        {
            ModuleName = "ヤッツィー",
            Questions = new()
            {
                [SYahtzee.InitialRoll] = new()
                {
                    // English: What was the initial roll on {0}?
                    Question = "{0}の最初のロール後の状態は？",
                    Answers = new()
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
            },
        },

        // Yellow Arrows
        [typeof(SYellowArrows)] = new()
        {
            ModuleName = "黄色矢印",
            Questions = new()
            {
                [SYellowArrows.StartingRow] = new()
                {
                    // English: What was the starting row letter in {0}?
                    Question = "{0}の開始行の英字は？",
                },
            },
        },

        // The Yellow Button
        [typeof(SYellowButton)] = new()
        {
            ModuleName = "黄色ボタン",
            Questions = new()
            {
                [SYellowButton.Colors] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in The Yellow Button?
                    Question = "{0}の{1}番目の色は？",
                    Answers = new()
                    {
                        ["Red"] = "赤",
                        ["Yellow"] = "黄色",
                        ["Green"] = ",緑",
                        ["Cyan"] = "シアン",
                        ["Blue"] = "青",
                        ["Magenta"] = "マゼンタ",
                    },
                },
            },
        },

        // Yellow Button’t
        [typeof(SYellowButtont)] = new()
        {
            ModuleName = "偽黄色ボタン",
            Questions = new()
            {
                [SYellowButtont.Filename] = new()
                {
                    // English: What was the filename in {0}?
                    Question = "{0}のファイル名は？",
                },
            },
        },

        // Yellow Cipher
        [typeof(SYellowCipher)] = new()
        {
            ModuleName = "黄色暗号",
            Questions = new()
            {
                [SYellowCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Yellow Cipher?
                    Question = "{0}の回答は？",
                    Arguments = new()
                    {
                        ["top"] = "上部",
                        ["middle"] = "中央",
                        ["bottom"] = "下部",
                    },
                },
            },
        },

        // Zero, Zero
        [typeof(SZeroZero)] = new()
        {
            ModuleName = "ゼロ・ゼロ",
            Questions = new()
            {
                [SZeroZero.Squares] = new()
                {
                    // English: Where was the {1} square in {0}?
                    // Example: Where was the red square in Zero, Zero?
                    Question = "{0}の{1}色の正方形の場所は？",
                    Arguments = new()
                    {
                        ["red"] = "赤",
                        ["green"] = "緑",
                        ["blue"] = "青",
                    },
                },
                [SZeroZero.StarColors] = new()
                {
                    // English: What color was the {1} star in {0}?
                    // Example: What color was the top-left star in Zero, Zero?
                    Question = "{0}の{1}の位置の星の色は？",
                    Arguments = new()
                    {
                        ["top-left"] = "左上",
                        ["top-right"] = "右上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                    Answers = new()
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
                [SZeroZero.StarPoints] = new()
                {
                    // English: How many points were on the {1} star in {0}?
                    // Example: How many points were on the top-left star in Zero, Zero?
                    Question = "{0}の{1}の位置の星の頂点数は？",
                    Arguments = new()
                    {
                        ["top-left"] = "左上",
                        ["top-right"] = "右上",
                        ["bottom-left"] = "左下",
                        ["bottom-right"] = "右下",
                    },
                },
            },
        },

        // Zoni
        [typeof(SZoni)] = new()
        {
            ModuleName = "ゾニ文字",
            Questions = new()
            {
                [SZoni.Words] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the first word in Zoni?
                    Question = "{0}で{1}番目に解読した単語は？",
                },
            },
        },

        // Épelle-moi Ça
        [typeof(SÉpelleMoiÇa)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SÉpelleMoiÇa.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "{0}において綴りを尋ねられた単語は？",
                },
            },
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
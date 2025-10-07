using System;
using System.Collections.Generic;

namespace Souvenir;

public class Translation_de : TranslationBase<Translation_de.TranslationInfo_de>
{
    public sealed class TranslationInfo_de : TranslationInfo
    {
        public string ModuleNameDative;
        public Gender Gender = Gender.Neuter;
    }

    public enum Gender
    {
        Masculine,
        Feminine,
        Neuter,
        Plural
    }

    public override string FormatModuleName(SouvenirHandlerAttribute handler, bool addSolveCount, int numSolved) => addSolveCount
        ? (_translations.Get(handler.EnumType)?.Gender ?? Gender.Neuter) switch
        {
            Gender.Feminine => $"der als {ordinal(numSolved)}e gelösten {_translations.Get(handler.EnumType)?.ModuleNameDative ?? _translations.Get(handler.EnumType)?.ModuleName ?? handler.ModuleName}",
            Gender.Masculine => $"dem als {ordinal(numSolved)}en gelösten {_translations.Get(handler.EnumType)?.ModuleNameDative ?? _translations.Get(handler.EnumType)?.ModuleName ?? handler.ModuleName}",
            Gender.Neuter => $"dem als {ordinal(numSolved)}es gelösten {_translations.Get(handler.EnumType)?.ModuleNameDative ?? _translations.Get(handler.EnumType)?.ModuleName ?? handler.ModuleName}",
            _ => /* Plural */ $"den als {ordinal(numSolved)}e gelösten {_translations.Get(handler.EnumType)?.ModuleNameDative ?? _translations.Get(handler.EnumType)?.ModuleName ?? handler.ModuleName}",
        }
        : _translations.Get(handler.EnumType)?.ModuleName ?? handler.ModuleNameWithThe;

    public override string Ordinal(int number) => ordinal(number);
    private string ordinal(int num) => num < 0 ? $"({num})t" : num switch
    {
        1 => "erst",
        2 => "zweit",
        3 => "dritt",
        4 => "viert",
        5 => "fünft",
        6 => "sechst",
        7 => "siebt",
        8 => "acht",
        9 => "neunt",
        10 => "zehnt",
        11 => "elft",
        12 => "zwölft",
        _ => $"{num}t"
    };

    protected override Dictionary<Type, TranslationInfo_de> _translations => new()
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
                    Question = "Was war bei {0} die anfänglich angezeigte Zahl?",
                },
            },
        },

        // 1000 Words
        [typeof(S1000Words)] = new()
        {
            ModuleName = "1000 Wörter",
            ModuleNameDative = "1000 Wörtern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [S1000Words.Words] = new()
                {
                    // English: What was the {1} word shown in {0}?
                    // Example: What was the first word shown in 1000 Words?
                    Question = "Was war bei {0} das {1}e angezeigte Wort?",
                },
            },
            Discriminators = new()
            {
                [S1000Words.Discriminator] = new()
                {
                    // English: the 1000 Words where the {0} word was {1}
                    // Example: the 1000 Words where the first word was Baken
                    Discriminator = "den 1000 Wörtern, bei denen das {0}e Wort {1} war,",
                },
            },
        },

        // 100 Levels of Defusal
        [typeof(S100LevelsOfDefusal)] = new()
        {
            ModuleName = "100 Ebenen der Entschärfung",
            Gender = Gender.Plural,
            Questions = new()
            {
                [S100LevelsOfDefusal.Letters] = new()
                {
                    // English: What was the {1} displayed letter in {0}?
                    // Example: What was the first displayed letter in 100 Levels of Defusal?
                    Question = "Was war bei {0} der {1}e angezeigte Buchstabe?",
                },
            },
            Discriminators = new()
            {
                [S100LevelsOfDefusal.Discriminator] = new()
                {
                    // English: the 100 Levels of Defusal where the {0} displayed letter was {1}
                    // Example: the 100 Levels of Defusal where the first displayed letter was B
                    Discriminator = "den 100 Ebenen der Entschärfung, deren {0}er Buchstabe {1} war,",
                },
            },
        },

        // The 1, 2, 3 Game
        [typeof(S123Game)] = new()
        {
            Questions = new()
            {
                [S123Game.Profile] = new()
                {
                    // English: Who was the opponent in {0}?
                    Question = "Wer war bei {0} der Gegner?",
                },
                [S123Game.Name] = new()
                {
                    // English: Who was the opponent in {0}?
                    Question = "Wer war bei {0} der Gegner?",
                },
            },
        },

        // 1D Chess
        [typeof(S1DChess)] = new()
        {
            ModuleName = "1D-Schach",
            Questions = new()
            {
                [S1DChess.Moves] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was your first move in 1D Chess?
                    Question = "Was war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["your first move"] = "dein erster Zug",
                        ["Rustmate’s first move"] = "Rustmates erster Zug",
                        ["your second move"] = "dein zweiter Zug",
                        ["Rustmate’s second move"] = "Rustmates zweiter Zug",
                        ["your third move"] = "dein dritter Zug",
                        ["Rustmate’s third move"] = "Rustmates dritter Zug",
                        ["your fourth move"] = "dein vierter Zug",
                        ["Rustmate’s fourth move"] = "Rustmates vierter Zug",
                        ["your fifth move"] = "dein fünfter Zug",
                        ["Rustmate’s fifth move"] = "Rustmates fünfter Zug",
                        ["your sixth move"] = "dein sechster Zug",
                        ["Rustmate’s sixth move"] = "Rustmates sechster Zug",
                        ["your seventh move"] = "dein siebter Zug",
                        ["Rustmate’s seventh move"] = "Rustmates siebter Zug",
                        ["your eighth move"] = "dein achter Zug",
                        ["Rustmate’s eighth move"] = "Rustmates achter Zug",
                    },
                },
            },
            Discriminators = new()
            {
                [S1DChess.Discriminator] = new()
                {
                    // English: the 1D Chess where {1} was {0}
                    // Example: the 1D Chess where your first move was B a→c
                    Discriminator = "dem 1D-Schach, bei dem {1} {0} war,",
                    Arguments = new()
                    {
                        ["your first move"] = "dein erster Zug",
                        ["Rustmate’s first move"] = "Rustmates erster Zug",
                        ["your second move"] = "dein zweiter Zug",
                        ["Rustmate’s second move"] = "Rustmates zweiter Zug",
                        ["your third move"] = "dein dritter Zug",
                        ["Rustmate’s third move"] = "Rustmates dritter Zug",
                        ["your fourth move"] = "dein vierter Zug",
                        ["Rustmate’s fourth move"] = "Rustmates vierter Zug",
                        ["your fifth move"] = "dein fünfter Zug",
                        ["Rustmate’s fifth move"] = "Rustmates fünfter Zug",
                        ["your sixth move"] = "dein sechster Zug",
                        ["Rustmate’s sixth move"] = "Rustmates sechster Zug",
                        ["your seventh move"] = "dein siebter Zug",
                        ["Rustmate’s seventh move"] = "Rustmates siebter Zug",
                        ["your eighth move"] = "dein achter Zug",
                        ["Rustmate’s eighth move"] = "Rustmates achter Zug",
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
                    Question = "Was war bei {0} die angezeigte Zahl?",
                },
            },
        },

        // 3D Maze
        [typeof(S3DMaze)] = new()
        {
            ModuleName = "3D-Labyrinth",
            Questions = new()
            {
                [S3DMaze.QMarkings] = new()
                {
                    // English: What were the markings in {0}?
                    Question = "Was waren bei {0} die Markierungen?",
                },
                [S3DMaze.QBearing] = new()
                {
                    // English: What was the cardinal direction in {0}?
                    Question = "Was war bei {0} die Himmelsrichtung?",
                    Answers = new()
                    {
                        ["North"] = "Norden",
                        ["South"] = "Süden",
                        ["West"] = "Westen",
                        ["East"] = "Osten",
                    },
                },
            },
            Discriminators = new()
            {
                [S3DMaze.DMarkings] = new()
                {
                    // English: the 3D Maze whose markings were {0}
                    // Example: the 3D Maze whose markings were ABC
                    Discriminator = "dem 3D-Labyrinth, dessen Markierungen {0} waren,",
                },
                [S3DMaze.DBearing] = new()
                {
                    // English: the 3D Maze whose cardinal direction was {0}
                    // Example: the 3D Maze whose cardinal direction was North
                    Discriminator = "dem 3D-Labyrinth, dessen Himmelsrichtung {0} war,",
                    Arguments = new()
                    {
                        ["North"] = "Norden",
                        ["South"] = "Süden",
                        ["West"] = "Westen",
                        ["East"] = "Osten",
                    },
                },
            },
        },

        // 3D Tap Code
        [typeof(S3DTapCode)] = new()
        {
            ModuleName = "3D-Klopfzeichen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [S3DTapCode.Word] = new()
                {
                    // English: What was the received word in {0}?
                    Question = "Was war bei {0} das empfangene Wort?",
                },
            },
        },

        // 3D Tunnels
        [typeof(S3DTunnels)] = new()
        {
            ModuleName = "3D-Tunnel",
            ModuleNameDative = "3D-Tunneln",
            Gender = Gender.Plural,
            Questions = new()
            {
                [S3DTunnels.TargetNode] = new()
                {
                    // English: What was the {1} goal node in {0}?
                    // Example: What was the first goal node in 3D Tunnels?
                    Question = "Was war bei {0} der Zielpunkt?",
                },
            },
        },

        // 3 LEDs
        [typeof(S3LEDs)] = new()
        {
            Questions = new()
            {
                [S3LEDs.InitialState] = new()
                {
                    // English: What was the initial state of the LEDs in {0} (in reading order)?
                    Question = "Was war bei {0} der Anfangszustand in Lesereihenfolge?",
                    Answers = new()
                    {
                        ["off/off/off"] = "aus/aus/aus",
                        ["off/off/on"] = "aus/aus/an",
                        ["off/on/off"] = "aus/an/aus",
                        ["off/on/on"] = "aus/an/an",
                        ["on/off/off"] = "an/aus/aus",
                        ["on/off/on"] = "an/aus/an",
                        ["on/on/off"] = "an/an/aus",
                        ["on/on/on"] = "an/an/an",
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
                    Question = "Welche Zahl war bei {0} anfänglich zu sehen?",
                },
            },
        },

        // 4D Tunnels
        [typeof(S4DTunnels)] = new()
        {
            ModuleName = "4D-Tunnel",
            ModuleNameDative = "4D-Tunneln",
            Gender = Gender.Plural,
            Questions = new()
            {
                [S4DTunnels.TargetNode] = new()
                {
                    // English: What was the {1} goal node in {0}?
                    // Example: What was the first goal node in 4D Tunnels?
                    Question = "Was war bei {0} der Zielpunkt?",
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
                    Question = "Was war die bei {0} angezeigte Zahl?",
                },
            },
        },

        // 7
        [typeof(S7)] = new()
        {
            Gender = Gender.Feminine,
            Questions = new()
            {
                [S7.QInitialValues] = new()
                {
                    // English: What was the {1} channel’s initial value in {0}?
                    // Example: What was the red channel’s initial value in 7?
                    Question = "Was war bei {0} der Anfangswert im {1}?",
                    Arguments = new()
                    {
                        ["red"] = "Rotkanal",
                        ["green"] = "Grünkanal",
                        ["blue"] = "Blaukanal",
                    },
                },
                [S7.QLedColors] = new()
                {
                    // English: What LED color was shown in stage {1} of {0}?
                    // Example: What LED color was shown in stage 1 of 7?
                    Question = "Was war bei {0} die LED-Farbe in Stufe {1}?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["blue"] = "blau",
                        ["green"] = "grün",
                        ["white"] = "weiß",
                    },
                },
            },
            Discriminators = new()
            {
                [S7.DInitialValues] = new()
                {
                    // English: the 7 whose {0} channel’s initial value was {1}
                    // Example: the 7 whose red channel’s initial value was -9
                    Discriminator = "der 7, deren {1} den Anfangswert {0} hatte,",
                    Arguments = new()
                    {
                        ["red"] = "Rotkanal",
                        ["green"] = "Grünkanal",
                        ["blue"] = "Blaukanal",
                    },
                },
                [S7.DLedColors] = new()
                {
                    // English: the 7 whose stage {0} LED color was {1}
                    // Example: the 7 whose stage 1 LED color was red
                    Discriminator = "der 7, deren LED in Stufe {0} {1} war,",
                    Arguments = new()
                    {
                        ["red"] = "rot",
                        ["blue"] = "blau",
                        ["green"] = "grün",
                        ["white"] = "weiß",
                    },
                },
            },
        },

        // 9-Ball
        [typeof(S9Ball)] = new()
        {
            Questions = new()
            {
                [S9Ball.Letters] = new()
                {
                    // English: What was the number of ball {1} in {0}?
                    // Example: What was the number of ball A in 9-Ball?
                    Question = "Welche Zahl hatte bei {0} die Kugel {1}?",
                },
                [S9Ball.Numbers] = new()
                {
                    // English: What was the letter of ball {1} in {0}?
                    // Example: What was the letter of ball 2 in 9-Ball?
                    Question = "Welchen Buchstaben hatte bei {0} die Kugel {1}?",
                },
            },
        },

        // Abyss
        [typeof(SAbyss)] = new()
        {
            Questions = new()
            {
                [SAbyss.Seed] = new()
                {
                    // English: What was the {1} character displayed on {0}?
                    // Example: What was the first character displayed on Abyss?
                    Question = "Welcher Buchstabe wurde bei {0} als {1}es angezeigt?",
                },
            },
        },

        // Accumulation
        [typeof(SAccumulation)] = new()
        {
            ModuleName = "Akkumulator",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SAccumulation.QBorderColor] = new()
                {
                    // English: What was the border color in {0}?
                    Question = "Was war bei {0} die Rahmenfarbe?",
                    Answers = new()
                    {
                        ["Blue"] = "Blau",
                        ["Brown"] = "Braun",
                        ["Green"] = "Grün",
                        ["Grey"] = "Grau",
                        ["Lime"] = "Limette",
                        ["Orange"] = "Orange",
                        ["Pink"] = "Pink",
                        ["Red"] = "Rot",
                        ["White"] = "Weiß",
                        ["Yellow"] = "Gelb",
                    },
                },
                [SAccumulation.QBackgroundColor] = new()
                {
                    // English: What was the background color in the {1} stage in {0}?
                    // Example: What was the background color in the first stage in Accumulation?
                    Question = "Was war bei {0} die Hintergrundfarbe im {1}en Schritt?",
                    Answers = new()
                    {
                        ["Blue"] = "Blau",
                        ["Brown"] = "Braun",
                        ["Green"] = "Grün",
                        ["Grey"] = "Grau",
                        ["Lime"] = "Limette",
                        ["Orange"] = "Orange",
                        ["Pink"] = "Pink",
                        ["Red"] = "Rot",
                        ["White"] = "Weiß",
                        ["Yellow"] = "Gelb",
                    },
                },
            },
            Discriminators = new()
            {
                [SAccumulation.DBorderColor] = new()
                {
                    // English: the Accumulation whose border was {0}
                    // Example: the Accumulation whose border was blue
                    Discriminator = "der Akkumulator, dessen Rahmen {0} war",
                    Arguments = new()
                    {
                        ["blue"] = "blau",
                        ["brown"] = "braun",
                        ["green"] = "grün",
                        ["grey"] = "grau",
                        ["lime"] = "limette",
                        ["orange"] = "orange",
                        ["pink"] = "pink",
                        ["red"] = "rot",
                        ["white"] = "weiß",
                        ["yellow"] = "gelb",
                    },
                },
                [SAccumulation.DBackgroundColor] = new()
                {
                    // English: the Accumulation whose background in the {1} stage was {0}
                    // Example: the Accumulation whose background in the first stage was blue
                    Discriminator = "der Akkumulator, dessen Hintergrund im {1}en Schritt {0} war",
                    Arguments = new()
                    {
                        ["blue"] = "blau",
                        ["brown"] = "braun",
                        ["green"] = "grün",
                        ["grey"] = "grau",
                        ["lime"] = "limette",
                        ["orange"] = "orange",
                        ["pink"] = "pink",
                        ["red"] = "rot",
                        ["white"] = "weiß",
                        ["yellow"] = "gelb",
                    },
                },
            },
        },

        // Adventure Game
        [typeof(SAdventureGame)] = new()
        {
            Questions = new()
            {
                [SAdventureGame.QCorrectItem] = new()
                {
                    // English: Which item was the {1} correct item you used in {0}?
                    // Example: Which item was the first correct item you used in Adventure Game?
                    Question = "Welches Objekt wurde bei {0} als {1}es korrekt verwendet?",
                },
                [SAdventureGame.QEnemy] = new()
                {
                    // English: What enemy were you fighting in {0}?
                    Question = "Welcher Gegner wurde bei {0} bekämpft?",
                },
            },
            Discriminators = new()
            {
                [SAdventureGame.DCorrectItem] = new()
                {
                    // English: the Adventure Game where the {0} was used
                    // Example: the Adventure Game where the Broadsword was used
                    Discriminator = "dem Adventure Game, bei dem {0} verwendet wurde,",
                },
                [SAdventureGame.DEnemy] = new()
                {
                    // English: the Adventure Game where the enemy was {0}
                    // Example: the Adventure Game where the enemy was Dragon
                    Discriminator = "dem Adventure Game mit {0} als Gegner",
                },
            },
        },

        // Affine Cycle
        [typeof(SAffineCycle)] = new()
        {
            ModuleName = "Affine Schiffer",
            ModuleNameDative = "Affinen Schiffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SAffineCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Affine Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}e Zeiger?",
                },
                [SAffineCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Affine Cycle?
                    Question = "Welcher Buchstabe stand bei {0} auf dem {1}en Zeiger?",
                },
            },
            Discriminators = new()
            {
                [SAffineCycle.LabelDiscriminator] = new()
                {
                    // English: the Affine Cycle that had the letter {0} on a dial
                    // Example: the Affine Cycle that had the letter A on a dial
                    Discriminator = "der Affinen Schiffer, bei der der Buchstabe {0} vorkam,",
                },
            },
        },

        // Alcoholic Rampage
        [typeof(SAlcoholicRampage)] = new()
        {
            ModuleName = "Amoksauf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SAlcoholicRampage.Mercenaries] = new()
                {
                    // English: Who was the {1} mercenary displayed in {0}?
                    // Example: Who was the first mercenary displayed in Alcoholic Rampage?
                    Question = "Welcher Söldner wurde bei {0} als {1}er angezeigt?",
                },
            },
        },

        // A Letter
        [typeof(SALetter)] = new()
        {
            ModuleName = "A Buachstob",
            Questions = new()
            {
                [SALetter.InitialLetter] = new()
                {
                    // English: What was the initial letter in {0}?
                    Question = "Woas woar bei {0} da Anfangsbuachstob?",
                },
            },
        },

        // Alfa-Bravo
        [typeof(SAlfaBravo)] = new()
        {
            Questions = new()
            {
                [SAlfaBravo.PressedLetter] = new()
                {
                    // English: Which letter was pressed in {0}?
                    Question = "Welcher Buchstabe wurde bei {0} eingegeben?",
                },
                [SAlfaBravo.LeftPressedLetter] = new()
                {
                    // English: Which letter was to the left of the pressed one in {0}?
                    Question = "Welcher Buchstabe war bei {0} links vom eingegebenen?",
                },
                [SAlfaBravo.RightPressedLetter] = new()
                {
                    // English: Which letter was to the right of the pressed one in {0}?
                    Question = "Welcher Buchstabe war bei {0} rechts vom eingegebenen?",
                },
                [SAlfaBravo.Digit] = new()
                {
                    // English: What was the last digit on the small display in {0}?
                    Question = "Was war bei {0} die letzte Ziffer in der kleinen Anzeige?",
                },
            },
        },

        // Algebra
        [typeof(SAlgebra)] = new()
        {
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SAlgebra.Equation1] = new()
                {
                    // English: What was the first equation in {0}?
                    Question = "Was war bei {0} die erste Gleichung?",
                },
                [SAlgebra.Equation2] = new()
                {
                    // English: What was the second equation in {0}?
                    Question = "Was war bei {0} die zweite Gleichung?",
                },
            },
            Discriminators = new()
            {
                [SAlgebra.Discriminator1] = new()
                {
                    // English: the Algebra where the first equation was {0}
                    // Example: the Algebra where the first equation was a=3z
                    Discriminator = "der Algebra, deren erste Gleichung {0} war,",
                },
                [SAlgebra.Discriminator2] = new()
                {
                    // English: the Algebra where the second equation was {0}
                    // Example: the Algebra where the second equation was b=(2x/10)-y
                    Discriminator = "der Algebra, deren zweite Gleichung {0} war,",
                },
            },
        },

        // Algorithmia
        [typeof(SAlgorithmia)] = new()
        {
            Questions = new()
            {
                [SAlgorithmia.QPositions] = new()
                {
                    // English: Which position was the {1} position in {0}?
                    // Example: Which position was the starting position in Algorithmia?
                    Question = "Was war bei {0} die {1}?",
                    Arguments = new()
                    {
                        ["starting"] = "Anfangsposition",
                        ["goal"] = "Zielposition",
                    },
                },
                [SAlgorithmia.QColor] = new()
                {
                    // English: What was the color of the colored bulb in {0}?
                    Question = "Welche Farbe hatte die gefärbte Glühlampe bei {0}?",
                },
                [SAlgorithmia.QSeed] = new()
                {
                    // English: Which number was present in the seed in {0}?
                    Question = "Welche Zahl war bei {0} unter den Startwerten enthalten?",
                },
            },
            Discriminators = new()
            {
                [SAlgorithmia.DPositions] = new()
                {
                    // English: the Algorithmia where this was the {0} position
                    // Example: the Algorithmia where this was the starting position
                    Discriminator = "dem Algorithmia, bei dem {0} hier war,",
                    Arguments = new()
                    {
                        ["starting"] = "die Startposition",
                        ["goal"] = "das Ziel",
                    },
                },
                [SAlgorithmia.DColor] = new()
                {
                    // English: the Algorithmia whose colored bulb was {0}
                    // Example: the Algorithmia whose colored bulb was red
                    Discriminator = "dem Algorithmia, dessen farbige Glühlampe {0} war,",
                    Arguments = new()
                    {
                        ["red"] = "rot",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["cyan"] = "türkis",
                        ["yellow"] = "gelb",
                        ["magenta"] = "magenta",
                    },
                },
                [SAlgorithmia.DSeed] = new()
                {
                    // English: the Algorithmia that had a {0} in the seed
                    // Example: the Algorithmia that had a 01 in the seed
                    Discriminator = "dem Algorithmia, bei dessen Startwerten {0} vorkam,",
                },
            },
        },

        // Alphabetical Ruling
        [typeof(SAlphabeticalRuling)] = new()
        {
            Questions = new()
            {
                [SAlphabeticalRuling.Letter] = new()
                {
                    // English: What was the letter displayed in the {1} stage of {0}?
                    // Example: What was the letter displayed in the first stage of Alphabetical Ruling?
                    Question = "Welcher Buchstabe wurde bei {0} im {1}en Schritt angezeigt?",
                },
                [SAlphabeticalRuling.Number] = new()
                {
                    // English: What was the number displayed in the {1} stage of {0}?
                    // Example: What was the number displayed in the first stage of Alphabetical Ruling?
                    Question = "Welche Zahl wurde bei {0} im {1}en Schritt angezeigt?",
                },
            },
        },

        // Alphabet Numbers
        [typeof(SAlphabetNumbers)] = new()
        {
            Questions = new()
            {
                [SAlphabetNumbers.DisplayedNumbers] = new()
                {
                    // English: Which of these numbers was on one of the buttons in the {1} stage of {0}?
                    // Example: Which of these numbers was on one of the buttons in the first stage of Alphabet Numbers?
                    Question = "Welche Zahl war auf einem der Knöpfe im {1}en Schritt von {0} zu sehen?",
                },
            },
        },

        // Alphabet Tiles
        [typeof(SAlphabetTiles)] = new()
        {
            ModuleName = "Alphabet-Kacheln",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SAlphabetTiles.QCycle] = new()
                {
                    // English: What was the {1} letter shown during the cycle in {0}?
                    // Example: What was the first letter shown during the cycle in Alphabet Tiles?
                    Question = "Welcher Buchstabe war im Zyklus bei {0} als {1}es zu sehen?",
                },
                [SAlphabetTiles.QMissingLetter] = new()
                {
                    // English: What was the missing letter in {0}?
                    Question = "Was war bei {0} der fehlende Buchstabe?",
                },
            },
            Discriminators = new()
            {
                [SAlphabetTiles.DCycle] = new()
                {
                    // English: the Alphabet Tiles where the {1} letter in the cycle was {0}
                    // Example: the Alphabet Tiles where the X letter in the cycle was first
                    Discriminator = "den Alphabet-Kacheln, deren {1}er Buchstabe im Zyklus {0} war,",
                },
                [SAlphabetTiles.DMissingLetter] = new()
                {
                    // English: the Alphabet Tiles whose missing letter was {0}
                    // Example: the Alphabet Tiles whose missing letter was A
                    Discriminator = "den Alphabet-Kacheln, deren fehlender Buchstabe {0} war,",
                },
            },
        },

        // Alpha-Bits
        [typeof(SAlphaBits)] = new()
        {
            Questions = new()
            {
                [SAlphaBits.DisplayedCharacters] = new()
                {
                    // English: What character was displayed on the {1} screen on the {2} in {0}?
                    // Example: What character was displayed on the first screen on the left in Alpha-Bits?
                    Question = "Welches Zeichen wurde bei {0} im {1}en {2} Display angezeigt?",
                    Arguments = new()
                    {
                        ["left"] = "linken",
                        ["right"] = "rechten",
                    },
                },
            },
        },

        // A Message
        [typeof(SAMessage)] = new()
        {
            ModuleName = "A Nachricht",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SAMessage.AMessage] = new()
                {
                    // English: What was the initial message in {0}?
                    Question = "Was war bei {0} die Anfangsnachricht?",
                },
            },
        },

        // Amusement Parks
        [typeof(SAmusementParks)] = new()
        {
            ModuleName = "Freizeitparks",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SAmusementParks.Rides] = new()
                {
                    // English: Which ride was available in {0}?
                    Question = "Welche Attraktion wurde bei {0} angeboten?",
                },
            },
        },

        // Ángel Hernández
        [typeof(SAngelHernandez)] = new()
        {
            Questions = new()
            {
                [SAngelHernandez.MainLetter] = new()
                {
                    // English: What letter was shown by the raised buttons on the {1} stage on {0}?
                    // Example: What letter was shown by the raised buttons on the first stage on Ángel Hernández?
                    Question = "Welcher Buchstabe wurde im {1}en Schritt von {0} durch die erhöhten Knöpfe dargestellt?",
                },
            },
        },

        // The Arena
        [typeof(SArena)] = new()
        {
            ModuleName = "Die Arena",
            ModuleNameDative = "Arena",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SArena.Damage] = new()
                {
                    // English: What was the maximum weapon damage of the attack phase in {0}?
                    Question = "Was war bei {0} in der Angriffsphase der maximale Waffenschaden?",
                },
                [SArena.Enemies] = new()
                {
                    // English: Which enemy was present in the defend phase of {0}?
                    Question = "Welcher Gegner war bei {0} in der Verteidigungsphase anwesend?",
                },
                [SArena.Numbers] = new()
                {
                    // English: Which was a number present in the grab phase of {0}?
                    Question = "Welche Zahl war bei {0} in der Sammelphase dabei?",
                },
            },
        },

        // Arithmelogic
        [typeof(SArithmelogic)] = new()
        {
            ModuleName = "Arithmologik",
            Questions = new()
            {
                [SArithmelogic.Submit] = new()
                {
                    // English: What was the symbol on the submit button in {0}?
                    Question = "Welches Symbol war bei {0} auf dem Eingabeknopf?",
                },
                [SArithmelogic.Numbers] = new()
                {
                    // English: Which number was selectable, but not the solution, in the {1} screen on {0}?
                    // Example: Which number was selectable, but not the solution, in the left screen on Arithmelogic?
                    Question = "Welche Zahl war bei {0} im {1} Bildschirm auswählbar, aber nicht die Lösung?",
                    Arguments = new()
                    {
                        ["left"] = "linken",
                        ["middle"] = "mittleren",
                        ["right"] = "rechten",
                    },
                },
            },
        },

        // ASCII Maze
        [typeof(SASCIIMaze)] = new()
        {
            ModuleName = "ASCII-Labyrinth",
            Questions = new()
            {
                [SASCIIMaze.Characters] = new()
                {
                    // English: What was the {1} character displayed on {0}?
                    // Example: What was the first character displayed on ASCII Maze?
                    Question = "Was war bei {0} das {1}e angezeigte Zeichen?",
                },
            },
        },

        // A Square
        [typeof(SASquare)] = new()
        {
            ModuleName = "A Quadrat",
            Questions = new()
            {
                [SASquare.IndexColors] = new()
                {
                    // English: Which of these was an index color in {0}?
                    Question = "Wia war’s bei {0} mid da Indexfoab?",
                },
                [SASquare.CorrectColors] = new()
                {
                    // English: Which color was submitted {1} in {0}?
                    // Example: Which color was submitted first in A Square?
                    Question = "Wos fia a Foab hodn’s bei {0} z’{1} eigegebn?",
                },
            },
        },

        // Audio Morse
        [typeof(SAudioMorse)] = new()
        {
            ModuleName = "Audio-Morse",
            Questions = new()
            {
                [SAudioMorse.Sound] = new()
                {
                    // English: What was signaled in {0}?
                    Question = "Was war bei {0} zu hören?",
                },
            },
        },

        // The Azure Button
        [typeof(SAzureButton)] = new()
        {
            ModuleName = "Der Azurfarbene Knopf",
            ModuleNameDative = "Azurfarbenen Knopf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SAzureButton.QDecoyArrowDirection] = new()
                {
                    // English: What was the {1} direction in the decoy arrow in {0}?
                    // Example: What was the first direction in the decoy arrow in The Azure Button?
                    Question = "Was war bei {0} die {1}e Richtung im ungenutzten Pfeil?",
                },
                [SAzureButton.QNonDecoyArrowDirection] = new()
                {
                    // English: What was the {1} direction in the {2} non-decoy arrow in {0}?
                    // Example: What was the first direction in the first non-decoy arrow in The Azure Button?
                    Question = "Was war bei {0} die {1}e Richtung im {2}en genutzten Pfeil?",
                },
                [SAzureButton.QT] = new()
                {
                    // English: What was T in {0}?
                    Question = "Was war bei {0} T?",
                },
                [SAzureButton.QNotT] = new()
                {
                    // English: Which of these cards was shown in Stage 1, but not T, in {0}?
                    Question = "Welche Karte war bei {0} in Schritt 1 zu sehen, aber nicht T?",
                },
                [SAzureButton.QM] = new()
                {
                    // English: What was M in {0}?
                    Question = "Was war bei {0} M?",
                },
            },
            Discriminators = new()
            {
                [SAzureButton.DCard] = new()
                {
                    // English: the Azure Button that had this card in Stage 1
                    Discriminator = "dem Azurfarbenen Knopf, bei dem in Schritt 1 diese Karte vorkam,",
                },
                [SAzureButton.DM] = new()
                {
                    // English: the Azure Button where M was {0}
                    // Example: the Azure Button where M was 1
                    Discriminator = "dem Azurfarbenen Knopf, bei dem M {0} war,",
                },
                [SAzureButton.DDecoyArrowDirection] = new()
                {
                    // English: the Azure Button where the decoy arrow went {0} at some point
                    // Example: the Azure Button where the decoy arrow went north at some point
                    Discriminator = "dem Azurfarbenen Knopf, dessen ungenutzter Pfeil einmal nach {0} ging",
                    Arguments = new()
                    {
                        ["north"] = "Norden",
                        ["north-east"] = "Nordosten",
                        ["east"] = "Osten",
                        ["south-east"] = "Südosten",
                        ["south"] = "Süden",
                        ["south-west"] = "Südwesten",
                        ["west"] = "Westen",
                        ["north-west"] = "Nordwesten",
                    },
                },
                [SAzureButton.DNonDecoyArrowDirection] = new()
                {
                    // English: the Azure Button where the {1} non-decoy arrow went {0} at some point
                    // Example: the Azure Button where the first non-decoy arrow went north at some point
                    Discriminator = "dem Azurfarbenen Knopf, dessen {1}er genutzter Pfeil einmal nach {0} ging",
                    Arguments = new()
                    {
                        ["north"] = "Norden",
                        ["north-east"] = "Nordosten",
                        ["east"] = "Osten",
                        ["south-east"] = "Südosten",
                        ["south"] = "Süden",
                        ["south-west"] = "Südwesten",
                        ["west"] = "Westen",
                        ["north-west"] = "Nordwesten",
                    },
                },
            },
        },

        // Bakery
        [typeof(SBakery)] = new()
        {
            ModuleName = "Bäckerei",
            Questions = new()
            {
                [SBakery.Items] = new()
                {
                    // English: Which menu item was present in {0}?
                    Question = "Was stand bei {0} auf dem Menü angeboten?",
                },
            },
        },

        // Bamboozled Again
        [typeof(SBamboozledAgain)] = new()
        {
            ModuleName = "Wieder Übers Ohr Gehauen",
            Questions = new()
            {
                [SBamboozledAgain.ButtonText] = new()
                {
                    // English: What text was initially shown on this button in {0}? (+ sprite)
                    // Example: What text was initially shown on this button in Bamboozled Again? (+ sprite)
                    Question = "Welcher Text war bei {0} am Anfang auf diesem Knopf?",
                },
                [SBamboozledAgain.ButtonColor] = new()
                {
                    // English: What was the initial color of this button in {0}? (+ sprite)
                    // Example: What was the initial color of this button in Bamboozled Again? (+ sprite)
                    Question = "Welche Farbe hatte bei {0} am Anfang dieser Knopf?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Lime"] = "Limette",
                        ["Green"] = "Grün",
                        ["Jade"] = "Jade",
                        ["Cyan"] = "Türkis",
                        ["Azure"] = "Azur",
                        ["Blue"] = "Blau",
                        ["Violet"] = "Violett",
                        ["Magenta"] = "Magenta",
                        ["Rose"] = "Rosa",
                        ["White"] = "Weiß",
                        ["Grey"] = "Grau",
                        ["Black"] = "Schwarz",
                    },
                },
                [SBamboozledAgain.DisplayTexts1] = new()
                {
                    // English: What was the {1} decrypted text on the display in {0}?
                    // Example: What was the first decrypted text on the display in Bamboozled Again?
                    Question = "Was war bei {0} der {1}e Text auf dem Display, aber entschlüsselt?",
                },
                [SBamboozledAgain.DisplayTexts2] = new()
                {
                    // English: What was the {1} decrypted text on the display in {0}?
                    // Example: What was the first decrypted text on the display in Bamboozled Again?
                    Question = "Was war bei {0} der {1}e Text auf dem Display, aber entschlüsselt?",
                },
                [SBamboozledAgain.DisplayColor] = new()
                {
                    // English: What color was the {1} text on the display in {0}?
                    // Example: What color was the first text on the display in Bamboozled Again?
                    Question = "In welcher Farbe wurde bei {0} der {1}e Text auf dem Display angezeigt?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Lime"] = "Limette",
                        ["Green"] = "Grün",
                        ["Jade"] = "Jade",
                        ["Cyan"] = "Türkis",
                        ["Azure"] = "Azur",
                        ["Blue"] = "Blau",
                        ["Violet"] = "Violett",
                        ["Magenta"] = "Magenta",
                        ["Rose"] = "Rosa",
                        ["White"] = "Weiß",
                        ["Grey"] = "Grau",
                    },
                },
            },
        },

        // Bamboozling Button
        [typeof(SBamboozlingButton)] = new()
        {
            ModuleName = "Wieder Übern Knopf Gehauen",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SBamboozlingButton.Color] = new()
                {
                    // English: What color was the button in the {1} stage of {0}?
                    // Example: What color was the button in the first stage of Bamboozling Button?
                    Question = "Welche Farbe hatte der Knopf bei {0} im {1}en Schritt?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Lime"] = "Limette",
                        ["Green"] = "Grün",
                        ["Jade"] = "Jade",
                        ["Cyan"] = "Türkis",
                        ["Azure"] = "Azur",
                        ["Blue"] = "Blau",
                        ["Violet"] = "Violett",
                        ["Magenta"] = "Magenta",
                        ["Rose"] = "Rosa",
                        ["White"] = "Weiß",
                        ["Grey"] = "Grau",
                        ["Black"] = "Schwarz",
                    },
                },
                [SBamboozlingButton.DisplayColor] = new()
                {
                    // English: What was the color of the {2} display in the {1} stage of {0}?
                    // Example: What was the color of the first display in the first stage of Bamboozling Button?
                    Question = "In welcher Farbe erschien bei {0} im {1}en Schritt die {2}e Anzeige auf dem Display?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Lime"] = "Limette",
                        ["Green"] = "Grün",
                        ["Jade"] = "Jade",
                        ["Cyan"] = "Türkis",
                        ["Azure"] = "Azur",
                        ["Blue"] = "Blau",
                        ["Violet"] = "Violett",
                        ["Magenta"] = "Magenta",
                        ["Rose"] = "Rosa",
                        ["White"] = "Weiß",
                        ["Grey"] = "Grau",
                    },
                },
                [SBamboozlingButton.Display] = new()
                {
                    // English: What was the {2} display in the {1} stage of {0}?
                    // Example: What was the first display in the first stage of Bamboozling Button?
                    Question = "Was war bei {0} im {1}en Schritt die {2}e Anzeige auf dem Display?",
                },
                [SBamboozlingButton.Label] = new()
                {
                    // English: What was the {2} label on the button in the {1} stage of {0}?
                    // Example: What was the top label on the button in the first stage of Bamboozling Button?
                    Question = "Was war bei {0} im {1}en Schritt die {2} Aufschrift auf dem Knopf?",
                    Arguments = new()
                    {
                        ["top"] = "obere",
                        ["bottom"] = "untere",
                    },
                },
            },
        },

        // Bar Charts
        [typeof(SBarCharts)] = new()
        {
            ModuleName = "Balkendiagramme",
            ModuleNameDative = "Balkendiagrammen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SBarCharts.Category] = new()
                {
                    // English: What was the category of {0}?
                    Question = "Was war bei {0} die Kategorie?",
                },
                [SBarCharts.Unit] = new()
                {
                    // English: What was the unit of {0}?
                    Question = "Was war bei {0} die Einheit?",
                },
                [SBarCharts.Label] = new()
                {
                    // English: What was the label of the {1} bar in {0}?
                    // Example: What was the label of the first bar in Bar Charts?
                    Question = "Was war bei {0} die Beschriftung des {1}en Balkens?",
                },
                [SBarCharts.Color] = new()
                {
                    // English: What was the color of the {1} bar in {0}?
                    // Example: What was the color of the first bar in Bar Charts?
                    Question = "Was war bei {0} die Farbe des {1}en Balkens?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                    },
                },
                [SBarCharts.Height] = new()
                {
                    // English: What was the position of the {1} bar in {0}?
                    // Example: What was the position of the shortest bar in Bar Charts?
                    Question = "An welcher Position war bei {0} der {1} Balken?",
                    Arguments = new()
                    {
                        ["shortest"] = "kürzeste",
                        ["second shortest"] = "zweitkürzeste",
                        ["second tallest"] = "zweithöchste",
                        ["tallest"] = "höchste",
                    },
                },
            },
        },

        // Barcode Cipher
        [typeof(SBarcodeCipher)] = new()
        {
            Questions = new()
            {
                [SBarcodeCipher.ScreenNumber] = new()
                {
                    // English: What was the screen number in {0}?
                    Question = "Welche Zahl war bei {0} auf dem Display?",
                },
                [SBarcodeCipher.BarcodeEdgework] = new()
                {
                    // English: What was the edgework represented by the {1} barcode in {0}?
                    // Example: What was the edgework represented by the first barcode in Barcode Cipher?
                    Question = "Was wurde bei {0} vom {1}en Barcode wiedergegeben?",
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
                    Question = "Was war bei {0} die Lösung für den {1}en Barcode?",
                },
            },
        },

        // Bartending
        [typeof(SBartending)] = new()
        {
            ModuleName = "Bartender",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SBartending.Ingredients] = new()
                {
                    // English: Which ingredient was in the {1} position on {0}?
                    // Example: Which ingredient was in the first position on Bartending?
                    Question = "Was war bei {0} die Zutat an {1}er Stelle?",
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
            ModuleName = "Bohnen",
            Questions = new()
            {
                [SBeans.Colors] = new()
                {
                    // English: What was this bean in {0}? (+ sprite)
                    Question = "Was war bei {0} diese Bohne?",
                    Answers = new()
                    {
                        ["Wobbly Orange"] = "Orange wackelnd",
                        ["Wobbly Yellow"] = "Gelb wackelnd",
                        ["Wobbly Green"] = "Grün wackelnd",
                        ["Not Wobbly Orange"] = "Orange nicht wackelnd",
                        ["Not Wobbly Yellow"] = "Gelb nicht wackelnd",
                        ["Not Wobbly Green"] = "Grün nicht wackelnd",
                    },
                },
            },
        },

        // Bean Sprouts
        [typeof(SBeanSprouts)] = new()
        {
            ModuleName = "Bohnensprossen",
            Questions = new()
            {
                [SBeanSprouts.Colors] = new()
                {
                    // English: What was sprout {1} in {0}?
                    // Example: What was sprout 1 in Bean Sprouts?
                    Question = "Was war bei {0} der Spross {1}?",
                    Answers = new()
                    {
                        ["Raw"] = "Roh",
                        ["Cooked"] = "Gekocht",
                        ["Burnt"] = "Angebrannt",
                        ["Fake"] = "Falsch",
                    },
                },
                [SBeanSprouts.Beans] = new()
                {
                    // English: What bean was on sprout {1} in {0}?
                    // Example: What bean was on sprout 1 in Bean Sprouts?
                    Question = "Welche Bohne war bei {0} auf Spross {1}?",
                    Answers = new()
                    {
                        ["Left"] = "Die linke",
                        ["Right"] = "Die rechte",
                        ["None"] = "Keine",
                        ["Both"] = "Beide",
                    },
                },
            },
        },

        // Big Bean
        [typeof(SBigBean)] = new()
        {
            ModuleName = "Großbohne",
            Questions = new()
            {
                [SBigBean.Color] = new()
                {
                    // English: What was the bean in {0}?
                    Question = "Was war bei {0} die Bohne?",
                    Answers = new()
                    {
                        ["Wobbly Orange"] = "Orange wackelnd",
                        ["Wobbly Yellow"] = "Gelb wackelnd",
                        ["Wobbly Green"] = "Grün wackelnd",
                        ["Not Wobbly Orange"] = "Orange nicht wackelnd",
                        ["Not Wobbly Yellow"] = "Gelb nicht wackelnd",
                        ["Not Wobbly Green"] = "Grün nicht wackelnd",
                    },
                },
            },
        },

        // Big Circle
        [typeof(SBigCircle)] = new()
        {
            ModuleName = "Großer Kreis",
            ModuleNameDative = "Großen Kreis",
            Questions = new()
            {
                [SBigCircle.Colors] = new()
                {
                    // English: What color was {1} in the solution to {0}?
                    // Example: What color was first in the solution to Big Circle?
                    Question = "Welche Farbe war bei {0} die {1}e Farbe in der Lösung?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Magenta"] = "Magenta",
                        ["White"] = "Weiß",
                        ["Black"] = "Schwarz",
                    },
                },
            },
        },

        // Binary
        [typeof(SBinary)] = new()
        {
            ModuleName = "Binär-LEDs",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SBinary.Word] = new()
                {
                    // English: What word was displayed in {0}?
                    Question = "Welches Wort wurde bei {0} angezeigt?",
                },
            },
        },

        // Binary LEDs
        [typeof(SBinaryLEDs)] = new()
        {
            ModuleName = "Binär-LEDs",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SBinaryLEDs.Value] = new()
                {
                    // English: At which numeric value did you cut the correct wire in {0}?
                    Question = "Bei welchem Zahlenwert wurde bei {0} der korrekte Draht durchtrennt?",
                },
            },
        },

        // Binary Shift
        [typeof(SBinaryShift)] = new()
        {
            Questions = new()
            {
                [SBinaryShift.InitialNumber] = new()
                {
                    // English: What was the {1} initial number in {0}?
                    // Example: What was the top-left initial number in Binary Shift?
                    Question = "Was war bei {0} die {1} Anfangszahl?",
                    Arguments = new()
                    {
                        ["top-left"] = "obere linke",
                        ["top-middle"] = "obere mittlere",
                        ["top-right"] = "obere rechte",
                        ["left-middle"] = "linke mittlere",
                        ["center"] = "mittlere",
                        ["right-middle"] = "rechte mittlere",
                        ["bottom-left"] = "untere linke",
                        ["bottom-middle"] = "untere mittlere",
                        ["bottom-right"] = "untere rechte",
                    },
                },
                [SBinaryShift.SelectedNumberPossition] = new()
                {
                    // English: What number was selected at stage {1} in {0}?
                    // Example: What number was selected at stage 0 in Binary Shift?
                    Question = "Welche Zahl wurde bei {0} in Schritt {1} ausgewählt?",
                    Answers = new()
                    {
                        ["top-left"] = "oben links",
                        ["top-middle"] = "oben Mitte",
                        ["top-right"] = "oben rechts",
                        ["left-middle"] = "Mitte links",
                        ["center"] = "Mitte Mitte",
                        ["right-middle"] = "Mitte rechts",
                        ["bottom-left"] = "unten links",
                        ["bottom-middle"] = "unten Mitte",
                        ["bottom-right"] = "unten rechts",
                    },
                },
                [SBinaryShift.NotSelectedNumberPossition] = new()
                {
                    // English: What number was not selected at stage {1} in {0}?
                    // Example: What number was not selected at stage 0 in Binary Shift?
                    Question = "Welche Zahl wurde bei {0} in Schritt {1} nicht ausgewählt?",
                    Answers = new()
                    {
                        ["top-left"] = "oben links",
                        ["top-middle"] = "oben Mitte",
                        ["top-right"] = "oben rechts",
                        ["left-middle"] = "Mitte links",
                        ["center"] = "Mitte Mitte",
                        ["right-middle"] = "Mitte rechts",
                        ["bottom-left"] = "unten links",
                        ["bottom-middle"] = "unten Mitte",
                        ["bottom-right"] = "unten rechts",
                    },
                },
            },
        },

        // Bitmaps
        [typeof(SBitmaps)] = new()
        {
            Questions = new()
            {
                [SBitmaps.Question] = new()
                {
                    // English: How many pixels were {1} in the {2} quadrant in {0}?
                    // Example: How many pixels were white in the top left quadrant in Bitmaps?
                    Question = "Wie viele Pixels waren bei {0} im {2} Quadranten {1}?",
                    Arguments = new()
                    {
                        ["white"] = "weiß",
                        ["black"] = "schwarz",
                        ["top left"] = "oberen linken",
                        ["top right"] = "oberen rechten",
                        ["bottom left"] = "unteren linken",
                        ["bottom right"] = "unteren rechten",
                    },
                },
            },
        },

        // Black Cipher
        [typeof(SBlackCipher)] = new()
        {
            ModuleName = "Schwarze Geheimschrift",
            ModuleNameDative = "Schwarzen Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SBlackCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Black Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["middle"] = "mittleren",
                        ["bottom"] = "unteren",
                    },
                },
            },
        },

        // Blindfolded Yahtzee
        [typeof(SBlindfoldedYahtzee)] = new()
        {
            ModuleName = "Blindes Kniffel",
            ModuleNameDative = "Blinden Kniffel",
            Questions = new()
            {
                [SBlindfoldedYahtzee.Claim] = new()
                {
                    // English: What roll did the module claim in the {1} stage of {0}?
                    // Example: What roll did the module claim in the first stage of Blindfolded Yahtzee?
                    Question = "Was hat das Modul bei {0} in der {1}en Stufe verzeichnet?",
                    Answers = new()
                    {
                        ["Yahtzee"] = "Kniffel (Fünfling)",
                        ["Large Straight"] = "Große Straße",
                        ["Small Straight"] = "Kleine Straße",
                        ["Full House"] = "Full-House",
                        ["Four of a Kind"] = "Vierling",
                        ["Chance"] = "Chance",
                        ["Three of a Kind"] = "Drilling",
                        ["1s"] = "Einsen",
                        ["2s"] = "Zweien",
                        ["3s"] = "Dreien",
                        ["4s"] = "Vieren",
                        ["5s"] = "Fünfen",
                        ["6s"] = "Sechsen",
                    },
                },
            },
        },

        // Blind Maze
        [typeof(SBlindMaze)] = new()
        {
            ModuleName = "Blinder Irrgarten",
            Questions = new()
            {
                [SBlindMaze.Colors] = new()
                {
                    // English: What color was the {1} button in {0}?
                    // Example: What color was the north button in Blind Maze?
                    Question = "Welche Farbe hatte der Knopf gen {1} bei {0}?",
                    Arguments = new()
                    {
                        ["north"] = "Norden",
                        ["east"] = "Osten",
                        ["west"] = "Westen",
                        ["south"] = "Süden",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Gray"] = "Grau",
                        ["Yellow"] = "Gelb",
                    },
                },
                [SBlindMaze.Maze] = new()
                {
                    // English: Which maze did you solve {0} on?
                    Question = "In welchem Labyrinth wurde {0} gelöst?",
                },
            },
        },

        // Blinking Notes
        [typeof(SBlinkingNotes)] = new()
        {
            ModuleName = "Blinkende Noten",
            ModuleNameDative = "Blinkenden Noten",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SBlinkingNotes.Song] = new()
                {
                    // English: What song was flashed in {0}?
                    Question = "Welcher Song blinkte bei {0} auf?",
                },
            },
        },

        // Blinkstop
        [typeof(SBlinkstop)] = new()
        {
            Questions = new()
            {
                [SBlinkstop.NumberOfFlashes] = new()
                {
                    // English: How many times did the LED flash in {0}?
                    Question = "Wie oft hat bei {0} die LED geblinkt?",
                },
                [SBlinkstop.FewestFlashedColor] = new()
                {
                    // English: Which color did the LED flash the fewest times in {0}?
                    Question = "Welche Farbe hat die LED bei {0} am wenigsten gezeigt?",
                    Answers = new()
                    {
                        ["Purple"] = "Lila",
                        ["Cyan"] = "Türkis",
                        ["Yellow"] = "Gelb",
                        ["Multicolor"] = "Mehrfarbig",
                    },
                },
            },
        },

        // Blockbusters
        [typeof(SBlockbusters)] = new()
        {
            Questions = new()
            {
                [SBlockbusters.LastLetter] = new()
                {
                    // English: What was the last letter pressed on {0}?
                    Question = "Welcher Buchstabe wurde bei {0} als letztes eingegeben?",
                },
            },
        },

        // Blue Arrows
        [typeof(SBlueArrows)] = new()
        {
            ModuleName = "Blaue Pfeile",
            Questions = new()
            {
                [SBlueArrows.InitialCharacters] = new()
                {
                    // English: What were the characters on the screen in {0}?
                    Question = "Welche Zeichen waren bei {0} auf dem Bildschirm?",
                },
            },
        },

        // The Blue Button
        [typeof(SBlueButton)] = new()
        {
            ModuleName = "Der Blaue Knopf",
            ModuleNameDative = "Blauen Knopf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SBlueButton.D] = new()
                {
                    // English: What was D in {0}?
                    Question = "Was war bei {0} D?",
                },
                [SBlueButton.EFGH] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was E in The Blue Button?
                    Question = "Was war bei {0} {1}?",
                },
                [SBlueButton.M] = new()
                {
                    // English: What was M in {0}?
                    Question = "Was war bei {0} M?",
                },
                [SBlueButton.N] = new()
                {
                    // English: What was N in {0}?
                    Question = "Was war bei {0} N?",
                },
                [SBlueButton.P] = new()
                {
                    // English: What was P in {0}?
                    Question = "Was war bei {0} P?",
                },
                [SBlueButton.Q] = new()
                {
                    // English: What was Q in {0}?
                    Question = "Was war bei {0} D?",
                    Answers = new()
                    {
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Cyan"] = "Türkis",
                        ["Red"] = "Rot",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                    },
                },
                [SBlueButton.X] = new()
                {
                    // English: What was X in {0}?
                    Question = "Was war bei {0} X?",
                },
            },
            Discriminators = new()
            {
                [SBlueButton.DQ] = new()
                {
                    // English: the Blue Button where Q was {0}
                    // Example: the Blue Button where Q was Blue
                    Discriminator = "der Blaue Knopf, bei dem Q {0} war,",
                    Arguments = new()
                    {
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Cyan"] = "Türkis",
                        ["Red"] = "Rot",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                    },
                },
                [SBlueButton.DOther] = new()
                {
                    // English: the Blue Button where {0} was {1}
                    Discriminator = "dem Blauen Knopf, bei dem {0} {1} war,",
                },
            },
        },

        // Blue Cipher
        [typeof(SBlueCipher)] = new()
        {
            ModuleName = "Blaue Geheimschrift",
            ModuleNameDative = "Blauen Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SBlueCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Blue Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["middle"] = "mittleren",
                        ["bottom"] = "unteren",
                    },
                },
            },
        },

        // Bob Barks
        [typeof(SBobBarks)] = new()
        {
            Questions = new()
            {
                [SBobBarks.Indicators] = new()
                {
                    // English: What was the {1} indicator label in {0}?
                    // Example: What was the top left indicator label in Bob Barks?
                    Question = "Welche Beschriftung hatte der {1} Indikator bei {0}?",
                    Arguments = new()
                    {
                        ["top left"] = "obere linke",
                        ["top right"] = "obere rechte",
                        ["bottom left"] = "untere linke",
                        ["bottom right"] = "untere rechte",
                    },
                },
                [SBobBarks.Positions] = new()
                {
                    // English: Which button flashed {1} in sequence in {0}?
                    // Example: Which button flashed first in sequence in Bob Barks?
                    Question = "Welcher Knopf blinkte als {1}er bei {0}?",
                    Answers = new()
                    {
                        ["top left"] = "oben links",
                        ["top right"] = "oben rechts",
                        ["bottom left"] = "unten links",
                        ["bottom right"] = "unten rechts",
                    },
                },
            },
        },

        // Boggle
        [typeof(SBoggle)] = new()
        {
            Questions = new()
            {
                [SBoggle.Letters] = new()
                {
                    // English: What letter was initially visible on {0}?
                    Question = "Welcher dieser Buchstaben war bei {0} anfänglich sichtbar?",
                },
            },
        },

        // Bomb Diffusal
        [typeof(SBombDiffusal)] = new()
        {
            Questions = new()
            {
                [SBombDiffusal.LicenseNumber] = new()
                {
                    // English: What was the license number in {0}?
                    Question = "Was war bei {0} die Lizenznummer?",
                },
            },
        },

        // Bone Apple Tea
        [typeof(SBoneAppleTea)] = new()
        {
            ModuleName = "Bonner Partie",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SBoneAppleTea.Phrase] = new()
                {
                    // English: Which phrase was shown on {0}?
                    Question = "Welcher Satz war bei {0} zu sehen?",
                },
            },
        },

        // Boob Tube
        [typeof(SBoobTube)] = new()
        {
            Questions = new()
            {
                [SBoobTube.Word] = new()
                {
                    // English: Which word was shown on {0}?
                    Question = "Welches Word war bei {0} zu sehen?",
                },
            },
        },

        // Book of Mario
        [typeof(SBookOfMario)] = new()
        {
            Questions = new()
            {
                [SBookOfMario.Pictures] = new()
                {
                    // English: Who said the {1} quote in {0}?
                    // Example: Who said the first quote in Book of Mario?
                    Question = "Wer sprach bei {0} das {1}e Zitat?",
                },
                [SBookOfMario.Quotes] = new()
                {
                    // English: What did {1} say in the {2} stage of {0}?
                    // Example: What did Goombell say in the first stage of Book of Mario?
                    Question = "Was sagte {1} bei {0} im {2}en Schritt?",
                },
            },
        },

        // Boolean Wires
        [typeof(SBooleanWires)] = new()
        {
            ModuleName = "Boolesche Drähte",
            Questions = new()
            {
                [SBooleanWires.EnteredOperators] = new()
                {
                    // English: Which operator did you submit in the {1} stage of {0}?
                    // Example: Which operator did you submit in the first stage of Boolean Wires?
                    Question = "Welcher Operator wurde bei {0} im {1}en Schritt eingegeben?",
                },
            },
        },

        // Boomtar the Great
        [typeof(SBoomtarTheGreat)] = new()
        {
            ModuleName = "Boomtar der Große",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SBoomtarTheGreat.Rules] = new()
                {
                    // English: What was rule {1} in {0}?
                    // Example: What was rule one in Boomtar the Great?
                    Question = "Was war bei {0} Regel Nr. {1}?",
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
            Questions = new()
            {
                [SBorderedKeys.BorderColor] = new()
                {
                    // English: What was the {1} key’s border color when it was pressed in {0}?
                    // Example: What was the first key’s border color when it was pressed in Bordered Keys?
                    Question = "Was war beim Druck auf den {1}en Knopf bei {0} die Randfarbe?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Cyan"] = "Türkis",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                    },
                },
                [SBorderedKeys.Digit] = new()
                {
                    // English: What was the digit displayed when the {1} key was pressed in {0}?
                    // Example: What was the digit displayed when the first key was pressed in Bordered Keys?
                    Question = "Was war beim Druck auf den {1}en Knopf bei {0} die angezeigte Ziffer?",
                },
                [SBorderedKeys.KeyColor] = new()
                {
                    // English: What was the {1} key’s key color when it was pressed in {0}?
                    // Example: What was the first key’s key color when it was pressed in Bordered Keys?
                    Question = "Was war beim Druck auf den {1}en Knopf bei {0} die Knopffarbe?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Cyan"] = "Türkis",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                    },
                },
                [SBorderedKeys.Label] = new()
                {
                    // English: What was the {1} key’s label when it was pressed in {0}?
                    // Example: What was the first key’s label when it was pressed in Bordered Keys?
                    Question = "Was war beim Druck auf den {1}en Knopf bei {0} die Aufschrift?",
                },
                [SBorderedKeys.LabelColor] = new()
                {
                    // English: What was the {1} key’s label color when it was pressed in {0}?
                    // Example: What was the first key’s label color when it was pressed in Bordered Keys?
                    Question = "Was war beim Druck auf den {1}en Knopf bei {0} die Aufschriftfarbe?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Cyan"] = "Türkis",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                    },
                },
            },
        },

        // Bottom Gear
        [typeof(SBottomGear)] = new()
        {
            Questions = new()
            {
                [SBottomGear.Tweet] = new()
                {
                    // English: What tweet was shown in {0}?
                    Question = "Welcher Tweet war bei {0} zu sehen?",
                },
            },
        },

        // Boxing
        [typeof(SBoxing)] = new()
        {
            Questions = new()
            {
                [SBoxing.StrengthByContestant] = new()
                {
                    // English: What was {1}’s strength rating on {0}?
                    // Example: What was Muhammad’s strength rating on Boxing?
                    Question = "Was war bei {0} die Kraftstufe von {1}?",
                },
                [SBoxing.ContestantByStrength] = new()
                {
                    // English: What was the {1} of the contestant with strength rating {2} on {0}?
                    // Example: What was the first name of the contestant with strength rating 0 on Boxing?
                    Question = "Was war bei {0} der {1} des Kandidaten mit Kraftstufe {2}?",
                    Arguments = new()
                    {
                        ["first name"] = "Vorname",
                        ["last name"] = "Nachname",
                        ["substitute’s first name"] = "Vorname des Ersatzmanns",
                        ["substitute’s last name"] = "Nachname des Ersatzmanns",
                    },
                },
                [SBoxing.Names] = new()
                {
                    // English: Which {1} appeared on {0}?
                    // Example: Which contestant’s first name appeared on Boxing?
                    Question = "Was war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["contestant’s first name"] = "der Vorname eines Kandidaten",
                        ["contestant’s last name"] = "der Nachname eines Kandidaten",
                        ["substitute’s first name"] = "der Vorname eines Ersatzmanns",
                        ["substitute’s last name"] = "der Nachname eines Ersatzmanns",
                    },
                },
            },
        },

        // Braille
        [typeof(SBraille)] = new()
        {
            ModuleName = "Blindenschrift",
            Questions = new()
            {
                [SBraille.Pattern] = new()
                {
                    // English: What was the {1} pattern in {0}?
                    // Example: What was the first pattern in Braille?
                    Question = "Was war bei {0} das {1}e Zeichen?",
                },
            },
        },

        // Breakfast Egg
        [typeof(SBreakfastEgg)] = new()
        {
            ModuleName = "Frühstücksei",
            Questions = new()
            {
                [SBreakfastEgg.Color] = new()
                {
                    // English: Which color appeared on the egg in {0}?
                    Question = "Welche Farbe erschien bei {0} auf dem Ei?",
                    Answers = new()
                    {
                        ["Crimson"] = "Karmin",
                        ["Orange"] = "Orange",
                        ["Pink"] = "Pink",
                        ["Beige"] = "Beige",
                        ["Cyan"] = "Türkis",
                        ["Lime"] = "Limette",
                        ["Petrol"] = "Benzin",
                    },
                },
            },
        },

        // Broken Buttons
        [typeof(SBrokenButtons)] = new()
        {
            ModuleName = "Kaputte Knöpfe",
            ModuleNameDative = "Kaputten Knöpfen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SBrokenButtons.Question] = new()
                {
                    // English: What was the {1} correct button you pressed in {0}?
                    // Example: What was the first correct button you pressed in Broken Buttons?
                    Question = "Was war bei {0} der als {1}e korrekt gedrückte Knopf?",
                },
            },
        },

        // Broken Guitar Chords
        [typeof(SBrokenGuitarChords)] = new()
        {
            ModuleName = "Kaputte Gitarrenakkorde",
            ModuleNameDative = "Kaputten Gitarrenakkorden",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SBrokenGuitarChords.DisplayedChord] = new()
                {
                    // English: What was the displayed chord in {0}?
                    Question = "Welcher Akkord wurde bei {0} angezeigt?",
                },
                [SBrokenGuitarChords.MutedString] = new()
                {
                    // English: In which position, from left to right, was the broken string in {0}?
                    Question = "An welcher Position, von links nach rechts, war bei {0} die kaputte Saite?",
                },
            },
        },

        // Brown Cipher
        [typeof(SBrownCipher)] = new()
        {
            ModuleName = "Braune Geheimschrift",
            ModuleNameDative = "Braunen Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SBrownCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Brown Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "ober",
                        ["middle"] = "mittler",
                        ["bottom"] = "unter",
                    },
                },
            },
        },

        // Brush Strokes
        [typeof(SBrushStrokes)] = new()
        {
            ModuleName = "Pinselstriche",
            ModuleNameDative = "Pinselstrichen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SBrushStrokes.MiddleColor] = new()
                {
                    // English: What was the color of the middle contact point in {0}?
                    Question = "Was war bei {0} die Farbe des mittleren Kontaktes?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Lime"] = "Limette",
                        ["Green"] = "Grün",
                        ["Cyan"] = "Türkis",
                        ["Sky"] = "Himmelblau",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                        ["Magenta"] = "Magenta",
                        ["Brown"] = "Braun",
                        ["White"] = "Weiß",
                        ["Gray"] = "Grau",
                        ["Black"] = "Schwarz",
                        ["Pink"] = "Pink",
                    },
                },
            },
        },

        // The Bulb
        [typeof(SBulb)] = new()
        {
            ModuleName = "Die Glühlampe",
            ModuleNameDative = "Glühlampe",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SBulb.InitialState] = new()
                {
                    // English: Was the bulb initially lit in {0}?
                    Question = "War die Glühlampe bei {0} anfänglich an?",
                    Answers = new()
                    {
                        ["Yes"] = "Ja",
                        ["No"] = "Nein",
                    },
                },
            },
        },

        // Burger Alarm
        [typeof(SBurgerAlarm)] = new()
        {
            ModuleName = "Alarmbeilage",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SBurgerAlarm.Digits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Burger Alarm?
                    Question = "Was war bei {0} die {1}e Ziffer auf dem Display?",
                },
                [SBurgerAlarm.OrderNumbers] = new()
                {
                    // English: What was the {1} order number in {0}?
                    // Example: What was the first order number in Burger Alarm?
                    Question = "Was war bei {0} die {1} Auftragsnummer?",
                },
            },
        },

        // Burglar Alarm
        [typeof(SBurglarAlarm)] = new()
        {
            ModuleName = "Alarmanlage",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SBurglarAlarm.Digits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Burglar Alarm?
                    Question = "Was war bei {0} die {1}e Ziffer auf dem Display?",
                },
            },
        },

        // The Button
        [typeof(SButton)] = new()
        {
            ModuleName = "Der Knopf",
            ModuleNameDative = "Knopf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SButton.LightColor] = new()
                {
                    // English: What color did the light glow in {0}?
                    Question = "In welcher Farbe leuchtete bei {0} der Streifen?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["blue"] = "blau",
                        ["yellow"] = "gelb",
                        ["white"] = "weiß",
                    },
                },
            },
        },

        // Buttonage
        [typeof(SButtonage)] = new()
        {
            ModuleName = "Knopferei",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SButtonage.Buttons] = new()
                {
                    // English: How many {1} buttons were there on {0}?
                    // Example: How many red buttons were there on Buttonage?
                    Question = "Wie viele {1} waren bei {0} zu sehen?",
                    Arguments = new()
                    {
                        ["red"] = "rote Tasten",
                        ["green"] = "grüne Tasten",
                        ["orange"] = "orangene Tasten",
                        ["blue"] = "blaue Tasten",
                        ["pink"] = "pinke Tasten",
                        ["white"] = "weiße Tasten",
                        ["black"] = "schwarze Tasten",
                        ["white-bordered"] = "weiß umrandete Tasten",
                        ["pink-bordered"] = "pink umrandete Tasten",
                        ["gray-bordered"] = "grau umrandete Tasten",
                        ["red-bordered"] = "rot umrandete Tasten",
                        ["“P”"] = "“P”-Tasten",
                        ["special"] = "Spezialtasten",
                    },
                },
            },
        },

        // Button Sequence
        [typeof(SButtonSequence)] = new()
        {
            ModuleName = "Knopffolgen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SButtonSequence.sColorOccurrences] = new()
                {
                    // English: How many of the buttons in {0} were {1}?
                    // Example: How many of the buttons in Button Sequence were red?
                    Question = "Wie viele der Knöpfe bei {0} waren {1}?",
                    Arguments = new()
                    {
                        ["red"] = "rot",
                        ["blue"] = "blau",
                        ["yellow"] = "gelb",
                        ["white"] = "weiß",
                    },
                },
            },
        },

        // Cacti’s Conundrum
        [typeof(SCactisConundrum)] = new()
        {
            ModuleName = "Kakteen-Zwickmühle",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCactisConundrum.Color] = new()
                {
                    // English: What color was the LED in the {1} stage of {0}?
                    // Example: What color was the LED in the first stage of Cacti’s Conundrum?
                    Question = "Welche Farbe hatte bei {0} die LED in der {1}en Stufe?",
                    Answers = new()
                    {
                        ["Blue"] = "Blau",
                        ["Lime"] = "Limette",
                        ["Orange"] = "Orange",
                        ["Red"] = "Rot",
                    },
                },
            },
        },

        // Caesar Cycle
        [typeof(SCaesarCycle)] = new()
        {
            ModuleName = "Cäsar-Schiffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCaesarCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Caesar Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}te Zeiger?",
                },
                [SCaesarCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Caesar Cycle?
                    Question = "Welcher Buchstabe stand bei {0} auf dem {1}en Zeiger?",
                },
            },
            Discriminators = new()
            {
                [SCaesarCycle.LabelDiscriminator] = new()
                {
                    // English: the Caesar Cycle that had the letter {0} on a dial
                    // Example: the Caesar Cycle that had the letter A on a dial
                    Discriminator = "der Cäsar-Schiffer, bei der der Buchstabe {0} vorkam,",
                },
            },
        },

        // Caesar Psycho
        [typeof(SCaesarPsycho)] = new()
        {
            ModuleName = "Cäsar-Psycho",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SCaesarPsycho.ScreenTexts] = new()
                {
                    // English: What text was on the top display in the {1} stage of {0}?
                    // Example: What text was on the top display in the first stage of Caesar Psycho?
                    Question = "Welcher Text war bei {0} in der {1}en Stufe auf dem oberen Display zu sehen?",
                },
                [SCaesarPsycho.ScreenColor] = new()
                {
                    // English: What color was the text on the top display in the second stage of {0}?
                    Question = "Welche Farbe hatte bei {0} in der {1}en Stufe der Text auf dem oberen Display?",
                },
            },
        },

        // Caesar's Maths
        [typeof(SCaesarsMaths)] = new()
        {
            ModuleName = "Cäsars Mathe",
            Questions = new()
            {
                [SCaesarsMaths.LED] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the first LED in Caesar's Maths?
                    Question = "Welche Farbe hatte bei {0} die {1}e LED?",
                    Answers = new()
                    {
                        ["Yellow"] = "Gelb",
                        ["Blue"] = "Blau",
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                    },
                },
            },
        },

        // Calendar
        [typeof(SCalendar)] = new()
        {
            ModuleName = "Kalender",
            Questions = new()
            {
                [SCalendar.LedColor] = new()
                {
                    // English: What was the LED color in {0}?
                    Question = "Welche Farbe hatte die LED bei {0}?",
                    Answers = new()
                    {
                        ["Green"] = "Grün",
                        ["Yellow"] = "Gelb",
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                    },
                },
            },
        },

        // CA-RPS
        [typeof(SCARPS)] = new()
        {
            Questions = new()
            {
                [SCARPS.Cell] = new()
                {
                    // English: What color was this cell initially in {0}? (+ sprite)
                    Question = "Welche Farbe hatte diese Zelle bei {0} am Anfang?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Black"] = "Schwarz",
                    },
                },
            },
        },

        // Cartinese
        [typeof(SCartinese)] = new()
        {
            ModuleName = "Cartinesisch",
            Questions = new()
            {
                [SCartinese.ButtonColors] = new()
                {
                    // English: What color was the {1} button in {0}?
                    // Example: What color was the up button in Cartinese?
                    Question = "Welche Farbe hatte bei {0} die {1}?",
                    Arguments = new()
                    {
                        ["up"] = "Hoch-Taste",
                        ["right"] = "Rechts-Taste",
                        ["down"] = "Runter-Taste",
                        ["left"] = "Links-Taste",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                    },
                },
                [SCartinese.Lyrics] = new()
                {
                    // English: What lyric was played by the {1} button in {0}?
                    // Example: What lyric was played by the up button in Cartinese?
                    Question = "Welcher Songtext wurde bei {0} von der {1} abgespielt?",
                    Arguments = new()
                    {
                        ["up"] = "Hoch-Taste",
                        ["right"] = "Rechts-Taste",
                        ["down"] = "Runter-Taste",
                        ["left"] = "Links-Taste",
                    },
                },
            },
        },

        // Catchphrase
        [typeof(SCatchphrase)] = new()
        {
            Questions = new()
            {
                [SCatchphrase.Colour] = new()
                {
                    // English: What was the colour of the {1} panel in {0}?
                    // Example: What was the colour of the top-left panel in Catchphrase?
                    Question = "Welche Farbe hatte bei {0} die {1} Tafel?",
                    Arguments = new()
                    {
                        ["top-left"] = "obere linke",
                        ["top-right"] = "obere rechte",
                        ["bottom-left"] = "untere linke",
                        ["bottom-right"] = "untere rechte",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Orange"] = "Orange",
                        ["Purple"] = "Lila",
                        ["Yellow"] = "Gelb",
                    },
                },
            },
        },

        // Challenge & Contact
        [typeof(SChallengeAndContact)] = new()
        {
            Questions = new()
            {
                [SChallengeAndContact.Answers] = new()
                {
                    // English: What was the {1} submitted answer in {0}?
                    // Example: What was the first submitted answer in Challenge & Contact?
                    Question = "Was war bei {0} die als {1}e eingegebene Antwort?",
                },
            },
        },

        // Character Codes
        [typeof(SCharacterCodes)] = new()
        {
            ModuleName = "Zeichencodes",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SCharacterCodes.Character] = new()
                {
                    // English: What was the {1} character in {0}?
                    // Example: What was the first character in Character Codes?
                    Question = "Was war bei {0} das {1}e Zeichen?",
                },
            },
        },

        // Character Shift
        [typeof(SCharacterShift)] = new()
        {
            ModuleName = "Zeichenschieber",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SCharacterShift.Letters] = new()
                {
                    // English: Which letter was present but not submitted on the left slider of {0}?
                    Question = "Welcher Buchstabe war bei {0} auf dem linken Schieber zu sehen, aber nicht Teil der Lösung?",
                },
                [SCharacterShift.Digits] = new()
                {
                    // English: Which digit was present but not submitted on the right slider of {0}?
                    Question = "Welche Ziffer war bei {0} auf dem rechten Schieber zu sehen, aber nicht Teil der Lösung?",
                },
            },
        },

        // Character Slots
        [typeof(SCharacterSlots)] = new()
        {
            ModuleName = "Figurenkabinett",
            Questions = new()
            {
                [SCharacterSlots.DisplayedCharacters] = new()
                {
                    // English: Who was displayed in the {1} slot in the {2} stage of {0}?
                    // Example: Who was displayed in the first slot in the first stage of Character Slots?
                    Question = "Wer war bei {0} in der {2}en Stufe an {1}er Stelle zu sehen?",
                },
            },
        },

        // Cheap Checkout
        [typeof(SCheapCheckout)] = new()
        {
            ModuleName = "Klingelkasse",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCheapCheckout.Paid] = new()
                {
                    // English: What was {1} in {0}?
                    // Example: What was the paid amount in Cheap Checkout?
                    Question = "Was war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["the paid amount"] = "der Betrag",
                        ["the first paid amount"] = "der erste Betrag",
                        ["the second paid amount"] = "der zweite Betrag",
                    },
                },
            },
        },

        // Cheat Checkout
        [typeof(SCheatCheckout)] = new()
        {
            ModuleName = "Schlingelkasse",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCheatCheckout.Currency] = new()
                {
                    // English: What was the cryptocurrency of {0}?
                    Question = "Was war bei {0} die Kryptowährung?",
                },
                [SCheatCheckout.Hack] = new()
                {
                    // English: What was the hack method for the {1} hack of {0}?
                    // Example: What was the hack method for the first hack of Cheat Checkout?
                    Question = "Was war bei {0} die {1}e Exploitmethode?",
                },
                [SCheatCheckout.Site] = new()
                {
                    // English: What was the site for the {1} hack of {0}?
                    // Example: What was the site for the first hack of Cheat Checkout?
                    Question = "Welche Domain wurde bei {0} vom {1}en Exploit angegriffen?",
                },
            },
        },

        // Cheep Checkout
        [typeof(SCheepCheckout)] = new()
        {
            ModuleName = "Zwitscherkasse",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCheepCheckout.Birds] = new()
                {
                    // English: Which bird {1} present in {0}?
                    // Example: Which bird was present in Cheep Checkout?
                    Question = "Welcher Vogel war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["was"] = "zu sehen",
                        ["was not"] = "nicht zu sehen",
                    },
                    Answers = new()
                    {
                        ["Auklet"] = "Alk",
                        ["Bluebird"] = "Berghüttensänger",
                        ["Chickadee"] = "Chickadee-Meise",
                        ["Dove"] = "Diamanttaube",
                        ["Egret"] = "Europa-Reiher",
                        ["Finch"] = "Fink",
                        ["Godwit"] = "Gugelschnepfe",
                        ["Hummingbird"] = "Hainkolibri",
                        ["Ibis"] = "Ibis",
                        ["Jay"] = "Jagdhäher",
                        ["Kinglet"] = "Königsgoldhähnchen",
                        ["Loon"] = "Lagunentaucher",
                        ["Magpie"] = "Meerelster",
                        ["Nuthatch"] = "Nusskleiber",
                        ["Oriole"] = "Opern-Pirol",
                        ["Pipit"] = "Pieper",
                        ["Quail"] = "Quantwachtel",
                        ["Raven"] = "Rabe",
                        ["Shrike"] = "Siebentöter",
                        ["Thrush"] = "Taunus-Drossel",
                        ["Umbrellabird"] = "Urschirmvogel",
                        ["Vireo"] = "Vireo",
                        ["Warbler"] = "Waldsänger",
                        ["Xantus’s Hummingbird"] = "Xantus-Kolibri",
                        ["Yellowlegs"] = "Yukon-Gelbschenkel",
                        ["Zigzag Heron"] = "Zickzackreiher",
                    },
                },
            },
        },

        // Chess
        [typeof(SChess)] = new()
        {
            ModuleName = "Schach",
            Questions = new()
            {
                [SChess.Coordinate] = new()
                {
                    // English: What was the {1} coordinate in {0}?
                    // Example: What was the first coordinate in Chess?
                    Question = "Was war bei {0} die {1}e Koordinate?",
                },
            },
        },

        // Chinese Counting
        [typeof(SChineseCounting)] = new()
        {
            ModuleName = "Chinesisch Zählen",
            Questions = new()
            {
                [SChineseCounting.LED] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the left LED in Chinese Counting?
                    Question = "Welche Farbe hatte bei {0} die {1} LED?",
                    Arguments = new()
                    {
                        ["left"] = "linke",
                        ["right"] = "rechte",
                    },
                    Answers = new()
                    {
                        ["White"] = "Weiß",
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Orange"] = "Orange",
                    },
                },
            },
        },

        // Chinese Remainder Theorem
        [typeof(SChineseRemainderTheorem)] = new()
        {
            ModuleName = "Chinesischer Restsatz",
            ModuleNameDative = "Chinesischen Restsatz",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SChineseRemainderTheorem.Equations] = new()
                {
                    // English: Which equation was used in {0}?
                    Question = "Welche Gleichung wurde bei {0} verwendet?",
                },
            },
        },

        // Chord Qualities
        [typeof(SChordQualities)] = new()
        {
            ModuleName = "Akkordqualität",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SChordQualities.Notes] = new()
                {
                    // English: Which note was part of the given chord in {0}?
                    Question = "Welche Note war bei {0} Teil des vorgegebenen Akkords?",
                },
            },
        },

        // ↻↺
        [typeof(SClockCounter)] = new()
        {
            Questions = new()
            {
                [SClockCounter.Arrows] = new()
                {
                    // English: Which arrow was shown in {0}?
                    Question = "Welcher Pfeil war bei {0} zu sehen?",
                },
            },
        },

        // The Code
        [typeof(SCode)] = new()
        {
            ModuleName = "Der Code",
            ModuleNameDative = "Code",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SCode.DisplayNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Welche Zahl wurde bei {0} angezeigt?",
                },
            },
        },

        // Codenames
        [typeof(SCodenames)] = new()
        {
            Questions = new()
            {
                [SCodenames.Answers] = new()
                {
                    // English: Which of these words was submitted in {0}?
                    Question = "Welches Wort war bei {0} ein Lösungswort?",
                },
            },
        },

        // Coffee Beans
        [typeof(SCoffeeBeans)] = new()
        {
            ModuleName = "Kaffeebohnen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SCoffeeBeans.Movements] = new()
                {
                    // English: What was the {1} movement in {0}?
                    // Example: What was the first movement in Coffee Beans?
                    Question = "Was war bei {0} die {1}e Bewegung?",
                    Answers = new()
                    {
                        ["Horizontal"] = "Horizontal",
                        ["Vertical"] = "Vertikal",
                        ["Diagonal"] = "Diagonal",
                        ["Nothing"] = "Keine",
                    },
                },
            },
        },

        // Coffeebucks
        [typeof(SCoffeebucks)] = new()
        {
            Questions = new()
            {
                [SCoffeebucks.Coffee] = new()
                {
                    // English: What was the last served coffee in {0}?
                    Question = "Welcher Kaffee wurde bei {0} als letzter serviert?",
                },
            },
        },

        // Coinage
        [typeof(SCoinage)] = new()
        {
            ModuleName = "Münzerei",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCoinage.Flip] = new()
                {
                    // English: Which coin was flipped in {0}?
                    Question = "Welche Münze wurde bei {0} umgedreht?",
                },
            },
        },

        // Color Addition
        [typeof(SColorAddition)] = new()
        {
            Questions = new()
            {
                [SColorAddition.Numbers] = new()
                {
                    // English: What was {1}’s number in {0}?
                    // Example: What was red’s number in Color Addition?
                    Question = "Was war bei {0} die Zahl im {1}-Kanal?",
                    Arguments = new()
                    {
                        ["red"] = "Rot",
                        ["green"] = "Grün",
                        ["blue"] = "Blau",
                    },
                },
            },
        },

        // Color Braille
        [typeof(SColorBraille)] = new()
        {
            ModuleName = "Farbbraille",
            Questions = new()
            {
                [SColorBraille.Color] = new()
                {
                    // English: What color was this dot in {0}? (+ sprite)
                    Question = "Welche Farbe hatte dieser Punkt bei {0}?",
                    Answers = new()
                    {
                        ["Black"] = "Schwarz",
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Cyan"] = "Türkis",
                        ["Red"] = "Rot",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                        ["White"] = "Weiß",
                    },
                },
            },
        },

        // Color Decoding
        [typeof(SColorDecoding)] = new()
        {
            ModuleName = "Farbdekodierung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SColorDecoding.IndicatorColors] = new()
                {
                    // English: Which color {1} in the {2}-stage indicator pattern in {0}?
                    // Example: Which color appeared in the first-stage indicator pattern in Color Decoding?
                    Question = "Welche Farbe kam bei {0} im Indikatormuster der {2}en Stufe {1}?",
                    Arguments = new()
                    {
                        ["appeared"] = "vor",
                        ["did not appear"] = "nicht vor",
                    },
                    Answers = new()
                    {
                        ["Green"] = "Grün",
                        ["Purple"] = "Lila",
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Yellow"] = "Gelb",
                    },
                },
                [SColorDecoding.IndicatorPattern] = new()
                {
                    // English: What was the {1}-stage indicator pattern in {0}?
                    // Example: What was the first-stage indicator pattern in Color Decoding?
                    Question = "Was war bei {0} das Indikatormuster der {1}en Stufe?",
                    Answers = new()
                    {
                        ["Checkered"] = "Schachbrett",
                        ["Horizontal"] = "Horizontal",
                        ["Vertical"] = "Vertikal",
                        ["Solid"] = "Einfarbig",
                    },
                },
            },
        },

        // Colored Keys
        [typeof(SColoredKeys)] = new()
        {
            ModuleName = "Gefärbte Tasten",
            ModuleNameDative = "Gefärbten Tasten",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SColoredKeys.DisplayWord] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "Was war bei {0} das Wort auf dem Display?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["blue"] = "blau",
                        ["green"] = "grün",
                        ["yellow"] = "gelb",
                        ["purple"] = "lila",
                        ["white"] = "weiß",
                    },
                },
                [SColoredKeys.DisplayWordColor] = new()
                {
                    // English: What was the displayed word’s color in {0}?
                    Question = "Welche Farbe hatte bei {0} das Wort auf dem Display?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["blue"] = "blau",
                        ["green"] = "grün",
                        ["yellow"] = "gelb",
                        ["purple"] = "lila",
                        ["white"] = "weiß",
                    },
                },
                [SColoredKeys.KeyLetter] = new()
                {
                    // English: What letter was on the {1} key in {0}?
                    // Example: What letter was on the top-left key in Colored Keys?
                    Question = "Welcher Buchstabe stand bei {0} auf der {1} Taste?",
                    Arguments = new()
                    {
                        ["top-left"] = "obere linken",
                        ["top-right"] = "obere rechten",
                        ["bottom-left"] = "untere linken",
                        ["bottom-right"] = "untere rechten",
                    },
                },
                [SColoredKeys.KeyColor] = new()
                {
                    // English: What was the color of the {1} key in {0}?
                    // Example: What was the color of the top-left key in Colored Keys?
                    Question = "Welche Farbe hatte bei {0} die {1} Taste?",
                    Arguments = new()
                    {
                        ["top-left"] = "obere linke",
                        ["top-right"] = "obere rechte",
                        ["bottom-left"] = "untere linke",
                        ["bottom-right"] = "untere rechte",
                    },
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["blue"] = "blau",
                        ["green"] = "grün",
                        ["yellow"] = "gelb",
                        ["purple"] = "lila",
                        ["white"] = "weiß",
                    },
                },
            },
        },

        // Colored Squares
        [typeof(SColoredSquares)] = new()
        {
            ModuleName = "Gefärbte Felder",
            ModuleNameDative = "Gefärbten Feldern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SColoredSquares.FirstGroup] = new()
                {
                    // English: What was the first color group in {0}?
                    Question = "Was war bei {0} die erste Farbgruppe?",
                    Answers = new()
                    {
                        ["White"] = "Weiß",
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Yellow"] = "Gelb",
                        ["Magenta"] = "Magenta",
                    },
                },
            },
        },

        // Colored Switches
        [typeof(SColoredSwitches)] = new()
        {
            ModuleName = "Gefärbte Schalter",
            ModuleNameDative = "Gefärbten Schaltern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SColoredSwitches.InitialPosition] = new()
                {
                    // English: What was the initial position of the switches in {0}?
                    Question = "Wie lagen bei {0} die Schalter am Anfang?",
                },
                [SColoredSwitches.WhenLEDsCameOn] = new()
                {
                    // English: What was the position of the switches when the LEDs came on in {0}?
                    Question = "Wie lagen bei {0} die Schalter, als die LEDs angingen?",
                },
            },
        },

        // Color Morse
        [typeof(SColorMorse)] = new()
        {
            ModuleName = "Gefärbte Morsezeichen",
            ModuleNameDative = "Gefärbten Morsezeichen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SColorMorse.Color] = new()
                {
                    // English: What was the color of the {1} LED in {0}?
                    // Example: What was the color of the first LED in Color Morse?
                    Question = "Welche Farbe hatte bei {0} die {1}e LED?",
                    Answers = new()
                    {
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Orange"] = "Orange",
                        ["Purple"] = "Lila",
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["White"] = "Weiß",
                    },
                },
                [SColorMorse.Character] = new()
                {
                    // English: What character was flashed by the {1} LED in {0}?
                    // Example: What character was flashed by the first LED in Color Morse?
                    Question = "Welches Zeichen wurde bei {0} von der {1}en LED signalisiert?",
                },
            },
        },

        // Color One Two
        [typeof(SColorOneTwo)] = new()
        {
            Questions = new()
            {
                [SColorOneTwo.Color] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the left LED in Color One Two?
                    Question = "Welche Farbe hatte bei {0} die {1} LED?",
                    Arguments = new()
                    {
                        ["left"] = "linke",
                        ["right"] = "rechte",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Yellow"] = "Gelb",
                    },
                },
            },
        },

        // Colors Maximization
        [typeof(SColorsMaximization)] = new()
        {
            ModuleName = "Farbmaximierung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SColorsMaximization.ColorCount] = new()
                {
                    // English: How many buttons were {1} in {0}?
                    // Example: How many buttons were red in Colors Maximization?
                    Question = "Wie viele Tasten war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["red"] = "rot",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                    },
                },
            },
        },

        // Coloured Cubes
        [typeof(SColouredCubes)] = new()
        {
            ModuleName = "Gefärbte Würfel",
            ModuleNameDative = "Gefärbten Würfeln",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SColouredCubes.Colours] = new()
                {
                    // English: What was the colour of this {1} in the {2} stage of {0}? (+ sprite)
                    // Example: What was the colour of this cube in the first stage of Coloured Cubes? (+ sprite)
                    Question = "Welche Farbe hatte {1} bei {0} in der {2}en Stufe?",
                    Arguments = new()
                    {
                        ["cube"] = "dieser Würfel",
                        ["stage light"] = "dieser Stufenindikator",
                    },
                    Answers = new()
                    {
                        ["Black"] = "Schwarz",
                        ["Indigo"] = "Indigo",
                        ["Blue"] = "Blau",
                        ["Forest"] = "Waldgrün",
                        ["Teal"] = "Dunkeltürkis",
                        ["Azure"] = "Azur",
                        ["Green"] = "Grün",
                        ["Jade"] = "Jade",
                        ["Cyan"] = "Helltürkis",
                        ["Maroon"] = "Kastanie",
                        ["Plum"] = "Pflaume",
                        ["Violet"] = "Violett",
                        ["Olive"] = "Olivgrün",
                        ["Grey"] = "Grau",
                        ["Maya"] = "Maya",
                        ["Lime"] = "Limette",
                        ["Mint"] = "Minze",
                        ["Aqua"] = "Aquamarin",
                        ["Red"] = "Rot",
                        ["Rose"] = "Rosenrot",
                        ["Magenta"] = "Magenta",
                        ["Orange"] = "Orange",
                        ["Salmon"] = "Lachs",
                        ["Pink"] = "Pink",
                        ["Yellow"] = "Gelb",
                        ["Cream"] = "Cremegelb",
                        ["White"] = "Weiß",
                    },
                },
            },
        },

        // Coloured Cylinder
        [typeof(SColouredCylinder)] = new()
        {
            ModuleName = "Gefärbter Zylinder",
            ModuleNameDative = "Gefärbten Zylinder",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SColouredCylinder.Colours] = new()
                {
                    // English: What was the {1} colour flashed on the cylinder in {0}?
                    // Example: What was the first colour flashed on the cylinder in Coloured Cylinder?
                    Question = "Welche Farbe erschien bei {0} auf dem Zylinder als {1}e?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Yellow"] = "Gelb",
                        ["Magenta"] = "Rosa",
                        ["White"] = "Weiß",
                        ["Black"] = "Schwarz",
                    },
                },
            },
        },

        // Colour Flash
        [typeof(SColourFlash)] = new()
        {
            ModuleName = "Gefärbte Folge",
            ModuleNameDative = "Gefärbten Folge",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SColourFlash.LastColor] = new()
                {
                    // English: What was the color of the last word in the sequence in {0}?
                    Question = "Welche Farbe hatte bei {0} das letzte Wort in der Folge?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Magenta"] = "Magenta",
                        ["White"] = "Weiß",
                    },
                },
            },
        },

        // Concentration
        [typeof(SConcentration)] = new()
        {
            ModuleName = "Konzentrationsspiel",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SConcentration.StartingDigit] = new()
                {
                    // English: What number began here in {0}? (+ sprite)
                    Question = "Welche Zahl war bei {0} am Anfang hier?",
                },
            },
            Discriminators = new()
            {
                [SConcentration.Discriminator] = new()
                {
                    // English: the Concentration which began with {1} in the {0} position (in reading order)
                    // Example: the Concentration which began with 1 in the first position (in reading order)
                    Discriminator = "dem Konzentrationsspiel, bei dem in der {0}en Position (in Lesereihenfolge) am Anfang eine {1} war,",
                },
            },
        },

        // Conditional Buttons
        [typeof(SConditionalButtons)] = new()
        {
            ModuleName = "Bedingte Knöpfe",
            ModuleNameDative = "Bedingten Knöpfen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SConditionalButtons.Colors] = new()
                {
                    // English: What was the color of this button in {0}? (+ sprite)
                    Question = "Was war bei {0} die Farbe von diesem Knopf?",
                    Answers = new()
                    {
                        ["black"] = "schwarz",
                        ["blue"] = "blau",
                        ["dark green"] = "dunkelgrün",
                        ["light green"] = "hellgrün",
                        ["orange"] = "orange",
                        ["pink"] = "pink",
                        ["purple"] = "lila",
                        ["red"] = "rot",
                        ["white"] = "weiß",
                        ["yellow"] = "gelb",
                    },
                },
            },
        },

        // Connected Monitors
        [typeof(SConnectedMonitors)] = new()
        {
            ModuleName = "Verbundene Monitore",
            ModuleNameDative = "Verbundenen Monitoren",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SConnectedMonitors.Number] = new()
                {
                    // English: What number was initially displayed on this screen in {0}? (+ sprite)
                    Question = "Welche Zahl war bei {0} anfänglich auf diesem Display zu sehen?",
                },
                [SConnectedMonitors.SingleIndicator] = new()
                {
                    // English: What colour was the indicator on this screen in {0}? (+ sprite)
                    Question = "Welche Farbe hatte bei {0} der Indikator auf diesem Display?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                        ["White"] = "Weiß",
                    },
                },
                [SConnectedMonitors.OrdinalIndicator] = new()
                {
                    // English: What colour was the {1} indicator on this screen in {0}? (+ sprite)
                    // Example: What colour was the first indicator on this screen in Connected Monitors? (+ sprite)
                    Question = "Welche Farbe hatte bei {0} der {1}e Indikator auf diesem Display?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                        ["White"] = "Weiß",
                    },
                },
            },
        },

        // Connection Check
        [typeof(SConnectionCheck)] = new()
        {
            ModuleName = "Verbindungsprüfung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SConnectionCheck.Numbers] = new()
                {
                    // English: What pair of numbers was present in {0}?
                    Question = "Welches Zahlenpaar war bei {0} vorhanden?",
                },
            },
            Discriminators = new()
            {
                [SConnectionCheck.NoNs] = new()
                {
                    // English: the Connection Check with no {0}s
                    // Example: the Connection Check with no 1s
                    Discriminator = "der Verbindungsprüfung mit keiner {0}",
                },
                [SConnectionCheck.OneN] = new()
                {
                    // English: the Connection Check with one {0}
                    // Example: the Connection Check with one 1
                    Discriminator = "der Verbindungsprüfung mit einer {0}",
                },
                [SConnectionCheck.TwoNs] = new()
                {
                    // English: the Connection Check with two {0}s
                    // Example: the Connection Check with two 1s
                    Discriminator = "der Verbindungsprüfung mit zwei {0}en",
                },
                [SConnectionCheck.ThreeNs] = new()
                {
                    // English: the Connection Check with three {0}s
                    // Example: the Connection Check with three 1s
                    Discriminator = "der Verbindungsprüfung mit drei {0}en",
                },
                [SConnectionCheck.FourNs] = new()
                {
                    // English: the Connection Check with four {0}s
                    // Example: the Connection Check with four 1s
                    Discriminator = "der Verbindungsprüfung mit vier {0}en",
                },
            },
        },

        // Coordinates
        [typeof(SCoordinates)] = new()
        {
            ModuleName = "Koordinaten",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SCoordinates.FirstSolution] = new()
                {
                    // English: What was the solution you selected first in {0}?
                    Question = "Welche Lösung wurde bei {0} als erstes gewählt?",
                },
                [SCoordinates.Size] = new()
                {
                    // English: What was the grid size in {0}?
                    Question = "Was war bei {0} die Rastergröße?",
                },
            },
        },

        // Coordination
        [typeof(SCoordination)] = new()
        {
            ModuleName = "Koordinierung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCoordination.Label] = new()
                {
                    // English: What was the label of the starting coordinate in {0}?
                    Question = "Was war bei {0} die Aufschrift der Startkoordinate?",
                },
                [SCoordination.Position] = new()
                {
                    // English: Where was the starting coordinate in {0}?
                    Question = "Was war bei {0} die Startkoordinate?",
                },
            },
        },

        // Coral Cipher
        [typeof(SCoralCipher)] = new()
        {
            ModuleName = "Korall-Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCoralCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Coral Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["middle"] = "mittleren",
                        ["bottom"] = "unteren",
                    },
                },
            },
        },

        // Corners
        [typeof(SCorners)] = new()
        {
            ModuleName = "Ecken",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SCorners.Colors] = new()
                {
                    // English: What was the color of the {1} corner in {0}?
                    // Example: What was the color of the top-left corner in Corners?
                    Question = "Welche Farbe hatte bei {0} die {1}e Ecke?",
                    Arguments = new()
                    {
                        ["top-left"] = "obere linke",
                        ["top-right"] = "obere rechte",
                        ["bottom-right"] = "untere rechte",
                        ["bottom-left"] = "untere linke",
                    },
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["yellow"] = "gelb",
                    },
                },
                [SCorners.ColorCount] = new()
                {
                    // English: How many corners in {0} were {1}?
                    // Example: How many corners in Corners were red?
                    Question = "Wie viele der Ecken bei {0} waren {1}?",
                    Arguments = new()
                    {
                        ["red"] = "rot",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["yellow"] = "gelb",
                    },
                },
            },
        },

        // Cornflower Cipher
        [typeof(SCornflowerCipher)] = new()
        {
            ModuleName = "Kornblumen-Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCornflowerCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Cornflower Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["middle"] = "mittleren",
                        ["bottom"] = "unteren",
                    },
                },
            },
        },

        // Cosmic
        [typeof(SCosmic)] = new()
        {
            ModuleName = "Kosmik",
            Questions = new()
            {
                [SCosmic.Number] = new()
                {
                    // English: What was the number initially shown in {0}?
                    Question = "What was the number initially shown in {0}?",
                },
            },
        },

        // Crazy Hamburger
        [typeof(SCrazyHamburger)] = new()
        {
            ModuleName = "Verrückter Hamburger",
            ModuleNameDative = "Verrückten Hamburger",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SCrazyHamburger.Ingredient] = new()
                {
                    // English: What was the {1} ingredient shown in {0}?
                    // Example: What was the first ingredient shown in Crazy Hamburger?
                    Question = "Was war bei {0} die {1}e Zutat?",
                },
            },
        },

        // Crazy Maze
        [typeof(SCrazyMaze)] = new()
        {
            ModuleName = "Verrückter Irrgarten",
            ModuleNameDative = "Verrückten Irrgarten",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SCrazyMaze.StartOrGoal] = new()
                {
                    // English: What was the {1} location in {0}?
                    // Example: What was the starting location in Crazy Maze?
                    Question = "Was war bei {0} die {1}?",
                    Arguments = new()
                    {
                        ["starting"] = "Anfangsposition",
                        ["goal"] = "Zielposition",
                    },
                },
            },
        },

        // Cream Cipher
        [typeof(SCreamCipher)] = new()
        {
            ModuleName = "Creme-Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCreamCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Cream Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["middle"] = "mittleren",
                        ["bottom"] = "unteren",
                    },
                },
            },
        },

        // Creation
        [typeof(SCreation)] = new()
        {
            ModuleName = "Schöpfung",
            Questions = new()
            {
                [SCreation.Weather] = new()
                {
                    // English: What were the weather conditions on the {1} day in {0}?
                    // Example: What were the weather conditions on the first day in Creation?
                    Question = "Was waren die Wetterbedingungen am {1} Tag in {0}?",
                    Answers = new()
                    {
                        ["Clear"] = "Wolkenlos",
                        ["Heat Wave"] = "Hitzewelle",
                        ["Meteor Shower"] = "Meteorschauer",
                        ["Rain"] = "Regen",
                        ["Windy"] = "Wind",
                    },
                },
            },
        },

        // Crimson Cipher
        [typeof(SCrimsonCipher)] = new()
        {
            ModuleName = "Karmin-Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCrimsonCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Crimson Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["middle"] = "mittleren",
                        ["bottom"] = "unteren",
                    },
                },
            },
        },

        // Critters
        [typeof(SCritters)] = new()
        {
            ModuleName = "Kriechtiere",
            ModuleNameDative = "Kriechtieren",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SCritters.Color] = new()
                {
                    // English: What was the color in {0}?
                    Question = "Was war die Farbe bei {0}?",
                    Answers = new()
                    {
                        ["Yellow"] = "Gelb",
                        ["Pink"] = "Pink",
                        ["Blue"] = "Blau",
                        ["White"] = "Weiß",
                    },
                },
            },
        },

        // Cruel Binary
        [typeof(SCruelBinary)] = new()
        {
            ModuleName = "Höllisches Binärsystem",
            ModuleNameDative = "Höllischen Binärsystem",
            Questions = new()
            {
                [SCruelBinary.DisplayedWord] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "Was war bei {0} das Wort auf dem Display?",
                },
            },
        },

        // Cruel Keypads
        [typeof(SCruelKeypads)] = new()
        {
            ModuleName = "Höllische Tastenfelder",
            ModuleNameDative = "Höllischen Tastenfelder",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SCruelKeypads.Colors] = new()
                {
                    // English: What was the color of the bar in the {1} stage of {0}?
                    // Example: What was the color of the bar in the first stage of Cruel Keypads?
                    Question = "Was war bei {0} die Farbe des Balkens in der {1}en Stufe?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Magenta"] = "Magenta",
                        ["White"] = "Weiß",
                    },
                },
                [SCruelKeypads.DisplayedSymbols] = new()
                {
                    // English: Which of these characters appeared in the {1} stage of {0}?
                    // Example: Which of these characters appeared in the first stage of Cruel Keypads?
                    Question = "Welches dieser Zeichen war bei {0} in der ersten Stufe zu sehen?",
                },
            },
        },

        // The cRule
        [typeof(SCRule)] = new()
        {
            ModuleName = "Die CRegel",
            ModuleNameDative = "CRegel",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCRule.SymbolPair] = new()
                {
                    // English: Which symbol pair was here in {0}? (+ sprite)
                    Question = "Welches Symbolpaar war bei {0} an dieser Stelle?",
                },
                [SCRule.SymbolPairCell] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was ♤♤ in The cRule?
                    Question = "Wo war {1} bei {0}?",
                },
                [SCRule.SymbolPairPresent] = new()
                {
                    // English: Which symbol pair was present on {0}?
                    Question = "Welches Symbolpaar kam bei {0} vor?",
                },
                [SCRule.Prefilled] = new()
                {
                    // English: Which cell was pre-filled at the start of {0}?
                    Question = "Welche Zelle war bei {0} am Anfang vorgegeben?",
                },
            },
        },

        // Cryptic Cycle
        [typeof(SCrypticCycle)] = new()
        {
            ModuleName = "Kryptische Schiffer",
            ModuleNameDative = "Kryptischen Schiffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCrypticCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Cryptic Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}te Zeiger?",
                },
                [SCrypticCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Cryptic Cycle?
                    Question = "Welcher Buchstabe stand bei {0} auf dem {1}en Zeiger?",
                },
            },
            Discriminators = new()
            {
                [SCrypticCycle.LabelDiscriminator] = new()
                {
                    // English: the Cryptic Cycle that had the letter {0} on a dial
                    // Example: the Cryptic Cycle that had the letter A on a dial
                    Discriminator = "der Kryptischen Schiffer, bei der der Buchstabe {0} vorkam,",
                },
            },
        },

        // Cryptic Keypad
        [typeof(SCrypticKeypad)] = new()
        {
            ModuleName = "Kryptisches Tastenfeld",
            ModuleNameDative = "Kryptischen Tastenfeld",
            Questions = new()
            {
                [SCrypticKeypad.Labels] = new()
                {
                    // English: What was the label of the {1} key in {0}?
                    // Example: What was the label of the top-left key in Cryptic Keypad?
                    Question = "Was war bei {0} die Aufschrift auf der {1} Tasten?",
                    Arguments = new()
                    {
                        ["top-left"] = "oben-linken",
                        ["top-right"] = "oben-rechten",
                        ["bottom-left"] = "unten-linken",
                        ["bottom-right"] = "unten-rechten",
                    },
                },
                [SCrypticKeypad.Rotations] = new()
                {
                    // English: Which cardinal direction was the {1} key rotated to in {0}?
                    // Example: Which cardinal direction was the top-left key rotated to in Cryptic Keypad?
                    Question = "In welche Himmelsrichtung wurde bei {0} die {1} Taste gedreht?",
                    Arguments = new()
                    {
                        ["top-left"] = "obere linke",
                        ["top-right"] = "obere rechte",
                        ["bottom-left"] = "untere linke",
                        ["bottom-right"] = "untere rechte",
                    },
                    Answers = new()
                    {
                        ["North"] = "Norden",
                        ["East"] = "Osten",
                        ["South"] = "Süden",
                        ["West"] = "Westen",
                    },
                },
            },
        },

        // The Cube
        [typeof(SCube)] = new()
        {
            ModuleName = "Der Würfel",
            ModuleNameDative = "Würfel",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SCube.Rotations] = new()
                {
                    // English: What was the {1} cube rotation in {0}?
                    // Example: What was the first cube rotation in The Cube?
                    Question = "Was war bei {0} die {1}e Rotation?",
                    Answers = new()
                    {
                        ["rotate cw"] = "im Uhrzeigersinn",
                        ["tip left"] = "nach links gekippt",
                        ["tip backwards"] = "nach hinten gekippt",
                        ["rotate ccw"] = "gegen den Uhrzeigersinn",
                        ["tip right"] = "nach rechts gekippt",
                        ["tip forwards"] = "nach vorne gekippt",
                    },
                },
            },
        },

        // Cursed Double-Oh
        [typeof(SCursedDoubleOh)] = new()
        {
            ModuleName = "Verfluchte Doppel-Null",
            ModuleNameDative = "Verflüchten Doppel-Null",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCursedDoubleOh.InitialPosition] = new()
                {
                    // English: What was the first digit of the initially displayed number in {0}?
                    Question = "Was war bei {0} am Anfang die erste Ziffer auf dem Display?",
                },
            },
        },

        // Customer Identification
        [typeof(SCustomerIdentification)] = new()
        {
            ModuleName = "Kundenidentifikation",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCustomerIdentification.Customer] = new()
                {
                    // English: Who was the {1} customer in {0}?
                    // Example: Who was the first customer in Customer Identification?
                    Question = "Wer war bei {0} der {1}e Kunde?",
                },
            },
        },

        // The Cyan Button
        [typeof(SCyanButton)] = new()
        {
            ModuleName = "Der Türkise Knopf",
            ModuleNameDative = "Türkisen Knopf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SCyanButton.Positions] = new()
                {
                    // English: Where was the button at the {1} stage in {0}?
                    // Example: Where was the button at the first stage in The Cyan Button?
                    Question = "Wo war bei {0} der Knopf in der {1}en Stufe?",
                    Answers = new()
                    {
                        ["top left"] = "oben links",
                        ["top middle"] = "oben Mitte",
                        ["top right"] = "oben rechts",
                        ["bottom left"] = "unten links",
                        ["bottom middle"] = "unten Mitte",
                        ["bottom right"] = "unten rechts",
                    },
                },
            },
        },

        // DACH Maze
        [typeof(SDACHMaze)] = new()
        {
            ModuleName = "DACH-Irrgarten",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SDACHMaze.Origin] = new()
                {
                    // English: Which region did you depart from in {0}?
                    Question = "Wo ging’s bei {0} los?",
                    Answers = new()
                    {
                        ["Burgenland, A"] = "Burgenland, A",
                        ["Carinthia, A"] = "Kärnten, A",
                        ["Lower Austria, A"] = "Niederösterreich, A",
                        ["North Tyrol, A"] = "Nordtirol, A",
                        ["Upper Austria, A"] = "Oberösterreich, A",
                        ["East Tyrol, A"] = "Osttirol, A",
                        ["Salzburg, A"] = "Salzburg, A",
                        ["Styria, A"] = "Steiermark, A",
                        ["Vorarlberg, A"] = "Vorarlberg, A",
                        ["Vienna, A"] = "Wien, A",
                        ["Aargau, CH"] = "Aargau, CH",
                        ["Appenzell Inner Rhodes, CH"] = "Appenzell Innerrhoden, CH",
                        ["Appenzell Outer Rhodes, CH"] = "Appenzell Ausserrhoden, CH",
                        ["Basel Country, CH"] = "Baselland, CH",
                        ["Bern, CH"] = "Bern, CH",
                        ["Basel City, CH"] = "Baselstadt, CH",
                        ["Fribourg, CH"] = "Freiburg, CH",
                        ["Geneva, CH"] = "Genf, CH",
                        ["Glarus, CH"] = "Glarus, CH",
                        ["Grisons, CH"] = "Graubünden, CH",
                        ["Jura, CH"] = "Jura, CH",
                        ["Luzern, CH"] = "Luzern, CH",
                        ["Nidwalden, CH"] = "Nidwalden, CH",
                        ["Neuchâtel, CH"] = "Neuenburg, CH",
                        ["Obwalden, CH"] = "Obwalden, CH",
                        ["Schaffhausen, CH"] = "Schaffhausen, CH",
                        ["St. Gallen, CH"] = "St. Gallen, CH",
                        ["Solothurn, CH"] = "Solothurn, CH",
                        ["Schwyz, CH"] = "Schwyz, CH",
                        ["Thurgau, CH"] = "Thurgau, CH",
                        ["Ticino, CH"] = "Tessin, CH",
                        ["Uri, CH"] = "Uri, CH",
                        ["Vaud, CH"] = "Vaud, CH",
                        ["Valais, CH"] = "Wallis, CH",
                        ["Zug, CH"] = "Zug, CH",
                        ["Zürich, CH"] = "Zürich, CH",
                        ["Brandenburg, D"] = "Brandenburg, D",
                        ["Berlin, D"] = "Berlin, D",
                        ["Baden-Württemberg, D"] = "Baden-Württemberg, D",
                        ["Bavaria, D"] = "Bayern, D",
                        ["Bremen, D"] = "Bremen, D",
                        ["Hesse, D"] = "Hessen, D",
                        ["Hamburg, D"] = "Hamburg, D",
                        ["Mecklenburg-Vorpommern, D"] = "Mecklenburg-Vorpommern, D",
                        ["Lower Saxony, D"] = "Niedersachsen, D",
                        ["North Rhine-Westphalia, D"] = "Nordrhein-Westfalen, D",
                        ["Rhineland-Palatinate, D"] = "Rheinland-Pfalz, D",
                        ["Schleswig-Holstein, D"] = "Schleswig-Holstein, D",
                        ["Saarland, D"] = "Saarland, D",
                        ["Saxony, D"] = "Sachsen, D",
                        ["Saxony-Anhalt, D"] = "Sachsen-Anhalt, D",
                        ["Thuringia, D"] = "Thüringen, D",
                        ["Liechtenstein"] = "Liechtenstein",
                    },
                },
            },
        },

        // Deaf Alley
        [typeof(SDeafAlley)] = new()
        {
            ModuleName = "Taube Gasse",
            ModuleNameDative = "Tauben Gasse",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SDeafAlley.Shape] = new()
                {
                    // English: What was the shape generated in {0}?
                    Question = "Was war bei {0} die generierte Form?",
                },
            },
        },

        // The Deck of Many Things
        [typeof(SDeckOfManyThings)] = new()
        {
            ModuleName = "Der Stapel Vieler Dinge",
            ModuleNameDative = "Stapel Vieler Dinge",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SDeckOfManyThings.FirstCard] = new()
                {
                    // English: What deck did the first card of {0} belong to?
                    Question = "Zu welchem Deck gehörte bei {0} die erste Karte?",
                    Answers = new()
                    {
                        ["Standard"] = "Französisches Blatt",
                        ["Metropolitan"] = "Großstadt",
                        ["Maritime"] = "Schifffahrt",
                        ["Arctic"] = "Arktis",
                        ["Tropical"] = "Tropen",
                        ["Oasis"] = "Oase",
                        ["Celestial"] = "Tarot",
                    },
                },
            },
        },

        // Decolored Squares
        [typeof(SDecoloredSquares)] = new()
        {
            ModuleName = "Entfärbte Felder",
            ModuleNameDative = "Entfärbten Feldern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SDecoloredSquares.StartingPos] = new()
                {
                    // English: What was the starting {1} defining color in {0}?
                    // Example: What was the starting column defining color in Decolored Squares?
                    Question = "Welche Farbe hat bei {0} die {1} bestimmt?",
                    Arguments = new()
                    {
                        ["column"] = "Spalte",
                        ["row"] = "Reihe",
                    },
                    Answers = new()
                    {
                        ["White"] = "Weiß",
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Yellow"] = "Gelb",
                        ["Magenta"] = "Magenta",
                    },
                },
            },
        },

        // Decolour Flash
        [typeof(SDecolourFlash)] = new()
        {
            ModuleName = "Entfärbte Folge",
            ModuleNameDative = "Entfärbten Folge",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SDecolourFlash.Goal] = new()
                {
                    // English: What was the {1} of the {2} goal in {0}?
                    // Example: What was the colour of the first goal in Decolour Flash?
                    Question = "Was war bei {0} {1} vom {2}en Ziel?",
                    Arguments = new()
                    {
                        ["colour"] = "die Farbe",
                        ["word"] = "das Wort",
                    },
                    Answers = new()
                    {
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Red"] = "Rot",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                        ["White"] = "Weiß",
                    },
                },
            },
        },

        // Denial Displays
        [typeof(SDenialDisplays)] = new()
        {
            ModuleName = "Dementi-Displays",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SDenialDisplays.Displays] = new()
                {
                    // English: What number was initially shown on display {1} in {0}?
                    // Example: What number was initially shown on display A in Denial Displays?
                    Question = "Welche Zahl war bei {0} am Anfang auf Display {1}?",
                },
            },
        },

        // DetoNATO
        [typeof(SDetoNATO)] = new()
        {
            Questions = new()
            {
                [SDetoNATO.Display] = new()
                {
                    // English: What was the {1} display in {0}?
                    // Example: What was the first display in DetoNATO?
                    Question = "Was stand bei {0} als {1}es auf dem Display?",
                },
            },
        },

        // Devilish Eggs
        [typeof(SDevilishEggs)] = new()
        {
            ModuleName = "Teufelseier",
            ModuleNameDative = "Teufelseiern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SDevilishEggs.Rotations] = new()
                {
                    // English: What was the {1} egg’s {2} rotation in {0}?
                    // Example: What was the top egg’s first rotation in Devilish Eggs?
                    Question = "Was war bei {0} die {2}e Rotation vom {1} Ei?",
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["bottom"] = "unteren",
                    },
                },
                [SDevilishEggs.Numbers] = new()
                {
                    // English: What was the {1} digit in the string of numbers on {0}?
                    // Example: What was the first digit in the string of numbers on Devilish Eggs?
                    Question = "Was war bei {0} die {1}e Ziffer in dem Zahlensalat?",
                },
                [SDevilishEggs.Letters] = new()
                {
                    // English: What was the {1} letter in the string of letters on {0}?
                    // Example: What was the first letter in the string of letters on Devilish Eggs?
                    Question = "Was war bei {0} der {1}e Buchstabe in dem Buchstabensalat?",
                },
            },
        },

        // Dialtones
        [typeof(SDialtones)] = new()
        {
            ModuleName = "Freizeichen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SDialtones.Dialtones] = new()
                {
                    // English: What dialtones were heard in {0}?
                    Question = "Welches Freizeichen war bei {0} zu hören?",
                },
            },
        },

        // Digisibility
        [typeof(SDigisibility)] = new()
        {
            Questions = new()
            {
                [SDigisibility.DisplayedNumber] = new()
                {
                    // English: What was the number on the {1} button in {0}?
                    // Example: What was the number on the first button in Digisibility?
                    Question = "Welche Zahl war bei {0} auf dem {1}en Knopf?",
                },
            },
        },

        // Digit String
        [typeof(SDigitString)] = new()
        {
            Questions = new()
            {
                [SDigitString.InitialNumber] = new()
                {
                    // English: What was the initial number in {0}?
                    Question = "Was war bei {0} die Anfangszahl?",
                },
            },
        },

        // Dimension Disruption
        [typeof(SDimensionDisruption)] = new()
        {
            ModuleName = "Dimensionsspaltung",
            Questions = new()
            {
                [SDimensionDisruption.VisibleLetters] = new()
                {
                    // English: Which of these was a visible character in {0}?
                    Question = "Welches dieser Zeichen war bei {0} zu sehen?",
                },
            },
        },

        // Directional Button
        [typeof(SDirectionalButton)] = new()
        {
            ModuleName = "Richtungsknopf",
            Questions = new()
            {
                [SDirectionalButton.ButtonCount] = new()
                {
                    // English: How many times did you press the button in the {1} stage of {0}?
                    // Example: How many times did you press the button in the first stage of Directional Button?
                    Question = "Wie oft wurde bei {0} in der {1}en Phase der Knopf gedrückt?",
                },
            },
        },

        // Discolored Squares
        [typeof(SDiscoloredSquares)] = new()
        {
            ModuleName = "Verfärbte Felder",
            ModuleNameDative = "Verfärbten Feldern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SDiscoloredSquares.RememberedPositions] = new()
                {
                    // English: What was {1}’s remembered position in {0}?
                    // Example: What was Blue’s remembered position in Discolored Squares?
                    Question = "Was war bei {0} die notierte Position von {1}?",
                    Arguments = new()
                    {
                        ["Blue"] = "Blau",
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Magenta"] = "Magenta",
                    },
                },
            },
        },

        // Disordered Keys
        [typeof(SDisorderedKeys)] = new()
        {
            Questions = new()
            {
                [SDisorderedKeys.MissingInfo] = new()
                {
                    // English: What was the missing information for the {1} key in {0}?
                    // Example: What was the missing information for the first key in Disordered Keys?
                    Question = "Welche Information fehlte bei {0} auf dem {1}en Knopf?",
                    Answers = new()
                    {
                        ["Key color"] = "Knopffarbe",
                        ["Label color"] = "Aufschriftfarbe",
                        ["Label"] = "Aufschrift",
                    },
                },
                [SDisorderedKeys.UnrevealedKeyColor] = new()
                {
                    // English: What was the unrevealed key color for the {1} key in {0}?
                    // Example: What was the unrevealed key color for the first key in Disordered Keys?
                    Question = "Was war bei {0} die unaufgedeckte Knopffarbe des {1}en Knopfes?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Cyan"] = "Türkis",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                    },
                },
                [SDisorderedKeys.UnrevealedLabelColor] = new()
                {
                    // English: What was the unrevealed label color for the {1} key in {0}?
                    // Example: What was the unrevealed label color for the first key in Disordered Keys?
                    Question = "Was war bei {0} die unaufgedeckte Aufschriftfarbe des {1}en Knopfes?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Cyan"] = "Türkis",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                    },
                },
                [SDisorderedKeys.UnrevealedKeyLabel] = new()
                {
                    // English: What was the unrevealed label for the {1} key in {0}?
                    // Example: What was the unrevealed label for the first key in Disordered Keys?
                    Question = "Was war bei {0} die unaufgedeckte Aufschrift des {1}en Knopfes?",
                },
                [SDisorderedKeys.RevealedKeyColor] = new()
                {
                    // English: What was the revealed key color for the {1} key in {0}?
                    // Example: What was the revealed key color for the first key in Disordered Keys?
                    Question = "Was war bei {0} die aufgedeckte Knopffarbe des {1}en Knopfes?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Cyan"] = "Türkis",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                    },
                },
                [SDisorderedKeys.RevealedLabelColor] = new()
                {
                    // English: What was the revealed label color for the {1} key in {0}?
                    // Example: What was the revealed label color for the first key in Disordered Keys?
                    Question = "Was war bei {0} die aufgedeckte Aufschriftfarbe des {1}en Knopfes?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Cyan"] = "Türkis",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                    },
                },
                [SDisorderedKeys.RevealedLabel] = new()
                {
                    // English: What was the revealed label for the {1} key in {0}?
                    // Example: What was the revealed label for the first key in Disordered Keys?
                    Question = "Was war bei {0} die aufgedeckte Aufschrift des {1}en Knopfes?",
                },
            },
        },

        // Divided Squares
        [typeof(SDividedSquares)] = new()
        {
            ModuleName = "Geteilte Kacheln",
            ModuleNameDative = "Geteilten Kacheln",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SDividedSquares.Color] = new()
                {
                    // English: What color was {1} while pressing it in {0}?
                    // Example: What color was the square while pressing it in Divided Squares?
                    Question = "Welche Farbe hatte bei {0} die {1} beim Gedrückthalten?",
                    Arguments = new()
                    {
                        ["the square"] = "Kachel",
                        ["the correct square"] = "korrekte Kachel",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Black"] = "Schwarz",
                        ["White"] = "Weiß",
                    },
                },
            },
        },

        // Divisible Numbers
        [typeof(SDivisibleNumbers)] = new()
        {
            ModuleName = "Teilbare Zahlen",
            ModuleNameDative = "Teilbaren Zahlen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SDivisibleNumbers.Numbers] = new()
                {
                    // English: What was the {1} stage’s number in {0}?
                    // Example: What was the first stage’s number in Divisible Numbers?
                    Question = "Was war bei {0} die Zahl in der {1}en Phase?",
                },
            },
        },

        // Doofenshmirtz Evil Inc.
        [typeof(SDoofenshmirtzEvilInc)] = new()
        {
            ModuleName = "Doofenshmirtz Gesellschaft mit böswilliger Haftung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SDoofenshmirtzEvilInc.Jingles] = new()
                {
                    // English: What jingle played in {0}?
                    Question = "Welcher Jingle war bei {0} zu hören?",
                },
                [SDoofenshmirtzEvilInc.Inators] = new()
                {
                    // English: Which image was shown in {0}?
                    Question = "Welches Bild war bei {0} zu sehen?",
                },
            },
        },

        // Double Arrows
        [typeof(SDoubleArrows)] = new()
        {
            ModuleName = "Doppelpfeile",
            ModuleNameDative = "Doppelpfeilen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SDoubleArrows.Start] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "Was war bei {0} die Anfangsposition?",
                },
                [SDoubleArrows.Movement] = new()
                {
                    // English: Which direction in the grid did the {1} arrow move in {0}?
                    // Example: Which direction in the grid did the inner up arrow move in Double Arrows?
                    Question = "Welche Bewegungsrichtung im Raster bewirkte bei {0} der {1}?",
                    Arguments = new()
                    {
                        ["inner up"] = "innere Pfeil nach oben",
                        ["inner down"] = "innere Pfeil nach unten",
                        ["inner left"] = "innere Pfeil nach links",
                        ["inner right"] = "innere Pfeil nach rechts",
                        ["outer up"] = "äußere Pfeil nach oben",
                        ["outer down"] = "äußere Pfeil nach unten",
                        ["outer left"] = "äußere Pfeil nach links",
                        ["outer right"] = "äußere Pfeil nach rechts",
                    },
                    Answers = new()
                    {
                        ["Up"] = "Hoch",
                        ["Right"] = "Rechts",
                        ["Left"] = "Links",
                        ["Down"] = "Runter",
                    },
                },
                [SDoubleArrows.Arrow] = new()
                {
                    // English: Which {1} arrow moved {2} in the grid in {0}?
                    // Example: Which inner arrow moved up in the grid in Double Arrows?
                    Question = "Welcher {1} Pfeil ging bei {0} nach {2}?",
                    Arguments = new()
                    {
                        ["inner"] = "innere",
                        ["outer"] = "äußere",
                        ["up"] = "oben",
                        ["down"] = "unten",
                        ["left"] = "links",
                        ["right"] = "rechts",
                    },
                    Answers = new()
                    {
                        ["Up"] = "Hoch",
                        ["Right"] = "Rechts",
                        ["Left"] = "Links",
                        ["Down"] = "Runter",
                    },
                },
            },
        },

        // Double Color
        [typeof(SDoubleColor)] = new()
        {
            ModuleName = "Doppelfarben",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SDoubleColor.Colors] = new()
                {
                    // English: What was the screen color on the {1} stage of {0}?
                    // Example: What was the screen color on the first stage of Double Color?
                    Question = "Was war bei {0} die Farbe des Displays in der {1}en Stufe?",
                    Answers = new()
                    {
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Red"] = "Rot",
                        ["Pink"] = "Pink",
                        ["Yellow"] = "Gelb",
                    },
                },
            },
        },

        // Double Digits
        [typeof(SDoubleDigits)] = new()
        {
            ModuleName = "Doppelziffern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SDoubleDigits.Displays] = new()
                {
                    // English: What was the digit on the {1} display in {0}?
                    // Example: What was the digit on the left display in Double Digits?
                    Question = "Was war bei {0} die Ziffer auf dem {1} Display?",
                    Arguments = new()
                    {
                        ["left"] = "linken",
                        ["right"] = "rechten",
                    },
                },
            },
        },

        // Double Expert
        [typeof(SDoubleExpert)] = new()
        {
            ModuleName = "Doppelexperte",
            ModuleNameDative = "Doppelexperten",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SDoubleExpert.StartingKeyNumber] = new()
                {
                    // English: What was the starting key number in {0}?
                    Question = "Was war bei {0} die anfängliche Schlüsselzahl?",
                },
                [SDoubleExpert.SubmittedWord] = new()
                {
                    // English: What was the word you submitted in {0}?
                    Question = "Was war bei {0} das eingegebene Wort?",
                },
            },
        },

        // Double Listening
        [typeof(SDoubleListening)] = new()
        {
            ModuleName = "Doppelt Zuhören",
            Questions = new()
            {
                [SDoubleListening.Sounds] = new()
                {
                    // English: What clip was played in {0}?
                    Question = "Welcher dieser Clips kam bei {0} vor?",
                },
            },
        },

        // Double-Oh
        [typeof(SDoubleOh)] = new()
        {
            ModuleName = "Doppel-Null",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SDoubleOh.SubmitButton] = new()
                {
                    // English: Which button was the submit button in {0}?
                    Question = "Welcher Knopf war bei {0} der Eingabeknopf?",
                },
            },
        },

        // Double Screen
        [typeof(SDoubleScreen)] = new()
        {
            ModuleName = "Doppel-Display",
            Questions = new()
            {
                [SDoubleScreen.Colors] = new()
                {
                    // English: What color was the {1} screen in the {2} stage of {0}?
                    // Example: What color was the top screen in the first stage of Double Screen?
                    Question = "Welche Farbe war bei {0} auf dem {1} Display in der {2}en Stufe?",
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["bottom"] = "unteren",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                    },
                },
            },
        },

        // Dr. Doctor
        [typeof(SDrDoctor)] = new()
        {
            ModuleName = "Arztpraxis",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SDrDoctor.Diseases] = new()
                {
                    // English: Which of these diseases was listed on {0}, but not the one treated?
                    Question = "Welche dieser Krankheiten war bei {0} aufgelistet, aber nicht die behandelte?",
                },
                [SDrDoctor.Symptoms] = new()
                {
                    // English: Which of these symptoms was listed on {0}?
                    Question = "Welches dieser Symptome war bei {0} aufgelistet?",
                },
            },
        },

        // Dreamcipher
        [typeof(SDreamcipher)] = new()
        {
            ModuleName = "Traumschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SDreamcipher.Word] = new()
                {
                    // English: What was the decrypted word in {0}?
                    Question = "Was war bei {0} das entschlüsselte Wort?",
                },
            },
        },

        // The Duck
        [typeof(SDuck)] = new()
        {
            ModuleName = "Die Ente",
            ModuleNameDative = "Ente",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SDuck.CurtainColor] = new()
                {
                    // English: What was the color of the curtain in {0}?
                    Question = "Welche Farbe hatte bei {0} der Vorhang?",
                    Answers = new()
                    {
                        ["blue"] = "blau",
                        ["yellow"] = "gelb",
                        ["green"] = "grün",
                        ["orange"] = "orange",
                        ["red"] = "rot",
                    },
                },
            },
        },

        // Dumb Waiters
        [typeof(SDumbWaiters)] = new()
        {
            ModuleName = "Speisenaufzug",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SDumbWaiters.PlayerAvailable] = new()
                {
                    // English: Which player {1} present in {0}?
                    // Example: Which player was present in Dumb Waiters?
                    Question = "Welcher Spieler war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["was"] = "dabei",
                        ["was not"] = "nicht dabei",
                    },
                },
            },
        },

        // Earthbound
        [typeof(SEarthbound)] = new()
        {
            Questions = new()
            {
                [SEarthbound.Background] = new()
                {
                    // English: What was the background in {0}?
                    Question = "Was war bei {0} im Hintergrund?",
                },
                [SEarthbound.Monster] = new()
                {
                    // English: Which monster was displayed in {0}?
                    Question = "Welches Monster kam bei {0} vor?",
                },
            },
        },

        // eeB gnillepS
        [typeof(SEeBgnillepS)] = new()
        {
            Questions = new()
            {
                [SEeBgnillepS.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "Welches Wort sollte bei {0} buchstabiert werden?",
                },
            },
        },

        // Eight
        [typeof(SEight)] = new()
        {
            ModuleName = "Acht",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SEight.LastSmallDisplayDigit] = new()
                {
                    // English: What was the last digit on the small display in {0}?
                    Question = "Welche Ziffer war bei {0} auf dem kleineren Display?",
                },
                [SEight.LastBrokenDigitPosition] = new()
                {
                    // English: What was the position of the last broken digit in {0}?
                    Question = "An welcher Stelle war bei {0} die letzte kaputte Ziffer?",
                },
                [SEight.LastResultingDigits] = new()
                {
                    // English: What were the last resulting digits in {0}?
                    Question = "Was waren bei {0} die letzten Ergebnisziffern?",
                },
                [SEight.LastDisplayedNumber] = new()
                {
                    // English: What was the last displayed number in {0}?
                    Question = "Welche Zahl wurde bei {0} zuletzt angezeigt?",
                },
            },
        },

        // Elder Futhark
        [typeof(SElderFuthark)] = new()
        {
            ModuleName = "Älteres Futhark",
            ModuleNameDative = "Älteren Futhark",
            Questions = new()
            {
                [SElderFuthark.Runes] = new()
                {
                    // English: What was the {1} rune shown on {0}?
                    // Example: What was the first rune shown on Elder Futhark?
                    Question = "Was war bei {0} die {1}e Rune?",
                },
            },
        },

        // Emoji
        [typeof(SEmoji)] = new()
        {
            Questions = new()
            {
                [SEmoji.Emoji] = new()
                {
                    // English: What was the {1} emoji in {0}?
                    // Example: What was the left emoji in Emoji?
                    Question = "Welches Emoji war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["left"] = "links",
                        ["right"] = "rechts",
                    },
                },
            },
        },

        // ƎNA Cipher
        [typeof(SEnaCipher)] = new()
        {
            ModuleName = "ƎNA-Chiffre",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SEnaCipher.KeywordAnswer] = new()
                {
                    // English: What was the {1} keyword in {0}?
                    // Example: What was the first keyword in ƎNA Cipher?
                    Question = "Was war bei {0} das {1}e Schlüsselwort?",
                },
                [SEnaCipher.ExtAnswer] = new()
                {
                    // English: What was the transposition key in {0}?
                    Question = "Was war bei {0} der Transpositionsschlüssel?",
                },
                [SEnaCipher.EncryptedAnswer] = new()
                {
                    // English: What was the encrypted word in {0}?
                    Question = "Was war bei {0} das verschlüsselte Wort?",
                },
            },
        },

        // Encrypted Dice
        [typeof(SEncryptedDice)] = new()
        {
            ModuleName = "Verschlüsselte Würfel",
            ModuleNameDative = "Verschlüsselten Würfel",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SEncryptedDice.Question] = new()
                {
                    // English: Which of these numbers appeared on a die in the {1} stage of {0}?
                    // Example: Which of these numbers appeared on a die in the first stage of Encrypted Dice?
                    Question = "Welche dieser Zahlen war bei {0} auf einem der Würfel in der {1}en Stufe zu sehen?",
                },
            },
        },

        // Encrypted Equations
        [typeof(SEncryptedEquations)] = new()
        {
            ModuleName = "Verschlüsselte Gleichungen",
            ModuleNameDative = "Verschlüsselten Gleichungen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SEncryptedEquations.Shapes] = new()
                {
                    // English: Which shape was the {1} operand in {0}?
                    // Example: Which shape was the first operand in Encrypted Equations?
                    Question = "Welche Form hatte bei {0} der {1} Operand?",
                },
            },
        },

        // Encrypted Hangman
        [typeof(SEncryptedHangman)] = new()
        {
            ModuleName = "Hangman Verschlüsselt",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SEncryptedHangman.Module] = new()
                {
                    // English: What module name was encrypted by {0}?
                    Question = "Welcher Modulname kam bei {0} verschlüsselt vor?",
                },
                [SEncryptedHangman.EncryptionMethod] = new()
                {
                    // English: What method of encryption was used by {0}?
                    Question = "Welche Verschlüsselungsmethode kam bei {0} zum Einsatz?",
                    Answers = new()
                    {
                        ["Caesar Cipher"] = "Cäsar-Chiffre",
                        ["Atbash Cipher"] = "Atbash-Chiffre",
                        ["Rot-13 Cipher"] = "Rot-13-Chiffre",
                        ["Affine Cipher"] = "Affine Chiffre",
                        ["Modern Cipher"] = "Moderne Chiffre",
                        ["Vigenère Cipher"] = "Vigenère-Chiffre",
                        ["Playfair Cipher"] = "Playfair-Chiffre",
                    },
                },
            },
        },

        // Encrypted Maze
        [typeof(SEncryptedMaze)] = new()
        {
            ModuleName = "Verschlüsseltes Labyrinth",
            ModuleNameDative = "Verschlüsselten Labyrinth",
            Questions = new()
            {
                [SEncryptedMaze.Symbols] = new()
                {
                    // English: Which symbol on {0} was spinning {1}?
                    // Example: Which symbol on Encrypted Maze was spinning clockwise?
                    Question = "Welches Symbol bei {0} hat sich {1} gedreht?",
                    Arguments = new()
                    {
                        ["clockwise"] = "im Uhrzeigersinn",
                        ["counter-clockwise"] = "gegen den Uhrzeigersinn",
                    },
                },
            },
        },

        // Encrypted Morse
        [typeof(SEncryptedMorse)] = new()
        {
            ModuleName = "Verschlüsselte Morsezeichen",
            ModuleNameDative = "Verschlüsselten Morsezeichen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SEncryptedMorse.CallResponse] = new()
                {
                    // English: What was the {1} on {0}?
                    // Example: What was the received call on Encrypted Morse?
                    Question = "Was war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["received call"] = "die empfangene Botschaft",
                        ["sent response"] = "die gesendete Antwort",
                    },
                },
            },
        },

        // Encryption Bingo
        [typeof(SEncryptionBingo)] = new()
        {
            ModuleName = "Verschlüsselungsbingo",
            Questions = new()
            {
                [SEncryptionBingo.Encoding] = new()
                {
                    // English: What was the first encoding used in {0}?
                    Question = "Welche Verschlüsselung kam bei {0} als erste zum Einsatz?",
                    Answers = new()
                    {
                        ["Morse Code"] = "Morsezeichen",
                        ["Tap Code"] = "Klopfzeichen",
                        ["Maritime Flags"] = "Schiffsignalflaggen",
                        ["Semaphore"] = "Winkeralphabet",
                        ["Pigpen"] = "Freimaurer",
                        ["Lombax"] = "Lombax",
                        ["Braille"] = "Blindenschrift",
                        ["Wingdings"] = "Wingdings",
                        ["Zoni"] = "Zoni",
                        ["Galactic Alphabet"] = "Galaktisches Alphabet",
                        ["Arrow"] = "Pfeile",
                        ["Listening"] = "Zuhören",
                        ["Regular Number"] = "Normale Zahl",
                        ["Chinese Number"] = "Chinesische Zahl",
                        ["Cube Symbols"] = "Würfel-Symbole",
                        ["Runes"] = "Runen",
                        ["New York Point"] = "New York Point",
                        ["Fontana"] = "Fontana",
                        ["ASCII Hex Code"] = "ASCII (hexadezimal)",
                    },
                },
            },
        },

        // English Entries
        [typeof(SEnglishEntries)] = new()
        {
            Questions = new()
            {
                [SEnglishEntries.Display] = new()
                {
                    // English: What was the displayed quote on {0}?
                    Question = "Welches Zitat kam bei {0} vor?",
                },
            },
        },

        // Enigma Cycle
        [typeof(SEnigmaCycle)] = new()
        {
            ModuleName = "Enigma-Schiffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SEnigmaCycle.DialDirectionsThree] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Enigma Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}e Zeiger?",
                },
                [SEnigmaCycle.DialDirectionsTwelve] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Enigma Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}e Zeiger?",
                },
                [SEnigmaCycle.DialDirectionsEight] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Enigma Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}e Zeiger?",
                },
                [SEnigmaCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Enigma Cycle?
                    Question = "Welcher Buchstabe stand bei {0} auf dem {1}en Zeiger?",
                },
            },
            Discriminators = new()
            {
                [SEnigmaCycle.LabelDiscriminator] = new()
                {
                    // English: the Enigma Cycle that had the letter {0} on a dial
                    // Example: the Enigma Cycle that had the letter A on a dial
                    Discriminator = "der Enigma-Schiffer, bei der der Buchstabe {0} vorkam,",
                },
            },
        },

        // Entry Number Four
        [typeof(SEntryNumberFour)] = new()
        {
            ModuleName = "Eintrag Nummer Vier",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SEntryNumberFour.Digits] = new()
                {
                    // English: What was the {1} digit in the {2} number shown in {0}?
                    // Example: What was the first digit in the first number shown in Entry Number Four?
                    Question = "Was war bei {0} die {1}e Ziffer der {2}en Zahl?",
                },
            },
        },

        // Entry Number One
        [typeof(SEntryNumberOne)] = new()
        {
            ModuleName = "Eintrag Nummer Eins",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SEntryNumberOne.Digits] = new()
                {
                    // English: What was the {1} digit in the {2} number shown in {0}?
                    // Example: What was the first digit in the first number shown in Entry Number One?
                    Question = "Was war bei {0} die {1}e Ziffer der {2}en Zahl?",
                },
            },
        },

        // Equations X
        [typeof(SEquationsX)] = new()
        {
            ModuleName = "X-Gleichungen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SEquationsX.Symbols] = new()
                {
                    // English: What was the displayed symbol in {0}?
                    Question = "Welches Symbol kam bei {0} vor?",
                },
            },
        },

        // Error Codes
        [typeof(SErrorCodes)] = new()
        {
            ModuleName = "Fehlercodes",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SErrorCodes.ActiveError] = new()
                {
                    // English: What was the active error code in {0}?
                    Question = "Was war bei {0} der aktive Fehlercode?",
                },
            },
        },

        // Etterna
        [typeof(SEtterna)] = new()
        {
            Questions = new()
            {
                [SEtterna.Number] = new()
                {
                    // English: What was the beat for the {1} arrow from the bottom in {0}?
                    // Example: What was the beat for the first arrow from the bottom in Etterna?
                    Question = "Welcher Beat entsprach bei {0} dem {1}en Pfeil von unten?",
                },
            },
        },

        // Exoplanets
        [typeof(SExoplanets)] = new()
        {
            ModuleName = "Exoplaneten",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SExoplanets.StartingTargetPlanet] = new()
                {
                    // English: What was the starting target planet in {0}?
                    Question = "Was war bei {0} am Anfang der Zielplanet?",
                    Answers = new()
                    {
                        ["outer"] = "der äußere",
                        ["middle"] = "der mittlere",
                        ["inner"] = "der innere",
                        ["none"] = "keiner",
                    },
                },
                [SExoplanets.StartingTargetDigit] = new()
                {
                    // English: What was the starting target digit in {0}?
                    Question = "Was war bei {0} am Anfang die Zielziffer?",
                },
                [SExoplanets.TargetPlanet] = new()
                {
                    // English: What was the final target planet in {0}?
                    Question = "Was war bei {0} am Ende der Zielplanet?",
                    Answers = new()
                    {
                        ["outer"] = "der äußere",
                        ["middle"] = "der mittlere",
                        ["inner"] = "der innere",
                        ["none"] = "keiner",
                    },
                },
                [SExoplanets.TargetDigit] = new()
                {
                    // English: What was the final target digit in {0}?
                    Question = "Was war bei {0} am Ende die Zielziffer?",
                },
            },
        },

        // Factoring Maze
        [typeof(SFactoringMaze)] = new()
        {
            ModuleName = "Faktorlabyrinth",
            Questions = new()
            {
                [SFactoringMaze.ChosenPrimes] = new()
                {
                    // English: What was one of the prime numbers chosen in {0}?
                    Question = "Welche Primzahl kam bei {0} zum Einsatz?",
                },
            },
        },

        // Factory Maze
        [typeof(SFactoryMaze)] = new()
        {
            ModuleName = "Fabriklabyrinth",
            Questions = new()
            {
                [SFactoryMaze.StartRoom] = new()
                {
                    // English: What room did you start in in {0}?
                    Question = "Wo ging’s bei {0} los?",
                },
            },
        },

        // Faerie Fires
        [typeof(SFaerieFires)] = new()
        {
            ModuleName = "Feenfeuer",
            Questions = new()
            {
                [SFaerieFires.Color] = new()
                {
                    // English: What color was the {1} faerie in {0}?
                    // Example: What color was the first faerie in Faerie Fires?
                    Question = "Welche Farbe hatte bei {0} die {1}e Fee?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Yellow"] = "Gelb",
                        ["Cyan"] = "Türkis",
                        ["Magenta"] = "Rosa",
                    },
                },
                [SFaerieFires.PitchOrdinal] = new()
                {
                    // English: What pitch did the {1} faerie sing in {0}?
                    // Example: What pitch did the first faerie sing in Faerie Fires?
                    Question = "In welcher Tonhöhe sang bei {0} die {1}e Fee?",
                },
                [SFaerieFires.PitchColor] = new()
                {
                    // English: What pitch did the {1} faerie sing in {0}?
                    // Example: What pitch did the red faerie sing in Faerie Fires?
                    Question = "In welcher Tonhöhe sang bei {0} die {1} Fee?",
                    Arguments = new()
                    {
                        ["red"] = "rote",
                        ["green"] = "grüne",
                        ["blue"] = "blaue",
                        ["yellow"] = "gelbe",
                        ["cyan"] = "türkise",
                        ["magenta"] = "rosane",
                    },
                },
            },
        },

        // Fast Math
        [typeof(SFastMath)] = new()
        {
            ModuleName = "Mathe in Zeitraffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SFastMath.LastLetters] = new()
                {
                    // English: What was the last pair of letters in {0}?
                    Question = "Was waren bei {0} die letzten zwei Buchstaben?",
                },
            },
        },

        // Fast Playfair Cipher
        [typeof(SFastPlayfairCipher)] = new()
        {
            ModuleName = "Playfair-Chiffre im Zeitraffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SFastPlayfairCipher.LastMessage] = new()
                {
                    // English: What was the last displayed message in {0}?
                    Question = "Was war bei {0} die letzte Botschaft?",
                },
            },
        },

        // Faulty Buttons
        [typeof(SFaultyButtons)] = new()
        {
            ModuleName = "Defekte Knöpfe",
            ModuleNameDative = "Defekten Knöpfen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SFaultyButtons.ReferredToThisButton] = new()
                {
                    // English: Which button referred to this button in {0}? (+ sprite)
                    // Example: Which button referred to this button in Faulty Buttons? (+ sprite)
                    Question = "Welcher Knopf nahm bei {0} Bezug auf diesen Knopf?",
                },
                [SFaultyButtons.ThisButtonReferredTo] = new()
                {
                    // English: Which button did this button refer to in {0}? (+ sprite)
                    // Example: Which button did this button refer to in Faulty Buttons? (+ sprite)
                    Question = "Auf welchen Knopf nahm bei {0} dieser Knopf Bezug?",
                },
            },
        },

        // Faulty RGB Maze
        [typeof(SFaultyRGBMaze)] = new()
        {
            ModuleName = "Defektes RGB-Labyrinth",
            ModuleNameDative = "Defekten RGB-Labyrinth",
            Questions = new()
            {
                [SFaultyRGBMaze.Keys] = new()
                {
                    // English: Where was the {1} key in {0}?
                    // Example: Where was the red key in Faulty RGB Maze?
                    Question = "Wo war bei {0} der {1}e Schlüssel?",
                    Arguments = new()
                    {
                        ["red"] = "rote",
                        ["green"] = "grüne",
                        ["blue"] = "blaue",
                    },
                },
                [SFaultyRGBMaze.Number] = new()
                {
                    // English: Which maze number was the {1} maze in {0}?
                    // Example: Which maze number was the red maze in Faulty RGB Maze?
                    Question = "Welche Zahl hatte bei {0} das {1} Labyrinth?",
                    Arguments = new()
                    {
                        ["red"] = "rote",
                        ["green"] = "grüne",
                        ["blue"] = "blaue",
                    },
                },
                [SFaultyRGBMaze.Exit] = new()
                {
                    // English: What was the exit coordinate in {0}?
                    Question = "Was war bei {0} die Koordinate des Ausgangs?",
                },
            },
        },

        // Find The Date
        [typeof(SFindTheDate)] = new()
        {
            ModuleName = "Datumsfindung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SFindTheDate.Month] = new()
                {
                    // English: What was the month displayed in the {1} stage of {0}?
                    // Example: What was the month displayed in the first stage of Find The Date?
                    Question = "Was war bei {0} der Monat in der {1}en Stufe?",
                },
                [SFindTheDate.Day] = new()
                {
                    // English: What was the day displayed in the {1} stage of {0}?
                    // Example: What was the day displayed in the first stage of Find The Date?
                    Question = "Was war bei {0} der Tag in der {1}en Stufe?",
                },
                [SFindTheDate.Year] = new()
                {
                    // English: What was the year displayed in the {1} stage of {0}?
                    // Example: What was the year displayed in the first stage of Find The Date?
                    Question = "Was war bei {0} das Jahr in der {1}en Stufe?",
                },
            },
        },

        // Five Letter Words
        [typeof(SFiveLetterWords)] = new()
        {
            ModuleName = "Wörter aus fünf Buchstaben",
            ModuleNameDative = "Wörtern aus fünf Buchstaben",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SFiveLetterWords.DisplayedWords] = new()
                {
                    // English: Which of these words was on the display in {0}?
                    Question = "Welches dieser Wörter kam bei {0} auf dem Display vor?",
                },
            },
        },

        // FizzBuzz
        [typeof(SFizzBuzz)] = new()
        {
            Questions = new()
            {
                [SFizzBuzz.DisplayedNumbers] = new()
                {
                    // English: What was the {1} digit on the {2} display of {0}?
                    // Example: What was the first digit on the top display of FizzBuzz?
                    Question = "Was war bei {0} die {1}e Ziffer auf dem {2} Display?",
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["middle"] = "mittleren",
                        ["bottom"] = "unteren",
                    },
                },
            },
        },

        // Flags
        [typeof(SFlags)] = new()
        {
            ModuleName = "Flaggen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SFlags.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Was war bei {0} die Zahl auf dem Display?",
                },
                [SFlags.MainCountry] = new()
                {
                    // English: What was the main country flag in {0}?
                    Question = "Was war bei {0} die Primärflagge?",
                },
                [SFlags.Countries] = new()
                {
                    // English: Which of these country flags was shown, but not the main country flag, in {0}?
                    Question = "Welche Flagge kam bei {0} vor, war aber nicht die Primärflagge?",
                },
            },
        },

        // Flashing Arrows
        [typeof(SFlashingArrows)] = new()
        {
            ModuleName = "Blinkende Pfeile",
            ModuleNameDative = "Blinkenden Pfeilen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SFlashingArrows.DisplayedValue] = new()
                {
                    // English: What number was displayed on {0}?
                    Question = "Welche Zahl war bei {0} auf dem Display?",
                },
                [SFlashingArrows.ReferredArrow] = new()
                {
                    // English: What color flashed {1} black on the relevant arrow in {0}?
                    // Example: What color flashed before black on the relevant arrow in Flashing Arrows?
                    Question = "Welche Farbe kam bei {0} {1} Schwarz auf dem entsprechenden Pfeil?",
                    Arguments = new()
                    {
                        ["before"] = "vor",
                        ["after"] = "nach",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                        ["White"] = "Weiß",
                    },
                },
            },
        },

        // Flashing Lights
        [typeof(SFlashingLights)] = new()
        {
            ModuleName = "Blinkende Lichter",
            ModuleNameDative = "Blinkenden Lichtern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SFlashingLights.LEDFrequency] = new()
                {
                    // English: How many times did the {1} LED flash {2} on {0}?
                    // Example: How many times did the top LED flash cyan on Flashing Lights?
                    Question = "Wie oft ist bei {0} die {1} LED {2} aufgeleuchtet?",
                    Arguments = new()
                    {
                        ["top"] = "obere",
                        ["bottom"] = "untere",
                        ["cyan"] = "türkis",
                        ["green"] = "grün",
                        ["red"] = "rot",
                        ["purple"] = "lila",
                        ["orange"] = "orange",
                    },
                },
            },
        },

        // Flavor Text
        [typeof(SFlavorText)] = new()
        {
            ModuleName = "Flairtext",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SFlavorText.Module] = new()
                {
                    // English: Which module’s flavor text was shown in {0}?
                    Question = "Der Flairtext welches Moduls kam bei {0} vor?",
                },
            },
        },

        // Flavor Text EX
        [typeof(SFlavorTextEX)] = new()
        {
            ModuleName = "Flairtext EX",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SFlavorTextEX.Module] = new()
                {
                    // English: Which module’s flavor text was shown in the {1} stage of {0}?
                    // Example: Which module’s flavor text was shown in the first stage of Flavor Text EX?
                    Question = "Der Flairtext welches Moduls kam bei {0} in der {1}en Stufe vor?",
                },
            },
        },

        // Flyswatting
        [typeof(SFlyswatting)] = new()
        {
            ModuleName = "Fliegenschlagen",
            Questions = new()
            {
                [SFlyswatting.Unpressed] = new()
                {
                    // English: Which fly was present, but not in the solution in {0}?
                    Question = "Welche Fliege war bei {0} anwesend, aber nicht Teil der Lösung?",
                },
            },
        },

        // Follow Me
        [typeof(SFollowMe)] = new()
        {
            ModuleName = "Folge Mir",
            Questions = new()
            {
                [SFollowMe.DisplayedPath] = new()
                {
                    // English: What was the {1} flashing direction in {0}?
                    // Example: What was the first flashing direction in Follow Me?
                    Question = "Welche Richtung ist bei {0} als {1}e aufgeleuchtet?",
                    Answers = new()
                    {
                        ["Up"] = "Hoch",
                        ["Down"] = "Runter",
                        ["Left"] = "Links",
                        ["Right"] = "Rechts",
                    },
                },
            },
        },

        // Forest Cipher
        [typeof(SForestCipher)] = new()
        {
            ModuleName = "Wald-Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SForestCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Forest Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "ober",
                        ["middle"] = "mittler",
                        ["bottom"] = "unter",
                    },
                },
            },
        },

        // Forget Any Color
        [typeof(SForgetAnyColor)] = new()
        {
            ModuleName = "Vergiss Jede Farbe",
            Questions = new()
            {
                [SForgetAnyColor.QCylinder] = new()
                {
                    // English: What colors were the cylinders during the {1} stage of {0}?
                    // Example: What colors were the cylinders during the first stage of Forget Any Color?
                    Question = "Was waren bei {0} die Zylinderfarben in der {1}en Stufe?",
                    // Refer to translations.md to understand the weird strings
                    Additional = new()
                    {
                        ["{0}, {1}, {2}"] = "{0}, {1}, {2}",
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Cyan"] = "Türkis",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                        ["White"] = "Weiß",
                        ["L"] = "L",
                        ["M"] = "M",
                        ["R"] = "R",
                    },
                },
                [SForgetAnyColor.QFigure] = new()
                {
                    // English: Which figure was used during the {1} stage of {0}?
                    // Example: Which figure was used during the first stage of Forget Any Color?
                    Question = "Welche Figur kam bei {0} in der {1}en Stufe zum Einsatz?",
                },
            },
            Discriminators = new()
            {
                [SForgetAnyColor.DCylinder] = new()
                {
                    // English: the Forget Any Color whose cylinders in the {1} stage were {0}
                    // Example: the Forget Any Color whose cylinders in the first stage were Orange, Yellow, Green
                    Discriminator = "dem Vergiss Jede Farbe, in dessen {1}er Stufe die Zylinderfarben {0} waren,",
                },
                [SForgetAnyColor.DFigure] = new()
                {
                    // English: the Forget Any Color which used figure {0} in the {1} stage
                    // Example: the Forget Any Color which used figure LLLMR in the first stage
                    Discriminator = "dem Vergiss Jede Farbe, in dessen {1}er Stufe die Figur {0} verwendet wurde,",
                },
            },
        },

        // Forget Everything
        [typeof(SForgetEverything)] = new()
        {
            ModuleName = "Vergiss Alles",
            Questions = new()
            {
                [SForgetEverything.QStageOneDisplay] = new()
                {
                    // English: What was the {1} displayed digit in the first stage of {0}?
                    // Example: What was the first displayed digit in the first stage of Forget Everything?
                    Question = "Was war bei {0} die {1}e Ziffer in der ersten Stufe?",
                },
            },
            Discriminators = new()
            {
                [SForgetEverything.DStageOneDisplay] = new()
                {
                    // English: the Forget Everything whose {0} displayed digit in that stage was {1}
                    // Example: the Forget Everything whose first displayed digit in that stage was 1
                    Discriminator = "dem Vergiss Alles, dessen {0}e Ziffer in der ersten Stufe {1} war,",
                },
            },
        },

        // Forget Me
        [typeof(SForgetMe)] = new()
        {
            ModuleName = "Vergiss Mein",
            Questions = new()
            {
                [SForgetMe.InitialState] = new()
                {
                    // English: What number was in the {1} position of the initial puzzle in {0}?
                    // Example: What number was in the top-left position of the initial puzzle in Forget Me?
                    Question = "Welche Zahl war bei {0} am Anfang {1}?",
                    Arguments = new()
                    {
                        ["top-left"] = "oben links",
                        ["top-middle"] = "oben Mitte",
                        ["top-right"] = "oben rechts",
                        ["middle-left"] = "Mitte links",
                        ["center"] = "Mitte",
                        ["middle-right"] = "Mitte rechts",
                        ["bottom-left"] = "unten links",
                        ["bottom-middle"] = "unten Mitte",
                        ["bottom-right"] = "unten rechts",
                    },
                },
            },
        },

        // Forget Me Not
        [typeof(SForgetMeNot)] = new()
        {
            ModuleName = "Vergiss Mein Nicht",
            Questions = new()
            {
                [SForgetMeNot.Question] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Forget Me Not?
                    Question = "Welche Ziffer wurde bei {0} in der {1}en Stufe angezeigt?",
                },
            },
            Discriminators = new()
            {
                [SForgetMeNot.Discriminator] = new()
                {
                    // English: the Forget Me Not which displayed a {0} in the {1} stage
                    // Example: the Forget Me Not which displayed a 1 in the first stage
                    Discriminator = "dem Vergiss Mein Nicht, in dessen {1}er Stufe {0} angezeigt wurde,",
                },
            },
        },

        // Forget Me Now
        [typeof(SForgetMeNow)] = new()
        {
            ModuleName = "Vergiss Mein Jetzt",
            Questions = new()
            {
                [SForgetMeNow.DisplayedDigits] = new()
                {
                    // English: What was the {1} displayed digit in {0}?
                    // Example: What was the first displayed digit in Forget Me Now?
                    Question = "Was war bei {0} die Ziffer in der {1}en Stufe?",
                },
            },
        },

        // Forget Our Voices
        [typeof(SForgetOurVoices)] = new()
        {
            ModuleName = "Vergiss Unsere Stimmen",
            Questions = new()
            {
                [SForgetOurVoices.Voice] = new()
                {
                    // English: What was played in the {1} stage of {0}?
                    // Example: What was played in the first stage of Forget Our Voices?
                    Question = "Was war bei {0} in der {1}en Stufe zu hören?",
                },
            },
            Discriminators = new()
            {
                [SForgetOurVoices.Discriminator] = new()
                {
                    // English: the Forget Our Voices which played a {0} in {1}’s voice in the {2} stage
                    // Example: the Forget Our Voices which played a 1 in Umbra Moruka’s voice in the first stage
                    Discriminator = "dem Vergiss Unsere Stimmen, bei dem in der {2}en Stufe eine {0} in {1}s Stimme zu hören war,",
                },
            },
        },

        // Forget’s Ultimate Showdown
        [typeof(SForgetsUltimateShowdown)] = new()
        {
            Questions = new()
            {
                [SForgetsUltimateShowdown.Answer] = new()
                {
                    // English: What was the {1} digit of the answer in {0}?
                    // Example: What was the first digit of the answer in Forget’s Ultimate Showdown?
                    Question = "Was war bei {0} die {1}e Ziffer in der Lösung?",
                },
                [SForgetsUltimateShowdown.Bottom] = new()
                {
                    // English: What was the {1} digit of the bottom number in {0}?
                    // Example: What was the first digit of the bottom number in Forget’s Ultimate Showdown?
                    Question = "Was war bei {0} die {1}e Ziffer in der unteren Zahl?",
                },
                [SForgetsUltimateShowdown.Initial] = new()
                {
                    // English: What was the {1} digit of the initial number in {0}?
                    // Example: What was the first digit of the initial number in Forget’s Ultimate Showdown?
                    Question = "Was war bei {0} die {1}e Ziffer in der Anfangszahl?",
                },
                [SForgetsUltimateShowdown.Method] = new()
                {
                    // English: What was the {1} method used in {0}?
                    // Example: What was the first method used in Forget’s Ultimate Showdown?
                    Question = "Was war bei {0} die als {1}e verwendete Methode?",
                    Answers = new()
                    {
                        ["Forget Me Not"] = "Vergiss Mein Nicht",
                        ["Simon’s Stages"] = "Simons Stufen",
                        ["Forget Me Later"] = "Vergiss Mein Später",
                        ["Forget Infinity"] = "Vergiss Unendlich",
                        ["A>N<D"] = "A>N<D",
                        ["Forget Me Now"] = "Vergiss Mein Jetzt",
                        ["Forget Everything"] = "Vergiss Alles",
                        ["Forget Us Not"] = "Vergiss Uns Nicht",
                    },
                },
            },
        },

        // Forget The Colors
        [typeof(SForgetTheColors)] = new()
        {
            ModuleName = "Vergiss Die Farben",
            Questions = new()
            {
                [SForgetTheColors.QGearNumber] = new()
                {
                    // English: What number was on the gear during stage {1} of {0}?
                    // Example: What number was on the gear during stage 0 of Forget The Colors?
                    Question = "Welche Zahl war bei {0} in Stufe {1} auf dem Zahnrad?",
                },
                [SForgetTheColors.QLargeDisplay] = new()
                {
                    // English: What number was on the large display during stage {1} of {0}?
                    // Example: What number was on the large display during stage 0 of Forget The Colors?
                    Question = "Welche Zahl war bei {0} in Stufe {1} auf dem großen Display?",
                },
                [SForgetTheColors.QSineNumber] = new()
                {
                    // English: What was the last decimal in the sine number received during stage {1} of {0}?
                    // Example: What was the last decimal in the sine number received during stage 0 of Forget The Colors?
                    Question = "Was war bei {0} die letzte Ziffer in der in Stufe {1} erhaltenen Sinuszahl?",
                },
                [SForgetTheColors.QGearColor] = new()
                {
                    // English: What color was the gear during stage {1} of {0}?
                    // Example: What color was the gear during stage 0 of Forget The Colors?
                    Question = "Welche Farbe hatte bei {0} das Zahnrad in Stufe {1}?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Cyan"] = "Türkis",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                        ["Pink"] = "Pink",
                        ["Maroon"] = "Kastanie",
                        ["White"] = "Weiß",
                        ["Gray"] = "Grau",
                    },
                },
                [SForgetTheColors.QRuleColor] = new()
                {
                    // English: Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?
                    // Example: Which edgework-based rule was applied to the sum of nixies and gear during stage 0 of Forget The Colors?
                    Question = "Welche peripheriebasierte Regel wurde bei {0} in Stufe {1} auf die Summe der Nixies und des Zahnrads angewandt?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Cyan"] = "Türkis",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                        ["Pink"] = "Pink",
                        ["Maroon"] = "Kastanie",
                        ["White"] = "Weiß",
                        ["Gray"] = "Grau",
                    },
                },
            },
            Discriminators = new()
            {
                [SForgetTheColors.DGearNumber] = new()
                {
                    // English: the Forget The Colors whose gear number was {0} in stage {1}
                    // Example: the Forget The Colors whose gear number was 1 in stage 1
                    Discriminator = "dem Vergiss Die Farben, dessen Zahnradzahl in Stufe {1} {0} war,",
                },
                [SForgetTheColors.DLargeDisplay] = new()
                {
                    // English: the Forget The Colors which had {0} on its large display in stage {1}
                    // Example: the Forget The Colors which had 426 on its large display in stage 1
                    Discriminator = "dem Vergiss Die Farben, dessen großes Display in Stufe {1} {0} anzeigte,",
                },
                [SForgetTheColors.DSineNumber] = new()
                {
                    // English: the Forget The Colors whose received sine number in stage {1} ended with a {0}
                    // Example: the Forget The Colors whose received sine number in stage 1 ended with a 0
                    Discriminator = "dem Vergiss Die Farben, dessen erhaltene Sinuszahl in Stufe {1} auf {0} endete,",
                },
                [SForgetTheColors.DColor] = new()
                {
                    // English: the Forget The Colors whose {2} was {0} in stage {1}
                    // Example: the Forget The Colors whose gear color was Red in stage 1
                    Discriminator = "dem Vergiss Die Farben, bei dem {2} in Stufe {1} {0} war,",
                    Arguments = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Cyan"] = "Türkis",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                        ["Pink"] = "Pink",
                        ["Maroon"] = "Kastanie",
                        ["White"] = "Weiß",
                        ["gear color"] = "die Zahnradfarbe",
                        ["rule color"] = "die Regelfarbe",
                    },
                },
            },
        },

        // Forget This
        [typeof(SForgetThis)] = new()
        {
            ModuleName = "Vergiss Dies",
            Questions = new()
            {
                [SForgetThis.QColors] = new()
                {
                    // English: What color was the LED in the {1} stage of {0}?
                    // Example: What color was the LED in the first stage of Forget This?
                    Question = "Welche Farbe hatte bei {0} die LED in Stufe {1}?",
                    Answers = new()
                    {
                        ["Cyan"] = "Türkis",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                        ["Black"] = "Schwarz",
                        ["White"] = "Weiß",
                        ["Green"] = "Grün",
                    },
                },
                [SForgetThis.QDigits] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Forget This?
                    Question = "Welche Ziffer wurde bei {0} in der {1}en Stufe angezeigt?",
                },
            },
            Discriminators = new()
            {
                [SForgetThis.DColors] = new()
                {
                    // English: the Forget This whose LED was {0} in the {1} stage
                    // Example: the Forget This whose LED was cyan in the first stage
                    Discriminator = "dem Vergiss Dies, dessen LED in der {1}en Stufe {0} war,",
                    Arguments = new()
                    {
                        ["cyan"] = "türkis",
                        ["magenta"] = "magenta",
                        ["yellow"] = "gelb",
                        ["black"] = "schwarz",
                        ["white"] = "weiß",
                        ["green"] = "grün",
                    },
                },
                [SForgetThis.DDigits] = new()
                {
                    // English: the Forget This which displayed {0} in the {1} stage
                    // Example: the Forget This which displayed A in the first stage
                    Discriminator = "dem Vergiss Dies, das in der {1}en Stufe {0} anzeigte,",
                },
            },
        },

        // Forget Us Not
        [typeof(SForgetUsNot)] = new()
        {
            ModuleName = "Vergiss Uns Nicht",
            Questions = new()
            {
                [SForgetUsNot.Stage] = new()
                {
                    // English: Which module name was used for stage {1} in {0}?
                    // Example: Which module name was used for stage 1 in Forget Us Not?
                    Question = "Welcher Modulname kam bei {0} in Stufe {1} zum Einsatz?",
                },
            },
            Discriminators = new()
            {
                [SForgetUsNot.Discriminator] = new()
                {
                    // English: the Forget Us Not in which {0} was used for stage {1}
                    // Example: the Forget Us Not in which Memory was used for stage 1
                    Discriminator = "dem Vergiss Uns Nicht, bei dem {0} in Stufe {1} zum Einsatz kam,",
                },
            },
        },

        // Free Parking
        [typeof(SFreeParking)] = new()
        {
            ModuleName = "Frei Parken",
            Questions = new()
            {
                [SFreeParking.Token] = new()
                {
                    // English: What was the player token in {0}?
                    Question = "Was war bei {0} die Spielfigur?",
                    Answers = new()
                    {
                        ["Dog"] = "Hund",
                        ["Wheelbarrow"] = "Schubkarre",
                        ["Cat"] = "Katze",
                        ["Iron"] = "Bügeleisen",
                        ["Top Hat"] = "Zylinderhut",
                        ["Car"] = "Auto",
                        ["Battleship"] = "Schlachtschiff",
                    },
                },
            },
        },

        // Functions
        [typeof(SFunctions)] = new()
        {
            ModuleName = "Funktionen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SFunctions.LastDigit] = new()
                {
                    // English: What was the last digit of your first query’s result in {0}?
                    Question = "Was war bei {0} die letzte Ziffer des ersten Abfrageergebnisses?",
                },
                [SFunctions.LeftNumber] = new()
                {
                    // English: What number was to the left of the displayed letter in {0}?
                    Question = "Welche Zahl war bei {0} auf dem Display links vom Buchstaben?",
                },
                [SFunctions.Letter] = new()
                {
                    // English: What letter was displayed in {0}?
                    Question = "Welcher Buchstabe war bei {0} auf dem Display?",
                },
                [SFunctions.RightNumber] = new()
                {
                    // English: What number was to the right of the displayed letter in {0}?
                    Question = "Welche Zahl war bei {0} auf dem Display rechts vom Buchstaben?",
                },
            },
        },

        // The Fuse Box
        [typeof(SFuseBox)] = new()
        {
            ModuleName = "Sicherungskasten",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SFuseBox.Flashes] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "Welche Farbe ist bei {0} als {1}e vorgekommen?",
                },
                [SFuseBox.Arrows] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "Welcher Pfeil war bei {0} der {1}e?",
                },
            },
        },

        // Gadgetron Vendor
        [typeof(SGadgetronVendor)] = new()
        {
            ModuleName = "Gadgetron-Verkäufer",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SGadgetronVendor.CurrentWeapon] = new()
                {
                    // English: What was your current weapon in {0}?
                    Question = "Was war bei {0} deine aktuelle Waffe?",
                },
                [SGadgetronVendor.WeaponForSale] = new()
                {
                    // English: What was the weapon up for sale in {0}?
                    Question = "Welche Waffe wurde bei {0} zum Verkauf angeboten?",
                },
            },
        },

        // Game of Life Cruel
        [typeof(SGameOfLifeCruel)] = new()
        {
            ModuleName = "Höllisches Spiel des Lebens",
            ModuleNameDative = "Höllischen Spiel des Lebens",
            Questions = new()
            {
                [SGameOfLifeCruel.Colors] = new()
                {
                    // English: Which of these was a color combination that occurred in {0}?
                    Question = "Welche Farbkombination kam bei {0} vor?",
                },
            },
        },

        // The Gamepad
        [typeof(SGamepad)] = new()
        {
            Questions = new()
            {
                [SGamepad.Numbers] = new()
                {
                    // English: What were the numbers on {0}?
                    Question = "Welche Zahlen waren bei {0} zu sehen?",
                },
            },
        },

        // Garfield Kart
        [typeof(SGarfieldKart)] = new()
        {
            Questions = new()
            {
                [SGarfieldKart.Track] = new()
                {
                    // English: What was the track in {0}?
                    Question = "Welche Rennstrecke kam bei {0} vor?",
                },
                [SGarfieldKart.PuzzleCount] = new()
                {
                    // English: How many puzzle pieces did {0} have?
                    Question = "Wie viele Puzzleteile hatte {0}?",
                },
            },
        },

        // The Garnet Thief
        [typeof(SGarnetThief)] = new()
        {
            ModuleName = "Der Granatdieb",
            ModuleNameDative = "Granatdieb",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SGarnetThief.Claim] = new()
                {
                    // English: Which faction did {1} claim to be in {0}?
                    // Example: Which faction did Jungmoon claim to be in The Garnet Thief?
                    Question = "Welcher Fraktion hat bei {0} {1} behauptet anzugehören?",
                },
            },
        },

        // Ghost Movement
        [typeof(SGhostMovement)] = new()
        {
            ModuleName = "Geisterbewegung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SGhostMovement.Position] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was Inky in Ghost Movement?
                    Question = "Wo war {1} bei {0}?",
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
                    Question = "What was the language sung in {0}?",
                },
            },
        },

        // The Glitched Button
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

        // Goofy’s Game
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

        // Grand Piano
        [typeof(SGrandPiano)] = new()
        {
            ModuleName = "Konzertflügel",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SGrandPiano.Key] = new()
                {
                    // English: Which key was part of the {1} set in {0}?
                    // Example: Which key was part of the first set in Grand Piano?
                    Question = "Welcher Ton war bei {0} in der {1}en Menge?",
                },
                [SGrandPiano.FinalKey] = new()
                {
                    // English: Which key was the fifth set in {0}?
                    Question = "Aus welchem Ton bestand bei {0} die fünfte Menge?",
                },
            },
        },

        // The Gray Button
        [typeof(SGrayButton)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGrayButton.Coordinates] = new()
                {
                    // English: What was the {1} coordinate on the display in {0}?
                    // Example: What was the horizontal coordinate on the display in The Gray Button?
                    Question = "What was the {1} coordinate on the display in {0}?",
                    Arguments = new()
                    {
                        ["horizontal"] = "horizontal",
                        ["vertical"] = "vertical",
                    },
                },
            },
        },

        // Gray Cipher
        [typeof(SGrayCipher)] = new()
        {
            ModuleName = "Graue Geheimschrift",
            ModuleNameDative = "Grauen Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SGrayCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Gray Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "ober",
                        ["middle"] = "mittler",
                        ["bottom"] = "unter",
                    },
                },
            },
        },

        // The Great Void
        [typeof(SGreatVoid)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGreatVoid.Digit] = new()
                {
                    // English: What was the {1} digit in {0}?
                    // Example: What was the first digit in The Great Void?
                    Question = "What was the {1} digit in {0}?",
                },
                [SGreatVoid.Color] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in The Great Void?
                    Question = "What was the {1} color in {0}?",
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
            ModuleName = "Grüne Pfeile",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SGreenArrows.LastScreen] = new()
                {
                    // English: What was the last number on the display on {0}?
                    Question = "Was war bei {0} die letzte Zahl auf dem Display?",
                },
            },
        },

        // The Green Button
        [typeof(SGreenButton)] = new()
        {
            ModuleName = "Der Grüne Knopf",
            ModuleNameDative = "Grünen Knopf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SGreenButton.Word] = new()
                {
                    // English: What was the word submitted in {0}?
                    Question = "Welches Wort wurde bei {0} eingegeben?",
                },
            },
        },

        // Green Cipher
        [typeof(SGreenCipher)] = new()
        {
            ModuleName = "Grüne Geheimschrift",
            ModuleNameDative = "Grünen Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SGreenCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Green Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "ober",
                        ["middle"] = "mittler",
                        ["bottom"] = "unter",
                    },
                },
            },
        },

        // Gridlock
        [typeof(SGridlock)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGridlock.StartingColor] = new()
                {
                    // English: What was the starting color in {0}?
                    Question = "What was the starting color in {0}?",
                    Answers = new()
                    {
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                    },
                },
                [SGridlock.StartingLocation] = new()
                {
                    // English: What was the starting location in {0}?
                    Question = "What was the starting location in {0}?",
                },
                [SGridlock.EndingLocation] = new()
                {
                    // English: What was the ending location in {0}?
                    Question = "What was the ending location in {0}?",
                },
            },
        },

        // Grocery Store
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

        // Gryphons
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

        // Guess Who?
        [typeof(SGuessWho)] = new()
        {
            ModuleName = "Wer ist es?",
            Questions = new()
            {
                [SGuessWho.Colors] = new()
                {
                    // English: Did {1} flash “YES” in {0}?
                    // Example: Did Red flash “YES” in Guess Who??
                    Question = "Ist bei {0} “JA” in {1} vorgekommen?",
                    Arguments = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Violet"] = "Violett",
                        ["Cyan"] = "Türkis",
                        ["Pink"] = "Pink",
                    },
                    Answers = new()
                    {
                        ["Yes"] = "Ja",
                        ["No"] = "Nein",
                    },
                },
            },
        },

        // Gyromaze
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                    },
                },
            },
        },

        // h
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

        // Halli Galli
        [typeof(SHalliGalli)] = new()
        {
            Questions = new()
            {
                [SHalliGalli.Fruit] = new()
                {
                    // English: Which fruit were there five of in {0}?
                    Question = "Von welcher Frucht gab es bei {0} fünf?",
                    Answers = new()
                    {
                        ["Strawberries"] = "Erdbeeren",
                        ["Melons"] = "Melonen",
                        ["Lemons"] = "Zitronen",
                        ["Raspberries"] = "Himbeeren",
                        ["Bananas"] = "Bananen",
                    },
                },
                [SHalliGalli.Counts] = new()
                {
                    // English: What were the relevant counts in {0}?
                    Question = "In welcher Anzahl kamen die relevanten Früchte bei {0} vor?",
                },
            },
        },

        // Hereditary Base Notation
        [typeof(SHereditaryBaseNotation)] = new()
        {
            Questions = new()
            {
                [SHereditaryBaseNotation.InitialNumber] = new()
                {
                    // English: What was the given number in {0}?
                    Question = "Welche Zahl war bei {0} vorgegeben?",
                },
            },
        },

        // The Hexabutton
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

        // Hexamaze
        [typeof(SHexamaze)] = new()
        {
            ModuleName = "Hexalabyrinth",
            Questions = new()
            {
                [SHexamaze.PawnColor] = new()
                {
                    // English: What was the color of the pawn in {0}?
                    Question = "Welche Farbe hatte die Spielfigur bei {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Cyan"] = "Türkis",
                        ["Blue"] = "Blau",
                        ["Pink"] = "Pink",
                    },
                },
            },
            Discriminators = new()
            {
                [SHexamaze.Discriminator] = new()
                {
                    // English: the Hexamaze that {0} a {1} marking on it
                    // Example: the Hexamaze that has a triangle marking on it
                    Discriminator = "dem Hexalabyrinth, das {0} {1}-Markierung hat",
                    Arguments = new()
                    {
                        ["has"] = "eine",
                        ["doesn’t have"] = "keine",
                        ["triangle"] = "Dreieck",
                        ["circle"] = "Kreis",
                        ["hexagon"] = "Sechseck",
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

        // Hickory Dickory Dock
        [typeof(SHickoryDickoryDock)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Tickitie-Tackitie-Tock",
            Questions = new()
            {
                [SHickoryDickoryDock.Time] = new()
                {
                    // English: What time was shown when the clock struck {1} on {0}?
                    // Example: What time was shown when the clock struck 1:00 on Hickory Dickory Dock?
                    Question = "Welche Uhrzeit war bei {0} zu sehen, als die Uhr {1} schlug?",
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
            Questions = new()
            {
                [SHiddenColors.LED] = new()
                {
                    // English: What was the color of the main LED in {0}?
                    Question = "What was the color of the main LED in {0}?",
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
            Questions = new()
            {
                [SHiddenValue.Display] = new()
                {
                    // English: What was displayed on {0}?
                    Question = "What was displayed on {0}?",
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

        // Hill Cycle
        [typeof(SHillCycle)] = new()
        {
            ModuleName = "Hill-Schiffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SHillCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Hill Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}te Zeiger?",
                },
                [SHillCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Hill Cycle?
                    Question = "Welcher Buchstabe stand bei {0} auf dem {1}en Zeiger?",
                },
            },
            Discriminators = new()
            {
                [SHillCycle.LabelDiscriminator] = new()
                {
                    // English: the Hill Cycle that had the letter {0} on a dial
                    // Example: the Hill Cycle that had the letter A on a dial
                    Discriminator = "der Hill-Schiffer, bei der der Buchstabe {0} vorkam,",
                },
            },
        },

        // Hinges
        [typeof(SHinges)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Scharniere",
            ModuleNameDative = "Scharnieren",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SHinges.Initial] = new()
                {
                    // English: Which of these hinges was initially {1} {0}?
                    // Example: Which of these hinges was initially present on Hinges?
                    Question = "Welches dieser Scharniere war bei {0} anfänglich {1}?",
                    Arguments = new()
                    {
                        ["present on"] = "vorhanden",
                        ["absent from"] = "abwesend",
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
            Questions = new()
            {
                [SHogwarts.House] = new()
                {
                    // English: Which House was {1} solved for in {0}?
                    // Example: Which House was Binary Puzzle solved for in Hogwarts?
                    Question = "Für welches Haus wurde bei {0} {1} gelöst?",
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
                    Question = "Welches Modul wurde bei {0} für {1} gelöst?",
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
            Questions = new()
            {
                [SHoldUps.Shadows] = new()
                {
                    // English: What was the name of the {1} shadow shown in {0}?
                    // Example: What was the name of the first shadow shown in Hold Ups?
                    Question = "Was war bei {0} der Name des ersten Schattens?",
                },
            },
        },

        // Holographic Memory
        [typeof(SHolographicMemory)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Holographisches Gedächtnis",
            ModuleNameDative = "Holographischen Gedächtnis",
            Questions = new()
            {
                [SHolographicMemory.InitialGrid] = new()
                {
                    // English: Which side did this symbol appear in {0}? (+ sprite)
                    Question = "Auf welcher Seite tauchte dieses Symbol bei {0} auf?",
                    Answers = new()
                    {
                        ["Light"] = "Hell",
                        ["Dark"] = "Dunkel",
                    },
                },
            },
        },

        // Homophones
        [typeof(SHomophones)] = new()
        {
            ModuleName = "Teekesselchen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SHomophones.DisplayedPhrases] = new()
                {
                    // English: What was the {1} displayed phrase in {0}?
                    // Example: What was the first displayed phrase in Homophones?
                    Question = "Was wurde bei {0} als {1}es angezeigt?",
                },
            },
        },

        // Horrible Memory
        [typeof(SHorribleMemory)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Grausiges Memory",
            ModuleNameDative = "Grausigen Memory",
            Questions = new()
            {
                [SHorribleMemory.Positions] = new()
                {
                    // English: In what position was the button pressed on the {1} stage of {0}?
                    // Example: In what position was the button pressed on the first stage of Horrible Memory?
                    Question = "In welcher Position war bei {0} die gedrückte Taste in Stufe {1}?",
                },
                [SHorribleMemory.Labels] = new()
                {
                    // English: What was the label of the button pressed on the {1} stage of {0}?
                    // Example: What was the label of the button pressed on the first stage of Horrible Memory?
                    Question = "Welche Aufschrift hatte bei {0} die gedrückte Taste in Stufe {1}?",
                },
                [SHorribleMemory.Colors] = new()
                {
                    // English: What color was the button pressed on the {1} stage of {0}?
                    // Example: What color was the button pressed on the first stage of Horrible Memory?
                    Question = "Welche Farbe hatte bei {0} die gedrückte Taste in Stufe {1}?",
                    Answers = new()
                    {
                        ["blue"] = "blau",
                        ["green"] = "grün",
                        ["red"] = "rot",
                        ["orange"] = "orange",
                        ["purple"] = "lila",
                        ["pink"] = "pink",
                    },
                },
            },
        },

        // Human Resources
        [typeof(SHumanResources)] = new()
        {
            ModuleName = "Personalabteilung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SHumanResources.Descriptors] = new()
                {
                    // English: Which was a descriptor shown in {1} in {0}?
                    // Example: Which was a descriptor shown in red in Human Resources?
                    Question = "Welcher Deskriptor kam bei {0} in {1} vor?",
                    Arguments = new()
                    {
                        ["red"] = "rot",
                        ["green"] = "grün",
                    },
                },
                [SHumanResources.HiredFired] = new()
                {
                    // English: Who was {1} in {0}?
                    // Example: Who was fired in Human Resources?
                    Question = "Wer wurde bei {0} {1}?",
                    Arguments = new()
                    {
                        ["fired"] = "gefeuert",
                        ["hired"] = "eingestellt",
                    },
                },
            },
        },

        // Hunting
        [typeof(SHunting)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SHunting.ColumnsRows] = new()
                {
                    // English: Which of the first three stages of {0} had the {1} symbol {2}?
                    // Example: Which of the first three stages of Hunting had the column symbol first?
                    Question = "In welchen der ersten drei Stufen von {0} war das {1} das {2}e?",
                    Arguments = new()
                    {
                        ["column"] = "Spaltensymbol",
                        ["row"] = "Reihensymbol",
                    },
                    Answers = new()
                    {
                        ["none"] = "keine",
                        ["first"] = "erste",
                        ["second"] = "zweite",
                        ["first two"] = "erste & zweite",
                        ["third"] = "dritte",
                        ["first & third"] = "erste & dritte",
                        ["second & third"] = "zweite & dritte",
                        ["all three"] = "alle drei",
                    },
                },
            },
        },

        // The Hypercube
        [typeof(SHypercube)] = new()
        {
            Questions = new()
            {
                [SHypercube.Rotations] = new()
                {
                    // English: What was the {1} rotation in {0}?
                    // Example: What was the first rotation in The Hypercube?
                    Question = "Was war die {1}e Rotation in {0}?",
                },
            },
        },

        // HyperForget
        [typeof(SHyperForget)] = new()
        {
            Questions = new()
            {
                [SHyperForget.Rotations] = new()
                {
                    // English: What was the rotation for the {1} stage in {0}?
                    // Example: What was the rotation for the first stage in HyperForget?
                    Question = "Was war bei {0} die Rotation in der {1}en Stufe?",
                },
            },
            Discriminators = new()
            {
                [SHyperForget.Discriminator] = new()
                {
                    // English: the HyperForget whose rotation in the {1} stage was {0}
                    // Example: the HyperForget whose rotation in the first stage was XY
                    Discriminator = "dem HyperForget, dessen Rotation in der {1}en Stufe {0} war,",
                },
            },
        },

        // The Hyperlink
        [typeof(SHyperlink)] = new()
        {
            ModuleName = "Der Hyperlink",
            ModuleNameDative = "Hyperlink",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SHyperlink.Characters] = new()
                {
                    // English: What was the {1} character of the hyperlink in {0}?
                    // Example: What was the first character of the hyperlink in The Hyperlink?
                    Question = "Was war bei {0} das erste Zeichen im Hyperlink?",
                },
                [SHyperlink.Answer] = new()
                {
                    // English: Which module was referenced on {0}?
                    Question = "Auf welches Modul wurde bei {0} verwiesen?",
                },
            },
        },

        // Ice Cream
        [typeof(SIceCream)] = new()
        {
            ModuleName = "Eiscreme",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SIceCream.Flavour] = new()
                {
                    // English: Which one of these flavours {1} to the {2} customer in {0}?
                    // Example: Which one of these flavours was on offer, but not sold, to the first customer in Ice Cream?
                    Question = "Welche Geschmacksrichtung wurde bei {0} dem/der {2}en Kunden/Kundin {1}?",
                    Arguments = new()
                    {
                        ["was on offer, but not sold,"] = "angeboten, aber nicht verkauft",
                        ["was not on offer"] = "nicht angeboten",
                    },
                },
                [SIceCream.Customer] = new()
                {
                    // English: Who was the {1} customer in {0}?
                    // Example: Who was the first customer in Ice Cream?
                    Question = "Wer war bei {0} der/die {1}e Kunde/Kundin?",
                },
            },
        },

        // Identification Crisis
        [typeof(SIdentificationCrisis)] = new()
        {
            ModuleName = "Identifikationskrise",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SIdentificationCrisis.Shape] = new()
                {
                    // English: What was the {1} shape used in {0}?
                    // Example: What was the first shape used in Identification Crisis?
                    Question = "Was war bei {0} die {1} verwendete Form?",
                },
                [SIdentificationCrisis.Dataset] = new()
                {
                    // English: What was the {1} identification module used in {0}?
                    // Example: What was the first identification module used in Identification Crisis?
                    Question = "Was war bei {0} das {1}e Identifikationsmodul?",
                    Answers = new()
                    {
                        ["Morse Identification"] = "Morse-Identifikation",
                        ["Boozleglyph Identification"] = "Boozleglyph-Identifikation",
                        ["Plant Identification"] = "Pflanzen-Identifikation",
                        ["Pickup Identification"] = "Pickup-Identifikation",
                        ["Emotiguy Identification"] = "Emotiguy-Identifikation",
                        ["Ars Goetia Identification"] = "Ars-Goetia-Identifikation",
                        ["Mii Identification"] = "Mii-Identifikation",
                        ["Customer Identification"] = "Kunden-Identifikation",
                        ["Spongebob Birthday Identification"] = "Spongebob-Geburtstags-Identifikation",
                        ["VTuber Identification"] = "VTuber-Identifikation",
                    },
                },
            },
        },

        // Identity Parade
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

        // The Impostor
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

        // Indigo Cipher
        [typeof(SIndigoCipher)] = new()
        {
            ModuleName = "Indigo-Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SIndigoCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Indigo Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "ober",
                        ["middle"] = "mittler",
                        ["bottom"] = "unter",
                    },
                },
            },
        },

        // Infinite Loop
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

        // Ingredients
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

        // Inner Connections
        [typeof(SInnerConnections)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SInnerConnections.LED] = new()
                {
                    // English: What color was the LED in {0}?
                    Question = "What color was the LED in {0}?",
                    Answers = new()
                    {
                        ["Black"] = "Black",
                        ["Blue"] = "Blue",
                        ["Red"] = "Red",
                        ["White"] = "White",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                    },
                },
                [SInnerConnections.Morse] = new()
                {
                    // English: What was the digit flashed in Morse in {0}?
                    Question = "What was the digit flashed in Morse in {0}?",
                },
            },
        },

        // Interpunct
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

        // IPA
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

        // The iPhone
        [typeof(SiPhone)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SiPhone.Digits] = new()
                {
                    // English: What was the {1} PIN digit in {0}?
                    // Example: What was the first PIN digit in The iPhone?
                    Question = "What was the {1} PIN digit in {0}?",
                },
            },
        },

        // Jenga
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

        // The Jewel Vault
        [typeof(SJewelVault)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SJewelVault.Wheels] = new()
                {
                    // English: What number was wheel {1} in {0}?
                    // Example: What number was wheel A in The Jewel Vault?
                    Question = "What number was wheel {1} in {0}?",
                },
            },
        },

        // Jumble Cycle
        [typeof(SJumbleCycle)] = new()
        {
            ModuleName = "Wirrwarr-Schiffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SJumbleCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Jumble Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}te Zeiger?",
                },
                [SJumbleCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Jumble Cycle?
                    Question = "Welcher Buchstabe stand bei {0} auf dem {1}en Zeiger?",
                },
            },
            Discriminators = new()
            {
                [SJumbleCycle.LabelDiscriminator] = new()
                {
                    // English: the Jumble Cycle that had the letter {0} on a dial
                    // Example: the Jumble Cycle that had the letter A on a dial
                    Discriminator = "der Wirrwarr-Schiffer, bei der der Buchstabe {0} vorkam,",
                },
            },
        },

        // Juxtacolored Squares
        [typeof(SJuxtacoloredSquares)] = new()
        {
            ModuleName = "Gefärbte Nachbarfelder",
            ModuleNameDative = "Gefärbten Nachbarfeldern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SJuxtacoloredSquares.ColorsByPosition] = new()
                {
                    // English: What was the color of this square in {0}? (+ sprite)
                    Question = "What was the color of this square in {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Magenta"] = "Magenta",
                        ["Orange"] = "Orange",
                        ["Cyan"] = "Cyan",
                        ["Purple"] = "Purple",
                        ["Chestnut"] = "Chestnut",
                        ["Brown"] = "Brown",
                        ["Mauve"] = "Mauve",
                        ["Azure"] = "Azure",
                        ["Jade"] = "Jade",
                        ["Forest"] = "Forest",
                        ["Gray"] = "Gray",
                        ["Black"] = "Black",
                    },
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

        // Kanji
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

        // The Kanye Encounter
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

        // KayMazey Talk
        [typeof(SKayMazeyTalk)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKayMazeyTalk.QWord] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the starting word in KayMazey Talk?
                    Question = "Was war bei {0} das {1}?",
                    Arguments = new()
                    {
                        ["starting"] = "Startwort",
                        ["goal"] = "Zielwort",
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

        // Keypad Magnified
        [typeof(SKeypadMagnified)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKeypadMagnified.LED] = new()
                {
                    // English: What was the position of the LED in {0}?
                    Question = "What was the position of the LED in {0}?",
                    Answers = new()
                    {
                        ["Top-left"] = "Top-left",
                        ["Top-right"] = "Top-right",
                        ["Bottom-left"] = "Bottom-left",
                        ["Bottom-right"] = "Bottom-right",
                    },
                },
            },
        },

        // Keypad Maze
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

        // Keypad Sequence
        [typeof(SKeypadSequence)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKeypadSequence.Labels] = new()
                {
                    // English: What was this key’s label on the {1} panel in {0}? (+ sprite)
                    // Example: What was this key’s label on the first panel in Keypad Sequence? (+ sprite)
                    Question = "What was the label on this button on the {1} panel in {0}?",
                },
            },
        },

        // Keywords
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

        // The Klaxon
        [typeof(SKlaxon)] = new()
        {
            ModuleName = "Das Klaxon",
            ModuleNameDative = "Klaxon",
            Questions = new()
            {
                [SKlaxon.FirstModule] = new()
                {
                    // English: What was the first module to set off {0}?
                    Question = "Welches Modul hat als erstes {0} ausgelöst?",
                },
            },
        },

        // Know Your Way
        [typeof(SKnowYourWay)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SKnowYourWay.Arrow] = new()
                {
                    // English: Which way was the arrow pointing in {0}?
                    Question = "Which way was the arrow pointing in {0}?",
                    Answers = new()
                    {
                        ["Up"] = "Up",
                        ["Down"] = "Down",
                        ["Left"] = "Left",
                        ["Right"] = "Right",
                    },
                },
                [SKnowYourWay.Led] = new()
                {
                    // English: Which LED was green in {0}?
                    Question = "Which LED was green in {0}?",
                    Answers = new()
                    {
                        ["Top"] = "Top",
                        ["Bottom"] = "Bottom",
                        ["Right"] = "Right",
                        ["Left"] = "Left",
                    },
                },
            },
        },

        // Kooky Keypad
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

        // Kugelblitz
        [typeof(SKugelblitz)] = new()
        {
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SKugelblitz.BlackOrangeYellowIndigoViolet] = new()
                {
                    // English: Which particles were present for the {1} stage of {0}?
                    // Example: Which particles were present for the first stage of Kugelblitz?
                    Question = "Welche Partikel waren bei {0} in der {1}en Stufe zu sehen?",
                    // Refer to translations.md to understand the weird strings
                    Additional = new()
                    {
                        ["R"] = "Ro",
                        ["O"] = "Or",
                        ["Y"] = "Ge",
                        ["G"] = "Gr",
                        ["B"] = "Bl",
                        ["I"] = "In",
                        ["V"] = "Vi",
                        ["{0}{1}{2}{3}{4}{5}{6}"] = "{0}{1}{2}{3}{4}{5}{6}",
                        ["None"] = "Keine",
                    },
                },
                [SKugelblitz.RedGreenBlue] = new()
                {
                    // English: What were the particles’ values for the {1} stage of {0}?
                    // Example: What were the particles’ values for the first stage of Kugelblitz?
                    Question = "Was waren bei {0} die Partikelwerte in der {1}en Stufe?",
                    // Refer to translations.md to understand the weird strings
                    Additional = new()
                    {
                        ["R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}"] = "Ro={0}, Or={1}, Ge={2}, Gr={3}, Bl={4}, In={5}, Vi={6}",
                    },
                },
            },
            Discriminators = new()
            {
                [SKugelblitz.Color] = new()
                {
                    // English: the {0} Kugelblitz
                    // Example: the black Kugelblitz
                    Discriminator = "dem {0} Kugelblitz",
                    Arguments = new()
                    {
                        ["black"] = "schwarzen",
                        ["red"] = "roten",
                        ["orange"] = "orangenen",
                        ["yellow"] = "gelben",
                        ["green"] = "grünen",
                        ["blue"] = "blauen",
                        ["indigo"] = "indigofarbenen",
                        ["violet"] = "violetten",
                    },
                },
                [SKugelblitz.NoLinks] = new()
                {
                    // English: the Kugelblitz linked with no other Kugelblitzes
                    Discriminator = "dem mit keinem anderen Kugelblitz gekoppelten Kugelblitz",
                },
                [SKugelblitz.Links] = new()
                {
                    // English: the {0} Kugelblitz linked with {1}
                    // Example: the black Kugelblitz linked with one other Kugelblitz
                    Discriminator = "dem {0} mit {1} gekoppelten Kugelblitz",
                    Arguments = new()
                    {
                        ["black"] = "schwarzen",
                        ["red"] = "roten",
                        ["orange"] = "orangenen",
                        ["yellow"] = "gelben",
                        ["green"] = "grünen",
                        ["blue"] = "blauen",
                        ["indigo"] = "indigofarbenen",
                        ["violet"] = "violetten",
                        ["one other Kugelblitz"] = "einem anderen Kugelblitz",
                        ["two other Kugelblitzes"] = "zwei anderen Kugelblitzen",
                        ["three other Kugelblitzes"] = "drei anderen Kugelblitzen",
                        ["four other Kugelblitzes"] = "vier anderen Kugelblitzen",
                        ["five other Kugelblitzes"] = "fünf anderen Kugelblitzen",
                        ["six other Kugelblitzes"] = "sechs anderen Kugelblitzen",
                        ["seven other Kugelblitzes"] = "sieben anderen Kugelblitzen",
                    },
                },
            },
        },

        // Kuro
        [typeof(SKuro)] = new()
        {
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SKuro.Mood] = new()
                {
                    // English: What was Kuro’s mood in {0}?
                    Question = "Was war bei {0} Kuros Stimmung?",
                },
            },
        },

        // The Labyrinth
        [typeof(SLabyrinth)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLabyrinth.PortalLocations] = new()
                {
                    // English: Where was one of the portals in layer {1} in {0}?
                    // Example: Where was one of the portals in layer 1 (Red) in The Labyrinth?
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
                    // English: In which layer was this portal in {0}? (+ sprite)
                    Question = "In which layer was this portal in {0}?",
                    Answers = new()
                    {
                        ["1 (Red)"] = "1 (Red)",
                        ["2 (Orange)"] = "2 (Orange)",
                        ["3 (Yellow)"] = "3 (Yellow)",
                        ["4 (Green)"] = "4 (Green)",
                        ["5 (Blue)"] = "5 (Blue)",
                    },
                },
            },
        },

        // Ladder Lottery
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

        // Ladders
        [typeof(SLadders)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLadders.Stage2Colors] = new()
                {
                    // English: Which color was present on the second ladder in {0}?
                    Question = "Which color was present on the second ladder in {0}?",
                    Answers = new()
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
                [SLadders.Stage3Missing] = new()
                {
                    // English: What color was missing on the third ladder in {0}?
                    Question = "What color was missing on the third ladder in {0}?",
                    Answers = new()
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
                    Question = "Which of these squares was initially {1} in {0}?",
                    Arguments = new()
                    {
                        ["black"] = "black",
                        ["white"] = "white",
                    },
                },
            },
        },

        // Lasers
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

        // LED Encryption
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

        // LED Grid
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

        // LED Math
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                    },
                },
            },
        },

        // LEDs
        [typeof(SLEDs)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLEDs.OriginalColor] = new()
                {
                    // English: What was the initial color of the changed LED in {0}?
                    Question = "What was the initial color of the changed LED in {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Purple"] = "Purple",
                        ["Black"] = "Black",
                        ["White"] = "White",
                    },
                },
            },
        },

        // LEGOs
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

        // Letter Math
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

        // Light Bulbs
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

        // Linq
        [typeof(SLinq)] = new()
        {
            Questions = new()
            {
                [SLinq.Function] = new()
                {
                    // English: What was the {1} function in {0}?
                    // Example: What was the first function in Linq?
                    Question = "Was war bei {0} die {1}e Funktion?",
                },
            },
            Discriminators = new()
            {
                [SLinq.Discriminator] = new()
                {
                    // English: the Linq whose {0} function was {1}
                    // Example: the Linq whose first function was First
                    Discriminator = "dem Linq, dessen {0}e Funktion {1} war,",
                },
            },
        },

        // Lion’s Share
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

        // Listening
        [typeof(SListening)] = new()
        {
            ModuleName = "Zuhören",
            Questions = new()
            {
                [SListening.Sound] = new()
                {
                    // English: What clip was played in {0}?
                    Question = "Welches Geräusch kam bei {0} vor?",
                },
            },
        },

        // Literal Maze
        [typeof(SLiteralMaze)] = new()
        {
            ModuleName = "Buchstäbliches Labyrinth",
            ModuleNameDative = "Buchstäblichen Labyrinth",
            Questions = new()
            {
                [SLiteralMaze.Letter] = new()
                {
                    // English: Which letter was in this position in {0}?
                    Question = "Welcher Buchstabe war bei {0} an dieser Stelle?",
                },
            },
        },

        // Logical Buttons
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                        ["Purple"] = "Purple",
                        ["White"] = "White",
                        ["Orange"] = "Orange",
                        ["Cyan"] = "Cyan",
                        ["Grey"] = "Grey",
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

        // Logic Gates
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

        // Lombax Cubes
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

        // The London Underground
        [typeof(SLondonUnderground)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SLondonUnderground.Stations] = new()
                {
                    // English: Where did the {1} journey on {0} {2}?
                    // Example: Where did the first journey on The London Underground depart from?
                    Question = "Where did the {1} journey on {0} {2}?",
                    Arguments = new()
                    {
                        ["depart from"] = "depart from",
                        ["arrive to"] = "arrive to",
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
                    Question = "What was the word on the top display on {0}?",
                },
            },
        },

        // Mad Memory
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

        // Mafia
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

        // Magenta Cipher
        [typeof(SMagentaCipher)] = new()
        {
            ModuleName = "Magenta-Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SMagentaCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Magenta Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "ober",
                        ["middle"] = "mittler",
                        ["bottom"] = "unter",
                    },
                },
            },
        },

        // Mahjong
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
                    Question = "Which main page did the {1} button's effect come from in {0}?",
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
                    Answers = new()
                    {
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                    },
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

        // M&Ms
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
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["orange"] = "orange",
                        ["blue"] = "blue",
                        ["yellow"] = "yellow",
                        ["brown"] = "brown",
                    },
                },
                [SMandMs.Labels] = new()
                {
                    // English: What was the text on the {1} button in {0}?
                    // Example: What was the text on the first button in M&Ms?
                    Question = "What was the text on the {1} button in {0}?",
                },
            },
        },

        // M&Ns
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
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["orange"] = "orange",
                        ["blue"] = "blue",
                        ["yellow"] = "yellow",
                        ["brown"] = "brown",
                    },
                },
                [SMandNs.Label] = new()
                {
                    // English: What was the text on the correct button in {0}?
                    Question = "What was the text on the correct button in {0}?",
                },
            },
        },

        // Maritime Flags
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

        // Maritime Semaphore
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
                    Question = "Which letter was shown by the {2} for the {1} position in {0}?",
                    Arguments = new()
                    {
                        ["left flag"] = "left flag",
                        ["right flag"] = "right flag",
                        ["semaphore"] = "semaphore",
                    },
                },
            },
        },

        // The Maroon Button
        [typeof(SMaroonButton)] = new()
        {
            ModuleName = "Der Kastanienknopf",
            ModuleNameDative = "Kastanienknopf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SMaroonButton.A] = new()
                {
                    // English: What was A in {0}?
                    Question = "Was war A bei {0}?",
                },
            },
        },

        // Maroon Cipher
        [typeof(SMaroonCipher)] = new()
        {
            ModuleName = "Kastanien-Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SMaroonCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Maroon Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "ober",
                        ["middle"] = "mittler",
                        ["bottom"] = "unter",
                    },
                },
            },
        },

        // Mashematics
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

        // Master Tapes
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
                    Question = "Which planet was present in the {1} stage of {0}?",
                },
            },
        },

        // Math ’em
        [typeof(SMathEm)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMathEm.Color] = new()
                {
                    // English: What was the color of this tile before the shuffle on {0}? (+ sprite)
                    Question = "What was the color of this tile before the shuffle on {0}?",
                    Answers = new()
                    {
                        ["White"] = "White",
                        ["Bronze"] = "Bronze",
                        ["Silver"] = "Silver",
                        ["Gold"] = "Gold",
                    },
                },
                [SMathEm.Label] = new()
                {
                    // English: What was the design on this tile before the shuffle on {0}? (+ sprite)
                    Question = "What was the design on this tile before the shuffle on {0}?",
                },
            },
        },

        // The Matrix
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

        // Maze
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
                        ["row"] = "row",
                        ["left"] = "left",
                        ["top"] = "top",
                    },
                },
            },
        },

        // Maze³
        [typeof(SMaze3)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMaze3.StartingFace] = new()
                {
                    // English: What was the color of the starting face in {0}?
                    Question = "What was the color of the starting face in {0}?",
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
                        ["Forwards"] = "Forwards",
                        ["Clockwise"] = "Clockwise",
                        ["Backwards"] = "Backwards",
                        ["Counter-clockwise"] = "Counter-clockwise",
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

        // Mazematics
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

        // Maze Scrambler
        [typeof(SMazeScrambler)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMazeScrambler.Start] = new()
                {
                    // English: What was the starting position on {0}?
                    Question = "What was the starting position on {0}?",
                    Answers = new()
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
                [SMazeScrambler.Goal] = new()
                {
                    // English: What was the goal on {0}?
                    Question = "What was the goal on {0}?",
                    Answers = new()
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
                [SMazeScrambler.Indicators] = new()
                {
                    // English: Which of these positions was a maze marking on {0}?
                    Question = "Which of these positions was a maze marking on {0}?",
                    Answers = new()
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
            NeedsTranslation = true,
            Questions = new()
            {
                [SMazeseeker.Cell] = new()
                {
                    // English: How many walls surrounded this cell in {0}? (+ sprite)
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

        // Maze Swap
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

        // Mega Man 2
        [typeof(SMegaMan2)] = new()
        {
            Questions = new()
            {
                [SMegaMan2.Master] = new()
                {
                    // English: Which master was shown in {0}?
                    Question = "Welcher Meister war bei {0} zu sehen?",
                },
                [SMegaMan2.Weapon] = new()
                {
                    // English: Which weapon was shown in {0}?
                    Question = "Welche Waffe war bei {0} zu sehen?",
                },
            },
        },

        // Melody Sequencer
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

        // Memorable Buttons
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

        // Memory
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

        // Memory Wires
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Blue"] = "Blue",
                        ["White"] = "White",
                        ["Black"] = "Black",
                    },
                },
                [SMemoryWires.DisplayedDigits] = new()
                {
                    // English: What was the digit displayed in the {1} stage of {0}?
                    // Example: What was the digit displayed in the first stage of Memory Wires?
                    Question = "What was the digit displayed in the {1} stage of {0}?",
                },
            },
        },

        // Metamorse
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

        // Metapuzzle
        [typeof(SMetapuzzle)] = new()
        {
            Questions = new()
            {
                [SMetapuzzle.Answer] = new()
                {
                    // English: What was the final answer in {0}?
                    Question = "Was war bei {0} die abschließende Antwort?",
                },
            },
        },

        // Minsk Metro
        [typeof(SMinskMetro)] = new()
        {
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SMinskMetro.Station] = new()
                {
                    // English: What was the name of starting station in {0}?
                    Question = "Wie hieß bei {0} die Anfangshaltestelle?",
                },
            },
        },

        // Mirror
        [typeof(SMirror)] = new()
        {
            ModuleName = "Spiegel",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SMirror.Word] = new()
                {
                    // English: What was the second word written by the original ghost in {0}?
                    Question = "Was war bei {0} das zweite Wort, das der ursprüngliche Geist geschrieben hat?",
                },
            },
        },

        // The Missing Letter
        [typeof(SMissingLetter)] = new()
        {
            ModuleName = "Der Fehlende Buchstabe",
            ModuleNameDative = "Fehlenden Buchstaben",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SMissingLetter.MissingLetter] = new()
                {
                    // English: What letter was missing in {0}?
                    Question = "Welcher Buchstabe hat bei {0} gefehlt?",
                },
            },
        },

        // Mister Softee
        [typeof(SMisterSoftee)] = new()
        {
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SMisterSoftee.SpongebobPosition] = new()
                {
                    // English: Where was the SpongeBob Bar on {0}?
                    Question = "Wo war bei {0} das Spongebob-Eis?",
                },
                [SMisterSoftee.TreatsPresent] = new()
                {
                    // English: Which treat was present on {0}?
                    Question = "Welches Eis war bei {0} im Angebot?",
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
                    Question = "What was the position of the submit button in {0}?",
                },
            },
        },

        // Modern Cipher
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

        // Module Listening
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

        // Module Maneuvers
        [typeof(SModuleManeuvers)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Modulmanöver",
            ModuleNameDative = "Modulmanövern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SModuleManeuvers.Goal] = new()
                {
                    // English: What was the goal location in {0}?
                    Question = "Was war bei {0} die Zielposition?",
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

        // Module Movements
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

        // Money Game
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

        // Monsplode, Fight!
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

        // Monsplode Trading Cards
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

        // The Moon
        [typeof(SMoon)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMoon.LitUnlit] = new()
                {
                    // English: What was the {1} set in clockwise order in {0}?
                    // Example: What was the first initially lit set in clockwise order in The Moon?
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
                    Answers = new()
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
            },
        },

        // More Code
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

        // Morse-A-Maze
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

        // Morse Buttons
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
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["yellow"] = "yellow",
                        ["orange"] = "orange",
                        ["purple"] = "purple",
                    },
                },
            },
        },

        // Morsematics
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

        // Morse War
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
                    Question = "What were the LEDs in the {1} row in {0} (1 = on, 0 = off)?",
                    Arguments = new()
                    {
                        ["bottom"] = "bottom",
                        ["middle"] = "middle",
                        ["top"] = "top",
                    },
                },
            },
        },

        // .--/---/..-.
        [typeof(SMorseWoF)] = new()
        {
            ModuleName = ".--/.../.-/-../-.-",
            Questions = new()
            {
                [SMorseWoF.Displays] = new()
                {
                    // English: What was the display in the {1} stage on {0}?
                    // Example: What was the display in the first stage on .--/---/..-.?
                    Question = "Was stand bei {0} in der {1}en Stufe auf dem Display?",
                },
            },
        },

        // Mouse in the Maze
        [typeof(SMouseInTheMaze)] = new()
        {
            ModuleName = "Maus im Labyrinth",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SMouseInTheMaze.Sphere] = new()
                {
                    // English: Which color sphere was the goal in {0}?
                    Question = "Welche Farbe hatte bei {0} die Zielkugel?",
                    Answers = new()
                    {
                        ["white"] = "weiß",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["yellow"] = "gelb",
                    },
                },
                [SMouseInTheMaze.Torus] = new()
                {
                    // English: What color was the torus in {0}?
                    Question = "Welche Farbe hatte bei {0} der Torus?",
                    Answers = new()
                    {
                        ["white"] = "weiß",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["yellow"] = "gelb",
                    },
                },
            },
        },

        // M-Seq
        [typeof(SMSeq)] = new()
        {
            Questions = new()
            {
                [SMSeq.Obtained] = new()
                {
                    // English: What was the {1} obtained digit in {0}?
                    // Example: What was the first obtained digit in M-Seq?
                    Question = "Was war bei {0} die {1}e erhaltene Ziffer?",
                },
                [SMSeq.Submitted] = new()
                {
                    // English: What was the final number from the iteration process in {0}?
                    Question = "Was war bei {0} die letzte Zahl in der Iteration?",
                },
            },
        },

        // Mssngv Wls
        [typeof(SMssngvWls)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Fehlender Vokal",
            ModuleNameDative = "Fehlenden Vokal",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SMssngvWls.MssNgvwL] = new()
                {
                    // English: Which vowel was missing in {0}?
                    // Example: Which vowel was missing in Mssngv Wls?
                    Question = "Welcher Vokal hat bei {0} gefehlt?",
                    // Refer to translations.md to understand the weird strings
                    Arguments = new()
                    {
                        ["AEIOU"] = "AEIOUÄÖÜ",
                    },
                },
            },
        },

        // Multicolored Switches
        [typeof(SMulticoloredSwitches)] = new()
        {
            ModuleName = "Vielfarbige Schalter",
            ModuleNameDative = "Vielfarbige Schaltern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SMulticoloredSwitches.LedColor] = new()
                {
                    // English: What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?
                    // Example: What color was the first LED on the top row when the tiny LED was lit in Multicolored Switches?
                    Question = "Welche Farbe hatte bei {0} die {1}e LED in der {2} Reihe, als die Mini-LED {3} war?",
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["bottom"] = "unteren",
                        ["lit"] = "an",
                        ["unlit"] = "aus",
                    },
                    Answers = new()
                    {
                        ["black"] = "schwarz",
                        ["red"] = "rot",
                        ["green"] = "grün",
                        ["yellow"] = "gelb",
                        ["blue"] = "blau",
                        ["magenta"] = "magenta",
                        ["cyan"] = "türkis",
                        ["white"] = "weiß",
                    },
                },
            },
        },

        // The Multiverse Hotline
        [typeof(SMultiverseHotline)] = new()
        {
            ModuleName = "Multiversum-Hotline",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SMultiverseHotline.OriginUniverse] = new()
                {
                    // English: What was the origin universe in {0}?
                    Question = "Was war bei {0} das Ursprungsuniversum?",
                },
                [SMultiverseHotline.OriginUniverseInitNumber] = new()
                {
                    // English: What was the origin universe’s initial number in {0}?
                    Question = "Was war bei {0} die Anfangszahl des Ursprungsuniversums?",
                },
            },
        },

        // Murder
        [typeof(SMurder)] = new()
        {
            ModuleName = "Mord",
            Questions = new()
            {
                [SMurder.Suspect] = new()
                {
                    // English: Which of these was {1} in {0}?
                    // Example: Which of these was a suspect but not the murderer in Murder?
                    Question = "Wer war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["a suspect but not the murderer"] = "ein Tatverdächtiger, aber nicht der Mörder",
                        ["not a suspect"] = "kein Tatverdächtiger",
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
                    Question = "Welche der folgenden Waffen war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["a potential weapon but not the murder weapon"] = "vorhanden aber nicht die Tatwaffe",
                        ["not a potential weapon"] = "nicht vorhanden",
                    },
                    Answers = new()
                    {
                        ["Candlestick"] = "Kerzenleuchter",
                        ["Dagger"] = "Dolch",
                        ["Lead Pipe"] = "Bleirohr",
                        ["Revolver"] = "Pistole",
                        ["Rope"] = "Seil",
                        ["Spanner"] = "Rohrzange",
                    },
                },
                [SMurder.BodyFound] = new()
                {
                    // English: Where was the body found in {0}?
                    Question = "Wo wurde bei {0} die Leiche gefunden?",
                    Answers = new()
                    {
                        ["Dining Room"] = "Esszimmer",
                        ["Study"] = "Arbeitszimmer",
                        ["Kitchen"] = "Küche",
                        ["Lounge"] = "Salon",
                        ["Billiard Room"] = "Billard-Zimmer",
                        ["Conservatory"] = "Wintergarten",
                        ["Ballroom"] = "Musikzimmer",
                        ["Hall"] = "Halle",
                        ["Library"] = "Bibliothek",
                    },
                },
            },
        },

        // Mystery Module
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

        // Mystic Square
        [typeof(SMysticSquare)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMysticSquare.Skull] = new()
                {
                    // English: Where was the skull in {0}?
                    Question = "Where was the skull in {0}?",
                    Answers = new()
                    {
                        ["top left"] = "top left",
                        ["top middle"] = "top middle",
                        ["top right"] = "top right",
                        ["middle left"] = "middle left",
                        ["center"] = "center",
                        ["middle right"] = "middle right",
                        ["bottom left"] = "bottom left",
                        ["bottom middle"] = "bottom middle",
                        ["bottom right"] = "bottom right",
                    },
                },
            },
        },

        // Name Codes
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

        // Naming Conventions
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

        // N&Ms
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

        // N&Ns
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Orange"] = "Orange",
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                        ["Brown"] = "Brown",
                    },
                },
            },
        },

        // Navigation Determination
        [typeof(SNavigationDetermination)] = new()
        {
            Questions = new()
            {
                [SNavigationDetermination.Color] = new()
                {
                    // English: What was the color of the maze in {0}?
                    Question = "Welche Farbe hatte bei {0} das Labyrinth?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                    },
                },
                [SNavigationDetermination.Label] = new()
                {
                    // English: What was the label of the maze in {0}?
                    Question = "Welche Aufschrift hatte bei {0} das Labyrinth?",
                },
            },
        },

        // Navinums
        [typeof(SNavinums)] = new()
        {
            ModuleName = "Navinummern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SNavinums.DirectionalButtons] = new()
                {
                    // English: What was the {1} directional button pressed in {0}?
                    // Example: What was the first directional button pressed in Navinums?
                    Question = "Welche Richtungstaste wurde bei {0} als {1}e gedrückt?",
                    Answers = new()
                    {
                        ["up"] = "hoch",
                        ["left"] = "links",
                        ["right"] = "rechts",
                        ["down"] = "runter",
                    },
                },
                [SNavinums.MiddleDigit] = new()
                {
                    // English: What was the initial middle digit in {0}?
                    Question = "Was war bei {0} am Anfang die Ziffer in der Mitte?",
                },
            },
        },

        // The Navy Button
        [typeof(SNavyButton)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Der Königsblaue Knopf",
            ModuleNameDative = "Königsblauen Knopf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SNavyButton.QGreekLetters] = new()
                {
                    // English: Which Greek letter appeared on {0} (case-sensitive)?
                    Question = "Welcher griechische Buchstabe kam bei {0} vor (auf Groß-/Kleinschreibung achten)?",
                },
                [SNavyButton.QGiven] = new()
                {
                    // English: What was the {1} of the given in {0}?
                    // Example: What was the (0-indexed) column of the given in The Navy Button?
                    Question = "Was war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["(0-indexed) column"] = "die (0-basierte) Spalte des vorgegebenen Wertes",
                        ["(0-indexed) row"] = "die (0-basierte) Reihe des vorgegebenen Wertes",
                        ["value"] = "der vorgegebene Wert",
                    },
                },
            },
            Discriminators = new()
            {
                [SNavyButton.DGreekLettersNV] = new()
                {
                    // English: the Navy Button that had a {0} on it
                    // Example: the Navy Button that had a Β on it
                    Discriminator = "dem Königsblauen Knopf, bei dem ein {0} vorkam,",
                },
                [SNavyButton.DGreekLettersV] = new()
                {
                    // English: the Navy Button that had an {0} on it
                    // Example: the Navy Button that had an Α on it
                    Discriminator = "dem Königsblauen Knopf, bei dem ein {0} vorkam,",
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
            ModuleName = "Der Königsblaue Knopf",
            ModuleNameDative = "Königsblauen Knopf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SNecronomicon.Chapters] = new()
                {
                    // English: What was the chapter number of the {1} page in {0}?
                    // Example: What was the chapter number of the first page in The Necronomicon?
                    Question = "What was the chapter number of the {1} page in {0}?",
                },
            },
        },

        // Negativity
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

        // Neptune
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

        // Neutralization
        [typeof(SNeutralization)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNeutralization.Color] = new()
                {
                    // English: What was the acid’s color in {0}?
                    Question = "What was the acid’s color in {0}?",
                    Answers = new()
                    {
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                    },
                },
                [SNeutralization.Volume] = new()
                {
                    // English: What was the acid’s volume in {0}?
                    Question = "What was the acid’s volume in {0}?",
                },
            },
        },

        // Next In Line
        [typeof(SNextInLine)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNextInLine.FirstWire] = new()
                {
                    // English: What color was the first wire in {0}?
                    Question = "What color was the first wire in {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Black"] = "Black",
                        ["White"] = "White",
                        ["Gray"] = "Gray",
                    },
                },
            },
        },

        // Nim
        [typeof(SNim)] = new()
        {
            Questions = new()
            {
                [SNim.MatchCountFirstRow] = new()
                {
                    // English: How many matches were in the {1} row in {0}?
                    // Example: How many matches were in the first row in Nim?
                    Question = "Wie viele Streichhölzer waren bei {0} in der {1}en Reihe?",
                },
                [SNim.MatchCountOtherRows] = new()
                {
                    // English: How many matches were in the {1} row in {0}?
                    // Example: How many matches were in the first row in Nim?
                    Question = "Wie viele Streichhölzer waren bei {0} in der {1}en Reihe?",
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
                    Question = "Welcher Knopf ist bei {0} in der {1}en Stufe aufgeleuchtet?",
                },
            },
        },

        // Not Colored Squares
        [typeof(SNotColoredSquares)] = new()
        {
            ModuleName = "Gefärbte Felder Mal Anders",
            ModuleNameDative = "Gefärbten Feldern Mal Anders",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SNotColoredSquares.InitialPosition] = new()
                {
                    // English: What was the position of the square you initially pressed in {0}?
                    Question = "Welches Feld wurde bei {0} zuerst gedrückt?",
                },
            },
        },

        // Not Colored Switches
        [typeof(SNotColoredSwitches)] = new()
        {
            ModuleName = "Gefärbte Schalter Mal Anders",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SNotColoredSwitches.Word] = new()
                {
                    // English: What was the encrypted word in {0}?
                    Question = "Was war bei {0} das verschlüsselte Wort?",
                },
            },
        },

        // Not Colour Flash
        [typeof(SNotColourFlash)] = new()
        {
            ModuleName = "Gefärbte Folge Mal Anders",
            ModuleNameDative = "Gefärbten Folge Mal Anders",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SNotColourFlash.InitialWord] = new()
                {
                    // English: What was {1} in the displayed word sequence in {0}?
                    // Example: What was first in the displayed word sequence in Not Colour Flash?
                    Question = "Was war bei {0} das {1}e Wort in der angezeigten Wortfolge?",
                },
                [SNotColourFlash.InitialColour] = new()
                {
                    // English: What was {1} in the displayed colour sequence in {0}?
                    // Example: What was first in the displayed colour sequence in Not Colour Flash?
                    Question = "Was war bei {0} die {1}e Farbe in der angezeigten Farbfolge?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                        ["White"] = "Weiß",
                    },
                },
            },
        },

        // Not Connection Check
        [typeof(SNotConnectionCheck)] = new()
        {
            ModuleName = "Verbindungsprüfung Mal Anders",
            Questions = new()
            {
                [SNotConnectionCheck.Flashes] = new()
                {
                    // English: What symbol flashed on the {1} button in {0}?
                    // Example: What symbol flashed on the top left button in Not Connection Check?
                    Question = "Welches Symbol ist bei {0} auf der {1} Taste aufgeleuchtet?",
                    Arguments = new()
                    {
                        ["top left"] = "oberen linken",
                        ["top right"] = "oberen rechten",
                        ["bottom left"] = "unteren linken",
                        ["bottom right"] = "unteren rechten",
                    },
                },
                [SNotConnectionCheck.Values] = new()
                {
                    // English: What was the value of the {1} button in {0}?
                    // Example: What was the value of the top left button in Not Connection Check?
                    Question = "Welchen Wert hatte bei {0} die {1} Taste?",
                    Arguments = new()
                    {
                        ["top left"] = "obere linke",
                        ["top right"] = "obere rechte",
                        ["bottom left"] = "untere linke",
                        ["bottom right"] = "untere rechte",
                    },
                },
            },
        },

        // Not Coordinates
        [typeof(SNotCoordinates)] = new()
        {
            ModuleName = "Koordinaten Mal Anders",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SNotCoordinates.SquareCoords] = new()
                {
                    // English: Which coordinate was part of the square in {0}?
                    Question = "Welche Koordinate war bei {0} Teil des Quadrats?",
                },
            },
        },

        // Not Double-Oh
        [typeof(SNotDoubleOh)] = new()
        {
            ModuleName = "Doppel-Null Mal Anders",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SNotDoubleOh.Position] = new()
                {
                    // English: What was the {1} displayed position in the second stage of {0}?
                    // Example: What was the first displayed position in the second stage of Not Double-Oh?
                    Question = "Was war bei {0} die {1}e angezeigte Position in der zweiten Stufe?",
                },
            },
        },

        // Not Keypad
        [typeof(SNotKeypad)] = new()
        {
            ModuleName = "Tastenfeld Mal Anders",
            Questions = new()
            {
                [SNotKeypad.Color] = new()
                {
                    // English: What color flashed {1} in the final sequence in {0}?
                    // Example: What color flashed first in the final sequence in Not Keypad?
                    Question = "Welche Farbe ist bei {0} in der letzten Sequenz als {1}e aufgeleuchtet?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["orange"] = "orange",
                        ["yellow"] = "gelb",
                        ["green"] = "grün",
                        ["cyan"] = "türkis",
                        ["blue"] = "blau",
                        ["purple"] = "lila",
                        ["magenta"] = "magenta",
                        ["pink"] = "pink",
                        ["brown"] = "braun",
                        ["grey"] = "grau",
                        ["white"] = "weiß",
                    },
                },
                [SNotKeypad.Symbol] = new()
                {
                    // English: Which symbol was on the button that flashed {1} in the final sequence in {0}?
                    // Example: Which symbol was on the button that flashed first in the final sequence in Not Keypad?
                    Question = "Welches Symbol war bei {0} auf der Taste, die als {1}e in der letzten Sequenz aufgeleuchtet ist?",
                },
            },
        },

        // Not Maze
        [typeof(SNotMaze)] = new()
        {
            ModuleName = "Labyrinth Mal Anders",
            Questions = new()
            {
                [SNotMaze.StartingDistance] = new()
                {
                    // English: What was the starting distance in {0}?
                    Question = "Was war bei {0} die Anfangsdistanz?",
                },
            },
        },

        // Not Morse Code
        [typeof(SNotMorseCode)] = new()
        {
            ModuleName = "Morsezeichen Mal Anders",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SNotMorseCode.Word] = new()
                {
                    // English: What was the {1} correct word you submitted in {0}?
                    // Example: What was the first correct word you submitted in Not Morse Code?
                    Question = "Was war bei {0} das {1}e korrekt eingegebene Wort?",
                },
            },
        },

        // Not Morsematics
        [typeof(SNotMorsematics)] = new()
        {
            ModuleName = "Morsematik Mal Anders",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SNotMorsematics.Word] = new()
                {
                    // English: What was the transmitted word on {0}?
                    Question = "Was war bei {0} das übertragene Wort?",
                },
            },
        },

        // Not Murder
        [typeof(SNotMurder)] = new()
        {
            ModuleName = "Mord Mal Anders",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SNotMurder.Room] = new()
                {
                    // English: What room was {1} in initially on {0}?
                    // Example: What room was Miss Scarlett in initially on Not Murder?
                    Question = "In welchem Zimmer war {1} bei {0} am Anfang?",
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
                        ["Ballroom"] = "Musikzimmer",
                        ["Billiard Room"] = "Billardzimmer",
                        ["Conservatory"] = "Wintergarten",
                        ["Dining Room"] = "Speisezimmer",
                        ["Hall"] = "Halle",
                        ["Kitchen"] = "Küche",
                        ["Library"] = "Bibliothek",
                        ["Lounge"] = "Salon",
                        ["Study"] = "Arbeitszimmer",
                    },
                },
                [SNotMurder.Weapon] = new()
                {
                    // English: What weapon did {1} possess initially on {0}?
                    // Example: What weapon did Miss Scarlett possess initially on Not Murder?
                    Question = "Welche Waffe hatte {1} bei {0} am Anfang?",
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
                        ["Candlestick"] = "Kerzenleuchter",
                        ["Dagger"] = "Dolch",
                        ["Lead Pipe"] = "Bleirohr",
                        ["Revolver"] = "Pistole",
                        ["Rope"] = "Seil",
                        ["Spanner"] = "Rohrzange",
                    },
                },
            },
            Discriminators = new()
            {
                [SNotMurder.Present] = new()
                {
                    // English: the Not Murder where {0} was present
                    // Example: the Not Murder where he was present
                    Discriminator = "dem Mord Mal Anders, bei dem {0} anwesend war,",
                    Arguments = new()
                    {
                        ["he"] = "er",
                        ["she"] = "sie",
                    },
                },
                [SNotMurder.InitialWeapon] = new()
                {
                    // English: the Not Murder where {0} initially held the {1}
                    // Example: the Not Murder where he initially held the Candlestick
                    Discriminator = "dem Mord Mal Anders, bei dem {0} am Anfang {1} hatte,",
                    Arguments = new()
                    {
                        ["he"] = "er",
                        ["she"] = "sie",
                        ["Candlestick"] = "den Kerzenleuchter",
                        ["Dagger"] = "den Dolch",
                        ["Lead Pipe"] = "das Bleirohr",
                        ["Revolver"] = "die Pistole",
                    },
                },
                [SNotMurder.InitialRoom] = new()
                {
                    // English: the Not Murder where {0} started in the {1}
                    // Example: the Not Murder where he started in the Ballroom
                    Discriminator = "dem Mord Mal Anders, bei dem {0} im {1} anfing,",
                    Arguments = new()
                    {
                        ["he"] = "er",
                        ["she"] = "sie",
                        ["Ballroom"] = "Musikzimmer",
                        ["Billiard Room"] = "Billardzimmer",
                        ["Conservatory"] = "Wintergarten",
                        ["Dining Room"] = "Speisezimmer",
                    },
                },
            },
        },

        // Not Number Pad
        [typeof(SNotNumberPad)] = new()
        {
            ModuleName = "Zahlenfeld Mal Anders",
            Questions = new()
            {
                [SNotNumberPad.Flashes] = new()
                {
                    // English: Which of these numbers {1} at the {2} stage of {0}?
                    // Example: Which of these numbers flashed at the first stage of Not Number Pad?
                    Question = "Welche Zahl ist bei {0} in der {2}en Stufe {1}?",
                    Arguments = new()
                    {
                        ["flashed"] = "aufgeleuchtet",
                        ["did not flash"] = "nicht aufgeleuchtet",
                    },
                },
            },
        },

        // Not Password
        [typeof(SNotPassword)] = new()
        {
            ModuleName = "Passwort Mal Anders",
            Questions = new()
            {
                [SNotPassword.Letter] = new()
                {
                    // English: Which letter was missing from {0}?
                    Question = "Welcher Buchstabe hat bei {0} gefehlt?",
                },
            },
        },

        // Not Perspective Pegs
        [typeof(SNotPerspectivePegs)] = new()
        {
            ModuleName = "Perspektivstöpsel Mal Anders",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SNotPerspectivePegs.Position] = new()
                {
                    // English: What was the position of the {1} flashing peg on {0}?
                    // Example: What was the position of the first flashing peg on Not Perspective Pegs?
                    Question = "An welcher Position befand sich bei {0} der als {1}e aufgeleuchtete Stöpsel?",
                    Answers = new()
                    {
                        ["top"] = "oben",
                        ["top-right"] = "oben rechts",
                        ["bottom-right"] = "unten rechts",
                        ["bottom-left"] = "unten links",
                        ["top-left"] = "oben links",
                    },
                },
                [SNotPerspectivePegs.Perspective] = new()
                {
                    // English: From what perspective did the {1} peg flash on {0}?
                    // Example: From what perspective did the first peg flash on Not Perspective Pegs?
                    Question = "Aus welcher Perspektive leuchtete bei {0} der {1}e Stöpsel auf?",
                    Answers = new()
                    {
                        ["top"] = "oben",
                        ["top-right"] = "oben rechts",
                        ["bottom-right"] = "unten rechts",
                        ["bottom-left"] = "unten links",
                        ["top-left"] = "oben links",
                    },
                },
                [SNotPerspectivePegs.Color] = new()
                {
                    // English: What was the color of the {1} flashing peg on {0}?
                    // Example: What was the color of the first flashing peg on Not Perspective Pegs?
                    Question = "Welche Farbe hatte bei {0} der als {1}e aufgeleuchtete Stöpsel?",
                    Answers = new()
                    {
                        ["blue"] = "blau",
                        ["green"] = "grün",
                        ["purple"] = "lila",
                        ["red"] = "rot",
                        ["yellow"] = "gelb",
                    },
                },
            },
        },

        // Not Piano Keys
        [typeof(SNotPianoKeys)] = new()
        {
            ModuleName = "Klavier Mal Anders",
            Questions = new()
            {
                [SNotPianoKeys.FirstSymbol] = new()
                {
                    // English: What was the first displayed symbol on {0}?
                    Question = "Was war bei {0} das erste Symbol?",
                },
                [SNotPianoKeys.SecondSymbol] = new()
                {
                    // English: What was the second displayed symbol on {0}?
                    Question = "Was war bei {0} das zweite Symbol?",
                },
                [SNotPianoKeys.ThirdSymbol] = new()
                {
                    // English: What was the third displayed symbol on {0}?
                    Question = "Was war bei {0} das dritte Symbol?",
                },
            },
        },

        // Not Red Arrows
        [typeof(SNotRedArrows)] = new()
        {
            ModuleName = "Rote Pfeile Mal Anders",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SNotRedArrows.Start] = new()
                {
                    // English: What was the starting number in {0}?
                    Question = "Was war bei {0} die Startzahl?",
                },
            },
        },

        // Not Simaze
        [typeof(SNotSimaze)] = new()
        {
            ModuleName = "Simobyrinth Mal Anders",
            Questions = new()
            {
                [SNotSimaze.Maze] = new()
                {
                    // English: Which maze was used in {0}?
                    Question = "Welches Labyrinth wurde bei {0} verwendet?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["orange"] = "orange",
                        ["yellow"] = "gelb",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["purple"] = "lila",
                    },
                },
                [SNotSimaze.Start] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "Was war bei {0} die Startposition?",
                    Answers = new()
                    {
                        ["(red, red)"] = "(rot, rot)",
                        ["(red, orange)"] = "(rot, orange)",
                        ["(red, yellow)"] = "(rot, gelb)",
                        ["(red, green)"] = "(rot, grün)",
                        ["(red, blue)"] = "(rot, blau)",
                        ["(red, purple)"] = "(rot, lila)",
                        ["(orange, red)"] = "(orange, rot)",
                        ["(orange, orange)"] = "(orange, orange)",
                        ["(orange, yellow)"] = "(orange, gelb)",
                        ["(orange, green)"] = "(orange, grün)",
                        ["(orange, blue)"] = "(orange, blau)",
                        ["(orange, purple)"] = "(orange, lila)",
                        ["(yellow, red)"] = "(gelb, rot)",
                        ["(yellow, orange)"] = "(gelb, orange)",
                        ["(yellow, yellow)"] = "(gelb, gelb)",
                        ["(yellow, green)"] = "(gelb, grün)",
                        ["(yellow, blue)"] = "(gelb, blau)",
                        ["(yellow, purple)"] = "(gelb, lila)",
                        ["(green, red)"] = "(grün, rot)",
                        ["(green, orange)"] = "(grün, orange)",
                        ["(green, yellow)"] = "(grün, gelb)",
                        ["(green, green)"] = "(grün, grün)",
                        ["(green, blue)"] = "(grün, blau)",
                        ["(green, purple)"] = "(grün, lila)",
                        ["(blue, red)"] = "(blau, rot)",
                        ["(blue, orange)"] = "(blau, orange)",
                        ["(blue, yellow)"] = "(blau, gelb)",
                        ["(blue, green)"] = "(blau, grün)",
                        ["(blue, blue)"] = "(blau, blau)",
                        ["(blue, purple)"] = "(blau, lila)",
                        ["(purple, red)"] = "(lila, rot)",
                        ["(purple, orange)"] = "(lila, orange)",
                        ["(purple, yellow)"] = "(lila, gelb)",
                        ["(purple, green)"] = "(lila, grün)",
                        ["(purple, blue)"] = "(lila, blau)",
                        ["(purple, purple)"] = "(lila, lila)",
                    },
                },
                [SNotSimaze.Goal] = new()
                {
                    // English: What was the goal position in {0}?
                    Question = "Was war bei {0} die Zielposition?",
                    Answers = new()
                    {
                        ["(red, red)"] = "(rot, rot)",
                        ["(red, orange)"] = "(rot, orange)",
                        ["(red, yellow)"] = "(rot, gelb)",
                        ["(red, green)"] = "(rot, grün)",
                        ["(red, blue)"] = "(rot, blau)",
                        ["(red, purple)"] = "(rot, lila)",
                        ["(orange, red)"] = "(orange, rot)",
                        ["(orange, orange)"] = "(orange, orange)",
                        ["(orange, yellow)"] = "(orange, gelb)",
                        ["(orange, green)"] = "(orange, grün)",
                        ["(orange, blue)"] = "(orange, blau)",
                        ["(orange, purple)"] = "(orange, lila)",
                        ["(yellow, red)"] = "(gelb, rot)",
                        ["(yellow, orange)"] = "(gelb, orange)",
                        ["(yellow, yellow)"] = "(gelb, gelb)",
                        ["(yellow, green)"] = "(gelb, grün)",
                        ["(yellow, blue)"] = "(gelb, blau)",
                        ["(yellow, purple)"] = "(gelb, lila)",
                        ["(green, red)"] = "(grün, rot)",
                        ["(green, orange)"] = "(grün, orange)",
                        ["(green, yellow)"] = "(grün, gelb)",
                        ["(green, green)"] = "(grün, grün)",
                        ["(green, blue)"] = "(grün, blau)",
                        ["(green, purple)"] = "(grün, lila)",
                        ["(blue, red)"] = "(blau, rot)",
                        ["(blue, orange)"] = "(blau, orange)",
                        ["(blue, yellow)"] = "(blau, gelb)",
                        ["(blue, green)"] = "(blau, grün)",
                        ["(blue, blue)"] = "(blau, blau)",
                        ["(blue, purple)"] = "(blau, lila)",
                        ["(purple, red)"] = "(lila, rot)",
                        ["(purple, orange)"] = "(lila, orange)",
                        ["(purple, yellow)"] = "(lila, gelb)",
                        ["(purple, green)"] = "(lila, grün)",
                        ["(purple, blue)"] = "(lila, blau)",
                        ["(purple, purple)"] = "(lila, lila)",
                    },
                },
            },
        },

        // Not Text Field
        [typeof(SNotTextField)] = new()
        {
            ModuleName = "Textfeld Mal Anders",
            Questions = new()
            {
                [SNotTextField.BackgroundLetter] = new()
                {
                    // English: Which letter appeared 9 times at the start of {0}?
                    Question = "Welcher Buchstabe war bei {0} am Anfang 9-mal vorhanden?",
                },
                [SNotTextField.InitialPresses] = new()
                {
                    // English: Which letter was pressed in the first stage of {0}?
                    Question = "Welcher Buchstabe wurde bei {0} in der ersten Stufe gedrückt?",
                },
            },
        },

        // Not The Bulb
        [typeof(SNotTheBulb)] = new()
        {
            ModuleName = "Die Glühlampe Mal Anders",
            ModuleNameDative = "Glühlampe Mal Anders",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SNotTheBulb.Word] = new()
                {
                    // English: What word flashed on {0}?
                    Question = "Welches Wort wurde bei {0} geblinkt?",
                },
                [SNotTheBulb.Color] = new()
                {
                    // English: What color was the bulb on {0}?
                    Question = "Welche Farbe hatte die Glühlampe bei {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Yellow"] = "Gelb",
                        ["Purple"] = "Lila",
                        ["White"] = "Weiß",
                    },
                },
                [SNotTheBulb.ScrewCap] = new()
                {
                    // English: What was the material of the screw cap on {0}?
                    Question = "Welches Material hatte der Schraubverschluss bei {0}?",
                    Answers = new()
                    {
                        ["Copper"] = "Kupfer",
                        ["Silver"] = "Silber",
                        ["Gold"] = "Gold",
                        ["Plastic"] = "Plastik",
                        ["Carbon Fibre"] = "Kohlenstofffaser",
                        ["Ceramic"] = "Keramik",
                    },
                },
            },
        },

        // Not The Button
        [typeof(SNotTheButton)] = new()
        {
            ModuleName = "Der Knopf Mal Anders",
            ModuleNameDative = "Knopf Mal Anders",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SNotTheButton.LightColor] = new()
                {
                    // English: What colors did the light glow in {0}?
                    Question = "In welchen Farben leuchtete das Licht bei {0}?",
                    Answers = new()
                    {
                        ["white"] = "weiß",
                        ["red"] = "rot",
                        ["yellow"] = "gelb",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["white/red"] = "weiß/rot",
                        ["white/yellow"] = "weiß/gelb",
                        ["white/green"] = "weiß/grün",
                        ["white/blue"] = "weiß/blau",
                        ["red/yellow"] = "rot/gelb",
                        ["red/green"] = "rot/grün",
                        ["red/blue"] = "rot/blau",
                        ["yellow/green"] = "gelb/grün",
                        ["yellow/blue"] = "gelb/blau",
                        ["green/blue"] = "grün/blau",
                    },
                },
            },
        },

        // Not The Plunger Button
        [typeof(SNotThePlungerButton)] = new()
        {
            ModuleName = "Der Kolbenknopf Mal Anders",
            ModuleNameDative = "Kolbenknopf Mal Anders",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SNotThePlungerButton.Background] = new()
                {
                    // English: What color did the background flash in {0}?
                    Question = "In welcher Farbe blinkte der Hintergrund bei {0}?",
                    Answers = new()
                    {
                        ["Black"] = "Schwarz",
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Cyan"] = "Türkis",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                        ["White"] = "Weiß",
                    },
                },
            },
        },

        // Not The Screw
        [typeof(SNotTheScrew)] = new()
        {
            ModuleName = "Die Schraube Mal Anders",
            ModuleNameDative = "Schraube Mal Anders",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SNotTheScrew.InitialPosition] = new()
                {
                    // English: What was the initial position in {0}?
                    Question = "Was war bei {0} die Startposition?",
                },
            },
        },

        // Not Who’s on First
        [typeof(SNotWhosOnFirst)] = new()
        {
            ModuleName = "Who’s On First Mal Anders",
            Questions = new()
            {
                [SNotWhosOnFirst.PressedPosition] = new()
                {
                    // English: In which position was the button you pressed in the {1} stage on {0}?
                    // Example: In which position was the button you pressed in the first stage on Not Who’s on First?
                    Question = "Welche Taste wurde bei {0} in der {1}en Stufe gedrückt?",
                    Answers = new()
                    {
                        ["top left"] = "oben links",
                        ["top right"] = "oben rechts",
                        ["middle left"] = "Mitte links",
                        ["middle right"] = "Mitte rechts",
                        ["bottom left"] = "unten links",
                        ["bottom right"] = "unten rechts",
                    },
                },
                [SNotWhosOnFirst.PressedLabel] = new()
                {
                    // English: What was the label on the button you pressed in the {1} stage on {0}?
                    // Example: What was the label on the button you pressed in the first stage on Not Who’s on First?
                    Question = "Was stand auf der Taste, die bei {0} in der {1}en Stufe gedrückt wurde?",
                },
                [SNotWhosOnFirst.ReferencePosition] = new()
                {
                    // English: In which position was the reference button in the {1} stage on {0}?
                    // Example: In which position was the reference button in the first stage on Not Who’s on First?
                    Question = "Welche Taste war bei {0} in der {1}en Stufe die Referenztaste?",
                    Answers = new()
                    {
                        ["top left"] = "oben links",
                        ["top right"] = "oben rechts",
                        ["middle left"] = "Mitte links",
                        ["middle right"] = "Mitte rechts",
                        ["bottom left"] = "unten links",
                        ["bottom right"] = "unten rechts",
                    },
                },
                [SNotWhosOnFirst.ReferenceLabel] = new()
                {
                    // English: What was the label on the reference button in the {1} stage on {0}?
                    // Example: What was the label on the reference button in the first stage on Not Who’s on First?
                    Question = "Was stand auf der Referenztaste, die bei {0} in der {1}en Stufe verwendet wurde?",
                },
                [SNotWhosOnFirst.Sum] = new()
                {
                    // English: What was the calculated number in the second stage on {0}?
                    Question = "Was war bei {0} in der zweiten Stufe die berechnete Zahl?",
                },
            },
        },

        // Not Word Search
        [typeof(SNotWordSearch)] = new()
        {
            ModuleName = "Wortsuche Mal Anders",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SNotWordSearch.Missing] = new()
                {
                    // English: Which of these consonants was missing in {0}?
                    Question = "Welcher dieser Konsonanten hat bei {0} gefehlt?",
                },
                [SNotWordSearch.FirstPress] = new()
                {
                    // English: What was the first correctly pressed letter in {0}?
                    Question = "Welcher Buchstabe wurde bei {0} als erstes korrekt gedrückt?",
                },
            },
        },

        // Not X01
        [typeof(SNotX01)] = new()
        {
            ModuleName = "X01 Mal Anders",
            Questions = new()
            {
                [SNotX01.SectorValues] = new()
                {
                    // English: Which sector value {1} present on {0}?
                    // Example: Which sector value was present on Not X01?
                    Question = "Welcher Sektorwert war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["was"] = "anwesend",
                        ["was not"] = "abwesend",
                    },
                },
            },
        },

        // Not X-Ray
        [typeof(SNotXRay)] = new()
        {
            ModuleName = "Röntgen Mal Anders",
            Questions = new()
            {
                [SNotXRay.ScannerColor] = new()
                {
                    // English: What was the scanner color in {0}?
                    Question = "Welche Farbe hatte bei {0} der Scanner?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Blue"] = "Blau",
                        ["White"] = "Weiß",
                    },
                },
                [SNotXRay.Table] = new()
                {
                    // English: What table were we in in {0} (numbered 1–8 in reading order in the manual)?
                    Question = "In welchem Diagramm befanden wir uns bei {0} (im Handbuch von 1 bis 8 in Lesereihenfolge nummeriert)?",
                },
                [SNotXRay.Directions] = new()
                {
                    // English: What direction was button {1} in {0}?
                    // Example: What direction was button 1 in Not X-Ray?
                    Question = "Mit welcher Richtung war Taste {1} bei {0} assoziiert?",
                    Answers = new()
                    {
                        ["Up"] = "Hoch",
                        ["Right"] = "Rechts",
                        ["Down"] = "Runter",
                        ["Left"] = "Links",
                    },
                },
                [SNotXRay.Buttons] = new()
                {
                    // English: Which button went {1} in {0}?
                    // Example: Which button went up in Not X-Ray?
                    Question = "Welche Taste ging bei {0} nach {1}?",
                    Arguments = new()
                    {
                        ["up"] = "oben",
                        ["right"] = "rechts",
                        ["down"] = "unten",
                        ["left"] = "links",
                    },
                },
            },
        },

        // Numbered Buttons
        [typeof(SNumberedButtons)] = new()
        {
            ModuleName = "Nummerierte Knöpfe",
            ModuleNameDative = "Nummerierten Knöpfen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SNumberedButtons.Buttons] = new()
                {
                    // English: Which number was correctly pressed on {0}?
                    Question = "Welche Zahl wurde bei {0} korrekt gedrückt?",
                },
            },
        },

        // The Number Game
        [typeof(SNumberGame)] = new()
        {
            ModuleName = "Zahlenspiel",
            Questions = new()
            {
                [SNumberGame.Maximum] = new()
                {
                    // English: What was the maximum number in {0}?
                    Question = "Was war bei {0} die Höchstzahl?",
                },
            },
        },

        // Numbers
        [typeof(SNumbers)] = new()
        {
            ModuleName = "Zahlen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SNumbers.TwoDigit] = new()
                {
                    // English: What two-digit number was given in {0}?
                    Question = "Was war bei {0} die zweistellige Zahl?",
                },
            },
        },

        // Numpath
        [typeof(SNumpath)] = new()
        {
            ModuleName = "Zahlenpfad",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SNumpath.Color] = new()
                {
                    // English: What was the color of the number on {0}?
                    Question = "Welche Farbe hatte bei {0} die Zahl auf dem Display?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                    },
                },
                [SNumpath.Digit] = new()
                {
                    // English: What was the number displayed on {0}?
                    Question = "Welche Zahl war bei {0} auf dem Display?",
                },
            },
        },

        // Object Shows
        [typeof(SObjectShows)] = new()
        {
            Questions = new()
            {
                [SObjectShows.Contestants] = new()
                {
                    // English: Which of these was a contestant on {0}?
                    Question = "Wer war bei {0} ein Kandidat?",
                },
            },
        },

        // The Octadecayotton
        [typeof(SOctadecayotton)] = new()
        {
            Questions = new()
            {
                [SOctadecayotton.Sphere] = new()
                {
                    // English: What was the starting sphere in {0}?
                    Question = "Was war bei {0} die Startkugel?",
                },
                [SOctadecayotton.Rotations] = new()
                {
                    // English: What was one of the subrotations in the {1} rotation in {0}?
                    // Example: What was one of the subrotations in the first rotation in The Octadecayotton?
                    Question = "Welche Teilrotation kam bei {0} bei der {1}en Rotation vor?",
                },
            },
        },

        // Odd One Out
        [typeof(SOddOneOut)] = new()
        {
            ModuleName = "Was Nicht Passt",
            Questions = new()
            {
                [SOddOneOut.Button] = new()
                {
                    // English: What was the button you pressed in the {1} stage of {0}?
                    // Example: What was the button you pressed in the first stage of Odd One Out?
                    Question = "Was war bei {0} die in der {1}en Stufe gedrückte Taste?",
                },
            },
        },

        // Off Keys
        [typeof(SOffKeys)] = new()
        {
            ModuleName = "Verstimmte Tasten",
            ModuleNameDative = "Verstimmten Tasten",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SOffKeys.IncorrectPitch] = new()
                {
                    // English: Which of these keys played at an incorrect pitch in {0}?
                    Question = "Welche dieser Tasten war bei {0} verstimmt?",
                },
                [SOffKeys.Runes] = new()
                {
                    // English: Which of these runes was displayed in {0}?
                    Question = "Welche dieser Runen war bei {0} zu sehen?",
                },
            },
        },

        // Off-White Cipher
        [typeof(SOffWhiteCipher)] = new()
        {
            ModuleName = "Eierschalengeheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SOffWhiteCipher.TopDisplay] = new()
                {
                    // English: What was on the top display in {0}?
                    Question = "Was stand bei {0} auf dem oberen Display?",
                },
                [SOffWhiteCipher.MiddleDisplay] = new()
                {
                    // English: What was on the middle display in {0}?
                    Question = "Was stand bei {0} auf dem mittleren Display?",
                },
                [SOffWhiteCipher.BottomDisplay] = new()
                {
                    // English: What was on the bottom display in {0}?
                    Question = "Was stand bei {0} auf dem unteren Display?",
                },
            },
        },

        // Old AI
        [typeof(SOldAI)] = new()
        {
            ModuleName = "Alte KI",
            ModuleNameDative = "Alten KI",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SOldAI.Group] = new()
                {
                    // English: What was the {1} of the numbers shown in {0}?
                    // Example: What was the group of the numbers shown in Old AI?
                    Question = "Zu welcher {1} gehörten die bei {0} angezeigten Zahlen?",
                    Arguments = new()
                    {
                        ["group"] = "Gruppe",
                        ["sub-group"] = "Untergruppe",
                    },
                },
            },
        },

        // Old Fogey
        [typeof(SOldFogey)] = new()
        {
            ModuleName = "Alter Kauz",
            ModuleNameDative = "Alten Kauz",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SOldFogey.StartingColor] = new()
                {
                    // English: What was the initial color of the status light in {0}?
                    Question = "Welche Farbe hatte bei {0} das Statuslicht am Anfang?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Yellow"] = "Gelb",
                        ["Blue"] = "Blau",
                        ["Magenta"] = "Magenta",
                        ["Cyan"] = "Türkis",
                        ["White"] = "Weiß",
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
                    Question = "Was war bei {0} der Anfangsartikel?",
                },
                [SOneLinksToAll.End] = new()
                {
                    // English: What was the ending article in {0}?
                    Question = "Was war bei {0} der Zielartikel?",
                },
            },
        },

        // Only Connect
        [typeof(SOnlyConnect)] = new()
        {
            Questions = new()
            {
                [SOnlyConnect.Hieroglyphs] = new()
                {
                    // English: Which Egyptian hieroglyph was in the {1} in {0}?
                    // Example: Which Egyptian hieroglyph was in the top left in Only Connect?
                    Question = "Welches ägyptische Hieroglyph war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["top left"] = "oben links",
                        ["top middle"] = "oben Mitte",
                        ["top right"] = "oben rechts",
                        ["bottom left"] = "unten links",
                        ["bottom middle"] = "unten Mitte",
                        ["bottom right"] = "unten rechts",
                    },
                    Answers = new()
                    {
                        ["Two Reeds"] = "Zwei Riede",
                        ["Lion"] = "Löwe",
                        ["Twisted Flax"] = "Gedrehter Flachs",
                        ["Horned Viper"] = "Gehörnte Viper",
                        ["Water"] = "Wasser",
                        ["Eye of Horus"] = "Auge des Horus",
                    },
                },
            },
        },

        // Orange Arrows
        [typeof(SOrangeArrows)] = new()
        {
            Questions = new()
            {
                [SOrangeArrows.Sequences] = new()
                {
                    // English: What was the {1} arrow on the display of the {2} stage of {0}?
                    // Example: What was the first arrow on the display of the first stage of Orange Arrows?
                    Question = "Was war bei {0} in der {2} Stufe der {1}e Pfeil auf dem Display?",
                    Answers = new()
                    {
                        ["Up"] = "Hoch",
                        ["Right"] = "Rechts",
                        ["Down"] = "Runter",
                        ["Left"] = "Links",
                    },
                },
            },
        },

        // Orange Cipher
        [typeof(SOrangeCipher)] = new()
        {
            ModuleName = "Orangene Geheimschrift",
            ModuleNameDative = "Orangenen Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SOrangeCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Orange Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "ober",
                        ["middle"] = "mittler",
                        ["bottom"] = "unter",
                    },
                },
            },
        },

        // Ordered Keys
        [typeof(SOrderedKeys)] = new()
        {
            Questions = new()
            {
                [SOrderedKeys.Colors] = new()
                {
                    // English: What color was this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "Welche Farbe hatte bei {0} diese Taste in der {1}en Stufe?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Yellow"] = "Gelb",
                        ["Cyan"] = "Türkis",
                        ["Magenta"] = "Magenta",
                    },
                },
                [SOrderedKeys.Labels] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What was the label of this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "Welche Aufschrift hatte bei {0} diese Taste in der {1}en Stufe?",
                },
                [SOrderedKeys.LabelColors] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was the label of this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "Welche Farbe hatte bei {0} die Aufschrift dieser Taste in der {1}en Stufe?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Yellow"] = "Gelb",
                        ["Cyan"] = "Türkis",
                        ["Magenta"] = "Magenta",
                    },
                },
            },
        },

        // Order Picking
        [typeof(SOrderPicking)] = new()
        {
            ModuleName = "Kommissionierung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SOrderPicking.Order] = new()
                {
                    // English: What was the order ID in the {1} order of {0}?
                    // Example: What was the order ID in the first order of Order Picking?
                    Question = "Was war bei {0} die Auftragsnummer des {1}en Auftrags?",
                },
                [SOrderPicking.Product] = new()
                {
                    // English: What was the product ID in the {1} order of {0}?
                    // Example: What was the product ID in the first order of Order Picking?
                    Question = "Was war bei {0} die Produktnummer des {1}en Auftrags?",
                },
                [SOrderPicking.Pallet] = new()
                {
                    // English: What was the pallet in the {1} order of {0}?
                    // Example: What was the pallet in the first order of Order Picking?
                    Question = "Was war bei {0} die Palette für den {1}en Auftrag?",
                },
            },
        },

        // Orientation Cube
        [typeof(SOrientationCube)] = new()
        {
            ModuleName = "Orientierungswürfel",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SOrientationCube.InitialObserverPosition] = new()
                {
                    // English: What was the observer’s initial position in {0}?
                    Question = "Was war bei {0} die Anfangsposition des Beobachters?",
                    Answers = new()
                    {
                        ["front"] = "vorne",
                        ["left"] = "links",
                        ["back"] = "hinten",
                        ["right"] = "rechts",
                    },
                },
            },
        },

        // Orientation Hypercube
        [typeof(SOrientationHypercube)] = new()
        {
            ModuleName = "Orientierungshyperwürfel",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SOrientationHypercube.InitialFaceColour] = new()
                {
                    // English: What was the initial colour of the {1} face in {0}?
                    // Example: What was the initial colour of the right face in Orientation Hypercube?
                    Question = "Was war bei {0} die Anfangsfarbe der {1} Seite?",
                    Arguments = new()
                    {
                        ["right"] = "rechten",
                        ["left"] = "linken",
                        ["top"] = "oberen",
                        ["bottom"] = "unteren",
                        ["back"] = "hinteren",
                        ["front"] = "vorderen",
                        ["zag"] = "zackeren",
                        ["zig"] = "zickeren",
                    },
                    Answers = new()
                    {
                        ["black"] = "schwarz",
                        ["red"] = "rot",
                        ["green"] = "grün",
                        ["yellow"] = "gelb",
                        ["blue"] = "blau",
                        ["magenta"] = "magenta",
                        ["cyan"] = "türkis",
                        ["white"] = "weiß",
                    },
                },
                [SOrientationHypercube.InitialObserverPosition] = new()
                {
                    // English: What was the observer’s initial position in {0}?
                    Question = "Was war bei {0} die Anfangsposition des Beobachters?",
                    Answers = new()
                    {
                        ["front"] = "vorne",
                        ["left"] = "links",
                        ["back"] = "hinten",
                        ["right"] = "rechts",
                    },
                },
            },
        },

        // Painting Cube
        [typeof(SPaintingCube)] = new()
        {
            ModuleName = "Malfarbenwürfel",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SPaintingCube.MissingColor] = new()
                {
                    // English: What color was missing in {0}?
                    Question = "Welche Farbe hat bei {0} gefehlt?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Indigo"] = "Indigo",
                        ["Violet"] = "Violett",
                    },
                },
            },
        },

        // Palindromes
        [typeof(SPalindromes)] = new()
        {
            ModuleName = "Palindrome",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SPalindromes.Numbers] = new()
                {
                    // English: What was {1}’s {2} digit from the right in {0}?
                    // Example: What was X’s first digit from the right in Palindromes?
                    Question = "Was war bei {0} die {2}e Ziffer von rechts {1}?",
                    Arguments = new()
                    {
                        ["X"] = "von X",
                        ["Y"] = "von Y",
                        ["Z"] = "von Z",
                        ["the screen"] = "auf dem Display",
                    },
                },
            },
        },

        // Papa’s Pizzeria
        [typeof(SPapasPizzeria)] = new()
        {
            ModuleName = "Papas Pizzeria",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SPapasPizzeria.Letter] = new()
                {
                    // English: What was the letter in the order number on {0}?
                    Question = "Was war bei {0} der Buchstabe in der Bestellnummer?",
                },
                [SPapasPizzeria.Digit] = new()
                {
                    // English: What was the {1} digit in the order number on {0}?
                    // Example: What was the first digit in the order number on Papa’s Pizzeria?
                    Question = "Was war bei {0} die {1}e Ziffer in der Bestellnummer?",
                },
            },
        },

        // Parity
        [typeof(SParity)] = new()
        {
            ModuleName = "Parität",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SParity.Display] = new()
                {
                    // English: What was shown on the display on {0}?
                    Question = "Was war bei {0} auf dem Display?",
                },
            },
        },

        // Partial Derivatives
        [typeof(SPartialDerivatives)] = new()
        {
            ModuleName = "Partielle Ableitungen",
            ModuleNameDative = "Partiellen Ableitungen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SPartialDerivatives.LedColors] = new()
                {
                    // English: What was the LED color in the {1} stage of {0}?
                    // Example: What was the LED color in the first stage of Partial Derivatives?
                    Question = "Was war bei {0} die LED-Farbe in der {1}en Stufe?",
                    Answers = new()
                    {
                        ["blue"] = "blau",
                        ["green"] = "grün",
                        ["orange"] = "orange",
                        ["purple"] = "lila",
                        ["red"] = "rot",
                        ["yellow"] = "gelb",
                    },
                },
                [SPartialDerivatives.Terms] = new()
                {
                    // English: What was the {1} term in {0}?
                    // Example: What was the first term in Partial Derivatives?
                    Question = "Was war bei {0} der {1}e Term?",
                },
            },
        },

        // Passport Control
        [typeof(SPassportControl)] = new()
        {
            ModuleName = "Passkontrolle",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SPassportControl.Passenger] = new()
                {
                    // English: What was the passport expiration year of the {1} inspected passenger in {0}?
                    // Example: What was the passport expiration year of the first inspected passenger in Passport Control?
                    Question = "Was war bei {0} das Auslaufjahr des {1}en inspizierten Passagiers?",
                },
            },
        },

        // Password Destroyer
        [typeof(SPasswordDestroyer)] = new()
        {
            ModuleName = "Passwortzerstörer",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SPasswordDestroyer.TwoFactorV2] = new()
                {
                    // English: What was the 2FAST™ value when you solved {0}?
                    Question = "Was war beim Entschärfen von {0} der 2FAST™-Wert?",
                },
            },
        },

        // Pattern Cube
        [typeof(SPatternCube)] = new()
        {
            ModuleName = "Musterwürfel",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SPatternCube.HighlightedSymbol] = new()
                {
                    // English: Which symbol was highlighted in {0}?
                    Question = "Welches Symbol war in {0} hervorgehoben?",
                },
            },
        },

        // Pattern Recognition
        [typeof(SPatternRecognition)] = new()
        {
            ModuleName = "Mustererkennung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SPatternRecognition.Pattern] = new()
                {
                    // English: What was the repeating pattern in {0}?
                    Question = "Was war bei {0} das wiederkehrende Muster?",
                },
            },
        },

        // The Pentabutton
        [typeof(SPentabutton)] = new()
        {
            ModuleName = "Pentaknopf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SPentabutton.BaseColor] = new()
                {
                    // English: What was the base colour in {0}?
                    Question = "Was war bei {0} die Basisfarbe?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                        ["White"] = "Weiß",
                    },
                },
            },
            Discriminators = new()
            {
                [SPentabutton.Label] = new()
                {
                    // English: the Pentabutton labelled “{0}”
                    // Example: the Pentabutton labelled “press”
                    Discriminator = "dem Pentaknopf mit der Aufschrift “{0}”",
                },
            },
        },

        // Periodic Words
        [typeof(SPeriodicWords)] = new()
        {
            ModuleName = "Periodische Wörter",
            ModuleNameDative = "Periodischen Wörtern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SPeriodicWords.DisplayedWords] = new()
                {
                    // English: What word was on the display in the {1} stage of {0}?
                    // Example: What word was on the display in the first stage of Periodic Words?
                    Question = "Welches Wort war in der {1}en Stufe von {0} auf dem Display?",
                },
            },
        },

        // Perspective Pegs
        [typeof(SPerspectivePegs)] = new()
        {
            ModuleName = "Perspektivstöpsel",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SPerspectivePegs.ColorSequence] = new()
                {
                    // English: What was the {1} color in the initial sequence in {0}?
                    // Example: What was the first color in the initial sequence in Perspective Pegs?
                    Question = "Was war bei {0} die {1}e Farbe in der Ausgangsfolge?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["yellow"] = "gelb",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["purple"] = "lila",
                    },
                },
            },
        },

        // Phosphorescence
        [typeof(SPhosphorescence)] = new()
        {
            ModuleName = "Phosphoreszenz",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SPhosphorescence.ButtonPresses] = new()
                {
                    // English: What was the {1} button press in {0}?
                    // Example: What was the first button press in Phosphorescence?
                    Question = "Was war bei {0} die {1}e Eingabe?",
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
                    Question = "Was war bei {0} das Offset?",
                },
            },
        },

        // Pickup Identification
        [typeof(SPickupIdentification)] = new()
        {
            Questions = new()
            {
                [SPickupIdentification.Item] = new()
                {
                    // English: What pickup was shown in the {1} stage of {0}?
                    // Example: What pickup was shown in the first stage of Pickup Identification?
                    Question = "Welches Sammlerstück war bei {0} in der {1}en Stufe zu sehen?",
                },
            },
        },

        // Pictionary
        [typeof(SPictionary)] = new()
        {
            Questions = new()
            {
                [SPictionary.Code] = new()
                {
                    // English: What was the code in {0}?
                    Question = "Was war bei {0} der Code?",
                },
            },
        },

        // Pie
        [typeof(SPie)] = new()
        {
            Questions = new()
            {
                [SPie.Digits] = new()
                {
                    // English: What was the {1} digit of the displayed number in {0}?
                    // Example: What was the first digit of the displayed number in Pie?
                    Question = "Was war die {1}e Ziffer der bei {0} angezeigten Zahl?",
                },
            },
        },

        // Pie Flash
        [typeof(SPieFlash)] = new()
        {
            Questions = new()
            {
                [SPieFlash.Digits] = new()
                {
                    // English: What number was not displayed in {0}?
                    Question = "Welche Zahl war bei {0} nicht zu sehen?",
                },
            },
        },

        // Pigpen Cycle
        [typeof(SPigpenCycle)] = new()
        {
            ModuleName = "Freimaurer-Schiffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SPigpenCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Pigpen Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}te Zeiger?",
                },
                [SPigpenCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Pigpen Cycle?
                    Question = "Welcher Buchstabe stand bei {0} auf dem {1}en Zeiger?",
                },
            },
            Discriminators = new()
            {
                [SPigpenCycle.LabelDiscriminator] = new()
                {
                    // English: the Pigpen Cycle that had the letter {0} on a dial
                    // Example: the Pigpen Cycle that had the letter A on a dial
                    Discriminator = "der Freimaurer-Schiffer, bei der der Buchstabe {0} vorkam,",
                },
            },
        },

        // The Pink Button
        [typeof(SPinkButton)] = new()
        {
            ModuleName = "Der Pinkfarbene Knopf",
            ModuleNameDative = "Pinkfarbenen Knopf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SPinkButton.Words] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the first word in The Pink Button?
                    Question = "Was war bei {0} das erste Wort?",
                },
                [SPinkButton.Colors] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in The Pink Button?
                    Question = "Was war bei {0} die {1}e Farbe?",
                    Answers = new()
                    {
                        ["black"] = "schwarz",
                        ["red"] = "rot",
                        ["green"] = "grün",
                        ["yellow"] = "gelb",
                        ["blue"] = "blau",
                        ["magenta"] = "magenta",
                        ["cyan"] = "türkis",
                        ["white"] = "weiß",
                    },
                },
            },
        },

        // Pinpoint
        [typeof(SPinpoint)] = new()
        {
            Questions = new()
            {
                [SPinpoint.Points] = new()
                {
                    // English: Which point occurred in {0}?
                    Question = "Welche Koordinate kam in {0} vor?",
                },
                [SPinpoint.Distances] = new()
                {
                    // English: Which distance occurred in {0}?
                    Question = "Welche Distanz kam in {0} vor?",
                },
            },
        },

        // Pixel Cipher
        [typeof(SPixelCipher)] = new()
        {
            ModuleName = "Pixelgeheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SPixelCipher.Keyword] = new()
                {
                    // English: What was the keyword in {0}?
                    Question = "Was war bei {0} das Schlüsselwort?",
                },
            },
        },

        // Placeholder Talk
        [typeof(SPlaceholderTalk)] = new()
        {
            Questions = new()
            {
                [SPlaceholderTalk.FirstPhrase] = new()
                {
                    // English: What was the first half of the first phrase in {0}?
                    Question = "Was war bei {0} die erste Hälfte der ersten Phrase?",
                },
                [SPlaceholderTalk.Ordinal] = new()
                {
                    // English: What was the last half of the first phrase in {0}?
                    Question = "Was war bei {0} die zweite Hälfte der ersten Phrase?",
                },
            },
        },

        // Placement Roulette
        [typeof(SPlacementRoulette)] = new()
        {
            ModuleName = "Platzierungsroulette",
            Questions = new()
            {
                [SPlacementRoulette.Char] = new()
                {
                    // English: What was the character listed on the information display in {0}?
                    Question = "Welche Figur wurde bei {0} auf dem Informationsdisplay angezeigt?",
                },
                [SPlacementRoulette.Track] = new()
                {
                    // English: What was the track listed on the information display in {0}?
                    Question = "Welche Rennstrecke wurde bei {0} auf dem Informationsdisplay angezeigt?",
                },
                [SPlacementRoulette.Vehicle] = new()
                {
                    // English: What was the vehicle listed on the information display in {0}?
                    Question = "Welches Fahrzeug wurde bei {0} auf dem Informationsdisplay angezeigt?",
                },
            },
        },

        // Planets
        [typeof(SPlanets)] = new()
        {
            ModuleName = "Planeten",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SPlanets.Strips] = new()
                {
                    // English: What was the color of the {1} strip (from the top) in {0}?
                    // Example: What was the color of the first strip (from the top) in Planets?
                    Question = "Welche Farbe hatte bei {0} der {1}e Streifen von oben?",
                    Answers = new()
                    {
                        ["Aqua"] = "Aqua",
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Lime"] = "Limette",
                        ["Orange"] = "Orange",
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["White"] = "Weiß",
                        ["Off"] = "Aus",
                    },
                },
                [SPlanets.Planet] = new()
                {
                    // English: What was the planet shown in {0}?
                    Question = "Welcher Planet kam bei {0} vor?",
                },
            },
        },

        // Playfair Cycle
        [typeof(SPlayfairCycle)] = new()
        {
            ModuleName = "Playfair-Schiffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SPlayfairCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Playfair Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}te Zeiger?",
                },
                [SPlayfairCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Playfair Cycle?
                    Question = "Welcher Buchstabe stand bei {0} auf dem {1}en Zeiger?",
                },
            },
            Discriminators = new()
            {
                [SPlayfairCycle.LabelDiscriminator] = new()
                {
                    // English: the Playfair Cycle that had the letter {0} on a dial
                    // Example: the Playfair Cycle that had the letter A on a dial
                    Discriminator = "der Playfair-Schiffer, bei der der Buchstabe {0} vorkam,",
                },
            },
        },

        // Poetry
        [typeof(SPoetry)] = new()
        {
            ModuleName = "Poesie",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SPoetry.Answers] = new()
                {
                    // English: What was the {1} correct answer you pressed in {0}?
                    // Example: What was the first correct answer you pressed in Poetry?
                    Question = "Was war bei {0} die als {1}e korrekt gedrückte Antwort?",
                },
            },
        },

        // Pointless Machines
        [typeof(SPointlessMachines)] = new()
        {
            ModuleName = "Sinnlose Maschinen",
            Questions = new()
            {
                [SPointlessMachines.Flashes] = new()
                {
                    // English: What color flashed {1} in {0}?
                    // Example: What color flashed first in Pointless Machines?
                    Question = "Welche Farbe hat bei {0} geblinkt?",
                    Answers = new()
                    {
                        ["White"] = "Weiß",
                        ["Purple"] = "Lila",
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Yellow"] = "Gelb",
                    },
                },
            },
        },

        // Polygons
        [typeof(SPolygons)] = new()
        {
            ModuleName = "Polygone",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SPolygons.Polygon] = new()
                {
                    // English: Which polygon was present on {0}?
                    Question = "Welches Polygon kam bei {0} vor?",
                },
            },
        },

        // Polyhedral Maze
        [typeof(SPolyhedralMaze)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Polyederlabyrinth",
            Questions = new()
            {
                [SPolyhedralMaze.StartPosition] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "Was war bei {0} die Startposition?",
                },
            },
            Discriminators = new()
            {
                [SPolyhedralMaze.MazeShape] = new()
                {
                    // English: {0}
                    // Example: the 4-truncated deltoidal icositetrahedral Polyhedral Maze
                    Discriminator = "dem Polyederlabyrinth mit einem {0}",
                    Arguments = new()
                    {
                        ["the 4-truncated deltoidal icositetrahedral Polyhedral Maze"] = "4-gestumpften Deltoidikositetraeder",
                        ["the chamfered dodecahedral Polyhedral Maze"] = "abgekanteten Dodekaeder",
                        ["the chamfered icosahedral Polyhedral Maze"] = "abgekanteten Ikosaeder",
                        ["the deltoidal hexecontahedral Polyhedral Maze"] = "Deltoidhexakontaeder",
                        ["the disdyakis dodecahedral Polyhedral Maze"] = "Disdyakisdodekaeder",
                        ["the joined snub cubic Polyhedral Maze"] = "verbundenen abgeschrägten Hexaeder",
                        ["the joined rhombicuboctahedral Polyhedral Maze"] = "verbundenen Rhombenkuboktaeder",
                        ["the pentagonal hexecontahedral Polyhedral Maze"] = "Pentagonhexakontaeder",
                        ["the orthokis propello cubic Polyhedral Maze"] = "Orthokis-Propello-Würfel",
                        ["the pentakis dodecahedral Polyhedral Maze"] = "Pentakisdodekaeder",
                        ["the rectified rhombicuboctahedral Polyhedral Maze"] = "Rhombenkuboktaederstumpf",
                        ["the triakis icosahedral Polyhedral Maze"] = "Triakisikosaeder",
                        ["the rhombicosidodecahedral Polyhedral Maze"] = "Rhombenikosidodekaeder",
                        ["the canonical rectified snub cubic Polyhedral Maze"] = "kanonischen abgeschrägten Hexaederstumpf",
                    },
                },
            },
        },

        // Prime Encryption
        [typeof(SPrimeEncryption)] = new()
        {
            ModuleName = "Primverschlüsselung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SPrimeEncryption.DisplayedValue] = new()
                {
                    // English: What was the number shown in {0}?
                    Question = "Welche Zahl wurde bei {0} angezeigt?",
                },
            },
        },

        // Prison Break
        [typeof(SPrisonBreak)] = new()
        {
            ModuleName = "Gefängnisausbruch",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SPrisonBreak.Prisoner] = new()
                {
                    // English: Which cell did the prisoner start in in {0}?
                    Question = "Welche Zelle hatte der Gefangene bei {0} am Anfang?",
                },
                [SPrisonBreak.Defuser] = new()
                {
                    // English: Where did you start in {0}?
                    Question = "Wo ging’s bei {0} los?",
                },
            },
        },

        // Probing
        [typeof(SProbing)] = new()
        {
            ModuleName = "Sondierung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SProbing.Frequencies] = new()
                {
                    // English: What was the missing frequency in the {1} wire in {0}?
                    // Example: What was the missing frequency in the red-white wire in Probing?
                    Question = "Was war bei {0} die fehlende Frequenz im {1}en Draht?",
                    Arguments = new()
                    {
                        ["red-white"] = "rot-weißen",
                        ["yellow-black"] = "gelb-schwarzen",
                        ["green"] = "grünen",
                        ["gray"] = "grauen",
                        ["yellow-red"] = "gelb-roten",
                        ["red-blue"] = "rot-blauen",
                    },
                },
            },
        },

        // Procedural Maze
        [typeof(SProceduralMaze)] = new()
        {
            ModuleName = "Prozedurales Labyrinth",
            ModuleNameDative = "Prozeduralen Labyrinth",
            Questions = new()
            {
                [SProceduralMaze.InitialSeed] = new()
                {
                    // English: What was the initial seed in {0}?
                    Question = "Was war bei {0} der Anfangswert?",
                },
            },
        },

        // ...?
        [typeof(SPunctuationMarks)] = new()
        {
            ModuleName = "Interpunktionszeichen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SPunctuationMarks.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Welche Zahl wurde bei {0} angezeigt?",
                },
            },
        },

        // Purple Arrows
        [typeof(SPurpleArrows)] = new()
        {
            ModuleName = "Lilane Pfeile",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SPurpleArrows.Finish] = new()
                {
                    // English: What was the target word on {0}?
                    Question = "Was war bei {0} das Zielwort?",
                },
            },
        },

        // The Purple Button
        [typeof(SPurpleButton)] = new()
        {
            ModuleName = "Der Lilane Knopf",
            ModuleNameDative = "Lilanen Knopf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SPurpleButton.Numbers] = new()
                {
                    // English: What was the {1} number in the cyclic sequence on {0}?
                    // Example: What was the first number in the cyclic sequence on The Purple Button?
                    Question = "Was war bei {0} die {1}e Zahl in der zyklischen Folge?",
                },
            },
        },

        // Puzzle Identification
        [typeof(SPuzzleIdentification)] = new()
        {
            Questions = new()
            {
                [SPuzzleIdentification.Num] = new()
                {
                    // English: What was the {1} puzzle number in {0}?
                    // Example: What was the first puzzle number in Puzzle Identification?
                    Question = "Was war bei {0} die {1}e Rätselnummer?",
                },
                [SPuzzleIdentification.Game] = new()
                {
                    // English: What game was the {1} puzzle in {0} from?
                    // Example: What game was the first puzzle in Puzzle Identification from?
                    Question = "Aus welchem Spiel war bei {0} das {1}e Rätsel?",
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
                    Question = "Was war bei {0} das {1}e Rätsel?",
                },
            },
        },

        // Puzzling Hexabuttons
        [typeof(SPuzzlingHexabuttons)] = new()
        {
            ModuleName = "Rätselhafte Hexaknöpfe",
            ModuleNameDative = "Rätselhaften Hexaknöpfen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SPuzzlingHexabuttons.Letter] = new()
                {
                    // English: What letter was displayed on the {1} hexabutton when submitting in {0}?
                    // Example: What letter was displayed on the top-left hexabutton when submitting in Puzzling Hexabuttons?
                    Question = "Welcher Buchstabe stand bei {0} im Eingabemodus auf dem {1} Hexaknopf?",
                    Arguments = new()
                    {
                        ["top-left"] = "oberen linken",
                        ["top-right"] = "oberen rechten",
                        ["middle-left"] = "mittleren linken",
                        ["center"] = "mittleren",
                        ["middle-right"] = "mittleren rechten",
                        ["bottom-left"] = "unteren linken",
                        ["bottom-right"] = "unteren rechten",
                    },
                },
            },
        },

        // Q & A
        [typeof(SQnA)] = new()
        {
            Questions = new()
            {
                [SQnA.Questions] = new()
                {
                    // English: What was the {1} question asked in {0}?
                    // Example: What was the first question asked in Q & A?
                    Question = "Was war bei {0} die {1}e gestellte Frage?",
                },
            },
        },

        // Quadrants
        [typeof(SQuadrants)] = new()
        {
            ModuleName = "Quadranten",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SQuadrants.Buttons] = new()
                {
                    // English: What was on the {1} button of the {2} stage in {0}?
                    // Example: What was on the first button of the first stage in Quadrants?
                    Question = "Was stand bei {0} in der {2}en Stufe auf der {1}en Taste?",
                },
            },
        },

        // Quantum Passwords
        [typeof(SQuantumPasswords)] = new()
        {
            ModuleName = "Quantenpasswörter",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SQuantumPasswords.Word] = new()
                {
                    // English: Which word was used in {0}?
                    Question = "Welches Wort kam bei {0} vor?",
                },
            },
        },

        // Quantum Ternary Converter
        [typeof(SQuantumTernaryConverter)] = new()
        {
            ModuleName = "Quanten-Ternär-Konverter",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SQuantumTernaryConverter.Number] = new()
                {
                    // English: Which number was shown in {0}?
                    Question = "Welche Zahl wurde bei {0} angezeigt?",
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
                    Question = "Was war bei {0} die Lösung zur {1}en Sequenz?",
                },
            },
        },

        // Question Mark
        [typeof(SQuestionMark)] = new()
        {
            ModuleName = "Fragezeichen",
            Questions = new()
            {
                [SQuestionMark.FlashedSymbols] = new()
                {
                    // English: Which of these symbols was part of the flashing sequence in {0}?
                    Question = "Welches Symbol kam bei {0} in der Blinksequenz vor?",
                },
            },
        },

        // Quick Arithmetic
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
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["yellow"] = "yellow",
                        ["white"] = "white",
                        ["black"] = "black",
                        ["orange"] = "orange",
                        ["pink"] = "pink",
                        ["purple"] = "purple",
                        ["cyan"] = "cyan",
                        ["brown"] = "brown",
                    },
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

        // Quintuples
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
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["blue"] = "blue",
                        ["orange"] = "orange",
                        ["green"] = "green",
                        ["pink"] = "pink",
                    },
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

        // Quiplash
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

        // Quiz Buzz
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

        // Qwirkle
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

        // Raiding Temples
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

        // Railway Cargo Loading
        [typeof(SRailwayCargoLoading)] = new()
        {
            ModuleName = "Bahnfrachtverladung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SRailwayCargoLoading.Cars] = new()
                {
                    // English: What was the {1} car in {0}?
                    // Example: What was the first car in Railway Cargo Loading?
                    Question = "Welcher Waggon wurde bei {0} als {1}er gekoppelt?",
                },
                [SRailwayCargoLoading.FreightTableRules] = new()
                {
                    // English: Which freight table rule {1} in {0}?
                    // Example: Which freight table rule was met in Railway Cargo Loading?
                    Question = "Welche Frachtregel in der Tabelle traf bei {0} {1}?",
                    Arguments = new()
                    {
                        ["was met"] = "zu",
                        ["wasn’t met"] = "nicht zu",
                    },
                },
            },
        },

        // Rainbow Arrows
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

        // Recolored Switches
        [typeof(SRecoloredSwitches)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Umgefärbte Schalter",
            ModuleNameDative = "Umgefärbten Schaltern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SRecoloredSwitches.LedColors] = new()
                {
                    // English: What was the color of the {1} LED in {0}?
                    // Example: What was the color of the first LED in Recolored Switches?
                    Question = "What was the color of the {1} LED in {0}?",
                    Answers = new()
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
            },
        },

        // Recursive Password
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

        // Red Arrows
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

        // Red Button’t
        [typeof(SRedButtont)] = new()
        {
            Questions = new()
            {
                [SRedButtont.Word] = new()
                {
                    // English: What was the word before “SUBMIT” in {0}?
                    Question = "What was the word before 'SUBMIT' in {0}?",
                },
            },
        },

        // Red Cipher
        [typeof(SRedCipher)] = new()
        {
            ModuleName = "Rote Geheimschrift",
            ModuleNameDative = "Roten Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SRedCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Red Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "ober",
                        ["middle"] = "mittler",
                        ["bottom"] = "unter",
                    },
                },
            },
        },

        // Red Herring
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

        // Reformed Role Reversal
        [typeof(SReformedRoleReversal)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SReformedRoleReversal.Condition] = new()
                {
                    // English: Which condition was the solving condition in {0}?
                    Question = "Which condition was the solving condition in {0}?",
                    Answers = new()
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
                [SReformedRoleReversal.Wire] = new()
                {
                    // English: What color was the {1} wire in {0}?
                    // Example: What color was the first wire in Reformed Role Reversal?
                    Question = "What color was the {1} wire in {0}?",
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
                    Question = "Which calculation was used for the {1} stage of {0}?",
                },
            },
        },

        // Regular Crazy Talk
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

        // Retirement
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

        // Reverse Morse
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

        // Reverse Polish Notation
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

        // RGB Encryption
        [typeof(SRGBEncryption)] = new()
        {
            ModuleName = "RGB-Verschlüsselung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SRGBEncryption.MorseSequence] = new()
                {
                    // English: What was the {1} Morse code sequence in {0}?
                    // Example: What was the first Morse code sequence in RGB Encryption?
                    Question = "Was waren die Morsezeichen in der {1}en Sequenz bei {0}?",
                },
                [SRGBEncryption.ColorSequence] = new()
                {
                    // English: What was the {1} color sequence in {0}?
                    // Example: What was the first color sequence in RGB Encryption?
                    Question = "Was waren die Farben in der {1}en Sequenz bei {0}?",
                },
            },
        },

        // RGB Maze
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

        // RGB Sequences
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
            NeedsTranslation = true,
            Questions = new()
            {
                [SRhythms.Color] = new()
                {
                    // English: What was the color in {0}?
                    Question = "What was the color in {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Blue",
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                    },
                },
            },
        },

        // RNG Crystal
        [typeof(SRNGCrystal)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "RNG-Kristall",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SRNGCrystal.Taps] = new()
                {
                    // English: Which bit had a tap in {0} (the output after shifting is at bit 0)?
                    Question = "Which bit had a tap in {0} (the output after shifting is at bit 0)?",
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
                    Question = "Where was the empty cell in {0}?",
                },
            },
        },

        // Robot Programming
        [typeof(SRobotProgramming)] = new()
        {
            Questions = new()
            {
                [SRobotProgramming.Color] = new()
                {
                    // English: What was the color of the {1} robot in {0}?
                    // Example: What was the color of the first robot in Robot Programming?
                    Question = "Welche Farbe hatte der {1}e Roboter bei {0}?",
                },
                [SRobotProgramming.Shape] = new()
                {
                    // English: What was the shape of the {1} robot in {0}?
                    // Example: What was the shape of the first robot in Robot Programming?
                    Question = "Welche Form hatte der {1}e Roboter bei {0}?",
                },
            },
        },

        // Roger
        [typeof(SRoger)] = new()
        {
            Questions = new()
            {
                [SRoger.Seed] = new()
                {
                    // English: What four-digit number was given in {0}?
                    Question = "Welche vierstellige Zahl war bei {0} vorgegeben?",
                },
            },
        },

        // Role Reversal
        [typeof(SRoleReversal)] = new()
        {
            ModuleName = "Rollenumkehr",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SRoleReversal.Wires] = new()
                {
                    // English: How many {1} wires were there in {0}?
                    // Example: How many warm-colored wires were there in Role Reversal?
                    Question = "Wie viele der Drähte bei {0} hatten eine {1} Farbe?",
                    Arguments = new()
                    {
                        ["warm-colored"] = "warme",
                        ["cold-colored"] = "kalte",
                        ["primary-colored"] = "primäre",
                        ["secondary-colored"] = "sekundäre",
                    },
                },
                [SRoleReversal.Number] = new()
                {
                    // English: What was the number corresponding to the correct condition in {0}?
                    Question = "Welche Zahl entspricht der Bedingung, die bei {0} zutraf?",
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
                    Question = "In welcher Runde hat bei {0} das {1} Team {2}?",
                    Arguments = new()
                    {
                        ["red"] = "rote",
                        ["blue"] = "blaue",
                        ["win"] = "gewonnen",
                        ["lose"] = "verloren",
                    },
                },
                [SRPSJudging.QDraw] = new()
                {
                    // English: Which round was a draw in {0}?
                    Question = "In welcher Runde war bei {0} unentschieden?",
                },
            },
            Discriminators = new()
            {
                [SRPSJudging.DWinner] = new()
                {
                    // English: the RPS Judging where the {0} team {1} the {2} round
                    // Example: the RPS Judging where the red team won the first round
                    Discriminator = "dem RPS Judging, bei dem das {0} Team die {2}e Runde {1},",
                    Arguments = new()
                    {
                        ["red"] = "red",
                        ["blue"] = "blue",
                        ["won"] = "gewann",
                        ["lost"] = "verlor",
                    },
                },
                [SRPSJudging.DDraw] = new()
                {
                    // English: the RPS Judging with a draw in the {0} round
                    // Example: the RPS Judging with a draw in the first round
                    Discriminator = "dem RPS Judging, bei dem in der {0}en Runde unentschieden war,",
                },
            },
        },

        // The Rule
        [typeof(SRule)] = new()
        {
            ModuleName = "Die Regel",
            ModuleNameDative = "Regel",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SRule.Number] = new()
                {
                    // English: What was the rule number in {0}?
                    Question = "Was war bei {0} die Regelnummer?",
                },
            },
        },

        // Rule of Three
        [typeof(SRuleOfThree)] = new()
        {
            ModuleName = "Dreierregel",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SRuleOfThree.QCoordinates] = new()
                {
                    // English: What was the {1} coordinate of the {2} vertex in {0}?
                    // Example: What was the X coordinate of the red vertex in Rule of Three?
                    Question = "Was war bei {0} die {1}-Koordinate der {2}en Ecke?",
                    Arguments = new()
                    {
                        ["red"] = "rot",
                        ["yellow"] = "gelb",
                        ["blue"] = "blau",
                    },
                },
                [SRuleOfThree.QCycles] = new()
                {
                    // English: What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?
                    // Example: What was the position of the red sphere on the X axis in the first cycle in Rule of Three?
                    Question = "Was war bei {0} die Position der {1} Kugel auf der {2}-Achse im {3}en Zyklus?",
                    Arguments = new()
                    {
                        ["red"] = "rot",
                        ["yellow"] = "gelb",
                        ["blue"] = "blau",
                    },
                },
            },
            Discriminators = new()
            {
                [SRuleOfThree.DCoordinates] = new()
                {
                    // English: the Rule of Three where the {1} coordinate of the {2} vertex was {0}
                    // Example: the Rule of Three where the X coordinate of the red vertex was 0
                    Discriminator = "die Dreierregel, bei der die {1}-Koordinate des {2}en Eckpunkts {0} war,",
                    Arguments = new()
                    {
                        ["red"] = "rot",
                        ["yellow"] = "gelb",
                        ["blue"] = "blau",
                    },
                },
                [SRuleOfThree.DCycles] = new()
                {
                    // English: the Rule of Three where the {1} sphere was {0} on the {2} axis in the {3} cycle
                    // Example: the Rule of Three where the red sphere was positive on the X axis in the first cycle
                    Discriminator = "die Dreierregel, bei der die {1}e Kugel auf der {2}-Achse {3} war,",
                    Arguments = new()
                    {
                        ["positive"] = "positiv",
                        ["negative"] = "negativ",
                        ["zero"] = "null",
                        ["red"] = "rot",
                        ["yellow"] = "gelb",
                        ["blue"] = "blau",
                    },
                },
            },
        },

        // Safety Square
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
            ModuleName = "Das Samsung",
            ModuleNameDative = "Samsung",
            Questions = new()
            {
                [SSamsung.AppPositions] = new()
                {
                    // English: Where was {1} in {0}?
                    // Example: Where was Duolingo in The Samsung?
                    Question = "Wo war bei {0} {1}?",
                },
            },
        },

        // Saturn
        [typeof(SSaturn)] = new()
        {
            Questions = new()
            {
                [SSaturn.Goal] = new()
                {
                    // English: Where was the goal in {0}?
                    Question = "Wo war bei {0} das Ziel?",
                },
            },
        },

        // Sbemail Songs
        [typeof(SSbemailSongs)] = new()
        {
            ModuleName = "Sbemail-Lieder",
            Questions = new()
            {
                [SSbemailSongs.Songs] = new()
                {
                    // English: What was the displayed song for stage {1} (hexadecimal) of {0}?
                    // Example: What was the displayed song for stage 01 (hexadecimal) of Sbemail Songs?
                    Question = "Was war bei {0} das in Stufe {1} (hexadezimal) angezeigte Lied?",
                },
            },
            Discriminators = new()
            {
                [SSbemailSongs.Digits] = new()
                {
                    // English: the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)
                    // Example: the Sbemail Songs which displayed ‘Oh, who is the guy that…’ in stage 01 (hexadecimal)
                    Discriminator = "den Sbemail-Liedern, deren Stufe {1} (hexadezimal) ‘{0}’ anzeigten,",
                },
            },
        },

        // Scavenger Hunt
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

        // Schlag den Bomb
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

        // Scramboozled Eggain
        [typeof(SScramboozledEggain)] = new()
        {
            ModuleName = "Wieder Mal Das Ei Gerührt",
            Questions = new()
            {
                [SScramboozledEggain.Word] = new()
                {
                    // English: What was the {1} encrypted word in {0}?
                    // Example: What was the first encrypted word in Scramboozled Eggain?
                    Question = "Was war bei {0} das {1}e verschlüsselte Wort?",
                },
            },
        },

        // Scripting
        [typeof(SScripting)] = new()
        {
            Questions = new()
            {
                [SScripting.VariableDataType] = new()
                {
                    // English: What was the submitted data type of the variable in {0}?
                    Question = "Was war bei {0} der korrekte Datentyp der Variable?",
                },
            },
        },

        // Scrutiny Squares
        [typeof(SScrutinySquares)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SScrutinySquares.FirstDifference] = new()
                {
                    // English: What was the modified property of the first display in {0}?
                    Question = "What was the modified property of the first display in {0}?",
                    Answers = new()
                    {
                        ["Word"] = "Word",
                        ["Color around word"] = "Color around word",
                        ["Color of background"] = "Color of background",
                        ["Color of word"] = "Color of word",
                    },
                },
            },
        },

        // Sea Shells
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

        // Semamorse
        [typeof(SSemamorse)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSemamorse.Color] = new()
                {
                    // English: What was the color of the display involved in the starting value in {0}?
                    Question = "What was the color of the display involved in the starting value in {0}?",
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["cyan"] = "cyan",
                        ["indigo"] = "indigo",
                        ["pink"] = "pink",
                    },
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

        // The Sequencyclopedia
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

        // S.E.T. Theory
        [typeof(SSetTheory)] = new()
        {
            Questions = new()
            {
                [SSetTheory.Equations] = new()
                {
                    // English: What equation was shown in the {1} stage of {0}?
                    // Example: What equation was shown in the first stage of S.E.T. Theory?
                    Question = "Welche Gleichung war bei {0} in der {1}en Stufe zu sehen?",
                },
            },
        },

        // Shapes And Bombs
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

        // Shape Shift
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

        // Shifted Maze
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

        // Shogi Identification
        [typeof(SShogiIdentification)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SShogiIdentification.Piece] = new()
                {
                    // English: What was the displayed piece in {0}?
                    Question = "What was the displayed piece in {0}?",
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

        // Silly Slots
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
                    Answers = new()
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

        // Simon Said
        [typeof(SSimonSaid)] = new()
        {
            ModuleName = "Simon Sagte",
            Questions = new()
            {
                [SSimonSaid.Flashes] = new()
                {
                    // English: What color flashed {1} in the final sequence of {0}?
                    // Example: What color flashed first in the final sequence of Simon Said?
                    Question = "Welche Farbe ist bei {0} in der vollen Sequenz als {1}es aufgeleuchtet?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Yellow"] = "Gelb",
                    },
                },
            },
        },

        // Simon Samples
        [typeof(SSimonSamples)] = new()
        {
            ModuleName = "Simons Samples",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SSimonSamples.Samples] = new()
                {
                    // English: What were the call samples {1} of {0}?
                    // Example: What were the call samples played in the first stage of Simon Samples?
                    Question = "Welche Rufsamples wurden bei {0} {1}?",
                    Arguments = new()
                    {
                        ["played in the first stage"] = "in der ersten Stufe gespielt",
                        ["added in the second stage"] = "in der zweiten Stufe hinzugefügt",
                        ["added in the third stage"] = "in der dritten Stufe hinzugefügt",
                    },
                },
            },
        },

        // Simon Says
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
                    Answers = new()
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
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonScrambles.Colors] = new()
                {
                    // English: What color flashed {1} in {0}?
                    // Example: What color flashed first in Simon Scrambles?
                    Question = "What color flashed {1} in {0}?",
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

        // Simon Screams
        [typeof(SSimonScreams)] = new()
        {
            ModuleName = "Simon Schreit",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SSimonScreams.Flashing] = new()
                {
                    // English: Which color flashed {1} in the final sequence in {0}?
                    // Example: Which color flashed first in the final sequence in Simon Screams?
                    Question = "Welche Farbe hat bei {0} als {1}e geblinkt?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                    },
                },
                [SSimonScreams.RuleSimple] = new()
                {
                    // English: In which stage(s) of {0} was “{1}” the applicable rule?
                    // Example: In which stage(s) of Simon Screams was “a color flashed, then a color two away, then the first again” the applicable rule?
                    Question = "In welcher/-n Stufe(n) bei {0} war “{1}” die zutreffende Regel?",
                    Arguments = new()
                    {
                        ["a color flashed, then a color two away, then the first again"] = "eine Farbe blinkt, dann eine mit einer Abstand, und dann die erste nochmal",
                        ["a color flashed, then a color two away, then the one opposite that"] = "eine Farbe blinkt, dann eine mit einer Abstand, und dann die gegenüberliegende davon",
                        ["a color flashed, then a color two away, then the one opposite the first"] = "eine Farbe blinkt, dann eine mit einer Abstand, und dann die gegenüberliegende der ersten",
                        ["a color flashed, then an adjacent color, then the first again"] = "eine Farbe blinkt, dann eine daneben, und dann die erste nochmal",
                        ["a color flashed, then another color, then the first"] = "eine Farbe blinkt, dann eine andere, dann die erste nochmal",
                        ["a color flashed, then one adjacent, then the one opposite that"] = "eine Farbe blinkt, dann eine daneben, und dann die gegenüberliegende",
                        ["a color flashed, then one adjacent, then the one opposite the first"] = "eine Farbe blinkt, dann eine daneben, und dann die gegenüberliegende der ersten",
                        ["a color flashed, then the one opposite, then one adjacent to that"] = "eine Farbe blinkt, dann die gegenüberliegende, und dann eine daneben",
                        ["a color flashed, then the one opposite, then one adjacent to the first"] = "eine Farbe blinkt, dann die gegenüberliegende, und dann eine neben der ersten",
                        ["a color flashed, then the one opposite, then the first again"] = "eine Farbe blinkt, dann die gegenüberliegende, dann die erste nochmal",
                        ["every color flashed at least once"] = "jede Farbe blinkt mindestens einmal",
                        ["exactly one color flashed exactly twice"] = "genau eine Farbe blinkt genau zweimal",
                        ["exactly one color flashed more than once"] = "genau eine Farbe blinkt mehr als einmal",
                        ["exactly two colors flashed exactly twice"] = "genau zwei Farben blinken genau zweimal",
                        ["exactly two colors flashed more than once"] = "genau zwei Farben blinken mehr als einmal",
                        ["no color flashed exactly twice"] = "keine Farbe blinkt genau zweimal",
                        ["no color flashed more than once"] = "keine Farbe blinkt mehr als einmal",
                        ["no two adjacent colors flashed in clockwise order"] = "keine zwei nebeneinanderliegenden Farben blinken im Uhrzeigersinn",
                        ["no two adjacent colors flashed in counter-clockwise order"] = "keine zwei nebeneinanderliegenden Farben blinken gegen den Uhrzeigersinn",
                        ["no two colors two apart flashed in clockwise order"] = "keine zwei Farben mit einer Farbe dazwischen blinken im Uhrzeigersinn",
                        ["no two colors two apart flashed in counter-clockwise order"] = "keine zwei Farben, mit einer Farbe dazwischen, blinken gegen den Uhrzeigersinn",
                        ["the colors flashing first and last are adjacent"] = "die erste und letzte blinkende Farbe liegen nebeneinander",
                        ["the colors flashing first and last are different and not adjacent"] = "die erste und letzte blinkende Farbe sind verschieden und liegen nicht nebeneinander",
                        ["the colors flashing first and last are the same"] = "die erste und letzte blinkende Farbe ist die selbe",
                        ["the number of distinct colors that flashed is even"] = "die Anzahl der unterschiedlichen blinkenden Farben ist gerade",
                        ["the number of distinct colors that flashed is odd"] = "die Anzahl der unterschiedlichen blinkenden Farben ist ungerade",
                        ["there are at least three colors that didn’t flash"] = "mindestens drei Farben blinken nicht",
                        ["there are exactly two colors that didn’t flash"] = "genau zwei Farben blinken nicht",
                        ["there are two colors adjacent to each other that didn’t flash"] = "zwei nebeneinander liegende Farben blinken nicht",
                        ["there are two colors opposite each other that didn’t flash"] = "zwei gegenüberliegende Farben blinken nicht",
                        ["there are two colors two away from each other that didn’t flash"] = "zwei Farben, mit einer Farbe dazwischen, blinken nicht",
                        ["there is exactly one color that didn’t flash"] = "genau eine Farbe blinkt nicht",
                        ["three adjacent colors did not flash"] = "drei nebeneinanderliegende Farben blinken nicht",
                        ["three adjacent colors flashed in clockwise order"] = "drei nebeneinanderliegende Farben blinken im Uhrzeigersinn",
                        ["three adjacent colors flashed in counter-clockwise order"] = "drei nebeneinanderliegende Farben blinken gegen den Uhrzeigersinn",
                        ["three colors, each two apart, flashed in clockwise order"] = "drei Farben mit jeweils einer dazwischen blinken im Uhrzeigersinn",
                        ["three colors, each two apart, flashed in counter-clockwise order"] = "drei Farben mit jeweils einer dazwischen blinken gegen den Uhrzeigersinn",
                        ["two adjacent colors flashed in clockwise order"] = "zwei nebeneinanderliegende Farben blinken im Uhrzeigersinn",
                        ["two adjacent colors flashed in counter-clockwise order"] = "zwei nebeneinanderliegende Farben blinken gegen den Uhrzeigersinn",
                        ["two colors two apart flashed in clockwise order"] = "zwei Farben mit einer Farbe dazwischen blinken im Uhrzeigersinn",
                        ["two colors two apart flashed in counter-clockwise order"] = "zwei Farben mit einer Farbe dazwischen blinken gegen den Uhrzeigersinn",
                    },
                    Answers = new()
                    {
                        ["first"] = "erste",
                        ["second"] = "zweite",
                        ["third"] = "dritte",
                        ["first and second"] = "erste und zweite",
                        ["first and third"] = "erste und dritte",
                        ["second and third"] = "zweite und dritte",
                        ["all of them"] = "alle",
                    },
                },
                [SSimonScreams.RuleComplex] = new()
                {
                    // English: In which stage(s) of {0} was “{1} flashed out of {2}, {3}, and {4}” the applicable rule?
                    // Example: In which stage(s) of Simon Screams was “at most one color flashed out of Red, Orange, and Yellow” the applicable rule?
                    Question = "In welcher/-n Stufe(n) bei {0} war “{1} der Farben {2}, {3} und {4} blinkt” die zutreffende Regel?",
                    Arguments = new()
                    {
                        ["at most one color"] = "maximal eine",
                        ["at least two colors"] = "mindestens zwei",
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Orange"] = "Orange",
                        ["Blue"] = "Blau",
                        ["Yellow"] = "Gelb",
                        ["Purple"] = "Lila",
                    },
                    Answers = new()
                    {
                        ["first"] = "erste",
                        ["second"] = "zweite",
                        ["third"] = "dritte",
                        ["first and second"] = "erste und zweite",
                        ["first and third"] = "erste und dritte",
                        ["second and third"] = "zweite und dritte",
                        ["all of them"] = "alle",
                    },
                },
            },
        },

        // Simon Selects
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

        // Simon Serves
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

        // Simon Shouts
        [typeof(SSimonShouts)] = new()
        {
            ModuleName = "Simon Ruft",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SSimonShouts.FlashingLetter] = new()
                {
                    // English: Which letter flashed on the {1} button in {0}?
                    // Example: Which letter flashed on the top button in Simon Shouts?
                    Question = "Welcher Buchstabe wurde bei {0} durch die {1} signalisiert?",
                    Arguments = new()
                    {
                        ["top"] = "Hoch-Taste",
                        ["left"] = "Links-Taste",
                        ["right"] = "Rechts-Taste",
                        ["bottom"] = "Runter-Taste",
                    },
                },
            },
        },

        // Simon Shrieks
        [typeof(SSimonShrieks)] = new()
        {
            ModuleName = "Simon Kreischt",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SSimonShrieks.FlashingButton] = new()
                {
                    // English: How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?
                    // Example: How many spaces clockwise from the arrow was the first flash in the final sequence in Simon Shrieks?
                    Question = "Wie weit vom Pfeil im Uhrzeigersinn war bei {0} die als {1}e aufgeleuchtete Taste?",
                },
            },
        },

        // Simon Shuffles
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

        // Simon Signals
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
                    // English: What color was the arrow with this shape in {0}? (+ sprite)
                    Question = "What color was the arrow with this shape in {0}?",
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["gray"] = "gray",
                    },
                },
                [SSimonSignals.ShapeToRotations] = new()
                {
                    // English: How many directions did the arrow with this shape have in {0}? (+ sprite)
                    Question = "How many directions did the arrow with this shape have in {0}?",
                },
                [SSimonSignals.RotationsToColor] = new()
                {
                    // English: What color was the arrow with {1} possible directions in {0}?
                    // Example: What color was the arrow with 3 possible directions in Simon Signals?
                    Question = "What color was the arrow with {1} possible directions in {0}?",
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["gray"] = "gray",
                    },
                },
                [SSimonSignals.RotationsToShape] = new()
                {
                    // English: What shape was the arrow with {1} possible directions in {0}?
                    // Example: What shape was the arrow with 3 possible directions in Simon Signals?
                    Question = "What shape was the arrow with {1} possible directions in {0}?",
                },
            },
        },

        // Simon Simons
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

        // Simon Sings
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

        // Simon Smiles
        [typeof(SSimonSmiles)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSmiles.Sounds] = new()
                {
                    // English: What sound did the {1} button press make in {0}?
                    // Example: What sound did the first button press make in Simon Smiles?
                    Question = "What sound did the {1} button press make in {0}?",
                },
            },
        },

        // Simon Smothers
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
                    Question = "What was the direction of the {1} flash in {0}?",
                    Answers = new()
                    {
                        ["Up"] = "Up",
                        ["Down"] = "Down",
                        ["Left"] = "Left",
                        ["Right"] = "Right",
                    },
                },
            },
        },

        // Simon Sounds
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
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["blue"] = "blue",
                        ["yellow"] = "yellow",
                        ["green"] = "green",
                    },
                },
            },
        },

        // Simon Speaks
        [typeof(SSimonSpeaks)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSpeaks.Positions] = new()
                {
                    // English: Which bubble flashed first in {0}?
                    Question = "Which bubble flashed first in {0}?",
                    Answers = new()
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
                    Answers = new()
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
            },
        },

        // Simon’s Star
        [typeof(SSimonsStar)] = new()
        {
            ModuleName = "Simons Stern",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SSimonsStar.Colors] = new()
                {
                    // English: Which color flashed {1} in {0}?
                    // Example: Which color flashed first in Simon’s Star?
                    Question = "Welche Farbe ist bei {0} als {1}es aufgeleuchtet?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["yellow"] = "gelb",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["purple"] = "lila",
                    },
                },
            },
        },

        // Simon Stacks
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

        // Simon Stages
        [typeof(SSimonStages)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Simon-Stufen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SSimonStages.Indicator] = new()
                {
                    // English: What color was the indicator in the {1} stage in {0}?
                    // Example: What color was the indicator in the first stage in Simon Stages?
                    Question = "Welche Farbe hatte bei {0} der Indikator in der {1}en Stufe?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["blue"] = "blau",
                        ["yellow"] = "gelb",
                        ["orange"] = "orange",
                        ["magenta"] = "magenta",
                        ["green"] = "grün",
                        ["pink"] = "pink",
                        ["lime"] = "limette",
                        ["cyan"] = "türkis",
                        ["white"] = "weiß",
                    },
                },
                [SSimonStages.Flashes] = new()
                {
                    // English: Which color flashed {1} in the {2} stage in {0}?
                    // Example: Which color flashed first in the first stage in Simon Stages?
                    Question = "Welche Farbe ist bei {0} als {1}e in der {2}en Stufe aufgeleuchtet?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["blue"] = "blau",
                        ["yellow"] = "gelb",
                        ["orange"] = "orange",
                        ["magenta"] = "magenta",
                        ["green"] = "grün",
                        ["pink"] = "pink",
                        ["lime"] = "limette",
                        ["cyan"] = "türkis",
                        ["white"] = "weiß",
                    },
                },
            },
        },

        // Simon States
        [typeof(SSimonStates)] = new()
        {
            ModuleName = "Simon Statuiert",
            Questions = new()
            {
                [SSimonStates.Display] = new()
                {
                    // English: Which {1} in the {2} stage in {0}?
                    // Example: Which color(s) flashed in the first stage in Simon States?
                    Question = "Welche Farbe(n) sind bei {0} in der {2}en Stufe {1}?",
                    Arguments = new()
                    {
                        ["color(s) flashed"] = "aufgeleuchtet",
                        ["color(s) didn’t flash"] = "nicht aufgeleuchtet",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Red, Yellow"] = "Rot, Gelb",
                        ["Red, Green"] = "Rot, Grün",
                        ["Red, Blue"] = "Rot, Blau",
                        ["Yellow, Green"] = "Gelb, Grün",
                        ["Yellow, Blue"] = "Gelb, Blau",
                        ["Green, Blue"] = "Grün, Blau",
                        ["all 4"] = "alle 4",
                        ["none"] = "keine",
                    },
                },
            },
        },

        // Simon Stops
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Violet"] = "Violet",
                    },
                },
            },
        },

        // Simon Stores
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

        // Simon Subdivides
        [typeof(SSimonSubdivides)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSimonSubdivides.Button] = new()
                {
                    // English: What color was the button at this position in {0}? (+ sprite)
                    Question = "What color was the button at this position in {0}?",
                    Answers = new()
                    {
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Red"] = "Red",
                        ["Violet"] = "Violet",
                    },
                },
            },
        },

        // Simon Supports
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

        // Simply Simon
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

        // Simultaneous Simons
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
                    Answers = new()
                    {
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                    },
                },
            },
        },

        // Skewed Slots
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

        // Skewers
        [typeof(SSkewers)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SSkewers.Color] = new()
                {
                    // English: What color was this gem in {0}? (+ sprite)
                    Question = "What color was this gem in {0}?",
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
            Questions = new()
            {
                [SSkyrim.Race] = new()
                {
                    // English: Which race was selectable, but not the solution, in {0}?
                    Question = "Which race was selectable, but not the solution, in {0}?",
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
                    Question = "Which weapon was selectable, but not the solution, in {0}?",
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
                    Question = "Which enemy was selectable, but not the solution, in {0}?",
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
                    Question = "Which city was selectable, but not the solution, in {0}?",
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
                    Question = "Which dragon shout was selectable, but not the solution, in {0}?",
                },
            },
        },

        // Slow Math
        [typeof(SSlowMath)] = new()
        {
            ModuleName = "Mathe in Zeitlupe",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SSlowMath.LastLetters] = new()
                {
                    // English: What was the last triplet of letters in {0}?
                    Question = "Was waren bei {0} die letzten drei Buchstaben?",
                },
            },
        },

        // Small Circle
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
                    Question = "Which color was {1} in the solution to {0}?",
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

        // Snooker
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

        // Snowflakes
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

        // Sonic & Knuckles
        [typeof(SSonicKnuckles)] = new()
        {
            Questions = new()
            {
                [SSonicKnuckles.Sounds] = new()
                {
                    // English: Which sound was played but not featured in the chosen zone in {0}?
                    Question = "Welcher Ton wurde bei {0} abgespielt, war aber nicht der angezeigten Zone zugehörig?",
                },
                [SSonicKnuckles.Badnik] = new()
                {
                    // English: Which badnik was shown in {0}?
                    Question = "Welcher Gegner kam bei {0} vor?",
                },
                [SSonicKnuckles.Monitor] = new()
                {
                    // English: Which monitor was shown in {0}?
                    Question = "Welcher Monitor kam bei {0} vor?",
                },
            },
        },

        // Sonic the Hedgehog
        [typeof(SSonicTheHedgehog)] = new()
        {
            Questions = new()
            {
                [SSonicTheHedgehog.Pictures] = new()
                {
                    // English: What was the {1} picture on {0}?
                    // Example: What was the first picture on Sonic the Hedgehog?
                    Question = "Was war bei {0} das {1}e Bild?",
                },
                [SSonicTheHedgehog.Sounds] = new()
                {
                    // English: Which sound was played by the {1} screen on {0}?
                    // Example: Which sound was played by the Running Boots screen on Sonic the Hedgehog?
                    Question = "Welcher Ton wurde bei {0} über den {1}-Bildschirm abgespielt?",
                    Arguments = new()
                    {
                        ["Running Boots"] = "Rennstiefel",
                        ["Invincibility"] = "Unbesiegbarkeit",
                        ["Extra Life"] = "Extraleben",
                        ["Rings"] = "Ringe",
                    },
                },
            },
        },

        // Sorting
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

        // Souvenir
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

        // Space Traders
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

        // Spelling Bee
        [typeof(SSpellingBee)] = new()
        {
            Questions = new()
            {
                [SSpellingBee.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "Welches Wort sollte bei {0} buchstabiert werden?",
                },
            },
        },

        // The Sphere
        [typeof(SSphere)] = new()
        {
            ModuleName = "Die Kugel",
            ModuleNameDative = "Kugel",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SSphere.Colors] = new()
                {
                    // English: What was the {1} flashed color in {0}?
                    // Example: What was the first flashed color in The Sphere?
                    Question = "Welche Farbe ist bei {0} als {1}e aufgeleuchtet?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["blue"] = "blau",
                        ["green"] = "grün",
                        ["orange"] = "orange",
                        ["pink"] = "pink",
                        ["purple"] = "lila",
                        ["grey"] = "grau",
                        ["white"] = "weiß",
                    },
                },
            },
        },

        // Splitting The Loot
        [typeof(SSplittingTheLoot)] = new()
        {
            ModuleName = "Beute Aufteilen",
            Questions = new()
            {
                [SSplittingTheLoot.ColoredBag] = new()
                {
                    // English: What bag was initially colored in {0}?
                    Question = "Welche Tasche hatte bei {0} schon am Anfang eine Farbe?",
                },
            },
        },

        // Spongebob Birthday Identification
        [typeof(SSpongebobBirthdayIdentification)] = new()
        {
            Questions = new()
            {
                [SSpongebobBirthdayIdentification.Children] = new()
                {
                    // English: Who was the {1} child displayed in {0}?
                    // Example: Who was the first child displayed in Spongebob Birthday Identification?
                    Question = "Welches Kind wurde bei {0} als {1}es angezeigt?",
                },
            },
        },

        // Stability
        [typeof(SStability)] = new()
        {
            ModuleName = "Stabilität",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SStability.LedColors] = new()
                {
                    // English: What was the color of the {1} lit LED in {0}?
                    // Example: What was the color of the first lit LED in Stability?
                    Question = "Welche Farbe hatte bei {0} die {1}e leuchtende LED?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Blue"] = "Blau",
                    },
                },
                [SStability.IdNumber] = new()
                {
                    // English: What was the identification number in {0}?
                    Question = "Was war bei {0} die Identifikationsnummer?",
                },
            },
        },

        // Stable Time Signatures
        [typeof(SStableTimeSignatures)] = new()
        {
            ModuleName = "Stabile Taktarten",
            ModuleNameDative = "Stabilen Taktarten",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SStableTimeSignatures.Signatures] = new()
                {
                    // English: What was the {1} time signature in {0}?
                    // Example: What was the first time signature in Stable Time Signatures?
                    Question = "Was war bei {0} die {1}e Taktart?",
                },
            },
        },

        // Stacked Sequences
        [typeof(SStackedSequences)] = new()
        {
            ModuleName = "Gestapelte Folgen",
            ModuleNameDative = "Gestapelten Folgen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SStackedSequences.Question] = new()
                {
                    // English: Which of these is the length of a sequence in {0}?
                    Question = "Welche Folgenlänge kam bei {0} vor?",
                },
            },
        },

        // Stars
        [typeof(SStars)] = new()
        {
            ModuleName = "Sterne",
            ModuleNameDative = "Sternen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SStars.Center] = new()
                {
                    // English: What was the digit in the center of {0}?
                    Question = "Welche Ziffer war bei {0} in der Mitte?",
                },
            },
        },

        // Starstruck
        [typeof(SStarstruck)] = new()
        {
            ModuleName = "Sternenbegeisterung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SStarstruck.Star] = new()
                {
                    // English: Which star was present on {0}?
                    Question = "Welcher Stern kam bei {0} vor?",
                },
            },
        },

        // State of Aggregation
        [typeof(SStateOfAggregation)] = new()
        {
            ModuleName = "Aggregatzustand",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SStateOfAggregation.Element] = new()
                {
                    // English: What was the element shown in {0}?
                    Question = "Welches Element kam bei {0} vor?",
                },
            },
        },

        // Stellar
        [typeof(SStellar)] = new()
        {
            ModuleName = "Sternchen",
            Questions = new()
            {
                [SStellar.Letters] = new()
                {
                    // English: What was the {1} letter in {0}?
                    // Example: What was the Morse code letter in Stellar?
                    Question = "Was war bei {0} der Buchstabe in {1}?",
                    Arguments = new()
                    {
                        ["Morse code"] = "Morsezeichen",
                        ["tap code"] = "Klopfzeichen",
                        ["Braille"] = "Blindenschrift",
                    },
                },
            },
        },

        // Stroop’s Test
        [typeof(SStroopsTest)] = new()
        {
            ModuleName = "Stroop-Test",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SStroopsTest.QWord] = new()
                {
                    // English: What was the {1} submitted word in {0}?
                    // Example: What was the first submitted word in Stroop’s Test?
                    Question = "Welches Wort wurde bei {0} eingegeben?",
                },
                [SStroopsTest.QColor] = new()
                {
                    // English: What was the {1} submitted word’s color in {0}?
                    // Example: What was the first submitted word’s color in Stroop’s Test?
                    Question = "Welche Farbe hatte das {1}e bei {0} eingegebene Wort?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Magenta"] = "Magenta",
                        ["White"] = "Weiß",
                    },
                },
            },
            Discriminators = new()
            {
                [SStroopsTest.DWord] = new()
                {
                    // English: the Stroop’s Test whose {0} submitted word was “{1}”
                    // Example: the Stroop’s Test whose first submitted word was “red”
                    Discriminator = "dem Stroop-Test, bei dem als {0}es Wort „{1}“ eingegeben wurde",
                },
                [SStroopsTest.DColor] = new()
                {
                    // English: the Stroop’s Test whose {0} submitted word’s color was {1}
                    // Example: the Stroop’s Test whose first submitted word’s color was red
                    Discriminator = "dem Stroop-Test, bei dem das als {0}es eingegeben Wort {1} war",
                    Arguments = new()
                    {
                        ["red"] = "rot",
                        ["yellow"] = "gelb",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["magenta"] = "magenta",
                        ["white"] = "weiß",
                    },
                },
            },
        },

        // Stupid Slots
        [typeof(SStupidSlots)] = new()
        {
            ModuleName = "Dummer Automat",
            ModuleNameDative = "Dummen Automat",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SStupidSlots.Values] = new()
                {
                    // English: What was the value of the {1} arrow in {0}?
                    // Example: What was the value of the top-left arrow in Stupid Slots?
                    Question = "Welchen Wert hatte bei {0} der {1} Pfeil?",
                    Arguments = new()
                    {
                        ["top-left"] = "obere linke",
                        ["top-middle"] = "obere mittlere",
                        ["top-right"] = "obere rechte",
                        ["bottom-left"] = "untere linke",
                        ["bottom-middle"] = "untere mittlere",
                        ["bottom-right"] = "untere rechte",
                    },
                },
            },
        },

        // Subbly Jubbly
        [typeof(SSubblyJubbly)] = new()
        {
            Questions = new()
            {
                [SSubblyJubbly.Substitutions] = new()
                {
                    // English: What was a substitution word in {0}?
                    Question = "Was war bei {0} ein Ersatzwort?",
                },
            },
        },

        // Subscribe to Pewdiepie
        [typeof(SSubscribeToPewdiepie)] = new()
        {
            Questions = new()
            {
                [SSubscribeToPewdiepie.SubCount] = new()
                {
                    // English: How many subscribers does {1} have in {0}?
                    // Example: How many subscribers does PewDiePie have in Subscribe to Pewdiepie?
                    Question = "Wie viele Abonnenten hatte {1} bei {0}?",
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
            Questions = new()
            {
                [SSubway.Bread] = new()
                {
                    // English: Which bread did the customer ask for in {0}?
                    Question = "Welches Brot wurde bei {0} vom Kunden verlangt?",
                },
                [SSubway.Items] = new()
                {
                    // English: Which of these was not asked for in {0}?
                    Question = "Welche Zutat wurde bei {0} nicht verlangt?",
                },
            },
        },

        // Sugar Skulls
        [typeof(SSugarSkulls)] = new()
        {
            ModuleName = "Zuckerschädel",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SSugarSkulls.Skull] = new()
                {
                    // English: What skull was shown on the {1} square in {0}?
                    // Example: What skull was shown on the top square in Sugar Skulls?
                    Question = "Welcher Schädel war bei {0} auf dem {1} Feld?",
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["bottom-left"] = "unteren linken",
                        ["bottom-right"] = "unteren rechten",
                    },
                },
                [SSugarSkulls.Availability] = new()
                {
                    // English: Which skull {1} present in {0}?
                    // Example: Which skull was present in Sugar Skulls?
                    Question = "Welcher Schädel war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["was"] = "anwesend",
                        ["was not"] = "abwesend",
                    },
                },
            },
        },

        // Suits and Colours
        [typeof(SSuitsAndColours)] = new()
        {
            Questions = new()
            {
                [SSuitsAndColours.Colour] = new()
                {
                    // English: What was the colour of this cell in {0}? (+ sprite)
                    // Example: What was the colour of this cell in Suits and Colours? (+ sprite)
                    Question = "Welche Farbe hatte diese Zelle bei {0}?",
                    Answers = new()
                    {
                        ["yellow"] = "gelb",
                        ["green"] = "grün",
                        ["orange"] = "orange",
                        ["red"] = "rot",
                    },
                },
                [SSuitsAndColours.Suit] = new()
                {
                    // English: What was the suit of this cell in {0}? (+ sprite)
                    // Example: What was the suit of this cell in Suits and Colours? (+ sprite)
                    Question = "Welche Spielkartenfarbe hatte diese Zelle bei {0}?",
                    Answers = new()
                    {
                        ["spades"] = "Pik",
                        ["hearts"] = "Herz",
                        ["clubs"] = "Kreuz",
                        ["diamonds"] = "Karo",
                    },
                },
            },
        },

        // Superparsing
        [typeof(SSuperparsing)] = new()
        {
            Questions = new()
            {
                [SSuperparsing.Displayed] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "Welches Wort war bei {0} auf dem Display?",
                },
            },
        },

        // SUSadmin
        [typeof(SSUSadmin)] = new()
        {
            Questions = new()
            {
                [SSUSadmin.Security] = new()
                {
                    // English: Which security protocol was installed in {0}?
                    Question = "Welches Sicherheitsprotokoll war bei {0} installiert?",
                },
                [SSUSadmin.Version] = new()
                {
                    // English: What was the version number in {0}?
                    Question = "Was war bei {0} die Versionsnummer?",
                },
            },
        },

        // The Switch
        [typeof(SSwitch)] = new()
        {
            ModuleName = "Der Schalter",
            ModuleNameDative = "Schalter",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SSwitch.InitialColor] = new()
                {
                    // English: What color was the {1} LED on the {2} flip of {0}?
                    // Example: What color was the top LED on the first flip of The Switch?
                    Question = "Welche Farbe hatte bei {0} die {1} LED beim {2}en Umschalten?",
                    Arguments = new()
                    {
                        ["top"] = "obere",
                        ["bottom"] = "untere",
                    },
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["orange"] = "orange",
                        ["yellow"] = "gelb",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["purple"] = "lila",
                    },
                },
            },
        },

        // Switches
        [typeof(SSwitches)] = new()
        {
            ModuleName = "Schalter",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SSwitches.InitialPosition] = new()
                {
                    // English: What was the initial position of the switches in {0}?
                    Question = "Wie standen bei {0} die Schalter am Anfang?",
                },
            },
        },

        // Switching Maze
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

        // Symbolic Coordinates
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

        // Symbolic Tasha
        [typeof(SSymbolicTasha)] = new()
        {
            ModuleName = "Symbole-Tasha",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SSymbolicTasha.DirectionFlashes] = new()
                {
                    // English: Which button flashed {1} in the final sequence of {0}?
                    // Example: Which button flashed first in the final sequence of Symbolic Tasha?
                    Question = "Welche Taste hat bei {0} als {1}e geblinkt?",
                    Answers = new()
                    {
                        ["Top"] = "obere",
                        ["Right"] = "rechte",
                        ["Bottom"] = "untere",
                        ["Left"] = "linke",
                    },
                },
                [SSymbolicTasha.ColorFlashes] = new()
                {
                    // English: Which button flashed {1} in the final sequence of {0}?
                    // Example: Which button flashed first in the final sequence of Symbolic Tasha?
                    Question = "Welche Taste hat bei {0} als {1}e geblinkt?",
                    Answers = new()
                    {
                        ["Pink"] = "pink",
                        ["Green"] = "grün",
                        ["Yellow"] = "gelb",
                        ["Blue"] = "blau",
                    },
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

        // Synapse Says
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

        // SYNC-125 [3]
        [typeof(SSync125_3)] = new()
        {
            Questions = new()
            {
                [SSync125_3.Word] = new()
                {
                    // English: What was displayed on the screen in the {1} stage of {0}?
                    // Example: What was displayed on the screen in the first stage of SYNC-125 [3]?
                    Question = "What was displayed on the screen in stage {1} of {0}?",
                },
            },
        },

        // Synonyms
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

        // Sysadmin
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

        // TAC
        [typeof(STAC)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [STAC.SwappedCard] = new()
                {
                    // English: Which card was {1} in the swap in {0}?
                    // Example: Which card was given away in the swap in TAC?
                    Question = "Welche Karte wurde bei {0} {1}?",
                    Arguments = new()
                    {
                        ["given away"] = "weggegeben",
                        ["received"] = "erhalten",
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
                        ["backwards 3"] = "rückwärts 3",
                        ["backwards 4"] = "rückwärts 4",
                        ["backwards 5"] = "rückwärts 5",
                        ["single-step 6"] = "Einzelschritt 6",
                        ["single-step 7"] = "Einzelschritt 7",
                        ["8 or discard"] = "8 oder Aussetzen",
                        ["9 or discard"] = "9 oder Aussetzen",
                        ["10 or discard"] = "10 oder Aussetzen",
                        ["Warrior"] = "Krieger",
                        ["Trickster"] = "Trickser",
                    },
                },
                [STAC.HeldCard] = new()
                {
                    // English: Which card was in your hand in {0}?
                    Question = "Welche Karte hatten wir war bei {0} auf der Hand?",
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
                        ["backwards 3"] = "rückwärts 3",
                        ["backwards 4"] = "rückwärts 4",
                        ["backwards 5"] = "rückwärts 5",
                        ["single-step 6"] = "Einzelschritt 6",
                        ["single-step 7"] = "Einzelschritt 7",
                        ["8 or discard"] = "8 oder Aussetzen",
                        ["9 or discard"] = "9 oder Aussetzen",
                        ["10 or discard"] = "10 oder Aussetzen",
                        ["Warrior"] = "Krieger",
                        ["Trickster"] = "Trickser",
                    },
                },
            },
        },

        // Tap Code
        [typeof(STapCode)] = new()
        {
            ModuleName = "Klopfzeichen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [STapCode.ReceivedWord] = new()
                {
                    // English: What was the received word in {0}?
                    Question = "Welches Wort wurde bei {0} empfangen?",
                },
            },
        },

        // Tasha Squeals
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
                    Answers = new()
                    {
                        ["Pink"] = "Pink",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                        ["Blue"] = "Blue",
                    },
                },
            },
        },

        // Tasque Managing
        [typeof(STasqueManaging)] = new()
        {
            Questions = new()
            {
                [STasqueManaging.StartingPos] = new()
                {
                    // English: Where was the starting position in {0}?
                    Question = "Wo ging es bei {0} los?",
                },
            },
        },

        // The Tea Set
        [typeof(STeaSet)] = new()
        {
            ModuleName = "Das Teeservice",
            ModuleNameDative = "Teeservice",
            Questions = new()
            {
                [STeaSet.DisplayedIngredients] = new()
                {
                    // English: Which ingredient was displayed {1}, from left to right, in {0}?
                    // Example: Which ingredient was displayed first, from left to right, in The Tea Set?
                    Question = "Welche Zutat war bei {0} die {1}e von links?",
                },
            },
        },

        // Technical Keypad
        [typeof(STechnicalKeypad)] = new()
        {
            ModuleName = "Technisches Tastenfeld",
            ModuleNameDative = "Technischen Tastenfeld",
            Questions = new()
            {
                [STechnicalKeypad.DisplayedDigits] = new()
                {
                    // This question is depicted visually, rather than with words. The translation here will only be used for logging.
                    Question = "Was war bei {0} die {1}e Ziffer auf dem Display?",
                },
            },
        },

        // Ten-Button Color Code
        [typeof(STenButtonColorCode)] = new()
        {
            ModuleName = "Zehn-Tasten-Farbcode",
            Questions = new()
            {
                [STenButtonColorCode.InitialColors] = new()
                {
                    // English: What was the initial color of the {1} button in the {2} stage of {0}?
                    // Example: What was the initial color of the first button in the first stage of Ten-Button Color Code?
                    Question = "Was war bei {0} am Anfang der {2}en Stufe die Farbe der {1}en Taste?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                    },
                },
            },
        },

        // Tenpins
        [typeof(STenpins)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Tenpin-Bowling",
            Questions = new()
            {
                [STenpins.Splits] = new()
                {
                    // English: What was the {1} split in {0}?
                    // Example: What was the red split in Tenpins?
                    Question = "Welcher Split war bei {0} {1}?",
                    Arguments = new()
                    {
                        ["red"] = "rot",
                        ["green"] = "grün",
                        ["blue"] = "blau",
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
            Questions = new()
            {
                [STetriamonds.PulsingColours] = new()
                {
                    // English: What colour triangle pulsed {1} in {0}?
                    // Example: What colour triangle pulsed first in Tetriamonds?
                    Question = "What colour triangle pulsed {1} in {0}?",
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

        // Thinking Wires
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
                    Question = "What was the display number in {0}?",
                },
            },
        },

        // Third Base
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

        // Thirty Dollar Module
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

        // Tic Tac Toe
        [typeof(STicTacToe)] = new()
        {
            ModuleName = "Tic-Tac-Toe",
            Questions = new()
            {
                [STicTacToe.InitialState] = new()
                {
                    // English: What was on the {1} button at the start of {0}?
                    // Example: What was on the top-left button at the start of Tic Tac Toe?
                    Question = "Was war bei {0} am Anfang auf der {1} Taste?",
                    Arguments = new()
                    {
                        ["top-left"] = "oberen linken",
                        ["top-middle"] = "oberen mittleren",
                        ["top-right"] = "oberen rechten",
                        ["middle-left"] = "mittleren linken",
                        ["middle-center"] = "mittigen",
                        ["middle-right"] = "mittleren rechten",
                        ["bottom-left"] = "unteren linken",
                        ["bottom-middle"] = "unteren mittleren",
                        ["bottom-right"] = "unteren rechten",
                    },
                },
            },
            Discriminators = new()
            {
                [STicTacToe.Discriminator] = new()
                {
                    // English: the Tic Tac Toe where the {0} button was {1}
                    // Example: the Tic Tac Toe where the top-left button was 1
                    Discriminator = "dem Tic-Tac-Toe, bei dem die {0} Taste {1} zeigte,",
                    Arguments = new()
                    {
                        ["top-left"] = "obere linke",
                        ["top-middle"] = "obere mittlere",
                        ["top-right"] = "obere rechte",
                        ["middle-left"] = "mittlere linke",
                        ["middle-center"] = "mittige",
                        ["middle-right"] = "mittlere rechte",
                        ["bottom-left"] = "untere linke",
                        ["bottom-middle"] = "untere mittlere",
                        ["bottom-right"] = "untere rechte",
                    },
                },
            },
        },

        // Time Signatures
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

        // Timezone
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
                    Question = "Which of these squares was safe in row {1} in {0}?",
                },
            },
        },

        // Topsy Turvy
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

        // Touch Transmission
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

        // Transmitted Morse
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
                    Question = "What colour triangle pulsed {1} in {0}?",
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

        // Triple Term
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

        // Turtle Robot
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

        // Two Bits
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

        // Twodoku
        [typeof(STwodoku)] = new()
        {
            ModuleName = "Zwodoku",
            Questions = new()
            {
                [STwodoku.Givens] = new()
                {
                    // English: Which of these squares in {0} was {1}?
                    // Example: Which of these squares in Twodoku was a given digit?
                    Question = "Welche dieser Felder bei {0} war {1}?",
                    Arguments = new()
                    {
                        ["a given digit"] = "eine vorgegebene Ziffer",
                        ["a given shape"] = "eine vorgegebene Form",
                        ["highlighted"] = "hervorgehoben",
                    },
                },
                [STwodoku.GridPositions] = new()
                {
                    // English: What was in this grid position in {0}? (+ sprite)
                    Question = "Was war bei {0} an dieser Stelle im Gitter?",
                },
            },
        },

        // Ultimate Cipher
        [typeof(SUltimateCipher)] = new()
        {
            ModuleName = "Ultimative Geheimschrift",
            ModuleNameDative = "Ultimativen Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SUltimateCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Ultimate Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "ober",
                        ["middle"] = "mittler",
                        ["bottom"] = "unter",
                    },
                },
            },
        },

        // Ultimate Cycle
        [typeof(SUltimateCycle)] = new()
        {
            ModuleName = "Ultimative Schiffer",
            ModuleNameDative = "Ultimativen Schiffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SUltimateCycle.DialDirections] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Ultimate Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}te Zeiger?",
                },
                [SUltimateCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Ultimate Cycle?
                    Question = "Welcher Buchstabe stand bei {0} auf dem {1}en Zeiger?",
                },
            },
            Discriminators = new()
            {
                [SUltimateCycle.LabelDiscriminator] = new()
                {
                    // English: the Ultimate Cycle that had the letter {0} on a dial
                    // Example: the Ultimate Cycle that had the letter A on a dial
                    Discriminator = "der Ultimativen Schiffer, bei der der Buchstabe {0} vorkam,",
                },
            },
        },

        // The Ultracube
        [typeof(SUltracube)] = new()
        {
            Questions = new()
            {
                [SUltracube.Rotations] = new()
                {
                    // English: What was the {1} rotation in {0}?
                    // Example: What was the first rotation in The Ultracube?
                    Question = "Was war die {1}e Rotation in {0}?",
                },
            },
        },

        // UltraStores
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

        // Uncolored Squares
        [typeof(SUncoloredSquares)] = new()
        {
            ModuleName = "Ungefärbte Felder",
            ModuleNameDative = "Ungefärbten Felder",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SUncoloredSquares.FirstStage] = new()
                {
                    // English: What was the {1} color in reading order used in the first stage of {0}?
                    // Example: What was the first color in reading order used in the first stage of Uncolored Squares?
                    Question = "Was war bei {0} in der ersten Stufe die in Leserichtung {1}e verwendete Farbe?",
                    Answers = new()
                    {
                        ["White"] = "Weiß",
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Yellow"] = "Gelb",
                        ["Magenta"] = "Magenta",
                    },
                },
            },
        },

        // Uncolored Switches
        [typeof(SUncoloredSwitches)] = new()
        {
            ModuleName = "Ungefärbte Schalter",
            ModuleNameDative = "Ungefärbten Schaltern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SUncoloredSwitches.InitialState] = new()
                {
                    // English: What was the initial state of the switches in {0}?
                    Question = "Wie sahen bei {0} die Schalter am Anfang aus?",
                },
                [SUncoloredSwitches.LedColors] = new()
                {
                    // English: What color was the {1} LED in reading order in {0}?
                    // Example: What color was the first LED in reading order in Uncolored Switches?
                    Question = "Welche Farbe hatte bei {0} die in Leserichtung {1}e LED?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["turquoise"] = "türkis",
                        ["orange"] = "orange",
                        ["purple"] = "lila",
                        ["white"] = "weiß",
                        ["black"] = "schwarz",
                    },
                },
            },
        },

        // Uncolour Flash
        [typeof(SUncolourFlash)] = new()
        {
            ModuleName = "Ungefärbte Folge",
            ModuleNameDative = "Ungefärbten Folge",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SUncolourFlash.Displays] = new()
                {
                    // English: What was the {1} in the {2} position of the “{3}” sequence of {0}?
                    // Example: What was the word in the first position of the “YES” sequence of Uncolour Flash?
                    Question = "Was war bei {0} {1} in der {2}en Position der „{3}“-Folge?",
                    Arguments = new()
                    {
                        ["word"] = "das Wort",
                        ["colour of the word"] = "die Wortfarbe",
                    },
                },
            },
            Discriminators = new()
            {
                [SUncolourFlash.Discriminator] = new()
                {
                    // English: the Uncolour Flash where the {0} in the {1} position of the “{2}” sequence was {3}
                    // Example: the Uncolour Flash where the word in the first position of the “YES” sequence was Red
                    Discriminator = "der Unfarbfolge, bei der {0} in der {1}en Position der „{2}“-Folge {3} war,",
                    Arguments = new()
                    {
                        ["word"] = "das Wort",
                        ["colour of the word"] = "die Wortfarbe",
                    },
                },
            },
        },

        // Undertunneling
        [typeof(SUndertunneling)] = new()
        {
            ModuleName = "Untertunnelung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SUndertunneling.PositionInMazeAfterPhaseOne] = new()
                {
                    // English: What was the position in the maze after the first phase in {0}?
                    Question = "Wo waren wir bei {0} nach der ersten Phase in Labyrinth?",
                },
            },
        },

        // Unfair Cipher
        [typeof(SUnfairCipher)] = new()
        {
            ModuleName = "Unfaire Geheimschrift",
            ModuleNameDative = "Unfairen Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SUnfairCipher.Instructions] = new()
                {
                    // English: What was the {1} received instruction in {0}?
                    // Example: What was the first received instruction in Unfair Cipher?
                    Question = "Was war bei {0} die {1}e empfangene Anweisung?",
                },
            },
        },

        // Unfair's Cruel Revenge
        [typeof(SUnfairsCruelRevenge)] = new()
        {
            ModuleName = "Unfairs Höllische Rache",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SUnfairsCruelRevenge.Instructions] = new()
                {
                    // English: What was the {1} decrypted instruction in {0}?
                    // Example: What was the first decrypted instruction in Unfair's Cruel Revenge?
                    Question = "Was war bei {0} die {1}e entschlüsselte Anweisung?",
                },
                [SUnfairsCruelRevenge.InstructionsLegacy] = new()
                {
                    // English: What was the {1} decrypted instruction in {0}?
                    // Example: What was the first decrypted instruction in Unfair's Cruel Revenge?
                    Question = "Was war bei {0} die {1}e entschlüsselte Anweisung?",
                },
            },
        },

        // Unfair’s Revenge
        [typeof(SUnfairsRevenge)] = new()
        {
            ModuleName = "Unfairs Rache",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SUnfairsRevenge.Instructions] = new()
                {
                    // English: What was the {1} decrypted instruction in {0}?
                    // Example: What was the first decrypted instruction in Unfair’s Revenge?
                    Question = "Was war bei {0} die {1}e entschlüsselte Anweisung?",
                },
            },
        },

        // Unicode
        [typeof(SUnicode)] = new()
        {
            Questions = new()
            {
                [SUnicode.SortedAnswer] = new()
                {
                    // English: What was the {1} submitted code in {0}?
                    // Example: What was the first submitted code in Unicode?
                    Question = "Was war bei {0} der als {1}e eingegebene Code?",
                },
            },
        },

        // UNO!
        [typeof(SUNO)] = new()
        {
            Questions = new()
            {
                [SUNO.InitialCard] = new()
                {
                    // English: What was the initial card in {0}?
                    Question = "Was war bei {0} die Anfangskarte?",
                    Answers = new()
                    {
                        ["Red 0"] = "Rote 0",
                        ["Red 1"] = "Rote 1",
                        ["Red 2"] = "Rote 2",
                        ["Red 3"] = "Rote 3",
                        ["Red 4"] = "Rote 4",
                        ["Red 5"] = "Rote 5",
                        ["Red 6"] = "Rote 6",
                        ["Red 7"] = "Rote 7",
                        ["Red 8"] = "Rote 8",
                        ["Red 9"] = "Rote 9",
                        ["Red +2"] = "Rote +2",
                        ["Red Skip"] = "Rote Aussetzen-Karte",
                        ["Red Reverse"] = "Rote Richtungswechsel-Karte",
                        ["Green 0"] = "Grüne 0",
                        ["Green 1"] = "Grüne 1",
                        ["Green 2"] = "Grüne 2",
                        ["Green 3"] = "Grüne 3",
                        ["Green 4"] = "Grüne 4",
                        ["Green 5"] = "Grüne 5",
                        ["Green 6"] = "Grüne 6",
                        ["Green 7"] = "Grüne 7",
                        ["Green 8"] = "Grüne 8",
                        ["Green 9"] = "Grüne 9",
                        ["Green +2"] = "Grüne +2",
                        ["Green Skip"] = "Grüne Aussetzen-Karte",
                        ["Green Reverse"] = "Grüne Richtungswechsel-Karte",
                        ["Yellow 0"] = "Gelbe 0",
                        ["Yellow 1"] = "Gelbe 1",
                        ["Yellow 2"] = "Gelbe 2",
                        ["Yellow 3"] = "Gelbe 3",
                        ["Yellow 4"] = "Gelbe 4",
                        ["Yellow 5"] = "Gelbe 5",
                        ["Yellow 6"] = "Gelbe 6",
                        ["Yellow 7"] = "Gelbe 7",
                        ["Yellow 8"] = "Gelbe 8",
                        ["Yellow 9"] = "Gelbe 9",
                        ["Yellow +2"] = "Gelbe +2",
                        ["Yellow Skip"] = "Gelbe Aussetzen-Karte",
                        ["Yellow Reverse"] = "Gelbe Richtungswechsel-Karte",
                        ["Blue 0"] = "Blaue 0",
                        ["Blue 1"] = "Blaue 1",
                        ["Blue 2"] = "Blaue 2",
                        ["Blue 3"] = "Blaue 3",
                        ["Blue 4"] = "Blaue 4",
                        ["Blue 5"] = "Blaue 5",
                        ["Blue 6"] = "Blaue 6",
                        ["Blue 7"] = "Blaue 7",
                        ["Blue 8"] = "Blaue 8",
                        ["Blue 9"] = "Blaue 9",
                        ["Blue +2"] = "Blaue +2",
                        ["Blue Skip"] = "Blaue Aussetzen-Karte",
                        ["Blue Reverse"] = "Blaue Richtungswechsel-Karte",
                        ["+4"] = "+4",
                        ["Wild"] = "Farbwunsch-Karte",
                    },
                },
            },
        },

        // Unordered Keys
        [typeof(SUnorderedKeys)] = new()
        {
            Questions = new()
            {
                [SUnorderedKeys.KeyColor] = new()
                {
                    // English: What color was this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "Welche Farbe hatte diese Taste in der {1}en Stufe von {0}?",
                },
                [SUnorderedKeys.LabelColor] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was the label of this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "Welche Farbe hatte die Aufschrift dieser Taste in der {1}en Stufe von {0}?",
                },
                [SUnorderedKeys.Label] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What was the label of this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "Welche Aufschrift hatte diese Taste in der {1}en Stufe von {0}?",
                },
            },
        },

        // Unown Cipher
        [typeof(SUnownCipher)] = new()
        {
            ModuleName = "Unown-Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SUnownCipher.Answers] = new()
                {
                    // English: What was the {1} submitted letter in {0}?
                    // Example: What was the first submitted letter in Unown Cipher?
                    Question = "Was war bei {0} der als {1}e übermittelte Buchstabe?",
                },
            },
        },

        // Unpleasant Squares
        [typeof(SUnpleasantSquares)] = new()
        {
            ModuleName = "Unangenehme Quadrate",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SUnpleasantSquares.Color] = new()
                {
                    // English: What was the color of this square in {0}? (+ sprite)
                    Question = "Welche Farbe hatte dieses Quadrat bei {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Jade"] = "Jade",
                        ["Azure"] = "Azur",
                        ["Violet"] = "Violett",
                    },
                },
            },
        },

        // Updog
        [typeof(SUpdog)] = new()
        {
            Questions = new()
            {
                [SUpdog.Word] = new()
                {
                    // English: What was the text on {0}?
                    Question = "Wie lautete bei {0} der Text?",
                },
                [SUpdog.Color] = new()
                {
                    // English: What was the {1} color in the sequence on {0}?
                    // Example: What was the first color in the sequence on Updog?
                    Question = "Was war bei {0} die {1} Farbe in der Farbsequenz?",
                    Arguments = new()
                    {
                        ["first"] = "erste",
                        ["last"] = "letzte",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Orange"] = "Orange",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                    },
                },
            },
        },

        // USA Cycle
        [typeof(SUSACycle)] = new()
        {
            ModuleName = "USA-Schiffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SUSACycle.Displayed] = new()
                {
                    // English: Which state was displayed in {0}?
                    Question = "Welcher Bundesstaat kam bei {0} vor?",
                },
            },
        },

        // USA Maze
        [typeof(SUSAMaze)] = new()
        {
            ModuleName = "USA-Labyrinth",
            Questions = new()
            {
                [SUSAMaze.Origin] = new()
                {
                    // English: Which state did you depart from in {0}?
                    Question = "In welchem Bundesstaat ging es bei {0} los?",
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
                    Question = "Welches Wort kam bei {0} {1}?",
                    Arguments = new()
                    {
                        ["was"] = "vor",
                        ["was not"] = "nicht vor",
                    },
                },
            },
        },

        // Valves
        [typeof(SValves)] = new()
        {
            ModuleName = "Ventile",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SValves.InitialState] = new()
                {
                    // English: What was the initial state of {0}?
                    Question = "Was war bei {0} der Anfangszustand?",
                },
            },
        },

        // Varicolored Squares
        [typeof(SVaricoloredSquares)] = new()
        {
            ModuleName = "Mischgefärbte Felder",
            ModuleNameDative = "Mischgefärbten Feldern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SVaricoloredSquares.InitialColor] = new()
                {
                    // English: What was the initially pressed color on {0}?
                    Question = "Welche Farbe wurde bei {0} anfangs gedrückt?",
                    Answers = new()
                    {
                        ["White"] = "Weiß",
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Yellow"] = "Gelb",
                        ["Magenta"] = "Magenta",
                    },
                },
            },
        },

        // Varicolour Flash
        [typeof(SVaricolourFlash)] = new()
        {
            ModuleName = "Farbenfrohe Folge",
            ModuleNameDative = "Farbenfrohen Folge",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SVaricolourFlash.Words] = new()
                {
                    // English: What was the word of the {1} goal in {0}?
                    // Example: What was the word of the first goal in Varicolour Flash?
                    Question = "Was war bei {0} das {1}e Zielwort?",
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
                    Question = "Was war bei {0} die {1}e Zielfarbe?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                        ["White"] = "Weiß",
                    },
                },
            },
        },

        // Variety
        [typeof(SVariety)] = new()
        {
            ModuleName = "Vielfalt",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SVariety.LED] = new()
                {
                    // English: What color was the LED flashing in {0}?
                    Question = "Welche Farbe ist bei {0} bei der LED vorgekommen?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Blue"] = "Blau",
                        ["White"] = "Weiß",
                        ["Black"] = "Schwarz",
                    },
                },
                [SVariety.DigitDisplay] = new()
                {
                    // English: What digit was displayed, but not the answer, for the digit display in {0}?
                    Question = "Welche Ziffer kam bei {0} auf dem Zifferndisplay vor, war aber nicht die Lösung?",
                },
                [SVariety.LetterDisplay] = new()
                {
                    // English: What word could be formed, but was not the answer, for the letter display in {0}?
                    Question = "Welches Wort konnte bei {0} mit dem Buchstabendisplay gebildet werden, war aber nicht die Lösung?",
                },
                [SVariety.Timer] = new()
                {
                    // English: What was the maximum display for the {1} in {0}?
                    // Example: What was the maximum display for the timer in Variety?
                    Question = "Was war bei {0} die höchste Zahl auf dem {1}?",
                    Arguments = new()
                    {
                        ["timer"] = "Timer",
                        ["ascending timer"] = "aufsteigenden Timer",
                        ["descending timer"] = "absteigenden Timer",
                    },
                },
                [SVariety.ColoredKnob] = new()
                {
                    // English: What was n for the {1} in {0}?
                    // Example: What was n for the knob in Variety?
                    Question = "Was war bei {0} der Wert n beim {1}?",
                    Arguments = new()
                    {
                        ["knob"] = "Drehregler",
                        ["colored knob"] = "farbigen Drehregler",
                        ["red knob"] = "roten Drehregler",
                        ["black knob"] = "schwarzen Drehregler",
                        ["blue knob"] = "blauen Drehregler",
                        ["yellow knob"] = "gelben Drehregler",
                    },
                },
                [SVariety.Bulb] = new()
                {
                    // English: What was n for the {1} in {0}?
                    // Example: What was n for the bulb in Variety?
                    Question = "Was war bei {0} der Wert n bei der {1}?",
                    Arguments = new()
                    {
                        ["bulb"] = "Glühlampe",
                        ["red bulb"] = "roten Glühlampe",
                        ["yellow bulb"] = "gelben Glühlampe",
                    },
                },
            },
            Discriminators = new()
            {
                [SVariety.Has] = new()
                {
                    // English: the Variety that has {0}
                    // Example: the Variety that has one
                    Discriminator = "der Vielfalt{0}",
                    // Refer to translations.md to understand the weird strings
                    Arguments = new()
                    {
                        ["one\uE003 (LED)"] = ", die eine hat,",
                        ["one\uE003 (digit display)"] = ", die eins hat,",
                        ["one\uE003 (letter display)"] = ", die eins hat,",
                        ["one\uE003 (timer)"] = ", die einen hat,",
                        ["one\uE003 (knob)"] = ", die einen hat,",
                        ["one\uE003 (colored knob)"] = ", die einen hat,",
                        ["one\uE003 (redknob)"] = ", die einen hat,",
                        ["one\uE003 (yellowknob)"] = ", die einen hat,",
                        ["one\uE003 (blueknob)"] = ", die einen hat,",
                        ["one\uE003 (blackknob)"] = ", die einen hat,",
                        ["one\uE003 (bulb)"] = ", die eine hat,",
                        ["one\uE003 (redbulb)"] = ", die eine hat,",
                        ["one\uE003 (yellowbulb)"] = ", die eine hat,",
                        ["a knob"] = " mit einem Drehregler",
                        ["a colored knob"] = " mit einem farbigen Drehregler",
                        ["a white knob"] = " mit einem weißen Drehregler",
                        ["a red knob"] = " mit einem roten Drehregler",
                        ["a black knob"] = " mit einem schwarzen Drehregler",
                        ["a blue knob"] = " mit einem blauen Drehregler",
                        ["a yellow knob"] = " mit einem gelben Drehregler",
                        ["a keypad"] = " mit einem Tastenfeld",
                        ["a white keypad"] = " mit einem weißen Tastenfeld",
                        ["a red keypad"] = " mit einem roten Tastenfeld",
                        ["a yellow keypad"] = " mit einem gelben Tastenfeld",
                        ["a blue keypad"] = " mit einem blauen Tastenfeld",
                        ["a slider"] = " mit einem Schieber",
                        ["a horizontal slider"] = " mit einem horizontalen Schieber",
                        ["a vertical slider"] = " mit einem vertikalen Schieber",
                        ["an LED"] = " mit einem LED",
                        ["a digit display"] = " mit einem Zifferndisplay",
                        ["a wire"] = " mit einem Draht",
                        ["a black wire"] = " mit einem schwarzen Draht",
                        ["a blue wire"] = " mit einem blauen Draht",
                        ["a red wire"] = " mit einem roten Draht",
                        ["a yellow wire"] = " mit einem gelben Draht",
                        ["a white wire"] = " mit einem weißen Draht",
                        ["a button"] = " mit einem Knopf",
                        ["a red button"] = " mit einem roten Knopf",
                        ["a yellow button"] = " mit einem gelben Knopf",
                        ["a blue button"] = " mit einem blauen Knopf",
                        ["a white button"] = " mit einem weißen Knopf",
                        ["a letter display"] = " mit einem Buchstaben-Display",
                        ["a Braille display"] = " mit einem Braille-Display",
                        ["a key-in-lock"] = " mit einem Schlüssel",
                        ["a switch"] = " mit einem Schalter",
                        ["a red switch"] = " mit einem roten Schalter",
                        ["a yellow switch"] = " mit einem gelben Schalter",
                        ["a blue switch"] = " mit einem blauen Schalter",
                        ["a white switch"] = " mit einem weißen Schalter",
                        ["a timer"] = " mit einem Timer",
                        ["an ascending timer"] = " mit einem aufsteigenden Timer",
                        ["a descending timer"] = " mit einem absteigenden Timer",
                        ["a die"] = " mit einem Würfel",
                        ["a light-on-dark die"] = " mit einem dunklen Würfel mit hellen Punkten",
                        ["a dark-on-light die"] = " mit einem hellen Würfel mit dunklen Punkten",
                        ["a bulb"] = " mit einer Glühlampe",
                        ["a red bulb"] = " mit einer roten Glühlampe",
                        ["a yellow bulb"] = " mit einer gelben Glühlampe",
                        ["a maze"] = " mit einem Labyrinth",
                        ["a 3×3 maze"] = " mit einem 3×3-Labyrinth",
                        ["a 3×4 maze"] = " mit einem 3×4-Labyrinth",
                        ["a 4×3 maze"] = " mit einem 4×3-Labyrinth",
                        ["a 4×4 maze"] = " mit einem 4×4-Labyrinth",
                    },
                },
            },
        },

        // Vcrcs
        [typeof(SVcrcs)] = new()
        {
            Questions = new()
            {
                [SVcrcs.Word] = new()
                {
                    // English: What was the word in {0}?
                    Question = "Was war das Wort bei {0}?",
                },
            },
        },

        // Vectors
        [typeof(SVectors)] = new()
        {
            ModuleName = "Vektoren",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SVectors.Colors] = new()
                {
                    // English: What was the color of the {1} vector in {0}?
                    // Example: What was the color of the first vector in Vectors?
                    Question = "Welche Farbe hatte der {1} Vektor bei {0}?",
                    Arguments = new()
                    {
                        ["first"] = "erste",
                        ["second"] = "zweite",
                        ["third"] = "dritte",
                        ["only"] = "einzige",
                    },
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Violett",
                    },
                },
            },
        },

        // Vexillology
        [typeof(SVexillology)] = new()
        {
            ModuleName = "Flaggenkunde",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SVexillology.Colors] = new()
                {
                    // English: What was the {1} flagpole color on {0}?
                    // Example: What was the first flagpole color on Vexillology?
                    Question = "Welche Farbe hatte bei {0} der {1}e Fahnenmast?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Green"] = "Grün",
                        ["Yellow"] = "Gelb",
                        ["Blue"] = "Blau",
                        ["Aqua"] = "Aqua",
                        ["White"] = "Weiß",
                        ["Black"] = "Schwarz",
                    },
                },
            },
        },

        // Violet Cipher
        [typeof(SVioletCipher)] = new()
        {
            ModuleName = "Violette Geheimschrift",
            ModuleNameDative = "Violetten Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SVioletCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Violet Cipher?
                    Question = "Was war bei {0} auf dem {1} Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["middle"] = "mittleren",
                        ["bottom"] = "unteren",
                    },
                },
            },
        },

        // Visual Impairment
        [typeof(SVisualImpairment)] = new()
        {
            ModuleName = "Sehbehinderung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SVisualImpairment.Colors] = new()
                {
                    // English: What was the desired color in the {1} stage on {0}?
                    // Example: What was the desired color in the first stage on Visual Impairment?
                    Question = "Welche Farbe wurde bei {0} in der {1}en Stufe verlangt?",
                    Answers = new()
                    {
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Red"] = "Rot",
                        ["White"] = "Weiß",
                    },
                },
            },
        },

        // Walking Cube
        [typeof(SWalkingCube)] = new()
        {
            ModuleName = "Wandernder Würfel",
            ModuleNameDative = "Wandernden Würfel",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SWalkingCube.Path] = new()
                {
                    // English: Which of these cells was part of the cube’s path in {0}?
                    Question = "Welche dieser Zellen kamen bei {0} im Pfad des Würfels vor?",
                },
            },
        },

        // Warning Signs
        [typeof(SWarningSigns)] = new()
        {
            ModuleName = "Warnschilder",
            ModuleNameDative = "Warnschildern",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SWarningSigns.DisplayedSign] = new()
                {
                    // English: What was the displayed sign in {0}?
                    Question = "Welches Schild wurde bei {0} angezeigt?",
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
                    Question = "Welche Örtlichkeit kam bei {0} vor?",
                },
            },
        },

        // Watching Paint Dry
        [typeof(SWatchingPaintDry)] = new()
        {
            ModuleName = "Farbe Beim Trocknen Zusehen",
            Questions = new()
            {
                [SWatchingPaintDry.StrokeCount] = new()
                {
                    // English: How many brush strokes were heard in {0}?
                    Question = "Wie viele Pinselstriche waren bei {0} zu hören?",
                },
            },
        },

        // Wavetapping
        [typeof(SWavetapping)] = new()
        {
            ModuleName = "Wellenschlagen",
            Questions = new()
            {
                [SWavetapping.Patterns] = new()
                {
                    // English: What was the correct pattern on the {1} stage in {0}?
                    // Example: What was the correct pattern on the first stage in Wavetapping?
                    Question = "Was war bei {0} das richtige Muster in der {1}en Stufe?",
                },
                [SWavetapping.Colors] = new()
                {
                    // English: What was the color on the {1} stage in {0}?
                    // Example: What was the color on the first stage in Wavetapping?
                    Question = "Was war bei {0} die Farbe in der {1}en Stufe?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Orange-Yellow"] = "Orange-Gelb",
                        ["Chartreuse"] = "Chartreuse",
                        ["Lime"] = "Limette",
                        ["Green"] = "Grün",
                        ["Seafoam Green"] = "Seeschaumgrün",
                        ["Cyan-Green"] = "Türkisgrün",
                        ["Turquoise"] = "Türkis",
                        ["Dark Blue"] = "Dunkelblau",
                        ["Indigo"] = "Indigo",
                        ["Purple"] = "Lila",
                        ["Purple-Magenta"] = "Lila-Magenta",
                        ["Magenta"] = "Magenta",
                        ["Pink"] = "Pink",
                        ["Gray"] = "Grau",
                    },
                },
            },
        },

        // The Weakest Link
        [typeof(SWeakestLink)] = new()
        {
            ModuleName = "Schwächstes Glied",
            ModuleNameDative = "Schwächsten Glied",
            Questions = new()
            {
                [SWeakestLink.Elimination] = new()
                {
                    // English: Who did you eliminate in {0}?
                    Question = "Wer wurde bei {0} eliminiert?",
                },
                [SWeakestLink.MoneyPhaseName] = new()
                {
                    // English: Who made it to the Money Phase with you in {0}?
                    Question = "Wer hat es bei {0} mit uns in die Geldphase geschafft?",
                },
                [SWeakestLink.Skill] = new()
                {
                    // English: What was {1}’s skill in {0}?
                    // Example: What was Annie’s skill in The Weakest Link?
                    Question = "Was war {1}s Fachgebiet bei {0}?",
                },
                [SWeakestLink.Ratio] = new()
                {
                    // English: What ratio did {1} get in the Question Phase in {0}?
                    // Example: What ratio did Annie get in the Question Phase in The Weakest Link?
                    Question = "Welches Verhältnis hat {1} in der Fragenphase bei {0} erreicht?",
                },
            },
        },

        // What’s on Second
        [typeof(SWhatsOnSecond)] = new()
        {
            Questions = new()
            {
                [SWhatsOnSecond.DisplayText] = new()
                {
                    // English: What was the display text in the {1} stage of {0}?
                    // Example: What was the display text in the first stage of What’s on Second?
                    Question = "Welcher Text erschien bei {0} in der {1}en Stufe?",
                },
                [SWhatsOnSecond.DisplayColor] = new()
                {
                    // English: What was the display text color in the {1} stage of {0}?
                    // Example: What was the display text color in the first stage of What’s on Second?
                    Question = "In welcher Farbe erschien bei {0} der Text in der {1}en Stufe?",
                    Answers = new()
                    {
                        ["Blue"] = "Blau",
                        ["Cyan"] = "Türkis",
                        ["Green"] = "Grün",
                        ["Magenta"] = "Magenta",
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                    },
                },
            },
        },

        // White Arrows
        [typeof(SWhiteArrows)] = new()
        {
            ModuleName = "Weiße Pfeile",
            ModuleNameDative = "Weißen Pfeilen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SWhiteArrows.Arrows] = new()
                {
                    // English: What was the {1} non-white arrow in {0}?
                    // Example: What was the first non-white arrow in White Arrows?
                    Question = "Was war bei {0} der {1}e nicht-weiße Pfeil?",
                    // Refer to translations.md to understand the weird strings
                    Additional = new()
                    {
                        ["Blue"] = "Blau",
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Purple"] = "Lila",
                        ["Orange"] = "Orange",
                        ["Cyan"] = "Türkis",
                        ["Teal"] = "Blaugrün",
                        ["Up"] = "Hoch",
                        ["Right"] = "Rechts",
                        ["Down"] = "Runter",
                        ["Left"] = "Links",
                        ["{0} {1}"] = "{0} {1}",
                    },
                },
            },
        },

        // White Cipher
        [typeof(SWhiteCipher)] = new()
        {
            ModuleName = "Weiße Geheimschrift",
            ModuleNameDative = "Weißen Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SWhiteCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in White Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "ober",
                        ["middle"] = "mittler",
                        ["bottom"] = "unter",
                    },
                },
            },
        },

        // WhoOF
        [typeof(SWhoOF)] = new()
        {
            Questions = new()
            {
                [SWhoOF.Display] = new()
                {
                    // English: What was the display in the {1} stage on {0}?
                    // Example: What was the display in the first stage on WhoOF?
                    Question = "Was war bei {0} in der {1}en Stufe auf dem Display?",
                },
            },
        },

        // Who’s on First
        [typeof(SWhosOnFirst)] = new()
        {
            Questions = new()
            {
                [SWhosOnFirst.Display] = new()
                {
                    // English: What was the display in the {1} stage on {0}?
                    // Example: What was the display in the first stage on Who’s on First?
                    Question = "Was war bei {0} in der {1}en Stufe auf dem Display?",
                },
            },
        },

        // Who’s on Gas
        [typeof(SWhosOnGas)] = new()
        {
            Questions = new()
            {
                [SWhosOnGas.Display] = new()
                {
                    // English: What was the display in the first phase of the {1} stage on {0}?
                    // Example: What was the display in the first phase of the first stage on Who’s on Gas?
                    Question = "Was war bei {0} in der ersten Phase der {1}en Stufe auf dem Display?",
                },
            },
        },

        // Who’s on Morse
        [typeof(SWhosOnMorse)] = new()
        {
            Questions = new()
            {
                [SWhosOnMorse.TransmitDisplay] = new()
                {
                    // English: What word was transmitted in the {1} stage on {0}?
                    // Example: What word was transmitted in the first stage on Who’s on Morse?
                    Question = "Welches Wort wurde bei {0} in der {1}en Stufe übermittelt?",
                },
            },
        },

        // The Wire
        [typeof(SWire)] = new()
        {
            ModuleName = "Der Draht",
            ModuleNameDative = "Draht",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SWire.DialColors] = new()
                {
                    // English: What was the color of the {1} dial in {0}?
                    // Example: What was the color of the top dial in The Wire?
                    Question = "Welche Farbe hatte bei {0} der {1} Drehregler?",
                    Arguments = new()
                    {
                        ["top"] = "obere",
                        ["bottom-left"] = "untere linke",
                        ["bottom-right"] = "untere rechte",
                    },
                    Answers = new()
                    {
                        ["blue"] = "blau",
                        ["green"] = "grün",
                        ["grey"] = "grau",
                        ["orange"] = "orange",
                        ["purple"] = "lila",
                        ["red"] = "rot",
                    },
                },
                [SWire.DisplayedNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Welche Zahl wurde bei {0} angezeigt?",
                },
            },
        },

        // Wire Ordering
        [typeof(SWireOrdering)] = new()
        {
            ModuleName = "Draht-Reihenfolge",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SWireOrdering.DisplayColor] = new()
                {
                    // English: What color was the {1} display from the left in {0}?
                    // Example: What color was the first display from the left in Wire Ordering?
                    Question = "Welche Farbe hatte bei {0} das {1}e Display von links?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["orange"] = "orange",
                        ["yellow"] = "gelb",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["purple"] = "lila",
                        ["white"] = "weiß",
                        ["black"] = "schwarz",
                    },
                },
                [SWireOrdering.DisplayNumber] = new()
                {
                    // English: What number was on the {1} display from the left in {0}?
                    // Example: What number was on the first display from the left in Wire Ordering?
                    Question = "Welche Zahl war bei {0} auf dem {1}en Display von links?",
                },
                [SWireOrdering.WireColor] = new()
                {
                    // English: What color was the {1} wire from the left in {0}?
                    // Example: What color was the first wire from the left in Wire Ordering?
                    Question = "Welche Farbe hatte bei {0} der {1}e Draht von links?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["orange"] = "orange",
                        ["yellow"] = "gelb",
                        ["green"] = "grün",
                        ["blue"] = "blau",
                        ["purple"] = "lila",
                        ["white"] = "weiß",
                        ["black"] = "schwarz",
                    },
                },
            },
        },

        // Wire Sequence
        [typeof(SWireSequence)] = new()
        {
            ModuleName = "Drahtfolge",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SWireSequence.ColorCount] = new()
                {
                    // English: How many {1} wires were there in {0}?
                    // Example: How many red wires were there in Wire Sequence?
                    Question = "Wie viele {1}e Drähte gab es bei {0}?",
                    Arguments = new()
                    {
                        ["red"] = "roten",
                        ["blue"] = "blauen",
                        ["black"] = "schwarzen",
                    },
                },
            },
        },

        // Wolf, Goat, and Cabbage
        [typeof(SWolfGoatAndCabbage)] = new()
        {
            ModuleName = "Wolf, Ziege und Kohl",
            Questions = new()
            {
                [SWolfGoatAndCabbage.Animals] = new()
                {
                    // English: Which of these was {1} on {0}?
                    // Example: Which of these was present on Wolf, Goat, and Cabbage?
                    Question = "Welches dieser Tiere kam bei {0} {1}?",
                    Arguments = new()
                    {
                        ["present"] = "vor",
                        ["not present"] = "nicht vor",
                    },
                },
                [SWolfGoatAndCabbage.BoatSize] = new()
                {
                    // English: What was the boat size in {0}?
                    Question = "Wie groß war bei {0} das Boot?",
                },
            },
        },

        // Word Count
        [typeof(SWordCount)] = new()
        {
            ModuleName = "Wortzähler",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SWordCount.Number] = new()
                {
                    // English: What was the displayed number on {0}?
                    Question = "Welche Zahl war bei {0} auf dem Display?",
                },
            },
        },

        // Working Title
        [typeof(SWorkingTitle)] = new()
        {
            ModuleName = "Arbeitstitel",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SWorkingTitle.Display] = new()
                {
                    // English: What was on the display in {0}?
                    Question = "Was stand bei {0} auf dem Display?",
                },
            },
        },

        // Wumbo
        [typeof(SWumbo)] = new()
        {
            Questions = new()
            {
                [SWumbo.Number] = new()
                {
                    // English: What was the number in {0}?
                    Question = "Welche Zahl war bei {0} zu sehen?",
                },
            },
        },

        // The Xenocryst
        [typeof(SXenocryst)] = new()
        {
            ModuleName = "Der Xenokrist",
            ModuleNameDative = "Xenokrist",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SXenocryst.Question] = new()
                {
                    // English: What was the color of the {1} flash in {0}?
                    // Example: What was the color of the first flash in The Xenocryst?
                    Question = "Welche Farbe ist bei {0} als {1}e aufgeleuchtet?",
                },
            },
        },

        // XmORse Code
        [typeof(SXmORseCode)] = new()
        {
            ModuleName = "XmORsezeichen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SXmORseCode.Word] = new()
                {
                    // English: What word did you decrypt in {0}?
                    Question = "Welches Wort wurde bei {0} entschlüsselt?",
                },
                [SXmORseCode.DisplayedLetters] = new()
                {
                    // English: What was the {1} displayed letter (in reading order) in {0}?
                    // Example: What was the first displayed letter (in reading order) in XmORse Code?
                    Question = "Welcher Buchstabe war bei {0} der {1}e (in Leserichtung)?",
                },
            },
        },

        // xobekuJ ehT
        [typeof(SXobekuJehT)] = new()
        {
            ModuleName = "xobkisuM eiD",
            Questions = new()
            {
                [SXobekuJehT.Song] = new()
                {
                    // English: What song was played on {0}?
                    Question = "Welches Lied wurde bei {0} abgespielt?",
                },
            },
        },

        // X-Ring
        [typeof(SXRing)] = new()
        {
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SXRing.Symbol] = new()
                {
                    // English: Which symbol was scanned in {0}?
                    Question = "Welches Symbol wurde bei {0} gescannt?",
                },
            },
        },

        // X-Rotor
        [typeof(SXRotor)] = new()
        {
            NeedsTranslation = true,
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
            ModuleName = "XY-Scanner",
            Questions = new()
            {
                [SXYRay.Shapes] = new()
                {
                    // English: Which shape was scanned by {0}?
                    Question = "Welcher Körper wurde bei {0} gescannt?",
                },
            },
        },

        // Yahtzee
        [typeof(SYahtzee)] = new()
        {
            ModuleName = "Kniffel",
            Questions = new()
            {
                [SYahtzee.InitialRoll] = new()
                {
                    // English: What was the initial roll on {0}?
                    Question = "Was wurde bei {0} am Anfang gewürfelt?",
                    Answers = new()
                    {
                        ["Yahtzee"] = "Kniffel (Fünfling)",
                        ["large straight"] = "große Straße",
                        ["small straight"] = "kleine Straße",
                        ["four of a kind"] = "Vierling",
                        ["full house"] = "Full-House",
                        ["three of a kind"] = "Drilling",
                        ["two pairs"] = "zwei Paare",
                        ["pair"] = "ein Paar",
                    },
                },
            },
        },

        // Yellow Arrows
        [typeof(SYellowArrows)] = new()
        {
            ModuleName = "Gelbe Pfeile",
            ModuleNameDative = "Gelben Pfeilen",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SYellowArrows.StartingRow] = new()
                {
                    // English: What was the starting row letter in {0}?
                    Question = "Welcher Buchstabe war bei {0} die Startreihe?",
                },
            },
        },

        // The Yellow Button
        [typeof(SYellowButton)] = new()
        {
            ModuleName = "Der Gelbe Knopf",
            ModuleNameDative = "Gelben Knopf",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SYellowButton.Colors] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in The Yellow Button?
                    Question = "Was war bei {0} die {1}e Farbe?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Cyan"] = "Türkis",
                        ["Blue"] = "Blau",
                        ["Magenta"] = "Magenta",
                    },
                },
            },
        },

        // Yellow Button’t
        [typeof(SYellowButtont)] = new()
        {
            ModuleName = "Gelber Knopf Mal Anders",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SYellowButtont.Filename] = new()
                {
                    // English: What was the filename in {0}?
                    Question = "Was war bei {0} der Dateiname?",
                },
            },
        },

        // Yellow Cipher
        [typeof(SYellowCipher)] = new()
        {
            ModuleName = "Gelbe Geheimschrift",
            ModuleNameDative = "Gelben Geheimschrift",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SYellowCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Yellow Cipher?
                    Question = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                    Arguments = new()
                    {
                        ["top"] = "ober",
                        ["middle"] = "mittler",
                        ["bottom"] = "unter",
                    },
                },
            },
        },

        // Zero, Zero
        [typeof(SZeroZero)] = new()
        {
            ModuleName = "Null, Null",
            Questions = new()
            {
                [SZeroZero.Squares] = new()
                {
                    // English: Where was the {1} square in {0}?
                    // Example: Where was the red square in Zero, Zero?
                    Question = "Wo war bei {0} das {1} Quadrat?",
                    Arguments = new()
                    {
                        ["red"] = "rote",
                        ["green"] = "grüne",
                        ["blue"] = "blaue",
                    },
                },
                [SZeroZero.StarColors] = new()
                {
                    // English: What color was the {1} star in {0}?
                    // Example: What color was the top-left star in Zero, Zero?
                    Question = "Welche Farbe hatte bei {0} der {1} Stern?",
                    Arguments = new()
                    {
                        ["top-left"] = "obere linke",
                        ["top-right"] = "obere rechte",
                        ["bottom-left"] = "untere linke",
                        ["bottom-right"] = "untere rechte",
                    },
                    Answers = new()
                    {
                        ["black"] = "schwarze",
                        ["blue"] = "blaue",
                        ["green"] = "grüne",
                        ["cyan"] = "türkise",
                        ["red"] = "rote",
                        ["magenta"] = "magentafarbene",
                        ["yellow"] = "gelbe",
                        ["white"] = "weiße",
                    },
                },
                [SZeroZero.StarPoints] = new()
                {
                    // English: How many points were on the {1} star in {0}?
                    // Example: How many points were on the top-left star in Zero, Zero?
                    Question = "Wie viele Zacken hatte bei {0} der {1} Stern?",
                    Arguments = new()
                    {
                        ["top-left"] = "obere linke",
                        ["top-right"] = "obere rechte",
                        ["bottom-left"] = "untere linke",
                        ["bottom-right"] = "untere rechte",
                    },
                },
            },
        },

        // Zoni
        [typeof(SZoni)] = new()
        {
            Questions = new()
            {
                [SZoni.Words] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the first word in Zoni?
                    Question = "Was war bei {0} das {1}e Wort?",
                },
            },
        },

        // Épelle-moi Ça
        [typeof(SÉpelleMoiÇa)] = new()
        {
            Questions = new()
            {
                [SÉpelleMoiÇa.Word] = new()
                {
                    // English: What word was asked to be spelled in {0}?
                    Question = "Welches Wort sollte bei {0} buchstabiert werden?",
                },
            },
        },

        #endregion
    };

    public override string[] IntroTexts => Ut.NewArray(
        "Was geht ab, Alter?",
        "Ey, du kommst hier net rein.", // Kaya Yanar
        "Was guckst du?!",  // Kaya Yanar
        "Joa... also hallo erstmal...",  // Rüdiger Hoffmann
        "Meine Herren, das geht alles von Ihrer Zeit ab.",   // Piet Klocke
        "Die Bombe ist dem Entschärfer sein Tod.",   // „Der Dativ ist dem Genitiv sein Tod“, Bastian Sick
        "Was nicht explodiert, wird explodierend gemacht.", // „Was nicht passt, wird passend gemacht“ (Film, 2002)
        "Das merkwürdige Verhalten explosionsreifer Bomben zur Detonationszeit", // „Das merkwürdige Verhalten geschlechtsreifer Großstädter zur Paarungszeit“ (Film, 1998)
        "Die fetten Entschärfungen sind vorbei."   // „Die fetten Jahre sind vorbei“ (Film, 2004)
    );
}
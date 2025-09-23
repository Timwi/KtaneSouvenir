using System;
using System.Collections.Generic;

namespace Souvenir;

public class Translation_de : TranslationBase<Translation_de.TranslationInfo_de>
{
    public sealed class TranslationInfo_de : TranslationInfo
    {
        public Gender Gender = Gender.Neuter;
        public string ModuleNameDative;
        public string ModuleNameWithThe;
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
        : _translations.Get(handler.EnumType)?.ModuleNameWithThe ?? _translations.Get(handler.EnumType)?.ModuleName ?? handler.ModuleNameWithThe;

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

        [typeof(S1000Words)] = new()
        {
            ModuleName = "1000 Wörter",
            Gender = Gender.Plural,
            ModuleNameDative = "1000 Wörtern",
            Questions = new()
            {
                [S1000Words.Words] = new()
                {
                    // English: What was the {1} word shown in {0}?
                    // Example: What was the first word shown in 1000 Words?
                    Question = "Was war bei {0} das {1}e angezeigte Wort?",
                },
            },
        },

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
        },

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
        },

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

        [typeof(S3DMaze)] = new()
        {
            ModuleName = "3D-Labyrinth",
            Questions = new()
            {
                [S3DMaze.Markings] = new()
                {
                    // English: What were the markings in {0}?
                    Question = "Was waren bei {0} die Markierungen?",
                },
                [S3DMaze.Bearing] = new()
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
        },

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

        [typeof(S3DTunnels)] = new()
        {
            ModuleName = "3D-Tunnels",
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

        [typeof(S7)] = new()
        {
            Questions = new()
            {
                [S7.InitialValues] = new()
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
                [S7.LedColors] = new()
                {
                    // English: What LED color was shown in stage {1} of {0}?
                    // Example: What LED color was shown in stage 0 of 7?
                    Question = "Was war beim {1}en Schritt von {0} die LED-Farbe?",
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["blue"] = "blau",
                        ["green"] = "grün",
                        ["white"] = "weiß",
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

        [typeof(SAccumulation)] = new()
        {
            ModuleName = "Akkumulator",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SAccumulation.BorderColor] = new()
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
                [SAccumulation.BackgroundColor] = new()
                {
                    // English: What was the background color on the {1} stage in {0}?
                    // Example: What was the background color on the first stage in Accumulation?
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
        },

        [typeof(SAdventureGame)] = new()
        {
            Questions = new()
            {
                [SAdventureGame.CorrectItem] = new()
                {
                    // English: Which item was the {1} correct item you used in {0}?
                    // Example: Which item was the first correct item you used in Adventure Game?
                    Question = "Welches Objekt wurde bei {0} als {1}es korrekt verwendet?",
                },
                [SAdventureGame.Enemy] = new()
                {
                    // English: What enemy were you fighting in {0}?
                    Question = "Welcher Gegner wurde bei {0} bekämpft?",
                },
            },
        },

        [typeof(SAffineCycle)] = new()
        {
            ModuleName = "Affine Schiffer",
            Gender = Gender.Feminine,
            ModuleNameDative = "Affinen Schiffer",
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
        },

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

        [typeof(SALetter)] = new()
        {
            ModuleName = "A Buachstob",
            Questions = new()
            {
                [SALetter.InitialLetter] = new()
                {
                    // English: What was the initial letter in {0}?
                    Question = "Was war bei {0} der Anfangsbuchstabe?",
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

        [typeof(SAlgebra)] = new()
        {
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
        },

        [typeof(SAlgorithmia)] = new()
        {
            Questions = new()
            {
                [SAlgorithmia.Positions] = new()
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
                [SAlgorithmia.Color] = new()
                {
                    // English: What was the color of the colored bulb in {0}?
                    Question = "Welche Farbe hatte die gefärbte Glühlampe bei {0}?",
                },
                [SAlgorithmia.Seed] = new()
                {
                    // English: Which number was present in the seed in {0}?
                    Question = "Welche Zahl war bei {0} im Startwert enthalten?",
                },
            },
        },

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

        [typeof(SAlphabetTiles)] = new()
        {
            Questions = new()
            {
                [SAlphabetTiles.Cycle] = new()
                {
                    // English: What was the {1} letter shown during the cycle in {0}?
                    // Example: What was the first letter shown during the cycle in Alphabet Tiles?
                    Question = "Welcher Buchstabe war im Zyklus bei {0} als {1}es zu sehen?",
                },
                [SAlphabetTiles.MissingLetter] = new()
                {
                    // English: What was the missing letter in {0}?
                    Question = "Was war bei {0} der fehlende Buchstabe?",
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
                    Question = "Welches Zeichen wurde bei {0} im {1}en {2} Display angezeigt?",
                    Arguments = new()
                    {
                        ["left"] = "linken",
                        ["right"] = "rechten",
                    },
                },
            },
        },

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

        [typeof(SArena)] = new()
        {
            Gender = Gender.Feminine,
            ModuleNameWithThe = "Die Arena",
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

        [typeof(SASquare)] = new()
        {
            ModuleName = "Ein Quadrat",
            Questions = new()
            {
                [SASquare.IndexColors] = new()
                {
                    // English: Which of these was an index color in {0}?
                    Question = "Welche Indexfarbe kam bei {0} vor?",
                },
                [SASquare.CorrectColors] = new()
                {
                    // English: Which color was submitted {1} in {0}?
                    // Example: Which color was submitted first in A Square?
                    Question = "Welche Farbe wurde bei {0} als {1}es eingegeben?",
                },
            },
        },

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

        [typeof(SAzureButton)] = new()
        {
            ModuleName = "Azurfarbenen Knopf",
            ModuleNameWithThe = "Der Azurfarbene Knopf",
            Questions = new()
            {
                [SAzureButton.QDecoyArrowDirection] = new()
                {
                    // English: What was the {1} direction in the decoy arrow in {0}?
                    // Example: What was the first direction in the decoy arrow in Azure Button?
                    Question = "Was war bei {0} die {1}e Richtung im ungenutzten Pfeil?",
                },
                [SAzureButton.QNonDecoyArrowDirection] = new()
                {
                    // English: What was the {1} direction in the {2} non-decoy arrow in {0}?
                    // Example: What was the first direction in the first non-decoy arrow in Azure Button?
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

        [typeof(SBamboozledAgain)] = new()
        {
            ModuleName = "Wieder Übers Ohr Gehauen",
            Questions = new()
            {
                [SBamboozledAgain.ButtonText] = new()
                {
                    // English: What was the text on the {1} correct button in {0}?
                    // Example: What was the text on the first correct button in Bamboozled Again?
                    Question = "Was war bei {0} die Aufschrift des {1}en korrekten Knopfes?",
                },
                [SBamboozledAgain.ButtonColor] = new()
                {
                    // English: What color was the {1} correct button in {0}?
                    // Example: What color was the first correct button in Bamboozled Again?
                    Question = "Welche Farbe hatte der {0}e korrekte Knopf bei {0}?",
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

        [typeof(SBamboozlingButton)] = new()
        {
            NeedsTranslation = true,
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

        [typeof(SBarCharts)] = new()
        {
            ModuleName = "Balkendiagramme",
            Gender = Gender.Plural,
            ModuleNameDative = "Balkendiagrammen",
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

        [typeof(SBigCircle)] = new()
        {
            NeedsTranslation = true,
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
                        ["top left"] = "oberen linken",
                        ["top right"] = "oberen rechten",
                        ["bottom left"] = "unteren linken",
                        ["bottom right"] = "unteren rechten",
                        ["black"] = "schwarz",
                    },
                },
            },
        },

        [typeof(SBlackCipher)] = new()
        {
            ModuleName = "Schwarze Geheimschrift",
            Gender = Gender.Feminine,
            ModuleNameDative = "Schwarzen Geheimschrift",
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

        [typeof(SBlindfoldedYahtzee)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Blindes Yahtzee",
            ModuleNameDative = "Blinden Yahtzee",
            Questions = new()
            {
                [SBlindfoldedYahtzee.Claim] = new()
                {
                    // English: What roll did the module claim in the {1} stage of {0}?
                    // Example: What roll did the module claim in the first stage of Blindfolded Yahtzee?
                    Question = "Was hat das Modul bei {0} in der {1}en Stufe verzeichnet?",
                    Answers = new()
                    {
                        ["Yahtzee"] = "Yahtzee",
                        ["Large Straight"] = "Große Straße",
                        ["Small Straight"] = "Kleine Straße",
                        ["Full House"] = "Full House",
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
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Gray"] = "Grau",
                        ["Yellow"] = "Gelb",
                    },
                    Arguments = new()
                    {
                        ["north"] = "Norden",
                        ["east"] = "Osten",
                        ["west"] = "Westen",
                        ["south"] = "Süden",
                    },
                },
                [SBlindMaze.Maze] = new()
                {
                    // English: Which maze did you solve {0} on?
                    Question = "In welchem Labyrinth wurde {0} gelöst?",
                },
            },
        },

        [typeof(SBlinkingNotes)] = new()
        {
            ModuleName = "Blinkende Noten",
            Gender = Gender.Plural,
            ModuleNameDative = "Blinkenden Noten",
            Questions = new()
            {
                [SBlinkingNotes.Song] = new()
                {
                    // English: What song was flashed in {0}?
                    Question = "Welcher Song blinkte bei {0} auf?",
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

        [typeof(SBlueButton)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Blauen Knopf",
            ModuleNameWithThe = "Der Blaue Knopf",
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
                    // Example: What was E in Blue Button?
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
                    Question = "Was war bei {0} X?",
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
                    Discriminator = "dem Blauen Knopf, bei dem {0} {1} war,",
                },
            },
        },

        [typeof(SBlueCipher)] = new()
        {
            ModuleName = "Blaue Geheimschrift",
            Gender = Gender.Feminine,
            ModuleNameDative = "Blauen Geheimschrift",
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

        [typeof(SBorderedKeys)] = new()
        {
            NeedsTranslation = true,
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

        [typeof(SBreakfastEgg)] = new()
        {
            NeedsTranslation = true,
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

        [typeof(SBrokenButtons)] = new()
        {
            ModuleName = "Kaputte Knöpfe",
            Gender = Gender.Plural,
            ModuleNameDative = "Kaputten Knöpfen",
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

        [typeof(SBrokenGuitarChords)] = new()
        {
            ModuleName = "Kaputte Gitarrenakkorde",
            Gender = Gender.Plural,
            ModuleNameDative = "Kaputten Gitarrenakkorden",
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

        [typeof(SBrownCipher)] = new()
        {
            ModuleName = "Braune Geheimschrift",
            Gender = Gender.Feminine,
            ModuleNameDative = "Braunen Geheimschrift",
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

        [typeof(SBrushStrokes)] = new()
        {
            NeedsTranslation = true,
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

        [typeof(SBulb)] = new()
        {
            ModuleName = "Glühlampe",
            Gender = Gender.Feminine,
            ModuleNameWithThe = "Die Glühlampe",
            Questions = new()
            {
                [SBulb.InitialState] = new()
                {
                    // English: Was the bulb initially lit in {0}?
                    Question = "War die Glühlampe bei {0} anfänglich an?",
                },
            },
        },

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

        [typeof(SButton)] = new()
        {
            ModuleName = "Knopf",
            Gender = Gender.Masculine,
            ModuleNameWithThe = "Der Knopf",
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

        [typeof(SCactisConundrum)] = new()
        {
            NeedsTranslation = true,
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
        },

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
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                    },
                    Arguments = new()
                    {
                        ["up"] = "Hoch-Taste",
                        ["right"] = "Rechts-Taste",
                        ["down"] = "Runter-Taste",
                        ["left"] = "Links-Taste",
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

        [typeof(SCatchphrase)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SCatchphrase.Colour] = new()
                {
                    // English: What was the colour of the {1} panel in {0}?
                    // Example: What was the colour of the top-left panel in Catchphrase?
                    Question = "Welche Farbe hatte bei {0} die {1} Tafel?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Orange"] = "Orange",
                        ["Purple"] = "Lila",
                        ["Yellow"] = "Gelb",
                    },
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

        [typeof(SCheepCheckout)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Zwitscherkasse",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SCheepCheckout.Birds] = new()
                {
                    // English: Which bird {1} present in {0}?
                    // Example: Which bird was present in Cheep Checkout?
                    Question = "Welcher Vogel war bei {0} {1}?",
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
                    Arguments = new()
                    {
                        ["was"] = "zu sehen",
                        ["was not"] = "nicht zu sehen",
                    },
                },
            },
        },

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

        [typeof(SChineseCounting)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Chinesisch Zählen",
            Questions = new()
            {
                [SChineseCounting.LED] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the left LED in Chinese Counting?
                    Question = "Welche Farbe hatte bei {0} die {1} LED?",
                    Answers = new()
                    {
                        ["White"] = "Weiß",
                        ["Red"] = "Rot",
                        ["Green"] = "Grün",
                        ["Orange"] = "Orange",
                    },
                    Arguments = new()
                    {
                        ["left"] = "linke",
                        ["right"] = "rechte",
                    },
                },
            },
        },

        [typeof(SChineseRemainderTheorem)] = new()
        {
            ModuleName = "Chinesischer Restsatz",
            Gender = Gender.Masculine,
            ModuleNameDative = "Chinesischen Restsatz",
            Questions = new()
            {
                [SChineseRemainderTheorem.Equations] = new()
                {
                    // English: Which equation was used in {0}?
                    Question = "Welche Gleichung wurde bei {0} verwendet?",
                },
            },
        },

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

        [typeof(SCode)] = new()
        {
            Gender = Gender.Masculine,
            ModuleNameWithThe = "Der Code",
            Questions = new()
            {
                [SCode.DisplayNumber] = new()
                {
                    // English: What was the displayed number in {0}?
                    Question = "Welche Zahl wurde bei {0} angezeigt?",
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
                    Question = "Welches Wort war bei {0} ein Lösungswort?",
                },
            },
        },

        [typeof(SCoffeeBeans)] = new()
        {
            NeedsTranslation = true,
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

        [typeof(SColorBraille)] = new()
        {
            NeedsTranslation = true,
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

        [typeof(SColorDecoding)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Farbdekodierung",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SColorDecoding.IndicatorColors] = new()
                {
                    // English: Which color {1} in the {2}-stage indicator pattern in {0}?
                    // Example: Which color appeared in the first-stage indicator pattern in Color Decoding?
                    Question = "Welche Farbe kam bei {0} im Indikatormuster der {2}en Stufe {1}?",
                    Answers = new()
                    {
                        ["Green"] = "Grün",
                        ["Purple"] = "Lila",
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Yellow"] = "Gelb",
                    },
                    Arguments = new()
                    {
                        ["appeared"] = "vor",
                        ["did not appear"] = "nicht vor",
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

        [typeof(SColoredKeys)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Gefärbte Tasten",
            Gender = Gender.Plural,
            ModuleNameDative = "Gefärbten Tasten",
            Questions = new()
            {
                [SColoredKeys.DisplayWord] = new()
                {
                    // English: What was the displayed word in {0}?
                    Question = "Was war bei {0} das Wort auf dem Display?",
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["yellow"] = "yellow",
                        ["purple"] = "purple",
                        ["white"] = "white",
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
                    Answers = new()
                    {
                        ["red"] = "rot",
                        ["blue"] = "blau",
                        ["green"] = "grün",
                        ["yellow"] = "gelb",
                        ["purple"] = "lila",
                        ["white"] = "weiß",
                    },
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

        [typeof(SColoredSquares)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Gefärbte Felder",
            Gender = Gender.Plural,
            ModuleNameDative = "Gefärbten Feldern",
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

        [typeof(SColoredSwitches)] = new()
        {
            ModuleName = "Gefärbte Schalter",
            Gender = Gender.Plural,
            ModuleNameDative = "Gefärbten Schaltern",
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

        [typeof(SColorMorse)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Gefärbte Morsezeichen",
            Gender = Gender.Plural,
            ModuleNameDative = "Gefärbten Morsezeichen",
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

        [typeof(SColorOneTwo)] = new()
        {
            Questions = new()
            {
                [SColorOneTwo.Color] = new()
                {
                    // English: What color was the {1} LED in {0}?
                    // Example: What color was the left LED in Color One Two?
                    Question = "Welche Farbe hatte bei {0} die {1} LED?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Yellow"] = "Gelb",
                    },
                    Arguments = new()
                    {
                        ["left"] = "linke",
                        ["right"] = "rechte",
                    },
                },
            },
        },

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

        [typeof(SColouredCubes)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Farbwürfel",
            Gender = Gender.Plural,
            ModuleNameDative = "Farbwürfeln",
            Questions = new()
            {
                [SColouredCubes.Colours] = new()
                {
                    // English: What was the colour of this {1} in the {2} stage of {0}? (+ sprite)
                    // Example: What was the colour of this cube in the first stage of Coloured Cubes? (+ sprite)
                    Question = "Welche Farbe hatte {1} bei {0} in der {2}en Stufe?",
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
                    Arguments = new()
                    {
                        ["cube"] = "dieser Würfel",
                        ["stage light"] = "dieser Stufenindikator",
                    },
                },
            },
        },

        [typeof(SColouredCylinder)] = new()
        {
            ModuleName = "Gefärbter Zylinder",
            Gender = Gender.Masculine,
            ModuleNameDative = "Gefärbten Zylinder",
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

        [typeof(SColourFlash)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Farbfolge",
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

        [typeof(SConditionalButtons)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Bedingte Knöpfe",
            Gender = Gender.Plural,
            ModuleNameDative = "Bedingten Knöpfen",
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

        [typeof(SConnectedMonitors)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Verbundene Monitore",
            Gender = Gender.Plural,
            ModuleNameDative = "Verbundenen Monitoren",
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

        [typeof(SCorners)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Ecken",
            Questions = new()
            {
                [SCorners.Colors] = new()
                {
                    // English: What was the color of the {1} corner in {0}?
                    // Example: What was the color of the top-left corner in Corners?
                    Question = "What was the color of the {1} corner in {0}?",
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["yellow"] = "yellow",
                    },
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

        [typeof(SCrazyHamburger)] = new()
        {
            ModuleName = "Verrückter Hamburger",
            Gender = Gender.Masculine,
            ModuleNameDative = "Verrückten Hamburger",
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

        [typeof(SCrazyMaze)] = new()
        {
            ModuleName = "Verrückter Irrgarten",
            Gender = Gender.Masculine,
            ModuleNameDative = "Verrückten Irrgarten",
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

        [typeof(SCritters)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Kriechtiere",
            Gender = Gender.Plural,
            ModuleNameDative = "Kriechtieren",
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

        [typeof(SCruelKeypads)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Höllische Tastenfelder",
            Gender = Gender.Plural,
            ModuleNameDative = "Höllischen Tastenfelder",
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

        [typeof(SCRule)] = new()
        {
            ModuleName = "CRegel",
            Gender = Gender.Feminine,
            ModuleNameWithThe = "Die CRegel",
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
                    // Example: Where was ♤♤ in cRule?
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

        [typeof(SCrypticCycle)] = new()
        {
            ModuleName = "Kryptische Schiffer",
            Gender = Gender.Feminine,
            ModuleNameDative = "Kryptischen Schiffer",
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
        },

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
                    Answers = new()
                    {
                        ["North"] = "Norden",
                        ["East"] = "Osten",
                        ["South"] = "Süden",
                        ["West"] = "Westen",
                    },
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

        [typeof(SCube)] = new()
        {
            ModuleName = "Würfel",
            Gender = Gender.Masculine,
            ModuleNameWithThe = "Der Würfel",
            Questions = new()
            {
                [SCube.Rotations] = new()
                {
                    // English: What was the {1} cube rotation in {0}?
                    // Example: What was the first cube rotation in Cube?
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

        [typeof(SCursedDoubleOh)] = new()
        {
            ModuleName = "Verfluchte Doppel-Null",
            Gender = Gender.Feminine,
            ModuleNameDative = "Verflüchten Doppel-Null",
            Questions = new()
            {
                [SCursedDoubleOh.InitialPosition] = new()
                {
                    // English: What was the first digit of the initially displayed number in {0}?
                    Question = "Was war bei {0} am Anfang die erste Ziffer auf dem Display?",
                },
            },
        },

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

        [typeof(SCyanButton)] = new()
        {
            ModuleName = "Türkiser Knopf",
            Gender = Gender.Masculine,
            ModuleNameDative = "Türkisen Knopf",
            ModuleNameWithThe = "Der Türkise Knopf",
            Questions = new()
            {
                [SCyanButton.Positions] = new()
                {
                    // English: Where was the button at the {1} stage in {0}?
                    // Example: Where was the button at the first stage in Cyan Button?
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

        [typeof(SDACHMaze)] = new()
        {
            NeedsTranslation = true,
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

        [typeof(SDeafAlley)] = new()
        {
            ModuleName = "Taube Gasse",
            Gender = Gender.Feminine,
            ModuleNameDative = "Tauben Gasse",
            Questions = new()
            {
                [SDeafAlley.Shape] = new()
                {
                    // English: What was the shape generated in {0}?
                    Question = "Was war bei {0} die generierte Form?",
                },
            },
        },

        [typeof(SDeckOfManyThings)] = new()
        {
            ModuleName = "Stapel Vieler Dinge",
            Gender = Gender.Masculine,
            ModuleNameWithThe = "Der Stapel Vieler Dinge",
            Questions = new()
            {
                [SDeckOfManyThings.FirstCard] = new()
                {
                    // English: What deck did the first card of {0} belong to?
                    Question = "Zu welchem Deck gehörte bei {0} die erste Karte?",
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
                    Question = "Welche Farbe hat bei {0} die {1} bestimmt?",
                    Answers = new()
                    {
                        ["White"] = "Weiß",
                        ["Red"] = "Rot",
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Yellow"] = "Gelb",
                        ["Magenta"] = "Magenta",
                    },
                    Arguments = new()
                    {
                        ["column"] = "Spalte",
                        ["row"] = "Reihe",
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
                    Question = "Was war bei {0} {1} vom {2}en Ziel?",
                    Answers = new()
                    {
                        ["Blue"] = "Blau",
                        ["Green"] = "Grün",
                        ["Red"] = "Rot",
                        ["Magenta"] = "Magenta",
                        ["Yellow"] = "Gelb",
                        ["White"] = "Weiß",
                    },
                    Arguments = new()
                    {
                        ["colour"] = "die Farbe",
                        ["word"] = "das Wort",
                    },
                },
            },
        },

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

        [typeof(SDevilishEggs)] = new()
        {
            ModuleName = "Teufelseier",
            Gender = Gender.Plural,
            ModuleNameDative = "Teufelseiern",
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

        [typeof(SDiscoloredSquares)] = new()
        {
            NeedsTranslation = true,
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

        [typeof(SDisorderedKeys)] = new()
        {
            NeedsTranslation = true,
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

        [typeof(SDividedSquares)] = new()
        {
            ModuleName = "Geteilte Kacheln",
            Gender = Gender.Plural,
            ModuleNameDative = "Geteilten Kacheln",
            Questions = new()
            {
                [SDividedSquares.Color] = new()
                {
                    // English: What color was {1} while pressing it in {0}?
                    // Example: What color was the square while pressing it in Divided Squares?
                    Question = "Welche Farbe hatte bei {0} die {1} beim Gedrückthalten?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Black"] = "Schwarz",
                        ["White"] = "Weiß",
                    },
                    Arguments = new()
                    {
                        ["the square"] = "Kachel",
                        ["the correct square"] = "korrekte Kachel",
                    },
                },
            },
        },

        [typeof(SDivisibleNumbers)] = new()
        {
            ModuleName = "Teilbare Zahlen",
            Gender = Gender.Plural,
            ModuleNameDative = "Teilbaren Zahlen",
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

        [typeof(SDoubleArrows)] = new()
        {
            ModuleName = "Doppelpfeile",
            Gender = Gender.Plural,
            ModuleNameDative = "Doppelpfeilen",
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
                    Answers = new()
                    {
                        ["Up"] = "Hoch",
                        ["Right"] = "Rechts",
                        ["Left"] = "Links",
                        ["Down"] = "Runter",
                    },
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
                },
                [SDoubleArrows.Arrow] = new()
                {
                    // English: Which {1} arrow moved {2} in the grid in {0}?
                    // Example: Which inner arrow moved up in the grid in Double Arrows?
                    Question = "Welcher {1} Pfeil ging bei {0} nach {2}?",
                    Answers = new()
                    {
                        ["Up"] = "Hoch",
                        ["Right"] = "Rechts",
                        ["Left"] = "Links",
                        ["Down"] = "Runter",
                    },
                    Arguments = new()
                    {
                        ["inner"] = "innere",
                        ["up"] = "oben",
                        ["outer"] = "äußere",
                        ["down"] = "unten",
                        ["left"] = "links",
                        ["right"] = "rechts",
                    },
                },
            },
        },

        [typeof(SDoubleColor)] = new()
        {
            NeedsTranslation = true,
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

        [typeof(SDoubleExpert)] = new()
        {
            ModuleName = "Doppelexperte",
            Gender = Gender.Masculine,
            ModuleNameDative = "Doppelexperten",
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
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                    },
                    Arguments = new()
                    {
                        ["top"] = "oberen",
                        ["bottom"] = "unteren",
                    },
                },
            },
        },

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

        [typeof(SDuck)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Ente",
            Gender = Gender.Feminine,
            ModuleNameWithThe = "Die Ente",
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

        [typeof(SEncryptedDice)] = new()
        {
            ModuleName = "Verschlüsselte Würfel",
            Gender = Gender.Plural,
            ModuleNameDative = "Verschlüsselten Würfel",
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

        [typeof(SEncryptedEquations)] = new()
        {
            ModuleName = "Verschlüsselte Gleichungen",
            Gender = Gender.Plural,
            ModuleNameDative = "Verschlüsselten Gleichungen",
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
            ModuleName = "Enigma-Schiffer",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SEnigmaCycle.DialDirectionsThree] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Enigma Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}te Zeiger?",
                },
                [SEnigmaCycle.DialDirectionsTwelve] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Enigma Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}te Zeiger?",
                },
                [SEnigmaCycle.DialDirectionsEight] = new()
                {
                    // English: Which direction was the {1} dial pointing in {0}?
                    // Example: Which direction was the first dial pointing in Enigma Cycle?
                    Question = "In welche Richtung zeigte bei {0} der {1}te Zeiger?",
                },
                [SEnigmaCycle.DialLabels] = new()
                {
                    // English: What letter was written on the {1} dial in {0}?
                    // Example: What letter was written on the first dial in Enigma Cycle?
                    Question = "Welcher Buchstabe stand bei {0} auf dem {1}en Zeiger?",
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
                    Answers = new()
                    {
                        ["outer"] = "outer",
                        ["middle"] = "middle",
                        ["inner"] = "inner",
                        ["none"] = "none",
                    },
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
                    Answers = new()
                    {
                        ["outer"] = "outer",
                        ["middle"] = "middle",
                        ["inner"] = "inner",
                        ["none"] = "none",
                    },
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Purple"] = "Purple",
                        ["White"] = "White",
                    },
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

        [typeof(SForgetAnyColor)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Vergiss Jede Farbe",
            Questions = new()
            {
                [SForgetAnyColor.QCylinder] = new()
                {
                    // English: What colors were the cylinders during the {1} stage of {0}?
                    // Example: What colors were the cylinders during the first stage of Forget Any Color?
                    Question = "Was waren bei {0} die Zylinderfarben in der {1}en Stufe?",
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
                    Question = "Which figure was used during the {1} stage of {0}?",
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
            ModuleName = "Vergissmeinnicht",
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
                    Discriminator = "dem Vergissmeinnicht, in dessen {1}er Stufe {0} angezeigt wurde,",
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
                    Answers = new()
                    {
                        ["Forget Me Not"] = "Forget Me Not",
                        ["Simon’s Stages"] = "Simon’s Stages",
                        ["Forget Me Later"] = "Forget Me Later",
                        ["Forget Infinity"] = "Forget Infinity",
                        ["A>N<D"] = "A>N<D",
                        ["Forget Me Now"] = "Forget Me Now",
                        ["Forget Everything"] = "Forget Everything",
                        ["Forget Us Not"] = "Forget Us Not",
                    },
                },
            },
        },

        [typeof(SForgetTheColors)] = new()
        {
            NeedsTranslation = true,
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
                    Discriminator = "dem Vergiss die Farben, dessen Zahnradzahl in Stufe {1} {0} war,",
                },
                [SForgetTheColors.DLargeDisplay] = new()
                {
                    // English: the Forget The Colors which had {0} on its large display in stage {1}
                    // Example: the Forget The Colors which had 426 on its large display in stage 1
                    Discriminator = "dem Vergiss die Farben, dessen großes Display in Stufe {1} {0} anzeigte,",
                },
                [SForgetTheColors.DSineNumber] = new()
                {
                    // English: the Forget The Colors whose received sine number in stage {1} ended with a {0}
                    // Example: the Forget The Colors whose received sine number in stage 1 ended with a 0
                    Discriminator = "dem Vergiss die Farben, dessen erhaltene Sinuszahl in Stufe {1} auf {0} endete,",
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
                    Discriminator = "dem Vergiss Dies, das in der {1}en Stufe {0} anzeigte,",
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
                    Question = "What was the player token in {0}?",
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
                    Question = "Which key made up the fifth set in {0}?",
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
            ModuleName = "Graue Geheimschrift",
            Gender = Gender.Feminine,
            ModuleNameDative = "Grauen Geheimschrift",
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
            ModuleName = "Grüne Geheimschrift",
            Gender = Gender.Feminine,
            ModuleNameDative = "Grünen Geheimschrift",
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

        [typeof(SGridLock)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SGridLock.StartingColor] = new()
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                    },
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
                    Answers = new()
                    {
                        ["Strawberries"] = "Strawberries",
                        ["Melons"] = "Melons",
                        ["Lemons"] = "Lemons",
                        ["Raspberries"] = "Raspberries",
                        ["Bananas"] = "Bananas",
                    },
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Cyan"] = "Cyan",
                        ["Blue"] = "Blue",
                        ["Pink"] = "Pink",
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
        },

        [typeof(SHinges)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Scharniere",
            Gender = Gender.Plural,
            ModuleNameDative = "Scharnieren",
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
                    Arguments = new()
                    {
                        ["column"] = "Spaltensymbol",
                        ["row"] = "Reihensymbol",
                    },
                },
            },
        },

        [typeof(SHypercube)] = new()
        {
            Questions = new()
            {
                [SHypercube.Rotations] = new()
                {
                    // English: What was the {1} rotation in {0}?
                    // Example: What was the first rotation in Hypercube?
                    Question = "Was war die {1}e Rotation in {0}?",
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

        [typeof(SHyperlink)] = new()
        {
            Gender = Gender.Masculine,
            ModuleNameWithThe = "Der Hyperlink",
            Questions = new()
            {
                [SHyperlink.Characters] = new()
                {
                    // English: What was the {1} character of the hyperlink in {0}?
                    // Example: What was the first character of the hyperlink in Hyperlink?
                    Question = "Was war bei {0} das erste Zeichen im Hyperlink?",
                },
                [SHyperlink.Answer] = new()
                {
                    // English: Which module was referenced on {0}?
                    Question = "Auf welches Modul wurde bei {0} verwiesen?",
                },
            },
        },

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
        },

        [typeof(SJuxtacoloredSquares)] = new()
        {
            NeedsTranslation = true,
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
                    // English: What was this key’s label on the {1} panel in {0}? (+ sprite)
                    // Example: What was this key’s label on the first panel in Keypad Sequence? (+ sprite)
                    Question = "What was the label on this button on the {1} panel in {0}?",
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
            ModuleNameWithThe = "Das Klaxon",
            Questions = new()
            {
                [SKlaxon.FirstModule] = new()
                {
                    // English: What was the first module to set off {0}?
                    Question = "Welches Modul hat als erstes {0} ausgelöst?",
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
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SKugelblitz.BlackOrangeYellowIndigoViolet] = new()
                {
                    // English: Which particles were present for the {1} stage of {0}?
                    // Example: Which particles were present for the first stage of Kugelblitz?
                    Question = "Welche Partikel waren bei {0} in der {1}en Stufe zu sehen?",
                },
                [SKugelblitz.RedGreenBlue] = new()
                {
                    // English: What were the particles’ values for the {1} stage of {0}?
                    // Example: What were the particles’ values for the first stage of Kugelblitz?
                    Question = "Was waren bei {0} die Partikelwerte in der {1}en Stufe?",
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
                        ["black"] = "black",
                        ["red"] = "red",
                        ["orange"] = "orange",
                        ["yellow"] = "yellow",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["indigo"] = "indigofarbenen",
                        ["violet"] = "violetten",
                    },
                },
                [SKugelblitz.NoLinks] = new()
                {
                    // English: the Kugelblitz linked with no other Kugelblitzes
                    Discriminator = "dem Kugelblitz, der mit keinem anderen Kugelblitzen gekoppelt ist,",
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                    },
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
                        ["left"] = "left",
                        ["right"] = "right",
                    },
                },
            },
        },

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
            Questions = new()
            {
                [SMetapuzzle.Answer] = new()
                {
                    // English: What was the final answer in {0}?
                    Question = "Was war bei {0} die abschließende Antwort?",
                },
            },
        },

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
            ModuleName = "Modulmanöver",
            Gender = Gender.Plural,
            ModuleNameDative = "Modulmanövern",
            Questions = new()
            {
                [SModuleManeuvers.Goal] = new()
                {
                    // English: What was the goal location in {0}?
                    Question = "Was war bei {0} die Zielposition?",
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

        [typeof(SMorseWoF)] = new()
        {
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

        [typeof(SMouseInTheMaze)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SMouseInTheMaze.Sphere] = new()
                {
                    // English: Which color sphere was the goal in {0}?
                    Question = "Which color sphere was the goal in {0}?",
                    Answers = new()
                    {
                        ["white"] = "white",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["yellow"] = "yellow",
                    },
                },
                [SMouseInTheMaze.Torus] = new()
                {
                    // English: What color was the torus in {0}?
                    Question = "What color was the torus in {0}?",
                    Answers = new()
                    {
                        ["white"] = "white",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["yellow"] = "yellow",
                    },
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
            ModuleName = "\uE001Fhl Ndv Kl\uE002",
            Gender = Gender.Plural,
            ModuleNameDative = "\uE001Fh Lnd Nvkln\uE002",
            Questions = new()
            {
                [SMssngvWls.MssNgvwL] = new()
                {
                    // English: Which vowel was missing in {0}?
                    // Example: Which vowel was missing in Mssngv Wls?
                    Question = "Welcher Vokal hat bei {0} gefehlt?",
                    Arguments = new()
                    {
                        ["AEIOU"] = "AEIOUÄÖÜ",
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
                        ["a suspect but not the murderer"] = "a suspect but not the murderer",
                        ["not a suspect"] = "not a suspect",
                    },
                },
                [SMurder.Weapon] = new()
                {
                    // English: Which of these was {1} in {0}?
                    // Example: Which of these was a potential weapon but not the murder weapon in Murder?
                    Question = "Which of these was {1} in {0}?",
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
                        ["a potential weapon but not the murder weapon"] = "a potential weapon but not the murder weapon",
                        ["not a potential weapon"] = "not a potential weapon",
                    },
                },
                [SMurder.BodyFound] = new()
                {
                    // English: Where was the body found in {0}?
                    Question = "Where was the body found in {0}?",
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

        [typeof(SNavigationDetermination)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SNavigationDetermination.Color] = new()
                {
                    // English: What was the color of the maze in {0}?
                    Question = "What was the color of the maze in {0}?",
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                    },
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
                    Answers = new()
                    {
                        ["up"] = "up",
                        ["left"] = "left",
                        ["right"] = "right",
                        ["down"] = "down",
                    },
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
            ModuleName = "Der Königsblaue Knopf",
            Gender = Gender.Masculine,
            ModuleNameDative = "Königsblauen Knopf",
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
                    // Example: What was the (0-indexed) column of the given in Navy Button?
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

        [typeof(SNecronomicon)] = new()
        {
            ModuleName = "Der Königsblaue Knopf",
            Gender = Gender.Masculine,
            ModuleNameDative = "Königsblauen Knopf",
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
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["orange"] = "orange",
                        ["yellow"] = "yellow",
                        ["green"] = "green",
                        ["cyan"] = "cyan",
                        ["blue"] = "blue",
                        ["purple"] = "purple",
                        ["magenta"] = "magenta",
                        ["pink"] = "pink",
                        ["brown"] = "brown",
                        ["grey"] = "grey",
                        ["white"] = "white",
                    },
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
            ModuleName = "Mord Mal Anders",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SNotMurder.Room] = new()
                {
                    // English: What room was {1} in initially on {0}?
                    // Example: What room was Miss Scarlett in initially on Not Murder?
                    Question = "In welchem Zimmer war {1} bei {0} am Anfang?",
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
                    Question = "Welche Waffe hatte {1} bei {0} am Anfang?",
                    Answers = new()
                    {
                        ["Candlestick"] = "Kerzenleuchter",
                        ["Dagger"] = "Dolch",
                        ["Lead Pipe"] = "Bleirohr",
                        ["Revolver"] = "Pistole",
                        ["Rope"] = "Seil",
                        ["Spanner"] = "Rohrzange",
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
                    Answers = new()
                    {
                        ["top"] = "top",
                        ["top-right"] = "top-right",
                        ["bottom-right"] = "bottom-right",
                        ["bottom-left"] = "bottom-left",
                        ["top-left"] = "top-left",
                    },
                },
                [SNotPerspectivePegs.Perspective] = new()
                {
                    // English: From what perspective did the {1} peg flash on {0}?
                    // Example: From what perspective did the first peg flash on Not Perspective Pegs?
                    Question = "From what perspective did the {1} peg flash on {0}?",
                    Answers = new()
                    {
                        ["top"] = "top",
                        ["top-right"] = "top-right",
                        ["bottom-right"] = "bottom-right",
                        ["bottom-left"] = "bottom-left",
                        ["top-left"] = "top-left",
                    },
                },
                [SNotPerspectivePegs.Color] = new()
                {
                    // English: What was the color of the {1} flashing peg on {0}?
                    // Example: What was the color of the first flashing peg on Not Perspective Pegs?
                    Question = "What was the color of the {1} flashing peg on {0}?",
                    Answers = new()
                    {
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["purple"] = "purple",
                        ["red"] = "red",
                        ["yellow"] = "yellow",
                    },
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
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["orange"] = "orange",
                        ["yellow"] = "yellow",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["purple"] = "purple",
                    },
                },
                [SNotSimaze.Start] = new()
                {
                    // English: What was the starting position in {0}?
                    Question = "What was the starting position in {0}?",
                    Answers = new()
                    {
                        ["(red, red)"] = "(red, red)",
                        ["(red, orange)"] = "(red, orange)",
                        ["(red, yellow)"] = "(red, yellow)",
                        ["(red, green)"] = "(red, green)",
                        ["(red, blue)"] = "(red, blue)",
                        ["(red, purple)"] = "(red, purple)",
                        ["(orange, red)"] = "(orange, red)",
                        ["(orange, orange)"] = "(orange, orange)",
                        ["(orange, yellow)"] = "(orange, yellow)",
                        ["(orange, green)"] = "(orange, green)",
                        ["(orange, blue)"] = "(orange, blue)",
                        ["(orange, purple)"] = "(orange, purple)",
                        ["(yellow, red)"] = "(yellow, red)",
                        ["(yellow, orange)"] = "(yellow, orange)",
                        ["(yellow, yellow)"] = "(yellow, yellow)",
                        ["(yellow, green)"] = "(yellow, green)",
                        ["(yellow, blue)"] = "(yellow, blue)",
                        ["(yellow, purple)"] = "(yellow, purple)",
                        ["(green, red)"] = "(green, red)",
                        ["(green, orange)"] = "(green, orange)",
                        ["(green, yellow)"] = "(green, yellow)",
                        ["(green, green)"] = "(green, green)",
                        ["(green, blue)"] = "(green, blue)",
                        ["(green, purple)"] = "(green, purple)",
                        ["(blue, red)"] = "(blue, red)",
                        ["(blue, orange)"] = "(blue, orange)",
                        ["(blue, yellow)"] = "(blue, yellow)",
                        ["(blue, green)"] = "(blue, green)",
                        ["(blue, blue)"] = "(blue, blue)",
                        ["(blue, purple)"] = "(blue, purple)",
                        ["(purple, red)"] = "(purple, red)",
                        ["(purple, orange)"] = "(purple, orange)",
                        ["(purple, yellow)"] = "(purple, yellow)",
                        ["(purple, green)"] = "(purple, green)",
                        ["(purple, blue)"] = "(purple, blue)",
                        ["(purple, purple)"] = "(purple, purple)",
                    },
                },
                [SNotSimaze.Goal] = new()
                {
                    // English: What was the goal position in {0}?
                    Question = "What was the goal position in {0}?",
                    Answers = new()
                    {
                        ["(red, red)"] = "(red, red)",
                        ["(red, orange)"] = "(red, orange)",
                        ["(red, yellow)"] = "(red, yellow)",
                        ["(red, green)"] = "(red, green)",
                        ["(red, blue)"] = "(red, blue)",
                        ["(red, purple)"] = "(red, purple)",
                        ["(orange, red)"] = "(orange, red)",
                        ["(orange, orange)"] = "(orange, orange)",
                        ["(orange, yellow)"] = "(orange, yellow)",
                        ["(orange, green)"] = "(orange, green)",
                        ["(orange, blue)"] = "(orange, blue)",
                        ["(orange, purple)"] = "(orange, purple)",
                        ["(yellow, red)"] = "(yellow, red)",
                        ["(yellow, orange)"] = "(yellow, orange)",
                        ["(yellow, yellow)"] = "(yellow, yellow)",
                        ["(yellow, green)"] = "(yellow, green)",
                        ["(yellow, blue)"] = "(yellow, blue)",
                        ["(yellow, purple)"] = "(yellow, purple)",
                        ["(green, red)"] = "(green, red)",
                        ["(green, orange)"] = "(green, orange)",
                        ["(green, yellow)"] = "(green, yellow)",
                        ["(green, green)"] = "(green, green)",
                        ["(green, blue)"] = "(green, blue)",
                        ["(green, purple)"] = "(green, purple)",
                        ["(blue, red)"] = "(blue, red)",
                        ["(blue, orange)"] = "(blue, orange)",
                        ["(blue, yellow)"] = "(blue, yellow)",
                        ["(blue, green)"] = "(blue, green)",
                        ["(blue, blue)"] = "(blue, blue)",
                        ["(blue, purple)"] = "(blue, purple)",
                        ["(purple, red)"] = "(purple, red)",
                        ["(purple, orange)"] = "(purple, orange)",
                        ["(purple, yellow)"] = "(purple, yellow)",
                        ["(purple, green)"] = "(purple, green)",
                        ["(purple, blue)"] = "(purple, blue)",
                        ["(purple, purple)"] = "(purple, purple)",
                    },
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Yellow"] = "Yellow",
                        ["Purple"] = "Purple",
                        ["White"] = "White",
                    },
                },
                [SNotTheBulb.ScrewCap] = new()
                {
                    // English: What was the material of the screw cap on {0}?
                    Question = "What was the material of the screw cap on {0}?",
                    Answers = new()
                    {
                        ["Copper"] = "Copper",
                        ["Silver"] = "Silver",
                        ["Gold"] = "Gold",
                        ["Plastic"] = "Plastic",
                        ["Carbon Fibre"] = "Carbon Fibre",
                        ["Ceramic"] = "Ceramic",
                    },
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
                    Answers = new()
                    {
                        ["white"] = "white",
                        ["red"] = "red",
                        ["yellow"] = "yellow",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["white/red"] = "white/red",
                        ["white/yellow"] = "white/yellow",
                        ["white/green"] = "white/green",
                        ["white/blue"] = "white/blue",
                        ["red/yellow"] = "red/yellow",
                        ["red/green"] = "red/green",
                        ["red/blue"] = "red/blue",
                        ["yellow/green"] = "yellow/green",
                        ["yellow/blue"] = "yellow/blue",
                        ["green/blue"] = "green/blue",
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
                    Question = "What color did the background flash in {0}?",
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
                    Answers = new()
                    {
                        ["top left"] = "top left",
                        ["top right"] = "top right",
                        ["middle left"] = "middle left",
                        ["middle right"] = "middle right",
                        ["bottom left"] = "bottom left",
                        ["bottom right"] = "bottom right",
                    },
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
                    Answers = new()
                    {
                        ["top left"] = "top left",
                        ["top right"] = "top right",
                        ["middle left"] = "middle left",
                        ["middle right"] = "middle right",
                        ["bottom left"] = "bottom left",
                        ["bottom right"] = "bottom right",
                    },
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Blue"] = "Blue",
                        ["White"] = "White",
                    },
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
                    Answers = new()
                    {
                        ["Up"] = "Up",
                        ["Right"] = "Right",
                        ["Down"] = "Down",
                        ["Left"] = "Left",
                    },
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Purple"] = "Purple",
                    },
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
            Questions = new()
            {
                [SObjectShows.Contestants] = new()
                {
                    // English: Which of these was a contestant on {0}?
                    Question = "Which of these was a contestant on {0} but not the final winner?",
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
                    Question = "Was war bei {0} die Startkugel?",
                },
                [SOctadecayotton.Rotations] = new()
                {
                    // English: What was one of the subrotations in the {1} rotation in {0}?
                    // Example: What was one of the subrotations in the first rotation in Octadecayotton?
                    Question = "Welche Teilrotation kam bei {0} bei der {1}en Rotation vor?",
                },
            },
        },

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

        [typeof(SOffKeys)] = new()
        {
            ModuleName = "Verstimmte Tasten",
            Gender = Gender.Plural,
            ModuleNameDative = "Verstimmten Tasten",
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
                    Answers = new()
                    {
                        ["Two Reeds"] = "Two Reeds",
                        ["Lion"] = "Lion",
                        ["Twisted Flax"] = "Twisted Flax",
                        ["Horned Viper"] = "Horned Viper",
                        ["Water"] = "Water",
                        ["Eye of Horus"] = "Eye of Horus",
                    },
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

        [typeof(SOrangeCipher)] = new()
        {
            ModuleName = "Orangene Geheimschrift",
            Gender = Gender.Feminine,
            ModuleNameDative = "Orangenen Geheimschrift",
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

        [typeof(SOrderedKeys)] = new()
        {
            NeedsTranslation = true,
            Questions = new()
            {
                [SOrderedKeys.Colors] = new()
                {
                    // English: What color was this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "What color was the {2} key in the {1} stage of {0}?",
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
                    Question = "What was the label on the {2} key in the {1} stage of {0}?",
                },
                [SOrderedKeys.LabelColors] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was the label of this key in the first stage of Ordered Keys? (+ sprite)
                    Question = "What color was the label of the {2} key in the {1} stage of {0}?",
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

        [typeof(SOrientationHypercube)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Orientierungshyperwürfel",
            Gender = Gender.Masculine,
            Questions = new()
            {
                [SOrientationHypercube.InitialFaceColour] = new()
                {
                    // English: What was the initial colour of the {1} face in {0}?
                    // Example: What was the initial colour of the right face in Orientation Hypercube?
                    Question = "Was war bei {0} die Anfangsfarbe der {1} Seite?",
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

        [typeof(SPartialDerivatives)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Partielle Ableitungen",
            Gender = Gender.Plural,
            ModuleNameDative = "Partiellen Ableitungen",
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

        [typeof(SPentabutton)] = new()
        {
            NeedsTranslation = true,
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

        [typeof(SPeriodicWords)] = new()
        {
            ModuleName = "Periodische Wörter",
            Gender = Gender.Plural,
            ModuleNameDative = "Periodischen Wörtern",
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

        [typeof(SPhosphorescence)] = new()
        {
            NeedsTranslation = true,
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
        },

        [typeof(SPinkButton)] = new()
        {
            NeedsTranslation = true,
            Gender = Gender.Masculine,
            ModuleNameDative = "Pinkfarbenen Knopf",
            ModuleNameWithThe = "Der Pinkfarbene Knopf",
            Questions = new()
            {
                [SPinkButton.Words] = new()
                {
                    // English: What was the {1} word in {0}?
                    // Example: What was the first word in Pink Button?
                    Question = "Was war bei {0} das erste Wort?",
                },
                [SPinkButton.Colors] = new()
                {
                    // English: What was the {1} color in {0}?
                    // Example: What was the first color in Pink Button?
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
                    Question = "What was the planet shown in {0}?",
                },
            },
        },

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
                    Question = "Where did the prisoner start in {0}?",
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
                    Question = "Which word was use in {0}?",
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
                    Question = "What was the {1} coupled car in {0}?",
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
            Questions = new()
            {
                [SRedButtont.Word] = new()
                {
                    // English: What was the word before “SUBMIT” in {0}?
                    Question = "What was the word before 'SUBMIT' in {0}?",
                },
            },
        },

        [typeof(SRedCipher)] = new()
        {
            ModuleName = "Rote Geheimschrift",
            Gender = Gender.Feminine,
            ModuleNameDative = "Roten Geheimschrift",
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
                        ["win"] = "gewonnen",
                        ["blue"] = "blaue",
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
                        ["won"] = "gewann",
                        ["lost"] = "verlor",
                        ["blue"] = "blue",
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

        [typeof(SRule)] = new()
        {
            ModuleName = "Regel",
            Gender = Gender.Feminine,
            ModuleNameDative = "Regel",
            ModuleNameWithThe = "Die Regel",
            Questions = new()
            {
                [SRule.Number] = new()
                {
                    // English: What was the rule number in {0}?
                    Question = "Was war bei {0} die Regelnummer?",
                },
            },
        },

        [typeof(SRuleOfThree)] = new()
        {
            ModuleName = "Dreierregel",
            Gender = Gender.Feminine,
            Questions = new()
            {
                [SRuleOfThree.Coordinates] = new()
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
                [SRuleOfThree.Cycles] = new()
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Yellow",
                        ["Green"] = "Green",
                        ["Blue"] = "Blue",
                        ["Purple"] = "Purple",
                    },
                },
                [SSimonScreams.RuleSimple] = new()
                {
                    // English: In which stage(s) of {0} was “{1}” the applicable rule?
                    // Example: In which stage(s) of Simon Screams was “a color flashed, then a color two away, then the first again” the applicable rule?
                    Question = "In welcher/-n Stufe(n) bei {0} war “{1}” die zutreffende Regel?",
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
                },
                [SSimonScreams.RuleComplex] = new()
                {
                    // English: In which stage(s) of {0} was “{1} flashed out of {2}, {3}, and {4}” the applicable rule?
                    // Example: In which stage(s) of Simon Screams was “at most one color flashed out of Red, Orange, and Yellow” the applicable rule?
                    Question = "In welcher/-n Stufe(n) bei {0} war “{1} der Farben {2}, {3} und {4} blinkt” die zutreffende Regel?",
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
                    Arguments = new()
                    {
                        ["at most one color"] = "maximal eine",
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["at least two colors"] = "mindestens zwei",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
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
                    // English: What sound did the {1} button press make in {0}?
                    // Example: What sound did the first button press make in Simon Smiles?
                    Question = "What sound did the {1} button press make in {0}?",
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
                    Arguments = new()
                    {
                        ["color(s) flashed"] = "aufgeleuchtet",
                        ["color(s) didn’t flash"] = "nicht aufgeleuchtet",
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

        [typeof(SSlowMath)] = new()
        {
            Questions = new()
            {
                [SSlowMath.LastLetters] = new()
                {
                    // English: What was the last triplet of letters in {0}?
                    Question = "What was the last pair of letters in {0}?",
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
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["orange"] = "orange",
                        ["pink"] = "pink",
                        ["purple"] = "purple",
                        ["grey"] = "grey",
                        ["white"] = "white",
                    },
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Yellow"] = "Yellow",
                        ["Blue"] = "Blue",
                    },
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
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["orange"] = "orange",
                        ["yellow"] = "yellow",
                        ["green"] = "green",
                        ["blue"] = "blue",
                        ["purple"] = "purple",
                    },
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
                    Question = "What was displayed on the screen in stage {1} of {0}?",
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
                    Answers = new()
                    {
                        ["red"] = "red",
                        ["green"] = "green",
                        ["blue"] = "blue",
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
                    Question = "What was the {1} split in {0}?",
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
            ModuleName = "Ultimative Geheimschrift",
            Gender = Gender.Feminine,
            ModuleNameDative = "Ultimativen Geheimschrift",
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

        [typeof(SUltimateCycle)] = new()
        {
            ModuleName = "Ultimative Schiffer",
            Gender = Gender.Feminine,
            ModuleNameDative = "Ultimativen Schiffer",
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
        },

        [typeof(SUltracube)] = new()
        {
            Questions = new()
            {
                [SUltracube.Rotations] = new()
                {
                    // English: What was the {1} rotation in {0}?
                    // Example: What was the first rotation in Ultracube?
                    Question = "Was war die {1}e Rotation in {0}?",
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
                    Answers = new()
                    {
                        ["White"] = "White",
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                        ["Magenta"] = "Magenta",
                    },
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
                    Answers = new()
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
            NeedsTranslation = true,
            Questions = new()
            {
                [SUnorderedKeys.KeyColor] = new()
                {
                    // English: What color was this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "What color was this key in the {1} stage of {0}?",
                },
                [SUnorderedKeys.LabelColor] = new()
                {
                    // English: What color was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What color was the label of this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "What color was the label of this key in the {1} stage of {0}?",
                },
                [SUnorderedKeys.Label] = new()
                {
                    // English: What was the label of this key in the {1} stage of {0}? (+ sprite)
                    // Example: What was the label of this key in the first stage of Unordered Keys? (+ sprite)
                    Question = "What color was the label of this key in the {1} stage of {0}?",
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
            NeedsTranslation = true,
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
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Yellow"] = "Gelb",
                        ["Orange"] = "Orange",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Lila",
                    },
                    Arguments = new()
                    {
                        ["first"] = "erste",
                        ["last"] = "letzte",
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
                    Answers = new()
                    {
                        ["White"] = "White",
                        ["Red"] = "Red",
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                        ["Magenta"] = "Magenta",
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
                    Question = "What was the word of the {1} goal in {0}?",
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
                    Question = "What was the colour of the {1} goal in {0}?",
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

        [typeof(SVariety)] = new()
        {
            NeedsTranslation = true,
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
                    Question = "Was war bei {0} die höchste Zahl auf dem {1}Timer?",
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
                    Question = "Was war bei {0} der Wert n beim {1}Drehregler?",
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
                    Question = "Was war bei {0} der Wert n bei der {1}Glühlampe?",
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
                    Discriminator = "der Vielfalt mit {0}",
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
                    Question = "Was war das Wort bei {0}?",
                },
            },
        },

        [typeof(SVectors)] = new()
        {
            NeedsTranslation = true,
            ModuleName = "Vektoren",
            Gender = Gender.Plural,
            Questions = new()
            {
                [SVectors.Colors] = new()
                {
                    // English: What was the color of the {1} vector in {0}?
                    // Example: What was the color of the first vector in Vectors?
                    Question = "Welche Farbe hatte bei {0} der {1} Vektor?",
                    Answers = new()
                    {
                        ["Red"] = "Rot",
                        ["Orange"] = "Orange",
                        ["Yellow"] = "Gelb",
                        ["Green"] = "Grün",
                        ["Blue"] = "Blau",
                        ["Purple"] = "Violett",
                    },
                    Arguments = new()
                    {
                        ["first"] = "erste",
                        ["second"] = "zweite",
                        ["third"] = "dritte",
                        ["only"] = "einzige",
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
                    Answers = new()
                    {
                        ["Red"] = "Red",
                        ["Orange"] = "Orange",
                        ["Green"] = "Green",
                        ["Yellow"] = "Yellow",
                        ["Blue"] = "Blue",
                        ["Aqua"] = "Aqua",
                        ["White"] = "White",
                        ["Black"] = "Black",
                    },
                },
            },
        },

        [typeof(SVioletCipher)] = new()
        {
            ModuleName = "Violette Geheimschrift",
            Gender = Gender.Feminine,
            ModuleNameDative = "Violetten Geheimschrift",
            Questions = new()
            {
                [SVioletCipher.Screen] = new()
                {
                    // English: What was on the {1} screen on page {2} in {0}?
                    // Example: What was on the top screen on page 1 in Violet Cipher?
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
                    Answers = new()
                    {
                        ["Blue"] = "Blue",
                        ["Green"] = "Green",
                        ["Red"] = "Red",
                        ["White"] = "White",
                    },
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
                    Question = "Which of these cells was part of the cube's path in {0}?",
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
            ModuleName = "Weiße Geheimschrift",
            Gender = Gender.Feminine,
            ModuleNameDative = "Weißen Geheimschrift",
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
                    Question = "What was the display in the {1} stage on {0}?",
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
                    Answers = new()
                    {
                        ["blue"] = "blue",
                        ["green"] = "green",
                        ["grey"] = "grey",
                        ["orange"] = "orange",
                        ["purple"] = "purple",
                        ["red"] = "red",
                    },
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
                    Answers = new()
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
                    Answers = new()
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
                    Answers = new()
                    {
                        ["Yahtzee"] = "Yahtzee",
                        ["large straight"] = "large straight",
                        ["small straight"] = "small straight",
                        ["four of a kind"] = "four of a kind",
                        ["full house"] = "full house",
                        ["three of a kind"] = "three of a kind",
                        ["two pairs"] = "two pairs",
                        ["pair"] = "pair",
                    },
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
            ModuleName = "Gelbe Geheimschrift",
            Gender = Gender.Feminine,
            ModuleNameDative = "Gelben Geheimschrift",
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
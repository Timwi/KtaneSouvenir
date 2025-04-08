using System.Collections.Generic;
using static Souvenir.Translation_de.Gender;

namespace Souvenir
{
    public class Translation_de : TranslationBase<Translation_de.TranslationInfo_de>
    {
        public sealed class TranslationInfo_de : TranslationInfo
        {
            public Gender Gender = Neuter;
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

        public override string FormatModuleName(Question question, bool addSolveCount, int numSolved) => addSolveCount
            ? (_translations.Get(question)?.Gender ?? Neuter) switch
            {
                Feminine => $"der als {ordinal(numSolved)}e gelösten {_translations.Get(question)?.ModuleNameDative ?? _translations.Get(question)?.ModuleName ?? Ut.GetAttribute(question).ModuleName}",
                Masculine => $"dem als {ordinal(numSolved)}en gelösten {_translations.Get(question)?.ModuleNameDative ?? _translations.Get(question)?.ModuleName ?? Ut.GetAttribute(question).ModuleName}",
                Neuter => $"dem als {ordinal(numSolved)}es gelösten {_translations.Get(question)?.ModuleNameDative ?? _translations.Get(question)?.ModuleName ?? Ut.GetAttribute(question).ModuleName}",
                _ => /* Plural */ $"den als {ordinal(numSolved)}e gelösten {_translations.Get(question)?.ModuleNameDative ?? _translations.Get(question)?.ModuleName ?? Ut.GetAttribute(question).ModuleName}",
            }
            : _translations.Get(question)?.ModuleNameWithThe ?? _translations.Get(question)?.ModuleName ?? Ut.GetAttribute(question).ModuleNameWithThe;

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

        protected override Dictionary<Question, TranslationInfo_de> _translations => new()
        {
            #region Translatable strings
            // 0
            // What was the initially displayed number in {0}?
            // What was the initially displayed number in 0?
            [Question._0Number] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initially displayed number in {0}?",
            },

            // 1000 Words
            // What was the {1} word shown in {0}?
            // What was the first word shown in 1000 Words?
            [Question._1000WordsWords] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "1000 Wörtern",
                QuestionText = "Was war bei {0} das {1}e angezeigte Wort?",
                ModuleName = "1000 Wörter",
            },

            // 100 Levels of Defusal
            // What was the {1} displayed letter in {0}?
            // What was the first displayed letter in 100 Levels of Defusal?
            [Question._100LevelsOfDefusalLetters] = new()
            {
                Gender = Gender.Plural,
                QuestionText = "Was war bei {0} der {1}e angezeigte Buchstabe?",
                ModuleName = "100 Ebenen der Entschärfung",
            },

            // The 1, 2, 3 Game
            // Who was the opponent in {0}?
            // Who was the opponent in The 1, 2, 3 Game?
            [Question._123GameProfile] = new()
            {
                QuestionText = "Wer war bei {0} der Gegner?",
            },
            // Who was the opponent in {0}?
            // Who was the opponent in The 1, 2, 3 Game?
            [Question._123GameName] = new()
            {
                QuestionText = "Wer war bei {0} der Gegner?",
            },

            // 1D Chess
            // What was {1} in {0}?
            // What was your first move in 1D Chess?
            [Question._1DChessMoves] = new()
            {
                QuestionText = "Was war bei {0} {1}?",
                ModuleName = "1D-Schach",
                FormatArgs = new Dictionary<string, string>
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

            // 3D Chess
            // Which of these pieces were present in {0}?
            // Which of these pieces were present in 3D Chess?
            [Question._3DChessPresentPieces] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these pieces were present in {0}?",
            },
            // Which of these pieces were not present in {0}?
            // Which of these pieces were not present in 3D Chess?
            [Question._3DChessAbsentPieces] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these pieces were not present in {0}?",
            },

            // 3D Maze
            // What were the markings in {0}?
            // What were the markings in 3D Maze?
            [Question._3DMazeMarkings] = new()
            {
                QuestionText = "Was waren bei {0} die Markierungen?",
                ModuleName = "3D-Labyrinth",
            },
            // What was the cardinal direction in {0}?
            // What was the cardinal direction in 3D Maze?
            [Question._3DMazeBearing] = new()
            {
                QuestionText = "Was war bei {0} die Himmelsrichtung?",
                ModuleName = "3D-Labyrinth",
                Answers = new Dictionary<string, string>
                {
                    ["North"] = "Norden",
                    ["South"] = "Süden",
                    ["West"] = "Westen",
                    ["East"] = "Osten",
                },
            },

            // 3D Tap Code
            // What was the received word in {0}?
            // What was the received word in 3D Tap Code?
            [Question._3DTapCodeWord] = new()
            {
                Gender = Gender.Plural,
                QuestionText = "Was war bei {0} das empfangene Wort?",
                ModuleName = "3D-Klopfzeichen",
            },

            // 3D Tunnels
            // What was the {1} goal node in {0}?
            // What was the first goal node in 3D Tunnels?
            [Question._3DTunnelsTargetNode] = new()
            {
                Gender = Gender.Plural,
                QuestionText = "Was war bei {0} der Zielpunkt?",
                ModuleName = "3D-Tunnels",
            },

            // 3 LEDs
            // What was the initial state of the LEDs in {0} (in reading order)?
            // What was the initial state of the LEDs in 3 LEDs (in reading order)?
            [Question._3LEDsInitialState] = new()
            {
                QuestionText = "Was war bei {0} der Anfangszustand in Lesereihenfolge?",
                Answers = new Dictionary<string, string>
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

            // 3N+1
            // What number was initially displayed in {0}?
            // What number was initially displayed in 3N+1?
            [Question._3NPlus1] = new()
            {
                QuestionText = "Welche Zahl war bei {0} anfänglich zu sehen?",
            },

            // 64
            // What was the displayed number in {0}?
            // What was the displayed number in 64?
            [Question._64DisplayedNumber] = new()
            {
                QuestionText = "Was war die bei {0} angezeigte Zahl?",
            },

            // 7
            // What was the {1} channel’s initial value in {0}?
            // What was the red channel’s initial value in 7?
            [Question._7InitialValues] = new()
            {
                QuestionText = "Was war bei {0} der Anfangswert im {1}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "Rotkanal",
                    ["green"] = "Grünkanal",
                    ["blue"] = "Blaukanal",
                },
            },
            // What LED color was shown in stage {1} of {0}?
            // What LED color was shown in stage 0 of 7?
            [Question._7LedColors] = new()
            {
                QuestionText = "Was war beim {1}en Schritt von {0} die LED-Farbe?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["white"] = "weiß",
                },
            },

            // 9-Ball
            // What was the number of ball {1} in {0}?
            // What was the number of ball A in 9-Ball?
            [Question._9BallLetters] = new()
            {
                QuestionText = "Welche Zahl hatte bei {0} die Kugel {1}?",
            },
            // What was the letter of ball {1} in {0}?
            // What was the letter of ball 2 in 9-Ball?
            [Question._9BallNumbers] = new()
            {
                QuestionText = "Welchen Buchstaben hatte bei {0} die Kugel {1}?",
            },

            // Abyss
            // What was the {1} character displayed on {0}?
            // What was the first character displayed on Abyss?
            [Question.AbyssSeed] = new()
            {
                QuestionText = "Welcher Buchstabe wurde bei {0} als {1}es angezeigt?",
            },

            // Accumulation
            // What was the background color on the {1} stage in {0}?
            // What was the background color on the first stage in Accumulation?
            [Question.AccumulationBackgroundColor] = new()
            {
                Gender = Gender.Masculine,
                QuestionText = "Was war bei {0} die Hintergrundfarbe im {1}en Schritt?",
                ModuleName = "Akkumulator",
                Answers = new Dictionary<string, string>
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
            // What was the border color in {0}?
            // What was the border color in Accumulation?
            [Question.AccumulationBorderColor] = new()
            {
                Gender = Gender.Masculine,
                QuestionText = "Was war bei {0} die Rahmenfarbe?",
                ModuleName = "Akkumulator",
                Answers = new Dictionary<string, string>
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

            // Adventure Game
            // Which item was the {1} correct item you used in {0}?
            // Which item was the first correct item you used in Adventure Game?
            [Question.AdventureGameCorrectItem] = new()
            {
                QuestionText = "Welches Objekt wurde bei {0} als {1}es korrekt verwendet?",
            },
            // What enemy were you fighting in {0}?
            // What enemy were you fighting in Adventure Game?
            [Question.AdventureGameEnemy] = new()
            {
                QuestionText = "Welcher Gegner wurde bei {0} bekämpft?",
            },

            // Affine Cycle
            // What was the {1} in {0}?
            // What was the message in Affine Cycle?
            [Question.AffineCycleWord] = new()
            {
                QuestionText = "Was war die {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Nachricht",
                    ["response"] = "Antwort",
                },
            },

            // A Letter
            // What was the initial letter in {0}?
            // What was the initial letter in A Letter?
            [Question.ALetterInitialLetter] = new()
            {
                QuestionText = "Was war bei {0} der Anfangsbuchstabe?",
                ModuleName = "Ein Buchstabe",
            },

            // Alfa-Bravo
            // Which letter was pressed in {0}?
            // Which letter was pressed in Alfa-Bravo?
            [Question.AlfaBravoPressedLetter] = new()
            {
                QuestionText = "Welcher Buchstabe wurde bei {0} eingegeben?",
            },
            // Which letter was to the left of the pressed one in {0}?
            // Which letter was to the left of the pressed one in Alfa-Bravo?
            [Question.AlfaBravoLeftPressedLetter] = new()
            {
                QuestionText = "Welcher Buchstabe war bei {0} links vom eingegebenen?",
            },
            // Which letter was to the right of the pressed one in {0}?
            // Which letter was to the right of the pressed one in Alfa-Bravo?
            [Question.AlfaBravoRightPressedLetter] = new()
            {
                QuestionText = "Welcher Buchstabe war bei {0} rechts vom eingegebenen?",
            },
            // What was the last digit on the small display in {0}?
            // What was the last digit on the small display in Alfa-Bravo?
            [Question.AlfaBravoDigit] = new()
            {
                QuestionText = "Was war bei {0} die letzte Ziffer in der kleinen Anzeige?",
            },

            // Algebra
            // What was the first equation in {0}?
            // What was the first equation in Algebra?
            [Question.AlgebraEquation1] = new()
            {
                QuestionText = "Was war bei {0} die erste Gleichung?",
            },
            // What was the second equation in {0}?
            // What was the second equation in Algebra?
            [Question.AlgebraEquation2] = new()
            {
                QuestionText = "Was war bei {0} die zweite Gleichung?",
            },

            // Algorithmia
            // Which position was the {1} position in {0}?
            // Which position was the starting position in Algorithmia?
            [Question.AlgorithmiaPositions] = new()
            {
                QuestionText = "Was war bei {0} die {1}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["starting"] = "Anfangsposition",
                    ["goal"] = "Zielposition",
                },
            },
            // What was the color of the colored bulb in {0}?
            // What was the color of the colored bulb in Algorithmia?
            [Question.AlgorithmiaColor] = new()
            {
                QuestionText = "Welche Farbe hatte die gefärbte Glühlampe bei {0}?",
            },
            // Which number was present in the seed in {0}?
            // Which number was present in the seed in Algorithmia?
            [Question.AlgorithmiaSeed] = new()
            {
                QuestionText = "Welche Zahl war bei {0} im Startwert enthalten?",
            },

            // Alphabetical Ruling
            // What was the letter displayed in the {1} stage of {0}?
            // What was the letter displayed in the first stage of Alphabetical Ruling?
            [Question.AlphabeticalRulingLetter] = new()
            {
                QuestionText = "Welcher Buchstabe wurde bei {0} im {1}en Schritt angezeigt?",
            },
            // What was the number displayed in the {1} stage of {0}?
            // What was the number displayed in the first stage of Alphabetical Ruling?
            [Question.AlphabeticalRulingNumber] = new()
            {
                QuestionText = "Welche Zahl wurde bei {0} im {1}en Schritt angezeigt?",
            },

            // Alphabet Numbers
            // Which of these numbers was on one of the buttons in the {1} stage of {0}?
            // Which of these numbers was on one of the buttons in the first stage of Alphabet Numbers?
            [Question.AlphabetNumbersDisplayedNumbers] = new()
            {
                QuestionText = "Welche Zahl war auf einem der Knöpfe im {1}en Schritt von {0} zu sehen?",
            },

            // Alphabet Tiles
            // What was the {1} letter shown during the cycle in {0}?
            // What was the first letter shown during the cycle in Alphabet Tiles?
            [Question.AlphabetTilesCycle] = new()
            {
                QuestionText = "Welcher Buchstabe war im Zyklus bei {0} als {1}es zu sehen?",
            },
            // What was the missing letter in {0}?
            // What was the missing letter in Alphabet Tiles?
            [Question.AlphabetTilesMissingLetter] = new()
            {
                QuestionText = "Was war bei {0} der fehlende Buchstabe?",
            },

            // Alpha-Bits
            // What character was displayed on the {1} screen on the {2} in {0}?
            // What character was displayed on the first screen on the left in Alpha-Bits?
            [Question.AlphaBitsDisplayedCharacters] = new()
            {
                QuestionText = "Welches Zeichen wurde bei {0} im {1}en {2} Display angezeigt?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "linken",
                    ["right"] = "rechten",
                },
            },

            // Amusement Parks
            // Which ride was available in {0}?
            // Which ride was available in Amusement Parks?
            [Question.AmusementParksRides] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which ride was available in {0}?",
            },

            // Ángel Hernández
            // What letter was shown by the raised buttons on the {1} stage on {0}?
            // What letter was shown by the raised buttons on the first stage on Ángel Hernández?
            [Question.AngelHernandezMainLetter] = new()
            {
                QuestionText = "Welcher Buchstabe wurde im {1}en Schritt von {0} durch die erhöhten Knöpfe dargestellt?",
            },

            // The Arena
            // What was the maximum weapon damage of the attack phase in {0}?
            // What was the maximum weapon damage of the attack phase in The Arena?
            [Question.ArenaDamage] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameWithThe = "Die Arena",
                QuestionText = "Was war bei {0} in der Angriffsphase der maximale Waffenschaden?",
                ModuleName = "Arena",
            },
            // Which enemy was present in the defend phase of {0}?
            // Which enemy was present in the defend phase of The Arena?
            [Question.ArenaEnemies] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameWithThe = "Die Arena",
                QuestionText = "Welcher Gegner war bei {0} in der Verteidigungsphase anwesend?",
                ModuleName = "Arena",
            },
            // Which was a number present in the grab phase of {0}?
            // Which was a number present in the grab phase of The Arena?
            [Question.ArenaNumbers] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameWithThe = "Die Arena",
                QuestionText = "Welche Zahl war bei {0} in der Sammelphase dabei?",
                ModuleName = "Arena",
            },

            // Arithmelogic
            // What was the symbol on the submit button in {0}?
            // What was the symbol on the submit button in Arithmelogic?
            [Question.ArithmelogicSubmit] = new()
            {
                QuestionText = "Welches Symbol war bei {0} auf dem Eingabeknopf?",
                ModuleName = "Arithmologik",
            },
            // Which number was selectable, but not the solution, in the {1} screen on {0}?
            // Which number was selectable, but not the solution, in the left screen on Arithmelogic?
            [Question.ArithmelogicNumbers] = new()
            {
                QuestionText = "Welche Zahl war bei {0} im {1} Bildschirm auswählbar, aber nicht die Lösung?",
                ModuleName = "Arithmologik",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "linken",
                    ["middle"] = "mittleren",
                    ["right"] = "rechten",
                },
            },

            // ASCII Maze
            // What was the {1} character displayed on {0}?
            // What was the first character displayed on ASCII Maze?
            [Question.ASCIIMazeCharacters] = new()
            {
                QuestionText = "Was war bei {0} das {1}e angezeigte Zeichen?",
                ModuleName = "ASCII-Labyrinth",
            },

            // A Square
            // Which of these was an index color in {0}?
            // Which of these was an index color in A Square?
            [Question.ASquareIndexColors] = new()
            {
                QuestionText = "Welche Indexfarbe kam bei {0} vor?",
                ModuleName = "Ein Quadrat",
            },
            // Which color was submitted {1} in {0}?
            // Which color was submitted first in A Square?
            [Question.ASquareCorrectColors] = new()
            {
                QuestionText = "Welche Farbe wurde bei {0} als {1}es eingegeben?",
                ModuleName = "Ein Quadrat",
            },

            // Audio Morse
            // What was signaled in {0}?
            // What was signaled in Audio Morse?
            [Question.AudioMorseSound] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was signaled in {0}?",
            },

            // The Azure Button
            // What was T in {0}?
            // What was T in The Azure Button?
            [Question.AzureButtonT] = new()
            {
                ModuleNameWithThe = "Der Azurfarbene Knopf",
                QuestionText = "Was war T bei {0}?",
                ModuleName = "Azurfarbenen Knopf",
            },
            // Which of these cards was shown in Stage 1, but not T, in {0}?
            // Which of these cards was shown in Stage 1, but not T, in The Azure Button?
            [Question.AzureButtonNotT] = new()
            {
                ModuleNameWithThe = "Der Azurfarbene Knopf",
                QuestionText = "Welche Karte war bei {0} in Schritt 1 zu sehen, aber nicht T?",
                ModuleName = "Azurfarbenen Knopf",
            },
            // What was M in {0}?
            // What was M in The Azure Button?
            [Question.AzureButtonM] = new()
            {
                ModuleNameWithThe = "Der Azurfarbene Knopf",
                QuestionText = "Was war M bei {0}?",
                ModuleName = "Azurfarbenen Knopf",
            },
            // What was the {1} direction in the decoy arrow in {0}?
            // What was the first direction in the decoy arrow in The Azure Button?
            [Question.AzureButtonDecoyArrowDirection] = new()
            {
                ModuleNameWithThe = "Der Azurfarbene Knopf",
                QuestionText = "Was war bei {0} die {1}e Richtung im ungenutzten Pfeil?",
                ModuleName = "Azurfarbenen Knopf",
            },
            // What was the {1} direction in the {2} non-decoy arrow in {0}?
            // What was the first direction in the first non-decoy arrow in The Azure Button?
            [Question.AzureButtonNonDecoyArrowDirection] = new()
            {
                ModuleNameWithThe = "Der Azurfarbene Knopf",
                QuestionText = "Was war bei {0} die {1}e Richtung im {2}en genutzten Pfeil?",
                ModuleName = "Azurfarbenen Knopf",
            },

            // Bakery
            // Which menu item was present in {0}?
            // Which menu item was present in Bakery?
            [Question.BakeryItems] = new()
            {
                QuestionText = "Was stand bei {0} auf dem Menü angeboten?",
                ModuleName = "Bäckerei",
            },

            // Bamboozled Again
            // What color was the {1} correct button in {0}?
            // What color was the first correct button in Bamboozled Again?
            [Question.BamboozledAgainButtonColor] = new()
            {
                QuestionText = "Welche Farbe hatte der {0}e korrekte Knopf bei {0}?",
                ModuleName = "Wieder Übers Ohr Gehauen",
                Answers = new Dictionary<string, string>
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
            // What was the text on the {1} correct button in {0}?
            // What was the text on the first correct button in Bamboozled Again?
            [Question.BamboozledAgainButtonText] = new()
            {
                QuestionText = "Was war bei {0} die Aufschrift des {1}en korrekten Knopfes?",
                ModuleName = "Wieder Übers Ohr Gehauen",
            },
            // What was the {1} decrypted text on the display in {0}?
            // What was the first decrypted text on the display in Bamboozled Again?
            [Question.BamboozledAgainDisplayTexts1] = new()
            {
                QuestionText = "Was war bei {0} der {1}e Text auf dem Display, aber entschlüsselt?",
                ModuleName = "Wieder Übers Ohr Gehauen",
            },
            // What was the {1} decrypted text on the display in {0}?
            // What was the first decrypted text on the display in Bamboozled Again?
            [Question.BamboozledAgainDisplayTexts2] = new()
            {
                QuestionText = "Was war bei {0} der {1}e Text auf dem Display, aber entschlüsselt?",
                ModuleName = "Wieder Übers Ohr Gehauen",
            },
            // What color was the {1} text on the display in {0}?
            // What color was the first text on the display in Bamboozled Again?
            [Question.BamboozledAgainDisplayColor] = new()
            {
                QuestionText = "In welcher Farbe wurde bei {0} der {1}e Text auf dem Display angezeigt?",
                ModuleName = "Wieder Übers Ohr Gehauen",
                Answers = new Dictionary<string, string>
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

            // Bamboozling Button
            // What color was the button in the {1} stage of {0}?
            // What color was the button in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonColor] = new()
            {
                QuestionText = "Welche Farbe hatte der Knopf bei {0} im {1}en Schritt?",
                Answers = new Dictionary<string, string>
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
            // What was the {2} label on the button in the {1} stage of {0}?
            // What was the top label on the button in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonLabel] = new()
            {
                QuestionText = "Was war bei {0} im {1}en Schritt die {2} Aufschrift auf dem Knopf?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "obere",
                    ["bottom"] = "untere",
                },
            },
            // What was the {2} display in the {1} stage of {0}?
            // What was the first display in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonDisplay] = new()
            {
                QuestionText = "Was war bei {0} im {1}en Schritt die {2}e Anzeige auf dem Display?",
            },
            // What was the color of the {2} display in the {1} stage of {0}?
            // What was the color of the first display in the first stage of Bamboozling Button?
            [Question.BamboozlingButtonDisplayColor] = new()
            {
                QuestionText = "In welcher Farbe erschien bei {0} im {1}en Schritt die {2}e Anzeige auf dem Display?",
                Answers = new Dictionary<string, string>
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

            // Bar Charts
            // What was the category of {0}?
            // What was the category of Bar Charts?
            [Question.BarChartsCategory] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Balkendiagrammen",
                QuestionText = "Was war bei {0} die Kategorie?",
                ModuleName = "Balkendiagramme",
            },
            // What was the color of the {1} bar in {0}?
            // What was the color of the first bar in Bar Charts?
            [Question.BarChartsColor] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Balkendiagrammen",
                QuestionText = "Was war bei {0} die Farbe des {1}en Balkens?",
                ModuleName = "Balkendiagramme",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                },
            },
            // What was the position of the {1} bar in {0}?
            // What was the position of the shortest bar in Bar Charts?
            [Question.BarChartsHeight] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Balkendiagrammen",
                QuestionText = "An welcher Position war bei {0} der {1} Balken?",
                ModuleName = "Balkendiagramme",
                FormatArgs = new Dictionary<string, string>
                {
                    ["shortest"] = "kürzeste",
                    ["second shortest"] = "zweitkürzeste",
                    ["second tallest"] = "zweithöchste",
                    ["tallest"] = "höchste",
                },
            },
            // What was the label of the {1} bar in {0}?
            // What was the label of the first bar in Bar Charts?
            [Question.BarChartsLabel] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Balkendiagrammen",
                QuestionText = "Was war bei {0} die Beschriftung des {1}en Balkens?",
                ModuleName = "Balkendiagramme",
            },
            // What was the unit of {0}?
            // What was the unit of Bar Charts?
            [Question.BarChartsUnit] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Balkendiagrammen",
                QuestionText = "Was war bei {0} die Einheit?",
                ModuleName = "Balkendiagramme",
            },

            // Barcode Cipher
            // What was the screen number in {0}?
            // What was the screen number in Barcode Cipher?
            [Question.BarcodeCipherScreenNumber] = new()
            {
                QuestionText = "Welche Zahl war bei {0} auf dem Display?",
            },
            // What was the edgework represented by the {1} barcode in {0}?
            // What was the edgework represented by the first barcode in Barcode Cipher?
            [Question.BarcodeCipherBarcodeEdgework] = new()
            {
                QuestionText = "Was wurde bei {0} vom {1}en Barcode wiedergegeben?",
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
                QuestionText = "Was war bei {0} die Lösung für den {1}en Barcode?",
            },

            // Bartending
            // Which ingredient was in the {1} position on {0}?
            // Which ingredient was in the first position on Bartending?
            [Question.BartendingIngredients] = new()
            {
                Gender = Gender.Masculine,
                QuestionText = "Was war bei {0} die Zutat an {1}er Stelle?",
                ModuleName = "Bartender",
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
                QuestionText = "Was war bei {0} diese Bohne?",
                ModuleName = "Bohnen",
                Answers = new Dictionary<string, string>
                {
                    ["Wobbly Orange"] = "Orange wackelnd",
                    ["Wobbly Yellow"] = "Gelb wackelnd",
                    ["Wobbly Green"] = "Grün wackelnd",
                    ["Not Wobbly Orange"] = "Orange nicht wackelnd",
                    ["Not Wobbly Yellow"] = "Gelb nicht wackelnd",
                    ["Not Wobbly Green"] = "Grün nicht wackelnd",
                },
            },

            // Bean Sprouts
            // What was sprout {1} in {0}?
            // What was sprout 1 in Bean Sprouts?
            [Question.BeanSproutsColors] = new()
            {
                QuestionText = "Was war bei {0} der Spross {1}?",
                ModuleName = "Bohnensprossen",
                Answers = new Dictionary<string, string>
                {
                    ["Raw"] = "Roh",
                    ["Cooked"] = "Gekocht",
                    ["Burnt"] = "Angebrannt",
                    ["Fake"] = "Falsch",
                },
            },
            // What bean was on sprout {1} in {0}?
            // What bean was on sprout 1 in Bean Sprouts?
            [Question.BeanSproutsBeans] = new()
            {
                QuestionText = "Welche Bohne war bei {0} auf Spross {1}?",
                ModuleName = "Bohnensprossen",
                Answers = new Dictionary<string, string>
                {
                    ["Left"] = "Die linke",
                    ["Right"] = "Die rechte",
                    ["None"] = "Keine",
                    ["Both"] = "Beide",
                },
            },

            // Big Bean
            // What was the bean in {0}?
            // What was the bean in Big Bean?
            [Question.BigBeanColor] = new()
            {
                QuestionText = "Was war bei {0} die Bohne?",
                ModuleName = "Großbohne",
                Answers = new Dictionary<string, string>
                {
                    ["Wobbly Orange"] = "Orange wackelnd",
                    ["Wobbly Yellow"] = "Gelb wackelnd",
                    ["Wobbly Green"] = "Grün wackelnd",
                    ["Not Wobbly Orange"] = "Orange nicht wackelnd",
                    ["Not Wobbly Yellow"] = "Gelb nicht wackelnd",
                    ["Not Wobbly Green"] = "Grün nicht wackelnd",
                },
            },

            // Big Circle
            // What color was {1} in the solution to {0}?
            // What color was first in the solution to Big Circle?
            [Question.BigCircleColors] = new()
            {
                ModuleNameDative = "Großen Kreis",
                QuestionText = "Welche Farbe war bei {0} die {1}e Farbe in der Lösung?",
                ModuleName = "Großer Kreis",
                Answers = new Dictionary<string, string>
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

            // Binary LEDs
            // At which numeric value did you cut the correct wire in {0}?
            // At which numeric value did you cut the correct wire in Binary LEDs?
            [Question.BinaryLEDsValue] = new()
            {
                Gender = Gender.Plural,
                QuestionText = "Bei welchem Zahlenwert wurde bei {0} der korrekte Draht durchtrennt?",
                ModuleName = "Binär-LEDs",
            },

            // Binary Shift
            // What was the {1} initial number in {0}?
            // What was the top-left initial number in Binary Shift?
            [Question.BinaryShiftInitialNumber] = new()
            {
                QuestionText = "Was war bei {0} die {1} Anfangszahl?",
                FormatArgs = new Dictionary<string, string>
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
            // What number was selected at stage {1} in {0}?
            // What number was selected at stage 0 in Binary Shift?
            [Question.BinaryShiftSelectedNumberPossition] = new()
            {
                QuestionText = "Welche Zahl wurde bei {0} in Schritt {1} ausgewählt?",
                Answers = new Dictionary<string, string>
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
            // What number was not selected at stage {1} in {0}?
            // What number was not selected at stage 0 in Binary Shift?
            [Question.BinaryShiftNotSelectedNumberPossition] = new()
            {
                QuestionText = "Welche Zahl wurde bei {0} in Schritt {1} nicht ausgewählt?",
                Answers = new Dictionary<string, string>
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

            // Binary
            // What word was displayed in {0}?
            // What word was displayed in Binary?
            [Question.BinaryWord] = new()
            {
                QuestionText = "Welches Wort wurde bei {0} angezeigt?",
            },

            // Bitmaps
            // How many pixels were {1} in the {2} quadrant in {0}?
            // How many pixels were white in the top left quadrant in Bitmaps?
            [Question.Bitmaps] = new()
            {
                QuestionText = "Wie viele Pixels waren bei {0} im {2} Quadranten {1}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["white"] = "weiß",
                    ["top left"] = "oberen linken",
                    ["top right"] = "oberen rechten",
                    ["bottom left"] = "unteren linken",
                    ["bottom right"] = "unteren rechten",
                    ["black"] = "schwarz",
                },
            },

            // Black Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Black Cipher?
            [Question.BlackCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameDative = "Schwarzen Geheimschrift",
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Schwarze Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "oberen",
                    ["middle"] = "mittleren",
                    ["bottom"] = "unteren",
                },
            },

            // Blind Maze
            // What color was the {1} button in {0}?
            // What color was the north button in Blind Maze?
            [Question.BlindMazeColors] = new()
            {
                QuestionText = "Welche Farbe hatte der Knopf gen {1} bei {0}?",
                ModuleName = "Blinder Irrgarten",
                FormatArgs = new Dictionary<string, string>
                {
                    ["north"] = "Norden",
                    ["east"] = "Osten",
                    ["west"] = "Westen",
                    ["south"] = "Süden",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Gray"] = "Grau",
                    ["Yellow"] = "Gelb",
                },
            },
            // Which maze did you solve {0} on?
            // Which maze did you solve Blind Maze on?
            [Question.BlindMazeMaze] = new()
            {
                Gender = Gender.Masculine,
                QuestionText = "In welchem Labyrinth wurde {0} gelöst?",
                ModuleName = "Blinder Irrgarten",
            },

            // Blinking Notes
            // What song was flashed in {0}?
            // What song was flashed in Blinking Notes?
            [Question.BlinkingNotesSong] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What song was flashed in {0}?",
            },

            // Blinkstop
            // How many times did the LED flash in {0}?
            // How many times did the LED flash in Blinkstop?
            [Question.BlinkstopNumberOfFlashes] = new()
            {
                QuestionText = "Wie oft hat bei {0} die LED geblinkt?",
            },
            // Which color did the LED flash the fewest times in {0}?
            // Which color did the LED flash the fewest times in Blinkstop?
            [Question.BlinkstopFewestFlashedColor] = new()
            {
                QuestionText = "Welche Farbe hat die LED bei {0} am wenigsten gezeigt?",
                Answers = new Dictionary<string, string>
                {
                    ["Purple"] = "Lila",
                    ["Cyan"] = "Türkis",
                    ["Yellow"] = "Gelb",
                    ["Multicolor"] = "Mehrfarbig",
                },
            },

            // Blockbusters
            // What was the last letter pressed on {0}?
            // What was the last letter pressed on Blockbusters?
            [Question.BlockbustersLastLetter] = new()
            {
                QuestionText = "Welcher Buchstabe wurde bei {0} als letztes eingegeben?",
            },

            // Blue Arrows
            // What were the characters on the screen in {0}?
            // What were the characters on the screen in Blue Arrows?
            [Question.BlueArrowsInitialCharacters] = new()
            {
                QuestionText = "Welche Zeichen waren bei {0} auf dem Bildschirm?",
                ModuleName = "Blaue Pfeile",
            },

            // The Blue Button
            // What was D in {0}?
            // What was D in The Blue Button?
            [Question.BlueButtonD] = new()
            {
                ModuleNameWithThe = "Der Blaue Knopf",
                QuestionText = "Was war D bei {0}?",
                ModuleName = "Blauen Knopf",
            },
            // What was {1} in {0}?
            // What was E in The Blue Button?
            [Question.BlueButtonEFGH] = new()
            {
                ModuleNameWithThe = "Der Blaue Knopf",
                QuestionText = "Was war {1} bei {0}?",
                ModuleName = "Blauen Knopf",
            },
            // What was M in {0}?
            // What was M in The Blue Button?
            [Question.BlueButtonM] = new()
            {
                ModuleNameWithThe = "Der Blaue Knopf",
                QuestionText = "Was war M bei {0}?",
                ModuleName = "Blauen Knopf",
            },
            // What was N in {0}?
            // What was N in The Blue Button?
            [Question.BlueButtonN] = new()
            {
                ModuleNameWithThe = "Der Blaue Knopf",
                QuestionText = "Was war N bei {0}?",
                ModuleName = "Blauen Knopf",
            },
            // What was P in {0}?
            // What was P in The Blue Button?
            [Question.BlueButtonP] = new()
            {
                ModuleNameWithThe = "Der Blaue Knopf",
                QuestionText = "Was war P bei {0}?",
                ModuleName = "Blauen Knopf",
            },
            // What was Q in {0}?
            // What was Q in The Blue Button?
            [Question.BlueButtonQ] = new()
            {
                ModuleNameWithThe = "Der Blaue Knopf",
                QuestionText = "Was war Q bei {0}?",
                ModuleName = "Blauen Knopf",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Cyan"] = "Türkis",
                    ["Red"] = "Rot",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                },
            },
            // What was X in {0}?
            // What was X in The Blue Button?
            [Question.BlueButtonX] = new()
            {
                ModuleNameWithThe = "Der Blaue Knopf",
                QuestionText = "Was war X bei {0}?",
                ModuleName = "Blauen Knopf",
            },

            // Blue Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Blue Cipher?
            [Question.BlueCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameDative = "Blauen Geheimschrift",
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Blaue Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "oberen",
                    ["middle"] = "mittleren",
                    ["bottom"] = "unteren",
                },
            },

            // Bob Barks
            // What was the {1} indicator label in {0}?
            // What was the top left indicator label in Bob Barks?
            [Question.BobBarksIndicators] = new()
            {
                QuestionText = "Welche Beschriftung hatte der {1} Indikator bei {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top left"] = "obere linke",
                    ["top right"] = "obere rechte",
                    ["bottom left"] = "untere linke",
                    ["bottom right"] = "untere rechte",
                },
            },
            // Which button flashed {1} in sequence in {0}?
            // Which button flashed first in sequence in Bob Barks?
            [Question.BobBarksPositions] = new()
            {
                QuestionText = "Welcher Knopf blinkte als {1}er bei {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "oben links",
                    ["top right"] = "oben rechts",
                    ["bottom left"] = "unten links",
                    ["bottom right"] = "unten rechts",
                },
            },

            // Boggle
            // What letter was initially visible on {0}?
            // What letter was initially visible on Boggle?
            [Question.BoggleLetters] = new()
            {
                QuestionText = "Welcher dieser Buchstaben war bei {0} anfänglich sichtbar?",
            },

            // Bomb Diffusal
            // What was the license number in {0}?
            // What was the license number in Bomb Diffusal?
            [Question.BombDiffusalLicenseNumber] = new()
            {
                QuestionText = "Was war bei {0} die Lizenznummer?",
            },

            // Bone Apple Tea
            // Which phrase was shown on {0}?
            // Which phrase was shown on Bone Apple Tea?
            [Question.BoneAppleTeaPhrase] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which phrase was shown on {0}?",
            },

            // Boob Tube
            // Which word was shown on {0}?
            // Which word was shown on Boob Tube?
            [Question.BoobTubeWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which word was shown on {0}?",
            },

            // Book of Mario
            // Who said the {1} quote in {0}?
            // Who said the first quote in Book of Mario?
            [Question.BookOfMarioPictures] = new()
            {
                QuestionText = "Wer sprach bei {0} das {1}e Zitat?",
            },
            // What did {1} say in the {2} stage of {0}?
            // What did Goombell say in the first stage of Book of Mario?
            [Question.BookOfMarioQuotes] = new()
            {
                QuestionText = "Was sagte {1} bei {0} im {2}en Schritt?",
            },

            // Boolean Wires
            // Which operator did you submit in the {1} stage of {0}?
            // Which operator did you submit in the first stage of Boolean Wires?
            [Question.BooleanWiresEnteredOperators] = new()
            {
                QuestionText = "Welcher Operator wurde bei {0} im {1}en Schritt eingegeben?",
                ModuleName = "Boolesche Drähte",
            },

            // Boomtar the Great
            // What was rule {1} in {0}?
            // What was rule one in Boomtar the Great?
            [Question.BoomtarTheGreatRules] = new()
            {
                Gender = Gender.Masculine,
                QuestionText = "Was war bei {0} Regel Nr. {1}?",
                ModuleName = "Boomtar der Große",
                FormatArgs = new Dictionary<string, string>
                {
                    ["one"] = "1",
                    ["two"] = "2",
                },
            },

            // Bordered Keys
            // What was the border color when you pressed the {1} key in {0}?
            // What was the border color when you pressed the first key in Bordered Keys?
            [Question.BorderedKeysBorderColor] = new()
            {
                QuestionText = "Was war beim Druck auf den {1}en Knopf bei {0} die Randfarbe?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                },
            },
            // What was the digit displayed when you pressed the {1} key in {0}?
            // What was the digit displayed when you pressed the first key in Bordered Keys?
            [Question.BorderedKeysDigit] = new()
            {
                QuestionText = "Was war beim Druck auf den {1}en Knopf bei {0} die angezeigte Ziffer?",
            },
            // What was the key color when you pressed the {1} key in {0}?
            // What was the key color when you pressed the first key in Bordered Keys?
            [Question.BorderedKeysKeyColor] = new()
            {
                QuestionText = "Was war beim Druck auf den {1}en Knopf bei {0} die Knopffarbe?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                },
            },
            // What was the label when you pressed the {1} key in {0}?
            // What was the label when you pressed the first key in Bordered Keys?
            [Question.BorderedKeysLabel] = new()
            {
                QuestionText = "Was war beim Druck auf den {1}en Knopf bei {0} die Aufschrift?",
            },
            // What was the label color when you pressed the {1} key in {0}?
            // What was the label color when you pressed the first key in Bordered Keys?
            [Question.BorderedKeysLabelColor] = new()
            {
                QuestionText = "Was war beim Druck auf den {1}en Knopf bei {0} die Aufschriftfarbe?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                },
            },

            // Bottom Gear
            // What tweet was shown in {0}?
            // What tweet was shown in Bottom Gear?
            [Question.BottomGearTweet] = new()
            {
                QuestionText = "Welcher Tweet war bei {0} zu sehen?",
            },

            // Boxing
            // Which {1} appeared on {0}?
            // Which contestant’s first name appeared on Boxing?
            [Question.BoxingNames] = new()
            {
                QuestionText = "Was war bei {0} {1}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["contestant’s first name"] = "der Vorname eines Kandidaten",
                    ["contestant’s last name"] = "der Nachname eines Kandidaten",
                    ["substitute’s first name"] = "der Vorname eines Ersatzmanns",
                    ["substitute’s last name"] = "der Nachname eines Ersatzmanns",
                },
            },
            // What was the {1} of the contestant with strength rating {2} on {0}?
            // What was the first name of the contestant with strength rating 0 on Boxing?
            [Question.BoxingContestantByStrength] = new()
            {
                QuestionText = "Was war bei {0} der {1} des Kandidaten mit Kraftstufe {2}?",
                ModuleName = "Boxen",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first name"] = "Vorname",
                    ["last name"] = "Nachname",
                    ["substitute’s first name"] = "Vorname des Ersatzmanns",
                    ["substitute’s last name"] = "Nachname des Ersatzmanns",
                },
            },
            // What was {1}’s strength rating on {0}?
            // What was Muhammad’s strength rating on Boxing?
            [Question.BoxingStrengthByContestant] = new()
            {
                QuestionText = "Was war bei {0} die Kraftstufe von {1}?",
                ModuleName = "Boxen",
            },

            // Braille
            // What was the {1} pattern in {0}?
            // What was the first pattern in Braille?
            [Question.BraillePattern] = new()
            {
                QuestionText = "Was war bei {0} das {1}e Zeichen?",
                ModuleName = "Blindenschrift",
            },

            // Breakfast Egg
            // Which color appeared on the egg in {0}?
            // Which color appeared on the egg in Breakfast Egg?
            [Question.BreakfastEggColor] = new()
            {
                QuestionText = "Welche Farbe erschien bei {0} auf dem Ei?",
                ModuleName = "Frühstücksei",
                Answers = new Dictionary<string, string>
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

            // Broken Buttons
            // What was the {1} correct button you pressed in {0}?
            // What was the first correct button you pressed in Broken Buttons?
            [Question.BrokenButtons] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Kaputten Knöpfen",
                QuestionText = "Was war bei {0} der als {1}e korrekt gedrückte Knopf?",
                ModuleName = "Kaputte Knöpfe",
            },

            // Broken Guitar Chords
            // What was the displayed chord in {0}?
            // What was the displayed chord in Broken Guitar Chords?
            [Question.BrokenGuitarChordsDisplayedChord] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Kaputten Gitarrenakkorden",
                QuestionText = "Welcher Akkord wurde bei {0} angezeigt?",
                ModuleName = "Kaputte Gitarrenakkorde",
            },
            // In which position, from left to right, was the broken string in {0}?
            // In which position, from left to right, was the broken string in Broken Guitar Chords?
            [Question.BrokenGuitarChordsMutedString] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Kaputten Gitarrenakkorden",
                QuestionText = "An welcher Position, von links nach rechts, war bei {0} die kaputte Saite?",
                ModuleName = "Kaputte Gitarrenakkorde",
            },

            // Brown Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Brown Cipher?
            [Question.BrownCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameDative = "Braunen Geheimschrift",
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Braune Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // Brush Strokes
            // What was the color of the middle contact point in {0}?
            // What was the color of the middle contact point in Brush Strokes?
            [Question.BrushStrokesMiddleColor] = new()
            {
                QuestionText = "Was war bei {0} die Farbe des mittleren Kontaktes?",
                Answers = new Dictionary<string, string>
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
                QuestionText = "Was war bei {0} die {1}e Ziffer auf dem Display?",
            },
            // What was the {1} order number in {0}?
            // What was the first order number in Burger Alarm?
            [Question.BurgerAlarmOrderNumbers] = new()
            {
                QuestionText = "Was war bei {0} die {1} Auftragsnummer?",
            },

            // Burglar Alarm
            // What was the {1} displayed digit in {0}?
            // What was the first displayed digit in Burglar Alarm?
            [Question.BurglarAlarmDigits] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} die {1}e Ziffer auf dem Display?",
                ModuleName = "Alarmanlage",
            },

            // The Button
            // What color did the light glow in {0}?
            // What color did the light glow in The Button?
            [Question.ButtonLightColor] = new()
            {
                Gender = Gender.Masculine,
                ModuleNameWithThe = "Der Knopf",
                QuestionText = "In welcher Farbe leuchtete bei {0} der Streifen?",
                ModuleName = "Knopf",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                    ["white"] = "weiß",
                },
            },

            // Buttonage
            // How many {1} buttons were there on {0}?
            // How many red buttons were there on Buttonage?
            [Question.ButtonageButtons] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many {1} buttons were there on {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "red",
                    ["green"] = "green",
                    ["orange"] = "orange",
                    ["blue"] = "blue",
                    ["pink"] = "pink",
                    ["white"] = "white",
                    ["black"] = "black",
                    ["white-bordered"] = "white-bordered",
                    ["pink-bordered"] = "pink-bordered",
                    ["gray-bordered"] = "gray-bordered",
                    ["red-bordered"] = "red-bordered",
                    ["“P”"] = "“P”",
                    ["special"] = "special",
                },
            },

            // Button Sequence
            // How many of the buttons in {0} were {1}?
            // How many of the buttons in Button Sequence were red?
            [Question.ButtonSequencesColorOccurrences] = new()
            {
                Gender = Gender.Plural,
                QuestionText = "Wie viele der Knöpfe bei {0} waren {1}?",
                ModuleName = "Knopffolgen",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                    ["white"] = "weiß",
                },
            },

            // Cacti’s Conundrum
            // What color was the LED in the {1} stage of {0}?
            // What color was the LED in the first stage of Cacti’s Conundrum?
            [Question.CactisConundrumColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the LED in the {1} stage of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blue",
                    ["Lime"] = "Lime",
                    ["Orange"] = "Orange",
                    ["Red"] = "Red",
                },
            },

            // Caesar Cycle
            // What was the {1} in {0}?
            // What was the message in Caesar Cycle?
            [Question.CaesarCycleWord] = new()
            {
                QuestionText = "Was war die {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Nachricht",
                    ["response"] = "Antwort",
                },
            },

            // Caesar Psycho
            // What text was on the top display in the {1} stage of {0}?
            // What text was on the top display in the first stage of Caesar Psycho?
            [Question.CaesarPsychoScreenTexts] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What text was on the top display in the {1} stage of {0}?",
            },
            // What color was the text on the top display in the second stage of {0}?
            // What color was the text on the top display in the second stage of Caesar Psycho?
            [Question.CaesarPsychoScreenColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the text on the top display in the second stage of {0}?",
            },

            // Calendar
            // What was the LED color in {0}?
            // What was the LED color in Calendar?
            [Question.CalendarLedColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the LED color in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                },
            },

            // CA-RPS
            // What color was this cell initially in {0}?
            // What color was this cell initially in CA-RPS?
            [Question.CARPSCell] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was this cell initially in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What color was the {1} button in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["up"] = "up",
                    ["right"] = "right",
                    ["down"] = "down",
                    ["left"] = "left",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                },
            },
            // What lyric was played by the {1} button in {0}?
            // What lyric was played by the up button in Cartinese?
            [Question.CartineseLyrics] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What lyric was played by the {1} button in {0}?",
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
            [Question.CatchphraseColour] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the colour of the {1} panel in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-right"] = "top-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Orange"] = "Orange",
                    ["Purple"] = "Lila",
                    ["Yellow"] = "Gelb",
                },
            },

            // Challenge & Contact
            // What was the {1} submitted answer in {0}?
            // What was the first submitted answer in Challenge & Contact?
            [Question.ChallengeAndContactAnswers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} submitted answer in {0}?",
            },

            // Character Codes
            // What was the {1} character in {0}?
            // What was the first character in Character Codes?
            [Question.CharacterCodesCharacter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} character in {0}?",
            },

            // Character Shift
            // Which letter was present but not submitted on the left slider of {0}?
            // Which letter was present but not submitted on the left slider of Character Shift?
            [Question.CharacterShiftLetters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which letter was present but not submitted on the left slider of {0}?",
            },
            // Which digit was present but not submitted on the right slider of {0}?
            // Which digit was present but not submitted on the right slider of Character Shift?
            [Question.CharacterShiftDigits] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which digit was present but not submitted on the right slider of {0}?",
            },

            // Character Slots
            // Who was displayed in the {1} slot in the {2} stage of {0}?
            // Who was displayed in the first slot in the first stage of Character Slots?
            [Question.CharacterSlotsDisplayedCharacters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Who was displayed in the {1} slot in the {2} stage of {0}?",
            },

            // Cheap Checkout
            // What was {1} in {0}?
            // What was the paid amount in Cheap Checkout?
            [Question.CheapCheckoutPaid] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1}paid amount in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["the paid amount"] = "the paid amount",
                    ["the first paid amount"] = "the first paid amount",
                    ["the second paid amount"] = "the second paid amount",
                },
            },

            // Cheep Checkout
            // Which bird {1} present in {0}?
            // Which bird was present in Cheep Checkout?
            [Question.CheepCheckoutBirds] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which bird {1} present in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "was",
                    ["was not"] = "was not",
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
                NeedsTranslation = true,
                QuestionText = "What was the {1} coordinate in {0}?",
            },

            // Chinese Counting
            // What color was the {1} LED in {0}?
            // What color was the left LED in Chinese Counting?
            [Question.ChineseCountingLED] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} LED in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "left",
                    ["right"] = "right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Weiß",
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Orange"] = "Orange",
                },
            },

            // Chinese Remainder Theorem
            // Which equation was used in {0}?
            // Which equation was used in Chinese Remainder Theorem?
            [Question.ChineseRemainderTheoremEquations] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which equation was used in {0}?",
            },

            // Chord Qualities
            // Which note was part of the given chord in {0}?
            // Which note was part of the given chord in Chord Qualities?
            [Question.ChordQualitiesNotes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which note was part of the given chord in {0}?",
            },

            // ↻↺
            // Which arrow was shown in {0}?
            // Which arrow was shown in ↻↺?
            [Question.ClockCounterArrows] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which arrow was shown in {0}?",
            },

            // The Code
            // What was the displayed number in {0}?
            // What was the displayed number in The Code?
            [Question.CodeDisplayNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed number in {0}?",
            },

            // Codenames
            // Which of these words was submitted in {0}?
            // Which of these words was submitted in Codenames?
            [Question.CodenamesAnswers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these words was submitted in {0}?",
            },

            // Coffee Beans
            // What was the {1} movement in {0}?
            // What was the first movement in Coffee Beans?
            [Question.CoffeeBeansMovements] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} movement in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Horizontal"] = "Horizontal",
                    ["Vertical"] = "Vertical",
                    ["Diagonal"] = "Diagonal",
                    ["Nothing"] = "Nothing",
                },
            },

            // Coffeebucks
            // What was the last served coffee in {0}?
            // What was the last served coffee in Coffeebucks?
            [Question.CoffeebucksCoffee] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the last served coffee in {0}?",
            },

            // Coinage
            // Which coin was flipped in {0}?
            // Which coin was flipped in Coinage?
            [Question.CoinageFlip] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which coin was flipped in {0}?",
            },

            // Color Addition
            // What was {1}’s number in {0}?
            // What was red’s number in Color Addition?
            [Question.ColorAdditionNumbers] = new()
            {
                QuestionText = "Was war bei {0} die Zahl im {1}-Kanal?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "Rot",
                    ["green"] = "Grün",
                    ["blue"] = "Blau",
                },
            },

            // Color Braille
            // What color was this dot in {0}?
            // What color was this dot in Color Braille?
            [Question.ColorBrailleColor] = new()
            {
                QuestionText = "Welche Farbe hatte dieser Punkt bei {0}?",
                ModuleName = "Farbbraille",
                Answers = new Dictionary<string, string>
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

            // Color Decoding
            // What was the {1}-stage indicator pattern in {0}?
            // What was the first-stage indicator pattern in Color Decoding?
            [Question.ColorDecodingIndicatorPattern] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1}-stage indicator pattern in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Checkered"] = "Checkered",
                    ["Horizontal"] = "Horizontal",
                    ["Vertical"] = "Vertical",
                    ["Solid"] = "Solid",
                },
            },
            // Which color {1} in the {2}-stage indicator pattern in {0}?
            // Which color appeared in the first-stage indicator pattern in Color Decoding?
            [Question.ColorDecodingIndicatorColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color {1} in the {2}-stage indicator pattern in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["appeared"] = "appeared",
                    ["did not appear"] = "did not appear",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "Grün",
                    ["Purple"] = "Lila",
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Yellow"] = "Gelb",
                },
            },

            // Colored Keys
            // What was the displayed word in {0}?
            // What was the displayed word in Colored Keys?
            [Question.ColoredKeysDisplayWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed word in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["yellow"] = "gelb",
                    ["purple"] = "lila",
                    ["white"] = "weiß",
                },
            },
            // What was the displayed word’s color in {0}?
            // What was the displayed word’s color in Colored Keys?
            [Question.ColoredKeysDisplayWordColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed word’s color in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["yellow"] = "gelb",
                    ["purple"] = "lila",
                    ["white"] = "weiß",
                },
            },
            // What was the color of the {1} key in {0}?
            // What was the color of the top-left key in Colored Keys?
            [Question.ColoredKeysKeyColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} key in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-right"] = "top-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["yellow"] = "gelb",
                    ["purple"] = "lila",
                    ["white"] = "weiß",
                },
            },
            // What letter was on the {1} key in {0}?
            // What letter was on the top-left key in Colored Keys?
            [Question.ColoredKeysKeyLetter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What letter was on the {1} key in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-right"] = "top-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
            },

            // Colored Squares
            // What was the first color group in {0}?
            // What was the first color group in Colored Squares?
            [Question.ColoredSquaresFirstGroup] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the first color group in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Weiß",
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Magenta"] = "Magenta",
                },
            },

            // Colored Switches
            // What was the initial position of the switches in {0}?
            // What was the initial position of the switches in Colored Switches?
            [Question.ColoredSwitchesInitialPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial position of the switches in {0}?",
            },
            // What was the position of the switches when the LEDs came on in {0}?
            // What was the position of the switches when the LEDs came on in Colored Switches?
            [Question.ColoredSwitchesWhenLEDsCameOn] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the position of the switches when the LEDs came on in {0}?",
            },

            // Color Morse
            // What was the color of the {1} LED in {0}?
            // What was the color of the first LED in Color Morse?
            [Question.ColorMorseColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} LED in {0}?",
                Answers = new Dictionary<string, string>
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
            // What character was flashed by the {1} LED in {0}?
            // What character was flashed by the first LED in Color Morse?
            [Question.ColorMorseCharacter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What character was flashed by the {1} LED in {0}?",
            },

            // Color One Two
            // What color was the {1} LED in {0}?
            // What color was the left LED in Color One Two?
            [Question.ColorOneTwoColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} LED in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "left",
                    ["right"] = "right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Blue"] = "Blue",
                    ["Green"] = "Green",
                    ["Yellow"] = "Yellow",
                },
            },

            // Colors Maximization
            // How many buttons were {1} in {0}?
            // How many buttons were red in Colors Maximization?
            [Question.ColorsMaximizationColorCount] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many buttons were {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                },
            },

            // Coloured Cubes
            // What was the colour of this {1} in the {2} stage of {0}?
            // What was the colour of this cube in the first stage of Coloured Cubes?
            [Question.ColouredCubesColours] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the colour of this {1} in the {2} stage of {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["cube"] = "cube",
                    ["stage light"] = "stage light",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Black"] = "Schwarz",
                    ["Indigo"] = "Indigo",
                    ["Blue"] = "Blau",
                    ["Forest"] = "Forest",
                    ["Teal"] = "Teal",
                    ["Azure"] = "Azure",
                    ["Green"] = "Grün",
                    ["Jade"] = "Jade",
                    ["Cyan"] = "Türkis",
                    ["Maroon"] = "Kastanie",
                    ["Plum"] = "Plum",
                    ["Violet"] = "Violet",
                    ["Olive"] = "Olive",
                    ["Grey"] = "Grey",
                    ["Maya"] = "Maya",
                    ["Lime"] = "Lime",
                    ["Mint"] = "Mint",
                    ["Aqua"] = "Aqua",
                    ["Red"] = "Rot",
                    ["Rose"] = "Rose",
                    ["Magenta"] = "Magenta",
                    ["Orange"] = "Orange",
                    ["Salmon"] = "Salmon",
                    ["Pink"] = "Pink",
                    ["Yellow"] = "Gelb",
                    ["Cream"] = "Cream",
                    ["White"] = "Weiß",
                },
            },

            // Coloured Cylinder
            // What was the {1} colour flashed on the cylinder in {0}?
            // What was the first colour flashed on the cylinder in Coloured Cylinder?
            [Question.ColouredCylinderColours] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} colour flashed on the cylinder in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What was the color of the last word in the sequence in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Magenta"] = "Magenta",
                    ["White"] = "Weiß",
                },
            },

            // Concentration
            // What number began here in {0}?
            // What number began here in Concentration?
            [Question.ConcentrationStartingDigit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What number began here in {0}?",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the Concentration which began with {1} in the {0} position (in reading order)"] = "the Concentration which began with {1} in the {0} position (in reading order)",
                },
            },

            // Conditional Buttons
            // What was the color of this button in {0}?
            // What was the color of this button in Conditional Buttons?
            [Question.ConditionalButtonsColors] = new()
            {
                NeedsTranslation = true,
                Gender = Gender.Plural,
                ModuleNameDative = "Bedingten Knöpfen",
                QuestionText = "Was war bei {0} die Farbe von diesem Knopf?",
                ModuleName = "Bedingte Knöpfe",
                Answers = new Dictionary<string, string>
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

            // Connected Monitors
            // What number was initially displayed on this screen in {0}?
            // What number was initially displayed on this screen in Connected Monitors?
            [Question.ConnectedMonitorsNumber] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Verbundenen Monitoren",
                QuestionText = "Welche Zahl war bei {0} anfänglich auf diesem Display zu sehen?",
                ModuleName = "Verbundene Monitore",
            },
            // What colour was the indicator on this screen in {0}?
            // What colour was the indicator on this screen in Connected Monitors?
            [Question.ConnectedMonitorsSingleIndicator] = new()
            {
                NeedsTranslation = true,
                Gender = Gender.Plural,
                ModuleNameDative = "Verbundenen Monitoren",
                QuestionText = "Welche Farbe hatte bei {0} der Indikator auf diesem Display?",
                ModuleName = "Verbundene Monitore",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Orange"] = "Orange",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Purple"] = "Lila",
                    ["White"] = "Weiß",
                },
            },
            // What colour was the {1} indicator on this screen in {0}?
            // What colour was the first indicator on this screen in Connected Monitors?
            [Question.ConnectedMonitorsOrdinalIndicator] = new()
            {
                NeedsTranslation = true,
                Gender = Gender.Plural,
                ModuleNameDative = "Verbundenen Monitoren",
                QuestionText = "Welche Farbe hatte bei {0} der {1}e Indikator auf diesem Display?",
                ModuleName = "Verbundene Monitore",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Orange"] = "Orange",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Purple"] = "Lila",
                    ["White"] = "Weiß",
                },
            },

            // Connection Check
            // What pair of numbers was present in {0}?
            // What pair of numbers was present in Connection Check?
            [Question.ConnectionCheckNumbers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What pair of numbers was present in {0}?",
            },

            // Coordinates
            // What was the solution you selected first in {0}?
            // What was the solution you selected first in Coordinates?
            [Question.CoordinatesFirstSolution] = new()
            {
                Gender = Gender.Plural,
                QuestionText = "Welche Lösung wurde bei {0} als erstes gewählt?",
                ModuleName = "Koordinaten",
            },
            // What was the grid size in {0}?
            // What was the grid size in Coordinates?
            [Question.CoordinatesSize] = new()
            {
                Gender = Gender.Plural,
                QuestionText = "Was war bei {0} die Rastergröße?",
                ModuleName = "Koordinaten",
            },

            // Coral Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Coral Cipher?
            [Question.CoralCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Korall-Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "oberen",
                    ["middle"] = "mittleren",
                    ["bottom"] = "unteren",
                },
            },

            // Corners
            // What was the color of the {1} corner in {0}?
            // What was the color of the top-left corner in Corners?
            [Question.CornersColors] = new()
            {
                QuestionText = "What was the color of the {1} corner in {0}?",
                ModuleName = "Ecken",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-right"] = "top-right",
                    ["bottom-right"] = "bottom-right",
                    ["bottom-left"] = "bottom-left",
                },
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                },
            },
            // How many corners in {0} were {1}?
            // How many corners in Corners were red?
            [Question.CornersColorCount] = new()
            {
                QuestionText = "How many corners in {0} were {1}?",
                ModuleName = "Ecken",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                },
            },

            // Cornflower Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Cornflower Cipher?
            [Question.CornflowerCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Kornblumen-Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "oberen",
                    ["middle"] = "mittleren",
                    ["bottom"] = "unteren",
                },
            },

            // Cosmic
            // What was the number initially shown in {0}?
            // What was the number initially shown in Cosmic?
            [Question.CosmicNumber] = new()
            {
                QuestionText = "What was the number initially shown in {0}?",
                ModuleName = "Kosmik",
            },

            // Crazy Hamburger
            // What was the {1} ingredient shown in {0}?
            // What was the first ingredient shown in Crazy Hamburger?
            [Question.CrazyHamburgerIngredient] = new()
            {
                Gender = Gender.Masculine,
                ModuleNameDative = "Verrückten Hamburger",
                QuestionText = "Was war bei {0} die {1}e Zutat?",
                ModuleName = "Verrückter Hamburger",
            },

            // Crazy Maze
            // What was the {1} location in {0}?
            // What was the starting location in Crazy Maze?
            [Question.CrazyMazeStartOrGoal] = new()
            {
                Gender = Gender.Masculine,
                ModuleNameDative = "Verrückten Irrgarten",
                QuestionText = "Was war bei {0} die {1}?",
                ModuleName = "Verrückter Irrgarten",
                FormatArgs = new Dictionary<string, string>
                {
                    ["starting"] = "Anfangsposition",
                    ["goal"] = "Zielposition",
                },
            },

            // Cream Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Cream Cipher?
            [Question.CreamCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Krem-Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "oberen",
                    ["middle"] = "mittleren",
                    ["bottom"] = "unteren",
                },
            },

            // Creation
            // What were the weather conditions on the {1} day in {0}?
            // What were the weather conditions on the first day in Creation?
            [Question.CreationWeather] = new()
            {
                QuestionText = "Was waren die Wetterbedingungen am {1} Tag in {0}?",
                ModuleName = "Schöpfung",
                Answers = new Dictionary<string, string>
                {
                    ["Clear"] = "Wolkenlos",
                    ["Heat Wave"] = "Hitzewelle",
                    ["Meteor Shower"] = "Meteorschauer",
                    ["Rain"] = "Regen",
                    ["Windy"] = "Wind",
                },
            },

            // Crimson Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Crimson Cipher?
            [Question.CrimsonCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Karmin-Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "oberen",
                    ["middle"] = "mittleren",
                    ["bottom"] = "unteren",
                },
            },

            // Critters
            // What was the color in {0}?
            // What was the color in Critters?
            [Question.CrittersColor] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Kriechtieren",
                QuestionText = "Was war die Farbe bei {0}?",
                ModuleName = "Kriechtiere",
                Answers = new Dictionary<string, string>
                {
                    ["Yellow"] = "Gelb",
                    ["Pink"] = "Pink",
                    ["Blue"] = "Blau",
                    ["White"] = "Weiß",
                },
            },

            // Cruel Binary
            // What was the displayed word in {0}?
            // What was the displayed word in Cruel Binary?
            [Question.CruelBinaryDisplayedWord] = new()
            {
                ModuleNameDative = "Höllischen Binärsystem",
                QuestionText = "Was war bei {0} das Wort auf dem Display?",
                ModuleName = "Höllisches Binärsystem",
            },

            // Cruel Keypads
            // Which of these characters appeared in the {1} stage of {0}?
            // Which of these characters appeared in the first stage of Cruel Keypads?
            [Question.CruelKeypadsDisplayedSymbols] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Höllischen Tastenfelder",
                QuestionText = "Welches dieser Zeichen war bei {0} in der ersten Stufe zu sehen?",
                ModuleName = "Höllische Tastenfelder",
            },
            // What was the color of the bar in the {1} stage of {0}?
            // What was the color of the bar in the first stage of Cruel Keypads?
            [Question.CruelKeypadsColors] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Höllischen Tastenfelder",
                QuestionText = "Was war bei {0} die Farbe des Balkens in der {1}en Stufe?",
                ModuleName = "Höllische Tastenfelder",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Magenta"] = "Magenta",
                    ["White"] = "Weiß",
                },
            },

            // The cRule
            // Which cell was pre-filled at the start of {0}?
            // Which cell was pre-filled at the start of The cRule?
            [Question.CRulePrefilled] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameWithThe = "Die CRegel",
                QuestionText = "Welche Zelle war bei {0} am Anfang vorgegeben?",
                ModuleName = "CRegel",
            },
            // Which symbol pair was here in {0}?
            // Which symbol pair was here in The cRule?
            [Question.CRuleSymbolPair] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameWithThe = "Die CRegel",
                QuestionText = "Welches Symbolpaar war bei {0} an dieser Stelle?",
                ModuleName = "CRegel",
            },
            // Which symbol pair was present on {0}?
            // Which symbol pair was present on The cRule?
            [Question.CRuleSymbolPairPresent] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameWithThe = "Die CRegel",
                QuestionText = "Welches Symbolpaar kam bei {0} vor?",
                ModuleName = "CRegel",
            },
            // Where was {1} in {0}?
            // Where was ♤♤ in The cRule?
            [Question.CRuleSymbolPairCell] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameWithThe = "Die CRegel",
                QuestionText = "Wo war {1} bei {0}?",
                ModuleName = "CRegel",
            },

            // Cryptic Cycle
            // What was the {1} in {0}?
            // What was the message in Cryptic Cycle?
            [Question.CrypticCycleWord] = new()
            {
                QuestionText = "Was war bei {0} die {1}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Nachricht",
                    ["response"] = "Antwort",
                },
            },

            // Cryptic Keypad
            // What was the label of the {1} key in {0}?
            // What was the label of the top-left key in Cryptic Keypad?
            [Question.CrypticKeypadLabels] = new()
            {
                ModuleNameDative = "Kryptischen Tastenfeld",
                QuestionText = "Was war bei {0} die Aufschrift auf der {1} Tasten?",
                ModuleName = "Kryptisches Tastenfeld",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "oben-linken",
                    ["top-right"] = "oben-rechten",
                    ["bottom-left"] = "unten-linken",
                    ["bottom-right"] = "unten-rechten",
                },
            },
            // Which cardinal direction was the {1} key rotated to in {0}?
            // Which cardinal direction was the top-left key rotated to in Cryptic Keypad?
            [Question.CrypticKeypadRotations] = new()
            {
                ModuleNameDative = "Kryptischen Tastenfeld",
                QuestionText = "In welche Himmelsrichtung wurde bei {0} die {1} Taste gedreht?",
                ModuleName = "Kryptisches Tastenfeld",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "obere linke",
                    ["top-right"] = "obere rechte",
                    ["bottom-left"] = "untere linke",
                    ["bottom-right"] = "untere rechte",
                },
                Answers = new Dictionary<string, string>
                {
                    ["North"] = "Norden",
                    ["East"] = "Osten",
                    ["South"] = "Süden",
                    ["West"] = "Westen",
                },
            },

            // The Cube
            // What was the {1} cube rotation in {0}?
            // What was the first cube rotation in The Cube?
            [Question.CubeRotations] = new()
            {
                Gender = Gender.Masculine,
                ModuleNameWithThe = "Der Würfel",
                QuestionText = "Was war bei {0} die {1}e Rotation?",
                ModuleName = "Würfel",
                Answers = new Dictionary<string, string>
                {
                    ["rotate cw"] = "im Uhrzeigersinn",
                    ["tip left"] = "nach links gekippt",
                    ["tip backwards"] = "nach hinten gekippt",
                    ["rotate ccw"] = "gegen den Uhrzeigersinn",
                    ["tip right"] = "nach rechts gekippt",
                    ["tip forwards"] = "nach vorne gekippt",
                },
            },

            // Cursed Double-Oh
            // What was the first digit of the initially displayed number in {0}?
            // What was the first digit of the initially displayed number in Cursed Double-Oh?
            [Question.CursedDoubleOhInitialPosition] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameDative = "Verflüchten Doppel-Null",
                QuestionText = "Was war bei {0} am Anfang die erste Ziffer auf dem Display?",
                ModuleName = "Verfluchte Doppel-Null",
            },

            // Customer Identification
            // Who was the {1} customer in {0}?
            // Who was the first customer in Customer Identification?
            [Question.CustomerIdentificationCustomer] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Wer war bei {0} der {1}e Kunde?",
                ModuleName = "Kundenidentifikation",
            },

            // The Cyan Button
            // Where was the button at the {1} stage in {0}?
            // Where was the button at the first stage in The Cyan Button?
            [Question.CyanButtonPositions] = new()
            {
                Gender = Gender.Masculine,
                ModuleNameDative = "Türkisen Knopf",
                ModuleNameWithThe = "Der Türkise Knopf",
                QuestionText = "Wo war bei {0} der Knopf in der {1}en Stufe?",
                ModuleName = "Türkiser Knopf",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "oben links",
                    ["top middle"] = "oben Mitte",
                    ["top right"] = "oben rechts",
                    ["bottom left"] = "unten links",
                    ["bottom middle"] = "unten Mitte",
                    ["bottom right"] = "unten rechts",
                },
            },

            // DACH Maze
            // Which region did you depart from in {0}?
            // Which region did you depart from in DACH Maze?
            [Question.DACHMazeOrigin] = new()
            {
                Gender = Gender.Masculine,
                QuestionText = "Wo ging’s bei {0} los?",
                ModuleName = "DACH-Irrgarten",
                Answers = new Dictionary<string, string>
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

            // Deaf Alley
            // What was the shape generated in {0}?
            // What was the shape generated in Deaf Alley?
            [Question.DeafAlleyShape] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameDative = "Tauben Gasse",
                QuestionText = "Was war bei {0} die generierte Form?",
                ModuleName = "Taube Gasse",
            },

            // The Deck of Many Things
            // What deck did the first card of {0} belong to?
            // What deck did the first card of The Deck of Many Things belong to?
            [Question.DeckOfManyThingsFirstCard] = new()
            {
                Gender = Gender.Masculine,
                ModuleNameWithThe = "Der Stapel Vieler Dinge",
                QuestionText = "Zu welchem Deck gehörte bei {0} die erste Karte?",
                ModuleName = "Stapel Vieler Dinge",
            },

            // Decolored Squares
            // What was the starting {1} defining color in {0}?
            // What was the starting column defining color in Decolored Squares?
            [Question.DecoloredSquaresStartingPos] = new()
            {
                QuestionText = "Welche Farbe hat bei {0} die {1} bestimmt?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["column"] = "Spalte",
                    ["row"] = "Reihe",
                },
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Weiß",
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Magenta"] = "Magenta",
                },
            },

            // Decolour Flash
            // What was the {1} of the {2} goal in {0}?
            // What was the colour of the first goal in Decolour Flash?
            [Question.DecolourFlashGoal] = new()
            {
                QuestionText = "Was war bei {0} {1} vom {2}en Ziel?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["colour"] = "die Farbe",
                    ["word"] = "das Wort",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Red"] = "Rot",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                    ["White"] = "Weiß",
                },
            },

            // Denial Displays
            // What number was initially shown on display {1} in {0}?
            // What number was initially shown on display A in Denial Displays?
            [Question.DenialDisplaysDisplays] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What number was initially shown on display {1} in {0}?",
            },

            // DetoNATO
            // What was the {1} display in {0}?
            // What was the first display in DetoNATO?
            [Question.DetoNATODisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} display in {0}?",
            },

            // Devilish Eggs
            // What was the {1} egg’s {2} rotation in {0}?
            // What was the top egg’s first rotation in Devilish Eggs?
            [Question.DevilishEggsRotations] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} egg’s {2} rotation in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["bottom"] = "bottom",
                },
            },
            // What was the {1} digit in the string of numbers on {0}?
            // What was the first digit in the string of numbers on Devilish Eggs?
            [Question.DevilishEggsNumbers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} digit in the string of numbers on {0}?",
            },
            // What was the {1} letter in the string of letters on {0}?
            // What was the first letter in the string of letters on Devilish Eggs?
            [Question.DevilishEggsLetters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} letter in the string of letters on {0}?",
            },

            // Dialtones
            // What dialtones were heard in {0}?
            // What dialtones were heard in Dialtones?
            [Question.DialtonesDialtones] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What dialtones were heard in {0}?",
            },

            // Digisibility
            // What was the number on the {1} button in {0}?
            // What was the number on the first button in Digisibility?
            [Question.DigisibilityDisplayedNumber] = new()
            {
                QuestionText = "Welche Zahl war bei {0} auf dem {1}en Knopf?",
            },

            // Digit String
            // What was the initial number in {0}?
            // What was the initial number in Digit String?
            [Question.DigitStringInitialNumber] = new()
            {
                QuestionText = "Was war bei {0} die Anfangszahl?",
            },

            // Dimension Disruption
            // Which of these was a visible character in {0}?
            // Which of these was a visible character in Dimension Disruption?
            [Question.DimensionDisruptionVisibleLetters] = new()
            {
                QuestionText = "Welches dieser Zeichen war bei {0} zu sehen?",
                ModuleName = "Dimensionsspaltung",
            },

            // Directional Button
            // How many times did you press the button in the {1} stage of {0}?
            // How many times did you press the button in the first stage of Directional Button?
            [Question.DirectionalButtonButtonCount] = new()
            {
                QuestionText = "Wie oft wurde bei {0} in der {1}en Phase der Knopf gedrückt?",
                ModuleName = "Richtungsknopf",
            },

            // Discolored Squares
            // What was {1}’s remembered position in {0}?
            // What was Blue’s remembered position in Discolored Squares?
            [Question.DiscoloredSquaresRememberedPositions] = new()
            {
                QuestionText = "Was war bei {0} die notierte Position von {1}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Blue"] = "Blau",
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Magenta"] = "Magenta",
                },
            },

            // Disordered Keys
            // What was the missing information for the {1} key in {0}?
            // What was the missing information for the first key in Disordered Keys?
            [Question.DisorderedKeysMissingInfo] = new()
            {
                QuestionText = "Welche Information fehlte bei {0} auf dem {1}en Knopf?",
                Answers = new Dictionary<string, string>
                {
                    ["Key color"] = "Knopffarbe",
                    ["Label color"] = "Aufschriftfarbe",
                    ["Label"] = "Aufschrift",
                },
            },
            // What was the revealed key color for the {1} key in {0}?
            // What was the revealed key color for the first key in Disordered Keys?
            [Question.DisorderedKeysRevealedKeyColor] = new()
            {
                QuestionText = "Was war bei {0} die aufgedeckte Knopffarbe des {1}en Knopfes?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                },
            },
            // What was the revealed label for the {1} key in {0}?
            // What was the revealed label for the first key in Disordered Keys?
            [Question.DisorderedKeysRevealedLabel] = new()
            {
                QuestionText = "Was war bei {0} die aufgedeckte Aufschrift des {1}en Knopfes?",
            },
            // What was the revealed label color for the {1} key in {0}?
            // What was the revealed label color for the first key in Disordered Keys?
            [Question.DisorderedKeysRevealedLabelColor] = new()
            {
                QuestionText = "Was war bei {0} die aufgedeckte Aufschriftfarbe des {1}en Knopfes?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                },
            },
            // What was the unrevealed key color for the {1} key in {0}?
            // What was the unrevealed key color for the first key in Disordered Keys?
            [Question.DisorderedKeysUnrevealedKeyColor] = new()
            {
                QuestionText = "Was war bei {0} die unaufgedeckte Knopffarbe des {1}en Knopfes?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                },
            },
            // What was the unrevealed label for the {1} key in {0}?
            // What was the unrevealed label for the first key in Disordered Keys?
            [Question.DisorderedKeysUnrevealedKeyLabel] = new()
            {
                QuestionText = "Was war bei {0} die unaufgedeckte Aufschrift des {1}en Knopfes?",
            },
            // What was the unrevealed label color for the {1} key in {0}?
            // What was the unrevealed label color for the first key in Disordered Keys?
            [Question.DisorderedKeysUnrevealedLabelColor] = new()
            {
                QuestionText = "Was war bei {0} die unaufgedeckte Aufschriftfarbe des {1}en Knopfes?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                },
            },

            // Divisible Numbers
            // What was the {1} stage’s number in {0}?
            // What was the first stage’s number in Divisible Numbers?
            [Question.DivisibleNumbersNumbers] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Teilbaren Zahlen",
                QuestionText = "Was war bei {0} die Zahl in der {1}en Phase?",
                ModuleName = "Teilbare Zahlen",
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
                Gender = Gender.Plural,
                ModuleNameDative = "Doppelpfeilen",
                QuestionText = "Was war bei {0} die Anfangsposition?",
                ModuleName = "Doppelpfeile",
            },
            // Which {1} arrow moved {2} in the grid in {0}?
            // Which inner arrow moved up in the grid in Double Arrows?
            [Question.DoubleArrowsArrow] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Doppelpfeilen",
                QuestionText = "Welcher {1} Pfeil ging bei {0} nach {2}?",
                ModuleName = "Doppelpfeile",
                FormatArgs = new Dictionary<string, string>
                {
                    ["inner"] = "innere",
                    ["up"] = "oben",
                    ["outer"] = "äußere",
                    ["down"] = "unten",
                    ["left"] = "links",
                    ["right"] = "rechts",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Hoch",
                    ["Right"] = "Rechts",
                    ["Left"] = "Links",
                    ["Down"] = "Runter",
                },
            },
            // Which direction in the grid did the {1} arrow move in {0}?
            // Which direction in the grid did the inner up arrow move in Double Arrows?
            [Question.DoubleArrowsMovement] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Doppelpfeilen",
                QuestionText = "Welche Bewegungsrichtung im Raster bewirkte bei {0} der {1}?",
                ModuleName = "Doppelpfeile",
                FormatArgs = new Dictionary<string, string>
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
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Hoch",
                    ["Right"] = "Rechts",
                    ["Left"] = "Links",
                    ["Down"] = "Runter",
                },
            },

            // Double Color
            // What was the screen color on the {1} stage of {0}?
            // What was the screen color on the first stage of Double Color?
            [Question.DoubleColorColors] = new()
            {
                Gender = Gender.Plural,
                QuestionText = "Was war bei {0} die Farbe des Displays in der {1}en Stufe?",
                ModuleName = "Doppelfarben",
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Red"] = "Rot",
                    ["Pink"] = "Pink",
                    ["Yellow"] = "Gelb",
                },
            },

            // Double Digits
            // What was the digit on the {1} display in {0}?
            // What was the digit on the left display in Double Digits?
            [Question.DoubleDigitsDisplays] = new()
            {
                Gender = Gender.Plural,
                QuestionText = "Was war bei {0} die Ziffer auf dem {1} Display?",
                ModuleName = "Doppelziffern",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "linken",
                    ["right"] = "rechten",
                },
            },

            // Double Expert
            // What was the starting key number in {0}?
            // What was the starting key number in Double Expert?
            [Question.DoubleExpertStartingKeyNumber] = new()
            {
                Gender = Gender.Masculine,
                ModuleNameDative = "Doppelexperten",
                QuestionText = "Was war bei {0} die anfängliche Schlüsselzahl?",
                ModuleName = "Doppelexperte",
            },
            // What was the word you submitted in {0}?
            // What was the word you submitted in Double Expert?
            [Question.DoubleExpertSubmittedWord] = new()
            {
                Gender = Gender.Masculine,
                ModuleNameDative = "Doppelexperten",
                QuestionText = "Was war bei {0} das eingegebene Wort?",
                ModuleName = "Doppelexperte",
            },

            // Double Listening
            // What clip was played in {0}?
            // What clip was played in Double Listening?
            [Question.DoubleListeningSounds] = new()
            {
                QuestionText = "Welcher dieser Clips kam bei {0} vor?",
                ModuleName = "Doppelt Zuhören",
            },

            // Double-Oh
            // Which button was the submit button in {0}?
            // Which button was the submit button in Double-Oh?
            [Question.DoubleOhSubmitButton] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Welcher Knopf war bei {0} der Eingabeknopf?",
                ModuleName = "Doppel-Null",
            },

            // Double Screen
            // What color was the {1} screen in the {2} stage of {0}?
            // What color was the top screen in the first stage of Double Screen?
            [Question.DoubleScreenColors] = new()
            {
                QuestionText = "Welche Farbe war bei {0} auf dem {1} Display in der {2}en Stufe?",
                ModuleName = "Doppel-Display",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "oberen",
                    ["bottom"] = "unteren",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                },
            },

            // Dr. Doctor
            // Which of these symptoms was listed on {0}?
            // Which of these symptoms was listed on Dr. Doctor?
            [Question.DrDoctorSymptoms] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Welches dieser Symptome war bei {0} aufgelistet?",
                ModuleName = "Arztpraxis",
            },
            // Which of these diseases was listed on {0}, but not the one treated?
            // Which of these diseases was listed on Dr. Doctor, but not the one treated?
            [Question.DrDoctorDiseases] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Welche dieser Krankheiten war bei {0} aufgelistet, aber nicht die behandelte?",
                ModuleName = "Arztpraxis",
            },

            // Dreamcipher
            // What was the decrypted word in {0}?
            // What was the decrypted word in Dreamcipher?
            [Question.DreamcipherWord] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} das entschlüsselte Wort?",
                ModuleName = "Traumschrift",
            },

            // The Duck
            // What was the color of the curtain in {0}?
            // What was the color of the curtain in The Duck?
            [Question.DuckCurtainColor] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameWithThe = "Die Ente",
                QuestionText = "Welche Farbe hatte bei {0} der Vorhang?",
                ModuleName = "Ente",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                    ["green"] = "grün",
                    ["orange"] = "orange",
                    ["red"] = "rot",
                },
            },

            // Dumb Waiters
            // Which player {1} present in {0}?
            // Which player was present in Dumb Waiters?
            [Question.DumbWaitersPlayerAvailable] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which player {1} present in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "was",
                    ["was not"] = "was not",
                },
            },

            // Earthbound
            // What was the background in {0}?
            // What was the background in Earthbound?
            [Question.EarthboundBackground] = new()
            {
                QuestionText = "What was the background number in {0}?",
            },
            // Which monster was displayed in {0}?
            // Which monster was displayed in Earthbound?
            [Question.EarthboundMonster] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which monster was displayed in {0}?",
            },

            // eeB gnillepS
            // What word was asked to be spelled in {0}?
            // What word was asked to be spelled in eeB gnillepS?
            [Question.eeBgnillepSWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What word was asked to be spelled in {0}?",
            },

            // Eight
            // What was the last digit on the small display in {0}?
            // What was the last digit on the small display in Eight?
            [Question.EightLastSmallDisplayDigit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the last digit on the small display in {0}?",
            },
            // What was the position of the last broken digit in {0}?
            // What was the position of the last broken digit in Eight?
            [Question.EightLastBrokenDigitPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the position of the last broken digit in {0}?",
            },
            // What were the last resulting digits in {0}?
            // What were the last resulting digits in Eight?
            [Question.EightLastResultingDigits] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the last resulting digits in {0}?",
            },
            // What was the last displayed number in {0}?
            // What was the last displayed number in Eight?
            [Question.EightLastDisplayedNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the last displayed number in {0}?",
            },

            // Elder Futhark
            // What was the {1} rune shown on {0}?
            // What was the first rune shown on Elder Futhark?
            [Question.ElderFutharkRunes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} rune shown on {0}?",
            },

            // Emoji
            // What was the {1} emoji in {0}?
            // What was the left emoji in Emoji?
            [Question.EmojiEmoji] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} emoji in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "left",
                    ["right"] = "right",
                },
            },

            // ENA Cipher
            // What was the {1} keyword in {0}?
            // What was the first keyword in ENA Cipher?
            [Question.EnaCipherKeywordAnswer] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} keyword in {0}?",
            },
            // What was the transposition key in {0}?
            // What was the transposition key in ENA Cipher?
            [Question.EnaCipherExtAnswer] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the transposition key in {0}?",
            },
            // What was the encrypted word in {0}?
            // What was the encrypted word in ENA Cipher?
            [Question.EnaCipherEncryptedAnswer] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the encrypted word in {0}?",
            },

            // Encrypted Dice
            // Which of these numbers appeared on a die in the {1} stage of {0}?
            // Which of these numbers appeared on a die in the first stage of Encrypted Dice?
            [Question.EncryptedDice] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these numbers appeared on a die in the {1} stage of {0}?",
            },

            // Encrypted Equations
            // Which shape was the {1} operand in {0}?
            // Which shape was the first operand in Encrypted Equations?
            [Question.EncryptedEquationsShapes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which shape was the {1} operand in {0}?",
            },

            // Encrypted Hangman
            // What method of encryption was used by {0}?
            // What method of encryption was used by Encrypted Hangman?
            [Question.EncryptedHangmanEncryptionMethod] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What method of encryption was used by {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What module name was encrypted by {0}?",
            },

            // Encrypted Maze
            // Which symbol on {0} was spinning {1}?
            // Which symbol on Encrypted Maze was spinning clockwise?
            [Question.EncryptedMazeSymbols] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which symbol on {0} was spinning {1}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["clockwise"] = "clockwise",
                    ["counter-clockwise"] = "counter-clockwise",
                },
            },

            // Encrypted Morse
            // What was the {1} on {0}?
            // What was the received call on Encrypted Morse?
            [Question.EncryptedMorseCallResponse] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} on {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["received call"] = "received call",
                    ["sent response"] = "sent response",
                },
            },

            // Encryption Bingo
            // What was the first encoding used in {0}?
            // What was the first encoding used in Encryption Bingo?
            [Question.EncryptionBingoEncoding] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the first encoding used in {0}?",
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
                QuestionText = "Was war die {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Nachricht",
                    ["response"] = "Antwort",
                },
            },

            // Entry Number Four
            // What was the {1} number shown in {0}?
            // What was the first number shown in Entry Number Four?
            [Question.EntryNumberFourNumbers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} number shown in {0}?",
            },
            // What was the expected fourth entry in {0}?
            // What was the expected fourth entry in Entry Number Four?
            [Question.EntryNumberFourExpected] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the expected fourth entry in {0}?",
            },
            // What was the constant coefficient in {0}?
            // What was the constant coefficient in Entry Number Four?
            [Question.EntryNumberFourCoeff] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the constant coefficient in {0}?",
            },

            // Entry Number One
            // What was the {1} number shown in {0}?
            // What was the first number shown in Entry Number One?
            [Question.EntryNumberOneNumbers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} number shown in {0}?",
            },
            // What was the expected first entry in {0}?
            // What was the expected first entry in Entry Number One?
            [Question.EntryNumberOneExpected] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the expected first entry in {0}?",
            },
            // What was the constant coefficient in {0}?
            // What was the constant coefficient in Entry Number One?
            [Question.EntryNumberOneCoeff] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the constant coefficient in {0}?",
            },

            // Épelle-moi Ça
            // What word was asked to be spelled in {0}?
            // What word was asked to be spelled in Épelle-moi Ça?
            [Question.EpelleMoiCaWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What word was asked to be spelled in {0}?",
            },

            // Equations X
            // What was the displayed symbol in {0}?
            // What was the displayed symbol in Equations X?
            [Question.EquationsXSymbols] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed symbol in {0}?",
            },

            // Error Codes
            // What was the active error code in {0}?
            // What was the active error code in Error Codes?
            [Question.ErrorCodesActiveError] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the active error code in {0}?",
            },

            // Etterna
            // What was the beat for the {1} arrow from the bottom in {0}?
            // What was the beat for the first arrow from the bottom in Etterna?
            [Question.EtternaNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the beat for the {1} arrow from the bottom in {0}?",
            },

            // Exoplanets
            // What was the starting target planet in {0}?
            // What was the starting target planet in Exoplanets?
            [Question.ExoplanetsStartingTargetPlanet] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting target planet in {0}?",
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
            [Question.ExoplanetsStartingTargetDigit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting target digit in {0}?",
            },
            // What was the final target planet in {0}?
            // What was the final target planet in Exoplanets?
            [Question.ExoplanetsTargetPlanet] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the final target planet in {0}?",
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
            [Question.ExoplanetsTargetDigit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the final target digit in {0}?",
            },

            // Factoring Maze
            // What was one of the prime numbers chosen in {0}?
            // What was one of the prime numbers chosen in Factoring Maze?
            [Question.FactoringMazeChosenPrimes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was one of the prime numbers chosen in {0}?",
            },

            // Factory Maze
            // What room did you start in in {0}?
            // What room did you start in in Factory Maze?
            [Question.FactoryMazeStartRoom] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What room did you start in in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What was the last pair of letters in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which button referred to the {1} button in reading order in {0}?",
            },
            // Which button did the {1} button in reading order refer to in {0}?
            // Which button did the first button in reading order refer to in Faulty Buttons?
            [Question.FaultyButtonsThisButtonReferredTo] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which button did the {1} button in reading order refer to in {0}?",
            },

            // Faulty RGB Maze
            // What was the exit coordinate in {0}?
            // What was the exit coordinate in Faulty RGB Maze?
            [Question.FaultyRGBMazeExit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the exit coordinate in {0}?",
            },
            // Where was the {1} key in {0}?
            // Where was the red key in Faulty RGB Maze?
            [Question.FaultyRGBMazeKeys] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was the {1} key in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                },
            },
            // Which maze number was the {1} maze in {0}?
            // Which maze number was the red maze in Faulty RGB Maze?
            [Question.FaultyRGBMazeNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which maze number was the {1} maze in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                },
            },

            // Find The Date
            // What was the day displayed in the {1} stage of {0}?
            // What was the day displayed in the first stage of Find The Date?
            [Question.FindTheDateDay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the day displayed in the {1} stage of {0}?",
            },
            // What was the month displayed in the {1} stage of {0}?
            // What was the month displayed in the first stage of Find The Date?
            [Question.FindTheDateMonth] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the month displayed in the {1} stage of {0}?",
            },
            // What was the year displayed in the {1} stage of {0}?
            // What was the year displayed in the first stage of Find The Date?
            [Question.FindTheDateYear] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the year displayed in the {1} stage of {0}?",
            },

            // Five Letter Words
            // Which of these words was on the display in {0}?
            // Which of these words was on the display in Five Letter Words?
            [Question.FiveLetterWordsDisplayedWords] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these words was on the display in {0}?",
            },

            // FizzBuzz
            // What was the {1} digit on the {2} display of {0}?
            // What was the first digit on the top display of FizzBuzz?
            [Question.FizzBuzzDisplayedNumbers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} digit on the {2} display of {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // Flags
            // What was the displayed number in {0}?
            // What was the displayed number in Flags?
            [Question.FlagsDisplayedNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed number in {0}?",
            },
            // What was the main country flag in {0}?
            // What was the main country flag in Flags?
            [Question.FlagsMainCountry] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the main country flag in {0}?",
            },
            // Which of these country flags was shown, but not the main country flag, in {0}?
            // Which of these country flags was shown, but not the main country flag, in Flags?
            [Question.FlagsCountries] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these country flags was shown, but not the main country flag, in {0}?",
            },

            // Flashing Arrows
            // What number was displayed on {0}?
            // What number was displayed on Flashing Arrows?
            [Question.FlashingArrowsDisplayedValue] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What number was displayed on {0}?",
            },
            // What color flashed {1} black on the relevant arrow in {0}?
            // What color flashed before black on the relevant arrow in Flashing Arrows?
            [Question.FlashingArrowsReferredArrow] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color flashed {1} black on the relevant arrow in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["before"] = "before",
                    ["after"] = "after",
                },
                Answers = new Dictionary<string, string>
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

            // Flashing Lights
            // How many times did the {1} LED flash {2} on {0}?
            // How many times did the top LED flash cyan on Flashing Lights?
            [Question.FlashingLightsLEDFrequency] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many times did the {1} LED flash {2} on {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["cyan"] = "türkis",
                    ["green"] = "grün",
                    ["red"] = "rot",
                    ["purple"] = "lila",
                    ["orange"] = "orange",
                    ["bottom"] = "bottom",
                },
            },

            // Flavor Text
            // Which module’s flavor text was shown in {0}?
            // Which module’s flavor text was shown in Flavor Text?
            [Question.FlavorTextModule] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which module’s flavor text was shown in {0}?",
            },

            // Flavor Text EX
            // Which module’s flavor text was shown in the {1} stage of {0}?
            // Which module’s flavor text was shown in the first stage of Flavor Text EX?
            [Question.FlavorTextEXModule] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which module’s flavor text was shown in the {1} stage of {0}?",
            },

            // Flyswatting
            // Which fly was present, but not in the solution in {0}?
            // Which fly was present, but not in the solution in Flyswatting?
            [Question.FlyswattingUnpressed] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which fly was present, but not in the solution in {0}?",
            },

            // Follow Me
            // What was the {1} flashing direction in {0}?
            // What was the first flashing direction in Follow Me?
            [Question.FollowMeDisplayedPath] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} flashing direction in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Up",
                    ["Down"] = "Down",
                    ["Left"] = "Left",
                    ["Right"] = "Right",
                },
            },

            // Forest Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Forest Cipher?
            [Question.ForestCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Wald-Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // Forget Any Color
            // What colors were the cylinders during the {1} stage of {0}?
            // What colors were the cylinders during the first stage of Forget Any Color?
            [Question.ForgetAnyColorCylinder] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Was waren bei {0} die Zylinderfarben in der {1}en Stufe?",
                ModuleName = "Vergiss Jede Farbe",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
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
                    ["the Forget Any Color which used figure {0} in the {1} stage"] = "dem Vergiss Jede Farbe, in dessen {1}er Stufe die Figur {0} verwendet wurde,",
                    ["the Forget Any Color whose cylinders in the {1} stage were {0}"] = "dem Vergiss Jede Farbe, in dessen {1}er Stufe die Zylinderfarben {0} waren,",
                },
            },
            // Which figure was used during the {1} stage of {0}?
            // Which figure was used during the first stage of Forget Any Color?
            [Question.ForgetAnyColorSequence] = new()
            {
                QuestionText = "Welche Figur kam bei {0} in der {1}en Stufe vor?",
                ModuleName = "Vergiss Jede Farbe",
            },

            // Forget Everything
            // What was the {1} displayed digit in the first stage of {0}?
            // What was the first displayed digit in the first stage of Forget Everything?
            [Question.ForgetEverythingStageOneDisplay] = new()
            {
                QuestionText = "Was war bei {0} die {1}e Ziffer in der ersten Stufe?",
                ModuleName = "Vergiss Alles",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the Forget Everything whose {0} displayed digit in that stage was {1}"] = "dem Vergiss Alles, dessen {0}e Ziffer in der ersten Stufe {1} war,",
                },
            },

            // Forget Me
            // What number was in the {1} position of the initial puzzle in {0}?
            // What number was in the top-left position of the initial puzzle in Forget Me?
            [Question.ForgetMeInitialState] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What number was in the {1} position of the initial puzzle in {0}?",
                FormatArgs = new Dictionary<string, string>
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

            // Forget Me Not
            // What was the digit displayed in the {1} stage of {0}?
            // What was the digit displayed in the first stage of Forget Me Not?
            [Question.ForgetMeNotDisplayedDigits] = new()
            {
                QuestionText = "Welche Ziffer wurde bei {0} in der {1}en Stufe angezeigt?",
                ModuleName = "Vergissmeinnicht",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the Forget Me Not which displayed a {0} in the {1} stage"] = "dem Vergissmeinnicht, in dessen {1}er Stufe {0} angezeigt wurde,",
                },
            },

            // Forget Me Now
            // What was the {1} displayed digit in {0}?
            // What was the first displayed digit in Forget Me Now?
            [Question.ForgetMeNowDisplayedDigits] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} displayed digit in {0}?",
            },

            // Forget’s Ultimate Showdown
            // What was the {1} digit of the answer in {0}?
            // What was the first digit of the answer in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownAnswer] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} digit of the answer in {0}?",
            },
            // What was the {1} digit of the initial number in {0}?
            // What was the first digit of the initial number in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownInitial] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} digit of the initial number in {0}?",
            },
            // What was the {1} digit of the bottom number in {0}?
            // What was the first digit of the bottom number in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownBottom] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} digit of the bottom number in {0}?",
            },
            // What was the {1} method used in {0}?
            // What was the first method used in Forget’s Ultimate Showdown?
            [Question.ForgetsUltimateShowdownMethod] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} method used in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Forget Me Not"] = "Vergissmeinnicht",
                    ["Simon’s Stages"] = "Simons Stufen",
                    ["Forget Me Later"] = "Vergiss Mich Später",
                    ["Forget Infinity"] = "Vergiss Unendlichkeit",
                    ["A>N<D"] = "U>N<D",
                    ["Forget Me Now"] = "Vergiss Mich Jetzt",
                    ["Forget Everything"] = "Vergiss Alles",
                    ["Forget Us Not"] = "Vergiss Uns Nicht",
                },
            },

            // Forget The Colors
            // What number was on the gear during stage {1} of {0}?
            // What number was on the gear during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsGearNumber] = new()
            {
                QuestionText = "Welche Zahl war bei {0} in Stufe {1} auf dem Zahnrad?",
                ModuleName = "Vergiss Die Farben",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the Forget The Colors whose gear number was {0} in stage {1}"] = "dem Vergiss die Farben, dessen Zahnradzahl in Stufe {1} {0} war,",
                    ["the Forget The Colors which had {0} on its large display in stage {1}"] = "dem Vergiss die Farben, dessen großes Display in Stufe {1} {0} anzeigte,",
                    ["the Forget The Colors whose received sine number in stage {1} ended with a {0}"] = "dem Vergiss die Farben, dessen erhaltene Sinuszahl in Stufe {1} auf {0} endete,",
                    ["the Forget The Colors whose gear color was {0} in stage {1}"] = "dem Vergiss die Farben, dessen Zahnradfarbe in Stufe {1} {0} war,",
                    ["the Forget The Colors whose rule color was {0} in stage {1}"] = "dem Vergiss die Farben, dessen Regelfarbe in Stufe {1} {0} war,",
                },
            },
            // What number was on the large display during stage {1} of {0}?
            // What number was on the large display during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsLargeDisplay] = new()
            {
                QuestionText = "Welche Zahl war bei {0} in Stufe {1} auf dem großen Display?",
                ModuleName = "Vergiss Die Farben",
            },
            // What was the last decimal in the sine number received during stage {1} of {0}?
            // What was the last decimal in the sine number received during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsSineNumber] = new()
            {
                QuestionText = "Was war bei {0} die letzte Ziffer in der in Stufe {1} erhaltenen Sinuszahl?",
                ModuleName = "Vergiss Die Farben",
            },
            // What color was the gear during stage {1} of {0}?
            // What color was the gear during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsGearColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Welche Farbe hatte bei {0} das Zahnrad in Stufe {1}?",
                ModuleName = "Vergiss Die Farben",
                Answers = new Dictionary<string, string>
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
            // Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?
            // Which edgework-based rule was applied to the sum of nixies and gear during stage 0 of Forget The Colors?
            [Question.ForgetTheColorsRuleColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Welche peripheriebasierte Regel wurde bei {0} in Stufe {1} auf die Summe der Nixies und des Zahnrads angewandt?",
                ModuleName = "Vergiss Die Farben",
                Answers = new Dictionary<string, string>
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

            // Forget This
            // What color was the LED in the {1} stage of {0}?
            // What color was the LED in the first stage of Forget This?
            [Question.ForgetThisColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Welche Farbe hatte bei {0} die LED in Stufe {1}?",
                ModuleName = "Vergiss Dies",
                Answers = new Dictionary<string, string>
                {
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                    ["Black"] = "Schwarz",
                    ["White"] = "Weiß",
                    ["Green"] = "Grün",
                },
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the Forget This whose LED was {0} in the {1} stage"] = "dem Vergiss Dies, dessen LED in der {1}en Stufe {0} war,",
                    ["the Forget This which displayed {0} in the {1} stage"] = "dem Vergiss Dies, das in der {1}en Stufe {0} anzeigte,",
                },
            },
            // What was the digit displayed in the {1} stage of {0}?
            // What was the digit displayed in the first stage of Forget This?
            [Question.ForgetThisDigits] = new()
            {
                QuestionText = "Welche Ziffer wurde bei {0} in der {1}en Stufe angezeigt?",
                ModuleName = "Vergiss Dies",
            },

            // Free Parking
            // What was the player token in {0}?
            // What was the player token in Free Parking?
            [Question.FreeParkingToken] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the player token in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What was the last digit of your first query’s result in {0}?",
            },
            // What number was to the left of the displayed letter in {0}?
            // What number was to the left of the displayed letter in Functions?
            [Question.FunctionsLeftNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What number was to the left of the displayed letter in {0}?",
            },
            // What letter was displayed in {0}?
            // What letter was displayed in Functions?
            [Question.FunctionsLetter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What letter was displayed in {0}?",
            },
            // What number was to the right of the displayed letter in {0}?
            // What number was to the right of the displayed letter in Functions?
            [Question.FunctionsRightNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What number was to the right of the displayed letter in {0}?",
            },

            // The Fuse Box
            // What color flashed {1} in {0}?
            // What color flashed first in The Fuse Box?
            // Note: This question is depicted visually, rather than with words. A translation here will only be used for logging.
            [Question.FuseBoxFlashes] = new()
            {
                Gender = Gender.Masculine,
                QuestionText = "Welche Farbe ist bei {0} als {1}e vorgekommen?",
                ModuleName = "Sicherungskasten",
            },
            // What arrow was shown {1} in {0}?
            // What arrow was shown first in The Fuse Box?
            // Note: This question is depicted visually, rather than with words. A translation here will only be used for logging.
            [Question.FuseBoxArrows] = new()
            {
                Gender = Gender.Masculine,
                QuestionText = "Welcher Pfeil war bei {0} der {1}e?",
                ModuleName = "Sicherungskasten",
            },

            // Gadgetron Vendor
            // What was your current weapon in {0}?
            // What was your current weapon in Gadgetron Vendor?
            [Question.GadgetronVendorCurrentWeapon] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was your current weapon in {0}?",
            },
            // What was the weapon up for sale in {0}?
            // What was the weapon up for sale in Gadgetron Vendor?
            [Question.GadgetronVendorWeaponForSale] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the weapon up for sale in {0}?",
            },

            // Game of Life Cruel
            // Which of these was a color combination that occurred in {0}?
            // Which of these was a color combination that occurred in Game of Life Cruel?
            [Question.GameOfLifeCruelColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these was a color combination that occurred in {0}?",
            },

            // The Gamepad
            // What were the numbers on {0}?
            // What were the numbers on The Gamepad?
            [Question.GamepadNumbers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the numbers on {0}?",
            },

            // Garfield Kart
            // How many puzzle pieces did {0} have?
            // How many puzzle pieces did Garfield Kart have?
            [Question.GarfieldKartPuzzleCount] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many puzzle pieces did {0} have?",
            },
            // What was the track in {0}?
            // What was the track in Garfield Kart?
            [Question.GarfieldKartTrack] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the track in {0}?",
            },

            // The Garnet Thief
            // Which faction did {1} claim to be in {0}?
            // Which faction did Jungmoon claim to be in The Garnet Thief?
            [Question.GarnetThiefClaim] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which faction did {1} claim to be in {0}?",
            },

            // Ghost Movement
            // Where was {1} in {0}?
            // Where was Inky in Ghost Movement?
            [Question.GhostMovementPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was {1} in {0}?",
            },

            // Girlfriend
            // What was the language sung in {0}?
            // What was the language sung in Girlfriend?
            [Question.GirlfriendLanguage] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the language sung in {0}?",
            },

            // The Glitched Button
            // What was the cycling bit sequence in {0}?
            // What was the cycling bit sequence in The Glitched Button?
            [Question.GlitchedButtonSequence] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the cycling bit sequence in {0}?",
            },

            // Goofy’s Game
            // What number was flashed by the {1} LED in {0}?
            // What number was flashed by the left LED in Goofy’s Game?
            [Question.GoofysGameNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What number was flashed by the {1} LED in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "left",
                    ["right"] = "right",
                    ["center"] = "center",
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
                NeedsTranslation = true,
                QuestionText = "What was the {1} coordinate on the display in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["horizontal"] = "horizontal",
                    ["vertical"] = "vertical",
                },
            },

            // Gray Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Gray Cipher?
            [Question.GrayCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameDative = "Grauen Geheimschrift",
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Graue Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // The Great Void
            // What was the {1} color in {0}?
            // What was the first color in The Great Void?
            [Question.GreatVoidColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} color in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                    ["Cyan"] = "Türkis",
                    ["White"] = "Weiß",
                },
            },
            // What was the {1} digit in {0}?
            // What was the first digit in The Great Void?
            [Question.GreatVoidDigit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} digit in {0}?",
            },

            // Green Arrows
            // What was the last number on the display on {0}?
            // What was the last number on the display on Green Arrows?
            [Question.GreenArrowsLastScreen] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the last number on the display on {0}?",
            },

            // The Green Button
            // What was the word submitted in {0}?
            // What was the word submitted in The Green Button?
            [Question.GreenButtonWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the word submitted in {0}?",
            },

            // Green Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Green Cipher?
            [Question.GreenCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameDative = "Grünen Geheimschrift",
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Grüne Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // Gridlock
            // What was the starting location in {0}?
            // What was the starting location in Gridlock?
            [Question.GridLockStartingLocation] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting location in {0}?",
            },
            // What was the ending location in {0}?
            // What was the ending location in Gridlock?
            [Question.GridLockEndingLocation] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the ending location in {0}?",
            },
            // What was the starting color in {0}?
            // What was the starting color in Gridlock?
            [Question.GridLockStartingColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting color in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                },
            },

            // Grocery Store
            // What was the first item shown in {0}?
            // What was the first item shown in Grocery Store?
            [Question.GroceryStoreFirstItem] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the first item shown in {0}?",
            },

            // Gryphons
            // What was the gryphon’s name in {0}?
            // What was the gryphon’s name in Gryphons?
            [Question.GryphonsName] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the gryphon’s name in {0}?",
            },
            // What was the gryphon’s age in {0}?
            // What was the gryphon’s age in Gryphons?
            [Question.GryphonsAge] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the gryphon’s age in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What color was the {1} LED in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["bottom"] = "bottom",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Blue"] = "Blue",
                    ["Green"] = "Green",
                    ["Yellow"] = "Yellow",
                },
            },

            // h
            // What was the transmitted letter in {0}?
            // What was the transmitted letter in h?
            [Question.HLetter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the transmitted letter in {0}?",
            },

            // Halli Galli
            // Which fruit were there five of in {0}?
            // Which fruit were there five of in Halli Galli?
            [Question.HalliGalliFruit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which fruit were there five of in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Strawberries"] = "Strawberries",
                    ["Melons"] = "Melons",
                    ["Lemons"] = "Lemons",
                    ["Raspberries"] = "Raspberries",
                    ["Bananas"] = "Bananas",
                },
            },
            // What were the relevant counts in {0}?
            // What were the relevant counts in Halli Galli?
            [Question.HalliGalliCounts] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the relevant counts in {0}?",
            },

            // Hereditary Base Notation
            // What was the given number in {0}?
            // What was the given number in Hereditary Base Notation?
            [Question.HereditaryBaseNotationInitialNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the given number in {0}?",
            },

            // The Hexabutton
            // What label was printed on {0}?
            // What label was printed on The Hexabutton?
            [Question.HexabuttonLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What label was printed on {0}?",
            },

            // Hexamaze
            // What was the color of the pawn in {0}?
            // What was the color of the pawn in Hexamaze?
            [Question.HexamazePawnColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the pawn in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Cyan"] = "Türkis",
                    ["Blue"] = "Blau",
                    ["Pink"] = "Pink",
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
                NeedsTranslation = true,
                QuestionText = "What were the deciphered letters in {0}?",
            },
            // What was the deciphered phrase in {0}?
            // What was the deciphered phrase in hexOS?
            [Question.HexOSOctCipher] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the deciphered phrase in {0}?",
            },
            // What was the {1} 3-digit number cycled by the screen in {0}?
            // What was the first 3-digit number cycled by the screen in hexOS?
            [Question.HexOSScreen] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} 3-digit number cycled by the screen in {0}?",
            },
            // What were the rhythm values in {0}?
            // What were the rhythm values in hexOS?
            [Question.HexOSSum] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the rhythm values in {0}?",
            },

            // Hidden Colors
            // What was the color of the main LED in {0}?
            // What was the color of the main LED in Hidden Colors?
            [Question.HiddenColorsLED] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the main LED in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Orange"] = "Orange",
                    ["Purple"] = "Lila",
                    ["Magenta"] = "Magenta",
                    ["White"] = "Weiß",
                },
            },

            // The Hidden Value
            // What was displayed on {0}?
            // What was displayed on The Hidden Value?
            [Question.HiddenValueDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was displayed on {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What was the position of the player in {0}?",
            },
            // What was the score of the player in {0}?
            // What was the score of the player in The High Score?
            [Question.HighScoreScore] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the score of the player in {0}?",
            },

            // Hill Cycle
            // What was the {1} in {0}?
            // What was the message in Hill Cycle?
            [Question.HillCycleWord] = new()
            {
                QuestionText = "Was war die {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Nachricht",
                    ["response"] = "Antwort",
                },
            },

            // Hinges
            // Which of these hinges was initially {1} {0}?
            // Which of these hinges was initially present on Hinges?
            [Question.HingesInitialHinges] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these hinges was initially {1} {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["present on"] = "present on",
                    ["absent from"] = "absent from",
                },
            },

            // Hogwarts
            // Which House was {1} solved for in {0}?
            // Which House was Binary Puzzle solved for in Hogwarts?
            [Question.HogwartsHouse] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which House was {1} solved for in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which module was solved for {1} in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What was the name of the {1} shadow shown in {0}?",
            },

            // Homophones
            // What was the {1} displayed phrase in {0}?
            // What was the first displayed phrase in Homophones?
            [Question.HomophonesDisplayedPhrases] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} displayed phrase in {0}?",
            },

            // Horrible Memory
            // In what position was the button pressed on the {1} stage of {0}?
            // In what position was the button pressed on the first stage of Horrible Memory?
            [Question.HorribleMemoryPositions] = new()
            {
                NeedsTranslation = true,
                QuestionText = "In what position was the button pressed on the {1} stage of {0}?",
            },
            // What was the label of the button pressed on the {1} stage of {0}?
            // What was the label of the button pressed on the first stage of Horrible Memory?
            [Question.HorribleMemoryLabels] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label of the button pressed on the {1} stage of {0}?",
            },
            // What color was the button pressed on the {1} stage of {0}?
            // What color was the button pressed on the first stage of Horrible Memory?
            [Question.HorribleMemoryColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the button pressed on the {1} stage of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["red"] = "rot",
                    ["orange"] = "orange",
                    ["purple"] = "lila",
                    ["pink"] = "pink",
                },
            },

            // Human Resources
            // Which was a descriptor shown in {1} in {0}?
            // Which was a descriptor shown in red in Human Resources?
            [Question.HumanResourcesDescriptors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which was a descriptor shown in {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                },
            },
            // Who was {1} in {0}?
            // Who was fired in Human Resources?
            [Question.HumanResourcesHiredFired] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Who was {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["fired"] = "fired",
                    ["hired"] = "hired",
                },
            },

            // Hunting
            // Which of the first three stages of {0} had the {1} symbol {2}?
            // Which of the first three stages of Hunting had the column symbol first?
            [Question.HuntingColumnsRows] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of the first three stages of {0} had the {1} symbol {2}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["column"] = "column",
                    ["row"] = "row",
                },
                Answers = new Dictionary<string, string>
                {
                    ["none"] = "none",
                    ["first"] = "first",
                    ["second"] = "second",
                    ["first two"] = "first two",
                    ["third"] = "third",
                    ["first & third"] = "first & third",
                    ["second & third"] = "second & third",
                    ["all three"] = "all three",
                },
            },

            // The Hypercube
            // What was the {1} rotation in {0}?
            // What was the first rotation in The Hypercube?
            [Question.HypercubeRotations] = new()
            {
                QuestionText = "Was war die {1}e Rotation in {0}?",
            },

            // HyperForget
            // What was the rotation for the {1} stage in {0}?
            // What was the rotation for the first stage in HyperForget?
            [Question.HyperForgetRotations] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the rotation for the {1} stage in {0}?",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the HyperForget whose rotation in the {1} stage was {0}"] = "the HyperForget whose rotation in the {1} stage was {0}",
                },
            },

            // The Hyperlink
            // What was the {1} character of the hyperlink in {0}?
            // What was the first character of the hyperlink in The Hyperlink?
            [Question.HyperlinkCharacters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} character of the hyperlink in {0}?",
            },
            // Which module was referenced on {0}?
            // Which module was referenced on The Hyperlink?
            [Question.HyperlinkAnswer] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which module was referenced on {0}?",
            },

            // Ice Cream
            // Which one of these flavours {1} to the {2} customer in {0}?
            // Which one of these flavours was on offer, but not sold, to the first customer in Ice Cream?
            [Question.IceCreamFlavour] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which one of these flavours {1} to the {2} customer in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was on offer, but not sold,"] = "was on offer, but not sold,",
                    ["was not on offer"] = "was not on offer",
                },
            },
            // Who was the {1} customer in {0}?
            // Who was the first customer in Ice Cream?
            [Question.IceCreamCustomer] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Who was the {1} customer in {0}?",
            },

            // Identification Crisis
            // What was the {1} shape used in {0}?
            // What was the first shape used in Identification Crisis?
            [Question.IdentificationCrisisShape] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} shape used in {0}?",
            },
            // What was the {1} identification module used in {0}?
            // What was the first identification module used in Identification Crisis?
            [Question.IdentificationCrisisDataset] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} identification module used in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which hair color {1} listed in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "was",
                    ["was not"] = "was not",
                },
            },
            // Which build {1} listed in {0}?
            // Which build was listed in Identity Parade?
            [Question.IdentityParadeBuilds] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which build {1} listed in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "was",
                    ["was not"] = "was not",
                },
            },
            // Which attire {1} listed in {0}?
            // Which attire was listed in Identity Parade?
            [Question.IdentityParadeAttires] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which attire {1} listed in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "was",
                    ["was not"] = "was not",
                },
            },

            // The Impostor
            // Which module was {0} pretending to be?
            // Which module was The Impostor pretending to be?
            [Question.ImpostorDisguise] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which module was {0} pretending to be?",
            },

            // Indigo Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Indigo Cipher?
            [Question.IndigoCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Indigo-Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // Infinite Loop
            // What was the selected word in {0}?
            // What was the selected word in Infinite Loop?
            [Question.InfiniteLoopSelectedWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the selected word in {0}?",
            },

            // Ingredients
            // Which ingredient was used in {0}?
            // Which ingredient was used in Ingredients?
            [Question.IngredientsIngredients] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which ingredient was used in {0}?",
            },
            // Which ingredient was listed but not used in {0}?
            // Which ingredient was listed but not used in Ingredients?
            [Question.IngredientsNonIngredients] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which ingredient was listed but not used in {0}?",
            },

            // Inner Connections
            // What color was the LED in {0}?
            // What color was the LED in Inner Connections?
            [Question.InnerConnectionsLED] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the LED in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Black"] = "Schwarz",
                    ["Blue"] = "Blau",
                    ["Red"] = "Rot",
                    ["White"] = "Weiß",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                },
            },
            // What was the digit flashed in Morse in {0}?
            // What was the digit flashed in Morse in Inner Connections?
            [Question.InnerConnectionsMorse] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the digit flashed in Morse in {0}?",
            },

            // Interpunct
            // What was the symbol displayed in the {1} stage of {0}?
            // What was the symbol displayed in the first stage of Interpunct?
            [Question.InterpunctDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the symbol displayed in the {1} stage of {0}?",
            },

            // IPA
            // What sound played in {0}?
            // What sound played in IPA?
            [Question.IpaSound] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What sound played in {0}?",
            },

            // The iPhone
            // What was the {1} PIN digit in {0}?
            // What was the first PIN digit in The iPhone?
            [Question.iPhoneDigits] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} PIN digit in {0}?",
            },

            // Jenga
            // Which symbol was on the first correctly pulled block in {0}?
            // Which symbol was on the first correctly pulled block in Jenga?
            [Question.JengaFirstBlock] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which symbol was on the first correctly pulled block in {0}?",
            },

            // The Jewel Vault
            // What number was wheel {1} in {0}?
            // What number was wheel A in The Jewel Vault?
            [Question.JewelVaultWheels] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What number was wheel {1} in {0}?",
            },

            // Jumble Cycle
            // What was the {1} in {0}?
            // What was the message in Jumble Cycle?
            [Question.JumbleCycleWord] = new()
            {
                QuestionText = "Was war die {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Nachricht",
                    ["response"] = "Antwort",
                },
            },

            // Juxtacolored Squares
            // What was the color of this square in {0}?
            // What was the color of this square in Juxtacolored Squares?
            [Question.JuxtacoloredSquaresColorsByPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of this square in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Magenta"] = "Magenta",
                    ["Orange"] = "Orange",
                    ["Cyan"] = "Türkis",
                    ["Purple"] = "Lila",
                    ["Chestnut"] = "Chestnut",
                    ["Brown"] = "Brown",
                    ["Mauve"] = "Mauve",
                    ["Azure"] = "Azure",
                    ["Jade"] = "Jade",
                    ["Forest"] = "Forest",
                    ["Gray"] = "Grau",
                    ["Black"] = "Schwarz",
                },
            },
            // Which square was {1} in {0}?
            // Which square was red in Juxtacolored Squares?
            [Question.JuxtacoloredSquaresPositionsByColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which square was {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                    ["green"] = "grün",
                    ["magenta"] = "magenta",
                    ["orange"] = "orange",
                    ["cyan"] = "türkis",
                    ["purple"] = "lila",
                    ["chestnut"] = "kastanienfarben",
                    ["brown"] = "braun",
                    ["mauve"] = "malvenfarben",
                    ["azure"] = "azurfarben",
                    ["jade"] = "jadefarben",
                    ["forest"] = "waldfarben",
                    ["gray"] = "grau",
                    ["black"] = "schwarz",
                },
            },

            // Kanji
            // What was the displayed word in the {1} stage of {0}?
            // What was the displayed word in the first stage of Kanji?
            [Question.KanjiDisplayedWords] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed word in the {1} stage of {0}?",
            },

            // The Kanye Encounter
            // What was a food item displayed in {0}?
            // What was a food item displayed in The Kanye Encounter?
            [Question.KanyeEncounterFoods] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was a food item displayed in {0}?",
            },

            // Keypad Combinations
            // Which number was displayed on the {1} button, but not part of the answer on {0}?
            // Which number was displayed on the first button, but not part of the answer on Keypad Combinations?
            [Question.KeypadCombinationWrongNumbers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which number was displayed on the {1} button, but not part of the answer on {0}?",
            },

            // Keypad Magnified
            // What was the position of the LED in {0}?
            // What was the position of the LED in Keypad Magnified?
            [Question.KeypadMagnifiedLED] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the position of the LED in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Top-left"] = "Top-left",
                    ["Top-right"] = "Top-right",
                    ["Bottom-left"] = "Bottom-left",
                    ["Bottom-right"] = "Bottom-right",
                },
            },

            // Keypad Maze
            // Which of these cells was yellow in {0}?
            // Which of these cells was yellow in Keypad Maze?
            [Question.KeypadMazeYellow] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these cells was yellow in {0}?",
            },

            // Keypad Sequence
            // What was this key’s label on the {1} panel in {0}?
            // What was this key’s label on the first panel in Keypad Sequence?
            [Question.KeypadSequenceLabels] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label on this button on the {1} panel in {0}?",
            },

            // Keywords
            // What were the first four letters on the display in {0}?
            // What were the first four letters on the display in Keywords?
            [Question.KeywordsDisplayedKey] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the first four letters on the display in {0}?",
            },

            // Know Your Way
            // Which way was the arrow pointing in {0}?
            // Which way was the arrow pointing in Know Your Way?
            [Question.KnowYourWayArrow] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which way was the arrow pointing in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Up",
                    ["Down"] = "Down",
                    ["Left"] = "Left",
                    ["Right"] = "Right",
                },
            },
            // Which LED was green in {0}?
            // Which LED was green in Know Your Way?
            [Question.KnowYourWayLed] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which LED was green in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Top"] = "Top",
                    ["Bottom"] = "Bottom",
                    ["Right"] = "Right",
                    ["Left"] = "Left",
                },
            },

            // Kooky Keypad
            // What color was the {1} button’s LED in {0}?
            // What color was the top-left button’s LED in Kooky Keypad?
            [Question.KookyKeypadColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} button’s LED in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-right"] = "top-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
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
                NeedsTranslation = true,
                QuestionText = "Which square was {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["pre-filled"] = "pre-filled",
                    ["not pre-filled"] = "not pre-filled",
                },
            },

            // Kuro
            // What was Kuro’s mood in {0}?
            // What was Kuro’s mood in Kuro?
            [Question.KuroMood] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was Kuro’s mood in {0}?",
            },

            // The Labyrinth
            // Where was one of the portals in layer {1} in {0}?
            // Where was one of the portals in layer 1 (Red) in The Labyrinth?
            [Question.LabyrinthPortalLocations] = new()
            {
                NeedsTranslation = true,
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
            [Question.LabyrinthPortalStage] = new()
            {
                NeedsTranslation = true,
                QuestionText = "In which layer was this portal in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["1 (Red)"] = "1 (Red)",
                    ["2 (Orange)"] = "2 (Orange)",
                    ["3 (Yellow)"] = "3 (Yellow)",
                    ["4 (Green)"] = "4 (Green)",
                    ["5 (Blue)"] = "5 (Blue)",
                },
            },

            // Ladder Lottery
            // Which light was on in {0}?
            // Which light was on in Ladder Lottery?
            [Question.LadderLotteryLightOn] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which light was on in {0}?",
            },

            // Ladders
            // Which color was present on the second ladder in {0}?
            // Which color was present on the second ladder in Ladders?
            [Question.LaddersStage2Colors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color was present on the second ladder in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Purple"] = "Lila",
                    ["Gray"] = "Grau",
                },
            },
            // What color was missing on the third ladder in {0}?
            // What color was missing on the third ladder in Ladders?
            [Question.LaddersStage3Missing] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was missing on the third ladder in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Purple"] = "Lila",
                    ["Gray"] = "Grau",
                },
            },

            // Langton’s Anteater
            // Which of these squares was initially {1} in {0}?
            // Which of these squares was initially black in Langton’s Anteater?
            [Question.LangtonsAnteaterInitialState] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these squares was initially {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["black"] = "schwarz",
                    ["white"] = "weiß",
                },
            },

            // Lasers
            // What was the number on the {1} hatch on {0}?
            // What was the number on the top-left hatch on Lasers?
            [Question.LasersHatches] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the number on the {1} hatch on {0}?",
                FormatArgs = new Dictionary<string, string>
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

            // LED Encryption
            // What was the correct letter you pressed in the {1} stage of {0}?
            // What was the correct letter you pressed in the first stage of LED Encryption?
            [Question.LEDEncryptionPressedLetters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the correct letter you pressed in the {1} stage of {0}?",
            },

            // LED Grid
            // How many LEDs were unlit in {0}?
            // How many LEDs were unlit in LED Grid?
            [Question.LEDGridNumBlack] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many LEDs were unlit in {0}?",
            },

            // LED Math
            // What color was {1} in {0}?
            // What color was LED A in LED Math?
            [Question.LEDMathLights] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["LED A"] = "LED A",
                    ["LED B"] = "LED B",
                    ["the operator LED"] = "the operator LED",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                },
            },

            // LEDs
            // What was the initial color of the changed LED in {0}?
            // What was the initial color of the changed LED in LEDs?
            [Question.LEDsOriginalColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial color of the changed LED in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Purple"] = "Lila",
                    ["Black"] = "Schwarz",
                    ["White"] = "Weiß",
                },
            },

            // LEGOs
            // What were the dimensions of the {1} piece in {0}?
            // What were the dimensions of the red piece in LEGOs?
            [Question.LEGOsPieceDimensions] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the dimensions of the {1} piece in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["cyan"] = "türkis",
                    ["magenta"] = "magenta",
                    ["yellow"] = "gelb",
                },
            },

            // Letter Math
            // What was the letter on the {1} display in {0}?
            // What was the letter on the left display in Letter Math?
            [Question.LetterMathDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the letter on the {1} display in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "left",
                    ["right"] = "right",
                },
            },

            // Light Bulbs
            // What was the color of the {1} bulb in {0}?
            // What was the color of the left bulb in Light Bulbs?
            [Question.LightBulbsColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} bulb in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "left",
                    ["right"] = "right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Purple"] = "Lila",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                },
            },

            // Linq
            // What was the {1} function in {0}?
            // What was the first function in Linq?
            [Question.LinqFunction] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} function in {0}?",
            },

            // Lion’s Share
            // Which year was displayed on {0}?
            // Which year was displayed on Lion’s Share?
            [Question.LionsShareYear] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which year was displayed on {0}?",
            },
            // Which lion was present but removed in {0}?
            // Which lion was present but removed in Lion’s Share?
            [Question.LionsShareRemovedLions] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which lion was present but removed in {0}?",
            },

            // Listening
            // What clip was played in {0}?
            // What clip was played in Listening?
            [Question.ListeningSound] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What clip was played in {0}?",
            },

            // Logical Buttons
            // What was the color of the {1} button in the {2} stage of {0}?
            // What was the color of the top button in the first stage of Logical Buttons?
            [Question.LogicalButtonsColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} button in the {2} stage of {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Purple"] = "Lila",
                    ["White"] = "Weiß",
                    ["Orange"] = "Orange",
                    ["Cyan"] = "Türkis",
                    ["Grey"] = "Grey",
                },
            },
            // What was the label on the {1} button in the {2} stage of {0}?
            // What was the label on the top button in the first stage of Logical Buttons?
            [Question.LogicalButtonsLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label on the {1} button in the {2} stage of {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
            },
            // What was the final operator in the {1} stage of {0}?
            // What was the final operator in the first stage of Logical Buttons?
            [Question.LogicalButtonsOperator] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the final operator in the {1} stage of {0}?",
            },

            // Logic Gates
            // What was {1} in {0}?
            // What was gate A in Logic Gates?
            [Question.LogicGatesGates] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
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

            // Lombax Cubes
            // What was the {1} letter on the button in {0}?
            // What was the first letter on the button in Lombax Cubes?
            [Question.LombaxCubesLetters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} letter on the button in {0}?",
            },

            // The London Underground
            // Where did the {1} journey on {0} {2}?
            // Where did the first journey on The London Underground depart from?
            [Question.LondonUndergroundStations] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where did the {1} journey on {0} {2}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["depart from"] = "depart from",
                    ["arrive to"] = "arrive to",
                },
            },

            // Long Words
            // What was the word on the top display on {0}?
            // What was the word on the top display on Long Words?
            [Question.LongWordsWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the word on the top display on {0}?",
            },

            // Mad Memory
            // What was on the display in the {1} stage of {0}?
            // What was on the display in the first stage of Mad Memory?
            [Question.MadMemoryDisplays] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was on the display in the {1} stage of {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first"] = "first",
                    ["second"] = "second",
                    ["third"] = "third",
                    ["4th"] = "4th",
                },
            },

            // Mafia
            // Who was a player, but not the Godfather, in {0}?
            // Who was a player, but not the Godfather, in Mafia?
            [Question.MafiaPlayers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Who was a player, but not the Godfather, in {0}?",
            },

            // Magenta Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Magenta Cipher?
            [Question.MagentaCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Magenta-Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // Mahjong
            // Which tile was part of the {1} matched pair in {0}?
            // Which tile was part of the first matched pair in Mahjong?
            [Question.MahjongMatches] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which tile was part of the {1} matched pair in {0}?",
            },
            // Which tile was shown in the bottom-left of {0}?
            // Which tile was shown in the bottom-left of Mahjong?
            [Question.MahjongCountingTile] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which tile was shown in the bottom-left of {0}?",
            },

            // Main Page
            // Which color did the bubble not display in {0}?
            // Which color did the bubble not display in Main Page?
            [Question.MainPageBubbleColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color did the bubble not display in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                },
            },
            // Which main page did the {1} button’s effect come from in {0}?
            // Which main page did the toons button’s effect come from in Main Page?
            [Question.MainPageButtonEffectOrigin] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which main page did the {1} button's effect come from in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which of the following messages did the bubble {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["display"] = "display",
                    ["not display"] = "not display",
                },
            },
            // Which main page did {1} come from in {0}?
            // Which main page did Homestar come from in Main Page?
            [Question.MainPageHomestarBackgroundOrigin] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which main page did {1} come from in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Homestar"] = "Homestar",
                    ["the background"] = "the background",
                },
            },

            // M&Ms
            // What color was the text on the {1} button in {0}?
            // What color was the text on the first button in M&Ms?
            [Question.MandMsColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the text on the {1} button in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["orange"] = "orange",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                    ["brown"] = "braun",
                },
            },
            // What was the text on the {1} button in {0}?
            // What was the text on the first button in M&Ms?
            [Question.MandMsLabels] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the text on the {1} button in {0}?",
            },

            // M&Ns
            // What color was the text on the {1} button in {0}?
            // What color was the text on the first button in M&Ns?
            [Question.MandNsColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the text on the {1} button in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["orange"] = "orange",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                    ["brown"] = "braun",
                },
            },
            // What was the text on the correct button in {0}?
            // What was the text on the correct button in M&Ns?
            [Question.MandNsLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the text on the correct button in {0}?",
            },

            // Maritime Flags
            // What bearing was signalled in {0}?
            // What bearing was signalled in Maritime Flags?
            [Question.MaritimeFlagsBearing] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What bearing was signalled in {0}?",
            },
            // Which callsign was signalled in {0}?
            // Which callsign was signalled in Maritime Flags?
            [Question.MaritimeFlagsCallsign] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which callsign was signalled in {0}?",
            },

            // Maritime Semaphore
            // In which position was the dummy in {0}?
            // In which position was the dummy in Maritime Semaphore?
            [Question.MaritimeSemaphoreDummy] = new()
            {
                NeedsTranslation = true,
                QuestionText = "In which position was the dummy in {0}?",
            },
            // Which letter was shown by the {2} in the {1} position in {0}?
            // Which letter was shown by the left flag in the first position in Maritime Semaphore?
            [Question.MaritimeSemaphoreLetter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which letter was shown by the {2} for the {1} position in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left flag"] = "left flag",
                    ["right flag"] = "right flag",
                    ["semaphore"] = "semaphore",
                },
            },

            // The Maroon Button
            // What was A in {0}?
            // What was A in The Maroon Button?
            [Question.MaroonButtonA] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was A in {0}?",
            },

            // Maroon Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Maroon Cipher?
            [Question.MaroonCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Kastanien-Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // Mashematics
            // What was the answer in {0}?
            // What was the answer in Mashematics?
            [Question.MashematicsAnswer] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the answer in {0}?",
            },
            // What was the {1} number in the equation on {0}?
            // What was the first number in the equation on Mashematics?
            [Question.MashematicsCalculation] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} number in the equation on {0}?",
            },

            // Master Tapes
            // Which song was played in {0}?
            // Which song was played in Master Tapes?
            [Question.MasterTapesPlayedSong] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which song was played in {0}?",
            },

            // Match Refereeing
            // Which planet was present in the {1} stage of {0}?
            // Which planet was present in the first stage of Match Refereeing?
            [Question.MatchRefereeingPlanet] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which planet was present in the {1} stage of {0}?",
            },

            // Math ’em
            // What was the color of this tile before the shuffle on {0}?
            // What was the color of this tile before the shuffle on Math ’em?
            [Question.MathEmColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of this tile before the shuffle on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Weiß",
                    ["Bronze"] = "Bronze",
                    ["Silver"] = "Silver",
                    ["Gold"] = "Gold",
                },
            },
            // What was the design on this tile before the shuffle on {0}?
            // What was the design on this tile before the shuffle on Math ’em?
            [Question.MathEmLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the design on this tile before the shuffle on {0}?",
            },

            // The Matrix
            // Which word was part of the latest access code in {0}?
            // Which word was part of the latest access code in The Matrix?
            [Question.MatrixAccessCode] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which word was part of the latest access code in {0}?",
            },
            // What was the glitched word in {0}?
            // What was the glitched word in The Matrix?
            [Question.MatrixGlitchWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the glitched word in {0}?",
            },

            // Maze
            // In which {1} was the starting position in {0}, counting from the {2}?
            // In which column was the starting position in Maze, counting from the left?
            [Question.MazeStartingPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "In which {1} was the starting position in {0}, counting from the {2}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["column"] = "column",
                    ["left"] = "left",
                    ["row"] = "row",
                    ["top"] = "top",
                },
            },

            // Maze³
            // What was the color of the starting face in {0}?
            // What was the color of the starting face in Maze³?
            [Question.Maze3StartingFace] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the starting face in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Magenta"] = "Magenta",
                    ["Orange"] = "Orange",
                },
            },

            // Maze Identification
            // What was the seed of the maze in {0}?
            // What was the seed of the maze in Maze Identification?
            [Question.MazeIdentificationSeed] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the seed of the maze in {0}?",
            },
            // What was the function of button {1} in {0}?
            // What was the function of button 1 in Maze Identification?
            [Question.MazeIdentificationNum] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the function of button {1} in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Forwards"] = "Forwards",
                    ["Clockwise"] = "Clockwise",
                    ["Backwards"] = "Backwards",
                    ["Counter-clockwise"] = "Counter-clockwise",
                },
            },
            // Which button {1} in {0}?
            // Which button moved you forwards in Maze Identification?
            [Question.MazeIdentificationFunc] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which button {1} in {0}?",
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
            [Question.MazematicsValue] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which was the {1} value in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["initial"] = "initial",
                    ["goal"] = "goal",
                },
            },

            // Maze Scrambler
            // What was the starting position on {0}?
            // What was the starting position on Maze Scrambler?
            [Question.MazeScramblerStart] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting position on {0}?",
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
            // What was the goal on {0}?
            // What was the goal on Maze Scrambler?
            [Question.MazeScramblerGoal] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the goal on {0}?",
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
            // Which of these positions was a maze marking on {0}?
            // Which of these positions was a maze marking on Maze Scrambler?
            [Question.MazeScramblerIndicators] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these positions was a maze marking on {0}?",
                Answers = new Dictionary<string, string>
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

            // Mazeseeker
            // How many walls surrounded this cell in {0}?
            // How many walls surrounded this cell in Mazeseeker?
            [Question.MazeseekerCell] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many walls surrounded this cell in {0}?",
            },
            // Where was the start in {0}?
            // Where was the start in Mazeseeker?
            [Question.MazeseekerStart] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was the start in {0}?",
            },
            // Where was the goal in {0}?
            // Where was the goal in Mazeseeker?
            [Question.MazeseekerGoal] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was the goal in {0}?",
            },

            // Maze Swap
            // Where was the {1} position in {0}?
            // Where was the starting position in Maze Swap?
            [Question.MazeSwapPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was the {1} position in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["starting"] = "starting",
                    ["goal"] = "goal",
                },
            },

            // Mega Man 2
            // Which master was shown in {0}?
            // Which master was shown in Mega Man 2?
            [Question.MegaMan2Master] = new()
            {
                QuestionText = "Welcher Meister war bei {0} zu sehen?",
            },
            // Which weapon was shown in {0}?
            // Which weapon was shown in Mega Man 2?
            [Question.MegaMan2Weapon] = new()
            {
                QuestionText = "Welche Waffe war bei {0} zu sehen?",
            },

            // Melody Sequencer
            // Which part was in slot #{1} at the start of {0}?
            // Which part was in slot #1 at the start of Melody Sequencer?
            [Question.MelodySequencerSlots] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which part was in slot #{1} at the start of {0}?",
            },
            // Which slot contained part #{1} at the start of {0}?
            // Which slot contained part #1 at the start of Melody Sequencer?
            [Question.MelodySequencerParts] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which slot contained part #{1} at the start of {0}?",
            },

            // Memorable Buttons
            // What was the {1} correct symbol pressed in {0}?
            // What was the first correct symbol pressed in Memorable Buttons?
            [Question.MemorableButtonsSymbols] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} correct symbol pressed in {0}?",
            },

            // Memory
            // What was the displayed number in the {1} stage of {0}?
            // What was the displayed number in the first stage of Memory?
            [Question.MemoryDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed number in the {1} stage of {0}?",
            },
            // In what position was the button that you pressed in the {1} stage of {0}?
            // In what position was the button that you pressed in the first stage of Memory?
            [Question.MemoryPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "In what position was the button that you pressed in the {1} stage of {0}?",
            },
            // What was the label of the button that you pressed in the {1} stage of {0}?
            // What was the label of the button that you pressed in the first stage of Memory?
            [Question.MemoryLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label of the button that you pressed in the {1} stage of {0}?",
            },

            // Memory Wires
            // What was the digit displayed in the {1} stage of {0}?
            // What was the digit displayed in the first stage of Memory Wires?
            [Question.MemoryWiresDisplayedDigits] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the digit displayed in the {1} stage of {0}?",
            },
            // What was the colour of wire {1} in {0}?
            // What was the colour of wire 1 in Memory Wires?
            [Question.MemoryWiresWireColours] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the colour of wire {1} in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Blue"] = "Blau",
                    ["White"] = "Weiß",
                    ["Black"] = "Schwarz",
                },
            },

            // Metamorse
            // What was the extracted letter in {0}?
            // What was the extracted letter in Metamorse?
            [Question.MetamorseExtractedLetter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the extracted letter in {0}?",
            },

            // Metapuzzle
            // What was the final answer in {0}?
            // What was the final answer in Metapuzzle?
            [Question.MetapuzzleAnswer] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the final answer in {0}?",
            },

            // Microcontroller
            // Which pin lit up {1} in {0}?
            // Which pin lit up first in Microcontroller?
            [Question.MicrocontrollerPinOrder] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which pin lit up {1} in {0}?",
            },

            // Minesweeper
            // What was the color of the starting cell in {0}?
            // What was the color of the starting cell in Minesweeper?
            [Question.MinesweeperStartingColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the starting cell in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["orange"] = "orange",
                    ["yellow"] = "gelb",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["purple"] = "lila",
                    ["black"] = "schwarz",
                },
            },

            // Mirror
            // What was the second word written by the original ghost in {0}?
            // What was the second word written by the original ghost in Mirror?
            [Question.MirrorWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the second word written by the original ghost in {0}?",
            },

            // Mister Softee
            // Where was the SpongeBob Bar on {0}?
            // Where was the SpongeBob Bar on Mister Softee?
            [Question.MisterSofteeSpongebobPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was the SpongeBob Bar on {0}?",
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
            [Question.MisterSofteeTreatsPresent] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which treat was present on {0}?",
            },

            // Mixometer
            // What was the position of the submit button in {0}?
            // What was the position of the submit button in Mixometer?
            [Question.MixometerSubmitButton] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the position of the submit button in {0}?",
            },

            // Modern Cipher
            // What was the decrypted word of the {1} stage in {0}?
            // What was the decrypted word of the first stage in Modern Cipher?
            [Question.ModernCipherWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the decrypted word of the {1} stage in {0}?",
            },

            // Module Listening
            // Which sound did the {1} button play in {0}?
            // Which sound did the red button play in Module Listening?
            [Question.ModuleListeningButtonAudio] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which sound did the {1} button play in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                },
            },
            // Which sound played in {0}?
            // Which sound played in Module Listening?
            [Question.ModuleListeningAnyAudio] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which sound played in {0}?",
            },

            // Module Maze
            // Which of the following was the starting icon for {0}?
            // Which of the following was the starting icon for Module Maze?
            [Question.ModuleMazeStartingIcon] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of the following was the starting icon for {0}?",
            },

            // Module Movements
            // What was the {1} module shown in {0}?
            // What was the first module shown in Module Movements?
            [Question.ModuleMovementsDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} module shown in {0}?",
            },

            // Money Game
            // What were the first and second words in the {1} phrase in {0}?
            // What were the first and second words in the first phrase in Money Game?
            [Question.MoneyGame1] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the first and second words in the {1} phrase in {0}?",
            },
            // What were the third and fourth words in the {1} phrase in {0}?
            // What were the third and fourth words in the first phrase in Money Game?
            [Question.MoneyGame2] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the third and fourth words in the {1} phrase in {0}?",
            },
            // What was the end of the {1} phrase in {0}?
            // What was the end of the first phrase in Money Game?
            [Question.MoneyGame3] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the end of the {1} phrase in {0}?",
            },

            // Monsplode, Fight!
            // Which creature was displayed in {0}?
            // Which creature was displayed in Monsplode, Fight!?
            [Question.MonsplodeFightCreature] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which creature was displayed in {0}?",
            },
            // Which one of these moves {1} selectable in {0}?
            // Which one of these moves was selectable in Monsplode, Fight!?
            [Question.MonsplodeFightMove] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which one of these moves {1} selectable in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "was",
                    ["was not"] = "was not",
                },
            },

            // Monsplode Trading Cards
            // What was the {1} before the last action in {0}?
            // What was the first card in your hand before the last action in Monsplode Trading Cards?
            [Question.MonsplodeTradingCardsCards] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} before the last action in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first card in your hand"] = "first card in your hand",
                    ["second card in your hand"] = "second card in your hand",
                    ["third card in your hand"] = "third card in your hand",
                    ["card on offer"] = "card on offer",
                },
            },
            // What was the print version of the {1} before the last action in {0}?
            // What was the print version of the first card in your hand before the last action in Monsplode Trading Cards?
            [Question.MonsplodeTradingCardsPrintVersions] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the print version of the {1} before the last action in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first card in your hand"] = "first card in your hand",
                    ["second card in your hand"] = "second card in your hand",
                    ["third card in your hand"] = "third card in your hand",
                    ["card on offer"] = "card on offer",
                },
            },

            // The Moon
            // What was the {1} set in clockwise order in {0}?
            // What was the first initially lit set in clockwise order in The Moon?
            [Question.MoonLitUnlit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} set in clockwise order in {0}?",
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
            [Question.MoreCodeWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the flashing word in {0}?",
            },

            // Morse-A-Maze
            // What was the starting location in {0}?
            // What was the starting location in Morse-A-Maze?
            [Question.MorseAMazeStartingCoordinate] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting location in {0}?",
            },
            // What was the ending location in {0}?
            // What was the ending location in Morse-A-Maze?
            [Question.MorseAMazeEndingCoordinate] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the ending location in {0}?",
            },
            // What was the word shown as Morse code in {0}?
            // What was the word shown as Morse code in Morse-A-Maze?
            [Question.MorseAMazeMorseCodeWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the word shown as Morse code in {0}?",
            },

            // Morse Buttons
            // What was the character flashed by the {1} button in {0}?
            // What was the character flashed by the first button in Morse Buttons?
            [Question.MorseButtonsButtonLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the character flashed by the {1} button in {0}?",
            },
            // What was the color flashed by the {1} button in {0}?
            // What was the color flashed by the first button in Morse Buttons?
            [Question.MorseButtonsButtonColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color flashed by the {1} button in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["yellow"] = "gelb",
                    ["orange"] = "orange",
                    ["purple"] = "lila",
                },
            },

            // Morsematics
            // What was the {1} received letter in {0}?
            // What was the first received letter in Morsematics?
            [Question.MorsematicsReceivedLetters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} received letter in {0}?",
            },

            // Morse War
            // What were the LEDs in the {1} row in {0} (1 = on, 0 = off)?
            // What were the LEDs in the bottom row in Morse War (1 = on, 0 = off)?
            [Question.MorseWarLeds] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the LEDs in the {1} row in {0} (1 = on, 0 = off)?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["bottom"] = "bottom",
                    ["middle"] = "middle",
                    ["top"] = "top",
                },
            },
            // What code was transmitted in {0}?
            // What code was transmitted in Morse War?
            [Question.MorseWarCode] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What code was transmitted in {0}?",
            },

            // Mouse in the Maze
            // What color was the torus in {0}?
            // What color was the torus in Mouse in the Maze?
            [Question.MouseInTheMazeTorus] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the torus in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["white"] = "weiß",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                },
            },
            // Which color sphere was the goal in {0}?
            // Which color sphere was the goal in Mouse in the Maze?
            [Question.MouseInTheMazeSphere] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color sphere was the goal in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["white"] = "weiß",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                },
            },

            // M-Seq
            // What was the {1} obtained digit in {0}?
            // What was the first obtained digit in M-Seq?
            [Question.MSeqObtained] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} obtained digit in {0}?",
            },
            // What was the final number from the iteration process in {0}?
            // What was the final number from the iteration process in M-Seq?
            [Question.MSeqSubmitted] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the final number from the iteration process in {0}?",
            },

            // Mssngv Wls
            // Which vowel was missing in {0}?
            // Which vowel was missing in \uE001Mssngv Wls\uE002?
            [Question.MssngvWlsMssNgvwL] = new()
            {
                NeedsTranslation = true,
                Gender = Gender.Plural,
                ModuleNameDative = "\uE001Fh Lnd Nvkln\uE002",
                QuestionText = "Welcher Vokal hat bei {0} gefehlt?",
                ModuleName = "\uE001Fhl Ndv Kl\uE002",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["AEIOU"] = "AEIOUÄÖÜ",
                },
            },

            // Multicolored Switches
            // What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?
            // What color was the first LED on the top row when the tiny LED was lit in Multicolored Switches?
            [Question.MulticoloredSwitchesLedColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["lit"] = "lit",
                    ["bottom"] = "bottom",
                    ["unlit"] = "unlit",
                },
                Answers = new Dictionary<string, string>
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

            // Murder
            // Where was the body found in {0}?
            // Where was the body found in Murder?
            [Question.MurderBodyFound] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was the body found in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which of these was {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["a suspect but not the murderer"] = "a suspect but not the murderer",
                    ["not a suspect"] = "not a suspect",
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
                NeedsTranslation = true,
                QuestionText = "Which of these was {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["a potential weapon but not the murder weapon"] = "a potential weapon but not the murder weapon",
                    ["not a potential weapon"] = "not a potential weapon",
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
                NeedsTranslation = true,
                QuestionText = "Which module was the first requested to be solved by {0}?",
            },
            // Which module was hidden by {0}?
            // Which module was hidden by Mystery Module?
            [Question.MysteryModuleHiddenModule] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which module was hidden by {0}?",
            },

            // Mystic Square
            // Where was the skull in {0}?
            // Where was the skull in Mystic Square?
            [Question.MysticSquareSkull] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was the skull in {0}?",
                Answers = new Dictionary<string, string>
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

            // Name Codes
            // What was the {1} index in {0}?
            // What was the left index in Name Codes?
            [Question.NameCodesIndices] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} index in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "left",
                    ["right"] = "right",
                },
            },

            // Naming Conventions
            // What was the label of the first button in {0}?
            // What was the label of the first button in Naming Conventions?
            [Question.NamingConventionsObject] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label of the first button in {0}?",
            },

            // N&Ms
            // What was the label of the correct button in {0}?
            // What was the label of the correct button in N&Ms?
            [Question.NandMsAnswer] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label of the correct button in {0}?",
            },

            // N&Ns
            // Which label was present in the {1} stage of {0}?
            // Which label was present in the first stage of N&Ns?
            [Question.NandNsLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which label was present in the {1} stage of {0}?",
            },
            // Which color was missing in the third stage of {0}?
            // Which color was missing in the third stage of N&Ns?
            [Question.NandNsColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color was missing in the third stage of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Red",
                    ["Green"] = "Green",
                    ["Orange"] = "Orange",
                    ["Blue"] = "Blue",
                    ["Yellow"] = "Yellow",
                    ["Brown"] = "Brown",
                },
            },

            // Navigation Determination
            // What was the color of the maze in {0}?
            // What was the color of the maze in Navigation Determination?
            [Question.NavigationDeterminationColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the maze in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                },
            },
            // What was the label of the maze in {0}?
            // What was the label of the maze in Navigation Determination?
            [Question.NavigationDeterminationLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label of the maze in {0}?",
            },

            // Navinums
            // What was the initial middle digit in {0}?
            // What was the initial middle digit in Navinums?
            [Question.NavinumsMiddleDigit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial middle digit in {0}?",
            },
            // What was the {1} directional button pressed in {0}?
            // What was the first directional button pressed in Navinums?
            [Question.NavinumsDirectionalButtons] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} directional button pressed in {0}?",
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
            [Question.NavyButtonGreekLetters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which Greek letter appeared on {0} (case-sensitive)?",
            },
            // What was the {1} of the given in {0} (0-indexed)?
            // What was the column of the given in The Navy Button (0-indexed)?
            [Question.NavyButtonGiven] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} of the given in {0} (0-indexed)?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["column"] = "column",
                    ["row"] = "row",
                    ["value"] = "value",
                },
            },

            // The Necronomicon
            // What was the chapter number of the {1} page in {0}?
            // What was the chapter number of the first page in The Necronomicon?
            [Question.NecronomiconChapters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the chapter number of the {1} page in {0}?",
            },

            // Negativity
            // In base 10, what was the value submitted in {0}?
            // In base 10, what was the value submitted in Negativity?
            [Question.NegativitySubmittedValue] = new()
            {
                NeedsTranslation = true,
                QuestionText = "In base 10, what was the value submitted in {0}?",
            },
            // Excluding 0s, what was the submitted balanced ternary in {0}?
            // Excluding 0s, what was the submitted balanced ternary in Negativity?
            [Question.NegativitySubmittedTernary] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Excluding 0s, what was the submitted balanced ternary in {0}?",
            },

            // Neutralization
            // What was the acid’s color in {0}?
            // What was the acid’s color in Neutralization?
            [Question.NeutralizationColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the acid’s color in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                },
            },
            // What was the acid’s volume in {0}?
            // What was the acid’s volume in Neutralization?
            [Question.NeutralizationVolume] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the acid’s volume in {0}?",
            },

            // Next In Line
            // What color was the first wire in {0}?
            // What color was the first wire in Next In Line?
            [Question.NextInLineFirstWire] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the first wire in {0}?",
                Answers = new Dictionary<string, string>
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

            // ❖
            // Which button flashed in the {1} stage in {0}?
            // Which button flashed in the first stage in ❖?
            // Note: This question is depicted visually, rather than with words. A translation here will only be used for logging.
            [Question.NonverbalSimonFlashes] = new()
            {
                QuestionText = "Welcher Knopf ist bei {0} in der {1}en Stufe aufgeleuchtet?",
            },

            // Not Colored Squares
            // What was the position of the square you initially pressed in {0}?
            // What was the position of the square you initially pressed in Not Colored Squares?
            [Question.NotColoredSquaresInitialPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the position of the square you initially pressed in {0}?",
            },

            // Not Colored Switches
            // What was the encrypted word in {0}?
            // What was the encrypted word in Not Colored Switches?
            [Question.NotColoredSwitchesWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the encrypted word in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What symbol flashed on the {1} button in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top left"] = "top left",
                    ["top right"] = "top right",
                    ["bottom left"] = "bottom left",
                    ["bottom right"] = "bottom right",
                },
            },
            // What was the value of the {1} button in {0}?
            // What was the value of the top left button in Not Connection Check?
            [Question.NotConnectionCheckValues] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the value of the {1} button in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top left"] = "top left",
                    ["top right"] = "top right",
                    ["bottom left"] = "bottom left",
                    ["bottom right"] = "bottom right",
                },
            },

            // Not Coordinates
            // Which coordinate was part of the square in {0}?
            // Which coordinate was part of the square in Not Coordinates?
            [Question.NotCoordinatesSquareCoords] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which coordinate was part of the square in {0}?",
            },

            // Not Double-Oh
            // What was the {1} displayed position in the second stage of {0}?
            // What was the first displayed position in the second stage of Not Double-Oh?
            [Question.NotDoubleOhPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} displayed position in the second stage of {0}?",
            },

            // Not Keypad
            // What color flashed {1} in the final sequence in {0}?
            // What color flashed first in the final sequence in Not Keypad?
            [Question.NotKeypadColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color flashed {1} in the final sequence in {0}?",
                Answers = new Dictionary<string, string>
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
                    ["grey"] = "grey",
                    ["white"] = "weiß",
                },
            },
            // Which symbol was on the button that flashed {1} in the final sequence in {0}?
            // Which symbol was on the button that flashed first in the final sequence in Not Keypad?
            [Question.NotKeypadSymbol] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which symbol was on the button that flashed {1} in the final sequence in {0}?",
            },

            // Not Maze
            // What was the starting distance in {0}?
            // What was the starting distance in Not Maze?
            [Question.NotMazeStartingDistance] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting distance in {0}?",
            },

            // Not Morse Code
            // What was the {1} correct word you submitted in {0}?
            // What was the first correct word you submitted in Not Morse Code?
            [Question.NotMorseCodeWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} correct word you submitted in {0}?",
            },

            // Not Morsematics
            // What was the transmitted word on {0}?
            // What was the transmitted word on Not Morsematics?
            [Question.NotMorsematicsWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the transmitted word on {0}?",
            },

            // Not Murder
            // What room was {1} in initially on {0}?
            // What room was Miss Scarlett in initially on Not Murder?
            [Question.NotMurderRoom] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What room was {1} in during {2} on {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What weapon did {1} possess during {2} on {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which of these numbers {1} at the {2} stage of {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["flashed"] = "flashed",
                    ["did not flash"] = "did not flash",
                },
            },

            // Not Password
            // Which letter was missing from {0}?
            // Which letter was missing from Not Password?
            [Question.NotPasswordLetter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which letter was missing from {0}?",
            },

            // Not Perspective Pegs
            // What was the position of the {1} flashing peg on {0}?
            // What was the position of the first flashing peg on Not Perspective Pegs?
            [Question.NotPerspectivePegsPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the position of the {1} flashing peg on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["top-right"] = "top-right",
                    ["bottom-right"] = "bottom-right",
                    ["bottom-left"] = "bottom-left",
                    ["top-left"] = "top-left",
                },
            },
            // From what perspective did the {1} peg flash on {0}?
            // From what perspective did the first peg flash on Not Perspective Pegs?
            [Question.NotPerspectivePegsPerspective] = new()
            {
                NeedsTranslation = true,
                QuestionText = "From what perspective did the {1} peg flash on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["top-right"] = "top-right",
                    ["bottom-right"] = "bottom-right",
                    ["bottom-left"] = "bottom-left",
                    ["top-left"] = "top-left",
                },
            },
            // What was the color of the {1} flashing peg on {0}?
            // What was the color of the first flashing peg on Not Perspective Pegs?
            [Question.NotPerspectivePegsColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} flashing peg on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "blue",
                    ["green"] = "green",
                    ["purple"] = "purple",
                    ["red"] = "red",
                    ["yellow"] = "yellow",
                },
            },

            // Not Piano Keys
            // What was the first displayed symbol on {0}?
            // What was the first displayed symbol on Not Piano Keys?
            [Question.NotPianoKeysFirstSymbol] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the first displayed symbol on {0}?",
            },
            // What was the second displayed symbol on {0}?
            // What was the second displayed symbol on Not Piano Keys?
            [Question.NotPianoKeysSecondSymbol] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the second displayed symbol on {0}?",
            },
            // What was the third displayed symbol on {0}?
            // What was the third displayed symbol on Not Piano Keys?
            [Question.NotPianoKeysThirdSymbol] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the third displayed symbol on {0}?",
            },

            // Not Red Arrows
            // What was the starting number in {0}?
            // What was the starting number in Not Red Arrows?
            [Question.NotRedArrowsStart] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting number in {0}?",
            },

            // Not Simaze
            // Which maze was used in {0}?
            // Which maze was used in Not Simaze?
            [Question.NotSimazeMaze] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which maze was used in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["orange"] = "orange",
                    ["yellow"] = "gelb",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["purple"] = "lila",
                },
            },
            // What was the starting position in {0}?
            // What was the starting position in Not Simaze?
            [Question.NotSimazeStart] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting position in {0}?",
                Answers = new Dictionary<string, string>
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
            // What was the goal position in {0}?
            // What was the goal position in Not Simaze?
            [Question.NotSimazeGoal] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the goal position in {0}?",
                Answers = new Dictionary<string, string>
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

            // Not Text Field
            // Which letter was pressed in the first stage of {0}?
            // Which letter was pressed in the first stage of Not Text Field?
            [Question.NotTextFieldInitialPresses] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which letter was pressed in the first stage of {0}?",
            },
            // Which letter appeared 9 times at the start of {0}?
            // Which letter appeared 9 times at the start of Not Text Field?
            [Question.NotTextFieldBackgroundLetter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which letter appeared 9 times at the start of {0}?",
            },

            // Not The Bulb
            // What word flashed on {0}?
            // What word flashed on Not The Bulb?
            [Question.NotTheBulbWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What word flashed on {0}?",
            },
            // What color was the bulb on {0}?
            // What color was the bulb on Not The Bulb?
            [Question.NotTheBulbColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the bulb on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Yellow"] = "Gelb",
                    ["Purple"] = "Lila",
                    ["White"] = "Weiß",
                },
            },
            // What was the material of the screw cap on {0}?
            // What was the material of the screw cap on Not The Bulb?
            [Question.NotTheBulbScrewCap] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the material of the screw cap on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Copper"] = "Copper",
                    ["Silver"] = "Silver",
                    ["Gold"] = "Gold",
                    ["Plastic"] = "Plastic",
                    ["Carbon Fibre"] = "Carbon Fibre",
                    ["Ceramic"] = "Ceramic",
                },
            },

            // Not the Button
            // What colors did the light glow in {0}?
            // What colors did the light glow in Not the Button?
            [Question.NotTheButtonLightColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What colors did the light glow in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["white"] = "weiß",
                    ["red"] = "rot",
                    ["yellow"] = "gelb",
                    ["green"] = "grün",
                    ["blue"] = "blau",
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

            // Not The Plunger Button
            // What color did the background flash in {0}?
            // What color did the background flash in Not The Plunger Button?
            [Question.NotThePlungerButtonBackground] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color did the background flash in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What was the initial position in {0}?",
            },

            // Not Who’s on First
            // In which position was the button you pressed in the {1} stage on {0}?
            // In which position was the button you pressed in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstPressedPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "In which position was the button you pressed in the {1} stage on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "top left",
                    ["top right"] = "top right",
                    ["middle left"] = "middle left",
                    ["middle right"] = "middle right",
                    ["bottom left"] = "bottom left",
                    ["bottom right"] = "bottom right",
                },
            },
            // What was the label on the button you pressed in the {1} stage on {0}?
            // What was the label on the button you pressed in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstPressedLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label on the button you pressed in the {1} stage on {0}?",
            },
            // In which position was the reference button in the {1} stage on {0}?
            // In which position was the reference button in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstReferencePosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "In which position was the reference button in the {1} stage on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["top left"] = "top left",
                    ["top right"] = "top right",
                    ["middle left"] = "middle left",
                    ["middle right"] = "middle right",
                    ["bottom left"] = "bottom left",
                    ["bottom right"] = "bottom right",
                },
            },
            // What was the label on the reference button in the {1} stage on {0}?
            // What was the label on the reference button in the first stage on Not Who’s on First?
            [Question.NotWhosOnFirstReferenceLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label on the reference button in the {1} stage on {0}?",
            },
            // What was the calculated number in the second stage on {0}?
            // What was the calculated number in the second stage on Not Who’s on First?
            [Question.NotWhosOnFirstSum] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the calculated number in the second stage on {0}?",
            },

            // Not Word Search
            // Which of these consonants was missing in {0}?
            // Which of these consonants was missing in Not Word Search?
            [Question.NotWordSearchMissing] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these consonants was missing in {0}?",
            },
            // What was the first correctly pressed letter in {0}?
            // What was the first correctly pressed letter in Not Word Search?
            [Question.NotWordSearchFirstPress] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the first correctly pressed letter in {0}?",
            },

            // Not X01
            // Which sector value {1} present on {0}?
            // Which sector value was present on Not X01?
            [Question.NotX01SectorValues] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which sector value {1} present on {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "was",
                    ["was not"] = "was not",
                },
            },

            // Not X-Ray
            // What table were we in in {0} (numbered 1–8 in reading order in the manual)?
            // What table were we in in Not X-Ray (numbered 1–8 in reading order in the manual)?
            [Question.NotXRayTable] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What table were we in in {0} (numbered 1–8 in reading order in the manual)?",
            },
            // What direction was button {1} in {0}?
            // What direction was button 1 in Not X-Ray?
            [Question.NotXRayDirections] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What direction was button {1} in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Up",
                    ["Right"] = "Right",
                    ["Down"] = "Down",
                    ["Left"] = "Left",
                },
            },
            // Which button went {1} in {0}?
            // Which button went up in Not X-Ray?
            [Question.NotXRayButtons] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which button went {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["up"] = "up",
                    ["right"] = "right",
                    ["down"] = "down",
                    ["left"] = "left",
                },
            },
            // What was the scanner color in {0}?
            // What was the scanner color in Not X-Ray?
            [Question.NotXRayScannerColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the scanner color in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Blue"] = "Blau",
                    ["White"] = "Weiß",
                },
            },

            // Numbered Buttons
            // Which number was correctly pressed on {0}?
            // Which number was correctly pressed on Numbered Buttons?
            [Question.NumberedButtonsButtons] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which number was correctly pressed on {0}?",
            },

            // The Number Game
            // What was the maximum number in {0}?
            // What was the maximum number in The Number Game?
            [Question.NumberGameMaximum] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the maximum number in {0}?",
            },

            // Numbers
            // What two-digit number was given in {0}?
            // What two-digit number was given in Numbers?
            [Question.NumbersTwoDigit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What two-digit number was given in {0}?",
            },

            // Numpath
            // What was the color of the number on {0}?
            // What was the color of the number on Numpath?
            [Question.NumpathColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the number on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Purple"] = "Lila",
                },
            },
            // What was the number displayed on {0}?
            // What was the number displayed on Numpath?
            [Question.NumpathDigit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the number displayed on {0}?",
            },

            // Object Shows
            // Which of these was a contestant on {0}?
            // Which of these was a contestant on Object Shows?
            [Question.ObjectShowsContestants] = new()
            {
                QuestionText = "Which of these was a contestant on {0} but not the final winner?",
            },

            // The Octadecayotton
            // What was the starting sphere in {0}?
            // What was the starting sphere in The Octadecayotton?
            [Question.OctadecayottonSphere] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting sphere in {0}?",
            },
            // What was one of the subrotations in the {1} rotation in {0}?
            // What was one of the subrotations in the first rotation in The Octadecayotton?
            [Question.OctadecayottonRotations] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was one of the subrotations in the {1} rotation in {0}?",
            },

            // Odd One Out
            // What was the button you pressed in the {1} stage of {0}?
            // What was the button you pressed in the first stage of Odd One Out?
            [Question.OddOneOutButton] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the button you pressed in the {1} stage of {0}?",
            },

            // Old AI
            // What was the {1} of the numbers shown in {0}?
            // What was the group of the numbers shown in Old AI?
            [Question.OldAIGroup] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} of the numbers shown in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["group"] = "group",
                    ["sub-group"] = "sub-group",
                },
            },

            // Old Fogey
            // What was the initial color of the status light in {0}?
            // What was the initial color of the status light in Old Fogey?
            [Question.OldFogeyStartingColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial color of the status light in {0}?",
                Answers = new Dictionary<string, string>
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

            // One Links To All
            // What was the starting article in {0}?
            // What was the starting article in One Links To All?
            [Question.OneLinksToAllStart] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting article in {0}?",
            },
            // What was the ending article in {0}?
            // What was the ending article in One Links To All?
            [Question.OneLinksToAllEnd] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the ending article in {0}?",
            },

            // Only Connect
            // Which Egyptian hieroglyph was in the {1} in {0}?
            // Which Egyptian hieroglyph was in the top left in Only Connect?
            [Question.OnlyConnectHieroglyphs] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which Egyptian hieroglyph was in the {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top left"] = "top left",
                    ["top middle"] = "top middle",
                    ["top right"] = "top right",
                    ["bottom left"] = "bottom left",
                    ["bottom middle"] = "bottom middle",
                    ["bottom right"] = "bottom right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Two Reeds"] = "Two Reeds",
                    ["Lion"] = "Lion",
                    ["Twisted Flax"] = "Twisted Flax",
                    ["Horned Viper"] = "Horned Viper",
                    ["Water"] = "Water",
                    ["Eye of Horus"] = "Eye of Horus",
                },
            },

            // Orange Arrows
            // What was the {1} arrow on the display of the {2} stage of {0}?
            // What was the first arrow on the display of the first stage of Orange Arrows?
            [Question.OrangeArrowsSequences] = new()
            {
                QuestionText = "Was war bei {0} in der {2} Stufe der {1}e Pfeil auf dem Display?",
                Answers = new Dictionary<string, string>
                {
                    ["Up"] = "Hoch",
                    ["Right"] = "Rechts",
                    ["Down"] = "Runter",
                    ["Left"] = "Links",
                },
            },

            // Orange Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Orange Cipher?
            [Question.OrangeCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameDative = "Orangenen Geheimschrift",
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Orangene Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // Ordered Keys
            // What color was this key in the {1} stage of {0}?
            // What color was this key in the first stage of Ordered Keys?
            [Question.OrderedKeysColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {2} key in the {1} stage of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                },
            },
            // What was the label of this key in the {1} stage of {0}?
            // What was the label of this key in the first stage of Ordered Keys?
            [Question.OrderedKeysLabels] = new()
            {
                QuestionText = "What was the label on the {2} key in the {1} stage of {0}?",
            },
            // What color was the label of this key in the {1} stage of {0}?
            // What color was the label of this key in the first stage of Ordered Keys?
            [Question.OrderedKeysLabelColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the label of the {2} key in the {1} stage of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                },
            },

            // Order Picking
            // What was the order ID in the {1} order of {0}?
            // What was the order ID in the first order of Order Picking?
            [Question.OrderPickingOrder] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} die Auftragsnummer des {1}en Auftrags?",
                ModuleName = "Kommissionierung",
            },
            // What was the product ID in the {1} order of {0}?
            // What was the product ID in the first order of Order Picking?
            [Question.OrderPickingProduct] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} die Produktnummer des {1}en Auftrags?",
                ModuleName = "Kommissionierung",
            },
            // What was the pallet in the {1} order of {0}?
            // What was the pallet in the first order of Order Picking?
            [Question.OrderPickingPallet] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} die Palette für den {1}en Auftrag?",
                ModuleName = "Kommissionierung",
            },

            // Orientation Cube
            // What was the observer’s initial position in {0}?
            // What was the observer’s initial position in Orientation Cube?
            [Question.OrientationCubeInitialObserverPosition] = new()
            {
                Gender = Gender.Masculine,
                QuestionText = "Was war bei {0} die Anfangsposition des Beobachters?",
                ModuleName = "Orientierungswürfel",
                Answers = new Dictionary<string, string>
                {
                    ["front"] = "vorne",
                    ["left"] = "links",
                    ["back"] = "hinten",
                    ["right"] = "rechts",
                },
            },

            // Orientation Hypercube
            // What was the observer’s initial position in {0}?
            // What was the observer’s initial position in Orientation Hypercube?
            [Question.OrientationHypercubeInitialObserverPosition] = new()
            {
                Gender = Gender.Masculine,
                QuestionText = "Was war bei {0} die Anfangsposition des Beobachters?",
                ModuleName = "Orientierungshyperwürfel",
                Answers = new Dictionary<string, string>
                {
                    ["front"] = "vorne",
                    ["left"] = "links",
                    ["back"] = "hinten",
                    ["right"] = "rechts",
                },
            },
            // What was the initial colour of the {1} face in {0}?
            // What was the initial colour of the right face in Orientation Hypercube?
            [Question.OrientationHypercubeInitialFaceColour] = new()
            {
                NeedsTranslation = true,
                Gender = Gender.Masculine,
                QuestionText = "Was war bei {0} die Anfangsfarbe der {1} Seite?",
                ModuleName = "Orientierungshyperwürfel",
                FormatArgs = new Dictionary<string, string>
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
                Answers = new Dictionary<string, string>
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

            // Palindromes
            // What was {1}’s {2} digit from the right in {0}?
            // What was X’s first digit from the right in Palindromes?
            [Question.PalindromesNumbers] = new()
            {
                Gender = Gender.Plural,
                QuestionText = "Was war bei {0} die {2}e Ziffer von rechts {1}?",
                ModuleName = "Palindrome",
                FormatArgs = new Dictionary<string, string>
                {
                    ["X"] = "von X",
                    ["Y"] = "von Y",
                    ["Z"] = "von Z",
                    ["the screen"] = "auf dem Display",
                },
            },

            // Papa’s Pizzeria
            // What was the order number on {0}?
            // What was the order number on Papa’s Pizzeria?
            [Question.PapasPizzeriaRequest] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the order number on {0}?",
            },

            // Parity
            // What was shown on the display on {0}?
            // What was shown on the display on Parity?
            [Question.ParityDisplay] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} auf dem Display?",
                ModuleName = "Parität",
            },

            // Partial Derivatives
            // What was the LED color in the {1} stage of {0}?
            // What was the LED color in the first stage of Partial Derivatives?
            [Question.PartialDerivativesLedColors] = new()
            {
                NeedsTranslation = true,
                Gender = Gender.Plural,
                ModuleNameDative = "Partiellen Ableitungen",
                QuestionText = "Was war bei {0} die LED-Farbe in der {1}en Stufe?",
                ModuleName = "Partielle Ableitungen",
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["orange"] = "orange",
                    ["purple"] = "lila",
                    ["red"] = "rot",
                    ["yellow"] = "gelb",
                },
            },
            // What was the {1} term in {0}?
            // What was the first term in Partial Derivatives?
            [Question.PartialDerivativesTerms] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Partiellen Ableitungen",
                QuestionText = "Was war bei {0} der {1}e Term?",
                ModuleName = "Partielle Ableitungen",
            },

            // Passport Control
            // What was the passport expiration year of the {1} inspected passenger in {0}?
            // What was the passport expiration year of the first inspected passenger in Passport Control?
            [Question.PassportControlPassenger] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} das Auslaufjahr des {1}en inspizierten Passagiers?",
                ModuleName = "Passkontrolle",
            },

            // Password Destroyer
            // What was the 2FAST™ value when you solved {0}?
            // What was the 2FAST™ value when you solved Password Destroyer?
            [Question.PasswordDestroyerTwoFactorV2] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the 2FAST™ value when you solved {0}?",
            },

            // Pattern Cube
            // Which symbol was highlighted in {0}?
            // Which symbol was highlighted in Pattern Cube?
            [Question.PatternCubeHighlightedSymbol] = new()
            {
                Gender = Gender.Masculine,
                QuestionText = "Welches Symbol war in {0} hervorgehoben?",
                ModuleName = "Musterwürfel",
            },

            // The Pentabutton
            // What was the base colour in {0}?
            // What was the base colour in The Pentabutton?
            [Question.PentabuttonBaseColor] = new()
            {
                NeedsTranslation = true,
                Gender = Gender.Masculine,
                QuestionText = "Was war bei {0} die Basisfarbe?",
                ModuleName = "Pentaknopf",
                Answers = new Dictionary<string, string>
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

            // Periodic Words
            // What word was on the display in the {1} stage of {0}?
            // What word was on the display in the first stage of Periodic Words?
            [Question.PeriodicWordsDisplayedWords] = new()
            {
                Gender = Gender.Plural,
                ModuleNameDative = "Periodischen Wörtern",
                QuestionText = "Welches Wort war in der {1}en Stufe von {0} auf dem Display?",
                ModuleName = "Periodische Wörter",
            },

            // Perspective Pegs
            // What was the {1} color in the initial sequence in {0}?
            // What was the first color in the initial sequence in Perspective Pegs?
            [Question.PerspectivePegsColorSequence] = new()
            {
                Gender = Gender.Masculine,
                QuestionText = "Was war bei {0} die {1}e Farbe in der Ausgangsfolge?",
                ModuleName = "Perspektivstöpsel",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["yellow"] = "gelb",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["purple"] = "lila",
                },
            },

            // Phosphorescence
            // What was the offset in {0}?
            // What was the offset in Phosphorescence?
            [Question.PhosphorescenceOffset] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} das Offset?",
                ModuleName = "Phosphoreszenz",
            },
            // What was the {1} button press in {0}?
            // What was the first button press in Phosphorescence?
            [Question.PhosphorescenceButtonPresses] = new()
            {
                NeedsTranslation = true,
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} die {1}e Eingabe?",
                ModuleName = "Phosphoreszenz",
                Answers = new Dictionary<string, string>
                {
                    ["Azure"] = "Azur",
                    ["Blue"] = "Blau",
                    ["Crimson"] = "Carmin",
                    ["Diamond"] = "Diamant",
                    ["Emerald"] = "Edelsmaragd",
                    ["Fuchsia"] = "Fuchsie",
                    ["Green"] = "Grün",
                    ["Ice"] = "Iglu",
                    ["Jade"] = "Jade",
                    ["Kiwi"] = "Kiwi",
                    ["Lime"] = "Limette",
                    ["Magenta"] = "Magenta",
                    ["Navy"] = "Nachtblau",
                    ["Orange"] = "Orange",
                    ["Purple"] = "Purpur",
                    ["Quartz"] = "Quarz",
                    ["Red"] = "Rot",
                    ["Salmon"] = "Seelachs",
                    ["Tan"] = "Taupe",
                    ["Ube"] = "Ultra",
                    ["Vibe"] = "Vibrant",
                    ["White"] = "Weiß",
                    ["Xotic"] = "Xotisch",
                    ["Yellow"] = "Yachtgelb",
                    ["Zen"] = "Zen",
                },
            },

            // Pickup Identification
            // What pickup was shown in the {1} stage of {0}?
            // What pickup was shown in the first stage of Pickup Identification?
            [Question.PickupIdentificationItem] = new()
            {
                QuestionText = "Welches Sammlerstück war bei {0} in der {1}en Stufe zu sehen?",
            },

            // Pictionary
            // What was the code in {0}?
            // What was the code in Pictionary?
            [Question.PictionaryCode] = new()
            {
                QuestionText = "Was war bei {0} der Code?",
            },

            // Pie
            // What was the {1} digit of the displayed number in {0}?
            // What was the first digit of the displayed number in Pie?
            [Question.PieDigits] = new()
            {
                QuestionText = "Was war die {1}e Ziffer der bei {0} angezeigten Zahl?",
            },

            // Pie Flash
            // What number was not displayed in {0}?
            // What number was not displayed in Pie Flash?
            [Question.PieFlashDigits] = new()
            {
                QuestionText = "Welche Zahl war bei {0} nicht zu sehen?",
            },

            // Pigpen Cycle
            // What was the {1} in {0}?
            // What was the message in Pigpen Cycle?
            [Question.PigpenCycleWord] = new()
            {
                QuestionText = "Was war bei {0} die {1}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Nachricht",
                    ["response"] = "Antwort",
                },
            },

            // The Pink Button
            // What was the {1} word in {0}?
            // What was the first word in The Pink Button?
            [Question.PinkButtonWords] = new()
            {
                Gender = Gender.Masculine,
                ModuleNameDative = "Pinkfarbenen Knopf",
                ModuleNameWithThe = "Der Pinkfarbene Knopf",
                QuestionText = "Was war bei {0} das erste Wort?",
            },
            // What was the {1} color in {0}?
            // What was the first color in The Pink Button?
            [Question.PinkButtonColors] = new()
            {
                NeedsTranslation = true,
                Gender = Gender.Masculine,
                ModuleNameDative = "Pinkfarbenen Knopf",
                ModuleNameWithThe = "Der Pinkfarbene Knopf",
                QuestionText = "Was war bei {0} die {1}e Farbe?",
                Answers = new Dictionary<string, string>
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

            // Pixel Cipher
            // What was the keyword in {0}?
            // What was the keyword in Pixel Cipher?
            [Question.PixelCipherKeyword] = new()
            {
                Gender = Gender.Feminine,
                QuestionText = "Was war bei {0} das Schlüsselwort?",
                ModuleName = "Pixelgeheimschrift",
            },

            // Placeholder Talk
            // What was the first half of the first phrase in {0}?
            // What was the first half of the first phrase in Placeholder Talk?
            [Question.PlaceholderTalkFirstPhrase] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the first half of the first phrase in {0}?",
            },
            // What was the last half of the first phrase in {0}?
            // What was the last half of the first phrase in Placeholder Talk?
            [Question.PlaceholderTalkOrdinal] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the last half of the first phrase in {0}?",
            },

            // Placement Roulette
            // What was the character listed on the information display in {0}?
            // What was the character listed on the information display in Placement Roulette?
            [Question.PlacementRouletteChar] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the character listed on the information display in {0}?",
            },
            // What was the track listed on the information display in {0}?
            // What was the track listed on the information display in Placement Roulette?
            [Question.PlacementRouletteTrack] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the track listed on the information display in {0}?",
            },
            // What was the vehicle listed on the information display in {0}?
            // What was the vehicle listed on the information display in Placement Roulette?
            [Question.PlacementRouletteVehicle] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the vehicle listed on the information display in {0}?",
            },

            // Planets
            // What was the planet shown in {0}?
            // What was the planet shown in Planets?
            [Question.PlanetsPlanet] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the planet shown in {0}?",
            },
            // What was the color of the {1} strip (from the top) in {0}?
            // What was the color of the first strip (from the top) in Planets?
            [Question.PlanetsStrips] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} strip (from the top) in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Aqua"] = "Aqua",
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Lime"] = "Lime",
                    ["Orange"] = "Orange",
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["White"] = "Weiß",
                    ["Off"] = "Off",
                },
            },

            // Playfair Cycle
            // What was the {1} in {0}?
            // What was the message in Playfair Cycle?
            [Question.PlayfairCycleWord] = new()
            {
                QuestionText = "Was war die {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Nachricht",
                    ["response"] = "Antwort",
                },
            },

            // Poetry
            // What was the {1} correct answer you pressed in {0}?
            // What was the first correct answer you pressed in Poetry?
            [Question.PoetryAnswers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} correct answer you pressed in {0}?",
            },

            // Pointless Machines
            // What color flashed {1} in {0}?
            // What color flashed first in Pointless Machines?
            [Question.PointlessMachinesFlashes] = new()
            {
                QuestionText = "Welche Farbe hat bei {0} geblinkt?",
                ModuleName = "Sinnlose Maschinen",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Weiß",
                    ["Purple"] = "Lila",
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Yellow"] = "Gelb",
                },
            },

            // Polygons
            // Which polygon was present on {0}?
            // Which polygon was present on Polygons?
            [Question.PolygonsPolygon] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which polygon was present on {0}?",
            },

            // Polyhedral Maze
            // What was the starting position in {0}?
            // What was the starting position in Polyhedral Maze?
            [Question.PolyhedralMazeStartPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting position in {0}?",
            },

            // Prime Encryption
            // What was the number shown in {0}?
            // What was the number shown in Prime Encryption?
            [Question.PrimeEncryptionDisplayedValue] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the number shown in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Where did you start in {0}?",
            },

            // Probing
            // What was the missing frequency in the {1} wire in {0}?
            // What was the missing frequency in the red-white wire in Probing?
            [Question.ProbingFrequencies] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the missing frequency in the {1} wire in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red-white"] = "red-white",
                    ["yellow-black"] = "yellow-black",
                    ["green"] = "grün",
                    ["gray"] = "grau",
                    ["yellow-red"] = "yellow-red",
                    ["red-blue"] = "red-blue",
                },
            },

            // Procedural Maze
            // What was the initial seed in {0}?
            // What was the initial seed in Procedural Maze?
            [Question.ProceduralMazeInitialSeed] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial seed in {0}?",
            },

            // ...?
            // What was the displayed number in {0}?
            // What was the displayed number in ...??
            [Question.PunctuationMarksDisplayedNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed number in {0}?",
            },

            // Purple Arrows
            // What was the target word on {0}?
            // What was the target word on Purple Arrows?
            [Question.PurpleArrowsFinish] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the target word on {0}?",
            },

            // The Purple Button
            // What was the {1} number in the cyclic sequence on {0}?
            // What was the first number in the cyclic sequence on The Purple Button?
            [Question.PurpleButtonNumbers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} number in the cyclic sequence on {0}?",
            },

            // Puzzle Identification
            // What was the {1} puzzle number in {0}?
            // What was the first puzzle number in Puzzle Identification?
            [Question.PuzzleIdentificationNum] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} puzzle number in {0}?",
            },
            // What game was the {1} puzzle in {0} from?
            // What game was the first puzzle in Puzzle Identification from?
            [Question.PuzzleIdentificationGame] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What game was the {1} puzzle in {0} from?",
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
                NeedsTranslation = true,
                QuestionText = "What was the {1} puzzle in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What was the {1} question asked in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which word was use in {0}?",
            },

            // Quantum Ternary Converter
            // Which number was shown in {0}?
            // Which number was shown in Quantum Ternary Converter?
            [Question.QuantumTernaryConverterNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which number was shown in {0}?",
            },

            // Quaver
            // What was the {1} sequence’s answer in {0}?
            // What was the first sequence’s answer in Quaver?
            [Question.QuaverArrows] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} sequence’s answer in {0}?",
            },

            // Question Mark
            // Which of these symbols was part of the flashing sequence in {0}?
            // Which of these symbols was part of the flashing sequence in Question Mark?
            [Question.QuestionMarkFlashedSymbols] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these symbols was part of the flashing sequence in {0}?",
            },

            // Quick Arithmetic
            // What was the {1} color in the primary sequence in {0}?
            // What was the first color in the primary sequence in Quick Arithmetic?
            [Question.QuickArithmeticColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} color in the primary sequence in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["yellow"] = "gelb",
                    ["white"] = "weiß",
                    ["black"] = "schwarz",
                    ["orange"] = "orange",
                    ["pink"] = "pink",
                    ["purple"] = "lila",
                    ["cyan"] = "türkis",
                    ["brown"] = "braun",
                },
            },
            // What was the {1} digit in the {2} sequence in {0}?
            // What was the first digit in the primary sequence in Quick Arithmetic?
            [Question.QuickArithmeticPrimSecDigits] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} digit in the {2} sequence in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What was the {1} digit in the {2} slot in {0}?",
            },
            // What color was the {1} digit in the {2} slot in {0}?
            // What color was the first digit in the first slot in Quintuples?
            [Question.QuintuplesColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} digit in the {2} slot in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["orange"] = "orange",
                    ["green"] = "grün",
                    ["pink"] = "pink",
                },
            },
            // How many numbers were {1} in {0}?
            // How many numbers were red in Quintuples?
            [Question.QuintuplesColorCounts] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many numbers were {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["orange"] = "orange",
                    ["green"] = "grün",
                    ["pink"] = "pink",
                },
            },

            // Quiplash
            // What number was shown on {0}?
            // What number was shown on Quiplash?
            [Question.QuiplashNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What number was shown on {0}?",
            },

            // Quiz Buzz
            // What was the number initially on the display in {0}?
            // What was the number initially on the display in Quiz Buzz?
            [Question.QuizBuzzStartingNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the number initially on the display in {0}?",
            },

            // Qwirkle
            // What tile did you place {1} in {0}?
            // What tile did you place first in Qwirkle?
            [Question.QwirkleTilesPlaced] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What tile did you place {1} in {0}?",
            },

            // Raiding Temples
            // How many jewels were in the starting common pool in {0}?
            // How many jewels were in the starting common pool in Raiding Temples?
            [Question.RaidingTemplesStartingCommonPool] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many jewels were in the starting common pool in {0}?",
            },

            // Railway Cargo Loading
            // What was the {1} car in {0}?
            // What was the first car in Railway Cargo Loading?
            [Question.RailwayCargoLoadingCars] = new()
            {
                QuestionText = "What was the {1} coupled car in {0}?",
            },
            // Which freight table rule {1} in {0}?
            // Which freight table rule was met in Railway Cargo Loading?
            [Question.RailwayCargoLoadingFreightTableRules] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which freight table rule {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was met"] = "was met",
                    ["wasn’t met"] = "wasn’t met",
                },
            },

            // Rainbow Arrows
            // What was the displayed number in {0}?
            // What was the displayed number in Rainbow Arrows?
            [Question.RainbowArrowsNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed number in {0}?",
            },

            // Recolored Switches
            // What was the color of the {1} LED in {0}?
            // What was the color of the first LED in Recolored Switches?
            [Question.RecoloredSwitchesLedColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} LED in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["cyan"] = "türkis",
                    ["orange"] = "orange",
                    ["purple"] = "lila",
                    ["white"] = "weiß",
                },
            },

            // Recursive Password
            // Which of these words appeared, but was not the password, in {0}?
            // Which of these words appeared, but was not the password, in Recursive Password?
            [Question.RecursivePasswordNonPasswordWords] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these words appeared, but was not the password, in {0}?",
            },
            // What was the password in {0}?
            // What was the password in Recursive Password?
            [Question.RecursivePasswordPassword] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the password in {0}?",
            },

            // Red Arrows
            // What was the starting number in {0}?
            // What was the starting number in Red Arrows?
            [Question.RedArrowsStartNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting number in {0}?",
            },

            // Red Button’t
            // What was the word before “SUBMIT” in {0}?
            // What was the word before “SUBMIT” in Red Button’t?
            [Question.RedButtontWord] = new()
            {
                QuestionText = "What was the word before 'SUBMIT' in {0}?",
            },

            // Red Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Red Cipher?
            [Question.RedCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameDative = "Roten Geheimschrift",
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Rote Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // Red Herring
            // What was the first color flashed by {0}?
            // What was the first color flashed by Red Herring?
            [Question.RedHerringFirstFlash] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the first color flashed by {0}?",
            },

            // Reformed Role Reversal
            // Which condition was the solving condition in {0}?
            // Which condition was the solving condition in Reformed Role Reversal?
            [Question.ReformedRoleReversalCondition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which condition was the solving condition in {0}?",
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
            [Question.ReformedRoleReversalWire] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} wire in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Navy"] = "Navy",
                    ["Lapis"] = "Lapis",
                    ["Blue"] = "Blau",
                    ["Sky"] = "Sky",
                    ["Teal"] = "Teal",
                    ["Plum"] = "Plum",
                    ["Violet"] = "Violet",
                    ["Purple"] = "Lila",
                    ["Magenta"] = "Magenta",
                    ["Lavender"] = "Lavender",
                },
            },

            // ReGret-B Filtering
            // Which calculation was used for the {1} stage of {0}?
            // Which calculation was used for the first stage of ReGret-B Filtering?
            [Question.ReGretBFilteringOperator] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which calculation was used for the {1} stage of {0}?",
            },

            // Regular Crazy Talk
            // What was the displayed digit that corresponded to the solution phrase in {0}?
            // What was the displayed digit that corresponded to the solution phrase in Regular Crazy Talk?
            [Question.RegularCrazyTalkDigit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed digit that corresponded to the solution phrase in {0}?",
            },
            // What was the embellishment of the solution phrase in {0}?
            // What was the embellishment of the solution phrase in Regular Crazy Talk?
            [Question.RegularCrazyTalkModifier] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the embellishment of the solution phrase in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which one of these houses was on offer, but not chosen by Bob in {0}?",
            },

            // Reverse Morse
            // What was the {1} character in the {2} message of {0}?
            // What was the first character in the first message of Reverse Morse?
            [Question.ReverseMorseCharacters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} character in the {2} message of {0}?",
            },

            // Reverse Polish Notation
            // What character was used in the {1} round of {0}?
            // What character was used in the first round of Reverse Polish Notation?
            [Question.ReversePolishNotationCharacter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What character was used in the {1} round of {0}?",
            },

            // RGB Maze
            // What was the exit coordinate in {0}?
            // What was the exit coordinate in RGB Maze?
            [Question.RGBMazeExit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the exit coordinate in {0}?",
            },
            // Where was the {1} key in {0}?
            // Where was the red key in RGB Maze?
            [Question.RGBMazeKeys] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was the {1} key in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                },
            },
            // Which maze number was the {1} maze in {0}?
            // Which maze number was the red maze in RGB Maze?
            [Question.RGBMazeNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which maze number was the {1} maze in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                },
            },

            // RGB Sequences
            // What was the color of the {1} LED in {0}?
            // What was the color of the first LED in RGB Sequences?
            [Question.RGBSequencesDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} LED in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Magenta"] = "Magenta",
                    ["Cyan"] = "Türkis",
                    ["Yellow"] = "Gelb",
                    ["White"] = "Weiß",
                },
            },

            // Rhythms
            // What was the color in {0}?
            // What was the color in Rhythms?
            [Question.RhythmsColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blau",
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
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
                NeedsTranslation = true,
                QuestionText = "Where was the empty cell in {0}?",
            },

            // Robot Programming
            // What was the color of the {1} robot in {0}?
            // What was the color of the first robot in Robot Programming?
            [Question.RobotProgrammingColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} robot in {0}?",
            },
            // What was the shape of the {1} robot in {0}?
            // What was the shape of the first robot in Robot Programming?
            [Question.RobotProgrammingShape] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the shape of the {1} robot in {0}?",
            },

            // Roger
            // What four-digit number was given in {0}?
            // What four-digit number was given in Roger?
            [Question.RogerSeed] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What four-digit number was given in {0}?",
            },

            // Role Reversal
            // What was the number to the correct condition in {0}?
            // What was the number to the correct condition in Role Reversal?
            [Question.RoleReversalNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the number to the correct condition in {0}?",
            },
            // How many {1} wires were there in {0}?
            // How many warm-colored wires were there in Role Reversal?
            [Question.RoleReversalWires] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many {1} wires were there in {0}?",
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
            [Question.RuleNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the rule number in {0}?",
            },

            // Rule of Three
            // What was the {1} coordinate of the {2} vertex in {0}?
            // What was the X coordinate of the red vertex in Rule of Three?
            [Question.RuleOfThreeCoordinates] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} coordinate of the {2} vertex in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["yellow"] = "gelb",
                    ["blue"] = "blau",
                },
            },
            // What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?
            // What was the position of the red sphere on the X axis in the first cycle in Rule of Three?
            [Question.RuleOfThreeCycles] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["yellow"] = "gelb",
                    ["blue"] = "blau",
                },
            },

            // Safety Square
            // What was the digit displayed on the {1} diamond in {0}?
            // What was the digit displayed on the red diamond in Safety Square?
            [Question.SafetySquareDigits] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the digit displayed on the {1} diamond in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["yellow"] = "gelb",
                    ["blue"] = "blau",
                },
            },
            // What was the special rule displayed on the white diamond in {0}?
            // What was the special rule displayed on the white diamond in Safety Square?
            [Question.SafetySquareSpecialRule] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the special rule displayed on the white diamond in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Where was {1} in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["TL"] = "TL",
                    ["TM"] = "TM",
                    ["TR"] = "TR",
                    ["ML"] = "ML",
                    ["MM"] = "MM",
                    ["MR"] = "MR",
                    ["BL"] = "BL",
                    ["BM"] = "BM",
                    ["BR"] = "BR",
                },
            },

            // Saturn
            // Where was the goal in {0}?
            // Where was the goal in Saturn?
            [Question.SaturnGoal] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was the goal in {0}?",
            },

            // Sbemail Songs
            // What was the displayed song for stage {1} (hexadecimal) of {0}?
            // What was the displayed song for stage 01 (hexadecimal) of Sbemail Songs?
            [Question.SbemailSongsSongs] = new()
            {
                QuestionText = "Was war bei {0} das in Stufe {1} (hexadezimal) angezeigte Lied?",
                ModuleName = "Sbemail-Lieder",
                TranslatableStrings = new Dictionary<string, string> // See translations.md for more information on this question.
                {
                    ["the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)"] = "den Sbemail-Liedern, deren Stufe {1} (hexadezimal) ‘{0}’ anzeigten,",
                },
            },

            // Scavenger Hunt
            // Which tile was correctly submitted in the first stage of {0}?
            // Which tile was correctly submitted in the first stage of Scavenger Hunt?
            [Question.ScavengerHuntKeySquare] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which tile was correctly submitted in the first stage of {0}?",
            },
            // Which of these tiles was {1} in the first stage of {0}?
            // Which of these tiles was red in the first stage of Scavenger Hunt?
            [Question.ScavengerHuntColoredTiles] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these tiles was {1} in the first stage of {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                },
            },

            // Schlag den Bomb
            // What was the contestant’s name in {0}?
            // What was the contestant’s name in Schlag den Bomb?
            [Question.SchlagDenBombContestantName] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the contestant’s name in {0}?",
            },
            // What was the contestant’s score in {0}?
            // What was the contestant’s score in Schlag den Bomb?
            [Question.SchlagDenBombContestantScore] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the contestant’s score in {0}?",
            },
            // What was the bomb’s score in {0}?
            // What was the bomb’s score in Schlag den Bomb?
            [Question.SchlagDenBombBombScore] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the bomb’s score in {0}?",
            },

            // Scramboozled Eggain
            // What was the {1} encrypted word in {0}?
            // What was the first encrypted word in Scramboozled Eggain?
            [Question.ScramboozledEggainWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} encrypted word in {0}?",
            },

            // Scripting
            // What was the submitted data type of the variable in {0}?
            // What was the submitted data type of the variable in Scripting?
            [Question.ScriptingVariableDataType] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the submitted data type of the variable in {0}?",
            },

            // Scrutiny Squares
            // What was the modified property of the first display in {0}?
            // What was the modified property of the first display in Scrutiny Squares?
            [Question.ScrutinySquaresFirstDifference] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the modified property of the first display in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Word"] = "Word",
                    ["Color around word"] = "Color around word",
                    ["Color of background"] = "Color of background",
                    ["Color of word"] = "Color of word",
                },
            },

            // Sea Shells
            // What were the first and second words in the {1} phrase in {0}?
            // What were the first and second words in the first phrase in Sea Shells?
            [Question.SeaShells1] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the first and second words in the {1} phrase in {0}?",
            },
            // What were the third and fourth words in the {1} phrase in {0}?
            // What were the third and fourth words in the first phrase in Sea Shells?
            [Question.SeaShells2] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the third and fourth words in the {1} phrase in {0}?",
            },
            // What was the end of the {1} phrase in {0}?
            // What was the end of the first phrase in Sea Shells?
            [Question.SeaShells3] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the end of the {1} phrase in {0}?",
            },

            // Semamorse
            // What was the {1} letter involved in the starting value in {0}?
            // What was the Morse letter involved in the starting value in Semamorse?
            [Question.SemamorseLetters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} letter involved in the starting value in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Morse"] = "Morse",
                    ["semaphore"] = "semaphore",
                },
            },
            // What was the color of the display involved in the starting value in {0}?
            // What was the color of the display involved in the starting value in Semamorse?
            [Question.SemamorseColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the display involved in the starting value in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["cyan"] = "türkis",
                    ["indigo"] = "indigo",
                    ["pink"] = "pink",
                },
            },

            // The Sequencyclopedia
            // What sequence was used in {0}?
            // What sequence was used in The Sequencyclopedia?
            [Question.SequencyclopediaSequence] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What sequence was used in {0}?",
            },

            // S.E.T. Theory
            // What equation was shown in the {1} stage of {0}?
            // What equation was shown in the first stage of S.E.T. Theory?
            [Question.SetTheoryEquations] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What equation was shown in the {1} stage of {0}?",
            },

            // Shapes And Bombs
            // What was the initial letter in {0}?
            // What was the initial letter in Shapes And Bombs?
            [Question.ShapesAndBombsInitialLetter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial letter in {0}?",
            },

            // Shape Shift
            // What was the initial shape in {0}?
            // What was the initial shape in Shape Shift?
            [Question.ShapeShiftInitialShape] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial shape in {0}?",
            },

            // Shifted Maze
            // What color was the {1} marker in {0}?
            // What color was the top-left marker in Shifted Maze?
            [Question.ShiftedMazeColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} marker in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-right"] = "top-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Weiß",
                    ["Blue"] = "Blau",
                    ["Yellow"] = "Gelb",
                    ["Magenta"] = "Magenta",
                    ["Green"] = "Grün",
                },
            },

            // Shifting Maze
            // What was the seed in {0}?
            // What was the seed in Shifting Maze?
            [Question.ShiftingMazeSeed] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the seed in {0}?",
            },

            // Shogi Identification
            // What was the displayed piece in {0}?
            // What was the displayed piece in Shogi Identification?
            [Question.ShogiIdentificationPiece] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed piece in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What was the deciphered word in {0}?",
            },

            // Silly Slots
            // What was the {1} slot in the {2} stage in {0}?
            // What was the first slot in the first stage in Silly Slots?
            [Question.SillySlots] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} slot in the {2} stage in {0}?",
                Answers = new Dictionary<string, string>
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

            // Silo Authorization
            // What was the message type in {0}?
            // What was the message type in Silo Authorization?
            [Question.SiloAuthorizationMessageType] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the message type in {0}?",
            },
            // What was the {1} part of the encrypted message in {0}?
            // What was the first part of the encrypted message in Silo Authorization?
            [Question.SiloAuthorizationEncryptedMessage] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} part of the encrypted message in {0}?",
            },
            // What was the received authentication code in {0}?
            // What was the received authentication code in Silo Authorization?
            [Question.SiloAuthorizationAuthCode] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the received authentication code in {0}?",
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
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Yellow"] = "Gelb",
                },
            },

            // Simon Samples
            // What were the call samples {1} of {0}?
            // What were the call samples played in the first stage of Simon Samples?
            [Question.SimonSamplesSamples] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the call samples {1} of {0}?",
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
            [Question.SimonSaysFlash] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color flashed {1} in the final sequence in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["yellow"] = "gelb",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                },
            },

            // Simon Scrambles
            // What color flashed {1} in {0}?
            // What color flashed first in Simon Scrambles?
            [Question.SimonScramblesColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color flashed {1} in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Yellow"] = "Gelb",
                },
            },

            // Simon Screams
            // Which color flashed {1} in the final sequence in {0}?
            // Which color flashed first in the final sequence in Simon Screams?
            [Question.SimonScreamsFlashing] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color flashed {1} in the final sequence in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Purple"] = "Lila",
                },
            },
            // In which stage(s) of {0} was “{1}” the applicable rule?
            // In which stage(s) of Simon Screams was “a color flashed, then a color two away, then the first again” the applicable rule?
            [Question.SimonScreamsRuleSimple] = new()
            {
                QuestionText = "In welcher/-n Stufe(n) bei {0} war “{1}” die zutreffende Regel?",
                ModuleName = "Simon Schreit",
                FormatArgs = new Dictionary<string, string>
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
                Answers = new Dictionary<string, string>
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
            // In which stage(s) of {0} was “{1} flashed out of {2}, {3}, and {4}” the applicable rule?
            // In which stage(s) of Simon Screams was “at most one color flashed out of Red, Orange, and Yellow” the applicable rule?
            [Question.SimonScreamsRuleComplex] = new()
            {
                NeedsTranslation = true,
                QuestionText = "In welcher/-n Stufe(n) bei {0} war “{1} der Farben {2}, {3} und {4} blinkt” die zutreffende Regel?",
                ModuleName = "Simon Schreit",
                FormatArgs = new Dictionary<string, string>
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
                Answers = new Dictionary<string, string>
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

            // Simon Selects
            // Which color flashed {1} in the {2} stage of {0}?
            // Which color flashed first in the first stage of Simon Selects?
            [Question.SimonSelectsOrder] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color flashed {1} in the {2} stage of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Purple"] = "Lila",
                    ["Magenta"] = "Magenta",
                    ["Cyan"] = "Türkis",
                },
            },

            // Simon Sends
            // What was the {1} received letter in {0}?
            // What was the red received letter in Simon Sends?
            [Question.SimonSendsReceivedLetters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} received letter in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                },
            },

            // Simon Serves
            // Who flashed {1} in course {2} of {0}?
            // Who flashed first in course 1 of Simon Serves?
            [Question.SimonServesFlash] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Who flashed {1} in course {2} of {0}?",
            },
            // Which item was not served in course {1} of {0}?
            // Which item was not served in course 1 of Simon Serves?
            [Question.SimonServesFood] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which item was not served in course {1} of {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What was the shape submitted at the end of {0}?",
            },

            // Simon Shouts
            // Which letter flashed on the {1} button in {0}?
            // Which letter flashed on the top button in Simon Shouts?
            [Question.SimonShoutsFlashingLetter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which letter flashed on the {1} button in {0}?",
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
            [Question.SimonShrieksFlashingButton] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?",
            },

            // Simon Signals
            // What shape was the {1} arrow in {0}?
            // What shape was the red arrow in Simon Signals?
            [Question.SimonSignalsColorToShape] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What shape was the {1} arrow in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["gray"] = "grau",
                },
            },
            // How many directions did the {1} arrow in {0} have?
            // How many directions did the red arrow in Simon Signals have?
            [Question.SimonSignalsColorToRotations] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many directions did the {1} arrow in {0} have?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["gray"] = "grau",
                },
            },
            // What color was the arrow with this shape in {0}?
            // What color was the arrow with this shape in Simon Signals?
            [Question.SimonSignalsShapeToColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the arrow with this shape in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["gray"] = "grau",
                },
            },
            // How many directions did the arrow with this shape have in {0}?
            // How many directions did the arrow with this shape have in Simon Signals?
            [Question.SimonSignalsShapeToRotations] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many directions did the arrow with this shape have in {0}?",
            },
            // What color was the arrow with {1} possible directions in {0}?
            // What color was the arrow with 3 possible directions in Simon Signals?
            [Question.SimonSignalsRotationsToColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the arrow with {1} possible directions in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["gray"] = "grau",
                },
            },
            // What shape was the arrow with {1} possible directions in {0}?
            // What shape was the arrow with 3 possible directions in Simon Signals?
            [Question.SimonSignalsRotationsToShape] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What shape was the arrow with {1} possible directions in {0}?",
            },

            // Simon Simons
            // What was the {1} flash in the final sequence in {0}?
            // What was the first flash in the final sequence in Simon Simons?
            [Question.SimonSimonsFlashingColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} flash in the final sequence in {0}?",
            },

            // Simon Sings
            // Which key’s color flashed {1} in the {2} stage of {0}?
            // Which key’s color flashed first in the first stage of Simon Sings?
            [Question.SimonSingsFlashing] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which key’s color flashed {1} in the {2} stage of {0}?",
            },

            // Simon Smiles
            // What sound did the {1} button press make {0}?
            // What sound did the first button press make Simon Smiles?
            [Question.SimonSmilesSounds] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What sound did the {1} button press make {0}?",
            },

            // Simon Smothers
            // What was the color of the {1} flash in {0}?
            // What was the color of the first flash in Simon Smothers?
            [Question.SimonSmothersColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} flash in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Blue"] = "Blau",
                    ["Magenta"] = "Magenta",
                    ["Cyan"] = "Türkis",
                },
            },
            // What was the direction of the {1} flash in {0}?
            // What was the direction of the first flash in Simon Smothers?
            [Question.SimonSmothersDirections] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the direction of the {1} flash in {0}?",
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
            [Question.SimonSoundsFlashingColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which sample button sounded {1} in the final sequence in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                    ["green"] = "grün",
                },
            },

            // Simon Speaks
            // Which bubble flashed first in {0}?
            // Which bubble flashed first in Simon Speaks?
            [Question.SimonSpeaksPositions] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which bubble flashed first in {0}?",
                Answers = new Dictionary<string, string>
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
            // Which bubble flashed second in {0}?
            // Which bubble flashed second in Simon Speaks?
            [Question.SimonSpeaksShapes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which bubble flashed second in {0}?",
            },
            // Which language was the bubble that flashed third in {0} in?
            // Which language was the bubble that flashed third in Simon Speaks in?
            [Question.SimonSpeaksLanguages] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which language was the bubble that flashed third in {0} in?",
            },
            // Which word was in the bubble that flashed fourth in {0}?
            // Which word was in the bubble that flashed fourth in Simon Speaks?
            [Question.SimonSpeaksWords] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which word was in the bubble that flashed fourth in {0}?",
            },
            // What color was the bubble that flashed fifth in {0}?
            // What color was the bubble that flashed fifth in Simon Speaks?
            [Question.SimonSpeaksColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the bubble that flashed fifth in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["black"] = "schwarz",
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["cyan"] = "türkis",
                    ["red"] = "rot",
                    ["purple"] = "lila",
                    ["yellow"] = "gelb",
                    ["white"] = "weiß",
                    ["gray"] = "grau",
                },
            },

            // Simon’s Star
            // Which color flashed {1} in sequence in {0}?
            // Which color flashed first in sequence in Simon’s Star?
            [Question.SimonsStarColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color flashed {1} in sequence in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["yellow"] = "gelb",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["purple"] = "lila",
                },
            },

            // Simon Stacks
            // Which color flashed in the {1} stage of {0}?
            // Which color flashed in the first stage of Simon Stacks?
            [Question.SimonStacksColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color flashed in the {1} stage of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Yellow"] = "Gelb",
                },
            },

            // Simon Stages
            // Which color flashed {1} in the {2} stage in {0}?
            // Which color flashed first in the first stage in Simon Stages?
            [Question.SimonStagesFlashes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color flashed {1} in the {2} stage in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                    ["orange"] = "orange",
                    ["magenta"] = "magenta",
                    ["green"] = "grün",
                    ["pink"] = "pink",
                    ["lime"] = "lime",
                    ["cyan"] = "türkis",
                    ["white"] = "weiß",
                },
            },
            // What color was the indicator in the {1} stage in {0}?
            // What color was the indicator in the first stage in Simon Stages?
            [Question.SimonStagesIndicator] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the indicator in the {1} stage in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["yellow"] = "gelb",
                    ["orange"] = "orange",
                    ["magenta"] = "magenta",
                    ["green"] = "grün",
                    ["pink"] = "pink",
                    ["lime"] = "lime",
                    ["cyan"] = "türkis",
                    ["white"] = "weiß",
                },
            },

            // Simon States
            // Which {1} in the {2} stage in {0}?
            // Which color(s) flashed in the first stage in Simon States?
            [Question.SimonStatesDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which {1} in the {2} stage in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["color(s) flashed"] = "color(s) flashed",
                    ["color(s) didn’t flash"] = "color(s) didn’t flash",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Red, Yellow"] = "Red, Yellow",
                    ["Red, Green"] = "Red, Green",
                    ["Red, Blue"] = "Red, Blue",
                    ["Yellow, Green"] = "Yellow, Green",
                    ["Yellow, Blue"] = "Yellow, Blue",
                    ["Green, Blue"] = "Green, Blue",
                    ["all 4"] = "all 4",
                    ["none"] = "none",
                },
            },

            // Simon Stops
            // Which color flashed {1} in the output sequence in {0}?
            // Which color flashed first in the output sequence in Simon Stops?
            [Question.SimonStopsColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color flashed {1} in the output sequence in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Violet"] = "Violet",
                },
            },

            // Simon Stores
            // Which color {1} {2} in the final sequence of {0}?
            // Which color flashed first in the final sequence of Simon Stores?
            [Question.SimonStoresColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color {1} {2} in the final sequence of {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["flashed"] = "flashed",
                    ["was among the colors flashed"] = "was among the colors flashed",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                },
            },

            // Simon Subdivides
            // What color was the button at this position in {0}?
            // What color was the button at this position in Simon Subdivides?
            [Question.SimonSubdividesButton] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the button at this position in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Red"] = "Rot",
                    ["Violet"] = "Violet",
                },
            },

            // Simon Supports
            // What was the {1} topic in {0}?
            // What was the first topic in Simon Supports?
            [Question.SimonSupportsTopics] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} topic in {0}?",
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
                QuestionText = "Where was {1} in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What color flashed {1} on the {2} Simon in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blau",
                    ["Yellow"] = "Gelb",
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                },
            },

            // Skewed Slots
            // What were the original numbers in {0}?
            // What were the original numbers in Skewed Slots?
            [Question.SkewedSlotsOriginalNumbers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What were the original numbers in {0}?",
            },

            // Skewers
            // What color was this gem in {0}?
            // What color was this gem in Skewers?
            [Question.SkewersColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was this gem in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which race was selectable, but not the solution, in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which weapon was selectable, but not the solution, in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which enemy was selectable, but not the solution, in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which city was selectable, but not the solution, in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which dragon shout was selectable, but not the solution, in {0}?",
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
                QuestionText = "What was the last pair of letters in {0}?",
            },

            // Small Circle
            // How much did the sequence shift by in {0}?
            // How much did the sequence shift by in Small Circle?
            [Question.SmallCircleShift] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How much did the sequence shift by in {0}?",
            },
            // Which wedge made the different noise in the beginning of {0}?
            // Which wedge made the different noise in the beginning of Small Circle?
            [Question.SmallCircleWedge] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which wedge made the different noise in the beginning of {0}?",
                Answers = new Dictionary<string, string>
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
            // Which color was {1} in the solution to {0}?
            // Which color was first in the solution to Small Circle?
            [Question.SmallCircleSolution] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which color was {1} in the solution to {0}?",
                Answers = new Dictionary<string, string>
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

            // Smash, Marry, Kill
            // In what category was {1} for {0}?
            // In what category was The Button for Smash, Marry, Kill?
            [Question.SmashMarryKillCategory] = new()
            {
                NeedsTranslation = true,
                QuestionText = "In what category was {1} for {0}?",
            },
            // Which module was in the {1} category for {0}?
            // Which module was in the SMASH category for Smash, Marry, Kill?
            [Question.SmashMarryKillModule] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which module was in the {1} category for {0}?",
            },

            // Snooker
            // How many red balls were there at the start of {0}?
            // How many red balls were there at the start of Snooker?
            [Question.SnookerReds] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many red balls were there at the start of {0}?",
            },

            // Snowflakes
            // Which snowflake was on the {1} button of {0}?
            // Which snowflake was on the top button of Snowflakes?
            [Question.SnowflakesDisplayedSnowflakes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which snowflake was on the {1} button of {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["right"] = "right",
                    ["bottom"] = "bottom",
                    ["left"] = "left",
                },
            },

            // Sonic & Knuckles
            // Which sound was played but not featured in the chosen zone in {0}?
            // Which sound was played but not featured in the chosen zone in Sonic & Knuckles?
            [Question.SonicKnucklesSounds] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which sound was played but not featured in the chosen zone in {0}?",
            },
            // Which badnik was shown in {0}?
            // Which badnik was shown in Sonic & Knuckles?
            [Question.SonicKnucklesBadnik] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which badnik was shown in {0}?",
            },
            // Which monitor was shown in {0}?
            // Which monitor was shown in Sonic & Knuckles?
            [Question.SonicKnucklesMonitor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which monitor was shown in {0}?",
            },

            // Sonic The Hedgehog
            // What was the {1} picture on {0}?
            // What was the first picture on Sonic The Hedgehog?
            [Question.SonicTheHedgehogPictures] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} picture on {0}?",
            },
            // Which sound was played by the {1} screen on {0}?
            // Which sound was played by the Running Boots screen on Sonic The Hedgehog?
            [Question.SonicTheHedgehogSounds] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which sound was played by the {1} screen on {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What positions were the last swap used to solve {0}?",
            },

            // Souvenir
            // What was the first module asked about in the other Souvenir on this bomb?
            // What was the first module asked about in the other Souvenir on this bomb?
            [Question.SouvenirFirstQuestion] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the first module asked about in the other Souvenir on this bomb?",
            },

            // Space Traders
            // What was the maximum tax amount per vessel in {0}?
            // What was the maximum tax amount per vessel in Space Traders?
            [Question.SpaceTradersMaxTax] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the maximum tax amount per vessel in {0}?",
            },

            // Spelling Bee
            // What word was asked to be spelled in {0}?
            // What word was asked to be spelled in Spelling Bee?
            [Question.SpellingBeeWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What word was asked to be spelled in {0}?",
            },

            // The Sphere
            // What was the {1} flashed color in {0}?
            // What was the first flashed color in The Sphere?
            [Question.SphereColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} flashed color in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["orange"] = "orange",
                    ["pink"] = "pink",
                    ["purple"] = "lila",
                    ["grey"] = "grey",
                    ["white"] = "weiß",
                },
            },

            // Splitting The Loot
            // What bag was initially colored in {0}?
            // What bag was initially colored in Splitting The Loot?
            [Question.SplittingTheLootColoredBag] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What bag was initially colored in {0}?",
            },

            // Spongebob Birthday Identification
            // Who was the {1} child displayed in {0}?
            // Who was the first child displayed in Spongebob Birthday Identification?
            [Question.SpongebobBirthdayIdentificationChildren] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Who was the {1} child displayed in {0}?",
            },

            // Stability
            // What was the color of the {1} lit LED in {0}?
            // What was the color of the first lit LED in Stability?
            [Question.StabilityLedColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} lit LED in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Blue"] = "Blau",
                },
            },
            // What was the identification number in {0}?
            // What was the identification number in Stability?
            [Question.StabilityIdNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the identification number in {0}?",
            },

            // Stable Time Signatures
            // What was the {1} time signature in {0}?
            // What was the first time signature in Stable Time Signatures?
            [Question.StableTimeSignaturesSignatures] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} time signature in {0}?",
            },

            // Stacked Sequences
            // Which of these is the length of a sequence in {0}?
            // Which of these is the length of a sequence in Stacked Sequences?
            [Question.StackedSequences] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these is the length of a sequence in {0}?",
            },

            // Stars
            // What was the digit in the center of {0}?
            // What was the digit in the center of Stars?
            [Question.StarsCenter] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the digit in the center of {0}?",
            },

            // Starstruck
            // Which star was present on {0}?
            // Which star was present on Starstruck?
            [Question.StarstruckStar] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which star was present on {0}?",
            },

            // State of Aggregation
            // What was the element shown in {0}?
            // What was the element shown in State of Aggregation?
            [Question.StateOfAggregationElement] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the element shown in {0}?",
            },

            // Stellar
            // What was the {1} letter in {0}?
            // What was the Morse code letter in Stellar?
            [Question.StellarLetters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} letter in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["Morse code"] = "Morse code",
                    ["tap code"] = "tap code",
                    ["Braille"] = "Braille",
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
                NeedsTranslation = true,
                QuestionText = "What was the value of the {1} arrow in {0}?",
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

            // Subbly Jubbly
            // What was a substitution word in {0}?
            // What was a substitution word in Subbly Jubbly?
            [Question.SubblyJubblySubstitutions] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was a substitution word in {0}?",
            },

            // Subscribe to Pewdiepie
            // How many subscribers does {1} have in {0}?
            // How many subscribers does PewDiePie have in Subscribe to Pewdiepie?
            [Question.SubscribeToPewdiepieSubCount] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many subscribers does {1} have in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "Which bread did the customer ask for in {0}?",
            },
            // Which of these was not asked for in {0}?
            // Which of these was not asked for in Subway?
            [Question.SubwayItems] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these was not asked for in {0}?",
            },

            // Sugar Skulls
            // What skull was shown on the {1} square in {0}?
            // What skull was shown on the top square in Sugar Skulls?
            [Question.SugarSkullsSkull] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What skull was shown on the {1} square in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
            },
            // Which skull {1} present in {0}?
            // Which skull was present in Sugar Skulls?
            [Question.SugarSkullsAvailability] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which skull {1} present in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "was",
                    ["was not"] = "was not",
                },
            },

            // Suits And Colours
            // What was the colour of this cell in {0}?
            // What was the colour of this cell in Suits And Colours?
            [Question.SuitsAndColourColour] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the colour of this cell in {0}?",
            },
            // What was the suit of this cell in {0}?
            // What was the suit of this cell in Suits And Colours?
            [Question.SuitsAndColourSuit] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the suit of this cell in {0}?",
            },

            // Superparsing
            // What was the displayed word in {0}?
            // What was the displayed word in Superparsing?
            [Question.SuperparsingDisplayed] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed word in {0}?",
            },

            // SUSadmin
            // Which security protocol was installed in {0}?
            // Which security protocol was installed in SUSadmin?
            [Question.SUSadminSecurity] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which security protocol was installed in {0}?",
            },

            // The Switch
            // What color was the {1} LED on the {2} flip of {0}?
            // What color was the top LED on the first flip of The Switch?
            [Question.SwitchInitialColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} LED on the {2} flip of {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["bottom"] = "bottom",
                },
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["orange"] = "orange",
                    ["yellow"] = "gelb",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["purple"] = "lila",
                },
            },

            // Switches
            // What was the initial position of the switches in {0}?
            // What was the initial position of the switches in Switches?
            [Question.SwitchesInitialPosition] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial position of the switches in {0}?",
            },

            // Switching Maze
            // What was the seed in {0}?
            // What was the seed in Switching Maze?
            [Question.SwitchingMazeSeed] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the seed in {0}?",
            },
            // What was the starting maze color in {0}?
            // What was the starting maze color in Switching Maze?
            [Question.SwitchingMazeColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting maze color in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                    ["Orange"] = "Orange",
                    ["Red"] = "Rot",
                    ["White"] = "Weiß",
                },
            },

            // Symbol Cycle
            // How many symbols were cycling on the {1} screen in {0}?
            // How many symbols were cycling on the left screen in Symbol Cycle?
            [Question.SymbolCycleSymbolCounts] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many symbols were cycling on the {1} screen in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "left",
                    ["right"] = "right",
                },
            },

            // Symbolic Coordinates
            // What was the {1} symbol in the {2} stage of {0}?
            // What was the left symbol in the first stage of Symbolic Coordinates?
            [Question.SymbolicCoordinateSymbols] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} symbol in the {2} stage of {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["left"] = "left",
                    ["middle"] = "middle",
                    ["right"] = "right",
                },
            },

            // Symbolic Tasha
            // Which button flashed {1} in the final sequence of {0}?
            // Which button flashed first in the final sequence of Symbolic Tasha?
            [Question.SymbolicTashaFlashes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which button flashed {1} in the final sequence of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Top"] = "Top",
                    ["Right"] = "Right",
                    ["Bottom"] = "Bottom",
                    ["Left"] = "Left",
                    ["Pink"] = "Pink",
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Blue"] = "Blau",
                },
            },
            // Which symbol was on the {1} button in {0}?
            // Which symbol was on the top button in Symbolic Tasha?
            [Question.SymbolicTashaSymbols] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which symbol was on the {1} button in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["right"] = "right",
                    ["bottom"] = "bottom",
                    ["left"] = "left",
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["yellow"] = "gelb",
                    ["pink"] = "pink",
                },
            },

            // SYNC-125 [3]
            // What was displayed on the screen in the {1} stage of {0}?
            // What was displayed on the screen in the first stage of SYNC-125 [3]?
            [Question.Sync125_3Word] = new()
            {
                QuestionText = "What was displayed on the screen in stage {1} of {0}?",
            },

            // Synonyms
            // Which number was displayed on {0}?
            // Which number was displayed on Synonyms?
            [Question.SynonymsNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which number was displayed on {0}?",
            },

            // Sysadmin
            // What error code did you fix in {0}?
            // What error code did you fix in Sysadmin?
            [Question.SysadminFixedErrorCodes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What error code did you fix in {0}?",
            },

            // Tap Code
            // What was the received word in {0}?
            // What was the received word in Tap Code?
            [Question.TapCodeReceivedWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the received word in {0}?",
            },

            // Tasha Squeals
            // What was the {1} flashed color in {0}?
            // What was the first flashed color in Tasha Squeals?
            [Question.TashaSquealsColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} flashed color in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Pink"] = "Pink",
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Blue"] = "Blau",
                },
            },

            // Tasque Managing
            // Where was the starting position in {0}?
            // Where was the starting position in Tasque Managing?
            [Question.TasqueManagingStartingPos] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was the starting position in {0}?",
            },

            // The Tea Set
            // Which ingredient was displayed {1}, from left to right, in {0}?
            // Which ingredient was displayed first, from left to right, in The Tea Set?
            [Question.TeaSetDisplayedIngredients] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which ingredient was displayed {1}, from left to right, in {0}?",
            },

            // Technical Keypad
            // What was the {1} displayed digit in {0}?
            // What was the first displayed digit in Technical Keypad?
            // Note: This question is depicted visually, rather than with words. A translation here will only be used for logging.
            [Question.TechnicalKeypadDisplayedDigits] = new()
            {
                ModuleNameDative = "Technischen Tastenfeld",
                QuestionText = "Was war bei {0} die {1}e Ziffer auf dem Display?",
                ModuleName = "Technisches Tastenfeld",
            },

            // Ten-Button Color Code
            // What was the initial color of the {1} button in the {2} stage of {0}?
            // What was the initial color of the first button in the first stage of Ten-Button Color Code?
            [Question.TenButtonColorCodeInitialColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial color of the {1} button in the {2} stage of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                },
            },

            // Tenpins
            // What was the {1} split in {0}?
            // What was the red split in Tenpins?
            [Question.TenpinsSplits] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} split in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
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
                NeedsTranslation = true,
                QuestionText = "What colour triangle pulsed {1} in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["orange"] = "orange",
                    ["lime"] = "lime",
                    ["jade"] = "jadefarben",
                    ["azure"] = "azurfarben",
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
                NeedsTranslation = true,
                QuestionText = "What was the displayed letter in {0}?",
            },

            // Thinking Wires
            // What was the position from top to bottom of the first wire needing to be cut in {0}?
            // What was the position from top to bottom of the first wire needing to be cut in Thinking Wires?
            [Question.ThinkingWiresFirstWire] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the position from top to bottom of the first wire needing to be cut in {0}?",
            },
            // What color did the second valid wire to cut have to have in {0}?
            // What color did the second valid wire to cut have to have in Thinking Wires?
            [Question.ThinkingWiresSecondWire] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color did the second valid wire to cut have to have in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                    ["White"] = "Weiß",
                    ["Black"] = "Schwarz",
                    ["Any"] = "Any",
                },
            },
            // What was the display number in {0}?
            // What was the display number in Thinking Wires?
            [Question.ThinkingWiresDisplayNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the display number in {0}?",
            },

            // Third Base
            // What was the display word in the {1} stage on {0}?
            // What was the display word in the first stage on Third Base?
            [Question.ThirdBaseDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the display word in the {1} stage on {0}?",
            },

            // Thirty Dollar Module
            // Which sound was used in {0}?
            // Which sound was used in Thirty Dollar Module?
            [Question.ThirtyDollarModuleSounds] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which sound was used in {0}?",
            },

            // Tic Tac Toe
            // What was on the {1} button at the start of {0}?
            // What was on the top-left button at the start of Tic Tac Toe?
            [Question.TicTacToeInitialState] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was on the {1} button at the start of {0}?",
                FormatArgs = new Dictionary<string, string>
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

            // Time Signatures
            // What was the {1} time signature in {0}?
            // What was the first time signature in Time Signatures?
            [Question.TimeSignaturesSignatures] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} time signature in {0}?",
            },

            // Timezone
            // What was the {1} city in {0}?
            // What was the departure city in Timezone?
            [Question.TimezoneCities] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} city in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["departure"] = "departure",
                    ["destination"] = "destination",
                },
            },

            // Tip Toe
            // Which of these squares was safe in row {1} in {0}?
            // Which of these squares was safe in row 9 in Tip Toe?
            [Question.TipToeSafeSquares] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these squares was safe in row {1} in {0}?",
            },

            // Topsy Turvy
            // What was the word initially shown in {0}?
            // What was the word initially shown in Topsy Turvy?
            [Question.TopsyTurvyWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the word initially shown in {0}?",
            },

            // Touch Transmission
            // What was the transmitted word in {0}?
            // What was the transmitted word in Touch Transmission?
            [Question.TouchTransmissionWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the transmitted word in {0}?",
            },
            // In what order was the Braille read in {0}?
            // In what order was the Braille read in Touch Transmission?
            [Question.TouchTransmissionOrder] = new()
            {
                NeedsTranslation = true,
                QuestionText = "In what order was the Braille read in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What was the {1} received message in {0}?",
            },

            // Triamonds
            // What colour triangle pulsed {1} in {0}?
            // What colour triangle pulsed first in Triamonds?
            [Question.TriamondsPulsingColours] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What colour triangle pulsed {1} in {0}?",
                Answers = new Dictionary<string, string>
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
                NeedsTranslation = true,
                QuestionText = "Which of these was one of the passwords in {0}?",
            },

            // Turtle Robot
            // What was the {1} line you commented out in {0}?
            // What was the first line you commented out in Turtle Robot?
            [Question.TurtleRobotCodeLines] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} line you commented out in {0}?",
            },

            // Two Bits
            // What was the {1} correct query response from {0}?
            // What was the first correct query response from Two Bits?
            [Question.TwoBitsResponse] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} correct query response from {0}?",
            },

            // Ultimate Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Ultimate Cipher?
            [Question.UltimateCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameDative = "Ultimativen Geheimschrift",
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Ultimative Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // Ultimate Cycle
            // What was the {1} in {0}?
            // What was the message in Ultimate Cycle?
            [Question.UltimateCycleWord] = new()
            {
                QuestionText = "Was war die {1} in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["message"] = "Nachricht",
                    ["response"] = "Antwort",
                },
            },

            // The Ultracube
            // What was the {1} rotation in {0}?
            // What was the first rotation in The Ultracube?
            [Question.UltracubeRotations] = new()
            {
                QuestionText = "Was war die {1}e Rotation in {0}?",
            },

            // UltraStores
            // What was the {1} rotation in the {2} stage of {0}?
            // What was the first rotation in the first stage of UltraStores?
            [Question.UltraStoresSingleRotation] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} rotation in the {2} stage of {0}?",
            },
            // What was the {1} rotation in the {2} stage of {0}?
            // What was the first rotation in the first stage of UltraStores?
            [Question.UltraStoresMultiRotation] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} rotation in the {2} stage of {0}?",
            },

            // Uncolored Squares
            // What was the {1} color in reading order used in the first stage of {0}?
            // What was the first color in reading order used in the first stage of Uncolored Squares?
            [Question.UncoloredSquaresFirstStage] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} color in reading order used in the first stage of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Weiß",
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Magenta"] = "Magenta",
                },
            },

            // Uncolored Switches
            // What was the initial state of the switches in {0}?
            // What was the initial state of the switches in Uncolored Switches?
            [Question.UncoloredSwitchesInitialState] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial state of the switches in {0}?",
            },
            // What color was the {1} LED in reading order in {0}?
            // What color was the first LED in reading order in Uncolored Switches?
            [Question.UncoloredSwitchesLedColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} LED in reading order in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                    ["turquoise"] = "turquoise",
                    ["orange"] = "orange",
                    ["purple"] = "lila",
                    ["white"] = "weiß",
                    ["black"] = "schwarz",
                },
            },

            // Unfair Cipher
            // What was the {1} received instruction in {0}?
            // What was the first received instruction in Unfair Cipher?
            [Question.UnfairCipherInstructions] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} received instruction in {0}?",
            },

            // Unfair’s Revenge
            // What was the {1} decrypted instruction in {0}?
            // What was the first decrypted instruction in Unfair’s Revenge?
            [Question.UnfairsRevengeInstructions] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} decrypted instruction in {0}?",
            },

            // Unicode
            // What was the {1} submitted code in {0}?
            // What was the first submitted code in Unicode?
            [Question.UnicodeSortedAnswer] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} submitted code in {0}?",
            },

            // UNO!
            // What was the initial card in {0}?
            // What was the initial card in UNO!?
            [Question.UnoInitialCard] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial card in {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What color was this key in the {1} stage of {0}?",
            },
            // What color was the label of this key in the {1} stage of {0}?
            // What color was the label of this key in the first stage of Unordered Keys?
            [Question.UnorderedKeysLabelColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the label of this key in the {1} stage of {0}?",
            },
            // What was the label of this key in the {1} stage of {0}?
            // What was the label of this key in the first stage of Unordered Keys?
            [Question.UnorderedKeysLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label of this key in the {1} stage of {0}?",
            },

            // Unown Cipher
            // What was the {1} submitted letter in {0}?
            // What was the first submitted letter in Unown Cipher?
            [Question.UnownCipherAnswers] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} submitted letter in {0}?",
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
                QuestionText = "Wie lautete bei {0} der Text?",
            },
            // What was the {1} color in the sequence on {0}?
            // What was the first color in the sequence on Updog?
            [Question.UpdogColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Was war bei {0} die {1} Farbe in der Farbsequenz?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first"] = "erste",
                    ["last"] = "letzte",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Orange"] = "Orange",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Purple"] = "Lila",
                },
            },

            // USA Cycle
            // Which state was displayed in {0}?
            // Which state was displayed in USA Cycle?
            [Question.USACycleDisplayed] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which state was displayed in {0}?",
            },

            // USA Maze
            // Which state did you depart from in {0}?
            // Which state did you depart from in USA Maze?
            [Question.USAMazeOrigin] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which state did you depart from in {0}?",
            },

            // V
            // Which word {1} shown in {0}?
            // Which word was shown in V?
            [Question.VWords] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which word {1} shown in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["was"] = "was",
                    ["was not"] = "was not",
                },
            },

            // Valves
            // What was the initial state of {0}?
            // What was the initial state of Valves?
            [Question.ValvesInitialState] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial state of {0}?",
            },

            // Varicolored Squares
            // What was the initially pressed color on {0}?
            // What was the initially pressed color on Varicolored Squares?
            [Question.VaricoloredSquaresInitialColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initially pressed color on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["White"] = "Weiß",
                    ["Red"] = "Rot",
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Yellow"] = "Gelb",
                    ["Magenta"] = "Magenta",
                },
            },

            // Varicolour Flash
            // What was the word of the {1} goal in {0}?
            // What was the word of the first goal in Varicolour Flash?
            [Question.VaricolourFlashWords] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the word of the {1} goal in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                    ["White"] = "Weiß",
                },
            },
            // What was the color of the {1} goal in {0}?
            // What was the color of the first goal in Varicolour Flash?
            [Question.VaricolourFlashColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the colour of the {1} goal in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Magenta"] = "Magenta",
                    ["Yellow"] = "Gelb",
                    ["White"] = "Weiß",
                },
            },

            // Variety
            // What color was the LED flashing in {0}?
            // What color was the LED flashing in Variety?
            [Question.VarietyLED] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the LED flashing in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Blue"] = "Blau",
                    ["White"] = "Weiß",
                    ["Black"] = "Schwarz",
                },
            },
            // What digit was displayed but not the answer for the digit display in {0}?
            // What digit was displayed but not the answer for the digit display in Variety?
            [Question.VarietyDigitDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What digit was displayed but not the answer for the digit display in {0}?",
            },
            // What word could be formed but was not the answer for the letter display in {0}?
            // What word could be formed but was not the answer for the letter display in Variety?
            [Question.VarietyLetterDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What word could be formed but was not the answer for the letter display in {0}?",
            },
            // What was the maximum display for the {1}timer in {0}?
            // What was the maximum display for the timer in Variety?
            [Question.VarietyTimer] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the maximum display for the {1}timer in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    [""] = "",
                    ["ascending "] = "ascending ",
                    ["descending "] = "descending ",
                },
            },
            // What was n for the {1}knob in {0}?
            // What was n for the knob in Variety?
            [Question.VarietyColoredKnob] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was n for the {1}knob in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    [""] = "",
                    ["colored "] = "colored ",
                    ["red "] = "red ",
                    ["black "] = "black ",
                    ["blue "] = "blue ",
                    ["yellow "] = "yellow ",
                },
            },
            // What was n for the {1}bulb in {0}?
            // What was n for the bulb in Variety?
            [Question.VarietyBulb] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was n for the {1}bulb in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    [""] = "",
                    ["red "] = "red ",
                    ["yellow "] = "yellow ",
                },
            },

            // Vcrcs
            // What was the word in {0}?
            // What was the word in Vcrcs?
            [Question.VcrcsWord] = new()
            {
                QuestionText = "Was war das Wort bei {0}?",
            },

            // Vectors
            // What was the color of the {1} vector in {0}?
            // What was the color of the first vector in Vectors?
            [Question.VectorsColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Welche Farbe hatte bei {0} der {1} Vektor?",
                ModuleName = "Vektoren",
                FormatArgs = new Dictionary<string, string>
                {
                    ["first"] = "erste",
                    ["second"] = "zweite",
                    ["third"] = "dritte",
                    ["only"] = "einzige",
                },
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Orange"] = "Orange",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Blue"] = "Blau",
                    ["Purple"] = "Violett",
                },
            },

            // Vexillology
            // What was the {1} flagpole color on {0}?
            // What was the first flagpole color on Vexillology?
            [Question.VexillologyColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} flagpole color on {0}?",
                Answers = new Dictionary<string, string>
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

            // Violet Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Violet Cipher?
            [Question.VioletCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameDative = "Violetten Geheimschrift",
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Violette Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // Visual Impairment
            // What was the desired color in the {1} stage on {0}?
            // What was the desired color in the first stage on Visual Impairment?
            [Question.VisualImpairmentColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the desired color in the {1} stage on {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blau",
                    ["Green"] = "Grün",
                    ["Red"] = "Rot",
                    ["White"] = "Weiß",
                },
            },

            // Walking Cube
            // Which of these cells was part of the cube’s path in {0}?
            // Which of these cells was part of the cube’s path in Walking Cube?
            [Question.WalkingCubePath] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these cells was part of the cube's path in {0}?",
            },

            // Warning Signs
            // What was the displayed sign in {0}?
            // What was the displayed sign in Warning Signs?
            [Question.WarningSignsDisplayedSign] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed sign in {0}?",
            },

            // WASD
            // What was the location displayed in {0}?
            // What was the location displayed in WASD?
            [Question.WasdDisplayedLocation] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the location displayed in {0}?",
            },

            // Watching Paint Dry
            // How many brush strokes were heard in {0}?
            // How many brush strokes were heard in Watching Paint Dry?
            [Question.WatchingPaintDryStrokeCount] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many brush strokes were heard in {0}?",
            },

            // Wavetapping
            // What was the color on the {1} stage in {0}?
            // What was the color on the first stage in Wavetapping?
            [Question.WavetappingColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color on the {1} stage in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Orange"] = "Orange",
                    ["Orange-Yellow"] = "Orange-Yellow",
                    ["Chartreuse"] = "Chartreuse",
                    ["Lime"] = "Lime",
                    ["Green"] = "Grün",
                    ["Seafoam Green"] = "Seafoam Green",
                    ["Cyan-Green"] = "Cyan-Green",
                    ["Turquoise"] = "Turquoise",
                    ["Dark Blue"] = "Dark Blue",
                    ["Indigo"] = "Indigo",
                    ["Purple"] = "Lila",
                    ["Purple-Magenta"] = "Purple-Magenta",
                    ["Magenta"] = "Magenta",
                    ["Pink"] = "Pink",
                    ["Gray"] = "Grau",
                },
            },
            // What was the correct pattern on the {1} stage in {0}?
            // What was the correct pattern on the first stage in Wavetapping?
            [Question.WavetappingPatterns] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the correct pattern on the {1} stage in {0}?",
            },

            // The Weakest Link
            // Who did you eliminate in {0}?
            // Who did you eliminate in The Weakest Link?
            [Question.WeakestLinkElimination] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Who did you eliminate in {0}?",
            },
            // Who made it to the Money Phase with you in {0}?
            // Who made it to the Money Phase with you in The Weakest Link?
            [Question.WeakestLinkMoneyPhaseName] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Who made it to the Money Phase with you in {0}?",
            },
            // What ratio did {1} get in the Question Phase in {0}?
            // What ratio did Annie get in the Question Phase in The Weakest Link?
            [Question.WeakestLinkRatio] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What ratio did {1} get in the Question Phase in {0}?",
            },
            // What was {1}’s skill in {0}?
            // What was Annie’s skill in The Weakest Link?
            [Question.WeakestLinkSkill] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was {1}’s skill in {0}?",
            },

            // What’s on Second
            // What was the display text in the {1} stage of {0}?
            // What was the display text in the first stage of What’s on Second?
            [Question.WhatsOnSecondDisplayText] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the display text in the {1} stage of {0}?",
            },
            // What was the display text color in the {1} stage of {0}?
            // What was the display text color in the first stage of What’s on Second?
            [Question.WhatsOnSecondDisplayColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the display text color in the {1} stage of {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Blue"] = "Blau",
                    ["Cyan"] = "Türkis",
                    ["Green"] = "Grün",
                    ["Magenta"] = "Magenta",
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
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
                Gender = Gender.Feminine,
                ModuleNameDative = "Weißen Geheimschrift",
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Weiße Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // WhoOF
            // What was the display in the {1} stage on {0}?
            // What was the display in the first stage on WhoOF?
            [Question.WhoOFDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the display in the {1} stage on {0}?",
            },

            // Who’s on First
            // What was the display in the {1} stage on {0}?
            // What was the display in the first stage on Who’s on First?
            [Question.WhosOnFirstDisplay] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the display in the {1} stage on {0}?",
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
                NeedsTranslation = true,
                QuestionText = "What word was transmitted in the {1} stage on {0}?",
            },

            // The Wire
            // What was the color of the {1} dial in {0}?
            // What was the color of the top dial in The Wire?
            [Question.WireDialColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} dial in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "top",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["grey"] = "grey",
                    ["orange"] = "orange",
                    ["purple"] = "lila",
                    ["red"] = "rot",
                },
            },
            // What was the displayed number in {0}?
            // What was the displayed number in The Wire?
            [Question.WireDisplayedNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the displayed number in {0}?",
            },

            // Wire Ordering
            // What color was the {1} display from the left in {0}?
            // What color was the first display from the left in Wire Ordering?
            [Question.WireOrderingDisplayColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} display from the left in {0}?",
                Answers = new Dictionary<string, string>
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
            // What number was on the {1} display from the left in {0}?
            // What number was on the first display from the left in Wire Ordering?
            [Question.WireOrderingDisplayNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What number was on the {1} display from the left in {0}?",
            },
            // What color was the {1} wire from the left in {0}?
            // What color was the first wire from the left in Wire Ordering?
            [Question.WireOrderingWireColor] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} wire from the left in {0}?",
                Answers = new Dictionary<string, string>
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

            // Wire Sequence
            // How many {1} wires were there in {0}?
            // How many red wires were there in Wire Sequence?
            [Question.WireSequenceColorCount] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many {1} wires were there in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["blue"] = "blau",
                    ["black"] = "schwarz",
                },
            },

            // Wolf, Goat, and Cabbage
            // Which of these was {1} on {0}?
            // Which of these was present on Wolf, Goat, and Cabbage?
            [Question.WolfGoatAndCabbageAnimals] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which of these was {1} on {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["present"] = "present",
                    ["not present"] = "not present",
                },
            },
            // What was the boat size in {0}?
            // What was the boat size in Wolf, Goat, and Cabbage?
            [Question.WolfGoatAndCabbageBoatSize] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the boat size in {0}?",
            },

            // Working Title
            // What was the label shown in {0}?
            // What was the label shown in Working Title?
            [Question.WorkingTitleLabel] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the label shown in {0}?",
            },

            // Wumbo
            // What was the number in {0}?
            // What was the number in Wumbo?
            [Question.WumboNumber] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the number in {0}?",
            },

            // The Xenocryst
            // What was the color of the {1} flash in {0}?
            // What was the color of the first flash in The Xenocryst?
            [Question.Xenocryst] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the color of the {1} flash in {0}?",
            },

            // XmORse Code
            // What was the {1} displayed letter (in reading order) in {0}?
            // What was the first displayed letter (in reading order) in XmORse Code?
            [Question.XmORseCodeDisplayedLetters] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} displayed letter (in reading order) in {0}?",
            },
            // What word did you decrypt in {0}?
            // What word did you decrypt in XmORse Code?
            [Question.XmORseCodeWord] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What word did you decrypt in {0}?",
            },

            // xobekuJ ehT
            // What song was played on {0}?
            // What song was played on xobekuJ ehT?
            [Question.XobekuJehTSong] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What song was played on {0}?",
            },

            // X-Ring
            // Which symbol was scanned in {0}?
            // Which symbol was scanned in X-Ring?
            [Question.XRingSymbol] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which symbol was scanned in {0}?",
            },

            // XY-Ray
            // Which shape was scanned by {0}?
            // Which shape was scanned by XY-Ray?
            [Question.XYRayShapes] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Which shape was scanned by {0}?",
            },

            // Yahtzee
            // What was the initial roll on {0}?
            // What was the initial roll on Yahtzee?
            [Question.YahtzeeInitialRoll] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the initial roll on {0}?",
                Answers = new Dictionary<string, string>
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

            // Yellow Arrows
            // What was the starting row letter in {0}?
            // What was the starting row letter in Yellow Arrows?
            [Question.YellowArrowsStartingRow] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the starting row letter in {0}?",
            },

            // The Yellow Button
            // What was the {1} color in {0}?
            // What was the first color in The Yellow Button?
            [Question.YellowButtonColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the {1} color in {0}?",
                Answers = new Dictionary<string, string>
                {
                    ["Red"] = "Rot",
                    ["Yellow"] = "Gelb",
                    ["Green"] = "Grün",
                    ["Cyan"] = "Türkis",
                    ["Blue"] = "Blau",
                    ["Magenta"] = "Magenta",
                },
            },

            // Yellow Button’t
            // What was the filename in {0}?
            // What was the filename in Yellow Button’t?
            [Question.YellowButtontFilename] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What was the filename in {0}?",
            },

            // Yellow Cipher
            // What was on the {1} screen on page {2} in {0}?
            // What was on the top screen on page 1 in Yellow Cipher?
            [Question.YellowCipherScreen] = new()
            {
                Gender = Gender.Feminine,
                ModuleNameDative = "Gelben Geheimschrift",
                QuestionText = "Was war bei {0} auf dem {1}en Bildschirm auf Seite {2}?",
                ModuleName = "Gelbe Geheimschrift",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top"] = "ober",
                    ["middle"] = "mittler",
                    ["bottom"] = "unter",
                },
            },

            // Zero, Zero
            // What color was the {1} star in {0}?
            // What color was the top-left star in Zero, Zero?
            [Question.ZeroZeroStarColors] = new()
            {
                NeedsTranslation = true,
                QuestionText = "What color was the {1} star in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-right"] = "top-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
                Answers = new Dictionary<string, string>
                {
                    ["black"] = "schwarz",
                    ["blue"] = "blau",
                    ["green"] = "grün",
                    ["cyan"] = "türkis",
                    ["red"] = "rot",
                    ["magenta"] = "magenta",
                    ["yellow"] = "gelb",
                    ["white"] = "weiß",
                },
            },
            // How many points were on the {1} star in {0}?
            // How many points were on the top-left star in Zero, Zero?
            [Question.ZeroZeroStarPoints] = new()
            {
                NeedsTranslation = true,
                QuestionText = "How many points were on the {1} star in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["top-left"] = "top-left",
                    ["top-right"] = "top-right",
                    ["bottom-left"] = "bottom-left",
                    ["bottom-right"] = "bottom-right",
                },
            },
            // Where was the {1} square in {0}?
            // Where was the red square in Zero, Zero?
            [Question.ZeroZeroSquares] = new()
            {
                NeedsTranslation = true,
                QuestionText = "Where was the {1} square in {0}?",
                FormatArgs = new Dictionary<string, string>
                {
                    ["red"] = "rot",
                    ["green"] = "grün",
                    ["blue"] = "blau",
                },
            },

            // Zoni
            // What was the {1} word in {0}?
            // What was the first word in Zoni?
            [Question.ZoniWords] = new()
            {
                QuestionText = "Was war bei {0} das {1}e Wort?",
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
            "Das merkwürdige Verhalten explosionsreifer Bomben zur Detonationszeit" // „Das merkwürdige Verhalten geschlechtsreifer Großstädter zur Paarungszeit“ (Film, 1998)
        );
    }
}
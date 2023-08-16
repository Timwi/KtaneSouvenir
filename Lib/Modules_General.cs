using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    const string Version = "5.3";

    // The values here are the “ModuleType” property on the KMBombModule components.
    const string _1000Words = "OneThousandWords";
    const string _100LevelsOfDefusal = "100LevelsOfDefusal";
    const string _1DChess = "1DChess";
    const string _3DMaze = "spwiz3DMaze";
    const string _3DTapCode = "3DTapCodeModule";
    const string _3DTunnels = "3dTunnels";
    const string _3LEDs = "threeLEDsModule";
    const string _3NPlus1 = "threeNPlusOne";
    const string _64 = "64";
    const string _7 = "7";
    const string _9Ball = "GSNineBall";
    const string _Abyss = "GSAbyss";
    const string _Accumulation = "accumulation";
    const string _AdventureGame = "spwizAdventureGame";
    const string _AffineCycle = "affineCycle";
    const string _ALetter = "LetterModule";
    const string _AlfaBravo = "alfa_bravo";
    const string _Algebra = "algebra";
    const string _Algorithmia = "algorithmia";
    const string _AlphabeticalRuling = "alphabeticalRuling";
    const string _AlphabetNumbers = "alphabetNumbers";
    const string _AlphabetTiles = "AlphabetTiles";
    const string _AlphaBits = "alphaBits";
    const string _AngelHernandez = "AngelHernandezModule";
    const string _Arithmelogic = "arithmelogic";
    const string _ASCIIMaze = "asciiMaze";
    const string _ASquare = "ASquareModule";
    const string _AzureButton = "AzureButtonModule";
    const string _Bakery = "bakery";
    const string _BamboozledAgain = "bamboozledAgain";
    const string _BamboozlingButton = "bamboozlingButton";
    const string _BarcodeCipher = "BarcodeCipherModule";
    const string _Bartending = "BartendingModule";
    const string _Beans = "beans";
    const string _BeanSprouts = "beanSprouts";
    const string _BigBean = "bigBean";
    const string _BigCircle = "BigCircle";
    const string _Binary = "Binary";
    const string _BinaryLEDs = "BinaryLeds";
    const string _BinaryShift = "binary_shift";
    const string _Bitmaps = "BitmapsModule";
    const string _BlackCipher = "blackCipher";
    const string _BlindMaze = "BlindMaze";
    const string _Blinkstop = "blinkstopModule";
    const string _Blockbusters = "blockbusters";
    const string _BlueArrows = "blueArrowsModule";
    const string _BlueButton = "BlueButtonModule";
    const string _BlueCipher = "blueCipher";
    const string _BobBarks = "ksmBobBarks";
    const string _Boggle = "boggle";
    const string _BombDiffusal = "bombDiffusal";
    const string _BooleanWires = "booleanWires";
    const string _BoomtarTheGreat = "boomtarTheGreat";
    const string _Boxing = "boxing";
    const string _Braille = "BrailleModule";
    const string _BreakfastEgg = "breakfastEgg";
    const string _BrokenButtons = "BrokenButtonsModule";
    const string _BrokenGuitarChords = "BrokenGuitarChordsModule";
    const string _BrownCipher = "brownCipher";
    const string _BrushStrokes = "brushStrokes";
    const string _Bulb = "TheBulbModule";
    const string _BurgerAlarm = "burgerAlarm";
    const string _BurglarAlarm = "burglarAlarm";
    const string _Button = "BigButton";
    const string _ButtonSequence = "buttonSequencesModule";
    const string _CaesarCycle = "caesarCycle";
    const string _Calendar = "calendar";
    const string _Cartinese = "cartinese";
    const string _Catchphrase = "catchphrase";
    const string _ChallengeAndContact = "challengeAndContact";
    const string _CharacterCodes = "characterCodes";
    const string _CharacterSlots = "characterSlots";
    const string _CheapCheckout = "CheapCheckoutModule";
    const string _CheepCheckout = "cheepCheckout";
    const string _Chess = "ChessModule";
    const string _ChineseCounting = "chineseCounting";
    const string _ChordQualities = "ChordQualities";
    const string _Code = "theCodeModule";
    const string _Codenames = "codenames";
    const string _Coffeebucks = "coffeebucks";
    const string _Coinage = "Coinage";
    const string _ColorAddition = "colorAddition";
    const string _ColorBraille = "ColorBrailleModule";
    const string _ColorDecoding = "Color Decoding";
    const string _ColoredKeys = "lgndColoredKeys";
    const string _ColoredSquares = "ColoredSquaresModule";
    const string _ColoredSwitches = "ColoredSwitchesModule";
    const string _ColorMorse = "ColorMorseModule";
    const string _ColorsMaximization = "colors_maximization";
    const string _ColourFlash = "ColourFlash";
    const string _ConnectionCheck = "graphModule";
    const string _Coordinates = "CoordinatesModule";
    const string _CoralCipher = "coralCipher";
    const string _Corners = "CornersModule";
    const string _CornflowerCipher = "cornflowerCipher";
    const string _Cosmic = "CosmicModule";
    const string _CrazyHamburger = "GSCrazyHamburger";
    const string _CrazyMaze = "CrazyMazeModule";
    const string _CreamCipher = "creamCipher";
    const string _Creation = "CreationModule";
    const string _CrimsonCipher = "crimsonCipher";
    const string _Critters = "CrittersModule";
    const string _CruelBinary = "CruelBinary";
    const string _CruelKeypads = "CruelKeypads";
    const string _CrypticCycle = "crypticCycle";
    const string _CrypticKeypad = "GSCrypticKeypad";
    const string _Cube = "cube";
    const string _CursedDoubleOh = "CursedDoubleOhModule";
    const string _CustomerIdentification = "xelCustomerIdentification";
    const string _CyanButton = "CyanButtonModule";
    const string _DACHMaze = "DACH";
    const string _DeafAlley = "deafAlleyModule";
    const string _DeckOfManyThings = "deckOfManyThings";
    const string _DecoloredSquares = "DecoloredSquaresModule";
    const string _DecolourFlash = "DecolourFlashModule";
    const string _DevilishEggs = "devilishEggs";
    const string _Digisibility = "digisibility";
    const string _DigitString = "digitString";
    const string _DimensionDisruption = "dimensionDisruption";
    const string _DirectionalButton = "directionalButton";
    const string _DiscoloredSquares = "DiscoloredSquaresModule";
    const string _DivisibleNumbers = "divisibleNumbers";
    const string _DoubleArrows = "doubleArrows";
    const string _DoubleColor = "doubleColor";
    const string _DoubleDigits = "doubleDigitsModule";
    const string _DoubleExpert = "doubleExpert";
    const string _DoubleOh = "DoubleOhModule";
    const string _DrDoctor = "DrDoctorModule";
    const string _Dreamcipher = "ksmDreamcipher";
    const string _Duck = "theDuck";
    const string _DumbWaiters = "dumbWaiters";
    const string _eeBgnillepS = "eeBgnilleps";
    const string _Eight = "eight";
    const string _ElderFuthark = "elderFuthark";
    const string _EnaCipher = "enaCipher";
    const string _EncryptedDice = "EncryptedDice";
    const string _EncryptedEquations = "EncryptedEquationsModule";
    const string _EncryptedHangman = "encryptedHangman";
    const string _EncryptedMaze = "encryptedMaze";
    const string _EncryptedMorse = "EncryptedMorse";
    const string _EncryptionBingo = "encryptionBingo";
    const string _EnigmaCycle = "enigmaCycle";
    const string _EntryNumberFour = "GSEntryNumberFour";
    const string _EntryNumberOne = "GSEntryNumberOne";
    const string _EquationsX = "equationsXModule";
    const string _Etterna = "etterna";
    const string _Exoplanets = "exoplanets";
    const string _FactoringMaze = "factoringMaze";
    const string _FactoryMaze = "factoryMaze";
    const string _FastMath = "fastMath";
    const string _FaultyButtons = "GSFaultyButtons";
    const string _FaultyRGBMaze = "faultyrgbMaze";
    const string _FindTheDate = "DateFinder";
    const string _FiveLetterWords = "FiveLetterWords";
    const string _FizzBuzz = "fizzBuzzModule";
    const string _Flags = "FlagsModule";
    const string _FlashingArrows = "flashingArrowsModule";
    const string _FlashingLights = "flashingLights";
    const string _Flyswatting = "flyswatting";
    const string _FollowMe = "FollowMe";
    const string _ForestCipher = "forestCipher";
    const string _ForgetAnyColor = "ForgetAnyColor";
    const string _ForgetEverything = "HexiEvilFMN";
    const string _ForgetMe = "forgetMe";
    const string _ForgetMeNot = "MemoryV2";
    const string _ForgetsUltimateShowdown = "ForgetsUltimateShowdownModule";
    const string _ForgetTheColors = "ForgetTheColors";
    const string _ForgetThis = "forgetThis";
    const string _FreeParking = "freeParking";
    const string _Functions = "qFunctions";
    const string _FuseBox = "FuseBox";
    const string _GadgetronVendor = "lgndGadgetronVendor";
    const string _GameOfLifeCruel = "GameOfLifeCruel";
    const string _Gamepad = "TheGamepadModule";
    const string _GarnetThief = "theGarnetThief";
    const string _Girlfriend = "Girlfriend";
    const string _GlitchedButton = "GlitchedButtonModule";
    const string _GrayButton = "GrayButtonModule";
    const string _GrayCipher = "grayCipher";
    const string _GreatVoid = "greatVoid";
    const string _GreenArrows = "greenArrowsModule";
    const string _GreenButton = "GreenButtonModule";
    const string _GreenCipher = "greenCipher";
    const string _GridLock = "GridlockModule";
    const string _GroceryStore = "groceryStore";
    const string _Gryphons = "gryphons";
    const string _GuessWho = "GuessWho";
    const string _h = "Averageh";
    const string _HereditaryBaseNotation = "hereditaryBaseNotationModule";
    const string _Hexabutton = "hexabutton";
    const string _Hexamaze = "HexamazeModule";
    const string _HexOS = "hexOS";
    const string _HiddenColors = "lgndHiddenColors";
    const string _HighScore = "ksmHighScore";
    const string _HillCycle = "hillCycle";
    const string _Hinges = "hinges";
    const string _Hogwarts = "HogwartsModule";
    const string _HoldUps = "KritHoldUps";
    const string _Homophones = "homophones";
    const string _HorribleMemory = "horribleMemory";
    const string _HumanResources = "HumanResourcesModule";
    const string _Hunting = "hunting";
    const string _Hypercube = "TheHypercubeModule";
    const string _Hyperlink = "hyperlink";
    const string _IceCream = "iceCreamModule";
    const string _IdentificationCrisis = "identificationCrisis";
    const string _IdentityParade = "identityParade";
    const string _Impostor = "impostor";
    const string _IndigoCipher = "indigoCipher";
    const string _InfiniteLoop = "InfiniteLoop";
    const string _Ingredients = "ingredients";
    const string _InnerConnections = "InnerConnectionsModule";
    const string _Interpunct = "interpunct";
    const string _IPA = "ipa";
    const string _iPhone = "iPhone";
    const string _Jenga = "jenga";
    const string _JewelVault = "jewelVault";
    const string _JumbleCycle = "jumbleCycle";
    const string _JuxtacoloredSquares = "JuxtacoloredSquaresModule";
    const string _Kanji = "KanjiModule";
    const string _KanyeEncounter = "TheKanyeEncounter";
    const string _KeypadMagnified = "keypadMagnified";
    const string _KnowYourWay = "KnowYourWay";
    const string _Kudosudoku = "KudosudokuModule";
    const string _Labyrinth = "labyrinth";
    const string _LadderLottery = "ladderLottery";
    const string _Ladders = "ladders";
    const string _Lasers = "lasers";
    const string _LEDEncryption = "LEDEnc";
    const string _LEDMath = "lgndLEDMath";
    const string _LEDs = "leds";
    const string _LEGOs = "LEGOModule";
    const string _LetterMath = "letterMath";
    const string _LightBulbs = "LightBulbs";
    const string _Linq = "Linq";
    const string _LionsShare = "LionsShareModule";
    const string _Listening = "Listening";
    const string _LogicalButtons = "logicalButtonsModule";
    const string _LogicGates = "logicGates";
    const string _LombaxCubes = "lgndLombaxCubes";
    const string _LondonUnderground = "londonUnderground";
    const string _LongWords = "LongWords";
    const string _MadMemory = "MadMemory";
    const string _Mafia = "MafiaModule";
    const string _MagentaCipher = "magentaCipher";
    const string _Mahjong = "MahjongModule";
    const string _MandMs = "MandMs";
    const string _MandNs = "MandNs";
    const string _MaritimeFlags = "MaritimeFlagsModule";
    const string _MaroonCipher = "maroonCipher";
    const string _Mashematics = "mashematics";
    const string _MathEm = "mathem";
    const string _Matrix = "matrix";
    const string _Maze = "Maze";
    const string _Maze3 = "maze3";
    const string _MazeIdentification = "GSMazeIdentification";
    const string _Mazematics = "mazematics";
    const string _MazeScrambler = "MazeScrambler";
    const string _Mazeseeker = "GSMazeseeker";
    const string _MegaMan2 = "megaMan2";
    const string _MelodySequencer = "melodySequencer";
    const string _MemorableButtons = "memorableButtons";
    const string _Memory = "Memory";
    const string _Metamorse = "metamorse";
    const string _Metapuzzle = "metapuzzle";
    const string _Microcontroller = "Microcontroller";
    const string _Minesweeper = "MinesweeperModule";
    const string _Mirror = "mirror";
    const string _MisterSoftee = "misterSoftee";
    const string _ModernCipher = "modernCipher";
    const string _ModuleListening = "moduleListening";
    const string _ModuleMaze = "ModuleMaze";
    const string _MonsplodeFight = "monsplodeFight";
    const string _MonsplodeTradingCards = "monsplodeCards";
    const string _Moon = "moon";
    const string _MoreCode = "MoreCode";
    const string _MorseAMaze = "MorseAMaze";
    const string _MorseButtons = "morseButtons";
    const string _Morsematics = "MorseV2";
    const string _MorseWar = "MorseWar";
    const string _MouseInTheMaze = "MouseInTheMaze";
    const string _MSeq = "mSeq";
    const string _MulticoloredSwitches = "R4YMultiColoredSwitches";
    const string _Murder = "murder";
    const string _MysteryModule = "mysterymodule";
    const string _MysticSquare = "MysticSquareModule";
    const string _NameCodes = "nameCodes";
    const string _NandMs = "NandMs";
    const string _Navinums = "navinums";
    const string _NavyButton = "NavyButtonModule";
    const string _Necronomicon = "necronomicon";
    const string _Negativity = "Negativity";
    const string _Neutralization = "neutralization";
    const string _NonverbalSimon = "nonverbalSimon";
    const string _NotColoredSquares = "NotColoredSquaresModule";
    const string _NotColoredSwitches = "NotColoredSwitchesModule";
    const string _NotConnectionCheck = "notConnectionCheck";
    const string _NotCoordinates = "notCoordinates";
    const string _NotKeypad = "NotKeypad";
    const string _NotMaze = "NotMaze";
    const string _NotMorseCode = "NotMorseCode";
    const string _NotMorsematics = "notMorsematics";
    const string _NotMurder = "notMurder";
    const string _NotNumberPad = "notNumberPad";
    const string _NotPerspectivePegs = "NotPerspectivePegsModule";
    const string _NotPianoKeys = "notPianoKeys";
    const string _NotSimaze = "NotSimaze";
    const string _NotTextField = "notTextField";
    const string _NotTheBulb = "notTheBulb";
    const string _NotTheButton = "NotButton";
    const string _NotTheScrew = "notTheScrew";
    const string _NotWhosOnFirst = "NotWhosOnFirst";
    const string _NotWordSearch = "notWordSearch";
    const string _NotX01 = "notX01";
    const string _NotXRay = "NotXRayModule";
    const string _NumberedButtons = "numberedButtonsModule";
    const string _Numbers = "Numbers";
    const string _Numpath = "numpath";
    const string _ObjectShows = "objectShows";
    const string _Octadecayotton = "TheOctadecayotton";
    const string _OddOneOut = "OddOneOutModule";
    const string _OldAI = "SCP079";
    const string _OldFogey = "oldFogey";
    const string _OnlyConnect = "OnlyConnectModule";
    const string _OrangeArrows = "orangeArrowsModule";
    const string _OrangeCipher = "orangeCipher";
    const string _OrderedKeys = "orderedKeys";
    const string _OrderPicking = "OrderPickingModule";
    const string _OrientationCube = "OrientationCube";
    const string _OrientationHypercube = "OrientationHypercube";
    const string _Palindromes = "palindromes";
    const string _Parity = "parity";
    const string _PartialDerivatives = "partialDerivatives";
    const string _PassportControl = "passportControl";
    const string _PasswordDestroyer = "pwDestroyer";
    const string _PatternCube = "PatternCubeModule";
    const string _PeriodicWords = "periodicWordsRB";
    const string _PerspectivePegs = "spwizPerspectivePegs";
    const string _Phosphorescence = "Phosphorescence";
    const string _Pictionary = "pictionaryModule";
    const string _Pie = "pieModule";
    const string _PieFlash = "pieFlash";
    const string _PigpenCycle = "pigpenCycle";
    const string _PinkButton = "PinkButtonModule";
    const string _PixelCipher = "pixelcipher";
    const string _PlaceholderTalk = "placeholderTalk";
    const string _PlacementRoulette = "PlacementRouletteModule";
    const string _Planets = "planets";
    const string _PlayfairCycle = "playfairCycle";
    const string _Poetry = "poetry";
    const string _PolyhedralMaze = "PolyhedralMazeModule";
    const string _PrimeEncryption = "primeEncryption";
    const string _Probing = "Probing";
    const string _ProceduralMaze = "ProceduralMaze";
    const string _PunctuationMarks = "punctuationMarks";
    const string _PurpleArrows = "purpleArrowsModule";
    const string _PurpleButton = "PurpleButtonModule";
    const string _PuzzleIdentification = "GSPuzzleIdentification";
    const string _Quaver = "Quaver";
    const string _QuestionMark = "Questionmark";
    const string _QuickArithmetic = "QuickArithmetic";
    const string _Quintuples = "quintuples";
    const string _Qwirkle = "qwirkle";
    const string _RaidingTemples = "raidingTemples";
    const string _RailwayCargoLoading = "RailwayCargoLoading";
    const string _RainbowArrows = "ksmRainbowArrows";
    const string _RecoloredSwitches = "R4YRecoloredSwitches";
    const string _RecursivePassword = "RecursivePassword";
    const string _RedArrows = "redArrowsModule";
    const string _RedCipher = "redCipher";
    const string _RedHerring = "RedHerring";
    const string _ReformedRoleReversal = "ReformedRoleReversal";
    const string _RegularCrazyTalk = "RegularCrazyTalkModule";
    const string _Retirement = "retirement";
    const string _ReverseMorse = "reverseMorse";
    const string _ReversePolishNotation = "revPolNot";
    const string _RGBMaze = "rgbMaze";
    const string _Rhythms = "MusicRhythms";
    const string _RoboScanner = "roboScannerModule";
    const string _Roger = "roger";
    const string _RoleReversal = "roleReversal";
    const string _Rule = "theRule";
    const string _RuleOfThree = "RuleOfThreeModule";
    const string _SafetySquare = "safetySquare";
    const string _Samsung = "theSamsung";
    const string _ScavengerHunt = "scavengerHunt";
    const string _SchlagDenBomb = "qSchlagDenBomb";
    const string _ScramboozledEggain = "ScramboozledEggainModule";
    const string _Scripting = "KritScripts";
    const string _ScrutinySquares = "scrutinySquares";
    const string _SeaShells = "SeaShells";
    const string _Semamorse = "semamorse";
    const string _Sequencyclopedia = "TheSequencyclopedia";
    const string _SetTheory = "SetTheory";
    const string _ShapesAndBombs = "ShapesBombs";
    const string _ShapeShift = "shapeshift";
    const string _ShiftedMaze = "shiftedMaze";
    const string _ShiftingMaze = "MazeShifting";
    const string _ShogiIdentification = "shogiIdentification";
    const string _SignLanguage = "signLanguage";
    const string _SillySlots = "SillySlots";
    const string _SiloAuthorization = "siloAuthorization";
    const string _SimonSaid = "simonSaidModule";
    const string _SimonSamples = "simonSamples";
    const string _SimonSays = "Simon";
    const string _SimonScrambles = "simonScrambles";
    const string _SimonScreams = "SimonScreamsModule";
    const string _SimonSelects = "simonSelectsModule";
    const string _SimonSends = "SimonSendsModule";
    const string _SimonShapes = "SimonShapesModule";
    const string _SimonShouts = "SimonShoutsModule";
    const string _SimonShrieks = "SimonShrieksModule";
    const string _SimonSimons = "simonSimons";
    const string _SimonSings = "SimonSingsModule";
    const string _SimonSmothers = "simonSmothers";
    const string _SimonSounds = "simonSounds";
    const string _SimonSpeaks = "SimonSpeaksModule";
    const string _SimonsStar = "simonsStar";
    const string _SimonStacks = "simonstacks";
    const string _SimonStages = "simonStages";
    const string _SimonStates = "SimonV2";
    const string _SimonStops = "simonStops";
    const string _SimonStores = "simonStores";
    const string _SimonSubdivides = "simonSubdivides";
    const string _SimonSupports = "simonSupports";
    const string _SkewedSlots = "SkewedSlotsModule";
    const string _Skyrim = "skyrim";
    const string _SlowMath = "SlowMathModule";
    const string _SmallCircle = "smallCircle";
    const string _Snooker = "snooker";
    const string _Snowflakes = "snowflakes";
    const string _SonicKnuckles = "sonicKnuckles";
    const string _SonicTheHedgehog = "sonic";
    const string _Sorting = "sorting";
    const string _Souvenir = "SouvenirModule";
    const string _SpaceTraders = "space_traders";
    const string _SpellingBee = "spellingBee";
    const string _Sphere = "sphere";
    const string _SplittingTheLoot = "SplittingTheLootModule";
    const string _SpongebobBirthdayIdentification = "spongebobBirthdayIdentification";
    const string _Stability = "stabilityModule";
    const string _StackedSequences = "stackedSequences";
    const string _Stars = "stars";
    const string _StateOfAggregation = "stateOfAggregation";
    const string _Stellar = "stellar";
    const string _StupidSlots = "stupidSlots";
    const string _SubscribeToPewdiepie = "subscribeToPewdiepie";
    const string _SugarSkulls = "sugarSkulls";
    const string _Superparsing = "superparsing";
    const string _Switch = "BigSwitch";
    const string _Switches = "switchModule";
    const string _SwitchingMaze = "MazeSwitching";
    const string _SymbolCycle = "SymbolCycleModule";
    const string _SymbolicCoordinates = "symbolicCoordinates";
    const string _SymbolicTasha = "symbolicTasha";
    const string _Sync_125_3 = "sync125_3";
    const string _Synonyms = "synonyms";
    const string _Sysadmin = "sysadmin";
    const string _TapCode = "tapCode";
    const string _TashaSqueals = "tashaSqueals";
    const string _TasqueManaging = "tasqueManaging";
    const string _TeaSet = "GSTeaSet";
    const string _TechnicalKeypad = "TechnicalKeypad";
    const string _TenButtonColorCode = "TenButtonColorCode";
    const string _Tenpins = "tenpins";
    const string _Tetriamonds = "tetriamonds";
    const string _TextField = "TextField";
    const string _ThinkingWires = "thinkingWiresModule";
    const string _ThirdBase = "ThirdBase";
    const string _TicTacToe = "TicTacToeModule";
    const string _Timezone = "timezone";
    const string _TipToe = "TipToe";
    const string _TopsyTurvy = "topsyTurvy";
    const string _TouchTransmission = "touchTransmission";
    const string _Trajectory = "Trajectory";
    const string _TransmittedMorse = "transmittedMorseModule";
    const string _Triamonds = "triamonds";
    const string _TripleTerm = "tripleTermModule";
    const string _TurtleRobot = "turtleRobot";
    const string _TwoBits = "TwoBits";
    const string _UltimateCipher = "ultimateCipher";
    const string _UltimateCycle = "ultimateCycle";
    const string _Ultracube = "TheUltracubeModule";
    const string _UltraStores = "UltraStores";
    const string _UncoloredSquares = "UncoloredSquaresModule";
    const string _UncoloredSwitches = "R4YUncoloredSwitches";
    const string _UnfairCipher = "unfairCipher";
    const string _UnfairsRevenge = "unfairsRevenge";
    const string _Unicode = "UnicodeModule";
    const string _UNO = "UNO";
    const string _UnownCipher = "UnownCipher";
    const string _USACycle = "USACycle";
    const string _USAMaze = "USA";
    const string _V = "V";
    const string _VaricoloredSquares = "VaricoloredSquaresModule";
    const string _VaricolourFlash = "varicolourFlash";
    const string _Vcrcs = "VCRCS";
    const string _Vectors = "vectorsModule";
    const string _Vexillology = "vexillology";
    const string _VioletCipher = "violetCipher";
    const string _VisualImpairment = "visual_impairment";
    const string _WarningSigns = "warningSigns";
    const string _Wavetapping = "Wavetapping";
    const string _WeakestLink = "TheWeakestLink";
    const string _WhatsOnSecond = "WhatsOnSecond";
    const string _WhiteCipher = "whiteCipher";
    const string _WhoOF = "whoOF";
    const string _WhosOnFirst = "WhosOnFirst";
    const string _WhosOnMorse = "whosOnMorseModule";
    const string _Wire = "wire";
    const string _WireOrdering = "kataWireOrdering";
    const string _WireSequence = "WireSequence";
    const string _WolfGoatAndCabbage = "wolfGoatCabbageModule";
    const string _WorkingTitle = "workingTitle";
    const string _Xenocryst = "GSXenocryst";
    const string _XmORseCode = "xmorse";
    const string _XobekuJehT = "xobekuj";
    const string _Yahtzee = "YahtzeeModule";
    const string _YellowArrows = "yellowArrowsModule";
    const string _YellowButton = "YellowButtonModule";
    const string _YellowCipher = "yellowCipher";
    const string _ZeroZero = "zeroZero";
    const string _Zoni = "lgndZoni";

    void Awake()
    {
        _moduleProcessors = new Dictionary<string, (Func<KMBombModule, IEnumerable<object>> processor, string moduleName, string contributor)>()
        {
            [_1000Words] = (Process1000Words, "1000 Words", "BigCrunch22"),
            [_100LevelsOfDefusal] = (Process100LevelsOfDefusal, "100 Levels of Defusal", "Espik"),
            [_1DChess] = (Process1DChess, "1D Chess", "Emik"),
            [_3DMaze] = (Process3DMaze, "3D Maze", "Timwi"),
            [_3DTapCode] = (Process3DTapCode, "3D Tap Code", "TasThiluna"),
            [_3DTunnels] = (Process3DTunnels, "3D Tunnels", "Timwi"),
            [_3LEDs] = (Process3LEDs, "3 LEDs", "Timwi"),
            [_3NPlus1] = (Process3NPlus1, "3N+1", "Hawker"),
            [_64] = (Process64, "64", "Kuro"),
            [_7] = (Process7, "7", "VFlyer"),
            [_9Ball] = (Process9Ball, "9-Ball", "GhostSalt"),
            [_Abyss] = (ProcessAbyss, "Abyss", "VFlyer"),
            [_Accumulation] = (ProcessAccumulation, "Accumulation", "luisdiogo98"),
            [_AdventureGame] = (ProcessAdventureGame, "Adventure Game", "Timwi"),
            [_AffineCycle] = (ProcessAffineCycle, "Affine Cycle", "TasThiluna"),
            [_ALetter] = (ProcessALetter, "A Letter", "Sierra"),
            [_AlfaBravo] = (ProcessAlfaBravo, "Alfa-Bravo", "NickLatkovich"),
            [_Algebra] = (ProcessAlgebra, "Algebra", "Timwi"),
            [_Algorithmia] = (ProcessAlgorithmia, "Algorithmia", "tandyCake"),
            [_AlphabeticalRuling] = (ProcessAlphabeticalRuling, "Alphabetical Ruling", "Timwi"),
            [_AlphabetNumbers] = (ProcessAlphabetNumbers, "Alphabet Numbers", "Kuro"),
            [_AlphabetTiles] = (ProcessAlphabetTiles, "Alphabet Tiles", "BigCrunch22"),
            [_AlphaBits] = (ProcessAlphaBits, "Alpha-Bits", "Timwi"),
            [_AngelHernandez] = (ProcessAngelHernandez, "Ángel Hernández", "QuinnWuest"),
            [_Arithmelogic] = (ProcessArithmelogic, "Arithmelogic", "JerryEris"),
            [_ASCIIMaze] = (ProcessASCIIMaze, "ASCII Maze", "Timwi"),
            [_ASquare] = (ProcessASquare, "A Square", "QuinnWuest"),
            [_AzureButton] = (ProcessAzureButton, "Azure Button, The", "Timwi"),
            [_Bakery] = (ProcessBakery, "Bakery", "TasThiluna"),
            [_BamboozledAgain] = (ProcessBamboozledAgain, "Bamboozled Again", "kavinkul"),
            [_BamboozlingButton] = (ProcessBamboozlingButton, "Bamboozling Button", "TasThiluna"),
            [_BarcodeCipher] = (ProcessBarcodeCipher, "Barcode Cipher", "Brawlboxgaming"),
            [_Bartending] = (ProcessBartending, "Bartending", "Timwi"),
            [_Beans] = (ProcessBeans, "Beans", "Anonymous"),
            [_BeanSprouts] = (ProcessBeanSprouts, "Bean Sprouts", "Anonymous"),
            [_BigBean] = (ProcessBigBean, "Big Bean", "Anonymous"),
            [_BigCircle] = (ProcessBigCircle, "Big Circle", "CaitSith2"),
            [_Binary] = (ProcessBinary, "Binary", "BigCrunch22"),
            [_BinaryLEDs] = (ProcessBinaryLEDs, "Binary LEDs", "Timwi"),
            [_BinaryShift] = (ProcessBinaryShift, "Binary Shift", "NickLatkovich"),
            [_Bitmaps] = (ProcessBitmaps, "Bitmaps", "Timwi"),
            [_BlackCipher] = (ProcessBlackCipher, "Black Cipher", "BigCrunch22"),
            [_BlindMaze] = (ProcessBlindMaze, "Blind Maze", "Timwi"),
            [_Blinkstop] = (ProcessBlinkstop, "Blinkstop", "Kuro"),
            [_Blockbusters] = (ProcessBlockbusters, "Blockbusters", "luisdiogo98"),
            [_BlueArrows] = (ProcessBlueArrows, "Blue Arrows", "kavinkul"),
            [_BlueButton] = (ProcessBlueButton, "Blue Button, The", "Timwi"),
            [_BlueCipher] = (ProcessBlueCipher, "Blue Cipher", "BigCrunch22"),
            [_BobBarks] = (ProcessBobBarks, "Bob Barks", "Kaito Sinclaire"),
            [_Boggle] = (ProcessBoggle, "Boggle", "luisdiogo98"),
            [_BombDiffusal] = (ProcessBombDiffusal, "Bomb Diffusal", "Kuro"),
            [_BooleanWires] = (ProcessBooleanWires, "Boolean Wires", "Kuro"),
            [_BoomtarTheGreat] = (ProcessBoomtarTheGreat, "Boomtar the Great", "Anonymous"),
            [_Boxing] = (ProcessBoxing, "Boxing", "Timwi"),
            [_Braille] = (ProcessBraille, "Braille", "Timwi"),
            [_BreakfastEgg] = (ProcessBreakfastEgg, "Breakfast Egg", "tandyCake"),
            [_BrokenButtons] = (ProcessBrokenButtons, "Broken Buttons", "Timwi"),
            [_BrokenGuitarChords] = (ProcessBrokenGuitarChords, "Broken Guitar Chords", "Kuro"),
            [_BrownCipher] = (ProcessBrownCipher, "Brown Cipher", "Marksam"),
            [_BrushStrokes] = (ProcessBrushStrokes, "Brush Strokes", "luisdiogo98"),
            [_Bulb] = (ProcessBulb, "Bulb, The", "Timwi"),
            [_BurgerAlarm] = (ProcessBurgerAlarm, "Burger Alarm", "Kuro"),
            [_BurglarAlarm] = (ProcessBurglarAlarm, "Burglar Alarm", "Timwi"),
            [_Button] = (ProcessButton, "Button, The", "Andrio Celos"),
            [_ButtonSequence] = (ProcessButtonSequence, "Button Sequence", "Timwi"),
            [_CaesarCycle] = (ProcessCaesarCycle, "Caesar Cycle", "TasThiluna"),
            [_Calendar] = (ProcessCalendar, "Calendar", "Timwi"),
            [_Cartinese] = (ProcessCartinese, "Cartinese", "Timwi"),
            [_Catchphrase] = (ProcessCatchphrase, "Catchphrase", "GoodHood"),
            [_ChallengeAndContact] = (ProcessChallengeAndContact, "Challenge & Contact", "luisdiogo98"),
            [_CharacterCodes] = (ProcessCharacterCodes, "Character Codes", "NickLatkovich"),
            [_CharacterSlots] = (ProcessCharacterSlots, "Character Slots", "Hawker"),
            [_CheapCheckout] = (ProcessCheapCheckout, "Cheap Checkout", "Timwi"),
            [_CheepCheckout] = (ProcessCheepCheckout, "Cheep Checkout", "BigCrunch22"),
            [_Chess] = (ProcessChess, "Chess", "Timwi"),
            [_ChineseCounting] = (ProcessChineseCounting, "Chinese Counting", "TasThiluna"),
            [_ChordQualities] = (ProcessChordQualities, "Chord Qualities", "Timwi"),
            [_Code] = (ProcessCode, "Code, The", "luisdiogo98"),
            [_Codenames] = (ProcessCodenames, "Codenames", "TasThiluna"),
            [_Coffeebucks] = (ProcessCoffeebucks, "Coffeebucks", "luisdiogo98"),
            [_Coinage] = (ProcessCoinage, "Coinage", "Emik"),
            [_ColorAddition] = (ProcessColorAddition, "Color Addition", "VFlyer"),
            [_ColorBraille] = (ProcessColorBraille, "Color Braille", "Timwi"),
            [_ColorDecoding] = (ProcessColorDecoding, "Color Decoding", "Timwi"),
            [_ColoredKeys] = (ProcessColoredKeys, "Colored Keys", "luisdiogo98"),
            [_ColoredSquares] = (ProcessColoredSquares, "Colored Squares", "Timwi"),
            [_ColoredSwitches] = (ProcessColoredSwitches, "Colored Switches", "Timwi"),
            [_ColorMorse] = (ProcessColorMorse, "Color Morse", "Timwi"),
            [_ColorsMaximization] = (ProcessColorsMaximization, "Colors Maximization", "NickLatkovich"),
            [_ColourFlash] = (ProcessColourFlash, "Colour Flash", "LotsOfS"),
            [_ConnectionCheck] = (ProcessConnectionCheck, "Connection Check", "Anonymous"),
            [_Coordinates] = (ProcessCoordinates, "Coordinates", "Timwi"),
            [_CoralCipher] = (ProcessCoralCipher, "Coral Cipher", "Timwi"),
            [_Corners] = (ProcessCorners, "Corners", "Timwi"),
            [_CornflowerCipher] = (ProcessCornflowerCipher, "Cornflower Cipher", "Timwi"),
            [_Cosmic] = (ProcessCosmic, "Cosmic", "BigCrunch22"),
            [_CrazyHamburger] = (ProcessCrazyHamburger, "Crazy Hamburger", "noting3548"),
            [_CrazyMaze] = (ProcessCrazyMaze, "Crazy Maze", "Timwi"),
            [_CreamCipher] = (ProcessCreamCipher, "Cream Cipher", "Timwi"),
            [_Creation] = (ProcessCreation, "Creation", "CaitSith2"),
            [_CrimsonCipher] = (ProcessCrimsonCipher, "Crimson Cipher", "Timwi"),
            [_Critters] = (ProcessCritters, "Critters", "Eltrick"),
            [_CruelBinary] = (ProcessCruelBinary, "Cruel Binary", "Kuro"),
            [_CruelKeypads] = (ProcessCruelKeypads, "Cruel Keypads", "Kuro"),
            [_CrypticCycle] = (ProcessCrypticCycle, "Cryptic Cycle", "TasThiluna"),
            [_CrypticKeypad] = (ProcessCrypticKeypad, "Cryptic Keypad", "Timwi"),
            [_Cube] = (ProcessCube, "Cube, The", "luisdiogo98"),
            [_CursedDoubleOh] = (ProcessCursedDoubleOh, "Cursed Double-Oh", "Kuro"),
            [_CustomerIdentification] = (ProcessCustomerIdentification, "Customer Identification", "Hawker"),
            [_CyanButton] = (ProcessCyanButton, "Cyan Button, The", "QuinnWuest"),
            [_DACHMaze] = (ProcessDACHMaze, "DACH Maze", "Timwi"),
            [_DeafAlley] = (ProcessDeafAlley, "Deaf Alley", "BigCrunch22"),
            [_DeckOfManyThings] = (ProcessDeckOfManyThings, "Deck of Many Things, The", "luisdiogo98"),
            [_DecoloredSquares] = (ProcessDecoloredSquares, "Decolored Squares", "luisdiogo98"),
            [_DecolourFlash] = (ProcessDecolourFlash, "Decolour Flash", "Timwi"),
            [_DevilishEggs] = (ProcessDevilishEggs, "Devilish Eggs", "Timwi"),
            [_Digisibility] = (ProcessDigisibility, "Digisibility", "tandyCake"),
            [_DigitString] = (ProcessDigitString, "Digit String", "GoodHood"),
            [_DimensionDisruption] = (ProcessDimensionDisruption, "Dimension Disruption", "Hawker"),
            [_DirectionalButton] = (ProcessDirectionalButton, "Directional Button", "Hawker"),
            [_DiscoloredSquares] = (ProcessDiscoloredSquares, "Discolored Squares", "luisdiogo98"),
            [_DivisibleNumbers] = (ProcessDivisibleNumbers, "Divisible Numbers", "shortc1rcuit"),
            [_DoubleArrows] = (ProcessDoubleArrows, "Double Arrows", "Anonymous"),
            [_DoubleColor] = (ProcessDoubleColor, "Double Color", "luisdiogo98"),
            [_DoubleDigits] = (ProcessDoubleDigits, "Double Digits", "QuinnWuest"),
            [_DoubleExpert] = (ProcessDoubleExpert, "Double Expert", "Kuro"),
            [_DoubleOh] = (ProcessDoubleOh, "Double-Oh", "Timwi"),
            [_DrDoctor] = (ProcessDrDoctor, "Dr. Doctor", "Timwi"),
            [_Dreamcipher] = (ProcessDreamcipher, "Dreamcipher", "BigCrunch22"),
            [_Duck] = (ProcessDuck, "Duck, The", "Kuro"),
            [_DumbWaiters] = (ProcessDumbWaiters, "Dumb Waiters", "BigCrunch22"),
            [_eeBgnillepS] = (ProcessEeBgnillepS, "eeB gnillepS", "BigCrunch22"),
            [_Eight] = (ProcessEight, "Eight", "NickLatkovich"),
            [_ElderFuthark] = (ProcessElderFuthark, "Elder Futhark", "Goofy"),
            [_EnaCipher] = (ProcessEnaCipher, "ƎNA Cipher", "KiloBites"),
            [_EncryptedDice] = (ProcessEncryptedDice, "Encrypted Dice", "Kuro"),
            [_EncryptedEquations] = (ProcessEncryptedEquations, "Encrypted Equations", "Timwi"),
            [_EncryptedHangman] = (ProcessEncryptedHangman, "Encrypted Hangman", "Timwi"),
            [_EncryptedMaze] = (ProcessEncryptedMaze, "Encrypted Maze", "Timwi"),
            [_EncryptedMorse] = (ProcessEncryptedMorse, "Encrypted Morse", "luisdiogo98"),
            [_EncryptionBingo] = (ProcessEncryptionBingo, "Encryption Bingo", "TasThiluna"),
            [_EnigmaCycle] = (ProcessEnigmaCycle, "Enigma Cycle", "Timwi"),
            [_EntryNumberFour] = (ProcessEntryNumberFour, "Entry Number Four", "GhostSalt"),
            [_EntryNumberOne] = (ProcessEntryNumberOne, "Entry Number One", "GhostSalt"),
            [_EquationsX] = (ProcessEquationsX, "Equations X", "kavinkul"),
            [_Etterna] = (ProcessEtterna, "Etterna", "Emik"),
            [_Exoplanets] = (ProcessExoplanets, "Exoplanets", "Brawlboxgaming"),
            [_FactoringMaze] = (ProcessFactoringMaze, "Factoring Maze", "Eltrick"),
            [_FactoryMaze] = (ProcessFactoryMaze, "Factory Maze", "luisdiogo98"),
            [_FastMath] = (ProcessFastMath, "Fast Math", "Timwi"),
            [_FaultyButtons] = (ProcessFaultyButtons, "Faulty Buttons", "Kuro"),
            [_FaultyRGBMaze] = (ProcessFaultyRGBMaze, "Faulty RGB Maze", "kavinkul"),
            [_FindTheDate] = (ProcessFindTheDate, "Find The Date", "Hawker"),
            [_FiveLetterWords] = (ProcessFiveLetterWords, "Five Letter Words", "Kuro"),
            [_FizzBuzz] = (ProcessFizzBuzz, "FizzBuzz", "Kuro"),
            [_Flags] = (ProcessFlags, "Flags", "Timwi"),
            [_FlashingArrows] = (ProcessFlashingArrows, "Flashing Arrows", "VFlyer"),
            [_FlashingLights] = (ProcessFlashingLights, "Flashing Lights", "luisdiogo98"),
            [_Flyswatting] = (ProcessFlyswatting, "Flyswatting", "tandyCake"),
            [_FollowMe] = (ProcessFollowMe, "Follow Me", "Kuro"),
            [_ForestCipher] = (ProcessForestCipher, "Forest Cipher", "Timwi"),
            [_ForgetAnyColor] = (ProcessForgetAnyColor, "Forget Any Color", "Kuro"),
            [_ForgetEverything] = (ProcessForgetEverything, "Forget Everything", "Kuro"),
            [_ForgetMe] = (ProcessForgetMe, "Forget Me", "tandyCake"),
            [_ForgetMeNot] = (ProcessForgetMeNot, "Forget Me Not", "Kuro"),
            [_ForgetsUltimateShowdown] = (ProcessForgetsUltimateShowdown, "Forget’s Ultimate Showdown", "Marksam"),
            [_ForgetTheColors] = (ProcessForgetTheColors, "Forget The Colors", "Kuro"),
            [_ForgetThis] = (ProcessForgetThis, "Forget This", "Kuro"),
            [_FreeParking] = (ProcessFreeParking, "Free Parking", "luisdiogo98"),
            [_Functions] = (ProcessFunctions, "Functions", "JerryEris"),
            [_FuseBox] = (ProcessFuseBox, "Fuse Box, The", "Anonymous"),
            [_GadgetronVendor] = (ProcessGadgetronVendor, "Gadgetron Vendor", "Kuro"),
            [_GameOfLifeCruel] = (ProcessGameOfLifeCruel, "Game of Life Cruel", "GhostSalt"),
            [_Gamepad] = (ProcessGamepad, "Gamepad, The", "Timwi"),
            [_GarnetThief] = (ProcessGarnetThief, "Garnet Thief, The", "Hawker"),
            [_Girlfriend] = (ProcessGirlfriend, "Girlfriend", "Hawker"),
            [_GlitchedButton] = (ProcessGlitchedButton, "Glitched Button, The", "Timwi"),
            [_GrayButton] = (ProcessGrayButton, "Gray Button, The", "Timwi"),
            [_GrayCipher] = (ProcessGrayCipher, "Gray Cipher", "BigCrunch22"),
            [_GreatVoid] = (ProcessGreatVoid, "Great Void, The", "Marksam"),
            [_GreenArrows] = (ProcessGreenArrows, "Green Arrows", "kavinkul"),
            [_GreenButton] = (ProcessGreenButton, "Green Button, The", "Timwi"),
            [_GreenCipher] = (ProcessGreenCipher, "Green Cipher", "BigCrunch22"),
            [_GridLock] = (ProcessGridLock, "Gridlock", "CaitSith2"),
            [_GroceryStore] = (ProcessGroceryStore, "Grocery Store", "BigCrunch22"),
            [_Gryphons] = (ProcessGryphons, "Gryphons", "JerryEris"),
            [_GuessWho] = (ProcessGuessWho, "Guess Who?", "BigCrunch22"),
            [_h] = (ProceessH, "h", "Hawker"),
            [_HereditaryBaseNotation] = (ProcessHereditaryBaseNotation, "Hereditary Base Notation", "kavinkul"),
            [_Hexabutton] = (ProcessHexabutton, "Hexabutton, The", "luisdiogo98"),
            [_Hexamaze] = (ProcessHexamaze, "Hexamaze", "Timwi"),
            [_HexOS] = (ProcessHexOS, "hexOS", "Emik"),
            [_HiddenColors] = (ProcessHiddenColors, "Hidden Colors", "TasThiluna"),
            [_HighScore] = (ProcessHighScore, "High Score, The", "Hawker"),
            [_HillCycle] = (ProcessHillCycle, "Hill Cycle", "TasThiluna"),
            [_Hinges] = (ProcessHinges, "Hinges", "Kuro"),
            [_Hogwarts] = (ProcessHogwarts, "Hogwarts", "Timwi"),
            [_HoldUps] = (ProcessHoldUps, "Hold Ups", "BigCrunch22"),
            [_Homophones] = (ProcessHomophones, "Homophones", "VFlyer"),
            [_HorribleMemory] = (ProcessHorribleMemory, "Horrible Memory", "luisdiogo98"),
            [_HumanResources] = (ProcessHumanResources, "Human Resources", "Timwi"),
            [_Hunting] = (ProcessHunting, "Hunting", "Timwi"),
            [_Hypercube] = (ProcessHypercube, "Hypercube, The", "luisdiogo98"),
            [_Hyperlink] = (ProcessHyperlink, "Hyperlink, The", "Espik"),
            [_IceCream] = (ProcessIceCream, "Ice Cream", "CaitSith2"),
            [_IdentificationCrisis] = (ProcessIdentificationCrisis, "Identification Crisis", "TasThiluna"),
            [_IdentityParade] = (ProcessIdentityParade, "Identity Parade", "Timwi"),
            [_Impostor] = (ProcessImpostor, "Impostor, The", "Kuro"),
            [_IndigoCipher] = (ProcessIndigoCipher, "Indigo Cipher", "BigCrunch22"),
            [_InfiniteLoop] = (ProcessInfiniteLoop, "Infinite Loop", "Eltrick"),
            [_Ingredients] = (ProcessIngredients, "Ingredients", "Timwi"),
            [_InnerConnections] = (ProcessInnerConnections, "Inner Connections", "Brawlboxgaming"),
            [_Interpunct] = (ProcessInterpunct, "Interpunct", "Eltrick"),
            [_IPA] = (ProcessIPA, "IPA", "Timwi"),
            [_iPhone] = (ProcessiPhone, "iPhone, The", "luisdiogo98"),
            [_Jenga] = (ProcessJenga, "Jenga", "tandyCake"),
            [_JewelVault] = (ProcessJewelVault, "Jewel Vault, The", "luisdiogo98"),
            [_JumbleCycle] = (ProcessJumbleCycle, "Jumble Cycle", "TasThiluna"),
            [_JuxtacoloredSquares] = (ProcessJuxtacoloredSquares, "Juxtacolored Squares", "Kuro"),
            [_Kanji] = (ProcessKanji, "Kanji", "Kuro"),
            [_KanyeEncounter] = (ProcessKanyeEncounter, "Kanye Encounter, The", "tandyCake"),
            [_KeypadMagnified] = (ProcessKeypadMagnified, "Keypad Magnified", "tandyCake"),
            [_KnowYourWay] = (ProcessKnowYourWay, "Know Your Way", "Kuro"),
            [_Kudosudoku] = (ProcessKudosudoku, "Kudosudoku", "Timwi"),
            [_Labyrinth] = (ProcessLabyrinth, "Labyrinth, The", "Anonymous"),
            [_LadderLottery] = (ProcessLadderLottery, "Ladder Lottery", "Hawker"),
            [_Ladders] = (ProcessLadders, "Ladders", "tandyCake"),
            [_Lasers] = (ProcessLasers, "Lasers", "luisdiogo98"),
            [_LEDEncryption] = (ProcessLEDEncryption, "LED Encryption", "CaitSith2"),
            [_LEDMath] = (ProcessLEDMath, "LED Math", "TasThiluna"),
            [_LEDs] = (ProcessLEDs, "LEDs", "tandyCake"),
            [_LEGOs] = (ProcessLEGOs, "LEGOs", "luisdiogo98"),
            [_LetterMath] = (ProcessLetterMath, "Letter Math", "QuinnWuest"),
            [_LightBulbs] = (ProcessLightBulbs, "Light Bulbs", "Kuro"),
            [_Linq] = (ProcessLinq, "Linq", "Emik"),
            [_LionsShare] = (ProcessLionsShare, "Lion’s Share", "Timwi"),
            [_Listening] = (ProcessListening, "Listening", "Timwi"),
            [_LogicalButtons] = (ProcessLogicalButtons, "Logical Buttons", "Timwi"),
            [_LogicGates] = (ProcessLogicGates, "Logic Gates", "Timwi"),
            [_LombaxCubes] = (ProcessLombaxCubes, "Lombax Cubes", "Marksam"),
            [_LondonUnderground] = (ProcessLondonUnderground, "London Underground, The", "Timwi"),
            [_LongWords] = (ProcessLongWords, "Long Words", "GoodHood"),
            [_MadMemory] = (ProcessMadMemory, "Mad Memory", "Kuro"),
            [_Mafia] = (ProcessMafia, "Mafia", "Timwi"),
            [_MagentaCipher] = (ProcessMagentaCipher, "Magenta Cipher", "Timwi"),
            [_Mahjong] = (ProcessMahjong, "Mahjong", "River"),
            [_MandMs] = (ProcessMandMs, "M&Ms", "TasThiluna"),
            [_MandNs] = (ProcessMandNs, "M&Ns", "TasThiluna"),
            [_MaritimeFlags] = (ProcessMaritimeFlags, "Maritime Flags", "Timwi"),
            [_MaroonCipher] = (ProcessMaroonCipher, "Maroon Cipher", "Timwi"),
            [_Mashematics] = (ProcessMashematics, "Mashematics", "Marksam"),
            [_MathEm] = (ProcessMathEm, "Math ’em", "tandyCake"),
            [_Matrix] = (ProcessMatrix, "Matrix, The", "BigCrunch22"),
            [_Maze] = (ProcessMaze, "Maze", "Andrio Celos"),
            [_Maze3] = (ProcessMaze3, "Maze³", "luisdiogo98"),
            [_MazeIdentification] = (ProcessMazeIdentification, "Maze Identification", "GhostSalt"),
            [_Mazematics] = (ProcessMazematics, "Mazematics", "luisdiogo98"),
            [_MazeScrambler] = (ProcessMazeScrambler, "Maze Scrambler", "luisdiogo98"),
            [_Mazeseeker] = (ProcessMazeseeker, "Mazeseeker", "GhostSalt"),
            [_MegaMan2] = (ProcessMegaMan2, "Mega Man 2", "Goofy"),
            [_MelodySequencer] = (ProcessMelodySequencer, "Melody Sequencer", "Goofy"),
            [_MemorableButtons] = (ProcessMemorableButtons, "Memorable Buttons", "Timwi"),
            [_Memory] = (ProcessMemory, "Memory", "Andrio Celos"),
            [_Metamorse] = (ProcessMetamorse, "Metamorse", "tandyCake"),
            [_Metapuzzle] = (ProcessMetapuzzle, "Metapuzzle", "GoodHood"),
            [_Microcontroller] = (ProcessMicrocontroller, "Microcontroller", "Timwi"),
            [_Minesweeper] = (ProcessMinesweeper, "Minesweeper", "CaitSith2"),
            [_Mirror] = (ProcessMirror, "Mirror", "Timwi"),
            [_MisterSoftee] = (ProcessMisterSoftee, "Mister Softee", "TasThiluna"),
            [_ModernCipher] = (ProcessModernCipher, "Modern Cipher", "luisdiogo98"),
            [_ModuleListening] = (ProcessModuleListening, "Module Listening", "TasThiluna"),
            [_ModuleMaze] = (ProcessModuleMaze, "Module Maze", "River"),
            [_MonsplodeFight] = (ProcessMonsplodeFight, "Monsplode, Fight!", "Timwi"),
            [_MonsplodeTradingCards] = (ProcessMonsplodeTradingCards, "Monsplode Trading Cards", "Timwi"),
            [_Moon] = (ProcessMoon, "Moon, The", "Timwi"),
            [_MoreCode] = (ProcessMoreCode, "More Code", "TasThiluna"),
            [_MorseAMaze] = (ProcessMorseAMaze, "Morse-A-Maze", "CaitSith2"),
            [_MorseButtons] = (ProcessMorseButtons, "Morse Buttons", "luisdiogo98"),
            [_Morsematics] = (ProcessMorsematics, "Morsematics", "Timwi"),
            [_MorseWar] = (ProcessMorseWar, "Morse War", "Timwi"),
            [_MouseInTheMaze] = (ProcessMouseInTheMaze, "Mouse in the Maze", "Timwi"),
            [_MSeq] = (ProcessMSeq, "M-Seq", "tandyCake"),
            [_MulticoloredSwitches] = (ProcessMulticoloredSwitches, "Multicolored Switches", "Timwi"),
            [_Murder] = (ProcessMurder, "Murder", "Timwi"),
            [_MysteryModule] = (ProcessMysteryModule, "Mystery Module", "Timwi"),
            [_MysticSquare] = (ProcessMysticSquare, "Mystic Square", "CaitSith2"),
            [_NameCodes] = (ProcessNameCodes, "Name Codes", "tandyCake"),
            [_NandMs] = (ProcessNandMs, "N&Ms", "TasThiluna"),
            [_Navinums] = (ProcessNavinums, "Navinums", "Timwi"),
            [_NavyButton] = (ProcessNavyButton, "Navy Button, The", "Timwi"),
            [_Necronomicon] = (ProcessNecronomicon, "Necronomicon, The", "luisdiogo98"),
            [_Negativity] = (ProcessNegativity, "Negativity", "VFlyer"),
            [_Neutralization] = (ProcessNeutralization, "Neutralization", "Timwi"),
            [_NonverbalSimon] = (ProcessNonverbalSimon, "❖", "Anonymous"),
            [_NotColoredSquares] = (ProcessNotColoredSquares, "Not Colored Squares", "Kuro"),
            [_NotColoredSwitches] = (ProcessNotColoredSwitches, "Not Colored Switches", "QuinnWuest"),
            [_NotConnectionCheck] = (ProcessNotConnectionCheck, "Not Connection Check", "QuinnWuest"),
            [_NotCoordinates] = (ProcessNotCoordinates, "Not Coordinates", "QuinnWuest"),
            [_NotKeypad] = (ProcessNotKeypad, "Not Keypad", "Andrio Celos"),
            [_NotMaze] = (ProcessNotMaze, "Not Maze", "Andrio Celos"),
            [_NotMorseCode] = (ProcessNotMorseCode, "Not Morse Code", "Andrio Celos"),
            [_NotMorsematics] = (ProcessNotMorsematics, "Not Morsematics", "QuinnWuest"),
            [_NotMurder] = (ProcessNotMurder, "Not Murder", "QuinnWuest"),
            [_NotNumberPad] = (ProcessNotNumberPad, "Not Number Pad", "QuinnWuest"),
            [_NotPerspectivePegs] = (ProcessNotPerspectivePegs, "Not Perspective Pegs", "QuinnWuest"),
            [_NotPianoKeys] = (ProcessNotPianoKeys, "Not Piano Keys", "tandyCake"),
            [_NotSimaze] = (ProcessNotSimaze, "Not Simaze", "Andrio Celos"),
            [_NotTextField] = (ProcessNotTextField, "Not Text Field", "tandyCake"),
            [_NotTheBulb] = (ProcessNotTheBulb, "Not The Bulb", "QuinnWuest"),
            [_NotTheButton] = (ProcessNotTheButton, "Not the Button", "Andrio Celos"),
            [_NotTheScrew] = (ProcessNotTheScrew, "Not The Screw", "GhostSalt"),
            [_NotWhosOnFirst] = (ProcessNotWhosOnFirst, "Not Who’s on First", "Andrio Celos"),
            [_NotWordSearch] = (ProcessNotWordSearch, "Not Word Search", "tandyCake"),
            [_NotX01] = (ProcessNotX01, "Not X01", "QuinnWuest"),
            [_NotXRay] = (ProcessNotXRay, "Not X-Ray", "Timwi"),
            [_NumberedButtons] = (ProcessNumberedButtons, "Numbered Buttons", "Eltrick"),
            [_Numbers] = (ProcessNumbers, "Numbers", "BigCrunch22"),
            [_Numpath] = (ProcessNumpath, "Numpath", "tandyCake"),
            [_ObjectShows] = (ProcessObjectShows, "Object Shows", "Timwi"),
            [_Octadecayotton] = (ProcessOctadecayotton, "Octadecayotton, The", "Emik"),
            [_OddOneOut] = (ProcessOddOneOut, "Odd One Out", "luisdiogo98"),
            [_OldAI] = (ProcessOldAI, "Old AI", "noting3548"),
            [_OldFogey] = (ProcessOldFogey, "Old Fogey", "Kuro"),
            [_OnlyConnect] = (ProcessOnlyConnect, "Only Connect", "Timwi"),
            [_OrangeArrows] = (ProcessOrangeArrows, "Orange Arrows", "kavinkul"),
            [_OrangeCipher] = (ProcessOrangeCipher, "Orange Cipher", "BigCrunch22"),
            [_OrderedKeys] = (ProcessOrderedKeys, "Ordered Keys", "TasThiluna"),
            [_OrderPicking] = (ProcessOrderPicking, "Order Picking", "Brawlboxgaming"),
            [_OrientationCube] = (ProcessOrientationCube, "Orientation Cube", "Timwi"),
            [_OrientationHypercube] = (ProcessOrientationHypercube, "Orientation Hypercube", "Kuro"),
            [_Palindromes] = (ProcessPalindromes, "Palindromes", "Emik"),
            [_Parity] = (ProcessParity, "Parity", "QuinnWuest"),
            [_PartialDerivatives] = (ProcessPartialDerivatives, "Partial Derivatives", "Timwi"),
            [_PassportControl] = (ProcessPassportControl, "Passport Control", "luisdiogo98"),
            [_PasswordDestroyer] = (ProcessPasswordDestroyer, "Password Destroyer", "Eltrick"),
            [_PatternCube] = (ProcessPatternCube, "Pattern Cube", "Timwi"),
            [_PeriodicWords] = (ProcessPeriodicWords, "Periodic Words", "Kuro"),
            [_PerspectivePegs] = (ProcessPerspectivePegs, "Perspective Pegs", "Andrio Celos"),
            [_Phosphorescence] = (ProcessPhosphorescence, "Phosphorescence", "Emik"),
            [_Pictionary] = (ProcessPictionary, "Pictionary", "Kuro"),
            [_Pie] = (ProcessPie, "Pie", "luisdiogo98"),
            [_PieFlash] = (ProcessPieFlash, "Pie Flash", "VFlyer"),
            [_PigpenCycle] = (ProcessPigpenCycle, "Pigpen Cycle", "TasThiluna"),
            [_PinkButton] = (ProcessPinkButton, "Pink Button, The", "Timwi"),
            [_PixelCipher] = (ProcessPixelCipher, "Pixel Cipher", "Eltrick"),
            [_PlaceholderTalk] = (ProcessPlaceholderTalk, "Placeholder Talk", "Emik"),
            [_PlacementRoulette] = (ProcessPlacementRoulette, "Placement Roulette", "Brawlboxgaming"),
            [_Planets] = (ProcessPlanets, "Planets", "KingSlendy"),
            [_PlayfairCycle] = (ProcessPlayfairCycle, "Playfair Cycle", "TasThiluna"),
            [_Poetry] = (ProcessPoetry, "Poetry", "Timwi"),
            [_PolyhedralMaze] = (ProcessPolyhedralMaze, "Polyhedral Maze", "Timwi"),
            [_PrimeEncryption] = (ProcessPrimeEncryption, "Prime Encryption", "VFlyer"),
            [_Probing] = (ProcessProbing, "Probing", "Timwi"),
            [_ProceduralMaze] = (ProcessProceduralMaze, "Procedural Maze", "Kuro"),
            [_PunctuationMarks] = (ProcessPunctuationMarks, "...?", "Kuro"),
            [_PurpleArrows] = (ProcessPurpleArrows, "Purple Arrows", "kavinkul"),
            [_PurpleButton] = (ProcessPurpleButton, "Purple Button, The", "Timwi"),
            [_PuzzleIdentification] = (ProcessPuzzleIdentification, "Puzzle Identification", "GhostSalt"),
            [_Quaver] = (ProcessQuaver, "Quaver", "Emik"),
            [_QuestionMark] = (ProcessQuestionMark, "Question Mark", "Kuro"),
            [_QuickArithmetic] = (ProcessQuickArithmetic, "Quick Arithmetic", "VFlyer"),
            [_Quintuples] = (ProcessQuintuples, "Quintuples", "Timwi"),
            [_Qwirkle] = (ProcessQwirkle, "Qwirkle", "GoodHood"),
            [_RaidingTemples] = (ProcessRaidingTemples, "Raiding Temples", "GoodHood"),
            [_RailwayCargoLoading] = (ProcessRailwayCargoLoading, "Railway Cargo Loading", "LotsOfS"),
            [_RainbowArrows] = (ProcessRainbowArrows, "Rainbow Arrows", "TasThiluna"),
            [_RecoloredSwitches] = (ProcessRecoloredSwitches, "Recolored Switches", "Timwi"),
            [_RecursivePassword] = (ProcessRecursivePassword, "Recursive Password", "Kuro"),
            [_RedArrows] = (ProcessRedArrows, "Red Arrows", "kavinkul"),
            [_RedCipher] = (ProcessRedCipher, "Red Cipher", "BigCrunch22"),
            [_RedHerring] = (ProcessRedHerring, "Red Herring", "tandyCake"),
            [_ReformedRoleReversal] = (ProcessReformedRoleReversal, "Reformed Role Reversal", "Emik"),
            [_RegularCrazyTalk] = (ProcessRegularCrazyTalk, "Regular Crazy Talk", "Espik"),
            [_Retirement] = (ProcessRetirement, "Retirement", "luisdiogo98"),
            [_ReverseMorse] = (ProcessReverseMorse, "Reverse Morse", "luisdiogo98"),
            [_ReversePolishNotation] = (ProcessReversePolishNotation, "Reverse Polish Notation", "shortc1rcuit"),
            [_RGBMaze] = (ProcessRGBMaze, "RGB Maze", "kavinkul"),
            [_Rhythms] = (ProcessRhythms, "Rhythms", "Timwi"),
            [_RoboScanner] = (ProcessRoboScanner, "Robo-Scanner", "QuinnWuest"),
            [_Roger] = (ProcessRoger, "Roger", "BigCrunch22"),
            [_RoleReversal] = (ProcessRoleReversal, "Role Reversal", "Emik"),
            [_Rule] = (ProcessRule, "Rule, The", "TasThiluna"),
            [_RuleOfThree] = (ProcessRuleOfThree, "Rule of Three", "QuinnWuest"),
            [_SafetySquare] = (ProcessSafetySquare, "Safety Square", "Kuro"),
            [_Samsung] = (ProcessSamsung, "Samsung, The", "TasThiluna"),
            [_ScavengerHunt] = (ProcessScavengerHunt, "Scavenger Hunt", "Timwi"),
            [_SchlagDenBomb] = (ProcessSchlagDenBomb, "Schlag den Bomb", "JerryEris"),
            [_ScramboozledEggain] = (ProcessScramboozledEggain, "Scramboozled Eggain", "QuinnWuest"),
            [_Scripting] = (ProcessScripting, "Scripting", "Kuro"),
            [_ScrutinySquares] = (ProcessScrutinySquares, "Scrutiny Squares", "Hawker"),
            [_SeaShells] = (ProcessSeaShells, "Sea Shells", "Timwi"),
            [_Semamorse] = (ProcessSemamorse, "Semamorse", "Timwi"),
            [_Sequencyclopedia] = (ProcessSequencyclopedia, "Sequencyclopedia, The", "BigCrunch22"),
            [_SetTheory] = (ProcessSetTheory, "S.E.T. Theory", "Timwi"),
            [_ShapesAndBombs] = (ProcessShapesAndBombs, "Shapes And Bombs", "KingSlendy"),
            [_ShapeShift] = (ProcessShapeShift, "Shape Shift", "Timwi"),
            [_ShiftedMaze] = (ProcessShiftedMaze, "Shifted Maze", "Timwi"),
            [_ShiftingMaze] = (ProcessShiftingMaze, "Shifting Maze", "BigCrunch22"),
            [_ShogiIdentification] = (ProcessShogiIdentification, "Shogi Identification", "tandyCake"),
            [_SignLanguage] = (ProcessSignLanguage, "Sign Language", "Hawker"),
            [_SillySlots] = (ProcessSillySlots, "Silly Slots", "Timwi"),
            [_SiloAuthorization] = (ProcessSiloAuthorization, "Silo Authorization", "Timwi"),
            [_SimonSaid] = (ProcessSimonSaid, "Simon Said", "QuinnWuest"),
            [_SimonSamples] = (ProcessSimonSamples, "Simon Samples", "Timwi"),
            [_SimonSays] = (ProcessSimonSays, "Simon Says", "Andrio Celos"),
            [_SimonScrambles] = (ProcessSimonScrambles, "Simon Scrambles", "luisdiogo98"),
            [_SimonScreams] = (ProcessSimonScreams, "Simon Screams", "Timwi"),
            [_SimonSelects] = (ProcessSimonSelects, "Simon Selects", "tachatat18"),
            [_SimonSends] = (ProcessSimonSends, "Simon Sends", "EternityShack"),
            [_SimonShapes] = (ProcessSimonShapes, "Simon Shapes", "tandyCake"),
            [_SimonShouts] = (ProcessSimonShouts, "Simon Shouts", "Timwi"),
            [_SimonShrieks] = (ProcessSimonShrieks, "Simon Shrieks", "Timwi"),
            [_SimonSimons] = (ProcessSimonSimons, "Simon Simons", "kavinkul"),
            [_SimonSings] = (ProcessSimonSings, "Simon Sings", "Timwi"),
            [_SimonSmothers] = (ProcessSimonSmothers, "Simon Smothers", "Kuro"),
            [_SimonSounds] = (ProcessSimonSounds, "Simon Sounds", "Timwi"),
            [_SimonSpeaks] = (ProcessSimonSpeaks, "Simon Speaks", "Timwi"),
            [_SimonsStar] = (ProcessSimonsStar, "Simon’s Star", "TasThiluna"),
            [_SimonStacks] = (ProcessSimonStacks, "Simon Stacks", "Kuro"),
            [_SimonStages] = (ProcessSimonStages, "Simon Stages", "Espik"),
            [_SimonStates] = (ProcessSimonStates, "Simon States", "Timwi"),
            [_SimonStops] = (ProcessSimonStops, "Simon Stops", "JerryEris"),
            [_SimonStores] = (ProcessSimonStores, "Simon Stores", "kavinkul"),
            [_SimonSubdivides] = (ProcessSimonSubdivides, "Simon Subdivides", "Anonymous"),
            [_SimonSupports] = (ProcessSimonSupports, "Simon Supports", "tandyCake"),
            [_SkewedSlots] = (ProcessSkewedSlots, "Skewed Slots", "Timwi"),
            [_Skyrim] = (ProcessSkyrim, "Skyrim", "Timwi"),
            [_SlowMath] = (ProcessSlowMath, "Slow Math", "QuinnWuest"),
            [_SmallCircle] = (ProcessSmallCircle, "Small Circle", "TasThiluna"),
            [_Snooker] = (ProcessSnooker, "Snooker", "TasThiluna"),
            [_Snowflakes] = (ProcessSnowflakes, "Snowflakes", "Kuro"),
            [_SonicKnuckles] = (ProcessSonicKnuckles, "Sonic & Knuckles", "Hawker"),
            [_SonicTheHedgehog] = (ProcessSonicTheHedgehog, "Sonic the Hedgehog", "Timwi"),
            [_Sorting] = (ProcessSorting, "Sorting", "Emik"),
            [_Souvenir] = (ProcessSouvenir, "Souvenir", "CaitSith2"),
            [_SpaceTraders] = (ProcessSpaceTraders, "Space Traders", "NickLatkovich"),
            [_SpellingBee] = (ProcessSpellingBee, "Spelling Bee", "BigCrunch22"),
            [_Sphere] = (ProcessSphere, "Sphere, The", "luisdiogo98"),
            [_SplittingTheLoot] = (ProcessSplittingTheLoot, "Splitting The Loot", "luisdiogo98"),
            [_SpongebobBirthdayIdentification] = (ProcessSpongebobBirthdayIdentification, "Spongebob Birthday Identification", "Hawker"),
            [_Stability] = (ProcessStability, "Stability", "NickLatkovich"),
            [_StackedSequences] = (ProcessStackedSequences, "Stacked Sequences", "GhostSalt"),
            [_Stars] = (ProcessStars, "Stars", "BigCrunch22"),
            [_StateOfAggregation] = (ProcessStateOfAggregation, "State of Aggregation", "BigCrunch22"),
            [_Stellar] = (ProcessStellar, "Stellar", "Timwi"),
            [_StupidSlots] = (ProcessStupidSlots, "Stupid Slots", "tandyCake"),
            [_SubscribeToPewdiepie] = (ProcessSubscribeToPewdiepie, "Subscribe to Pewdiepie", "BigCrunch22"),
            [_SugarSkulls] = (ProcessSugarSkulls, "Sugar Skulls", "BigCrunch22"),
            [_Superparsing] = (ProcessSuperparsing, "Superparsing", "tandyCake"),
            [_Switch] = (ProcessSwitch, "Switch, The", "Timwi"),
            [_Switches] = (ProcessSwitches, "Switches", "Timwi"),
            [_SwitchingMaze] = (ProcessSwitchingMaze, "Switching Maze", "BigCrunch22"),
            [_SymbolCycle] = (ProcessSymbolCycle, "Symbol Cycle", "CaitSith2"),
            [_SymbolicCoordinates] = (ProcessSymbolicCoordinates, "Symbolic Coordinates", "CaitSith2"),
            [_SymbolicTasha] = (ProcessSymbolicTasha, "Symbolic Tasha", "Timwi"),
            [_Sync_125_3] = (ProcessSync_125_3, "SYNC-125 [3]", "Timwi"),
            [_Synonyms] = (ProcessSynonyms, "Synonyms", "Timwi"),
            [_Sysadmin] = (ProcessSysadmin, "Sysadmin", "NickLatkovich"),
            [_TapCode] = (ProcessTapCode, "Tap Code", "Timwi"),
            [_TashaSqueals] = (ProcessTashaSqueals, "Tasha Squeals", "luisdiogo98"),
            [_TasqueManaging] = (ProcessTasqueManaging, "Tasque Managing", "tandyCake"),
            [_TeaSet] = (ProcessTeaSet, "Tea Set, The", "Kuro"),
            [_TechnicalKeypad] = (ProcessTechnicalKeypad, "Technical Keypad", "Kuro"),
            [_TenButtonColorCode] = (ProcessTenButtonColorCode, "Ten-Button Color Code", "Timwi"),
            [_Tenpins] = (ProcessTenpins, "Tenpins", "TasThiluna"),
            [_Tetriamonds] = (ProcessTetriamonds, "Tetriamonds", "Kuro"),
            [_TextField] = (ProcessTextField, "Text Field", "CaitSith2"),
            [_ThinkingWires] = (ProcessThinkingWires, "Thinking Wires", "kavinkul"),
            [_ThirdBase] = (ProcessThirdBase, "Third Base", "CaitSith2"),
            [_TicTacToe] = (ProcessTicTacToe, "Tic Tac Toe", "Timwi"),
            [_Timezone] = (ProcessTimezone, "Timezone", "Timwi"),
            [_TipToe] = (ProcessTipToe, "Tip Toe", "Kuro"),
            [_TopsyTurvy] = (ProcessTopsyTurvy, "Topsy Turvy", "BigCrunch22"),
            [_TouchTransmission] = (ProcessTouchTransmission, "Touch Transmission", "tandyCake"),
            [_Trajectory] = (ProcessTrajectory, "Trajectory", "tandyCake"),
            [_TransmittedMorse] = (ProcessTransmittedMorse, "Transmitted Morse", "kavinkul"),
            [_Triamonds] = (ProcessTriamonds, "Triamonds", "Kuro"),
            [_TripleTerm] = (ProcessTripleTerm, "Triple Term", "QuinnWuest"),
            [_TurtleRobot] = (ProcessTurtleRobot, "Turtle Robot", "CaitSith2"),
            [_TwoBits] = (ProcessTwoBits, "Two Bits", "Timwi"),
            [_UltimateCipher] = (ProcessUltimateCipher, "Ultimate Cipher", "BigCrunch22"),
            [_UltimateCycle] = (ProcessUltimateCycle, "Ultimate Cycle", "TasThiluna"),
            [_Ultracube] = (ProcessUltracube, "Ultracube, The", "luisdiogo98"),
            [_UltraStores] = (ProcessUltraStores, "UltraStores", "Marksam"),
            [_UncoloredSquares] = (ProcessUncoloredSquares, "Uncolored Squares", "Timwi"),
            [_UncoloredSwitches] = (ProcessUncoloredSwitches, "Uncolored Switches", "Timwi"),
            [_UnfairCipher] = (ProcessUnfairCipher, "Unfair Cipher", "luisdiogo98"),
            [_UnfairsRevenge] = (ProcessUnfairsRevenge, "Unfair’s Revenge", "VFlyer"),
            [_Unicode] = (ProcessUnicode, "Unicode", "Marksam"),
            [_UNO] = (ProcessUNO, "UNO!", "Hawker"),
            [_UnownCipher] = (ProcessUnownCipher, "Unown Cipher", "kavinkul"),
            [_USACycle] = (ProcessUSACycle, "USA Cycle", "tandyCake"),
            [_USAMaze] = (ProcessUSAMaze, "USA Maze", "luisdiogo98"),
            [_V] = (ProcessV, "V", "BigCrunch22"),
            [_VaricoloredSquares] = (ProcessVaricoloredSquares, "Varicolored Squares", "luisdiogo98"),
            [_VaricolourFlash] = (ProcessVaricolourFlash, "Varicolour Flash", "QuinnWuest"),
            [_Vcrcs] = (ProcessVcrcs, "Vcrcs", "Timwi"),
            [_Vectors] = (ProcessVectors, "Vectors", "kavinkul"),
            [_Vexillology] = (ProcessVexillology, "Vexillology", "luisdiogo98"),
            [_VioletCipher] = (ProcessVioletCipher, "Violet Cipher", "BigCrunch22"),
            [_VisualImpairment] = (ProcessVisualImpairment, "Visual Impairment", "Timwi"),
            [_WarningSigns] = (ProcessWarningSigns, "Warning Signs", "Kuro"),
            [_Wavetapping] = (ProcessWavetapping, "Wavetapping", "KingSlendy"),
            [_WeakestLink] = (ProcessWeakestLink, "Weakest Link, The", "Hawker"),
            [_WhatsOnSecond] = (ProcessWhatsOnSecond, "What’s on Second", "BigCrunch22"),
            [_WhiteCipher] = (ProcessWhiteCipher, "White Cipher", "BigCrunch22"),
            [_WhoOF] = (ProcessWhoOF, "WhoOF", "VFlyer"),
            [_WhosOnFirst] = (ProcessWhosOnFirst, "Who’s on First", "Andrio Celos"),
            [_WhosOnMorse] = (ProcessWhosOnMorse, "Who’s on Morse", "VFlyer"),
            [_Wire] = (ProcessWire, "Wire, The", "Timwi"),
            [_WireOrdering] = (ProcessWireOrdering, "Wire Ordering", "Andrio Celos"),
            [_WireSequence] = (ProcessWireSequence, "Wire Sequence", "Andrio Celos"),
            [_WolfGoatAndCabbage] = (ProcessWolfGoatAndCabbage, "Wolf, Goat, and Cabbage", "Marksam"),
            [_WorkingTitle] = (ProcessWorkingTitle, "Working Title", "BigCrunch22"),
            [_Xenocryst] = (ProcessXenocryst, "Xenocryst, The", "GhostSalt"),
            [_XmORseCode] = (ProcessXmORseCode, "XmORse Code", "shortc1rcuit"),
            [_XobekuJehT] = (ProcessXobekuJehT, "xobekuJ ehT", "QuinnWuest"),
            [_Yahtzee] = (ProcessYahtzee, "Yahtzee", "Timwi"),
            [_YellowArrows] = (ProcessYellowArrows, "Yellow Arrows", "kavinkul"),
            [_YellowButton] = (ProcessYellowButton, "Yellow Button, The", "Timwi"),
            [_YellowCipher] = (ProcessYellowCipher, "Yellow Cipher", "BigCrunch22"),
            [_ZeroZero] = (ProcessZeroZero, "Zero, Zero", "Timwi"),
            [_Zoni] = (ProcessZoni, "Zoni", "luisdiogo98"),
        };
    }

    /* Generalized handlers for modules that are extremely similar */

    // Used by Affine Cycle, Caesar Cycle, Pigpen Cycle and Playfair Cycle
    private IEnumerable<object> processSpeakingEvilCycle1(KMBombModule module, string componentName, Question question, string moduleId)
    {
        var comp = GetComponent(module, componentName);
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(moduleId);

        var messages = GetArrayField<string>(comp, "message").Get();
        var responses = GetArrayField<string>(comp, "response").Get();
        var index = GetIntField(comp, "r").Get(ix =>
            ix < 0 ? "negative" :
            ix >= messages.Length ? $"greater than ‘message’ length ({messages.Length})" :
            ix >= responses.Length ? $"greater than ‘response’ length ({responses.Length})" : null);

        var message = Regex.Replace(messages[index], @"(?<!^).", m => m.Value.ToLowerInvariant());
        var response = Regex.Replace(responses[index], @"(?<!^).", m => m.Value.ToLowerInvariant());
        addQuestions(module,
          makeQuestion(question, moduleId, formatArgs: new[] { "message" }, correctAnswers: new[] { message }, preferredWrongAnswers: new[] { response }),
          makeQuestion(question, moduleId, formatArgs: new[] { "response" }, correctAnswers: new[] { response }, preferredWrongAnswers: new[] { message }));
    }

    // Used by Cryptic Cycle, Hill Cycle, Jumble Cycle and Ultimate Cycle
    private IEnumerable<object> processSpeakingEvilCycle2(KMBombModule module, string componentName, Question question, string moduleId)
    {
        var comp = GetComponent(module, componentName);
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(moduleId);

        var words = GetArrayField<string[]>(comp, "message").Get(expectedLength: 2);
        var messages = words[0];
        var responses = words[1];
        var index = GetIntField(comp, "r").Get(ix =>
            ix < 0 ? "‘r’ is negative." :
            ix >= messages.Length ? $"‘r’ is greater than ‘message’ length ({messages.Length})." :
            ix >= responses.Length ? $"‘r’ is greater than ‘response’ length ({responses.Length})." : null);

        var message = Regex.Replace(messages[index], @"(?<!^).", m => m.Value.ToLowerInvariant());
        var response = Regex.Replace(responses[index], @"(?<!^).", m => m.Value.ToLowerInvariant());
        addQuestions(module,
          makeQuestion(question, moduleId, formatArgs: new[] { "message" }, correctAnswers: new[] { message }, preferredWrongAnswers: new[] { response }),
          makeQuestion(question, moduleId, formatArgs: new[] { "response" }, correctAnswers: new[] { response }, preferredWrongAnswers: new[] { message }));
    }

    // Used by the World Mazes modules (currently: USA Maze, DACH Maze)
    private IEnumerable<object> processWorldMaze(KMBombModule module, string script, string moduleCode, Question question)
    {
        var comp = GetComponent(module, script);
        var fldOrigin = GetField<string>(comp, "_originState");
        var fldActive = GetField<bool>(comp, "_isActive");
        var mthGetStates = GetMethod<List<string>>(comp, "GetAllStates", 0);
        var mthGetName = GetMethod<string>(comp, "GetStateFullName", 1);

        // wait for activation
        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        // then wait for the module to get solved
        while (fldActive.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(moduleCode);

        var stateCodes = mthGetStates.Invoke() ?? throw new AbandonModuleException("GetAllStates() returned null.");
        if (stateCodes.Count == 0)
            throw new AbandonModuleException("GetAllStates() returned an empty list.");

        var states = stateCodes.Select(code => mthGetName.Invoke(code)).ToArray();
        var origin = mthGetName.Invoke(fldOrigin.Get());
        if (!states.Contains(origin))
            throw new AbandonModuleException($"‘_originState’ was not contained in the list of all states (“{origin}” not in: [{states.JoinString(", ")}]).");

        addQuestions(module, makeQuestion(question, moduleCode, correctAnswers: new[] { origin }, preferredWrongAnswers: states));
    }

    // Used by Black, Blue, Brown, Coral, Cornflower, Cream, Crimson, Forest, Gray, Green, Indigo, Magenta, Maroon, Orange, Red, Violet, White, Yellow, and Ultimate Cipher
    private IEnumerable<object> processColoredCiphers(KMBombModule module, string componentName, Question question, string moduleId)
    {
        var comp = GetComponent(module, componentName);

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };

        while (!solved)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(moduleId);

        var pages = GetField<IList>(comp, "pages").Get(v => v.Count == 0 ? "expected at least one page" : null);
        var fldScreens = GetProperty<IList>(pages[0], "Screens", isPublic: true);
        var fldText = GetProperty<string>(fldScreens.Get(v => v.Count == 0 ? "expected at least one screen per page" : null)[0], "Text", isPublic: true);
        var fldAvoid = GetProperty<bool>(fldScreens.Get(v => v.Count == 0 ? "expected at least one screen per page" : null)[0], "SouvenirAvoid", isPublic: true);

        var allWordsType = comp.GetType().Assembly.GetType("Words.Data") ?? throw new AbandonModuleException("I cannot find the Words.Data type.");
        var allWordsObj = Activator.CreateInstance(allWordsType);
        var allWords = GetArrayField<List<string>>(allWordsObj, "_allWords").Get(expectedLength: 5);

        string[] generateWrongAnswers(string correctAnswer, AnswerGeneratorAttribute gen)
        {
            var set = new HashSet<string> { correctAnswer };
            var iter = 0;
            while (set.Count < 6 && iter < 100)
            {
                foreach (var ans in gen.GetAnswers(this).Take(6 - set.Count))
                    set.Add(ans);
                iter++;
            }
            return set.ToArray();
        }

        string[] generateWrongAnswersFnc(string correctAnswer, Func<string> gen)
        {
            var set = new HashSet<string> { correctAnswer };
            var iter = 0;
            while (set.Count < 6 && iter < 100)
            {
                set.Add(gen());
                iter++;
            }
            return set.ToArray();
        }

        var screenNames = new[] { "top", "middle", "bottom" };
        var romanNumerals = new[] { "I", "II", "III", "IV", "V", "VI", "VII", "VIII" };
        addQuestions(module, Enumerable.Range(0, pages.Count).SelectMany(pageIx =>
        {
            var screenObjs = fldScreens.GetFrom(pages[pageIx], v => v.Count == 0 ? "expected at least one screen per page" : null);
            var screenTexts = Enumerable.Range(0, screenObjs.Count).Select(scrIx => (page: pageIx, screen: scrIx, text: fldText.GetFrom(screenObjs[scrIx], nullAllowed: true), avoid: fldAvoid.GetFrom(screenObjs[scrIx])));
            return screenTexts.Where(tup => !tup.avoid && !string.IsNullOrEmpty(tup.text)).Select(tup =>
            {
                // Black Cipher special case: A-VII-IV-V
                var rom = romanNumerals.JoinString("|");
                if (Regex.IsMatch(tup.text, $@"^[ABC]-({rom})-({rom})-({rom})$"))
                    return makeQuestion(question, module.ModuleType, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, () => $"{"ABC"[Rnd.Range(0, 3)]}-{romanNumerals.ToArray().Shuffle().Take(3).JoinString("-")}"));

                // Black Cipher special case: NJ-SG-CV
                if (Regex.IsMatch(tup.text, @"^[A-Z]{2}(-[A-Z]{2})+$"))
                {
                    var n = (tup.text.Length + 1) / 3;
                    string gen()
                    {
                        var shuffle = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().Shuffle().Take(2 * n).JoinString();
                        for (var i = n - 1; i >= 1; i--)
                            shuffle = shuffle.Insert(2 * i, "-");
                        return shuffle;
                    }
                    return makeQuestion(question, module.ModuleType, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, gen));
                }

                // Brown Cipher page 2 screen 3 will only have letters A to F
                if (Regex.IsMatch(tup.text, @"^[A-F]+$"))
                    return makeQuestion(question, module.ModuleType, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, 'A', 'F')));

                // Cornflower Cipher special case: three letters and a digit
                if (Regex.IsMatch(tup.text, @"^[A-Z]{3} \d$"))
                    return makeQuestion(question, module.ModuleType, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, () => $"{"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)]}{"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)]}{"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)]} {Rnd.Range(0, 10)}"));

                // Indigo Cipher special case: 24 ? 52 = 12
                if (Regex.IsMatch(tup.text, @"^\d+ \? \d+ = \d+$"))
                    return makeQuestion(question, module.ModuleType, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, () => $"{Rnd.Range(0, 64)} ? {Rnd.Range(0, 64)} = {Rnd.Range(0, 64)}"));

                // Yellow Cipher special case: 8-5-7-20
                if (Regex.IsMatch(tup.text, @"^\d+-\d+-\d+-\d+$"))
                    return makeQuestion(question, module.ModuleType, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, () => $"{Rnd.Range(0, 26)}-{Rnd.Range(0, 26)}-{Rnd.Range(0, 26)}-{Rnd.Range(0, 26)}"));

                // Screens that have a word on them: pick other words of the same length as wrong answers
                if (tup.text.Length >= 4 && tup.text.Length <= 8 && allWords[tup.text.Length - 4].Contains(tup.text))
                    return makeQuestion(question, module.ModuleType, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: allWords[tup.text.Length - 4].ToArray());

                // Screens that have only 0s and 1s on them
                if (tup.text.Length >= 3 && tup.text.All(ch => ch == '0' || ch == '1'))
                    return makeQuestion(question, module.ModuleType, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, '0', '1')));

                // Screens that have only digits on them
                if (tup.text.All(ch => ch >= '0' && ch <= '9'))
                    return makeQuestion(question, module.ModuleType, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, '0', '9')));

                // Screens that have only capital letters on them
                if (tup.text.All(ch => ch >= 'A' && ch <= 'Z'))
                    return makeQuestion(question, module.ModuleType, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, 'A', 'Z')));

                // All other cases: jumble of letters and digits
                return makeQuestion(question, module.ModuleType, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                    preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")));
            });
        }));
    }

    // Used by The Hypercube and The Ultracube
    private IEnumerable<object> processHypercubeUltracube(KMBombModule module, string componentName, Question question, string moduleId)
    {
        var comp = GetComponent(module, componentName);
        var rotations = GetStaticField<string[]>(comp.GetType(), "_rotationNames").Get();
        var sequence = GetArrayField<int>(comp, "_rotations").Get(expectedLength: 5, validator: rot => rot < 0 || rot >= rotations.Length ? $"expected range 0–{rotations.Length - 1}" : null);

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(moduleId);
        addQuestions(module,
            makeQuestion(question, moduleId, formatArgs: new[] { "first" }, correctAnswers: new[] { rotations[sequence[0]] }),
            makeQuestion(question, moduleId, formatArgs: new[] { "second" }, correctAnswers: new[] { rotations[sequence[1]] }),
            makeQuestion(question, moduleId, formatArgs: new[] { "third" }, correctAnswers: new[] { rotations[sequence[2]] }),
            makeQuestion(question, moduleId, formatArgs: new[] { "fourth" }, correctAnswers: new[] { rotations[sequence[3]] }),
            makeQuestion(question, moduleId, formatArgs: new[] { "fifth" }, correctAnswers: new[] { rotations[sequence[4]] }));
    }

    // Used by Triamonds and Tetriamonds
    private IEnumerable<object> processPolyiamonds(KMBombModule module, string componentName, Question question, string moduleId, string[] colourNames)
    {
        var comp = GetComponent(module, componentName);
        var fldSolved = GetField<bool>(comp, "solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(moduleId);

        var posColour = GetField<int[]>(comp, "poscolour").Get();
        var pulsing = GetField<int[]>(comp, "pulsing").Get();

        var qs = new List<QandA>();
        for (int pos = 0; pos < 3; pos++)
            qs.Add(makeQuestion(question, _Tetriamonds, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { colourNames[posColour[pulsing[pos]]] }));
        addQuestions(module, qs);
    }
}

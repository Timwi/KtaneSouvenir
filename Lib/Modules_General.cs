using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    const string Version = "4.4";

    // The values here are the “ModuleType” property on the KMBombModule components.
    const string _1000Words = "OneThousandWords";
    const string _100LevelsOfDefusal = "100LevelsOfDefusal";
    const string _1DChess = "1DChess";
    const string _3DMaze = "spwiz3DMaze";
    const string _3DTapCode = "3DTapCodeModule";
    const string _3DTunnels = "3dTunnels";
    const string _3LEDs = "threeLEDsModule";
    const string _7 = "7";
    const string _9Ball = "GSNineBall";
    const string _Accumulation = "accumulation";
    const string _AdventureGame = "spwizAdventureGame";
    const string _AffineCycle = "affineCycle";
    const string _AlfaBravo = "alfa_bravo";
    const string _Algebra = "algebra";
    const string _AlphabeticalRuling = "alphabeticalRuling";
    const string _AlphabetTiles = "AlphabetTiles";
    const string _AlphaBits = "alphaBits";
    const string _Arithmelogic = "arithmelogic";
    const string _ASCIIMaze = "asciiMaze";
    const string _ASquare = "ASquareModule";
    const string _BamboozledAgain = "bamboozledAgain";
    const string _BamboozlingButton = "bamboozlingButton";
    const string _Bakery = "bakery";
    const string _BarcodeCipher = "BarcodeCipherModule";
    const string _Bartending = "BartendingModule";
    const string _BigCircle = "BigCircle";
    const string _Binary = "Binary";
    const string _BinaryLEDs = "BinaryLeds";
    const string _BinaryShift = "binary_shift";
    const string _Bitmaps = "BitmapsModule";
    const string _BlackCipher = "blackCipher";
    const string _BlindMaze = "BlindMaze";
    const string _Blockbusters = "blockbusters";
    const string _BlueArrows = "blueArrowsModule";
    const string _BlueButton = "BlueButtonModule";
    const string _BlueCipher = "blueCipher";
    const string _BobBarks = "ksmBobBarks";
    const string _Boggle = "boggle";
    const string _Boxing = "boxing";
    const string _Braille = "BrailleModule";
    const string _BrokenButtons = "BrokenButtonsModule";
    const string _BrownCipher = "brownCipher";
    const string _BrushStrokes = "brushStrokes";
    const string _Bulb = "TheBulbModule";
    const string _BurglarAlarm = "burglarAlarm";
    const string _Button = "BigButton";
    const string _ButtonSequences = "buttonSequencesModule";
    const string _CaesarCycle = "caesarCycle";
    const string _Cartinese = "cartinese";
    const string _Calendar = "calendar";
    const string _ChallengeAndContact = "challengeAndContact";
    const string _CheapCheckout = "CheapCheckoutModule";
    const string _CheepCheckout = "cheepCheckout";
    const string _Chess = "ChessModule";
    const string _ChineseCounting = "chineseCounting";
    const string _ChordQualities = "ChordQualities";
    const string _Code = "theCodeModule";
    const string _Codenames = "codenames";
    const string _Coffeebucks = "coffeebucks";
    const string _Coinage = "Coinage";
    const string _ColorBraille = "ColorBrailleModule";
    const string _ColorDecoding = "Color Decoding";
    const string _ColoredKeys = "lgndColoredKeys";
    const string _ColoredSquares = "ColoredSquaresModule";
    const string _ColoredSwitches = "ColoredSwitchesModule";
    const string _ColorMorse = "ColorMorseModule";
    const string _ColorsMaximization = "colors_maximization";
    const string _ColourFlash = "ColourFlash";
    const string _Coordinates = "CoordinatesModule";
    const string _Corners = "CornersModule";
    const string _Cosmic = "CosmicModule";
    const string _Creation = "CreationModule";
    const string _Critters = "CrittersModule";
    const string _CrypticCycle = "crypticCycle";
    const string _CrypticKeypad = "GSCrypticKeypad";
    const string _Cube = "cube";
    const string _CyanButton = "CyanButtonModule";
    const string _DACHMaze = "DACH";
    const string _DeafAlley = "deafAlleyModule";
    const string _DeckOfManyThings = "deckOfManyThings";
    const string _DecoloredSquares = "DecoloredSquaresModule";
    const string _DevilishEggs = "devilishEggs";
    const string _DiscoloredSquares = "DiscoloredSquaresModule";
    const string _DivisibleNumbers = "divisibleNumbers";
    const string _DoubleColor = "doubleColor";
    const string _DoubleDigits = "doubleDigitsModule";
    const string _DoubleOh = "DoubleOhModule";
    const string _DrDoctor = "DrDoctorModule";
    const string _Dreamcipher = "ksmDreamcipher";
    const string _DumbWaiters = "dumbWaiters";
    const string _eeBgnillepS = "eeBgnilleps";
    const string _Eight = "eight";
    const string _ElderFuthark = "elderFuthark";
    const string _EncryptedEquations = "EncryptedEquationsModule";
    const string _EncryptedHangman = "encryptedHangman";
    const string _EncryptedMaze = "encryptedMaze";
    const string _EncryptedMorse = "EncryptedMorse";
    const string _EncryptionBingo = "encryptionBingo";
    const string _EntryNumberFour = "GSEntryNumberFour";
    const string _EntryNumberOne = "GSEntryNumberOne";
    const string _EquationsX = "equationsXModule";
    const string _Etterna = "etterna";
    const string _Exoplanets = "exoplanets";
    const string _FactoringMaze = "factoringMaze";
    const string _FactoryMaze = "factoryMaze";
    const string _FastMath = "fastMath";
    const string _FaultyRGBMaze = "faultyrgbMaze";
    const string _Flags = "FlagsModule";
    const string _FlashingArrows = "flashingArrowsModule";
    const string _FlashingLights = "flashingLights";
    const string _ForgetAnyColor = "ForgetAnyColor";
    const string _ForgetsUltimateShowdown = "ForgetsUltimateShowdownModule";
    const string _ForgetTheColors = "ForgetTheColors";
    const string _FreeParking = "freeParking";
    const string _Functions = "qFunctions";
    const string _GameOfLifeCruel = "GameOfLifeCruel";
    const string _Gamepad = "TheGamepadModule";
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
    const string _HereditaryBaseNotation = "hereditaryBaseNotationModule";
    const string _Hexabutton = "hexabutton";
    const string _Hexamaze = "HexamazeModule";
    const string _HexOS = "hexOS";
    const string _HiddenColors = "lgndHiddenColors";
    const string _HillCycle = "hillCycle";
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
    const string _IndigoCipher = "indigoCipher";
    const string _InfiniteLoop = "InfiniteLoop";
    const string _Ingredients = "ingredients";
    const string _InnerConnections = "InnerConnectionsModule";
    const string _Interpunct = "interpunct";
    const string _IPA = "ipa";
    const string _iPhone = "iPhone";
    const string _JewelVault = "jewelVault";
    const string _JumbleCycle = "jumbleCycle";
    const string _KanyeEncounter = "TheKanyeEncounter";
    const string _KeypadMagnified = "keypadMagnified";
    const string _Kudosudoku = "KudosudokuModule";
    const string _Ladders = "ladders";
    const string _Lasers = "lasers";
    const string _LEDEncryption = "LEDEnc";
    const string _LEDMath = "lgndLEDMath";
    const string _LEGOs = "LEGOModule";
    const string _LetterMath = "letterMath";
    const string _Linq = "Linq";
    const string _LionsShare = "LionsShareModule";
    const string _Listening = "Listening";
    const string _LogicalButtons = "logicalButtonsModule";
    const string _LogicGates = "logicGates";
    const string _LombaxCubes = "lgndLombaxCubes";
    const string _LondonUnderground = "londonUnderground";
    const string _Mafia = "MafiaModule";
    const string _Mahjong = "MahjongModule";
    const string _MandMs = "MandMs";
    const string _MandNs = "MandNs";
    const string _MaritimeFlags = "MaritimeFlagsModule";
    const string _Mashematics = "mashematics";
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
    const string _NotConnectionCheck = "notConnectionCheck";
    const string _NotCoordinates = "notCoordinates";
    const string _NotKeypad = "NotKeypad";
    const string _NotMaze = "NotMaze";
    const string _NotMorseCode = "NotMorseCode";
    const string _NotMorsematics = "notMorsematics";
    const string _NotMurder = "notMurder";
    const string _NotNumberPad = "notNumberPad";
    const string _NotSimaze = "NotSimaze";
    const string _NotTheBulb = "notTheBulb";
    const string _NotTheButton = "NotButton";
    const string _NotTheScrew = "notTheScrew";
    const string _NotWhosOnFirst = "NotWhosOnFirst";
    const string _NotX01 = "notX01";
    const string _NotXRay = "NotXRayModule";
    const string _NumberedButtons = "numberedButtonsModule";
    const string _Numbers = "Numbers";
    const string _Numpath = "numpath";
    const string _ObjectShows = "objectShows";
    const string _Octadecayotton = "TheOctadecayotton";
    const string _OddOneOut = "OddOneOutModule";
    const string _OnlyConnect = "OnlyConnectModule";
    const string _OrangeArrows = "orangeArrowsModule";
    const string _OrangeCipher = "orangeCipher";
    const string _OrderedKeys = "orderedKeys";
    const string _OrderPicking = "OrderPickingModule";
    const string _OrientationCube = "OrientationCube";
    const string _Palindromes = "palindromes";
    const string _PartialDerivatives = "partialDerivatives";
    const string _PassportControl = "passportControl";
    const string _PasswordDestroyer = "pwDestroyer";
    const string _PatternCube = "PatternCubeModule";
    const string _PerspectivePegs = "spwizPerspectivePegs";
    const string _Phosphorescence = "Phosphorescence";
    const string _Pie = "pieModule";
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
    const string _PurpleArrows = "purpleArrowsModule";
    const string _PurpleButton = "PurpleButtonModule";
    const string _PuzzleIdentification = "GSPuzzleIdentification";
    const string _Quaver = "Quaver";
    const string _Quintuples = "quintuples";
    const string _RailwayCargoLoading = "RailwayCargoLoading";
    const string _RainbowArrows = "ksmRainbowArrows";
    const string _RecoloredSwitches = "R4YRecoloredSwitches";
    const string _RedArrows = "redArrowsModule";
    const string _RedCipher = "redCipher";
    const string _ReformedRoleReversal = "ReformedRoleReversal";
    const string _RegularCrazyTalk = "RegularCrazyTalkModule";
    const string _Retirement = "retirement";
    const string _ReverseMorse = "reverseMorse";
    const string _ReversePolishNotation = "revPolNot";
    const string _RGBMaze = "rgbMaze";
    const string _Rhythms = "MusicRhythms";
    const string _Roger = "roger";
    const string _RoleReversal = "roleReversal";
    const string _Rule = "theRule";
    const string _RuleOfThree = "RuleOfThreeModule";
    const string _Samsung = "theSamsung";
    const string _ScavengerHunt = "scavengerHunt";
    const string _SchlagDenBomb = "qSchlagDenBomb";
    const string _SeaShells = "SeaShells";
    const string _Semamorse = "semamorse";
    const string _Sequencyclopedia = "TheSequencyclopedia";
    const string _ShapesBombs = "ShapesBombs";
    const string _ShapeShift = "shapeshift";
    const string _ShellGame = "shellGame";
    const string _ShiftedMaze = "shiftedMaze";
    const string _ShiftingMaze = "MazeShifting";
    const string _SillySlots = "SillySlots";
    const string _SimonSamples = "simonSamples";
    const string _SimonSays = "Simon";
    const string _SimonScrambles = "simonScrambles";
    const string _SimonScreams = "SimonScreamsModule";
    const string _SimonSelects = "simonSelectsModule";
    const string _SimonSends = "SimonSendsModule";
    const string _SimonShouts = "SimonShoutsModule";
    const string _SimonShrieks = "SimonShrieksModule";
    const string _SimonSimons = "simonSimons";
    const string _SimonSings = "SimonSingsModule";
    const string _SimonSounds = "simonSounds";
    const string _SimonSpeaks = "SimonSpeaksModule";
    const string _SimonsStar = "simonsStar";
    const string _SimonStages = "simonStages";
    const string _SimonStates = "SimonV2";
    const string _SimonStops = "simonStops";
    const string _SimonStores = "simonStores";
    const string _SimonSupports = "simonSupports";
    const string _SkewedSlots = "SkewedSlotsModule";
    const string _Skyrim = "skyrim";
    const string _SmallCircle = "smallCircle";
    const string _Snooker = "snooker";
    const string _SonicTheHedgehog = "sonic";
    const string _Sorting = "sorting";
    const string _Souvenir = "SouvenirModule";
    const string _SpaceTraders = "space_traders";
    const string _SpellingBee = "spellingBee";
    const string _Sphere = "sphere";
    const string _SplittingTheLoot = "SplittingTheLootModule";
    const string _SpotTheDifference = "SpotTheDifference";
    const string _StackedSequences = "stackedSequences";
    const string _Stars = "stars";
    const string _StateOfAggregation = "stateOfAggregation";
    const string _Stellar = "stellar";
    const string _StupidSlots = "stupidSlots";
    const string _SubscribeToPewdiepie = "subscribeToPewdiepie";
    const string _SugarSkulls = "sugarSkulls";
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
    const string _TenButtonColorCode = "TenButtonColorCode";
    const string _Tenpins = "tenpins";
    const string _TextField = "TextField";
    const string _ThinkingWires = "thinkingWiresModule";
    const string _ThirdBase = "ThirdBase";
    const string _TicTacToe = "TicTacToeModule";
    const string _Timezone = "timezone";
    const string _TopsyTurvy = "topsyTurvy";
    const string _TransmittedMorse = "transmittedMorseModule";
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
    const string _UnownCipher = "UnownCipher";
    const string _USAMaze = "USA";
    const string _V = "V";
    const string _VaricoloredSquares = "VaricoloredSquaresModule";
    const string _Vcrcs = "VCRCS";
    const string _Vectors = "vectorsModule";
    const string _Vexillology = "vexillology";
    const string _VioletCipher = "violetCipher";
    const string _VisualImpairment = "visual_impairment";
    const string _Wavetapping = "Wavetapping";
    const string _WhatsOnSecond = "WhatsOnSecond";
    const string _WhiteCipher = "whiteCipher";
    const string _WhosOnFirst = "WhosOnFirst";
    const string _Wire = "wire";
    const string _WireOrdering = "kataWireOrdering";
    const string _WireSequence = "WireSequence";
    const string _WolfGoatAndCabbage = "wolfGoatCabbageModule";
    const string _WorkingTitle = "workingTitle";
    const string _Xenocryst = "GSXenocryst";
    const string _XmORseCode = "xmorse";
    const string _Yahtzee = "YahtzeeModule";
    const string _YellowArrows = "yellowArrowsModule";
    const string _YellowButton = "YellowButtonModule";
    const string _YellowCipher = "yellowCipher";
    const string _ZeroZero = "zeroZero";
    const string _Zoni = "lgndZoni";

    void Awake()
    {
        _moduleProcessors = new Dictionary<string, Func<KMBombModule, IEnumerable<object>>>()
        {
            { _1000Words, Process1000Words },
            { _100LevelsOfDefusal, Process100LevelsOfDefusal },
            { _1DChess, Process1DChess },
            { _3DMaze, Process3DMaze },
            { _3DTapCode, Process3DTapCode },
            { _3DTunnels, Process3DTunnels },
            { _3LEDs, Process3LEDs },
            { _7, Process7 },
            { _9Ball, Process9Ball },
            { _Accumulation, ProcessAccumulation },
            { _AdventureGame, ProcessAdventureGame },
            { _AffineCycle, ProcessAffineCycle },
            { _AlfaBravo, ProcessAlfaBravo },
            { _Algebra, ProcessAlgebra },
            { _AlphabeticalRuling, ProcessAlphabeticalRuling },
            { _AlphabetTiles, ProcessAlphabetTiles },
            { _AlphaBits, ProcessAlphaBits },
            { _Arithmelogic, ProcessArithmelogic },
            { _ASCIIMaze, ProcessASCIIMaze },
            { _ASquare, ProcessASquare },
            { _BamboozledAgain, ProcessBamboozledAgain },
            { _BamboozlingButton, ProcessBamboozlingButton },
            { _Bakery, ProcessBakery },
            { _BarcodeCipher, ProcessBarcodeCipher },
            { _Bartending, ProcessBartending },
            { _BigCircle, ProcessBigCircle },
            { _Binary, ProcessBinary },
            { _BinaryLEDs, ProcessBinaryLEDs },
            { _BinaryShift, ProcessBinaryShift },
            { _Bitmaps, ProcessBitmaps },
            { _BlackCipher, ProcessBlackCipher },
            { _BlindMaze, ProcessBlindMaze },
            { _Blockbusters, ProcessBlockbusters },
            { _BlueArrows, ProcessBlueArrows },
            { _BlueButton, ProcessBlueButton },
            { _BlueCipher, ProcessBlueCipher },
            { _BobBarks, ProcessBobBarks },
            { _Boggle, ProcessBoggle },
            { _Boxing, ProcessBoxing },
            { _Braille, ProcessBraille },
            { _BrokenButtons, ProcessBrokenButtons },
            { _BrownCipher, ProcessBrownCipher },
            { _BrushStrokes, ProcessBrushStrokes },
            { _Bulb, ProcessBulb },
            { _BurglarAlarm, ProcessBurglarAlarm },
            { _Button, ProcessButton },
            { _ButtonSequences, ProcessButtonSequences },
            { _CaesarCycle, ProcessCaesarCycle },
            { _Calendar, ProcessCalendar },
            { _Cartinese, ProcessCartinese },
            { _ChallengeAndContact, ProcessChallengeAndContact },
            { _CheapCheckout, ProcessCheapCheckout },
            { _CheepCheckout, ProcessCheepCheckout },
            { _Chess, ProcessChess },
            { _ChineseCounting, ProcessChineseCounting },
            { _ChordQualities, ProcessChordQualities },
            { _Code, ProcessCode },
            { _Codenames, ProcessCodenames },
            { _Coffeebucks, ProcessCoffeebucks },
            { _Coinage, ProcessCoinage },
            { _ColorBraille, ProcessColorBraille },
            { _ColorDecoding, ProcessColorDecoding },
            { _ColoredKeys, ProcessColoredKeys },
            { _ColoredSquares, ProcessColoredSquares },
            { _ColoredSwitches, ProcessColoredSwitches },
            { _ColorMorse, ProcessColorMorse },
            { _ColorsMaximization, ProcessColorsMaximization },
            { _ColourFlash, ProcessColourFlash },
            { _Coordinates, ProcessCoordinates },
            { _Corners, ProcessCorners },
            { _Cosmic, ProcessCosmic },
            { _Creation, ProcessCreation },
            { _Critters, ProcessCritters },
            { _CrypticCycle, ProcessCrypticCycle },
            { _CrypticKeypad, ProcessCrypticKeypad },
            { _Cube, ProcessCube },
            { _CyanButton, ProcessCyanButton },
            { _DACHMaze, ProcessDACHMaze },
            { _DeafAlley, ProcessDeafAlley },
            { _DeckOfManyThings, ProcessDeckOfManyThings },
            { _DecoloredSquares, ProcessDecoloredSquares },
            { _DevilishEggs, ProcessDevilishEggs },
            { _DiscoloredSquares, ProcessDiscoloredSquares },
            { _DivisibleNumbers, ProcessDivisibleNumbers },
            { _DoubleColor, ProcessDoubleColor },
            { _DoubleDigits, ProcessDoubleDigits },
            { _DoubleOh, ProcessDoubleOh },
            { _DrDoctor, ProcessDrDoctor },
            { _Dreamcipher, ProcessDreamcipher },
            { _DumbWaiters, ProcessDumbWaiters },
            { _eeBgnillepS, ProcessEeBgnillepS },
            { _Eight, ProcessEight },
            { _ElderFuthark, ProcessElderFuthark },
            { _EncryptedEquations, ProcessEncryptedEquations },
            { _EncryptedHangman, ProcessEncryptedHangman },
            { _EncryptedMaze, ProcessEncryptedMaze },
            { _EncryptedMorse, ProcessEncryptedMorse },
            { _EncryptionBingo, ProcessEncryptionBingo },
            { _EntryNumberFour, ProcessEntryNumberFour },
            { _EntryNumberOne, ProcessEntryNumberOne },
            { _EquationsX, ProcessEquationsX },
            { _Etterna, ProcessEtterna },
            { _Exoplanets, ProcessExoplanets },
            { _FactoringMaze, ProcessFactoringMaze },
            { _FactoryMaze, ProcessFactoryMaze },
            { _FastMath, ProcessFastMath },
            { _FaultyRGBMaze, ProcessFaultyRGBMaze },
            { _Flags, ProcessFlags },
            { _FlashingArrows, ProcessFlashingArrows },
            { _FlashingLights, ProcessFlashingLights },
            { _ForgetAnyColor, ProcessForgetAnyColor },
            { _ForgetsUltimateShowdown, ProcessForgetsUltimateShowdown },
            { _ForgetTheColors, ProcessForgetTheColors },
            { _FreeParking, ProcessFreeParking },
            { _Functions, ProcessFunctions },
            { _GameOfLifeCruel, ProcessGameOfLifeCruel },
            { _Gamepad, ProcessGamepad },
            { _GlitchedButton, ProcessGlitchedButton },
            { _GrayButton, ProcessGrayButton },
            { _GrayCipher, ProcessGrayCipher },
            { _GreatVoid, ProcessGreatVoid },
            { _GreenArrows, ProcessGreenArrows },
            { _GreenButton, ProcessGreenButton },
            { _GreenCipher, ProcessGreenCipher },
            { _GridLock, ProcessGridLock },
            { _GroceryStore, ProcessGroceryStore },
            { _Gryphons, ProcessGryphons },
            { _GuessWho, ProcessGuessWho },
            { _HereditaryBaseNotation, ProcessHereditaryBaseNotation },
            { _Hexabutton, ProcessHexabutton },
            { _Hexamaze, ProcessHexamaze },
            { _HexOS, ProcessHexOS },
            { _HiddenColors, ProcessHiddenColors },
            { _HillCycle, ProcessHillCycle },
            { _Hogwarts, ProcessHogwarts },
            { _HoldUps, ProcessHoldUps },
            { _Homophones, ProcessHomophones },
            { _HorribleMemory, ProcessHorribleMemory },
            { _HumanResources, ProcessHumanResources },
            { _Hunting, ProcessHunting },
            { _Hypercube, ProcessHypercube },
            { _Hyperlink, ProcessHyperlink },
            { _IceCream, ProcessIceCream },
            { _IdentificationCrisis, ProcessIdentificationCrisis },
            { _IdentityParade, ProcessIdentityParade },
            { _IndigoCipher, ProcessIndigoCipher },
            { _InfiniteLoop, ProcessInfiniteLoop },
            { _Ingredients, ProcessIngredients },
            { _InnerConnections, ProcessInnerConnections },
            { _Interpunct, ProcessInterpunct },
            { _IPA, ProcessIPA },
            { _iPhone, ProcessiPhone },
            { _JewelVault, ProcessJewelVault },
            { _JumbleCycle, ProcessJumbleCycle },
            { _KanyeEncounter, ProcessKanyeEncounter },
            { _KeypadMagnified, ProcessKeypadMagnified },
            { _Kudosudoku, ProcessKudosudoku },
            { _Ladders, ProcessLadders },
            { _Lasers, ProcessLasers },
            { _LEDEncryption, ProcessLEDEncryption },
            { _LEDMath, ProcessLEDMath },
            { _LEGOs, ProcessLEGOs },
            { _LetterMath, ProcessLetterMath },
            { _Linq, ProcessLinq },
            { _LionsShare, ProcessLionsShare },
            { _Listening, ProcessListening },
            { _LogicalButtons, ProcessLogicalButtons },
            { _LogicGates, ProcessLogicGates },
            { _LombaxCubes, ProcessLombaxCubes },
            { _LondonUnderground, ProcessLondonUnderground },
            { _Mafia, ProcessMafia },
            { _Mahjong, ProcessMahjong },
            { _MandMs, ProcessMandMs },
            { _MandNs, ProcessMandNs },
            { _MaritimeFlags, ProcessMaritimeFlags },
            { _Mashematics, ProcessMashematics },
            { _Matrix, ProcessMatrix },
            { _Maze, ProcessMaze },
            { _Maze3, ProcessMaze3 },
            { _MazeIdentification, ProcessMazeIdentification },
            { _Mazematics, ProcessMazematics },
            { _MazeScrambler, ProcessMazeScrambler },
            { _Mazeseeker, ProcessMazeseeker },
            { _MegaMan2, ProcessMegaMan2 },
            { _MelodySequencer, ProcessMelodySequencer },
            { _MemorableButtons, ProcessMemorableButtons },
            { _Memory, ProcessMemory },
            { _Metamorse, ProcessMetamorse },
            { _Microcontroller, ProcessMicrocontroller },
            { _Minesweeper, ProcessMinesweeper },
            { _Mirror, ProcessMirror },
            { _MisterSoftee, ProcessMisterSoftee },
            { _ModernCipher, ProcessModernCipher },
            { _ModuleListening, ProcessModuleListening },
            { _ModuleMaze, ProcessModuleMaze },
            { _MonsplodeFight, ProcessMonsplodeFight },
            { _MonsplodeTradingCards, ProcessMonsplodeTradingCards },
            { _Moon, ProcessMoon },
            { _MoreCode, ProcessMoreCode },
            { _MorseAMaze, ProcessMorseAMaze },
            { _MorseButtons, ProcessMorseButtons },
            { _Morsematics, ProcessMorsematics },
            { _MorseWar, ProcessMorseWar },
            { _MouseInTheMaze, ProcessMouseInTheMaze },
            { _MulticoloredSwitches, ProcessMulticoloredSwitches },
            { _Murder, ProcessMurder },
            { _MysteryModule, ProcessMysteryModule },
            { _MysticSquare, ProcessMysticSquare },
            { _NameCodes, ProcessNameCodes },
            { _NandMs, ProcessNandMs },
            { _Navinums, ProcessNavinums },
            { _NavyButton, ProcessNavyButton },
            { _Necronomicon, ProcessNecronomicon },
            { _Negativity, ProcessNegativity },
            { _Neutralization, ProcessNeutralization },
            { _NotConnectionCheck, ProcessNotConnectionCheck },
            { _NotCoordinates, ProcessNotCoordinates },
            { _NotKeypad, ProcessNotKeypad },
            { _NotMaze, ProcessNotMaze },
            { _NotMorseCode, ProcessNotMorseCode },
            { _NotMorsematics, ProcessNotMorsematics },
            { _NotMurder, ProcessNotMurder },
            { _NotNumberPad, ProcessNotNumberPad },
            { _NotSimaze, ProcessNotSimaze },
            { _NotTheBulb, ProcessNotTheBulb },
            { _NotTheButton, ProcessNotTheButton },
            { _NotTheScrew, ProcessNotTheScrew },
            { _NotWhosOnFirst, ProcessNotWhosOnFirst },
            { _NotX01, ProcessNotX01 },
            { _NotXRay, ProcessNotXRay },
            { _NumberedButtons, ProcessNumberedButtons },
            { _Numbers, ProcessNumbers },
            { _Numpath, ProcessNumpath },
            { _ObjectShows, ProcessObjectShows },
            { _Octadecayotton, ProcessOctadecayotton },
            { _OddOneOut, ProcessOddOneOut },
            { _OnlyConnect, ProcessOnlyConnect },
            { _OrangeArrows, ProcessOrangeArrows },
            { _OrangeCipher, ProcessOrangeCipher },
            { _OrderedKeys, ProcessOrderedKeys },
            { _OrderPicking, ProcessOrderPicking },
            { _OrientationCube, ProcessOrientationCube },
            { _Palindromes, ProcessPalindromes },
            { _PartialDerivatives, ProcessPartialDerivatives },
            { _PassportControl, ProcessPassportControl },
            { _PasswordDestroyer, ProcessPasswordDestroyer },
            { _PatternCube, ProcessPatternCube },
            { _PerspectivePegs, ProcessPerspectivePegs },
            { _Phosphorescence, ProcessPhosphorescence },
            { _Pie, ProcessPie },
            { _PigpenCycle, ProcessPigpenCycle },
            { _PinkButton, ProcessPinkButton },
            { _PixelCipher, ProcessPixelCipher },
            { _PlaceholderTalk, ProcessPlaceholderTalk },
            { _PlacementRoulette, ProcessPlacementRoulette },
            { _Planets, ProcessPlanets },
            { _PlayfairCycle, ProcessPlayfairCycle },
            { _Poetry, ProcessPoetry },
            { _PolyhedralMaze, ProcessPolyhedralMaze },
            { _PrimeEncryption, ProcessPrimeEncryption },
            { _Probing, ProcessProbing },
            { _PurpleArrows, ProcessPurpleArrows },
            { _PurpleButton, ProcessPurpleButton },
            { _PuzzleIdentification, ProcessPuzzleIdentification },
            { _Quaver, ProcessQuaver },
            { _Quintuples, ProcessQuintuples },
            { _RailwayCargoLoading, ProcessRailwayCargoLoading },
            { _RainbowArrows, ProcessRainbowArrows },
            { _RecoloredSwitches, ProcessRecoloredSwitches },
            { _RedArrows, ProcessRedArrows },
            { _RedCipher, ProcessRedCipher },
            { _ReformedRoleReversal, ProcessReformedRoleReversal },
            { _RegularCrazyTalk, ProcessRegularCrazyTalk },
            { _Retirement, ProcessRetirement },
            { _ReverseMorse, ProcessReverseMorse },
            { _ReversePolishNotation, ProcessReversePolishNotation },
            { _RGBMaze, ProcessRGBMaze },
            { _Rhythms, ProcessRhythms },
            { _Roger, ProcessRoger },
            { _RoleReversal, ProcessRoleReversal },
            { _Rule, ProcessRule },
            { _RuleOfThree, ProcessRuleOfThree },
            { _Samsung, ProcessSamsung },
            { _ScavengerHunt, ProcessScavengerHunt },
            { _SchlagDenBomb, ProcessSchlagDenBomb },
            { _SeaShells, ProcessSeaShells },
            { _Semamorse, ProcessSemamorse },
            { _Sequencyclopedia, ProcessSequencyclopedia },
            { _ShapesBombs, ProcessShapesAndBombs },
            { _ShapeShift, ProcessShapeShift },
            { _ShellGame, ProcessShellGame },
            { _ShiftedMaze, ProcessShiftedMaze },
            { _ShiftingMaze, ProcessShiftingMaze },
            { _SillySlots, ProcessSillySlots },
            { _SimonSamples, ProcessSimonSamples },
            { _SimonSays, ProcessSimonSays },
            { _SimonScrambles, ProcessSimonScrambles },
            { _SimonScreams, ProcessSimonScreams },
            { _SimonSelects, ProcessSimonSelects },
            { _SimonSends, ProcessSimonSends },
            { _SimonShouts, ProcessSimonShouts },
            { _SimonShrieks, ProcessSimonShrieks },
            { _SimonSimons, ProcessSimonSimons },
            { _SimonSings, ProcessSimonSings },
            { _SimonSounds, ProcessSimonSounds },
            { _SimonSpeaks, ProcessSimonSpeaks },
            { _SimonsStar, ProcessSimonsStar },
            { _SimonStages, ProcessSimonStages },
            { _SimonStates, ProcessSimonStates },
            { _SimonStops, ProcessSimonStops },
            { _SimonStores, ProcessSimonStores },
            { _SimonSupports, ProcessSimonSupports },
            { _SkewedSlots, ProcessSkewedSlots },
            { _Skyrim, ProcessSkyrim },
            { _SmallCircle, ProcessSmallCircle },
            { _Snooker, ProcessSnooker },
            { _SonicTheHedgehog, ProcessSonicTheHedgehog },
            { _Sorting, ProcessSorting },
            { _Souvenir, ProcessSouvenir },
            { _SpaceTraders, ProcessSpaceTraders },
            { _SpellingBee, ProcessSpellingBee },
            { _Sphere, ProcessSphere },
            { _SplittingTheLoot, ProcessSplittingTheLoot },
            { _SpotTheDifference, ProcessSpotTheDifference },
            { _StackedSequences, ProcessStackedSequences },
            { _Stars, ProcessStars },
            { _StateOfAggregation, ProcessStateOfAggregation },
            { _Stellar, ProcessStellar },
            { _StupidSlots, ProcessStupidSlots },
            { _SubscribeToPewdiepie, ProcessSubscribeToPewdiepie },
            { _SugarSkulls, ProcessSugarSkulls },
            { _Switch, ProcessSwitch },
            { _Switches, ProcessSwitches },
            { _SwitchingMaze, ProcessSwitchingMaze },
            { _SymbolCycle, ProcessSymbolCycle },
            { _SymbolicCoordinates, ProcessSymbolicCoordinates },
            { _SymbolicTasha, ProcessSymbolicTasha },
            { _Sync_125_3, ProcessSync_125_3 },
            { _Synonyms, ProcessSynonyms },
            { _Sysadmin, ProcessSysadmin },
            { _TapCode, ProcessTapCode },
            { _TashaSqueals, ProcessTashaSqueals },
            { _TenButtonColorCode, ProcessTenButtonColorCode },
            { _Tenpins, ProcessTenpins },
            { _TextField, ProcessTextField },
            { _ThinkingWires, ProcessThinkingWires },
            { _ThirdBase, ProcessThirdBase },
            { _TicTacToe, ProcessTicTacToe },
            { _Timezone, ProcessTimezone },
            { _TopsyTurvy, ProcessTopsyTurvy },
            { _TransmittedMorse, ProcessTransmittedMorse },
            { _TurtleRobot, ProcessTurtleRobot },
            { _TwoBits, ProcessTwoBits },
            { _UltimateCipher, ProcessUltimateCipher },
            { _UltimateCycle, ProcessUltimateCycle },
            { _Ultracube, ProcessUltracube },
            { _UltraStores, ProcessUltraStores },
            { _UncoloredSquares, ProcessUncoloredSquares },
            { _UncoloredSwitches, ProcessUncoloredSwitches },
            { _UnfairCipher, ProcessUnfairCipher },
            { _UnfairsRevenge, ProcessUnfairsRevenge },
            { _Unicode, ProcessUnicode },
            { _UnownCipher, ProcessUnownCipher },
            { _USAMaze, ProcessUSAMaze },
            { _V, ProcessV },
            { _VaricoloredSquares, ProcessVaricoloredSquares },
            { _Vcrcs, ProcessVcrcs },
            { _Vectors, ProcessVectors },
            { _Vexillology, ProcessVexillology },
            { _VioletCipher, ProcessVioletCipher },
            { _VisualImpairment, ProcessVisualImpairment },
            { _Wavetapping, ProcessWavetapping },
            { _WhatsOnSecond, ProcessWhatsOnSecond },
            { _WhiteCipher, ProcessWhiteCipher },
            { _WhosOnFirst, ProcessWhosOnFirst },
            { _Wire, ProcessWire },
            { _WireOrdering, ProcessWireOrdering },
            { _WireSequence, ProcessWireSequence },
            { _WolfGoatAndCabbage, ProcessWolfGoatAndCabbage },
            { _WorkingTitle, ProcessWorkingTitle },
            { _Xenocryst, ProcessXenocryst },
            { _XmORseCode, ProcessXmORseCode },
            { _Yahtzee, ProcessYahtzee },
            { _YellowArrows, ProcessYellowArrows },
            { _YellowButton, ProcessYellowButton },
            { _YellowCipher, ProcessYellowCipher },
            { _ZeroZero, ProcessZeroZero },
            { _Zoni, ProcessZoni }
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
            ix >= messages.Length ? string.Format("greater than ‘message’ length ({0})", messages.Length) :
            ix >= responses.Length ? string.Format("greater than ‘response’ length ({0})", responses.Length) : null);

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
            ix >= messages.Length ? string.Format("‘r’ is greater than ‘message’ length ({0}).", messages.Length) :
            ix >= responses.Length ? string.Format("‘r’ is greater than ‘response’ length ({0}).", responses.Length) : null);

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

        var stateCodes = mthGetStates.Invoke();
        if (stateCodes == null)
            throw new AbandonModuleException("GetAllStates() returned null.");
        if (stateCodes.Count == 0)
            throw new AbandonModuleException("GetAllStates() returned an empty list.");

        var states = stateCodes.Select(code => mthGetName.Invoke(code)).ToArray();
        var origin = mthGetName.Invoke(fldOrigin.Get());
        if (!states.Contains(origin))
            throw new AbandonModuleException("‘_originState’ was not contained in the list of all states ({0} not in: {1}).", origin, states.JoinString(", "));

        addQuestions(module, makeQuestion(question, moduleCode, correctAnswers: new[] { origin }, preferredWrongAnswers: states));
    }

    // Used by Red, Orange, Yellow, Green, Blue, Indigo, Violet, White, Gray, Black, Brown and Ultimate Cipher
    private IEnumerable<object> processColoredCiphers(KMBombModule module, string componentName, Question question, string moduleId)
    {
        var comp = GetComponent(module, componentName);

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };

        while (!solved)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(moduleId);

        var memory = GetField<string>(comp, "answer").Get();
        var answerList = GetListField<List<string>>(comp, "wordList").Get()[2].Select(str => str.ToLowerInvariant()).ToArray();
        addQuestions(module, makeQuestion(question, moduleId, formatArgs: null, correctAnswers: new[] { memory.ToLowerInvariant() }, preferredWrongAnswers: answerList));
    }

    // Used by The Hypercube and The Ultracube
    private IEnumerable<object> processHypercubeUltracube(KMBombModule module, string componentName, Question question, string moduleId)
    {
        var comp = GetComponent(module, componentName);
        var rotations = GetStaticField<string[]>(comp.GetType(), "_rotationNames").Get();
        var sequence = GetArrayField<int>(comp, "_rotations").Get(expectedLength: 5, validator: rot => rot < 0 || rot >= rotations.Length ? string.Format("expected range 0–{0}", rotations.Length - 1) : null);

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
}

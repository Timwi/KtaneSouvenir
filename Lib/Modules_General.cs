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
    const string Version = "5.7";

    void Awake()
    {
        _moduleProcessors = new Dictionary<string, ModuleHandlerInfo>()
        {
            ["0"] = (Process0, "0", "Anonymous"),
            ["OneThousandWords"] = (Process1000Words, "1000 Words", "BigCrunch22"),
            ["100LevelsOfDefusal"] = (Process100LevelsOfDefusal, "100 Levels of Defusal", "Espik"),
            ["TheOneTwoThreeGame"] = (Process123Game, "1, 2, 3 Game, The", "Anonymous"),
            ["1DChess"] = (Process1DChess, "1D Chess", "Emik"),
            ["spwiz3DMaze"] = (Process3DMaze, "3D Maze", "Timwi"),
            ["3DTapCodeModule"] = (Process3DTapCode, "3D Tap Code", "TasThiluna"),
            ["3dTunnels"] = (Process3DTunnels, "3D Tunnels", "Timwi"),
            ["threeLEDsModule"] = (Process3LEDs, "3 LEDs", "Timwi"),
            ["threeNPlusOne"] = (Process3NPlus1, "3N+1", "Hawker"),
            ["64"] = (Process64, "64", "Kuro"),
            ["7"] = (Process7, "7", "VFlyer"),
            ["GSNineBall"] = (Process9Ball, "9-Ball", "GhostSalt"),
            ["GSAbyss"] = (ProcessAbyss, "Abyss", "VFlyer"),
            ["accumulation"] = (ProcessAccumulation, "Accumulation", "luisdiogo98"),
            ["spwizAdventureGame"] = (ProcessAdventureGame, "Adventure Game", "Timwi"),
            ["affineCycle"] = (ProcessAffineCycle, "Affine Cycle", "TasThiluna"),
            ["LetterModule"] = (ProcessALetter, "A Letter", "Sierra"),
            ["alfa_bravo"] = (ProcessAlfaBravo, "Alfa-Bravo", "NickLatkovich"),
            ["algebra"] = (ProcessAlgebra, "Algebra", "Timwi"),
            ["algorithmia"] = (ProcessAlgorithmia, "Algorithmia", "tandyCake"),
            ["alphabeticalRuling"] = (ProcessAlphabeticalRuling, "Alphabetical Ruling", "Timwi"),
            ["alphabetNumbers"] = (ProcessAlphabetNumbers, "Alphabet Numbers", "Kuro"),
            ["AlphabetTiles"] = (ProcessAlphabetTiles, "Alphabet Tiles", "BigCrunch22"),
            ["alphaBits"] = (ProcessAlphaBits, "Alpha-Bits", "Timwi"),
            ["amusementParks"] = (ProcessAmusementParks, "Amusement Parks", "Anonymous"),
            ["AngelHernandezModule"] = (ProcessAngelHernandez, "Ángel Hernández", "Quinn Wuest"),
            ["TheArena"] = (ProcessArena, "Arena, The", "Hawker"),
            ["arithmelogic"] = (ProcessArithmelogic, "Arithmelogic", "JerryEris"),
            ["asciiMaze"] = (ProcessASCIIMaze, "ASCII Maze", "Timwi"),
            ["ASquareModule"] = (ProcessASquare, "A Square", "Quinn Wuest"),
            ["lgndAudioMorse"] = (ProcessAudioMorse, "Audio Morse", "Anonymous"),
            ["AzureButtonModule"] = (ProcessAzureButton, "Azure Button, The", "Timwi"),
            ["bakery"] = (ProcessBakery, "Bakery", "TasThiluna"),
            ["bamboozledAgain"] = (ProcessBamboozledAgain, "Bamboozled Again", "kavinkul"),
            ["bamboozlingButton"] = (ProcessBamboozlingButton, "Bamboozling Button", "TasThiluna"),
            ["GSBarCharts"] = (ProcessBarCharts, "Bar Charts", "Hawker"),
            ["BarcodeCipherModule"] = (ProcessBarcodeCipher, "Barcode Cipher", "Brawlboxgaming"),
            ["BartendingModule"] = (ProcessBartending, "Bartending", "Timwi"),
            ["beans"] = (ProcessBeans, "Beans", "Anonymous"),
            ["beanSprouts"] = (ProcessBeanSprouts, "Bean Sprouts", "Anonymous"),
            ["bigBean"] = (ProcessBigBean, "Big Bean", "Anonymous"),
            ["BigCircle"] = (ProcessBigCircle, "Big Circle", "CaitSith2"),
            ["Binary"] = (ProcessBinary, "Binary", "BigCrunch22"),
            ["BinaryLeds"] = (ProcessBinaryLEDs, "Binary LEDs", "Timwi"),
            ["binary_shift"] = (ProcessBinaryShift, "Binary Shift", "NickLatkovich"),
            ["BitmapsModule"] = (ProcessBitmaps, "Bitmaps", "Timwi"),
            ["blackCipher"] = (ProcessBlackCipher, "Black Cipher", "BigCrunch22"),
            ["BlindMaze"] = (ProcessBlindMaze, "Blind Maze", "Timwi"),
            ["blinkingNotes"] = (ProcessBlinkingNotes, "Blinking Notes", "Anonymous"),
            ["blinkstopModule"] = (ProcessBlinkstop, "Blinkstop", "Kuro"),
            ["blockbusters"] = (ProcessBlockbusters, "Blockbusters", "luisdiogo98"),
            ["blueArrowsModule"] = (ProcessBlueArrows, "Blue Arrows", "kavinkul"),
            ["BlueButtonModule"] = (ProcessBlueButton, "Blue Button, The", "Timwi"),
            ["blueCipher"] = (ProcessBlueCipher, "Blue Cipher", "BigCrunch22"),
            ["ksmBobBarks"] = (ProcessBobBarks, "Bob Barks", "Kaito Sinclaire"),
            ["boggle"] = (ProcessBoggle, "Boggle", "luisdiogo98"),
            ["bombDiffusal"] = (ProcessBombDiffusal, "Bomb Diffusal", "Kuro"),
            ["boobTubeModule"] = (ProcessBoobTube, "Boob Tube", "Anonymous"),
            ["BookOfMarioModule"] = (ProcessBookOfMario, "Book of Mario", "Hawker"),
            ["booleanWires"] = (ProcessBooleanWires, "Boolean Wires", "Kuro"),
            ["boomtarTheGreat"] = (ProcessBoomtarTheGreat, "Boomtar the Great", "Anonymous"),
            ["GSBottomGear"] = (ProcessBottomGear, "Bottom Gear", "Anonymous"),
            ["borderedKeys"] = (ProcessBorderedKeys, "Bordered Keys", "Hawker"),
            ["boxing"] = (ProcessBoxing, "Boxing", "Timwi"),
            ["BrailleModule"] = (ProcessBraille, "Braille", "Timwi"),
            ["breakfastEgg"] = (ProcessBreakfastEgg, "Breakfast Egg", "tandyCake"),
            ["BrokenButtonsModule"] = (ProcessBrokenButtons, "Broken Buttons", "Timwi"),
            ["BrokenGuitarChordsModule"] = (ProcessBrokenGuitarChords, "Broken Guitar Chords", "Kuro"),
            ["brownCipher"] = (ProcessBrownCipher, "Brown Cipher", "Marksam"),
            ["brushStrokes"] = (ProcessBrushStrokes, "Brush Strokes", "luisdiogo98"),
            ["TheBulbModule"] = (ProcessBulb, "Bulb, The", "Timwi"),
            ["burgerAlarm"] = (ProcessBurgerAlarm, "Burger Alarm", "Kuro"),
            ["burglarAlarm"] = (ProcessBurglarAlarm, "Burglar Alarm", "Timwi"),
            ["BigButton"] = (ProcessButton, "Button, The", "Andrio Celos"),
            ["buttonSequencesModule"] = (ProcessButtonSequence, "Button Sequence", "Timwi"),
            ["CactusPConundrum"] = (ProcessCactisConundrum, "Cacti’s Conundrum", "Anonymous"),
            ["caesarCycle"] = (ProcessCaesarCycle, "Caesar Cycle", "TasThiluna"),
            ["caesarPsycho"] = (ProcessCaesarPsycho, "Caesar Psycho", "Quinn Wuest"),
            ["calendar"] = (ProcessCalendar, "Calendar", "Timwi"),
            ["caRPS"] = (ProcessCARPS, "CA-RPS", "Anonymous"),
            ["cartinese"] = (ProcessCartinese, "Cartinese", "Timwi"),
            ["catchphrase"] = (ProcessCatchphrase, "Catchphrase", "GoodHood"),
            ["challengeAndContact"] = (ProcessChallengeAndContact, "Challenge & Contact", "luisdiogo98"),
            ["characterCodes"] = (ProcessCharacterCodes, "Character Codes", "NickLatkovich"),
            ["characterShift"] = (ProcessCharacterShift, "Character Shift", "Kuro"),
            ["characterSlots"] = (ProcessCharacterSlots, "Character Slots", "Hawker"),
            ["CheapCheckoutModule"] = (ProcessCheapCheckout, "Cheap Checkout", "Timwi"),
            ["cheepCheckout"] = (ProcessCheepCheckout, "Cheep Checkout", "BigCrunch22"),
            ["ChessModule"] = (ProcessChess, "Chess", "Timwi"),
            ["chineseCounting"] = (ProcessChineseCounting, "Chinese Counting", "TasThiluna"),
            ["ChordQualities"] = (ProcessChordQualities, "Chord Qualities", "Timwi"),
            ["clockCounter"] = (ProcessClockCounter, "↻↺", "Anonymous"),
            ["theCodeModule"] = (ProcessCode, "Code, The", "luisdiogo98"),
            ["codenames"] = (ProcessCodenames, "Codenames", "TasThiluna"),
            ["coffeebucks"] = (ProcessCoffeebucks, "Coffeebucks", "luisdiogo98"),
            ["Coinage"] = (ProcessCoinage, "Coinage", "Emik"),
            ["colorAddition"] = (ProcessColorAddition, "Color Addition", "VFlyer"),
            ["ColorBrailleModule"] = (ProcessColorBraille, "Color Braille", "Timwi"),
            ["Color Decoding"] = (ProcessColorDecoding, "Color Decoding", "Timwi"),
            ["lgndColoredKeys"] = (ProcessColoredKeys, "Colored Keys", "luisdiogo98"),
            ["ColoredSquaresModule"] = (ProcessColoredSquares, "Colored Squares", "Timwi"),
            ["ColoredSwitchesModule"] = (ProcessColoredSwitches, "Colored Switches", "Timwi"),
            ["ColorMorseModule"] = (ProcessColorMorse, "Color Morse", "Timwi"),
            ["colorOneTwo"] = (ProcessColorOneTwo, "Color One Two", "Anonymous"),
            ["colors_maximization"] = (ProcessColorsMaximization, "Colors Maximization", "NickLatkovich"),
            ["ColouredCubes"] = (ProcessColouredCubes, "Coloured Cubes", "Kuro"),
            ["colouredCylinder"] = (ProcessColouredCylinder, "Coloured Cylinder", "Anonymous"),
            ["ColourFlash"] = (ProcessColourFlash, "Colour Flash", "LotsOfS"),
            ["ConcentrationModule"] = (ProcessConcentration, "Concentration", "Anonymous"),
            ["conditionalButtons"] = (ProcessConditionalButtons, "Conditional Buttons", "Hawker"),
            ["ConnectedMonitorsModule"] = (ProcessConnectedMonitors, "Connected Monitors", "Anonymous"),
            ["graphModule"] = (ProcessConnectionCheck, "Connection Check", "Anonymous"),
            ["CoordinatesModule"] = (ProcessCoordinates, "Coordinates", "Timwi"),
            ["coralCipher"] = (ProcessCoralCipher, "Coral Cipher", "Timwi"),
            ["CornersModule"] = (ProcessCorners, "Corners", "Timwi"),
            ["cornflowerCipher"] = (ProcessCornflowerCipher, "Cornflower Cipher", "Timwi"),
            ["CosmicModule"] = (ProcessCosmic, "Cosmic", "BigCrunch22"),
            ["GSCrazyHamburger"] = (ProcessCrazyHamburger, "Crazy Hamburger", "noting3548"),
            ["CrazyMazeModule"] = (ProcessCrazyMaze, "Crazy Maze", "Timwi"),
            ["creamCipher"] = (ProcessCreamCipher, "Cream Cipher", "Timwi"),
            ["CreationModule"] = (ProcessCreation, "Creation", "CaitSith2"),
            ["crimsonCipher"] = (ProcessCrimsonCipher, "Crimson Cipher", "Timwi"),
            ["CrittersModule"] = (ProcessCritters, "Critters", "Eltrick"),
            ["CruelBinary"] = (ProcessCruelBinary, "Cruel Binary", "Kuro"),
            ["CruelKeypads"] = (ProcessCruelKeypads, "Cruel Keypads", "Kuro"),
            ["the_cRule"] = (ProcessCRule, "cRule, The", "Timwi"),
            ["crypticCycle"] = (ProcessCrypticCycle, "Cryptic Cycle", "TasThiluna"),
            ["GSCrypticKeypad"] = (ProcessCrypticKeypad, "Cryptic Keypad", "Timwi"),
            ["cube"] = (ProcessCube, "Cube, The", "luisdiogo98"),
            ["CursedDoubleOhModule"] = (ProcessCursedDoubleOh, "Cursed Double-Oh", "Kuro"),
            ["xelCustomerIdentification"] = (ProcessCustomerIdentification, "Customer Identification", "Hawker"),
            ["CyanButtonModule"] = (ProcessCyanButton, "Cyan Button, The", "Quinn Wuest"),
            ["DACH"] = (ProcessDACHMaze, "DACH Maze", "Timwi"),
            ["deafAlleyModule"] = (ProcessDeafAlley, "Deaf Alley", "BigCrunch22"),
            ["deckOfManyThings"] = (ProcessDeckOfManyThings, "Deck of Many Things, The", "luisdiogo98"),
            ["DecoloredSquaresModule"] = (ProcessDecoloredSquares, "Decolored Squares", "luisdiogo98"),
            ["DecolourFlashModule"] = (ProcessDecolourFlash, "Decolour Flash", "Timwi"),
            ["DenialDisplaysModule"] = (ProcessDenialDisplays, "Denial Displays", "Quinn Wuest"),
            ["Detonato"] = (ProcessDetoNATO, "DetoNATO", "Hawker"),
            ["devilishEggs"] = (ProcessDevilishEggs, "Devilish Eggs", "Timwi"),
            ["xelDialtones"] = (ProcessDialtones, "Dialtones", "Anonymous"),
            ["digisibility"] = (ProcessDigisibility, "Digisibility", "tandyCake"),
            ["digitString"] = (ProcessDigitString, "Digit String", "GoodHood"),
            ["dimensionDisruption"] = (ProcessDimensionDisruption, "Dimension Disruption", "Hawker"),
            ["directionalButton"] = (ProcessDirectionalButton, "Directional Button", "Hawker"),
            ["DiscoloredSquaresModule"] = (ProcessDiscoloredSquares, "Discolored Squares", "luisdiogo98"),
            ["disorderedKeys"] = (ProcessDisorderedKeys, "Disordered Keys", "Hawker"),
            ["divisibleNumbers"] = (ProcessDivisibleNumbers, "Divisible Numbers", "shortc1rcuit"),
            ["doofenshmirtzEvilIncModule"] = (ProcessDoofenshmirtzEvilInc, "Doofenshmirtz Evil Inc.", "Anonymous"),
            ["doubleArrows"] = (ProcessDoubleArrows, "Double Arrows", "Anonymous"),
            ["doubleColor"] = (ProcessDoubleColor, "Double Color", "luisdiogo98"),
            ["doubleDigitsModule"] = (ProcessDoubleDigits, "Double Digits", "Quinn Wuest"),
            ["doubleExpert"] = (ProcessDoubleExpert, "Double Expert", "Kuro"),
            ["doubleListening"] = (ProcessDoubleListening, "Double Listening", "Anonymous"),
            ["DoubleOhModule"] = (ProcessDoubleOh, "Double-Oh", "Timwi"),
            ["doubleScreenModule"] = (ProcessDoubleScreen, "Double Screen", "Anonymous"),
            ["DrDoctorModule"] = (ProcessDrDoctor, "Dr. Doctor", "Timwi"),
            ["ksmDreamcipher"] = (ProcessDreamcipher, "Dreamcipher", "BigCrunch22"),
            ["theDuck"] = (ProcessDuck, "Duck, The", "Kuro"),
            ["dumbWaiters"] = (ProcessDumbWaiters, "Dumb Waiters", "BigCrunch22"),
            ["EarthboundModule"] = (ProcessEarthbound, "Earthbound", "Hawker"),
            ["eeBgnilleps"] = (ProcessEeBgnillepS, "eeB gnillepS", "BigCrunch22"),
            ["eight"] = (ProcessEight, "Eight", "NickLatkovich"),
            ["elderFuthark"] = (ProcessElderFuthark, "Elder Futhark", "Goofy"),
            ["emoji"] = (ProcessEmoji, "Emoji", "Anonymous"),
            ["enaCipher"] = (ProcessEnaCipher, "ƎNA Cipher", "KiloBites"),
            ["EncryptedDice"] = (ProcessEncryptedDice, "Encrypted Dice", "Kuro"),
            ["EncryptedEquationsModule"] = (ProcessEncryptedEquations, "Encrypted Equations", "Timwi"),
            ["encryptedHangman"] = (ProcessEncryptedHangman, "Encrypted Hangman", "Timwi"),
            ["encryptedMaze"] = (ProcessEncryptedMaze, "Encrypted Maze", "Timwi"),
            ["EncryptedMorse"] = (ProcessEncryptedMorse, "Encrypted Morse", "luisdiogo98"),
            ["encryptionBingo"] = (ProcessEncryptionBingo, "Encryption Bingo", "TasThiluna"),
            ["enigmaCycle"] = (ProcessEnigmaCycle, "Enigma Cycle", "Timwi"),
            ["GSEntryNumberFour"] = (ProcessEntryNumberFour, "Entry Number Four", "GhostSalt"),
            ["GSEntryNumberOne"] = (ProcessEntryNumberOne, "Entry Number One", "GhostSalt"),
            ["epelleMoiCa"] = (ProcessEpelleMoiCa, "Épelle-moi Ça", "Quinn Wuest"),
            ["equationsXModule"] = (ProcessEquationsX, "Equations X", "kavinkul"),
            ["errorCodes"] = (ProcessErrorCodes, "Error Codes", "Hawker"),
            ["etterna"] = (ProcessEtterna, "Etterna", "Emik"),
            ["exoplanets"] = (ProcessExoplanets, "Exoplanets", "Brawlboxgaming"),
            ["factoringMaze"] = (ProcessFactoringMaze, "Factoring Maze", "Eltrick"),
            ["factoryMaze"] = (ProcessFactoryMaze, "Factory Maze", "luisdiogo98"),
            ["fastMath"] = (ProcessFastMath, "Fast Math", "Timwi"),
            ["GSFaultyButtons"] = (ProcessFaultyButtons, "Faulty Buttons", "Kuro"),
            ["faultyrgbMaze"] = (ProcessFaultyRGBMaze, "Faulty RGB Maze", "kavinkul"),
            ["DateFinder"] = (ProcessFindTheDate, "Find The Date", "Hawker"),
            ["FiveLetterWords"] = (ProcessFiveLetterWords, "Five Letter Words", "Kuro"),
            ["fizzBuzzModule"] = (ProcessFizzBuzz, "FizzBuzz", "Kuro"),
            ["FlagsModule"] = (ProcessFlags, "Flags", "Timwi"),
            ["flashingArrowsModule"] = (ProcessFlashingArrows, "Flashing Arrows", "VFlyer"),
            ["flashingLights"] = (ProcessFlashingLights, "Flashing Lights", "luisdiogo98"),
            ["FlavorText"] = (ProcessFlavorText, "Flavor Text", "Hawker"),
            ["FlavorTextCruel"] = (ProcessFlavorTextEX, "Flavor Text EX", "Hawker"),
            ["flyswatting"] = (ProcessFlyswatting, "Flyswatting", "tandyCake"),
            ["FollowMe"] = (ProcessFollowMe, "Follow Me", "Kuro"),
            ["forestCipher"] = (ProcessForestCipher, "Forest Cipher", "Timwi"),
            ["ForgetAnyColor"] = (ProcessForgetAnyColor, "Forget Any Color", "Kuro"),
            ["HexiEvilFMN"] = (ProcessForgetEverything, "Forget Everything", "Kuro"),
            ["forgetMe"] = (ProcessForgetMe, "Forget Me", "tandyCake"),
            ["MemoryV2"] = (ProcessForgetMeNot, "Forget Me Not", "Kuro"),
            ["ForgetMeNow"] = (ProcessForgetMeNow, "Forget Me Now", "Kuro"),
            ["ForgetsUltimateShowdownModule"] = (ProcessForgetsUltimateShowdown, "Forget’s Ultimate Showdown", "Marksam"),
            ["ForgetTheColors"] = (ProcessForgetTheColors, "Forget The Colors", "Kuro"),
            ["forgetThis"] = (ProcessForgetThis, "Forget This", "Kuro"),
            ["freeParking"] = (ProcessFreeParking, "Free Parking", "luisdiogo98"),
            ["qFunctions"] = (ProcessFunctions, "Functions", "JerryEris"),
            ["FuseBox"] = (ProcessFuseBox, "Fuse Box, The", "Anonymous"),
            ["lgndGadgetronVendor"] = (ProcessGadgetronVendor, "Gadgetron Vendor", "Kuro"),
            ["GameOfLifeCruel"] = (ProcessGameOfLifeCruel, "Game of Life Cruel", "GhostSalt"),
            ["TheGamepadModule"] = (ProcessGamepad, "Gamepad, The", "Timwi"),
            ["garfieldKart"] = (ProcessGarfieldKart, "Garfield Kart", "Hawker"),
            ["theGarnetThief"] = (ProcessGarnetThief, "Garnet Thief, The", "Hawker"),
            ["ghostMovement"] = (ProcessGhostMovement, "Ghost Movement", "Anonymous"),
            ["Girlfriend"] = (ProcessGirlfriend, "Girlfriend", "Hawker"),
            ["GlitchedButtonModule"] = (ProcessGlitchedButton, "Glitched Button, The", "Timwi"),
            ["goofysgame"] = (ProcessGoofysGame, "Goofy’s Game", "Anonymous"),
            ["GrayButtonModule"] = (ProcessGrayButton, "Gray Button, The", "Timwi"),
            ["grayCipher"] = (ProcessGrayCipher, "Gray Cipher", "BigCrunch22"),
            ["greatVoid"] = (ProcessGreatVoid, "Great Void, The", "Marksam"),
            ["greenArrowsModule"] = (ProcessGreenArrows, "Green Arrows", "kavinkul"),
            ["GreenButtonModule"] = (ProcessGreenButton, "Green Button, The", "Timwi"),
            ["greenCipher"] = (ProcessGreenCipher, "Green Cipher", "BigCrunch22"),
            ["GridlockModule"] = (ProcessGridLock, "Gridlock", "CaitSith2"),
            ["groceryStore"] = (ProcessGroceryStore, "Grocery Store", "BigCrunch22"),
            ["gryphons"] = (ProcessGryphons, "Gryphons", "JerryEris"),
            ["GuessWho"] = (ProcessGuessWho, "Guess Who?", "BigCrunch22"),
            ["gyromaze"] = (ProcessGyromaze, "Gyromaze", "Anonymous"),
            ["Averageh"] = (ProcessH, "h", "Hawker"),
            ["halliGalli"] = (ProcessHalliGalli, "Halli Galli", "Anonymous"),
            ["hereditaryBaseNotationModule"] = (ProcessHereditaryBaseNotation, "Hereditary Base Notation", "kavinkul"),
            ["hexabutton"] = (ProcessHexabutton, "Hexabutton, The", "luisdiogo98"),
            ["HexamazeModule"] = (ProcessHexamaze, "Hexamaze", "Timwi"),
            ["hexOrbits"] = (ProcessHexOrbits, "hexOrbits", "Anonymous"),
            ["hexOS"] = (ProcessHexOS, "hexOS", "Emik"),
            ["lgndHiddenColors"] = (ProcessHiddenColors, "Hidden Colors", "TasThiluna"),
            ["theHiddenValue"] = (ProcessHiddenValue, "HiddenValue, The", "Anonymous"),
            ["ksmHighScore"] = (ProcessHighScore, "High Score, The", "Hawker"),
            ["hillCycle"] = (ProcessHillCycle, "Hill Cycle", "TasThiluna"),
            ["hinges"] = (ProcessHinges, "Hinges", "Kuro"),
            ["HogwartsModule"] = (ProcessHogwarts, "Hogwarts", "Timwi"),
            ["KritHoldUps"] = (ProcessHoldUps, "Hold Ups", "BigCrunch22"),
            ["homophones"] = (ProcessHomophones, "Homophones", "VFlyer"),
            ["horribleMemory"] = (ProcessHorribleMemory, "Horrible Memory", "luisdiogo98"),
            ["HumanResourcesModule"] = (ProcessHumanResources, "Human Resources", "Timwi"),
            ["hunting"] = (ProcessHunting, "Hunting", "Timwi"),
            ["TheHypercubeModule"] = (ProcessHypercube, "Hypercube, The", "luisdiogo98"),
            ["HyperForget"] = (ProcessHyperforget, "Hyperforget", "Anonymous"),
            ["hyperlink"] = (ProcessHyperlink, "Hyperlink, The", "Espik"),
            ["iceCreamModule"] = (ProcessIceCream, "Ice Cream", "CaitSith2"),
            ["identificationCrisis"] = (ProcessIdentificationCrisis, "Identification Crisis", "TasThiluna"),
            ["identityParade"] = (ProcessIdentityParade, "Identity Parade", "Timwi"),
            ["impostor"] = (ProcessImpostor, "Impostor, The", "Kuro"),
            ["indigoCipher"] = (ProcessIndigoCipher, "Indigo Cipher", "BigCrunch22"),
            ["InfiniteLoop"] = (ProcessInfiniteLoop, "Infinite Loop", "Eltrick"),
            ["ingredients"] = (ProcessIngredients, "Ingredients", "Timwi"),
            ["InnerConnectionsModule"] = (ProcessInnerConnections, "Inner Connections", "Brawlboxgaming"),
            ["interpunct"] = (ProcessInterpunct, "Interpunct", "Eltrick"),
            ["ipa"] = (ProcessIPA, "IPA", "Timwi"),
            ["iPhone"] = (ProcessiPhone, "iPhone, The", "luisdiogo98"),
            ["jenga"] = (ProcessJenga, "Jenga", "tandyCake"),
            ["jewelVault"] = (ProcessJewelVault, "Jewel Vault, The", "luisdiogo98"),
            ["jumbleCycle"] = (ProcessJumbleCycle, "Jumble Cycle", "TasThiluna"),
            ["JuxtacoloredSquaresModule"] = (ProcessJuxtacoloredSquares, "Juxtacolored Squares", "Kuro"),
            ["KanjiModule"] = (ProcessKanji, "Kanji", "Kuro"),
            ["TheKanyeEncounter"] = (ProcessKanyeEncounter, "Kanye Encounter, The", "tandyCake"),
            ["keypadCombinations"] = (ProcessKeypadCombination, "Keypad Combination", "Hawker"),
            ["keypadMagnified"] = (ProcessKeypadMagnified, "Keypad Magnified", "tandyCake"),
            ["KeypadMaze"] = (ProcessKeypadMaze, "Keypad Maze", "Anonymous"),
            ["keypadSeq"] = (ProcessKeypadSequence, "Keypad Sequence", "Anonymous"),
            ["xtrkeywords"] = (ProcessKeywords, "Keywords", "Kuro"),
            ["KnowYourWay"] = (ProcessKnowYourWay, "Know Your Way", "Kuro"),
            ["kookyKeypadModule"] = (ProcessKookyKeypad, "Kooky Keypad", "Anonymous"),
            ["KudosudokuModule"] = (ProcessKudosudoku, "Kudosudoku", "Timwi"),
            ["Kuro"] = (ProcessKuro, "Kuro", "Hawker"),
            ["labyrinth"] = (ProcessLabyrinth, "Labyrinth, The", "Anonymous"),
            ["ladderLottery"] = (ProcessLadderLottery, "Ladder Lottery", "Hawker"),
            ["ladders"] = (ProcessLadders, "Ladders", "tandyCake"),
            ["GSLangtonsAnteater"] = (ProcessLangtonsAnteater, "Langton’s Anteater", "Kuro"),
            ["lasers"] = (ProcessLasers, "Lasers", "luisdiogo98"),
            ["LEDEnc"] = (ProcessLEDEncryption, "LED Encryption", "CaitSith2"),
            ["ledGrid"] = (ProcessLEDGrid, "LED Grid", "Hawker"),
            ["lgndLEDMath"] = (ProcessLEDMath, "LED Math", "TasThiluna"),
            ["leds"] = (ProcessLEDs, "LEDs", "tandyCake"),
            ["LEGOModule"] = (ProcessLEGOs, "LEGOs", "luisdiogo98"),
            ["letterMath"] = (ProcessLetterMath, "Letter Math", "Quinn Wuest"),
            ["LightBulbs"] = (ProcessLightBulbs, "Light Bulbs", "Kuro"),
            ["Linq"] = (ProcessLinq, "Linq", "Emik"),
            ["LionsShareModule"] = (ProcessLionsShare, "Lion’s Share", "Timwi"),
            ["Listening"] = (ProcessListening, "Listening", "Timwi"),
            ["logicalButtonsModule"] = (ProcessLogicalButtons, "Logical Buttons", "Timwi"),
            ["logicGates"] = (ProcessLogicGates, "Logic Gates", "Timwi"),
            ["lgndLombaxCubes"] = (ProcessLombaxCubes, "Lombax Cubes", "Marksam"),
            ["londonUnderground"] = (ProcessLondonUnderground, "London Underground, The", "Timwi"),
            ["LongWords"] = (ProcessLongWords, "Long Words", "GoodHood"),
            ["MadMemory"] = (ProcessMadMemory, "Mad Memory", "Kuro"),
            ["MafiaModule"] = (ProcessMafia, "Mafia", "Timwi"),
            ["magentaCipher"] = (ProcessMagentaCipher, "Magenta Cipher", "Timwi"),
            ["MahjongModule"] = (ProcessMahjong, "Mahjong", "River"),
            ["mainpage"] = (ProcessMainPage, "Main Page", "ObjectsCountries"),
            ["MandMs"] = (ProcessMandMs, "M&Ms", "TasThiluna"),
            ["MandNs"] = (ProcessMandNs, "M&Ns", "TasThiluna"),
            ["MaritimeFlagsModule"] = (ProcessMaritimeFlags, "Maritime Flags", "Timwi"),
            ["MaritimeSemaphoreModule"] = (ProcessMaritimeSemaphore, "Maritime Semaphore", "Anonymous"),
            ["MaroonButtonModule"] = (ProcessMaroonButton, "Maroon Button, The", "Anonymous"),
            ["maroonCipher"] = (ProcessMaroonCipher, "Maroon Cipher", "Timwi"),
            ["mashematics"] = (ProcessMashematics, "Mashematics", "Marksam"),
            ["masterTape"] = (ProcessMasterTapes, "Master Tapes", "Kuro"),
            ["matchRefereeing"] = (ProcessMatchRefereeing, "Match Refereeing", "Quinn Wuest"),
            ["mathem"] = (ProcessMathEm, "Math ’em", "tandyCake"),
            ["matrix"] = (ProcessMatrix, "Matrix, The", "BigCrunch22"),
            ["Maze"] = (ProcessMaze, "Maze", "Andrio Celos"),
            ["maze3"] = (ProcessMaze3, "Maze³", "luisdiogo98"),
            ["GSMazeIdentification"] = (ProcessMazeIdentification, "Maze Identification", "GhostSalt"),
            ["mazematics"] = (ProcessMazematics, "Mazematics", "luisdiogo98"),
            ["MazeScrambler"] = (ProcessMazeScrambler, "Maze Scrambler", "luisdiogo98"),
            ["GSMazeseeker"] = (ProcessMazeseeker, "Mazeseeker", "GhostSalt"),
            ["mazeSwap"] = (ProcessMazeSwap, "Maze Swap", "Anonymous"),
            ["megaMan2"] = (ProcessMegaMan2, "Mega Man 2", "Goofy"),
            ["melodySequencer"] = (ProcessMelodySequencer, "Melody Sequencer", "Goofy"),
            ["memorableButtons"] = (ProcessMemorableButtons, "Memorable Buttons", "Timwi"),
            ["Memory"] = (ProcessMemory, "Memory", "Andrio Celos"),
            ["memoryWires"] = (ProcessMemoryWires, "Memory Wires", "Kuro"),
            ["metamorse"] = (ProcessMetamorse, "Metamorse", "tandyCake"),
            ["metapuzzle"] = (ProcessMetapuzzle, "Metapuzzle", "GoodHood"),
            ["Microcontroller"] = (ProcessMicrocontroller, "Microcontroller", "Timwi"),
            ["MinesweeperModule"] = (ProcessMinesweeper, "Minesweeper", "CaitSith2"),
            ["mirror"] = (ProcessMirror, "Mirror", "Timwi"),
            ["misterSoftee"] = (ProcessMisterSoftee, "Mister Softee", "TasThiluna"),
            ["mixometer"] = (ProcessMixometer, "Mixometer", "Hawker"),
            ["modernCipher"] = (ProcessModernCipher, "Modern Cipher", "luisdiogo98"),
            ["moduleListening"] = (ProcessModuleListening, "Module Listening", "TasThiluna"),
            ["ModuleMaze"] = (ProcessModuleMaze, "Module Maze", "River"),
            ["moduleMovements"] = (ProcessModuleMovements, "Module Movements", "Hawker"),
            ["MoneyGame"] = (ProcessMoneyGame, "MoneyGame", "Anonymous"),
            ["monsplodeFight"] = (ProcessMonsplodeFight, "Monsplode, Fight!", "Timwi"),
            ["monsplodeCards"] = (ProcessMonsplodeTradingCards, "Monsplode Trading Cards", "Timwi"),
            ["moon"] = (ProcessMoon, "Moon, The", "Timwi"),
            ["MoreCode"] = (ProcessMoreCode, "More Code", "TasThiluna"),
            ["MorseAMaze"] = (ProcessMorseAMaze, "Morse-A-Maze", "CaitSith2"),
            ["morseButtons"] = (ProcessMorseButtons, "Morse Buttons", "luisdiogo98"),
            ["MorseV2"] = (ProcessMorsematics, "Morsematics", "Timwi"),
            ["MorseWar"] = (ProcessMorseWar, "Morse War", "Timwi"),
            ["MouseInTheMaze"] = (ProcessMouseInTheMaze, "Mouse in the Maze", "Timwi"),
            ["mSeq"] = (ProcessMSeq, "M-Seq", "tandyCake"),
            ["MssngvWls"] = (ProcessMssngvWls, "Mssngv Wls", "Anonymous"),
            ["R4YMultiColoredSwitches"] = (ProcessMulticoloredSwitches, "Multicolored Switches", "Timwi"),
            ["murder"] = (ProcessMurder, "Murder", "Timwi"),
            ["mysterymodule"] = (ProcessMysteryModule, "Mystery Module", "Timwi"),
            ["MysticSquareModule"] = (ProcessMysticSquare, "Mystic Square", "CaitSith2"),
            ["nameCodes"] = (ProcessNameCodes, "Name Codes", "tandyCake"),
            ["NamingConventions"] = (ProcessNamingConventions, "Naming Conventions", "Anonymous"),
            ["NandMs"] = (ProcessNandMs, "N&Ms", "TasThiluna"),
            ["NandNs"] = (ProcessNandNs, "N&Ns", "Anonymous"),
            ["NavigationDeterminationModule"] = (ProcessNavigationDetermination, "Navigation Determination", "Quinn Wuest"),
            ["navinums"] = (ProcessNavinums, "Navinums", "Timwi"),
            ["NavyButtonModule"] = (ProcessNavyButton, "Navy Button, The", "Timwi"),
            ["necronomicon"] = (ProcessNecronomicon, "Necronomicon, The", "luisdiogo98"),
            ["Negativity"] = (ProcessNegativity, "Negativity", "VFlyer"),
            ["neutralization"] = (ProcessNeutralization, "Neutralization", "Timwi"),
            ["NextInLine"] = (ProcessNextInLine, "Next In Line", "Anonymous"),
            ["nonverbalSimon"] = (ProcessNonverbalSimon, "❖", "Anonymous"),
            ["NotColoredSquaresModule"] = (ProcessNotColoredSquares, "Not Colored Squares", "Kuro"),
            ["NotColoredSwitchesModule"] = (ProcessNotColoredSwitches, "Not Colored Switches", "Quinn Wuest"),
            ["notColourFlash"] = (ProcessNotColourFlash, "Not Colour Flash", "Anonymous"),
            ["notConnectionCheck"] = (ProcessNotConnectionCheck, "Not Connection Check", "Quinn Wuest"),
            ["notCoordinates"] = (ProcessNotCoordinates, "Not Coordinates", "Quinn Wuest"),
            ["NotDoubleOhModule"] = (ProcessNotDoubleOh, "Not Double-Oh", "Anonymous"),
            ["NotKeypad"] = (ProcessNotKeypad, "Not Keypad", "Andrio Celos"),
            ["NotMaze"] = (ProcessNotMaze, "Not Maze", "Andrio Celos"),
            ["NotMorseCode"] = (ProcessNotMorseCode, "Not Morse Code", "Andrio Celos"),
            ["notMorsematics"] = (ProcessNotMorsematics, "Not Morsematics", "Quinn Wuest"),
            ["notMurder"] = (ProcessNotMurder, "Not Murder", "Quinn Wuest"),
            ["notNumberPad"] = (ProcessNotNumberPad, "Not Number Pad", "Quinn Wuest"),
            ["NotPassword"] = (ProcessNotPassword, "Not Password", "Anonymous"),
            ["NotPerspectivePegsModule"] = (ProcessNotPerspectivePegs, "Not Perspective Pegs", "Quinn Wuest"),
            ["notPianoKeys"] = (ProcessNotPianoKeys, "Not Piano Keys", "tandyCake"),
            ["notRedArrowsModule"] = (ProcessNotRedArrows, "Not Red Arrows", "Anonymous"),
            ["NotSimaze"] = (ProcessNotSimaze, "Not Simaze", "Andrio Celos"),
            ["notTextField"] = (ProcessNotTextField, "Not Text Field", "tandyCake"),
            ["notTheBulb"] = (ProcessNotTheBulb, "Not The Bulb", "Quinn Wuest"),
            ["NotButton"] = (ProcessNotTheButton, "Not the Button", "Andrio Celos"),
            ["notPlungerButtonModule"] = (ProcessNotThePlungerButton, "Not The Plunger Button", "Anonymous"),
            ["notTheScrew"] = (ProcessNotTheScrew, "Not The Screw", "GhostSalt"),
            ["NotWhosOnFirst"] = (ProcessNotWhosOnFirst, "Not Who’s on First", "Andrio Celos"),
            ["notWordSearch"] = (ProcessNotWordSearch, "Not Word Search", "tandyCake"),
            ["notX01"] = (ProcessNotX01, "Not X01", "Quinn Wuest"),
            ["NotXRayModule"] = (ProcessNotXRay, "Not X-Ray", "Timwi"),
            ["numberedButtonsModule"] = (ProcessNumberedButtons, "Numbered Buttons", "Eltrick"),
            ["TheNumberGame"] = (ProcessNumberGame, "Number Game, The", "Anonymous"),
            ["Numbers"] = (ProcessNumbers, "Numbers", "BigCrunch22"),
            ["numpath"] = (ProcessNumpath, "Numpath", "tandyCake"),
            ["objectShows"] = (ProcessObjectShows, "Object Shows", "Timwi"),
            ["TheOctadecayotton"] = (ProcessOctadecayotton, "Octadecayotton, The", "Emik"),
            ["OddOneOutModule"] = (ProcessOddOneOut, "Odd One Out", "luisdiogo98"),
            ["SCP079"] = (ProcessOldAI, "Old AI", "noting3548"),
            ["oldFogey"] = (ProcessOldFogey, "Old Fogey", "Kuro"),
            ["oneLinksToAllModule"] = (ProcessOneLinksToAll, "One Links To All", "Anonymous"),
            ["OnlyConnectModule"] = (ProcessOnlyConnect, "Only Connect", "Timwi"),
            ["orangeArrowsModule"] = (ProcessOrangeArrows, "Orange Arrows", "kavinkul"),
            ["orangeCipher"] = (ProcessOrangeCipher, "Orange Cipher", "BigCrunch22"),
            ["orderedKeys"] = (ProcessOrderedKeys, "Ordered Keys", "TasThiluna"),
            ["OrderPickingModule"] = (ProcessOrderPicking, "Order Picking", "Brawlboxgaming"),
            ["OrientationCube"] = (ProcessOrientationCube, "Orientation Cube", "Timwi"),
            ["OrientationHypercube"] = (ProcessOrientationHypercube, "Orientation Hypercube", "Kuro"),
            ["palindromes"] = (ProcessPalindromes, "Palindromes", "Emik"),
            ["parity"] = (ProcessParity, "Parity", "Quinn Wuest"),
            ["partialDerivatives"] = (ProcessPartialDerivatives, "Partial Derivatives", "Timwi"),
            ["passportControl"] = (ProcessPassportControl, "Passport Control", "luisdiogo98"),
            ["pwDestroyer"] = (ProcessPasswordDestroyer, "Password Destroyer", "Eltrick"),
            ["PatternCubeModule"] = (ProcessPatternCube, "Pattern Cube", "Timwi"),
            ["GSPentabutton"] = (ProcessPentabutton, "Pentabutton, The", "Anonymous"),
            ["periodicWordsRB"] = (ProcessPeriodicWords, "Periodic Words", "Kuro"),
            ["spwizPerspectivePegs"] = (ProcessPerspectivePegs, "Perspective Pegs", "Andrio Celos"),
            ["Phosphorescence"] = (ProcessPhosphorescence, "Phosphorescence", "Emik"),
            ["PickupIdentification"] = (ProcessPickupIdentification, "Pickup Identification", "Anonymous"),
            ["pictionaryModule"] = (ProcessPictionary, "Pictionary", "Kuro"),
            ["pieModule"] = (ProcessPie, "Pie", "luisdiogo98"),
            ["pieFlash"] = (ProcessPieFlash, "Pie Flash", "VFlyer"),
            ["pigpenCycle"] = (ProcessPigpenCycle, "Pigpen Cycle", "TasThiluna"),
            ["PinkButtonModule"] = (ProcessPinkButton, "Pink Button, The", "Timwi"),
            ["pixelcipher"] = (ProcessPixelCipher, "Pixel Cipher", "Eltrick"),
            ["placeholderTalk"] = (ProcessPlaceholderTalk, "Placeholder Talk", "Emik"),
            ["PlacementRouletteModule"] = (ProcessPlacementRoulette, "Placement Roulette", "Brawlboxgaming"),
            ["planets"] = (ProcessPlanets, "Planets", "KingSlendy"),
            ["playfairCycle"] = (ProcessPlayfairCycle, "Playfair Cycle", "TasThiluna"),
            ["poetry"] = (ProcessPoetry, "Poetry", "Timwi"),
            ["PointlessMachines"] = (ProcessPointlessMachines, "Pointless Machines", "Anonymous"),
            ["polygons"] = (ProcessPolygons, "Polygons", "Anonymous"),
            ["PolyhedralMazeModule"] = (ProcessPolyhedralMaze, "Polyhedral Maze", "Timwi"),
            ["primeEncryption"] = (ProcessPrimeEncryption, "Prime Encryption", "VFlyer"),
            ["prisonBreak"] = (ProcessPrisonBreak, "Prison Break", "Anonymous"),
            ["Probing"] = (ProcessProbing, "Probing", "Timwi"),
            ["ProceduralMaze"] = (ProcessProceduralMaze, "Procedural Maze", "Kuro"),
            ["punctuationMarks"] = (ProcessPunctuationMarks, "...?", "Kuro"),
            ["purpleArrowsModule"] = (ProcessPurpleArrows, "Purple Arrows", "kavinkul"),
            ["PurpleButtonModule"] = (ProcessPurpleButton, "Purple Button, The", "Timwi"),
            ["GSPuzzleIdentification"] = (ProcessPuzzleIdentification, "Puzzle Identification", "GhostSalt"),
            ["puzzlingHexabuttons"] = (ProcessPuzzlingHexabuttons, "Puzzling Hexabuttons", "Anonymous"),
            ["q&a"] = (ProcessQnA, "Q & A", "Anonymous"),
            ["quantumPasswords"] = (ProcessQuantumPasswords, "Quantum Passwords", "Anonymous"),
            ["Quaver"] = (ProcessQuaver, "Quaver", "Emik"),
            ["Questionmark"] = (ProcessQuestionMark, "Question Mark", "Kuro"),
            ["QuickArithmetic"] = (ProcessQuickArithmetic, "Quick Arithmetic", "VFlyer"),
            ["quintuples"] = (ProcessQuintuples, "Quintuples", "Timwi"),
            ["QLModule"] = (ProcessQuiplash, "Quiplash", "Anonymous"),
            ["quizBuzz"] = (ProcessQuizBuzz, "Quiz Buzz", "Kuro"),
            ["qwirkle"] = (ProcessQwirkle, "Qwirkle", "GoodHood"),
            ["raidingTemples"] = (ProcessRaidingTemples, "Raiding Temples", "GoodHood"),
            ["RailwayCargoLoading"] = (ProcessRailwayCargoLoading, "Railway Cargo Loading", "LotsOfS"),
            ["ksmRainbowArrows"] = (ProcessRainbowArrows, "Rainbow Arrows", "TasThiluna"),
            ["R4YRecoloredSwitches"] = (ProcessRecoloredSwitches, "Recolored Switches", "Timwi"),
            ["RecursivePassword"] = (ProcessRecursivePassword, "Recursive Password", "Kuro"),
            ["redArrowsModule"] = (ProcessRedArrows, "Red Arrows", "kavinkul"),
            ["redbuttont"] = (ProcessRedButtont, "Red Button’t", "Anonymous"),
            ["redCipher"] = (ProcessRedCipher, "Red Cipher", "BigCrunch22"),
            ["RedHerring"] = (ProcessRedHerring, "Red Herring", "tandyCake"),
            ["ReformedRoleReversal"] = (ProcessReformedRoleReversal, "Reformed Role Reversal", "Emik"),
            ["regretbFiltering"] = (ProcessReGretBFiltering, "ReGret-B Filtering", "Anonymous"),
            ["RegularCrazyTalkModule"] = (ProcessRegularCrazyTalk, "Regular Crazy Talk", "Espik"),
            ["reorderedKeys"] = (ProcessReorderedKeys, "Reordered Keys", "Anonymous"),
            ["retirement"] = (ProcessRetirement, "Retirement", "luisdiogo98"),
            ["reverseMorse"] = (ProcessReverseMorse, "Reverse Morse", "luisdiogo98"),
            ["revPolNot"] = (ProcessReversePolishNotation, "Reverse Polish Notation", "shortc1rcuit"),
            ["rgbMaze"] = (ProcessRGBMaze, "RGB Maze", "kavinkul"),
            ["RGBSequences"] = (ProcessRGBSequences, "RGB Sequences", "Hawker"),
            ["MusicRhythms"] = (ProcessRhythms, "Rhythms", "Timwi"),
            ["rngCrystal"] = (ProcessRNGCrystal, "RNG Crystal", "Anonymous"),
            ["roboScannerModule"] = (ProcessRoboScanner, "Robo-Scanner", "Quinn Wuest"),
            ["robotProgramming"] = (ProcessRobotProgramming, "Robot Programming", "Hawker"),
            ["roger"] = (ProcessRoger, "Roger", "BigCrunch22"),
            ["roleReversal"] = (ProcessRoleReversal, "Role Reversal", "Emik"),
            ["theRule"] = (ProcessRule, "Rule, The", "TasThiluna"),
            ["RuleOfThreeModule"] = (ProcessRuleOfThree, "Rule of Three", "Quinn Wuest"),
            ["safetySquare"] = (ProcessSafetySquare, "Safety Square", "Kuro"),
            ["theSamsung"] = (ProcessSamsung, "Samsung, The", "TasThiluna"),
            ["saturn"] = (ProcessSaturn, "Saturn", "Anonymous"),
            ["sbemailsongs"] = (ProcessSbemailSongs, "Sbemail Songs", "ObjectsCountries"),
            ["scavengerHunt"] = (ProcessScavengerHunt, "Scavenger Hunt", "Timwi"),
            ["qSchlagDenBomb"] = (ProcessSchlagDenBomb, "Schlag den Bomb", "JerryEris"),
            ["ScramboozledEggainModule"] = (ProcessScramboozledEggain, "Scramboozled Eggain", "Quinn Wuest"),
            ["KritScripts"] = (ProcessScripting, "Scripting", "Kuro"),
            ["scrutinySquares"] = (ProcessScrutinySquares, "Scrutiny Squares", "Hawker"),
            ["SeaShells"] = (ProcessSeaShells, "Sea Shells", "Timwi"),
            ["semamorse"] = (ProcessSemamorse, "Semamorse", "Timwi"),
            ["TheSequencyclopedia"] = (ProcessSequencyclopedia, "Sequencyclopedia, The", "BigCrunch22"),
            ["SetTheory"] = (ProcessSetTheory, "S.E.T. Theory", "Timwi"),
            ["ShapesBombs"] = (ProcessShapesAndBombs, "Shapes And Bombs", "KingSlendy"),
            ["shapeshift"] = (ProcessShapeShift, "Shape Shift", "Timwi"),
            ["shiftedMaze"] = (ProcessShiftedMaze, "Shifted Maze", "Timwi"),
            ["MazeShifting"] = (ProcessShiftingMaze, "Shifting Maze", "BigCrunch22"),
            ["shogiIdentification"] = (ProcessShogiIdentification, "Shogi Identification", "tandyCake"),
            ["signLanguage"] = (ProcessSignLanguage, "Sign Language", "Hawker"),
            ["SillySlots"] = (ProcessSillySlots, "Silly Slots", "Timwi"),
            ["siloAuthorization"] = (ProcessSiloAuthorization, "Silo Authorization", "Timwi"),
            ["simonSaidModule"] = (ProcessSimonSaid, "Simon Said", "Quinn Wuest"),
            ["simonSamples"] = (ProcessSimonSamples, "Simon Samples", "Timwi"),
            ["Simon"] = (ProcessSimonSays, "Simon Says", "Andrio Celos"),
            ["simonScrambles"] = (ProcessSimonScrambles, "Simon Scrambles", "luisdiogo98"),
            ["SimonScreamsModule"] = (ProcessSimonScreams, "Simon Screams", "Timwi"),
            ["simonSelectsModule"] = (ProcessSimonSelects, "Simon Selects", "tachatat18"),
            ["SimonSendsModule"] = (ProcessSimonSends, "Simon Sends", "EternityShack"),
            ["simonServes"] = (ProcessSimonServes, "Simon Serves", "Hawker"),
            ["SimonShapesModule"] = (ProcessSimonShapes, "Simon Shapes", "tandyCake"),
            ["SimonShoutsModule"] = (ProcessSimonShouts, "Simon Shouts", "Timwi"),
            ["SimonShrieksModule"] = (ProcessSimonShrieks, "Simon Shrieks", "Timwi"),
            ["SimonSignalsModule"] = (ProcessSimonSignals, "Simon Signals", "Timwi"),
            ["simonSimons"] = (ProcessSimonSimons, "Simon Simons", "kavinkul"),
            ["SimonSingsModule"] = (ProcessSimonSings, "Simon Sings", "Timwi"),
            ["SimonSmiles"] = (ProcessSimonSmiles, "Simon Smiles", "Anonymous"),
            ["simonSmothers"] = (ProcessSimonSmothers, "Simon Smothers", "Kuro"),
            ["simonSounds"] = (ProcessSimonSounds, "Simon Sounds", "Timwi"),
            ["SimonSpeaksModule"] = (ProcessSimonSpeaks, "Simon Speaks", "Timwi"),
            ["simonsStar"] = (ProcessSimonsStar, "Simon’s Star", "TasThiluna"),
            ["simonstacks"] = (ProcessSimonStacks, "Simon Stacks", "Kuro"),
            ["simonStages"] = (ProcessSimonStages, "Simon Stages", "Espik"),
            ["SimonV2"] = (ProcessSimonStates, "Simon States", "Timwi"),
            ["simonStops"] = (ProcessSimonStops, "Simon Stops", "JerryEris"),
            ["simonStores"] = (ProcessSimonStores, "Simon Stores", "kavinkul"),
            ["simonSubdivides"] = (ProcessSimonSubdivides, "Simon Subdivides", "Anonymous"),
            ["simonSupports"] = (ProcessSimonSupports, "Simon Supports", "tandyCake"),
            ["simultaneousSimons"] = (ProcessSimultaneousSimons, "Simultaneous Simons", "Quinn Wuest"),
            ["SkewedSlotsModule"] = (ProcessSkewedSlots, "Skewed Slots", "Timwi"),
            ["Skewers"] = (ProcessSkewers, "Skewers", "Anonymous"),
            ["skyrim"] = (ProcessSkyrim, "Skyrim", "Timwi"),
            ["SlowMathModule"] = (ProcessSlowMath, "Slow Math", "Quinn Wuest"),
            ["smallCircle"] = (ProcessSmallCircle, "Small Circle", "TasThiluna"),
            ["smashmarrykill"] = (ProcessSmashMarryKill, "Smash, Marry, Kill", "Anonymous"),
            ["snooker"] = (ProcessSnooker, "Snooker", "TasThiluna"),
            ["snowflakes"] = (ProcessSnowflakes, "Snowflakes", "Kuro"),
            ["sonicKnuckles"] = (ProcessSonicKnuckles, "Sonic & Knuckles", "Hawker"),
            ["sonic"] = (ProcessSonicTheHedgehog, "Sonic the Hedgehog", "Timwi"),
            ["sorting"] = (ProcessSorting, "Sorting", "Emik"),
            ["SouvenirModule"] = (ProcessSouvenir, "Souvenir", "CaitSith2"),
            ["space_traders"] = (ProcessSpaceTraders, "Space Traders", "NickLatkovich"),
            ["spellingBee"] = (ProcessSpellingBee, "Spelling Bee", "BigCrunch22"),
            ["sphere"] = (ProcessSphere, "Sphere, The", "luisdiogo98"),
            ["SplittingTheLootModule"] = (ProcessSplittingTheLoot, "Splitting The Loot", "luisdiogo98"),
            ["spongebobBirthdayIdentification"] = (ProcessSpongebobBirthdayIdentification, "Spongebob Birthday Identification", "Hawker"),
            ["stabilityModule"] = (ProcessStability, "Stability", "NickLatkovich"),
            ["StableTimeSignatures"] = (ProcessStableTimeSignatures, "Stable Time Signatures", "Anonymous"),
            ["stackedSequences"] = (ProcessStackedSequences, "Stacked Sequences", "GhostSalt"),
            ["stars"] = (ProcessStars, "Stars", "BigCrunch22"),
            ["starstruck"] = (ProcessStarstruck, "Starstruck", "Anonymous"),
            ["stateOfAggregation"] = (ProcessStateOfAggregation, "State of Aggregation", "BigCrunch22"),
            ["stellar"] = (ProcessStellar, "Stellar", "Timwi"),
            ["stroopsTest"] = (ProcessStroopsTest, "Stroop’s Test", "Anonymous"),
            ["stupidSlots"] = (ProcessStupidSlots, "Stupid Slots", "tandyCake"),
            ["subblyJubbly"] = (ProcessSubblyJubbly, "Subbly Jubbly", "Anonymous"),
            ["subscribeToPewdiepie"] = (ProcessSubscribeToPewdiepie, "Subscribe to Pewdiepie", "BigCrunch22"),
            ["subway"] = (ProcessSubway, "Subway", "Hawker"),
            ["sugarSkulls"] = (ProcessSugarSkulls, "Sugar Skulls", "BigCrunch22"),
            ["GSSuitsAndColours"] = (ProcessSuitsAndColours, "Suits and Colours", "Hawker"),
            ["superparsing"] = (ProcessSuperparsing, "Superparsing", "tandyCake"),
            ["susadmin"] = (ProcessSUSadmin, "SUSadmin", "Anonymous"),
            ["BigSwitch"] = (ProcessSwitch, "Switch, The", "Timwi"),
            ["switchModule"] = (ProcessSwitches, "Switches", "Timwi"),
            ["MazeSwitching"] = (ProcessSwitchingMaze, "Switching Maze", "BigCrunch22"),
            ["SymbolCycleModule"] = (ProcessSymbolCycle, "Symbol Cycle", "CaitSith2"),
            ["symbolicCoordinates"] = (ProcessSymbolicCoordinates, "Symbolic Coordinates", "CaitSith2"),
            ["symbolicTasha"] = (ProcessSymbolicTasha, "Symbolic Tasha", "Timwi"),
            ["sync125_3"] = (ProcessSync_125_3, "SYNC-125 [3]", "Timwi"),
            ["synonyms"] = (ProcessSynonyms, "Synonyms", "Timwi"),
            ["sysadmin"] = (ProcessSysadmin, "Sysadmin", "NickLatkovich"),
            ["tapCode"] = (ProcessTapCode, "Tap Code", "Timwi"),
            ["tashaSqueals"] = (ProcessTashaSqueals, "Tasha Squeals", "luisdiogo98"),
            ["tasqueManaging"] = (ProcessTasqueManaging, "Tasque Managing", "tandyCake"),
            ["GSTeaSet"] = (ProcessTeaSet, "Tea Set, The", "Kuro"),
            ["TechnicalKeypad"] = (ProcessTechnicalKeypad, "Technical Keypad", "Kuro"),
            ["TenButtonColorCode"] = (ProcessTenButtonColorCode, "Ten-Button Color Code", "Timwi"),
            ["tenpins"] = (ProcessTenpins, "Tenpins", "TasThiluna"),
            ["tetriamonds"] = (ProcessTetriamonds, "Tetriamonds", "Kuro"),
            ["TextField"] = (ProcessTextField, "Text Field", "CaitSith2"),
            ["thinkingWiresModule"] = (ProcessThinkingWires, "Thinking Wires", "kavinkul"),
            ["ThirdBase"] = (ProcessThirdBase, "Third Base", "CaitSith2"),
            ["TicTacToeModule"] = (ProcessTicTacToe, "Tic Tac Toe", "Timwi"),
            ["timeSignatures"] = (ProcessTimeSignatures, "Time Signatures", "Anonymous"),
            ["timezone"] = (ProcessTimezone, "Timezone", "Timwi"),
            ["TipToe"] = (ProcessTipToe, "Tip Toe", "Kuro"),
            ["topsyTurvy"] = (ProcessTopsyTurvy, "Topsy Turvy", "BigCrunch22"),
            ["touchTransmission"] = (ProcessTouchTransmission, "Touch Transmission", "tandyCake"),
            ["Trajectory"] = (ProcessTrajectory, "Trajectory", "tandyCake"),
            ["transmittedMorseModule"] = (ProcessTransmittedMorse, "Transmitted Morse", "kavinkul"),
            ["triamonds"] = (ProcessTriamonds, "Triamonds", "Kuro"),
            ["TribalCouncil"] = (ProcessTribalCouncil, "Tribal Council", "Anonymous"),
            ["tripleTermModule"] = (ProcessTripleTerm, "Triple Term", "Quinn Wuest"),
            ["turtleRobot"] = (ProcessTurtleRobot, "Turtle Robot", "CaitSith2"),
            ["TwoBits"] = (ProcessTwoBits, "Two Bits", "Timwi"),
            ["ultimateCipher"] = (ProcessUltimateCipher, "Ultimate Cipher", "BigCrunch22"),
            ["ultimateCycle"] = (ProcessUltimateCycle, "Ultimate Cycle", "TasThiluna"),
            ["TheUltracubeModule"] = (ProcessUltracube, "Ultracube, The", "luisdiogo98"),
            ["UltraStores"] = (ProcessUltraStores, "UltraStores", "Marksam"),
            ["UncoloredSquaresModule"] = (ProcessUncoloredSquares, "Uncolored Squares", "Timwi"),
            ["R4YUncoloredSwitches"] = (ProcessUncoloredSwitches, "Uncolored Switches", "Timwi"),
            ["unfairCipher"] = (ProcessUnfairCipher, "Unfair Cipher", "luisdiogo98"),
            ["unfairsRevenge"] = (ProcessUnfairsRevenge, "Unfair’s Revenge", "VFlyer"),
            ["UnicodeModule"] = (ProcessUnicode, "Unicode", "Marksam"),
            ["UNO"] = (ProcessUNO, "UNO!", "Hawker"),
            ["unorderedKeys"] = (ProcessUnorderedKeys, "Unordered Keys", "Anonymous"),
            ["UnownCipher"] = (ProcessUnownCipher, "Unown Cipher", "kavinkul"),
            ["Updog"] = (ProcessUpdog, "Updog", "Anonymous"),
            ["USACycle"] = (ProcessUSACycle, "USA Cycle", "tandyCake"),
            ["USA"] = (ProcessUSAMaze, "USA Maze", "luisdiogo98"),
            ["V"] = (ProcessV, "V", "BigCrunch22"),
            ["valves"] = (ProcessValves, "Valves", "Hawker"),
            ["VaricoloredSquaresModule"] = (ProcessVaricoloredSquares, "Varicolored Squares", "luisdiogo98"),
            ["varicolourFlash"] = (ProcessVaricolourFlash, "Varicolour Flash", "Quinn Wuest"),
            ["VarietyModule"] = (ProcessVariety, "Variety", "Anonymous"),
            ["VCRCS"] = (ProcessVcrcs, "Vcrcs", "Timwi"),
            ["vectorsModule"] = (ProcessVectors, "Vectors", "kavinkul"),
            ["vexillology"] = (ProcessVexillology, "Vexillology", "luisdiogo98"),
            ["violetCipher"] = (ProcessVioletCipher, "Violet Cipher", "BigCrunch22"),
            ["visual_impairment"] = (ProcessVisualImpairment, "Visual Impairment", "Timwi"),
            ["WalkingCubeModule"] = (ProcessWalkingCube, "Walking Cube", "Anonymous"),
            ["warningSigns"] = (ProcessWarningSigns, "Warning Signs", "Kuro"),
            ["wasdModule"] = (ProcessWasd, "WASD", "Kuro"),
            ["watchingPaintDry"] = (ProcessWatchingPaintDry, "Watching Paint Dry", "Anonymous"),
            ["Wavetapping"] = (ProcessWavetapping, "Wavetapping", "KingSlendy"),
            ["TheWeakestLink"] = (ProcessWeakestLink, "Weakest Link, The", "Hawker"),
            ["WhatsOnSecond"] = (ProcessWhatsOnSecond, "What’s on Second", "BigCrunch22"),
            ["whiteCipher"] = (ProcessWhiteCipher, "White Cipher", "BigCrunch22"),
            ["whoOF"] = (ProcessWhoOF, "WhoOF", "VFlyer"),
            ["WhosOnFirst"] = (ProcessWhosOnFirst, "Who’s on First", "Andrio Celos"),
            ["whosOnMorseModule"] = (ProcessWhosOnMorse, "Who’s on Morse", "VFlyer"),
            ["wire"] = (ProcessWire, "Wire, The", "Timwi"),
            ["kataWireOrdering"] = (ProcessWireOrdering, "Wire Ordering", "Andrio Celos"),
            ["WireSequence"] = (ProcessWireSequence, "Wire Sequence", "Andrio Celos"),
            ["wolfGoatCabbageModule"] = (ProcessWolfGoatAndCabbage, "Wolf, Goat, and Cabbage", "Marksam"),
            ["workingTitle"] = (ProcessWorkingTitle, "Working Title", "BigCrunch22"),
            ["GSXenocryst"] = (ProcessXenocryst, "Xenocryst, The", "GhostSalt"),
            ["xmorse"] = (ProcessXmORseCode, "XmORse Code", "shortc1rcuit"),
            ["xobekuj"] = (ProcessXobekuJehT, "xobekuJ ehT", "Quinn Wuest"),
            ["xring"] = (ProcessXRing, "X-Ring", "Anonymous"),
            ["YahtzeeModule"] = (ProcessYahtzee, "Yahtzee", "Timwi"),
            ["yellowArrowsModule"] = (ProcessYellowArrows, "Yellow Arrows", "kavinkul"),
            ["YellowButtonModule"] = (ProcessYellowButton, "Yellow Button, The", "Timwi"),
            ["yellowbuttont"] = (ProcessYellowButtont, "Yellow Button’t", "Anonymous"),
            ["yellowCipher"] = (ProcessYellowCipher, "Yellow Cipher", "BigCrunch22"),
            ["zeroZero"] = (ProcessZeroZero, "Zero, Zero", "Timwi"),
            ["lgndZoni"] = (ProcessZoni, "Zoni", "luisdiogo98"),
        };
    }

    /* Generalized handlers for modules that are extremely similar */

    // Used by Affine Cycle, Caesar Cycle, Pigpen Cycle and Playfair Cycle
    private IEnumerator<YieldInstruction> processSpeakingEvilCycle1(ModuleData module, string componentName, Question question)
    {
        var comp = GetComponent(module, componentName);
        yield return WaitForSolve;

        var messages = GetArrayField<string>(comp, "message").Get();
        var responses = GetArrayField<string>(comp, "response").Get();
        var index = GetIntField(comp, "r").Get(ix =>
            ix < 0 ? "negative" :
            ix >= messages.Length ? $"greater than ‘message’ length ({messages.Length})" :
            ix >= responses.Length ? $"greater than ‘response’ length ({responses.Length})" : null);

        var message = Regex.Replace(messages[index], @"(?<!^).", m => m.Value.ToLowerInvariant());
        var response = Regex.Replace(responses[index], @"(?<!^).", m => m.Value.ToLowerInvariant());
        addQuestions(module,
          makeQuestion(question, module, formatArgs: new[] { "message" }, correctAnswers: new[] { message }, preferredWrongAnswers: new[] { response }),
          makeQuestion(question, module, formatArgs: new[] { "response" }, correctAnswers: new[] { response }, preferredWrongAnswers: new[] { message }));
    }

    // Used by Cryptic Cycle, Hill Cycle, Jumble Cycle and Ultimate Cycle
    private IEnumerator<YieldInstruction> processSpeakingEvilCycle2(ModuleData module, string componentName, Question question)
    {
        var comp = GetComponent(module, componentName);
        yield return WaitForSolve;

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
          makeQuestion(question, module, formatArgs: new[] { "message" }, correctAnswers: new[] { message }, preferredWrongAnswers: new[] { response }),
          makeQuestion(question, module, formatArgs: new[] { "response" }, correctAnswers: new[] { response }, preferredWrongAnswers: new[] { message }));
    }

    // Used by the World Mazes modules (currently: USA Maze, DACH Maze)
    private IEnumerator<YieldInstruction> processWorldMaze(ModuleData module, string script, Question question)
    {
        var comp = GetComponent(module, script);
        var fldOrigin = GetField<string>(comp, "_originState");
        var mthGetStates = GetMethod<List<string>>(comp, "GetAllStates", 0);
        var mthGetName = GetMethod<string>(comp, "GetStateFullName", 1);

        yield return WaitForSolve;

        var stateCodes = mthGetStates.Invoke() ?? throw new AbandonModuleException("GetAllStates() returned null.");
        if (stateCodes.Count == 0)
            throw new AbandonModuleException("GetAllStates() returned an empty list.");

        var states = stateCodes.Select(code => mthGetName.Invoke(code)).ToArray();
        var origin = mthGetName.Invoke(fldOrigin.Get());
        if (!states.Contains(origin))
            throw new AbandonModuleException($"‘_originState’ was not contained in the list of all states (“{origin}” not in: [{states.JoinString(", ")}]).");

        addQuestion(module, question, correctAnswers: new[] { origin }, preferredWrongAnswers: states);
    }

    // Used by Black, Blue, Brown, Coral, Cornflower, Cream, Crimson, Forest, Gray, Green, Indigo, Magenta, Maroon, Orange, Red, Violet, White, Yellow, and Ultimate Cipher
    private IEnumerator<YieldInstruction> processColoredCiphers(ModuleData module, string componentName, Question question)
    {
        var comp = GetComponent(module, componentName);
        yield return WaitForSolve;

        var pages = GetField<IList>(comp, "pages").Get(v => v.Count == 0 ? "expected at least one page" : null);
        var fldScreens = GetProperty<IList>(pages[0], "Screens", isPublic: true);
        var fldText = GetProperty<string>(fldScreens.Get(v => v.Count == 0 ? "expected at least one screen per page" : null)[0], "Text", isPublic: true);
        var fldAvoid = GetProperty<bool>(fldScreens.Get(v => v.Count == 0 ? "expected at least one screen per page" : null)[0], "SouvenirAvoid", isPublic: true);

        var allWordsType = comp.GetType().Assembly.GetType("Words.Data") ?? throw new AbandonModuleException("I cannot find the Words.Data type.");
        var allWordsObj = Activator.CreateInstance(allWordsType);
        var allWords = GetArrayField<List<string>>(allWordsObj, "_allWords").Get(expectedLength: 5);

        string[] generateWrongAnswers(string correctAnswer, AnswerGeneratorAttribute<string> gen)
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
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
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
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, gen));
                }

                // Brown Cipher page 2 screen 3 will only have letters A to F
                if (Regex.IsMatch(tup.text, @"^[A-F]+$"))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, 'A', 'F')));

                // Cornflower Cipher special case: three letters and a digit
                if (Regex.IsMatch(tup.text, @"^[A-Z]{3} \d$"))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, () => $"{"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)]}{"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)]}{"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)]} {Rnd.Range(0, 10)}"));

                // Indigo Cipher special case: 24 ? 52 = 12
                if (Regex.IsMatch(tup.text, @"^\d+ \? \d+ = \d+$"))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, () => $"{Rnd.Range(0, 64)} ? {Rnd.Range(0, 64)} = {Rnd.Range(0, 64)}"));

                // Yellow Cipher special case: 8-5-7-20
                if (Regex.IsMatch(tup.text, @"^\d+-\d+-\d+-\d+$"))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, () => $"{Rnd.Range(0, 26)}-{Rnd.Range(0, 26)}-{Rnd.Range(0, 26)}-{Rnd.Range(0, 26)}"));

                // Screens that have a word on them: pick other words of the same length as wrong answers
                if (tup.text.Length >= 4 && tup.text.Length <= 8 && allWords[tup.text.Length - 4].Contains(tup.text))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: allWords[tup.text.Length - 4].ToArray());

                // Screens that have only 0s and 1s on them
                if (tup.text.Length >= 3 && tup.text.All(ch => ch == '0' || ch == '1'))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, '0', '1')));

                // Screens that have only digits on them
                if (tup.text.All(ch => ch >= '0' && ch <= '9'))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, '0', '9')));

                // Screens that have only capital letters on them
                if (tup.text.All(ch => ch >= 'A' && ch <= 'Z'))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, 'A', 'Z')));

                // All other cases: jumble of letters and digits
                return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                    preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")));
            });
        }));
    }

    // Used by The Hypercube and The Ultracube
    private IEnumerator<YieldInstruction> processHypercubeUltracube(ModuleData module, string componentName, Question question)
    {
        var comp = GetComponent(module, componentName);
        var rotations = GetStaticField<string[]>(comp.GetType(), "_rotationNames").Get();
        var sequence = GetArrayField<int>(comp, "_rotations").Get(expectedLength: 5, validator: rot => rot < 0 || rot >= rotations.Length ? $"expected range 0–{rotations.Length - 1}" : null);

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(question, module, formatArgs: new[] { "first" }, correctAnswers: new[] { rotations[sequence[0]] }),
            makeQuestion(question, module, formatArgs: new[] { "second" }, correctAnswers: new[] { rotations[sequence[1]] }),
            makeQuestion(question, module, formatArgs: new[] { "third" }, correctAnswers: new[] { rotations[sequence[2]] }),
            makeQuestion(question, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { rotations[sequence[3]] }),
            makeQuestion(question, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { rotations[sequence[4]] }));
    }

    // Used by Triamonds and Tetriamonds
    private IEnumerator<YieldInstruction> processPolyiamonds(ModuleData module, string componentName, Question question, string[] colourNames)
    {
        var comp = GetComponent(module, componentName);
        yield return WaitForSolve;

        var posColour = GetField<int[]>(comp, "poscolour").Get();
        var pulsing = GetField<int[]>(comp, "pulsing").Get();

        var qs = new List<QandA>();
        for (int pos = 0; pos < 3; pos++)
            qs.Add(makeQuestion(question, module, formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { colourNames[posColour[pulsing[pos]]] }));
        addQuestions(module, qs);
    }
}

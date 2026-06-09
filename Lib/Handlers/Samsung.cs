using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SSamsung
{
    [Question("What was the language of the equation shown by Duolingo in {0}?", TwoColumns4Answers, "Spanish", "Italian", "Chinese", "French", "Afrikaans", "Swahili", "Japanese", "Korean", "Mongolian", "Thai", TranslateAnswers = true)]
    DuolingoLanguage,

    [Question("What was a {1} coordinate shown by Google Maps in {0}?", TwoColumns4Answers, ExampleAnswers = ["32.886970", "50.007742", "18.554616", "56.218924", "49.336579", "-31.079284", "50.836475", "31.428669", "-21.518496", "-33.339706"], Arguments = ["latitude", "longitude"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    GoogleMapsCoordinate,

    [Question("What was the {1} symbol of the cycle shown by Photomath in {0}?", ThreeColumns6Answers, "Σ", "ℝ", "≜", "!", "δ", "∞", "⋰", "∝", "∴", "¬", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    PhotomathCycleSymbol,

    [Question("What color was the {1} symbol of the cycle shown by Photomath in {0}?", TwoColumns4Answers, "blue", "purple", "green", "yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    PhotomathCycleColor,

    [Question("What was the starting symbol for Photomath in {0}?", ThreeColumns6Answers, "Σ", "ℝ", "≜", "!", "δ", "∞", "⋰", "∝", "∴", "¬")]
    PhotomathStartymbol,

    [Question("What song was played by Spotify in {0}?", TwoColumns4Answers, Type = AnswerType.Audio, ForeignAudioID = "theSamsung")]
    SpotifySong,

    [Question("What Braille pattern was shown by Discord in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Circles(2, 3, 20, 20, SuppressEmpty = true)]
    DiscordPattern
}

public partial class SouvenirModule
{
    [Handler("theSamsung", "Samsung", typeof(SSamsung), "Espik", AddThe = true)]
    [ManualQuestion("What was the language of the equation in Duolingo?")]
    [ManualQuestion("What were the coordinates in Google Maps?")]
    [ManualQuestion("What were the symbols and their colors in the cycle in Photomath?")]
    [ManualQuestion("What was the starting symbol in Photomath?")]
    [ManualQuestion("What song was played in Spotify?")]
    [ManualQuestion("What was the Braille pattern in Discord?")]
    private IEnumerator<SouvenirInstruction> ProcessSamsung(ModuleData module)
    {
        var comp = GetComponent(module, "theSamsung");
        yield return WaitForSolve;

        // Duolingo
        var language = GetIntField(comp, "languageIndex").Get();
        var allLanguages = SSamsung.DuolingoLanguage.GetAnswers();

        yield return question(SSamsung.DuolingoLanguage).Answers(allLanguages[language]);

        // Google Maps
        var topCoordTexts = GetArrayField<TextMesh>(comp, "gmapstext1", isPublic: true).Get(expectedLength: 2);
        var bottomCoordTexts = GetArrayField<TextMesh>(comp, "gmapstext2", isPublic: true).Get(expectedLength: 2);
        var latitudes = new[] { topCoordTexts[0].text, bottomCoordTexts[0].text };
        var longitudes = new[] { topCoordTexts[1].text, bottomCoordTexts[1].text };

        var topCountryValue = GetIntField(comp, "country1").Get();
        var bottomCountryValue = GetIntField(comp, "country2").Get();
        var preferredWrongCountries = Enumerable.Range(0, 10).Except([topCountryValue, bottomCountryValue]).ToArray();

        /*
        var allLatitudes = GetArrayField<float[]>(comp, "latCords").Get(expectedLength: 10);
        var allLongitudes = GetArrayField<float[]>(comp, "lonCords").Get(expectedLength: 10);
        */
        var allLatitudes = new float[][] {
            new[] { 32.886970f, 40.353244f },
            new[] { 50.007742f, 64.623881f },
            new[] { 18.554616f, 25.459147f },
            new[] { 56.218924f, 65.425386f },
            new[] { 49.336579f, 52.972463f },
            new[] { -31.079284f, -20.739675f },
            new[] { 50.836475f, 53.035930f },
            new[] { 31.428669f, 39.707192f },
            new[] { -21.518496f, -5.633482f },
            new[] { -33.339706f, -29.531405f }
        };

        var allLongitudes = new float[][] {
            new[] { -116.121098f, -81.843754f },
            new[] { -122.115239f, -97.417976f },
            new[] { -103.130860f, -98.455078f },
            new[] { 40.464846f, 128.988279f },
            new[] { 7.470706f, 11.610353f },
            new[] { 119.950928f, 144.386716f },
            new[] { -4.003420f, -0.259280f },
            new[] { 82.001950f, 113.642575f },
            new[] { -56.329102f, -40.535155f },
            new[] { 18.769041f, 24.117188f }
        };

        var preferredWrongLatitudes = new List<string>();
        var preferredWrongLongitudes = new List<string>();

        preferredWrongLatitudes.Add(latitudes[0]);
        preferredWrongLatitudes.Add(latitudes[1]);
        preferredWrongLongitudes.Add(longitudes[0]);
        preferredWrongLongitudes.Add(longitudes[1]);

        for (var i = 0; i < preferredWrongCountries.Length; i++)
        {
            var randomCoord = 0.0f;

            do
            {
                randomCoord = UnityEngine.Random.Range(allLatitudes[preferredWrongCountries[i]][0], allLatitudes[preferredWrongCountries[i]][1]);
            }
            while ((Math.Floor(randomCoord) == Math.Floor(float.Parse(latitudes[0]))) || (Math.Floor(randomCoord) == Math.Floor(float.Parse(latitudes[1]))));

            preferredWrongLatitudes.Add(randomCoord.ToString());

            do
            {
                randomCoord = UnityEngine.Random.Range(allLongitudes[preferredWrongCountries[i]][0], allLongitudes[preferredWrongCountries[i]][1]);
            }
            while ((Math.Floor(randomCoord) == Math.Floor(float.Parse(longitudes[0]))) || (Math.Floor(randomCoord) == Math.Floor(float.Parse(longitudes[1]))));

            preferredWrongLongitudes.Add(randomCoord.ToString());
        }

        yield return question(SSamsung.GoogleMapsCoordinate, args: ["latitude"]).Answers(latitudes, all: preferredWrongLatitudes.ToArray());
        yield return question(SSamsung.GoogleMapsCoordinate, args: ["longitude"]).Answers(longitudes, all: preferredWrongLongitudes.ToArray());

        // Photomath
        var symbols = GetListField<string>(comp, "mathSymbols").Get(expectedLength: 10);
        var cycleValues = GetArrayField<int>(comp, "values").Get(expectedLength: 4);
        var startValue = GetIntField(comp, "startingValue").Get();

        var allColors = SSamsung.PhotomathCycleColor.GetAnswers();
        var colorIndecies = GetListField<int>(comp, "photomathUsedColors").Get(expectedLength: 4);
        var cycleColors = GetArrayField<int>(comp, "operations").Get(expectedLength: 4);

        for (var i = 0; i < cycleValues.Length; i++)
        {
            yield return question(SSamsung.PhotomathCycleSymbol, args: [Ordinal(i + 1)]).Answers(symbols[cycleValues[i]]);
            yield return question(SSamsung.PhotomathCycleColor, args: [Ordinal(i + 1)]).Answers(allColors[colorIndecies[cycleColors[i]]]);
        }

        yield return question(SSamsung.PhotomathStartymbol).Answers(symbols[startValue]);

        // Spotify
        var samsungSettings = GetField<object>(comp, "Settings").Get();
        var noCopyright = GetField<bool>(samsungSettings, "noCopyright", isPublic: true).Get();

        var allSounds = GetArrayField<AudioClip>(comp, "allSounds", isPublic: true).Get();
        var songs = new List<AudioClip>();
        var noCopyrightSongs = new List<AudioClip>();
        var decoySongs = new List<AudioClip>();

        /*
        var songNames = GetArrayField<string>(comp, "songNames").Get(expectedLength: 10);
        var noCopyrightSongNames = GetArrayField<string>(comp, "ncSongNames").Get(expectedLength: 10);
        */
        var songNames = new[] { "harderBetterFasterStronger", "beatIt", "dangerZone", "youSpinMeRound", "hardwareStore", "xoTourLlif3", "runaway", "dontFearTheReaper", "touchToneTelephone", "rulerOfEverything" };
        var noCopyrightSongNames = new[] { "dannyDontYouKnow", "atTheSpeedOfLight", "vitality", "exitThisEarthsAtomosphere", "ransom", "newFriendly", "astronomia", "spanishFlea", "mountainKing", "clutterfunk" };

        var fullSolution = GetArrayField<int>(comp, "solution").Get();
        var songIndex = fullSolution[5];

        var decoyUsed = GetField<bool>(comp, "decoyUsed").Get();
        var decoyIndex = GetIntField(comp, "decoyIndex").Get();

        for (var i = 0; i < songNames.Length; i++)
        {
            songs.Add(allSounds.First(x => x.name == songNames[i]));
            noCopyrightSongs.Add(allSounds.First(x => x.name == noCopyrightSongNames[i]));
            decoySongs.Add(allSounds.First(x => x.name == "decoy" + i));
        }

        songs.Add(decoySongs[decoyIndex]);

        if (noCopyright)
            yield return question(SSamsung.SpotifySong).Answers(noCopyrightSongs[songIndex], all: noCopyrightSongs.ToArray());

        else if (decoyUsed)
            yield return question(SSamsung.SpotifySong).Answers(decoySongs[decoyIndex], all: songs.ToArray());

        else
            yield return question(SSamsung.SpotifySong).Answers(songs[songIndex], all: songs.ToArray());

        // Discord
    }
}

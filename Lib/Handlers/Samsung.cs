using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

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
    PhotomathStartSymbol,

    [Question("What song was played by Spotify in {0}?", TwoColumns4Answers, AnswerType = InfoType.Audio, ForeignAudioID = "theSamsung")]
    SpotifySong,

    [Question("What Braille pattern was shown by Discord in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites)]
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

        var allLatitudes = GetStaticField<float[][]>(comp.GetType(), "latCords").Get(v => v.Length != 10 ? "expected length 10" : null);
        var allLongitudes = GetStaticField<float[][]>(comp.GetType(), "lonCords").Get(v => v.Length != 10 ? "expected length 10" : null);

        var preferredWrongLatitudes = new List<string>();
        var preferredWrongLongitudes = new List<string>();

        preferredWrongLatitudes.Add(latitudes[0]);
        preferredWrongLatitudes.Add(latitudes[1]);
        preferredWrongLongitudes.Add(longitudes[0]);
        preferredWrongLongitudes.Add(longitudes[1]);

        var latitudesParsed = latitudes.Select(str => Math.Floor(float.Parse(str))).ToArray();
        var longitudesParsed = longitudes.Select(str => Math.Floor(float.Parse(str))).ToArray();
        for (var i = 0; i < preferredWrongCountries.Length; i++)
        {
            float randomCoord;
            do
                randomCoord = Rnd.Range(allLatitudes[preferredWrongCountries[i]][0], allLatitudes[preferredWrongCountries[i]][1]);
            while (latitudesParsed.Contains(Math.Floor(randomCoord)));

            preferredWrongLatitudes.Add(randomCoord.ToString());

            do
                randomCoord = Rnd.Range(allLongitudes[preferredWrongCountries[i]][0], allLongitudes[preferredWrongCountries[i]][1]);
            while (longitudesParsed.Contains(Math.Floor(randomCoord)));

            preferredWrongLongitudes.Add(randomCoord.ToString());
        }

        yield return question(SSamsung.GoogleMapsCoordinate, args: ["latitude"]).Answers(latitudes, all: preferredWrongLatitudes.ToArray());
        yield return question(SSamsung.GoogleMapsCoordinate, args: ["longitude"]).Answers(longitudes, all: preferredWrongLongitudes.ToArray());

        // Photomath
        var symbols = GetListField<string>(comp, "mathSymbols").Get(expectedLength: 10);
        var cycleValues = GetArrayField<int>(comp, "values").Get(expectedLength: 4);
        var startValue = GetIntField(comp, "startingValue").Get();

        var allColors = SSamsung.PhotomathCycleColor.GetAnswers();
        var colorIndices = GetListField<int>(comp, "photomathUsedColors").Get(expectedLength: 4);
        var cycleColors = GetArrayField<int>(comp, "operations").Get(expectedLength: 4);

        for (var i = 0; i < cycleValues.Length; i++)
        {
            yield return question(SSamsung.PhotomathCycleSymbol, args: [Ordinal(i + 1)]).Answers(symbols[cycleValues[i]]);
            yield return question(SSamsung.PhotomathCycleColor, args: [Ordinal(i + 1)]).Answers(allColors[colorIndices[cycleColors[i]]]);
        }

        yield return question(SSamsung.PhotomathStartSymbol).Answers(symbols[startValue]);

        // Spotify
        var samsungSettings = GetField<object>(comp, "Settings").Get();
        var noCopyright = GetField<bool>(samsungSettings, "noCopyright", isPublic: true).Get();

        var allSounds = GetArrayField<AudioClip>(comp, "allSounds", isPublic: true).Get();
        var songs = new List<AudioClip>();
        var noCopyrightSongs = new List<AudioClip>();
        var decoySongs = new List<AudioClip>();

        var songNames = GetStaticField<string[]>(comp.GetType(), "songNames").Get(v => v.Length != 10 ? "expected length 10" : null);
        var noCopyrightSongNames = GetStaticField<string[]>(comp.GetType(), "ncSongNames").Get(v => v.Length != 10 ? "expected length 10" : null);

        var songIndex = GetArrayField<int>(comp, "solution").Get(expectedLength: 8)[5];
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
        var users = GetField<IList>(comp, "users").Get(x => x.Count != 6 ? "expected length 6" : null);
        var fldPosition = GetIntField(users[0], "positionNumber", isPublic: true);
        var userPositions = users.Cast<object>().Select(x => fldPosition.GetFrom(x)).ToArray();

        var filledDots = 0;
        var dotPositions = new int[6] { 0, 1, 4, 5, 8, 9 };

        for (var i = 0; i < dotPositions.Length; i++)
            if (userPositions.Any(x => x == dotPositions[i]))
                filledDots |= 1 << i;

        if (filledDots != 0)
            yield return question(SSamsung.DiscordPattern).Answers(Sprites.GenerateCirclesSprite(2, 3, filledDots, 20, 20));
    }
}

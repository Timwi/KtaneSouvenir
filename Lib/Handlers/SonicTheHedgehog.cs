using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSonicTheHedgehog
{
    [SouvenirQuestion("What was the {1} picture on {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, Type = AnswerType.Sprites, SpriteFieldName = "SonicTheHedgehogSprites")]
    Pictures,
    
    [SouvenirQuestion("Which sound was played by the {1} screen on {0}?", TwoColumns4Answers, Type = AnswerType.Audio, AudioFieldName = "SonicTheHedgehogAudio", AudioSizeMultiplier = 4, Arguments = ["Running Boots", "Invincibility", "Extra Life", "Rings"], TranslateArguments = [true], ArgumentGroupSize = 1)]
    Sounds
}

public partial class SouvenirModule
{
    [SouvenirHandler("sonic", "Sonic the Hedgehog", typeof(SSonicTheHedgehog), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSonicTheHedgehog(ModuleData module)
    {
        var comp = GetComponent(module, "sonicScript");
        var fldsButtonSounds = new[] { "boots", "invincible", "life", "rings" }.Select(name => GetField<string>(comp, name + "Press"));
        var fldsPics = Enumerable.Range(0, 3).Select(i => GetField<Texture>(comp, "pic" + (i + 1))).ToArray();
        yield return WaitForSolve;

        if (SonicTheHedgehogSprites.Length != 15)
            throw new AbandonModuleException($@"Sonic the Hedgehog should have 15 sprites. Counted {SonicTheHedgehogSprites.Length}");

        var soundNameMapping = "boss|breathe|bumper|continueSFX|drown|emerald|extraLife|finalZone|invincibleSFX|jump|lamppost|marbleZone|skid|spikes|spin|spring"
            .Split('|').Select((s, i) => (s, i)).ToDictionary(t => t.s, t => t.i);

        var pictureNames = new string[] { "annoyedSonic", "ballhog", "blueLamppost", "burrobot", "buzzBomber", "crabMeat", "deadSonic", "drownedSonic", "fallingSonic", "motoBug", "redLamppost", "redSpring", "standingSonic", "switch", "yellowSpring" };
        var pics = fldsPics.Select(f => f.Get(p => p.name == null || !pictureNames.Contains(p.name) ? "unknown pic" : null)).ToArray();
        var sounds = fldsButtonSounds.Select(f => f.Get(s => !soundNameMapping.ContainsKey(s) ? "unknown sound" : null)).ToArray();

        var screenNames = new[] { "Running Boots", "Invincibility", "Extra Life", "Rings" };
        var spriteArr = new Sprite[][]
        {
            SonicTheHedgehogSprites.Where(sprite => new[] { "ballhog", "burrobot", "buzzBomber", "crabMeat", "motoBug" }.Contains(sprite.name)).ToArray(),
            SonicTheHedgehogSprites.Where(sprite => new[] { "annoyedSonic", "deadSonic", "drownedSonic", "fallingSonic", "standingSonic" }.Contains(sprite.name)).ToArray(),
            SonicTheHedgehogSprites.Where(sprite => new[] { "blueLamppost", "redLamppost", "redSpring", "switch", "yellowSpring" }.Contains(sprite.name)).ToArray()
        };

        var qs = new List<QandA>();

        for (var stage = 0; stage < 3; stage++)
            qs.Add(makeQuestion(
                question: Question.SonicTheHedgehogPictures,
                formatArgs: new[] { Ordinal(stage + 1) },
                data: module,
                allAnswers: spriteArr[stage],
                correctAnswers: new[] { spriteArr[stage].First(sprite => sprite.name == pics[stage].name) }));

        for (var screen = 0; screen < 4; screen++)
            qs.Add(makeQuestion(
                Question.SonicTheHedgehogSounds,
                data: module,
                formatArgs: new[] { screenNames[screen] },
                correctAnswers: new[] { SonicTheHedgehogAudio[soundNameMapping[sounds[screen]]] },
                preferredWrongAnswers: sounds.Select(s => SonicTheHedgehogAudio[soundNameMapping[s]]).ToArray()));

        addQuestions(module, qs);
    }
}
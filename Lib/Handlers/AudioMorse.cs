using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SAudioMorse
{
    [Question("What was signaled in {0}?", OneColumn4Answers, AnswerType = InfoType.Audio, ForeignAudioID = Sounds.Generated)]
    Sound
}

public partial class SouvenirModule
{
    [Handler("lgndAudioMorse", "Audio Morse", typeof(SAudioMorse), "Anonymous")]
    [ManualQuestion("What was the Morse code?")]
    private IEnumerator<SouvenirInstruction> ProcessAudioMorse(ModuleData module)
    {
        var morse = "   ;A.-;B-...;C-.-.;D-..;E.;F..-.;G--.;H....;I..;J.---;K-.-;L.-..;M--;N-.;O---;P.--.;Q--.-;R.-.;S...;T-;U..-;V...-;W.--;X-..-;Y-.--;Z--..;1.----;2..---;3...--;4....-;5.....;6-....;7--...;8---..;9----.;0-----"
            .Split(';').ToDictionary(s => s[0], s => s.Substring(1) + " ");

        var comp = GetComponent(module, "AudioMorseModuleScript");
        var words = GetArrayField<string>(comp, "words").Get(expectedLength: 87, validator: v => v.Any(c => c is < 'A' or > 'Z') ? "Expected only uppercase letters" : null);

        var word = words[GetIntField(comp, "wordIndex").Get(min: 0, max: words.Length)];
        var a = GetIntField(comp, "num1Index").Get(min: 0, max: 9);
        var b = GetIntField(comp, "num2Index").Get(min: 0, max: 9);
        var c = GetIntField(comp, "num3Index").Get(min: 0, max: 9);

        var key = $"{word} {a}{b}{c}";

        var all = new HashSet<string>() { key };
        while (all.Count < 6)
            all.Add($"{words.PickRandom()} {Rnd.Range(0, 1000):D3}");

        var clips = all.Select(k =>
        {
            var clips = new List<Sounds.AudioPosition>();
            var head = 0f;
            var m = k.Select(c => morse[c]).JoinString();
            for (var i = 0; i < m.Length; i++)
            {
                switch (m[i])
                {
                    case '.':
                        clips.Add((AudioMorseAudio[0], head));
                        head += 0.125f;
                        break;
                    case '-':
                        clips.Add((AudioMorseAudio[1], head));
                        goto case ' ';
                    case ' ':
                        head += 0.25f;
                        break;
                }
            }
            return Sounds.Combine($"AudioMorse_{k}", clips.ToArray());
        }).ToArray();

        yield return WaitForSolve;

        // Turn off the LEDs
        var leds = GetArrayField<Renderer>(comp, "LEDs", isPublic: true).Get(expectedLength: 2);
        var ledMaterials = GetArrayField<Material>(comp, "LEDMat", isPublic: true).Get(expectedLength: 2);
        for (var i = 0; i < leds.Length; i++)
            leds[i].material = ledMaterials[0]; // off

        yield return question(SAudioMorse.Sound).Answers(clips[0], all: clips);
    }
}

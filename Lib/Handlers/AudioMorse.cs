using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SAudioMorse
{
    [SouvenirQuestion("What was signaled in {0}?", OneColumn4Answers, Type = AnswerType.Audio, ForeignAudioID = Sounds.Generated)]
    Sound
}

public partial class SouvenirModule
{
    [SouvenirHandler("lgndAudioMorse", "Audio Morse", typeof(SAudioMorse), "Anonymous")]
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
        yield return question(SAudioMorse.Sound).Answers(clips[0], all: clips);
    }
}

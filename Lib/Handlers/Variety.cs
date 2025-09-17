using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SVariety
{
    [SouvenirQuestion("What color was the LED flashing in {0}?", TwoColumns4Answers, "Red", "Yellow", "Blue", "White", "Black", TranslateAnswers = true)]
    LED,

    [SouvenirQuestion("What digit was displayed, but not the answer, for the digit display in {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "0")]
    DigitDisplay,

    [SouvenirQuestion("What word could be formed, but was not the answer, for the letter display in {0}?", ThreeColumns6Answers, "ACE", "ACT", "AID", "AIM", "AIR", "ALE", "ALL", "AND", "ANT", "APT", "ARM", "ART", "AWE", "AYE", "BAD", "BAG", "BAR", "BAT", "BAY", "BED", "BEE", "BEG", "BET", "BID", "BIG", "BIT", "BIZ", "BOB", "BOW", "BOY", "BUT", "BUY", "BYE", "CAN", "CAP", "CAR", "CAT", "COP", "COT", "COW", "CUE", "CUP", "CUT", "DAD", "DAM", "DAY", "DIE", "DIG", "DIM", "DIP", "DOG", "DOT", "DRY", "DUE", "DUG", "DUO", "DYE", "EAR", "EAT", "FAN", "FAQ", "FAR", "FAT", "FAX", "FED", "FEE", "FEN", "FEW", "FIN", "FIT", "FIX", "FLY", "FOG", "FOR", "FRK", "FRQ", "FRY", "FUN", "FUR", "GET", "GIG", "GIN", "GUM", "GUT", "GUY", "HAM", "HAT", "HAY", "HEN", "HER", "HEY", "HIM", "HIP", "HIT", "HOP", "HOT", "HOW", "HUT", "ILK", "ILL", "IND", "INK", "IRK", "JAM", "JAR", "JAW", "JOB", "JOY", "KID", "KIN", "KIT", "LAD", "LAP", "LAW", "LAY", "LEG", "LET", "LID", "LIE", "LIP", "LIT", "LOG", "LOO", "LOT", "LOW", "LUA", "LUG", "MAD", "MAN", "MAP", "MAT", "MAX", "MAY", "MIC", "MID", "MIX", "MOB", "MOD", "MUD", "MUG", "MUM", "NET", "NEW", "NIL", "NLL", "NOD", "NOR", "NOT", "NOW", "NUN", "NUT", "OIL", "OPT", "OUR", "OUT", "OWE", "OWL", "PAD", "PAN", "PAR", "PAT", "PAY", "PEG", "PEN", "PER", "PET", "PIE", "PIG", "PIN", "PIT", "POP", "POT", "POW", "PUB", "PUT", "QUA", "QUE", "QUO", "RAG", "RAM", "RAT", "RAW", "RED", "RGB", "RIB", "RID", "RIG", "RIM", "ROB", "ROD", "ROT", "ROW", "RUB", "RUG", "RUM", "RUN", "SAD", "SAW", "SAY", "SEA", "SEE", "SET", "SHE", "SHY", "SIC", "SIG", "SIN", "SIR", "SIT", "SIX", "SLY", "SND", "SUE", "SUM", "SUN", "TAG", "TAP", "TAX", "TEA", "TEE", "TEN", "TGB", "THY", "TIE", "TIN", "TIP", "TOE", "TOO", "TOP", "TOY", "TRN", "TRY", "TUB", "VAT", "VET", "WAR", "WAX", "WAY", "WEE", "WET", "WHY", "WIG", "WIN", "WIT", "WIZ", "WRY", "YEN", "YET", "ZAG", "ZIG")]
    LetterDisplay,

    [SouvenirQuestion("What was the maximum display for the {1} in {0}?", ThreeColumns6Answers, "1 1", "2 1", "4 1", "6 1", "1 2", "2 2", "4 2", "1 4", "2 4", Arguments = ["timer", "ascending timer", "descending timer"], TranslateArguments = [true], ArgumentGroupSize = 1)]
    Timer,

    [SouvenirQuestion("What was n for the {1} in {0}?", TwoColumns4Answers, Arguments = ["knob", "colored knob", "red knob", "black knob", "blue knob", "yellow knob"], TranslateArguments = [true], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(3, 6)]
    ColoredKnob,

    [SouvenirQuestion("What was n for the {1} in {0}?", ThreeColumns6Answers, Arguments = ["bulb", "red bulb", "yellow bulb"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(5, 13)]
    Bulb,

    [SouvenirDiscriminator("the Variety that has {0}", Arguments = ["one\uE003 (LED)", "one\uE003 (digit display)", "one\uE003 (letter display)", "one\uE003 (timer)", "one\uE003 (knob)", "one\uE003 (colored knob)", "one\uE003 (redknob)", "one\uE003 (yellowknob)", "one\uE003 (blueknob)", "one\uE003 (blackknob)", "one\uE003 (bulb)", "one\uE003 (redbulb)", "one\uE003 (yellowbulb)", "a knob", "a colored knob", "a white knob", "a red knob", "a black knob", "a blue knob", "a yellow knob", "a keypad", "a white keypad", "a red keypad", "a yellow keypad", "a blue keypad", "a slider", "a horizontal slider", "a vertical slider", "an LED", "a digit display", "a wire", "a black wire", "a blue wire", "a red wire", "a yellow wire", "a white wire", "a button", "a red button", "a yellow button", "a blue button", "a white button", "a letter display", "a Braille display", "a key-in-lock", "a switch", "a red switch", "a yellow switch", "a blue switch", "a white switch", "a timer", "an ascending timer", "a descending timer", "a die", "a light-on-dark die", "a dark-on-light die", "a bulb", "a red bulb", "a yellow bulb", "a maze", "a 3×3 maze", "a 3×4 maze", "a 4×3 maze", "a 4×4 maze"], ArgumentGroupSize = 1)]
    Has
}

public partial class SouvenirModule
{
    [SouvenirHandler("VarietyModule", "Variety", typeof(SVariety), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessVariety(ModuleData module)
    {
        var comp = GetComponent(module, "VarietyModule");

        var items = GetField<IEnumerable>(comp, "_items").Get().Cast<object>().GroupBy(item => item.GetType().Name).ToDictionary(gr => gr.Key, gr => gr.ToArray());
        if (items.Values.Sum(ar => ar.Length) is int total and not 10)
            throw new AbandonModuleException($"Expected 10 items, found {total}.");

        yield return WaitForSolve;

        var disableSelectables = false;

        foreach (var led in items.Get("Led", []))
        {
            var c1 = GetProperty<object>(led, "Color1", isPublic: true).Get(null, v => (int) v is < 0 or > 4 ? $"Unknown LED color {v}" : null).ToString();
            var c2 = GetProperty<object>(led, "Color2", isPublic: true).Get(null, v => (int) v is < 0 or > 4 ? $"Unknown LED color {v}" : null).ToString();
            yield return question(SVariety.LED).Answers([c1, c2]);
            yield return new Discriminator(SVariety.Has, "led", true)
            {
                ArgumentsFromQuestion = q => SVariety.LED.Equals(q) ? ["one\uE003 (LED)"] : ["an LED"],
                PriorityFromQuestion = q => SVariety.LED.Equals(q) ? 0 : 1
            };
            disableSelectables = true;
        }

        foreach (var display in items.Get("DigitDisplay", []))
        {
            var amount = GetField<int>(display, "_numStates").Get(v => v is < 1 or > 9 ? $"Bad number of digit display states {v}" : null);
            if (amount == 1)
                Debug.Log($"<Souvenir #{_moduleId}> Variety: Not asking about the digit display because there was only one valid digit.");
            else
            {
                var displays = GetArrayField<int>(display, "_displayedDigitPerState").Get(expectedLength: 9);
                var solution = GetProperty<int>(display, "State", isPublic: true).Get(v => v is < 0 || v >= amount ? $"Bad digit display solution state {v}" : null);
                var ans = Enumerable.Range(0, amount).Except([solution]).Select(ix => displays[ix].ToString()).ToArray();
                yield return question(SVariety.DigitDisplay).Answers(ans, preferredWrong: [displays[solution].ToString()]);
            }
            yield return new Discriminator(SVariety.Has, "digit", true)
            {
                ArgumentsFromQuestion = q => SVariety.DigitDisplay.Equals(q) ? ["one\uE003 (digit display)"] : ["a digit display"],
                PriorityFromQuestion = q => SVariety.DigitDisplay.Equals(q) ? 0 : 1
            };
            disableSelectables = true;
        }

        foreach (var display in items.Get("LetterDisplay", []))
        {
            // Actually a property, but this method handles that
            var words = GetArrayField<string>(display, "FormableWords", isPublic: true).Get();
            if (words.Length == 1)
                Debug.Log($"<Souvenir #{_moduleId}> Variety: Not asking about the letter display because there was only one valid word.");
            else
            {
                var solution = GetProperty<int>(display, "State", isPublic: true).Get(v => v is < 0 || v >= words.Length ? $"Bad letter display solution state {v}" : null);
                yield return question(SVariety.LetterDisplay).Answers(words.Where((_, i) => i != solution).ToArray(), preferredWrong: [words[solution]]);
            }
            yield return new Discriminator(SVariety.Has, "letter", true)
            {
                ArgumentsFromQuestion = q => SVariety.LetterDisplay.Equals(q) ? ["one\uE003 (letter display)"] : ["a letter display"],
                PriorityFromQuestion = q => SVariety.LetterDisplay.Equals(q) ? 0 : 1
            };
            disableSelectables = true;
        }

        if (items.Get("Timer")?.Select(timer =>
        {
            var a = GetField<int>(timer, "_a").Get(v => v is not 2 and not 3 and not 5 and not 7 ? $"Unknown timer A value {v}" : null);
            var b = GetField<int>(timer, "_b").Get(v => v is not 2 and not 3 and not 5 || a is 3 && v is 5 || a is 5 && v is > 2 || a is 7 && v is not 2 ? $"Unknown timer B value {v}" : null);
            var flavor = (int) GetProperty<object>(timer, "FlavorType", isPublic: true).Get(null, validator: v => (int) v is < 0 or > 1 ? $"Unknown timer flavor {v}" : null);
            return (a, b, flavor);
        }).ToArray() is { } timers)
        {
            var flavorNames = new[] { "ascending timer", "descending timer" };
            foreach (var (a, b, flavor) in timers)
            {
                yield return question(SVariety.Timer, args: [timers.Length == 1 ? "timer" : flavorNames[flavor]]).Answers($"{a - 1} {b - 1}");
                yield return new Discriminator(SVariety.Has, $"timer-{flavor}", args: [flavor == 0 ? "an ascending timer" : "a descending timer"]) { Priority = 2 };
            }
            yield return new Discriminator(SVariety.Has, "timer", true)
            {
                ArgumentsFromQuestion = q => SVariety.Timer.Equals(q) ? ["one\uE003 (timer)"] : ["a timer"],
                PriorityFromQuestion = q => SVariety.Timer.Equals(q) ? 0 : 1
            };
            disableSelectables = true;
        }

        var cknobs = items.Get("ColoredKnob", []);
        var wknobs = items.Get("Knob", []);
        if (cknobs.Length > 0)
        {
            var format = cknobs.Length == 1 && wknobs.Length > 0 ? "colored knob" : cknobs.Length == 1 ? "knob" : null;
            foreach (var knob in cknobs)
            {
                var ans = GetProperty<int>(knob, "NumStates", isPublic: true).Get(null, v => v is < 3 or > 6 ? $"Bad colored knob state count {v}" : null);
                var flavor = GetProperty<object>(knob, "Color", isPublic: true).Get(null, v => (int) v is < 0 or > 3 ? $"Unknown knob color {v}" : null).ToString();
                flavor = flavor.Substring(0, flavor.Length - 4).ToLowerInvariant();
                yield return question(SVariety.ColoredKnob, args: [format ?? $"{flavor} knob"]).Answers(ans.ToString());
                yield return new Discriminator(SVariety.Has, $"cknob-{flavor}", args: [$"a {flavor} knob"]) { Priority = 3 };
            }
            yield return new Discriminator(SVariety.Has, "cknob", true)
            {
                ArgumentsFromQuestion = q => SVariety.ColoredKnob.Equals(q) ? ["one\uE003 (colored knob)"] : ["a colored knob"],
                PriorityFromQuestion = q => SVariety.ColoredKnob.Equals(q) ? 0 : 2
            };
            disableSelectables = true;
        }

        if (items.Get("Bulb") is { } bulbs)
        {
            var format = bulbs.Length == 1 ? "bulb" : null;
            foreach (var bulb in bulbs)
            {
                var ans = GetProperty<int>(bulb, "N", isPublic: true).Get(null, v => v is < 5 or > 13 ? $"Unknown bulb N {v}" : null);
                var flavor = GetProperty<object>(bulb, "Color", isPublic: true).Get(null, v => (int) v is < 0 or > 1 ? $"Unknown bulb color {v}" : null).ToString();
                flavor = flavor.Substring(0, flavor.Length - 4).ToLowerInvariant();
                yield return question(SVariety.Bulb, args: [format ?? $"{flavor} bulb"]).Answers(ans.ToString());
                yield return new Discriminator(SVariety.Has, $"bulb-{flavor}", args: [$"a {flavor} bulb"]) { Priority = 2 };
            }
            yield return new Discriminator(SVariety.Has, "bulb", true)
            {
                ArgumentsFromQuestion = q => SVariety.Bulb.Equals(q) ? ["one\uE003 (bulb)"] : ["a bulb"],
                PriorityFromQuestion = q => SVariety.Bulb.Equals(q) ? 0 : 1
            };
            disableSelectables = true;
        }

        if (items.Get("Key") != null)
            yield return new Discriminator(SVariety.Has, "key", args: ["a key-in-lock"]) { Priority = 1 };

        if (items.Get("Keypad") != null || items.Get("ColoredKeypad") != null)
            yield return new Discriminator(SVariety.Has, "keypad", args: ["a keypad"]) { Priority = 1 };

        if (items.Get("Keypad") != null)
            yield return new Discriminator(SVariety.Has, "white-keypad", args: ["a white keypad"]) { Priority = 2 };

        if (items.Get("ColoredKeypad") is { } keypads)
        {
            foreach (var item in keypads)
            {
                var color = GetProperty<object>(item, "Color", isPublic: true).Get().ToString().ToLowerInvariant();
                yield return new Discriminator(SVariety.Has, $"colored-keypad-{color}", args: [$"a {color.Substring(0, color.Length - 6)} keypad"]) { Priority = 2 };
            }
            yield return new Discriminator(SVariety.Has, "colored-keypad", args: ["a colored keypad"]) { Priority = 2 };
        }

        if (items.Get("Knob") != null || items.Get("ColoredKnob") != null)
            yield return new Discriminator(SVariety.Has, "knob", args: ["a knob"]) { Priority = 1 };

        if (items.Get("Knob") != null)
            yield return new Discriminator(SVariety.Has, "white-knob", args: ["a white knob"]) { Priority = 2 };

        if (items.Get("ColoredKnob") is { } knobs)
        {
            foreach (var item in knobs)
            {
                var color = GetProperty<object>(item, "Color", isPublic: true).Get().ToString().ToLowerInvariant();
                yield return new Discriminator(SVariety.Has, $"colored-knob-{color}", args: [$"a {color.Substring(0, color.Length - 4)} knob"]) { Priority = 3 };
            }
            yield return new Discriminator(SVariety.Has, "colored-knob", args: ["a colored knob"]) { Priority = 2 };
        }

        if (items.Get("BrailleDisplay") != null)
            yield return new Discriminator(SVariety.Has, "braille", args: ["a Braille display"]) { Priority = 1 };

        if (items.Get("Button") is { } buttons)
        {
            foreach (var button in buttons)
            {
                var color = GetProperty<object>(button, "Color", isPublic: true).Get().ToString().ToLowerInvariant();
                yield return new Discriminator(SVariety.Has, $"button-{color}", args: [$"a {color.Substring(0, color.Length - 6)} button"]) { Priority = 2 };
            }
            yield return new Discriminator(SVariety.Has, "button", args: ["a button"]) { Priority = 1 };
        }

        if (items.Get("Die") is { } dice)
        {
            foreach (var die in dice)
            {
                var flavor = GetProperty<bool>(die, "_flavor").Get();
                yield return new Discriminator(SVariety.Has, $"die-{flavor}", args: [flavor ? "a dark-on-light die" : "a light-on-dark die"]) { Priority = 2 };
            }
            yield return new Discriminator(SVariety.Has, "die", args: ["a die"]) { Priority = 1 };
        }

        if (items.Get("Maze") is { } mazes)
        {
            foreach (var maze in mazes)
            {
                var width = GetProperty<int>(maze, "Width", isPublic: true).Get();
                var height = GetProperty<int>(maze, "Height", isPublic: true).Get();
                yield return new Discriminator(SVariety.Has, $"maze-{width}-{height}", args: [$"a {width}×{height} maze"]) { Priority = 2 };
            }
            yield return new Discriminator(SVariety.Has, "maze", args: ["a maze"]) { Priority = 1 };
        }

        if (items.Get("Slider") is { } sliders)
        {
            foreach (var slider in sliders)
            {
                var orientation = GetProperty<object>(slider, "Orientation", isPublic: true).Get().ToString().ToLowerInvariant();
                yield return new Discriminator(SVariety.Has, $"slider-{orientation}", args: [$"a {orientation.Substring(0, orientation.Length - 6)} slider"]) { Priority = 2 };
            }
            yield return new Discriminator(SVariety.Has, "slider", args: ["a slider"]) { Priority = 1 };
        }

        if (items.Get("Switch") is { } switches)
        {
            foreach (var @switch in switches)
            {
                var color = GetProperty<object>(@switch, "Color", isPublic: true).Get().ToString().ToLowerInvariant();
                yield return new Discriminator(SVariety.Has, $"switch-{color}", args: [$"a {color.Substring(0, color.Length - 6)} switch"]) { Priority = 2 };
            }
            yield return new Discriminator(SVariety.Has, "switch", args: ["a switch"]) { Priority = 1 };
        }

        if (items.Get("Wire") is { } wires)
        {
            foreach (var wire in wires)
            {
                var color = GetProperty<object>(wire, "Color", isPublic: true).Get().ToString().ToLowerInvariant();
                yield return new Discriminator(SVariety.Has, $"wire-{color}", args: [$"a {color.Substring(0, color.Length - 4)} wire"]) { Priority = 2 };
            }
            yield return new Discriminator(SVariety.Has, "wire", args: ["a wire"]) { Priority = 1 };
        }

        if (disableSelectables)
            foreach (var c in GetField<KMSelectable>(comp, "ModuleSelectable", isPublic: true).Get().Children)
                if (c is not null)
                {
                    c.OnInteract = () =>
                    {
                        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, c.transform);
                        c.AddInteractionPunch(.25f);
                        return false;
                    };
                    c.OnInteractEnded = () => { };
                }
    }
}

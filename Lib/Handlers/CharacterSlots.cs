using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SCharacterSlots
{
    [SouvenirQuestion("Who was displayed in the {1} slot in the {2} stage of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CharacterSlotsSprites", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    DisplayedCharacters
}

public partial class SouvenirModule
{
    [SouvenirHandler("characterSlots", "Character Slots", typeof(SCharacterSlots), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessCharacterSlots(ModuleData module)
    {
        var comp = GetComponent(module, "CharacterSlotsScript");
        yield return WaitForSolve;

        var characters = GetField<Array>(comp, "slotStates").Get(arr => arr.Rank != 2 || arr.GetLength(0) != 3 || arr.GetLength(1) != 3 ? "expected size 3×3 array" : null);

        var fldName = GetField<Enum>(characters.GetValue(0, 0), "characterName");
        var stageNumber = GetField<int>(comp, "stageNumber").Get();

        for (var row = 0; row < stageNumber; row++)
        {
            for (var col = 0; col < 3; col++)
            {
                var name = fldName.GetFrom(characters.GetValue(row, col), ch => !CharacterSlotsSprites.Any(s => s.name.Replace(" ", "") == ch.ToString()) ? "unexpected character name" : null).ToString();
                yield return question(SCharacterSlots.DisplayedCharacters, args: [Ordinal(col + 1), Ordinal(row + 1)])
                    .Answers(CharacterSlotsSprites.First(sprite => sprite.name.Replace(" ", "") == name), preferredWrong: CharacterSlotsSprites);
            }
        }
    }
}

using System;
using UnityEngine;

namespace Souvenir;

public abstract class QuestionExtra
{
    public static implicit operator QuestionExtra(Sprite sprite) => sprite == null ? null : new QuestionExtraSprite(sprite, 0f);
    public static implicit operator QuestionExtra(string text) => text == null ? null : new QuestionExtraText(text, null, null);
    public abstract double[][] Setup(SouvenirModule souv);
    public abstract QuestionExtra Uplift(SouvenirModule souv, InfoType type);
}

public class QuestionExtraSprite(Sprite sprite, float spriteRotation) : QuestionExtra()
{
    public Sprite Sprite { get; private set; } = sprite;
    public float SpriteRotation { get; private set; } = spriteRotation;

    public override double[][] Setup(SouvenirModule souv)
    {
        var sprite = Sprite.Create(Sprite.texture, Sprite.rect, new Vector2(1, .5f), Sprite.pixelsPerUnit);
        sprite.name = Sprite.name;
        souv.QuestionSprite.sprite = sprite;
        souv.QuestionSprite.transform.localEulerAngles = new Vector3(90, SpriteRotation);
        souv.QuestionSprite.gameObject.SetActive(true);

        return Ut.NewArray<double[]>(
            [0, 1.1896],
            [0.0712, 1.258],
            [0.1476, 1.258],
            [0.306, 1.3442],
            [0.443, 1.668],
            [0.549, 1.668],
            [0.55, 1.6 - .874 * Sprite.rect.width / Sprite.pixelsPerUnit]);
    }

    public override string ToString() => $"sprite={Sprite.name}{(SpriteRotation != 0 ? $" (rot {SpriteRotation})" : "")}";
    public override QuestionExtra Uplift(SouvenirModule souv, InfoType type) => this;
}

public class QuestionExtraText(string text, Font font, Texture fontTexture) : QuestionExtra()
{
    public string Text { get; } = text;
    public Font Font { get; } = font;
    public Texture FontTexture { get; } = fontTexture;

    public override double[][] Setup(SouvenirModule souv)
    {
        souv.QuestionExtraText.text = Text;
        souv.QuestionExtraText.font = Font;
        souv.QuestionExtraText.GetComponent<MeshRenderer>().material.mainTexture = FontTexture;
        souv.QuestionExtraText.gameObject.SetActive(true);

        return Ut.NewArray<double[]>(
            [0, 1.1896],
            [0.0712, 1.258],
            [0.1476, 1.258],
            [0.306, 1.3442],
            [0.442, 1.3442],
            [0.443, 1.6 - (1.668 / .125 * souv.QuestionExtraTextRenderer.bounds.size.x)],
            [0.8, 1.6 - (1.668 / .125 * souv.QuestionExtraTextRenderer.bounds.size.x)],
            [0.81, 1.668]);
    }

    public override string ToString() => $"extra=“{Text}”{(Font == null ? "" : $" ({Font.name})")}";
    public override QuestionExtra Uplift(SouvenirModule souv, InfoType type) =>
        Font != null && FontTexture != null ? this :
        new QuestionExtraText(Text, souv.Fonts[(int) type], souv.FontTextures[(int) type]);
}

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
            [0, 0.6418705035971224],
            [0.03841726618705037, 0.6787769784172663],
            [0.07964028776978418, 0.6787769784172663],
            [0.16510791366906477, 0.7252877697841729],
            [0.23902877697841732, 0.9],
            [0.2962230215827339, 0.9],
            [0.29676258992805765, 0.85 - 0.47 * Sprite.rect.width / Sprite.pixelsPerUnit]);
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
            [0, 0.6418705035971224],
            [0.03841726618705037, 0.6787769784172663],
            [0.07964028776978418, 0.6787769784172663],
            [0.16510791366906477, 0.7252877697841729],
            [0.23848920863309356, 0.7252877697841729],
            [0.23902877697841732, 0.85 - souv.QuestionExtraTextRenderer.bounds.size.x / souv.SurfaceSizeFactor],
            [0.4316546762589929, 0.85 - souv.QuestionExtraTextRenderer.bounds.size.x / souv.SurfaceSizeFactor],
            [0.43705035971223033, 0.9]);
    }

    public override string ToString() => $"extra=“{Text}”{(Font == null ? "" : $" ({Font.name})")}";
    public override QuestionExtra Uplift(SouvenirModule souv, InfoType type) =>
        Font != null && FontTexture != null ? this :
        new QuestionExtraText(Text, souv.Fonts[(int) type], souv.FontTextures[(int) type]);
}

using UnityEngine;

namespace Souvenir;

public readonly struct TextAnswerInfo(Font font, int? fontSize, float? characterSize, Texture fontTexture, Material fontMaterial)
{
    public Font Font { get; } = font;
    public int? FontSize { get; } = fontSize;
    public float? CharacterSize { get; } = characterSize;
    public Texture FontTexture { get; } = fontTexture;
    public Material FontMaterial { get; } = fontMaterial;
}

using UnityEngine;

namespace Souvenir;

public readonly struct TextAnswerInfo(Font font = null, Texture fontTexture = null, Material fontMaterial = null, int? fontSize = null, float? characterSize = null)
{
    public Font Font { get; } = font;
    public Texture FontTexture { get; } = fontTexture;
    public Material FontMaterial { get; } = fontMaterial;
    public int? FontSize { get; } = fontSize;
    public float? CharacterSize { get; } = characterSize;
}

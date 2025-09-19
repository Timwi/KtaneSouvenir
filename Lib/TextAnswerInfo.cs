using UnityEngine;

namespace Souvenir;

public readonly struct TextAnswerInfo(Font font = null, Texture fontTexture = null, Material fontMaterial = null)
{
    public Font Font { get; } = font;
    public Texture FontTexture { get; } = fontTexture;
    public Material FontMaterial { get; } = fontMaterial;
}

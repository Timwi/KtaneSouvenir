using UnityEngine;

namespace Souvenir;

public readonly struct TextAnswerInfo(Font font = null, Texture fontTexture = null, float raiseBy = 0f)
{
    public Font Font { get; } = font;
    public Texture FontTexture { get; } = fontTexture;
    public float RaiseBy { get; } = raiseBy;
}

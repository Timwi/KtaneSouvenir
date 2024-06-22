using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace Souvenir
{
    public static class Sprites
    {
        private static readonly Dictionary<string, Texture2D> _gridSpriteCache = new();
        private static readonly Dictionary<AudioClip, Texture2D> _audioSpriteCache = new();

        public static Sprite GenerateGridSprite(Coord coord, float size = 1f)
        {
            var tw = 4 * coord.Width + 1;
            var th = 4 * coord.Height + 1;
            var key = $"{coord.Width}:{coord.Height}:{coord.Index}";
            if (!_gridSpriteCache.TryGetValue(key, out var tx))
            {
                tx = new Texture2D(tw, th, TextureFormat.ARGB32, false);
                tx.SetPixels32(Ut.NewArray(tw * th, ix =>
                    (ix % tw) % 4 == 0 || (ix / tw) % 4 == 0 ? new Color32(0xFF, 0xF8, 0xDD, 0xFF) :
                    (ix % tw) / 4 + coord.Width * (coord.Height - 1 - (ix / tw / 4)) == coord.Index ? new Color32(0xD8, 0x40, 0x00, 0xFF) : new Color32(0xFF, 0xF8, 0xDD, 0x00)));
                tx.Apply();
                tx.wrapMode = TextureWrapMode.Clamp;
                tx.filterMode = FilterMode.Point;
                _gridSpriteCache.Add(key, tx);
            }
            var sprite = Sprite.Create(tx, new Rect(0, 0, tw, th), new Vector2(0, .5f), th * (60f / 17) / size);
            sprite.name = coord.ToString();
            return sprite;
        }

        public static Sprite GenerateGridSprite(int width, int height, int index, float size = 1f)
        {
            return GenerateGridSprite(new Coord(width, height, index), size);
        }

        public static Sprite GenerateGridSprite(string spriteKey, int tw, int th, (int x, int y)[] squares, int highlightedCell, string spriteName, float? pixelsPerUnit = null)
        {
            var key = $"{spriteKey}:{highlightedCell}";
            if (!_gridSpriteCache.TryGetValue(key, out var tx))
            {
                tx = new Texture2D(tw, th, TextureFormat.ARGB32, false);
                var pixels = Ut.NewArray(tw * th, _ => new Color32(0xFF, 0xF8, 0xDD, 0x00));
                for (var sqIx = 0; sqIx < squares.Length; sqIx++)
                {
                    var (x, y) = squares[sqIx];
                    for (var i = 0; i <= 4; i++)
                        pixels[x + i + tw * (th - 1 - y)] = pixels[x + i + tw * (th - 1 - y - 4)] =
                            pixels[x + tw * (th - 1 - y - i)] = pixels[x + 4 + tw * (th - 1 - y - i)] = new Color32(0xFF, 0xF8, 0xDD, 0xFF);
                    if (sqIx == highlightedCell)
                        for (var i = 0; i < 3 * 3; i++)
                            pixels[x + 1 + (i % 3) + tw * (th - y - 2 - (i / 3))] = new Color32(0xD8, 0x40, 0x00, 0xFF);
                }
                tx.SetPixels32(pixels);
                tx.Apply();
                tx.wrapMode = TextureWrapMode.Clamp;
                tx.filterMode = FilterMode.Point;
                _gridSpriteCache.Add(key, tx);
            }
            var sprite = Sprite.Create(tx, new Rect(0, 0, tw, th), new Vector2(0, .5f), pixelsPerUnit ?? th * (60f / 17));
            sprite.name = spriteName;
            return sprite;
        }

        public static Sprite TranslateSprite(this Sprite sprite, float? pixelsPerUnit = null, string name = null)
        {
            var newSprite = Sprite.Create((sprite ?? throw new ArgumentNullException(nameof(sprite))).texture, sprite.rect, new Vector2(0, .5f), pixelsPerUnit ?? sprite.pixelsPerUnit);
            newSprite.name = name ?? sprite.name;
            return newSprite;
        }

        public static IEnumerable<Sprite> TranslateSprites(this IEnumerable<Sprite> sprites, float? pixelsPerUnit) =>
            (sprites ?? throw new ArgumentNullException(nameof(sprites))).Select(spr => TranslateSprite(spr, pixelsPerUnit));

        // Currently this takes quite a lot of CPU time to generate.
        // Perhaps we should:
        // - Generate all possible sprites on bomb load (with handlers)
        // - Rewrite this to work on the GPU
        // - Decrease the resolution
        // - Generate them at build time
        //
        // It's probably fine to discard the extra samples (currently handled in the block scope after the for loop)
        public static Sprite RenderWaveform(AudioClip answer, SouvenirModule module, float multiplier)
        {
            const int WIDTH = 128;
            // Height must be even
            const int HEIGHT = 64;
            Color32 cream = new(0xFF, 0xF8, 0xDD, 0xFF);
            Color32 black = new(0xFF, 0xF8, 0xDD, 0x00);

            if (!_audioSpriteCache.TryGetValue(answer, out Texture2D tex))
            {
                tex = new(WIDTH, HEIGHT, TextureFormat.RGBA32, false, false);
                _audioSpriteCache.Add(answer, tex);

                answer.LoadAudioData();
                float[] data = new float[answer.samples * answer.channels];
                answer.GetData(data, 0);

                if (data.Length < WIDTH)
                {
                    Debug.Log($"[Souvenir #{module._moduleId}] Warning!: Audio clip too short (minimum data length = {WIDTH}): {answer.name}");
                }
                else
                {
                    var columns = new Color32[WIDTH][];
                    var step = data.Length / WIDTH;
                    int start = 0;
                    for (int ix = 0; ix < WIDTH - 1; start += step, ix++)
                    {
                        var RMS = Math.Sqrt(data.Skip(start).Take(step).Select(v => v * v).Average());
                        var lastOn = (int) Mathf.Lerp(1, HEIGHT / 2, (float) RMS * multiplier);
                        var halfCol = Enumerable.Repeat(cream, lastOn).Concat(Enumerable.Repeat(black, HEIGHT / 2 - lastOn)).ToArray();
                        columns[ix] = halfCol.Reverse().Concat(halfCol).ToArray();
                    }
                    // The final segment might have more data than the rest
                    {
                        var RMS = Math.Sqrt(data.Skip(start).Select(v => v * v).Average());
                        var lastOn = (int) Mathf.Lerp(1, HEIGHT / 2, (float) RMS * multiplier);
                        var halfCol = Enumerable.Repeat(cream, lastOn).Concat(Enumerable.Repeat(black, HEIGHT / 2 - lastOn)).ToArray();
                        columns[WIDTH - 1] = halfCol.Reverse().Concat(halfCol).ToArray();
                    }
                    tex.SetPixels32(
                        Enumerable.Range(0, HEIGHT).SelectMany(row => Enumerable.Range(0, WIDTH).Select(col => columns[col][row])).ToArray()
                    );
                    tex.Apply(false, true);
                }
            }

            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, WIDTH, HEIGHT), new Vector2(0, 0.5f), WIDTH);
            sprite.name = answer.name;
            return sprite;
        }
    }
}

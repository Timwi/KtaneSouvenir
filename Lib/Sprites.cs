using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Souvenir
{
    public static class Sprites
    {
        private static readonly Dictionary<string, Sprite> _circleSpriteCache = new();
        private static readonly Dictionary<string, Sprite> _gridSpriteCache = new();
        private static readonly Dictionary<AudioClip, Sprite> _audioSpriteCache = new();

        private static bool IsPointInCircle(int pixelX, int pixelY, int radius, int gap, int dotX, int dotY)
        {
            var centerX = (2 * radius + gap) * dotX + radius;
            var centerY = (2 * radius + gap) * dotY + radius;
            return (pixelX - centerX) * (pixelX - centerX) + (pixelY - centerY) * (pixelY - centerY) < radius * radius;
        }

        private static bool IsPointInAnnulus(int pixelX, int pixelY, int radius, int innerRadius, int gap, int dotX, int dotY)
        {
            var centerX = (2 * radius + gap) * dotX + radius;
            var centerY = (2 * radius + gap) * dotY + radius;
            var value = (pixelX - centerX) * (pixelX - centerX) + (pixelY - centerY) * (pixelY - centerY);
            return value < radius * radius && value > innerRadius * innerRadius;
        }

        /// <summary>
        /// Generates a sprite consisting of circles arranged in a grid (e.g. Braille, Valves).
        /// </summary>
        /// <param name="width">How many circles will appear in each row.</param>
        /// <param name="height">How many circles will appear in each column.</param>
        /// <param name="circlesPresent">A bitfield specifying which circles are visible (LSB = top-left).</param>
        /// <param name="radius">The radius of each circle, in pixels.</param>
        /// <param name="gap">The gap between circles, in pixels.</param>
        /// <param name="outline">Specifies whether circles that are not visible should have an outline.</param>
        /// <param name="vertical">If <c>true</c>, <paramref name="circlesPresent"/> is in vertical order (Braille); else, reading order.</param>
        public static Sprite GenerateCirclesSprite(int width, int height, int circlesPresent, int radius, int gap, bool outline = false, bool vertical = false)
        {
            if (vertical)
            {
                var newCirclesPresent = 0;
                for (var x = 0; x < width; x++)
                    for (var y = 0; y < height; y++)
                        if ((circlesPresent & (1 << (y + height * x))) != 0)
                            newCirclesPresent |= 1 << (x + width * y);
                circlesPresent = newCirclesPresent;
            }

            var textureWidth = width * radius * 2 + (width - 1) * gap;
            var textureHeight = height * radius * 2 + (height - 1) * gap;
            var key = $"{width}:{height}:{circlesPresent}:{radius}:{gap}:{outline}";

            // If the sprite is not cached, create it
            if (!_circleSpriteCache.TryGetValue(key, out var sprite))
            {
                // Create the base of the texture
                var tx = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);
                tx.SetPixels32(Ut.NewArray(textureWidth * textureHeight, pixel =>
                {
                    for (var dotX = 0; dotX < width; dotX++)
                        for (var dotY = 0; dotY < height; dotY++)
                            if ((circlesPresent & (1 << (dotX + width * dotY))) != 0
                                    ? IsPointInCircle(pixel % textureWidth, textureHeight - 1 - (pixel / textureWidth), radius, gap, dotX, dotY)
                                    : outline && IsPointInAnnulus(pixel % textureWidth, textureHeight - 1 - (pixel / textureWidth), radius, radius - 5, gap, dotX, dotY))
                                return new Color32(0xFF, 0xF8, 0xDD, 0xFF);
                    return new Color32(0x00, 0x00, 0x00, 0x00);
                }));
                tx.Apply();
                tx.wrapMode = TextureWrapMode.Clamp;
                tx.filterMode = FilterMode.Point;

                sprite = Sprite.Create(tx, new Rect(0, 0, textureWidth, textureHeight), new Vector2(0, .5f), textureHeight * (60f / 17));
                sprite.name = $"Circles {key}";
                _circleSpriteCache.Add(key, sprite);
            }
            return sprite;
        }

        public static Sprite GenerateGridSprite(Coord coord, float size = 1f)
        {
            var tw = 4 * coord.Width + 1;
            var th = 4 * coord.Height + 1;
            var key = $"{coord.Width}:{coord.Height}:{coord.Index}:{size}";
            if (!_gridSpriteCache.TryGetValue(key, out var sprite))
            {
                var tx = new Texture2D(tw, th, TextureFormat.ARGB32, false);
                tx.SetPixels32(Ut.NewArray(tw * th, ix =>
                    (ix % tw) % 4 == 0 || (ix / tw) % 4 == 0 ? new Color32(0xFF, 0xF8, 0xDD, 0xFF) :
                    (ix % tw) / 4 + coord.Width * (coord.Height - 1 - (ix / tw / 4)) == coord.Index ? new Color32(0xD8, 0x40, 0x00, 0xFF) : new Color32(0xFF, 0xF8, 0xDD, 0x00)));
                tx.Apply();
                tx.wrapMode = TextureWrapMode.Clamp;
                tx.filterMode = FilterMode.Point;

                sprite = Sprite.Create(tx, new Rect(0, 0, tw, th), new Vector2(0, .5f), th * (60f / 17) / size);
                sprite.name = coord.ToString();
                _gridSpriteCache[key] = sprite;
            }
            return sprite;
        }

        public static Sprite GenerateGridSprite(int width, int height, int index, float size = 1f)
        {
            return GenerateGridSprite(new Coord(width, height, index), size);
        }

        public static Sprite GenerateGridSprite(string spriteKey, int tw, int th, (int x, int y)[] squares, int highlightedCell, string spriteName, float? pixelsPerUnit = null)
        {
            var key = $"{spriteKey}:{highlightedCell}";
            if (!_gridSpriteCache.TryGetValue(key, out var sprite))
            {
                var tx = new Texture2D(tw, th, TextureFormat.ARGB32, false);
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

                sprite = Sprite.Create(tx, new Rect(0, 0, tw, th), new Vector2(0, .5f), pixelsPerUnit ?? th * (60f / 17));
                sprite.name = spriteName;
                _gridSpriteCache.Add(key, sprite);
            }
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

        public static IEnumerable<Sprite> TranslateSpritesScaled(this IEnumerable<Sprite> sprites, float pixelsPerUnitMultiplier = 1f) =>
            (sprites ?? throw new ArgumentNullException(nameof(sprites))).Select(spr => TranslateSprite(spr, spr.pixelsPerUnit * pixelsPerUnitMultiplier));

        public static Sprite ToSprite(this Texture2D texture)
        {
            var newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, .5f), texture.height * (60f / 17));
            newSprite.name = texture.name;
            return newSprite;
        }

        // Height must be even, should be a power of 2
        const int HEIGHT = 128;
        const int WIDTH = HEIGHT * 4;
        const int MIN_LINE = 3;
        public static Sprite RenderWaveform(AudioClip answer, SouvenirModule module, float multiplier)
        {
            if (!_audioSpriteCache.TryGetValue(answer, out var sprite))
            {
                var tex = new Texture2D(WIDTH, HEIGHT, TextureFormat.RGBA32, false, false)
                {
                    wrapMode = TextureWrapMode.Clamp,
                    filterMode = FilterMode.Bilinear
                };

                answer.LoadAudioData();
                Color[] result = Enumerable.Repeat((Color) new Color32(0xFF, 0xF8, 0xDD, 0x00), WIDTH * HEIGHT).ToArray();
                tex.SetPixels(result);
                tex.Apply(false, false);

                if (answer.samples * answer.channels < WIDTH)
                {
                    Debug.Log($"[Souvenir #{module._moduleId}] Warning!: Audio clip too short (minimum data length = {WIDTH}): {answer.name}");
                }
                else
                {
                    Debug.Log($"‹Souvenir #{module._moduleId}› Starting thread to render waveform: {answer.name}");
                    var runner = new GameObject($"Waveform Renderer - {answer.name}", typeof(DataBehaviour));
                    UnityEngine.Object.DontDestroyOnLoad(runner);
                    var behavior = runner.GetComponent<DataBehaviour>();
                    behavior.Result = result;
                    float[] data = new float[answer.samples * answer.channels];
                    answer.GetData(data, 0);

                    new Thread(() => RenderRMS(data, behavior, multiplier))
                    {
                        IsBackground = true,
                        Name = $"Waveform Renderer - {answer.name}"
                    }.Start();
                    behavior.StartCoroutine(CopyData(behavior, tex, runner, answer.name, module._moduleId));
                }

                sprite = Sprite.Create(tex, new Rect(0, 0, WIDTH, HEIGHT), new Vector2(0, 0.5f), WIDTH);
                sprite.name = answer.name;
                _audioSpriteCache.Add(answer, sprite);
            }
            return sprite;
        }

        private static IEnumerator CopyData(DataBehaviour behavior, Texture2D tex, GameObject runner, string name, int id)
        {
            while (behavior.FinishedColumns <= WIDTH - 1)
                yield return null;

            tex.SetPixels(behavior.Result);
            tex.Apply(false, true);
            UnityEngine.Object.Destroy(runner);

            Debug.Log($"‹Souvenir #{id}› Finished rendering waveform: {name}");
        }

        private static void RenderRMS(float[] data, DataBehaviour behavior, float multiplier)
        {
            Color32 cream = new(0xFF, 0xF8, 0xDD, 0xFF);
            Color32 black = new(0xFF, 0xF8, 0xDD, 0x00);

            var step = data.Length / WIDTH;
            int start = 0;
            for (int ix = 0; ix < WIDTH; start += step, ix++)
            {
                float totalPlus = 0f, totalMinus = 0f;
                int countPlus = 0, countMinus = 0;
                for (int j = start; j < start + step; j++)
                {
                    if (data[j] > 0f)
                    {
                        totalPlus += data[j] * data[j];
                        countPlus++;
                    }
                    else
                    {
                        totalMinus += data[j] * data[j];
                        countMinus++;
                    }
                }
                var RMSPlus = countPlus == 0 ? 0f : Math.Sqrt(totalPlus / countPlus);
                var RMSMinus = countMinus == 0 ? 0f : Math.Sqrt(totalMinus / countMinus);
                var creamCountPlus = (int) Mathf.Lerp(MIN_LINE, HEIGHT / 2, (float) RMSPlus * multiplier);
                var creamCountMinus = (int) Mathf.Lerp(MIN_LINE, HEIGHT / 2, (float) RMSMinus * multiplier);
                var blackCount = HEIGHT / 2 - creamCountPlus;
                int i = 0;
                for (; i < blackCount; i++)
                    behavior.Result[ix + i * WIDTH] = black;
                for (; i < creamCountPlus + creamCountMinus + blackCount; i++)
                    behavior.Result[ix + i * WIDTH] = cream;
                for (; i < HEIGHT; i++)
                    behavior.Result[ix + i * WIDTH] = black;
                behavior.FinishedColumns++;
            }
        }

        private class DataBehaviour : MonoBehaviour
        {
            public int FinishedColumns = 0;
            public Color[] Result;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Souvenir
{
    public static class Grid
    {
        private static readonly Dictionary<string, Texture2D> _gridSpriteCache = new();

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
    }
}

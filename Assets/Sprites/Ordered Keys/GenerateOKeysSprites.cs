// This file should not be included in the final build.
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class GenerateOKeysSprites : MonoBehaviour
{
    [MenuItem("Souvenir/Generate Ordered Keys Sprites")]
    public static void GenerateSprites()
    {
        const int KW = 5, DY = 1, W = 1 + (KW + 1) * 6, H = 27;
        var tx = new Texture2D(W, H, TextureFormat.ARGB32, false);
        var basePixels = Enumerable.Repeat(new Color32(0xFF, 0xF8, 0xDD, 0x00), W * H).ToArray();
        for (var x = 0; x < W; x++)
            basePixels[x + W * (H - 1)] = new Color32(0xFF, 0xF8, 0xDD, 0xFF);
        for (var colIx = 0; colIx < 7; colIx++)
        {
            for (var y = Mathf.Max((colIx - 1) * DY, 0); y < H; y++)
                basePixels[(KW + 1) * colIx + W * y] = new Color32(0xFF, 0xF8, 0xDD, 0xFF);
        }
        for (var colIx = 0; colIx < 6; colIx++)
        {
            var x = 1 + (KW + 1) * colIx;
            for (int dx = 0; dx < KW; dx++)
                basePixels[x + dx + W * colIx * DY] = new Color32(0xFF, 0xF8, 0xDD, 0xFF);
        }

        for (int i = 0; i < 6; i++)
        {
            var pixels = new Color32[W * H];
            basePixels.CopyTo(pixels, 0);

            var x = 1 + (KW + 1) * i;
            for (int dx = 0; dx < KW; dx++)
                for (int y = 1 + i * DY; y < H - 1; y++)
                    pixels[x + dx + W * y] = new Color32(0xD8, 0x40, 0x00, 0xFF);

            tx.SetPixels32(pixels);
            tx.Apply();
            tx.wrapMode = TextureWrapMode.Clamp;
            tx.filterMode = FilterMode.Point;

            var png = tx.EncodeToPNG();
            using (var fs = File.Create($"Assets/Sprites/Ordered Keys/Key{i + 1}.png"))
                fs.Write(png, 0, png.Length);
        }
    }
}
#endif

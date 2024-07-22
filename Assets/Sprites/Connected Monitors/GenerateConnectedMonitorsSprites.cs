// This file should not be included in the final build.
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class GenerateConnectedMonitorsSprites : MonoBehaviour
{
    [MenuItem("Souvenir/Generate Connected Monitors Sprites")]
    public static void GenerateSprites()
    {
        var colors = Enumerable.Repeat((Color) new Color32(255, 248, 221, 0), 4).ToArray();
        for (int i = 0; i < 15; i++)
        {
            var ix = i >= 3 ? i + 1 : i;
            var sp = Souvenir.Sprites.GenerateGridSprite(4, 4, ix).texture;
            sp.SetPixels(13, 16, 4, 1, colors);
            sp.SetPixels(16, 13, 1, 4, colors);
            var png = sp.EncodeToPNG();
            using (var fs = File.Create($"Assets/Sprites/Connected Monitors/Monitor{i}.png"))
                fs.Write(png, 0, png.Length);
        }
    }
}
#endif

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

// This class will not be included in the final build.
#if UNITY_EDITOR
public class GenerateDiffusionSprites : MonoBehaviour
{
    [MenuItem("Souvenir/Generate Diffusion Sprites")]
    public static void GenerateSprites()
    {
        var ixs = new int[] { 0, 1, 2, 3, 7, 11, 15, 14, 13, 12, 8, 4 };
        var colors = Enumerable.Repeat((Color) new Color32(255, 248, 221, 0), 7).ToArray();
        for (int i = 0; i < 12; i++)
        {
            var sp = Souvenir.Sprites.GenerateGridSprite(4, 4, ixs[i]).texture;
            sp.SetPixels(8, 5, 1, 7, colors);
            sp.SetPixels(5, 8, 7, 1, colors);
            var png = sp.EncodeToPNG();
            using (var fs = File.Create($"Assets/Sprites/Diffusion/Compartment{i}.png"))
                fs.Write(png, 0, png.Length);
        }
    }
}
#endif
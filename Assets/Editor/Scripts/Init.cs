using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;
using Newtonsoft.Json;

[InitializeOnLoad]
public class EditorInit
{
	static EditorInit()
	{
		KtaneAssemblyImporterWindow.UpdateAssemblyDefinitions = () => {
			var asmDefName = string.IsNullOrEmpty(ModConfig.ID) ? "KtaneMod" : ModConfig.ID;
            var asmDefPath = CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName(asmDefName);
            if(!string.IsNullOrEmpty(asmDefPath))
            {
                var asmDef = new AssemblyDefinitions.AssemblyDefinition
                {
                    name = asmDefName,
                    references = new[] { "GameProxies" }
                };
                File.WriteAllText(Path.Combine(Application.dataPath, asmDefName + ".asmdef"), JsonConvert.SerializeObject(asmDef, Formatting.Indented));
            }
		};
	}
}

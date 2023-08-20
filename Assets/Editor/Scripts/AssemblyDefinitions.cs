using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

public static class AssemblyDefinitions
{
    // Prevent any loaded assemblies from being the name of an Assembly Definition.
    // For example, Unity will not load an Assembly Definition for Assembly-CSharp, as it is reserved.
    public static readonly string[] reserved = AppDomain.CurrentDomain.GetAssemblies().Select(x => x.GetName().Name).Concat(new [] { "Assembly-CSharp", "Assembly-CSharp-Editor", "Assembly-CSharp-Firstpass", "Assembly-CSharp-Editor-Firstpass" }).ToArray();
    private static readonly DirectoryInfo rootDirectory = Directory.GetParent(Application.dataPath);
    private static readonly string slnFile = Path.Combine(rootDirectory.FullName, rootDirectory.Name) + ".sln";
    private static readonly string editorPath = Path.Combine(Application.dataPath, "Editor");

    private static readonly string[] ignoredDefinitions = new[]
    {
        "GameProxies"
    };
    
    internal static bool DefinitionExists()
    {
        return CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName(ModConfig.ID) != null;
    }
    
    
    internal static void RunChecks()
    {
        // Don't do anything if the ID hasn't changed. This also avoids a false error from reserved due to the current assembly definition being loaded in the project.
        if (DefinitionExists())
            return;
        if (reserved.Contains(ModConfig.ID) || string.IsNullOrEmpty(ModConfig.ID))
        {
            Debug.LogWarningFormat("The selected id [\"{0}\"] cannot be used with this project due to it being reserved or empty. Please choose a different id for your mod.", ModConfig.ID);
            return;
        }
        // Delete any existing assembly definitions, as they may conflict with the new ones. Meta files are also deleted.
        // Additionally delete any previous .csproj files. These may conflict with Visual Studio Code, and may clutter the root folder if used consecutively.
        RemoveDefinitions();
        CreateDefinitions();
    }

    private static void RemoveDefinitions()
    {
        var currentDefinitions = CompilationPipeline.GetAssemblies().Select(x => x.name);
        foreach (string definition in currentDefinitions)
        {
            if(ignoredDefinitions.Contains(definition))
                continue;
            var defPath = CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName(definition);
            File.Delete(Path.Combine(rootDirectory.FullName, definition + ".csproj"));
            // If no definitions exist, there are no more files to delete.
            if (defPath == null)
                continue;
            File.Delete(defPath);
            File.Delete(defPath + ".meta");
        }
    }

    private static void CreateDefinitions()
    {
        var id = ModConfig.ID;
        var newDef = AssemblyDefinition.CreateDefinition(id);
        var editorDef = AssemblyDefinition.CreateEditorDefinition(id);
        File.WriteAllText(newDef.filePath, JsonConvert.SerializeObject(newDef, Formatting.Indented));
        File.WriteAllText(editorDef.filePath, JsonConvert.SerializeObject(editorDef, Formatting.Indented));
        Updatesln();
        // Refresh the assets in Unity so that you can see the new assembly definitions.
        // Note that the project needs to be refreshed in the IDE after changing/adding assembly definitions. This can be done automatically through the "Open C# Project" option in Unity.
        AssetDatabase.Refresh();
    }

    // Manually change the startup project in the generated sln, as Unity does not change this after creating an assembly definition.
    private static void Updatesln()
    {
        if(!File.Exists(slnFile))
            Initsln();
        var sln = File.ReadAllText(slnFile);
        sln = Regex.Replace(sln, @"StartupItem = .+\.csproj", "StartupItem = " + ModConfig.ID + ".csproj");
        File.WriteAllText(slnFile, sln);
    }
    
    //Force Unity to generate the solution file
    private static void Initsln()
    {
        System.Reflection.Assembly.GetAssembly(typeof(MonoScript)).GetType("UnityEditor.SyncVS").GetMethod("SyncSolution", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[0]);
    }

    // Unity does not supply an option to generate assembly definitions through code, so we must create our own.
    internal class AssemblyDefinition
    {
        internal static AssemblyDefinition CreateDefinition(string id)
        {
            var asmDef = new AssemblyDefinition
            {
                name = id,
                _filePath = Path.Combine(Application.dataPath, id + ".asmdef")
            };
            if(!string.IsNullOrEmpty(CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName("GameProxies")))
                asmDef.references = new[] { "GameProxies" };
            return asmDef;
        }

        internal static AssemblyDefinition CreateEditorDefinition(string id)
        {
            return new AssemblyDefinition
            {
                name = id + "-Editor",
                references = new [] { id },
                // Create an assembly definition for the editor so that editor scripts do not get compiled into the player.
                // Assembly-CSharp does this automatically but this is overwritten if an assembly definition is created in the /Assets folder.
                includePlatforms = new [] { "Editor" },
                _filePath = Path.Combine(editorPath, id + "-Editor.asmdef")
            };
        }

        internal static AssemblyDefinition CreateProxyDefinition()
        {
            return new AssemblyDefinition
            {
                name = "GameProxies",
                _filePath = Path.Combine(Application.dataPath, "Scripts/GameProxies/GameProxies.asmdef")
            };
        }

        private string _filePath;
        internal string filePath
        {
            get
            {
                return _filePath;
            }
        }
        public string name;
        public string[] references = new string[0];
        public string[] includePlatforms = new string[0];
        public string[] excludePlatforms = new string[0];
    }
}

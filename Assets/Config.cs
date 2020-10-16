using System.Collections.Generic;

public class Config
{
    public bool ExcludeVanillaModules;
    public bool ExcludeIgnoredModules;
    public bool ExcludeMysteryModule;
    public bool ExcludeSouvenir;

    /// <summary>Checks if a modded(!) module is excluded. This function does not check for vanilla modules.</summary>
    public bool IsExcluded(KMBombModule module, HashSet<string> ignoredModules)
    {
        switch (module.ModuleType)
        {
            case "mysterymodule": return ExcludeMysteryModule;
            case "SouvenirModule": return ExcludeSouvenir;
        }
        return ExcludeIgnoredModules && ignoredModules.Contains(module.ModuleDisplayName);
    }

    public static readonly Dictionary<string, object>[] TweaksEditorSettings = new Dictionary<string, object>[]
    {
        new Dictionary<string, object>
        {
            { "Filename", "Souvenir-settings.txt" },
            { "Name", "Souvenir" },
            { "Listings", new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object> { { "Key", "ExcludeVanillaModules" }, { "Text", "Exclude vanilla modules" }, { "Description", "avoid questions about vanilla modules" } },
                    new Dictionary<string, object> { { "Key", "ExcludeIgnoredModules" }, { "Text", "Exclude ignored modules" }, { "Description", "avoid questions about boss modules (except other Souvenirs)" } },

                    new Dictionary<string, object> { { "Text", "Exclude specific modules" }, { "Type", "Section" } },
                    new Dictionary<string, object> { { "Key", "ExcludeMysteryModule" }, { "Text", "Mystery Module" } },
                    new Dictionary<string, object> { { "Key", "ExcludeSouvenir" }, { "Text", "Souvenir" }, { "Description", "avoid questions about other Souvenirs on the same bomb" } }
                }
            }
        }
    };
}

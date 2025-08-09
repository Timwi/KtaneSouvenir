using System.Collections.Generic;
using System.Linq;
using Souvenir;

public class Config
{
    public bool ExcludeVanillaModules;
    public bool ExcludeIgnoredModules;
    public bool ExcludeMysteryModule;
    public bool ExcludeSouvenir;
    public bool IgnoreTpAutosolvers;
    public string Language;

    /// <summary>Checks if a modded(!) module is excluded. This function does not check for vanilla modules.</summary>
    public bool IsExcluded(KMBombModule module, HashSet<string> ignoredModuleIDs) => module.ModuleType switch
    {
        "mysterymodule" => ExcludeMysteryModule,
        "SouvenirModule" => ExcludeSouvenir,
        var id => ExcludeIgnoredModules && ignoredModuleIDs.Contains(id),
    };

    public static readonly Dictionary<string, object>[] TweaksEditorSettings = Ut.NewArray(
        new Dictionary<string, object>
        {
            ["Filename"] = "Souvenir-settings.txt",
            ["Name"] = "Souvenir",
            ["Listings"] = Ut.NewList(
                new Dictionary<string, object> { ["Key"] = "ExcludeVanillaModules", ["Text"] = "Exclude vanilla modules", ["Description"] = "avoid questions about vanilla modules" },
                new Dictionary<string, object> { ["Key"] = "ExcludeIgnoredModules", ["Text"] = "Exclude ignored modules", ["Description"] = "avoid questions about boss modules (except other Souvenirs)" },
                new Dictionary<string, object> { ["Key"] = "IgnoreTpAutosolvers", ["Text"] = "Ignore TP autosolvers", ["Description"] = "ask questions about modules even if they were autosolved in TP (useful for debugging)" },
                new Dictionary<string, object> { ["Key"] = "Language", ["Text"] = "Language", ["Type"] = "Dropdown", ["DropdownItems"] = new string[] { "en" }.Concat(TranslationInfo.AllTranslations.Keys).Cast<object>().ToList() },
                new Dictionary<string, object> { ["Text"] = "Exclude specific modules", ["Type"] = "Section" },
                new Dictionary<string, object> { ["Key"] = "ExcludeMysteryModule", ["Text"] = "Mystery Module" },
                new Dictionary<string, object> { ["Key"] = "ExcludeSouvenir", ["Text"] = "Souvenir", ["Description"] = "avoid questions about other Souvenirs on the same bomb" })
        }
    );
}

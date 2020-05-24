using System.Collections.Generic;
using Newtonsoft.Json;

public class Config
{
    [JsonIgnore]
    public HashSet<string> ExcludedModules = new HashSet<string>();

    public bool ExcludeVanillaModules;
    public bool ExcludeIgnoredModules;

    // Non-ignored mini-boss modules
    public bool ExcludeMinibossModules { get; set; }
    public bool ExcludeMysteryModule { get { return ExcludedModules.Contains("mysterymodule"); } set { SetExcludedModule("mysterymodule", value); } }
    public bool ExcludeSouvenir { get { return ExcludedModules.Contains("SouvenirModule"); } set { SetExcludedModule("SouvenirModule", value); } }

    public static readonly Dictionary<string, object>[] TweaksEditorSettings = new Dictionary<string, object>[]
    {
        new Dictionary<string, object>
        {
            { "Filename", "Souvenir-settings.txt" },
            { "Name", "Souvenir" },
            { "Listings", new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object> { { "Key", "ExcludeVanillaModules" }, { "Text", "Exclude vanilla modules" } },
                    new Dictionary<string, object> { { "Key", "ExcludeIgnoredModules" }, { "Text", "Exclude ignored modules" }, { "Description", "e.g. Forget The Colors" } },

                    new Dictionary<string, object> { { "Text", "Exclude mini-boss modules" }, { "Type", "Section" } },
                    new Dictionary<string, object> { { "Key", "ExcludeMinibossModules" }, { "Text", "(Exclude all)" }, { "Description", "Overrides the settings below if set." } },
                    new Dictionary<string, object> { { "Key", "ExcludeMysteryModule" }, { "Text", "Mystery Module" } },
                    new Dictionary<string, object> { { "Key", "ExcludeSouvenir" }, { "Text", "Souvenir" } }
                }
            }
        }
    };

    public void UpdateExcludedModules()
    {
        if (ExcludeMinibossModules)
        {
            ExcludeMysteryModule = true;
            ExcludeSouvenir = true;
        }
    }

    public void SetExcludedModule(string moduleType, bool exclude)
    {
        if (exclude) ExcludedModules.Add(moduleType);
        else ExcludedModules.Remove(moduleType);
    }
}
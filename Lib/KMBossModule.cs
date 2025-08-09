using System;
using System.Collections.Generic;
using Souvenir;
using UnityEngine;

public class KMBossModule : MonoBehaviour
{
    public string[] GetIgnoredModuleIDs(KMBombModule module, string[] @default = null) =>
        GetIgnoredModules(module.ModuleType, @default, ids: true);

    public string[] GetIgnoredModules(KMBombModule module, string[] @default = null, bool ids = false) =>
        GetIgnoredModules(module.ModuleDisplayName, @default, ids);

    public string[] GetIgnoredModules(string moduleName, string[] @default = null, bool ids = false)
    {
        if (Application.isEditor)
            return @default ?? [];

        var bossModuleManagerAPIGameObject = GameObject.Find("BossModuleManager");
        if (bossModuleManagerAPIGameObject == null) // Boss Module Manager is not installed
        {
            Debug.LogFormat(@"[KMBossModule] Boss Module Manager is not installed.");
            return @default ?? [];
        }

        var key = ids ? "GetIgnoredModuleIDs" : "GetIgnoredModules";
        var bossModuleManagerAPI = bossModuleManagerAPIGameObject.GetComponent<IDictionary<string, object>>();
        if (bossModuleManagerAPI == null || !bossModuleManagerAPI.ContainsKey(key))
        {
            Debug.LogFormat(@"[KMBossModule] Boss Module Manager does not have a list on record for “{0}”.", moduleName);
            return @default ?? [];
        }

        var list = ((Func<string, string[]>) bossModuleManagerAPI[key])(moduleName);
        Debug.LogFormat(@"[KMBossModule] Boss Module Manager returned list for “{0}”: {1}", moduleName, list == null ? "<null>" : list.JoinString(", "));
        return list ?? @default ?? [];
    }
}

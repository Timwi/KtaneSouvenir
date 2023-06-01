using System;
using System.Reflection;
using UnityEngine;
using KModkit;

//Methods for working around bugs in the game
public static class GameFixes
{
#region Double KMSelectable.OnDefocus fix
    /* Bug description:
     * When a selectable loses focus, the game calls the KMSelectable.OnDefocus delegate twice when it should only do so once.
     *
     * References: (KTANE Discord Server: https://discord.gg/ktane)
     *      (occurrence) https://discord.com/channels/160061833166716928/201105291830493193/928710731044646982
     *      (cause) https://discord.com/channels/160061833166716928/201105291830493193/928741113634906152
     *
     * The workaround is introducing a boolean variable that gets toggled on every OnDefocus call, and will be used so to ignore every other call.
     * The function below takes the action that would normally be added to KMSelectable.OnDefocus, wraps it in another one which accounts for the double calls and returns it, the result then can be added to KMSelectable.OnDefocus.
     */
    
    public static Action OnDefocus(Action action)
    {
#if UNITY_EDITOR        //KMSelectable.OnDefocus works properly in TestHarness
        return action;
#else
        var call = false;
        return () =>
        {
            call ^= true;
            if (call)
                action();
        };
#endif
    }
    
#endregion
    

#region KMSelectable.UdpateChildren fix
    /*
     * Bug description:
     * Adding children to a KMSelectable by instantiating prefabs with KMSelectable components on them doesn't work.
     * When the game loads a mod, it adds the internal types to all of the prefabs in it for all the proxy types (KM...) it founds (here: KMSelectable -> ModSelectable).
     * The internal ModSelectable type has a method called "CopySettingsFromProxy" which copies the settings from the KMSelectable type to the ModSelectable type which are required for it to work properly.
     * On an instantiated prefab, these settings will most likely change (the one that really matters here is the parent setting),
     *  but the internal ModSelectable.OnUpdateChildren method would only update these settings if the selectable wouldn't have the ModSelectable component, but the component got added when the mod was laoded, thus the settings are never updated.
     *
     * Reference: (KTANE Discord Server: https://discord.gg/ktane)
     *  https://discord.com/channels/160061833166716928/201105291830493193/928755335626387516
     *
     * The fix for this is simply calling ModSelectable.CopySettingsFromProxy manually for the children (and root selectable if its settings were changed) before calling KMSelectable.UdpateChildren using reflection.
     * The extension methods below can be used to call KMSelectable.UpdateChildren with updating the settings automatically before (UpdateChildrenProperly), and for just updating the settings in general (UpdateSettings).
     */

#pragma warning disable 649
    private static Type ModSelectableType;
    private static MethodInfo CopySettingsFromProxyMethod;
#pragma warning restore 649
    
    public static void UpdateChildrenProperly(this KMSelectable selectable, KMSelectable childToSelect = null)
    {
        if(selectable == null)
            return;
        foreach (var child in selectable.Children)
            child.UpdateSettings();
        selectable.UpdateSettings();
        selectable.UpdateChildren(childToSelect);
    }

    public static void UpdateSettings(this KMSelectable selectable)
    {
        if (selectable != null && CopySettingsFromProxyMethod != null)
            CopySettingsFromProxyMethod.Invoke(
                selectable.GetComponent(ModSelectableType) ?? selectable.gameObject.AddComponent(ModSelectableType),
                new object[0]);
    }
    
#endregion
    

#if !UNITY_EDITOR
    static GameFixes()
    {
        ModSelectableType = ReflectionHelper.FindGameType("ModSelectable");
        if (ModSelectableType != null)
            CopySettingsFromProxyMethod =
                ModSelectableType.GetMethod("CopySettingsFromProxy", BindingFlags.Public | BindingFlags.Instance);
    }
#endif
}

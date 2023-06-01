using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[HelpURL("https://github.com/Qkrisi/ktanemodkit/wiki/AssemblyShare")]
[AddComponentMenu("")]
public class AssemblyShare : MonoBehaviour
{
    public Dictionary<string, object> SharedObjects = new Dictionary<string, object>();
    private static Dictionary<string, object> _SharedObjectsInstance;

    private static Dictionary<string, object> SharedObjectsInstance
    {
        get
        {
            LoadInstance();
            return _SharedObjectsInstance;
        }
    }

    private static void LoadInstance()
    {
        if (_SharedObjectsInstance != null)
            return;
        var obj = GameObject.Find("KMAssemblyShareObject");
        if (obj == null)
        {
            obj = new GameObject("KMAssemblyShareObject");
            DontDestroyOnLoad(obj);
        }
        var assemblyShare = obj.GetComponent("AssemblyShare") ?? obj.AddComponent<AssemblyShare>();
        var sharedObjectsField = assemblyShare.GetType()
            .GetField("SharedObjects", BindingFlags.Public | BindingFlags.Instance);
        _SharedObjectsInstance = (Dictionary<string, object>)sharedObjectsField.GetValue(assemblyShare);
    }

    public static void SetValue(string key, object value)
    {
        SharedObjectsInstance[key] = value;
    }
    
    public static void Add(string key, object value)
    {
        SharedObjectsInstance.Add(key, value);
    }

    public static void SetOrAdd(string key, object value)
    {
        if(ContainsKey(key))
            SetValue(key, value);
        else Add(key, value);
    }

    public static T GetValue<T>(string key)
    {
        return (T)SharedObjectsInstance[key];
    }
    
    public static bool TryGetValue<T>(string key, out T value)
    {
        object _value;
        var success = SharedObjectsInstance.TryGetValue(key, out _value);
        value = success ? (T)_value : default(T);
        return success;
    }

    public static T GetOrAdd<T>(string key, T defaultValue)
    {
        T value;
        if (!TryGetValue(key, out value))
        {
            value = defaultValue;
            Add(key, value);
        }
        return value;
    }

    public static bool ContainsKey(string key)
    {
        return SharedObjectsInstance.ContainsKey(key);
    }
}
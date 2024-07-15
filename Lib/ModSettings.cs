using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

class ModSettings<T> where T : new()
{
    public ModSettings(string filename, Action<Exception> onRead = null)
    {
        var settingsFolder = Path.Combine(Application.persistentDataPath, "Modsettings");
        // The persistent data path is different in the editor compared to the real game, so the Modsettings folder might not exist.
        if (Application.isEditor && !Directory.Exists(settingsFolder))
            Directory.CreateDirectory(settingsFolder);

        settingsPath = Path.Combine(settingsFolder, filename + ".txt");
        OnRead = onRead;
    }

    private readonly string settingsPath;

    /// <summary>Serializes settings the same way it's written to the file. Supports settings that use enums.</summary>
    public static string SerializeSettings(T settings)
    {
        return JsonConvert.SerializeObject(settings, Formatting.Indented, new StringEnumConverter());
    }

    private static readonly object settingsFileLock = new object();

    /// <summary>Whether or not there has been a successful read of the settings file.</summary>
    public bool SuccessfulRead;
    /// <summary>Called every time the settings are read. Parameter is null if the read was successful or an exception if it wasn't.</summary>
    public Action<Exception> OnRead;

    /// <summary>
    /// Reads the settings from the settings file.
    /// If the settings couldn't be read, the default settings will be returned.
    /// </summary>
    public T Read()
    {
        try
        {
            lock (settingsFileLock)
            {
                if (!File.Exists(settingsPath))
                {
                    File.WriteAllText(settingsPath, SerializeSettings(new T()));
                }

                string text = File.ReadAllText(settingsPath);
                T deserialized = JsonConvert.DeserializeObject<T>(text) ?? throw new Exception(settingsPath + " should not be empty.");

                SuccessfulRead = true;

                // Update the file if settings have been added or removed by checking if the original serialized settings matches the new serialized settings.
                if (text != SerializeSettings(deserialized))
                    Write(deserialized);
                OnRead?.Invoke(null);
                return deserialized;
            }
        }
        catch (Exception e)
        {
            Debug.LogFormat("An exception has occurred while attempting to read the settings from {0}\nDefault settings will be used for the type of {1}.\nDelete the file to restore settings the next time this type is used.", settingsPath, typeof(T).ToString());
            Debug.LogException(e);

            SuccessfulRead = false;
            OnRead?.Invoke(e);
            return new T();
        }
    }

    /// <summary>
    /// Writes the settings to the settings file.
    /// To protect the user settings, this does nothing if the last read wasn't successful.
    /// </summary>
    public void Write(T value)
    {
        if (!SuccessfulRead)
            return;

        lock (settingsFileLock)
        {
            try
            {
                File.WriteAllText(settingsPath, SerializeSettings(value));
            }
            catch (Exception e)
            {
                Debug.LogFormat("Failed to write to {0}", settingsPath);
                Debug.LogException(e);
            }
        }
    }

    public override string ToString()
    {
        return SerializeSettings(Read());
    }
}
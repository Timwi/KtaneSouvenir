using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

namespace Souvenir
{
    /// <summary>
    ///     Static utilities for generating <see cref="AudioClip"/>s. Clips will be generated with `ForeignAudioID =
    ///     Sounds.Generated`.</summary>
    public static class Sounds
    {
        public const string Generated = "\uE047generated";

        /// <summary>Represents an <see cref="AudioClip"/> played at a specific time.</summary>
        public readonly struct AudioPosition
        {
            /// <summary>The clip to play.</summary>
            public readonly AudioClip Clip;
            /// <summary>The time to play the clip at.</summary>
            public readonly float Time;

            public AudioPosition(AudioClip clip, float time)
            {
                Clip = clip;
                Time = time;
            }

            public static implicit operator AudioPosition((AudioClip, float) tup) => new(tup.Item1, tup.Item2);
            public static implicit operator AudioPosition((float, AudioClip) tup) => new(tup.Item2, tup.Item1);
            public static implicit operator AudioPosition(AudioClip clip) => new(clip, 0f);

            public readonly int EmptySize => (int) (Time * Clip.frequency) * Clip.channels;
            public readonly int DataSize => Clip.samples * Clip.channels;
            public readonly int BufferSize => EmptySize + DataSize;
        }

        /// <summary>
        ///     Combines multiple audio clips together via addition. Each clip is assumed to have the same sample rate and
        ///     channel count.</summary>
        /// <remarks>
        ///     See <see cref="SouvenirModule.ProcessSynesthesia(ModuleData)"/> for an example of how to use this.</remarks>
        /// <param name="name">
        ///     The name of the final clip.</param>
        /// <param name="clips">
        ///     The clips to combine.</param>
        /// <returns>
        ///     The new audio clip.</returns>
        public static AudioClip Combine(string name, params AudioPosition[] clips) => Combine(clips, name, null);

        /// <summary>
        ///     Combines multiple audio clips together via addition. Each clip is assumed to have the same sample rate and
        ///     channel count. Any sound after the specified length is ignored.</summary>
        /// <remarks>
        ///     See <see cref="SouvenirModule.ProcessSynesthesia(ModuleData)"/> for an example of how to use this.</remarks>
        /// <param name="name">
        ///     The name of the final clip.</param>
        /// <param name="name">
        ///     The length of the final clip.</param>
        /// <param name="clips">
        ///     The clips to combine.</param>
        /// <returns>
        ///     The new audio clip.</returns>
        public static AudioClip Combine(string name, float length, params AudioPosition[] clips) => Combine(clips, name, length);

        private static AudioClip Combine(AudioPosition[] clips, string name, float? length)
        {
            if (clips is not { Length: > 0 }) throw new ArgumentException("No clips provided.", "clips");

            var bufferSize = length is null
                ? clips.Select(c => c.BufferSize).Max()
                : (int) (length * clips[0].Clip.frequency * clips[0].Clip.channels);
            var buffer = new float[bufferSize];

            foreach (var c in clips)
            {
                var offset = c.EmptySize;
                var count = Mathf.Min(c.DataSize, bufferSize - offset);
                var data = new float[count];
                c.Clip.LoadAudioData();
                c.Clip.GetData(data, 0);
                for (var i = 0; i < count; i++)
                    buffer[offset + i] += data[i];
            }

            for (int i = 0; i < bufferSize; i++)
                buffer[i] = Mathf.Clamp(buffer[i], -1f, 1f);

            var clip = AudioClip.Create(name, bufferSize / clips[0].Clip.channels, clips[0].Clip.channels, clips[0].Clip.frequency, false);
            clip.SetData(buffer, 0);
            return clip.Inject();
        }

        /// <summary>
        ///     Generates an empty audio clip of the given length.</summary>
        /// <param name="length">
        ///     The length of the clip in seconds.</param>
        /// <param name="name">
        ///     The name of the clip.</param>
        /// <returns>
        ///     The audio clip.</returns>
        public static AudioClip Empty(float length, string name = "Empty") =>
            AudioClip.Create(name, (int) (length * 44100), 1, 44100, true, (arr) => Array.Clear(arr, 0, arr.Length)).Inject();

        private static Action<List<AudioClip>, string> _injectAudio;
        private static AudioClip Inject(this AudioClip clip)
        {
            if (_injectAudio is null)
            {
                var type = Type.GetType("Mod, Assembly-CSharp");
                var nullMod = Expression.Constant(Activator.CreateInstance(type, ""), type);
                var method = type.GetMethod("AddAudioClipsToGroup", BindingFlags.Instance | BindingFlags.NonPublic);
                var arg1 = Expression.Parameter(typeof(List<AudioClip>), "audioClips");
                var arg2 = Expression.Parameter(typeof(string), "groupName");
                var expr = Expression.Call(nullMod, method, arg1, arg2, Expression.Constant(32, typeof(int)));
                _injectAudio = Expression.Lambda<Action<List<AudioClip>, string>>(expr, arg1, arg2).Compile();
            }
            _injectAudio(new List<AudioClip>() { clip }, $"{Generated}_{clip.name}");
            return clip;
        }

        /// <summary>
        ///     Play a sound from another mod. The sound does not loop.</summary>
        /// <param name="foreignAudioID">
        ///     The mod ID of the mod.</param>
        /// <param name="name">
        ///     The name of the audio clip.</param>
        /// <param name="transform">
        ///     The transform to play the sound at.</param>
        /// <returns>
        ///     A reference to stop the sound early.</returns>
        public static KMAudio.KMAudioRef PlayForeignClip(string foreignAudioID, string name, Transform transform)
        {
            var aref = new KMAudio.KMAudioRef();
            var result = Type.GetType("DarkTonic.MasterAudio.MasterAudio, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null")
                .GetMethod("PlaySound3DAtTransform", BindingFlags.Public | BindingFlags.Static)
                .Invoke(null, new object[] { $"{foreignAudioID}_{name}", transform, 1f, null, 0f, null, false, false });
            // Skip setting loop = true since we don't want that anyways
            aref.StopSound += () =>
            {
                var variation = result?.GetType().GetProperty("ActingVariation", BindingFlags.Public | BindingFlags.Instance)
                    ?.GetValue(result, new object[0]);
                variation?.GetType().GetMethod("Stop", BindingFlags.Public | BindingFlags.Instance)
                    ?.Invoke(variation, new object[] { false, false });
            };
            return aref;
        }

        private static Func<string, AudioClip> _getForeign;
        /// <summary>
        ///     Get an <see cref="AudioClip"/> from another mod.</summary>
        /// <param name="foreignAudioID">
        ///     The mod ID of the mod.</param>
        /// <param name="name">
        ///     The name of the audio clip.</param>
        /// <returns>
        ///     The clip.</returns>
        public static AudioClip GetForeignClip(string foreignAudioID, string name)
        {
            if (_getForeign is null)
            {
                // fullName => DarkTonic
                //          .MasterAudio
                //          .MasterAudio
                //          .AudioSourcesBySoundType[fullName]
                //          .Sources[0]
                //          .Source
                //          .clip

                var tMasterAudio = Type.GetType("DarkTonic.MasterAudio.MasterAudio, Assembly-CSharp");
                var fldDict = tMasterAudio.GetField("AudioSourcesBySoundType", BindingFlags.Static | BindingFlags.NonPublic);
                var dictIndex = typeof(IDictionary).GetProperties(BindingFlags.Public | BindingFlags.Instance).First(p => p.GetIndexParameters().Length > 0);
                var tAgi = fldDict.FieldType.GetGenericArguments()[1];
                var fldSources = tAgi.GetField("Sources", BindingFlags.Public | BindingFlags.Instance);
                var listIndex = typeof(IList).GetProperties(BindingFlags.Public | BindingFlags.Instance).First(p => p.GetIndexParameters().Length > 0);
                var tAi = fldSources.FieldType.GetGenericArguments()[0];
                var fldSource = tAi.GetField("Source", BindingFlags.Public | BindingFlags.Instance);
                var propClip = typeof(AudioSource).GetProperty("clip", BindingFlags.Public | BindingFlags.Instance);

                var dict = fldDict.GetValue(null);
                var param = Expression.Parameter(typeof(string), "fullName");
                var agi = Expression.Call(Expression.Constant(dict), dictIndex.GetGetMethod(), param);
                var sources = Expression.Field(Expression.Convert(agi, tAgi), fldSources);
                var ai = Expression.Call(sources, listIndex.GetGetMethod(), Expression.Constant(0, typeof(int)));
                var source = Expression.Field(Expression.Convert(ai, tAi), fldSource);
                var clip = Expression.Property(source, propClip);
                var lambda = Expression.Lambda<Func<string, AudioClip>>(clip, param);
                _getForeign = lambda.Compile();
            }

            return _getForeign($"{foreignAudioID}_{name}");
        }
    }
}

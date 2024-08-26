// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Set by Tweaks through Reflection", Scope = "member", Target = "~F:SouvenirModule.TimeModeAwardPoints")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by Twitch Plays through Reflection", Scope = "member", Target = "~F:SouvenirModule.TwitchHelpMessage")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by Twitch Plays through Reflection", Scope = "member", Target = "~M:SouvenirModule.ProcessTwitchCommand(System.String)~System.Collections.IEnumerator")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by Twitch Plays through Reflection", Scope = "member", Target = "~M:SouvenirModule.TwitchHandleForcedSolve~System.Collections.IEnumerator")]
[assembly: SuppressMessage("Style", "IDE0053:Use expression body for lambda expression", Justification = "I want lambda expressions with side effects (such as assignments) to have a block body")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Unity depends on it")]

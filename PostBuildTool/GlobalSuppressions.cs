// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "Really doesn’t matter")]
[assembly: SuppressMessage("Performance", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.", Justification = "Really doesn’t matter")]
[assembly: SuppressMessage("Style", "IDE0305:Simplify collection initialization", Justification = "Let me use .ToArray if I want to")]

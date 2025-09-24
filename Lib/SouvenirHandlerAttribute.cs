using System;
using System.Collections.Generic;
using System.Reflection;

namespace Souvenir;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class SouvenirHandlerAttribute(string moduleId, string moduleName, Type enumType, string contributor) : Attribute
{
    public string ModuleId { get; } = moduleId;
    public string ModuleName { get; } = moduleName;
    public Type EnumType { get; } = enumType;
    public string Contributor { get; } = contributor;

    public bool AddThe { get; set; }
    public bool IsBossModule { get; set; }
    public MethodInfo Method { get; set; }

    public string ModuleNameWithThe => (AddThe ? "The\u00a0" : "") + ModuleName;
}

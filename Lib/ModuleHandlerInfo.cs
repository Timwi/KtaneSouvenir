using System;
using System.Collections.Generic;
using UnityEngine;

namespace Souvenir;

public struct ModuleHandlerInfo
{
    public Func<ModuleData, IEnumerator<YieldInstruction>> Processor { get; private set; }
    public string ModuleName { get; private set; }
    public string Contributor { get; private set; }

    public ModuleHandlerInfo(Func<ModuleData, IEnumerator<YieldInstruction>> processor, string moduleName, string contributor)
    {
        Processor = processor;
        ModuleName = moduleName;
        Contributor = contributor;
    }

    public static implicit operator ModuleHandlerInfo((Func<ModuleData, IEnumerator<YieldInstruction>> processor, string moduleName, string contributor) value) =>
        new(value.processor, value.moduleName, value.contributor);
}

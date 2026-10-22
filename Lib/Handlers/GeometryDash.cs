using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGeometryDash
{
    [Question("Which level was featured in {0}?", OneColumn4Answers, "A Bizarre Phantasm", "AntiPixel", "ColorZ", "Drowning", "Dry Out", "Dynamic On Track", "Electroman Adventures", "Galatic Fragility", "Grind District", "Lights And Thunder", "Lunar Ocean", "Old Auto Zone", "Problematic", "ReTraY", "Sakupen Hell", "Speed Racer", "Takeoff", "Time Machine", "Uprise", "Valor", "love baba")]
    Level
}

public partial class SouvenirModule
{
    [Handler("geometryDashModule", "Geometry Dash", typeof(SGeometryDash), "Timwi")]
    [ManualQuestion("Which level was featured?")]
    private IEnumerator<SouvenirInstruction> ProcessGeometryDash(ModuleData module)
    {
        var comp = GetComponent(module, "GeometryDashScript");
        yield return WaitForSolve;
        yield return question(SGeometryDash.Level).Answers(GetField<string>(comp, "lvlname").Get());
    }
}

using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPolyhedralMaze
{
    [SouvenirQuestion("What was the starting position in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 61)]
    StartPosition
}

public partial class SouvenirModule
{
    [SouvenirHandler("PolyhedralMazeModule", "Polyhedral Maze", typeof(SPolyhedralMaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessPolyhedralMaze(ModuleData module)
    {
        Dictionary<string, string> nameMapping = new()
        {
            ["4TruncatedDeltoidalIcositetrahedron2"] = "the 4-truncated deltoidal icositetrahedral Polyhedral Maze",
            ["ChamferedDodecahedron1"] = "the chamfered dodecahedral Polyhedral Maze",
            ["ChamferedIcosahedron2"] = "the chamfered icosahedral Polyhedral Maze",
            ["DeltoidalHexecontahedron"] = "the deltoidal hexecontahedral Polyhedral Maze",
            ["DisdyakisDodecahedron"] = "the disdyakis dodecahedral Polyhedral Maze",
            ["JoinedLsnubCube"] = "the joined snub cubic Polyhedral Maze",
            ["JoinedRhombicuboctahedron"] = "the joined rhombicuboctahedral Polyhedral Maze",
            ["LpentagonalHexecontahedron"] = "the pentagonal hexecontahedral Polyhedral Maze",
            ["OrthokisPropelloCube"] = "the orthokis propello cubic Polyhedral Maze",
            ["PentakisDodecahedron"] = "the pentakis dodecahedral Polyhedral Maze",
            ["RectifiedRhombicuboctahedron"] = "the rectified rhombicuboctahedral Polyhedral Maze",
            ["TriakisIcosahedron"] = "the triakis icosahedral Polyhedral Maze",
            ["Rhombicosidodecahedron"] = "the rhombicosidodecahedral Polyhedral Maze",
            ["CanonicalRectifiedLsnubCube"] = "the canonical rectified snub cubic Polyhedral Maze",
        };

        var comp = GetComponent(module, "PolyhedralMazeModule");
        var polyhedron = GetField<object>(comp, "_polyhedron").Get();
        var internalName = GetField<string>(polyhedron, "Name", isPublic: true).Get(s => !nameMapping.ContainsKey(s) ? "Unexpected polyhedron name" : null);
        var souvenirName = nameMapping[internalName];
        _polyhedralMazeTypes.Add(souvenirName);

        yield return WaitForSolve;

        string format = null;
        if (_polyhedralMazeTypes.Count(n => n == souvenirName) == 1 && Rnd.Range(0, 2) != 0)
            format = translateString(SPolyhedralMaze.StartPosition, souvenirName);

        yield return question(SPolyhedralMaze.StartPosition).Answers(GetIntField(comp, "_startFace").Get().ToString());
    }
}

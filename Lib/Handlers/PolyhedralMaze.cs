using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SPolyhedralMaze
{
    [SouvenirQuestion("What was the starting position in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 61)]
    StartPosition,

    [SouvenirDiscriminator("{0}", Arguments = ["the 4-truncated deltoidal icositetrahedral Polyhedral Maze", "the chamfered dodecahedral Polyhedral Maze", "the chamfered icosahedral Polyhedral Maze", "the deltoidal hexecontahedral Polyhedral Maze", "the disdyakis dodecahedral Polyhedral Maze", "the joined snub cubic Polyhedral Maze", "the joined rhombicuboctahedral Polyhedral Maze", "the pentagonal hexecontahedral Polyhedral Maze", "the orthokis propello cubic Polyhedral Maze", "the pentakis dodecahedral Polyhedral Maze", "the rectified rhombicuboctahedral Polyhedral Maze", "the triakis icosahedral Polyhedral Maze", "the rhombicosidodecahedral Polyhedral Maze", "the canonical rectified snub cubic Polyhedral Maze"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    MazeShape
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

        yield return WaitForSolve;
        yield return question(SPolyhedralMaze.StartPosition).Answers(GetIntField(comp, "_startFace").Get().ToString());
        yield return new Discriminator(SPolyhedralMaze.MazeShape, "shape", internalName, args: [souvenirName]);
    }
}

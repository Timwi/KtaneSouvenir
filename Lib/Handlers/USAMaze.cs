using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SUSAMaze
{
    [SouvenirQuestion("Which state did you depart from in {0}?", TwoColumns4Answers, "Alaska", "Alabama", "Arkansas", "Arizona", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", "North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming")]
    Origin
}

public partial class SouvenirModule
{
    [SouvenirHandler("USA", "USA Maze", typeof(SUSAMaze), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessUSAMaze(ModuleData module) => processWorldMaze(module, "USAMaze", Question.USAMazeOrigin);
}
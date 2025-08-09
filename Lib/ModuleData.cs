namespace Souvenir;

public sealed class ModuleData
{
    /// <summary>The actual module component</summary>
    public KMBombModule Module;
    /// <summary>Set to <see langword="true"/> after <see cref="KMBombModule.HandlePass"/> has been called.</summary>
    public bool IsSolved => !Unsolved;
    /// <summary>Set to <see langword="false"/> after <see cref="KMBombModule.HandlePass"/> has been called.</summary>
    public bool Unsolved = true;
    /// <summary>The order in which this module has been solved, or <see langword="0"/> if it is currently unsolved.</summary>
    public int SolveIndex;
}

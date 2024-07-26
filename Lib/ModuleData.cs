namespace Souvenir
{
    public sealed class ModuleData
    {
        /// <summary>The actual module component</summary>
        public KMBombModule Module;
        /// <summary>Set to <code>true</code> after <code>KMBombModule.HandlePass()</code> has been called.</summary>
        public bool IsSolved => !Unsolved;
        /// <summary>Set to <code>false</code> after <code>KMBombModule.HandlePass()</code> has been called.</summary>
        public bool Unsolved = true;
        /// <summary>The order in which this module has been solved, or 0 if it is currently unsolved.</summary>
        public int SolveIndex;
    }
}

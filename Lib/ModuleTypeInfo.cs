using System.Collections.Generic;
using Souvenir;

/// <summary>Contains information about the modules of a particular type (e.g. Hexamaze) on the bomb.</summary>
public class ModuleTypeInfo
{
    /// <summary>Indicates how many of this module type are present on the bomb.</summary>
    public int NumModules;
    /// <summary>Indicates how many of this module type have already been solved.</summary>
    public int NumSolved;
    /// <summary>Indicates how many of the module handlers for this module type have finished.</summary>
    public int NumFinished;
    /// <summary>Keeps track of the discriminators returned by each handler, indexed by their ID.</summary>
    public Dictionary<KMBombModule, Dictionary<string, Discriminator>> Discriminators = [];
}

using System;
using System.Collections.Generic;

namespace Souvenir;

public class EnumEqualityComparer : IEqualityComparer<Enum>
{
    public static readonly EnumEqualityComparer Default = new();
    public bool Equals(Enum e1, Enum e2) => e1 == null ? e2 == null : e1.Equals(e2);
    public int GetHashCode(Enum e) => e.GetType().GetHashCode() * 47 + (int) (object) e;
}

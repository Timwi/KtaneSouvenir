using System;

namespace Souvenir;

/// <summary>Use this on a Souvenir question or discriminator to tell the translation tool to remove it.</summary>
internal class RemoveAttribute : Attribute
{
    public RemoveAttribute()
    {
    }
}

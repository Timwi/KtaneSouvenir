using System;

namespace Souvenir;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class NoDiscriminatorAttribute() : Attribute
{
}

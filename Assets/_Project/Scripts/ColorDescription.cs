using System;
using UnityEngine;

namespace com.ColorSelect
{
    [Serializable]
    public struct ColorDescription : IEquatable<ColorDescription>
    {
        public string Name;
        public Color Color;

        public static bool operator ==(ColorDescription a, ColorDescription b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(ColorDescription a, ColorDescription b)
        {
            return !(a == b);
        }

        public bool Equals(ColorDescription other)
        {
            return Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            return obj is ColorDescription other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
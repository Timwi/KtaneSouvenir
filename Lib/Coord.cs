using System;

namespace Souvenir
{
    /// <summary>Represents a position in an orthogonal, rectangular grid.</summary>
    public class Coord : IEquatable<Coord>
    {
        private readonly int _index;
        private readonly int _width;
        private readonly int _height;

        public int Index => _index;
        public int Width => _width;
        public int Height => _height;
        public int X => _index % _width;
        public int Y => _index / _width;

        public Coord(int width, int height, int index) { _width = width; _height = height; _index = index; }
        public Coord(int width, int height, int x, int y) : this(width, height, x + width * y) { }

        public bool Equals(Coord other) => _width == other._width && _height == other._height && _index == other._index;
        public override bool Equals(object obj) => obj is Coord other && Equals(other);
        public override int GetHashCode() => unchecked(_height * 47 + _width * 23 + _index);

        public override string ToString() => $"{(char) ('A' + X)}{Y + 1} ({_width}×{_height})";
    }
}

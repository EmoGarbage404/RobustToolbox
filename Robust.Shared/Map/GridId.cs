using System;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Serialization;

namespace Robust.Shared.Map
{
    [Serializable, NetSerializable]
    [Obsolete("Use EntityUids instead.")]
    public struct GridId : IEquatable<GridId>
    {
        /// <summary>
        /// An invalid grid ID.
        /// </summary>
        public static readonly GridId Invalid = new(0);

        internal readonly int Value;

        /// <summary>
        /// Constructs a new instance of <see cref="GridId"/>.
        /// </summary>
        /// <remarks>
        /// This should NOT be used in regular code, and is only public for special/legacy
        /// cases. Generally you should only use this for parsing a GridId in console commands
        /// and immediately check if the grid actually exists in the <see cref="IMapManager"/>.
        /// </remarks>
        public GridId(int value)
        {
            Value = value;
        }

        public bool IsValid()
        {
            return Value > 0;
        }

        /// <inheritdoc />
        public bool Equals(GridId other)
        {
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is GridId id && Equals(id);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(GridId a, GridId b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(GridId a, GridId b)
        {
            return !(a == b);
        }

        public static explicit operator int(GridId self)
        {
            return self.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

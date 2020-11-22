using System;

namespace Mmu.Rl.WpfUi.Models
{
    public class State : IEquatable<State>
    {
        public int Row { get; }
        public int Column { get; }

        public State(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public bool Equals(State other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Row == other.Row && Column == other.Column;
        }

        public static bool operator ==(State x, State y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return x.Equals(y);
        }

        public static bool operator !=(State obj1, State obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((State)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
    }
}
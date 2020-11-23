using System;

namespace Mmu.Rl.WpfUi.Domain.Models
{
    public class State : IEquatable<State>
    {
        public State(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Column { get; }
        public int Row { get; }

        public string AsString()
        {
            return $"Row {Row} Column {Column}.";
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

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((State)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
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
    }
}
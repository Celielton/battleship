namespace Xerris_Battleship.Model
{
    public class Position
    {
        public Position(int vertical, char horizontal)
        {
            Vertical = vertical;
            Horizontal = horizontal.ToString().ToUpper()[0];
        }

        public int Vertical { get; set; }
        public char Horizontal { get; set; }

        public override bool Equals(object obj)
        {
            return ((Position)obj).Horizontal == Horizontal && ((Position)obj).Vertical == Vertical;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

using Xerris_Battleship.Model;

namespace Xerris_Battleship.Interfaces
{
    public interface IShip
    {
        bool CheckHit(Position shot);
        bool IsSunk();
        bool IsValid();
        bool IsReShot(Position shot);
        void AddTry(Position shot, bool onTarget);
    }
}

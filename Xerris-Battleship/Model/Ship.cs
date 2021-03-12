using System.Collections.Generic;
using System.Linq;
using Xerris_Battleship.Enumerators;
using Xerris_Battleship.Interfaces;

namespace Xerris_Battleship.Model
{
    public class Ship : IShip
    {
        public Ship(string playerName, List<Position> position, GameLevel level = GameLevel.Easy)
        {
            PlayerName = playerName;
            Level = level;
            _position = position;
            _shots = new List<Position>();
            _onTarget = new List<Position>();

            Orientation = position.Select(x => x.Horizontal).Distinct().Count() > 1 ? Orientation.Horizontal : Orientation.Vertical;
        }
        public string PlayerName { get; private set; }
        public Orientation Orientation { get; private set; }
        public int Hits { get; private set; }
        private List<Position> _position { get; set; }
        public IReadOnlyCollection<Position> Position => _position;
        private List<Position> _shots { get; set; }
        public IReadOnlyCollection<Position> Shots => _shots;

        private List<Position> _onTarget { get; set; }
        public IReadOnlyCollection<Position> OnTarget => _onTarget;
        public GameLevel Level { get; private set; }

        private void AddHit()
        {
            Hits += 1;
        }

        public bool IsSunk()
        {
            return Hits >= (int)Level;
        }

        public bool CheckHit(Position shot)
        {
            if (Position.Any(a => a.Equals(shot)))
            {
                AddHit();
                return true;
            }

            return false;
        }

        public bool IsValid()
        {
            if (Orientation == Orientation.Horizontal)
            {
                var pos = _position.Select(s => (int)s.Horizontal).OrderByDescending(s => s).ToArray();
                return !string.IsNullOrWhiteSpace(PlayerName) && _position.Select(x => x.Vertical).Distinct().Count() == 1
                                                            && (pos[0] - 1 == pos[1] && pos[1] - 1 == pos[2]);
            }
            else
            {
                var pos = _position.Select(s => s.Vertical).OrderByDescending(s => s).ToArray();
                return !string.IsNullOrWhiteSpace(PlayerName) && _position.Select(x => (int)x.Horizontal).Distinct().Count() == 1
                                && (pos[0] - 1 == pos[1] && pos[1] - 1 == pos[2]);
            }
        }

        public bool IsReShot(Position shot)
        {
            return Shots.Any(s => s.Equals(shot));
        }

        public void AddTry(Position shot, bool onTarget)
        {
            _shots.Add(shot);
            if (onTarget)
                _onTarget.Add(shot);
        }

    }
}

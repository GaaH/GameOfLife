
using System.Collections.Generic;
using System.Linq;

namespace gameoflife
{
    public enum CellState { Alive, Dead }

    public class Cell
    {
        public CellState State { get; set; }

        private IEnumerable<Cell> neighborhood;
        public IEnumerable<Cell> Neighborhood
        {
            get
            {
                if (neighborhood == null)
                {
                    neighborhood = OuterWorld.Neighbors(this);
                }
                return neighborhood;
            }
        }
        public Grid OuterWorld { get; }

        public bool IsAlive => State == CellState.Alive;
        public bool IsDead => State == CellState.Dead;

        public Cell(CellState state, Grid world)
        {
            State = state;
            OuterWorld = world;
        }

        public void NextState()
        {
            var alive_neighbors_count = Neighborhood.Count(x => x.IsAlive);
            var new_state = CellState.Dead;

            if (State == CellState.Alive)
            {
                if (alive_neighbors_count == 2 || alive_neighbors_count == 3)
                {
                    new_state = CellState.Alive;
                }
            }
            else
            {
                if (alive_neighbors_count == 3)
                {
                    new_state = CellState.Alive;
                }
            }

            State = new_state;
        }
    }
}
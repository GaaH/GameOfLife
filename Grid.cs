
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gameoflife
{
    public class Grid
    {
        private ICollection<Cell> Cells;
        public uint Width { get; }
        public uint Height { get; }
        public uint Length { get; }

        public Grid(uint width, uint height, double alive_rate = 0.1)
        {
            var rng = new Random();

            Width = width;
            Height = height;
            Length = width * height;
            var states = Enumerable.Range(0, (int)Length)
                .Select(_ => rng.NextDouble())
                .Select(p => p <= alive_rate ? CellState.Alive : CellState.Dead);
            Cells = states.Select(s => new Cell(s, this)).ToArray();
        }

        public IEnumerable<Cell> Neighbors(Cell cell)
        {
            var (copy, ind) = Cells
                .Select((c, i) => (c, (uint)i))
                .First(tup => tup.Item1.Equals(cell));

            var (x, y) = IndexToCoordinates(ind);

            var indices = new List<int>();
            foreach (var ix in new int[] {-1, 0, 1})
            {
                var nx = x + ix;

                // If out of bound, skip to next iteration
                if (nx < 0 || nx >= Width)
                {
                    continue;
                }

                foreach (var iy in new int[] {-1, 0, 1})
                {
                    var ny = y + iy;

                    // If same cell, skip to next iteration
                    if (nx == 0 && ny == 0)
                    {
                        continue;
                    }

                    // If out of bound, skip to next iteration
                    if (ny < 0 || ny >= Height)
                    {
                        continue;
                    }

                    indices.Add((int)CoordinatesToIndex((uint)nx, (uint)ny));
                }
            }

            return indices.Select(i => Cells.ElementAt(i));
        }

        public (uint x, uint y) IndexToCoordinates(uint index)
        {
            var x = index % Width;
            var y = index / Width;
            return (x, y);
        }

        public uint CoordinatesToIndex(uint x, uint y)
        {
            return y * Width + x;
        }

        public void NextStep()
        {
            foreach (var c in Cells)
            {
                c.NextState();
            }
        }

        override public string ToString()
        {
            var sb = new StringBuilder();
            var enumerator = Cells.GetEnumerator();

            sb.AppendLine(new String('#', (int)Width + 2));

            for (var y = 0 ; y < Height ; ++y)
            {
                sb.Append('#');
                for (var x = 0 ; x < Width ; ++x)
                {
                    enumerator.MoveNext();
                    var c = enumerator.Current;
                    sb.Append(c.IsAlive ? '@' : ' ');
                }
                sb.Append("#\n");
            }

            sb.AppendLine(new String('#', (int)Width + 2));
            return sb.ToString();
        }
    }
}
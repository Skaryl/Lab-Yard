using UnityEngine;

namespace LecureyGames {
    public class SubGrid {
        protected const int DEFAULT_SUBGRID_SIZE = 100;


        protected int size;
        public int Size => size;
        protected Cell[,] cells;




        public SubGrid(int size = DEFAULT_SUBGRID_SIZE) : this() {
            this.size = size;
            cells = new Cell[size, size];
        }

        public SubGrid() {
        }

        public Cell GetCell(int row, int column) => cells[row, column];
        public void SetCell(int row, int column, Cell cell) => cells[row, column] = cell;

        public Vector2Int GetCellPosition(int row, int column) => new Vector2Int(row, column);
    }

    public class SubGridPosition {
        public int Row { get; set; }
        public int Column { get; set; }


    }
}
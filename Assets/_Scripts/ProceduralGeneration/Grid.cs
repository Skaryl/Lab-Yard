using UnityEngine;

namespace LecureyGames {
    public class Grid {
        [SerializeField] protected SubGrid[,] subGrids;
        [SerializeField] protected int rows;
        [SerializeField] protected int columns;

        protected Grid(int rows, int columns) {
            this.rows = rows;
            this.columns = columns;
            subGrids = new SubGrid[rows, columns];
        }

        public SubGrid GetSubGrid(int row, int column) {
            return subGrids[row, column];
        }

    }
}
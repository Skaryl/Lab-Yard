public class Cell {
    public bool IsWater { get; set; }
    public CellType Type { get; set; }

    public Cell(bool isWater) {
        this.IsWater = isWater;
        Type = isWater ? CellType.Water : CellType.Grass;
    }
}

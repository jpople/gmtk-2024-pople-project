using System;
using System.Collections.Generic;
using Godot;
public partial class GridCell : Node {

	private GridCellContents contents;
	private GridCell[] neighbors;
	private List<Enemy> enemies;
	private int row, column;

	public static Dictionary<GridCellContents, char> ASCIIMap = new() {
		{GridCellContents.Empty, '.'},
		{GridCellContents.Jewel, '*'},
		{GridCellContents.Block, 'X'}
	};

	public GridCell() {
		this.contents = GridCellContents.Empty;
		neighbors = new GridCell[Enum.GetValues(typeof(GridDirection)).Length];
	}

	public GridCell(int row, int column, GridCellContents contents = GridCellContents.Empty) {
		this.contents = contents;
		this.row = row;
		this.column = column;
		neighbors = new GridCell[Enum.GetValues(typeof(GridDirection)).Length];
		enemies = new List<Enemy>();
	}

	public void setContents(GridCellContents contents) {
		this.contents = contents;
	}

	public void addEnemy(Enemy enemy)	{
		enemies.Add(enemy);
	}

	public void removeEnemy(Enemy enemy)	{
		enemies.Remove(enemy);
	}

	public bool hasEnemy()	{
		return enemies.Count > 0;
	}

	public GridCellContents getContents() {
		return contents;
	}

	public void setNeighbor(GridDirection direction, GridCell cell) {
		neighbors[(int)direction] = cell;
		cell.neighbors[(int)direction.opposite()] = this;
	}

	public GridCell getNeighbor(GridDirection direction) {
		return neighbors[(int)direction];
	}

	public int getRow() {
		return row;
	}

	public int getColumn() {
		return column;
	}

	public override string ToString() {
		if(enemies.Count > 0)
			return "%";
		return $"{ASCIIMap[contents]}";
	}
}

public enum GridCellContents {
	Empty,
	Block,
	Jewel
}

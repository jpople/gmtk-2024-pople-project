using System;
using Godot;
public partial class GridCell : Node   {

	private GridCellContents contents;
	private GridCell[] neighbors;
	private int row, column;

	public GridCell()   {
		this.contents = GridCellContents.Empty;
		neighbors = new GridCell[Enum.GetValues(typeof(GridDirection)).Length];
	}

	public GridCell(GridCellContents contents, int row, int column)  {
		this.contents = contents;
		this.row = row;
		this.column = column;
	}

	public void setContents (GridCellContents contents)   {
		this.contents = contents;
	}

	public GridCellContents getContents()	{
		return contents;
	}

	public void setNeighbor (GridDirection direction, GridCell cell)   {
		neighbors[(int)direction] = cell;
		cell.neighbors[(int)direction.opposite()] = this;
	}

	public GridCell getNeighbor (GridDirection direction)   {
		return neighbors[(int)direction];
	}

	public int getRow()	{
		return row;
	}

	public int getColumn()	{
		return column;
	}

	public void update()   {
		//GD.Print(toString());
	}

	public String toString()    {
		return "" + contents;
	}
}

public enum GridCellContents {
	Empty,
	Block,
	Jewel
}

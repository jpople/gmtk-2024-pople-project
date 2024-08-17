using System;
using Godot;
public partial class GridCell : Node   {

	private GridCellContents contents;
	private GridCell[] neighbors;

	public GridCell()   {
		this.contents = GridCellContents.Empty;
		neighbors = new GridCell[Enum.GetValues(typeof(GridDirection)).Length];
	}

	public GridCell(GridCellContents gcc)  {
		this.contents = gcc;
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
	Sun
}

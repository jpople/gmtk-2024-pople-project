using System;
using System.Collections.Generic;
using Godot;
public partial class Block : Node   {

	private Grid grid;
	private List<GridCell> location;
	private BlockType type;

	public Block(BlockType type, List<GridCell> location, Grid grid)    {
		this.location = location;
		this.type = type;
		this.grid = grid;
	}

	public void update()    {

	}

	public void move(GridDirection direction)  {
		if(canMove(direction))  {
			for (int i = location.Count-1;i >= 0 ;i--) {
				location[i].setContents(GridCellContents.Empty);
				location[i] = location[i].getNeighbor(direction);
				location[i].setContents(GridCellContents.Block);
			}    
		}
	}

	bool canMove(GridDirection direction)   {
		foreach (GridCell cell in location)
		{
			if (cell.getNeighbor(direction) == null)
				return false;
			else if (cell.getNeighbor(direction).getContents() == GridCellContents.Block && location.Contains(cell.getNeighbor(direction)) == false)   {
				return false;
			}
		}
		return true;
	}
}

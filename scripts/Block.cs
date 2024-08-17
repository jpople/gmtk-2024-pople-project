using System;
using System.Collections.Generic;
using Godot;
public partial class Block : Node   {
	private List<GridCell> location;
	private BlockType type;

	public Block(BlockType type, List<GridCell> location)    {
		this.location = location;
		this.type = type;
	}

	public void update()    {

	}

	public void move(GridDirection direction)  {
		if(canMove(direction))  {
			for (int i = location.Count-1;i >= 0 ;i--) {
				location[i].changeContents(GridCellContents.Empty);
				location[i] = location[i].getNeighbor(direction);
				location[i].changeContents(GridCellContents.Block);
			}    
		}
	}

	bool canMove(GridDirection direction)   {
		foreach (GridCell cell in location)
		{
			if (cell.getNeighbor(direction) == null)
				return false;
		}
		return true;
	}
}

public enum BlockType   {
	oneSquare,
	twoSquare
}

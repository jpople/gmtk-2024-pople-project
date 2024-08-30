using System;
using System.Collections.Generic;
using System.Linq;
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

	public void moveBlock(GridDirection direction)  {
		GridCell jewelCell = null;
		foreach (GridCell cell in location) {
			GridCell targetCell = cell.getNeighbor(direction);
			if (canMove(targetCell))	{
				jewelCell = (targetCell.getContents() == GridCellContents.Jewel && jewelCell == null ) ? targetCell : jewelCell;
				continue;
			}

			return;
		}

		for (int i = location.Count-1;i >= 0 ;i--) {
			location[i] = move(location[i], direction);
		}

		if (jewelCell != null)	{
				GridCell newJewelLocation = location.FirstOrDefault(c => c.getNeighbor(GridDirection.N).getContents() == GridCellContents.Empty).getNeighbor(GridDirection.N);
				newJewelLocation.setContents(GridCellContents.Jewel);
		}
	}

	private GridCell move(GridCell cell, GridDirection direction)	{

		cell.setContents(GridCellContents.Empty);
		cell.getNeighbor(direction).setContents(GridCellContents.Block);

		return cell.getNeighbor(direction);
	}

	private bool canMove(GridCell targetCell)	{
		return targetCell != null && (location.Contains(targetCell) || targetCell.getContents() != GridCellContents.Block);
	}
}

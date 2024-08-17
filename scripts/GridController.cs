using System;
using System.Collections.Generic;

public class GridController   {

	private Grid grid;
	private List<Block> blocks;

	public void createGrid()   {
		grid = Grid.initialize(GridData.DEFAULT_GRID_HEIGHT, GridData.DEFAULT_GRID_WIDTH);
		blocks = new List<Block>();
	}

	public void addBlock(int column, BlockType type)    {
		HashSet<GridCell> location = new HashSet<GridCell>();

		if (column < 0)
			column = 0;
		else if (column > grid.width)
			column = grid.width;

		switch(type)   {
			case BlockType.oneSquare:
				location.Add(grid.getGridCell(0,column));
				break;
			case BlockType.twoSquare:
				if (column+1 >= grid.width)    {
					column -= 1;
				}

				location.Add(grid.getGridCell(0,column));
				location.Add(grid.getGridCell(1,column));
				location.Add(grid.getGridCell(0,column+1));
				location.Add(grid.getGridCell(1,column+1));
				break;
		}

		grid.addBlock(location);
		blocks.Add(new Block(type, location));
	}

	public void update()    {
		grid.update();
		foreach (Block block in blocks)
		{
			block.update();
		}
	}

	public void printGrid() {
		grid.print();
	}
}

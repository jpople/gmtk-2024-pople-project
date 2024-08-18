using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class GridController   {

	private Grid grid;
	private List<Block> blocks;

	public void createGrid()   {
		grid = Grid.initialize(GridData.DEFAULT_GRID_HEIGHT, GridData.DEFAULT_GRID_WIDTH);
		blocks = new List<Block>();
	}

	public void addBlock(int column, BlockType type)    {
		if (column < 0)
			column = 0;
		else if (column > grid.width)
			column = grid.width;

		if (column+2 >= grid.width)
			column -= 2;
		else if (column+1 >= grid.width)
			column -= 1;

		List<GridCell> location = getBlockLocation(column, type);

		grid.addBlock(location);
		blocks.Add(new Block(type, location,grid));
		
		update();
	}

	List<GridCell> getBlockLocation(int column, BlockType type)	{
		List<GridCell> location = new List<GridCell>();

		int[,] pattern = type.getPattern();

		for (int i = 0;i < pattern.GetLength(0);i++)	{
			for (int j = 0;j < pattern.GetLength(1);j++)	{
				if(pattern[i,j] == 1)	{
					location.Add(grid.getGridCell(i,column+j));
				}
			}
		}
		return location;
	}

	public void moveBlocks()	{
		foreach (Block block in blocks)	{
			block.move(GridDirection.S);
		}
		update();
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

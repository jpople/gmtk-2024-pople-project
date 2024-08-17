using System;
using System.Collections.Generic;
using Godot;

public partial class Grid : Node   {
	private GridCell[,] cells;

	public int width;
	public int height;

//Static method to generate a gride based on width and height
	public static Grid initialize(int width, int height)   {
		Grid grid = new Grid();
		grid.createCells(width, height);
		grid.update();
		return grid;
	}

//Updates all cells in the grid to be called on a regular cadence. A floating block falls, enemies climb, and user input is reflected
public void update()   {
		int rows = cells.GetLength(0);
		int columns = cells.GetLength(1);

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				cells[i, j].update();
			}
		}

		print();
	}


//Builds cell array and populate with empty instances
	void createCells(int height, int width)  {
		this.width = width;
		this.height = height;

		cells = new GridCell[height, width];

		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				GridCell gc = new GridCell();
				cells[i, j] = gc;
				addNeighbors(i,j,gc);
			}
		}
	}

	void addNeighbors (int row, int column, GridCell cell) {
		if(column > 0) {
			cell.setNeighbor(GridDirection.W,cells[row,column-1]);
			if (row > 0)    {
				cell.setNeighbor(GridDirection.NW,cells[row-1,column-1]);
			}
		}
		if (row > 0)    {
				cell.setNeighbor(GridDirection.N,cells[row-1,column]);
		}
	}

	public void addBlock (HashSet<GridCell> location)  {
		foreach (GridCell cell in location)  {
			cell.changeContents(GridCellContents.Block);
		}
	}

//Print grid to console for debugging
	public void print()    {
		String print = "";

		int rows = cells.GetLength(0);
		int columns = cells.GetLength(1);

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				print += cells[i, j].toString() + "\t";
			}
			print += "\n";
		}

		GD.Print(print);
	}

	public GridCell getGridCell(int column, int row)   {
		return cells[column,row];
	}
}

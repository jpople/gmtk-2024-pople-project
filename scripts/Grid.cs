using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Godot;

public partial class Grid : Node {
	private GridCell[,] cells;
	public GridCell[,] Cells { get => cells; }

	public int width = GridData.DEFAULT_GRID_WIDTH;
	public int height = GridData.DEFAULT_GRID_HEIGHT;

	//Static method to generate a gride based on width and height
	public static Grid initialize() {
		Grid grid = new Grid();
		grid.createCells();
		grid.update();
		return grid;
	}

	public static Grid initialize(int[,] map)	{
		Grid grid = new Grid();
		grid.createCells(map);
		grid.update();
		return grid;
	}

//Updates all cells in the grid to be called on a regular cadence. A floating block falls, enemies climb, and user input is reflected
	public void update()   {
		int rows = cells.GetLength(0);
		int columns = cells.GetLength(1);

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				// cells[i, j].update();
			}
		}

		print();
	}


	//Builds cell array and populate with empty instances
	void createCells() {
		cells = new GridCell[height, width];

		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				createCell(GridCellContents.Empty,i,j);
			}
		}
	}

	void createCells(int[,] map)  {
		this.height = map.GetLength(0);
		this.width = map.GetLength(1);

		cells = new GridCell[height, width];

		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				createCell((GridCellContents)map[i,j],i,j);
			}
		}
	}

	void createCell(GridCellContents contents, int row, int column)	{
		GridCell gc = new GridCell(row, column, contents);
		cells[row, column] = gc;
		addNeighbors(row,column,gc);
	}

	void addNeighbors (int row, int column, GridCell cell) {
		if(column > 0) {
			cell.setNeighbor(GridDirection.W,cells[row,column-1]);
			if (row > 0)    {
				cell.setNeighbor(GridDirection.NW,cells[row-1,column-1]);
			}
		}
		if (row > 0) {
			cell.setNeighbor(GridDirection.N, cells[row - 1, column]);
		}
	}

	public void addBlock(List<GridCell> location) {
		foreach (GridCell cell in location) {
			cell.setContents(GridCellContents.Block);
		}
	}

	public void addJewel(GridCell location) {
		location.setContents(GridCellContents.Jewel);
	}

	//Print grid to console for debugging
	public void print() {
		string print = "";

		for (int i = 0; i < GridData.DEFAULT_GRID_HEIGHT; i++) {
			for (int j = 0; j < GridData.DEFAULT_GRID_WIDTH; j++) {
				print += $"{cells[i, j]}";
			}
			print += "\n";
		}

		GD.Print(print);
	}

	public GridCell getGridCell(int column, int row) {
		return cells[column, row];
	}
}

using System;
using System.Collections.Generic;
using Godot;

public partial class Grid : Node {
	private GridCell[,] cells;
	public GridCell[,] Cells { get => cells; }

	public int width = GridData.DEFAULT_GRID_WIDTH;
	public int height = GridData.DEFAULT_GRID_HEIGHT;

	BlockType heldBlock;
	public BlockType HeldBlock {
		get => heldBlock;
		set => heldBlock = value;
	}

	int cursorLocation;

	//Static method to generate a gride based on width and height
	public static Grid initialize() {
		Grid grid = new Grid();
		grid.createCells();
		grid.heldBlock = BlockTypeHelper.GetRandomBlockType();
		return grid;
	}

	public static Grid initialize(int[,] map) {
		Grid grid = new Grid();
		grid.createCells(map);
		grid.height = map.GetLength(0);
		grid.width = map.GetLength(1);
		grid.heldBlock = BlockTypeHelper.GetRandomBlockType();
		return grid;
	}

	//Builds cell array and populate with empty instances
	void createCells() {
		cells = new GridCell[height, width];

		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				createCell(GridCellContents.Empty, i, j);
			}
		}
	}

	void createCells(int[,] map) {
		this.height = map.GetLength(0);
		this.width = map.GetLength(1);

		cells = new GridCell[height, width];

		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				createCell((GridCellContents)map[i, j], i, j);
			}
		}
	}

	void createCell(GridCellContents contents, int row, int column) {
		GridCell gc = new GridCell(row, column, contents);
		cells[row, column] = gc;
		addNeighbors(row, column, gc);
	}

	void addNeighbors(int row, int column, GridCell cell) {
		if (column > 0) {
			cell.setNeighbor(GridDirection.W, cells[row, column - 1]);
			if (row > 0) {
				cell.setNeighbor(GridDirection.NW, cells[row - 1, column - 1]);
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

	public void MoveCursor(int direction) {
		if (direction != 1 && direction != -1) {
			throw new ArgumentException("movement must be 1 or -1");
		}
		cursorLocation = Mathf.Clamp(cursorLocation + direction, 0, GridData.DEFAULT_GRID_WIDTH - 1);
	}

	public void addEnemy(GridCell location, Enemy e) {
		location.addEnemy(e);
	}

	//Print grid to console for debugging
	public void print() {
		string print = "";

		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				print += $"{cells[i, j]}";
			}
			print += "\n";
		}

		GD.Print(print);
	}

	public GridCell getGridCell(int row, int column) {
		return cells[row, column];
	}

	public (int column, BlockType type) GetCursorState() => (cursorLocation, heldBlock);
}

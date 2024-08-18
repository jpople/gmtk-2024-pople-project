using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Godot;

public class GridController {

	public Grid grid;
	private List<Block> blocks;
	private List<Jewel> jewels;

	public void createGrid() {
		grid = Grid.initialize();
		blocks = new List<Block>();
		jewels = new List<Jewel>();
	}

	public void addBlock(int column, BlockType type) {
		if (column < 0)
			column = 0;
		else if (column > grid.width)
			column = grid.width;

		if (column + 2 >= grid.width)
			column -= 2;
		else if (column + 1 >= grid.width)
			column -= 1;

		List<GridCell> location = getBlockLocation(column, type);

		grid.addBlock(location);
		blocks.Add(new Block(type, location, grid));

		update();
	}

	public void addJewel(int column) {
		GridCell location = grid.getGridCell(grid.height - 1, column);
		grid.addJewel(location);
		jewels.Add(new Jewel(location));

		update();
	}

	List<GridCell> getBlockLocation(int column, BlockType type) {
		List<GridCell> location = new List<GridCell>();

		int[,] pattern = type.getPattern();

		for (int i = 0; i < pattern.GetLength(0); i++) {
			for (int j = 0; j < pattern.GetLength(1); j++) {
				if (pattern[i, j] == 1) {
					location.Add(grid.getGridCell(i, column + j));
				}
			}
		}
		return location;
	}

	public void moveBlocks() {
		foreach (Block block in blocks) {
			block.move(GridDirection.S);
		}
		update();
	}

	public void update() {
		grid.update();
		foreach (Block block in blocks) {
			block.update();
		}
	}

	public void printGrid() {
		grid.print();
	}

	public GridCell[,] GetCells() => grid.Cells;
}

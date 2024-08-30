using System;
using System.Collections.Generic;
using Godot;

public class GridController {

	public Grid grid;
	public bool youLose = false;

	private List<Block> blocks;
	private List<Jewel> jewels;
	private List<Enemy> enemies;

	public void createGrid() {
		grid = Grid.initialize();
		blocks = new List<Block>();
		jewels = new List<Jewel>();
		enemies = new List<Enemy>();
	}

	public void createGrid(int[,] grid)	{
		this.grid = Grid.initialize(grid);
		blocks = new List<Block>();
		jewels = new List<Jewel>();
		enemies = new List<Enemy>();
	}

	public void addBlock(int column, BlockType type)    {
		List<GridCell> location = determineBlockLocations(column, type);

		grid.addBlock(location);
		blocks.Add(new Block(type, location, grid));
	}

	public void PlaceHeldBlock() {
		(int location, BlockType type) = grid.GetCursorState();
		addBlock(location, type);
		BlockType b =  BlockTypeHelper.GetRandomBlockType();
		grid.HeldBlock = b;
		GD.Print(grid.HeldBlock + " was chosen as next block");
	}

	public void addJewel(int column) {
		GridCell location = grid.getGridCell(grid.height - 1, column);
		grid.addJewel(location);
		jewels.Add(new Jewel(location));
	}

	public void addEnemy(GridCell location)	{
		Enemy e = new Enemy(grid, location);
		enemies.Add(e);
		grid.addEnemy(location, e);
	}

	public List<GridCell> determineBlockLocations(int column, BlockType type)	{

		if (column < 0)
			column = 0;
		else if (column > grid.width)
			column = grid.width;

		if (type == BlockType.bigN || type == BlockType.smallN || type == BlockType.pyramid && column+2 >= grid.width)
			column -= 2;
		else if (column+1 >= grid.width)
			column -= 1;

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
			block.moveBlock(GridDirection.S);
		}
	}

	public void moveEnemies()	{
		foreach (Enemy enemy in enemies)	{
			if(enemy.move())	{
				youLose = true;
				return;
			}
		}
	}

	public void printGrid() {
		grid.print();
	}

	public GridCell[,] GetCells() => grid.Cells;
	public (int column, BlockType block) GetCursorState() => grid.GetCursorState();
	public void MoveCursor(int direction) => grid.MoveCursor(direction);
}

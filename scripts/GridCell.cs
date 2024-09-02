using System;
using System.Collections.Generic;
using Godot;
public partial class GridCell : Node2D {

	private GridCellContents contents;
	private GridCell[] neighbors;
	private List<Enemy> enemies;
	private int row, column;
	private Block block;

	public static Dictionary<GridCellContents, Texture2D> textureMap = new() {
		{GridCellContents.Jewel, GD.Load<Texture2D>("res://sprites/tile_sun.png")},
		{GridCellContents.Block, GD.Load<Texture2D>("res://sprites/BrickOrange.jpg")},
		{GridCellContents.Empty, GD.Load<Texture2D>("res://sprites/TransparentSquare.png")}
	};

	public GridCell() {
		this.contents = GridCellContents.Empty;
		neighbors = new GridCell[Enum.GetValues(typeof(GridDirection)).Length];
	}

	public GridCell(int row, int column, GridCellContents contents = GridCellContents.Empty) {
		this.contents = contents;
		this.row = row;
		this.column = column;
		neighbors = new GridCell[Enum.GetValues(typeof(GridDirection)).Length];
		enemies = new List<Enemy>();
	}

	public void addBlock (Block block)	{
		this.block = block;
		contents = GridCellContents.Block;
	}
	public void setContents(GridCellContents contents) {
		if (contents == GridCellContents.Empty || contents == GridCellContents.Jewel)
			block = null;
		this.contents = contents;
	}

	public void addEnemy(Enemy enemy)	{
		enemies.Add(enemy);
	}

	public void removeEnemy(Enemy enemy)	{
		enemies.Remove(enemy);
	}

	public bool hasEnemy()	{
		return enemies.Count > 0;
	}

	public bool emptyTop()	{
		GridCell north = getNeighbor(GridDirection.N);
		
		return (north != null) ? north.getContents() == GridCellContents.Empty : true;
	}

	public GridCellContents getContents() {
		return contents;
	}

	public void setNeighbor(GridDirection direction, GridCell cell) {
		neighbors[(int)direction] = cell;
		cell.neighbors[(int)direction.opposite()] = this;
	}

	public GridCell getNeighbor(GridDirection direction) {
		return neighbors[(int)direction];
	}

	public int getRow() {
		return row;
	}

	public int getColumn() {
		return column;
	}

	public Vector2 getPosition()	{
		return new Vector2() {X = column * GridData.CELL_WIDTH,
			Y = row * GridData.CELL_HEIGHT};
	}
	
	public Texture2D getTexture()	{
		if (contents != GridCellContents.Block || block == null)
			return textureMap[contents];

		return GD.Load<Texture2D>("res://sprites/Brick"+block.getBlockColor()+".jpg");
	}
}

public enum GridCellContents {
	Empty,
	Block,
	Jewel
}

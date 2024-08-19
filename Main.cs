using System;
using Godot;

public partial class Main : Node2D {
	
	private const int EnemySpawnRate = 7;
	private const int EnemySpeed = 2;
	
	Texture2D brickTexture = GD.Load<Texture2D>("res://sprites/tile_brick.png");
	Texture2D sunTexture = GD.Load<Texture2D>("res://sprites/tile_sun.png");
	Texture2D antTexture = GD.Load<Texture2D>("res://sprites/ants.png");

	GridController controller;
	int turnCounter = 0;

	public override void _Ready() {
		GetWindow().Size = new Vector2I() {
			X = GridData.DEFAULT_GRID_WIDTH * GridData.CELL_WIDTH,
			Y = GridData.DEFAULT_GRID_HEIGHT * GridData.CELL_HEIGHT,
		};

		controller = new GridController();
		controller.createGrid();
		controller.addJewel(GridData.DEFAULT_GRID_WIDTH/2);
	}

	public override void _Draw() {
		foreach (GridCell cell in controller.GetCells()) {
			var texture = cell.getContents() switch {
				GridCellContents.Block => brickTexture,
				GridCellContents.Jewel => sunTexture,
				_ => null
			};

			var position = new Vector2() {X = cell.getColumn() * GridData.CELL_WIDTH,
			Y = cell.getRow() * GridData.CELL_HEIGHT};

			if (texture != null) {
				
				DrawTexture(texture, position);
			}
			
			if(cell.hasEnemy())
				DrawTexture(antTexture, position);
		}
 
		(int cursorLocation, BlockType? heldBlock) = controller.GetCursorState();
		if (heldBlock == BlockType.none) {
			return;
		}
		else{
			foreach(GridCell location in controller.determineBlockLocations(cursorLocation,controller.grid.HeldBlock))	{
				var cursorPosition = new Vector2() {
					X = location.getColumn() * GridData.CELL_WIDTH,
					Y = location.getRow() * GridData.CELL_HEIGHT
				};
			DrawTexture(brickTexture, cursorPosition, new Color(1, 1, 1, 0.5f));
			}
		}
	}

	private void Advance() {
		if(!controller.youLose)	{
			controller.moveBlocks();
			if (turnCounter % EnemySpeed == 0)	{
				controller.moveEnemies();
			}
			QueueRedraw();
		}

		if(turnCounter != 0 && turnCounter % EnemySpawnRate == 0 && turnCounter > 30)	{
			GridCell randomCell = controller.grid.getGridCell(GridData.DEFAULT_GRID_HEIGHT-1, (int)(GD.Randi() % GridData.DEFAULT_GRID_WIDTH));
			if(!randomCell.hasEnemy())
				controller.addEnemy(randomCell);
		}
		turnCounter++;
	}

	public override void _Input(InputEvent @event) {
		if (@event is not InputEventKey e || !e.Pressed) {
			return;
		}
		switch (e.Keycode) {
			case Key.Space:
				controller.PlaceHeldBlock();
				Advance();
				break;
			case Key.Left:
				controller.MoveCursor(-1);
				Advance();
				break;
			case Key.Right:
				controller.MoveCursor(+1);
				Advance();
				break;
		}
	}
}

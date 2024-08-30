using System;
using Godot;

public partial class Main : Node2D {
	
	private const int EnemySpawnRate = 7;
	private const int EnemySpeed = 5;
	private const int EnemySpawnDelay = 55;
	
	Texture2D brickTexture = GD.Load<Texture2D>("res://sprites/tile_brick.png");
	Texture2D sunTexture = GD.Load<Texture2D>("res://sprites/tile_sun.png");
	Texture2D antTexture = GD.Load<Texture2D>("res://sprites/ants.png");

	Timer timer;

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

		timer = GetNode<Timer>("Timer");
		timer.Timeout += () => Advance(false);
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

	private void Advance(bool userMove) {
		if(!controller.youLose && !userMove)	{
			controller.moveBlocks();
			if (turnCounter % EnemySpeed == 0)	{
				controller.moveEnemies();
			}
		turnCounter++;

		if(turnCounter % EnemySpawnRate == 0 && turnCounter > EnemySpawnDelay)	{
			GridCell randomCell = controller.grid.getGridCell(GridData.DEFAULT_GRID_HEIGHT-1, (int)(GD.Randi() % GridData.DEFAULT_GRID_WIDTH));
			if(!randomCell.hasEnemy())
				controller.addEnemy(randomCell);
			}
		}
		QueueRedraw();
	}

	public override void _Input(InputEvent @event) {
		if (@event is not InputEventKey e || !e.Pressed) {
			return;
		}
		switch (e.Keycode) {
			case Key.Space:
				timer.Start();
				controller.PlaceHeldBlock();
				Advance(true);
				break;
			case Key.Left:
				controller.MoveCursor(-1);
				Advance(true);
				break;
			case Key.Right:
				controller.MoveCursor(+1);
				Advance(true);
				break;
		}
	}
}

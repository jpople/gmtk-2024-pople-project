using System;
using Godot;

public partial class Main : Node2D {

	Timer timer;

	GridController controller;
	int turnCounter = 0;
	Texture2D antTexture = GD.Load<Texture2D>("res://sprites/ants.png");

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
			DrawTexture(cell.getTexture(), cell.getPosition());
			if (cell.hasEnemy())	{
				DrawTexture(antTexture,cell.getPosition());
			}
		}

		(int cursorLocation, BlockType? heldBlock) = controller.GetCursorState();
		
		foreach(GridCell cell in controller.determineBlockLocations(cursorLocation,controller.grid.HeldBlock))	{
			DrawTexture(GridCell.textureMap[GridCellContents.Block], cell.getPosition(), new Color(1, 1, 1, 0.5f));
		}
	}

	private void Advance(bool userMove) {
		if(!controller.youLose && !userMove)	{
			controller.moveBlocks();
			if (turnCounter % Enemy.EnemySpeed == 0)	{
				controller.moveEnemies();
			}
		turnCounter++;

		if(turnCounter % Enemy.EnemySpawnRate == 0 && turnCounter > Enemy.EnemySpawnDelay)	{
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

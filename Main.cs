using System;
using Godot;

public partial class Main : Node2D {
    Texture2D brickTexture = GD.Load<Texture2D>("res://sprites/tile_brick.png");
    Texture2D sunTexture = GD.Load<Texture2D>("res://sprites/tile_sun.png");

    GridController controller;

    public override void _Ready() {
        GetWindow().Size = new Vector2I() {
            X = GridData.DEFAULT_GRID_WIDTH * GridData.CELL_WIDTH,
            Y = GridData.DEFAULT_GRID_HEIGHT * GridData.CELL_HEIGHT,
        };

        controller = new GridController();
        controller.createGrid();
    }

    public override void _Draw() {
        foreach (GridCell cell in controller.GetCells()) {
            var texture = cell.getContents() switch {
                GridCellContents.Block => brickTexture,
                GridCellContents.Jewel => sunTexture,
                _ => null
            };
            if (texture == null) {
                continue;
            }
            var position = new Vector2() {
                X = cell.getColumn() * GridData.CELL_WIDTH,
                Y = cell.getRow() * GridData.CELL_HEIGHT,
            };
            DrawTexture(texture, position);
        }
    }

    private void Advance() {
        controller.moveBlocks();
        if (GD.Randf() < 0.2f) {
            var randomColumn = (int)GD.Randi() % GridData.DEFAULT_GRID_WIDTH;
            var randomBlockType = (int)(GD.Randi() % Enum.GetValues<BlockType>().Length);
            controller.addBlock(randomColumn, (BlockType)randomBlockType);
        }
        QueueRedraw();
    }

    public override void _Input(InputEvent @event) {
        if (@event is not InputEventKey e || e.Keycode != Key.Space || !e.Pressed) {
            return;
        }
        Advance();
    }
}

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
        (int cursorLocation, BlockType? heldBlock) = controller.GetCursorState();
        if (heldBlock == BlockType.none) {
            return;
        }
        var cursorPosition = new Vector2() {
            X = cursorLocation * GridData.CELL_WIDTH,
            Y = 0
        };
        DrawTexture(brickTexture, cursorPosition, new Color(1, 1, 1, 0.5f));
    }

    private void Advance() {
        controller.moveBlocks();
        QueueRedraw();
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

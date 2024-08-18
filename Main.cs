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
        controller.addBlock(0, BlockType.oneSquare);
        controller.addBlock(2, BlockType.oneSquare);
        controller.addBlock(4, BlockType.twoSquare);
        controller.moveBlocks();
        controller.moveBlocks();
        controller.moveBlocks();
        controller.moveBlocks();
        controller.moveBlocks();
        controller.moveBlocks();
        controller.moveBlocks();
        controller.moveBlocks();
        controller.moveBlocks();
        controller.moveBlocks();
    }

    // public override void _Draw() {
    //     foreach (GridCell cell in controller.GetCells()) {
    //         var texture = cell.getContents() switch {
    //             GridCellContents.Block => brickTexture,
    //             GridCellContents.Sun => sunTexture,
    //             _ => null
    //         };
    //         var position = new Vector2() {
    //             X = cell.getColumn() * GridData.CELL_WIDTH,
    //             Y = cell.getRow() * GridData.CELL_HEIGHT,
    //         };
    //         // DrawTexture(texture, )
    //     }
    // }

    // private void Render() {
    // }
}

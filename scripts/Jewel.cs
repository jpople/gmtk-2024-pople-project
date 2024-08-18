using Godot;

public partial class Jewel : Node   {
    GridCell location;

    public Jewel(GridCell location) {
        this.location = location;
    }
}
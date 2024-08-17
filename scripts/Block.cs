using System;
using System.Collections.Generic;
using Godot;
public partial class Block : Node   {
    private HashSet<GridCell> location;
    private BlockType type;

    public Block(BlockType type, HashSet<GridCell> location)    {
        this.location = location;
        this.type = type;
    }
}

public enum BlockType   {
    oneSquare,
    twoSquare
}
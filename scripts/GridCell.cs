using System;
using Godot;
public partial class GridCell : Node   {

    private GridCellContents contents;

    public GridCell()   {
        this.contents = GridCellContents.Empty;
    }

    public GridCell(GridCellContents gcc)  {
        this.contents = gcc;
    }

    public void changeContents (GridCellContents contents)   {
        this.contents = contents;
    }

    public void update()   {
        //GD.Print(toString());
    }

    public String toString()    {
        return "" + contents;
    }
}

public enum GridCellContents {
    Empty,
    Block,
    Sun
}
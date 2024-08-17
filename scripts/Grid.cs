using System;
using System.Data;
using Godot;

public partial class Grid : Node   {
    private GridCell[,] cells;

//Static method to generate a gride based on width and height
    public static Grid initialize(int width, int height)   {
        Grid grid = new Grid();
        grid.createCells(width, height);
        grid.update();
        return grid;
    }

//Updates all cells
void update()   {
        int rows = cells.GetLength(0);
        int columns = cells.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                cells[i, j].update();
            }
        }

        print();
    }


//Builds cell array and populate with empty instances
    void createCells(int width, int height)  {
        cells = new GridCell[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int value = i * width + j;
                cells[i, j] = new GridCell();
            }
        }
    }

//Print grid to console for debugging
    void print()    {
        String print = "";

        int rows = cells.GetLength(0);
        int columns = cells.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                print += cells[i, j].toString() + "\t";
            }
            print += "\n";
        }

        GD.Print(print);
    }
}
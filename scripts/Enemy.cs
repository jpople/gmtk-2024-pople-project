using System;
using System.Collections.Generic;
using Godot;

public partial class Enemy : Node   {

    Grid grid;
    GridCell location;

    public Enemy(Grid grid, GridCell location) {
        this.grid = grid;
        this.location = location;
    }

    public void move()  {
        List<GridCell> path = findPathToJewel();

        if(path != null)    {
            location.setContents(GridCellContents.Empty);
		    location = path[0];
		    location.setContents(GridCellContents.Enemy);
        }
    }

    List<GridCell> findPathToJewel()
    {
        int rows = grid.height;
        int cols = grid.width;
        
        // Queue for BFS that stores the current position
        Queue<GridCell> queue = new Queue<GridCell>();
        
        // Enqueue the starting position
        queue.Enqueue(location);

        // Dictionary to store the previous node for each visited cell
        Dictionary<GridCell, GridCell> previous = new Dictionary<GridCell, GridCell>();
        
        // Keep track of visited positions
        bool[,] visited = new bool[rows, cols];

        visited[location.getRow(), location.getColumn()] = true;

        while (queue.Count > 0)
        {
            GridCell current = queue.Dequeue();
            
            // If we reached the goal (value 2), return true
            if (current.getContents() == GridCellContents.Jewel)
            {
                return reconstructPath(previous, current);
            }

            // Explore the four possible directions
            for (int i = 0; i < 4; i++)
            {
                GridCell newLocation = location.getNeighbor((GridDirection)(i * 2)); //Even directions are cardinal

                // Check if the new position is within bounds, a block, and not visited
                if (isValidMove(newLocation, visited))
                {
                    queue.Enqueue(newLocation);
                    visited[newLocation.getRow(), newLocation.getColumn()] = true;
                }
            }
        }
        return null;
    }

    List<GridCell> reconstructPath(
        Dictionary<GridCell, GridCell> previous,
        GridCell current)
    {
        List<GridCell> path = new List<GridCell>();

        // Backtrack from the goal to the start
        while (current != location)
        {
            path.Add(current);
            current = previous[current];
        }

        path.Add(location); // Add the starting point
        path.Reverse(); // Reverse the list to get the path from start to goal

        return path;
    }

    bool isValidMove(GridCell location, bool[,] visited)
    {
        // Check if the position is within bounds, is an allowable terrain, and has not been visited yet
        return location.getRow() >= 0 && location.getRow() < grid.height && location.getColumn() >= 0 && location.getColumn() < grid.width &&
               (grid.getGridCell(location.getRow(), location.getColumn()).getContents() == GridCellContents.Block || grid.getGridCell(location.getRow(), location.getColumn()).getContents() == GridCellContents.Jewel) && !visited[location.getRow(), location.getColumn()];
    }
}
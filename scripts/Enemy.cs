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

	//returns true if the player loses the game
	public bool move()  {
		List<GridCell> path = findPathToJewel();

		if(path != null)    {
			GridCell newLocation = path[0];
			location.removeEnemy(this);
			location = newLocation;
			location.addEnemy(this);
		}

		return location.getContents() == GridCellContents.Jewel;
	}

	List<GridCell> findPathToJewel()
	{
		
		// Queue for BFS that stores the current position 
		Queue<GridCell> queue = new Queue<GridCell>();
		queue.Enqueue(location);

		// Dictionary to store the previous node for each visited cell
		Dictionary<GridCell, GridCell> previous = new Dictionary<GridCell, GridCell>();
		
		// Keep track of visited positions
		bool[,] visited = new bool[grid.height, grid.width];
		visited[location.getRow(), location.getColumn()] = true;

		while (queue.Count > 0)
		{
			GridCell current = queue.Dequeue();


			// If we reached the goal (value 2), return true
			if (current.getContents() == GridCellContents.Jewel)
			{
				GD.Print("A path was found!");
				return reconstructPath(previous, current);
			}

			// Explore the four possible directions (Even directions are cardinal)
			for (int i = 0; i < 4; i++)
			{
				GridCell newLocation = current.getNeighbor((GridDirection)(i * 2));
				// Check if the new position is within bounds, a block, and not visited
				if (isValidMove(newLocation, visited))
				{
					queue.Enqueue(newLocation);
					visited[newLocation.getRow(), newLocation.getColumn()] = true;
					previous[newLocation] = current;
				}
			}
		}
		
		GD.Print("No path to the Jewel");
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

		path.Reverse(); // Reverse the list to get the path from start to goal

		return path;
	}

	bool isValidMove(GridCell location, bool[,] visited)
	{
		if (location == null)
			return false;

		bool isBound = location.getRow() >= 0 && location.getRow() < grid.height && location.getColumn() >= 0 && location.getColumn() < grid.width;
		bool isValid = (location.getContents() == GridCellContents.Block || location.getContents() == GridCellContents.Jewel);
		bool isUnvisited = !visited[location.getRow(), location.getColumn()];
		
		// Check if the position is within bounds, is an allowable terrain, and has not been visited yet
		return isBound && isValid && isUnvisited;
	}
}

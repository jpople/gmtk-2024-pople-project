public enum GridDirection
{
	N, NE, E, SE, S, SW, W, NW
}

public static class GridDirectionHelper
{

	public static GridDirection opposite(this GridDirection direction)
	{
		return (int)direction < 4 ? (direction + 4) : (direction - 4);
	}

	public static GridDirection previous(this GridDirection direction)
	{
		return direction == GridDirection.N ? GridDirection.NW : (direction - 1);
	}

	public static GridDirection next(this GridDirection direction)
	{
		return direction == GridDirection.NW ? GridDirection.N : (direction + 1);
	}
}
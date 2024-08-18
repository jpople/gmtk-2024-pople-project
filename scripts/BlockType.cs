
using System;
using Godot;

public enum BlockType {
	twoSquare,
	smallL,
	bigL,
	smallT,
	pyramid,
	none
}

public static class BlockTypeHelper {
	public static int[,] getPattern(this BlockType block) {
		switch (block) {
			case BlockType.twoSquare:
				int[,] ts = {{1, 1, 0},
						 	{1, 1, 0},
						 	{0, 0, 0}};
				return ts;

			case BlockType.smallL:
				int[,] sl = {{0, 1, 0},
						 	{1, 1, 0},
						 	{0, 0, 0}};
				return sl;

			case BlockType.bigL:
				int[,] bl = {{0, 1, 0},
						 	{0, 1, 0},
						 	{1, 1, 0}};
				return bl;

			case BlockType.smallT:
				int[,] bt = {{0, 1, 0},
						 	 {1, 1, 0},
						 	 {0, 1, 0}};
				return bt;
			case BlockType.pyramid:
				int[,] p = {{0, 0, 0},
						 	 {0, 1, 0},
						 	 {1, 1, 1}};
				return p;
		}
		return null;
	}

	public static BlockType GetRandomBlockType()	{
		BlockType b = (BlockType)(GD.Randi() % (Enum.GetValues<BlockType>().Length-1));
		GD.Print(b + " was chosen as next block");
		return b;
	}
}

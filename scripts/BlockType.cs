
using System;
using Godot;

public enum BlockType {
	twoSquare,
	smallLL,
	smallLR,
	bigLL,
	bigLR,
	smallTR,
	smallTL,
	pyramid,
	smallN,
	bigN,
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

			case BlockType.smallLL:
				int[,] sll = {{0, 1, 0},
						 	{1, 1, 0},
						 	{0, 0, 0}};
				return sll;

			case BlockType.smallLR:
				int[,] slr = {{1, 0, 0},
						 	{1, 1, 0},
						 	{0, 0, 0}};
				return slr;

			case BlockType.bigLR:
				int[,] blr = {{1, 0, 0},
						 	{1, 0, 0},
						 	{1, 1, 0}};
				return blr;

			case BlockType.bigLL:
				int[,] bll = {{0, 1, 0},
						 	{0, 1, 0},
						 	{1, 1, 0}};
				return bll;

			case BlockType.smallTR:
				int[,] str = {{0, 1, 0},
						 	 {1, 1, 0},
						 	 {0, 1, 0}};
				return str;
			case BlockType.smallTL:
				int[,] stl = {{1, 0, 0},
						 	 {1, 1, 0},
						 	 {1, 0, 0}};
				return stl;
			case BlockType.pyramid:
				int[,] p = {{0, 0, 0},
						 	 {0, 1, 0},
						 	 {1, 1, 1}};
				return p;

			case BlockType.bigN:
				int[,] bn = {{1, 0, 1},
						 	 {1, 1, 1},
						 	 {1, 0, 1}};
				return bn;

			case BlockType.smallN:
				int[,] sn = {{0, 0, 0},
						 	 {1, 1, 1},
						 	 {1, 0, 1}};
				return sn;
		}
		return null;
	}

	public static BlockType GetRandomBlockType()	{
		BlockType b = (BlockType)(GD.Randi() % (Enum.GetValues<BlockType>().Length-1));
		GD.Print(b + " was chosen as next block");
		return b;
	}
}

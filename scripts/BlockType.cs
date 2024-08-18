
using System;
using Godot;

public enum BlockType {
	oneSquare,
	twoSquare,
	smallL,
	bigL
}

public static class BlockTypeHelper {
	public static int[,] getPattern(this BlockType block) {
		switch (block) {
			case BlockType.oneSquare:
				int[,] os = {{1, 0, 0},
						 {0, 0, 0},
						 {0, 0, 0}};
				return os;

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
		}
		return null;
	}

	public static BlockType GetRandomBlockType() => (BlockType)(GD.Randi() % Enum.GetValues<BlockType>().Length);
}

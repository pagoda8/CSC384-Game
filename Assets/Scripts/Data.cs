//Static data for game
//Source: tetris.fandom.com/wiki

using System.Collections.Generic;
using UnityEngine;

public static class Data {
	//Data used for rotations
	public static readonly float cos = Mathf.Cos(Mathf.PI / 2f);
	public static readonly float sin = Mathf.Sin(Mathf.PI / 2f);
	public static readonly float[] RotationMatrix = new float[] { cos, sin, -sin, cos };

	//Dictionary that holds Tetrominos and an associated array of vector data defining its cells
	public static readonly Dictionary<Tetromino, Vector2Int[]> Cells = new Dictionary<Tetromino, Vector2Int[]>() {
		{ Tetromino.I, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1) } },
		{ Tetromino.J, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
		{ Tetromino.L, new Vector2Int[] { new Vector2Int( 1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
		{ Tetromino.O, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
		{ Tetromino.S, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0) } },
		{ Tetromino.T, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
		{ Tetromino.Z, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
	};

	//Array of vector data used for testing wall kicks of Tetromino I
	private static readonly Vector2Int[,] WallKicksI = new Vector2Int[,] {
		//Index 0: Test rotation index 0 -> 1
		{ new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int( 1, 0), new Vector2Int(-2,-1), new Vector2Int( 1, 2) },
		//Index 1: Test rotation index 1 -> 0
		{ new Vector2Int(0, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 1), new Vector2Int(-1,-2) },
		//Index 2: Test rotation index 1 -> 2
		{ new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 2), new Vector2Int( 2,-1) },
		//Index 3: Test rotation index 2 -> 1
		{ new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int(-2, 0), new Vector2Int( 1,-2), new Vector2Int(-2, 1) },
		//Index 4: Test rotation index 2 -> 3
		{ new Vector2Int(0, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 1), new Vector2Int(-1,-2) },
		//Index 5: Test rotation index 3 -> 2
		{ new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int( 1, 0), new Vector2Int(-2,-1), new Vector2Int( 1, 2) },
		//Index 6: Test rotation index 3 -> 0
		{ new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int(-2, 0), new Vector2Int( 1,-2), new Vector2Int(-2, 1) },
		//Index 7: Test rotation index 0 -> 3
		{ new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 2), new Vector2Int( 2,-1) },
	};

	//Array of vector data used for testing wall kicks of Tetrominos J, L, O, S, T, Z
	private static readonly Vector2Int[,] WallKicksJLOSTZ = new Vector2Int[,] {
		//Index 0: Test rotation index 0 -> 1
		{ new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0,-2), new Vector2Int(-1,-2) },
		//Index 1: Test rotation index 1 -> 0
		{ new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1,-1), new Vector2Int(0, 2), new Vector2Int( 1, 2) },
		//Index 2: Test rotation index 1 -> 2
		{ new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1,-1), new Vector2Int(0, 2), new Vector2Int( 1, 2) },
		//Index 3: Test rotation index 2 -> 1
		{ new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0,-2), new Vector2Int(-1,-2) },
		//Index 4: Test rotation index 2 -> 3
		{ new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int(0,-2), new Vector2Int( 1,-2) },
		//Index 5: Test rotation index 3 -> 2
		{ new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1,-1), new Vector2Int(0, 2), new Vector2Int(-1, 2) },
		//Index 6: Test rotation index 3 -> 0
		{ new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1,-1), new Vector2Int(0, 2), new Vector2Int(-1, 2) },
		//Index 7: Test rotation index 0 -> 3
		{ new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int(0,-2), new Vector2Int( 1,-2) },
	};

	//Dictionary that holds Tetrominos and an associated array of wall kick test data
	public static readonly Dictionary<Tetromino, Vector2Int[,]> WallKicks = new Dictionary<Tetromino, Vector2Int[,]>() {
		{ Tetromino.I, WallKicksI },
		{ Tetromino.J, WallKicksJLOSTZ },
		{ Tetromino.L, WallKicksJLOSTZ },
		{ Tetromino.O, WallKicksJLOSTZ },
		{ Tetromino.S, WallKicksJLOSTZ },
		{ Tetromino.T, WallKicksJLOSTZ },
		{ Tetromino.Z, WallKicksJLOSTZ },
	};

}



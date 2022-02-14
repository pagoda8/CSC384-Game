//Describes tetrominos

using UnityEngine;
using UnityEngine.Tilemaps;

//Collection of tetrominos
public enum Tetromino
{
	I,
	O,
	T,
	J,
	L,
	S,
	Z,
}

//Enable the editor to interpret tetromino data
[System.Serializable]

//Defines a tetromino
public struct TetrominoData
{
	public Tetromino tetromino;
	//Tile to use
	public Tile tile;
	//Cells that form the tetromino in original rotation
	public Vector2Int[] cells { get; private set; }

	//Ititialise cell data for tetromino
	public void Initialise()
	{
		this.cells = Data.Cells[this.tetromino];
	}
}
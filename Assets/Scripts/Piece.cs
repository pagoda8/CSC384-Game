//Implements an in game tetromino piece

using UnityEngine;

public class Piece : MonoBehaviour
{
	public Board board { get; private set; }
	public TetrominoData data { get; private set; }
	public Vector3Int position { get; private set; }
	//Cells that form the piece during game
	public Vector3Int[] cells { get; private set; }

	public void Initialise(Board board, Vector3Int position, TetrominoData data)
	{
		this.board = board;
		this.position = position;
		this.data = data;

		if (cells == null)
		{
			this.cells = new Vector3Int[data.cells.Length];
		}

		//Assign cell data for piece
		for (int i = 0; i < data.cells.Length; i++)
		{
			this.cells[i] = (Vector3Int)data.cells[i];
		}
	}
}

//Implements an in game tetromino piece

using UnityEngine;

public class Piece : MonoBehaviour {
	public Board board { get; private set; } //Board used
	public TetrominoData data { get; private set; } //Data of the piece's tetromino type
	public Vector3Int position { get; private set; } //Position on board
	public Vector3Int[] cells { get; private set; } //Cells that form the piece during game

	public void Initialise(Board board, Vector3Int position, TetrominoData data) {
		this.board = board;
		this.position = position;
		this.data = data;

		if (cells == null) {
			this.cells = new Vector3Int[data.cells.Length];
		}

		//Assign cell data for piece
		for (int i = 0; i < data.cells.Length; i++) {
			this.cells[i] = (Vector3Int)data.cells[i];
		}
	}

	//Called repeatedly when game is running. Refresh piece.
	//Handles player input
	private void Update() {
		this.board.Clear(this);

		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Move(Vector2Int.left);
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Move(Vector2Int.right);
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Move(Vector2Int.down);
		} else if (Input.GetKeyDown(KeyCode.Space)) {
			HardDrop();
		}

		this.board.Set(this);
	}

	//Moves piece as down as possible
	private void HardDrop() {
		while (Move(Vector2Int.down)) {
			continue;
		}
	}

	//Moves the position of the piece by a vector
	//Returns a bool to indicate if piece moved
	private bool Move(Vector2Int vector) {
		Vector3Int newPosition = this.position;
		newPosition.x += vector.x;
		newPosition.y += vector.y;

		bool allowedMove = this.board.IsValidPosition(this, newPosition);

		if (allowedMove) {
			this.position = newPosition;
		}

		return allowedMove;
	}
}



//Implements an in game tetromino piece

using UnityEngine;

public class Piece : MonoBehaviour {
	public Board board { get; private set; } //Board used
	public TetrominoData data { get; private set; } //Data of the piece's tetromino type
	public Vector3Int position { get; private set; } //Position on board
	public Vector3Int[] cells { get; private set; } //Cells that form the piece during game
	public int rotationIndex { get; private set; } //Defines the rotation phase

	public void Initialise(Board board, Vector3Int position, TetrominoData data) {
		this.board = board;
		this.position = position;
		this.data = data;
		this.rotationIndex = 0;

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

		if (Input.GetKeyDown(KeyCode.Q)) {
			Rotate(-1);
		} else if (Input.GetKeyDown(KeyCode.E)) {
			Rotate(1);
		}

		if (Input.GetKeyDown(KeyCode.A)) {
			Move(Vector2Int.left);
		} else if (Input.GetKeyDown(KeyCode.D)) {
			Move(Vector2Int.right);
		}

		if (Input.GetKeyDown(KeyCode.S)) {
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

	//Rotates a piece given a direction
	//(-1) => anti-clockwise, (1) => clockwise
	private void Rotate(int direction) {
		this.rotationIndex += direction;
		//Prevents from index going over 3 (4 phases in total)
		this.rotationIndex = this.rotationIndex % 4;

		ApplyRotationMatrix(direction);
	}

	//Applies rotation matrix to cells of piece
	private void ApplyRotationMatrix(int direction) {
		float[] matrix = Data.RotationMatrix;

		//Loop through all cells and apply rotation matrix
		for (int i = 0; i < cells.Length; i++) {
			Vector3 cell = cells[i];

			//Rotation algorithm depends on type of tetromino
			switch (data.tetromino) {
				case Tetromino.I:
					cell.x -= 0.5f;
					cell.y -= 0.5f;
					this.cells[i].x = Mathf.CeilToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
					this.cells[i].y = Mathf.CeilToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
					break;
				case Tetromino.O:
					break;
				default:
					this.cells[i].x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
					this.cells[i].y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
					break;
			}
		}
	}
}




















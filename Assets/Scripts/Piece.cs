//Implements an in game tetromino piece

using UnityEngine;
using System;

public class Piece : MonoBehaviour {
	public Board board { get; private set; } //Board used
	public TetrominoData data { get; private set; } //Data of the piece's tetromino type
	public Vector3Int position { get; private set; } //Position on board
	public Vector3Int[] cells { get; private set; } //Cells that form the piece during game
	public int rotationIndex { get; private set; } //Defines the rotation phase

	public float stepDelay = 1f; //Amount of time that has to pass for game to step (move piece down)
	public float nextStepTime; //Time of next step

	public void Initialise(Board board, Vector3Int position, TetrominoData data) {
		this.board = board;
		this.position = position;
		this.data = data;
		this.rotationIndex = 0;

		//Set next step time (current time + delay)
		this.nextStepTime = Time.time + this.stepDelay;

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

		if (Time.time >= this.nextStepTime) {
			Step();
		}

		this.board.Set(this);
	}

	//Moves the piece down by one place
	private void Step() {
		//Try to move down. If can't move, lock piece.
		if (!Move(Vector2Int.down)) {
			Lock();
		}
	}

	//Places the piece on the board for the last time and spawns a new piece
	//Updates stepDelay and player stats
	private void Lock() {
		this.board.Set(this);

		//Update lines cleared
		int linesCleared = this.board.ClearLines();
		ScoreManager.shared.AddLinesCleared(linesCleared);

		//Update scores
		float bonusPoints = 0;
		if (linesCleared > 1) {
			bonusPoints = ((linesCleared * linesCleared) / 2f) * 25f;
		}
		float pointsToAdd = (100f * linesCleared + bonusPoints) * (1f / stepDelay);
		ScoreManager.shared.AddPoints((int) Math.Round(pointsToAdd, 0));

		//Increase speed
		this.stepDelay -= this.stepDelay * (float) 0.1 * linesCleared;

		this.board.SpawnPiece();
	}

	//Moves piece as down as possible
	private void HardDrop() {
		while (Move(Vector2Int.down)) {
			continue;
		}
		Lock();
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
			//Update next step time if moving down
			if (vector == Vector2Int.down) {
				this.nextStepTime = Time.time + this.stepDelay;
			}
		}

		return allowedMove;
	}

	//Rotates a piece given a direction
	//(-1) => anti-clockwise, (1) => clockwise
	private void Rotate(int direction) {
		int oldRotationIndex = this.rotationIndex;
		this.rotationIndex += direction;
		this.rotationIndex = Wrap(this.rotationIndex, 0, 4);

		ApplyRotationMatrix(direction);

		//If all wall kick tests fail, undo rotation
		if (!TestWallKicks(this.rotationIndex, direction)) {
			this.rotationIndex = oldRotationIndex;
			ApplyRotationMatrix(-direction);
		}
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

	//Performs wall kick tests on a piece
	//First test is always without movement (i.e. vector (0, 0))
	//If it fails, other tests try different movements
	//If all tests fail, returns false
	private bool TestWallKicks(int rotationIndex, int direction) {
		int wallKickIndex = GetWallKickIndex(rotationIndex, direction);

		//Perform 5 wall kick tests
		//Tests end as soon as a test passes
		for (int i = 0; i < 5; i++) {
			//Get vector for a specific test
			Vector2Int vector = this.data.wallKicks[wallKickIndex, i];

			//Try to move with the vector
			if (Move(vector)) {
				return true;
			}
		}
		return false;
	}

	//Returns an index in the wall kick test data array depending on the current rotation index and rotation direction
	private int GetWallKickIndex(int rotationIndex, int direction) {
		//Get index for clockwise rotation (by default)
		int wallKickIndex = rotationIndex * 2;

		//If rotating anti-clockwise update index accordingly
		if (direction < 0) {
			wallKickIndex--;
		}

		return Wrap(wallKickIndex, 0, 8);
	}

	//Puts input into range [min, max]
	private int Wrap(int input, int min, int max) {
		if (input < min) {
			return max - (min - input) % (max - min);
		} else {
			return min + (input - min) % (max - min);
		}
	}
}




















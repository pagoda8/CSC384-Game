//Implements a Board object

using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour {
	public TetrominoData[] tetrominos; //Array of defined tetrominos for Board to use
	public Piece activePiece { get; private set; } //Active piece on board
	public Tilemap tilemap { get; private set; } //Tilemap for placing the tiles on
	public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0); //Starting position of new active piece
	public Vector2Int boardSize = new Vector2Int(10, 20); //Size of board

	//Bounds of the board
	public RectInt Bounds {
		get {
			//Offset the position by half of the board size in negative direction (bottom-left)
			//In other words get vector to bottom-left corner of board
			Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);

			return new RectInt(position, this.boardSize);
		}
	}

	//When Board gets initialised
	private void Awake() {
		//Gets the Tilemap and Piece to be used
		this.tilemap = GetComponentInChildren<Tilemap>();
		this.activePiece = gameObject.AddComponent<Piece>();

		//Initialise tetromino data
		for (int i = 0; i < this.tetrominos.Length; i++) {
			this.tetrominos[i].Initialise();
		}
	}

	//When game starts
	private void Start() {
		SpawnPiece();
	}

	//Spawn new random piece on top
	public void SpawnPiece() {
		//Get random tetromino data
		int r = Random.Range(0, this.tetrominos.Length);
		TetrominoData data = this.tetrominos[r];

		this.activePiece.Initialise(this, this.spawnPosition, data);
		if (IsValidPosition(this.activePiece, this.spawnPosition)) {
			Set(this.activePiece);
		} else {
			GameOver();
		}
	}

	//Checks if a position is allowed for a piece by returning a bool
	public bool IsValidPosition(Piece piece, Vector3Int position) {
		RectInt bounds = this.Bounds;

		//Check all cells of piece
		for (int i = 0; i < piece.cells.Length; i++) {
			Vector3Int tilePosition = piece.cells[i] + position;

			//If tile is out of bounds
			if (!bounds.Contains((Vector2Int)tilePosition)) {
				return false;
			}

			//If tile already occupied
			if (this.tilemap.HasTile(tilePosition)) {
				return false;
			}
		}

		return true;
	}

	//When game ends
	private void GameOver() {
		this.tilemap.ClearAllTiles();

		// ...
	}

	//Set piece on board
	public void Set(Piece piece) {
		//Place tiles of the piece on the tilemap
		for (int i = 0; i < piece.cells.Length; i++) {
			Vector3Int tilePosition = piece.cells[i] + piece.position;
			this.tilemap.SetTile(tilePosition, piece.data.tile);
		}
	}

	//Take piece off board
	public void Clear(Piece piece) {
		//Clear tiles of the piece on the tilemap
		for (int i = 0; i < piece.cells.Length; i++) {
			Vector3Int tilePosition = piece.cells[i] + piece.position;
			this.tilemap.SetTile(tilePosition, null);
		}
	}

	//Clears all full lines on the board
	public void ClearLines() {
		RectInt bounds = this.Bounds;
		int row = bounds.yMin;

		while (row < bounds.yMax) {
			if (IsLineFull(row)) {
				ClearLine(row);
			} else {
				row++;
			}
		}
	}

	//Checks if a line on the board is full and returns bool
	private bool IsLineFull(int row) {
		RectInt bounds = this.Bounds;

		//Iterate over tiles in line
		for (int col = bounds.xMin; col < bounds.xMax; col++) {
			Vector3Int position = new Vector3Int(col, row, 0);

			if (!this.tilemap.HasTile(position)) {
				return false;
			}
		}
		return true;
	}

	//Clears given line on the board
	private void ClearLine(int row) {
		RectInt bounds = this.Bounds;

		//Iterate over tiles in line
		for (int col = bounds.xMin; col < bounds.xMax; col++) {
			//Remove tile
			Vector3Int position = new Vector3Int(col, row, 0);
			this.tilemap.SetTile(position, null);
		}

		//Shift all lines above down
		while (row < bounds.yMax) {
			//Iterate over tiles in row
			for (int col = bounds.xMin; col < bounds.xMax; col++) {
				//Get tile obove
				Vector3Int position = new Vector3Int(col, row + 1, 0);
				TileBase above = this.tilemap.GetTile(position);

				//Shift tile down
				position = new Vector3Int(col, row, 0);
				this.tilemap.SetTile(position, above);
			}
			row++;
		}
	}
}
























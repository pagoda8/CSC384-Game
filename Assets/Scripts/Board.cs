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

	public void SpawnPiece() {
		//Get random tetromino data
		int r = Random.Range(0, this.tetrominos.Length);
		TetrominoData data = this.tetrominos[r];

		//Set active piece
		this.activePiece.Initialise(this, this.spawnPosition, data);
		Set(this.activePiece);
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
}
























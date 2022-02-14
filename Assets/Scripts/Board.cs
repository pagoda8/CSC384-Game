//Implements a Board object

using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
	//Array of defined tetrominos for Board to use
	public TetrominoData[] tetrominos;
	public Piece activePiece { get; private set; }
	public Tilemap tilemap { get; private set; }
	public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);

	//When Board gets initialised
	private void Awake()
	{
		//Gets the Tilemap and Piece to be used
		this.tilemap = GetComponentInChildren<Tilemap>();
		this.activePiece = gameObject.AddComponent<Piece>();

		//Initialise tetromino data
		for (int i = 0; i < this.tetrominos.Length; i++)
		{
			this.tetrominos[i].Initialise();
		}
	}

	//When game starts
	private void Start()
	{
		SpawnPiece();
	}

	public void SpawnPiece()
	{
		//Get random tetromino data
		int r = Random.Range(0, this.tetrominos.Length);
		TetrominoData data = this.tetrominos[r];

		//Set active piece
		this.activePiece.Initialise(this, this.spawnPosition, data);
		Set(this.activePiece);
	}

	//Set piece on board
	public void Set(Piece piece)
	{
		//Place tiles of the piece on the tilemap
		for (int i = 0; i < piece.cells.Length; i++)
		{
			Vector3Int tilePosition = piece.cells[i] + piece.position;
			this.tilemap.SetTile(tilePosition, piece.data.tile);
		}
	}
}

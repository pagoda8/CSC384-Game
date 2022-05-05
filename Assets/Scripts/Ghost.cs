//Implements a ghost piece

using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour  {
    public Tile tile; //Tile to render
    public Board board; //Reference to board
    public Piece trackedPiece; //Piece to track for ghost piece
    public Tilemap tilemap { get; private set; } //Tilemap to place ghost pieces
    public Vector3Int[] cells { get; private set; } //Cells of ghost piece
    public Vector3Int position { get; private set; } //Position on board

    //When ghost piece gets initialised
    private void Awake() {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.cells = new Vector3Int[4];
    }

    //Gets called after all other Update() functions in game have been called
    private void LateUpdate() {
        Clear();
        Copy();
        Drop();
        Set();
    }

    //Clear ghost piece
    private void Clear() {
        for (int i = 0; i < this.cells.Length; i++) {
            Vector3Int tilePosition = this.cells[i] + this.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    //Copy tiles from tracked piece
    private void Copy() {
        for (int i = 0; i < this.cells.Length; i++) {
            this.cells[i] = this.trackedPiece.cells[i];
        }
    }

    //Hard drop ghost piece
    private void Drop() {
        Vector3Int position = this.trackedPiece.position;

        int currentRow = position.y;
        int bottom = -this.board.boardSize.y / 2 - 1;

        this.board.Clear(this.trackedPiece);

        for (int row = currentRow; row >= bottom; row--) {
            position.y = row;

            if (this.board.IsValidPosition(this.trackedPiece, position)) {
                this.position = position;
            } else {
                break;
            }
        }

        this.board.Set(this.trackedPiece);
    }

    //Set ghost piece on board
    private void Set() {
        //Place tiles of the ghost piece on the tilemap
        for (int i = 0; i < this.cells.Length; i++) {
            Vector3Int tilePosition = this.cells[i] + this.position;
            this.tilemap.SetTile(tilePosition, this.tile);
        }
    }
}

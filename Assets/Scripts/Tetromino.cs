//List and definition of tetromino shapes

using UnityEngine;
using UnityEngine.Tilemaps;

//Names of shapes
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

//Enable the editor to interpret shape data
[System.Serializable]

//Definition of shapes
public struct TetrominoData
{
    public Tetromino tetromino;
    public Tile tile;
    public Vector2Int[] cells { get; private set; }

    public void Initialise()
    {
        //Assign cell data
        this.cells = Data.Cells[this.tetromino]
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Transform _cam;


    private Dictionary<Vector2, Tile> _tiles;

    private void Awake()
    {
        instance = this;
    }


    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x%2 == 0 && y%2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);


                _tiles[new Vector2(x,y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.instance.ChangeState(Gamestate.SpawnPawn);
    }

    public Tile GetPawnSpawnTile() 
    {
     //return _tiles.Where(t=> t.Key.x < _width/2 && t.Value.walkable).OrderBy(t=>Random.value).First().Value;
    return _tiles[new Vector2(2,1)];
    
    }


    public Tile GetKingSpawnTile() 
    {
        // fer un switch amb les diferents pe�es i fer return per cada pe�a
        return _tiles[new Vector2(2, 5)];


    }



    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}

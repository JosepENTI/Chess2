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

                
                spawnedTile.Init(x,y);


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

    public Tile GetInitialTile()
    {        
        return _tiles[new Vector2(0, 0)];

    }

    public Tile GetKingSpawnTile() 
    {
        // fer un switch amb les diferents peçes i fer return per cada peça
        return _tiles[new Vector2(2, 5)];
    }

    public Tile GetEnemySpawnTile()
    {
       
       return _tiles.Where(t => t.Key.y > _height / 2 ).OrderBy(t => Random.value).First().Value;
        
                    
    }

    public Tile GetWallSpawnTile()
    {

        return _tiles.Where(t => t.Key.y > _height / 4).OrderBy(t => Random.value).First().Value;


    }



    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }

    public void SetWalkableOff()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                GridManager.instance.GetTileAtPosition(new Vector2(0 + i, 0 + j)).isWalkable = false;
            }

        }

    }
}

using UnityEngine;
using Object = UnityEngine.Object;

public class Chunk
{
    private GameObject _chunkObject;
    private int _chunkX = 0;
    private int _chunkY = 0;

    private short[,] tiles = new short[16, 16];

 
    public Chunk(GameObject obj, int x, int y)
    {
        this._chunkObject = obj;
        this._chunkX = x;
        this._chunkY = y;
    }
    
    public short[,] GetTiles()
    {
        return tiles;
    }

    public int ChunkX
    {
        get => _chunkX;
        set => _chunkX = value;
    }

    public int ChunkY
    {
        get => _chunkY;
        set => _chunkY = value;
    }

    public GameObject ChunkObject
    {
        get => _chunkObject;
        set => _chunkObject = value;
    }

    public string GetName()
    {
        return _chunkX + ":" + _chunkY;
    }

    public void SpawnChunkTiles()
    {
        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 16; x++)
            {
                switch (tiles[y, x])
                {
                    case 1:  // air
                        break;
                    case 2: // stone
                        Object.Instantiate(ChunkManager.Stone, new Vector2( _chunkX * 16 + x, _chunkY * 16 + y), Quaternion.identity).transform.SetParent(this._chunkObject.transform);
                        break;
                    case 3: // grass
                        Object.Instantiate(ChunkManager.Grass, new Vector2(_chunkX * 16 + x, _chunkY * 16 + y), Quaternion.identity).transform.SetParent(_chunkObject.transform);;
                        break;
                    case 4: // dirt
                        Object.Instantiate(ChunkManager.Dirt, new Vector2(_chunkX * 16 + x, _chunkY * 16 + y), Quaternion.identity).transform.SetParent(_chunkObject.transform);;
                        break;
                    case 5: // unknown
                        break;
                }
            }
        }
    }

    public void RespawnChunk()
    {
        foreach (Transform child in this.ChunkObject.transform) {
            Object.Destroy(child.gameObject);
        }

        SpawnChunkTiles();
    }
}
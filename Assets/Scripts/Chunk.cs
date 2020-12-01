using UnityEngine;

public class Chunk
{
    private GameObject _chunkObject;
    private float _chunkX = 0;
    private float _chunkY = 0;

    private short[,] tiles = new short[16, 16];

 
    public Chunk(GameObject obj, float x, float y)
    {
        this._chunkObject = obj;
        this._chunkX = x;
        this._chunkY = y;
    }
    
    public short[,] GetTiles()
    {
        return tiles;
    }

    public float ChunkX
    {
        get => _chunkX;
        set => _chunkX = value;
    }

    public float ChunkY
    {
        get => _chunkY;
        set => _chunkY = value;
    }

    public GameObject ChunkObject
    {
        get => _chunkObject;
        set => _chunkObject = value;
    }

    public void SpawnChunkTiles()
    {
        Debug.Log("spawn");
        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 16; x++)
            {
                switch (tiles[y, x])
                {
                    case 1:  // air
                        break;
                    case 2: // stone
                        Object.Instantiate(ChunkManager.Stone, new Vector2((int) _chunkX * 16 + x, (int)_chunkY * 16 + y), Quaternion.identity).transform.SetParent(this._chunkObject.transform);
                        break;
                    case 3: // grass
                        Object.Instantiate(ChunkManager.Grass, new Vector2(), Quaternion.identity).transform.SetParent(_chunkObject.transform);;
                        break;
                    case 4: // dirt
                        Object.Instantiate(ChunkManager.Dirt, new Vector2(), Quaternion.identity).transform.SetParent(_chunkObject.transform);;
                        break;
                    case 5: // unknown
                        break;
                }
            }
        }
    }
}
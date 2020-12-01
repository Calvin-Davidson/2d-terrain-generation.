using UnityEngine;

public class SpawnWorld : MonoBehaviour
{
    [SerializeField] private float height = 100;
    [SerializeField] private float width = 100;

    [SerializeField] private float spawnWhen;

    private void Start()
    {
        CreateWorld();
    }

    public float GetPerlinValue(float x, float y)
    {
        return Mathf.PerlinNoise(x, y);
    }
    
    private void CreateWorld()
    {
        for (int chunkY = 0; chunkY < height / 10; chunkY++)
        {
            for (int chunkX = 0; chunkX < width / 10; chunkX++)
            {
                Chunk chunk = new Chunk(new GameObject {name = chunkX + ":" + chunkY}, chunkX, chunkY);
                chunk.ChunkObject.SetActive(false);
                chunk.ChunkObject.transform.position = new Vector2(chunkX * 16, chunkY * 16);

                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        float perlinValue = GetPerlinValue((float) (chunkX * 16 + x) / 10, (float) (chunkY * 16 + y) / 10);
                        if (perlinValue > spawnWhen)
                        {
                            chunk.GetTiles()[y, x] = (short) TileType.Stone;
                        }
                    }
                }

                ChunkManager.GetChunks().Add(chunk);
                chunk.SpawnChunkTiles();
            }
        }
    }
}
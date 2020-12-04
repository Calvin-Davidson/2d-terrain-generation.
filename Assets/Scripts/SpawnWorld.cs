using System;
using TreeEditor;
using UnityEngine;
using UnityEngine.WSA;
using Random = UnityEngine.Random;

public class SpawnWorld : MonoBehaviour
{
    [SerializeField] private int height = 100;
    [SerializeField] private int width = 100;
    [SerializeField] private float spawnWhen;
    [SerializeField] private int seed;

    [SerializeField] private float heightMultiplier;
    [SerializeField] private int heightAddition;
    [SerializeField] private float smoothness;

    private void Awake()
    {
        if (seed == 0) seed = GeneratedSeed();
    }


    private void Start()
    {
        CreateSurface();
        CreateCaves();

        foreach (var chunk in ChunkManager.GetChunks())
        {
            chunk.SpawnChunkTiles();
        }
    }

    public float GetPerlinValue(float x, float y)
    {
        return Mathf.PerlinNoise(x + seed, y + seed);
    }

    private int GeneratedSeed()
    {
        String intString = String.Empty;
        for (int i = 0; i < 6; i++)
        {
            intString += Random.Range(0, 10);
        }

        return Int32.Parse(intString);
    }

    private void CreateCaves()
    {
        foreach (var chunk in ChunkManager.GetChunks())
        {
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    // if surface chunk
                    if (chunk.ChunkY == height / 10 - 1)
                    {
                        float perlinValue =
                            GetPerlinValue((float) (chunk.ChunkX * 16 + x) / 10, (float) (chunk.ChunkY * 16 + y) / 10);
                        if (perlinValue < spawnWhen/2)
                        {
                            chunk.GetTiles()[y, x] = (short) TileType.Air;
                        }
                    }
                    else
                    {
                        float perlinValue =
                            GetPerlinValue((float) (chunk.ChunkX * 16 + x) / 10, (float) (chunk.ChunkY * 16 + y) / 10);
                        if (perlinValue < spawnWhen)
                        {
                            chunk.GetTiles()[y, x] = (short) TileType.Air;
                        }   
                    }
                }
            }
        }
    }

    private void CreateSurface()
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
                        if (chunk.ChunkY == height / 10 - 1)
                        {
                            float perlinValue = GetPerlinValue((float) x / 10 + chunk.ChunkX,
                                (float) y / 10 + chunk.ChunkY);
                            perlinValue *= 8;
                            if (perlinValue > 15) perlinValue = 15;
                            if (perlinValue < 0) perlinValue = 0;

                            chunk.GetTiles()[(int) perlinValue, x] = (short) TileType.Grass;

                            for (int i = (int) perlinValue - 1; i > -1; i--)
                            {
                                chunk.GetTiles()[i, x] = (short) TileType.Dirt;

                                if (GetPerlinValue(y / 10 + chunkX, x / 10 + chunkY) < 0.5 && i < (int) perlinValue / 2)
                                {
                                    chunk.GetTiles()[i,x] = (short) TileType.Stone;
                                }
                            }

                            if (chunk.GetTiles()[y, x] == (short) TileType.Stone)
                                chunk.GetTiles()[y, x] = (short) TileType.Air;
                        }
                        else
                        {
                            chunk.GetTiles()[y, x] = (short) TileType.Stone;
                        }
                    }
                }

                ChunkManager.GetChunks().Add(chunk);
            }
        }
    }
}
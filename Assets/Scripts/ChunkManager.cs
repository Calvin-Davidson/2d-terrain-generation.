using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private GameObject dirt;
    [SerializeField] private GameObject grass;
    [SerializeField] private GameObject stone;

    public static GameObject Dirt; 
    public static GameObject Grass; 
    public static GameObject Stone;
    private void Awake()
    {
        Dirt = dirt;
        Grass = grass;
        Stone = stone;
    }

    private static List<Chunk> _chunks = new List<Chunk>();

    public static List<Chunk> GetChunks()
    {
        return _chunks;
    }


    public short GetTileTypeFrom(float x, float y)
    {
        return 1;
    }
    
}

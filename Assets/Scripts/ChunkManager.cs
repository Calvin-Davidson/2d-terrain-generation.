using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

public class ChunkManager : MonoBehaviour
{
    //private static readonly List<Chunk> Chunks = new List<Chunk>();
    private static Dictionary<String, Chunk> Chunks = new Dictionary<string, Chunk>();
    private static string _saveFolderPath;
    
    [SerializeField] private GameObject dirt;
    [SerializeField] private GameObject grass;
    [SerializeField] private GameObject stone;

    public static GameObject Dirt; 
    public static GameObject Grass; 
    public static GameObject Stone;
    private void Awake()
    {
        ChunkManager._saveFolderPath = Application.dataPath + "/saves/save_01";
        Dirt = dirt;
        Grass = grass;
        Stone = stone;
    }
    
    public static Dictionary<String, Chunk> GetChunks()
    {
        return Chunks;
    }


    public short GetTileTypeFrom(float x, float y)
    {
        return 1;
    }
    
}

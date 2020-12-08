using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveChunksInCamera : MonoBehaviour
{
    private Transform _camTrans;
    private List<Chunk> _loadedChunks = new List<Chunk>();

    [SerializeField] private int showChunksX;
    [SerializeField] private int showChunksY;
    
    private void Awake()
    {
        if (Camera.main == null) return;
        _camTrans = Camera.main.transform;
        
    }

    public void UpdateChunks()
    {
        for (var i = 0; i < _loadedChunks.Count; i++)
        {
            Chunk chunk = _loadedChunks[i];
            
            String chunkName = Math.Abs(chunk.ChunkX) + ":" + Math.Abs(chunk.ChunkY);
            if ((chunk.ChunkX + ":" + chunk.ChunkY).Equals(chunkName))
            {
                if (Vector3.Distance(_camTrans.position, chunk.ChunkObject.transform.position) > 20)
                {
                    chunk.ChunkObject.SetActive(false);
                }
                break;
            }
        }  
        var position = _camTrans.position;
        float camX = position.x;
        float camY = position.y;

        int camChunkX = (int) (camX / 16);
        int camChunkY = (int) (camY / 16);

        for (int i = -Math.Abs(showChunksY); i < Math.Abs(showChunksY) + 1; i++)
        {
            for (int j = -Math.Abs(showChunksX); j < Math.Abs(showChunksX) + 1; j++)
            {
                String chunkName = Math.Abs(camChunkX + j) + ":" + Math.Abs(camChunkY + i);
                
                if (AlreadyActive(chunkName)) continue;
                
                foreach (var chunk in ChunkManager.GetChunks())
                {
                    if ((chunk.ChunkX + ":" + chunk.ChunkY).Equals(chunkName) && !_loadedChunks.Contains(chunk))
                    {
                        chunk.ChunkObject.SetActive(true);
                        _loadedChunks.Add(chunk);
                        break;
                    }
                }
            }
        }
    }

    private bool AlreadyActive(string chunkName)
    {
        foreach (var loadedChunk in _loadedChunks)
        {
            if ((loadedChunk.ChunkX + ":" + loadedChunk.ChunkY).Equals(chunkName))
            {
                return true;
            }
        }

        return false;
    }
}
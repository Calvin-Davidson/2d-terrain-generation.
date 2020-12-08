using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private void Start()
    {
        UpdateChunks();
    }

    public void UpdateChunks()
    {
        var position = _camTrans.position;
        float camX = position.x;
        float camY = position.y;

        int camChunkX = (int) (camX / 16);
        int camChunkY = (int) (camY / 16);

        List<Chunk> newChunks = new List<Chunk>();
        
        for (int i = -Math.Abs(showChunksY); i < Math.Abs(showChunksY) + 1; i++)
        {
            for (int j = -Math.Abs(showChunksX); j < Math.Abs(showChunksX) + 1; j++)
            {
                String chunkName = Math.Abs(camChunkX + j) + ":" + Math.Abs(camChunkY + i);
                
                if (!ChunkManager.GetChunks().ContainsKey(chunkName)) return;

                Chunk chunk = ChunkManager.GetChunks()[chunkName];
                newChunks.Add(chunk);
                
                if (AlreadyActive(chunk)) continue;

                if (chunk.GetName().Equals(chunkName))
                {
                    chunk.ChunkObject.SetActive(true);
                    _loadedChunks.Add(chunk);
                    break;
                }
            }
        }
        
        for (var i = 0; i < _loadedChunks.Count; i++)
        {
            if (!newChunks.Contains(_loadedChunks[i]))
            {
                _loadedChunks[i].ChunkObject.SetActive(false);
                _loadedChunks.RemoveAt(i);
                return;
            }
        }
    }
    private bool AlreadyActive(Chunk chunk)
    {
        return _loadedChunks.Contains(chunk);
    }
}


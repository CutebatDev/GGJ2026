using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MaskManager : MonoBehaviour
{
    
    public static MaskManager Instance { get; private set; }
    
    private List<GameObject> spriteMasksAll;
    private List<GameObject> spriteMasksActive;
    private LayerManager layerManager;

    private Dictionary<E_TerrainLayerTags, List<SpriteRenderer>> terrainsByLayer;
    
    
    private void Awake() // singleton
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        layerManager = LayerManager.Instance;
        
        
        spriteMasksAll = new List<GameObject>();
        spriteMasksActive = new List<GameObject>();
        
        
        //change to foreach enum
        spriteMasksAll.AddRange(GameObject.FindGameObjectsWithTag(E_CollisionLayerTags.CollisionMask1.ToString()));
        spriteMasksAll.AddRange(GameObject.FindGameObjectsWithTag(E_CollisionLayerTags.CollisionMask2.ToString()));
        spriteMasksAll.AddRange(GameObject.FindGameObjectsWithTag(E_CollisionLayerTags.CollisionMask3.ToString()));
        
        
        Debug.Log(spriteMasksAll);

        FindAllTerrains();
        
        UpdateMaskLayer();
        DisableMasks();
    }

    private void FindAllTerrains()
    {
        terrainsByLayer = new Dictionary<E_TerrainLayerTags, List<SpriteRenderer>>();
        
        foreach (E_TerrainLayerTags layer in System.Enum.GetValues(typeof(E_TerrainLayerTags)))
        {
            if (layer == E_TerrainLayerTags.maxVal) continue;
            
            GameObject[] terrainObjects = GameObject.FindGameObjectsWithTag(layer.ToString());

            terrainsByLayer[layer] = new List<SpriteRenderer>();
            
            foreach (GameObject terrain in terrainObjects)
            {
                SpriteRenderer sr = terrain.GetComponent<SpriteRenderer>();
                terrainsByLayer[layer].Add(sr);
            }
            
        }
    }
    public void UpdateMaskLayer()
    {
        spriteMasksActive.Clear();
        foreach (var VARIABLE in spriteMasksAll)
        {
            VARIABLE.SetActive(false);
            if (VARIABLE.CompareTag(layerManager.currentLayerTag.ToString()))
            {
                spriteMasksActive.Add(VARIABLE);
            }
        }
        UpdateTerrainMaskInteraction();
    }
    
    private void UpdateTerrainMaskInteraction()
    {
        E_TerrainLayerTags currentLayer = layerManager.currentTerrainTag;
        
        foreach (var kvp in terrainsByLayer)
        {
            E_TerrainLayerTags layer = kvp.Key;
            List<SpriteRenderer> sprites = kvp.Value;
            
            SpriteMaskInteraction interaction;
            
            if (layer == currentLayer)
            {
                // Current layer: masks should affect this terrain
                interaction = SpriteMaskInteraction.VisibleOutsideMask;
            }
            else
            {
                // Other layers: masks should NOT affect this terrain
                interaction = SpriteMaskInteraction.None;
            }
            
            // Apply to all sprites in this layer
            foreach (SpriteRenderer sr in sprites)
            {
                if (sr != null)
                {
                    sr.maskInteraction = interaction;
                }
            }
        }
        
    }
    
    public void UpdateMaskPosition(Vector2 newPositiion, Vector2 size)
    {
        SpriteRenderer sr;
        foreach (var VARIABLE in spriteMasksActive)
        {
            sr = VARIABLE.GetComponent<SpriteRenderer>();
            VARIABLE.transform.position = newPositiion;
            VARIABLE.transform.localScale = size * 10;
        }
    }

    public void DisableMasks()
    {
        foreach (var VARIABLE in spriteMasksActive)
        {
            VARIABLE.SetActive(false);
        }
    }
    public void EnableMasks()
    {
        foreach (var VARIABLE in spriteMasksActive)
        {
            VARIABLE.SetActive(true);
        }
    }
    
}
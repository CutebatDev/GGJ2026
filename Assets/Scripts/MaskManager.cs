using System;
using UnityEngine;

public enum E_CollisionLayerTags
    {
        CollisionMask,
        CollisionMask2,
        CollisionMask3
    }
public class MaskManager : MonoBehaviour
{
    
    public static MaskManager Instance { get; private set; }
    
    private GameObject[] spriteMasks;

    public E_CollisionLayerTags currentLayerTag = E_CollisionLayerTags.CollisionMask;

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

        spriteMasks = GameObject.FindGameObjectsWithTag(currentLayerTag.ToString());
        DisableMasks();
    }
    
    
    public void UpdateMaskPosition(Vector2 newPositiion, Vector2 size)
    {
        SpriteRenderer sr;
        foreach (var VARIABLE in spriteMasks)
        {
            sr = VARIABLE.GetComponent<SpriteRenderer>();
            VARIABLE.transform.position = newPositiion;
            VARIABLE.transform.localScale = size * 10;
        }
    }

    public void DisableMasks()
    {
        foreach (var VARIABLE in spriteMasks)
        {
            VARIABLE.SetActive(false);
        }
    }
    public void EnableMasks()
    {
        foreach (var VARIABLE in spriteMasks)
        {
            VARIABLE.SetActive(true);
        }
    }
    
}
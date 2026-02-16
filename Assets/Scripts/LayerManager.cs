using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum E_CollisionLayerTags
{
    CollisionMask1 = 0,
    CollisionMask2,
    CollisionMask3,
    maxVal
}

public enum E_TerrainLayerTags
{
    ColliderRender1 = 0,
    ColliderRender2,
    ColliderRender3,
    maxVal
}
public class LayerManager : MonoBehaviour
{
    [SerializeField] private InputActionReference switchLayerInput;
 
    [HideInInspector] public E_CollisionLayerTags currentLayerTag = E_CollisionLayerTags.CollisionMask1;
    [HideInInspector] public E_TerrainLayerTags currentTerrainTag = E_TerrainLayerTags.ColliderRender1;

    [SerializeField] private MaskManager maskManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SpriteRenderer bgRendered;
    [SerializeField] private Material[] bgMaterials;
    
    
    private void OnEnable()
    {
        switchLayerInput.action.started += SwitchToNextLayer;

    }
    
    private void OnDisable()
    {
        switchLayerInput.action.started -= SwitchToNextLayer;
    }

    private void Start()
    {
        bgRendered.material = bgMaterials[0];
    }

    public void SwitchToNextLayer(InputAction.CallbackContext ctx)
    {
		SwitchToLayer(((((int)currentLayerTag) + 1) % (int)E_CollisionLayerTags.maxVal))
    }

    public void SwitchToLayer(int layerNum)
    {
        bgRendered.material = bgMaterials[layerNum - 1];
        audioManager.ChangeAudioTrack(layerNum);
        currentLayerTag = (E_CollisionLayerTags)layerNum - 1;
        currentTerrainTag = (E_TerrainLayerTags)layerNum - 1;

        maskManager.UpdateMaskLayer();

    }
}
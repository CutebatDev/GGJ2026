// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.UI;
//
//
// public class SJBADSD : MonoBehaviour
// {
//     
//     public static MaskManager Instance { get; private set; }
//     
//     private List<GameObject> spriteMasksAll;
//     private List<GameObject> spriteMasksActive;
//     private LayerManager layerManager;
//
//     private bool _initiated = false; 
//     
//     private void Awake() // singleton
//     {
//         if (Instance != null && Instance != this)
//         {
//             Destroy(this.gameObject);
//         }
//         else
//         {
//             Instance = this;
//         }
//
//         layerManager = LayerManager.Instance;
//         
//         
//         spriteMasksAll = new List<GameObject>();
//         spriteMasksActive = new List<GameObject>();
//         
//         
//         spriteMasksAll.AddRange(GameObject.FindGameObjectsWithTag(E_CollisionLayerTags.CollisionMask.ToString()));
//         spriteMasksAll.AddRange(GameObject.FindGameObjectsWithTag(E_CollisionLayerTags.CollisionMask2.ToString()));
//         
//         
//         Debug.Log(spriteMasksAll);
//
//         UpdateMaskLayer();
//         DisableMasks();
//     }
//
//     public void UpdateMaskLayer()
//     {
//         foreach (var VARIABLE in spriteMasksAll)
//         {
//             VARIABLE.SetActive(false);
//             spriteMasksActive.Clear();
//             if (VARIABLE.CompareTag(layerManager.currentLayerTag.ToString()))
//             {
//                 spriteMasksActive.Add(VARIABLE);
//             }
//         }
//     }
//     
//     public void UpdateMaskPosition(Vector2 newPositiion, Vector2 size)
//     {
//         SpriteRenderer sr;
//         foreach (var VARIABLE in spriteMasksActive)
//         {
//             sr = VARIABLE.GetComponent<SpriteRenderer>();
//             VARIABLE.transform.position = newPositiion;
//             VARIABLE.transform.localScale = size * 10;
//         }
//     }
//
//     public void DisableMasks()
//     {
//         foreach (var VARIABLE in spriteMasksActive)
//         {
//             VARIABLE.SetActive(false);
//         }
//     }
//     public void EnableMasks()
//     {
//         foreach (var VARIABLE in spriteMasksActive)
//         {
//             VARIABLE.SetActive(true);
//         }
//     }
//     
// }
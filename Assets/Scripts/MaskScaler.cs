// using System;
// using UnityEngine;
//
// public class MaskScaler : MonoBehaviour
// {
//     [SerializeField] 
//     private SpriteMask spriteMask;
//     
//     /*
//      * stretch ann place
//      */
//     
//     public void UpdateSpriteMask(Rect rect)
//     {
//         
//         rt.anchorMin = new Vector2(0, 0);
//         rt.anchorMax = new Vector2(0, 0);
//         rt.pivot = new Vector2(0, 0);
//
//         // Set position and size based on the screen Rect
//         rt.position = new Vector3(screenRect.x, screenRect.y, rt.position.z);
//         rt.sizeDelta = new Vector2(screenRect.width, screenRect.height);
//     }
// }
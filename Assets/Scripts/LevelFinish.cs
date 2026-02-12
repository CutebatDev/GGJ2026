using System;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private int nextLvl = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameStateManager.LoadLevel(nextLvl);
    }
}

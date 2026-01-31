using System;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameStateManager.LoadNextLevel();
    }
}

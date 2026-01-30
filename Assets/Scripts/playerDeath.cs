using System;
using UnityEngine;

public class playerDeath : MonoBehaviour
{
    public Transform spawnPointTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        spawnPointTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        void ColideWithHazard(Collider2D other)
        {
            if (other.gameObject.CompareTag("Hazard"))
            {
                transform.position = spawnPointTransform.position;
            }

        }
    }
}





// pseudocode
/*
get component spawn point position (spawn point = empty object prefab)
if player colides on trigger with tag hazard -> player position = spawn point position
how do you get the respawnpoint instead of dragging it each time?
*/
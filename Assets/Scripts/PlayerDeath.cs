using System;
using System.Collections;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    [SerializeField] private AudioManager audioManager;
    private bool isInWall;

    private int breatheTicks = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            HandleDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered Trigger");
        if (other.gameObject.CompareTag("Suffocate"))
        {
            Debug.Log("Entered wall");
            isInWall = true;
            StartCoroutine(Suffocate());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exited Trigger");
        
        if (other.gameObject.CompareTag("Suffocate"))
        {
            Debug.Log("Exited wall");
            breatheTicks = 3;
            isInWall = false;
            
        }
    }

    private IEnumerator Suffocate()
    {
        var time = Time.fixedTime;
        while (isInWall && breatheTicks > 0)
        {
            Debug.Log("Timer tick");
            breatheTicks--;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Timer Ended");
        
        if(isInWall)
            HandleDeath();
        
    }
    private void HandleDeath()
    {
        GameStateManager.RestartScene();
        audioManager.PlayDeathSFX();
    }
}

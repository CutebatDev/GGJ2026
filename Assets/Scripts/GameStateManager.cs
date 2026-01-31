using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private InputActionReference NextLevelInput;
    [SerializeField] private InputActionReference RestartLevelInput;

    static private int currentLevelNumber = 0;

    private void OnEnable()
    {
        NextLevelInput.action.started += LoadNextLevelPress;
        RestartLevelInput.action.started += RestartScenePress;
    }

    private void OnDisable()
    {
        
    }


    private static void RestartScenePress(InputAction.CallbackContext ctx)
    {
        RestartScene();
    }
    public static void RestartScene()
    {
        LoadLevel(currentLevelNumber);
    }

    public static void LoadScene(Scene newScene)
    {
        SceneManager.LoadScene(newScene.name);
    }
    
    public static void LoadLevel(int levelNumber)
    {
        currentLevelNumber = levelNumber;
        SceneManager.LoadScene(levelNumber);
    }

    private static void LoadNextLevelPress(InputAction.CallbackContext ctx)
    {
        LoadNextLevel();
    }
    public static void LoadNextLevel()
    {
        LoadLevel(currentLevelNumber + 1);
    }

}

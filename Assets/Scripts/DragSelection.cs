using UnityEngine;
using UnityEngine.InputSystem;

public class DragSelection : MonoBehaviour
{
    [SerializeField] private InputActionReference pointerInput;
    [SerializeField] private InputActionReference clickInput;
    [SerializeField] private InputActionReference pauseInput;
    [SerializeField] private SpriteRenderer debugSprite;
    private Vector2 startWorldPos;
    private bool dragging;

    private bool isPaused = false;
    private bool pauselock = false;

    [SerializeField] private MaskManager maskManager;
    
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }
    
    private void OnEnable()
    {
        clickInput.action.started += OnClick;
        clickInput.action.canceled += OnRelease;
        pointerInput.action.performed += OnDrag;
        pauseInput.action.performed += Pause;
    }
    
    private void OnDisable()
    {
        clickInput.action.started -= OnClick;
        clickInput.action.canceled -= OnRelease;
        pointerInput.action.performed -= OnDrag;
        pauseInput.action.performed -= Pause;
    }
    
    private void OnClick(InputAction.CallbackContext ctx)
    {
        pauselock = true;
        Time.timeScale = 0;
        maskManager.DisableMasks();
        debugSprite.enabled = true;
        
        
        Vector2 screenPos = pointerInput.action.ReadValue<Vector2>();
        Debug.Log(cam);
        startWorldPos = cam.ScreenToWorldPoint(screenPos);
    
        dragging = true;
    
        debugSprite.transform.position = startWorldPos;
        debugSprite.size = Vector2.zero;
        debugSprite.enabled = true;
    }
    
    private void OnDrag(InputAction.CallbackContext ctx)
    {
        
        if (!dragging) return;
    
        Vector2 screenPos = pointerInput.action.ReadValue<Vector2>();
        Vector2 currentWorldPos = cam.ScreenToWorldPoint(screenPos);
    
        Vector2 size = new Vector2(currentWorldPos.x - startWorldPos.x,currentWorldPos.y - startWorldPos.y);
    
    
        debugSprite.size = size;
        debugSprite.transform.position = startWorldPos + size/2;
    }
    
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        if (!isPaused){
            Time.timeScale = 1;
            pauselock = false;
        }
        maskManager.UpdateMaskPosition(debugSprite.transform.position, debugSprite.size);
        maskManager.EnableMasks();
        debugSprite.enabled = false;
        
        dragging = false;
    }

    public void Pause()
    {
        if (isPaused)
        {
            Unpause();
            return;
        }
        if(pauselock)
            return;
        Time.timeScale = 0;
        isPaused = true;
    }
    public void Pause(InputAction.CallbackContext ctx)
    {
        if (isPaused)
        {
            Unpause();
            return;
        }
        if(pauselock)
            return;
        Time.timeScale = 0;
        isPaused = true;
    }
    private void Unpause()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}
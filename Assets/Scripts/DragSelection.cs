using UnityEngine;
using UnityEngine.InputSystem;

public class DragSelection : MonoBehaviour
{
    [SerializeField] private InputActionReference pointerInput;
    [SerializeField] private InputActionReference clickInput;
    [SerializeField] private SpriteRenderer debugSprite;
    
    private Vector2 startWorldPos;
    private bool dragging;

    [SerializeField] private MaskManager maskManager;

    [SerializeField] private float Selection_Max_Width = 1000;
    [SerializeField] private float Selection_Max_Heigt = 1000;
    
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
    }
    
    private void OnDisable()
    {
        clickInput.action.started -= OnClick;
        clickInput.action.canceled -= OnRelease;
        pointerInput.action.performed -= OnDrag;
    }
    
    private void OnClick(InputAction.CallbackContext ctx)
    {
        Debug.Log("Click");
        Time.timeScale = 0;
        maskManager.DisableMasks();
        debugSprite.enabled = true;
        
        
        Vector2 screenPos = pointerInput.action.ReadValue<Vector2>();
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

        size.x = Mathf.Clamp(size.x, -Selection_Max_Width, Selection_Max_Width);
        size.y = Mathf.Clamp(size.y, -Selection_Max_Heigt, Selection_Max_Heigt);
    
        debugSprite.size = size;
        debugSprite.transform.position = startWorldPos + size/2;
    }
    
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        Debug.Log("Release");
        Time.timeScale = 1;
        maskManager.UpdateMaskPosition(debugSprite.transform.position, debugSprite.size);
        maskManager.EnableMasks();
        debugSprite.enabled = false;
        
        dragging = false;
    }
}
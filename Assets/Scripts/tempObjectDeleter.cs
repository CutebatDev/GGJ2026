using UnityEngine;

public class tempObjectDeleter : MonoBehaviour
{
    private GameObject clickedObject;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                clickedObject = hit.collider.gameObject;
                Destroy(clickedObject.gameObject);
            }
        }
    }

}

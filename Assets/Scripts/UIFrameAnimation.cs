using UnityEngine;
using UnityEngine.UI;

public class UIFrameAnimation : MonoBehaviour
{
    public Sprite[] frames;
    public float fps = 12f;
    public bool loop = true;

    private Image image;
    private int index;
    private float timer;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.unscaledDeltaTime; // ignores timescale (good for UI)

        if (timer >= 1f / fps)
        {
            timer = 0f;
            index++;

            if (index >= frames.Length)
            {
                if (loop)
                    index = 0;
                else
                    index = frames.Length - 1;
            }

            image.sprite = frames[index];
        }
    }
}

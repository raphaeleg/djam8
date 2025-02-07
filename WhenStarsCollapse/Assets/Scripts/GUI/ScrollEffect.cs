using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gives a Raw Image a constant Scroll effect.
/// </summary>
public class ScrollEffect : MonoBehaviour
{
    private RawImage img;
    [SerializeField] private Vector2 displacement = new(0.01f,0f);

    private void Start()
    {
        img = GetComponent<RawImage>();
    }
    private void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + displacement * Time.deltaTime, img.uvRect.size);
    }
}

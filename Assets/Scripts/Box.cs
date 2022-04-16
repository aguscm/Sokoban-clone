using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color colorWhenAllocated;
    public bool isAllocated = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BoxTarget"))
        {
            spriteRenderer.color = colorWhenAllocated;
            isAllocated = true;
        }

        LevelManager.instance.CheckIfWin();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BoxTarget"))
        {
            spriteRenderer.color = originalColor;
            isAllocated = false;
        }
    }
}

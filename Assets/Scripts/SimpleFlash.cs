using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
    [SerializeField]
    private Material flashMaterial;
    [SerializeField]
    private float duration = 2f;
    [SerializeField]
    private int timesFlashed = 4;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

     public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        for(int i=0; i<timesFlashed; i++)
        {
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds((duration/timesFlashed)/2);
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds((duration/timesFlashed)/2);
        }
        flashRoutine = null;
    }

}


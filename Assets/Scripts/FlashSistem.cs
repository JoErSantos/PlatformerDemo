using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSistem : MonoBehaviour
{
    [SerializeField]
    private Material dmgFlash;
    
    [SerializeField]
    private Material healingFlash;
    
    [SerializeField]
    private Material invencibilityFlash;


    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

     public void Flash(float effectDuration,int numberOfFlash)
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(MultipleFlashRoutine(effectDuration,numberOfFlash,dmgFlash));
    }

    public void Flash(int index, float effectDuration)
    {
        if(flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        if(index == 1)
            flashRoutine = StartCoroutine(SingleFlashRoutine(effectDuration, healingFlash));
        else if(index == 2)
            flashRoutine = StartCoroutine(SingleFlashRoutine(effectDuration, invencibilityFlash));
    }

    private IEnumerator MultipleFlashRoutine(float effectDuration,int numberOfFlash,Material flashMaterial)
    {
        for(int i=0; i<numberOfFlash; i++)
        {
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds((effectDuration/numberOfFlash)/2);
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds((effectDuration/numberOfFlash)/2);
        }
        flashRoutine = null;
    }

    private IEnumerator SingleFlashRoutine(float effectDuration, Material appliedMaterial)
    {
        spriteRenderer.material = appliedMaterial;
        yield return new WaitForSeconds(effectDuration);
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
    }

}


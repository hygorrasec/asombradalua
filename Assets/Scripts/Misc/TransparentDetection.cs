using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]  // Range limita o valor mínimo e máximo que a variável pode ter
    [SerializeField] private float transparencyAmount = 0.8f;  // SerializeField permite que a variável seja visível no Inspector
    [SerializeField] private float fadeTime = 0.4f;  // SerializeField permite que a variável seja visível no Inspector

    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    // OnTriggerEnter2D é chamado quando o Collider2D entra no trigger.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Enter spriteRenderer = " + spriteRenderer);
        //Debug.Log("Enter tilemap = " + tilemap);
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (spriteRenderer)
            {
                StartCoroutine(FadeRoutineSpriteRenderer(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));
            }
            else if (tilemap)
            {
                StartCoroutine(FadeRoutineTilemap(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
            }
        }
    }

    // OnTriggerExit2D é chamado quando o Collider2D sai do trigger.
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Exit spriteRenderer = " + spriteRenderer);
        //Debug.Log("Exit tilemap = " + tilemap);
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (spriteRenderer)
            {
                StartCoroutine(FadeRoutineSpriteRenderer(spriteRenderer, fadeTime, spriteRenderer.color.a, 1f));
            }
            else if (tilemap)
            {
                StartCoroutine(FadeRoutineTilemap(tilemap, fadeTime, tilemap.color.a, 1f));
            }
        }
    }

    private IEnumerator FadeRoutineSpriteRenderer(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeTime);
            Color newColor = spriteRenderer.color;
            newColor.a = Mathf.Lerp(startValue, targetTransparency, t);
            spriteRenderer.color = newColor;
            yield return null;
        }
    }

    private IEnumerator FadeRoutineTilemap(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeTime);
            Color newColor = tilemap.color;
            newColor.a = Mathf.Lerp(startValue, targetTransparency, t);
            tilemap.color = newColor;
            yield return null;
        }
    }
}

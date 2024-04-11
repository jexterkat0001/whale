using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whaleSpotterScript : MonoBehaviour
{
    [SerializeField] private float lifetime;
    public SpriteRenderer spriteRenderer;

    private Vector3 position;

    public void spawnAt(Vector3 particlePosition)
    {
        position = particlePosition;
        StartCoroutine(spawn());
    }
    IEnumerator spawn()
    {
        transform.position = position;
        transform.GetComponent<CircleCollider2D>().enabled = true;
        for (int i = 0; i < lifetime * 25; i++)
        {
            spriteRenderer.color = new Color(0f, 1f, 0f, 0.1f * Mathf.Cos(i * Mathf.PI / 50 / lifetime));
            yield return new WaitForSeconds(0.04f);
        }
        spriteRenderer.color = new Color(0f, 1f, 0f, 0f);
        transform.GetComponent<CircleCollider2D>().enabled = false;
    }
}


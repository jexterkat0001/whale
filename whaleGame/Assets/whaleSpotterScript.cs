using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whaleSpotterScript : MonoBehaviour
{
    [SerializeField] private int lifetime;
    public SpriteRenderer spriteRenderer;

    IEnumerator spawn(float x, float y)
    {
        transform.position = new Vector3(x, y, -0.75f);
        gameObject.SetActive(true);
        for(int i = 0; i < lifetime * 25; i++)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.2f * Mathf.Cos(i * Mathf.PI / 50 / lifetime));
            yield return new WaitForSeconds(0.04f);
        }
        gameObject.SetActive(false);
    }
}

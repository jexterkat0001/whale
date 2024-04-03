using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipUpgradeScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D shipCollider;
    public Sprite[] shipSprites;
    
    Vector2[] sizes = {new Vector2(1f,0.1f), new Vector2(1.4f,0.2f), new Vector2(2f,0.25f), new Vector2(2.8f,0.35f)};

    public int ship = 0;

    [ContextMenu("upgrade ship")]
    public void upgradeShip()
    {
        ship++;
        spriteRenderer.sprite = shipSprites[ship];
        shipCollider.size = sizes[ship];
    }
}

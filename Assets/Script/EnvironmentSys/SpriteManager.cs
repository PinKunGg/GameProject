using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpriteManager : MonoBehaviour
{
    SpriteRenderer SpriteRenderer, PlayerSpriteRender;
    Transform playerPos;
    public TileMapReader tileMapReader;
    public bool isChangeSort, isPlayerIn;
    public Vector2 CheckDis, CenterCheckPos;
    public LayerMask layer;

    private void Awake()
    {
        SpriteRenderer = this.GetComponent<SpriteRenderer>();
        PlayerSpriteRender = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        playerPos = GameObject.Find("Player").transform;  
        tileMapReader = GameObject.Find("GM").GetComponent<TileMapReader>();
    }
    void Start()
    {
        //this.enabled = false;
    }

    void Update()
    {
        if(SpriteRenderer.sortingOrder > PlayerSpriteRender.sortingOrder && this.gameObject.CompareTag("Tree"))
        {
            isPlayerIn = Physics2D.OverlapBox(this.transform.position + (Vector3)CenterCheckPos,CheckDis,0f, layer);
        }
        if(playerPos.transform.position.y < this.transform.position.y)
        {
            if(!isChangeSort)
            {
                SpriteRenderer.sortingOrder = SpriteRenderer.sortingOrder -= 2;
                isChangeSort = true;
            }
        }
        else
        {
            if(isChangeSort)
            {
                SpriteRenderer.sortingOrder = SpriteRenderer.sortingOrder += 2;
                isChangeSort = false;
            }
        }

        if(SpriteRenderer.sortingOrder > PlayerSpriteRender.sortingOrder && isPlayerIn)
        {
            Color color = SpriteRenderer.color;
            if(color.a != 0.3f)
            {
                color.a = 0.3f;
                SpriteRenderer.color = color;
            }
            else
            {
                return;
            }
        }
        else
        {
            Color color = SpriteRenderer.color;

            if(color.a != 1f)
            {
                color.a = 1f;
                SpriteRenderer.color = color;
            }
            else
            {
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position + (Vector3)CenterCheckPos,CheckDis);
    }
}

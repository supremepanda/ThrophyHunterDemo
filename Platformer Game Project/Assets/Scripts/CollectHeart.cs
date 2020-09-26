using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHeart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if(player.GetHealth() < 3) 
            {
                Destroy(gameObject);
            }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.0f);
        Destroy(gameObject);
        //check for enemy layer
        if(collision.gameObject.layer == 13)
        {
            Destroy(collision.gameObject);
        }
    }
}

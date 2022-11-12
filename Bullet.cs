using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        // do other functions if you wish, then bullet destroys itself on collission
        Destroy(gameObject);
    }

}

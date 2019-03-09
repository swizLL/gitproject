using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float Speed = 100f;
    private void LateUpdate()
    {
        gameObject.transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.enemy)
        {
            Destroy(this.gameObject);
        }
    }
}

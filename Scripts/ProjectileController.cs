using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject explosion;
    public int damage;

    private void Start()
    {
        Invoke("DestroyProjetile", lifeTime);
    }
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    
    void DestroyProjetile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjetile();
        }
    }


}


using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public Rigidbody2D rb;
    private ParticleSystem ps;
    public GameObject impact;
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        ps.Play();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        KillerBird bird = other.gameObject.GetComponent<KillerBird>();
        if (bird != null)
        {
            bird.TakeDamage(damage);
        }
        Destroy(Instantiate(impact, transform.position, transform.rotation),0.15f);
        Destroy(gameObject);
    }
}

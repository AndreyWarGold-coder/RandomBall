using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemi : MonoBehaviour
{
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private Vector2 direct;
    private float Speed;
    private int a;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "D")
        {
            if (a > 0)
            {
                Destroy(gameObject);
            }
            a++;
        }
    }

    public void Move(Vector2 dir, float speed)
    {
        Speed = speed;
        direct = dir;
        StartCoroutine(moved());
    }

    private IEnumerator moved()
    {
        RB.AddForce(direct.normalized*Speed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        StartCoroutine(moved());
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}

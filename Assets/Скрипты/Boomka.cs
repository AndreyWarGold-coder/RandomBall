using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomka : MonoBehaviour
{
    private Boss1 B;
    public Boss2 B2;
    public bool To2;
    [SerializeField] GameObject EffectBoomk;
    public void ItsMe(Boss1 B1)
    {
        B = B1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(To2 == false)
            {
                B.OnBoom();
            }
            else
            {
                B2.OnBoom();
            }
            Destroy(Instantiate(EffectBoomk, gameObject.transform.position, Quaternion.identity), 5f);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}

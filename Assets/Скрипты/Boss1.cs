using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [SerializeField] private int SecondTime, LastTimeUpGame;
    [SerializeField] private GameObject Enemis;
    [SerializeField] private Animator anim;
    [SerializeField] private float MaxY, MaxX, MinY, MinX;
    [SerializeField] private GameObject EffectBoom;
    [SerializeField] private GameObject EffectDead;
    [SerializeField] private GameObject Boom;
    [SerializeField] private int HP;
    public RandomSpawnCoin RSC; 
    void Start()
    {
        StartCoroutine(TimeGo());
    }

    private void NextTime()
    {
        if (SecondTime - LastTimeUpGame == 10)
        {
            LastTimeUpGame = SecondTime;
            OneIvent();
        }
        if(SecondTime == 20)
        {
            anim.Play("Boss1_1");
        }
        if(SecondTime == 40)
        {
            StartCoroutine(SpawnBoom());
        }

    }

    private void Dead()
    {
        RSC.IsBoss = false;
        Destroy(Instantiate(EffectDead, gameObject.transform.position, Quaternion.identity), 1f);
        Destroy(gameObject);
    }

    public void OnBoom()
    {
        if (HP - 1 <= 0)
        {
            Dead();
        }
        else
        {
            HP -= 1;
            Destroy(Instantiate(EffectBoom, gameObject.transform.position, Quaternion.identity), 1f);
        }
        StartCoroutine(SpawnBoom());
    }
    private IEnumerator SpawnBoom()
    {
        yield return new WaitForSeconds(3f);
        float X = Random.Range(MinX, MaxX);
        float Y = Random.Range(MinY, MaxY);
        Instantiate(Boom, new Vector3(X, Y, 0), Quaternion.identity).GetComponent<Boomka>().ItsMe(gameObject.GetComponent<Boss1>());
    }

    private void OneIvent()
    {
        for(int i = 0; i <= 20; i++)
        {
            if (i <= 10)
            {
                StartCoroutine(spawnto(0.5f * i, new Vector2(gameObject.transform.position.x + 3 - (0.5f) * i, gameObject.transform.position.y - (0.2f * i))*-1, 2f, transform.position));
            }
            else
            {
                StartCoroutine(spawnto(0.5f * i, new Vector2(gameObject.transform.position.x - 3 + (0.5f) * (i-10), gameObject.transform.position.y - (0.2f * 10) + (0.2f*(i-10)))*-1, 2f, transform.position));
            }
        }
    }

    private IEnumerator spawnto(float wait, Vector2 dir, float speed, Vector3 spawn)
    {
        yield return new WaitForSeconds(wait);
        SpawnEnemi(dir, speed, spawn);
    }

    private void SpawnEnemi(Vector2 dir, float speed, Vector3 spawn)
    {
        GameObject tests = Instantiate(Enemis, spawn, Quaternion.identity);
        tests.GetComponent<Enemi>().Move(dir, speed);
    }

    private IEnumerator TimeGo()
    { 
        SecondTime++;
        yield return new WaitForSeconds(1f);
        StartCoroutine(TimeGo());
        NextTime();
    }
    void Update()
    {
        
    }
}

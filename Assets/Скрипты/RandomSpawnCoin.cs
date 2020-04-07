using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class RandomSpawnCoin : MonoBehaviour
{
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject Enemis;
    [SerializeField] private Text textVoid;
    [SerializeField] private Text textVoid2;
    [SerializeField] private Animator textVoidAnim;
    [SerializeField] private Animator textVoidAnim2;
    [SerializeField] private Playercontrol PC;
    [SerializeField] private float MaxY, MaxX, MinX, MinY;
    [SerializeField] private float TimeToSpawn;
    [SerializeField] private int SecondTime, LastTimeUpGame;
    [SerializeField] private GameObject Bos;
    public bool IsBoss;

    void Start()
    {

        StartCoroutine(RndSpawn());
        StartCoroutine(RndSpawnEnemi());
        StartCoroutine(TimeGo());
    }

    public void SetTextVoid(string txt)
    {
        textVoid.text = txt;
        textVoidAnim.Play("Visible");

    }


    public void TextAdd(string txt)
    {
        textVoid2.text = txt;
        textVoidAnim2.Play("Visible");
    }

    private void SpawnBoss1()
    {
        IsBoss = true;
        Instantiate(Bos, gameObject.transform.position * 3, Quaternion.identity).GetComponent<Boss1>().RSC = gameObject.GetComponent<RandomSpawnCoin>();

    }

    private void RandVoid()
    {
        int a = Random.Range(0, 100);
        if (a >= 0 && a <= 10)
        {

        }
    }

    private void NextTime()
    {
        if (SecondTime-LastTimeUpGame == 15)
        {
            LastTimeUpGame = SecondTime;
            StartCoroutine(RndSpawnEnemi());
            SetTextVoid("Level Up");
            
        }
        if (SecondTime == 40)
        {
            SpawnBoss1();
            TextAdd("Boss");
        }
        
    }

    private IEnumerator TimeGo()
    {
        if (IsBoss != true)
        {
            SecondTime++;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(TimeGo());
        if(IsBoss != true)
        {
            NextTime();
        }
        

    }

    private IEnumerator RndSpawnEnemi()
    {
        yield return new WaitForSeconds(3f);
        if (IsBoss != true)
        {
            float X = Random.Range(MinX, MaxX);
            Vector3 spawn = new Vector3(X, MaxY, 0);
            X = Random.Range(MinX, MaxX);
            float Y = Random.Range(MinY, 0f);
            Vector2 dir = new Vector2(X, Y);
            float speed = Random.Range(3f, 8f);
            GameObject tests = Instantiate(Enemis, spawn, Quaternion.identity);
            tests.GetComponent<Enemi>().Move(dir, speed);
        }
        StartCoroutine(RndSpawnEnemi());
    }

    private IEnumerator RndSpawn()
    {
        yield return new WaitForSeconds(TimeToSpawn);
        if (IsBoss != true)
        {
            float X = Random.Range(MinX, MaxX);
            float Y = Random.Range(MinY, MaxY);
            Instantiate(Coin, new Vector3(X, Y, 0), Quaternion.identity);
        }
        StartCoroutine(RndSpawn());
    }

 
    void Update()
    {
        
    }
}

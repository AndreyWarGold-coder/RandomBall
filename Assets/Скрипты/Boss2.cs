using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    [SerializeField] private int SecondTime, LastTimeUpGame;
    [SerializeField] private GameObject Enemis;
    [SerializeField] private Animator anim;
    [SerializeField] private float MaxY, MaxX, MinY, MinX;
    [SerializeField] private GameObject EffectBoom;
    [SerializeField] private GameObject EffectDead;
    [SerializeField] private GameObject Boom;
    [SerializeField] private int HP;
    [SerializeField] private Transform[] PointShoot;
    [SerializeField] private float speed;
    [SerializeField] private bool isMove;
    [SerializeField] private Vector2 EndPos;

    public RandomSpawnCoin RSC;
    private void Dead()
    {
        RSC.IsBoss = false;
        Destroy(Instantiate(EffectDead, gameObject.transform.position, Quaternion.identity), 1f);
        Destroy(gameObject);
    }
    private IEnumerator SpawnBoom()
    {
        yield return new WaitForSeconds(3f);
        float X = Random.Range(MinX, MaxX);
        float Y = Random.Range(MinY, MaxY);
        GameObject al = Instantiate(Boom, new Vector3(X, Y, 0), Quaternion.identity);
        al.GetComponent<Boomka>().B2 = gameObject.GetComponent<Boss2>();
        al.GetComponent<Boomka>().To2 = true;
    }
    private IEnumerator spawnto(float wait, Vector2 dir, float speed, Vector3 spawn)
    {
        yield return new WaitForSeconds(wait);
        SpawnEnemi(dir, speed, spawn);
    }
    private void SpawnEnemi(Vector2 dir, float speed, Vector3 spawn)
    {
        GameObject tests = Instantiate(Enemis, spawn, Quaternion.identity);
        tests.GetComponent<Enemi>().SetColor(new Color(0f, 02835703f, 1f));
        tests.GetComponent<Enemi>().Move(dir, speed);
    }
    private void OnVokrugBoom()
    {
        foreach (Transform x in PointShoot)
        {
            Vector2 pos = new Vector2(x.position.x - transform.position.x, x.position.y - transform.position.y);
            StartCoroutine(spawnto(1f, pos, 4f, gameObject.transform.position));
            //SpawnEnemi(x.position, 4f, gameObject.transform.position);
        }
    }
    private IEnumerator TimeGo()
    {
        SecondTime++;
        yield return new WaitForSeconds(1f);
        StartCoroutine(TimeGo());
        NextTime();
    }
    private void Move(Vector2 pos)
    {
        isMove = true;
        EndPos = pos;
        pos = new Vector2(pos.x - transform.position.x, pos.y - transform.position.y);
        StartCoroutine(MoveTo(pos.normalized));
    }
    private IEnumerator MoveTo(Vector2 pos)
    {
        yield return new WaitForSeconds(0.001f);
        if (isMove)
        {
            Vector2 pos_player = new Vector2(transform.position.x, transform.position.y);
            float dis = Vector2.Distance(pos_player, EndPos);
            Debug.Log(dis);
            if (dis > 0.2f)
            {
                transform.Translate(pos*speed*Time.deltaTime);
                StartCoroutine(MoveTo(pos));
            }
            else
            {
                isMove = false;
            }
        }

    }
    private void NextTime()
    {
        if (SecondTime - LastTimeUpGame == 5)
        {
            LastTimeUpGame = SecondTime;
            //anim.Play("Boss2_shoot");
            OnVokrugBoom();
            
        }
        if (SecondTime == 22)
        {
            Move(new Vector2(8.32f, 3.3f));
        }
        if (SecondTime == 32)
        {
            StartCoroutine(SpawnBoom());
            Move(new Vector2(-8.23f, -3.41f));
        }
        if (SecondTime == 42)
        {
            Move(new Vector2(-8.2f, 3.3f));
        }
        if (SecondTime == 52)
        {
            Move(new Vector2(8.8f, -3.3f));
            SecondTime = 12;
        }
        

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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeGo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

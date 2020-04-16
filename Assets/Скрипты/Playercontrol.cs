using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buff
{
    public float speed;
    public string name;
    public float time;
    public bool IsUse;
    public Buff(float sp, string nm, float tim)
    {
        speed = sp;
        name = nm;
        time = tim;
    }
}

public class Playercontrol : MonoBehaviour
{
    [SerializeField] private Rigidbody2D RB;
    private bool IsClick;
    private float StartXPos;
    private float StartYPos;
    private float StartF;
    private List<Buff> Buffs = new List<Buff>();
    [SerializeField] private float F;
    [SerializeField] private Text ScopeText;
    [SerializeField] private int Scope;
    [SerializeField] private RandomSpawnCoin RSC;
    [SerializeField] private GameObject EffectCoin;
    [SerializeField] private GameObject EffectDead;
    [SerializeField] private GameObject EffectCold;
    [SerializeField] private GameObject EffectSpeed;
    [SerializeField] private GameObject EffectShield;

    public void AddBuff(float sp = 0f, string nm = "", float tim = 0f)
    {
        Buffs.Add(new Buff(sp, nm, tim));
        UpdateBuff();
    }

    private void UpdateBuff()
    {
        foreach(Buff x in Buffs)
        {
            if (x.IsUse == false)
            {
                x.IsUse = true;
                StartCoroutine(TimeBuff(x , x.time));
            }
        }
    }

    private IEnumerator TimeBuff(Buff x ,float TimeToLive)
    {

        yield return new WaitForSeconds(TimeToLive);
        F -= x.speed;
        Buffs.Remove(x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy( Instantiate(EffectCoin, collision.gameObject.transform.position, Quaternion.identity), 5f);
            Destroy(collision.gameObject);
            AddScope(1);
        }
        if (collision.gameObject.tag == "E")
        {
            Instantiate(EffectDead, gameObject.transform.position, Quaternion.identity);
            RSC.TextAdd("Gameover");
            Destroy(gameObject);
        }
    }

    private void Move(Vector2 direct)
    {
        RB.AddForce(direct.normalized*F, ForceMode2D.Impulse);
    }

    private void AddScope(int add)
    {
        Scope += add;
        ScopeText.text = "Scope: " + Scope.ToString();
    }

    void Start()
    {
        StartF = F;
        Debug.Log(Screen.currentResolution.height + " Y; X: " + Screen.currentResolution.width);
        RSC.SetTextVoid("Свайп по экрану, для управления");
    }

    
    void Update()
    {
        Vector2 mouse = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            IsClick = true;
            StartXPos = mouse.x;
            StartYPos = mouse.y;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 direct = new Vector2(mouse.x - StartXPos, mouse.y - StartYPos);
            Move(direct);
        }
    }
}

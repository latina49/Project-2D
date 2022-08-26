using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigid;
    public bool checkDelay;
    public Animator animator;
    public float V;
    float delayTime = 0;
    float slowTime = 0;
    float timeAni = 0;
    public float maxHP = 100;
    public float HP;
    public float state_time;
    float current_time;
    float x;
    public GameObject coin;
    public GameObject bonus;
    public bool isLive;
    public HealthBarEnemy HealthBar;
    public enum State
    {
        MoveLeft, MoveRight, Stand, Attack
    }
    State state;

    // Start is called before the first frame update
    public void Init()
    {
        HP = maxHP;
        HealthBar.SetHealth(HP, maxHP);
        V = 0.75f;
        Application.targetFrameRate = 60;
        Time.fixedDeltaTime = 1f / 60f;
        rigid = GetComponent<Rigidbody2D>();
        x = transform.position.x;
        isLive = true;
    }

    public void UpDate()
    {
        
    }
    public void FixedUpdateEnemy(Vector3 c_position, GameObject Slime)
    {
        if (!isLive)
        {            
            return;
        }
        if (slowTime > 0)
        {
            slowTime -= Time.deltaTime;
            if (slowTime <= 0)
                V = 0.75f;
        }
        if (delayTime > 0)
        {
            delayTime -= Time.deltaTime;
        }
        else if (timeAni > 0)
        {
            if (HP <= 0)
            {
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                PlayAnimation("Die", 0, 0f);
            }               
            else
                PlayAnimation("Hurt", 0, 0f);
            timeAni -= Time.deltaTime;
        }
        else 
        {
            if (!checkDetect)
            {
                Wallking(V, 3);
            }
            else
            {
                if (gameObject.tag == "E2")
                    Wallking(V+0.5f, 7);
                if (gameObject.tag == "E1")
                {
                    Wallking(0, 0);
                }                   
            }
            if ((Mathf.Abs(rigid.transform.position.x - c_position.x) < 0.8 && checkDetect == true && gameObject.tag == "E2") || (checkDetect && gameObject.tag == "E1"))
            {
                    PlayAnimation("Attacking", 0, 0);
                    rigid.velocity = new Vector2(0, rigid.velocity.y);              
            }
            else if (delayTime <= 0)
                PlayAnimation(rigid.velocity.x != 0 ? "Walking" :  "Idle", 0, 0.5f);
        }
        animator.SetFloat("V",V);
        Detect(c_position, Slime);
        CheckDie();
    }

    void Wallking(float v, int m)
    {
            switch (state)
            {
                case State.MoveLeft:
                    rigid.velocity = new Vector2(-v, rigid.velocity.y);
                    if (transform.position.x < x - m && !checkDetect)
                    {
                        state = State.Stand;
                        current_time = state_time;
                    }
                    break;
                case State.Stand:
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                    if (current_time > 0)
                    {
                        current_time -= Time.deltaTime;
                    }
                    else if (checkDetect)
                    {
                        if (transform.position.x > x)
                            state = State.MoveRight;
                        else
                            state = State.MoveLeft;
                    }
                    else
                    {
                        if (transform.position.x > x)
                        {
                            rigid.transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                            state = State.MoveLeft;
                        }

                        else
                        {
                            rigid.transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                            state = State.MoveRight;
                        }
                    }
                    break;
                case State.MoveRight:
                    rigid.velocity = new Vector2(v, rigid.velocity.y);
                    if (transform.position.x > x && !checkDetect)
                    {
                        state = State.Stand;
                        current_time = state_time;

                    }
                    break;
            }
    }

    Vector2 hit_range = new Vector2(5, 0.8f);
    public bool checkDetect;
    public void Detect(Vector3 c_position, GameObject SLime)
    {
        if (c_position.y - rigid.transform.position.y < hit_range.y && SLime.activeSelf)
        {
            switch (state)
            {
                case State.MoveLeft:
                    if (rigid.transform.position.x - c_position.x >= 0 && rigid.transform.position.x - c_position.x < hit_range.x)
                    {
                        checkDetect = true;
                        current_time = 0;
                    }
                    else
                        checkDetect = false;
                    break;
                case State.MoveRight:
                    if (c_position.x - rigid.transform.position.x >= 0 && c_position.x - rigid.transform.position.x < hit_range.x)
                    {
                        checkDetect = true;
                        current_time = 0;
                    }
                    else
                        checkDetect = false;
                    break;
                case State.Stand:
                    if (rigid.transform.position.x - c_position.x >= 0  && rigid.transform.position.x - c_position.x < hit_range.x && transform.position.x <= x-3)
                    {
                        checkDetect = true;
                        current_time = 0;
                    }
                    else if (c_position.x - rigid.transform.position.x >= 0 && c_position.x - rigid.transform.position.x < hit_range.x && transform.position.x >= x)
                    {
                        checkDetect = true;
                        current_time = 0;
                    }
                    else
                        checkDetect = false;
                    break;
            }           
        }
        else
            checkDetect = false;
        //Debug.Log(c_position.y - rigid.transform.position.y);
    }
    public void Bonus()
    {
        float posi = 0;
        int max = Random.Range(1, 5);
        GameObject[] c = new GameObject[max];
        for (int i=0; i<max; i++)
        {
            posi += 0.3f;
            c[i] = Instantiate(coin, null);
            c[i].transform.position = bonus.transform.position + new Vector3(posi, 0, 0);
        }
    }
    public void CheckDie()
    {
        if (HP <= 0 && timeAni<=0)
        {
            Bonus();
            Destroy(gameObject);
            isLive = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 12)
        {
            HP -= CharacterControl.instance.ATK;
            HealthBar.SetHealth(HP, maxHP);
            timeAni = 0.22f;
            if (CharacterControl.IDBullet == 1)
            {
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                if(HP > 0)
                {
                    PlayAnimation("Stand", 0, 0.5f);
                    delayTime = 1.5f;
                }
                else
                    delayTime = 0;
            }
            if (CharacterControl.IDBullet == 2)
            {
                if (HP > 0)
                {
                    slowTime = 2.5f;
                    V = 0.25f;
                }
                else
                    slowTime = 0;
                
            }
        }
    }
    string anim_name;
    private void PlayAnimation(string name, int fram_delay, float transition_time)
    {
        if (name != anim_name)
        {
            animator.Play(name, fram_delay, transition_time);
            anim_name = name;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public static CharacterControl instance;
    public static Rigidbody2D rigid;
    public int IDSlime;
    public float V_Run;
    public Animator animator;
    public bool run;
    Collider2D Collider;
    public GameObject Bullet;
    public GameObject BulletSpawn;
    public float curHealth;
    public float maxHealth = 100;
    public HealthBar healthBar;
    public float mana;
    public HealthBar manaBar;
    public int coin;
    public GameObject shield;
    public float checkShield;
    public float delayPower;
    public float ATK = 14;
    public static int IDBullet = 0;
    public GameObject Choose;
    Collider2D colli;
    //public SpriteRenderer shieldPosition;
    // Start is called before the first frame update
    public void Init()
    {
        instance = this;
        IDSlime = DataStore.instance.playerModel.IDSlime;
        curHealth = DataStore.instance.playerModel.HP;
        coin = DataStore.instance.playerModel.coin + 10;
        mana = DataStore.instance.playerModel.mana;
        gameObject.transform.position = DataStore.instance.playerModel.location;
        Application.targetFrameRate = 60;
        Time.fixedDeltaTime = 1f / 60f;
        rigid = GetComponent<Rigidbody2D>();
    }

    public void UpDateAnimator(GameObject Slime)
    {
        animator = Slime.GetComponent<Animator>();   
    }
    Animator ani;
    public void CheckCoin()
    {
        if (ani != animator)
        {
            coin -= 10;
            ani = animator;
        }
    }
    // Update is called once per frame
    public void UpDate()
    {
        
    }
    public bool isGrounded;
    public bool isJump;
    public bool shooting;
    public void FixedUpdateSlime()
    {
        if (!checkDie)
            return;
        if (delayPower> 0)
        {
            delayPower -= Time.deltaTime;
            if (delayPower <= 0)
            {
                V_Run = 2;
                IDBullet = 0;
                ATK = 14;
            }
                
        }
       
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, Layers.CanJump);
        isGrounded = hit.collider != null;
        float x = Input.GetAxis("Horizontal");
        float gound_vec = 0;
        if (isGrounded)
        {
            if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                gound_vec = hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (!shooting)
            {
                shooting = true;
                InvokeRepeating(nameof(Fire), 0.1f, 1f);
            }
        }
        else
        {
            CancelInvoke();
            shooting = false;
        }
        if (isGrounded)
        {
            rigid.velocity = new Vector2(x * V_Run + gound_vec, rigid.velocity.y);
            transform.eulerAngles = new Vector3(0, x > 0 ? 0 : (x < 0 ? 180 : transform.eulerAngles.y), 0);
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (!isJump)
                {
                    isJump = true;
                    Jump();
                }
            }
            else
            {
                isJump = false;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (hit.collider.tag== "Platform")
                {
                    colli = hit.collider;
                    Physics2D.IgnoreCollision(hit.collider, GetComponent<CapsuleCollider2D>(), true);
                }
            }
            if (colli != null && hit.collider != colli)
            {
                Physics2D.IgnoreCollision(colli, GetComponent<CapsuleCollider2D>(), false);
            }
            if (Mathf.Abs(x) > 0)
            {
                PlayAnimation(isJump ? "Jump" : "Run", 0, 0.5f);
            }
            else
            {
                PlayAnimation(isJump ? "Jump" : "Stand", 0, 0.5f);
            }

        }
        Die();
        CallPower();
        if (Choose.activeSelf == false)
        {
            CheckCoin();
        }

    }
    
    public static class Layers
    {
        public const int Ground = 1 << 6;
        public const int Enemy = 1 << 7;
        public const int Character = 1 << 8;
        public const int Stress = 1 << 9;
        public const int CanJump = Stress;
        public const int AllLayers = Ground | Enemy | Character | Stress;
    }
    public float jump_force;
    void Jump()
    {
        rigid.AddForce(transform.rotation.y == 1 ? new Vector2(-100f, 0.5f * jump_force)  : new Vector2(100f, 0.5f * jump_force) );
    }

    public bool checkDie = true;
    public void Die()
    {
        if (curHealth <= 0)
        {
            gameObject.SetActive(false);
            checkDie = false;
        }
    }
    void Fire()
    {
        Vector2 screen_position = Input.mousePosition;
        Vector3 world_position = Camera.main.ScreenToWorldPoint(new Vector3(screen_position.x, screen_position.y, -Camera.main.transform.position.z));
        if (Input.GetMouseButton(0))
        {
            Vector3 diection = world_position - BulletSpawn.transform.position;
            float angle_0 = Vector3.SignedAngle(Vector3.up, diection, Vector3.forward);
            float angle_1 = Mathf.LerpAngle(BulletSpawn.transform.eulerAngles.z, angle_0, Time.deltaTime * 60);
            transform.eulerAngles = new Vector3(0, (angle_0 < 126 && angle_0 > 0) ? -180 : 0, 0);
            BulletSpawn.transform.eulerAngles = new Vector3(0, 0, angle_1);
        }
        GameObject g = GameObject.Instantiate(Bullet, null);
        Shoot b = g.GetComponent<Shoot>();
        b.Init(BulletSpawn.transform.position, BulletSpawn.transform.eulerAngles, 7, 0.75f);
    }
    private string anim_name;
    private void PlayAnimation(string name, int fram_delay, float transition_time)
    {
        if (name != anim_name)
        {
            animator.Play(name, fram_delay, transition_time);
            anim_name = name;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 10)
        {
            Destroy(col.gameObject);
            if (mana<75)
                mana++;
            manaBar.SetHealth(mana);
        }
        //if (col.gameObject.layer == 11)
        //{
        //    checkShield-=5;
        //    Trap();
        //}
        if (col.gameObject.layer==14 && col.gameObject.tag == "E1")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.layer == 14)
        {
            if (checkShield > 0)
                checkShield -= 3;
            else
            {
                shield.SetActive(false);
                curHealth -= (Random.Range(5678, 9876) / 1000f);
                healthBar.SetHealth(curHealth);
            }

        }

        if (col.gameObject.layer == 13)
        {
            Destroy(col.gameObject);
            coin++;
        }

        if (col.gameObject.name== "Ground die")
        {
            curHealth = 0;
            healthBar.SetHealth(curHealth);
        }
    }

    public bool checkAnemo;
    private void CallPower()
    {
        if (animator.name!="Gio" && mana>=25)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (animator.name == "nuoc")
                {
                    Power.Heal(ref curHealth);
                    healthBar.SetHealth(curHealth);
                }    
                if (animator.name == "Tho")
                    Power.Shield(shield, ref checkShield, 21);
                if (animator.name == "Bang")
                {
                    Power.Shield(shield, ref checkShield, 12);
                    IDBullet = 2;
                    delayPower = 10;
                }
                    
                if (animator.name == "dien")
                {
                    Power.Electro(ref V_Run, ref IDBullet);
                    delayPower = 8;
                }   
                if (animator.name == "lua")
                {
                    Power.ATK(ref ATK);
                    delayPower = 8;
                }
                mana -= 25;
                manaBar.SetHealth(mana);
            }
        }
        else if (animator.name == "Gio")
        {
            if (Input.GetKey(KeyCode.W) && mana >= 0.02)
            {
                Power.Anemo(rigid, 0.75f);
                mana -= 0.045f;
                manaBar.SetHealth(mana);
                checkAnemo = true;
            }
            else if (checkAnemo)
            {
                Power.Anemo(rigid, -0.5f);
                if (isGrounded)
                    checkAnemo = false;
            }
        }
        
    }

    //void Trap()
    //{
    //    if (checkShield <= 0)
    //    {
    //        curHealth -= 10;
    //        shield.SetActive(false);
    //    }
            
    //}
}

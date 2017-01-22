using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatScript : MonoBehaviour 
{
    GameObject self;
    public MouseScript _mouse;
    Transform playerPOS;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    public int maxHealth = 65;
    public int normalDamage = 7;
    public int rangeDamage = 3;
    [HideInInspector]
    public int playerDamage = 3;
    //[HideInInspector]
    public int lightDamage = 200;
    public int attackSpeed = 5;
    public int defense;
    public int armor;
    public float dexterity; // chance of hitting
    public float meleeRange = 0.8f;
    private float meleeAdjustment = 0.5f;
    public int health;
    [Range(0.03f, 0.08f)]
    public float criticalChance;
    [Range(2, 5)]
    public int criticalDamage;
    private float chargeMultiplier = 100.0f;
    [HideInInspector]
    public float chargeDistance;
    public bool melee = true;
    Rigidbody2D projectile;
    
    [HideInInspector]
    public Transform target;
    GameObject smokeChild;
    [HideInInspector]
    public float chargeShot;
    Color32 startColor;
    Color32 endColor;
    Image energyBar;
    GameObject energy;
    
    //***calculators**
    float calculator;
    float calculator2;
    float calculator3;
    float calculator4;
    //[HideInInspector]
    public int spells = 0;


    [HideInInspector]
    public int splash;
    [HideInInspector]
    public float attackRate;  //rate of attack

    //**AUDIOS SOURCES**
    [HideInInspector]
    public AudioSource au_bow1;
    [HideInInspector]
    public AudioSource au_arrow1;
    [HideInInspector]
    public AudioSource au_arrow2;
    [HideInInspector]
    public AudioSource au_swing1;
    [HideInInspector]
    public AudioSource au_flame1;
    [HideInInspector]
    public AudioSource au_flame2;
    [HideInInspector]
    public AudioSource au_heal;
    [HideInInspector]
    public AudioSource au_light;


    ////----------EXP--------
    //[HideInInspector]
    //public float exp;
    //public int playerLevel = 1;
    //public float maxExp = 0f;


    void Awake()
    {
        self = this.gameObject;
        playerPOS = self.transform;
        health = maxHealth;
    }

    // Use this for initialization
    void Start()
    {
        Transform melee = transform.GetChild(0);
        up = melee.GetChild(0).gameObject;
        down = melee.GetChild(1).gameObject;
        left = melee.GetChild(2).gameObject;
        right = melee.GetChild(3).gameObject;

        smokeChild = GameObject.FindGameObjectWithTag("Smoke");

        //============Grabs all the Visual Componenets from Combat Visuals========
        CombatVisuals visuals = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatVisuals>();

        projectile = visuals.projectile;
        startColor = visuals.startColor;
        endColor = visuals.endColor;
        energyBar = visuals.energyBar;
        energy = visuals.energy;

        //setting the scale of objects to range of melee weapon
        up.transform.localScale = new Vector3(0, meleeRange, 0);
        up.transform.localPosition = new Vector3(0, meleeAdjustment, 0);
        down.transform.localScale = new Vector3(0, -meleeRange, 0);
        down.transform.localPosition = new Vector3(0, -meleeAdjustment, 0);
        left.transform.localScale = new Vector3(-meleeRange, 0, 0);
        left.transform.localPosition = new Vector3(-meleeAdjustment, 0, 0);
        right.transform.localScale = new Vector3(meleeRange, 0, 0);
        right.transform.localPosition = new Vector3(meleeAdjustment, 0, 0);

        //=================All of Chris' Dumb Audio Stuff=====================
        au_bow1 = gameObject.AddComponent<AudioSource>();
        AudioClip bow1;

        // Resources must be in any folder named Resources.  load as type and cast as type because Unity returns Object by default.
        bow1 = (AudioClip)Resources.Load("Audio/Combat Sounds/Bow", typeof(AudioClip));
        au_bow1.clip = bow1;

        au_arrow1 = gameObject.AddComponent<AudioSource>();
        AudioClip arrow1;

        // Resources must be in any folder named Resources.  load as type and cast as type because Unity returns Object by default.
        arrow1 = (AudioClip)Resources.Load("Audio/Combat Sounds/Arrow 6", typeof(AudioClip));
        au_arrow1.clip = arrow1;

        au_arrow2 = gameObject.AddComponent<AudioSource>();
        AudioClip arrow2;

        // Resources must be in any folder named Resources.  load as type and cast as type because Unity returns Object by default.
        arrow2 = (AudioClip)Resources.Load("Audio/Combat Sounds/Arrow 7", typeof(AudioClip));
        au_arrow2.clip = arrow2;

        au_swing1 = gameObject.AddComponent<AudioSource>();
        AudioClip swing1;

        // Resources must be in any folder named Resources.  load as type and cast as type because Unity returns Object by default.
        swing1 = (AudioClip)Resources.Load("Audio/Combat Sounds/Sword Swish 1", typeof(AudioClip));
        au_swing1.clip = swing1;

        au_flame1 = gameObject.AddComponent<AudioSource>();
        AudioClip flame1;

        flame1 = (AudioClip)Resources.Load("Audio/Spells/magicFlamethrowerSoundEffect", typeof(AudioClip));
        au_flame1.clip = flame1;

        au_flame2 = gameObject.AddComponent<AudioSource>();
        AudioClip flame2;

        flame2 = (AudioClip)Resources.Load("Audio/Spells/magicFlamethrowerSoundEffectTail", typeof(AudioClip));
        au_flame2.clip = flame2;

        au_heal = gameObject.AddComponent<AudioSource>();
        AudioClip heal;

        heal = (AudioClip)Resources.Load("Audio/Spells/magicHealingSpellSoundEffect", typeof(AudioClip));
        au_heal.clip = heal;

        au_light = gameObject.AddComponent<AudioSource>();
        AudioClip light;

        light = (AudioClip)Resources.Load("Audio/Spells/magicLightningSoundEffect", typeof(AudioClip));
        au_light.clip = light;
    }

    // Update is called once per frame
    void Update()
    {
        //switching from melee to range
        if (Input.GetKeyUp(KeyCode.Q))
        {
            melee = !melee;
        }

        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);
            Application.LoadLevel("GameOverScreen");
            //pay animation
            //pay sound
        }
        if (health > maxHealth)
            health = maxHealth;

        if (Input.GetMouseButtonDown(0) && attackRate == 0) //left click
        {
            if (melee == true)
            {
                //attack whie sprint is not active (normal attack)
                if (!self.GetComponent<PlayerMovement>().isSprinting)
                {
                    playerDamage = normalDamage;
                    splash = 1;
                    attackRate = 5;

                    //the value of this variable,splash, determines how many foes can be hit within a single attack
                    //since it's currently set to 1, only one foe can be hit at a time.
                    //certain spells, such as a multi attack, will require this variable to increase.


                    au_swing1.Play();
                    //attack animation
                }
            }
        }

        //charging the bow
        if (Input.GetMouseButton(0) && melee == false && attackRate == 0)
        {
            self.GetComponent<PlayerMovement>().moveSpeed = 0;
            //self.GetComponent<PlayerMovement>().moveY = 0;
            if (chargeShot <= 0)
            {
                au_bow1.Play();
            }


            energy.SetActive(true);
            if (chargeShot < 1.0f)
                chargeShot += 1 * Time.deltaTime;
            if (chargeShot > 1.0f)
                chargeShot = 1.0f;
            playerDamage = (int) (rangeDamage * chargeShot * 2);
            calculator = chargeShot / 1.0f;
            SetEnergy(calculator);
            //print ("charge is... " + chargeShot);
        }

        //using arrows
        if (Input.GetMouseButtonUp(0) && melee == false && attackRate == 0)
        {
            au_bow1.Stop();
            au_arrow1.Play();
            au_arrow2.Play();

            energy.SetActive(false);
            attackRate = chargeShot * 3;
            if (attackRate <= 3)
                attackRate = 3;

            if (!target)
                target = GameObject.FindWithTag("Mouse").transform;

            Vector3 v3Pos;
            float fAngle;

            v3Pos = Camera.main.WorldToScreenPoint(target.transform.position);
            v3Pos = Input.mousePosition - v3Pos;
            fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
            if (fAngle < 0.0f) fAngle += 360.0f;

            //instantiate projectile
            Rigidbody2D clone;
            clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody2D;

            //detecting arrow direction with the variable direction
            if (fAngle <= 135.0F && fAngle > 45.0F)
            {
                //print ("up");
                clone.velocity = (GameObject.Find("Mouse").transform.position - transform.position).normalized * Random.Range(15, 20);
                down.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
                up.SetActive(true);
            }

            if (fAngle <= 45.0F || fAngle > 315.0F)
            {
                //print ("right");
                clone.velocity = (GameObject.Find("Mouse").transform.position - transform.position).normalized * Random.Range(15, 20);
                up.SetActive(false);
                down.SetActive(false);
                left.SetActive(false);
                right.SetActive(true);
            }

            if (fAngle <= 225.0F && fAngle > 135.0F)
            {
                //print ("left");
                clone.velocity = (GameObject.Find("Mouse").transform.position - transform.position).normalized * Random.Range(15, 20);
                up.SetActive(false);
                down.SetActive(false);
                right.SetActive(false);
                left.SetActive(true);
            }

            if (fAngle <= 315.0F && fAngle > 225.0F)
            {
                //print ("down");
                clone.velocity = (GameObject.Find("Mouse").transform.position - transform.position).normalized * Random.Range(15, 20);
                up.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
                down.SetActive(true);
            }
        }

        //attack speed
        if (attackRate > 0)
        {
            attackRate -= attackSpeed * Time.deltaTime;
            //prevents moving during attack
            self.GetComponent<PlayerMovement>().moveSpeed = 0;
            //self.GetComponent<PlayerMovement>().moveY = 0;
        }

        //charge recovery
        if (chargeDistance > 1)
            chargeDistance -= chargeDistance * Time.deltaTime;

        if (chargeDistance > 0 && chargeDistance <= 1)
        {
            attackRate = 10;
            chargeDistance = 0;
        }

        //**directional combat**

        //facing right
        if (self.GetComponent<PlayerMovement>().moveRight)
        {
            up.SetActive(false);
            down.SetActive(false);
            left.SetActive(false);
            right.SetActive(true);

        }

        //facing left
        if (self.GetComponent<PlayerMovement>().moveLeft)
        {
            up.SetActive(false);
            down.SetActive(false);
            left.SetActive(true);
            right.SetActive(false);

        }

        //facing up
        if (self.GetComponent<PlayerMovement>().moveUp)
        {
            up.SetActive(true);
            down.SetActive(false);
            left.SetActive(false);
            right.SetActive(false);

        }

        //facing down
        if (self.GetComponent<PlayerMovement>().moveDown)
        {
            up.SetActive(false);
            down.SetActive(true);
            left.SetActive(false);
            right.SetActive(false);
        }
    }

    //de-activating smokeChild

    void OnTriggerStay2D(Collider2D target)
    {
        if (target.gameObject.tag == "Smoke" && chargeDistance <= 0)
        {
            smokeChild.SetActive(false);
        }
    }
    //charge shot calculations

    public void SetEnergy(float myEnergy)
    {
        energyBar.transform.localScale = new Vector3(myEnergy, energyBar.transform.localScale.y, energyBar.transform.localScale.z);
    }
}
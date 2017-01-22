using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CombatSpells : MonoBehaviour {

    public MouseScript _mouse;

    List<ProjectileSpell> proSpells = new List<ProjectileSpell>();

    //---------Directions----------
    GameObject up;
    GameObject down;
    GameObject left;
    GameObject right;

    AudioSource spellSounds;
    Transform target;

    //========Projectile============
    public float fireDamage = 0.02f;
    public Rigidbody2D flamePrefab;
    bool canFire;
    bool isGrabbed;
    AudioClip projectileSFX;

    //=========Burst============
    Transform restorationPrefab;
    public AudioClip burst;

    //========Buffs============
    GameObject shieldChild;
    public int shield;
    public int healthRestore = 25;
    public AudioClip buffClip;

    //=======idNums========
    int projectileIdNum = -1;

    //========MaxCoolDowns=====
    int maxShieldCoolDown;
    int maxRestoreCoolDown;
    int maxProjectileCoolDown;
    int maxLightCoolDown;

    //========CurrCoolDown====
    [HideInInspector]
    public float currProjCoolDown;

    //**Spell Timers**
    float shieldTimer;
    float restoreTimer;
    float projectileTimer;

    //**image cooldowns**
    Image CoolDownImageShield;
    Image CoolDownImageRestore;
    Image CoolDownImageFire;
    Image CoolDownImageLight;

    CombatScript stats;

    void Start()
    {
        stats = GetComponent<CombatScript>();

        up = stats.up;
        down = stats.down;
        left = stats.left;
        right = stats.right;

        spellSounds = gameObject.AddComponent<AudioSource>();

        proSpells = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpellCreator>().proSpells;

        //maxShieldCoolDown = shieldCoolDown;
        //maxFireCoolDown = fireCoolDown;
        //maxLightCoolDown = lightCoolDown;
        //maxRestoreCoolDown = restoreCoolDown;
    }


	// Update is called once per frame
	void Update ()
    {
        foreach (ProjectileSpell spell in proSpells)
        {
            Image cooldown = spell.UI.transform.FindChild("Border").FindChild("Cooldown").GetComponent<Image>();
            if (!spell.isUse && spell.currCooldown >= 0)
            {
                spell.currCooldown -= (int)(40 * Time.deltaTime);
                CoolDown(spell.currCooldown / spell.cooldown, cooldown);
            }
        }
    }

    public void projectileSpell(int idNum)
    {
        if (idNum != projectileIdNum)
        {
            maxProjectileCoolDown = (int) proSpells[idNum].cooldown;
            currProjCoolDown = (int)proSpells[idNum].currCooldown;
            projectileSFX = proSpells[idNum].sfx;
            flamePrefab = proSpells[idNum].obj;
            CoolDownImageFire = proSpells[idNum].UI.transform.FindChild("Border").FindChild("Cooldown").GetComponent<Image>();
            projectileIdNum = idNum;
        }
        /*
                if (!canFire)
                {
                    if (currProjCoolDown < (maxProjectileCoolDown / 2))
                        canFire = true;
                    else
                    {
                        currProjCoolDown -= (int)(40 * Time.deltaTime);
                        CoolDown(currProjCoolDown / maxProjectileCoolDown, CoolDownImageFire);
                    }

                    return;
                }  
        */

        if (Input.GetMouseButton(1))  //right click
        {
            if (currProjCoolDown < maxProjectileCoolDown)
            {
                currProjCoolDown += (int)(80 * Time.deltaTime);
                CoolDown(currProjCoolDown / maxProjectileCoolDown, CoolDownImageFire);

                //prevent player from moving
                GetComponent<PlayerMovement>().moveSpeed = 0;

                if (!target)
                    target = GameObject.FindWithTag("Mouse").transform;

                Vector3 v3Pos;
                float fAngle;

                v3Pos = Camera.main.WorldToScreenPoint(target.transform.position);
                v3Pos = Input.mousePosition - v3Pos;
                fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;

                Rigidbody2D clone;
                clone = Instantiate(flamePrefab, transform.position, transform.rotation) as Rigidbody2D;

                if (fAngle < 0.0f)
                    fAngle += 360.0f;

                //playing the sound effect
                if (!spellSounds.isPlaying)
                {
                    spellSounds.clip = projectileSFX;
                    spellSounds.Play();
                }

                //flame goes in the direction of the mouse
                if (fAngle <= 135.0F && fAngle > 45.0F)
                {
                    //print ("up");
                    clone.velocity = (GameObject.Find("Mouse").transform.position - transform.position).normalized * Random.Range(7, 10);
                    down.SetActive(false);
                    left.SetActive(false);
                    right.SetActive(false);
                    up.SetActive(true);
                }

                if (fAngle <= 45.0F || fAngle > 315.0F)
                {
                    //print ("right");
                    clone.velocity = (GameObject.Find("Mouse").transform.position - transform.position).normalized * Random.Range(7, 10);
                    up.SetActive(false);
                    down.SetActive(false);
                    left.SetActive(false);
                    right.SetActive(true);
                }

                if (fAngle <= 225.0F && fAngle > 135.0F)
                {
                    //print ("left");
                    clone.velocity = (GameObject.Find("Mouse").transform.position - transform.position).normalized * Random.Range(7, 10);
                    up.SetActive(false);
                    down.SetActive(false);
                    right.SetActive(false);
                    left.SetActive(true);
                }

                if (fAngle <= 315.0F && fAngle > 225.0F)
                {
                    //print ("down");
                    clone.velocity = (GameObject.Find("Mouse").transform.position - transform.position).normalized * Random.Range(7, 10);
                    up.SetActive(false);
                    left.SetActive(false);
                    right.SetActive(false);
                    down.SetActive(true);
                }
            }
            else
            {
                //over the max limit
            }
        }

        if (!Input.GetMouseButton(1) && spellSounds.isPlaying)
        {
            spellSounds.Stop();
        }
    }

    public void healSpell(int healCoolDown, AudioClip healSound)
    {
        //*******MAGIC SPELLS***********        
        //Restoration spell (Revivify)
        if (Input.GetMouseButtonDown(1) && healCoolDown <= 0) //right click
        {
            spellSounds.clip = buffClip;
            spellSounds.Play();
            Rigidbody2D clone;
            clone = Instantiate(restorationPrefab, transform.position, transform.rotation) as Rigidbody2D;
            int health = stats.health;
            int maxHealth = stats.maxHealth;
            health += healthRestore;
            if (health > maxHealth)
                health = maxHealth;
            restoreTimer = 3;
            healCoolDown = maxRestoreCoolDown;
        }

        if (restoreTimer > 0)
        {
            restoreTimer -= 1 * Time.deltaTime;
            //prevent player from moving
            GetComponent<PlayerMovement>().moveSpeed = 0;
            //self.GetComponent<PlayerMovement>().moveY = 0;
        }
        if (healCoolDown > 0)
        {
            healCoolDown -= (int) (1 * Time.deltaTime);
            //CoolDownRestore(healCoolDown / 10);
        }
        if (healCoolDown < 0)
            healCoolDown = 0;
    }

    public void shieldSpell(int shieldCoolDown, AudioClip shieldSound)
    {
        //Cold Spell (StormShield)
        if (Input.GetMouseButton(1) && shieldCoolDown <= 0) //right click
        {
            shieldChild.SetActive(true);
            shieldCoolDown = maxShieldCoolDown;
            shieldTimer = 18;

            int defense = stats.defense;
            defense += shield;

            //prevent player from moving
            GetComponent<PlayerMovement>().moveSpeed = 0;
        }
        //turning shield off
        if (shieldChild.activeSelf && shieldTimer != 0 && shieldTimer < 1)
        {
            int defense = stats.defense;

            shieldTimer = 0;
            shieldChild.SetActive(false);
            defense -= shield;
        }
        //shield timer
        if (shieldTimer >= 1)
            shieldTimer -= 1 * Time.deltaTime;

        if (shieldCoolDown > 0)
        {
            //CoolDownShield(shieldCoolDown / 50);
            shieldCoolDown -= (int) (1 * Time.deltaTime);
        }
        if (shieldCoolDown < 0)
            shieldCoolDown = 0;
    }

    public void burstSpell(int burstCoolDown, AudioClip burstSound)
    {
        if (Input.GetMouseButtonDown(1)  && burstCoolDown <= 0)  //right click
        {
            _mouse.Lightning();
            burstCoolDown = 80;
            spellSounds.clip = burst;
            spellSounds.Play();
        }

        if (burstCoolDown > 0)
        {
            //CoolDownLight(burstCoolDown / 80);
            burstCoolDown -= (int) (1 * Time.deltaTime);
        }
        else
            burstCoolDown = 0;
    }

    
    //fire cooldown calculations
    public void CoolDown(float coolValue, Image coolImage)
    {
        coolImage.transform.localScale = new Vector3(coolImage.transform.localScale.x, coolValue, coolImage.transform.localScale.z);
    }

    /*
    //cooldown for restoration
    public void CoolDownRestore(float Restorex)
    {
        CoolDownImageRestore.transform.localScale = new Vector3(CoolDownImageRestore.transform.localScale.x, Restorex, CoolDownImageRestore.transform.localScale.z);
    }
    //cooldown for shield
    public void CoolDownShield(float Sheildx)
    {
        CoolDownImageShield.transform.localScale = new Vector3(CoolDownImageShield.transform.localScale.x, Sheildx, CoolDownImageShield.transform.localScale.z);
    }
    //cooldown for shield
    public void CoolDownLight(float Lightx)
    {
        CoolDownImageLight.transform.localScale = new Vector3(CoolDownImageLight.transform.localScale.x, Lightx, CoolDownImageLight.transform.localScale.z);
    }
    */
}

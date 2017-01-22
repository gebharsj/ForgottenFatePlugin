using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillSelector : MonoBehaviour {

    GameObject player;
	public GameObject whiteBox1, whiteBox2, whiteBox3, whiteBox4;
    public bool oneUnlocked, twoUnlocked, threeUnlocked, fourUnlocked = false;
    bool isGrabbed;

    List<ProjectileSpell> proSpells = new List<ProjectileSpell>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        proSpells = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpellCreator>().proSpells;
    }

    void Update()
    {
        if (Input.GetKey(proSpells[0].key) && oneUnlocked)
        {
            player.GetComponent<CombatSpells>().projectileSpell(0);
            proSpells[0].isUse = true;
            proSpells[0].UI.transform.FindChild("Selected").gameObject.SetActive(true);
            isGrabbed = false;
        }
        else
        {
            if (!isGrabbed)
            {
                print("Grabs: " + player.GetComponent<CombatSpells>().currProjCoolDown + " and makes it equal to " + proSpells[0].currCooldown);
                proSpells[0].currCooldown = player.GetComponent<CombatSpells>().currProjCoolDown;
                proSpells[0].isUse = false;
                proSpells[0].UI.transform.FindChild("Selected").gameObject.SetActive(false);
                isGrabbed = true;
            }
        }

        if (Input.GetKey("2") && twoUnlocked)
        {
            player.GetComponent<CombatScript>().spells = 1;
            whiteBox1.SetActive(false);
            whiteBox2.SetActive(true);
            whiteBox3.SetActive(false);
            whiteBox4.SetActive(false);
        }

        if (Input.GetKey("3") && threeUnlocked)
        {
            player.GetComponent<CombatScript>().spells = 2;
            whiteBox1.SetActive(false);
            whiteBox2.SetActive(false);
            whiteBox3.SetActive(true);
            whiteBox4.SetActive(false);
        }

        if (Input.GetKey("4") && fourUnlocked)
        {
            player.GetComponent<CombatScript>().spells = 3;
            whiteBox1.SetActive(false);
            whiteBox2.SetActive(false);
            whiteBox3.SetActive(false);
            whiteBox4.SetActive(true);
        }
    }

    //IEnumerator Wait() {
    //       yield return new WaitForSeconds(3);
    //       whiteBox1.SetActive(false);
    //       whiteBox2.SetActive(false);
    //       whiteBox3.SetActive(false);
    //       whiteBox4.SetActive(false);
    //}
}
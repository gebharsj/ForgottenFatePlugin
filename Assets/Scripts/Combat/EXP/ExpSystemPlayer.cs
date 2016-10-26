using UnityEngine;
using System.Collections;

public class ExpSystemPlayer : MonoBehaviour
{
    public bool random;

    //==========Set Vars=======
    public int healthGained;
    public int staminaGained;
    public int attackGained;
    public int defenseGained;

    //========Random Vars=======
    public int healthGainedMin, healthGainedMax;
    public int staminaGainedMin, staminaGainedMax;
    public int attackGainedMin, attackGainedMax;
    public int defenseGainedMin, defenseGainedMax;

    [HideInInspector]
    public float exp;
    [HideInInspector]
    public int playerLevel = 1;
    [HideInInspector]
    public float maxExp = 100f;

    GameObject _player;
    CombatScript stats;
    PlayerMovement movement;

    // Use this for initialization
    void Start()
    {
        maxExp = 100 * playerLevel;
        _player = this.gameObject;
        stats = _player.GetComponent<CombatScript>();
        movement = _player.GetComponent<PlayerMovement>();
    }

	public void CalcExp(int enemyLvl)
	{
		exp += (enemyLvl * 10);
		
		if (exp >= maxExp)
		{
			playerLevel++;
			exp = exp - maxExp;

            if (random)
                levelUpRandom();
            else
                levelUpFixed();

            maxExp = 100 * playerLevel;
        }
	}

    void levelUpFixed()
    {
        stats.maxHealth += healthGained;
        movement.maxStamina += staminaGained;
        stats.normalDamage += attackGained;
        stats.defense += defenseGained;
    }

    void levelUpRandom()
    {
        stats.maxHealth += Random.Range(healthGainedMin, (healthGainedMax + 1));
        movement.maxStamina += Random.Range(staminaGainedMin, (staminaGainedMax + 1));
        stats.normalDamage += Random.Range(attackGainedMin, (attackGainedMax + 1));
        stats.defense += Random.Range(defenseGainedMin, (defenseGainedMax + 1));
    }
}
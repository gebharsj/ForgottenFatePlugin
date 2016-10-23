using UnityEngine;
using System.Collections;

public class ExpSystemPlayer : MonoBehaviour
{

    [HideInInspector]
    public float exp;
    [HideInInspector]
    public int playerLevel = 1;
    [HideInInspector]
    public float maxExp = 100f;

    public GameObject _player;

    // Use this for initialization
    void Start()
    {
        maxExp = 100 * playerLevel;
        _player = this.gameObject;
    }

	public void CalcExp(int enemyLvl)
	{
		exp += (enemyLvl * 10);
		
		if (exp >= maxExp)
		{
			playerLevel++;
			exp = exp - maxExp;
			_player.GetComponent<CombatScript>().normalDamage++;

            maxExp = 100 * playerLevel;
        }
	}
}
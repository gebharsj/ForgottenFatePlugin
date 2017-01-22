using UnityEngine;
using System.Collections.Generic;

public class SpellViewer : MonoBehaviour {

    public List<ProjectileSpell> proSpells = new List<ProjectileSpell>();

    // Use this for initialization
    void Start ()
    {
        proSpells = this.gameObject.GetComponent<SpellCreator>().proSpells;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpellCreator : MonoBehaviour {

    public AudioClip sfx;
    public Image UiImage;
    public KeyCode key;
    public bool unlocked;
    public float cooldown;
    public float damage;
    public int healing;
    public Rigidbody2D obj;

    CombatVisuals visuals;

    public List<ProjectileSpell> proSpells = new List<ProjectileSpell>();
    public List<HealingSpell> healSpells = new List<HealingSpell>();

    public void CreateProjectileSpell()
    {
        ProjectileSpell newSpell = new ProjectileSpell(sfx, UiImage, key, unlocked, cooldown, damage, obj);
        proSpells.Add(newSpell);
        visuals = gameObject.GetComponent<CombatVisuals>();
        newSpell.UI = visuals.CreateSpellUI(UiImage);
        Debug.Log(proSpells[0].key);
    }

    public void CreateHealingSpell()
    {
        HealingSpell newSpell = new HealingSpell(sfx, UiImage, key, unlocked, cooldown, healing);
        healSpells.Add(newSpell);
        Debug.Log(healSpells[0].key);
    }

    public void ClearProjectileSpell()
    {
        proSpells.Clear();
    }

    public void ClearHealingSpell()
    {
        healSpells.Clear();
    }
}

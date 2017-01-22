using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class ProjectileSpell : BaseSpell {

    public float damage;
    public Rigidbody2D obj;
    public GameObject UI;

    public ProjectileSpell(AudioClip sfx, Image UI, KeyCode key, bool unlocked, float cooldown, float damage, Rigidbody2D obj)
    {
        this.sfx = sfx;
        this.UiImage = UI;
        this.key = key;
        this.unlocked = unlocked;
        this.cooldown = cooldown;
        this.damage = damage;
        this.obj = obj;
    }

    public override string ToString()
    {
        //string message = "SFX: " + sfx.name + "\n Image: " + UiImage.name + "\n Damage: " + damage + "\n KeyCode: " + key;
        string message = "SFX: " + sfx.name + "\n Damage: " + damage + "\n Button Mapped: " + key;
        return message;
    }
}


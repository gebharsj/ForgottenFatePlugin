using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]

public class HealingSpell : BaseSpell {

    public int healing;

    public HealingSpell(AudioClip sfx, Image UI, KeyCode key, bool unlocked, float cooldown, int healing)
    {
        this.sfx = sfx;
        this.UiImage = UI;
        this.key = key;
        this.unlocked = unlocked;
        this.cooldown = cooldown;
        this.healing = healing;
    }

    public override string ToString()
    {
        //string message = "SFX: " + sfx.name + "\n Image: " + UiImage.name + "\n Damage: " + damage + "\n KeyCode: " + key;
        string message = "SFX: " + sfx.name + "\n Healing: " + healing + "\n Button Mapped: " + key;
        return message;
    }
}

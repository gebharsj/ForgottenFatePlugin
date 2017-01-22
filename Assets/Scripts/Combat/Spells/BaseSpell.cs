using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class BaseSpell {

    public AudioClip sfx;
    public Image UiImage;
    public KeyCode key;
    public bool unlocked;
    public bool isUse;
    public float cooldown;
    public float currCooldown;
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatVisuals : MonoBehaviour {

    /*The purpose of this script is to allow easy adjustablity to the combat script
      without having to deal with the code. Seperates code and visual components
    */
    public Rigidbody2D projectile;
    public Rigidbody2D flamePrefab;
    public Color32 startColor;
    public Color32 endColor;
    public Image energyBar;
    public GameObject energy;
    public Transform restorationPrefab;
    public GameObject shieldChild;

    //**image cooldowns**
    public Image CoolDownImageShield;
    public Image CoolDownImageRestore;
    public Image CoolDownImageFire;
    public Image CoolDownImageLight;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

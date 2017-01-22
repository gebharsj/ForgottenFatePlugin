using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

    public GameObject baseUI;

    GameObject spellsUI;

    public List<GameObject> spellImages = new List<GameObject>();

    void OnEnable()
    {
    }

    public GameObject CreateSpellUI(Image UI)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        spellsUI = canvas.transform.FindChild("Spells").gameObject;
        GameObject newUI = (GameObject) Instantiate(baseUI);
        newUI.transform.parent = spellsUI.transform;
        newUI.transform.localPosition = Vector3.zero;
        newUI.transform.FindChild("Image").GetComponent<Image>().sprite = UI.sprite;

        return newUI;
    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

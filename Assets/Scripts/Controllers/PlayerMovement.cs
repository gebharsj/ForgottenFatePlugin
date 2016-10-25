using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D player;
    public float speed = 200.0f;
    public float sprint = 2;
    [HideInInspector]
    public bool isSprinting = false;
    public float stamina = 5;
    public int maxStamina = 5;
    public float staminaRecoveryRate = 0.3f;
    public int staminRechargeDelay = 5;
    private float staminaRechargeCnt;
    [HideInInspector]
    public float moveSpeed;

    private Vector3 targetPosition;
    private bool isMoving;

    [HideInInspector]
    public float moveX;
    [HideInInspector]
    public float moveY;

    [HideInInspector]
    public bool moveUp;
    [HideInInspector]
    public bool moveDown;
    [HideInInspector]
    public bool moveRight;
    [HideInInspector]
    public bool moveLeft;

    //const int LEFT_MOUSE_BUTTON = 0;
    void start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        staminaRechargeCnt = staminRechargeDelay;
        stamina = maxStamina;

        if (isSprinting == false)
        {
            moveSpeed = speed;
        }
    }

    // Character controller - the mouse will always override the keypad. Also, this control type does not
    // apply to X and Y cordinates but X and Z coordinates. To change, switch the "forward" function to 
    // "up" and the "back" function to "down." Rotation of camera may also affect the control. 

    void Update()
    {
        if (Input.GetKey(PlayerPrefs.GetString("MoveRight")))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            moveRight = true;
            moveLeft = false;
            moveUp = false;
            moveDown = false;
        }
        if (Input.GetKey(PlayerPrefs.GetString("MoveLeft")))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            moveRight = false;
            moveLeft = true;
            moveUp = false;
            moveDown = false;
        }
        if (Input.GetKey(PlayerPrefs.GetString("MoveUp")))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            moveRight = false;
            moveLeft = false;
            moveUp = true;
            moveDown = false;
        }
        if (Input.GetKey(PlayerPrefs.GetString("MoveDown")))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            moveRight = false;
            moveLeft = false;
            moveUp = false;
            moveDown = true;
        }

        //sprinting
        if (Input.GetKeyDown("space") && isSprinting == false && stamina > 0)
            isSprinting = true;

        if (Input.GetKeyUp("space") && isSprinting == true)
            isSprinting = false;

        if (stamina <= 0)
            isSprinting = false;


        // Only when left mouse button is not clicked, will the WSAD controls work.) 
        if (isSprinting == false)
        {
            moveSpeed = speed;
        }
        else
        {
            moveSpeed = speed * sprint;
            stamina -= 1 * Time.deltaTime;
            staminaRechargeCnt = 0;
        }

        //stamina must recharge before it can recover
        if (staminaRechargeCnt < staminRechargeDelay)
        {
            staminaRechargeCnt += 1 * Time.deltaTime;

            //if staminaRecharge should happen to go above 5
            if (staminaRechargeCnt > staminRechargeDelay)
                staminaRechargeCnt = staminRechargeDelay;
        }

        //stamina recovery
        if (isSprinting == false && stamina < maxStamina && staminaRechargeCnt == 5)
        {
            stamina += 1 * Time.deltaTime * staminaRecoveryRate;

            //if stamina should happen to go above the max value
            if (stamina > maxStamina)
                stamina = maxStamina;
        }
    }
}
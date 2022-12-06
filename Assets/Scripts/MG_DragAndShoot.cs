using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class MG_DragAndShoot : MonoBehaviour
{
    //Script to controll the player
    [SerializeField]private float maxPower;
    private float shootPower;
    private float gravity = 2;

    Transform direction;
    public Rigidbody2D rb;
    LineRenderer line;
    LineRenderer screenLine;

    // Vectors // 
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private Vector2 startMousePos;
    private Vector2 currentMousePos;
    private Vector2 constatntMousePos; 

    public bool canShoot;//Vairable to allow or not shooting

    public bool levelWon;//Variable to activate level win

    //Sound effects
    public AudioSource bounceSoundEffect;
    public AudioSource ThrowSoundEffect;

    void Start()
    {
        //Acces ball componenets
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
        direction = transform.GetChild(0);
        screenLine = direction.GetComponent<LineRenderer>();

        canShoot = true;//Make sure its true
        levelWon = false;//Make sure ots false
    }

    void Update()
    {   

        if (Input.GetMouseButtonDown(0))
        {
            MouseClick();
        }
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.currentSelectedGameObject) return;//Dont ignore UI
            MouseDrag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.currentSelectedGameObject) return;//Dont ignore UI
            MouseRelease();
        }
    }

    // MOUSE INPUTS
    void MouseClick()
    {
        if (canShoot)
        {
            Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.right = dir * 1;

            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }  
    }

    void MouseDrag()
    {
        if (canShoot)
        {
            LookAtShootDirection();
            DrawLine();
            DrawScreenLine();

            float distance = Vector2.Distance(currentMousePos, startMousePos);

            if (distance > 1)
            {
                line.enabled = true;
                screenLine.enabled = true;
            }
        }
    }

    void MouseRelease()
    {
        if (canShoot)
        {
            Shoot();
            screenLine.enabled = false;
            line.enabled = false;
            ThrowSoundEffect.Play();
        }    
    }

    // ACTIONS  
    void LookAtShootDirection()
    {
        Vector3 dir = startMousePos - currentMousePos;

        transform.right = dir * -1;
 
        float dis = Vector2.Distance(startMousePos, currentMousePos);
        dis *= 4;

        if (dis < maxPower)
        {
            direction.localPosition = new Vector2(dis / 6, 0);
            shootPower = dis;
        }
        else
        {
            shootPower = maxPower;
            direction.localPosition = new Vector2(maxPower / 6, 0);
        }
    }

    public void Shoot()//we shoot
    {
        canShoot = false;
        rb.velocity = transform.right * shootPower;
    }

    private void OnCollisionEnter2D(Collision2D other)//When the ball colides
    {
        if (!canShoot && rb.velocity.magnitude>5)//if we go slow sound
        {
            bounceSoundEffect.Play();
        }
        if (other.gameObject.CompareTag("Goal"))//if we hit the goal we win the level
        {
            levelWon = true;
        }
    }

    void DrawScreenLine()//Draw lines when the ball is moving
    {
        screenLine.positionCount = 1;
        screenLine.SetPosition(0, startMousePos);

        screenLine.positionCount = 2;
        screenLine.SetPosition(1, currentMousePos);
    }

    void DrawLine()//Draw lines wne charging shoot
    {
        startPosition = transform.position;

        line.positionCount = 1;
        line.SetPosition(0, startPosition);

        targetPosition = direction.transform.position;
        currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        line.positionCount = 2;
        line.SetPosition(1, targetPosition);
    }

    Vector3[] positions;
}

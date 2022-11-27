using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class DragAndShoot : MonoBehaviour
{
    [Header("Movement")]
    public float maxPower;
    public float shootPower;
    public float gravity = 1;

    Transform direction;
    public Rigidbody2D rb;
    LineRenderer line;
    LineRenderer screenLine;

    // Vectors // 
    Vector2 startPosition;
    Vector2 targetPosition;
    Vector2 startMousePos;
    Vector2 currentMousePos;

    public bool canShoot = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        line = GetComponent<LineRenderer>();
        direction = transform.GetChild(0);
        screenLine = direction.GetComponent<LineRenderer>();

        canShoot = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseClick();
        }
        if (Input.GetMouseButton(0))
        {
            MouseDrag();
        }
        if (Input.GetMouseButtonUp(0))
        {
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

    public void Shoot()
    {
        canShoot = false;
        rb.velocity = transform.right * shootPower;
    }

    void DrawScreenLine()
    {
        screenLine.positionCount = 1;
        screenLine.SetPosition(0, startMousePos);

        screenLine.positionCount = 2;
        screenLine.SetPosition(1, currentMousePos);
    }

    void DrawLine()
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

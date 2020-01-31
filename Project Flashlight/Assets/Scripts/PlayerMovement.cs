using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {


    PlayerControls controls;
    Vector2 move;
    Vector2 rotation;

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    public float rotateSpeed = 500f;
    Vector2 movement;
    Vector2 mousePos;


    Animator animator;
    bool isMoving;

    float playerAngle = 90f;
    public float stickThreshold = 0.65f;

    public Rigidbody2D rigidbody;


    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Move.performed += ctx => move= ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Rotate.performed += ctx => rotation = ctx.ReadValue<Vector2>();
        controls.Gameplay.Rotate.canceled += ctx => rotation = rotation;
    }
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        isMoving = false;
    }


    private void Move()
    {
        
    }

    // Update is called once per frame
    void Update () {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = 0f;

        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime * moveSpeed;
        transform.Translate(m,Space.World);

        


       
       
        if(rotation.x >= stickThreshold || rotation.x <= -stickThreshold || rotation.y >= stickThreshold || rotation.y <=-stickThreshold)
        {
            Vector2 r = new Vector2(rotation.x, rotation.y); //* Time.deltaTime * rotateSpeed;

            float angle = Mathf.Atan2(-rotation.y, -rotation.x) * Mathf.Rad2Deg;
            angle += 180;

            Quaternion currentRot = transform.rotation;

            //transform.rotation = Vector3.Lerp(currentRot, new Vector3(0, 0, angle), 1.0f);

            transform.rotation = Quaternion.RotateTowards(currentRot, Quaternion.Euler(0, 0, angle), rotateSpeed * Time.deltaTime);

           // transform.rotation = Quaternion.Euler(0, 0, angle);
        }
       


        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * (-180 * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * (180 * Time.deltaTime));
        }


        if(Input.GetKey(KeyCode.UpArrow))
        {

            transform.position += transform.right * Time.deltaTime * moveSpeed;
            if(isMoving !=true)
            {
                isMoving = true;
                setMoving(isMoving);
            }

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= transform.right * Time.deltaTime * moveSpeed;
            if (isMoving != true)
            {
                isMoving = true;
                setMoving(isMoving);
            }
            
        }
        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            if(isMoving == true)
            {
                isMoving = false;
                setMoving(isMoving);
            }
            
        }


        if (m.x != 0 && m.y != 0)
        {
            
            if (isMoving != true)
            {
                isMoving = true;
                setMoving(isMoving);
            }

        }
        else
        {
            
            if (isMoving == true)
            {
                isMoving = false;
                setMoving(isMoving);
            }
        }


        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void setMoving(bool move)
    {
        animator.SetBool("IsMoving", move);
    }

}

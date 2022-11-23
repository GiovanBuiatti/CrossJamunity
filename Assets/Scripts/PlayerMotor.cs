using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;


    private float speed = 20.0f;
    

    private float animationDuration = 2.0f;
    private float startTime;

    private bool isDead = false;
    private int rotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
        {
            return;
        }


        if (Time.time - startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;

        // X - Gauche et Droite
        moveVector.x =  Input.GetAxisRaw("Horizontal")* speed;

        
        // CONTROL MOBILE
        if (Input.GetMouseButton(0))
        {
            if(Input.mousePosition.x > Screen.width / 2)
            {
                transform.rotation = new Quaternion(0, 0, 10, 1);
                if (rotation < 20)
                {
                    rotation += 1;
                }
                if(transform.position.x < 5)
                {
                    moveVector.x = 10;
                }
                
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, -10, 1);
                if (rotation > -20)
                {
                    rotation -= 1;
                }
                if (transform.position.x>-5)
                {
                    moveVector.x = -10;
                }


            }
            Debug.Log(transform.rotation);
            Debug.Log(rotation);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0 , 0);
        }

        
        // Z - Avant et Arri�re
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
    }

    public void SetSpeed(float modifier)
    {
        speed = 20.0f + modifier * 5;
    }

    //Called everytime our capsule hits something
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Rock")
        {
            if (hit.point.z > transform.position.z + controller.radius)
            {
                Death();
            }
                
        }

        
    }
    private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
    }
}

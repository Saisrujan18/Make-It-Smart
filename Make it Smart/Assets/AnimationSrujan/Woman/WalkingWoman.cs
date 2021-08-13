using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingWoman : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 directionVector;
    private Transform myTransform;
    public float speed=2;
    private Rigidbody2D myRigidbody;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>(); 
        myRigidbody = GetComponent<Rigidbody2D>();
        //ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        //UpdateAnimation();
    }

    void UpdateAnimation()
    {
        anim.SetFloat("MoveX", directionVector.x);
        anim.SetFloat("MoveY", directionVector.y);
    }

    /*void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch(direction)
        {
            case 0:
                directionVector = Vector3.right;
                break;
            case 1:
                directionVector = Vector3.up;
                break;
            case 2:
                directionVector = Vector3.left;
                break;
            default:
                directionVector = Vector3.down;
                break;
        }
        UpdateAnimation();
    }*/

    void Move()
    {
        myRigidbody.MovePosition(myTransform.position + directionVector * speed * Time.deltaTime);
    }
}
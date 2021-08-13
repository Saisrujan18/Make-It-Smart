using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPath : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform[] waypoints;
    [SerializeField] public float moveSpeed = 2f;
    //private Rigidbody2D myRigidbody;
    private int waypointIndex = 0;
    private Vector3 velocityVector;
    private Animator anim;
    //public float time1 = 1f;
    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = waypoints[waypointIndex].transform.position;
        //myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            velocityVector = new Vector3(waypoints[waypointIndex].transform.position.x - transform.position.x, waypoints[waypointIndex].transform.position.y - transform.position.y, 0);
            UpdateAnimation();
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
            
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;

                /*if (waypointIndex == 0)
                {
                    reverse = false;
                    waypointIndex = 0;
                }
                if (!reverse)
                {
                    waypointIndex += 1;
                }
                if (reverse)
                {
                    waypointIndex -= 1;
                }*/
                //yield return new WaitForSeconds(time1);
            }
            if (waypointIndex >= waypoints.Length - 1)
            {

                transform.position = waypoints[0].transform.position;
                waypointIndex = 0;

            }

        }
    }
    void UpdateAnimation()
    {
        anim.SetFloat("MoveX", velocityVector.x);
        anim.SetFloat("MoveY", velocityVector.y);
    }
}

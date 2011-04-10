using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {
    // We define the state of the element
    public AIState State;                       // State Machine
    public float distanceUntilFade  = 3;
    public float fadeSpeed          = 5;
    public float walkSpeed          = 5;
    public float runSpeed           = 10;
    public float turnSpeed          = 10;
    public Vector3 endPosition;

    // A star
    public AStar aStar = new AStar();
    public bool isGoing;

    private float _currentSpeed;

    public bool usingAStar;                     // A Star
    public bool Idle{get; set;}                // Idle

    private const float DISTANCE_FROM_SELF_FOR_RANDOM = 4;  // Wander
    private const float DISTANCE_UNTIL_GO_TO_IDLE = 1;

    void Start()
    {
        _currentSpeed = runSpeed;
    }

    void Update()
    {
        if(!isGoing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                endPosition = GameObject.Find("Goal").transform.position;
            }
            
            AStarNode startNode = AStarNode.GetClosestNode(transform.position);
            
            AStarNode endNode = AStarNode.GetClosestNode(endPosition);

            aStar.FindPath(startNode, endNode);
            startNode.renderer.material.color = Color.green;
            endNode.renderer.material.color = Color.red;
            isGoing = true;
        }
        // A*
        if(aStar != null && aStar.path != null)
        {
            var i = aStar.path.Count - 1;

            if(i == 0)
            {
                if(Vector3.Distance(transform.position, aStar.path[i].transform.position) <= distanceUntilFade)
                    _currentSpeed = Mathf.Lerp(_currentSpeed, walkSpeed, Time.deltaTime * fadeSpeed);
            }
            else
                _currentSpeed = runSpeed;

            float distanceToTarget = Vector3.Distance(transform.position, aStar.path[i].transform.position);
            if (distanceToTarget <= .25f)
            {
                aStar.path.Remove(aStar.path[i]);
                if (i <= 0)
                {
                    isGoing = false;
                    rigidbody.velocity = Vector3.zero;
                    return;
                }
            }
            if(i < aStar.path.Count && i >=  0)
            {
                transform.rotation = Quaternion.Slerp(  transform.rotation,
                                                        Quaternion.LookRotation(aStar.path[i].transform.position - transform.position),
                                                        Time.deltaTime * turnSpeed);
            }
        }

        Movement();
        Steering();
    }

    public void Steering()
    {
        if (Physics.Raycast(transform.position, transform.forward, 2))
        {
            var direction = transform.right; // right value of var?
            if (Physics.Raycast(transform.position, (transform.forward + transform.right).normalized, 2)) // Left
            {
                direction = -direction;
                if (Physics.Raycast(transform.position, (transform.forward - transform.right).normalized, 2)) // Back
                {
                    direction = -transform.forward;
                }
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        Time.deltaTime * turnSpeed);
            }
        }
    }

    public void Movement()
    {
        if (!Idle)
        {
            rigidbody.AddRelativeForce(Vector3.forward * _currentSpeed); // Move forward if not idle
        }

        if (rigidbody.velocity.magnitude > _currentSpeed * .3f && _currentSpeed > walkSpeed) // If We are walking we are going to speed up
        {
            rigidbody.velocity = transform.forward * _currentSpeed;
        }
    }
};

// States of the element
// Can be Idle, AStarWander or wonder
public enum AIState
{
    Idle,
    AStarWander,
    Wander
};
using System.Collections.Generic;
using UnityEngine;

// Each navigation point is called node
public class Node : MonoBehaviour
{
    // The node have different neighbors, the neighbors
    // are stored in a List
    public List<Neighbor> Neighbors = new List<Neighbor>();
    // Check if the node is on the closed list
    public bool Closed = false;

    public void Start()
    {
        FindConnections();
    }

    private void FindConnections()
    {
        FindBack();     // Red
        FindForward();  // Blue
        FindLeft();     // Yellow
        FindRight();    // Green
        FindBackLeft(); // Cyan
        FindBackRight(); // Black
        FindForwardLeft(); // Magenta
        FindForwardRight(); //White
    }

    // Find Back:: Ray is drawn on red.
    // Don't know why has to be -Vector3.back
    private void FindBack()
    {
        RaycastHit hit;
        Vector3 transformDirection = transform.TransformDirection(-Vector3.back);
        int mToHit = 10;

        Debug.DrawRay(transform.position, transformDirection * mToHit, Color.red);
        if (Physics.Raycast(transform.position, transformDirection, out hit, mToHit))
        {
            print("Back: " + hit.distance);
        }
    }

    private void FindForward()
    {
        RaycastHit hit;
        Vector3 transformDirection = transform.TransformDirection(-Vector3.forward);
        int mToHit = 10;

        Debug.DrawRay(transform.position, transformDirection * mToHit, Color.blue);
        if (Physics.Raycast(transform.position, transformDirection, out hit, mToHit))
        {
            print("Forward: " + hit.distance);
        }
    }

    private void FindRight()
    {
        RaycastHit hit;
        Vector3 transformDirection = transform.TransformDirection(Vector3.right);
        int mToHit = 10;

        Debug.DrawRay(transform.position, transformDirection * mToHit, Color.green);
        if (Physics.Raycast(transform.position, transformDirection, out hit, mToHit))
        {
            print("Right: " + hit.distance);
        }
    }

    private void FindLeft()
    {
        RaycastHit hit;
        Vector3 transformDirection = transform.TransformDirection(Vector3.left);
        int mToHit = 10;

        Debug.DrawRay(transform.position, transformDirection * mToHit, Color.yellow);
        if (Physics.Raycast(transform.position, transformDirection, out hit, mToHit))
        {
            print("Left: " + hit.distance);
        }
    }

    private void FindBackLeft()
    {
        RaycastHit hit;
        Vector3 transformDirection = transform.TransformDirection(-Vector3.back - Vector3.right);
        int mToHit = 10;

        Debug.DrawRay(transform.position, transformDirection * mToHit, Color.cyan);
        if (Physics.Raycast(transform.position, transformDirection, out hit, mToHit))
        {
            print("Back: " + hit.distance);
        }
    }

    private void FindBackRight()
    {
        RaycastHit hit;
        Vector3 transformDirection = transform.TransformDirection(-Vector3.back + Vector3.right);
        int mToHit = 10;

        Debug.DrawRay(transform.position, transformDirection * mToHit, Color.black);
        if (Physics.Raycast(transform.position, transformDirection, out hit, mToHit))
        {
            print("Back: " + hit.distance);
        }
    }

    private void FindForwardLeft()
    {
        RaycastHit hit;
        Vector3 transformDirection = transform.TransformDirection(Vector3.back - Vector3.right);
        int mToHit = 10;

        Debug.DrawRay(transform.position, transformDirection * mToHit, Color.magenta);
        if (Physics.Raycast(transform.position, transformDirection, out hit, mToHit))
        {
            print("Back: " + hit.distance);
        }
    }

    private void FindForwardRight()
    {
        RaycastHit hit;
        Vector3 transformDirection = transform.TransformDirection(Vector3.back + Vector3.right);
        int mToHit = 10;

        Debug.DrawRay(transform.position, transformDirection * mToHit, Color.white);
        if (Physics.Raycast(transform.position, transformDirection, out hit, mToHit))
        {
            print("Back: " + hit.distance);
        }
    }


    public void Update()
    {
        FindConnections();
    }
} ;

public class Neighbor : MonoBehaviour
{
    public Node Node;
    public float Cost;
} ;

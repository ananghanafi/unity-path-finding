using System.Collections.Generic;
using UnityEngine;

// Each navigation point is called NodeM
public class NodeM : MonoBehaviour
{
    // The NodeM have different neighbors, the neighbors
    // are stored in a List
    public List<Neighbor> Neighbors = new List<Neighbor>();
    // Check if the NodeM is on the closed list
    public bool Closed = false;
    // Add neighbors
    public bool Add = true;
    // Start NodeM?
    public bool start   = false;
    public bool end     = false;
    private float _f = 0;
    private float _g = 0;
    private float _h = 0;
    private NodeM _parent;

    public NodeM Parent
    {
        get { return _parent; }
        set { _parent = value; }
    }

    public float F
    {
        get { return _f; }
        set { _f = value; }
    }

    public float G
    {
        get { return _g; }
        set { _g = value; }
    }

    public float H
    {
        get { return _h; }
        set { _h = value; }
    }

    public void Start()
    {
        FindConnections();
        Add = false;
        if(start)
            transform.Find("NodeMCube").gameObject.renderer.material.color = Color.green;
        if(end)
            transform.Find("NodeMCube").gameObject.renderer.material.color = Color.red;
    }

    private void FindConnections()
    {
        FindBack();             // Red
        FindForward();          // Blue
        FindLeft();             // Yellow
        FindRight();            // Green
        FindBackLeft();         // Cyan
        FindBackRight();        // Black
        FindForwardLeft();      // Magenta
        FindForwardRight();     // White
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
            if (hit.collider.gameObject.transform.parent.gameObject.name == "NodeM")
            {
                Neighbor neighbor = new Neighbor();
                neighbor.NodeM = hit.collider.gameObject.transform.parent.gameObject;
                neighbor.Cost = Vector3.Distance(transform.position, hit.collider.gameObject.transform.parent.transform.position); ;
                if(Add) Neighbors.Add(neighbor);
                print("Back added");
            }
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
            if (hit.collider.gameObject.transform.parent.gameObject.name == "NodeM")
            {
                Neighbor neighbor = new Neighbor();
                neighbor.NodeM = hit.collider.gameObject.transform.parent.gameObject;
                neighbor.Cost = Vector3.Distance(transform.position, hit.collider.gameObject.transform.parent.transform.position);
                if (Add) Neighbors.Add(neighbor);
                print("Forward added");
            }
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
            if (hit.collider.gameObject.transform.parent.gameObject.name == "NodeM")
            {
                Neighbor neighbor = new Neighbor();
                neighbor.NodeM = hit.collider.gameObject.transform.parent.gameObject;
                neighbor.Cost = Vector3.Distance(transform.position, hit.collider.gameObject.transform.parent.transform.position);
                if (Add) Neighbors.Add(neighbor);
                print("Right added");
            }
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
            if (hit.collider.gameObject.transform.parent.gameObject.name == "NodeM")
            {
                Neighbor neighbor = new Neighbor();
                neighbor.NodeM = hit.collider.gameObject.transform.parent.gameObject;
                neighbor.Cost = Vector3.Distance(transform.position, hit.collider.gameObject.transform.parent.transform.position);
                if (Add) Neighbors.Add(neighbor);
                print("Left added");
            }
        }
    }

    private void FindBackLeft()
    {
        RaycastHit hit;
        Vector3 transformDirection = transform.TransformDirection(-Vector3.back - Vector3.right);
        int mToHit = 14;

        Debug.DrawRay(transform.position, transformDirection * mToHit, Color.cyan);
        if (Physics.Raycast(transform.position, transformDirection, out hit, mToHit))
        {
            if (hit.collider.gameObject.transform.parent.gameObject.name == "NodeM")
            {
                Neighbor neighbor = new Neighbor();
                neighbor.NodeM = hit.collider.gameObject.transform.parent.gameObject;
                neighbor.Cost = Vector3.Distance(transform.position, hit.collider.gameObject.transform.parent.transform.position);
                if (Add) Neighbors.Add(neighbor);
                print("Back Left added");
            }
        }
    }

    private void FindBackRight()
    {
        RaycastHit hit;
        Vector3 transformDirection = transform.TransformDirection(-Vector3.back + Vector3.right);
        int mToHit = 14;

        Debug.DrawRay(transform.position, transformDirection * mToHit, Color.black);
        if (Physics.Raycast(transform.position, transformDirection, out hit, mToHit))
        {
            if (hit.collider.gameObject.transform.parent.gameObject.name == "NodeM")
            {
                Neighbor neighbor = new Neighbor();
                neighbor.NodeM = hit.collider.gameObject.transform.parent.gameObject;
                neighbor.Cost = Vector3.Distance(transform.position, hit.collider.gameObject.transform.parent.transform.position);
                if (Add) Neighbors.Add(neighbor);
                print("Back right added");
            }
        }
    }

    private void FindForwardLeft()
    {
        RaycastHit hit;
        Vector3 transformDirection = transform.TransformDirection(Vector3.back - Vector3.right);
        int mToHit = 14;

        Debug.DrawRay(transform.position, transformDirection * mToHit, Color.magenta);
        if (Physics.Raycast(transform.position, transformDirection, out hit, mToHit))
        {
            if (hit.collider.gameObject.transform.parent.gameObject.name == "NodeM")
            {
                Neighbor neighbor = new Neighbor();
                neighbor.NodeM = hit.collider.gameObject.transform.parent.gameObject;
                neighbor.Cost = Vector3.Distance(transform.position, hit.collider.gameObject.transform.parent.transform.position);
                if (Add) Neighbors.Add(neighbor);
                print("Forward Left added");
            }
        }
    }

    private void FindForwardRight()
    {
        RaycastHit hit;
        Vector3 transformDirection = transform.TransformDirection(Vector3.back + Vector3.right);
        int mToHit = 14;

        Debug.DrawRay(transform.position, transformDirection * mToHit, Color.white);
        if (Physics.Raycast(transform.position, transformDirection, out hit, mToHit))
        {
            if (hit.collider.gameObject.transform.parent.gameObject.name == "NodeM")
            {
                Neighbor neighbor = new Neighbor();
                neighbor.NodeM = hit.collider.gameObject.transform.parent.gameObject;
                neighbor.Cost = Vector3.Distance(transform.position, hit.collider.gameObject.transform.parent.transform.position);
                if (Add) Neighbors.Add(neighbor);
                print("Forward right added");
            }
        }
    }

    public void Update()
    {
        if(Input.GetKey("z"))
        {
            DebugNeighbors();
        }

        updateTextInfo();
    }

    public void updateTextInfo()
    {
        transform.Find("tF").gameObject.GetComponent<TextMesh>().text = ((int)_f).ToString();
        transform.Find("tG").gameObject.GetComponent<TextMesh>().text = ((int)_g).ToString();
        transform.Find("tH").gameObject.GetComponent<TextMesh>().text = ((int)_h).ToString();
    }

    public void DebugNeighbors()
    {
        for (int i = 0; i < Neighbors.Count; i++)
        {
            Transform n = Neighbors[i].NodeM.transform.Find("NodeMCube");
            GameObject parent = n.gameObject;
            parent.renderer.material.color = Color.red;
        }
    }
} ;

public class Neighbor
{
    public GameObject NodeM;
    public float Cost;
} ;

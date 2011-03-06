using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

    private GameObject[] Nodes;
    private bool BStartNode = false;
    private bool BEndNode = false;
    private Node start;
    private Node end;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("n"))
        {
            // Save nodes game object
            if (Nodes == null)
            {
                print("Looking for nodes");
                FindNodes();
                PrintNodes();
                print("Search for nodes ended");
                return;
            }

            if (!BStartNode && !BEndNode)
            {
                print("Look for start and end node");
                print("Search ended for start and end node");
                BEndNode = true;
                return;
            }

        }
	}

    private void FindNodes()
    {
        Nodes = GameObject.FindGameObjectsWithTag("Node");
    }

    private void PrintNodes()
    {
        for (int i = 0; i < Nodes.Length; i++)
        {
            GameObject n = Nodes[i];
            print(n.name);
        }
    }

    private void FindStartNode()
    {
        for (int i = 0; i < Nodes.Length; i++)
        {
            GameObject go = Nodes[i];
            Node n = go.GetComponent("Node") as Node;
            if (n.start)
            {
                start = n;
                BStartNode = true;
                return;
            }
        }
    }

    private void FindEndNode()
    {
        for (int i = 0; i < Nodes.Length; i++)
        {
            GameObject go = Nodes[i];
            Node n = go.GetComponent("Node") as Node;
            if (n.end)
            {
                end = n;
                BEndNode = true;
                return;
            }
        }
    }
};

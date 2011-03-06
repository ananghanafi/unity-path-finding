using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {

    private GameObject[] Nodes;
    private bool BStartNode = false;
    private bool BEndNode = false;
    private Node StartNode;
    private Node EndNode;
    private List<Node> OpenList = new List<Node>();
    private List<Node> ClosedList = new List<Node>();
    private bool SStarted = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("n"))
        {
            // Find all nodes
            if (Nodes == null)
            {
                //print("Looking for nodes");
                FindNodes();
                //PrintNodes();
                print("Search for nodes ended");
                return;
            }

            // Looks for start node and end node
            if (!BStartNode && !BEndNode)
            {
                //print("Look for start and end node");
                FindStartNode();
                FindEndNode();
                SetNodesH();
                print("Search ended for start and end node");
                return;
            }
            // If search has not started
            if(!SStarted)
            {
                LookNeighbors(StartNode);
                SetNeighborsG(StartNode);
                SetNeighborsF(StartNode);
                DoSearching(startNode);
                SStarted = true;
                return;
            }
            
        }
	}

    private void DoSearching(Node n)
    {
        Node move;
        float f = 1000000; // Solve this
        for (int i = 0; i < n.Neighbors.Count; i++)
        {
            Node _t = n.Neighbors[i].Node.GetComponent("Node") as Node;
            if (_t.F < f)
            {
                f = _t.F;
                move = _t;
            }
        }
        
        LookNeighbors(move);
        SetNeighborsG(move);
        SetNeighborsF(move);
    }

    private void SetNeighborsG(Node n)
    {
        for (int i = 0; i < n.Neighbors.Count; i++)
        {
            Node _t = n.Neighbors[i].Node.GetComponent("Node") as Node;
            _t.G = n.Neighbors[i].Cost;
        }
    }

    private void SetNeighborsF(Node n)
    {
        for (int i = 0; i < n.Neighbors.Count; i++)
        {
            Node _t = n.Neighbors[i].Node.GetComponent("Node") as Node;
            _t.F = _t.G + _t.H;
        }
    }

    private void SetNodesH()
    {
        for (int i = 0; i < Nodes.Length; i++)
        {
            Node n  = Nodes[i].GetComponent("Node") as Node;
            n.H = Vector3.Distance(n.transform.position, EndNode.transform.position);
        }
    }

    private void LookNeighbors(Node n)
    {
        for (int i = 0; i < n.Neighbors.Count; i++)
        {
            Node _t = n.Neighbors[i].Node.GetComponent("Node") as Node;
            if (!IsItOnOpenList(_t))
            {
                _t.Parent = n;
                OpenList.Add(_t);
            }
        }
        OpenList.Remove(n);
        n.Closed = true;
        ClosedList.Add(n);
        print("Size of Open List: " + OpenList.Count);
        print("Size of Closed List: " + ClosedList.Count);
    }

    private bool IsItOnOpenList(Node n)
    {
        bool answer = false;
        for (int i = 0; i < OpenList.Count; i++)
        {
            if (n == OpenList[i])
            {
                answer = true;
                break;
            }
        }
        return answer;
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
                StartNode = n;
                BStartNode = true;
                OpenList.Add(StartNode);
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
                EndNode = n;
                BEndNode = true;
                return;
            }
        }
    }

};

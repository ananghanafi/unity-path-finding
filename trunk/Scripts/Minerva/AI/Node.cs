using System.Collections.Generic;
using UnityEngine;

// Each navigation point is called node
public class Node : MonoBehaviour
{
    // The node have different neighbors, the neighbors
    // are stored in a List
    public List<Node> Nodes = new List<Node>();
    public bool Closed = false;
} ;
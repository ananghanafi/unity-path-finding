using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Minerva/A* Node")]
public class AStarNode : MonoBehaviour 
{

    public static List<AStarNode> nodes = new List<AStarNode>();

    public bool closed = false;
    public float gScore;
    public float hScore;
    public float fScore;

    public AStarNode parent;

    public List<AStarNode> connections = new List<AStarNode>();

	// Use this for initialization
	void Start () {
	    nodes.Add(this);
	}
	
	// Update is called once per frame
	public void Reset()
	{
	    closed = false;
	    parent = null;
	    gScore = 0;
	    fScore = 0;
	    gScore = 0;
	    renderer.material.color = Color.grey;
	}

    public static AStarNode GetClosestNode(Vector3 position)
    {
        // AStarNode to return
        AStarNode retVal = null;
        // Shortest Distance is infinitely positive
        var shDist = float.PositiveInfinity;
        // Iterate through all the nodes
        foreach (var aStarNode in nodes)
        {
            //Debug.Log("Is " + aStarNode.name + " the closest node to "+position.ToString()+"? ");
            // Distance from this node, to the aStarNode
            var dist = Vector3.Distance(position, aStarNode.transform.position);
            // if distance is between is less than infinite positive then
            if(dist <= shDist)
            {
                //Debug.Log("Yes, it is!");
                // shortest distance = distance
                shDist = dist;
                // return value is the current evaluated a star node
                retVal = aStarNode;
            }
        }
        // return value
        return retVal;
    }
}

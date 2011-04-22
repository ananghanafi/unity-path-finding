using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AStar 
{
    public List<AStarNode> path;
    static Comparison<AStarNode> FScoreComparison = new Comparison<AStarNode>(CompareNodesByFScore);
    
    static int CompareNodesByFScore(AStarNode x, AStarNode y)
    {
        if (x.fScore > y.fScore) return 1;
        if (x.fScore < y.fScore) return -1;

        return 0;
    }

    public void printPath()
    {
        for(int i = 0; i < path.Count; i++)
        {
            //Debug.Log("[ " + i + " ]: " +  path[i].name);
        }
    }

    public void FindPath(AStarNode startNode, AStarNode endNode)
    {
        // Reset all scores
        foreach (AStarNode node in AStarNode.nodes)
        {
            node.Reset();
        }
        // Set of nodes that were already evaluated (visited)
        List<AStarNode> closed  = new List<AStarNode>();
        // Set of nodes that are temptative to evaluation (not visited)
        List<AStarNode> open    = new List<AStarNode>();
        // Distance to the start node
        startNode.gScore = 0;
        // Distance to the goal node
        startNode.hScore = DistanceBetweenNodes(startNode, endNode);
        open.Add(startNode);
        while (open.Count > 0)
        {
            // Sort by lowest f_scores
            open.Sort(FScoreComparison);
            // Select the node with the lowest score
            AStarNode current = open[0];
            // Remove current from the open set and add it to the closed set
            open.Remove(current);
            closed.Add(current);
            // Set current node variable close to true
            current.closed = true;
            // Check if current evaluated node is end goal
            if(current == endNode)
            {
                // It is!
                path = new List<AStarNode>();
                AStarNode node = endNode;
                while (node != null)
                {
                    path.Add(node);
                    node = node.parent;
                }
                printPath();
                return;
            } // it isn't!
            // Now we are going to evaluate the current node neighbours
            foreach (AStarNode neighbour in current.connections)
            {
                //Debug.Log("Neighbour that's being evaluated: " + neighbour.name);
                // If neighbour is already close, meaning it's already in closed set we
                // don't have to evaluate it);
                if (neighbour.closed)
                    continue;
                // Calculate tentative g_score
                float tentativeGScore = current.gScore + DistanceBetweenNodes(current, neighbour);
                // Set tentativeIsBetter to false
                bool tentativeIsBetter = false;
                // If neighbour is not in the open set
                if(!open.Contains(neighbour))
                {
                    // Add it to the open set
                    open.Add(neighbour);
                    tentativeIsBetter = true;
                }
                else if(tentativeGScore < neighbour.gScore)
                {
                    tentativeIsBetter = true;
                }
                else
                {
                    tentativeIsBetter = false;
                }

                if(tentativeIsBetter)
                {
                    // Set parent node
                    neighbour.parent = current;
                    neighbour.gScore = tentativeGScore;
                    neighbour.hScore = DistanceBetweenNodes(neighbour, endNode);
                    neighbour.fScore = neighbour.gScore + neighbour.hScore;
                }
            }
        }
    }
    
    public void FindPathDJ(AStarNode startNode, AStarNode endNode)
    {
        // Reset all scores
        foreach (AStarNode node in AStarNode.nodes)
        {
            node.Reset();
        }
        // Set of nodes that were already evaluated (visited)
        List<AStarNode> closed  = new List<AStarNode>();
        // Set of nodes that are temptative to evaluation (not visited)
        List<AStarNode> open    = new List<AStarNode>();
        // Distance to the start node
        startNode.gScore = 0;
        // Distance to the goal node
        startNode.hScore = DistanceBetweenNodes(startNode, endNode);
        open.Add(startNode);
        while (open.Count > 0)
        {
            // Sort by lowest f_scores
            open.Sort(FScoreComparison);
            // Select the node with the lowest score
            AStarNode current = open[0];
            // Remove current from the open set and add it to the closed set
            open.Remove(current);
            closed.Add(current);
            // Set current node variable close to true
            current.closed = true;
            // Check if current evaluated node is end goal
            if(current == endNode)
            {
                // It is!
                path = new List<AStarNode>();
                AStarNode node = endNode;
                while (node != null)
                {
                    path.Add(node);
                    node = node.parent;
                }
                printPath();
                return;
            } // it isn't!
            // Now we are going to evaluate the current node neighbours
            foreach (AStarNode neighbour in current.connections)
            {
                //Debug.Log("Neighbour that's being evaluated: " + neighbour.name);
                // If neighbour is already close, meaning it's already in closed set we
                // don't have to evaluate it);
                if (neighbour.closed)
                    continue;
                // Calculate tentative g_score
                float tentativeGScore = current.gScore + DistanceBetweenNodes(current, neighbour);
                // Set tentativeIsBetter to false
                bool tentativeIsBetter = false;
                // If neighbour is not in the open set
                if(!open.Contains(neighbour))
                {
                    // Add it to the open set
                    open.Add(neighbour);
                    tentativeIsBetter = true;
                }
                else if(tentativeGScore < neighbour.gScore)
                {
                    tentativeIsBetter = true;
                }
                else
                {
                    tentativeIsBetter = false;
                }

                if(tentativeIsBetter)
                {
                    // Set parent node
                    neighbour.parent = current;
                    neighbour.gScore = tentativeGScore;
                    neighbour.hScore = DistanceBetweenNodes(neighbour, endNode);
                    neighbour.fScore = neighbour.gScore/* + neighbour.hScore*/;
                }
            }
        }
    }

    private static float DistanceBetweenNodes(AStarNode from, AStarNode to)
    {
        return Vector3.Distance(from.transform.position, to.transform.position);
    }
    /*
    public void FindPath(AStarNode startNode, AStarNode endNode)
    {
        // Reset all scores
        foreach (AStarNode node in AStarNode.nodes)
        {
            node.Reset();
        }
        // Path
        path = null;
        // Nodes that'll be evaluated
        var openList = new List<AStarNode> { startNode };
        // While we evaluate
        while (openList.Count > 0)
        {
            openList.Sort(FScoreComparison);
            var current = openList[0];
            openList.Remove(current);
            current.closed = true;

            if (current == endNode)
            {
                path = new List<AStarNode>();
                var node = endNode;
                //node.renderer.material.color = Color.yellow;
                while(node != null)
                {
                    path.Add(node);
                    node = node.parent;
                    //node.renderer.material.color = Color.green;
                }
                printPath();
                return;
            }

            foreach (var neighbor in current.connections)
            {
                if (neighbor.closed)
                    continue;
                var neighborGScore = current.gScore + Vector3.Distance(current.transform.position, neighbor.transform.position);
                if(!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                    neighbor.parent = current;
                    neighbor.gScore = neighborGScore;
                    neighbor.hScore = Vector3.Distance(
                                                        neighbor.transform.position,
                                                        endNode.transform.position
                                                        );
                }
                else if(neighborGScore < neighbor.gScore)
                {
                    neighbor.parent = current;
                    neighbor.gScore = neighborGScore;
                    neighbor.fScore = neighbor.gScore + neighbor.hScore;
                }
            }
        }
    }
    */
}

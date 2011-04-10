using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIMaster : MonoBehaviour {

    private List<AI> agents;

	// Use this for initialization
	void Start ()
    {
        agents = new List<AI>();
        populateAgentList();
        printAgentList();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // populate agent list
    void populateAgentList()
    {
        GameObject[] _agents = GameObject.FindGameObjectsWithTag("Agent");
        foreach (GameObject agent in _agents)
        {
           // Debug.Log(agent.GetComponents.ToString());
            AI ai = agent.GetComponent<AI>();
            agents.Add(ai);
        }
    }

    void printAgentList()
    {
        foreach (AI ai in agents)
        {
            Debug.Log("Name: " + ai.name);
        }
    }
}

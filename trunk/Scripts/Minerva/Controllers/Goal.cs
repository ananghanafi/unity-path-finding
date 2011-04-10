using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        float distance = Vector3.Distance(r.origin, transform.position);
        Vector3 direction = r.direction * distance;
                    
        transform.position = new Vector3(direction.x, transform.position.y, direction.z);
	}
}
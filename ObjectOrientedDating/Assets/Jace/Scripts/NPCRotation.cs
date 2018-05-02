using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRotation : MonoBehaviour {
    public Transform targetTransform;
    private float speed = 5f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 targetDir = targetTransform.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        newDir.y = 0;
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}

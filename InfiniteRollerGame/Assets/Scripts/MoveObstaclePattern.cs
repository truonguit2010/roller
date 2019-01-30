using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MoveObstaclePattern : MonoBehaviour {

    [SerializeField]
    private float speed = 2f;
    private bool moving = true;
	
	// Update is called once per frame
	void Update () {
        if (moving) {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    public void startMove() {
        moving = true;
    }

    public void endMove() {
        moving = false;
    }
}

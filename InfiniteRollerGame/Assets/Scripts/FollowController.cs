using UnityEngine;

[DisallowMultipleComponent]
public class FollowController : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset = Vector3.zero;

    private Transform myTransform;

	void Start () {
        this.myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        this.myTransform.position = target.position + offset;
	}
}

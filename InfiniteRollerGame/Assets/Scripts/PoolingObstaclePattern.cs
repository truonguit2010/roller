using UnityEngine;

[DisallowMultipleComponent]
public class PoolingObstaclePattern : MonoBehaviour {

    public bool isFree { get; private set; }

    public GameObject prefab { get; set; }
    public Transform trigger { get; set; }
    private Transform myTransform;

    private void Awake()
    {
        this.myTransform = this.transform;
    }

    private void Update()
    {
        if (!this.isFree) {
            if (this.myTransform.position.x < this.trigger.position.x)
            {
                this.isFree = true;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void forceFree() {
        this.isFree = true;
        this.gameObject.SetActive(false);
    }

    public void useMe() {
        if (this.isFree) {
            this.isFree = false;
            this.gameObject.SetActive(true);
        } else {
            throw new System.Exception("Wrong flow.");
        }
    }
}

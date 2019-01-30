using UnityEngine;

[DisallowMultipleComponent]
public class ScoreTrigger : MonoBehaviour {
    [SerializeField]
    private int score = 10;

    public Transform characterTransform;
    private Transform myTransform;
    private bool isTrigger = false;

    private GameController gameController;

	// Use this for initialization
	void Start () {
        this.myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isTrigger) {
            if (myTransform.position.x < characterTransform.position.x) {
                isTrigger = true;
                gameController.addScore(score);
            }
        }
	}

    public void setOrReset(Transform character, GameController gameController) {
        this.characterTransform = character;
        this.gameController = gameController;
        isTrigger = false;
    }
}

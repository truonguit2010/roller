using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[DisallowMultipleComponent]
public class Tap2JumpButton : MonoBehaviour {

    [SerializeField]
    private Character character;

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            character.doJump();
        });
	}
	
}

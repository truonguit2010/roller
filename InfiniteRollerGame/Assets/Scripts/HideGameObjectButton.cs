using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[DisallowMultipleComponent]
public class HideGameObjectButton : MonoBehaviour {

    [SerializeField]
    private GameObject willHideObject;

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            willHideObject.SetActive(false);
        });
	}
	
}

using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]
public class ScoreText : MonoBehaviour {

    [SerializeField]
    private Text text;

    private void Start()
    {
        if (this.text == null) {
            this.text = GetComponent<Text>();
        }
    }

    public void setScore(int score) {
        this.text.text = string.Format("Score: {0}", score);
    }
}

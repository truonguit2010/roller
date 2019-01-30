using UnityEngine;

[DisallowMultipleComponent]
public class PatternScoreTrigger : MonoBehaviour {

    [SerializeField]
    private ScoreTrigger[] scoreTriggers;

    public void setOrReset(Transform character, GameController gameController)
    {
        for (int i = 0; i < scoreTriggers.Length; i++) {
            scoreTriggers[i].setOrReset(character, gameController);
        }
    }
}

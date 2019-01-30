using UnityEngine;

[System.Serializable]
public class ScoreObject {
    public string userName;
    public int score;

    public string toJson()
    {
        return JsonUtility.ToJson(this);
    }
}

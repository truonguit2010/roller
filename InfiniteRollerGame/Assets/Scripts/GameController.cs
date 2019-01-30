using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Collections;

[System.Serializable]
public class OnScoreChanged : UnityEvent<int> {}

public class GameController : MonoBehaviour {
    private readonly string TAG = typeof(GameController).Name;

    private const string LEADER_BOARD_URL = "http://kixeyetest.com/leaderboard";

    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Text pauseButtonText;
    private bool isPause = false;

    public int totalScore { get; private set; }
    [SerializeField]
    private OnScoreChanged onScoreChanged;

    [SerializeField]
    private ObstacleSpawner obstacleSpawner;
    [SerializeField]
    private Character character;

    private void Awake()
    {
        pauseButton.onClick.AddListener(() =>
        {
            if (isPause) {
                resumeGame();
            } else {
                pauseGame();
            }
            isPause = !isPause;
        });
    }

    public void startGame() {
        obstacleSpawner.startGame();

        character.startGame();

        totalScore = 0;
        onScoreChanged.Invoke(totalScore);

        isPause = false;
    }

    public void pauseGame() {
        this.character.pauseGame();
        this.obstacleSpawner.pauseGame();
        this.pauseButtonText.text = "Resume";
    }

    public void resumeGame() {
        this.character.resumeGame();
        this.obstacleSpawner.resumeGame();
        this.pauseButtonText.text = "Pause";
    }

    public void exitGame() {
        Application.Quit();
    }

    public void endGame() {
        obstacleSpawner.endGame();
        this.character.endGame();
        submitScore();
    }

    public void addScore(int score) {
#if DEBUG
        Debug.LogFormat("{0} - addScore {1}", TAG, score);
#endif
        this.totalScore += score;
        onScoreChanged.Invoke(this.totalScore);
    }

    private void submitScore() {
        ScoreObject scoreObject = new ScoreObject();
        scoreObject.userName = "Username";
        scoreObject.score = 120;

        string json = scoreObject.toJson();
        StartCoroutine(PostRequest(LEADER_BOARD_URL, json));
    }

    IEnumerator PostRequest(string url, string bodyJsonString)
    {
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(bodyJsonString);
        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(bodyRaw);
        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        var request = new UnityWebRequest(url, "POST");
        request.uploadHandler = uploadHandlerRaw;
        request.downloadHandler = downloadHandlerBuffer;
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.responseCode == 404)
        {
#if DEBUG
            Debug.Log("Username not found(user has not registered with the leaderboard service)");
#endif
        }
        else if (request.responseCode == 405)
        {
#if DEBUG
            Debug.Log("Invalid Username supplied");
#endif
        }
        else if (request.responseCode == 200)
        {
#if DEBUG
            Debug.Log("OK");
#endif
        }
        else
        {
#if DEBUG
            Debug.LogFormat("{0} - {1}", TAG, request.error);
#endif
        }
    }
}

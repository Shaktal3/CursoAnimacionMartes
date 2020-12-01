using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour
{

    private List<GameObject> spawnPositions = new List<GameObject>();
    public GameObject coin;
    private int lastSpawnPositionIndex=-2;
    private int score;

    public float time;
    private float timeLeft;


    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI timeLeftText;
    [SerializeField]
    private GameObject EndGameObject;


    public static Manager _instance;
    public static Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Manager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("Manager");
                    _instance = container.AddComponent<Manager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        timeLeft = time;
        spawnPositions.AddRange(GameObject.FindGameObjectsWithTag("SpawnCoin"));
        SpawnCoinInRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft = timeLeft - Time.deltaTime;
        timeLeftText.text = "Time: " +  Mathf.CeilToInt(timeLeft);
        if(timeLeft <= 0)
        {
            EndGame();
        }
    }


    public void SpawnCoinInRandomPosition()
    { 
        GameObject c = Instantiate(coin);
        int spawnPositionIndex = -1;
        while (spawnPositionIndex < 0 || spawnPositionIndex == lastSpawnPositionIndex)
        {
            spawnPositionIndex = Random.Range(0, spawnPositions.Count);
        }
        lastSpawnPositionIndex = spawnPositionIndex;
        c.transform.position = spawnPositions[spawnPositionIndex].transform.position;
    }

    public void AddScore(int amount)
    {
        score = score + amount;
        scoreText.text = "Score: " + score;
    }

    public void AddTime(float amount)
    {
        timeLeft = timeLeft + amount;
    }

    private void EndGame()
    {
        EndGameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }



}

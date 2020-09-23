using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Instance sebagai global access
    public static GameManager instance;
    int playerScore;
    public Text scoreText;
    public GameObject multiplierText;
    public int scoreMultiplier = 1;
    public bool wasMatched = false;

    // Start is called before the first frame update

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        print("Was Matched = " + wasMatched);
        multiplierText.SetActive(wasMatched);
    }

    //Update score dan ui
    public void GetScore(int point)
    {
        playerScore += point * scoreMultiplier;
        scoreText.text = playerScore.ToString();
    }

    public int Score()
    {
        return playerScore;
    }

    public void ResetScore()
    {
        playerScore = 0;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        wasMatched = false;
        scoreText = GameObject.Find("Canvas").transform.GetChild(0).GetChild(0).GetComponent<Text>();
        multiplierText = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
    }
}

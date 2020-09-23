using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public Text TimerText;
    [SerializeField] private float duration = 60f;
    [SerializeField] private GameObject scoreView;
    [SerializeField] private Text lastScore;

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        TimerText.text = "Time Left " + (int)duration;
        if (duration <= 0 && !scoreView.activeInHierarchy)
        {
            StopAllCoroutines();
            Time.timeScale = 0;
            scoreView.SetActive(true);
            lastScore.text = GameManager.instance.Score().ToString();
        }
    }
}

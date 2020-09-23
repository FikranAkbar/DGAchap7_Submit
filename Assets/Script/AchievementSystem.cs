using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AchievementSystem : Observer
{
    public Image achievementBanner;
    public Text achievementText;

    // Event
    TileEvent cookiesEvent, cakeEvent, gumEvent;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();

        // Buat event
        cookiesEvent = new CookiesTileEvent(3);
        cakeEvent = new CakeTileEvent(10);
        gumEvent = new GumTileEvent(5);

        StartCoroutine(RegisterObserver());
    }

    public override void OnNotify(string value)
    {
        print("print value " + value + " dari onNotify AchievementSystem");
        string key;

        // Seleksi event yang terjadi, dan panggil class eventnya
        if (value.Equals("Cookies event"))
        {
            cookiesEvent.OnMatch();
            if (cookiesEvent.AchievementCompleted())
            {
                key = "Match first cookies";
                NotifyAchievement(key, value);
            }
        }

        if (value.Equals("Cake event"))
        {
            cakeEvent.OnMatch();
            if (cakeEvent.AchievementCompleted())
            {
                key = "Match 10 cake";
                NotifyAchievement(key, value);
            }
        }

        if (value.Equals("Gum event"))
        {
            gumEvent.OnMatch();
            if (gumEvent.AchievementCompleted())
            {
                key = "Match 5 gum";
                NotifyAchievement(key, value);
            }
        }
    }

    void NotifyAchievement(string key, string value)
    {
        // check jika achievement sudah diperoleh
        if (PlayerPrefs.GetInt(value) == 1)
        {
            return;
        }

        PlayerPrefs.SetInt(value, 1);
        if (achievementText != null)
        {
            achievementText.text = key + " Unlocked !";
        }

        // pop up notifikias
        StartCoroutine(ShowAchievementBanner());
    }

    void ActivateAchievementBanner(bool active)
    {
        if (achievementBanner != null)
        {
            achievementBanner.gameObject.SetActive(active);
        }
    }

    IEnumerator ShowAchievementBanner()
    {
        ActivateAchievementBanner(true);
        yield return new WaitForSeconds(2f);
        ActivateAchievementBanner(false);
    }

    IEnumerator RegisterObserver()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (var poi in FindObjectsOfType<PointOfInterest>())
        {
            poi.RegisterObserver(this);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        achievementText = GameObject.Find("Canvas").transform.GetChild(2).GetChild(0).GetComponent<Text>();
    }
}

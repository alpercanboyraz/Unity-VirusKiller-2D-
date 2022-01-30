using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;



public class kontrol : MonoBehaviour
{
    public GameObject[] virus;
    GameObject oyunKontrol;
    public float timer;
    public float firstTime = 1.3f;
    public int score = -1;
    GameObject scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI textmesh;
    public TextMeshProUGUI textmesh2;
    int virusCount ;
    int highScore = 0;
    
    int reklamCount = 0;
    AudioSource sound;

    public object InterstitialVideoControl { get; private set; }

    private void Start()
    {
        olustur();
        oyunKontrol = GameObject.FindGameObjectWithTag("gameover");
        timer = firstTime;
       // textmesh = FindObjectOfType<TextMeshProUGUI>();
        scoreText = GameObject.FindGameObjectWithTag("scoreText");
        highScore = PlayerPrefs.GetInt("kayit");
        sound = GetComponent<AudioSource>();
       
        reklamCount = PlayerPrefs.GetInt("reklamSayac");
        reklamCount++;
        PlayerPrefs.SetInt("reklamSayac", reklamCount);
    }


    public void Update()
    {
       
        timer -= Time.deltaTime;
        var firstAidObject = GameObject.FindWithTag("myVirus");

        if (firstAidObject == null)
        {
            Debug.Log("null");
        }
        else
        {
            var managerComponent = firstAidObject.GetComponent<Clicker>();


            if (managerComponent.control == true)
            {
                sound.Play();
                olustur();
               
                managerComponent.control = false;

                if (virusCount <= 20)
                {
                    timer = firstTime;
                }
                else if(virusCount > 20 && virusCount <= 40  ) {
                    timer = firstTime - 0.2f;
                  }
                else if (virusCount > 40 && virusCount <= 60)
                {
                    timer = firstTime - 0.4f;
                }
                else if (virusCount > 60 && virusCount <= 80)
                {
                    timer = firstTime - 0.6f;
                }
                else if (virusCount > 80 && virusCount <= 100)
                {
                    timer = firstTime - 0.8f;
                }
                else if (virusCount > 100)
                {
                    timer = firstTime - 0.9f;
                }

                textmesh.SetText($"Score : {score.ToString()}");
            }
        }
        if (timer <= 0)
        {
            if (reklamCount == 2)
            {
                var reklam = GameObject.FindGameObjectWithTag("reklam").GetComponent<AdsControl>();
                reklam.ShowInterstitial();
                PlayerPrefs.SetInt("reklamSayac", 0);
                reklamCount = 0;
            }
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("kayit", highScore);
            }
            textmesh2.SetText($"HIGH SCORE : {highScore.ToString()}");
            oyunKontrol.GetComponent<GameOver>().gameover();
            firstAidObject.SetActive(false);
            gameOverPanel.SetActive(true);
           
        }
    }
    void olustur()
    {
        float a = (5/2)-1;
        int nesne ; 
        float x = Random.Range(-a,a );
        float y = Random.Range(-a-1, 3);
        if(virusCount < 30){
            nesne = 0;
        }
        else if(virusCount >= 30 && virusCount < 60)
        {
            nesne = 1;
        }
        else
        {
            nesne = 2;
        }
        virus[nesne].transform.position = new Vector2(x, y);

        Instantiate(virus[nesne], virus[nesne].transform.position, Quaternion.identity);
        score++;
        virusCount++;
    }
    public void playAgain()
    {
    
        SceneManager.LoadScene("GameSCene");

    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

     
}


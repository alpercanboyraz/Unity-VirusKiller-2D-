using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource sound;
    private TextMeshProUGUI textmesh;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.Play();
        textmesh = FindObjectOfType<TextMeshProUGUI>();
        int highScore = PlayerPrefs.GetInt("kayit");
        textmesh.SetText($"HIGH SCORE : {highScore.ToString()}");
    }
}

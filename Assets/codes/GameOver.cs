using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    GameObject myControl;
    GameObject myclick;
    public Animator animator;
    private void Start()
    {
        myControl = GameObject.FindGameObjectWithTag("myControl");
        myclick = GameObject.FindGameObjectWithTag("myVirus");    
    }

    public void gameover()
    {
        animator.SetTrigger("gameOverAnim");
        myControl.GetComponent<kontrol>().enabled = false;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
   
    public bool control = true;
  
    
    public void OnMouseDown()
    {
       
        Destroy(gameObject);
        control = true;
    }


}

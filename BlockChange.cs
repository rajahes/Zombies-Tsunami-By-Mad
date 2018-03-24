using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChange : MonoBehaviour {
    public static bool desblock=false;
    public float number = 4;
    static public float n = 1;
    
   
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.tag == "pchild")
        {
           n++;
           if (n >= number+2) { n = number+2; }
        }

        PlayerController.Intance.grounded = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "pchild")
        {
            if (n <= 1)
                return;
            n--;
         
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (n >= number)
        {
            
            StartCoroutine("destroyer");
           
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(n);
        }

        CanvasController.Intansce.txtBlock.text = n + "/" + number;
    }
    private void LateUpdate()
    {

    }
    IEnumerator destroyer()
    {
        yield return new WaitForSeconds(0.8f);
        if (n >= number)
        {        
            gameObject.SetActive(false);
            PlayerController.Intance.create();
            n = 1;
        }
      
    }
}

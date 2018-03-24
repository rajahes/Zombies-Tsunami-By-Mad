using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PchildController : MonoBehaviour {
    float d;// neu child mac ket hay cach xa se die
    Rigidbody2D Body;
    private void Awake()
    {  
        Body = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.ToLower() == "item")
        {
            PlayerController.Intance.create();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag.ToLower() == "block")
        {
             Destroy(gameObject);
             //PlayerController.Intance.remove(this.Body); 
             PlayerController.Intance.listG.Remove(gameObject);
        }
    }
    private void Update()
    {
        if (gameObject == null)
        {
            return;
        }
        else
        {
            d = PlayerController.thisT.position.x - this.transform.position.x;
        }
       
        if (d > 10f)
        {
            PlayerController.Intance.remove(this.Body);
            PlayerController.Intance.listG.Remove(gameObject);
            Destroy(gameObject);
           // gameObject.SetActive(false);
        }
        if (transform.position.x < PlayerController.Intance.transform.position.x)
        {
            Body.velocity = new Vector3(PlayerController.Intance.speed, Body.velocity.y);
        }
        else
        {
            Body.velocity = new Vector3(-1, Body.velocity.y);
        }        
    }
    //private void FixedUpdate()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        StartCoroutine("changeColider");
    //    }
    //}
    //IEnumerator changeColider()
    //{
    //    yield return new WaitForFixedUpdate();
    //    gameObject.GetComponent<GameObject>().GetComponent<Collider2D>().isTrigger = true;
       
    //}
}

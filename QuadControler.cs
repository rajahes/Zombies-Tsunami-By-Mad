using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadControler : MonoBehaviour {
    public float speed = 0.01f,smooth=5;
    private float offsetX;
    float offer;
    MeshRenderer render;
    Vector3 offer2;
    private float textureOffset = 0;
    public GameObject ObjFollow;
    void Start()
    {
        render = this.GetComponent<MeshRenderer>();
        offsetX = this.transform.position.x - ObjFollow.transform.position.x;
        //offer2 = this.transform.position - PlayerController.Intance.transform.position;
       
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
         float newX = ObjFollow.transform.position.x + offsetX;
        Vector3 pos = this.transform.position;
         pos.x = newX;
        this.transform.position = Vector3.Lerp(transform.position, pos, smooth * Time.deltaTime);
        /*Vector3 new2 = PlayerController.Intance.transform.position + offer2;
        this.transform.position = new2;*/
        textureOffset += speed * Time.deltaTime;
        render.material.mainTextureOffset = new Vector2(textureOffset, 0.01f);
       
        
    }
}

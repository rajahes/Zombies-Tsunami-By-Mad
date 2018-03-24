using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    static PlayerController _intance;
    public static PlayerController Intance
    {
        get
        {
            if (_intance == null)
            {
                _intance = FindObjectOfType<PlayerController>();
            }
            return _intance;
        }
    }
    Rigidbody2D myBD;
    public float speed=2f;
    public float high;
    public bool grounded;
    //child
    public GameObject prefabP;
    public List<Child> listChild = new List<Child>();
    public List<GameObject> listG;
    //
    static public Transform thisT;
	// Use this for initialization
	void Start () {
        _intance = this;
        myBD = GetComponent<Rigidbody2D>();
        listG.Add(this.gameObject);
        listChild.Add(new Child()
        {
            body = myBD
            
        });
	}
    #region Colider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.ToLower() == "item")
        {
            create();
            Destroy(other.gameObject);

        }
        if (other.gameObject.tag.ToLower() == "block")
        {

            if (listChild.Count <= 1)
            {
                GameEnd();
            }
        }
        if (other.tag == "ground")
        {
            grounded = true; //fix loi player treo lo lung ma child cham dat
            stop = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.ToLower() == "ground"||other.gameObject.tag=="blockchage")
        {
            grounded = true;
        }
    }
    #endregion
    #region Create and remove trong list
    public void create() 
    {
        GameObject g = Instantiate(prefabP, this.transform.position + Vector3.left * (listChild.Count + 1) * 0.2f, Quaternion.identity);
        listChild.Add(new Child()
        {          
            body = g.GetComponent<Rigidbody2D>()
        });
        listG.Add(g);
    }
    public void remove(Rigidbody2D c)
    {
        foreach (Child a in listChild.ToList())
        {
            if (a.body == c)
            {
                listChild.Remove(a);
            }
        }
    }
    #endregion
    void GameEnd()
    {
        Time.timeScale = 0;
    }
    // Update is called once per frame
    void Update () {
        myBD.velocity = new Vector3(speed, myBD.velocity.y);
        //if (Input.GetMouseButtonDown(0)&&grounded)
        //{
        //    foreach (Child c in listChild)
        //    {
        //        c.listPostJump.Add(this.transform.position);
        //    }
        //    this.myBD.velocity = new Vector3(myBD.velocity.x, high);
        //    grounded = false;
        //}
       
        foreach (Child c in listChild.ToList())
        {
            c.CheckJump(high);
           // c.Move(speed,this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.LogError(listChild.Count);
        }
        #region Test Jump
        if (Input.GetMouseButton(0))
        {
            mouseUp = false;
            if (!mouseUp && grounded && !stop)
            {
                foreach (Child c in listChild)
                {
                    c.listPostJump.Add(this.transform.position);
                }
                this.myBD.velocity = new Vector3(myBD.velocity.x, high);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            mouseUp = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseUp = true;
        }
        if (this.myBD.transform.position.y >= 4f)
        {
            stop = true;
            mouseUp = true;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("mouse  " + mouseUp + "  Stop  " + stop + "  ground " + grounded);
        }
        #endregion
    }
    //test
    bool stop = false;
    bool mouseUp;
    private void LateUpdate()
    {
        
        CanvasController.Intansce.txtList.text = listG.Count.ToString();
        thisT = this.transform;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.LogError(listChild.Count);
        }
        foreach(var g in listG.ToList())  // kiem tra xem co missing gameobj? ==> xoa g neu missing
        {
            if (!g.gameObject) { listG.Remove(g); }
        }
        foreach(var c in listChild.ToList())// su dung .Tolist() de ko bi loi "InvalidOperationException: Collection was modified; enumeration operation may not execute."
        {
            if (!c.body) { listChild.Remove(c); }
        }
    }
}


public class Child 
{
    
    public Rigidbody2D body;
    public List<Vector3> listPostJump = new List<Vector3>();
    public void CheckJump(float high)
    {
        if (listPostJump.Count == 0) return;
        if (!body)
        {
            return;
        }
        else
        {
            if (body.transform.position.x >= listPostJump[0].x)
            {
                body.velocity = new Vector3(body.velocity.x, high);
                listPostJump.RemoveAt(0);
            }
        }
    }
    
    public void Move(float v,GameObject g)
    {
        if (body.transform.position.x > (g.transform.position.x+0.1f))
        {
            body.velocity = new Vector2(0,body.velocity.y);
        }
        else
        {
            if (!body)
            {
                return;
            }
            else
            {
                body.velocity = new Vector2(v, body.velocity.y);
            }
        }
        
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadScale : MonoBehaviour {
    public float size = 4f;
    // Use this for initialization
    void Start()
    {
        //var worldHeigh = Camera.main.orthographicSize * 2f;
        var worldHeigh = Camera.main.orthographicSize * size;
        //Debug.Log(worldHeigh);
        var worldWitdh = worldHeigh * Screen.width / Screen.height;

        transform.localScale = new Vector3(worldWitdh, worldHeigh, 0f);
    }
}

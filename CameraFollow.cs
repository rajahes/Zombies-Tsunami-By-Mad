using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float smooth;
    private Vector3 offset;
    Vector3 targetCampos;
    public Transform ObjFollow;
    // Use this for initialization
    void Start()
    {
        offset = transform.position - ObjFollow.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetCampos = offset + ObjFollow.position;
        this.transform.position = Vector3.Lerp(transform.position, targetCampos, smooth * Time.deltaTime);
    }
}


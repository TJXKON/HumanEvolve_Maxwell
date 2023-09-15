using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    private GameObject target;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   if (target!=null){

         transform.position = new Vector3 (target.transform.position.x,target.transform.position.y,0f)+ offset;
    }
       
    }

    public void setTarget(GameObject target){
        this.target=target;
    }
}

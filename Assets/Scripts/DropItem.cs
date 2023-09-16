using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public enum ItemTypes{
        style,
        heal,
        others
    }
    public ItemTypes itemType;
    [SerializeField] public string itemName;
    private bool pickable = false;
    private static GameObject thisItem;

    public GameObject pickupEffect;




    // Update is called once per frame
    void Update()
    {
        if (pickable){
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickUp();
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            pickable = true;
            Debug.Log("Press E to pick up");
        }
    }

        void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            pickable = false;
        }
    }


    void pickUp(){
        if (itemType == ItemTypes.style){
            FindObjectOfType<PlayerStatusManager>().style=itemName;
            Debug.Log("Player style changed to "+itemName);

            

            GameObject g = GameObject.Find("GroundCheck");
            GameObject go = Instantiate(pickupEffect,new Vector3 (g.transform.position.x,g.transform.position.y,0f), Quaternion.identity);
            //go.transform.parent = GameObject.Find("Player").transform;
            go.GetComponent<FollowObject>().setTarget(g);
            Destroy(go,0.8f);
        }
        thisItem = gameObject;
        Destroy(thisItem);
    }

    void OnDestroy(){
        if (thisItem == gameObject){
            thisItem = null;
        }
    }

}

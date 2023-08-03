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


    void pickUp(){
        if (itemType == ItemTypes.style){
            FindObjectOfType<PlayerStatusManager>().style=itemName;
            Debug.Log("Player style changed to "+itemName);
        }

        Destroy(this.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StyleIcon : MonoBehaviour
{
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
         ChangeStyleIcon(GameObject.Find("Player").GetComponent<PlayerStatusManager>().style);
    }

    public void ChangeStyleIcon(string style){
        switch (style)
        {
            case("Normal"):
                image.color = Color.black;
                break;
            case("Fire"):
                image.color = Color.red;
                break;
            case("Laser"):
                image.color = Color.cyan;
                break;
            case("Gun"):
                image.color = Color.blue;
                break;
            case("Magic"):
                image.color = Color.green;
                break;
            default:
                return;
        }
    }
}

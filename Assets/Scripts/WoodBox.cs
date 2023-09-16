using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBox : MonoBehaviour
{
    public void OnPlayerAttack()
    {
        // Perform actions when player attacks the box
        Destroy(gameObject); // Destroy the box GameObject
    }
}

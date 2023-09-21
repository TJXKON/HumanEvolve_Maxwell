using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
        public Text scoreText;

        public int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText=GameObject.Find("Score").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString()+" pts";
    }

    public void addScore(int i){
        score+=i;
    }
}

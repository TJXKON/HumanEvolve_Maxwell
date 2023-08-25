using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{


    public int maxHP = 100;
    [HideInInspector] public int currentHP;
    public bool Iframe = false;
    private HpBar hpBar;
    private float IDuration = 0.6f;

    [SerializeField] private GameObject sprite;
    [SerializeField] private Material flashMaterial;

    private float flashtime = 0.12f;
    SpriteRenderer sr;
    Material defaultMaterial;

    public string style = "Normal";

    void Start(){
        currentHP = maxHP;
        sr = sprite.GetComponent<SpriteRenderer>();
        defaultMaterial=sr.material;
        //hpBar = GameObject.Find("Hp Bar").GetComponent<HpBar>();
        hpBar = FindObjectOfType<HpBar>();
        hpBar.setMaxHp(maxHP);
    }
    void Update() {
        if (currentHP<=0){
            FindObjectOfType<GameManager>().gameOver();
        }
        hpBar.setHp(currentHP);

        if(Iframe){
            
            if (IDuration>0f){
                IDuration-=Time.deltaTime*1f;
            }   else{
                Iframe=false;
                IDuration = 0.6f;
            }
        }
    }

    public void takeDamage(int dmg){
        currentHP-=dmg;
        Iframe=true;
        StartCoroutine(flash());

    }
    IEnumerator keepFlash(){
        yield return new WaitForSeconds(flashtime);
        if (Iframe){
            StartCoroutine(flash());
        }
        
    }
    IEnumerator flash(){
        sr.material = flashMaterial;
        Debug.Log("flash");
        yield return new WaitForSeconds(flashtime);
        sr.material = defaultMaterial;
        if (Iframe){
            StartCoroutine(keepFlash());
        }
    }





}

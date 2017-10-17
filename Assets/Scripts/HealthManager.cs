﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    
    Rigidbody2D myRigidbody;

    public float KnockBackX;
    public float KnockBackY;

	public static HealthManager healthManager;

    private Animator playerAnim;
    public float animDelay;

    public int playerHealth;
    public int enemyDamage;
	public GameObject player;
    public static bool playerDead;

	public bool invincible;

    // Use this for initialization
    void Start () {
        playerDead = false;
        myRigidbody = GetComponent<Rigidbody2D>();
		healthManager = this;
        playerAnim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag ("Player");
    }
		

    void OnTriggerEnter2D(Collider2D col)
    {
		if (!invincible) 
		{
			if(col.tag == "Enemigo")
			{
				playerHealth -= enemyDamage;
				if(playerHealth > 0)
				{
					StartCoroutine ("color");
					invincible = true;
					StartCoroutine ("tiempoEspera");
					if (col.GetComponent<SpriteRenderer>().flipX == false)
					{
						myRigidbody.velocity = new Vector2(-KnockBackX, KnockBackY);
					}
					else
					{
						myRigidbody.velocity = new Vector2(KnockBackX, KnockBackY);
					}
				}
				else
				{
                    GetComponent<BoxCollider2D>().enabled = false;
                    playerDead = true;
                    playerAnim.SetBool("isDead", true);
                    Component[] comps = GetComponents<Component>() as Component[];
                    foreach(Component comp in comps)
                    {
                        if (comp != gameObject.GetComponent<Transform>())
                        {
                        Destroy(comp,animDelay);
                        }
                    }

                }
			}
		}

    }
	/*
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Enemigo")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
*/
	IEnumerator tiempoEspera() {
		yield return new WaitForSeconds (1.2f);
		invincible = false;
	}

	IEnumerator color(){
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (0.05f);
		GetComponent<SpriteRenderer>().color = Color.white;
		yield return new WaitForSeconds (0.05f);
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (0.05f);
		GetComponent<SpriteRenderer>().color = Color.white;
	}

}
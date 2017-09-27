﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool tocandoPiso;
    public float fuerzaSalto = 6.5f;
    
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private bool jump;
	private bool seAgacha;
	private bool disparo;



	// Use this for initialization
	void Start () {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        // Hacemos que cambie el sprite pero comprobando siempre contra un valor positivo (0.1).
        // Por eso usamos Abs (valor absoluto)

        myAnimator.SetFloat("Speed", Mathf.Abs(myRigidbody2D.velocity.x));
        myAnimator.SetBool("TocandoPiso", tocandoPiso);
		myAnimator.SetBool ("SeAgacha", seAgacha);
		myAnimator.SetBool ("Disparo", disparo);

		if (Input.GetKey (KeyCode.DownArrow) && tocandoPiso) {
			seAgacha = true;
		} else 
			 {
			 	seAgacha = false;
			 }


        if (Input.GetKeyDown(KeyCode.UpArrow) && tocandoPiso)
        {
            jump = true;
        }

		if (Input.GetKeyDown (KeyCode.Space)) {
			disparo = true;

		} else if (Input.GetKeyUp (KeyCode.Space)) {

			disparo = false;
		}


			
	}

    private void FixedUpdate()
    {

        Vector3 fixedVelocity = myRigidbody2D.velocity;
        fixedVelocity.x *= 0.75f;

        if (tocandoPiso)
        {
            myRigidbody2D.velocity = fixedVelocity;
        }

        // Lo de arriba es para solucionar que no se mueva siempre, ya que pusimos
        // que las plataformas no tengan fricción.

        float direccion = Input.GetAxis("Horizontal");


        myRigidbody2D.AddForce(Vector2.right * speed * direccion);

        // Clamp toma un valor y le aplica un filtro (un valor mínimo y un valor máximo)
        float limiteVelocidad = Mathf.Clamp(myRigidbody2D.velocity.x, -maxSpeed, maxSpeed);
        myRigidbody2D.velocity = new Vector2(limiteVelocidad, myRigidbody2D.velocity.y);

        if (direccion > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (direccion < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (jump)
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, 0); // Para que cancele la velocidad vertical y no se produzcan "saltos dobles"
            myRigidbody2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            jump = false;
        }
			
    }

    private void OnBecameInvisible()
        // Sólo para las pruebas
    {
        transform.position = new Vector3(-7, 0, 0);
    }
}
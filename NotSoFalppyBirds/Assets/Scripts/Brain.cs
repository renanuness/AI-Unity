using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    int DNALength = 5;

    public DNA dna;
    public GameObject eyes;
    public float timeAlive = 0;
    public float distanceTraveled = 0;
    public int crash = 0;

    private bool seeDownWall = false;
    private bool seeUpWall = false;
    private bool seeBottom = false;
    private bool seeTop = false;
    private Vector3 startPosition;
    private bool alive = true;
    private Rigidbody2D rb;

    public void Init()
    {
        dna = new DNA(DNALength, 200);
        transform.Translate(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "top" ||
            col.gameObject.tag == "bottom" ||
            col.gameObject.tag == "upwall" ||
            col.gameObject.tag == "downwall")
        {
            crash++;
        }
        else if(col.gameObject.tag == "dead")
        {
            alive = false;
        }
    }

    private void Update ()
    {
        if (!alive) return;

        seeUpWall = false;
        seeDownWall = false;
        seeBottom = false;
        seeTop = false;
        RaycastHit2D hit = Physics2D.Raycast(eyes.transform.position, eyes.transform.forward, 1.0f);

        if(hit.collider != null)

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {

    //genes
    public float R;
    public float G;
    public float B;
    public float S;
    
    public float TimeToDie = 0;

    private bool _dead = false;
    private SpriteRenderer _spriteRendere;
    private Collider2D _collider2D;

	private void Start () {
        _spriteRendere = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _spriteRendere.color = new Color(R,G,B);
        _collider2D.transform.localScale = new Vector3(S, S, 0);
	}
	
	private void Update () {
		
	}

    void OnMouseDown()
    {
        _dead = true;
        TimeToDie = PopulationManager.TimeElapsed;
        _spriteRendere.enabled = false;
        _collider2D.enabled = false;
    }

}

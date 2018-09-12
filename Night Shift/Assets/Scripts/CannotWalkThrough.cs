using UnityEngine;
using System.Collections;

public class CannotWalkThrough : MonoBehaviour {

    public SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f,0.5f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

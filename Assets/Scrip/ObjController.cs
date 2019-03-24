using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController : MonoBehaviour {


	public Sprite[] sprites =new Sprite[5];
	private SpriteRenderer sprite;
	public int defaultSprite = 0;
	public PhysicsMaterial2D material;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		sprite.sprite = sprites[defaultSprite];
		Destroy(GetComponent<PolygonCollider2D>());
		PolygonCollider2D polygon = gameObject.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
		polygon.sharedMaterial = material;
	}


	public void  changeSprite(){
		sprite.sprite = sprites[defaultSprite];
		Destroy(GetComponent<PolygonCollider2D>());
		PolygonCollider2D polygon = gameObject.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
		polygon.sharedMaterial = material;
	}
	// Update is called once per frame
	void Update () {
		
	}
}

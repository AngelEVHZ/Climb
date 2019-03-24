using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController : MonoBehaviour {


	
	private SpriteRenderer spriteR;
	public Sprite sprite;

	public PhysicsMaterial2D material;
	// Use this for initialization
	void Start () {
		spriteR = GetComponent<SpriteRenderer> ();
		spriteR.sprite = sprite;
		Destroy(GetComponent<PolygonCollider2D>());
		PolygonCollider2D polygon = gameObject.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
		polygon.sharedMaterial = material;
	}

	public void setSprite(Sprite sprite_){
		sprite = sprite_;
	}

	public void  updateCollider(){
		spriteR.sprite = sprite;
		Destroy(GetComponent<PolygonCollider2D>());
		PolygonCollider2D polygon = gameObject.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
		polygon.sharedMaterial = material;
	}
	// Update is called once per frame
	void Update () {
		
	}
}

﻿using UnityEngine;

//by Niklas Bachmann
//10.08.2017

public class Projectile : MonoBehaviour {

	private Transform target;

	public float speed = 20f;

	public float explosionRadius = 0f;

	public int damage = 1;

	public GameObject impactEffekt;


	public void Seek (Transform _target) {

		target = _target;

	}
		

	void Update () {

		if(target == null){

			Destroy (gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame) {

			HitTarget ();
			return;

		}

		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt (target);

	}


	void HitTarget (){

		GameObject effectIns = Instantiate (impactEffekt, transform.position, transform.rotation);

		Destroy (effectIns, 2f);

		if (explosionRadius > 0f) {
		
			Explode ();
		
		} else {

			Damage (target);

		}

		Destroy (gameObject);

	}

	void Explode () {
	
		Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRadius);

		foreach (Collider collider in colliders) {

			if (collider.tag == "Enemy") {
				Damage (collider.transform);
			}
			
		}
	
	}

	void Damage (Transform enemy) {

		enemy.GetComponent<Enemy> ().SubHitPoints (damage);

	}


	void OnDrawGizmosSelected () {
	
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, explosionRadius);
	
	}

}
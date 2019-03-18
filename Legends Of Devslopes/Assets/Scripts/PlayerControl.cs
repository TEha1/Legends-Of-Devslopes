using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerControl : MonoBehaviour {

	[SerializeField] private float playerSpeed = 10f;
	CharacterController characterController;

	[SerializeField] private LayerMask layerMask;
	private Vector3 currentLookTarget = Vector3.zero;

	private Animator anim;
	private BoxCollider[] boxCollider;
 
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		boxCollider = GetComponentsInChildren<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.GameOver()) {
			
			Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			characterController.SimpleMove (moveDirection * playerSpeed);

			if (moveDirection == Vector3.zero) {
				anim.SetBool ("IsWalking", false);
			} else {
				anim.SetBool ("IsWalking", true);
			}

			if (Input.GetMouseButtonDown (0)) {
				anim.Play ("DoubleChop");
			}
			if (Input.GetMouseButtonDown (1)) {
				anim.Play ("SpinAttack");
			}
		}
	}

	void FixedUpdate() {
		if (!GameManager.instance.GameOver ()) {
			// hna da kolo 3l4an 23ml RAY bytl3 mn L CAMERA li L POINT bta3t L mouse --> 3l4an 23rf 2t7km fi PLAYER mn 5lal L MOUSE
			RaycastHit hit; // hna byrg3 L POINT elli nzl 3leha 4o3a3 L CAMERA --> fi L (hit)
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); // hna bt7dd n L RAY hytl3 mn L MAIN_CAMERA w hyro7 li MOUSE_POINT 
			Debug.DrawRay (ray.origin, ray.direction, Color.blue); // hna bdi l RAY COLOR
			if (Physics.Raycast (ray, out hit/*RAY direction*/, 500/*maxDistance*/, layerMask, QueryTriggerInteraction.Ignore)) { // hna b2a byt CREATE l RAY 
				if (hit.point != currentLookTarget) {
					currentLookTarget = hit.point;
				}
				Vector3 targetPosition = new Vector3 (hit.point.x, transform.position.y, hit.point.z); // hna bt7dd L Direction elli hy2dr L Mouse y3ml ROTATIOn feha 
				Quaternion rotation = Quaternion.LookRotation (targetPosition - transform.position);
				transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * 10f);
			}
		}
	}

	public void HeroBeginAttack01 () {
		foreach (var weapon in boxCollider) {
			weapon.enabled = true;
		}
	}
	public void HeroEndAttack01 () {
		foreach (var weapon in boxCollider) {
			weapon.enabled = false;
		}
	}
	public void HeroBeginAttack02 () {
		foreach (var weapon in boxCollider) {
			weapon.enabled = true;
		}
	}
	public void HeroEndAttack02 () {
		foreach (var weapon in boxCollider) {
			weapon.enabled = false;
		}
	}

}

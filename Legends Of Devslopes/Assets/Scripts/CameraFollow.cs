using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour {

	[SerializeField] Transform target;
	private float smoothing_t = 5f; 
	Vector3 offset;

	void Awake() {
		Assert.IsNotNull (target);
	}

	// Use this for initialization
	void Start () {
		offset = transform.position - target.position; // hna tr7t L mkan elli wa2fa feh L Camera mn mkan L target --> 3l4an 2geb L msafa benhom
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetCameraPosition = offset + target.position;// hna b3rfo L msafa elli beno w ben L target --> 3l4an L camera tb2a 3arfa L msafa w mkan L target elli hyb2a wrah 3ltool
		transform.position = Vector3.Lerp (transform.position, targetCameraPosition, smoothing_t * Time.deltaTime); // hna b2a L position bta3 L Camera hyb2a L output bta3 [ Vector3.Lerp ] --> w Lerp di hya elli bt5li L Camera tt7rk wra L target b Smoothly --> 3n tre2 ni bdeha L START_POINT w L END_POINT w L smoothing_t
	
	}


}

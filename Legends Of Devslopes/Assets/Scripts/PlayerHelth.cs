using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHelth : MonoBehaviour {

	[SerializeField] float startingHealth = 100f;
	[SerializeField] float timeSinceLastHit = 2f;
	[SerializeField] private Slider slider;

	private Animator anim;
	private CharacterController chaControl;
	private AudioSource playerAudioHited;
	private float timer;
	private float currentHealth;
	private ParticleSystem bloodFlow;

	void Awake() {
		Assert.IsNotNull (slider);
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		chaControl = GetComponent<CharacterController> ();
		playerAudioHited = GetComponent<AudioSource> ();
		currentHealth = startingHealth;
		bloodFlow = GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
	}

	void OnTriggerEnter (Collider other) { // di function m7goza w (it's called automatically)
		if (timer > timeSinceLastHit/*di 3l4an by2a fi w2t ben kol (Hit) w L tanya*/ && !GameManager.instance.GameOver ()) {
			if (other.tag == "weapon") {
				takeHit ();
				timer = 0; // ba (reset) L (timer) b3d kol drpa .. w yrg3 L (timer) y3d tani fi L (Update) fo2 
			}
		}
	}

	void takeHit () {
		GameManager.instance.PlayerHit (currentHealth);
		if (!GameManager.instance.GameOver ()) {
			anim.Play ("Hurt");
			currentHealth -= 10;
			slider.value = currentHealth;
			playerAudioHited.PlayOneShot (playerAudioHited.clip);
			bloodFlow.Play ();
		} else {
			playerDie ();
		}
	}

	void playerDie () {
		anim.Play ("Die");
		playerAudioHited.PlayOneShot (playerAudioHited.clip);
		chaControl.enabled = false;
	}

}

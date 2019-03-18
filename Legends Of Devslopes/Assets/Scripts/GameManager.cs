using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	[SerializeField] public GameObject Player;
	[SerializeField] private GameObject Solider;
	[SerializeField] private GameObject Ranger;
	[SerializeField] private GameObject Tanker;
	[SerializeField] private GameObject[] SpawmPoints;
 	[SerializeField] private Text textLevel;

	private bool gameOver = false;
	private float currentSpawmTime = 0;
	private float timeToGenerateSpwam = 1;
	private int currentLevel;
	private GameObject newEnemy;

	List<EnemyHealth> enemyRegisterd = new List<EnemyHealth> ();
	List<EnemyHealth> enemyKilled = new List<EnemyHealth> ();

	public void RegisteredEnemy (EnemyHealth enemy) {
		enemyRegisterd.Add (enemy);
	}

	public void KilledEnemy (EnemyHealth enemy) {
		enemyKilled.Add (enemy);
	}

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
		Assert.IsNotNull (Player);
		Assert.IsNotNull (Solider);
		Assert.IsNotNull (Ranger);
		Assert.IsNotNull (Tanker);
		Assert.IsNotNull (textLevel);
		Assert.IsNotNull (SpawmPoints);
	}

	// Use this for initialization
	void Start () {
		currentLevel = 1;
		StartCoroutine (spawm ());
	}
	
	// Update is called once per frame
	void Update () {
		currentSpawmTime += Time.deltaTime;
	}

	public bool GameOver () {
		return gameOver;
	}

	public void PlayerHit (float currentHP) {
		if (currentHP > 0) {
			gameOver = false;
		} else {
			gameOver = true;
		}
	}

	IEnumerator spawm() {
		
		if (currentSpawmTime > timeToGenerateSpwam) {
			currentSpawmTime = 0;

			if (enemyRegisterd.Count < currentLevel) {
				int SpawmLocation = Random.Range (0, SpawmPoints.Length - 1);
				GameObject enemyLocation = SpawmPoints [SpawmLocation];
				int randomEnemy = Random.Range (0, 3);
				if (randomEnemy == 0) {
					newEnemy = Instantiate (Ranger) as GameObject;
				} else if (randomEnemy == 1) {
					newEnemy = Instantiate (Solider) as GameObject;
				} else if (randomEnemy == 2) {
					newEnemy = Instantiate (Tanker) as GameObject;
				}/*
				switch (randomEnemy) {
				case 0:
					newEnemy = Instantiate (Ranger) as GameObject;
					break;
				case 1:
					newEnemy = Instantiate (Solider) as GameObject;
					break;
				case 2:
					newEnemy = Instantiate (Tanker) as GameObject;
					break;
				}*/
				newEnemy.transform.position = enemyLocation.transform.position;
			}
			if (enemyKilled.Count == currentLevel) {
				enemyRegisterd.Clear();
				enemyKilled.Clear();
				yield return new WaitForSeconds (3f);
				currentLevel++;
				textLevel.text = "Level " + currentLevel;
			}
		}

		yield return null;
		StartCoroutine (spawm ());
	}



}

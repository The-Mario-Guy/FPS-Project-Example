using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Enemies : MonoBehaviour
{
	public TextMeshProUGUI enemyCount;
	// Use this for initialization
	int enemiesLeft = 0;
	bool killedAllEnemies = false;
	void Start()
	{
		enemiesLeft = 10; // or whatever;
		enemyCount.text = enemiesLeft.ToString();
	}

	// Update is called once per frame
	void Update()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyBullet");
		enemiesLeft = enemies.Length;
		enemyCount.text = enemiesLeft.ToString();
		if (enemiesLeft == 0)
		{
			endGame();
		}
	}

	void endGame()
	{
		killedAllEnemies = true;
		SceneManager.LoadScene(4);
	}

	void OnGUI()
	{
		/*if (killedAllEnemies)
		{
			GUI.Label(new Rect(0, 0, 200, 20), "all gone");
		}
		else
		{
			GUI.Label(new Rect(0, 0, 200, 20), "Enemies Remaining : " + enemiesLeft);
		}*/
	}
}

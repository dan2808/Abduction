using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text abductedHumans;
    PlayerController playerControllerScript;

    [SerializeField] GameObject uFO;

    bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = uFO.GetComponent<PlayerController>();
        playerControllerScript.OnPlayerDeath += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }

        }

    }

    void OnGameOver()
    {
        gameOverScreen.SetActive(true);
        abductedHumans.text = playerControllerScript.humansAbducted.ToString();
        gameOver = true;
    }
}

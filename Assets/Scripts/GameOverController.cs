using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        int playerScore = GameController.Instance.scoresList[0];
        int computerScore = GameController.Instance.scoresList[1];

        if(playerScore > computerScore) {
            resultText.text = "You Won!";
        } else if(playerScore < computerScore) {
            resultText.text = "Computer Won!";
        } else {
            resultText.text = "Draw!";
        }
        
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MainMenu() {
        SceneManager.LoadScene("MainMenuScene");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<Collider2D> pocketList;
    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;

    private enum Player {
        Player1,
        Player2,
    }

    private int whitePoints = 1;
    private int blackPoints = 1;
    private int queenPoints = 2;

    private Player activePlayer = Player.Player1;

    private List<int> scoresList = new List<int>();

    private void Awake() {
        scoresList.Add(0);
        scoresList.Add(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(Collider2D pocket in pocketList) {
            pocket.GetComponent<Pockets>().OnPocketTriggered += OnPiecePocketed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPiecePocketed(object sender, Pockets.OnPocketTriggeredEventArgs e)
    {
        GameObject piece = e.piece;

        switch(e.piece.tag) {
            case "White" :
                UpdateScore(piece, whitePoints);
                break;

            case "Black" :
                UpdateScore(piece, blackPoints);
                break;

            case "Queen" :
                UpdateScore(piece, queenPoints);
                break;
        }
    }

    private void UpdateScore(GameObject piece, int points) {
        switch (activePlayer) {
                    case Player.Player1:
                        scoresList[0] += points;
                        player1ScoreText.text = scoresList[0].ToString();
                        break;
                    case Player.Player2:
                        scoresList[1] += points;
                        player2ScoreText.text = scoresList[1].ToString();
                        break;
                }
                Destroy(piece);
    }
}

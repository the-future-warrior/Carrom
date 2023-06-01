using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private List<Collider2D> pocketList;
    [SerializeField] Rigidbody2D striker;   
	[SerializeField] Rigidbody2D[] pucks;
    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;
    [SerializeField] private Image player1Image;
    [SerializeField] private Image player2Image;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Slider strikerSlider;
    [SerializeField] private GameObject pocketParticles;

    public enum Player {
        Player1,
        Player2,
    }

    private int whitePoints = 1;
    private int blackPoints = 1;
    private int queenPoints = 2;

    public Player activePlayer = Player.Player1;

    public List<int> scoresList = new List<int>();
    public bool resetDone = true;

    private void Awake() {
        Instance = this;
        scoresList.Add(0);
        scoresList.Add(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(Collider2D pocket in pocketList) {
            pocket.GetComponent<Pockets>().OnPocketTriggered += OnPiecePocketed;
        }

        
        player1Image.color = Color.green;
        player2Image.color = Color.white;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
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

         Instantiate(pocketParticles, e.transform.position, Quaternion.identity);
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
        piece.SetActive(false);
    }

    private void LateUpdate() {
        //Debug.Log(resetDone);
        if (!resetDone && striker.IsSleeping())
		{
			foreach (var p in GameAI.Instance.pucks)
				if (!p.IsSleeping() && p.gameObject.activeSelf) {
                    resetDone = false;
                    return;
                }
					
            striker.GetComponent<Striker>().ResetStriker();
			resetDone = true;
            SwitchPlayer();
		}
        
    }

    private void SwitchPlayer()
    {
        switch (activePlayer) {
            case Player.Player1:
                activePlayer = Player.Player2;
                player2Image.color = Color.green;
                player1Image.color = Color.white;
                arrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                StartCoroutine(WaitForFunction());
                break;
            case Player.Player2:
                activePlayer = Player.Player1;
                player1Image.color = Color.green;
                player2Image.color = Color.white;
                arrow.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                strikerSlider.gameObject.SetActive(true);
                strikerSlider.value = 0f;
                break;
        }
    }

    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(2);
        GameAI.Instance.Calc_BestShot(2);
    }
}

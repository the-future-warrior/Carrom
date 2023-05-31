using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Striker : MonoBehaviour
{
    [SerializeField] private Slider strikerSlider;
    [SerializeField] private Transform arrow;
    [SerializeField] private Transform circle;
    [SerializeField] private Transform stikerVisual;
    private Rigidbody2D rb;
    private Vector3 oldPosition;

    private float forceMagnitude = 0.0f;
    private float forceMultiplier = 7.0f;
    private Vector3 targetDirection;
    private bool shotTaken = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        strikerSlider.onValueChanged.AddListener(onSliderChanged);
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if(transform.position == oldPosition && shotTaken) {
        //     ResetStriker();
        // } else {
        //     oldPosition = transform.position;
        // }
    }

    public void onSliderChanged(float xPos) {
        transform.position = new Vector2(xPos, -15.6f);
    }

    private void OnMouseDown()
    {
        //GetComponent<SpriteRenderer>().color = Color.red;
        //GetComponent<LineRenderer>().enabled = true;
        if(GameController.Instance.activePlayer == GameController.Player.Player1 && GameController.Instance.resetDone) {
            arrow.gameObject.SetActive(true);
            circle.gameObject.SetActive(true);
        }
    }

    private void OnMouseUp()
    {
        if(GameController.Instance.activePlayer == GameController.Player.Player1 && GameController.Instance.resetDone) {
            arrow.gameObject.SetActive(false);
            circle.gameObject.SetActive(false);
            arrow.transform.rotation = Quaternion.identity;

            rb.AddForce(forceMagnitude * targetDirection * forceMultiplier);
            GameController.Instance.resetDone = false;
            strikerSlider.gameObject.SetActive(false);
        }
    }

    private void OnMouseDrag()
    {
        if(GameController.Instance.activePlayer == GameController.Player.Player1 && GameController.Instance.resetDone) {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetDirection = arrow.transform.position - newPosition;

        
            forceMagnitude = targetDirection.magnitude * 1f;
            circle.transform.localScale = new Vector3(forceMagnitude, forceMagnitude, forceMagnitude);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, Quaternion.LookRotation(targetDirection, transform.forward).eulerAngles.z-180);
        }

    }

    public void ResetStriker() {
        float yPos = GameController.Instance.activePlayer == GameController.Player.Player1 ? 15.6f : -15.6f;
        transform.position = new Vector3(0f, yPos, 0f);
    }
}


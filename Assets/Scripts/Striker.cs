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

    private float forceMagnitude = 0.0f;
    private float forceMultiplier = 5.0f;
    private Vector3 targetDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        strikerSlider.onValueChanged.AddListener(onSliderChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onSliderChanged(float xPos) {
        transform.position = new Vector2(xPos, transform.position.y);
    }

    private void OnMouseDown()
    {
        //GetComponent<SpriteRenderer>().color = Color.red;
        //GetComponent<LineRenderer>().enabled = true;

        arrow.gameObject.SetActive(true);
        circle.gameObject.SetActive(true);
    }

    private void OnMouseUp()
    {
        arrow.gameObject.SetActive(false);
        circle.gameObject.SetActive(false);

        rb.AddForce(forceMagnitude * targetDirection * forceMultiplier);
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetDirection = transform.position - newPosition;

    
        forceMagnitude = targetDirection.magnitude * 1f;
        circle.transform.localScale = new Vector3(forceMagnitude, forceMagnitude, forceMagnitude);
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, Quaternion.LookRotation(targetDirection, transform.forward).eulerAngles.z-180);

    }
}


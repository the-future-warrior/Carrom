using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pockets : MonoBehaviour
{
    public event EventHandler<OnPocketTriggeredEventArgs> OnPocketTriggered;
    public class OnPocketTriggeredEventArgs : EventArgs {
        public GameObject piece;
        public Transform transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.layer == 9) {
            OnPocketTriggered?.Invoke(this, new OnPocketTriggeredEventArgs {
                piece = collider.gameObject,
                transform = collider.transform
            });
        }
    }
}

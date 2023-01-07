using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour
{
    // turns bark on every 0.5 seconds,
    // if the Bark button (space and left click) is pressed
    public enum Barks {Stop = 0, Go = 1, NumberOfBarks = 2}
    
    public SpriteRenderer barkRadius;
    [SerializeField] protected bool barking = false;
    public bool IsBarking { // barking is read-only
        get {return barking;}
    }

    protected Barks barkState = Barks.Go;
    public Barks BarkState {
        get {return barkState;}
    }
    private float barkDelta = 0f;
    private float nextBark = 0.5f;
    private float myTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        myTime = myTime + Time.deltaTime;
        if (Input.GetButton("Bark") && myTime > nextBark) { // if space is being pressed

            nextBark = myTime + barkDelta;
            barking = true;
            // animate bark
            barkRadius.color = new Color(barkRadius.color.r, barkRadius.color.g, barkRadius.color.b, 0.8f);
            nextBark = nextBark - myTime;
            myTime = 0.0f;
            // Debug.Log("Barking: " + barking);
        } else {
            barking = false;
            barkRadius.color = new Color(barkRadius.color.r, barkRadius.color.g, barkRadius.color.b, 1.0f);
            // Debug.Log("Barking: " + barking);
        }

        // if (Input.GetButtonDown("SwitchBark")) {
        //     if (barkState == Barks.Stop) { // definitely a better way to do this
        //         barkState = Barks.Go;
        //         barkRadius.color = Color.green;
        //     } else if (barkState == Barks.Go) {
        //         barkState = Barks.Stop;
        //         barkRadius.color = Color.blue;
        //     }
        // }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour
{
    // turns bark on every 0.5 seconds,
    // if the Bark button (space and right click) is pressed
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

    // default right now is a cyan blue aura
    private Color defaultColor = new Color(0.0f, 0.8571167f, 1.0f, 1.0f);

    // Update is called once per frame
    void Update()
    {
        myTime = myTime + Time.deltaTime;
        if (Input.GetButton("Bark") && myTime > nextBark) { // if space is being pressed

            nextBark = myTime + barkDelta;
            barking = true;
            // animate bark
            // changes to a red aura
            barkRadius.color = new Color(barkRadius.color.r + 1.0f, barkRadius.color.g - 0.7f, barkRadius.color.b - 1.0f, 0.8f);
            nextBark = nextBark - myTime;
            myTime = 0.0f;
            // Debug.Log("Barking: " + barking);
        } else {
            barking = false;
            barkRadius.color = defaultColor;
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

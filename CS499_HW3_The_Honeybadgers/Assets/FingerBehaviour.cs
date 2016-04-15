using UnityEngine;
using System.Collections;
using CS499;

public abstract class FingerBehaviour : MonoBehaviour {
    public FingerID finger;
    public Color RandomColor;
    float addSub = 0.01f;

    protected Ray fingerRay {
        get {
            return HandWrapper.inst.GetFingerRay(finger);
        }
    }

	// Use this for initialization
	void Start () {
        RandomColor = new Color(0,0,0);
    }

    // Update is called once per frame
    protected virtual void Update () {
       
        if (HandWrapper.inst.handCount == 0 && finger != FingerID.Emulated) return;
        transform.position = fingerRay.origin;
        transform.rotation = Quaternion.LookRotation(fingerRay.direction, Vector3.up);
        FingerUpdate();
	}

    protected void AscendingColor()
    {
        if (RandomColor[0] >= 0.0f && RandomColor[0] <= 1.0f)
        {
            RandomColor[0] += addSub;
        }
        else if (RandomColor[1] >= 0.0f && RandomColor[1] <= 1.0f)
        {
            RandomColor[1] += addSub;
        }
        else if (RandomColor[2] >= 0.0f && RandomColor[2] <= 1.0f)
        {
            RandomColor[2] += addSub;
        }


        if (RandomColor[0] >= 1.0f && RandomColor[1] >= 1.0f && RandomColor[2] >= 1.0f)
        {
            RandomColor[0] = 1.0f;
            RandomColor[1] = 1.0f;
            RandomColor[2] = 1.0f;

            addSub = -0.01f;
        }
        else if (RandomColor[0] <= 0.0f && RandomColor[1] <= 0.0f && RandomColor[2] <= 0.0f)
        {
            RandomColor[0] = 0.0f;
            RandomColor[1] = 0.0f;
            RandomColor[2] = 0.0f;

            addSub = 0.01f;
        }
    }

    protected void FingerUpdate()
    {
        //Color RandomColor = new Color(Random.value, Random.value, Random.value); //each tick is a random color

        Ray r = fingerRay;
        RaycastHit hit;
        if (Physics.Raycast(r, out hit))
        {
            Vector3 pnt = hit.point;
            ColoringWall cw = hit.collider.GetComponent<ColoringWall>();
            if (HandWrapper.inst.handCount != 0 && finger != FingerID.Emulated)
            {
                AscendingColor(); //the color changes the more you draw

                //Debug.DrawLine(pnt, Vector3.forward * 8,RandomColor);
                Debug.Log(RandomColor);
                cw.SetColor(pnt, RandomColor, 0.4f);
            }
        }
    }
}

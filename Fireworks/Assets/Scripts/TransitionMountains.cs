using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionMountains : MonoBehaviour
{
	public static TransitionMountains instance;

    List<SpriteRenderer> mountains;
    public float t;
    float secondsToMidnight;
    public Color[] transitions;
    
    int numTransitions;
    public float interval;
    public float targetInterval;

    // Start is called before the first frame update
    void Start()
    {
		if (instance) {
			Destroy(instance);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

        numTransitions = transitions.Length;
        mountains = new List<SpriteRenderer>();
        int i = 0;
        foreach (Transform child in transform) {
            if (child.GetComponent<SpriteRenderer>()) {
                child.position -= Vector3.forward * i++;
                mountains.Add(child.GetComponent<SpriteRenderer>());
            }
        }   

        targetInterval = 0f;
        interval = 1f / FindObjectOfType<TransitionSky>().skyCount;
        secondsToMidnight = FindObjectOfType<TransitionSky>().secondsToMidnight;
    }

    public void TriggerTransition () {
        targetInterval += interval * 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) {
            TriggerTransition();
        }
        int index = Mathf.FloorToInt(t * numTransitions);
        Color c1 = transitions[index];
        Color c2 = transitions[index+1];
        
        foreach (SpriteRenderer mountain in mountains) {
            print(t * numTransitions - Mathf.Floor(t * numTransitions));
            mountain.color = Color.Lerp(c1, c2, t * numTransitions - Mathf.Floor(t * numTransitions));
        }    

        if (t < targetInterval) {
            t += Time.deltaTime / secondsToMidnight;
        } else {
            t = targetInterval;
        }
    }
}

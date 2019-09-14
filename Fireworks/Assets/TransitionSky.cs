using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSky : MonoBehaviour
{
    public float t = 0f;
    public float secondsToMidnight = 60f;
    List<SpriteRenderer> skies;
    [HideInInspector] public int skyCount;
    float diff;
    float interval;
    float targetInterval;

    // Start is called before the first frame update
    void Awake()
    {
        skies = new List<SpriteRenderer>();
        int i = 0;
        foreach (Transform child in transform) {
            if (child.GetComponent<SpriteRenderer>()) {
                child.position -= Vector3.forward * i++;
                skies.Add(child.GetComponent<SpriteRenderer>());
            }
        }
        skyCount = skies.Count;
        interval = 1f / skyCount;
    }

    public void TriggerTransition () {
        targetInterval += interval;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J)){
            TriggerTransition();
        }

        for (int i = 0; i < skyCount; i++) {
            diff = Mathf.Clamp01(i * interval - t) / interval;
            Color c = skies[i].color;
            skies[i].color = new Color(c.r, c.g, c.b, 1 - diff);
        }

        if (t < targetInterval) {
            t += Time.deltaTime / secondsToMidnight;
        } else {
            t = targetInterval;
        }
    }
}

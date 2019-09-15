using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineHandler : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin noise;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) {
            Explode();
        }
    }

    void Explode () {
        StartCoroutine(ExplodeCR());
    }

    IEnumerator ExplodeCR () {
        float t = 0f;
        noise.m_AmplitudeGain = 0f;
        while (t < 1f) {
            noise.m_AmplitudeGain = Mathf.Sin(t * Mathf.PI) * 5f;
            t += Time.deltaTime * 2f;
            yield return new WaitForEndOfFrame();
        }
        noise.m_AmplitudeGain = 0f;
    }
}

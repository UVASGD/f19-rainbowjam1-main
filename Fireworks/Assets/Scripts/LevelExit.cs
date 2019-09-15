using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Cinemachine;

// [RequireComponent(typeof(PlayableDirector))]
public class LevelExit : MonoBehaviour
{

    public int nextLevelIndex = 1;
    PlayerBody playerBody;
    PlayerController playerController;
    private bool movePlayer;

    void Start () {
        movePlayer = false;
    }

    void OnTriggerEnter2D (Collider2D other) {
        print("UWU");
        if (other.CompareTag("Player") && other.name == "Player") {
            // print(GetComponent<PlayableDirector>());
            // GetComponent<PlayableDirector>().Play();
            playerBody = other.GetComponent<PlayerBody>();
            playerController = other.GetComponent<PlayerController>();
            ChangeLevel();
        
        }
    }

    void Update () {
        if (movePlayer && playerBody) {
            playerBody.HandleInput(1f, false, false);
        }
    }

    public void ChangeLevel () {
        print("DOING IT");
        StartCoroutine(ChangeLevelCR());
    }

    private IEnumerator ChangeLevelCR () {
        CinemachineVirtualCamera cam = FindObjectOfType<CinemachineVirtualCamera>();
		cam.transform.parent.Find("FollowPoint").GetComponent<FollowPoint>().player = transform;
        playerController.activated = false;
        movePlayer = true;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        print("INDEX: " + currentIndex);
        print("Scene Count: " + SceneManager.sceneCountInBuildSettings);
        print("To load: " + (currentIndex+1)%SceneManager.sceneCountInBuildSettings);
        yield return new WaitForSeconds(2.5f);
        Destroy(playerBody.gameObject);
		SceneManager.LoadScene((currentIndex + 1) % SceneManager.sceneCountInBuildSettings, LoadSceneMode.Additive);
		yield return new WaitForSeconds(2.5f);
		print("Follow: " + cam.Follow.name);
		CinemachineFramingTransposer transposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
		bool og_softZone = transposer.m_UnlimitedSoftZone;
		transposer.m_UnlimitedSoftZone = true;
		transposer.m_XDamping *= 2;
		transposer.m_YDamping *= 2;
		transposer.m_ZDamping *= 2;

		cam.transform.parent.Find("FollowPoint").GetComponent<FollowPoint>().player = GameObject.FindGameObjectWithTag("Player").transform;
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().activated = false;
		yield return new WaitForSeconds(5f);
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().activated = true;
		transposer.m_UnlimitedSoftZone = og_softZone;
		transposer.m_XDamping /= 2;
		transposer.m_YDamping /= 2;
		transposer.m_ZDamping /= 2;
		SceneManager.UnloadSceneAsync(currentIndex);
    }
}

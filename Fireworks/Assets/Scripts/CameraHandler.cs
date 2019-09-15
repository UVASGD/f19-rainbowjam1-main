using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
	public static CameraHandler instance;

	private void Awake () {
		if (instance) {
			Destroy(gameObject);
		} else {
			instance = this;
			transform.SetParent(null);
			DontDestroyOnLoad(gameObject);
		}
	}
}

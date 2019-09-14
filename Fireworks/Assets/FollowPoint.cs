using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour {
	public Transform player;
	Vector3 vel;
	private void Update () {
		if (player) {
			transform.position = Vector3.SmoothDamp(transform.position, player.position, ref vel, 0.5f);
		} else {
			player = GameObject.Find("Player").transform;
		}
	}
}
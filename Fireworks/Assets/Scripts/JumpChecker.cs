using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GroundDel(bool ground);
public enum JumpCheckerType { Ground, LeftWall, RightWall }

[RequireComponent(typeof(Collider2D))]
public class JumpChecker : MonoBehaviour
{
    public GroundDel GroundEvent;
    public JumpCheckerType type;
    public int grounded;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        grounded++;
        GroundEvent?.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        grounded--;
        if (grounded < 0)
            grounded = 0;
        GroundEvent?.Invoke(false);
    }
}

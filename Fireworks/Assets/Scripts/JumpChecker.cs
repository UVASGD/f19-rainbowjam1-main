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
    List<GameObject> collided_objects = new List<GameObject>();

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void LateUpdate()
    {
        for (int i = 0; i < collided_objects.Count; i++)
        {
            try
            {
                if (!collided_objects[i])
                {
                    grounded--;
                    if (grounded < 0)
                        grounded = 0;
                    else
                        GroundEvent?.Invoke(false);
                    collided_objects.Remove(collided_objects[i]);
                }
            }
            catch { }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        collided_objects.Add(collider.gameObject);
        grounded++;
        GroundEvent?.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        collided_objects.Remove(collider.gameObject);
        grounded--;
        if (grounded < 0)
            grounded = 0;
        GroundEvent?.Invoke(false);
    }
}

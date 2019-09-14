using UnityEngine;
using System.Collections;
 
[ExecuteInEditMode]
[RequireComponent(typeof(EdgeCollider2D))]
public class HollowCircleCollider : MonoBehaviour
{
    public float Radius = 1.0f;
    public int NumPoints = 32;
 
    public EdgeCollider2D EdgeCollider;
    float CurrentRadius = 0.0f;
 
    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start ()
    {
        CreateCircle();
    }
 
    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        // If the radius or point count has changed, update the circle
        if(NumPoints != EdgeCollider.pointCount || CurrentRadius != Radius)
        {
            CreateCircle();
        }
    }
 
    /// <summary>
    /// Creates the circle.
    /// </summary>
    void CreateCircle()
    {
        Vector2[] edgePoints = new Vector2[NumPoints + 1];
        if (EdgeCollider == null) {
            EdgeCollider = GetComponent<EdgeCollider2D>();
        }
       
        for(int loop = 0; loop <= NumPoints; loop++)
        {
            float angle = (Mathf.PI * 2.0f / NumPoints) * loop;
            edgePoints[loop] = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        }
       
        EdgeCollider.points = edgePoints;
        CurrentRadius = Radius;
    }
}
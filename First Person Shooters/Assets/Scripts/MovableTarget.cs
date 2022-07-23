using UnityEngine;
/// <summary>
/// Movable Target Class. Allows a target to be moved along a group of points.
/// </summary>
public class MovableTarget : MonoBehaviour
{
    public Transform[] path;                //The path array to travel
    public float stopDistance = 0.05f;      //Stores the minimum distance to reach a point of the path
    public float speed = 5f;                //The movement speed of the target
    private int currentPoint = 0;           //The current target position to move to

    /// <summary>
    /// Unity Update Callback
    /// </summary>
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, path[currentPoint].position) < stopDistance) {

            if (currentPoint == path.Length - 1)
            {
                currentPoint = 0;
            }
            else { 
                currentPoint++;
            }
        }
    }
    /// <summary>
    /// Unity OnDrawGizmosCallback
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        for (int i = 0; i < path.Length; i++)
        {
            if (path[i] != null) {

                if (i == 0)
                {
                    Gizmos.DrawLine(path[i].position, path[path.Length - 1].position);
                }
                else {
                    Gizmos.DrawLine(path[i].position, path[i - 1].position);
                }
            }
        }
    }
}
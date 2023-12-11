using Unity.VisualScripting;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private GameObject[] balls;
    [SerializeField] private TableParams table;
    [SerializeField] Transform firstBallPos;


    private void Start()
    {
        var cueBall = balls[0];
        var ballPos = CalculateOtherBallPositions(cueBall.GetComponent<SphereCollider>().radius);

        for(int i = 1; i < balls.Length; i++)
        {
            balls[i].transform.position = ballPos[i];
        }
    }
    Vector3[] CalculateOtherBallPositions(float ballRadius)
    {
        // Define the offsets for the positions of the other balls relative to the cue ball.
        Vector3[] offsets = new Vector3[]
        {
            firstBallPos.position,
            new Vector3(firstBallPos.position.x, firstBallPos.position.y, -2 * ballRadius), // Ball 1
            new Vector3(firstBallPos.position.x, firstBallPos.position.y, 2 * ballRadius),  // Ball 2
            new Vector3(-ballRadius * 1.5f, firstBallPos.position.y, -ballRadius),  // Ball 3
            new Vector3(-ballRadius * 1.5f, firstBallPos.position.y, ballRadius),   // Ball 4
            new Vector3(ballRadius * 1.5f, firstBallPos.position.y, -ballRadius),   // Ball 5
            new Vector3(ballRadius * 1.5f, firstBallPos.position.y, ballRadius),    // Ball 6
            new Vector3(-ballRadius * 3f, firstBallPos.position.y, firstBallPos.position.z),             // Ball 7
            new Vector3(firstBallPos.position.x, firstBallPos.position.y, -ballRadius * 3),             // Ball 8
            new Vector3(firstBallPos.position.x, firstBallPos.position.y, ballRadius * 3),              // Ball 9
            new Vector3(ballRadius * 3f, firstBallPos.position.y, firstBallPos.position.z),              // Ball 10
            new Vector3(-ballRadius * 3f, firstBallPos.position.y, -ballRadius * 1.5f),  // Ball 11
            new Vector3(-ballRadius * 3f, firstBallPos.position.y, ballRadius * 1.5f),   // Ball 12
            new Vector3(ballRadius * 3f, firstBallPos.position.y, -ballRadius * 1.5f),   // Ball 13
            new Vector3(ballRadius * 3f, firstBallPos.position.y, ballRadius * 1.5f),    // Ball 14
            new Vector3(firstBallPos.position.x, firstBallPos.position.y, firstBallPos.position.z)                          // Ball 15 (Black ball)
        };

        // Calculate positions of the other balls based on the cue ball's position.
        Vector3[] ballPositions = new Vector3[offsets.Length];
        for (int i = 0; i < offsets.Length; i++)
        {
            ballPositions[i] = firstBallPos.position + offsets[i];
        }

        return ballPositions;
    }
}

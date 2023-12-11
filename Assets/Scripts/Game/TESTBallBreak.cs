using UnityEngine;
using System.Collections.Generic;

public enum GameMode
{
    EIGHTBALL,
    NINEBALL
}
public class TESTBallBreak : MonoBehaviour
{
    [SerializeField] List<Ball> balls = new();
    [SerializeField] TableParams tableParams;
    [SerializeField] GameMode gameMode;
    float spacingFactor = 1e-3f;

    public void Start()
    {
        CreateBreak(gameMode);
    }
    public void CreateBreak(GameMode mode)
    {
        if (mode == GameMode.EIGHTBALL)
            CreateEightBallBreak();
        else if (mode == GameMode.NINEBALL)
            CreateNineBallBreak();
    }

    private void CreateNineBallBreak()
    {
        Vector3 startPos = balls[0].transform.position;
        float radius = balls[0].params_.R;
        float distance = 2 * radius;

        // Positions for the 9-ball diamond rack
        Vector3[] positions = new Vector3[9];

        // First row - 1 ball
        positions[0] = startPos;

        // Second row - 2 balls
        positions[1] = startPos + new Vector3(-distance / 2, 0, -distance * Mathf.Sqrt(3) / 2);
        positions[2] = startPos + new Vector3(distance / 2, 0, -distance * Mathf.Sqrt(3) / 2);

        // Third row - 3 balls (with the 9-ball in the center)
        positions[3] = startPos + new Vector3(-distance, 0, -2 * distance * Mathf.Sqrt(3) / 2);
        positions[4] = startPos + new Vector3(0, 0, -2 * distance * Mathf.Sqrt(3) / 2);
        positions[5] = startPos + new Vector3(distance, 0, -2 * distance * Mathf.Sqrt(3) / 2);

        // Fourth row - 2 balls
        positions[6] = startPos + new Vector3(-distance / 2, 0, -3 * distance * Mathf.Sqrt(3) / 2);
        positions[7] = startPos + new Vector3(distance / 2, 0, -3 * distance * Mathf.Sqrt(3) / 2);

        // Fifth row - 1 ball
        positions[8] = startPos + new Vector3(0, 0, -4 * distance * Mathf.Sqrt(3) / 2);

        for (int i = 0; i < positions.Length; i++)
        {
            balls[i].state.rvw[0] = positions[i];
        }
        (balls[8].state.rvw[0], balls[4].state.rvw[0]) = (balls[4].state.rvw[0], balls[8].state.rvw[0]);

        for (int i = positions.Length; i < balls.Count; i++)
        {
            balls[i].gameObject.SetActive(false);
        }
    }
    private void CreateEightBallBreak()
    {
        List<Vector3> poses = new();
        Vector3 startPos = balls[0].transform.position;
        float radius = balls[0].params_.R;
        float distance = 2 * radius;
        int ballsInRow = 5; // Number of balls in the bottom row

        for (int row = 0; row < ballsInRow; row++)
        {
            for (int col = 0; col <= row; col++)
            {
                Vector3 ballPos = startPos + new Vector3((col - row * 0.5f) * distance, 0, row * distance * Mathf.Sqrt(3) / 2);
                poses.Add(ballPos);
                Debug.Log(poses.Count - 1 + ":" + ballPos);
            }
        }
        BreakRandomizer(ref poses);
        for (int i = 0; i < balls.Count; ++i)
        {
            balls[i].state.rvw[0] = poses[i];
        }
        (balls[7].state.rvw[0], balls[4].state.rvw[0]) = (balls[4].state.rvw[0], balls[7].state.rvw[0]);
    }
    private void BreakRandomizer(ref List<Vector3> list)
    {
        int excludeIndex = 4;
        Vector3 excludeValue = list[excludeIndex];
        Debug.Log(excludeValue);

        // Remove the excluded value from the list
        list.RemoveAt(excludeIndex);

        // Shuffle the remaining list
        System.Random rng = new();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }

        // Insert the excluded value back to its original position
        list.Insert(excludeIndex, excludeValue);
    }
}

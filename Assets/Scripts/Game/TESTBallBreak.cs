using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;
using NUnit.Framework;

public class TESTBallBreak : MonoBehaviour
{
    [SerializeField] List<Ball> balls = new();
    [SerializeField] TableParams tableParams;
    float spacingFactor = 1e-3f;

    public void Start()
    {
        CreateBreak();
    }
    public void CreateBreak()
    {
        List<Vector3> poses = new();
        Vector3 startPos = balls[0].transform.position; // Replace with your starting position
        float radius = balls[0].params_.R; // Replace with your ball's radius
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
            balls[i].transform.position = poses[i];
        }
        (balls[7].transform.position, balls[4].transform.position) = (balls[4].transform.position, balls[7].transform.position);

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
            Vector3 value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        // Insert the excluded value back to its original position
        list.Insert(excludeIndex, excludeValue);
    }
}

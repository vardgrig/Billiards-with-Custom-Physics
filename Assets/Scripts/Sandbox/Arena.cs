using System.Collections.Generic;
using UnityEngine;

public class Arenaa : MonoBehaviour
{
    public Vector3 getPos(Ball ball, ITableSpecs table)
    {
        return new Vector3()
        {
            x = (table.w - 2 * ball.params_.R) * Random.Range(0f, 1f) + ball.params_.R,
            y = ball.params_.R,
            z = (table.l - 2 * ball.params_.R) * Random.Range(0f, 1f) + ball.params_.R
        };
    }
}

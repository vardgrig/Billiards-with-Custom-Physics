using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<BallCollisions> balls = new();

    public bool CanShot()
    {
        foreach(var b in balls) 
        {
            if (!b.GetComponent<MeshRenderer>().enabled)
            {
                RemoveBall(b);
                return false;
            }
            else if (b.GetComponent<Rigidbody>().velocity.magnitude > 0)
                return false;
        }
        return true;
    }

    void RemoveBall(BallCollisions b)
    {
        balls.Remove(b);
        b.gameObject.SetActive(false);
    }
}
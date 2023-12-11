using UnityEngine;

public class testshot : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Vector3 dir;

    [SerializeField] private float minPower;
    [SerializeField] private float maxPower;
    [SerializeField] BallCollisions[] balls;
    [SerializeField] private float constPower;
    [SerializeField] ForceMode forceMode;

    [SerializeField] Cue cue;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && CanShot())
        {
            dir = Camera.main.transform.forward;
            //float power = Random.Range(minPower, maxPower);
            rb.velocity += dir * constPower;
            //rb.AddForce(dir * constPower, forceMode);
        }
    }
    private bool CanShot()
    {
        foreach(var b in balls)
        {
            if (b.GetComponent<Rigidbody>().velocity.magnitude > 0)
                return false;
        }
        return true;
    }
}

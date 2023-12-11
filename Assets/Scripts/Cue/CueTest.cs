using System;
using Unity.VisualScripting;
using UnityEngine;

public class CueTest : MonoBehaviour
{
    [SerializeField] private float maxPower;
    [SerializeField] private Rigidbody rb;

    [SerializeField] BallCollisions cueBall;
    [SerializeField] GameManager manager;

    [SerializeField] MeshRenderer renderer;

    private float a = 0; // side english
    private float b = 0.25f; // vertical english
    private float m; // ball mass
    private float M; // cue mass
    private float v0 = 2f;
    private bool throttleEnglish = false;
    private float phi = 0;
    private float theta = 0;
    private float R; //ball Radius
    private bool canShot = false;


    private CueSpecs cueSpecs;


    private void Start()
    {
        cueSpecs = new CueSpecs();
        M = cueSpecs.M;
        m = cueBall.GetComponent<Rigidbody>().mass;
        R = cueBall.GetComponent<SphereCollider>().radius;
    }

    private void Update()
    {
        if (!manager.CanShot() || cueBall.LastVelocity.magnitude > 0)
        {
            renderer.enabled = false;
            canShot = false;
        }
        else
        {
            renderer.enabled = true;
            canShot = true;
        }
        if (Input.GetMouseButtonDown(0) && canShot)
        {
            var newParams = StrikeCueBall(throttleEnglish);

            cueBall.Strike(newParams);
        }
    }

    Vector3[] StrikeCueBall(bool _throttleEnglish)
    {
        a *= R;
        b *= R;

        if (_throttleEnglish)
        {
            a *= Constants.english_fraction;
            b *= Constants.english_fraction;
        }

        phi *= Mathf.PI / 180;
        theta = Mathf.PI / 180;

        var I = 2 / (5 * m * R * R);
        var c = Mathf.Sqrt(R * R - a * a - b * b);
        var numerator = 2 * M * v0;
        var temp = a * a + Mathf.Pow(b * Mathf.Cos(theta), 2) +
                           Mathf.Pow(c * Mathf.Cos(theta), 2) -
                           2 * b * c * Mathf.Cos(theta) * Mathf.Sin(theta);

        var denominator = 1 + (m / M) + 5.0f / (2 * R * R) * temp;
        var F = numerator / denominator;
        var v_B = -F / m * new Vector3(0, 0, Mathf.Cos(theta));

        var vec_x = -c * Mathf.Sin(theta) + b * Mathf.Cos(theta);
        var vec_y = -a * Mathf.Cos(theta);
        var vec_z = a * Mathf.Sin(theta);

        var vec = new Vector3(vec_x, vec_y, vec_z);
        var w_B = F / I * vec;

        var rot_angle = phi + Mathf.PI / 2;
        var v_T = MathUtilities.CoordinateRotation(v_B, rot_angle);
        var w_T = MathUtilities.CoordinateRotation(w_B, rot_angle);

        return new Vector3[2]
        {
            v_T,
            w_T
        };
    }
}
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BallCollisions : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] SphereCollider ballCollider;
    private float R;
    private float m = 0.170097f;
    private float h = 0.64f * 2f * 0.028575f;
    private float e_c = 0.85f;

    private Vector3 lastVelocity;
    private Vector3 lastAngularVelocity;

    private void Start()
    {
        R = ballCollider.radius;
    }

    public void Strike(Vector3[] newParams)
    {
        rb.velocity = newParams[0].magnitude * Camera.main.transform.forward;
        rb.angularVelocity = newParams[1].magnitude * Camera.main.transform.forward;
    }
    public float Radius => R;
    public float Mass => m;

    private void Update()
    {
        lastVelocity = rb.velocity;
        lastAngularVelocity = rb.angularVelocity;
    }
    //private void FixedUpdate()
    //{
    //    if (rb.velocity.magnitude < Constants.EPS_Space)
    //    {
    //        Debug.LogAssertion($"The {gameObject.name}'s velocity is nearly 0");
    //        rb.velocity = Vector3.zero;
    //        Debug.Log($"I tried to set {gameObject.name}'s velocity to zero: {rb.velocity}");
    //    }

    //    if (rb.angularVelocity.magnitude < Constants.EPS_Space)
    //    {
    //        Debug.LogAssertion($"The {gameObject.name}'s angular velocity is nearly 0");
    //        rb.angularVelocity = Vector3.zero;
    //        Debug.Log($"I tried to set {gameObject.name}'s angular velocity to zero: {rb.angularVelocity}");

    //    }
    //}

    public Vector3 LastVelocity => lastVelocity;
    public Vector3 LastAngularVelocity => lastAngularVelocity;

    private void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.TryGetComponent<CushionInfo>(out var cushion))
        //{
        //    Vector3 normal = cushion.GetNormal();
        //    Vector3[] newParams = Han2005(normal, other.relativeVelocity);
        //    rb.velocity = newParams[1];
        //    rb.angularVelocity = newParams[2];
        //}

        if (other.gameObject.TryGetComponent<BallCollisions>(out var ball))
        {
            //if (ball.lastVelocity.magnitude > lastVelocity.magnitude)
            //    return;

            Vector3[] my_rvw = new Vector3[3]
            {
                transform.position,
                lastVelocity,
                lastAngularVelocity
            };
            Vector3[] other_rvw = new Vector3[3]
            {
                ball.transform.position,
                ball.lastVelocity,
                ball.lastAngularVelocity
            };
            Vector3[] newParams = ResolveBallBallCollision(my_rvw, other_rvw);
            rb.velocity = newParams[0];
            ball.rb.velocity = newParams[1];
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pot"))
        {
            if (transform.CompareTag("CueBall"))
            {
                transform.position = new Vector3(0, 0, 0.626f);
                rb.velocity = Vector3.zero;
                return;
            }
            print(gameObject.name + " has potted");
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public Vector3[] ResolveBallBallCollision(Vector3[] rvw1, Vector3[] rvw2)
    {
        Vector3 r1 = rvw1[0]; Vector3 v1 = rvw1[1];
        Vector3 r2 = rvw2[0]; Vector3 v2 = rvw2[1];

        Vector3 n = (r2 - r1).normalized;
        Vector3 t = MathUtilities.CoordinateRotation(n, Mathf.PI / 2);

        Debug.Log("Normal vector (n): " + n);
        Debug.Log("Tangent vector (t): " + t);

        // Debugging: Check if 't' is perpendicular to 'n'
        float dotProduct = Vector3.Dot(n, t);
        Debug.Log("Dot product (should be close to 0): " + dotProduct);

        if (Mathf.Abs(dotProduct) < 0.0001f)
        {
            Debug.Log("Tangent vector 't' is perpendicular to normal vector 'n'.");
        }
        else
        {
            Debug.LogError("Tangent vector 't' is NOT perpendicular to normal vector 'n'. Check the CoordinateRotation function.");
        }
        var v_rel = v1 - v2;
        float v_mag = v_rel.magnitude;

        float beta = Vector3.Angle(v_rel, n) * Mathf.Deg2Rad;

        rvw1[1] = (Mathf.Sin(beta) * v_mag * t) + v2;
        rvw2[1] = (Mathf.Cos(beta) * v_mag * n) + v2;
        return new Vector3[] { rvw1[1], rvw2[1] };
    }
    private Vector3[] Han2005(Vector3 normal, Vector3 velocity)
    {
        Vector3[] rvw = new Vector3[3] { transform.position, velocity, rb.angularVelocity };

        normal = Vector3.Dot(normal, Vector3.forward) > 0 ? normal : -normal;
        float psi = Mathf.Atan2(normal.x, normal.z);

        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, -psi * Mathf.Rad2Deg, 0));
        Vector3[] rvw_R = new Vector3[3];

        for (int i = 0; i < 3; i++)
            rvw_R[i] = rotationMatrix.MultiplyPoint3x4(rvw[i]);

        float phi = Mathf.Atan2(rvw_R[1].z, rvw_R[1].x) % (2 * Mathf.PI);
        float theta_a = Mathf.Asin(h / R - 1);

        float sx = rvw_R[1].x * Mathf.Sin(theta_a) - rvw_R[1].z * Mathf.Cos(theta_a) + R * rvw_R[1].y;
        float sy = -rvw_R[1].y - R * rvw_R[1].z * Mathf.Cos(theta_a) + R * rvw_R[1].x * Mathf.Sin(theta_a);
        float c = rvw_R[1].x * Mathf.Cos(theta_a);

        float I = 2f / 5f * m * R * R;
        float A = 7f / 2f / m;
        float B = 1f / m;

        float e = e_c;
        float mu = 0.2f;

        float PzE = (1 + e) * c / B;
        float PzS = Mathf.Sqrt(sx * sx + sy * sy) / A;

        float PX, PY, PZ;

        if (PzS <= PzE)
        {
            PX = -sx / A * Mathf.Sin(theta_a) - (1 + e) * c / B * Mathf.Cos(theta_a);
            PY = sy / A;
            PZ = sx / A * Mathf.Cos(theta_a) - (1 + e) * c / B * Mathf.Sin(theta_a);
        }
        else
        {
            PX = -mu * (1 + e) * c / B * Mathf.Cos(phi) * Mathf.Sin(theta_a) - (1 + e) * c / B * Mathf.Cos(theta_a);
            PY = mu * (1 + e) * c / B * Mathf.Sin(phi);
            PZ = mu * (1 + e) * c / B * Mathf.Cos(phi) * Mathf.Cos(theta_a) - (1 + e) * c / B * Mathf.Sin(theta_a);
        }

        rvw_R[1].x += PX / m;
        rvw_R[1].y += PY / m;

        rvw_R[2].z += -R / I * PY * Mathf.Sin(theta_a);
        rvw_R[2].z += R / I * (PX * Mathf.Sin(theta_a) - PZ * Mathf.Cos(theta_a));
        rvw_R[2].z += R / I * PY * Mathf.Cos(theta_a);

        Matrix4x4 inverseRotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, psi * Mathf.Rad2Deg, 0));
        Vector3[] rvw_back = new Vector3[3];

        for (int i = 0; i < 3; i++)
        {
            rvw_back[i] = inverseRotationMatrix.MultiplyPoint3x4(rvw_R[i]);
        }

        return rvw_back;
    }
}
using UnityEngine;

public class CushionInfo : MonoBehaviour
{
    private Vector3 normal;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    public float height;

    private float lx;
    private float lz;


    private void Start()
    {
        lx = CalculateLX();
        lz = CalculateLZ();
        normal = MathUtilities.UnitVector(new Vector3(lx, 0, lz));
    }
    private float CalculateLX()
    {
        if (end.position.x - start.position.x == 0)
            return 1;
        return -(end.position.y - start.position.y) / (end.position.x - start.position.x);
    }
    private float CalculateLZ()
    {
        if (end.position.x - start.position.x == 0)
            return 0;
        return 1;
    }
    public Vector3 GetNormal(Vector3[] rvw)
    {
        return normal;
    }
}

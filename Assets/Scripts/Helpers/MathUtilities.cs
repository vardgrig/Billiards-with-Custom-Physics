using UnityEngine;

public static class MathUtilities
{
    public static Vector3 UnitVector(Vector3 v, bool handleZero = false)
    {
        var norm = v.magnitude;
        if(handleZero && norm == 0)
            return Vector3.zero;

        return v / norm;
    }
    public static Vector3[] Transpose(Vector3[] v)
    {
        Vector3[] transposedVector = new Vector3[v.Length];
        int rowCount = v.Length;
        int colCount = 3;
        for(int col = 0; col < colCount; col++)
        {
            Vector3 transposedColumn = Vector3.zero;
            for(int row = 0; row < rowCount; row++)
            {
                transposedColumn[row] = v[row][col];
            }
            transposedVector[col] = transposedColumn;
        }
        return transposedVector;
    }
    public static float Angle(Vector3 v2, Vector3 v1)
    {
        float angle = Mathf.Atan2(v2.z, v2.x) - Mathf.Atan2(v1.z, v1.x);

        if (angle < 0)
            angle += 2 * Mathf.PI;

        return angle;
    }
    public static float Angle(Vector2 v)
    {
        return Angle(v, new Vector2(1, 0));
    }
    public static Vector3 CoordinateRotation(Vector3 v, float phi)
    {
        float cosPhi = Mathf.Cos(phi);
        float sinPhi = Mathf.Sin(phi);

        Matrix4x4 rotationMatrix = Matrix4x4.identity;

        rotationMatrix[0, 0] = cosPhi; // Rotation about the X-axis
        rotationMatrix[0, 2] = -sinPhi; // Negative sine for the Z-axis
        rotationMatrix[2, 0] = sinPhi; // Sine for the X-axis
        rotationMatrix[2, 2] = cosPhi; // Rotation about the Z-axis
        
        return rotationMatrix.MultiplyPoint3x4(v);
    }
    public static Vector3[] CoordinateRotation(Vector3[] v, float phi)
    {
        float cosPhi = Mathf.Cos(phi);
        float sinPhi = Mathf.Sin(phi);

        // Create a 3x3 rotation matrix
        Matrix4x4 rotationMatrix = Matrix4x4.identity;

        rotationMatrix[0, 0] = cosPhi; // Rotation about the X-axis
        rotationMatrix[0, 2] = -sinPhi; // Negative sine for the Z-axis
        rotationMatrix[2, 0] = sinPhi; // Sine for the X-axis
        rotationMatrix[2, 2] = cosPhi; // Rotation about the Z-axis

        // Initialize an array to store the rotated vectors
        Vector3[] rotatedVectors = new Vector3[3];

        // Apply the rotation to each input vector
        for (int i = 0; i < 3; i++)
        {
            rotatedVectors[i] = rotationMatrix.MultiplyPoint3x4(v[i]);
        }

        return rotatedVectors;
    }

    public static Vector3 PointOnLineClosestToPoint(Vector3 p1, Vector3 p2, Vector3 p0)
    {
        Vector3 diff = p2 - p1;
        float t = -Vector3.Dot(p1 - p0, diff) / Vector3.Dot(diff, diff);
        return p1 + diff * t;
    }
    public static float Norm3D(Vector3 vec)
    {
        return Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
    }
    public static Vector3 Cross(Vector3 u, Vector3 v)
    {
        Vector3 crossVector = new()
        {
            x = u[1] * v[2] - u[2] * v[1],
            y = u[2] * v[0] - u[0] * v[2],
            z = u[0] * v[1] - u[1] * v[0]
        };

        return crossVector;
    }

    public static Vector3[] T(Vector3[] v)
    {
        int rows = v.GetLength(0);
        int cols = v.GetLength(1);

        Vector3[] transposedVectorArray = new Vector3[cols * rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                transposedVectorArray[j * rows + i] = v[i * cols + j];
            }
        }

        return transposedVectorArray;
    }
    public static int Orientation(Vector3 p, Vector3 q, Vector3 r)
    {
        var val = ((q.z - p.z) * (r.x - q.x)) - ((q.x - p.x) * (r.z - q.z));
        if (val > 0)
            return 1;
        else if (val < 0)
            return 2;
        else
            return 0;
    }
}

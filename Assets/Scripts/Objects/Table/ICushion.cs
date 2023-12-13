using UnityEngine;
public interface ICushion
{
    float Height { get;}
    Vector3 GetNormal(Vector3[] rvw);
}

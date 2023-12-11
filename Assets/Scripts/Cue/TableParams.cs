using UnityEngine;

public class TableParams : MonoBehaviour
{
    [SerializeField] private float _w;
    [SerializeField] private float _l;

    public float w => _w;
    public float l => _l;
}
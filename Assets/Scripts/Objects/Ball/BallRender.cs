using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallRender : MonoBehaviour
{
    public Ball ball;
    public List<Quaternion> quats;
    Quaternion quat;
    public void SetObjectStateAsRenderState(bool patch = false)
    {
        Vector3 xyz = this.GetRenderState();
        if (patch)
            xyz.y = this.ball.params_.R;

        this.ball.state.rvw[0] = xyz;
    }
    void SetRenderStateAsObjectState()
    {
        this.SetRenderState(ball.state.rvw[0]);
    }
    Vector3 GetRenderState()
    {
        Vector3 xyz = this.transform.position;
        return xyz;
    }
    void SetRenderState(Vector3 pos)
    {
        this.transform.SetPositionAndRotation(pos, quat);
    }
    void SetRenderStateFromHistory(BallHistory ballHistory, int i)
    {
        quat = quats.Count > 0 ? quats[i] : Quaternion.identity;
        this.SetRenderState(ballHistory[i].rvw[0]);
    }
    void SetQuats(BallHistory history)
    {
        Tuple<Vector3[], int[], float[]> rvws_ts = history.Vectorize();
        List<float> ws = new();
        foreach (var w in rvws_ts.Item1)
        {
            ws.Add(w.y);
        }
        for (int i = 0; i < ws.Count; i++)
        {
            quats.Add(Quaternion.Euler(ws[i], 0, rvws_ts.Item3[i]));
        }
    }
    void RandomizeOrientation()
    {
        float[] randoms = new float[3];
        randoms[0] = Random.Range(-180f, 180f);
        randoms[1] = Random.Range(-180f, 180f);
        randoms[2] = Random.Range(-180f, 180f);
        this.transform.rotation = Quaternion.Euler(randoms[0], randoms[1], randoms[2]);
    }
    BallOrientation GetOrientation()
    {
        return new BallOrientation()
        {
            pos = this.transform.position,
            sphere = this.transform.rotation.eulerAngles
        };
    }
    BallOrientation GetDinalOrientation()
    {
        return new BallOrientation()
        {
            pos = this.transform.position,
            sphere = this.quats[quats.Count - 1].eulerAngles
        };
    }
    void SetOrientation(BallOrientation orientation) 
    {
        this.transform.SetPositionAndRotation(orientation.pos, Quaternion.Euler(orientation.sphere));
    }
    void ResetAngularIntegration()
    {
        this.transform.rotation = this.transform.rotation * Quaternion.Euler(this.transform.position);
        this.transform.position = Vector3.zero;
    }
}

using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class CueRender : MonoBehaviour
{
    private Ball follow;
    private Cue _cue;
    //private ClockObject strokeClock;
    private bool hasFocus;
    private List<float> strokePos;
    private List<float> strokeTime;
    private bool rendered;
    [SerializeField] GameObject cueStick;
    [SerializeField] GameObject cueStickFocus;
    [SerializeField] GameObject cueStickModel;
    public CueRender(Cue cue)
    {
        this.rendered = false;
        this.follow = null;
        this._cue = cue;
        //this.strokeClock = new ClockObject();
        this.hasFocus = false;
        this.strokePos = new List<float>();
        this.strokeTime = new List<float>();
    }

    public void SetObjectStateAsRenderState(bool skipV0 = false)
    {
        float V0;
        (V0, this._cue.phi, this._cue.theta, this._cue.a, this._cue.b, this._cue.cue_ball_id) = this.GetRenderState();

        if (!skipV0)
        {
            this._cue.V0 = V0;
        }
    }

    public void SetRenderStateAsObjectState()
    {
        this.MatchBallPosition();

        cueStickFocus.transform.rotation = Quaternion.Euler(0, this._cue.phi + 180, 0);
        cueStickFocus.transform.Rotate(Vector3.right, -this._cue.theta); // theta
        cueStick.transform.position = new Vector3(0, -this._cue.a * this.follow.params_.R, this._cue.b * this.follow.params_.R);
    }

    public void InitModel()
    {
        cueStickModel.name = "cue_stick_model";
        cueStickModel.transform.parent = cueStick.transform;

        //this.nodes["cue_stick"] = cueStick;
        //this.nodes["cue_stick_model"] = cueStickModel;
    }

    public void InitFocus(Ball ball)
    {
        this.follow = ball;

        this.cueStickModel.transform.position = new Vector3(this.follow.params_.R, 0, 0);

        //GameObject cueStickFocus = new GameObject("cue_stick_focus");
        //this.nodes["cue_stick_focus"] = cueStickFocus;

        this.MatchBallPosition();
        this.cueStick.transform.parent = cueStickFocus.transform;

        this.hasFocus = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Bounds bounds = this.cueStick.GetComponent<Renderer>().bounds;
        float x = 0;
        float X = bounds.max.x - bounds.min.x;

        var collisionNode = GameObject.Find("cue_cseg");
        collisionNode.layer = 0;
        collisionNode.transform.parent = this.cueStickModel.transform;

    }
    //public void InitCollisionHandling(CollisionHandler collisionHandler)
    //{
    //    if (!Settings.Gameplay.CueCollision)
    //    {
    //        return;
    //    }
    //    Bounds bounds = this.GetNode("cue_stick").GetComponent<Renderer>().bounds;

    //    float x = 0;
    //    float X = bounds.max.x - bounds.min.x;

    //    GameObject collisionNode = new GameObject("cue_cseg");
    //    collisionNode.layer = 0;
    //    collisionNode.AddComponent<BoxCollider>();
    //    collisionNode.transform.parent = this.cueStickModel.transform;

    //    this.nodes["cue_cseg"] = collisionNode;
    //    collisionHandler.AddCollider(collisionNode.GetComponent<Collider>());

    //    if (Settings.Graphics.Debug)
    //    {
    //        collisionNode.GetComponent<Renderer>().enabled = true;
    //    }
    //}

    public float GetLength()
    {
        Bounds bounds = this.cueStick.GetComponent<Renderer>().bounds;
        return bounds.max.x - bounds.min.x;
    }

    public void TrackStroke()
    {
        // Initialize variables for storing cue position during stroke
        this.strokePos = new List<float>();
        this.strokeTime = new List<float>();
        //this.strokeClock.Reset();
    }

    public void AppendStrokeData()
    {
        // Append current cue position and timestamp to the cue tracking data
        this.strokePos.Add(this.cueStick.transform.position.x);
        //this.strokeTime.Add(this.strokeClock.RealTime);
    }

    public List<Vector3> GetStrokeSequence()
    {
        // Initialize a stroke sequence based on self.strokePos and self.strokeTime
        GameObject cueStick = this.cueStick;
        List<Vector3> strokeSequence = new();

        // If the stroke is longer than max_time seconds, truncate to max_time
        float maxTime = 1.0f;
        float backstrokeTime, apexTime, strikeTime;
        (backstrokeTime, apexTime, strikeTime) = this.GetStrokeTimes();
        if (strikeTime - apexTime > maxTime)
        {
            int idx = this.strokePos.FindIndex(i => Mathf.Approximately(this.strokePos[(int)i], strikeTime - maxTime));
            this.strokePos = this.strokePos.GetRange(idx, this.strokePos.Count - idx);
            this.strokeTime = this.strokeTime.GetRange(idx, this.strokeTime.Count - idx);
        }

        float[] xs = this.strokePos.ToArray();
        float[] dts = new float[this.strokeTime.Count - 1];

        for (int i = 0; i < dts.Length; i++)
        {
            dts[i] = this.strokeTime[i + 1] - this.strokeTime[i];
        }

        float y = cueStick.transform.position.y;
        float z = cueStick.transform.position.z;

        for (int i = 0; i < dts.Length; i++)
        {
            strokeSequence.Add(Vector3.Lerp(cueStick.transform.position, new Vector3(xs[i + 1], y, z), dts[i]));
        }

        return strokeSequence;
    }

    public (float, float, float) GetStrokeTimes(bool asIndex = false)
    {
        // Get key moments in the trajectory of the stroke
        if (this.strokePos.Count == 0)
        {
            return (0, 0, 0);
        }

        // Find the index of the apex (highest point in the backswing)
        int apexIndex = Array.IndexOf(this.strokePos.ToArray(), this.strokePos.Max());
        float apexTime = this.strokeTime[apexIndex];

        // Find the index of the backstroke start (lowest point before the apex)
        int backstrokeIndex = Array.IndexOf(this.strokePos.Take(apexIndex + 1).ToArray(), this.strokePos.Min());
        float backstrokeTime = this.strokeTime[backstrokeIndex];

        // The last position in the list is considered the strike
        int strikeIndex = this.strokePos.Count - 1;
        float strikeTime = this.strokeTime[strikeIndex];

        if (asIndex)
        {
            return (backstrokeIndex, apexIndex, strikeIndex);
        }
        else
        {
            return (backstrokeTime, apexTime, strikeTime);
        }
    }

    public bool IsShot()
    {
        if (this.strokeTime.Count < 10)
            return false;

        if (!this.strokePos.Any(x => x > 0))
            return false;

        float backstrokeTime, _, strikeTime;
        (backstrokeTime, _, strikeTime) = this.GetStrokeTimes();

        if (strikeTime - backstrokeTime < 0.3f)
        {
            // Stroke is too short
            return false;
        }

        return true;
    }

    public float CalcV0FromStroke()
    {
        float _, apexTime, strikeTime;
        try
        {
            (_, apexTime, strikeTime) = this.GetStrokeTimes();
        }
        catch (IndexOutOfRangeException)
        {
            throw new Exception("Unresolved edge case");
        }

        float maxTime = 0.1f;
        if (strikeTime - apexTime < maxTime)
        {
            throw new Exception("Unresolved edge case");
        }

        for (int i = this.strokeTime.Count - 1; i >= 0; i--)
        {
            if (strikeTime - this.strokeTime[i] > maxTime)
            {
                return this.strokePos[i] / maxTime;
            }
        }

        return 0;
    }

    public void MatchBallPosition()
    {
        // Update the cue stick's position to match the cueing ball's position
        this.cueStickFocus.transform.position = this.follow.transform.position;
    }

    public Tuple<float, float, float, float, float, string> GetRenderState()
    {
        // Return phi, theta, V0, a, and b as determined by the cue_stick node
        GameObject cueStick = this.cueStick;
        GameObject cueStickFocus = this.cueStickFocus;

        float phi = (cueStickFocus.transform.eulerAngles.y + 180) % 360;

        float V0;
        try
        {
            V0 = this.CalcV0FromStroke();
        }
        catch (Exception)
        {
            V0 = 0.1f;
        }

        float theta = -cueStickFocus.transform.eulerAngles.x;
        float a = -cueStick.transform.position.y / this.follow.params_.R;
        float b = cueStick.transform.position.z / this.follow.params_.R;
        string ballId = this.follow.id;

        return new Tuple<float, float, float, float, float, string>(V0, phi, theta, a, b, ballId);
    }
}
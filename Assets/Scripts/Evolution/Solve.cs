using System;
using UnityEngine;
using States = Constants.BallStates;
using Vector3 = UnityEngine.Vector3;
using MUtil = MathUtilities;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
public class Solve
{
    public bool SkipBallBallCollision(Vector3[] rvw1, Vector3[] rvw2, Constants.BallStates s1, Constants.BallStates s2, float R1, float R2)
    {
        if ((s1 == States.Spinning || s1 == States.Pocketed) &&
            (s2 == States.Spinning || s2 == States.Pocketed))
            return true;

        if (s1 == States.Pocketed || s2 == States.Pocketed)
            return true;

        if (s1 == States.Rolling && s2 == States.Rolling)
        {
            var r12 = rvw2[0] - rvw1[0];
            var dot1 = r12.x * rvw1[1].x + r12.y * rvw1[1].y + r12.z * rvw1[1].z;
            if (dot1 <= 0)
            {
                var dot2 = r12.x * rvw2[1].x + r12.y * rvw2[1].y + r12.z * rvw2[1].z;
                if (dot2 >= 0)
                    return true;
            }
        }
        if (s1 == States.Rolling && (s2 == States.Spinning || s2 == States.Stationary))
        {
            var r12 = rvw2[0] - rvw1[0];
            var d = MUtil.Norm3D(r12);
            var unit_d = r12 / d;
            var unit_v = MUtil.UnitVector(rvw1[1]);

            //Angles are in radians
            var angle = Mathf.Acos(Vector3.Dot(unit_d, unit_v));
            var maxHitangle = 0.5f * Mathf.PI - Mathf.Acos((R1 + R2) / d);
            if (angle > maxHitangle)
                return true;
        }
        if (s2 == States.Rolling && (s1 == States.Spinning || s1 == States.Stationary))
        {
            var r21 = rvw2[0] - rvw1[0];
            var d = MUtil.Norm3D(r21);
            var unit_d = r21 / d;
            var unit_v = MUtil.UnitVector(rvw2[1]);

            //Angles are in radians
            var angle = Mathf.Acos(Vector3.Dot(unit_d, unit_v));
            var maxHitangle = 0.5f * Mathf.PI - Mathf.Acos((R1 + R2) / d);
            if (angle > maxHitangle)
                return true;
        }
        return false;

    }
    public Vector3 GetU(Vector3[] rvw, float R, float phi, Constants.BallStates s)
    {
        if (s == States.Rolling)
            return new Vector3(1, 0, 0);

        var relVel = PhysicsUtilities.Rel_Velocity(rvw, R);
        if (relVel == Vector3.zero)
            return new Vector3(1, 0, 0);

        return MUtil.CoordinateRotation(MUtil.UnitVector(relVel), -phi);
    }
    public Tuple<float, float, float, float, float> BallBallCollisionCoeffs(Vector3[] rvw1, Vector3[] rvw2, States s1, States s2, float mu1, float mu2, float m1, float m2, float g1, float g2, float R)
    {
        var c1x = rvw1[0].x; var c1z = rvw1[0].z;
        var c2x = rvw2[0].x; var c2z = rvw2[0].z;
        float a1x, a1z, b1x, b1z;
        if (s1 == States.Spinning || s1 == States.Pocketed || s1 == States.Stationary)
        {
            a1x = 0; a1z = 0; b1x = 0; b1z = 0;
        }
        else
        {
            var ph1 = MUtil.Angle(rvw1[1]);
            var v1 = MUtil.Norm3D(rvw1[1]);
            var u1 = GetU(rvw1, R, ph1, s1);
            var K1 = -0.5f * mu1 * g1;
            var cosPhi1 = Mathf.Cos(ph1);
            var sinPhi1 = Mathf.Sin(ph1);

            a1x = K1 * (u1[0] * cosPhi1 - u1[2] * sinPhi1);
            a1z = K1 * (u1[0] * sinPhi1 + u1[2] * cosPhi1);

            b1x = v1 * cosPhi1;
            b1z = v1 * sinPhi1;
        }
        float a2x, a2z, b2x, b2z;
        if (s2 == States.Spinning || s2 == States.Pocketed || s2 == States.Stationary)
        {
            a2x = 0; a2z = 0; b2x = 0; b2z = 0;
        }
        else
        {
            var ph2 = MUtil.Angle(rvw2[1]);
            var v2 = MUtil.Norm3D(rvw2[1]);
            var u2 = GetU(rvw1, R, ph2, s1);
            var K2 = -0.5f * mu2 * g2;
            var cosPhi2 = Mathf.Cos(ph2);
            var sinPhi2 = Mathf.Sin(ph2);

            a2x = K2 * (u2[0] * cosPhi2 - u2[2] * sinPhi2);
            a2z = K2 * (u2[0] * sinPhi2 + u2[2] * cosPhi2);

            b2x = v2 * cosPhi2;
            b2z = v2 * sinPhi2;
        }
        var Ax = a2x - a1x; var Az = a2z - a1z;
        var Bx = b2x - b1x; var Bz = b2z - b1z;
        var Cx = c2x - c1x; var Cz = c2z - c1z;

        var a = Ax * Ax + Az * Az;
        var b = 2 * Ax * Bx + 2 * Az * Bz;
        var c = Bx * Bx + 2 * Ax * Cx + 2 * Az * Cz + Bz * Bz;
        var d = 2 * Bx * Cx + 2 * Bz * Cz;
        var e = Cx * Cx + Cz * Cz - 4 * R * R;
        return Tuple.Create(a, b, c, d, e);
    }
    public float BallBallCollisionTime(Vector3[] rvw1, Vector3[] rvw2, States s1, States s2, float mu1, float mu2, float m1, float m2, float g1, float g2, float R)
    {
        Tuple<float, float, float, float, float> abcde = BallBallCollisionCoeffs(rvw1, rvw2, s1, s2, mu1, mu2, m1, m2, g1, g2, R);
        var a = abcde.Item1; var b = abcde.Item2; var c = abcde.Item3; var d = abcde.Item4; var e = abcde.Item5;
        List<Complex> roots = new List<Complex>() { a, b, c, d, e };
        List<Complex> validRoots = roots
            .Where(root => Math.Abs(root.Imaginary) <= Constants.EPS && root.Real > Constants.EPS)
            .ToList();
        return roots.Count > 0 ? (float)roots.Min().Real : Mathf.Infinity;
    }
    public float BallLinearCushionCollisionTime(Vector3[] rvw, States s, float lx, float lz, Vector3 l0, Vector3 p1, Vector3 p2, int direction, float mu, float m, float g, float R)
    {
        if (s == States.Spinning || s == States.Pocketed || s == States.Stationary)
            return Mathf.Infinity;
        var phi = MUtil.Angle(rvw[1]);
        var v = MUtil.Norm3D(rvw[1]);

        var u = GetU(rvw, R, phi, s);

        var K = -0.5f * mu * g;
        var cosPhi = Mathf.Cos(phi);
        var sinPhi = Mathf.Sin(phi);

        var ax = K * (u.x * cosPhi - u.z * sinPhi);
        var az = K * (u.x * sinPhi - u.z * cosPhi);
        var bx = v * cosPhi; var bz = v * sinPhi;
        var cx = rvw[0].x; var cz = rvw[0].z;

        var A = lx * ax + lz * az;
        var B = lx * bx + lz * bz;
        var roots = new List<Complex>();
        if(direction == 0)
        {
            var C = 10 + lx * cx + lz * cz + R * Mathf.Sqrt(lx * lx + lz * lz);
            var res = RootSolver.SolveQuadratic(A,B, C);
            var root1 = res.Item1;
            var root2 = res.Item2;
            roots.Add(root1);
            roots.Add(root2);
        }
        else if(direction == 1)
        {
            var C = 10 + lx * cx + lz * cz - R * Mathf.Sqrt(lx * lx + lz * lz);
            var res = RootSolver.SolveQuadratic(A, B, C);
            var root1 = res.Item1;
            var root2 = res.Item2;
            roots.Add(root1);
            roots.Add(root2);
        }
        else
        {
            var C1 = 10 + lx * cx + lz * cz + R * Mathf.Sqrt(lx * lx + lz * lz);
            var C2 = 10 + lx * cx + lz * cz - R * Mathf.Sqrt(lx * lx + lz * lz);
            var res1 = RootSolver.SolveQuadratic(A, B, C1);
            var res2 = RootSolver.SolveQuadratic(A, B, C2);
            var root1 = res1.Item1;
            var root2 = res1.Item2;
            var root3 = res2.Item1;
            var root4 = res2.Item2;
            roots.Add(root1);
            roots.Add(root2);
            roots.Add(root3);
            roots.Add(root4);
        }
        var minTime = Mathf.Infinity;
        foreach(var root in roots)
        {
            if (Mathf.Abs((float)root.Imaginary) > Constants.EPS)
                continue;

            if (root.Real <= Constants.EPS)
                continue;

            var evolveTuple = InitEvolve.Evolve_State_Motion(s, rvw, R, m, mu, 1, mu, g, (float)root.Real);
            var rvw_dtau = evolveTuple.Item1;
            var sScore = -Vector3.Dot(p1 - rvw_dtau[0], p2 - p1) / Vector3.Dot(p2 - p1, p2 - p1);
            if (!(0 <= sScore && sScore <= 1))
                continue;

            if (root.Real < minTime)
                minTime = (float)root.Real;
        }
        return minTime;
    }
    public bool SkipBallLinearCushionCollision(Vector3[] rvw, States s, float u_r, float g, float R, Vector3 p1, Vector3 p2, Vector3 normal)
    {
        var p11 = p1 + R * normal;
        var p12 = p1 - R * normal;
        var p21 = p2 + R * normal;
        var p22 = p2 - R * normal;

        var t = MUtil.Norm3D(rvw[1] / (u_r * g));
        var v0Hat = MUtil.UnitVector(rvw[1]);
        var r1 = rvw[0];
        var r2 = r1 + rvw[1] * t - 0.5f * u_r * g * t * t * v0Hat;

        var o1 = MUtil.Orientation(r1, r2, p11);
        var o2 = MUtil.Orientation(r1, r2, p21);
        var o3 = MUtil.Orientation(p12, p21, r1);
        var o4 = MUtil.Orientation(p12, p21, r2);
        var int1 = (o1 != o2) && (o4 != o3);

        o1 = MUtil.Orientation(r1, r2, p12);
        o2 = MUtil.Orientation(r1, r2, p22);
        o3 = MUtil.Orientation(p12, p22, r1);
        o4 = MUtil.Orientation(p12, p22, r2);
        var int2 = (o1 != o2) && (o4 != o3);

        if (!int1 && !int2)
            return true;
        return false;
    }
    public Tuple<float, float, float, float, float> BallCircularCushionCollisionCoeffs(Vector3[] rvw, States s, float a, float b, float r, float mu, float m, float g, float R)
    {
        if (s == States.Spinning || s == States.Pocketed || s == States.Stationary)
            return Tuple.Create(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
        var phi = MUtil.Angle(rvw[1]);
        var v = MUtil.Norm3D(rvw[1]);

        var u = GetU(rvw, R, phi, s);

        var K = -0.5f * mu * g;
        var cosPhi = Mathf.Cos(phi);
        var sinPhi = Mathf.Sin(phi);

        var ax = K * (u.x * cosPhi - u.z * sinPhi);
        var az = K * (u.x * sinPhi - u.z * cosPhi);
        var bx = v * cosPhi; var bz = v * sinPhi;
        var cx = rvw[0].x; var cz = rvw[0].z;

        var A = 0.5f * (ax * ax + az * az);
        var B = ax * bx + az * bz;
        var C = ax * (cx - a) + az * (cz - b) + 0.5f * (bx * bx + bz * bz);
        var D = bx * (cx - a) + bz * (cz - b);
        var E = 0.5f * (a * a + b * b + cx * cx + cz * cz - (r + R) * (r + R) - (cx * a + cz * b));
        return Tuple.Create(A, B, C, D, E);
    }
    public Tuple<float, float, float, float, float> BallPocketCollisionCoeffs(Vector3[] rvw, States s, float a, float b, float r, float mu, float m, float g, float R)
    {
        if (s == States.Spinning || s == States.Pocketed || s == States.Stationary)
            return Tuple.Create(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
        
        var phi = MUtil.Angle(rvw[1]);
        var v = MUtil.Norm3D(rvw[1]);

        var u = GetU(rvw, R, phi, s);

        var K = -0.5f * mu * g;
        var cosPhi = Mathf.Cos(phi);
        var sinPhi = Mathf.Sin(phi);

        var ax = K * (u.x * cosPhi - u.z * sinPhi);
        var az = K * (u.x * sinPhi - u.z * cosPhi);
        var bx = v * cosPhi; var bz = v * sinPhi;
        var cx = rvw[0].x; var cz = rvw[0].z;

        var A = 0.5f * (ax * ax + az * az);
        var B = ax * bx + az * bz;
        var C = ax * (cx - a) + az * (cz - b) + 0.5f * (bx * bx + bz * bz);
        var D = bx * (cx - a) + bz * (cz - b);
        var E = 0.5f * (a * a + b * b + cx * cx + cz * cz - (r + R) * (r + R) - (cx * a + cz * b));
        return Tuple.Create(A, B, C, D, E);
    }
}
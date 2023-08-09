using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DynamicParticle;

public class ParticleHelper : MonoBehaviour
{

    private static List<DynamicParticle> allParticles;

    void Update()
    {
        if (particlesToChange == null || particlesToChange.Count == 0)
        {
            return;
        }

        if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastChangeTime > stateChangeInterval)
        {
            lastChangeTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            int currentLayer = particlesToChange[0].adjacentLayer;
            for (int i = 0; i < particlesToChange.Count; i++)
            {
                if (particlesToChange[i].adjacentLayer != currentLayer)
                {
                    return;
                }
                particlesToChange[i].SetState(targetState);
                particlesToChange[i].SetLifeTime(5);
                particlesToChange.RemoveAt(i);
                i--;
            }
        }
    }

    private static List<DynamicParticle> particlesToChange;
    private static STATES targetState;
    private static long stateChangeInterval, lastChangeTime;

    public static void changeParticlesToState(List<DynamicParticle> particles, STATES state, long intervalMillis)
    {
        particlesToChange = particles;
        targetState = state;
        stateChangeInterval = intervalMillis;
        lastChangeTime = 0;
    }

    public static List<DynamicParticle> findAllAdjacentParticles(DynamicParticle origin)
    {
        allParticles = new List<DynamicParticle>(GameObject.FindObjectsOfType<DynamicParticle>());
        List<DynamicParticle> adjacentParticles = findAllAdjacentParticlesRecursion(origin, 1);
        adjacentParticles.Sort((x, y) => x.adjacentLayer.CompareTo(y.adjacentLayer));
        return adjacentParticles;
    }

    private static List<DynamicParticle> findAllAdjacentParticlesRecursion(DynamicParticle origin, int layer)
    {
        allParticles.Sort((x, y) => getDistanceSq(origin, x).CompareTo(getDistanceSq(origin, y)));
        List<DynamicParticle> adjacentParticles = new List<DynamicParticle>();

        for (int i = 0; i < allParticles.Count; i++)
        {
            DynamicParticle dp = allParticles[i];
            dp.adjacentLayer = layer;
            if (getDistanceSq(dp, origin) < 1F * 1F)
            {
                adjacentParticles.Add(dp);
                allParticles.RemoveAt(i);
                i--;
            }
        }

        foreach (DynamicParticle dp in new List<DynamicParticle>(adjacentParticles))
        {
            adjacentParticles.AddRange(findAllAdjacentParticlesRecursion(dp, layer + 1));
        }

        return adjacentParticles;
    }

    public class DistanceComparer : IComparer
    {
        public DynamicParticle originalParticle;

        public DistanceComparer(DynamicParticle originalParticle)
        {
            this.originalParticle = originalParticle;
        }

        public int Compare(object x, object y)
        {
            float d = getDistanceSq(originalParticle, (DynamicParticle)x) - getDistanceSq(originalParticle, (DynamicParticle)y);
            if (d < 0)
            {
                return -1;
            }
            else if (d > 0)
            {
                return 1;
            }
            return 0;
        }
    }

    public static float getDistanceSq(DynamicParticle dp1, DynamicParticle dp2)
    {
        Vector3 p1 = dp1.transform.position;
        Vector3 p2 = dp2.transform.position;
        return (p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y) + (p1.z - p2.z) * (p1.z - p2.z);
    }

}

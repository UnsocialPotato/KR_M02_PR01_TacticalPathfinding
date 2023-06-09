using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : Seek
{
    public GameObject[] path;

    int currentPathIndex;

    float targetRadius = .5f;

    public override SteeringOutput getSteering()
    {
        // if we're just starting out we won't have an initial target
        // find the nearest waypoint and use that as our initial target
        if (target == null)
        {
            int nearestPathIndex = 0;
            float distanceToNearest = float.PositiveInfinity;
            for (int i = 0; i < path.Length; i++)
            {
                GameObject candidate = path[i];
                Vector3 vectorToCandidate = candidate.transform.position - character.transform.position;
                float distanceToCandidate = vectorToCandidate.magnitude;
                if (distanceToCandidate < distanceToNearest)
                {
                    nearestPathIndex = i;
                    distanceToNearest = distanceToCandidate;
                }
            }

            target = path[nearestPathIndex];
        }

        // if we've reached a waypoint, target the next
        float distanceToTarget = (target.transform.position - character.transform.position).magnitude;
        if (distanceToTarget < targetRadius)
        {
            currentPathIndex++;
            if (currentPathIndex > path.Length - 1)
            {
                currentPathIndex = 0;
            }
            target = path[currentPathIndex];
        }

        // delegate to seek
        return base.getSteering();
    }
}
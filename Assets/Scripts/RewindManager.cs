using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;

public class RewindManager : MonoBehaviour
{
    public float attractionForce = 10f;
    public float repulsionForce = 10f;

    public List<Rewindable> rewindableObjects = new List<Rewindable>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) )
        {
            foreach (var obj in rewindableObjects) { obj.Rewind(); }
        }

        HandleAttractionAndRepulsion();
    }

    void HandleAttractionAndRepulsion()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (Rewindable rewindableObject in rewindableObjects)
            {
                rewindableObject.ApplyForceToRepel(transform.position, repulsionForce);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (Rewindable rewindableObject in rewindableObjects)
            {
                rewindableObject.ApplyForceToAttract(transform.position, attractionForce);
            }
        }
    }

    public void RegisterRewindable(Rewindable rewindableObject)
    {
        rewindableObjects.Add(rewindableObject);
    }
}

public struct Snapshot
{
    public Vector3 Position;
    public Quaternion Rotation;
}
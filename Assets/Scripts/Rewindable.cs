using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rewindable : MonoBehaviour
{
    public List<Snapshot> rewindData = new List<Snapshot>();
    private bool isRewinding = false;

    private Rigidbody rb;

    void Start()
    {
        FindObjectOfType<RewindManager>().RegisterRewindable(this);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RecordFrame();
    }

    private void RecordFrame()
    {
        if (!isRewinding)
        {
            if (rewindData.Count < 300)
            {
                if (transform.position != rewindData.LastOrDefault().Position && transform.rotation != rewindData.LastOrDefault().Rotation)
                {
                    Snapshot snapshot = new Snapshot();

                    snapshot.Position = transform.position;
                    snapshot.Rotation = transform.rotation;

                    rewindData.Add(snapshot);
                }
            }
        }
    }

    public void Rewind()
    {
        if (!isRewinding)
        {
            StartCoroutine(StartRewind());
        }
    }

    private IEnumerator StartRewind()
    {
        isRewinding = true;

        GetComponent<Rigidbody>().isKinematic = true;

        rewindData.Reverse();

        foreach (var snapshoot in rewindData)
        {
            this.transform.position = snapshoot.Position;
            this.transform.rotation = snapshoot.Rotation;
            yield return new WaitForSeconds(0.02f);
        }

        rewindData.Clear();

        yield return null;

        GetComponent<Rigidbody>().isKinematic = false;

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        isRewinding = false;
    }

    public void ApplyForceToAttract(Vector3 targetPosition, float force)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        Vector3 direction = targetPosition - transform.position;
        rb.AddForce(direction.normalized * force);
    }

    public void ApplyForceToRepel(Vector3 repelFromPosition, float force)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        Vector3 direction = transform.position - repelFromPosition;
        rb.AddForce(direction.normalized * force);
    }
}
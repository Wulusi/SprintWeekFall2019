using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Vector3 offset;

    [SerializeField]
    List<Transform> targets;

    [SerializeField]
    float smoothing = 1f;

    [SerializeField]
    float edgeScreenBuffer = 1f;

    [SerializeField]
    float minScreenSize = 6f;

    Vector3 velocity;
    Camera cam;
    float zoomSpeed;

    Vector3 avgPos = Vector3.zero;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }


    void LateUpdate()
    {
        CheckTargetsExist();
        MoveCamera();
        ZoomCamera();
    }

    void CheckTargetsExist()
    {
        bool foundError = false;
        do
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null)
                {
                    targets.RemoveAt(i);
                }
            }
        }
        while (foundError);
    }

    void MoveCamera()
    {
        avgPos = Vector3.zero;
        for (int i = 0; i < targets.Count; i++)
        {
            avgPos += targets[i].position;
        }

        if (targets.Count > 0)
            avgPos /= targets.Count;
        else return;

        avgPos.z = -10;

        transform.position = Vector3.Lerp(transform.position, avgPos, smoothing);

        transform.position = Vector3.SmoothDamp(transform.position, avgPos, ref velocity, smoothing);
    }

    void ZoomCamera()
    {
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, FindRequiredSize(), ref zoomSpeed, smoothing);
    }

    private float FindRequiredSize()
    {
        // Find the position the camera rig is moving towards in its local space.
        Vector3 desiredLocalPos = transform.InverseTransformPoint(avgPos);

        // Start the camera's size calculation at zero.
        float size = 0f;

        // Go through all the targets...
        for (int i = 0; i < targets.Count; i++)
        {

            // Otherwise, find the position of the target in the camera's local space.
            Vector3 targetLocalPos = transform.InverseTransformPoint(targets[i].position);

            // Find the position of the target from the desired position of the camera's local space.
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            // Choose the largest out of the current size and the distance of the tank 'up' or 'down' from the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            // Choose the largest out of the current size and the calculated size based on the tank being to the left or right of the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / cam.aspect);
        }

        // Add the edge buffer to the size.
        size += edgeScreenBuffer;

        // Make sure the camera's size isn't below the minimum.
        size = Mathf.Max(size, minScreenSize);

        return size;
    }
}

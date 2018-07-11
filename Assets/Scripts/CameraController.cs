using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private static CameraController cameraController;

    public static CameraController instance
    {
        get
        {
            if (!cameraController)
            {
                cameraController = Camera.main.GetComponent<CameraController>();
                if (!cameraController)
                {
                    cameraController = Camera.main.gameObject.AddComponent(typeof(CameraController)) as CameraController;
                    Debug.Log("Automatically added CameraController script to main camera, is this what you intended?");
                }
            }
            return cameraController;
        }
    }

    public static void Follow(GameObject obj) {
        Debug.Log("Camera::Follow::" + obj);
        instance.follow = obj;
    }

    public static void StopFollowing(GameObject obj) {
        Debug.Log("Camera::StopFollowing::" + obj);
        var i = instance;
        if (i.follow == obj) {
            i.follow = null;
        }
    }

    public static void Shake(float amount) {
        Debug.Log("Camera::Shake::"+amount);
        instance.shake = amount;
    }

    public static void StopShaking() {
        Shake(0);
    }

    public static void Target(Vector2 pos) {
        Debug.Log("Camera::Target::" + pos);
        instance.target = pos;
    }

    // Follow refers to a game object that the camera will continue to point at.
    // Follow takes precendence over the 'target' property.
    public GameObject follow;

    // The x/y position the camera should point at. If 'follow' is set this property
    // will be updated automatically to the position of the followed object.
    public Vector2 target = new Vector2(0f, 0f);

    public float shake = 0;
    public float shakeFalloff = .96f;

    void Update () 
    {
        Debug.Log("Follow="+follow);
        if (follow) {
            var followPos = follow.transform.position;
            target = new Vector2(followPos.x, followPos.y);
        }
        float currentz = transform.position.z;
        Vector2 current = new Vector2(transform.position.x, transform.position.y);
        Vector2 delta = (target - current) / 10f; 

        Vector3 newPosition = new Vector3(current.x + delta.x, current.y + delta.y, currentz);
        if (shake > 0) {
            newPosition.x += Random.Range(-shake, shake);
            newPosition.y += Random.Range(-shake, shake);
            shake = shake * shakeFalloff;
            if (shake < 0.01f) {
                shake = 0;
            }
        }
        transform.position = newPosition;
    }
}

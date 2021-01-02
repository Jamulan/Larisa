using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class SimpleCamaraController : MonoBehaviour
{

    class CameraState
    {
        public Vector3 orientation;
        public Vector3 position;

        public void SetFromTransform(Transform t)
        {
            orientation = t.eulerAngles;
            position = t.position;
        }

        public void UpdateTransform(Transform t)
        {
            t.eulerAngles = orientation;
            t.position = position;
        }
    }

    CameraState cameraState = new CameraState();
    
    // Start is called before the first frame update
    void OnEnable()
    {
        cameraState.SetFromTransform(transform);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Exit
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        // Rotation
        var mouseMovement = new Vector3(Input.GetAxis("Mouse Y") * -1, Input.GetAxis("Mouse X"), 0);

        cameraState.orientation += mouseMovement;
        cameraState.UpdateTransform(transform);
    }
}

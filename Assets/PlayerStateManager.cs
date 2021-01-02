using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public class PlayerState
    {
        public Vector3 pos;
        public Vector3 look;

        public void SetFromTransform(Transform t)
        {
            pos = t.position;
            look = t.eulerAngles;
        }

        public void UpdateTransform(Transform t)
        {
            t.position = pos;
            t.Find("Head").eulerAngles = look;
            t.Find("Body").eulerAngles = new Vector3(0, look.y, 0);
        }
    }

    private PlayerState playerState = new PlayerState();
    // Start is called before the first frame update
    void Start()
    {
        playerState.SetFromTransform(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        var mouseMovement = new Vector3(Input.GetAxis("Mouse Y") * -1, Input.GetAxis("Mouse X"), 0);

        playerState.look += mouseMovement;
        
        playerState.UpdateTransform(transform);
    }
}

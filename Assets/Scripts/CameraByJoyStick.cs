using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraByJoyStick : MonoBehaviour
{
    public GameObject Player;
    public GameObject lookAtTarget;
    public GameObject CameraObject;
    public VariableJoystick variableJoystick;
    public float TiltMax = 45.0f ;
    public float OrbitSpeed = 30.0f;
    public float distanceFromPlayer = 1.5f;
    public float lookatYOffset = 1.5f;
    public float CameraOrbitOn = 0.7f;
    public bool GamePad = false;
    float AngleFromPlayerForward = 180.0f;
    float cameraY;

    // Start is called before the first frame update
    void Start()
    {
        cameraY = gameObject.transform.position.y - Player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        float rotX = 0.0f;

        if (variableJoystick != null)
        {
            rotX = variableJoystick.Vertical * TiltMax * -1.0f;
            CameraObject.transform.localRotation = Quaternion.Euler(rotX, 0.0f, 0.0f);

            if (Mathf.Abs(variableJoystick.Horizontal) > CameraOrbitOn)
            {
                AngleFromPlayerForward += variableJoystick.Horizontal * OrbitSpeed * Time.deltaTime * -1.0f;
            }
        }
        if (GamePad)
        {
            rotX = Input.GetAxis("Vertical") * TiltMax * -1.0f;
            CameraObject.transform.localRotation = Quaternion.Euler(rotX, 0.0f, 0.0f);

            if (Mathf.Abs(Input.GetAxis("Horizontal")) > CameraOrbitOn)
            {
                AngleFromPlayerForward += Input.GetAxis("Horizontal") * OrbitSpeed * Time.deltaTime * -1.0f;
            }

        }

        Vector3 dir = Quaternion.Euler(0.0f, AngleFromPlayerForward, 0.0f) * Player.transform.forward;
        Vector3 pos = Player.transform.position + dir * distanceFromPlayer;
        pos.y += cameraY;
        gameObject.transform.position = pos;

        gameObject.transform.LookAt(lookAtTarget.transform);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapMovement : MonoBehaviour {

    [System.Serializable]
    public class InputInfo
    {
        public Vector3 carMovement = Vector3.zero;
        public float carRotation   = 0.0f;

        public Vector2 camRotation = Vector2.zero;
    }
    

    [System.Serializable]
    public class MovementInfo
    {
        public float gravity = -10.0f;
        public float speed   = 5.0f;

        public Vector3 currentVelocity = Vector3.zero;
    }

    [System.Serializable]
    public class RotationInfo
    {
        public float rotationSpeed   = 500.0f;

        public float currentRotation = 0.0f;
    }

    [System.Serializable]
    public class CamInfo
    {
        public float rotationSpeed = 10.0f;

        public float verticalAngleLock   = Mathf.PI / 3;
        public float horizontalAngleLock = Mathf.PI / 2;

        public Vector2 currentRotation = Vector2.zero;
    }

    public InputInfo mInput;
    public MovementInfo mMovement;
    public RotationInfo mRotation;
    public CamInfo mCam;

    CharacterController mChar;
    
	// Use this for initialization
	void Start () {
        mChar = GetComponent<CharacterController>();        
	}
	
	void Update () {
        GetInput();
	}

    void FixedUpdate()
    {
        mRotation.currentRotation = mInput.carRotation;
        mChar.transform.Rotate(0.0f, mRotation.currentRotation * mRotation.rotationSpeed * Time.fixedDeltaTime, 0.0f);

        Quaternion transformRotation = mChar.transform.rotation;
        mMovement.currentVelocity = transformRotation * mInput.carMovement;
        mChar.Move(mMovement.currentVelocity * mMovement.speed * Time.fixedDeltaTime);

        mCam.currentRotation = mInput.camRotation;
    }

    void GetInput()
    {
        mInput.carMovement.x = Input.GetAxis("Horizontal");
        mInput.carMovement.z = Input.GetAxis("Vertical");

        mInput.carRotation = Input.GetAxis("Mouse X");
        
        // TODO: CamRotation
        // Which input (?)
    }
}

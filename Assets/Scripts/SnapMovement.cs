using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controls the SnapCar movement:
 * 1.- Omnidirectional movement ('forward' defined as the direction the model is facing)
 * 2.- Turn around
 * 3.- Look around
 */

public class SnapMovement : MonoBehaviour {

    [System.Serializable]
    public class InputInfo
    {
        public Vector3 carMovement = Vector3.zero;

        public bool turnLeft  = false;
        public bool turnRight = false;

        public float lookHorizontal = 0.0f;
        public float lookVertical = 0.0f;
    }
    

    [System.Serializable]
    public class MovementInfo
    {
        public float gravity = -10.0f;
        public float speed   = 5.0f;
    }

    [System.Serializable]
    public class RotationInfo
    {
        public float rotationSpeed   = 100.0f;
    }

    [System.Serializable]
    public class CamInfo
    {
        public float rotationSpeed = 100.0f;

        public float verticalAngleMin = 30.0f;
        public float verticalAngleMax = 10.0f;

        public float horizontalAngleMin = 60.0f;
        public float horizontalAngleMax = 60.0f;

        public Vector2 currentRotation = Vector2.zero;
    }

    public InputInfo mInput;
    public MovementInfo mMovement;
    public RotationInfo mRotation;
    public CamInfo mCam;

    CharacterController mChar;
    Transform mModel;
    Transform mCamera;
    
	// Use this for initialization
	void Start () {
        mChar = GetComponent<CharacterController>();
        mModel = transform.Find("Model");
        mCamera = transform.Find("Main Camera");
    }
	
	void Update () {
        GetInput();
	}

    void FixedUpdate()
    {
        UpdateMovement();
        UpdateTurn();
        UpdateLook();
    }

    void GetInput()
    {
        /*
         * Omnidirectional movement using { Left, Right, Up, Down } or { W, A, S, D }
         */
        mInput.carMovement.x = Input.GetAxis("Horizontal");
        mInput.carMovement.z = Input.GetAxis("Vertical");

        /*
         * Turn around using { J, L }
         */
        mInput.turnLeft  = Input.GetKey("j");
        mInput.turnRight = Input.GetKey("l");

        /*
         * Look around using mouse movement
         */
        mInput.lookHorizontal = Input.GetAxis("Mouse X");
        mInput.lookVertical = Input.GetAxis("Mouse Y");
    }

    void UpdateMovement()
    {
        Quaternion transformRotation = mModel.rotation;

        Vector3 velocity = transformRotation * mInput.carMovement;
        mChar.Move(velocity * mMovement.speed * Time.fixedDeltaTime);
    }

    void UpdateTurn()
    {
        float rotation = 0.0f;

        if (mInput.turnLeft)
            rotation = mRotation.rotationSpeed * (-1);
        else if (mInput.turnRight)
            rotation = mRotation.rotationSpeed;
        
        mModel.Rotate(0.0f, rotation * Time.fixedDeltaTime, 0.0f);
    }

    void UpdateLook()
    {
    
        mCam.currentRotation.x += mInput.lookVertical * mCam.rotationSpeed * Time.fixedDeltaTime * (-1);
        mCam.currentRotation.x = Mathf.Clamp(mCam.currentRotation.x, -mCam.verticalAngleMin, mCam.verticalAngleMax);

        mCam.currentRotation.y += mInput.lookHorizontal * mCam.rotationSpeed * Time.fixedDeltaTime;
        mCam.currentRotation.y = Mathf.Clamp(mCam.currentRotation.y, -mCam.horizontalAngleMin, mCam.horizontalAngleMax);

        mCamera.eulerAngles = mCam.currentRotation;
    }
}

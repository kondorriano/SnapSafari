using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapMovement : MonoBehaviour {

    [System.Serializable]
    public class InputMove
    {
        public float vAxis = 0;
        public float hAxis = 0;
    }

    [System.Serializable]
    public class PhysicsMove
    {
        public float gravity = -10f;
        public Vector3 velocity = Vector3.zero;
        public float speed = 5f;
    }


    public InputMove myInput;
    public PhysicsMove myPhysics;

    CharacterController myChar;
    

	// Use this for initialization
	void Start () {
        myChar = GetComponent<CharacterController>();

        
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
	}

    void FixedUpdate()
    {
        SetMovement();
        myChar.Move(myPhysics.velocity*Time.fixedDeltaTime);
    }

    void GetInput()
    {
        myInput.hAxis = Input.GetAxis("Horizontal");
        myInput.vAxis = Input.GetAxis("Vertical");

    }

    void SetMovement()
    {
        myPhysics.velocity = new Vector3(myInput.hAxis, 0, myInput.vAxis)*myPhysics.speed + Vector3.up*myPhysics.gravity;
    }
}

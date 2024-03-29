﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool DebugOn;

	public float GroundProximityTolerence;
	public float TestRadius;

	public float RollForce;
	public float JumpForce;
	public float TweakValue;

	public GameObject MainCam;
	public GameObject CamOrbit;
	public GameObject CameraRestingPos;
	public GameObject CameraResetPosition;

	public Vector3 CamOffset;
	public float CamAngle;
	public float MouseSensitivity;

	private Rigidbody rb;
	public bool grounded;
	private float HorizontlComponent;
	private float VerticalComponent;
	private Vector3 MovementVector;
	private float OrbitAngle;

	private bool CameraCollision;

	private GameObject gc;

	public bool useSeeThrough;
	private GameObject objectHit;

	public Material defaultMaterial;
	public Material hitMaterial;
	public Material previousMaterial;

	[HideInInspector] public Vector3 ResetPosition;
	[HideInInspector] public Quaternion ResetRotation; // not actually used yet

	public AudioSource rollingBall;
    public bool playingSound = false;


    void Start ()
	{
		DebugOn = true;

		rb = GetComponent<Rigidbody> ();
		gc = GameObject.FindGameObjectWithTag ("GameController");
		ResetPosition = gc.GetComponent<GameController> ().LevelStart.transform.position + gc.GetComponent<GameController> ().StartOffset;

		ResetRotation = Quaternion.identity; // because I'm lazy... eventually change this to the orientation of the start object
	}

	void FixedUpdate ()
	{
		HandleInput ();
		UpdateCamera ();

		if (DebugOn == true)
		{
			Debug.DrawRay (transform.position, Vector3.down * GroundProximityTolerence, Color.green);
			Debug.DrawRay (transform.position, (transform.position - CameraRestingPos.transform.position)*-1, Color.yellow);
		}
	}
	private void Update()
	{
		StartCoroutine(RollSound());
		Debug.LogWarning("The ball is rolling");
	}


	private IEnumerator RollSound()
	{
        
		if (grounded && playingSound == false)
		{
			playingSound = true;
			rollingBall.Play();
			yield return new WaitForSeconds(5f);
			playingSound = false;
			//StopAllCoroutines();
		}
		else
		{
			rollingBall.Stop();
		}
		
	}

	private bool CheckGrounded()
	{
		if (Physics.Raycast (transform.position, Vector3.down, GroundProximityTolerence))
        {
			grounded = true;
			return true;
		}
        else
        {
			grounded = false;
			return false;
		}
	}

	private void HandleInput()
	{
		if (Input.GetKey (KeyCode.R))
			ResetPlayer ();

		if (CheckGrounded() == true)
			if (Input.GetKey (KeyCode.Space))
				rb.AddForce (0, JumpForce, 0, ForceMode.Impulse);

		HorizontlComponent = (Input.GetAxis("Horizontal"));
		VerticalComponent = (Input.GetAxis("Vertical"));
		MovementVector = new Vector3 (HorizontlComponent, 0, VerticalComponent);
		MovementVector = Quaternion.AngleAxis (OrbitAngle, Vector3.up) * MovementVector;
		// NEXT LINE IS NEW - BIT JANK
		// MovementVector = Quaternion.AngleAxis(CamAngle, Vector3.right) * MovementVector;
		MovementVector.Normalize ();

		rb.AddForce (MovementVector, ForceMode.Impulse);
		rb.AddForce (MovementVector*TweakValue, ForceMode.VelocityChange);

		OrbitAngle += (Input.GetAxis("Mouse X")*MouseSensitivity);

		// NEXT LINE ALLOWS LOOKING UP AND DOWN, BUT DOES NOT ROTATE CAMERA AROUND PLAYER. ALSO NEEDS CLAMP.
		// CamAngle -= (Input.GetAxis("Mouse Y") * MouseSensitivity);

		if (Mathf.Abs (Input.GetAxis ("Mouse X")) > 0)
			CameraCollision = false;
	}

	private void UpdateCamera ()
	{
		CamOrbit.transform.position = transform.position;

		if (CameraCollision == false)
		{
			MainCam.transform.position = transform.position + CamOffset;
			MainCam.transform.eulerAngles = new Vector3 (CamAngle, 0, 0);
			CamOrbit.transform.Rotate (0, OrbitAngle, 0);
		}

		CheckCamCollision ();
	}

	private void CheckCamCollision()
	{
		Vector3 a = transform.position;
		Vector3 b = (transform.position - CameraRestingPos.transform.position) * -1;
		float l = Vector3.Magnitude (transform.position - CameraRestingPos.transform.position);
		RaycastHit h;
		Ray r = new Ray (a, b); 
		if (!useSeeThrough)
        {
			if (Physics.Raycast(r, out h, l))
				CameraRestingPos.transform.position = h.point;
			else
				CameraRestingPos.transform.position = CameraResetPosition.transform.position;

			CameraCollision = Physics.Raycast(r, l);

			MainCam.transform.position = CameraRestingPos.transform.position;
		}
		else
        {
			if (Physics.Raycast(r, out h, l))
			{
				previousMaterial = defaultMaterial;
				if (h.transform.gameObject.GetComponent<Renderer>().sharedMaterial != hitMaterial)
				{
					defaultMaterial = h.transform.gameObject.GetComponent<Renderer>().material;
				}
				if (h.transform.gameObject != objectHit && objectHit != null)
				{
					objectHit.transform.gameObject.GetComponent<MeshRenderer>().material = previousMaterial;
				}
				objectHit = h.transform.gameObject;
				objectHit.GetComponent<MeshRenderer>().material = hitMaterial;
			}
			else
			{
				if (objectHit != null)
				{
					objectHit.transform.gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
				}
			}
			CameraCollision = Physics.Raycast(r, l);
		}
		
	}

	public void ResetPlayer()
	{
		rb.angularVelocity = Vector3.zero;
		rb.velocity = Vector3.zero;
		transform.position = ResetPosition; //gc.GetComponent<GameController> ().LevelStart.transform.position+gc.GetComponent<GameController> ().StartOffset;

		//OrbitAngle = 0.0f;
		//OrbitAngle = ResetRotation;
	}
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class AI1Character : MonoBehaviour
{
	[SerializeField] float movingTurnSpeed = 360;
	[SerializeField] float stationaryTurnSpeed = 180;
	[SerializeField] float jumpPower = 12f;
	[SerializeField] float groundCheckDistance = 0.1f;

	Rigidbody rigidbodyAi;
	//Animator animator;
	bool isGrounded;
	float origGroundCheckDistance;
	const float k_Half = 0.5f;
	float turnAmount;
	float forwardAmount;
	Vector3 groundNormal;
	float capsuleHeight;
	Vector3 capsuleCenter;
	CapsuleCollider capsule;
	bool crouching;


	void Start()
	{
        //animator = GetComponent<Animator>();
        rigidbodyAi = GetComponent<Rigidbody>();
		capsule = GetComponent<CapsuleCollider>();
		capsuleHeight = capsule.height;
		capsuleCenter = capsule.center;
        rigidbodyAi.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		origGroundCheckDistance = groundCheckDistance;
	}


	public void Move(Vector3 move, bool crouch, bool jump)
	{
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f) move.Normalize(); 
		move = transform.InverseTransformDirection(move);
		CheckGroundStatus();
		move = Vector3.ProjectOnPlane(move, groundNormal);
		turnAmount = Mathf.Atan2(move.x, move.z);
		forwardAmount = move.z;

		ApplyExtraTurnRotation();

		ScaleCapsuleForCrouching(crouch);
		PreventStandingInLowHeadroom();
	}


	void ScaleCapsuleForCrouching(bool crouch)
	{
		if (isGrounded && crouch)
		{
			if (crouching) return;
			capsule.height = capsule.height / 2f;
			capsule.center = capsule.center / 2f;
			crouching = true;
		}
		else
		{
			Ray crouchRay = new Ray(rigidbodyAi.position + Vector3.up * capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = capsuleHeight - capsule.radius * k_Half;
			if (Physics.SphereCast(crouchRay, capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
			{
				crouching = true;
				return;
			}
			capsule.height = capsuleHeight;
			capsule.center = capsuleCenter;
			crouching = false;
		}
	}

	void PreventStandingInLowHeadroom()
	{
		// prevent standing up in crouch-only zones
		if (!crouching)
		{
			Ray crouchRay = new Ray(rigidbodyAi.position + Vector3.up * capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = capsuleHeight - capsule.radius * k_Half;
			if (Physics.SphereCast(crouchRay, capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
			{
				crouching = true;
			}
		}
	}

	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
		transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
	}

	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
		{
			groundNormal = hitInfo.normal;
			isGrounded = true;
			//animator.applyRootMotion = true;
		}
		else
		{
			isGrounded = false;
			groundNormal = Vector3.up;
			//animator.applyRootMotion = false;
		}
	}
}


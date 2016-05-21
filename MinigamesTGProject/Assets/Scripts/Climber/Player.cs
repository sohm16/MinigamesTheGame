using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public string playerName;

	//Inspector editable properties for setting physics properties
	public float jumpHeight;
	public float timeToJumpApex;
	public float accelerationTimeAirborne;
	public float accelerationTimeGrounded;
	public float moveSpeed;

	public Vector2 wallJumpClimb;
	public Vector2 wallDrop;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax;
	public float wallStickTime = .2f;

	[HideInInspector]
	public bool stop;

	private float wallTimeUnstick;

	//physics related properties
	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	private Controller2D controller;

	private string horizontal;
	private string vertical;
	private string button;

	void Start () {
		controller = GetComponent<Controller2D> ();
		stop = false;

		gravity = - (2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs (gravity * timeToJumpApex);

		horizontal = playerName + "Horizontal";
		vertical = playerName + "Vertical";
		button = playerName + "Button";
	}

	void Update () {
		if (stop)
			return;

		Vector2 input = new Vector2 (Input.GetAxisRaw (horizontal), Input.GetAxisRaw (vertical));
		int wallDirX = (controller.collisions.left) ? -1 : 1;

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

		bool wallSliding = false;

		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < - wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (wallTimeUnstick > 0) {

				velocityXSmoothing = 0;
				velocity.x = 0f;

				if (input.x != wallDirX && input.x != 0) {
					wallTimeUnstick -= Time.deltaTime;
				} else {
					wallTimeUnstick = wallStickTime;
				}
			} else {
				wallTimeUnstick = wallStickTime;
			}
		}

		if (controller.collisions.above || controller.collisions.below)
			velocity.y = 0;

		if (Input.GetButtonDown (button)) {
			if (wallSliding) {
				if (wallDirX == input.x) {
					velocity.x = -wallDirX * wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				} else if (input.x == 0) {
					velocity.x = -wallDirX * wallDrop.x;
					velocity.y = wallDrop.y;
				} else {
					velocity.x = -wallDirX * wallLeap.x;
					velocity.y = wallLeap.y;
				}
			}
			if (controller.collisions.below){
				velocity.y = jumpVelocity;
			}
		}

		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour {

	public float speed = 1500f;
	public float rotationSpeed = 15f;

	public WheelJoint2D backWheel;
	public WheelJoint2D frontWheel;

	public Rigidbody2D rb;

	private float movement = 0f;
	private float rotation = 0f;

	//public GameObject backpack;//子彈預設物
	public Transform fire;//宣告一個變形對應到發射位置
	public backpackController backpack;
	public List <backpackController> backpack_list;

	private float x = 0.29f;//size of the backpack
	private float y = 0.30f;
	private float z = 1.17f;

	public float jumpSpeed = 5000f;


	void Start()
	{
		backpack_list = new List<backpackController> ();
	}

	void Update ()
	{
		movement = -Input.GetAxisRaw("Vertical") * speed;
		rotation = Input.GetAxisRaw("Horizontal");


	}

	void FixedUpdate ()
	{
		if (movement == 0f)
		{
			backWheel.useMotor = false;
			frontWheel.useMotor = false;
		} else
		{
			backWheel.useMotor = true;
			frontWheel.useMotor = true;

			JointMotor2D motor = new JointMotor2D { motorSpeed = movement, maxMotorTorque = 10000 };
			backWheel.motor = motor;
			frontWheel.motor = motor;
		}

		if (Input.GetMouseButtonDown(0) ) {
			var obj = Instantiate(backpack, fire.position, fire.rotation);
			obj.transform.localScale = new Vector3(x, y, z);
			backpack_list.Add (obj);

		}

		if (Input.GetKeyDown(KeyCode.Z)){
			GetComponent<Rigidbody2D>().AddForce(new Vector2( 0,jumpSpeed));
			Debug.Log ("jump");

		}
		rb.AddTorque(-rotation * rotationSpeed * Time.fixedDeltaTime);
    }

}

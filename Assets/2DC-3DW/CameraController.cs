using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
		public Transform target;
		public float angleY = 35;
		public bool InvertY;
		public float rotationSmoothing = 10;
		public float rotationSensitivity = 7;
		public float distance = 10;
		public float VerticalRotLimit = 40;

		private Vector3 _angle = new Vector3();
		private Quaternion _oldRotation = new Quaternion();

		private Transform _t;

		public Vector2 CurrentRotation { get { return _angle; } }

		void Start()
		{
			_t = transform;
			_oldRotation = _t.rotation;//asigna la rotacion de la camara
			_angle.y = angleY;
		}

		public void ClampAngle(ref Vector3 angle)
		{
			if(angle.x < -180) angle.x += 360;
			else if(angle.x > 180) angle.x -= 360;

			if(angle.y < -VerticalRotLimit) angle.y = -VerticalRotLimit;
			else if(angle.y > VerticalRotLimit) angle.y = VerticalRotLimit;

			if(angle.z < -180) angle.z += 360;
			else if(angle.z > 180) angle.z -= 360;
		}

		void Update()
		{
			if(target && Input.GetMouseButton(1))
			{
				_angle.x += Input.GetAxis("Mouse X") * rotationSensitivity;

				if (InvertY){
				  _angle.y -= Input.GetAxis("Mouse Y") * rotationSensitivity;
				}else{
				  _angle.y += Input.GetAxis("Mouse Y") * rotationSensitivity;
				}
				
				ClampAngle(ref _angle);
				
			}/*else{
				_angle.x += Input.GetAxis("Horizontal2")* rotationSensitivity;
				_angle.y += Input.GetAxis("Vertical2")* rotationSensitivity;
				ClampAngle(ref _angle);
			}*/
					 
				
		}

		void LateUpdate()
		{
			if(target)
			{
				Quaternion angleRotation = Quaternion.Euler(_angle.y, _angle.x, 0);
				Quaternion currentRotation = Quaternion.Lerp(_oldRotation, angleRotation, Time.deltaTime * rotationSmoothing);

				_oldRotation = currentRotation;

				_t.position = target.position - currentRotation * Vector3.forward * distance;
				_t.LookAt(target.position, Vector3.up);
			}
		}
	
}
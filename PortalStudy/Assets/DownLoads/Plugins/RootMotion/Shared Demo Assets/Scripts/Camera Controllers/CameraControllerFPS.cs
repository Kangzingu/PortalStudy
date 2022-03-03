using UnityEngine;
using System.Collections;

namespace RootMotion {

	/// <summary>
	/// The very simple FPS camera.
	/// </summary>
	public class CameraControllerFPS: MonoBehaviour {

        private Animator avatarAnimator;
		public float rotationSensitivity = 3f;
		public float yMinLimit = -89f;
		public float yMaxLimit = 89f;
        public Rigidbody character;
        public GameObject head;
		public float x, y;

        void Awake ()
        {
            avatarAnimator = character.GetComponent<Animator>();
            Vector3 angles = transform.eulerAngles;
			x = angles.y;
			y = angles.x;
		}

		public void LateUpdate() {
			Cursor.lockState = CursorLockMode.Locked;

			x += Input.GetAxis("Mouse X") * rotationSensitivity;
            y = ClampAngle(y - Input.GetAxis("Mouse Y") * rotationSensitivity, yMinLimit, yMaxLimit);
            avatarAnimator.SetFloat("mouseX", Input.GetAxis("Mouse X") * rotationSensitivity);
            // Rotation
            transform.rotation = Quaternion.AngleAxis(x, Vector3.up) * Quaternion.AngleAxis(y, Vector3.right);
            transform.position = head.transform.position;
            character.transform.rotation = Quaternion.AngleAxis(x, Vector3.up);
		}
        // Clamping Euler angles
		private float ClampAngle (float angle, float min, float max) {
			if (angle < -360) angle += 360;
			if (angle > 360) angle -= 360;
			return Mathf.Clamp (angle, min, max);
		}

	}
}

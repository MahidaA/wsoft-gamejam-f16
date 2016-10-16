using UnityEngine;
using System.Collections;

public class SecurityCameraModel : MonoBehaviour {

	public SecurityCamera camera;

	void LateUpdate(){
		transform.eulerAngles=new Vector3(-camera.cameraFOV.transform.eulerAngles.z-90, 90, 0);
	}

}

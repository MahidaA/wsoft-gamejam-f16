using UnityEngine;
using System.Collections;

public class SecurityCamera : MonoBehaviour {

	public float angleRange;
	public float rotationSpeed;
	public float waitTime;
	public float distModifier=1;

	public GameObject cameraFOV;

	private float defaultAngle;
	private int rotateDir=1;
	private float timeLeft;

	public bool active;

	void Start(){
		cameraFOV=(GameObject)Instantiate(cameraFOV);
		cameraFOV.transform.position=transform.position;
		defaultAngle=transform.eulerAngles.z;
		cameraFOV.transform.eulerAngles=new Vector3(0,0,defaultAngle+angleRange);
		cameraFOV.transform.localScale*=distModifier;
		distModifier=1;
	}

	// Update is called once per frame
	void Update () {

		cameraFOV.SetActive(active);
		if(!active)
			return;

		if(timeLeft<=0&&Mathf.Abs(defaultAngle-transform.eulerAngles.z)<angleRange){
			cameraFOV.transform.eulerAngles+=rotationSpeed*Time.deltaTime*new Vector3(0,0,rotateDir);
			if(Mathf.Abs(Mathf.DeltaAngle(defaultAngle, cameraFOV.transform.eulerAngles.z))>=angleRange){
				cameraFOV.transform.eulerAngles=new Vector3(0,0,defaultAngle+angleRange*rotateDir);
				rotateDir*=-1;
				timeLeft=waitTime;
			}
		}else if(timeLeft>0){
			timeLeft-=Time.deltaTime;
			if(timeLeft<=0){
				cameraFOV.transform.eulerAngles+=new Vector3(0,0,rotationSpeed*Time.deltaTime*rotateDir);
			}
		}
	}

	void OnDrawGizmos(){
		if(cameraFOV==null) return;

		float angle=transform.eulerAngles.z*Mathf.Deg2Rad;
		Vector3 scale=cameraFOV.transform.lossyScale;
		float FOV=Mathf.Atan2(scale.y/2, scale.x);
		angle+=angleRange*Mathf.Deg2Rad+FOV;
		float dist=new Vector2(scale.x, scale.y).magnitude*2*distModifier;

		Gizmos.color=Color.red;
		Gizmos.DrawLine(transform.position, transform.position+new Vector3(Mathf.Cos(angle)*dist,
			Mathf.Sin(angle)*dist));
		angle-=2*FOV;
		Gizmos.DrawLine(transform.position, transform.position+new Vector3(Mathf.Cos(angle)*dist,
			Mathf.Sin(angle)*dist));

		angle-=2*angleRange*Mathf.Deg2Rad;
		Gizmos.DrawLine(transform.position, transform.position+new Vector3(Mathf.Cos(angle)*dist,
			Mathf.Sin(angle)*dist));
		angle+=2*FOV;
		Gizmos.DrawLine(transform.position, transform.position+new Vector3(Mathf.Cos(angle)*dist,
			Mathf.Sin(angle)*dist));
	}
}

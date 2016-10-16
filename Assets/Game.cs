﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	private Coroutine routine; 

	private float alpha=0;
	private Texture2D tex;
	private bool over;

	void Start(){
		tex = new Texture2D(1, 1);
	}

	public void gameOver(){
		over=true;
		if(routine==null){
			alpha=0;
			Time.timeScale=0.5F;
			routine=StartCoroutine(fadeToBlack());
		}
	}

	private IEnumerator fadeToBlack(){
		while(alpha<1){
			alpha+=Time.deltaTime;
			tex.SetPixel(0,0,new Color(0,0,0,alpha));
			tex.Apply();
			yield return new WaitForEndOfFrame();
		}
		Time.timeScale=1;
		yield return new WaitForSecondsRealtime(2);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void OnPostRender(){
		if(!over)
			return;
		
		GL.PushMatrix();
		GL.LoadOrtho();
		Graphics.DrawTexture(new Rect(0,0,1, 1), tex);
		GL.PopMatrix();
	}
}
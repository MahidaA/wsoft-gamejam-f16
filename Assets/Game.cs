using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	public static readonly string[] LEVELS=new string[]{"scene_level1", "scene_level2", "scene_level3", "scene_levelFinal"};
	private static int currentLevel=0;

    private Coroutine routine;

    public AudioSource detected;

	private float alpha=0;
	private Texture2D tex;
	private bool over;

	void Start(){
		tex = new Texture2D(1, 1);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.U))
			SceneManager.LoadScene(LEVELS[0]);
	}

	public void nextLevel(){
		over=true;
        if (routine==null){
			alpha=0;
			Time.timeScale=0.5F;
			currentLevel++;
			routine=StartCoroutine(fadeToBlack(LEVELS[currentLevel]));
		}
	}

	public void gameOver(){
		over=true;
        detected.Play();
        if (routine==null){
			alpha=0;
			Time.timeScale=0.5F;
			routine=StartCoroutine(fadeToBlack(SceneManager.GetActiveScene().name));
		}
	}

	private IEnumerator fadeToBlack(string sceneToLoad){
		while(alpha<1){
			alpha+=Time.deltaTime;
			tex.SetPixel(0,0,new Color(0,0,0,alpha));
			tex.Apply();
			yield return new WaitForEndOfFrame();
		}
		Time.timeScale=1;
		yield return new WaitForSecondsRealtime(2);
		SceneManager.LoadScene(sceneToLoad);
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

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ElevatorScript : MonoBehaviour {
	
	public bool moving;
	public float  y;
	Animator anim;
	public float speed;
	public GameObject Camera;
	public CameraController camScript;
	public Vector3 target;
	private int CurrSel;
	public GameObject ElevCanvas;
	public Text ElevText;
	private bool go;

	IEnumerator Start () {
		anim = GetComponent<Animator> ();
		moving = false;
		go = false;
		camScript = Camera.GetComponent<CameraController>();
		CurrSel = 0;
		ElevCanvas.SetActive (false);
		for (int i = 1; i > 0; i++) {
			yield return StartCoroutine (waiting ());									//do nothing until the player wants to use the elevator
			yield return new WaitForSeconds (0.1f);
			yield return selFloor ();													//select target floor
			ElevCanvas.SetActive (false);
			go = true;																	//start movement
			yield return new WaitForSeconds (0.1f);
		}
	}

	void Update () {
		
		if (go) {															//move with F only if the player is in the elevator
			anim.Play ("CloseDoors");
			target = new Vector3 (0, CurrSel * 7.5f, -8.25f);
			moving = true;
			go = false;
		}

		if (moving) {
			Vector3 dir = target - this.transform.position;
			float distFrame = speed * Time.deltaTime;
			if (dir.magnitude < distFrame) {												// move exactly to destination position
				this.transform.position = target;
				moving = false;
				anim.Play ("OpenDoors");
			} else {
					this.transform.Translate (dir.normalized * distFrame, Space.World);
				}
		}

	}

	IEnumerator selFloor() {
		while (!Input.GetKeyDown(KeyCode.F)) {
			if (Camera.transform.parent == null) {
				ElevCanvas.SetActive (false);
				yield return StartCoroutine (waitreturn ());
			}
			ElevCanvas.SetActive(true);
			ElevText.text = "Please select the floor you want to go to: \n0: Kitchen\n1: Bread & Pasta\n2: Meat & Eggs\n3: Seafood\n4:Cheese\n5: Spices" +
				"\n6: Vegetables\n7: Fruit\n8: Dressings\n9: Sweets\nYour current choice is floor number " + CurrSel;
			if (Input.GetKeyDown(KeyCode.G))
				CurrSel++;
			if (CurrSel == 10)
				CurrSel = 0;
			yield return null;
		}
	}

	IEnumerator waiting () {
		while (!(Input.GetKeyDown (KeyCode.F) && Camera.transform.parent != null && !moving))
			yield return null;
	}
			
	IEnumerator waitreturn () {
		while (Camera.transform.parent == null)
			yield return null;
	}
}
	





	
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using HMD;

public class CameraController : MonoBehaviour {
	//public float Tspeed;														//translational speed
	//public float Rspeed;														//rotational speed

	public struct Gains {														//possible gains and repetitions needed
		public float[] values;
		public int[] times;
	}
	public Gains gains;
	public bool changeGain;



	public struct ing {															//definition of the ingredient variable
		public string name;
		public float floor;
	}
	public ing Bread, BreadCrumbs, Rice, Pasta, Flour;
	public ing Ham, ChickenBreast, ChickenWings, PorkFillet, Eggs;
	public ing Squids, Mussels, Prawns, Cod;
	public ing Butter, Mozzarella, Feta, Scamorza, Parmesan;
	public ing Paprika, Salt, ChiliPepper, Ginger, Pepper;
	public ing Onions, Garlic, Tomatoes, Eggplants, Zucchini;
	public ing Melon, Peaches, Hazelnuts, Apples, Oranges;
	public ing OliveOil, Vinegar, Truffles, Basil, Mint;
	public ing Honey, Chocolate, Sugar, Yogurt;
	public ing end;

	public struct recipe {														//how a recipe is defined
		public ing[] ingredients;
		public string name;
	}
	public recipe app1, app2, app3, app4;
	public recipe fc1, fc2, fc3, fc4, fc5;
	public recipe sc1, sc2, sc3, sc4, sc5;
	public recipe de1, de2, de3, de4;

	public string zone;
	public float CurrGain;
	public Text txt;
	public GameObject canvas;
	public bool active = true;
	public float time = 0f;
	public bool counting = false;
	private int CurrSel;
	public recipe CurrRec;
	public ing CurrIng;

	public ITracker currentTracker;
	MovementController cameraMovementController;
	CurvatureRedirTracker redirTracker;
	DataLogger dataLogger;
	GameObjectData cameraData;
	void Awake()
	{
		redirTracker = FindObjectOfType<CurvatureRedirTracker> ();
		cameraMovementController = FindObjectOfType<MovementController> ();

		if (currentTracker != null)
			cameraMovementController.SetTracker (currentTracker);
		else {
			cameraMovementController.SetTracker (redirTracker);
			currentTracker = redirTracker;	
		}
		// cameraData = mainCamera.FindObjectOfType<GameObjectData>();
		dataLogger = FindObjectOfType<DataLogger> ();
		dataLogger.StudyData.Add (cameraData);
		cameraData.IsRecorded = true;
	}
	IEnumerator Start () {
		initialize();															//initialize recipes and ingredients
		gains.values = new float[] { 0.8f, 0.9f, 1.0f, 1.2f, 1.4f };
		gains.times = new int[] { 5, 5, 5, 5, 5 };
		changeGain = false;
		CurrGain = newgain();
		yield return StartCoroutine (appetizers ());
		yield return StartCoroutine (firstcourses());
		yield return StartCoroutine (secondcourses());
		yield return StartCoroutine (desserts());
		txt.text = "Congratulations, your fabolous dinner is ready! \n\n Buon Appetito!";
		yield return null;
	}

	void Update () {													

		/*if (Input.GetKey(KeyCode.W))											//move and rotate with WASD, move laterally with QE
			transform.Translate(0, 0, Tspeed * Time.deltaTime * CurrGain);
		if (Input.GetKey(KeyCode.S))
			transform.Translate(0, 0, -Tspeed * Time.deltaTime * CurrGain);
		if (Input.GetKey(KeyCode.A))
			transform.Rotate(-Vector3.up * Rspeed * Time.deltaTime);
		if (Input.GetKey(KeyCode.D))
			transform.Rotate(Vector3.up * Rspeed * Time.deltaTime);		
		if (Input.GetKey(KeyCode.Q))
			transform.Translate(-Tspeed * Time.deltaTime * CurrGain, 0, 0);		
		if (Input.GetKey(KeyCode.E))
			transform.Translate(Tspeed * Time.deltaTime * CurrGain, 0, 0);
*/
		if (Input.GetKey (KeyCode.T) && !active) {												//if T is pressed, activate text for 4 seconds
			active = true;
			canvas.SetActive (true);
			time = 4f;
			counting = true;
		}
		if (time > 0f)
			time -= Time.deltaTime;
		if (!(time > 0f) && counting == true) {
			active = false;
			canvas.SetActive (false);
			counting = false;
		}

		if (changeGain) {															//update gain if needed (commanded by CubeCollisions Script)
			CheckEmpty ();
			CurrGain = newgain ();
			changeGain = false;
		}

	}

	IEnumerator appetizers () {														//appetizers subroutine
		CurrSel = 1;
		yield return StartCoroutine (selectApp ());
		if (CurrSel == 1)
			CurrRec = app1;
		if (CurrSel == 2)
			CurrRec = app2;
		if (CurrSel == 3)
			CurrRec = app3;
		if (CurrSel == 4)
			CurrRec = app4;
		yield return StartCoroutine(cook (CurrRec));
	}

	IEnumerator firstcourses() {													//first courses subroutine
		CurrSel = 1;
		yield return StartCoroutine (selectFir ());
		if (CurrSel == 1)
			CurrRec = fc1;
		if (CurrSel == 2)
			CurrRec = fc2;
		if (CurrSel == 3)
			CurrRec = fc3;
		if (CurrSel == 4)
			CurrRec = fc4;
		if (CurrSel == 5)
			CurrRec = fc5;
		yield return StartCoroutine(cook (CurrRec));
	}

	IEnumerator secondcourses() {													//second courses subroutine
		CurrSel = 1;
		yield return StartCoroutine (selectSec ());
		if (CurrSel == 1)
			CurrRec = sc1;
		if (CurrSel == 2)
			CurrRec = sc2;
		if (CurrSel == 3)
			CurrRec = sc3;
		if (CurrSel == 4)
			CurrRec = sc4;
		if (CurrSel == 5)
			CurrRec = sc5;
		yield return StartCoroutine(cook (CurrRec));
	}

	IEnumerator desserts () {														//desserts subroutine
		CurrSel = 1;
		yield return StartCoroutine (selectDes ());
		if (CurrSel == 1)
			CurrRec = de1;
		if (CurrSel == 2)
			CurrRec = de2;
		if (CurrSel == 3)
			CurrRec = de3;
		if (CurrSel == 4)
			CurrRec = de4;
		yield return StartCoroutine(cook (CurrRec));
	}

	IEnumerator cook(recipe CurrRec) {														//gets all the ingredients for the current recipe
		int t = 0;
		for (int i = 0; CurrRec.ingredients[i].name != "end"; i++)
			t++;
		for (int i = 0; i < t; i++) {
			active = true;
			canvas.SetActive (true);
			time = 0f;		//in case the player picks an object up shortly after having called the text back
			counting = false;
			CurrIng = CurrRec.ingredients [i];
			txt.text = "You are currently cooking " + CurrRec.name + ".\n\n Please, go fetch some " + CurrIng.name + ".";
			yield return StartCoroutine (waitpickup ());
		}
		active = true;
		canvas.SetActive (true);
		counting = false;
		time = 0f;
		txt.text = "You have collected everything, go drop it in the kitchen.";
		CurrIng = end;
		yield return StartCoroutine (waitpickup ());
		yield return new WaitForSeconds (0.1f);
		active = true;
		canvas.SetActive (true);
		time = 0f;
		counting = false;
	}
	

	IEnumerator waitpickup () {														//stop script until CurrIng is picked up
		yield return new WaitForSeconds (4f);										//disable text after 3 seconds
		active = false;
		canvas.SetActive (false);
		while (!(Input.GetKeyDown(KeyCode.F) && (zone == CurrIng.name)))				//press F to pick up ingredients
			yield return null;
	}
		

	IEnumerator selectApp () {													//stop script until a valid input is given
		while (!Input.GetKeyDown(KeyCode.F)) {
			txt.text = "Please select the appetizer you want to prepare: \n1: " + app1.name + "\n2: " + app2.name + "\n3: " + app3.name +
				"\n4: " + app4.name + "\nYour current choice is dish number " + CurrSel;
			if (Input.GetKeyDown(KeyCode.G))
				CurrSel++;
			if (CurrSel == 5)
				CurrSel = 1;
			yield return null;
			}
	}

	IEnumerator selectFir () {													//stop script until a valid input is given
		while (!Input.GetKeyDown(KeyCode.F)) {
			txt.text = "Please select the first course you want to prepare: \n1: " + fc1.name + "\n2: " + fc2.name + "\n3: " + fc3.name + "\n4: " + fc4.name + 
				"\n5: " + fc5.name + "\nYour current choice is dish number " + CurrSel;
			if (Input.GetKeyDown(KeyCode.G))
				CurrSel++;
			if (CurrSel == 6)
				CurrSel = 1;
			yield return null;
		}
	}

	IEnumerator selectSec () {													//stop script until a valid input is given
		while (!Input.GetKeyDown(KeyCode.F)) {
			txt.text = "Please select the second course you want to prepare: \n1: " + sc1.name + "\n2: " + sc2.name + "\n3: " + sc3.name + "\n4: " + sc4.name + 
				"\n5: " + sc5.name + "\nYour current choice is dish number " + CurrSel;
			if (Input.GetKeyDown(KeyCode.G))
				CurrSel++;
			if (CurrSel == 6)
				CurrSel = 1;
			yield return null;
		}
	}

	IEnumerator selectDes () {													//stop script until a valid input is given
		while (!Input.GetKeyDown(KeyCode.F)) {
			txt.text = "Please select the dessert you want to prepare: \n1: " + de1.name + "\n2: " + de2.name + "\n3: " + de3.name + 
				"\n4: " + de4.name + "\nYour current choice is dish number " + CurrSel;
			if (Input.GetKeyDown(KeyCode.G))
				CurrSel++;
			if (CurrSel == 5)
				CurrSel = 1;
			yield return null;
		}
	}

	float newgain() {
		int t, s;
		s = 0;
		t = 0;
		for (int i = 0; i < 4; i++)
			t = t + gains.times [i];
		int n = UnityEngine.Random.Range (1, t + 1);
		for (int i = 0; i < 4; i++) {
			s = s + gains.times [i];
			if (s >= n) {
				gains.times [i]--;
				return gains.values [i];
			}
		}
		return 0;
	}


	void CheckEmpty() {															//check if we still have gains to apply
		bool r = true;															//if not, add 1 to every gain's needed quantity
		for (int i = 0; i < 4; i++)
			if (gains.times [i] != 0)
				r = false;
		if (r)
			for (int i = 0; i < 4; i++)
				gains.times [i] = 1;
		return;
	}

	void initialize() {

		Bread.name = "Bread";					Bread.floor = 1;
		BreadCrumbs.name = "BreadCrumbs"; 		BreadCrumbs.floor = 1;
		Rice.name = "Rice"; 					Rice.floor = 1;
		Pasta.name = "Pasta";					Pasta.floor = 1;
		Flour.name = "Flour";					Flour.floor = 1;

		Ham.name = "Ham";						Ham.floor = 2;
		ChickenBreast.name = "ChickenBreast";	ChickenBreast.floor = 2;
		ChickenWings.name = "ChickenWings";		ChickenWings.floor = 2;
		PorkFillet.name = "PorkFillet";			PorkFillet.floor = 2;
		Eggs.name = "Eggs";						Eggs.floor = 2;

		Squids.name = "Squids";					Squids.floor = 3;
		Mussels.name = "Mussels";				Mussels.floor = 3;
		Prawns.name = "Prawns";					Prawns.floor = 3;
		Cod.name = "Cod";						Cod.floor = 3;

		Butter.name = "Butter";					Butter.floor = 4;
		Mozzarella.name = "Mozzarella";			Mozzarella.floor = 4;
		Feta.name = "Feta";						Feta.floor = 4;
		Scamorza.name = "Scamorza";				Scamorza.floor = 4;
		Parmesan.name = "Parmesan";				Parmesan.floor = 4;

		Paprika.name = "Paprika";				Paprika.floor = 5;
		Salt.name = "Salt";						Salt.floor = 5;
		ChiliPepper.name = "ChiliPepper";		ChiliPepper.floor = 5;
		Ginger.name = "Ginger";					Ginger.floor = 5;
		Pepper.name = "Pepper";					Pepper.floor = 5;

		Onions.name = "Onions";					Onions.floor = 6;
		Garlic.name = "Garlic";					Garlic.floor = 6;
		Tomatoes.name = "Tomatoes";				Tomatoes.floor = 6;
		Eggplants.name = "Eggplants";			Eggplants.floor = 6;
		Zucchini.name = "Zucchini";				Zucchini.floor = 6;

		Melon.name = "Melon";					Melon.floor = 7;
		Peaches.name = "Peaches";				Peaches.floor = 7;
		Hazelnuts.name = "Hazelnuts";			Hazelnuts.floor = 7;
		Apples.name = "Apples";					Apples.floor = 7;
		Oranges.name = "Oranges";				Oranges.floor = 7;

		OliveOil.name = "OliveOil";				OliveOil.floor = 8;
		Vinegar.name = "Vinegar";				Vinegar.floor = 8;
		Truffles.name = "Truffles";				Truffles.floor = 8;
		Basil.name = "Basil";					Basil.floor = 8;
		Mint.name = "Mint";						Mint.floor = 8;

		Honey.name = "Honey";					Honey.floor = 9;
		Chocolate.name = "Chocolate";			Chocolate.floor = 9;
		Sugar.name = "Sugar";					Sugar.floor = 9;
		Yogurt.name = "Yogurt";					Yogurt.floor = 9;

		end.name = "end";						end.floor = 0;


		app1.name = "Squid Rings";				
		app1.ingredients = new ing[] { BreadCrumbs, Squids, Paprika, Salt, OliveOil, end};
		app2.name = "Rice Arancini";			
		app2.ingredients = new ing[] {BreadCrumbs, Rice, Ham, Butter, Scamorza, Mozzarella, Salt, OliveOil, end};
		app3.name = "Aromatic Chicken Sticks";
		app3.ingredients = new ing[] {Bread, ChickenBreast, Eggs, ChiliPepper, Ginger, Onions, Mint, end};
		app4.name = "Grilled Eggplants Boats";
		app4.ingredients = new ing[] {Bread, Eggplants, Garlic, Salt, Pepper, Mint, OliveOil, Vinegar, end};

		fc1.name = "Truffle Pasta";
		fc1.ingredients = new ing[] {Pasta, Butter, Salt, Garlic, OliveOil, Truffles, end};
		fc2.name = "Beans and Squids Pasta";
		fc2.ingredients = new ing[] {Pasta, Squids, ChiliPepper, Salt, Tomatoes, Garlic, OliveOil, end};
		fc3.name = "Peach and Pasta Salad";
		fc3.ingredients = new ing[] {Pasta, Feta, Salt, Pepper, Tomatoes, Peaches, Basil, end};
		fc4.name = "Ham and Melon Rice Salad";
		fc4.ingredients = new ing[] {Rice, Ham, Salt, Pepper, Melon, OliveOil, end};
		fc5.name = "Paella de mariscos";
		fc5.ingredients = new ing[] {Rice, Mussels, Squids, Prawns, Paprika, ChiliPepper, Onions, Tomatoes, OliveOil, end};

		sc1.name = "Fried Chicken Wings";
		sc1.ingredients = new ing[] {Flour, ChickenWings, Eggs, Paprika, Salt, Pepper, OliveOil, end};
		sc2.name = "Cordon Bleu";
		sc2.ingredients = new ing[] {BreadCrumbs, ChickenBreast, Ham, Eggs, Scamorza, OliveOil, end};
		sc3.name = "Honey and Ginger Pork Fillet";
		sc3.ingredients = new ing[] {PorkFillet, Ginger, ChiliPepper, Salt, OliveOil, Honey, end};
		sc4.name = "Stuffed Zucchini";
		sc4.ingredients = new ing[] {Bread, Parmesan, Salt, Pepper, Zucchini, Onions, Tomatoes, OliveOil, end};
		sc5.name = "Fish Sticks";
		sc5.ingredients = new ing[] {Flour, BreadCrumbs, Eggs, Cod, Salt, OliveOil, end};

		de1.name = "Brownies";
		de1.ingredients = new ing[] {Flour, Eggs, Butter, Hazelnuts, Sugar, end};
		de2.name = "Apple Muffins";
		de2.ingredients = new ing[] {Flour, Eggs, Apples, Yogurt, Sugar, end};
		de3.name = "Orange Cake";
		de3.ingredients = new ing[] {Flour, Eggs, Butter, Oranges, Sugar, end};
		de4.name = "Chocolate Cake";
		de4.ingredients = new ing[] {Flour, Eggs, Butter, Salt, Chocolate, Sugar, end};

	}
}


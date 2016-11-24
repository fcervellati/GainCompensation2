using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextEditor : MonoBehaviour {

	private Text txt;
	private bool holding;
	private string RecName, obj, tar;
	private string[] CurRec;
	private int points, i;
	private bool play;

	public struct Recipe {																//how a recipe is defined
		public string[] ingredients;
		public string name;
		}
	public Recipe rec1, rec2, rec3, rec4, rec5;
	private bool[] diary = new bool[] {false, false, false, false, false};				//to remember which recipes have been completed
	//dimension of "diary" AND recipes to win (below) must be changed accordingly to the number of recipes

	void Start () {
		play = true;
		txt = gameObject.GetComponent<Text> ();											//initialize txt to the Text object in game

		points = 0;
		obj = "nothing";																//at the beginning, nothing is being held
		holding = false;

		rec1.ingredients = new string[] { "sweets", "vegetables", "done" };				//define name and ingredients for each recipe
		rec1.name = "Recipe1";
		rec2.ingredients = new string[] { "fruit", "bread", "drinks", "done" };
		rec2.name = "Recipe2";
		rec3.ingredients = new string[] { "vegetables", "done"};
		rec3.name = "Recipe3";
		rec4.ingredients = new string[] { "drinks", "sweets", "done"};
		rec4.name = "Recipe4";
		rec5.ingredients = new string[] { "bread", "fruit", "vegetables", "done" };
		rec5.name = "Recipe5";

		CurRec = newtar();																//select first recipe and target
		i = 0;
		tar = CurRec[i];
	}

	void Update () {

		if (play && Input.GetKeyDown ("backspace")) {									//drop object if backspace is pressed
			holding = false;
			obj = "nothing";
		}

		if (play && Input.GetKeyDown("space") && !holding)								//if "space" is hit, check if an object is grabbed
			obj = Zone ();
		
		if (play && Input.GetKeyDown ("r") && inCauldron())  {							//if "r" is hit, check if an object is released
			
			if (obj == tar) {															//check if held object is correct
				i++;
				
				if (CurRec[i] != "done")												//continue recipe if still incomplete
					tar = CurRec [i];

				if (CurRec[i] == "done") {																		
					points++;
					if (points < 5) {													//start new recipe if possible
						i = 0;
						CurRec = newtar ();
						tar = CurRec [i];
					}

					if (points == 5)													//terminate game if no more recipes 
						play = false;

				}
			}

			holding = false;															//always release held object
			obj = "nothing";
		}

		if (play)
			txt.text = "You are cooking " + RecName + ".\nPlease fetch me some " + tar + ".\nYou are currently holding " + obj + ".\nRecipes completed: " + points;
		if (!play)
			txt.text = "You won!";
	}



	string Zone () {																	//identify current zone
		string Result = "nothing";														//and grabbed object
		Vector3 pos = transform.root.position;										
		float x = pos.x;
		float z = pos.z;
		if (x > -1.0 && x < 1.0 && z > 8.0 && z < 10.0)			
			Result = "vegetables";
		if (x > -5.5 && x < -3.5 && z > 8.0 && z < 10.0)
			Result = "drinks";
		if (x > 3.5 && x < 5.5 && z > 8.0 && z < 10.0)
			Result = "fruit";
		if (x > 8.0 && x < 10.0 && z > 5.5 && z < 7.5)
			Result = "bread";
		if (x > -10.0 && x < 8.0 && z > 5.5 && z < 7.5)
			Result = "sweets";
		if (Result != "nothing")
			holding = true;
		return Result;
	}

	bool inCauldron () {																//check whether you are in the cauldron zone
		Vector3 pos = transform.root.position;		
		float x = pos.x;
		float z = pos.z;
		if (x > -1.0 && x < 1.0 && z > -10 && z < -8.0)
			return true;
		return false;
	}

	string[] newtar() {																	//choose a new not completed recipe randomly
		int n = Random.Range (0, 5);
		if (diary [n])
			return newtar ();
		
		diary [n] = true;
		if (n == 0) {
			RecName = rec1.name;
			return rec1.ingredients;
		}
		if (n == 1) {
			RecName = rec2.name;
			return rec2.ingredients;
		}
		if (n == 2) {
			RecName = rec3.name;
			return rec3.ingredients;
		}
		if (n == 3) {
			RecName = rec4.name;
			return rec4.ingredients;
		}
		RecName = rec5.name;
		return rec5.ingredients;
		}
		
}

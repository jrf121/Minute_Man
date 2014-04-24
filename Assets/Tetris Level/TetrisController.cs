using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetrisController : MonoBehaviour {
	public bool running;
	public float timeBetweenUpdates;
	public Vector2 bottomRightCorner;
	public List<GameObject> pieces;
	
	private float lastUpdate;
	private GameObject[,] playField;
	private GameObject pivot;
	private List<GameObject> activePieces;
	
	private int[,] ILayout = {{	1, 1, 2, 1},
							{	0, 0, 0, 0}};
								
	private int[,] JLayout = {{	1, 2, 1, 0},
							{	0, 0, 1, 0}};
								
	private int[,] LLayout = {{	1, 2, 1, 0},
							{	1, 0, 0, 0}};
								
	private int[,] OLayout = {{	0, 1, 1, 0},
							{	0, 1, 1, 0}};
								
	private int[,] SLayout = {{	0, 2, 1, 0},
							{	1, 1, 0, 0}};
								
	private int[,] TLayout = {{	1, 2, 1, 0},
							{	0, 1, 0, 0}};
								
	private int[,] ZLayout = {{	1, 1, 0, 0},
							{	0, 2, 1, 0}};
								
	private List<int[,]> Layouts;
	
	// Use this for initialization
	void Start () {
		Layouts = new List<int[,]>();
		Layouts.AddRange(new int[][,]{ILayout, JLayout, LLayout, OLayout, SLayout, TLayout, ZLayout});
		playField = new GameObject[10, 22];
		activePieces = new List<GameObject>();
		DropNext();
		TimedUpdate();
	}
	
	// Update is called once per frame
	void Update () {
		if (!running)
			return;
		
		if (Input.GetAxis("Vertical") == -1f)
			TimedUpdate();
		if (Input.GetButtonDown("Tetris Shift")){
			if (Input.GetAxis("Tetris Shift") == -1){
				this.Rotate(false);
			}
			else if (Input.GetAxis("Tetris Shift") == 1){
				this.Rotate(true);
			}
		}
			
		if (Input.GetButtonDown("Horizontal")){
			bool moveable = true;
			for (int j = 0; j < playField.GetLength(1); j++){
				for(int i = 0; i < playField.GetLength(0); i++){
					int x = i + (int) Mathf.Sign(Input.GetAxis("Horizontal"));
					if (activePieces.Contains(playField[i, j]) && (x < 0 || x >= playField.GetLength(0) || 
							playField[x, j] != null && !activePieces.Contains(playField[x, j]))){
						moveable = false;
						break;
					}
				}
			}
			if (moveable){
				for (int j = 0; j < playField.GetLength(1); j++){
					if (Mathf.Sign(Input.GetAxis("Horizontal")) == -1f){
						for(int i = 0; i < playField.GetLength(0); i++){
							if (activePieces.Contains(playField[i, j])){
								playField[i - 1, j] = playField[i, j];
								playField[i, j] = null;
							}
						}
					}
					else{
						for(int i = playField.GetLength(0) - 1; i >= 0; i--){
							if (activePieces.Contains(playField[i, j])){
								playField[i + 1, j] = playField[i, j];
								playField[i, j] = null;
							}
						}
					}
					for(int i = 0; i < playField.GetLength(0); i++){
						if(playField[i, j] != null){
							playField[i, j].transform.position = bottomRightCorner + 
								new Vector2((i + 0.5f) * playField[i, j].GetComponent<BoxCollider2D>().size.x, 
								            (j + 0.5f) * playField[i, j].GetComponent<BoxCollider2D>().size.y);
						}
					}
				}
			}
		}		
			
		
		if (Time.time - lastUpdate >= timeBetweenUpdates){
			TimedUpdate();
		}
	}
	
	public void Rotate(bool clockwise){
		GameObject[,] toRotate = new GameObject[5, 5];
		
		//Locates the pivot point and sets the bounds of the rotation array
		int lowRow = 0;
		int highRow = 0;
		int lowCol = 0;
		int highCol = 0;
		Vector2 pivotLocation = new Vector2(0,0);
		for (int j = 0; j < playField.GetLength(1); j++){
			for(int i = 0; i < playField.GetLength(0); i++){
				if (playField[i, j] != null && playField[i, j].Equals(pivot)){
					lowRow = -2;
					highRow = 2;
					lowCol = -2;
					highCol = +2;
					
					pivotLocation = new Vector2(i, j);
					//Forces the rotation array to only
					while (lowRow + i < 0)
						lowRow++;
					while (lowCol + j < 0)
						lowCol++;
					while (highRow + i >= playField.GetLength(0))
						highRow--;
					while (highCol + j >= playField.GetLength(1))
						highCol--;
					break;
				}
			}
		}
		if (pivot == null)
			return;
			
		
		for(int j = 2 + lowCol; j <= 2 + highCol; j++){
			for (int i = 2 + lowRow; i <= 2 + highRow; i++){
				toRotate[i, j] = playField[(int)pivotLocation.x + lowCol + i, (int)pivotLocation.y + lowRow + j];
				if (toRotate[i, j] == null){
					toRotate[i, j] = new GameObject();
					toRotate[i, j].name = "Remove";
				}
			}
		}
		
		GameObject[,] rotated = new GameObject[5, 5];
		if (clockwise)
			rotated = RotateArrayCockwise(toRotate);
		else 
			rotated = RotateArrayCountercockwise(toRotate);
		bool goodForRotate = false;
		for(int j = 0; j < 5; j++){
			for (int i = 0; i < 5; i++){
				if (!activePieces.Contains(rotated[i, j]) || (toRotate[i, j] != null && (toRotate[i, j].name == "Remove") || activePieces.Contains(toRotate[i, j])))
					goodForRotate = true;
			}
		}
		for(int j = 2 + lowCol; j <= 2 + highCol; j++){
			for (int i = 2 + lowRow; i <= 2 + highRow; i++){
				if (goodForRotate){
					Debug.Log("Rotate Good");
					if (activePieces.Contains(playField[(int)pivotLocation.x + lowCol + i, (int)pivotLocation.y + lowRow + j])){
						playField[(int)pivotLocation.x + lowCol + i, (int)pivotLocation.x + lowRow + j] = null;
					}
					if (activePieces.Contains(rotated[i, j])){
						playField[(int)pivotLocation.x + i + lowRow,(int)pivotLocation.y + j + lowCol] = rotated[i, j];
						playField[(int)pivotLocation.x + i + lowRow,(int)pivotLocation.y + j + lowCol].transform.position = bottomRightCorner + 
							new Vector2((pivotLocation.x + i + lowRow + 0.5f) * playField[(int)pivotLocation.x + i + lowRow, (int)pivotLocation.y + j + lowCol].GetComponent<BoxCollider2D>().size.x, 
							            (pivotLocation.y + j + lowCol + 0.5f) * playField[(int)pivotLocation.x + i + lowRow, (int)pivotLocation.y + j + lowCol].GetComponent<BoxCollider2D>().size.y);
					}
				}
			}
		}
		for(int j = 0; j < 5; j++){
			for (int i = 0; i < 5; i++){
				if(rotated[i, j] != null && rotated[i, j].name == "Remove")
					Destroy(rotated[i, j]);
			}
		}
	}
	
	public GameObject[,] RotateArrayCockwise(GameObject[,] cock){
		GameObject[,] toReturn = new GameObject[cock.GetLength(0), cock.GetLength(0)];
		for (int i = 0; i < cock.GetLength(0); i++){
			for(int j = 0; j < cock.GetLength(0); j++){
				toReturn[cock.GetLength(0) - 1 - j, i] = cock[i, j];
			}
		}
		return toReturn;
	}
	
	public GameObject[,] RotateArrayCountercockwise(GameObject[,] cock){
		GameObject[,] toReturn = new GameObject[cock.GetLength(0), cock.GetLength(0)];
		for (int i = 0; i < cock.GetLength(0); i++){
			for(int j = 0; j < cock.GetLength(0); j++){
				toReturn[j, cock.GetLength(0) - 1 - i] = cock[i, j];
			}
		}
		return toReturn;
	}
	
	
	
	public void Lose(){
		Application.LoadLevel("Tetris Level");
	}
	
	public void DropNext(){
	int pieceType = Random.Range(0, 7);
		for (int i = 0; i < 2; i++){
			for (int j = 0; j < 4; j++){
				if (Layouts[pieceType][i, j] > 0){
					if (playField[j + 3, 21 - i] != null){
						Lose ();
						return;
					}
					playField[j + 3, 21 - i] = (GameObject)GameObject.Instantiate(pieces[pieceType]);
					activePieces.Add(playField[j + 3, 21 - i]);
					if (Layouts[pieceType][i, j] == 2)
						pivot = playField[j + 3, 21 - i];
				}
			}
		}
	}
	
	public void TimedUpdate(){
		bool goodForDrop = true;
		for (int j = 0; j < playField.GetLength(1); j++){
			for(int i = 0; i < playField.GetLength(0); i++){
				if(playField[i, j] != null){
					playField[i, j].transform.position = bottomRightCorner + 
						new Vector2((i + 0.5f) * playField[i, j].GetComponent<BoxCollider2D>().size.x, 
						            (j + 0.5f) * playField[i, j].GetComponent<BoxCollider2D>().size.y);
				}
				if (activePieces.Contains(playField[i, j]) && (j == 0 || (playField[i, j - 1] != null && !activePieces.Contains(playField[i, j - 1])))){
					activePieces.Clear();
					pivot = null;
					DropNext();
				}	
			}
		}
		if (goodForDrop){
			for (int j = 0; j < playField.GetLength(1); j++){
				for(int i = 0; i < playField.GetLength(0); i++){
					if (activePieces.Contains(playField[i, j])){
						playField[i, j - 1] = playField[i, j];
						playField[i, j] = null;
					}
				}
			}
		}
		lastUpdate = Time.time;
	}
}

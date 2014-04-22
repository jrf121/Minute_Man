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
	private List<GameObject> activePieces;
	
	private bool[,] ILayout = {{	true, 	true, 	true, 	true},
								{	false,	false, 	false, 	false}};
								
	private bool[,] JLayout = {{	true, 	true, 	true, 	false},
								{	false,	false, 	true, 	false}};
								
	private bool[,] LLayout = {{	false, 	true, 	true, 	true},
								{	false,	true, 	false, 	false}};
								
	private bool[,] OLayout = {{	false, 	true, 	true, 	false},
								{	false,	true, 	true, 	false}};
								
	private bool[,] SLayout = {{	false, 	true, 	true, 	false},
								{	true,	true, 	false, 	false}};
								
	private bool[,] TLayout = {{	true, 	true, 	true, 	false},
								{	false,	true, 	false, 	false}};
								
	private bool[,] ZLayout = {{	false, 	true, 	true, 	false},
								{	false,	false, 	true, 	true}};
								
	private List<bool[,]> Layouts;
	
	// Use this for initialization
	void Start () {
		Layouts = new List<bool[,]>();
		Layouts.AddRange(new bool[][,]{ILayout, JLayout, LLayout, OLayout, SLayout, TLayout, ZLayout});
		playField = new GameObject[10, 22];
		activePieces = new List<GameObject>();
		DropNext();
		TimedUpdate();
	}
	
	// Update is called once per frame
	void Update () {
		if (!running)
			return;
		if (Input.GetButtonDown("Horizontal")){
			bool moveable = true;
			for (int j = 0; j < playField.GetLength(1); j++){
				for(int i = 0; i < playField.GetLength(0); i++){
					int x = i + (int) Mathf.Sign(Input.GetAxis("Horizontal"));
					if (activePieces.Contains(playField[i, j]) && (x < 0 || x >= playField.GetLength(0))){
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
				}
			}
		}		
			
		
		if (Time.time - lastUpdate >= timeBetweenUpdates){
			TimedUpdate();
		}
	}
	
	public void Lose(){
		Application.LoadLevel("Tetris Level");
	}
	
	public void DropNext(){
	int pieceType = Random.Range(0, 7);
		for (int i = 0; i < 2; i++){
			for (int j = 0; j < 4; j++){
				if (Layouts[pieceType][i, j]){
					if (playField[j + 3, 21 - i] != null){
						Lose ();
						return;
					}
					playField[j + 3, 21 - i] = (GameObject)GameObject.Instantiate(pieces[pieceType]);
					activePieces.Add(playField[j + 3, 21 - i]);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

	private string type;
	private bool atRiver;

	public Piece(string t){
		type = t;
	}

	public Piece(){
		type = "";
	}	

	public void SetType(string t){
		type = t;
	}

	public string GetType(){
		return type;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	public Piece[,] pieces = new Piece[10,9];
	//Red gameobjects
	public GameObject chariotRed;
	public GameObject horseRed;
	public GameObject elephantRed;
	public GameObject advisorRed;
	public GameObject generalRed;
	public GameObject cannonRed;
	public GameObject soldierRed;
	
	//Blue gameobjects
	public GameObject chariotBlue;
	public GameObject horseBlue;
	public GameObject elephantBlue;
	public GameObject advisorBlue;
	public GameObject generalBlue;
	public GameObject cannonBlue;
	public GameObject soldierBlue;


	private void Start()
	{
		GenerateBoard();
	}

	private void GenerateBoard()
	{
		//Generate Red Team
		GenerateChariot(0, 0, 46.14f, 46.04f, 4.3f, true);
		GenerateChariot(0, 8, -46.14f, 46.04f, 4.3f, true);
		GenerateHorse(0, 1, 33.79f, 45.82f, 6.89f, true);
		GenerateHorse(0, 7, -37.37f, 45.82f, 6.89f, true);
		GenerateElephant(0, 2, 23.78f, 44.61f, 4.3f, true);
		GenerateElephant(0, 6, -23.78f, 44.61f, 4.3f, true);
		GenerateAdvisor(0, 3, 11.9f, 46.79f, 4.3f, true);
		GenerateAdvisor(0, 5, -11.9f, 46.79f, 4.3f, true);
		GenerateGeneral(0, 4, 0f, 45.95f, 4.3f, true);
		GenerateCannon(2, 1, 34.8f, 26.5f, 4.3f, true);
		GenerateCannon(2, 7, -35.9f, 26.5f, 4.3f, true);
		GenerateSoldier(3, 0, 46.5f, 18.1f, 4.3f, true);
		GenerateSoldier(3, 2, 23.5f, 18.1f, 4.3f, true);
		GenerateSoldier(3, 4, 0f, 18.1f, 4.3f, true);
		GenerateSoldier(3, 0, -46.5f, 18.1f, 4.3f, true);
		GenerateSoldier(3, 2, -23.5f, 18.1f, 4.3f, true);

		//Generate Blue Team
		GenerateChariot(0, 0, 46.14f, -47.04f, 4.3f, false);
		GenerateChariot(0, 8, -46.14f, -47.04f, 4.3f, false);
		GenerateHorse(0, 1, 35.79f, -46.82f, 6.89f, false);
		GenerateHorse(0, 7, -35.37f, -46.82f, 6.89f, false);
		GenerateElephant(0, 2, 23.78f, -45.61f, 4.3f, false);
		GenerateElephant(0, 6, -23.78f, -45.61f, 4.3f, false);
		GenerateAdvisor(0, 3, 11.9f, -47.79f, 4.3f, false);
		GenerateAdvisor(0, 5, -11.9f, -47.79f, 4.3f, false);
		GenerateGeneral(0, 4, 0f, -46.95f, 4.3f, false);
		GenerateCannon(2, 1, 34.8f, -28.5f, 4.3f, false);
		GenerateCannon(2, 7, -35.9f, -28.5f, 4.3f, false);
		GenerateSoldier(3, 0, 46.5f, -19.1f, 4.3f, false);
		GenerateSoldier(3, 2, 23.5f, -19.1f, 4.3f, false);
		GenerateSoldier(3, 4, 0f, -19.1f, 4.3f, false);
		GenerateSoldier(3, 0, -46.5f, -19.1f, 4.3f, false);
		GenerateSoldier(3, 2, -23.5f, -19.1f, 4.3f, false);
	}

	private void GenerateChariot(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(chariotRed) as GameObject;
		}else{
			go = Instantiate(chariotBlue) as GameObject;
		}
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}
	private void GenerateHorse(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(horseRed) as GameObject;
		}else{
			go = Instantiate(horseBlue) as GameObject;
		}
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}
	private void GenerateElephant(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(elephantRed) as GameObject;
		}else{
			go = Instantiate(elephantBlue) as GameObject;
		}
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}
	private void GenerateAdvisor(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(advisorRed) as GameObject;
		}else{
			go = Instantiate(advisorBlue) as GameObject;
		}
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}
	private void GenerateGeneral(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(generalRed) as GameObject;
		}else{
			go = Instantiate(generalBlue) as GameObject;
		}
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}
	private void GenerateCannon(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(cannonRed) as GameObject;
		}else{
			go = Instantiate(cannonBlue) as GameObject;
		}
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}
	private void GenerateSoldier(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(soldierRed) as GameObject;
		}else{
			go = Instantiate(soldierBlue) as GameObject;
		}
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}

}

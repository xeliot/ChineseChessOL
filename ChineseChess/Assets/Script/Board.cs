using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	public Piece[,] pieces = new Piece[10,9];
	public GameObject chariotRed;
	public GameObject horseRed;
	public GameObject elephantRed;
	public GameObject advisorRed;
	public GameObject generalRed;
	public GameObject cannonRed;
	public GameObject soldierRed;

	private void Start()
	{
		GenerateBoard();
	}

	private void GenerateBoard()
	{
		//Generate Red Team
		GenerateChariot(0, 0, 46.14f, 46.04f, 4.3f, true);
		GenerateChariot(0, 8, -46.14f, 46.04f, 4.3f, true);
		Debug.Log("here1");
		GenerateHorse(0, 1, 33.79f, 45.82f, 6.89f, true);
		GenerateHorse(0, 7, -37.37f, 45.82f, 6.89f, true);
		Debug.Log("here2");
		GenerateElephant(0, 2, 23.78f, 44.61f, 4.3f, true);
		GenerateElephant(0, 6, -23.78f, 44.61f, 4.3f, true);
		GenerateAdvisor(0, 3, 11.9f, 46.79f, 4.3f, true);
		GenerateAdvisor(0, 5, -11.9f, 46.79f, 4.3f, true);
		GenerateGeneral(0, 4, 0f, 45.95f, 4.3f, true);
		GenerateCannon(2, 1, 34.8f, 26.5f, 4.3f, true);
		GenerateCannon(2, 7, -36.8f, 26.5f, 4.3f, true);
	}

	private void GenerateChariot(int x, int y, float px, float py, float pz, bool red)
	{
		/*
		if(red){
			GameObject go = Instantiate(chariotRed) as GameObject;
			Piece p = go.GetComponent<Piece>();
			pieces[x, y] = p;
		}else{
			GameObject go = Instantiate(chariotBlue) as GameObject;
			Piece p = go.GetComponent<Piece>();
			pieces[x, y] = p;
		}
		 */
		GameObject go = Instantiate(chariotRed) as GameObject;
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}
	private void GenerateHorse(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go = Instantiate(horseRed) as GameObject;
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}
	private void GenerateElephant(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go = Instantiate(elephantRed) as GameObject;
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}
	private void GenerateAdvisor(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go = Instantiate(advisorRed) as GameObject;
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}
	private void GenerateGeneral(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go = Instantiate(generalRed) as GameObject;
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}
	private void GenerateCannon(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go = Instantiate(cannonRed) as GameObject;
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}
	private void GenerateSoldier(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go = Instantiate(soldierRed) as GameObject;
		go.transform.position = new Vector3(px, py, pz);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
	}

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board : MonoBehaviour {

	public ChessPiece[,] pieces = new ChessPiece[10,9];
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

	private float mouseOverX;
	private float mouseOverY;
	private Vector3 mousePosition;
	private Vector2 boardPosition;

	private ChessPiece selectedPiece;
	private Vector2 startDrag;
	private Vector2 endDrag;

	private void Start()
	{
		GenerateBoard();
	}

	private void Update()
	{
		UpdateMouseOver();
		
		//Id it is my turn
		int x = (int) boardPosition.x;
		int y = (int) boardPosition.y;

		if(Input.GetMouseButtonDown(0)){
			SelectPiece(x, y);
		}
		
		if(Input.GetMouseButtonUp(0)){
			TryMove((int) startDrag.x, (int) startDrag.y, x, y);
		}
	}

	private void UpdateMouseOver()
	{
		if(!Camera.main) //Check if camera exists
		{
			Debug.Log("Unable to find main camera.");
			return;
		}

		mouseOverX = (Input.mousePosition.x);
        mouseOverY = (Input.mousePosition.y);
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (mouseOverX,mouseOverY,94.3f));
		boardPosition = GetBoardPosition(mousePosition.x, mousePosition.y);
	}

	private void SelectPiece(int x, int y)
	{
		Debug.Log(x+","+y);
		//Check for Out of Bounds
		if(x < 0 || x > 9 || y < 0 || y > 8){
			return;
		}

		ChessPiece p = pieces[x, y];
		//p.transform.position = p.transform.position + new Vector3(0, 0, 300);

		if(p != null){
			selectedPiece = p;
			startDrag = boardPosition;
			//Debug.Log(selectedPiece.name);
		}
	}

	private void TryMove(int startX, int startY, int endX, int endY)
	{
		startDrag = new Vector2(startX, startY);
		endDrag = new Vector2(endX, endY);
		selectedPiece = pieces[startX, startY];
		
		//Debug.Log("("+startX+","+startY+") -> ("+endX+","+endY+")");
		//Debug.Log(selectedPiece.name);

		MovePiece(selectedPiece, endX, endY);

		// Check if out of Bounds

		// Is there a selected pieces


	}

	private void MovePiece(ChessPiece piece, int x, int y){
		if(piece==null){
			return;
		}
		//Debug.Log(piece.name);
		int startX = (int) piece.GetBoardPosition().x;
		int startY = (int) piece.GetBoardPosition().y;
		//Debug.Log(startX+","+startY);
		int xDifference = x - startX;
		int yDifference = y - startY;
		piece.transform.position = piece.transform.position + new Vector3(yDifference * 12.5f, xDifference * 10.0f, 0.0f);
		piece.SetBoardPosition(x, y);
		pieces[x, y] = piece;
		pieces[startX, startY] = null;
	}

	private Vector2 GetBoardPosition(float x, float y){
		int yResult = (int) Math.Round((x+50.0)/12.5);
		int xResult;
		if(y<0){
			xResult = (int) Math.Round((y+50)/10.0);
			if(xResult==5){
				xResult = 4;
			}
		}else{
			xResult = (int) Math.Round((y+40)/10.0);
			if(xResult==4){
				xResult = 5;
			}
		}
		Vector2 boardPos = new Vector2(xResult, yResult);
		return boardPos;
	}

	private void GenerateBoard()
	{

		//Generate Blue Team
		GenerateChariot(0, 8, 46.14f, -47.04f, 4.3f, false);
		GenerateChariot(0, 0, -46.14f, -47.04f, 4.3f, false);
		GenerateHorse(0, 7, 35.79f, -46.82f, 6.89f, false);
		GenerateHorse(0, 1, -35.37f, -46.82f, 6.89f, false);
		GenerateElephant(0, 6, 23.78f, -45.61f, 4.3f, false);
		GenerateElephant(0, 2, -23.78f, -45.61f, 4.3f, false);
		GenerateAdvisor(0, 5, 11.9f, -47.79f, 4.3f, false);
		GenerateAdvisor(0, 3, -11.9f, -47.79f, 4.3f, false);
		GenerateGeneral(0, 4, 0f, -46.95f, 4.3f, false);
		GenerateCannon(2, 7, 34.8f, -28.5f, 4.3f, false);
		GenerateCannon(2, 1, -35.9f, -28.5f, 4.3f, false);
		GenerateSoldier(3, 0, -46.5f, -19.1f, 4.3f, false);
		GenerateSoldier(3, 2, -23.5f, -19.1f, 4.3f, false);
		GenerateSoldier(3, 4, 0f, -19.1f, 4.3f, false);
		GenerateSoldier(3, 6, 46.5f, -19.1f, 4.3f, false);
		GenerateSoldier(3, 8, 23.5f, -19.1f, 4.3f, false);

		//Generate Red Team
		GenerateChariot(9, 8, 46.14f, 46.04f, 4.3f, true);
		GenerateChariot(9, 0, -46.14f, 46.04f, 4.3f, true);
		GenerateHorse(9, 7, 33.79f, 45.82f, 6.89f, true);
		GenerateHorse(9, 1, -37.37f, 45.82f, 6.89f, true);
		GenerateElephant(9, 6, 23.78f, 44.61f, 4.3f, true);
		GenerateElephant(9, 2, -23.78f, 44.61f, 4.3f, true);
		GenerateAdvisor(9, 5, 11.9f, 46.79f, 4.3f, true);
		GenerateAdvisor(9, 3, -11.9f, 46.79f, 4.3f, true);
		GenerateGeneral(9, 4, 0f, 45.95f, 4.3f, true);
		GenerateCannon(7, 7, 34.8f, 26.5f, 4.3f, true);
		GenerateCannon(7, 1, -35.9f, 26.5f, 4.3f, true);
		GenerateSoldier(6, 0, -46.5f, 18.1f, 4.3f, true);
		GenerateSoldier(6, 2, -23.5f, 18.1f, 4.3f, true);
		GenerateSoldier(6, 4, 0f, 18.1f, 4.3f, true);
		GenerateSoldier(6, 6, 46.5f, 18.1f, 4.3f, true);
		GenerateSoldier(6, 8, 23.5f, 18.1f, 4.3f, true);
	}

	private void GenerateChariot(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(chariotRed) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("chariot");
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(chariotBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("chariot");
			go.GetComponent<ChessPiece>().SetRed(false);
		}
		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
		go.transform.position = new Vector3(px, py, pz);
		ChessPiece p = go.GetComponent<ChessPiece>();
		pieces[x, y] = p;
	}
	private void GenerateHorse(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(horseRed) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("horse");
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(horseBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("horse");
			go.GetComponent<ChessPiece>().SetRed(false);
		}
		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
		go.transform.position = new Vector3(px, py, pz);
		ChessPiece p = go.GetComponent<ChessPiece>();
		pieces[x, y] = p;
	}
	private void GenerateElephant(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(elephantRed) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("elephant");
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(elephantBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("elephant");
			go.GetComponent<ChessPiece>().SetRed(false);
		}
		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
		go.transform.position = new Vector3(px, py, pz);
		ChessPiece p = go.GetComponent<ChessPiece>();
		pieces[x, y] = p;
	}
	private void GenerateAdvisor(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(advisorRed) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("advisor");
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(advisorBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("advisor");
			go.GetComponent<ChessPiece>().SetRed(false);
		}
		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
		go.transform.position = new Vector3(px, py, pz);
		ChessPiece p = go.GetComponent<ChessPiece>();
		pieces[x, y] = p;
	}
	private void GenerateGeneral(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(generalRed) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("general");
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(generalBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("general");
			go.GetComponent<ChessPiece>().SetRed(false);
		}
		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
		go.transform.position = new Vector3(px, py, pz);
		ChessPiece p = go.GetComponent<ChessPiece>();
		pieces[x, y] = p;
	}
	private void GenerateCannon(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(cannonRed) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("cannon");
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(cannonBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("cannon");
			go.GetComponent<ChessPiece>().SetRed(false);
		}
		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
		go.transform.position = new Vector3(px, py, pz);
		ChessPiece p = go.GetComponent<ChessPiece>();
		pieces[x, y] = p;
	}
	private void GenerateSoldier(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(soldierRed) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("soldier");
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(soldierBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().SetType("soldier");
			go.GetComponent<ChessPiece>().SetRed(false);
		}
		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
		go.transform.position = new Vector3(px, py, pz);
		ChessPiece p = go.GetComponent<ChessPiece>();
		pieces[x, y] = p;
	}

}

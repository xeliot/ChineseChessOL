using UnityEngine;
using System;
using System.Collections;

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
	private bool dragging = false;
	private Vector3 originalPosition;

	private bool moveCompleted;
	private bool isRedTurn = true;

	private Vector2 generalRedPos;
	private Vector2 generalBluePos;
	private Vector2 piecesPos;

	private void Start()
	{
		GenerateBoard();
	}

	private void Update()
	{
		UpdateMouseOver();
		//Debug.Log(boardPosition);
		//Id it is my turn
		
		int x = (int) boardPosition.x;
		int y = (int) boardPosition.y;
		

		if(dragging && selectedPiece != null){
			DragPiece(selectedPiece);
		}
		

		if(Input.GetMouseButtonDown(0)){
			SelectPiece(x, y);
			if(selectedPiece != null && selectedPiece.GetRed()==isRedTurn){
				dragging = true;
				originalPosition = selectedPiece.transform.position;
			}
		}
		
		if(Input.GetMouseButtonUp(0)){
			if(selectedPiece != null && selectedPiece.GetRed()==isRedTurn){
				TryMove((int) startDrag.x, (int) startDrag.y, x, y);
				dragging = false;
				selectedPiece = null;
				if(moveCompleted){
					isRedTurn = ! isRedTurn;
				}
			}
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
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (mouseOverX,mouseOverY,186.75f));
		boardPosition = GetBoardPosition(mousePosition.x, mousePosition.y);
	}

	private void SelectPiece(int x, int y)
	{
		//Debug.Log(x+","+y);
		//Check for Out of Bounds
		if(x < 0 || x > 9 || y < 0 || y > 8){
			return;
		}

		ChessPiece p = pieces[x, y];
		//Debug.Log(p.name);
		//p.transform.position = mousePosition;

		if(p != null){
			selectedPiece = p;
			startDrag = boardPosition;
			//Debug.Log(selectedPiece.name);
		}
	}

	private void DragPiece(ChessPiece sP)
	{
		if(sP.GetComponent<ChessPiece>().Type=="horse"){
			sP.transform.position = Camera.main.ScreenToWorldPoint(new Vector3 (mouseOverX,mouseOverY,172.45f));
		}else{
			sP.transform.position = Camera.main.ScreenToWorldPoint(new Vector3 (mouseOverX,mouseOverY,177.75f));
		}
	}

	private void TryMove(int startX, int startY, int endX, int endY)
	{
		//Debug.Log(selectedPiece.name);
		startDrag = new Vector2(startX, startY);
		endDrag = new Vector2(endX, endY);
		selectedPiece = pieces[startX, startY];
		
		//Debug.Log("("+startX+","+startY+") -> ("+endX+","+endY+")");
		//Debug.Log(selectedPiece.name);
		selectedPiece.transform.position = originalPosition;
		if(endX >= 0 && endX < 10 && endY >= 0 && endY < 9 && selectedPiece != null){
			//if(!GeneralChecked(isRedTurn)){
				if(isValidMove(startX, startY, endX, endY, selectedPiece.Type)){
					MovePiece(selectedPiece, endX, endY);
					moveCompleted = true;
					return;
				}
			//}
		}
		moveCompleted = false;

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
		piece.transform.position = piece.transform.position + new Vector3(yDifference * 20f, xDifference * 20f, 0.0f);
		if(pieces[x, y]!=null){
			pieces[x, y].SetBoardPosition(-1, -1);
			pieces[x, y].transform.position = pieces[x, y].transform.position + new Vector3(0f, 0f, 300f);
		}
		pieces[startX, startY] = null;
		piece.SetBoardPosition(x, y);
		pieces[x, y] = piece;
	}

	private Vector2 GetBoardPosition(float x, float y){
		int yResult = (int) Math.Round((x+80.0)/20.0);
		int xResult = (int) Math.Round((y+90.0)/20.0);
		Vector2 boardPos = new Vector2(xResult, yResult);
		return boardPos;
	}

	private void GenerateBoard()
	{

		//Generate Blue Team
		GenerateChariot(0, 0, -80f, -90f, 9.0f, false);
		GenerateChariot(0, 8, 80f, -90f, 9.0f, false);

		GenerateHorse(0, 1, -56.7f, -90f, 14.3f, false);
		GenerateHorse(0, 7, 61.56f, -90f, 14.3f, false);

		GenerateElephant(0, 2, -40f, -90f, 9.0f, false);
		GenerateElephant(0, 6, 40f, -90f, 9.0f, false);

		GenerateAdvisor(0, 3, -20f, -90f, 9.0f, false);
		GenerateAdvisor(0, 5, 20f, -90f, 9.0f, false);

		GenerateGeneral(0, 4, 0f, -90f, 9.0f, false);

		GenerateCannon(2, 1, -58.96f, -50f, 9.0f, false);
		GenerateCannon(2, 7, 59.14f, -50f, 9.0f, false);

		GenerateSoldier(3, 0, -80f, -30f, 9.0f, false);
		GenerateSoldier(3, 2, -40f, -30f, 9.0f, false);
		GenerateSoldier(3, 4, 0f, -30f, 9.0f, false);
		GenerateSoldier(3, 6, 40f, -30f, 9.0f, false);
		GenerateSoldier(3, 8, 80f, -30f, 9.0f, false);

		//Generate Red Team
		GenerateChariot(9, 0, -80f, 90f, 9.0f, true);
		GenerateChariot(9, 8, 80f, 90f, 9.0f, true);

		GenerateHorse(9, 1, -61.56f, 90f, 14.3f, true);
		GenerateHorse(9, 7, 56.7f, 90f, 14.3f, true);

		GenerateElephant(9, 2, -40f, 90f, 9.0f, true);
		GenerateElephant(9, 6, 40f, 90f, 9.0f, true);

		GenerateAdvisor(9, 3, -20f, 90f, 9.0f, true);
		GenerateAdvisor(9, 5, 20f, 90f, 9.0f, true);

		GenerateGeneral(9, 4, 0f, 90f, 9.0f, true);

		GenerateCannon(7, 1, -59.14f, 50f, 9.0f, true);
		GenerateCannon(7, 7, 58.96f, 50f, 9.0f, true);

		GenerateSoldier(6, 0, -80f, 30f, 9.0f, true);
		GenerateSoldier(6, 2, -40f, 30f, 9.0f, true);
		GenerateSoldier(6, 4, 0f, 30f, 9.0f, true);
		GenerateSoldier(6, 6, 40f, 30f, 9.0f, true);
		GenerateSoldier(6, 8, 80f, 30f, 9.0f, true);
	}

	private bool GeneralChecked(bool isRed){
		int generalX = -1;
		int generalY = -1;
		for(int i=0; i<10; i++){
			for(int j=0; j<9; j++){
				if(pieces[i, j]!=null){
					Debug.Log(pieces[i, j].Type);
					if(pieces[i, j].Type=="general" && pieces[i, j].GetRed()==isRed){
						generalX = i;
						generalY = j;
					}
				}
			}
		}
		//Debug.Log(generalX+", "+generalY);
		for(int i=0; i<10; i++){
			for(int j=0; j<9; j++){
				if(pieces[i, j]!=null){
					ChessPiece p = pieces[i, j];
					if(p.GetRed()!=isRed && isValidMove(i, j, generalX, generalY, p.Type)){
						return true;
					}
				}
			}
		}
		return false;
	}

	private bool isValidMove(int startX, int startY, int endX, int endY, string type){
		//Debug.Log("("+startX+", "+startY+") --> ("+endX+", "+endY+")");
		//cannot eat the same color
		if(pieces[endX, endY]!=null){
			if(pieces[startX, startY].GetRed() ==pieces[endX, endY].GetRed()){
				return false;
			}
		}
		ArrayList possibleMoves = new ArrayList();
		bool isRed = pieces[startX, startY].GetRed();
		if(type=="chariot"){
			if(startX!=endX && startY!=endY)return false;
			if(startX==endX && startY==endY)return false;
			int start;
			int end;
			if(startY!=endY){
				if(endY>startY){
					start = startY;
					end = endY;
				}else{
					start = endY;
					end = startY;
				}
				for(int i=start+1; i<end; i++){
					if(pieces[startX, i] != null)return false;
				}
				return true;
			}else if(startX!=endX){
				if(endX>startX){
					start = startX;
					end = endX;
				}else{
					start = endX;
					end = startX;
				}
				for(int i=start+1; i<end; i++){
					if(pieces[i, startY] != null)return false;
				}
				return true;
			}			
		}else if(type=="horse"){
			if(isInBounds(startX+1, startY)){
				if(pieces[startX+1, startY]==null){
					possibleMoves.Add(new Vector2(startX+2, startY-1));
					possibleMoves.Add(new Vector2(startX+2, startY+1));
				}
			}
			if(isInBounds(startX-1, startY)){
				if(pieces[startX-1, startY]==null){
					possibleMoves.Add(new Vector2(startX-2, startY-1));
					possibleMoves.Add(new Vector2(startX-2, startY+1));
				}
			}
			if(isInBounds(startX, startY-1)){
				if(pieces[startX, startY-1]==null){
					possibleMoves.Add(new Vector2(startX+1, startY-2));
					possibleMoves.Add(new Vector2(startX-1, startY-2));
				}
			}
			if(isInBounds(startX, startY+1)){
				if(pieces[startX, startY+1]==null){
					possibleMoves.Add(new Vector2(startX+1, startY+2));
					possibleMoves.Add(new Vector2(startX-1, startY+2));
				}
			}
		}else if(type=="elephant"){
			if(isInBounds(startX+1, startY+1)){
				if(pieces[startX+1, startY+1] == null){
					if(!isRed){
						if(startX+2 <= 4){
							possibleMoves.Add(new Vector2(startX+2, startY+2));
						}
					}else{
						possibleMoves.Add(new Vector2(startX+2, startY+2));
					}
				}
			}
			if(isInBounds(startX+1, startY-1)){
				if(pieces[startX+1, startY-1] == null){
					if(!isRed){
						if(startX+2 <= 4){
							possibleMoves.Add(new Vector2(startX+2, startY-2));
						}
					}else{
						possibleMoves.Add(new Vector2(startX+2, startY-2));
					}
				}
			}
			if(isInBounds(startX-1, startY+1)){
				if(pieces[startX-1, startY+1]==null){
					if(isRed){
						if(startX-2 >= 5){
							possibleMoves.Add(new Vector2(startX-2, startY+2));
						}
					}else{
						possibleMoves.Add(new Vector2(startX-2, startY+2));
					}
				}
			}
			if(isInBounds(startX-1, startY-1)){
				if(pieces[startX-1, startY-1]==null){
					if(isRed){
						if(startX-2 >= 5){
							possibleMoves.Add(new Vector2(startX-2, startY-2));
						}
					}else{
						possibleMoves.Add(new Vector2(startX-2, startY-2));
					}
				}
			}
		}else if(type=="advisor"){
			ArrayList advisorBox = new ArrayList();
			if(isRed){
				advisorBox.Add(new Vector2(7, 3));
				advisorBox.Add(new Vector2(7, 5));
				advisorBox.Add(new Vector2(8, 4));
				advisorBox.Add(new Vector2(9, 3));
				advisorBox.Add(new Vector2(9, 5));
			}else{
				advisorBox.Add(new Vector2(0, 3));
				advisorBox.Add(new Vector2(0, 5));
				advisorBox.Add(new Vector2(1, 4));
				advisorBox.Add(new Vector2(2, 3));
				advisorBox.Add(new Vector2(2, 5));
			}
			possibleMoves.Add(new Vector2(startX+1, startY+1));
			possibleMoves.Add(new Vector2(startX+1, startY-1));
			possibleMoves.Add(new Vector2(startX-1, startY+1));
			possibleMoves.Add(new Vector2(startX-1, startY-1));
			foreach (Vector2 pos in possibleMoves){
				if(pos.x == endX && pos.y == endY && advisorBox.Contains(pos)){
					if(isInBounds(endX, endY)){
						return true;
					}
				}
			}
			return false;
		}else if(type=="general"){
			ArrayList generalBox = new ArrayList();
			if(isRed){
				generalBox.Add(new Vector2(7, 4));
				generalBox.Add(new Vector2(8, 3));
				generalBox.Add(new Vector2(8, 4));
				generalBox.Add(new Vector2(8, 5));
				generalBox.Add(new Vector2(9, 4));
			}else{
				generalBox.Add(new Vector2(2, 4));
				generalBox.Add(new Vector2(1, 3));
				generalBox.Add(new Vector2(1, 4));
				generalBox.Add(new Vector2(1, 5));
				generalBox.Add(new Vector2(0, 4));
			}
			possibleMoves.Add(new Vector2(startX+1, startY));
			possibleMoves.Add(new Vector2(startX-1, startY));
			possibleMoves.Add(new Vector2(startX, startY+1));
			possibleMoves.Add(new Vector2(startX, startY-1));
			foreach (Vector2 pos in possibleMoves){
				if(pos.x == endX && pos.y == endY && generalBox.Contains(pos)){
					if(isInBounds(endX, endY)){
						return true;
					}
				}
			}
			return false;
		}else if(type=="cannon"){
			if(startX!=endX && startY!=endY){return false;} //cannot move diagonally
			if(startX==endX && startY==endY){return false;} //cannot move to the same current place.
			int start;
			int end;
			if(pieces[endX, endY]!=null){
				if(pieces[endX, endY].GetRed() == pieces[startX, startY].GetRed()){return false;}
				else{
					int countBetween = 0;
					if(startX!=endX){
						if(startX<endX){start=startX; end=endX;}else{start=endX; end=startX;}
						for(int i=start+1; i<end; i++){
							if(pieces[i, startY] != null)countBetween++;
						}
					}else if(startY!=endY){
						if(startY<endY){start=startY; end=endY;}else{start=endY; end=startY;}
						for(int i=start+1; i<end; i++){
							if(pieces[startX, i] != null)countBetween++;
						}
					}
					Debug.Log(countBetween);
					return countBetween==1;
				}
			}else{
				if(startX!=endX){
					if(startX<endX){start=startX; end=endX;}else{start=endX; end=startX;}
					for(int i=start+1; i<end; i++){
						if(pieces[i, startY] != null)return false;
					}
				}else if(startY!=endY){
					if(startY<endY){start=startY; end=endY;}else{start=endY; end=startY;}
					for(int i=start+1; i<end; i++){
						if(pieces[startX, i] != null)return false;
					}
				}
				return true;
			}
		}else if(type=="soldier"){
			bool crossedRiver = false;
			if(isRed){
				possibleMoves.Add(new Vector2(startX-1, startY));
				if(startX<=4){crossedRiver=true;}
			}else{
				possibleMoves.Add(new Vector2(startX+1, startY));
				if(startX>=5){crossedRiver=true;}
			}
			if(crossedRiver){
				possibleMoves.Add(new Vector2(startX, startY-1));
				possibleMoves.Add(new Vector2(startX, startY+1));
			}
		}
		foreach (Vector2 pos in possibleMoves){
			if(pos.x == endX && pos.y == endY){
				if(isInBounds(endX, endY)){
					return true;
				}
			}
		}
		return false;
	}

	private bool isInBounds(int x, int y){
		if(x >= 0 && x < 10 && y >= 0 && y < 9){
			return true;
		}else{
			return false;
		}
	}
	
	private void GenerateChariot(int x, int y, float px, float py, float pz, bool red)
	{
		GameObject go;
		if(red){
			go = Instantiate(chariotRed) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().Type = "chariot";
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(chariotBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().Type = "chariot";
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
			go.GetComponent<ChessPiece>().Type = "horse";
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(horseBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().Type = "horse";
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
			go.GetComponent<ChessPiece>().Type = "elephant";
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(elephantBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().Type = "elephant";
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
			go.GetComponent<ChessPiece>().Type = "advisor";
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(advisorBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().Type = "advisor";
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
			go.GetComponent<ChessPiece>().Type = "general";
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(generalBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().Type = "general";
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
			go.GetComponent<ChessPiece>().Type = "cannon";
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(cannonBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().Type = "cannon";
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
			go.GetComponent<ChessPiece>().Type = "soldier";
			go.GetComponent<ChessPiece>().SetRed(true);
		}else{
			go = Instantiate(soldierBlue) as GameObject;
			go.AddComponent<ChessPiece>();
			go.GetComponent<ChessPiece>().Type = "soldier";
			go.GetComponent<ChessPiece>().SetRed(false);
		}
		go.GetComponent<ChessPiece>().SetBoardPosition(x, y);
		go.transform.position = new Vector3(px, py, pz);
		ChessPiece p = go.GetComponent<ChessPiece>();
		pieces[x, y] = p;
	}

}

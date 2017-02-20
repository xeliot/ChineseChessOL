using UnityEngine;

public class ChessPiece : MonoBehaviour {

	private string type;
	private bool red;
	private bool atRiver;
	private Vector2 boardPosition;

	public ChessPiece(string t, bool r, int x, int y){
		type = t;
		red = r;
		boardPosition = new Vector2(x, y);
	}

	public ChessPiece(){
		type = "";
		red = true;
		boardPosition = new Vector2(-1, -1);
	}

    public string Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public void SetRed(bool r){
		red = r;
	}

	public bool GetRed(){
		return red;
	}

	public void SetBoardPosition(int x, int y){
		boardPosition = new Vector2(x, y);
	}

	public Vector2 GetBoardPosition(){
		return boardPosition;
	}
}

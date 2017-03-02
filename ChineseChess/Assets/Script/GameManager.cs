using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance{set; get;}

	public GameObject mainMenu;
	public GameObject serverMenu;
	public GameObject connectMenu;

	// Use this for initialization
	private void Start () {
		Instance = this;
		serverMenu.SetActive(false);
		connectMenu.SetActive(false);
		DontDestroyOnLoad(gameObject);
	}

	public void ConnectButton(){
		Debug.Log("Connect");
	}

	public void HostButton(){
		Debug.Log("Host");
	}
	public void ConnectToServerButton(){

	}
	public void BackButton(){

	}
}

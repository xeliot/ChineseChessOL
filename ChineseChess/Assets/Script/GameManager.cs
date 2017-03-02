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
		mainMenu.SetActive(false);
		connectMenu.SetActive(true);
	}

	public void HostButton(){
		mainMenu.SetActive(false);
		serverMenu.SetActive(true);
	}
	public void ConnectToServerButton(){

	}
	public void BackButton(){
		mainMenu.SetActive(true);
		serverMenu.SetActive(false);
		connectMenu.SetActive(false);
	}
}

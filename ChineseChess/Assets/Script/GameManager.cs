using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager Instance{set; get;}

	public GameObject mainMenu;
	public GameObject serverMenu;
	public GameObject connectMenu;

	public GameObject serverPrefab;
	public GameObject clientPrefab;

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

		try
		{
			Server s = Instantiate(serverPrefab).GetComponent<Server>();
			s.Init();
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}

		mainMenu.SetActive(false);
		serverMenu.SetActive(true);
	}
	public void ConnectToServerButton(){
		string hostAddress = GameObject.Find("HostInput").GetComponent<InputField>().text;
		if(hostAddress == ""){hostAddress = "127.0.0.1";}
		try
		{
			Client c = Instantiate(clientPrefab).GetComponent<Client>();
			c.ConnectToServer(hostAddress, 6321);
			connectMenu.SetActive(false);
		}
		catch (Exception e){
			Debug.Log(e.Message);
		}
	}
	public void BackButton(){
		mainMenu.SetActive(true);
		serverMenu.SetActive(false);
		connectMenu.SetActive(false);
	}
}

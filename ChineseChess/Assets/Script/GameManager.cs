using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance{set; get;}

	public GameObject mainMenu;
	public GameObject serverMenu;
	public GameObject connectMenu;
	public GameObject aboutSection;

	public GameObject serverPrefab;
	public GameObject clientPrefab;

	public InputField nameInput;

	// Use this for initialization
	private void Start () {
		Instance = this;
		serverMenu.SetActive(false);
		connectMenu.SetActive(false);
		aboutSection.SetActive(false);
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

			Client c = Instantiate(clientPrefab).GetComponent<Client>();
			c.clientName = nameInput.text;
			c.isHost = true;
			if(c.clientName == ""){
				c.clientName = "Host";
			}
			c.ConnectToServer("127.0.0.1", 6321);
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}

		mainMenu.SetActive(false);
		serverMenu.SetActive(true);
	}
	public void AboutButton(){
		mainMenu.SetActive(false);
		aboutSection.SetActive(true);
	}
	public void DaveGithub(){
		Application.OpenURL("https://github.com/xeliot");
	}
	public void CCOLRepo(){
		Application.OpenURL("https://github.com/xeliot/ChineseChessOL");
	}
	public void ConnectToServerButton(){
		string hostAddress = GameObject.Find("HostInput").GetComponent<InputField>().text;
		if(hostAddress == ""){hostAddress = "127.0.0.1";}
		try
		{
			Client c = Instantiate(clientPrefab).GetComponent<Client>();
			c.clientName = nameInput.text;
			if(c.clientName == ""){
				c.clientName = "Client";
			}
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
		aboutSection.SetActive(false);

		Server s = FindObjectOfType<Server>();
		if(s != null){
			Destroy(s.gameObject);
		}

		Client c = FindObjectOfType<Client>();
		if(c != null){
			Destroy(c.gameObject);
		}
	}

	public void StartGame(){
		SceneManager.LoadScene("begin");
	}
}

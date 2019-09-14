using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SignUpController : MonoBehaviour {

	List<bool> inputIsBlank;
	public GameObject buttonSignUp;
	public GameObject loaderIcon;
	private User user;
	
	private Text serverErrorText;
	// Use this for initialization
	void Start () {
		serverErrorText = GameObject.Find("TextServerError").GetComponent<Text>();
		SceneChanger changer = gameObject.GetComponent<SceneChanger>();
		changer.FadeSceneOut(2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddUser()
	{ 
		string NickName = GameObject.Find("InputFieldNickName").GetComponent<InputField>().text;
		string FirstName = GameObject.Find("InputFieldFirstName").GetComponent<InputField>().text;
		string LastName = GameObject.Find("InputFieldLastName").GetComponent<InputField>().text;

		inputIsBlank = new List<bool>();
		ValidateInput(NickName, "TextNickNameError", "Necesitas un Apodo");
		ValidateInput(FirstName, "TextFirstNameError", "Necesitas un Nombre");
		ValidateInput(LastName, "TextLastNameError", "Necesitas un Apellido");

		bool foundBlankInput = inputIsBlank.Find(blank => blank);

		if( !foundBlankInput )
		{	
			serverErrorText.text = "";
			LoadSignUpAnimation(true);

			user = new User();
			user.NickName = NickName;
			user.FirstName = FirstName;
			user.LastName = LastName;
			SendUserSignUp(user);
		}
	}

	private void SendUserSignUp(User user){
		
		HttpRequestApi api = gameObject.AddComponent<HttpRequestApi>();

		List<User> rows = new List<User>();
		rows.Add(user);

		APPInfo appInfo = new APPInfo();
		appInfo.page = 1;
		appInfo.size = 1000;
		appInfo.domain = 246;
		appInfo.row_model = "User";
		appInfo.rows = rows;

		var JSON_Body = JsonUtility.ToJson(appInfo);

		Debug.Log(JSON_Body);

		PostRequestCallback signUpMethod = FinishSignUp;
		var result = StartCoroutine(api.PostRequest(JSON_Body, signUpMethod));
	}

	public void ChangeScene()
	{
		SceneChanger changer = gameObject.GetComponent<SceneChanger>();
		changer.LoadNewScene();
	}

	private void FinishSignUp(bool requestError, string response)
	{
		if(requestError)
		{	
			WebUser webUser = new WebUser();
			string key = "";
			try
			{
				JsonUtility.FromJsonOverwrite(response, webUser);

				if(webUser.success)
				{
					ChangeScene();
					key = webUser.keys[0];
					
					SaveSystem.SaveGameStatus("", ElfStatus.baseLife, ElfStatus.baseEnergy, Vector3.zero, user, key);
					Debug.Log("This is the user key");
					Debug.Log(key);
				}
				else
				{
					LoadSignUpAnimation(false);
					serverErrorText.text = webUser.error;
					Debug.Log(webUser.error);
				}
			}
			catch(Exception e){
				LoadSignUpAnimation(false);
				Debug.Log(e);
			}
		}
		else
		{
			LoadSignUpAnimation(false);
		}
	}

	private void LoadSignUpAnimation(bool isLoading){
		loaderIcon.SetActive(isLoading);
		buttonSignUp.SetActive(!isLoading);
	}

	private void ValidateInput(string inputText, string errorTextField, string textError)
	{
		inputIsBlank.Add(EmptyInput(inputText, errorTextField, textError));
	}

	private bool EmptyInput(string inputText, string errorTextFieldId, string textError)
	{
		GameObject errorTextField = GameObject.Find(errorTextFieldId);
		bool IsNullOrEmpty = string.IsNullOrEmpty(inputText);

		if(IsNullOrEmpty)
		{
			errorTextField.SetActive(true);
			errorTextField.GetComponent<Text>().text = textError;
		}
		else{
			errorTextField.GetComponent<Text>().text = "";
		}

		return IsNullOrEmpty;
	}
}

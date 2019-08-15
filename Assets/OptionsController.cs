using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour {
	// Use this for initialization
	private Animator animator;
	private DialogsController dialogController;
	private float closingAnimationTime = 2f;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ShowBook(DialogsController dialogController)
	{
		this.dialogController = dialogController;
		gameObject.SetActive(true);

		GameObject book = gameObject.transform.Find("Book").gameObject;
		animator = book.GetComponent<Animator>();
		animator.Play("BookOpen");
	}

	public void CloseBook()
	{
		animator.Play("BookClose");
		StartCoroutine(hideOptionsPanel());
	}

	IEnumerator hideOptionsPanel()
	{
		print("Hola");
		print(Time.time);
		yield return new WaitForSeconds(closingAnimationTime);
		print(Time.time);
		gameObject.SetActive(false);
		dialogController.CloseDialog();
		print("Adios");
	}
}

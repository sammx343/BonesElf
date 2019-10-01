using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour {
	// Use this for initialization
	private Animator animator;
	GameObject book;
	private DialogsController dialogController;
	private float closingAnimationTime = 1f;

	public void ShowBook(DialogsController dialogController)
	{
		this.dialogController = dialogController;
		gameObject.SetActive(true);

		book = gameObject.transform.Find("Book").gameObject;
		book.GetComponent<Fader>().fadeObjectWithChilds(FadeDirection.In, closingAnimationTime);

		animator = book.GetComponent<Animator>();
		//animator.Play("BookOpen");
	}

	public void CloseBook()
	{
		//animator.Play("BookClose");
		book.GetComponent<Fader>().fadeObjectWithChilds(FadeDirection.Out, closingAnimationTime);
		StartCoroutine(hideOptionsPanel());
	}

	IEnumerator hideOptionsPanel()
	{
		// print("Hola");
		// print(Time.time);
		yield return new WaitForSeconds(closingAnimationTime);
		// print(Time.time);
		gameObject.SetActive(false);
		dialogController.CloseDialog();
		// print("Adios");
	}
}

using UnityEngine;
using System.Collections;

public class StartLevelButton : MyButton
{
	protected override void Click()
	{
		base.Click();

		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
	}
}

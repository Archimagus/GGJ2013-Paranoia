using UnityEngine;
using System.Collections;

public class QuitButton : MyButton
{
	protected override void Click()
	{
		base.Click();
		Application.Quit();
	}
}

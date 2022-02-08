using UnityEngine;
using UnityEditor;

public class CurveToggleWindow : EditorWindow
{
	[MenuItem("Window/Curve Toggle")]
	public static void ShowWindow()
	{
		GetWindow<CurveToggleWindow>("Curve Toggle");
	}

	private void OnGUI()
	{
		if(GUILayout.Button("Turn Curve Off"))
		{
			Enable();
		}

		if(GUILayout.Button("Turn Curve On"))
		{
			Disable();
		}
	}

	private void Enable()
	{
		Shader.EnableKeyword("IN_EDITOR");
		Debug.Log("Disabling world curve, this may take several seconds");
	}

	private void Disable()
	{
		Shader.DisableKeyword("IN_EDITOR");
		Debug.Log("Enabling world curve, this may take several seconds");
	}
}
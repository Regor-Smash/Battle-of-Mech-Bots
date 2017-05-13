using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyColorPicker : MonoBehaviour {

	public Texture2D colorPicker;
	public CustomColorUpdater colorUpdater;
	Color tempColor;

	int pixX;
	float sizeX;
	int pixY;
	float sizeY;

	Vector2 mousePos;

	void Awake () {
		GetComponent<RawImage> ().texture = colorPicker;
	}

	public void ApplyColor () {
		//Get pixels per UI unit
		sizeX = colorPicker.width / GetComponent<RectTransform> ().rect.width;
		sizeY = colorPicker.height / GetComponent<RectTransform> ().rect.height;

		//Get position of mouse with respect to the center of the color picker
		RectTransformUtility.ScreenPointToLocalPointInRectangle (GetComponent<RectTransform> (), Input.mousePosition, Camera.current, out mousePos);

		//Get position of mouse (in pixels) with respect to the lower left corner of the color picker
		pixX = (int) (mousePos.x * sizeX + colorPicker.width/2);
		pixY = (int) (mousePos.y * sizeY + colorPicker.height/2);

		//Get selected color at pixel
		tempColor = colorPicker.GetPixel (pixX, pixY);
		colorUpdater.UpdateColor (tempColor);
	}
}

using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Colors Data", menuName = "Data Templates/Colors", order = 1)]
public class ColorData : ScriptableObject {

	public Color dayColor;
	public Color sunsetColor;
	public Color nightColor;
	public Color dawnColor;

	public Color wubSkyColor;
	public Color[] wubGlowColors;
}

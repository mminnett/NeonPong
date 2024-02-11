using UnityEngine;
using UnityEngine.UI;

public class NumericSprite : MonoBehaviour
 {
	 int _number;

	 public Sprite[] numbers;

     // Set the number for the current score. Note how the numbers array is setup in editor
	 public int Number
	 {
		 get { return _number; }
		 set
		 {
			 _number = Mathf.Clamp(value, 0, numbers.Length - 1);
			 GetComponent<Image>().sprite = numbers[_number];
		 }
	 }
}

# Unity Ease Function

Ease functions I used for other Unity projects because Unity doesn't seem to have any.  
Will be adding more functions.

## Features

* Good for faking physical movement
* Can be used to create counter
* Configurable Monomial Shaping Ease Function
* Overshoot / Undershoot controlling

## Examples

```cs
	private int frame;
	void Start () {
		frame ++;
	}

	void FixedUpdate () {

		// Moves this object in an interesting way!
		this.transform.position = new Vector3(EaseFunctions.EaseIn(0, 100, 3, frame, 180, false, false), 0, 0);

		// Creates frame counter!
		Text textComponent = this.gameObject.GetComponent<Text>();
		textComponent.text = Math.Floor(EaseFunctions.Linear(0, 1, frame, 1, true, false)).ToString();
	}
```

## Functions Currently Available

* Linear
* EaseIn
* EaseOut
	* Can set its rate freely

## I think I found a bug

Please create new [issue](https://github.com/Clpsplug/UnityEaseFunction/issues/new), thanks!

## Installation

Just put the `EaseFunctions.cs` into Assets folder and it should work!

# License

MIT License

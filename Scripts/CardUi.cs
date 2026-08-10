using Godot;
using System;

public partial class CardUi : Control
{
	// Called when the node enters the scene tree for the first time.
	public string cardName { get; init; }
	[Export] public string cardID { get; set; }
	public string cardType { get; init; }
	public int cardPower { get; init; }
	public string cardEffect { get; init; }
	public string cardText { get; init; }

	private CardAssets _assets { get; init; } = new CardAssets();

	private bool _flipped;
	private Tween _flipTween;

	private Texture2D cardFrontTexture { get; set; }
	private Texture2D cardBackTexture { get; set; }



	public override void _Ready()
	{
		cardFrontTexture = GD.Load<Texture2D>(_assets.FindCard(cardID));
		cardBackTexture = GD.Load<Texture2D>(_assets.GetCardBack(cardType));
		var artRect = GetNode<TextureRect>("ArtRect");
		artRect.Texture = cardFrontTexture;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _GuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton b
			&& b.Pressed
			&& b.ButtonIndex == MouseButton.Left)
		{
			Flip();
		}
	}

	public void Tap()
	{
		PivotOffset = Size / 2f;
		RotationDegrees += 90f;
	}

	public void Flip()
	{
		// guard: ignore clicks while animating
		if (_flipTween != null && _flipTween.IsRunning()) return;

		PivotOffset = Size / 2f;   // fold around center, not corner

		_flipTween = CreateTween();
		_flipTween.TweenProperty(this, "scale:x", 0f, 0.15f);
		_flipTween.TweenCallback(Callable.From(() =>
	{
		// swap texture here
		_flipped = !_flipped;
		if (_flipped)
		{
			_LoadCardBack();
		}
		else
		{
			_LoadCardFront();
		}

	}));
		_flipTween.TweenProperty(this, "scale:x", 1f, 0.15f);


	}

	private void _LoadCardBack()
	{

		var artRect = GetNode<TextureRect>("ArtRect");
		artRect.Texture = cardBackTexture;
	}

	private void _LoadCardFront()
	{
		var artRect = GetNode<TextureRect>("ArtRect");
		artRect.Texture = cardFrontTexture;
	}
}

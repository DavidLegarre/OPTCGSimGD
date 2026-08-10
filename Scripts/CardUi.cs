using Godot;

public partial class CardUi : Control
{
	[Export] public string cardID { get; set; }

	private CardAssets _assets { get; init; } = new CardAssets();

	private bool _flipped;
	private Tween _flipTween;

	private Texture2D cardFrontTexture { get; set; }
	private Texture2D cardBackTexture { get; set; }

	public override void _Ready()
	{
		cardFrontTexture = GD.Load<Texture2D>(_assets.FindCard(cardID));
		cardBackTexture = GD.Load<Texture2D>(_assets.GetCardBack());
		_SetTexture(cardFrontTexture);
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
		_SetTexture(_flipped ? cardBackTexture : cardFrontTexture);
	}));
		_flipTween.TweenProperty(this, "scale:x", 1f, 0.15f);
	}

	private void _SetTexture(Texture2D texture)
	{
		GetNode<TextureRect>("ArtRect").Texture = texture;
	}
}

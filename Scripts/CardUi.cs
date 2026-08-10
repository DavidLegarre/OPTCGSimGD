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

	public override void _Ready()
	{
		var tex = GD.Load<Texture2D>(_assets.FindCard(cardID));
		var artRect = GetNode<TextureRect>("ArtRect");
		artRect.Texture = tex;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

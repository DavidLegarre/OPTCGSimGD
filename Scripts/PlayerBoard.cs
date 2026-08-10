using Godot;
using System;

public partial class PlayerBoard : Control
{
	private PackedScene _cardScene;
	private string _cardScenePath = "res://Scenes/card_ui.tscn";


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_cardScene = GD.Load<PackedScene>(_cardScenePath);
		FillCharacterArea();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private CardUi SpawnCard(string cardID, Node parent)
	{
		var card = (CardUi)_cardScene.Instantiate();
		card.cardID = cardID;
		parent.AddChild(card);
		return card;
	}

	private void FillCharacterArea()
	{
		var area = GetNode<Container>("%CharacterArea");
		for (int i = 0; i < 5; i++) SpawnCard("OP16-001", area);

	}
}

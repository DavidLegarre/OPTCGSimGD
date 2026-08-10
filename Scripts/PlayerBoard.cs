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
		FillSingleArea("%LeaderArea");
		FillSingleArea("%StageArea");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private async void SpawnCard(string cardID, Control parent)
	{
		var card = (CardUi)_cardScene.Instantiate();
		card.cardID = cardID;
		card.CustomMinimumSize = new Vector2(90, 128);
		parent.AddChild(card);
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		var h = parent.Size.Y;
		card.CustomMinimumSize = new Vector2(h * CardUi.CardAspect, h);
	}

	private void FillCharacterArea()
	{
		var area = GetNode<Container>("%CharacterArea");
		for (int i = 0; i < 5; i++) SpawnCard("OP16-001", area);

	}

	private void FillSingleArea(string areaName)
	{
		var area = GetNode<Container>(areaName);
		SpawnCard("OP16-001", area);
	}
}

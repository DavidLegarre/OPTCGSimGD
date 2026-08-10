using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class PlayerBoard : Control
{
	private PackedScene _cardScene;
	private string _cardScenePath = "res://Scenes/card_ui.tscn";
	private readonly List<CardUi> _lifeCards = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_cardScene = GD.Load<PackedScene>(_cardScenePath);
		FillCharacterArea();
		FillSingleArea("%LeaderArea");
		FillSingleArea("%StageArea");
		FillLifeArea();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private CardUi SpawnCard(string cardID, Control parent)
	{
		var card = (CardUi)_cardScene.Instantiate();
		card.cardID = cardID;
		card.CustomMinimumSize = new Vector2(90, 128);
		parent.AddChild(card);
		return card;
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

	private async void FillLifeArea()
	{
		var area = GetNode<Container>("%LifeArea");
		for (int i = 0; i < 5; i++)
		{
			var card = _cardScene.Instantiate<CardUi>();
			card.cardID = "OP16-001";
			card.CustomMinimumSize = new Vector2(90, 128);
			area.AddChild(card);
			_lifeCards.Add(card);
		}
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		foreach (var card in _lifeCards)
		{
			card.RotationDegrees = -90;
			card.Flip();
		}
	}
}

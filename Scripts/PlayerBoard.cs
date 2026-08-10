using Godot;
using System.Collections.Generic;

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
		FillSingleArea("%LeaderSlot");
		FillSingleArea("%StageSlot");
		FillSingleArea("%DeckSlot");
		FillSingleArea("%DonDeckSlot");
		FillSingleArea("%TrashSlot");
		FillLifeArea();
		FillDonArea();
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
		var area = GetNode<Container>("%CharRow");
		for (int i = 0; i < 5; i++) SpawnCard("OP16-001", area);
	}

	private void FillSingleArea(string areaName)
	{
		var area = GetNode<Container>(areaName);
		SpawnCard("OP16-001", area);
	}

	private void FillLifeArea()
	{
		var area = GetNode<Container>("%LifeStack");
		for (int i = 0; i < 5; i++)
		{
			var card = SpawnCard("OP16-001", area);
			card.Flip();
			card.Tap();
			_lifeCards.Add(card);
		}
	}

	private void FillDonArea()
	{
		var area = GetNode<Container>("%DonAreaSlot");
		for (int i = 0; i < 10; i++) SpawnCard("OP16-001", area);
	}
}

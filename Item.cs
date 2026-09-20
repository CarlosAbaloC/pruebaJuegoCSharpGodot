using Godot;
using System;

public partial class Item : Area2D
{
	private RandomNumberGenerator rng = new RandomNumberGenerator();
	private bool posicionValida = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnItemAreaEntered;
		AddToGroup("items");

		//GD.Print(GetParent().Name);
		//GD.Print(GetParent().GetScript());
		//GD.Print(GetParent().GetType());
		

		// Position = new Vector2(
		// 	rng.RandfRange(30, 1122),
		// 	rng.RandfRange(20, 620)
		// );
		
		Vector2 randomPosition;
		do{
			posicionValida = true;

			randomPosition =new Vector2(
				rng.RandfRange(30, 1122),
				rng.RandfRange(20, 620) 
			);
			

			foreach(Item item in GetTree().GetNodesInGroup("items"))
			{
				if(randomPosition.DistanceTo(item.Position) < 116)
				{
					//GD.Print("Intento colisionar");
					posicionValida = false;
					break;
				}
			}
		}  
		while (!posicionValida);

		Position = randomPosition;

		
		CollisionShape2D col = GetNode<CollisionShape2D>("CollisionShape2D");
		RectangleShape2D rect = (RectangleShape2D)col.Shape;

		Vector2 tamanio = rect.Size;

		//GD.Print(tamanio);
	}

	private void OnItemAreaEntered(Area2D otro)
	{
		/*
		if(otro.Name == "Personaje") {
		GD.Print("Chocado");
		GetParent().Call("IncrementarPuntos");
		QueueFree();
		}
		*/
		//GD.Print("Chocado");

		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}

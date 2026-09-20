using Godot;
using System;

public partial class Bienvenida : Control
{
	private bool accion = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		//GD.Print("ENTRA");
	    GetNode<Label>("Label1").Text = "EL NUEVO PROYECTO";

		if (OS.GetName() == "Android")
		{
			//GD.Print("ANDROID");
			GetNode<Label>("Label2").Text = OS.GetName();
			GetNode<Label>("Label3").Text = "FUNCIONA";

		}
		else
		{
			GetNode<Label>("Label2").Text = "NO funciona";
			//GD.Print("OTRO");
		}
	}

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventScreenTouch touch && touch.Pressed)
			accion = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.J) || accion)
		{
			GetTree().ChangeSceneToFile("res://escena_juego.tscn");
		}
	}
}

using Godot;
using System;

public partial class Salida : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		int puntuacionFinal = 0;
		Vector2 tamanioPantalla = GetViewportRect().Size;
		puntuacionFinal = Global.Puntos;
		Label puntuacion = GetNode<Label>("Puntuacion");
		Label textoComentario = GetNode<Label>("Comentario");
		Label decision = GetNode<Label>("Decision");
		Button Continuar = GetNode<Button>("Continuar");
		Continuar.Pressed += volver;
		Button Salir = GetNode<Button>("Salida");
		Salir.Pressed += salir;
		Global.tiempoCaida = 1;

		puntuacion.Text = "Puntuacion:" + puntuacionFinal;
		var button = new Button();
		button.Text = "Crear";
		AddChild(button);

		
		puntuacion.Position = new Vector2(tamanioPantalla.X/2 - puntuacion.Size.X/2, tamanioPantalla.Y/120);
		textoComentario.Position = new Vector2(tamanioPantalla.X/2 - textoComentario.Size.X/2, puntuacion.Position.Y + puntuacion.Size.Y);
		decision.Position = new Vector2(tamanioPantalla.X/2 - decision.Size.X/2, textoComentario.Position.Y + textoComentario.Size.Y);
		button.Position = new Vector2(tamanioPantalla.X/2 - button.Size.X/2, decision.Position.Y + decision.Size.Y);
		Continuar.Position = new Vector2(tamanioPantalla.X/4 - Continuar.Size.X/2, button.Position.Y + button.Size.Y);
		Salir.Position = new Vector2((tamanioPantalla.X * 3) / 4 - Salir.Size.X/2, button.Position.Y + button.Size.Y);
		

		if(puntuacionFinal < 200) {
			Label comentario = GetNode<Label>("Comentario");
			comentario.Text = "Inutil";
		}
		else if(puntuacionFinal < 400) {
			Label comentario = GetNode<Label>("Comentario");
			comentario.Text = "Novato";
		}
		else if(puntuacionFinal < 700) {
			Label comentario = GetNode<Label>("Comentario");
			comentario.Text = "Bueno";
		}
		else if(puntuacionFinal < 1000) {
			Label comentario = GetNode<Label>("Comentario");
			comentario.Text = "Increible";
		}
		else if(puntuacionFinal < 1400) {
			Label comentario = GetNode<Label>("Comentario");
			comentario.Text = "Pro";
		}

		Global.Puntos = 0;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.J))
		{
			GetTree().ChangeSceneToFile("res://escena_juego.tscn");
		}
		if (Input.IsKeyPressed(Key.X))
		{
			GetTree().Quit();
		}

	}

	private void volver()
	{
		GD.Print("Volver");
		GetTree().ChangeSceneToFile("res://escena_juego.tscn");
	}

	private void salir()
	{
		GD.Print("Salir");
		GetTree().Quit();
	}
	private void ButtonPressed()
	{
		GD.Print("Hello world!");
	}
}

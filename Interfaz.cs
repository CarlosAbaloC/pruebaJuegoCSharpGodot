using Godot;
using System;

public partial class Interfaz : Godot.Control
{

	private int _total_TimeInSecs = 0;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Vector2 tPantalla = GetViewportRect().Size;

		Label tPosicion = GetNode<Label>("TextoTiempo");
		tPosicion.Position = new Vector2(tPantalla.X/2 - tPosicion.Size.X/2, tPosicion.Position.Y);


	}
	
	private void _on_timer_timeout() {
		_total_TimeInSecs++;
	
		int m = (int)(_total_TimeInSecs /60f);
		int s = _total_TimeInSecs - m * 60;
		GetNode<Label>("TextoTiempo").Text = "Temporizador: " + m.ToString("D2") + ":" + s.ToString("D2");
	}
}

using Godot;
using System;

public partial class Flecha : Area2D
{
	//Ulong es una variable numerica entera y larga sin signos de c#
    private ulong idCaida;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnItemAreaEntered;
		
		GD.Print("=== FLECHA CREADA ===");
		GD.Print("ID flecha: " + GetInstanceId());
		GD.Print("Padre: " + GetParent());
		GD.Print("Nombre padre: " + GetParent().Name);
		GD.Print("ID padre: " + GetParent().GetInstanceId());

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void OnItemAreaEntered(Area2D otro)
	{

		if(idCaida == otro.GetInstanceId()) {
			GD.Print("=== COLISIÓN ===");
			GD.Print("Flecha ID: " + GetInstanceId());
			GD.Print("Padre de esta flecha: " + GetParent().Name);
			GD.Print("ID padre: " + GetParent().GetInstanceId());
			GD.Print("Ha chocado con: " + otro.Name);
			GD.Print("ID del que choca: " + otro.GetInstanceId());
			QueueFree();
		}
		
		
	}
	public ulong IdCaida
	{
		get { return idCaida; }
		private set { idCaida = value; }
	}
	public void AsignarCaida(ulong id)
	{
		idCaida = id;
	}
}

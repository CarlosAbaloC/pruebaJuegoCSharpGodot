using Godot;
using System;

public partial class Enemigo : Area2D
{
	public int velocidad;
	private EscenaJuego juego;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup("enemigos");

		AreaEntered += OnItemAreaEntered;
   		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("andar");
		velocidad = 50;

		//Velocidad basica, el movimiento normal sin la parte de buscar al personaje
		//velocidad = new Vector2(150, 0);

		juego = GetNode<EscenaJuego>("/root/EscenaJuego");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 posPersonaje = juego.GetPosicionPersonaje();
		Vector2 distancia = posPersonaje - Position;

		Vector2 haciaJugador = (posPersonaje - Position).Normalized();

		Vector2 separacion = ObtenerSeparacion();

		Vector2 direccionFinal = haciaJugador + separacion * 0.3f;

		float dt = (float)delta;

		Position += direccionFinal.Normalized() * velocidad * dt;
/*
		float dt = (float)delta;
		//Sumamos la distancia del personaje pero normalizada, para que no 
		// vaya mas rapido si esta lejos del personaje
		Position += distancia.Normalized() * velocidad * dt;

*/
		/*
		Esta es la forma de que se muevan para alante y para atras
		float dt = (float)delta;

		Position += velocidad * dt;
		if(Position.X > 900) {
			velocidad = new Vector2(-150, 0);
		}

		if(Position.X < 0)
		{
			velocidad = new Vector2(150, 0);
		}
		*/

	} 

	private Vector2 ObtenerSeparacion()
	{
		Vector2 separacion = Vector2.Zero;

		foreach (Node nodo in GetTree().GetNodesInGroup("enemigos"))
		{
			Enemigo otroEnemigo = nodo as Enemigo;

			if (otroEnemigo == this)
				continue;

			float distancia = Position.DistanceTo(otroEnemigo.Position);

			if (distancia < 55)
			{
				Vector2 alejamiento = Position - otroEnemigo.Position;

				separacion += alejamiento.Normalized();
			}
		}

		return separacion;
	}

	private void OnItemAreaEntered(Area2D otro)
	{
		if(otro.Name == "Personaje") {
		//GD.Print("asdasdasdasdasd");
		
		}
		
	}
}

using Godot;
using System;
//SonidoMoneda
public partial class Personaje : Area2D
{
	public float velocidad = 100;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		AreaEntered += OnItemAreaEntered;
		
		
		//sprite = GetNode<AnimatedSprite>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Convertimos delta a float porque Vector2 trabaja con float en Godot 4.
		float dt = (float)delta;
		//Hay que ponerlo en este orden que es como lo lee el GetVector
		Vector2 teclado = Input.GetVector(
			"ui_left",
			"ui_right",
			"ui_up",
			"ui_down"
		);
		
		// if (Input.IsActionPressed("ui_right"))
		// {
		// 	/*Para cambiar el sprite segun a donde vaya
		// 		sprite.Play("derecha)
			
		// 	*/
		// 	Position += new Vector2(velocidad * dt, 0);
		// 	// Esto sirve para que la velocidad sea constante independientemente de los FPS.
		// }
		// if (Input.IsActionPressed("ui_left"))
		// {
		// 	Position += new Vector2(-velocidad * dt, 0);
		// 	// Esto sirve para que la velocidad sea constante independientemente de los FPS.
		// }
		// if (Input.IsActionPressed("ui_up"))
		// {
		// 	Position += new Vector2(0, -velocidad * dt);
		// 	// Esto sirve para que la velocidad sea constante independientemente de los FPS.
		// }
		// if (Input.IsActionPressed("ui_down"))
		// {
		// 	Position += new Vector2(0, velocidad * dt);
		// 	// Esto sirve para que la velocidad sea constante independientemente de los FPS.
		// }
		// else
		// {
		// 	//sprite.Play("default");
		// }
		Position += teclado * velocidad * dt;
		Vector2 tamañoPantalla = GetViewportRect().Size;

		Position = new Vector2(
			Mathf.Clamp(Position.X, 23, tamañoPantalla.X - 23),
			Mathf.Clamp(Position.Y, 22, tamañoPantalla.Y - 22)
		);
	}

	private void OnItemAreaEntered(Area2D otro)
	{

		if(otro.IsInGroup("items")) {
			GetParent().Call("IncrementarPuntos");
			AudioStreamPlayer sonido = GetNode<AudioStreamPlayer>("/root/EscenaJuego/SonidoMoneda");
			sonido.Play(0.4f);
			otro.QueueFree();
		
		}
		else if(otro.IsInGroup("caidas")) {
			GD.Print("Ha chocado con el gastli");
			AudioStreamPlayer sonido = GetNode<AudioStreamPlayer>("/root/EscenaJuego/SonidoGolpe");
			sonido.Play(0.4f);
			GetParent().Call("PerdidaVidas"); 
		
		}
		else if(otro.IsInGroup("enemigos")) {
			//GD.Print("Ha chocado con los enemigos");
			AudioStreamPlayer sonido = GetNode<AudioStreamPlayer>("/root/EscenaJuego/SonidoGolpe");
			sonido.Play(0.4f);
			GetParent().Call("PerdidaVidas");
		}
		else {
			//GD.Print("No se que mierda ha tocado");
		}
		
	}

	
}

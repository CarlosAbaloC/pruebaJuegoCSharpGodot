using Godot;
using System;

public partial class Caida : Area2D
{
	private PackedScene objeto;
	private RandomNumberGenerator rng = new RandomNumberGenerator();
	private Vector2 velocidad;

	
	private float tiempo = 0f;
	private float duracion = 4f;
	private float tiempoParpadeo = 0f;
	private bool caida = false;

	private Sprite2D imagen;
	private CollisionShape2D colision;

	// Called when the node enters the scene tree for the first time.
	
	//Esta es la forma de crear objetos llamados flechas asi como su 
	// relacion entre ellas y la conexion entre ka cauda y la flecha
	//Los .. son comentarios
	// public override void _Ready()
	// {
	// 	objeto = GD.Load<PackedScene>("res://Flecha.tscn");

	// 	AddToGroup("caidas");
	// 	Vector2 tPantalla = GetViewportRect().Size;
	// 	..GD.Print("YA CAE");
	// 	..Se pone el node porque hace falta para modificar datos 
	// 	var flecha = objeto.Instantiate<Flecha>();
	// 	GetParent().AddChild(flecha);
	// 	flecha.AsignarCaida(GetInstanceId());
	// 	velocidad = new Vector2(0, 50);
	// 	Vector2 posicionObjeto = new Vector2(	rng.RandfRange(20, tPantalla.X - 20), -300);
	// 	Position = posicionObjeto;
	// 	Vector2 tamañoPantalla = GetViewportRect().Size;
	// 	flecha.Position = new Vector2(
	// 		posicionObjeto.X,
	// 		20
	// 	);
	// 	var sprite = flecha.GetNode<Sprite2D>("Sprite2D");
	// 	sprite.Scale = new Vector2(0.1f, 0.1f);
	// 	sprite.Visible = true;
		
	// 	..GD.Print("Flecha creada: " + objetoCae);
	// 	..GD.Print("Posición: " + objetoCae.Position);
	// 	..GD.Print("Sprite: " + sprite);
	// 	..GD.Print("Textura: " + sprite.Texture);
	// 	..GD.Print("Visible: " + sprite.Visible);
	// 	..GD.Print("Tamaño: " + sprite.Texture.GetSize());
	// 	AreaEntered += OnItemAreaEntered;
	// }

	public override void _Ready()
	{
		objeto = GD.Load<PackedScene>("res://Flecha.tscn");

		AddToGroup("caidas");
		Vector2 tPantalla = GetViewportRect().Size;
		velocidad = new Vector2(0, 50);
		Position = new Vector2(	rng.RandfRange(20, tPantalla.X - 20), 20);
		Vector2 tamañoPantalla = GetViewportRect().Size;
		
		imagen = GetNode<Sprite2D>("Sprite2D");
		colision = GetNode<CollisionShape2D>("CollisionShape2D");
		colision.Disabled = true;
		imagen.Hframes = 1;
		imagen.Frame = 0;
		//Formas de girar una imagen:
		imagen.Rotation = Mathf.DegToRad(90);
		//imagen.RotationDegrees = -90;
		imagen.Scale = new Vector2(0.1f, 0.1f);
		


		AreaEntered += OnItemAreaEntered;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float dt = (float)delta;
		tiempo += dt;
		
		
		Vector2 tPantalla = GetViewportRect().Size;

		if(tiempo >= duracion && !caida)
		{
			imagen.Visible = true;
			imagen.Texture = GD.Load<Texture2D>("res://assets/FireBall/caida.jpg");
			Position = new Vector2(Position.X, -40);
			imagen.Hframes = 1;
			imagen.Vframes = 1;
			imagen.Frame = 0;
			imagen.RotationDegrees = 0;
			imagen.Scale = new Vector2(0.1f, 0.1f);
			colision.Disabled = false;

	
			caida = true;
			//Cambio de imagen
			return;
		}


		//Se va acercando al 1 
		float progreso = tiempo/duracion;

		//Frecuencia del parpadeo
		float frecuencia = Mathf.Lerp(0f, 15f, progreso);

		if(frecuencia > 0 && !caida) 
		{
			tiempoParpadeo += dt;
			if(tiempoParpadeo >= 1f / frecuencia)
			{
				imagen.Visible = !imagen.Visible;
				tiempoParpadeo = 0f;
			}
		}
		if(caida) {
			Position += velocidad * dt;
		}

		if(Position.Y > tPantalla.Y)
		{
			QueueFree();
		} 
	}

	public float Velocidad
	{
		get {return velocidad.Y;}
		set {velocidad.Y = value;}
	}

	private void OnItemAreaEntered(Area2D otro)
	{
		//GD.Print("Chocado con la caida");	
	}
}

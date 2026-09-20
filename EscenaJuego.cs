using Godot;
using System;


public partial class Global : Node
{
    public static int Puntos = 0;
	public static int altoVentana = 0;
	public static int anchoVentana = 0;
	public static int cuentaFondo = 1;
	public static int tiempoCaida = 1;
	
}

//PARA PARAR EL TIEMPO ES TIMER.PAUSE Y LUEGO TRUE O FALSE, TRUE LO PARA FALSE LO CONTINUA
public partial class EscenaJuego : Node2D
{
	private int vidas = 5;
	private Personaje personaje;
	private RandomNumberGenerator rng = new RandomNumberGenerator();
	private RandomNumberGenerator rngNum = new RandomNumberGenerator();
	private PackedScene objeto;
	private float tiempoEspera;
	private float tiempoEntreCaidas;
	private int num = 0;
	private int velocidadAum = 0;
	//private int num = 3;
	private TextureRect fondo;
	private CheckButton bPantalla;
	private Label lPantalla;

	private Enemigo enemigo;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Vector2 tamañoPantalla = GetViewportRect().Size;
		//GD.Print("Ancho: " + tamañoPantalla.X);
		//GD.Print("Alto: " + tamañoPantalla.Y);
	    //GD.Print("SCRIPT DE ESCENAJUEGO CARGADO");
		rng.Randomize();

		lPantalla = GetNode<Label>("/root/EscenaJuego/MenuPause/CanvasLayer/Control/VentanaMenu/LFullScreen");
		bPantalla = GetNode<CheckButton>("/root/EscenaJuego/MenuPause/CanvasLayer/Control/VentanaMenu/BFullScreen");


		if(OS.HasFeature("mobile")) 
		{
			GD.Print("Esto es movil");
			lPantalla.Visible = false;
			bPantalla.Visible = false;
		}
		else
		{
			bPantalla.Toggled += CambiarPantalla;
		}
		
		//Modigica el nodo para poder parar el juego
    	ProcessMode = Node.ProcessModeEnum.Pausable;

		personaje = GetNode<Personaje>("/root/EscenaJuego/Personaje");
		enemigo = GetNode<Enemigo>("Enemigo");
		objeto = GD.Load<PackedScene>("res://caida.tscn");
		fondo = GetNode<TextureRect>("/root/EscenaJuego/CanvaFondo/Fondo");

		tiempoEntreCaidas = 5.0f;
		tiempoEspera = 0;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		tiempoEspera += (float)delta;
		//Delta es el tiempo
		//Cambiamos el inicio de la cadia con el space a la caida con tiempo de espera
		//if(Input.IsKeyPressed(Key.Space))
		if(tiempoEspera > tiempoEntreCaidas)
		{
			//GD.Print("Cayendo");
			var objetoCae = objeto.Instantiate<Caida>();
			AddChild(objetoCae);

			GD.Print("Base: ", objetoCae.Velocidad);
			GD.Print("Aumento: ", velocidadAum);

			objetoCae.Velocidad += velocidadAum;

			GD.Print("FINAL: ", objetoCae.Velocidad);
			tiempoEspera = 0;
		}
		
	}

	public void IncrementarPuntos() {
		Global.Puntos += 10;

		Control iu = GetNode<Control>("/root/EscenaJuego/MenuPause/CanvasLayer/Control");
		iu.ActualizarPuntos(Global.Puntos);
		if(Global.Puntos %	 100 == 0)
		{
			string texto = "fondo" + Global.cuentaFondo;
			// TextureRect es el nodo que muestra la imagen, y Texture2D es la imagen que contiene.
			fondo.Texture = GD.Load<Texture2D>("res://assets/Personajes/" + texto + ".jpg");
			if(Global.cuentaFondo == 1) {
				Global.cuentaFondo = 2;
			}
			else 
			{
				Global.cuentaFondo = 1;
			}
		}
		CallDeferred(nameof(CrearNuevoItem));
	}

	public Vector2 GetPosicionPersonaje()
	{
		return personaje.Position;
	}

	public void CrearNuevoItem()
	{
		PackedScene escenaItem = GD.Load<PackedScene>("res://item.tscn");
		Item item = escenaItem.Instantiate<Item>();

		

		AddChild(item);
	}


	public void PerdidaVidas() {
		//GD.Print("Llegaste");
		
		
		vidas += -1;
		//GD.Print("Puntos: " + puntos);
		Control iu = GetNode<Control>("/root/EscenaJuego/MenuPause/CanvasLayer/Control");

		iu.ActualizarVidas(vidas);
	}
	public void _on_timer_timeout () 
	{
		
		GD.Print("Han pasado 5 segundos");
		if(num == 1) {
			//GD.Print("1");
			personaje.velocidad += 25;
		}
		else if(num == 2) {
			//GD.Print("2");
			AumentarDificultad();
		}
		else {
			//GD.Print("3");
			
			GD.Print("Tiempo actual" + Global.tiempoCaida);
			int numRandom = rngNum.RandiRange(1, 2);

			//GD.Print("El numero es: " + numRandom);
			if(tiempoEntreCaidas >= 1.5f && numRandom == 1) {
				tiempoEntreCaidas -= 0.1f;
				//GD.Print("El numero es: Caida: " + numRandom + " -- tiempo de caidas: " + tiempoEntreCaidas);

			}
			else if(tiempoEntreCaidas < 1.0f && velocidadAum < 200 || numRandom == 2){
				//GD.Print("El numero es: Velocidad " + numRandom);
				velocidadAum += 2;
			}
			
			
			num = 0;
		}

		if(Global.tiempoCaida % 6 == 0)
		{
			GD.Print("Muchos Objetos");
			int cantidadCaidas = 0;
			do {
				var lluvia = objeto.Instantiate();
				AddChild(lluvia);
				cantidadCaidas++;
				GD.Print("Entra");

			}
			while(cantidadCaidas < 8);
		}
		num += 1;
		Global.tiempoCaida += 1;
	}
	public void AumentarDificultad()
	{
		foreach (Enemigo enemigo in GetTree().GetNodesInGroup("enemigos"))
		{
			AnimatedSprite2D sprite = enemigo.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			enemigo.velocidad += 24;
			//Esta forma cambia de manera global, si pones un 2.0f duplica la velocidad actual
			sprite.SpeedScale  += 0.1f;
			GD.Print("La velocidad es: " + sprite.SpeedScale);
		}
	}
	public void CambiarPantalla(bool activado)
	{
		GD.Print("HAS PULSADO");
		if(activado)
		{
			GetWindow().Mode = Window.ModeEnum.Fullscreen;
		}
		else
		{
			//Modo ventana
			GetWindow().Mode = Window.ModeEnum.Windowed;
		}
	}
}

using Godot;
using System;

public partial class Control : Godot.Control
{
	private bool espacioAnterior = false;
	private Button menu;
	private Panel panel;
	private Panel fondoMenu;
	private Button cerrar;
	private HScrollBar bBarraSonido; 
	private CheckButton pantallaCompleta; 
	private Label lBarraSonido;
	private Label lFullScreen;
	private Button salir;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		

		Vector2 tPantalla = GetViewportRect().Size;
		panel = GetNode<Panel>("VentanaMenu");
		fondoMenu = GetNode<Panel>("Oscurecer");
		bBarraSonido = GetNode<HScrollBar>("VentanaMenu/BBarraSonido");

		pantallaCompleta = GetNode<CheckButton>("VentanaMenu/BFullScreen");
		lBarraSonido = GetNode<Label>("VentanaMenu/LBarraSonido");
		lFullScreen = GetNode<Label>("VentanaMenu/LFullScreen");
		bBarraSonido.ValueChanged += CambiarVolumen;

		panel.Position = new Vector2(tPantalla.X/2 -panel.Size.X/2, tPantalla.Y/2 -panel.Size.Y/2);
		Label vPosicion = GetNode<Label>("Vidas");
		menu = GetNode<Button>("MenuButton");
		cerrar = GetNode<Button>("VentanaMenu/Cerrar");
		fondoMenu.Size = tPantalla;
		salir = GetNode<Button>("VentanaMenu/SalirJuego");
		
		//GD.Print(vPosicion,Size.x);
		//GD.Print("El tamaño de la ventana: " + tPantalla.X);
		// 		//vPosicion.Position = new Vector2();
		// GD.Print(GetPath());
		// GD.Print(GetNodeOrNull<Label>("TextoTiempo"));

		vPosicion.Position = new Vector2(tPantalla.X - vPosicion.Size.X -20, vPosicion.Position.Y);
		lBarraSonido.Position = new Vector2( panel.Size.X / 2 - lBarraSonido.Size.X / 2, lBarraSonido.Position.Y );
		bBarraSonido.Position = new Vector2( panel.Size.X / 2 - bBarraSonido.Size.X / 2, lBarraSonido.Position.Y + lBarraSonido.Size.Y);
		lFullScreen.Position = new Vector2( panel.Size.X / 2 - lFullScreen.Size.X / 2, bBarraSonido.Position.Y + bBarraSonido.Size.Y*2);		
		pantallaCompleta.Position = new Vector2( panel.Size.X / 2 - pantallaCompleta.Size.X / 2, lFullScreen.Position.Y + lFullScreen.Size.Y);

		salir.Position = new Vector2( panel.Size.X / 2 - salir.Size.X / 2, salir.Position.Y);

		menu.Pressed += AbrirMenu;
		cerrar.Pressed += CerrarMenu;
		salir.Pressed += SalirJuego;
		
	}


	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventScreenTouch touch && touch.Pressed) {
			// GD.Print(touch.Position);
			VirtualJoystick joystick = GetNode<VirtualJoystick>("VirtualJoystick");
			joystick.Position = touch.Position;
			// GD.Print("touch: ", touch.Position.X);
			// GD.Print("touch: ", touch.Position.Y);
			// GD.Print("JOYSTICK: ", joystick.Position);
			// GD.Print("Posicion x: ", joystick.Position.X);
			// GD.Print("Posicion y: ", joystick.Position.Y);

			joystick.Position = new Vector2(
				touch.Position.X - (joystick.Size.X /2), 
				touch.Position.Y - (joystick.Size.Y /2)
			);
			//joystick.Position = touch.Position;

		}
			
	}

	public void ActualizarPuntos(int valor) {
		Label texto = GetNode<Label>("TextoPuntos");
		texto.Text = "Puntos:" + valor;
	}

	public void ActualizarVidas(int vidas) {
		if(vidas > 0) {
			Label texto = GetNode<Label>("Vidas");
			texto.Text = "Vidas:" + vidas;
		}
		else {
			
			GetTree().ChangeSceneToFile("res://Salida.tscn");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		bool espacioAhora = Input.IsKeyPressed(Key.Space);
		
		// if(Input.IsKeyPressed(Key.S)) 
		// {
		// 	GD.Print("HA PULSADO In Visible");
		// 	GetTree().Paused = false;
		// 	panel.Visible = false;
		// 	fondoMenu.Visible = false;

		// }

		// espacioAnterior = espacioAhora;
	}

	public void AbrirMenu() 
	{
			// GD.Print("HA PULSADO abrir");
			GetTree().Paused = true;
			panel.Visible = true;
			fondoMenu.Visible = true;

	}
	
	public void CerrarMenu() 
	{
			// GD.Print("HA PULSADO cerrar");

			// GD.Print("Valor barra sonido: " + bBarraSonido.Value);
			GetTree().Paused = false;
			panel.Visible = false;
			fondoMenu.Visible = false;

	}

	public void SalirJuego()
	{
		GetTree().Quit();
	}

	//Tiene un double que es el valor del 
	public void CambiarVolumen(double valor)
	{
		// GD.Print("Valor barra sonido directamente: " + bBarraSonido.Value);
		// GD.Print("Valor barra sonido con valor: " + valor);
		int indiceSfx = AudioServer.GetBusIndex("Master");

		float volumenDb = Mathf.LinearToDb((float)valor / 100f);

		AudioServer.SetBusVolumeDb(indiceSfx, volumenDb);	
	}

}


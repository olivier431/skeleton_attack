using Godot;
using System;

public class Pause : CanvasLayer
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	TextureRect black;
	Button button;
	Button button2;
	Button button3;

	AudioStreamPlayer Music;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		black = GetNode<TextureRect>("TextureRect");
		button = GetNode<Button>("VBoxContainer/Button");
		button2 = GetNode<Button>("VBoxContainer/Button2");
		button3 = GetNode<Button>("VBoxContainer/Button3");
		Music = GetNode<AudioStreamPlayer>("World1/MusicWorld1");
	}

	private void _on_Button_pressed()
	{
		black.Visible = !black.Visible;
		button.Visible = !button.Visible;
		button2.Visible = !button2.Visible;
		button3.Visible = !button3.Visible;
		//Music.Playing = !Music.Playing;
		GetTree().Paused = !GetTree().Paused;
	}
	
	private void _on_Button2_pressed()
	{
		GetTree().Paused = !GetTree().Paused;
		GetTree().ChangeScene("res://Menu.tscn");
		GlobalVariable.score = 0;
	}
	
	private void _on_Button3_pressed()
	{
		GetTree().Quit();
	}
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	
  }

	public override void _Input(InputEvent inputEvent)
	{
		if (Input.IsActionPressed("Pause"))
		{
			black.Visible = !black.Visible;
			button.Visible = !button.Visible;
			button2.Visible = !button2.Visible;
			button3.Visible = !button3.Visible;
			GetTree().Paused = !GetTree().Paused;
		}
	}
}













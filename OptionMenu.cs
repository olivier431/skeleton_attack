using Godot;
using System;

public class OptionMenu : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	AudioStreamPlayer music1;
	AudioStreamPlayer music2;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		music1 = GetNode<AudioStreamPlayer>("res://World1/MusicWorld1");
		music2 = GetNode<AudioStreamPlayer>("res://World2/MusicWorld2");
		
	}
	
	private void _on_BackToMainMenu_pressed()
	{
		GetTree().ChangeScene("res://Menu.tscn");
	}
	
	private void _on_CheckBox_pressed()
	{
		music1.Playing = false;
		music1.Playing = false;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}







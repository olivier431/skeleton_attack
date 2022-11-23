using Godot;
using System;

public class Music : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	static AudioStreamPlayer music;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	}
	public static void stopMusic(){
		
		music.Playing = !music.Playing;
	
	}
	
	public static bool getMusicStatus(){
		
		return music.Playing;
	
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
public override void _Process(float delta)
	{
	  if (Input.IsActionJustPressed("MusicPause") && ((GetTree().CurrentScene.Name != "Menu" && GetTree().CurrentScene.Name != "OptionMenu")))
		{		
				GD.Print(GetTree().CurrentScene.Name);
				stopMusic();
		}
		
	}
}



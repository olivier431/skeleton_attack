using Godot;
using System;

public class Player : KinematicBody2D
{
	Vector2 UP = new Vector2(0, -1);
	const int GRAVITY = 20;
	const int MAXFALLSPEED = 200;
	const int MAXSPEED = 100;
	const int JUMPFORCE = 335;
	
	const int ACCEL = 10;
	Vector2 vZero = new Vector2();

	bool facing_right = true;
	Vector2 motion = new Vector2();
	
	 AnimatedSprite _animatedSprite;
	Camera2D _cam;
	
	public override void _Ready()
	{
		 _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		_cam = GetNode<Camera2D>("Camera2D");
		GD.Print(_cam);
		_cam.Zoom = new Vector2(0.3f, 0.3f);
	}

 public override void _PhysicsProcess(float delta)
	{
		motion.y += GRAVITY;

		if(motion.y > MAXFALLSPEED) {
			motion.y = MAXFALLSPEED;
		}

		if (facing_right) {
			_animatedSprite.FlipH = false;
		} else {
			_animatedSprite.FlipH = true;
		}

		 motion.x = motion.Clamped(MAXSPEED).x;

		if (Input.IsActionPressed("ui_left")) {
			motion.x -= ACCEL;
			facing_right = false;
			_animatedSprite.Play("run");
		} else if (Input.IsActionPressed("ui_right")) {
			motion.x += ACCEL;
			facing_right = true;
			_animatedSprite.Play("run");
		} else {
			motion = motion.LinearInterpolate(Vector2.Zero, 0.2f);
			_animatedSprite.Play("idle");
		}

		if (IsOnFloor())
			// On ne regarde qu'un seul fois et non le maintient de la touche
			if (Input.IsActionJustPressed("ui_jump")) {
				motion.y = -JUMPFORCE;
				GD.Print($"motion.y = {motion.y}");
				Console.WriteLine($"motion.y = {motion.y}");
			}

		if (!IsOnFloor()) {
			if (motion.y < 0) {
				_animatedSprite.Play("jump");
			} else if (motion.y > 0) {
				_animatedSprite.Play("fall");
			}
		}

		motion = MoveAndSlide(motion, UP);
		
		if (Input.IsActionPressed("Attack1")) {
			_animatedSprite.Play("attack1");
		}
		
		if (Input.IsActionPressed("Attack2")) {
			_animatedSprite.Play("attack2");
		}
	}
}

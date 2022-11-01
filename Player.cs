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
	
	int life = 3;

	bool facing_right = true;
	Vector2 motion = new Vector2();
	bool IsAttacking = false; 
	AnimatedSprite _animatedSprite;
	Camera2D _cam;
	Area2D _Hurtbox;
	
	bool hurt = false;
	
	bool death = false;
	
	public override void _Ready()
	{
		 _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		_cam = GetNode<Camera2D>("Camera2D");
		GD.Print(_cam);
		_cam.Zoom = new Vector2(0.15f, 0.15f);
	}
	private void _on_AnimatedSprite_animation_finished()
	{
		IsAttacking = false;
		hurt = false;
	}
	
	private void _on_HurtBox_area_entered(Area2D area)
	{
		if(area.IsInGroup("enemy_hitbox")){
			hurt = true;
		}
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

		motion.x = Mathf.Lerp(motion.x, MAXSPEED * motion.x > 0 ? 1 : -1, (ACCEL * 1f) / MAXSPEED);

		if (Input.IsActionPressed("ui_left") && IsAttacking == false) {
			motion.x -= ACCEL;
			facing_right = false;
			_animatedSprite.Play("run");
		} else if (Input.IsActionPressed("ui_right") && IsAttacking == false) {
			motion.x += ACCEL;
			facing_right = true;
			_animatedSprite.Play("run");
		} else {
			motion = motion.LinearInterpolate(Vector2.Zero, 0.2f);
			
			if(IsAttacking == false && hurt == false){
				_animatedSprite.Play("idle");
			}	
		}

		if (IsOnFloor())
			// On ne regarde qu'un seul fois et non le maintient de la touche
			if (Input.IsActionJustPressed("ui_jump")) {
				motion.y = -JUMPFORCE;
				GD.Print($"motion.y = {motion.y}");
				Console.WriteLine($"motion.y = {motion.y}");
			}

		if (!IsOnFloor()) {
			if (motion.y < 0  && IsAttacking == false) {
				_animatedSprite.Play("jump");
			} else if (motion.y > 0  && IsAttacking == false) {
				_animatedSprite.Play("fall");
			}
		}

		motion = MoveAndSlide(motion, UP);
		
		if (Input.IsActionPressed("Attack1")) {
			_animatedSprite.Play("attack1");
			IsAttacking = true;
		}
		
		if (Input.IsActionPressed("Attack2")) {
			_animatedSprite.Play("attack2");
			IsAttacking = true;
		}
		
		if(hurt){
		_animatedSprite.Play("hit");
		hurt = true;
		life--;
		GD.Print(life);
		}
		
		if(life == 0){
			GD.Print(life);
			_animatedSprite.Play("death");
			death = true;
		}
	
	
	}
}






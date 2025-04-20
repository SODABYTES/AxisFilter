/*
Axis Filter for Godot Mono - ©2025 Daniel Sosa

This C# class can be used to easily clamp and apply deadzones to raw input from
controller axes.

ApplyCircleDeadzone:
	Set a minimum and maximum radius for a stick.
	The stick's angle is unaffected.

ApplyCrossDeadzone:
	Applies deadzones to the X and Y axes individually.
	Snaps to cardinal directions.

ApplySquareDeadzone:
	Same as cross deadzone, except the circular clamp is resized to be a square
	that reaches corners.

ApplyTriggerDeadzone:
	Set a linear deadzone for a trigger.
*/

using Godot;
using System;

[GlobalClass]
public partial class AxisFilter : Godot.GodotObject
{	
	const double defaultDeadzone = 0.1;
	const double defaultMaximum = 0.9;
	
	public static Vector2 ApplyCircleDeadzone(Vector2 rawInput, double deadzone = defaultDeadzone, double maximum = defaultMaximum){
		float multiplier = 1 / (float)maximum;
		float deadzonef = (float)deadzone;
		float newLength = Mathf.Clamp((Mathf.Max(rawInput.Length() - deadzonef, 0) / (1 - deadzonef)) * multiplier, 0, 1);
		return rawInput.Normalized() * newLength;
	}
	
	public static Vector2 ApplyCrossDeadzone(Vector2 rawInput, double deadzone = defaultDeadzone, double maximum = defaultMaximum){
		float multiplier = 1 / (float)maximum;
		float deadzonef = (float)deadzone;
		
		Vector2 deadzoned = new Vector2(0,0);
		deadzoned.X = Mathf.Clamp((Mathf.Max(Math.Abs(rawInput.X) - deadzonef, 0) / (1 - deadzonef)) * multiplier, -1, 1);
		deadzoned.Y = Mathf.Clamp((Mathf.Max(Math.Abs(rawInput.Y) - deadzonef, 0) / (1 - deadzonef)) * multiplier, -1, 1);
		
		if (rawInput.X < 0){
			deadzoned.X = -deadzoned.X;
		}
		if (rawInput.Y < 0){
			deadzoned.Y = -deadzoned.Y;
		}
		
		return deadzoned.Normalized() * Mathf.Min(deadzoned.Length(), 1);
	}
	
	public static Vector2 ApplySquareDeadzone(Vector2 rawInput, double deadzone = defaultDeadzone, double maximum = defaultMaximum){
		Vector2 crossOutput = ApplyCrossDeadzone(rawInput, deadzone, maximum);
		return _ConvertCrossToSquare(crossOutput);
	}
	
	public static float ApplyTriggerDeadzone(float rawInput, double deadzone = defaultDeadzone, double maximum = defaultMaximum){
		float multiplier = 1 / (float)maximum;
		float deadzonef = (float)deadzone;
		return Mathf.Clamp((Mathf.Max(rawInput - deadzonef, 0) / (1 - deadzonef)) * multiplier, 0, 1);
	}
	
	private static Vector2 _ConvertCrossToSquare(Vector2 stick){
		const float magic = 0.707106781186548f; //(float)(Math.Sqrt(2) / 2);
		float lerpFactor = (float)Math.Abs(Math.Cos(stick.Angle() * 2));
		
		Vector2 stickMagic = new Vector2(0,0);
		stickMagic.X = stick.X / magic;
		stickMagic.Y = stick.Y / magic;
		
		Vector2 result = new Vector2(0,0);
		result.X = Mathf.Clamp(Mathf.Lerp(stick.X, stickMagic.X, 1 - lerpFactor), -1, 1);
		result.Y = Mathf.Clamp(Mathf.Lerp(stick.Y, stickMagic.Y, 1 - lerpFactor), -1, 1);
		
		return result;
	}
}

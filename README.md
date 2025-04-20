# AxisFilter
It's a simple C# class meant for Godot Engine. It takes raw inputs from a controller's analog sticks/triggers, applies deadzones to them, and clamps them. This is meant to be an alternative to the Input Map's deadzone settings.
It's really nothing special, but it's the kind of thing you only really need to write once and then never have to worry about again, so here it is.

### How do I use it?
The file you need is in "data/script/class/AxisFilter.cs". Just drop that bad boy wherever it needs to be in your own Godot project, then build your project. Afterwards, it should be accessible from both GDScript and C# as a global class.

### Is there a GDScript version?
No, sorry. Maybe I'll make one later.

## AxisFilter Demo requirements
You need the .NET version of Godot 4.4.1... and a controller.

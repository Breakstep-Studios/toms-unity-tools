using System;

[Flags]
public enum DirectionType {
	// Decimal     //Binary
	None = 0, // 000000
	Up = 1, // 000001
	Left = 2, // 000010
	Down = 4, // 000100
	Right = 8 // 001000
}
namespace UnifyNuget;

public static class Splash
{
    public static void Print()
    {
        var splash = @"
__/\\\________/\\\____________________________/\\\\\_____________________
__\/\\\_______\/\\\__________________________/\\\///_____________________
___\/\\\_______\/\\\________________/\\\_____/\\\_______/\\\__/\\\_______
____\/\\\_______\/\\\__/\\/\\\\\\___\///___/\\\\\\\\\___\//\\\/\\\_______
_____\/\\\_______\/\\\_\/\\\////\\\___/\\\_\////\\\//_____\//\\\\\_______
______\/\\\_______\/\\\_\/\\\__\//\\\_\/\\\____\/\\\________\//\\\_______
_______\//\\\______/\\\__\/\\\___\/\\\_\/\\\____\/\\\_____/\\_/\\\_______
_________\///\\\\\\\\\/___\/\\\___\/\\\_\/\\\____\/\\\____\//\\\\/________
____________\/////////_____\///____\///__\///_____\///______\////________";

        var color = Console.ForegroundColor;

        foreach (var c in splash)
        {
            if (c == '_')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(c);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(c);
            }
        }

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("by: daThorstholm");
        Console.ForegroundColor = color;
    }
}
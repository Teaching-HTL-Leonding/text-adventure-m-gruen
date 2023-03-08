// Text Adventure
//using System.Diagnostics;

#region Constants
const string INTRO_SCENE_OPTIONS = "north/east/south/west";
const string SHOW_SHADOW_FIGURE_OPTIONS = "north/east/south";
const string CAMERA_SCENE_OPTIONS = "north/south";
const string HAUNTED_ROOM_OPTIONS = "north/east/west";
const string SHOW_SKELETONS_OPTIONS = "north/east/west";
const string STRANGE_CREATURE_OPTIONS = "west/south";

const string INTRO_SCENE = "IntroScene";
const string SHOW_SHADOW_FIGURE = "ShowShadowFigure";
const string CAMERA_SCENE = "CameraScene";
const string HAUNTED_ROOM = "HauntedRoom";
const string SHOW_SKELETONS = "ShowSkeletons";
const string STRANGE_CREATURE = "StrangeCreature";
#endregion

/* DateTime start = DateTime.Now;
TimeSpan elapsed = start - DateTime.Now;

if (elapsed > TimeSpan.FromSeconds(30))
{

}

Stopwatch watch = Stopwatch.StartNew();

if (watch.Elapsed > TimeSpan.FromSeconds(30))
{

} */

#region Main Program
{
    string solution = @"
       EXIT
         |
   CameraScene()         Wall           Wall with weapon
         |                |                  |
ShowShadowFigure() -- IntroScene() -- ShowSkeletons()  -- StrangeCreature()
         |                |                                    |
       Wall               |                                  EXIT
         DEAD   --  HauntedRoom() --  EXIT
";

    System.Console.Write("If you want to see the solution enter 'S':  ");
    if (Console.ReadLine()!.ToLower() == "s")
    {
        System.Console.WriteLine(solution);
    }

    PrintWelcomeMessage();
    GetName();

    bool dead = false, foundExit = false, knife = false, creatureKilled = false;

    string nextRoom = IntroScene();
    string oldRoom = INTRO_SCENE;
    do
    {
        switch (nextRoom)
        {
            case INTRO_SCENE:
                oldRoom = nextRoom;
                nextRoom = IntroScene();
                break;
            case SHOW_SHADOW_FIGURE:
                oldRoom = nextRoom;
                nextRoom = ShowShadowFigure();
                break;
            case CAMERA_SCENE:
                oldRoom = nextRoom;
                nextRoom = CameraScene();
                break;
            case HAUNTED_ROOM:
                oldRoom = nextRoom;
                nextRoom = HauntedRoom();
                break;
            case SHOW_SKELETONS:
                oldRoom = nextRoom;
                nextRoom = ShowSkeletons();
                break;
            case STRANGE_CREATURE:
                oldRoom = nextRoom;
                nextRoom = StrangeCreature(ref creatureKilled, knife);
                break;
            case "Wall":
                nextRoom = oldRoom;
                Wall();
                break;
            case "WallKnife":
                nextRoom = oldRoom;
                knife = true;
                WallKnife();
                break;
            case "Exit":
                ExitRoom();
                return;
            case "Dead":
                DeadRoom();
                dead = true;
                break;
            default:
                System.Console.WriteLine("You are in a room that doesn't exist. You are dead.");
                dead = true;
                break;
        }
    }
    while (!dead && !foundExit);
}
#endregion

#region Methods
string IntroScene()
{
    System.Console.WriteLine("You are at a crossroads, and you can choose to go down any of the four hallways. Where would you like to go?");
    string direction = GetDirection(INTRO_SCENE_OPTIONS);

    switch (direction)
    {
        case "west":
            return SHOW_SHADOW_FIGURE;
        case "south":
            return HAUNTED_ROOM;
        case "east":
            return SHOW_SKELETONS;
        case "north":
            return "Wall";
        default:
            return "";
    }
}

string ShowShadowFigure()
{
    System.Console.WriteLine("You see a dark shadowy figure appear in the distance. You are creeped out. Where would you like to go?");
    string direction = GetDirection(SHOW_SHADOW_FIGURE_OPTIONS);

    switch (direction)
    {
        case "south":
            return "Wall";
        case "east":
            return INTRO_SCENE;
        case "north":
            return CAMERA_SCENE;
        default:
            return "";
    }
}

string CameraScene()
{
    System.Console.WriteLine("You see a camera that has been dropped on the ground. Someone has been here recently. Where would you like to go?");
    string direction = GetDirection(CAMERA_SCENE_OPTIONS);

    switch (direction)
    {
        case "south":
            return SHOW_SHADOW_FIGURE;
        case "north":
            return "Exit";
        default:
            return "";
    }
}

string HauntedRoom()
{
    System.Console.WriteLine("You hear strange voices. You think you have awoken some of the dead. Where would you like to go?");
    string direction = GetDirection(HAUNTED_ROOM_OPTIONS);

    switch (direction)
    {
        case "west":
            return "Dead";
        case "east":
            return "Exit";
        case "south":
            return INTRO_SCENE;
        default:
            return "";
    }
}

string ShowSkeletons()
{
    System.Console.WriteLine("You see a wall of skeletons as you walk into the room. Someone is watching you. Where would you like to go?");
    string direction = GetDirection(SHOW_SKELETONS_OPTIONS);

    switch (direction)
    {
        case "west":
            return INTRO_SCENE;
        case "east":
            return STRANGE_CREATURE;
        case "north":
            return "WallKnife";
        default:
            return "";
    }
}

string StrangeCreature(ref bool creatureKilled, bool knife)
{
    if (creatureKilled)
    {
        System.Console.WriteLine("You see the Goul-like creature that you checkmated earlier. What a relief! Where would you like to go?");
        string direction = GetDirection(STRANGE_CREATURE_OPTIONS);

        switch (direction)
        {
            case "south":
                return "Exit";
            case "west":
                return SHOW_SKELETONS;
            default:
                return "";
        }
    }
    else
    {
        System.Console.WriteLine("A strange Goul-like creature has appeared. You can either run or fight it. What would you like to do?");
        string direction = GetDirection("flee/fight");
        if (direction == "fight" && !knife)
        {
            System.Console.WriteLine("The Goul-like creature checkmated you, because you had no Queen! Then he killed you!");
            Environment.Exit(0);
        }
        if (direction == "fight" && knife)
        {
            System.Console.WriteLine("After a wild chees game against the Goul-like creature, where you sacrificed the Rook and checkmated him with a pawn, you beat him.");
            creatureKilled = true;
            System.Console.WriteLine("Where would you like to go now?");
            direction = GetDirection(STRANGE_CREATURE_OPTIONS);

            switch (direction)
            {
                case "south":
                    return "Exit";
                default:
                    return SHOW_SKELETONS;
            }
        }
        if (direction == "flee")
        {
            return SHOW_SKELETONS;
        }
    }

    return "Lol";
}

void DeadRoom()
{
    System.Console.WriteLine("Multiple Goul-like creatures start emerging as you enter the room. You are killed.");
}

void ExitRoom()
{
    System.Console.WriteLine("You made it! You've found an exit. and exit the game.");
}

void Wall()
{
    System.Console.WriteLine("You find that this door opens into a wall.");
}

void WallKnife()
{
    System.Console.WriteLine("You find that this door opens into a wall. You open some of the drywall to discover the Queen.");
}

string GetDirection(string directions)
{
    bool isValid;
    string input;
    do
    {
        System.Console.WriteLine($"Your Options: {directions} ");
        System.Console.Write("Your choice: ");
        input = System.Console.ReadLine()!.ToLower();
        isValid = directions.Contains(input);
        if (!isValid)
        {
            System.Console.WriteLine("Wrong input...");
        }
    }
    while (!isValid);

    return input;
}

void PrintWelcomeMessage()
{
    System.Console.WriteLine(@"Welcome to the Adventure Game!
==============================
As an avid traveler, you have decided to visit the Catacombs of Paris.
However, during your exploration, you find yourself lost.
You can choose to walk in multiple directions to find a way out.");
}

void GetName()
{
    System.Console.Write("Let's start with your name: ");
    System.Console.WriteLine($"Good luck, {Console.ReadLine()!}");
}
#endregion

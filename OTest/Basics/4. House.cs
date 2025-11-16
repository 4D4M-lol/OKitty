// 4. House
//
// This example shows how to render simple shapes and animated elements
// using OKitty's renderer. It draws a basic house composed of triangles
// and rectangles, and animates clouds sliding across the sky.
//
// Concepts demonstrated:
// - Rendering multiple shapes per frame.
// - Using RenderLine and RenderLines for geometry.
// - Combining static and animated elements in the same scene.
// - Using OShapes helper methods to generate common shapes.
// - Wrapping moving objects when they leave the screen.

using System.Collections.ObjectModel;
using OKitty;
using static OKitty.OkInterface;
using static OKitty.OkMath;
using static OKitty.OkStyling;

namespace OTest.Basics;

public static class House
{
    // Number of clouds and how long each cloud line is.
    
    private const int CLOUD_AMOUNT = 10;
    private const int CLOUD_LENGTH = 250;

    // Cloud speed range (pixels per second).
    
    private const int MIN_SPEED = 60;
    private const int MAX_SPEED = 120;

    private static OWindow window;

    // Shape data for the house.
    
    private static ReadOnlyCollection<(OVector2<float> start, OVector2<float> end)> roof, door, wall;

    // Moving clouds.
    
    private static List<(OVector2<float> position, float speed)> clouds = new List<(OVector2<float> position, float speed)>();
    private static Random random = new Random();

    public static void Start()
    {
        // Configure the window.
        
        OWindowOptions options = new OWindowOptions()
        {
            Name = "House",
            Border = OWindow.OWindowBorder.Fixed,     // No resizing.
            BackgroundColor = OColor.Black,           // Night backdrop.
            PresentAfterCallback = true               // Render inside OnUpdate.
        };

        window = new OWindow(options);

        // Update callback for animation and rendering.
        
        window.OnUpdate += Update;

        // Build house geometry using OShapes.
        
        roof = OShapes.Triangle(new OVector2<float>(400, 150), new OVector2<float>(200, 112.5f), 0, OShapes.OTriangleType.Isosceles);
        door = OShapes.Rectangle(new OVector2<float>(75, 150), new OVector2<float>(220, 312.5f), 0);
        wall = OShapes.Rectangle(new OVector2<float>(400, 200), new OVector2<float>(200, 262.5f), 0);

        // Generate cloud positions and speeds.
        
        for (int i = 0; i < CLOUD_AMOUNT; i++)
        {
            OVector2<float> position = new OVector2<float>(random.NextSingle() * 800, (random.NextSingle() * 90) + 10);
            float speed = MIN_SPEED + (random.NextSingle() * (MAX_SPEED - MIN_SPEED));

            clouds.Add((position, speed));
        }

        // Start the window.
        
        window.Initialize();
        window.Run();
        window.Dispose();
    }

    private static void Update(double deltaTime)
    {
        // Move each cloud horizontally.
        
        List<(OVector2<float> position, float speed)> updated = new List<(OVector2<float> position, float speed)>();

        foreach ((OVector2<float> position, float speed) cloud in clouds)
        {
            float distance = cloud.speed * (float)deltaTime;
            OVector2<float> newPos = new OVector2<float>(cloud.position.X + distance, cloud.position.Y);

            // Wrap cloud when it slides offscreen.
            
            if (newPos.X >= 800 + CLOUD_LENGTH)
                newPos = new OVector2<float>(-CLOUD_LENGTH, cloud.position.Y);

            updated.Add((newPos, cloud.speed));
        }

        clouds = updated;

        // Draw clouds as white horizontal lines.
        
        foreach ((OVector2<float> position, float _) cloud in clouds)
            window.Renderer.RenderLine(cloud.position, new OVector2<float>(cloud.position.X + CLOUD_LENGTH, cloud.position.Y), OColor.White);

        // Draw the ground.
        
        window.Renderer.RenderLine(new OVector2<float>(0, 462.5f), new OVector2<float>(800, 462.5f), OColor.Green);

        // Draw house structures.
        
        window.Renderer.RenderLines(roof, OColor.Brown);
        window.Renderer.RenderLines(door, OColor.Brown);
        window.Renderer.RenderLines(wall, OColor.Red);
    }
}

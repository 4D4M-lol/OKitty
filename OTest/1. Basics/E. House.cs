// E. House
//
// This example renders a simple animated landscape scene:
//
// - A blue sky background
// - A house made from rectangles (wall, door) and a triangle (roof)
// - A ground rectangle
// - Several moving “clouds” represented by white horizontal lines
//
// Each cloud moves from left to right at a random speed,
// and once it exits the screen it respawns on the left again.
//
// Concepts shown in this example:
//
// - Rendering filled shapes using OShapes.Rectangle and OShapes.Triangle
// - Animating objects with deltaTime
// - Using RenderShape() to draw filled geometry
// - Using RenderLine() for lightweight animated effects
// - Combining static and dynamic elements in a scene
//

using OKitty;
using static OKitty.OkInstance;
using static OKitty.OkInterface;
using static OKitty.OkMath;
using static OKitty.OkStyling;

namespace OTest.Basics;

public static class House
{
    // Cloud animation settings

    private const int CLOUD_AMOUNT = 10;
    private const int CLOUD_LENGTH = 250;
    private const int MIN_SPEED = 24;
    private const int MAX_SPEED = 48;

    // Each cloud stores: (position, speed)

    private static List<(OVector2<float> position, float speed)> clouds = new List<(OVector2<float> position, float speed)>();
    private static Random random = new Random();

    // House shapes

    private static OShapeInfo ground, wall, door, roof;

    // Main window

    private static OWindow window;

    public static void Start()
    {
        // Configure a fixed-size window with a sky-blue background

        OWindowOptions options = new OWindowOptions()
        {
            Name = "House",
            Border = OWindow.OWindowBorder.Fixed,
            BackgroundColor = new OColor(OColor.OColors.LightSkyBlue),
            PresentAfterCallback = true // Required when drawing inside OnUpdate
        };

        window = new OWindow(options);

        // Attach per-frame update callback

        window.OnUpdate += Update;

        // Create static scene elements (ground, wall, door, roof)

        ground = OShapes.Rectangle(
            new OVector2<float>(800, 137.5f),
            new OVector2<float>(0, 462.5f),
            0,
            0,
            OColor.Green
        );

        wall = OShapes.Rectangle(
            new OVector2<float>(400, 200),
            new OVector2<float>(200, 262.5f),
            0,
            0,
            OColor.Red
        );

        door = OShapes.Rectangle(
            new OVector2<float>(75, 150),
            new OVector2<float>(220, 312.5f),
            0,
            0,
            OColor.Brown
        );

        roof = OShapes.Triangle(
            new OVector2<float>(400, 150),
            new OVector2<float>(200, 112.5f),
            0,
            0,
            OShapes.OTriangleType.Isosceles,
            OColor.Brown
        );

        // Create cloud positions and speeds

        for (int i = 0; i < CLOUD_AMOUNT; i++)
        {
            OVector2<float> pos = new OVector2<float>(
                random.NextSingle() * 800,
                (random.NextSingle() * 90) + 10 // random height near top
            );

            float speed = MIN_SPEED + (random.NextSingle() * (MAX_SPEED - MIN_SPEED));

            clouds.Add((pos, speed));
        }

        // Start window lifecycle

        window.Initialize();
        window.Run();
        window.Dispose();
    }

    private static void Update(double deltaTime)
    {
        // Move all clouds based on their speed

        List<(OVector2<float> position, float speed)> updated = new List<(OVector2<float> position, float speed)>();

        foreach ((OVector2<float> position, float speed) cloud in clouds)
        {
            float distance = cloud.speed * (float)deltaTime;

            // Move cloud horizontally

            OVector2<float> newPos = new OVector2<float>(
                cloud.position.X + distance,
                cloud.position.Y
            );

            // Respawn cloud if it exits the screen

            if (newPos.X >= 800 + CLOUD_LENGTH)
                newPos = new OVector2<float>(
                    -CLOUD_LENGTH,
                    (random.NextSingle() * 90) + 10
                );

            updated.Add((newPos, cloud.speed));
        }

        clouds = updated;

        // Render clouds as white horizontal lines

        foreach ((OVector2<float> position, float _) cloud in clouds)
            window.Renderer.RenderLine(
                cloud.position,
                new OVector2<float>(cloud.position.X + CLOUD_LENGTH, cloud.position.Y),
                OColor.White
            );

        // Render the filled shapes that form the house and ground
        
        window.Renderer.RenderShape(ground);
        window.Renderer.RenderShape(wall);
        window.Renderer.RenderShape(door);
        window.Renderer.RenderShape(roof);
    }
}

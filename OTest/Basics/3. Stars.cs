// 3. Stars
//
// This example demonstrates basic 2D particle motion using OKitty.
// A field of white stars moves diagonally across the screen to create
// a simple starfield effect.
//
// Concepts shown in this example:
// - Using deltaTime to create frame rate independent movement.
// - Updating many objects each frame.
// - Drawing pixels using the renderer.
// - Repositioning objects when they leave the screen.
// - Using PresentAfterCallback to allow rendering inside OnUpdate.

using OKitty;
using static OKitty.OkMath;
using static OKitty.OkStyling;

namespace OTest.Basics;

public static class Stars
{
    // Total number of stars to display.
    
    private const int STAR_AMOUNT = 500;
    
    // Minimum and maximum travel speed for each star.
    
    private const float MIN_SPEED = 60;
    private const float MAX_SPEED = 120;

    private static OWindow window;
    private static Random random = new Random();

    // Each star stores its position and its individual movement speed.
    
    private static List<(OVector2<float> position, float speed)> stars = new List<(OVector2<float> position, float speed)>();

    public static void Start()
    {
        // Configure the window.
        
        OWindowOptions options = new OWindowOptions()
        {
            Name = "Stars",
            Border = OWindow.OWindowBorder.Fixed,       // Prevent resizing.
            BackgroundColor = OColor.Black,             // Dark background.
            PresentAfterCallback = true                 // Required to draw correctly inside OnUpdate.
        };

        window = new OWindow(options);

        // Subscribe to the update event.
        // This is where movement and drawing occur.
        
        window.OnUpdate += Update;

        // Generate random stars at different positions and speeds.
        
        for (int i = 0; i < STAR_AMOUNT; i++)
        {
            OVector2<float> position = new OVector2<float>(random.NextSingle() * 800, random.NextSingle() * 600);
            float speed = MIN_SPEED + (random.NextSingle() * (MAX_SPEED - MIN_SPEED));

            stars.Add((position, speed));
        }

        // Start the window loop.
        
        window.Initialize();
        window.Run();
        window.Dispose();
    }

    private static void Update(double deltaTime)
    {
        // Create a new list to store updated star positions.
        
        List<(OVector2<float> position, float speed)> updated = new List<(OVector2<float> position, float speed)>();

        foreach ((OVector2<float> position, float speed) star in stars)
        {
            // Move the star diagonally based on its speed and the time since last frame.
            
            float distance = star.speed * (float)deltaTime;
            OVector2<float> newPosition = new OVector2<float>(star.position.X + distance, star.position.Y + distance);

            // If the star exits the screen, wrap it back to a random location
            // along either the top edge or the left edge.
            
            if (newPosition.X >= 800 || newPosition.Y >= 600)
            {
                if (random.Next(0, 2) == 0)
                    newPosition = new OVector2<float>(random.NextSingle() * 800, 0);
                else
                    newPosition = new OVector2<float>(0, random.NextSingle() * 600);
            }

            updated.Add((newPosition, star.speed));
        }

        stars = updated;

        // Draw every star as a white point.
        
        foreach ((OVector2<float> position, float _) star in stars)
            window.Renderer.RenderPoint(star.position, OColor.White);
    }
}

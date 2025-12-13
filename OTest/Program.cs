namespace OTest;

class Program
{
    static void Main(string[] args)
    {
        // If no argument is provided, run the default example (Window Demo).
        
        if (args.Length != 1)
        {
            Basics.Window.Start();
        }
        else
        {
            // Convert the argument to lowercase for consistent comparison.
            
            switch (args[0].ToLower())
            {
                case "window":
                    // Run the basic window lifecycle example.
                    
                    Basics.Window.Start();
                    
                    break;
                case "rainbow":
                    // Run the animated rainbow background example.
                    
                    Basics.Rainbow.Start();
                    
                    break;
                case "stars":
                    // Run the animated stars example.
                
                    Basics.Stars.Start();
                    
                    break;
                case "illusion":
                    // Run the illusion example.
                    
                    Basics.Illusion.Start();
                    
                    break;
                case "house":
                    // Run the house drawing example.

                    Basics.House.Start();
                    
                    break;
                case "frame":
                    // Run the frame GUI example.
                    
                    Interfaces.Frame.Start();
                    
                    break;
                default:
                    // Unknown argument, fall back to default example.
                    
                    Interfaces.Test.Start();
                    
                    break;
            }
        }
    }
}
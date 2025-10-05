using System.Collections.ObjectModel;
using OKitty;
using static OKitty.OkInput;
using static OKitty.OkInstance;
using static OKitty.OkMath;
using static OKitty.OkScript;
using static OKitty.OkSecurity;
using static OKitty.OkStyling;

namespace OTest;

class Program
{
    static void Main(string[] args)
    {
        OWindowOptions options = new OWindowOptions()
        {
            Name = "OKitty"
        };
        OWindow window = new OWindow(options);
        ReadOnlyCollection<OColorSequence> sequences = new ReadOnlyCollection<OColorSequence>(new List<OColorSequence>()
        {
            OColorSequence.Cool, OColorSequence.Hot, OColorSequence.Grayscale, OColorSequence.Nebula, OColorSequence.Spring, OColorSequence.Summer, OColorSequence.Autumn, OColorSequence.Winter,
            OColorSequence.Sky, OColorSequence.Rainbow, OColorSequence.Fire, OColorSequence.Water, OColorSequence.Plasma
        });

        foreach (OColorSequence sequence in sequences)
        {
            for (int i = 0; i < 100; i++)
            {
                OColor color = sequence.GetValue((i + 1) / 100f);

                Console.Write($"\x1b[48;2;{color.Argb.Red};{color.Argb.Green};{color.Argb.Blue}m");
                Console.Write($"\x1b[38;2;{color.Argb.Red};{color.Argb.Green};{color.Argb.Blue}m.");
            }
            
            Console.WriteLine("\x1b[0m");
        }
        
        window.Initialize();
        window.Run();
        window.Dispose();
    }
}
namespace Automation
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Console - TgimbaService.cs - ProcessUser(args) - Process is starting!");

            System.Console.WriteLine("Console - TgimbaService.cs - ProcessUser(args) - Process is running!");
            System.Threading.Thread.Sleep(5000); // simulate some application call

            System.Console.WriteLine("Console - TgimbaService.cs - ProcessUser(args) - Process is stopped!");
        }
    }
}

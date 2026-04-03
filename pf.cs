using System;
using System.IO;

class PathFind
{
    static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: pf <filename>");
            Console.WriteLine("  Searches the PATH environment variable for the given filename.");
            Console.WriteLine("  Wildcards * and ? are supported.");
            return 1;
        }

        string searchPattern = args[0];
        string pathEnv = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
        string[] directories = pathEnv.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries);

        bool found = false;
        foreach (string dir in directories)
        {
            if (!Directory.Exists(dir))
                continue;

            string[] matches;
            try
            {
                matches = Directory.GetFiles(dir, searchPattern);
            }
            catch (UnauthorizedAccessException)
            {
                continue;
            }
            catch (IOException)
            {
                continue;
            }

            foreach (string match in matches)
            {
                Console.WriteLine(match);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine($"'{searchPattern}' not found in PATH.");
        }

        return found ? 0 : 1;
    }
}

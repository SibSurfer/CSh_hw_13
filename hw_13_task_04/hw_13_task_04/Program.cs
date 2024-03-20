class Program
{
    static String FindFile(String filename)
    {
        var filesQueue = new Queue<DirectoryInfo>();
        filesQueue.Enqueue(new DirectoryInfo(@"c:\Temp\"));
        while (filesQueue.Count > 0)
        {
            var directory = filesQueue.Dequeue();
            try
            {
                foreach (var file in directory.GetFiles())
                {
                    if (file.Name.Contains(filename))
                    {
                        return file.FullName;
                    }
                }
                foreach (var dir in directory.GetDirectories())
                {
                    filesQueue.Enqueue(dir);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        throw new Exception("impossible to find this file");
    }

    static void Main(String[] args)
    {
        Console.WriteLine(FindFile("myfile.txt"));
    }
}
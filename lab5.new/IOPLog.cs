static class IOPLog { // Input-Output-Process Logger
    static TextWriter tw;

    public static void init(bool appendFile) {
        tw = new StreamWriter("log.lg", appendFile);
        tw.WriteLine($"APPFILE?::[{appendFile}]");
    }

    public static string? read() {
        string temp = Console.ReadLine();
        tw.WriteLine($"READ_INPUT[{temp}]");
        return temp;
    }

    public static void write(string mes, bool consoleWrite = true) {
        tw.WriteLine($"WRITE_OUTPUT[{mes}]");
        if(consoleWrite)
            Console.WriteLine(mes);
    }
}
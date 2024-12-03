using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

internal class Program {
    static IWorkbook book;
    IEnumerable<int> test =
        (IEnumerable<int>)(from t in book.GetSheetAt(0)
        where Convert.ToInt32($"{t.GetCell(0)}") > 70
        select t);
    private static void Main(string[] args) {
        using (FileStream fs = new FileStream("LR5-var11.xlsx", FileMode.Open, FileAccess.Read))
            book = new XSSFWorkbook(fs);
        int taskNum;
        do {
            taskNum = securedInt("Выберите номер задания(1-7). 0 - завершить программу: ", 0, 7);

            switch(taskNum) {
                case 0 : break; // Прекращаем выполнение сразу же.
                case 1 : task1(); break;
                case 2 : task2(); break;
                default : Console.WriteLine("Недопустимое значение."); break;
            }
        } while(taskNum != 0);
    }

    private static void task1() {
        int listNum;
        do {
            Console.WriteLine($"=== TASK 1 ===");
            listNum = securedInt("Выберите лист(1-3), 0 - выход: ", 0, 3);
            if(listNum > 0)
                subtask1(listNum);
        } while(listNum != 0);
    }

    private static void subtask1(int list) {
        int rowNum;
        do {
            Console.WriteLine($"=== {book.GetSheetAt(list-1).SheetName} ===");
            rowNum = securedInt($"Выберите строку(1-{book.GetSheetAt(list-1).Count()}), 0 - выход, -1 - все: ", -1, book.GetSheetAt(list-1).Count());
            if(rowNum > 0)
                Console.WriteLine(stringRow(book.GetSheetAt(list-1).GetRow(rowNum-1)));
            if(rowNum == -1)
                foreach(IRow row in book.GetSheetAt(list-1))
                    Console.WriteLine(stringRow(row));
            Console.WriteLine();
        } while(rowNum != 0);
    }

    private static void task2() {

    }

    private static int securedInt(string mes = "Введите число: ", int min = -20000000, int max = 2000000) {
        int res;

        do {
            Console.WriteLine(mes);
        } while(!(int.TryParse(Console.ReadLine(), out res)) ||
            !(res >= min && res<=max));

        return res;
    } 
}

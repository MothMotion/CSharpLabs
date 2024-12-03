using System.ComponentModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

internal class Program {
    static IWorkbook book;
    static Database db = new();
    private static void Main(string[] args) {
        IOPLog.init(securedBool("Записывать в старый файл? (true/false)", false));
        try {
            using (FileStream fs = new FileStream("LR5-var11.xlsx", FileMode.Open, FileAccess.Read))
                book = new XSSFWorkbook(fs);
        } catch {
            IOPLog.write("Невозможно открыть файл(LR5-var11.xlsx).");
            return;
        }
        int taskNum;
        do {
            taskNum = securedInt("Выберите номер задания(1-6). 0 - завершить программу: ", 0, 6);

            switch(taskNum) {
                case 0 : break; // Прекращаем выполнение сразу же.
                case 1 : {if(!db.Loaded) db.loadDatabase(book); else Console.WriteLine("База данных уже загружена!");}; break;
                case 2 : task2(); break;
                case 3 : task3(); break;
                case 4 : task4(); break;
                case 5 : task5(); break;
                case 6 : task6(); break;
                default : Console.WriteLine("Недопустимое значение."); break;
            }
        } while(taskNum != 0);
    }

    private static void task2() {
        int listNum;
        do {
            Console.WriteLine($"=== TASK 2 ===\nПосмотреть элемент");
            listNum = securedInt("Выберите лист(1-3), 0 - выход: ", 0, 3);
            if(listNum > 0)
                subtask2(listNum);
        } while(listNum != 0);
    }

    private static void subtask2(int list) {
        int rowNum;
        do {
            Console.WriteLine($"=== {db.GetTableName(list-1)} ===");
            rowNum = securedInt($"Выберите строку(1-{db.GetTable(list-1).Count}), 0 - выход, -1 - все: ", -1, db.GetTable(list-1).Count);
            if(rowNum == -1)
                foreach(var row in db.GetTable(list-1))
                    IOPLog.write(row.ToString());
            if(rowNum > 0)
                IOPLog.write(db.GetTable(list-1)[rowNum-1].ToString());
            
        } while(rowNum != 0);
    }

    private static void task3() {
        int listNum;
        do {
            Console.WriteLine($"=== TASK 3 ===\nУдалить элемент");
            listNum = securedInt("Выберите лист(1-3), 0 - выход: ", 0, 3);
            if(listNum > 0)
                subtask3(listNum);
        } while(listNum != 0);
    }

    private static void subtask3(int list) {
        int id;
        do {
            Console.WriteLine($"=== {db.GetTableName(list-1)} ===");
            id = securedInt($"Выберите идентификатор, 0 - выход: ", 0);
            if(id > 0)
                db.removeData(list-1, id);
            
        } while(id != 0);
    }

    private static void task4() {
        int listNum;
        do {
            Console.WriteLine($"=== TASK 4 ===\nРедактировать элемент");
            listNum = securedInt("Выберите лист(1-3), 0 - выход: ", 0, 3);
            if(listNum > 0)
                subtask4(listNum);
        } while(listNum != 0);
    }

    private static void subtask4(int list) {
        int id;
        do {
            Console.WriteLine($"=== {db.GetTableName(list-1)} ===");
            id = securedInt($"Выберите идентификатор, 0 - выход: ", 0);
            if(id > 0)
                db.updateData(list-1, id);
            
        } while(id != 0);
    }

    private static void task5() {
        int listNum;
        do {
            Console.WriteLine($"=== TASK 5 ===\nДобавить элемент");
            listNum = securedInt("Выберите лист(1-3), 0 - выход: ", 0, 3);
            if(listNum > 0)
                subtask5(listNum);
        } while(listNum != 0);
    }

    private static void subtask5(int list) {
        int id;
        do {
            Console.WriteLine($"=== {db.GetTableName(list-1)} ===");
            id = securedInt($"Выберите идентификатор, 0 - выход: ", 0);
            if(id > 0)
                db.addData(list-1, id);
            
        } while(id != 0);
    }

    private static void task6() {
        int taskNum;
        do {
            Console.WriteLine($"=== TASK 6 ===\nВыполнить запрос");
            taskNum = securedInt("Выберите запрос(1-4), 0 - выход: ", 0, 4);
            switch(taskNum) {
                case 1 : db.task1(); break;
                case 2 : db.task2(); break;
                case 3 : db.task3(); break;
                case 4 : db.task4(); break;

            }
        } while(taskNum != 0);
    }

    private static int securedInt(string mes = "Введите число: ", int min = -20000000, int max = 2000000) {
        int res;
        IOPLog.write($"SI[fr:{min},to:{max}]", false);

        do {
            Console.WriteLine(mes);
        } while(!(int.TryParse(IOPLog.read(), out res)) ||
            !(res >= min && res<=max));

        return res;
    }
    private static bool securedBool(string mes = "Введите число: ", bool writeLog = true) {
        bool res = false;

        do {
            Console.WriteLine(mes);
        } while(writeLog && !bool.TryParse(IOPLog.read(), out res) || !writeLog && !bool.TryParse(Console.ReadLine(), out res));

        return res;
    }
}
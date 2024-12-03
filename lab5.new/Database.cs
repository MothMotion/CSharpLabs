using System.Collections;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;

class Database {
    private List<Artists> artists = [];
    private List<Styles> styles = [];
    private List<Pictures> pictures = [];

    private bool loaded = false;
    
    public List<Artists> Artists {get => artists; private set => artists = value;}
    public List<Styles> Styles {get => styles; private set => styles = value;}
    public List<Pictures> Pictures {get => pictures; private set => pictures = value;}
    public bool Loaded {get => loaded; private set => loaded = value;}

    public List<ITable> GetTable(int index) {
        switch(index) {
            case 0 : return artists.Cast<ITable>().ToList();
            case 1 : return styles.Cast<ITable>().ToList();
            case 2 : return pictures.Cast<ITable>().ToList();
            default : return null;
        }
    }

    public string GetTableName(int index) {
        switch(index) {
            case 0 : return "Artists";
            case 1 : return "Styles";
            case 2 : return "Pictures";
            default : return null;
        }
    }

    public void loadDatabase(IWorkbook book) {
        foreach(ISheet sheet in book) {
            for(int i=1; i<sheet.Count(); ++i)
                switch(sheet.SheetName) {
                    case "Картины" : pictures.Add(new Pictures(sheet.GetRow(i))); break;
                    case "Стиль" : styles.Add(new Styles(sheet.GetRow(i))); break;
                    case "Художники" : artists.Add(new Artists(sheet.GetRow(i))); break;
                }
        }
        loaded = true;
    }

    public void removeData(int sheetIndex, int id) {
        IOPLog.write($"RD[sh:{sheetIndex}|id:{id}]", false);
        int index = findID(sheetIndex, id);
        if(index >= 0) {
            switch(sheetIndex) {
                case 0 : artists.RemoveAt(index); break;
                case 1 : styles.RemoveAt(index); break;
                case 2 : pictures.RemoveAt(index); break;
            }
            return;
        }
        IOPLog.write("Элемент не найден.");
    }

    public void addData(int sheetIndex, int id) {
        string data;
        IOPLog.write($"AD[sh:{sheetIndex}|id:{id}]", false);
        if(findID(sheetIndex, id) != -1) {
            IOPLog.write("Такой элемент уже есть."); return;}
        switch (sheetIndex) {
            case 0 : {
                    do {
                        Console.WriteLine("Введите имя для художника(<ФИО>): ");
                        data = IOPLog.read();
                    } while (data == null || data == "");
                    artists.Add(new Artists(id, data));
                }
                break;
            case 1 : {
                    do {
                        Console.WriteLine("Введите название для стиля(<Название>): ");
                        data = IOPLog.read();
                    } while (data == null || data == "");
                    styles.Add(new Styles(id, data));
                }
                break;
            case 2 : {
                    do
                    {
                        Console.WriteLine("Введите данные для картины через \";\".\nГод и ID стиля опционально.\n(<Название>;<ID художника>;<Часть Эрмитража>;[<Год|NULL>;<ID стиля|NULL>]): ");
                        data = IOPLog.read();
                    } while (data == null ||
                        data.Split(';').Length < 3 || data.Split(';').Length > 5 ||
                        !int.TryParse(data.Split(';')[1], out _) ||
                        !int.TryParse(data.Split(';')[2], out _) ||
                        data.Split(';').Length == 5 && !int.TryParse(data.Split(';')[4], out _));
                    pictures.Add(new Pictures(id, data));
                }
                break;
        }
    }

    public void updateData(int sheetIndex, int id) {
        IOPLog.write($"UD[sh:{sheetIndex}|id:{id}]", false);
        if(findID(sheetIndex, id) == -1) {
            IOPLog.write("Записи с данным идентификатором нет"); return;}
        removeData(sheetIndex, id); // lazy update.1
        addData(sheetIndex, id); // lazy update.2
    }



    public void task1() {
        IOPLog.write("Картины, имеющие стиль с идентификатором 1.");
        IEnumerable<Pictures> picturesStyle = from p in pictures where p.StyleID == 1 select p;
        foreach(Pictures i in picturesStyle)
            IOPLog.write(i.ToString());
    }

    public void task2() {
        IOPLog.write("Картины, написанные художником с ФИО \"Батони, Помпео\"");
        var picturesByBatoni = from pic in pictures
            join art in artists on pic.ArtistID equals art.ID
            where art.Name == "Батони, Помпео"
            select new {picName = pic.Name, artName = art.Name};
        foreach(var v in picturesByBatoni)
            IOPLog.write($"{v.picName} {v.artName}");
    }

    public void task3() {
        IOPLog.write("Число картин, написанных Беллотто, Бернардо в стиле Рококо");
        int amountByBellottoInRokoko = (from pic in pictures
            join art in artists on pic.ArtistID equals art.ID
            join sty in styles on pic.StyleID equals sty.ID
            where art.Name == "Беллотто, Бернардо" && sty.Name == "Рококо"
            select art).Count();
        IOPLog.write(amountByBellottoInRokoko.ToString());
    }
    public void task4() {
        IOPLog.write("Число работ стиля \"Реализм\" в первой части Эрмитража автора Берхем, Николас Питерс");
        int amountByBarguInReal = (from pic in pictures
            join art in artists on pic.ArtistID equals art.ID
            join sty in styles on pic.StyleID equals sty.ID
            where pic.Part == 1 && art.Name == "Берхем, Николас Питерс" && sty.Name == "Реализм"
            select pic).Count();
        IOPLog.write(amountByBarguInReal.ToString());
    }

    private int findID(int sheetIndex, int id) {
        IOPLog.write($"FI[sh:{sheetIndex}|id:{id}]::[{GetTable(sheetIndex).FindIndex(v => v.ID == id)}]", false);
        return GetTable(sheetIndex).FindIndex(v => v.ID == id);
    }
}
//sve metode su iste
//saveNesto(string s) - sprema string s u datoteku imena nestodata.txt
//getNesto() - vraca string spremljen u datoteci nestodata.txt
//metode nestoDataExists() provjeravaju postoji li datoteka nesto.txt

class Saving {
    private readonly static string highscores_path = Application.StartupPath + @"..\..\..\data\highscores.txt";
    private readonly static string map_path = Application.StartupPath + @"..\..\..\data\map.txt";
    private readonly static string worker_path = Application.StartupPath + @"..\..\..\data\worker.txt";
    private readonly static string manager_path = Application.StartupPath + @"..\..\..\data\manager.txt";
    private readonly static string soldier_path = Application.StartupPath + @"..\..\..\data\soldier.txt";
    private readonly static string wolf_path = Application.StartupPath + @"..\..\..\data\wolf.txt";

    public Saving() { }


    public static bool highscoresDataExists() {
        return File.Exists(highscores_path);
    }

    public static bool mapDataExists() {
        return File.Exists(map_path);
    }

    public static bool workerDataExists() {
        return File.Exists(worker_path);
    }

    public static bool managerDataExists() {
        return File.Exists(manager_path);
    }

    public static bool soldierDataExists() {
        return File.Exists(soldier_path);
    }

    public static bool wolfDataExists() {
        return File.Exists(wolf_path);
    }

    public static void saveHighscores(string highscore_data) {
        File.WriteAllText(highscores_path, highscore_data);
    }

    public static string getHighscores() {
        string text = File.ReadAllText(highscores_path);
        return text;
    }

    public static void saveMap(string map_data) {
        File.WriteAllText(map_path, map_data);
    }

    public static string getMap() {
        string text = File.ReadAllText(map_path);
        return text;
    }

    public static void saveWorker(string worker_data) {
        File.WriteAllText(worker_path, worker_data);
    }

    public static string getWorker() {
        string text = File.ReadAllText(worker_path);
        return text;
    }

    public static void saveManager(string manager_data) {
        File.WriteAllText(manager_path, manager_data);
    }

    public static string getManager() {
        string text = File.ReadAllText(manager_path);
        return text;
    }

    public static void saveSoldier(string soldier_data) {
        File.WriteAllText(soldier_path, soldier_data);
    }

    public static string getSoldier() {
        string text = File.ReadAllText(soldier_path);
        return text;
    }

    public static void saveWolf(string wolf_data) {
        File.WriteAllText(wolf_path, wolf_data);
    }

    public static string getWolf() {
        string text = File.ReadAllText(wolf_path);
        return text;
    }
}

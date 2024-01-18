//Forma koja drži u sebi mapu i obrađuje vremensku komponentu igre

using System.ComponentModel;
using System.Diagnostics;

namespace City_Rp3 {
    public partial class Game : Form, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        Map map;
        Workers workers;
        Soldiers soldiers;
        Wolves wolves;
        Manager manager;
        int tickNumber = 0;
        static int wolfTurn = 60;

        public event EventHandler<(int x, int y)>? MapClick; //proslijeđuje klikove iz MapUC

        public Map Map {
            get {
                return map;
            }
            set {
                if (map == null || !Map.Cmp(value)) {
                    if (value == null) map = null;
                    else map = new Map(value);
                    mapuc1.Map = value;
                    OnPropertyChanged("Map");
                }
            }
        }
        public Workers Workers {
            get {
                return workers;
            }
            set {
                if (workers == null || !Workers.Cmp(value)) {
                    if (value == null) workers = null;
                    //else workers = new Workers(value.dump());
                    else workers = new Workers(value);
                    mapuc1.Workers = value;
                    OnPropertyChanged("Workers");
                }
            }
        }
        public Soldiers Soldiers {
            get {
                return soldiers;
            }
            set {
                if (soldiers == null || !Soldiers.Cmp(value)) {
                    if (value == null) soldiers = null;
                    //else soldiers = new Soldiers(value.dump());
                    else soldiers = new Soldiers(value);
                    mapuc1.Soldiers = value;
                    OnPropertyChanged("Soldiers");
                }
            }
        }
        public Wolves Wolves {
            get {
                return wolves;
            }
            set {
                if (wolves == null || !Wolves.Cmp(value)) {
                    if (value == null) wolves = null;
                    else wolves = new Wolves(value);
                    mapuc1.Wolves = value;
                    OnPropertyChanged("Wolves");
                }
            }
        }
        public Manager Manager {
            get {
                return manager;
            }
            set {
                if (manager == null || !Manager.Cmp(value)) {
                    if (value == null) manager = null;
                    else manager = new Manager(value);
                    OnPropertyChanged("Manager");
                }
            }
        }
        //vraća najbližeg vuka vojniku sold_ID
        private static int? getClosestWolf(int sold_ID, Soldiers soldiers, Wolves wolves) {
            Dictionary<int, int> wolfDist = new Dictionary<int, int>();
            (int x, int y) soldCoords = soldiers.getPos(sold_ID);
            foreach (var id in wolves.getAllIds()) {
                (int x, int y) wolfCoords = wolves.getPos(id);
                wolfDist.Add(id, Math.Abs(soldCoords.x - wolfCoords.x) + Math.Abs(soldCoords.y - wolfCoords.y));
            }
            if (wolfDist.Count == 0) return null;
            return wolfDist.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
        }
        //vraća najbližeg ratnika vuku wold_ID; ako nema ratnika vraća najbližeg radnika
        private static (int?, bool) getClosestSoldierOrWorker(int wolf_ID, Soldiers soldiers, Wolves wolves, Workers workers) {
            bool targetingSoldier = true;
            Dictionary<int, int> eDist = new Dictionary<int, int>();
            (int x, int y) wolfCoords = wolves.getPos(wolf_ID);
            var soldIds = soldiers.getAllIds();
            if (soldIds.Any()) { //ako postoji ikoji vojnik njih prve zadaj, inače zadaj radnike
                foreach (var id in soldIds) {
                    (int x, int y) eCoords = soldiers.getPos(id);
                    eDist.Add(id, Math.Abs(eCoords.x - wolfCoords.x) + Math.Abs(eCoords.y - wolfCoords.y));
                }
            }
            else {
                targetingSoldier = false;
                foreach (var id in workers.getAllIds()) {
                    (int x, int y) eCoords = workers.getPos(id);
                    eDist.Add(id, Math.Abs(eCoords.x - wolfCoords.x) + Math.Abs(eCoords.y - wolfCoords.y));
                }
            }
            if (eDist.Count == 0) return (null, targetingSoldier);
            return (eDist.Aggregate((l, r) => l.Value < r.Value ? l : r).Key, targetingSoldier);
        }
        private void Mapuc1_Flush() { //šalje sve podatke mapup user kontroli za mapu
            mapuc1.Map = Map;
            mapuc1.Workers = Workers;
            mapuc1.Soldiers = Soldiers;
            mapuc1.Wolves = Wolves;
        }
        public Game() {

            Debug.WriteLine(Application.StartupPath);
            map = new Map();
            workers = new Workers();
            soldiers = new Soldiers();
            wolves = new Wolves();
            manager = new Manager();
            InitializeComponent();
            Mapuc1_Flush();
            mapuc1.MapClick += (sender, e) => MapClick?.Invoke(this, e);
        }
        private void timer1_Tick(object sender, EventArgs e) { //glavni tajmer za igru, tu se sve vremenski događa
            tickNumber++;
            if (Workers.hasID(0)) System.Diagnostics.Debug.WriteLine(Workers.getPos(0));
            if (tickNumber % wolfTurn == 0 && (tickNumber / wolfTurn) > 10) { //periodički napadi vukova
                Random rnd = new Random();
                int attackNo = (tickNumber / wolfTurn) - 10;
                int[] weights = new int[] {
                    100,
                    (10 * (attackNo-2))>0?(10 * (attackNo - 2)):0,
                    (20 * (attackNo-4))>0?(10 * (attackNo - 4)):0,
                    (30 * (attackNo-6))>0?(10 * (attackNo - 6)):0,
                    (40 * (attackNo-8))>0?(10 * (attackNo - 8)) : 0,
                    (50 * (attackNo-10))>0?(10 * (attackNo - 10)) : 0,
                    (60 * (attackNo-12))>0?(10 * (attackNo - 12)) : 0,
                    (70 * (attackNo-14))>0?(10 * (attackNo - 14)) : 0
                };
                int total = weights.Sum();
                int rnum = rnd.Next(total);
                int wolfNo = 0;
                for (int i = 0; i < weights.Length; i++) {
                    rnum -= weights[i];
                    if (rnum < 0) {
                        wolfNo = i + 1;
                        break;
                    }
                }
                System.Diagnostics.Debug.WriteLine("Napadaju nas " + wolfNo + " vuka");
                for (int i = 0; i < wolfNo; i++) {
                    bool placed = false;
                    while (!placed) {
                        int place = rnd.Next(80);
                        if (place < 20) {
                            if (Map.get((place, 0)) == Constants.Grass) {
                                placed = true;
                                Wolves.add((place, 0), Manager.Wolf_health());
                            }
                        }
                        else if (place < 40) {
                            if (Map.get((place - 20, 19)) == Constants.Grass) {
                                placed = true;
                                Wolves.add((place - 20, 19), Manager.Wolf_health());
                            }
                        }
                        else if (place < 60) {
                            if (Map.get((0, place - 40)) == Constants.Grass) {
                                placed = true;
                                Wolves.add((0, place - 40), Manager.Wolf_health());
                            }
                        }
                        else {
                            if (Map.get((19, place - 60)) == Constants.Grass) {
                                placed = true;
                                Wolves.add((19, place - 60), Manager.Wolf_health());
                            }
                        }
                    }

                }
            } //napad vukova
            if (tickNumber % (wolfTurn * 2) == 0 && (tickNumber / wolfTurn) > 11) { //periodička pojačanja vukova
                Random rand = new Random();
                int rno = rand.Next(2);
                if (rno == 0) {
                    System.Diagnostics.Debug.WriteLine("Upgrade vukova att");
                    Manager.boost_wolf_attack();
                }
                else {
                    System.Diagnostics.Debug.WriteLine("Upgrade vukova def");
                    Manager.boost_wolf_health();
                }
            }
            System.Diagnostics.Debug.WriteLine("tick " + tickNumber);
            List<int> workersAttacked = new List<int>();
            List<int> soldiersAttacked = new List<int>();
            List<int> wolvesAttacked = new List<int>();
            var work_IDs = Workers.getAllIds();
            foreach (var id in work_IDs) {//ponašanje svih radnika svaki tick
                Workers.step(id);
                if (Workers.getHealth(id) < Manager.Soldier_defence()) Workers.setHealth(id, Workers.getHealth(id) + Manager.Soldier_defence() / 10); //regeneracija
                if (Workers.getNextStep(id) == (null, null) && Workers.getDes(id) == Workers.getHomePos(id) && Workers.getDes(id) != (null, null)) { //ako sam doma ostavi resurse i odi na posao
                    if (Workers.isWaiting(id)) {
                        Workers.isWaiting(id, false);
                        Workers.goToWork(id);
                        Workers.updatePath(id, Map);
                        if (Workers.hasItem(id)) {
                            Workers.hasItem(id, false);
                            switch (Constants.toSingleBuildingId(Map.get(Workers.getWorkPos(id)))) {
                                case Constants.Wood:
                                    System.Diagnostics.Debug.WriteLine(id + " kaze da je donio drva.");
                                    Manager.Wood += Manager.Worker_efficiency_wood();
                                    break;
                                case Constants.Stone:
                                    System.Diagnostics.Debug.WriteLine(id + " kaze da je donio kamena.");
                                    Manager.Stone += Manager.Worker_efficiency_stone();
                                    break;
                                case Constants.Mine:
                                    System.Diagnostics.Debug.WriteLine(id + " kaze da je donio zeljeza.");
                                    Manager.Iron += Manager.Worker_efficiency_iron();
                                    break;
                                case Constants.Farm:
                                    System.Diagnostics.Debug.WriteLine(id + " kaze da je donio hrane.");
                                    Manager.Wheat += Manager.Worker_efficiency_wheat();
                                    break;
                                case Constants.Clayworks:
                                    System.Diagnostics.Debug.WriteLine(id + " kaze da je donio gline.");
                                    Manager.Clay += Manager.Worker_efficiency_clay();
                                    break;
                                default:
                                    throw new Exception("wrong working place defined for worker");
                            }
                        }
                    }
                    else Workers.isWaiting(id, true);
                }
                else if (Workers.getNextStep(id) == (null, null) && Workers.getDes(id) == Workers.getWorkPos(id) && Workers.getDes(id) != (null, null)) { //ako sam na poslu uzmi nove resurse i idi doma
                    if (Workers.isWaiting(id)) {
                        Workers.isWaiting(id, false);
                        Workers.goHome(id);
                        Workers.updatePath(id, Map);
                        Workers.hasItem(id, true);
                    }
                    else Workers.isWaiting(id, true);
                }

            }
            var sold_IDs = Soldiers.getAllIds();
            foreach (var id in sold_IDs) {
                if (Soldiers.getHealth(id) < Manager.Soldier_defence()) Soldiers.setHealth(id, Soldiers.getHealth(id) + Manager.Soldier_defence() / 10); //regen

                if (Soldiers.getTargetId(id) == null || !Wolves.hasID((int)Soldiers.getTargetId(id))) { //treba mi novi vuk za napast
                    Soldiers.setTargetId(id, getClosestWolf(id, Soldiers, Wolves));
                }

                if (Soldiers.getTargetId(id) != null) {
                    if (Soldiers.getPos(id) != Wolves.getPos((int)Soldiers.getTargetId(id))) { //micanje
                        Soldiers.setDes(Wolves.getPos((int)Soldiers.getTargetId(id)), id);
                        Soldiers.updatePath(id, Map);
                        Soldiers.step(id);
                    }
                    else wolvesAttacked.Add((int)Soldiers.getTargetId(id)); //napadanje vuka
                }
            } //ponašanje svih ratnika svaki tick
            var wolf_IDs = Wolves.getAllIds();
            foreach (var id in wolf_IDs) {
                if (Wolves.getHealth(id) < Manager.Wolf_health()) Wolves.setHealth(id, Wolves.getHealth(id) + Manager.Wolf_health() / 10); //regen

                if (Wolves.getTargetId(id) == null || (Wolves.isTargetingSoldier(id) ? (!Soldiers.hasID((int)Wolves.getTargetId(id))) : (!Workers.hasID((int)Wolves.getTargetId(id))))) { //treba mi novi vojnik/radnik za napast
                    (int?, bool) csow = getClosestSoldierOrWorker(id, soldiers, wolves, workers);
                    Wolves.setTargetId(id, csow.Item1);
                    Wolves.isTargetingSoldier(id, csow.Item2);
                }

                if (Wolves.getTargetId(id) != null) { //ako imam target
                    if (Wolves.isTargetingSoldier(id)) {
                        if (Wolves.getPos(id) != Soldiers.getPos((int)Wolves.getTargetId(id))) {//micanje
                            Wolves.setDes(Soldiers.getPos((int)Wolves.getTargetId(id)), id);
                            Wolves.updatePath(id, Map);
                            Wolves.step(id);
                        }
                        else soldiersAttacked.Add((int)Wolves.getTargetId(id)); //napadanje vojnika
                    }
                    else {
                        if (Wolves.getPos(id) != Workers.getPos((int)Wolves.getTargetId(id))) {//micanje
                            Wolves.setDes(Workers.getPos((int)Wolves.getTargetId(id)), id);
                            Wolves.updatePath(id, Map);
                            Wolves.step(id);
                        }
                        else workersAttacked.Add((int)Wolves.getTargetId(id));//napadanje radnika
                    }
                }
            } //ponašanje svih vukova svaki tick
            foreach (var id in wolvesAttacked) if (Wolves.hasID(id)) Wolves.setHealth(id, Wolves.getHealth(id) - Manager.Soldier_attack());
            foreach (var id in soldiersAttacked) if (Soldiers.hasID(id)) Soldiers.setHealth(id, Soldiers.getHealth(id) - Manager.Wolf_attack());
            foreach (var id in workersAttacked) if (Workers.hasID(id)) Workers.setHealth(id, Workers.getHealth(id) - Manager.Wolf_attack());


            OnPropertyChanged("Map");
            OnPropertyChanged("Workers");
            OnPropertyChanged("Soldiers");
            OnPropertyChanged("Wolves");
            OnPropertyChanged("Manager");

            Mapuc1_Flush();
        }
    }
}
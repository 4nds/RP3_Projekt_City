//User kontrol koja obrađuje mapu igre

namespace City_Rp3 {
    public partial class mapUC : UserControl {
        private Map? map;
        public event EventHandler<(int x, int y)>? MapClick; //event za klik na mapi
        public Map Map {
            get {
                return map;
            }
            set {
                if (map == null || !map.Cmp(value)) {
                    if (value == null) map = null;
                    else map = new Map(value);
                    pictureBox1.Refresh();
                }
            }
        }
        private Workers? workers;
        public Workers Workers {
            get { return workers; }
            set {
                if (workers == null || !workers.Cmp(value)) {
                    if (value == null) soldiers = null;
                    else workers = new Workers(value);
                    pictureBox1.Refresh();
                }
            }
        }
        private Soldiers? soldiers;
        public Soldiers Soldiers {
            get {
                return soldiers;
            }
            set {
                if (soldiers == null || !soldiers.Cmp(value)) {
                    if (value == null) soldiers = null;
                    //else soldiers = new Soldiers(value.dump());
                    else soldiers = new Soldiers(value);
                    pictureBox1.Refresh();
                }
            }
        }
        private Wolves? wolves;
        public Wolves Wolves {
            get {
                return wolves;
            }
            set {
                if (wolves == null || !wolves.Cmp(value)) {
                    if (value == null) wolves = null;
                    //else wolves = new Wolves(value.dump());
                    else wolves = new Wolves(value);
                    pictureBox1.Refresh();
                }
            }
        }
        public mapUC() {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) { //izcrtava mapu ovisno o objektu Map koji je dan ovom objektu
            if (Map == null) return;
            for (int i = 0; i < 20; i++) {
                for (int j = 0; j < 20; j++)
                    e.Graphics.DrawImage(Constants.getImageById(Map.get((i, j))), new Point(35 * i, 35 * j));

            }
            foreach (int id in Workers.getAllIds()) {
                (int, int) pos = Workers.getPos(id);
                e.Graphics.DrawImage(Constants.img_worker, new Point(35 * pos.Item1, 35 * pos.Item2));
            }
            foreach (int id in Soldiers.getAllIds()) {
                (int, int) pos = Soldiers.getPos(id);
                e.Graphics.DrawImage(Constants.img_soldier, new Point(35 * pos.Item1, 35 * pos.Item2));
            }
            foreach (int id in Wolves.getAllIds()) {
                (int, int) pos = Wolves.getPos(id);
                e.Graphics.DrawImage(Constants.img_wolf, new Point(35 * pos.Item1, 35 * pos.Item2));
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e) { //obrađuje klikove na mapu, šalje event daljnjem kodu
            Point cli_poi = PointToClient(new Point(MousePosition.X, MousePosition.Y));
            int x = cli_poi.X / 35;
            int y = cli_poi.Y / 35;
            MapClick?.Invoke(this, (x, y));
            int? worker_id = null, soldier_id = null, wolf_id = null;
            foreach (int id in Workers.getAllIds())
                if (Workers.getPos(id) == (x, y)) worker_id = id;
            foreach (int id in Soldiers.getAllIds())
                if (Soldiers.getPos(id) == (x, y)) soldier_id = id;
            foreach (int id in Wolves.getAllIds())
                if (Wolves.getPos(id) == (x, y)) wolf_id = id;
            switch (Constants.toSingleBuildingId(Map.get((x, y)))) {
                case Constants.Grass:
                    System.Diagnostics.Debug.WriteLine("Grass on " + x + " " + y);
                    break;
                case Constants.Wood:
                    System.Diagnostics.Debug.WriteLine("Wood on " + x + " " + y);
                    break;
                case Constants.Stone:
                    System.Diagnostics.Debug.WriteLine("Stone on " + x + " " + y);
                    break;
                case Constants.Iron:
                    System.Diagnostics.Debug.WriteLine("Iron on " + x + " " + y);
                    break;
                case Constants.Water:
                    System.Diagnostics.Debug.WriteLine("Water on " + x + " " + y);
                    break;
                case Constants.MainBuilding:
                    System.Diagnostics.Debug.WriteLine("MainBuilding on " + x + " " + y);
                    break;
                case Constants.Farm:
                    System.Diagnostics.Debug.WriteLine("Farm on " + x + " " + y);
                    break;
                case Constants.Mine:
                    System.Diagnostics.Debug.WriteLine("Mine on " + x + " " + y);
                    break;
                case Constants.Clayworks:
                    System.Diagnostics.Debug.WriteLine("Clayworks on " + x + " " + y);
                    break;
                case Constants.Wonder:
                    System.Diagnostics.Debug.WriteLine("Wonder on " + x + " " + y);
                    break;
                case Constants.Stockpile:
                    System.Diagnostics.Debug.WriteLine("Stockpile on " + x + " " + y);
                    break;
                case Constants.Smithy:
                    System.Diagnostics.Debug.WriteLine("Smithy on " + x + " " + y);
                    break;
                case Constants.Armory:
                    System.Diagnostics.Debug.WriteLine("Armory on " + x + " " + y);
                    break;
            }
            if (worker_id != null) System.Diagnostics.Debug.WriteLine("Worker with id " + worker_id);
            if (soldier_id != null) System.Diagnostics.Debug.WriteLine("Soldier with id " + soldier_id);
            if (wolf_id != null) System.Diagnostics.Debug.WriteLine("Wolf with id " + wolf_id);
            System.Diagnostics.Debug.WriteLine("");
        }
    }
}

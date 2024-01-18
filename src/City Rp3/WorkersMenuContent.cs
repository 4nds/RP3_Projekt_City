// Klasa WorkersMenuContent 
//
// kontrola u kojoj je sadržaj izbornika radnika
// (klasa se zove isključivo iz klase WorkersMenu)

using System.ComponentModel;

namespace City_Rp3 {
    public partial class WorkersMenuContent : UserControl, INotifyPropertyChanged {
        private static readonly Size WORKER_PANEL_SIZE = new(180, 40);
        private static readonly Size WORKER_PICTURE_SIZE = new(35, 35);
        private static readonly Size WORKER_LABEL_SIZE = new(100, 35);
        private static readonly Size JOB_PICTURE_SIZE = new(35, 35);
        private const int HORIZONTAL_MARGIN = 4;
        private const int WORKER_PANEL_MARGIN = 20;
        private const int MAX_WORKERS = 10;
        private static readonly (int x, int y) MAIN_BUILDING_POSITION = (10, 10);

        private Panel[] _worker_panels;
        private int _selected_worker_id;

        private readonly Menu _menu;
        private Map _map;
        private Manager _manager;
        //private FakeResources _resources;
        //private Soldiers _soldiers;
        //private Wolves _wolves;
        private Workers _workers;

        public Manager Manager {
            get => _manager;
            set => _manager = !_manager.Cmp(value) ? value : _manager;
        }

        public Map Map {
            get => _map;
            set => _map = !_map.Cmp(value) ? value : _map;
        }

        public Workers Workers {
            get => _workers;
            set {
                if (!_workers.Cmp(value)) {
                    foreach (int worker_id in _workers.getAllIds()) {
                        if (!(value.hasID(worker_id) && _workers.getWorkPos(worker_id)
                            == value.getWorkPos(worker_id))) {
                            showWorkers();
                            break;
                        }
                    }
                    if (value.getAllIds().Length != _worker_panels.Length) {
                        showWorkers();
                    }

                    _workers = value;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public WorkersMenuContent(Menu menu) {
            InitializeComponent();

            _menu = menu;
            _manager = new();
            _map = new();
            _workers = new();
            _worker_panels = new Panel[0];
            _selected_worker_id = -1;
        }

        private void workerClick(Panel worker_panel, int worker_id) {
            foreach (Panel panel in _worker_panels) {
                panel.BorderStyle = BorderStyle.None;
            }
            worker_panel.BorderStyle = BorderStyle.FixedSingle;
            _selected_worker_id = worker_id;
        }

        private Panel createWorkerPanel(int worker_id) {
            Panel worker_panel = new() {
                Size = WORKER_PANEL_SIZE,
            };

            int worker_picture_x = HORIZONTAL_MARGIN;
            PictureBox worker_picture_box = new() {
                Image = Constants.img_worker,
                Size = WORKER_PICTURE_SIZE,
                Location = new Point(worker_picture_x, 0),
            };
            worker_panel.Controls.Add(worker_picture_box);

            int worker_label_x = worker_picture_x + WORKER_PICTURE_SIZE.Width;
            Label worker_label = new() {
                Text = $"Worker {worker_id}",
                Size = WORKER_LABEL_SIZE,
                Location = new Point(worker_label_x, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 0, 0),
            };
            worker_panel.Controls.Add(worker_label);

            (int?, int?) job_position = _workers.getWorkPos(worker_id);
            if (job_position != (null, null)) {
                int asset_id = Constants.toSingleBuildingId(_map.get(job_position));
                int job_picture_x = worker_label_x + WORKER_LABEL_SIZE.Width;
                PictureBox job_picture_box = new() {
                    Image = Constants.getImageById(asset_id),
                    Size = JOB_PICTURE_SIZE,
                    Location = new Point(job_picture_x, 0),
                };
                worker_panel.Controls.Add(job_picture_box);
            }

            EventHandler worker_click = (sender, e) =>
                workerClick(worker_panel, worker_id);
            worker_picture_box.Click += worker_click;
            worker_label.Click += worker_click;
            worker_panel.Click += worker_click;
            return worker_panel;
        }

        internal void showWorkers() {
            int[] worker_ids = _workers.getAllIds();
            workers_panel.Controls.Clear();
            _worker_panels = new Panel[worker_ids.Length];
            for (int i = 0; i < worker_ids.Length; i++) {
                int worker_id = worker_ids[i];
                Panel worker_panel = createWorkerPanel(worker_id);
                int worker_panel_y = WORKER_PANEL_MARGIN +
                    i * (WORKER_PANEL_SIZE.Height + WORKER_PANEL_MARGIN);
                worker_panel.Location =
                    new Point(HORIZONTAL_MARGIN, worker_panel_y);
                workers_panel.Controls.Add(worker_panel);
                worker_panel.BringToFront();
                _worker_panels[i] = worker_panel;
            }

            (int wood, int wheat, int stone, int iron, int clay) =
                    Constants.getCost(Constants.Worker);
            if (worker_ids.Length < MAX_WORKERS
                && (_manager.Wood >= wood && _manager.Wheat >= wheat
                && _manager.Stone >= stone && _manager.Iron >= iron
                && _manager.Clay >= clay)) {
                add_worker_button.Enabled = true;
            }
            else {
                add_worker_button.Enabled = false;
            }
        }

        private void add_worker_button_Click(object sender, EventArgs e) {
            int[] worker_ids = _workers.getAllIds();
            if (worker_ids.Length < MAX_WORKERS) {
                Random rnd = new();
                int index = rnd.Next(8);
                (int x, int y)[] grass_positions = new (int, int)[]
                {
                     (MAIN_BUILDING_POSITION.x - 1, MAIN_BUILDING_POSITION.y - 2),
                     (MAIN_BUILDING_POSITION.x, MAIN_BUILDING_POSITION.y - 2),
                     (MAIN_BUILDING_POSITION.x + 1, MAIN_BUILDING_POSITION.y - 1),
                     (MAIN_BUILDING_POSITION.x + 1, MAIN_BUILDING_POSITION.y),
                     (MAIN_BUILDING_POSITION.x, MAIN_BUILDING_POSITION.y + 1),
                     (MAIN_BUILDING_POSITION.x - 1, MAIN_BUILDING_POSITION.y + 1),
                     (MAIN_BUILDING_POSITION.x - 2, MAIN_BUILDING_POSITION.y),
                     (MAIN_BUILDING_POSITION.x - 2, MAIN_BUILDING_POSITION.y - 1),
                };
                if (_manager.build_worker()) {
                    int worker_id = _workers.add(grass_positions[index],
                        _manager.Soldier_defence());
                    _workers.setHomePos(MAIN_BUILDING_POSITION, worker_id);
                    showWorkers();
                    onPropertyChanged(Manager, "Manager");
                    onPropertyChanged(Workers, "Workers");
                }
            }
        }

        internal void mapClick((int x, int y) position, int id) {
            if (_selected_worker_id >= 0) {
                bool is_resource_place = id switch {
                    Constants.Wood => true,
                    Constants.Farm => true,
                    Constants.Stone => true,
                    Constants.Mine => true,
                    Constants.Clayworks => true,
                    _ => false,
                };
                if (is_resource_place) {
                    _workers.setWorkPos(position, _selected_worker_id);
                    _workers.goHome(_selected_worker_id);
                    _workers.updatePath(_selected_worker_id, _map);
                    showWorkers();
                    onPropertyChanged(Workers, "Workers");
                }
                bool is_stock_place = id switch {
                    Constants.MainBuilding => true,
                    Constants.Stockpile => true,
                    _ => false,
                };
                if (is_stock_place) {
                    _workers.setHomePos(position, _selected_worker_id);
                    if (_workers.getWorkPos(_selected_worker_id) != (null, null)) {
                        _workers.goHome(_selected_worker_id);
                        _workers.updatePath(_selected_worker_id, _map);
                    }
                    onPropertyChanged(Workers, "Workers");
                }
                if (is_resource_place || is_stock_place) {
                    foreach (Panel panel in _worker_panels) {
                        panel.BorderStyle = BorderStyle.None;
                    }
                    _selected_worker_id = -1;
                }

            }
        }

        private void onPropertyChanged(Object sender, string? propertyName) {
            PropertyChanged?.Invoke(sender,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

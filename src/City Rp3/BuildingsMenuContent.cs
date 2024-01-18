// Klasa BuildingsMenuContent 
//
// kontrola u kojoj je sadržaj izbornika zgrada
// (klasa se zove isključivo iz klase BuildingsMenu)

using System.ComponentModel;

namespace City_Rp3 {
    public partial class BuildingsMenuContent : UserControl, INotifyPropertyChanged {
        private static readonly Size BUILDING_PANEL_SIZE = new(270, 170);
        private static readonly Size BUILDING_PICTURE_SIZE = new(35, 35);
        private static readonly Size BUILDING_LABEL_SIZE = new(130, 35);
        private static readonly Size RESOURCES_PANEL_SIZE = new(270, 60);
        private static readonly Size RESOURCE_PICTURE_SIZE = new(30, 30);
        private static readonly Size RESOURCE_LABEL_SIZE = new(50, 20);
        private const int RESOURCE_FONT_SIZE = 7;
        private static readonly Size BUILD_BUTTON_SIZE = new(80, 30);
        private const int HORIZONTAL_MARGIN = 4;
        private const int VERTICAL_MARGIN = 6;
        private const int BUILDING_PANEL_MARGIN = 30;
        private const int SOLDIERS_HEALTH = 10;

        private static readonly Dictionary<int, string> BUILDINGS = new()
        {
            { Constants.Farm, "Farm" },
            { Constants.Mine, "Mine" },
            { Constants.Clayworks, "Clayworks" },
            { Constants.Stockpile, "Stockpile" },
            { Constants.Smithy, "Smithy" },
            { Constants.Armory, "Armory" },
            { Constants.Wonder, "Wonder" },
        };

        private readonly Dictionary<int, Button> _build_buttons;
        private int _selected_building_id;

        private readonly Menu _menu;
        private Manager _manager;
        private Map _map;
        private Soldiers _soldiers;
        private Wolves _wolves;
        private Workers _workers;

        public Map Map {
            get => _map;
            set => _map = !_map.Cmp(value) ? value : _map;
        }

        public Manager Manager {
            get => _manager;
            set => _manager = !_manager.Cmp(value) ? value : _manager;
        }

        public Soldiers Soldiers {
            get => _soldiers;
            set => _soldiers = !_soldiers.Cmp(value) ? value : _soldiers;
        }

        public Wolves Wolves {
            get => _wolves;
            set => _wolves = !_wolves.Cmp(value) ? value : _wolves;
        }

        public Workers Workers {
            get => _workers;
            set => _workers = !_workers.Cmp(value) ? value : _workers;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public BuildingsMenuContent(Menu menu) {
            InitializeComponent();

            _menu = menu;
            _map = new();
            _manager = new();
            _soldiers = new();
            _wolves = new();
            _workers = new();
            _build_buttons = new();
            _selected_building_id = -1;
            showBuildings();
        }

        private void showBuildings() {
            for (int i = 0; i < BUILDINGS.Count; i++) {
                KeyValuePair<int, string> building = BUILDINGS.ElementAt(i);
                int building_id = building.Key;
                string building_name = building.Value;
                Panel building_panel =
                    createBuildingPanel(building_id, building_name);
                int building_panel_y = VERTICAL_MARGIN +
                    i * (BUILDING_PANEL_SIZE.Height + BUILDING_PANEL_MARGIN);
                building_panel.Location =
                    new Point(HORIZONTAL_MARGIN, building_panel_y);
                Controls.Add(building_panel);
                building_panel.BringToFront();
            }
            updateBuildButtons();
        }

        private void build(Button build_button, int building_id) {
            foreach (Button button in _build_buttons.Values) {
                button.Enabled = true;
            }
            build_button.Enabled = false;
            _selected_building_id = building_id;
        }

        private Panel createBuildingPanel(int building_id,
            string building_name) {
            Panel building_panel = new() {
                Size = BUILDING_PANEL_SIZE,
                BorderStyle = BorderStyle.FixedSingle,
            };

            int building_picture_x = HORIZONTAL_MARGIN;
            PictureBox building_picture_box = new() {
                Image = Constants.getImageById(building_id),
                Size = BUILDING_PICTURE_SIZE,
                Location = new Point(building_picture_x, VERTICAL_MARGIN),
            };
            building_panel.Controls.Add(building_picture_box);

            int building_label_x = building_picture_x + BUILDING_PICTURE_SIZE.Width;
            Label building_label = new() {
                Text = building_name,
                Size = BUILDING_LABEL_SIZE,
                Location = new Point(building_label_x, VERTICAL_MARGIN),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 0, 0),
            };
            building_panel.Controls.Add(building_label);

            Panel resources_panel = createResourcesPanel(building_id);
            int resources_panel_y = 3 * VERTICAL_MARGIN +
                BUILDING_PICTURE_SIZE.Height;
            resources_panel.Location =
                    new Point(0, resources_panel_y);
            building_panel.Controls.Add(resources_panel);

            int building_button_x =
                (BUILDING_PANEL_SIZE.Width - BUILD_BUTTON_SIZE.Width) / 2;
            int building_button_y = 2 * VERTICAL_MARGIN +
                resources_panel_y + RESOURCES_PANEL_SIZE.Height;
            Button build_button = new() {
                Text = "Build",
                Size = BUILD_BUTTON_SIZE,
                Location = new Point(building_button_x, building_button_y),
            };
            _build_buttons[building_id] = build_button;
            build_button.Click += (sender, e) =>
                build(build_button, building_id);
            building_panel.Controls.Add(build_button);

            return building_panel;

        }

        private Panel createResourcesPanel(int building_id) {
            Panel resources_panel = new() {
                Size = RESOURCES_PANEL_SIZE,
            };

            (int wood, int wheat, int stone, int iron, int clay) =
                Constants.getCost(building_id);
            Dictionary<int, int> building_resources = new()
            {
                { Constants.Wood, wood },
                { Constants.Wheat, wheat },
                { Constants.Stone, stone },
                { Constants.Iron, iron },
                { Constants.Clay, clay },
            };
            for (int i = 0; i < building_resources.Count; i++) {
                KeyValuePair<int, int> resource =
                    building_resources.ElementAt(i);
                int resource_id = resource.Key;
                int resource_quantity = resource.Value;

                int resource_x = HORIZONTAL_MARGIN +
                    i * (RESOURCE_LABEL_SIZE.Width + HORIZONTAL_MARGIN);
                int resource_picture_x = resource_x +
                    (RESOURCE_LABEL_SIZE.Width - RESOURCE_PICTURE_SIZE.Width) / 2;
                PictureBox resource_picture_box = new() {
                    Image = Constants.getImageById(resource_id),
                    Size = RESOURCE_PICTURE_SIZE,
                    Location = new Point(resource_picture_x, 0),
                };
                resources_panel.Controls.Add(resource_picture_box);

                Label resource_label = new() {
                    Text = resource_quantity.ToString(),
                    Size = RESOURCE_LABEL_SIZE,
                    Location = new Point(resource_x, 40),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", RESOURCE_FONT_SIZE),
                };
                resources_panel.Controls.Add(resource_label);
            }
            return resources_panel;
        }

        internal void updateBuildButtons() {
            foreach (KeyValuePair<int, Button> build_button_pair in _build_buttons) {
                int building_id = build_button_pair.Key;
                Button build_button = build_button_pair.Value;
                (int wood, int wheat, int stone, int iron, int clay) =
                    Constants.getCost(building_id);
                if (_manager.Wood >= wood && _manager.Wheat >= wheat
                    && _manager.Stone >= stone && _manager.Iron >= iron
                    && _manager.Clay >= clay) {
                    build_button.Enabled = true;
                }
                else {
                    build_button.Enabled = false;
                }
            }
        }

        internal void mapClick((int x, int y) position, int id) {
            if (_selected_building_id >= 0) {
                (int cols, int rows) = Constants.BuildingSize(_selected_building_id);
                if (PlacingAndPathFind.IsPossibleToPlace(position,
                    _selected_building_id, _map)) {
                    Func<bool> manager_build = _selected_building_id switch {
                        Constants.Farm => _manager.build_farm,
                        Constants.Mine => _manager.build_mine,
                        Constants.Clayworks => _manager.build_clayworks,
                        Constants.Wonder => _manager.build_wonder,
                        Constants.Stockpile => _manager.build_stockpile,
                        Constants.Smithy => _manager.build_smithy,
                        Constants.Armory => _manager.build_armory,
                        _ => () => false,
                    };

                    if (manager_build()) {
                        _map.build(position, _selected_building_id);
                        foreach (int worker_id in _workers.getAllIds()) {
                            if (_workers.getDes(worker_id) != (null, null)) {
                                _workers.updatePath(worker_id, _map);
                            }
                        }
                        foreach (int wolf_id in _wolves.getAllIds()) {
                            if (_wolves.getDes(wolf_id) != (null, null)) {
                                _wolves.updatePath(wolf_id, _map);
                            }
                        }
                        foreach (int soldier_id in _soldiers.getAllIds()) {
                            if (_soldiers.getDes(soldier_id) != (null, null)) {
                                _soldiers.updatePath(soldier_id, _map);
                            }
                        }
                        onPropertyChanged(Map, "Map");
                        onPropertyChanged(Workers, "Workers");
                        onPropertyChanged(Wolves, "Wolves");
                        onPropertyChanged(Soldiers, "Soldiers");

                        _menu.hide();
                    }
                    onPropertyChanged(Manager, "Manager");

                    foreach (Button button in _build_buttons.Values) {
                        button.Enabled = true;
                    }
                    _selected_building_id = -1;
                }
            }
        }

        private void onPropertyChanged(Object sender, string? propertyName) {
            PropertyChanged?.Invoke(sender,
                new PropertyChangedEventArgs(propertyName));
        }

    }
}

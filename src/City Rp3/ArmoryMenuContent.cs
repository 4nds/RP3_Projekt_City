// Klasa ArmoryMenuContent 
//
// kontrola u kojoj je sadržaj izbornika oružarnica
// (klasa se zove isključivo iz klase ArmoryMenu)


using System.ComponentModel;

namespace City_Rp3 {
    public partial class ArmoryMenuContent : UserControl, INotifyPropertyChanged {
        private static readonly Size RESOURCES_PANEL_SIZE = new(270, 60);
        private static readonly Size RESOURCE_PICTURE_SIZE = new(30, 30);
        private static readonly Size RESOURCE_LABEL_SIZE = new(50, 20);
        private const int RESOURCE_FONT_SIZE = 7;
        private const int RESOURCES_PANEL_Y = 50;
        private const int HORIZONTAL_MARGIN = 4;
        private const int MAX_SOLDIERS = 10;
        private static readonly (int x, int y) MAIN_BUILDING_POSITION = (10, 10);

        private readonly Menu _menu;
        private Manager _manager;
        private Map _map;
        private Soldiers _soldiers;
        //private Wolves _wolves;
        //private Workers _workers;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Manager Manager {
            get => _manager;
            set => _manager = !_manager.Cmp(value) ? value : _manager;
        }

        public Map Map {
            get => _map;
            set => _map = !_map.Cmp(value) ? value : _map;
        }

        public Soldiers Soldiers {
            get => _soldiers;
            set => _soldiers = !_soldiers.Cmp(value) ? value : _soldiers;
        }

        public ArmoryMenuContent(Menu menu) {
            InitializeComponent();

            _menu = menu;
            _manager = new();
            _map = new();
            _soldiers = new();
            showAttackResources();
            showDefenceResources();
            updateLabelsAndButton();
        }

        internal void updateLabelsAndButton() {
            int attack_level = _manager.Soldier_attack() / 10;
            attack_label.Text = $"Attack: {attack_level}";
            int defence_level = _manager.Soldier_defence() / 10;
            defense_label.Text = $"Defense: {defence_level}";

            int[] soldier_ids = _soldiers.getAllIds();
            (int wood, int wheat, int stone, int iron, int clay) =
                    Constants.getCost(Constants.Soldier);
            if (soldier_ids.Length < MAX_SOLDIERS &&
                (_manager.Wood >= wood && _manager.Wheat >= wheat
                && _manager.Stone >= stone && _manager.Iron >= iron
                && _manager.Clay >= clay)) {
                add_soldier_button.Enabled = true;
            }
            else {
                add_soldier_button.Enabled = false;
            }
        }

        private Panel createResourcesPanel(string type) {
            (int wood, int wheat, int stone, int iron, int clay) = type switch {
                "attack" => Constants.getCost(Constants.BoostAttack),
                "defence" => Constants.getCost(Constants.BoostAttack),
                _ => throw new ArgumentException($"Unsupported type {type}."),
            };

            Dictionary<int, int> resources = new()
            {
                { Constants.Wood, wood },
                { Constants.Wheat, wheat },
                { Constants.Stone, stone },
                { Constants.Iron, iron },
                { Constants.Clay, clay },
            };

            Panel resources_panel = new() {
                Size = RESOURCES_PANEL_SIZE,
            };

            for (int i = 0; i < resources.Count; i++) {
                KeyValuePair<int, int> resource =
                    resources.ElementAt(i);
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

        private void showAttackResources() {
            Panel resources_panel = createResourcesPanel("attack");
            int resources_panel_x =
                (Width - RESOURCES_PANEL_SIZE.Width) / 2;
            resources_panel.Location =
                    new Point(resources_panel_x, RESOURCES_PANEL_Y);
            attack_panel.Controls.Add(resources_panel);
        }

        private void showDefenceResources() {
            Panel resources_panel = createResourcesPanel("defence");
            int resources_panel_x =
                (Width - RESOURCES_PANEL_SIZE.Width) / 2;
            resources_panel.Location =
                    new Point(resources_panel_x, RESOURCES_PANEL_Y);
            defence_panel.Controls.Add(resources_panel);
        }

        private void add_soldier_button_Click(object sender, EventArgs e) {
            int[] soldier_ids = _soldiers.getAllIds();
            if (soldier_ids.Length < MAX_SOLDIERS) {
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
                if (_manager.build_soldier()) {
                    int soldier_id = _soldiers.add(grass_positions[index],
                        _manager.Soldier_defence());
                    updateLabelsAndButton();
                    onPropertyChanged(Manager, "Manager");
                    onPropertyChanged(Soldiers, "Soldiers");
                }
            }
        }

        private void onPropertyChanged(Object sender, string? propertyName) {
            PropertyChanged?.Invoke(sender,
                new PropertyChangedEventArgs(propertyName));
        }

        private void attack_button_Click(object sender, EventArgs e) {
            if (_manager.boost_soldier_attack()) {
                updateLabelsAndButton();
                onPropertyChanged(Manager, "Manager");
            }
        }

        private void defense_button_Click(object sender, EventArgs e) {
            if (_manager.boost_soldier_defence()) {
                updateLabelsAndButton();
                onPropertyChanged(Manager, "Manager");
            }
        }
    }
}

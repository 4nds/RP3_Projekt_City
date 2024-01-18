// Klasa SmithyMenuContent 
//
// kontrola u kojoj je sadržaj izbornika kovačnice
// (klasa se zove isključivo iz klase SmithyMenu)

using System.ComponentModel;

namespace City_Rp3 {
    public partial class SmithyMenuContent : UserControl, INotifyPropertyChanged {
        private static readonly Size RESOURCES_PANEL_SIZE = new(270, 60);
        private static readonly Size RESOURCE_PICTURE_SIZE = new(30, 30);
        private static readonly Size RESOURCE_LABEL_SIZE = new(50, 20);
        private const int RESOURCE_FONT_SIZE = 7;
        private static readonly Size EFFICIENCY_PANEL_SIZE = new(290, 175);
        private static readonly Size EFFICIENCY_LABEL_SIZE = new(180, 25);
        private static readonly Size UPGRADE_BUTTON_SIZE = new(100, 36);
        private const int HORIZONTAL_MARGIN = 4;
        private const int VERTICAL_MARGIN = 6;
        private const int EFFICIENCY_PANEL_MARGIN = 10;

        private static readonly Dictionary<int, string> RESOURCES = new()
        {
            { Constants.Wood, "Wood" },
            { Constants.Wheat, "Wheat" },
            { Constants.Stone, "Stone" },
            { Constants.Iron, "Iron" },
            { Constants.Clay, "Clay" },
        };

        private readonly Dictionary<int, Button> _upgrade_buttons;
        private readonly Dictionary<int, Label> _upgrade_labels;

        private readonly Menu _menu;
        private Manager _manager;
        //private Map _map;
        //private FakeResources _resources;
        //private Soldiers _soldiers;
        //private Wolves _wolves;
        //private Workers _workers;      

        public Manager Manager {
            get => _manager;
            set => _manager = !_manager.Cmp(value) ? value : _manager;
        }

        public event EventHandler<EventArgs>? onUpgradeWorkerEfficiency;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SmithyMenuContent(Menu menu) {
            InitializeComponent();

            _menu = menu;
            _manager = new();
            _upgrade_buttons = new();
            _upgrade_labels = new();
            Controls.Clear();
            showResuourceEfficiencies();
        }

        private Panel createResourcesPanel(int resource_id) {
            int boost_id = resource_id switch {
                Constants.Wood => Constants.BoostWood,
                Constants.Wheat => Constants.BoostWheat,
                Constants.Stone => Constants.BoostStone,
                Constants.Iron => Constants.BoostIron,
                Constants.Clay => Constants.BoostClay,
                _ => throw new ArgumentException($"Not supported resource_id {resource_id}"),
            };

            (int wood, int wheat, int stone, int iron, int clay) =
                Constants.getCost(boost_id);
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
                int lresource_id = resource.Key;
                int resource_quantity = resource.Value;

                int resource_x = HORIZONTAL_MARGIN +
                    i * (RESOURCE_LABEL_SIZE.Width + HORIZONTAL_MARGIN);
                int resource_picture_x = resource_x +
                    (RESOURCE_LABEL_SIZE.Width - RESOURCE_PICTURE_SIZE.Width) / 2;
                PictureBox resource_picture_box = new() {
                    Image = Constants.getImageById(lresource_id),
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

        private void upgrade(Button upgrade_button, int resource_id) {
            Func<bool> upgrade_method = resource_id switch {
                Constants.Wood => _manager.boost_efficiency_wood,
                Constants.Wheat => _manager.boost_efficiency_wheat,
                Constants.Stone => _manager.boost_efficiency_stone,
                Constants.Iron => _manager.boost_efficiency_iron,
                Constants.Clay => _manager.boost_efficiency_clay,
                _ => () => false,
            };

            upgrade_method();

            int efficiency_level = resource_id switch {
                Constants.Wood => _manager.Worker_efficiency_wood(),
                Constants.Wheat => _manager.Worker_efficiency_wheat(),
                Constants.Stone => _manager.Worker_efficiency_stone(),
                Constants.Iron => _manager.Worker_efficiency_iron(),
                Constants.Clay => _manager.Worker_efficiency_clay(),
                _ => 0,
            };

            efficiency_level /= 10;

            _upgrade_labels[resource_id].Text =
                $"{RESOURCES[resource_id]} Efficiency: {efficiency_level}";

            onPropertyChanged(Manager, "Manager");
        }

        private Panel createEfficiencyPanel(int resource_id,
            string resource_name) {
            Panel efficiency_panel = new() {
                Size = EFFICIENCY_PANEL_SIZE,
                BorderStyle = BorderStyle.FixedSingle,
            };

            int efficiency_label_x = HORIZONTAL_MARGIN;
            int efficiency_level = resource_id switch {
                Constants.Wood => _manager.Worker_efficiency_wood(),
                Constants.Wheat => _manager.Worker_efficiency_wheat(),
                Constants.Stone => _manager.Worker_efficiency_stone(),
                Constants.Iron => _manager.Worker_efficiency_iron(),
                Constants.Clay => _manager.Worker_efficiency_clay(),
                _ => 0,
            };
            efficiency_level /= 10;
            Label efficiency_label = new() {
                Text = $"{resource_name} Efficiency: {efficiency_level}",
                Size = EFFICIENCY_LABEL_SIZE,
                Location = new Point(efficiency_label_x, VERTICAL_MARGIN),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 0, 0),
            };
            _upgrade_labels[resource_id] = efficiency_label;
            efficiency_panel.Controls.Add(efficiency_label);

            Panel resources_panel = createResourcesPanel(resource_id);
            int resources_panel_x =
                (Width - 30 - RESOURCES_PANEL_SIZE.Width) / 2;
            int resources_panel_y = 3 * VERTICAL_MARGIN +
                EFFICIENCY_LABEL_SIZE.Height;
            resources_panel.Location =
                    new Point(resources_panel_x, resources_panel_y);
            efficiency_panel.Controls.Add(resources_panel);

            int upgrade_button_x =
                (EFFICIENCY_PANEL_SIZE.Width - UPGRADE_BUTTON_SIZE.Width) / 2;
            int upgrade_button_y = 3 * VERTICAL_MARGIN +
                resources_panel_y + RESOURCES_PANEL_SIZE.Height;
            Button upgrade_button = new() {
                Text = "Upgrade",
                Size = UPGRADE_BUTTON_SIZE,
                Location = new Point(upgrade_button_x, upgrade_button_y),
            };
            _upgrade_buttons[resource_id] = upgrade_button;
            upgrade_button.Click += (sender, e) =>
                upgrade(upgrade_button, resource_id);
            efficiency_panel.Controls.Add(upgrade_button);

            return efficiency_panel;

        }

        private void showResuourceEfficiencies() {
            for (int i = 0; i < RESOURCES.Count; i++) {
                KeyValuePair<int, string> resource = RESOURCES.ElementAt(i);
                int resource_id = resource.Key;
                string resource_name = resource.Value;
                Panel efficiency_panel =
                    createEfficiencyPanel(resource_id, resource_name);
                int efficiency_panel_y = VERTICAL_MARGIN +
                    i * (EFFICIENCY_PANEL_SIZE.Height + EFFICIENCY_PANEL_MARGIN);
                efficiency_panel.Location =
                    new Point(HORIZONTAL_MARGIN, efficiency_panel_y);
                Controls.Add(efficiency_panel);
                efficiency_panel.BringToFront();
            }
            updateUpgradeButtons();
        }

        internal void updateUpgradeButtons() {
            foreach (KeyValuePair<int, Button> upgrade_button_pair in _upgrade_buttons) {
                int resource_id = upgrade_button_pair.Key;
                Button upgrade_button = upgrade_button_pair.Value;
                int boost_id = resource_id switch {
                    Constants.Wood => Constants.BoostWood,
                    Constants.Wheat => Constants.BoostWheat,
                    Constants.Stone => Constants.BoostStone,
                    Constants.Iron => Constants.BoostIron,
                    Constants.Clay => Constants.BoostClay,
                    _ => throw new ArgumentException($"Not supported resource_id {resource_id}"),
                };
                (int wood, int wheat, int stone, int iron, int clay) =
                    Constants.getCost(boost_id);
                if (_manager.Wood >= wood && _manager.Wheat >= wheat
                    && _manager.Stone >= stone && _manager.Iron >= iron
                    && _manager.Clay >= clay) {
                    upgrade_button.Enabled = true;
                }
                else {
                    upgrade_button.Enabled = false;
                }
            }
        }
        private void onPropertyChanged(Object sender, string? propertyName) {
            PropertyChanged?.Invoke(sender,
                new PropertyChangedEventArgs(propertyName));
        }

    }
}

// Klasa MainBuildingMenuContent 
//
// kontrola u kojoj je sadržaj izbornika glavne zgrade
// (klasa se zove isključivo iz klase MainBuildingMenu)

using System.ComponentModel;

namespace City_Rp3 {
    internal partial class MainBuildingMenuContent : UserControl, INotifyPropertyChanged {
        private static readonly Size RESOURCES_PANEL_SIZE = new(270, 60);
        private static readonly Size RESOURCE_PICTURE_SIZE = new(30, 30);
        private static readonly Size RESOURCE_LABEL_SIZE = new(50, 20);
        private const int RESOURCE_FONT_SIZE = 7;
        private const int RESOURCES_PANEL_Y = 165;
        private const int HORIZONTAL_MARGIN = 4;
        private const int VERTICAL_MARGIN = 5;

        private readonly Dictionary<int, Label> _resources_labels;

        private readonly Menu _menu;
        private Manager _manager;
        //private Map _map;
        //private Soldiers _soldiers;
        //private Wolves _wolves;
        //private Workers _workers;

        public Manager Manager {
            get => _manager;
            set {
                if (!_manager.Cmp(value)) {
                    _manager = value;
                    updateResources();
                }
            }

        }

        public event EventHandler<EventArgs>? ShowBuildings;
        public event EventHandler<EventArgs>? ShowWorkers;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainBuildingMenuContent(Menu menu) {
            InitializeComponent();

            _menu = menu;
            _manager = new();
            _resources_labels = new();
            showResources();
        }

        private Panel createResourcesPanel() {
            int[] resources = new[]
            {
                Constants.Wood,
                Constants.Wheat,
                Constants.Stone,
                Constants.Iron,
                Constants.Clay,
            };

            Panel resources_panel = new() {
                Size = RESOURCES_PANEL_SIZE,
            };

            for (int i = 0; i < resources.Length; i++) {
                int resource_id = resources[i];

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

                int resource_label_y = resource_picture_box.Height + VERTICAL_MARGIN;
                Label resource_label = new() {
                    Size = RESOURCE_LABEL_SIZE,
                    Location = new Point(resource_x, resource_label_y),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", RESOURCE_FONT_SIZE),
                };
                _resources_labels[resource_id] = resource_label;
                resources_panel.Controls.Add(resource_label);
            }
            return resources_panel;
        }

        internal void updateResources() {
            Dictionary<int, int> resources = new()
            {
                { Constants.Wood, _manager.Wood },
                { Constants.Wheat, _manager.Wheat },
                { Constants.Stone, _manager.Stone },
                { Constants.Iron, _manager.Iron },
                { Constants.Clay, _manager.Clay },
            };

            for (int i = 0; i < resources.Count; i++) {
                KeyValuePair<int, int> resource =
                    resources.ElementAt(i);
                int resource_id = resource.Key;
                int resource_quantity = resource.Value;
                _resources_labels[resource_id].Text = resource_quantity.ToString();
            }
        }

        private void showResources() {
            Panel resources_panel = createResourcesPanel();
            int resources_panel_x = HORIZONTAL_MARGIN +
                (Width - RESOURCES_PANEL_SIZE.Width) / 2;
            resources_panel.Location =
                    new Point(resources_panel_x, RESOURCES_PANEL_Y);
            Controls.Add(resources_panel);
            updateResources();
        }

        private void buildings_button_Click(object sender, EventArgs e) {
            _menu.hide();
            ShowBuildings?.Invoke(this, EventArgs.Empty);
        }

        private void workers_button_Click(object sender, EventArgs e) {
            _menu.hide();
            ShowWorkers?.Invoke(this, EventArgs.Empty);
        }

        /*
        private void onPropertyChanged(Object sender, string? propertyName)
        {
            PropertyChanged?.Invoke(sender,
                new PropertyChangedEventArgs(propertyName));
        } 
        */

    }
}

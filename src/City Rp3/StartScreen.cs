namespace City_Rp3 {
    public partial class StartScreen : Form {
        //private Permanent _permanent;
        private Window _ParentWindow {
            get => (Window)MdiParent;
        }

        public event EventHandler<EventArgs>? onStartGame;
        public event EventHandler<EventArgs>? onLoadGame;

        public StartScreen() {
            InitializeComponent();

            //_permanent = permanent;
        }

        private void loadGame() {
            /*
            string map_data = _permanent.getMap();
            string workers_data = _permanent.getWorkers();
            string resources_data = _permanent.getResources();
            string upgrades_data = _permanent.getUpgrades();

            map.load(map_data);
            workers.load(workers_data);
            resources.load(resources_data);
            upgrades.load(upgrades_data);
            */
        }

        private void new_game_button_Click(object sender, EventArgs e) {
            onStartGame?.Invoke(this, EventArgs.Empty);
            _ParentWindow.showScreen("game");
        }

        private void load_game_button_Click(object sender, EventArgs e) {
            onLoadGame?.Invoke(this, EventArgs.Empty);
            _ParentWindow.showScreen("game");

        }

        private void quit_button_Click(object sender, EventArgs e) {
            _ParentWindow.Close();
        }
    }
}

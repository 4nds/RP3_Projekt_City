// Klasa GameMenuContent 
//
// kontrola u kojoj je sadržaj izbornika igre
// (klasa se zove isključivo iz klase GameMenu)

using System.ComponentModel;

namespace City_Rp3 {
    internal partial class GameMenuContent : UserControl, INotifyPropertyChanged {
        private readonly Menu _menu;
        //private Manager _manager;
        //private Map _map;
        //private Soldiers _soldiers;
        //private Wolves _wolves;
        //private Workers _workers;

        public event EventHandler<EventArgs>? Quit;
        public event EventHandler<EventArgs>? SaveGame;

        public event PropertyChangedEventHandler? PropertyChanged;

        public GameMenuContent(Menu menu) {
            InitializeComponent();

            _menu = menu;
        }

        private void resume_button_Click(object sender, EventArgs e) {
            _menu.hide();
        }

        private void save_button_Click(object sender, EventArgs e) {
            SaveGame?.Invoke(this, EventArgs.Empty);
            _menu.hide();
        }

        private void quit_button_Click(object sender, EventArgs e) {
            //SaveGame?.Invoke(this, EventArgs.Empty);
            _menu.hide();
            Quit?.Invoke(this, EventArgs.Empty);
        }

        /*
        private void onPropertyChanged(Object sender, string? propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
        */

    }
}

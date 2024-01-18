// Klasa Menu 
//
// bazna klasa za menijeve
//
// UserControl Content - kontrola u kojoj je sadržaj menija
// bool Visible - svojstvo koje govori je li meni vidljiv na ekranu
// Size Size - veličina menija
// void show(Point location) - metoda za pokazivanje menija na zadanoj lokaciji
// void show(Point location, (Point, Point) do_not_cover) - metoda za pokazivanje menija na zadanoj lokaciji,
//     do_not_cover predstavlja pravokutnik kojeg meni neće prekrit


using System.ComponentModel;

namespace City_Rp3 {

    public class ShowMenuEventArgs : EventArgs {
        public Point Location { get; set; }

        public (Point, Point) Do_not_cover { get; set; }

        public ShowMenuEventArgs(Point location,
            (Point, Point) do_not_cover) {
            Location = location;
            Do_not_cover = do_not_cover;
        }
    }

    public class Menu : INotifyPropertyChanged {
        private MenuContainer _container;
        private bool _visible;

        protected Form _screen;
        protected UserControl _content;

        protected IMenuContainer _Container {
            get => _container;
            set => _container = (MenuContainer)value;
        }

        public UserControl Content {
            get => _content;
            set => _content = value;
        }

        internal Form _Screen {
            get => _screen;
        }

        public string title = "Menu";

        public bool Visible {
            get => _visible;
        }

        public Size Size {
            get => _container.Size;
        }

        public Point last_location = new(0, 0);
        public (Point, Point) last_do_not_cover = (Point.Empty, Point.Empty);

        public event PropertyChangedEventHandler? PropertyChanged;

        public void show(Point location) {
            last_location = location;
            show(location, (Point.Empty, Point.Empty));
        }

        protected virtual void onShow() {
        }

        public void show(Point location, (Point, Point) do_not_cover) {
            onShow();
            last_location = location;
            last_do_not_cover = do_not_cover;
            _container.show(location, do_not_cover);
            _visible = true;
        }

        public void hide() {
            onHide();
            _container.hide();
            _visible = false;
        }

        protected virtual void onHide() {
        }

        protected void addContentToContainer() {
            _content.Left = 0;
            _content.Top = _container.Title_bar_panel_height;
            _container.Controls.Add(_content);
        }

        protected void receiveChange(Object? sender, PropertyChangedEventArgs e) {
            PropertyChanged?.Invoke(sender,
                new PropertyChangedEventArgs(e.PropertyName));
        }

    }
}

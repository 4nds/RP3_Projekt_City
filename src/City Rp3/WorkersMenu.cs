// Klasa WorkersMenu 
//
// klasa za izbornik radnika
//
// WorkersMenu(Form screen, bool draggable = true) - konstruktor koji uzima formu screen na kojoj se prikazuje,
//     argumnet draggable govori je li se izbornik može povlačiti
// void mapClick((int x, int y) position, int id) - metoda koja se poziva kad je izbornik radnika prikazan na formi
//     te je kliknuta pozicija position na mapi, argument id predstavlja id kliknutog elementa na mapi

namespace City_Rp3 {
    public class WorkersMenu : Menu {
        public WorkersMenu(Form screen, bool draggable = true) {
            title = "Workers";
            _screen = screen;
            _content = new WorkersMenuContent(this);
            _Container = new MenuContainer(this, draggable) {
                //Size = new Size(200, 340),
                Size = new Size(_content.Size.Width, _content.Size.Height + 40),
            };
            ((WorkersMenuContent)_content).PropertyChanged +=
                (sender, e) => receiveChange(sender, e);
            addContentToContainer();
        }

        public void mapClick((int x, int y) position, int id) {
            ((WorkersMenuContent)_content).mapClick(position, id);
        }

        protected override void onShow() {
            ((WorkersMenuContent)_content).showWorkers();
        }

    }

}

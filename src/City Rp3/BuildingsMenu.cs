// Klasa BuildingsMenu 
//
// klasa za izbornik zgrada
//
// BuildingsMenu(Form screen, bool draggable = true) - konstruktor koji uzima formu screen na kojoj se prikazuje,
//     argumnet draggable govori je li se izbornik može povlačiti
// void mapClick((int x, int y) position, int id) - metoda koja se poziva kad je izbornik zgrada prikazan na formi
//     te je kliknuta pozicija position na mapi, argument id predstavlja id kliknutog elementa na mapi

namespace City_Rp3 {
    public class BuildingsMenu : Menu {
        public BuildingsMenu(Form screen, bool draggable = true) {
            title = "Buildings";
            _screen = screen;
            _content = new BuildingsMenuContent(this);
            _Container = new MenuContainer(this, draggable) {
                //Size = new Size(255, 340),
                Size = new Size(_content.Size.Width, _content.Size.Height + 40),
            };


            ((BuildingsMenuContent)_content).PropertyChanged +=
                (sender, e) => receiveChange(sender, e);
            addContentToContainer();
        }

        public void mapClick((int x, int y) position, int id) {
            ((BuildingsMenuContent)_content).mapClick(position, id);
        }

        protected override void onShow() {
            ((BuildingsMenuContent)_content).updateBuildButtons();
        }

    }
}

// Klasa SmithyMenu 
//
// klasa za izbornik kovačnice
//
// SmithyMenu(Form screen, bool draggable = true) - konstruktor koji uzima formu screen na kojoj se prikazuje,
//     argumnet draggable govori je li se izbornik može povlačiti

namespace City_Rp3 {
    public class SmithyMenu : Menu {
        public SmithyMenu(Form screen, bool draggable = true) {
            title = "Smithy";
            _screen = screen;
            _content = new SmithyMenuContent(this);
            _Container = new MenuContainer(this, draggable) {
                //Size = new Size(275, 445),
                Size = new Size(_content.Size.Width, _content.Size.Height + 40),
            };
            //((SmithyMenuContent)_content).onUpgradeWorkerEfficiency +=
            //    (sender, e) => resources.upgradeWorkerEfficiency();      
            ((SmithyMenuContent)_content).PropertyChanged +=
                (sender, e) => receiveChange(sender, e);
            addContentToContainer();
        }

        protected override void onShow() {
            ((SmithyMenuContent)_content).updateUpgradeButtons();
        }

    }

}

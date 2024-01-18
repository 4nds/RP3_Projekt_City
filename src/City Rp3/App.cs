using System.ComponentModel;
using System.Diagnostics;

namespace City_Rp3 {
    public class App {
        private const int TILE_SIZE = 35;

        private Window _window;

        private Manager _manager;
        private Map _map;
        private Soldiers _soldiers;
        private Wolves _wolves;
        private Workers _workers;

        private Manager? _start_manager;
        private Map? _start_map;
        private Soldiers? _start_soldiers;
        private Wolves? _start_wolves;
        private Workers? _start_workers;

        private Manager? _load_manager;
        private Map? _load_map;
        private Soldiers? _load_soldiers;
        private Wolves? _load_wolves;
        private Workers? _load_workers;

        public App() {
            _window = new();

            _manager = new();
            _map = new();
            _soldiers = new();
            _wolves = new();
            _workers = new();

            _start_manager = new();
            _start_map = new();
            _start_soldiers = new();
            _start_wolves = new();
            _start_workers = new();

            _load_manager = new();
            _load_map = new();
            _load_soldiers = new();
            _load_wolves = new();
            _load_workers = new();

            //tempCreateFiles();
        }

        private void tempCreateFiles() {
            string manager_data = _manager.dump();
            string map_data = _map.dump();
            string soldiers_data = _soldiers.dump();
            string workers_data = _workers.dump();
            string wolves_data = _wolves.dump();

            Saving.saveManager(manager_data);
            //Debug.WriteLine("");
            //Debug.WriteLine("save manager_data");
            //Debug.WriteLine(manager_data);
            //Debug.WriteLine("");
            Saving.saveMap(map_data);
            Saving.saveSoldier(soldiers_data);
            Saving.saveWorker(workers_data);
            Saving.saveWolf(wolves_data);
        }

        public Window setUp() {
            _window.ScreenShown += (sender, screen_name) => {
                if (screen_name == "start") {
                    setUpStartData();
                    setUpLoadData();
                }
            };

            addScreens();
            addMenus();

            _window.showScreen("start");

            return _window;
        }

        private void addScreens() {
            StartScreen start_screen = new();
            start_screen.onStartGame += (sender, e) => startGame();
            start_screen.onLoadGame += (sender, e) => loadGame();
            _window.addScreen("start", start_screen);

            Game game_screen = new();
            game_screen.PropertyChanged += receiveGameChange;
            _window.addScreen("game", game_screen);
        }

        private static (Point, Point) getDoNotCover(int full_id, (int x, int y) position) {
            int building_id = Constants.toSingleBuildingId(full_id);
            (int building_columns, int building_rows) = Constants.BuildingSize(building_id);
            int building_width = TILE_SIZE * building_columns;
            int building_height = TILE_SIZE * building_rows;
            Point top_left, bottom_right;

            string tile_type = full_id switch {
                Constants.MainBuilding11 => "1_1",
                Constants.MainBuilding12 => "1_2",
                Constants.MainBuilding21 => "2_1",
                Constants.MainBuilding22 => "2_2",
                Constants.Smithy11 => "1_1",
                Constants.Smithy12 => "1_2",
                Constants.Armory11 => "1_1",
                Constants.Armory12 => "1_2",
                _ => throw new ArgumentException($"Not supported building id ({full_id})"),
            };

            switch (tile_type) {
                case "1_1":
                    top_left = new(TILE_SIZE * position.x,
                             TILE_SIZE * position.y);
                    bottom_right = new(top_left.X + building_width,
                        top_left.Y + building_height);
                    return (top_left, bottom_right);
                case "1_2":
                    top_left = new(TILE_SIZE * (position.x - 1),
                             TILE_SIZE * position.y);
                    bottom_right = new(top_left.X + building_width,
                        top_left.Y + building_height);
                    return (top_left, bottom_right);
                case "2_1":
                    top_left = new(TILE_SIZE * position.x,
                             TILE_SIZE * (position.y - 1));
                    bottom_right = new(top_left.X + building_width,
                        top_left.Y + building_height);
                    return (top_left, bottom_right);
                case "2_2":
                    top_left = new(TILE_SIZE * (position.x - 1),
                             TILE_SIZE * (position.y - 1));
                    bottom_right = new(top_left.X + building_width,
                        top_left.Y + building_height);
                    return (top_left, bottom_right);
                default:
                    throw new ArgumentException($"Not supported tile type ({tile_type})");
            }
        }

        private void addMenus() {
            Game game = (Game)_window.Screens["game"];

            _window.MenuShown += (sender, menu_name) => {
                if (menu_name == "game") game.timer1.Enabled = false;
            };

            GameMenu game_menu = new(_window.Screens["game"], false);
            game_menu.Quit += (sender, e) => resetGame();
            game_menu.SaveGame += (sender, e) => saveGame();
            game_menu.ResumeGame += (sender, e) => game.timer1.Enabled = true;
            _window.addMenu("game", game_menu);

            BuildingsMenu buildings_menu =
                new(_window.Screens["game"], true);
            buildings_menu.PropertyChanged +=
                (sender, e) => receiveChange(sender, e);
            _window.addMenu("buildings", buildings_menu);

            WorkersMenu workers_menu =
                new(_window.Screens["game"], true);
            workers_menu.PropertyChanged +=
                (sender, e) => receiveChange(sender, e);
            _window.addMenu("workers", workers_menu);


            MainBuildingMenu main_building_menu =
                new(_window.Screens["game"], true);
            main_building_menu.ShowBuildings += (sender, e) =>
                _window.showMenu("buildings", e.Location, e.Do_not_cover);
            main_building_menu.ShowWorkers += (sender, e) =>
                _window.showMenu("workers", e.Location, e.Do_not_cover);
            _window.addMenu("main building", main_building_menu);

            SmithyMenu smithy_menu =
                new(_window.Screens["game"], true);
            smithy_menu.PropertyChanged +=
                (sender, e) => receiveChange(sender, e);
            _window.addMenu("smithy", smithy_menu);

            ArmoryMenu armory_menu =
                new(_window.Screens["game"], true);
            armory_menu.PropertyChanged +=
                (sender, e) => receiveChange(sender, e);
            _window.addMenu("armory", armory_menu);



            game.MapClick += (sener, position) => {
                int full_id = _map.get(position);
                int id = Constants.toSingleBuildingId(full_id);
                if (_window.isMenuVisible("main building")) {
                }
                else if (_window.isMenuVisible("buildings")) {
                    buildings_menu.mapClick(position, id);
                }
                else if (_window.isMenuVisible("workers")) {
                    workers_menu.mapClick(position, id);
                }
                else {

                    Point location = new(TILE_SIZE * position.x,
                        TILE_SIZE * position.y);
                    (Point, Point) do_not_cover;
                    switch (id) {
                        case Constants.MainBuilding:
                            do_not_cover = getDoNotCover(full_id, position);
                            _window.showMenu("main building", location, do_not_cover);
                            break;
                        case Constants.Smithy:
                            do_not_cover = getDoNotCover(full_id, position);
                            _window.showMenu("smithy", location, do_not_cover);
                            break;
                        case Constants.Armory:
                            do_not_cover = getDoNotCover(full_id, position);
                            _window.showMenu("armory", location, do_not_cover);
                            break;
                        default:
                            if (!Constants.isValidId(id)) {
                                throw new ArgumentException($"Not supported building id ({id})");
                            }
                            break;
                    }
                }
            };

        }

        private void setUpStartData() {
            _start_map = new Map(Randomize.RandomizeMap());

            if (_start_map is Map start_map) {

                _start_workers?.add((11, 10), _manager.Soldier_defence());
                _start_workers?.setHomePos((10, 10), 0);

                _start_workers?.add((10, 11), _manager.Soldier_defence());
                _start_workers?.setHomePos((10, 10), 1);

            }

            if (_start_manager is Manager start_manager) {

                start_manager.Wood = 0;
                start_manager.Wheat = 0;
                start_manager.Iron = 0;
                start_manager.Stone = 0;
                start_manager.Clay = 0;

            }
        }

        private void startGame() {
            if (_start_manager is Manager start_manager) {
                _manager = new(_start_manager);
                sendChange("Manager");
            }
            if (_start_map is Map start_map) {
                _map = new(start_map);
                sendChange("Map");
            }
            if (_start_soldiers is Soldiers start_soldiers) {
                _soldiers = new(start_soldiers);
                sendChange("Soldiers");
            }
            if (_start_wolves is Wolves start_wolves) {
                _wolves = new(start_wolves);
                sendChange("Wolves");
            }
            if (_start_workers is Workers start_workers) {
                _workers = new(start_workers);
                sendChange("Workers");
            }

            _start_manager = null;
            _start_map = null;
            _start_soldiers = null;
            _start_wolves = null;
            _start_workers = null;

            ((Game)_window.Screens["game"]).timer1.Enabled = true;
        }

        private void setUpLoadData() {
            if (Saving.managerDataExists() || Saving.mapDataExists()
                || Saving.soldierDataExists() || Saving.soldierDataExists()
                || Saving.wolfDataExists() || Saving.workerDataExists()) {
                string manager_data = Saving.getManager();
                _load_manager = new(manager_data);

                string map_data = Saving.getMap();
                _load_map = new(map_data);

                if (_load_map is Map load_map) {
                    string soldiers_data = Saving.getSoldier();
                    _load_soldiers = new(soldiers_data, load_map);

                    string wolves_data = Saving.getWolf();
                    _load_wolves = new(wolves_data, load_map);

                    string workers_data = Saving.getWorker();
                    _load_workers = new(workers_data, load_map);
                }

                ((StartScreen)_window.Screens["start"])
                    .load_game_button.Enabled = true;
            }
            else {
                ((StartScreen)_window.Screens["start"])
                    .load_game_button.Enabled = false;
            }
        }

        private void loadGame() {
            if (_load_manager is Manager load_manager) {
                _manager = new(load_manager);
                sendChange("Manager");
            }
            if (_load_map is Map load_map) {
                _map = new(load_map);
                sendChange("Map");
            }
            if (_load_soldiers is Soldiers load_soldiers) {
                _soldiers = new(load_soldiers);
                sendChange("Soldiers");
            }
            if (_load_wolves is Wolves load_wolves) {
                _wolves = new(load_wolves);
                sendChange("Wolves");
            }
            if (_load_workers is Workers load_workers) {
                _workers = new(load_workers);
                sendChange("Workers");
            }

            _load_manager = null;
            _load_map = null;
            _load_soldiers = null;
            _load_wolves = null;
            _load_workers = null;

            ((Game)_window.Screens["game"]).timer1.Enabled = true;
        }

        private void saveGame() {
            string manager_data = _manager.dump();
            string map_data = _map.dump();
            string soldiers_data = _soldiers.dump();
            string wolves_data = _wolves.dump();
            string workers_data = _workers.dump();

            Saving.saveManager(manager_data);
            Saving.saveMap(map_data);
            Saving.saveSoldier(soldiers_data);
            Saving.saveWolf(wolves_data);
            Saving.saveWorker(workers_data);
        }

        private void resetGame() {
            _manager = new();
            _map = new();
            _soldiers = new();
            _wolves = new();
            _workers = new();

            _start_manager = new();
            _start_map = new();
            _start_soldiers = new();
            _start_wolves = new();
            _start_workers = new();

            _load_manager = new();
            _load_map = new();
            _load_soldiers = new();
            _load_wolves = new();
            _load_workers = new();

            GC.Collect();


            sendChange("Manager");
            sendChange("Map");
            sendChange("Soldiers");
            sendChange("Wolves");
            sendChange("Workers");

            _window.showScreen("start");
        }

        public void receiveGameChange(Object? sender, PropertyChangedEventArgs e) {
            if (sender != null) {
                Debug.WriteLine($"receiveGameChange {e.PropertyName}");
                Object sender_property = e.PropertyName switch {
                    "Manager" => ((Game)sender).Manager,
                    "Map" => ((Game)sender).Map,
                    "Workers" => ((Game)sender).Workers,
                    "Soldiers" => ((Game)sender).Soldiers,
                    "Wolves" => ((Game)sender).Wolves,
                    _ => throw new ArgumentException($"Not supported  property {e.PropertyName} of Game class"),
                };
                receiveChange(sender_property, e);
            }
        }

        public void receiveChange(Object? sender, PropertyChangedEventArgs e) {
            if (sender != null) {
                switch (e.PropertyName) {
                    case "Manager":
                        if (!_manager.Cmp((Manager)sender)) _manager = new Manager(((Manager)sender));
                        break;
                    case "Map":
                        if (!_map.Cmp((Map)sender)) _map = new Map(((Map)sender));
                        break;
                    case "Workers":
                        if (!_workers.Cmp((Workers)sender)) _workers = new Workers(((Workers)sender));
                        break;
                    case "Soldiers":
                        if (!_soldiers.Cmp((Soldiers)sender)) _soldiers = new Soldiers(((Soldiers)sender));
                        break;
                    case "Wolves":
                        if (!_wolves.Cmp((Wolves)sender)) _wolves = new Wolves(((Wolves)sender));
                        break;
                    default:
                        throw new ArgumentException($"Not supported PropertyName ({e.PropertyName})");
                }
                sendChange(e.PropertyName);
            }
        }

        private void sendChange(string property_name) {
            Game game = (Game)_window.Screens["game"];
            GameMenuContent game_menu_content =
                (GameMenuContent)_window.Menus["game"].Content;
            BuildingsMenuContent buildings_menu_content =
                (BuildingsMenuContent)_window.Menus["buildings"].Content;
            WorkersMenuContent workers_menu_content =
                (WorkersMenuContent)_window.Menus["workers"].Content;
            MainBuildingMenuContent main_building_menu_content =
                (MainBuildingMenuContent)_window.Menus["main building"].Content;
            SmithyMenuContent smithy_menu_content =
                (SmithyMenuContent)_window.Menus["smithy"].Content;
            ArmoryMenuContent armory_menu_content =
                (ArmoryMenuContent)_window.Menus["armory"].Content;

            switch (property_name) {
                case "Manager":
                    game.Manager = _manager;
                    //game_menu_content.Manager = _manager;
                    buildings_menu_content.Manager = _manager;
                    workers_menu_content.Manager = _manager;
                    main_building_menu_content.Manager = _manager;
                    smithy_menu_content.Manager = _manager;
                    armory_menu_content.Manager = _manager;
                    break;
                case "Map":
                    game.Map = _map;
                    //game_menu_content.Map = _map;
                    buildings_menu_content.Map = _map;
                    workers_menu_content.Map = _map;
                    //main_building_menu_content.Map = _map;
                    //smithy_menu_content.Map = _map;
                    armory_menu_content.Map = _map;
                    break;
                case "Workers":
                    game.Workers = _workers;
                    //game_menu_content.Workers = _workers;
                    buildings_menu_content.Workers = _workers;
                    workers_menu_content.Workers = _workers;
                    //main_building_menu_content.Workers = _workers;
                    //smithy_menu_content.Workers = _workers;
                    //armory_menu_content.Workers = _workers;
                    break;
                case "Soldiers":
                    game.Soldiers = _soldiers;
                    //game_menu_content.Soldiers = _soldiers;
                    buildings_menu_content.Soldiers = _soldiers;
                    //workers_menu_content.Soldiers = _soldiers;
                    //main_building_menu_content.Soldiers = _soldiers;
                    //smithy_menu_content.Soldiers = _soldiers;
                    armory_menu_content.Soldiers = _soldiers;
                    break;
                case "Wolves":
                    game.Wolves = _wolves;
                    //game_menu_content.Wolves = _wolves;
                    buildings_menu_content.Wolves = _wolves;
                    //workers_menu_content.Wolves = _wolves;
                    //main_building_menu_content.Wolves = _wolves;
                    //smithy_menu_content.Wolves = _wolves;
                    //armory_menu_content.Wolves = _wolves;
                    break;
                default:
                    throw new ArgumentException($"Not supported PropertyName ({property_name})");
            }
        }

    }
}

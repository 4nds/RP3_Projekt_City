namespace City_Rp3 {
    public class Manager {
        //pratiti resurse, oduzimati nakon gradnje, pratiti koliko radnik donosi
        //postavljamo sve resurse na početnu vrijednost
        private int wood = 0;
        private int wheat = 0;
        private int stone = 0;
        private int iron = 0;
        private int clay = 0;

        //postavljamo početne vrijednosti jačine napada vukova i zdravlje vukova
        private int wolf_attack = 10;
        private int wolf_health = 10;

        //postavljamo početnu učinkovitost radnika dok skuplja pojedini resurs
        private int worker_efficiency_wood = 10;
        private int worker_efficiency_wheat = 10;
        private int worker_efficiency_stone = 10;
        private int worker_efficiency_iron = 10;
        private int worker_efficiency_clay = 10;

        //postavljamo početne vrijednosti jačine napada vojnika i obrane bojnika
        private int soldier_attack = 10;
        private int soldier_defence = 10;

        public Manager() {
        }

        //copy constructor za Manager
        public Manager(Manager m) {
            wood = m.wood;
            wheat = m.wheat;
            stone = m.stone;
            iron = m.iron;
            clay = m.clay;

            wolf_attack = m.wolf_attack;
            wolf_health = m.wolf_health;

            worker_efficiency_wood = m.worker_efficiency_wood;
            worker_efficiency_wheat = m.worker_efficiency_wheat;
            worker_efficiency_stone = m.worker_efficiency_stone;
            worker_efficiency_iron = m.worker_efficiency_iron;
            worker_efficiency_clay = m.worker_efficiency_clay;

            soldier_attack = m.soldier_attack;
            soldier_defence = m.soldier_defence;


        }

        //funkcija dump koja sprema trenutno stanje klase u string 
        public string dump() {
            string result = "MANAGER,";
            result += wood + "," + wheat + "," + stone + "," + iron + "," + clay + ",";
            result += wolf_attack + "," + wolf_health + ",";
            result += worker_efficiency_wood + "," + worker_efficiency_wheat + "," + worker_efficiency_stone
                 + "," + worker_efficiency_iron + "," + worker_efficiency_clay + ",";
            result += soldier_attack + "," + soldier_defence;
            return result;
        }

        //konstrukntor koji iz stringa rekonstruira prijašnje stanje klase
        public Manager(string loadString) {
            string[] words = loadString.Split(',');
            wood = int.Parse(words[1]);
            wheat = int.Parse(words[2]);
            stone = int.Parse(words[3]);
            iron = int.Parse(words[4]);
            clay = int.Parse(words[5]);

            wolf_attack = int.Parse(words[6]);
            wolf_health = int.Parse(words[7]);

            worker_efficiency_wood = int.Parse(words[8]);
            worker_efficiency_wheat = int.Parse(words[9]);
            worker_efficiency_stone = int.Parse(words[10]);
            worker_efficiency_iron = int.Parse(words[11]);
            worker_efficiency_clay = int.Parse(words[12]);

            soldier_attack = int.Parse(words[13]);
            soldier_defence = int.Parse(words[14]);


        }

        //funkcija koja služi usporedbi dvije instance klase manager
        public bool Cmp(Manager m) {
            if (wood != m.wood) return false;
            if (wheat != m.wheat) return false;
            if (stone != m.stone) return false;
            if (iron != m.iron) return false;
            if (clay != m.clay) return false;

            if (wolf_attack != m.wolf_attack) return false;
            if (wolf_health != m.wolf_health) return false;

            if (worker_efficiency_wood != m.worker_efficiency_wood) return false;
            if (worker_efficiency_wheat != m.worker_efficiency_wheat) return false;
            if (worker_efficiency_stone != m.worker_efficiency_stone) return false;
            if (worker_efficiency_iron != m.worker_efficiency_iron) return false;
            if (worker_efficiency_clay != m.worker_efficiency_clay) return false;

            if (soldier_attack != m.soldier_attack) return false;
            if (soldier_defence != m.soldier_defence) return false;

            return true;
        }

        /*klasa/klase koje čuvaju broj resursa u banci, nivoe poboljšanja iz kovačnice, snaga vukova
        ima metode za izgradnju zgrada dobije x, y, zgradu (oduzme resurse, promijeni kartu)*/
        //svojstva
        public int Wood {
            get {
                return this.wood;
            }
            set {
                wood = value;

            }
        }
        public int Wheat {
            get {
                return this.wheat;
            }
            set {
                wheat = value;

            }
        }
        public int Stone {
            get {
                return this.stone;
            }
            set {
                stone = value;

            }
        }
        public int Iron {
            get {
                return this.iron;
            }
            set {
                iron = value;

            }
        }
        public int Clay {
            get {
                return this.clay;
            }
            set {
                clay = value;

            }
        }
        /*
        *ima metodu za poboljšanja(ulaz ime_poboljšanje) - oduzme resurse
         * sjeća drva, kopanje....
         * obrana
         */

        //metode za izgradnju građevina: farme, rudnika, glinokopa, wondera
        public bool build_farm() {
            int building = Constants.Farm;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool build_mine() {
            int building = Constants.Mine;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool build_clayworks() {
            int building = Constants.Clayworks;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool build_stockpile() {
            int building = Constants.Stockpile;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool build_smithy() {
            int building = Constants.Smithy;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool build_armory() {
            int building = Constants.Armory;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool build_wonder() {
            int building = Constants.Wonder;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }

        /*daj mi nivo poboljšanja, daj mi koliko imam drva...*/
        //funkcije vraćaju zdravlje i napad vukova i vojnika, te učinkovitost radnika
        public int Wolf_attack() {
            return this.wolf_attack;
        }
        public int Wolf_health() {
            return this.wolf_health;
        }
        public int Worker_efficiency_wood() {
            return this.worker_efficiency_wood;
        }
        public int Worker_efficiency_wheat() {
            return this.worker_efficiency_wheat;
        }
        public int Worker_efficiency_iron() {
            return this.worker_efficiency_iron;
        }
        public int Worker_efficiency_clay() {
            return this.worker_efficiency_clay;
        }
        public int Worker_efficiency_stone() {
            return this.worker_efficiency_stone;
        }
        public int Soldier_attack() {
            return this.soldier_attack;
        }
        public int Soldier_defence() {
            return this.soldier_defence;
        }
        /*poveća poboljšanje u klasi*/
        //metode koje služe za povećaje učinkoitosti radnika pri sakupljanju jedne vrste resursa
        //ili zdravlja i napada vukova 

        public bool build_worker() {
            int building = Constants.Worker;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool build_soldier() {
            int building = Constants.Soldier;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool boost_efficiency_wood() {
            int building = Constants.BoostWood;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                worker_efficiency_wood += 10;
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool boost_efficiency_wheat() {
            int building = Constants.BoostWheat;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                worker_efficiency_wheat += 10;
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool boost_efficiency_stone() {
            int building = Constants.BoostStone;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                worker_efficiency_stone += 10;
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool boost_efficiency_iron() {
            int building = Constants.BoostIron;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                worker_efficiency_iron += 10;
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool boost_efficiency_clay() {
            int building = Constants.BoostClay;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                worker_efficiency_clay += 10;
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool boost_wolf_attack() {
            wolf_attack += 10;

            return true;
        }
        public bool boost_wolf_health() {
            wolf_health += 10;

            return true;
        }
        public bool boost_soldier_attack() {

            int building = Constants.BoostAttack;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                soldier_attack += 10;
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
        public bool boost_soldier_defence() {
            int building = Constants.BoostHealth;
            if (wood >= Constants.getCost(building).Wood &&
                wheat >= Constants.getCost(building).Wheat &&
                stone >= Constants.getCost(building).Stone &&
                iron >= Constants.getCost(building).Iron &&
                clay >= Constants.getCost(building).Clay
                ) {
                soldier_defence += 10;
                wood -= Constants.getCost(building).Wood;
                stone -= Constants.getCost(building).Stone;
                iron -= Constants.getCost(building).Iron;
                clay -= Constants.getCost(building).Clay;
                wheat -= Constants.getCost(building).Wheat;

                return true;
            }
            return false;
        }
    }

}

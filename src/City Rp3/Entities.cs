//Klasa Entities
//
//predložak za ostale klase, njezine metode naslijeđuju, ne koristiti samu
//
//freeID prati koji slijedeći id treba dodijeliti entitetu kojeg stvaramo
//Ima OnPropertyChanged handler koji služi za javljanje roditeljskim klasama da se nešto mijenjalo

public abstract class Entities {
    protected class Entity {
        public int pos_x;
        public int pos_y;
        public int? des_x;
        public int? des_y;
        public int? home_x;
        public int? home_y;
        public int health;
        public List<(int, int)> path = new List<(int, int)>();
    }
    protected int freeID;
    public Entities() {
        freeID = 0;
    }

}

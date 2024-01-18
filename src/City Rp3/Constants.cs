//Klasa koja sasdrži sve konstante koje se koriste u programu te jednostavne observatore za njih
//Sadrži i sve slike koje se crtaju, tako da se samo jednom loadaju s obzirom da je to iznimno skupo
//int toSingleBuildingId(int id) - dobiva id od djelića zgrade i vraća id od cijele zgrade, ako dobije išta drugo reflektira
//(int width, int height) BuildingSize(int id) - dobiva id od zgrade i vraća njezine dimenzije u tuple-u
//Image getImageById(int asset_id) dobiva id i vraća pripasnu sliku za crtanje
//static bool isValidId(int id) - vraća true ako je Id u klasi, inače false
static class Constants {
    public const int Error = -1;
    public const int Grass = 0;
    public const int Wood = 1;
    public const int Wheat = 2;
    public const int Stone = 3;
    public const int Iron = 4;
    public const int Water = 5;
    public const int Clay = 6;
    public const int Worker = 7;
    public const int Soldier = 8;
    public const int Wolf = 9;
    public const int MainBuilding = 10;
    public const int Farm = 11;
    public const int Mine = 12;
    public const int Clayworks = 13;
    public const int Wonder = 14;
    public const int Stockpile = 15;
    public const int Smithy = 16;
    public const int Armory = 17;

    public const int Armory11 = 18;
    public const int Armory12 = 19;
    public const int Clayworks11 = 20;
    public const int Clayworks12 = 21;
    public const int Farm11 = 22;
    public const int Farm12 = 23;
    public const int Farm13 = 24;
    public const int Farm21 = 25;
    public const int Farm22 = 26;
    public const int Farm23 = 27;
    public const int Farm31 = 28;
    public const int Farm32 = 29;
    public const int Farm33 = 30;
    public const int MainBuilding11 = 31;
    public const int MainBuilding12 = 32;
    public const int MainBuilding21 = 33;
    public const int MainBuilding22 = 34;
    public const int Mine11 = 35;
    public const int Mine12 = 36;
    public const int Mine21 = 37;
    public const int Mine22 = 38;
    public const int Smithy11 = 39;
    public const int Smithy12 = 40;
    public const int Wonder11 = 41;
    public const int Wonder12 = 42;
    public const int Wonder13 = 43;
    public const int Wonder21 = 44;
    public const int Wonder22 = 45;
    public const int Wonder23 = 46;
    public const int Wonder31 = 47;
    public const int Wonder32 = 48;
    public const int Wonder33 = 49;

    public const int BoostWood = 50;
    public const int BoostWheat = 51;
    public const int BoostStone = 52;
    public const int BoostIron = 53;
    public const int BoostClay = 54;
    public const int BoostAttack = 55;
    public const int BoostHealth = 56;


    public readonly static Image img_grass = Image.FromFile(Application.StartupPath + @"..\..\..\img\grass.png");
    public readonly static Image img_wood = Image.FromFile(Application.StartupPath + @"..\..\..\img\wood.png");
    public readonly static Image img_wheat = Image.FromFile(Application.StartupPath + @"..\..\..\img\wheat.png");
    public readonly static Image img_stone = Image.FromFile(Application.StartupPath + @"..\..\..\img\rock.png");
    public readonly static Image img_iron = Image.FromFile(Application.StartupPath + @"..\..\..\img\iron.png");
    public readonly static Image img_water = Image.FromFile(Application.StartupPath + @"..\..\..\img\water.png");
    public readonly static Image img_clay = Image.FromFile(Application.StartupPath + @"..\..\..\img\clay.png");
    public readonly static Image img_worker = Image.FromFile(Application.StartupPath + @"..\..\..\img\worker.png");
    public readonly static Image img_soldier = Image.FromFile(Application.StartupPath + @"..\..\..\img\soldier.png");
    public readonly static Image img_wolf = Image.FromFile(Application.StartupPath + @"..\..\..\img\wolf.png");
    public readonly static Image img_mainBuilding = Image.FromFile(Application.StartupPath + @"..\..\..\img\mainBuilding.png");
    public readonly static Image img_farm = Image.FromFile(Application.StartupPath + @"..\..\..\img\farm.png");
    public readonly static Image img_mine = Image.FromFile(Application.StartupPath + @"..\..\..\img\mine.png");
    public readonly static Image img_clayworks = Image.FromFile(Application.StartupPath + @"..\..\..\img\clayworks.png");
    public readonly static Image img_wonder = Image.FromFile(Application.StartupPath + @"..\..\..\img\wonder.png");
    public readonly static Image img_stockpile = Image.FromFile(Application.StartupPath + @"..\..\..\img\stockpile.png");
    public readonly static Image img_smithy = Image.FromFile(Application.StartupPath + @"..\..\..\img\smithy.png");
    public readonly static Image img_armory = Image.FromFile(Application.StartupPath + @"..\..\..\img\armory.png");


    public readonly static Image[,] img_armory_mul = new Image[,] {
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\armoury1.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\armoury2.png")
        }
    };
    public readonly static Image[,] img_clayworks_mul = new Image[,] {
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\clayworks1.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\clayworks2.png")
        }
    };
    public readonly static Image[,] img_farm_mul = new Image[,] {
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\farm11.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\farm12.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\farm13.png")
        },
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\farm21.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\farm22.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\farm23.png")
        },
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\farm31.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\farm32.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\farm33.png")
        }
    };
    public readonly static Image[,] img_mainBuilding_mul = new Image[,] {
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\mainBuilding11.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\mainBuilding12.png")
        },
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\mainBuilding21.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\mainBuilding22.png")
        }
    };
    public readonly static Image[,] img_mine_mul = new Image[,] {
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\mine11.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\mine12.png")
        },
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\mine21.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\mine22.png")
        }
    };
    public readonly static Image[,] img_smithy_mul = new Image[,] {
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\smithy1.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\smithy2.png")
        }
    };
    public readonly static Image[,] img_wonder_mul = new Image[,] {
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\wonder11.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\wonder12.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\wonder13.png")
        },
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\wonder21.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\wonder22.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\wonder23.png")
        },
        {
            Image.FromFile(Application.StartupPath + @"..\..\..\img\wonder31.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\wonder32.png"),
            Image.FromFile(Application.StartupPath + @"..\..\..\img\wonder33.png")
        }
    };

    public static (int Wood, int Wheat, int Stone, int Iron, int Clay) getCost(int id) {
        return id switch {
            MainBuilding => (0, 0, 0, 0, 0),
            Farm => (200, 0, 0, 0, 0),
            Mine => (200, 0, 100, 0, 0),
            Clayworks => (500, 0, 200, 100, 0),
            Wonder => (10000, 10000, 10000, 10000, 10000),
            Stockpile => (400, 0, 0, 0, 100),
            Smithy => (0, 0, 200, 300, 0),
            Armory => (0, 0, 200, 100, 0),
            BoostWood => (500, 0, 0, 0, 0),
            BoostWheat => (0, 500, 0, 0, 0),
            BoostStone => (0, 0, 500, 0, 0),
            BoostIron => (0, 0, 0, 500, 0),
            BoostClay => (0, 0, 0, 0, 500),
            BoostAttack => (0, 0, 0, 500, 0),
            BoostHealth => (0, 0, 0, 500, 0),
            Worker => (0, 100, 0, 0, 0),
            Soldier => (0, 100, 0, 50, 0),
            _ => throw new ArgumentException("Wrong id"),
        };
    }

    public static int toSingleBuildingId(int id) {
        if (id >= Armory11 && id <= Armory12) return Armory;
        if (id >= Clayworks11 && id <= Clayworks12) return Clayworks;
        if (id >= Farm11 && id <= Farm33) return Farm;
        if (id >= MainBuilding11 && id <= MainBuilding22) return MainBuilding;
        if (id >= Mine11 && id <= Mine22) return Mine;
        if (id >= Smithy11 && id <= Smithy12) return Smithy;
        if (id >= Wonder11 && id <= Wonder33) return Wonder;
        if (id == Stockpile) return Stockpile;
        return id;
    }
    static public (int width, int height) BuildingSize(int id) {
        return id switch {
            MainBuilding => (2, 2),
            Farm => (3, 3),
            Mine => (2, 2),
            Clayworks => (2, 1),
            Wonder => (3, 3),
            Stockpile => (1, 1),
            Smithy => (2, 1),
            Armory => (2, 1),
            _ => throw new ArgumentException("Wrong id"),
        };
    }

    public static Image getImageById(int asset_id) {
        return asset_id switch {
            Grass => img_grass,
            Wood => img_wood,
            Wheat => img_wheat,
            Stone => img_stone,
            Iron => img_iron,
            Water => img_water,
            Clay => img_clay,
            Soldier => img_soldier,
            Wolf => img_wood,
            MainBuilding => img_mainBuilding,
            Farm => img_farm,
            Mine => img_mine,
            Clayworks => img_clayworks,
            Wonder => img_wonder,
            Stockpile => img_stockpile,
            Smithy => img_smithy,
            Armory => img_armory,
            Armory11 => img_armory_mul[0, 0],
            Armory12 => img_armory_mul[0, 1],
            Farm11 => img_farm_mul[0, 0],
            Farm12 => img_farm_mul[0, 1],
            Farm13 => img_farm_mul[0, 2],
            Farm21 => img_farm_mul[1, 0],
            Farm22 => img_farm_mul[1, 1],
            Farm23 => img_farm_mul[1, 2],
            Farm31 => img_farm_mul[2, 0],
            Farm32 => img_farm_mul[2, 1],
            Farm33 => img_farm_mul[2, 2],
            MainBuilding11 => img_mainBuilding_mul[0, 0],
            MainBuilding12 => img_mainBuilding_mul[0, 1],
            MainBuilding21 => img_mainBuilding_mul[1, 0],
            MainBuilding22 => img_mainBuilding_mul[1, 1],
            Mine11 => img_mine_mul[0, 0],
            Mine12 => img_mine_mul[0, 1],
            Mine21 => img_mine_mul[1, 0],
            Mine22 => img_mine_mul[1, 1],
            Smithy11 => img_smithy_mul[0, 0],
            Smithy12 => img_smithy_mul[0, 1],
            Wonder11 => img_wonder_mul[0, 0],
            Wonder12 => img_wonder_mul[0, 1],
            Wonder13 => img_wonder_mul[0, 2],
            Wonder21 => img_wonder_mul[1, 0],
            Wonder22 => img_wonder_mul[1, 1],
            Wonder23 => img_wonder_mul[1, 2],
            Wonder31 => img_wonder_mul[2, 0],
            Wonder32 => img_wonder_mul[2, 1],
            Wonder33 => img_wonder_mul[2, 2],
            Clayworks11 => img_clayworks_mul[0, 0],
            Clayworks12 => img_clayworks_mul[0, 1],
            _ => throw new ArgumentException($"Unsupported asset_id {asset_id}."),
        };
    }

    public static bool isValidId(int id) {
        return id >= Grass && id <= Wonder33;
    }

}

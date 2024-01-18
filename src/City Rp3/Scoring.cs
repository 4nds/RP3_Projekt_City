using System.Diagnostics;

class Scoring {

    Stopwatch stopwatch;

    public Scoring() {
        stopwatch = new Stopwatch();
    }

    //vraca rezultat u sekundama
    public int get_score() {
        TimeSpan stopwatchElapsed = stopwatch.Elapsed;
        int value = Convert.ToInt32(stopwatchElapsed.TotalSeconds);
        return value;
    }

    //pocetak mjerenja 
    public void begin_time() {
        stopwatch.Start();
    }

    //kraj mjerenja
    public void end_time() {
        stopwatch.Stop();
    }
}

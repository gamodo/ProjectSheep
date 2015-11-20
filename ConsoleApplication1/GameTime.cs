using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSheep{
    class GameTime{
        Stopwatch watch;
        public TimeSpan Total { get; private set; }
        public TimeSpan Ellapsed { get; private set; }
        public GameTime(){
            watch = new Stopwatch();
            Total = new TimeSpan();
            Ellapsed = new TimeSpan();
            watch.Start();
        }
        public void Update(){
            Ellapsed = watch.Elapsed - Total;
            Total = watch.Elapsed;
        }
    }
}
//class GameTime
//{
//    Stopwatch watch;
//    public TimeSpan Ellapsed { get; private set; }
//    public TimeSpan Total { get; private set; }

//    public GameTime()
//    {
//        watch = new Stopwatch();
//        Ellapsed = new TimeSpan();
//        Total = new TimeSpan();

//        watch.Start();
//    }

//    public void Update()
//    {
//        Ellapsed = watch.Elapsed - Total;
//        Total = watch.Elapsed;
//    }
//}

using ScheduleApp.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp.controller
{
    public class CustomEvents
    {
        //salje koji korak je kompletiran, ako se to poklapa s korakom na kome se nalazi korisnik, onda omogucavamo nastavak tutoriala
        public static event EventHandler<int> TutorialStepCompleted;

        //u metodi koja kompletira korak pozovemo ovo cudo i prosledimo o kom se koraku radi
        public static void RaiseTutorialStepCompletedEvent(Steps step) {
            TutorialStepCompleted?.Invoke(null,(int)step);
        }
    }
}

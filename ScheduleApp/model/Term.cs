using System;

namespace ScheduleApp.model
{
    public enum WEEKDAY {MONDAY,TUESDAY,WEDNESDAY,THURSDAY,FRIDAY,SATURDAY} //ucionice ne rade nedeljom, pa tada nema termina
    //predstavlja jedan termin
    public class Term
    {
        public Classroom Classroom { get; set; }
        public Subject Subject { get; set; } //iz predmeta mozemo saznati minimalni duzine termina predmeta(u casovima od po 45 min) Broj termina koji predmet zahteva
        public TimeSpan Time { get; set;} //vreme kad pocinje
        public WEEKDAY Day { get; set; }
        public int Length { get; set; } //duzina u jedinicama od po 45 min
    }
}
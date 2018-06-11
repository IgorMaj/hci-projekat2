using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp.model
{
    public class Tutorial
    {
        public List<TutorialStep> TutorialSteps { get;}
        private int currentStepIndex;

        public int CurrentStepIndex { get { return currentStepIndex; } }

        public TutorialStep CurrentStep { get {
                if (TutorialSteps.Count == 0) {
                    return null;
                }
                return TutorialSteps[currentStepIndex];
            } }

        public Tutorial() {
            currentStepIndex = 0;
            TutorialSteps = new List<TutorialStep>();
        }

        public void NextStep() {
            if (currentStepIndex < (TutorialSteps.Count - 1)) {
                currentStepIndex++;
            }
        }

        public void resetTutorial() {
            currentStepIndex = 0;
        }
        

        public void setup()
        {
            TutorialStep step1 = new TutorialStep() { Title="Korak 1", Content="Dobrodosli u aplikaciju. Pratite sledece instrukcije, kako biste naucili da je koristite."};
            TutorialSteps.Add(step1);

            TutorialStep step2 = new TutorialStep() { Title = "Korak 2", Content = "Da biste napravili novu ucionicu, udjite u Novi->Ucionica. Ili odradite Alt+U" };
            TutorialSteps.Add(step2);

            TutorialStep step3 = new TutorialStep() { Title = "Korak 3", Content = "Odlicno. Popunite podatke za novu ucionicu. Onda je dodajte." };
            TutorialSteps.Add(step3);

            TutorialStep step4 = new TutorialStep() { Title = "Korak 4", Content = "Sada je vreme da dodate smer. Postupak je slican kao kod ucionice.\nZa ulazak u odgovarajucu formu pritisnite (Novi->smer ili Alt+D).\nNakon toga popunite formu." };
            TutorialSteps.Add(step4);

            TutorialStep step5 = new TutorialStep() { Title = "Korak 5", Content = "Dodajte predmet(Novi-> predmet ili Alt+P).\nPrimeticete da mozete da ga dodelite smeru koji ste prethodno napravili.\nTrudite se da predmet i ucionica(koju ste dodali) budu kompatibilni po pitanju zahteva(tabla,projektor...)." };
            TutorialSteps.Add(step5);

            TutorialStep step6 = new TutorialStep() { Title = "Korak 6", Content = "Udjite u semu rasporeda(Raspored ili Alt+R). Prikazace vam se prozor s rasporedom gde cete moci da pravite termine." };
            TutorialSteps.Add(step6);

            TutorialStep step7 = new TutorialStep() { Title = "Korak 7", Content = "U semi rasporeda imate tabelu s danima i vremenom. Tabela je na nivou jedne ucionice radi preglednosti.\nIz comboboxa gore je moguce izabrati ucionicu. Termini se prave prevlacenjem dostupnih predmeta sa strane na tablu. Probajte da uradite to." };
            TutorialSteps.Add(step7);

            TutorialStep lastStep = new TutorialStep() { Title = "Poslednji korak", Content = "Uspesno ste zavrsili ovaj tutorial i naucili kako da upotrebljavate osnovne funkcije aplikacije. Cestitamo!" };
            TutorialSteps.Add(lastStep);
        }
    }
}

namespace ScheduleApp.model
{
    public enum Steps { CLASSROOM_FORM_ENTERED = 1,CLASSROOM_ADDED,DEPARTMENT_ADDED,SUBJECT_ADDED,SCHEDULE_ENTERED,DRAG_AND_DROP_DONE}
    public class TutorialStep
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public TutorialStep() {
            Title = "Default title";
            Content = "Default content";
        }

        public TutorialStep(string title, string content) {
            Title = title;
            Content = content;
        }
    }
}
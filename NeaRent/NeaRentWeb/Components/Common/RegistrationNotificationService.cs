using Microsoft.Graph.Models.TermStore;

namespace NeaRentWeb.Components.Common
{
    public class NotifyResumeService
    {
        public EventHandler? ResumeClicked;

        public void OnResumeClicked(object sender, ResumeEventArgs e)
        {
            ResumeClicked(this, e);
        }

        public EventHandler? BackClicked;

        public void OnBackClicked(object sender, ResumeEventArgs e)
        {
            BackClicked(this, e);
        }

        public EventHandler? BackMayProgress;

        public void OnBackMayProgress(object sender, ResumeEventArgs e)
        {
            BackMayProgress(this, e);
        }

        public EventHandler? NextClicked;

        public void OnNextClicked(object sender, ResumeEventArgs e)
        {
            NextClicked(this, e);
        }

        public EventHandler? NextMayProgress;

        public void OnNextMayProgress(object sender, ResumeEventArgs e)
        {
            NextMayProgress(this, e);
        }

        public EventHandler? SigninClicked;

        public void OnSigninClicked(object sender, ResumeEventArgs e)
        {
            SigninClicked(this, e);
        }

        public EventHandler? EnteredCredentialMode;

        public void OnEnteredCredentialMode(object sender, ResumeEventArgs e)
        {
            EnteredCredentialMode(this, e);
        }

        public EventHandler? EnteredRegisterMode;

        public void OnEnteredRegisterMode(object sender, ResumeEventArgs e)
        {
            EnteredRegisterMode(this, e);
        }


        public EventHandler? SaveClicked;

        public void OnSaveClicked(object sender, ResumeEventArgs e)
        {
            SaveClicked(this, e);
        }

        public EventHandler<ResumeEventArgs>? ForcedStepChange;

        public void OnForcedStepChange(object sender, ResumeEventArgs e, int newStep)
        {
            e = new ResumeEventArgs(newStep);
            ForcedStepChange(this, e);
        }
    }

    public class ResumeEventArgs : EventArgs
    {
        private int step = 1;
        public int Step
        {
            get
            {
                return step;
            }

            set
            {
                step = value;
            }
        }

        public ResumeEventArgs() : base()
        {
        }

        public ResumeEventArgs(int step) : base()
        {
            this.step = step;
        }
    }
}

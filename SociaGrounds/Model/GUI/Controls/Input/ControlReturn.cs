namespace SociaGrounds.Model.GUI.Controls.Input
{
    public class ControlReturn
    {
        public bool IsReleased { get; }
        public int State { get; }

        public ControlReturn(bool isReleased, int state)
        {
            State = state;
            IsReleased = isReleased;
        }
    }
}

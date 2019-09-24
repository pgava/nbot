namespace nbot.contracts
{
    public class ActionProvider : IActions
    {
        private decimal currentLinearSpeed;
        private decimal currentAngularSpeed;
        private decimal currentX;
        private decimal currentY;
        private decimal currentDirection;
        private decimal forward;

        public void Ahead(decimal d)
        {
            forward = d;
        }
        public void Back(decimal d)
        {
            forward = -d;
        }
        public void Right(decimal d)
        {

        }
        public void Left(decimal d)
        {

        }

    }
}
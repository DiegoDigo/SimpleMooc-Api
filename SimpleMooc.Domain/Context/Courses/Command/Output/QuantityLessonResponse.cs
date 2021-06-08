namespace SimpleMooc.Domain.Context.Courses.Command.Output
{
    public class QuantityLessonResponse
    {
        public int Quantity { get; private set; }

        public QuantityLessonResponse(int quantity)
        {
            Quantity = quantity;
        }
    }
}
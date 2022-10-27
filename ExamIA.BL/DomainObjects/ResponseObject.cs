namespace ExamIA.BL.DomainObjects
{
    public class ResponseObject
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Value { get; set; }
    }
}

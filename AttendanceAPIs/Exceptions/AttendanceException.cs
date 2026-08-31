namespace PMPoshanWithAngular.Server.Exceptions
{
    public class AttendanceException : Exception
    {
        public AttendanceException() { }

        public AttendanceException(string message)
            : base(message) { }

        public AttendanceException(string message, Exception inner)
            : base(message, inner) { }
    }

}

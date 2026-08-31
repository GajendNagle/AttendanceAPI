namespace PMPoshanWithAngular.Server.Exceptions
{
    public class MPPMPoshaBadRequestExpection : Exception
    {
        public MPPMPoshaBadRequestExpection() { }

        public MPPMPoshaBadRequestExpection(string message)
            : base(message) { }

        public MPPMPoshaBadRequestExpection(string message, Exception inner)
            : base(message, inner) { }
    }

}

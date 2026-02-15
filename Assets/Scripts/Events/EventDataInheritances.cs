namespace Events
{
    //Event that informs subscribers of a debug log
    public class SendDebugLog : EventData
    {
        public readonly string Debuglog;

        public SendDebugLog(string givenLog) : base(EventType.ReceiveDebug)
        {
            Debuglog = givenLog;
        }
    }
    
    public class OutOfFuel : EventData
    {
        public OutOfFuel() : base(EventType.OutOfFuel)
        {
            
        }
    }
    
    
}

namespace Sharphook.Models
{
    public class AbstractedResponce<AbstractedObject>
    {
        public AbstractedObject? ResponceObject { get; private set; }
        public RatelimitInfo RatelimitInfo { get; private set; }

        public AbstractedResponce(AbstractedObject? responceObject, RatelimitInfo ratelimitInfo)
        {
            ResponceObject = responceObject;
            RatelimitInfo = ratelimitInfo;
        }
    }
}

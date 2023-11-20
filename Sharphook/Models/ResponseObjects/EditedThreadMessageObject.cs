using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharphook.Models.ResponseObjects
{
    public class EditedThreadMessageObject : ThreadMessageObject
    {
        public string edited_timestamp { get; set; }
    }
}

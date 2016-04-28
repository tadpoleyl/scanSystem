using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TWAINCapture
{
    class WIA_PROPERTIES
    {
        public const uint WIA_RESERVED_FOR_NEW_PROPS = 1024;
        public const uint WIA_DIP_FIRST = 2;
        public const uint WIA_DPA_FIRST = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
        public const uint WIA_DPC_FIRST = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
        //
        // Scanner only device properties (DPS)
        //
        public const uint WIA_DPS_FIRST = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
        public const uint WIA_DPS_DOCUMENT_HANDLING_STATUS = WIA_DPS_FIRST + 13;
        public const uint WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_FIRST + 14;

    }
}

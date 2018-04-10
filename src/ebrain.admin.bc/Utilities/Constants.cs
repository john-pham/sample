using System;
using System.Collections.Generic;
using System.Text;

namespace ebrain.admin.bc.Utilities
{
    public enum Behavior : byte
    {
        View = 1,
        Edit = 2,
        Delete = 4,
        Create = 8
    }


    public class Constants
    {
    }

    public enum EnumIOType
    {
        IORegisCourse = 1,
        IOInput = 2,
    }

    public enum EnumPayment
    {
        PaymentIOOUT = 1,
    }
}

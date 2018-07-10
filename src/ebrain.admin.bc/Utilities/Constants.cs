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
        public const string PAYMENTLIST = "376F3FFE-408A-49A8-B0EA-B69654E11B33";
        public const string PAYMENTDETAIL = "376F3FFE-408A-49A8-B0EA-B69654E11B34";
        public const int PAGING_DEFAULT = 1;
        public const int SIZE_DEFAULT = 20;
        public const string IMAGE_DEFAULT = "/uploads/logos/logo_superbrain.png";
    }

    public enum EnumIOType
    {
        IORegisCourse = 1,
        IOInput = 2,
    }

    public enum EnumPayment
    {
        PaymentIOOUT = 1,
        PaymentIOINPUT = 2,
    }
}

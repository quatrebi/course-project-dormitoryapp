//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DormitoryApp.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResidentModel
    {
        public int RID { get; set; }
        public int OrderNumber { get; set; }
        public System.DateTime CheckInDate { get; set; }
        public System.DateTime CheckOutDate { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public int GroupNumber { get; set; }
        public int RoomModelRID { get; set; }
    
        public virtual UserModel UserModel { get; set; }
        public virtual RoomModel RoomModel { get; set; }
    }
}

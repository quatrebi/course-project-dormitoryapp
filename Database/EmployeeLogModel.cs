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
    
    public partial class EmployeeLogModel
    {
        public int ELID { get; set; }
        public string Description { get; set; }
        public System.DateTime Datetime { get; set; }
        public int EmployeeModelUID { get; set; }
        public int RoomModelRID { get; set; }
    
        public virtual EmployeeModel EmployeeModel { get; set; }
        public virtual RoomModel RoomModel { get; set; }
    }
}
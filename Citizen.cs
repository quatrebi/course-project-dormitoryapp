//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DormitoryApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Citizen : Human
    {
    
        public virtual UniversityInfo UniversityInfo { get; set; }
        public virtual DormitoryInfo DormitoryInfo { get; set; }
    }
}

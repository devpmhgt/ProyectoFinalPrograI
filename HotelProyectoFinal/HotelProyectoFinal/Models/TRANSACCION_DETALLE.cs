//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelProyectoFinal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TRANSACCION_DETALLE
    {
        public int Documento { get; set; }
        public int IdServicio { get; set; }
        public int IDTipoDoc { get; set; }
        public string Cantidad { get; set; }
        public string Total { get; set; }
    
        public virtual SERVICIO SERVICIO { get; set; }
        public virtual TRANSACCION TRANSACCION { get; set; }
    }
}
